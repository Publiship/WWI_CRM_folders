using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DAL.Pricer;
using ParameterPasser;

public partial class Popupcontrol_order_selector : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //running in server mode
        this.LinqServerModePod.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePod_Selecting);
 
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            bind_dll_archive(); 
            //sending selected orderid back to price quote?
            //sending selected orderid back to file manager? 
            string _ref = Request.QueryString["qr"] != null ? Request.QueryString["qr"].ToString() : "";
            if (!string.IsNullOrEmpty(_ref))
            {
                this.dxhfsource.Clear();
                this.dxhfsource.Set("requestid", wwi_security.EncryptString(_ref, "publiship"));
                //hide tick boxes - single selection only
                this.dxgdvOrder.Columns["#"].Visible = false;
                this.dxbtnsave.Visible = false;
            }
            

        }
    
    }
    //end load

    #region grid databinding
    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModePod_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {

        Int32 _companyid = -1; //after testing default to empty string 
        SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        ParameterCollection _params = new ParameterCollection();
        //check if session created from advanced search, in which case we can use the parameters passed back
        string _query = "";
        int _mode = this.dxhfsource.Contains("mode") ? wwi_func.vint(this.dxhfsource["mode"].ToString()) : 1; //default mode to quick search

        //a simplified version of the methods used in order tracking - no history but advanced search is available
        if (_sessionWrapper["query"] != null && _mode == 2)
        {
            _query = _sessionWrapper["query"].ToString();
        }
        else
        {
            _query = get_filter();
        }
        //company id: always add as a search param if user is logged in
        //if (Page.Session["user"] != null)
        //company id: always add as a search param if user is logged in UNLESS _mode = -1 which we can use to bypass params
        if (Page.Session["user"] != null)
        {
            _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;

            if (_companyid != -1) //-1 is a WWI company
            {
                Parameter _p = return_default_view(-1);
                if (_p != null) { _params.Add(_p); }
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

        }
        else //default to display nothing in grid 
        {
            //var _nquery = new linq_classesDataContext().view_orders.Where(c => c.OrderNumber == -1);
            var _nquery = new linq.linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months).Where(c => c.OrderNumber == -1);
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }
    }
    //end linq 
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
    //end get filter name

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
    //end default view
    /// <summary>
    /// update pricer with linked order id and order table with the quote id
    /// </summary>
    /// <param name="orderid">int32 orderid from grid</param>
    /// <returns></returns>
    protected bool save_to_quote(Int32 orderid, Int32 orderno, Int32 quoteid)
    {
        bool _result = false;
        int recordsaffected = 0;

        //save orderid to price values


        if (orderid > 0 && quoteid > 0)
        {
            //append to audit log
            UserClass _thisuser = (UserClass)Session["user"];
            DAL.Pricer.PriceOrderLog _oq = new DAL.Pricer.PriceOrderLog();
            _oq.CompanyId = _thisuser.CompanyId;
            _oq.UserId = _thisuser.UserId;
            _oq.QuoteId = quoteid;
            _oq.LogDate = DateTime.Now;
            _oq.OrderId = orderid;
            _oq.OrderNo = orderno;
            _oq.Save();

            //get log id
            Int32 _newid = (Int32)_oq.GetPrimaryKeyValue();

            //save log id to price table
            SubSonic.Update upd1 = new SubSonic.Update(DAL.Pricer.Schemas.PriceValue);
            recordsaffected = upd1.Set("po_log_id").EqualTo(_newid)
                                   .Where("quote_id").IsEqualTo(quoteid)
                                   .Execute();

            //save quote id to order table
            SubSonic.Update upd2 = new SubSonic.Update(DAL.Logistics.Schemas.OrderTable);
            recordsaffected = upd2.Set("quote_id").EqualTo(quoteid)
                                   .Where("OrderID").IsEqualTo(orderid)
                                   .Execute();

            if (recordsaffected > 0) { _result = true; }
        }
        return _result;
    }
