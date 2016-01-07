using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using SubSonic;
using DAL.Logistics;
/// <summary>

/// <summary>
/// Summary description for Service_Feeds
/// </summary>
namespace DAL.Services
{
    [WebService(Namespace = "http://www.publiship.com/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service_Xml : System.Web.Services.WebService
    {

        public struct keyvalues
        {
            public int itemId;
            public string itemDescription;
        }

        public Service_Xml()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "This method generates and sends an ASN (advanced shipping notification) xml file via FTP", EnableSession = false)]
        public void send_asn()
        {
            if (process_asn(123456)) {
                ftp_send();
            }
        }
        //end send_asn

        protected bool process_asn(int orderno)
        {
            bool _result = false; //returned value true if processed
            //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
            DateTime _now = DateTime.Now;
            //issuedatetime
            //string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
            string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
            string _carriercode = "Shipper"; 
            string _carriernamecode = "PUBLISHIP";

            //class in xsds folder DAL.Logistics namespace
            //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
            PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();

            //header data
            _pda.Header.DespatchAdviceNumber = 1; //despatch number? 
            _pda.Header.IssueDateTime = XmlConvert.ToDateTime(_now.ToString(), _issue).ToString();  //system.xml.xmlconvert
            //reference coded elements
            _pda.Header.ReferenceCoded.ReferenceTypeCode = "";
            _pda.Header.ReferenceCoded.ReferenceNumber = "0";
            //
            _pda.Header.PurposeCode = "";
            //datecoded elements
            _pda.Header.DateCoded.DateQualifierCode = "";
            _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
            //buyer party elements 1-6 address lines  (CUSTOMER? in publiship db)
            _pda.Header.BuyerParty.PartyName.NameLine = "";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[0] = "";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[1] = "";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[2] = "";
            _pda.Header.BuyerParty.PostalAddress.PostalCode = "";
            //seller party elements 1-6 address lines (PRINTER? in publiship db)
            _pda.Header.SellerParty.PartyName.NameLine = "";
            _pda.Header.SellerParty.PostalAddress.AddressLine[0] = "";
            _pda.Header.SellerParty.PostalAddress.AddressLine[1] = "";
            _pda.Header.SellerParty.PostalAddress.AddressLine[2] = "";
            _pda.Header.SellerParty.PostalAddress.PostalCode = "";
            //ship to party party id elements
            _pda.Header.ShipToParty.PartyID.PartyIDType = "";
            _pda.Header.ShipToParty.PartyID.Identifier = "";
            //ship to party elements 1-6 address lines (DELIVERYTO? in publiship db)
            _pda.Header.ShipToParty.PartyName.NameLine = "";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[0] = "";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[1] = "";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[2] = "";
            _pda.Header.ShipToParty.PostalAddress.PostalCode = "";
            //delivery elements
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
            _pda.Header.Delivery.TrailerNumber = "";
            //end of header

            //items emuneration
            //we will also build counters for summary data at the same time
            //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st pallet detail section will contain the
            //carton details that can be delivered in quantities of the pack size. the 2nd pallet detail section will contain the remainder
            //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
            string[] _cols = { "Copies", "ISBN" }; 
            DataTable _items = DB.Select(_cols).From(DAL.Logistics.Tables.DeliverySubTable)
                .LeftOuterJoin(DAL.Logistics.DeliverySubSubTable.DeliveryIDColumn, DAL.Logistics.DeliverySubTable.DeliveryIDColumn)
                .LeftOuterJoin(DAL.Logistics.ItemTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
                .Where(DAL.Logistics.DeliverySubTable.OrderNumberColumn).IsEqualTo(orderno).ExecuteDataSet().Tables[0];

            int _lines = 0; //item line numbers & count
            //calculate total copies
            int _copies = wwi_func.vint(_items.Compute("Copies","Copies > 0").ToString());  
            //pack size (Publiship only seems to record number of packages so use that to work out pack size)
            int _packs = wwi_func.vint("NumberOfPackages");
            //
            for (int _ix = 0; _ix < _items.Rows.Count; _ix++)
            {
                //increment line count
                _lines++; 
                       
                //build xml for item
                
            }
            //end loop

            //footer summary data

            return _result;
        }
        //end process asn

        protected bool ftp_send()
        {
            bool _result = false;

            return _result;
        }

        //for testing purpooses
        protected bool process_asn_test(int orderno)
        {
            bool _result = false; //returned value true if processed
            //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
            DateTime _now = DateTime.Now;
            //issuedatetime
            //string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
            string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
            string _carriercode = "Shipper";
            string _carriernamecode = "PUBLISHIP";

            //class in xsds folder DAL.Logistics namespace
            //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
            PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();

            //header data
            _pda.Header.DespatchAdviceNumber = 1; //despatch number? 
            _pda.Header.IssueDateTime = XmlConvert.ToDateTime(_now.ToString(), _issue).ToString();  //system.xml.xmlconvert
            //reference coded elements
            _pda.Header.ReferenceCoded.ReferenceTypeCode = "BuyersReference";
            _pda.Header.ReferenceCoded.ReferenceNumber = "7A00000041172";
            //
            _pda.Header.PurposeCode = "";
            //datecoded elements
            _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
            _pda.Header.DateCoded.DateQualifierCode = "Not Yet Shipped";
            //buyer party elements 1-6 address lines  (CUSTOMER? in publiship db)
            _pda.Header.BuyerParty.PartyName.NameLine = "Pearson Education Ltd";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[0] = "Halley Court";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[1] = "Jordan Hill";
            _pda.Header.BuyerParty.PostalAddress.AddressLine[2] = "Oxford";
            _pda.Header.BuyerParty.PostalAddress.PostalCode = "OX2 8EJ";
            //seller party elements 1-6 address lines (PRINTER? in publiship db)
            _pda.Header.SellerParty.PartyName.NameLine = "Pearson Education Asia";
            _pda.Header.SellerParty.PostalAddress.AddressLine[0] = "Cornwall House";
            _pda.Header.SellerParty.PostalAddress.AddressLine[1] = "Taikoo Place";
            _pda.Header.SellerParty.PostalAddress.AddressLine[2] = "Hong Kong";
            _pda.Header.SellerParty.PostalAddress.PostalCode = "";
            //ship to party party id elements
            _pda.Header.ShipToParty.PartyID.PartyIDType = "";
            _pda.Header.ShipToParty.PartyID.Identifier = "";
            //ship to party elements 1-6 address lines (DELIVERYTO? in publiship db)
            _pda.Header.ShipToParty.PartyName.NameLine = "Pearson Distribution Centre";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[0] = "Unit 1";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[1] = "Castle Mound Way";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[2] = "Rugby";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[3] = "Warwickshire";
            _pda.Header.ShipToParty.PostalAddress.AddressLine[4] = "UK";
            _pda.Header.ShipToParty.PostalAddress.PostalCode = "CV23 0WB";
            //delivery elements
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
            _pda.Header.Delivery.TrailerNumber = "";
            //end of header

            //items emuneration
            //we will also build counters for summary data at the same time
            //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st pallet detail section will contain the
            //carton details that can be delivered in quantities of the pack size. the 2nd pallet detail section will contain the remainder
            //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
            int _counter = 0;
            _pda.ItemDetail[_counter].LineNumber = _counter + 1;
            _pda.ItemDetail[_counter].ProductID[_counter].ProductIDType = "ISBN";
            _pda.ItemDetail[_counter].ProductID[_counter].Identifier = "9781292061313";
            _pda.ItemDetail[_counter].ItemDescription.TitleDetail = "Tarbuck: Earth Science GE_p14";
            _pda.ItemDetail[_counter].ItemDescription.BindingDescription = "UNKNOWN";
            _pda.ItemDetail[_counter].Impression = "0";
            _pda.ItemDetail[_counter].Quantity = 0;

            //measurements for this item
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasureTypeCode = "Height";
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasureTypeCode = "Width";
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasureTypeCode = "Depth";
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasureTypeCode = "UnitNetWeight";
            _pda.ItemDetail[_counter].ItemDescription.Measure[0].MeasurementValue = 0;
            //end measurements
            //reference codes
            _pda.ItemDetail[_counter].ReferenceCoded[0].ReferenceTypeCode = "BuyersOrderNumber";
            _pda.ItemDetail[_counter].ReferenceCoded[0].ReferenceNumber = "0";
            _pda.ItemDetail[_counter].ReferenceCoded[0].ReferenceTypeCode = "PrintersJobNumber";
            _pda.ItemDetail[_counter].ReferenceCoded[0].ReferenceNumber = "0";
            //end reference codes
            //pallet details for this item
            _pda.ItemDetail[_counter].PalletDetail[0].NumberOfPallets = 1;
            _pda.ItemDetail[_counter].PalletDetail[0].PalletIdentifierList[0] = "00718908562723189";
            _pda.ItemDetail[_counter].PalletDetail[0].BooksPerPallet = 1585;
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.NumberOfParcels = 1;
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.NumberOfOdds = 0;
            //measurements for pallet
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasureTypeCode = "Height";
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasureTypeCode = "Width";
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasureTypeCode = "Depth";
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasurementValue = 0;
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasureTypeCode = "UnitGrossWeight";
            _pda.ItemDetail[_counter].PalletDetail[0].ParcelDetail.Measure[0].MeasurementValue = 0;
            
            //footer summary data
            _pda.Summary.NumberOfLines = 1;
            _pda.Summary.NumberOfPallets = 1;
            _pda.Summary.NumberOfLines = 1;
            _pda.Summary.NumberOfUnits = 1;

            //serialize to file using sytem.xml.serialization and sytem.io.streamwriter
            string _folder ="//generated";
            System.Xml.Serialization.XmlSerializer _writer = new System.Xml.Serialization.XmlSerializer(typeof(PrintersDespatchAdvice));

            StreamWriter _file = new StreamWriter(_folder + "//test.xml");
            _writer.Serialize(_file, _pda);
            return _result;
        }
        //end process asn
    }
}
