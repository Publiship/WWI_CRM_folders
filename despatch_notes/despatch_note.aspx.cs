using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxGridView;
using SubSonic;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class despatch_note : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!isLoggedIn())
        {
            if (!Page.IsCallback) { Response.Redirect("~/tracking/shipment_tracking_unsigned.aspx", true); }
        }
        else
        {
            if (!Page.IsCallback && !Page.IsPostBack) {

                //not required, we save the despatch note ref before adding items. create a temporary session id we cna use while adding pallets to the despatch note
                //int _tempid = new Select(Aggregate.Max("DespatchId")).From(DAL.Logistics.Tables.DespatchNote).ExecuteScalar<int>() + 1; 
                //    .Where(DAL.Logistics.DespatchNoteItem.DespatchNoteIdColumn) //new Random().Next(1, 10000);  
                //Session["despatchid"] = _tempid;  //(Int32)((UserClass)Page.Session["user"]).UserId + _tempid; 
                string _mode = get_token("mode");
                if (_mode == "Edit" || _mode == "ReadOnly")
                {
                    string _pid = get_token("pid");
                    string _ref = get_token("ref"); 
                    if(_pid != null)
                    {
                        int _id = wwi_func.vint(wwi_security.DecryptString(_pid,"publiship"));
                        set_key_id(_id); 
                        this.dxpgeConsigment.ActiveTabIndex = 1;
                        this.dxlblNoteRef.Text = _ref; //wwi_func.lookup_value("despatch_ref", "despatch_note", "despatch_id", _id);
                    }
                }
                else
                {
                    this.dxpgeConsigment.ActiveTabIndex = 0;
                    this.dxlblNoteRef.Text = "";
                }//end if

            } //end if
        }

        bind_commands(); 
        bind_items_grid();
        this.linqdsContainer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(linqdsContainer_Selecting);

    }

    #region form events and callbacks
    /// <summary>
    /// fired when conatiner is selected from drop down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridContainer_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        string _param = !string.IsNullOrEmpty(e.Parameters) ? e.Parameters.ToString() : "";
   
        if (_param == "getdata")
        {
            Page.Session["containeritemsloaded"] = true;

        }
        

        //rebind grid
        this.dxgridContainer.DataBind();
        
    }

    /// <summary>
    /// save consignment ref
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnSave_Click(object sender, EventArgs e)
    {
        string _ref = this.dxtxtRef.Text.ToString();
        int _noteid = 0;

        if (!string.IsNullOrEmpty(_ref))
        {
            //append to log file
            _noteid = append_note(_ref);
            
            //make sure we have an id
            if (_noteid > 0)
            {
                //store in hiddin field we don't need the variable to be accessible anywhere but on this page
                //Page.Session["currentid"] = _id.ToString();  
                set_key_id(_noteid); 
                //tab to next step add items
                this.dxpgeConsigment.ActiveTabIndex = this.dxpgeConsigment.ActiveTabIndex + 1;
                this.dxlblNoteRef.Text =  _ref;
            }
            else
            {
                this.dxlblErr.Text = "Unable to find log Id";
                this.dxpnlErr.ClientVisible = true;
            }//end id > 0

        }//end ref isnulloremtpy        
    }
    //end btnsave
    protected void dxbtnUpdate_Click(object sender, EventArgs e)
    {
        string _ref = this.dxtxtRefEdit.Text.ToString();
        int _noteid = wwi_func.vint(wwi_security.DecryptString(get_token("noteid").ToString(), "publiship"));

        if (!string.IsNullOrEmpty(_ref))
        {
            //make sure we have an id
            if (_noteid > 0)
            {
                //append to log file
                update_note(_noteid, _ref);
                //go to item input
                this.dxpgeConsigment.ActiveTabIndex = 1;
                this.dxlblNoteRef.Text = _ref;
            }
            else
            {
                this.dxlblErr.Text = "Unable to find log Id";
                this.dxpnlErr.ClientVisible = true;
            }//end id > 0

        }//end ref isnulloremtpy        
    }
    //end btnupdate
    protected void set_key_id(int id)
    {
        if (this.dxhfIds.Contains("pid")) { this.dxhfIds.Set("pid", id); } else { this.dxhfIds.Add("pid", id); }     
        //Session["despatchid"] = id;
        //string _current = wwi_security.EncryptString(id.ToString(), "publiship");  
        //this.dxhfIds.Clear();
        //this.dxhfIds.Add("noteid", _current);
    }

    /// <summary>
    /// when finish button clicked output barcodes to pdf and build asn file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnComplete_Click(object sender, EventArgs e)
    {
        int _pid = wwi_func.vint(get_token("pid"));
        string _txref = this.dxlblNoteRef.Text.ToString();
        string _user = this.Session["user"] != null ? ((UserClass)this.Session["user"]).UserName.ToString() : "";

        //returns error messages if we fail at either step
        string _msg = wwi_asn.create_and_upload_asn(_pid, _user, false);
        _msg += Environment.NewLine + output_barcodes_pdf(_txref, _pid);
        
        if(!string.IsNullOrEmpty(_msg))
        {
            this.dxlblErr.Text = _msg;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    #endregion

    #region crud events
    /// <summary>
    /// create new srr log file
    /// </summary>
    /// <param name="consignmentref">string</param>
    /// <returns></returns>
    protected int append_note(string consignmentref)
    {
        int _id = 0;

        try
        {
            //save 
            DespatchNote _note = new DespatchNote();
            _note.DespatchRef = consignmentref;
            _note.CreatedBy = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
            _note.CreatedDate = DateTime.Now;
            _note.Save();
            //return ident
            _id = wwi_func.vint(_note.GetPrimaryKeyValue().ToString());
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }

        return _id;
    }
    //end append srr log
    protected void update_note(int noteid, string newref)
    {
        string _oldref = this.dxlblNoteRef.Text.ToString();
        if (newref != _oldref)
        {
            try
            {
                //update if ref has changed 
                DespatchNote _note = new DespatchNote(noteid);
                _note.DespatchRef = newref;
                _note.CreatedBy = Page.Session["user"] != null ? (string)((UserClass)Page.Session["user"]).UserName : "";
                _note.CreatedDate = DateTime.Now;
                _note.Save();

            }
            catch (Exception ex)
            {
                this.dxlblErr.Text = ex.Message.ToString();
                this.dxpnlErr.ClientVisible = true;
            }//end try catch block

        }//endif
    }
    //end update
    /// <summary>
    /// append item details to items
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdItemDetails_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            string _noteid = wwi_security.DecryptString(get_token("noteid").ToString(), "publiship");
            if (!string.IsNullOrEmpty(_noteid))
            {
                DespatchNoteItem _item = new DespatchNoteItem();
                _item.DespatchNoteId = wwi_func.vint(_noteid);
                //roughly the order they are used in asn file
                _item.PublishipRef = e.NewValues["publiship_ref"] != null ? e.NewValues["publiship_ref"].ToString() : "";
                //roughly the order they are used in asn file
                _item.Isbn = e.NewValues["isbn"] != null ? e.NewValues["isbn"].ToString() : "";
                //itemdescription
                _item.Title = e.NewValues["title"] != null ? e.NewValues["title"].ToString() : "";
                //unit measurements
                _item.Height = e.NewValues["height"] != null ? wwi_func.vdecimal(e.NewValues["height"].ToString()) : 0;
                _item.Width = e.NewValues["width"] != null ? wwi_func.vdecimal(e.NewValues["width"].ToString()) : 0;
                _item.Depth = e.NewValues["depth"] != null ? wwi_func.vdecimal(e.NewValues["depth"].ToString()) : 0;
                _item.UnitNetWeight = e.NewValues["unit_net_weight"] != null ? wwi_func.vdecimal(e.NewValues["unit_net_weight"].ToString()) : 0;
                //end itemdescription
                _item.Impression = e.NewValues["impression"] != null ? e.NewValues["impression"].ToString() : "";
                _item.TotalQty = e.NewValues["total_qty"] != null ? wwi_func.vint(e.NewValues["total_qty"].ToString()) : 0;
                //referencecoded buyers order ref - pearson's ref
                _item.BuyersOrderNumber = e.NewValues["buyers_order_number"] != null ? e.NewValues["buyers_order_number"].ToString() : "";    
                //printers order ref
                _item.PrintersJobNumber = e.NewValues["printers_job_number"] != null ? e.NewValues["printers_job_number"].ToString() : "";
                //end referencecoded
                //palletdetail
                _item.FullPallets = e.NewValues["full_pallets"] != null ? wwi_func.vint(e.NewValues["full_pallets"].ToString()) : 0;
                _item.UnitsFull = e.NewValues["units_full"] != null ? wwi_func.vint(e.NewValues["units_full"].ToString()) : 0;
                _item.PartPallets = e.NewValues["part_pallets"] != null ? wwi_func.vint(e.NewValues["part_pallets"].ToString()) : 0;
                _item.UnitsPart = e.NewValues["units_part"] != null ? wwi_func.vint(e.NewValues["units_part"].ToString()) : 0;
                //carton detail, we can derive books per pallet as cartoncount * unitspercarton
                _item.ParcelCount = e.NewValues["parcel_count"] != null ? wwi_func.vint(e.NewValues["parcel_count"].ToString()) : 0;
                _item.UnitsPerParcel = e.NewValues["units_per_parcel"] != null ? wwi_func.vint(e.NewValues["units_per_parcel"].ToString()) : 0;
                _item.ParcelHeight = e.NewValues["parcel_height"] != null ? wwi_func.vdecimal(e.NewValues["parcel_height"].ToString()) : 0;
                _item.ParcelWidth = e.NewValues["parcel_width"] != null ? wwi_func.vdecimal(e.NewValues["parcel_width"].ToString()) : 0;
                _item.ParcelDepth = e.NewValues["parcel_depth"] != null ? wwi_func.vdecimal(e.NewValues["parcel_depth"].ToString()) : 0;
                _item.ParcelUnitgrossweight = e.NewValues["parcel_unitgrossweight"] != null ? wwi_func.vdecimal(e.NewValues["parcel_unitgrossweight"].ToString()) : 0;
                _item.ParcelsPerLayer = e.NewValues["parcels_per_layer"] != null ? wwi_func.vint(e.NewValues["parcels_per_layer"].ToString()) : 0;
                _item.OddsCount = e.NewValues["odds_count"] != null ? wwi_func.vint(e.NewValues["odds_count"].ToString()) : 0;
                //end palletdetail
                _item.Save(); 
                //return identif
                int _id = wwi_func.vint(_item.GetPrimaryKeyValue().ToString());
                //auto-generate pallet iddentifiers (sscc codes) for each pallet
                append_pallet_ids(_id, _item.FullPallets, _item.PartPallets, _item.TotalQty, _item.PublishipRef); 
            }
            else
            {
                this.dxlblErr.Text = "Unable to find log Id";
                this.dxpnlErr.ClientVisible = true;
            }//end if id 
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            //bind_data_grid(); 
        }
    }
    //end row inserting
    /// <summary>
    /// row update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdItemDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            //get row id
            string _itemid = e.Keys["item_id"].ToString();
            //get log id
            //string _srrlogid = wwi_security.DecryptString(get_token("srrid").ToString(), "publiship");

            if (!string.IsNullOrEmpty(_itemid))
            {
                int _nitemid = wwi_func.vint(_itemid);

                DespatchNoteItem _item = new DespatchNoteItem(_nitemid);
                //_item.SrrLogId = wwi_func.vint(_srrlogid);
                _item.PublishipRef = e.NewValues["publiship_ref"] != null ? e.NewValues["publiship_ref"].ToString() : ""; 
                //roughly the order they are used in asn file
                _item.Isbn = e.NewValues["isbn"] != null ? e.NewValues["isbn"].ToString() : "";
                //itemdescription
                _item.Title = e.NewValues["title"] != null ? e.NewValues["title"].ToString() : "";
                //unit measurements
                _item.Height = e.NewValues["height"] != null ? wwi_func.vdecimal(e.NewValues["height"].ToString()) : 0;
                _item.Width = e.NewValues["width"] != null ? wwi_func.vdecimal(e.NewValues["width"].ToString()) : 0;
                _item.Depth = e.NewValues["depth"] != null ? wwi_func.vdecimal(e.NewValues["depth"].ToString()) : 0;
                _item.UnitNetWeight = e.NewValues["unit_net_weight"] != null ? wwi_func.vdecimal(e.NewValues["unit_net_weight"].ToString()) : 0;
                //end itemdescription
                _item.Impression = e.NewValues["impression"] != null ? e.NewValues["impression"].ToString() : "";
                _item.TotalQty = e.NewValues["total_qty"] != null ? wwi_func.vint(e.NewValues["total_qty"].ToString()) : 0;
                //rerencdecoded buyers order ref - pearson's ref
                _item.BuyersOrderNumber = e.NewValues["buyers_order_number"] != null ? e.NewValues["buyers_order_number"].ToString() : "";
                //printers order ref
                _item.PrintersJobNumber = e.NewValues["printers_job_number"] != null ? e.NewValues["printers_job_number"].ToString() : "";
                //end referencecoded
                //palletdetail
                _item.FullPallets = e.NewValues["full_pallets"] != null ? wwi_func.vint(e.NewValues["full_pallets"].ToString()) : 0;
                _item.UnitsFull = e.NewValues["units_full"] != null ? wwi_func.vint(e.NewValues["units_full"].ToString()) : 0;
                _item.PartPallets = e.NewValues["part_pallets"] != null ? wwi_func.vint(e.NewValues["part_pallets"].ToString()) : 0;
                _item.UnitsPart = e.NewValues["units_part"] != null ? wwi_func.vint(e.NewValues["units_part"].ToString()) : 0;
                //carton detail, we can derive books per pallet as cartoncount * unitspercarton
                _item.ParcelCount = e.NewValues["parcel_count"] != null ? wwi_func.vint(e.NewValues["parcel_count"].ToString()) : 0;
                _item.UnitsPerParcel = e.NewValues["units_per_parcel"] != null ? wwi_func.vint(e.NewValues["units_per_parcel"].ToString()) : 0;
                _item.ParcelHeight = e.NewValues["parcel_height"] != null ? wwi_func.vdecimal(e.NewValues["parcel_height"].ToString()) : 0;
                _item.ParcelWidth = e.NewValues["parcel_width"] != null ? wwi_func.vdecimal(e.NewValues["parcel_width"].ToString()) : 0;
                _item.ParcelDepth = e.NewValues["parcel_depth"] != null ? wwi_func.vdecimal(e.NewValues["parcel_depth"].ToString()) : 0;
                _item.ParcelUnitgrossweight = e.NewValues["parcel_unitgrossweight"] != null ? wwi_func.vdecimal(e.NewValues["parcel_unitgrossweight"].ToString()) : 0;
                _item.ParcelsPerLayer = e.NewValues["parcels_per_layer"] != null ? wwi_func.vint(e.NewValues["parcels_per_layer"].ToString()) : 0;
                _item.OddsCount = e.NewValues["odds_count"] != null ? wwi_func.vint(e.NewValues["odds_count"].ToString()) : 0;
                //end palletdetail
                _item.Save();
                //repair pallet id list
                delete_pallet_ids(_nitemid);
                append_pallet_ids(_nitemid, _item.FullPallets, _item.PartPallets, _item.TotalQty, _item.PublishipRef); 
            }
            else
            {
                this.dxlblErr.Text = "Unable to find item Id";
                this.dxpnlErr.ClientVisible = true;
            }//end if id 
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_items_grid(); 
        }
    }
    //end update
    /// <summary>
    /// delete item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgrdItemDetails_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView _grd = (ASPxGridView)sender;

        try
        {
            //get row id
            string _itemid = e.Keys["item_id"].ToString();

            if (!string.IsNullOrEmpty(_itemid))
            {
                int _nitemid = wwi_func.vint(_itemid);
                DespatchNoteItemController _item = new DespatchNoteItemController();
                _item.Delete(_nitemid);
                delete_pallet_ids(_nitemid);  
            }
            else
            {
                this.dxlblErr.Text = "Unable to find item Id";
                this.dxpnlErr.ClientVisible = true;
            }//end if id 
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }
        finally
        {
            //MUST call this after insert or error: Specified method is not supported
            e.Cancel = true;
            _grd.CancelEdit();
            bind_items_grid(); 
        }
    }
    /// <summary>
    /// delete palet ids when title is updated or removed
    /// </summary>
    /// <param name="itemid"></param>
    protected void delete_pallet_ids(int itemid)
    {
        try
        {
            if (itemid > 0)
            {
                //remove ipallets
                SqlQuery _sql = new Delete().From(DAL.Logistics.Tables.DespatchNotePalletId).
                        Where(DAL.Logistics.DespatchNotePalletId.DespatchItemIdColumn).IsEqualTo(itemid);

                int _del = _sql.Execute();

            }//end if itemid > 0
        }
        catch (Exception ex)
        {
            string _ex = ex.Message.ToString();
            this.dxlblErr.Text = _ex;
            this.dxpnlErr.ClientVisible = true;
        }
    }
    /// <summary>
    /// generated automatically when a  title is saved/updated
    /// </summary>
    /// <param name="palletcount"></param>
    protected void append_pallet_ids(int itemid, int? full, int? part,  int? totalqty, string publishipref)
    {
        const int _max = 17; //base length sscc pallet id 17 digits + 1 for checksum
        const char _chr = '0'; //left padding
        int? _palletcount = full + part;

        for (int _ix = 1; _ix <= _palletcount; _ix++)
        {
            //base for sscc is 17 digits plus 1 digit checksum
            //use total quantity + ix t give an incremetnal numner 0 seperator + publiship ref 
            string _sscc = string.Format("{0}0{1}", (totalqty + _ix).ToString(), publishipref);
            //leading 0's to make it up to 17 characters
            _sscc = _sscc.PadLeft(_max, _chr);
            //append checksum
            _sscc += wwi_func.sscc_checkdigit(_sscc);
            //save to pallet id's
            DespatchNotePalletId _id = new DespatchNotePalletId();
            _id.DespatchItemId = itemid; //link to srr item
            _id.Sscc = _sscc;
            _id.PalletType = _ix <= full ? "Full" : "Part";
            _id.Save();
        }
        //end for 
    }
    //end pallet ids
    #endregion

    #region grid databinding
    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linqdsContainer_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        ParameterCollection _params = new ParameterCollection();
        string _query = "";
        //****************
        //get starting ETS to match container drill down for deliveries from xml file (which makes it easy to change if necessary)
        DateTime? _ets = wwi_func.vdatetime(wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "pearsonstartETS", "value"));

        //using exworks
        //DateTime? _exworks = wwi_func.vdatetime(wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "startExWorks", "value"));
        //****************
        //get container id
        //26/02/15 we aren't passing containerid any more just use containernumber
        string _containerno = this.dxcboContainer.Text  != null ? this.dxcboContainer.Text.ToString(): "";

        if (Page.Session["containeritemsloaded"] != null)
        {
            //09/04/15 don't need to add as a param, using a new linq query with containernumber included in it
            //if(!string.IsNullOrEmpty(_containerno))
            //{
            //    _params.Add("ContainerNumber", "\"" + _containerno + "\""); 
            //}      
            //***************
            string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
            //09.04.2015 Paul Edwards check which clients are are visible for this company
            IList<string> _clientids = null;
            _clientids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/clientids/clientid/value");
            if (_clientids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _clients = "(CompanyID ==" + string.Join(" OR CompanyID ==", _clientids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _clients);

            }
            //21.10.2014 Paul Edwards for delivery tracking check which DeliveryID's are visible for this company
            IList<string> _deliveryids = null;
            _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
            if (_deliveryids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _deliveries = "(DeliveryAddress==" + string.Join(" OR DeliveryAddress==", _deliveryids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _deliveries); //select for this company off list

            }
            //****************

            //now rebuild query with additional parameters AFTER page is loaded
            string _f = "";
            if (_params.Count > 0)
            {
                foreach (Parameter p in _params)
                {
                    string _pname = p.Name.ToString();
                    string _op = "AND";
                    string[] _check = _pname.Split("_".ToCharArray());
                    _pname = _check[0].ToString();
                    _op = _check.Length > 1 ? _check[1].ToString() : _op;

                    string _a = _f != "" ? " " + _op + " " : "";
                    _f += _pname != "NULL" ? _a + "(" + _pname + "==" + p.DefaultValue.ToString() + ")" : _a + "(" + p.DefaultValue.ToString() + ")";

                }

                if (_query != "") { _query = _f + " AND " + _query; } else { _query = _f; }
            }//end params

        }//end if

        //important! need a key id or error=key expression is undefined
        e.KeyExpression = "OrderIx";//"SubDeliveryID"; //"OrderIx"; //"OrderID"; //a key expression is required 
        if (!string.IsNullOrEmpty(_query))
        {
            //make sure using System.Linq.Dynamic; and using System.Linq.Expressions or can't use a query string;
            var _nquery = new linq.linq_container_contentsDataContext().view_container_contents(_ets, _containerno).Where(_query);
            e.QueryableSource = _nquery;

        }
        else if (!string.IsNullOrEmpty(_containerno))
        {
            var _nquery = new linq.linq_container_contentsDataContext().view_container_contents(_ets, _containerno).Where(c => c.ContainerNumber != null);
            e.QueryableSource = _nquery;
        }
        else //default to display nothing until page is loaded 
        {
            var _nquery = new linq.linq_container_contentsDataContext().view_container_contents(_ets, _containerno).Where(c => c.OrderNumber == -1); //c => c.CompanyID == 7
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }


    }

    /// <summary>
    /// grid binding
    /// </summary>
    protected void bind_items_grid()
    {
        try
        {
            //get current asn log id
            int _despatchid = wwi_func.vint(get_token("pid")); //Session["despatchid"] != null ? wwi_func.vint(Session["despatchid"].ToString()) : 0;  
            int _uid = (Int32)((UserClass)Page.Session["user"]).UserId;

            if (_despatchid > 0 && _uid > 0) {

                //is it worth saving to db4o?
                //List<DespatchNoteItem>  _l = db4o_despatch.SelectById(_despatchid);   
                DataTable _dt = new Select().From(DAL.Logistics.Tables.DespatchNoteItem)
                    .Where(DAL.Logistics.DespatchNoteItem.DespatchNoteIdColumn).IsEqualTo(_despatchid).ExecuteDataSet().Tables[0];
        
                this.dxgrdItemDetails.KeyFieldName = "item_id"; //use ItemId if db4o
                this.dxgrdItemDetails.DataSource = _dt;
                this.dxgrdItemDetails.DataBind(); 
            }
        }
        catch (Exception ex)
        {
            this.dxlblErr.Text = ex.Message.ToString();
            this.dxpnlErr.ClientVisible = true;
        }

    }
    //end bind data grid
    /// <summary>
    /// databiding for child grid showing pallet identifiers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgridPalletIds_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView _detail = (ASPxGridView)sender;
        //11/11/2010 can't use the ordernumber as keyfield on grid cause it might not be unique
        //have to get row value
        //Int32 _ordernumber = (Int32)_detail.GetMasterRowKeyValue();  //(sender as ASPxGridView).GetMasterRowKeyValue();
        String[] _keys = { "item_id" };
        int _itemid = (int)_detail.GetMasterRowFieldValues(_keys);

        if (_itemid > 0)
        {
            //datareader much faster than bulding a strongly typed collection
            string[] _cols = { "pallet_id", "sscc", "pallet_type" };
            DataTable _dt = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.DespatchNotePalletId).
                   Where(DAL.Logistics.DespatchNotePalletId.DespatchItemIdColumn).IsEqualTo(_itemid).ExecuteDataSet().Tables[0];

            _detail.DataSource = _dt; //_sb.DefaultView;
            //_detail.DataBind(); //don't call this or details view will not work
        }

    }
    #endregion

    #region command menu databinding
    /// <summary>
    /// command menu for courier details we only need a New option which we can then call through javascript
    /// so the courier xml item deos not contain a navigate url
    /// </summary>
    protected void bind_commands()
    {
        string _path = AppDomain.CurrentDomain.BaseDirectory;
        _path += "xml\\consignment_commands.xml";

        XmlDataSource _xml = new XmlDataSource();
        _xml.DataFile = _path;
        _xml.XPath = "//item[@Filter='Items']"; //you need xpath or tab will not databind!
        _xml.DataBind();
        //Run time population of GridViewDataComboBoxColumn column with data

        //DevExpress.Web.ASPxMenu.ASPxMenu _mnu = (DevExpress.Web.ASPxMenu.ASPxMenu)this.FindControl("dxmnuCommand");
        //if (_mnu != null)
        //{
        //    _mnu.DataSource = _xml;
        //    _mnu.DataBind();
        //}
        //this.dxmnuCommand.DataSource = _xml;
        //this.dxmnuCommand.DataBind();

    }

    protected void dxmnuCommand_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
        {
            string _page = System.IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);//e.g. "BOL_Edit";
            string _id = get_token("pno");
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl)) { e.Item.NavigateUrl = String.Format(e.Item.NavigateUrl, _page, _id); }
        }
    }
    #endregion

    #region document output
    /// <summary>
    /// output barcodes for asn
    /// </summary>
    /// <param name="consignmentid"></param>
    /// <returns></returns>
    public static string output_barcodes_pdf(string consignmentref, int consignmentid)
    {
        //for a custom httphandler make sure it's referenced in web.config in httpHandlers
        //<add verb="GET" path="*barcode.gif" type="barcode_handler" validate ="false" />
        //
        string _msg = ""; //message returned blank if successful else error message
        float _ypos = 0; //horizontal position
        int _ppg = 4; //items per page
        Document _doc = new Document(); //itextsharp document 

        try
        {
            //get all pallet identifiers for this consignment
            string[] _cols = { "despatch_note_id", "publiship_ref", "title", "sscc" };
            DataTable _dt = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.DespatchNotePalletId).
                    LeftOuterJoin(DAL.Logistics.DespatchNoteItem.ItemIdColumn, DAL.Logistics.DespatchNotePalletId.DespatchItemIdColumn).
                    Where(DAL.Logistics.DespatchNoteItem.DespatchNoteIdColumn).IsEqualTo(consignmentid).ExecuteDataSet().Tables[0];

            //open memeroary stream
            System.IO.MemoryStream _mem = new System.IO.MemoryStream();
            //create instance of itextsharp pdf writer
            PdfWriter _pdf = PdfWriter.GetInstance(_doc, _mem);
            _doc.Open();
            _pdf.Open();

            //build table
            //PdfPTable _tbl = new PdfPTable(1);
            //_tbl.DefaultCell.Border  = iTextSharp.text.Rectangle.NO_BORDER;
            //hide all borders except bottom which acts as line seperator
            //_tbl.DefaultCell.BorderWidthTop = 0;
            //_tbl.DefaultCell.BorderWidthLeft = 0;
            //_tbl.DefaultCell.BorderWidthRight = 0;
            //_tbl.DefaultCell.BorderColorBottom  = BaseColor.LIGHT_GRAY;
            //_tbl.DefaultCell.PaddingTop = 10;
            //_tbl.DefaultCell.PaddingBottom = 5;
            //make sure to force split if we reun over page
            //_tbl.SplitRows = true;

            for (int _ix = 0; _ix < _dt.Rows.Count; _ix++)
            {
                //generate barcodes
                //int _wd = 120; //context.Request["wd"] != null ? wwi_func.vint(context.Request["wd"].ToString()) : 120; //width
                //int _ht = 30; //context.Request["ht"] != null ? wwi_func.vint(context.Request["ht"].ToString()) : 30; //height
                string _code = _dt.Rows[_ix]["sscc"] != null ? _dt.Rows[_ix]["sscc"].ToString() : "";
                string _title = _dt.Rows[_ix]["title"] != null ? _dt.Rows[_ix]["title"].ToString() : "";
                string _ref = _dt.Rows[_ix]["publiship_ref"] != null ? _dt.Rows[_ix]["publiship_ref"].ToString() : "";

                if (!string.IsNullOrEmpty(_code))
                {
                    Barcode128 _bc = new Barcode128();
                    _bc.CodeType = Barcode.CODE128;
                    _bc.ChecksumText = true;
                    _bc.GenerateChecksum = true;
                    _bc.Code = _code;

                    //add barcode to to pdf
                    PdfContentByte _cb = _pdf.DirectContent;
                    iTextSharp.text.Image _img = _bc.CreateImageWithBarcode(_cb, BaseColor.BLACK, BaseColor.BLACK);

                    PdfPTable _tbl = new PdfPTable(1);
                    _tbl.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    _tbl.DefaultCell.PaddingTop = 10;
                    _tbl.SplitLate = true;
                    _tbl.AddCell(_ref + " " + _title);
                    _tbl.AddCell(_img);
                    //don't need to add to doc if we use writeselectedrows
                    _doc.Add(_tbl);
                    //_ypos = _pdf.GetVerticalPosition(false);
                    //_tbl.WriteSelectedRows(0, _tbl.Rows.Count, _doc.LeftMargin, _ypos, _pdf.DirectContent);
                    //4 bacodes per page?
                    bool _new = (_ix + 1) % _ppg == 0 ? true : false;
                    if (_new)
                    {
                        _doc.NewPage();
                    }
                    else
                    {
                        // seperator between items
                        _pdf.DirectContent.SetColorStroke(BaseColor.LIGHT_GRAY);
                        _ypos = _pdf.GetVerticalPosition(false);
                        _pdf.DirectContent.MoveTo(_doc.LeftMargin, _ypos);
                        _pdf.DirectContent.LineTo(_doc.PageSize.Width - _doc.RightMargin, _ypos);
                        _pdf.DirectContent.Stroke();
                    }//end if new

                }//end if code not empty
            }

            //pushes to output stream
            _doc.Close();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + consignmentref + ".pdf");
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.BinaryWrite(_mem.ToArray());
        }
        catch (DocumentException dex)
        {
            _msg = dex.Message.ToString();
            //throw (dex);
            //this.dxlblerr.Text = dex.Message.ToString();
            //this.dxpageorder.ActiveTabIndex = 4; //error page
        }
        catch (IOException ioex)
        {
            _msg = ioex.Message.ToString();
            //throw (ioex);
            //this.dxlblerr.Text = ioex.Message.ToString();
            //this.dxpageorder.ActiveTabIndex = 4; //error page
        }
        finally
        {
            _doc.Close();
        }

        return _msg;
    }
    //end output barcodes
    #endregion

    #region functions
    /// return string value from named token 
    /// checking hidden fields first then cookies if value not found
    /// </summary>
    /// <param name="namedtoken">name of token</param>
    /// <returns></returns>
    protected string get_token(string namedtoken)
    {
        string _value = this.dxhfIds.Contains(namedtoken) ? this.dxhfIds.Get(namedtoken).ToString() : null;

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

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }
    #endregion

    #region incremental filtering
    //container search
    protected void dxcboContainer_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        Int32 _id = 0;
        if (e.Value != null) { _id = wwi_func.vint(e.Value.ToString()); }

        //use datareaders - much faster than loading into collections
        string[] _cols = { "ContainerSubID", "ContainerNumber" };
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Views.PearsonRugbyContainerView).WhereExpression("ContainerSubID").IsEqualTo(_id);

        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "ContainerSubID";
        _combo.TextField = "ContainerNumber";
        _combo.DataBindItems();

    }
    protected void dxcboContainer_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxComboBox _combo = (DevExpress.Web.ASPxEditors.ASPxComboBox)source;

        string _filter = !string.IsNullOrEmpty(e.Filter) ? e.Filter : "";

        //use datareaders - much faster than loading into collections
        string[] _cols = { "ContainerSubID", "ContainerNumber" };
        string[] _sort = { "ContainerNumber" };

        //SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Tables.NameAndAddressBook).Paged(e.BeginIndex + 1, e.EndIndex + 1, "CompanyID").WhereExpression("CompanyName").Like(string.Format("%{0}%", e.Filter.ToString()));
        SubSonic.SqlQuery _query = DAL.Logistics.DB.Select(_cols).From(DAL.Logistics.Views.PearsonRugbyContainerView).Paged(e.BeginIndex + 1, e.EndIndex + 1).Where("ContainerNumber").StartsWith(string.Format("{0}%", _filter));


        IDataReader _rd = _query.ExecuteReader();
        _combo.DataSource = _rd;
        _combo.ValueField = "ContainerSubID";
        _combo.TextField = "ContainerNumber";
        _combo.DataBindItems();
    }
    //end container search
    #endregion
    
    protected void dxgrdItemDetails_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //rebind grid
        //bind_items_grid(); 
    }
    //end custom callabck
    
}
