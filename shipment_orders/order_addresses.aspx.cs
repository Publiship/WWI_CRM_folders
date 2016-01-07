using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxEditors;
using SubSonic;

public partial class order_addresses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                string _mode = get_token("mode");
                //do before seting mode
                bind_commands();
                bind_tabs();
                set_mode(_mode);

                //****
                //replacing objectdatasource bind in code
                //use order id 303635 for testing purposes
                bind_formview(_mode);
                //****

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
    /// <summary>
    /// replacing objectdatasource address data in in OrderTable
    /// </summary>
    protected void bind_formview(string viewmode)
    {
        string[] _key = { "OrderID" };
        string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
        int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));

        try
        {
            OrderTableCollection _o = new OrderTableCollection();
            OrderTable _t = viewmode != "Insert" ? new OrderTable(_orderid) : new OrderTable();
            _o.Add(_t);
            this.fmvAddresses.DataSource = _o;  //303635; //183689; 303635 is the orderid for test record order number 999909  
            this.fmvAddresses.DataKeyNames = _key;
            this.fmvAddresses.DataBind();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }

    }
    /// <summary>
    /// deprecated we are binding in code fed up with flaky objectdatasource
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsOrderAddresses_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        e.InputParameters["OrderNumber"] = _orderno;
    }
    protected void fmvAddresses_DataBound(object sender, EventArgs e)
    {
        OrderTable _row = (OrderTable)this.fmvAddresses.DataItem;
        //summary info
        this.dxlblOrderNo.Text = _row.OrderNumber.ToString();
        //this.dxlblOfficeIndicator.Text = _rd["OfficeIndicator"].ToString();
        //this.dxlblCreated.Text = "Date created: " + Convert.ToDateTime(_rd["DateOrderCreated"].ToString()).ToShortDateString();

        //publiship order
        bool? _checked = _row.PublishipOrder;
        this.dximgJobPubliship.ClientVisible = _checked.Value;
        this.dxlblJobPubliship.ClientVisible = _checked.Value;
        //job closed
        _checked = _row.JobClosed;
        this.dximgJobClosed.ClientVisible = _checked.Value;
        this.dxlblJobClosed.ClientVisible = _checked.Value;
        //hot job
        _checked = _row.HotJob;
        this.dximgJobHot.ClientVisible = _checked.Value;
        this.dxlblJobHot.ClientVisible = _checked.Value;
       
        //view labels
        if (this.fmvAddresses.CurrentMode == FormViewMode.ReadOnly)
        {
          
            sub_decks_view();
        }//end read only form
        else if(this.fmvAddresses.CurrentMode == FormViewMode.Edit)  //edit mode sub decks
        {
            sub_decks_edit();

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboAgentAtDestinationIDEdit");
            if (_cbo != null && _cbo.Value != null) { bind_destination_controller(wwi_func.vint(_cbo.SelectedItem.GetValue("CountryID").ToString())); }
  
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
        this.dxmnuCommand.DataSource = _xml;
        this.dxmnuCommand.DataBind();

    }

    protected void dxmnuCommand_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
       //do not set navigateurl as it prevents itemclick event from firing
        e.Item.NavigateUrl = "";
       //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
       // {
       //     string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
       //     string _id = get_token("pno");
       //     if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
       // }
    }
    #endregion

    #region sub decks
    protected void sub_decks_view()
    {
        string[] _fields = { "ConsigneeID", "NotifyPartyID", "ClearingAgentID", "OnCarriageID", "AgentAtDestinationID", "DestinationPortControllerID" };
        string _test = "";
        //step through field names
        //get hidden field value from hf<fieldname>
        //set name text on dxlbl<fieldname>Name label
        //set address text on dxlbl<fieldname>View label
        for (int _ix = 0; _ix < _fields.Length; _ix++)
        {
            HiddenField _hf = (HiddenField)this.fmvAddresses.FindControl("hf" + _fields[_ix]);
            if (_hf != null)
            {
                int _id = wwi_func.vint(_hf.Value.ToString());

                if (_fields[_ix] != "DestinationPortControllerID")
                {
                    _test = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _id);
                }
                else
                {
                    _test = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _id);
                }

                if (_test != "")
                {
                    string[] _lines = _test.Split(Environment.NewLine.ToCharArray()); //split returned string
                    ASPxLabel _lbl = (ASPxLabel)this.fmvAddresses.FindControl("dxlbl" + _fields[_ix] + "Name");
                    if (_lbl != null) { _lbl.Text = _lines[0].ToString().Trim(); }
                    //remove companyname from string and populate address label (doesn't apply to DestinationPortControllerID
                    _lbl = (ASPxLabel)this.fmvAddresses.FindControl("dxlbl" + _fields[_ix] + "View");
                    if (_lbl != null) { _lbl.Text = _test.Replace(_lines[0], "").Trim(); }

                }//end test
            }//end if
        }//end loop
    }
    protected void sub_decks_edit()
    {
        string[] _fields = { "ConsigneeID", "NotifyPartyID", "ClearingAgentID", "OnCarriageID", "AgentAtDestinationID", "DestinationPortControllerID" };
        string _s = "";
        //step through field names
        //get combobox value from dxcbo<fieldname>Edit
        //set address text on dxlbl<fieldname>Edit label
        for (int _ix = 0; _ix < _fields.Length; _ix++)
        {
            _s = "";
            ASPxComboBox _cbCompany = (ASPxComboBox)this.fmvAddresses.FindControl("dxcbo" + _fields[_ix] + "Edit");
            if (_cbCompany != null && _cbCompany.SelectedItem != null && _cbCompany.Value != null)
            {
                if (_cbCompany.SelectedItem.GetValue("Address1") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address1").ToString(); } //(string)_cbCompany.SelectedItem.Value.ToString();
                _s += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("Address2") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address2").ToString(); }
                _s += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("Address3") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address3").ToString(); }
                _s += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("CountryName") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("CountryName").ToString(); }
                _s += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("TelNo") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("TelNo").ToString(); }
                _s += Environment.NewLine;

            }
            ASPxLabel _lblCompany = (ASPxLabel)this.fmvAddresses.FindControl("dxlbl" + _fields[_ix] + "Edit");
            if (_lblCompany != null) { _lblCompany.Text = _s; }

            
        }//end loop
    }
    #endregion

    #region dll binding
    
    protected void dxcboDestinationPortControllerIDEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //rebind destination controller
        bind_destination_controller(wwi_func.vint(e.Parameter.ToString()));
    }

    protected void bind_destination_controller(int countryId)
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboDestinationPortControllerIDEdit"); 
        if (_combo != null)
        {

            string[] _cols = { "EmployeesTable.EmployeeID, EmployeesTable.Name", "EmployeesTable.DepartmentID", "OfficeTable.OfficeID"};
            string[] _order = { "Name" };
            //don't need nameandaddressbook as get countryid from officetable
            //SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
            //    InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn).
            //    InnerJoin(DAL.Logistics.NameAndAddressBook.CountryIDColumn, DAL.Logistics.OfficeTable.CountryIDColumn);
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
                InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn);

            if (countryId > 0)
            {
                 _qry.Where("CountryID").IsEqualTo(countryId); 
            }

            _qry.And(DAL.Logistics.EmployeesTable.LiveColumn).IsEqualTo(1).OrderAsc(_order);
            //string _q = _qry.ToString(); //fo testing
            //DataTable _dt = _qry.ExecuteDataSet().Tables[0];  //for testing
            
            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
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
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer", "CountryID" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("{0}%", e.Filter.ToString()));

       
        switch(_combo.ID){
            case "dxcboClearingAgentIDEdit":
                {
                    int[] _vals = { 3, 6 };
                    _query.And("TypeID").In(_vals);  
                    break;
                }
            case "dxcboOnCarriageIDEdit":
                {
                    _query.And("TypeID").IsEqualTo(3);
                    break;
                }
            case "dxcboAgentAtDestinationIDEdit":
                {
                    _query.And("TypeID").IsEqualTo(3);
                    break;
                }
            default:
                {
                    break;
                }
        }

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "CompanyName";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer", "CountryID" };

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

    /// <summary>
    /// destinatiobn controller
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboDestControl_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;
        ASPxComboBox _destinationagent = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboAgentAtDestinationIDEdit"); 
        if (_combo != null)
        {

            string[] _cols = { "EmployeesTable.EmployeeID, EmployeesTable.Name", "EmployeesTable.DepartmentID", "OfficeTable.OfficeID", " NameAndAddressBook.CompanyID" };
            string[] _order = { "Name" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
                InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn).
                InnerJoin(DAL.Logistics.NameAndAddressBook.CountryIDColumn, DAL.Logistics.OfficeTable.CountryIDColumn);

            if (_destinationagent != null && _destinationagent.SelectedItem != null && _destinationagent.Value != null)
            {
                int _filter = wwi_func.vint(_destinationagent.SelectedItem.GetValue("CountryID").ToString());
                if (_filter > 0) { _qry.Where("CountryID").IsEqualTo(_filter); }
            }

            _qry.And(DAL.Logistics.EmployeesTable.LiveColumn).IsEqualTo(true).OrderAsc(_order);

            DataTable _dt = _qry.ExecuteDataSet().Tables[0]; 
            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }
    protected void dxcboDestControl_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;
        if (_combo != null)
        {

            string[] _cols = { "EmployeesTable.EmployeeID, EmployeesTable.Name", "EmployeesTable.DepartmentID", "OfficeTable.OfficeID", " NameAndAddressBook.CompanyID" };
            string[] _order = { "Name" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
                InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn).
                InnerJoin(DAL.Logistics.NameAndAddressBook.CountryIDColumn, DAL.Logistics.OfficeTable.CountryIDColumn);

           
            _qry.And(DAL.Logistics.EmployeesTable.LiveColumn).IsEqualTo(true).OrderAsc(_order);
            DataTable _dt = _qry.ExecuteDataSet().Tables[0]; 
            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }
    #endregion

    #region crud events
    protected void update_shipment()
    {
        //in code or might as well use formview seeing as we need it anyway
    }
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
                        bind_formview("Insert"); 
                        break;
                    }
                case "cmdEdit":
                    {
                        set_mode("Edit");
                        //for the mode to change call databind
                        bind_formview("Edit"); 
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                        //this.fmvAddresses.DeleteItem(); 
                        break;
                    }
                case "cmdUpdate":
                    {
                        update_addresses();
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        bind_formview("ReadOnly"); 
                        //this.fmvAddresses.UpdateItem(false);
                        break;
                    }
                case "cmdSave":
                    {
                       insert_addresses();
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        bind_formview("ReadOnly"); 
                        //no insert option needed for charges
                        //this.fmvAddresses.InsertItem(false);
                        break;
                    }
                case "cmdCancel":
                    {
                        set_mode("ReadOnly");
                        //for the mode to change call databind
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

    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvAddresses.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvAddresses.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvAddresses.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to readonly
                {
                    this.fmvAddresses.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }
    /// <summary>
    /// fires when menu item clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvAddresses_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvAddresses.ChangeMode(e.NewMode);
    }
    //end mode changing

    #region crud events
    protected void update_addresses()
    {
        try
        {
            //use order id 303635 for testing purposes
            int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));

            if (_orderid > 0)
            {
                OrderTable _t = new OrderTable(_orderid);

                //dlls
                int? _intnull = null;
                ASPxComboBox _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboConsigneeIDEdit");
                if (_cb != null) { _t.ConsigneeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboClearingAgentIDEdit");
                if (_cb != null) { _t.ClearingAgentID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboAgentAtDestinationIDEdit");
                if (_cb != null) { _t.AgentAtDestinationID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboNotifyPartyIDEdit");
                if (_cb != null) { _t.NotifyPartyID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboOnCarriageIDEdit");
                if (_cb != null) { _t.OnCarriageID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboDestinationPortControllerIDEdit");
                if (_cb != null) { _t.DestinationPortControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

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
    }
    //end update
    /// <summary>
    /// we don't really need an insert sub as address details can't be entered until main order details have been saved
    /// </summary>
    protected void insert_addresses()
    {
        try
        {
            //use order id 303635 for testing purposes
            int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));

            if (_orderid > 0)
            {
                OrderTable _t = new OrderTable(_orderid);

                //dlls
                int? _intnull = null;
                ASPxComboBox _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboConsigneeIDEdit");
                if (_cb != null) { _t.ConsigneeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboClearingAgentIDEdit");
                if (_cb != null) { _t.ClearingAgentID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboAgentAtDestinationIDEdit");
                if (_cb != null) { _t.AgentAtDestinationID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboNotifyPartyIDEdit");
                if (_cb != null) { _t.NotifyPartyID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboOnCarriageIDEdit");
                if (_cb != null) { _t.OnCarriageID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvAddresses.FindControl("dxcboDestinationPortControllerIDEdit");
                if (_cb != null) { _t.DestinationPortControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                //save record
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
    }
    //end insert
    #endregion

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

