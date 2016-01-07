using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxSiteMapControl;
using ParameterPasser;

public partial class WWI_m1 : System.Web.UI.MasterPage
{
    /// <summary>
    /// custom role based security 
    /// </summary>
    public class MySiteMapProvider : UnboundSiteMapProvider
    {
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            return IsRolesAccessibleToCurrentUser(node.Roles);
        }
    }

    public static bool IsRolesAccessibleToCurrentUser(IList roles)
    {
        // TODO: Your custom logic here
        if (roles.Contains("editor") && !isEditor()) //have to be both signed in and an editor for this to return true
            return false;
        if (roles.Contains("secure") && !isLoggedIn()) //have to be signed in
            return false;
        if (roles.Contains("intra") && !isIntra())
            return false;
        if (roles.Contains("deliveries") && !seeDeliveries())
            return false;
        
        return true;
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    protected static bool isEditor()
    {
        // check session and if user has editor rights
        if (HttpContext.Current.Session["user"] == null)
            return false;
        
        UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
            if (_thisuser.IsEditor == 0)
                return false;
        
        return true;
    }
    protected static bool isIntra()
    {
        //check session and if user is internal Publiship (CompanyId = -1)
        if (HttpContext.Current.Session["user"] == null)
            return false;

        UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
        if (_thisuser.CompanyId != -1)
            return false;

        return true;
    }
    protected static bool seeDeliveries()
    {
        //check if Pulbiship user (ComapnyId = -1) OR
        //if user can see delivery tracking page by looking up default page in contact_iso.xml
        if (HttpContext.Current.Session["user"] == null)
            return false;

        UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
        if (_thisuser.CompanyId != -1 && wwi_func.lookup_xml_string("\\xml\\contact_iso.xml", "id", _thisuser.UserId.ToString() , "id", "")  == "")
            return false;

        return true;
    }
    //end role security

    protected void Page_Load(object sender, EventArgs e)
    {
        //***********
        //temporarily disabled so we can identify why it seems to be logging out during use
        //this.CheckSessionTimeout();
        
        //***********
        if (!Page.IsPostBack)
        {
                //UserClass.CheckLogin(Page);

                //07/03/2011 not relevant as we do not use default screen
                //get images for slider
                //set_slide_images();
        }
   
        //current date to top right
        this.dxlbldate.Text = DateTime.Today.DayOfWeek.ToString() + ", " + DateTime.Today.ToLongDateString();
        set_page_defaults();
        //check menus
        set_menu_by_role();
    }

    
    protected void set_menu_by_role()
    {
        //custom role based security for main menu
        string _source = Server.MapPath("~/wwiweb1.sitemap"); //this ONLY works if every link in the sitemap is different!
        MySiteMapProvider _pr = new MySiteMapProvider();
        _pr.SiteMapFileName = _source;
        _pr.EnableRoles = true;
        this.dxsitemapdata.Provider = _pr;

    }

    /// <summary>
    /// check current login status from session and set page up as required
    /// </summary>
    protected void set_page_defaults()
    {
        //check child page to see if it has a login button on it! do not display master page login if it does
        //as the login popup will not work for some reason
        DevExpress.Web.ASPxEditors.ASPxButton _btn = (DevExpress.Web.ASPxEditors.ASPxButton)this.ContentPlaceHolderM1.FindControl("btnLogin");
        if (_btn != null)
        {
            this.btnLogout.ClientVisible = false;
            this.btnLogin.ClientVisible = false;
        }
        else if (Page.Session["user"] != null)
        {
            UserClass _thisuser = (UserClass)Session["user"];
            this.lblResult.Text = _thisuser.UserName;
            //this.btnLogin.Text = "LogOut";
            this.btnLogout.ClientVisible = true;
            this.btnLogin.ClientVisible = false;
        }
        else
        {
            Session_Reset_All(); 
        }
    }

    /// <summary>
    /// logout - clear user session from server
    /// set login/logout buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        try
        {

            Session_Reset_All(); 
            ////should we force redirect to end session page (supposed to be hidden)
            ////onunload="window.open('End_Session.aspx',null,'height=100,width=100')

            //forces page to reload therefore clearing any data on page
            //string _startpageurl = ConfigurationManager.AppSettings["order_tracking"];
            //if (_startpageurl != string.Empty) { Response.Redirect(_startpageurl); }
            //
            //Page.ClientScript.RegisterStartupScript(GetType(), "IMG_KEY", "window.setunlockedimg();", true);
            this.Response.Redirect(this.Request.Url.AbsoluteUri, false);
            //


        }
        catch (Exception ex)
        {
            Response.Write("<br/><br/><br/>" + ex.Message.ToString());
        }
    }

    /// <summary>
    /// clear grid of all data, filters and groups
    /// clear session wrappers so previus search params are not retained
    /// </summary>
    protected void Session_Reset_All()
    {

        //remove all filtering/grouping from grid
        ASPxGridView _grid = (ASPxGridView)this.ContentPlaceHolderM1.FindControl("gridOrder");
        if (_grid != null)
        {
            _grid.FilterExpression = null;
            wwi_func.remove_dxgrid_grouping(_grid);
        }

        //Session["user"] = null;
        Session.Remove("user");

        Session.Clear();
        SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        _sessionWrapper["query"] = "(OrderNumber==-1)";
        _sessionWrapper["name"] = null;
        _sessionWrapper["mode"] = null;

        this.lblResult.Text = "You are not signed in";
        this.btnLogout.ClientVisible = false;
        this.btnLogin.ClientVisible = true;


        //kill cookie
        if (Request.Browser.Cookies)
        {
            if (Request.Cookies["UserInfo"] != null)
            {
                HttpCookie _acookie = new HttpCookie("UserInfo");
                _acookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(_acookie);
            }
        }
        //end kill cookie
    }
    //end session reset all

    /// <summary>
    /// call back panel to force update of datagridview on login /timer?
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void callBack1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
            //if (!sessionTimedOut())
            //{
            ASPxGridView _grid = (ASPxGridView)this.ContentPlaceHolderM1.FindControl("gridOrder");
            if (_grid != null) { _grid.DataBind(); }
            //}
            //else
            //{
            //    Page.Response.Redirect("Session_Sys_Login.aspx", true);
            //}
    }

    protected void set_slide_images()
    {
        //this.eoslidGraphic.DataSource = new string[]
        //{
        //    "~/Images/slides_1.jpg",
        //    "~/Images/slides_2.jpg",
        //    "~/Images/slides_3.jpg",
        //};

        //this.eoslidGraphic.DataSource = CreateDataSource();
        //this.eoslidGraphic.DataBind();

    }

    private DataTable CreateDataSource()
    {
        DataTable table = new DataTable();
        table.Columns.Add("image", typeof(string));
        table.Columns.Add("thumbnail", typeof(string));
        table.Columns.Add("title", typeof(string));
        table.Columns.Add("summary", typeof(string));
        table.Columns.Add("link_text", typeof(string));

        table.Rows.Add(
            "~/Images/slides_1.jpg",
            "~/Images/thumbs_1.jpg",
            "Logistics",
            "Specialist logistics provider to the printing and publishing industry.",
            "Lostistics");
        table.Rows.Add(
             "~/Images/slides_2.jpg",
            "~/Images/thumbs_2.jpg",
            "Service",
            "Ensure your product arrives in the right place, on time, at the right price.",
            "Service");
        table.Rows.Add(
             "~/Images/slides_3.jpg",
            "~/Images/thumbs_3.jpg",
            "Offices",
            "Publiship has a network of offices, strategically located in the world's main printing and publishing centres.",
            "Offices");

        return table;
    }

    /// <summary>
    /// script to time page out just before session timeout when user is signed in
    /// </summary>
    private void CheckSessionTimeout()
    {
        string _script = "";
        
        DevExpress.Web.ASPxEditors.ASPxLabel mpLabel = (DevExpress.Web.ASPxEditors.ASPxLabel)this.FindControl("lblResult");

        if (mpLabel != null)
        {
                _script = string.Format(@"function verify_user(){{
                    var us = document.getElementById('{0}').innerHTML; 
                    return us;
                    }};", mpLabel.ClientID);
        }
            
        //time to redirect, 5 milliseconds before session ends
        string _t = this.Session.Timeout.ToString();
        string _pagename = System.IO.Path.GetFileName(Request.Path);
        string str_Script = "";
        int _MilliSecondsTimeOut = (this.Session.Timeout * 60000) - 5;

        //141212 no auto logout pages (Publiship users only)
        if (isIntra()) // && _pagename == "Crm_Customer_Target.aspx")
        {
            str_Script = "";
        }
        else
        {
            str_Script = _script + @"
                var myTimeOut; +
                clearTimeout(myTimeOut); " +
                    "var sessionTimeout = " + _MilliSecondsTimeOut.ToString() + ";" +
                    "function doRedirect(){ var verify=verify_user();  if(verify != 'You are not signed in') {window.location.href='~/user_accounts/signin.aspx'}else{window.location.reload(true)}; }" + @"
                myTimeOut=setTimeout('doRedirect()', sessionTimeout); ";
        }
        
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(),
                  "CheckSessionOut", str_Script, true);
        //}
    }
    //end script

    /// <summary>
    /// call back for main menu e.g. after user has signed in
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbmenu_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        set_menu_by_role();
    }
    /// <summary>
    /// redirect to login page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user_accounts/signin.aspx?&rp=" + wwi_security.EncryptString("tracking/order_tracking","publiship"));  
    }
    protected void dxmenumain_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        //open in a new window (e.g. for onedrive account)
        if(e.Item.NavigateUrl.StartsWith("https://")){e.Item.Target = "_blank";} 
    }
   
}
