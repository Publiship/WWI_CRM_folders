using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;
using DevExpress.Web.ASPxEditors;

public partial class Popupcontrol_vessel_name : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack & !Page.IsCallback) { 
            //clear session of vessel names added
            Page.Session["vesselnames"] = null; 
        }
        
        //if names have been added display on information panel
        if (Page.Session["vesselnames"] != null) {
            this.dxlblInfo.Text = Page.Session["vesselnames"].ToString();
            this.dxpnlMsg.ClientVisible = true;
        }

    }

    #region incremental filtering for large combobox datasets
    //14/07/14 DEPRECATED 
    //can't use OnItemsRequestedByFilterCondition and OnItemRequestedByValue on this combo as server-side filtring makes the search case sensitive
    //changing the collation on VesselName column would fix this but the sql query still runs as case sensitive. why?
    //incremental filtering for large datasets on combos
    /// <summary>
    /// incremental filtering and partial loading of name and address book for speed
    /// both ItemsRequestedByFilterCondition and ItemRequestedByValue must be set up for this to work
    /// company name is only available to publiship users
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dxcboVesselID_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
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
    protected void dxcboVesselID_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "VesselID", "VesselName" };
        string[] _sort = { "VesselName" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.VesselTable).Paged(e.BeginIndex + 1, e.EndIndex + 1).Where("VesselName").Like(string.Format("{0}%", _filter));

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "VesselID";
        _combo.TextField = "VesselName";
        _combo.DataBindItems();
    }
    //end incremental filtering of vessel name
    #endregion

    #region form events
    /// <summary>
    /// save and exit
    /// check combo text against vessel table - no duplicates
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnComplete_Click(object sender, EventArgs e)
    {
        //make sure the combobox on this form is 'dropdown' not 'dropdownlist' or you will not be able to type in and retain new items
        //the entered text will be lost on postback/callback
        ASPxComboBox _cbo = (ASPxComboBox)this.FindControl("dxcboVesselID");
        string _vesselname = _cbo.Text != null ? _cbo.Text.ToString() : "";

        int _matched = wwi_func.lookup_match("VesselName", "VesselTable", _vesselname);

        //if at least 1 match found do not save and exit
        if (_matched > 0)
        {
            this.dxlblErr.Text = string.Format("{0} {2} of {1} found in the database. {1} has not been added again.", _matched, _vesselname, _matched > 1 ? "duplicates": "duplicate");
            this.dxpnlErr.ClientVisible = true;
        }
        else
        { 
            //save 
            insert_vessel_name(_vesselname); 
            //clear session
            Page.Session["vesselname"] = null; 
            //close popup
            this.ClientScript.RegisterStartupScript(GetType(), "EXT_KEY", "window.parent.ppcDefault.HideWindow(window.parent.ppcDefault.GetWindowByName('vessel_name'));", true);
        }
    }
    //end btncomplete click
    /// <summary>
    /// add vessel and clear form for new input
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnAdd_Click(object sender, EventArgs e)
    {
        //make sure the combobox on this form is 'dropdown' not 'dropdownlist' or you will not be able to type in and retain new items
        //the entered text will be lost on postback/callback
        ASPxComboBox _cbo = (ASPxComboBox)this.FindControl("dxcboVesselID");
        string _vesselname = _cbo.Value != null ? _cbo.Text.ToString() : "";

        int _matched = wwi_func.lookup_match("VesselName", "VesselTable", _vesselname);

        //if at least 1 match found do not save and exit
        if (_matched > 0)
        {
            this.dxlblErr.Text = string.Format("{0} vessel(s) named {1} found in the database. {1} has not been saved.", _matched, _vesselname);
            this.dxpnlErr.ClientVisible = true;
        }
        else
        {
            //save 
            insert_vessel_name(_vesselname);
            //add vessel name to current session
            Page.Session["vesselname"] += Environment.NewLine + _vesselname + " saved"; 
            //clear combo
            _cbo.SelectedIndex = -1;
            _cbo.Text = null;
        }
    }
    //end btnadd click
    #endregion

    #region crud events
    protected void insert_vessel_name(string vesselname)
    {
        try
        {
            VesselTable _tbl = new VesselTable();
            _tbl.VesselName = vesselname;
            _tbl.Save(); 
        }
        catch(Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.Visible = true;
        }
    }
    #endregion
}
