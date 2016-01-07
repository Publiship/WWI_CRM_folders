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
    /// Controller class for AONT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class AontController
    {
        // Preload our schema..
        Aont thisSchemaLoad = new Aont();
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
        public AontCollection FetchAll()
        {
            AontCollection coll = new AontCollection();
            Query qry = new Query(Aont.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AontCollection FetchByID(object OrderNumber)
        {
            AontCollection coll = new AontCollection().Where("OrderNumber", OrderNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public AontCollection FetchByQuery(Query qry)
        {
            AontCollection coll = new AontCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object OrderNumber)
        {
            return (Aont.Delete(OrderNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object OrderNumber)
        {
            return (Aont.Destroy(OrderNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime DateCreated)
	    {
		    Aont item = new Aont();
		    
            item.DateCreated = DateCreated;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int OrderNumber,DateTime DateCreated)
	    {
		    Aont item = new Aont();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.OrderNumber = OrderNumber;
				
			item.DateCreated = DateCreated;
				
	        item.Save(UserName);
	    }
    }
}