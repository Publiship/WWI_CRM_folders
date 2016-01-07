using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DevExpress.Web.ASPxGridView;
using ParameterPasser;

public partial class cargo_updates : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Int32 _iseditor = Page.Session["user"] != null? (Int32)((UserClass)Page.Session["user"]).IsEditor: 0;

            if (_iseditor != 0)
            {
                if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
                {
                    //290413 additional filtering to restrict dataset by age (DateOrderCreated)
                    bind_dll_archive();
                }

                //additional filtering on user/company? 
                bind_combos();

                //07/03/2011 history sumary disabled, they can always follow lionk if they need to see it
                //MUST rebind repeater here or itemcommand will not function as expected
                //get_update_history("10");
                ////

                ////we are using linq datasource for speed this code is deprecated
                //if (Session["filter"] != null)
                //{

                //{ Session["filter"] = "OrderNumber = -1"; } //default so we do not load any records in start up
                //DataTable _dt = get_datatable();
                //this.gridOrder.DataSource = _dt;
                //this.gridOrder.DataBind();
                //if (_dt.Rows.Count == 1) { this.gridOrder.DetailRows.ExpandAllRows(); }
                ////
                //}
            }
            else
            {
                Response.Redirect("../Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());  
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if(!Page.IsPostBack)
        {
            this.gridOrder.SettingsEditing.Mode = GridViewEditingMode.Inline;
            reset_hidden();
        }

        if (!Page.ClientScript.IsClientScriptBlockRegistered("lg_key"))
        {
            register_client_scripts();
        }
      
        //new linq databinding 
        //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
        //bind_linq_datasource(); 
        //running in server mode
        this.LinqServerModeOrders.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModeOrders_Selecting);

    }

    #region databinding
    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModeOrders_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {

        ParameterCollection _params = new ParameterCollection();
        SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        Int32 _companyid = -1; //after testing default to empty string 

        //check if session created from advanced search, in which case we can use the parameters passed back
        string _query = "";
        string _name = "";
        string _mode = "";

        if (_sessionWrapper["mode"] != null) //this is only usually set when we pass it back from history search or logout
        {
            _mode = (string)_sessionWrapper["mode"];
            _sessionWrapper["mode"] = null;
        }
        else
        {
            _mode = this.dxhfMethod.Contains("mode") ? this.dxhfMethod["mode"].ToString() : "0"; //this.dxhfQuery["method"].ToString();
        }

        //mode 0=history, 1=basic search so rebuild filter, 2=advanced search, 3=user status report
        if (_mode == "0")
        {
            if (_sessionWrapper["query"] != null && _sessionWrapper["name"] != null)
            {
                _query = _sessionWrapper["query"].ToString();
                _name = _sessionWrapper["name"].ToString();
            }
        }
        else if (_mode == "1")
        {
            _query = get_filter();
            _name = get_name();
        }
        else if (_mode == "2")
        {
            if (_sessionWrapper["query"] != null && _sessionWrapper["name"] != null)
            {
                _query = _sessionWrapper["query"].ToString();
                _name = _sessionWrapper["name"].ToString();
            }
        }

        //company id: always add as a search param if user is logged in
        //if (Page.Session["user"] != null)
        //company id: always add as a search param if user is logged in UNLESS _mode = -1 which we can use to bypass params
        if (Page.Session["user"] != null && _mode != "-1" && _mode != "0")
        {
            _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;

            if (_companyid != -1) //-1 is a WWI company
            {
                Parameter _p = return_default_view(-1);
                if (_p != null) { _params.Add(_p); }
            }

            //11/03/2011 this is done below!
            //19/10/2010 if mode=3 add contact id/employee as param
            //if (_mode == "3")
            //{
            //    if (_companyid != -1) //-1 = ~WWI employee so need to use employee id
            //    {
            //        Parameter _p = return_default_view(1);
            //        if (_p != null) { _params.Add(_p); }
            //    }
            //    else
            //    {
            //        Parameter _p = return_default_view(3);
            //        if (_p != null) { _params.Add(_p); }
            //    }
            //    //for status report just show active jobs? or use combo below
            //    //_params.Add("JobClosed", Convert.ToBoolean("false").ToString());
            //} 
        }

        //check for additional parameters if query is not from history (mode=0)
        //but you can only apply these filters when a) user is logged in or b) a query parameter has been entered
        //otherwise you could create a default query of e.g. (Jobclosed==false) which would return ALL closed jobs in database
        //if ((_mode != "0") && (_query !="" | _companyid !=-1))
        //
        if ((_mode != "0" && _mode != "1") && (_query != "" || _params.Count > 0))
        {
            //28/08/2010 users can filter by individual contacts within the company
            //if its an employee we need to check
            //((OrderControllerID=={cboname.Value}) or (OperationsControllerID=={cboname.Value})) or
            //(OriginPortControllerID=={cboname.Value}) or (DestinationPortControllerID=={cboname.Value})) !!!
            //19/10/2010 only use contact id is user is not doing their own status report
            //if (_mode != "3" && this.cboName.Value != null)
            if (this.cboName.Value != null)
            {
                //11/03/2011 (All users) option added value = -1 use companyid to pull out all records for company instead of individual user
                //so we don't need to do anyhing as companyid has been parametised above
                if (!String.IsNullOrEmpty(this.cboName.Value.ToString()) && this.cboName.Value.ToString() != "-1")
                {
                    _params.Add("ContactID", this.cboName.Value.ToString());
                }

            }

            //15/09/2010 include closed jobs y/n when combo = Active jobs (jobclosed = 1) or combo = closed jobs (jobclosed = 0)
            //when combo = All jobs we can ignore this filter
            if (this.dxcboclosedyn.Value != null)
            {
                _params.Add("JobClosed", Convert.ToBoolean(this.dxcboclosedyn.Value).ToString());

            }
        }

        //additional filtering options e.g. today's updates
        if(this.dxhfMethod.Contains("hfparam"))
        {
            string _hfparam = this.dxhfMethod.Get("hfparam").ToString();

            switch (_hfparam)
            {
                case "today": {
                    _params.Add("dtupdated.Value.Date", "DateTime.Parse(\"" + DateTime.Today.ToShortDateString() + "\")");  
                    break;
                }
                default: {
                    break;
                }
            }

            this.dxhfMethod.Remove("hfparam"); 
        }

        //now build query with additional parameters
        string _f = "";
        if (_params.Count > 0)
        {
            foreach (Parameter p in _params)
            {
                string _a = _f != "" ? " AND " : "";
                _f += _a + "(" + p.Name.ToString() + "==" + p.DefaultValue.ToString() + ")";
            }

            if (_query != "") { _query = _f + " AND " + _query; } else { _query = _f; }
        }

        //get start date from dll value
        //e.g if search is for last 12 months dllvalue 1 start date = current date - (1 * 12) months
        //if search is for 2-3 years dllvalue 3 start date = current date - (3 * 12) months
        int _dllvalue = this.dxcboRange.Value != null ? wwi_func.vint(this.dxcboRange.Value.ToString()) : 1;
        //number of months to include after start date
        int _months = 12;
        //multiply base * 12 to get start date
        int _lowest = 0 - (_dllvalue * 12);
        DateTime _minDate = DateTime.Now.AddMonths(_lowest);

        //dynamic queries using system.Linq.dynamic + Dynamic.cs library
        //20/10/2010 we have build a unqiue index (OrderIx) from OrderId, TitleId, ContainerSubId as usual primary keys are not going to 
        //be unique in the view. aspxgrid only works properly when it has a unique key 
        e.KeyExpression = "OrderIx"; //"OrderID"; //a key expression is required 

        if (!string.IsNullOrEmpty(_query))
        {
            //var _nquery = new linq_classesDataContext().view_orders.Where(_query); //c => c.CompanyID == 7
            var _nquery = new linq.linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months).Where(_query); //c => c.CompanyID == 7
            e.QueryableSource = _nquery;
            //Int32 _count = _nquery.Count();

            if (!String.IsNullOrEmpty(_name)) { append_to_query_log(_query, _name); }
        }
        else //default to display nothing in grid 
        {
            //var _nquery = new linq_classesDataContext().view_orders.Where(c => c.OrderNumber == -1);
            var _nquery = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0).Where(c => c.OrderNumber == -1) ;
            //_count = _nquery.Count();
            e.QueryableSource = _nquery;
        }
    }

 

    /// <summary>
    /// once databinding complete, should we save any advanced searches
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_DataBound(object sender, EventArgs e)
    {
        //25/05/2011 switched off as it slows things down unnecessarily
        //append_to_query_log(); //save record to query log <moved to LinqServerModeOrders_Selecting>
        //19/10/2010 mode=3 axpand all
        
        //try
        //{
        //    string _mode = this.dxhfMethod.Get("mode").ToString();
        //
        //    if (_mode == "3")
        //    {
        //        this.btnExpandAll.Text = "Hide Detail";
        //        
        //        //this.gridOrder.DetailRows.ExpandAllRows();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message.ToString());
        //}
    }
    #endregion

    #region form events buttons, dll changed etc

    protected void btnEndGroup_Click(object sender, EventArgs e)
    {
        remove_grid_grouping();
    }

    /// <summary>
    /// rebind linq datasource
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuickFilter_Click(object sender, EventArgs e)
    {
        //clear session if there is one
        Session["filter"] = null;
        this.gridOrder.DataBind();
    }

    protected void btnExpandAll_Click(object sender, EventArgs e)
    {
        if (this.btnExpandAll.Text == "Show detail")
        {
            this.btnExpandAll.Text = "Hide detail";
            this.gridOrder.DetailRows.ExpandAllRows();
        }
        else
        {
            this.btnExpandAll.Text = "Show detail";
            this.gridOrder.DetailRows.CollapseAllRows();
        }
    }

    /// <summary>
    /// rebind grid so filter will be recreated including user id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clear session if there is one
        //Session["filter"] = null;
        this.gridOrder.DataBind();
    }

    /// <summary>
    /// remove filter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEndFilter_Click(object sender, EventArgs e)
    {
        this.txtQuickSearch.Text = "";
        this.cboName.Value = null;

        SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        _sessionWrapper["mode"] = null;
        _sessionWrapper["query"] = null;
        _sessionWrapper["name"] = null;

        //Session["filter"] = null; //so we don't save it again
        reset_hidden(); //sets mode back to default 0
        this.gridOrder.DataBind();
    }

    /// <summary>
    /// deprecated - using javscript submit query to do grd callback
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aspxbtnFilter_Click(object sender, EventArgs e)
    {
        //Session["filter"] = null;
    }

    protected void dxcboclosedyn_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridOrder.DataBind();
    }

    /// <summary>
    /// open search history form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkHistory_Click(object sender, EventArgs e)
    {
        //set mode to 0
        Response.Redirect("~/Ord_View_Cargo_Audit.aspx");
    }

    /// <summary>
    /// batch update button
    /// enum through hidden field storing 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        process_updates();
    }
    #endregion

    #region subs
    protected void reset_hidden()
    {
        this.dxhfMethod.Set("mode", -1);
    }

    //saves query, IP address, etc to database
    protected void append_to_query_log(string _query, string _name)
    {
        try
        {
            if (!string.IsNullOrEmpty(_query))
            {

                DbQueryLog _newlog = new DbQueryLog();

                _newlog.QryText = _query;
                _newlog.QryDesc = _name;
                _newlog.LogIp = userRequestingIP();
                _newlog.LogQryDate = DateTime.Now;
                _newlog.ByContactID = 0;
                _newlog.ByEmployeeID = 0;
                _newlog.QrySource = "OCE"; //order cargo edit

                if (Session["user"] != null)
                {
                    UserClass _thisuser = (UserClass)Session["user"];

                    if (_thisuser.CompanyId == -1)
                    { //employee
                        _newlog.ByContactID = 0;
                        _newlog.ByEmployeeID = _thisuser.UserId;
                    }
                    else
                    {
                        _newlog.ByContactID = _thisuser.UserId;
                        _newlog.ByEmployeeID = 0;
                    }
                }
                _newlog.Save();

                this.dxhfMethod.Set("status", 1);
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }

    /// <summary>
    /// remove all groupings from datagrid
    /// </summary>
    /// 
    protected void remove_grid_grouping()
    {
        try
        {
            for (int i = 0; i < this.gridOrder.Columns.Count; i++)
                if (this.gridOrder.Columns[i] is GridViewDataColumn)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.gridOrder.Columns[i];
                    if (gridViewDataColumn.GroupIndex > -1)
                        this.gridOrder.UnGroup(gridViewDataColumn);
                }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());  
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }


    protected void register_client_scripts()
    {
        // Gets a reference to a Label control that not in 
        // a ContentPlaceHolder
        DevExpress.Web.ASPxEditors.ASPxLabel mpLabel = (DevExpress.Web.ASPxEditors.ASPxLabel)Master.FindControl("lblResult");
        string _script = "";

        if (mpLabel != null)
        {
            _script = string.Format(@"function verify_user(){{
                    var us = document.getElementById('{0}').innerHTML; 
                    return us;
                    }}", mpLabel.ClientID);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "lg_key", _script, true);

        }
    }

    /// <summary>
    /// check user is logged in
    /// then build xml formatted string of records to update
    /// pass xml to server 
    /// stored procedure on sql server carries out actual data operations
    /// </summary>
    protected void process_updates()
    {

        DevExpress.Web.ASPxHiddenField.ASPxHiddenField _hf = this.dxhfeditor;
        if (Session["user"] != null)
        {
            Int32 _selected = _hf.Count;
            string _companyid = Convert.ToString((Int32)((UserClass)Page.Session["user"]).CompanyId);
            string _userid = Convert.ToString((Int32)((UserClass)Page.Session["user"]).UserId);
            string _updguid = wwi_func.pack_guid(Guid.NewGuid()); //unique identifier for this update group
            string _row = "<row orderid='{0}' cargoready='{1}' estpallets='{2}' estweight='{3}' estvolume='{4}' updguid='{5}' " +
                          "companyid='" + _companyid + "' userid='" + _userid + "'/>";

            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            _sb.Append("<root>");

            //element 0 is used to store OrderID
            foreach (KeyValuePair<string, Object> _item in _hf)
            {
                string[] _pars = _item.Value.ToString().Split(';'); //element 0 is order id
                string _newdate = !string.IsNullOrEmpty(_pars[0]) ? Convert.ToDateTime(_pars[0]).ToString("yyyy/MM/dd") : "";
                _sb.Append(String.Format(_row, _item.Key.ToString().Replace("key", ""), _newdate, _pars[1], _pars[2], _pars[3], _updguid));
            }

            _sb.Append("</root>");

            //pass to batch processing on server side
            batch_update(_selected, _sb.ToString(), _updguid);
        }
    }
 
