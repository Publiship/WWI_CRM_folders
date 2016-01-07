using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DAL.Pricer;
using ParameterPasser;

public partial class file_manager : System.Web.UI.Page
{
    private int _countUpdated = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        this.dxpnlerr.Visible = false;
        this.dxlblerr.Text = ""; //clear default message
        this.dxpnlinfo.Visible = false;
        this.dxlblinfo2.Text = ""; //clear default message

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            this.dxhfmanager.Clear();
            this.dxhfmanager.Add("upld", false);
            Session["orderlist"] = null;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!Page.IsPostBack)
            //{

            if (isLoggedIn())
            {
                UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
                //this.dxhfmanager.Set("uid", wwi_security.EncryptString(_thisuser.UserId.ToString(), "publiship"));

                //orderid and title id are passed through - remove semi-colon?
                //check querystring or default to label where order id might have been passed back from order search form
                string _orderno = Request.QueryString["pod"] != null ? wwi_security.DecryptString(Request.QueryString["pod"].Replace(",", ""), "publiship") : null;
                if (!string.IsNullOrEmpty(_orderno))
                {
                    //100112 check to see if attached to a document folder other than it's order number
                    string _docfolder = Request.QueryString["dfd"] != null ? wwi_security.DecryptString(Request.QueryString["dfd"].Replace(",", ""), "publiship") : null;
                    //100112 check to see folder status 
                    string _cdir = Request.QueryString["cd"] != null ? Request.QueryString["cd"].Replace(",", "") : null;
                    if (!string.IsNullOrEmpty(_cdir)) { this.dxhfmanager.Set("cdir", _cdir); }
                    //160112 get house b/l 
                    string _housebl = Request.QueryString["hbl"] != null ? wwi_security.DecryptString(Request.QueryString["hbl"].Replace(",", ""), "publiship") : null;
                    //if (_orderno != null)

                    //{
                    //this.dxhfmanager.Set("ord", _orderno);
                    //this.dxlblorderno1.Text = wwi_security.DecryptString(_orderno, "publiship");

                    //}
                    //else
                    //{
                    //    this.dxlblorderno1.Text = "";
                    //}


                    //role based security
                    //companyid-1 internal users can upload/edit/delete
                    //other users are view only
                    //this DOES NOT Work!
                    //UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
                    //this.dxfmpod.SettingsPermissions.Role = _thisuser.CompanyId!=-1? "editor": "viewer";

                    bool _iseditor = _thisuser.CompanyId == -1 ? true : false; //full rights for internal users only (companyid=-1)

                    set_fm_permissions(_iseditor);
                    set_visible_folder(_iseditor, _orderno, _docfolder);

                    //datatable of selected orderno's
                    bind_order_data(wwi_func.vint(_orderno), _housebl);

                    this.dxfmpod.ClientVisible = true;
                    this.dxpnlerr.Visible = false;
                    this.dxpnlinfo.Visible = false;
                }
                else
                {
                    Response.Redirect("../tracking/shipment_tracking.aspx?");
                }
            }
            else
            {
                Response.Redirect("../user_accounts/signin.aspx?" + "rp=" + wwi_security.EncryptString("tracking/file_manager", "publiship"));
            }
            //}
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpnlerr.Visible = true;
        }
    }

    protected static bool isLoggedIn()
    {
        // TODO: Your custom logic here
        return HttpContext.Current.Session["user"] != null;
    }

    /// <summary>
    /// set options in code as role based directly to filemanager does not work
    /// </summary>
    /// <param name="cId"></param>
    protected void set_fm_permissions(bool iseditor)
    {
        //toolbar
        this.dxfmpod.SettingsToolbar.ShowCreateButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowDeleteButton = false; //iseditor; don't give anyone the option to delete?
        this.dxfmpod.SettingsToolbar.ShowMoveButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowRenameButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowPath = iseditor;
        //editor
        this.dxfmpod.SettingsEditing.AllowCreate = iseditor;
        this.dxfmpod.SettingsEditing.AllowDelete = iseditor;
        this.dxfmpod.SettingsEditing.AllowMove = iseditor;
        this.dxfmpod.SettingsEditing.AllowRename = iseditor;
        //upload
        //switch off we are using uploader so we can do mulitple files
        this.dxfmpod.SettingsUpload.Enabled = false; //iseditor;
    }
    //end set permissions


    /// <summary>
    /// only internal users can browse folder structure
    /// other users are locked to the specified order number
    /// </summary>
    /// <param name="orderid"></param>
    protected void set_visible_folder(bool iseditor, string orderno, string docfolder)
    {
        //order can be attached to a folder other than it's own (for multiple document sets) so use the docfolder if there is one
        string _folder = wwi_func.vint(docfolder) > 0 ? docfolder : orderno;

        bool _dir = false;
        int createfolder = this.dxhfmanager.Contains("cdir") ? wwi_func.vint(this.dxhfmanager.Get("cdir").ToString()) : 0;
        string _root = this.dxfmpod.GetAppRelativeRootFolder().ToString();
        string _fullpath = "~\\documents\\" + _folder;


        //default folder if viewer restrict to specific folder
        if (!string.IsNullOrEmpty(_folder))
        {
            _dir = folder_exists(_fullpath);
            if (!_dir && iseditor)
            {
                switch (createfolder)
                {
                    case 0:
                        {
                            this.dxlblMsgbox.Text = "There is no folder for Order Number " + _folder + " do you want to create one now?";
                            this.dxpopmsgbox.ShowOnPageLoad = true;
                            break;
                        }
                    case 1:
                        {
                            _dir = folder_create(_fullpath);
                            this.dxhfmanager.Remove("cdir");
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            //if (_dir)
            //{
            //    _files = wwi_func.file_count(_fullpath); //check to see if we have any files uploaded
            //}
            //else
            //{
            //    _dir = iseditor ? confirm_create_folder(_fullpath) == _folder : false;
            //}
        }
        //end check

        //if (_dir && _files > 0) //we must have a valid folder and uploaded files
        if (_dir)
        {
            if (iseditor) //need to replace this filemanager with somthing more adaptable! you can't switch on/off folder tree once page is loaded! rubbish 
            {
                string _view = this.dxhfmanager.Contains("view") ? this.dxhfmanager.Get("view").ToString() : null;
                if (!string.IsNullOrEmpty(_view))
                {
                    if (_view == "All folders")
                    {
                        //intitial folder drvies from root folder so don't need to specify full path
                        //290113 initial folder is very slow as it loads all folder names to directory tree before displaying. Find a better component
                        //this.dxfmpod.Settings.InitialFolder = _folder;
                        this.dxfmpod.Settings.RootFolder = _fullpath;
                    }
                    else
                    {
                        this.dxfmpod.Settings.RootFolder = _fullpath;
                    }
                }
                else
                {
                    this.dxfmpod.Settings.RootFolder = _fullpath;
                }
            }
            else
            {
                //access only to this folder
                this.dxfmpod.Settings.RootFolder = _fullpath;
            }
            //show filenanager panel, hide the other panels
            //this.dxlblinfo.Text = "";
            //this.dxpnlinfo.Visible = false;

            this.dxpnlerr.Visible = false;
        }
        else
        {
            if (iseditor)
            {
                this.dxfmpod.Settings.RootFolder = "~\\documents\\not found";
                this.dxfmpod.Enabled = true;
            }
            else
            {
                this.dxfmpod.Settings.RootFolder = "~\\documents\\not found";
                this.dxfmpod.Enabled = false;
            }
        }
    }
    //end set visible folder

    /// <summary>
    /// find a folder in the site
    /// </summary>
    /// <param name="dirname"></param>
    /// <returns></returns>
    protected bool folder_exists(string dirname)
    {
        bool _filename = false;
        try
        {
            System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(Server.MapPath(dirname));

            if (_di.Exists) { _filename = true; }
        }
        catch
        {
            _filename = false;
        }

        return _filename;
    }
    //end folder exists

    //generate new folder
    protected bool folder_create(string dirname)
    {
        bool _created = false;
        System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(Page.Server.MapPath(dirname));

        if (!_di.Exists)
        {
            try
            {
                _di = System.IO.Directory.CreateDirectory(Page.Server.MapPath(dirname));
                //100112 set cdir flag to 2 so we don't ask to create folder again
                this.dxhfmanager.Set("cdir", 2);
                _created = true;
            }
            catch (Exception ex)
            {
                _created = false;
                this.dxlblerr.Text = ex.Message.ToString();
                this.dxpnlerr.Visible = true;

            }
        }
        else
        {
            _created = true;
        }

        return _created;
    }

    /// <summary>
    /// copy all files in named folder to another folder
    /// </summary>
    /// <param name="dirname"></param>
    /// <returns></returns>
    protected int file_transfer(string sourcePath, string targetPath, bool overWrite)
    {
        int copied = 0;

        if (System.IO.Directory.Exists(Server.MapPath(sourcePath)))
        {
            string[] _files = System.IO.Directory.GetFiles(Server.MapPath(sourcePath));

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in _files)
            {
                // Use static Path methods to extract only the file name from the path.
                string _fileName = System.IO.Path.GetFileName(s);
                string _destFile = System.IO.Path.Combine(targetPath, _fileName);
                System.IO.File.Copy(s, Server.MapPath(_destFile), overWrite);
                copied += 1;
            }

        }
        else
        {
            this.dxlblerr.Text = "Unable to copy files from temporary folder to target folder";
            this.dxpnlerr.Visible = true;
        }

        return copied;
    }

    /// <summary>
    /// get files count for specified directory
    /// deprecated can use same function from wwi_func
    /// </summary>
    /// <param name="dirname"></param>
    /// <returns></returns>
    protected int file_count_deprecated(string dirname)
    {
        int _filecount = 0;

        _filecount = System.IO.Directory.GetFiles(Page.Server.MapPath(dirname)).Length;

        return _filecount;
    }
    //end get file count

    /// <summary>
    ///if not null use initial folder
    ///else if not null use root folder
    /// </summary>
    /// <returns></returns>
    protected string return_selected_folder()
    {
        string _folderid = "";

        if (!string.IsNullOrEmpty(this.dxfmpod.Settings.InitialFolder.ToString()))
            _folderid = this.dxfmpod.Settings.InitialFolder.ToString();
        else if (!string.IsNullOrEmpty(this.dxfmpod.Settings.RootFolder.ToString()))
            _folderid = this.dxfmpod.Settings.RootFolder.ToString();

        return _folderid;
    }
    //end return selected folder

    /// <summary>
    /// get folder id from full path
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected int return_selected_folderid(string folderpath)
    {
        int _folderid = 0;


        if (!string.IsNullOrEmpty(folderpath))
        {
            char[] _ch = "\\".ToCharArray();
            string[] _path = folderpath.Split(_ch);
            _folderid = wwi_func.vint(_path.GetValue(_path.Length - 1).ToString());
        }

        return _folderid;
    }

    protected void dxcbpodfiles_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        try
        {
            string _folderpath = "";
            //int _orderno = 0;
            int _ordernocount = 0;
            Int32 _folderid = 0;
            bool _uploaded = false; //flagged if uploaded completed ok

            _folderpath = return_selected_folder();
            _folderid = return_selected_folderid(_folderpath);
            _uploaded = this.dxhfmanager.Contains("upld") ? (bool)this.dxhfmanager.Get("upld") : false;

            //09012011 stored in ilist to save casting datatable for IN query
            //05012011 multiple order no's are stored in data table
            //_ordernocount = _ol.Rows.Count;
            //_orderno = wwi_func.vint(this.dxlblorderno1.Text.ToString());  //this.dxhfmanager.Contains("ord") ? wwi_func.vint(wwi_security.DecryptString(this.dxhfmanager.Get("ord").ToString(), "publiship")) : 0;
            //DataTable _ol = (DataTable)Session["orderlist"];
            IList<Int32> _ol = (IList<Int32>)Session["orderlist"];
            _ordernocount = _ol.Count;


            if (_uploaded & _folderid > 0 && _ordernocount > 0)
            //if (_uploaded & _folderid > 0 && _orderno > 0)
            {
                //copy files from temporary folder
                //string _uid = wwi_security.DecryptString(this.dxhfmanager.Get("uid").ToString(), "publiship");
                string _uid = ((UserClass)HttpContext.Current.Session["user"]).UserId.ToString();
                string _tempdir = DateTime.Now.ToShortDateString().Replace("/", "") + _uid;
                string _sourcepath = System.IO.Path.Combine("~\\documents\\not found", _tempdir);

                int _copies = file_transfer(_sourcepath, _folderpath, true);
                if (_copies > 0)
                {
                    //update order table
                    //save quote id to order table
                    //if (_ordernocount == 1)
                    //{
                    //    //(DataRow)_ol.Rows[0][0]
                    //    SubSonic.Update upd2 = new SubSonic.Update(DAL.Logistics.Schemas.OrderTable);
                    //    recordsaffected = upd2.Set("document_folder").EqualTo(_folderid)
                    //                           .Where("OrderNumber").IsEqualTo(_ol[0])
                    //                           .Execute();
                    //}
                    //else
                    //{
                    //string _csv = wwi_func.datatable_to_string(_ol, ",");
                    string _csv = string.Join(",", _ol.Select(i => i.ToString()).ToArray());

                    //establish connection
                    ConnectionStringSettings _cs = ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"];
                    SqlConnection _cn = new SqlConnection(_cs.ConnectionString);
                    //create a sql data adapter
                    System.Data.SqlClient.SqlDataAdapter _adapter = new System.Data.SqlClient.SqlDataAdapter();
                    //instantiate event handler
                    _adapter.RowUpdated += new SqlRowUpdatedEventHandler(adapter_RowUpdated);

                    _adapter.SelectCommand = new SqlCommand("SELECT [document_folder], [orderNumber] FROM OrderTable WHERE [OrderNumber] IN (" + _csv + ")", _cn);
                    //populate datatable using selected order numbers
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);

                    //update document folder for each order number in _dt
                    for (int ix = 0; ix < _dt.Rows.Count; ix++)
                    {
                        _dt.Rows[ix]["document_folder"] = _folderid;
                    }

                    //update command
                    _adapter.UpdateCommand = new SqlCommand("UPDATE OrderTable SET [document_folder] = @document_folder WHERE [OrderNumber] = @OrderNumber;", _cn);
                    _adapter.UpdateCommand.Parameters.Add("@OrderNumber", SqlDbType.Int).SourceColumn = "OrderNumber";
                    _adapter.UpdateCommand.Parameters.Add("@document_folder", SqlDbType.Int).SourceColumn = "document_folder";

                    _adapter.UpdateBatchSize = 5; //_ordernocount;
                    _adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    _adapter.Update(_dt);
                    _adapter.Dispose();
                    _cn.Close();

                    //}

                    if (_countUpdated > 0) //countupdated will return the number of batches sent NOT the number of records updated
                    {
                        //disable session expiration which occurs when a directory is deleted
                        //uses System.Reflection
                        PropertyInfo p = typeof(System.Web.HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                        object o = p.GetValue(null, null);
                        FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                        object monitor = f.GetValue(o);
                        MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
                        m.Invoke(monitor, new object[] { });
                        //kill temp folder
                        System.IO.Directory.Delete(Server.MapPath(_sourcepath), true);


                        this.dxlblinfo2.Text += _copies + " files have been uploaded to folder " + _folderid + " and linked to order numbers(s) " + _csv;
                        this.dxpnlinfo.Visible = true;
                    }
                    else
                    {
                        this.dxlblerr.Text = "We have not been able to link order number(s) to online folder " + _folderid + " please refer to Publiship I.T.";
                        this.dxpnlerr.Visible = true;
                    }
                    //end check records affected
                }
            }
            //end update order table
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpnlerr.Visible = true;
        }
    }
    //end callback 

    protected static void adapter_RowUpdating(
  object sender, SqlRowUpdatingEventArgs args)
    {
        if (args.StatementType == StatementType.Update)
        {

        }
    }

    private void adapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
    {
        _countUpdated++;
    }

    /// <summary>
    /// save each file to correct path as it is uploaded
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxuploadpodfiles_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
    {
        if (e.IsValid)
        {
            //string _uid = wwi_security.DecryptString(this.dxhfmanager.Get("uid").ToString(), "publiship");
            string _uid = ((UserClass)HttpContext.Current.Session["user"]).UserId.ToString();
            //save to temporary folder then copy after all files have been uploaded during dxcbpodfiles_Callback
            string _filename = e.UploadedFile.FileName;
            string _tempdir = DateTime.Now.ToShortDateString().Replace("/", "") + _uid;
            string _filepath = System.IO.Path.Combine("~\\documents\\not found", _tempdir);
            if (folder_create(_filepath) == true)
            {
                e.UploadedFile.SaveAs(Server.MapPath(System.IO.Path.Combine(_filepath, _filename)));
            }
        }
        else
        {
            this.dxlblerr.Text = "File upload failed";
            this.dxpnlerr.Visible = true;
        }
    }
    //end file upload complate

    /// <summary>
    /// button disabled do we really want to give access to all folders? 
    /// redirect to view with folders tree activated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxbtnfolders_Click(object sender, EventArgs e)
    {

        //redirect to alternate view?  
        string _cdir = this.dxhfmanager.Contains("cdir") ? this.dxhfmanager.Get("cdir").ToString() : "0";
        string _orderno = Request.QueryString["pod"] != null ? Request.QueryString["pod"].ToString() : null;
        string _housebl = Request.QueryString["hbl"] != null ? Request.QueryString["hbl"] : null;
        Response.Redirect("file_manager_all.aspx?pod=" + _orderno + "&cd=" + _cdir + "&hbl=" + _housebl);
    }

    /// ************************************************************
    /// <summary>
    /// rebind datatable to session or create datatable if it does not exist
    /// </summary>
    protected void bind_order_data()
    {
        try
        {
            if (Session["orderlist"] != null)
            {
                IList<Int32> _ol = (IList<Int32>)Session["orderlist"];
                //DataTable _ol = (DataTable)Session["orderlist"];
                this.dlorder.DataSource = _ol;
                this.dlorder.DataBind();
            }
            else
            {
                IList<Int32> _ol = build_order_table();
                //_ol.Add(orderno);
                //DataTable _ol = build_order_table();
                //_ol.Rows.Add(orderno); 
                Session["orderlist"] = _ol;
                this.dlorder.DataSource = _ol;
                this.dlorder.DataBind();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    // overloads
    protected void bind_order_data(Int32 orderno, string houseBL)
    {
        try
        {
            if (Session["orderlist"] != null)
            {
                IList<Int32> _ol = (IList<Int32>)Session["orderlist"];
                //DataTable _ol = (DataTable)Session["orderlist"];
                this.dlorder.DataSource = _ol;
                this.dlorder.DataBind();
            }
            else
            {
                //IList<Int32> _ol = build_order_table();
                //_ol.Add(orderno);
                //160112 find any other order numbers covered by house b/l and ad to ilist
                IList<Int32> _ol = new ordertablecustomcontroller().get_orders_by_houseBL(houseBL);
                if (_ol.Count == 0)
                {
                    _ol.Add(orderno);
                }
                //DataTable _ol = build_order_table();
                //_ol.Rows.Add(orderno); 

                Session["orderlist"] = _ol;
                this.dlorder.DataSource = _ol;
                this.dlorder.DataBind();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //end bind session data

    /// <summary>
    /// temporary storage of order numbers 
    /// </summary>
    private IList<Int32> build_order_table()
    {
        IList<Int32> _list = new List<Int32>();
        return _list;
        //DataTable _table = new DataTable();
        //_table.Columns.Add("orderno", typeof(System.Int32));
        //return _table;
    }
    //end return order table

    /// <summary>
    /// callback for repeater displaying order numbers held in session data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxcborder_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

        ///generates random 8 digit number for testing purposes
        //Random _r = new Random();
        //int _num = r.Next(10000000, 100000000);
        //passed via javascript submit_order_callback()
        Int32 _num = wwi_func.vint(e.Parameter.ToString());
        if (_num > 0)
        {
            //add selected order number to datatable
            //DataTable _ol = (DataTable)Session["orderlist"];
            IList<Int32> _ol = (IList<Int32>)Session["orderlist"];
            _ol.Add(_num);
            Session["orderlist"] = _ol; ;
            this.dlorder.DataSource = _ol;
            this.dlorder.DataBind();
        }
    }


    /// <summary>
    /// datalist item command
    /// remove clicked order no
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dlorder_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string _arg = e.CommandArgument.ToString();

        if (e.CommandName == "delete" && !string.IsNullOrEmpty(_arg))
        {
            //DataTable _ol = (DataTable)Session["orderlist"];
            //
            //don't let them delete orderno if count = 1 or we'll nowhere to upload to?
            //if (_ol.Rows.Count > 1)
            //{
            //    //foreach (DataRow _dr in _ol.Rows) can't enumerate like this if we are planning on deleting rows have to count backwards
            //   for (int _i = _ol.Rows.Count - 1; _i >= 0; _i--)
            //    {
            //        string _criteria = _ol.Rows[_i]["orderno"].ToString();   //_dr["criteria"].ToString();
            //        if (_criteria == _arg)
            //        {
            //            _ol.Rows[_i].Delete();  //_dr.Delete(); 
            //        }
            //    }
            //    Session["orderlist"] = _ol;
            //    bind_order_data();
            //}
            IList<Int32> _ol = (IList<Int32>)Session["orderlist"];
            _ol.RemoveAt(_ol.IndexOf(wwi_func.vint(_arg)));

            Session["orderlist"] = _ol;
            bind_order_data();
        }
    }
    //end datalist item command
    //************************************************************

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/tracking/order_tracking.aspx", false);
    }
}

