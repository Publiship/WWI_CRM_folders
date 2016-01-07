using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization; 
using System.Web.Services;
using DAL.Logistics;
using DAL.Pricer;
using SubSonic;

namespace DAL.Services
{
    public struct uPriceParams
    { 
        public int quoteId;
        public string book_title;
        public int in_dimensions;
        public string in_currency;
        public string in_pallet;
        public double in_length;
        public double in_width;
        public double in_depth;
        public double in_weight;
        public double in_extent;
        public double in_papergsm;
        public bool in_hardback;
        public int copies_carton;
        public string origin_name;
        public string country_name;
        public string final_name;
        public int tot_copies;
    }

    public struct uPriceValues
    {
        public int quoteId;
        public double price_loose_gbp; //shipped as loose cost per copy
        public double price_pallet_gbp; //pre-palletised cost per copy
        public double price_total_gbp; //total price
        public double price_loose; //shipped as loose cost per copy
        public double price_pallet; //pre-palletised cost per copy
        public double price_total; //total price
        public double price_client; //price the client sees
        public string ship_via; //shipping via
        public string pallet_type; //pallet type
        public string loose_name; //shipped as loose description 
        //pre-palletised values 
        public string lcl_name; //lcl pre-palletised/loose description 
        public double lcl_v; //lcl
        public double lcl_v20; //lcl pre-palletised/loose 20
        public double lcl_v40; //lcl pre-palletised/loose 40
        public double lcl_v40hc; //lcl pre-palletised/loose 40 HC
        //shipped loose values 
        public string lcl_loose_name; //lcl loose description
        public double lcl_vloose; //lcl
        public double lcl_vloose20; //lcl loose 20
        public double lcl_vloose40; //lcl loose 40
        public double lcl_vloose40hc; //lcl loose 40 HC
        //output paper size and weight 
        public double out_length;
        public double out_width;
        public double out_depth;
        public double out_weight;
    }

    public struct uCostingSummary
    {
        public int quoteId;
        //pre-carriage
        public double pre_part;
        public double pre_full;
        public double pre_thc20;
        public double pre_thc40;
        public double pre_thclcl;
        public double pre_docs;
        public double pre_origin;
        public double pre_haul20;
        public double pre_haul40;
        //freight
        public double freight_lcl;
        public double freight_20;
        public double freight_40;
        public double freight_40hq;
        //on carriage
        public double on_dest_lcl;
        public double on_pier_etc;
        public double on_dest_20;
        public double on_dest_40;
        public double on_docs;
        public double on_customs;
        public double on_part;
        public double on_full;
        public double on_haul20;
        public double on_haul40;
        public double on_shunt20;
        public double on_shunt40;
        public double on_pallets;
        public double on_other;
    }

    public struct uShipmentSize
    {
        public int quoteId;
        //palletised
        public double tot_cartons;
        public double calc_copiescarton;
        public double pal_cartons;
        public double pal_full;
        public double pal_full_wt;
        public double pal_full_cu;
        public double pal_layers;
        public double pal_layer_count;
        public double pal_total_wt;
        public double pal_total_cu;
        public double pal_ratio;
        //carton
        public double ctn_hgt;
        public double ctn_len;
        public double ctn_wid;
        public double ctn_wt;
        //part pallets
        public double par_count;
        public double ctn_remaining;
        public double residue_cu;
        public double residue_wt;
        //carton totals
        public double ctn_total_wt;
        public double ctn_total_cu;
        public double ctn_ratio;
    }

    //270212 simplified structure to just return the prices and the original identifier used by the client
    //for e.g. GetQuoteCsv
    public struct uQuote
    {
        public string client_ref; //clients identifier
        public double price_ppc; //per copy price
        public double price_total; //total price
    }
    //public struct PriceRequest
    //{
    //    public int in_dimensions;
    //    public string in_currency;
    //    public string in_pallet;
    //    public string book_title;
    //    public double book_length;
    //    public double book_width;
    //    public double book_depth;
    //    public double book_weight;
    //    public int copies_carton;
    //    public string origin_name;
    //    public string final_name;
    //    public int tot_copies;
    //}

