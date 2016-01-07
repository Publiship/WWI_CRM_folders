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
using SubSonic;
using DevExpress.Web.ASPxGridView;
using linq;

public partial class order_template_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                bind_commands("SearchTemplates"); //search commands (do we have any for template search?)
                
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("system_templates/order_template_search", "publiship"));
        }

        //filtering linq datasource
        this.linqSearch.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqOrders_Selecting);
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
            //need this!
            e.KeyExpression = "OrderTemplateID";

            //make sure you have: using System.Linq; using System.Linq.Dynamic; using System.Linq.Expressions; or you will get the error
            //The type arguments for method 'System.Linq.Enumerable.Where<TSource>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,int,bool>)' cannot be inferred from the usage.
            //if (!string.IsNullOrEmpty(_query))
            //{
            //
            //    var _nquery = new linq.linq_view_order_templatesDataContext().view_order_templates.Where(_query);   
            //    e.QueryableSource = _nquery;
            //}
            //else
            //{
            //    var _nquery = new linq.linq_view_order_templatesDataContext().view_order_templates.Where(_t => _t.OrderTemplateID == -1);   
            //    e.QueryableSource = _nquery;
            //}
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.Visible = true;
        }
    }

   
    #endregion

    #region call backs
    /// <summary>
    /// fires when e.g. called from javascript
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridOrders_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //if(e.Parameters == "new_order"){
        //    Response.Redirect("~/Order_Edit?mode=New");
        //}
        //else
        //{
        this.dxgridOrders.DataBind();
        //}
    }

    /// <summary>
    /// custom command buttons view/edit/delete (maybe?)/create order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridOrders_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView _grid = (ASPxGridView)sender;
        //get order number off grid
        string[] _fields = { "OrderTemplateID" };
        //pass order no as encryped data
        string _id = wwi_security.EncryptString(_grid.GetRowValues(e.VisibleIndex, _fields).ToString(), "publiship");
        //track requesting page so we can return to it
        //string _req = this.dxhfSearch.Get("req").ToString(); //populated on page load   //wwi_security.EncryptString("Search", "publiship");

        //page to redirect build as required
        string _page = "";

        //pass templateid as primary key id 'pid'
        switch (e.ButtonID.ToString())
        {
            case "cmdView": //redirect to order_view.aspx
                {
                    _page = string.Format("../system_templates/order_template.aspx?pid={0}&mode={1}", _id, "ReadOnly");
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page);  //Response.Redirect(_page); 
                    break;
                }
            case "cmdEdit": //redirect to order_edit.aspx
                {
                    _page = string.Format("../system_templates/order_template.aspx?pid={0}&mode={1}", _id, "Edit");
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            case "cmdCreateOrder": //use template to create on order 
                {
                    _page = string.Format("../system_templates/order_template.aspx?pid={0}&mode={1}", _id, "ReadOnly");
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            default:
                {
                    break;
                }
        }//end switch

    }
    //end custom buttons
    protected void dxgridOrders_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        string _fx = this.dxgridOrders.FilterExpression.ToString();
        if (!string.IsNullOrEmpty(_fx)) { this.dxgridOrders.SettingsText.Title = _fx; }

        
    }
    #endregion

    #region form binding

    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_commands(string mode)
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='" + mode + "']"; //you need this or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuToolbar.DataSource = _xml;
        this.dxmnuToolbar.DataBind();

    }
    #endregion

    #region functions
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion

}
