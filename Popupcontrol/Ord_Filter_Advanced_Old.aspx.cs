using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;


public partial class Ord_Filter_Advanced_Old : System.Web.UI.Page
{
    DataTable _df = new DataTable();
 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Session["user"] == null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "LOG_KEY", "window.parent.popWindows.ShowWindow(window.parent.popWindows.GetWindowByName('loginform'));", true);
            //ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", "window.parent.closefilterWindow();", true);
        }
  
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           get_filters(); //07/10/2010 only required if we use aspxfiltercontrol load available filter fields
        }
    }

    protected void get_filters()
    {

        DbFilterFieldCollection _filter = new DbFilterFieldCollection().Load();
        DataTable _dt = (DataTable)_filter.ToDataTable();

        for (Int32 ix = 0; ix <= _dt.Rows.Count - 1; ix++)
        {
            DevExpress.Web.ASPxEditors.FilterControlColumn _col = new DevExpress.Web.ASPxEditors.FilterControlColumn();
            _col.DisplayName = _dt.Rows[ix]["filter_caption"].ToString();
            _col.PropertyName = _dt.Rows[ix]["field_name"].ToString();
            _col.ColumnType = get_column_type(_dt.Rows[ix]["column_type"].ToString());
            this.advancedFilter.Columns.Add(_col);
        }
    }

    protected DevExpress.Web.ASPxEditors.FilterControlColumnType get_column_type(String coltype)
    {
        DevExpress.Web.ASPxEditors.FilterControlColumnType _columntype = new DevExpress.Web.ASPxEditors.FilterControlColumnType();

        switch (coltype)
        {
            case "boolean":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Boolean;
                break;
            case "datetime":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.DateTime;
                break;
            case "decimal":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Decimal;
                break;
            case "default":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Default;
                break;
            case "double":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Double;
                break;
            case "integer":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.Integer;
                break;
            case "string":
                _columntype = DevExpress.Web.ASPxEditors.FilterControlColumnType.String;
                break;
        }
        return _columntype;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //string _magic = "\""; //set up quotes correctly in string 
        //this.txtQueryResult.Text = this.advancedFilter.FilterExpression.Replace("#", _magic).ToString(); //this.advancedFilter.FilterExpression.ToString();

        string _fex = this.advancedFilter.GetFilterExpressionForOracle().ToString();
        string _fdesc = this.aspxtxtName.Text.ToString();
        if (string.IsNullOrEmpty(_fdesc)) { _fdesc = "Advanced filter " + DateTime.Today.ToShortDateString(); };
            
        if (_fex != string.Empty)
        {
            _fex = set_string_parser(_fex);

            if (_fex.Contains("#"))
            {
                _fex = set_datetime_parser(_fex);
            }

            Session["savedfilter"] =  _fdesc ;//"n";
            Session["filter"] = _fex;
            this.txtQueryResult.Text = _fdesc;

            //pass search params back to parent
            //ClientScript.RegisterStartupScript(GetType(), "QRY_KEY", "window.parent.lblmsgbox.SetText(txtQuery.GetText());", true);
            //close this window
            //ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", "window.parent.closefilterWindow();", true);
            //force reload of primary grid
            //ClientScript.RegisterStartupScript(GetType(), "DAT_KEY", "window.parent.grdOrder.PerformCallback();", true);
            //all one script
            //one script
            ClientScript.RegisterStartupScript(GetType(), "QRY_KEY", "window.parent.lblmsgbox.SetText(txtQuery.GetText());window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));window.parent.submit_query(2);", true);
        }
        else
        {  
            //just close popup
            //Session["savedfilter"] = null;//"n";
            //Session["filter"] = null;
            Session.Remove("savedfilter");//"n";
            Session.Remove("filter");

            ClientScript.RegisterStartupScript(GetType(), "EXT_KEY", "window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));", true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterexpression"></param>
    /// <returns></returns>
    protected string set_string_parser(string filterexpression)
    {
        string _fex = filterexpression;

        //parse for string comparisons like '%xyz', like '%xyz', like '%xyz%', like 'xyz'
        //replace single quotes with "
        if (_fex.Contains("like"))
        {
            if (_fex.Contains("'%") && !_fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".EndsWith(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else if (!_fex.Contains("'%") && _fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".StartsWith(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else if (_fex.Contains("'%") && _fex.Contains("%'"))
            {
                _fex = _fex.Replace(" like ", ".Contains(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
            else
            {
                _fex = _fex.Replace(" like ", ".Contains(\"").Replace("')", "\"))").Replace("'", "").Replace("%", "");
            }
        }
        else if (_fex.Contains("= '"))
        {
            _fex = _fex.Replace(" = '", " == \"").Replace("'", "\""); 

        }
        
        //boolean operators, linq does not use is, is not
        _fex = _fex.Replace("not", " ! ");
        //
        _fex = _fex.Replace("is", " == ");

        return _fex; 
    }

    /// <summary>
    /// surround datetime conditions with Datetime.Parse() or query string won't work Linq can't implicitly cast
    /// </summary>
    /// <param name="filterexpression"></param>
    protected string set_datetime_parser(string filterexpression)
    {
        string _fex = filterexpression;

        try
        {

            char[] _dlims = { '#' };
            string[] _words = _fex.Split(_dlims);
            Int32 _pos = 0;

            //foreach (string _s in _words)
            _fex = "";
            for (Int32 ix = 0; ix < _words.Length - 1; ix++) //every word except the last one
            {
                string _s = _words[ix];
                if (_pos == 0)
                {
                    _fex += _s + "DateTime.Parse(\"";
                    _pos = 1;
                }
                else
                {
                    _fex += _s + "\")";
                    _pos = 0;
                }
            }
            _fex += _words[_words.Length - 1]; //append last to end of string 
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());  
        }

        return _fex; 

    }
}

