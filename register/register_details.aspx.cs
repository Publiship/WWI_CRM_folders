using System;
using System.Data; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;

public partial class register_details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bind_cbo_country();
        }
    }
    protected void dxbtnsend_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.dxcapt1.IsValid)
            {
                //build contact object
                Registration1 _reg = new Registration1();

                _reg.RegDate = DateTime.Now;
                _reg.RegName1 = this.dxtxtname1.Text.ToString();
                _reg.RegName2 = this.dxtxtname2.Text.ToString();
                _reg.RegCompany = this.dxtxtcompany.Text.ToString();
                _reg.RegTel = this.dxtxtphone.Text.ToString();
                _reg.RegEmail = this.dxtxtemail.Text.ToString();
                _reg.RegExtra = this.dxmemoadd.Text.ToString();
                _reg.RegMailing = (bool)this.dxckmailing.Value;
                _reg.RegWhere = this.dxcbowhere.Value.ToString();
                _reg.RegIp = wwi_func.user_RequestingIP();
                _reg.RegCountry = this.dxcbocountry.Text.ToString();
 
                //save!
                _reg.Save(); 

                //successful save notify publiship
                string[] _to = { "discount@publiship.com", "paule@publiship.com" }; //"discount@publiship.com", "paule@publiship.com"
                string _subject = "Registration (discount) " + _reg.RegCompany.ToString(); 

                string _tr = "<tr><td bgcolor=\"#e8edff\" valign=\"middle\" width=\"230px\">" + "{0}" + "</td><td width=\"350px\">" + "{1}" + "</td></tr>";

                string _msg = "<table cellpadding=\"5px\" style=\"border-color: #669\">" +
                                String.Format(_tr, "Name", _reg.RegName1.ToString() + ' ' + _reg.RegName2.ToString()) + 
                                String.Format(_tr, "Company", _reg.RegCompany.ToString()) +
                                String.Format(_tr, "Country", _reg.RegCountry.ToString()) +
                                String.Format(_tr, "Phone", _reg.RegTel.ToString())  +
                                String.Format(_tr, "Email", _reg.RegEmail.ToString()) +
                                String.Format(_tr, "Comments", _reg.RegExtra.ToString()) +
                                "</table>";

                string _emailed = ""; 
                
                _emailed = MailHelper.gen_email(_to, true, _subject, _msg, "");
                //anckowledgement to visitor
                if (!string.IsNullOrEmpty(_reg.RegEmail.ToString()))
                {
                    string[] _ak = { _reg.RegEmail.ToString() };
                    _emailed = MailHelper.gen_email(_ak, true, "Thank you registering with Publiship", "<p>We will be in touch soon to discuss your requirements</p><p>Regards</p><p>The Publiship Team</p>", "");
                }
                //finally confirm to user
                this.pnlmsg2.Visible = true;
                this.pnlmsg1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string _err = ex.Message.ToString();
            this.pnlmsg1.Visible = true;
            this.pnlmsg2.Visible = false;
            this.dxbtnsend.ClientEnabled  = true;
        }
    }
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.publiship.com", true); 
    }

    /// <summary>
    /// bind country names to dropdown from resource pricer_pallet_type. Just add directly to ddl no need to be over
    /// elaborate as resource only holds a few items
    /// </summary>
    protected void bind_cbo_country()
    {
        try
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory;
            _path += "xml\\country_iso.xml";

            // pass _qryFilter to have keyword-filter RSS Feed
            // i.e. _qryFilter = XML -> entries with XML will be returned
            DataSet _ds = new DataSet();
            _ds.ReadXml(_path);
            DataView _dv = _ds.Tables[0].DefaultView;
            //_dv.RowFilter = "ddls ='pallet'";

            this.dxcbocountry.DataSource = _dv;
            this.dxcbocountry.DataBind();
            this.dxcbocountry.Value = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //end bind country

}
