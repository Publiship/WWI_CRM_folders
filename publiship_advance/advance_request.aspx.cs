using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;
using ParameterPasser;
using DevExpress.Web.ASPxGridView;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

public partial class advance_request : System.Web.UI.Page
{
     

    protected void Page_Load(object sender, EventArgs e)
    {
        //string _test = get_titles_html(120600259);
       
        if (Page.Session["user"] != null)
        {
            string _orderid = Request.QueryString["oid"] != null ? Request.QueryString["oid"].ToString() : null;   //Request.QueryString["oid"] != null ? wwi_func.vint(Request.QueryString["oid"].ToString()) : 0;
            UserClass _user = (UserClass)Page.Session["user"];

            if (!string.IsNullOrEmpty(_orderid))
            {
                order_DataBind(wwi_func.vint(_orderid), _user.OfficeName.ToString(), _user.CompanyId.ToString());
            }

            this.dxlblsender2.Text = _user.OfficeName.ToString();
            this.dxlblprinter1.Text = _user.CompanyId.ToString();
            this.dxlblprinter2.Text = _user.UserId.ToString();

            //bind current cartons in code behind as we append to this grid using callbacks
            //which don't force a declarative datasource to be refreshed
            //currentcartons_DataBind_Db4(_user.UserName.Replace(" ", ""));
            currentcartons_DataBind();
            this.dxgrdtitles.DataBind();

            if (!Page.IsPostBack && !Page.IsCallback)
            {
                bind_cbo_country();
                //some testing stiff for local db4objects
                //test_data(_user.UserName,false);
            }
            else //pop up control with previous addresses callback fired event from jaavscript
            {
                bind_cbo_addresses(_user.CompanyId);      
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("publiship_advance/advance_request", "publiship")); 
        }


        //rebind summary on postback
        if (!this.dxhforder.Contains("orderid")) { this.dxlblorderno2.Text = "[New order]"; }
        this.dxpageorder.ActiveTabIndex = 0;
    
    }

    /// <summary>
    /// used for testing the db4objects engine 
    /// unique guids are generated for each record inserted to book title/carton records
    /// </summary>
    /// <param name="username"></param>
    /// <param name="addtest"></param>
    protected void test_data(string username, bool addtest)
    {
        if (addtest)
        {
            username = username.Replace(" ", "");
            db4o_advance.BookTitle _t = new db4o_advance.BookTitle(username, "test title 2");

            for (int ix = 0; ix < 3; ix++)
            {
                db4o_advance.Carton _c = new db4o_advance.Carton(10 * (ix + 1), 20 * (ix + 1), 30 * (ix + 1), 40 * (ix + 1));

                _t.addCarton(_c);
            }

            string _titleid = db4o_advance.InsertTitle(_t); 
        }
       
        List<db4o_advance.BookTitle> _n = db4o_advance.SelectByTitle("test title 1", 0, 0);
    }

 #region callback, postback events
    /// <summary>
    /// //postback to get order id, why does this not retain info using client side functionality?!
    /// ie: dxcallorder_Callback (-action = 0)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnorder_Click(object sender, EventArgs e)
    {
        Int32 _newvalue = 0;

        string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : null;
        if (!string.IsNullOrEmpty(_orderid))
        {
            update_order(wwi_func.vint(_orderid));
        }
        else
        {
            _newvalue = append_order();
            this.dxhforder.Set("orderid", _newvalue.ToString());
            this.dxlblorderno2.Text = _newvalue.ToString();
        }
        this.dxpageorder.ActiveTabIndex = this.dxpageorder.ActiveTabIndex + 1; 
    }
    //end order click

    /// <summary>
    /// update title, again why can't this be done on postback?
    /// e: dxcallorder_Callback (-action = 1)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtntitle_Click(object sender, EventArgs e)
    {
        string _titleid = this.dxhforder.Contains("titleid") ? this.dxhforder.Get("titleid").ToString() : null;
        string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : null;
        Int32 _newvalue = 0; 

        if (!string.IsNullOrEmpty(_orderid))
        {
            if (!string.IsNullOrEmpty(_titleid))
            {
                update_title(wwi_func.vint(_titleid), this.dxcbotitle.Text.ToString());
            }
            else
            {
                _newvalue = append_title(wwi_func.vint(_orderid), this.dxcbotitle.Text.ToString());
                this.dxhforder.Set("titleid", _newvalue.ToString());
                currentcartons_DataBind();
            }

            this.dxlblcartons.Text = "Enter carton details for " + this.dxcbotitle.Text.ToString();
            this.dxpageorder.ActiveTabIndex = 2; 
        }
        else
        {
            show_error_message("An order needs to be created before you can add titles.");
        }
        
    }
    //end title save

