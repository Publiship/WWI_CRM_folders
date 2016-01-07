using System;
using System.Web;
using System.Web.UI;
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using SubSonic;
using DevExpress.Web;
 
namespace DAL.Logistics
{
    /// <summary>
    /// Summary description for wwi_func
    /// </summary>
    
    public class wwi_func
    {
        public wwi_func()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        /// <summary>
        /// derive the 18th digit (check digit) for an sscc (serial shipping container) bar code
        /// From the right to left, start with odd position, assign the odd/even position to each digit.
        /// Sum all digits in odd position and multiply the result by 3.
        /// Sum all digits in even position.
        /// Sum the results of step 3 and step 4.
        /// Divide the result of step 4 by 10. The check digit is the number which adds the remainder to 10.
        /// sscc check digit test
        /// 00718908562723189 should return 6
        /// 12345678901234567 should return 5
        /// 00000000000000000 should return 0
        /// </summary>
        /// <param name="sscc_code">17 digit code</param>
        /// <returns>string containing check digit or error message</returns>
        public static string sscc_checkdigit(string sscc_code)
        {
            string _check = ""; //returns check digit or message if there is an error
            int _odds = 0; //sum of values in odd positions
            int _evens = 0; //sum of values in even positions
            int _mod = 0; //modulus result for step 4
            int _multi = 3; //multiplier for odds
            int _div = 10; //divider for step 4

            if (sscc_code.Length == 17) //must have 17 digits
            {
                //get sum of odd numbers and even numbers from right to left. length -1 string methods are 0 based
                for (int _ix = sscc_code.Length - 1; _ix >= 0; _ix--)
                {
                    //get integer at position _ix
                    int _digit = vint(sscc_code.Substring(_ix, 1));

                    //1 and 2. check if position _ix is odd or even and add to appropriate total
                    if (is_odd(_ix + 1))
                    {
                        _odds += _digit;
                    }
                    else
                    {
                        _evens += _digit;
                    }
                }//end loop

                //3. multiple odds by 3
                _odds = _odds * _multi;
                //4. add odds and evens and divide by 10 to get modulus
                _mod = (_odds + _evens) % _div;
                //5. subtract sum from 10  to get check digit 
                //result is 10 minus modulus. if result > 9 return 0
                _div = _div - _mod;
                //if final result is not a single digit return 0
                _check = _div >= 0 && _div <= 9 ? _div.ToString() : "0";
            }
            else
            {
                _check = "SSCC Please enter 17 digits with no spaces";
            }

            return _check;
        }

        /// <summary>
        /// returns true if a number is odd
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool is_odd(int value)
        {
            return value % 2 != 0;
        }
        //end ssc_checkdigit

        /// <summary>
        /// build html table from datatable
        /// </summary>
        /// <param name="dt">datatable to convert int html</param>
        /// <returns></returns>
        public static string get_html_table(DataTable dt)
        {
            StringBuilder _sb = new StringBuilder();
            string _tbl = get_from_global_resx("cargo_updated_table");
            string _th = get_from_global_resx("cargo_updated_th");
            string _td = get_from_global_resx("cargo_updated_td");

            //define table
            _sb.Append("<table style='" + _tbl + "'>");
            
            //*****Add a headings row.
            _sb.Append("<tr style='"+ _th + "'>");

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
            //for (int i = 0; i <= dt.Columns.Count; i++)
            //{
            //    if (!dt.Columns[i].ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do not include in table
            //    {
            //        _sb.Append("<td align='left' valign='top'>");
            //        _sb.Append(dt.Columns[i].ColumnName.ToString().Trim());
            //        _sb.Append("</td>");
            //    }
            //}
            _sb.Append("</tr>");
            //*******

            //for (int r = 0; r <= dt.Rows.Count; r++)
            //{
            //    _sb.Append("<tr>");
            //    
            //    for (int i = 0; i <= dt.Columns.Count; i++)
            //    {
            //        if (!dt.Columns[i].ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do not include in table
            //        {
            //            _sb.Append("<td style='" + _td + "'>");
            //            _sb.Append(str_format(dt.Rows[r][i].ToString().Trim()));
            //            _sb.Append("</td>");
            //        }
            //    }
            //    
            //    _sb.Append("</tr>");
            //}

            //*****Add data rows
            foreach (DataRow _dr in dt.Rows)
            {
                _sb.Append("<tr>");
            
            
                foreach (DataColumn _dc in dt.Columns)
                {
                    if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do no include in table
                    {
                        _sb.Append("<td style='" + _td + "'>");
                        _sb.Append(str_format(_dr[_dc.ColumnName].ToString().Trim()));
                        _sb.Append("</td>");
                    }
                }
                _sb.Append("</tr>");
            }
            //*****
            //end table definition
            _sb.Append("</table>");

            //return string 
            return _sb.ToString();
        }

