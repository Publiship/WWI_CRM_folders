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
using DevExpress.Web.ASPxGridView;
using SubSonic;

public partial class order_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            if (this.dxhfOrder.Contains("pno")) { this.dxhfOrder.Remove("pno"); }
            if (this.dxhfOrder.Contains("pid")) { this.dxhfOrder.Remove("pid"); }
            bind_summary();

            if (!Page.IsPostBack)
            {
                bind_tabs(); //navigation tabs
                bind_commands(); //crud commands
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("order_search", "publiship"));
        }
    }

    #region form binding
    /// <summary>
    /// order details summary using order id (pid) or we can use order number (pno) as other tables are bound to
    /// order number not id
    /// </summary>
    protected void bind_summary()
    {
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"),"publiship")); //999909;
        if (_orderno > 0)
        {
            SubSonic.SqlQuery _q = new SubSonic.SqlQuery();
            _q = DB.Select().From("view_order_summary").Where("OrderNumber").IsEqualTo(_orderno);    
            IDataReader _rd = _q.ExecuteReader();
            //drop data into table
            while (_rd.Read())
            {
                //checkboxes
                this.dxckEditJobPubliship.Value  = _rd["PublishipOrder"];
                this.dxckEditIssueDl.Value = _rd["ExpressBL"];
                this.dxckEditFumigation.Value = _rd["FumigationCert"];
                this.dxckEditGSP.Value = _rd["GSPCert"];
                this.dxckEditPacking.Value = _rd["PackingDeclaration"];
                this.dxckEditJobHot.Value = _rd["HotJob"];
                this.dxckEditPalletised.Value = _rd["Palletise"];
                this.dxckEditDocsAppr.Value = _rd["DocsRcdAndApproved"];
                this.dxckJobClosed.Value = _rd["JobClosed"];

                //icon for publiship job
                this.dxlblJobPubliship.ClientVisible = this.dxckEditJobPubliship.Checked;
                this.dximgJobPubliship.ClientVisible = this.dxckEditJobPubliship.Checked;
                //icon for hot job
                this.dxlblJobHot.ClientVisible = this.dxckEditJobHot.Checked;
                this.dximgJobHot.ClientVisible = this.dxckEditJobHot.Checked;
                //icon for job closed
                this.dxlblJobClosed.ClientVisible = this.dxckJobClosed.Checked;
                this.dximgJobClosed.ClientVisible = this.dxckJobClosed.Checked;

                //labels
                this.dxlblOfficeIndicator.Text = _rd["OfficeIndicator"].ToString();
                this.dxlblDateCreated.Text = _rd["DateOrderCreated"].ToString();
                this.dxlblViewController.Text = _rd["OrderController"].ToString();
                this.dxlblViewContact.Text = _rd["ContactName"].ToString();
                this.dxlblViewOps.Text = _rd["OpsController"].ToString();
                this.dxlblContactEmail.Text = _rd["EMail"].ToString();
                this.dxlblViewCompany.Text = _rd["CompanyName"].ToString();
                this.dxlblViewPrinter.Text = _rd["PrinterName"].ToString();
                this.dxlblViewDocs.Text = _rd["OtherDocsRequired"].ToString();
                this.dxlblViewCountry.Text = _rd["OriginCountry"].ToString();
                this.dxlblViewOrigin.Text = _rd["PlaceName"].ToString();
                this.dxlblViewOriginPort.Text = _rd["OriginPort"].ToString();
                this.dxlblViewAgentAtOrigin.Text = _rd["OriginAgent"].ToString();
                this.dxlblViewDestPort.Text = _rd["DestinationPort"].ToString();
                this.dxlblViewFinal.Text = _rd["FinalDestination"].ToString();
                this.dxlblViewCustomerRef.Text = _rd["CustomersRef"].ToString();
                this.dxlblViewRemarksAgent.Text = _rd["Remarks"].ToString();
                this.dxlblViewRemarksToCustomer.Text = _rd["RemarksToCustomer"].ToString();
                this.dxlblViewSellingRate.Text = _rd["Sellingrate"].ToString();
                this.dxlblViewSellingAgent.Text = _rd["SellingrateAgent"].ToString();
                //260211 some older jobs have an origin controller but no origin agent in those cases don't display the origin controller
                if (!string.IsNullOrEmpty(this.dxlblViewAgentAtOrigin.Text.ToString()))
                {
                    this.dxlblViewOriginController.Text = _rd["OriginPortController"].ToString();
                }

                //dates
                this.dxlblViewBookingReceived.Text = !string.IsNullOrEmpty(_rd["BookingReceived"].ToString())  ? Convert.ToDateTime(_rd["BookingReceived"].ToString()).Date.ToString() :  "";
                this.dxlblViewCargoReady.Text = !string.IsNullOrEmpty(_rd["CargoReady"].ToString()) ? Convert.ToDateTime(_rd["CargoReady"].ToString()).Date.ToString(): "";
                //this.dxlblViewWarehouse.Text = !string.IsNullOrEmpty(_rd["WarehouseDate"].ToString()) ? Convert.ToDateTime(_rd["WarehouseDate"].ToString()).Date.ToString(): "";
                this.dxlblViewExWorks.Text = !string.IsNullOrEmpty(_rd["ExWorksDate"].ToString()) ? Convert.ToDateTime(_rd["ExWorksDate"].ToString()).Date.ToString() : "";
                this.dxlblViewDocsApprovedDate.Text = !string.IsNullOrEmpty(_rd["DocsApprovedDate"].ToString()) ? Convert.ToDateTime(_rd["DocsApprovedDate"].ToString()).Date.ToString(): "";
                
                //addresses
               this.dxlblCompanyAddress.Text = _rd["Address1"].ToString() + Environment.NewLine + _rd["Address2"].ToString() +
                    Environment.NewLine + _rd["Address3"].ToString() + Environment.NewLine + _rd["CountryName"].ToString() +
                    Environment.NewLine + _rd["TelNo"].ToString();
               
               
                this.dxlblPrinterAddress.Text = _rd["PrinterAdd1"].ToString() + Environment.NewLine + _rd["PrinterAdd2"].ToString() +
                        Environment.NewLine + _rd["PrinterAdd3"].ToString() + Environment.NewLine + _rd["PrinterCountry"].ToString() +
                        Environment.NewLine + _rd["PrinterTel"].ToString();

                this.dxlblOriginAgentAddress.Text = _rd["OriginAgentAddress1"].ToString() + Environment.NewLine + _rd["OriginAgentAddress2"].ToString() +
                       Environment.NewLine + _rd["OriginAgentAddress3"].ToString() + Environment.NewLine + _rd["OriginAgentCountry"].ToString() +
                       Environment.NewLine + _rd["OriginAgentTel"].ToString();

                //order no and order id to hidden fields
                this.dxhfOrder.Add("pno", wwi_security.EncryptString( _rd["OrderNumber"].ToString(), "publiship"));
                this.dxhfOrder.Add("pid", wwi_security.EncryptString(_rd["OrderID"].ToString(), "publiship"));

                this.dxlblOrderNo.Text = _rd["OrderNumber"].ToString();

            }

        }
        else
        {
            string _ex = "No Order number";
            this.dxlblErr.Text = _ex;
            this.pnlErr.Visible = true;
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
        string _orderno = get_token("pno");
        string _label = this.dxlbOrderDetails1.Text.Replace("|", "").Trim();
 
        for (int _ix = 0; _ix < this.dxtabOrder.Tabs.Count; _ix++)
        {
            //match tab text to title label and use it to set active tab
            if (this.dxtabOrder.Tabs[_ix].Text == _label ) { this.dxtabOrder.ActiveTabIndex = _ix; } 
            //format urls with order no
            if (!string.IsNullOrEmpty(this.dxtabOrder.Tabs[_ix].NavigateUrl)) { this.dxtabOrder.Tabs[_ix].NavigateUrl = string.Format(this.dxtabOrder.Tabs[_ix].NavigateUrl, _orderno); }
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
        _xml.XPath = "//item[@Filter='View']"; //you need this or tab will not databind!
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
    /// <summary>
    /// once command menu initialised format urls with current order no (not decrypted) 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxmnuOrder_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _orderno = get_token("pno");
        if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _orderno); }
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

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
#endregion

   
}

