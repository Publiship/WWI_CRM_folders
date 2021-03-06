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
    /// Controller class for B3LookupConsigneeID
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class B3LookupConsigneeIDController
    {
        // Preload our schema..
        B3LookupConsigneeID thisSchemaLoad = new B3LookupConsigneeID();
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
        public B3LookupConsigneeIDCollection FetchAll()
        {
            B3LookupConsigneeIDCollection coll = new B3LookupConsigneeIDCollection();
            Query qry = new Query(B3LookupConsigneeID.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3LookupConsigneeIDCollection FetchByID(object B3CustomerID)
        {
            B3LookupConsigneeIDCollection coll = new B3LookupConsigneeIDCollection().Where("B3CustomerID", B3CustomerID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3LookupConsigneeIDCollection FetchByQuery(Query qry)
        {
            B3LookupConsigneeIDCollection coll = new B3LookupConsigneeIDCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object B3CustomerID)
        {
            return (B3LookupConsigneeID.Delete(B3CustomerID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object B3CustomerID)
        {
            return (B3LookupConsigneeID.Destroy(B3CustomerID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int B3CustomerID,int ConsigneeID)
	    {
		    B3LookupConsigneeID item = new B3LookupConsigneeID();
		    
            item.B3CustomerID = B3CustomerID;
            
            item.ConsigneeID = ConsigneeID;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int B3CustomerID,int ConsigneeID)
	    {
		    B3LookupConsigneeID item = new B3LookupConsigneeID();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.B3CustomerID = B3CustomerID;
				
			item.ConsigneeID = ConsigneeID;
				
	        item.Save(UserName);
	    }
    }
}
