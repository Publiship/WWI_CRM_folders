using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using SubSonic;
using DAL.Logistics;

/// <summary>
/// Summary description for wwi_asn
/// </summary>
public class wwi_asn
{
	public wwi_asn()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string create_and_upload_asn(int despatchnodeid, string requestby, bool deleteoriginal)
    {
        int _fileno = 0;
        //remote and local folder names
        string[] _folders = wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASNftpfolder", "value", true).Split(",".ToCharArray());
        string _local = System.IO.Path.GetFullPath(@_folders[1]);
        string _result = "";
        
        try
        {
            //log request and get despatch number
            _fileno = log_asn_file_requested(requestby);
            if (_fileno > 0)
            {

                //generate filename
                string _filename = get_asn_file_name(_fileno) + ".xml";
                _local += "\\" + _filename;
                
                //process asn file 
                
                //if (process_asn_multi_titles_data_test(_fileno, _local, test_data()) == true)
                if (process_asn_titles(despatchnodeid, _local) == true)
                {
                    //get and decrypt ftp site details from parameters xml stored as a single comma seperated encryped string
                    string[] _ftphost = wwi_security.DecryptString(wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASNftp", "value", true),"publiship").Split(",".ToCharArray());
                    //upload  
                    //ftp_handler _ftp = new ftp_handler(_ftphost[0], _ftphost[1], _ftphost[2]);
                    //_result = _ftp.upload(_folders[0] + "/" + _filename , _local);
                    //
                    //if string is not empty there must have been an error, else upload successful
                    if (string.IsNullOrEmpty(_result))
                    {
                        log_asn_file_uploaded(_fileno, _filename);   
            
                    }//end if ftp upload

                }//end if process titles
            }//end if fileno > 0
            else
            {
                _result = "Failed to log ASN request, file has not been uploaded";
            }
        }
        catch (Exception ex)
        {
            _result = ex.Message.ToString();
        }
        finally
        { 
            //delete local copy of file
            if( deleteoriginal ) {wwi_file.fso_kill_file(_local);} 
        }
        
        return _result;

    }
    
    /// <summary>
    /// record file as generated and get new file number
    /// </summary>
    /// <param name="createdby"></param>
    /// <returns></returns>
    public static int log_asn_file_requested(string requestby)
    {
        int _fileno = 0;

        AsnFileLog _log = new AsnFileLog();
        _log.DateRequested = DateTime.Now;
        _log.RequestedBy = requestby;
        _log.Save();
 
        _fileno = (int)_log.GetPrimaryKeyValue();
 
        return _fileno;
         
    }

    public static void log_asn_file_uploaded(int fileno, string filename)
    {
        //bool _logged = false;

        AsnFileLog _log = new AsnFileLog(fileno);
        _log.FileNumber = fileno;
        _log.FileName = filename;
        _log.DateUploaded = DateTime.Now;
        
        //_logged = true;

        //return _logged;
    }
    /// <summary>
    /// ASN filename specification:
    /// 2 character supplier code (Publiship's = "PU")
    /// identifier ("ASN")
    /// 5 digit formatted sequence number starting as 1 ("00001")
    /// e.g PUASN00001
    /// </summary>
    /// <returns></returns>
    public static string get_asn_file_name(int fileno)
    {
        string _result = "";
        //look up the 3 parts required from xml. the parts are in 3 seperate elements of the paramters file
        //so get all 3 and return as single string with comma-seperated values
        //2 char supplier code, identifier and next number in sequence 
        //string[] _parts = {wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASN supplier code", "value"),
        //                   wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASN identifier", "value"),
        //                  ""};
        string[] _parts = wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASNfilename", "value", true).Split(",".ToCharArray());
        
        //how many leading 0's for sequence
        //this is now _parts[2]
        //string _pad = wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "ASN sequence pad", "value");
   
        //format sequence with leading 0's e.g. 00001, the "D" modifier us usede to create leading 0's
        _parts[2] = fileno.ToString("D" + _parts[2]);

        //concatenate results
        _result = string.Join("", _parts);
 
        return _result;
    }
    //end get file name
    /// <summary>
    /// 
    /// </summary>
    /// <param name="despatchno"></param>
    /// <param name="filename"></param>
    /// <param name="asndata"></param>
    /// <returns></returns>
    public static bool process_asn_titles(int despatchnoteid, string filename)
    {

        bool _result = false;
        //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
        DateTime _now = DateTime.Now;
        //issuedatetime
        string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
        //string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
        int _npallets = 0; //total number of pallets
        int _nlines = 0; //total of line numbers
        int _nunits = 0; //total number of books
        //constants
        const string _carriercode = "Shipper";
        const string _carriernamecode = "PUBLISHIP";
        const decimal _asnversion = 0.9M;
        const string _purpose = "Original";
        const int _perlayer = 10; //parcels per layer default value
        //*************************
        //get datareader for despatch note details and titles

        Query _q = new Query(DAL.Logistics.ViewAsnConsignment.Schema);
        _q.WHERE("despatch_note_id", Comparison.Equals, despatchnoteid);

        DAL.Logistics.ViewAsnConsignmentCollection _v = new ViewAsnConsignmentCollection();
        _v.LoadAndCloseReader(_q.ExecuteReader());
        DataTable _dt = _v.ToDataTable(); 
        //**************************
        
        //should ONLY be 1 printer and buyer, but just in case step though and create a seperate pda for each printer
        //get distinct printers
        string[] _col = { "printer_name", "printer_addr1", "printer_addr2", "printer_addr3", "printer_postcode", 
                          "buyer_name", "buyer_addr1", "buyer_addr2", "buyer_addr3", "buyer_postcode"};
        DataTable _pr = _dt.DefaultView.ToTable(true, _col); 
        
        for (int _pn = 0; _pn < _pr.Rows.Count; _pn++)
        {
            //class in xsds folder DAL.Logistics namespace
            //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
            PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();
            
            //header data
            _pda.Header = new PrintersDespatchAdviceHeader();
            _pda.Header.DespatchAdviceNumber = despatchnoteid; //file number? // orderno; //despatch number? 
            _pda.version = _asnversion;
            _pda.Header.IssueDateTime = _issue;
            _pda.Header.PurposeCode = _purpose;
            //reference coded elements
            _pda.Header.ReferenceCoded = new PrintersDespatchAdviceHeaderReferenceCoded();
            _pda.Header.ReferenceCoded.ReferenceNumber = "PDC20033"; ;
            _pda.Header.ReferenceCoded.ReferenceTypeCode = "BuyersReference";
            //datecoded elements
            _pda.Header.DateCoded = new PrintersDespatchAdviceHeaderDateCoded();
            _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
            _pda.Header.DateCoded.DateQualifierCode = "Not yet shipped";
            //buyer party elements 1-6 address lines  (client? in publiship db)
            _pda.Header.BuyerParty = new PrintersDespatchAdviceHeaderBuyerParty();
            _pda.Header.BuyerParty.PartyName = new PrintersDespatchAdviceHeaderBuyerPartyPartyName();
            _pda.Header.BuyerParty.PartyName.NameLine = _pr.Rows[_pn].IsNull("buyer_name") ? "" : _pr.Rows[_pn]["buyer_name"].ToString(); //"Pearson Education Ltd";
            _pda.Header.BuyerParty.PostalAddress = new PrintersDespatchAdviceHeaderBuyerPartyPostalAddress();
            _pda.Header.BuyerParty.PostalAddress.AddressLine = new string[] { 
                _pr.Rows[_pn].IsNull("buyer_addr1") ? "": _pr.Rows[_pn]["buyer_addr1"].ToString(), 
                _pr.Rows[_pn].IsNull("buyer_addr2") ? "": _pr.Rows[_pn]["buyer_addr2"].ToString(),
                _pr.Rows[_pn].IsNull("buyer_addr3") ? "": _pr.Rows[_pn]["buyer_addr3"].ToString()
            }; //{ "Halley Court", "Jordan Hill", "Oxford" };
            _pda.Header.BuyerParty.PostalAddress.PostalCode = _pr.Rows[_pn].IsNull("buyer_postcode") ? "" : _pr.Rows[_pn]["buyer_postcode"].ToString(); //"OX2 8EJ";
            //seller party elements 1-6 address lines (PRINTER? in publiship db)
            _pda.Header.SellerParty = new PrintersDespatchAdviceHeaderSellerParty();
            _pda.Header.SellerParty.PartyName = new PrintersDespatchAdviceHeaderSellerPartyPartyName();
            _pda.Header.SellerParty.PartyName.NameLine = _pr.Rows[_pn].IsNull("printer_name") ? "" : _pr.Rows[_pn]["printer_name"].ToString(); //"Jiwabaru";
            _pda.Header.SellerParty.PostalAddress = new PrintersDespatchAdviceHeaderSellerPartyPostalAddress();
            _pda.Header.SellerParty.PostalAddress.AddressLine = new string[] { 
                 _pr.Rows[_pn].IsNull("printer_addr1") ? "": _pr.Rows[_pn]["printer_addr1"].ToString(), 
                 _pr.Rows[_pn].IsNull("printer_addr2") ? "": _pr.Rows[_pn]["printer_addr2"].ToString(), 
                 _pr.Rows[_pn].IsNull("printer_addr3") ? "": _pr.Rows[_pn]["printer_addr3"].ToString()
            }; //{ "NO: 2, JALAN P/8, KAWASAN MIEL FASA 2", "BANDAR BARU BANGI", "SELANGOR DARUL EHSAN", "43650", "Malaysia" };
            _pda.Header.SellerParty.PostalAddress.PostalCode = _pr.Rows[_pn].IsNull("printer_postcode") ? "" : _pr.Rows[_pn]["printer_postcode"].ToString(); 
            //ship to party party id elements
            _pda.Header.ShipToParty = new PrintersDespatchAdviceHeaderShipToParty();
            //required
            _pda.Header.ShipToParty.PartyID = new PrintersDespatchAdviceHeaderShipToPartyPartyID();
            _pda.Header.ShipToParty.PartyID.PartyIDType = "EAN";
            _pda.Header.ShipToParty.PartyID.Identifier = "PEAR011";
            //
            _pda.Header.ShipToParty.PartyName = new PrintersDespatchAdviceHeaderShipToPartyPartyName();
            _pda.Header.ShipToParty.PartyName.NameLine = "Pearson Distribution Centre";
            _pda.Header.ShipToParty.PostalAddress = new PrintersDespatchAdviceHeaderShipToPartyPostalAddress();
            _pda.Header.ShipToParty.PostalAddress.AddressLine = new string[] { "Unit 1", "Castle Mound Way", "Rugby", "Warwickshire", "UK" };
            _pda.Header.ShipToParty.PostalAddress.PostalCode = "CV23 0WB";
            //delivery elements
            _pda.Header.Delivery = new PrintersDespatchAdviceHeaderDelivery();
            _pda.Header.Delivery.TrailerNumber = "PU1"; //from database?
            //delivery carrier
            _pda.Header.Delivery.Carrier = new PrintersDespatchAdviceHeaderDeliveryCarrier();
            //delivery carrier carriername coded
            _pda.Header.Delivery.Carrier.CarrierNameCoded = new PrintersDespatchAdviceHeaderDeliveryCarrierCarrierNameCoded();
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
            _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
            //end header        

            //11.11.2014 Pearson agreed that when we have 4 titles on a delivery we can just give 1 SSCC code per title instead of
            //SSCC codes for every pallet
            //items emuneration
            //we will also build counters for summary data at the same time
            //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st item detail section will contain the
            //carton details that can be delivered in quantities of the pack size. the 2nd item detail section will contain the remainder
            //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
            //there should be 2 PrintersDespatchAdviceItemDetail
            int _itemcount = _dt.Select("printer_name = '" + _pda.Header.SellerParty.PartyName.NameLine.ToString() + "'").Length;
            PrintersDespatchAdviceItemDetail[] _items = new PrintersDespatchAdviceItemDetail[_itemcount];
            for (int _ix = 0; _ix < _items.Length; _ix++)
            {
                string _thisprinter = _dt.Rows[_ix].IsNull("printer_name") ? "" :_dt.Rows[_ix]["printer_name"].ToString();
                int _itemid = _dt.Rows[_ix].IsNull("item_id") ? 0 : wwi_func.vint(_dt.Rows[_ix]["item_id"].ToString());   

                if (_thisprinter == _pda.Header.SellerParty.PartyName.NameLine)
                {
                    //get copies and parcels
                    int _copies = _dt.Rows[_ix].IsNull("total_qty") ? 0 : wwi_func.vint(_dt.Rows[_ix]["total_qty"].ToString());
                    int _parcels = _dt.Rows[_ix].IsNull("parcel_count") ? 0 : wwi_func.vint(_dt.Rows[_ix]["parcel_count"].ToString());
                    //
                    _nlines += 1;//add to total line count
                    _nunits += _copies;//add to total copies 
                    _items[_ix] = new PrintersDespatchAdviceItemDetail();
                    _items[_ix].LineNumber = _ix + 1;
                    _items[_ix].Impression = _dt.Rows[_ix].IsNull("impression") ? "" : _dt.Rows[_ix]["impression"].ToString();  //impression number from database
                    _items[_ix].Quantity = _copies;

                    //item product id's include isbn and ean13 even if we don't have values for them
                    //DateTime? _ets = wwi_func.vdatetime(wwi_func.l.lookup_xml_string("xml\\parameters.xml", "name", "startETS", "value"));
                    string[] _ids = { "ISBN", "EAN13" };
                    PrintersDespatchAdviceItemDetailProductID[] _productids = new PrintersDespatchAdviceItemDetailProductID[_ids.Length];
                    for (int _nx = 0; _nx < _ids.Length; _nx++)
                    {
                        _productids[_nx] = new PrintersDespatchAdviceItemDetailProductID();
                        _productids[_nx].ProductIDType = _ids[_nx];
                        //23/03/15 populate both ISBN and EAN with ISBN 
                        _productids[_nx].Identifier = _dt.Rows[_ix].IsNull("isbn") ? "":  _dt.Rows[_ix]["isbn"].ToString();
                    }
                    _items[_ix].ProductID = _productids;
                    //end productids for this item

                    //item description
                    _items[_ix].ItemDescription = new PrintersDespatchAdviceItemDetailItemDescription();
                    _items[_ix].ItemDescription.TitleDetail = _dt.Rows[_ix]["Title"].ToString();
                    _items[_ix].ItemDescription.BindingDescription = "UNKNOWN";

                    //measurements include even if unknown
                    string[] _itemx = { "Height", "Width", "Depth", "Unit_Net_Weight" };
                    PrintersDespatchAdviceItemDetailItemDescriptionMeasure[] _measures = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure[_itemx.Length];
                    for (int _nx = 0; _nx < _itemx.Length; _nx++)
                    {
                        double _value = _dt.Rows[_ix].IsNull(_itemx[_nx]) ? 0:  wwi_func.vdouble(_dt.Rows[_ix][_itemx[_nx]].ToString());
                        _measures[_nx] = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure();
                        _measures[_nx].MeasureTypeCode = _itemx[_nx].Replace("_", "") ;
                        _measures[_nx].MeasurementValue = _value;
                    }
                    _items[_ix].ItemDescription.Measure = _measures;
                    //end measurements for item
                    //item referencecoded
                    string[] _reftypes = { "Buyers_Order_Number", "Printers_Job_Number" };//printers SRR file, publiship order number from database
                    PrintersDespatchAdviceItemDetailReferenceCoded[] _refcodes = new PrintersDespatchAdviceItemDetailReferenceCoded[_reftypes.Length];
                    for (int _nx = 0; _nx < _reftypes.Length; _nx++)
                    {
                        _refcodes[_nx] = new PrintersDespatchAdviceItemDetailReferenceCoded();
                        _refcodes[_nx].ReferenceTypeCode = _reftypes[_nx].Replace("_", "") ;
                        _refcodes[_nx].ReferenceNumber = _dt.Rows[_ix].IsNull(_reftypes[_nx]) ? "" :  _dt.Rows[_ix][_reftypes[_nx]].ToString();
                    }
                    _items[_ix].ReferenceCoded = _refcodes;
                    //end refcodes
                    //there should be no more than 2 pallet detail sections. the 1st one for whole pallets, 2nd for part pallet
                    //item pallet detail each pallet will have a pallet identifier SSCC code unless it' a 4 title delivery in which
                    //case each pallet with have a single SSCC for the title e.g. if there's 2 pallets they would have the same SSCC
                    //for testing generate a random pallet count for the 1st detail section, 2nd detail section should only have the odds
                    //
                    int _fullpallets = _dt.Rows[_ix].IsNull("full_pallets") ? 0 : wwi_func.vint(_dt.Rows[_ix]["full_pallets"].ToString()); 
                    int _partpallets = _dt.Rows[_ix].IsNull("part_pallets") ? 0: wwi_func.vint( _dt.Rows[_ix]["part_pallets"].ToString());
                    PrintersDespatchAdviceItemDetailPalletDetail[] _pallets = new PrintersDespatchAdviceItemDetailPalletDetail[_partpallets == 0? 1: 2];
                    for (int _nx = 0; _nx < _pallets.Length; _nx++)
                    {
                        //do we need to add details for part pallet even if no part pallets exist?
                        //if not just move this declaration inside if statement below
                        _pallets[_nx] = new PrintersDespatchAdviceItemDetailPalletDetail();

                        //if (_nx == 0 && _test > 0 || _nx == 1 && _part > 0)
                        //{
                        _pallets[_nx].NumberOfPallets = _nx == 0 ? _fullpallets : _partpallets;
                        
                        int _booksperpallet = 0;
                        if(_nx == 0){
                            _booksperpallet = _dt.Rows[_ix].IsNull("units_full") ? 0 : wwi_func.vint(_dt.Rows[_ix]["units_full"].ToString());
                        }
                        else
                        { 
                            _booksperpallet = _dt.Rows[_ix].IsNull("units_part") ? 0: wwi_func.vint(_dt.Rows[_ix]["units_part"].ToString());
                        }

                        _pallets[_nx].BooksPerPallet = _booksperpallet;
                        //add total # of pallets ot pallet count
                        _npallets += _pallets[_nx].NumberOfPallets;
                        //pallet identifier for each nxpallet, in a 4 title delivery they will all be same for the current title
                        //so we only need to put the sscc in ONCE regardless of _nxpallets
                        //string[] _id = new string[] { _sscc[_ix] };
                        string _pallettype = _nx == 0 ? "Full" : "Part";
                        //List<String> _ssccs = new List<String>();
                        DataTable _sscc = DAL.Logistics.DB.Select("SSCC").
                            From(DAL.Logistics.Tables.DespatchNotePalletId).
                            Where(DAL.Logistics.DespatchNotePalletId.DespatchItemIdColumn).IsEqualTo(_itemid).
                            And(DAL.Logistics.DespatchNotePalletId.PalletTypeColumn).IsEqualTo(_pallettype).ExecuteDataSet().Tables[0];
                        string[] _ssccs = wwi_func.datatable_to_array(_sscc, 0); 
                        //split json string and add pallet identifiers for this item
                        _pallets[_nx].PalletIdentifierList = _ssccs;//new string[] { _dt.Rows[_ix].IsNull("SSCC") ? "": _dt.Rows[_ix]["SSCC"].ToString() };
                        //
                        //parcel details
                        _pallets[_nx].ParcelDetail = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetail();
                        _pallets[_nx].ParcelDetail.NumberOfParcels = _dt.Rows[_ix].IsNull("parcel_count") ? 0:  wwi_func.vint(_dt.Rows[_ix]["parcel_count"].ToString()); //_nx == 0 ? (int)_full : _part > 0 ? 1 : 0;
                        _pallets[_nx].ParcelDetail.BooksPerParcel = _dt.Rows[_ix].IsNull("units_per_parcel") ? 0: wwi_func.vint(_dt.Rows[_ix]["units_per_parcel"].ToString()); //_parcels;//_nx == 0 ? (int)_books : (int)_part;
                        _pallets[_nx].ParcelDetail.NumberOfOdds = _dt.Rows[_ix].IsNull("odds_count") ? 0:  wwi_func.vint(_dt.Rows[_ix]["odds_count"].ToString());
                        _pallets[_nx].ParcelDetail.ParcelsPerLayer = _dt.Rows[_ix].IsNull("parcels_per_layer") ? _perlayer : wwi_func.vint(_dt.Rows[_ix]["parcels_per_layer"].ToString()); //default to 10

                        //measurements for parcel
                        string[] _parcelx = { "Parcel_Height", "Parcel_Width", "Parcel_Depth", "Parcel_UnitGrossWeight" };
                        PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[] _parcelmeasures = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[_parcelx.Length];
                        for (int _px = 0; _px < _parcelx.Length; _px++)
                        {
                            double _value = _dt.Rows[_ix].IsNull(_itemx[_px]) ? 0: wwi_func.vdouble(_dt.Rows[_ix][_itemx[_px]].ToString());
                            _parcelmeasures[_px] = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure();
                            _parcelmeasures[_px].MeasureTypeCode = _parcelx[_px].Replace("Parcel_", "");
                            _parcelmeasures[_px].MeasurementValue = _value;
                        }
                        //end parcel measurements
                        _pallets[_nx].ParcelDetail.Measure = _parcelmeasures;
                        //end parcel
                        //}
                    }
                    _items[_ix].PalletDetail = _pallets;
                    //end pallets
                }//end if printer namse
            }
            _pda.ItemDetail = _items;
            //end itemdatails

            //footer summary data
            _pda.Summary = new PrintersDespatchAdviceSummary();
            _pda.Summary.NumberOfLines = _nlines;
            _pda.Summary.NumberOfPallets = _npallets;
            _pda.Summary.NumberOfUnits = _nunits;
            //end pda processing

            //serialize to file using sytem.xml.serialization and sytem.io.streamwriter
            //for testing
            //string _path = "c:\\ASNTEST\\" + _filename + ".xml";
            //create a temporary file
            //string _path = "~\\asn\\" + filename + ".xml";

            System.Xml.Serialization.XmlSerializer _szr = new System.Xml.Serialization.XmlSerializer((typeof(PrintersDespatchAdvice)));
            //remove namespace references from output
            XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
            _ns.Add("", "");
            XmlWriterSettings _st = new XmlWriterSettings();
            //_st.NewLineOnAttributes = true; //not necessary forcing indent should be enough
            _st.Indent = true;
            _st.OmitXmlDeclaration = true; //remove declaration and create usng WriteRaw and WriteProcessingInstruction below so we can include standalone
            _st.Encoding = Encoding.UTF8;

            //enclose in using to ensure all processes are closed once write is complete
            using (System.Xml.XmlWriter _wrt = System.Xml.XmlWriter.Create(filename, _st))
            {
                //make sure the header lines are on multiple lines
                _wrt.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                _wrt.WriteRaw(Environment.NewLine);
                _wrt.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"CLDespatchAdvice-BIC.xsl\"");
                _wrt.WriteRaw(Environment.NewLine);
                //make sure standalone=true is included in xml declaration 
                //System.IO.StreamWriter _file = new System.IO.StreamWriter(_path);
                _szr.Serialize(_wrt, _pda, _ns);
            }
        }//end printers loop

        _result = true;

        return _result;
    }
    //end process asn

