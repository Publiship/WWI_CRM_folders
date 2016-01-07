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
using DevExpress.Web.ASPxGridView;

public partial class order_courier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (isLoggedIn())
            {
                string _mode = get_token("mode");

                if (!Page.IsPostBack)
                {
                    bind_summary(_mode);
                    bind_tabs();
                    bind_commands();
                    set_mode(_mode);
                }

                bind_gridview();
            }
            else
            {
                //string _orderid = Request.QueryString["pid"] != null ? "&pid=" + Request.QueryString["pid"].ToString() : "";
                //Response.Redirect("~/Sys_Session_Login.aspx?" + "rp=" + wwi_security.EncryptString("Order_Search", "publiship") + _orderid);
                Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("order_search", "publiship"));
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
    }

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        //return HttpContext.Current.Session["user"] != null;
        return true;
    }
    
    #region form binding
    protected void bind_gridview()
    {
        //string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
        //CourierDetailsSubTableCollection _c = new CourierDetailsSubTableCollection().Where("OrderNumber", Comparison.Equals, _orderno);
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship")); //999909; //Request.QueryString["pid"] != null ? wwi_func.vint(Request.QueryString["pid"].ToString()) : 0;

        try
        {
            //query courier details table
            SubSonic.Query _q = new SubSonic.Query(Tables.CourierDetailsSubTable, "WWIProv").WHERE("OrderNumber", Comparison.Equals, _orderno);
            DataSet _ds = _q.ExecuteDataSet();

            this.dxgrdCourier.KeyFieldName = "CourierDetailID";
            this.dxgrdCourier.DataSource = _ds;
            this.dxgrdCourier.DataBind();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }//end try
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
                Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
                string[] _cols = { "OrderNumber", "PublishipOrder", "OfficeIndicator", "DateOrderCreated", "JobClosed", "HotJob" };
                SubSonic.SqlQuery _q = new SubSonic.SqlQuery();
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
    /// tab menu 
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
    /// command menu for courier details we only need a New option which we can then call through javascript
    /// so the courier xml item deos not contain a navigate url
    /// </summary>
    protected void bind_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='Courier']"; //you need xpath or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuCommand.DataSource = _xml;
        this.dxmnuCommand.DataBind();

    }

    protected void dxmnuCommand_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        {
            string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
            string _id = get_token("pno");
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        }
    }
#endregion

   

    #region call back events
    /// <summary>
    /// button to enter new courier in empty data template
    /// sets mode to "new"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnInsert_Click(object sender, EventArgs e)
    {
        set_mode("insert");
    }
    /// <summary>
    /// client contact callback fires when company id is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboContact_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_client_contact(e.Parameter);
     }
