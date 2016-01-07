using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DAL.Logistics;

public partial class country_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!isLoggedIn())
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("data_admin/country_search", "publiship"));
        }

        if (!Page.IsPostBack & !Page.IsCallback)
        {
            bind_toolbar();
        }

        bind_data_grid();
    }

    #region databinding
    /// <summary>
    /// simple binding to country table using DAL
    /// </summary>
    protected void bind_data_grid()
    {
        CountryTableController  _con = new CountryTableController();
        DataTable _tbl = _con.FetchAll().ToDataTable();
        this.dxgridSearch.DataSource = _tbl;
        this.dxgridSearch.KeyFieldName = "countryid";
        this.dxgridSearch.DataBind(); 
       
    }
    #endregion

    #region menu binding and events
    protected void bind_toolbar()
    {
        //using generic FormView commands 
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='GenericSearchForm']"; //you need this or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuToolbar.DataSource = _xml;
        this.dxmnuToolbar.DataBind();
        //int _test = this.dxmnuContainer.Items.Count;
    }
    protected void dxmnuSearchTools_DataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        {
            //get path to form
            string _folder = System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath);
            //change to formview page by removing 'search' e.g. container_search.aspx becomes container.aspx
            string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath).Replace("_search","") ;
            //primary key id
            string _id = get_token("pno");
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _folder + "\\" + _page, _id); }
        }
    }
    #endregion

    #region page callbacks
    protected void dxgridSearch_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView _grid = (ASPxGridView)sender;
        string _url = "";
        string[] _args = { "", "", "" };
        //primary key number 
        _args[0] = wwi_security.EncryptString(_grid.GetRowValues(e.VisibleIndex, _grid.KeyFieldName).ToString(),"publiship"); 
        
        switch (e.ButtonID.ToString())
        {
            case "cmdView": //not necessary on this form we can see all in the grid
                {
                    _args[1] = "ReadOnly";
                    _args[2] = "";
                    break;
                }
            case "cmdEdit": //open container form in edit mode
                {
                    _args[1] = "Edit";
                    _args[2] = "";
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
        
        //open container form
        _url = string.Format("../data_admin/country.aspx?pid={0}&mode={1}", _args);
        //can't use response, error= input string not in correct format as can't utilise response during callback
        //Response.Redirect(_url, true);   
        DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_url);
    }
    //end custombutton callback
    /// <summary>
    /// fires after callback update title with filter description
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridSearch_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        string _fx = this.dxgridSearch.FilterExpression.ToString();
        if (!string.IsNullOrEmpty(_fx)) { this.dxgridSearch.SettingsText.Title = _fx; }//this.dxlblSearch.Text = _fx; }

        //yes, but how to use this to clear the grid of data
        //if callbackname = 'APPLYFILTER' and eventarg is empty string CLEAR button has been pressed
        //force grid to rebind with no filter as if page 1st load
        //string _cbn = e.CallbackName.ToString();
        //string _arg = e.Args.Length > 0 ? e.Args[0].ToString() : "";
        //if (_cbn == "APPLYFILTER" && _arg == "")
        //{
        //    Page.Session["SESSIONFILTER"] = "clear";
        //    //this.dxgridContainer.DataBind(); 
        //}
    }
    //end after perform callback
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

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion

    
}
