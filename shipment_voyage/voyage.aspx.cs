using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxPanel;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHiddenField;
using SubSonic;


public partial class voyage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for testing only *****
        //get primary key VoyageID
        string _mode = get_token("mode"); //Request.QueryString["mode"] != null ? wwi_security.DecryptString(Request.QueryString["mode"].ToString(), "publiship") : "review";
        
        if (isLoggedIn())
        {
            if (!Page.IsPostBack)
            {
                bind_formview_commands(); //crud commands for formview
                set_mode(_mode);
                bind_formview(_mode);
            }
        }   
        else
        {
            Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("shipment_voyage/voyage", "publiship")); 
        }
    }
    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    #region menu binding
    /// <summary>
    /// command menu 
    /// </summary>
    protected void bind_formview_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\order_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;

        _xml.XPath = "//item[@Filter='GenericFormView']"; //you need this or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        this.dxmnuFormView.DataSource = _xml;
        this.dxmnuFormView.DataBind();

    }

    /// <summary>
    /// on menu databound modify navigate urls to current page and primary key (pid) 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxmnuFormView_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        //don't set navigateurl it prevents menu click event from firing
        e.Item.NavigateUrl = "";

        //if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        //{
        //    string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
        //    string _id = get_token("pid");
        //    if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        //}
    }
    #endregion

    #region command and tab events
    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvVoyage.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvVoyage.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvVoyage.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvVoyage.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }

    //requried of formview mode will not change
    protected void fmvBol_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvVoyage.ChangeMode(e.NewMode);
    }

    //crud commands for formview
    protected void dxmnuFormView_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();
        List<string> _menuitems = new List<string>();

            switch (_item)
            {
                case "cmdNew":
                    {
                        set_mode("Insert");
                        //call databind to change mode
                        bind_formview("Insert"); 
                        break;
                    }
                case "cmdEdit":
                    {
                        set_mode("Edit");
                        //call databind to change mode
                        bind_formview("Edit"); 
                        break;
                    }
                case "cmdDelete": //not enabled - do we want users to delete records?
                    {
                       break;
                    }
                case "cmdUpdate":
                    {
                        update_voyage(); 
                        set_mode("ReadOnly");
                        //call databind to change mode
                        bind_formview("ReadOnly"); 
                        //this.fmvVoyage.UpdateItem(false);
                        //set_mode("view"); not necesary form will revert to read only after save
                        break;
                    }
                case "cmdSave":
                    {
                        this.dxhfOrder.Clear();
                        int _newid = insert_voyage();
                        if (_newid > 0)
                        {
                            //set_mode("ReadOnly");
                            //call databind to change mode
                            //bind_formview("ReadOnly");
                            string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath),
                                                wwi_security.EncryptString(_newid.ToString(), "publiship"), 
                                                "ReadOnly" };
                            string _url = string.Format("{0}\\{1}.aspx?pid={2}&mode={3}", _args);
                            Response.Redirect(_url);
                        }
                        else
                        {
                            if (this.dxlblErr.Text == "") { this.dxlblErr.Text = "Error new record NOT saved"; }
                            this.dxpnlErr.ClientVisible = true;
                        }
                        
                        //this.fmvVoyage.InsertItem(false);
                        //set_mode("view"); not necesary form will revert to read only after save
                        break;
                    }
                case "cmdCancel":
                    {
                        set_mode("ReadOnly");
                        bind_formview("ReadOnly"); 
                        break;
                    }
                case "cmdClose":
                    {
                        string[] _args = {  System.IO.Path.GetDirectoryName(Page.AppRelativeVirtualPath),
                                                "voyage_search",};
                        string _url = string.Format("{0}\\{1}.aspx?", _args);
                        Response.Redirect(_url);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //end switch
        
       
    }
    //end container menu commands

   
    /// <summary>
    /// enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_menu_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmnuFormView.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmnuFormView.Items[_ix].Name == "cmd" + active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmnuFormView.Items[_ix].ClientVisible = _isactive;
        }//end menu names loop
    }
    #endregion

    #region call backs
    /// <summary>
    /// ****** ETS and ETA callbacks deprecated. These grids are bound to VoyageID not VesselID no need for call back
    /// ETS/ETA gtrid calbacks when vessel is changed
    /// rebind dates for selected vessel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        int _voyageid =  wwi_func.vint(e.Parameters.ToString());
        bind_ets_grid(_voyageid);
 
    }
    protected void dxgrdETA_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        int _voyageid = wwi_func.vint(e.Parameters.ToString());
        bind_eta_grid(_voyageid);
    }
    //end ETS/ETA grid callbacks 
    #endregion

    #region formview crud events
    protected void fmvVoyage_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvVoyage.ChangeMode(e.NewMode);
    }

    protected void bind_formview(string mode)
    {
        //have to use a collection as formview needs to bind to enumerable
        VoyageTableCollection _tbl = new VoyageTableCollection();
        if (mode != "Insert")
        {
            int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
            VoyageTable _vt = new VoyageTable(_pid);
            _tbl.Add(_vt);
        }
        else
        {
            VoyageTable _vt = new VoyageTable();
            _tbl.Add(_vt);
        }

        this.fmvVoyage.DataSource = _tbl;
        this.fmvVoyage.DataBind();
    }

    protected void fmvVoyage_DataBound(object sender, EventArgs e)
    {
        string _test = "";
        VoyageTable _t = (VoyageTable)this.fmvVoyage.DataItem;

        //voyage id is also added as encryped primary key number to hiddenfields on page load
        this.dxlblID.Text = this.fmvVoyage.CurrentMode == FormViewMode.Insert ? "New voyage" : _t.Joined!= null? _t.Joined.ToString(): "";
        
        //lookup values for readonly mode
        if (this.fmvVoyage.CurrentMode == FormViewMode.ReadOnly)
        {
            //vessel name
            _test = wwi_func.lookup_value("VesselName", "VesselTable", "VesselID", _t.VesselID);
            ASPxLabel _lbl = (ASPxLabel)this.fmvVoyage.FindControl("dxlblVesselIDView");
            if (_lbl != null) { _lbl.Text = _test; }

            //added by
            _test = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _t.AddedBy);
            _lbl = (ASPxLabel)this.fmvVoyage.FindControl("dxlblAddedByView");
            if (_lbl != null) { _lbl.Text = _test; }

        }
        else if (this.fmvVoyage.CurrentMode == FormViewMode.Edit)
        {
            //vessel name is bound to linqdatasource

            //added by
            _test = wwi_func.lookup_value("Name", "EmployeesTable", "EmployeeID", _t.AddedBy);
            ASPxLabel _lbl = (ASPxLabel)this.fmvVoyage.FindControl("dxlblAddedByName");
            if (_lbl != null) { _lbl.Text = _test; }
        }
        else //insert
        {
            //vessel name is bound to linqdatasource

            //set addedby and added date just for appearance as we save the userid not the name in voyagetable
            ASPxLabel _lbl = (ASPxLabel)this.fmvVoyage.FindControl("dxlblAddedByName");
            if (_lbl != null) { _lbl.Text = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName: "";}
            
            _lbl = (ASPxLabel)this.fmvVoyage.FindControl("dxlblDateAddedView");
            if (_lbl != null) { _lbl.Text = DateTime.Now.ToShortDateString() ; } 

            //not needed, this is set automatically. dll binding just for added by as vessel is bound dynamically
            //bind_added_by();
        }

        if (_t != null)
        {
            //bind ets, eta grids on VoyageID
            bind_ets_grid(_t.VoyageID);
            bind_eta_grid(_t.VoyageID);
            //we need these values for crud events on ets/eta grids. They can also be changed client-side
            this.dxhfOrder.Remove("voyage");
            this.dxhfOrder.Set("voyage", _t.VoyageID);
            this.dxhfOrder.Remove("vessel");
            this.dxhfOrder.Set("vessel", _t.VesselID); 

        }
    }
    //end databound

    /// <summary>
    /// update hvoyagetable
    /// </summary>
    /// <param name="hblid">int</param>
    protected void update_voyage()
    {
        //voyageid
        int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));

        if (_pid > 0)
        {
            try
            {
                //new instance of record
                VoyageTable _tbl = new VoyageTable(_pid);
                string _joined = "";

                //get values off editform
                //vessel id
                ASPxComboBox _cbo = (ASPxComboBox)this.fmvVoyage.FindControl("dxcboVesselID");
                if (_cbo != null && _cbo.Value != null) { 
                    _tbl.VesselID = wwi_func.vint(_cbo.Value.ToString());
                    _joined = _cbo.Text.ToString(); 
                }   

                //voyage number
                ASPxTextBox _txt = (ASPxTextBox)this.fmvVoyage.FindControl("dxtxtVoyageNumber");
                if (_txt != null) { 
                    _tbl.VoyageNumber = _txt.Text.ToString();
                    _joined += " " + _txt.Text.ToString();
                }
                
                //joined = vessel name & voyage number
                 _tbl.Joined =  _joined; 
                //user
                _tbl.AddedBy = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
                //date
                _tbl.DateAdded = DateTime.Now; 
                 //update
                _tbl.Save();
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
                this.dxpnlErr.ClientVisible = true;
            }
        }
        else
        {
            string _ex = "Can't update record VoyageId = 0";
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    //end update

    /// <summary>
    /// new record
    /// </summary>
    protected int insert_voyage()
    {
        int _newid = 0;

        try
        {
            ///new instance of record
            VoyageTable _tbl = new VoyageTable();
            string _joined = "";

            //get values off editform
            //vessel id
            ASPxComboBox _cbo = (ASPxComboBox)this.fmvVoyage.FindControl("dxcboVesselID");
            if (_cbo != null && _cbo.Value != null)
            {
                _tbl.VesselID = wwi_func.vint(_cbo.Value.ToString());
                _joined = _cbo.Text.ToString();
            }

            //voyage number
            ASPxTextBox _txt = (ASPxTextBox)this.fmvVoyage.FindControl("dxtxtVoyageNumber");
            if (_txt != null)
            {
                _tbl.VoyageNumber = _txt.Text.ToString();
                _joined += " " + _txt.Text.ToString();
            }

            //joined = vessel name & voyage number
            _tbl.Joined = _joined;
            //user
            _tbl.AddedBy = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).UserId : 0;
            //date
            _tbl.DateAdded = DateTime.Now;
            //insert
            _tbl.Save();
            //get new id
            _newid = (int)_tbl.GetPrimaryKeyValue(); 
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
       
        return _newid;
    }
    #endregion

    #region ETS grid binding and crud events
    /// <summary>
    /// grid bindings
    /// </summary>
    /// <param name="voyageid"></param>
    protected void bind_ets_grid(int voyageid)
    {
        string[] _cols = { "VoyageETSSubTable.VoyageETSSubID", "VoyageETSSubTable.VoyageID", "VoyageETSSubTable.OriginPortID", "VoyageETSSubTable.ETS", "PortTable.PortName" };
        SubSonic.SqlQuery _qry = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VoyageETSSubTable).
            LeftOuterJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETSSubTable.OriginPortIDColumn) 
            .WhereExpression("VoyageID").IsEqualTo(voyageid);
        
        DataTable  _dt = _qry.ExecuteDataSet().Tables[0];
        this.dxgrdETS.DataSource = _dt;
        this.dxgrdETS.KeyFieldName = "VoyageETSSubID";
        this.dxgrdETS.DataBind();
    }

    /// <summary>
    /// change OriginPortID column to display portname instead when in view mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName != "OriginPortID") return;
        e.DisplayText = e.GetFieldValue(e.VisibleRowIndex, "PortName")!= null? e.GetFieldValue(e.VisibleRowIndex, "PortName").ToString(): "";
    }
    /// <summary>
    /// bind port combo when editting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "OriginPortID") {
            ASPxComboBox _combo = (ASPxComboBox)e.Editor;
           
            string[] _cols = { "PortTable.PortID, PortTable.PortName" };
            string[] _order = { "PortName" };
            SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();
            _qry = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).OrderAsc(_order);


            //bind origin ports
            IDataReader _rd = _qry.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "PortID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PortName";
            _combo.DataBindItems(); 
        }  
    }
    /// <summary>
    /// insert event for ETS grid
    /// save to VoyageETSSubTable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender; 
        DateTime? _dtnull = null;
        
        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint( this.dxhfOrder.Get("voyage").ToString() ): 0;
        int _vesselid = this.dxhfOrder.Contains("vessel") ? wwi_func.vint(this.dxhfOrder.Get("vessel").ToString()) : 0; 
        int _portid = e.NewValues["OriginPortID"] != null ? wwi_func.vint(e.NewValues["OriginPortID"].ToString()) : 0;
        DateTime? _dtets = e.NewValues["ETS"] != null ? wwi_func.vdatetime(e.NewValues["ETS"].ToString()): _dtnull;

        if (_voyageid > 0)
        {
            VoyageETSSubTable _tbl = new VoyageETSSubTable();
            _tbl.VoyageID = _voyageid;
            _tbl.OriginPortID = _portid;
            _tbl.Ets = _dtets;
            _tbl.Save();

            if (_portid > 0 && _vesselid > 0)
            {
                update_ordertable_ets(_portid, _vesselid, _dtets);
            }
        }
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit(); 
        //rebind
        bind_ets_grid(_voyageid);  
        
    }
    /// <summary>
    /// update event for ETS grid
    /// check DATE. if it's been changed will need to update order table
    /// ordertable.ETS = new ETS, ordertable.VesselLastUpdated = now where ordertable.PortID = OriginPortID and ordertable.VesselID = VesselID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        
        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint(this.dxhfOrder.Get("voyage").ToString()) : 0;
        int _vesselid = this.dxhfOrder.Contains("vessel") ? wwi_func.vint(this.dxhfOrder.Get("vessel").ToString()) : 0; 
        //key for this record
        int _etsid = e.Keys["VoyageETSSubID"] != null ? wwi_func.vint(e.Keys["VoyageETSSubID"].ToString()) : 0;
        //other required data
        int _portid =  e.NewValues["OriginPortID"] != null ? wwi_func.vint(e.NewValues["OriginPortID"].ToString()) : 0;
        DateTime? _dtets = e.NewValues["ETS"] != null ? wwi_func.vdatetime(e.NewValues["ETS"].ToString()) : (DateTime?)null; 
        
        ASPxComboBox _cbo = (ASPxComboBox)this.fmvVoyage.FindControl("dxcboVesselID");
        if (_cbo != null && _cbo.Value != null)
        {
            _vesselid = wwi_func.vint(_cbo.Value.ToString());
        }

        if (_etsid > 0)
        {
            VoyageETSSubTable _tbl = new VoyageETSSubTable(_etsid);
            _tbl.OriginPortID = _portid;
            _tbl.Ets = _dtets;
            _tbl.Save();

            //***** for testing
            //_portid = 1;
            //_vesselid = 3246;
            //*****

            //update ordertable if _portid > 0 and _vesselid > 0 
            //use voayge id NOT vessel id
            //if (_portid > 0 && _vesselid > 0) //&& (e.NewValues["ETS"] != e.OldValues["ETS"]))
            if (_portid > 0 && _voyageid > 0)
            {
                //update_ordertable_ets(_portid, _vesselid, _dtets);
                update_ordertable_ets(_portid, _voyageid, _dtets);

                //also does not work!
                //string[] _cols = { "OrderID", "ETS", "VesselLastUpdated" };
                //SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.OrderTable).
                //    Where(OrderTable.PortIDColumn).IsEqualTo(_portid).And(OrderTable.VesselIDColumn).IsEqualTo(_vesselid);
                //DataTable _dt = new DataTable();
                //DataColumn[] _col = { _dt.Columns["OrderID"] }; //need to define a primary key for update to work
                //_dt.PrimaryKey = _col;
                //
                //SqlDataAdapter _da = new SqlDataAdapter();
                //SqlCommandBuilder _cb = new SqlCommandBuilder(_da); 
                //ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                //using (SqlConnection _con = new SqlConnection(_cs.ConnectionString))
                //using (SqlCommand _cmd = new SqlCommand())
                //{
                    //_cmd.CommandText = "SELECT OrderID, ETS, VesselLastUpdated FROM OrderTable WHERE ((PortID=@idport) AND (VesselID=@idvessel));";
                    //_cmd.Parameters.AddWithValue("@idport", _portid);
                    //_cmd.Parameters.AddWithValue("@idvessel", _vesselid);
                    //_cmd.Connection = _con;
                    //_da.SelectCommand = _cmd; 
                    //_da.Fill(_dt);
                                       
                    //for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
                    //{
                    //    _dt.Rows[_ix]["ETS"] = Convert.ToDateTime(_dtets);
                    //    _dt.Rows[_ix]["VesselLastUpdated"] = Convert.ToDateTime(_now);
                    //}
                    //
                    //int _result = _da.Update(_dt); 
                //}
                
                //does not work
                //string _sql = "UPDATE OrderTable " +
                //                    "SET OrderTable.Ets=@etsdate, " +
                //                    "OrderTable.VesselLastUpdated=@now " +
                //                    "FROM OrderTable as o " +
                //                    "WHERE o.OrderID IN (SELECT DISTINCT OrderID FROM OrderTable WHERE ((o.PortID=@port) AND (o.VesselID=@vessel)));";
                //ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                //using (SqlConnection _con = new SqlConnection(_cs.ConnectionString))
                //using (SqlCommand _cmd = new SqlCommand())
                //{
                //    _cmd.CommandText = _sql;
                //    _cmd.Parameters.AddWithValue("@etsdate", Convert.ToDateTime(_dtets));
                //   _cmd.Parameters.AddWithValue("@now", Convert.ToDateTime(_now));
                //   _cmd.Parameters.AddWithValue("@port", _portid);
                //    _cmd.Parameters.AddWithValue("@vessel", _vesselid);
                //   _cmd.Connection = _con;
                //    _con.Open();
                //    int _result = _cmd.ExecuteNonQuery();
                //}
                
                //does not work
                //string[] _cols = { "OrderID" };
                //SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.OrderTable).
                //    Where(OrderTable.PortIDColumn).IsEqualTo(_portid).And(OrderTable.VesselIDColumn).IsEqualTo(_vesselid);
                //
                //IList<int> _orders = _qry.ExecuteTypedList<int>();
                //if (_orders.Count > 0)
                //{ 
                //    SubSonic.Update _upd = new SubSonic.Update(DAL.Logistics.Schemas.OrderTable);
                //    IList<int> _recordsaffected = _upd.Set(OrderTable.EtsColumn).EqualTo(Convert.ToDateTime(_dtets)).
                //                            Set(OrderTable.VesselLastUpdatedColumn).EqualTo(Convert.ToDateTime(_now)).
                //                            Where(OrderTable.OrderIDColumn).In(_orders).ExecuteTypedList<int>();        
                //}
            }
            //end if vesselid > 0
        }//end if voyageetssubid > 0

        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //rebind
        bind_ets_grid(_voyageid);  
    }
    //end ets row updating

    /// <summary>
    /// delete row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETS_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint(this.dxhfOrder.Get("voyage").ToString()) : 0;
        
       
        int _etsid = e.Keys["VoyageETSSubID"] != null ? wwi_func.vint(e.Keys["VoyageETSSubID"].ToString()) : 0;
        if (_etsid > 0)
        {
            VoyageETSSubTable.Delete(_etsid);  
        }
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //rebind
        bind_ets_grid(_voyageid);  
    }

    protected void update_ordertable_ets(int portid, int vesselid, DateTime? dtets)
    {
        try
        {
            SubSonic.SqlQuery _qry = DB.Select().From(DAL.Logistics.Tables.OrderTable).Where("PortID").IsEqualTo(portid).And("VesselID").IsEqualTo(vesselid);
            OrderTableCollection _orders = new OrderTableCollection();
            _orders.LoadAndCloseReader(_qry.ExecuteReader());

            if (_orders.Count > 0)
            {
                for (int _ix = 0; _ix < _orders.Count; _ix++)
                {
                    _orders[_ix].Ets = dtets;
                    _orders[_ix].VesselLastUpdated = DateTime.Now;
                }
                _orders.BatchSave();

                //this.dxlblInfo.Text = string.Format("{0} Orders have been updated", _orders.Count.ToString());
                //this.dxpnlMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            //this.dxlblErr.Text = _err;
            //this.dxpnlErr.Visible = true;
        }
    }
    #endregion

    #region ETA grid binding and crud events
    protected void bind_eta_grid(int voyageid)
    {
        string[] _cols = { "VoyageETASubTable.VoyageETASubID", "VoyageETASubTable.VoyageID", "VoyageETASubTable.DestinationPortID", "VoyageETASubTable.ETA", "PortTable.PortName" };
        SubSonic.SqlQuery _qry = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VoyageETASubTable).
            LeftOuterJoin(DAL.Logistics.PortTable.PortIDColumn, DAL.Logistics.VoyageETASubTable.DestinationPortIDColumn)
            .WhereExpression("VoyageID").IsEqualTo(voyageid);
        
        DataTable _dt = _qry.ExecuteDataSet().Tables[0];
        this.dxgrdETA.DataSource = _dt;
        this.dxgrdETA.KeyFieldName = "VoyageETASubID";
        this.dxgrdETA.DataBind();
    }

    /// <summary>
    /// change OriginPortID column to display portname instead when in view mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETA_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName != "DestinationPortID") return;
        e.DisplayText = e.GetFieldValue(e.VisibleRowIndex, "PortName") != null ? e.GetFieldValue(e.VisibleRowIndex, "PortName").ToString() : "";
    }

    /// <summary>
    /// bind port combo when editting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETA_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "DestinationPortID")
        {
            ASPxComboBox _combo = (ASPxComboBox)e.Editor;

            string[] _cols = { "PortTable.PortID, PortTable.PortName" };
            string[] _order = { "PortName" };
            SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();
            _qry = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).OrderAsc(_order);


            //bind origin ports
            IDataReader _rd = _qry.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "PortID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PortName";
            _combo.DataBindItems();
        }
    }
    /// <summary>
    /// insert event for ETA grid
    /// just need to save to VoyageETASubTable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETA_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        DateTime? _dtnull = null;
        
        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint(this.dxhfOrder.Get("voyage").ToString()) : 0;
        int _vesselid = this.dxhfOrder.Contains("vessel") ? wwi_func.vint(this.dxhfOrder.Get("vessel").ToString()) : 0; 
        int _portid = e.NewValues["DestinationPortID"] != null? wwi_func.vint(e.NewValues["DestinationPortID"].ToString()): 0;
        DateTime? _dteta =  e.NewValues["ETA"] != null ? wwi_func.vdatetime(e.NewValues["ETA"].ToString()): _dtnull;

        if (_voyageid > 0)
        {
            VoyageETASubTable _tbl = new VoyageETASubTable();
            _tbl.VoyageID = _voyageid;
            _tbl.DestinationPortID = _portid;
            _tbl.Eta = _dteta;
            _tbl.Save();

            if (_portid > 0 && _vesselid > 0)
            {
                update_ordertable_eta(_portid, _vesselid, _dteta);
            }
        }

        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //rebind
        bind_eta_grid(_voyageid);  
    }
    /// <summary>
    /// update event for ETS grid
    /// check DATE. if it's been changed will need to update order table
    /// ordertabel.ETA = new ETA, ordertable.VesselLastUpdated = now wher ordertable.DestinationPortID = DestinationPortID and ordertable.VesselID = VesselID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETA_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint(this.dxhfOrder.Get("voyage").ToString()) : 0;
        int _vesselid = this.dxhfOrder.Contains("vessel") ? wwi_func.vint(this.dxhfOrder.Get("vessel").ToString()) : 0; 
        //key for this record
        int _etaid = wwi_func.vint(e.Keys["VoyageETASubID"].ToString());
        //other required data
        int _portid = e.NewValues["DestinationPortID"] != null ? wwi_func.vint(e.NewValues["DestinationPortID"].ToString()) : 0;
        DateTime? _dteta = e.NewValues["ETA"] != null ? wwi_func.vdatetime(e.NewValues["ETA"].ToString()) : (DateTime?)null;
        
        if (_etaid > 0)
        {
            VoyageETASubTable _tbl = new VoyageETASubTable(_etaid);
            _tbl.DestinationPortID = _portid;
            _tbl.Eta = _dteta;
            _tbl.Save();

            //******** for testing 
            //_portid = 80;
            //_vesselid = 196;
            //********
            //use voyage id NOT vesel id
            //if (_portid > 0 && _vesselid > 0) //&& (e.NewValues["ETA"] != e.OldValues["ETA"]))
            if(_portid > 0 && _voyageid > 0)
            {
                //update_ordertable_eta(_portid, _vesselid, _dteta);
                update_ordertable_eta(_portid, _voyageid, _dteta);
            }
            //update ordertable if _VoyageETASubID > 0 and _VesselID > 0 and date has been changed
            //if (_vesselid > 0 && (e.NewValues["ETA"] != e.OldValues["ETA"]))
            //{
            //    SubSonic.Update _upd = new SubSonic.Update(DAL.Logistics.Schemas.OrderTable);
            //    int _recordsaffected = _upd.Set(OrderTable.EtaColumn).EqualTo(_tbl.Eta)
            //                           .Where(OrderTable.PortIDColumn).IsEqualTo(_tbl.DestinationPortID)
            //                           .And(OrderTable.VesselIDColumn).IsEqualTo(_vesselid)
            //                           .Execute();
            //}

            //end if vesselid > 0
        }//end if voyageetasubid > 0

        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //rebind
        bind_eta_grid(_voyageid);  
    }
    //end eta row updating

    /// <summary>
    /// delete row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdETA_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _voyageid = this.dxhfOrder.Contains("voyage") ? wwi_func.vint(this.dxhfOrder.Get("voyage").ToString()) : 0;


        int _etaid = e.Keys["VoyageETASubID"] != null ? wwi_func.vint(e.Keys["VoyageETASubID"].ToString()) : 0;
        if (_etaid > 0)
        {
            VoyageETASubTable.Delete(_etaid);
        }
        //MUST call this after insert or error: Specified method is not supported
        e.Cancel = true;
        _grd.CancelEdit();
        //rebind
        bind_eta_grid(_voyageid);
    }

    protected void update_ordertable_eta(int portid, int vesselid, DateTime? dteta)
    {
        try
        {
            SubSonic.SqlQuery _qry = DB.Select().From(DAL.Logistics.Tables.OrderTable).Where("DestinationPortID").IsEqualTo(portid).And("VesselID").IsEqualTo(vesselid);
            OrderTableCollection _orders = new OrderTableCollection();
            _orders.LoadAndCloseReader(_qry.ExecuteReader());

            if (_orders.Count > 0)
            {
                for (int _ix = 0; _ix < _orders.Count; _ix++)
                {
                    _orders[_ix].Eta = dteta;
                    _orders[_ix].VesselLastUpdated = DateTime.Now;
                }
                _orders.BatchSave();

                //this.dxlblInfo.Text = string.Format("{0} Orders have been updated", _orders.Count.ToString());
                //this.dxpnlMsg.Visible   = true;
            }
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            //this.dxlblErr.Text = _err;
            //this.dxpnlErr.Visible  = true;
        }
    }
    #endregion

    #region incremental filtering for large combobox datasets
    //14/07/14 dxcboVesselID_ItemRequestedByValue and dxcboVesselID_ItemsRequestedByFilterCondition DEPRECATED 
    //can't use OnItemsRequestedByFilterCondition and OnItemRequestedByValue on this combo as server-side filtring makes the search case sensitive
    //incremental filtering for large datasets on combos
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// company name is only available to publiship users
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboVesselID_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VesselID", "VesselName" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).WhereExpression("CompanyID").IsEqualTo(_id);
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VesselTable).WhereExpression("VesselID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VesselID";
        _combo.TextField = "VesselName";
        _combo.DataBindItems();

    }
    protected void dxcboVesselID_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VesselID", "VesselName" };
        string[] _sort = { "VesselName" };
        
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VesselTable).Paged(e.BeginIndex + 1, e.EndIndex + 1).Where("VesselName").StartsWith(string.Format("{0}%", _filter));
           

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VesselID";
        _combo.TextField = "VesselName";
        _combo.DataBindItems();
    }
    //end incremental filtering of vessel name

    /// <summary>
    /// origin ports in ETS grid 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboOriginPortID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        //ASPxComboBox _combo = (ASPxComboBox)source;
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgrdETS.FindEditRowCellTemplateControl(
               this.dxgrdETS.Columns["colOriginPortID"] as GridViewDataComboBoxColumn, "dxcboOriginPortID"));

        //use datareaders - much faster than loading into collections
        string[] _cols = { "PortTable.PortID, PortTable.PortName" };
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "PortID").WhereExpression("PortName").StartsWith(string.Format("{0}%", e.Filter.ToString()));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "PortID";
        _combo.ValueType = typeof(Int32);
        _combo.TextField = "PortName";
        _combo.DataBind();
    }


    protected void dxcboOriginPortID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgrdETS.FindEditRowCellTemplateControl(
               this.dxgrdETS.Columns["colOriginPortID"] as GridViewDataComboBoxColumn, "dxcboOriginPortID"));

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "PortTable.PortID, PortTable.PortName" };
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).WhereExpression("PortID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "PortID";
        _combo.ValueType = typeof(Int32);
        _combo.TextField = "PortName";
        _combo.DataBind();

    }
    //end origin ports ****************

    /// <summary>
    /// destination ports in ETA grid 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboDestinationPortID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        //ASPxComboBox _combo = (ASPxComboBox)source;
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgrdETS.FindEditRowCellTemplateControl(
               this.dxgrdETS.Columns["colDestinationPortID"] as GridViewDataComboBoxColumn, "dxcboDestinationPortID"));

        //use datareaders - much faster than loading into collections
        string[] _cols = { "PortTable.PortID, PortTable.PortName" };
        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex +1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString())).And("Customer").IsEqualTo(true) ;
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).Paged(e.BeginIndex + 1, e.EndIndex + 1, "PortID").WhereExpression("PortName").StartsWith(string.Format("{0}%", e.Filter.ToString()));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "PortID";
        _combo.ValueType = typeof(Int32);
        _combo.TextField = "PortName";
        _combo.DataBind();
    }


    protected void dxcboDestinationPortID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox _combo = ((ASPxComboBox)this.dxgrdETS.FindEditRowCellTemplateControl(
               this.dxgrdETS.Columns["colDestinationPortID"] as GridViewDataComboBoxColumn, "dxcboDestinationPortID"));

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "PortTable.PortID, PortTable.PortName" };
        SubSonic.SqlQuery _query = new SubSonic.SqlQuery();
        _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).WhereExpression("PortID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "PortID";
        _combo.ValueType = typeof(Int32);
        _combo.TextField = "PortName";
        _combo.DataBind();

    }
    //end destination ports
    #endregion

    #region dll binding
    protected void bind_added_by()
    {
        string[] _cols = { "EmployeeID, Name" };
        string[] _order = { "Name" };
        SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.EmployeesTable).Where("Live").IsEqualTo(true).OrderAsc(_order);

        //order controller
        ASPxComboBox _combo = (ASPxComboBox)this.fmvVoyage.FindControl("dxcboAddedBy");
        if (_combo != null)
        {
            IDataReader _rd1 = _qry.ExecuteReader();
            _combo.DataSource = _rd1;
            _combo.ValueField = "EmployeeID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "Name";
            _combo.DataBindItems();
        }
    }
    /// <summary>
    /// DEPRECATED we are using incremental filtering for origin ports and destination ports 
    /// origin port combo 
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_origin_port()
    {
        GridViewDataComboBoxColumn _combo = (GridViewDataComboBoxColumn)this.dxgrdETS.Columns["colOriginPortID"];

        if (_combo != null)
        {
           

            string[] _cols = { "PortTable.PortID, PortTable.PortName" };
            string[] _order = { "PortName" };
            SubSonic.SqlQuery _qry = new SubSonic.SqlQuery();
            _qry = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.PortTable).OrderAsc(_order);

            
            //bind origin ports
            IDataReader _rd = _qry.ExecuteReader();
            _combo.PropertiesComboBox.DataSource = _rd;
            _combo.PropertiesComboBox.ValueField = "PortID";
            _combo.PropertiesComboBox.ValueType = typeof(int);
            _combo.PropertiesComboBox.TextField = "PortName";
            //can't databinditems here, do it in cell editor intialise
        }
    }

    /// <summary>
    ///  DEPRECATED we are using incremental filtering for origin ports and destination ports 
    /// destination port combo filtered by VoyageID
    /// </summary>
    /// <param name="voyageid">from voyage combobox int</param>
    protected void bind_destination_port(int voyageId)
    {
        ASPxComboBox _combo = (ASPxComboBox)this.fmvVoyage.FindControl("dxcboDestPort");
        if (_combo != null)
        {
            string[] _cols = { "PortTable.PortID", "PortTable.PortName" };
            string[] _order = { "PortName" };

            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.PortTable);
               
            //rebind dest ports
            IDataReader _rd = _qry.ExecuteReader();
            _combo.DataSource = _rd;
            _combo.ValueField = "PortID";
            _combo.ValueType = typeof(int);
            _combo.TextField = "PortName";
            _combo.DataBindItems();
        }
    }
    #endregion

    #region functions
    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_token(string namedtoken)
    {
        string _value = this.dxhfOrder.Contains(namedtoken) ? this.dxhfOrder.Get(namedtoken).ToString() : null;

        if (string.IsNullOrEmpty(_value))
        {
            _value = Page.Request[namedtoken] != null ? HttpUtility.UrlDecode(Page.Request[namedtoken].ToString(), System.Text.Encoding.Default) : null;
        }

        //don't decrypt it here
        //if (_value != null) { _value = wwi_security.DecryptString(_value, "publiship"); }
        return _value;
    }
    //end get saved token
    /// <summary>
    /// some formating functions for Evals  this does not work with Bind you can't enclose Bind with functions
    /// </summary>
    /// <param name="testvalue">object to check</param>
    /// <returns>empty string if null or object value</returns>
    public string nullValue(object testvalue)
    {
        if (testvalue == null)
            return "";
        return testvalue.ToString();

    }
    #endregion

}
