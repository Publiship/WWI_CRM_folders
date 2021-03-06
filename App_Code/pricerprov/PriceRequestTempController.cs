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
    /// Controller class for price_request_temp
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PriceRequestTempController
    {
        // Preload our schema..
        PriceRequestTemp thisSchemaLoad = new PriceRequestTemp();
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
        public PriceRequestTempCollection FetchAll()
        {
            PriceRequestTempCollection coll = new PriceRequestTempCollection();
            Query qry = new Query(PriceRequestTemp.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PriceRequestTempCollection FetchByID(object RequestId)
        {
            PriceRequestTempCollection coll = new PriceRequestTempCollection().Where("request_Id", RequestId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PriceRequestTempCollection FetchByQuery(Query qry)
        {
            PriceRequestTempCollection coll = new PriceRequestTempCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object RequestId)
        {
            return (PriceRequestTemp.Delete(RequestId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object RequestId)
        {
            return (PriceRequestTemp.Destroy(RequestId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? RequestDate,int? RequestUserId,int? RequestCompanyId,string RequestIp,string BookTitle,int InDimensions,string InCurrency,string InPallet,double? InLength,double? InWidth,double? InDepth,double? InWeight,double? InExtent,double? InPapergsm,bool? InHardback,int? CopiesCarton,string OriginName,string CountryName,string FinalName,int? TotCopies,byte[] PrTimestamp)
	    {
		    PriceRequestTemp item = new PriceRequestTemp();
		    
            item.RequestDate = RequestDate;
            
            item.RequestUserId = RequestUserId;
            
            item.RequestCompanyId = RequestCompanyId;
            
            item.RequestIp = RequestIp;
            
            item.BookTitle = BookTitle;
            
            item.InDimensions = InDimensions;
            
            item.InCurrency = InCurrency;
            
            item.InPallet = InPallet;
            
            item.InLength = InLength;
            
            item.InWidth = InWidth;
            
            item.InDepth = InDepth;
            
            item.InWeight = InWeight;
            
            item.InExtent = InExtent;
            
            item.InPapergsm = InPapergsm;
            
            item.InHardback = InHardback;
            
            item.CopiesCarton = CopiesCarton;
            
            item.OriginName = OriginName;
            
            item.CountryName = CountryName;
            
            item.FinalName = FinalName;
            
            item.TotCopies = TotCopies;
            
            item.PrTimestamp = PrTimestamp;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int RequestId,DateTime? RequestDate,int? RequestUserId,int? RequestCompanyId,string RequestIp,string BookTitle,int InDimensions,string InCurrency,string InPallet,double? InLength,double? InWidth,double? InDepth,double? InWeight,double? InExtent,double? InPapergsm,bool? InHardback,int? CopiesCarton,string OriginName,string CountryName,string FinalName,int? TotCopies,byte[] PrTimestamp)
	    {
		    PriceRequestTemp item = new PriceRequestTemp();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.RequestId = RequestId;
				
			item.RequestDate = RequestDate;
				
			item.RequestUserId = RequestUserId;
				
			item.RequestCompanyId = RequestCompanyId;
				
			item.RequestIp = RequestIp;
				
			item.BookTitle = BookTitle;
				
			item.InDimensions = InDimensions;
				
			item.InCurrency = InCurrency;
				
			item.InPallet = InPallet;
				
			item.InLength = InLength;
				
			item.InWidth = InWidth;
				
			item.InDepth = InDepth;
				
			item.InWeight = InWeight;
				
			item.InExtent = InExtent;
				
			item.InPapergsm = InPapergsm;
				
			item.InHardback = InHardback;
				
			item.CopiesCarton = CopiesCarton;
				
			item.OriginName = OriginName;
				
			item.CountryName = CountryName;
				
			item.FinalName = FinalName;
				
			item.TotCopies = TotCopies;
				
			item.PrTimestamp = PrTimestamp;
				
	        item.Save(UserName);
	    }
    }
}
