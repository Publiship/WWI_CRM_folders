using System;
using System.Text; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using DAL.Logistics; //remove if creating c# script

public partial class pricer_reports : System.Web.UI.Page
{
    //on report button click timer is enabled and cbreport callback is enabled
    //on every timer tick cbtimer callback is fired, which gets progress value and updates progress bar
    //on cbreport end callback timer is switched off
    private static decimal _progress = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        ///********************* for testing web service
        ////switch to this when debugging and check port number!
        ////use global resx to identify web site location
        //localhost.Service_Reports _ws = new localhost.Service_Reports();
        //string _location = "http://localhost:4015/WWI_CRM/services/Service_Reports.asmx";
        //_ws.Url = _location;
        //_ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //string _test = _ws.runReportDateFiltered("publiship", "discrepancy","29/10/2012","31/10/2012");  //_ws.runReport("ZhkSjf7Te0/0LOoqk8swRA==", "n+13Qo1TOzMhK3OSZDbBHg==");
        ////********************

        //don't require login but make sure this is a Publiship user not a client
        //by checking querystring
        Int32 _publiship = 0;
        //hide error message
        this.dxpnlmsg.ClientVisible = false;

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            _progress = 0;
            this.dxtmrprogress.Enabled = false;
            this.dxtmrinit.Enabled = false;
        }

        if(Page.Session["user"] != null){
            _publiship = (Int32)((UserClass)Page.Session["user"]).CompanyId;
        }
        else
        {
            _publiship = Request.QueryString["id"] != null ? wwi_func.vint(wwi_security.DecryptString(Request.QueryString["id"].ToString(), "publiship")): 0;
        }

        if (_publiship == -1)
        {
            string _report = "discrepancy"; //Request.QueryString["rt"] != null ? wwi_security.DecryptString(Request.QueryString["rt"].ToString(), "publiship") : "";

            switch (_report)
            {
                case "discrepancy":
                    {
                        this.dxlblinfo2.Text = "Discrepancy report";
                        break;
                    }
                default:
                    {
                        this.dxlblinfo2.Text = "";
                        break;
                    }
            }
            //end switch
        }
        else
        {
            if (!Page.IsPostBack) { Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("pricer_reports", "publiship")); }
        }
    }

    #region report processes
    /// <summary>
    /// discrepency report
    /// split by controller names and emailed individually
    /// </summary>
    protected void process_discrepancy_report()
    {
        //only needed for connection string if we convert to a script
        //string _server = "WWISQL";
        //string _dbname = "PublishipSQL";
        //string _userid = "sa";
        //string _pw = "BaldblokeVN900";

        //t-sql designed to emulate MS Acess DescrepenciesReportQuery
        //make sure datetime are converted to date only or this query will return more records than MS Access
        //do some basic comparisons
        //shipped on board (SOB) with not yet shipped status
        //Exworks > 7 from now but not shipped on board status
        //ETA > now - 10 no status
        //ExWorks > ETS
        string _query = "SELECT OrderTable.OrderNumber, OrderTable.CompanyID AS [hfCompanyID], NameAndAddressBook.CompanyName AS Company, OrderTable.ShippedOnBoard as SOB, " +
                      "OrderTable.JobClosed, DeliverySubTable.CurrentStatusID as hfCurrentStatusID, CurrentStatus.Field1 AS [Current status], OrderTable.ExWorksDate, OrderTable.ETA, " +
                      "OrderTable.ETS, OrderTable.OrderControllerID as hfControllerID, EmployeesTable.Name AS Controller, EmployeesTable.EmailAddress as hfEmail, PortTable.PortName AS [Origin port], " +
                      "PortTable_1.PortName AS [Destination port], OrderTable.RemarksToCustomer " +
                      "FROM         EmployeesTable INNER JOIN " +
                      "OrderTable INNER JOIN " +
                      "NameAndAddressBook ON OrderTable.CompanyID = NameAndAddressBook.CompanyID INNER JOIN " +
                      "PortTable ON OrderTable.PortID = PortTable.PortID INNER JOIN " +
                      "DeliverySubTable ON OrderTable.OrderNumber = DeliverySubTable.OrderNumber INNER JOIN " +
                      "PortTable AS PortTable_1 ON OrderTable.DestinationPortID = PortTable_1.PortID ON " +
                      "EmployeesTable.EmployeeID = OrderTable.OrderControllerID INNER JOIN " +
                      "CurrentStatus ON DeliverySubTable.CurrentStatusID = CurrentStatus.ID " +
                      "WHERE     (OrderTable.OrderNumber < 1000000) AND (OrderTable.ShippedOnBoard = 1) AND (OrderTable.JobClosed = 0) AND " +
                      "(DeliverySubTable.CurrentStatusID = 12 OR " +
                      "DeliverySubTable.CurrentStatusID = 14) OR " +
                      "(OrderTable.OrderNumber < 1000000) AND (OrderTable.ShippedOnBoard = 0) AND (OrderTable.JobClosed = 0) AND " +
                      "(NameAndAddressBook.CompanyName <> N'Usborne Publishing Limited') AND (OrderTable.ExWorksDate > CONVERT(DATETIME, " +
                      "'2010-05-01 00:00:00', 102)) AND (OrderTable.ExWorksDate < DATEADD(dd, - 10, CONVERT(varchar, GETDATE(), 102))) OR " +
                      "(OrderTable.OrderNumber < 1000000) AND (OrderTable.JobClosed = 0) AND (DeliverySubTable.CurrentStatusID = 1 OR " +
                      "DeliverySubTable.CurrentStatusID = 12) AND (OrderTable.ETA > CONVERT(DATETIME, '2010-05-01 00:00:00', 102)) AND " +
                      "(OrderTable.ETA < DATEADD(dd, - 10, CONVERT(varchar, GETDATE(), 102))) OR " +
                      "(OrderTable.OrderNumber < 1000000) AND (OrderTable.ShippedOnBoard = - 1) AND (OrderTable.JobClosed = 0) AND " +
                      "(OrderTable.ExWorksDate > OrderTable.ETS) AND (OrderTable.CompanyID <> 5234) AND (OrderTable.CompanyID <> 9712) " +
                      "ORDER BY Controller, [hfCompanyID], OrderTable.OrderNumber;";

        ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
        SqlConnection _cn = new SqlConnection();
        _cn.ConnectionString = _cts.ConnectionString;
        //use this if we decide to create a seperate script
        //SqlConnection _cn = new SqlConnection("server=" + _server + ";Trusted_Connection=false;database=" + _dbname + ";Pwd=" + _pw + ";User ID=" + _userid + ";");
        _cn.Open();

        SqlDataAdapter _da = new SqlDataAdapter(_query,_cn);

        try
        {
            DataTable _dt = new DataTable();
            _da.Fill(_dt);

            //get unique controller names
            string[] _fields = { "hfControllerID", "Controller", "hfEmail" };
            DataTable _dc = _dt.DefaultView.ToTable(true, _fields);

            
            //enumerate through unique controller names
            for (int _rw = 0; _rw < _dc.Rows.Count; _rw++)
            {   
                string _controller = _dc.Rows[_rw]["Controller"].ToString();
                //filter data by controller name
                DataRow[] _set = _dt.Select("Controller = '"  + _controller + "'");
                //copy table def without rows
                DataTable _ds = _dt.Clone();
                //drop filtered rows into sub dataset
                foreach (DataRow _dr in _set)
                {
                    _ds.ImportRow(_dr); 
                }
                //build to html from sub datatable ds
                string _html = return_html_table(_ds);
                //using mailhelper
                if(!string.IsNullOrEmpty(_html))
                {
                    //string[] _to = { _controller, "paule@publiship.com" };
                    string[] _to = { "pauled2109@hotmail.co.uk", "paule@publiship.com" };
                    _html = "<p>Looks for: 'Shipped on Board' with 'Not Yet Shipped', 'ExWorks' more than 7 days but not 'Shipped on Board', " + 
                            "ETA more than 10 days with no updated status; Ex-Works later than Sailing date</p>" +
                            "<p>" + _ds.Rows.Count.ToString() +  " records found"+ "</p>" + _html;
                    MailHelper.gen_email(_to, false, "Discrepancy report " + DateTime.Now.ToShortDateString() + " for " + _controller, _html, "Paul Edwards");
                }
                //update progress % need to force at least 1 number in calculation to decimal or result will be cast to int by default
                _progress = ((decimal)100.00 / _dc.Rows.Count) * (_rw + 1); 
        }
        }
        catch (Exception e)
        {
            this.lblmsg.Text = e.Message.ToString();
            this.dxpnlmsg.Visible = true;
            //Response.Write(e.Message.ToString());  
            //Console.WriteLine(e.ToString());
        }
        finally
        {
            if (_cn.State == ConnectionState.Open)
                _cn.Close();
        }
    }
    //end process discrepancy report

    /// <summary>
    /// build html table from datatable
    /// </summary>
    /// <param name="dt">datatable to convert int html</param>
    /// <returns></returns>
    public static string return_html_table(DataTable dt)
    {
        StringBuilder _sb = new StringBuilder();
        string _tbl = "font-family: Lucida Sans Unicode,Lucida Grande, Sans-Serif; font-size: 12px; margin: 45px; width: 100%; text-align: left;" + 
                      "border-width: 1px; border-spacing: 0px; border-style: solid; border-color: gray; border-collapse: collapse; background-color: white;"; //get_from_global_resx("cargo_updated_table");
        string _th = "background-color: #e8edff; border-width: 1px; padding: 1px; border-style: solid; border-color: gray;"; //get_from_global_resx("cargo_updated_th");
        string _td = "padding: 1px; border-width: 1px; padding: 1px; border-style: solid; border-color: gray;"; //get_from_global_resx("cargo_updated_td");

        //define table
        _sb.Append("<p><table style='" + _tbl + "'>");

        //*****Add a headings row.
        _sb.Append("<tr style='" + _th + "'>");

        //for loop is 2x faster than foreach
        foreach (DataColumn _dc in dt.Columns)
        {
            if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do not include in table
            {
                _sb.Append("<td align='left' valign='top'>");
                _sb.Append(_dc.ColumnName.ToString().Trim());
                _sb.Append("</td>");
            }
        }
        _sb.Append("</tr>");
        //*******

        //*****Add data rows
        foreach (DataRow _dr in dt.Rows)
        {
            _sb.Append("<tr>");


            foreach (DataColumn _dc in dt.Columns)
            {
                string _cell = _cell = _dr[_dc.ColumnName].ToString(); ;
                //format datetimes
                //create table cell
                if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do no include in table
                {
                    if (_dc.ColumnName == "ExWorksDate")
                    {
                        _cell = str_format(_cell);
                        
                        //datetime validation
                        DateTime _exw = !string.IsNullOrEmpty(_cell) ? (DateTime)_dr["ExWorksDate"]: DateTime.MinValue ;
                        DateTime _ets = !string.IsNullOrEmpty(_dr["ETS"].ToString()) ? (DateTime)_dr["ETS"]: DateTime.MinValue;

                        if (_exw != DateTime.MinValue && _ets != DateTime.MinValue)
                        {
                            if (_exw > _ets) { _cell += "<br>Ex-Works Date later than Sailing"; }
                        }
                    }
                    else if(_dc.ColumnName == "ETS" || _dc.ColumnName == "ETA")
                    {
                        _cell = str_format(_cell);
                    }
                    else if(_dc.ColumnName == "SOB"){
                        _cell = _cell == "False" ? "No" : "Yes";
                    }
                    else if (_dc.ColumnName == "JobClosed")
                    {
                        _cell = _cell == "False" ? "No" : "Yes";
                    }  

                    _sb.Append("<td style='" + _td + "'>");
                    _sb.Append(_cell);
                    _sb.Append("</td>");
                }
            }
            _sb.Append("</tr>");
        }
        //*****
        //end table definition
        _sb.Append("</table></p>");

        //return string 
        return _sb.ToString();
    }
    //end get html table

    /// <summary>
    /// format string
    /// dates to local short date format
    /// </summary>
    /// <param name="value">string to be formatted</param>
    /// <returns></returns>
    public static string str_format(string value)
    {
        Double _num = 0;
        string _result = "";
        string _format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        try
        {   //is is a number? don't try and convert to date
            if (Double.TryParse(value, out _num))
            {
                _result = value;
            }
            else
            {
                DateTime _dt = DateTime.Parse(value);
                _result = System.Xml.XmlConvert.ToString(_dt, _format);
            }
        }
        catch
        {
            _result = value;
        }
        return _result;
    }
    //end str format
#endregion

    #region server side functionality

    /// <summary>
    /// callback panel fired when Report button is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void dxcbreport_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        string _report = this.dxlblinfo2.Text.Replace("report","").ToLower().Trim();

        switch (_report)
        {
            case "discrepancy":
                {
                    process_discrepancy_report();
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    //end report call back

    /// <summary>
    /// timer event used to update progress
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbtimer_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = _progress.ToString();
    }
    //end timer callback
#endregion


}
