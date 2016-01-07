using System;
using System.IO;
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
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;

public partial class order : System.Web.UI.Page
{
    //static string _yapfile = Path.Combine(
    //                         Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    //                         "{0}_order.yap");  

    protected void Page_Load(object sender, EventArgs e)
    {
               
        if (isLoggedIn())
        {              
            if (!Page.IsPostBack)
            {
                //set formview mode to template we are using or controls will not be rendered
                string _mode = get_token("mode");
                bind_commands(_mode); //crud commands  
                set_mode(_mode);
                bind_tabs(_mode); //navigation tabs
                
                //****
                //replacing objectdatasource bind in code
                //use order id 303635 for testing purposes
                bind_formview(_mode);
                //****
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_orders/order_search", "publiship"));
        }
    }

    #region form binding
    /// <summary>
    /// replacing objectdatasource
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
            this.formOrder.DataSource = _o;  //303635; //183689; 303635 is the orderid for test record order number 999909  
            this.formOrder.DataKeyNames = _key;
            this.formOrder.DataBind();
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order Number {0}. Error: {1}", _orderno , _ex);
            this.dxpnlErr.ClientVisible = true;
        }
    }

    protected void formOrder_DataBound(object sender, EventArgs e)
    {
        try
        {
            OrderTable _row = (OrderTable)this.formOrder.DataItem;
            //icons
            //publiship order
            this.dximgJobPubliship.ClientVisible = _row != null? _row.PublishipOrder: false;
            this.dxlblJobPubliship.ClientVisible = _row != null ? _row.PublishipOrder : false;
            //job closed
            this.dximgJobClosed.ClientVisible = _row != null ? _row.JobClosed : false;
            this.dxlblJobClosed.ClientVisible = _row != null ? _row.JobClosed : false;
            //hot job
            this.dximgJobHot.ClientVisible = _row != null ? _row.HotJob : false;
            this.dxlblJobHot.ClientVisible = _row != null ? _row.HotJob : false;

            string _office = "";
            int _officeid = 0;
            if (this.formOrder.CurrentMode == FormViewMode.Insert)
            {
                //order number and office in header
                this.dxlblOrderNo.Text = "[New Order]";
                _officeid = Page.Session["user"] != null ? (int)((UserClass)Page.Session["user"]).OfficeId : 0;
                _office = wwi_func.lookup_xml_string("xml\\office_names.xml", "value", _officeid.ToString(), "name");
            }
            else
            {
                this.dxlblOrderNo.Text = _row.OrderNumber.ToString();
                _office = _row != null ? _row.OfficeIndicator : "";
                _officeid = wwi_func.vint(wwi_func.lookup_xml_string("xml\\office_names.xml", "name", _office, "value"));

            }
            this.dxlbOrderDetails1.Text = "|" + _office;
            //just pass it as a param
            //this.dxhfOfficeID.Clear();
            //this.dxhfOfficeID.Add("officeid", _officeid.ToString());  

            //16/10/13 view returned to this page and single summary query used to populate look-up values
            //view moved to seperate page (Order_View.aspx) so we can avoid all these calls to the database!
            if (this.formOrder.CurrentMode == FormViewMode.Edit || this.formOrder.CurrentMode == FormViewMode.Insert)
            {
                //have to bind the standard ddls here or they don't populate for new orders
                bind_company_combos(_officeid);
                bind_origin_combos(-1);
                bind_dest_combos();

                sub_decks();
            }
            else //readonly
            {
                string _test ="";

                SubSonic.SqlQuery _q = new SubSonic.SqlQuery();
                _q = DB.Select().From("view_order_summary").Where("OrderNumber").IsEqualTo(_row.OrderNumber);
                DataTable _dt = _q.ExecuteDataSet().Tables[0];

                //labels
                ASPxLabel _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewController");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["OrderController"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewContact");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["ContactName"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOps");
                if (_lbl != null) { _lbl.Text = _lbl.Text = _dt.Rows[0]["OpsController"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblContactEmail");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["EMail"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewCompany");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["CompanyName"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewPrinter");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["PrinterName"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewCountry");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["OriginCountry"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOrigin");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["PlaceName"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOriginPort");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["OriginPort"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewAgentAtOrigin");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["OriginAgent"].ToString(); 
                    _test = _dt.Rows[0]["OriginAgent"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewDestPort");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["DestinationPort"].ToString(); }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewFinal");
                if (_lbl != null) { _lbl.Text = _dt.Rows[0]["FinalDestination"].ToString(); }

                //260211 some older jobs have an origin controller but no origin agent in those cases don't display the origin controller
                if (!string.IsNullOrEmpty(_test))
                {
                    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOriginController");
                    if (_lbl != null) { _lbl.Text = _dt.Rows[0]["OriginPortController"].ToString(); }
                }

                //date formatting
                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblDateCreated");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewExWorks");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewBookingReceived");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewCargoReady");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewDueWarehouse");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewDocsApprovedDate");
                if (_lbl != null) { _lbl.Text = _lbl.Text != "" ? wwi_func.vdatetime(_lbl.Text).ToShortDateString() : ""; }
                                
                //addresses
                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblCompanyAddress");
                if (_lbl != null)
                {
                    _lbl.Text = _dt.Rows[0]["Address1"].ToString() + Environment.NewLine + _dt.Rows[0]["Address2"].ToString() +
                        Environment.NewLine + _dt.Rows[0]["Address3"].ToString() + Environment.NewLine + _dt.Rows[0]["CountryName"].ToString() +
                        Environment.NewLine + _dt.Rows[0]["TelNo"].ToString();
                }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblPrinterAddress");
                if (_lbl != null)
                {
                    _lbl.Text = _dt.Rows[0]["PrinterAdd1"].ToString() + Environment.NewLine + _dt.Rows[0]["PrinterAdd2"].ToString() +
                        Environment.NewLine + _dt.Rows[0]["PrinterAdd3"].ToString() + Environment.NewLine + _dt.Rows[0]["PrinterCountry"].ToString() +
                        Environment.NewLine + _dt.Rows[0]["PrinterTel"].ToString();
                }

                _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblOriginAgentAddress");
                if (_lbl != null)
                {
                    _lbl.Text = _dt.Rows[0]["OriginAgentAddress1"].ToString() + Environment.NewLine + _dt.Rows[0]["OriginAgentAddress2"].ToString() +
                       Environment.NewLine + _dt.Rows[0]["OriginAgentAddress3"].ToString() + Environment.NewLine + _dt.Rows[0]["OriginAgentCountry"].ToString() +
                       Environment.NewLine + _dt.Rows[0]["OriginAgentTel"].ToString();
                }

                //deprecated look-up code
                //if (this.formOrder.CurrentMode == FormViewMode.ReadOnly)
                //{
                //    //text values for view template
                //    ASPxLabel _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewController");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _row.OrderControllerID); }
                //   _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewContact");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("ContactName", "ContactTable", "ContactID", _row.ContactID); }

                //   _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOps");
                //   if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _row.OperationsControllerID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewCompany");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("CompanyName", "view_delivery_address", "CompanyID", _row.CompanyID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewPrinter");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("PrinterName", "PrinterView", "CompanyID", _row.PrinterID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewCountry");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("CountryName", "CountryTable", "CountryID", _row.CountryID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOrigin");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("PlaceName", "PlacesTable", "PlaceID", _row.OriginPointID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOriginPort");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("PortName", "PortTable", "PortID", _row.PortID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewAgentAtOrigin");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("OriginAgent", "OriginAgentView", "OriginAgentID", _row.AgentAtOriginID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewDestPort");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("PortName", "PortTable", "PortID", _row.DestinationPortID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewFinal");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("PlaceName", "PlacesTable", "PlaceID", _row.FinalDestinationID); }

                //   _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblViewOriginController");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _row.OriginPortControllerID); }

                //    //address labels
                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblCompanyAddress");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_multi_values("Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", _row.CompanyID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblContactEmail");
                //   if (_lbl != null) { _lbl.Text = wwi_func.lookup_value("EMail", "ContactTable", "ContactID", _row.ContactID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblPrinterAddress");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_multi_values("PrinterAdd1,PrinterAdd2,PrinterAdd3,PrinterCountry,PrinterTel", "PrinterView", "CompanyID", _row.PrinterID); }

                //    _lbl = (ASPxLabel)this.formOrder.FindControl("dxlblOriginAgentAddress");
                //    if (_lbl != null) { _lbl.Text = wwi_func.lookup_multi_values("OriginAgentAddress1,OriginAgentAddress2,OriginAgentAddress3,OriginAgentCountry", "OriginAgentView", "OriginAgentID", _row.AgentAtOriginID); }
                //}
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.Visible = true;
        }
    }
    /// <summary>
    /// tab menu 
    /// </summary>
    protected void bind_tabs(string mode)
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\form_menus.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        //080814 for Insert mode just display Order detials tab (Titles must be added to a new order)  
        _xml.XPath = string.Format("//menuitem[@AppliesTo='{0}Order']", mode == "Insert"? "Insert": ""); //you need this or tab will not databind!
        //_xml.XPath = "//menuitem[@AppliesTo='Order']"; //you need this or tab will not databind!
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
            if (this.dxtabOrder.Tabs[_ix].Text == this.dxlbOrderDetails1.Text) { this.dxtabOrder.ActiveTabIndex = _ix; }
            //format urls with order no
            if (!string.IsNullOrEmpty(this.dxtabOrder.Tabs[_ix].NavigateUrl)) { this.dxtabOrder.Tabs[_ix].NavigateUrl = string.Format(this.dxtabOrder.Tabs[_ix].NavigateUrl, _orderid, _orderno); }
            //080814 no need to this as we restrict visible tabs when bind_tabs is called
            //if new record hide tabs until main order details have been saved
            //if (this.formOrder.CurrentMode == FormViewMode.Insert && _ix > 0) { this.dxtabOrder.Tabs[_ix].ClientVisible = false; } 
        }
    }

    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_commands(string mode)
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
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pid");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        //}
    }
    #endregion

    #region sub deck text and flags
    /// <summary>
    /// sub decks e.g. full address for company added to form
    /// try and derive this info from multicolumn combos rather than making a call to the database
    /// </summary>
    protected void sub_decks()
    {
        //company address
        string _s = "";

        //only for edit/insert modes
        if (this.formOrder.CurrentMode != FormViewMode.ReadOnly)
        {
            try
            {
                _s = "";
                ASPxComboBox _cbCompany = (ASPxComboBox)this.formOrder.FindControl("dxcboCompany");
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
                ASPxLabel _lblCompany = (ASPxLabel)this.formOrder.FindControl("dxlblCompanyAddress");
                if (_lblCompany != null) { _lblCompany.Text = _s; }

                //client contact email
                _s = "";
                ASPxComboBox _cbContact = (ASPxComboBox)this.formOrder.FindControl("dxcboClientContact");
                if (_cbContact != null && _cbContact.SelectedItem != null && _cbContact.Value != null)
                {
                    if (_cbContact.SelectedItem.GetValue("Email") != null)
                    {
                        _s = (string)_cbContact.SelectedItem.GetValue("Email").ToString();
                    }
                }
                ASPxLabel _lblContact = (ASPxLabel)this.formOrder.FindControl("dxlblContactEmail");
                if (_lblContact != null) { _lblContact.Text = _s; }

                //printer address
                _s = "";
                ASPxComboBox _cbPrinter = (ASPxComboBox)this.formOrder.FindControl("dxcboPrinter");
                if (_cbPrinter != null && _cbPrinter.SelectedItem != null && _cbPrinter.Value != null)
                {
                    if (_cbPrinter.SelectedItem.GetValue("PrinterAdd1") != null) { _s = _s + (string)_cbPrinter.SelectedItem.GetValue("PrinterAdd1").ToString(); } //(string)_cbCompany.SelectedItem.Value.ToString();
                    _s += Environment.NewLine;
                    if (_cbPrinter.SelectedItem.GetValue("PrinterAdd2") != null) { _s = _s + (string)_cbPrinter.SelectedItem.GetValue("PrinterAdd2").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbPrinter.SelectedItem.GetValue("PrinterAdd3") != null) { _s = _s + (string)_cbPrinter.SelectedItem.GetValue("PrinterAdd3").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbPrinter.SelectedItem.GetValue("PrinterCountry") != null) { _s = _s + (string)_cbPrinter.SelectedItem.GetValue("PrinterCountry").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbPrinter.SelectedItem.GetValue("PrinterTel") != null) { _s = _s + (string)_cbPrinter.SelectedItem.GetValue("PrinterTel").ToString(); }
                    _s += Environment.NewLine;

                }
                ASPxLabel _lblPrinter = (ASPxLabel)this.formOrder.FindControl("dxlblPrinterAddress");
                if (_lblPrinter != null) { _lblPrinter.Text = _s; }

                //agent at origin
                _s = "";
                ASPxComboBox _cbOriginAgent = (ASPxComboBox)this.formOrder.FindControl("dxcboAgentAtOrigin");
                if (_cbOriginAgent != null && _cbOriginAgent.SelectedItem != null && _cbOriginAgent.Value != null)
                {
                    if (_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress1") != null) { _s = _s + (string)_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress1").ToString(); } //(string)_cbCompany.SelectedItem.Value.ToString();
                    _s += Environment.NewLine;
                    if (_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress2") != null) { _s = _s + (string)_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress2").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress3") != null) { _s = _s + (string)_cbOriginAgent.SelectedItem.GetValue("OriginAgentAddress3").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbOriginAgent.SelectedItem.GetValue("OriginAgentCountry") != null) { _s = _s + (string)_cbOriginAgent.SelectedItem.GetValue("OriginAgentCountry").ToString(); }
                    _s += Environment.NewLine;
                    if (_cbOriginAgent.SelectedItem.GetValue("OriginAgentTel") != null) { _s = _s + (string)_cbOriginAgent.SelectedItem.GetValue("OriginAgentTel").ToString(); }
                    _s += Environment.NewLine;
                }
                ASPxLabel _lblOriginAgent = (ASPxLabel)this.formOrder.FindControl("dxlblOriginAgentAddress");
                if (_lblOriginAgent != null) { _lblOriginAgent.Text = _s; }

            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
            }
        }
    }
    #endregion
    
    #region callback events

    /// <summary>
    /// client contact callback fires when company id is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboContact_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_client_contact(e.Parameter);
    }

    /// <summary>
    /// fires when origin agent is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOriginController_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin_controller(e.Parameter);
    }

    /// <summary>
    /// callback for origin group
    /// we have to use a callback panel because we need to be able to reset the cascading combos 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkOriginGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //int _group = wwi_func.get_company_group();
        
        int _ix = wwi_func.vint(e.Parameter.ToString());
        //bind_origin_combos(_ix);  no point passing parameter as we have to rebind all 3 combos anyway because they are multicolumn and do not cache
        bind_origin_combos(-1);  
    }
    //end callback group 
    /// <summary>
    /// callbacks for origin and origin port as we aree not using a callback panel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOrigin_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin_combos(1);
    }


    protected void dxcboOriginPort_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin_combos(2);
    }
