using System;
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

public partial class quote_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            //this page requires a quote id to be passed to it but is otherwise not secure
            //to allow quick linking into other parts of system
            int _usequote = Request.QueryString["qr"] != null ? wwi_func.vint(Request.QueryString["qr"].ToString()) : 0;
            if (_usequote > 0)
            {
                this.dxhfpriceview.Clear();
                this.dxhfpriceview.Add("qr", _usequote);
                get_quote_details();
                this.dxpagepriceview.ActiveTabIndex = 0;
            }
            else
            {
                //set_error_msg("No quote reference has been found");
                //this.dxpagepriceview.ActiveTabIndex = 4;
            }
        }

    }
    protected void dxbtnclosebrowser_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=\"javascript\">window.close();self.close();</script>");
    }

    protected void dxcbkquotev1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        get_quote_details();
       
    }
    protected void get_quote_details()
    {
        Int32 _quoteid = this.dxhfpriceview.Contains("qr")? (Int32)this.dxhfpriceview.Get("qr"): 0 ;

        if (_quoteid > 0)
        {

            //datareader is faster?
            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select().From(DAL.Pricer.Tables.PriceValue).WhereExpression("quote_id").IsEqualTo(_quoteid);
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            this.rptpricevalue1.DataSource = _dt;
            this.rptpricevalue1.DataBind();
            _rd.Close();

            //
            //bound on form
            //some formatting
            //int _dims = wwi_func.vint( _dt.Rows[0]["in_dimensions"].ToString());
            //
            ////price request summary
            //this.dxlblqot1.Text = _dims == 1 ? "Book size (mm)" : _dims == 2 ? "Carton size" : "Paper size and extent";
            //this.dxlblqot2.Text = _dims == 1 ? "Book weight (gms)" : _dims == 2 ? "Carton weight (kgs)" : "Paper weight (gsm)";
            //this.dxlblquoteh1.Text = _dt.Rows[0]["quote_id"] + ": " + _dt.Rows[0]["book_title"] != null? _dt.Rows[0]["book_title"].ToString() : "";
            //
            //switch (_dims)
            //{
            //    case 1:
            //        {
            //            this.dxlblsize.Text = String.Format("{0} x {1} x {2}", _dt.Rows[0]["in_length"].ToString(), _dt.Rows[0]["in_width"].ToString(), _dt.Rows[0]["in_depth"].ToString());
            //            this.dxlblweight.Text = String.Format("{0}", _dt.Rows[0]["in_weight"].ToString());
            //            break;
            //        }
            //   case 2:
            //        {
            //            this.dxlblsize.Text = String.Format("{0} x {1} x {2}", _dt.Rows[0]["in_length"].ToString(), _dt.Rows[0]["in_width"].ToString(), _dt.Rows[0]["in_depth"].ToString());
            //            this.dxlblweight.Text = String.Format("{0}", _dt.Rows[0]["in_weight"].ToString());
            //            break;
            //        }
            //    case 3:
            //        {
            //            this.dxlblsize.Text = String.Format("{0} x {1} x {2}", _dt.Rows[0]["in_length"].ToString(), _dt.Rows[0]["in_width"].ToString(), _dt.Rows[0]["in_extent"].ToString());
            //            this.dxlblweight.Text = String.Format("{0}", _dt.Rows[0]["in_papergsm"].ToString());
            //            break;
            //       }
            //}
            //
            //
            //this.dxlblfrom.Text = _dt.Rows[0]["origin_name"]!= null? _dt.Rows[0]["origin_name"].ToString(): "";
            //this.dxlblto.Text = _dt.Rows[0]["final_name"] != null ? _dt.Rows[0]["final_name"].ToString() : "";
            //this.dxlblcopies.Text = _dt.Rows[0]["tot_copies"].ToString() + " copies";
            //this.dxlblppc.Text = String.Format("{0} per copy", _dt.Rows[0]["in_currency"].ToString());
            //
            //this.dxlblvia.Text = _dt.Rows[0]["ship_via"] != null ? _dt.Rows[0]["ship_via"].ToString() : "";
            //this.dxlbltype.Text = _dt.Rows[0]["in_pallet"] != null ? _dt.Rows[0]["in_pallet"].ToString() : "";
            //this.dxlblshiploose.Text = _dt.Rows[0]["loose_name"] != null ? _dt.Rows[0]["loose_name"].ToString() : "";
            //this.dxlbllclname.Text = _dt.Rows[0]["lcl_name"] != null ? _dt.Rows[0]["lcl_name"].ToString() : "";
            ////prices
            //this.dxlblv.Text = Math.Round((double)_dt.Rows[0]["lcl_v"], 2).ToString();
            //this.dxlblv20.Text = Math.Round((double)_dt.Rows[0]["lcl_v20"], 2).ToString();
            //this.dxlblv40.Text = Math.Round((double)_dt.Rows[0]["lcl_v40"], 2).ToString();
            //this.dxlblv40hc.Text = Math.Round((double)_dt.Rows[0]["lcl_v40hc"], 2).ToString();
            //this.dxlblvloose.Text = Math.Round((double)_dt.Rows[0]["lcl_vloose"], 2).ToString();
            //this.dxlblvloose20.Text = Math.Round((double)_dt.Rows[0]["lcl_vloose20"], 2).ToString();
            //this.dxlblvloose40.Text = Math.Round((double)_dt.Rows[0]["lcl_vloose40"], 2).ToString();
            //this.dxlblvloose40hc.Text = Math.Round((double)_dt.Rows[0]["lcl_vloose40hc"], 2).ToString();
            //

        }
        else
        {
            //set_error_msg("No quote reference has been found");
            //this.dxpagepriceview.ActiveTabIndex = 4;
        }
    }
    //end get quote details

    /// <summary>
    /// load shipment details on callback
    /// set flag so we don't tr y and reload again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkshipv1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Int32 _quoteid = this.dxhfpriceview.Contains("qr") ? (Int32)this.dxhfpriceview.Get("qr") : 0;
       
        if (_quoteid > 0)
        {
            //var _nquery = new linq_pricerDataContext().shipment_sizes.Where(c => c.quote_id == _quoteid);
            //_detail.DataSource = _nquery;
            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select().From(DAL.Pricer.Tables.ShipmentSize).WhereExpression("quote_id").IsEqualTo(_quoteid);
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            
            this.rptshipment1.DataSource = _dt;
            this.rptshipment1.DataBind(); 
            _rd.Close();
        }
        else
        {
            //set_error_msg("No quote reference has been found");
            //this.dxpagepriceview.ActiveTabIndex = 4;
        }
    }
    //end get shipment 
    
    /// <summary>
    /// load costing pre-palletised
    /// set flag so we don't try and reload again
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcbkcostingprev1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Int32 _quoteid = this.dxhfpriceview.Contains("qr") ? (Int32)this.dxhfpriceview.Get("qr") : 0;

        if (_quoteid > 0)
        {

            string[] _cols = {"costing_summary.quote_id", "costing_summary.summary_type" ,"costing_summary.pre_part"  ,"costing_summary.pre_full",
                                 "costing_summary.pre_thc20" ,"costing_summary.pre_thc40"  ,"costing_summary.pre_thclcl" ,"costing_summary.pre_docs",
                                 "costing_summary.pre_origin" ,"costing_summary.pre_haul20" ,"costing_summary.pre_haul40","costing_summary.freight_lcl",
                                 "costing_summary.freight_20","costing_summary.freight_40", "costing_summary.freight_40hq","costing_summary.on_dest_lcl",
                                 "costing_summary.on_pier_etc","costing_summary.on_dest_20", "costing_summary.on_dest_40" ,"costing_summary.on_docs",
                                 "costing_summary.on_customs","costing_summary.on_part", "costing_summary.on_full" ,"costing_summary.on_haul20",
                                 "costing_summary.on_haul40","costing_summary.on_shunt20", "costing_summary.on_shunt40", "costing_summary.on_pallets",
                                 "costing_summary.on_other", "price_values.tot_copies", "price_values.lcl_name"};

            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select(_cols).From(DAL.Pricer.Tables.CostingSummary).LeftOuterJoin("price_values", "quote_id", "costing_summary", "quote_id").Where("quote_id").IsEqualTo(_quoteid).And("summary_type").IsEqualTo("pre-palletised");
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);

            this.rptcosting1.DataSource = _dt;
            this.rptcosting1.DataBind();
            _rd.Close();
        }
        else
        {
            //set_error_msg("No quote reference has been found");
            //this.dxpagepriceview.ActiveTabIndex = 4;
        }
    }
    //end get costing summary 

    protected void dxcbkcostingloosev1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Int32 _quoteid = this.dxhfpriceview.Contains("qr") ? (Int32)this.dxhfpriceview.Get("qr") : 0;
      
        if (_quoteid > 0)
        {

            string[] _cols = {"costing_summary.quote_id", "costing_summary.summary_type" ,"costing_summary.pre_part"  ,"costing_summary.pre_full",
                                 "costing_summary.pre_thc20" ,"costing_summary.pre_thc40"  ,"costing_summary.pre_thclcl" ,"costing_summary.pre_docs",
                                 "costing_summary.pre_origin" ,"costing_summary.pre_haul20" ,"costing_summary.pre_haul40","costing_summary.freight_lcl",
                                 "costing_summary.freight_20","costing_summary.freight_40", "costing_summary.freight_40hq","costing_summary.on_dest_lcl",
                                 "costing_summary.on_pier_etc","costing_summary.on_dest_20", "costing_summary.on_dest_40" ,"costing_summary.on_docs",
                                 "costing_summary.on_customs","costing_summary.on_part", "costing_summary.on_full" ,"costing_summary.on_haul20",
                                 "costing_summary.on_haul40","costing_summary.on_shunt20", "costing_summary.on_shunt40", "costing_summary.on_pallets",
                                 "costing_summary.on_other", "price_values.tot_copies", "price_values.loose_name"};

            SubSonic.SqlQuery _query = DAL.Pricer.DB.Select(_cols).From(DAL.Pricer.Tables.CostingSummary).LeftOuterJoin("price_values", "quote_id", "costing_summary", "quote_id").Where("quote_id").IsEqualTo(_quoteid).And("summary_type").IsEqualTo("loose");
            IDataReader _rd = _query.ExecuteReader();
            DataTable _dt = new DataTable();
            _dt.Load(_rd);
            
            this.rptcosting2.DataSource = _dt;
            this.rptcosting2.DataBind();
            _rd.Close();
        }
        else
        {
            //set_error_msg("No quote reference has been found");
            //this.dxpagepriceview.ActiveTabIndex = 4;
        }
    }
    //end get costing summary

    protected void set_error_msg(string msg)
    { 
        
    }
    //end set error msg
    
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
    /// returns caption for input dimensions absed on input type
    /// </summary>
    /// <param name="testvalue">input type 0,1,2 (book, carton, paper)</param>
    /// <param name="caption">caption type 0 for size, 1 for weight</param>
    /// <returns>string</returns>
    public string inputDimsTitle(object testvalue, int caption)
    {
        string _result = "";

        if (testvalue == null)
        {
            _result = "";
        }
        else
        {   
            string _dims = testvalue.ToString();
            if (caption == 0)
            {
                _result = _dims == "1" ? "Book size (mm)" : _dims == "2" ? "Carton size" : "Paper size and extent";
            }
            else
            {
                _result = _dims == "1" ? "Book weight (gms)" : _dims == "2" ? "Carton weight (kgs)" : "Paper weight (gsm)";
            }
        }

        return _result; 
    }

    /// <summary>
    /// 090811 DEPRECATED we are using output values
    /// returns input dimensions Length, width, depth as string for book/carton and length, width, extent for paper 
    /// </summary>
    /// <param name="testvalue">0,1,2 (book, carton, paper)</param>
    /// <param name="inLen">input length</param>
    /// <param name="inWid">input width</param>
    /// <param name="inDep">input depth</param>
    /// <param name="inExt">paper size and exent</param>
    /// <returns>string</returns>
    public string inputDimsValue080911(object testvalue, object inLen, object inWid, object inDep, object inExt)
    {
        string _r = "";
        string _f = "{0} x {1} x {2}";

        if (testvalue != null)
        {
            string _dims = testvalue.ToString();
            _r = _dims == "1" ? string.Format(_f, inLen.ToString(), inWid.ToString(), inDep.ToString()) : _dims == "2" ? string.Format(_f, inLen.ToString(), inWid.ToString(), inDep.ToString()) : string.Format(_f, inLen.ToString(), inWid.ToString(), inExt.ToString());
        }

        return _r;
    }
    /// <summary>
    /// returns output dimensions Length, width, depth as string for book/carton and length, width
    /// </summary>
    /// <param name="inLen">out length</param>
    /// <param name="inWid">out width</param>
    /// <param name="inDep">out depth</param>
    /// <returns>string</returns>
    public string inputDimsValue(object outLen, object outWid, object outDep)
    {
        string _r = "";
        string _f = "{0} x {1} x {2}";

        _r = string.Format(_f, outLen.ToString(), outWid.ToString(), outDep.ToString());
        
        return _r;
    }
    /// <summary>
    /// returns weight for book/carton and paper gsm for paper 
    /// </summary>
    /// <param name="testvalue">0,1,2 (book, carton, paper)</param>
    /// <param name="inWgt">input weight</param>
    /// <param name="inGsm">input paper gsm</param>
    /// <returns>string weight</returns>
    public string inputWeightValue(object testvalue, object inWgt, object inGsm)
    {
        string _r = "";
        
        if (testvalue != null)
        {
            string _dims = testvalue.ToString();
            _r = _dims == "1" ? inWgt.ToString() : _dims == "2" ? inWgt.ToString() : inGsm.ToString();
        }

        return _r;
    }
    //end input weight value
    //end formatting functions

    //for testing purposes
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Wbs_Pricer_View.aspx?qr=576");
    }
    //end test button
}
