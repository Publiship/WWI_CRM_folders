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
    /// Controller class for InvoiceTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InvoiceTableController
    {
        // Preload our schema..
        InvoiceTable thisSchemaLoad = new InvoiceTable();
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
        public InvoiceTableCollection FetchAll()
        {
            InvoiceTableCollection coll = new InvoiceTableCollection();
            Query qry = new Query(InvoiceTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InvoiceTableCollection FetchByID(object InvoiceNumber)
        {
            InvoiceTableCollection coll = new InvoiceTableCollection().Where("InvoiceNumber", InvoiceNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InvoiceTableCollection FetchByQuery(Query qry)
        {
            InvoiceTableCollection coll = new InvoiceTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InvoiceNumber)
        {
            return (InvoiceTable.Delete(InvoiceNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InvoiceNumber)
        {
            return (InvoiceTable.Destroy(InvoiceNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,float? InvoiceCurrencyID,string Description,decimal? ExchangeRate,string Notes,bool? Crosstrade,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,decimal? InsuranceValue,string Contact,decimal? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? DateQueryRaised,string DetailsOfQuery,string Resolution,int? ConfirmedCompleted,DateTime? CompletionDate,int? Controller,string LossApproved,DateTime? LossApprovedDate,byte[] Tscol)
	    {
		    InvoiceTable item = new InvoiceTable();
		    
            item.OrderNumber = OrderNumber;
            
            item.DepartmentID = DepartmentID;
            
            item.InvoiceDate = InvoiceDate;
            
            item.Customer = Customer;
            
            item.InvoiceCurrencyID = InvoiceCurrencyID;
            
            item.Description = Description;
            
            item.ExchangeRate = ExchangeRate;
            
            item.Notes = Notes;
            
            item.Crosstrade = Crosstrade;
            
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
            
            item.LossApproved = LossApproved;
            
            item.LossApprovedDate = LossApprovedDate;
            
            item.Tscol = Tscol;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int InvoiceNumber,int? OrderNumber,int? DepartmentID,DateTime? InvoiceDate,int? Customer,float? InvoiceCurrencyID,string Description,decimal? ExchangeRate,string Notes,bool? Crosstrade,DateTime? PaymentDueDate,int? InvoiceRaisedBy,DateTime? InvoiceRaised,decimal? InsuranceValue,string Contact,decimal? OriginalProfitMargin,DateTime? OriginalProfitDate,DateTime? DateQueryRaised,string DetailsOfQuery,string Resolution,int? ConfirmedCompleted,DateTime? CompletionDate,int? Controller,string LossApproved,DateTime? LossApprovedDate,byte[] Tscol)
	    {
		    InvoiceTable item = new InvoiceTable();
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
				
			item.Crosstrade = Crosstrade;
				
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
				
			item.LossApproved = LossApproved;
				
			item.LossApprovedDate = LossApprovedDate;
				
			item.Tscol = Tscol;
				
	        item.Save(UserName);
	    }
    }
}
