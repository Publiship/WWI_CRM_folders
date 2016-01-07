using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using DAL.Logistics;


public partial class Sys_Login_Remind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //make sure message panels are hidden
        this.pnlmsg1.Visible = false;
        this.pnlmsg2.Visible = false;
        this.btnCancel.Visible = true;
        this.btnSend.Visible = true;
    }

    /// <summary>
    /// 25/03/2011
    /// validate email address and capcha text for security then attempt to email account details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        Boolean _sent = false;

        if (this.dxcaptcha1.IsValid)
        {
            String _mail = wwi_security.EncryptString(this.txtdxmail.Text.ToString(),"publiship");
            String _verified = wwi_security.getuserAccount(_mail);

            if (!String.IsNullOrEmpty(_verified))
            {
                char[] _ch = new char[] { '#' };
                string[] _acc = _verified.Split(_ch);
                MailHelper.MailRecipientClass _to = new MailHelper.MailRecipientClass();

                _to.rEmail = _mail;
                _to.rSubject = "Your Publiship account";
                _to.rMessage = string.Format("<p>Your user name: {0}</p><p>Your password: {1}</p>", _acc);
                _verified = MailHelper.send_mail_message(_to);

                //if no error msg is returned, email has gone ok
                if (String.IsNullOrEmpty(_verified)) { _sent = true; }
            }
        }

        if (!_sent) //error
        {
            this.pnlmsg1.Visible = true;
        }
        else
        {
            //this.lblmsgok.Visible = true;
            this.pnlmsg2.Visible = true;
            this.btnCancel.Visible = false;
            this.btnSend.Visible = false;
        }
    }
    //end button send
}
