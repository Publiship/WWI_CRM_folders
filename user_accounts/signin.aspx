<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="signin.aspx.cs" Inherits="signin" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
    


<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">


    <script type="text/javascript">
        function showReminderWindow() {
            var win = window.parent.popWindows.GetWindowByName('reminderform');
            window.parent.popWindows.SetWindowContentUrl(win, '');
            window.parent.popWindows.SetWindowContentUrl(win, '../Popupcontrol/Sys_Login_Remind.aspx');
            window.parent.popWindows.ShowWindow(win);
            
            //window.parent.popWindows.ShowWindow(window.parent.popWindows.GetWindowByName('reminderform'));
        }
     </script>

     <div class="innertube">  <!-- just a container div --> 
                
                <!-- centered box as not much on this screen! -->
                <div class="formcenter">
                <div class="info">
                    <dx:ASPxLabel ID="dxlblinfo" ClientInstanceName="lblinfo" runat="server" 
                        Text="We noticed you haven't used Publiship Logbook for 30 minutes, so to ensure your security we have logged you out automatically." 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                    <br /><br />
                    <dx:ASPxLabel ID="dxlblinfo2" ClientInstanceName="lblinfo2" runat="server" 
                        Text=" You can log back in using your user name and password below." 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                </div> 
                <!-- end info -->
                <div>
                    <dx:ASPxPanel ID="dxpnlmsg" ClientInstanceName="pnlmsg" runat="server" 
                            Visible="false" Height="50px">
                            <PanelCollection>
                                <dx:PanelContent>
                                 <div class="alert"> 
                                    <dx:ASPxLabel ID="lblmsg" runat="server"
                                        Text=" Invalid user name or password" Visible="true" 
                                         CssFilePath="~/App_Themes/Office2010Black/{0}/styles.css" 
                                         CssPostfix="Office2010Black" ClientInstanceName="dxlblmsg">
                                    </dx:ASPxLabel>
                                    </div>
                                </dx:PanelContent> 
                            </PanelCollection> 
                        </dx:ASPxPanel>
                </div> 
                <!-- end alert -->
                <table style="margin: 0 Auto; height:250px;width:250px;border:0">
                <tr>
                    <td></td> 
                </tr>
                <tr>
                  <td valign="middle" align="center">
                      <table cellspacing="0" cellpadding="4" border="0">
                        <tr>
                          <td valign="top" align="right" style="width: 80px" >
                              <dx:ASPxLabel ID="lbluser" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  Text="User name" Width="70px">
                              </dx:ASPxLabel>
                          </td>
                          <td align="left">
                            
                              <dx:ASPxTextBox ID="txtUserName" runat="server" 
                                  Width="140px" ClientInstanceName="txtResult" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                  CssPostfix="Office2010Blue" Height="22px" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                      <ValidationSettings CausesValidation="false" EnableCustomValidation="true"  
                                          SetFocusOnError="True" Display="Dynamic">
                                            <RegularExpression ErrorText="Please check user name" 
                                                ValidationExpression="^[0-9a-zA-Z''-'\s]+$" />
                                            <RequiredField IsRequired="true" ErrorText="You must enter a user name" /> 
                                      </ValidationSettings>
                              </dx:ASPxTextBox>  
                            </td>
                            <td></td> 
                        </tr>
                        <tr>
                          <td valign="top" align="right" style="width: 80px">
                              <dx:ASPxLabel ID="lblpass" runat="server" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  Text="Password">
                              </dx:ASPxLabel>
                            </td>
                          <td align="left">
                              <dx:ASPxTextBox 
                                  ID="txtPassword" runat="server" Width="140px" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Height="22px" 
                                  Password="True">
                              <ValidationSettings CausesValidation="false" EnableCustomValidation="true"  
                                  SetFocusOnError="True">
                                   <RegularExpression ErrorText="Please check password" 
                                       ValidationExpression="^[\d_0-9a-zA-Z]{1,50}$" />
                                  <RequiredField IsRequired="True" ErrorText="You must enter a password" />
                              </ValidationSettings>
                                  <MaskSettings AllowMouseWheel="False" />
                              </dx:ASPxTextBox>
                             </td> 
                             <td></td>
                        </tr>
                        
                        <tr>
                        <td align="right" colspan="1" style="width: 80px" ></td> 
                        <td align="left" colspan="2">
                            <dx:ASPxCheckBox ID="dxchbSavePassword" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="chbSavePassword" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Text="Remember password">
                            </dx:ASPxCheckBox>
                        </td>
                        </tr>
                        <tr>
                          <td align="right" colspan="1" style="width: 80px">
                            
                          </td>
                          <td align="left" colspan="1">
                                <dx:ASPxButton ID="btnLogin" ClientInstanceName="dxReport" runat="server" 
                                  CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" CssPostfix="Office2003Olive" 
                                  onclick="cmdLogin_Click" SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                                  Text="Ok" Width="100px" Height="9px">
                              </dx:ASPxButton>
                            </td>
                            <td>
                            <dx:ASPxButton ID="btnCancel" ClientInstanceName="dxCancel" runat="server" 
                                  CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                                  SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Cancel" 
                                  Width="100px" CausesValidation="False" onclick="btnCancel_Click">
                                  <ClientSideEvents Click="function(s, e) {}" />
                              </dx:ASPxButton>
                            </td>
                        </tr>
                        
                        <tr>
                          <td colspan="1"  style="width: 80px"></td>  
                          <td align="center" colspan="2" >
                              <a href="javascript:showReminderWindow();" id="hl1" class="level3" 
                                  style="float: left">
                        forgotten your user name/password?</a></td>
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
                </div>     
              <div style="min-height: 200px"></div> 
     </div> 
     <!-- end inner tube -->
     
</asp:Content>

