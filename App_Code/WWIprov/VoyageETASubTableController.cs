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
    /// Controller class for VoyageETASubTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class VoyageETASubTableController
    {
        // Preload our schema..
        VoyageETASubTable thisSchemaLoad = new VoyageETASubTable();
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
        public VoyageETASubTableCollection FetchAll()
        {
            VoyageETASubTableCollection coll = new VoyageETASubTableCollection();
            Query qry = new Query(VoyageETASubTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageETASubTableCollection FetchByID(object VoyageETASubID)
        {
            VoyageETASubTableCollection coll = new VoyageETASubTableCollection().Where("VoyageETASubID", VoyageETASubID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageETASubTableCollection FetchByQuery(Query qry)
        {
            VoyageETASubTableCollection coll = new VoyageETASubTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object VoyageETASubID)
        {
            return (VoyageETASubTable.Delete(VoyageETASubID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object VoyageETASubID)
        {
            return (VoyageETASubTable.Destroy(VoyageETASubID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? VoyageID,int? DestinationPortID,DateTime? Eta,byte[] Ts)
	    {
		    VoyageETASubTable item = new VoyageETASubTable();
		    
            item.VoyageID = VoyageID;
            
            item.DestinationPortID = DestinationPortID;
            
            item.Eta = Eta;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int VoyageETASubID,int? VoyageID,int? DestinationPortID,DateTime? Eta,byte[] Ts)
	    {
		    VoyageETASubTable item = new VoyageETASubTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.VoyageETASubID = VoyageETASubID;
				
			item.VoyageID = VoyageID;
				
			item.DestinationPortID = DestinationPortID;
				
			item.Eta = Eta;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
