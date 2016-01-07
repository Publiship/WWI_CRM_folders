using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using DAL.Logistics;
using DAL.Pricer;
using SubSonic;

/// <summary>
/// Summary description for wwi_spreadsheet
/// </summary>
public class wwi_file
{
    public wwi_file()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region pricer files
    public static string get_latest_pricer(string dirname)
    {
        string _filename = null;
        DateTime _cdt = DateTime.Now.AddYears(-10); //-10 year ahould be plenty for comparison

        try
        {
            System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(dirname));

            System.IO.FileInfo[] _files = _di.GetFiles("*.xls"); //all excel sheets  

            foreach (System.IO.FileInfo _f in _files)
            {
                DateTime _fdt = _f.CreationTime;
                if (_f.Name.ToLower().StartsWith("officepricer", true, System.Globalization.CultureInfo.CurrentCulture) && _fdt.Ticks > _cdt.Ticks)
                {
                    _cdt = _fdt;
                    _filename = _f.Name.ToString();
                }
            }
            // Get only subdirectories that are excel
            //System.IO.DirectoryInfo[] _dirs = _di.GetDirectories("*.xls*");
            //Console.WriteLine("Number of directories with a p: {0}", dirs.Length);

            // Count all the files in each subdirectory that contain the letter "e."
            //foreach (DirectoryInfo diNext in dirs)
            //{
            //    Console.WriteLine("The number of files in {0} with an e is {1}", diNext,
            //        diNext.GetFiles("*e*").Length);
            //}

        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(_filename)) { _filename = ex.Message.ToString(); }
        }

        return _filename;
    }
    //overload get latest pricer including compny id
    //check for pricers pre-fixed with company id instead of "officepricer"
    public static string get_latest_pricer(string dirname, string companygroup)
    {
        string _filename = null;
        DateTime _cdt = DateTime.Now.AddYears(-10); //-10 year ahould be plenty for comparison

        try
        {
            System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(dirname));

            System.IO.FileInfo[] _files = _di.GetFiles(companygroup + "*.xls"); //all excel sheets prefixed by companygroup string

            foreach (System.IO.FileInfo _f in _files)
            {
                DateTime _fdt = _f.CreationTime;
                if (_f.Name.ToLower().StartsWith(companygroup, true, System.Globalization.CultureInfo.CurrentCulture) && _fdt.Ticks > _cdt.Ticks)
                {
                    _cdt = _fdt;
                    _filename = _f.Name.ToString();
                }
            }
            // Get only subdirectories that are excel
            //System.IO.DirectoryInfo[] _dirs = _di.GetDirectories("*.xls*");
            //Console.WriteLine("Number of directories with a p: {0}", dirs.Length);

            // Count all the files in each subdirectory that contain the letter "e."
            //foreach (DirectoryInfo diNext in dirs)
            //{
            //    Console.WriteLine("The number of files in {0} with an e is {1}", diNext,
            //        diNext.GetFiles("*e*").Length);
            //}

        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(_filename)) { _filename = ex.Message.ToString(); }
        }

        return _filename;
    }
    //end get latest pricer
    #endregion

    #region system IO file manager
    /// <summary>
    /// return list of all files in named folder
    /// list has better performance than arraylist, because in arraylist boxing occurs, which is a way the runtime turns a value type into an object
    /// </summary>
    /// <param name="dirname">folder to search in</param>
    /// <param name="companygroup">companygroup to restrict pricer list to ones specific to the company or 0 is internal log in</param>
    /// <returns></returns>
    public static List<string> get_files(string dirname, string searchpattern)
    {
        List<string> _l = new List<string>();

        System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(dirname));

        System.IO.FileInfo[] _files = _di.GetFiles(searchpattern); //e.g *.xls all excel sheets prefixed by companygroup string

        foreach (System.IO.FileInfo _f in _files)
        {
            _l.Add(_f.Name.ToString());
        }
        return _l;
    }
    //when group is known
    public static List<string>get_files(string dirname,string searchpattern, int companygroup)
    {
        List<string> _l = new List<string>();

        System.IO.DirectoryInfo _di = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(dirname));

        System.IO.FileInfo[] _files = _di.GetFiles(companygroup + searchpattern); //all excel sheets prefixed by companygroup string

        foreach (System.IO.FileInfo _f in _files)
        {
            if (_f.Name.ToLower().StartsWith(companygroup.ToString(), true, System.Globalization.CultureInfo.CurrentCulture))
            {
                _l.Add(_f.Name.ToString());
            }
        }
        return _l;
    }
    //end get pricers
    /// <summary>
    /// make a copy of a file using file system object
    /// </summary>
    /// <param name="filein">string full path to file to copy</param>
    /// <param name="fileout">string full path of file to create</param>
    /// <returns></returns>
    public static void fso_copy_file(string filein, string fileout)
    {
        string _result = null;

        try
        {
            if (!string.IsNullOrEmpty(filein) && !string.IsNullOrEmpty(fileout))
            {
                string _source = HttpContext.Current.Server.MapPath(filein);
                string _dest = HttpContext.Current.Server.MapPath(fileout);
                System.IO.File.Copy(_source, _dest);
            }
        }
        catch (Exception ex)
        {
            _result = ex.Message.ToString();
        }
    }
    //end fso copy file

    public static void fso_kill_file(string filename)
    {
        string _result = null;

        try
        {
            if (!string.IsNullOrEmpty(filename))
            {
                string _source = HttpContext.Current.Server.MapPath(filename);
                if (System.IO.File.Exists(_source)) { System.IO.File.Delete(_source); }
            }
        }
        catch (Exception ex)
        {
            _result = ex.Message.ToString();
        }
    }
    //end fso kill file

    /// <summary>
    /// return datatable of item names/values
    /// </summary>
    /// <param name="dir">location of excel worknook</param>
    /// <param name="comapnygroup">identifies worksheet to use depending on company</param>
    /// <param name="xlsheet">worksheet to use within workbbok</param>
    /// <param name="filter">used to determine which range to use</param>
    /// <returns>indexed datatable of items</returns>
    public static DataTable getfromfile(string dir, string companygroup, string xlsheet, string xlstart, string xlend)
    {
        //*****
        //100212 check against company id, if no file just use "officepricer" prefix
        //namecustomcontroller _name = new namecustomcontroller();
        //string _cg = _name.get_company_group(companyid);
        string _source = companygroup != "0" ? get_latest_pricer(dir, companygroup) : get_latest_pricer(dir); //find latest upload of pricer 
        //****
        string _copy = companygroup.ToString() + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".xls";
        fso_copy_file(dir + _source, dir + _copy);

        SpreadsheetGear.IWorkbook _wb = SpreadsheetGear.Factory.GetWorkbook(HttpContext.Current.Server.MapPath(dir + _copy));

        //disable password protection for now
        _wb.Unprotect("Trueblue");
        //is this necessary in web apps - seems primarily for threading in winforms
        //_wb.WorkbookSet.GetLock(); //acquire lock

        //get range
        SpreadsheetGear.IRange _range = _wb.Worksheets[xlsheet].Range[xlstart + ":" + xlend]; 
        //_range.Replace("", "*", SpreadsheetGear.LookAt.Whole, SpreadsheetGear.SearchOrder.ByColumns, false);
        
        DataTable _dt = new DataTable(); 
        //_dt =_range.GetDataTable(SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders);
        
        //add a datatabe name and a column name so e.g. can data bind to combo
        _dt.Columns.Add("item_index", typeof(int));
        _dt.Columns.Add("item", typeof(string));
        _dt.TableName = "item_table";
        _dt.PrimaryKey = new DataColumn[] { _dt.Columns["Item_index"] };

        //_dt.Columns[0].ColumnName = "item";
        int _rowx = 0;
        for (int _cx = 0; _cx < _range.ColumnCount; _cx++)
        {
            for (int _ix = 0; _ix < _range.RowCount; _ix++)
            {
                DataRow _dr = _dt.NewRow();
                string _s = _range.Rows[_ix, _cx].Value != null ? _range.Rows[_ix, _cx].Value.ToString() : null;
                //160512 older spreadsheets sometimes have numeric values embedded in the data lists
                //check and do not include?
                //if (!string.IsNullOrEmpty(_s) && wwi_func.vint(_s) == 0)
                if (!string.IsNullOrEmpty(_s))
                {
                    _dr["item"] = _s;
                    _dr["item_index"] = _rowx;
                    _dt.Rows.Add(_dr);
                    _rowx++;
                }

            }
        }
                
        _wb.Protect("Trueblue", true, true);
        _wb.Close();
        //_wb.WorkbookSet.ReleaseLock();
        
  
        //delete teporary copy
        fso_kill_file(dir + _copy);

        return _dt;
    }
    //end getfromfile
    /// <summary>
    /// return datatable of origin names when there is a start range but no end range
    /// iterate through from start cell and end when an empty cell is reached
    /// </summary>
    /// <param name="dir">location of excel worknook</param>
    /// <param name="comapnygroup">identifies worksheet to use depending on company</param>
    /// <param name="xlsheet">worksheet to use within workbbok</param>
    /// <param name="startrange">start cell on worksheet colunm/row value e.g. A1 or A1:A50</param>
     /// <returns>indexed datatable of items</returns>
    public static DataTable getfromfile(string dir, string companygroup, string xlsheet, string xlstart)
    {
        //*****
        //100212 check against company id, if no file just use "officepricer" prefix
        //namecustomcontroller _name = new namecustomcontroller();
        //string _cg = _name.get_company_group(companyid);
        string _source = companygroup != "0" ? get_latest_pricer(dir, companygroup) : get_latest_pricer(dir); //find latest upload of pricer 
        //****
        string _copy = companygroup.ToString() + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + ".xls";

        fso_copy_file(dir + _source, dir + _copy);

        SpreadsheetGear.IWorkbook _wb = SpreadsheetGear.Factory.GetWorkbook(HttpContext.Current.Server.MapPath(dir + _copy));

        //disable password protection for now
        _wb.Unprotect("Trueblue");
        //is this necessary in web apps - seems primarily for threading in winforms
        //_wb.WorkbookSet.GetLock(); //acquire lock

        //get range
        SpreadsheetGear.IRange _range = _wb.Worksheets[xlsheet].Range[xlstart];
        //_range.Replace("", "*", SpreadsheetGear.LookAt.Whole, SpreadsheetGear.SearchOrder.ByColumns, false);

        DataTable _dt = new DataTable();
        //_dt =_range.GetDataTable(SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders);

        //add a datatabe name and a column name so e.g. can data bind to combo
        _dt.Columns.Add("item_index", typeof(int));
        _dt.Columns.Add("item", typeof(string));
        _dt.TableName = "item_table";
        _dt.PrimaryKey = new DataColumn[] { _dt.Columns["Item_index"] };

        //_dt.Columns[0].ColumnName = "item";
        Boolean _end = false;
        int _ix = 0;
        while(_end == false)
        {
            DataRow _dr = _dt.NewRow();
            string _s = _range.Rows[_ix, 0].Value != null ? _range.Rows[_ix, 0].Value.ToString() : null;
            if (!string.IsNullOrEmpty(_s))
            {
                _ix +=  1;
                _dr["item"] = _s;
                _dr["item_index"] = _ix;
                _dt.Rows.Add(_dr);
            }
            else
            {
                _end = true;
            }
        }

        _wb.Protect("Trueblue", true, true);
        _wb.Close();
        //_wb.WorkbookSet.ReleaseLock();


        //delete teporary copy
        fso_kill_file(dir + _copy);

        return _dt;
    }
    //end getfromfile
