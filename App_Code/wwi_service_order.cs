using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using SubSonic;
using DAL.Logistics;
/// <summary>

/// <summary>
/// Summary description for Service_Feeds
/// </summary>
namespace DAL.Services
{
    [WebService(Namespace = "http://www.publiship.com/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service_Order : System.Web.Services.WebService
    {
        public struct columnOrder
        {
            public string colName;
            public string colWidth;
            public int visibleIndex;
            public bool colVisible;
        }

        //order structure same as linq view but without ID fields apart from order ID
        //make sure datetimes and numbers are nullable e.g. (DateTime?) or (Nullable<DateTime>)
        public struct orderDetail
        {
            public DateTime? CargoReady;
            public string CompanyName;
            public string ConsigneeName;
            public string ContactName;
            public string ContainerNumber;
            public string CustomersRef;
            public DateTime? DateOrderCreated;
            public string destplace;
            public string destport;
            public DateTime? dtupdated;
            public int? EstPallets;
            public int? EstVolume;
            public int? EstWeight;
            public DateTime? ETA;
            public DateTime? ETS;
            public DateTime? ETW;
            public DateTime? ExWorksDate;
            public string HouseBLNUmber;
            public string Impression;
            public string ISBN;
            public bool JobClosed;
            public string Joined;
            public string Name;
            public int? OrderID;
            public int? OrderNumber;
            public string originplace;
            public string originport;
            public string printername;
            public string Title;
            public float? UnitPricePerCopy;
            public DateTime? WarehouseDate;
        }
        
        public Service_Order()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "This method returns order details as a json string required parameters order number, user name, password", EnableSession = false)]
        public string getorderJson(int orderno, string username, string password)
        {
            //JSON dates are returned as epoch date "\/Date(1315910100000)\/" so will need formatting e.g. DateTime d = new DateTime(1315910100000)
            string _json = "";

            if (!string.IsNullOrEmpty(wwi_security.getuserIds(username, password)))
            {
                //number of months to include after start date
                int _months = 60;
                //going back 5 years should be sufficient
                DateTime _minDate = DateTime.Now.AddMonths(0 - _months);

                //var _rec = from o in new linq_classesDataContext().view_orders.Where(o => o.OrderNumber == orderno)
                var _rec = from o in new linq.linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months).Where(o => o.OrderNumber == orderno)
                select new orderDetail
                           {
                               CargoReady = (DateTime?)o.CargoReady,
                               CompanyName = o.CompanyName,
                               ConsigneeName = o.consignee_name,
                               ContactName = o.ContactName,
                               ContainerNumber = o.ContainerNumber,
                               CustomersRef = o.CustomersRef,
                               DateOrderCreated = (DateTime?)o.DateOrderCreated,
                               destplace = (string)o.destination_place,
                               destport = o.destination_port,
                               dtupdated = (DateTime)o.dtupdated,
                               EstPallets = (int?)o.EstPallets,
                               EstVolume = (int?)o.EstVolume,
                               EstWeight = (int?)o.EstWeight,
                               ETA = (DateTime?)o.ETA,
                               ETS = (DateTime?)o.ETS,
                               ETW = (DateTime?)o.ETW,
                               ExWorksDate = (DateTime)o.ExWorksDate,
                               HouseBLNUmber = o.HouseBLNUmber,
                               Impression = o.Impression,
                               ISBN = o.ISBN,
                               JobClosed = (bool)o.JobClosed,
                               Joined = o.vessel_name,
                               Name = o.Name,
                               OrderID = o.OrderID,
                               OrderNumber = (int?)o.OrderNumber,
                               originplace = o.origin_place,
                               originport = o.origin_port,
                               printername = o.printer_name,
                               Title = o.Title,
                               UnitPricePerCopy = (float?)o.UnitPricePerCopy,
                               WarehouseDate = (DateTime?)o.WarehouseDate
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            return _json;
        }
        //end get order json

        [WebMethod(Description = "This method returns all active orders for a company as a json string required parameters user name, password", EnableSession = false)]
        public string getordersJson(string username, string password)
        {
            //JSON dates are returned as epoch date "\/Date(1315910100000)\/" so will need formatting e.g. DateTime d = new DateTime(1315910100000)
            string _json = "";
            int _companyid = wwi_security.getuserIds(username, password, "company");

            if (_companyid > 0)
            {
                //number of months to include after start date
                int _months = 60;
                //going back 5 years should be sufficient
                DateTime _minDate = DateTime.Now.AddMonths(0 - _months);

                //var _rec = from o in new linq_classesDataContext().view_orders.Where(o => o.CompanyID == _companyid && o.JobClosed == false)
                var _rec = from o in new linq.linq_view_orders_udfDataContext().view_orders_by_age(_minDate, _months).Where(o => o.CompanyID == _companyid && o.JobClosed == false)
                select new orderDetail
                           {
                               CargoReady = (DateTime?)o.CargoReady,
                               CompanyName = o.CompanyName,
                               ConsigneeName = o.consignee_name,
                               ContactName = o.ContactName,
                               ContainerNumber = o.ContainerNumber,
                               CustomersRef = o.CustomersRef,
                               DateOrderCreated = (DateTime?)o.DateOrderCreated,
                               destplace = (string)o.destination_place,
                               destport = o.destination_port,
                               dtupdated = (DateTime)o.dtupdated,
                               EstPallets = (int?)o.EstPallets,
                               EstVolume = (int?)o.EstVolume,
                               EstWeight = (int?)o.EstWeight,
                               ETA = (DateTime?)o.ETA,
                               ETS = (DateTime?)o.ETS,
                               ETW = (DateTime?)o.ETW,
                               ExWorksDate = (DateTime)o.ExWorksDate,
                               HouseBLNUmber = o.HouseBLNUmber,
                               Impression = o.Impression,
                               ISBN = o.ISBN,
                               JobClosed = (bool)o.JobClosed,
                               Joined = o.vessel_name,
                               Name = o.Name,
                               OrderID = o.OrderID,
                               OrderNumber = (int?)o.OrderNumber,
                               originplace = o.origin_place,
                               originport = o.origin_port,
                               printername = o.printer_name,
                               Title = o.Title,
                               UnitPricePerCopy = (float?)o.UnitPricePerCopy,
                               WarehouseDate = (DateTime?)o.WarehouseDate
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            return _json;
        }
        //end get orders json

        [WebMethod(Description = "This method returns order details as XML required parameters order number, user name, password", EnableSession = false)]
        public string getorderXml(int orderno, string username, string password)
        {
            string _xml = "";

            if (!string.IsNullOrEmpty(wwi_security.getuserIds(username, password)))
            {
                string[] _cols = { "view_order.CargoReady", "view_order.CompanyName", "view_order.ConsigneeName", "view_order.ContactName", "view_order.ContainerNumber",
                               "view_order.CustomersRef", "view_order.DateOrderCreated", "view_order.destplace", "view_order.destport", "view_order.dtupdated",
                               "view_order.EstPallets", "view_order.EstVolume", "view_order.EstWeight", "view_order.ETA", "view_order.ETS", "view_order.ETW", 
                               "view_order.ExWorksDate", "view_order.HouseBLNUmber", "view_order.Impression", "view_order.ISBN", "view_order.JobClosed",
                               "view_order.Joined", "view_order.Name", "view_order.OrderID", "view_order.OrderNumber", "view_order.originplace", 
                               "view_order.originport", "view_order.printer_name", "view_order.Title", "view_order.UnitPricePerCopy", "view_order.WarehouseDate"};

                SubSonic.SqlQuery _qry = DB.Select(_cols).From("view_order").Where("OrderNumber").IsEqualTo(orderno);
                System.IO.StringWriter _sw = new System.IO.StringWriter();

                ViewOrderCollection _order = new ViewOrderCollection();
                _order.LoadAndCloseReader(_qry.ExecuteReader());
                DataTable _dt = (DataTable)_order.ToDataTable();
                //parse to xml sformatted string
                _dt.WriteXml(_sw);
                _xml = _sw.ToString();
            }
            return _xml; 
        }
        //end get order xml

        [WebMethod(Description = "This method returns all active orders for a company as XML required parameters user name, password", EnableSession = false)]
        public string getordersXml(string username, string password)
        {
            string _xml = "";

            int _companyid = wwi_security.getuserIds(username, password, "company");

            if (_companyid > 0)
            {
                string[] _cols = { "view_order.CargoReady", "view_order.CompanyName", "view_order.ConsigneeName", "view_order.ContactName", "view_order.ContainerNumber",
                               "view_order.CustomersRef", "view_order.DateOrderCreated", "view_order.destplace", "view_order.destport", "view_order.dtupdated",
                               "view_order.EstPallets", "view_order.EstVolume", "view_order.EstWeight", "view_order.ETA", "view_order.ETS", "view_order.ETW", 
                               "view_order.ExWorksDate", "view_order.HouseBLNUmber", "view_order.Impression", "view_order.ISBN", "view_order.JobClosed",
                               "view_order.Joined", "view_order.Name", "view_order.OrderID", "view_order.OrderNumber", "view_order.originplace", 
                               "view_order.originport", "view_order.printer_name", "view_order.Title", "view_order.UnitPricePerCopy", "view_order.WarehouseDate"};

                SubSonic.SqlQuery _qry = DB.Select(_cols).From("view_order").Where("CompanyID").IsEqualTo(_companyid).And("JobClosed").IsEqualTo(false);
                System.IO.StringWriter _sw = new System.IO.StringWriter();

                ViewOrderCollection _order = new ViewOrderCollection();
                _order.LoadAndCloseReader(_qry.ExecuteReader());
                DataTable _dt = (DataTable)_order.ToDataTable();
                //parse to xml sformatted string
                _dt.WriteXml(_sw);
                _xml = _sw.ToString();
            }
            return _xml;
        }
        //end get orders xml

    }
    //end service_order
}
