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


public partial class dimensions : System.Web.UI.Page
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
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("data_admin/country_search", "publiship"));
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
                    this.fmvDimensions.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvDimensions.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvDimensions.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvDimensions.ChangeMode(FormViewMode.ReadOnly);
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
                    update_dimensions();
                    set_mode("ReadOnly");
                    //call databind to change mode
                    bind_formview("ReadOnly");
                    //this.fmvVoyage.UpdateItem(false);
                    //set_mode("view"); not necesary form will revert to read only after save
                    break;
                }
            case "cmdSave":
                {
                    break; //not available on this form
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
                                                "country_search",};
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
    protected void fmvDimensions_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvDimensions.ChangeMode(e.NewMode);
    }

    protected void bind_formview(string mode)
    {
        //have to use a collection as formview needs to bind to enumerable
        DeliverySubSubTableCollection _tbl = new DeliverySubSubTableCollection();
        if (mode != "Insert")
        {
            int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
            DeliverySubSubTable  _ct = new DeliverySubSubTable(_pid);
            _tbl.Add(_ct);
            this.fmvDimensions.DataSource = _tbl;
            this.fmvDimensions.DataBind();
    
        }
        else //there's no insert option on this form it's jsut fer editting book/cartrton dimension
        {
            this.dxlblErr.Text = "This option is not available";
            this.dxpnlErr.ClientVisible = true;
        }
    }

    
    /// <summary>
    /// update hvoyagetable
    /// </summary>
    /// <param name="hblid">int</param>
    protected void update_dimensions()
    {
        //voyageid
        int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
        
        if (_pid > 0)
        {
            try
            {
                //get instance of record
                DeliverySubSubTable _tbl = new DeliverySubSubTable(_pid);
                ASPxTextBox _txt = null;

                //depth
                _txt = (ASPxTextBox)this.fmvDimensions.FindControl("dxtxtDepthEdit");
                if (_txt != null && _txt.Text != "")
                {
                    _tbl.BookDepth = wwi_func.vdecimal(_txt.Text.ToString());
                }

                //length
                _txt = (ASPxTextBox)this.fmvDimensions.FindControl("dxtxtLengthEdit");
                if (_txt != null && _txt.Text != "")
                {
                    _tbl.BookLength = wwi_func.vdecimal(_txt.Text.ToString());
                }

                //weight
                _txt = (ASPxTextBox)this.fmvDimensions.FindControl("dxtxtWeightEdit");
                if (_txt != null && _txt.Text != "")
                {
                    _tbl.BookWeight = wwi_func.vdecimal(_txt.Text.ToString());
                }

                //width
                _txt = (ASPxTextBox)this.fmvDimensions.FindControl("dxtxtWidthEdit");
                if (_txt != null && _txt.Text != "")
                {
                    _tbl.BookWidth = wwi_func.vdecimal(_txt.Text.ToString());
                }

                //update
                _tbl.Save();
                //get values off edit form
                
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

    #region call back
    /// <summary>
    /// call back panel when a country is selected in edit mode
    /// populate edit form with selected details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbpEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _countryid = e.Parameter[0].ToString();
        if (!string.IsNullOrEmpty(_countryid)) {
            this.dxhfOrder.Clear();
            this.dxhfOrder.Add("pid", _countryid);  
            set_mode("Edit"); 
            bind_formview("Edit");  
        }
    }
    #endregion
}
