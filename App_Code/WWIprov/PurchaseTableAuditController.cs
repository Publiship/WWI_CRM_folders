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
    /// Controller class for PurchaseTableAudit
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PurchaseTableAuditController
    {
        // Preload our schema..
        PurchaseTableAudit thisSchemaLoad = new PurchaseTableAudit();
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
        public PurchaseTableAuditCollection FetchAll()
        {
            PurchaseTableAuditCollection coll = new PurchaseTableAuditCollection();
            Query qry = new Query(PurchaseTableAudit.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PurchaseTableAuditCollection FetchByID(object PurchaseAuditIDa)
        {
            PurchaseTableAuditCollection coll = new PurchaseTableAuditCollection().Where("PurchaseAuditIDa", PurchaseAuditIDa).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PurchaseTableAuditCollection FetchByQuery(Query qry)
        {
            PurchaseTableAuditCollection coll = new PurchaseTableAuditCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PurchaseAuditIDa)
        {
            return (PurchaseTableAudit.Delete(PurchaseAuditIDa) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PurchaseAuditIDa)
        {
            return (PurchaseTableAudit.Destroy(PurchaseAuditIDa) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? PuchaseTableID,int? InvoiceNumber,int? SupplierID,decimal? EstimatedAmount,DateTime? EstimationDate,string OldValue,decimal? Amount,DateTime? DatePassed,string PurchaseInvNumber,string Remarks,decimal? ValueForProfit,DateTime? Added,int? AddedBy,byte[] Ts)
	    {
		    PurchaseTableAudit item = new PurchaseTableAudit();
		    
            item.PuchaseTableID = PuchaseTableID;
            
            item.InvoiceNumber = InvoiceNumber;
            
            item.SupplierID = SupplierID;
            
            item.EstimatedAmount = EstimatedAmount;
            
            item.EstimationDate = EstimationDate;
            
            item.OldValue = OldValue;
            
            item.Amount = Amount;
            
            item.DatePassed = DatePassed;
            
            item.PurchaseInvNumber = PurchaseInvNumber;
            
            item.Remarks = Remarks;
            
            item.ValueForProfit = ValueForProfit;
            
            item.Added = Added;
            
            item.AddedBy = AddedBy;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PurchaseAuditIDa,int? PuchaseTableID,int? InvoiceNumber,int? SupplierID,decimal? EstimatedAmount,DateTime? EstimationDate,string OldValue,decimal? Amount,DateTime? DatePassed,string PurchaseInvNumber,string Remarks,decimal? ValueForProfit,DateTime? Added,int? AddedBy,byte[] Ts)
	    {
		    PurchaseTableAudit item = new PurchaseTableAudit();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PurchaseAuditIDa = PurchaseAuditIDa;
				
			item.PuchaseTableID = PuchaseTableID;
				
			item.InvoiceNumber = InvoiceNumber;
				
			item.SupplierID = SupplierID;
				
			item.EstimatedAmount = EstimatedAmount;
				
			item.EstimationDate = EstimationDate;
				
			item.OldValue = OldValue;
				
			item.Amount = Amount;
				
			item.DatePassed = DatePassed;
				
			item.PurchaseInvNumber = PurchaseInvNumber;
				
			item.Remarks = Remarks;
				
			item.ValueForProfit = ValueForProfit;
				
			item.Added = Added;
				
			item.AddedBy = AddedBy;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