    /// <summary>
    /// callback to get order, title id's asynchronously
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcallorder_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string _action = e.Parameter.ToString();
        Int32 _newvalue = 0;

        switch (_action) //append order
        {
            case "0": //append order info to table and return new orderid
                {
                    string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : null;
                    if (!string.IsNullOrEmpty(_orderid))
                    {
                        update_order(wwi_func.vint(_orderid));
                    }
                    else
                    {
                        _newvalue = append_order();
                        this.dxhforder.Set("orderid", _newvalue.ToString());
                        this.dxlblorderno2.Text = _newvalue.ToString(); 
                    }
                    break;
                }
            case "1": //title(s)
                {
                    string _titleid = this.dxhforder.Contains("titleid") ? this.dxhforder.Get("titleid").ToString() : null;
                    string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : null;

                    if (!string.IsNullOrEmpty(_orderid))
                    {
                        if (!string.IsNullOrEmpty(_titleid))
                        {
                            update_title(wwi_func.vint(_titleid), this.dxcbotitle.Text.ToString());
                        }
                        else
                        {
                            _newvalue = append_title(wwi_func.vint(_orderid), this.dxcbotitle.Text.ToString());
                            this.dxhforder.Set("titleid", _newvalue.ToString());
                        }

                        this.dxlblcartons.Text = "Enter carton details for " + this.dxcbotitle.Text.ToString();
                    }
                    else
                    {
                        show_error_message("An order needs to be created before you can add titles.");
                    }
                    //_newvalue  = this.dxcbotitle.SelectedIndex; 
                    //if (_newvalue == -1) {
                    //    _newvalue = append_title(_orderid, this.dxcbotitle.Text.ToString());
                    //}

                    break;
                }
            case "2": //carton(s)
                {
                    string _titleid = this.dxhforder.Contains("titleid") ? this.dxhforder.Get("titleid").ToString() : null;
                    if (!string.IsNullOrEmpty(_titleid))
                    {
                        _newvalue = append_carton(wwi_func.vint(_titleid)); //returns total cartons for 
                        this.dxhforder.Set("cartons", _newvalue);
                        //this.dxgrdcurrentcartons.DataBind();
                    }
                    break;
                }
            case "3": //new title
                {
                    kill_title_details();
                    this.dxpageorder.ActiveTabIndex = 1;
                    break;
                }
            case "4": //new order
                {
                    kill_order_details();
                    this.dxpageorder.ActiveTabIndex = 0;
                    break;
                }
            case "5": //reprint labels
                {
                    //get label data into datatable create pdf
                    labels_ExportToPdf();
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    //end callback

    protected void dxgrdcurrentcartons_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        this.dxgrdcurrentcartons.DataBind(); 
    }

    protected void dxbtnsummary_Click(object sender, EventArgs e)
    {
        string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : "";

        //summary info of order details
        this.dxlbltabsummary.Text = "Summary of order " + _orderid;  
        this.dxlblsumm2.Text = this.dxtxtpayee.Text.ToString();
        this.dxlblsumm4.Text = this.dxmemaddress.Text.Replace(Environment.NewLine,", ").ToString();
        this.dxlblsumm6.Text = this.dxtxtfao.Text.ToString();
        this.dxlblsumm8.Text = this.dxcbocountry.Text.ToString();
  
        //cartons and titles
        this.dxpageorder.ActiveTabIndex = 3;
        this.dxgrdtitles.DetailRows.ExpandAllRows();

    }

 
    protected void dxbtnsave_Click(object sender, EventArgs e)
    {
        Int32 _orderid = this.dxhforder.Contains("orderid") ?  wwi_func.vint(this.dxhforder.Get("orderid").ToString()) : 0;
        
        if(_orderid > 0)
        {
            set_tallies(_orderid);
            generate_email(_orderid.ToString());
            show_finish_message(); 
            //change tab on client side or conflicks with pdf generation
            //if all done success message
        }
        //end if
    }
    protected void show_finish_message()
    {
        //change tab on client side or conflicts with pdf generation
        //if all done success message
        string _msg = wwi_func.get_from_global_resx("advance_finish").ToString().Replace(":", Environment.NewLine);
        this.dxlblmsg.Text = _msg;

        //does not work!
        this.dxpageorder.ActiveTabIndex = 5; //message page
        //this.dxpnlFinish.ClientVisible = true;
    }

    protected void show_error_message(string err)
    {
        this.dxlblerr.Text = err;
        this.dxpageorder.ActiveTabIndex = 4; //error page
    }
    //end show messages


    /// <summary>
    /// commands to add new order or new title
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnaddorder_Click(object sender, EventArgs e)
    {
        kill_order_details();
        
        currentcartons_DataBind(); //databinds to empty dataset (titleid is empty)
        this.dxpageorder.ActiveTabIndex = 0; //new title
    
    }
    protected void dxbtnaddtitle_Click(object sender, EventArgs e)
    {
        kill_title_details();
        this.dxlblcartons.Text = "Enter carton details";
        currentcartons_DataBind(); //databinds to empty dataset (titleid is empty)
        this.dxpageorder.ActiveTabIndex = 1; //new title
    }
    /// <summary>
    /// option to reprint pdfs have to do full post back or it's not thread safe
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnpdf_Click(object sender, EventArgs e)
    {
        string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : "";

        if (!string.IsNullOrEmpty(_orderid))
        { labels_ExportToPdf(); }
    }

    /// <summary>
    /// exit button clear current order details redirext to default 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnexit_Click(object sender, EventArgs e)
    {
        kill_order_details();
        Response.Redirect("~/default.aspx"); 
    }

    /// <summary>
    /// remove current title details
    /// </summary>
    protected void kill_title_details()
    {
        //clear current title and carton info
        this.dxcbotitle.Text = "";
        if (this.dxhforder.Contains("titleid"))
        {
            this.dxhforder.Set("titleid", "");

        }
    }
    /// <summary>
    /// clear all current data
    /// </summary>
    protected void kill_order_details()
    {
        //clear all
        this.dxlblorderno2.Text = "[New order]";
        this.dxlblcartons.Text = "Enter carton details";
        this.dxcbotitle.Text = "";
        this.dxmemaddress.Text = "";
        this.dxtxtfao.Text = "";
        this.dxtxtpayee.Text = "";

        if (this.dxhforder.Contains("titleid"))
        {
            this.dxhforder.Set("titleid", "");

        }
        if (this.dxhforder.Contains("orderid"))
        {
            this.dxhforder.Set("orderid", "");

        }
        this.dxhforder.Clear();
 
    }
    //end kill order
#endregion

 #region databinding

    protected void objdstitleCartons_Selecting_Deprecated(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 _titleid = this.dxhforder.Contains("titleid") ? wwi_func.vint(this.dxhforder.Get("titleid").ToString()) : -1;
        e.InputParameters["PATitleID"] = _titleid;
    }

    protected void objdsTitles_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 _orderid = this.dxhforder.Contains("orderid") ? wwi_func.vint(this.dxhforder.Get("orderid").ToString()) : -1;
        e.InputParameters["PAOrderID"] = _orderid;
    }

