using System;
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

public partial class usercontrols_Pod_Search_Top : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //new linq databinding 
        //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
        //bind_linq_datasource(); 
        //running in server mode
        this.dxlblerr5.Visible = false;
        this.LinqServerModeSearchTop.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModeSearchTop_Selecting);
    
    }

    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModeSearchTop_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        try
        {
            Int32 _companyid = -1; //after testing default to empty string 
            Int32 _userid = -1;

            //company id: always add as a search param as user must be logged in
            //if (Page.Session["user"] != null)
            if (Page.Session["user"] != null)
            {
                _companyid = (Int32)((UserClass)Page.Session["user"]).CompanyId;
                _userid = (Int32)((UserClass)Page.Session["user"]).UserId;
            }


            //dynamic queries using system.Linq.dynamic + Dynamic.cs library
            //20/10/2010 we have build a unique index (OrderIx) from OrderId, TitleId, ContainerSubId as usual primary keys are not going to 
            //be unique in the view. aspxgrid only works properly when it has a unique key 
            e.KeyExpression = "qry_ID"; //"OrderID"; //a key expression is required 

            if (_companyid > 0 && _userid > 0)
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.by_contactID == _userid).OrderByDescending(c => c.qry_ID).Take(10); //c => c.CompanyID == 7
                e.QueryableSource = _nquery;
                //Int32 _count = _nquery.Count();

            }
            else if (_companyid == -1 && _userid > 0)  //internal user 
            {
                //var _nquery = new linq_classesDataContext().view_orders.OrderBy("orderix").OrderByDescending(c => c.OrderIx).Take(10); //.Where(c => c.OrderNumber == -1 );
                var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.by_employeeID == _userid).OrderByDescending(c => c.qry_ID).Take(10); //c => c.CompanyID == 7
                //_count = _nquery.Count();

                e.QueryableSource = _nquery;
            }
            else //they should never reach this as must be logged in to get dashboard but just a precaution
            {
                var _nquery = new linq.linq_query_logDataContext().db_query_logs.Where(c => c.qry_ID == -1);
                //_count = _nquery.Count();

                e.QueryableSource = _nquery;
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr5.Text = ex.Message.ToString();
            this.dxlblerr5.Visible = true;
        }

    }
    //end linq selecting

    //row comand elements
    protected void dxgridsearchtop_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();

            string _id = this.dxgridsearchtop.GetRowValues(_idx, "OrderNumber").ToString();

            if (!string.IsNullOrEmpty(_id))
            {
                switch (_command)
                {
                    case "viewdetails": //open search page
                        {
                            //encryption 
                            string _redirect = "Ord_View_Tracking.aspx?" + "sq=" + wwi_security.EncryptString("Publiship Reference==" + _id, "publiship");
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
            this.dxlblerr5.Text = ex.Message.ToString();
            this.dxlblerr5.Visible = true;
        }
    }
    //end row commands

}