#endregion
 
    #region combobox binding
    protected void bind_company_combos(int officeid)
    {
        //order controller & operations controller
        bind_employee_names(officeid);

        //client contact name
        ASPxComboBox _dxcboCompany = (ASPxComboBox)this.formOrder.FindControl("dxcboCompany");
        if (_dxcboCompany != null)
        {
            bind_client_contact(_dxcboCompany.Value != null ? _dxcboCompany.Value.ToString() : "");
        }

        ASPxComboBox _cboAgentAtOrigin = (ASPxComboBox)this.formOrder.FindControl("dxcboAgentAtOrigin");
        if (_cboAgentAtOrigin != null)
        {
            bind_origin_controller(_cboAgentAtOrigin.Value != null ?_cboAgentAtOrigin.Value.ToString(): ""); 
        }
    }

    
    /// <summary>
    /// origin combos
    /// if select combo == -1 bind all combs otherwisde bind combo identified by selectcombo
    /// </summary>
    /// <param name="selectcombo">int</param>
    protected void bind_origin_combos(int selectcombo)
    {
        //070113 we are not using a callback panel just combo box built in callback
        //DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel _call = (DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel)this.formOrder.FindControl("dxcbkOriginGroup");
        //if (_call != null)
        //{
            if (selectcombo == -1 || selectcombo == 0)
            {
                //country id
                bind_countries();
            }

            if (selectcombo == -1 || selectcombo == 1)
            {
                //origin point id
                //ASPxComboBox _dxcboCountry = (ASPxComboBox)_call.FindControl("dxcboCountry");
                ASPxComboBox _dxcboCountry = (ASPxComboBox)this.formOrder.FindControl("dxcboCountry");
                if (_dxcboCountry != null)
                {
                    bind_origin(_dxcboCountry.Value != null ? _dxcboCountry.Value.ToString() : "");
                }
            }

            if (selectcombo == -1 || selectcombo == 2)
            {
                //060512 origin port is not filtered in Access database?
                bind_origin_port("");

                //origin port
                //ASPxComboBox _dxcboOrigin = (ASPxComboBox)_call.FindControl("dxcboOrigin");
                //ASPxComboBox _dxcboOrigin = (ASPxComboBox)this.formOrder.FindControl("dxcboOrigin");
                //if (_dxcboOrigin != null)
                //{
                //    bind_origin_port(_dxcboOrigin.Value != null ? _dxcboOrigin.Value.ToString() : "");
                //}
            }
        //}
    }
    protected void bind_dest_combos()
    {
        //destination port
        bind_dest_port();

        //final destination not filtered in access database?
        bind_final_dest();
    }
    //end bind combos

    protected void bind_employee_names(int officeid)
    {
        string[] _cols = { "EmployeeID, Name" };
        string[] _order = { "Name" };
        
        
        //order controller employee must be live and in office where order is created
        //int _officeid = this.dxhfOfficeID.Contains("officeid") ? wwi_func.vint(this.dxhfOfficeID.Get("officeid").ToString()) : 0;
        SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("OfficeID").IsEqualTo(officeid).And("Live").IsEqualTo(true).OrderAsc(_order);
        ASPxComboBox _dxcboController = (ASPxComboBox)this.formOrder.FindControl("dxcboController");
        if (_dxcboController != null)
        {
            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboController.DataSource = _rd1;
            _dxcboController.ValueField = "EmployeeID";
            _dxcboController.ValueType = typeof(int); 
            _dxcboController.TextField = "Name";
            _dxcboController.DataBindItems();
        }

        _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);
        //operations controller
        ASPxComboBox _dxcboOps = (ASPxComboBox)this.formOrder.FindControl("dxcboOps");
        if (_dxcboOps != null)
        {
            IDataReader _rd2 = _qry.ExecuteReader();
            _dxcboOps.DataSource = _rd2;
            _dxcboOps.ValueField = "EmployeeID";
            _dxcboOps.ValueType = typeof(int);
            _dxcboOps.TextField = "Name";
            _dxcboOps.DataBindItems();
        }

    }
    //end bind employees
    protected void bind_countries()
    {
        //DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel _call = (DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel)this.formOrder.FindControl("dxcbkOriginGroup");
        //if (_call != null)
        //{
            //ASPxComboBox _dxcboCountry = (ASPxComboBox)_call.FindControl("dxcboCountry");
        ASPxComboBox _dxcboCountry = (ASPxComboBox)this.formOrder.FindControl("dxcboCountry");
            if (_dxcboCountry != null)
            {
                string[] _cols = { "CountryID, CountryName" };
                string[] _order = { "CountryName" };
                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.CountryTable).OrderAsc(_order);
                IDataReader _rd1 = _qry.ExecuteReader();
                _dxcboCountry.DataSource = _rd1;
                _dxcboCountry.ValueField = "CountryID";
                _dxcboCountry.ValueType = typeof(int); 
                _dxcboCountry.TextField = "CountryName";
                _dxcboCountry.DataBindItems();
            }
        //}
    }
    //end bind country
    protected void bind_origin(string countryID)
    {
        //DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel _call = (DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel)this.formOrder.FindControl("dxcbkOriginGroup");
        //if (_call != null)
        //{
            //ASPxComboBox _dxcboOrigin = (ASPxComboBox)_call.FindControl("dxcboOrigin");
        ASPxComboBox _dxcboOrigin = (ASPxComboBox)this.formOrder.FindControl("dxcboOrigin");
            if (_dxcboOrigin != null)
            {
                string[] _cols = { "PlaceID, PlaceName" };
                string[] _order = { "PlaceName" };
                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PlacesTable).OrderAsc(_order);

                int _filter = -1;
                if (!string.IsNullOrEmpty(countryID))
                {
                    _filter = wwi_func.vint(countryID);
                    if (_filter > 0) { _qry.Where("CountryID").IsEqualTo(_filter); }
                }

                IDataReader _rd1 = _qry.ExecuteReader();
                _dxcboOrigin.DataSource = _rd1;
                _dxcboOrigin.ValueField = "PlaceID";
                _dxcboOrigin.ValueType = typeof(int); 
                _dxcboOrigin.TextField = "PlaceName";
                _dxcboOrigin.DataBindItems();
            }
        //}
    }
    //end bind origin
    protected void bind_origin_port(string originID)
    {
        //DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel _call = (DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel)this.formOrder.FindControl("dxcbkOriginGroup");
        //if (_call != null)
        //{
            //ASPxComboBox _dxcboOriginPort = (ASPxComboBox)_call.FindControl("dxcboOriginPort");
        ASPxComboBox _dxcboOriginPort = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginPort");

            if (_dxcboOriginPort != null)
            {
                string[] _cols = { "PortID, PortName" };
                string[] _order = { "PortName" };
                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PortTable).OrderAsc(_order);

                int _filter = -1;
                if (!string.IsNullOrEmpty(originID))
                {
                     _filter = wwi_func.vint(originID);
                     if (_filter > 0) { _qry.Where("PortID").IsEqualTo(_filter); }
                }

                IDataReader _rd1 = _qry.ExecuteReader();
                _dxcboOriginPort.DataSource = _rd1;
                _dxcboOriginPort.ValueField = "PortID";
                _dxcboOriginPort.ValueType = typeof(int); 
                _dxcboOriginPort.TextField = "PortName";
                _dxcboOriginPort.DataBindItems();
            }
        //}
    }
    //end bind origin port
    protected void bind_dest_port()
    {
        ASPxComboBox _dxcboDestPort = (ASPxComboBox)this.formOrder.FindControl("dxcboDestPort");
        if (_dxcboDestPort != null)
        {
            string[] _cols = { "PortID, PortName" };
            string[] _order = { "PortName" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PortTable).OrderAsc(_order);
            
            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboDestPort.DataSource = _rd1;
            _dxcboDestPort.ValueField = "PortID";
            _dxcboDestPort.ValueType = typeof(int); 
            _dxcboDestPort.TextField = "PortName";
            _dxcboDestPort.DataBindItems();
        }
    }
    //end bind destination port
    protected void bind_final_dest()
    {
        ASPxComboBox _dxcboFinal = (ASPxComboBox)this.formOrder.FindControl("dxcboFinal");
        if (_dxcboFinal != null)
        {
            string[] _cols = { "PlaceID, PlaceName", "CountryID" };
            string[] _order = { "PlaceName" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PlacesTable).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboFinal.DataSource = _rd1;
            _dxcboFinal.ValueField = "PlaceID";
            _dxcboFinal.ValueType = typeof(int); 
            _dxcboFinal.TextField = "PlaceName";
            _dxcboFinal.DataBindItems();
        }
    }

    protected void bind_client_contact(string companyID)
    {
        //must have a filter or display nothing
        if (!string.IsNullOrEmpty(companyID))
        {
            ASPxComboBox _dxcboContact = (ASPxComboBox)this.formOrder.FindControl("dxcboClientContact");
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
                _dxcboContact.ValueType = typeof(int);
                _dxcboContact.TextField = "ContactName";
                _dxcboContact.DataBindItems();
            }
        }
    }
    //end bind client contact
    protected void bind_origin_controller(string originAgentID)
    {
        //260211 some older jobs have an origin controller but no origin agent in those cases don't display the origin controller
        //must have a filter or display nothing
        if (!string.IsNullOrEmpty(originAgentID))
        {
            ASPxComboBox _dxcboController = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginController");
            if (_dxcboController != null)
            {

                string[] _cols = { "EmployeesTable.EmployeeID, EmployeesTable.Name", "EmployeesTable.DepartmentID", "OfficeTable.OfficeID", " NameAndAddressBook.CompanyID" };
                string[] _order = { "Name" };
                SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
                    InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn).
                    InnerJoin(DAL.Logistics.NameAndAddressBook.CountryIDColumn, DAL.Logistics.OfficeTable.CountryIDColumn);   
                    
                if (!string.IsNullOrEmpty(originAgentID))
                {
                    int _filter = wwi_func.vint(originAgentID);
                    if (_filter > 0) { _qry.Where("CompanyID").IsEqualTo(_filter); }
                }

                _qry.And(DAL.Logistics.EmployeesTable.LiveColumn).IsEqualTo(true).OrderAsc(_order);
                
                IDataReader _rd1 = _qry.ExecuteReader();
                _dxcboController.DataSource = _rd1;
                _dxcboController.ValueField = "EmployeeID";
                _dxcboController.ValueType = typeof(int); 
                _dxcboController.TextField = "Name";
                _dxcboController.DataBindItems();
            }
        }
    }
    //end bind origin port controller
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
        string[] _cols = { "CompanyID", "CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer" };

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
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
        string[] _cols = { "CompanyID", "CompanyName", "Address1", "Address2", "Address3", "CountryName", "TelNo", "Customer" };

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

    //printer
    protected void dxcboprinter_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID", "PrinterName", "PrinterAdd1", "PrinterAdd2", "PrinterAdd3", "PrinterCountry", "PrinterTel" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("PrinterNameView").Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("PrinterName").Like(string.Format("{0}%", e.Filter.ToString()));
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int); 
        _combo.TextField = "PrinterName";
        _combo.DataBindItems();
        //    }
        //}
    }
    protected void dxcboprinter_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
        string[] _cols = { "CompanyID", "PrinterName", "PrinterAdd1", "PrinterAdd2", "PrinterAdd3", "PrinterCountry", "PrinterTel" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("PrinterNameView").WhereExpression("CompanyID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.ValueType = typeof(int); 
        _combo.TextField = "PrinterName";
        _combo.DataBindItems();
        //  }
        //}
    }
    //end incremental filtering of agentatorigin
    //printer
    protected void dxcboagentatorigin_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "OriginAgentID", "OriginAgent", "OriginAgentAddress1", "OriginAgentAddress2", "OriginAgentAddress3", "OriginAgentCountry", "OriginAgentTel" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("OriginAgentView").Paged(e.BeginIndex + 1, e.EndIndex + 1, "OriginAgentID").WhereExpression("OriginAgent").Like(string.Format("{0}%", e.Filter.ToString()));
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "OriginAgentID";
        _combo.ValueType = typeof(int); 
        _combo.TextField = "OriginAgent";
        _combo.DataBindItems();
        //    }
        //}
    }
    //agent at origin
    protected void dxcboagentatorigin_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
        string[] _cols = { "OriginAgentID", "OriginAgent", "OriginAgentAddress1", "OriginAgentAddress2", "OriginAgentAddress3", "OriginAgentCountry", "OriginAgentTel" };
 
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From("OriginAgentView").WhereExpression("OriginAgentID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "OriginAgentID";
        _combo.ValueType = typeof(int); 
        _combo.TextField = "OriginAgent";
        _combo.DataBindItems();
        //  }
        //}
    }
    //end incremental filtering agentarorigin