        public static string datatable_to_csv(DataTable dt)
        {
            StringBuilder _sb = new StringBuilder();
            string _temp = "";
            char[] _ch = ",".ToCharArray();
 
            //for loop is 2x faster than foreach
            foreach (DataColumn _dc in dt.Columns)
            {
                if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do not include in table
                {
                    _temp += _dc.ColumnName.ToString().Trim() + ",";
                }
            }
            _sb.AppendLine(_temp.TrimEnd(_ch)); 
            //*******

            //*****Add data rows
            foreach (DataRow _dr in dt.Rows)
            {
                _temp = "";

                foreach (DataColumn _dc in dt.Columns)
                {
                    if (!_dc.ColumnName.ToString().StartsWith("hf")) //hf=hidden fields e.g. email addresses we do no include in table
                    {
                        _temp += _dr[_dc.ColumnName].ToString().Trim() + ",";
                    }
                }

                _sb.AppendLine(_temp.TrimEnd(_ch));  
            }
            //*****
            
            //return string 
            return _sb.ToString();
        }
        /// <summary>
        /// build string from datatable
        /// </summary>
        /// <param name="dt">datatable to convert int html</param>
        /// <returns></returns>
        public static string datatable_to_string(DataTable dt, string seperator)
        {
            StringBuilder _sb = new StringBuilder();
            //for loop is 2x faster than foreach
            
            for (int r = 0; r <= dt.Rows.Count; r++)
            {
                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    string _seperator = i < dt.Columns.Count ? seperator : ""; 
                    _sb.Append(dt.Rows[r][i].ToString().Trim() + _seperator);
                }
                
            }

            //return string 
            return _sb.ToString();
        }
        //end datatable to string 
        /// <summary>
        /// build string array from datatable
        /// </summary>
        /// <param name="dt">datatable to convert int html</param>
        /// <returns></returns>
        public static string[] datatable_to_array(DataTable dt, int col)
        {
            List<string> _ls = new List<string>();
            //for loop is 2x faster than foreach

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                if(!dt.Rows[r].IsNull(col)){ _ls.Add(dt.Rows[r][col].ToString()); }
            }

