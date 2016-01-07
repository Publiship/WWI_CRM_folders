using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;


public partial class order_titles : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            bind_order_titles();
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
        //have we redirected from new order?
        string _new = get_token("cno");
        if (!string.IsNullOrEmpty(_new))
        {
            this.dxlblInfo.Text = "Please add titles to this order (Click the + button or 'Add title' button";
            this.dxpnlMsg.ClientVisible = true;

        } 

        bind_summary();
        bind_tabs();
        bind_commands();
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

    #region form binding
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
            if (!string.IsNullOrEmpty(this.dxtabOrder.Tabs[_ix].NavigateUrl)) { this.dxtabOrder.Tabs[_ix].NavigateUrl = string.Format(this.dxtabOrder.Tabs[_ix].NavigateUrl,_orderid , _orderno); }
        }

    }

    /// <summary>
    /// command menu for courier details we only need a New option which we can then call through javascript
    /// so the courier xml item deos not contain a navigate url
    /// </summary>
    protected void bind_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='Titles']"; //you need xpath or tab will not databind!
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
            string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
            string _id = get_token("pno");
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        }
    }

    protected void bind_order_titles()
    {
        //get order number from querystrings
        Int32 _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"),"publiship")); //999909; //Request.QueryString["pid"] != null ? wwi_func.vint(Request.QueryString["pid"].ToString()) : 0;
        
        //query item table
        SubSonic.Query _q = new SubSonic.Query(DAL.Logistics.Tables.ItemTable, "WWIProv").WHERE("OrderNumber", Comparison.Equals, _orderno);
        DataSet _ds = _q.ExecuteDataSet();
        this.dxgridTitles.KeyFieldName = "TitleID";
        this.dxgridTitles.DataSource = _ds;
        this.dxgridTitles.DataBind(); 
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

    protected void bind_summary()
    {
        try
        {
            int _rowcount = 0;
            int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
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
            //int _titleid = 0;

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
            
            _i.Save();
        }
        
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit(); 
        //no need to rebind it will happen after callback anyway
        //bind_order_titles(); 
        string _new = get_token("cno");
        if (!string.IsNullOrEmpty(_new))
        {
            this.dxlblInfo.Text = "";
            this.dxpnlMsg.ClientVisible = false;

        }
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
        int _titleid = wwi_func.vint(e.Keys["TitleID"].ToString());
        GridViewDataColumn _col = (GridViewDataColumn)_grd.Columns["colTitle"];
        ASPxComboBox _cbo = (ASPxComboBox)_grd.FindEditRowCellTemplateControl(_col, "dxcbotitle");
        if (_cbo != null)
        {
            _title = _cbo.Text != null ? _cbo.Text.ToString() : "";
        }

        ItemTable _i = new ItemTable(_titleid);
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
           
        _i.Save();

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
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
               this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));

        //use datareaders - much faster than loading into collections
        string[] _cols = { "ItemTable.TitleID, ItemTable.Title" };
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.ItemTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "TitleID").WhereExpression("Title").StartsWith(string.Format("{0}%", e.Filter.ToString()));
        
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "TitleID";
        _combo.ValueType = typeof(Int32); 
        _combo.TextField = "Title";
        _combo.DataBind();
    }


    protected void dxcbotitle_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgridTitles.FindEditRowCellTemplateControl(
               this.dxgridTitles.Columns["colTitle"] as GridViewDataComboBoxColumn, "dxcbotitle"));

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
