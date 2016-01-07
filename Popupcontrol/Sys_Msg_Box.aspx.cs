using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Logistics;

public partial class Popupcontrol_Sys_Msg_Box : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get flags for message box
        //1st 2 elements
        //0 = Title
        //1 = Message
        
        //3rd element value for button options
        //0 = okbutton
        //1 = yesno buttons
        //2 = yesnocancel buttons
        
        //4th element value for image options
        //0 = information image
        //1 = question mark image
        //2 = exlclamation mark image
        NameValueCollection _nv = HttpUtility.ParseQueryString(Request.QueryString.ToString());

        //enumerate namevalue pairs and set obects
        for (int ix = 0; ix < _nv.Count; ix++)
        {
            string _n = _nv.GetKey(ix); //name
            string _v = _nv.Get(ix); //value

            switch (_n)
            {
                case "ttl":
                    {
                        this.Title = _v; //title 
                        break;
                    }
                case "msg":
                    {
                        this.dxlblMsgbox.Text = _v; //msg 
                        break;
                    }
                case "btn":
                    {
                        set_buttons(wwi_func.vint(_v));
                        break;
                    }
                case "img":
                    {
                        set_image(wwi_func.vint(_v));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //end case
        }//end enumerate namevalues
        
    }
    //end page load

    protected void set_buttons(int buttonoptions)
    {
        if (buttonoptions == 0) //show yesnocancel
        {
            this.dxpnlbuttons1.Visible = true;
            this.dxpnlbuttons2.Visible = false;
        }
        else //show ok
        {
            this.dxpnlbuttons1.Visible = false;
            this.dxpnlbuttons2.Visible = true;
        }
    }

    protected void set_image(int imageoptions)
    {

    }

}