    protected void order_DataBind(Int32 orderid, string sender, string printerid)
    {
        PublishipAdvanceOrderTable _o = new PublishipAdvanceOrderTable(orderid);
        if (_o != null)
        {
            this.dxlblorderno2.Text = _o.OrderID.ToString();
            this.dxmemaddress.Text = _o.DeliveryAddress.ToString();
            this.dxtxtfao.Text = _o.Fao.ToString();
            this.dxlblsender2.Text = sender;
            this.dxlblprinter2.Text = printerid; 
        }
    }

    /// <summary>
    /// bingding to db4ojects local data source 
    /// database is identified by user as tempdb + {DateTime.Now.Ticks + _ + userid
    /// </summary>
    protected void currentcartons_DataBind_Db4(string username)
    {
        
    }

    /// <summary>
    /// bind to  current cartons objectdatasource in code
    /// </summary>
    protected void currentcartons_DataBind()
    {
        string _titleid = this.dxhforder.Contains("titleid") ? this.dxhforder.Get("titleid").ToString() : "-1";

        if (!string.IsNullOrEmpty(_titleid))
        {
            ObjectDataSource _ds = new ObjectDataSource();
            _ds.TypeName = "DAL.Logistics.PublishipAdvanceCartonTableCustomcontroller";
            //select
            _ds.SelectMethod = "CartonsFetchByPATitleID";
            _ds.SelectParameters.Add("PATitleID", DbType.Int32, _titleid);
            //delete
            _ds.DeleteMethod = "CartonDelete";
            _ds.DeleteParameters.Add("PubAdvCartonID", DbType.Int32, "-1");
            //update
            _ds.UpdateMethod = "CartonUpdate";
            _ds.UpdateParameters.Add("PubAdvCartonID", DbType.Int32, "-1");
            _ds.UpdateParameters.Add("PATitleID", DbType.Int32, "-1");
            _ds.UpdateParameters.Add("CartonLength", DbType.Decimal, "-1");
            _ds.UpdateParameters.Add("CartonWidth", DbType.Decimal, "-1");
            _ds.UpdateParameters.Add("CartonHeight", DbType.Decimal, "-1");
            _ds.UpdateParameters.Add("CartonWeight", DbType.Decimal, "-1");


            this.dxgrdcurrentcartons.DataSource = _ds;
            this.dxgrdcurrentcartons.KeyFieldName = "PubAdvCartonID";
            this.dxgrdcurrentcartons.DataBind();
        }
        else
        {
            this.dxgrdcurrentcartons.DataSource = null;
            this.dxgrdcurrentcartons.KeyFieldName = "";
            this.dxgrdcurrentcartons.DataBind();
        }
    }
    //end current cartons databind

    
    protected void grdcartons_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _cartons = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "PATitleID" };
        Int32 _titleid = (Int32)_cartons.GetMasterRowFieldValues(_keys);

        if (_titleid > 0)
        {
            objdsCartons.SelectParameters["PATitleID"].DefaultValue = _titleid.ToString(); 
        }
    }

    /// <summary>
    /// make sure order id is set when adding a new title on summary grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdtitles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //ASPxGridView _titles = (ASPxGridView)sender;
        //get titleid for new carton
        if (e.NewValues["PAOrderID"] == null)
        {
            string _orderid = this.dxhforder.Contains("orderid")? this.dxhforder.Get("orderid").ToString() : "-1";  
            //e.NewValues["PaTitleID"] = _titleid;
            this.objdsTitles.InsertParameters["PAOrderID"].DefaultValue = _orderid;
        }
    }
    /// <summary>
    /// details row on insert need to set titleid or will get an error when saving
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdcartons_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _cartons = (ASPxGridView)sender;
        //get titleid for new carton
        if (e.NewValues["PaTitleID"] == null)
        {
            string _titleid = _cartons.GetMasterRowKeyValue().ToString();
            //e.NewValues["PaTitleID"] = _titleid;
            this.objdsCartons.InsertParameters["PATitleID"].DefaultValue = _titleid; 
        }
    }
    //end inserting

    /// <summary>
    /// hide buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdtitles_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }
    protected void dxgrdcartons_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

