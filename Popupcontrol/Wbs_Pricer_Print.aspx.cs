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

public partial class Wbs_Pricer_Print : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            //Int32 _userid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;

            //if (_userid != 0)
            //{
            //    if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
            //    {
            //
            //    }
            //
            //}
            //else
            //{
            //    Response.Redirect("~/Sys_Session_Login.aspx?" + "rp=" + wwi_security.EncryptString("Wbs_Pricer_Audit_1", "publiship")); 
            //}
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
        this.LinqServerModePricer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePricer_Selecting);

    }

    /// <summary>
    /// this code is used with LinqServerModePricer_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModePricer_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
      

        //get user id to limit result set
        if (Session["user"] != null)
        {
            UserClass _thisuser = (UserClass)Session["user"];
            Int32 _uid = _thisuser.UserId;
            Int32 _cid = _thisuser.CompanyId; 

            //dynamic queries using system.Linq.dynamic + Dynamic.cs library
            e.KeyExpression = "quote_Id"; //a key expression is required 

            if (_uid != 0 && _cid != 0)
            {
                var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(
                    c => c.request_date.GetValueOrDefault().Date == DateTime.Today && 
                        c.client_visible == true &&
                        c.request_user_id == _thisuser.UserId && 
                        c.request_company_id == _thisuser.CompanyId 
                    ); //c => c.CompanyID == 7
                e.QueryableSource = _nquery;
                //Int32 _count = _nquery.Count();

            }
        }
        else //default to display nothing in grid 
        {
            e.KeyExpression = "quote_Id"; //a key expression is required 

            var _nquery = new linq.linq_pricer_view1DataContext().view_price_clients.Where(c => c.quote_Id == -1);
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }
        
    }//end linq server mode

    

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
                        this.gridExport.PaperKind = System.Drawing.Printing.PaperKind.A4 ; 
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
            string _ex = ex.Message.ToString();
            Response.Write(ex); 
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + _ex + "</div>";
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
            Response.Write(ex.Message.ToString());  
            //this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }
    //end remove grid grouping
    protected void btnEndFilter_Click(object sender, EventArgs e)
    {
        //this.txtQuickSearch.Text = "";disabled as this text box is not on this form
         
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
                            string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=0";
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false); 
                            break;
                        }
                    case "copypricer": //drop record into pricer as a new record (client_visisble=true)
                        {
                            //encryption 
                            string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=1";
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                             Response.Redirect(_redirect, false);
                            break;
                        }

                    case "hidequote": //flag as clientvisible = 0
                        {
                            if (hide_quote(wwi_func.vint(_quoteId))) { this.gridviewPrices1.DataBind(); } 
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
    //end row commands

    /// <summary>
    /// flag quote as hidden from client vieww
    /// </summary>
    /// <param name="quoteid">Int32 unique id of quote</param>
    protected bool hide_quote(Int32 quoteid)
    {
        //save log id to price table
        int recordsaffected = 0;

        SubSonic.Update upd1 = new SubSonic.Update(DAL.Pricer.Schemas.PriceValue);
        recordsaffected = upd1.Set("client_visible").EqualTo(false)
                               .Where("quote_id").IsEqualTo(quoteid)
                               .Execute();

        if (recordsaffected > 0) return true;
        return false;
    }
    //end hide quote
}
