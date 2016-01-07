using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using SubSonic;

public partial class order_deliveries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                //do before seting mode
                bind_tabs();
                bind_commands();
                bind_summary();
            }

            //not required on this page not using formview
            //string _mode = get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
            //set_mode(_mode);
            bind_deliveries();
            bind_status_types();
            //not required we can get this info when formView is databound
            //bind_summary(_mode);
            //show detail grid
            this.dxgridDeliveries.DetailRows.ExpandAllRows(); 
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
    #region master grid and form binding
    /// <summary>
    /// sumary details
    /// </summary>
    /// <param name="mode"></param>
    protected void bind_summary()
    {
        try
        {
            Int32 _rowcount = 0;
            Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
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
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end bind summary

    protected void bind_deliveries()
    {
        try
        {
            int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
            //query delivefries table
            SubSonic.Query _q = new SubSonic.Query(DAL.Logistics.Tables.DeliverySubTable, "WWIProv").WHERE("OrderNumber", Comparison.Equals, _orderno);
            DataSet _ds = _q.ExecuteDataSet();
            this.dxgridDeliveries.KeyFieldName = "DeliveryID";
            this.dxgridDeliveries.DataSource = _ds;
            this.dxgridDeliveries.DataBind();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
   
    /// <summary>
    /// on binding expand detail rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_OnDataBound(object sender, EventArgs e)
    {
        ((ASPxGridView)sender).DetailRows.ExpandAllRows();
    }
    

    protected void dxgridDeliveries_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        try
        {
            string _s = "";
            int _id = 0;

            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
            {

                _id = wwi_func.vint(e.GetValue("DeliveryID").ToString());

                if (_id > 0)
                {
                    _s = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo,PalletDims,MaxPalletWeight,MaxPalletHeight,SpecialDeliveryInstructions", "view_delivery_instruction", "DeliveryID", _id, "|");
                    if (!string.IsNullOrEmpty(_s))
                    {
                        string[] _labels = { "DeliveryName", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "PalletSpec", "PalletWeight", "PalletHeight", "Instructions" };
                        string[] _elements = _s.Split("|".ToCharArray());

                        for (int _ix = 0; _ix < _labels.Length; _ix++)
                        {
                            ASPxLabel _lbl = (ASPxLabel)this.dxgridDeliveries.FindRowTemplateControl(e.VisibleIndex, "dxlbl" + _labels[_ix]);
                            if (_lbl != null)
                            {
                                _lbl.Text += _elements[_ix] + Environment.NewLine;
                            }
                        }//end loop
                    }//end if
                }

                //current status
                _id = e.GetValue("CurrentStatusID") != null ? wwi_func.vint(e.GetValue("CurrentStatusID").ToString()) : 0;
                string _f = "";
                if (_id > 0)
                {
                    _f = wwi_func.lookup_value("Field1", "CurrentStatus", "ID", _id);
                    ASPxLabel _lbl = (ASPxLabel)this.dxgridDeliveries.FindRowTemplateControl(e.VisibleIndex, "dxlblCurrentStatusID");
                    if (_lbl != null) { _lbl.Text = _f; }
                }

                //modify javascript on button click so we can drop dll sub decks into hidden field to pass onto edit form
                //will save making a callback to the server 
                ASPxButton _btn = (ASPxButton)this.dxgridDeliveries.FindRowTemplateControl(e.VisibleIndex, "dxbtnEdit");
                if (_btn != null)
                {
                    _s += "|" + _f;
                    _btn.ClientSideEvents.Click = "function(s, e) { hfOrder.Set(\"subdecks\",\"" + _s + "\"); gridDeliveries.StartEditRow(" + e.VisibleIndex.ToString() + "); }"; //gridDeliveries.StartEditRow(' + Container.VisibleIndex + ')

                }

            }//end data template
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }
    }//end html row prepared

    /// <summary>
    /// copy combobox values to non-updateable labels
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        try
        {
            string _s = this.dxhfOrder.Contains("subdecks") ? this.dxhfOrder.Get("subdecks").ToString() : "";
            if (!string.IsNullOrEmpty(_s))
            {
                string[] _labels = { "DeliveryAddressSub", "DeliveryAddressSub", "DeliveryAddressSub", "DeliveryAddressSub", "DeliveryAddressSub", "PalletSpecSub", "PalletWeightSub", "PalletHeightSub", "InstructionsSub" };
                string[] _elements = _s.Split("|".ToCharArray());

                //don't need element 0 company name as we have the combo there
                for (int _ix = 1; _ix < _labels.Length; _ix++)
                {
                    if (_elements[_ix] != "")
                    {
                        string test = "dxlbl" + _labels[_ix];
                        //find label at ix -1 as we want to start labels at element 0
                        ASPxLabel _lbl = (ASPxLabel)this.dxgridDeliveries.FindEditFormTemplateControl("dxlbl" + _labels[_ix - 1]);
                        if (_lbl != null)
                        {
                            _lbl.Text += _elements[_ix] + Environment.NewLine;
                        }
                    }
                }//end loop
            }
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
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
        _xml.XPath = "//item[@Filter='NoCommands']"; //you need this or tab will not databind!
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
            string _orderno = get_token("pno");
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _orderno); }
        }
    }
    #endregion

    #region detail grid binding for delivery titles
    /// <summary>
    /// binding titles in child grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_BeforePerformDataSelect(object sender, EventArgs e)
    {
        try
        {
            ASPxGridView _detail = (ASPxGridView)sender;
            String[] _keys = { "DeliveryID" };
            int _deliveryid = (int)_detail.GetMasterRowFieldValues(_keys);

            SubSonic.Query _q = new SubSonic.Query(DAL.Logistics.Tables.DeliverySubSubTable, "WWIProv").WHERE("DeliveryID", Comparison.Equals, _deliveryid);
            DataSet _ds = _q.ExecuteDataSet();
            _detail.KeyFieldName = "SubDeliveryID";
            _detail.DataSource = _ds;
            //DO NOT CALL DATABIND during BeforePerformDataSelect as it causes an infinite loop
            //_detail.DataBind();

            //deprecated we are binding in code behind
            //this.odsDeliveryTitles.SelectParameters["DeliveryID"].DefaultValue = _deliveryid.ToString();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
        //end try
    }
    #endregion

    #region dll binding
    protected void bind_status_types()
    {
        GridViewDataComboBoxColumn _combo = this.dxgridDeliveries.Columns["colCurrentStatusID"] as GridViewDataComboBoxColumn;

        string[] _cols = { "ID, Field1" };
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.CurrentStatus);

        IDataReader _rd = _query.ExecuteReader();

        _combo.PropertiesComboBox.DataSource = _rd;
        _combo.PropertiesComboBox.ValueType = typeof(int);
        _combo.PropertiesComboBox.TextField = "Field1";
        _combo.PropertiesComboBox.ValueField = "ID";
    }
    #endregion

    #region incremental filtering for large combobox datasets
    //incremental filtering for large datasets on combos
    /// <summary>
    /// incremental filtering and partial loading of vessels for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboAddress_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "PalletDims", "MaxPalletWeight", "MaxPalletHeight", "SpecialDeliveryInstructions" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("{0}%", e.Filter.ToString()));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "CompanyName";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcboAddress_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;

        Int32 _id = 0;
        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "PalletDims", "MaxPalletWeight", "MaxPalletHeight", "SpecialDeliveryInstructions" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").WhereExpression("CompanyID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "CompanyName";
        _combo.DataBindItems();
        //  }
        //}
    }
    //end address

    /// <summary>
    /// titles detail grid
    /// </summary>
    /// <summary>
    /// applies to the Title column editform template
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// company name is only available to publiship users
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbotitle_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        string _orderno = this.dxlblOrderNo.Text.ToString();
        if (_orderno != "")
        {
            ASPxComboBox _combo = (ASPxComboBox)source;
            //ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
            //      this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));

            //use datareaders - much faster than loading into collections
            string[] _cols = { "ItemTable.TitleID, ItemTable.Title" };
            SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
            _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.ItemTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "TitleID").Where("Title").StartsWith(string.Format("{0}%", e.Filter.ToString())).And("OrderNumber").IsEqualTo(_orderno);

            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "TitleID";
            _combo.ValueType = typeof(Int32);
            _combo.TextField = "Title";
            _combo.DataBind();
        }
    }


    protected void dxcbotitle_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;
        //ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
        //       this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "ItemTable.TitleID, ItemTable.Title" };
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.ItemTable).WhereExpression("TitleID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "TitleID";
        _combo.ValueType = typeof(Int32);
        _combo.TextField = "Title";
        _combo.DataBind();

    }
    #endregion

    #region deliveries crud events
    /// <summary>
    /// custom call backs for deliveries
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != null)
        {
            string[] _params = e.Parameters.Split("|".ToCharArray());
            if (_params[0] == "AddTitles")
            {
                int _index = wwi_func.vint(_params[1]); //visible row
                ASPxGridView _grd = (ASPxGridView)sender;
                ASPxGridView _sub = (ASPxGridView)_grd.FindDetailRowTemplateControl(_index, "dxgridTitles"); //find detail grid

                if (_sub != null)
                {
                    int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship")); //get order number
                    int _deliveryid = wwi_func.vint(_sub.GetMasterRowKeyValue().ToString()); //get parent key id
                    append_order_titles(_orderno, _deliveryid); //add titles to this detials grid
                    _sub.DataBind(); //rebind
                }//end if sub not null
            }//end if params = addtitles
        }//end if parameters not null
    }
    //end custom call back
    /// <summary>
    /// insert
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        int _newkey = 0;

        try
        {
            //courier details are joined to OrderTable on OrderNumber
        
            if (_orderno > 0)
            {
                //get details
                DeliverySubTable _t = new DeliverySubTable();
                //REQUIRED to link to ordertable
                _t.OrderNumber = _orderno;
                //dlls
                if (e.NewValues["DeliveryAddress"] != null) { _t.DeliveryAddress = wwi_func.vint(e.NewValues["DeliveryAddress"].ToString()); }
                if (e.NewValues["CurrentStatusID"] != null) { _t.CurrentStatusID = wwi_func.vint(e.NewValues["CurrentStatusID"].ToString()); }

                //dates
                if (e.NewValues["CurrentStatusDate"] != null) { _t.CurrentStatusDate = wwi_func.vdatetime(e.NewValues["CurrentStatusDate"].ToString()); }
                if (e.NewValues["StatusDate"] != null) { _t.StatusDate = wwi_func.vdatetime(e.NewValues["StatusDate"].ToString()); }//status last updated
                
                //tick boxes
                _t.Delivered = e.NewValues["Delivered"] != null? wwi_func.vbool(e.NewValues["Delivered"].ToString()) == true? true: false: false;
                _t.PODRequired = e.NewValues["PODRequired"] != null? wwi_func.vbool(e.NewValues["PODRequired"].ToString()) == true ? true : false: false;
                //what is this field for and where is it updated?
                //_t.Added = wwi_func.vbool(e.NewValues["Added"].ToString()) == true ? true : false;

                //memo
                if (e.NewValues["SpecialInstructions"] != null) { _t.SpecialInstructions = e.NewValues["SpecialInstructions"].ToString(); }
  
                //insert record
                _t.Save();
                //get new deliveryID and add to hidden fields - on rowinserted we can use it to populate the new delivery with titles
                _newkey = wwi_func.vint(_t.GetPrimaryKeyValue().ToString());
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Delivery # {0} NOT created. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_deliveries();
            _grd.FocusedRowIndex = _grd.FindVisibleIndexByKeyValue(_newkey);
            //normally we'd do this on rowinserted event but CancelEdit prevents rowinserted event from firing
            append_order_titles(_orderno, _newkey); 
        }

    }
    /// <summary>
    /// update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        int _deliveryid = wwi_func.vint(e.Keys["DeliveryID"].ToString());

        try
        {
            //courier details are joined to OrderTable on OrderNumber

            if (_orderno > 0 && _deliveryid > 0)
            {
                //get details
                DeliverySubTable _t = new DeliverySubTable(_deliveryid);
                //REQUIRED to line to ordertable
                _t.OrderNumber = _orderno;
                //dlls
                if (e.NewValues["DeliveryAddress"] != null) { _t.DeliveryAddress = wwi_func.vint(e.NewValues["DeliveryAddress"].ToString()); }
                if (e.NewValues["CurrentStatusID"] != null) { _t.CurrentStatusID = wwi_func.vint(e.NewValues["CurrentStatusID"].ToString()); }

                //dates
                if (e.NewValues["CurrentStatusDate"] != null) { _t.CurrentStatusDate = wwi_func.vdatetime(e.NewValues["CurrentStatusDate"].ToString()); }
                if (e.NewValues["StatusDate"] != null) { _t.StatusDate = wwi_func.vdatetime(e.NewValues["StatusDate"].ToString()); }//status last updated

                //tick boxes
                _t.Delivered = e.NewValues["Delivered"] != null ? wwi_func.vbool(e.NewValues["Delivered"].ToString()) == true ? true : false : false;
                _t.PODRequired = e.NewValues["PODRequired"] != null ? wwi_func.vbool(e.NewValues["PODRequired"].ToString()) == true ? true : false : false;
                //what is this field for and where is it updated?
                //_t.Added = wwi_func.vbool(e.NewValues["Added"].ToString()) == true ? true : false;

                //memo
                if (e.NewValues["SpecialInstructions"] != null) { _t.SpecialInstructions = e.NewValues["SpecialInstructions"].ToString(); }
                //insert record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Delivery # {0} NOT updated. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_deliveries();
        }
    }

    /// <summary>
    /// delete selected row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridDeliveries_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _deliveryid = wwi_func.vint(e.Keys["DeliveryID"].ToString());

        try
        {
            if (_deliveryid > 0)
            {
                //get details
                DeliverySubTable.Delete("DeliveryID", _deliveryid);
                
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Delivery # {0} NOT deleted. Error: {1}", _deliveryid, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_deliveries();
        }
    }
    #endregion

    #region titles crud events
    /// <summary>
    /// fires when edit row get current value of combobox and populate on combobox databound or we will lose initial text
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView _grid = (ASPxGridView)sender;
        string[] _fields = { "Title" };
        string _title = _grid.GetRowValuesByKeyValue(e.EditingKeyValue, _fields).ToString();
        Session["Title"] = _title;
    }
    /// <summary>
    /// we need this or the current title will be lost when the combo binds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbotitle_DataBound(object sender, EventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)sender;
        if (Session["Title"] != null) { _combo.Text = Session["Title"].ToString(); Session["Title"] = null; }
    }
    /// <summary>
    /// callback fires when current status id is changed
    /// set status last updated date to current date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        ASPxDateEdit _dte = _grd.FindEditRowCellTemplateControl((GridViewDataColumn)_grd.Columns["StatusDate"], "ASPxGridViewTemplateReplacement3") as ASPxDateEdit;
        _dte.Value = DateTime.Now.ToShortDateString();
    }
    /// <summary>
    /// update record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _deliveryid = wwi_func.vint(_grd.GetMasterRowKeyValue().ToString());
        int _subdeliveryid = wwi_func.vint(e.Keys["SubDeliveryID"].ToString());

        try
        {
            if (_deliveryid > 0 && _subdeliveryid > 0)
            {
                //need to get title from edit itemtemplate it won't be found in NewValues
                string _title = "";
                int? _titleid = null;
                int? _intnull = null;
                GridViewDataColumn _col = (GridViewDataColumn)_grd.Columns["colTitle"];
                ASPxComboBox _cbo = (ASPxComboBox)_grd.FindEditRowCellTemplateControl(_col, "dxcbotitle");
                if (_cbo != null)
                {
                    _title = _cbo.Text != null ? _cbo.Text.ToString() : "";
                    _titleid = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;

                }

                DeliverySubSubTable _t = new DeliverySubSubTable(_subdeliveryid);
                _t.Title = _title;
                _t.TitleID = _titleid;
                //18.02.15 pencepercopy field renamed actualppc
                if (e.NewValues["ActualPPC"] != null) { _t.ActualPPC = wwi_func.vdecimal(e.NewValues["ActualPPC"].ToString()); } else { _t.ActualPPC = null;  }
                if (e.NewValues["Copies"] != null) { _t.Copies = wwi_func.vint(e.NewValues["Copies"].ToString()); } else { _t.Copies  = null; }
                if (e.NewValues["CopiesPerCarton"] != null) { _t.CopiesPerCarton = wwi_func.vint(e.NewValues["CopiesPerCarton"].ToString()); } else { _t.CopiesPerCarton = null; }
                if (e.NewValues["CartonLength"] != null) { _t.CartonLength = wwi_func.vdecimal(e.NewValues["CartonLength"].ToString()); } else { _t.CartonLength = null; }
                if (e.NewValues["CartonDepth"] != null) { _t.CartonDepth = wwi_func.vdecimal(e.NewValues["CartonDepth"].ToString()); }  else { _t.CartonDepth = null;  }
                if (e.NewValues["CartonHeight"] != null) { _t.CartonHeight = wwi_func.vdecimal(e.NewValues["CartonHeight"].ToString()); } else { _t.CartonHeight = null; }
                if (e.NewValues["TotalCartons"] != null) { _t.TotalCartons = wwi_func.vint(e.NewValues["TotalCartons"].ToString()); } else { _t.TotalCartons = null; }
                if (e.NewValues["CartonWeight"] != null) { _t.CartonWeight = wwi_func.vfloat(e.NewValues["CartonWeight"].ToString()); } else { _t.CartonWeight = null; }
                if (e.NewValues["LastCarton"] != null) { _t.LastCarton = wwi_func.vfloat(e.NewValues["LastCarton"].ToString()); } else { _t.LastCarton = null; }
                if (e.NewValues["CartonsPerFullPallet"] != null) { _t.CartonsPerFullPallet = wwi_func.vint(e.NewValues["CartonsPerFullPallet"].ToString()); } else { _t.CartonsPerFullPallet = null; }
                if (e.NewValues["FullPallets"] != null) { _t.FullPallets = wwi_func.vint(e.NewValues["FullPallets"].ToString()); }  else { _t.FullPallets = null;  }
                if (e.NewValues["CartonsPerPartPallet"] != null) { _t.CartonsPerPartPallet = wwi_func.vint(e.NewValues["CartonsPerPartPallet"].ToString()); } else { _t.CartonsPerPartPallet = null; }
                if (e.NewValues["Jackets"] != null) { _t.Jackets = wwi_func.vint(e.NewValues["Jackets"].ToString()); } else { _t.Jackets  = null; }
                if (e.NewValues["PartPallets"] != null) { _t.PartPallets = wwi_func.vint(e.NewValues["PartPallets"].ToString()); } else { _t.PartPallets = null; }
                if (e.NewValues["TotalConsignmentWeight"] != null) { _t.TotalConsignmentWeight = wwi_func.vfloat(e.NewValues["TotalConsignmentWeight"].ToString()); } else { _t.TotalConsignmentWeight = null; }
                if (e.NewValues["TotalConsignmentCube"] != null) { _t.TotalConsignmentCube = wwi_func.vfloat(e.NewValues["TotalConsignmentCube"].ToString()); } else { _t.TotalConsignmentCube = null; }
                if (e.NewValues["Remarks"] != null) { _t.Remarks = e.NewValues["Remarks"].ToString(); } else { _t.Remarks = ""; }
                //new fields 18.02.15
                if (e.NewValues["EstimatedPPC"] != null) { _t.EstimatedPPC = wwi_func.vdecimal(e.NewValues["EstimatedPPC"].ToString()); } else { _t.EstimatedPPC = null; }
                if (e.NewValues["BookLength"] != null) { _t.BookLength = wwi_func.vdecimal(e.NewValues["BookLength"].ToString()); } else { _t.BookLength = null; }
                if (e.NewValues["BookWidth"] != null) { _t.BookWidth = wwi_func.vdecimal(e.NewValues["BookWidth"].ToString()); } else { _t.BookWidth = null; }
                if (e.NewValues["BookDepth"] != null) { _t.BookDepth = wwi_func.vdecimal(e.NewValues["BookDepth"].ToString()); } else { _t.BookDepth = null; }
                if (e.NewValues["BookWeight"] != null) { _t.BookWeight = wwi_func.vdecimal(e.NewValues["BookWeight"].ToString()); } else { _t.BookWeight = null; }
                //save record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Title # {0} NOT updated. Error: {1}", _deliveryid, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            _grd.DataBind();
        }
    }
    /// <summary>
    /// insert new title
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _deliveryid = wwi_func.vint(_grd.GetMasterRowKeyValue().ToString());

        try
        {
            if (_deliveryid > 0)
            {
                //need to get title from edit itemtemplate it won't be found in NewValues
                string _title = "";
                int? _titleid = null;
                int? _intnull = null;

                GridViewDataColumn _col = (GridViewDataColumn)_grd.Columns["colTitle"];
                ASPxComboBox _cbo = (ASPxComboBox)_grd.FindEditRowCellTemplateControl(_col, "dxcbotitle");
                if (_cbo != null)
                {
                    _title = _cbo.Text != null ? _cbo.Text.ToString() : "";
                    _titleid = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;

                }

                DeliverySubSubTable _t = new DeliverySubSubTable();
                //required
                _t.DeliveryID = _deliveryid;
                //
                _t.Title = _title;
                _t.TitleID = _titleid;
                //18.02.15 pencepercopy field renamed actualppc
                if (e.NewValues["ActualPPC"] != null) { _t.ActualPPC = wwi_func.vdecimal(e.NewValues["ActualPPC"].ToString()); } else { _t.ActualPPC = null; }
                if (e.NewValues["Copies"] != null) { _t.Copies = wwi_func.vint(e.NewValues["Copies"].ToString()); } else { _t.Copies = null; }
                if (e.NewValues["CopiesPerCarton"] != null) { _t.CopiesPerCarton = wwi_func.vint(e.NewValues["CopiesPerCarton"].ToString()); } else { _t.CopiesPerCarton = null; }
                if (e.NewValues["CartonLength"] != null) { _t.CartonLength = wwi_func.vdecimal(e.NewValues["CartonLength"].ToString()); } else { _t.CartonLength = null; }
                if (e.NewValues["CartonDepth"] != null) { _t.CartonDepth = wwi_func.vdecimal(e.NewValues["CartonDepth"].ToString()); } else { _t.CartonDepth = null; }
                if (e.NewValues["CartonHeight"] != null) { _t.CartonHeight = wwi_func.vdecimal(e.NewValues["CartonHeight"].ToString()); } else { _t.CartonHeight = null; }
                if (e.NewValues["TotalCartons"] != null) { _t.TotalCartons = wwi_func.vint(e.NewValues["TotalCartons"].ToString()); } else { _t.TotalCartons = null; }
                if (e.NewValues["CartonWeight"] != null) { _t.CartonWeight = wwi_func.vfloat(e.NewValues["CartonWeight"].ToString()); } else { _t.CartonWeight = null; }
                if (e.NewValues["LastCarton"] != null) { _t.LastCarton = wwi_func.vfloat(e.NewValues["LastCarton"].ToString()); } else { _t.LastCarton = null; }
                if (e.NewValues["CartonsPerFullPallet"] != null) { _t.CartonsPerFullPallet = wwi_func.vint(e.NewValues["CartonsPerFullPallet"].ToString()); } else { _t.CartonsPerFullPallet = null; }
                if (e.NewValues["FullPallets"] != null) { _t.FullPallets = wwi_func.vint(e.NewValues["FullPallets"].ToString()); } else { _t.FullPallets = null; }
                if (e.NewValues["CartonsPerPartPallet"] != null) { _t.CartonsPerPartPallet = wwi_func.vint(e.NewValues["CartonsPerPartPallet"].ToString()); } else { _t.CartonsPerPartPallet = null; }
                if (e.NewValues["Jackets"] != null) { _t.Jackets = wwi_func.vint(e.NewValues["Jackets"].ToString()); } else { _t.Jackets = null; }
                if (e.NewValues["PartPallets"] != null) { _t.PartPallets = wwi_func.vint(e.NewValues["PartPallets"].ToString()); } else { _t.PartPallets = null; }
                if (e.NewValues["TotalConsignmentWeight"] != null) { _t.TotalConsignmentWeight = wwi_func.vfloat(e.NewValues["TotalConsignmentWeight"].ToString()); } else { _t.TotalConsignmentWeight = null; }
                if (e.NewValues["TotalConsignmentCube"] != null) { _t.TotalConsignmentCube = wwi_func.vfloat(e.NewValues["TotalConsignmentCube"].ToString()); } else { _t.TotalConsignmentCube = null; }
                if (e.NewValues["Remarks"] != null) { _t.Remarks = e.NewValues["Remarks"].ToString(); } else { _t.Remarks = ""; }
                //new fields 18.02.15
                if (e.NewValues["EstimatedPPC"] != null) { _t.EstimatedPPC = wwi_func.vdecimal(e.NewValues["EstimatedPPC"].ToString()); } else { _t.EstimatedPPC = null; }
                if (e.NewValues["BookLength"] != null) { _t.BookLength = wwi_func.vdecimal(e.NewValues["BookLength"].ToString()); } else { _t.BookLength = null; }
                if (e.NewValues["BookWidth"] != null) { _t.BookWidth = wwi_func.vdecimal(e.NewValues["BookWidth"].ToString()); } else { _t.BookWidth = null; }
                if (e.NewValues["BookDepth"] != null) { _t.BookDepth = wwi_func.vdecimal(e.NewValues["BookDepth"].ToString()); } else { _t.BookDepth = null; }
                if (e.NewValues["BookWeight"] != null) { _t.BookWeight = wwi_func.vdecimal(e.NewValues["BookWeight"].ToString()); } else { _t.BookWeight = null; }
                //append new record
                _t.Save(); 
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Title # {0} NOT added. Error: {1}", _deliveryid, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            _grd.DataBind(); 
        }
    }
    //end insert
    //delete selected row
    protected void dxgridTitles_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _subdeliveryid = wwi_func.vint(e.Keys["SubDeliveryID"].ToString());

        try
        {
            if (_subdeliveryid > 0)
            {
                //get details
                DeliverySubSubTable.Delete("SubDeliveryID", _subdeliveryid);
                
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Title # {0} NOT deleted. Error: {1}", _subdeliveryid, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_deliveries();
        }
    }

    /// <summary>
    /// called when add titles button is clicked or new delivery is saved
    /// </summary>
    /// <param name="orderno">int</param>
    /// <param name="deliveryid">int</param>
    protected void append_order_titles(int orderno, int deliveryid)
    {
        if (orderno > 0 && deliveryid > 0)
        {
            string _sql = "INSERT INTO DeliverySubSubTable ( Title, TitleID, Copies, DeliveryID ) " +
                          "SELECT ItemTable.Title, ItemTable.TitleID, ItemTable.Copies, @DeliveryID FROM ItemTable " +
                          "WHERE (((ItemTable.OrderNumber)=@OrderNo));";

            string _dbc = System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ToString();
            using (SqlConnection _cnn = new SqlConnection(_dbc))
            {
                _cnn.Open(); 
                using (SqlCommand _cmd = new SqlCommand(_sql, _cnn))
                {
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.AddWithValue("@OrderNo", orderno);
                    _cmd.Parameters.AddWithValue("@DeliveryID", deliveryid);
                    int _result = _cmd.ExecuteNonQuery();
                }
                //end command
                _cnn.Close(); 
            }
            //end connection
        }//end if
    }
    //end append titles
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

    #region deprecated code
    /// <summary>
    /// DEPRECATED bind in code-behind objectdatasources are too flaky
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsOrderDeliveries_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        e.InputParameters["OrderNumber"] = _orderno;
    }
    /// <summary>
    /// deprecated we are using nested grids
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptDeliveriesView_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //address and pallet info
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string _s = "";
            DeliverySubTable _row = (DeliverySubTable)e.Item.DataItem;

            if (_row.DeliveryID > 0)
            {
                _s = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo,PalletDims,MaxPalletWeight,MaxPalletHeight,SpecialDeliveryInstructions", "view_delivery_instruction", "DeliveryID", _row.DeliveryID, "|");
                if (!string.IsNullOrEmpty(_s))
                {
                    string[] _labels = { "DeliveryName", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "DeliveryAddress", "PalletSpec", "PalletWeight", "PalletHeight", "PalletSpec" };
                    string[] _elements = _s.Split("|".ToCharArray());

                    for (int _ix = 0; _ix < _labels.Length; _ix++)
                    {
                        ASPxLabel _lbl = (ASPxLabel)e.Item.FindControl("dxlbl" + _labels[_ix]);
                        if (_lbl != null)
                        {
                            _lbl.Text += _elements[_ix] + Environment.NewLine;
                        }
                    }//end loop
                }//end if
            }

            //current status
            if (_row.CurrentStatusID > 0)
            {
                _s = wwi_func.lookup_value("Field1", "CurrentStatus", "ID", _row.CurrentStatusID);
                ASPxLabel _lbl = (ASPxLabel)e.Item.FindControl("dxlblCurrentStatusID");
                if (_lbl != null) { _lbl.Text = _s; }
            }
        }

    }
    //end deprecated code
    #endregion
   
}

