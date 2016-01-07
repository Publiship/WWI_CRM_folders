using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace DAL.Pricer
{
    /// <summary>
    /// Controller class for pricer_dest_country
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PricerDestCountryController
    {
        // Preload our schema..
        PricerDestCountry thisSchemaLoad = new PricerDestCountry();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public PricerDestCountryCollection FetchAll()
        {
            PricerDestCountryCollection coll = new PricerDestCountryCollection();
            Query qry = new Query(PricerDestCountry.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PricerDestCountryCollection FetchByID(object DestCountryId)
        {
            PricerDestCountryCollection coll = new PricerDestCountryCollection().Where("dest_country_ID", DestCountryId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PricerDestCountryCollection FetchByQuery(Query qry)
        {
            PricerDestCountryCollection coll = new PricerDestCountryCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object DestCountryId)
        {
            return (PricerDestCountry.Delete(DestCountryId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object DestCountryId)
        {
            return (PricerDestCountry.Destroy(DestCountryId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int OriginPointId,string CountryName,int CountryId)
	    {
		    PricerDestCountry item = new PricerDestCountry();
		    
            item.OriginPointId = OriginPointId;
            
            item.CountryName = CountryName;
            
            item.CountryId = CountryId;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int DestCountryId,int OriginPointId,string CountryName,int CountryId)
	    {
		    PricerDestCountry item = new PricerDestCountry();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.DestCountryId = DestCountryId;
				
			item.OriginPointId = OriginPointId;
				
			item.CountryName = CountryName;
				
			item.CountryId = CountryId;
				
	        item.Save(UserName);
	    }
    }
}