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
using System.Web.Services;
using DAL.Logistics;
using DevExpress.Web.ASPxPanel;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using SubSonic;


public partial class move_container : System.Web.UI.Page
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
                set_mode(_mode);
                bind_container(_mode);
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
            int _containerid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

            ContainerTable _tbl = new ContainerTable(_containerid);
            _tbc.Add(_tbl);
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
    /// command menu 
    /// </summary>
    protected void bind_container_commands()
    {
        //using generic FormView commands 
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='MoveContainer']"; //you need this or tab will not databind!
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

    //mode changing
    protected void fmvContainer_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvContainer.ChangeMode(e.NewMode);
    }
    #endregion

    #region web methods (must include using System.Web.Services;)
    [WebMethod]
    public static string get_button_response(string keyid, string commandmethod)
    {
        //page to redirect build as required
        string _page = "";

        //track requesting page so we can return to it
        //can't use 'this' in a static method
        //string _req = this.dxhfOrder.Get("req").ToString(); //populated on page load   //wwi_security.EncryptString("Search", "publiship");
        //string _req = wwi_security.EncryptString("Search", "publiship");
        
        //commandid is taken from custom button name
        switch (commandmethod)
        {
            case "cmdVessel":
                {
                    //open voyage search form, no pid required
                    _page = string.Format("../shipment_voyage/voyage_search.aspx");
                    //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page);  //Response.Redirect(_page); 
                    break;
                }
            case "cmdContainer":
                {
                    //container id
                    //container form using container id stord in form binding
                    _page = string.Format("../shipment_containers/container.aspx?mode={0}&pid={1}", "ReadOnly", keyid);
                    //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            default:
                {
                    break;
                }
        }//end switch
     
        return _page;
    }
    //end web method
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
                    _menuitems.Add("MoveContainer");
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvContainer.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("MoveContainer");
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
                    set_mode("ReadOnly");
                    bind_container("ReadOnly");
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
            case "cmdMoveContainer":
                {
                    int _containerid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
                    int _vesselid = this.dxcboNewVessel.Value != null? wwi_func.vint(this.dxcboNewVessel.Value.ToString()): 0;
 
                    if (_containerid > 0 && _vesselid > 0)
                    {
                        if (change_vessel(_containerid, _vesselid))
                        {
                            //show panel
                            this.dxlblInfo.Text = "Container has been moved";
                            this.dxpnlMsg.ClientVisible = true;
                            set_mode("ReadOnly");
                            bind_container("ReadOnly"); 
                        }
                    }
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
    /// origin port combo filtered by VoyageID called client-side when voayge is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOriginPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString());
        bind_origin_port(_filter);

    }

    /// <summary>
    /// destination port combo filterd by VoyageID called client-side when voayge is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbDestPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString());
        bind_destination_port(_filter);

    }
    #endregion

    #region form crud events
    protected void fmvContainer_DataBound(object sender, EventArgs e)
    {
        string _test = "";
        ASPxLabel _lbl = null;
        ContainerTable _c = (ContainerTable)this.fmvContainer.DataItem;

        this.dxlblStatus.Text = this.fmvContainer.CurrentMode == FormViewMode.Insert ? "New container" : _c.LoadedOnBoard == true ? "Container confirmed 'LOADED ON BOARD'" : "Container not yet confirmed on board";

        //container id to hidden field keep encryption we use it for web methods
        string _cid = get_token("pid");
        if (this.dxhfContainer.Contains("containerid")) { this.dxhfContainer.Remove("containerid"); }
        this.dxhfContainer.Add("containerid", _cid);

        //add them to hidden fields as well as the ports are used to determine if an order can be allocated
        if (_c != null)
        {
            //origin port
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _c.OriginPortID);
            //origin port id
            if (this.dxhfContainer.Contains("ptstart")) { this.dxhfContainer.Remove("ptstart"); }
            this.dxhfContainer.Add("ptstart", _c.OriginPortID);
            //origin port name
            if (this.dxhfContainer.Contains("ctnrorigin")) { this.dxhfContainer.Remove("ctnrorigin"); }
            this.dxhfContainer.Add("ctnrorigin", _test);
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewOriginPort");
            if (_lbl != null)
            { _lbl.Text = _test; }


            //destination port
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _c.DestinationPortID);
            //destination port id
            if (this.dxhfContainer.Contains("ptend")) { this.dxhfContainer.Remove("ptend"); }
            this.dxhfContainer.Add("ptend", _c.OriginPortID);
            //destination port name
            if (this.dxhfContainer.Contains("ctnrdestination")) { this.dxhfContainer.Remove("ctnrdestination"); }
            this.dxhfContainer.Add("ctnrdestination", _test);
            _lbl = (ASPxLabel)this.fmvContainer.FindControl("dxlblViewDestPort");
            if (_lbl != null)
            {
                _lbl.Text = _test;
            }
        }

        //look up values these for edit/insert modes
        if (this.fmvContainer.CurrentMode == FormViewMode.Edit)
        {
            //get voyageid but no need to bind voyage as it is bound using incremental filtering as a large dataset
            int _id = (int)_c.VoyageID;
            //dll binding
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

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerType");
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
            if (_cbo != null) { _tbl.DestinationControllerID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            ASPxMemo _mem = (ASPxMemo)this.fmvContainer.FindControl("dxmemDevan");
            if (_mem != null) { _tbl.DevanNotes = _mem.Text.ToString(); }

            ASPxCheckBox _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDelivered");
            if (_ckb != null) { _tbl.Delivered = _ckb.Value != null ? wwi_func.vbool(_ckb.Value.ToString()) : false; }

            ASPxDateEdit _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDeliveryDate");
            if (_dte != null) { _tbl.DeliveryDate = _dte.Value != null ? wwi_func.vdatetime(_dte.Value.ToString()) : _dtnull; }

            _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDevanned");
            if (_ckb != null) { _tbl.Devanned = _ckb.Value != null ? wwi_func.vbool(_ckb.Value.ToString()) : false; }

            _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDevanDate");
            if (_dte != null) { _tbl.DevanDate = _dte.Value != null ? wwi_func.vdatetime(_dte.Value.ToString()) : _dtnull; }

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

            ASPxComboBox _cbo = (ASPxComboBox)this.fmvContainer.FindControl("dxcboContainerType");
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
            if (_cbo != null) { _tbl.DestinationControllerID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

            ASPxMemo _mem = (ASPxMemo)this.fmvContainer.FindControl("dxmemDevan");
            if (_mem != null) { _tbl.DevanNotes = _mem.Text.ToString(); }

            ASPxCheckBox _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDelivered");
            if (_ckb != null) { _tbl.Delivered = _ckb.Value != null ? wwi_func.vbool(_ckb.Value.ToString()) : false; }

            ASPxDateEdit _dte = (ASPxDateEdit)this.fmvContainer.FindControl("dxdtDeliveryDate");
            if (_dte != null) { _tbl.DeliveryDate = _dte.Value != null ? wwi_func.vdatetime(_dte.Value.ToString()) : _dtnull; }

            _ckb = (ASPxCheckBox)this.fmvContainer.FindControl("dxckDevanned");
            if (_ckb != null) { _tbl.Devanned = _ckb.Value != null ? wwi_func.vbool(_ckb.Value.ToString()) : false; }

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
    /// <summary>
    /// new vessel selection
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboNewVessel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //if (_filter != "")
        //{
            //use datareaders - much faster than loading into collections
            string[] _cols = { "VoyageID", "Joined", "ETS", "ETA", "DestinationPortID", "OriginPortID" };
            string[] _sort = { "Joined" };
            //additional filters on this dll
            string _originportid = this.dxhfContainer.Contains("ptstart") ? this.dxhfContainer.Get("ptstart").ToString() : "0";
            string _destportid = this.dxhfContainer.Contains("ptend") ? this.dxhfContainer.Get("ptend").ToString() : "0";
            //
            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("Page2VesselView").
                    Paged(e.BeginIndex + 1, e.EndIndex + 1, "VoyageID").Where("Joined").Like(string.Format("{0}%", e.Filter.ToString())).And("DestinationPortID").IsEqualTo(_destportid).And("OriginPortID").IsEqualTo(_originportid).OrderAsc(_sort);

            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "VoyageID";
            _combo.TextField = "Joined";
            _combo.DataBindItems();
        //} //endif
    }
    protected void dxcboNewVessel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined", "ETS", "ETA", "DestinationPortID", "OriginPortID" };

        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("Page2VesselView").WhereExpression("VoyageID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VoyageID";
        _combo.TextField = "Joined";
        _combo.DataBindItems();

    }
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
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VoyageTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "VoyageID").WhereExpression("Joined").Like(string.Format("{0}%", e.Filter.ToString()));

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

    //end package type
    #endregion

    #region move container and orders
    /// <summary>
    /// move container to a different vessel
    /// 1. update Order table set VesselID = 'new vessel id', VesselLastUpdated = current date where containerid = N .Equivalent to access "ContainerVesselUpdateVesselQuery"
    /// 2. update Order table set ETS = VoyageETSSubTable.ETS, ETA=VoyageETSSubTable.ETA where containerid = N .Equivalent to access "ContainerVesselUpdateDatesQuery"
    /// 3. update Container table set VoyageID = 'new voyage id' where containerid = N .Equivalent to access ""ContainerVesselUpdateContainerQuery"
    /// </summary>
    /// <param name="containerid"></param>
    protected bool change_vessel(int containerid, int vesselid)
    {
        bool _changed = false;
        DateTime _currentdate = DateTime.Now;

        //for testing
        //containerid = 2332;

        using (SharedDbConnectionScope _sc = new SharedDbConnectionScope())
        {
            using (System.Transactions.TransactionScope _ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    //for testing q1
                    //string _q1 = "SELECT o.OrderID, VesselId, VesselLastUpdated FROM OrderTable as o INNER JOIN  " + 
                    //"ContainerTable AS c INNER JOIN " +
                    //"ContainerSubTable AS s ON c.ContainerID = s.ContainerID ON o.OrderNumber = s.OrderNumber WHERE (c.ContainerID = 2332);";
                    //object[] _p1 = { containerid };
                    //int _result = new SubSonic.CodingHorror().ExecuteScalar<int>(_q1, _p1);
                    OrderTableCollection _q2 = new Select().From(DAL.Logistics.Tables.OrderTable)
                        .InnerJoin(DAL.Logistics.ContainerSubTable.OrderNumberColumn, DAL.Logistics.OrderTable.OrderNumberColumn)
                        .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
                        .Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid).ExecuteAsCollection<OrderTableCollection>();

                    for (int _ix = 0; _ix < _q2.Count; _ix++)
                    {
                        _q2[_ix].VesselID = vesselid;
                        _q2[_ix].VesselLastUpdated = _currentdate;
            
                    }
                    _q2.SaveAll();
                    //does not work because the triggers on the ordertable cause errors 
                    //Subquery returned more than 1 value. This is not permitted when the subquery follows =, !=, <, <= , >, >= or when the subquery is used as an expression
                    //1st statement paramatised and using subsonic.codinghorror, table aliases prevent 'multi-part identifier can't be bound' tsql error
                    //string _q1 = "UPDATE OrderTable " +
                    //                "SET VesselID = @vesselid, VesselLastUpdated = @lastupdated " + 
                    //                "FROM OrderTable INNER JOIN " + 
                    //                "ContainerTable AS c INNER JOIN " + 
                    //                "ContainerSubTable AS s ON c.ContainerID = s.ContainerID ON OrderTable.OrderNumber = s.OrderNumber " + 
                    //                "WHERE (c.ContainerID = @containerid);";
                    //
                    //object[] _p1 = { vesselid, _currentdate,  containerid };
                    //int _result = new SubSonic.CodingHorror().ExecuteScalar<int>(_q1, _p1);

                    //for testing q2
                    //SELECT c.ContainerID, ets.ets, eta.eta, OrderTable.OrderID FROM ContainerTable AS c INNER JOIN 
                    //ContainerSubTable AS s ON c.ContainerID = s.ContainerID INNER JOIN 
                    //VoyageTable AS v INNER JOIN VoyageETASubTable AS eta ON v.VoyageID = eta.VoyageID 
                    //INNER JOIN VoyageETSSubTable AS ets ON v.VoyageID = ets.VoyageID INNER JOIN 
                    //OrderTable ON v.VoyageID = OrderTable.VesselID ON s.OrderNumber = OrderTable.OrderNumber
                    //WHERE (c.ContainerID = @containerid);
                    //object[] _p2 = { containerid };
                    //_result = new SubSonic.CodingHorror().ExecuteScalar<int>(_q2, _p2);
                    //q2
                    string[] _cols2 = { "OrderTable.OrderID", "VoyageETSSubTable.ETS", "VoyageETASubTable.ETA" };
                    DataTable _dt = new Select(_cols2).From(DAL.Logistics.Tables.OrderTable)
                    .InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn)
                    .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
                    .InnerJoin(DAL.Logistics.VoyageTable.VoyageIDColumn, DAL.Logistics.OrderTable.VesselIDColumn)
                    .InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn)
                    .InnerJoin(DAL.Logistics.VoyageETASubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn)
                    .Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid).ExecuteDataSet().Tables[0];

                    if (_dt.Rows.Count > 0)
                    {
                        OrderTableCollection _ot = new OrderTableCollection(); 
                        for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
                        {
                            int _id = wwi_func.vint(_dt.Rows[_ix]["OrderID"].ToString());
                            DateTime _ets = wwi_func.vdatetime(_dt.Rows[_ix]["Ets"].ToString());
                            DateTime _eta = wwi_func.vdatetime(_dt.Rows[_ix]["Eta"].ToString());
                            //update
                            OrderTable _tb = new OrderTable(_id);
                            _tb.Ets = _ets;
                            _tb.Eta = _eta;
                            _ot.Add(_tb); 
                        }
                        _ot.SaveAll(); 
                    }
                    //does not work because the triggers on the ordertable cause errors 
                    //2nd statement paramatised and using subsonic.codinghorror, table aliases prevent 'multi-part identifier can't be bound' tsql error
                    //string _q2 = "UPDATE OrderTable " + 
                    //                "SET ETS = ets.ETS, ETA = eta.ETA " + 
                    //                "FROM ContainerTable AS c INNER JOIN " + 
                    //                "ContainerSubTable AS s ON c.ContainerID = s.ContainerID INNER JOIN " + 
                    //                "VoyageTable AS v INNER JOIN " + 
                    //                "VoyageETASubTable AS eta ON v.VoyageID = eta.VoyageID INNER JOIN " + 
                    //                "VoyageETSSubTable AS ets ON v.VoyageID = ets.VoyageID INNER JOIN " + 
                    //                "OrderTable ON v.VoyageID = OrderTable.VesselID ON s.OrderNumber = OrderTable.OrderNumber " + 
                    //                "WHERE (c.ContainerID = @containerid);";
                    //object[] _p2 = { containerid };
                    //_result = new SubSonic.CodingHorror().ExecuteScalar<int>(_q2, _p2);
                    
                    //3rd statement 
                    Update _q3 = new Update(DAL.Logistics.Tables.ContainerTable);
                    _q3.Set(DAL.Logistics.ContainerTable.VoyageIDColumn).EqualTo(vesselid);
                    _q3.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
                    //_test = _q3.ToString();
                    _q3.Execute();   


                    //commit transaction
                    _ts.Complete();

                    _changed = true;
                }
                catch (Exception ex)
                {
                    
                    string _er = ex.Message.ToString();
                    this.dxlblErr.Text = _er;
                    this.dxpnlErr.ClientVisible = true;
                }

            }
        }

        return _changed;
    }
    /// <summary>
    /// deprecated version
    /// </summary>
    /// <param name="containerid"></param>
    /// <param name="vesselid"></param>
    /// <returns></returns>
    protected bool change_vessel_deprecated(int containerid, int vesselid)
    {
        bool _changed = false;
        DateTime _currentdate = DateTime.Now;

        //for testing
        containerid = 2332;

        try
        {
            //for testing q1 ********
            //SqlQuery _s1 = new Select(DAL.Logistics.Tables.OrderTable);
            //_s1.InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn);
            //_s1.InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn);
            //_s1.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
            //DataTable _dt1 = _s1.ExecuteDataSet().Tables[0];

            string[] _cols1 = { "OrderTable.OrderID", "OrderTable.VesselID", "OrderTable.VesselLastUpdated" };
            DataTable _dt = new Select(_cols1).From(DAL.Logistics.Tables.OrderTable)
            .InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn)
            .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
            .Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid).ExecuteDataSet().Tables[0];
            
            if (_dt.Rows.Count > 0)
            {
                for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
                {
                    int _id = wwi_func.vint(_dt.Rows[_ix]["OrderID"].ToString());
                    //update
                    OrderTable _tb = new OrderTable(_id);
                    _tb.VesselID = vesselid;
                    _tb.VesselLastUpdated = _currentdate;
                    _tb.Save(); 
                }
            }
            
            
            //alternative method?
            //IList<int> _ids = new Select().From(DAL.Logistics.Tables.OrderTable)
            //.InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn)
            //.InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
            //.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid).ExecuteTypedList<int>();
            //
            //IList<int> _q1 = new SubSonic.Update(DAL.Logistics.Tables.OrderTable)
            //    .Set(OrderTable.Columns.VesselID).EqualTo(vesselid)
            //    .Set(OrderTable.Columns.VesselLastUpdated).EqualTo(_currentdate)
            //    .Where(OrderTable.Columns.OrderID).In(_ids).ExecuteTypedList<int>();  
            
            //for testing q2 *******
            //Update _s2 = new Update(DAL.Logistics.Tables.OrderTable);
            //_s2.InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn);
            //_s2.InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn);
            //_s2.InnerJoin(DAL.Logistics.VoyageTable.VoyageIDColumn, DAL.Logistics.OrderTable.VesselIDColumn);
            //_s2.InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn);
            //_s2.InnerJoin(DAL.Logistics.VoyageETASubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn);
            //_s2.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
            //DataTable _dt2 = _s2.ExecuteDataSet().Tables[0];
            //**********************

            //can't use an update query here as there will likely be multiple orders to update and you will get 'multi-part identifier can't be bound'
            //get data with reader as we need the ETS and ETA values
            string[] _cols2 = { "OrderTable.OrderID", "VoyageETSSubTable.ETS", "VoyageETASubTable.ETA" };
            _dt = new Select(_cols2).From(DAL.Logistics.Tables.OrderTable)
            .InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn)
            .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
            .InnerJoin(DAL.Logistics.VoyageTable.VoyageIDColumn, DAL.Logistics.OrderTable.VesselIDColumn)
            .InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn)
            .InnerJoin(DAL.Logistics.VoyageETASubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn)
            .Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid).ExecuteDataSet().Tables[0];
 
            for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
            {
                int _id = wwi_func.vint(_dt.Rows[_ix]["OrderID"].ToString());
                DateTime _ets = wwi_func.vdatetime(_dt.Rows[_ix]["Ets"].ToString());
                DateTime _eta = wwi_func.vdatetime(_dt.Rows[_ix]["Eta"].ToString());
                //update
                OrderTable _tb = new OrderTable(_id);
                _tb.Ets = _ets;
                _tb.Eta = _eta;
                _tb.Save();
            }
            
            //can't do this as causes error multi-part identifier can't be bound
            //Update _q2 = new Update(DAL.Logistics.Tables.OrderTable);
            //    _q2.InnerJoin(DAL.Logistics.ContainerSubTable.OrderIDColumn, DAL.Logistics.OrderTable.OrderIDColumn);
            //    _q2.InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn);    
            //    _q2.InnerJoin(DAL.Logistics.VoyageTable.VoyageIDColumn, DAL.Logistics.OrderTable.VesselIDColumn);
            //    _q2.InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn);
            //    _q2.InnerJoin(DAL.Logistics.VoyageETASubTable .VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn);
            //    _q2.Set(DAL.Logistics.OrderTable.EtsColumn).EqualTo(DAL.Logistics.VoyageETSSubTable.EtsColumn);
            //    _q2.Set(DAL.Logistics.OrderTable.EtaColumn).EqualTo(DAL.Logistics.VoyageETASubTable.EtaColumn);
            //    _q2.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
            //_test = _q2.ToString();      
            //_q2.Execute();   

            //for testing q3 *******
            //SqlQuery _s3 = new Select(DAL.Logistics.Tables.ContainerTable);
            //_s3.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
            //DataTable _dt3 = _s3.ExecuteDataSet().Tables[0];
            //**********************

            Update _q3 = new Update(DAL.Logistics.Tables.ContainerTable);
            _q3.Set(DAL.Logistics.ContainerTable.VoyageIDColumn).EqualTo(vesselid);
            _q3.Where(DAL.Logistics.ContainerTable.ContainerIDColumn).IsEqualTo(containerid);
            //_test = _q3.ToString();
            _q3.Execute();   

            _changed = true;
        }
        catch (Exception ex)
        {
            string _er = ex.Message.ToString();
            this.dxlblErr.Text = _er;
            this.dxpnlErr.Visible = true;
        }

        return _changed;
    }
    #endregion

    #region click events
    /// <summary>
    /// deprecated redirect to voyage form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnVessel_Click(object sender, EventArgs e)
    {
        //check primary key VoyageID stored in hiddenfields on formview databind
        int _id = wwi_func.vint(wwi_security.DecryptString(get_token("voyage"), "publiship"));
        if (_id > 0)
        {
            Response.Redirect(string.Format("../shipping_voyage/voyage.aspx?pno={0}&mode=Readonly", _id));
        }
    }
    /// <summary>
    /// deprecated redirct to container form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnContainer_Click(object sender, EventArgs e)
    {
        //check primary key ContainerID
        int _id = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        if (_id > 0)
        { 
            Response.Redirect(string.Format("../shipping_containers/container.aspx?pno={0}&mode=Readonly",_id)); 
        }
    }
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
}
