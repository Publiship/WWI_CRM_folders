using System;
using System.Data.SqlClient;
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

public partial class clone_order : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get primary key number (pno) -  OrderNumber for the clone page
        //we must have an ordernumber to make a clone from
        int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
        if (_orderid > 0)
        {
            if (isLoggedIn())
            {
                if (!Page.IsPostBack)
                {
                    //get office name - this will be used to determine the office indicator when the cloned order is saved
                    //and it might be different to the originial order depending on who is creating the clone
                    
                    string _officename = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).OfficeName : "";
                    this.dxlblOffice.Text = _officename;

                    //do before seting mode
                    //clone template should only have Readonly mode
                    bind_tabs();
                    bind_commands();

                    //we only need to view the details
                    string _mode = "ReadOnly"; //get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
                    set_mode(_mode);

                    //bind form
                    bind_formview(_orderid);
                }

                //*****************
                //no need for binding clone does not have Edit/Insert function it is for making straight copies of an existing order
                //dll binding for unfiltered dlls
                //bind_order_controller();
                //bind_operations_controller();
                //bind_dest_port(); //destination port
                //bind_final_dest(); //final destination not filtered in access database?

                /////filtered dlls
                ////client contact name
                //ASPxComboBox _dxcboCompany = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboCompanyID");
                //if (_dxcboCompany != null){
                //    bind_client_contact(_dxcboCompany.Value != null ? _dxcboCompany.Value.ToString() : "");
                //}

                //ASPxComboBox _cboAgentAtOrigin = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboAgentAtOriginID");
                //if (_cboAgentAtOrigin != null)
                //{   bind_origin_controller(_cboAgentAtOrigin.Value != null ? _cboAgentAtOrigin.Value.ToString() : ""); }

                //ASPxComboBox _cbo = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboAgentAtDestinationID");
                //if (_cbo != null) { 
                //    bind_destination_controller(_cbo.Value != null ? _cbo.Value.ToString() : "");  }

                //3 combos cascading set initial values here
                //bind_origin_combos(-1);
                //******************
            }
            else
            {
                Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_orders/clone_order", "publiship"));
            }
        }
        else
        {
            Response.Redirect("../shipment_orders/order_search.aspx");
        }
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #region form binding
    /// <summary>
    /// clone form is just ReadOnly we do not need any other functionality as it is for identical copies of an existing order
    /// if orderid is passed as param (pid) get data from order table
    /// </summary>
    /// <param name="ordertemplateid"></param>
    protected void bind_formview(int orderid)
    {
        string[] _key = { "OrderNumber" };
        CloneOrderTableCollection _col = new CloneOrderTableCollection();

        if (this.fmvTemplate.CurrentMode == FormViewMode.ReadOnly)
        {
            //blank template
            CloneOrderTable _clone = new CloneOrderTable();
 
            //create clone from selected order number (the clone table does not contain order id)
            int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
            
            if (_orderid > 0)
            {
                //copy from order to template
                OrderTable _order = new OrderTable(_orderid);

                _clone.DateOrderCreated = DateTime.Now;
                _clone.PublishipOrder = _order.PublishipOrder;
                _clone.OfficeIndicator = _order.OfficeIndicator;
                _clone.CompanyID = _order.CompanyID;
                _clone.ConsigneeID = _order.ConsigneeID;
                _clone.NotifyPartyID = _order.NotifyPartyID;
                _clone.AgentAtOriginID = _order.AgentAtOriginID;
                _clone.AgentAtDestinationID = _order.AgentAtDestinationID;
                _clone.PrinterID = _order.PrinterID;
                _clone.ClearingAgentID = _order.ClearingAgentID;
                _clone.OnCarriageID = _order.OnCarriageID;
                _clone.OrderControllerID = _order.OrderControllerID;
                _clone.OperationsControllerID = _order.OperationsControllerID;
                _clone.OriginPortControllerID = _order.OriginPortControllerID;
                _clone.DestinationPortControllerID = _order.DestinationPortControllerID;
                _clone.CustomersRef = _order.CustomersRef;
                _clone.ContactID = _order.ContactID;
                _clone.OriginPointID = _order.OriginPointID;
                _clone.PortID = _order.PortID;
                _clone.DestinationPortID = _order.DestinationPortID;
                _clone.FinalDestinationID = _order.FinalDestinationID;
                _clone.CountryID = _order.CountryID;
                _clone.DestinationCountryID = _order.DestinationCountryID;
            }
            
            _col.Add(_clone);

            this.fmvTemplate.DataSource = _col;
            this.fmvTemplate.DataKeyNames = _key;
            this.fmvTemplate.DataBind(); 

        }
        else
        {
            //_col.Add(new CloneOrderTable());
            //error we must have an order number to clone from
            this.dxlblErr.Text = "No order number has been selected for cloning";
            this.dxpnlErr.ClientVisible = true;
        }
    }

    protected void fmvTemplate_DataBound(object sender, EventArgs e)
    {
        //summary info
        //order number
        int _pno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        this.dxlblOrderNumber.Text = _pno.ToString();
        
        //view labels
        if (this.fmvTemplate.CurrentMode == FormViewMode.ReadOnly)
        {
            sub_decks_view(_pno);
        }//end read only form
        else  //edit and insert mode sub decks
        {
            sub_decks_edit();    
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
        _xml.XPath = "//menuitem"; //you need this or tab will not databind!
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
        //for the clone for we only have readonly option so disable all tabs apart from 1st one
        //string _orderid = get_token("pid");
        //string _orderno = get_token("pno");
        //string _label = this.dxlbOrderDetails1.Text.Replace("|", "").Trim();

        for (int _ix = 1; _ix < this.dxtabOrder.Tabs.Count; _ix++)
        {
            //NOT RELEVANT ON THIS FORM match tab text to title label and use it to set active tab
            //if (this.dxtabOrder.Tabs[_ix].Text == this.dxlbOrderDetails1.Text) { this.dxtabOrder.ActiveTabIndex = _ix; }
            //format urls with order no
            //if (!string.IsNullOrEmpty(this.dxtabOrder.Tabs[_ix].NavigateUrl)) { this.dxtabOrder.Tabs[_ix].NavigateUrl = string.Format(this.dxtabOrder.Tabs[_ix].NavigateUrl, _orderid, _orderno); }
            this.dxtabOrder.Tabs[_ix].ClientVisible = false;
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
        _xml.XPath = "//item[@Filter='Clone']"; //you need this or tab will not databind!
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
        //do not setr navigate url as it prevents itemclick event from firing
        e.Item.NavigateUrl = "";
        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        //{
        //    //get path to form
        //    string _folder = System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath);
        //    //change to formview page by removing 'search' e.g. container_search.aspx becomes container.aspx
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath).Replace("_search", "");
        //    //primary key id
        //    string _id = get_token("pno");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _folder + "\\" + _page, _id); }
        //}
    }
    #endregion

    #region sub decks
    protected void sub_decks_view(int orderno)
    {
        linq.linq_order_sheet_udfDataContext _linq = new linq.linq_order_sheet_udfDataContext();

        //order details from table valued function
        IList<linq.order_sheetResult> _o = _linq.order_sheet(orderno).ToList<linq.order_sheetResult>();

        //1st table row
        ASPxLabel _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOrderControllerIDName");
        if (_lbl != null && _o[0].OrderController!= null) { _lbl.Text = _o[0].OrderController.ToString(); }

        //office id on original order
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOfficeIndicatorName");
        if (_lbl != null && _o[0].OfficeIndicator != null) { _lbl.Text = _o[0].OfficeIndicator.ToString(); }
        
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblCustomersRefName");
        if (_lbl != null && _o[0].CustomersRef != null) { _lbl.Text = _o[0].CustomersRef.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOperationsControllerIDName");
        if (_lbl != null && _o[0].OpsController != null) { _lbl.Text = _o[0].OpsController.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblContactIDName");
        if (_lbl != null && _o[0].ContactName != null) { _lbl.Text = _o[0].ContactName.ToString(); }

        //2nd table row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblCountryIDName");
        if (_lbl != null && _o[0].OriginCountry != null) { _lbl.Text = _o[0].OriginCountry.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblDestinationPortIDName");
        if (_lbl != null && _o[0].DestinationPort != null) { _lbl.Text = _o[0].DestinationPort.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblCompanyIDName");
        if (_lbl != null && _o[0].CustomerName != null) { _lbl.Text = _o[0].CustomerName.ToString(); }

        //3rd table row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOriginPointIDName");
        if (_lbl != null && _o[0].OriginPlace != null) { _lbl.Text = _o[0].OriginPlace.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblFinalDestinationIDName");
        if (_lbl != null && _o[0].FinalDestination != null) { _lbl.Text = _o[0].FinalDestination.ToString(); }
        
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblCompanyIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].CustomerAddress1 != null ? _o[0].CustomerAddress1.ToString() : "";
            _lbl.Text += _o[0].CustomerAddress2 != null ? Environment.NewLine + _o[0].CustomerAddress2.ToString() : "";
            _lbl.Text += _o[0].CustomerAddress3 != null ? Environment.NewLine + _o[0].CustomerAddress3.ToString() : "";
            _lbl.Text += _o[0].CustomerPostCode != null ? Environment.NewLine + _o[0].CustomerPostCode.ToString() : ""; 
            _lbl.Text += _o[0].CustomerCountry != null ? Environment.NewLine + _o[0].CustomerCountry.ToString() : ""; 
            _lbl.Text += _o[0].CustomerTelNo != null ? Environment.NewLine + _o[0].CustomerTelNo.ToString() : ""; 
        }

        //4th row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblPortIDName");
        if (_lbl != null && _o[0].OriginPort != null) { _lbl.Text = _o[0].OriginPort.ToString(); }

        //5th row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOriginPortControllerIDName");
        if (_lbl != null && _o[0].OriginController != null) { _lbl.Text = _o[0].OriginController.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblDestinationPortControllerIDName");
        if (_lbl != null && _o[0].DestController != null) { _lbl.Text = _o[0].DestController.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblPrinterIDName");
        if (_lbl != null && _o[0].PrinterName != null) { _lbl.Text = _o[0].PrinterName.ToString(); }

        //6th row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblPrinterIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].PrinterAddress1 != null ? _o[0].PrinterAddress1.ToString() : "";
            _lbl.Text += _o[0].PrinterAddress2 != null ? Environment.NewLine + _o[0].PrinterAddress2.ToString() : "";
            _lbl.Text += _o[0].PrinterAddress3 != null ? Environment.NewLine + _o[0].PrinterAddress3.ToString() : "";
            _lbl.Text += _o[0].PrinterPostCode != null ? Environment.NewLine + _o[0].PrinterPostCode.ToString() : "";
            _lbl.Text += _o[0].PrinterCountry != null ? Environment.NewLine + _o[0].PrinterCountry.ToString() : "";
            _lbl.Text += _o[0].PrinterTelNo != null ? Environment.NewLine + _o[0].PrinterTelNo.ToString() : ""; 
        }
    
        //7th row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblConsigneeIDName");
        if (_lbl != null && _o[0].ConsigneeName != null) { _lbl.Text = _o[0].ConsigneeName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblConsigneeIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].ConsigneeAddress1 != null ? _o[0].ConsigneeAddress1.ToString() : "";
            _lbl.Text += _o[0].ConsigneeAddress2 != null ? Environment.NewLine + _o[0].ConsigneeAddress2.ToString() : "";
            _lbl.Text += _o[0].ConsigneeAddress3 != null ? Environment.NewLine + _o[0].ConsigneeAddress3.ToString() : "";
            _lbl.Text += _o[0].ConsigneePostCode != null ? Environment.NewLine + _o[0].ConsigneePostCode.ToString() : "";
            _lbl.Text += _o[0].ConsigneeCountry != null ? Environment.NewLine + _o[0].ConsigneeCountry.ToString() : "";
            _lbl.Text += _o[0].ConsigneeTelNo != null ? Environment.NewLine + _o[0].ConsigneeTelNo.ToString() : ""; 
        }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblAgentAtOriginIDName");
        if (_lbl != null && _o[0].OriginAgentName != null) { _lbl.Text = _o[0].OriginAgentName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblAgentAtOriginIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].OriginAgentAddress1 != null ? _o[0].OriginAgentAddress1.ToString() : "";
            _lbl.Text += _o[0].OriginAgentAddress2 != null ? Environment.NewLine + _o[0].OriginAgentAddress2.ToString() : "";
            _lbl.Text += _o[0].OriginAgentAddress3 != null ? Environment.NewLine + _o[0].OriginAgentAddress3.ToString() : "";
            _lbl.Text += _o[0].OriginAgentPostCode != null ? Environment.NewLine + _o[0].OriginAgentPostCode.ToString() : "";
            _lbl.Text += _o[0].OriginAgentCountry != null ? Environment.NewLine + _o[0].OriginAgentCountry.ToString() : "";
            _lbl.Text += _o[0].OriginAgentTelNo != null ? Environment.NewLine + _o[0].OriginAgentTelNo.ToString() : ""; 
         }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblNotifyIDName");
        if (_lbl != null && _o[0].NotifyName != null) { _lbl.Text = _o[0].NotifyName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblNotifyIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].NotifyAddress1 != null ? _o[0].NotifyAddress1.ToString() : "";
            _lbl.Text += _o[0].NotifyAddress2 != null ? Environment.NewLine + _o[0].NotifyAddress2.ToString() : "";
            _lbl.Text += _o[0].NotifyAddress3 != null ? Environment.NewLine + _o[0].NotifyAddress3.ToString() : "";
            _lbl.Text += _o[0].NotifyAddress4 != null ? Environment.NewLine + _o[0].NotifyAddress4.ToString() : "";
            _lbl.Text += _o[0].NotifyCountry != null ? Environment.NewLine + _o[0].NotifyCountry.ToString() : "";
            _lbl.Text += _o[0].NotifyTelNo != null ? Environment.NewLine + _o[0].NotifyTelNo.ToString() : ""; 
        }
        
        //final row
        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblAgentAtDestinationIDName");
        if (_lbl != null && _o[0].DestAgentName != null) { _lbl.Text = _o[0].DestAgentName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblAgentAtDestinationIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].DestAgentAddress1 != null ? _o[0].DestAgentAddress1.ToString() : "";
            _lbl.Text += _o[0].DestAgentAddress2 != null ? Environment.NewLine + _o[0].DestAgentAddress2.ToString() : "";
            _lbl.Text += _o[0].DestAgentAddress3 != null ? Environment.NewLine + _o[0].DestAgentAddress3.ToString() : "";
            _lbl.Text += _o[0].DestAgentpostCode != null ? Environment.NewLine + _o[0].DestAgentpostCode.ToString() : "";
            _lbl.Text += _o[0].DestAgentCountry != null ? Environment.NewLine + _o[0].DestAgentCountry.ToString() : "";
            _lbl.Text += _o[0].DestAgentTelNo != null ? Environment.NewLine + _o[0].DestAgentTelNo.ToString() : ""; 
          }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblClearingAgentIDName");
        if (_lbl != null && _o[0].ClearingAgentName != null) { _lbl.Text = _o[0].ClearingAgentName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblClearingAgentIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].ClearingAgentAddress1 != null ? _o[0].ClearingAgentAddress1.ToString() : "";
            _lbl.Text += _o[0].ClearingAgentAddress2 != null ? Environment.NewLine + _o[0].ClearingAgentAddress2.ToString() : "";
            _lbl.Text += _o[0].ClearingAgentAddress3 != null ? Environment.NewLine + _o[0].ClearingAgentAddress3.ToString() : "";
            _lbl.Text += _o[0].ClearingAgentAddress4 != null ? Environment.NewLine + _o[0].ClearingAgentAddress4.ToString() : "";
            _lbl.Text += _o[0].ClearingAgentCountry != null ? Environment.NewLine + _o[0].ClearingAgentCountry.ToString() : "";
            _lbl.Text += _o[0].ClearingAgentTelNo != null ? Environment.NewLine + _o[0].ClearingAgentTelNo.ToString() : ""; 
        }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOnCarriageIDName");
        if (_lbl != null && _o[0].OnCarriageName != null) { _lbl.Text = _o[0].OnCarriageName.ToString(); }

        _lbl = (ASPxLabel)this.fmvTemplate.FindControl("dxlblOnCarriageIDView");
        if (_lbl != null)
        {
            _lbl.Text = _o[0].OnCarriageAddress1 != null ? _o[0].OnCarriageAddress1.ToString() : "";
            _lbl.Text += _o[0].OnCarriageAddress2 != null ? Environment.NewLine + _o[0].OnCarriageAddress2.ToString() : "";
            _lbl.Text += _o[0].OnCarriageAddress3 != null ? Environment.NewLine + _o[0].OnCarriageAddress3.ToString() : "";
            _lbl.Text += _o[0].OnCarriageAddress4 != null ? Environment.NewLine + _o[0].OnCarriageAddress4.ToString() : "";
            _lbl.Text += _o[0].OnCarriageCountry != null ? Environment.NewLine + _o[0].OnCarriageCountry.ToString() : "";
            _lbl.Text += _o[0].OnCarriageTelNo != null ? Environment.NewLine + _o[0].OnCarriageTelNo.ToString() : ""; 
        }
    }
    protected void sub_decks_edit()
    {
        //fields with sub text
        string[] _fields = { "CompanyID", "PrinterID", "ConsigneeID", "AgentAtOriginID", "NotifyPartyID", "AgentAtDestinationID", "ClearingAgentID", "OnCarriageID" };
        string _s = "";
        //step through field names
        //get combobox value from dxcbo<fieldname>Edit
        //set address text on dxlbl<fieldname>Edit label
        for (int _ix = 0; _ix < _fields.Length; _ix++)
        {
            _s = "";
            string _cboid = "dxcbo" + _fields[_ix];
            ASPxComboBox _cbCompany = (ASPxComboBox)this.fmvTemplate.FindControl("dxcbo" + _fields[_ix]);
            if (_cbCompany != null && _cbCompany.SelectedItem != null && _cbCompany.Value != null)
            {
                _s = _cbCompany.SelectedItem.Text.ToString().Replace(";", Environment.NewLine.ToString()) ;
                
                //if (_cbCompany.SelectedItem.GetValue("Address1") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address1").ToString(); } //(string)_cbCompany.SelectedItem.Value.ToString();
                //_s += Environment.NewLine;
                //if (_cbCompany.SelectedItem.GetValue("Address2") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address2").ToString(); }
                //_s += Environment.NewLine;
                //if (_cbCompany.SelectedItem.GetValue("Address3") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("Address3").ToString(); }
                //_s += Environment.NewLine;
                //if (_cbCompany.SelectedItem.GetValue("CountryName") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("CountryName").ToString(); }
                //_s += Environment.NewLine;
                //if (_cbCompany.SelectedItem.GetValue("TelNo") != null) { _s = _s + (string)_cbCompany.SelectedItem.GetValue("TelNo").ToString(); }
                //_s += Environment.NewLine;

            }
            ASPxLabel _lblCompany = (ASPxLabel)this.fmvTemplate.FindControl("dxlbl" + _fields[_ix] + "View");
            if (_lblCompany != null) { _lblCompany.Text = _s; }

            
        }//end loop
    }
    #endregion
 
    #region dll binding
   
    protected void bind_order_controller()
    {
        string[] _cols = { "EmployeeID, Name" };
        string[] _order = { "Name" };
        SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);

        //order controller
        ASPxComboBox _dxcboController = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboOrderControllerID");
        if (_dxcboController != null)
        {
            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboController.DataSource = _rd1;
            _dxcboController.ValueField = "EmployeeID";
            _dxcboController.ValueType = typeof(int);
            _dxcboController.TextField = "Name";
            _dxcboController.DataBindItems();
        }
    }
    
    protected void bind_operations_controller()
    {
        string[] _cols = { "EmployeeID, Name" };
        string[] _order = { "Name" };
        SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);

        //operations controller
        ASPxComboBox _dxcboOps = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboOperationsControllerID");
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
    /// <summary>
    /// contact callback fired when company is changed
    /// </summary>
    /// <param name="companyID"></param>
    protected void bind_client_contact(string companyID)
    {
        //must have a filter or display nothing
        if (!string.IsNullOrEmpty(companyID))
        {
            ASPxComboBox _dxcboContact = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboContactID");
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

    protected void bind_countries()
    {
        //DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel _call = (DevExpress.Web.ASPxCallbackPanel.ASPxCallbackPanel)this.formOrder.FindControl("dxcbkOriginGroup");
        //if (_call != null)
        //{
        //ASPxComboBox _dxcboCountry = (ASPxComboBox)_call.FindControl("dxcboCountry");
        ASPxComboBox _dxcboCountry = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboCountryID");
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
        ASPxComboBox _dxcboOrigin = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboOriginPointID");
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
        ASPxComboBox _dxcboOriginPort = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboPortID");

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
            ASPxComboBox _dxcboCountry = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboCountryID");
            if (_dxcboCountry != null)
            {
                bind_origin(_dxcboCountry.Value != null ? _dxcboCountry.Value.ToString() : "");
            }
        }

        if (selectcombo == -1 || selectcombo == 2)
        {
            //060512 origin port is not filtered in Access database?
            bind_origin_port("");
         
        }
        //}
    }

    //end bind origin port
    protected void bind_dest_port()
    {
        ASPxComboBox _dxcboDestPort = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboDestinationPortID");
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
        ASPxComboBox _dxcboFinal = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboFinalDestinationID");
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

    
    protected void bind_origin_controller(string originAgentID)
    {
        //260211 some older jobs have an origin controller but no origin agent in those cases don't display the origin controller
        //must have a filter or display nothing
        if (!string.IsNullOrEmpty(originAgentID))
        {
            ASPxComboBox _dxcboController = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboOriginPortControllerID");
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

    protected void bind_destination_controller(string destinationAgentID)
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboDestinationPortControllerID"); 
        if (_combo != null)
        {

            string[] _cols = { "EmployeesTable.EmployeeID, EmployeesTable.Name", "EmployeesTable.DepartmentID", "OfficeTable.OfficeID"};
            string[] _order = { "Name" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).
                InnerJoin(DAL.Logistics.OfficeTable.OfficeIDColumn, DAL.Logistics.EmployeesTable.OfficeIDColumn);

            if (!string.IsNullOrEmpty(destinationAgentID))
            {
                int _filter = wwi_func.vint(destinationAgentID);
                _qry.Where("OfficeID").IsEqualTo(_filter); 
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
    #endregion

    #region incremental filtering for large combobox datasets
    //incremental filtering for large datasets on combos
    /// <summary>
    /// incremental filtering and partial loading of vessels for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboCompanyID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
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
            case "dxcboClearingAgentID":
                {
                    int[] _vals = { 3, 6 };
                    _query.And("TypeID").In(_vals);  
                    break;
                }
            case "dxcboOnCarriageID":
                {
                    _query.And("TypeID").IsEqualTo(3);
                    break;
                }
            case "dxcboAgentAtDestinationID":
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
    protected void dxcboCompanyID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
    /// <summary>
    /// destinatiobn controller
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboDestControl_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)source;
        ASPxComboBox _destinationagent = (ASPxComboBox)this.fmvTemplate.FindControl("dxcboAgentAtDestinationIDEdit"); 
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

            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }

    //printer
    protected void dxcboPrinterID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        //{
        //    Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
        //    if (_companyid == -1)
        //    {
        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "CompanyID, PrinterName", "PrinterAdd1", "PrinterAdd2", "PrinterAdd3", "PrinterCountry", "PrinterTel" };

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
    protected void dxcboPrinterID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
        string[] _cols = { "CompanyID, PrinterName", "PrinterAdd1", "PrinterAdd2", "PrinterAdd3", "PrinterCountry", "PrinterTel" };

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
    //end incremental filtering of printer
    #endregion

    #region crud events
    /// <summary>
    /// on save 
    /// copy order from ordertable to new record in ordertable
    /// </summary>
    protected int insert_clone(int orderid, int neworderno, string officeindicator)
    {
        int _newid = 0;
        
        try
        {
            //get order details we are cloning
            OrderTable _clone = new OrderTable(orderid); 
            
            //new order
            OrderTable _neworder = new OrderTable();
            //values changed from original order
            _neworder.DateOrderCreated = DateTime.Now;
            _neworder.OfficeIndicator = officeindicator;
            _neworder.OrderNumber = neworderno;

            //values derived from original order
            _neworder.PublishipOrder = _clone.PublishipOrder;
            _neworder.CompanyID = _clone.CompanyID;
            _neworder.ConsigneeID = _clone.ConsigneeID;
            _neworder.NotifyPartyID = _clone.NotifyPartyID;
            _neworder.AgentAtOriginID = _clone.AgentAtOriginID;
            _neworder.AgentAtDestinationID = _clone.AgentAtDestinationID;
            _neworder.PrinterID = _clone.PrinterID;
            _neworder.ClearingAgentID = _clone.ClearingAgentID;
            _neworder.OnCarriageID = _clone.OnCarriageID;
            _neworder.OrderControllerID = _clone.OrderControllerID;
            _neworder.OperationsControllerID = _clone.OperationsControllerID;
            _neworder.OriginPortControllerID = _clone.OriginPortControllerID;
            _neworder.DestinationPortControllerID = _clone.DestinationPortControllerID;
            _neworder.CustomersRef = _clone.CustomersRef;
            _neworder.ContactID = _clone.ContactID;
            _neworder.OriginPointID = _clone.OriginPointID;
            _neworder.PortID = _clone.PortID;
            _neworder.DestinationPortID = _clone.DestinationPortID;
            _neworder.FinalDestinationID = _clone.FinalDestinationID;
            _neworder.CountryID = _clone.CountryID;
            _neworder.DestinationCountryID = _clone.DestinationCountryID;

            _neworder.Save();

            _newid = (int)_neworder.GetPrimaryKeyValue();
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

    #region menu and tab control
    protected void dxmnuCommand_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();
        List<string> _menuitems = new List<string>();
        this.dxpnlErr.ClientVisible = false;

        try
        {
            switch (_item)
            {
                case "cmdNew": //not enabled
                    {
                        //set_mode("Insert");
                        break;
                    }
                case "cmdEdit": //not enabled
                    {
                        //set_mode("Edit");
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                        //this.formOrder.DeleteItem(); 
                        break;
                    }
                case "cmdUpdate": //not enabled
                    {
                        //this.fmvContainer.UpdateItem(false);
                        //int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
                        //if (_orderno > 0)
                        //{
                         //   
                        //    set_mode("ReadOnly");
                        //    //set_mode("view"); not necesary form will revert to read only after save
                        //}
                        break;
                    }
                case "cmdSave":
                    {
                        //this.fmvContainer.InsertItem(false);
                        //orderid of order to clone
                        int _orderid = wwi_func.vint(wwi_security.DecryptString(get_token("pid"), "publiship"));
                        //get officeid, officeindicator - this will be used to determine the office indicator when the cloned order is saved
                        //and it might be different to the original order depending on who is creating the clone
                        int _officeid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).OfficeId : 0;
                        //get new office indicator
                        //using xml file for lookup at the officelookuptable in the database is not correctly configured (check with Dave re: fix)
                        string _newoffice = wwi_func.lookup_xml_string("xml\\office_names.xml", "value", _officeid.ToString(), "name");
                        //initialise values ordernumber
                        int _neworderno = 0;
                        //new orderid
                        int _neworderid = 0;

                        if (_orderid > 0)
                        {
                            //get an orderno but onlhy as a placeholder as it's required in ordertable. ordrno is set only after record appended to ordertable
                            //can't use a random int as ordernumber is indexed as unique
                            _neworderno = wwi_data.next_order_number(_officeid);  
                            //then append clone to ordertable returning new orderid if successful
                            _neworderid = insert_clone(_orderid, _neworderno, _newoffice);
                            if (_neworderid > 0)
                            {
                                //update_office_ordernumber:  append ordernumber to appropriate ordernumber table depending on office and
                                //update new order in ordertable with actual ordernumber
                                _neworderno = wwi_data.update_office_ordernumber(_neworderid, _officeid);
                                if (_neworderno > 0)
                                {
                                    string _pid = wwi_security.EncryptString(_neworderid.ToString(), "publiship");
                                    string _pno = wwi_security.EncryptString(_neworderno.ToString(), "publiship");
                                    //redirect to cloned record
                                    Response.Redirect(string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&req={2}&pno={3}", "ReadOnly", _pid, "Search", _pno));
                                }
                                else
                                {
                                    this.dxlblErr.Text = string.Format("Order ID {0} has been saved without an order number", _neworderid);
                                    this.dxpnlErr.ClientVisible = true;
                                }
                            }
                            else
                            {
                                this.dxlblErr.Text = "Error creating clone";
                                this.dxpnlErr.ClientVisible = true;
                            }//endif      
                        }
                        break;
                    }
                case "cmdCancel":
                    {
                        //set_mode("ReadOnly");
                        //redirect back to order search
                        Response.Redirect("../shipment_orders/order_search.aspx",true);
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
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end menu commands

    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit": //clone does not have this option
                {
                    this.fmvTemplate.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert": //clone does not have this option
                {
                    this.fmvTemplate.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly": //only available mode, but gives the user option to save or cancel as if it was insert
                {
                    this.fmvTemplate.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    //_menuitems.Add("Edit");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvTemplate.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Save");
                    _menuitems.Add("Cancel");
                    //_menuitems.Add("Edit");
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
        this.fmvTemplate.ChangeMode(e.NewMode);
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
        string _value = this.dxhfTemplate.Contains(namedtoken) ? this.dxhfTemplate.Get(namedtoken).ToString() : null;

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

    #region call backs
    /// <summary>
    /// filgtder contactID's based on selected company
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboContactID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_client_contact(e.Parameter);
    }
    /// <summary>
    /// callbacks for origin and origin port as we aree not using a callback panel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboOriginPointID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin(e.Parameter);
    }

    protected void dxcboPortID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin_combos(2);
    }

    protected void dxcboOriginPortControllerID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_origin_controller(e.Parameter);
    }

    protected void dxcboDestinationPortControllerID_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_destination_controller(e.Parameter);   
    }
#endregion

    
}