#endregion

    #region cargo update processing
    /// <summary>
    /// step through selected grid items and add to data if checked
    /// </summary>
    protected void get_selected_rows(Int32 batchcommand)
    {
       
       //Int32 _selected = this.gridOrder.Selection.Count;
       string[] _fields = {"OrderID"};
       List<Object> _orderids =  this.gridOrder.GetSelectedFieldValues(_fields);
       Int32 _selected = _orderids.Count;
            
            //this only retrieves selected rows on visible page
            //for (int _rowidx = 0; _rowidx < this.gridOrder.VisibleRowCount; _rowidx++)
            // {
            //    if (this.gridOrder.Selection.IsRowSelected(_rowidx))
            //    {
            //        if (this.gridOrder.GetRowValues(_rowidx, "OrderID") != null)
            //        {
            //            string _item = this.gridOrder.GetRowValues(_rowidx, "OrderID").ToString();
            //            _sb.Append(String.Format(_updcargo, _item, _companyid, _userid));
            //        }
            //    }
            //}

        if (batchcommand == 1) //update  cargo
        {
            if ((Page.Session["updcargo"] != null) && (Page.Session["user"] != null))
            {
               update_cargo_from_session(_selected, _orderids);
            }
         }
    }
    /// <summary>
    /// update cargo ready info
    /// get derived xml from selected grid items
    /// pass to stored procedure
    /// returns: no. of items logged for update
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="orderxml"></param>
    protected void update_cargo_from_session(Int32 selected, List<Object> orderids)
    {
       
            string _updcargo = (String)Session["updcargo"];
            string _updguid = wwi_func.pack_guid(Guid.NewGuid());

            if(!String.IsNullOrEmpty(_updcargo))
            {
                //build xml rows
                System.Text.StringBuilder _sb = new System.Text.StringBuilder();
                
                _sb.Append("<root>");

                for (int _rowidx = 0; _rowidx <= orderids.Count - 1; _rowidx++)
                {
                    string _item = orderids[_rowidx].ToString();
                    _sb.Append(String.Format(_updcargo, _item, _updguid));
                }
                _sb.Append("</root>");

                //pass to batch porcessing on server side
                batch_update(selected, _sb.ToString(), _updguid);  
               
            }
        
        //Session["updcargo"] = null;
        Session.Remove("updcargo");
    }

    /// <summary>
    /// can be used to check batch is processing data correctly without doing database updates
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="strxml"></param>
    /// <param name="updguid"></param>
    protected void TEST_batch_update_TEST(Int32 selected, string strxml, string updguid)
    {
        string _msg = string.Empty; //email message
        try
        {
            if (!String.IsNullOrEmpty(strxml))
            {
                string _user = Page.Session["user"] != null? (string)((UserClass)Page.Session["user"]).UserName : "";
                string[] _to = { "hf3", "hf4" }; //email address columns company/ordercontroller
                string _emailed = MailHelper.gen_email("cargo_updated", true, updguid, _to, true, "Cargo Announced Ready  " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), _user);
                if (_emailed != string.Empty)
                {
                    //mail NOT sent
                    this.lblmsgboxdiv.Text ="<div class='innercentererror'>" + _emailed + "</div>";
                }
                else
                {
                    //email sent
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }
    /// <summary>
    /// use generated xml stream to update ordertable and append to order_update_log
    /// each batch is given a unique 22-character guid
    /// </summary>
    /// <param name="selected">count of orders being updated</param>
    /// <param name="strxml">pre-generated xml stream</param>
    /// <param name="updguid">unique id for this batch</param>
    protected void batch_update(Int32 selected, string strxml, string updguid)
    {

        try
        {
            if (!String.IsNullOrEmpty(strxml))
            {
                
                ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                SqlConnection _cn = new SqlConnection(_cs.ConnectionString);
                SqlCommand _cmd = new SqlCommand("sp_update_order_cargo", _cn);
                SqlParameter _rtv = new SqlParameter("@returnvalue", SqlDbType.Int);

                _rtv.Direction = ParameterDirection.ReturnValue;
                _rtv.DbType = DbType.Int32;

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@XMLCargo", strxml);
                _cmd.Parameters.Add(_rtv);

                using (_cn)
                {
                    _cn.Open();
                    //_cmd.ExecuteNonQuery();
                    //returns total number of records logged
                    SqlDataReader _rdr = _cmd.ExecuteReader();
                    Int32 _result = Convert.ToInt32(_rtv.Value);
                    
                    if ((_result != selected))
                    {
                        Response.Write("Update failed, please refer to Publiship IT support");
                    }
                    else
                    {
                        //email address columns company/ordercontroller
                        //derived from query stored in wwi_global.resx resource file
                        string _user = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
                        string[] _to = { "hf3", "hf4"}; 
                        string _emailed = MailHelper.gen_email("cargo_updated", true, updguid, _to, true, "Cargo Announced Ready " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),_user);
                        if (_emailed != string.Empty)
                        {
                            //mail NOT sent
                            Response.Write("<div class='innercentererror'>" + _emailed + "</div>");
                        }
                        else
                        { 
                            //email sent

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }

        //OLD batch update how to get this to work in case we need to replace sproc?
        //ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
        //SqlConnection _cn = new SqlConnection(_cs.ConnectionString);
        //SqlDataAdapter _sd = new SqlDataAdapter();
        //SqlCommand _cmd = new SqlCommand();
        //
        //_cmd.CommandText = "UPDATE OrderTable SET " +
        //                   "CargoReady = @cargoready, " +
        //                   "EstPallets = @estpallets, " +
        //                   "EstWeight = @estweight, " +
        //                   "EstVolume = @estvolume " + 
        //                   "WHERE OrderID = @orderid;";
        //
        //_cmd.Parameters.Add(new SqlParameter("@orderid", SqlDbType.Int)).SourceColumn = "OrderID";
        //_cmd.Parameters.Add(new SqlParameter("@cargoready", SqlDbType.SmallDateTime)).SourceColumn = "CargoReady";
        //_cmd.Parameters.Add(new SqlParameter("@estpallets", SqlDbType.Int)).SourceColumn = "EstPallets";
        //_cmd.Parameters.Add(new SqlParameter("@estweight", SqlDbType.Int)).SourceColumn = "EstWeight";
        //_cmd.Parameters.Add(new SqlParameter("@estvolume", SqlDbType.Int)).SourceColumn = "EstVolume";
        //
        //_sd.UpdateCommand = _cmd;
        //_sd.UpdateBatchSize = 0; //use maximumm allowed records per batch on server
        //_sd.Update(_dt); 
    }
    #endregion

    #region functions
    /// <summary>
    /// check text box for input and build simple filter string 
    /// </summary>
    protected string get_filter()
    {
        string _filter = ""; // "(OrderNumber==-1)"; //safe default will return no records";

        if (this.dxcbofields.Text.ToString() != string.Empty && this.txtQuickSearch.Text.ToString() != string.Empty)
        {
            string _fieldname = this.dxcbofields.SelectedItem.GetValue("fieldname").ToString();
            string _fieldtype = this.dxcbofields.SelectedItem.GetValue("columntype").ToString(); 
            string _txtsearch = this.txtQuickSearch.Text.ToLower();
            string _lquoted = "";
            string _rquoted = "";
            string _criteria = "==";

            if (_fieldtype.ToLower() == "string")
            {
                _fieldname += ".ToString().ToLower()";
                _lquoted = "\"";
                _rquoted = "\"";

                //02/08/2011 allow partial search on all text fields
                //if (_fieldname.ToLower() == "mintitle" && (Page.Session["user"] != null)) 
                if (Page.Session["user"] != null)
                {
                    _criteria = ".Contains("; //".Contains("
                    _rquoted = "\")"; //"\")";
                }
            }

            //make sure you use escape character for quoting string literals or you will get errors back from dynamic.cs
            //_filter = string.Format("({0}{1}{2}{3}{4})", _fieldname , _criteria  ,_lquoted,_txtsearch, _rquoted );
            string _formstr = _criteria == "==" ? "({0}{1}{2}{3}{2})" : "({0}{1}{2}{3}{2}))";
            _filter = string.Format(_formstr, _fieldname, _criteria, _lquoted, _txtsearch, _rquoted);

        }

        return _filter;
    
    }

    /// <summary>
    /// return name for basic search based on field name, value, job type and contact
    /// </summary>
    /// <returns></returns>
    protected string get_name()
    {
        string _name = "";

        if (this.dxcbofields.Value != null)
        {
            _name += this.dxcbofields.Text.ToString();
        }

        if (this.txtQuickSearch.Value != null)
        {
            _name += " " + this.txtQuickSearch.Text.ToString();  
        }
        //28/08/2010 users can filter by individual contacts within the company
        if (this.cboName.Value != null)
        {
            _name += " user " + this.cboName.Text.ToString(); 
        }

        //15/09/2010 include closed jobs y/n when combo = Active jobs (jobclosed = 1) or combo = closed jobs (jobclosed = 0)
        //when combo = All jobs we can ingore this filter
        if (this.dxcboclosedyn.Value != null)
        {
            _name += " " + this.dxcboclosedyn.Text.ToString().Replace("Search",""); 
        }

        return _name;
    }

    /// <summary>
    /// returns user preferred view as string e.g. "CompanyId", "ContactId", "OfficeId"
    /// </summary>
    /// <param name="defaultid"></param>
    /// <returns></returns>
    protected Parameter return_default_view(Int32 _viewid)
    {
        Int32 _defaultid = 0;
        Parameter _view = new Parameter();

        if (_viewid == -1)
        {
            _defaultid = (Int32)((UserClass)Page.Session["user"]).DefaultView;
        }
        else
        {
            _defaultid = _viewid;
        }

        switch (_defaultid)
        {
            case 0:
                {
                    _view.Name = "CompanyID";
                    _view.DefaultValue = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                    break;
                }
            case 1:
                {
                    _view.Name = "ContactID";
                    _view.DefaultValue = ((UserClass)Page.Session["user"]).UserId.ToString();
                    break;
                }
            case 2:
                {
                    _view.Name = "OfficeID";
                    _view.DefaultValue = ((UserClass)Page.Session["user"]).OfficeId.ToString();
                    break;
                }
            case 3:
                {
                    _view.Name = "OrderControllerID"; //"EmployeeID";
                    _view.DefaultValue = ((UserClass)Page.Session["user"]).UserId.ToString();
                    break;
                }
            case 4:
                {
                    _view.Name = "PrinterID"; //"EmployeeID";
                    _view.DefaultValue = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                    break;
                }
            default:
                {
                    _view = null;
                    break;
                }
        }

        return _view;
    }

    //returns the IP address of the user requesting a page
    protected string userRequestingIP()
    {
        string strIpAddress;

        strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (strIpAddress == null)

            strIpAddress = Request.ServerVariables["REMOTE_ADDR"];


        return strIpAddress;
    }
    #endregion

    #region dll binding
    /// <summary>
    /// date range selector for grid 
    /// </summary>
    protected void bind_dll_archive()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\ddl_items.xml";

        // pass _qryFilter to have keyword-filter RSS Feed
        // i.e. _qryFilter = XML -> entries with XML will be returned
        DataSet _ds = new DataSet();
        _ds.ReadXml(_path);
        DataView _dv = _ds.Tables[0].DefaultView;
        _dv.RowFilter = "ddls ='trackingdate'";

        this.dxcboRange.DataSource = _dv;
        this.dxcboRange.ValueType = typeof(int);
        this.dxcboRange.TextField = "name";
        this.dxcboRange.ValueField = "value";
        this.dxcboRange.DataBind();

        //default to last 12 months
        this.dxcboRange.SelectedIndex = 0;
    }
    //end bind dll archive

    protected void bind_combos()
    {
        //company if company is not -1 (WWI employee)
        if (Session["user"] != null)
        {
            Int32 _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;
            Int32 _officeid = (Int32)((UserClass)Page.Session["user"]).OfficeId;

            if (_companyid != -1) //bind contacts for this company
            {
                //contact names
                Query _qry = new Query(Tables.ContactTable, "WWIprov").AddWhere("CompanyID", Comparison.Equals, _companyid).ORDER_BY("ContactName", "asc");
                ContactTableCollection _contact = new ContactTableCollection();
                _contact.LoadAndCloseReader(_qry.ExecuteReader());
                DataTable _dt = (DataTable)_contact.ToDataTable();
                this.cboName.DataSource = _dt;
                this.cboName.ValueField = "ContactID";
                this.cboName.TextField = "ContactName";
                this.cboName.DataBind();
            }
            else //bind users by office instead  
            {
                //Query _qry = new Query(Tables.EmployeesTable,"WWIprov").AddWhere("OfficeID", Comparison.Equals, _officeid).ORDER_BY("Name", "asc");
                //EmployeesTableCollection _employee = new EmployeesTableCollection();
                //_employee.LoadAndCloseReader(_qry.ExecuteReader());
                //DataTable _dt = (DataTable)_employee.ToDataTable();
                //this.cboName.DataSource = _dt;
                //this.cboName.ValueField = "EmployeeID";
                //this.cboName.TextField = "Name";
                //this.cboName.DataBind();
            }

            //11/03/2011 append ALL USERS option as 1st item in dropdown
            //Code here to populate DropDownList
            DevExpress.Web.ASPxEditors.ListEditItem _li = new DevExpress.Web.ASPxEditors.ListEditItem("(All users)", "-1");
            this.cboName.Items.Insert(0, _li);
            //default to all users
            this.cboName.SelectedIndex = 0;

            //250112 bind quick filter fields if user is logged in or not
            this.ObjectDataSourceFields.SelectMethod = "FetchByActive";
            this.ObjectDataSourceFields.DataBind();
        }
        else
        {
            //250112 bind quick filter fields if user is logged in or not
            this.ObjectDataSourceFields.SelectMethod = "FetchByActiveAnonymous";
            this.ObjectDataSourceFields.DataBind();
        }
    }
    #endregion

    #region deprecated code
    /// <summary>
    /// check text box for input and build simple filter string 
    /// </summary>
    protected void check_filter150910()
    {

        if (this.txtQuickSearch.Text.ToString() != string.Empty)
        {
            string _txtsearch = this.txtQuickSearch.Text.ToLower();
            string _filter = "";
            Int32 _intsearch = 0;
            //ParameterCollection _pars = new ParameterCollection();

            //[OrderNumber]={0} OR [HouseBLNUmber]='{0}' OR [CustomersRef]='{0}'"
            //make sure you use escape character for quoting string literals or you will get errors back from dynamic.cs
            if (int.TryParse(_txtsearch, out _intsearch) == false)
            {
                //_filter = string.Format("HouseBLNUmber.Contains(\"{0}\")", _txtsearch);
                _filter = string.Format("HouseBLNUmber.ToString().ToLower()==\"{0}\" OR ContainerNumber.ToString().ToLower()==\"{0}\" OR CustomersRef.ToString().ToLower()==\"{0}\" OR ISBN.ToString().ToLower()==\"{0}\"", _txtsearch);
                //_filter = string.Format("HouseBLNUmber==\"{0}\" OR ContainerNumber==\"{0}\"",_txtsearch);
                //_filter = "HouseBLNUmber==\"@0\" OR ContainerNumber==\"@1\"";
                //_pars.Add("p1",DbType.String ,_txtsearch); 
            }
            else
            {
                _filter = string.Format("OrderNumber=={0} OR HouseBLNUmber.ToString().ToLower()==\"{1}\" OR ContainerNumber.ToString().ToLower()==\"{1}\" OR CustomersRef.ToString().ToLower()==\"{1}\" OR ISBN.ToString().ToLower()==\"{1}\"", _intsearch, _txtsearch);
                //_filter = string.Format("OrderNumber=={0} OR HouseBLNUmber==\"{1}\" OR ContainerNumber==\"{1}\"", _intsearch, _txtsearch );
                //_filter = "OrderNumber==@0 OR HouseBLNUmber==\"@1\" OR ContainerNumber==\"@2\"";
                //_pars.Add("p1", DbType.Int32, _txtsearch); 
                //_pars.Add("p2", DbType.String, _txtsearch); 

            }

            Session["savedfilter"] = "Search for reference " + _txtsearch; //do we need to save quick searches? if so set this to "n" instead of null
            Session["filter"] = _filter;
            //Session["params"] = _pars;
        }
    }

    /// <summary>
    /// DEPRECATED databinding using LINQ but this does not run in server mode!
    /// </summary>
    protected void bind_linq_datasource_non_server_mode()
    {
        string _filter = "OrderNumber = -1";
        string _companyid = "";
        
        //_companyid = "-1";
        //if (Page.Session["user"] != null)
        //{
        //    _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
        // 
        //}
        //_filter = " CompanyID = " + _companyid;

        if (Session["filter"] != null)
        {
            string _f = (string)Session["filter"];
            if (_companyid != string.Empty) { _filter += " AND " + _f; } else { _filter = _f; }
        }
               
        
        //dynamic queries using system.Linq.dynamic + Dynamic.cs library
        if (_filter != "")
        {
            //var _query = new linq_classesDataContext().view_orders.Where(_filter, System.StringComparison.InvariantCultureIgnoreCase);
            var _query = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0).Where(_filter); 
            this.gridOrder.DataSource = _query;
        }
        else
        {
            //var _query = new linq_classesDataContext().view_orders;
            var _query = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0);
            this.gridOrder.DataSource = _query;
        }

        this.gridOrder.DataBind();
    }

    /// <summary>
    /// get data table for main grid DEPRECATED using Linq instead
    /// </summary>
    /// <param name="pageIndex">startpage for pagination</param>
    /// <param name="pageSize">number of records to return for pagination</param>
    /// <returns>datatable</returns>
    public DataTable get_datatable()
    {
        //using a collection as it's the only way to directly apply a query string!
        //query to grid
        //
        DataTable _dt = new DataTable();
        
        try
        {
            String _companyid = "";
            
            //check to see if we have a company id
            //user MUST be logged in for advanced searches
            ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["WWI_intra"];
            SqlConnection _cn = new SqlConnection();
            _cn.ConnectionString = _cts.ConnectionString;
            _cn.Open();
 
            //need to switch this off when we are testing!
            String _qry = "";
            ////
            //Session.Remove("user");
            ////
            if (Page.Session["user"] != null)
            {
                _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                _qry = " WHERE [CompanyID] = " + _companyid;
            }

            String _filter = "";
            if (Session["filter"] != null)
            {
                _filter = Session["filter"].ToString();
                if (_companyid != string.Empty) { _qry += " AND " + _filter; } else { _qry = " WHERE " + _filter; }
            }
            //Session["filter"] = "OrderNumber = -1";  //default so we do not load any records in start up THIS MESSES UP EXPORT!
            String _select = "SELECT [OrderNumber],[HouseBLNUmber],[CustomersRef],[DateOrderCreated],[originport],[destport],[VesselName],[ETS],[ETA],[destplace],[originplace],[mintitle],[JobClosed],[ContainerNumber], [CompanyID] FROM [view_order]" + _qry + ";";
            SqlCommand _cmd = new SqlCommand(_select, _cn);
            
            ViewOrderCollection _order = new ViewOrderCollection();
            _order.LoadAndCloseReader(_cmd.ExecuteReader());

            _dt = (DataTable)_order.ToDataTable();
            
            _cmd.Dispose(); 
            _cn.Close();
            _cn.Dispose(); 
            
        }
        catch (Exception ex)
        {
            //Response.Write(_err.Message.ToString());  
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
        return _dt;
    }
    //
    public DataTable get_datatable_subsonic()
    {
        //using a collection as it's the only way to directly apply a query string!
        //query to grid
        //Int32 _page = this.gridOrder.PageIndex;
        //Int32 _pagesize = this.gridOrder.SettingsPager.PageSize;
        //
        String _companyid = "";
        String _filter = "";

        //check to see if we have a company id
        //user MUST be logged in for advanced searches
        Query _qry = new Query(Views.ViewOrder);

        //need to switch this off when we are testing!
        Page.Session.Remove("user");
        if (Page.Session["user"] != null)
        {
            _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
            _qry.WHERE("CompanyID = " + _companyid);
            //_query = "WHERE CompanyId = " + _companyid; 
        }

        if (Session["filter"] != null)
        {
            _filter = Session["filter"].ToString();
            if (_companyid != string.Empty) { _qry.AND(_filter); } else { _qry.WHERE(_filter); }
            //if (_companyid != string.Empty) { _query+= "AND " + _filter; } else {_query = " WHERE " + _filter ; }

        }

        //InlineQuery _qry = new InlineQuery();

        //_qry.PageIndex = _page+1; //gridview page is 0 based, but subsonic is 1 based
        //_qry.PageSize = _pagesize;

        ViewOrderCollection _order = new ViewOrderCollection();
        _order.LoadAndCloseReader(_qry.ExecuteReader());
        DataTable _dt = (DataTable)_order.ToDataTable();

        return _dt;
    }

    public Int32 get_total_orders(Int32 companyId)
    {
        Int32 _count = 0;

        ViewOrderCollection _order = new ViewOrderCollection();
        Where _wh = new Where();
        _wh.ColumnName = "CompanyId";
        _wh.ParameterValue = companyId;
        _wh.DbType = DbType.Int32;
 
        _order.Where(_wh);
        _count = _order.Count;

        return _count;

    }
    #endregion

    #region detail row binding
    /// <summary>
    /// detail grid for delivery info
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailOrder_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "OrderNumber" };
        Int32 _ordernumber = (Int32)_detail.GetMasterRowFieldValues(_keys);

        if (_ordernumber > 0)
        {
            //1st detail grid for order item information
            //String[] _cols = {"TitleID", "OrderNumber", "Title", "Isbn", "PONumber", "Copies", "ItemDesc", "DeliveryNoteID", "StatusDate", "CurrentStatusDate", "SpecialInstructions", "CompanyName", "Address1", "PostCode", "Field1"};
            //
            //ViewOrderDeliveryCollection _deliver = DB.Select(_cols)
            //     .From(Tables.DeliverySubTable)
            //     .LeftOuterJoin(Tables.DeliverySubSubTable).LeftOuterJoin(Tables.ItemTable)
            //     .LeftOuterJoin(Tables.CurrentStatus)
            //     .LeftOuterJoin(Tables.NameAndAddressBook)
            //     .Where("OrderNumber").IsEqualTo(_ordernumber)
            //     .ExecuteAsCollection<ViewOrderDeliveryCollection>();
            //
            //
            //DataTable _dt = (DataTable)_deliver.ToDataTable(); 
            //e.DetailTableView.DataSource = _dt;

            ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
            SqlConnection _cn = new SqlConnection();
            _cn.ConnectionString = _cts.ConnectionString;
            _cn.Open();
            String _select = "SELECT * FROM view_order_delivery where OrderNumber = " + _ordernumber + ";";
            SqlCommand _cmd = new SqlCommand(_select, _cn);

            //180811 datareader much faster than bulding collection
            //ViewOrderDeliveryCollection _deliver = new ViewOrderDeliveryCollection();
            //_deliver.LoadAndCloseReader(_cmd.ExecuteReader());
            //
            IDataReader _rd = _cmd.ExecuteReader();
            DataTable _sb = new DataTable();  //DataTable _sb = (DataTable)_deliver.ToDataTable();
            _sb.Load(_rd);
            _detail.DataSource = _sb;  //_sb.DefaultView;
            _rd.Close();
            _cn.Close();

            ///this does not return the correct number of rows    
            //Query _qry = new Query(Views.ViewOrderDelivery).AddWhere("OrderNumber", Comparison.Equals, _ordernumber);
            //ViewOrderDeliveryCollection _deliver = new ViewOrderDeliveryCollection();
            //_deliver.LoadAndCloseReader(_qry.ExecuteReader());
            ///

            //_detail.DataBind(); //don't call this or details view will not work
         }

    }

    /// <summary>
    /// detail grid for invoice info
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailInvoice_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "OrderNumber" };
        Int32 _ordernumber = (Int32)_detail.GetMasterRowFieldValues(_keys); 
        
        if (_ordernumber > 0)
        {

            ConnectionStringSettings _cts = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
            SqlConnection _cn = new SqlConnection();
            _cn.ConnectionString = _cts.ConnectionString;
            _cn.Open();
            String _select = "SELECT * FROM view_order_invoice where OrderNumber = " + _ordernumber + ";";
            SqlCommand _cmd = new SqlCommand(_select, _cn);

            //180811 datareader much faster than bulding collection
            //ViewOrderInvoiceCollection _invoice = new ViewOrderInvoiceCollection();
            //_invoice.LoadAndCloseReader(_cmd.ExecuteReader());
            //
            IDataReader _rd = _cmd.ExecuteReader();
            DataTable _sb = new DataTable();  //(DataTable)_invoice.ToDataTable();
            _sb.Load(_rd);
            _detail.DataSource = _sb; //_sb.DefaultView;
            _rd.Close();
            _cn.Close();
            //_detail.DataBind(); //don't call this or details view will not work
        }

    }
    #endregion

    #region search history repeater deprecated
    /// <summary>
    /// data repeater responding to linkbutton click event
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptQuery_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            //string _filter = ((LinkButton)e.CommandSource).Text.ToString();
            string _query = ((LinkButton)e.CommandSource).CommandArgument.ToString();
            string _named = ((LinkButton)e.CommandSource).Text.ToString();

            if (!string.IsNullOrEmpty(_query))
            {
                //System.Collections.Hashtable _htfilter = new System.Collections.Hashtable();
                //_htfilter.Add("query", _query);
                //_htfilter.Add("named", _named);
                //_htfilter.Add("state", "0"); //so we don't save again

                //rebuild session and flag hidden field
                //Session["filter"] = _htfilter;
                SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
                _sessionWrapper["query"] = _query;
                _sessionWrapper["name"] = _named;
                
                this.dxhfMethod.Set("mode", "0"); 
                this.gridOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void dxcallbackHistory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //get_update_history("5");
    }

    /// <summary>
    /// format length of query name <= 34
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptQuery_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            LinkButton _lnk = (LinkButton)e.Item.FindControl("lnkquery");
            if (_lnk != null && _lnk.Text.Length > 34)
            {
                _lnk.Text = _lnk.Text.Substring(0, 34).ToString() + " ...";
            }
        }

    }
    #endregion

    #region grid events
    /// <summary>
    /// Fires when javascript function submit_query() is called
    /// e.g. by quick search button or advanced filter
    /// or edit_pallet popup requests a batch update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //from popupcontrol e.g. Ord_Edit_Pallet.aspx      
        if (e.Parameters == "batchupdate")
        {
            get_selected_rows(1);
        }

        //rebind data
        //this.gridOrder.DataSource = get_datatable();
        this.gridOrder.DataBind();
    }
    /// <summary>
    /// rowcreated event can be usde to preformat grid
    /// e.g. change cell colour
    /// or provide additional functionality
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
           if (e.RowType == GridViewRowType.Data)
        {
            ASPxGridView _grid = (ASPxGridView)sender;
            //string _hfKey = "key" + e.KeyValue.ToString(); //this seems to be problematic (string too big for key??)
            string _hfKey = "key" + e.GetValue("OrderID").ToString();

            //find order id
            //string _test = e.GetValue("OrderID").ToString();

            //find template controls
            GridViewDataColumn _col1 = (GridViewDataColumn)_grid.Columns["CargoReady"];
            DevExpress.Web.ASPxEditors.ASPxDateEdit _dte = (DevExpress.Web.ASPxEditors.ASPxDateEdit)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col1, "dxdtcargoready");

            GridViewDataColumn _col2 = (GridViewDataColumn)_grid.Columns["EstPallets"];
            DevExpress.Web.ASPxEditors.ASPxTextBox _tx1 = (DevExpress.Web.ASPxEditors.ASPxTextBox)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col2, "dxtxestpallets");

            GridViewDataColumn _col3 = (GridViewDataColumn)_grid.Columns["EstWeight"];
            DevExpress.Web.ASPxEditors.ASPxTextBox _tx2 = (DevExpress.Web.ASPxEditors.ASPxTextBox)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col3, "dxtxestweight");

            GridViewDataColumn _col4 = (GridViewDataColumn)_grid.Columns["EstVolume"];
            DevExpress.Web.ASPxEditors.ASPxTextBox _tx3 = (DevExpress.Web.ASPxEditors.ASPxTextBox)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col4, "dxtxestvolume");

            //create client side events
            //and pass data from hidden field
            if (_dte != null && _tx1 != null && _tx1 != null && _tx3 != null)
            {
                //_dte.ClientSideEvents.ValueChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + ",dtcargo" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx1.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx2.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx3.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";

                //use s.gettext() for date box or we will be getting the date back in very long format!
                //_dte.ClientSideEvents.ValueChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + "," + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx1.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + "," + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx2.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + "," + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                //_tx3.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.KeyValue.ToString() + "," + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";

                _dte.ClientSideEvents.ValueChanged = "function(s,e){ProcessValueChanged(" + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                _tx1.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                _tx2.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";
                _tx3.ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + e.GetValue("OrderID") + ",dtcargo" + e.KeyValue.ToString() + ".GetText()" + ",txpallets" + e.KeyValue.ToString() + ".GetValue()" + ",txweight" + e.KeyValue.ToString() + ".GetValue()" + ",txvolume" + e.KeyValue.ToString() + ".GetValue()" + ");}";

                if (this.dxhfeditor.Contains(_hfKey))
                {
                    //element 0 is used to store OrderID
                    string[] _pars = Convert.ToString(this.dxhfeditor[_hfKey]).Split(';');
                    _dte.Value = _pars[0];
                    _tx1.Text = _pars[1];
                    _tx2.Text = _pars[2];
                    _tx3.Text = _pars[3];
                    //e.Row.Cells[2].Style.Add("color", "Red");
                }

            }
        }
    }
    /// <summary>
    /// init editors - we need to generate client instance namaes on the fly so we can refer back to thenm
    /// using javascript
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxdtcargoready_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer _c = ((DevExpress.Web.ASPxEditors.ASPxDateEdit)sender).NamingContainer
           as GridViewDataItemTemplateContainer;
        ((DevExpress.Web.ASPxEditors.ASPxDateEdit)sender).ClientInstanceName = "dtcargo" + _c.KeyValue.ToString();
    }

    protected void dxtxestpallets_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer _c = ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).NamingContainer
           as GridViewDataItemTemplateContainer;
        ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).ClientInstanceName = "txpallets" + _c.KeyValue.ToString();
    }

    protected void dxtxestweight_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer _c = ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).NamingContainer
           as GridViewDataItemTemplateContainer;
        ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).ClientInstanceName = "txweight" + _c.KeyValue.ToString();
    }

    protected void dxtxestvolume_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer _c = ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).NamingContainer
           as GridViewDataItemTemplateContainer;
        ((DevExpress.Web.ASPxEditors.ASPxTextBox)sender).ClientInstanceName = "txvolume" + _c.KeyValue.ToString();
    }

    /// <summary>
    /// hightlight where values have been changed?
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName != "CargoReady" && e.DataColumn.FieldName != "EstPallets" && e.DataColumn.FieldName != "EstWeight" && e.DataColumn.FieldName != "EstVolume") return;

        string _hfKey = "key" + e.GetValue("OrderID").ToString();

        if (this.dxhfeditor.Contains(_hfKey))
            e.Cell.BackColor = System.Drawing.Color.Green;

    }
    #endregion

}

