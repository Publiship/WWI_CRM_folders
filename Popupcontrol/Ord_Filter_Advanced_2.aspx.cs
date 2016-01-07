using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using ParameterPasser;
using DevExpress.Web;

public partial class Ord_Filter_Advanced_2 : System.Web.UI.Page
{
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Session["user"] == null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "LOG_KEY", "window.parent.popWindows.ShowWindow(window.parent.popWindows.GetWindowByName('loginform'));", true);
            //ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", "window.parent.closefilterWindow();", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //31082011 check for page source pg=<encryped string>
            string _source = Request.QueryString["src"] != null ? wwi_security.DecryptString(Request.QueryString["src"].ToString(),"publiship") : "";

            switch (_source)
            {
                case "pod_search": //we don't save queries from the quick search pop-up no name is required
                    {
                        this.aspxlblName.ClientVisible = false;
                        this.aspxtxtName.ClientVisible = false; 
                        break;
                    }
                default:
                    {
                        this.aspxlblName.ClientVisible = true;
                        this.aspxtxtName.ClientVisible = true;
                        break;
                    }
            }
        }

        //if use clicked Clear button a the kill param will be set to "y"
        //so kill current data session before binding
        //if (this.dxhfparam.Contains("kill"))
        //{
        //    string _k = this.dxhfparam.Get("kill").ToString();
        //    if (_k == "y")
        //    {
        //        this.dxhfparam.Set("fname", ""); //clear field name/criteria so we can databind with no new row
        //        this.dxhfparam.Set("crits", "");
        //
        //        Session.Remove("querytable");
        //    }
        //}

        //default hide input boxes until we need them
        set_value_fields("", "");
        bind_session_data();

        //testing easyquery 
        //string path = AppDomain.CurrentDomain.BaseDirectory;
        //path += "xml\\WWI_easyq_model.xml";
        //Korzh.EasyQuery.DataModel model = new Korzh.EasyQuery.DataModel();
        //model.LoadFromFile(path);
        //
        //Korzh.EasyQuery.Query query = new Korzh.EasyQuery.Query();
        //query.Model = model;
        //Session["Query"] = query;
        //this.QueryPanel1.Query = (Korzh.EasyQuery.Query)Session["Query"]; 
    
  
    }
    //end load

    /// <summary>
    /// rebind datatable to session or create datatable if it does not exist
    /// </summary>
    protected void bind_session_data()
    {
        try
        {
            if (Session["querytable"] != null)
            {
                DataTable _dt = (DataTable)Session["querytable"];
                this.rptfilter.DataSource = _dt;
                this.rptfilter.DataBind();
            }
            else
            {
                DataTable _dt = build_query_table();
                Session["querytable"] = _dt;
                this.rptfilter.DataSource = _dt;
                this.rptfilter.DataBind();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //end bind session data
    protected DevExpress.Web.ASPxEditors.FilterControlColumnType get_column_type(String coltype)
    {
        DevExpress.Web.ASPxEditors.FilterControlColumnType _columntype = new DevExpress.Web.ASPxEditors.FilterControlColumnType();

        switch (coltype)
        {
            case "boolean":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Boolean;
                break;
            case "datetime":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.DateTime;
                break;
            case "decimal":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Decimal;
                break;
            case "default":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Default;
                break;
            case "double":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Double;
                break;
            case "integer":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Integer;
                break;
            case "string":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.String;
                break;
        }
        return _columntype;
    }

    /// <summary>
    /// temporary storage of query 
    /// </summary>
    private DataTable build_query_table()
    { 
        DataTable _table = new DataTable();
        _table.Columns.Add("fieldcaption", typeof(string));
        _table.Columns.Add("criteriacaption", typeof(string));
        _table.Columns.Add("values", typeof(string));
        _table.Columns.Add("criteria", typeof(string));
        _table.Columns.Add("fieldtype", typeof(string));
      
        return _table;
    }

    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //01122011 PME if user has created a query string but datatable is empty i.e. they did not click the + button to add to datatable
        //populate datatable with query row
        DataTable _dt = (DataTable)Session["querytable"];
        if (_dt.Rows.Count == 0 ) //force check to see whats in input boxes
        {
            datatable_add_row(); 
        }

        //string _magic = "\""; //set up quotes correctly in string 
        //this.txtQueryResult.Text = this.advancedFilter.FilterExpression.Replace("#", _magic).ToString(); //this.advancedFilter.FilterExpression.ToString();
        //System.Collections.Hashtable _htfilter = new System.Collections.Hashtable();
        //
        //12/10/2010 new search builder does not require parsing for Linq
        string _fex = get_query_from_rows(); //this.advancedFilter.GetFilterExpressionForOracle().ToString();
        //24/06/2011 for date in next/previous X days, parse out the current dates and replace with markers
        //so we can re-use the query

        //24/06/2011 if add to reports is ticked, flag in database so we can add to drop down on search form
        string _ck = this.dxckReport.Checked == true ? "___report" : "";
        string _fname = get_query_name() + _ck; 
        //force advanced search session to clear
        //09/06/2011 do not clear session so user can re-apply
        //Session.Remove("querytable");
        
        if (_fex != string.Empty)
        {
            //12/10/2010 new search builder does not require parsing for Linq, as the string
            //is formatted depending on the linq attribute in xml 
            //we just need to build the query from data rows in repeater
            //  
            //_fex = set_string_parser(_fex);
            //    
            //if (_fex.Contains("#"))
            //{
            //      _fex = set_datetime_parser(_fex);
            //}
            
            SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
            _sessionWrapper["query"] = _fex;
            _sessionWrapper["name"] = _fname;
            
            this.txtQueryResult.Text = _fname;

            //pass search params back to parent
            //ClientScript.RegisterStartupScript(GetType(), "QRY_KEY", "window.parent.lblmsgbox.SetText(txtQuery.GetText());", true);
            //close this window
            //ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", "window.parent.closefilterWindow();", true);
            //force reload of primary grid
            //ClientScript.RegisterStartupScript(GetType(), "DAT_KEY", "window.parent.grdOrder.PerformCallback();", true);
            //all one script
            //one script
            this.ClientScript.RegisterStartupScript(GetType(), "QRY_KEY", "window.parent.lblmsgbox.SetText(txtQuery.GetText());window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));window.parent.submit_query(2);", true);
        }
        else
        {  
            //just close popup
            //Session.Remove("querytable");
            ClientScript.RegisterStartupScript(GetType(), "EXT_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));", true);
        }
    }

    /// <summary>
    /// 12/10/2010 read every row in repeater and add to query string, bracketing as we build
    /// </summary>
    /// <returns></returns>
    protected string get_query_from_rows()
    {
        string _fex = "";

        if (Session["querytable"] != null)
        {
            DataTable _dt = (DataTable)Session["querytable"];
            
            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    string _ftype = _dr[4].ToString();
                    string _fvalue = _dr[3].ToString();

                    if (_ftype == "int") { _fvalue = _fvalue.Replace("'", ""); } 
                    if (!string.IsNullOrEmpty(_fex)) { _fex += " AND "; }
                    _fex += "(" + _fvalue + ")"; 
                }

                //replace single quotes ' with \" (escape character \ required so you can insert ")
                _fex = _fex.Replace("'", "\""); 
            }
        }
                
        return _fex;
    }

    protected string get_query_name()
    {
        string _fname = this.aspxtxtName.Text.ToString();
        DataTable _dt = (DataTable)Session["querytable"];

        if (string.IsNullOrEmpty(_fname))
        {
            if (_dt.Rows.Count > 0)
            {
                _fname = _dt.Rows[0]["fieldcaption"].ToString() + " " + _dt.Rows[0]["criteriacaption"].ToString() + " " + _dt.Rows[0]["values"].ToString();
            }
            else
            {
                _fname = "Advanced filter " + DateTime.Today.ToShortDateString();
            }
        }
        
        return _fname;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterexpression"></param>
    /// <returns></returns>
    protected string set_string_parser(string filterexpression)
    {
        string _fex = filterexpression;

        //parse for string comparisons like '%xyz', like '%xyz', like '%xyz%', like 'xyz'
        //replace single quotes with "
        if (_fex.Contains("like"))
        {
            if (_fex.Contains("'%") && !_fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".EndsWith(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else if (!_fex.Contains("'%") && _fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".StartsWith(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else if (_fex.Contains("'%") && _fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".Contains(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else
            {
                _fex = _fex.Replace(" like ", ".Contains(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
        }
        else if (_fex.Contains("= '"))
        {
            _fex = _fex.Replace(" = '", " == \"").Replace("'", "\""); 

        }
        
        //boolean operators, linq does not use is, is not
        _fex = _fex.Replace("not", " ! ");
        //
        _fex = _fex.Replace("is", " == ");

        //
        return _fex; 
    }

    /// <summary>
    /// surround datetime conditions with Datetime.Parse() or query string won't work Linq can't implicitly cast
    /// </summary>
    /// <param name="filterexpression"></param>
    protected string set_datetime_parser(string filterexpression)
    {
        string _fex = filterexpression;

        try
        {

            char[] _dlims = { '#' };
            string[] _words = _fex.Split(_dlims);
            Int32 _pos = 0;

            //foreach (string _s in _words)
            _fex = "";
            for (Int32 ix = 0; ix < _words.Length - 1; ix++) //every word except the last one
            {
                string _s = _words[ix];
                if (_pos == 0)
                {
                    _fex += _s + "DateTime.Parse(\"";
                    _pos = 1;
                }
                else
                {
                    _fex += _s + "\")";
                    _pos = 0;
                }
            }
            _fex += _words[_words.Length - 1]; //append last to end of string 
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());  
        }

        return _fex; 

    }

    /// <summary>
    /// call back called by javascript function when cboadvfield is updated
    /// rebind criteria dropdown based on field type e.f. string, int
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbocrits_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        try
        {
            //string _filter = e.Parameter.ToString();
            string _filter = this.dxcboadvfields.SelectedItem.GetValue("columntype").ToString();   
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\WWI_criteria_model.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "[TYPES] LIKE '%" + _filter + "%'";
            
            this.dxcbocrits.DataSource = _dv;
            this.dxcbocrits.DataBind();
            this.dxcbocrits.SelectedIndex = 0;

            set_value_fields(_filter, this.dxcbocrits.Value.ToString() ); 
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    /// <summary>
    /// call back from javascript function when critieria ddl is changed 
    /// check criteria requirements and display appropriaye value boxes
    /// e.g. single text box for "string equals" or 2 datepickers for "date between"
    /// simple Y/N combo for boolean
    /// </summary>
    protected void dxcbvalues_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

        string _types = this.dxcboadvfields.SelectedItem.GetValue("columntype").ToString(); //this.dxcboadvfields == null? "": this.dxcboadvfields.Value.ToString(); //field type 
        string _crits = e.Parameter == null? "": e.Parameter.ToString().ToLower(); //criteria string 
        
        set_value_fields(_types, _crits);  
    }

    protected void set_value_fields(string typefield, string typecrit)
    {
        int[] _vals = { 0, 0, 0, 0 };
 

        if (typefield != "" && typecrit != "" && !typecrit.Contains("null") )
        {
            if (typefield != "datetime")
            {
                _vals[0] = 1;

                if (typecrit.Contains("and")) { _vals[0] = 2; }
            }
            else if (typefield == "datetime")
            {
                _vals[1] = 1;

                if(typecrit.Contains("datetime.now"))
                {
                    _vals[1] = 0;
                    _vals[3] = 1; }
                else if (typecrit.Contains("and"))
                {
                    _vals[1] = 2;
                }
            
            }
            else if (typefield == "bool")
            {
                _vals[2] = 1;
            }
        }

        //show text boxes test vals[0]
        this.dxpanel1.Visible = _vals[0] > 0 ? true : false;
        this.dxpanel2.Visible = _vals[0] > 1 ? true : false;
        //show datepickers test vals[1]
        this.dxpanel3.Visible = _vals[1] > 0 ? true : false;
        this.dxpanel4.Visible = _vals[1] > 1 ? true : false;
        //show y/n test vals[2]
        this.dxpanel5.Visible = _vals[2] > 0 ? true : false; 
        //show n days combo
        this.dxpanel6.Visible = _vals[3] > 0 ? true : false; 
        
    }

    /// <summary>
    /// repeater call back panel fies when Add buton is hit
    /// via javascript function: updatefilters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbrepeater_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //rebuild datatable with new row and recreate session state
        try
        {
            //090811 check if user clicked Reset all button
            //if so, clear datatable and rebind
            string _reset = e.Parameter.ToString(); //passed from javascript function killfilters()
            if (_reset == "1")
            {
                this.dxhfparam.Set("kill", "n"); 
                Session.Remove("querytable");
                bind_session_data();//rebuild empty table

                DataTable _dt = (DataTable)Session["querytable"];
                this.rptfilter.DataSource = _dt;
                this.rptfilter.DataBind();
            }
            else  //add new row to datatable
            {
                datatable_add_row();
            }//end add new row
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        finally
        {
            //clear variables and hidden fields
            this.dxhfparam.Clear();
            clear_input_values();
        }
    }

    protected void datatable_add_row()
    {
        DataTable _dt = (DataTable)Session["querytable"];
        string _fieldtype = this.dxcboadvfields.SelectedItem.GetValue("columntype").ToString(); //field type
        string _fieldname = this.dxhfparam["fname"] != null ? this.dxhfparam["fname"].ToString() : "";
        string _criteria = this.dxhfparam["crits"] != null ? this.dxhfparam["crits"].ToString() : "";
        string _v1 = ""; //param 1
        string _v2 = ""; //param 2
        DateTime _testdate;

        if (!string.IsNullOrEmpty(_fieldname) && !string.IsNullOrEmpty(_criteria))
            //don't need to check for values if criteria ==null or !=null
            if (!_criteria.ToLower().Contains("null"))
            {
                if (_fieldtype == "string")  //check text boxes
                {
                    if (this.dxtxvalue1.Value != null) { _v1 = this.dxtxvalue1.Value.ToString(); }
                    if (this.dxtxvalue2.Value != null) { _v2 = this.dxtxvalue2.Value.ToString(); }
                }
                else if (_fieldtype == "int") //check text boxes
                {
                    if (this.dxtxvalue1.Value != null) { _v1 = this.dxtxvalue1.Value.ToString(); }
                    if (this.dxtxvalue2.Value != null) { _v2 = this.dxtxvalue2.Value.ToString(); }
                    //strip quotes out of crtieria
                    _criteria = _criteria.Replace("'", "");
                }
                else if (_fieldtype == "datetime")  //check datetimes
                {
                    if (!_criteria.Contains("Datetime.Now"))
                    {
                        if (this.dxdtvalue1.Value != null)
                        {
                            _testdate = (DateTime)this.dxdtvalue1.Value;
                            _v1 = _testdate.ToShortDateString();
                        }
                        if (this.dxdtvalue2.Value != null)
                        {
                            _testdate = (DateTime)this.dxdtvalue2.Value;
                            _v2 = _testdate.ToShortDateString();
                        }
                    }
                    else
                        if (this.dxspindays.Value != null)
                        {
                            Double _days = wwi_func.vdouble(this.dxspindays.Value.ToString());
                            _v1 = DateTime.Now.Date.ToShortDateString();
                            _v2 = this.dxcbocrits.Text.Contains("previous") ? DateTime.Now.AddDays(0 - _days).ToShortDateString() : DateTime.Now.AddDays(_days).ToShortDateString();
                        }
                }
                else if (_fieldtype == "bool")
                {
                    _v1 = this.dxcboyesno.Value.ToString();
                }
            }
            else
            {
                _v1 = " "; //just allows us to bypass value requirement below, it isn't used for anything
            }
        else
        {
            //fieldname or criteria where empty so don't process request
            _v1 = null;
        }

        if (!string.IsNullOrEmpty(_v1)) //need at last one value
        {
            _criteria = _criteria.Replace("{Datetime.Now}", "{1}"); //09/06/11 for date + next N days 
            _criteria = String.Format(_criteria, _fieldname, _v1, _v2);

            if (!this.dxcbocrits.Text.Contains("next") && !this.dxcbocrits.Text.Contains("previous"))
            {
                _v1 = !string.IsNullOrEmpty(_v2) ? _v1 + " and " + _v2 : _v1;
            }
            else
            {
                _v1 = this.dxspindays.Value.ToString() + " days";
            }

            _dt.Rows.Add(this.dxcboadvfields.Text, this.dxcbocrits.Text, _v1, _criteria, _fieldtype);
            Session["querytable"] = _dt; ;
            this.rptfilter.DataSource = _dt;
            this.rptfilter.DataBind();
        }
    }
    //end datatable add row

    protected void clear_input_values()
    {
        this.dxtxvalue1.Value = null;
        this.dxtxvalue2.Value = null;
        this.dxdtvalue1.Value = null;
        this.dxdtvalue2.Value = null;
        this.dxcboyesno.Value = null;
    }
   

    /// <summary>
    /// add javascript onclick event to attributes collection for each row in repeater 
    /// this will enable us to delete individual rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptfilter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //DevExpress.Web.ASPxEditors.ASPxButton _btn = (DevExpress.Web.ASPxEditors.ASPxButton)e.Item.FindControl("dxbtnremove");
        //DevExpress.Web.ASPxEditors.ASPxTextBox _txt = (DevExpress.Web.ASPxEditors.ASPxTextBox)e.Item.FindControl("dxtxtfilter");
        //
        //if (_txt != null)
        //{
        //    _btn.Attributes.Add("onclick", "removefilters(" + _txt.ClientID.ToString() + ");"); 
        //}
    }

    protected void rptfilter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _arg = e.CommandArgument.ToString();

        if (e.CommandName == "delete" && !string.IsNullOrEmpty(_arg))
        {
            DataTable _dt = (DataTable)Session["querytable"];
            

            //foreach (DataRow _dr in _dt.Rows) can't enumerate like this if we are planning on deleting rows have to count backwards
            for(int _i=_dt.Rows.Count-1;_i>=0;_i--)
            {
                string _criteria = _dt.Rows[_i]["criteria"].ToString();   //_dr["criteria"].ToString();
                if(_criteria  == _arg)
                {
                    _dt.Rows[_i].Delete();  //_dr.Delete(); 
                }
            }
            Session["querytable"] = _dt; 
            bind_session_data(); 
        }
    }

    /// <summary>
    /// 09/06/2011 client side callback (cbcancel.PerformCallback()) has been disabled to we can retain current querytable session and use it again
    /// call back control triggered when Cancel button is hit 
    /// to force query session to clear and therefore reset repeater
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbcancel_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        //Session["querytable"] = null;
        Session.Remove("querytable");
    }

    /// <summary>
    /// 23/06/2011 clear all crtieria from search. Kill datatable and rebind
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Session.Remove("querytable");
        bind_session_data(); 
    }
}

