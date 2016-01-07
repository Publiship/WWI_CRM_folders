using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
//using System.Threading; need to enable this for asyn operations
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPager;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;

public partial class register_and_quote : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        Int32 _uid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
        this.dxpagepricer.ActiveTabIndex = 0;
        //*********** DO NOT CHANGE ********
        this.dxhfpricer.Set("tabb", 5); //DO NOT CHANGE THIS ensure default is user can only see the client quote form
        //*********** END DO NOT CHANGE ********
        this.dxbtnquote0.ClientVisible = false;
        this.dxbtnback0.ClientVisible = false;

   
            if (!Page.IsPostBack)
            {
                //user info to form
                //this.dxtxtusername.Text = (string)((UserClass)Page.Session["user"]).UserName;
                //this.dxtxtusercomp.Text = (string)((UserClass)Page.Session["user"]).OfficeName;
                //this.dxtxtuseremail.Text = (string)((UserClass)Page.Session["user"]).mailTo;
                //this.dxtxtusertel.Text = (string)((UserClass)Page.Session["user"]).telNo;

                //bind ddls
                bind_cbo_origin();
                bind_cbo_country(this.dxcborigin.Value != null ? this.dxcborigin.Value.ToString() : "0");
                bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "0");
                bind_cbo_currency();
                bind_cbo_reg_country(); //registration country 

                //if ((Int32)((UserClass)Page.Session["user"]).CompanyId == -1) //internal users  
                //{
                //    bind_cbo_pallet(); //only for internal users
                //    this.dxlbltypepallet.ClientVisible = true;
                //    this.dxcbotypepallet.ClientVisible = true;
                //}
                //else
                //{
                //    bind_cbo_contact(); //only for external users contacts by cotnrolling office 
                //    this.dxlbltypepallet.ClientVisible = false;
                //    this.dxcbotypepallet.ClientVisible = false;
                //
                //}
                    this.dxlbltypepallet.ClientVisible = false;
                    this.dxcbotypepallet.ClientVisible = false;
                                
                //defaults
                if (!this.dxhfpricer.Contains("dims"))
                {
                    this.dxhfpricer.Add("dims", 1);
                } //default input type

                if (!this.dxhfpricer.Contains("crnc"))
                {
                    this.dxhfpricer.Add("crnc", "Sterling (pence)");
                } //default currency

                if (!this.dxhfpricer.Contains("pall"))
                {
                    this.dxhfpricer.Add("pall", "Standard");
                } //default type of pallet
            }
            else
            {
                //have to do this here?
                string _dims = this.dxhfpricer.Get("dims").ToString()
;
                switch (_dims)
                {
                    case "1":
                        {
                            this.dxpanelbook.ClientVisible = true;
                            this.dxpanelcarton.ClientVisible = false;
                            this.dxpanelpaper.ClientVisible = false;
                            break;
                        }
                    case "2":
                        {
                            this.dxpanelbook.ClientVisible = false;
                            this.dxpanelcarton.ClientVisible = true;
                            this.dxpanelpaper.ClientVisible = false;
                            break;
                        }
                    case "3":
                        {
                            this.dxpanelbook.ClientVisible = false;
                            this.dxpanelcarton.ClientVisible = false;
                            this.dxpanelpaper.ClientVisible = true;
                            break;
                        }
                }
                //end case statement
            }

    }




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
    /// pager page index changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxpgrpricer_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxPager _pager = sender as ASPxPager;
        this.dxpagepricer.ActiveTabIndex = _pager.PageIndex;

    }

    /// <summary>
    /// get quote on this form simply sends quore info back to publiship, does not trigger web service
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnget_Click(object sender, EventArgs e)
    {
        bool _failed = true;

        try
        {
            if (this.dxcapt1.IsValid)
            {
                //build contact object
                Registration1 _reg = new Registration1();

                _reg.RegDate = DateTime.Now;
                _reg.RegName1 = this.dxtxtname1.Text.ToString();
                _reg.RegName2 = this.dxtxtname2.Text.ToString();
                _reg.RegCompany = this.dxtxtcompany.Text.ToString();
                _reg.RegTel = this.dxtxtphone.Text.ToString();
                _reg.RegEmail = this.dxtxtemail.Text.ToString();
                _reg.RegExtra = this.dxmemoadd.Text.ToString();
                _reg.RegMailing = (bool)this.dxckmailing.Value;
                _reg.RegWhere = this.dxcbowhere.Value.ToString();
                _reg.RegIp = wwi_func.user_RequestingIP();
                _reg.RegCountry = this.dxcboregcountry.Text.ToString(); 

                //quote info
                int _dims = wwi_func.vint(this.dxhfpricer.Get("dims").ToString());
                string _curr = this.dxhfpricer.Get("crnc").ToString();
                string _pall = this.dxhfpricer.Get("pall").ToString();
                
                _reg.QinCurrency = _curr;
                _reg.QinDimensions = _dims;
                _reg.QinPallet = _pall;
                _reg.QoriginName = this.dxcborigin.Text.ToString();
                _reg.QfinalName = this.dxcbfinal.Text.ToString();
                _reg.QtotCopies = wwi_func.vint(this.dxtxtcopies.Text.ToString());
 
                switch (_dims)
                {
                    case 1:
                        {
                            _reg.QbookTitle = this.dxtxttitle.Text.ToString();
                            _reg.QinLength = wwi_func.vdouble(this.dxtxtlength.Text.ToString());
                            _reg.QinWidth = wwi_func.vdouble(this.dxtxtwidth.Text.ToString());
                            _reg.QinDepth = wwi_func.vdouble(this.dxtxtdepth.Text.ToString());
                            _reg.QinWeight = wwi_func.vdouble(this.dxtxtweight.Text.ToString());
                            _reg.QinExtent = 0;
                            _reg.QinPapergsm = 0;
                            _reg.QinHardback = false;
                            _reg.QcopiesCarton = wwi_func.vint(this.dxtcarton.Text.ToString());
                            break;
                        }
                    case 2:
                        {
                            _reg.QbookTitle = this.dxtxtcarttitle.Text.ToString();
                            _reg.QinLength = wwi_func.vdouble(this.dxtxtside1.Text.ToString());
                            _reg.QinWidth = wwi_func.vdouble(this.dxtxtside2.Text.ToString());
                            _reg.QinDepth = wwi_func.vdouble(this.dxtxtcartdepth.Text.ToString());
                            _reg.QinWeight = wwi_func.vdouble(this.dxtxtcartweight.Text.ToString());
                            _reg.QinExtent = 0;
                            _reg.QinPapergsm = 0;
                            _reg.QinHardback = false;
                            _reg.QcopiesCarton = wwi_func.vint(this.dxtxtcartcopy.Text.ToString());
                            break;
                        }
                    case 3:
                        {
                            _reg.QbookTitle = this.dxtxttitle3.Text.ToString();
                            _reg.QinLength = wwi_func.vdouble(this.dxtxtblock1.Text.ToString());
                            _reg.QinWidth = wwi_func.vdouble(this.dxtxtblock2.Text.ToString());
                            _reg.QinDepth = 0;
                            _reg.QinWeight = 0;
                            _reg.QinExtent = wwi_func.vdouble(this.dxtxtextent.Text.ToString());
                            _reg.QinPapergsm = wwi_func.vdouble(this.dxspinpaper.Text.ToString());
                            _reg.QinHardback = (bool)this.dxckcover.Checked;
                            _reg.QcopiesCarton = wwi_func.vint(this.dxtxtcartcopy.Text.ToString());
                            break;
                        }
                }
                //save!
                _reg.Save();
                //Int32 _newid = (Int32)_reg.GetPrimaryKeyValue();
                //successful save notify publiship
                
                string _tr = "<tr><td bgcolor=\"#e8edff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";

                string _msg1 = String.Format(_tr, "Name", _reg.RegName1.ToString() + ' ' + _reg.RegName2.ToString()) +
                                String.Format(_tr, "Company", _reg.RegCompany.ToString()) +
                                String.Format(_tr, "Country", _reg.RegCountry.ToString()) +
                                String.Format(_tr, "Phone", _reg.RegTel.ToString()) +
                                String.Format(_tr, "Email", _reg.RegEmail.ToString()) +
                                String.Format(_tr, "Comments", _reg.RegExtra.ToString()) +
                                String.Format(_tr, "Currency", _reg.QinCurrency.ToString());

                string _msg2 = "<p>Your quote request has been received, and we will be in touch soon to discuss your requirements.</p><p>For your own reference, here is the information you sent us:</p>";

                string _quote = String.Format(_tr, "Input type", _reg.QinDimensions == 1 ? "Book dimensions" : _reg.QinDimensions == 2 ? "Carton dimesnions" : "Paper type and extent") +
                                String.Format(_tr, "Title or description", _reg.QbookTitle.ToString()) +
                                String.Format(_tr, _dims == 1 ? "Length (mm)" : _dims == 2 ? "Longest side (mm)" : "Block (longest)", _reg.QinLength.ToString()) +
                                String.Format(_tr, _dims == 1 ? "Width (mm)" : _dims == 2 ? "Next side (mm)" : "Block (other side)", _reg.QinWidth.ToString()) +
                                String.Format(_tr, _dims != 3 ? "Depth (mm)" : "Extent (pages)", _dims != 3 ? _reg.QinDepth.ToString() : _reg.QinExtent.ToString()) +
                                String.Format(_tr, _dims == 1 ? "Weight (grams)" : _dims == 2 ? "Weight (kg)" : "Paper type gsm", _dims != 3 ? _reg.QinWeight.ToString() : _reg.QinPapergsm.ToString()) +
                                String.Format(_tr, _dims != 3 ? "Copies per carton" : "Hardback", _dims != 3 ? _reg.QcopiesCarton.ToString() : _reg.QinHardback == true ? "Yes" : "No") +
                                String.Format(_tr, "Origin", _reg.QoriginName.ToString()) +
                                String.Format(_tr, "Final destination", _reg.QfinalName.ToString()) +
                                String.Format(_tr, "Total copies", _reg.QtotCopies.ToString());


                //email to publiship
                string _emailed = "";
                string[] _to = { "discount@publiship.com", "paule@publiship.com" }; //"discount@publiship.com", "paule@publiship.com"
                _emailed = MailHelper.gen_email(_to, true, "Registration and Quote " + _reg.RegCompany.ToString(), "<table cellpadding=\"5px\" style=\"border-color: #669\">" +  _msg1 +  _quote +  "</table>", "");
                //anckowledgement to visitor
                if (!string.IsNullOrEmpty(_reg.RegEmail.ToString()))
                {
                    string[] _ak = { _reg.RegEmail.ToString() };
                    _emailed = MailHelper.gen_email(_ak, true, "Thank you for your quote request", _msg2 + "<table cellpadding=\"5px\" style=\"border-color: #669\">" + _quote + "</table><p>Regards</p><p>The Publiship Team</p>", "");
                }
                _failed = false;
            }

        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
        
        }
        finally{
       
            if(_failed)
            {
                this.dxbtnquote0.ClientVisible = true;
                this.dxbtnback0.ClientVisible = true;
                this.dxbtnext0.ClientVisible = false;
                this.pnlmsg1.Visible = true;
                this.pnlmsg2.Visible = false;
            }
            else
            {
                this.dxbtnquote0.ClientVisible = false;
                this.dxbtnback0.ClientVisible = false;
                this.dxbtnext0.ClientVisible = false;
                this.pnlmsg2.Visible = true;
                this.pnlmsg1.Visible = false;
            }

            
            this.dxpagepricer.ActiveTabIndex = 2;
            
        }
    }
    //end qet quote
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.publiship.com");
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
    /// cascading combos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbcountry_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_cbo_country(e.Parameter);

    }

    protected void dxcbfinal_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_cbo_final(e.Parameter);
    }

    /// <summary>
    /// databind origins list
    /// </summary>
    protected void bind_cbo_origin()
    {
        Query _qry = new Query(DAL.Pricer.Tables.PricerOriginPoint, "pricerprov").AddWhere("origin_point_id", Comparison.GreaterThan, 0).ORDER_BY("origin_point", "asc");
        PricerOriginPointCollection _origins = new PricerOriginPointCollection();
        _origins.LoadAndCloseReader(_qry.ExecuteReader());

        DataTable _dt = (DataTable)_origins.ToDataTable();
        this.dxcborigin.DataSource = _dt;
        this.dxcborigin.ValueField = "origin_point_id";
        this.dxcborigin.TextField = "origin_point";
        this.dxcborigin.DataBind();
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

    //end bind combo

    /// <summary>
    /// databind final destination list when country is selected 
    /// </summary>
    protected void bind_cbo_final(string countryid)
    {
        //check origin
        int _countryid = wwi_func.vint(countryid);

        if (_countryid > 0)
        {
            Query _qry = new Query(DAL.Pricer.Tables.PricerDestFinal, "pricerprov").AddWhere("dest_country_id", Comparison.Equals, _countryid).ORDER_BY("dest_final", "asc");
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

    /// <summary>
    /// bind pallet types to dropdown from resource pricer_pallet_type. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_pallet()
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
            this.dxcbotypepallet.DataBind();
            this.dxcbotypepallet.Value = "Standard";
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
    protected void bind_cbo_currency()
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
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\ddl_items.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            _dv.RowFilter = "ddls ='currency'";

            this.dxcbocurrency.DataSource = _dv;
            this.dxcbocurrency.DataBind();
            this.dxcbocurrency.Value = "GBP";
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //end bind currency type    

    /// <summary>
    /// bind country names to dropdown from resource pricer_pallet_type. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_reg_country()
    {
        try
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\country_iso.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            //_dv.RowFilter = "ddls ='pallet'";

            this.dxcboregcountry.DataSource = _dv;
            this.dxcboregcountry.DataBind();
            this.dxcboregcountry.Value = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //end bind reg country

    /// <summary>
    /// callback group
    /// we have to use a callback panel because wee need to be able to reset the cascding combos 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkgroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        bind_cbo_country(this.dxcborigin.Value != null ? this.dxcborigin.Value.ToString() : "0");
        bind_cbo_final(this.dxcbcountry.Value != null ? this.dxcbcountry.Value.ToString() : "0");

        string _ix = e.Parameter.ToString();
        switch (_ix)
        {
            case "0": //callback from origin combo
                {
                    this.dxcbcountry.Focus();
                    break;
                }
            case "1": //callback from country combo
                {
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
    /// have to do this on init as it's the only way to get items with an image and text!
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxrblintpe_Init(object sender, EventArgs e)
    {
        string[] _items = {"<img src='Images/pricer/pricer_book.gif' ALIGN=ABSMIDDLE></img> Book dimensions", 
                              "<img src='Images/pricer/pricer_carton.gif' ALIGN=ABSMIDDLE></img> Carton dimensions",
                              "<img src='Images/pricer/pricer_paper.gif' ALIGN=ABSMIDDLE></img> Paper type and extent"};

        this.dxrblintpe.EncodeHtml = false;
        this.dxrblintpe.Items.Add(_items[0], 1);
        this.dxrblintpe.Items.Add(_items[1], 2);
        this.dxrblintpe.Items.Add(_items[2], 3);
        this.dxrblintpe.Items[0].Selected = true;

    }

    //end radiobutton init
    protected void dxbtnend0_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.publiship.com"); 
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

        this.dxpagepricer.ActiveTabIndex = _pageidx;

        //hidden field so we know which quote tab to return to
        this.dxhfpricer.Set("tabb", _pageidx);

    }
    //end show quote tab 

}