    //end process asn
    public static DataTable test_data()
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("testID", typeof(int));
        _dt.Columns.Add("Title", typeof(string));
        _dt.Columns.Add("Impression", typeof(string));
        _dt.Columns.Add("Copies", typeof(int));
        _dt.Columns.Add("Parcels", typeof(int));
        _dt.Columns.Add("SSCC", typeof(string));
        _dt.Columns.Add("ISBN", typeof(string));
        _dt.Columns.Add("POnumber", typeof(string));
        _dt.Columns.Add("PublishipNo", typeof(string));
        _dt.Columns.Add("Height", typeof(double));
        _dt.Columns.Add("Width", typeof(double));
        _dt.Columns.Add("Depth", typeof(double));
        _dt.Columns.Add("UnitNetWeight", typeof(double));
        _dt.Columns.Add("BuyersOrderNumber", typeof(string));
        _dt.Columns.Add("PrintersJobNumber", typeof(string));
        //parcel dimensions  
        _dt.Columns.Add("Parcel_Height", typeof(double));
        _dt.Columns.Add("Parcel_Width", typeof(double));
        _dt.Columns.Add("Parcel_Depth", typeof(double));
        _dt.Columns.Add("Parcel_UnitGrossWeight", typeof(double));
  
        DataColumn[] _dc = {_dt.Columns["testID"]};
        _dt.PrimaryKey = _dc;
        
