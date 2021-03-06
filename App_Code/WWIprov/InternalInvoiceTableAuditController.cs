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
    /// Controller class for InternalInvoiceTableAudit
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InternalInvoiceTableAuditController
    {
        // Preload our schema..
        InternalInvoiceTableAudit thisSchemaLoad = new InternalInvoiceTableAudit();
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
        public InternalInvoiceTableAuditCollection FetchAll()
        {
            InternalInvoiceTableAuditCollection coll = new InternalInvoiceTableAuditCollection();
            Query qry = new Query(InternalInvoiceTableAudit.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceTableAuditCollection FetchByID(object InvoiceAuditID)
        {
            InternalInvoiceTableAuditCollection coll = new InternalInvoiceTableAuditCollection().Where("InvoiceAuditID", InvoiceAuditID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceTableAuditCollection FetchByQuery(Query qry)
        {
            InternalInvoiceTableAuditCollection coll = new InternalInvoiceTableAuditCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InvoiceAuditID)
        {
            return (InternalInvoiceTableAudit.Delete(InvoiceAuditID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InvoiceAuditID)
        {
            return (InternalInvoiceTableAudit.Destroy(InvoiceAuditID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int InvoiceAuditID,int? InvoiceNumber,int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,int? InvoiceCurrencyID,string Description,decimal? ExchangeRate,string Notes,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,decimal? InsuranceValue,string Contact,decimal? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? AddedToTable,int? AddedBy,byte[] Ts)
	    {
		    InternalInvoiceTableAudit item = new InternalInvoiceTableAudit();
		    
            item.InvoiceAuditID = InvoiceAuditID;
            
            item.InvoiceNumber = InvoiceNumber;
            
            item.OrderNumber = OrderNumber;
            
            item.DepartmentID = DepartmentID;
            
            item.InvoiceDate = InvoiceDate;
            
            item.Customer = Customer;
            
            item.InvoiceCurrencyID = InvoiceCurrencyID;
            
            item.Description = Description;
            
            item.ExchangeRate = ExchangeRate;
            
            item.Notes = Notes;
            
            item.PaymentDueDate = PaymentDueDate;
            
            item.InvoiceRaisedBy = InvoiceRaisedBy;
            
            item.InvoiceRaised = InvoiceRaised;
            
            item.InsuranceValue = InsuranceValue;
            
            item.Contact = Contact;
            
            item.OriginalProfitMargin = OriginalProfitMargin;
            
            item.OriginalProfitDate = OriginalProfitDate;
            
            item.AddedToTable = AddedToTable;
            
            item.AddedBy = AddedBy;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int InvoiceAuditID,int? InvoiceNumber,int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,int? InvoiceCurrencyID,string Description,decimal? ExchangeRate,string Notes,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,decimal? InsuranceValue,string Contact,decimal? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? AddedToTable,int? AddedBy,byte[] Ts)
	    {
		    InternalInvoiceTableAudit item = new InternalInvoiceTableAudit();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.InvoiceAuditID = InvoiceAuditID;
				
			item.InvoiceNumber = InvoiceNumber;
				
			item.OrderNumber = OrderNumber;
				
			item.DepartmentID = DepartmentID;
				
			item.InvoiceDate = InvoiceDate;
				
			item.Customer = Customer;
				
			item.InvoiceCurrencyID = InvoiceCurrencyID;
				
			item.Description = Description;
				
			item.ExchangeRate = ExchangeRate;
				
			item.Notes = Notes;
				
			item.PaymentDueDate = PaymentDueDate;
				
			item.InvoiceRaisedBy = InvoiceRaisedBy;
				
			item.InvoiceRaised = InvoiceRaised;
				
			item.InsuranceValue = InsuranceValue;
				
			item.Contact = Contact;
				
			item.OriginalProfitMargin = OriginalProfitMargin;
				
			item.OriginalProfitDate = OriginalProfitDate;
				
			item.AddedToTable = AddedToTable;
				
			item.AddedBy = AddedBy;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
