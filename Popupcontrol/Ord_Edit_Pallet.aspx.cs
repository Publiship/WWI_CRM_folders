using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;

public partial class Ord_Edit_Pallet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //bind_data();
        }

    }

    protected void bind_data()
    {
        //string _orderno = Request.QueryString["or"]; //use application internal ID to retrive details
        //this.dxtxtpallets.Text = _orderno; 
    }

    /// <summary>
    /// update buton
    /// close this form
    /// sumbit_batch_request() javascript function on parent form gets selected rows
    /// and forces grid to rebind
    /// by calling grid customcallback() event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnupdate_Click(object sender, EventArgs e)
    {
        if (Page.Session["user"] != null)
        {
            //get companyid 
            String _companyid = Convert.ToString((Int32)((UserClass)Page.Session["user"]).CompanyId);
            ///get userid
            String _userid = Convert.ToString((Int32)((UserClass)Page.Session["user"]).UserId);
            //selected cargo ready date which MUST be formatted to yyyy/MM/dd
            //Datetime? is nullable 
            DateTime? _dt1 = null;
            string _dts = "";

            _dt1 = (DateTime?)this.dxdtcargoready.Value;

            if (_dt1.HasValue)
            {
                DateTime _dt2 = (DateTime)this.dxdtcargoready.Value;
               _dts = _dt2.ToString("yyyy/MM/dd");
            }

            //create string to pass back to parent form - the orderid can be populated e.g. from grid selected items
            String _row = "<row orderid='{0}' cargoready='" + _dts 
                + "' estpallets='" + this.dxtxtpallets.Value.ToString() + "' estweight='"
                + this.dxtxtweight.Value.ToString() + "' estvolume='" + this.dxtxtvolume.Value.ToString() + "' updguid='{1}' "
                + "companyid='" + _companyid + "' userid='" + _userid + "'/>";

            Session["updcargo"] = _row;
            //script to close this form and trigger batch update
            this.ClientScript.RegisterStartupScript(GetType(), "PAL_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('editpalletform'));window.parent.submit_batch_request();", true);

        }
    }
}
