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
    /// Controller class for VoyageTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class VoyageTableController
    {
        // Preload our schema..
        VoyageTable thisSchemaLoad = new VoyageTable();
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
        public VoyageTableCollection FetchAll()
        {
            VoyageTableCollection coll = new VoyageTableCollection();
            Query qry = new Query(VoyageTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageTableCollection FetchByID(object VoyageID)
        {
            VoyageTableCollection coll = new VoyageTableCollection().Where("VoyageID", VoyageID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageTableCollection FetchByQuery(Query qry)
        {
            VoyageTableCollection coll = new VoyageTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object VoyageID)
        {
            return (VoyageTable.Delete(VoyageID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object VoyageID)
        {
            return (VoyageTable.Destroy(VoyageID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string VoyageNumber,int? VesselID,string Joined,int? AddedBy,DateTime? DateAdded,byte[] Ts)
	    {
		    VoyageTable item = new VoyageTable();
		    
            item.VoyageNumber = VoyageNumber;
            
            item.VesselID = VesselID;
            
            item.Joined = Joined;
            
            item.AddedBy = AddedBy;
            
            item.DateAdded = DateAdded;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int VoyageID,string VoyageNumber,int? VesselID,string Joined,int? AddedBy,DateTime? DateAdded,byte[] Ts)
	    {
		    VoyageTable item = new VoyageTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.VoyageID = VoyageID;
				
			item.VoyageNumber = VoyageNumber;
				
			item.VesselID = VesselID;
				
			item.Joined = Joined;
				
			item.AddedBy = AddedBy;
				
			item.DateAdded = DateAdded;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}