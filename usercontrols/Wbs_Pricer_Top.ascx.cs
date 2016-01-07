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

public partial class usercontrols_Wbs_Pricer_Top : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //new linq databinding 
        //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
        //bind_linq_datasource(); 
        //running in server mode
        this.dxlblerr6.Visible = false;
        this.LinqServerModePricerTop.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePricerTop_Selecting);
    
    }

    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModePricerTop_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
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
            e.KeyExpression = "quote_Id"; //"OrderID"; //a key expression is required 

            if (_companyid > 0 && _userid > 0)
            {
                //var _query = new linq_classesDataContext().view_orders.Where(_filter);
                var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(p => p.request_company_id == _companyid && p.request_user_id == _userid && p.client_visible == true).OrderByDescending(p => p.quote_Id).Take(10); //c => c.CompanyID == 7
                e.QueryableSource = _nquery;
                //Int32 _count = _nquery.Count();

            }
            else //they should never reach this as must be logged in to get dashboard but just a precaution
            {
                var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(p => p.client_visible == true).OrderByDescending(p => p.quote_Id).Take(10); //c => c.CompanyID == 7

                //var _nquery = new linq_pricer_view1DataContext().view_price_clients.Where(p => p.quote_Id == -1); 
                //_count = _nquery.Count();

                e.QueryableSource = _nquery;
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr6.Text = ex.Message.ToString();
            this.dxlblerr6.Visible = true;
        }
    }
    //end linq selecting

    //row comand elements
    protected void dxgridpricertop_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();

            string _quoteId = this.dxgridpricertop.GetRowValues(_idx, "quote_Id").ToString();

            if (!string.IsNullOrEmpty(_quoteId))
            {
                switch (_command)
                {
                    case "editpricer": //drop record into pricer to re-calc
                        {
                            //encryption 
                            string _redirect = "../pricer/price_quote.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=0"; 
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false);
                            break;
                        }
                    case "copypricer": //drop record into pricer as a new record (client_visisble=true)
                        {
                            //encryption 
                            string _redirect = "../pricer/price_quote.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=1";
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false);
                            break;
                        }

                    case "hidequote": //flag as clientvisible = 0
                        {
                            //if (hide_quote(wwi_func.vint(_quoteId))) { this.gridviewPrices1.DataBind(); }
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
            this.dxlblerr6.Text = ex.Message.ToString();
            this.dxlblerr6.Visible = true;
        }
    }
    //end row commands

    /// <summary>
    /// on row create disable commandbuttons unless user is logged in
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridpricertop_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (Session["user"] == null)
        {
            GridViewDataColumn _col1 = (GridViewDataColumn)this.dxgridpricertop.Columns["Calc"];
            DevExpress.Web.ASPxEditors.ASPxButton _cmd1 = (DevExpress.Web.ASPxEditors.ASPxButton)this.dxgridpricertop.FindRowCellTemplateControl(e.VisibleIndex, _col1, "dxbtnquote");
            if (_cmd1!= null) { _cmd1.ClientEnabled  = false; }
        
            GridViewDataColumn _col2 = (GridViewDataColumn)this.dxgridpricertop.Columns["Copy"];
            DevExpress.Web.ASPxEditors.ASPxButton _cmd2 = (DevExpress.Web.ASPxEditors.ASPxButton)this.dxgridpricertop.FindRowCellTemplateControl(e.VisibleIndex, _col2, "dxbtncopy");
            if (_cmd2 != null) { _cmd2.ClientEnabled = false; }
        }
    }
    //end row created
}