            //return string array
            return _ls.ToArray();
        }
        //end datatable to string 
        /// <summary>
        /// get data table using supplied sql
        /// </summary>
        /// <param name="qry">sql query used to return data</param>
        /// <param name="updguid">identifying packed quid by which the selected data is grouped on append</param>
        /// <returns>datatable</returns>
        public static DataTable get_datatable(string qry, Boolean iskey, string updguid)
        {
            //using a collection as it's the only way to directly apply a query string!
            //query to grid
            //
            DataTable _dt = new DataTable();

            try
            {
                if (iskey) //retrive query from keyvalue repository using qry as key
                {
                    qry = get_from_global_resx(qry);
                }

                if (!qry.StartsWith("<error>"))
                {
                    //check to see if we have a company id
                    //user MUST be logged in for advanced searches
                    ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                    SqlConnection _cn = new SqlConnection();
                     
                    _cn.ConnectionString = _cts.ConnectionString;
                    _cn.Open();

                    SqlCommand _cmd = new SqlCommand(qry, _cn);
                    _cmd.Parameters.AddWithValue("@qryguid", updguid);
                    SqlDataAdapter _da = new SqlDataAdapter();
                    _da.SelectCommand = _cmd;
                    _da.Fill(_dt); 

                    _cmd.Dispose();
                    _cn.Close();
                    _cn.Dispose();
                }

            }
            catch(Exception _ex)
            {
                string _x = _ex.Message.ToString(); 
                _dt = null;
            }
            return _dt;
        }

        
        /// <summary>
        /// return value from global resource
        /// </summary>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static String get_from_global_resx(string skey)
        {
            String _result = String.Empty;

            // Get the global resource string.

            try
            {

                _result = (String)System.Web.HttpContext.GetGlobalResourceObject("wwi_global", skey);

            }

            catch(Exception _ex)
            {

                _result = "<error>" + _ex.Message.ToString();
            }

            return _result; 
        }

        /// <summary>
        /// return unique field values for selected column in datatable
        /// </summary>
        /// <param name="dt">datatable containing records to search</param>
        /// <param name="col">fieldname to derive unique values from</param>
        /// <returns></returns>
        public static ArrayList get_distinct_value(DataTable dt, string col)
        {
            ArrayList _values = new ArrayList();
            string _last = null;
            
            //for(int i=0; i <= dt.Rows.Count; i ++) CHECK THIS CODE!
            foreach (DataRow _dr in dt.Select("", col)) 
            {
                //if ((dt.Rows[i][col].ToString() != "") && (dt.Rows[i][col].ToString() != _last))
                if ((_dr[col].ToString() != "") && (_dr[col].ToString() != _last))
                {
                    //_last = dt.Rows[i][col].ToString();
                    //_values.Add(dt.Rows[i][col].ToString());   
                    _last = _dr[col].ToString();
                    _values.Add(_dr[col].ToString());  
                }
            }

            return _values;
        }
        /// <summary>
        // This produces a 22-character string representation of a GUID. It contains
        // only alphanumerics, - and _. These characters aren't affected by URL encoding,
        // which makes them a tad easier to pass around in query strings and suchlike.
        /// </summary>
        /// <param name="guid">full guid</param>
        /// <returns></returns>
        public static string pack_guid(Guid guid)
        {
            
            // The Base64 encoded version of the GUID always ends in two equals signs,
            // so removing them loses no uniqueness.
            return Convert.ToBase64String(guid.ToByteArray()).Replace('+', '_').Replace('/', '-').Replace("=", "");
        }

        /// <summary>
        /// Remove sql escape characters from text to prevent injection attacks
        /// escape single quote to double ' to '' (possible the most important
        /// remove @, +, [, ], ;, =
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string no_sql(string value)
        {
            string[] _ch = { "'", "@", "+", "[", "]", ";", "=" };
            string _result = "";

            if (!string.IsNullOrEmpty(value))
            {
                for (int _ix = 0; _ix < _ch.Length; _ix++)
                {
                    if (_ch[_ix] == "'")
                    {
                        value = value.Replace(_ch[_ix], _ch[_ix] + _ch[_ix]);
                    }
                    else
                    {
                        value = value.Replace(_ch[_ix], " ");
                    }
                }
            }
            _result = value;
            
            return _result;
        }
        //end no sql

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


        /// <summary>
        /// remove all groupings from aspxdatagrid
        /// </summary>
        /// 
        public static void remove_dxgrid_grouping(DevExpress.Web.ASPxGridView.ASPxGridView grid)
        {
             try
            {
                for (int i = 0; i < grid.Columns.Count; i++)
                    if (grid.Columns[i] is DevExpress.Web.ASPxGridView.GridViewDataColumn)
                    {
                        DevExpress.Web.ASPxGridView.GridViewDataColumn _gridViewDataColumn = (DevExpress.Web.ASPxGridView.GridViewDataColumn)grid.Columns[i];
                        if (_gridViewDataColumn.GroupIndex > -1)
                            grid.UnGroup(_gridViewDataColumn);
                    }
            }
            catch (Exception ex)
            {
                string _result = ex.Message.ToString(); 
                //Response.Write(ex.Message.ToString());  
                //this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
            }
        }
        //end remove dx grid grouping


        /// <summary>
        /// getexchangerate
        /// </summary>
        /// <param name="_rate">e.g. GBP (british pund), THB (thai baht), AUD (australian dollars)</param>
        /// <returns>double currency exchange rate</returns>
        public static double getexchangerate(string _currfrom, string _currto)
        {
            // prepare the web page we will be asking for
            // this site is based on Euros so we will need to convert GPB to Euro then Euro to e.g. USD
            string _exratesource = "http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml";
            double _dblrate = 1; //default returned rate to 1
            //Encoding ascii =  Encoding.ASCII;

            try
            {
                System.Net.HttpWebRequest _request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_exratesource);

                System.IO.StreamReader _exratedata = new System.IO.StreamReader(_request.GetResponse().GetResponseStream());
                System.Xml.XmlDocument _xml = new System.Xml.XmlDocument();
                _xml.Load(_exratedata);
                _exratedata.Close();

                //rate 1 first
                System.Xml.XmlNode _nodef = _xml.SelectSingleNode("*/*/*/*[@currency='" + _currfrom + "']/@rate");
                double _f = 1;
                if (_nodef != null)
                {
                    _f =  vdouble(_nodef.Value);
              
                }

               //then rate 2 against rate 1
                double _t = 1;
                System.Xml.XmlNode _nodet = _xml.SelectSingleNode("*/*/*/*[@currency='" + _currto + "']/@rate");
                if (_nodet != null)
                {
                    _t = vdouble(_nodet.Value);
                
                }

                //actual rate
                Math.Round(_dblrate = _t / _f, 2);
            }
            catch
            {
                _dblrate = 0; //return 0 is an error
            }

            return _dblrate;
        }
        //end get exchange rate

        /// <summary>
        /// parse string to see if it can be converted to int 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        public static bool? vbool(string svalue)
        {
            bool _result = false;
            bool _test = false;

            _test = bool.TryParse(svalue, out _result);

            return _result;
        }
        /// <summary>
        /// parse string to see if it can be converted to int 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        public static int vint(string svalue)
        {
            int _result = 0;
            Boolean _test = false;

            _test = int.TryParse(svalue, out _result);

            return _result;
        }
        /// <summary>
        /// parse string to see if it can be converted to double 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        public static double vdouble(string svalue)
        {
            double _result = 0;
            Boolean _test = false;

            _test = Double.TryParse(svalue, out _result);

            return _result;
        }
        //end vdouble

        public static DateTime vdatetime(string svalue)
        {
            DateTime _result = DateTime.MinValue;

            Boolean _test = false;

            _test = DateTime.TryParse(svalue, out _result);

            return _result;
        }

        /// <summary>
        /// parse string to see if it can be converted to decimal 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        public static Decimal vdecimal(string svalue)
        {
            Decimal _result = 0;
            Boolean _test = false;

            _test = Decimal.TryParse(svalue, out _result);

            return _result;
        }
        //end vdecimal

        /// <summary>
        /// parse string to see if it can be converted to float 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        public static float vfloat(string svalue)
        {
            float _result = 0;
            Boolean _test = false;

            _test = float.TryParse(svalue, out _result);

            return _result;
        }
        //end vfloat

        //returns the IP address of the user requesting a page
        public static string user_RequestingIP()
        {
            string strIpAddress;

            strIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (strIpAddress == null)

                strIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


            return strIpAddress;
        }
        //end user requesting ip

        /// <summary>
        /// return company group for user
        /// company group modifies the pricer spreadsheet to use
        /// and also the country/destination lists
        /// </summary>
        /// <returns></returns>
        public static Int32 get_company_group()
        {
            Int32 _cg = 0;

            if (HttpContext.Current.Session["user"] != null)
            {
                _cg = ((Int32)((UserClass)HttpContext.Current.Session["user"]).companyGroup);
            }

            return _cg;
        }
        //end get company group

        //generate new folder
        public static int file_count(string dirname)
        {
            int _filecount = 0;

            _filecount = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(dirname)).Length;

            return _filecount;
        }
        //end get file count

        /// <summary>
        /// get arraylist of named element values from an xml file
        /// </summary>
        /// <param name="xmlfile">path to xml file</param>
        /// <param name="filtername">name of element to use as a filter</param>
        /// <param name="filtervalue">value of filter</param>
        /// <param name="returnelement">name of element from which to return values</param>
        /// <returns></returns>
        public static ArrayList array_from_xml_deprecated(string xmlfile, string filtername, string filtervalue, string returnelement)
        {

            ArrayList _results = new ArrayList();

            try
            {
                string _path = AppDomain.CurrentDomain.BaseDirectory;
                _path += xmlfile;

                // pass _qryFilter to have keyword-filter RSS Feed
                // i.e. _qryFilter = XML -> entries with XML will be returned
                DataSet _ds = new DataSet();
                _ds.ReadXml(_path);
               
                DataRow[] _rows;
                //apply filter if necessary
                if (!string.IsNullOrEmpty(filtername))
                {
                    _rows = _ds.Tables[0].Select(filtername + " ='" + filtervalue + "'");
                }
                else
                {
                    _rows = _ds.Tables[0].Select();
                }

                //enumerate through view and get the returnelement values
                for (int ix = 0; ix < _rows.Length; ix++)
                {
                    if (!string.IsNullOrEmpty(_rows[ix][returnelement].ToString())) { _results.Add(_rows[ix][returnelement].ToString()); }
                }
            }
            catch(Exception ex)
            {
                _results.Add(ex.Message.ToString());  
            }

            return _results; 
        }
        /// <summary>
        /// get araylist of named element values from an xml file
        /// </summary>
        /// <param name="xmlfile">path to xml file</param>
        /// <param name="filter">row filter e.g. elementname ='value'</param>
        /// <param name="returnelement">name of element from which to return values</param>
        /// <returns></returns>
        public static IList<string> array_from_xml(string xmlfile, string nodepath)
        {

            IList<string>  _results = new List<string>();

            try
            {
                string _path = AppDomain.CurrentDomain.BaseDirectory;
                _path += xmlfile;

                System.Xml.XmlDocument _xdoc = new System.Xml.XmlDocument();
                _xdoc.Load(_path);

                System.Xml.XmlNodeList _nodes = _xdoc.SelectNodes(nodepath);

                foreach (System.Xml.XmlNode _n in _nodes)
                {
                    string _text = _n.InnerText.ToString();
                    if (!string.IsNullOrEmpty(_text)) { _results.Add(_text); }
                }
     
            }
            catch (Exception ex)
            {
                _results.Add(ex.Message.ToString());
            }

            return _results;
        }
        /// <summary>
        /// get count of named element values from an xml file
        /// </summary>
        /// <param name="xmlfile">path to xml file</param>
        /// <param name="filter">row filter e.g. elementname ='value'</param>
        /// <param name="returnelement">name of element from which to return values</param>
        /// <returns></returns>
        public static int count_elements_from_xml(string xmlfile, string nodepath)
        {
            int _nodecount = 0;

            try
            {
                string _path = AppDomain.CurrentDomain.BaseDirectory;
                _path += xmlfile;

                System.Xml.XmlDocument _xdoc = new System.Xml.XmlDocument();
                _xdoc.Load(_path);

                System.Xml.XmlNodeList _nodes = _xdoc.SelectNodes(nodepath);

                _nodecount = _nodes.Count;              

            }
            catch
            {
                _nodecount = -1;
            }

            return _nodecount;
        }

        /// <summary>
        /// split a string using seperator and return naemd element
        /// remove empty elements
        /// </summary>
        /// <param name="strvalue">string to split</param>
        /// <param name="strseperator">string seperator</param>
        /// <param name="intlement">element to return</param>
        /// <returns></returns>
        public static string get_string_element(string strvalue, string strseperator, int intelement)
        {
            string _result = "";
            string[] _elements = strvalue.Split(new string[] { strseperator }, StringSplitOptions.RemoveEmptyEntries);

            if (intelement <= _elements.Length - 1)
            {
                _result = _elements[intelement]; 
            }

            return _result; 
        }
        //end get element

        /// <summary>
        /// finds capitals in string and prefixes witbh a space unless following char is also a capital
        /// e.g. CamelCode = Camel Code, CamelCodeAGAIN = Camel Code AGAIN
        /// </summary>
        /// <param name="cameltext"></param>
        /// <returns></returns>
        public static string uncamel(string cameltext)
        {
            StringBuilder _sb = new StringBuilder();
            char[] _ch = cameltext.ToCharArray();
           
            for (int _ix = 0; _ix < _ch.Length; _ix++)
            {
                if (char.IsUpper(_ch[_ix]) && _sb.Length > 0)
                {
                    if (!char.IsUpper(_ch[_ix - 1])) { _sb.Append(" "); }
                }
                
                _sb.Append(_ch[_ix]); 
            }
               
            return _sb.ToString(); 
        }
        //end uncamel

        /// <summary>
        /// checks if the object is enumerable i.e. an array and returns as ineumerable is true
        /// else returns null
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>ienumerable</returns>
        public static IEnumerable get_object_to_ienum(object obj)
        {
            IEnumerable _enum = obj as IEnumerable;
            if (_enum != null)
            {
                return _enum;
            }
            return null;
        }
        //end get object to ienum 

        public static int lookup_match(string fieldname, string tablename, string fieldvalue)
        {
            int _result = 0;

            SqlQuery _qry = new Select(fieldname).From(tablename).Where(fieldname).IsEqualTo(fieldvalue);
            IDataReader _rd = _qry.ExecuteReader();
            while (_rd.Read())
            {
                _result += 1; //_rd[fieldname].ToString();
            }
            return _result;
        }
        /// <summary>
        /// check to see if a value already exists in database
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="tablename"></param>
        /// <param name="valuetofind"></param>
        /// <returns></returns>
        public static bool value_exists(string fieldname, string tablename, string valuetofind)
        {
            bool _result = false;

            SqlQuery _qry = new Select().From(tablename).Where(fieldname).IsEqualTo(valuetofind);
            object _obj = _qry.ExecuteScalar();
            if (_obj != null && (int)_obj > 0)
            {
                _result = true;
            }

            return _result;
        }
        /// <summary>
        /// lookups
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="tablename"></param>
        /// <param name="idname"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string lookup_value(string fieldname, string tablename, string idname, int? id)
        {
            string _result = "";

            SqlQuery _qry = new Select(fieldname).From(tablename).Where(idname).IsEqualTo(id);
            IDataReader _rd = _qry.ExecuteReader();
            while (_rd.Read())
            {
                _result = _rd[fieldname].ToString(); 
            }
            return _result;
        }
        public static string lookup_value(string fieldname, string tablename, string idname, int? id, bool firstpopulated)
        {
            string _result = "";
            bool _populated = false;
            SqlQuery _qry = new Select(fieldname).From(tablename).Where(idname).IsEqualTo(id);
            IDataReader _rd = _qry.ExecuteReader();
            while (_rd.Read() && !_populated)
            {
                if (_rd[fieldname].ToString() != "") { _result = _rd[fieldname].ToString(); _populated = true; }
            }
            return _result;
        }

        public static string lookup_multi_values(string fieldnames, string tablename, string idname, int? id)
        {
            string _result = "";
            string[] _cols = fieldnames.Split(",".ToCharArray());
 
            SqlQuery _qry = new Select(_cols).From(tablename).Where(idname).IsEqualTo(id);
            IDataReader _rd = _qry.ExecuteReader();

            while (_rd.Read())
            {
                for (int _cx = 0; _cx < _cols.Length; _cx++)
                {
                    string _f = _cols[_cx]; 
                    _result += _rd[_f].ToString() + Environment.NewLine; 
                }
            }
            return _result;
        }
        public static string lookup_multi_values(string fieldnames, string tablename, string idname, int? id, string delimiter)
        {
            string _result = "";
            string[] _cols = fieldnames.Split(",".ToCharArray());

            SqlQuery _qry = new Select(_cols).From(tablename).Where(idname).IsEqualTo(id);
            IDataReader _rd = _qry.ExecuteReader();

            while (_rd.Read())
            {
                for (int _cx = 0; _cx < _cols.Length; _cx++)
                {
                    string _f = _cols[_cx];
                    _result += _rd[_f].ToString() + delimiter;
                }
            }
            return _result;
        }

        /// <summary>
        /// find a value from named xml file
        /// </summary>
        /// <param name="xmlPath">xml file to search including folder name full path is derived from BaseDirectory</param>
        /// <param name="filterElement">xml element to filter on</param>
        /// <param name="filterValue">filter value</param>
        /// <param name="returnElement">xml element to get return value from</param>
        /// <param name="returnValue"></param>
        /// <returns>int defaults to 0</returns>
        public static int lookup_xml_value(string xmlPath, string filterElement, string filterValue, string returnElement)
        {
            int _result = 0; //value to return

            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath ;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if (_dv.Count > 0) { _result = wwi_func.vint(_dv[0][returnElement].ToString()) ; }
            return _result;
        }
        /// <summary>
        /// find a value from named xml file
        /// </summary>
        /// <param name="xmlPath">xml file to search including folder name full path is derived from BaseDirectory</param>
        /// <param name="filterElement">xml element to filter on</param>
        /// <param name="filterValue">filter value</param>
        /// <param name="returnElement">xml element to get return value from</param>
        /// <param name="returnValue"></param>
        /// <returns>string defaults to null</returns>
        public static string lookup_xml_string(string xmlPath, string filterElement, string filterValue, string returnElement)
        {
            string _result = "";
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if(_dv.Count > 0){  _result = _dv[0][returnElement].ToString();  }
            return _result;
        }
        /// <summary>
        /// returning all matched elements as a delimited string
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="filterElement"></param>
        /// <param name="filterValue"></param>
        /// <param name="returnElement"></param>
        /// <returns></returns>
        public static string lookup_xml_string(string xmlPath, string filterElement, string filterValue, string returnElement, bool allElementsDelimited)
        {
            string _result = "";
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if (_dv.Count > 0) {
                for (int _ix = 0; _ix < _dv.Count; _ix++)
                {
                    string _delimiter = _ix != _dv.Count - 1 ?  "," : "";
                    _result += _dv[_ix][returnElement].ToString() + _delimiter; 
                }
            }
            return _result;
        }
        //with a default string
        /// <summary>
        /// find a value from named xml file
        /// </summary>
        /// <param name="xmlPath">xml file to search including folder name full path is derived from BaseDirectory</param>
        /// <param name="filterElement">xml element to filter on</param>
        /// <param name="filterValue">filter value</param>
        /// <param name="returnElement">xml element to get return value from</param>
        /// <param name="returnValue"></param>
        /// <returns>defaulString if no match found</returns>
        public static string lookup_xml_string(string xmlPath, string filterElement, string filterValue, string returnElement, string defaultString)
        {
            string _result = defaultString;
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if (_dv.Count > 0) { _result = _dv[0][returnElement].ToString(); }
            return _result;
        }
        /// <summary>
        /// with a specified element to search
        /// </summary>
        /// <param name="xmlPath">xml file to search including folder name full path is derived from BaseDirectory</param>
        /// <param name="filterElement">xml element to filter on</param>
        /// <param name="filterValue">filter value</param>
        /// <param name="matchElement">element in filtered result set to search</param>
        /// <param name="matchValue">value of search element in filtered results</param>
        /// <param name="returnElement">element in filtered results to return</param>
        /// <returns>string</returns>
        public static string lookup_xml_string(string xmlPath, string filterElement, string filterValue, string matchElement, string matchValue, string returnElement)
        {
            bool _match = false;
            string _result = "";
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if (_dv.Count > 0)
            {
                if (matchValue != "")
                {
                    int _ix = 0;
                    while (!_match && _ix < _dv.Count)
                    {
                        if (_dv[_ix][matchElement].ToString()  == matchValue)
                        {
                            _result = _dv[_ix][returnElement] != null ? _dv[_ix][returnElement].ToString() : "";
                            _match = true;
                        }
                        _ix++;
                    } //end while
                }//endif
            }
            return _result;
        }

        /// <summary>
        /// find whether a named value exists in xml file
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="filterElement"></param>
        /// <param name="filterValue"></param>
        /// <param name="matchElement"></param>
        /// <param name="matchValue"></param>
        /// <returns></returns>
        public static bool find_in_xml(string xmlPath, string filterElement, string filterValue, string matchElement, string matchValue)
        {
            bool _match = false;
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += xmlPath;

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = filterElement + " ='" + filterValue + "'";

            if (_dv.Count > 0)
            {
                if (matchValue != "")
                {
                    int _ix = 0;
                    while (!_match && _ix < _dv.Count)
                    {
                        if (_dv[_ix][matchElement].ToString() == matchValue)
                        {
                            _match = true;
                        }
                        _ix++;
                    } //end while
                }//endif
            }
            return _match;
        }
        /// <summary>
        /// split a string into array and return selected array range as string
        /// </summary>
        /// <param name="sourcestring">string to split</param>
        /// <param name="startindex">starting element of array</param>
        /// <param name="endindex">ending element of array</param>
        /// <param name="demiter">string used to split</param>
        /// <returns>string result</returns>
        public static string return_split_str(string sourcestring, int startindex)
        {
            string _result = "";

            if (sourcestring != "")
            {
                string[] _lines = sourcestring.Split(Environment.NewLine.ToCharArray());

                for (int _ix = 0; _ix < _lines.Length; _ix++)
                {
                    if (_ix >= startindex)
                    {
                        _result += _lines[_ix];
                    }//end if
                } //end loop
            }//end if string != ""

            return _result;
        }
        public static string return_split_str(string sourcestring, int startindex, int endindex)
        {
            string _result = "";

            if (sourcestring != "")
            {
                string[] _lines = sourcestring.Split(Environment.NewLine.ToCharArray());

                for (int _ix = 0; _ix < _lines.Length; _ix++)
                {
                    if (_ix >= startindex && _ix <= endindex)
                    {
                        _result += _lines[_ix];
                    }//end if
                } //end loop
            }//end if string != ""

            return _result;
        }
        
        //overloaded string delimiter
        public static string return_split_str(string sourcestring, int startindex, int endindex, string delimiter)
        {
            string _result = "";
            
            if (sourcestring != "") {
                char[] _delim = delimiter.ToCharArray(); 
                string[] _lines = sourcestring.Split(_delim);

                for (int _ix = 0; _ix < _lines.Length; _ix++) {
                    if (_ix >= startindex && _ix <= endindex)
                    {
                        _result += _lines[_ix];
                    }//end if
                } //end loop
            }//end if string != ""

            return _result;
        }
        
        /// <summary>
        // clears entire app cache
        // An enumerator remains valid as long as the collection remains unchanged. If changes are made to the collection, such as adding, modifying,
        // or deleting elements, the enumerator is irrecoverably invalidated and the next call to MoveNext or Reset throws an InvalidOperationException. 
        // If the collection is modified between MoveNext and Current, Current returns the element that it is set to, even if the enumerator is 
        // already invalidated. This means that if we would try to remove items from the Cache directly while enumerating it, we could face 
        // Exceptions and unpredictable effects so the only clean way to do this is to first safely copy all the key names that currently exist in 
        // Cache into separate list.
        /// </summary>
        public void ClearApplicationCache()
        {
            List<string> keys = new List<string>();

            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator(); 

            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
        }

        public static string get_current_page_name()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string _sRet = oInfo.Name;
            return _sRet;
        } 
    }
}