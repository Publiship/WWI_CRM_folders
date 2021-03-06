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
    /// Controller class for InternalPurchaseTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InternalPurchaseTableController
    {
        // Preload our schema..
        InternalPurchaseTable thisSchemaLoad = new InternalPurchaseTable();
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
        public InternalPurchaseTableCollection FetchAll()
        {
            InternalPurchaseTableCollection coll = new InternalPurchaseTableCollection();
            Query qry = new Query(InternalPurchaseTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalPurchaseTableCollection FetchByID(object Id)
        {
            InternalPurchaseTableCollection coll = new InternalPurchaseTableCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalPurchaseTableCollection FetchByQuery(Query qry)
        {
            InternalPurchaseTableCollection coll = new InternalPurchaseTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (InternalPurchaseTable.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (InternalPurchaseTable.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? InvoiceNumber,int? SupplierID,float? EstimatedAmount,DateTime? EstimationDate,float? Amount,DateTime? DatePassed,string PurchaseInvNumber,string Remarks,float? ValueForProfit)
	    {
		    InternalPurchaseTable item = new InternalPurchaseTable();
		    
            item.InvoiceNumber = InvoiceNumber;
            
            item.SupplierID = SupplierID;
            
            item.EstimatedAmount = EstimatedAmount;
            
            item.EstimationDate = EstimationDate;
            
            item.Amount = Amount;
            
            item.DatePassed = DatePassed;
            
            item.PurchaseInvNumber = PurchaseInvNumber;
            
            item.Remarks = Remarks;
            
            item.ValueForProfit = ValueForProfit;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? InvoiceNumber,int? SupplierID,float? EstimatedAmount,DateTime? EstimationDate,float? Amount,DateTime? DatePassed,string PurchaseInvNumber,string Remarks,float? ValueForProfit)
	    {
		    InternalPurchaseTable item = new InternalPurchaseTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.InvoiceNumber = InvoiceNumber;
				
			item.SupplierID = SupplierID;
				
			item.EstimatedAmount = EstimatedAmount;
				
			item.EstimationDate = EstimationDate;
				
			item.Amount = Amount;
				
			item.DatePassed = DatePassed;
				
			item.PurchaseInvNumber = PurchaseInvNumber;
				
			item.Remarks = Remarks;
				
			item.ValueForProfit = ValueForProfit;
				
	        item.Save(UserName);
	    }
    }
}