        //TEST DATA
        Random _rnd = new Random(); 
        DataRow _dr1 = _dt.NewRow();
        _dr1["testID"] = 1;
        _dr1["Title"] = "Cutting Edge: Advanced Teacher's Book: A Practical Approach to Task Based Learning";
        _dr1["Impression"] = "010";
        _dr1["Copies"] = 350;
        _dr1["Parcels"] = 20;
        _dr1["SSCC"] = "008957634534534523";
        _dr1["ISBN"] = "9780582469440";
        _dr1["POnumber"] = "4F00000045336";
        _dr1["PublishipNo"] = "19478484";
        _dr1["Height"] = _rnd.Next(200, 300);
        _dr1["Width"] = _rnd.Next(200, 300);
        _dr1["Depth"] = _rnd.Next(200, 300);
        _dr1["UnitNetWeight"] = Math.Round(_rnd.NextDouble() * 800, 2);
        _dr1["BuyersOrderNumber"] = _dr1["POnumber"];
        _dr1["PrintersJobNumber"] = _dr1["PublishipNo"];
        //pallet dimensions  
        _dr1["Parcel_Height"] = _rnd.Next(350, 500);
        _dr1["Parcel_Width"] = _rnd.Next(350, 500);
        _dr1["Parcel_Depth"] = _rnd.Next(350, 500);
        _dr1["Parcel_UnitGrossWeight"] = Math.Round(_rnd.NextDouble() * 500, 2);
        _dt.Rows.Add(_dr1);
 
