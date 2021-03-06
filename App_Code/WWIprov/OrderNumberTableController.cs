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
    /// Controller class for OrderNumberTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class OrderNumberTableController
    {
        // Preload our schema..
        OrderNumberTable thisSchemaLoad = new OrderNumberTable();
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
        public OrderNumberTableCollection FetchAll()
        {
            OrderNumberTableCollection coll = new OrderNumberTableCollection();
            Query qry = new Query(OrderNumberTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public OrderNumberTableCollection FetchByID(object OrderNumber)
        {
            OrderNumberTableCollection coll = new OrderNumberTableCollection().Where("OrderNumber", OrderNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public OrderNumberTableCollection FetchByQuery(Query qry)
        {
            OrderNumberTableCollection coll = new OrderNumberTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object OrderNumber)
        {
            return (OrderNumberTable.Delete(OrderNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object OrderNumber)
        {
            return (OrderNumberTable.Destroy(OrderNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? DateCreated,string RefX,byte[] Ts)
	    {
		    OrderNumberTable item = new OrderNumberTable();
		    
            item.DateCreated = DateCreated;
            
            item.RefX = RefX;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int OrderNumber,DateTime? DateCreated,string RefX,byte[] Ts)
	    {
		    OrderNumberTable item = new OrderNumberTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.OrderNumber = OrderNumber;
				
			item.DateCreated = DateCreated;
				
			item.RefX = RefX;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
