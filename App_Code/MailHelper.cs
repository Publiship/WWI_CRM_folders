using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;

namespace DAL.Logistics
{
    /// <summary>
    /// Summary description for MailHelper
    /// </summary>
    public class MailHelper
    {
        //class func
        public class MailRecipientClass
        {

            private string _rName;
            private string _rClient;
            private string _rEmail;
            private string _rSubject;
            private string _rMessage;
            private Boolean _rFake;
            private ArrayList _rBcc;
            private ArrayList _rCc;
            //
            public string rName
            {
                get { return _rName; }
                set { _rName = value; }
            }

            public string rClient
            {
                get { return _rClient; }
                set { _rClient = value; }
            }

            public string rEmail
            {
                get { return _rEmail; }
                set { _rEmail = value; }
            }

            public string rMessage
            {
                get { return _rMessage; }
                set { _rMessage = value; }
            }

            public string rSubject
            {
                get { return _rSubject; }
                set { _rSubject = value; }
            }

            public Boolean rFake
            {
                get { return _rFake; }
                set { _rFake = value; }
            }
            
            public ArrayList rCc
            {
                get { return _rCc; }
                set { _rCc = value; }
            }
            public ArrayList rBcc
            {
                get { return _rBcc; }
                set { _rBcc = value; }
            }
        }

        /// <summary>
        /// generate emails
        /// compile table of results based on query/resource
        /// add table to email and send to all recipients
        /// </summary>
        /// <param name="qry">query string or resource key</param>
        /// <param name="iskey">true if using resource key</param>
        /// <param name="updguid">unique guid identifying batch of records to find</param>
        /// <returns></returns>
        public static string gen_email(string qry, Boolean iskey, string updguid, string[] addr, Boolean isbcc, string subj, string _user)
        {

            string _generated = string.Empty;
            string _msg = string.Empty;

            //derive datatable
            DataTable _dt =  wwi_func.get_datatable("cargo_updated", true, updguid);
            ArrayList _to = new ArrayList();
            
            //check to see if we really want to send emails
            if (_user.ToLower() == "paul edwards" )
            {
                string[] _fakes = {  "paule@publiship.com", "pauled2109@hotmail.co.uk"};
                _to.AddRange(_fakes);  
            }
            else if (_user.ToLower() == "dave thompson")
            {
                string[] _fakes = { "paule@publiship.com", "dave@publiship.com" };
                _to.AddRange(_fakes);
            }
            else
            {
                //compile list of recipients
                foreach (string ad in addr)
                {
                    //list of unique addresses for named column
                    ArrayList _unique = wwi_func.get_distinct_value(_dt, ad);
                    //merge to full list
                    if (_unique.Count > 0) { _to.AddRange(_unique); }
                }
            }

            //pass to email if we have a message and some recipients
            if (_to.Count > 0 && _dt.Rows.Count >0)
            {
                //generate message text 
                _msg = wwi_func.get_html_table(_dt);

                MailRecipientClass _mail = new MailRecipientClass();
                _mail.rEmail = _to[0].ToString();//primary recipient

                if (isbcc)
                {
                    _mail.rBcc = _to;
                }
                else
                {
                    _mail.rCc = _to;
                }


                _mail.rSubject = subj;
                _mail.rMessage = _msg;
                _generated = send_mail_message(_mail);
            }

            return _generated;
        }