        DataRow _dr2 = _dt.NewRow();
        _dr2["testID"] = 2;
        _dr2["Title"] = "Cutting Edge: Upper Intermediate Workbook with Key";
        _dr2["Impression"] = "001";
        _dr2["Copies"] = 8000;
        _dr2["Parcels"] = 40;
        _dr2["SSCC"] = "003643244612645260";
        _dr2["ISBN"] = "9781447906773";
        _dr2["POnumber"] = "4F00000045372";
        _dr2["PublishipNo"] = "10033893";
        _dr2["Height"] = _rnd.Next(200, 300);
        _dr2["Width"] = _rnd.Next(200, 300);
        _dr2["Depth"] = _rnd.Next(200, 300);
        _dr2["UnitNetWeight"] = Math.Round(_rnd.NextDouble() * 800, 2);
        _dr2["BuyersOrderNumber"] = _dr2["POnumber"];
        _dr2["PrintersJobNumber"] = _dr2["PublishipNo"];
        //parcel dimensions  
        _dr2["Parcel_Height"] = _rnd.Next(350, 500);
        _dr2["Parcel_Width"] = _rnd.Next(350, 500);
        _dr2["Parcel_Depth"] = _rnd.Next(350, 500);
        _dr2["Parcel_UnitGrossWeight"] = Math.Round(_rnd.NextDouble() * 500, 2);
        _dt.Rows.Add(_dr2);
 
