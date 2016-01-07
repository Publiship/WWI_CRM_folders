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

public partial class usercontrols_Pod_ETA_Month : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Calendar.SelectedDate = new DateTime(DateTime.Now.Year, 3, 14);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.dxlblerr2.Visible = false;

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
            ordertablecustomcontroller _orders = new ordertablecustomcontroller();
            IDataReader _dr = _orders.GetOrderCountByEtaMonth(companyid);
            DataTable _dt = new DataTable();
            _dt.Load(_dr);

            //DevExpress.XtraCharts.Series _s = this.dxchartEta.Series[0];
            //
            Series _s = this.dxchartEta.Series[0];
            XYDiagram _xy = (XYDiagram)this.dxchartEta.Diagram;

            this.dxchartEta.Legend.Visible = false;
            //stagger x axis lables
            _xy.AxisX.Label.Staggered = false;
            //rotate them.
            _xy.AxisX.Label.Angle = -30;
            _xy.AxisX.Label.Antialiasing = true;

            _s.Label.TextOrientation = TextOrientation.Horizontal;
            _s.DataSource = _dt;

            //columns
            _s.ArgumentScaleType = ScaleType.DateTime;
            _s.ArgumentDataMember = "GroupByOfETA";

            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOrderId" });
            this.dxchartEta.DataBind();
        }
        catch (Exception ex)
        {
            this.dxlblerr2.Text = ex.Message.ToString();
            this.dxlblerr2.Visible = true;
        }
    }
    //end bind guage
}
