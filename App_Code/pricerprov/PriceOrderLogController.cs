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
namespace DAL.Pricer
{
    /// <summary>
    /// Controller class for price_order_log
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PriceOrderLogController
    {
        // Preload our schema..
        PriceOrderLog thisSchemaLoad = new PriceOrderLog();
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
        public PriceOrderLogCollection FetchAll()
        {
            PriceOrderLogCollection coll = new PriceOrderLogCollection();
            Query qry = new Query(PriceOrderLog.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PriceOrderLogCollection FetchByID(object PoLogId)
        {
            PriceOrderLogCollection coll = new PriceOrderLogCollection().Where("po_log_id", PoLogId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PriceOrderLogCollection FetchByQuery(Query qry)
        {
            PriceOrderLogCollection coll = new PriceOrderLogCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PoLogId)
        {
            return (PriceOrderLog.Delete(PoLogId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PoLogId)
        {
            return (PriceOrderLog.Destroy(PoLogId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int QuoteId,int OrderId,int? OrderNo,int? UserId,int? CompanyId,DateTime? LogDate,byte[] LogTs)
	    {
		    PriceOrderLog item = new PriceOrderLog();
		    
            item.QuoteId = QuoteId;
            
            item.OrderId = OrderId;
            
            item.OrderNo = OrderNo;
            
            item.UserId = UserId;
            
            item.CompanyId = CompanyId;
            
            item.LogDate = LogDate;
            
            item.LogTs = LogTs;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PoLogId,int QuoteId,int OrderId,int? OrderNo,int? UserId,int? CompanyId,DateTime? LogDate,byte[] LogTs)
	    {
		    PriceOrderLog item = new PriceOrderLog();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PoLogId = PoLogId;
				
			item.QuoteId = QuoteId;
				
			item.OrderId = OrderId;
				
			item.OrderNo = OrderNo;
				
			item.UserId = UserId;
				
			item.CompanyId = CompanyId;
				
			item.LogDate = LogDate;
				
			item.LogTs = LogTs;
				
	        item.Save(UserName);
	    }
    }
}
