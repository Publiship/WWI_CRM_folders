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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class order_search : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //filtering linq datasource
        this.linqOrders.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqOrders_Selecting);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (isLoggedIn())
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                bind_commands("Search"); //search commands
                //date range for order grid
                bind_dll_archive();
                //29.01.15 default to all orders so we load page with an empty grid until user enters a filter
                //default open to orders for logged in user if it's publiship UK otherwise default to all orders?
                this.dxhfOrder.Clear();
                this.dxlblSearch.Text = "All orders";
                //if ((int)((UserClass)Page.Session["user"]).OfficeId == 1)
                //{
                //    this.dxhfOrder.Add("orders", "user");
                //    this.dxlblSearch.Text = "My orders";
                //}
                //else
                //{
                //    this.dxlblSearch.Text = "All orders";
                //}

                //store option in hidden field
                this.dxhfOrder.Add("req", wwi_security.EncryptString("Search", "publiship"));
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_orders/order_search", "publiship"));
        }

        
    }

    #region datagrid events
    /// <summary>
    /// called on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linqOrders_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        try
        {
            //default filter everything after 01/01/2007
            string _f = "";
            string _query = ""; //"(ExWorksDate >= DateTime.Parse(\"01/01/2007\"))";
            string _officeid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).OfficeId.ToString() : "";
            string _userid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).UserId.ToString() : ""; //use id=125 for testing
            ParameterCollection _params = new ParameterCollection();
            //number of months to include after start date
            int _months = 12;
            int _dllvalue = this.dxcboRange.Value != null ? wwi_func.vint(this.dxcboRange.Value.ToString()) : 1;
            //multiply base * 12 to get start date
            int _lowest = 0 - (_dllvalue * 12);
            DateTime _minDate = DateTime.Now.AddMonths(_lowest);

            if (Page.Session["ordersearchloaded"] != null)
            {
                //******
                //restrict records returned by age
                //get start date from dll value
                //e.g if search is for last 12 months dllvalue 1 start date = current date - (1 * 12) months
                //if search is for 2-3 years dllvalue 3 start date = current date - (3 * 12) months
                //*******
                //get additional params
                //if this user has their own orders default to myorders, else default to all orders
                //int _orders = new ordertablecustomcontroller().GetOrderCountByUserId(_userid, _minDate, _minDate.AddMonths(_months));
                //if (this.dxhfOrder.Contains("orders") && _orders > 0)
                if (this.dxhfOrder.Contains("orders"))
                {
                    //check hidden fields for additional params
                    //filter by current user
                    //if (this.dxhfOrder.Contains("orders"))
                    //{
                    string _usefilter = this.dxhfOrder.Get("orders").ToString();
                    if (_usefilter == "user" && _userid != "")
                    {
                        //only include live jobs
                        //where OrderControllerID or OperationsControllerID or OriginPortControllerID or DestinationPortControllerID = this user
                        //01/04/15 include closed jobs
                        //string[] _fields = { "JobClosed=false AND OrderControllerID", "JobClosed=false AND OperationsControllerID", "JobClosed=false AND OriginPortControllerID", "JobClosed=false AND DestinationPortControllerID" };
                        string[] _fields = { "OrderControllerID", "OperationsControllerID", "OriginPortControllerID", "DestinationPortControllerID" };


                        for (int ix = 0; ix < _fields.Length; ix++)
                        {
                                _params.Add(_fields[ix] + "_OR", _userid.ToString());
                        }

                        this.dxlblSearch.Text = "My orders";
                    }
                    else if (_usefilter == "office" && _officeid != "")
                    {
                        //get office name
                        _officeid = wwi_func.lookup_xml_string("xml\\office_names.xml", "value", _officeid, "name");
                        //only include live jobs
                        //01/04/15 include closed jobs
                        //_params.Add("JobClosed", "false");
                        //make sure to use escape characters and double quotes i.e you cabn't use single quotes or will get a linq binding error
                        //linq seems especially dim-witted about these things
                        _params.Add("OfficeIndicator", "\"" + _officeid + "\"");

                        this.dxlblSearch.Text = "Office orders";
                    }
                    //}
                }
                else if (!string.IsNullOrEmpty(this.dxgridOrders.FilterExpression.ToString()))
                //if (this.dxhfOrder.Contains("filter"))
                {
                    //default to showing all orders but use a sinmple query to show orders with order number > -1 
                    //this will allow the filter expression to be processed along with the query
                    //and avoids defaulting to no records when linq datasource is bound
                    //01/04/15 include closed jobs
                    //_params.Add("JobClosed=false AND OrderNumber_AND_>", "-1");  //split string array into 3 items in params below
                    _params.Add("OrderNumber_AND_>", "-1");  //split string array into 3 items in params below
                    
                    this.dxlblSearch.Text = "All orders";
                    if (this.dxhfOrder.Contains("orders")) { this.dxhfOrder.Remove("orders"); }
                }
                //******

                if (_params.Count > 0)
                {
                    foreach (Parameter p in _params)
                    {
                        string _pname = p.Name.ToString();
                        string _op = "AND";
                        string _compare = "==";
                        string[] _check = _pname.Split("_".ToCharArray());
                        _pname = _check[0].ToString();
                        if (_check.Length > 1 && _check[1].ToString().Trim() != "") { _op = _check[1].ToString(); }
                        if (_check.Length > 2 && _check[2].ToString().Trim() != "") { _compare = _check[2].ToString(); }

                        string _a = _f != "" ? " " + _op + " " : "";
                        _f += _a + "(" + _pname + _compare + p.DefaultValue.ToString() + ")";

                    }

                    if (_query != "") { _query = "(" + _f + ") AND " + _query; } else { _query = _f; }
                }
            }

            //need this!
            e.KeyExpression = "OrderIx";

            //make sure you have: using System.Linq; using System.Linq.Dynamic; using System.Linq.Expressions; or you will get the error
            //The type arguments for method 'System.Linq.Enumerable.Where<TSource>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,int,bool>)' cannot be inferred from the usage.
            if (!string.IsNullOrEmpty(_query))
            {
                
                //var _nquery = new linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months);
                var _nquery = new linq.linq_user_ordersDataContext().user_orders(_minDate, _months).Where(_query); //c => c.CompanyID == 7
                e.QueryableSource = _nquery;
            }
            else
            {
                //default to show no records
                var _nquery = new linq.linq_user_ordersDataContext().user_orders(_minDate, _months).Where(o => o.OrderNumber == -1);
                e.QueryableSource = _nquery;
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.Visible = true;
        }
    }
    #endregion

    #region web methods (must include using System.Web.Services;)
    [WebMethod]
    public static string get_secure_url(IList<string> keyids, string commandmethod)
    {
        //page to redirect build as required
        string _page = "";

        //orderids = OrderID, doc filter, OrderNumber passed as string seperated by semi-colon
        //string[] _ids = keyids.Split(",".ToCharArray());

        if (keyids.Count >= 2)
        {
            //get order number off grid
            //string[] _fields = { "OrderNumber", "OrderID" };
            //pass order no, id as encryped data
            string _orderid = wwi_security.EncryptString(keyids[0], "publiship");
            string _orderno = wwi_security.EncryptString(keyids[1], "publiship");
            //these 2 are needed for document management
            string _docs = keyids.Count >= 4 ? wwi_security.EncryptString(keyids[2], "publiship") : "";
            string _houseblno = keyids.Count >= 4 ? wwi_security.EncryptString(keyids[3], "publiship") : "";

            //track requesting page so we can return to it
            //can't use 'this' in a static method
            //string _req = this.dxhfOrder.Get("req").ToString(); //populated on page load   //wwi_security.EncryptString("Search", "publiship");
            string _req = wwi_security.EncryptString("Search", "publiship");

            //commandid is taken from custom button name
            switch (commandmethod)
            {
                case "cmdView":
                    {
                        //send orderid and orderno as related tables are linked by orderno not orderid
                        _page = string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&req={2}&pno={3}", "ReadOnly", _orderid, _req, _orderno);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page);  //Response.Redirect(_page); 
                        break;
                    }
                case "cmdEdit":
                    {
                        //send orderid and orderno as related tables are linked by orderno not orderid
                        _page = string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&req={2}&pno={3}", "Edit", _orderid, _req, _orderno);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        break;
                    }
                case "cmdViewFilter": //deprecated we no longer use seperate order_view.aspx so just use cmdView
                    {
                        _page = string.Format("../shipment_orders/order_view.aspx?mode={0}&pid={1}&req={2}", "ReadOnly", _orderid, _req);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        break;
                    }
                case "cmdEditFilter": //deprecated
                    {
                        _page = string.Format("../shipment_orders/order_edit.aspx?mode={0}&pid={1}&req={2}", "Edit", _orderid, _req);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        break;
                    }
                case "cmdOrderSheet": //order sheet 
                    {
                        //for testing barcode output httphandler output to web page
                        //_page = string.Format("../popupcontrol/barcode.aspx?code={0}", _orderno);
                        //for testing output to pdf
                        //_page = itextsharp_out.orderno_barcode(_orderno);
                        //_page = null; //do not return a valid page
                        //
                        _page = string.Format("../shipment_orders/order_sheet.aspx?pno={0}", _orderno);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        //**** this code does not work 
                        //string _err = itextsharp_out.order_sheet(wwi_func.vint(_orderno));
                        //if (_err != "") {
                        //    this.dxlblErr.Text = _err;
                        //    this.dxpnlErr.Visible = true; }
                        break;
                    }
                case "cmdTemplate": //make template out of selected order passed as source 'src' set mode to readonly until user saves template
                    {
                        _page = string.Format("../system_templates/order_template.aspx?src={0}&pno={1}&mode={2}", _orderid,"ReadOnly");
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        break;
                    }
                case "cmdClone": //make identical copy of selected order
                    {
                        _page = string.Format("../shipment_orders/clone_order.aspx?pid={0}&pno={1}", _orderid, _orderno);
                        //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                        break;
                    }
                case "cmdFiles":
                    {
                        //file upload publiship users only!
                        string _companyid = HttpContext.Current.Session["user"] != null ? ((UserClass)HttpContext.Current.Session["user"]).CompanyId.ToString() : "0";
                        if (_companyid == "-1")
                        {
                            //return formatted url 
                            _page = string.Format("../Popupcontrol/order_upload_manager.aspx?pod={0}&dfd={1}&hbl={2}", _orderno, _docs, _houseblno);
                        }
                        else
                        {
                            //return something as returning a null value will report an error in onMethodComplete javascript function
                            _page = "denied";
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }//end switch
        }
        return _page;
    }
    //end web method

#endregion

    #region call backs
    /// <summary>
    /// fires when e.g. called from javascript
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridOrders_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "getdata")
        {
            Page.Session["ordersearchloaded"] = true;
        }
        this.dxgridOrders.DataBind();
        
    }

    /// <summary>
    /// this event is not being used, we are using client side asynchronous callbacks and webmethod get_secure_url so we can open page in new window
    /// custom command buttons vierw/edit/delete (maybe?)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridOrders_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView _grid = (ASPxGridView)sender;
        //get order number off grid
        //string[] _fields = { "OrderNumber", "OrderID" };
        //pass order no, id as encryped data
        string _orderno = wwi_security.EncryptString(_grid.GetRowValues(e.VisibleIndex, "OrderNumber").ToString(), "publiship");
        string _orderid = wwi_security.EncryptString(_grid.GetRowValues(e.VisibleIndex, "OrderID").ToString(), "publiship");

        //track requesting page so we can return to it
        string _req = this.dxhfOrder.Get("req").ToString(); //populated on page load   //wwi_security.EncryptString("Search", "publiship");

        //page to redirect build as required
        string _page = "";

        switch (e.ButtonID.ToString())
        {
            case "cmdView": 
                {
                    //send orderid and orderno as related tables are linked by orderno not orderid
                    _page = string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&req={2}&pno={3}", "ReadOnly", _orderid, _req, _orderno);
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page);  //Response.Redirect(_page); 
                    break;
                }
            case "cmdEdit": 
                {
                    //send orderid and orderno as related tables are linked by orderno not orderid
                    _page = string.Format("../shipment_orders/order.aspx?mode={0}&pid={1}&req={2}&pno={3}", "Edit", _orderid, _req, _orderno);
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            case "cmdViewFilter": //deprecated we no longer use seperate order_view.aspx so just use cmdView
                {
                    _page = string.Format("../shipment_orders/order_view.aspx?mode={0}&pid={1}&req={2}", "ReadOnly", _orderid, _req);
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            case "cmdEditFilter": //deprecated
                {
                    _page = string.Format("../shipment_orders/order_edit.aspx?mode={0}&pid={1}&req={2}", "Edit", _orderid, _req);
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            case "cmdOrderSheet": //order sheet 
                {
                    _page = string.Format("../shipment_orders/order_sheet.aspx?pno={0}", _orderno);
                    //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    //**** this code does not work 
                    //string _err = itextsharp_out.advance_labels(120600005);
                    //_page = string.Format("~/Order_View.aspx?pno={0}", _orderno);
                    //DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page);  //Response.Redirect(_page); 
                    //****
                    //string _err = itextsharp_out.order_sheet(wwi_func.vint(_orderno));
                    //if (_err != "") {
                    //    this.dxlblErr.Text = _err;
                    //    this.dxpnlErr.Visible = true; }
                    break;
                }
            case "cmdTemplate": //make template out of selected order
                {
                    _page = string.Format("../system_templates/order_template.aspx?pid={0}&mode={1}", _orderid, "Insert");
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_page); //Response.Redirect(_page); 
                    break;
                }
            case "cmdClone": //make identical copy of selected order
                {
                    _page = string.Format("../shipment_orders/clone_order.aspx?pid={0}&pno={1}",_orderid , _orderno);
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
    
    #region functions
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion

    #region order sheet output 

    protected void order_sheet(string orderno)
    {
        string _err = "";
               
        //output to pdf
        if (orderno != null)
        {
           

            _err = itextsharp_out.order_sheet(wwi_func.vint(orderno));
            if (_err != "")
            {
                this.dxlblErr.Text = _err;
                this.dxpnlErr.Visible = true;

            }//end if
        }
    }

    #endregion
    
}