#endregion

    //**********************************
    #region temporary file manager

    /// <summary>
    /// returns error message on fail else retrns tmp file name
    /// </summary>
    /// <returns>name of tmp file</returns>
    public static string CreateTmpFile()
    {
        string fileName = string.Empty;

        try
        {
            // Get the full name of the newly created Temporary file. 
            // Note that the GetTempFileName() method actually creates
            // a 0-byte file and returns the name of the created file.
            fileName = Path.GetTempFileName();

            // Craete a FileInfo object to set the file's attributes
            FileInfo fileInfo = new FileInfo(fileName);

            // Set the Attribute property of this file to Temporary. 
            // Although this is not completely necessary, the .NET Framework is able 
            // to optimize the use of Temporary files by keeping them cached in memory.
            fileInfo.Attributes = FileAttributes.Temporary;

            //Console.WriteLine("TEMP file created at: " + fileName);
        }
        catch (Exception ex)
        {
            fileName = "ERROR " + ex.Message.ToString(); 
            //Console.WriteLine("Unable to create TEMP file or set its attributes: " + ex.Message);
        }

        return fileName;
    }

    /// <summary>
    /// </summary>
    /// <param name="tmpFile">tmp file name</param>
    /// <param name="tmpContent">file content</param>
    /// <returns>returns error message on fail else returns empty string</returns>
    public static string UpdateTmpFile(string tmpFile, string tmpContent)
    {
        string result = null;

        try
        {
            // Write to the temp file.
            StreamWriter streamWriter = File.AppendText(tmpFile);
            streamWriter.WriteLine(tmpContent);
            streamWriter.Flush();
            streamWriter.Close();

            //Console.WriteLine("TEMP file updated.");
        }
        catch (Exception ex)
        {
            result = "ERROR " + ex.Message.ToString(); 
            //Console.WriteLine("Error writing to TEMP file: " + ex.Message);
        }

        return result;
    }

    /// <summary>
    /// read tmp file 
    /// </summary>
    /// <param name="tmpFile">tmp file name</param>
    /// <returns>error message on fail else returns contents</returns> 
    public static string ReadTmpFile(string tmpFile)
    {
        string result = null;

        try
        {
            // Read from the temp file.
            StreamReader myReader = File.OpenText(tmpFile);
            //Console.WriteLine("TEMP file contents: " + myReader.ReadToEnd());
            result = myReader.ReadToEnd(); 
            myReader.Close();
        }
        catch (Exception ex)
        {
            result = "ERROR " + ex.Message.ToString(); 
            //Console.WriteLine("Error reading TEMP file: " + ex.Message);
        }

        return result;
    }

    /// <summary>
    /// delete tmp file
    /// </summary>
    /// <param name="tmpFile">name of tmp file</param>
    /// <returns>returns error on fail else returns null string</returns> 
    public static string DeleteTmpFile(string tmpFile)
    {
        string result = null;
        try
        {
            // Delete the temp file (if it exists)
            if (File.Exists(tmpFile))
            {
                File.Delete(tmpFile);
                //Console.WriteLine("TEMP file deleted.");
            }
        }
        catch (Exception ex)
        {
            result = "ERROR " + ex.Message.ToString(); 
            //Console.WriteLine("Error deleteing TEMP file: " + ex.Message);
        }

        return result;
    }
    #endregion

}
