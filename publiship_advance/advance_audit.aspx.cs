using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;
using DevExpress.Web.ASPxGridView;
using ParameterPasser;

public partial class advance_audit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLoggedIn())
        {
             //check comapnyid if not publiship useer redirect to restricted version of this page
            Int32 _companyid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : 0;
            if (_companyid == -1)
            {
                //new linq databinding 
                //this method of using linq does not run in server mode, you MUST use a LinqServerModeDataSource
                //bind_linq_datasource(); 
                //running in server mode
                if (!Page.ClientScript.IsClientScriptBlockRegistered("lg_key"))
                {
                    register_client_scripts();
                }

                //bind gridview countries on page load
                GridViewDataComboBoxColumn _cbo = this.dxgrdorders.Columns["DestinationCountry"] as GridViewDataComboBoxColumn;
                if (_cbo != null)
                {
                    string _path = AppDomain.CurrentDomain.BaseDirectory;
                    _path += "xml\\country_iso.xml";

                    // pass _qryFilter to have keyword-filter RSS Feed
                    // i.e. _qryFilter = XML -> entries with XML will be returned
                    DataSet _ds = new DataSet();
                    _ds.ReadXml(_path);
                    DataView _dv = _ds.Tables[0].DefaultView;
                    //_dv.RowFilter = "ddls ='pallet'";

                    _cbo.PropertiesComboBox.DataSource = _dv;
                    _cbo.PropertiesComboBox.TextField = "name";
                    _cbo.PropertiesComboBox.ValueField = "name";
                }

                //using objectdatasource
                //this.LinqServerModePricer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePricer_Selecting);

                this.dxgrdorders.DataBind();
                //show details by default?
                //this.dxgrdorders.DetailRows.ExpandAllRows();
            }
            else
            {
                if (!Page.IsCallback) { Response.Redirect("~/publiship_advance/advance_history.aspx", true); } 
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("publiship_advance/advance_audit", "publiship"));      
        }
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    /// <summary>
    /// some formating functions for Evals  
    /// </summary>
    /// <param name="testvalue">object to check</param>
    /// <returns>empty string if null or object value</returns>
    public string nullValue(object testvalue)
    {
        if (testvalue == null)
            return "";
        return testvalue.ToString();

    }

    /// <summary>
    /// pass filter field AND contact id for selecting by user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void objdsOrders_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Int32 _userid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : -1;
        Int32 _companyid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : -1;
        
        //e.InputParameters["ContactID"] = _userid;
        Query _qry = new Query(PublishipAdvanceOrderTable.Schema);
        
        if (this.dxcbofields.Text.ToString() != string.Empty && this.txtQuickSearch.Text.ToString() != string.Empty)
        {
            string _fieldname = this.dxcbofields.SelectedItem.GetValue("fieldname").ToString();
            string _txtsearch = this.txtQuickSearch.Text.ToLower();

            _qry.AND(_fieldname, Comparison.Equals, _txtsearch);
        }

        if (!string.IsNullOrEmpty((string)this.dxcbocompany.Value))
        {
            _qry.AND("CompanyID", Comparison.Equals, this.dxcbocompany.Value.ToString());  
        }
        
        e.InputParameters["qry"] = _qry;
    }

    //end titles selecting
    /// <summary>
    /// check text box for input and build simple filter string 
    /// </summary>
    protected string get_filter()
    {
        string _filter = ""; // "(OrderNumber==-1)"; //safe default will return no records";

        if (this.dxcbofields.Text.ToString() != string.Empty && this.txtQuickSearch.Text.ToString() != string.Empty)
        {
            string _fieldname = this.dxcbofields.SelectedItem.GetValue("fieldname").ToString();
            string _fieldtype = this.dxcbofields.SelectedItem.GetValue("columntype").ToString();
            string _txtsearch = this.txtQuickSearch.Text.ToLower();
            string _lquoted = "";
            string _rquoted = "";
            string _criteria = "==";

            if (_fieldtype.ToLower() == "string")
            {
                _fieldname += ".ToString().ToLower()";
                _lquoted = "\"";
                _rquoted = "\"";

                //02/08/2011 allow partial search on all text fields
                //if (_fieldname.ToLower() == "mintitle" && (Page.Session["user"] != null)) 
                if (Page.Session["user"] != null)
                {
                    _criteria = ".Contains("; //".Contains("
                    _rquoted = "\")"; //"\")";
                }
            }

            //make sure you use escape character for quoting string literals or you will get errors back from dynamic.cs
            //_filter = string.Format("({0}{1}{2}{3}{4})", _fieldname , _criteria  ,_lquoted,_txtsearch, _rquoted );
            string _formstr = _criteria == "==" ? "({0}{1}{2}{3}{2})" : "({0}{1}{2}{3}{2}))";
            _filter = string.Format(_formstr, _fieldname, _criteria, _lquoted, _txtsearch, _rquoted);

        }

        return _filter;
    }
    //end get filter

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
    //end button state

    /// <summary>
    /// hide/show custom buttons for internal user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdorders_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        Int32 _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : 0;
        if (_cid != -1)
        {
            //200812 this is not relevant as we had to put the detailed form on a seperate page
            //detailed edit button, external users get the standard form with restricted details not the whole lot
            if (e.ButtonID == "cmdEdit") { e.Visible = DevExpress.Utils.DefaultBoolean.False; } 
        }
    }
    //end custom button initialise

    protected void register_client_scripts()
    {
        // Gets a reference to a Label control that not in 
        // a ContentPlaceHolder
        DevExpress.Web.ASPxEditors.ASPxLabel mpLabel = (DevExpress.Web.ASPxEditors.ASPxLabel)Master.FindControl("lblResult");
        string _script = "";

        if (mpLabel != null)
        {
            _script = string.Format(@"function verify_user(){{
                    var us = document.getElementById('{0}').innerHTML; 
                    return us;
                    }}", mpLabel.ClientID);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "lg_key", _script, true);

        }
    }
    //end client script

    #region postback events
    /// <summary>
    /// this option replaces the multiple command buttons
    /// get selected value from combo box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string _export = (string)this.aspxcboExport.Value; //default to pdf?

        try
        {
            switch (_export)
            {
                case "0": //pdf
                    {
                        this.dxgrdexport.Landscape = true;
                        this.dxgrdexport.MaxColumnWidth = 200;
                        this.dxgrdexport.PaperKind = System.Drawing.Printing.PaperKind.Legal; //Legal would hopefully wide enough for one page
                        this.dxgrdexport.BottomMargin = 1;
                        this.dxgrdexport.TopMargin = 1;
                        this.dxgrdexport.LeftMargin = 1;
                        this.dxgrdexport.RightMargin = 1;
                        this.dxgrdexport.WritePdfToResponse();
                        break;
                    }
                case "1": //excel
                    {
                        this.dxgrdexport.WriteXlsToResponse();
                        break;
                    }
                case "2": //excel 2008+
                    {
                        this.dxgrdexport.WriteXlsxToResponse();
                        break;
                    }
                case "3": //csv
                    {
                        this.dxgrdexport.WriteCsvToResponse();
                        break;
                    }
                case "4": //rtf
                    {
                        this.dxgrdexport.WriteRtfToResponse();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            //error = Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.
            //is this a .net bug?
            //do nothing!
            string _ex = ex.ToString();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + _ex + "</div>";
        }
    }
    //end buton export

    protected void btnExpandAll_Click(object sender, EventArgs e)
    {
        if (this.btnExpandAll.Text == "Show detail")
        {
            this.btnExpandAll.Text = "Hide detail";
            this.dxgrdorders.DetailRows.ExpandAllRows();
        }
        else
        {
            this.btnExpandAll.Text = "Show detail";
            this.dxgrdorders.DetailRows.CollapseAllRows();
        }
    }

    protected void btnEndGroup_Click(object sender, EventArgs e)
    {
        remove_grid_grouping();
    }

    /// <summary>
    /// remove all groupings from datagrid
    /// </summary>
    /// 
    protected void remove_grid_grouping()
    {
        try
        {
            for (int i = 0; i < this.dxgrdorders.Columns.Count; i++)
                if (this.dxgrdorders.Columns[i] is GridViewDataColumn)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.dxgrdorders.Columns[i];
                    if (gridViewDataColumn.GroupIndex > -1)
                        this.dxgrdorders.UnGroup(gridViewDataColumn);
                }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());  
            this.lblmsgboxdiv.Text += "<div class='alert'>Error description" + ": " + ex.Message.ToString() + "</div>";
        }
    }
    //end remove grid grouping
    protected void btnEndFilter_Click(object sender, EventArgs e)
    {
        this.txtQuickSearch.Text = "";

        //not needed here no advanced search
        //SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        //_sessionWrapper["mode"] = null;
        //_sessionWrapper["query"] = null;
        //_sessionWrapper["name"] = null;
        //Session["filter"] = null; //so we don't save it again
        
        reset_hidden(); //sets mode back to default 0
        this.dxgrdorders.DataBind();
    }
    //end clear filter
    protected void reset_hidden()
    {
        this.dxhfMethod.Set("mode", -1);
    }
    //end reset hidden


    /// <summary>
    /// row command postbacks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdorders_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {

        try
        {
            Int32 _idx = e.VisibleIndex;
            //string[] _fields = {"qry_text","qry_desc"};
            string _command = e.CommandArgs.CommandArgument.ToString();

            Int32 _orderid = wwi_func.vint(this.dxgrdorders.GetRowValues(_idx, "OrderID").ToString());

            if (_orderid > 0)
            {
                switch(_command){
                    case "print_labels": //drop record into pricer
                        {
                            string _err = itextsharp_out.advance_labels(_orderid);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }//end switch
                
            }//end if
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //end row command
    #endregion

    #region databinding
    

    protected void grdtitles_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _titles = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "OrderID" };
        Int32 _orderid = (Int32)_titles.GetMasterRowFieldValues(_keys);
  
        if (_orderid > 0)
        {
            Session["selPAOrderId"] = _orderid.ToString();
            this.objdsTitles.SelectParameters["PAOrderID"].DefaultValue = _orderid.ToString();
        }
    }

    /// <summary>
    /// call titles databound event to automatically expand carton rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdtitles_DataBound(object sender, EventArgs e)
    {
        ASPxGridView _titles = (ASPxGridView)sender;
        _titles.DetailRows.ExpandAllRows();
    }

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
           this.objdsCartons.SelectParameters["PATitleID"].DefaultValue = _titleid.ToString();
        }
    }


    /// <summary>
    /// databind combos deprecated bound to sqldatasource
    /// </summary>
    protected void bind_combos()
    {
        //company name
        //string[] _cols = { "NameAndAddressBook.CompanyId", "NameAndAddressBook.CompanyName" }; //MUST have defined columns or datareader will not work!
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Where("Customer").IsEqualTo(true);
        //
        //NameAndAddressBookCollection _company = new NameAndAddressBookCollection();
        //_company.LoadAndCloseReader(_query.ExecuteReader());
        //DataTable _dt = (DataTable)_company.ToDataTable();
        //
        //this.dxcbocompany.DataSource = _dt;
        //this.dxcbocompany.ValueField = "CompanyId";
        //this.dxcbocompany.TextField = "CompanyName";
        //this.dxcbocompany.DataBind();
        
    }
    //end bind combos
    protected void dxcbocountry_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _cbo = (DevExpress.Web.ASPxEditors.ASPxComboBox)sender;

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

            _cbo.DataSource = _dv;
            _cbo.DataBind();
            _cbo.Value = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }


    

    
    #endregion
         
    #region database operations
    /// <summary>
    /// on databound check if publiship user of client
    /// if client hide fields in edit form they do not have permission to edit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdorders_DataBound(object sender, EventArgs e)
    {
        //Int32 _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : -1;
        //GridViewDataColumn _c = new GridViewDataColumn();
        //
        //if (_cid != -1)
        //{
        //    string[] _f = { "OrderNumber", "CompanyID", "ConsigneeID", "OrderNumber", "CompanyID", "ConsigneeID", "PrinterID", "CustomerRef",
        //                  "PrintersRef", "ContactID","CargoReadyDate","CargoReceivedDate","OriginID","FinalDestID","Etd","Eta",
        //                  "ActualWeight","ActualVolume", "HAWBno", "HAWBAdded","RemarkstoAgent","RemarkstoCust","JobClosed","JobClosureDate",
        //                  "CompositeInvRef","InsuranceValue","CancelRequestRcd","OrderCancelled","CancelDate","CancelledByID" };
        //
        //    for (int _ix = 0; _ix < _f.Length; _ix++)
        //    {
        //        _c = (GridViewDataColumn)this.dxgrdorders.Columns[_f[_ix]];
        //        if (_c != null) { _c.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False; }
        //
        //        //ASPxGridViewTemplateReplacement _r = new ASPxGridViewTemplateReplacement();
        //        //_r.ColumnID = "col" + _f[_ix];
        //        //_r.Visible = false;
        //    }
        //
        //} //end if

        ////hidden for all users as these are auto-updated
        //_c = (GridViewDataColumn)this.dxgrdorders.Columns["Titles"];
        //if (_c != null) { _c.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False; }
        //
        //_c = (GridViewDataColumn)this.dxgrdorders.Columns["Cartons"];
        //if (_c != null) { _c.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False; }

    }
    //end databound

    /// <summary>
    /// called from custom command buttons in gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "cmdRemove")
        {
            string[] _f = new string[] { "DateOrderReceived", "Payee", "DeliveryAddress", "DestinationCountry", 
                                     "Titles", "Cartons", "Fao" };

            //user id
            Int32 _user = Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;

            //get rows values from grid
            ASPxGridView _g = (ASPxGridView)sender;
            object _id = _g.GetRowValues(e.VisibleIndex, _g.KeyFieldName);
            object _rw = _g.GetRowValues(e.VisibleIndex, _f);
            //convert object to ienumerable so we can step through values
            IEnumerable _enum = wwi_func.get_object_to_ienum(_rw);

            if (_id != null && _user != 0)
            {
                Int32 _cid = (Int32)((UserClass)Page.Session["user"]).CompanyId;
                string _uname = (string)((UserClass)Page.Session["user"]).UserName;
                //for testing
                //_cid = 99;
                //
                //publiship users can just update the order, clients must send a request for cancellation 
                if (_cid == -1)
                {
                    //log cancellation
                    int _updated = new Update(PublishipAdvanceOrderTable.Schema)
                                   .Set(PublishipAdvanceOrderTable.Columns.CancelDate).EqualTo(DateTime.Now.ToShortDateString())
                                   .Set(PublishipAdvanceOrderTable.Columns.CancelledByID).EqualTo(_user)
                                   .Set(PublishipAdvanceOrderTable.Columns.OrderCancelled).EqualTo(true)
                                   .Where(PublishipAdvanceOrderTable.Columns.OrderID).IsEqualTo(_id).Execute();

                    //confirm cancellation to client and publiship
                    if (_updated > 0)
                    {
                        //string _test = return_html_content(_enum, _f);
                        send_advance_email(_id.ToString(), "Order Ref. " + _id + "has been cancelled", return_html_content(_enum, _f));
                    }
                }
                else
                {
                    //log request
                    int _updated = new Update(PublishipAdvanceOrderTable.Schema)
                                   .Set(PublishipAdvanceOrderTable.Columns.CancelRequestRcd).EqualTo(DateTime.Now.ToShortDateString())
                                   .Set(PublishipAdvanceOrderTable.Columns.CancelRequestByID).EqualTo(_user)
                                   .Where(PublishipAdvanceOrderTable.Columns.OrderID).IsEqualTo(_id).Execute();

                    //confirm request received to client and publiship
                    if (_updated > 0)
                    {
                        //string _test = return_html_content(_enum, _f);
                        send_advance_email(_id.ToString(), "Cancellation request for order ID " + _id.ToString() + " from " + _uname, return_html_content(_enum, _f));
                    }
                }
            }
        }//end remove command
        //refresh data
        this.dxgrdorders.DataBind(); 
    }
    //end custom button
    //end custom button
    public string return_html_content(IEnumerable enm, string[] lbl)
    {
        string _tr = wwi_func.get_from_global_resx("html_table_tr");
        string _tb = wwi_func.get_from_global_resx("html_table_body");
        string _tx = "";

        if (enm != null)
        {
            //pass info to table structure
            int _ix = 0;
            foreach (object _obj in enm)
            {
                _tx += string.Format(_tr, lbl[_ix], _obj != null ? _obj.ToString() : "", "");
                _ix++;
            }

            //format to html table
            _tx = string.Format(_tb, _tx);
        }
        return _tx;
    }
    //end return html
    /// <summary>
    /// ********************* order grid update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdorders_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        if(e.AffectedRecords != 0)
        {
            //for building email table
            string _tr = wwi_func.get_from_global_resx("html_table_tr");
            string _tb = wwi_func.get_from_global_resx("html_table_body");
            string _tx = "";
            //
            
            //to enumerate through values
            string _key = e.Keys[0].ToString(); //this should be OrderID
            System.Collections.Specialized.OrderedDictionary _list = new System.Collections.Specialized.OrderedDictionary();
            _list = (System.Collections.Specialized.OrderedDictionary)e.OldValues;
            int _ix = 0;

            //loop through each entry in dictionary
            foreach (System.Collections.DictionaryEntry _entry in _list) 
            {
                string[] _args = new string[3];

                _args[0] = _entry.Key != null ? _entry.Key.ToString() : ""; //field name
                _args[1] = e.OldValues[_ix] != null? e.OldValues[_ix].ToString(): "";
                _args[2] = e.NewValues[_ix] != null ? e.NewValues[_ix].ToString() : "";
                
                if (_args[2] != _args[1])
                {
                    _tx += string.Format(_tr, _args); 
                }
                _ix++;
            }//end loop
            
            //create email if changes
            if (_tx != "")
            {   
                //add header row
                _tx = string.Format(_tr, "Item", "Changed from", "Changed to") + _tx;
                //format to html table
                _tx = string.Format(_tb, _tx);
                //send

                send_advance_email(_key,"order changed", _tx);
            }
        } //end if affected records
    }
    //******************************

    /// <summary>
    /// ***************************** titles child grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdtitles_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView _titles = (ASPxGridView)sender;
        
        string _key = _titles.GetMasterRowKeyValue().ToString(); 
        //string _key = e.OldValues["PAOrderID"].ToString();   //e.Keys[0].ToString(); //this should be PATitleID
        //we only need to check title, no point in iterating through dataset
        string _old = e.OldValues["Title"].ToString();
        string _new = e.NewValues["Title"].ToString();

        if (_new != _old)
        {
            //send email 
            //for building email table
            string _tr = wwi_func.get_from_global_resx("html_table_tr");
            string _tb = wwi_func.get_from_global_resx("html_table_body");
            string _tx = string.Format(_tr, "Title", _old, _new);

            //add header row
            _tx = string.Format(_tr, "Item", "Changed from", "Changed to") + _tx;
            //format to html table
            _tx = string.Format(_tb, _tx);
            //send
            send_advance_email(_key,"title changed", _tx);
        }
        //get titleid for new carton
        //if (e.NewValues["PAOrderID"] == null)
        //{
        //    Int32 _idx = e.VisibleIndex;
        //    //string _orderid = this.dxhforder.Contains("orderid") ? this.dxhforder.Get("orderid").ToString() : "-1";
        //    string _orderid = _titles.GetRowValues(_idx, "quote_Id").ToString();
        //    //e.NewValues["PaTitleID"] = _titleid;
        //    this.objdsTitles.InsertParameters["PAOrderID"].DefaultValue = _orderid;
        //}
    }
    protected void dxgrdtitles_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        if (e.AffectedRecords != 0)
        {
            string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            string _title = e.Values["Title"].ToString();

            set_tally(wwi_func.vint(_key), -1, "titles");  
            send_advance_email(_key,"title removed", "This title has been removed: " + Environment.NewLine + _title);
        }
    }

    protected void dxgrdtitles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //ASPxGridView _titles = (ASPxGridView)sender;
        //get titleid for new carton
        //if (e.NewValues["PAOrderID"] == null)
        //{
            string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            //e.NewValues["PaTitleID"] = _titleid;
            this.objdsTitles.InsertParameters["PAOrderID"].DefaultValue = _key;
        //}
    }
    

    protected void dxgrdtitles_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.AffectedRecords != 0)
        {
            string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            string _title = e.NewValues["Title"].ToString();

            set_tally(wwi_func.vint(_key), +1, "titles");  
            send_advance_email(_key.ToString(),"title added", "A new title has been added: " + Environment.NewLine + _title);
        }
    }
    //****************************** end titles 
    
    /// <summary>
    /// **************************** carton child grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdcartons_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //if(e.AffectedRecords == 1)
        //{
        //for building email table
        string _tr = wwi_func.get_from_global_resx("html_table_tr");
        string _tb = wwi_func.get_from_global_resx("html_table_body");
        string _tx = "";
        //

        //to enumerate through values
        ASPxGridView _cartons = (ASPxGridView)sender;

        string _key = Session["selPAOrderId"] != null ? Session["selPAOrderId"].ToString() : _cartons.GetMasterRowKeyValue().ToString();
        //string _key = e.NewValues["OrderID"].ToString();   //e.Keys[0].ToString(); //this should be PACartonID
        System.Collections.Specialized.OrderedDictionary _list = new System.Collections.Specialized.OrderedDictionary();
        _list = (System.Collections.Specialized.OrderedDictionary)e.OldValues;
        int _ix = 0;

        //loop through each entry in dictionary
        foreach (System.Collections.DictionaryEntry _entry in _list)
        {
            string[] _args = new string[3];

            _args[0] = _entry.Key != null ? _entry.Key.ToString() : ""; //field name
            _args[1] = e.OldValues[_ix] != null ? e.OldValues[_ix].ToString() : "";
            _args[2] = e.NewValues[_ix] != null ? e.NewValues[_ix].ToString() : "";

            if (_args[2] != _args[1])
            {
                _tx += string.Format(_tr, _args);
            }
            _ix++;
        }//end loop

        //create email if changes
        if (_tx != "")
        {
            //add header row
            _tx = string.Format(_tr, "Item", "Changed from", "Changed to") + _tx;
            //format to html table
            _tx = string.Format(_tb, _tx);
            //send

            send_advance_email(_key,"carton changed", _tx);
        }
        //}end if affected records
    }
    protected void dxgrdcartons_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        if (e.AffectedRecords != 0)
        {
            string _tr = wwi_func.get_from_global_resx("html_table_tr");
            string _tb = wwi_func.get_from_global_resx("html_table_body");
            string _tx = "";

            //string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            //use order ref from session if it's there
            string _key = Session["selPAOrderId"] != null ? Session["selPAOrderId"].ToString() :"";
            //Int32 _titleid = wwi_func.vint((sender as ASPxGridView).GetMasterRowKeyValue().ToString());
            
            _tx += string.Format(_tr, "CartonLength", e.Values["CartonLength"].ToString(),"");
            _tx += string.Format(_tr, "CartonWidth", e.Values["CartonWidth"].ToString(), "");
            _tx += string.Format(_tr, "CartonHeight", e.Values["CartonHeight"].ToString(), "");
            _tx += string.Format(_tr, "CartonWeight", e.Values["CartonWeight"].ToString(), "");
            _tx = string.Format(_tb, _tx);

            set_tally(wwi_func.vint(_key), -1, "cartons");
            send_advance_email(_key,"carton removed", "This carton has been removed: " + Environment.NewLine + _tx);
        }
    }

    /// <summary>
    /// details row on insert need to set titleid or will get an error when saving
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdcartons_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //get titleid for new carton
        //if (e.NewValues["PaTitleID"] == null)
        //{
            string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            //e.NewValues["PaTitleID"] = _titleid;
            this.objdsCartons.InsertParameters["PATitleID"].DefaultValue = _key;
        //}
    }

    protected void dxgrdcartons_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.AffectedRecords != 0)
        {
            string _tr = wwi_func.get_from_global_resx("html_table_tr");
            string _tb = wwi_func.get_from_global_resx("html_table_body");
            string _tx = "";

            //string _key = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            //use order ref from session if it's there, if not just have to use title id
            string _key = Session["selPAOrderId"] != null ? Session["selPAOrderId"].ToString() : "";
            //int _titleid = wwi_func.vint((sender as ASPxGridView).GetMasterRowKeyValue().ToString());
            //int _cartons = wwi_func.vint(e.NewValues["cartons"].ToString());

            _tx += string.Format(_tr, "CartonLength", e.NewValues["CartonLength"].ToString(),"");
            _tx += string.Format(_tr, "CartonWidth", e.NewValues["CartonWidth"].ToString(),"");
            _tx += string.Format(_tr, "CartonHeight", e.NewValues["CartonHeight"].ToString(), "");
            _tx += string.Format(_tr, "CartonWeight", e.NewValues["CartonWeight"].ToString(), "");
            _tx = string.Format(_tb, _tx);

            set_tally(wwi_func.vint(_key), +1, "cartons"); 
            //set_tally_cartons(_key, get_tally_cartons(_titleid)); 
            send_advance_email(_key.ToString(), "carton added","A new carton has been added: " + Environment.NewLine + _tx);
        }
    }
    //****************************** end carton grid 
    /// <summary>
    /// set tallies for titles and cartons for current order and update order table
    /// </summary>
    /// <param name="orderid"></param>
    protected void set_tally(int orderId, int valueModifier, string appliesTo)
    {
        if (orderId > 0)
        {
            
            try
            {
                //Query q = new Query(DAL.Logistics.Tables.PublishipAdvanceOrderTable) {QueryType = QueryType.Update};
                //q.WHERE("OrderId", SubSonic.Comparison.Equals, orderId);
                //q.AddUpdateSetting(PublishipAdvanceOrderTable.Columns.Titles, 1);
  
                //update order table
                //PublishipAdvanceOrderTableCollection _o = new PublishipAdvanceOrderTableCollection().Where("OrderID", SubSonic.Comparison.Equals, orderId).Load();
                publishipadvanceOrderTableCustomcontroller _o = new publishipadvanceOrderTableCustomcontroller();

                switch (appliesTo)
                {
                        case "titles":
                            {
                                _o.UpdateTitleCount(orderId, valueModifier);  
                                break;
                            }
                        case "cartons":
                            {
                                _o.UpdateCartonCount(orderId, valueModifier);  
                                break;
                            }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString()); 
            }
        }
    }
    //****************************** end tallies
    #endregion

    #region email processing
    
    protected void send_advance_email(string keyid, string htmlheader, string htmlbody)
    {
        string[] _args = new string[3];
        
        if (Page.Session["user"] != null)
        {
            _args[0] = (string)((UserClass)Page.Session["user"]).UserName; //user
            _args[1] = (string)((UserClass)Page.Session["user"]).mailTo; //client 
            _args[2] = (string)((UserClass)Page.Session["user"]).OfficeName; //company
        }

        string _cc = wwi_func.get_from_global_resx("advance_email").ToString();
        if (!string.IsNullOrEmpty(_args[1])) { _cc = _cc + ";" + _args[1]; }
        string[] _to = _cc.Split(";".ToCharArray());

        //string[] _to = { "paule@publiship.com", "pauled2109@hotmail.co.uk", "pmedwards1@gmail.com" };
        string _emailed = MailHelper.gen_email(_to, true, " Order Ref " + keyid + ": " + htmlheader, htmlbody, "");
    }
    //end send advance email


#endregion

    #region company filters
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcbocompany_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";
        {
            //use datareaders - much faster than loading into collections
            string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };

            //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
            SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
            IDataReader _rd = _query.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "CompanyID";
            _combo.TextField = "CompanyName";
            _combo.DataBind();
        }
    }
    protected void dxcbocompany_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        //use datareaders - much faster than loading into collections
        string[] _cols = { "NameAndAddressBook.CompanyID, NameAndAddressBook.CompanyName, NameAndAddressBook.Customer" };

        Int32 _id = wwi_func.vint(e.Value.ToString());
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "CompanyID";
        _combo.TextField = "CompanyName";
        _combo.DataBind();

    }
    //end incremental filtering of company name
    #endregion
}