        /// <summary>
        /// generate emails
        /// does not build table of results
        /// </summary>
        /// <returns></returns>
        public static string gen_email(string[] addr, Boolean isbcc, string subj, string _msg, string _user)
        {

            string _generated = string.Empty;
            ArrayList _to = new ArrayList();

            //check to see if we really want to send emails
            //if (_user.ToLower() == "paul edwards" || _user.ToLower() == "dave thompson")
            if (_user.ToLower() == "paul edwards")
            {
                string[] _fakes = { "paule@publiship.com", "pauled2109@hotmail.co.uk" }; //, "dave@publiship.com" };
                _to.AddRange(_fakes);
            }
            else if (_user.ToLower() == "dave thompson")
            {
                string[] _fakes = { "paule@publiship.com", "dave@publiship.com" };
                _to.AddRange(_fakes);
            }
            else
            {
                //compile list of recipients
                foreach (string ad in addr)
                {
                    _to.Add(ad); 
                }
            }

            //pass to email if we have a message and some recipients
            if (_to.Count > 0)
            {
                MailRecipientClass _mail = new MailRecipientClass();
                _mail.rEmail = _to[0].ToString();//primary recipient

                if (isbcc)
                {
                    _mail.rBcc = _to;
                }
                else
                {
                    _mail.rCc = _to;
                }


                _mail.rSubject = subj;
                _mail.rMessage = _msg;
                _generated = send_mail_message(_mail);
            }

            return _generated;
        }

       
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recipient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        /// there is an alternate version of this in lib class
        public static string send_mail_message(MailRecipientClass mrcTo)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();
            string _result = string.Empty;

            // Set the sender address of the mail message
            //do we just default from web.config?
            //mMailMessage.From = new MailAddress(mrcTo.rEmail);

            // Set the recipient address of the mail message
            if (!String.IsNullOrEmpty(mrcTo.rEmail))
            {
                mMailMessage.To.Add(new MailAddress(mrcTo.rEmail));
                //mMailMessage.To.Add(new MailAddress("dave@publiship.com"));


                if (mrcTo.rBcc!= null && mrcTo.rBcc.Count > 0)
                {
                    foreach (string _bccto in mrcTo.rBcc)
                    {
                        // Set the CC address of the mail message
                        mMailMessage.CC.Add(new MailAddress(_bccto));
                        //mMailMessage.CC.Add(new MailAddress("paule@publiship.com"));
                    }
                }

                // Check if the cc value is null or an empty value
                if (mrcTo.rCc!= null && mrcTo.rCc.Count > 0)
                {
                    foreach (string _rccto in mrcTo.rCc)
                    {
                        // Set the CC address of the mail message
                        mMailMessage.CC.Add(new MailAddress(_rccto));
                        //mMailMessage.CC.Add(new MailAddress("pauled2109@hotmail.co.uk"));
                    }
                }

                // Set the subject of the mail message
                mMailMessage.Subject = mrcTo.rSubject;
                // Set the body of the mail message
                mMailMessage.Body = mrcTo.rMessage;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                try
                {
                    SmtpClient mSmtpClient = new SmtpClient();
                    string ConfigPath = "~\\Web.config";
                    Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(ConfigPath);
                    //send the message
                    MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

                    if (mailSettings != null)
                    {

                        int port = mailSettings.Smtp.Network.Port;
                        string host = mailSettings.Smtp.Network.Host;
                        string password = mailSettings.Smtp.Network.Password;
                        string username = mailSettings.Smtp.Network.UserName;

                        mSmtpClient.Port = Convert.ToInt32(port);
                        mSmtpClient.ServicePoint.MaxIdleTime = 1; //without this the connection is idle too long and not terminated, times out at the server and gives sequencing errors

                        if (username != null && username != "")
                        {
                            //to authenticate we set the username and password properites on the SmtpClient
                            mSmtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                        }
                        // Send the mail message
                        //*****************
                        mSmtpClient.Send(mMailMessage);
                        //*****************
                    }

                }
                catch (SmtpException ex)
                {
                    //A problem occurred when sending the email message
                    _result = ex.ToString();
                }
            }
            return _result;
        }
        
        // **********************************
        // *  Replace tags in email content
        // *********************************

        public static string ReplaceEmailContentTags(string mailBody, string sName, string sFirstName, string sClient, string sFault, Int32 iWorksheet)
        {
            mailBody = mailBody.Replace("<% FullName %>", sName);
            mailBody = mailBody.Replace("<% FirstName %>", sFirstName);
            mailBody = mailBody.Replace("<% Client %>", sClient);
            mailBody = mailBody.Replace("<% Fault %>", sFault);
            mailBody = mailBody.Replace("<% WorksheetId %>", iWorksheet.ToString());

            return mailBody;
        }
    }
}
