using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;

public partial class user_downloads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string _description = wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "excelDownload", "value");
        //this.dxlblDescription.Text = _description;

        if (Page.Session["user"] == null)
        {
            if (!Page.IsCallback) { Response.Redirect("../Default.aspx", true); }
        }
    }
}
