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
    /// Controller class for VesselTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class VesselTableController
    {
        // Preload our schema..
        VesselTable thisSchemaLoad = new VesselTable();
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
        public VesselTableCollection FetchAll()
        {
            VesselTableCollection coll = new VesselTableCollection();
            Query qry = new Query(VesselTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public VesselTableCollection FetchByID(object VesselID)
        {
            VesselTableCollection coll = new VesselTableCollection().Where("VesselID", VesselID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public VesselTableCollection FetchByQuery(Query qry)
        {
            VesselTableCollection coll = new VesselTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object VesselID)
        {
            return (VesselTable.Delete(VesselID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object VesselID)
        {
            return (VesselTable.Destroy(VesselID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string VesselName,byte[] Ts)
	    {
		    VesselTable item = new VesselTable();
		    
            item.VesselName = VesselName;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int VesselID,string VesselName,byte[] Ts)
	    {
		    VesselTable item = new VesselTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.VesselID = VesselID;
				
			item.VesselName = VesselName;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
