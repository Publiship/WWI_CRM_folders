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
    /// Controller class for InvoiceSubTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class InvoiceSubTableController
    {
        // Preload our schema..
        InvoiceSubTable thisSchemaLoad = new InvoiceSubTable();
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
        public InvoiceSubTableCollection FetchAll()
        {
            InvoiceSubTableCollection coll = new InvoiceSubTableCollection();
            Query qry = new Query(InvoiceSubTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public InvoiceSubTableCollection FetchByID(object EntryNumber)
        {
            InvoiceSubTableCollection coll = new InvoiceSubTableCollection().Where("EntryNumber", EntryNumber).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public InvoiceSubTableCollection FetchByQuery(Query qry)
        {
            InvoiceSubTableCollection coll = new InvoiceSubTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EntryNumber)
        {
            return (InvoiceSubTable.Delete(EntryNumber) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EntryNumber)
        {
            return (InvoiceSubTable.Destroy(EntryNumber) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? InvoiceNumber,int? ChargeType,string Detail,decimal? Amount,bool Vat,double? VATAmount,byte[] Ts)
	    {
		    InvoiceSubTable item = new InvoiceSubTable();
		    
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
	    public void Update(int EntryNumber,int? InvoiceNumber,int? ChargeType,string Detail,decimal? Amount,bool Vat,double? VATAmount,byte[] Ts)
	    {
		    InvoiceSubTable item = new InvoiceSubTable();
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
