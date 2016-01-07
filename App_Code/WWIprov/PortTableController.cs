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
    /// Controller class for PortTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PortTableController
    {
        // Preload our schema..
        PortTable thisSchemaLoad = new PortTable();
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
        public PortTableCollection FetchAll()
        {
            PortTableCollection coll = new PortTableCollection();
            Query qry = new Query(PortTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PortTableCollection FetchByID(object PortID)
        {
            PortTableCollection coll = new PortTableCollection().Where("PortID", PortID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PortTableCollection FetchByQuery(Query qry)
        {
            PortTableCollection coll = new PortTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PortID)
        {
            return (PortTable.Delete(PortID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PortID)
        {
            return (PortTable.Destroy(PortID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string PortName,int? CountryID,byte[] Ts)
	    {
		    PortTable item = new PortTable();
		    
            item.PortName = PortName;
            
            item.CountryID = CountryID;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PortID,string PortName,int? CountryID,byte[] Ts)
	    {
		    PortTable item = new PortTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PortID = PortID;
				
			item.PortName = PortName;
				
			item.CountryID = CountryID;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}