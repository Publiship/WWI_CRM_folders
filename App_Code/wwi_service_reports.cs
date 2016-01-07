using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SubSonic;
using DAL.Logistics;
using System.Net.Mail;
using System.Xml.Serialization;
/// <summary>

/// <summary>
/// Summary description for Service_Feeds
/// </summary>
namespace DAL.Services
{
    [WebService(Namespace = "http://www.publiship.com/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service_Reports : System.Web.Services.WebService
    {
        [Serializable]
        private class empInfo
        {
            public int EmployeeID { get; set; }

            public string Name { get; set; }

            public string EmailAddress { get; set; }
        }

        //[Serializable]
        //private class empsInfo
        //{
        //    public string ErrorMessage { get; set; }
        //    
        //    public List<empInfo> empItems { get; set; }
        //}
 

        public Service_Reports()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "This is a restricted method", EnableSession = false)]
        public string runReport(string credentials, string report)
        {
            string _result = "";
            string _auth = "publiship";
            string _rep = report;

            if (_auth == "publiship")
            {
                switch (_rep)
                {
                    case "discrepancy":
                        {
                            _result = process_discrepancy_reports("", "");
                            break;
                        }
                    default:
                        {
                            _result = "bad input  parameter 2: '" + _rep + "'";
                            break;
                        }
                }
                //end switch
            }
            else
            {
                _result = "bad input parameter 1: '" + _auth + "'";
            }

            return _result;
        }
        [WebMethod(Description = "This is a restricted method", EnableSession = false)]
        public string runReportDateFiltered(string credentials, string report, string sdate1, string sdate2)
        {
            string _result = "";
            string _auth = "publiship";
            string _rep = report;
            string _d1 = !string.IsNullOrEmpty(sdate1) ? wwi_func.vdatetime(sdate1).ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            string _d2 = !string.IsNullOrEmpty(sdate2) ? wwi_func.vdatetime(sdate2).ToString("yyyy-MM-dd"): DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
  
            if (_auth == "publiship")
            {
                switch (_rep)
                {
                    case "discrepancy":
                        {
                            _result = process_discrepancy_reports(_d1, _d2);
                            break;
                        }
                    default:
                        {
                            _result = "bad input  parameter 2: '" + _rep + "'";
                            break;
                        }
                }
                //end switch
            }
            else
            {
                _result = "bad input parameter 1: '" + _auth + "'";
            }

            return _result;
        }
        //end run report
        /// <summary>
        /// 3 reports and 1 data update 
        /// </summary>
        /// <returns></returns>
        protected string process_discrepancy_reports(string sdate1, string sdate2)
        {
            //list of returned tmp files
            DataSet _ds = new DataSet("all_reports");
            string _xml = "";
      
            IList<empInfo> _ids = get_user_ids();
            IList<string> _tmp = new List<string>(new string[] { "Orders_With_No_Delivery_Details", "Orders_Without_Customer_Contact", 
                                                "Discrepancies_Report_Pre_Shipment", "Discrepancies_Report_Post_Shipment", 
                                                "Check_Delivery_Status", "Usborne_ExWorks_Date_Overdue_Report" });

            
            //for each user (probably most of these users will not have any reports to send
            //process reports and get tmp filename for each
            for (int _ix = 0; _ix < _tmp.Count ; _ix++)
            {
                //check to see if this report is scheduled
          
                if (in_schedule(_tmp[_ix]))
                {
                    DataTable _dt = process_report(_tmp[_ix], sdate1.ToString(), sdate2.ToString()); //append report results to report name  
                    //141112 include all data tables so user knows if 0 rcords found  
                    if (_dt != null)
                    //if (_dt != null && _dt.Rows.Count > 0)
                    {
                        _dt.TableName = _tmp[_ix]; //must have table names or can't parse to xml
                        _ds.Tables.Add(_dt);
                    }
                }
            }

            if (_ds.Tables.Count > 0)
            {
                //now step through users and filter data by user id
                for (int _ix = 0; _ix < _ids.Count; _ix++)
                {
                    empInfo _user = _ids[_ix];

                    IList<Attachment> _csvs = new List<Attachment>();
                    string _mailto = _user.EmailAddress;
                    string _controller = _user.Name;
                    string _html = "";
                    
                    //step through all query datatables and filter by current user
                    for (int _dx = 0; _dx < _ds.Tables.Count; _dx++)
                    {
                        int _records = 0; //reset for each table for this user

                        if (_ds.Tables[_dx].Rows.Count > 0)
                        {
                            DataRow[] _set = _ds.Tables[_dx].Select("hfControllerID = " + _user.EmployeeID);

                            //copy table def without rows
                            DataTable _du = _ds.Tables[_dx].Clone();
                            //drop filtered rows into sub dataset
                            foreach (DataRow _dr in _set)
                            {
                                _du.ImportRow(_dr);
                            }

                            //create email attachment if has rows
                            if (_du.Rows.Count > 0)
                            {
                                // Stream the Excel spreadsheet to the client in a format
                                // compatible with Excel 97/2000/XP/2003/2007/2010.
                                System.IO.MemoryStream _stream = new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(return_formatted_csv(_du)));
                                Attachment _a = new Attachment(_stream, _tmp[_dx] + ".csv");
                                _csvs.Add(_a);
                                _records = _du.Rows.Count;
                            }
                        }
                        //look for a descriptive note
                        string _note = "<em>" + wwi_func.get_from_global_resx(_tmp[_dx] + "_Note") + "</em>";
                        //build summary of results for each query name include report name even if no records found and thereefore no csv for that report 
                        _html += "<li>" + _tmp[_dx].Replace("_", " ") + ": " + _records.ToString() + " records<br/>" + _note + "</li>";
                        //
                    }//end loop through datatables

                    //append list tags
                    _html = "<ul>" + _html + "</ul>";

                    //send mail if we have attachments
                    if (_csvs.Count > 0)
                    {
                        string[] _to = { _mailto, "services@publiship-online.com" };
                        //string[] _to = { "paule@publiship.com" };
                        //email
                        send_spreadsheet(_to, "Discrepancy reports " + DateTime.Now.ToShortDateString() + " for " + _controller, _html, _controller, _csvs);
                    }
                }//end loop through users
            }//end if table.count

            //output xml  
            _xml = serialize_list(_tmp);
            return _xml;
        }
        //end process discrepancy report

