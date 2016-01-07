using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;

public partial class order_charge_allocation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            string _mode = get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
            bind_summary(_mode);

            if (!Page.IsPostBack)
            {
                bind_tabs();
                bind_commands();
                set_mode(_mode);
                bind_formview(_mode);
            }
         
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("order_search", "publiship"));
        }

    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #region form binding
    protected void fmvCharge_DataBound(object sender, EventArgs e)
    {
        //bind dlls here or they won't populate
        if (this.fmvCharge.CurrentMode != FormViewMode.ReadOnly)
        {
            //dlls for charge allocation are all bound to text from xml file
            bind_dlls();
        }
    }
    /// <summary>
    /// summary details for order
    /// </summary>
    protected void bind_summary(string mode)
    {
        try
        {

            if (mode != "insert")
            {
                Int32 _rowcount = 0;
                Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship")) ;
                string[] _cols = { "OrderNumber", "PublishipOrder", "OfficeIndicator", "DateOrderCreated", "JobClosed", "HotJob" };
                SubSonic.SqlQuery _q = new SubSonic.SqlQuery();
                //_q = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.OrderTable).WhereExpression("OrderNumber").IsEqualTo(_orderno);
                _q = DB.Select().From("view_order_summary").Where("OrderNumber").IsEqualTo(_orderno);    

                IDataReader _rd = _q.ExecuteReader();
                while (_rd.Read())
                {
                    this.dxlblOrderNo.Text = _rd["OrderNumber"].ToString();
                    //this.dxlblOfficeIndicator.Text = _rd["OfficeIndicator"].ToString();
                    //this.dxlblCreated.Text = "Date created: " + Convert.ToDateTime(_rd["DateOrderCreated"].ToString()).ToShortDateString();

                    //publiship order
                    bool? _checked = wwi_func.vbool(_rd["PublishipOrder"].ToString());
                    this.dximgJobPubliship.ClientVisible = _checked.Value;
                    this.dxlblJobPubliship.ClientVisible = _checked.Value;
                    //job closed
                    _checked = wwi_func.vbool(_rd["JobClosed"].ToString());
                    this.dximgJobClosed.ClientVisible = _checked.Value;
                    this.dxlblJobClosed.ClientVisible = _checked.Value;
                    //hot job
                    _checked = wwi_func.vbool(_rd["HotJob"].ToString());
                    this.dximgJobHot.ClientVisible = _checked.Value;
                    this.dxlblJobHot.ClientVisible = _checked.Value;
              
                    _rowcount++;
                }
                if (_rowcount == 0)
                {
                    this.dxlblInfo.Text = "No record found for Order number " + _orderno.ToString();
                    this.dxpnlMsg.ClientVisible = true;
                }
            }
            else //new record
            {
                this.dxlblOrderNo.Text = "New record";
            }
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    }

    /// <summary>
    /// tab menu items from xml file
    /// </summary>
    protected void bind_tabs()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\form_menus.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//menuitem[@AppliesTo='Order']"; //you need this or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data
        //DevExpress.Web.ASPxTabControl.ASPxTabControl _tab = (DevExpress.Web.ASPxTabControl.ASPxTabControl)this.FindControl("dxtabOrder");
        this.dxtabOrder.DataSource = _xml;
        this.dxtabOrder.DataBind();

    }
    /// <summary>
    /// once tabs have been initialised format urls with current order no (not decrypted) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxtabOrder_DataBound(object sender, EventArgs e)
    {
        string _orderid = get_token("pid");
        string _orderno = get_token("pno");
        string _label = this.dxlbOrderDetails1.Text.Replace("|", "").Trim();

        for (int _ix = 0; _ix < this.dxtabOrder.Tabs.Count; _ix++)
        {
            //match tab text to title label and use it to set active tab
            if (this.dxtabOrder.Tabs[_ix].Text == _label) { this.dxtabOrder.ActiveTabIndex = _ix; }
            //format urls with order no
            if (!string.IsNullOrEmpty(this.dxtabOrder.Tabs[_ix].NavigateUrl)) { this.dxtabOrder.Tabs[_ix].NavigateUrl = string.Format(this.dxtabOrder.Tabs[_ix].NavigateUrl, _orderid, _orderno); }
        }
    }
    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='GenericFormView']"; //you need this or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuData.DataSource = _xml;
        this.dxmnuData.DataBind();

    }

    protected void dxmnuData_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        //do NOT set the NavigateUrl property as it prevents itemclick event fom being fired! You can only have one or the other
        e.Item.NavigateUrl = "";

        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        //{
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pno");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        //}
    }



    /// <summary>
    /// allocation of charges
    /// </summary>
    protected void bind_formview(string viewmode)
    {
        string[] _key = { "OrderID" };
        string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
        int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));

        try
        {
            //have to use a collection as formview requires i IListSource, IEnumerable, or IDataSource.
            OrderTableCollection _tbc = new OrderTableCollection();
            OrderTable _tbl = viewmode != "Insert" ? new OrderTable(_orderid) : new OrderTable();
            _tbc.Add(_tbl);
            //bind formview to collection
            this.fmvCharge.DataSource = _tbc;
            this.fmvCharge.DataBind(); 
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
    }


    /// <summary>
    /// bind data combos
    /// </summary>
    protected void bind_dlls()
    {
        try
        {
            string[] _dllname = { "dxcboClientsTerms", "dxcboOriginTrucking", "DXCBOOrignTHC", "dxcboOriginDocs", "dxcboFreight", 
                                    "dxcboDestTHC", "dxcboDestPalletisation", "dxcboCustomsClearance", "dxcboDeliveryCharges" };

            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ddl_charge_allocation.xml";

            for (int _ix = 0; _ix < _dllname.Length; _ix++)
            {
                // pass _qryFilter to have keyword-filter RSS Feed
                // i.e. _qryFilter = XML -> entries with XML will be returned
                DataSet _ds = new DataSet();
                _ds.ReadXml(_path);
                DataView _dv = _ds.Tables[0].DefaultView;
                _dv.RowFilter = "ddls ='" + _dllname[_ix] + "'";

                //populate dll
                ASPxComboBox _cbo = (ASPxComboBox)this.fmvCharge.FindControl(_dllname[_ix]);
                if (_cbo != null)
                {
                    _cbo.DataSource = _dv;
                    _cbo.ValueType = typeof(int);
                    _cbo.TextField = "name";
                    _cbo.ValueField = "value";
                    _cbo.DataBindItems();
                }
            }
            
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    
    }
    //end bind combos
    #endregion

    #region formview events
    protected void fmvCharge_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvCharge.ChangeMode(e.NewMode);
    }
    #endregion

    #region crud events
    protected void update_charges()
    {
        try
        {
            int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
            //Int32 _orderid = this.dxhfOrder.Contains("pid") ? wwi_func.vint(this.dxhfOrder.Get("pid").ToString()) : 0;
            if (_orderid > 0)
            {
                OrderTable _ot = new OrderTable(_orderid);

                ASPxComboBox _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboClientsTerms");
                if (_cbo != null) { _ot.ClientsTerms = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOriginTrucking");
                if (_cbo != null) { _ot.OriginTrucking = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOrignTHC");
                if (_cbo != null) { _ot.OrignTHC = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOriginDocs");
                if (_cbo != null) { _ot.OriginDocs = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboFreight");
                if (_cbo != null) { _ot.Freight = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDestTHC");
                if (_cbo != null) { _ot.DestTHC = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDestPalletisation");
                if (_cbo != null) { _ot.DestPalletisation = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboCustomsClearance");
                if (_cbo != null) { _ot.CustomsClearance = _cbo.Text.ToString(); }

                _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDeliveryCharges");
                if (_cbo != null) { _ot.DeliveryCharges = _cbo.Text.ToString(); }

                ASPxMemo _mem = (ASPxMemo)this.fmvCharge.FindControl("dxmemCoLoaderComments");
                if (_mem != null) { _ot.CoLoaderComments = _mem.Text.ToString(); }
                
                _ot.Save();
                //confirm saved
                //string _msg = "Order " + this.dxlblOrderNo.Text.ToString() + " has been updated";
                //this.dxlblInfo.Text = _msg;
                //this.dxpnlMsg.ClientVisible = true;
            }
            else
            {
                string _err = "Unable to update charges. Order number not found.";
                this.dxlblErr.Text = _err;
                this.dxpnlErr.ClientVisible = true;
            }
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// we won't need this as charges are in ordertable which must have key information saved before chrge allocations can be entered
    /// </summary>
    protected void insert_charges()
    {
        try
        {
            OrderTable _ot = new OrderTable();

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboClientsTerms");
            if (_cbo != null) { _ot.ClientsTerms = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOriginTrucking");
            if (_cbo != null) { _ot.OriginTrucking = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOrignTHC");
            if (_cbo != null) { _ot.OrignTHC = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboOriginDocs");
            if (_cbo != null) { _ot.OriginDocs = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboFreight");
            if (_cbo != null) { _ot.Freight = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDestTHC");
            if (_cbo != null) { _ot.DestTHC = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDestPalletisation");
            if (_cbo != null) { _ot.DestPalletisation = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboCustomsClearance");
            if (_cbo != null) { _ot.CustomsClearance = _cbo.Text.ToString(); }

            _cbo = (ASPxComboBox)this.fmvCharge.FindControl("dxcboDeliveryCharges");
            if (_cbo != null) { _ot.DeliveryCharges = _cbo.Text.ToString(); }

            ASPxMemo _mem = (ASPxMemo)this.fmvCharge.FindControl("dxmemCoLoaderComments");
            if (_mem != null) { _ot.CoLoaderComments = _cbo.Text.ToString(); }

            _ot.Save();

            //return new order id
            Int32 _newid = (Int32)_ot.GetPrimaryKeyValue();
                            
            //confirm saved
            //string _msg = "Order " + _newid.ToString() + " has been updated";
            //this.dxlblOrderNo.Text = _newid.ToString(); 
            //this.dxlblInfo.Text = _msg;
            //this.dxpnlMsg.ClientVisible = true;
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end insert
      #endregion

    #region menu and tab control
    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvCharge.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvCharge.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvCharge.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to readonly
                {
                    _menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }

    protected void dxmnuData_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();
        List<string> _menuitems = new List<string>();

        try
        {
            switch (_item)
            {
                case "cmdNew":
                    {
                        set_mode("Insert");
                        //for the mode to change call databind
                        bind_formview("Insert"); 
                        break;
                    }
                case "cmdEdit":
                    {
                        set_mode("Edit");
                        bind_formview("Edit"); 
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                        //this.formOrder.DeleteItem(); 
                        break;
                    }
                case "cmdUpdate":
                    {
                        update_charges();
                        set_mode("ReadOnly");
                        bind_formview("ReadOnly"); 
                        break;
                    }
                case "cmdSave":
                    {
                        //no insert option needed for charges as it os part of OrderTable data
                        insert_charges();
                        set_mode("ReadOnly");
                        bind_formview("ReadOnly"); 
                        break;
                    }
                case "cmdCancel":
                    {
                        set_mode("ReadOnly");
                        bind_formview("ReadOnly"); 
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //end switch
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
        }
    }
    //end menu commands

    /// <summary>
    /// enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_menu_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmnuData.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuData.Items[_ix].Name == "cmd" + active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmnuData.Items[_ix].ClientVisible = _isactive;
        }
    }//end menu names loop

    #endregion

    #region functions

    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_token(string namedtoken)
    {
        string _value = this.dxhfOrder.Contains(namedtoken) ? this.dxhfOrder.Get(namedtoken).ToString() : null;

        if (string.IsNullOrEmpty(_value))
        {
            _value = Page.Request[namedtoken] != null ? HttpUtility.UrlDecode(Page.Request[namedtoken].ToString(), System.Text.Encoding.Default) : null;
        }

        //don't decrypt it here
        //if (_value != null) { _value = wwi_security.DecryptString(_value, "publiship"); }
        return _value;
    }
    //end get saved token
    /// <summary>
    /// some formating functions for Evals  this does not work with Bind you can't enclose Bind with functions
    /// </summary>
    /// <param name="testvalue">object to check</param>
    /// <returns>empty string if null or object value</returns>
    public string nullValue(object testvalue)
    {
        if (testvalue == null)
            return "";
        return testvalue.ToString();

    }

    #endregion

}