#endregion

    #region grid custom row commands
    //custom row commands
    protected void dxgdvOrder_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();

            string _orderid = this.dxgdvOrder.GetRowValues(_idx, "OrderID").ToString();
            
            if (!string.IsNullOrEmpty(_orderid))
            {
                switch (_command)
                {
                    case "selectpod": //tick button clicked drop record into pricer or return selected order id to form
                        {
                            //performs same action as save button below
                            string _key = _orderid + ";" + this.dxgdvOrder.GetRowValues(_idx, "OrderNumber").ToString();
                            this.dxhfsource.Set("orderidkey", _key); 
                            process_request(); 
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
    //end custom row commands
    #endregion

    #region form events button click, dll etc
    protected void dxbtnsave_Click(object sender, EventArgs e)
    {
        process_request(); 
    }
    //end button save

    /// <summary>
    /// reset search
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEndFilter_Click(object sender, EventArgs e)
    {
        this.txtQuickSearch.Text = "";
        this.dxhfsource.Set("mode", 1); //default to quick search 
        SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        _sessionWrapper["mode"] = null;
        _sessionWrapper["query"] = null;
        _sessionWrapper["name"] = null;

        this.dxgdvOrder.DataBind();
    }
    #endregion

    #region subs
    /// <summary>
    /// custom call back requests for grid depending on what we are doing
    /// </summary>
    protected void process_request()
    {
        try
        {

            this.dxpnlerr.Visible = false;
            string[] _fields = { "OrderID" };
            //does not work!
            //Int32 _orderid = wwi_func.vint(this.dxgdvOrder.GetSelectedFieldValues(_fields).ToString());
            string _key = this.dxhfsource.Contains("orderidkey") ? this.dxhfsource.Get("orderidkey").ToString() : "";
            if (!string.IsNullOrEmpty(_key))
            {
                string[] _keys = _key.Split(';');
                Int32 _orderid = wwi_func.vint(_keys[0]);
                Int32 _orderno = wwi_func.vint(_keys[1]);
                Int32 _quoteid = 0;

                string _ref = this.dxhfsource.Contains("requestid") ? wwi_security.DecryptString(this.dxhfsource.Get("requestid").ToString(), "publiship") : "";

                if (_ref != "fm") //fm = filemanager
                {
                    _quoteid = wwi_func.vint(_ref);

                    if (_orderid > 0 && _quoteid > 0)
                    {
                        if (save_to_quote(_orderid, _orderno, _quoteid))
                        {
                            //close popup and send order id back to parent form
                            ClientScript.RegisterStartupScript(GetType(), "CB1_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));window.parent.submit_callback_request(" + _orderid + ");", true);
                        }
                        else
                        {
                            this.lblerr.Text = "We have not been able to save this request, please refer to Publship I.T. (Quote Id:" + _quoteid + ", Order Number:" + _orderno + ")";
                            this.dxpnlerr.Visible = true;

                        }
                    }//end order/quote >0
                }
                else //just need to pass selected order id back to parent form
                {
                    //close popup and send order id back to parent form
                    //sending to call back control does not work we need to check orderno on page load try using a session key
                    //SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
                    //_sessionWrapper["or"] = _orderno.ToString();
                    ClientScript.RegisterStartupScript(GetType(), "CB2_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));window.parent.submit_order_callback(" + _orderno + ");", true);
                }
            }//end !nullor empty
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.lblerr.Text = _ex;
            this.dxpnlerr.Visible = true;
        }
    }
    //end process request

    
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
    #endregion

    #region grid events
    /// <summary>
    /// Fires when javascript function submit_query() is called
    /// e.g. by quick search button or advanced filter
    /// or edit_pallet popup requests a batch update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgdvOrder_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //not being used
        //if (e.Parameters == "order") //link order to quote
        //{
        //    string[] _fields = { "OrderID" };
        //    List<Object> _orderids = this.dxgdvOrder.GetSelectedFieldValues(_fields);
        //    if (_orderids.Count > 0)
        //    {
        //        //process_request();
        //        string x = _orderids.Count.ToString();  
        //    }
        //}
        
        //rebind data
        //this.gridOrder.DataSource = get_datatable();
        this.dxgdvOrder.DataBind();
    }
    //end grid call back
    /// <summary>
    /// javascript added to rows on the fly so we can track what has been ticked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgdvOrder_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            DevExpress.Web.ASPxGridView.ASPxGridView _grid = (DevExpress.Web.ASPxGridView.ASPxGridView)sender;
            //string _hfKey = "key" + e.KeyValue.ToString(); //this seems to be problematic (string too big for key??)
            //string _hfKey = "key" + e.GetValue("OrderID").ToString();
            string _hfKey = "orderidkey";

            //find order id
            //string _test = e.GetValue("OrderID").ToString();

            //find template controls
            DevExpress.Web.ASPxGridView.GridViewDataColumn _col1 = (DevExpress.Web.ASPxGridView.GridViewDataColumn)_grid.Columns["chkcustom"];
            DevExpress.Web.ASPxEditors.ASPxCheckBox _chk = (DevExpress.Web.ASPxEditors.ASPxCheckBox)_grid.FindRowCellTemplateControl(e.VisibleIndex, _col1, "dxchktick");
            //var _chk = (DevExpress.Web.ASPxGridView.Rendering.GridViewTableCommandCell)e.Row.Cells[0]; 
            //and pass data from hidden field if it's stored - make sure text box is ticked
            if (_chk != null)
            {
                _chk.ClientSideEvents.CheckedChanged = "function(s,e){rowTicked(" + e.GetValue("OrderID") + "," + e.GetValue("OrderNumber") + ");}";

                if (this.dxhfsource.Contains(_hfKey))
                {
                    if (this.dxhfsource.Get("orderidkey").ToString() == e.GetValue("OrderID").ToString()) { _chk.Checked = true; }

                }

            }
        }
    }

    #endregion

}
