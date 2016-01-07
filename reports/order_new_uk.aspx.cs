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

public partial class order_new_uk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                this.dxlblDate.Text = DateTime.Now.ToShortDateString(); 
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("reports/order_new_uk", "publiship"));
        }

        //filtering linq datasource
        this.linqOrders.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqOrders_Selecting);
    }

    #region datagrid
    /// <summary>
    /// called on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linqOrders_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        try
        {
            //params for UK ORDERS
            //return recorda after start exworks date 25/09/2011 equivalent to (CAST('2011-09-25' as datime) in t-sql
            DateTime _startexworks = wwi_func.vdatetime("2011-09-25");
            //return records after starting order number 1000000 for uk orders 
            int _loworder = 0;
            int _highorder = 1000000;
            //agetnatoriginid and agentatdestinationid should be the same e.g. 1018 for uk
            int _agent = 1018;
            //@filecoverprinted should be 0
            int _filecoverprinted = 0;

            //need this!
            e.KeyExpression = "OrderID";

            //make sure you have: using System.Linq; using System.Linq.Dynamic; using System.Linq.Expressions; or you will get the error
            //The type arguments for method 'System.Linq.Enumerable.Where<TSource>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,int,bool>)' cannot be inferred from the usage.
            var _nquery = new linq.linq_new_orders_udfDataContext().new_orders(_startexworks, _loworder, _highorder, _agent, _filecoverprinted);
            e.QueryableSource = _nquery;
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.Visible = true;
        }
    }

   
    #endregion
    
    #region functions
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion

    #region menu events
    protected void dxmnuGrid1_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        switch (e.Item.Name.ToString())
        {
            case "mnuExport": //replaces export button & combo
                {
                    export_grid_data();
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
                string _exportfile = _cbo.Text.ToString().Replace("Export to","").ToLower().Trim();  //this.dxmnuGrid1.Items[1].Template.Text.ToString().ToLower(); 
          
                switch (_exportfile)
                {
                    case "pdf": //pdf
                        {
                            this.gridExporter.Landscape = true;
                            this.gridExporter.MaxColumnWidth = 200;
                            this.gridExporter.PaperKind = System.Drawing.Printing.PaperKind.A4; //Legal would hopefully wide enough for one page but it wasn't
                            this.gridExporter.BottomMargin = 1;
                            this.gridExporter.TopMargin = 1;
                            this.gridExporter.LeftMargin = 1;
                            this.gridExporter.RightMargin = 1;

                            this.gridExporter.WritePdfToResponse();
                            break;
                        }
                    case "excel": //excel
                        {
                            this.gridExporter.WriteXlsToResponse();
                            break;
                        }
                    case "excel 2007": //excel 2008+
                        {
                            this.gridExporter.WriteXlsxToResponse();
                            break;
                        }
                    case "csv": //csv
                        {
                            this.gridExporter.WriteCsvToResponse();
                            break;
                        }
                    case "rtf": //rtf
                        {
                            this.gridExporter.WriteRtfToResponse();
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
            this.dxlblErr.Text += "<div class='alert'>Error description" + ": " + _ex + "</div>";
            this.dxpnlErr.ClientVisible = true;
        }
    }
    #endregion
}
