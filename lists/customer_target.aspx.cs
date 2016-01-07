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
using DAL.CustomerTarget;
using DevExpress.Web.ASPxGridView;
using ParameterPasser;

public partial class customer_target : System.Web.UI.Page
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

                //using objectdatasource
                //this.LinqServerModePricer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModePricer_Selecting);
                bind_target_combos();
                this.dxgrdTarget.DataBind();
                
                //show details by default?
                //this.dxgrdorders.DetailRows.ExpandAllRows();
            }
            else
            {
                if (!Page.IsCallback) { Response.Redirect("../Default.aspx", true); } 
            }
        }
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("lists/customer_target", "publiship"));      
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
    /// datasource selecting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void objdsTarget_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        
    }

    

    /// <summary>
    /// hide buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdHistory_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }
    protected void dxgrdTarget_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }
    //end button state

    /// <summary>
    /// hide/show custom buttons for internal user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdTarget_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
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
                        this.dxgrdExport.Landscape = true;
                        this.dxgrdExport.MaxColumnWidth = 200;
                        this.dxgrdExport.PaperKind = System.Drawing.Printing.PaperKind.Legal; //Legal would hopefully wide enough for one page
                        this.dxgrdExport.BottomMargin = 1;
                        this.dxgrdExport.TopMargin = 1;
                        this.dxgrdExport.LeftMargin = 1;
                        this.dxgrdExport.RightMargin = 1;
                        this.dxgrdExport.WritePdfToResponse();
                        break;
                    }
                case "1": //excel
                    {
                        this.dxgrdExport.WriteXlsToResponse();
                        break;
                    }
                case "2": //excel 2008+
                    {
                        this.dxgrdExport.WriteXlsxToResponse();
                        break;
                    }
                case "3": //csv
                    {
                        this.dxgrdExport.WriteCsvToResponse();
                        break;
                    }
                case "4": //rtf
                    {
                        this.dxgrdExport.WriteRtfToResponse();
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
            this.dxgrdTarget.DetailRows.ExpandAllRows();
        }
        else
        {
            this.btnExpandAll.Text = "Show detail";
            this.dxgrdTarget.DetailRows.CollapseAllRows();
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
            for (int i = 0; i < this.dxgrdTarget.Columns.Count; i++)
                if (this.dxgrdTarget.Columns[i] is GridViewDataColumn)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.dxgrdTarget.Columns[i];
                    if (gridViewDataColumn.GroupIndex > -1)
                        this.dxgrdTarget.UnGroup(gridViewDataColumn);
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
        //not needed here no advanced search
        //SessionParameterPasser _sessionWrapper = new SessionParameterPasser();
        //_sessionWrapper["mode"] = null;
        //_sessionWrapper["query"] = null;
        //_sessionWrapper["name"] = null;
        //Session["filter"] = null; //so we don't save it again
        
        reset_hidden(); //sets mode back to default 0
        this.dxgrdTarget.DataBind();
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

            Int32 _orderid = wwi_func.vint(this.dxgrdTarget.GetRowValues(_idx, "OrderID").ToString());

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
    /// <summary>
    /// populates combo box column(s) in datagrid
    /// </summary>
    protected void bind_target_combos()
    {
        //****************
        //priority codes 0 to 5
        //280113 changed to string and added new codes
        //bind combo in cell editor initialise event
        //WP – working prospect.  KP – Key prospect      PP – pending prospect  A – account  D – disregard
        //****************
        //for (int _ix = 0; _ix <= 5; _ix++)
        //{
        //    _combo.PropertiesComboBox.Items.Add(_ix.ToString(), _ix);
        //}

        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\ddl_items.xml";

        // pass _qryFilter to have keyword-filter RSS Feed
        // i.e. _qryFilter = XML -> entries with XML will be returned
        DataSet _ds = new DataSet();
        _ds.ReadXml(_path);
        DataView _dv = _ds.Tables[0].DefaultView;
        _dv.RowFilter = "ddls ='prioritycode'";

        //Run time population of GridViewDataComboBoxColumn column with data
        GridViewDataComboBoxColumn _combo = this.dxgrdTarget.Columns["colPriorityCode"] as GridViewDataComboBoxColumn;
        _combo.PropertiesComboBox.DataSource = _dv;
        _combo.PropertiesComboBox.ValueType = typeof(int);
        _combo.PropertiesComboBox.TextField = "name";
        _combo.PropertiesComboBox.ValueField = "value";

    }
    //end bind target combos

    protected void grdHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _history = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "TargetID" };
        Int32 _targetid = (Int32)_history.GetMasterRowFieldValues(_keys);

        if (_targetid > 0)
        {
            this.objdsTargetHistory.SelectParameters["TargetID"].DefaultValue = _targetid.ToString();
        }
    }

    /// <summary>
    /// call titles databound event to automatically expand carton rows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdHistory_DataBound(object sender, EventArgs e)
    {
        ASPxGridView _grdHistory = (ASPxGridView)sender;
        _grdHistory.DetailRows.ExpandAllRows();
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
    /// called from custom command buttons in gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //if (e.ButtonID == "cmdRemove")
        //{
        //    string[] _f = new string[] { "DateOrderReceived", "Payee", "DeliveryAddress", "DestinationCountry", 
        //                             "Titles", "Cartons", "Fao" };
        //
        //    //user id
        //    Int32 _user = Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
        //
        //    //get rows values from grid
        //    ASPxGridView _g = (ASPxGridView)sender;
        //    object _id = _g.GetRowValues(e.VisibleIndex, _g.KeyFieldName);
        //    object _rw = _g.GetRowValues(e.VisibleIndex, _f);
        // 
        //}//end remove command
        //refresh data
        //this.dxgrdTarget.DataBind(); 
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
    /// ********************* customer target grid update append updated field(s) to history table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdTarget_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        if(e.AffectedRecords != 0)
        {
            
            //for batch save
            TargetHistoryCollection _t = new TargetHistoryCollection();
            string _user = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
            //to enumerate through values
            string _key = e.Keys[0].ToString(); //this should be TargetID
            System.Collections.Specialized.OrderedDictionary _list = new System.Collections.Specialized.OrderedDictionary();
            _list = (System.Collections.Specialized.OrderedDictionary)e.OldValues;
            int _ix = 0;

            //loop through each entry in dictionary
            foreach (System.Collections.DictionaryEntry _entry in _list)
            {
                string[] _args = new string[3];

                _args[0] = _entry.Key != null ? _entry.Key.ToString() : "";
                if (_args[0] != "UpdateUser" && _args[0] != "UpdateDate" && _args[0] != "InsertDate") //we don't need to log changes to these
                {
                    _args[1] = e.OldValues[_ix] != null ? e.OldValues[_ix].ToString() : "";
                    _args[2] = e.NewValues[_ix] != null ? e.NewValues[_ix].ToString() : "";

                    if (_args[2] != _args[1])
                    {
                        TargetHistory _item = new TargetHistory();
                        _item.IDtarget = wwi_func.vint(_key);
                        _item.FieldName = wwi_func.uncamel(_args[0]); //field name
                        _item.ChangedFrom  = _args[1];
                        _item.ChangedTo = _args[2]; 
                        _item.LogDate = DateTime.Now.Date;
                        _item.LogUser = _user;
                        //append to history
                        _t.Add(_item);
                    }
                }
                _t.SaveAll(); 
                _ix++;
            }//end loop
        } //end if affected records
    }
    /// <summary>
    /// on insert set created date and user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdTarget_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //make sure the insert params match those in the objectdatasource
        string _user = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
        this.objdsCustomerTarget.InsertParameters["InsertUser"].DefaultValue = _user;
        this.objdsCustomerTarget.InsertParameters["InsertDate"].DefaultValue = DateTime.Now.Date.ToShortDateString();
    }
    /// <summary>
    /// on update pass user info and date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdTarget_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //make sure the update params match those in the objectdatasource
        string _user = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
        //update last modified details
        this.objdsCustomerTarget.UpdateParameters["UpdateUser"].DefaultValue = _user;
        this.objdsCustomerTarget.UpdateParameters["UpdateDate"].DefaultValue = DateTime.Now.Date.ToShortDateString();

    }

    /// <summary>
    /// catch insert error and replace with meaningful descriptions
    /// could also over ride this and throw exception on row_inserting event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdTarget_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.Exception is NullReferenceException)
        { e.ErrorText = "Null Reference Exception" + e.Exception.Message.ToString(); }
        else if (e.Exception is InvalidOperationException)
        { e.ErrorText = "Invalid Operation Exception" + e.Exception.Message.ToString(); }
        else if (e.Exception is System.Reflection.TargetInvocationException && e.Exception.InnerException.ToString().Contains("duplicate key"))
        { e.ErrorText = "Not able to save record due to a duplicate company name"; }
    }

    /// <summary>
    /// why do we need to hack this additional param out of objectdatasource insert
    /// it is not being passed or added anywhere!?
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void objdsCustomerTarget_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (e.InputParameters.Contains("TargetID")) { e.InputParameters.Remove("TargetID"); }
    }
    //******************************

   /// <summary>
   /// disable auto populated fields on edit
   /// hide auto populated fields on new
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void dxgrdTarget_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Name == "colTargetID" || e.Column.Name == "colUpdateUser" || e.Column.Name == "colInsertUser")
        {
            DevExpress.Web.ASPxEditors.ASPxTextBox _tx = ((DevExpress.Web.ASPxEditors.ASPxTextBox)e.Editor);
            if (e.Column.Grid.IsNewRowEditing)
            {
                _tx.ClientVisible = false;
            }
            else if (e.Column.Grid.IsEditing)
            {
                _tx.ClientEnabled = false;
            }
        }
        //end text columns

        if (e.Column.Name == "colInsertDate" || e.Column.Name == "colUpdateDate")
        {
            DevExpress.Web.ASPxEditors.ASPxDateEdit _dx = ((DevExpress.Web.ASPxEditors.ASPxDateEdit)e.Editor);
            if (e.Column.Grid.IsNewRowEditing)
            {
                _dx.ClientVisible = false;
            }
            else if (e.Column.Grid.IsEditing)
            {
                _dx.ClientEnabled = false;
            }
        }

       //290113 bind priority codes
       if (e.Column.Name == "colPriorityCode"){
           DevExpress.Web.ASPxEditors.ASPxComboBox _cb = ((DevExpress.Web.ASPxEditors.ASPxComboBox)e.Editor);
           _cb.DataBind(); 
       }

    }
    //end init cell editors
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