    [WebService(Namespace = "http://www.publiship.com/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // The following attribute allows the service to 
    // be called from script using ASP.NET AJAX.
    [System.Web.Script.Services.ScriptService]
    public class Publiship_Pricer : WebService
    {
        //declare structs here, don't pass as parameters memory overhead
        //16/06/2011 use DAL instead of local structs
        //private PriceRequest _pr;
        //private PriceValues _r;
        private PriceValue _pr = new PriceValue();
 
        public Publiship_Pricer()
        {
        }

        private void AssignBaseValues()
        {
            //default values
            // This is where you use your business components. 
            // Method calls on Business components are used to populate the data.
            // For demonstration purposes, I will add a string to the Code and
            //		use a random number generator to create the price feed.
            //Random RandomNumber = new System.Random();

            //_r.price_pallet = 0; //int.Parse(new System.Random(RandomNumber.Next(1, 10)).Next().ToString("##.##"));
            _pr.PriceLooseGbp = 0;

            _pr.PricePalletGbp = 0;
            _pr.PriceTotalGbp = 0;
            _pr.PriceLoose = 0;
            _pr.PricePallet = 0;
            _pr.PriceTotal = 0;
            _pr.ShipVia = "";
            _pr.PalletType = "";
            _pr.LooseName = "";
            _pr.LclName = "";
            _pr.LclV = 0;
            _pr.LclV20 = 0;
            _pr.LclV40 = 0;
            _pr.LclV40hc = 0;
            _pr.LclLooseName = "";
            _pr.LclVloose = 0;
            _pr.LclVloose20 = 0;
            _pr.LclVloose40 = 0;
            _pr.LclVloose40hc = 0;
            //output values for paper size and extent
            _pr.OutLength = 0;
            _pr.OutWidth = 0;
            _pr.OutDepth = 0;
            _pr.OutWeight = 0;
            _pr.PoLogId = 0;
            _pr.ClientVisible = true;
            _pr.CopyFromId = 0;
        }

        /// <summary>
        /// deprecated use QuoteToFormatStr
        /// </summary>
        /// <param name="_prices"></param>
        /// <returns></returns>
        private string ilistQuoteToJson(IList<uPriceValues> prices)
        {
            // Return JSON data
            string _json = string.Empty;
            
            JavaScriptSerializer _js = new JavaScriptSerializer();
            
            //don't need this just use library serializer
            //for (int _i = 0; _i < _prices.Count; _i++)
            //{
            //    _json += _js.Serialize(_prices[_i]);
            //}
            _json += _js.Serialize(prices);
            return _json;

            
        }
        //end quote to json

        [WebMethod(Description = "This method call can be used for testing for a web service response.", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetResponse(string Name)
        {
            return "Hello " + Name + ". The time is: " +
                 DateTime.Now.ToString();
        }
        //end hello user#

        [WebMethod(Description = "This method call will get a price per copy and total price for the given book details.", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetBookQuote(int Dimensions, string Currency, string Pallet, string Title, double Length, double Width, double Depth, double Weight, int Carton, string Origin, string Country, string Final_Destination, int Copies, Int32 cId, Int32 uId, Int32 qId)
        {
            //040712 has a pricer been passed back with pallet type?
            string _pricer = pricer_submitted(Pallet);
            string _pallet = wwi_func.get_string_element(Pallet, "&", 0); 

            AssignBaseValues();
                       
            //backup for form validation - make sure length > width and weight > 50 grams
            if (Length < Width)
            {
                double temp = Length;
                Length = Width;
                Width = temp;
            }

            if (Weight < 50) { Weight = 50; }

            _pr.RequestCompanyId = cId;
            _pr.RequestDate = DateTime.Now;
            _pr.RequestUserId = uId;
            _pr.RequestIp = wwi_func.user_RequestingIP();
            //   
            _pr.BookTitle = Title;
            _pr.InDimensions = Dimensions; 
            _pr.InCurrency  = Currency;
            _pr.InPallet = _pallet; //Pallet;
            _pr.InLength = Length;
            _pr.InWidth = Width;
            _pr.InDepth = Depth;
            _pr.InWeight = Weight;
            _pr.InExtent = 0;
            _pr.InPapergsm = 0;
            _pr.InHardback = false;
            _pr.CopiesCarton = Carton;
            _pr.OriginName = Origin;
            _pr.CountryName = Country; 
            _pr.FinalName = Final_Destination;
            _pr.TotCopies = Copies;

            //if a quoteid has been passed though it might be positive (a copy of a previous quote treat as new) or negative (re-doing a previous quote) 
            _pr.ClientVisible = qId == 0 ? true : qId > 0? true: false ;
            _pr.CopyFromId = Math.Abs(qId); 
 
            //dump to exel pricer and calculate
            submitPricerXl(Dimensions, _pricer,"");
            
            //16/06/2011 use DAL
            //use generics to build a list so we can return 1-n results if required
            //IList<PriceValues> _prices = new List<PriceValues>();
            Pricer.PriceValueCollection _prices = new PriceValueCollection();

            _prices.Add(_pr);
            
            //return ilistQuoteToJson(_prices);
            return QuoteToJson(_prices);
        }
        //end get book quote

        [WebMethod(Description = "This method call will get a price per copy and total price for the given carton details.", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetCartonQuote(int Dimensions, string Currency, string Pallet, string Title, double Length, double Width, double Depth, double Weight, int Carton, string Origin, string Country, string Final_Destination, int Copies, Int32 cId, Int32 uId, Int32 qId)
        {
            //040712 has a pricer been passed back with pallet type?
            string _pricer = pricer_submitted(Pallet);
            string _pallet = wwi_func.get_string_element(Pallet, "&", 0); 

            AssignBaseValues();
                      
            //backup for form validation - make sure length > width
            if (Length < Width)
            {
                double temp = Length;
                Length = Width;
                Width = temp;
            }

            _pr.RequestCompanyId = cId;
            _pr.RequestDate = DateTime.Now;
            _pr.RequestUserId = uId;
            _pr.RequestIp = wwi_func.user_RequestingIP();
            //   
            _pr.BookTitle = Title; 
            _pr.InDimensions = Dimensions;
            _pr.InCurrency = Currency;
            _pr.InPallet = _pallet; //Pallet;
            _pr.InLength = Length;
            _pr.InWidth = Width;
            _pr.InDepth = Depth;
            _pr.InWeight = Weight;
            _pr.InExtent = 0;
            _pr.InPapergsm = 0;
            _pr.InHardback = false;
            _pr.CopiesCarton = Carton;
            _pr.OriginName = Origin;
            _pr.CountryName = Country; 
            _pr.FinalName = Final_Destination;
            _pr.TotCopies = Copies;

            //if a quoteid has been passed though it might be positive (a copy of a previous quote treat as new) or negative (re-doing a previous quote) 
            _pr.ClientVisible = qId == 0 ? true : qId > 0 ? true : false;
            _pr.CopyFromId = Math.Abs(qId);
            //dump to exel pricer and calculate
            submitPricerXl(Dimensions, _pricer,"");

            //16/06/2011 use DAL
            //use generics to build a list so we can return 1-n results if required
            //IList<PriceValues> _prices = new List<PriceValues>();
            Pricer.PriceValueCollection _prices = new PriceValueCollection();

            _prices.Add(_pr);

            //return ilistQuoteToJson(_prices);
            return QuoteToJson(_prices);
        }
        //end get carton quote

        [WebMethod(Description = "This method call will get a price per copy and total price for the given paper size and extent.", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetPaperQuote(int Dimensions, string Currency, string Pallet, string Title, double Length, double Width, double Extent, double PaperGsm, bool Hardback, string Origin, string Country, string Final_Destination, int Copies, Int32 cId, Int32 uId, Int32 qId, string testGsm)
        {
            //040712 has a pricer been passed back with pallet type?
            string _pricer = pricer_submitted(Pallet);
            string _pallet = wwi_func.get_string_element(Pallet, "&", 0); 
                        
            AssignBaseValues();

            _pr.RequestCompanyId = cId;
            _pr.RequestDate = DateTime.Now;
            _pr.RequestUserId = uId;
            _pr.RequestIp = wwi_func.user_RequestingIP();
            //   
            _pr.BookTitle = Title;
            _pr.InDimensions = Dimensions;
            _pr.InCurrency = Currency;
            _pr.InPallet = _pallet; //Pallet;
            _pr.InLength = Length;
            _pr.InWidth = Width;
            _pr.InDepth = 0;
            _pr.InWeight = 0;
            _pr.CopiesCarton = 0;
            _pr.InExtent = Extent;
            _pr.InPapergsm = PaperGsm;
            _pr.InHardback = Hardback;
            _pr.OriginName = Origin;
            _pr.CountryName = Country;
            _pr.FinalName = Final_Destination;
            _pr.TotCopies = Copies;

            //if a quoteid has been passed though it might be positive (a copy of a previous quote treat as new) or negative (re-doing a previous quote) 
            _pr.ClientVisible = qId == 0 ? true : qId > 0 ? true : false;
            _pr.CopyFromId = Math.Abs(qId); 
            //dump to exel pricer and calculate
            submitPricerXl(Dimensions, _pricer, testGsm);

            //16/06/2011 use DAL
            //use generics to build a list so we can return 1-n results if required
            //IList<PriceValues> _prices = new List<PriceValues>();
            Pricer.PriceValueCollection _prices = new PriceValueCollection();

            _prices.Add(_pr);

            //return ilistQuoteToJson(_prices);
            return QuoteToJson(_prices);
        }
        //end get paper quote

        [WebMethod(Description = "This method call will get a price per copy and total price for the given dimensions returning  a csv string", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetQuoteCsv(int Dimensions, string Currency, string Pallet, string Title, double Length, double Width, double depthOrExtent, double gsmOrWeight, int carton, bool Hardback, string Origin, string Country, string Final_Destination, int Copies, string reference, string userName, string passWord)
        {
            //validate user
            string _result = "";
            string _user = wwi_security.getuserIds(userName, passWord);

            if (!string.IsNullOrEmpty(_user))
            {
                AssignBaseValues();
                string[] _ids = _user.Split("#".ToCharArray());
                int _cid = wwi_func.vint(_ids[0]); //companyid
                //040712 has a pricer been passed back with pallet type?
                string _pricer = pricer_submitted(Pallet);
                string _pallet = wwi_func.get_string_element(Pallet, "&", 0); 

                //backup for form validation - make sure length > width
                if (Length < Width)
                {
                    double temp = Length;
                    Length = Width;
                    Width = temp;
                }

                _pr.RequestCompanyId = _cid;
                _pr.RequestUserId = wwi_func.vint(_ids[1]);
                _pr.RequestDate = DateTime.Now;
                _pr.RequestIp = wwi_func.user_RequestingIP();

                //   
                _pr.BookTitle = Title;
                _pr.InDimensions = Dimensions;
                _pr.InCurrency = Currency;
                _pr.InPallet = _pallet; //Pallet;
                _pr.InLength = Length;
                _pr.InWidth = Width;
                _pr.OriginName = Origin;
                _pr.CountryName = Country;
                _pr.FinalName = Final_Destination;
                _pr.TotCopies = Copies;

                if (Dimensions != 3) //3= paper quote
                {
                    _pr.CopiesCarton = carton;
                    _pr.InDepth = depthOrExtent;
                    _pr.InWeight = gsmOrWeight;
                    _pr.InExtent = 0;
                    _pr.InPapergsm = 0;
                    _pr.InHardback = false;
                }
                else
                {
                    _pr.CopiesCarton = 0;
                    _pr.InDepth = 0;
                    _pr.InWeight = 0;
                    _pr.InExtent = depthOrExtent;
                    _pr.InPapergsm = gsmOrWeight;
                    _pr.InHardback = Hardback;
                }

                //does not apply to this service
                _pr.ClientVisible = false;
                _pr.CopyFromId = 0;
                //dump to exel pricer and calculate
                submitPricerXl(Dimensions, _pricer, "");

                //use generics to build a list so we can return 1-n results if required
                //IList<uPriceValues> _prices = new List<uPriceValues>();
                //_prices.Add(_pr);

                _result = reference + "," + _pr.PriceClient + "," + _pr.PriceTotal + Environment.NewLine;
            }
            else
            {
                _result = reference + "," + "Login" + "," + "Failed" + Environment.NewLine;
            }
            return _result;
        }
        //end get quote csv
        [WebMethod(Description = "This method call will get a price per copy and total price for the given dimensions sent in csv format returning 1 price for each csv record", EnableSession = false)]
        //public PriceInfo GetPriceInfo(string Title, double Length, double Width, double Depth, double Weight, string Origin, string Final_Destination, int Copies)
        public string GetBatchQuoteCsv(string batchfile, string userName, string passWord)
        {
            string _result = "";

            //parse csv and get a price for each record
            string _user = wwi_security.getuserIds(userName, passWord);

            if (!string.IsNullOrEmpty(_user))
            {
                string[] _ids = _user.Split("#".ToCharArray());

                //get records and step through
                
                AssignBaseValues();
            }

            return _result; 
        }
        //end get bactch quote csv

        [WebMethod(Description = "This method call will get the quote criteria for a given quote number", EnableSession = false)]
        public string GetQuoteCriteria(int quoteid)
        {
            string _cs = string.Empty;

            if (quoteid > 0)
            {
                _cs = QuoteCriteriaToJson(quoteid);
            }
            else
            {
                _cs = "You do not have a Quote Number";
            }
            return _cs;
        }
        //end get quote criteria 

        [WebMethod(Description = "This method call will get the costing summary for a given quote number", EnableSession = false)]
        public string GetCostingSummary(int quoteid, string costingtype)
        {
            string _cs = string.Empty;

                if (quoteid > 0)
                {
                    _cs = CostingToJson(quoteid, costingtype);
                }
                else
                {
                    _cs = "You do not have a Quote Number";
                }
            return _cs;
        }
        //end get costing 


        [WebMethod(Description = "This method call will get the shipment size for a given quote number", EnableSession = false)]
        public string GetShipmentSize(int quoteid)
        {
            string _cs = string.Empty;

                if (quoteid > 0)
                {
                    _cs = ShipmentToJson(quoteid);
                }
                else
                {
                    _cs = "You do not have a Quote Number";
                }
            return _cs;
        }
        //end get shipment size 
        
        protected void submitPricerXl(int input_type, string pricerfile, string testGsm)
        {
            //open file
            string _dir = "~/Include/";
            string _cg = "";
            string _source = "";
            //string _file  = "C:\\Documents and Settings\\pauledwards\\My Documents\\Work\\Publiship\\OfficePricerQ211_2.xls"; //Server.MapPath();
            
            //*****
            //100212 check against company id, if no file just use "officepricer" prefix
            //040712 use the submited spreadsheet if there is one (publiship users only)
            if (string.IsNullOrEmpty(pricerfile))
            {
                namecustomcontroller _name = new namecustomcontroller();
                _cg = _name.get_company_group((int)_pr.RequestCompanyId);
                _source = _cg != "0" ? wwi_file.get_latest_pricer(_dir, _cg) : wwi_file.get_latest_pricer(_dir); //find latest upload of pricer 
            }
            else
            {
                _source = pricerfile;  
            }
            //****
            //**** 230713 added a randon number to file name
            string _random = new Random(1).Next(1,100).ToString(); 
            string _copy = _pr.RequestUserId.ToString() + "_" + _random + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".xls";


            wwi_file.fso_copy_file(_dir + _source, _dir + _copy);

            string _file = Server.MapPath(_dir + _copy);
            SpreadsheetGear.IWorkbook _wb = SpreadsheetGear.Factory.GetWorkbook(_file);
            
            //disable password protection for now 
            _wb.Unprotect("Trueblue");
            //do not use this - it seems to be for winforms only
            //_wb.WorkbookSet.GetLock(); //acquire lock
            
            try
            {
                //10/08/2011 only need 1st part of currency string for workbook, leave off e.g. GBP, USD or calculation will fail
                string _longcurrency = _pr.InCurrency.Split(',').GetValue(0).ToString().Trim();
                bool _isHCP = _source.Contains("HCP") ? true : false;
                //120312 deprecated code as we are using the custom pricers anyway
                //if (_cg != "0")
                //{
                //    //get rates from spreadsheet and update with special rates if necessary
                //    SpreadsheetGear.IRange _range = _wb.Worksheets["sheet1"].Range["z241:ab286"]; //is this the right range?
                //    //SpreadsheetGear.IRange _range = _wb.Worksheets["sheet1"].Range["r404:v448"]; //or this?
                //    DataTable _rates = _range.GetDataTable(SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders);
                //
                //
                //    string[] _cols = { "NameAndAddressBook.CompanyId", "pricer_company_rates.zone_name", "pricer_company_rates.zone_rate", "pricer_company_rates.pricer_group_id" }; //MUST have defined columns or datareader will not work!
                //    SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).InnerJoin(DAL.Logistics.PricerCompanyRate.PricerGroupIdColumn, DAL.Logistics.NameAndAddressBook.PricerGroupColumn).Where("CompanyID").IsEqualTo(_pr.RequestCompanyId);
                //    IDataReader _rd = _query.ExecuteReader();
                //    if (_rd != null)
                //    {
                //        while (_rd.Read())
                //        {
                //
                //            string _zone = _rd[1].ToString();
                //            DataRow[] _found = _rates.Select("column1 = '" + _zone + "'");
                //            if (_found.Length == 1) //only update if one match is found
                //            {
                //                string _rate = _rd[2].ToString();
                //                _found[0][1] = _rate; //set rate in datatable for Range["z241:ab286"]
                //                //_found[0][4] = _rate; //set rate in datatable for Range["r404:v448"] 
                //            }
                //        }
                //    }
                //    // Get the top left cell for the DataTable
                //    _range = _wb.Worksheets["sheet1"].Range["z241"];
                //    _range.CopyFromDataTable(_rates, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                //}
                //end get rates

                    
                //reset input cells to 0 and save
                resetPricerXl(_wb, _isHCP);
                
                //same for all formats
                SpreadsheetGear.IRange _curr = _wb.Worksheets["sheet1"].Cells["a17"];
                SpreadsheetGear.IRange _pall = _wb.Worksheets["sheet1"].Cells["g33"];
                SpreadsheetGear.IRange _typeof = _wb.Worksheets["uploaded"].Cells["b6"];
                //important - don't forget origin/final destination as this can modify prices
                SpreadsheetGear.IRange _origin = _wb.Worksheets["uploaded"].Cells["l6"];
                SpreadsheetGear.IRange _dest = _wb.Worksheets["uploaded"].Cells["n6"];
                SpreadsheetGear.IRange _country = _wb.Worksheets["uploaded"].Cells["o6"]; 
                SpreadsheetGear.IRange _copies = _wb.Worksheets["uploaded"].Cells["p6"];
                SpreadsheetGear.IRange _curr2 = _wb.Worksheets["uploaded"].Cells["q6"];

                input_type = Math.Abs(input_type);
                //get target cells depending on input type
                if (input_type == 1) //book
                {   
                    SpreadsheetGear.IRange _blen = _wb.Worksheets["uploaded"].Cells["c6"];
                    SpreadsheetGear.IRange _bwid = _wb.Worksheets["uploaded"].Cells["d6"];
                    SpreadsheetGear.IRange _bdep = _wb.Worksheets["uploaded"].Cells["e6"];
                    SpreadsheetGear.IRange _bwgt = _wb.Worksheets["uploaded"].Cells["f6"];
                    SpreadsheetGear.IRange _carton = _wb.Worksheets["uploaded"].Cells["k6"];
                    
                    //set values of target cells
                    _typeof.Value = 1;  //_pr.InDimensions;
                    _curr.Value = _longcurrency; //_pr.InCurrency;
                    _curr2.Value = _longcurrency; //_pr.InCurrency;
                    _pall.Value = _pr.InPallet; 
                    _blen.Value = _pr.InLength;
                    _bwid.Value = _pr.InWidth;
                    _bdep.Value = _pr.InDepth;
                    _bwgt.Value = _pr.InWeight;
                    _copies.Value = _pr.TotCopies;
                    _carton.Value = _pr.CopiesCarton;
                    _origin.Value = _pr.OriginName;
                    _country.Value = _pr.CountryName; 
                    _dest.Value = _pr.FinalName;
                }
                else if (input_type == 2) //carton
                {
                    SpreadsheetGear.IRange _blen = _wb.Worksheets["uploaded"].Cells["g6"];
                    SpreadsheetGear.IRange _bwid = _wb.Worksheets["uploaded"].Cells["h6"];
                    SpreadsheetGear.IRange _bdep = _wb.Worksheets["uploaded"].Cells["i6"];
                    SpreadsheetGear.IRange _bwgt = _wb.Worksheets["uploaded"].Cells["j6"];
                    SpreadsheetGear.IRange _carton = _wb.Worksheets["uploaded"].Cells["k6"];
                    
                    //set values of target cells
                    _typeof.Value = 2;  //_pr.InDimensions;
                    _curr.Value = _longcurrency; //_pr.InCurrency;
                    _curr2.Value = _longcurrency; //_pr.InCurrency;
                    _pall.Value = _pr.InPallet; 
                    _blen.Value = _pr.InLength;
                    _bwid.Value = _pr.InWidth;
                    _bdep.Value = _pr.InDepth;
                    _bwgt.Value = _pr.InWeight;
                    _copies.Value = _pr.TotCopies;
                    _carton.Value = _pr.CopiesCarton;
                    _origin.Value = _pr.OriginName;
                    _country.Value = _pr.CountryName; 
                    _dest.Value = _pr.FinalName;
                }
                else //paper size and extent
                {
                    SpreadsheetGear.IRange _plen = _wb.Worksheets["sheet1"].Cells["c70"]; 
                    SpreadsheetGear.IRange _pwid = _wb.Worksheets["sheet1"].Cells["c71"];
                    SpreadsheetGear.IRange _pext = _wb.Worksheets["sheet1"].Cells["c72"];
                    SpreadsheetGear.IRange _pgsm =  _isHCP == true? _wb.Worksheets["sheet1"].Cells["a92"]: _wb.Worksheets["sheet1"].Cells["a86"];
                    SpreadsheetGear.IRange _hbak = _wb.Worksheets["sheet1"].Cells["c73"];
                    SpreadsheetGear.IRange _carton = _wb.Worksheets["uploaded"].Cells["k6"];

                    //set values of target cells
                    _typeof.Value = 1; //_pr.InDimensions;
                    _curr.Value = _longcurrency;  //_pr.InCurrency;
                    _curr2.Value = _longcurrency; //_pr.InCurrency;
                    _pall.Value = _pr.InPallet; 
                    _plen.Value = _pr.InLength;
                    _pwid.Value = _pr.InWidth;
                    _pext.Value = _pr.InExtent;
                    _pgsm.Value = testGsm; //_pr.InPapergsm;
                    _hbak.Value = _pr.InHardback;
                    _copies.Value = _pr.TotCopies;
                    _origin.Value = _pr.OriginName;
                    _country.Value = _pr.CountryName; 
                    _dest.Value = _pr.FinalName;
                    _carton.Value  = 0; //does not apply to paper size, it gets calculated from dimensions
                }

                //reset formulas
                //bool _setloose = true;
                //if (_pr.CountryName == "India" || _pr.CountryName == "China" || _pr.CountryName == "Taiwan" || _pr.CountryName == "Thailand") { _setloose = false; }
                //resetFormulas(_wb, input_type, _setloose, _pr.OriginName);
                
                resetFormulas(_wb, input_type, _pr.OriginName, _isHCP);

                //force calculation
                _wb.Save();
                _wb.WorkbookSet.BackgroundCalculation = true;
                _wb.WorkbookSet.Calculation = SpreadsheetGear.Calculation.Automatic;
                _wb.WorkbookSet.CalculateFull();

                //testing 
                //string _testing = "";
                //_testing = _wb.Worksheets["sheet1"].Cells["a93"].Value.ToString(); 
                //_testing = _wb.Worksheets["sheet1"].Cells["b93"].Formula.ToString(); 
                //_testing = _wb.Worksheets["sheet1"].Cells["b93"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d85"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d86"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d88"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d89"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d90"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d91"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d92"].Value.ToString();
                //_testing = _wb.Worksheets["sheet1"].Cells["d93"].Value.ToString();
                //
                //
                //_testing = _wb.Worksheets["uploaded"].Cells["c6"].Value.ToString();
                //_testing = _wb.Worksheets["uploaded"].Cells["d6"].Value.ToString();
                //_testing = _wb.Worksheets["uploaded"].Cells["e6"].Value.ToString();
                //_testing = _wb.Worksheets["uploaded"].Cells["f6"].Value.ToString();

                //book prices to struct
                //more detail
                _pr.ShipVia = _wb.Worksheets["sheet1"].Cells["e32"].Value != null? _wb.Worksheets["sheet1"].Cells["e32"].Value.ToString(): ""; //shipping via
                _pr.PalletType =_wb.Worksheets["sheet1"].Cells["f35"].Value != null?  _wb.Worksheets["sheet1"].Cells["f35"].Value.ToString(): ""; //pallet type
                //
                _pr.LooseName = _wb.Worksheets["prices"].Cells["e194"].Value != null? _wb.Worksheets["prices"].Cells["e194"].Value.ToString(): ""; //shipped as loose description 
                _pr.LclName = _wb.Worksheets["prices"].Cells["j206"].Value != null? _wb.Worksheets["prices"].Cells["j206"].Value.ToString():""; //lcl pre-palletised description 
                //
                _pr.LclV = dbl(_wb.Worksheets["prices"].Cells["r206"].Value.ToString()); //lcl
                _pr.LclV20 = dbl(_wb.Worksheets["prices"].Cells["s206"].Value.ToString()); //lcl pre-palletised 20
                _pr.LclV40 = dbl(_wb.Worksheets["prices"].Cells["t206"].Value.ToString()); //lcl pre-palletised 40
                _pr.LclV40hc = dbl(_wb.Worksheets["prices"].Cells["u206"].Value.ToString()); //lcl pre-palletised 40 HC
                //
                _pr.LclLooseName = _wb.Worksheets["prices"].Cells["j217"].Value != null? _wb.Worksheets["prices"].Cells["j217"].Value.ToString(): ""; //lcl loose description
                _pr.LclVloose = dbl(_wb.Worksheets["prices"].Cells["r217"].Value.ToString()); //lcl
                _pr.LclVloose20 = dbl(_wb.Worksheets["prices"].Cells["s217"].Value.ToString()); //lcl loose 20
                _pr.LclVloose40 = dbl(_wb.Worksheets["prices"].Cells["t217"].Value.ToString()); //lcl loose 40
                _pr.LclVloose40hc = dbl(_wb.Worksheets["prices"].Cells["u217"].Value.ToString()); //lcl loose 40 HC
                //
                //_pr.PriceLooseGbp = dbl(_wb.Worksheets["prices"].Cells["g224"].Value.ToString()); //shipped as loose cost per copy
                _pr.PriceLooseGbp = dbl(_wb.Worksheets["prices"].Cells["g227"].Value.ToString()); //110811 per copy price regardless of whether shipped loose or palletised (also look at k228 concat)
                _pr.PricePalletGbp  = dbl(_wb.Worksheets["prices"].Cells["f227"].Value.ToString()); //pre-palletised cost per copy
                _pr.PriceTotalGbp = (_pr.PriceLooseGbp / 100) * _pr.TotCopies;  //total price
                //120811 for now we just use converted results straignt off spreadsheet 
                //will activate daily conversion rates when everyone is used to web based system 
                //remember to use UNCONVERTED prices when we switch over
                //get rate and convert to selected currency
                //_pr.PriceEx = _pr.InCurrency != "Sterling (pence),GBP" ? wwi_func.getexchangerate("GBP", _pr.InCurrency.Split(',').GetValue(1).ToString().Trim()) : 1;
                //110811 priceloose = per copy price regardless of whether shipped loose or palletised (also look at k228 concat)

                _pr.PriceLoose = dbl(_wb.Worksheets["prices"].Cells["g224"].Value.ToString());      //_pr.PriceLooseGbp * _pr.PriceEx; g230
                _pr.PricePallet = dbl(_wb.Worksheets["prices"].Cells["g228"].Value.ToString());    //_pr.PricePalletGbp * _pr.PriceEx;

                //have to do this here - can't just get value from g230 as the formula sometimes isn't there?
                string _test = _wb.Worksheets["sheet1"].Cells["d34"].Value.ToString();

                if (_test == "3")
                {
                    _pr.PriceClient = _pr.PriceLoose;
                }
                else
                {
                    _pr.PriceClient = _pr.PriceLoose == 0 ? _pr.PricePallet : _pr.PriceLoose < _pr.PricePallet ? _pr.PriceLoose : _pr.PricePallet;
                }
                //switch(_test){
                //    case "1"://show best of prices to client
                //        {
                //            _pr.PriceClient = _pr.PriceLoose == 0 ? _pr.PricePallet : _pr.PriceLoose < _pr.PricePallet ? _pr.PriceLoose : _pr.PricePallet;
                //            break;
                //        }
                //    case "2": //show palletised only
                //        {
                //             _pr.PriceClient = _pr.PricePallet;
                //            break;
                //        }
                //    case "3": //show loose only
                //        {
                //             _pr.PriceClient = _pr.PriceLoose;
                //            break;
                //        }
                //    default:
                //        {
                //            _pr.PriceClient = _pr.PriceLoose == 0 ? _pr.PricePallet : _pr.PriceLoose < _pr.PricePallet ? _pr.PriceLoose : _pr.PricePallet;
                //            break;
                //        }
                //}
               
                
                _pr.PriceTotal = (_pr.PriceClient / 100) * _pr.TotCopies;
    
                //paper sizes which might be different from input dimensions
                if (input_type == 3)
                {
                    _pr.OutLength = dbl(_wb.Worksheets["uploaded"].Cells["c6"].Value.ToString());
                    _pr.OutWidth = dbl(_wb.Worksheets["uploaded"].Cells["d6"].Value.ToString());
                    _pr.OutDepth = dbl(_wb.Worksheets["uploaded"].Cells["e6"].Value.ToString());
                    _pr.OutWeight = dbl(_wb.Worksheets["uploaded"].Cells["f6"].Value.ToString());
                }
                else
                {
                    _pr.OutLength = _pr.InLength;
                    _pr.OutWidth = _pr.InWidth;
                    _pr.OutDepth = _pr.InDepth;
                    _pr.OutWeight = _pr.InWeight;
                }
                
                //060712 save pricer spreadsheet used
                _pr.SpreadsheetUsed = _source.Length > 75? _source.Substring(0, 75): _source;
                //save to database and get id for child records. We don't return this as part of the JSON string unless it has been
                //requested but we save it now 
                _pr.Save();
                //return id number
                Int32 _newid = (Int32)_pr.GetPrimaryKeyValue();
                //save summary info
                //get the costing summaries - pre-palletised and loose
                SpreadsheetGear.IWorksheet _wcs = _wb.Worksheets["pricessummary"];
                Pricer.CostingSummaryCollection _cs = new Pricer.CostingSummaryCollection();
                
                for (int _ix = 0; _ix <= 1; _ix++)
                {
                    //summaries are on different rows of spreadsheet but same column refs
                    int _rw = _ix == 0 ? 71 : 81;
                    Pricer.CostingSummary _c = new Pricer.CostingSummary();
                    //pre-palletised
                    _c.QuoteId = _newid;
                    _c.SummaryType = _ix == 0 ? "pre-palletised" : "loose";
                    _c.PrePart = dbl(_wcs.Cells["m" + _rw].Value.ToString()); 
                    _c.PreFull = dbl(_wcs.Cells["n" + _rw].Value.ToString()); 
                    _c.PreThc20 = dbl(_wcs.Cells["o" + _rw].Value.ToString()); 
                    _c.PreThc40 = dbl(_wcs.Cells["p" + _rw].Value.ToString()); 
                    _c.PreThclcl = dbl(_wcs.Cells["q" + _rw].Value.ToString()); 
                    _c.PreDocs = dbl(_wcs.Cells["r" + _rw].Value.ToString());
                    _c.PreOrigin = dbl(_wcs.Cells["s" + _rw].Value.ToString()); 
                    _c.PreHaul20 = dbl(_wcs.Cells["t" + _rw].Value.ToString()); 
                    _c.PreHaul40 = dbl(_wcs.Cells["u" + _rw].Value.ToString()); 
                    _c.FreightLcl = dbl(_wcs.Cells["v" + _rw].Value.ToString()); 
                    _c.Freight20 = dbl(_wcs.Cells["w" + _rw].Value.ToString()); 
                    _c.Freight40 = dbl(_wcs.Cells["x" + _rw].Value.ToString());
                    _c.Freight40hq = dbl(_wcs.Cells["y" + _rw].Value.ToString()); 
                    _c.OnDestLcl = dbl(_wcs.Cells["z" + _rw].Value.ToString()); 
                    _c.OnPierEtc = dbl(_wcs.Cells["aa" + _rw].Value.ToString()); 
                    _c.OnDest20 = dbl(_wcs.Cells["ab" + _rw].Value.ToString()); 
                    _c.OnDest40 = dbl(_wcs.Cells["ac" + _rw].Value.ToString()); 
                    _c.OnDocs = dbl(_wcs.Cells["ad" + _rw].Value.ToString()); 
                    _c.OnCustoms = dbl(_wcs.Cells["ae" + _rw].Value.ToString()); 
                    _c.OnPart = dbl(_wcs.Cells["af" + _rw].Value.ToString()); 
                    _c.OnFull = dbl(_wcs.Cells["ag" + _rw].Value.ToString()); 
                    _c.OnHaul20 = dbl(_wcs.Cells["ah" + _rw].Value.ToString()); 
                    _c.OnHaul40 = dbl(_wcs.Cells["ai" + _rw].Value.ToString()); 
                    _c.OnShunt20 = dbl(_wcs.Cells["aj" + _rw].Value.ToString()); 
                    _c.OnShunt40 = dbl(_wcs.Cells["ak" + _rw].Value.ToString()); 
                    _c.OnPallets = dbl(_wcs.Cells["al" + _rw].Value.ToString()); 
                    _c.OnOther = dbl(_wcs.Cells["am" + _rw].Value.ToString()); 
                    //add to collection
                    _cs.Add(_c); 
                }
                
                //shipment sizes
                SpreadsheetGear.IWorksheet _wsz = _wb.Worksheets["shipmentsummary"];
                Pricer.ShipmentSize _sz = new Pricer.ShipmentSize();
                _sz.QuoteId = _newid;
                _sz.CalcCopiescarton = dbl(_wsz.Cells["d3"].Value.ToString());
                _sz.TotCartons = dbl(_wsz.Cells["d5"].Value.ToString());
                _sz.PalCartons = dbl(_wsz.Cells["d7"].Value.ToString());
                _sz.PalFull = dbl(_wsz.Cells["d9"].Value.ToString());
                _sz.PalFullWt = dbl(_wsz.Cells["d11"].Value.ToString());
                _sz.PalFullCu = dbl(_wsz.Cells["d13"].Value.ToString());
                _sz.PalLayers = dbl(_wsz.Cells["d15"].Value.ToString());
                _sz.PalLayerCount = dbl(_wsz.Cells["d17"].Value.ToString());
                _sz.PalTotalWt = dbl(_wsz.Cells["d19"].Value.ToString());
                _sz.PalTotalCu = dbl(_wsz.Cells["d21"].Value.ToString());
                _sz.PalRatio = dbl(_wsz.Cells["d23"].Value.ToString());
                //carton
                _sz.CtnHgt = dbl(_wsz.Cells["i3"].Value.ToString());
                _sz.CtnLen = dbl(_wsz.Cells["i5"].Value.ToString());
                _sz.CtnWid = dbl(_wsz.Cells["i7"].Value.ToString());
                _sz.CtnWt = dbl(_wsz.Cells["i9"].Value.ToString());
                //part pallets
                _sz.ParCount = dbl(_wsz.Cells["i11"].Value.ToString());
                _sz.CtnRemaining = dbl(_wsz.Cells["i13"].Value.ToString());
                _sz.ResidueCu = dbl(_wsz.Cells["i15"].Value.ToString());
                _sz.ResidueWt = dbl(_wsz.Cells["i17"].Value.ToString());
                //carton totals
                _sz.CtnTotalWt = dbl(_wsz.Cells["i19"].Value.ToString());
                _sz.CtnTotalCu = dbl(_wsz.Cells["i21"].Value.ToString());
                _sz.CtnRatio = dbl(_wsz.Cells["i23"].Value.ToString());

                //save costing summaries
                _cs.SaveAll();
                //save shipment size
                _sz.Save();
                //end save summary info

                //**************
                //for testing
                //**************
                //string _t =  send_error_email("This is the error");
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();

                if (send_error_email(_ex) != null)
                {
                    _ex += " This problem has been reported to Publiship.";
                }
                _pr.LclName = "exception";
                _pr.LclLooseName = _ex;
                _pr.PriceTotal = -1;
   
            }
            finally
            {
                //reset protection
                _wb.Protect("Trueblue", true, true);
                //_wb.WorkbookSet.ReleaseLock();
                _wb.Close(); 
                //delete temporary copy
                wwi_file.fso_kill_file(_dir + _copy); 
            }
        }
        

        //end submitpricerxl
        protected string send_error_email(string err)
        {
            string _mailed = null;
            string _tr = "<tr><td bgcolor=\"#e8edff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";
            string _tbl = "<p><table cellpadding=\"5px\" style=\"border-color: #669\">{0}</table></p><p>{1}</p>";
            
            string _msg = String.Format(_tr, "User Id: ", _pr.RequestCompanyId.ToString()) +
                                String.Format(_tr, "Company Id: ", _pr.RequestUserId.ToString()) +
                                String.Format(_tr, "Request date: ", _pr.RequestDate.ToString()) +
                                String.Format(_tr, "Title: ", _pr.BookTitle.ToString()) +
                                String.Format(_tr, "Dimensions: ", _pr.InDimensions.ToString()) +
                                String.Format(_tr, "Currency: ", _pr.InCurrency.ToString()) +
                                String.Format(_tr, "Pallet type: ", _pr.InPallet.ToString()) +
                                String.Format(_tr, "Length: ", _pr.InLength .ToString()) +
                                String.Format(_tr, "Width: ", _pr.InWidth .ToString()) +
                                String.Format(_tr, "Depth: ", _pr.InDepth.ToString()) +
                                String.Format(_tr, "Weight: ", _pr.InWeight.ToString()) +
                                String.Format(_tr, "Copies carton: ", _pr.CopiesCarton.ToString()) +
                                String.Format(_tr, "Paper extent: ", _pr.InExtent.ToString()) +
                                String.Format(_tr, "Paper GSM: ", _pr.InPapergsm.ToString()) +
                                String.Format(_tr, "Hardback: ", _pr.InHardback.ToString()) +
                                String.Format(_tr, "Origin: ", _pr.OriginName.ToString()) +
                                String.Format(_tr, "Destination country: ", _pr.CountryName.ToString()) +
                                String.Format(_tr, "Final destination: ", _pr.FinalName .ToString()) +
                                String.Format(_tr, "Total copies: ",_pr.TotCopies.ToString());

            string[] _to = { "services@publiship-online.com", "paule@publiship.com" };
            _mailed = MailHelper.gen_email(_to, true, "Online pricer error report", String.Format(_tbl, _msg, err), "");

            return _mailed;
        }
       
        /// <summary>
        /// rset cell values to 0 for all input types
        /// </summary>
        /// <param name="wb">spreadsheetgear workbook</param>
        protected void resetPricerXl(SpreadsheetGear.IWorkbook wb, bool isHCP)
        {
            
            //reset values of target cells
            wb.Worksheets["sheet1"].Cells["a17"].Range.Value = "Sterling (pence)"; //currency
            wb.Worksheets["sheet1"].Cells["g33"].Range.Value = "Standard"; //pallet
            wb.Worksheets["uploaded"].Cells["b6"].Range.Value = ""; //input type
            wb.Worksheets["uploaded"].Cells["p6"].Range.Value = 0 ; //copies
            wb.Worksheets["uploaded"].Cells["k6"].Range.Value = 0; //copies per carton

            //book prices
            wb.Worksheets["uploaded"].Cells["c6"].Range.Value = 0; //length
            wb.Worksheets["uploaded"].Cells["d6"].Range.Value = 0; //width
            wb.Worksheets["uploaded"].Cells["e6"].Range.Value = 0; //depth
            wb.Worksheets["uploaded"].Cells["f6"].Range.Value = 0; //weight
            //carton
            wb.Worksheets["uploaded"].Cells["g6"].Range.Value = 0; //length
            wb.Worksheets["uploaded"].Cells["h6"].Range.Value = 0; //width
            wb.Worksheets["uploaded"].Cells["i6"].Range.Value = 0; //depth
            wb.Worksheets["uploaded"].Cells["j6"].Range.Value = 0; //weight

            //paper size and extent
            wb.Worksheets["sheet1"].Cells["c70"].Range.Value = 0; //len
            wb.Worksheets["sheet1"].Cells["c71"].Range.Value = 0; //width
            wb.Worksheets["sheet1"].Cells["c72"].Range.Value = 0; //paper size
            //don't do this as paper gsm might not be numeric
            //check this as it might by a86 or b93 depending on pricer being used!
            //if (isHCP)
            //{
            //    wb.Worksheets["sheet1"].Cells["a93"].Range.Value = 0; //paper gsm
            //}
            //else
            //{
            //    wb.Worksheets["sheet1"].Cells["a86"].Range.Value = 0; //paper gsm
            //}
            wb.Worksheets["sheet1"].Cells["c73"].Range.Value = 0; //hardback

            //country
            wb.Worksheets["uploaded"].Cells["l6"].Range.Value = ""; //origin
            wb.Worksheets["uploaded"].Cells["n6"].Range.Value = ""; //destination country
            wb.Worksheets["uploaded"].Cells["o6"].Range.Value = ""; //final dest 

            //wb.Save();
        }
        //end reset pricer

        /// <summary>
        /// reset r1c1 formulas across all sheets
        /// </summary>
        //protected void resetFormulas(SpreadsheetGear.IWorkbook wb, int intype, bool setloose, string origin)
        protected void resetFormulas(SpreadsheetGear.IWorkbook wb, int intype, string origin, bool isHCP)
        {
            //deprecated 14032012
            //set loose
            //if (!setloose)
            //{
            //    wb.Worksheets["sheet1"].Cells["d34"].Range.Value = 1;
            //}
            //else
            //{
            //    wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=VLOOKUP(R28C1,R241C18:R539C21,4,FALSE)";
            //}
            //string _s = origin.ToLower();
            string _s = origin.ToLower().Replace("port", "").Trim();
            switch (_s)
            {
                case "fob hong kong": //case "fob hong kong port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=VLOOKUP(R[-6]C[-3],R[207]C[14]:R[566]C[20],4,FALSE)";
                        break;
                    }
                case "fob bangkok": //case "fob bangkok port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=2";
                        break;
                    }
                case "fob mumbai": //case "fob mumbai port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                case "fob chennai": //case "fob chennai port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                case "fob shanghai": //case "fob shanghai port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                case "fob kaohsiung": //case "fob kaohsiung port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                case "fob keelung": //case "fob keelung port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                case "fob port kelang": //case "fob port kelang":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=VLOOKUP(R[-6]C[-3],R[207]C[14]:R[566]C[20],4,FALSE)";
                        break;
                    }
                case "fob dubai ": // case "fob dubai port":
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
                default:
                    {
                        wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=1";
                        break;
                    }
            }
            //end switch

            setloose(wb);

            if (intype == 3)
            {
                //paper size modifies some of the input data
                wb.Worksheets["uploaded"].Cells["c6"].FormulaR1C1 = intype == 3 ? "=Sheet1!R[89]C[1]" : "";
                wb.Worksheets["uploaded"].Cells["d6"].FormulaR1C1 = intype == 3 ? "=Sheet1!R[90]C" : "";
                wb.Worksheets["uploaded"].Cells["e6"].FormulaR1C1 = intype == 3 ? "=Sheet1!R[91]C[-1]" : "";
                wb.Worksheets["uploaded"].Cells["f6"].FormulaR1C1 = intype == 3 ? "=Sheet1!R[92]C[-2]" : "";

                wb.Worksheets["sheet1"].Cells["g32"].FormulaR1C1 = "=VLOOKUP(RC[-1],R[8]C[-3]:R[10]C[-2],2,FALSE)";
                wb.Worksheets["sheet1"].Cells["g33"].FormulaR1C1 = "=(R[-1]C)"; //"=G32";
                wb.Worksheets["sheet1"].Cells["E32"].FormulaR1C1 = "=(Sheet1!RC[-1])";
                wb.Worksheets["sheet1"].Cells["F35"].FormulaR1C1 = "=(Sheet1!R[-2]C[1])";
                wb.Worksheets["sheet1"].Cells["E70"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E71"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E72"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E73"].FormulaR1C1 = "=R[13]C[-4]";
                wb.Worksheets["sheet1"].Cells["E74"].FormulaR1C1 = "=IF(RC[-1]=True,\"Yes\",\"No\")";
            }
            else
            {
                wb.Worksheets["sheet1"].Cells["g32"].FormulaR1C1 = "=VLOOKUP(RC[-1],R[8]C[-3]:R[10]C[-2],2,FALSE)";
                wb.Worksheets["sheet1"].Cells["g33"].FormulaR1C1 = "=(R[-1]C)";
                wb.Worksheets["sheet1"].Cells["E32"].FormulaR1C1 = "=(Sheet1!RC[-1])";
                wb.Worksheets["sheet1"].Cells["F35"].FormulaR1C1 = "=(Sheet1!R[-2]C[1])";
            }

            //shipment summary
            wb.Worksheets["shipmentsummary"].Cells["d3"].Range.FormulaR1C1 = "=BookCalcs!R[7]C[6]";
            wb.Worksheets["shipmentsummary"].Cells["D5"].Range.FormulaR1C1 = "=BookCalcs!R[4]C[1]";
            wb.Worksheets["shipmentsummary"].Cells["D7"].Range.FormulaR1C1 = "=BookCalcs!R[23]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D8"].Range.FormulaR1C1 = "";
            wb.Worksheets["shipmentsummary"].Cells["D9"].Range.FormulaR1C1 = "=BookCalcs!R[21]C[11]";
            wb.Worksheets["shipmentsummary"].Cells["D11"].Range.FormulaR1C1 = "=BookCalcs!R[25]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D13"].Range.FormulaR1C1 = "=BookCalcs!R[11]C[11]";
            wb.Worksheets["shipmentsummary"].Cells["D15"].Range.FormulaR1C1 = "=BookCalcs!R[8]C[5]";
            wb.Worksheets["shipmentsummary"].Cells["D17"].Range.FormulaR1C1 = "=BookCalcs!R[1]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D19"].Range.FormulaR1C1 = "=BookCalcs!R[17]C[15]";
            wb.Worksheets["shipmentsummary"].Cells["D21"].Range.FormulaR1C1 = "=BookCalcs!R[9]C[15]";
            wb.Worksheets["shipmentsummary"].Cells["D23"].Range.FormulaR1C1 = "=ROUND(BookCalcs!R[19]C[15],3)";
            wb.Worksheets["shipmentsummary"].Cells["I3"].Range.FormulaR1C1 = "=BookCalcs!R[3]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I5"].Range.FormulaR1C1 = "=BookCalcs!R[-1]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I7"].Range.FormulaR1C1 = "=BookCalcs!R[-2]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I9"].Range.FormulaR1C1 = "=BookCalcs!R[-2]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I11"].Range.FormulaR1C1 = "=IF(BookCalcs!R[19]C[7]>0,1,0)";
            wb.Worksheets["shipmentsummary"].Cells["I13"].Range.FormulaR1C1 = "=BookCalcs!R[17]C[7]";
            wb.Worksheets["shipmentsummary"].Cells["I15"].Range.FormulaR1C1 = "=BookCalcs!R[15]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["I17"].Range.FormulaR1C1 = "=BookCalcs!R[19]C[6]";
            wb.Worksheets["shipmentsummary"].Cells["I19"].Range.FormulaR1C1 = "=BookCalcs!R[-6]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I21"].Range.FormulaR1C1 = "=ROUND(BookCalcs!R[-10]C[-4],3)";
            wb.Worksheets["shipmentsummary"].Cells["I23"].Range.FormulaR1C1 = "=ROUND(R[-2]C/(R[-4]C/1000),3)";

            //Reset the price calculations
            wb.Worksheets["prices"].Cells["J178"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-15]C[-3]:R[-2]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["E194"].Range.FormulaR1C1 = "=IF(Sheet1!R[-169]C[-4]=\"Dubai\",\"Pre-Palletised Only\",IF(Sheet1!R[-160]C[-1]=1,\"Pre-Palletised Only\",\"Shipped As Loose Cartons\"))";
            wb.Worksheets["prices"].Cells["R206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-8],R[-8]C[-8]:R[-2]C[3],9,FALSE)";
            wb.Worksheets["prices"].Cells["S206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-9],R[-8]C[-9]:R[-2]C[2],10,FALSE)";
            wb.Worksheets["prices"].Cells["T206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-10],R[-8]C[-10]:R[-2]C[1],11,FALSE)";
            wb.Worksheets["prices"].Cells["U206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-11],R[-8]C[-11]:R[-2]C,12,FALSE)";
            wb.Worksheets["prices"].Cells["J206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-8]C[-3]:R[-1]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["J217"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-8]C[-3]:R[-2]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["R217"].Range.FormulaR1C1 = "=IF(R[-23]C[-13]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-8],R[-8]C[-8]:R[-2]C[3],9,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["S217"].Range.FormulaR1C1 = "=IF(R[-23]C[-14]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-9],R[-8]C[-9]:R[-2]C[2],10,FALSE),\"  \")";
            wb.Worksheets["prices"].Cells["T217"].Range.FormulaR1C1 = "=IF(R[-23]C[-15]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-10],R[-8]C[-10]:R[-2]C[1],11,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["U217"].Range.FormulaR1C1 = "=IF(R[-23]C[-16]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-11],R[-8]C[-11]:R[-2]C,12,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["p219"].Range.FormulaR1C1 = "=IF(R[-25]C[-11]=\"Shipped as loose cartons\",\"Loose cartons\",\"Pre-palletised\")";
            wb.Worksheets["prices"].Cells["R219"].Range.FormulaR1C1 = "=IF(R[-25]C[-13]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["s219"].Range.FormulaR1C1 = "=IF(R[-25]C[-14]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["T219"].Range.FormulaR1C1 = "=IF(R[-25]C[-15]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["u219"].Range.FormulaR1C1 = "=IF(R[-25]C[-16]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["G221"].Range.FormulaR1C1 = "=IF(R[-27]C[-2]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["H221"].Range.FormulaR1C1 = "=IF(R[-27]C[-3]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["I221"].Range.FormulaR1C1 = "=IF(R[-27]C[-4]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["G223"].Range.FormulaR1C1 = "=Uploaded!R[-217]C[10]";
            wb.Worksheets["prices"].Cells["G224"].Range.FormulaR1C1 = "=IF(R[-1]C=\"Euros (cents)\",R[-3]C[-1],IF(R[-1]C=\"Sterling (pence)\",R[-3]C,IF(R[-1]C=\"US Dollars (cents)\",R[-3]C[1],R[-3]C[2])))";
            wb.Worksheets["prices"].Cells["G225"].Range.FormulaR1C1 = "=VLOOKUP(R[-2]C,R[-40]C[-4]:R[-37]C[-3],2,FALSE)";
            wb.Worksheets["prices"].Cells["H224"].Range.FormulaR1C1 = "=R[-3]C";
            wb.Worksheets["prices"].Cells["G225"].Range.FormulaR1C1 = "=VLOOKUP(R[-2]C,R[-40]C[-4]:R[-37]C[-3],2,FALSE)";
            wb.Worksheets["prices"].Cells["F227"].Range.FormulaR1C1 = "=Sum(Round(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["G227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["H227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["I227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["G228"].Range.FormulaR1C1 = "=IF(R[-5]C=\"Euros (cents)\",R[-1]C[-1],IF(R[-5]C=\"Sterling (pence)\",R[-1]C,IF(R[-5]C=\"Us Dollars (cents)\",R[-1]C[1],R[-1]C[2])))";
            wb.Worksheets["prices"].Cells["H228"].Range.FormulaR1C1 = "=R[-1]C";
            wb.Worksheets["prices"].Cells["G231"].Range.FormulaR1C1 = "=ROUND((R[-1]C/100)*Uploaded!R[-225]C[9],2)";
            wb.Worksheets["prices"].Cells["H231"].Range.FormulaR1C1 = "=ROUND((R[-1]C/100)*Uploaded!R[-225]C[8],2)";
            wb.Worksheets["prices"].Cells["G232"].Range.FormulaR1C1 = "=VLOOKUP(R[-46]C[-5],R[-47]C[-4]:R[-44]C[-1],4,FALSE)";
            wb.Worksheets["prices"].Cells["H232"].Range.FormulaR1C1 = "=R[-44]C[-2]";
            wb.Worksheets["prices"].Cells["K228"].Range.FormulaR1C1 = "=CONCATENATE(R[2]C[-4],R[-1]C[2],R[-3]C[-4])";
            wb.Worksheets["prices"].Cells["K229"].Range.FormulaR1C1 = "=CONCATENATE(R[2]C[-4],R[-2]C[2],R[3]C[-4])";
            wb.Worksheets["prices"].Cells["R229"].Range.FormulaR1C1 = "=IF(Sheet1!R[-220]C[-11]=0,CONCATENATE(Prices!R[-2]C[3],Prices!R[-2]C[2],Prices!R[-1]C[3]),CONCATENATE(Prices!R[-2]C[3],Prices!R[-2]C[2],Prices!R[-2]C,Prices!R[-2]C[2],Prices!R[-2]C[1]))";

            //sorting
            //SpreadsheetGear.SortKey _key1 = new SpreadsheetGear.SortKey(6, SpreadsheetGear.SortOrder.Ascending, SpreadsheetGear.SortDataOption.Normal);
            //SpreadsheetGear.SortKey _key2 = new SpreadsheetGear.SortKey(9, SpreadsheetGear.SortOrder.Ascending, SpreadsheetGear.SortDataOption.Normal);
            //SpreadsheetGear.SortKey[] _keys = {_key1, _key2}; 
            //wb.Worksheets["prices"].Range["A162:U177"].Sort(SpreadsheetGear.SortOrientation.Rows, false ,_keys); 

            //Range("A162:U177").sort Key1:=.Range("G163"), _
            // Order1:=xlAscending, Key2:=.Range("J163"), _
            //Order2:=xlAscending, Header:=xlGuess, _
            //OrderCustom:=1, MatchCase:=False, _
            //Orientation:=xlTopToBottom

            //this is done after sortkeys in original pricer
            wb.Worksheets["prices"].Cells["E195"].Range.FormulaR1C1 = "=IF(R194C5=\"Shipped As Loose Cartons\",R[22]C[5],\"Shipment As Loose Cartons Not Available\")";
            wb.Worksheets["prices"].Cells["G230"].Range.FormulaR1C1 = "=IF(Sheet1!R[-196]C[-3]=3,R[-6]C,IF(R[-6]C=\" \",R[-2]C,IF(R[-6]C<R[-2]C,R[-6]C,R[-2]C)))";
            wb.Worksheets["prices"].Cells["H230"].Range.FormulaR1C1 = "=IF(Sheet1!R[-196]C[-4]=3,R[-9]C,IF(R[-6]C[-1]=\" \",R[-2]C,IF(R[-6]C<R[-2]C,R[-6]C,R[-2]C)))";

            //what is this for?!
            wb.Worksheets["pricessummary"].Range["A2:AS17"].Copy(wb.Worksheets["pricessummary"].Range["A52"]);
            wb.Worksheets["pricessummary"].Range["A52:AS65"].Copy(wb.Worksheets["pricessummary"].Range["A71"]);
            wb.Worksheets["pricessummary"].Range["A37:AS43"].Copy(wb.Worksheets["pricessummary"].Range["A81"]);

            //WHY do we keep neding to do this?!
            wb.Worksheets["prices"].Cells["g230"].Formula = "=IF(Sheet1!D34=3,G224,IF(G224=\" \",G228,IF(G224<G228,G224,G228)))";

            wb.Save(); 
        }

        protected void setloose(SpreadsheetGear.IWorkbook wb)
        {
            string _country = wb.Worksheets["Uploaded"].Cells["m6"].Value != null ? wb.Worksheets["Uploaded"].Cells["m6"].Value.ToString() : "";

            switch (_country)
            {
                case "India":
                    {
                        wb.Worksheets["sheet1"].Range["D34"].FormulaR1C1 = "1";
                        break;
                    }
                case "China":
                    {
                        wb.Worksheets["sheet1"].Range["D34"].FormulaR1C1 = "1";
                        break;
                    }
                case "Taiwan":
                    {
                        wb.Worksheets["sheet1"].Range["D34"].FormulaR1C1 = "1";
                        break;
                    }
                case "Thailand":
                    {
                        wb.Worksheets["sheet1"].Range["D34"].FormulaR1C1 = "1";
                        break;
                    } 
                default:
                    {
                        wb.Worksheets["sheet1"].Range["D34"].FormulaR1C1 = "=VLOOKUP(R28C1,R241C18:R539C21,4,FALSE)";
                        break;
                    }
            }
        }
        /// <summary>
        /// reset r1c1 formulas across all sheets
        /// </summary>
        protected void resetFormulas080911(SpreadsheetGear.IWorkbook wb, int intype, bool setloose)
        {
            //set loose
            if (!setloose)
            {
                wb.Worksheets["sheet1"].Cells["d34"].Range.Value = 1;
            }
            else
            {
                wb.Worksheets["sheet1"].Cells["d34"].FormulaR1C1 = "=VLOOKUP(R28C1,R241C18:R539C21,4,FALSE)";
            }

            //book calcs
            wb.Worksheets["bookcalcs"].Cells["b17"].Range.FormulaR1C1 = "=VLOOKUP(Sheet1!R[15]C[4],Sheet1!R[23]C[2]:R[26]C[8],3,FALSE)";
            wb.Worksheets["bookcalcs"].Cells["b18"].Range.FormulaR1C1 = "=VLOOKUP(Sheet1!R[14]C[4],Sheet1!R[22]C[2]:R[25]C[8],4,FALSE)";
            wb.Worksheets["bookcalcs"].Cells["b19"].Range.FormulaR1C1 = "=VLOOKUP(Sheet1!R[13]C[4],Sheet1!R[21]C[2]:R[24]C[8],5,FALSE)";
            wb.Worksheets["bookcalcs"].Cells["b20"].Range.FormulaR1C1 = "=VLOOKUP(Sheet1!R[12]C[4],Sheet1!R[20]C[2]:R[23]C[8],6,FALSE)";
            wb.Worksheets["bookcalcs"].Cells["b21"].Range.FormulaR1C1 = "=VLOOKUP(Sheet1!R[11]C[4],Sheet1!R[19]C[2]:R[22]C[8],7,FALSE)";
    
            //paper size modifies some of the input data
            wb.Worksheets["uploaded"].Cells["c6"].Range.FormulaR1C1 = intype == 3 ? "=Sheet1!R[89]C[1]" : "";
            wb.Worksheets["uploaded"].Cells["d6"].Range.FormulaR1C1 = intype == 3 ? "=Sheet1!R[90]C" : "";
            wb.Worksheets["uploaded"].Cells["e6"].Range.FormulaR1C1 = intype == 3 ? "=Sheet1!R[91]C[-1]" : "";
            wb.Worksheets["uploaded"].Cells["f6"].Range.FormulaR1C1 = intype == 3 ? "=Sheet1!R[92]C[-2]" : "";

            //different formulas for paper size
            if (intype != 3)
            {
                wb.Worksheets["sheet1"].Cells["g32"].FormulaR1C1 = "=VLOOKUP(RC[-1],R[8]C[-3]:R[10]C[-2],2,FALSE)";
                wb.Worksheets["sheet1"].Cells["g33"].FormulaR1C1 = "=(R[-1]C)";
                wb.Worksheets["sheet1"].Cells["E32"].FormulaR1C1 = "=(Sheet1!RC[-1])";
                wb.Worksheets["sheet1"].Cells["F35"].FormulaR1C1 = "=(Sheet1!R[-2]C[1])";

            }
            else
            {
                //TempPrintOut
                //wb.Worksheets["TempPrintOut"].Cells["A42"].Value = "Block Dims";
                //wb.Worksheets["TempPrintOut"].Cells["A42"].Value = "Paper";
                //wb.Worksheets["TempPrintOut"].Cells["B43"].Value = "weight gsm";
                //wb.Worksheets["TempPrintOut"].Cells["C43"].Value = ("Sheet1!E73");
                
                //sheet1
                wb.Worksheets["sheet1"].Cells["g32"].FormulaR1C1 = "=VLOOKUP(RC[-1],R[8]C[-3]:R[10]C[-2],2,FALSE)";
                wb.Worksheets["sheet1"].Cells["g33"].FormulaR1C1 = "=(R[-1]C)";
                wb.Worksheets["sheet1"].Cells["E32"].FormulaR1C1 = "=(Sheet1!RC[-1])";
                wb.Worksheets["sheet1"].Cells["F35"].FormulaR1C1 = "=(Sheet1!R[-2]C[1])";
                wb.Worksheets["sheet1"].Cells["E70"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E71"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E72"].FormulaR1C1 = "=RC[-2]";
                wb.Worksheets["sheet1"].Cells["E73"].FormulaR1C1 = "=R[13]C[-4]";
                wb.Worksheets["sheet1"].Cells["E74"].FormulaR1C1 = "=IF(RC[-1]=True,\"Yes\",\"No\")";
            }

            //shipment summary
            wb.Worksheets["shipmentsummary"].Cells["d3"].Range.FormulaR1C1 = "=BookCalcs!R[7]C[6]";
            wb.Worksheets["shipmentsummary"].Cells["D5"].Range.FormulaR1C1 = "=BookCalcs!R[4]C[1]";
            wb.Worksheets["shipmentsummary"].Cells["D7"].Range.FormulaR1C1 = "=BookCalcs!R[23]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D8"].Range.FormulaR1C1 = "";
            wb.Worksheets["shipmentsummary"].Cells["D9"].Range.FormulaR1C1 = "=BookCalcs!R[21]C[11]";
            wb.Worksheets["shipmentsummary"].Cells["D11"].Range.FormulaR1C1 = "=BookCalcs!R[25]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D13"].Range.FormulaR1C1 = "=BookCalcs!R[11]C[11]";
            wb.Worksheets["shipmentsummary"].Cells["D15"].Range.FormulaR1C1 = "=BookCalcs!R[8]C[5]";
            wb.Worksheets["shipmentsummary"].Cells["D17"].Range.FormulaR1C1 = "=BookCalcs!R[1]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["D19"].Range.FormulaR1C1 = "=BookCalcs!R[17]C[15]";
            wb.Worksheets["shipmentsummary"].Cells["D21"].Range.FormulaR1C1 = "=BookCalcs!R[9]C[15]";
            wb.Worksheets["shipmentsummary"].Cells["D23"].Range.FormulaR1C1 = "=ROUND(BookCalcs!R[19]C[15],3)";
            wb.Worksheets["shipmentsummary"].Cells["I3"].Range.FormulaR1C1 = "=BookCalcs!R[3]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I5"].Range.FormulaR1C1 = "=BookCalcs!R[-1]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I7"].Range.FormulaR1C1 = "=BookCalcs!R[-2]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I9"].Range.FormulaR1C1 = "=BookCalcs!R[-2]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I11"].Range.FormulaR1C1 = "=IF(BookCalcs!R[19]C[7]>0,1,0)";
            wb.Worksheets["shipmentsummary"].Cells["I13"].Range.FormulaR1C1 = "=BookCalcs!R[17]C[7]";
            wb.Worksheets["shipmentsummary"].Cells["I15"].Range.FormulaR1C1 = "=BookCalcs!R[15]C[9]";
            wb.Worksheets["shipmentsummary"].Cells["I17"].Range.FormulaR1C1 = "=BookCalcs!R[19]C[6]";
            wb.Worksheets["shipmentsummary"].Cells["I19"].Range.FormulaR1C1 = "=BookCalcs!R[-6]C[-4]";
            wb.Worksheets["shipmentsummary"].Cells["I21"].Range.FormulaR1C1 = "=ROUND(BookCalcs!R[-10]C[-4],3)";
            wb.Worksheets["shipmentsummary"].Cells["I23"].Range.FormulaR1C1 = "=ROUND(R[-2]C/(R[-4]C/1000),3)";

            //Reset the price calculations
            wb.Worksheets["prices"].Cells["J178"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-15]C[-3]:R[-2]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["E194"].Range.FormulaR1C1 = "=IF(Sheet1!R[-169]C[-4]=\"Dubai\",\"Pre-Palletised Only\",IF(Sheet1!R[-160]C[-1]=1,\"Pre-Palletised Only\",\"Shipped As Loose Cartons\"))";
            wb.Worksheets["prices"].Cells["R206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-8],R[-8]C[-8]:R[-2]C[3],9,FALSE)";
            wb.Worksheets["prices"].Cells["S206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-9],R[-8]C[-9]:R[-2]C[2],10,FALSE)";
            wb.Worksheets["prices"].Cells["T206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-10],R[-8]C[-10]:R[-2]C[1],11,FALSE)";
            wb.Worksheets["prices"].Cells["U206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-11],R[-8]C[-11]:R[-2]C,12,FALSE)";
            wb.Worksheets["prices"].Cells["J206"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-8]C[-3]:R[-1]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["J217"].Range.FormulaR1C1 = "=VLOOKUP(RC[-3],R[-8]C[-3]:R[-2]C,4,FALSE)";
            wb.Worksheets["prices"].Cells["R217"].Range.FormulaR1C1 = "=IF(R[-23]C[-13]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-8],R[-8]C[-8]:R[-2]C[3],9,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["S217"].Range.FormulaR1C1 = "=IF(R[-23]C[-14]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-9],R[-8]C[-9]:R[-2]C[2],10,FALSE),\"  \")";
            wb.Worksheets["prices"].Cells["T217"].Range.FormulaR1C1 = "=IF(R[-23]C[-15]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-10],R[-8]C[-10]:R[-2]C[1],11,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["U217"].Range.FormulaR1C1 = "=IF(R[-23]C[-16]=\"Shipped As Loose Cartons\",VLOOKUP(RC[-11],R[-8]C[-11]:R[-2]C,12,FALSE),\" \")";
            wb.Worksheets["prices"].Cells["p219"].Range.FormulaR1C1 = "=IF(R[-25]C[-11]=\"Shipped as loose cartons\",\"Loose cartons\",\"Pre-palletised\")";
            wb.Worksheets["prices"].Cells["R219"].Range.FormulaR1C1 = "=IF(R[-25]C[-13]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["s219"].Range.FormulaR1C1 = "=IF(R[-25]C[-14]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["T219"].Range.FormulaR1C1 = "=IF(R[-25]C[-15]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["u219"].Range.FormulaR1C1 = "=IF(R[-25]C[-16]=\"Shipped as loose cartons\",R[-2]C,R[-13]C)";
            wb.Worksheets["prices"].Cells["G221"].Range.FormulaR1C1 = "=IF(R[-27]C[-2]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["H221"].Range.FormulaR1C1 = "=IF(R[-27]C[-3]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["I221"].Range.FormulaR1C1 = "=IF(R[-27]C[-4]=\"Shipped As Loose Cartons\",SUM(ROUND(R[-3]C,3)*100),\" \")";
            wb.Worksheets["prices"].Cells["G223"].Range.FormulaR1C1 = "=Uploaded!R[-217]C[10]";
            wb.Worksheets["prices"].Cells["G224"].Range.FormulaR1C1 = "=IF(R[-1]C=\"Euros (cents)\",R[-3]C[-1],IF(R[-1]C=\"Sterling (pence)\",R[-3]C,IF(R[-1]C=\"US Dollars (cents)\",R[-3]C[1],R[-3]C[2])))";
            wb.Worksheets["prices"].Cells["G225"].Range.FormulaR1C1 = "=VLOOKUP(R[-2]C,R[-40]C[-4]:R[-37]C[-3],2,FALSE)";
            wb.Worksheets["prices"].Cells["H224"].Range.FormulaR1C1 = "=R[-3]C";
            wb.Worksheets["prices"].Cells["G225"].Range.FormulaR1C1 = "=VLOOKUP(R[-2]C,R[-40]C[-4]:R[-37]C[-3],2,FALSE)";
            wb.Worksheets["prices"].Cells["F227"].Range.FormulaR1C1 = "=Sum(Round(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["G227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["H227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["I227"].Range.FormulaR1C1 = "=SUM(ROUND(R[-20]C,3)*100)";
            wb.Worksheets["prices"].Cells["G228"].Range.FormulaR1C1 = "=IF(R[-5]C=\"Euros (cents)\",R[-1]C[-1],IF(R[-5]C=\"Sterling (pence)\",R[-1]C,IF(R[-5]C=\"Us Dollars (cents)\",R[-1]C[1],R[-1]C[2])))";
            wb.Worksheets["prices"].Cells["H228"].Range.FormulaR1C1 = "=R[-1]C";
            wb.Worksheets["prices"].Cells["G231"].Range.FormulaR1C1 = "=ROUND((R[-1]C/100)*Uploaded!R[-225]C[9],2)";
            wb.Worksheets["prices"].Cells["H231"].Range.FormulaR1C1 = "=ROUND((R[-1]C/100)*Uploaded!R[-225]C[8],2)";
            wb.Worksheets["prices"].Cells["G232"].Range.FormulaR1C1 = "=VLOOKUP(R[-46]C[-5],R[-47]C[-4]:R[-44]C[-1],4,FALSE)";
            wb.Worksheets["prices"].Cells["H232"].Range.FormulaR1C1 = "=R[-44]C[-2]";
            wb.Worksheets["prices"].Cells["K228"].Range.FormulaR1C1 = "=CONCATENATE(R[2]C[-4],R[-1]C[2],R[-3]C[-4])";
            wb.Worksheets["prices"].Cells["K229"].Range.FormulaR1C1 = "=CONCATENATE(R[2]C[-4],R[-2]C[2],R[3]C[-4])";
            wb.Worksheets["prices"].Cells["R229"].Range.FormulaR1C1 = "=IF(Sheet1!R[-220]C[-11]=0,CONCATENATE(Prices!R[-2]C[3],Prices!R[-2]C[2],Prices!R[-1]C[3]),CONCATENATE(Prices!R[-2]C[3],Prices!R[-2]C[2],Prices!R[-2]C,Prices!R[-2]C[2],Prices!R[-2]C[1]))";

            //sorting
            //SpreadsheetGear.SortKey _key1 = new SpreadsheetGear.SortKey(6, SpreadsheetGear.SortOrder.Ascending, SpreadsheetGear.SortDataOption.Normal);
            //SpreadsheetGear.SortKey _key2 = new SpreadsheetGear.SortKey(9, SpreadsheetGear.SortOrder.Ascending, SpreadsheetGear.SortDataOption.Normal);
            //SpreadsheetGear.SortKey[] _keys = {_key1, _key2}; 
            //wb.Worksheets["prices"].Range["A162:U177"].Sort(SpreadsheetGear.SortOrientation.Rows, false ,_keys); 
            
            //Range("A162:U177").sort Key1:=.Range("G163"), _
            // Order1:=xlAscending, Key2:=.Range("J163"), _
            //Order2:=xlAscending, Header:=xlGuess, _
            //OrderCustom:=1, MatchCase:=False, _
            //Orientation:=xlTopToBottom

            //this is done after sortkeys in original pricer
            wb.Worksheets["prices"].Cells["E195"].Range.FormulaR1C1 = "=IF(R194C5=\"Shipped As Loose Cartons\",R[22]C[5],\"Shipment As Loose Cartons Not Available\")";
            wb.Worksheets["prices"].Cells["G230"].Range.FormulaR1C1 = "=IF(Sheet1!R[-196]C[-3]=3,R[-6]C,IF(R[-6]C=\" \",R[-2]C,IF(R[-6]C<R[-2]C,R[-6]C,R[-2]C)))";
            wb.Worksheets["prices"].Cells["H230"].Range.FormulaR1C1 = "=IF(Sheet1!R[-196]C[-4]=3,R[-9]C,IF(R[-6]C[-1]=\" \",R[-2]C,IF(R[-6]C<R[-2]C,R[-6]C,R[-2]C)))";

            //what is this for?!
            wb.Worksheets["pricessummary"].Range["A2:AS17"].Copy(wb.Worksheets["pricessummary"].Range["A52"]);
            wb.Worksheets["pricessummary"].Range["A52:AS65"].Copy(wb.Worksheets["pricessummary"].Range["A71"]);
            wb.Worksheets["pricessummary"].Range["A37:AS43"].Copy(wb.Worksheets["pricessummary"].Range["A81"]);

            wb.Save(); 
        }
        //end reset formulas

        /// <summary>
        /// serialise price data to JSON formatted string
        /// </summary>
        /// <param name="prices">pricevalue collection of calculated price quotes</param>
        /// <returns></returns>
        private string QuoteToJson(PriceValueCollection prices)
        {
            string _json = string.Empty;

            try
            {
                //Return JSON data
                //beware using the serialiser on tables with  a timestamp - timsetamps will not convert
                //use linq to select the data to return, any other method will return the whole class inc. structure and we would have to enumerate columns/rows!
                if (prices.Count > 0)
                {
                    var _rec = from p in prices
                               select new uPriceValues
                               {
                                   quoteId = p.QuoteId,
                                   price_loose_gbp = (double)p.PriceLooseGbp,
                                   price_pallet_gbp = (double)p.PricePalletGbp,
                                   price_total_gbp = (double)p.PriceTotalGbp,
                                   price_loose = (double)p.PriceLoose,
                                   price_pallet = (double)p.PricePallet,
                                   price_total = (double)p.PriceTotal,
                                   price_client = (double)p.PriceClient,
                                   ship_via = p.ShipVia,
                                   pallet_type = p.PalletType,
                                   loose_name = p.LooseName,
                                   lcl_name = p.LclName,
                                   lcl_v = (double)p.LclV,
                                   lcl_v20 = (double)p.LclV20,
                                   lcl_v40 = (double)p.LclV40,
                                   lcl_v40hc = (double)p.LclV40hc,
                                   lcl_loose_name = p.LclLooseName,
                                   lcl_vloose = (double)p.LclVloose,
                                   lcl_vloose20 = (double)p.LclVloose20,
                                   lcl_vloose40 = (double)p.LclVloose40,
                                   lcl_vloose40hc = (double)p.LclVloose40hc,
                                   out_length = (double)p.OutLength,
                                   out_width = (double)p.OutWidth,
                                   out_depth = (double)p.OutDepth,
                                   out_weight = (double)p.OutWeight
                               };

                    JavaScriptSerializer _js = new JavaScriptSerializer();
                    _json += _js.Serialize(_rec);
                }
                else
                {
                    _json = "{pricevalues=0}";
                }
            }
            catch (Exception ex)
            {
                _json = ex.InnerException.ToString();
            }
            return _json;
        }
        //end quote to json

        /// <summary>
        /// serialise price quote input parameters to JSON formatted string
        /// </summary>
        /// <param name="quoteid">unique quote id created when the full price quote was created</param>
        /// <returns></returns>
        private string QuoteCriteriaToJson(int quoteid)
        {
            string _json = string.Empty;

            try
            {
                // Return JSON data
                //beware using the serialiser on tables with a timestamp - timsetamps will not convert
                //use linq to select the data to return, any other method will return the whole class inc. structure and we would have to enumerate columna/rows!
                var _rec = from c in new linq.linq_pricerDataContext().price_values.Where(c => c.quote_Id == quoteid)
                           select new uPriceParams 
                           {
                               quoteId = c.quote_Id,
                               book_title = c.book_title,
                               in_dimensions = c.in_dimensions ,
                               in_currency = c.in_currency,
                               in_pallet= c.in_pallet,
                               in_length = (double)c.in_length,
                               in_width = (double)c.in_width,
                               in_depth = (double)c.in_depth,
                               in_weight = (double)c.in_weight,
                               in_extent = (double)c.in_extent,
                               in_papergsm = (double)c.in_papergsm,
                               in_hardback = (bool)c.in_hardback,
                               copies_carton = (int)c.copies_carton, 
                               origin_name = c.origin_name, 
                               country_name = c.country_name,
                               final_name = c.final_name, 
                               tot_copies = (int)c.tot_copies 
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            catch (Exception ex)
            {
                _json = ex.InnerException.ToString();
            }
            return _json;
        }
        //end quote params to json
        /// <summary>
        /// serialise price quote data to JSON formatted string
        /// </summary>
        /// <param name="quoteid">unique quote id created when the full price quote was created</param>
        /// <returns></returns>
        private string PriceToJson(int quoteid)
        {
            //should we parse nulls using e.g. price_loose_gbp = c.price_loose_gbp ?? 0
            //the default values are set to 0 when AssignBasevalues() anyway

            string _json = string.Empty;

            try
            {
                // Return JSON data
                //beware using the serialiser on tables witha timestamp - timsetamps will not convert
                //use linq to select the data to return, any other method will return the whole class inc. structure and we would have to enumerate columns/rows!
                var _rec = from c in new linq.linq_pricerDataContext().price_values.Where(c => c.quote_Id == quoteid)
                           select new uPriceValues
                           {
                               quoteId = c.quote_Id,
                               price_loose_gbp = (double)c.price_loose_gbp,
                               price_pallet_gbp = (double)c.price_pallet_gbp,
                               price_total_gbp = (double)c.price_total_gbp,
                               price_loose = (double)c.price_loose,
                               price_pallet = (double)c.price_pallet,
                               price_total = (double)c.price_total,
                               price_client = (double)c.price_client, 
                               ship_via = c.ship_via,
                               pallet_type = c.pallet_type,
                               loose_name = c.loose_name,
                               lcl_name = c.lcl_name,
                               lcl_v = (double)c.lcl_v,
                               lcl_v20 = (double)c.lcl_v20,
                               lcl_v40 = (double)c.lcl_v40,
                               lcl_v40hc = (double)c.lcl_v40hc,
                               lcl_loose_name = c.lcl_loose_name,
                               lcl_vloose = (double)c.lcl_vloose,
                               lcl_vloose20 = (double)c.lcl_vloose20,
                               lcl_vloose40 = (double)c.lcl_vloose40,
                               lcl_vloose40hc = (double)c.lcl_vloose40hc,
                               out_length = (double)c.out_length,
                               out_width = (double)c.out_width,
                               out_depth = (double)c.out_depth,
                               out_weight = (double)c.out_weight 
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            catch (Exception ex)
            {
                _json = ex.InnerException.ToString();
            }
            return _json;
        }
        //end quote to json

        /// <summary>
        /// serialise costing summary data to JSON formatted string
        /// </summary>
        /// <param name="quoteid">unique quote id created when the full price quote was created</param>
        /// <returns></returns>
        private string CostingToJson(int quoteid, string costingtype)
        {
            //should we parse nulls using e.g. pre_part = c.pre_part ?? 0
            //the default values for costings and shipment sizes = 0 anyway

            string _json = string.Empty;

            if (string.IsNullOrEmpty(costingtype)) { costingtype = "pre-palletised"; }

            try
            {
                var _rec = from c in new linq.linq_pricerDataContext().costing_summaries.Where(c => c.quote_Id == quoteid && c.summary_type == costingtype)
                           select new uCostingSummary
                           {
                               quoteId = (int)c.quote_Id,
                               pre_part = (double)c.pre_part,
                               pre_full = (double)c.pre_full,
                               pre_thc20 = (double)c.pre_thc20,
                               pre_thc40 = (double)c.pre_thc40,
                               pre_thclcl = (double)c.pre_thclcl,
                               pre_docs = (double)c.pre_docs,
                               pre_origin = (double)c.pre_origin,
                               pre_haul20 = (double)c.pre_haul20,
                               pre_haul40 = (double)c.pre_haul40,
                               freight_lcl = (double)c.freight_lcl,
                               freight_20 = (double)c.freight_20,
                               freight_40 = (double)c.freight_40,
                               freight_40hq = (double)c.freight_40hq,
                               on_dest_lcl = (double)c.on_dest_lcl,
                               on_pier_etc = (double)c.on_pier_etc,
                               on_dest_20 = (double)c.on_dest_20,
                               on_dest_40 = (double)c.on_dest_40,
                               on_docs = (double)c.on_docs,
                               on_customs = (double)c.on_customs,
                               on_part = (double)c.on_part,
                               on_full = (double)c.on_full,
                               on_haul20 = (double)c.on_haul20,
                               on_haul40 = (double)c.on_haul40,
                               on_shunt20 = (double)c.on_shunt20,
                               on_shunt40 = (double)c.on_shunt40,
                               on_pallets = (double)c.on_pallets,
                               on_other = (double)c.on_other
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            catch (Exception ex)
            {
                _json = ex.InnerException.ToString();
            }
            return _json;
        }
        //end costing summary to json

        /// <summary>
        /// serialise shipment size data to JSON formatted string
        /// </summary>
        /// <param name="quoteid">unique quote id created when the full price quote was created</param>
        /// <returns></returns>
        private string ShipmentToJson(int quoteid)
        {
            //should we parse nulls using e.g. tot_cartons = c.tot_cartons ?? 0
            //the default values for costings and shipment sizes = 0 anyway

            string _json = string.Empty;

            try
            {
                var _rec = from c in new linq.linq_pricerDataContext().shipment_sizes.Where(c => c.quote_id == quoteid)
                           select new uShipmentSize
                           {
                               quoteId = (int)c.quote_id,
                               calc_copiescarton = (double)c.calc_copiescarton, 
                               tot_cartons = (double)c.tot_cartons, 
                               pal_cartons = (double)c.pal_cartons,
                               pal_full = (double)c.pal_full,
                               pal_full_wt = (double)c.pal_full_wt,
                               pal_full_cu = (double)c.pal_full_cu,
                               pal_layers = (double)c.pal_layers,
                               pal_layer_count = (double)c.pal_layer_count,
                               pal_total_wt = (double)c.pal_total_wt,
                               pal_total_cu = (double)c.pal_total_cu,
                               pal_ratio = (double)c.pal_ratio,
                               ctn_hgt = (double)c.ctn_hgt,
                               ctn_len = (double)c.ctn_len,
                               ctn_wid = (double)c.ctn_wid,
                               ctn_wt = (double)c.ctn_wt,
                               par_count = (double)c.par_count,
                               ctn_remaining = (double)c.ctn_remaining,
                               residue_cu = (double)c.residue_cu,
                               residue_wt = (double)c.residue_wt,
                               ctn_total_wt = (double)c.ctn_total_wt,
                               ctn_total_cu = (double)c.ctn_total_cu,
                               ctn_ratio = (double)c.ctn_ratio,
                           };

                JavaScriptSerializer _js = new JavaScriptSerializer();
                _json += _js.Serialize(_rec);
            }
            catch (Exception ex)
            {
                _json = ex.InnerException.ToString();
            }
            return _json;
        }
        //end shipment size to json

        

        /// <summary>
        /// parse string to see if it can be converted to double 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        protected double dbl(string svalue)
        {
            double _result = 0;
            Boolean _test = false;

            _test = Double.TryParse(svalue, out _result);

            return _result;
        }
        //end testdouble

        /// <summary>
        /// parse string to see if it can be converted to integer 
        /// </summary>
        /// <param name="svalue">string to parse</param>
        /// <returns>double</returns>
        protected int ntg(string svalue)
        {
            int _result = 0;
            Boolean _test = false;

            _test = int.TryParse(svalue, out _result);

            return _result;
        }
        //end testdouble

        /// <summary>
        /// check to see if a specific pricer has been submitted (publiship users can choose which spreadsheet they use)
        /// other users check company group to see if they have a default pricer
        /// </summary>
        /// <param name="svalue">string which can contain spreadsheet name</param>
        /// <param name="companyid">company id of user</param>
        /// <returns></returns>
        protected string pricer_submitted(string svalue)
        {
            string _result = "";
            string _dir = "~/Include/";
  
            if (svalue.Contains("&&"))
            {
                string _spreadsheet = wwi_func.get_string_element(svalue, "&", 1);
                //string[] _parts = svalue.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                //string _spreadsheet = _parts[_parts.Length - 1]; //should be last element in array 
                if (!string.IsNullOrEmpty(_spreadsheet))
                {
                    string _source = HttpContext.Current.Server.MapPath(_dir + _spreadsheet);
                    if (System.IO.File.Exists(_source)) { _result = _spreadsheet; }
                }
            }
            
            return _result; 
        }
    }
}
