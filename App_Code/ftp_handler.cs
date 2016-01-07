using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ftp_handler
/// </summary>
public class ftp_handler
{
    private string host = null;
    private string user = null;
    private string pass = null;
    private FtpWebRequest ftpRequest = null;
    private FtpWebResponse ftpResponse = null;
    private Stream ftpStream = null;
    private int bufferSize = 2048;

    /* Construct Object */
	public ftp_handler(string hostIP, string userName, string password) 
	{
		//
		// TODO: Add constructor logic here
		//
        host = hostIP; 
        user = userName; 
        pass = password;
	}

    /* Download File */
    public string download(string remoteFile, string localFile)
    {
        string _result = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Get the FTP Server's Response Stream */
            ftpStream = ftpResponse.GetResponseStream();
            /* Open a File Stream to Write the Downloaded File */
            FileStream _localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] _byteBuffer = new byte[bufferSize];
            int _bytesRead = ftpStream.Read(_byteBuffer, 0, bufferSize);
            /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
            //try
            //{
            while (_bytesRead > 0)
            {
                _localFileStream.Write(_byteBuffer, 0, _bytesRead);
                _bytesRead = ftpStream.Read(_byteBuffer, 0, bufferSize);
            }
            //}
            //catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            _localFileStream.Close();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString());
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* Upload File */
    public string upload(string remoteFile, string localFile)
    {
        string _result = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpRequest.GetRequestStream();
            /* Open a File Stream to Read the File for Upload */
            /* use openorcreate as just open will overwrite the file if it already exists */
            FileStream _localFileStream = new FileStream(localFile, FileMode.OpenOrCreate);
            /* Buffer for the Downloaded Data */
            byte[] _byteBuffer = new byte[bufferSize];
            int _bytesSent = _localFileStream.Read(_byteBuffer, 0, bufferSize);
            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
            //try
            //{
            while (_bytesSent != 0)
            {
                ftpStream.Write(_byteBuffer, 0, _bytesSent);
                _bytesSent = _localFileStream.Read(_byteBuffer, 0, bufferSize);
            }
            //}
            //catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            
            _localFileStream.Close();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString()); 
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            ftpStream.Flush();
            ftpStream.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* Delete File */
    public string delete(string deleteFile)
    {
        string _result = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
           
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString()); 
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* Rename File */
    public string rename(string currentFileNameAndPath, string newFileName)
    {
        string _result = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            /* Rename the File */
            ftpRequest.RenameTo = newFileName;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString());
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* Create a New Directory on the FTP Server */
    public string createDirectory(string newDirectory)
    {
        string _result = null;
        
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            
        }
        catch (Exception ex) {
            //Console.WriteLine(ex.ToString()); 
            _result = ex.Message.ToString();
        }
        finally 
        {
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* Get the Date/Time a File was Created */
    public string getFileCreatedDateTime(string fileName)
    {
        string _result = null;
        string _fileInfo = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader _ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            /* Read the Full Response Stream */
            _fileInfo = _ftpReader.ReadToEnd(); 
            _ftpReader.Close();
            /* Return File Created Date Time */
            _result = _fileInfo;
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString());
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            ftpStream.Flush();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        /* Return an Empty string Array if an Exception Occurs */
        return _result;
    }

    /* Get the Size of a File */
    public string getFileSize(string fileName)
    {
        string _result = null;
        string _fileInfo = null;

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader _ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */

            /* Read the Full Response Stream */
            while (_ftpReader.Peek() != -1)
            {
                _fileInfo = _ftpReader.ReadToEnd();
            }
            _ftpReader.Close();

            /* Return File Size */
            _result = _fileInfo;
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString()); 
            _result = ex.Message.ToString();
        }
        finally
        {
            /* Resource Cleanup */
            
            ftpStream.Flush();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        return _result;
    }

    /* List Directory Contents File/Folder Name Only */
    public List<string> directoryListSimple(string directory)
    {
        List<string> _directoryList = null;
        string _chr = "|";

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();

            /* Get the FTP Server's Response Stream */
            StreamReader _ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string _directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            while (_ftpReader.Peek() != -1)
            {
                _directoryRaw += _ftpReader.ReadLine() + _chr;
            }
            _ftpReader.Close();
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            _directoryList = _directoryRaw.Split(_chr.ToCharArray()).ToList(); 
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString()); 
            _directoryList.Add(ex.Message.ToString()); 
        }
        finally
        {
            /* Resource Cleanup */
            ftpStream.Flush();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        /* Return an Empty string Array if an Exception Occurs */
        return _directoryList;
    }

    /* List Directory Contents in Detail (Name, Size, Created, etc.) */
    public List<string>  directoryListDetailed(string directory)
    {
        List<string> _directoryList = null;
        string _chr = "|";

        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();

            /* Get the FTP Server's Response Stream */
            StreamReader _ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string _directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            while (_ftpReader.Peek() != -1)
            {
                _directoryRaw += _ftpReader.ReadLine() + _chr;
            }

            _ftpReader.Close();
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            _directoryList = _directoryRaw.Split(_chr.ToCharArray()).ToList();
           
        }
        catch (Exception ex) { 
            //Console.WriteLine(ex.ToString());
            _directoryList.Add(ex.Message.ToString());   
        }
        finally
        {
            /* Resource Cleanup */
            ftpStream.Flush();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        /* Return an Empty string Array if an Exception Occurs */
        return _directoryList;
    }
}
