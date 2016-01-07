using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;
using SubSonic;
using DAL.Logistics;
using DAL.Pricer;
using iTextSharp.text;
using iTextSharp.text.pdf;
/// <summary>
/// Summary description for itextsharp_out
/// pdf output jusing itextsharp library
/// </summary>
public class itextsharp_out
{
	public itextsharp_out()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// publiship advance labels
    /// </summary>
    /// <param name="orderid"></param>
    /// <returns></returns>
    public static string advance_labels(Int32 orderid)
    {
        string _msg = ""; //returns error message or nothing if no error

        Query _q = new Query(ViewAdvanceDelivery.Schema);
        _q.WHERE("OrderID", Comparison.Equals, orderid);

        DAL.Logistics.ViewAdvanceDeliveryCollection _v = new ViewAdvanceDeliveryCollection();
        _v.LoadAndCloseReader(_q.ExecuteReader());

        if (_v != null && _v.Count > 0)
        {
            Document _doc = new Document();

            try
            {

                System.IO.MemoryStream _mem = new System.IO.MemoryStream();
                PdfWriter _pdf = PdfWriter.GetInstance(_doc, _mem);
                Chunk _dl = new Chunk(new iTextSharp.text.pdf.draw.DottedLineSeparator());

                _doc.Open();

                int _cartons = _v.Count;
                string _txt = "";
                int _br = 0;

                for (int _ix = 0; _ix < _cartons; _ix++)
                {
                    PdfPTable _tbl = new PdfPTable(5);
                    _tbl.TotalWidth = _doc.PageSize.Width - _doc.LeftMargin - _doc.RightMargin;
                    
                    ////paragraphs is one way of positioning tables on page by adding spacing but inconsistent page breaks 
                    ////don't work for us. use absolute positioning instead
                    //Paragraph _par = new Paragraph();
                    //_par.SpacingBefore = 150f;
                    
                    //format logo
                    iTextSharp.text.Image _img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/PublishipAdvanceGif2.gif"));
                    _img.ScalePercent(24f); //scale to 300dpi for print quality?

                    //top row publiship advance logo and order id
                    //publiship advance logo and order id
                    PdfPCell _c0 = new PdfPCell(_img);
                    _c0.Padding = 3;
                    _c0.Colspan = 3;
                    _c0.BorderWidthLeft = 0;
                    _c0.BorderWidthTop = 0;
                    _tbl.AddCell(_c0);

                    PdfPCell _c1 = new PdfPCell(new Phrase("CONSIGNMENT REF:" + Environment.NewLine + _v[_ix].OrderID.ToString()));
                    _c1.Padding = 3;
                    _c1.Colspan = 2;
                    _c1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    _tbl.AddCell(_c1);
                    //end top row

                    //delivery address 
                    _txt = !string.IsNullOrEmpty(_v[_ix].DeliveryAddress) ? _v[_ix].DeliveryAddress.ToString() : "";
                    _txt += !string.IsNullOrEmpty(_v[_ix].DestinationCountry) ? Environment.NewLine + _v[_ix].DestinationCountry.ToString() : "";
                    PdfPCell _c2 = new PdfPCell(new Phrase("DELIVERY TO:")); //"delivery to: " + Environment.Newline + address.tostring();
                    _c2.MinimumHeight = 100;
                    _c2.Padding = 3;
                    _c2.Colspan = 1;
                    _tbl.AddCell(_c2);

                    _c2 = new PdfPCell(new Phrase(_txt));
                    _c2.MinimumHeight = 100;
                    _c2.Padding = 3;
                    _c2.Colspan = 4;
                    _tbl.AddCell(_c2);
                    //end address

                    //fao 
                    _txt = !string.IsNullOrEmpty(_v[_ix].Fao) ? _v[_ix].Fao.ToString() : "";
                    PdfPCell _c3 = new PdfPCell(new Phrase("ATTENTION:"));
                    _c3.Padding = 3;
                    _c3.Colspan = 1;
                    _tbl.AddCell(_c3);

                    _c3 = new PdfPCell(new Phrase(_txt));
                    _c3.Padding = 3;
                    _c3.Colspan = 4;
                    _tbl.AddCell(_c3);
                    //end fao

                    //title 
                    _txt = !string.IsNullOrEmpty(_v[_ix].Title) ? _v[_ix].Title.ToString() : "";
                    PdfPCell _c4 = new PdfPCell(new Phrase("TITLE:"));
                    _c4.Padding = 3;
                    _c4.Colspan = 1;
                    _tbl.AddCell(_c4);

                    _c4 = new PdfPCell(new Phrase(_txt));
                    _c4.Padding = 3;
                    _c4.Colspan = 4;
                    _tbl.AddCell(_c4);
                    //end title

                    //bottom row
                    //sender - check for companyid, if -1 it's a publiship user so use username/office name otherwise use contact name/company name
                    if (_v[_ix].CompanyID != -1)
                    { _txt = !string.IsNullOrEmpty(_v[_ix].CompanyName) ? _v[_ix].CompanyName.ToString() : ""; }
                    else
                    { _txt = !string.IsNullOrEmpty(_v[_ix].OfficeName) ? _v[_ix].OfficeName.ToString() : ""; }
                    PdfPCell _c5 = new PdfPCell(new Phrase("SENDER:"));
                    _c5.Padding = 3;
                    _c5.Colspan = 1;
                    _tbl.AddCell(_c5);

                    _c5 = new PdfPCell(new Phrase(_txt));
                    _c5.Padding = 3;
                    _c5.Colspan = 3;
                    _tbl.AddCell(_c5);

                    PdfPCell _c6 = new PdfPCell(new Phrase("CARTON:" + Environment.NewLine + (_ix + 1).ToString() + " / " + _cartons.ToString()));
                    _c6.Padding = 3;
                    _c6.Colspan = 1;
                    _c6.HorizontalAlignment = Element.ALIGN_RIGHT;
                    _tbl.AddCell(_c6);
                    //end bottom row 

                    //dotted line above 2nd table on page
                    //float[1] = top of page, [0] = bottom of page -.5 inch (pdfs print at 72 dpi so 36 should be .5inch)
                    //we can then use modulus function to detemine which absolute y position to drop table
                    //mod 2 should return 0 for every second table
                    float[] _ypos = new float[] { _doc.BottomMargin + _tbl.TotalHeight + 36, _doc.PageSize.Height - _doc.TopMargin };

                    //060612 use _cpy value instead as we need 2 copies of each label
                    //_br = (_ix + 1) % 2;

                    //2 copies of each label
                    for (int _cpy = 0; _cpy <= 1; _cpy++)
                    {
                        ////only 2 labels per sheet of A4 Make the page break 
                        _br = (_cpy + 1) % 2;

                        _tbl.WriteSelectedRows(0, _tbl.Rows.Count, _doc.LeftMargin, _ypos[_br], _pdf.DirectContent);
                        _tbl.SplitLate = false;

                        //add table to paragraph
                        //_par.Add(_tbl);

                        if (_br == 0)
                        {
                            _doc.NewPage();
                        }
                        else //draw a line between labels on same page
                        {

                            _pdf.DirectContent.SetColorStroke(BaseColor.LIGHT_GRAY);
                            _pdf.DirectContent.MoveTo(_doc.LeftMargin, _doc.PageSize.Height / 2);
                            _pdf.DirectContent.LineTo(_doc.PageSize.Width - _doc.RightMargin, _doc.PageSize.Height / 2);
                            _pdf.DirectContent.Stroke();
                        }
                    }
                    //add paragraph to doc
                    //_doc.Add(_par);

                }

                _doc.Close(); //pushes to output stream 

                //direct output client side
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + orderid + ".pdf");
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.BinaryWrite(_mem.ToArray());
                //this also works
                //Response.Clear();
                //Response.OutputStream.Write(_mem.GetBuffer(), 0, _mem.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.End();
            }
            catch (DocumentException dex)
            {
                _msg = dex.Message.ToString(); 
                //throw (dex);
                //this.dxlblerr.Text = dex.Message.ToString();
                //this.dxpageorder.ActiveTabIndex = 4; //error page
            }
            catch (IOException ioex)
            {
                _msg = ioex.Message.ToString();
                //throw (ioex);
                //this.dxlblerr.Text = ioex.Message.ToString();
                //this.dxpageorder.ActiveTabIndex = 4; //error page
            }
            finally
            {
                _doc.Close();
            }
        }
        return _msg;
    }
    //end advance labels

    /// <summary>
    /// or use order_output.aspx
    /// </summary>
    /// <param name="ordernumber"></param>
    /// <returns></returns>
    public static string order_sheet(int ordernumber)
    {
        string _msg = "";
       
        //for testing 
        ordernumber = 1049040;
        //
        Document _doc = new Document();

        try
        {
            //get datacontext
            linq.linq_order_sheet_udfDataContext _linq = new linq.linq_order_sheet_udfDataContext();

            //return iqueryable order by order number
            //IQueryable<order_sheetResult> _order = _linq.order_sheet(1049040);
            //details for 1st order the linq datacontext only returns 1 record by order number
            linq.order_sheetResult _o = _linq.order_sheet(ordernumber).First<linq.order_sheetResult>();

            System.IO.MemoryStream _mem = new System.IO.MemoryStream();
            PdfWriter _pdf = PdfWriter.GetInstance(_doc, _mem);

            _doc.Open();
            _doc.NewPage(); 
            PdfPTable _tbl1 = new PdfPTable(6);

            //order number large text top left
            PdfPCell _c1 = new PdfPCell(new Phrase(_o.OrderNumber.ToString()));
            _c1.Padding = 3;
            _c1.Colspan = 4;
            _c1.Rowspan = 2;
            _tbl1.AddCell(_c1);

            //customer top right then customers ref and ex works date underneath
            PdfPCell _c2 = new PdfPCell(new Phrase(_o.CustomerName));
            _c2.Padding = 3;
            _c2.Colspan = 2;
            _c2.Rowspan = 1;
            _tbl1.AddCell(_c2);

            PdfPCell _c3 = new PdfPCell(new Phrase("Customers Ref:"));
            _c3.Padding = 3;
            _c3.Colspan = 1;
            _c3.Rowspan = 1;
            _tbl1.AddCell(_c3);

            PdfPCell _c4 = new PdfPCell(new Phrase(_o.CustomersRef));
            _c4.Padding = 3;
            _c4.Colspan = 1;
            _c4.Rowspan = 1;
            _tbl1.AddCell(_c4);

            PdfPCell _c5 = new PdfPCell(new Phrase("Ex Works:"));
            _c5.Padding = 3;
            _c5.Colspan = 1;
            _c5.Rowspan = 1;
            _tbl1.AddCell(_c5);

            PdfPCell _c6 = new PdfPCell(new Phrase(_o.ExWorksDate.ToString()));
            _c5.Padding = 3;
            _c5.Colspan = 1;
            _c5.Rowspan = 1;
            _tbl1.AddCell(_c6);

            _tbl1.WriteSelectedRows(0, _tbl1.Rows.Count, _doc.LeftMargin , _doc.PageSize.Height - _doc.TopMargin, _pdf.DirectContent); 
            //end of top section

            //new table for origin & destination details
            PdfPTable _tbl2 = new PdfPTable(6); 
            
            //row 1 origin point + customer contact
            _c6 = new PdfPCell(new Phrase("Origin Point:"));
            _c6.Padding = 3;
            _c6.Colspan = 1;
            _c6.Rowspan = 1;
            _tbl2.AddCell(_c6);

            PdfPCell _c7 = new PdfPCell(new Phrase(_o.OriginPort));
            _c7.Padding = 3;
            _c7.Colspan = 2;
            _c7.Rowspan = 1;
            _tbl2.AddCell(_c7);

            PdfPCell _c8 = new PdfPCell(new Phrase("Customer Contact:"));
            _c8.Padding = 3;
            _c8.Colspan = 1;
            _c8.Rowspan = 1;
            _tbl2.AddCell(_c8);

            PdfPCell _c9 = new PdfPCell(new Phrase(_o.ContactName));
            _c9.Padding = 3;
            _c9.Colspan = 2;
            _c9.Rowspan = 1;
            _tbl2.AddCell(_c9);

            //row 2 origin port and order controller
            PdfPCell _c10 = new PdfPCell(new Phrase("Origin port:"));
            _c10.Padding = 3;
            _c10.Colspan = 1;
            _c10.Rowspan = 1;
            _tbl2.AddCell(_c10);

            PdfPCell _c11 = new PdfPCell(new Phrase(_o.OriginPort));
            _c11.Padding = 3;
            _c11.Colspan = 2;
            _c11.Rowspan = 1;
            _tbl2.AddCell(_c11);

            PdfPCell _c = new PdfPCell(new Phrase("Order Controller:"));
            _c.Padding = 3;
            _c.Colspan = 1;
            _c.Rowspan = 1;
            _tbl2.AddCell(_c);

            _tbl2.WriteSelectedRows(0, _tbl2.Rows.Count, 0, 0, _pdf.DirectContent); 

            //MultiColumnText columns = new MultiColumnText();
            //columns.AddSimpleColumn(36f, 336f);
            //columns.AddSimpleColumn(360f, _doc.PageSize.Width - 36f);
            
            _doc.Close(); //pushes to output stream 

            //direct output client side 
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + ordernumber + ".pdf");
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.BinaryWrite(_mem.ToArray());
        }
        catch (DocumentException dex)
        {
            _msg = dex.Message.ToString();
            //throw (dex);
            //this.dxlblerr.Text = dex.Message.ToString();
            //this.dxpageorder.ActiveTabIndex = 4; //error page
        }
        catch (IOException ioex)
        {
            _msg = ioex.Message.ToString();
            //throw (ioex);
            //this.dxlblerr.Text = ioex.Message.ToString();
            //this.dxpageorder.ActiveTabIndex = 4; //error page
        }
        finally
        {
            _doc.Close();
        }

        return _msg;
    }
    //end order sheet

    #region functions
    public static string get_doctype(object original)
    {
        string _doctype = "";
        
        switch (wwi_func.vint(original.ToString())) {
            case 1:
                {
                    _doctype = "Originals";
                    break;
                }
            case 2:
                {
                    _doctype = "Copies";
                    break;
                }
            default:
                {
                    _doctype = "Send By Email";
                    break;
                }
        }

        return _doctype;
    }

    public static string get_mailtype(object sendbyemail)
    {
        string _mailtype = "";

        if (wwi_func.vint(sendbyemail.ToString()) == 1)
        {
             _mailtype = "Send By Email";
        }
       
        return _mailtype;
    }

    public static int check_original(object original)
    {
        int _check = 0;

        if (wwi_func.vint(original.ToString()) == 1)
        {
            _check = 1;
        }

        return _check;
    }
    #endregion
}
