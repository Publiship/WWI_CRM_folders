using System;
using System.Data; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;

public partial class Popupcontrol_despatch_note_item : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback) {
            get_item_data();
        } 
    }

    #region crud events
    /// <summary>
    /// get item data
    /// </summary>
    protected void get_item_data()
    {
        int _subdeliveryid = Request.QueryString["ids"] != null ? wwi_func.vint(Request.QueryString["ids"].Replace(",", "").ToString()) : 0;
        string _container = Request.QueryString["ctr"] != null ? Request.QueryString["ctr"].Replace(",", "").ToString() : "";

        if (_subdeliveryid > 0)
        {
            //get data using subdeliveryid so we should only get one row
            string[] _cols = { "Title", "Copies", "ISBN", "CopiesPerCarton", "CartonLength", "CartonDepth", "CartonHeight", 
                                         "TotalCartons", "CartonWeight", "BookLength", "BookDepth", "BookWidth", "BookWeight",
                                            "Impression", "OrderNumber", "CustomersRef",
                                            "CartonsPerFullPallet", "CartonsPerPartPallet", };

            DataRow _dr = DB.Select(_cols).From(DAL.Logistics.Tables.DeliverySubSubTable)
                .LeftOuterJoin(DAL.Logistics.DeliverySubTable.DeliveryIDColumn, DAL.Logistics.DeliverySubSubTable.DeliveryIDColumn)
                .LeftOuterJoin(DAL.Logistics.ItemTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
                .LeftOuterJoin(DAL.Logistics.OrderTable.OrderNumberColumn, DAL.Logistics.DeliverySubTable.OrderNumberColumn)
                .Where(DAL.Logistics.DeliverySubSubTable.SubDeliveryIDColumn).IsEqualTo(_subdeliveryid).ExecuteDataSet().Tables[0].Rows[0];

            this.dxlblContainerNo.Text = string.Format("Container {0}", _container);
            this.dxtxtCopies.Text = _dr.IsNull("Copies") ? "" : _dr["Copies"].ToString();
            this.dxlblOrderNoValue.Text = _dr.IsNull("OrderNumber") ? "" : _dr["OrderNumber"].ToString();
            this.dxlblIsbnValue.Text = _dr.IsNull("ISBN") ? "" : _dr["ISBN"].ToString();
            this.dxlblTitleValue.Text = _dr.IsNull("Title") ? "" : _dr["Title"].ToString();
            this.dxlblImpressionValue.Text = _dr.IsNull("Impression") ? "" : _dr["Impression"].ToString();
            this.dxlblBuyerRefValue.Text = _dr.IsNull("CustomersRef") ? "" : _dr["CustomersRef"].ToString();
            //carton details
            this.dxtxtParcelHeight.Text = _dr.IsNull("CartonHeight") ? "" : _dr["CartonHeight"].ToString();
            this.dxtxtParcelDepth.Text = _dr.IsNull("CartonDepth") ? "" : _dr["CartonDepth"].ToString();
            this.dxtxtParcelWeight.Text = _dr.IsNull("CartonWeight") ? "" : _dr["CartonWeight"].ToString();
            this.dxtxtParcelWidth.Text = _dr.IsNull("CartonLength") ? "" : _dr["CartonLength"].ToString();
            //book details
            this.dxtxtCpc.Text = _dr.IsNull("CopiesPerCarton") ? "" : _dr["CopiesPerCarton"].ToString();
            this.dxtxtHeight.Text = _dr.IsNull("BookLength") ? "" : _dr["BookLength"].ToString();
            this.dxtxtWidth.Text = _dr.IsNull("BookWidth") ? "" : _dr["BookWidth"].ToString();
            this.dxtxtDepth.Text = _dr.IsNull("BookDepth") ? "" : _dr["BookDepth"].ToString();
            this.dxtxtWeight.Text = _dr.IsNull("BookWeight") ? "" : _dr["BookWeight"].ToString();
        }
        //end if
    }
    //end get item data

    /// <summary>
    /// append to despatch item table
    /// </summary>
    protected void save_item()
    {
        int _despatchnoteId = Request.QueryString["idn"] != null ? wwi_func.vint(Request.QueryString["idn"].Replace(",","").ToString()) : 0;

        if (_despatchnoteId > 0)
        {
            DespatchNoteItem _item = new DespatchNoteItem();
            _item.DespatchNoteId = _despatchnoteId;
            _item.PublishipRef = this.dxlblOrderNoValue.Text.ToString();
            _item.Isbn = this.dxlblIsbnValue.Text.ToString();
            _item.Title = this.dxlblTitleValue.Text.ToString();
            _item.Impression = this.dxlblImpressionValue.Text.ToString();
            _item.BuyersOrderNumber = this.dxlblBuyerRefValue.Text.ToString(); 
            //carton details
            _item.ParcelHeight = this.dxtxtParcelHeight.Text.ToString() != "" ? wwi_func.vdecimal(this.dxtxtParcelHeight.Text.ToString()) : 0;
            _item.ParcelDepth = this.dxtxtParcelDepth.Text.ToString() != "" ? wwi_func.vdecimal(this.dxtxtParcelDepth.Text.ToString()) : 0;
            _item.ParcelUnitgrossweight = this.dxtxtParcelWeight.Text.ToString() != "" ? wwi_func.vdecimal(this.dxtxtParcelWeight.Text.ToString()) : 0;
            _item.ParcelWidth = this.dxtxtParcelWidth.Text.ToString() != "" ? wwi_func.vdecimal(this.dxtxtParcelWidth.Text.ToString()) : 0;
            //book details
            _item.UnitsPerParcel = this.dxtxtCpc.Text != "" ? wwi_func.vint(this.dxtxtCpc.Text.ToString()) : 0;
            _item.Height = this.dxtxtHeight.Text != "" ? wwi_func.vdecimal(this.dxtxtHeight.Text.ToString()) : 0;
            _item.Width = this.dxtxtWidth.Text != "" ? wwi_func.vdecimal(this.dxtxtWidth.Text.ToString()) : 0;
            _item.Depth = this.dxtxtDepth.Text != "" ? wwi_func.vdecimal(this.dxtxtDepth.Text.ToString()) : 0;
            _item.UnitNetWeight = this.dxtxtWeight.Text != "" ? wwi_func.vdecimal(this.dxtxtWeight.Text.ToString()) : 0;   
            //user input
            _item.TotalQty = wwi_func.vint(this.dxtxtCopies.Text.ToString()); //copies?
            _item.FullPallets = wwi_func.vint(this.dxtxtFullPallets.Text.ToString());
            _item.UnitsFull = wwi_func.vint(this.dxtxtUnitsFull.Text.ToString()); ;  //CartonsPerFullPallet
            _item.PartPallets = wwi_func.vint(this.dxtxtPartPallets.Text.ToString());
            _item.UnitsPart = wwi_func.vint(this.dxtxtUnitsPart.Text.ToString());//CartonsPerPartPallet
            _item.ParcelCount = wwi_func.vint(this.dxtxtCartonsAdd.Text.ToString());
            _item.ParcelsPerLayer = wwi_func.vint(this.dxtxtPerLayer.Text.ToString());
            _item.OddsCount = wwi_func.vint(this.dxtxtOdds.Text.ToString());

            _item.Save(); 
        }
        //end if
    }
    //end save item
    #endregion

    #region control events
    /// <summary>
    /// save button clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnAdd_Click(object sender, EventArgs e)
    {
        //append to items
        save_item();
        //close popup on parent form and rebind items grid
        this.ClientScript.RegisterStartupScript(GetType(), "ITM_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('Cartons'));window.parent.update_items('');", true);

    }
    //end add click
    /// <summary>
    /// cancel click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnCancel_Click(object sender, EventArgs e)
    {
        //close popup on parent form 
        this.ClientScript.RegisterStartupScript(GetType(), "ITX_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('Cartons'));", true);

    }
    //end cancel click
    #endregion
    
}
