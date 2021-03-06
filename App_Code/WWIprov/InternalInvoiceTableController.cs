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
    /// Controller class for InternalInvoiceTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InternalInvoiceTableController
    {
        // Preload our schema..
        InternalInvoiceTable thisSchemaLoad = new InternalInvoiceTable();
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
        public InternalInvoiceTableCollection FetchAll()
        {
            InternalInvoiceTableCollection coll = new InternalInvoiceTableCollection();
            Query qry = new Query(InternalInvoiceTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceTableCollection FetchByID(object InvoiceNumber)
        {
            InternalInvoiceTableCollection coll = new InternalInvoiceTableCollection().Where("InvoiceNumber", InvoiceNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceTableCollection FetchByQuery(Query qry)
        {
            InternalInvoiceTableCollection coll = new InternalInvoiceTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InvoiceNumber)
        {
            return (InternalInvoiceTable.Delete(InvoiceNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InvoiceNumber)
        {
            return (InternalInvoiceTable.Destroy(InvoiceNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,int? InvoiceCurrencyID,string Description,float? ExchangeRate,string Notes,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,float? InsuranceValue,string Contact,float? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? DateQueryRaised,string DetailsOfQuery,string Resolution,bool? ConfirmedCompleted,DateTime? CompletionDate,int? Controller,byte[] Ts)
	    {
		    InternalInvoiceTable item = new InternalInvoiceTable();
		    
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
            
            item.DateQueryRaised = DateQueryRaised;
            
            item.DetailsOfQuery = DetailsOfQuery;
            
            item.Resolution = Resolution;
            
            item.ConfirmedCompleted = ConfirmedCompleted;
            
            item.CompletionDate = CompletionDate;
            
            item.Controller = Controller;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int InvoiceNumber,int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,int? InvoiceCurrencyID,string Description,float? ExchangeRate,string Notes,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,float? InsuranceValue,string Contact,float? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? DateQueryRaised,string DetailsOfQuery,string Resolution,bool? ConfirmedCompleted,DateTime? CompletionDate,int? Controller,byte[] Ts)
	    {
		    InternalInvoiceTable item = new InternalInvoiceTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
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
				
			item.DateQueryRaised = DateQueryRaised;
				
			item.DetailsOfQuery = DetailsOfQuery;
				
			item.Resolution = Resolution;
				
			item.ConfirmedCompleted = ConfirmedCompleted;
				
			item.CompletionDate = CompletionDate;
				
			item.Controller = Controller;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
