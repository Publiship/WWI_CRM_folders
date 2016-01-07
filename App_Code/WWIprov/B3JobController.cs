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
    /// Controller class for B3Jobs
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class B3JobController
    {
        // Preload our schema..
        B3Job thisSchemaLoad = new B3Job();
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
        public B3JobCollection FetchAll()
        {
            B3JobCollection coll = new B3JobCollection();
            Query qry = new Query(B3Job.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3JobCollection FetchByID(object Id)
        {
            B3JobCollection coll = new B3JobCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public B3JobCollection FetchByQuery(Query qry)
        {
            B3JobCollection coll = new B3JobCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (B3Job.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (B3Job.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? JobID,int? JobIDParent,string FreightSupplierCode,string FreightSupplierName,int? ProcessLogRecordID,string Cancelled,string SupplierPurchaseOrderNo,string FreightPurchaseOrderNo,int? PickUpLocationID,string PickUpLocationDesc,string PickUpCountryISO,string PortOfEntry,string ActualPortOfEntry,string Vessel,DateTime? RAPDate,DateTime? EstimatedDeliveryDate,DateTime? CriticalDate,int? PrinterSupplierID,string PrinterSupplierName,int? ControllerID,string ControllerName,int? CustomerID,string CustomerName,bool? PrinterDelay,bool? Booked,bool? Completed,string BookingInReference,bool? ManualDropShip,string Remarks,int? LoadItemID,bool? AllUnitsShipped,string Title,string Isbn,string BookCode,int? SupplyEdition,decimal? Rvd,int? DealNo,string CustomerOrderRef,string Currency,decimal? ProformaValue,int? B3DeliveryID,int? DeliveryAddressID,string DeliveryAddressDesc,int? Quantity,int? DestinationLocationID,string DestinationLocationDesc,string DestinationCountryISO,string Incoterm,string TransportMode,int? CartonLength,int? CartonWidth,int? CartonHeight,int? CartonWeight,int? UnitsPerCarton,int? NetCartonWeight,int? OrderedQuantity,int? TotalCartons,int? PalletType,int? NumPallets,int? NumFullPalletLabels,int? NumPartPalletLabels,int? CartonPerFullPalletLayer,int? LayersPerFullPallet,int? RemainderCartons,int? CartonPerFullPallet,int? ShipmentID,string Container,int? ShippedQuantity,string LoadWeight,string LoadVolume,string Comment,DateTime? Entered,DateTime? ExWorks,DateTime? Original,DateTime? ExWorksOriginal,DateTime? ExWorksEstimated,DateTime? ExWorksActual,DateTime? DepartedOriginal,DateTime? DepartedEstimated,DateTime? DepartedActual,DateTime? ArrivedPortOriginal,DateTime? ArrivedPortEstimated,DateTime? ArrivedPortActual,DateTime? DeliveredOriginal,DateTime? DeliveredEstimated,DateTime? DeliveredActual,DateTime? InvoicedOriginal,DateTime? InvoicedEstimated,DateTime? InvoicedActual,DateTime? CompletedOriginal,DateTime? CompletedEstimated,DateTime? CompletedActual,string Comment1,string Comment2,string Comment3,string Comment4,string Comment5,string DealNumber,string SuccessIndicator,string Message,string ErrorMsg,DateTime? DateTimeOfCancellation,string CancelConfirm)
	    {
		    B3Job item = new B3Job();
		    
            item.JobID = JobID;
            
            item.JobIDParent = JobIDParent;
            
            item.FreightSupplierCode = FreightSupplierCode;
            
            item.FreightSupplierName = FreightSupplierName;
            
            item.ProcessLogRecordID = ProcessLogRecordID;
            
            item.Cancelled = Cancelled;
            
            item.SupplierPurchaseOrderNo = SupplierPurchaseOrderNo;
            
            item.FreightPurchaseOrderNo = FreightPurchaseOrderNo;
            
            item.PickUpLocationID = PickUpLocationID;
            
            item.PickUpLocationDesc = PickUpLocationDesc;
            
            item.PickUpCountryISO = PickUpCountryISO;
            
            item.PortOfEntry = PortOfEntry;
            
            item.ActualPortOfEntry = ActualPortOfEntry;
            
            item.Vessel = Vessel;
            
            item.RAPDate = RAPDate;
            
            item.EstimatedDeliveryDate = EstimatedDeliveryDate;
            
            item.CriticalDate = CriticalDate;
            
            item.PrinterSupplierID = PrinterSupplierID;
            
            item.PrinterSupplierName = PrinterSupplierName;
            
            item.ControllerID = ControllerID;
            
            item.ControllerName = ControllerName;
            
            item.CustomerID = CustomerID;
            
            item.CustomerName = CustomerName;
            
            item.PrinterDelay = PrinterDelay;
            
            item.Booked = Booked;
            
            item.Completed = Completed;
            
            item.BookingInReference = BookingInReference;
            
            item.ManualDropShip = ManualDropShip;
            
            item.Remarks = Remarks;
            
            item.LoadItemID = LoadItemID;
            
            item.AllUnitsShipped = AllUnitsShipped;
            
            item.Title = Title;
            
            item.Isbn = Isbn;
            
            item.BookCode = BookCode;
            
            item.SupplyEdition = SupplyEdition;
            
            item.Rvd = Rvd;
            
            item.DealNo = DealNo;
            
            item.CustomerOrderRef = CustomerOrderRef;
            
            item.Currency = Currency;
            
            item.ProformaValue = ProformaValue;
            
            item.B3DeliveryID = B3DeliveryID;
            
            item.DeliveryAddressID = DeliveryAddressID;
            
            item.DeliveryAddressDesc = DeliveryAddressDesc;
            
            item.Quantity = Quantity;
            
            item.DestinationLocationID = DestinationLocationID;
            
            item.DestinationLocationDesc = DestinationLocationDesc;
            
            item.DestinationCountryISO = DestinationCountryISO;
            
            item.Incoterm = Incoterm;
            
            item.TransportMode = TransportMode;
            
            item.CartonLength = CartonLength;
            
            item.CartonWidth = CartonWidth;
            
            item.CartonHeight = CartonHeight;
            
            item.CartonWeight = CartonWeight;
            
            item.UnitsPerCarton = UnitsPerCarton;
            
            item.NetCartonWeight = NetCartonWeight;
            
            item.OrderedQuantity = OrderedQuantity;
            
            item.TotalCartons = TotalCartons;
            
            item.PalletType = PalletType;
            
            item.NumPallets = NumPallets;
            
            item.NumFullPalletLabels = NumFullPalletLabels;
            
            item.NumPartPalletLabels = NumPartPalletLabels;
            
            item.CartonPerFullPalletLayer = CartonPerFullPalletLayer;
            
            item.LayersPerFullPallet = LayersPerFullPallet;
            
            item.RemainderCartons = RemainderCartons;
            
            item.CartonPerFullPallet = CartonPerFullPallet;
            
            item.ShipmentID = ShipmentID;
            
            item.Container = Container;
            
            item.ShippedQuantity = ShippedQuantity;
            
            item.LoadWeight = LoadWeight;
            
            item.LoadVolume = LoadVolume;
            
            item.Comment = Comment;
            
            item.Entered = Entered;
            
            item.ExWorks = ExWorks;
            
            item.Original = Original;
            
            item.ExWorksOriginal = ExWorksOriginal;
            
            item.ExWorksEstimated = ExWorksEstimated;
            
            item.ExWorksActual = ExWorksActual;
            
            item.DepartedOriginal = DepartedOriginal;
            
            item.DepartedEstimated = DepartedEstimated;
            
            item.DepartedActual = DepartedActual;
            
            item.ArrivedPortOriginal = ArrivedPortOriginal;
            
            item.ArrivedPortEstimated = ArrivedPortEstimated;
            
            item.ArrivedPortActual = ArrivedPortActual;
            
            item.DeliveredOriginal = DeliveredOriginal;
            
            item.DeliveredEstimated = DeliveredEstimated;
            
            item.DeliveredActual = DeliveredActual;
            
            item.InvoicedOriginal = InvoicedOriginal;
            
            item.InvoicedEstimated = InvoicedEstimated;
            
            item.InvoicedActual = InvoicedActual;
            
            item.CompletedOriginal = CompletedOriginal;
            
            item.CompletedEstimated = CompletedEstimated;
            
            item.CompletedActual = CompletedActual;
            
            item.Comment1 = Comment1;
            
            item.Comment2 = Comment2;
            
            item.Comment3 = Comment3;
            
            item.Comment4 = Comment4;
            
            item.Comment5 = Comment5;
            
            item.DealNumber = DealNumber;
            
            item.SuccessIndicator = SuccessIndicator;
            
            item.Message = Message;
            
            item.ErrorMsg = ErrorMsg;
            
            item.DateTimeOfCancellation = DateTimeOfCancellation;
            
            item.CancelConfirm = CancelConfirm;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? JobID,int? JobIDParent,string FreightSupplierCode,string FreightSupplierName,int? ProcessLogRecordID,string Cancelled,string SupplierPurchaseOrderNo,string FreightPurchaseOrderNo,int? PickUpLocationID,string PickUpLocationDesc,string PickUpCountryISO,string PortOfEntry,string ActualPortOfEntry,string Vessel,DateTime? RAPDate,DateTime? EstimatedDeliveryDate,DateTime? CriticalDate,int? PrinterSupplierID,string PrinterSupplierName,int? ControllerID,string ControllerName,int? CustomerID,string CustomerName,bool? PrinterDelay,bool? Booked,bool? Completed,string BookingInReference,bool? ManualDropShip,string Remarks,int? LoadItemID,bool? AllUnitsShipped,string Title,string Isbn,string BookCode,int? SupplyEdition,decimal? Rvd,int? DealNo,string CustomerOrderRef,string Currency,decimal? ProformaValue,int? B3DeliveryID,int? DeliveryAddressID,string DeliveryAddressDesc,int? Quantity,int? DestinationLocationID,string DestinationLocationDesc,string DestinationCountryISO,string Incoterm,string TransportMode,int? CartonLength,int? CartonWidth,int? CartonHeight,int? CartonWeight,int? UnitsPerCarton,int? NetCartonWeight,int? OrderedQuantity,int? TotalCartons,int? PalletType,int? NumPallets,int? NumFullPalletLabels,int? NumPartPalletLabels,int? CartonPerFullPalletLayer,int? LayersPerFullPallet,int? RemainderCartons,int? CartonPerFullPallet,int? ShipmentID,string Container,int? ShippedQuantity,string LoadWeight,string LoadVolume,string Comment,DateTime? Entered,DateTime? ExWorks,DateTime? Original,DateTime? ExWorksOriginal,DateTime? ExWorksEstimated,DateTime? ExWorksActual,DateTime? DepartedOriginal,DateTime? DepartedEstimated,DateTime? DepartedActual,DateTime? ArrivedPortOriginal,DateTime? ArrivedPortEstimated,DateTime? ArrivedPortActual,DateTime? DeliveredOriginal,DateTime? DeliveredEstimated,DateTime? DeliveredActual,DateTime? InvoicedOriginal,DateTime? InvoicedEstimated,DateTime? InvoicedActual,DateTime? CompletedOriginal,DateTime? CompletedEstimated,DateTime? CompletedActual,string Comment1,string Comment2,string Comment3,string Comment4,string Comment5,string DealNumber,string SuccessIndicator,string Message,string ErrorMsg,DateTime? DateTimeOfCancellation,string CancelConfirm)
	    {
		    B3Job item = new B3Job();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.JobID = JobID;
				
			item.JobIDParent = JobIDParent;
				
			item.FreightSupplierCode = FreightSupplierCode;
				
			item.FreightSupplierName = FreightSupplierName;
				
			item.ProcessLogRecordID = ProcessLogRecordID;
				
			item.Cancelled = Cancelled;
				
			item.SupplierPurchaseOrderNo = SupplierPurchaseOrderNo;
				
			item.FreightPurchaseOrderNo = FreightPurchaseOrderNo;
				
			item.PickUpLocationID = PickUpLocationID;
				
			item.PickUpLocationDesc = PickUpLocationDesc;
				
			item.PickUpCountryISO = PickUpCountryISO;
				
			item.PortOfEntry = PortOfEntry;
				
			item.ActualPortOfEntry = ActualPortOfEntry;
				
			item.Vessel = Vessel;
				
			item.RAPDate = RAPDate;
				
			item.EstimatedDeliveryDate = EstimatedDeliveryDate;
				
			item.CriticalDate = CriticalDate;
				
			item.PrinterSupplierID = PrinterSupplierID;
				
			item.PrinterSupplierName = PrinterSupplierName;
				
			item.ControllerID = ControllerID;
				
			item.ControllerName = ControllerName;
				
			item.CustomerID = CustomerID;
				
			item.CustomerName = CustomerName;
				
			item.PrinterDelay = PrinterDelay;
				
			item.Booked = Booked;
				
			item.Completed = Completed;
				
			item.BookingInReference = BookingInReference;
				
			item.ManualDropShip = ManualDropShip;
				
			item.Remarks = Remarks;
				
			item.LoadItemID = LoadItemID;
				
			item.AllUnitsShipped = AllUnitsShipped;
				
			item.Title = Title;
				
			item.Isbn = Isbn;
				
			item.BookCode = BookCode;
				
			item.SupplyEdition = SupplyEdition;
				
			item.Rvd = Rvd;
				
			item.DealNo = DealNo;
				
			item.CustomerOrderRef = CustomerOrderRef;
				
			item.Currency = Currency;
				
			item.ProformaValue = ProformaValue;
				
			item.B3DeliveryID = B3DeliveryID;
				
			item.DeliveryAddressID = DeliveryAddressID;
				
			item.DeliveryAddressDesc = DeliveryAddressDesc;
				
			item.Quantity = Quantity;
				
			item.DestinationLocationID = DestinationLocationID;
				
			item.DestinationLocationDesc = DestinationLocationDesc;
				
			item.DestinationCountryISO = DestinationCountryISO;
				
			item.Incoterm = Incoterm;
				
			item.TransportMode = TransportMode;
				
			item.CartonLength = CartonLength;
				
			item.CartonWidth = CartonWidth;
				
			item.CartonHeight = CartonHeight;
				
			item.CartonWeight = CartonWeight;
				
			item.UnitsPerCarton = UnitsPerCarton;
				
			item.NetCartonWeight = NetCartonWeight;
				
			item.OrderedQuantity = OrderedQuantity;
				
			item.TotalCartons = TotalCartons;
				
			item.PalletType = PalletType;
				
			item.NumPallets = NumPallets;
				
			item.NumFullPalletLabels = NumFullPalletLabels;
				
			item.NumPartPalletLabels = NumPartPalletLabels;
				
			item.CartonPerFullPalletLayer = CartonPerFullPalletLayer;
				
			item.LayersPerFullPallet = LayersPerFullPallet;
				
			item.RemainderCartons = RemainderCartons;
				
			item.CartonPerFullPallet = CartonPerFullPallet;
				
			item.ShipmentID = ShipmentID;
				
			item.Container = Container;
				
			item.ShippedQuantity = ShippedQuantity;
				
			item.LoadWeight = LoadWeight;
				
			item.LoadVolume = LoadVolume;
				
			item.Comment = Comment;
				
			item.Entered = Entered;
				
			item.ExWorks = ExWorks;
				
			item.Original = Original;
				
			item.ExWorksOriginal = ExWorksOriginal;
				
			item.ExWorksEstimated = ExWorksEstimated;
				
			item.ExWorksActual = ExWorksActual;
				
			item.DepartedOriginal = DepartedOriginal;
				
			item.DepartedEstimated = DepartedEstimated;
				
			item.DepartedActual = DepartedActual;
				
			item.ArrivedPortOriginal = ArrivedPortOriginal;
				
			item.ArrivedPortEstimated = ArrivedPortEstimated;
				
			item.ArrivedPortActual = ArrivedPortActual;
				
			item.DeliveredOriginal = DeliveredOriginal;
				
			item.DeliveredEstimated = DeliveredEstimated;
				
			item.DeliveredActual = DeliveredActual;
				
			item.InvoicedOriginal = InvoicedOriginal;
				
			item.InvoicedEstimated = InvoicedEstimated;
				
			item.InvoicedActual = InvoicedActual;
				
			item.CompletedOriginal = CompletedOriginal;
				
			item.CompletedEstimated = CompletedEstimated;
				
			item.CompletedActual = CompletedActual;
				
			item.Comment1 = Comment1;
				
			item.Comment2 = Comment2;
				
			item.Comment3 = Comment3;
				
			item.Comment4 = Comment4;
				
			item.Comment5 = Comment5;
				
			item.DealNumber = DealNumber;
				
			item.SuccessIndicator = SuccessIndicator;
				
			item.Message = Message;
				
			item.ErrorMsg = ErrorMsg;
				
			item.DateTimeOfCancellation = DateTimeOfCancellation;
				
			item.CancelConfirm = CancelConfirm;
				
	        item.Save(UserName);
	    }
    }
}
