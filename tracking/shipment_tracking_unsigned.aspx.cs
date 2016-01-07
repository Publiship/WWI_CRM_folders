using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using ParameterPasser;
using SubSonic;

public partial class shipment_tracking_unsigned : System.Web.UI.Page
{
    

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //for encrypting url on joomla site
            //string _test = wwi_security.EncryptString("publiship_advance/advance_request", "publiship"); 
 
            if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
            {
                //date range filter
                bind_dll_archive();
            }
                      
            
            //additional filtering on user/company? custom reports?
            bind_combos();
            this.dxcbocompany.Enabled = false; //not available unless logged in
            //grid set up depends on user preferences
            //disable cookies
            //sort columns based on user prefs
            //hide columms based on user prefs
            //get user info

            //041112 do not need to be logged in
            //string _uid = "000_000";

            if (Page.Session["user"] != null)
            {
                
            }
                       
            //07/03/2011 history summary disabled, they can always follow link if they need to see it
            //MUST rebind repeater here or itemcommand will not function as expected
            //get_search_history("5"); 
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
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());  
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if(!Page.IsPostBack)
        {
            //reset_hidden();
        }

        if (Page.Session["user"] != null)
        {
            //Response.Redirect("~/Ord_View_Tracking.aspx");
        }
        else
        {
            set_visible_data("0"); //hide cols
            this.gridOrder.SettingsCookies.Enabled = false;
            this.gridOrder.SettingsCustomizationWindow.Enabled = false;
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
        string _countries = "";
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
        else if (_mode == "4")
        {
            _query = "OrderNumber > -1";
            this.dxhfMethod.Set("mode", 0); 
        }
        //company id: always add as a search param if user is logged in
        //if (Page.Session["user"] != null)
        //company id: always add as a search param if user is logged in UNLESS _mode = -1 which we can use to bypass params
        if (Page.Session["user"] != null)
        {
            bool _usedefaultid = false;

            if (_mode != "-1" && _mode != "0")
            {
                _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;

                //if intrernal user check for a selected company
                //if external user but they have access to company drop dowm check for selected company and if restricted to particular countries for selected companyID
                //else return default
                if (_companyid == -1)
                {
                    if (!string.IsNullOrEmpty((string)this.dxcbocompany.Value)) { _params.Add("CompanyID", this.dxcbocompany.Value.ToString()); }
                    _name += this.dxcbocompany.Text.ToString() != "" ? ", company name equals " + this.dxcbocompany.Text.ToString() : ""; 
                }
                else if (_companyid > 0 && this.dxcbocompany.Enabled)
                {
                    string _selectedcompany = string.IsNullOrEmpty((string)this.dxcbocompany.Value) ? _companyid.ToString() : this.dxcbocompany.Value.ToString();
                    if (!string.IsNullOrEmpty(_selectedcompany))
                    {
                        _params.Add("CompanyID", this.dxcbocompany.Value.ToString()); //select for this company of list
                        _params.Add("ConsigneeID", _companyid.ToString()); //select for this user's company

                        //check for additional country restrictions e.g. EDC can see usborne shipments but ONLY to the US
                        IList<string> _args = wwi_func.array_from_xml("xml\\company_iso.xml", "companylist/company[id='" + _companyid + "']/visibleitems/item[itemid='" + this.dxcbocompany.Value.ToString() + "']/destinationid");
                        if (_args.Count > 0)
                        {
                            //don't use sql IN(n) as linq won't parse the statement
                            _countries = "CountryID==" + string.Join(" OR CountryID==", _args.Select(i => i.ToString()).ToArray());
                        }
                    }
                    else
                    {
                        _usedefaultid = true;
                    }
                }
                else
                {
                    _usedefaultid = true;
                }

            }
            else
            {
                _usedefaultid = true;
            }

            if (_usedefaultid)
            {
                Parameter _p = return_default_view(-1);
                if (_p != null) { _params.Add(_p); }
            }
        }

        //check for additional parameters if query is not from history (mode=0)
        //but you can only apply these filters when a) user is logged in or b) a query parameter has been entered
        //otherwise you could create a default query of e.g. (Jobclosed==false) which would return ALL closed jobs in database
        //if ((_mode != "0") && (_query !="" | _companyid !=-1))
        //
        if ((_mode != "0" & _mode != "1") && (_query != "" || _params.Count > 0))
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
                    _name += ", user name equals " + this.cboName.Text.ToString(); 
                }

            }

            //15/09/2010 include closed jobs y/n when combo = Active jobs (jobclosed = 1) or combo = closed jobs (jobclosed = 0)
            //when combo = All jobs we can ignore this filter
            if (this.dxcboclosedyn.Value != null)
            {
                _params.Add("JobClosed", Convert.ToBoolean(this.dxcboclosedyn.Value).ToString());
                _name += ", job status equals " + this.dxcboclosedyn.Text.ToString().Replace("Search", "");  

            }

        }

        //now rebuild query with additional parameters
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

        //add country restrictions
        if (_countries != "") { _query += " AND (" + _countries + ")"; }
        //finally if there's no query string check filter expression and force search, otherwise the default search is to display no records. this is only a problem for publiship 
        //users as everyone else will at least have the company filter
        if (_query == "") {
            if (this.gridOrder.FilterExpression.ToString() != "") {
                _query = "OrderNumber > -1";
            }  
        } 
        //19/04/2013 re-designed query means we can use subdeliveryid as key NO IT DOESN'T back to using OrderIx
        //dynamic queries using system.Linq.dynamic + Dynamic.cs library
        //20/10/2010 we have build a unqiue index (OrderIx) from OrderId, TitleId, ContainerSubId as usual primary keys are not going to 
        //be unique in the view. aspxgrid only works properly when it has a unique key 
        e.KeyExpression = "OrderIx"; //"OrderID"; //a key expression is required 
        
        if (!string.IsNullOrEmpty(_query))
        {
            //get start date from dll value
            //e.g if search is for last 12 months dllvalue 1 start date = current date - (1 * 12) months
            //if search is for 2-3 years dllvalue 3 start date = current date - (3 * 12) months
            int _dllvalue = this.dxcboMonths.Value != null ? wwi_func.vint(this.dxcboMonths.Value.ToString()) : 1;
            //number of months to include after start date
            int _months = 12;
            //multiply base * 12 to get start date
            int _lowest = 0 - (_dllvalue * 12);
            DateTime _minDate = DateTime.Now.AddMonths(_lowest);
            //get from date range startdate + N months
            //290413 using new usedefined inline table function so we can parametise with month range
            //in the anon view default to last 12 months
            //var _nquery = new linq_classesDataContext().view_order_2s.Where(_query); //c => c.CompanyID == 7
            var _nquery = new linq.linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months).Where(_query); //c => c.CompanyID == 7
            e.QueryableSource = _nquery;
            //Int32 _count = _nquery.Count();

            if (!String.IsNullOrEmpty(_name))
            {
                append_to_query_log(_query, _name);
            }
            this.gridOrder.SettingsText.Title = "Search results: " + _name;
        }
        else //default to display nothing in grid 
        {
            //var _nquery = new linq_classesDataContext().view_order_2s.Where(c => c.SubDeliveryID == -1);
            var _nquery = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0).Where(c => c.OrderNumber == -1); //c => c.CompanyID == 7
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }


    }
    
    #region grid events
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
        //        
        //        //this.btnExpandAll.Text = "Hide Detail";
        //        //this.gridOrder.DetailRows.ExpandAllRows();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //Response.Write(ex.Message.ToString());  
        //    this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        //}
    }

    /// <summary>
    /// Fires when javascript function submit_query() is called
    /// e.g. by quick search button or advanced filter
    /// or edit_pallet popup requests a batch update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //batch updating disabled on this form
        //
        //from popupcontrol e.g. Ord_Edit_Pallet.aspx      
        //if (e.Parameters == "batchupdate")
        //{
        //    get_selected_rows(1);
        //}

        //rebind data
        //this.gridOrder.DataSource = get_datatable();
        this.gridOrder.DataBind();
    }

    /// <summary>
    /// populate unbound fields e.g. est. time to warehouse (ETW) = ETA + 7 days
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
        if (e.Column.FieldName == "ETW")
        {
            if (e.GetListSourceFieldValue("ETA") != null)
            {
                string _eta = Convert.ToDateTime(e.GetListSourceFieldValue("ETA")).ToString("yyyy/MM/dd");
                e.Value = Convert.ToDateTime(_eta).AddDays(7);
            }
        }

    }

    /// <summary>
    /// hide link to documents if no document folder linked to order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOrder_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        int _docs = e.GetValue("document_folder") != null ? (int)e.GetValue("document_folder") : 0;
        DevExpress.Web.ASPxEditors.ASPxButton _obj = this.gridOrder.FindRowCellTemplateControl(e.VisibleIndex, null, "dxbtnDocs") as DevExpress.Web.ASPxEditors.ASPxButton;

        if (_obj != null)
        {
            //set clientside functionality here as we need to pass document folder id to javascript function
            _obj.Visible = _docs > 0 ? true : false;
            _obj.ClientSideEvents.Click = string.Format("function(s, e) {{ onViewDocuments('{0}'); }}", _docs.ToString());
        }
    }
    //end row created
    protected void gridOrder_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();

            string _orderno = this.gridOrder.GetRowValues(_idx, "OrderNumber").ToString();
            //100112 check to see if this order is attached to a document folder other than it's own ordernumber
            string _docfolder = this.gridOrder.GetRowValues(_idx, "document_folder").ToString();
            //160112 pass house BL as we can use this to ientify which orders are grouped together 
            string _housebl = this.gridOrder.GetRowValues(_idx, "HouseBLNUmber").ToString();

            if (!string.IsNullOrEmpty(_orderno))
            {
                switch (_command)
                {
                    case "link_filemanager": //drop record into pricer
                        {
                            //internal use only!
                            Session["orderlist"] = null;
                            string _redirect = "Ord_Edit_Files_1.aspx?" + "pod=" + wwi_security.EncryptString(_orderno, "publiship") + "&dfd=" + wwi_security.EncryptString(_docfolder, "publiship") + "&hbl=" + wwi_security.EncryptString(_housebl, "publiship");

                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false);
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }//end switch

            }//end if
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //end row command
#endregion
    
    #region cargo updating not available on this page
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
               update_cargo(_selected, _orderids);
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
    protected void update_cargo(Int32 selected, List<Object> orderids)
    {
        try
        {
            String _updcargo = (String)Session["updcargo"];
            if(!String.IsNullOrEmpty(_updcargo))
            {
                System.Text.StringBuilder _sb = new System.Text.StringBuilder();
                _sb.Append("<root>");

                for (int _rowidx = 0; _rowidx <= orderids.Count - 1; _rowidx++)
                {
                    string _item = orderids[_rowidx].ToString();
                    _sb.Append(String.Format(_updcargo, _item));
                }
                _sb.Append("</root>");

                ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                SqlConnection _cn = new SqlConnection(_cs.ConnectionString);
                SqlCommand _cmd = new SqlCommand("sp_update_order_cargo", _cn);
                SqlParameter _rtv = new SqlParameter("@returnvalue", SqlDbType.Int);

                _rtv.Direction = ParameterDirection.ReturnValue;
                _rtv.DbType = DbType.Int32;

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@XMLCargo", _sb.ToString());
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
                        //Response.Write("Update failed, please refer to Publiship IT support");
                        this.lblmsgboxdiv.Text += "<div class='alert'>Update failed: please refer to Publiship IT support</div>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";

        }

        //Session["updcargo"] = null;
        Session.Remove("updcargo");
        
        //OLD batch update how to get this to work?
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
                //030914 Paul Edwards this line suddenly causes an error when the filter is parsed in dynamic.cs. It used to work!
                //_fieldname += ".ToString().ToLower()";
                _lquoted = "\"";
                _rquoted = "\""; 
                
                //02/08/2011 allow partial search on all text fields
                //if (_fieldname.ToLower() == "mintitle" && (Page.Session["user"] != null)) 
                if (Page.Session["user"] != null) 
                {
                    _criteria = ".StartsWith("; //".Contains("
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
    //end get filter

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
    
        return _name;
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


    /// <summary>
    /// returns user preferred view as string e.g. "CompanyId", "ContactId", "OfficeId"
    /// </summary>
    /// <param name="defaultid"></param>
    /// <returns></returns>
    protected Parameter return_default_view(Int32 _viewid)
    {
        int _defaultid = 0;
        string _value = null;
        string _name = null;
        Parameter _view = new Parameter();
        UserClass _user = ((UserClass)Page.Session["user"]);

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
                    if (_user.CompanyId > 0)
                    {
                        _value = _user.CompanyId.ToString();
                        _name = "CompanyID";
                    }
                    //_view.Name = "CompanyID";
                    //_view.DefaultValue = _value;
                    break;
                }
            case 1:
                {
                    if (_user.UserId > 0)
                    {
                        _value = _user.UserId.ToString();
                        _name = "ContactID";
                    }
                    //_view.Name  = "ContactID";
                    //_view.DefaultValue = ((UserClass)Page.Session["user"]).UserId.ToString();
                    break;
                }
            case 2:
                {
                    if (_user.OfficeId > 0)
                    {
                        _value = _user.OfficeId.ToString();
                        _name = "OfficeID";
                    }
                    //_view.Name = "OfficeID";
                    //_view.DefaultValue = ((UserClass)Page.Session["user"]).OfficeId.ToString();
                    break;
                }
            case 3:
                {
                    if (_user.UserId > 0)
                    {
                        _value = _user.UserId.ToString();
                        _name = "OrderControllerID";
                    }
                    //_view.Name = "OrderControllerID"; //"EmployeeID";
                    //_view.DefaultValue = ((UserClass)Page.Session["user"]).UserId.ToString();
                    break;
                }
            case 4:
                {
                    if (_user.CompanyId > 0)
                    {
                        _value = _user.CompanyId.ToString();
                        _name = "PrinterID";
                    }
                    //_view.Name = "PrinterID"; //"EmployeeID";
                    //_view.DefaultValue = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                    break;
                }
            case 5:
                {
                    if (_user.CompanyId > 0)
                    {
                        _value = _user.CompanyId.ToString();
                        _name = "AgentAtDestinationID";
                    }
                    //_view.Name = "AgentAtDestinationID";
                    //_view.DefaultValue = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                    break;
                }
            default:
                {
                    _view = null;
                    break;
                }
        }

        if (_name == null || _value == null)
        {
            _view = null;
        }
        else
        {
            _view.Name = _name; _view.DefaultValue = _value;
        }

        return _view;
    }

    #endregion
    
    #region subs
    //saves query, IP address, etc to database
    protected void append_to_query_log(string _query, string _name)
    {
        try
        {
            if (!string.IsNullOrEmpty(_query))
            {

                DbQueryLog _newlog = new DbQueryLog();

                _newlog.QryText = _query;
                _newlog.QryDesc = _name.Replace("___report", ""); ;
                _newlog.LogIp = userRequestingIP();
                _newlog.LogQryDate = DateTime.Now;
                _newlog.ByContactID = 0;
                _newlog.ByEmployeeID = 0;
                _newlog.QrySource = _name.Contains("___report") ? "OTR" : "OTV"; ///order tracking REPORT (goes to combobox) or order tracking view

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

    protected void reset_hidden()
    {
        this.dxhfMethod.Set("mode", -1);
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
    //end remove grid grouping

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
    #endregion

    #region dll binding
    /// <summary>
    /// date range restriction
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

        this.dxcboMonths.DataSource = _dv;
        this.dxcboMonths.ValueType = typeof(int);
        this.dxcboMonths.TextField = "name";
        this.dxcboMonths.ValueField = "value";
        this.dxcboMonths.DataBind();

        this.dxcboMonths.SelectedIndex = 0;
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

                //180811 datareader much faster than loading to datatabble
                //ContactTableCollection _contact = new ContactTableCollection();
                //_contact.LoadAndCloseReader(_qry.ExecuteReader());
                //DataTable _dt = (DataTable)_contact.ToDataTable();
                IDataReader _rd = _qry.ExecuteReader();

                //this.cboName.DataSource = _dt;
                this.cboName.DataSource = _rd;
                this.cboName.ValueField = "ContactID";
                this.cboName.TextField = "ContactName";
                this.cboName.DataBind();
            }
            else //bind users by office instead  
            {
                //Query _qry = new Query(Tables.EmployeesTable, "WWIprov").AddWhere("OfficeID", Comparison.Equals, _officeid).ORDER_BY("Name", "asc");
                //EmployeesTableCollection _employee = new EmployeesTableCollection();
                //_employee.LoadAndCloseReader(_qry.ExecuteReader());
                //DataTable _dt = (DataTable)_employee.ToDataTable();
                //this.cboName.DataSource = _dt;
                //this.cboName.ValueField = "EmployeeID";
                //this.cboName.TextField = "Name";
                //this.cboName.DataBind();
            }

            //does user have any custom reports?
            //Int32 _userid = (Int32)((UserClass)Page.Session["user"]).UserId;
            //Query _qr = new Query(Tables.DbQueryLog, "WWIprov").AddWhere("by_contactID", Comparison.Equals, _userid).AddWhere("qry_source", Comparison.Equals, "OTR").ORDER_BY("qry_id", "desc");
            //DbQueryLogCollection _reports = new DbQueryLogCollection();
            //_reports.LoadAndCloseReader(_qr.ExecuteReader());
            //DataTable _dtb = (DataTable)_reports.ToDataTable();
            //this.cboName.DataSource = _dtb;
            //this.cboName.ValueField = "qry_text";
            //this.cboName.TextField = "qry_desc";
            //this.cboName.DataBind();


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

        //291111 set a default option for the exporter
        //don't need code just set Selected to true for the item you want
    }
    #endregion

    #region company filters
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// company name is only available to publiship users
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;
        
        if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        {
            Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
            if (_companyid == -1 || _companyid == 1865) 
            {
                string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : ""; 

                //use datareaders - much faster than loading into collections
                string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };

                //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
                SubSonic.SqlQuery _query = new SubSonic.SqlQuery();

                if (_companyid == -1)
                {
                    _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
                }
                else
                {
                    //check in xml file company_filter for to see if this user can select any companies (use "companylist/company[@id=''" if you want to search on attributes)
                    IList<string> _args = wwi_func.array_from_xml("xml\\company_iso.xml", "companylist/company[id='" + _companyid + "']/visibleitems/item/itemname");
                    //string[] _args = { "Education Development Corporation", "Usborne Publishing Limited", "Kane Miller Book Publishers" };
                    
                    _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").In(_args);
                }
                    
                IDataReader _rd = _query.ExecuteReader();
                _combo.DataSource = _rd;
                _combo.ValueField = "CompanyID";
                _combo.TextField = "CompanyName";
                _combo.DataBind();
            }
        }
    }

    
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        if (Page.Session["user"] != null) //if publiship user allow filter to be used otherwise filter null
        {
            Int32 _companyid = wwi_func.vint(((UserClass)Page.Session["user"]).CompanyId.ToString());
            if (_companyid == -1 || _companyid == 1865)
            {
                _id = wwi_func.vint(e.Value.ToString());

                //use datareaders - much faster than loading into collections
                string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };

                //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
                SubSonic.SqlQuery _query = new SubSonic.SqlQuery();

                if (_companyid == -1)
                {
                    _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
                }
                else
                {
                    //Int32[] _args = { 1865, 1167, 22848 };
                    //System.Collections.ArrayList
                    IList<string> _args = wwi_func.array_from_xml("xml\\company_iso.xml", "companylist/company[id='" + _companyid + "']/visibleitems/item/itemid");
                    _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").In(_args).OrderAsc(new string[] { "CompanyName" });
                }

                IDataReader _rd = _query.ExecuteReader();
                _combo.DataSource = _rd;
                _combo.ValueField = "CompanyID";
                _combo.TextField = "CompanyName";
                _combo.DataBind();
            }
        }
    }
    //end incremental filtering of company name
    //check to see if this company can see other company records
    protected Boolean has_company_filters(Int32 companyId)
    {
        Boolean _result = false;
        int _nodes = wwi_func.count_elements_from_xml("xml\\company_iso.xml", "companylist/company[id='" + companyId + "']");
        if (_nodes > 0)
        {
            _result = true;
        }
        return _result;
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
            //290413 using new usedefined inline table function so we can parametise with month range
            //for anon view default to last 12 months only
            //var _query = new linq_classesDataContext().view_order_2s.Where(_filter, System.StringComparison.InvariantCultureIgnoreCase);
            var _query = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0).Where(_filter, System.StringComparison.InvariantCultureIgnoreCase); //c => c.CompanyID == 7
            this.gridOrder.DataSource = _query;
        }
        else
        {
            // var _query = new linq_classesDataContext().view_order_2s;
            var _query = new linq.linq_view_orders_udfDataContext().view_orders_by_age(DateTime.Now, 0).Where(c => c.OrderNumber == -1); //c => c.CompanyID == 7
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
        catch (Exception _err)
        {
            Response.Write(_err.Message.ToString());  
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

    #region detail binding
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

    #region form events (ddl, buttons, etc)
    /// <summary>
    /// month filter for shipment tracking
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcboMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridOrder.DataBind();
    }

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


    /// <summary>
    /// rebind grid so filter will be recreated including user id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clear session if there is one
        //Session["filter"] = null;
        this.dxhfMethod.Set("mode", 3); //set to my report
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

    /// <summary>
    /// open search history form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkHistory_Click(object sender, EventArgs e)
    {
        //set mode to 0
        Response.Redirect("~/Ord_View_Tracking_Audit.aspx");
    }
    protected void dxcboclosedyn_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.gridOrder.DataBind();
    }

    
    #endregion

    #region search history deprecated code

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
            //Response.Write(ex.Message.ToString());
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }



    protected void dxcallbackHistory_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //get_search_history("5");
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
                _lnk.Text = _lnk.Text.Substring(0, 34).ToString() + " ..." ;
            }
        }

    }
    #endregion
        
    #region column fixing
    /// <summary>
    /// deprecated
    /// show or hide columns
    /// using width. don't use visible property as visible = false will set visibleindex to -1 and change the column position unpredictably
    /// also causes problems with field chooser and grid cookies 
    /// </summary>
    /// <param name="setvisible"></param>
    protected void set_visible_data(bool setvisible)
    {
        try
        {
            //disable cookies
            this.gridOrder.SettingsCookies.Enabled = false;

            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ord_view_tracking_hide.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "cid ='*'"; //no user id so filter for all users

            //for (int ix = 0; ix < _ds.Tables[0].Rows.Count; ix++)
            for (int ix = 0; ix < _dv.Count; ix++)
            {
                string _fieldname = _dv.Table.Rows[ix][0].ToString();  //_ds.Tables[0].Rows[ix][0].ToString();
                int _width = setvisible == true ? (int)_dv.Table.Rows[ix][1] : 0;

                //show/hide columm don't use visibleindex
                this.gridOrder.Columns[_fieldname].Width = _width;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //this version HIDES the columns by user id
    protected bool set_visible_data(string cid)
    {
        bool _fixedgrid = false;

        try
        {
            //string _path = AppDomain.CurrentDomain.BaseDirectory;
            //_path += "xml\\ord_view_tracking_hide.xml";
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\" + cid + "_ord_view_tracking_cols.xml";

            if (File.Exists(_path))
            {
                //disable cookies
                //do this before fixing grid or it doesn't work properly in internet explorer (a law to itself as usual)
                this.gridOrder.SettingsCookies.Enabled = false;

                // pass _qryFilter to have keyword-filter RSS Feed
                // i.e. _qryFilter = XML -> entries with XML will be returned
                DataSet _ds = new DataSet();
                _ds.ReadXml(_path);
                DataView _dv = _ds.Tables[0].DefaultView;

                //_dv.RowFilter = "cid ='" + cid + "'";

                if (_dv.Count > 0)
                {
                    //for (int ix = 0; ix < _ds.Tables[0].Rows.Count; ix++)
                    for (int ix = 0; ix < _dv.Count; ix++)
                    {
                        //column name
                        string _fieldname = _dv[ix][0].ToString();  //_ds.Tables[0].Rows[ix][0].ToString();
                        //visibleindex
                        int _visibleindex = wwi_func.vint(_dv[ix][1].ToString());
                        //width
                        int _width = wwi_func.vint(_dv.Table.Rows[ix][2].ToString().Replace("px", ""));
                        //visible
                        bool _visible = Convert.ToBoolean(_dv[ix][3].ToString()); // == "True"? true: false; //_ds.Tables[0].Rows[ix][3].ToString();

                        //show/hide columm don't set visibleindex unless > -1 as it messes the grid up
                        if (this.gridOrder.Columns[_fieldname] != null)
                        {
                            if (_visibleindex > -1)
                            {
                                this.gridOrder.Columns[_fieldname].VisibleIndex = _visibleindex;
                            }

                            this.gridOrder.Columns[_fieldname].Width = _width;
                            this.gridOrder.Columns[_fieldname].Visible = _visible; //= false will drop column into field choose
                        }
                    }

                    _fixedgrid = true;
                }
            }
            if (!_fixedgrid) { this.gridOrder.SettingsCookies.Enabled = true; }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }

        return _fixedgrid;
    }
    //end set visible data
    /// <summary>
    /// create a JSON string storing column info then save to resource
    /// using user id and companyid as resource key name
    /// </summary>
    protected void set_fixed_cols_resx(string resxkey)
    {
        //grid set up depends on user preferences stored in an encrypted cookie
        //not a .resx file as they can't be edited on the fly
        //
        //sort columns based on user prefs
        //hide colummd based on user prefs

        //create array for prefs
        uTrackingPrefs[] _prefs = new uTrackingPrefs[this.gridOrder.Columns.Count];

        //enum grid columns
        for (int c = 0; c <= this.gridOrder.Columns.Count - 1; c++)
        {
            _prefs[c].colName = this.gridOrder.Columns[c].Name;
            _prefs[c].visible = this.gridOrder.Columns[c].Visible;
            _prefs[c].visibleindex = this.gridOrder.Columns[c].VisibleIndex;
        }

        //serialise using System.Web.Script.Serialization
        JavaScriptSerializer _js = new JavaScriptSerializer();
        string _jsprefs = wwi_security.EncryptString(_js.Serialize(_prefs), "publiship");
        HttpCookie _settings = new HttpCookie("grid_" + resxkey, _jsprefs);
        _settings.Expires = DateTime.Now.AddYears(5);
        Response.Cookies.Add(_settings);

        //HttpCookie _settings = Request.Cookies["grid_" + resxkey];
        //if (_settings != null) 
        //    { 
        //    _settings.Value = _jsprefs; } 
        //else 
        //{
        //    _settings = new HttpCookie("grid_" + resxkey,_jsprefs);          
        //    }

        //write to global resources. if the key allready exists it will simply be updated with the new string
        // Create a file stream to encapsulate items.resources.
        //FileStream fs = new FileStream(Server.MapPath("App_GlobalResources//grids_uid.resx"), FileMode.OpenOrCreate, FileAccess.Write);
        //IResourceWriter _writer = new ResXResourceWriter(fs);
        //_writer.AddResource(resxkey, _jsprefs);
        //_writer.Generate(); 
        //_writer.Close();

    }
    //end set fixed colx resx

    protected void kill_fixed_cols_resx(string resxkey)
    {
        //set the expiration date of the cookie to -1d, which will force the browser to remove it
        if (resxkey != "000_000" & Request.Cookies["grid_" + resxkey] != null)
        {
            HttpCookie myCookie = new HttpCookie("grid_" + resxkey);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }

        //kill for anonymous user
        if (Request.Cookies["grid_000_000"] != null)
        {
            HttpCookie myCookie = new HttpCookie("grid_000_000");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        //write to global resources. if the key allready exists it will simply be updated with the new string
        //FileStream fs = new FileStream(Server.MapPath("App_GlobalResources//grids_uid.resx"), FileMode.OpenOrCreate, FileAccess.Write);
        //IResourceWriter _writer = new ResXResourceWriter(fs);
        //_writer.AddResource(resxkey, "");
        //_writer.Generate(); 
        //_writer.Close(); 
    }

    /// <summary>
    /// get resource storing column info using key name user id _ company id
    /// </summary>
    protected bool get_fixed_cols_resx(string resxkey)
    {
        bool _success = false;
        //use local cookie to store info as it is trying to edit .resx files in code will corrupt the file
        //string _settings = (String)System.Web.HttpContext.GetGlobalResourceObject("grids_uid", resxkey);
        //if user is not logged in chyeck in anonymous
        HttpCookie _settings = Request.Cookies["grid_" + resxkey] != null ? Request.Cookies["grid_" + resxkey] : Request.Cookies["grid_000_000"];

        if (_settings != null)
        {
            //deserialise using System.Web.Script.Serialization
            JavaScriptSerializer _js = new JavaScriptSerializer();
            IList<uTrackingPrefs> _prefs = _js.Deserialize<IList<uTrackingPrefs>>(wwi_security.DecryptString(_settings.Value.ToString(), "publiship"));
            if (_prefs.Count > 0)
            {
                for (int c = 0; c < _prefs.Count; c++)
                {
                    DevExpress.Web.ASPxGridView.GridViewColumn _col = this.gridOrder.Columns[_prefs[c].colName];
                    if (_col != null)
                    {
                        _col.Visible = _prefs[c].visible;
                        _col.VisibleIndex = _prefs[c].visibleindex;
                    }
                }
            }
            _success = true;
        }

        return _success;
    }
    //end get fixed colx resx
#endregion

    #region menu post backs replacing old buttons
    protected void dxmnuGrid2_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        switch (e.Item.Name.ToString())
        {
            case "mnuExport": //replaces export button & combo
                {
                    export_grid_data();
                    break;
                }
            case "mnuUngroup": //replaces ungroup button
                {
                    remove_grid_grouping();
                    break;
                }
            case "mnuClear": //replaces clear button
                {
                    clear_search();
                    break;
                }
            case "mnuDetail": //replaces show/hide details button
                {

                    if (e.Item.Text == "Show detail")
                    {
                        e.Item.Text = "Hide detail";
                        this.gridOrder.DetailRows.ExpandAllRows();
                    }
                    else
                    {
                        e.Item.Text = "Show detail";
                        this.gridOrder.DetailRows.CollapseAllRows();
                    }
                    break;
                }
            case "mnuLock": //replaces lock columns button
                {
                    fix_columns();
                    break;
                }
            default:
                {
                    break;
                }
        }//end switch
    }

    protected void export_grid_data()
    {
        try
        {
            ASPxComboBox _cbo = (ASPxComboBox)this.dxmnuGrid1.Items[0].FindControl("aspxcboExport");
            if (_cbo != null)
            {
                string _exportfile = _cbo.Text.ToString().Replace("Export to", "").ToLower().Trim();  //this.dxmnuGrid1.Items[1].Template.Text.ToString().ToLower(); 

                switch (_exportfile)
                {
                    case "pdf": //pdf
                        {
                            this.orderGridExport.Landscape = true;
                            this.orderGridExport.MaxColumnWidth = 200;
                            this.orderGridExport.PaperKind = System.Drawing.Printing.PaperKind.A4; //Legal would hopefully wide enough for one page but it wasn't
                            this.orderGridExport.BottomMargin = 1;
                            this.orderGridExport.TopMargin = 1;
                            this.orderGridExport.LeftMargin = 1;
                            this.orderGridExport.RightMargin = 1;

                            this.orderGridExport.WritePdfToResponse();
                            break;
                        }
                    case "excel": //excel
                        {
                            this.orderGridExport.WriteXlsToResponse();
                            break;
                        }
                    case "excel 2007": //excel 2008+
                        {
                            this.orderGridExport.WriteXlsxToResponse();
                            break;
                        }
                    case "csv": //csv
                        {
                            this.orderGridExport.WriteCsvToResponse();
                            break;
                        }
                    case "rtf": //rtf
                        {
                            this.orderGridExport.WriteRtfToResponse();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }//end  switch
            }//endif
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            //error = Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.
            //is this a .net bug?
            //do nothing!
            string _ex = ex.ToString();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + _ex + "</div>";
        }
    }

    protected void clear_search()
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

    protected void fix_columns()
    {
       
    }
    #endregion
  
}

