using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;
using DevExpress.Web.ASPxGridView;
using ParameterPasser;

public partial class price_audit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Int32 _userid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
            bool _intra = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId == -1 ? true : false : false;

            if (_userid != 0)
            {
                if (_intra != false)
                {
                    if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
                    {
                        bind_combos();
                    }
                }
                else
                {
                    Response.Redirect("~/quote_history.aspx");
                }
            }
            else
            {
                Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("pricer/price_audit", "publiship")); 
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //new linq databinding 
        //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
        //bind_linq_datasource(); 
        //running in server mode
        if (!Page.ClientScript.IsClientScriptBlockRegistered("lg_key"))
        {
            register_client_scripts();
        }

        this.LinqServerModePricer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePricer_Selecting);
  
    }

    /// <summary>
    /// this code is used with LinqServerModePricer_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModePricer_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
      
        if (Session["user"] != null)
        {
            string _query = "";
            string _mode = this.dxhfMethod.Contains("mode") ? this.dxhfMethod["mode"].ToString() : "0"; //only 0 or 1 (quick search)
            UserClass _thisuser = (UserClass)Session["user"];
            ParameterCollection _params = new ParameterCollection();
            
            //check quick search parameters
            if (_mode == "1")
            {
                _query = get_filter(); 
            }

            //company id
            if (this.dxcbocompany.Value  != null && this.dxcbocompany.Value.ToString() != "")
            {
                _params.Add("request_company_id", this.dxcbocompany.Value.ToString());   
            }
            //now rebuild query with additional parameters if any
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

            //dynamic queries using system.Linq.dynamic + Dynamic.cs library
            e.KeyExpression = "quote_Id"; //a key expression is required 

            if (!string.IsNullOrEmpty(_query))
            {
                var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(_query); //c => c.CompanyID == 7
                e.QueryableSource = _nquery;
                //Int32 _count = _nquery.Count();

            }
            else //default to display nothing in grid 
            {
                var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(c => c.quote_Id == -1);
                //_count = _nquery.Count();

                e.QueryableSource = _nquery;
            }
        }
        
    }//end linq server mode

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
    //end get filter

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
    //end client script

    /// <summary>
    /// this option replaces the multiple command buttons
    /// get selected value from combo box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string _export = (string)this.aspxcboExport.Value; //default to pdf?

        try
        {
            switch (_export)
            {
                case "0": //pdf
                    {
                        this.gridExport.Landscape = true;
                        this.gridExport.MaxColumnWidth = 200;
                        this.gridExport.PaperKind = System.Drawing.Printing.PaperKind.Legal; //Legal would hopefully wide enough for one page
                        this.gridExport.BottomMargin = 1;
                        this.gridExport.TopMargin = 1;
                        this.gridExport.LeftMargin = 1;
                        this.gridExport.RightMargin = 1;
                        this.gridExport.WritePdfToResponse();
                        break;
                    }
                case "1": //excel
                    {
                        this.gridExport.WriteXlsToResponse();
                        break;
                    }
                case "2": //excel 2008+
                    {
                        this.gridExport.WriteXlsxToResponse();
                        break;
                    }
                case "3": //csv
                    {
                        this.gridExport.WriteCsvToResponse();
                        break;
                    }
                case "4": //rtf
                    {
                        this.gridExport.WriteRtfToResponse();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
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
    //end buton export

    
    protected void btnEndGroup_Click(object sender, EventArgs e)
    {
        remove_grid_grouping();
    }

    /// <summary>
    /// remove all groupings from datagrid
    /// </summary>
    /// 
    protected void remove_grid_grouping()
    {
        try
        {
            for (int i = 0; i < this.gridviewPrices1.Columns.Count; i++)
                if (this.gridviewPrices1.Columns[i] is GridViewDataColumn)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.gridviewPrices1.Columns[i];
                    if (gridViewDataColumn.GroupIndex > -1)
                        this.gridviewPrices1.UnGroup(gridViewDataColumn);
                }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());  
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }
    //end remove grid grouping
    protected void btnEndFilter_Click(object sender, EventArgs e)
    {
        this.txtQuickSearch.Text = "";

        //not needed here no advanced search
        //SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        //_sessionWrapper["mode"] = null;
        //_sessionWrapper["query"] = null;
        //_sessionWrapper["name"] = null;
        //Session["filter"] = null; //so we don't save it again
        
        reset_hidden(); //sets mode back to default 0
        this.gridviewPrices1.DataBind();
    }
    //end clear filter
    protected void reset_hidden()
    {
        this.dxhfMethod.Set("mode", -1);
    }
    //end reset hidden

    /// <summary>
    /// Fires when javascript function submit_query() is called
    /// e.g. by quick search button or advanced filter
    /// or edit_pallet popup requests a batch update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridviewPrices1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        this.gridviewPrices1.DataBind(); 
    }
    //end call back

    protected void gridviewPrices1_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {

        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();
 
            string _quoteId = this.gridviewPrices1.GetRowValues(_idx, "quote_Id").ToString();

            if (!string.IsNullOrEmpty(_quoteId))
            {
                switch(_command){
                    case "editpricer": //drop record into pricer
                        {
                            //encryption 
                            string _redirect = "~/pricer/price_quote.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship");
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false); 
                            break;
                        }
                    case "removepod": //break link to order if there is one
                        {
                            if (remove_pod(wwi_func.vint(_quoteId))) { this.gridviewPrices1.DataBind(); } 
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

    /// <summary>
    /// if this quote linked to an order use this to clear the order number from price_values table
    /// </summary>
    /// <param name="quoteid">Int32 unique id of quote</param>
    protected bool remove_pod(Int32 quoteid)
    {
        //save log id to price table
        int recordsaffected = 0;

        SubSonic.Update upd1 = new SubSonic.Update(DAL.Pricer.Schemas.PriceValue);
        recordsaffected = upd1.Set("po_log_id").EqualTo(0)
                               .Where("quote_id").IsEqualTo(quoteid)
                               .Execute();

        if(recordsaffected >0) return true;
        return false;
    }
    //end remove pod
    /// <summary>
    /// detail grid for shipment size
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void shipment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "quote_Id" };
        Int32 _quoteid = (Int32)_detail.GetMasterRowFieldValues(_keys);

        if (_quoteid > 0)
        {
            //var _nquery = new linq_pricerDataContext().shipment_sizes.Where(c => c.quote_id == _quoteid);
            //_detail.DataSource = _nquery;
            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select().From(DAL.Pricer.Tables.ShipmentSize).WhereExpression("quote_id").IsEqualTo(_quoteid);
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            _detail.DataSource = _dt;
            _rd.Close(); 
        }

    } //end shipment size data select

    /// <summary>
    /// detail grid for costing 1 (pre-palletised)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void costing1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "quote_Id" };
        Int32 _quoteid = (Int32)_detail.GetMasterRowFieldValues(_keys);

        if (_quoteid > 0)
        {
            //var _nquery = new linq_pricerDataContext().costing_summaries.Where(c => c.quote_Id == _quoteid && c.summary_type == "pre-palletised");
            //var _nquery = from c in new linq_pricerDataContext().costing_summaries
            //              where c.quote_Id == _quoteid && c.summary_type == "pre-palletised"
            //              select c;
            ////int _count = _nquery.Count();
            //_detail.DataSource = _nquery;

            //datareader is faster?
            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select().From(DAL.Pricer.Tables.CostingSummary).WhereExpression("quote_id").IsEqualTo(_quoteid).And("summary_type").IsEqualTo("pre-palletised");
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            _detail.DataSource = _dt;
            _rd.Close(); 

        }

    } //end costing1 data select

    /// <summary>
    /// detail grid for costing 1 (loose)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void costing2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "quote_Id" };
        Int32 _quoteid = (Int32)_detail.GetMasterRowFieldValues(_keys);

        if (_quoteid > 0)
        {
            //var _nquery = new linq_pricerDataContext().costing_summaries.Where(c => c.quote_Id == _quoteid && c.summary_type == "loose");
            //_detail.DataSource = _nquery;
            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select().From(DAL.Pricer.Tables.CostingSummary).WhereExpression("quote_id").IsEqualTo(_quoteid).And("summary_type").IsEqualTo("loose");
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            _detail.DataSource = _dt;
            _rd.Close(); 
        }

    } //end costing1 data select

    /// <summary>
    /// databind combos deprecated bound to sqldatasource
    /// </summary>
    protected void bind_combos()
    {
        //company name
        //string[] _cols = { "NameAndAddressBook.CompanyId", "NameAndAddressBook.CompanyName" }; //MUST have defined columns or datareader will not work!
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Where("Customer").IsEqualTo(true);
        //
        //NameAndAddressBookCollection _company = new NameAndAddressBookCollection();
        //_company.LoadAndCloseReader(_query.ExecuteReader());
        //DataTable _dt = (DataTable)_company.ToDataTable();
        //
        //this.dxcbocompany.DataSource = _dt;
        //this.dxcbocompany.ValueField = "CompanyId";
        //this.dxcbocompany.TextField = "CompanyName";
        //this.dxcbocompany.DataBind();
        
    }
    //end bind combos
    
    //not in use
    protected void dxcbocompany_DataBound(object sender, EventArgs e)
    {
        //11/03/2011 append ALL option as 1st item in dropdown
        DevExpress.Web.ASPxEditors.ListEditItem _li = new DevExpress.Web.ASPxEditors.ListEditItem("(All)", "-1");
        this.dxcbocompany.Items.Insert(0, _li);
        //default to all users
        this.dxcbocompany.SelectedIndex = 0; 

    }

    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter= !string.IsNullOrEmpty(e.Filter) ? e.Filter: "";
        {
            //use datareaders - much faster than loading into collections
            string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };
         
            //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "CompanyID";
            _combo.TextField = "CompanyName";
            _combo.DataBind();
 
            //use sqldatasource 
            //this.sdsCompany.ConnectionString = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ToString();
            //this.sdsCompany.SelectCommand = @"SELECT [CompanyID], [CompanyName], [Customer] FROM (select [CompanyID], [CompanyName], [Customer], row_number()over(order by t.[CompanyName]) as [rn] from [NameAndAddressBook] as t where (([CompanyName]) LIKE @filter)) as st where st.[rn] between @startIndex and @endIndex;";
            //this.sdsCompany.SelectParameters.Clear();
            //this.sdsCompany.SelectParameters.Add("filter", TypeCode.String, string.Format("%{0}%", _filter));
            //this.sdsCompany.SelectParameters.Add("startindex", TypeCode.Int32, (e.BeginIndex + 1).ToString());
            //this.sdsCompany.SelectParameters.Add("endindex", TypeCode.Int32, (e.EndIndex + 1).ToString());
            //_combo.DataSource = this.sdsCompany;
            //_combo.DataBind();
        }
    }
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //use datareaders - much faster than loading into collections
        string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };
        
        Int32 _id = wwi_func.vint(e.Value.ToString()); 
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.TextField = "CompanyName";
        _combo.DataBind();

        //this.sdsCompany.ConnectionString = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ToString();
        //this.sdsCompany.SelectCommand = @"SELECT [CompanyID], [CompanyName], [Customer] FROM [NNameAndAddressBook] WHERE (CompanyID = @ID)";
        //this.sdsCompany.SelectParameters.Clear();
        //this.sdsCompany.SelectParameters.Add("ID", TypeCode.Int32, e.Value.ToString());
        //_combo.DataSource = this.sdsCompany;
        //_combo.DataBind();
    }
    //end incremental filtering of company name

    /// <summary>
    /// row created show break pod link button if quote is linked to pod
    /// button clientviasible=false by default
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridviewPrices1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            DevExpress.Web.ASPxGridView.ASPxGridView _grid = (DevExpress.Web.ASPxGridView.ASPxGridView)sender;
            Int32 _onKey = e.GetValue("order_no")!= null? wwi_func.vint(e.GetValue("order_no").ToString()): 0;

            if (_onKey > 0)
            {
                //find template controls
                DevExpress.Web.ASPxGridView.GridViewDataColumn _col1 = (DevExpress.Web.ASPxGridView.GridViewDataColumn)_grid.Columns["colorderno"];
                DevExpress.Web.ASPxEditors.ASPxButton _btn = (DevExpress.Web.ASPxEditors.ASPxButton)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col1, "dxbtnbreak");
                //var _chk = (DevExpress.Web.ASPxGridView.Rendering.GridViewTableCommandCell)e.Row.Cells[0]; 
                //and pass data from hidden field if it's stored - make sure text box is ticked
                if (_btn != null) 
                {   
                    _btn.ClientVisible = true;
                }
            }
        }
    }
    //end row created
}
