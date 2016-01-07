using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using DAL.Logistics;
using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for barcode_handler
/// </summary>
public class barcode_handler : IHttpHandler 
{
	public barcode_handler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool IsReusable
    {
        get { return false; }
    }

    /// <summary>
    /// this version outputs gif to web page
    /// ProcessRequest is intrinsic function of hppthandler, do not change the name
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        //for a custom httphandler make sure it's referenced in web.config in httpHandlers
        //<add verb="GET" path="*barcode.gif" type="barcode_handler" validate ="false" />
        //
        if(context.Request["code"] != null){

            string _code = wwi_security.DecryptString(context.Request["code"].ToString(),"publiship"); //code to use
            int _wd = 120; //context.Request["wd"] != null ? wwi_func.vint(context.Request["wd"].ToString()) : 120; //width
            int _ht = 30; //context.Request["ht"] != null ? wwi_func.vint(context.Request["ht"].ToString()) : 30; //height

            Barcode128 _bc = new Barcode128();
            _bc.CodeType = Barcode.CODE128;
            _bc.ChecksumText = true;
            _bc.GenerateChecksum = true;
            _bc.Code = _code;

            //draws directly to web page with no code underneath bar
            //System.Drawing.Bitmap _bm = new System.Drawing.Bitmap(_bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
            //_bm.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

            //draws with actual code underneath bar
            //create bitmap from System.Drawing library, add some height for actual code underneath
            Bitmap _bm = new Bitmap(_wd, _ht + 10);
            //provide this, else the background will be black by default
            Graphics _gr = Graphics.FromImage(_bm);
            
            _gr.PageUnit = GraphicsUnit.Pixel;
            _gr.Clear(Color.White); 
            //draw the barcode
            _gr.DrawImage(_bc.CreateDrawingImage(Color.Black, System.Drawing.Color.White), new Point(0,0));
            //place text underneath - if you want the placement to be dynamic, calculate the point based on size of the image
            System.Drawing.Font _ft = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            SizeF _sz = _gr.MeasureString(_code, _ft); 
            //position text
            _wd = (_wd - (int)_sz.Width) / 2;
            
            StringFormat _sf = new StringFormat();
            _sf.Alignment = StringAlignment.Center;
            _sf.LineAlignment = StringAlignment.Center;

            _gr.DrawString(_code, _ft, SystemBrushes.WindowText,_wd,_ht, _sf);
            //output as gif to web page,  can also save it to external file
            _bm.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }//end if
    }
    //end processrequest
    
}
