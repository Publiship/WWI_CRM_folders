<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sys_Login.aspx.cs" Inherits="Sys_Login" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
    
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Publiship Online v1.0</title>
    <link rel="stylesheet" type="text/css" href="~/App_Themes/style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="~/App_Themes/custom.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="~/App_Themes/typography.css" media="screen" />
 
    <script type="text/javascript">
        function showReminderWindow() {
            var win = window.parent.popWindows.GetWindowByName('reminderform');
            window.parent.popWindows.SetWindowContentUrl(win, '');
            window.parent.popWindows.SetWindowContentUrl(win, 'Popupcontrol/Sys_Login_Remind.aspx');
            window.parent.popWindows.ShowWindow(win);
            
            //window.parent.popWindows.ShowWindow(window.parent.popWindows.GetWindowByName('reminderform'));
        }
     </script>
     
     
     <script type="text/javascript">
         function cancelWindow() {
             window.parent.popWindows.HideWindow(window.parent.popWindows.GetWindowByName('loginform'));
         }
     </script> 
   
</head>

<body>

<form id="form1" runat="server">

                            
                            <table style="height:250px;width:250px;border:0">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblmsg" runat="server" BackColor="#FFE6DF" CssClass="alert" 
                                            CssFilePath="~/App_Themes/typography.css" CssPostfix="alert" 
                                            EnableDefaultAppearance="False" Font-Names="Arial,Helvetica,Sans-serif" 
                                            Font-Size="Small" ForeColor="#CC0000" Height="50px" 
                                            Text=" Invalid user name or password" Visible="False" width="100%">
                                            <Border BorderColor="#FFD9CF" BorderStyle="Solid" BorderWidth="1px" />
                                            <BackgroundImage HorizontalPosition="right" 
                                                ImageUrl="~/Images/typography/box_alert.png" Repeat="NoRepeat" 
                                                VerticalPosition="top" />
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">
                                        <table border="0" cellpadding="4" cellspacing="0">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dx:ASPxLabel ID="lbluser" runat="server" 
                                                        CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" 
                                                        Text="User name">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left">
                                                    <dx:ASPxTextBox ID="txtUserName" runat="server" ClientInstanceName="txtResult" 
                                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                        CssPostfix="Office2003Blue" Height="22px" 
                                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="140px">
                                                        <ValidationSettings CausesValidation="True" Display="Dynamic" 
                                                            ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                            <RegularExpression ErrorText="Please check user name" 
                                                                ValidationExpression="^[\d_a-zA-Z' ']{1,50}$" />
                                                            <RequiredField ErrorText="You must enter a user name" IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="" valign="top">
                                                    <dx:ASPxLabel ID="lblpass" runat="server" 
                                                        CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" 
                                                        Text="Password">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="left">
                                                    <dx:ASPxTextBox ID="txtPassword" runat="server" 
                                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                        CssPostfix="Office2003Blue" Height="22px" Password="True" 
                                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="140px">
                                                        <ValidationSettings CausesValidation="True" ErrorTextPosition="Bottom" 
                                                            SetFocusOnError="True">
                                                            <RegularExpression ErrorText="Please check password" 
                                                                ValidationExpression="^[\d_0-9a-zA-Z]{1,50}$" />
                                                            <RequiredField ErrorText="You must enter a password" IsRequired="True" />
                                                        </ValidationSettings>
                                                        <MaskSettings AllowMouseWheel="False" />
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:CheckBox ID="chbSavePassword" runat="server" CssClass="checkbox" 
                                                        Font-Names="Tahoma" Font-Size="10pt" Text="Remember Password" 
                                                        TextAlign="Left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="1">
                                                    <dx:ASPxButton ID="btnLogin" runat="server" 
                                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" Height="9px" onclick="cmdLogin_Click" 
                                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Ok" 
                                                        Width="100px">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="left" colspan="1">
                                                    <dx:ASPxButton ID="btnCancel" runat="server" CausesValidation="False" 
                                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" onclick="btnCancel_Click" 
                                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Cancel" 
                                                        Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <a id="hl1" class="level3" href="javascript:showReminderWindow();" 
                                                        style="float: left">forgotten your user name/password?</a></td>
                                            </tr>
                                            <tr>
                          <!--<td align="center" colspan="2"><BR>
                            <asp:LinkButton id="cmdGuest" runat="server" CausesValidation="False">Login as Guest</asp:LinkButton>
                          </td>-->
                                            </tr>
                                            <tr>
                          <!--<td align=middle valign=bottom  colspan=2><br>
                                <a href="register.aspx">Register</a>
                   
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="remind.aspx">Forgot password?</a>

                           </td>-->
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
   
</form> 

</body>

</html>
