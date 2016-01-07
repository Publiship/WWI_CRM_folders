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
namespace DAL.Logistics
{
    /// <summary>
    /// Controller class for PlacesTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PlacesTableController
    {
        // Preload our schema..
        PlacesTable thisSchemaLoad = new PlacesTable();
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
        public PlacesTableCollection FetchAll()
        {
            PlacesTableCollection coll = new PlacesTableCollection();
            Query qry = new Query(PlacesTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PlacesTableCollection FetchByID(object PlaceID)
        {
            PlacesTableCollection coll = new PlacesTableCollection().Where("PlaceID", PlaceID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PlacesTableCollection FetchByQuery(Query qry)
        {
            PlacesTableCollection coll = new PlacesTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PlaceID)
        {
            return (PlacesTable.Delete(PlaceID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PlaceID)
        {
            return (PlacesTable.Destroy(PlaceID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string PlaceName,int? CountryID,int? DefaultPalletID,byte[] Ts)
	    {
		    PlacesTable item = new PlacesTable();
		    
            item.PlaceName = PlaceName;
            
            item.CountryID = CountryID;
            
            item.DefaultPalletID = DefaultPalletID;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PlaceID,string PlaceName,int? CountryID,int? DefaultPalletID,byte[] Ts)
	    {
		    PlacesTable item = new PlacesTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PlaceID = PlaceID;
				
			item.PlaceName = PlaceName;
				
			item.CountryID = CountryID;
				
			item.DefaultPalletID = DefaultPalletID;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}