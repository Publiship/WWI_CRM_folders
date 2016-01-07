using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;

public partial class signin : System.Web.UI.Page
{
    private string reqpage = "";

    /// <summary>
    /// check user session
    /// if user session is populated redirect to correct page
    /// else
    /// wait for user to login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //make sure session data is cleared
        Session.Remove("user");
        this.dxpnlmsg.Visible = false;

        //Put user code to initialize the page here
        Label lm = this.FindControl("lblMessage") as Label;
        if (lm != null) lm.Text = "";

        //message to user
        //from encryped string e.g. http://www.publiship-online.com/Sys_Session_Login.aspx?rp=8/o5xvnLH1r3tesi4ZYj33YGKfHmBWzv
        
        string _requestingpage = Request.QueryString["rp"] != null?  wwi_security.DecryptString(Request.QueryString["rp"] , "publiship"): null;
        
        if(!string.IsNullOrEmpty(_requestingpage))
        {
            switch (_requestingpage)
            {
                case "lists/customer_target":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can use the customer target list";
                        break;
                    }
                case "tracking/shipment_tracking":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "Welcome to Publiship's advanced shipment tracking";
                        break;
                    }
                case "tracking/file_manager":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can use the online file manager";
                        break;
                    }
                case "tracking/dashboard": 
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use the dashboard";
                        break;
                    }
                case "shipment_orders/order_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search orders";
                        break;
                    }
                case "shipment_orders/order_titles":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can access order details";
                        break;
                    }
                case "shipment_orders/clone_order":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can clone an order";
                        break;
                    }
                case "pricer/price_quote":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can use the Pricer";
                        break; 
                    }
                case "pricer/price_audit":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can view your price quotes";
                        break;
                    }
                case "pricer/quote_history":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search quotes";
                        break;
                    }
                case "publiship_advance/advance_request":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can use Publiship Advance.";
                        break;
                    }
                case "publiship_advance/advance_audit":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can view advance shipments";
                        break;
                    }
                case "publiship_advance/advance_history":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can view advance shipments";
                        break;
                    }
                case "user_accounts/signin":
                    {
                        reqpage = "Default";
                        this.dxlblinfo.Text = "Enter your user name and password to sign in";
                        break; 
                    }
                case "pricer/pricer_reports":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can view reporting options";
                        break;
                    }
                case "shipment_containers/container_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search containers";
                        break;
                    }
                case "shipment_bol/housebl_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search House BL's";
                        break;
                    }
                case "shipment_voyage/voyage_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search voyages";
                        break;
                    }
                case "system_templates/order_template_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can search order templates";
                        break;
                    }
                case "reports/order_new_uk":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can open the 'new orders received' report";
                        break;
                    }
                case "tracking/test_page":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in before you can see the test page";
                        break;
                    }
                case "data_admin/country_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/port_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/place_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/contact_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/company_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/package_type_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/employee_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/company_type_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "data_admin/department_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to use data admin";
                        break;
                    }
                case "despatch_notes/despatch_note_search":
                    {
                        reqpage = _requestingpage;
                        this.dxlblinfo.Text = "You must sign in to to view despatch notes";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
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
                        dxchbSavePassword.Checked = false;
                    }
                    dxchbSavePassword.Visible = true;
                }
                else
                {
                    dxchbSavePassword.Visible = false;
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
        string _message = null;

        try
        {
            string _username = txtUserName.Text.Replace("'", "''");
            string _pass = wwi_security.EncryptString(txtPassword.Text.Replace("'", "''"), "publiship");

            _userlogin = _userlogin.Login(_username, _pass);

            if (_userlogin != null && _userlogin.loginValue != 0)
            //if (_userlogin != null && _userlogin.ID != Guid.Empty)
            {

                Session["user"] = _userlogin;


                if (Request.Browser.Cookies)
                {
                    if (dxchbSavePassword.Checked)
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
                        DateTime _dt = DateTime.Now.AddMinutes(10);  //DateTime.Now + new TimeSpan(1, 0, 0);
                        _acookie.Expires = _dt;
                    }
                    Response.Cookies.Add(_acookie);
                }
            }
            else
            {
                if (_userlogin == null)
                //if (_userlogin == null || _userlogin.ID == Guid.Empty)
                {
                    //this.lblMsg.Text = "<div class='fberrorbox'>Null login</div>";
                    _message  = " Invalid user name or password";
                }
                else //userLogin.loginValue = 0 to indicate error
                {
                    _message = " Not able to verify user due to a technical error";
                    //this.lblMsg.Text = "<div class='fberrorbox'>Invalid Login</div>";
                }
               
            }
        }
        catch
        {
            _userlogin = null;
            Session.Remove("user");
        }
        finally {
            //if (_userlogin != null)
            if(_message == null)
            {
                Redirect(true);
            }
            else
            {
                _userlogin = null;
                Session.Remove("user");
                this.lblmsg.Text = _message;
                this.dxpnlmsg.Visible = true;
               }    
        }//end finally
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


                //show logout
                //ClientScript.RegisterStartupScript(GetType(), "LOG_KEY", "window.parent.loginSwitch();", true);
                //
                //09/03/2011 reset submit_query -1 which should cause default filter to fire therefore not auto-loading data
                ClientScript.RegisterStartupScript(GetType(), "CLR_KEY", "window.parent.submit_query(-1);", true);
                //pass username back to parent
                ClientScript.RegisterStartupScript(GetType(), "LBL_KEY", "window.parent.dxlbllogin.SetText(txtResult.GetText());", true);
                //290311 not required here! close this window
                ClientScript.RegisterStartupScript(GetType(), "INN_KEY", "window.parent.closeloginWindow(1);", true);
                //30/03/2011 for forms authentication
                //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                if (!string.IsNullOrEmpty(reqpage))
                {
                    Response.Redirect("../" + reqpage + ".aspx" , true);
                }
                else
                {
                    Response.Redirect("../Default.aspx", true);
                }
            }
            else
            {
                //close this window
                ClientScript.RegisterStartupScript(GetType(), "OUT_KEY", "window.parent.cancelloginWindow();", true);
                //30/03/2011 for forms authentication
                //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                //string _root = AppDomain.CurrentDomain.BaseDirectory;
                Response.Redirect("../Default.aspx", true); 

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
        catch (Exception ex)
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
        //        case "ordertracking": //lowest level for client sign in
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
