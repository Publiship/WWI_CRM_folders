<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_details.aspx.cs" Inherits="register_details" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register with Publiship</title>
    <link href="http://www.publiship-online.dtemp.net/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="formcenter580">
        <dl class="dl4">
            <dt>First name</dt>
            <dd>
                <dx:ASPxTextBox ID="dxtxtname1" ClientInstanceName="txtname1" runat="server" 
                    Width="200px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxTextBox>
            </dd> 
            <dt>Surname</dt>
            <dd>
                <dx:ASPxTextBox ID="dxtxtname2" ClientInstanceName="txtname2" runat="server" 
                    Width="200px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxTextBox>
            </dd>
            <dt>Company</dt>
            <dd>
                <dx:ASPxTextBox ID="dxtxtcompany" ClientInstanceName="txtcompany" 
                    runat="server" Width="200px" 
                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                    <ValidationSettings>
                          <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value"  />
                            <RequiredField ErrorText="Required" IsRequired="true" /> 
                     </ValidationSettings>
                </dx:ASPxTextBox>
            </dd>
            <dt>Phone number</dt>
            <dd>
                <dx:ASPxTextBox ID="dxtxtphone" ClientInstanceName="txtphone" runat="server" 
                    Width="200px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxTextBox>
            </dd>
            <dt>Email address</dt>
            <dd>
                <dx:ASPxTextBox ID="dxtxtemail" ClientInstanceName="txtemail" runat="server" 
                    Width="250px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxTextBox>
            </dd>
            <dt>Country</dt>
            <dd><dx:ASPxComboBox ID="dxcbocountry" ClientInstanceNam="cbocountry" runat="server" 
                    ValueType="System.String" 
                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                    IncrementalFilteringMode="StartsWith" TextField="name" ValueField="name">
                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ValidationSettings>
                        <RequiredField ErrorText="Required" IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox></dd>
            <dt>Additional information</dt>
            <dd>
                <dx:ASPxMemo ID="dxmemoadd" ClientInstanceName="memoadd" runat="server" 
                    Height="71px" Width="250px" 
                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxMemo>
            </dd>
            <dt>Include me in mailing list</dt>
            <dd>
                <dx:ASPxCheckBox ID="dxckmailing" ClientInstanceName="ckmailing" Checked="True" 
                    runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                </dx:ASPxCheckBox>
            </dd>
            <dt>Where did you hear about us?</dt>
            <dd>
                <dx:ASPxComboBox ID="dxcbowhere" ClientInstanceNam="cbowhere" runat="server" 
                    ValueType="System.String" 
                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                    <Items>
                        <dx:ListEditItem Text="First shipment invite" Value="First shipment" />
                        <dx:ListEditItem Text="Web search" Value="Web search" />
                        <dx:ListEditItem Text="Other" Value="Other" />
                    </Items>
                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                    </LoadingPanelImage>
                </dx:ASPxComboBox>
            </dd>
            </dl>
           
            <div style="padding: 10px 10px 45px 205px; border-bottom: 10px; height: auto;">
                <dx:ASPxCaptcha ID="dxcapt1" ClientInstanceName="capt1" runat="server" 
                    Height="133px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
<ChallengeImage BackgroundColor="#DDECFE" BorderColor="#002D96" BorderWidth="1"></ChallengeImage>
                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                    </LoadingPanelImage>
                    <TextBox Position="Bottom" />
                </dx:ASPxCaptcha>
            </div> 
            <div style="padding: 10px 10px 10px 205px;">
                <div style="clear:both;  float: left">
                    <dx:ASPxButton ID="dxbtnsend" ClientInstanceName="dtnsend" Text="Submit" 
                        runat="server" Native="True" AutoPostBack="False" CausesValidation="true" UseSubmitBehavior="false" onclick="dxbtnsend_Click">
                    </dx:ASPxButton>
                </div>  
            </div> 
            
           <div style="padding:  10px; margin: 10px">
                  <asp:Panel ID="pnlmsg1" runat="server"  
                            Visible="False" EnableViewState="False">
                                <table>
                                    <tr>
                                    <td><img src="../Images/typography/box_alert.png" title = "Account not verified" 
                                            alt="" align="top"/></td> 
                                    <td><div>Sorry, we are unable to process your details. Please check you email 
                                        address and security code above.</div></td>
                                    </tr>
                                </table> 
                         </asp:Panel>
                         
                        <asp:Panel ID="pnlmsg2" runat="server"  Visible="False" EnableViewState="False">
                                <table>
                                    <tr>
                                    <td><img src="../Images/typography/list_check.png" title = "Reminder email sent" 
                                            alt="" align="top"/></td> 
                                    <td><div class="level3">
                                        Your details have been sent to Publiship,&nbsp; we will contact you shortly. Thank 
                                        you for your interest.</div><div style="padding: 0px 0px 0px 155px;">
                                     <dx:ASPxButton ID="btnCancel2" runat="server" 
                                          CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                          SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Ok" 
                                          Width="80px" CausesValidation="False" UseSubmitBehavior="false" Native="True" 
                                                onclick="btnCancel2_Click" ClientEnabled="False" ClientVisible="False" 
                                                Visible="False">
                                    
                                          <ClientSideEvents Click="hideReminderWindow" />
                                      </dx:ASPxButton>
                                    </div></td>
                                    </tr>
                                </table> 
                         </asp:Panel>
                </div>          
    </div>
    </form>
</body>
</html>
