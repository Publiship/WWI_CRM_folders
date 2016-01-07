using System;
using System.Data;
using System.Collections; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Ord_view_Tracking_Anon : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //get path to form
        string _folder = System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath);
        //change to formview page by removing 'search' e.g. container-search.aspx becomes container.aspx
        string _page = "tracking\\shipment_tracking.aspx";

        Response.Redirect(_folder + "\\" + _page, true);
        
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
    }
     
}
