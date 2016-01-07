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
using DevExpress.Web.ASPxHiddenField;
using SubSonic;


public partial class housebl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for testing only *****
        //this.dxhfOrder.Clear(); 
        //this.dxhfOrder.Add("pno", 185);
        //**********************
        string _mode = get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
        
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                bind_formview_commands(); //crud commands for formview
                set_mode(_mode);
                bind_formview(_mode); 
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_bol/bill_of_lading", "publiship")); 
        }
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    #region menu binding
    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_formview_commands()
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
        this.dxmnuFormView.DataSource = _xml;
        this.dxmnuFormView.DataBind();

    }

    /// <summary>
    /// on menu databound modify navigate urls to current page and primary key (pno) 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxmnuFormView_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
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
    #endregion

    #region command and tab events
    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvBol.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvBol.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvBol.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvBol.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }

    //requried of formview mode will not change
    protected void fmvBol_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvBol.ChangeMode(e.NewMode);
    }

    //crud commands for formview
    protected void dxmnuFormView_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();
        List<string> _menuitems = new List<string>();
        
        switch (_item)
            {
                case "cmdNew":
                    {
                        set_mode("Insert");
                        //call databind to change mode
                        bind_formview("Insert"); 
                        break;
                    }
                case "cmdEdit":
                    {
                        set_mode("Edit");
                        //call databind to change mode
                        bind_formview("Edit"); 
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                       break;
                    }
                case "cmdUpdate":
                    {
                        update_housebl();
                        set_mode("ReadOnly");
                        //call databind to change mode
                        bind_formview("ReadOnly"); 
                        //this.fmvBol.UpdateItem(false);
                        //set_mode("view"); not necesary form will revert to read only after save
                        break;
                    }
                case "cmdSave":
                    {
                        int _newid = insert_housebl();
                       
                        if (_newid > 0)
                        {
                            //set_mode("ReadOnly");
                            //call databind to change mode
                            //bind_formview("ReadOnly");
                            //string _mode = "ReadOnly";
                            //string _pid = wwi_security.EncryptString(_newid.ToString(), "publiship");  
                            //Response.Redirect(string.Format("../shipment_bol/housebl.aspx?pid={0}&mode={1}", _pid, _mode));
                            string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath),
                                                wwi_security.EncryptString(_newid.ToString(), "publiship"), 
                                                "ReadOnly" };
                            string _url = string.Format("{0}\\{1}.aspx?pid={2}&mode={3}", _args);
                            Response.Redirect(_url);
      
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
                        //call databind to change mode
                        bind_formview("ReadOnly"); 
                        break;
                    }
                case "cmdClose":
                    {
                        string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "housebl_search",};
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

        for (int _ix = 0; _ix < this.dxmnuFormView.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuFormView.Items[_ix].Name == "cmd" + active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmnuFormView.Items[_ix].ClientVisible = _isactive;
        }
    }//end menu names loop


    #endregion

    #region form call backs
    /// <summary>
    /// origin port combo filtered by VoyageID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOriginPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString()) ;
        //ASPxComboBox dxcboVoyage = (ASPxComboBox)this.fmvBol.FindControl("dxcboVoyageID");
        //if (dxcboVoyage != null)
        //{
        //    if (dxcboVoyage.Value != null)
        //    {
        //        _filter = wwi_func.vint(dxcboVoyage.Value.ToString());
        //    }
        //}

        bind_origin_port(_filter);
       
    }

    /// <summary>
    /// destination port combo filterd by VoyageID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbDestPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _filter = wwi_func.vint(e.Parameter.ToString());
        //ASPxComboBox dxcboVoyage = (ASPxComboBox)this.fmvBol.FindControl("dxcboVoyageID");
        //if (dxcboVoyage != null)
        //{
        //    if (dxcboVoyage.Value != null)
        //    {
        //        _filter = wwi_func.vint(dxcboVoyage.Value.ToString());
        //    }
        //}

        bind_destination_port(_filter);

    }
    #endregion

    #region formview crud events
    protected void bind_formview(string mode)
    {
        //try
        //{
            //have to use a collection as formview needs to bind to enumerable
            HouseBLTableCollection _hbl = new HouseBLTableCollection();
            if (mode != "Insert")
            {
                //get key id
                int _hblid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

                HouseBLTable _tbl = new HouseBLTable(_hblid);
                _hbl.Add(_tbl);
            }
            else
            {
                HouseBLTable _tbl = new HouseBLTable();
                _hbl.Add(_tbl);
            }
            this.fmvBol.DataSource = _hbl;
            this.fmvBol.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    this.dxlblErr.Text = ex.Message.ToString();
        //    this.dxpnlErr.ClientVisible = true;
        //}
    }
    //end form binding

    /// <summary>
    /// on databound get data for dlls, etc
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvBOL_DataBound(object sender, EventArgs e)
    {
        string _test = "";
        HouseBLTable _t = (HouseBLTable)this.fmvBol.DataItem;
               
        //lookup values for readonly mode
        if (this.fmvBol.CurrentMode == FormViewMode.ReadOnly && _t != null)
        {
            this.dxlblStatus.Text = _t.HouseBLNumber.ToString();
            //agent at destination
            _test = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _t.AgentAtDestinationID);
            string[] _lines = _test.Split(Environment.NewLine.ToCharArray()); //split returned string
            ASPxLabel _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblAgentAtDestinationIDView");
            if (_lbl != null) {   _lbl.Text = _lines[0].Trim(); } //company name

            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblAgentAtDestinationIDSub"); //address
            if (_lbl != null) { _lbl.Text = _test.Replace(_lines[0],"").Trim() ; }

            //consignee
            _test = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _t.ConsigneeID);
            _lines = _test.Split(Environment.NewLine.ToCharArray());
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblConsigneeIDView");
            if (_lbl != null) { _lbl.Text = _lines[0].Trim(); } //name

            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblConsigneeIDSub");
            if (_lbl != null && _test != "") { _lbl.Text = _test.Replace(_lines[0], "").Trim(); } //address

            //vessel
            _test = wwi_func.lookup_multi_values("Joined", "VoyageTable", "VoyageID", _t.VoyageID);
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblVoyageIDView");
            if (_lbl != null) { _lbl.Text = _test; }
            
            //housebldate is databound just format it
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblHouseBLDateView");
            if (_lbl != null) { _lbl.Text = wwi_func.vdatetime(_lbl.Text.ToString()).ToShortDateString(); }
            //ets is bound in bol table as well as voyage sub table
            //_test = wwi_func.lookup_value("ETS", "VoyageETSSubTable", "VoyageID", _t.VoyageID, true);
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtsSub");
            if (_lbl != null) { 
                _lbl.Text = _lbl.Text != ""? wwi_func.vdatetime(_lbl.Text).ToShortDateString(): "";
                //add to hidden field
                this.dxhfOrder.Remove("ets");
                this.dxhfOrder.Set("ets", _lbl.Text);   
            }

            //eta is bound in bol table as well as voyage sub table
             //_test = wwi_func.lookup_value("ETA", "VoyageETASubTable", "VoyageID", _t.VoyageID, true);
             _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtaSub");
             if (_lbl != null) { 
                 _lbl.Text = _lbl.Text != ""? wwi_func.vdatetime(_lbl.Text).ToShortDateString(): "";
                    //add to hidden field
                    this.dxhfOrder.Remove("eta");
                    this.dxhfOrder.Set("eta", _lbl.Text);   
             }

            //origin port and ets
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _t.OriginPort);
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblOriginPortView");
            if (_lbl != null) {_lbl.Text = _test ; }

            //destination port and eta
            _test = wwi_func.lookup_value("PortName", "PortTable", "PortID", _t.DestinationPort);
            _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblDestinationPortView");
            if (_lbl != null) {_lbl.Text = _test; }

            //saved orders list binding using hblnumber
            bind_housebl_orders(_t.HouseBLNumber.ToString());
            //available orders list binding
            bind_housebl_orders(wwi_func.vint(_t.ConsigneeID.ToString()), wwi_func.vint(_t.OriginPort.ToString()), wwi_func.vint(_t.DestinationPort.ToString()));
        
        }
        else if (this.fmvBol.CurrentMode == FormViewMode.Edit && _t != null)
        {
            this.dxlblStatus.Text = _t.HouseBLNumber.ToString();

            string[] _fields = { "AgentAtDestinationID", "ConsigneeID" };
            string _s = "";
            //step through field names
            //get combobox value from dxcbo<fieldname>Edit
            //set address text on dxlbl<fieldname>Edit label
            for (int _ix = 0; _ix < _fields.Length; _ix++)
            {
                _s = "";
                ASPxComboBox _cboCompany = (ASPxComboBox)this.fmvBol.FindControl("dxcbo" + _fields[_ix]);
                if (_cboCompany != null && _cboCompany.SelectedItem != null && _cboCompany.Value != null)
                {
                    if (_cboCompany.SelectedItem.GetValue("Address1") != null) { _s = _s + (string)_cboCompany.SelectedItem.GetValue("Address1").ToString(); } //(string)_cboCompany.SelectedItem.Value.ToString();
                    _s += Environment.NewLine;
                    if (_cboCompany.SelectedItem.GetValue("Address2") != null) { _s = _s + (string)_cboCompany.SelectedItem.GetValue("Address2").ToString(); }
                    _s += Environment.NewLine;
                    if (_cboCompany.SelectedItem.GetValue("Address3") != null) { _s = _s + (string)_cboCompany.SelectedItem.GetValue("Address3").ToString(); }
                    _s += Environment.NewLine;
                    if (_cboCompany.SelectedItem.GetValue("CountryName") != null) { _s = _s + (string)_cboCompany.SelectedItem.GetValue("CountryName").ToString(); }
                    _s += Environment.NewLine;
                    if (_cboCompany.SelectedItem.GetValue("TelNo") != null) { _s = _s + (string)_cboCompany.SelectedItem.GetValue("TelNo").ToString(); }
                    _s += Environment.NewLine;

                }
                ASPxLabel _lblCompany = (ASPxLabel)this.fmvBol.FindControl("dxlbl" + _fields[_ix] + "Sub");
                if (_lblCompany != null) { _lblCompany.Text = _s; }
            }//end loop

            ASPxComboBox _cbVoyage = (ASPxComboBox)this.fmvBol.FindControl("dxcboVoyageID");
            if (_cbVoyage != null && _cbVoyage.SelectedItem != null && _cbVoyage.Value != null)
            {
                ASPxLabel _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtsSub");
                if (_lbl != null) { 
                    _lbl.Text = _t.Ets != null?  wwi_func.vdatetime(_t.Ets.ToString()).ToShortDateString(): "";
                    //add to hidden field
                    this.dxhfOrder.Remove("ets");
                    this.dxhfOrder.Set("ets", _t.Ets);   
                }
                //if (_lbl != null) { _lbl.Text = _cbVoyage.SelectedItem.GetValue("ETS") != null ? _cbVoyage.SelectedItem.GetValue("ETS").ToString() : ""; }


                _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtaSub");
                if (_lbl != null) { 
                    _lbl.Text = _t.Eta != null ? wwi_func.vdatetime(_t.Eta.ToString()).ToShortDateString() : "";
                    //add to hidden field
                    this.dxhfOrder.Remove("eta");
                    this.dxhfOrder.Set("eta", _t.Eta);   
                }
                //if (_lbl != null) { _lbl.Text = _cbVoyage.SelectedItem.GetValue("ETA") != null ? _cbVoyage.SelectedItem.GetValue("ETA").ToString() : ""; }
            }//endif

            //dll binding
            //get voyageid
            //int _id = 0;
            //ASPxComboBox _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboVoyageID");
            //if (_cbo != null) { _id = wwi_func.vint(_cbo.SelectedItem.GetValue("VoyageID").ToString()); }
            int _voyageid = wwi_func.vint(_t.VoyageID.ToString());
            bind_origin_port(_voyageid);
            bind_destination_port(_voyageid);

            //saved orders list binding using hblnumber
            bind_housebl_orders(_t.HouseBLNumber.ToString());
            //available orders list binding
            bind_housebl_orders(wwi_func.vint(_t.ConsigneeID.ToString()), wwi_func.vint(_t.OriginPort.ToString()), wwi_func.vint(_t.DestinationPort.ToString()));

        }
        else if (this.fmvBol.CurrentMode == FormViewMode.Insert)
        {
            this.dxlblStatus.Text = "New record";
            this.dxcbpOrderNumbers.Enabled = false;
        }
        //end formview mode
    }
    //end databound

    /// <summary>
    /// update house bl table by houseblID
    /// </summary>
    /// <param name="hblid">int</param>
    protected void update_housebl()
    {
        int _hblid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship")); 

        if (_hblid > 0)
        {
            try
            {
                //new instance of record
                HouseBLTable _hbl = new HouseBLTable(_hblid);
                //for nullable dates
                DateTime? _dtnull = null;
                //for nullable ints
                int? _intnull = null;

                //get values off editform
                //hbl number
                ASPxTextBox _txt = (ASPxTextBox)this.fmvBol.FindControl("dxtxtHouseBLNumber");
                if (_txt != null) { _hbl.HouseBLNumber = _txt.Text.ToString(); }
                //hbl date
                ASPxDateEdit _dte = (ASPxDateEdit)this.fmvBol.FindControl("dxdtHBLdate");
                //if (_dte != null) { _hbl.HBLDate = _dte.Date != null ? _dte.Date : _dtnull; }
                if (_dte != null && _dte.Value != null) { _hbl.HBLDate = _dte.Date; }

                //agent at destination
                ASPxComboBox _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboAgentAtDestinationID");
                if (_cbo != null) { _hbl.AgentAtDestinationID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()): _intnull; }
                //consignee
                _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboConsigneeID");
                if (_cbo != null) { _hbl.ConsigneeID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }
                //vessel
                _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboVesselID");
                if (_cbo != null) { _hbl.VoyageID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull; }
                
                //origin port
                _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboOriginPort");
                if (_cbo != null)
                {
                    _hbl.OriginPort = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;
                    //ets we can't get it off label or use selecteditem.getvalue because label is updated client side
                    _hbl.Ets = this.dxhfOrder.Contains("ets") ? wwi_func.vdatetime(this.dxhfOrder.Get("ets").ToString()): _dtnull;     
                    //_hbl.Ets = _cbo.Value != null  && _cbo.SelectedItem.GetValue("ETS") != null ? wwi_func.vdatetime(_cbo.SelectedItem.GetValue("ETS").ToString()) : _dtnull;
                }
                //ets
                
                //destination port

                _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboDestinationPort");
                if (_cbo != null) {
                    _hbl.DestinationPort = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;
                    _hbl.Eta = this.dxhfOrder.Contains("eta") ? wwi_func.vdatetime(this.dxhfOrder.Get("eta").ToString()) : _dtnull;    
                    //ets we can't get it off label because they are updated client side
                    //_hbl.Eta = _cbo.Value != null && _cbo.SelectedItem.GetValue("ETA") != null ? wwi_func.vdatetime(_cbo.SelectedItem.GetValue("ETA").ToString()) : _dtnull;
                }
                
                //eta
                //_lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtaSub");
                //if (_lbl != null) { _hbl.Eta = wwi_func.vdatetime(_lbl.Text.ToString()); }
               
                //update
                _hbl.Save();
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
                this.dxpnlErr.ClientVisible = true;
            }
        }
        else
        {
            string _ex = "Can't update record Id = 0";
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// new record
    /// </summary>
    protected int insert_housebl()
    {
        int _newid = 0;

        try
        {
            //new instance of record
            HouseBLTable _hbl = new HouseBLTable();
            //for nullable dates
            DateTime? _dtnull = null;
            //for nullable ints
            int? _intnull = null;

            //get values off editform
            //hbl number
            ASPxTextBox _txt = (ASPxTextBox)this.fmvBol.FindControl("dxtxtHouseBLNumber");
            if (_txt != null) { _hbl.HouseBLNumber = _txt.Text.ToString(); }
            //hbl date
            ASPxDateEdit _dte = (ASPxDateEdit)this.fmvBol.FindControl("dxdtHBLdate");
            //if (_dte != null) { _hbl.HBLDate = _dte.Date != null ? _dte.Date : _dtnull; }
            if (_dte != null && _dte.Value != null) { _hbl.HBLDate = _dte.Date; }
            //agent at destination
            ASPxComboBox _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboAgentAtDestinationID");
            if (_cbo != null) { _hbl.AgentAtDestinationID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
            //consignee
            _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboConsigneeID");
            if (_cbo != null) { _hbl.ConsigneeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
            //vessel
            _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboVesselID");
            if (_cbo != null) { _hbl.VoyageID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
            //origin port
            _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboOriginPort");
            if (_cbo != null)
            {
                _hbl.OriginPort = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;
                //ets we can't get it off label or use selecteditem.getvalue because label is updated client side
                _hbl.Ets = this.dxhfOrder.Contains("ets") ? wwi_func.vdatetime(this.dxhfOrder.Get("ets").ToString()) : _dtnull;     
                //_hbl.Ets = _cbo.Value != null && _cbo.SelectedItem.GetValue("ETS") != null ? wwi_func.vdatetime(_cbo.SelectedItem.GetValue("ETS").ToString()) : _dtnull;
            }
            //ets
            //ASPxLabel _lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtsSub");
            //if (_lbl != null) { _hbl.Ets = wwi_func.vdatetime(_lbl.Text.ToString()); }
            
            //destination port
            _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboDestinationPort");
            if (_cbo != null)
            {
                _hbl.DestinationPort = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull;
                //ets we can't get it off label because they are updated client side
                _hbl.Eta = this.dxhfOrder.Contains("eta") ? wwi_func.vdatetime(this.dxhfOrder.Get("eta").ToString()) : _dtnull;     
                //_hbl.Eta = _cbo.Value != null && _cbo.SelectedItem.GetValue("ETA") != null ? wwi_func.vdatetime(_cbo.SelectedItem.GetValue("ETA").ToString()) : _dtnull;
            }
            
            //eta
            //_lbl = (ASPxLabel)this.fmvBol.FindControl("dxlblEtaSub");
            //if (_lbl != null) { _hbl.Eta = wwi_func.vdatetime(_lbl.Text.ToString()); }

            //insert
            _hbl.Save();
            //get new id
            _newid = (int)_hbl.GetPrimaryKeyValue(); 
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
        
        return _newid;
    }
    #endregion

    #region callbackpanels
    /// <summary>
    /// panel containing the order selection lists
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbpOrderNumbers_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _param = e.Parameter != null ? e.Parameter.ToString() : "";
        int _hblid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

        switch (_param)
        {
            case "Save":
                {
                    save_selected_orders(_hblid);
                    break;
                }
            case "Remove":
                {
                    remove_selected_orders(_hblid);
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    #endregion

    #region listbox ordernos crud events
    /// <summary>
    /// update order table for newly selecgted orders on lstSelected listbox
    /// </summary>
    /// <param name="hblid">int houseblid of current house bl record</param>
    protected void save_selected_orders(int hblid)
    {
        if (this.dxlstSelected.Items.Count > 0 && hblid > 0)
        {
            try
            {
                //get  house bl details
                HouseBLTable _hbl = new HouseBLTable(hblid);  
                
                //IList<string> _ordernos = new List<string>(); 
                OrderTableCollection _col = new OrderTableCollection();
                //step through selected list items and get order numbers
                for (int _ix = 0; _ix < this.dxlstSelected.Items.Count; _ix++)
                {
                    string _no = this.dxlstSelected.Items[_ix].Text.ToString();
                    //_ordernos.Add(_no); 
                    OrderTable _tbl = new OrderTable("OrderNumber",_no);

                    _tbl.Ets = _hbl.Ets; //_h.Ets ;
                    _tbl.Eta = _hbl.Eta; //_h.Eta;
                    _tbl.HouseBLNUmber = _hbl.HouseBLNumber; //_h.HouseBLNumber;
                    _tbl.HouseBLAdded = true; //make sure we do this
                    _tbl.VesselLastUpdated = DateTime.Now; //and this

                    _col.Add(_tbl); 
                }//end loop

                //save 
                _col.SaveAll(); 
   
                 //clear selected list and rebind other lists
                //saved orders list binding using hblnumber
                bind_housebl_orders(_hbl.HouseBLNumber.ToString());
                //available orders list binding
                bind_housebl_orders(wwi_func.vint(_hbl.ConsigneeID.ToString()), wwi_func.vint(_hbl.OriginPort.ToString()), wwi_func.vint(_hbl.DestinationPort.ToString()));
                //clear selected list
                this.dxlstSelected.Items.Clear(); 
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
                this.dxpnlErr.ClientVisible = true;
            }
        }                              
    }

    protected void remove_selected_orders(int hblid)
    {
        if (this.dxlstSaved.Items.Count > 0 && hblid > 0)
        {
            try
            {
                //get  house bl details
                HouseBLTable _hbl = new HouseBLTable(hblid);  
                //DateTime? _nulldt = null; //use for clearing ets, eta if we need to
                
                //IList<string> _ordernos = new List<string>(); 
                OrderTableCollection _col = new OrderTableCollection();
                //step through selected list items and get order numbers
                for (int _ix = 0; _ix < this.dxlstSaved.Items.Count; _ix++)
                {
                    if (this.dxlstSaved.Items[_ix].Selected == true)
                    {
                        string _no = this.dxlstSaved.Items[_ix].Text.ToString();
                        //_ordernos.Add(_no); 
                        OrderTable _tbl = new OrderTable("OrderNumber", _no);

                        _tbl.HouseBLNUmber = null; //_h.HouseBLNumber;
                        _tbl.HouseBLAdded = false;
                        //_tbl.Ets = _nulldt;  //should we clear this? ms access db doesn't bother
                        //_tbl.Eta = _nulldt;  //should we clear this? ms access db doesn't bother
                        //_tbl.VesselLastUpdated = DateTime.Now; //should we clear this? ms access db doesn't bother

                        _col.Add(_tbl);
                    }//end if
                }//end loop

                //save 
                _col.SaveAll();

                //clear selected list and rebind other lists
                //saved orders list binding using hblnumber
                bind_housebl_orders(_hbl.HouseBLNumber.ToString());
                //available orders list binding
                bind_housebl_orders(wwi_func.vint(_hbl.ConsigneeID.ToString()), wwi_func.vint(_hbl.OriginPort.ToString()), wwi_func.vint(_hbl.DestinationPort.ToString()));
                //clear selected list
                this.dxlstSelected.Items.Clear();
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
                this.dxpnlErr.ClientVisible = true;
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
    protected void dxcboVesselID_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
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
    protected void dxcboVesselID_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VoyageID", "Joined", "ETS", "ETA", "DestinationPortID", "OriginPortID" };
        string[] _sort = { "Joined" };
        
        //additional filters on this dll
        string _originportid = ""; //get_token("pstart").ToString(); 
        string _destportid = "";//get_token("pend").ToString(); 

        ASPxComboBox _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboOriginPort");
        if(_cbo != null && _cbo.SelectedItem != null){ _originportid = _cbo.SelectedItem.Value.ToString(); }
        
        _cbo = (ASPxComboBox)this.fmvBol.FindControl("dxcboDestinationPort");
        if(_cbo != null && _cbo.SelectedItem != null){ _destportid = _cbo.SelectedItem.Value.ToString(); }
       
        //
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("Page2VesselView").Paged(e.BeginIndex + 1, e.EndIndex + 1, "VoyageID").Where("Joined").Like(string.Format("{0}%", e.Filter.ToString()));
            
        if(_originportid != "" && _destportid != ""){
              _query.And("DestinationPortID").IsEqualTo(_destportid).And("OriginPortID").IsEqualTo(_originportid).OrderAsc(_sort);
        }

        string test = _query.ToString();

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VoyageID";
        _combo.TextField = "Joined";
        _combo.DataBindItems();
    }
    //end incremental filtering of voyage name

    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// for devan warehouse the company list is limted to company typeid = 5
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer"  };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("view_delivery_address").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("{0}%", e.Filter.ToString()));

        switch (_combo.ID)
        {
            case "dxcboAgentAtDestinationID":
                {
                    int[] _vals = { 3, 6 };
                    _query.And("TypeID").In(_vals);  
                    break;
                }
            case "dxcboConsigneeID":
                {
                    _query.And("Consignee").IsEqualTo(1);  
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
    protected void dxcbocompany_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
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
    /// origin port combo filtered by VoyageID
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_origin_port(int voyageId)
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvBol.FindControl("dxcboOriginPort");
        if (_combo != null)
        {
            IDataReader _rd = null;

            if (voyageId > 0)
            {
                //string[] _cols = { "VoyageETSSubTable.OriginPortID", "PortTable.PortName", "VoyageETSSubTable.ETS" };
                string[] _cols = { "PortTable.PortID", "PortTable.PortName", "VoyageETSSubTable.ETS" };
                string[] _order = { "PortName" };


                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.VoyageTable).
                    InnerJoin(DAL.Logistics.VoyageETSSubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn).
                    InnerJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETSSubTable.OriginPortIDColumn);

            
                _qry.Where(DAL.Logistics.VoyageTable.VoyageIDColumn).IsEqualTo(voyageId);

                _rd = _qry.ExecuteReader();
            }

            //rebind origin ports
            _combo.DataSource = _rd;
            _combo.ValueField = "PortID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PortName";
            _combo.DataBindItems();
        }
    }

    /// <summary>
    /// destination port combo filtered by VoyageID
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_destination_port(int voyageId)
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvBol.FindControl("dxcboDestinationPort");
        if (_combo != null)
        {
            IDataReader _rd = null;

            if (voyageId > 0)
            {
                string[] _cols = { "PortTable.PortID", "PortTable.PortName", "VoyageETASubTable.ETA" };
                string[] _order = { "PortName" };

                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.VoyageTable).
                    InnerJoin(DAL.Logistics.VoyageETASubTable.VoyageIDColumn, DAL.Logistics.VoyageTable.VoyageIDColumn).
                    InnerJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETASubTable.DestinationPortIDColumn);


                _qry.Where(DAL.Logistics.VoyageTable.VoyageIDColumn).IsEqualTo(voyageId);
                _rd = _qry.ExecuteReader();
            }
            //rebind dest ports
            _combo.DataSource = _rd;
            _combo.ValueField = "PortID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PortName";
            _combo.DataBindItems();
        }
    }
    #endregion

    #region listbox binding
    /// <summary>
    /// overloaded sub for list boxes
    /// saved orders: HouseBLNUmber = houseblnumber
    /// available orders: JobClosed = 0, HouseBLAdded = 0, ConsigneeID = consigneeid, OriginPort = originport, DestinationPort = destinationport
    /// </summary>
    protected void bind_housebl_orders(string houseblnumber)
    {
        //ASPxListBox _list = (ASPxListBox)this.FindControl("dxlstSaved");
        //if (_list != null)
        //{
            string[] _cols = { "OrderTable.OrderID", "OrderTable.OrderNumber", "OrderTable.HouseBLNUmber", "OrderTable.HouseBLAdded" };
            string[] _order = { "OrderID" };

            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.OrderTable).Where(DAL.Logistics.OrderTable.HouseBLNUmberColumn)
                .IsEqualTo(houseblnumber).OrderAsc(_order); 

            //bind list
            IDataReader _rd = _qry.ExecuteReader();
            this.dxlstSaved.DataSource = _rd;
            this.dxlstSaved.ValueField = "OrderID";
            this.dxlstSaved.ValueType = typeof(int);
            this.dxlstSaved.TextField = "OrderNumber";
            this.dxlstSaved.DataBindItems();
        //}
    }
    /// <param name="consigneeId">int consignee's id</param>
    /// <param name="originPort">int port id</param>
    /// <param name="destinationPort">int port id</param>
    protected void bind_housebl_orders(int consigneeid, int originport, int destinationport)
    {
        //ASPxListBox _list = (ASPxListBox)this.FindControl("dxlstAvailable");
        //if (_list != null)
        //{
            string[] _cols = { "OrderTable.OrderID", "OrderTable.OrderNumber", "OrderTable.HouseBLNUmber", "OrderTable.HouseBLAdded", 
                             "OrderTable.ActualWeight", "OrderTable.ActualVolume"};
            string[] _order = { "OrderID" };

            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.OrderTable).
                Where(DAL.Logistics.OrderTable.JobClosedColumn).IsEqualTo(false)
                .And(DAL.Logistics.OrderTable.HouseBLAddedColumn).IsEqualTo(false)
                .And(DAL.Logistics.OrderTable.ConsigneeIDColumn).IsEqualTo(consigneeid)
                .And(DAL.Logistics.OrderTable.PortIDColumn).IsEqualTo(originport)
                .And(DAL.Logistics.OrderTable.DestinationPortIDColumn).IsEqualTo(destinationport)
                .OrderAsc(_order);

            //bind list
            IDataReader _rd = _qry.ExecuteReader();
            this.dxlstAvailable.DataSource = _rd;
            this.dxlstAvailable.ValueField = "OrderID";
            this.dxlstAvailable.ValueType = typeof(int);
            this.dxlstAvailable.TextField = "OrderNumber";
            this.dxlstAvailable.DataBindItems();
        //}
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
