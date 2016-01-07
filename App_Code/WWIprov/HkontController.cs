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
    /// Controller class for HKONT
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class HkontController
    {
        // Preload our schema..
        Hkont thisSchemaLoad = new Hkont();
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
        public HkontCollection FetchAll()
        {
            HkontCollection coll = new HkontCollection();
            Query qry = new Query(Hkont.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public HkontCollection FetchByID(object OrderNumber)
        {
            HkontCollection coll = new HkontCollection().Where("OrderNumber", OrderNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public HkontCollection FetchByQuery(Query qry)
        {
            HkontCollection coll = new HkontCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object OrderNumber)
        {
            return (Hkont.Delete(OrderNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object OrderNumber)
        {
            return (Hkont.Destroy(OrderNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? DateCreated,byte[] Ts)
	    {
		    Hkont item = new Hkont();
		    
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
		    Hkont item = new Hkont();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.OrderNumber = OrderNumber;
				
			item.DateCreated = DateCreated;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
