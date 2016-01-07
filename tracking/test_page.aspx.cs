using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Gauges.Digital;
using DevExpress.XtraCharts;

public partial class test_page : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            //bind_order_titles();
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("order_search", "publiship"));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (isLoggedIn() && !Page.IsPostBack)
        //{
        //temporarily disabled. have we redirected from new order?
        //string _new = get_token("cno");
        //if (!string.IsNullOrEmpty(_new))
        //{
        //    this.dxlblInfo.Text = "Please add titles to this order (Click the + button or 'Add title' button";
        //    this.dxpnlMsg.ClientVisible = true;
        //
        //}
        if (!Page.IsPostBack)
        {
            string _test = wwi_security.EncryptString("144816", "publiship");
            this.dxhfOrder.Clear();
            this.dxhfOrder.Add("pno", _test);

        }
        this.barCodeSSCC.Visible = false;
        
        //}
        //else
        //{
        //    string _orderid = Request.QueryString["pid"] != null ? "&pid=" + Request.QueryString["pid"].ToString() : "";
        //    Response.Redirect("~/Sys_Session_Login.aspx?" + "rp=" + wwi_security.EncryptString("Pod_Titles", "publiship") + _orderid);
        //}
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    #region testing sscc_checkdigit 
    protected void dxcbpBarCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _item = this.dxtxtCode.Text.Replace(" ", "").ToString();
        string _result = wwi_func.sscc_checkdigit(_item);
        bool _msg = false;
        if(!string.IsNullOrEmpty(_result))
        {
            //should have returned a single check digit 0-9 anything else is an error
            //display message 
            _msg = _result.Length > 1; //true if not a single digit
            this.dxlblErr.Text = _result;
            this.dxpnlErr.ClientVisible = _msg;
            
            //show checkdigit results
            this.dxtxtResult.Text = _result;
            
            //Paul Edwards 020914 don't use the Spire barcode control it has a bug when generating sscc18 codes it does not display check digit
            //generate bar code
            //The SSCC 18 barcode is encoded with Code128 symbology (UCC/EAN-128). 
            //A two-digits AI (also called Application Identifier) of '00' is added to the beginning of the barcode. 
            //this.barcodeDisplay.ShowTextOnBottom = true;
            //this.barcodeDisplay.Type = BarCodeType.SSCC18;
            //
            //this.barcodeDisplay.Data = _item;
            //using nbarcode control
            this.barCodeSSCC.Type = NBarCodes.BarCodeType.Code128;
            this.barCodeSSCC.Data = _item + _result;
            set_barcode();
        }
    }

    protected void set_barcode()
    {
        this.barCodeSSCC.Visible = !this.barCodeSSCC.Visible;
    }
    #endregion

    #region form binding
    protected void bind_order_titles()
    {
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        int _user = Session["user"] != null? wwi_func.vint(((UserClass)Session["user"]).UserId.ToString()): 0;
  
        //using db4o
        //this.dxgridTitles.KeyFieldName = "TitleID";
        //this.dxgridTitles.DataSource = db4o_itemtable.SelectByUserSession(_orderno, _user);
        //this.dxgridTitles.DataBind();
        
    }

    protected void dxgridTitles_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        bind_order_titles();
    }

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
    protected void dxcbotitle_DataBound(object sender, EventArgs e)
    {
        ASPxComboBox _combo = (ASPxComboBox)sender;
        if (Session["Title"] != null) { _combo.Text = Session["Title"].ToString(); Session["Title"] = null; }
    }
    #endregion

    #region crud events
    /// <summary>
    /// insert to itemtable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //999909 is agood orderno for testing as has lot of titles
        ASPxGridView _grd = (ASPxGridView)sender;
        int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
        if (_orderno > 0)
        {
            //need to get title from edit itemtemplate it won't be found in NewValues
            string _title = "";
            
            GridViewDataColumn _col = (GridViewDataColumn)_grd.Columns["colTitle"];
            ASPxComboBox _cbo = (ASPxComboBox)_grd.FindEditRowCellTemplateControl(_col, "dxcbotitle");
            if (_cbo != null) {
                _title = _cbo.Text != null? _cbo.Text.ToString(): ""; 
            }
            
            ItemTable _i = new ItemTable();
            _i.OrderNumber = _orderno;
            _i.Title = _title; //e.NewValues["Title"].ToString();
            if (e.NewValues["Copies"] != null) { _i.Copies = wwi_func.vint(e.NewValues["Copies"].ToString()); }
            if (e.NewValues["ISBN"] != null) { _i.Isbn = e.NewValues["ISBN"].ToString(); }
            if (e.NewValues["Impression"] != null) { _i.Impression = e.NewValues["Impression"].ToString(); }
            if (e.NewValues["PONumber"] != null) { _i.PONumber = e.NewValues["PONumber"].ToString(); }
            if (e.NewValues["OtherRef"] != null) { _i.OtherRef = e.NewValues["OtherRef"].ToString(); }
            if (e.NewValues["TotalValue"] != null) { _i.TotalValue = wwi_func.vdecimal(e.NewValues["TotalValue"].ToString()); }
            if (e.NewValues["SSRNo"] != null) { _i.SSRNo = e.NewValues["SSRNo"].ToString(); }
            if (e.NewValues["SSRDate"] != null) { _i.SSRDate = wwi_func.vdatetime(e.NewValues["SSRDate"].ToString()); }
            if (e.NewValues["PerCopy"] != null) { _i.PerCopy = wwi_func.vdouble(e.NewValues["PerCopy"].ToString()); }
            
            //saving to db40 NOT itemtable
            //_i.Save();
            db4o_itemtable.InsertItem(_i); 
        }
        
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit(); 
        //no need to rebind it will happen after callback anyway
        //bind_order_titles(); 
        //string _new = get_token("cno");
        //if (!string.IsNullOrEmpty(_new))
        //{
        //    this.dxlblInfo.Text = "";
        //    this.dxpnlMsg.ClientVisible = false;
        //}
    }
    /// <summary>
    /// update itemtable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridTitles_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        //need to get title from edit itemtemplate it won't be found in NewValues
        string _title = "";
        //when storing in db4o use userid for titleid, as db4o generates its own index and we need to track itesm by user
        int _titleid = wwi_func.vint(((UserClass)Session["user"]).UserId.ToString());   //wwi_func.vint(e.Keys["TitleID"].ToString());
        GridViewDataColumn _col = (GridViewDataColumn)_grd.Columns["colTitle"];
        ASPxComboBox _cbo = (ASPxComboBox)_grd.FindEditRowCellTemplateControl(_col, "dxcbotitle");
        if (_cbo != null)
        {
            _title = _cbo.Text != null ? _cbo.Text.ToString() : "";
        }

        ItemTable _i = new ItemTable();
        _i.Title = _title; //e.NewValues["Title"].ToString();

        if (e.NewValues["Copies"] != null) { _i.Copies = wwi_func.vint(e.NewValues["Copies"].ToString()); }
        if (e.NewValues["ISBN"] != null) { _i.Isbn = e.NewValues["ISBN"].ToString(); }
        if (e.NewValues["Impression"] != null) { _i.Impression = e.NewValues["Impression"].ToString(); }
        if (e.NewValues["PONumber"] != null) { _i.PONumber = e.NewValues["PONumber"].ToString(); }
        if (e.NewValues["OtherRef"] != null) { _i.OtherRef = e.NewValues["OtherRef"].ToString(); }
        if (e.NewValues["TotalValue"] != null) { _i.TotalValue = wwi_func.vdecimal(e.NewValues["TotalValue"].ToString()); }
        if (e.NewValues["SSRNo"] != null) { _i.SSRNo = e.NewValues["SSRNo"].ToString(); }
        if (e.NewValues["SSRDate"] != null) { _i.SSRDate = wwi_func.vdatetime(e.NewValues["SSRDate"].ToString()); }
        if (e.NewValues["PerCopy"] != null) { _i.PerCopy = wwi_func.vdouble(e.NewValues["PerCopy"].ToString()); }

        //updating db40 NOT itemtable
        //_i.Save();
        db4o_itemtable.UpdateItem(_i); 
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //no need to rebind it will happen after callback anyway
        //bind_order_titles(); 
    }


    protected void dxgridTitles_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        //need to get title from edit itemtemplate it won't be found in NewValues
       
        int _titleid = wwi_func.vint(e.Keys["TitleID"].ToString());

        if (_titleid > 0)
        {
            ItemTable.Delete("TitleID", _titleid);     
        }

        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //no need to rebind it will happen after callback anyway
        //bind_order_titles(); 
    }
    #endregion

    #region dll incremental filtering
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
        //ASPxComboBox _combo = (ASPxComboBox)source;
        //ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
        //       this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));

        ////use datareaders - much faster than loading into collections
        //string[] _cols = { "ItemTable.TitleID, ItemTable.Title" };
        ////SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        //SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        //_query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.ItemTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "TitleID").WhereExpression("Title").StartsWith(string.Format("{0}%", e.Filter.ToString()));
        
        //IDataReader _rd = _query.ExecuteReader();
        //_combo.DataSource = _rd;
        //_combo.ValueField = "TitleID";
        //_combo.ValueType = typeof(Int32); 
        //_combo.TextField = "Title";
        //_combo.DataBind();
    }


    protected void dxcbotitle_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        //ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
        //       this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));
        //
        //Int32 _id = 0;
        //if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }
        //
        ////use datareaders - much faster than loading into collections
        //string[] _cols = { "ItemTable.TitleID, ItemTable.Title" };
        //SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        //_query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.ItemTable).WhereExpression("TitleID").IsEqualTo(_id);
        //
        //IDataReader _rd = _query.ExecuteReader();
        //_combo.DataSource = _rd;
        //_combo.ValueField = "TitleID";
        //_combo.ValueType = typeof(Int32); 
        //_combo.TextField = "Title";
        //_combo.DataBind();

    }
    //end incremental filtering of title
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
