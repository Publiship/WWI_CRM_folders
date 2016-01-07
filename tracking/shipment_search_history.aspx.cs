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
using DevExpress.Web.ASPxGridView;
using ParameterPasser;

public partial class shipment_search_history : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }

        //new linq databinding 
        //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
        //bind_linq_datasource(); 
        //running in server mode
        this.linqservermodequerylog.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqservermodesearch_Selecting);

    }


    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linqservermodesearch_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        ParameterCollection _params = new ParameterCollection();

        if (Session["user"] != null)
        {
            UserClass _thisuser = (UserClass)Session["user"];
            Int32 _id = _thisuser.UserId;
            Parameter _p = new Parameter();

            //dynamic queries using system.Linq.dynamic + Dynamic.cs library
            e.KeyExpression = "qry_ID"; //a key expression is required 


            if (_thisuser.CompanyId != -1) //external company not WWI so search in bycontactid
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.by_contactID == _id); //.Where(c => c.qry_source == "OV"); restrict by query source too?
                //_count = _query.Count();
                e.QueryableSource = _nquery;
            }
            else //search in byemployeeid
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.by_employeeID == _id); //.Where(c => c.qry_source=="OV");
                //_count = _query.Count();
                e.QueryableSource = _nquery;
            }
        }
        else
        {
            string _userip = userRequestingIP();

            e.KeyExpression = "qry_ID"; //a key expression is required 

            //var _query = new linq_classesDataContext().view_orders.Where(_filter);
            //09/09/2011 don't load by IP, not very secure, just default to 0 records
            //var _nquery = new linq_classesDataContext().db_query_logs.Where(c => c.log_ip == _userip).Where(c => c.by_employeeID == 0).Where(c => c.by_contactID == 0); //.Where(c => c.qry_source == "OV");
            var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.by_employeeID == -1).Where(c => c.by_contactID == -1); //.Where(c => c.qry_source == "OV");
            
            //_count = _query.Count();
            e.QueryableSource = _nquery;
        }
    }//end linq server mode

    /// <summary>
    /// derive filter from selected row and apply to orders view
    /// rebuild session with selected filter
    /// redirect to orders search page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdquerylog_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};

            string _fex = this.dxgrdquerylog.GetRowValues(_idx, "qry_text").ToString();
            string _fname = this.dxgrdquerylog.GetRowValues(_idx, "qry_desc").ToString();
            string _fsource = this.dxgrdquerylog.GetRowValues(_idx, "qry_source").ToString();
            string _default = "shipment";//default to shipment tracking
            string _url = "../tracking/{0}_tracking.aspx";

            _url = _fsource != "" ? string.Format(_url, _fsource): string.Format(_url, _default);    
            //old code we are using form names instead of codes in qry_source as of 20/03/15 
            //if (_fsource == "CNTR") { 
            //    _url = "../tracking/container_tracking.aspx";
            //}
            //else if(_fsource == "DLVRY"){
            //     _url = "../tracking/delivery_tracking.aspx";
            //}
            //else if(_fsource == "NSHIP"){
            //     _url =  "../tracking/not_shipped_tracking.aspx";
            //}
           
            if (!string.IsNullOrEmpty(_fex))
            {
                SessionParameterPasser _sessionWrapper = new SessionParameterPasser(_url);
                _sessionWrapper["query"] = _fex;
                _sessionWrapper["name"] = _fname;
                _sessionWrapper["mode"] = "0";
                _sessionWrapper["source"] = _fsource;
                _sessionWrapper.PassParameters();
                //force submit of query and set mode to 0 (history)
                //this does not work!
                //Page.ClientScript.RegisterStartupScript(GetType(), "QRY_MOD", "window.submit_query(0);", true);
                //Response.Redirect("~/Default.aspx",false); 
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message.ToString());  
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../tracking/shipment_tracking.aspx"); 
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

}