#endregion

    #region bind dlls
    
    /// <summary>
    /// rebind contact list when company is changed
    /// </summary>
    /// <param name="companyID">passed from comapny combo</param>
    protected void bind_client_contact(string companyID)
    {
        //must have a filter or display nothing
        if (!string.IsNullOrEmpty(companyID))
        {
            ASPxComboBox _dxcboContact = (ASPxComboBox) this.dxgrdCourier.FindEditFormTemplateControl("dxcboClientContact");
            if (_dxcboContact != null)
            {

                string[] _cols = { "ContactID, ContactName", "Email" };
                string[] _order = { "ContactName" };
                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.ContactTable).OrderAsc(_order);

                int _filter = -1;
                if (!string.IsNullOrEmpty(companyID))
                {
                    _filter = wwi_func.vint(companyID);
                    if (_filter > 0) { _qry.Where("CompanyID").IsEqualTo(_filter); }
                }

                IDataReader _rd1 = _qry.ExecuteReader();
                _dxcboContact.DataSource = _rd1;
                _dxcboContact.ValueField = "ContactID";
                _dxcboContact.TextField = "ContactName";
                _dxcboContact.DataBindItems();
        
            }
        }
    }
    #endregion

    #region incremental filtering for large combobox datasets
    //incremental filtering for large datasets on combos
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// company name is only available to publiship users
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("{0}%", e.Filter.ToString()));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.TextField = "CompanyName";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        //check value of e
        //when itemrequestedby fucntionality is being used in the grid edit template this will not be set when a new record is initialised
        //and will display as a js error "obejct reference not set to an instance of an object".
        //You can verify this by setting the ASPxGridView.EnableCallBacks property to "false"
        //this will then display the server error page not the js error message
        if (e.Value != null)
        {
            DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

            Int32 _id = 0;
            //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
            //{
            //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
            //    if (_companyid == -1)
            //    {
            _id = wwi_func.vint(e.Value.ToString());

            //use datareaders - much faster than loading into collections
            string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer" };

            //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").WhereExpression("CompanyID").IsEqualTo(_id);

            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "CompanyID";
            _combo.TextField = "CompanyName";
            _combo.DataBindItems();
            //  }
            //}
        }
    }
    //end incremental filtering of company name
    #endregion
      

    #region menu and tab control
    protected void dxmnuCommand_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
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
                        //bind_formview("Insert", 0); 
                        break;
                    }
                case "cmdEdit":
                    {
                        set_mode("Edit");
                        //for the mode to change call databind
                        //bind_formview("Edit", 0); 
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                        //this.fmvCourier.DeleteItem(); 
                        break;
                    }
                case "cmdUpdate":
                    {
                        //update_courier();
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        //bind_formview("ReadOnly", 0); 
                        //this.fmvCourier.UpdateItem(false);
                        //set_mode("view"); will return to view automatically after updating
                        break;
                    }
                case "cmdSave":
                    {
                        //no insert option needed for courier
                        //insert_courier();
                        set_mode("ReadOnly");
                        //rebind griview
                        this.dxgrdCourier.DataBind(); 
                        //DEPRECTED we are not using a formview. for the mode to change call databind
                        //bind_formview("ReadOnly", 0); 
                        //this.fmvCourier.InsertItem(false);
                        //set_mode("view"); will return to view automatically after inserting
                        break;
                    }
                case "cmdCancel":
                    {
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        //bind_formview("Insert", 0); 
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

    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    //this.fmvCourier.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    //this.fmvCourier.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    //this.fmvCourier.ChangeMode(FormViewMode.ReadOnly);
                    //avaialable mode depends on whether any records in courierdetailsubtable joined to order table on ordernumber
                    //if (this.fmvCourier.DataItemCount == 0)
                    //{ _menuitems.Add("Add"); }
                    //else
                    //{ _menuitems.Add("Edit"); }

                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to readonly
                {
                    //this.fmvCourier.ChangeMode(FormViewMode.ReadOnly);
                    //if (this.fmvCourier.DataItemCount == 0)
                    //{ _menuitems.Add("Add"); }
                    //else
                    //{ _menuitems.Add("Edit"); }
                    //enable_menu_items(_menuitems);
                    break;
                }
        }
    }
    

    /// <summary>
    /// enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_menu_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmnuCommand.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuCommand.Items[_ix].Name == "cmd" + active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmnuCommand.Items[_ix].ClientVisible = _isactive;
        }
    }//end menu names loop


    #endregion

    #region gridview crud events
    /// <summary>
    /// on row created get lookup values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdCourier_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            if (e.RowType == GridViewRowType.Data)
            {
                //company
                int _id = wwi_func.vint(e.GetValue("DocsDespatchID").ToString());
                string _txt = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _id);
                if (_txt != "")
                {
                    string[] _lx = _txt.Split(Environment.NewLine.ToCharArray());
                    //company
                    ASPxLabel _lbl = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblDocsDespatchIDView");
                    if (_lbl != null) { _lbl.Text = _lx[0]; }
                    //address
                    _lbl = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblDocsDespatchIDView2");
                    if (_lbl != null) { _lbl.Text = _txt.Replace(_lx[0], "").Trim(); ; }

                }

                //contact
                _id = wwi_func.vint(e.GetValue("ContactID").ToString());
                _txt = wwi_func.lookup_multi_values("ContactName,Email", "ContactTable", "ContactID", _id, "|");
                if (_txt != "")
                {
                    string[] _lx = _txt.Split("|".ToCharArray());
                    //name
                    ASPxLabel _lbl = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblContactIDView");
                    if (_lbl != null) { _lbl.Text = _lx[0].Trim(); }
                    //email
                    _lbl = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblContactIDView2");
                    if (_lbl != null) { _lbl.Text = _lx.Length > 0 ? _lx[1] : ""; }
                }

                //get the status value
                int _original = wwi_func.vint(e.GetValue("Original").ToString());
                string _target = _original <= 2 ? "Despatch Date" : "Date Emailed";

                //format date if original = 2 pass to docs despatched date, if 3 pass to emailed date
                string _dt = e.GetValue("DocumentationDespatched") != null ? wwi_func.vdatetime(e.GetValue("DocumentationDespatched").ToString()).ToShortDateString() : "";
                //set caption
                ASPxLabel _lb = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblDespatchDateCaption");
                if (_lb != null) { _lb.Text = _target; }
                _lb = (ASPxLabel)_grd.FindRowTemplateControl(e.VisibleIndex, "dxlblDespatchDateView");
                if (_lb != null) { _lb.Text = _dt; }
            }
            else if( e.RowType == GridViewRowType.EditForm)
            {
                //edit form stuff
                //populate company address label 
                int _id = wwi_func.vint(e.GetValue("DocsDespatchID").ToString());
                string _txt = wwi_func.lookup_multi_values("Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _id);
                if (_txt != "")
                {
                    //string[] _lx = _txt.Split(Environment.NewLine.ToCharArray());
                    //address
                    ASPxLabel _lb = (ASPxLabel)_grd.FindEditFormTemplateControl("dxlblAddress2");
                    if (_lb != null) { _lb.Text = _txt; }
                }

                //bind contact or we lose the contact name on edit
                ASPxComboBox _editor = (ASPxComboBox)_grd.FindEditFormTemplateControl("dxcboClientContact");
                if (_editor != null)
                {
                    string[] _cols = { "ContactID, ContactName", "Email" };
                    string[] _order = { "ContactName" };
                    SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.ContactTable).OrderAsc(_order);
                    if (_id > 0) { _qry.Where("CompanyID").IsEqualTo(_id); }

                    IDataReader _rd1 = _qry.ExecuteReader();
                    _editor.DataSource = _rd1;
                    _editor.ValueField = "ContactID";
                    _editor.TextField = "ContactName";
                    _editor.DataBind();
                    _editor.SelectedItem = _editor.Items.FindByValue(e.GetValue("ContactID"));
                }
            }//end if
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text += _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }   
    //end html row created

    /// <summary>
    /// add new courier
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdCourier_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            //courier details are joined to OrderTable on OrderNumber
            int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));

            if (_orderno > 0)
            {
                //get details
                CourierDetailsSubTable _t = new CourierDetailsSubTable();
                //REQUIRED to line to ordertable
                _t.OrderNumber = _orderno;

                //radiobutton
                if (e.NewValues["Original"] != null) { _t.Original = wwi_func.vint(e.NewValues["Original"].ToString()); }
            
                //dlls
                if (e.NewValues["DocsDespatchID"] != null) { _t.DocsDespatchID = wwi_func.vint(e.NewValues["DocsDespatchID"].ToString()); }
                if (e.NewValues["ContactID"] != null) { _t.ContactID = wwi_func.vint(e.NewValues["ContactID"].ToString()); }
                
                //text boxes
                //courier or email depending on which text is visible
                if (e.NewValues["CourierName"] != null) { _t.CourierName = e.NewValues["CourierName"].ToString(); }
                if (e.NewValues["EmailAddress"] != null) { _t.EmailAddress = e.NewValues["EmailAddress"].ToString(); }
                if (e.NewValues["AWBNumber"] != null) { _t.AWBNumber = e.NewValues["AWBNumber"].ToString(); }
  
                //dates depending on which date is visible
                if (e.NewValues["DocumentationDespatched"] != null) { _t.DocumentationDespatched = wwi_func.vdatetime(e.NewValues["DocumentationDespatched"].ToString()); }
                
                //insert record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order # {0} NOT updated. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_gridview(); 
        }
    }
    //end row inserting

    /// <summary>
    /// update courier
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdCourier_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            //courier details are joined to OrderTable on OrderNumber
            int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));

            int _courierid = wwi_func.vint(e.Keys["CourierDetailID"].ToString());   //get_courierdetailid();

            if (_orderno > 0 && _courierid > 0)
            {
                //get details
                CourierDetailsSubTable _t = new CourierDetailsSubTable(_courierid);

                //REQUIRED to line to ordertable
                _t.OrderNumber = _orderno;

                //radiobutton
                if (e.NewValues["Original"] != null) { _t.Original = wwi_func.vint(e.NewValues["Original"].ToString()); }

                //dlls
                if (e.NewValues["DocsDespatchID"] != null) { _t.DocsDespatchID = wwi_func.vint(e.NewValues["DocsDespatchID"].ToString()); }
                if (e.NewValues["ContactID"] != null) { _t.ContactID = wwi_func.vint(e.NewValues["ContactID"].ToString()); }

                //text boxes
                //courier or email depending on which text is visible
                if (e.NewValues["CourierName"] != null) { _t.CourierName = e.NewValues["CourierName"].ToString(); }
                if (e.NewValues["EmailAddress"] != null) { _t.EmailAddress = e.NewValues["EmailAddress"].ToString(); }
                if (e.NewValues["AWBNumber"] != null) { _t.AWBNumber = e.NewValues["AWBNumber"].ToString(); }

                //dates depending on which date is visible
                if (e.NewValues["DocumentationDespatched"] != null) { _t.DocumentationDespatched = wwi_func.vdatetime(e.NewValues["DocumentationDespatched"].ToString()); }
               
                //update record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order # {0} NOT updated. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit(); 
             bind_gridview(); 
        }
    }
    //end row inserting
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
