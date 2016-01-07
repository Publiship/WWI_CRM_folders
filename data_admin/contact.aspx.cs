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


public partial class contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for testing only *****
        //get primary key CountryID
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
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("data_admin/contact_search", "publiship"));
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
    /// on menu databound modify navigate urls to current page and primary key (pid) 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxmnuFormView_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        //don't set navigateurl it prevents menu click event from firing
        e.Item.NavigateUrl = "";

        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        //{
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pid");
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
                    this.fmvContact.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvContact.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvContact.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvContact.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
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
                    update_contact();
                    set_mode("ReadOnly");
                    //call databind to change mode
                    bind_formview("ReadOnly");
                    //this.fmvVoyage.UpdateItem(false);
                    //set_mode("view"); not necesary form will revert to read only after save
                    break;
                }
            case "cmdSave":
                {
                    this.dxhfOrder.Clear();
                    int _newid = insert_contact();
                    if (_newid > 0)
                    {
                        //set_mode("ReadOnly");
                        //call databind to change mode
                        //bind_formview("ReadOnly");
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

                    //this.fmvVoyage.InsertItem(false);
                    //set_mode("view"); not necesary form will revert to read only after save
                    break;
                }
            case "cmdCancel":
                {
                    set_mode("ReadOnly");
                    bind_formview("ReadOnly");
                    break;
                }
            case "cmdClose":
                {
                    string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "contact_search",};
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
        }//end menu names loop
    }
    #endregion

    #region formview crud events
    protected void fmvContact_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvContact.ChangeMode(e.NewMode);
    }

    protected void bind_formview(string mode)
    {
        //have to use a collection as formview needs to bind to enumerable
        ContactTableCollection _tbl = new ContactTableCollection();
        if (mode != "Insert")
        {
            int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

            ContactTable _ct = new ContactTable(_pid);
            _tbl.Add(_ct);

           
        }
        else
        {
            ContactTable _ct = new ContactTable();
            _tbl.Add(_ct);
        }

        this.fmvContact.DataSource = _tbl;
        this.fmvContact.DataBind();
    }

    protected void fmvContact_DataBound(object sender, EventArgs e)
    {
        if (this.fmvContact.CurrentMode == FormViewMode.ReadOnly)
        {
            ASPxLabel _lbl = null;
            //get company, controlling office, permission level when in view mode
            _lbl = (ASPxLabel)this.fmvContact.FindControl("dxlblCompanyView");
            if (_lbl != null)
            {
                if (_lbl.Value != null)
                {
                    _lbl.Text = wwi_func.lookup_multi_values("CompanyName,Address1,Address2,Address3,CountryName,TelNo", "view_delivery_address", "CompanyID", wwi_func.vint(_lbl.Value.ToString()));
                }
            }

            _lbl = (ASPxLabel)this.fmvContact.FindControl("dxlblControllingOfficeView");
            if (_lbl != null)
            {
                if (_lbl.Value != null)
                {
                    _lbl.Text = wwi_func.lookup_value("OfficeName", "OfficeTable", "OfficeID", wwi_func.vint(_lbl.Value.ToString()));

                }
            }
            _lbl = (ASPxLabel)this.fmvContact.FindControl("dxlblPermissionView");
            if (_lbl != null)
            {
                if (_lbl.Value != null)
                {
                    //don't need the full path for function
                    //string _path = AppDomain.CurrentDomain.BaseDirectory;
                    //    _path += "xml\\ddl_items.xml";
                    string _path = "xml\\ddl_items.xml";
                    _lbl.Text = wwi_func.lookup_xml_string(_path, "ddls", "contact_permission", "value", _lbl.Value.ToString(), "name");
                }
            }
        }
        else
       {
            //bind lookups, comapny address is incrementally filtered
            bind_permission_levels(); //from xml ddl_tems
            bind_controlling_office(); //from office table in database

            //address sub deeck
            string _s = "";
            ASPxComboBox _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboCompany");
            if (_cbo != null && _cbo.SelectedItem != null && _cbo.Value != null)
            {
                if (_cbo.SelectedItem.GetValue("Address1") != null) { _s = _s + (string)_cbo.SelectedItem.GetValue("Address1").ToString(); } //(string)_cbo.SelectedItem.Value.ToString();
                _s += Environment.NewLine;
                if (_cbo.SelectedItem.GetValue("Address2") != null) { _s = _s + (string)_cbo.SelectedItem.GetValue("Address2").ToString(); }
                _s += Environment.NewLine;
                if (_cbo.SelectedItem.GetValue("Address3") != null) { _s = _s + (string)_cbo.SelectedItem.GetValue("Address3").ToString(); }
                _s += Environment.NewLine;
                if (_cbo.SelectedItem.GetValue("CountryName") != null) { _s = _s + (string)_cbo.SelectedItem.GetValue("CountryName").ToString(); }
                _s += Environment.NewLine;
                if (_cbo.SelectedItem.GetValue("TelNo") != null) { _s = _s + (string)_cbo.SelectedItem.GetValue("TelNo").ToString(); }
                _s += Environment.NewLine;

            }
            ASPxLabel _lblCompany = (ASPxLabel)this.fmvContact.FindControl("dxlblCompanyAddress");
            if (_lblCompany != null) { _lblCompany.Text = _s; }

        }
    }
    /// <summary>
    /// update Place table
    /// </summary>
    /// <param name="hblid">int</param>
    protected void update_contact()
    {
        //id
        int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
        
        if (_pid > 0)
        {
            try
            {
                int? _intnull = null;
                //new instance of record
                ContactTable _tbl = new ContactTable(_pid);

                //get values off insert form
                //check duplicate name
                ASPxTextBox _txt = (ASPxTextBox)this.fmvContact.FindControl("txtContactEdit");
                if (_txt != null && _txt.Text != "")
                {
                    //name
                    _tbl.ContactName = _txt.Text.ToString();
                    //email
                    _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtEmailEdit");
                    if (_txt != null) { _tbl.EMail = _txt.Text.ToString(); }
                    //company
                    ASPxComboBox _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboCompany");
                    if(_cbo != null){_tbl.CompanyID = _cbo.Value != null? wwi_func.vint(_cbo.Value.ToString()): _intnull;  }
                    //controlling office
                    _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboOffice");
                    if (_cbo != null) { _tbl.ControllingOfficeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                    //order acknowledgement
                    ASPxCheckBox _ck = (ASPxCheckBox)this.fmvContact.FindControl("dxckOrderAckEdit");
                    if (_ck != null) { _tbl.OrderAck = _ck.Checked; }

                    //web site log in
                    //livet
                    _ck = (ASPxCheckBox)this.fmvContact.FindControl("dxckLiveEdit");
                    if (_ck != null) { _tbl.OrderAck = _ck.Checked; }
                    //log-in name
                    _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtLoginEdit");
                    if (_txt != null) { _tbl.Name = _txt.Text.ToString(); }
                    //password
                    _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtPassEdit");
                    if (_txt != null) { _tbl.Password = _txt.Text.ToString(); }
                    //permission
                    _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboPermission");
                    if (_cbo != null) { _tbl.Permission = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

                    _tbl.Save(); 
                }
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
            string _ex = "Can't update record Contact ID = 0";
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// new record
    /// </summary>
    protected int insert_contact()
    {
        int _newid = 0;

        try
        {
            ///new instance of record
            ContactTable _tbl = new ContactTable();

            int? _intnull = null;
            
            //get values off insert form
            //check duplicate name
            ASPxTextBox _txt = (ASPxTextBox)this.fmvContact.FindControl("txtContactInsert");
            if (_txt != null && _txt.Text != "")
            {
                //name
                _tbl.ContactName = _txt.Text.ToString();
                //email
                _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtEmailInsert");
                if (_txt != null) { _tbl.EMail = _txt.Text.ToString(); }
                //company
                ASPxComboBox _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboCompany");
                if (_cbo != null) { _tbl.CompanyID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                //controlling office
                _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboOffice");
                if (_cbo != null) { _tbl.ControllingOfficeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                //order acknowledgement
                ASPxCheckBox _ck = (ASPxCheckBox)this.fmvContact.FindControl("dxckOrderAckInsert");
                if (_ck != null) { _tbl.OrderAck = _ck.Checked; }

                //web site log in
                //live
                _ck = (ASPxCheckBox)this.fmvContact.FindControl("dxckLiveInsert");
                if (_ck != null) { _tbl.OrderAck = _ck.Checked; }
                //log-in name
                _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtLoginInsert");
                if (_txt != null) { _tbl.Name = _txt.Text.ToString(); }
                //password
                _txt = (ASPxTextBox)this.fmvContact.FindControl("dxtxtPassInsert");
                if (_txt != null) { _tbl.Password = _txt.Text.ToString(); }
                //permission
                _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboPermission");
                if (_cbo != null) { _tbl.Permission = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }

                _tbl.Save();
                //get new id
                _newid = (int)_tbl.GetPrimaryKeyValue();
            }
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

    #region dll binding
    protected void bind_permission_levels()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\ddl_items.xml";

        // pass _qryFilter to have keyword-filter RSS Feed
        // i.e. _qryFilter = XML -> entries with XML will be returned
        DataSet _ds = new DataSet();
        _ds.ReadXml(_path);
        DataView _dv = _ds.Tables[0].DefaultView;
        _dv.RowFilter = "ddls ='contact_permission'";

        ASPxComboBox _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboPermission");
        if (_cbo != null) {
            _cbo.DataSource = _dv;
            _cbo.ValueType = typeof(int);
            _cbo.TextField = "name";
            _cbo.ValueField = "value";
            _cbo.DataBindItems(); //use datbibditems rather than databind when binding to dataview
        }
    }
    //end permission levels

    protected void bind_controlling_office()
    {
        ASPxComboBox _cbo = (ASPxComboBox)this.fmvContact.FindControl("dxcboOffice");
        if (_cbo != null) {
            string[] _cols = { "OfficeTable.OfficeID, OfficeTable.OfficeName", "OfficeTable.CountryID" };
            string[] _order = { "OfficeName" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.OfficeTable).OrderAsc(_order); 

            IDataReader _rd1 = _qry.ExecuteReader();
            _cbo.DataSource = _rd1;
            _cbo.ValueField = "OfficeID";
            _cbo.ValueType = typeof(int);
            _cbo.TextField = "OfficeName";
            _cbo.DataBindItems();
        }
    }
    #endregion

    #region incremental filtering for large combobox datasets
    //14/07/14 dxcboVesselID_ItemRequestedByValue and dxcboVesselID_ItemsRequestedByFilterCondition DEPRECATED 
    //can't use OnItemsRequestedByFilterCondition and OnItemRequestedByValue on this combo as server-side filtring makes the search case sensitive
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
