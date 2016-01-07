using System;
using System.Data;
using System.Globalization;
using System.Collections; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using ParameterPasser;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString);
            //connection.Open();
            //if ((connection.State == ConnectionState.Open))
            //{
            //    connection.Close();
            
                //get path 
                string _default = "tracking\\shipment_tracking.aspx";
                string _uid = this.Session["user"] != null ? ((UserClass)this.Session["user"]).UserId.ToString() : "";
                //20.10.14 check individual account to identify default page (e.g. MacMillan defaults to delivery tracking not shipment tracking
                //string _page = wwi_func.lookup_xml_string(_folder + "\\xml\\company.iso",""
                string _page = _uid != "" ? wwi_func.lookup_xml_string("\\xml\\contact_iso.xml", "id", _uid, "default", _default) : _default;
                //get path to form
                string _folder = System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath);
                //change to formview page by removing 'search' e.g. container-search.aspx becomes container.aspx
                Response.Redirect(_folder + "\\" + _page, false);
                //old code
                //Response.Redirect("../tracking/shipment_tracking.aspx", true);

                //old code
                //if (Session["user"] != null)
                //{
                //    Response.Redirect("~/Online_Tracking/Ord_View_Tracking_Unsigned.aspx", true);
                //}
                //else
                //{
                //    Response.Redirect("~/Online_Tracking/Ord_View_Tracking_Unsigned.aspx", true);
                //}
               
            //}
            //else
            //{
            //    Response.Write("No Connection!");
            //}
        }
        catch(Exception ex)
        {
            //Response.Write("No Connection!");
            Response.Write(ex.Message.ToString());
        }

        //bool _test1 = wwi_func.process_asn_test_multi_titles(12345);
        //
        //bool _test2 = wwi_func.process_asn_test_1title (54321);
        //
        //string _test3 = wwi_asn.create_and_upload_asn("AUTO");
        //ftp_handler _ftp = new ftp_handler("ftp.2008hosting.net", "pauleds2109", "togger2109");
        //string _test = _ftp.upload("~/Include/12345_37.xml", "c;/12345_37.xml");
        
        ///fizzbuzz programming test
        //string[] _txt = { "Fizz", "Buzz" };
        //int _max = 100;
        //int _mod3 = 3;
        //int _mod5 = 5;
        //for (int _ix = 1; _ix <= _max; _ix++)
        //{
        //    string _out = _ix.ToString() + " "; 
        //    _out += _ix % _mod3 == 0 ? _ix % _mod5 == 0 ? _txt[0] + _txt[1] : _txt[0] : _ix % _mod5 == 0 ? _txt[1] : _ix.ToString();
        //
        //    Response.Write(_out + System.Environment.NewLine);
        //}
        //page request sent from logout button client side event
        //once the sessions have been cleared page will direct to default and the log in/log out buttons will be reset on master
        
        //*********
        //moved to webmethos on master page
        //if (Page.Request["logout"] != null)
        //{
        //    //clear all sessions
        //    Session.Remove("user");
        //
        //    Session.Clear();
        //    SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        //    _sessionWrapper["query"] = "(OrderNumber==-1)";
        //    _sessionWrapper["name"] = null;
        //   _sessionWrapper["mode"] = null;
        //
        //
        //    //kill cookie
        //    if (Request.Browser.Cookies)
        //    {
        //        if (Request.Cookies["user"] != null)
        //        {
        //            HttpCookie _acookie = new HttpCookie("user");
        //            _acookie.Expires = DateTime.Now.AddDays(-1d);
        //            Response.Cookies.Add(_acookie);
        //        }
        //    }
        //    //end kill cookie
        //}
        //*********

      
    }
     
}
