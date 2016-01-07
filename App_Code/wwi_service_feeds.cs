using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using SubSonic;
using DAL.Pricer;
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
    public class Service_Feeds : System.Web.Services.WebService
    {

        public struct keyvalues
        {
            public int itemId;
            public string itemDescription;
        }

        public Service_Feeds()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "This method returns a json scripted list of countries upon request it does not require parameters", EnableSession = false)]
        public string getOrigin()
        {

            string _json = "";

            var _rec = from o in new linq.linq_pricer_feedsDataContext().pricer_origin_points.Where(o => o.origin_point_ID > 0)
                       select new keyvalues { itemId = o.origin_point_ID, itemDescription = o.origin_point };

            JavaScriptSerializer _js = new JavaScriptSerializer();
            _json += _js.Serialize(_rec);
            return _json;
        }
       
        //end get origin country

        [WebMethod(Description = "This method returns a json scripted list of destinations based on the origin name", EnableSession = false)]
        public string getDestination(string Origin)
        {

            string _json = "";
            var _linq = new linq.linq_pricer_feedsDataContext();

            var _rec = from d in _linq.pricer_dest_countries
                       join o in _linq.pricer_origin_points
                           on d.origin_point_ID equals o.origin_point_ID
                       where o.origin_point == Origin
                       orderby d.country_name
                       select new keyvalues { itemId = d.dest_country_ID, itemDescription = d.country_name };


            JavaScriptSerializer _js = new JavaScriptSerializer();
            _json += _js.Serialize(_rec);
            return _json;
        }
        //end get destination country

        [WebMethod(Description = "This method returns a json scripted list of destinations based on the origin name and destination name", EnableSession = false)]
        public string getFinal(string Origin, string CountryName)
        {

            string _json = "";
            var _linq = new linq.linq_pricer_feedsDataContext();

            var _rec = from f in _linq.pricer_dest_finals
                       join d in _linq.pricer_dest_countries on f.dest_country_ID equals d.country_id
                       join o in _linq.pricer_origin_points on d.origin_point_ID equals o.origin_point_ID
                       where d.country_name == CountryName
                       where o.origin_point == Origin
                       orderby f.dest_final
                       select new keyvalues { itemId = f.dest_final_ID, itemDescription = f.dest_final };


            JavaScriptSerializer _js = new JavaScriptSerializer();
            _json += _js.Serialize(_rec);
            return _json;
        }
        //end get final destination

    }
}
