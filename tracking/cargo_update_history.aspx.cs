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

public partial class cargo_update_history : System.Web.UI.Page
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
        this.linqservermodecargo.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqservermodesearch_Selecting);

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
            Int32 _cid = _thisuser.CompanyId;
            Int32 _uid = _thisuser.UserId; 
            Parameter _p = new Parameter();

            //dynamic queries using system.Linq.dynamic + Dynamic.cs library
            e.KeyExpression = "cargoupdateid"; //a key expression is required 


            if (_cid != -1) //external company not WWI so search in by company id
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.Linq_Classes_CargoDataContext().view_cargo_updates.Where(c => c.companyid == _cid);
                //_count = _query.Count();
                e.QueryableSource = _nquery;
            }
            else //search in byemployeeid
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.Linq_Classes_CargoDataContext().view_cargo_updates.Where(c => c.userid == _uid);
                //_count = _query.Count();
                e.QueryableSource = _nquery;
            }

        }
        else
        {
            e.KeyExpression = "cargoupdateid"; //a key expression is required 

            //var _query = new linq_classesDataContext().view_orders.Where(_filter);
            var _nquery = new linq.Linq_Classes_CargoDataContext().view_cargo_updates.Where(c => c.cargoupdateid == 0);
            //_count = _query.Count();
            e.QueryableSource = _nquery;
        }
    }//end linq server mode

    /// <summary>
    /// derive filter from selected row and apply to cargo update editor - find by selected order id
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

            string _orderid = this.dxgrdquerylog.GetRowValues(_idx, "orderid").ToString();

            if (!string.IsNullOrEmpty(_orderid))
            {
                SessionParameterPasser _sessionWrapper = new SessionParameterPasser("~/Ord_Edit_Cargo.aspx");
                _sessionWrapper["query"] = "orderid==" + _orderid;
                _sessionWrapper["name"] = "query_order_id";
                _sessionWrapper["mode"] = "0";
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
    //end row command
 
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Ord_Edit_Cargo.aspx"); 
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