#endregion

 #region email output
    protected void generate_email(string orderid)
    {
        try
        {
            //************
            //100713 this code deprecated as it does not output full column headings since we upgraded to lasted versioon of DevExpress controls
            //heartile sick of problems like this since the control upgrade
            //DevExpress.Web.ASPxGridView.Export.Helper.GridViewLink _link = new DevExpress.Web.ASPxGridView.Export.Helper.GridViewLink(this.dxgrdexport);
            //DevExpress.XtraPrinting.PrintingSystem _ps = _link.CreatePS();
            //
            //System.IO.MemoryStream _stream = new System.IO.MemoryStream();
            //_ps.ExportToHtml(_stream);
            //
            //string _html = System.Text.Encoding.UTF8.GetString(_stream.GetBuffer());
            //end extract html 
            //************
            int _ncartons = wwi_func.vint(this.dxlblcartons1.Text.ToString());
            string _html = _ncartons > 0? get_cartons_html(orderid): get_titles_html(orderid);
            //emails to hk ofice, helen and printer
            string _user = "";
            string _client = "";
            string _company = "";


            
            if (Page.Session["user"] != null)
            {
                _user = (string)((UserClass)Page.Session["user"]).UserName;
                _client = (string)((UserClass)Page.Session["user"]).mailTo;
                _company = (string)((UserClass)Page.Session["user"]).OfficeName;
            }

            string _cc = wwi_func.get_from_global_resx("advance_email").ToString();
            if(!string.IsNullOrEmpty(_client)) { _cc = _cc + ";" + _client; }
            string[] _to = _cc.Split(";".ToCharArray());
                       
            //use for testing
            //string[] _to = { "paule@publiship.com" };
            //_user = "";
            /////

            //build email content as a table
            string _subject = "Publiship Advance Consignment Ref: " + orderid;
            
            string _tr = "<tr><td bgcolor=\"#ffffff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";
            string _tbl = "<p><table cellpadding=\"5px\" style=\"border-color: #a9a9a9\">{0}</table></p>";

            string _msg1 = String.Format(_tr, "Consignment Ref: ", orderid) +
                                String.Format(_tr, "Delivery to: ", this.dxmemaddress.Text.Replace(Environment.NewLine,", ").ToString()) +
                                String.Format(_tr, "Destination country: ", this.dxcbocountry.Text.ToString()) +
                                String.Format(_tr, "Cargo ready date: ", this.dxdtcargoready.Text.ToString()) +
                                String.Format(_tr, "Attention: ", this.dxtxtfao.Text.ToString()) +
                                String.Format(_tr, "Sender: ", this.dxlblsender2.Text.ToString())+
                                String.Format(_tr, "Order created by: ", _user) +
                                String.Format(_tr, "", _company) +
                                String.Format(_tr, "Title(s): ", this.dxlbltitles1.Text.ToString())+
                                String.Format(_tr, "Carton(s): ", this.dxlblcartons1.Text.ToString());

            string _emailed = MailHelper.gen_email(_to, true, _subject, String.Format(_tbl, _msg1) + "<div style=\"padding-top:20px\">" + _html + "</div>", _user);
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpageorder.TabIndex = 4; //error page
        }
    }

    protected string get_cartons_html(string orderid)
    {
        //html table formating
        string _htmltable = "<p><table cellpadding=\"4px\" cellspacing=\"0px\"; border=\"1\" style=\"border: solid 1px #aca899; border-collapse:collapse;\">{0}</table></p>";
        //html formatting for rows
        //if cartons couunt = 0 we don't need columns for carton dimensions
        string _htmlrow = "<tr><td bgcolor=\"#e8edff\">{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";
        
        //id comparisons
        string _currentid = null;
        string _id = null;
        //riw pos in reader
        int _row = 1; 
        //stringbuilder for table
        StringBuilder _msg = new StringBuilder();
        
        //get titles and cartons based on order id
        string[] _cols2 = { "PublishipAdvanceTitleTable.PATitleID", "PublishipAdvanceTitleTable.PAOrderID", "PublishipAdvanceTitleTable.Title", "PublishipAdvanceCartonTable.CartonLength", "PublishipAdvanceCartonTable.CartonWidth", "PublishipAdvanceCartonTable.CartonHeight", "PublishipAdvanceCartonTable.CartonWeight" };
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols2).From("PublishipAdvanceTitleTable").LeftOuterJoin("PublishipAdvanceCartonTable", "PATitleID", "PublishipAdvanceTitleTable", "PATitleID").Where("PAOrderID").IsEqualTo(orderid).OrderAsc("PATitleID");
        IDataReader _rd = _query.ExecuteReader();

        //append header row
        string[] _items = { "Title", "Carton #", "Carton Length", "Carton Width", "Carton Height", "Carton Weight" };
        _msg.AppendFormat(_htmlrow, _items);
  
        while (_rd.Read())
        {
            _id = _rd["PATitleID"] != null ? _rd["PATitleID"].ToString() : "";  //_rd["Title"] != null ? _rd["Title"].ToString() : ""; 
            string _title = "";

            if (_id != _currentid || _row == 1)
            {
                //new title row to table 
                _currentid = _id;
                _title = _rd["Title"] != null ? _rd["Title"].ToString() : "";
            }

            //append new row to table
            _items[0] = _title;
            _items[1] = _row.ToString();
            _items[2] = _rd["CartonLength"] != null ? _rd["CartonLength"].ToString() : "";
            _items[3] = _rd["CartonWidth"] != null ? _rd["CartonWidth"].ToString() : "";
            _items[4] = _rd["CartonHeight"] != null ? _rd["CartonHeight"].ToString() : "";
            _items[5] = _rd["CartonWeight"] != null ? _rd["CartonWeight"].ToString() : "";
            _msg.AppendFormat(_htmlrow, _items);
            //increment row number
            _row++;
        }

        _htmltable = string.Format(_htmltable, _msg.ToString());
        return  _htmltable;
    }

    protected string get_titles_html(string orderid)
    {
        //html table formating
        string _htmltable = "<p><table cellpadding=\"4px\" cellspacing=\"0px\"; border=\"1\" style=\"border: solid 1px #aca899; border-collapse:collapse;\">{0}</table></p>";
        //html formatting for rows
        //if cartons couunt = 0 we don't need columns for carton dimensions
        string _htmlrow = "<tr><td bgcolor=\"#ffffff\">{0}</td></tr>";

        //id comparisons
        string _currentid = null;
        string _id = null;
        //riw pos in reader
        int _row = 1;
        //stringbuilder for table
        StringBuilder _msg = new StringBuilder();

        //get titles and cartons based on order id
        string[] _cols2 = { "PublishipAdvanceTitleTable.PATitleID", "PublishipAdvanceTitleTable.PAOrderID", "PublishipAdvanceTitleTable.Title" };
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols2).From("PublishipAdvanceTitleTable").Where("PAOrderID").IsEqualTo(orderid).OrderAsc("PATitleID");
        IDataReader _rd = _query.ExecuteReader();

        //append header row
        _msg.AppendFormat(_htmlrow, "Title");

        while (_rd.Read())
        {
            _id = _rd["PATitleID"] != null ? _rd["PATitleID"].ToString() : "";  //_rd["Title"] != null ? _rd["Title"].ToString() : ""; 
            string _title = "";

            if (_id != _currentid || _row == 1)
            {
                //new title row to table 
                _currentid = _id;
                _title = _rd["Title"] != null ? _rd["Title"].ToString() : "";
            }

            //append new row to table
            _msg.AppendFormat(_htmlrow, _title);
            //increment row number
            _row++;
        }

        _htmltable = string.Format(_htmltable, _msg.ToString());
        return _htmltable;
    }