        protected DataTable process_report(string reportname, string sdate1, string sdate2)
        {
            //only needed for connection string if we convert to a script
            //string _server = "WWISQL";
            //string _dbname = "PublishipSQL";
            //string _userid = "sa";
            //string _pw = "BaldblokeVN900";
            //get query from global resources
            //datatable used for filtering below
            //make sure _dc and _dt have tablenames or you will not be able to parse into xml 
            //
            DataTable _dc = new DataTable(reportname.Replace(" ", ""));
            string _query = wwi_func.get_from_global_resx(reportname).Replace("@inputdate1",sdate1).Replace("@inputdate2",sdate2);
            
            if (!string.IsNullOrEmpty(_query))
            {
                ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                SqlConnection _cn = new SqlConnection();
                _cn.ConnectionString = _cts.ConnectionString;
                //use this if we decide to create a seperate script
                //SqlConnection _cn = new SqlConnection("server=" + _server + ";Trusted_Connection=false;database=" + _dbname + ";Pwd=" + _pw + ";User ID=" + _userid + ";");
                _cn.Open();

                SqlDataAdapter _da = new SqlDataAdapter(_query, _cn);
                
                try
                {
                    _da.Fill(_dc);
                }

                catch (Exception e)
                {
                    _dc = null;
                    string _ex = e.Message.ToString(); 
                    //091012 return error string
                    ////add a row to botom of dc with error message in it
                    //***

                    //DataRow _row = _dc.NewRow();
                    //_row["Controller"] = "Error";
                    //_row["hfEmail"] = e.Message.ToString();
                    //_dc.Rows.Add(_row); 

                    //_result = e.Message.ToString();
                }

                finally
                {
                    if (_cn.State == ConnectionState.Open)
                        _cn.Close();
                }
            }
            //091012 can't send xml from here as there might be more than 1 query to process so return either error or name of tmp file
            //returns xml of all users emailed or if it stopped at errror
            //****
            //System.IO.StringWriter _writer = new System.IO.StringWriter();
            //_dc.WriteXml(_writer, XmlWriteMode.WriteSchema, false);
            //_result = _writer.ToString();
          
            return _dc;
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
                            DateTime _exw = !string.IsNullOrEmpty(_cell) ? (DateTime)_dr["ExWorksDate"] : DateTime.MinValue;
                            DateTime _ets = !string.IsNullOrEmpty(_dr["ETS"].ToString()) ? (DateTime)_dr["ETS"] : DateTime.MinValue;

                            if (_exw != DateTime.MinValue && _ets != DateTime.MinValue)
                            {
                                if (_exw > _ets) { _cell += "<br>Ex-Works Date later than Sailing"; }
                            }
                        }
                        else if (_dc.ColumnName == "ETS" || _dc.ColumnName == "ETA")
                        {
                            _cell = str_format(_cell);
                        }
                        else if (_dc.ColumnName == "SOB")
                        {
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

        public static string return_formatted_csv(DataTable dt)
        {
            StringBuilder _sb = new StringBuilder(); 
            string _line = "";
            string _cell = "";
            char[] _ch = ",".ToCharArray();
 
            //DataTable _ndt = new DataTable(); 
            //for loop is 2x faster than foreach
            foreach (DataColumn _dc in dt.Columns)
            {
                if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do not include in table
                {
                    _line += _dc.ColumnName.ToString() + ","; 
                }
            }
            _sb.AppendLine(_line.TrimEnd(_ch)); 
            //*******

            //*****Add data rows
            foreach (DataRow _dr in dt.Rows)
            {
                _line = "";

                foreach (DataColumn _dc in dt.Columns)
                {
                    if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do no include in table
                    {
                        _cell = _dr[_dc.ColumnName].ToString().Trim();

                        if (_dc.ColumnName == "ExWorksDate")
                        {
                            _cell = str_format(_cell);

                            //datetime validation if we have ETS
                            if (dt.Columns.Contains("ETS"))
                            {
                                DateTime _exw = !string.IsNullOrEmpty(_cell) ? (DateTime)_dr["ExWorksDate"] : DateTime.MinValue;
                                DateTime _ets = !string.IsNullOrEmpty(_dr["ETS"].ToString()) ? (DateTime)_dr["ETS"] : DateTime.MinValue;

                                if (_exw != DateTime.MinValue && _ets != DateTime.MinValue)
                                {
                                    if (_exw > _ets) { _cell += " Ex-Works Date later than Sailing"; }
                                }
                            }
                        }
                        else if (_dc.ColumnName == "ETS" || _dc.ColumnName == "ETA")
                        {
                            _cell = str_format(_cell);
                        }
                        else if (_dc.ColumnName == "SOB")
                        {
                            _cell = _cell == "False" ? "No" : "Yes";
                        }
                        else if (_dc.ColumnName == "JobClosed")
                        {
                            _cell = _cell == "False" ? "No" : "Yes";
                        }

                        _line += _cell + ",";
                        _cell = "";
                    }
                }
                _sb.AppendLine(_line.TrimEnd(_ch));  
            }
            //*****

            //return string 
            return _sb.ToString();
        }
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

        /// <summary>
        /// Sends an mail message
        /// use this simplified version instead of mailhelper class
        /// so we can stram attachment without having to save it as a file
        /// </summary>
        public static string send_spreadsheet(string[] sendto, string subject, string msg, string named, IList<Attachment>attachments)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();
            string _result = string.Empty;

            // Set the sender address of the mail message
            //do we just default from web.config?
            //mMailMessage.From = new MailAddress(mrcTo.rEmail);
            for (int _ix = 0; _ix < sendto.Length; _ix++)
            {
                mMailMessage.To.Add(new MailAddress(sendto[_ix]));
            }
            // Set the recipient address of the mail message
            if (!String.IsNullOrEmpty(msg))
            {
                
                if (attachments.Count > 0)
                {
                    for (int _ix = 0; _ix < attachments.Count; _ix++)
                    {
                        // Stream the Excel spreadsheet to the client in a format
                        // compatible with Excel 97/2000/XP/2003/2007/2010.
                        //System.IO.MemoryStream _stream = new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(attached)); 
                        //Attachment _a = new Attachment(_stream, named + ".csv");
                        mMailMessage.Attachments.Add(attachments[_ix]);
                    }
                }

                // Set the subject of the mail message
                mMailMessage.Subject = subject;
                // Set the body of the mail message
                mMailMessage.Body = msg;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                try
                {
                    SmtpClient mSmtpClient = new SmtpClient();
                    string ConfigPath = "~\\Web.config";
                    Configuration configurationFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConfigPath);
                    //send the message
                    System.Net.Configuration.MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as System.Net.Configuration.MailSettingsSectionGroup;

                    if (mailSettings != null)
                    {

                        int port = mailSettings.Smtp.Network.Port;
                        string host = mailSettings.Smtp.Network.Host;
                        string password = mailSettings.Smtp.Network.Password;
                        string username = mailSettings.Smtp.Network.UserName;

                        mSmtpClient.Port = Convert.ToInt32(port);

                        if (username != null && username != "")
                        {
                            //to authenticate we set the username and password properites on the SmtpClient
                            mSmtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                        }
                        // Send the mail message
                        //*****************
                        mSmtpClient.Send(mMailMessage);
                        //*****************
                    }

                }
                catch (SmtpException ex)
                {
                    //A problem occurred when sending the email message
                    _result = ex.ToString();
                }
            }
            return _result;
        }
        //end send email

        public static string get_excel_columnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        //end get column name

        public static string serialize_list(IList<string> items)
        {
            
            XmlSerializer _serializer = new XmlSerializer(typeof(List<string>));
            StringWriter _sw = new StringWriter();
            XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
            _ns.Add("", "");

            _serializer.Serialize(_sw, items, _ns);

            return _sw.ToString(); //xml string
        }

        private static IList<empInfo> get_user_ids()
        {
            //return DB.Select(EmployeesTable.Columns.EmployeeID, EmployeesTable.Columns.Name, EmployeesTable.Columns.EmailAddress).From<EmployeesTable>().Where(EmployeesTable.Columns.EmailAddress).IsNotEqualTo("").And(EmployeesTable.Columns.DepartmentID).IsEqualTo(1).Or(EmployeesTable.Columns.DepartmentID).IsEqualTo(3).ExecuteTypedList<empInfo>();
            return DB.Select(EmployeesTable.Columns.EmployeeID, EmployeesTable.Columns.Name, EmployeesTable.Columns.EmailAddress).From<EmployeesTable>().Where(EmployeesTable.Columns.EmailAddress).IsNotEqualTo("").And(EmployeesTable.Columns.OfficeID).IsEqualTo(1).And(EmployeesTable.Columns.Live).IsEqualTo(-1).ExecuteTypedList<empInfo>();
        }
        //end serialiase list

        public static bool in_schedule(string reportname)
        {
            bool _result = false;

            //get day of week using globalisation to it will work anywhere in world
            string _dayofweek = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)System.DateTime.Now.DayOfWeek];
            //check report has a schedule or not
            string _schedule = wwi_func.get_from_global_resx(reportname + "_Schedule");

            if (string.IsNullOrEmpty(_schedule) || _schedule.Contains(_dayofweek))
            {
                _result = true;
            }

            return _result;
        }
        //end in schedule


    }//end class
}

