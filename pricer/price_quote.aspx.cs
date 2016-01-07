using System;
using System.Data;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
//using System.Threading; need to enable this for asyn operations
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPager;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;

public partial class price_quote : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32 _uid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
        //*********
        //IMPORTANT
        //when setting up new pricer groups, check the pricer_range.iso
        //origin, destination country and final destination might have a range determined by the group so if you don't set the range up
        //you will get errors whenm trying to bind the dlls (e.g.Index 0 is either negative or above rows count)
        //*********
        int _group = check_group(); //wwi_func.get_company_group();

        this.dxpagepricer.ActiveTabIndex = 0;
        //*********** DO NOT CHANGE ********
        this.dxhfpricer.Set("tabb", 5); //DO NOT CHANGE THIS ensure default is user can only see the client quote form
        //*********** END DO NOT CHANGE ********
        this.dxbtnquote0.ClientVisible = false;
        this.dxbtnback0.ClientVisible = false;
        this.dxbtnprint0.ClientVisible = false;
        this.dxbtnprice0.ClientVisible = false;

        if (_uid > 0)
        {
            if (!Page.IsPostBack)
            {
                //not required, just change text to 'subject to agreed validity terms' for all users 
                //080115 for HarperCollins change message in client quote page
                //string _cid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).CompanyId.ToString() : "";
                //if(_cid != ""){
                //    this.dxrndprice.HeaderText = 
                //        wwi_func.find_in_xml("xml\\parameters.xml", "name", "companyid_no_message", "value", _cid) == false?  "Your quote (held firm for orders within the next 7 days)": "Your quote";
                //}

                //defaults
                //250811 user cookie?
                int _dims = Page.Request.Cookies["dims"] != null ? wwi_func.vint(Page.Request.Cookies["dims"].Value.ToString()) : 1;
                ///HttpUtility.UrlDecode is equivalent to javacript unescape() function used when creating this cookie
                string _currency = Page.Request.Cookies["crnc"] != null ? HttpUtility.UrlDecode(Page.Request.Cookies["crnc"].Value.ToString(), System.Text.Encoding.Default) : "Sterling (pence),GBP";
                string _pallet = Page.Request.Cookies["pall"] != null ? Page.Request.Cookies["pall"].Value.ToString() : "Standard";
                

                if ((Int32)((UserClass)Page.Session["user"]).CompanyId == -1) //internal users  
                {
                    bind_cbo_pallet(_pallet); //only for internal users
                    this.dxlbltypepallet.ClientVisible = true;
                    this.dxcbotypepallet.ClientVisible = true;
                }
                else
                {
                    bind_cbo_contact(); //only for external users contacts by controlling office 
                    this.dxlbltypepallet.ClientVisible = false;
                    this.dxcbotypepallet.ClientVisible = false;

                }

                //user info to form
                this.dxtxtusername.Text = (string)((UserClass)Page.Session["user"]).UserName;
                this.dxtxtusercomp.Text = (string)((UserClass)Page.Session["user"]).OfficeName;
                this.dxtxtuseremail.Text = (string)((UserClass)Page.Session["user"]).mailTo;
                this.dxtxtusertel.Text = (string)((UserClass)Page.Session["user"]).telNo;

                if (!this.dxhfpricer.Contains("dims"))
                {
                    this.dxhfpricer.Add("dims", _dims);
                    
                } //default input type

                if (!this.dxhfpricer.Contains("crnc"))
                {
                    this.dxhfpricer.Add("crnc", _currency);
                    //this.dxhfpricer.Add("crnc", "Sterling (pence)");
                    //250811 user cookie?
                } //default currency

                if (!this.dxhfpricer.Contains("pall"))
                {
                    this.dxhfpricer.Add("pall", _pallet);
                    //250811 user cookie?
                } //default type of pallet

                //databindings
                //bind_cbo_origin();
                bind_cbo_origin(_group);
                bind_cbo_currency(_currency);
                //040712 office users can select which pricer to use
                bind_cbo_pricers(_group);
                //110413 get paper gsm from spreadsheet to restrict range of values
                bind_cbo_paper_gsm(_group);
                
                //default value for input type
                this.dxrblintpe.Items[_dims -1].Selected = true; 

                //04/02/15 set required fields
                //HarperCollins require title field but do not necessarily require country, destination as they just want to be able to save a quote
                //to databasw without generatinmg a price
                //set_required_fields(_group);
 
                //040811 check to see if user wants to re-use a previous quote passed back from wbs_pricer_audit
                int _usequote = Request.QueryString["qr"] != null ? wwi_func.vint(wwi_security.DecryptString(Request.QueryString["qr"].ToString(),"publiship")): 0 ;
                if (_usequote > 0)
                {
                    //120911 is user editing a quote (save as client_visible = false) or using as a new quote (save client_visible= true)
                    //if clientvislble=0 pass quoterid as negative value
                    int _clientvisible = Request.QueryString["cv"] != null ? wwi_func.vint(Request.QueryString["cv"]) : 1;
                    if (_clientvisible == 0) { _clientvisible = 0 - _usequote; } else { _clientvisible = _usequote; }
                    this.dxhfpricer.Set("cv", _clientvisible);
                
                    get_price_criteria(_usequote, _group);   //bind dlls based on selected items in get_price_criteria
                }
                else
                {
                    //bind ddls to default values
                    //bind_cbo_country(this.dxcborigin.Value != null ? this.dxcborigin.Value.ToString() : "0");
                    bind_cbo_country(_group, this.dxcborigin.Text != null ? this.dxcborigin.Text.ToString() : "");
                    //bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "0");
                    bind_cbo_final(_group, this.dxcbcountry.Text != null ? this.dxcbcountry.Text.ToString() : "");
                }
                               
            }
            else
            {
                //have to do this here?
                string _dims = this.dxhfpricer.Get("dims").ToString();
                set_input_type_panel(_dims);
                //rebind on postback or combos do not hold correct values
                bind_cbo_country(_group, this.dxcborigin.Text != null ? this.dxcborigin.Text.ToString() : "");
                //bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "0");
                bind_cbo_final(_group, this.dxcbcountry.Text != null ? this.dxcbcountry.Text.ToString() : "");
            }
        }
        else
        {
            //user is not logged in so open login form
            //DevExpress.Web.ASPxPopupControl.ASPxPopupControl _p = (DevExpress.Web.ASPxPopupControl.ASPxPopupControl)Master.FindControl("popWindows");
            //if (_p != null) { _p.Windows[0].ShowOnPageLoad = true; }
            if (!Page.IsPostBack)
            {
               Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("pricer/price_quote", "publiship")); 
            }
        }

    }


    #region form events
    /// <summary>
    /// pager page index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxpgrpricer_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxPager _pager = sender as ASPxPager;
        this.dxpagepricer.ActiveTabIndex = _pager.PageIndex;

    }

    protected void dxbtnget_Click(object sender, EventArgs e)
    {
        if (Page.Session["user"] != null)
        {
            
            const int _maxattempts = 2;
            
            int _attempts = 0;
            int _dims = 0;
            string _error = "";
            string _test = "";
            bool _returned = false;

            //050912 use currently selected values if not null otherwise check cookies & hidden fields for values 
            //int _dims = !string.IsNullOrEmpty(this.dxrblintpe.Value.ToString()) ? wwi_func.vint(this.dxrblintpe.Value.ToString()) : wwi_func.vint(get_saved_token("dims"));
            _dims = this.dxrblintpe.Value != null ? wwi_func.vint(this.dxrblintpe.Value.ToString()) : wwi_func.vint(get_saved_token("dims"));
            //make sure we have valid inputs for all dimensions
            _test = test_input_values(Math.Abs(_dims));
            if (_test != "")
            {
                while (_attempts <= _maxattempts && _returned == false)
                {
                    try
                    {
                        localhost.Publiship_Pricer _ws = new localhost.Publiship_Pricer();

                        //switch to this when debugging and check port number!
                        //use global resx to identify web site location

                        string _location = get_site_url(wwi_func.get_from_global_resx("web_site_location")); //for testing use: get_site_url("local");
                        _ws.Url = _location;

                        _ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        //consume web service
                        //Test string
                        //string _result = _ws.GetPriceQuote(1, this.dxhfpricer.Get("crnc").ToString(), this.dxtxttitle.Text.ToString(),
                        //        305, 242, 18, 800, 100, this.dxtxtorigin.Text.ToString(), this.dxtxtdest.Text.ToString(), 5000);

                        //080112 if this information is not in hidden fields (seems to be problematic for some users? mac os?
                        //use cookie values

                        Int32 _quoteid = this.dxhfpricer.Contains("cv") ? wwi_func.vint(this.dxhfpricer.Get("cv").ToString()) : 0; //210911 pass quote id through (as a negative number if re-doing quote)
                        Int32 _cid = (Int32)((UserClass)Page.Session["user"]).CompanyId;
                        Int32 _uid = (Int32)((UserClass)Page.Session["user"]).UserId;

                        //040712 only pass this through for publiship users, clients can't choose a pricer 
                        string _spreadsheet = _cid == -1 ? this.dxcbospreadsheet.Text.ToString() : "";

                        string _curr = !string.IsNullOrEmpty(this.dxcbocurrency.Text.ToString()) ? this.dxcbocurrency.Text.ToString() + "," + this.dxcbocurrency.Value.ToString() : get_saved_token("crnc");
                        string _pall = this.dxcbotypepallet.Value != null ? this.dxcbotypepallet.Value.ToString() : get_saved_token("pall");
                        _pall += "&&" + _spreadsheet; ;

                        //290113 check inputs for length, width, height, weight, etc.
                        string[] _inputs = _test.Split(";".ToCharArray());
                        double[] _size = Array.ConvertAll(_inputs, new Converter<string, double>(measurementsTosize));
                        string _title = test_title(Math.Abs(_dims));
                        int _cartons = test_cartons(Math.Abs(_dims));
                        bool _cover = (bool)this.dxckcover.Checked; ;
                        //clear result string
                        string _result = "";

                        switch (Math.Abs(_dims))
                        {
                            case 1:
                                {
                                    //120911 append quoteid and clientvisible status to title string
                                    //_title = !string.IsNullOrEmpty(this.dxtxttitle.Text.ToString()) ? this.dxtxttitle.Text.ToString() : get_saved_token("title"); // +(this.dxhfpricer.Contains("cv") ? this.dxhfpricer.Get("cv").ToString() : "");
                                    //_cartons = !string.IsNullOrEmpty(this.dxtcarton.Text.ToString()) ? wwi_func.vint(this.dxtcarton.Text.ToString()):  wwi_func.vint(get_saved_token("cartons"));

                                    _result = _ws.GetBookQuote(_dims, _curr, _pall, _title,
                                        _size[0], _size[1], _size[2], _size[3], _cartons, this.dxcborigin.Text.ToString(), this.dxcbcountry.Text.ToString(), this.dxcbfinal.Text.ToString(), wwi_func.vint(this.dxtxtcopies.Text.ToString()), _cid, _uid, _quoteid);

                                    break;
                                }
                            case 2:
                                {
                                    //120911 append quoteid and clientvisible status to title string
                                    //_title = !string.IsNullOrEmpty(this.dxtxtcarttitle.Text.ToString()) ? this.dxtxtcarttitle.Text.ToString() : get_saved_token("title");
                                    //_cartons = !string.IsNullOrEmpty(this.dxtxtcartcopy.Text.ToString())? wwi_func.vint(this.dxtxtcartcopy.Text.ToString()): wwi_func.vint(get_saved_token("cartons"));

                                    _result = _ws.GetCartonQuote(_dims, _curr, _pall, _title,
                                        _size[0], _size[1], _size[2], _size[3], _cartons, this.dxcborigin.Text.ToString(), this.dxcbcountry.Text.ToString(), this.dxcbfinal.Text.ToString(), wwi_func.vint(this.dxtxtcopies.Text.ToString()), _cid, _uid, _quoteid);

                                    break;
                                }
                            case 3:
                                {
                                    //120911 append quoteid and clientvisible status to title string
                                    //_title = !string.IsNullOrEmpty(this.dxtxttitle3.Text.ToString()) ? this.dxtxttitle3.Text.ToString() : get_saved_token("title");
                                    //_cover = (bool)this.dxckcover.Checked;

                                    _result = _ws.GetPaperQuote(_dims, _curr, _pall, _title,
                                    _size[0], _size[1], _size[2], _size[3], _cover, this.dxcborigin.Text.ToString(), this.dxcbcountry.Text.ToString(), this.dxcbfinal.Text.ToString(), wwi_func.vint(this.dxtxtcopies.Text.ToString()), _cid, _uid, _quoteid, _inputs[3]);
                                     //_result = pricer_test.GetPaperQuote(_dims, _curr, _pall, _title,
                                     //_size[0], _size[1], _size[2], _size[3], _cover, this.dxcborigin.Text.ToString(), this.dxcbcountry.Text.ToString(), this.dxcbfinal.Text.ToString(), wwi_func.vint(this.dxtxtcopies.Text.ToString()), _cid, _uid, _quoteid);

                                    break;
                                }
                        }

                        //this code creates an asynchronous request to web service - it works - but how do we get the results to a page?!
                        //the service will return a result but the UI can't see it
                        //_ws.BeginGetPriceQuote(1, this.dxhfpricer.Get("crnc").ToString(), this.dxtxttitle.Text.ToString(),
                        //        305, 242, 18, 800, 100, this.dxtxtorigin.Text.ToString(), this.dxtxtdest.Text.ToString(), 5000, new AsyncCallback(callbackHandler), _ws);

                        //get JSON string and format it
                        //this.dxlblquote.Text = _result;
                        this.dxhfpricer.Set("qote", _result);

                        //deserialise JSON string 
                        JavaScriptSerializer _js = new JavaScriptSerializer();
                        //DAL.Pricer.PriceValueCollection _pvs = _js.Deserialize<DAL.Pricer.PriceValueCollection>(_result);
                        //using ilist
                        IList<DAL.Services.uPriceValues> _pvs = _js.Deserialize<IList<DAL.Services.uPriceValues>>(_result);

                        if (_pvs.Count > 0)
                        {
                            //if there was an error total price should return as -1
                            if (_pvs[0].price_total > 0)
                            {
                                //get dimensions from output values - for paper size they might be different than input dims
                                this.dxlblsize.Text = String.Format("{0} x {1} x {2}", _pvs[0].out_length.ToString(), _pvs[0].out_width.ToString(), _pvs[0].out_depth.ToString());
                                this.dxlblweight.Text = String.Format("{0}", _pvs[0].out_weight.ToString());

                                //bind to template
                                //price request summary
                                this.dxlblqot1.Text = _dims == 1 ? "Book size (mm)" : _dims == 2 ? "Carton size" : "Paper size and extent";
                                this.dxlblqot2.Text = _dims == 1 ? "Book weight (gms)" : _dims == 2 ? "Carton weight (kgs)" : "Paper weight (gsm)";
                                this.dxlblbookname.Text = _dims == 1 ? this.dxtxttitle.Text : _dims == 2 ? this.dxtxtcarttitle.Text : this.dxtxttitle3.Text;
                                //size is formatted above

                                this.dxlblfrom.Text = this.dxcborigin.Text.ToString();
                                this.dxlblto.Text = this.dxcbfinal.Text.ToString(); //this.dxcbcountry.Text.ToString();
                                this.dxlblcopies.Text = String.Format("{0} copies", this.dxtxtcopies.Text.ToString());
                                this.dxlblppc.Text = String.Format("{0} per copy", this.dxcbocurrency.Text.ToString());

                                // code for ilist
                                this.dxlblquote.Text = _pvs[0].quoteId.ToString();
                                this.dxlblvia.Text = _pvs[0].ship_via != null ? _pvs[0].ship_via.ToString() : ""; //shipping via
                                this.dxlbltype.Text = _pvs[0].pallet_type.ToString(); //pallet type
                                this.dxlblshiploose.Text = _pvs[0].loose_name.ToString(); //description loose pallets
                                this.dxlbllclname.Text = _pvs[0].lcl_name.ToString(); //all lcl description
                                //prices
                                this.dxlblv.Text = Math.Round(_pvs[0].lcl_v, 2).ToString();
                                this.dxlblv20.Text = Math.Round(_pvs[0].lcl_v20, 2).ToString();
                                this.dxlblv40.Text = Math.Round(_pvs[0].lcl_v40, 2).ToString();
                                this.dxlblv40hc.Text = Math.Round(_pvs[0].lcl_v40hc, 2).ToString();
                                this.dxlblvloose.Text = Math.Round(_pvs[0].lcl_vloose, 2).ToString();
                                this.dxlblvloose20.Text = Math.Round(_pvs[0].lcl_vloose20, 2).ToString();
                                this.dxlblvloose40.Text = Math.Round(_pvs[0].lcl_vloose40, 2).ToString();
                                this.dxlblvloose40hc.Text = Math.Round(_pvs[0].lcl_vloose40hc, 2).ToString();

                                //client page uses a summary view 
                                this.dxlblbookname2.Text = _dims == 1 ? this.dxtxttitle.Text : _dims == 2 ? this.dxtxtcarttitle.Text : this.dxtxttitle3.Text;
                                this.dxlblquote2.Text = _pvs[0].quoteId.ToString();

                                //book price
                                this.dxlblclient1.Text = String.Format("{0} mm", this.dxtxtlength.Text.ToString());
                                this.dxlblclient2.Text = String.Format("{0} mm", this.dxtxtwidth.Text.ToString());
                                this.dxlblclient3.Text = String.Format("{0} mm", this.dxtxtdepth.Text.ToString());
                                this.dxlblclient4.Text = String.Format("{0} gms", this.dxtxtweight.Text.ToString());
                                this.dxlblclient5.Text = this.dxtcarton.Text.ToString();
                                //carton price
                                this.dxlblclient6.Text = String.Format("{0} mm", this.dxtxtside1.Text.ToString());
                                this.dxlblclient7.Text = String.Format("{0} mm", this.dxtxtside2.Text.ToString());
                                this.dxlblclient8.Text = String.Format("{0} mm", this.dxtxtcartdepth.Text.ToString());
                                this.dxlblclient9.Text = String.Format("{0} kgs", this.dxtxtcartweight.Text.ToString());
                                this.dxlblclient10.Text = this.dxtxtcartcopy.Text.ToString();
                                //paper price
                                this.dxlblclient11.Text = String.Format("{0} mm", this.dxtxtblock1.Text.ToString());
                                this.dxlblclient12.Text = String.Format("{0} mm", this.dxtxtblock2.Text.ToString());
                                this.dxlblclient13.Text = String.Format("{0} pp", this.dxtxtextent.Text.ToString());
                                //this.dxlblclient14.Text = String.Format(this.dxlblclient14.Text, this.dxspinpaper.Text.ToString());
                                this.dxlblclient14.Text = String.Format("{0} gsm", this.dxcboPaper.Text.ToString());
                                this.dxlblclient15.Text = this.dxckcover.Checked ? "Yes" : "No";
                                //other client summary stuff 
                                this.dxlblfrom2.Text = this.dxcborigin.Text.ToString();
                                this.dxlblto2.Text = this.dxcbfinal.Text.ToString(); //this.dxcbcountry.Text.ToString();
                                this.dxlblvia2.Text = this.dxlblvia.Text;
                                this.dxlblcopies2.Text = this.dxlblcopies.Text;
                                this.dxlblppc2.Text = this.dxlblppc.Text; //currency description

                                //05/08/11 moved to service
                                //exchange rate?
                                string _currvalue = this.dxcbocurrency.Value.ToString();
                                //double _rate = wwi_func.getexchangerate(_currvalue, "GBP");

                                //09/08/2011 don't display sterling price as well request by Dave Thompson
                                //so this section is unnecessary, gbp <dl>'s have beeen hidden in client page
                                //if (_currvalue != "GBP")
                                //{
                                //    this.dxlblpricepre.Text = Math.Round(_pvs[0].price_pallet, 2).ToString() + " (£" + Math.Round(_pvs[0].price_pallet_gbp, 2).ToString() + ")"; //pre-palletised price
                                //    this.dxlblpriceloose.Text = Math.Round(_pvs[0].price_loose, 2).ToString() + " (£" + Math.Round(_pvs[0].price_loose_gbp, 2).ToString() + ")"; //price loose 
                                //    this.dxlblppc3.Text = Math.Round(_pvs[0].price_loose, 2).ToString(); //per copy by currency
                                //    this.dxlbltotprice2.Text = Math.Round(_pvs[0].price_total,2).ToString(); //total price by currency
                                //    this.dxlblexprice2.Text = Math.Round(_pvs[0].price_loose_gbp, 2).ToString(); //per copy sterling
                                //    this.dxlblexprice3.Text = Math.Round(_pvs[0].price_total_gbp, 2).ToString(); //total sterling
                                //}
                                //else
                                //{
                                //    this.dxlblpricepre.Text = Math.Round(_pvs[0].price_pallet,2).ToString(); //pre-palletised price
                                //    this.dxlblpriceloose.Text = Math.Round(_pvs[0].price_loose,2).ToString(); //price loose 
                                //    this.dxlblppc3.Text = Math.Round(_pvs[0].price_loose_gbp, 2).ToString(); //per copy sterling
                                //    this.dxlbltotprice2.Text = Math.Round(_pvs[0].price_total_gbp, 2).ToString(); //total price sterling
                                //    //client view hide unnecessary fields
                                //    this.dxlblexprice0.Text = "";
                                //    this.dxlblexprice1.Text = "";
                                //    this.dxlblexprice2.Text = "";
                                //    this.dxlblexprice3.Text = "";
                                //}
                                //format currency description
                                string[] _currtext = this.dxcbocurrency.Text.Replace(")", "").ToString().Split('(');

                                //internal
                                this.dxlblpricepre.Text = Math.Round(_pvs[0].price_pallet, 2).ToString() + " " + _currtext[1].ToString(); //pre-palletised price
                                this.dxlblpriceloose.Text = Math.Round(_pvs[0].price_loose, 2).ToString() + " " + _currtext[1].ToString(); //price loose 
                                //client
                                this.dxlblppc3.Text = Math.Round(_pvs[0].price_client, 2).ToString() + " " + _currtext[1].ToString(); //per copy by currency that client sees
                                this.dxlbltotprice2.Text = Math.Round(_pvs[0].price_total, 2).ToString() + " " + _currtext[0].ToString(); //total price by currency
                                
                                //moved to end while
                                //show_quote_tab();
                                _returned = true;
                            }
                            else
                            {
                                _attempts += 1;
                                _error += Environment.NewLine + Environment.NewLine + "Attempt " + _attempts.ToString() + ": Price not available." + Environment.NewLine + "Unfortunately it was not possible to calculate a price using the information supplied." + Environment.NewLine + "Please check your input details or contact Publiship for more information."; ;
                                //this.dxlblerr.Text = "Price not available." + Environment.NewLine + "Unfortunately it was not possible to calculate a price using the information supplied." + Environment.NewLine + "Please check your input details or contact Publiship for more information.";
                                //this.dxpagepricer.ActiveTabIndex = 6;

                            }
                        }
                        else
                        {
                            _attempts += 1;
                            string _ex = _pvs[0].lcl_name == "exception" ? _pvs[0].lcl_loose_name.ToString() : "";
                            //this.dxlblerr.Text = "Price not available." + Environment.NewLine + "Unfortunately it was not possible to calculate a price using the information supplied." + Environment.NewLine + "Please check your input details or contact Publiship for more information." + _ex;
                            //this.dxpagepricer.ActiveTabIndex = 6;
                            _error += Environment.NewLine + Environment.NewLine + "Attempt " + _attempts.ToString() + ": Price not available." + Environment.NewLine + "Unfortunately it was not possible to calculate a price using the information supplied." + Environment.NewLine + _ex;

                        }
                    }
                    catch (Exception ex)
                    {
                        _attempts += 1;
                        string _ex = ex.Message.ToString();
                        //this.dxlblerr.Text = _ex;
                        //this.dxpagepricer.ActiveTabIndex = 6;
                        _error += Environment.NewLine + Environment.NewLine + "Attempt " + _attempts.ToString() + " Error: " + Environment.NewLine + _ex;
                    }
                } //end while

                if (_returned == false)
                {
                    string _mailsent = error_reported() == true ? Environment.NewLine + Environment.NewLine + "The details you entered have been forwarded to publiship for checking." : "";
                    this.dxlblerr.Text = _error + _mailsent;
                    this.dxpagepricer.ActiveTabIndex = 6;
                }
                else
                {
                    show_quote_tab();
                    //290113 clear tracking in hidden fields
                    if (this.dxhfpricer.Contains("title")) { this.dxhfpricer.Remove("title"); }
                    if (this.dxhfpricer.Contains("input")) { this.dxhfpricer.Remove("input"); }
                    if (this.dxhfpricer.Contains("cartons")) { this.dxhfpricer.Remove("cartons"); }
                }
            }//end if result != ""
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("pricer/price_quote", "publiship")); 
        }
    }

    /// <summary>
    /// have to do this on init as it's the only way to get items with an image and text!
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxrblintpe_Init(object sender, EventArgs e)
    {
        string[] _items = {"<img src='../Images/pricer/pricer_book.gif' ALIGN=ABSMIDDLE></img> Book dimensions", 
                              "<img src='../Images/pricer/pricer_carton.gif' ALIGN=ABSMIDDLE></img> Carton dimensions",
                              "<img src='../Images/pricer/pricer_paper.gif' ALIGN=ABSMIDDLE></img> Paper type and extent"};

        this.dxrblintpe.EncodeHtml = false;
        this.dxrblintpe.Items.Add(_items[0], 1);
        this.dxrblintpe.Items.Add(_items[1], 2);
        this.dxrblintpe.Items.Add(_items[2], 3);
        this.dxrblintpe.Items[0].Selected = true;

    }

    //end radiobutton init
    protected void dxbtnend0_Click(object sender, EventArgs e)
    {
        Response.Redirect("..//Default.aspx");
    }

    protected void show_quote_tab()
    {
        //change view depending on internal user or client
        int _pageidx = 5; //default to summary price
        if (Page.Session["user"] != null)
        {
            string _companyid = ((UserClass)Page.Session["user"]).CompanyId.ToString();
            if (_companyid == "-1") { _pageidx = 2; }
        }

        //set tabindex and buttons
        this.dxbtnquote0.ClientVisible = false;
        this.dxbtnback0.ClientVisible = true;
        this.dxbtnext0.ClientVisible = false;
        this.dxbtnprint0.ClientVisible = true;

        this.dxpagepricer.ActiveTabIndex = _pageidx;

        //hidden field so we know which quote tab to return to
        this.dxhfpricer.Set("tabb", _pageidx);

    }
    //end show quote tab 

    protected void set_input_type_panel(string dims)
    {
        switch (dims)
        {
            case "1":
                {
                    this.dxpnlclientbook.ClientVisible = true;
                    this.dxpnlclientcarton.ClientVisible = false;
                    this.dxpnlclientpaper.ClientVisible = false;
                    break;
                }
            case "2":
                {
                    this.dxpnlclientbook.ClientVisible = false;
                    this.dxpnlclientcarton.ClientVisible = true;
                    this.dxpnlclientpaper.ClientVisible = false;
                    break;
                }
            case "3":
                {
                    this.dxpnlclientbook.ClientVisible = false;
                    this.dxpnlclientcarton.ClientVisible = false;
                    this.dxpnlclientpaper.ClientVisible = true;
                    break;
                }
        }
        //end case statement

    }
    #endregion

    #region pricer specific functions
    /// <summary>
    /// calculate cart height 
    /// </summary>
    /// <param name="copycart">input copies per carton</param>
    /// <param name="bookdepth">input book depth</param>
    /// <param name="calchgt">calculated carton height</param>
    /// <returns></returns>
    protected Double get_cart_hgt(Double copycart, Double bookdepth, Double calchgt)
    {
        Double _hgt = 0;

        _hgt = copycart == 0 ? calchgt : ((copycart / 2) * bookdepth) + 25;

        return _hgt;
    }
    //end get_cart_hgt

    /// <summary>
    /// calculate cart weight 
    /// </summary>
    /// <param name="copycart"></param>
    /// <param name="bookdepth"></param>
    /// <param name="calchgt"></param>
    /// <returns></returns>
    protected Double get_cart_wht(Double copycart, Double bookweight, Double calcwht)
    {
        Double _wht = 0;

        _wht = copycart == 0 ? calcwht : ((copycart * bookweight) / 1000) + 1;

        return _wht;
    }
    //end get_cart_wht

    /// <summary>
    /// return type of pallet from database lookup
    /// </summary>
    /// <param name="zonename">final destination</param>
    /// <returns></returns>
    protected Int32 get_pallet_type(String zonename)
    {
        Int32 _pallet = 1;

        return _pallet;
    }

    /// <summary>
    /// check we have valid input dimensions, if they can't be derived from text boxes
    /// try hidden field/cookie tokens
    /// </summary>
    /// <param name="dims"></param>
    /// <returns></returns>
    protected string test_input_values(int dims)
    {
        string[] _test = { "", "", "", "" };
        string _result = "";

        switch (dims)
        {
            case 1:
                {
                    _test[0] = this.dxtxtlength.Text.ToString();
                    _test[1] = this.dxtxtwidth.Text.ToString();
                    _test[2] = this.dxtxtdepth.Text.ToString();
                    _test[3] = this.dxtxtweight.Text.ToString();
                    break;
                }
            case 2:
                {
                    _test[0] = this.dxtxtside1.Text.ToString();
                    _test[1] = this.dxtxtside2.Text.ToString();
                    _test[2] = this.dxtxtcartdepth.Text.ToString();
                    _test[3] = this.dxtxtcartweight.Text.ToString();
                    break;
                }
            case 3:
                {
                    _test[0] = this.dxtxtblock1.Text.ToString();
                    _test[1] = this.dxtxtblock2.Text.ToString();
                    _test[2] = this.dxtxtextent.Text.ToString();
                    _test[3] = this.dxcboPaper.Text.ToString(); //this.dxspinpaper.Text.ToString();
                    break;
                }
            default:
                {
                    break;
                }
        }//end switch

        //none of the input dimensions can be null
        _result = all_valid_inputs(String.Join(";", _test));
        if (_result == "")
        {
            _result = all_valid_inputs(get_saved_token("trackInput"));
        }
        //if (!string.IsNullOrEmpty(_test[0]) & !string.IsNullOrEmpty(_test[1]) & !string.IsNullOrEmpty(_test[2]) & !string.IsNullOrEmpty(_test[3]))
        //{
        //    _result = String.Join(";", _test);
        //}
        //else
        //{
        //    _result = get_saved_token("trackInput");
        //}

        return _result;
    }

    /// <summary>
    /// make sure all input values are > 0
    /// </summary>
    /// <param name="inputs">string input dimensions seperated by semi-colon</param>
    /// <returns></returns>
    protected string all_valid_inputs(string inputs)
    {

        string _result = "";
        bool _invalid = false;
        if (!string.IsNullOrEmpty(inputs))
        {
            int _ix = 0;

            string[] _in = inputs.Split(";".ToCharArray());

            while (_ix < _in.Length && _invalid == false)
            {
                if (string.IsNullOrEmpty(_in[_ix])) { _invalid = true; }
                _ix++;
            }

            if (!_invalid) { _result = inputs; }
        }
        return _result;
    }

    protected Double measurementsTosize(string svalue)
    {
        Double _result = 0;
        bool _test = Double.TryParse(svalue, out _result);

        return _result;
    }

    protected string test_title(int dims)
    {
        string _title = "";

        switch (dims)
        {
            case 1:
                {
                    _title = !string.IsNullOrEmpty(this.dxtxttitle.Text.ToString()) ? this.dxtxttitle.Text.ToString() : get_saved_token("title");
                    break;
                }
            case 2:
                {
                    _title = !string.IsNullOrEmpty(this.dxtxtcarttitle.Text.ToString()) ? this.dxtxtcarttitle.Text.ToString() : get_saved_token("title");
                    break;
                }
            case 3:
                {
                    _title = !string.IsNullOrEmpty(this.dxtxttitle3.Text.ToString()) ? this.dxtxttitle3.Text.ToString() : get_saved_token("title");
                    break;
                }
            default:
                {
                    break;
                }
        }

        return _title;
    }

    protected int test_cartons(int dims)
    {
        int _cartons = 0;

        switch (dims)
        {
            case 1:
                {
                    _cartons = !string.IsNullOrEmpty(this.dxtcarton.Text.ToString()) ? wwi_func.vint(this.dxtcarton.Text.ToString()) : wwi_func.vint(get_saved_token("cartons"));
                    break;
                }
            case 2:
                {
                    _cartons = !string.IsNullOrEmpty(this.dxtxtcartcopy.Text.ToString()) ? wwi_func.vint(this.dxtxtcartcopy.Text.ToString()) : wwi_func.vint(get_saved_token("cartons"));
                    break;
                }
            default:
                {
                    break;
                }
        }

        return _cartons;
    }
    //end test cartons
    #endregion
    
    #region databindings
    /// <summary>
    /// 110413 replace spinedit with combo to restrict range of values user can select
    /// get data off spreadsheet
    /// </summary>
    protected void bind_cbo_paper_gsm(int pricergroup)
    {
        //get databable off spreadsheet using identified cell range 
        //DataTable _dt = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet1", "A70", "A84");
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\pricer_range_iso.xml";

        DataSet _ds = new DataSet();
        _ds.ReadXml(_path);
        DataView _dv = _ds.Tables[0].DefaultView;
        if (pricergroup > 0)
        {
            _dv.RowFilter = "(group = '" + pricergroup.ToString() + "' AND listing = 'pallets')";
        }
        else
        {
            _dv.RowFilter = "(group = '*' AND listing = 'pallets')";
        }
        //get datatable off spreadsheet using identified cell range 
        DataTable _dt = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet1", _dv[0][1].ToString(), _dv[0][2].ToString());
        
        if (_dt != null)
        {
            //drop into a dataview so we can remove null elements which cause error "input string was not in the correct format"
            //DataView _dv = new DataView(_dt, "IsNull(item,'null') <> 'null'", "item ASC", DataViewRowState.OriginalRows);
            this.dxcboPaper.DataSource = _dt;
            this.dxcboPaper.ValueField = "item"; 
            this.dxcboPaper.TextField = "item"; 
            this.dxcboPaper.DataBind();
        }
    }
    /// <summary>
    /// databind origins list
    /// this is called when country changed
    /// or
    /// cbfinal has focus AND no data items
    /// </summary>
    protected void bind_cbo_origin()
    {
        //get company group or default to 0
        Int32 _cg = check_group(); //wwi_func.get_company_group();
 
        Query _qry = new Query(DAL.Pricer.Tables.PricerOriginPoint, "pricerprov").AddWhere("origin_point_id", Comparison.GreaterThan, 0).AND("company_group", Comparison.Equals, _cg).ORDER_BY("origin_point", "asc");
        PricerOriginPointCollection _origins = new PricerOriginPointCollection();
        _origins.LoadAndCloseReader(_qry.ExecuteReader());
  
        DataTable _dt = (DataTable)_origins.ToDataTable();
        this.dxcborigin.DataSource = _dt;
        this.dxcborigin.ValueField = "origin_point_id";
        this.dxcborigin.TextField = "origin_point";
        this.dxcborigin.DataBind();
    }
    //using spreadsheet depending on pricer group
    protected void bind_cbo_origin(int pricergroup)
    {
        try
        {
            //get cell range from xml file no hard coding for the cell ranges will make it easier to maintain
            //the origins, countrries and final destinations all have a specific range on the excel spreadsheet
            //this range can be different depending on the spreadsheet used 
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\pricer_range_iso.xml";

            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "group = '" + pricergroup.ToString() + "' AND listing = 'origin'";

            //get databable off spreadsheet using identified cell range 
            DataTable _dt = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet1", _dv[0][1].ToString(), _dv[0][2].ToString());

            if (_dt != null)
            {
                //drop into a dataview so we can remove null elements which cause error "input string was not in the correct format"
                //DataView _dv = new DataView(_dt, "IsNull(item,'null') <> 'null'", "item ASC", DataViewRowState.OriginalRows);
                this.dxcborigin.DataSource = _dt;
                this.dxcborigin.ValueField = "item_index"; //"origin_point_id";
                this.dxcborigin.TextField = "item"; //"origin_point";
                this.dxcborigin.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpagepricer.ActiveTabIndex = 6;
        }
    }
    //end bind combo
  
    /// <summary>
    /// databind country list when origin is selected 
    /// </summary>
    protected void bind_cbo_country(string originid)
    {
 
        //check origin
        int _originid = wwi_func.vint(originid);

        if (_originid > 0)
        {
            Query _qry = new Query(DAL.Pricer.Tables.PricerDestCountry, "pricerprov").AddWhere("origin_point_id", Comparison.Equals, _originid).ORDER_BY("country_name", "asc");
            PricerDestCountryCollection _countries = new PricerDestCountryCollection();
            _countries.LoadAndCloseReader(_qry.ExecuteReader());

            DataTable _dt = (DataTable)_countries.ToDataTable();
            this.dxcbcountry.DataSource = _dt;
            this.dxcbcountry.ValueField = "country_id";
            this.dxcbcountry.TextField = "country_name";
            this.dxcbcountry.DataBind();

        }
    }
    //using spreadsheet depending on pricer group
    protected void bind_cbo_country(int pricergroup, string originport)
    {
        try
        {
            if (!string.IsNullOrEmpty(originport))
            {
                //get cell range from xml file no hard coding for the cell ranges will make it easier to maintain
                //the origins, countrries and final destinations all have a specific range on the excel spreadsheet
                //this range can be different depending on the spreadsheet used 
                string _path = AppDomain.CurrentDomain.BaseDirectory;
                _path += "xml\\pricer_range_iso.xml";

                DataSet _ds = new DataSet();
                _ds.ReadXml(_path);
                DataView _dv = _ds.Tables[0].DefaultView;
                _dv.RowFilter = "(location ='" + originport + "') AND (group = '" + pricergroup.ToString() + "' OR group = '*') AND (listing = 'country')";

                //get databable off spreadsheet using identified cell range 
                //use the last data row if more than one as you might get 1 row back under * ans one row back under group
                int _ix = 0;
                if (_dv.Count > 1) { _ix = _dv.Count - 1; }
                //get databable off spreadsheet using identified cell range 
                DataTable _dt = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet3", _dv[_ix][1].ToString(), _dv[_ix][2].ToString());
                //DataTable _dt = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet3", _dv[0][1].ToString(), _dv[0][2].ToString());
                
                if (_dt != null)
                {
                    //drop into a dataview so we can remove null elements which cause error "input string was not in the correct format"
                    //DataView _dv = new DataView(_dt, "IsNull(item,'null') <> 'null'", "item ASC", DataViewRowState.OriginalRows);

                    this.dxcbcountry.DataSource = _dt;
                    this.dxcbcountry.ValueField = "item_index"; //"origin_point_id";
                    this.dxcbcountry.TextField = "item"; //"origin_point";
                    this.dxcbcountry.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpagepricer.ActiveTabIndex = 6;
        }
    }
    //end bind combo

    /// <summary>
    /// databind final destination list when country is selected 
    /// </summary>
    protected void bind_cbo_final(string countryid)
    {
        //check origin
        int _countryid = wwi_func.vint(countryid);
        Int32 _cg = check_group(); //wwi_func.get_company_group();

        if (_countryid > 0)
        {
            Query _qry = new Query(DAL.Pricer.Tables.PricerDestFinal, "pricerprov").AddWhere("dest_country_id", Comparison.Equals, _countryid).AND("company_group",Comparison.Equals, _cg) .ORDER_BY("dest_final", "asc");
            PricerDestFinalCollection _final = new PricerDestFinalCollection();
            _final.LoadAndCloseReader(_qry.ExecuteReader());

            DataTable _dt = (DataTable)_final.ToDataTable();
            this.dxcbfinal.DataSource = _dt;
            this.dxcbfinal.ValueField = "dest_final_id";
            this.dxcbfinal.TextField = "dest_final";
            this.dxcbfinal.DataBind();
        }
    }
    //end bind cbo final 
    //using spreadsheet depending on pricer group
    protected void bind_cbo_final(int pricergroup, string countryname)
    {
        try
        {
            this.dxcbfinal.DataSource = null;

            //get cell range from xml file no hard coding for the cell ranges will make it easier to maintain
            //the origins, countrries and final destinations all have a specific range on the excel spreadsheet
            //this range can be different depending on the spreadsheet used 
            if (!string.IsNullOrEmpty(countryname))
            {
                string _path = AppDomain.CurrentDomain.BaseDirectory;
                _path += "xml\\pricer_range_iso.xml";

                DataSet _ds = new DataSet();
                _ds.ReadXml(_path);
                DataView _dv = _ds.Tables[0].DefaultView;
                _dv.RowFilter = "(location ='" + countryname + "') AND (group = '" + pricergroup.ToString() + "' OR group = '*') AND (listing = 'final')";

                //get databable off spreadsheet using identified cell range 
                DataTable _dtfinal = wwi_file.getfromfile("~/Include/", pricergroup.ToString(), "sheet3", _dv[0][1].ToString(), _dv[0][2].ToString());

                 if (_dtfinal != null)
                {
                    //drop into a dataview so we can remove null elements which cause error "input string was not in the correct format"
                    //DataView _dv = new DataView(_dt, "IsNull(item,'null') <> 'null'", "item ASC", DataViewRowState.OriginalRows);

                    this.dxcbfinal.DataSource = _dtfinal;
                    this.dxcbfinal.ValueField = "item_index"; //"origin_point_id";
                    this.dxcbfinal.TextField = "item"; //"origin_point";
                    this.dxcbfinal.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpagepricer.ActiveTabIndex = 6;
        }
    }
    //end bind combo

    /// <summary>
    /// bind pallet types to dropdown from resource pricer_pallet_type. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_pallet(string svalue)
    {
        try
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ddl_items.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "ddls ='pallet'";

            this.dxcbotypepallet.DataSource = _dv;
            this.dxcbotypepallet.TextField = "name";
            this.dxcbotypepallet.ValueField = "value";
            this.dxcbotypepallet.DataBind();
            this.dxcbotypepallet.Value = svalue;  //"Standard";
            //this.dxcbotypepallet.SelectedItem = this.dxcbotypepallet.Items.FindByValue(svalue);  
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString()); 
        }
    }
    //end bind pallet type

    /// <summary>
    /// bind currency types to dropdown from resource pricer_currency. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_currency(string svalue)
    {
        try
        {
            //using a rsource does not work with the path when it's compiled
            //string[] res = GetType().Assembly.GetManifestResourceNames();
            //string _path = AppDomain.CurrentDomain.BaseDirectory;
            //_path += "App_globalResources\\pricer_currency.resx";
            //System.Resources.ResXResourceReader _rx = new System.Resources.ResXResourceReader(_path);
            //foreach (System.Collections.DictionaryEntry _d in _rx)
            //{
            //    this.dxcbocurrency.Items.Add(_d.Value.ToString(), _d.Value.ToString());    
            //}
            string _currency1 = svalue.Split(',').GetValue(0).ToString();
            string _currency2 = svalue.Split(',').GetValue(1).ToString(); //GPB, USD, etc. from full string which is formatted as Sterling (pence),GBP
  
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ddl_items.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "ddls ='currency'";

            this.dxcbocurrency.DataSource = _dv;
            this.dxcbocurrency.TextField = "name";
            this.dxcbocurrency.ValueField = "value";

            this.dxcbocurrency.DataBind();
            this.dxcbocurrency.Value  = _currency2; //"GBP";
            //this.dxcbocurrency.SelectedItem = this.dxcbocurrency.Items.FindByValue(_currency2);  
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //end bind currency type    

    /// <summary>
    /// databind final destination list when country is selected 
    /// </summary>
    protected void bind_cbo_contact()
    {
        //check controlling office id
        Int32 _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).controlOfficeId : 1; //default 1 - liverpool office?
        Int32 _oid = _cid >= 1? _cid: 1;

        if (_oid > 0)
        {
            string[] _cols = { "EmployeeID, Name, EmailAddress" };
            string[] _order = { "Name" }; 
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("OfficeID").IsEqualTo(_oid).And("Live").IsEqualTo(true).And("EmailAddress").IsNotNull().OrderAsc(_order) ;

            //EmployeesTableCollection  _contacts = new EmployeesTableCollection();
            //_contacts.LoadAndCloseReader(_qry.ExecuteReader());

            DataSet _dt = (DataSet)_qry.ExecuteDataSet(); //  _contacts.ToDataTable();
            this.dxcbocontact.DataSource = _dt;
            this.dxcbocontact.ValueField = "EmailAddress";
            this.dxcbocontact.TextField = "Name";
            this.dxcbocontact.DataBind();
        }
    }
    //end bind cbo contact

    protected void bind_cbo_pricers(int group)
    {
        try
        {
            string _dir = "~/Include/";
            string _pattern = "*.xls";

            List<string> _pricers = group == 0 ? wwi_file.get_files(_dir, _pattern) : wwi_file.get_files(_dir, _pattern, group);

            if (_pricers.Count > 0)
            {
                this.dxcbospreadsheet.DataSource = _pricers;
                this.dxcbospreadsheet.DataBind();

                if (group == 0)
                {
                    //publiship users default to office pricer
                    this.dxcbospreadsheet.Value = _pricers.Find(delegate(string _s) { return _s.StartsWith("Office"); });
                }
                else
                {
                    //clients will only have one option so default to 1st instance in list 
                    //this.dxcbospreadsheet.SelectedIndex = 0;
                    this.dxcbospreadsheet.Value = _pricers.Find(delegate(string _s) { return _s.StartsWith(_pricers[0]); });
                }
            }

            Int32 _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : 0;
            Boolean _visible = _cid == -1 ? true : false;
            this.dxcbospreadsheet.ClientVisible = _visible; //only publiship users can change the pricer
            this.dxlblspreadsheet.ClientVisible = _visible;
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            Response.Write(ex); 
        }
    }
    //end cbo pricers
    /// <summary>
    /// not being used
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkorigin_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //internal users identify group from selected spreadheet
        int _group = check_group();
        bind_cbo_origin(_group);  
    }
    /// <summary>
    /// callback group
    /// we have to use a callback panel because we need to be able to reset the cascading combos 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkgroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //internal users identify group from selected spreadheet
        int _group = check_group();
        
        //deprecated use xml for cell range and then derive list from spreadsheet easier to maintain than seperate database
        //as group spreadsheets can have different drop down options might as well just get from spreadsheet
        //bind_cbo_country(this.dxcborigin.Value != null ? this.dxcborigin.Value.ToString() : "0");
        
        //bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "0");
        
 
        string _ix = e.Parameter.ToString();
        switch (_ix) {
            case "-1":
                {
                    bind_cbo_origin(_group);  
                    break;
                }
            case "0": //callback from origin combo
                {
                    bind_cbo_country(_group, this.dxcborigin.Text != null ? this.dxcborigin.Text.ToString() : "");
                    this.dxcbcountry.Focus();
                    break;
                }
            case "1": //callback from country combo
                {
                    bind_cbo_final(_group, this.dxcbcountry.Text != null ? this.dxcbcountry.Text.ToString() : "");
                    this.dxcbfinal.Focus();
                    break;
                }
            default:
                {
                    this.dxcbcountry.Focus();
                    break;
                }
        }//end switch
    }
    //end callback group 


    /// <summary>
    /// get original price values from a previous quote and use in pricer
    /// </summary>
    /// <param name="quoteid"></param>
    protected void get_price_criteria(int quoteid, int pricergroup)
    {
        localhost.Publiship_Pricer _ws = new localhost.Publiship_Pricer();

        string _location = get_site_url(wwi_func.get_from_global_resx("web_site_location"));
        _ws.Url = _location;
        
        _ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

        string _params = _ws.GetQuoteCriteria(quoteid);
        //deserialise JSON string 
        JavaScriptSerializer _js = new JavaScriptSerializer();
        //using ilist
        IList<DAL.Services.uPriceParams> _val = _js.Deserialize<IList<DAL.Services.uPriceParams>>(_params);

        //common to all input types
        this.dxrblintpe.Items[_val[0].in_dimensions-1].Selected = true; 
        //11/08/2011 can use either 1st part of currency description (Text) or 2nd part (Value) to set currenct
        this.dxcbocurrency.Value  = _val[0].in_currency.Split(',').GetValue(1).ToString().Trim();
        
        //bound on load as it does as data does not need to change so we can get value immediately
        //this.dxcborigin.Value = this.dxcborigin.Items.FindByText(_val[0].origin_name != null ? _val[0].origin_name : ""); 
        //bind country amd final dlls based on selected text this way we can return the correct Value as well
        //we have to do this as otherwise the ddls will not have any data in them and therefore no valid value when we set the text
        string _orig = _val[0].origin_name != null ? _val[0].origin_name.ToString() : "";
        string _ctry = _val[0].country_name != null ? _val[0].country_name.ToString() : "";
        string _fnl = _val[0].final_name != null ? _val[0].final_name.ToString() : "";
        
        //120312 use datasource from pricer spreadsheet 
        //this.dxcborigin.Text = _val[0].origin_name != null? _val[0].origin_name:"";
        this.dxcborigin.SelectedItem = this.dxcborigin.Items.FindByText(_orig);
        //bind_cbo_country(this.dxcborigin.Value != null ? this.dxcborigin.Value.ToString() : "");
        bind_cbo_country(pricergroup, this.dxcborigin.Text);
        //this.dxcbcountry.Text = _ctry;
        this.dxcbcountry.SelectedItem = this.dxcbcountry.Items.FindByText(_ctry);  
        //120312 use datasource from pricer spreadsheet 
        //bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "");
        bind_cbo_final(pricergroup, this.dxcbcountry.Text);
        this.dxcbfinal.SelectedItem = this.dxcbfinal.Items.FindByText(_fnl);

        
        this.dxtxtcopies.Text = _val[0].tot_copies.ToString();
        this.dxcbotypepallet.Text = _val[0].in_pallet != null? _val[0].in_pallet.ToString():"";
        
        //change from defaults so we dispay correct info
        this.dxhfpricer.Set("dims", _val[0].in_dimensions);
        this.dxhfpricer.Set("crnc", _val[0].in_currency.ToString());
        this.dxhfpricer.Set("pall", this.dxcbotypepallet.Text.ToString());
        
        //specific to input type
        switch (_val[0].in_dimensions)
        {
            case 1:
                {
                    this.dxtxttitle.Text = _val[0].book_title != null?_val[0].book_title.ToString():"";
                    this.dxtxtlength.Text = _val[0].in_length.ToString();
                    this.dxtxtwidth.Text = _val[0].in_width.ToString(); 
                    this.dxtxtdepth.Text = _val[0].in_depth.ToString(); 
                    this.dxtxtweight.Text = _val[0].in_weight.ToString();
                    this.dxtcarton.Text = _val[0].copies_carton.ToString();  
                    break;
                }
            case 2:
                {
                    this.dxtxtcarttitle.Text = _val[0].book_title != null ? _val[0].book_title.ToString() : "";
                    this.dxtxtside1.Text = _val[0].in_length.ToString();
                    this.dxtxtside2.Text = _val[0].in_width.ToString();
                    this.dxtxtcartdepth.Text = _val[0].in_depth.ToString();
                    this.dxtxtcartweight.Text = _val[0].in_weight.ToString();
                    this.dxtxtcartcopy.Text = _val[0].copies_carton.ToString();  
                    break;
                }
            case 3:
                {
                    this.dxtxttitle3.Text = _val[0].book_title != null ? _val[0].book_title.ToString() : "";
                    this.dxtxtblock1.Text = _val[0].in_length.ToString();
                    this.dxtxtblock2.Text = _val[0].in_width.ToString();
                    this.dxtxtextent.Text = _val[0].in_extent.ToString();
                    //this.dxspinpaper.Text = _val[0].in_papergsm.ToString();
                    this.dxcboPaper.Text = _val[0].in_papergsm.ToString();
                    this.dxckcover.Checked = _val[0].in_hardback;  
                    break;
                }
        }//end switch
    }
    //end get price values
    #endregion

    #region callbacks


    protected void dxcboPaper_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int _group = check_group();
        bind_cbo_paper_gsm(_group);
    }
    /// <summary>
    /// Call back handler for when we use asyncallback above
    /// This code works
    /// BUT how do we get a page response once the call back is complete? 
    /// </summary>
    /// <param name="iar"></param>
    protected void callbackHandler(IAsyncResult iar)
    {
        try
        {
            //waithandle requires System.Threading enabled
            //WaitHandle _wait = iar.AsyncWaitHandle;
            //_wait.WaitOne(new TimeSpan(0, 15, 0), true); //wait for a max of 15 minutes 

            // Finalize the asynchronous call
            string _result = ((localhost.Publiship_Pricer)iar.AsyncState).EndGetBookQuote(iar);

        } // try
        catch (Exception ex)
        {
            string strMsg =
               String.Format("Error terminating asynchronous " +
               "call: '{0}'", ex.Message);

        } // catch

    }//end call back handler

    /// <summary>
    /// this is called when origin changed
    /// or
    /// cdbcountry has focus AND no data items
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbcountry_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (this.dxcbcountry.Items.Count <= 1)
        {
            bind_cbo_country(e.Parameter);
            this.dxcbcountry.SelectedIndex = -1;
        }
    }

    protected void dxcbfinal_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (this.dxcbfinal.Items.Count <= 1)
        {
            bind_cbo_final(e.Parameter);
            this.dxcbfinal.SelectedIndex = -1;
        }
    }
    /// <summary>
    /// get costing summary pre-palletised /loose / all
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkcosting_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _type = e.Parameter.ToString(); 
        string _squoteid = this.dxlblquote.Text.ToString();
        int _idquote = wwi_func.vint(_squoteid);

        if (_idquote > 0)
        {
            localhost.Publiship_Pricer _ws = new localhost.Publiship_Pricer();
            string _location = get_site_url(wwi_func.get_from_global_resx("web_site_location"));
            _ws.Url = _location;
            
            _ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

            string _costing = _ws.GetCostingSummary(_idquote, _type);
            //deserialise JSON string 
            JavaScriptSerializer _js = new JavaScriptSerializer();
            //using ilist
            IList<DAL.Services.uCostingSummary> _pvs = _js.Deserialize<IList<DAL.Services.uCostingSummary>>(_costing);

            this.dxlblcostingh1.Text = "Costing summary " + _type;
            this.dxlblcostingh2.Text = string.Format(this.dxlblcostingh2.Text, " " + this.dxtxtcopies.Text.ToString(), _type == "loose" ? " " + this.dxlblloosename.Text.ToString() : " " + this.dxlbllclname.Text.ToString()); 
            //pre-carriage
            this.dxlblpre1.Text = Math.Round(_pvs[0].pre_part,2).ToString();
            this.dxlblpre2.Text = Math.Round(_pvs[0].pre_full,2).ToString();
            this.dxlblpre3.Text = Math.Round(_pvs[0].pre_thc20,2).ToString();
            this.dxlblpre4.Text = Math.Round(_pvs[0].pre_thc40,2).ToString();
            this.dxlblpre5.Text = Math.Round(_pvs[0].pre_thclcl,2).ToString();
            this.dxlblpre6.Text = Math.Round(_pvs[0].pre_docs,2).ToString();
            this.dxlblpre7.Text = Math.Round(_pvs[0].pre_origin,2).ToString();
            this.dxlblpre8.Text = Math.Round(_pvs[0].pre_haul20,2).ToString();
            this.dxlblpre9.Text = Math.Round(_pvs[0].pre_haul40, 2).ToString();
            //freight
            this.dxlblfre1.Text = Math.Round(_pvs[0].freight_lcl, 2).ToString();
            this.dxlblfre2.Text = Math.Round(_pvs[0].freight_20, 2).ToString();
            this.dxlblfre3.Text = Math.Round(_pvs[0].freight_40, 2).ToString();
            this.dxlblfre4.Text = Math.Round(_pvs[0].freight_40hq, 2).ToString();
            //on destination
            this.dxlblonc1.Text = Math.Round(_pvs[0].on_dest_lcl, 2).ToString();
            this.dxlblonc2.Text = Math.Round(_pvs[0].on_pier_etc, 2).ToString();
            this.dxlblonc3.Text = Math.Round(_pvs[0].on_dest_20, 2).ToString();
            this.dxlblonc4.Text = Math.Round(_pvs[0].on_dest_40, 2).ToString();
            this.dxlblonc5.Text = Math.Round(_pvs[0].on_docs, 2).ToString();
            this.dxlblonc6.Text = Math.Round(_pvs[0].on_customs, 2).ToString();
            this.dxlblonc7.Text = Math.Round(_pvs[0].on_part, 2).ToString();
            this.dxlblonc8.Text = Math.Round(_pvs[0].on_full, 2).ToString();
            this.dxlblonc9.Text = Math.Round(_pvs[0].on_haul20, 2).ToString();
            this.dxlblonc10.Text = Math.Round(_pvs[0].on_haul40, 2).ToString();
            this.dxlblonc11.Text = Math.Round(_pvs[0].on_shunt20, 2).ToString();
            this.dxlblonc12.Text = Math.Round(_pvs[0].on_shunt40, 2).ToString();
            this.dxlblonc13.Text = Math.Round(_pvs[0].on_pallets, 2).ToString();
            this.dxlblonc14.Text = Math.Round(_pvs[0].on_other, 2).ToString();
        }
    }
    //end get costing summary

    /// <summary>
    /// shipment size data callback
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkshipment_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _squoteid = this.dxlblquote.Text.ToString();
        int _dims = wwi_func.vint(this.dxhfpricer.Get("dims").ToString());
        int _idquote = wwi_func.vint(_squoteid);

        if (_idquote > 0)
        {
            localhost.Publiship_Pricer _ws = new localhost.Publiship_Pricer();
            string _location = get_site_url(wwi_func.get_from_global_resx("web_site_location"));
            _ws.Url = _location;
            
            _ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

            string _shipment = _ws.GetShipmentSize(_idquote);
            //deserialise JSON string 
            JavaScriptSerializer _js = new JavaScriptSerializer();
            //using ilist
            IList<DAL.Services.uShipmentSize> _siz = _js.Deserialize<IList<DAL.Services.uShipmentSize>>(_shipment);

            //pre-palletised
            this.dxlblship1.Text = Math.Round(_siz[0].calc_copiescarton, 2).ToString(); //_dims ==1 ? this.dxtcarton.Text.ToString(): _dims==2? this.dxtxtcartcopy.Text.ToString(): "0"; 
            this.dxlblship2.Text = Math.Round(_siz[0].tot_cartons,2).ToString();
            this.dxlblship3.Text = Math.Round(_siz[0].pal_cartons,2).ToString();
            this.dxlblship4.Text = Math.Round(_siz[0].pal_full,2).ToString();
            this.dxlblship5.Text = Math.Round(_siz[0].pal_full_wt,2).ToString();
            this.dxlblship6.Text = Math.Round(_siz[0].pal_full_cu,2).ToString();
            this.dxlblship7.Text = Math.Round(_siz[0].pal_layers,2).ToString();
            this.dxlblship8.Text = Math.Round(_siz[0].pal_layer_count,2).ToString();
            this.dxlblship9.Text = Math.Round(_siz[0].pal_total_wt,2).ToString();
            this.dxlblship10.Text = Math.Round(_siz[0].pal_total_cu,2).ToString();
            this.dxlblship11.Text = Math.Round(_siz[0].pal_ratio,2).ToString();

            //cartons
            this.dxlblship12.Text = Math.Round(_siz[0].ctn_hgt,2).ToString();
            this.dxlblship13.Text = Math.Round(_siz[0].ctn_len,2).ToString();
            this.dxlblship14.Text = Math.Round(_siz[0].ctn_wid,2).ToString();
            this.dxlblship15.Text = Math.Round(_siz[0].ctn_wt,2).ToString();
            this.dxlblship16.Text = Math.Round(_siz[0].par_count,2).ToString();
            this.dxlblship17.Text = Math.Round(_siz[0].ctn_remaining,2).ToString();
            this.dxlblship18.Text = Math.Round(_siz[0].residue_cu,2).ToString();
            this.dxlblship19.Text = Math.Round(_siz[0].residue_wt,2).ToString();
            this.dxlblship20.Text = Math.Round(_siz[0].ctn_total_wt,2).ToString();
            this.dxlblship21.Text = Math.Round(_siz[0].ctn_total_cu,2).ToString();
            this.dxlblship22.Text = Math.Round(_siz[0].ctn_ratio, 2).ToString();
        }

    }
    //end shipment size callback
    #endregion
    
    #region email generation
    /// <summary>
    /// generate and send email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnemail_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.dxcbocontact.Text.ToString()))
        {
            int _dims = wwi_func.vint(this.dxhfpricer.Get("dims").ToString());

            string _user = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";

            StringBuilder _cc = new StringBuilder(); 
            //***************
            //040412 temporary fix until we can change database
            //look to see if company has a default contact email and use that instead of selected
            //replace company name ampersands, spaces and apostrophes 
            //and check in global for a contact email
            
            string _test = wwi_func.get_from_global_resx(this.dxtxtusercomp.Text.Replace("&", "").Replace(" ", "").Replace("'","").ToLower());
            if(string.IsNullOrEmpty(_test)){ _test = !string.IsNullOrEmpty(this.dxcbocontact.Value.ToString()) ? this.dxcbocontact.Value.ToString() : this.dxcbocontact.Text.ToString();}
            //***************
            if(!string.IsNullOrEmpty(_test)){ _cc.Append(_test + ";"); } 
            //NB: if you change the dropdown style to dropdown instead of dropdownlist, user can free type email address as well as select
            //for testing
            //_cc.Append("paule@publiship.com;pauled2109@hotmail.co.uk;");
            //291112 check for other users who receive pricer emails including services@publiship-online.com
            _test = wwi_func.get_from_global_resx("pricer_email").ToLower();
            if (!string.IsNullOrEmpty(_test)) { _cc.Append(_test); }
            //move to array
            string[] _to = _cc.ToString().Split(";".ToCharArray());
            //*******
            
            string _subject1 = "Quote Ref: " + this.dxlblquote.Text.ToString();
            string _subject2 = "P.O. Number: " + this.dxtxtponum.Text.ToString();
            string _tr = "<tr><td bgcolor=\"#e8edff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";
            string _tbl = "<p><table cellpadding=\"5px\" style=\"border-color: #669\">{0}</table></p>";
            string _copiescarton = _dims == 1? this.dxtcarton.Text.ToString(): _dims == 2? this.dxtxtcartcopy.Text.ToString(): "NA";

            StringBuilder _msg1 = new StringBuilder();
            _msg1.AppendFormat(_tr, "P.O Number: ", this.dxtxtponum.Text.ToString());
            _msg1.AppendFormat(_tr, "ISBN: ", this.dxtxtisbn.Text.ToString());
            _msg1.AppendFormat(_tr, "Impression: ", this.dxtxtimpression.Text.ToString());
            _msg1.AppendFormat(_tr, "Printer: ", this.dxtxtprinter.Text.ToString());
            _msg1.AppendFormat(_tr, "Ex-works date: ", this.dxdtexworks.Text.ToString());
            _msg1.AppendFormat(_tr, "Delivery due date: ", this.dxdtdue.Text.ToString());
            _msg1.AppendFormat(_tr, "Final destination: ", this.dxlblto.Text.ToString());
            _msg1.AppendFormat(_tr, "Comments: ", this.dxmemocomment.Text.ToString());
            _msg1.AppendFormat(_tr, "Sent by: ", this.dxtxtusername.Text.ToString());
            _msg1.AppendFormat(_tr, "Company: ", this.dxtxtusercomp.Text.ToString());
            _msg1.AppendFormat(_tr, "Contact Tel.: ", this.dxtxtusertel.Text.ToString());
            _msg1.AppendFormat(_tr, "Contact Email: ", this.dxtxtuseremail.Text.ToString());
            _msg1.AppendFormat(_tr, "Quote Ref: ", this.dxlblquote.Text.ToString());

            //200212 let clients see per copy price and title
            //120315 let clients see total copies
            StringBuilder _val1 = new StringBuilder();
            _val1.AppendFormat(_tr, "Title or description: ", this.dxlblbookname.Text.ToString());
            _val1.AppendFormat(_tr, "Per Copy price: ", this.dxlblppc3.Text.ToString());
            _val1.AppendFormat(_tr, "Total copies: ", this.dxtxtcopies.Text.ToString());
            _val1.AppendFormat(_tr, "Note: ", "Subject to agreed validity terms");
            //080115 not required just change text to 'subject to agreed validity terms' for all users
            //080115 do not include this line for HarperCollins
            //string _cid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).CompanyId.ToString() : "";
            //if (!wwi_func.find_in_xml("xml\\parameters.xml", "name", "companyid_no_message", "value", _cid))
            //{
            //    _val1.AppendFormat(_tr, "Note: ", "Quote held firm for orders placed within the next 7 days");
            //}

            StringBuilder _msg2 = new StringBuilder();
            _msg2.AppendFormat(_tr, "Title or description: ", this.dxlblbookname.Text.ToString());
            _msg2.AppendFormat(_tr, this.dxlblqot1.Text.ToString(), this.dxlblsize.Text.ToString());
            _msg2.AppendFormat(_tr, this.dxlblqot2.Text.ToString(), this.dxlblweight.Text.ToString());
            _msg2.AppendFormat(_tr, "Currency: ", this.dxcbocurrency.Text.ToString());
            _msg2.AppendFormat(_tr, "Pallet type: ", this.dxcbotypepallet.Text.ToString());
            _msg2.AppendFormat(_tr, "Origin: ", this.dxlblfrom.Text.ToString());
            _msg2.AppendFormat(_tr, "Destination country: ", this.dxcbcountry.Text.ToString());
            _msg2.AppendFormat(_tr, "Copies per carton: ", _copiescarton);
            _msg2.AppendFormat(_tr, "Final destination: ", this.dxlblto.Text.ToString());
            _msg2.AppendFormat(_tr, "Total copies: ", this.dxtxtcopies.Text.ToString());
            _msg2.AppendFormat(_tr, "Per Copy price: ", this.dxlblppc3.Text.ToString());
            _msg2.AppendFormat(_tr, "Total price: ", this.dxlbltotprice2.Text.ToString());
            _msg2.AppendFormat(_tr, "Using pricer: ", this.dxcbospreadsheet.Text.ToString());
                            
            string _emailed = MailHelper.gen_email(_to, true, _subject1 + " " + _subject2, String.Format(_tbl, _msg1.ToString() + _msg2.ToString() ), _user);
 
            //message if failed
            if (_emailed != string.Empty)
            {
                this.dxlblerr.Text = _emailed;
                this.dxpagepricer.ActiveTabIndex = 6;
            }
            else
            {
                //120911 confirmation email to client - they only get msg1 + val1, not price details in msg2
                if (!string.IsNullOrEmpty(this.dxtxtuseremail.Text.ToString()))
                {
                    string[] _confirm = { this.dxtxtuseremail.Text.ToString() };
                    _emailed = MailHelper.gen_email(_confirm, true, "Thank you for emailing us regarding " + _subject2, "<p><i>This is an automated response, you do not need to reply to this email.</i></p><p>For your reference, this is the information you requested:</p>" + String.Format(_tbl, _val1.ToString() + _msg1.ToString() ), _user);
                }

                append_to_email_log();
                show_quote_tab(); //return to quote 
            }

        }
    }

    //error message to IT dept as Publiship
    protected bool error_reported()
    {
        bool _result = false;

        int _dims = Math.Abs(wwi_func.vint(this.dxhfpricer.Get("dims").ToString()));

        string _uid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).UserId.ToString() : "";
        string _cid = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).CompanyId.ToString() : "";
        string _mto = Page.Session["user"] != null ? ((UserClass)Page.Session["user"]).mailTo.ToString() : "";

        StringBuilder _cc = new StringBuilder();
        //move to array
        string[] _to = { "services@publiship-online.com" };
        //*******

        string[] _inputs = test_input_values(_dims).Split(";".ToCharArray());
        string _title = test_title(_dims);
        int _cartons = test_cartons(_dims); 
        bool _cover = (bool)this.dxckcover.Checked;
        
        
        string _subject1 = "ERROR REPORT";
        string _tr = "<tr><td bgcolor=\"#e8edff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";
        string _tbl = "<p><table cellpadding=\"5px\" style=\"border-color: #669\">{0}</table></p>";
        
        StringBuilder _msg1 = new StringBuilder();
        _msg1.AppendFormat(_tr, "Dims: ", _dims.ToString());
        _msg1.AppendFormat(_tr, "Length: ", _inputs[0]);
        _msg1.AppendFormat(_tr, "Width: ", _inputs[1]);
        _msg1.AppendFormat(_tr, "Depth: ", _dims != 3? _inputs[2]: "");
        _msg1.AppendFormat(_tr, "Weight: ", _dims != 3? _inputs[3]: "");
        _msg1.AppendFormat(_tr, "Cartons: ", _cartons);
        _msg1.AppendFormat(_tr, "Paper size: ", _dims == 3? _inputs[2]: "");
        _msg1.AppendFormat(_tr, "Pager GSM: ", _dims == 3? _inputs[3]: "");
        _msg1.AppendFormat(_tr, "Hardback: ", _dims == 3? _cover.ToString(): "");
        _msg1.AppendFormat(_tr, "Company: ", _cid);
        _msg1.AppendFormat(_tr, "User: ",_uid);
        _msg1.AppendFormat(_tr, "User email: ", _mto);

        string _emailed = MailHelper.gen_email(_to, true, _subject1, String.Format(_tbl, _msg1.ToString()), "services@publiship-online.com");
        if (_emailed == string.Empty) { _result = true;  }//no error returned
        return _result;
    }
    //end error email
    //end send email
    protected void append_to_email_log()
    {
        //280212 append record to email log
        PricerEmailLog _log = new PricerEmailLog();
        _log.QuoteId = wwi_func.vint(this.dxlblquote.Text.ToString());
        _log.PoNumber = this.dxtxtponum.Text.ToString();
        _log.Isbn = this.dxtxtisbn.Text.ToString();
        _log.Printer = this.dxtxtprinter.Text.ToString();
        if (!string.IsNullOrEmpty(this.dxdtexworks.Text.ToString())) { _log.ExworksDate = Convert.ToDateTime(this.dxdtexworks.Text.ToString()); }
        if (!string.IsNullOrEmpty(this.dxdtdue.Text.ToString())) { _log.DueDate = Convert.ToDateTime(this.dxdtdue.Text.ToString()); }
        _log.Comments = this.dxmemocomment.Text.ToString();
        _log.SentBy = this.dxtxtusername.Text.ToString();
        _log.Company = this.dxtxtusercomp.Text.ToString();
        _log.ContactTel = this.dxtxtusertel.Text.ToString();
        _log.ContactEmail = this.dxtxtuseremail.Text.ToString();
        _log.PublishipContact = this.dxcbocontact.Text.ToString();
        _log.Save();
    }
    //end append to email log
