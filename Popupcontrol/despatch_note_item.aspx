<%@ Page Language="C#" AutoEventWireup="true" CodeFile="despatch_note_item.aspx.cs" Inherits="Popupcontrol_despatch_note_item" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formDespatchItem" runat="server">
             <div>
                         <dx:ASPxLabel ID="dxlblContainerNo" runat="server" 
                                                     ClientInstanceName="lblContainerNo" 
                                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                     CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                                     Text="Container" ForeColor="#333333">
                                                 </dx:ASPxLabel>
                     </div>
                     <div>
                             <table id ="tblCartons" cellpadding="3px" border="0" width="600px" >
                            <tbody>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCartonCaption" runat="server" 
                                            ClientInstanceName="lblCartonTitle" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Title">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <dx:ASPxLabel ID="dxlblTitleValue" runat="server" 
                                            ClientInstanceName="lblTitleValue" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                  <tr>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblIsbn" runat="server" ClientInstanceName="lblIsbn" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="ISBN">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblIsbnValue" runat="server" 
                                              ClientInstanceName="lblIsbnValue" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                        <dx:ASPxLabel ID="dxlblOrderNo" runat="server" 
                                            ClientInstanceName="lblOrderNo" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Publiship Ref">
                                        </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblOrderNoValue" runat="server" 
                                              ClientInstanceName="lblOrderNoValue" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue">
                                          </dx:ASPxLabel>
                                      </td>
                                </tr>
                                  <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblImpression" runat="server" 
                                            ClientInstanceName="lblImpression" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Impression">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblImpressionValue" runat="server" 
                                            ClientInstanceName="lblImpressionValue" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblBuyerRef" runat="server" 
                                            ClientInstanceName="lblBuyerRef" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Buyer ref">
                                        </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblBuyerRefValue" runat="server" 
                                              ClientInstanceName="lblBuyerRefValue" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue">
                                          </dx:ASPxLabel>
                                      </td>
                                </tr>
                                  <tr>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblQty" runat="server" ClientInstanceName="lblQty" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Title quantitiy">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtCopies" runat="server" ClientInstanceName="txtCopies" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                          </dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblParcelHeight" runat="server" 
                                              ClientInstanceName="lblParcelHeight" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Carton height">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtParcelHeight" runat="server" 
                                              ClientInstanceName="txtParcelHeight" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                          </dx:ASPxTextBox>
                                      </td>
                                </tr>
                                  <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCartonAdd" runat="server" 
                                            ClientInstanceName="lblCartonAdd" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Carton quantity">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtCartonsAdd" runat="server" 
                                            ClientInstanceName="txtCartonsAdd" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblParcelDepth" runat="server" 
                                            ClientInstanceName="lblParcelDepth" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Carton depth">
                                        </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtParcelDepth" runat="server" 
                                              ClientInstanceName="txtParcelDepth" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                          </dx:ASPxTextBox>
                                      </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblFullPallets" runat="server" 
                                            ClientInstanceName="lblFullPallets" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Full pallets">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtFullPallets" runat="server" 
                                            ClientInstanceName="txtFullPallets" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblParcelWeight" runat="server" 
                                            ClientInstanceName="lblParcelWeight" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Carton weight">
                                        </dx:ASPxLabel>
                                     </td>
                                     <td>
                                         <dx:ASPxTextBox ID="dxtxtParcelWeight" runat="server" 
                                             ClientInstanceName="txtParcelWeight" 
                                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                             CssPostfix="Office2010Blue" 
                                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                         </dx:ASPxTextBox>
                                     </td>
                                </tr>
                                   <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblUnitsFull" runat="server" 
                                            ClientInstanceName="lblUnitsFull" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Cartons per full pallet">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtUnitsFull" runat="server" 
                                            ClientInstanceName="txtUnitsFull" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblParcelWidth" runat="server" 
                                            ClientInstanceName="lblParcelWidth" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Carton width">
                                        </dx:ASPxLabel>
                                       </td>
                                       <td>
                                           <dx:ASPxTextBox ID="dxtxtParcelWidth" runat="server" 
                                               ClientInstanceName="txtParcelWidth" 
                                               CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                               CssPostfix="Office2010Blue" 
                                               SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                           </dx:ASPxTextBox>
                                       </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblPartPallets" runat="server" 
                                            ClientInstanceName="lblPartPallets" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Full pallets">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtPartPallets" runat="server" 
                                            ClientInstanceName="txtPartPallets" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCpc" runat="server" ClientInstanceName="lblCpc" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Copies per carton">
                                        </dx:ASPxLabel>
                                     </td>
                                     <td>
                                         <dx:ASPxTextBox ID="dxtxtCpc" runat="server" ClientInstanceName="txtCpc" 
                                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                             CssPostfix="Office2010Blue" 
                                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                         </dx:ASPxTextBox>
                                     </td>
                                </tr>
                                   <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblUnitsPart" runat="server" 
                                            ClientInstanceName="lblUnitsPart" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Cartons per part pallet">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtUnitsPart" runat="server" 
                                            ClientInstanceName="txtUnitsPart" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblHeight" runat="server" ClientInstanceName="lblHeight" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Book height">
                                        </dx:ASPxLabel>
                                       </td>
                                       <td>
                                           <dx:ASPxTextBox ID="dxtxtHeight" runat="server" ClientInstanceName="txtHeight" 
                                               CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                               CssPostfix="Office2010Blue" 
                                               SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                           </dx:ASPxTextBox>
                                       </td>
                                </tr>
                                    <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblPerLayer" runat="server" 
                                            ClientInstanceName="lblPerLayer" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Cartons perlayer">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtPerLayer" runat="server" 
                                            ClientInstanceName="txtPerLayer" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblWidth" runat="server" ClientInstanceName="lblWidth" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Book width">
                                        </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="dxtxtWidth" runat="server" ClientInstanceName="txtWidth" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                            </dx:ASPxTextBox>
                                        </td>
                                </tr>
                                  <tr>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblOdds" runat="server" 
                                            ClientInstanceName="lblOdds" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="No. of odds">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="dxtxtOdds" runat="server" 
                                            ClientInstanceName="txtOdds" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblDepth" runat="server" ClientInstanceName="lblDepth" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Book depth">
                                        </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtDepth" runat="server" ClientInstanceName="txtDepth" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                          </dx:ASPxTextBox>
                                      </td>
                                </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblWeight" runat="server" ClientInstanceName="lblWeight" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Book weight">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtWeight" runat="server" ClientInstanceName="txtWeight" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                          </dx:ASPxTextBox>
                                      </td>
                                </tr>
                                  <tr>
                                    <td>
                                        <dx:ASPxHiddenField ID="dxhfdContainer" runat="server" 
                                            ClientInstanceName="hfdContainer">
                                        </dx:ASPxHiddenField>
                                      </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                      <td>
                                          &nbsp;</td>
                                </tr>
                                  <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                          <dx:ASPxButton ID="dxbtnAdd" runat="server" ClientInstanceName="btnAdd" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                                Text="Add to items" HorizontalAlign="Left"  
                                                                            AutoPostBack="False" 
                                              CausesValidation="False" Wrap="False" onclick="dxbtnAdd_Click">
                                                                            <Image Height="26px" ToolTip="Save" Url="~/Images/icons/metro/save.png" 
                                                                                Width="26px">
                                                                            </Image>
                                                                            </dx:ASPxButton> 
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                      <td>
                                          <dx:ASPxButton ID="dxbtnCancel" runat="server" AutoPostBack="False" 
                                              CausesValidation="False" ClientInstanceName="btnCancel" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Text="Cancel" onclick="dxbtnCancel_Click">
                                              <Image Height="26px" Url="~/Images/icons/metro/cancel.png" Width="26px">
                                              </Image>
                                          </dx:ASPxButton>
                                      </td>
                                </tr>
                            </tbody> 
                        </table>
                       </div>
    </form>
</body>
</html>
