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
    /// Controller class for PublishipAdvanceOrderTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PublishipAdvanceOrderTableController
    {
        // Preload our schema..
        PublishipAdvanceOrderTable thisSchemaLoad = new PublishipAdvanceOrderTable();
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
        public PublishipAdvanceOrderTableCollection FetchAll()
        {
            PublishipAdvanceOrderTableCollection coll = new PublishipAdvanceOrderTableCollection();
            Query qry = new Query(PublishipAdvanceOrderTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PublishipAdvanceOrderTableCollection FetchByID(object OrderID)
        {
            PublishipAdvanceOrderTableCollection coll = new PublishipAdvanceOrderTableCollection().Where("OrderID", OrderID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PublishipAdvanceOrderTableCollection FetchByQuery(Query qry)
        {
            PublishipAdvanceOrderTableCollection coll = new PublishipAdvanceOrderTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object OrderID)
        {
            return (PublishipAdvanceOrderTable.Delete(OrderID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object OrderID)
        {
            return (PublishipAdvanceOrderTable.Destroy(OrderID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string OrderNumber,DateTime? DateOrderReceived,string Payee,string DeliveryAddress,string DestinationCountry,int? CompanyID,int? ConsigneeID,int? PrinterID,string CustomerRef,string PrintersRef,int? ContactID,DateTime? CargoReadyDate,DateTime? CargoReceivedDate,int? OriginID,int? DestID,int? FinalDestID,int? DeliveryAddressID,int? AttentionOfID,int? FlightID,DateTime? Etd,DateTime? Eta,string HAWBno,bool? HAWBAdded,bool? ShippedonBoard,int? Titles,int? Cartons,decimal? ActualWeight,decimal? ActualVolume,string RemarkstoAgent,string RemarkstoCust,bool? JobClosed,string JobClosureDate,string CompositeInvRef,decimal? InsuranceValue,DateTime? CancelRequestRcd,int? CancelRequestByID,bool? OrderCancelled,DateTime? CancelDate,int? CancelledByID,string Fao,byte[] Ts)
	    {
		    PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();
		    
            item.OrderNumber = OrderNumber;
            
            item.DateOrderReceived = DateOrderReceived;
            
            item.Payee = Payee;
            
            item.DeliveryAddress = DeliveryAddress;
            
            item.DestinationCountry = DestinationCountry;
            
            item.CompanyID = CompanyID;
            
            item.ConsigneeID = ConsigneeID;
            
            item.PrinterID = PrinterID;
            
            item.CustomerRef = CustomerRef;
            
            item.PrintersRef = PrintersRef;
            
            item.ContactID = ContactID;
            
            item.CargoReadyDate = CargoReadyDate;
            
            item.CargoReceivedDate = CargoReceivedDate;
            
            item.OriginID = OriginID;
            
            item.DestID = DestID;
            
            item.FinalDestID = FinalDestID;
            
            item.DeliveryAddressID = DeliveryAddressID;
            
            item.AttentionOfID = AttentionOfID;
            
            item.FlightID = FlightID;
            
            item.Etd = Etd;
            
            item.Eta = Eta;
            
            item.HAWBno = HAWBno;
            
            item.HAWBAdded = HAWBAdded;
            
            item.ShippedonBoard = ShippedonBoard;
            
            item.Titles = Titles;
            
            item.Cartons = Cartons;
            
            item.ActualWeight = ActualWeight;
            
            item.ActualVolume = ActualVolume;
            
            item.RemarkstoAgent = RemarkstoAgent;
            
            item.RemarkstoCust = RemarkstoCust;
            
            item.JobClosed = JobClosed;
            
            item.JobClosureDate = JobClosureDate;
            
            item.CompositeInvRef = CompositeInvRef;
            
            item.InsuranceValue = InsuranceValue;
            
            item.CancelRequestRcd = CancelRequestRcd;
            
            item.CancelRequestByID = CancelRequestByID;
            
            item.OrderCancelled = OrderCancelled;
            
            item.CancelDate = CancelDate;
            
            item.CancelledByID = CancelledByID;
            
            item.Fao = Fao;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int OrderID,string OrderNumber,DateTime? DateOrderReceived,string Payee,string DeliveryAddress,string DestinationCountry,int? CompanyID,int? ConsigneeID,int? PrinterID,string CustomerRef,string PrintersRef,int? ContactID,DateTime? CargoReadyDate,DateTime? CargoReceivedDate,int? OriginID,int? DestID,int? FinalDestID,int? DeliveryAddressID,int? AttentionOfID,int? FlightID,DateTime? Etd,DateTime? Eta,string HAWBno,bool? HAWBAdded,bool? ShippedonBoard,int? Titles,int? Cartons,decimal? ActualWeight,decimal? ActualVolume,string RemarkstoAgent,string RemarkstoCust,bool? JobClosed,string JobClosureDate,string CompositeInvRef,decimal? InsuranceValue,DateTime? CancelRequestRcd,int? CancelRequestByID,bool? OrderCancelled,DateTime? CancelDate,int? CancelledByID,string Fao,byte[] Ts)
	    {
		    PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.OrderID = OrderID;
				
			item.OrderNumber = OrderNumber;
				
			item.DateOrderReceived = DateOrderReceived;
				
			item.Payee = Payee;
				
			item.DeliveryAddress = DeliveryAddress;
				
			item.DestinationCountry = DestinationCountry;
				
			item.CompanyID = CompanyID;
				
			item.ConsigneeID = ConsigneeID;
				
			item.PrinterID = PrinterID;
				
			item.CustomerRef = CustomerRef;
				
			item.PrintersRef = PrintersRef;
				
			item.ContactID = ContactID;
				
			item.CargoReadyDate = CargoReadyDate;
				
			item.CargoReceivedDate = CargoReceivedDate;
				
			item.OriginID = OriginID;
				
			item.DestID = DestID;
				
			item.FinalDestID = FinalDestID;
				
			item.DeliveryAddressID = DeliveryAddressID;
				
			item.AttentionOfID = AttentionOfID;
				
			item.FlightID = FlightID;
				
			item.Etd = Etd;
				
			item.Eta = Eta;
				
			item.HAWBno = HAWBno;
				
			item.HAWBAdded = HAWBAdded;
				
			item.ShippedonBoard = ShippedonBoard;
				
			item.Titles = Titles;
				
			item.Cartons = Cartons;
				
			item.ActualWeight = ActualWeight;
				
			item.ActualVolume = ActualVolume;
				
			item.RemarkstoAgent = RemarkstoAgent;
				
			item.RemarkstoCust = RemarkstoCust;
				
			item.JobClosed = JobClosed;
				
			item.JobClosureDate = JobClosureDate;
				
			item.CompositeInvRef = CompositeInvRef;
				
			item.InsuranceValue = InsuranceValue;
				
			item.CancelRequestRcd = CancelRequestRcd;
				
			item.CancelRequestByID = CancelRequestByID;
				
			item.OrderCancelled = OrderCancelled;
				
			item.CancelDate = CancelDate;
				
			item.CancelledByID = CancelledByID;
				
			item.Fao = Fao;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
