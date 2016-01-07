using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Popupcontrol_barcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack & !Page.IsCallback)
        {
            if (Request.QueryString["code"] != null)
            {
                string _barcode = Request.QueryString["code"].ToString();
               
                //barcode.gif reference defined in web.config in httphandlers
                dximg.ImageUrl = string.Format("barcode.gif?code={0}", _barcode); 
            }
        }
    }

}