#endregion

 #region PDF output
    
    /// <summary>
    /// can't use threadpool causes error when generating pdf
    /// </summary>
    protected void labels_async_request()
    { 
        System.Threading.ThreadPool.QueueUserWorkItem(delegate
        {
                labels_ExportToPdf();
         });
    }
    /// <summary>
    /// bind to  current cartons objectdatasource in code
    /// </summary>
    protected void labels_ExportToPdf()
    {
        Int32 _orderid = this.dxhforder.Contains("orderid") ? wwi_func.vint(this.dxhforder.Get("orderid").ToString()) : -1; //"120600061"
        string _err = "";

        if (_orderid > -1)
        {
            _err = itextsharp_out.advance_labels(_orderid);
            if (_err == "")
            {
                show_finish_message();
            }
            else
            {
                show_error_message(_err);
            }//end if
        }
    }
    //end pdf
    #endregion

 #region incremental filtering for previous titles
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbotitle_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        //restricted to logged in user
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;
        int _printerid = wwi_func.vint(this.dxlblprinter2.Text.ToString());

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";
        {
            //use datareaders - much faster than loading into collections
            string[] _cols = { "PublishipAdvanceTitleTable.PATitleID, PublishipAdvanceTitleTable.Title, PublishipAdvanceOrderTable.PrinterID" };
            //string[] _cols = { "PublishipAdvanceTitleTable.PATitleID, PublishipAdvanceTitleTable.Title" };

            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PublishipAdvanceOrderTable).LeftOuterJoin("PublishipAdvanceTitleTable", "PAOrderID", "PublishipAdvanceOrderTable", "OrderID").Paged(e.BeginIndex + 1, e.EndIndex + 1, "PATitleID").WhereExpression("Title").Like(string.Format("%{0}%", e.Filter.ToString())).And("PrinterId").IsEqualTo(_printerid);
            //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PublishipAdvanceTitleTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "PATitleID").WhereExpression("Title").Like(string.Format("%{0}%", e.Filter.ToString()));
           
            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "PATitleID";
            _combo.TextField = "Title";
            _combo.DataBind();
        }
    }
    protected void dxcbotitle_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;
        int _printerid = wwi_func.vint(this.dxlblprinter2.Text.ToString());
  
        //use datareaders - much faster than loading into collections
        string[] _cols = { "PublishipAdvanceTitleTable.PATitleID, PublishipAdvanceTitleTable.Title, PublishipAdvanceOrderTable.PrinterID" };

        Int32 _id = wwi_func.vint(e.Value.ToString());
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PublishipAdvanceOrderTable).LeftOuterJoin("PublishipAdvanceTitleTable", "PAOrderID", "PublishipAdvanceOrderTable", "OrderID").WhereExpression("PATitleID").IsEqualTo(_id).And("PrinterId").IsEqualTo(_printerid);
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "PATitleID";
        _combo.TextField = "Title";
        _combo.DataBind();

    }
    //end incremental filtering of company name