#endregion

#region functions 
    /// <summary>
    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_saved_token(string namedtoken)
    {
        string _value = this.dxhfpricer.Contains(namedtoken) ? this.dxhfpricer.Get(namedtoken).ToString() : null;

        if(string.IsNullOrEmpty(_value)){
            _value = Page.Request.Cookies[namedtoken] != null ? HttpUtility.UrlDecode(Page.Request.Cookies[namedtoken].Value.ToString(), System.Text.Encoding.Default): null;
        }

        return _value;
        
        //used to replace this code
        //int _dims = wwi_func.vint(this.dxhfpricer.Get("dims").ToString());
        //string _curr = this.dxhfpricer.Get("crnc").ToString();
        //string _pall = this.dxhfpricer.Get("pall").ToString();

        //080112 if this information is not in hidden fields (seems to be problematic for some users? mac os?
        //use cookie values
        //if (_dims == 0) { _dims = Page.Request.Cookies["dims"] != null ? wwi_func.vint(Page.Request.Cookies["dims"].Value.ToString()) : 1; }
        //if (string.IsNullOrEmpty(_curr)) { _curr = Page.Request.Cookies["currency"] != null ? HttpUtility.UrlDecode(Page.Request.Cookies["currency"].Value.ToString(), System.Text.Encoding.Default) : "Sterling (pence),GBP"; }
        //if (string.IsNullOrEmpty(_pall)) { _pall = Page.Request.Cookies["pallet"] != null ? Page.Request.Cookies["pallet"].Value.ToString() : "Standard"; }
    }
    //end get saved token

    protected string get_site_url(string location)
    {
        //To specify a port for the ASP.NET Development Server (local host)
        //1.In Solution Explorer, click the name of the application.
        //2. In the Properties pane, click the down-arrow beside Use dynamic ports and select False from the dropdown list.
        //This will enable editing of the Port number property.
        //3. In the Properties pane, click the text box beside Port number and type in a port number.
        //4. Click outside of the Properties pane. This saves the property settings.
        //Each time you run a file-system Web site within Visual Web Developer, the ASP.NET Development Server will listen on the specified port.
        //Also make sure when testing local that the path is correct (ie: /WW_CRM_folders/)
        string _url = "";

        switch (location)
        {
            case "local":
                {
                    _url = "http://localhost:3644/WWI_CRM_folders/services/Service_Pricer.asmx";
                    break;
                }
            case "online":
                {
                    _url = "http://www.publiship-online.com/services/Service_Pricer.asmx";
                    break;
                }
            case "online-test":
                {
                    _url = "http://www.publiship-online-test.dtemp.net/services/Service_Pricer.asmx";
                    break;
                }
            default:
                {
                    _url = "http://www.publiship-online.com/services/Service_Pricer.asmx";
                    break;
                }
        }
        return _url;
    }
    //end get site url
    protected int check_group()
    {
        Int32 _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : 0;
        int _group = 0;

        if (_cid != -1)
        {
            _group = wwi_func.get_company_group();
        }
        else
        {
            string _pricer = this.dxcbospreadsheet.SelectedItem != null ? this.dxcbospreadsheet.Value.ToString() : "";
            if (_pricer != "") {
                string[] _subs = _pricer.Split("_".ToCharArray());  
                _group = wwi_func.vint(_subs[0]); }
        }

        return _group;
    }
#endregion

}

