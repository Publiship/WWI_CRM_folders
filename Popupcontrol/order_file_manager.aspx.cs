using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;
using DAL.Pricer;
using ParameterPasser;

public partial class Popupcontrol_order_file_manager : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.dxpnlerr.Visible = false;
        this.dxpnlinfo.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (isLoggedIn())
            {
                //orderid and title id are passed through - remove semi-colon?
                string _orderno = Request.QueryString["or"] != null ? Request.QueryString["or"].Replace(",","").ToString() : "";
                //
                //should we encrypt this?
                //_orderno = !string.IsNullOrEmpty(_orderno) ? wwi_security.EncryptString(_orderno,"publiship") : ""; 
                //

                //role based security
                //companyid-1 internal users can upload/edit/delete
                //other users are view only
                //this DOES NOT Work!
                //UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
                //this.dxfmpod.SettingsPermissions.Role = _thisuser.CompanyId!=-1? "editor": "viewer";

                UserClass _thisuser = (UserClass)HttpContext.Current.Session["user"];
                bool _iseditor = _thisuser.CompanyId == -1 ? true : false; //full rights for internal users only (companyid=-1)

                set_fm_permissions(_iseditor);
                set_visible_folder(_iseditor, _orderno);
                this.dxfmpod.ClientVisible = true;

            }
            else
            {
                this.dxlblerr.Text = "You must be logged in to use the file manager";
                this.dxpnlerr.Visible = true;
                this.dxpnlinfo.Visible = false;
                this.dxpnlfiles.Visible = false;
            }
        }
        catch (Exception ex)
        {
            this.dxlblerr.Text = ex.Message.ToString();
            this.dxpnlerr.Visible = true;
            this.dxpnlinfo.Visible = false;
            this.dxpnlfiles.Visible = false;
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
        this.dxfmpod.SettingsToolbar.ShowDeleteButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowMoveButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowRenameButton = iseditor;
        this.dxfmpod.SettingsToolbar.ShowPath = iseditor;
        //editor
        this.dxfmpod.SettingsEditing.AllowCreate = iseditor;
        this.dxfmpod.SettingsEditing.AllowDelete = iseditor;
        this.dxfmpod.SettingsEditing.AllowMove = iseditor;
        this.dxfmpod.SettingsEditing.AllowRename = iseditor;
        //switch OFF here as we do not want people uploadinmg unless we can save foder id's to ordertable
        this.dxfmpod.SettingsUpload.Enabled = false; //iseditor;
        this.dxfmpod.Styles.FolderContainer.Width = 0; //hide folders

    }
    //end set permissions

    /// <summary>
    /// only internal users can browse folder structure
    /// other users are locked to the specified order number
    /// </summary>
    /// <param name="orderno"></param>
    protected void set_visible_folder(bool iseditor, string orderno)
    {
        int _files = 0;
        bool _dir = false;
        string _root = this.dxfmpod.GetAppRelativeRootFolder().ToString();
        string _fullpath = "~/documents/" + orderno;

        //default folder if viewer restrict to specific folder
        if (!string.IsNullOrEmpty(orderno))
        {
            _dir = folder_exists(_fullpath);
            if (_dir)
            {
                _files = wwi_func.file_count(_fullpath); //check to see if we have any files uploaded
            }
            else
            {
                _dir = iseditor? confirm_create_folder(_fullpath) == orderno: false;
            }
        }
        //end check

        if (_dir && _files > 0) //we must have a valid folder and uploaded files
        {
            if (iseditor)
            {
                //do we let editor level users navigate between folders?
                this.dxfmpod.Settings.RootFolder = _fullpath;
                //using this is really slow as it loads EVERY SINGLE folder name to the display tree! find a better control
                //this.dxfmpod.Settings.InitialFolder = orderno + "\\";
            }
            else
            {
                //access only to this folder
                this.dxfmpod.Settings.RootFolder = _fullpath;
            }
            //show filenanager panel, hide the other panels
            this.dxlblinfo.Text = "";
            this.dxpnlinfo.Visible = false;
            this.dxpnlerr.Visible = false;
            this.dxpnlfiles.Enabled = true;
        }
        else
        {
            if (iseditor)
            {
                this.dxfmpod.Settings.RootFolder = _fullpath;
                this.dxfmpod.Enabled  = true;
            }
            else
            {
                this.dxfmpod.Settings.RootFolder = "~/documents/not found";
                this.dxfmpod.Enabled  = false;
            }
            //hide filemanager and show info
            this.dxlblinfo.Text = "There are no documents online for reference " + orderno;
            this.dxpnlinfo.Visible = true;
        }
    }
    //end set visible folder

    /// <summary>
    /// 071111 create folder automatically when its an editor
    /// editors can create new folders
    /// </summary>
    /// <param name="fullpath"></param>
    protected string confirm_create_folder(string fullpath)
    {
        string _folder = folder_create(fullpath); 
        //this.dxlblinfo.Text = "This folder does not exist, do you weant to create it now?";
        //this.dxpnlinfo.Visible = true;
        //this.dxpnlerr.Visible = false;
        //this.dxpnlfiles.Enabled = false;
        return _folder;
    }
    //end confirm create folder

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
    protected string folder_create(string dirname)
    {
        string _created = null;
        System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(Page.Server.MapPath(dirname));

        if(!_di.Exists)
        {
            try
            {
                _di = System.IO.Directory.CreateDirectory(Page.Server.MapPath(dirname));
                _created = _di.Name.ToString();
            }
            catch(Exception ex)
            {
                _created = ex.Message.ToString();
                
            }
        }

        return _created;
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
}
