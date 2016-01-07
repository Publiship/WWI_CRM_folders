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
    /// Controller class for InternalInvoiceSubTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InternalInvoiceSubTableController
    {
        // Preload our schema..
        InternalInvoiceSubTable thisSchemaLoad = new InternalInvoiceSubTable();
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
        public InternalInvoiceSubTableCollection FetchAll()
        {
            InternalInvoiceSubTableCollection coll = new InternalInvoiceSubTableCollection();
            Query qry = new Query(InternalInvoiceSubTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceSubTableCollection FetchByID(object EntryNumber)
        {
            InternalInvoiceSubTableCollection coll = new InternalInvoiceSubTableCollection().Where("EntryNumber", EntryNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InternalInvoiceSubTableCollection FetchByQuery(Query qry)
        {
            InternalInvoiceSubTableCollection coll = new InternalInvoiceSubTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EntryNumber)
        {
            return (InternalInvoiceSubTable.Delete(EntryNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EntryNumber)
        {
            return (InternalInvoiceSubTable.Destroy(EntryNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? InvoiceNumber,int? ChargeType,string Detail,decimal? Amount,bool Vat,decimal? VATAmount,byte[] Ts)
	    {
		    InternalInvoiceSubTable item = new InternalInvoiceSubTable();
		    
            item.InvoiceNumber = InvoiceNumber;
            
            item.ChargeType = ChargeType;
            
            item.Detail = Detail;
            
            item.Amount = Amount;
            
            item.Vat = Vat;
            
            item.VATAmount = VATAmount;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int EntryNumber,int? InvoiceNumber,int? ChargeType,string Detail,decimal? Amount,bool Vat,decimal? VATAmount,byte[] Ts)
	    {
		    InternalInvoiceSubTable item = new InternalInvoiceSubTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.EntryNumber = EntryNumber;
				
			item.InvoiceNumber = InvoiceNumber;
				
			item.ChargeType = ChargeType;
				
			item.Detail = Detail;
				
			item.Amount = Amount;
				
			item.Vat = Vat;
				
			item.VATAmount = VATAmount;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}