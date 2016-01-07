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
    /// Controller class for B3LookupContactID
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class B3LookupContactIDController
    {
        // Preload our schema..
        B3LookupContactID thisSchemaLoad = new B3LookupContactID();
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
        public B3LookupContactIDCollection FetchAll()
        {
            B3LookupContactIDCollection coll = new B3LookupContactIDCollection();
            Query qry = new Query(B3LookupContactID.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3LookupContactIDCollection FetchByID(object B3ControllerID)
        {
            B3LookupContactIDCollection coll = new B3LookupContactIDCollection().Where("B3ControllerID", B3ControllerID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3LookupContactIDCollection FetchByQuery(Query qry)
        {
            B3LookupContactIDCollection coll = new B3LookupContactIDCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object B3ControllerID)
        {
            return (B3LookupContactID.Delete(B3ControllerID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object B3ControllerID)
        {
            return (B3LookupContactID.Destroy(B3ControllerID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int B3ControllerID,int ContactID)
	    {
		    B3LookupContactID item = new B3LookupContactID();
		    
            item.B3ControllerID = B3ControllerID;
            
            item.ContactID = ContactID;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int B3ControllerID,int ContactID)
	    {
		    B3LookupContactID item = new B3LookupContactID();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.B3ControllerID = B3ControllerID;
				
			item.ContactID = ContactID;
				
	        item.Save(UserName);
	    }
    }
}
