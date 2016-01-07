using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Popupcontrol_order_name_address : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get key id
        //int _pid = wwi_func.vint(wwi_security.DecryptString(get_token("pid").ToString(), "publiship"));
        int _pid = wwi_func.vint(get_token("pid"));
         
        if (!Page.IsPostBack)
        {
            //bind company form
            string _mode = get_token("mode"); 
            set_mode(_mode);
            bind_company(_mode);
       }
       
        //company contacts only visible in formview.Edit mode
        bind_contacts(_pid);
        
    }

    #region form data binding
    protected void bind_company(string mode)
    {
        int _pid = wwi_func.vint(get_token("pid"));

        //SubSonic.Query _q = new SubSonic.Query(Tables.NameAndAddressBook, "WWIProv").WHERE("CompanyID", Comparison.Equals, _pid);
        DAL.Logistics.NameAndAddressBookCollection _tbc = new DAL.Logistics.NameAndAddressBookCollection();

        if (mode != "Insert")
        {
           

            DAL.Logistics.NameAndAddressBook _tbl = new DAL.Logistics.NameAndAddressBook(_pid);
            _tbc.Add(_tbl);
        }
        else
        {
            DAL.Logistics.NameAndAddressBook _tbl = new DAL.Logistics.NameAndAddressBook();
            _tbc.Add(_tbl);
        }
        
        string[] _keys = { "CompanyID" };
        //IDataReader _dr = _q.ExecuteReader(); 
        this.fmvAddressBook.DataSource = _tbc; //_dr;
        this.fmvAddressBook.DataKeyNames = _keys;
        this.fmvAddressBook.DataBind();
        //_dr.Close(); 

       
    }

    
    /// </summary>
    /// <param name="companyID"></param>
    protected void bind_contacts(int companyid)
    {
        ASPxGridView _grid = (ASPxGridView)this.fmvAddressBook.FindControl("dxgridCompanyContacts");
        //ASPxGridView _grid = (ASPxGridView)this.dxpnlContacts.FindControl("dxgridCompanyContacts");
        if (_grid != null)
        {
            SubSonic.Query _q = new SubSonic.Query(Tables.ContactTable, "WWIProv").WHERE("CompanyID", Comparison.Equals, companyid);
            DataSet _ds = _q.ExecuteDataSet(); 
            //IDataReader _dr = _q.ExecuteReader(); can't use datareder it's a read only structure!
            _grid.KeyFieldName = "ContactID";
            _grid.DataSource = _ds;
            _grid.DataBind();
        }       
    }
     
    /// <summary>
    /// DEPRECATED we are binding in code-behind. Filter data grid by company id using FetchbyQuery method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void objdsContact_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        int _cid = Request.QueryString["cid"] != null ? wwi_func.vint(Request.QueryString["cid"].ToString()) : -1;
        SubSonic.Query _q = new SubSonic.Query(Tables.ContactTable,"WWIProv").WHERE("CompanyID", SubSonic.Comparison.Equals, _cid); 
        e.InputParameters["qry"] = _q;
    }

    protected void dxgridContact_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {

        Int32 _mode = Request.QueryString["cid"] != null ? wwi_func.vint(Request.QueryString["cid"].ToString()) : 0;
        bind_contacts(_mode);
    }

    protected void fmvAddressBook_DataBound(object sender, EventArgs e)
    {
        //lookup values for country and company type
        NameAndAddressBook _row = (NameAndAddressBook)this.fmvAddressBook.DataItem;
        
        if (this.fmvAddressBook.CurrentMode == FormViewMode.ReadOnly)
        {

            ASPxLabel _lbl = (ASPxLabel)this.fmvAddressBook.FindControl("dxlblitemCountryID");
            if (_lbl != null)
            {
                int? _country = _row.CountryID != null ? _row.CountryID : 0;
                _lbl.Text = wwi_func.lookup_value("CountryName", "CountryTable", "CountryID", _country);
            }

            _lbl = (ASPxLabel)this.fmvAddressBook.FindControl("dxlblitemTypeID");
            if (_lbl != null)
            {
                int? _type = _row.TypeID != null ? _row.TypeID : 0;
                _lbl.Text = wwi_func.lookup_value("TypeName", "TypeTable", "TypeID", _type);
            }
        }
        else
        {
            //dlls
            bind_country();
            bind_company_type();
            //if a type id ahs been passed
            string _type = get_token("type");
            if (!string.IsNullOrEmpty(_type)) {
                //insert mode modify label text
                ASPxLabel _dxlbl = (ASPxLabel)this.fmvAddressBook.FindControl("dxlblinsertCompanyID");
                if (_dxlbl != null) {
                    _dxlbl.Text = "New " + _type; 
                }
                //set company type
                ASPxComboBox _dxcboType = (ASPxComboBox)this.fmvAddressBook.FindControl("dxcboCompanyType");
                if (_dxcboType != null) {
                    _dxcboType.SelectedItem = _dxcboType.Items.FindByText(_type); 
                }
            }
            //bind contacts grid
            if (_row != null) { bind_contacts(_row.CompanyID); }
        }
       
    }
    #endregion

    #region combo binding
    protected void bind_country()
    {
        ASPxComboBox _dxcboCountry = (ASPxComboBox)this.fmvAddressBook.FindControl("dxcboCountry");
        if (_dxcboCountry != null)
        {
            string[] _cols = { "CountryID, CountryName" };
            string[] _order = { "CountryName" };
            SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.CountryTable).OrderAsc(_order);
            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboCountry.DataSource = _rd1;
            _dxcboCountry.ValueField = "CountryID";
            _dxcboCountry.TextField = "CountryName";
            _dxcboCountry.DataBindItems();
        }
    }
    //end bind country

    protected void bind_company_type()
    {
        ASPxComboBox _dxcboType = (ASPxComboBox)this.fmvAddressBook.FindControl("dxcboCompanyType");
        if (_dxcboType != null)
        {
            string[] _cols = { "TypeID, TypeName" };
            string[] _order = { "TypeName" };
            SubSonic.SqlQuery _qry = new Select(_cols).From(DAL.Logistics.Tables.TypeTable).OrderAsc(_order);

            IDataReader _rd1 = _qry.ExecuteReader();
            _dxcboType.DataSource = _rd1;
            _dxcboType.ValueField = "TypeID";
            _dxcboType.TextField = "TypeName";
            _dxcboType.DataBindItems();
        }
    }
    //end bind company type
    #endregion

    #region menuitem events
    protected void set_mode(string mode)
    {
        List<string> _menuitems = new List<string>();

        switch (mode)
        {
            case "Edit":
                {
                    this.fmvAddressBook.ChangeMode(FormViewMode.Edit);
                    _menuitems.Add("Update");
                    _menuitems.Add("Cancel");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "Insert":
                {
                    this.fmvAddressBook.ChangeMode(FormViewMode.Insert);
                    _menuitems.Add("Save");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            case "ReadOnly":
                {
                    this.fmvAddressBook.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
            default: //default to view
                {
                    this.fmvAddressBook.ChangeMode(FormViewMode.ReadOnly);
                    _menuitems.Add("Edit");
                    _menuitems.Add("Close");
                    enable_menu_items(_menuitems);
                    break;
                }
        }
    }
    /// <summary>
    /// you must have this for changing mode on click events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvAddressBook_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvAddressBook.ChangeMode(e.NewMode);
    }
    //end mode changing

    /// <summary>
    /// on click events
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxeditFormMenu_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        string _item = e.Item.Name.ToString();

        List<string> _menuitems = new List<string>();

        switch (_item)
        {
            case "miNew":
                {
                    set_mode("Insert");
                    //rebind formview to force mode change
                    bind_company("Insert"); 
                    break;
                }
            case "miEdit":
                {
                    set_mode("Edit");
                    bind_company("Edit"); 
                    break;
                }
            case "miDelete": //not enabled - do we want users to delete records?
                {
                    //this.fmvAddressBook.DeleteItem(); 
                    break;
                }
            case "miUpdate":
                {
                    //for testing purposes
                    //string _s = "";
                    //ASPxMemo _memo = (ASPxMemo)this.formOrder.FindControl("dxmemoRemarksToCustomer");
                    //if (_memo != null) {
                    //    _s = _memo.Text.ToString(); 
                    //}
                    //OrderTable _o = new OrderTable(303635);
                    //_o.RemarksToCustomer = _s;
                    //_o.Save(); 
                    this.fmvAddressBook.UpdateItem(false);
                    set_mode("ReadOnly");
                    bind_company("ReadOnly"); 
                    break;
                }
            case "miSave":
                {
                    this.fmvAddressBook.InsertItem(false);
                    set_mode("ReadOnly");
                    bind_company("ReadOnly"); 
                    break;
                }
            case "miCancel": 
                {
                    //this.fmvAddressBook.ChangeMode(FormViewMode.ReadOnly);
                    set_mode("ReadOnly");
                    bind_company("ReadOnly"); 
                    break;
                }
            default:
                {
                    break;
                }
        }
        //end switch
    }
    //end menu commands

    /// <summary>
    /// enable menu items passed as list, disable all items not in list
    /// </summary>
    /// <param name="active">list of menu iotems to enable</param>
    protected void enable_menu_items(List<string> active)
    {
        bool _isactive;

        for (int _ix = 0; _ix < this.dxmenuFormView.Items.Count; _ix++)
        {
            _isactive = false;

            for (int _mx = 0; _mx < active.Count; _mx++)
            {
                if (this.dxmenuFormView.Items[_ix].Text == active[_mx]) { _isactive = true; }
            }//end active names loop

            this.dxmenuFormView.Items[_ix].ClientVisible = _isactive;
        }
    }//end menu names loop

    #endregion

    #region company crud operations
    protected void fmvAddressBook_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int _user = Page.Session["user"] != null ? (int)((UserClass)Page.Session["user"]).UserId : 0;
        int? _intnull = null;
        //this.objdsAddressBook.InsertParameters["DateRecordAdded"].DefaultValue = DateTime.Now.ToShortDateString();
        //this.objdsAddressBook.InsertParameters["RecordAddedBY"].DefaultValue = _username;
        FormView _f = (FormView)this.FindControl("fmvAddressBook");
        if (_f != null)
        {
            NameAndAddressBook _n = new NameAndAddressBook();
            _n.DateRecordAdded = DateTime.Now;
            _n.RecordAddedBY = _user;

            ASPxTextBox _tx = (ASPxTextBox)_f.FindControl("dxtxtname");
            if (_tx != null) { _n.CompanyName = _tx.Text != null? _tx.Text.ToString(): ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress1");
            if (_tx != null) { _n.Address1 = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress2");
            if (_tx != null) { _n.Address2 = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress3");
            if (_tx != null) { _n.Address3 = _tx.Text != null ? _tx.Text.ToString() : ""; }

            ASPxComboBox _cb = (ASPxComboBox)_f.FindControl("dxcboCountry");
            if (_cb != null) { _n.CountryID = _cb.Value != null? wwi_func.vint(_cb.Value.ToString()): _intnull; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtPostCode");
            if (_tx != null) { _n.PostCode = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtTelNo");
            if (_tx != null) { _n.TelNo = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtFax");
            if (_tx != null) { _n.FaxNo = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtEmail");
            if (_tx != null) { _n.MainEmail = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _cb = (ASPxComboBox)_f.FindControl("dxcboCompanyType");
            if (_cb != null) { _n.TypeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

            ASPxMemo _m = (ASPxMemo)_f.FindControl("dxmemoDelivery");
            if (_m != null) { _n.SpecialDeliveryInstructions = _m.Text != null ? _m.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtPalletSpec");
            if (_tx != null) { _n.PalletDims = _tx.Text != null ? _tx.Text.ToString() : ""; }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtMaxWeight"); //check for nullable int
            if (_tx != null)
            {
                int? _v = 0;
                if (!string.IsNullOrEmpty(_tx.Text.ToString())) { _v = wwi_func.vint(_tx.Text.ToString()); }
                _n.MaxPalletWeight = _v.Value;
            }

            _tx = (ASPxTextBox)_f.FindControl("dxtxtMaxHeight"); //check for nullable int
            if (_tx != null)
            {
                int? _v = 0;
                if (!string.IsNullOrEmpty(_tx.Text.ToString())) { _v = wwi_func.vint(_tx.Text.ToString()); }
                _n.MaxPalletHeight = _v.Value;
            }

            ASPxCheckBox _ck = (ASPxCheckBox)_f.FindControl("dxckExporter");
            if (_ck != null) { _n.Exporter = _ck.Checked; }

            _ck = (ASPxCheckBox)_f.FindControl("dxckCustomer");
            if (_ck != null) { _n.Customer = _ck.Checked; }

            _ck = (ASPxCheckBox)_f.FindControl("dxckConsignee");
            if (_ck != null) { _n.Consignee = _ck.Checked; }

            _ck = (ASPxCheckBox)_f.FindControl("dxckInsurance");
            if (_ck != null) { _n.Insurance = _ck.Checked; }

            _ck = (ASPxCheckBox)_f.FindControl("dxckSalesTarget");
            if (_ck != null) { _n.SalesModule = _ck.Checked; }

            _n.Save();
            //return new company id
            int _newid = (Int32)_n.GetPrimaryKeyValue();
            //add to hidden field as we won't be able to retrive this from page request
            if (this.dxhfOrder.Contains("pid")) { this.dxhfOrder.Remove("pid"); }
            this.dxhfOrder.Add("pid", _newid);
            ///update id label
            ASPxLabel _lb = (ASPxLabel)_f.FindControl("dxlblCompanyID");
            if (_lb != null) { _lb.Text = _newid.ToString(); }
        }
    }
    //end formview inserting
    /// <summary>
    /// by binding in code behind we can avoid objectdatasource not updating nullable values correctly
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvAddressBook_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        int _id = wwi_func.vint(get_token("pid"));
        int? _intnull = null;

        if (_id > 0)
        {
            FormView _f = (FormView)this.FindControl("fmvAddressBook");
            if (_f != null)
            {
                //ASPxLabel _lb = (ASPxLabel)_f.FindControl("dxlblCompanyID");
                //if (_lb != null) { _id = _lb.Text.ToString(); }

                NameAndAddressBook _n = new NameAndAddressBook(_id);

                ASPxTextBox _tx = (ASPxTextBox)_f.FindControl("dxtxtName");
                if (_tx != null) { _n.CompanyName = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress1");
                if (_tx != null) { _n.Address1 = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress2");
                if (_tx != null) { _n.Address2 = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtAddress3");
                if (_tx != null) { _n.Address3 = _tx.Text != null ? _tx.Text.ToString() : ""; }

                ASPxComboBox _cb = (ASPxComboBox)_f.FindControl("dxcboCountry");
                if (_cb != null) { _n.CountryID = wwi_func.vint(_cb.Value.ToString()); }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtPostCode");
                if (_tx != null) { _n.PostCode = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtTelNo");
                if (_tx != null) { _n.TelNo = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtFax");
                if (_tx != null) { _n.FaxNo = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtEmail");
                if (_tx != null) { _n.MainEmail = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _cb = (ASPxComboBox)_f.FindControl("dxcboCompanyType");
                if (_cb != null) { _n.TypeID = _cb.Value != null ? wwi_func.vint(_cb.Value.ToString()) : _intnull; }

                ASPxMemo _m = (ASPxMemo)_f.FindControl("dxmemoDelivery");
                if (_m != null) { _n.SpecialDeliveryInstructions = _m.Text!= null? _m.Text.ToString(): ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtPalletSpec");
                if (_tx != null) { _n.PalletDims = _tx.Text != null ? _tx.Text.ToString() : ""; }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtMaxWeight"); //check for nullable int
                if (_tx != null)
                {
                    int? _v = 0;
                    if (!string.IsNullOrEmpty(_tx.Text.ToString())) { _v = wwi_func.vint(_tx.Text.ToString()); }
                    _n.MaxPalletWeight = _v;
                }

                _tx = (ASPxTextBox)_f.FindControl("dxtxtMaxHeight"); //check for nullable int
                if (_tx != null)
                {
                    int? _v = 0;
                    if (!string.IsNullOrEmpty(_tx.Text.ToString())) { _v = wwi_func.vint(_tx.Text.ToString()); }
                    _n.MaxPalletHeight = _v;
                }

                ASPxCheckBox _ck = (ASPxCheckBox)_f.FindControl("dxckExporter");
                if (_ck != null) { _n.Exporter = _ck.Checked; }

                _ck = (ASPxCheckBox)_f.FindControl("dxckCustomer");
                if (_ck != null) { _n.Customer = _ck.Checked; }

                _ck = (ASPxCheckBox)_f.FindControl("dxckConsignee");
                if (_ck != null) { _n.Consignee = _ck.Checked; }

                _ck = (ASPxCheckBox)_f.FindControl("dxckInsurance");
                if (_ck != null) { _n.Insurance = _ck.Checked; }

                _ck = (ASPxCheckBox)_f.FindControl("dxckSalesTarget");
                if (_ck != null) { _n.SalesModule = _ck.Checked; }

                _n.Save();
            }
            //end formview updating
        }
    }
    
    #endregion

    #region company contact crud
    /// <summary>
    /// update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridCompanyContacts_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _pid = wwi_func.vint(get_token("pid"));
        int _contactid = wwi_func.vint(e.Keys["ContactID"].ToString());

        try
        {
            //courier details are joined to OrderTable on OrderNumber

            if (_contactid > 0)
            {
                //get details
                ContactTable _t = new ContactTable(_contactid);
                //text
                if (e.NewValues["ContactName"] != null) { _t.ContactName = e.NewValues["ContactName"].ToString(); } else { _t.ContactName = ""; }
                if (e.NewValues["ContactInitials"] != null) { _t.ContactInitials = e.NewValues["ContactInitials"].ToString(); } else { _t.ContactInitials = ""; }
                if (e.NewValues["EMail"] != null) { _t.EMail = e.NewValues["EMail"].ToString(); } else { _t.EMail = ""; }
                //tick boxes
                _t.OrderAck = e.NewValues["OrderAck"] != null ? wwi_func.vbool(e.NewValues["OrderAck"].ToString()) == true ? true : false : false;

                //update record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            //this.dxlblErr.Text = string.Format("Delivery # {0} NOT updated. Error: {1}", _orderno, _ex);
            //this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_contacts(_pid);
        }
    }
    /// <summary>
    /// insert
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridCompanyContacts_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _pid = wwi_func.vint(get_token("pid"));

        try
        {
            //courier details are joined to OrderTable on OrderNumber

            if (_pid > 0)
            {
                //get details
                ContactTable _t = new ContactTable();
                //set company id
                _t.CompanyID = _pid;
                //text
                if (e.NewValues["ContactName"] != null) { _t.ContactName = e.NewValues["ContactName"].ToString(); } else { _t.ContactName = ""; }
                if (e.NewValues["ContactInitials"] != null) { _t.ContactInitials = e.NewValues["ContactInitials"].ToString(); } else { _t.ContactInitials = ""; }
                if (e.NewValues["EMail"] != null) { _t.EMail = e.NewValues["EMail"].ToString(); } else { _t.EMail = ""; }
                //tick boxes
                _t.OrderAck = e.NewValues["OrderAck"] != null ? wwi_func.vbool(e.NewValues["OrderAck"].ToString()) == true ? true : false : false;

                //insert record
                _t.Save();
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            //this.dxlblErr.Text = string.Format("Delivery # {0} NOT updated. Error: {1}", _orderno, _ex);
            //this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_contacts(_pid);
        }
    }
    /// <summary>
    /// delete
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridCompanyContacts_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;
        int _pid = wwi_func.vint(get_token("pid"));
        int _contactid = wwi_func.vint(e.Keys["ContactID"].ToString());

        try
        {
            //courier details are joined to OrderTable on OrderNumber

            if (_contactid > 0)
            {
                //delete details
                ContactTable.Delete("ContactID", _contactid);
            }
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            //this.dxlblErr.Text = string.Format("Delivery # {0} NOT updated. Error: {1}", _orderno, _ex);
            //this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_contacts(_pid);
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
