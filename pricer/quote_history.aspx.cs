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

public partial class quote_history : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Int32 _userid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;

            if (_userid != 0)
            {
                if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
                {

                }

            }
            else
            {
                Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("quote_history", "publiship")); 
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
            string _query = "";
            string _mode = this.dxhfMethod.Contains("mode") ? this.dxhfMethod["mode"].ToString() : "0"; //only 0 or 1 (quick search)
            
            UserClass _thisuser = (UserClass)Session["user"];
            Int32 _id = _thisuser.UserId;
            ParameterCollection _params = new ParameterCollection();
            
            //check quick search parameters
            if (_mode == "1")
            {
                _query = get_filter(); 
            }

            //020911 only show records where client_visible = true
            Parameter _p0 = new Parameter();
            _p0.Name = "client_visible";
            _p0.DefaultValue = true.ToString();
            _params.Add(_p0); 

            //check user id
            Parameter _p1= new Parameter(); 
            _p1.Name = "request_user_id";
            _p1.DefaultValue = _thisuser.UserId.ToString();
            _params.Add(_p1); 

            //and company id
            Parameter _p2 = new Parameter();
            _p2.Name = "request_company_id";
            _p2.DefaultValue = _thisuser.CompanyId.ToString();
            _params.Add(_p2); 


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
                            string _redirect = "~/pricer/price_quote.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=0";
                            //string _redirect = "Wbs_Pricer_Service.aspx?" + "qr=" + _quoteId.ToString();
                            Response.Redirect(_redirect, false); 
                            break;
                        }
                    case "copypricer": //drop record into pricer as a new record (client_visisble=true)
                        {
                            //encryption 
                            string _redirect = "~/pricer/price_quote.aspx?" + "qr=" + wwi_security.EncryptString(_quoteId, "publiship") + "&cv=1";
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