#endregion
    
    #region form and crud events 

    /// <summary>
    /// fires when menu item clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void formOrder_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.formOrder.ChangeMode(e.NewMode);  
    }
    //end mode changing

    /// <summary>
    /// DEPRECATED we are binding in code behind as objectdatasources are too flaky 
    /// on selecting filter by order ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void objdsOrder_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //use order id 303635 for testing purposes
        Int32  _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
        e.InputParameters["OrderID"] = _orderid;  //303635; //183689; 303635 is the orderid for test record order number 999909  
        this.dxlblOrderNo.Text = _orderid.ToString(); 
        //
    }
    /// DEPRECATED we are binding in code behind as objectdatasources are too flaky 
    //end selecting
    protected void formOrder_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            this.dxlblErr.Text = e.Exception.Message.ToString();
            this.dxpnlErr.Visible = true;
        }
        else
        {
            //have to do this or formview will automatically swtich to read-only mode after update
            //e.KeepInEditMode = true;
            Response.Redirect("~/Order_View.aspx?pid=" + get_token("pid"), true);
        }
        
    }
    /// DEPRECATED we are binding in code behind as objectdatasources are too flaky 
    protected void formOrder_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            this.dxlblErr.Text = e.Exception.Message.ToString();
            this.dxpnlErr.Visible = true;
        }
        else
        {
            Response.Redirect("~/Order_View.aspx?pid=" + get_token("pid"), true);
        }
        //have to do this or formview will automatically swtich to read-only mode after insert
        //e.KeepInInsertMode = true;
    }

    /// <summary>
    /// update order table
    /// 140114 replacing objectdatasource with code behind
    /// </summary>
    protected void update_order()
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
                ASPxComboBox _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboController");
                if (_cb != null) { _t.OrderControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOps");
                if (_cb != null) { _t.OperationsControllerID =_cb.Value != null ? wwi_func.vint(_cb.Value.ToString()): _intnull; }
  
                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCompany");
                if (_cb != null) { _t.CompanyID =  _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCountry");
                if (_cb != null) { _t.CountryID = _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOrigin");
                if (_cb != null) { _t.OriginPointID =  _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginPort");
                if (_cb != null) { _t.PortID = _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboDestPort");
                if (_cb != null) { _t.DestinationPortID =  _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboFinal");
                if (_cb != null) { _t.FinalDestinationID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboClientContact");
                if (_cb != null) { _t.ContactID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()): _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboPrinter");
                if (_cb != null) { _t.PrinterID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()): _intnull; }
                
                _cb= (ASPxComboBox)this.formOrder.FindControl("dxcboAgentAtOrigin");
                if (_cb != null) { _t.AgentAtOriginID = _cb.Value != null ?wwi_func.vint(_cb.Value.ToString()): _intnull; }
               
                _cb= (ASPxComboBox)this.formOrder.FindControl("dxcboOriginController");
                if (_cb != null) { _t.OriginPortControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }
               
                //dates
                DateTime? _dtnull = null; //default for nullable datetimes
                ASPxDateEdit _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtExWorks");
                if (_dt != null) { _t.ExWorksDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()): _dtnull; } 
    
                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtCargoReady");
                if (_dt != null) { _t.CargoReady = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }
                
                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtWarehouse");
                if (_dt != null) { _t.WarehouseDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }
            
                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtBookingReceived");
                if (_dt != null) { _t.BookingReceived = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }
            
                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtDocsApproved");
                if (_dt != null) { _t.DocsApprovedDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                //checkboxes
                ASPxCheckBox _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobPubliship");
                if (_ck != null) { _t.PublishipOrder = _ck.Checked; }

                //jobclosed not visible on this form
                //_ck = (ASPxCheckBox)this.formOrder.FindControl("dxckJobClosed");
                //if (_ck != null) { 
                //    //check if job has been closed in this update
                //    if (_t.JobClosed == false && _ck.Checked == true) { _t.JobClosureDate = DateTime.Now; }
                //    _t.JobClosed = _ck.Checked; 
                //}
                
                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobHot");
                if (_ck != null) { _t.HotJob = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPalletised");
                if (_ck != null) { _t.Palletise = _ck.Checked? -1: 0; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditDocsAppr");
                if (_ck != null) { _t.DocsRcdAndApproved = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditIssueDl");
                if (_ck != null) { _t.ExpressBL = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditFumigation");
                if (_ck != null) { _t.FumigationCert = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditGSP");
                if (_ck != null) { _t.GSPCert = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPacking");
                if (_ck != null) { _t.PackingDeclaration = _ck.Checked; }
                
                //memos
                ASPxMemo _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToCustomer");
                if(_mo != null){ _t.RemarksToCustomer = _mo.Text.ToString(); }
                
                _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToAgent");
                if(_mo != null){ _t.Remarks = _mo.Text.ToString(); }
                
                _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoDocs");
                if(_mo != null){ _t.OtherDocsRequired = _mo.Text.ToString(); }
                
                //text boxes
                ASPxTextBox _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtCustomersRef");   
                if(_tx != null){ _t.CustomersRef = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingRate");   
                if(_tx != null){ _t.Sellingrate = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingAgent");
                if (_tx != null) { _t.SellingrateAgent = _tx.Text.ToString(); }

                //update record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _orderno = wwi_security.DecryptString(get_token("pno"), "publiship");
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order # {0} NOT updated. Error: {1}", _orderno ,  _ex);
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// append to order table
    /// 140114 replacing objectdatasource with code behind
    /// </summary>
    /// <param name="officeid">required so we can get next ordernumber from appropriate table</param>
    /// <returns></returns>
    protected int insert_order(int officeid, int newordernumber, string officeindicator)
    {
        //orderid of saved record
        int _newid = 0;
        
        try
        {
            if (newordernumber > 0)
            {
                OrderTable _t = new OrderTable();

                //defaults
                _t.OrderNumber = newordernumber;
                _t.OfficeIndicator = officeindicator;
                _t.DateOrderCreated = DateTime.Now;
                _t.EWDLastUpdated = DateTime.Now; //exworks last updated

                //dlls
                int? _intnull = null;
                ASPxComboBox _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboController");
                if (_cb != null) { _t.OrderControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOps");
                if (_cb != null) { _t.OperationsControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCompany");
                if (_cb != null) { _t.CompanyID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCountry");
                if (_cb != null) { _t.CountryID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOrigin");
                if (_cb != null) { _t.OriginPointID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginPort");
                if (_cb != null) { _t.PortID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboDestPort");
                if (_cb != null) { _t.DestinationPortID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboFinal");
                if (_cb != null) { _t.FinalDestinationID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboClientContact");
                if (_cb != null) { _t.ContactID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboPrinter");
                if (_cb != null) { _t.PrinterID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboAgentAtOrigin");
                if (_cb != null) { _t.AgentAtOriginID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginController");
                if (_cb != null) { _t.OriginPortControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                //dates
                DateTime? _dtnull = null; //default for nullable datetimes
                ASPxDateEdit _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtExWorks");
                if (_dt != null) { _t.ExWorksDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtCargoReady");
                if (_dt != null) { _t.CargoReady = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtWarehouse");
                if (_dt != null) { _t.WarehouseDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtBookingReceived");
                if (_dt != null) { _t.BookingReceived = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtDocsApproved");
                if (_dt != null) { _t.DocsApprovedDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

                //checkboxes
                ASPxCheckBox _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobPubliship");
                if (_ck != null) { _t.PublishipOrder = _ck.Checked; }

                //job closed not visible on this form
                //_ck = (ASPxCheckBox)this.formOrder.FindControl("dxckJobClosed");
                //if (_ck != null)
                //{
                //    //check if job has been closed in this append
                //    if (_t.JobClosed == false && _ck.Checked == true) { _t.JobClosureDate = DateTime.Now; }
                //    _t.JobClosed = _ck.Checked;
                //}

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobHot");
                if (_ck != null) { _t.HotJob = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPalletised");
                if (_ck != null) { _t.Palletise = _ck.Checked ? -1 : 0; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditDocsAppr");
                if (_ck != null) { _t.DocsRcdAndApproved = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditIssueDl");
                if (_ck != null) { _t.ExpressBL = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditFumigation");
                if (_ck != null) { _t.FumigationCert = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditGSP");
                if (_ck != null) { _t.GSPCert = _ck.Checked; }

                _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPacking");
                if (_ck != null) { _t.PackingDeclaration = _ck.Checked; }

                //memos
                ASPxMemo _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToCustomer");
                if (_mo != null) { _t.RemarksToCustomer = _mo.Text.ToString(); }

                _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToAgent");
                if (_mo != null) { _t.Remarks = _mo.Text.ToString(); }

                _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoDocs");
                if (_mo != null) { _t.OtherDocsRequired = _mo.Text.ToString(); }

                //text boxes
                ASPxTextBox _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtCustomersRef");
                if (_tx != null) { _t.CustomersRef = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingRate");
                if (_tx != null) { _t.Sellingrate = _tx.Text.ToString(); }

                _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingAgent");
                if (_tx != null) { _t.SellingrateAgent = _tx.Text.ToString(); }

                //append record
                _t.Save();
                //get new id
                _newid = (int)_t.GetPrimaryKeyValue();
            }
            else
            {
                string _ex = "Not able to find next Order Number";
                this.dxlblErr.Text = string.Format("Order NOT saved. Error: {0}", _ex);
                this.dxpnlErr.ClientVisible = true;
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = string.Format("Order NOT saved. Error: {0}", _ex);
            this.dxpnlErr.ClientVisible = true;
        }

        return _newid;
    }
    //end insert
#endregion

    #region db4objects persistence this code is not in use
    protected int? store_order_details()
    {
        //to use this code add these references to page and also enable static string for yapfile
        //using Db4objects.Db4o;
        //using Db4objects.Db4o.Config;
        //using Db4objects.Db4o.Query;

        int? _result = 0;
        //using ordertable class but saving to db4o
        OrderTable _t = new OrderTable();

        _t.OrderNumber = new Random().Next(int.MinValue, int.MaxValue); //temporary order number
        //dlls
        int? _intnull = null;
        ASPxComboBox _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboController");
        if (_cb != null) { _t.OrderControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOps");
        if (_cb != null) { _t.OperationsControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCompany");
        if (_cb != null) { _t.CompanyID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboCountry");
        if (_cb != null) { _t.CountryID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOrigin");
        if (_cb != null) { _t.OriginPointID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginPort");
        if (_cb != null) { _t.PortID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboDestPort");
        if (_cb != null) { _t.DestinationPortID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboFinal");
        if (_cb != null) { _t.FinalDestinationID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboClientContact");
        if (_cb != null) { _t.ContactID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboPrinter");
        if (_cb != null) { _t.PrinterID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboAgentAtOrigin");
        if (_cb != null) { _t.AgentAtOriginID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        _cb = (ASPxComboBox)this.formOrder.FindControl("dxcboOriginController");
        if (_cb != null) { _t.OriginPortControllerID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

        //dates
        DateTime? _dtnull = null; //default for nullable datetimes
        ASPxDateEdit _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtExWorks");
        if (_dt != null) { _t.ExWorksDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

        _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtCargoReady");
        if (_dt != null) { _t.CargoReady = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

        _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtWarehouse");
        if (_dt != null) { _t.WarehouseDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

        _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtBookingReceived");
        if (_dt != null) { _t.BookingReceived = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

        _dt = (ASPxDateEdit)this.formOrder.FindControl("dxdtDocsApproved");
        if (_dt != null) { _t.DocsApprovedDate = _dt.Value != null ? wwi_func.vdatetime(_dt.Value.ToString()) : _dtnull; }

        //checkboxes
        ASPxCheckBox _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobPubliship");
        if (_ck != null) { _t.PublishipOrder = _ck.Checked; }

        //job closed not visible on this form
        //_ck = (ASPxCheckBox)this.formOrder.FindControl("dxckJobClosed");
        //if (_ck != null)
        //{
        //    //check if job has been closed in this append
        //    if (_t.JobClosed == false && _ck.Checked == true) { _t.JobClosureDate = DateTime.Now; }
        //    _t.JobClosed = _ck.Checked;
        //}

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditJobHot");
        if (_ck != null) { _t.HotJob = _ck.Checked; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPalletised");
        if (_ck != null) { _t.Palletise = _ck.Checked ? -1 : 0; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditDocsAppr");
        if (_ck != null) { _t.DocsRcdAndApproved = _ck.Checked; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditIssueDl");
        if (_ck != null) { _t.ExpressBL = _ck.Checked; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditFumigation");
        if (_ck != null) { _t.FumigationCert = _ck.Checked; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditGSP");
        if (_ck != null) { _t.GSPCert = _ck.Checked; }

        _ck = (ASPxCheckBox)this.formOrder.FindControl("dxckEditPacking");
        if (_ck != null) { _t.PackingDeclaration = _ck.Checked; }

        //memos
        ASPxMemo _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToCustomer");
        if (_mo != null) { _t.RemarksToCustomer = _mo.Text.ToString(); }

        _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToAgent");
        if (_mo != null) { _t.Remarks = _mo.Text.ToString(); }

        _mo = (ASPxMemo)this.formOrder.FindControl("dxmemoDocs");
        if (_mo != null) { _t.OtherDocsRequired = _mo.Text.ToString(); }

        //text boxes
        ASPxTextBox _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtCustomersRef");
        if (_tx != null) { _t.CustomersRef = _tx.Text.ToString(); }

        _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingRate");
        if (_tx != null) { _t.Sellingrate = _tx.Text.ToString(); }

        _tx = (ASPxTextBox)this.formOrder.FindControl("dxtxtSellingAgent");
        if (_tx != null) { _t.SellingrateAgent = _tx.Text.ToString(); }

        //save to yap file
        //get userid to identify unique yap file name 
        //code disabled we aren't using it
        //string _userid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).UserId.ToString() : "";
        //string _yap = string.Format(_yapfile, _userid);
        
        //delete any old db4object yap files 
        //File.Delete(_yapfile);

        //Db4objects.Db4o classes
        //using (IObjectContainer _db = Db4oEmbedded.OpenFile(_yap))
        //{
        //    _db.Store(_t);
        //}
        //end object
        _result = _t.OrderNumber;
        return _result;
    }
    #endregion

    #region menuitem control
    /// <summary>
    /// crud events
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxmnuCommand_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _command = e.Item.Name.ToString();
        try
        {
            switch (_command)
            {
                case "cmdEdit": //edit
                    {
                        set_mode("Edit");
                        //for the mode to change call databind
                        bind_formview("Edit"); 
                        break;
                    }
                case "cmdSave": //insert and redirect to readonly view
                    {
                        int _officeid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).OfficeId : 0;
                        //using xml file for lookup at the OfficeIndicatorLookupTable in the database is not correctly configured (check with Dave re: fix)
                        string _newoffice = wwi_func.lookup_xml_string("xml\\office_names.xml", "value", _officeid.ToString(), "name");
                        //next available ordernumber, can't save to order table without a unique ordernumber
                        //but just use as placeholder until new order is saved, don't save new ordernumber until new after order is saved
                        //can't use a random int as ordernumber is indexed as unique
                        int _newordernumber = wwi_data.next_order_number(_officeid);
                        //append order and return orderid
                        int _newid = insert_order(_officeid, _newordernumber, _newoffice);
                        //id new order saved update  appropriate ordernumber table and the newly created order
                        if (_newid > 0)
                        {
                            //update_office_ordernumber: append new ordernumber to approprate ordernumber table and then update ordertable with this ordernumber
                            _newordernumber = wwi_data.update_office_ordernumber(_newid, _officeid);
                        
                            if (_newordernumber > 0)
                            {
                                string _pid = wwi_security.EncryptString(_newid.ToString(), "publiship");
                                string _pno = wwi_security.EncryptString(_newordernumber.ToString(), "publiship");
                                //Response.Redirect(string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&pno={2}", "ReadOnly", _pid, _pno));
                                //130814 after saved ok redirect to titles form and flag create new order (cno = 1)
                                Response.Redirect(string.Format("../shipment_orders/order_titles.aspx?mode={0}&pid={1}&pno={2}&cno={3}", "ReadOnly", _pid, _pno, 1));
                            }
                            else
                            {
                                this.dxlblErr.Text = string.Format("Order ID {0} has been saved without an order number", _newid);
                                this.dxpnlErr.ClientVisible = true;
                                set_mode("ReadOnly");
                                //for the mode to change call databind
                                bind_formview("ReadOnly"); 
                            }
                        }
                        break;
                    }
                case "cmdUpdate": //update
                    {
                        update_order(); 
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        bind_formview("ReadOnly"); 
                        //this.formOrder.UpdateItem(false);
                        break;
                    }
                case "cmdCancel":
                    {
                        set_mode("ReadOnly");
                        //for the mode to change call databind
                        bind_formview("ReadOnly");
                        break;
                    }
                case "cmdClose":
                    {
                        string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "order_search",};
                        string _url = string.Format("{0}\\{1}.aspx?", _args);
                        Response.Redirect(_url);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }//end switch
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
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
                    this.formOrder.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.formOrder.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.formOrder.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to readonly
                {
                    this.formOrder.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
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
        }//end menu names loop
    }
    /// <summary>
    /// DEPRECATED we are binding commands from xml file so we can filter required options from there
    /// </summary>
    protected void set_commands()
    {
        List<string> _menuitems = new List<string>();

        switch (this.formOrder.CurrentMode) {
            case FormViewMode.ReadOnly:
                {
                    _menuitems.Add("miNew");
                    _menuitems.Add("miEdit");
                    enable_command_items(_menuitems);
                    
                    break;
                }
            case FormViewMode.Edit:
                {
                    _menuitems.Add("miUpdate");
                    _menuitems.Add("miCancel");
                    enable_command_items(_menuitems);
                    break;
                }
            case FormViewMode.Insert:
                {
                    _menuitems.Add("miSave");
                    _menuitems.Add("miCancel");
                    enable_command_items(_menuitems);
                    break;
                }
        }
    }
    /// <summary>
    /// DEPRECATED enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_command_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmnuCommand.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuCommand.Items[_ix].Name == active[_mx]) { _isactive = true; }  
            }//end active names loop

            this.dxmnuCommand.Items[_ix].ClientVisible  = _isactive;
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

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion
}