#endregion

 #region addresses by companyid

    protected void bind_cbo_addresses(Int32 companyid)
    { 
         SubSonic.SqlQuery query = new SubSonic.SqlQuery();
         IDataReader _rd;
         
        try
         {
            //testing************
            //DataTable _dt = new DataTable();
            //_dt.Columns.Add("DeliveryAddress", typeof(string));
            //_dt.Columns.Add("DestinationCountry", typeof(string));
            //_dt.Columns.Add("ID", typeof(int));
            //DataRow _dr1 = _dt.NewRow();
            //_dr1["DeliveryAddress"] = "Test address 1";
            //_dr1["DestinationCountry"] = "UK";
            //_dr1["ID"] = 1;
            //_dt.Rows.Add(_dr1);
            //DataRow _dr2 = _dt.NewRow();
            //_dr2["DeliveryAddress"] = "Another address";
            //_dr2["DestinationCountry"] = "USA";
            //_dr2["ID"] = 2;
            //_dt.Rows.Add(_dr2);
            //this.dxcboaddress.DataSource = _dt;
            //this.dxcboaddress.ValueField = "ID";
            //this.dxcboaddress.DataBind(); 

            string[] _cols = { "PublishipAdvanceOrderTable.Payee", "PublishipAdvanceOrderTable.DeliveryAddress, PublishipAdvanceOrderTable.DestinationCountry, PublishipAdvanceOrderTable.CompanyID, PublishipAdvanceOrderTable.OrderID" };
             
            if (companyid != -1)
            {
                query = DAL.Logistics.DB.Select(_cols).From("PublishipAdvanceOrderTable").Where("CompanyID").IsEqualTo(companyid).And("DeliveryAddress").IsNotNull().Distinct().OrderAsc("Payee");
            }
            else
            {
                query = DAL.Logistics.DB.Select(_cols).From("PublishipAdvanceOrderTable").And("DeliveryAddress").IsNotNull().Distinct().OrderAsc("Payee");
            }

            //string _sql = query.ToString();
            _rd = query.ExecuteReader();
             
            //this.dxcboaddress.DataSource = _rd;
            //need a unique value field or data will not bind, but no need to specify textfield as we have multiple columnns
            //this.dxcboaddress.ValueField = "OrderID"; 
            //this.dxcboaddress.DataBind();
            this.dxgridAddress.DataSource = _rd;
            this.dxgridAddress.KeyFieldName = "OrderID";
            this.dxgridAddress.DataBind(); 

         }
         catch (Exception ex)
         {
             show_error_message(ex.Message.ToString());  
         }
    }
