<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sys_Msg_Box.aspx.cs" Inherits="Popupcontrol_Sys_Msg_Box" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px Auto; width: 360px">

        <div style="margin: 10px">
        <dx:ASPxLabel ID="dxlblMsgbox" ClientInstanceName="lblMsgbox" runat="server" 
        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" Text="Message here!">
        </dx:ASPxLabel>
        </div>
        
       <div style="margin: 10px">
        
        <dx:ASPxPanel ID="dxpnlbuttons1" ClientInstanceName="pnlbuttons1" runat="server" Visible="false">
        <PanelCollection>
            <dx:PanelContent ID="btns1"> 
            
                <div style="float: left; width: 90px">
                    <dx:ASPxButton ID="dxbtnyes" ClientInstanceName="btnyes"  runat="server" 
                        Text="Yes" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
                </div>
                <div style="float: left; width: 90px; padding-left: 35px">
                    <dx:ASPxButton ID="dxbtnno" ClientInstanceName="btnno"  runat="server" 
                         Text="No" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                         CssPostfix="Office2010Blue" 
                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
                </div>
                 <div style="float: right; width: 90px">
                    <dx:ASPxButton ID="dxbtncancel" ClientInstanceName="btncancel"  runat="server" 
                         Text="Cancel" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                         CssPostfix="Office2010Blue" 
                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
                </div>
                
            </dx:PanelContent>
         </PanelCollection>
         </dx:ASPxPanel>   
       
         <dx:ASPxPanel ID="dxpnlbuttons2" ClientInstanceName="pnlbuttons2" runat="server" Visible="false">
        <PanelCollection>
            <dx:PanelContent ID="btns2">
            
            <div style="width: 90px; padding-left: 130px">
                    <dx:ASPxButton ID="dxbtnok" ClientInstanceName="btnok"  runat="server" 
                         Text="Ok" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                         CssPostfix="Office2010Blue" 
                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
             </div>        
            </dx:PanelContent>
         </PanelCollection>
         </dx:ASPxPanel>  
       </div>
          
    </div>
    </form>
</body>
</html>
