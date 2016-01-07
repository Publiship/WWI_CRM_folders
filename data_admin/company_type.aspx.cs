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


public partial class company_type : System.Web.UI.Page
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
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("data_admin/company_type_search", "publiship"));
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
                    this.fmvCompanyType.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvCompanyType.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvCompanyType.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvCompanyType.ChangeMode(FormViewMode.ReadOnly);
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
                    update_company_type();
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
                    int _newid = insert_company_type();
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
                                                "company_type_search",};
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
    protected void fmvCompanyType_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvCompanyType.ChangeMode(e.NewMode);
    }

    protected void bind_formview(string mode)
    {
        //have to use a collection as formview needs to bind to enumerable
        TypeTableCollection _tbl = new TypeTableCollection();
        if (mode != "Insert")
        {
            int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
            TypeTable _ct = new TypeTable(_pid);
            _tbl.Add(_ct);

            //store original value for company type name so we can check against database when saving
            if (this.dxhfOrder.Contains("oldvalue")) { this.dxhfOrder.Remove("oldvalue"); }
            this.dxhfOrder.Add("oldvalue", _ct.TypeName); 
        }
        else
        {
            TypeTable _ct = new TypeTable();
            _tbl.Add(_ct);
        }

        this.fmvCompanyType.DataSource = _tbl;
        this.fmvCompanyType.DataBind();
    }

    
    /// <summary>
    /// update Place table
    /// </summary>
    /// <param name="hblid">int</param>
    protected void update_company_type()
    {
        //voyageid
        int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
        //original value for name
        string _oldvalue = this.dxhfOrder.Contains("oldvalue") ? this.dxhfOrder.Get("oldvalue").ToString() : "";
   
        if (_pid > 0)
        {
            try
            {
                //new instance of record
                TypeTable _tbl = new TypeTable(_pid);

                //get values off insert form
                //check duplicate name
                ASPxTextBox _txt = (ASPxTextBox)this.fmvCompanyType.FindControl("dxtxtTypeNameEdit");
                if (_txt != null && _txt.Text != "")
                {  
                    string _newvalue = _txt.Text.Trim().ToString(); //country name
                    bool _duplicate = _newvalue != _oldvalue ? wwi_func.value_exists("TypeName", "TypeTable", _newvalue) : false;

                    if (!_duplicate)
                    {
                        _tbl.TypeName  = _newvalue;
                        
                        //update
                        _tbl.Save();
                    }
                    else
                    {
                        string _ex = string.Format("{0} is already in database. This record will not be saved", _newvalue);
                        this.dxlblErr.Text = _ex;
                        this.dxpnlErr.ClientVisible = true;
                    }
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
            string _ex = "Can't update record ID = 0";
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// new record
    /// </summary>
    protected int insert_company_type()
    {
        int _newid = 0;

        try
        {
            ///new instance of record
            TypeTable _tbl = new TypeTable();
            
            //get values off insert form
            //check for duplicate name
            ASPxTextBox _txt = (ASPxTextBox)this.fmvCompanyType.FindControl("dxtxtTypeNameInsert");
            if (_txt != null && _txt.Text != "")
            {
                string _newvalue = _txt.Text.Trim().ToString(); //country name

                if (!wwi_func.value_exists("TypeName", "TypeTable", _newvalue))
                {
                    _tbl.TypeName = _txt.Text.Trim().ToString();

                    //insert
                    _tbl.Save();
                    //get new id
                    _newid = (int)_tbl.GetPrimaryKeyValue();
                }
                else
                {
                    string _ex = string.Format("{0} is already in database. This record will not be saved", _newvalue);
                    this.dxlblErr.Text = _ex;
                    this.dxpnlErr.ClientVisible = true;
                }
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
