using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Gauges.Digital;
using DevExpress.XtraCharts;

public partial class tracking_dashboard : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
            //bind_order_titles();
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("order_search", "publiship"));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (isLoggedIn() && !Page.IsPostBack)
        //{
        //temporarily disabled. have we redirected from new order?
        //string _new = get_token("cno");
        //if (!string.IsNullOrEmpty(_new))
        //{
        //    this.dxlblInfo.Text = "Please add titles to this order (Click the + button or 'Add title' button";
        //    this.dxpnlMsg.ClientVisible = true;
        //
        //}
        if (!Page.IsPostBack)
        {
            bind_dashboard(); 
        }
       
        //}
        //else
        //{
        //    string _orderid = Request.QueryString["pid"] != null ? "&pid=" + Request.QueryString["pid"].ToString() : "";
        //    Response.Redirect("~/Sys_Session_Login.aspx?" + "rp=" + wwi_security.EncryptString("Pod_Titles", "publiship") + _orderid);
        //}
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    protected void bind_dashboard()
    {
        bind_all_gauges();
        bind_all_eta_charts("days", 7);
        bind_all_ets_charts("month", -1);
        bind_metrics();

    }


    #region databinding for gauge
    protected void bind_all_gauges()
    {
        try
        {
            ParameterCollection _params = new ParameterCollection();
            string _query = "";
            float[] _values = { 0, 0, 0, 0 }; //containers, weight, cube (cbm), pallets

            //***************
            //21.10.2014 Paul Edwards for delivery tracking and container check which DeliveryID's are visible for this company
            //check individual contact ID iso 
            string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
            IList<string> _deliveryids = null;
            _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
            if (_deliveryids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _deliveries = "(DeliveryAddress==" + string.Join(" OR DeliveryAddress==", _deliveryids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _deliveries); //select for this company off list

            }
            else
            {
                string _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                _params.Add("CompanyID", _companyid);
            }
            //****************

            string _f = "";
            if (_params.Count > 0)
            {
                foreach (Parameter p in _params)
                {
                    string _pname = p.Name.ToString();
                    string _op = "AND";
                    string[] _check = _pname.Split("_".ToCharArray());
                    _pname = _check[0].ToString();
                    _op = _check.Length > 1 ? _check[1].ToString() : _op;

                    string _a = _f != "" ? " " + _op + " " : "";
                    _f += _pname != "NULL" ? _a + "(" + _pname + "==" + p.DefaultValue.ToString() + ")" : _a + "(" + p.DefaultValue.ToString() + ")";

                }

                if (_query != "") { _query = _f + " AND " + _query; } else { _query = _f; }
            }

            //****************
            //get starting ETS and ending ETS for current date
            //convert(datetime,'28/03/2013 10:32',103)
            //if no datetimes seelcted default to 0/01/1900 so no data displayed
            DateTime? _ets1 = DateTime.Now;
            DateTime? _ets2 = DateTime.Now;
            //****************

            //important! need a key id or error=key expression is undefined
            //e.KeyExpression = "ContainerIdx";

            //10.11/2014 if necessary send to datatable then use datatable.compute method on clientname to summarise without showing
            //breakdown by delivery address
            if (!string.IsNullOrEmpty(_query))
            {
                linq.linq_aggregate_containers_udfDataContext _ct = new linq.linq_aggregate_containers_udfDataContext();
                IEnumerator<linq.aggregate_containers_by_etsResult> _in = _ct.aggregate_containers_by_ets(_ets1, _ets2).Where(_query).GetEnumerator();

                while (_in.MoveNext())
                {
                    linq.aggregate_containers_by_etsResult _c = _in.Current;
                    _values[0] += (float)_c.ContainerCount;
                    _values[1] += (float)_c.SumPackages;
                    _values[2] += (float)_c.SumWeight;
                    _values[3] += (float)_c.SumCbm;
                }

            }

            //bind aggregates to labels !guage
            this.dxGaugeContainers.Value = _values[0].ToString("N");
            this.dxGaugePallets.Value = _values[1].ToString("N");
            this.dxgaugeWeight.Value = _values[2].ToString("N");
            this.dxgaugeCbm.Value = _values[3].ToString("N");



            //for (int _ix = 0; _ix < _values.Length; _ix++)
            //{
            //this.dxguageTotals.AddGauge(DevExpress.XtraGauges.Base.GaugeType.Digital);
            //((DigitalGauge)this.dxguageTotals.Gauges[_ix]).Text = _values[_ix].ToString("N"); 
            //string _cap = "dxlblCount" + _ix.ToString();
            //ASPxLabel _lbl = (ASPxLabel)this.FindControl(_cap);
            //if (_lbl != null) { _lbl.Text = _values[_ix].ToString("N"); }
            //not being used multiple guages too messy to format
            //((DigitalGauge)this.dxguageTotals.Gauges[_ix]).Text = _values[_ix].ToString("N"); 
            //}
            //end for
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.Visible = true;
        }
    }
    #endregion

    #region databinding charts
    //protected void dxchartShipping_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
    protected void bind_all_ets_charts(string datetype, int daterange)
    {
        //if (e.Parameter == "getdata" && Page.Session["shippingloaded"] == null)
        //{
        string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
        IList<string> _deliveryids = null;
        _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
        if (_deliveryids.Count == 0)
        {
            string _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
            _deliveryids.Add(_companyid);  
        }

        //object[] _in = { 406, 12346, 13776 };
        DateTime _dx = DateTime.MinValue;
        if (datetype == "days")
        {
            _dx = DateTime.Now.AddDays(daterange);
        }
        else
        {
            _dx = DateTime.Now.AddMonths(daterange);

        }

        DataTable _dt = new Select(Aggregate.Count(DAL.Logistics.ContainerTable.ContainerIDColumn),
             Aggregate.Sum(DAL.Logistics.ContainerSubTable.WeightColumn),
             Aggregate.Sum(DAL.Logistics.ContainerSubTable.CbmColumn),
             Aggregate.Sum(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
             Aggregate.GroupBy(DAL.Logistics.NameAndAddressBook.CompanyNameColumn))
             .From(DAL.Logistics.Tables.DeliverySubTable)
            .InnerJoin(DAL.Logistics.ContainerSubTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
            .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
            .InnerJoin(DAL.Logistics.OrderTable.OrderIDColumn, DAL.Logistics.ContainerSubTable.OrderIDColumn)
            .InnerJoin(DAL.Logistics.NameAndAddressBook.CompanyIDColumn, DAL.Logistics.OrderTable.CompanyIDColumn)
            .Where(DAL.Logistics.DeliverySubTable.DeliveryAddressColumn).In(_deliveryids).And(DAL.Logistics.OrderTable.EtsColumn).IsGreaterThanOrEqualTo(_dx)
                .ExecuteDataSet().Tables[0];

        //bind bar chart serices for containers ETS this week
        ////this.dxchartShipping.DataSource = _dt;
        //this.dxchartShipping.Series.Add("Containers", ViewType.Bar);
        //Series _s = this.dxchartShipping.Series["Containers"]; //containers
        //_s.Label.TextOrientation = TextOrientation.Horizontal;
        //_s.DataSource = _dt;
        //columns
        //_s.ArgumentScaleType = ScaleType.Qualitative;
        //_s.ArgumentDataMember = "GroupByOfCompanyName";
        //rows
        //_s.ValueScaleType = ScaleType.Numerical;
        //_s.ValueDataMembers.AddRange(new string[] { "CountOfContainerID" });
        if (datetype == "days")
        {
            Series _s = this.dxchartPalletsWeightWW.Series["Pallets"]; //containers
            _s.DataSource = _dt;

            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName";

            XYDiagram _xy = (XYDiagram)this.dxchartPalletsMM.Diagram;
            this.dxchartPalletsWeightWW.Legend.Visible = true;
            //stagger x axis lables
            _xy.AxisX.Label.Staggered = true;
            //rotate them.
            _xy.AxisX.Label.Angle = 45;
            _xy.AxisX.Label.Antialiasing = true;
            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOfNumberOfPackages" });

            _s = this.dxchartPalletsWeightWW.Series["Weight"]; //containers
            _s.DataSource = _dt;

            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName";
            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOfWeight" });
            this.dxchartPalletsWeightWW.DataBind();
        }
        else
        {
            //bind bar chart series for pallets ETS this week
            //this.dxchartPalletsMM.Series.Add("Pallets", ViewType.Bar); defined in properties
            Series _s = this.dxchartPalletsMM.Series["Pallets"]; //define series
            _s.DataSource = _dt;
            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName";

            XYDiagram _xy = (XYDiagram)this.dxchartPalletsMM.Diagram;
            this.dxchartPalletsMM.Legend.Visible = true;
            //stagger x axis lables
            _xy.AxisX.Label.Staggered = false;
            //rotate them.
            _xy.AxisX.Label.Angle = -30;
            _xy.AxisX.Label.Antialiasing = true;

            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOfNumberOfPackages" });

            //bind bar chart series for cube ETS this week
            //this.dxchartPalletsMM.Series.Add("Cube", ViewType.Bar);
            //_s = this.dxchartPalletsMM.Series["Cube"]; //containers
            //_s.DataSource = _dt;

            //columns
            //_s.ArgumentScaleType = ScaleType.Qualitative;
            //_s.ArgumentDataMember = "GroupByOfCompanyName";
            //rows
            //_s.ValueScaleType = ScaleType.Numerical;
            //_s.ValueDataMembers.AddRange(new string[] { "SumOfCbm" });
            //this.dxchartPalletsMM.DataBind();
            //***************
            //bind bar chart series for cube ETS this week
            //this.dxchartCubeMM.Series.Add("Cube", ViewType.Bar);
            _s = this.dxchartCubeMM.Series["Cube"]; //containers
            _s.DataSource = _dt;

            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName";

            _xy = (XYDiagram)this.dxchartCubeMM.Diagram;
            this.dxchartCubeMM.Legend.Visible = true;
            //stagger x axis lables
            _xy.AxisX.Label.Staggered = false;
            //rotate them.
            _xy.AxisX.Label.Angle = -30;
            _xy.AxisX.Label.Antialiasing = true;
            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOfCbm" });
            this.dxchartCubeMM.DataBind();

            //***************
            //bind bar chart series for weight ETS this week (seperate from chartShipping as likely to be much higher numbers)
            //this.dxchartWeight.DataSource = _dt;

            //this.dxchartWeightMM.Series.Add("Weight", ViewType.Bar);
            _s = this.dxchartWeightMM.Series["Weight"]; //containers

            _xy = (XYDiagram)this.dxchartWeightMM.Diagram;

            this.dxchartPalletsMM.Legend.Visible = true;
            //stagger x axis lables
            _xy.AxisX.Label.Staggered = false;
            //rotate them.
            _xy.AxisX.Label.Angle = -30;
            _xy.AxisX.Label.Antialiasing = true;

            _s.Label.TextOrientation = TextOrientation.Horizontal;
            _s.DataSource = _dt;

            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName";
            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "SumOfWeight" });
            this.dxchartWeightMM.DataBind();

            //***************
            //pie chart deliveries by client
            //total containers
            //double _containers = wwi_func.vdouble(_dt.Compute("Sum(CountOfContainerID)", "CountOfContainerID >=0").ToString());
            //_dt.Columns.Add(new DataColumn("PcOfContainerID", typeof(double)));

            //for(int _ix =0; _ix < _dt.Rows.Count; _ix++)
            //{
            //_dt.Rows[_ix]["PcOfContainerID"] = (_containers / 100) * wwi_func.vdouble(_dt.Rows[_ix]["CountOfContainerID"].ToString()); 
            //}

            //this.dxchartContainersMM.Series.Add("Containers", ViewType.Pie);
            _s = this.dxchartContainersMM.Series["Containers"]; //containers

            //_xy = (XYDiagram)this.dxchartWeight.Diagram;

            //this.dxchartShipping.Legend.Visible = false;
            //stagger x axis lables
            //_xy.AxisX.Label.Staggered = false;
            //rotate them.
            //_xy.AxisX.Label.Angle = -30;
            //_xy.AxisX.Label.Antialiasing = true;

            _s.Label.TextOrientation = TextOrientation.Horizontal;
            _s.DataSource = _dt;
            _s.LegendPointOptions.Pattern = "{A}";
            //_s.PointOptions.Pattern = "{A}: {V:F1}";

            //columns
            _s.ArgumentScaleType = ScaleType.Qualitative;
            _s.ArgumentDataMember = "GroupByOfCompanyName"; //GroupByOfCompanyName
            //rows
            _s.ValueScaleType = ScaleType.Numerical;
            _s.ValueDataMembers.AddRange(new string[] { "CountOfContainerID" }); //PcOfContainerID
            this.dxchartContainersMM.DataBind();
        }
        //}
    }
    //end ets charts
    protected void bind_all_eta_charts(string datetype, int daterange)
    {
        //if (e.Parameter == "getdata" && Page.Session["shippingloaded"] == null)
        //{
        Page.Session["shippingloaded"] = true;

        string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
        IList<string> _deliveryids = null;
        _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
        if (_deliveryids.Count == 0)
        {
            string _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
            _deliveryids.Add(_companyid);
        }

        //object[] _in = { 406, 12346, 13776 };
        DateTime _dx = DateTime.MinValue;
        if (datetype == "days")
        {
            _dx = DateTime.Now.AddDays(daterange);
        }
        else
        {
            _dx = DateTime.Now.AddMonths(daterange);

        }

        DataTable _dt = new Select(Aggregate.Count(DAL.Logistics.ContainerTable.ContainerIDColumn),
             Aggregate.Sum(DAL.Logistics.ContainerSubTable.WeightColumn),
             Aggregate.Sum(DAL.Logistics.ContainerSubTable.CbmColumn),
             Aggregate.Sum(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
             Aggregate.GroupBy(DAL.Logistics.NameAndAddressBook.CompanyNameColumn))
             .From(DAL.Logistics.Tables.DeliverySubTable)
            .InnerJoin(DAL.Logistics.ContainerSubTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
            .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
            .InnerJoin(DAL.Logistics.OrderTable.OrderIDColumn, DAL.Logistics.ContainerSubTable.OrderIDColumn)
            .InnerJoin(DAL.Logistics.NameAndAddressBook.CompanyIDColumn, DAL.Logistics.OrderTable.CompanyIDColumn)
            .Where(DAL.Logistics.DeliverySubTable.DeliveryAddressColumn).In(_deliveryids).And(DAL.Logistics.OrderTable.EtaColumn).IsGreaterThanOrEqualTo(_dx)
                .ExecuteDataSet().Tables[0];

        Series _s = this.dxchartPalletsWeightWW.Series["Pallets"]; //containers
        _s.DataSource = _dt;
        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfCompanyName";

        XYDiagram _xy = (XYDiagram)this.dxchartPalletsWeightWW.Diagram;
        this.dxchartPalletsWeightWW.Legend.Visible = true;
        //stagger x axis lables
        _xy.AxisX.Label.Staggered = false;
        //rotate them.
        _xy.AxisX.Label.Angle = -30;
        _xy.AxisX.Label.Antialiasing = true;
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "SumOfNumberOfPackages" });

        _s = this.dxchartPalletsWeightWW.Series["Weight"]; //containers
        _s.DataSource = _dt;

        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfCompanyName";
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "SumOfWeight" });
        this.dxchartPalletsWeightWW.DataBind();

        //******************
        _s = this.dxchartContainersWW.Series["Containers"]; //containers

        _s.Label.TextOrientation = TextOrientation.Horizontal;
        _s.DataSource = _dt;
        _s.LegendPointOptions.Pattern = "{A}";
        //_s.PointOptions.Pattern = "{A}: {V:F1}";

        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfCompanyName"; //GroupByOfCompanyName
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "CountOfContainerID" }); //PcOfContainerID
        this.dxchartContainersWW.DataBind();
    }
    //end eta charts

    protected void bind_metrics()
    {
        string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
        IList<string> _deliveryids = null;
        _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
        if (_deliveryids.Count == 0)
        {
            string _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
            _deliveryids.Add(_companyid);
        }

        //object[] _in = { 406, 12346, 13776 };
        DateTime _dx = DateTime.Now.AddMonths(-1);

        DataTable _dt = new Select(Aggregate.Count(DAL.Logistics.ContainerTable.ContainerIDColumn),
            Aggregate.Avg(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
            Aggregate.Min(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
            Aggregate.Max(DAL.Logistics.OrderTable.NumberOfPackagesColumn),
            Aggregate.GroupBy(DAL.Logistics.OrderTable.EtsColumn))
            .From(DAL.Logistics.Tables.DeliverySubTable)
           .InnerJoin(DAL.Logistics.ContainerSubTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
           .InnerJoin(DAL.Logistics.ContainerTable.ContainerIDColumn, DAL.Logistics.ContainerSubTable.ContainerIDColumn)
           .InnerJoin(DAL.Logistics.OrderTable.OrderIDColumn, DAL.Logistics.ContainerSubTable.OrderIDColumn)
           .InnerJoin(DAL.Logistics.NameAndAddressBook.CompanyIDColumn, DAL.Logistics.OrderTable.CompanyIDColumn)
           .Where(DAL.Logistics.DeliverySubTable.DeliveryAddressColumn).In(_deliveryids).And(DAL.Logistics.OrderTable.EtsColumn).IsGreaterThanOrEqualTo(_dx)
               .ExecuteDataSet().Tables[0];

        Series _s = this.dxchartMetrics.Series["Lowest"]; //define series
        _s.DataSource = _dt;
        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfETS";

        XYDiagram _xy = (XYDiagram)this.dxchartMetrics.Diagram;
        this.dxchartMetrics.Legend.Visible = true;
        //stagger x axis lables
        _xy.AxisX.Label.Staggered = false;
        //rotate them.
        _xy.AxisX.Label.Angle = -30;
        _xy.AxisX.Label.Antialiasing = true;
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "MinOfNumberOfPackages" });

        _s = this.dxchartMetrics.Series["Average"]; //define series
        _s.DataSource = _dt;
        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfETS";
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "AvgOfNumberOfPackages" });

        _s = this.dxchartMetrics.Series["Highest"]; //define series
        _s.DataSource = _dt;
        //columns
        _s.ArgumentScaleType = ScaleType.Qualitative;
        _s.ArgumentDataMember = "GroupByOfETS";
        //rows
        _s.ValueScaleType = ScaleType.Numerical;
        _s.ValueDataMembers.AddRange(new string[] { "MaxOfNumberOfPackages" });

        this.dxchartMetrics.DataBind();
    }
    //end metrics
    #endregion

    #region functions

    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_token(string namedtoken)
    {
        string _value = this.dxhfOrder.Contains(namedtoken) ? this.dxhfOrder.Get(namedtoken).ToString() : null;

        if (string.IsNullOrEmpty(_value))
        {
            _value = Page.Request[namedtoken] != null ? HttpUtility.UrlDecode(Page.Request[namedtoken].ToString(), System.Text.Encoding.Default) : null;
        }

        //don't decrypt it here
        //if (_value != null) { _value = wwi_security.DecryptString(_value, "publiship"); }
        return _value;
    }
    //end get saved token
    /// <summary>
    /// some formating functions for Evals  this does not work with Bind you can't enclose Bind with functions
    /// </summary>
    /// <param name="testvalue">object to check</param>
    /// <returns>empty string if null or object value</returns>
    public string nullValue(object testvalue)
    {
        if (testvalue == null)
            return "";
        return testvalue.ToString();

    }
    #endregion


    
}
