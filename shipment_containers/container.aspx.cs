using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxPanel;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using SubSonic;


public partial class container : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //for testing
        //validate_allocations("1040413");
        //get_order_qty_allocated(321740); //order number 1040413

        if (isLoggedIn())
        {
            string _mode = get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
            

            if (!Page.IsPostBack)
            {
                bind_container_commands(); //crud commands for container
                bind_order_commands(); //crud commands for order allocations
                set_mode(_mode);
                bind_container(_mode); 
            }

            //attached orders grid binding 
            int _id = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
            bind_orders_to_container(_id);

            //if insert mode disable order allocation until new container is saved
            bool _enabled = _mode == "Insert" ? false : true;
            this.dxgrdContainerOrders.Enabled = _enabled;
            this.dxmnuOrder.Enabled = _enabled;

            if (_enabled)
            {
                //dll binding for allocation panel
                bind_package_type();
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_containers/container", "publiship")); 
        }
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    #region page binding
  
    protected void bind_container(string mode)
    {
        //have to use a collection as formview requires i IListSource, IEnumerable, or IDataSource.
        ContainerTableCollection _tbc = new ContainerTableCollection();

        if (mode != "Insert")
        {
            //get key id
            string _pid = get_token("pid");
            if(_pid != null)
            {
                int _containerid = wwi_func.vint(wwi_security.DecryptString(_pid, "publiship"));
                ContainerTable _tbl = new ContainerTable(_containerid);
                _tbc.Add(_tbl);
            }
            else
            {
                string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "container_search",};
                string _url = string.Format("{0}\\{1}.aspx?", _args);
                Response.Redirect(_url);
            }
        }
        else
        {
            ContainerTable _tbl = new ContainerTable();
            _tbc.Add(_tbl);
        }
        
        //bind formview to collection
        this.fmvContainer.DataSource = _tbc;
        this.fmvContainer.DataBind(); 
    }

    //deprecated we are binding in code behind
    //for edit mode get container details
    protected void odsContainer_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //use order id 303635 for testing purposes
        int _containerid = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        e.InputParameters["ContainerID"] = _containerid;
    }

    /// <summary>
    /// datebinding for orders grid
    /// </summary>
    /// <param name="containerid">int id of current container from formview</param>
    protected void bind_orders_to_container(int containerid)
    {
        string[] _cols = { "ContainerSubTable.ContainerSubID", "ContainerSubTable.ContainerID", "ContainerSubTable.OrderNumber", "ContainerSubTable.Packages",
                             "ContainerSubTable.PackageTypeID", "PackageTypeTable.PackageType", "ContainerSubTable.Weight", "ContainerSubTable.Cbm", "ContainerSubTable.OrderID" };

        SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.ContainerSubTable).
                LeftOuterJoin(DAL.Logistics.PackageTypeTable.PackageTypeIDColumn, DAL.Logistics.ContainerSubTable.PackageTypeIDColumn).
                Where(DAL.Logistics.ContainerSubTable.ContainerIDColumn).IsEqualTo(containerid);    

        DataTable _tbl = _qry.ExecuteDataSet().Tables[0];
        this.dxgrdContainerOrders.DataSource = _tbl;
        this.dxgrdContainerOrders.KeyFieldName = "ContainerSubID";
        this.dxgrdContainerOrders.DataBind(); 
    }
    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_container_commands()
    {
        //using generic FormView commands 
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='GenericFormView']"; //you need this or tab will not databind! "//item[@Filter='GenericFormView'] | //item[@Filter='Container']";
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuContainer.DataSource = _xml;
        this.dxmnuContainer.DataBind();
        //int _test = this.dxmnuContainer.Items.Count;
    }
    protected void dxmnuContainer_DataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        //do not set navigate url it prevents itemclick event from firing
        e.Item.NavigateUrl = "";
        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        // {
        //    string _folder = System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath); 
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pno");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _folder + "\\" + _page, _id); }
        //}
    }

    /// <summary>
    /// allocate order to container menu 
    /// </summary>
    protected void bind_order_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        //these commands moved to bind_container_commands so this menu is not needed
        _xml.XPath = "//item[@Filter='Container']"; //you need this or tab will not databind! "//item[@Filter='NoCommands']";
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuOrder.DataSource = _xml;
        this.dxmnuOrder.DataBind();

    }
    
    //mode changing
    protected void fmvContainer_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvContainer.ChangeMode(e.NewMode);
    }
    #endregion

    #region command and tab events
    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvContainer.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvContainer.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvContainer.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvContainer.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }

    protected void dxmnuContainer_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();
        List<string> _menuitems = new List<string>();

        switch (_item)
        {
            case "cmdNew":
                {
                    set_mode("Insert");
                    //rebind formview to force mode change
                    bind_container("Insert");
                    break;
                }
            case "cmdEdit":
                {
                    set_mode("Edit");
                    //rebind formview to force mode change
                    bind_container("Edit");
                    break;
                }
            case "cmdDelete": //not enabled - do we want users to delete records?
                {
                    //this.formOrder.DeleteItem(); 
                    break;
                }
            case "cmdUpdate":
                {
                    //this.fmvContainer.UpdateItem(false);
                    int _id = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
                    if (_id > 0)
                    {
                        update_container(_id);
                        set_mode("ReadOnly");
                        //rebind formview to force mode change
                        bind_container("ReadOnly");
                        //set_mode("view"); not necesary form will revert to read only after save
                    }
                    break;
                }
            case "cmdSave":
                {

                    //this.fmvContainer.InsertItem(false);
                    int _newid = insert_container();
                    if (_newid > 0)
                    {
                        string _secure = wwi_security.EncryptString(_newid.ToString(), "publiship");
                        string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath),
                                                _secure, 
                                                "ReadOnly" };
                        string _url = string.Format("{0}\\{1}.aspx?pid={2}&mode={3}", _args);
                        Response.Redirect(_url);

                        //Response.Redirect(String.Format("../shipment_containers/container.aspx?pno={0}&mode=ReadOnly", _secure));
                        //set_mode("ReasdOnly"); 
                    }
                    else
                    {
                        if (this.dxlblErr.Text == "") { this.dxlblErr.Text = "Error new record NOT saved"; }
                        this.dxpnlErr.ClientVisible = true;
                    }
                    break;
                }
            case "cmdCancel":
                {
                    //if (this.fmvContainer.CurrentMode != FormViewMode.Insert)
                    //{
                        set_mode("ReadOnly");
                        bind_container("ReadOnly");
                    //}
                    //else
                    //{
                    //    string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                    //                            "container_search",};
                    //    string _url = string.Format("{0}\\{1}.aspx?", _args);
                    //    Response.Redirect(_url);
                    //}
                    break;
                }
            case "cmdClose":
                {
                    string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "container_search",};
                    string _url = string.Format("{0}\\{1}.aspx?", _args);
                    Response.Redirect(_url);
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    //end container menu commands

   
    /// <summary>
    /// enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_menu_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmnuContainer.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuContainer.Items[_ix].Name == "cmd" + active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmnuContainer.Items[_ix].ClientVisible = _isactive;
        }
    }//end menu names loop


    #endregion

    #region form call backs
    /// <summary>
    /// origin port combo filtered by VoyageID called client-side when voyage is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOriginPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString());
        bind_origin_port(_filter);

    }

    /// <summary>
    /// destination port combo filterd by VoyageID called client-side when voyage is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbDestPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString());
        bind_destination_port(_filter);

    }
    #endregion

    #region grid call backs and events

    /// <summary>
    /// rebind orders grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdContainerOrders_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //attached orders grid binding 
        int _id = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
        bind_orders_to_container(_id);
    }

    /// <summary>
    /// fires after user confirms delete
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdContainerOrders_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        //need to get containersubID from edit containersubtable it won't be found in NewValues

        int _subid = wwi_func.vint(e.Keys["ContainerSubID"].ToString());

        if (_subid > 0)
        {
            ContainerSubTable.Delete("ContainerSubID", _subid);
        }

        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //no need to rebind it will happen after callback anyway
        //bind_order_titles(); 
    }
    /// <summary>
    /// DEPRECATED we only need the delete button so call dxgrdContainerOrders_RowDeleting event instead of a custom callback 
    /// custom butons on grid for insert/remove order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdContainerOrders_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        switch (e.ButtonID)
        {
            case "btnAdd": //intialise a new row DEPRECATED we need to use a custom form 
                {
                    //ASPxGridView _grid = (ASPxGridView)sender;
                    //_grid.AddNewRow(); 
                    break;
                }
            case "btnRemove": //delete row
                {
                    ASPxGridView _grid = (ASPxGridView)sender;
                    string _subid = _grid.GetRowValues(e.VisibleIndex, _grid.KeyFieldName).ToString(); //get containersubid  
                    //delete_order_from_container(_subid);
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    //end custom buton callback
    #endregion

    #region form crud events
    protected void fmvContainer_DataBound(object sender, EventArgs e)
    {
        string _test = "";
        ASPxLabel _lbl = null;
        ContainerTable _c = (ContainerTable)this.fmvContainer.DataItem;

        this.dxlblStatus.Text = this.fmvContainer.CurrentMode == FormViewMode.Insert ? "New container" : _c.LoadedOnBoard == true ? "Container confirmed 'LOADED ON BOARD'" : "Container not yet confirmed on board";

        
        if (_c != null)
        {
            //add ports to hidden fields as well, they are used to determine if an order can be allocated to this container
            //origin port
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _c.OriginPortID);
            if (this.dxhfContainer.Contains("ctnrorigin")) { this.dxhfContainer.Remove("ctnrorigin"); }
            this.dxhfContainer.Add("ctnrorigin", _test);
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewOriginPort");
            if (_lbl != null)
            { _lbl.Text = _test; }


            //destination port
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _c.DestinationPortID);
            if (this.dxhfContainer.Contains("ctnrdestination")) { this.dxhfContainer.Remove("ctnrdestination"); }
            this.dxhfContainer.Add("ctnrdestination", _test);
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewDestPort");
            if (_lbl != null)
            {
                _lbl.Text = _test;
            }

            //if terms = CY (1) only show delivery date/checkbox else if CFS (2) show devanned date/checkbox
            if (_c.Cycfs != null && _c.Cycfs == 1)
            {

            }
            else if (_c.Cycfs != null && _c.Cycfs == 2)
            { 
            
            }
        }
        
        //look up values these for edit/insert modes
        if (this.fmvContainer.CurrentMode == FormViewMode.Edit)
        {
            //get voyageid but no need to bind voyage as it is bound using incremental filtering as a large dataset
            int _id = _c.VoyageID != null? (int)_c.VoyageID: 0;
            //dll binding
            bind_container_status(); 
            //container type
            bind_container_type();
            //terms
            bind_terms();

            //devan warehouse
            _test = "";
            ASPxComboBox _cbCompany = (ASPxComboBox)this.fmvContainer.FindControl("dxcboWarehouse");
            if (_cbCompany != null && _cbCompany.SelectedItem != null && _cbCompany.Value != null)
            {
                if (_cbCompany.SelectedItem.GetValue("Address1") != null) { _test = _test + (string)_cbCompany.SelectedItem.GetValue("Address1").ToString(); } //(string)_cbCompany.SelectedItem.Value.ToString();
                _test += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("Address2") != null) { _test = _test + (string)_cbCompany.SelectedItem.GetValue("Address2").ToString(); }
                _test += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("Address3") != null) { _test = _test + (string)_cbCompany.SelectedItem.GetValue("Address3").ToString(); }
                _test += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("CountryName") != null) { _test = _test + (string)_cbCompany.SelectedItem.GetValue("CountryName").ToString(); }
                _test += Environment.NewLine;
                if (_cbCompany.SelectedItem.GetValue("TelNo") != null) { _test = _test + (string)_cbCompany.SelectedItem.GetValue("TelNo").ToString(); }
                _test += Environment.NewLine;
            }
            ASPxLabel _lblCompany = (ASPxLabel)this.fmvContainer.FindControl("dxlblWareHouseAddress");
            if (_lblCompany != null) { _lblCompany.Text = _test; }

            //origin port
            bind_origin_port(_id);
            //destination port
            bind_destination_port(_id);
            //origin controller
            bind_origin_controller();
            //destination controller
            bind_destination_controller(); 
        }
        else if (this.fmvContainer.CurrentMode == FormViewMode.ReadOnly)
        {
            //status
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewContainerStatus");
            if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("ContainerStatus", "ContainerStatusTable", "ContainerStatusID", _c.ContainerStatusID); }

            //container type 
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewContainerType");
            if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("ContainerType", "ContainerTypeTable", "ContainerSizeID", _c.SizeTypeID); }

            //voyage
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewVoyage");
            if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Joined", "VoyageTable", "VoyageID", _c.VoyageID); }

            //terms
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewTerms");
            if (_lbl != null) { _lbl.Text = _c.Cycfs == 1 ? "CY" : _c.Cycfs == 2 ? "CFS" : ""; }

            //devan warehouse
            _test = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _c.DevanWarehouseID);
            if (!string.IsNullOrEmpty(_test))
            {

                string[] _lines = _test.Split(Environment.NewLine.ToCharArray()); //split returned string
                //company name
                _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewWarehouseName");
                if (_lbl != null) { _lbl.Text = _lines[0].ToString().Trim(); }
                //address
                _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewWarehouseAddress");
                if (_lbl != null) { _lbl.Text = _test.Replace(_lines[0], "").Trim(); }
            }

            //origin controller
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewOriginController");
            if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _c.OriginControllerID); }

            //destination controller
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewDestController");
            if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _c.DestinationControllerID); }

            //date formatting
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewDeliveryDate");
            if (_lbl != null) { _lbl.Text = _c.DeliveryDate != null ? wwi_func.vdatetime(_c.DeliveryDate.ToString()).ToShortDateString() : ""; }

            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewDevanDate");
            if (_lbl != null) { _lbl.Text = _c.DevanDate != null ? wwi_func.vdatetime(_c.DevanDate.ToString()).ToShortDateString() : ""; }
        }
        else
        { 
            //insert mode bind the non-incremental dll's 
            bind_container_status(); 
            //container type
            bind_container_type();
            //terms
            bind_terms();
            //origin controller
            bind_origin_controller();
            //destination controller
            bind_destination_controller(); 
        }
    }
    //end databound

    protected void update_container(int containerid)
    {
        //for nullable values
        int? _intnull = null;
        DateTime? _dtnull = null;

        try
        {
            ContainerTable _tbl = new ContainerTable(containerid);

            ASPxTextBox _txt = (ASPxTextBox)this.fmvContainer.FindControl("dxtxtContainerNo");
            if (_txt != null) { _tbl.ContainerNumber = _txt.Text.ToString(); }

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerStatus");
            if (_cbo != null) { _tbl.ContainerStatusID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; ; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerType");
            if (_cbo != null) { _tbl.SizeTypeID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()) : _intnull; ; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboVoyage");
            if (_cbo != null) { _tbl.VoyageID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginPort");
            if (_cbo != null) { _tbl.OriginPortID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestPort");
            if (_cbo != null) { _tbl.DestinationPortID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboTerms");
            if (_cbo != null) { _tbl.Cycfs = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboWarehouse");
            if (_cbo != null) { _tbl.DevanWarehouseID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginController");
            if (_cbo != null) { _tbl.OriginControllerID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestController");
            if (_cbo != null) { _tbl.DestinationControllerID =  _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            ASPxMemo _mem = (ASPxMemo)this.fmvContainer.FindControl("dxmemDevan");
            if (_mem != null) { _tbl.DevanNotes = _mem.Text.ToString(); }

            ASPxCheckBox _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDelivered");
            if (_ckb != null) { _tbl.Delivered = _ckb.Value != null? wwi_func.vbool(_ckb.Value.ToString()): false; }

            ASPxDateEdit _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDeliveryDate");
            if (_dte != null) { _tbl.DeliveryDate = _dte.Value != null? wwi_func.vdatetime(_dte.Value.ToString()): _dtnull; }

            _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDevanned");
            if (_ckb != null) { _tbl.Devanned = _ckb.Value != null? wwi_func.vbool(_ckb.Value.ToString()): false; }

            _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDevanDate");
            if (_dte != null) { _tbl.DevanDate = _dte.Value != null? wwi_func.vdatetime(_dte.Value.ToString()): _dtnull; }

            _tbl.Save();
        }
        catch (Exception _ex)
        {
            this.dxlblErr.Text = _ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    protected int insert_container()
    {
        int _newid = 0;
        //for nullable values
        int? _intnull = null;
        DateTime? _dtnull = null;

        try
        {
            ContainerTable _tbl = new ContainerTable();
 
            ASPxTextBox _txt = (ASPxTextBox)this.fmvContainer.FindControl("dxtxtContainerNo");
            if (_txt != null) { _tbl.ContainerNumber = _txt.Text.ToString(); }

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerStatus");
            if (_cbo != null) { _tbl.ContainerStatusID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; ; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerType");
            if (_cbo != null) { _tbl.SizeTypeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; ; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboVoyage");
            if (_cbo != null) { _tbl.VoyageID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginPort");
            if (_cbo != null) { _tbl.OriginPortID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestPort");
            if (_cbo != null) { _tbl.DestinationPortID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboTerms");
            if (_cbo != null) { _tbl.Cycfs = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboWarehouse");
            if (_cbo != null) { _tbl.DevanWarehouseID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginController");
            if (_cbo != null) { _tbl.OriginControllerID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestController");
            if (_cbo != null) { _tbl.DestinationControllerID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }

            ASPxMemo _mem = (ASPxMemo)this.fmvContainer.FindControl("dxmemDevan");
            if (_mem != null) { _tbl.DevanNotes  = _mem.Text.ToString(); }

            ASPxCheckBox _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDelivered");
            if (_ckb != null) { _tbl.Delivered = _ckb.Value != null? wwi_func.vbool(_ckb.Value.ToString()): false; }

            ASPxDateEdit _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDeliveryDate");
            if (_dte != null) { _tbl.DeliveryDate = _dte.Value != null ? wwi_func.vdatetime(_dte.Value.ToString()) : _dtnull; }

            _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDevanned");
            if (_ckb != null) { _tbl.Devanned = _ckb.Value != null? wwi_func.vbool(_ckb.Value.ToString()): false; }

            _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDevanDate");
            if (_dte != null) { _tbl.DevanDate = _dte.Value != null ? wwi_func.vdatetime(_dte.Value.ToString()) : _dtnull; }

            _tbl.Save();

            //get new id
            _newid = (int)_tbl.GetPrimaryKeyValue(); 
        }
        catch (Exception _ex)
        {
            this.dxlblErr.Text = _ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }

        return _newid;
    }
    //end insert

    /// <summary>
    /// do we need these events? Errors are being trapped in save/insert anyway. check for errors on item updated or inserted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvContainer_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
         
            this.dxlblErr.Text = e.Exception.InnerException.ToString();
            this.dxpnlErr.Visible = true;
        }
        else
        {
            //have to do this or formview will automatically swtich to read-only mode after update
            //e.KeepInEditMode = true;
            Response.Redirect("..shipment_containers/container.aspx?&mode=ReadOnly&pno=" + get_token("pno"), true);
        }

    }
    protected void fmvContainer_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        //string _newid = e.Values["ContainerID"].ToString();
  
        if (e.Exception != null)
        {
            this.dxlblErr.Text = e.Exception.InnerException.ToString();
            this.dxpnlErr.Visible = true;
        }
        else
        {
            //have to do this or formview will automatically swtich to read-only mode after update
            //e.KeepInEditMode = true;
            Response.Redirect("..shipment_containers/container.aspx?&mode=ReadOnly&pno=" + get_token("pno"), true);
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
    protected void dxcboVoyage_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VoyageTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "VoyageID").WhereExpression("Joined").StartsWith(string.Format("{0}%", e.Filter.ToString()));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VoyageID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "Joined";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcboVoyage_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VoyageTable).WhereExpression("VoyageID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VoyageID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "Joined";
        _combo.DataBindItems();
        //  }
        //}
    }
    //end incremental filtering of voyage name

    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// for devan warehouse the company list is limted to company typeid = 5
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboWarehouse_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
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
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("{0}%", e.Filter.ToString())).And("TypeId").IsEqualTo(5);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int);
        _combo.TextField = "CompanyName";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcboWarehouse_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer" };

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
    //end incremental filtering of company name
    #endregion

    #region dll binding
    /// <summary>
    /// container status addedm 19/03/15
    /// </summary>
    protected void bind_container_status()
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerStatus");
        if (_combo != null)
        {
            string[] _cols = { "ContainerStatusID, ContainerStatus" };
            string[] _order = { "ContainerStatus" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.ContainerStatusTable).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "ContainerStatusID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "ContainerStatus";
            _combo.DataBindItems();
        }//end if
    }
    /// <summary>
    /// container type
    /// </summary>
    protected void bind_container_type()
    { 
        ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerType");
        if (_combo != null)
        {
            string[] _cols = { "ContainerSizeID, ContainerType" };
            string[] _order = { "ContainerType" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.ContainerTypeTable).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "ContainerSizeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "ContainerType";
            _combo.DataBindItems();  
        }//end if
    }
    //end bind container type
    /// <summary>
    /// container terms only 2 options get from xml
    /// </summary>
    protected void bind_terms()
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboTerms");
        if (_combo != null)
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ddl_items.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "ddls ='containerterms'";

            _combo.DataSource = _dv;
            _combo.ValueType = typeof(int);
            _combo.TextField = "name";
            _combo.ValueField = "value";
            //Databinding methods such as Eval(), XPath(), and Bind() can only be used in the context of a databound control
            _combo.DataBindItems(); 
        }
    }
    //end bind dll terms
    /// <summary>
    /// origin port combo filtered by VoyageID
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_origin_port(int voyageId)
    {
        if (voyageId > 0) //don't call if no voyageid
        {
            ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginPort");
            if (_combo != null)
            {
                string[] _cols = { "VoyageETSSubTable.OriginPortID", "PortTable.PortName" };
                string[] _order = { "PortName" };


                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.VoyageTable).
                    InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn).
                    InnerJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETSSubTable.OriginPortIDColumn).
                    Where(DAL.Logistics.VoyageTable.VoyageIDColumn).IsEqualTo(voyageId);

                //rebind origin ports
                IDataReader _rd = _qry.ExecuteReader();
                _combo.DataSource = _rd;
                _combo.ValueField = "OriginPortID";
                _combo.ValueType = typeof(int);
                _combo.TextField = "PortName";
                _combo.DataBindItems();
            }
        }
    }

    /// <summary>
    /// destination port combo filtered by VoyageID
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_destination_port(int voyageId)
    {
        if (voyageId > 0)  //don't call if no voyageid
        {
            ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestPort");
            if (_combo != null)
            {
                string[] _cols = { "VoyageETASubTable.DestinationPortID", "PortTable.PortName" };
                string[] _order = { "PortName" };

                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.VoyageTable).
                    InnerJoin(DAL.Logistics.VoyageETASubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn).
                    InnerJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETASubTable.DestinationPortIDColumn).
                    Where(DAL.Logistics.VoyageTable.VoyageIDColumn).IsEqualTo(voyageId);

                //rebind dest ports
                IDataReader _rd = _qry.ExecuteReader();
                _combo.DataSource = _rd;
                _combo.ValueField = "DestinationPortID";
                _combo.ValueType = typeof(int);
                _combo.TextField = "PortName";
                _combo.DataBindItems();
            }
        }
    }

    /// <summary>
    /// origin controller
    /// </summary>
    protected void bind_origin_controller()
    {
        //order controller
        ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboOriginController");
        if (_combo != null)
        {
            string[] _cols = { "EmployeeID, Name" };
            string[] _order = { "Name" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }

    /// <summary>
    /// destination controller
    /// </summary>
    protected void bind_destination_controller()
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboDestController");
        if (_combo != null)
        {
            string[] _cols = { "EmployeeID, Name" };
            string[] _order = { "Name" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }

    /// <summary>
    /// package types on allocation panel
    /// </summary>
    protected void bind_package_type()
    {
        ASPxComboBox _combo = (ASPxComboBox)this.dxpnlOrder.FindControl("dxcboAllocatePackageType");
        if (_combo != null)
        {
            string[] _cols = { "PackageTypeID, PackageType" };
            string[] _order = { "PackageType" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PackageTypeTable).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "PackageTypeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PackageType";
            _combo.DataBindItems();
        }
    }
    //end package type
    #endregion

    #region functions
    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_token(string namedtoken)
    {
        string _value = this.dxhfContainer.Contains(namedtoken) ? this.dxhfContainer.Get(namedtoken).ToString() : null;

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

    #region allocation call back
    /// <summary>
    /// check order details
    /// before inserting new order to container following criteria must be met : function validate_order
    /// 1. order origin port = container origin port else error 'container origin port is X and order origin port is Y': function validate_order
    /// 2. order destination port = container destination port else error 'container destination port is X and order destination port is Y': function validate_order
    /// 3. order must have actual weight > 0 else error 'No weight entered for order'
    /// 4. order must have available weight > 0 else error 'This shipment allready allocated': function validate_allocation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbpOrder_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _param = e.Parameter != null? e.Parameter.ToString().ToLower() : "";


        if (_param == "save")
        {
            //called from javacript function beginSave. Prcoess new allocation then window closes
            save_allocated(); 
        }
        else
        {
            //called from javacript function OnOrderEntered. Reset some stuff then check order number
            this.dxhfOrderId.Clear();
            this.dxlblAlert.Text = "";
            this.dxpnlAlert.ClientVisible = false;
            this.dxpnlOrder.ClientVisible = false;
            //check selected order number
            validate_allocations(this.dxtxtOrderNo.Text.ToString());
        }
    }
    //end callback
    #endregion

    #region allocation functions 
    protected double[] get_allocated(int orderid)
    {
        double[] _allocated = { 0, 0, 0 };
        //aggregate values
        //this emulates the query used in Access database (OrderQuantityAllocatedToContainerQuery), but we don't need all this
        //we only need the aggregated number of packages, weight and volumne from container sub table, the other data can just be derived from order table
        //SubSonic.Aggregate[] _ags = {
        //                            Aggregate.Count(DAL.Logistics.ContainerSubTable.ContainerSubIDColumn),
        //                            Aggregate.GroupBy(DAL.Logistics.ContainerSubTable.OrderNumberColumn), 
        //                            Aggregate.GroupBy(DAL.Logistics.ContainerSubTable.PackagesColumn), 
        //                            Aggregate.GroupBy(DAL.Logistics.OrderTable.OrderIDColumn),
        //                            Aggregate.GroupBy(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
        //                            Aggregate.GroupBy(DAL.Logistics.OrderTable.ActualWeightColumn),
        //                            Aggregate.GroupBy(DAL.Logistics.OrderTable.ActualWeightColumn),
        //                            Aggregate.Sum(DAL.Logistics.ContainerSubTable.WeightColumn), 
        //                            Aggregate.Sum(DAL.Logistics.ContainerSubTable.CbmColumn), 
        //                           };
        //SqlQuery _qry = new Select(_ags).From(DAL.Logistics.Tables.OrderTable).
        //      LeftOuterJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn).
        //      Where(DAL.Logistics.OrderTable.OrderIDColumn).IsEqualTo(orderid);   

        //why do we group for packages column and not sum? check with Dave
        SubSonic.Aggregate[] _ags = {
                                    Aggregate.Count(DAL.Logistics.ContainerSubTable.ContainerSubIDColumn,"ContainerSubID"),
                                    Aggregate.GroupBy(DAL.Logistics.ContainerSubTable.PackagesColumn,"SumPackages"), 
                                    Aggregate.Sum(DAL.Logistics.ContainerSubTable.WeightColumn, "SumWeight"), 
                                    Aggregate.Sum(DAL.Logistics.ContainerSubTable.CbmColumn, "SumCbm") 
                                    };

        SqlQuery _qry = new Select(_ags).From(DAL.Logistics.Tables.ContainerSubTable).Where(DAL.Logistics.ContainerSubTable.OrderIDColumn).IsEqualTo(orderid);   

        //DataSet _set = _qry.ExecuteDataSet(); //useful for testing
        //get totals aggregate query might return multiple rows
        IDataReader _rdr = _qry.ExecuteReader();
        while (_rdr.Read())
        {
            _allocated[0] += wwi_func.vdouble(_rdr["SumWeight"].ToString());
            _allocated[1] += wwi_func.vdouble(_rdr["SumPackages"].ToString());
            _allocated[2] += wwi_func.vdouble(_rdr["SumCbm"].ToString());
        }

        return _allocated; 
    }


    protected linq.order_allocationResult validate_order(int ordernumber)
    {
        linq.order_allocationResult _validorder = null;

        IList<linq.order_allocationResult> _orders = new linq.linq_order_allocation_udfDataContext().order_allocation(ordernumber).ToList<linq.order_allocationResult>();
        if (_orders.Count > 0)
        {
            _validorder = _orders[0];
        }
   
        return _validorder;
    }

    protected void validate_allocations(string ordernumber)
    {
        int _orderno = wwi_func.vint(ordernumber);
        string _error = null;

        if (_orderno > 0)
        {
            //retrieve order info
            linq.order_allocationResult _order = validate_order(_orderno);
            double[] _allocated = null;

            if (_order != null)
            {
                //get container ports off hidden fields
                string _containerorign = this.dxhfContainer.Contains("ctnrorigin") ? this.dxhfContainer.Get("ctnrorigin").ToString() : "not identified";
                string _containerdestination = this.dxhfContainer.Contains("ctnrdestination") ? this.dxhfContainer.Get("ctnrdestination").ToString() : "not identified";

                //compare ports
                if (_order.OrignPort != _containerorign)
                {
                    _error = string.Format("container origin port is {0} and order origin port is {1}", _containerorign, _order.OrignPort);
                }

                if (_order.DestinationPort != _containerdestination)
                {
                    _error = string.Format("container destination port is {0} and order destination port is {1}", _containerdestination, _order.DestinationPort);
                }

                if (_order.ActualWeight <= 0)
                {
                    _error = string.Format("No weight entered for order {0}", _orderno.ToString());
                }

                _allocated = get_allocated(_order.OrderID);
                //now check available weight
                if ((_allocated != null) && (_order.ActualWeight - _allocated[0] <= 0))
                {
                    _error = "This shipment allready allocated";
                }
            }
            else
            {
                _error = string.Format("Order number {0} not found", _orderno.ToString());
            }
            //end if

            //display
            //if no error open allocation panel else display error
            this.dxlblAlert.Text = _error;
            this.dxpnlAlert.ClientVisible = _error != null ? true : false;
            this.dxpnlOrder.ClientVisible = _error != null ? false : true;

            //if allocation panel visible pass info to boxes
            if (this.dxpnlOrder.ClientVisible)
            {
                //store order id
                this.dxhfOrderId.Clear();
                this.dxhfOrderId.Add("OrderID", _order.OrderID.ToString());
                this.dxhfOrderId.Add("OrderNo", _order.OrderNumber.ToString());

                //amount to allocate default to what's available?
                this.dxtxtAllocateWeight.Text = (_order.ActualWeight - _allocated[0]).ToString();
                this.dxtxtAllocatePackages.Text = (_order.NumberOfPackages - _allocated[1]).ToString();
                this.dxtxtAllocateCbm.Text = (_order.ActualVolume - _allocated[2]).ToString();
                this.dxcboAllocatePackageType.SelectedItem = this.dxcboAllocatePackageType.Items.FindByValue(_order.PackageTypeID);
                //available do this afte amount to alocate so we can get package type off combo
                this.dxlblOrderWeight.Text = (_order.ActualWeight - _allocated[0]).ToString();
                this.dxlblOrderPackages.Text = (_order.NumberOfPackages - _allocated[1]).ToString();
                this.dxlblOrderCbm.Text = (_order.ActualVolume - _allocated[2]).ToString();
                this.dxlblOrderPackageTypeId.Text = this.dxcboAllocatePackageType.SelectedItem != null ? this.dxcboAllocatePackageType.Text.ToString() : ""; //_order.PackageTypeID.ToString();
            }
        }
    }
    #endregion

    #region allocation crud events
    /// <summary>
    /// append allocated values to container sub table
    /// </summary>
    protected void save_allocated()
    {
        try
        {
            //for nullable values
            int? _intnull = null;

            int _containerid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
            int _orderid = this.dxhfOrderId.Contains("OrderID") ? wwi_func.vint(this.dxhfOrderId.Get("OrderID").ToString()): 0;
            int _orderno = this.dxhfOrderId.Contains("OrderNo") ? wwi_func.vint(this.dxhfOrderId.Get("OrderNo").ToString()) : 0;

            if (_containerid > 0 && _orderid > 0)
            {
                ContainerSubTable _c = new ContainerSubTable();
                _c.ContainerID = _containerid;
                _c.OrderID = _orderid;
                _c.OrderNumber = _orderno;
                _c.PackageTypeID = this.dxcboAllocatePackageType.Value != null ? wwi_func.vint(this.dxcboAllocatePackageType.Value.ToString()) : _intnull;
                //allocated values
                _c.Packages = wwi_func.vint(this.dxtxtAllocatePackages.Text.ToString());
                _c.Weight = wwi_func.vdouble(dxtxtAllocateWeight.Text.ToString());
                _c.Cbm = wwi_func.vdouble(dxtxtAllocateCbm.Text.ToString());

                _c.Save();

                //Doesn't matter, changed save button to client-side event. NONE OF THESE SCRIPS WORK. WHY? if successful save, close pop-up
                //string script = string.Format("<script type=\"text/javascript\"> hideAllocationWindow(); </script>");
                //ClientScript.RegisterStartupScript(Type.GetType("System.String"), "key", script);
                //ClientScript.RegisterStartupScript(GetType(), "EXT_KEY", "window.ppcContainer.HideWindow(window.ppcContainer.GetWindowByName('ppcAllocateOrder'));", true);
            }
            else
            {
                this.dxlblAlert.Text = "Not able to save record. Container ref or Order ref not found";
                this.dxpnlAlert.ClientVisible = true;
            }
        }
        catch (Exception ex)
        {
            string _er = ex.Message.ToString();
            this.dxlblAlert.Text = _er;
            this.dxpnlAlert.Visible = true;
        }
    }


    #endregion

}
