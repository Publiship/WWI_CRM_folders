using System;
using System.Data;
using System.Configuration; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography; 
using SubSonic;
using DAL.Logistics;

namespace DAL.Logistics
{
    public struct uTrackingPrefs
    {
        public string colName;
        public bool visible;
        public int visibleindex;
    }
    
    /// <summary>
    /// Summary description for wwi_security
    /// </summary>
    public class wwi_security
    {
        
        public wwi_security()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string EncryptString(string Message, string Passphrase)
        {
            string _result = "";

            if (!string.IsNullOrEmpty(Message))
            {
                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below
                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                // Step 3. Setup the encoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToEncrypt = UTF8.GetBytes(Message);

                // Step 5. Attempt to encrypt the string
                try
                {
                    ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the encrypted string as a base64 encoded string
                _result = Convert.ToBase64String(Results);
            }

            return _result;
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            string _result = "";

            if (!string.IsNullOrEmpty(Message))
            {
                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                // Step 3. Setup the decoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                //byte[] DataToDecrypt = Convert.FromBase64String(Message);
                byte[] DataToDecrypt = Convert.FromBase64String(Message.Replace(" ", "+")); //prevents invalid length for base 64 char aray
                // Step 5. Attempt to decrypt the string
                try
                {
                    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the decrypted string in UTF8 format
                _result = UTF8.GetString(Results);
            }

            return _result; 
        }
        //end decrypt string

        /// <summary>
        /// derive user details from submitted email address
        /// </summary>
        /// <param name="txtmail"></param>
        /// <returns></returns>
        public static String getuserAccount(string txtmail)
        {
            txtmail = wwi_security.DecryptString(txtmail, "publiship");  
            String _account = "";
            Query _qryb = new Query(Tables.ContactTable).WHERE("Email", Comparison.Equals, txtmail).AND("Live", Comparison.Equals, true);
            ContactTableCollection _contact = new ContactTableCollection();
            _contact.LoadAndCloseReader(_qryb.ExecuteReader());

            if (_contact.Count != 0)
            {
                _account = (String)_contact[0].ContactName + "#" + (String)_contact[0].Password;

            }
            else
            {
                Query _qry = new Query(Tables.EmployeesTable).WHERE("EmailAddress", Comparison.Equals, txtmail).AND("Live", Comparison.Equals, true);
                EmployeesTableCollection _employ = new EmployeesTableCollection();
                _employ.LoadAndCloseReader(_qry.ExecuteReader());

                if (_employ.Count != 0)
                {
                    _account = (String)_employ[0].Name + "#" + (String)_employ[0].Password;
                }
            }

            return _account;
        }
        //end get user account

        /// <summary>
        /// derive user ids as userid#companyid for a valid login
        /// return empty string if login fails
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">encrypted password</param>
        /// <returns></returns>
        public static String getuserIds(string username, string password)
        {
            //should we rquire password encryption here?
            //string txtPassword = wwi_security.DecryptString(password, "publiship");
            
            String _account = "";
            Query _qry1 = new Query(Tables.ContactTable).WHERE("ContactName", Comparison.Equals, username).AND("Password", Comparison.Equals, password).AND("Live", Comparison.Equals, true);
            ContactTableCollection _contact = new ContactTableCollection();
            _contact.LoadAndCloseReader(_qry1.ExecuteReader());

            if (_contact.Count != 0)
            {
                _account = _contact[0].ContactID.ToString() + "#" + (String)_contact[0].CompanyID.ToString();

            }
            else
            {
                Query _qry2 = new Query(Tables.EmployeesTable).WHERE("Name", Comparison.Equals, username).AND("Password", Comparison.Equals, password).AND("Live", Comparison.Equals, true);
                EmployeesTableCollection _employ = new EmployeesTableCollection();
                _employ.LoadAndCloseReader(_qry2.ExecuteReader());

                if (_employ.Count != 0)
                {
                    _account = _employ[0].EmployeeID.ToString() + "#-1"; //internal users do not have a company id set to -1
                }
            }

            return _account;
        }
        //overloaded
        public static int getuserIds(string username, string password, string returnid)
        {
            //should we rquire password encryption here?
            //string txtPassword = wwi_security.DecryptString(password, "publiship");

            int _account = 0;
            Query _qry1 = new Query(Tables.ContactTable).WHERE("ContactName", Comparison.Equals, username).AND("Password", Comparison.Equals, password).AND("Live", Comparison.Equals, true);
            ContactTableCollection _contact = new ContactTableCollection();
            _contact.LoadAndCloseReader(_qry1.ExecuteReader());

            if (_contact.Count != 0)
            {
                _account = returnid == "user"? (int)_contact[0].ContactID : (int)_contact[0].CompanyID;

            }
            else
            {
                Query _qry2 = new Query(Tables.EmployeesTable).WHERE("Name", Comparison.Equals, username).AND("Password", Comparison.Equals, password).AND("Live", Comparison.Equals, true);
                EmployeesTableCollection _employ = new EmployeesTableCollection();
                _employ.LoadAndCloseReader(_qry2.ExecuteReader());

                if (_employ.Count != 0)
                {
                    _account = returnid == "user"? (int)_employ[0].EmployeeID: -1;
                }
            }

            return _account;
        }
        //end get user ids
    }//class func

    public class UserClass
    {

        private Guid _id;
        //
        private Int32 _userId;
        private string _userName;
        private string _userInitials;
        private Int32 _officeId;
        private string _passWord;
        private Int32 _companyId;
        private Int32 _defaultView;
        private Int32 _isEditor;
        private string _officeName;
        private string _mailTo;
        private string _telNo;
        private int _controlOfficeId;
        private int _companyGroup;
        private int _loginValue;

        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public Int32 UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string UserInitials
        {
            get { return _userInitials; }
            set { _userInitials = value; }
        }

        public Int32 OfficeId
        {
            get { return _officeId; }
            set { _officeId = value; }
        }

        public string Password
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        public Int32 CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        public Int32 DefaultView
        {
            get { return _defaultView; }
            set { _defaultView = value; }
        }

        public Int32 IsEditor
        {
            get { return _isEditor; }
            set { _isEditor = value; }
        }

        public string OfficeName
        {
            get { return _officeName; }
            set { _officeName = value; }
        }

        public string mailTo
        {
            get { return _mailTo; }
            set { _mailTo = value; }
        }

        public string telNo
        {
            get { return _telNo; }
            set { _telNo = value; }
        }

        public int controlOfficeId
        {
            get { return _controlOfficeId; }
            set { _controlOfficeId = value; }
        }

        public int companyGroup
        {
            get { return _companyGroup; }
            set { _companyGroup = value; }
        }

        public int loginValue
        {
            get { return _loginValue; }
            set { _loginValue = value; }
        }

        public UserClass Login(string txtUser, string txtPassword)
        {
            //26/05/2011 we now encrypt passwords
            txtPassword = wwi_security.DecryptString(txtPassword, "publiship");
            
            //initialise
            UserClass UserLogin = new UserClass();
            UserLogin.ID = Guid.Empty;
            int _rowcount = 0;
            IDataReader _rd;
            SubSonic.SqlQuery query = new SubSonic.SqlQuery();

            try
            {
                string[] _cols = { "ContactTable.ContactID", "ContactTable.ContactName", "ContactTable.Name", "ContactTable.ContactInitials", "ContactTable.CompanyID", "ContactTable.DefaultView", "ContactTable.IsEditor", "ContactTable.EMail", "ContactTable.ControllingOfficeID", "NameAndAddressBook.CompanyName", "NameAndAddressBook.TelNo", "NameAndAddressBook.Pricer_Group" };
                //query = DB.Select(_cols).From("ContactTable").LeftOuterJoin("NameAndAddressBook", "CompanyID", "ContactTable", "CompanyID").Where("ContactName").IsEqualTo(txtUser).And("Password").IsEqualTo(txtPassword).And("Live").IsEqualTo(true);
                //have check ContactName AND Name as usage is inconsistent in database
                query = DB.Select(_cols).From("ContactTable").LeftOuterJoin("NameAndAddressBook", "CompanyID", "ContactTable", "CompanyID").
                    Where("ContactName").IsEqualTo(txtUser).And("Password").IsEqualTo(txtPassword).And("Live").IsEqualTo(true).
                    Or("Name").IsEqualTo(txtUser).And("Password").IsEqualTo(txtPassword).And("Live").IsEqualTo(true);

                _rd = query.ExecuteReader();

                while (_rd.Read())
                {
                    UserLogin.ID = Guid.NewGuid();
                    UserLogin.UserId = (Int32)_rd["ContactID"];
                    UserLogin.UserName = Convert.ToString(_rd["ContactName"]); ; //Convert.ToString(_rd["ContactName"]);
                    UserLogin.UserInitials = _rd["ContactInitials"] != null ? Convert.ToString(_rd["ContactInitials"]) : "";
                    UserLogin.OfficeId = -1; //external client
                    UserLogin.CompanyId = (Int32)_rd["CompanyID"];
                    UserLogin.DefaultView = (Int32)_rd["DefaultView"];
                    UserLogin.IsEditor = (byte)_rd["IsEditor"];
                    UserLogin.mailTo = _rd["EMail"] != null ? Convert.ToString(_rd["EMail"]) : "";
                    UserLogin.OfficeName = _rd["CompanyName"] != null ? Convert.ToString(_rd["CompanyName"]) : "";
                    UserLogin.telNo = _rd["TelNo"] != null ? Convert.ToString(_rd["TelNo"]) : "";
                    UserLogin.controlOfficeId = (Int32)_rd["ControllingOfficeID"]; //this should be the new controller office link id
                    UserLogin.companyGroup = _rd["Pricer_Group"] != null ? wwi_func.vint(_rd["Pricer_Group"].ToString())  : 0;
                    UserLogin.loginValue = 1; //successful login
                    _rowcount++;
                }

                //Query _qryb = new Query(Tables.ContactTable).WHERE("ContactName", Comparison.Equals, txtUser).AND("Password", Comparison.Equals, txtPassword).AND("Live", Comparison.Equals, true);
                //ContactTableCollection _contact = new ContactTableCollection();
                //_contact.LoadAndCloseReader(_qryb.ExecuteReader());
                //
                //if (_contact.Count != 0)
                // {
                //    UserLogin.ID = Guid.NewGuid();
                //    UserLogin.UserId = (Int32)_contact[0].ContactID; ;
                //    UserLogin.UserName = (String)_contact[0].ContactName;
                //    UserLogin.UserInitials = (String)_contact[0].ContactInitials;
                //    UserLogin.OfficeId = -1; //external client
                //    UserLogin.CompanyId = (Int32)_contact[0].CompanyID;
                //    UserLogin.DefaultView  = (Int32)_contact[0].DefaultView;
                //    UserLogin.IsEditor = (Int32)_contact[0].IsEditor;  
                //} 
                if (_rowcount == 0) //try internal user table instead
                {
                    //string[] _cols2 = { "EmployeesTable.EmployeeID, EmployeesTable.Name, EmployeesTable.OfficeID, EmployeesTable.DefaultView, EmployeesTable.IsEditor, EmployeesTable.EmailAddress", "OfficeTable.OfficeName" };
                    string[] _cols2 = { "EmployeesTable.EmployeeID", "EmployeesTable.Name", "EmployeesTable.OfficeID", "EmployeesTable.DefaultView", "EmployeesTable.IsEditor", "EmployeesTable.EmailAddress", "OfficeTable.OfficeName" };
                    query = DB.Select(_cols2).From("EmployeesTable").LeftOuterJoin("OfficeTable", "OfficeID", "EmployeesTable", "OfficeID").Where("Name").IsEqualTo(txtUser).And("Password").IsEqualTo(txtPassword).And("Live").IsEqualTo(true);

                    _rd = query.ExecuteReader();

                    while (_rd.Read())
                    {
                        UserLogin.ID = Guid.NewGuid();
                        UserLogin.UserId = (Int32)_rd["EmployeeID"];
                        UserLogin.UserName = (String)_rd["Name"];
                        UserLogin.UserInitials = "";  //does not apply to internal user
                        UserLogin.OfficeId = (Int32)_rd["OfficeID"];
                        UserLogin.CompanyId = -1;  //does not apply to internal user
                        UserLogin.DefaultView = (Int32)_rd["DefaultView"];
                        UserLogin.IsEditor = (byte)_rd["IsEditor"];
                        UserLogin.mailTo = _rd["EmailAddress"] != null ? Convert.ToString(_rd["EmailAddress"]) : "";
                        UserLogin.OfficeName = _rd["OfficeName"] != null ? Convert.ToString(_rd["OfficeName"]) : "";
                        UserLogin.telNo = "";
                        UserLogin.controlOfficeId = -1; //does not apply to internal user
                        UserLogin.companyGroup = 0;
                        UserLogin.loginValue = 1; //successful login
                        _rowcount++;
                    }
                    //build query using username and password
                    //check smaller employees table THEN contact table

                    //Query _qry = new Query(Tables.EmployeesTable).WHERE("Name", Comparison.Equals, txtUser).AND("Password", Comparison.Equals, txtPassword).AND("Live", Comparison.Equals, true);
                    //EmployeesTableCollection _employ = new EmployeesTableCollection();
                    //_employ.LoadAndCloseReader(_qry.ExecuteReader());
                    //
                    //if (_employ.Count != 0)
                    //{
                    //    UserLogin.ID = Guid.NewGuid();
                    //    UserLogin.UserId = (Int32)_employ[0].EmployeeID;
                    //    UserLogin.UserName = (String)_employ[0].Name;
                    //    UserLogin.UserInitials = "";
                    //    UserLogin.OfficeId = (Int32)_employ[0].OfficeID;
                    //    UserLogin.CompanyId = -1;
                    //    UserLogin.DefaultView = (Int32)_employ[0].DefaultView;
                    //    UserLogin.IsEditor = (Int32)_employ[0].IsEditor;
                    //    
                }
            }
            catch (Exception ex)
            {
                string _ex = ex.Message.ToString();
                //set guid or login will end up returning as null
                UserLogin.ID = Guid.NewGuid();
                //return indicator that there was an error do not return error message as we want to hide that from user
                UserLogin.loginValue = 0; 
            }
            finally
            {
                if (UserLogin.ID == Guid.Empty) { UserLogin = null; }
                //if (UserLogin.ID == Guid.Empty) return null;
                //else
                //{
                //    return UserLogin;  
                //}
            }

            return UserLogin;
        }

        //public static void CheckLogin(System.Web.UI.Page Page)
        public static Boolean CheckLogin(System.Web.UI.Page Page)
        {
            Boolean _loggedin = false;

            try
            {
                //if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["loginmethod"]) || (ConfigurationManager.AppSettings["loginmethod"]).ToUpper() == "WITHOUTLOGIN") return;
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["loginmethod"]) || (ConfigurationManager.AppSettings["loginmethod"]).ToUpper() == "WITHOUTLOGIN") _loggedin = true;

                ////    Page.Response.CacheControl = "no-cache";
                ////    Page.Response.AddHeader("Pragma", "no-cache");
                ////    Page.Response.Expires = -1;

                if (Page.Session["user"] != null)
                {
                    _loggedin = true;
                    //Page.Response.Redirect(ConfigurationManager.AppSettings["loginurl"] + "?url=" + Page.Request.RawUrl);
                }
                else if (((UserClass)Page.Session["user"]).ID != Guid.Empty)
                {
                    _loggedin = true;
                    //Page.Response.Redirect(ConfigurationManager.AppSettings["loginurl"] + "?url=" + Page.Request.RawUrl);
                }
            }
            catch (Exception err)
            {
                String _err = err.Message.ToString(); 
                _loggedin = false;
            }
            return _loggedin; 
        }

    }//end of userclass

     
}