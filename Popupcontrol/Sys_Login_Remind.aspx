<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sys_Login_Remind.aspx.cs" Inherits="Sys_Login_Remind" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
    
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Publiship Online v1.0</title>
    <link rel="stylesheet" type="text/css" href="~/App_Themes/custom.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="~/App_Themes/style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="~/App_Themes/typography.css" media="screen" />
 
    <script type="text/javascript">
        function hideReminderWindow() {
            var win = window.parent.popWindows.GetWindowByName('reminderform');
            window.parent.popWindows.HideWindow(win);
            window.parent.popWindows.SetWindowContentUrl(win, '');
            
            //window.parent.popWindows.HideWindow(window.parent.popWindows.GetWindowByName('reminderform'));
        }
     </script>
   
    <style type="text/css">
        .style1
        {
            width: 148px;
        }
    </style>
   
</head>

<body>

<form id="form1" runat="server">
        
          <table style="margin: 0 auto;height:300px;width:300px;border:0">
                <tr>
                    <td></td> 
                </tr>
                <tr>
                  <td valign="middle" align="center">
                      <table cellspacing="0" cellpadding="4" border="0">
                        <tr>
                          <td valign="top" align="right" class="style1">
                              <dx:ASPxLabel ID="lblmail" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  Text="Enter your email address">
                              </dx:ASPxLabel>
                          </td>
                          <td align="left">
                            
                              <dx:ASPxTextBox ID="txtdxmail" runat="server" 
                                  Width="200px"                                   ClientInstanceName="txtmail" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                  CssPostfix="Office2010Blue" Height="22px" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                  NullText="Your email address" EnableViewState="False">
                                      <ValidationSettings CausesValidation="True" 
                                          SetFocusOnError="True" Display="Dynamic">
                                            <RegularExpression ErrorText="Please check email address" 
                                                ValidationExpression="^[@.\d_a-zA-Z' ']{1,50}$" />
                                            <RequiredField IsRequired="true" ErrorText="Email address is incorrect" /> 
                                      </ValidationSettings>
                              </dx:ASPxTextBox>  
                            </td>
                        </tr>
                        <tr>
                          <td valign="top" align="right" class="style1">
                              &nbsp;</td>
                          <td align="left">
                              <dx:ASPxCaptcha ID="dxcaptcha1" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                  CssPostfix="Office2010Blue" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                  ClientInstanceName="captcha1" EnableViewState="False" TabIndex="1">
                                  <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                  </LoadingPanelImage>
<ChallengeImage BorderColor="#859EBF" BorderWidth="1" ForegroundColor="#859EBF">
                                  <BackgroundImage ImageUrl="~/App_Themes/Office2010Blue/Editors/caChallengeImageBack.png" />
                                  </ChallengeImage>
                                  <TextBox Position="Bottom" />
                              </dx:ASPxCaptcha>
                             </td> 
                        </tr>
                        
                        <tr>
                          <td align="right" colspan="1" class="style1" >
                              <dx:ASPxButton ID="btnSend" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                  Text="Send" Width="80px" Height="9px" onclick="btnSend_Click" TabIndex="4">
                              </dx:ASPxButton>
                          </td>
                          <td align="left" colspan="1">
                              <dx:ASPxButton ID="btnCancel" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Cancel" 
                                  Width="80px" CausesValidation="False">
                                  <ClientSideEvents Click="hideReminderWindow" />
                              </dx:ASPxButton>
                            </td>
                        </tr>
                        
                    <tr>
                    <td align="center" colspan="2" valign="top">
                        
                         <asp:Panel ID="pnlmsg1" runat="server" BackColor="#FFE6DF" ForeColor="#CC0000"
                            BorderColor="#F5FAEB" BorderStyle="Solid" BorderWidth="1px" 
                            Visible="False" EnableViewState="False">
                                <table>
                                    <tr>
                                    <td><img src="../Images/typography/box_alert.png" title = "Account not verified" 
                                            alt="" align="top"/></td> 
                                    <td><div class="level3">Sorry, we are unable to verify your account details. Please check you email 
                                        address and security code above.</div></td>
                                    </tr>
                                </table> 
                         </asp:Panel>
                         
                        <asp:Panel ID="pnlmsg2" runat="server" BackColor="#F5FAEB" 
                            BorderColor="#D6EBCD" BorderStyle="Solid" BorderWidth="1px" 
                            Visible="False" EnableViewState="False">
                                <table>
                                    <tr>
                                    <td><img src="../Images/typography/list_check.png" title = "Reminder email sent" 
                                            alt="" align="top"/></td> 
                                    <td><div class="level3">
                                    Your account details have been sent, please check your email. If the email does not appear in your inbox, please check your junk email.
                                    </div><div>
                                     <dx:ASPxButton ID="btnCancel2" runat="server" 
                                          CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                          SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Ok" 
                                          Width="80px" CausesValidation="False">
                                    
                                          <ClientSideEvents Click="hideReminderWindow" />
                                      </dx:ASPxButton>
                                    </div></td>
                                    </tr>
                                </table> 
                         </asp:Panel>
                    </td> 
                    </tr>
                        
                      </table>
                    </td>
                </tr>
              </table>
    
</form> 

</body>

</html>

