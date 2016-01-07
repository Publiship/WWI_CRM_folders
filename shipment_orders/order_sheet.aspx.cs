using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxEditors;
using SubSonic;

public partial class order_sheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                //we only need the readonly view
                this.fmvorder.ChangeMode(FormViewMode.ReadOnly);
                //ordernumber = 1049040 for testing

                int _orderno = wwi_func.vint(wwi_security.DecryptString(get_token("pno"), "publiship"));
                if (_orderno > 0)
                {
                    bind_order(_orderno);
                    //bind_order(1049040);
                    //bind subreports after order details have been bound
                    bind_sub_reports(_orderno);
                }
                else
                {
                    this.dxlblErr.Text = string.Format("Order # {0} not found", _orderno);
                    this.dxpnlErr.ClientVisible = true;
                }
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                this.dxlblErr.Text = _ex;
                this.dxpnlErr.ClientVisible = true;
            }
            //end catch
        }
    }

    #region form binding
    /// <summary>
    /// get order sheet record as Ienumerable so we can bind it for formview
    /// </summary>
    /// <param name="ordernumber"></param>
    protected void bind_order(int ordernumber)
    {

        try
        {
            //get datacontext
            linq.linq_order_sheet_udfDataContext _linq = new linq.linq_order_sheet_udfDataContext();

            //return iqueryable order by order number
            //IQueryable<order_sheetResult> _order = _linq.order_sheet(1049040);
            //details for 1st order the linq datacontext only returns 1 record by order number
            
            //order details
            IList<linq.order_sheetResult> _o = _linq.order_sheet(ordernumber).ToList<linq.order_sheetResult>();
            this.fmvorder.DataSource = _o;
            this.fmvorder.DataBind();
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            Response.Write(_err);

        }
    }
    //end bind order

    /// <summary>
    /// some conditional formatting on dstabound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmvorder_DataBound(object sender, EventArgs e)
    {
        //If([Palletise]=-1,"To be shipped Pre-palletised","Ship as loose cartons for palletisation at destination")
        ASPxLabel _lbl = this.fmvorder.FindControl("dxlblorderPalletise") as ASPxLabel;
        if (_lbl != null)
        {
            if (_lbl.Text == "True") { _lbl.Text = "To be shipped Pre-palletised"; } else { _lbl.Text = "Ship as loose cartons for palletisation at destination"; }
        }

        //bill of lading style
        _lbl = this.fmvorder.FindControl("dxlblorderExBL") as ASPxLabel;
        if (_lbl != null)
        {
            if (_lbl.Text == "True") { _lbl.Text = "Please issue an Express Bill of Lading"; } else { _lbl.Text = "Negotiable Bill of Lading required"; }
        }

        //date formatting
        string[] _lbls = { "ExWorks", "Ets", "Eta" };
        for (int _ix = 0; _ix < _lbls.Length; _ix++)
        {
            _lbl = this.fmvorder.FindControl("dxlblorder" + _lbls[_ix].ToString()) as ASPxLabel;
            if (_lbl != null)
            {
                _lbl.Text = wwi_func.vdatetime(_lbl.Text.ToString()).ToShortDateString();
            }
        }

        //hot job
        _lbl = this.fmvorder.FindControl("dxlblorderHot") as ASPxLabel;
        if (_lbl.Text != "False") { _lbl.Text = "Hot job"; _lbl.ClientVisible = true; } else { _lbl.ClientVisible = false; }

        //curent date
        _lbl = this.fmvorder.FindControl("dxlblCurrentDate") as ASPxLabel;
        if (_lbl != null) { _lbl.Text = DateTime.Now.ToShortDateString(); }
        
      
    }

    protected void bind_sub_reports(int ordernumber)
    {
        //courier details
        Repeater _r = this.fmvorder.FindControl("rptcouriers") as Repeater;
        if (_r != null)
        {
            linq.linq_courier_detailsDataContext _courier = new linq.linq_courier_detailsDataContext();
            IList<linq.courier_detailsResult> _c = _courier.courier_details(ordernumber).ToList<linq.courier_detailsResult>();
            _r.DataSource = _c;
            _r.DataBind();
        }

        //delivery details
        _r = this.fmvorder.FindControl("rptdeliveries") as Repeater;
        if (_r != null)
        {
            linq.linq_delivery_detailsDataContext _delivery = new linq.linq_delivery_detailsDataContext();
            IList<linq.delivery_detailsResult> _d = _delivery.delivery_details(ordernumber).ToList<linq.delivery_detailsResult>();
            _r.DataSource = _d;
            _r.DataBind();
        }

        //titles
        _r = this.fmvorder.FindControl("rptordertitles") as Repeater;
        if (_r != null)
        {
            //string[] _cols = { "ItemTable.Title", "ItemTable.Copies", "ItemTable.ISBN", "ItemTable.PONumber" };
            IDataReader _t = DAL.Logistics.ItemTable.FetchByParameter("OrderNumber", Comparison.Equals, ordernumber);
            _r.DataSource = _t;         
            _r.DataBind();
        }
    }
    //end sub reports

    /// <summary>
    /// conditional formatting for courier details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptcouriers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //courier_detailsResult _c = (courier_detailsResult)e.Item.DataItem;
            string _original = "";
            ASPxLabel _lbl = (ASPxLabel)e.Item.FindControl("dxlblcroriginal");
            if (_lbl != null) { _original = _lbl.Text.ToString(); }

            //if (_c.Original == 1)
            if (_original == "1")
            {
                //doc type
                ((ASPxLabel)e.Item.FindControl("dxlblcroriginal")).Text = "Originals";
            }
            //else if (_c.Original == 2)
            else if (_original == "2")
            {
                ((ASPxLabel)e.Item.FindControl("dxlblcroriginal")).Text = "Copies";
            }
            else
            {
                ((ASPxLabel)e.Item.FindControl("dxlblcroriginal")).Text = "Send by email";
                //change emailto from courier name to email address
                string _email = "";
                _lbl = (ASPxLabel)e.Item.FindControl("dxlblcremail");
                if (_lbl != null) { _email = _lbl.Text.ToString(); }
                ((ASPxLabel)e.Item.FindControl("dxlblcrmailto")).Text = _email;
                //do not show awbnumber
                ((ASPxLabel)e.Item.FindControl("dxlblcrawbno")).Text = "";
                //do not show docs despatched
                ((ASPxLabel)e.Item.FindControl("dxlblcrdocs")).Text = "";
            }
            //end if original 
        }//end if itemtype
    }

    
    #endregion

    #region formview events
    protected void fmvorder_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        this.fmvorder.ChangeMode(e.NewMode);
    }
    #endregion

    #region pdf output
    protected void pdf_out()
    {

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
        string _value = this.dxhforder.Contains(namedtoken) ? this.dxhforder.Get(namedtoken).ToString() : null;

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
