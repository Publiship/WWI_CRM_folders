using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization; 
using System.Text;
using System.Security.Cryptography;
using SubSonic;
using DAL.Logistics;
/// <summary>
/// code derived from wwi_security class as a means to log in via web service
/// e.g. for logins from mobile apps
/// </summary>
namespace DAL.Services
{
    [WebService(Namespace = "http://www.publiship.com/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
     // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Service_Security : System.Web.Services.WebService
    {

        public Service_Security()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        /// <summary>
        /// service to determine if user/password combination is valid
        /// returns an empty string if invalid login
        /// returns encryped string of user class if valid login
        /// </summary>
        /// <param name="UserName">string</param>
        /// <param name="Password">string</param>
        /// <returns></returns>
        [WebMethod(Description = "This method call will authenticate a user from encrypted user name and password combination", EnableSession = false)]
        public string GetUserAuth(string UserName, string Password)
        {
           
            //return JSON serialised encrypted user info
            
            int _saved = 0;
            string _json = "";
            UserClass _authLogin = new UserClass();
 
            _authLogin = UserClass.crypto1Login(UserName, Password);
            if (_authLogin != null && _authLogin.ID != Guid.Empty)
            {
                JavaScriptSerializer _js = new JavaScriptSerializer();
                _saved = Service_Security.append_to_user_log(_authLogin);
                _json += _saved != 0? wwi_security.EncryptString(_js.Serialize(_authLogin),"publiship"): ""; 
            }

            return _json;
        }
        //end get user auth

        [WebMethod(Description = "This method call will return user id encrypted from encrypted user name and password combination", EnableSession = false)]
        public string GetUserHasId(string UserName, string Password)
        {

            //return JSON serialised encrypted user info

            int _saved = 0;
            string _ids = ""; //user id and company id seperated by #
            UserClass _authLogin = new UserClass();

            _authLogin = UserClass.crypto1Login(UserName, Password);
            if (_authLogin != null && _authLogin.ID != Guid.Empty)
            {
                JavaScriptSerializer _js = new JavaScriptSerializer();
                _saved = Service_Security.append_to_user_log(_authLogin);
                _ids = _saved != 0 ? wwi_security.EncryptString(_authLogin.UserId + "#" + _authLogin.CompanyId, "publiship") : "";
            }

            return _ids;
        }
        //end get user has id

        public static int append_to_user_log(UserClass thisuser)
        {
            int _logged = 0; 

            try
            {
                UserLog _newlog = new UserLog();

                if (thisuser.CompanyId == -1)
                { //employee
                    _newlog.ContactID = 0;
                    _newlog.EmployeeID = thisuser.UserId;
                }
                else
                {
                    _newlog.ContactID = thisuser.UserId;
                    _newlog.EmployeeID = 0;
                }

                _newlog.LogDate = DateTime.Now;
                _newlog.Save();
                _logged = 1;

            }
            catch 
            {
                _logged = 0;
            }
            return _logged; 
        }
        //end append to log        

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

        /// <summary>
        /// encrypto1login for utf8/md5 encryption
        /// </summary>
        /// <param name="txtUser"></param>
        /// <param name="txtPassword"></param>
        /// <returns></returns>
        public static UserClass crypto1Login(string txtUser, string txtPassword)
        {

            txtUser = wwi_security.DecryptString(txtUser, "publiship");
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
                //string[] _cols = { "ContactTable.ContactID, ContactTable.ContactName, ContactTable.ContactInitials, ContactTable.CompanyID, ContactTable.DefaultView, ContactTable.IsEditor, ContactTable.EMail, ContactTable.ControllingOfficeID, NameAndAddressBook.CompanyName, NameAndAddressBook.TelNo, NameAndAddressBook.Pricer_Group" };
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
                    UserLogin.UserName = Convert.ToString(_rd["ContactName"]);
                    UserLogin.UserInitials = _rd["ContactInitials"] != null ? Convert.ToString(_rd["ContactInitials"]) : "";
                    UserLogin.OfficeId = -1; //external client
                    UserLogin.CompanyId = (Int32)_rd["CompanyID"];
                    UserLogin.DefaultView = (Int32)_rd["DefaultView"];
                    UserLogin.IsEditor = (byte)_rd["IsEditor"];
                    UserLogin.mailTo = _rd["EMail"] != null ? Convert.ToString(_rd["EMail"]) : "";
                    UserLogin.OfficeName = _rd["CompanyName"] != null ? Convert.ToString(_rd["CompanyName"]) : "";
                    UserLogin.telNo = _rd["TelNo"] != null ? Convert.ToString(_rd["TelNo"]) : "";
                    UserLogin.controlOfficeId = (Int32)_rd["ControllingOfficeID"]; //this should be the new controller office link id
                    UserLogin.companyGroup = _rd["Pricer_Group"] != null ? wwi_func.vint(_rd["Pricer_Group"].ToString()) : 0;
                    UserLogin.loginValue = 1; //successful login
                    _rowcount++;
                }
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
        //end log in
    }
    //end user class
}
