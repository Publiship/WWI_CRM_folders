<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sys_Not_Available.aspx.cs" Inherits="Popupcontrol_Sys_Not_Available" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>This option is not yet available</title>
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
                         <asp:Panel ID="pnlinfo" runat="server" Width="300px" Height="150px">
                            <div>
                            <div class="info"> 
                            <asp:Label ID="lblinfo" runat="server" Text="This option is not yet available"></asp:Label>
                            </div>
                   
                            <div style="width:280px; height:60px; padding: 10px"> 
                                 <div style="float: left; width: 25%"></div> 
                                 <div style="float: right; width: 75%">
                                    <dx:ASPxButton ID="dxbtnmsg" runat="server"  
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Ok" 
                                        Width="100px" CausesValidation="False" 
                                        AutoPostBack="False">
                                        <ClientSideEvents 
                                            Click="function(s, e) {
	                                        window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('sysna'));}" />
                                     </dx:ASPxButton>
                                   </div> 
                                   </div>
                            </div> 
                         </asp:Panel> 
    </form>
</body>
</html>
