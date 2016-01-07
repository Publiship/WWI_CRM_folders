using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Gauges.Linear;

public partial class usercontrols_weights_shipped : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region databinding
    protected void dxgaugeTotals_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        try
        {
            ParameterCollection _params = new ParameterCollection();
            string _query = "";
            float[] _values = { 0, 0, 0, 0 }; //containers, weight, cube (cbm), pallets

            //***************
            //21.10.2014 Paul Edwards for delivery tracking and container check which DeliveryID's are visible for this company
            //check individual contact ID iso 
            string _contactid = ((UserClass)Page.Session["user"]).UserId.ToString();
            IList<string> _deliveryids = null;
            _deliveryids = wwi_func.array_from_xml("xml\\contact_iso.xml", "contactlist/contact[id='" + _contactid + "']/deliveryids/deliveryid/value");
            if (_deliveryids.Count > 0)
            {
                //don't use sql IN(n) as linq won't parse the statement
                string _deliveries = "(DeliveryAddress==" + string.Join(" OR DeliveryAddress==", _deliveryids.Select(i => i.ToString()).ToArray()) + ")";
                _params.Add("NULL", _deliveries); //select for this company off list

            }
            //****************

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
            }

            //****************
            //get starting ETS and ending ETS for current date
            //convert(datetime,'28/03/2013 10:32',103)
            //if no datetimes seelcted default to 0/01/1900 so no data displayed
            DateTime? _ets = DateTime.Now;
            //****************

            //important! need a key id or error=key expression is undefined
            //e.KeyExpression = "ContainerIdx";

            //10.11/2014 if necessary send to datatable then use datatable.compute method on clientname to summarise without showing
            //breakdown by delivery address
            if (!string.IsNullOrEmpty(_query))
            {
                linq.linq_aggregate_containers_udfDataContext _ct = new linq.linq_aggregate_containers_udfDataContext();
                IEnumerator<linq.aggregate_containers_by_etsResult> _in = _ct.aggregate_containers_by_ets(_ets, _ets).Where(_query).GetEnumerator();
                while (_in.MoveNext())
                {
                    linq.aggregate_containers_by_etsResult _c = _in.Current;
                    _values[0] += (float)_c.ContainerCount;
                    _values[1] += (float)_c.SumWeight;
                    _values[2] += (float)_c.SumCbm;
                    _values[3] += (float)_c.SumPackages;
                }
            }

            //bind aggregates to guage
            for (int _ix = 0; _ix < _values.Length; _ix++) {
                ((LinearGauge)this.dxguageTotals.Gauges[_ix]).Scales[0].Value = _values[_ix];
            }
            //end for
        }
        catch (Exception ex)
        {
            this.dxlblerr1.Text = ex.Message.ToString();
            this.dxlblerr1.Visible = true;
        }
    }
    #endregion
}
