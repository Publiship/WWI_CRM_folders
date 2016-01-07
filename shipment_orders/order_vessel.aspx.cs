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

public partial class order_vessel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            string _mode = get_token("mode"); 
            if (!Page.IsPostBack)
            {
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
    /// replacing objectdatasource
    /// vessel and shipmennt data is in the OrderTable
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
            this.fmvShipment.DataSource = _o;  //303635; //183689; 303635 is the orderid for test record order number 999909  
            this.fmvShipment.DataKeyNames = _key;
            this.fmvShipment.DataBind();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno, _ex);
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end bind formview

    //DEPRECATED biding in code-behind objectdatasource too flaky
    protected void odsOrderShipment_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        e.InputParameters["OrderNumber"] = _orderno;
    }
    //end selecting

    protected void fmvShipment_DataBound(object sender, EventArgs e)
    {
        try
        {
            OrderTable _row = (OrderTable)this.fmvShipment.DataItem;
            //publiship order
            this.dximgJobPubliship.ClientVisible = _row != null ? _row.PublishipOrder : false;
            this.dxlblJobPubliship.ClientVisible = _row != null ? _row.PublishipOrder : false;
            //job closed
            this.dximgJobClosed.ClientVisible = _row != null ? _row.JobClosed : false;
            this.dxlblJobClosed.ClientVisible = _row != null ? _row.JobClosed : false;
            //hot job
            this.dximgJobHot.ClientVisible = _row != null ? _row.HotJob : false;
            this.dxlblJobHot.ClientVisible = _row != null ? _row.HotJob : false;
            //order number and office in header
            this.dxlblOrderNo.Text = _row.OrderNumber.ToString();

            string[] _cols = { "OrderNumber", "PublishipOrder", "OfficeIndicator", "DateOrderCreated", "JobClosed", "HotJob", "PortID", "DestinationPortID", "VesselID", "PackageTypeID", "FCLLCL" };
            SubSonic.SqlQuery _q = new SubSonic.SqlQuery();
            _q = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.OrderTable).WhereExpression("OrderNumber").IsEqualTo(_row.OrderNumber);

            DataTable _dt = _q.ExecuteDataSet().Tables[0];
            if (_dt.Rows.Count > 0)
            {
                string _vessel = wwi_func.lookup_value("Joined", "VoyageTable", "VoyageID", wwi_func.vint(_dt.Rows[0]["VesselID"].ToString()));
                string _package = wwi_func.lookup_value("PackageType", "PackageTypeTable", "PackageTypeID", wwi_func.vint(_dt.Rows[0]["PackageTypeID"].ToString()));
                string _fcllcl = wwi_func.lookup_xml_string("//xml//ddl_items.xml", "ddls", "FCLLCL", "value", _dt.Rows[0]["FCLLCL"].ToString(), "name");

                this.dxhfOrder.Remove("ptstart"); //origin port
                this.dxhfOrder.Remove("ptend");  //destination port
                this.dxhfOrder.Remove("vssl");  //vessel name

                this.dxhfOrder.Add("ptstart", _dt.Rows[0]["PortID"].ToString());
                this.dxhfOrder.Add("ptend", _dt.Rows[0]["DestinationPortID"].ToString());
                this.dxhfOrder.Add("vssl", _vessel);

                //containers list in all views
                bind_containers();

                if (this.fmvShipment.CurrentMode != FormViewMode.ReadOnly)
                {
                    //bind dlls here or won't populate on new record
                    bind_fcl_lcl();
                    bind_package_type();
                }
                else
                {
                    //set values for dlls in ReadOnly mode
                    ASPxLabel _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldVessel");
                    if (_lbl != null) { _lbl.Text = _vessel; }

                    _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldPackageTypeID");
                    if (_lbl != null) { _lbl.Text = _package; }
                    
                    _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldFCLLCL");
                    if (_lbl != null) { _lbl.Text = _fcllcl; }

                    //date formatting
                    _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldETS");
                    if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                    _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldETA");
                    if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                    _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblFieldJobClosureDate");
                    if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }
                }//endif
            }//endif
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end databound

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
        //do NOT set the NavigateUrl property as it prevents itemclick event fom being fired! You can only have one or the other
        e.Item.NavigateUrl = "";
       
        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        //{
        //   string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pno");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        //}
    }
    #endregion

    #region dll binding
    /// <summary>
    /// make sure VALUETYPE is defined for all dll's or they will display value rather than text
    /// </summary>
    protected void bind_fcl_lcl()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\ddl_items.xml";

        // pass _qryFilter to have keyword-filter RSS Feed
        // i.e. _qryFilter = XML -> entries with XML will be returned
        DataSet _ds = new DataSet();
        _ds.ReadXml(_path);
        DataView _dv = _ds.Tables[0].DefaultView;
        _dv.RowFilter = "ddls ='FCLLCL'";

        //Run time population of GridViewDataComboBoxColumn column with data
        ASPxComboBox _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboFCLLCL");
        if (_cb != null)
        {
            _cb.DataSource = _dv;
            _cb.ValueType = typeof(int);
            _cb.TextField = "name";
            _cb.ValueField = "value";
            _cb.DataBindItems();
        }
    }
    //end bind fcl lcl dll
    protected void bind_package_type()
    {
        try
        {
            string[] _cols = { "PackageTypeID, PackageType" };
            string[] _order = { "PackageType" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PackageTypeTable).OrderAsc(_order);

            //order controller
            ASPxComboBox _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboPackageTypeID");
            if (_cb != null)
            {
                IDataReader _rd1 = _qry.ExecuteReader();
                _cb.DataSource = _rd1;
                _cb.ValueType = typeof(int);
                _cb.ValueField = "PackageTypeID";
                _cb.TextField = "PackageType";
                _cb.DataBindItems();
            }

        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.dxlblErr.Text = _err;
            this.dxpnlErr.ClientVisible = true;
        }

    }
    //end bind vessel
    /// <summary>
    /// container list for this order 
    /// </summary>
    protected void bind_containers()
    {
        string[] _cols = { "ContainerSubID", "ContainerNumber" };
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_order_container").Where("OrderNumber").IsEqualTo(_orderno);

        ASPxLabel _lbl;
        if (this.fmvShipment.CurrentMode == FormViewMode.ReadOnly)
        {
            _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblContainerListView");
        }
        else
        {
            _lbl = (ASPxLabel)this.fmvShipment.FindControl("dxlblContainerListEdit");
        }

        if (_lbl != null)
        {
            _lbl.Text = "";
            //enumerate to label
            IDataReader _rd = _query.ExecuteReader();
            while (_rd.Read())
            {
                _lbl.Text += _rd["ContainerNumber"].ToString() + Environment.NewLine;  
            }
        }
    }
    /// <summary>
    /// container list for this order deprecated just drop resutls to a label not editable on this form
    /// </summary>
    protected void bind_containers_deprecated()
    {
        string[] _cols = { "ContainerSubID", "ContainerNumber" };
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_order_container").Where("OrderNumber").IsEqualTo(_orderno);   
     
        ASPxListBox _lst;
        if(this.fmvShipment.CurrentMode == FormViewMode.ReadOnly){
            _lst = (ASPxListBox)this.fmvShipment.FindControl("dxlstContainerView");
        }
        else
        {
            _lst = (ASPxListBox)this.fmvShipment.FindControl("dxlstContainerEdit");
        }

        if (_lst != null)
         {
            IDataReader _rd = _query.ExecuteReader();
            _lst.TextField = "ContainerNumber";
            _lst.ValueField = "ContainerSubID";
            _lst.ValueType = typeof(int); 
            _lst.DataSource = _rd;
            _lst.DataBindItems();
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
    protected void dxcboVesselID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined", "ETS", "ETA", "DestinationPortID", "OriginPortID" };
        string[] _sort = { "Joined" };
        //additional filters on this dll
        string _originportid = this.dxhfOrder.Contains("ptstart") ? this.dxhfOrder.Get("ptstart").ToString() : "";
        string _destportid = this.dxhfOrder.Contains("ptend") ? this.dxhfOrder.Get("ptend").ToString() : "";

        if (_originportid != "" && _destportid != "")
        {
            //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("Page2VesselView").Paged(e.BeginIndex + 1, e.EndIndex + 1, "VoyageID").Where("Joined").Like(string.Format("{0}%", e.Filter.ToString())).And("DestinationPortID").IsEqualTo(_destportid).And("OriginPortID").IsEqualTo(_originportid).OrderAsc(_sort);

            string test = _query.ToString();

            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "VoyageID";
            _combo.TextField = "Joined";
            _combo.DataBindItems();
        }
    }
    protected void dxcboVesselID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined", "ETS", "ETA", "DestinationPortID", "OriginPortID" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("Page2VesselView").WhereExpression("VoyageID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VoyageID";
        _combo.TextField = "Joined";
        _combo.DataBindItems();
    }
    #endregion

    #region crud events
    protected void update_shipment()
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
                ASPxComboBox _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboVesselID");
                if (_cb != null) { _t.VesselID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboPackageTypeID");
                if (_cb != null) { _t.PackageTypeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                //why are the LCL/FCL values set to lcl=1, fcl=2 when the db data type is a bit? this doesn't make any sense
                //disbable code until confirmation from dave
                //_cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboFCLLCL");
                //if (_cb != null && _cb.Value != null) {
                //    string _lclfcl = _cb.Value.ToString();
                //    _t.Fcllcl = _lclfcl == "-1" ? true : false : false; //0 or -1
                //} 
            
                //textboxes
                //estimated 
                ASPxTextBox _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstPallets");
                if (_tx != null && _tx.Text != "") { _t.EstPallets = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstCartons");
                if (_tx != null && _tx.Text != "") { _t.EstCartons = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstWeight");
                if (_tx != null && _tx.Text != "") { _t.EstWeight = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstVolume");
                if (_tx != null && _tx.Text != "") { _t.EstVolume = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstLCLWt");
                if (_tx != null && _tx.Text != "") { _t.EstLCLWt = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstLCLVol");
                if (_tx != null && _tx.Text != "") { _t.EstLCLVol = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEst20");
                if (_tx != null && _tx.Text != "") { _t.Est20 = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEst40");
                if (_tx != null && _tx.Text != "") { _t.Est40 = wwi_func.vint(_tx.Text.ToString()); }

                //actuals
                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActualWeight");
                if (_tx != null && _tx.Text != "") { _t.ActualWeight = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActualVolume");
                if (_tx != null && _tx.Text != "") { _t.ActualVolume = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtLCLWt");
                if (_tx != null && _tx.Text != "") { _t.LCLWt = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtLCLvol");
                if (_tx != null && _tx.Text != "") { _t.LCLVol = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActual20");
                if (_tx != null && _tx.Text != "") { _t.No20 = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActual40");
                if (_tx != null && _tx.Text != "") { _t.No40 = wwi_func.vint(_tx.Text.ToString()); }

                //other
                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtNumberOfPackages");
                if (_tx != null && _tx.Text != "") { _t.NumberOfPackages = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHouseBLNUmber");
                if (_tx != null && _tx.Text != "") { _t.HouseBLNUmber = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtJackets");
                if (_tx != null && _tx.Text != "") { _t.Jackets = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtConsolNumber");
                if (_tx != null && _tx.Text != "") { _t.ConsolNumber = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtConsolNumber");
                if (_tx != null && _tx.Text != "") { _t.HodderPricePerCopy = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHCCompositeRef");
                if (_tx != null && _tx.Text != "") { _t.HCCompositeRef = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHCInvoiceAmount2");
                if (_tx != null && _tx.Text != "") { _t.HCInvoiceAmount = wwi_func.vdecimal(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtInsuranceValue");
                if (_tx != null && _tx.Text != "") { _t.InsuranceValue = wwi_func.vdecimal(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtImpression");
                if (_tx != null && _tx.Text != "") { _t.Impression = _tx.Text.ToString(); }

                //datetimes
                DateTime? _dtnull = null; //default for nullable datetimes
                ASPxDateEdit _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtETS");
                if (_dt != null) { _t.Ets = _dt.Value != null? wwi_func.vdatetime(_dt.Date.ToString()): _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtETA");
                if (_dt != null) {  _t.Eta = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()): _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtJobClosureDate");
                if (_dt != null) {  _t.JobClosureDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()): _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtWarehouseDate");
                if (_dt != null) { _t.WarehouseDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()): _dtnull; }
                                
                //checkboxes
                ASPxCheckBox _ck = (ASPxCheckBox)this.fmvShipment.FindControl("dxckJobClosed");
                if (_ck != null) { _t.JobClosed = _ck.Checked; }

                _ck = (ASPxCheckBox)this.fmvShipment.FindControl("dxckOnBoard");
                if (_ck != null) { _t.ShippedOnBoard = _ck.Checked; }

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

    }//end update

    /// <summary>
    /// we don't actually need an insert sub as shipment/vessel data is part of the ordertable which must have
    /// been saved before we get to shipment/vessel
    /// </summary>
    protected void insert_shipment()
    {
        try
        {
            //use order id 303635 for testing purposes
            int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));

            if (_orderid > 0)
            {
                OrderTable _t = new OrderTable(_orderid);

                //dlls
                //dlls
                int? _intnull = null;
                ASPxComboBox _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboVesselID");
                if (_cb != null) { _t.VesselID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboPackageTypeID");
                if (_cb != null) { _t.PackageTypeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }


                //why are the LCL/FCL values set to lcl=1, fcl=2 when the db data type is a bit? this doesn't make any sense
                //disbable code until confirmation from dave
                //_cb = (ASPxComboBox)this.fmvShipment.FindControl("dxcboFCLLCL");
                //if (_cb != null && _cb.Value != null) {
                //    string _lclfcl = _cb.Value.ToString();
                //    _t.Fcllcl = _lclfcl == "-1" ? true : false : false; //0 or -1
                //} 

                ASPxTextBox _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstPallets");
                if (_tx != null && _tx.Text != "") { _t.EstPallets = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstCartons");
                if (_tx != null && _tx.Text != "") { _t.EstCartons = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstWeight");
                if (_tx != null && _tx.Text != "") { _t.EstWeight = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstVolume");
                if (_tx != null && _tx.Text != "") { _t.EstVolume = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstLCLWt");
                if (_tx != null && _tx.Text != "") { _t.EstLCLWt = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEstLCLVol");
                if (_tx != null && _tx.Text != "") { _t.EstLCLVol = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEst20");
                if (_tx != null && _tx.Text != "") { _t.Est20 = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtEst40");
                if (_tx != null && _tx.Text != "") { _t.Est40 = wwi_func.vint(_tx.Text.ToString()); }

                //actuals
                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActualWeight");
                if (_tx != null && _tx.Text != "") { _t.ActualWeight = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActualVolume");
                if (_tx != null && _tx.Text != "") { _t.ActualVolume = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtLCLWt");
                if (_tx != null && _tx.Text != "") { _t.LCLWt = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtLCLvol");
                if (_tx != null && _tx.Text != "") { _t.LCLVol = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActual20");
                if (_tx != null && _tx.Text != "") { _t.No20 = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtActual40");
                if (_tx != null && _tx.Text != "") { _t.No40 = wwi_func.vint(_tx.Text.ToString()); }

                //other
                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtNumberOfPackages");
                if (_tx != null && _tx.Text != "") { _t.NumberOfPackages = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHouseBLNUmber");
                if (_tx != null && _tx.Text != "") { _t.HouseBLNUmber = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtJackets");
                if (_tx != null && _tx.Text != "") { _t.Jackets = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtConsolNumber");
                if (_tx != null && _tx.Text != "") { _t.ConsolNumber = wwi_func.vint(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtConsolNumber");
                if (_tx != null && _tx.Text != "") { _t.HodderPricePerCopy = wwi_func.vfloat(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHCCompositeRef");
                if (_tx != null && _tx.Text != "") { _t.HCCompositeRef = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtHCInvoiceAmount2");
                if (_tx != null && _tx.Text != "") { _t.HCInvoiceAmount = wwi_func.vdecimal(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtInsuranceValue");
                if (_tx != null && _tx.Text != "") { _t.InsuranceValue = wwi_func.vdecimal(_tx.Text.ToString()); }

                _tx = (ASPxTextBox)this.fmvShipment.FindControl("dxtxtImpression");
                if (_tx != null && _tx.Text != "") { _t.Impression = _tx.Text.ToString(); }

                //datetimes
                DateTime? _dtnull = null; //default for nullable datetimes
                ASPxDateEdit _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtETS");
                if (_dt != null) { _t.Ets = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtETA");
                if (_dt != null) { _t.Eta = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtJobClosureDate");
                if (_dt != null) { _t.JobClosureDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.fmvShipment.FindControl("dxdtWarehouseDate");
                if (_dt != null) { _t.WarehouseDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Date.ToString()) : _dtnull; }

                //checkboxes
                ASPxCheckBox _ck = (ASPxCheckBox)this.fmvShipment.FindControl("dxckJobClosed");
                if (_ck != null) { _t.JobClosed = _ck.Checked; }

                _ck = (ASPxCheckBox)this.fmvShipment.FindControl("dxckOnBoard");
                if (_ck != null) { _t.ShippedOnBoard = _ck.Checked; }

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

    }//end insert
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
                        //no insert option needed for charges
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
                        //this.formOrder.DeleteItem(); 
                        break;
                    }
                case "cmdUpdate":
                    {
                        update_shipment();
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        bind_formview("ReadOnly"); 
                        //this.fmvShipment.UpdateItem(false); 
                        //set_mode("view"); will return to view automatically after updating
                        break;
                    }
                case "cmdSave":
                    {
                        //no insert option needed for charges
                        //this.fmvShipment.InsertItem(false);
                        //set_mode("view"); will return to view automatically after inserting
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
                    this.fmvShipment.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvShipment.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvShipment.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to readonly
                {
                    this.fmvShipment.ChangeMode(FormViewMode.ReadOnly);
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
    protected void formOrder_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvShipment.ChangeMode(e.NewMode);
    }
    //end mode changing

    

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

