using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using SubSonic;

public partial class container_contents : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack) //should make sure the last search session is cleared after browser has been closed then reopened
            {
                //21.10.14 not relevant do delivery tracking
                //****************
                //check to see if company filters available MUST be done in init or ListEditItemRequestedByValueEventArgs will pass as null
                //int _cid = Page.Session["user"] != null ? (Int32)((UserClass)Page.Session["user"]).CompanyId : 0;

                //only internal users can clear selection, other users must select something if they have access to the company list
                //if (_cid != -1)
                //{
                //    this.dxcbocompany.Buttons[0].Visible = false; 
                //}

                //if(has_company_filters(_cid)){ 
                //    dxcbocompany_ItemRequestedByValue(this.dxcbocompany, new DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs(-1));
                //    this.dxcbocompany.Enabled = true;
                //}

                //290413 additional filtering to restrict dataset by age (DateOrderCreated)
                //bind_dll_archive();
                //******************
            }


            //21.10.14 not relevant do delivery tracking
            //****************            
            //additional filtering on user/company? custom reports?
            //bind_combos();
            // 
            //if (this.dxcbocompany.Enabled) { this.dxcbocompany.SelectedIndex = 0; } 
            //****************
            //new linq databinding running in server mode
            this.LinqServerModeContainer.Selecting += new EventHandler<DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs>(LinqServerModeContainer_Selecting);

        }
        catch (Exception ex)
        {
            this.dxlblerr1.ClientVisible = true;
            this.dxlblerr1.Visible = true;
            //Response.Write(ex.Message.ToString());  
            this.dxlblerr1.Text +=  ex.Message.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //string _containerno =  Request.QueryString["cno"] != null ? Request.QueryString["cno"].Replace(",", "") : "";
        string _containerno = Request.QueryString["cid"] != null ? wwi_security.DecryptString(Request.QueryString["cid"].Replace(",", ""), "publiship") : "";
        this.dxlblTitle.Text = "Container number : " + _containerno;
    }

    #region grid databinding
    /// <summary>
    /// this code is used with LinqServerModeDataSource_Selecting so we can run in server mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinqServerModeContainer_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
    {
        ParameterCollection _params = new ParameterCollection();
        string _query = "";
        //****************
        //get starting ETS to match container drill down for deliveries from xml file (which makes it easy to change if necessary)
        DateTime? _ets = wwi_func.vdatetime(wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "startETS", "value"));
                   
        //using exworks
        //DateTime? _exworks = wwi_func.vdatetime(wwi_func.lookup_xml_string("xml\\parameters.xml", "name", "startExWorks", "value"));
        //****************
        //get container id
        //26/02/15 we aren't passing containerid any more just use containernumber
        string _containerno = Request.QueryString["cid"] != null ? wwi_security.DecryptString(Request.QueryString["cid"].Replace(",", ""), "publiship") : null;
            
        if (Page.Session["containerpoploaded"] != null)
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
            //check individual contact ID iso 
            IList<string> _deliveryids = null;
            _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
            if (_deliveryids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _deliveries = "(DeliveryAddress==" + string.Join(" OR DeliveryAddress==", _deliveryids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _deliveries); //select for this company off list
           
            }
            else
            {
                _params.Add("DeliveryAddress", _contactid);  
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
        //make sure using System.Linq.Dynamic; and using System.Linq.Expressions or can't use a query string;
        if (!string.IsNullOrEmpty(_query))
        {
            var _nquery = new linq.linq_container_contentsDataContext().view_container_contents(_ets, _containerno).Where(_query);
            e.QueryableSource = _nquery;

        }
        else //default to display nothing until page is loaded 
        {
            //var _nquery = new linq_classesDataContext().view_order_2s.Where(c => c.OrderNumber == -1);
            var _nquery = new linq.linq_container_contentsDataContext().view_container_contents(_ets, _containerno).Where(c => c.OrderNumber == -1); //c => c.CompanyID == 7
            //_count = _nquery.Count();

            e.QueryableSource = _nquery;
        }


    }
    #endregion

    #region grid events
    
    protected void gridContainer_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "getdata")
        {
            Page.Session["containerpoploaded"] = true;
            //batch updating disabled on this form
            //
            //from popupcontrol e.g. Ord_Edit_Pallet.aspx      
            //if (e.Parameters == "batchupdate")
            //{
            //    get_selected_rows(1);
            //}

            //rebind data
            //this.gridOrder.DataSource = get_datatable();

        }
        //rebind grid
        this.gridContainer.DataBind();
    }
    #endregion
}