#endregion

 #region database operations

    /// <summary>
    /// append to order table
    /// </summary>
    /// <returns>new record id of order 0 if failed</returns>
    protected Int32 append_order()
    {
        Int32 _newid = 0;
        DateTime? _dtnull = null;
        publishipadvanceOrderTableCustomcontroller _o = new publishipadvanceOrderTableCustomcontroller();
        int _len = this.dxmemaddress.Text.Length > 200 ? 200 : this.dxmemaddress.Text.Length;
        string _addr = wwi_func.no_sql(this.dxmemaddress.Text.ToString().Substring(0, _len));  
        string _dest = this.dxcbocountry.Text != null? this.dxcbocountry.Text.ToString(): "";
        DateTime? _cargo = this.dxdtcargoready.Text != ""? wwi_func.vdatetime(this.dxdtcargoready.Text): _dtnull;

        _newid = _o.PartialInsert(this.dxlblorderno2.Text, this.dxtxtpayee.Text, _addr, _dest, wwi_func.vint(this.dxlblprinter1.Text), wwi_func.vint(this.dxlblprinter2.Text),  _cargo , this.dxtxtfao.Text);
        this.dxhforder.Set("orderid", _newid.ToString());

        return _newid;
    }
    //end append order
    /// <summary>
    /// update if we have an order id
    /// </summary>
    /// <param name="orderid"></param>
    protected void update_order(Int32 orderid)
    {
        //string _addr = this.dxmemaddress.Text.Length > 200 ? this.dxmemaddress.Text.Substring(0, 200).ToString() : this.dxmemaddress.Text.ToString();
        int _len = this.dxmemaddress.Text.Length > 200 ? 200 : this.dxmemaddress.Text.Length;
        string _addr = wwi_func.no_sql(this.dxmemaddress.Text.ToString().Substring(0, _len));  
        string _dest = this.dxcbocountry.Text != null ? this.dxcbocountry.Text.ToString() : "";
        DateTime _cargo = wwi_func.vdatetime(this.dxdtcargoready.Text);

        PublishipAdvanceOrderTable _o = new PublishipAdvanceOrderTable(orderid);
        _o.Payee = this.dxtxtpayee.Text.ToString();
        _o.DeliveryAddress = _addr;
        _o.DestinationCountry = _dest;
        _o.CargoReadyDate = _cargo;
        _o.Fao  = this.dxtxtfao.Text.ToString();
        _o.Save(); 
  
        //PublishipAdvanceOrderTableCollection _o = new PublishipAdvanceOrderTableCollection().Where("OrderID", SubSonic.Comparison.Equals, orderid).Load();
        //_o[0].Payee = this.dxtxtpayee.Text.ToString();
        //_o[0].DeliveryAddress = this.dxmemaddress.Text.ToString();
        //_o[0].Fao  = this.dxtxtfao.Text.ToString();
        //_o[0].Cartons = this.dxhforder.Contains("cartons") ? wwi_func.vint(this.dxhforder.Get("cartons").ToString()) : 0;  
        //_o[0].Save(); 
  
    }
    //end update order

    /// <summary>
    /// append new title to db
    /// </summary>
    /// <param name="orderid">int32</param>
    /// <param name="newtitle">string</param>
    /// <returns>new record id of title</returns>
    protected Int32 append_title(Int32 orderid, string newtitle)
    {
        Int32 _newid = 0;

            if (!string.IsNullOrEmpty(newtitle))
            {
                //append title with new id
                PublishipAdvanceTitleTable _t = new PublishipAdvanceTitleTable();
                _t.PAOrderID = orderid;
                _t.Title = newtitle;
                _t.Save();

                //get new id
                _newid = (Int32)_t.GetPrimaryKeyValue();
            }
       
        return _newid;
    }
    /// <summary>
    /// update title if we have a titleid
    /// </summary>
    /// <param name="titleid"></param>
    protected void update_title(Int32 titleid, string newtitle)
    {
        PublishipAdvanceTitleTable _t = new PublishipAdvanceTitleTable(titleid);
        _t.Title = newtitle;
        _t.Save(); 
        //PublishipAdvanceTitleTableCollection _t = new PublishipAdvanceTitleTableCollection().Where("PATitleID", SubSonic.Comparison.Equals, titleid).Load();
        //_t[0].Title = newtitle;
        //_t[0].Save(); 
    }
    //end update title

    protected Int32 append_carton(Int32 titleid)
    {
        Int32 _cartons = 0;
        int _copies = wwi_func.vint(this.dxtxtcount.Text.ToString());
 
        //append N cartons with title id
        PublishipAdvanceCartonTableCollection _c = new PublishipAdvanceCartonTableCollection();
        
            for (int _ix = 0; _ix < _copies; _ix++)
            {
                PublishipAdvanceCartonTable _n = new PublishipAdvanceCartonTable();
                _n.PATitleID = titleid;
                _n.CartonLength = Convert.ToDecimal( this.dxtxtlength.Text.ToString());
                _n.CartonWidth = Convert.ToDecimal( this.dxtxtwidth.Text.ToString());
                _n.CartonWeight = Convert.ToDecimal( this.dxtxtweight.Text.ToString());
                _n.CartonHeight = Convert.ToDecimal( this.dxtxtheight.Text.ToString());
                _c.Add(_n);

            }
        _c.SaveAll();

        _cartons = _c.Count;
        return _cartons;
    }
    //end save cartons
    /// <summary>
    /// append cartons stored in cache datatable 
    /// </summary>
    /// <returns>number of rows</returns>
    protected Int32 append_cartons_DEPRECATED(Int32 titleid)
    {
        Int32 _cartons = 0;
        //append cartons with title id
        PublishipAdvanceCartonTableCollection _c = new PublishipAdvanceCartonTableCollection();
        DataTable _dt = (DataTable)Cache["cartontable"];
        if (_dt != null)
        {
            for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
            {
                PublishipAdvanceCartonTable _n = new PublishipAdvanceCartonTable();
                _n.PATitleID = titleid;
                _n.CartonLength = (decimal)_dt.Rows[_ix]["length"];
                _n.CartonWidth = (decimal)_dt.Rows[_ix]["width"];
                _n.CartonWeight = (decimal)_dt.Rows[_ix]["weight"];
                _n.CartonHeight = (decimal)_dt.Rows[_ix]["height"];
                _c.Add(_n);

            }
        }
        _c.SaveAll();

        _cartons = _dt.Rows.Count;
        return _cartons;
    }
    //end save cartons

    /// <summary>
    /// return tallies for titles and cartons for current order and update order table
    /// </summary>
    /// <param name="orderid"></param>
    protected void set_tallies(Int32 orderid)
    {
        
        if(orderid > 0)
        {
            int _titles = get_tally_title(orderid);
            int _cartons = get_tally_cartons(orderid); 

            //update order table
            PublishipAdvanceOrderTable _o = new PublishipAdvanceOrderTable(orderid);
            _o.Cartons = _cartons;
            _o.Titles = _titles; 
            _o.Save();

            this.dxlbltitles1.Text = _titles.ToString();
            this.dxlblcartons1.Text = _cartons.ToString(); 

        }
    }

    protected int get_tally_title(Int32 orderid)
    {
        int _tally  = new Select(PublishipAdvanceTitleTable.PAOrderIDColumn).From<PublishipAdvanceTitleTable>().
            Where(PublishipAdvanceTitleTable.PAOrderIDColumn).IsEqualTo(orderid).GetRecordCount();
        return _tally;
    }

    protected int get_tally_cartons(Int32 orderid)
    {

        int _tally = new Select(PublishipAdvanceCartonTable.PubAdvCartonIDColumn).From<PublishipAdvanceCartonTable>().
           LeftOuterJoin("PublishipAdvanceTitleTable", "PATitleID", "PublishipAdvanceCartonTable", "PATitleID").
           Where(PublishipAdvanceTitleTable.PAOrderIDColumn).IsEqualTo(orderid).GetRecordCount();

        return _tally;
    }
    //end get tallies

    /// bind country names to dropdown from resource pricer_pallet_type. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_country()
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

            this.dxcbocountry.DataSource = _dv;
            this.dxcbocountry.DataBind();
            this.dxcbocountry.Value = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //end bind country
#endregion

}
