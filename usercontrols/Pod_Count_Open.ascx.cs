using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DevExpress.XtraCharts;

public partial class usercontrols_Pod_Count_Open : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Calendar.SelectedDate = new DateTime(DateTime.Now.Year, 3, 14);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.dxlblerr1.Visible = false;

        if (!Page.IsPostBack)
        {
            Int32 _companyid = -1; //after testing default to empty string 
            Int32 _userid = -1;

            //company id: always add as a search param as user must be logged in
            //if (Page.Session["user"] != null)
            if (Page.Session["user"] != null)
            {
                _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;
                _userid = (Int32)((UserClass)Page.Session["user"]).UserId;
            }

            bind_chart(_companyid);
        }
    }

    //databind to total orders by eta and company id
    protected void bind_chart(Int32 companyid)
    {
        try
        {
            int _countopen = 0;

            //total open orders for this company or total open orders for company = -1
            if (companyid > -1)
            {
                _countopen = new Select(OrderTable.OrderIDColumn).From<OrderTable>().Where(OrderTable.CompanyIDColumn).IsEqualTo(companyid).
                    And(OrderTable.JobClosedColumn).IsEqualTo(false).GetRecordCount();
            }
            else
            {
                _countopen = new Select(OrderTable.OrderIDColumn).From<OrderTable>().Where(OrderTable.JobClosedColumn).IsEqualTo(false).GetRecordCount();
            }
            //bind to gauge
            this.dxgaugeSumopen.Value = _countopen.ToString();
        }
        catch (Exception ex)
        {
            this.dxlblerr1.Text = ex.Message.ToString();
            this.dxlblerr1.Visible = true;
        }
    }
    //end bind guage
}
