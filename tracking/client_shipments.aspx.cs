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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using ParameterPasser;
using SubSonic;

public partial class tracking_client_shipments : System.Web.UI.Page
{
 
    protected void Page_Init(object sender, EventArgs e)
    {
     
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.Session["user"] != null)
        {
            
        }
        else
        {
            if (!Page.IsCallback) { Response.Redirect("~/tracking/shipment_tracking_unsigned.aspx", true); }
        }

        this.linqAggregates.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqAggregates_Selecting);

    }

    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linqAggregates_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        ParameterCollection _params = new ParameterCollection();
        string _query = "";

        if (Page.Session["shipmentsloaded"] != null)
        {
            //***************
            //21.10.2014 Paul Edwards for delivery tracking and container check which DeliveryID's are visible for this company
            //check individual contact ID iso 
            string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
            IList<string> _deliveryids = null;
            _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
            if (_deliveryids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _deliveries = "(DeliveryAddress==" + string.Join(" OR DeliveryAddress==", _deliveryids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _deliveries); //select for this company off list

            }
            else
            {
                string _conmpanyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
                _params.Add("DeliveryAddress", _conmpanyid);
            }
            //****************

            //now rebuild query with additional parameters AFTER page is loaded
            string _f = "";
            if (_params.Count > 0)
            {
                foreach (Parameter p in _params)
                {
                    string _pname = p.Name.ToString();
                    string _op = "AND";
                    string[] _check = _pname.Split("_".ToCharArray());
                    _pname = _check[0].ToString();
                    _op = _check.Length > 1 ? _check[1].ToString() : _op;

                    string _a = _f != "" ? " " + _op + " " : "";
                    _f += _pname != "NULL" ? _a + "(" + _pname + "==" + p.DefaultValue.ToString() + ")" : _a + "(" + p.DefaultValue.ToString() + ")";

                }

                if (_query != "") { _query = _f + " AND " + _query; } else { _query = _f; }
            }

        }

        //22.10.14 Paul Edwards Session["shipmentsloaded"] allows us to init the grid with no data
        //Session["containersloaded"] is null on 1st page init when we call selecting event for grid
        //When Javascript Grid.Init is called Grid.CustomCallback event is fired which sets Session["containersloaded"] = true and allows filter query to build
        //This gives a slight delay allowing the page to load BEFORE the grid is populated for a better user experience
        
        //****************
        //get starting ETS date for deliveries from xml file (which maqes it easy to change if necessary)
        //convert(datetime,'28/03/2013 10:32',103)
        //if no datetimes seelcted default to 0/01/1900 so no data displayed
        DateTime? _ets1 = this.dxdt1.Value != null ? wwi_func.vdatetime(this.dxdt1.Value.ToString()) : new DateTime(1900, 1, 1);
        DateTime? _ets2 = this.dxdt2.Value != null ? wwi_func.vdatetime(this.dxdt2.Value.ToString()) : new DateTime(1900, 1, 1);
        if (_ets1 > _ets2) { 
            DateTime? _temp = _ets1;
            _ets1 = _ets2;
            _ets2 = _temp;
        }
        //****************

        //important! need a key id or error=key expression is undefined
        e.KeyExpression = "ContainerIdx";
        
        //10.11/2014 if necessary send to datatable then use datatable.compute method on clientname to summarise without showing
        //breakdown by delivery address
        if (!string.IsNullOrEmpty(_query))
        {
            //var _query = new linq_classesDataContext().view_orders.Where(_filter);
            //290413 using new userdefined inline table function so we can parametise with month range from current date
            //var _nquery = new linq_view_orders_udfDataContext().view_orders_by_age(1, 12).Where(_query); //c => c.CompanyID == 7
            var _nquery = new linq.linq_aggregate_containers_udfDataContext().aggregate_containers_by_ets(_ets1, _ets2).Where(_query); //c => c.CompanyID == 7
            e.QueryableSource = _nquery;

            //Int32 _count = _nquery.Count();

            //if (!String.IsNullOrEmpty(_name))
            //{
            //    append_to_query_log(_query, _name);
            //    this.gridContainer.SettingsText.Title = "Search results: " + _name;
            //}
        }
        else //default to display nothing until page is loaded 
        {
            //var _nquery = new linq_classesDataContext().view_order_2s.Where(c => c.OrderNumber == -1);
            var _nquery = new linq.linq_aggregate_containers_udfDataContext().aggregate_containers_by_ets(_ets1, _ets2).Where(c => c.ContainerIdx == 0); //c => c.CompanyID == 7
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }

        //this.dxgridShipments.GroupBy(this.dxgridShipments.Columns["colDeliveryAddress"], 0);  
    }

    #region grid events
    protected void dxgridShipments_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "getdata")
        {
            Page.Session["shipmentsloaded"] = true;
       
        }
        //rebind grid
        this.dxgridShipments.DataBind();
    }
    #endregion
    
}
