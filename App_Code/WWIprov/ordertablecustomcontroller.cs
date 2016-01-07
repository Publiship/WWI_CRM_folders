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


namespace DAL.Logistics
{
    /// <summary>
    /// ordertablecustomcontroller
    /// custom functions for ordertable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ordertablecustomcontroller
    {
        // Preload our schema..
        DbFilterField thisSchemaLoad = new DbFilterField();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
                if (userName.Length == 0)
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        userName = System.Web.HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                    }
                }
                return userName;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public OrderTableCollection PodFetchByID(object OrderID)
        {
            OrderTableCollection coll = new OrderTableCollection().Where("OrderID", OrderID).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public OrderTableCollection PodFetchByNo(object OrderNumber)
        {
            OrderTableCollection coll = new OrderTableCollection().Where("OrderNumber", OrderNumber).Load();
            return coll;
        }

        //return total orders with ETA of <date>
        //required date and companyid
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public double GetOrderCountByEta(Int32 companyid, DateTime etadate)
        {
            double _ordercount = 0;

            if (companyid != -1)//restrict to specific comnpany
            {
                _ordercount = new Select(Aggregate.Count(OrderTable.OrderIDColumn, "SumOrderID")).From(OrderTable.Schema).
                Where(OrderTable.CompanyIDColumn).IsEqualTo(companyid).And(OrderTable.EtaColumn).IsEqualTo(etadate.ToShortDateString()).ExecuteScalar<double>();
            }
            else
            {
                _ordercount = new Select(Aggregate.Count(OrderTable.OrderIDColumn, "SumOrderID")).From(OrderTable.Schema).
                    Where(OrderTable.EtaColumn).IsEqualTo(etadate.ToShortDateString()).ExecuteScalar<double>();
                
            }
            return _ordercount;
        }

        //return total orders with ETA of <date>
        //required companyid
        //returns a datareader that can e.g. be loaded to a datatable
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IDataReader GetOrderCountByEtaMonth(Int32 companyid)
        {
            DateTime today = DateTime.Today;
            DateTime _firstday = new DateTime(today.Year, today.Month, 1);
            DateTime _lastday = _firstday.AddMonths(1).AddDays(-1);

            Aggregate _agr1 = Aggregate.GroupBy(OrderTable.EtaColumn);
            Aggregate _agr2 = Aggregate.Count(OrderTable.OrderIDColumn, "SumOrderID");
            Aggregate[] _agrs = {_agr1, _agr2};
            SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();

            if (companyid != -1) //restrict to specified company
            {
                _qry = new Select(_agrs).From(OrderTable.Schema).
                Where(OrderTable.CompanyIDColumn).IsEqualTo(companyid).And(OrderTable.EtaColumn).IsBetweenAnd(_firstday, _lastday);
            }
            else
            {
                 _qry = new Select(_agrs).From(OrderTable.Schema).
                Where(OrderTable.EtaColumn).IsBetweenAnd(_firstday, _lastday);
            }

            IDataReader _rd = _qry.ExecuteReader();

            return _rd;
        }

        //return total orders for the week based on <date>
        //required companyid
        //returns a datareader that can e.g. be loaded to a datatable
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IDataReader GetOrderCountByEtaWeek(Int32 companyid)
        {
            System.Globalization.CultureInfo _ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.DayOfWeek _fd = _ci.DateTimeFormat.FirstDayOfWeek;
            
            DateTime _firstday = DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - _fd));
            DateTime _lastday = _firstday.AddDays(4); //monday to friday + 4 

            Aggregate _agr1 = Aggregate.GroupBy(OrderTable.EtaColumn);
            Aggregate _agr2 = Aggregate.Count(OrderTable.OrderIDColumn, "SumOrderID");
            Aggregate[] _agrs = { _agr1, _agr2 };
            SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();

            if (companyid != -1) //restrict to specified company
            {
                _qry = new Select(_agrs).From(OrderTable.Schema).
                Where(OrderTable.CompanyIDColumn).IsEqualTo(companyid).And(OrderTable.EtaColumn).IsBetweenAnd(_firstday, _lastday);
            }
            else
            {
                _qry = new Select(_agrs).From(OrderTable.Schema).
               Where(OrderTable.EtaColumn).IsBetweenAnd(_firstday, _lastday);
            }

            IDataReader _rd = _qry.ExecuteReader();

            return _rd;
        }

        //return count of total orders for an order controller where UserId is
        //OrderControllerID or OperationsControllerID or OriginPortControllerID or DestinationPortControllerID
        //and within date range
        //required UserId, mindate (starting date), maxdate (ending date)
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public int GetOrderCountByUserId(Int32 userid, DateTime mindate, DateTime maxdate)
        {
            int _ordercount = 0;

            if (userid > 0)
            {
                _ordercount = new Select(Aggregate.Count(OrderTable.OrderIDColumn, "SumOrderID")).From(OrderTable.Schema).
                Where(OrderTable.DateOrderCreatedColumn).IsBetweenAnd(mindate, maxdate).And(OrderTable.OrderControllerIDColumn).IsEqualTo(userid).And(OrderTable.JobClosedColumn).IsEqualTo(false).
                Or(OrderTable.DateOrderCreatedColumn).IsBetweenAnd(mindate, maxdate).And(OrderTable.OrderControllerIDColumn).IsEqualTo(userid).And(OrderTable.JobClosedColumn).IsEqualTo(false).
                Or(OrderTable.DateOrderCreatedColumn).IsBetweenAnd(mindate, maxdate).And(OrderTable.OriginPortControllerIDColumn).IsEqualTo(userid).And(OrderTable.JobClosedColumn).IsEqualTo(false).
                Or(OrderTable.DateOrderCreatedColumn).IsBetweenAnd(mindate, maxdate).And(OrderTable.DestinationPortControllerIDColumn).IsEqualTo(userid).And(OrderTable.JobClosedColumn).IsEqualTo(false). 
                ExecuteScalar<int>();
            }
            
            return _ordercount;
        }

        //return list of order numbers associated with house B/L
        //required string house B/L
        //returns ilist
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IList<Int32> get_orders_by_houseBL(string housebl)
        {
            IList<Int32> _orders = null;

            try
            {
               

                string[] _cols = { "OrderTable.OrderNumber" };
                SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();

                _qry = new Select(_cols).From(OrderTable.Schema).
                        Where(OrderTable.HouseBLNUmberColumn).IsEqualTo(housebl);

                _orders = _qry.ExecuteTypedList<Int32>();

            
            }
            catch (Exception ex)
            {
                string _error = ex.Message.ToString();    
            }

            return _orders;
        }
        //end

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool PodDelete(object OrderID)
        {
            return (OrderTable.Delete(OrderID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool PodDestroy(object OrderID)
        {
            return (OrderTable.Destroy(OrderID) == 1);
        }

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void PodInsert(int? OrderNumber, string OfficeIndicator, bool PublishipOrder, DateTime? DateOrderCreated, bool HotJob, int? CompanyID, int? ConsigneeID, int? NotifyPartyID, int? AgentAtOriginID, int? AgentAtDestinationID, int? PrinterID, int? ClearingAgentID, int? OnCarriageID, int? OrderControllerID, int? OperationsControllerID, int? OriginPortControllerID, int? DestinationPortControllerID, string CustomersRef, int? ContactID, DateTime? ExWorksDate, DateTime? EWDLastUpdated, DateTime? CargoReady, DateTime? WarehouseDate, bool? OnTime, DateTime? BookingReceived, int? OriginPointID, int? PortID, int? DestinationPortID, int? FinalDestinationID, int? CountryID, string OldVesselName, int? DestinationCountryID, int? VesselID, DateTime? VesselLastUpdated, DateTime? Ets, DateTime? Eta, string PearsonDivCode, string PearsonSSRRef, string HouseBLNUmber, bool HouseBLAdded, bool ShippedOnBoard, float? EstCopies, DateTime? CopiesLastUpdated, int? EstCartons, int? EstPallets, int? EstWeight, float? EstVolume, int? Palletise, int? PackageTypeID, int? NumberOfPackages, int? ActualCartons, int? ActualPallets, int? Jackets, int? ActualWeight, DateTime? WeightLastUpdated, float? ActualVolume, DateTime? VolumeLastUpdated, bool? Fcllcl, int? Est20, int? Est40, int? EstLCLWt, float? EstLCLVol, int? No20, int? No40, int? LCLWt, float? LCLVol, string Remarks, string RemarksToCustomer, int? QuoteRef, string Sellingrate, string SellingrateAgent, bool DocsRcdAndApproved, DateTime? DocsApprovedDate, DateTime? JobClosureDate, bool JobClosed, bool ExpressBL, bool FumigationCert, bool GSPCert, bool COfO, bool PackingDeclaration, string OtherDocsRequired, string Incoterms, float? PricePerCopy, string Customs, string Currency, int? InvoiceAddresseeID, int? ConsolNumber, float? UnitPricePerCopy, bool OnHold, string ContainerInfo, DateTime? Cleared, float? HodderPricePerCopy, bool FileCoverPrintedOrigin, bool FileCoverPrintedDest, string ClientsTerms, string OriginTrucking, string OrignTHC, string OriginDocs, string Freight, string DestTHC, string DestPalletisation, string CustomsClearance, string DeliveryCharges, string CoLoaderComments, string Pdcid, string HCCompositeRef, decimal? HCInvoiceAmount, string Impression, decimal? InsuranceValue, double? InsuranceValues, int? InvoiceNumber, DateTime? InvoiceDate, DateTime? CancelRequestRcd, bool? OrderCancelled, DateTime? CancelDate, int? CancelledBy, int? InvoiceTo, decimal? HCInvoiceAmount2, bool? OrderAckSent, int? CargoUpdateId, int? QuoteId, int? DocumentFolder, byte[] Ts)
        {
            OrderTable item = new OrderTable();

            item.OrderNumber = OrderNumber;

            item.OfficeIndicator = OfficeIndicator;

            item.PublishipOrder = PublishipOrder;

            item.DateOrderCreated = DateOrderCreated;

            item.HotJob = HotJob;

            item.CompanyID = CompanyID;

            item.ConsigneeID = ConsigneeID;

            item.NotifyPartyID = NotifyPartyID;

            item.AgentAtOriginID = AgentAtOriginID;

            item.AgentAtDestinationID = AgentAtDestinationID;

            item.PrinterID = PrinterID;

            item.ClearingAgentID = ClearingAgentID;

            item.OnCarriageID = OnCarriageID;

            item.OrderControllerID = OrderControllerID;

            item.OperationsControllerID = OperationsControllerID;

            item.OriginPortControllerID = OriginPortControllerID;

            item.DestinationPortControllerID = DestinationPortControllerID;

            item.CustomersRef = CustomersRef;

            item.ContactID = ContactID;

            item.ExWorksDate = ExWorksDate;

            item.EWDLastUpdated = EWDLastUpdated;

            item.CargoReady = CargoReady;

            item.WarehouseDate = WarehouseDate;

            item.OnTime = OnTime;

            item.BookingReceived = BookingReceived;

            item.OriginPointID = OriginPointID;

            item.PortID = PortID;

            item.DestinationPortID = DestinationPortID;

            item.FinalDestinationID = FinalDestinationID;

            item.CountryID = CountryID;

            item.OldVesselName = OldVesselName;

            item.DestinationCountryID = DestinationCountryID;

            item.VesselID = VesselID;

            item.VesselLastUpdated = VesselLastUpdated;

            item.Ets = Ets;

            item.Eta = Eta;

            item.PearsonDivCode = PearsonDivCode;

            item.PearsonSSRRef = PearsonSSRRef;

            item.HouseBLNUmber = HouseBLNUmber;

            item.HouseBLAdded = HouseBLAdded;

            item.ShippedOnBoard = ShippedOnBoard;

            item.EstCopies = EstCopies;

            item.CopiesLastUpdated = CopiesLastUpdated;

            item.EstCartons = EstCartons;

            item.EstPallets = EstPallets;

            item.EstWeight = EstWeight;

            item.EstVolume = EstVolume;

            item.Palletise = Palletise;

            item.PackageTypeID = PackageTypeID;

            item.NumberOfPackages = NumberOfPackages;

            item.ActualCartons = ActualCartons;

            item.ActualPallets = ActualPallets;

            item.Jackets = Jackets;

            item.ActualWeight = ActualWeight;

            item.WeightLastUpdated = WeightLastUpdated;

            item.ActualVolume = ActualVolume;

            item.VolumeLastUpdated = VolumeLastUpdated;

            item.Fcllcl = Fcllcl;

            item.Est20 = Est20;

            item.Est40 = Est40;

            item.EstLCLWt = EstLCLWt;

            item.EstLCLVol = EstLCLVol;

            item.No20 = No20;

            item.No40 = No40;

            item.LCLWt = LCLWt;

            item.LCLVol = LCLVol;

            item.Remarks = Remarks;

            item.RemarksToCustomer = RemarksToCustomer;

            item.QuoteRef = QuoteRef;

            item.Sellingrate = Sellingrate;

            item.SellingrateAgent = SellingrateAgent;

            item.DocsRcdAndApproved = DocsRcdAndApproved;

            item.DocsApprovedDate = DocsApprovedDate;

            item.JobClosureDate = JobClosureDate;

            item.JobClosed = JobClosed;

            item.ExpressBL = ExpressBL;

            item.FumigationCert = FumigationCert;

            item.GSPCert = GSPCert;

            item.COfO = COfO;

            item.PackingDeclaration = PackingDeclaration;

            item.OtherDocsRequired = OtherDocsRequired;

            item.Incoterms = Incoterms;

            item.PricePerCopy = PricePerCopy;

            item.Customs = Customs;

            item.Currency = Currency;

            item.InvoiceAddresseeID = InvoiceAddresseeID;

            item.ConsolNumber = ConsolNumber;

            item.UnitPricePerCopy = UnitPricePerCopy;

            item.OnHold = OnHold;

            item.ContainerInfo = ContainerInfo;

            item.Cleared = Cleared;

            item.HodderPricePerCopy = HodderPricePerCopy;

            item.FileCoverPrintedOrigin = FileCoverPrintedOrigin;

            item.FileCoverPrintedDest = FileCoverPrintedDest;

            item.ClientsTerms = ClientsTerms;

            item.OriginTrucking = OriginTrucking;

            item.OrignTHC = OrignTHC;

            item.OriginDocs = OriginDocs;

            item.Freight = Freight;

            item.DestTHC = DestTHC;

            item.DestPalletisation = DestPalletisation;

            item.CustomsClearance = CustomsClearance;

            item.DeliveryCharges = DeliveryCharges;

            item.CoLoaderComments = CoLoaderComments;

            item.Pdcid = Pdcid;

            item.HCCompositeRef = HCCompositeRef;

            item.HCInvoiceAmount = HCInvoiceAmount;

            item.Impression = Impression;

            item.InsuranceValue = InsuranceValue;

            item.InsuranceValues = InsuranceValues;

            item.InvoiceNumber = InvoiceNumber;

            item.InvoiceDate = InvoiceDate;

            item.CancelRequestRcd = CancelRequestRcd;

            item.OrderCancelled = OrderCancelled;

            item.CancelDate = CancelDate;

            item.CancelledBy = CancelledBy;

            item.InvoiceTo = InvoiceTo;

            item.HCInvoiceAmount2 = HCInvoiceAmount2;

            item.OrderAckSent = OrderAckSent;

            item.CargoUpdateId = CargoUpdateId;

            item.QuoteId = QuoteId;

            item.DocumentFolder = DocumentFolder;

            item.Ts = Ts;


            item.Save(UserName);
        }
        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void PodUpdate(int OrderID, int OrderNumber, bool HotJob, int? CompanyID, int? AgentAtOriginID, int? PrinterID, int? OrderControllerID, int? OperationsControllerID, int? OriginPortControllerID, int? ContactID, DateTime? ExWorksDate, DateTime? CargoReady, DateTime? WarehouseDate, DateTime? BookingReceived, int? OriginPointID, int? PortID, int? DestinationPortID, int? FinalDestinationID, int? CountryID, int? Palletise, string RemarksToCustomer, bool DocsRcdAndApproved, DateTime? DocsApprovedDate, bool ExpressBL, bool FumigationCert, bool GSPCert, bool PackingDeclaration, string OtherDocsRequired)
        {
            //this is a fix for the issue: ods controller does not update nullable columns to null values
            //When using the Update method on ODSController (ie using GridView / FormView in an ASP.NET //application) and passing null values to nullable columns, the nullable column value remains //unchanged.
            //By creating an empty MyItem instance (all fields are null) and setting a nullable field to //null doesn' t allow the column to finish in the DirtyColumns collection (see ActiveHelper //GetUpdateCommand).
            //loading the specified record using key means MyItem instance is populated and so the nulled //column wil be dirty
            //OrderTable item = new OrderTable();
            OrderTable item = new OrderTable(OrderID);
            item.MarkOld();
            item.IsLoaded = true;

            item.OrderID = OrderID;

            item.OrderNumber = OrderNumber; //required field

            item.HotJob = HotJob;

            item.CompanyID = CompanyID;

            item.AgentAtOriginID = AgentAtOriginID;

            item.PrinterID = PrinterID;

            item.OrderControllerID = OrderControllerID;

            item.OperationsControllerID = OperationsControllerID;

            item.OriginPortControllerID = OriginPortControllerID;

            item.ContactID = ContactID;

            item.ExWorksDate = ExWorksDate;

            item.CargoReady = CargoReady;

            item.WarehouseDate = WarehouseDate;

            item.BookingReceived = BookingReceived;

            item.OriginPointID = OriginPointID;

            item.PortID = PortID;

            item.DestinationPortID = DestinationPortID;

            item.FinalDestinationID = FinalDestinationID;

            item.CountryID = CountryID;

            item.Palletise = Palletise;

            item.RemarksToCustomer = RemarksToCustomer;

            item.DocsRcdAndApproved = DocsRcdAndApproved;

            item.DocsApprovedDate = DocsApprovedDate;

            item.ExpressBL = ExpressBL;

            item.FumigationCert = FumigationCert;

            item.GSPCert = GSPCert;

            item.PackingDeclaration = PackingDeclaration;

            item.OtherDocsRequired = OtherDocsRequired;

            item.Save(UserName);
        }
        //end custom update


    }
    //end class
}