using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using ParameterPasser;

public partial class Sys_Login : System.Web.UI.Page
{
    /// <summary>
    /// check user session
    /// if user session is poplulated redirect to correct page
    /// else
    /// wait for user to login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        //Put user code to initialize the page here
        Label lm = this.FindControl("lblMessage") as Label;
        if (lm != null) lm.Text = "";

        if (!Page.IsPostBack)
        {

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["loginmethod"]) || (ConfigurationManager.AppSettings["loginmethod"]).ToUpper() == "WITHOUTLOGIN")
            {
                Redirect(false);
                return;
            }
            if (Request.QueryString["user"] != null)
            {
                txtUserName.Text = Request.QueryString["user"];
                txtPassword.Attributes.Add("value", "");
            }
            else
            {
                if (Request.Browser.Cookies)
                {
                    if (Request.Cookies["user"] != null)
                    {
                        txtUserName.Text = Request.Cookies["user"]["_userlogin"];
                        txtPassword.Attributes.Add("value", wwi_security.DecryptString(Request.Cookies["user"]["UserPwd"],"publiship"));
                        chbSavePassword.Checked = false;
                    }
                    chbSavePassword.Visible = true;
                }
                else
                {
                    chbSavePassword.Visible = false;
                }
            }
            Session.Clear();
        }
        else
        {
            if (Page.Session["user"] != null)
            {
                Redirect(true);

            }
            else
            {
                if (txtUserName.Text == "") Page.SetFocus(txtUserName);
                else Page.SetFocus(txtPassword);
            }
        }

    }

    /// <summary>
    /// check user name/password combination 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdLogin_Click(object sender, System.EventArgs e)
    {
        
        UserClass _userlogin = new UserClass();
        HttpCookie _acookie = new HttpCookie("user");

        try
        {
            string _username = txtUserName.Text.Replace("'", "''");
            string _pass = wwi_security.EncryptString(txtPassword.Text.Replace("'", "''"),"publiship");

            _userlogin = _userlogin.Login(_username, _pass);

            if (_userlogin != null && _userlogin.ID != Guid.Empty)
            {
               
                Session["user"] = _userlogin;
                
                if (Request.Browser.Cookies)
                {
                    if (chbSavePassword.Checked)
                    {
                        _acookie["userlogin"] = txtUserName.Text;
                        _acookie["userpwd"] = _pass; //txtPassword.Text;
                        //expires  after 1 year
                        _acookie.Expires = DateTime.Now.AddYears(1);
                    }
                    else
                    {
                        //expires  midnight
                        //DateTime _dt = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
                        
                        //expires in 1 hour
                        DateTime _dt = DateTime.Now.AddHours(1);  //DateTime.Now + new TimeSpan(1, 0, 0);
                        _acookie.Expires = _dt;
                    }
                    Response.Cookies.Add(_acookie);
                }
            }
            else
            {
                if (_userlogin == null)
                {
                    //this.lblMsg.Text = "<div class='fberrorbox'>Null login</div>";
                }
                else if (_userlogin.ID == Guid.Empty)
                {
                    //this.lblMsg.Text = "<div class='fberrorbox'>No guid</div>";
                }
                else
                {
                    //this.lblMsg.Text = "<div class='fberrorbox'>Invalid Login</div>";
                }
                _userlogin = null;
                Session.Remove("user");
            }
        }
        catch
        {
            _userlogin = null;
            Session.Remove("user");
            //this.lblMsg.Text = "<div class='fberrorbox'>Invalid Login</div>";
        }
        if (_userlogin != null)
        {
            Redirect(true);
           

        }
        else
        {

            this.lblmsg.Visible = true;
        }
     }

    /// <summary>
    /// this version of redirect just forces the login to close
    /// </summary>
    protected void Redirect(bool loggedIn)
    {
        try
        {
            if (loggedIn)
            {
                UserClass _thisuser = (UserClass)Session["user"];
                
                //this.txtUserName.Text = _thisuser.UserName;

                //save login history
                append_to_user_log(_thisuser);
                
                //290311 REQUIRED FOR FORMS AUTHENTICATION
                //30/03/2011 for forms authentication
                //System.Web.Security.FormsAuthentication.SetAuthCookie(txtUserName.Text, false); 
         
                //show logout
                //ClientScript.RegisterStartupScript(GetType(), "LOG_KEY", "window.parent.loginSwitch();", true);
                //
                //09/03/2011 reset submit_query -1 which should cause default filter to fire therefore not auto-loading data

                //27/06/2011 don't fire this script for ubmit_query(-1) as it means we can only use this login form with search forms e.g. ord_view_tracking.aspx
                //set session wrapper instead
                SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
                _sessionWrapper["mode"] = "-1";
                //ClientScript.RegisterStartupScript(GetType(), "CLR_KEY", "window.parent.submit_query(-1);", true);
                //pass user name back to parent
                ClientScript.RegisterStartupScript(GetType(), "LBL_KEY", "window.parent.lblResult.SetText(txtResult.GetText());", true);
                //09/08/2011 re-initialise menu
                ClientScript.RegisterStartupScript(GetType(), "LBL_MNU", "window.parent.setMenu();", true);
                //close login  
                ClientScript.RegisterStartupScript(GetType(), "INN_KEY", "window.parent.closeloginWindow(1);", true); 
           
            }
            else
            {
                //close this window and redirect to default non-secure page
                //29/06/2011 don't use script as we are going to redirect anyway
                ClientScript.RegisterStartupScript(GetType(), "OUT_KEY", "window.parent.cancelloginWindow();", true);
                //force popup to close
                }
            
        }
        catch (Exception ex)
        {
            this.lblmsg.Text = ex.Message.ToString(); 
        }
    }

    protected void append_to_user_log(UserClass thisuser)
    {
        try
        {
            UserLog _newlog = new UserLog();

            if (thisuser.CompanyId == -1)
            { //employee
                _newlog.ContactID = 0;
                _newlog.EmployeeID = thisuser.UserId;
            }
            else
            {
                _newlog.ContactID = thisuser.UserId;
                _newlog.EmployeeID = 0;
            }

            _newlog.LogDate = DateTime.Now;  
            _newlog.Save(); 

        }
        catch(Exception ex)
        {
            Response.Write(ex.Message.ToString()); 
        }
    }

    /// <summary>
    /// redirct to preferred url
    /// </summary>
    protected void RedirectToURL()
    {
        string _startpageurl = "";

        _startpageurl = ConfigurationManager.AppSettings["order_tracking"];
        //clear login state
        Session["loginstate"] = "";
        Response.Redirect(_startpageurl);

            //redirect phone support straight to "add new worksheet"
            //if (Session["user"] != null)
            //{
            //    
            //    UserClass _thisuser = (UserClass)Session["user"];
            //
            //    switch (_thisuser.DefaultForm)
            //    {
            //        case "ordertracking": //lowest level for client log in
            //            { _startpageurl = ConfigurationManager.AppSettings["order_tracking"]; }
            //            break;
            //        //    case 1: //resticted access e.g. phone support
            //        //        { _startpageurl = ConfigurationManager.AppSettings["StartPage_L1"]; }
            //        //        break;
            //        //    case 2: //standard users e.g. engineers
            //        //        { _startpageurl = ConfigurationManager.AppSettings["StartPage_L2"]; }
            //        //        break;
            //        //    case 3: //management level users
            //        //        { _startpageurl = ConfigurationManager.AppSettings["StartPage_L3"]; }
            //        //        break;
            //        default: //other users default back to login as they can't have a group id!
            //            { _startpageurl = ConfigurationManager.AppSettings["start_page"]; }
            //            break;
            //    }
            //}
            //else
            //{
            //    _startpageurl = Request.QueryString["url"];
            //}
            //if (_startpageurl == "")
            //{
            //    Response.Write("<script language=javascript>alert('" + "Start page isn't set" + "');</script>");
            //}
            //else
            //{
            //    //clear login state
            //    Session["loginstate"] = "";
            //    Response.Redirect(_startpageurl);
            //}
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //close login form and set up login/logout butons
        Redirect(false); 
    }

    //returns the IP address of the user requesting a page
    protected string GetRequestingIP()
    {
        string strIpAddress;

        strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (strIpAddress == null)

            strIpAddress = Request.ServerVariables["REMOTE_ADDR"];


        return strIpAddress;
    }
}