        return _dt;
    }

    public static bool process_asn_multi_titles_data_test(int despatchno, string filename, DataTable asndata)
    {
        bool _result = false;
        //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
        DateTime _now = DateTime.Now;
        //issuedatetime
        string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
        //string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
        int _npallets = 0; //total number of pallets
        int _nlines = 0; //total of line numbers
        int _nunits = 0; //total number of books
        //constants
        const string _carriercode = "Shipper";
        const string _carriernamecode = "PUBLISHIP";
        const decimal _asnversion = 0.9M;
        const string _purpose = "Original";


        //*************************
        //for testing a multi title pallet
        //**************************
        //class in xsds folder DAL.Logistics namespace
        //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
        PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();

        //header data

        _pda.Header = new PrintersDespatchAdviceHeader();
        _pda.Header.DespatchAdviceNumber = despatchno; //file number? // orderno; //despatch number? 
        _pda.version = _asnversion;
        _pda.Header.IssueDateTime = _issue;
        _pda.Header.PurposeCode = _purpose;
        //reference coded elements
        _pda.Header.ReferenceCoded = new PrintersDespatchAdviceHeaderReferenceCoded();
        _pda.Header.ReferenceCoded.ReferenceNumber = "PDC20033"; ;
        _pda.Header.ReferenceCoded.ReferenceTypeCode = "BuyersReference";
        //datecoded elements
        _pda.Header.DateCoded = new PrintersDespatchAdviceHeaderDateCoded();
        _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
        _pda.Header.DateCoded.DateQualifierCode = "Not yet shipped";
        //buyer party elements 1-6 address lines  (CUSTOMER? in publiship db)
        _pda.Header.BuyerParty = new PrintersDespatchAdviceHeaderBuyerParty();
        _pda.Header.BuyerParty.PartyName = new PrintersDespatchAdviceHeaderBuyerPartyPartyName();
        _pda.Header.BuyerParty.PartyName.NameLine = "Pearson Education Ltd";
        _pda.Header.BuyerParty.PostalAddress = new PrintersDespatchAdviceHeaderBuyerPartyPostalAddress();
        _pda.Header.BuyerParty.PostalAddress.AddressLine = new string[] { "Halley Court", "Jordan Hill", "Oxford" };
        _pda.Header.BuyerParty.PostalAddress.PostalCode = "OX2 8EJ";
        //seller party elements 1-6 address lines (PRINTER? in publiship db)
        _pda.Header.SellerParty = new PrintersDespatchAdviceHeaderSellerParty();
        _pda.Header.SellerParty.PartyName = new PrintersDespatchAdviceHeaderSellerPartyPartyName();
        _pda.Header.SellerParty.PartyName.NameLine = "Jiwabaru";
        _pda.Header.SellerParty.PostalAddress = new PrintersDespatchAdviceHeaderSellerPartyPostalAddress();
        _pda.Header.SellerParty.PostalAddress.AddressLine = new string[] { "NO: 2, JALAN P/8, KAWASAN MIEL FASA 2", "BANDAR BARU BANGI", "SELANGOR DARUL EHSAN", "43650", "Malaysia" };
        _pda.Header.SellerParty.PostalAddress.PostalCode = "";
        //ship to party party id elements
        _pda.Header.ShipToParty = new PrintersDespatchAdviceHeaderShipToParty();
        //required
        _pda.Header.ShipToParty.PartyID = new PrintersDespatchAdviceHeaderShipToPartyPartyID();
        _pda.Header.ShipToParty.PartyID.PartyIDType = "EAN";
        _pda.Header.ShipToParty.PartyID.Identifier = "PEAR011";
        //
        _pda.Header.ShipToParty.PartyName = new PrintersDespatchAdviceHeaderShipToPartyPartyName();
        _pda.Header.ShipToParty.PartyName.NameLine = "Pearson Distribution Centre";
        _pda.Header.ShipToParty.PostalAddress = new PrintersDespatchAdviceHeaderShipToPartyPostalAddress();
        _pda.Header.ShipToParty.PostalAddress.AddressLine = new string[] { "Unit 1", "Castle Mound Way", "Rugby", "Warwickshire", "UK" };
        _pda.Header.ShipToParty.PostalAddress.PostalCode = "CV23 0WB";
        //delivery elements
        _pda.Header.Delivery = new PrintersDespatchAdviceHeaderDelivery();
        _pda.Header.Delivery.TrailerNumber = "PU1"; //from database
        //delivery carrier
        _pda.Header.Delivery.Carrier = new PrintersDespatchAdviceHeaderDeliveryCarrier();
        //delivery carrier carriername coded
        _pda.Header.Delivery.Carrier.CarrierNameCoded = new PrintersDespatchAdviceHeaderDeliveryCarrierCarrierNameCoded();
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
        //end header        

        //11.11.2014 Pearson aggreed that when we have 4 titles on a delivery we can just give 1 SSCC code per title instead of
        //SSCC codes for every pallet
        //items emuneration
        //we will also build counters for summary data at the same time
        //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st item detail section will contain the
        //carton details that can be delivered in quantities of the pack size. the 2nd item detail section will contain the remainder
        //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
        //there should be 2 PrintersDespatchAdviceItemDetail
        PrintersDespatchAdviceItemDetail[] _items = new PrintersDespatchAdviceItemDetail[asndata.Rows.Count];
        for (int _ix = 0; _ix < _items.Length; _ix++)
        {
            //get copeis and parcels
            int _copies = wwi_func.vint(asndata.Rows[_ix]["Copies"].ToString());
            int _parcels = wwi_func.vint(asndata.Rows[_ix]["Parcels"].ToString());
            //
            _nlines += 1;//add to total line count
            _nunits += _copies;//add to total copies 
            _items[_ix] = new PrintersDespatchAdviceItemDetail();
            _items[_ix].LineNumber = _ix + 1;
            _items[_ix].Impression = asndata.Rows[_ix]["Impression"].ToString();  //impression number from database
            _items[_ix].Quantity = _copies;

            //item product id's include isbn and ean13 even if we don't have values for them
            //DateTime? _ets = wwi_func.vdatetime(wwi_func.l.lookup_xml_string("xml\\parameters.xml", "name", "startETS", "value"));
            string[] _ids = { "ISBN", "EAN13" };
            PrintersDespatchAdviceItemDetailProductID[] _productids = new PrintersDespatchAdviceItemDetailProductID[_ids.Length];
            for (int _nx = 0; _nx < _ids.Length; _nx++)
            {
                _productids[_nx] = new PrintersDespatchAdviceItemDetailProductID();
                _productids[_nx].ProductIDType = _ids[_nx];
                //23/03/15 populate ISBN and EAN with ISBN
                _productids[_nx].Identifier = asndata.Rows[_ix]["ISBN"].ToString();
            }
            _items[_ix].ProductID = _productids;
            //end productids for this item

            //item description
            _items[_ix].ItemDescription = new PrintersDespatchAdviceItemDetailItemDescription();
            _items[_ix].ItemDescription.TitleDetail = asndata.Rows[_ix]["Title"].ToString();
            _items[_ix].ItemDescription.BindingDescription = "UNKNOWN";

            //measurements include even if unknown
            string[] _itemx = { "Height", "Width", "Depth", "UnitNetWeight" };
            PrintersDespatchAdviceItemDetailItemDescriptionMeasure[] _measures = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure[_itemx.Length];
            for (int _nx = 0; _nx < _itemx.Length; _nx++)
            {
                double _value =  wwi_func.vdouble(asndata.Rows[_ix][_itemx[_nx]].ToString());
                _measures[_nx] = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure();
                _measures[_nx].MeasureTypeCode = _itemx[_nx];
                _measures[_nx].MeasurementValue = _value;
            }
            _items[_ix].ItemDescription.Measure = _measures;
            //end measurements for item
            //item referencecoded
            string[] _reftypes = { "BuyersOrderNumber", "PrintersJobNumber" };//printers SRR file, publiship order number from database
            PrintersDespatchAdviceItemDetailReferenceCoded[] _refcodes = new PrintersDespatchAdviceItemDetailReferenceCoded[_reftypes.Length];
            for (int _nx = 0; _nx < _reftypes.Length; _nx++)
            {
                _refcodes[_nx] = new PrintersDespatchAdviceItemDetailReferenceCoded();
                _refcodes[_nx].ReferenceTypeCode = _reftypes[_nx];
                _refcodes[_nx].ReferenceNumber = asndata.Rows[_ix][_reftypes[_nx]].ToString();
            }
            _items[_ix].ReferenceCoded = _refcodes;
            //end refcodes
            //there should be no more than 2 pallet detail sections. the 1st one for whole pallets, 2nd for part pallet
            //item pallet detail each pallet will have a pallet identifier SSCC code unless it' a 4 title delivery in which
            //case each pallet with have a single SSCC for the title e.g. if there's 2 pallets they would have the same SSCC
            //for testing generate a random pallet count for the 1st detail section, 2nd detail section should only have the odds
            //
            //testing generate a random number of books per pallet

            double _test = _copies / _parcels;
            double _part = _copies % _parcels; //get remainder BOOK count to go on part pallets
            double _full = Math.Floor(_test); //get full pallet count
            int _details = _part > 0 ? 2 : 1; //
            PrintersDespatchAdviceItemDetailPalletDetail[] _pallets = new PrintersDespatchAdviceItemDetailPalletDetail[_details];
            for (int _nx = 0; _nx < _pallets.Length; _nx++)
            {
                //do we need to add details for part pallet even if no part pallets exist?
                //if not just move this declaration inside if statement below
                _pallets[_nx] = new PrintersDespatchAdviceItemDetailPalletDetail();

                //if (_nx == 0 && _test > 0 || _nx == 1 && _part > 0)
                //{
                _pallets[_nx].NumberOfPallets = 1; //_nx == 0 ? (int)_full : _part > 0 ? 1: 0;
                _pallets[_nx].BooksPerPallet = _nx == 0 ? (int)_copies - (int)_part : (int)_part;
                //add total # of pallets ot pallet count
                _npallets += _pallets[_nx].NumberOfPallets;
                //pallet identifier for each nxpallet, in a 4 title delivery they will all be same for the current title
                //so we only need to put the sscc in ONCE regardless of _nxpallets
                //string[] _id = new string[] { _sscc[_ix] };
                _pallets[_nx].PalletIdentifierList = new string[] { asndata.Rows[_ix]["SSCC"].ToString() };

                //parcel details
                _pallets[_nx].ParcelDetail = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetail();
                _pallets[_nx].ParcelDetail.NumberOfParcels = _nx == 0 ? (int)_full : _part > 0 ? 1 : 0;
                _pallets[_nx].ParcelDetail.BooksPerParcel = _parcels;//_nx == 0 ? (int)_books : (int)_part;
                _pallets[_nx].ParcelDetail.NumberOfOdds = 0;
                _pallets[_nx].ParcelDetail.ParcelsPerLayer = 10;

                //measurements for parcel
                string[] _parcelx = { "Parcel_Height", "Parcel_Width", "Parcel_Depth", "Parcel_UnitGrossWeight" };
                PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[] _parcelmeasures = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[_parcelx.Length];
                for (int _px = 0; _px < _parcelx.Length; _px++)
                {
                    double _value = wwi_func.vdouble(asndata.Rows[_ix][_itemx[_px]].ToString());
                    _parcelmeasures[_px] = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure();
                    _parcelmeasures[_px].MeasureTypeCode = _parcelx[_px].Replace("Parcel_","");
                    _parcelmeasures[_px].MeasurementValue =_value;
                }
                //end parcel measurements
                _pallets[_nx].ParcelDetail.Measure = _parcelmeasures;
                //end parcel
                //}
            }
            _items[_ix].PalletDetail = _pallets;
            //end pallets
        }
        _pda.ItemDetail = _items;
        //end itemdatails

        //footer summary data
        _pda.Summary = new PrintersDespatchAdviceSummary();
        _pda.Summary.NumberOfLines = _nlines;
        _pda.Summary.NumberOfPallets = _npallets;
        _pda.Summary.NumberOfUnits = _nunits;
        //end pda processing

        //serialize to file using sytem.xml.serialization and sytem.io.streamwriter
        //for testing
        //string _path = "c:\\ASNTEST\\" + _filename + ".xml";
        //create a temporary file
        //string _path = "~\\asn\\" + filename + ".xml";

        System.Xml.Serialization.XmlSerializer _szr = new System.Xml.Serialization.XmlSerializer((typeof(PrintersDespatchAdvice)));
        //remove namespace references from output
        XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
        _ns.Add("", "");
        XmlWriterSettings _st = new XmlWriterSettings();
        //_st.NewLineOnAttributes = true; //not necessary forcing indent should be enough
        _st.Indent = true;
        _st.OmitXmlDeclaration = true; //remove declaration and create usng WriteRaw and WriteProcessingInstruction below so we can include standalone
        _st.Encoding = Encoding.UTF8;

        //enclose in using to ensure all processes are closed once write is complete
        using (System.Xml.XmlWriter _wrt = System.Xml.XmlWriter.Create(filename, _st))
        {
            //make sure the header lines are on multiple lines
            _wrt.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            _wrt.WriteRaw(Environment.NewLine);
            _wrt.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"CLDespatchAdvice-BIC.xsl\"");
            _wrt.WriteRaw(Environment.NewLine);
            //make sure standalone=true is included in xml declaration 
            //System.IO.StreamWriter _file = new System.IO.StreamWriter(_path);
            _szr.Serialize(_wrt, _pda, _ns);
        }
        _result = true;

        return _result;
    }
    //end process asn

    public static bool process_asn_multi_titles_simple_test(int despatchno, string filename)
    {
        bool _result = false;
        //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
        DateTime _now = DateTime.Now;
        //issuedatetime
        string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
        //string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
        int _npallets = 0; //total number of pallets
        int _nlines = 0; //total of line numbers
        int _nunits = 0; //total number of books
        //constants
        const string _carriercode = "Shipper";
        const string _carriernamecode = "PUBLISHIP";
        const decimal _asnversion = 0.9M;
        const string _purpose = "Original";


        //*************************
        //for testing a multi title pallet
        Random _rnd = new Random();
        string[] _titles = { "Cutting Edge: Advanced Teacher's Book: A Practical Approach to Task Based Learning", "Cutting Edge: Upper Intermediate Workbook with Key" }; //{ "New General Maths Jss Sb 1", "New General Maths Jss Sb 2", "New General Maths Ne Ng Ss 3" };
        string[] _impression = { "010", "001" };
        int[] _copies = { 350, 8000 }; //{ 3000 , _rnd.Next(40, 65), _rnd.Next(30, 70)};
        int[] _parcels = { 20, 40 };
        string[] _sscc = { "008957634534534523", "003643244612645260" };
        string[] _isbn = { "9780582469440", "9781447906773" };
        string[] _ponumbers = { "4F00000045336 ", "4F00000045372" };
        string[] _puborder = { "19478484 ", "10033893" };
        //**************************
        //class in xsds folder DAL.Logistics namespace
        //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
        PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();

        //header data

        _pda.Header = new PrintersDespatchAdviceHeader();
        _pda.Header.DespatchAdviceNumber = despatchno; //file number? // orderno; //despatch number? 
        _pda.version = _asnversion;
        _pda.Header.IssueDateTime = _issue;
        _pda.Header.PurposeCode = _purpose;
        //reference coded elements
        _pda.Header.ReferenceCoded = new PrintersDespatchAdviceHeaderReferenceCoded();
        _pda.Header.ReferenceCoded.ReferenceNumber = "PDC20033"; ;
        _pda.Header.ReferenceCoded.ReferenceTypeCode = "BuyersReference";
        //datecoded elements
        _pda.Header.DateCoded = new PrintersDespatchAdviceHeaderDateCoded();
        _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
        _pda.Header.DateCoded.DateQualifierCode = "Not yet shipped";
        //buyer party elements 1-6 address lines  (CUSTOMER? in publiship db)
        _pda.Header.BuyerParty = new PrintersDespatchAdviceHeaderBuyerParty();
        _pda.Header.BuyerParty.PartyName = new PrintersDespatchAdviceHeaderBuyerPartyPartyName();
        _pda.Header.BuyerParty.PartyName.NameLine = "Pearson Education Ltd";
        _pda.Header.BuyerParty.PostalAddress = new PrintersDespatchAdviceHeaderBuyerPartyPostalAddress();
        _pda.Header.BuyerParty.PostalAddress.AddressLine = new string[] { "Halley Court", "Jordan Hill", "Oxford" };
        _pda.Header.BuyerParty.PostalAddress.PostalCode = "OX2 8EJ";
        //seller party elements 1-6 address lines (PRINTER? in publiship db)
        _pda.Header.SellerParty = new PrintersDespatchAdviceHeaderSellerParty();
        _pda.Header.SellerParty.PartyName = new PrintersDespatchAdviceHeaderSellerPartyPartyName();
        _pda.Header.SellerParty.PartyName.NameLine = "Jiwabaru";
        _pda.Header.SellerParty.PostalAddress = new PrintersDespatchAdviceHeaderSellerPartyPostalAddress();
        _pda.Header.SellerParty.PostalAddress.AddressLine = new string[] { "NO: 2, JALAN P/8, KAWASAN MIEL FASA 2", "BANDAR BARU BANGI", "SELANGOR DARUL EHSAN", "43650", "Malaysia" };
        _pda.Header.SellerParty.PostalAddress.PostalCode = "";
        //ship to party party id elements
        _pda.Header.ShipToParty = new PrintersDespatchAdviceHeaderShipToParty();
        //required
        _pda.Header.ShipToParty.PartyID = new PrintersDespatchAdviceHeaderShipToPartyPartyID();
        _pda.Header.ShipToParty.PartyID.PartyIDType = "EAN";
        _pda.Header.ShipToParty.PartyID.Identifier = "PEAR011";
        //
        _pda.Header.ShipToParty.PartyName = new PrintersDespatchAdviceHeaderShipToPartyPartyName();
        _pda.Header.ShipToParty.PartyName.NameLine = "Pearson Distribution Centre";
        _pda.Header.ShipToParty.PostalAddress = new PrintersDespatchAdviceHeaderShipToPartyPostalAddress();
        _pda.Header.ShipToParty.PostalAddress.AddressLine = new string[] { "Unit 1", "Castle Mound Way", "Rugby", "Warwickshire", "UK" };
        _pda.Header.ShipToParty.PostalAddress.PostalCode = "CV23 0WB";
        //delivery elements
        _pda.Header.Delivery = new PrintersDespatchAdviceHeaderDelivery();
        _pda.Header.Delivery.TrailerNumber = "PU1"; //from database
        //delivery carrier
        _pda.Header.Delivery.Carrier = new PrintersDespatchAdviceHeaderDeliveryCarrier();
        //delivery carrier carriername coded
        _pda.Header.Delivery.Carrier.CarrierNameCoded = new PrintersDespatchAdviceHeaderDeliveryCarrierCarrierNameCoded();
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
        //end header        

        //11.11.2014 Pearson aggreed that when we have 4 titles on a delivery we can just give 1 SSCC code per title instead of
        //SSCC codes for every pallet
        //items emuneration
        //we will also build counters for summary data at the same time
        //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st item detail section will contain the
        //carton details that can be delivered in quantities of the pack size. the 2nd item detail section will contain the remainder
        //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
        //there should be 2 PrintersDespatchAdviceItemDetail
        PrintersDespatchAdviceItemDetail[] _items = new PrintersDespatchAdviceItemDetail[_titles.Length];
        for (int _ix = 0; _ix < _items.Length; _ix++)
        {
            _nlines += 1;//add to total line count
            _nunits += _copies[_ix];//add to total copies 
            _items[_ix] = new PrintersDespatchAdviceItemDetail();
            _items[_ix].LineNumber = _ix + 1;
            _items[_ix].Impression = _impression[_ix];  //impression number from database
            _items[_ix].Quantity = _copies[_ix];

            //item product id's include isbn and ean13 even if we don't have values for them
            //DateTime? _ets = wwi_func.vdatetime(wwi_func.l.lookup_xml_string("xml\\parameters.xml", "name", "startETS", "value"));
            string[] _ids = { "ISBN", "EAN13" };
            PrintersDespatchAdviceItemDetailProductID[] _productids = new PrintersDespatchAdviceItemDetailProductID[_ids.Length];
            for (int _nx = 0; _nx < _ids.Length; _nx++)
            {
                _productids[_nx] = new PrintersDespatchAdviceItemDetailProductID();
                _productids[_nx].ProductIDType = _ids[_nx];
                //23/03/15 populate ISBN and EAN with ISBN
                _productids[_nx].Identifier = _isbn[_ix]; //_ids[_nx] == "ISBN"? _isbn[_ix]: "";
            }
            _items[_ix].ProductID = _productids;
            //end productids for this item

            //item description
            _items[_ix].ItemDescription = new PrintersDespatchAdviceItemDetailItemDescription();
            _items[_ix].ItemDescription.TitleDetail = _titles[_ix];
            _items[_ix].ItemDescription.BindingDescription = "UNKNOWN";

            //measurements include even if unknown
            string[] _itemx = { "Height", "Width", "Depth", "UnitNetWeight" };
            PrintersDespatchAdviceItemDetailItemDescriptionMeasure[] _measures = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure[_itemx.Length];
            for (int _nx = 0; _nx < _itemx.Length; _nx++)
            {
                _measures[_nx] = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure();
                _measures[_nx].MeasureTypeCode = _itemx[_nx];
                _measures[_nx].MeasurementValue = _nx < 3 ? _rnd.Next(200, 300) : Math.Round(_rnd.NextDouble() * 800, 2);
            }
            _items[_ix].ItemDescription.Measure = _measures;
            //end measurements for item
            //item referencecoded
            string[] _reftypes = { "BuyersOrderNumber", "PrintersJobNumber" };//printers SRR file, publiship order number from database
            PrintersDespatchAdviceItemDetailReferenceCoded[] _refcodes = new PrintersDespatchAdviceItemDetailReferenceCoded[_reftypes.Length];
            for (int _nx = 0; _nx < _reftypes.Length; _nx++)
            {
                _refcodes[_nx] = new PrintersDespatchAdviceItemDetailReferenceCoded();
                _refcodes[_nx].ReferenceTypeCode = _reftypes[_nx];
                _refcodes[_nx].ReferenceNumber = _nx == 0 ? _ponumbers[_nx] : _puborder[_nx];
            }
            _items[_ix].ReferenceCoded = _refcodes;
            //end refcodes
            //there should be no more than 2 pallet detail sections. the 1st one for whole pallets, 2nd for part pallet
            //item pallet detail each pallet will have a pallet identifier SSCC code unless it' a 4 title delivery in which
            //case each pallet with have a single SSCC for the title e.g. if there's 2 pallets they would have the same SSCC
            //for testing generate a random pallet count for the 1st detail section, 2nd detail section should only have the odds
            //
            //testing generate a random number of books per pallet

            double _test = _copies[_ix] / _parcels[_ix];
            double _part = _copies[_ix] % _parcels[_ix]; //get remainder BOOK count to go on part pallets
            double _full = Math.Floor(_test); //get full pallet count
            int _details = _part > 0 ? 2 : 1; //
            PrintersDespatchAdviceItemDetailPalletDetail[] _pallets = new PrintersDespatchAdviceItemDetailPalletDetail[_details];
            for (int _nx = 0; _nx < _pallets.Length; _nx++)
            {
                //do we need to add details for part pallet even if no part pallets exist?
                //if not just move this declaration inside if statement below
                _pallets[_nx] = new PrintersDespatchAdviceItemDetailPalletDetail();

                //if (_nx == 0 && _test > 0 || _nx == 1 && _part > 0)
                //{
                _pallets[_nx].NumberOfPallets = 1; //_nx == 0 ? (int)_full : _part > 0 ? 1: 0;
                _pallets[_nx].BooksPerPallet = _nx == 0 ? (int)_copies[_ix] - (int)_part : (int)_part;
                //add total # of pallets ot pallet count
                _npallets += _pallets[_nx].NumberOfPallets;
                //pallet identifier for each nxpallet, in a 4 title delivery they will all be same for the current title
                //so we only need to put the sscc in ONCE regardless of _nxpallets
                //string[] _id = new string[] { _sscc[_ix] };
                _pallets[_nx].PalletIdentifierList = new string[] { _sscc[_nx] }; //_nx== 0 ? _id : _part > 0 ? _id : new string[] {""};

                //parcel details
                _pallets[_nx].ParcelDetail = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetail();
                _pallets[_nx].ParcelDetail.NumberOfParcels = _nx == 0 ? (int)_full : _part > 0 ? 1 : 0;
                _pallets[_nx].ParcelDetail.BooksPerParcel = _parcels[_ix];//_nx == 0 ? (int)_books : (int)_part;
                _pallets[_nx].ParcelDetail.NumberOfOdds = 0;
                _pallets[_nx].ParcelDetail.ParcelsPerLayer = 10;

                //measurements for parcel
                string[] _parcelx = { "Height", "Width", "Depth", "UnitGrossWeight" };
                PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[] _parcelmeasures = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[_parcelx.Length];
                for (int _px = 0; _px < _parcelx.Length; _px++)
                {
                    _parcelmeasures[_px] = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure();
                    _parcelmeasures[_px].MeasureTypeCode = _parcelx[_px];
                    _parcelmeasures[_px].MeasurementValue = _nx < 3 ? _rnd.Next(350, 500) : Math.Round(_rnd.NextDouble() * 500, 2);
                }
                //end parcel measurements
                _pallets[_nx].ParcelDetail.Measure = _parcelmeasures;
                //end parcel
                //}
            }
            _items[_ix].PalletDetail = _pallets;
            //end pallets
        }
        _pda.ItemDetail = _items;
        //end itemdatails

        //footer summary data
        _pda.Summary = new PrintersDespatchAdviceSummary();
        _pda.Summary.NumberOfLines = _nlines;
        _pda.Summary.NumberOfPallets = _npallets;
        _pda.Summary.NumberOfUnits = _nunits;
        //end pda processing

        //serialize to file using sytem.xml.serialization and sytem.io.streamwriter
        //for testing
        //string _path = "c:\\ASNTEST\\" + _filename + ".xml";
        //create a temporary file
        //string _path = "~\\asn\\" + filename + ".xml";
        
        System.Xml.Serialization.XmlSerializer _szr = new System.Xml.Serialization.XmlSerializer((typeof(PrintersDespatchAdvice)));
        //remove namespace references from output
        XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
        _ns.Add("", "");
        XmlWriterSettings _st = new XmlWriterSettings();
        //_st.NewLineOnAttributes = true; //not necessary forcing indent should be enough
        _st.Indent = true;
        _st.OmitXmlDeclaration = true; //remove declaration and create usng WriteRaw and WriteProcessingInstruction below so we can include standalone
        _st.Encoding = Encoding.UTF8;

        //enclose in using to ensure all processes are closed once write is complete
        using (System.Xml.XmlWriter _wrt = System.Xml.XmlWriter.Create(filename, _st))
        {
            //make sure the header lines are on multiple lines
            _wrt.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            _wrt.WriteRaw(Environment.NewLine);
            _wrt.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"CLDespatchAdvice-BIC.xsl\"");
            _wrt.WriteRaw(Environment.NewLine);
            //make sure standalone=true is included in xml declaration 
            //System.IO.StreamWriter _file = new System.IO.StreamWriter(_path);
            _szr.Serialize(_wrt, _pda, _ns);
        }
        _result = true;
        
        return _result;
    }
    //end process asn
    
    public static bool process_asn_test_1title(int orderno)
    {
        bool _result = false; //returned value true if processed
        //all datetimes in the .xsd schema manually converted to strings otherwise we can't format them as required in ASN specification
        DateTime _now = DateTime.Now;
        //issuedatetime
        string _issue = _now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); //for formatting issue date to 2014-01-30T13:07:45+00:00
        //string _issue = "yyyy-MM-dd\"T\"HH:mm:sszzz"; //for formatting issue date to 2014-01-30T13:07:45+00:00
        int _npallets = 0; //total number of pallts
        int _nlines = 0; //total of line numbers
        int _nunits = 0; //total number of books
        //constants
        const int _details = 0;
        const string _carriercode = "Shipper";
        const string _carriernamecode = "PUBLISHIP";
        const decimal _asnversion = 0.9M;
        const string _purpose = "Original";

        //*************************
        //for testing a 1 title pallet
        const byte _numpallets = 1;
        string[] _titles = { "8 Hein Maths 4 Number Workbk Pk 8" };
        int[] _copies = { 1200 };
        //full pallets
        string[] _sscc1 = { "000312414124512516", "008956784575745752", "006523877238568293" }; //need an SSCC for each PALLET 
        //odd pallet
        string[] _sscc2 = { "003854336045342954" };
        //**************************
        //class in xsds folder DAL.Logistics namespace
        //DAL.Logistics.PrintersDespatchAdvice _xml = new DAL.Logistics.PrintersDespatchAdvice  
        PrintersDespatchAdvice _pda = new PrintersDespatchAdvice();

        //header data
        _pda.Header = new PrintersDespatchAdviceHeader();
        _pda.version = _asnversion;
        _pda.Header.DespatchAdviceNumber = 1; // orderno; //despatch number? 
        _pda.Header.IssueDateTime = _issue;
        _pda.Header.PurposeCode = _purpose;
        //reference coded elements
        _pda.Header.ReferenceCoded = new PrintersDespatchAdviceHeaderReferenceCoded();
        _pda.Header.ReferenceCoded.ReferenceNumber = "7A00000041172"; ;
        _pda.Header.ReferenceCoded.ReferenceTypeCode = "BuyersReference";
        //datecoded elements
        _pda.Header.DateCoded = new PrintersDespatchAdviceHeaderDateCoded();
        _pda.Header.DateCoded.Date = _now.ToString("yyyyMMdd"); ///format yyyyMMdd 
        _pda.Header.DateCoded.DateQualifierCode = "Not yet shipped";
        //buyer party elements 1-6 address lines  (CUSTOMER? in publiship db)
        _pda.Header.BuyerParty = new PrintersDespatchAdviceHeaderBuyerParty();
        _pda.Header.BuyerParty.PartyName = new PrintersDespatchAdviceHeaderBuyerPartyPartyName();
        _pda.Header.BuyerParty.PartyName.NameLine = "Pearson Education Ltd"; ;
        _pda.Header.BuyerParty.PostalAddress = new PrintersDespatchAdviceHeaderBuyerPartyPostalAddress();
        _pda.Header.BuyerParty.PostalAddress.AddressLine = new string[] { "Halley Court", "Jordan Hill", "Oxford" };
        _pda.Header.BuyerParty.PostalAddress.PostalCode = "OX2 8EJ";
        //seller party elements 1-6 address lines (PRINTER? in publiship db)
        _pda.Header.SellerParty = new PrintersDespatchAdviceHeaderSellerParty();
        _pda.Header.SellerParty.PartyName = new PrintersDespatchAdviceHeaderSellerPartyPartyName();
        _pda.Header.SellerParty.PartyName.NameLine = "CTPS Holdings Inc";
        _pda.Header.SellerParty.PostalAddress = new PrintersDespatchAdviceHeaderSellerPartyPostalAddress();
        _pda.Header.SellerParty.PostalAddress.AddressLine = new string[] { "6/F, Reliance Mfy. Building", "24 Wong Chuk Hang Road", "Hong Kong/China" };
        _pda.Header.SellerParty.PostalAddress.PostalCode = "";
        //ship to party party id elements
        _pda.Header.ShipToParty = new PrintersDespatchAdviceHeaderShipToParty();
        //required
        _pda.Header.ShipToParty.PartyID = new PrintersDespatchAdviceHeaderShipToPartyPartyID();
        _pda.Header.ShipToParty.PartyID.PartyIDType = "EAN";
        _pda.Header.ShipToParty.PartyID.Identifier = "PEAR011";
        //
        _pda.Header.ShipToParty.PartyName = new PrintersDespatchAdviceHeaderShipToPartyPartyName();
        _pda.Header.ShipToParty.PartyName.NameLine = "Pearson Distribution Centre";
        _pda.Header.ShipToParty.PostalAddress = new PrintersDespatchAdviceHeaderShipToPartyPostalAddress();
        _pda.Header.ShipToParty.PostalAddress.AddressLine = new string[] { "Unit 1", "Castle Mound Way", "Rugby", "Warwickshire", "UK" };
        _pda.Header.ShipToParty.PostalAddress.PostalCode = "CV23 0WB";
        //delivery elements
        _pda.Header.Delivery = new PrintersDespatchAdviceHeaderDelivery();
        _pda.Header.Delivery.TrailerNumber = "";
        //delivery carrier
        _pda.Header.Delivery.Carrier = new PrintersDespatchAdviceHeaderDeliveryCarrier();
        //delivery carrier carriername coded
        _pda.Header.Delivery.Carrier.CarrierNameCoded = new PrintersDespatchAdviceHeaderDeliveryCarrierCarrierNameCoded();
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCodeType = _carriercode;
        _pda.Header.Delivery.Carrier.CarrierNameCoded.CarrierNameCode = _carriernamecode;
        //end header        

        //11.11.2014 Pearson aggreeed that when we have 4 titles on a delivery we can just give 1 SSCC code per title instead of
        //SSCC codes for every pallet
        //items emuneration
        //we will also build counters for summary data at the same time
        //get total number of books for the ISBN and divide by books per pallet (pack size). the 1st item detail section will contain the
        //carton details that can be delivered in quantities of the pack size. the 2nd item detail section will contain the remainder
        //e.g. no of books = 270, pack size = 40: 1st pallet section number of pallets = 6 x 40, 2nd section = 1 x 30.
        //there should be 2 PrintersDespatchAdviceItemDetail
        PrintersDespatchAdviceItemDetail[] _items = new PrintersDespatchAdviceItemDetail[_titles.Length];
        for (int _ix = 0; _ix < _items.Length; _ix++)
        {
            _nlines += 1;//add to total line count
            _nunits += _copies[_ix];//add to total copies 
            _items[_ix] = new PrintersDespatchAdviceItemDetail();
            _items[_ix].LineNumber = _ix + 1;
            _items[_ix].Impression = "0";
            _items[_ix].Quantity = _copies[_ix];

            //item product id's
            string[] _ids = { "ISBN" };
            PrintersDespatchAdviceItemDetailProductID[] _productids = new PrintersDespatchAdviceItemDetailProductID[_ids.Length];
            for (int _nx = 0; _nx < _ids.Length; _nx++)
            {
                _productids[_nx] = new PrintersDespatchAdviceItemDetailProductID();
                _productids[_nx].ProductIDType = _ids[_nx];
                _productids[_nx].Identifier = "9781742660301";
            }
            _items[_ix].ProductID = _productids;
            //end produtids for this item


            //item description
            _items[_ix].ItemDescription = new PrintersDespatchAdviceItemDetailItemDescription();
            _items[_ix].ItemDescription.TitleDetail = _titles[_ix];
            _items[_ix].ItemDescription.BindingDescription = "UNKNOWN";

            //measurements include even if unknown
            string[] _itemx = { "Height", "Width", "Depth", "UnitNetWeight" };
            PrintersDespatchAdviceItemDetailItemDescriptionMeasure[] _measures = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure[_itemx.Length];
            for (int _nx = 0; _nx < _itemx.Length; _nx++)
            {
                _measures[_nx] = new PrintersDespatchAdviceItemDetailItemDescriptionMeasure();
                _measures[_nx].MeasureTypeCode = _itemx[_nx];
                _measures[_nx].MeasurementValue = 0;
            }
            _items[_ix].ItemDescription.Measure = _measures;
            //end measurements for item

            //item referencecoded
            string[] _reftypes = { "BuyersOrderNumber", "PrintersJobNumber" };
            PrintersDespatchAdviceItemDetailReferenceCoded[] _refcodes = new PrintersDespatchAdviceItemDetailReferenceCoded[_reftypes.Length];
            for (int _nx = 0; _nx < _reftypes.Length; _nx++)
            {
                _refcodes[_nx] = new PrintersDespatchAdviceItemDetailReferenceCoded();
                _refcodes[_nx].ReferenceTypeCode = _reftypes[_nx];
                _refcodes[_nx].ReferenceNumber = "12345";
            }
            _items[_ix].ReferenceCoded = _refcodes;
            //end refcodes
            //there should be no more than 2 pallet detail sections, 1st for whole pallets, 2nd for part pallet
            //item pallet detail each pallet will have a pallet identifier SSCC code unless it' a 4 title delivery in which
            //case each pallet with have a single SSCC for the title e.g. if there's 2 pallets they would have the same SSCC
            //for testing generate a random pallet count for the 1st detail section, 2nd detail section should only have 1 pallet (the odds)
            PrintersDespatchAdviceItemDetailPalletDetail[] _pallets = new PrintersDespatchAdviceItemDetailPalletDetail[_details];
            for (int _nx = 0; _nx < _pallets.Length; _nx++)
            {
                //add to total pallets
                //enumerate through pallet detail sections add number of pallets in this section, etc
                int _nxpallets = _nx == 0 ? _numpallets : 1; //for testing
                _npallets += _nxpallets;
                _pallets[_nx] = new PrintersDespatchAdviceItemDetailPalletDetail();
                _pallets[_nx].NumberOfPallets = _nxpallets;
                _pallets[_nx].BooksPerPallet = _nx == 0 ? _copies[_ix] / _numpallets : _copies[_ix] % _numpallets;
                //need an SSCC code for EACH pallet
                _pallets[_nx].PalletIdentifierList = _nx == 0 ? _sscc1 : _sscc2;

                //parcel details
                _pallets[_nx].ParcelDetail = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetail();
                _pallets[_nx].ParcelDetail.NumberOfParcels = 1;
                _pallets[_nx].ParcelDetail.NumberOfOdds = 0;
                _pallets[_nx].ParcelDetail.BooksPerParcel = _nx == 0 ? (int)(_copies[_ix] / _nxpallets) : (int)(_copies[_ix] % _numpallets);
                //measurements for parcel
                string[] _parcelx = { "Height", "Width", "Depth", "UnitGrossWeight" };
                PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[] _parcelmeasures = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure[_parcelx.Length];
                for (int _px = 0; _px < _parcelx.Length; _px++)
                {
                    _parcelmeasures[_px] = new PrintersDespatchAdviceItemDetailPalletDetailParcelDetailMeasure();
                    _parcelmeasures[_px].MeasureTypeCode = _parcelx[_px];
                    _parcelmeasures[_px].MeasurementValue = 0;
                }
                //end parcel measurements
                _pallets[_nx].ParcelDetail.Measure = _parcelmeasures;
                //end parcel
            }
            _items[_ix].PalletDetail = _pallets;
            //end pallets
        }
        _pda.ItemDetail = _items;
        //end itemdatails

        //footer summary data
        _pda.Summary = new PrintersDespatchAdviceSummary();
        _pda.Summary.NumberOfLines = _nlines;
        _pda.Summary.NumberOfPallets = _npallets;
        _pda.Summary.NumberOfUnits = _nunits;
        //end pda processing

        //serialize to file using sytem.xml.serialization and sytem.io.streamwriter
        string _path = "c:\\ASNTEST\\" + orderno.ToString() + "_" + DateTime.Now.Second.ToString() + ".xml";
        System.Xml.Serialization.XmlSerializer _writer = new System.Xml.Serialization.XmlSerializer((typeof(PrintersDespatchAdvice)));
        //remove namespace references from output
        XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
        _ns.Add("", "");
        XmlWriterSettings _st = new XmlWriterSettings();
        //_st.NewLineOnAttributes = true; //not necessary forcing indent should be enough
        _st.Indent = true;
        _st.OmitXmlDeclaration = true; //remove declaration and create usng WriteRaw and WriteProcessingInstruction below so we can include standalone
        _st.Encoding = Encoding.UTF8;

        System.Xml.XmlWriter _file = System.Xml.XmlWriter.Create(_path, _st);
        //make sure the header lines are on multiple lines
        _file.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        _file.WriteRaw(Environment.NewLine);
        _file.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"CLDespatchAdvice-BIC.xsl\"");
        _file.WriteRaw(Environment.NewLine);
        //make sure standalone=true is included in xml declaration 
        //System.IO.StreamWriter _file = new System.IO.StreamWriter(_path);
        _writer.Serialize(_file, _pda, _ns);
        _result = true;

        return _result;
    }
    //end process asn
}
