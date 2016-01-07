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
    /// Controller class for BRONT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class BrontController
    {
        // Preload our schema..
        Bront thisSchemaLoad = new Bront();
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
        public BrontCollection FetchAll()
        {
            BrontCollection coll = new BrontCollection();
            Query qry = new Query(Bront.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public BrontCollection FetchByID(object OrderNumber)
        {
            BrontCollection coll = new BrontCollection().Where("OrderNumber", OrderNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public BrontCollection FetchByQuery(Query qry)
        {
            BrontCollection coll = new BrontCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object OrderNumber)
        {
            return (Bront.Delete(OrderNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object OrderNumber)
        {
            return (Bront.Destroy(OrderNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? DateCreated,byte[] Ts)
	    {
		    Bront item = new Bront();
		    
            item.DateCreated = DateCreated;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int OrderNumber,DateTime? DateCreated,byte[] Ts)
	    {
		    Bront item = new Bront();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.OrderNumber = OrderNumber;
				
			item.DateCreated = DateCreated;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}