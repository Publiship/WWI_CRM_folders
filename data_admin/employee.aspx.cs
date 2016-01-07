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


public partial class employee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for testing only *****
        //get primary key
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
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("data_admin/employee_search", "publiship"));
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
                    this.fmvEmployee.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvEmployee.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvEmployee.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvEmployee.ChangeMode(FormViewMode.ReadOnly);
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
                                                "employee_search",};
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
    protected void fmvEmployee_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvEmployee.ChangeMode(e.NewMode);
    }

    protected void bind_formview(string mode)
    {
        //have to use a collection as formview needs to bind to enumerable
        EmployeesTableCollection _tbl = new EmployeesTableCollection();
        if (mode != "Insert")
        {
            int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

            EmployeesTable _ct = new EmployeesTable(_pid);
            _tbl.Add(_ct);

            //store original value for name so we can check against database when saving
            if (this.dxhfOrder.Contains("oldvalue")) { this.dxhfOrder.Remove("oldvalue"); }
            this.dxhfOrder.Add("oldvalue", _ct.Name); 
        }
        else
        {
            EmployeesTable _ct = new EmployeesTable();
            _tbl.Add(_ct);

           
        }

        this.fmvEmployee.DataSource = _tbl;
        this.fmvEmployee.DataBind();
    }

    protected void fmvEmployee_DataBound(object sender, EventArgs e)
    {
        if (this.fmvEmployee.CurrentMode == FormViewMode.ReadOnly)
        {
            ASPxLabel _lbl = null;

            //get office and department
            _lbl = (ASPxLabel)this.fmvEmployee.FindControl("dxlblOffice");
            if (_lbl != null)
            {
                if (_lbl.Value != null)
                {
                    _lbl.Text = wwi_func.lookup_value("OfficeName", "OfficeTable", "OfficeID", wwi_func.vint(_lbl.Value.ToString()));

                }
            }

            _lbl = (ASPxLabel)this.fmvEmployee.FindControl("dxlblDepartment");
            if (_lbl != null)
            {
                if (_lbl.Value != null)
                {
                    _lbl.Text = wwi_func.lookup_value("Department", "Departments", "DepartmentID", wwi_func.vint(_lbl.Value.ToString()));

                }
            }
        }
        else
        { 
            //drop downs
            bind_office(); //from office table in database
            bind_department(); //from departments table in database 
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
        //original value for countryname
        string _oldvalue = this.dxhfOrder.Contains("oldvalue") ? this.dxhfOrder.Get("oldvalue").ToString() : "";

        if (_pid > 0)
        {
            try
            {
                int? _intnull = null;
                //new instance of record
                EmployeesTable _tbl = new EmployeesTable(_pid);

                //get values off insert form
                //check duplicate name
                ASPxTextBox _txt = (ASPxTextBox)this.fmvEmployee.FindControl("dxtxtEmployeeEdit");
                if (_txt != null && _txt.Text != "")
                {
                    string _newvalue = _txt.Text.Trim().ToString(); //country name
                    bool _duplicate = _newvalue != _oldvalue ? wwi_func.value_exists("Name", "EmployeesTable", _newvalue) : false;

                    if (!_duplicate)
                    {
                        //name
                        _tbl.Name = _txt.Text.ToString();
                        //email
                        _txt = (ASPxTextBox)this.fmvEmployee.FindControl("dxtxtEmailEdit");
                        if (_txt != null) { _tbl.EmailAddress = _txt.Text.ToString(); }
                        //office
                        ASPxComboBox _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboOfficeID");
                        if (_cbo != null) { _tbl.OfficeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                        //department
                        _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboDepartmentID");
                        if (_cbo != null) { _tbl.DepartmentID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                        //live
                        ASPxCheckBox _ck = (ASPxCheckBox)this.fmvEmployee.FindControl("dxckLiveEdit");
                        if (_ck != null) { _tbl.Live = _ck.Checked; }

                        _tbl.Save();
                    }//end if
                    else
                    {
                        string _ex = string.Format("{0} is already in database. This record will not be saved", _newvalue);
                        this.dxlblErr.Text = _ex;
                        this.dxpnlErr.ClientVisible = true;
                    }//end if duplicate
                }//end if text
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
        //original value for countryname
        string _oldvalue = this.dxhfOrder.Contains("oldvalue") ? this.dxhfOrder.Get("oldvalue").ToString() : "";

        try
        {
            int? _intnull = null;
            //new instance of record
            EmployeesTable _tbl = new EmployeesTable();

            //get values off insert form
            //check duplicate name
            ASPxTextBox _txt = (ASPxTextBox)this.fmvEmployee.FindControl("dxtxtEmployeeInsert");
            if (_txt != null && _txt.Text != "")
            {
                string _newvalue = _txt.Text.Trim().ToString(); //country name
                bool _duplicate = _newvalue != _oldvalue ? wwi_func.value_exists("Name", "EmployeesTable", _newvalue) : false;

                if (!_duplicate)
                {
                    //name
                    _tbl.Name = _txt.Text.ToString();
                    //email
                    _txt = (ASPxTextBox)this.fmvEmployee.FindControl("dxtxtEmailInsert");
                    if (_txt != null) { _tbl.EmailAddress = _txt.Text.ToString(); }
                    //office
                    ASPxComboBox _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboOfficeID");
                    if (_cbo != null) { _tbl.OfficeID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                    //department
                    _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboDepartmentID");
                    if (_cbo != null) { _tbl.DepartmentID = _cbo.Value != null ? wwi_func.vint(_cbo.Value.ToString()) : _intnull; }
                    //live
                    ASPxCheckBox _ck = (ASPxCheckBox)this.fmvEmployee.FindControl("dxckLiveInsert");
                    if (_ck != null) { _tbl.Live = _ck.Checked; }

                    _tbl.Save();
                    //get new id
                    _newid = (int)_tbl.GetPrimaryKeyValue();
                }//end if
                else
                {
                    string _ex = string.Format("{0} is already in database. This record will not be saved", _newvalue);
                    this.dxlblErr.Text = _ex;
                    this.dxpnlErr.ClientVisible = true;
                }//end if duplicate
            }//end if text
            
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
    protected void bind_office()
    {
        ASPxComboBox _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboOfficeID");
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
    //end bind office
    protected void bind_department()
    {
        ASPxComboBox _cbo = (ASPxComboBox)this.fmvEmployee.FindControl("dxcboDepartmentID");
        if (_cbo != null)
        {
            string[] _cols = { "Departments.DepartmentID, Departments.Department" };
            string[] _order = { "Department" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.Department).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _cbo.DataSource = _rd1;
            _cbo.ValueField = "DepartmentID";
            _cbo.ValueType = typeof(int);
            _cbo.TextField = "Department";
            _cbo.DataBindItems();
        }
    }
    //end bind departments
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
