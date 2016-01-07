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
    /// Controller class for VoyageETSSubTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class VoyageETSSubTableController
    {
        // Preload our schema..
        VoyageETSSubTable thisSchemaLoad = new VoyageETSSubTable();
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
        public VoyageETSSubTableCollection FetchAll()
        {
            VoyageETSSubTableCollection coll = new VoyageETSSubTableCollection();
            Query qry = new Query(VoyageETSSubTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageETSSubTableCollection FetchByID(object VoyageETSSubID)
        {
            VoyageETSSubTableCollection coll = new VoyageETSSubTableCollection().Where("VoyageETSSubID", VoyageETSSubID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public VoyageETSSubTableCollection FetchByQuery(Query qry)
        {
            VoyageETSSubTableCollection coll = new VoyageETSSubTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object VoyageETSSubID)
        {
            return (VoyageETSSubTable.Delete(VoyageETSSubID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object VoyageETSSubID)
        {
            return (VoyageETSSubTable.Destroy(VoyageETSSubID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? VoyageID,int? OriginPortID,DateTime? Ets,byte[] Ts)
	    {
		    VoyageETSSubTable item = new VoyageETSSubTable();
		    
            item.VoyageID = VoyageID;
            
            item.OriginPortID = OriginPortID;
            
            item.Ets = Ets;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int VoyageETSSubID,int? VoyageID,int? OriginPortID,DateTime? Ets,byte[] Ts)
	    {
		    VoyageETSSubTable item = new VoyageETSSubTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.VoyageETSSubID = VoyageETSSubID;
				
			item.VoyageID = VoyageID;
				
			item.OriginPortID = OriginPortID;
				
			item.Ets = Ets;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}