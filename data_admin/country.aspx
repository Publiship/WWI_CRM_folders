<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="country.aspx.cs" Inherits="country" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
       function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }

        var postponedCallbackRequired = false;
        function onCountryChanged(s, e) {
            if (cbpOrder.InCallback())
                postponedCallbackRequired = true;
            else
                cbpEdit.PerformCallback();
        }
        function OnEndCallback(s, e) {
            if (postponedCallbackRequired) {
                cbpEdit.PerformCallback();
                postponedCallbackRequired = false;
            }
        }
                
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }
        // ]]>
    </script>    

    <div class="container_16">
        <!-- messages -->
        <div class="grid_16 pad_bottom">
           <dx:ASPxPanel ID="dxpnlErr" ClientInstanceName="pnlErr" ClientVisible ="false" 
                    runat="server">
                <PanelCollection>
                    <dx:PanelContent ID="pnlErr">
                        <div class="alert"> 
                            <dx:ASPxLabel ID="dxlblErr" ClientInstanceName="lblErr" runat="server" Text="" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                            </dx:ASPxLabel> 
                        </div>
                    </dx:PanelContent> 
                </PanelCollection> 
            </dx:ASPxPanel>
            <dx:ASPxPanel ID="dxpnlMsg" ClientInstanceName="pnlMsg" ClientVisible ="false" 
                    runat="server">
                <PanelCollection>
                 <dx:PanelContent ID="pnlMsg">
                        <div class="info"> 
                            <dx:ASPxLabel ID="dxlblInfo" ClientInstanceName="lblInfo" runat="server" 
                                Text="" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                            </dx:ASPxLabel> 
                        </div>
                    </dx:PanelContent> 
                </PanelCollection> 
            </dx:ASPxPanel> 
        </div> 
        <div class="clear"></div>
          
        <!-- left column for centering form put some padding in it as empty elements are not rendered! -->
        <div class="grid_3 pad_bottom"></div>    
        <div class="grid_10 pad_bottom">
              <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/data_admin/country_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblDetails" runat="server" 
                             ClientInstanceName="lblDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Country information">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <div class="grid_3 pad_bottom"></div>    
        <!-- images and text -->
        
        <!-- tabs -->
        <div class="clear"></div>
        
        <!-- left column for centering form put some padding in it as empty elements are not rendered! -->
        <div class="grid_3 pad_bottom">
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" 
                ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
       
        </div>    
        <!-- view form columns -->
        <div class="grid_10"> 
            <asp:FormView ID="fmvCountry" runat="server" Width="100%" 
                Height="59px" 
                onmodechanging="fmvCountry_ModeChanging">
                 <EditItemTemplate>
                     <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                   <colgroup>
                                      <col class="caption8" />
                                      <col />
                                  </colgroup>
                                      <tr class="row_divider">
                                          <td>
                                              <dx:ASPxLabel ID="dxlblCountryEdit" runat="server" 
                                                  ClientInstanceName="lblCountryEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Country">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtCountryEdit" runat="server" 
                                                  ClientInstanceName="txtCountryEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('CountryName') %>" Width="250px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                          </td>
                                    </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblEuroEdit" runat="server" 
                                                 ClientInstanceName="lblEuroEdit" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="Euro union?">
                                             </dx:ASPxLabel>
                                        </td>
                                        <td>
                                               <dx:ASPxCheckBox ID="dxckEuroEdit" runat="server" CheckState="Unchecked" 
                                                   ClientInstanceName="ckEuroEdit" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" 
                                                   SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                   Value="<%# Bind('EU') %>">
                                               </dx:ASPxCheckBox>
                                        </td>
                                      </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblIsoEdit" runat="server" ClientInstanceName="lblIsoEdit" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="ISO code">
                                             </dx:ASPxLabel>
                                          </td>
                                        <td>
                                             <dx:ASPxTextBox ID="dxtxtISOEdit" runat="server" 
                                                 ClientInstanceName="txtISOEdit" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" 
                                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                 Width="150px" MaxLength="10" Text="<%# Bind('ISOCode') %>">
                                             </dx:ASPxTextBox>
                                          </td>
                                      </tr> 
                                      <tr class="row_divider">
                                        <td>
                                             &nbsp;</td>
                                          <td>
                                              &nbsp;</td>
                                      </tr> 
                                  </table>
                </EditItemTemplate>
                <ItemTemplate>
                    <table id="tblView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption8" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblCountry" runat="server" 
                                       ClientInstanceName="lblCountry" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Name">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblCountryView" runat="server" 
                                    ClientInstanceName="lblCountryView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("CountryName") %>'>
                                </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblUnion" runat="server" 
                                       ClientInstanceName="lblUnion" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="European union">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblUnionView" runat="server" 
                                       ClientInstanceName="lblUnionView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('EU') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblISO" runat="server" ClientInstanceName="lblISO" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="ISO code">
                                 </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblISOView" runat="server" 
                                    ClientInstanceName="lblISOView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("ISOCode") %>'>
                                </dx:ASPxLabel>
                              </td>
                          </tr> 
                      </table>      
                </ItemTemplate>   
                <InsertItemTemplate>
                       <table id="tblInsert" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                   <colgroup>
                                      <col class="caption8" />
                                      <col />
                                  </colgroup>
                                      <tr class="row_divider">
                                          <td>
                                              <dx:ASPxLabel ID="dxlblCountryInsert" runat="server" 
                                                  ClientInstanceName="lblCountryInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Country">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtCountryInsert" runat="server" 
                                                  ClientInstanceName="txtCountryInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('CountryName') %>" Width="250px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                          </td>
                                    </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblEuroInsert" runat="server" 
                                                 ClientInstanceName="lblEuroInsert" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="Euro union?">
                                             </dx:ASPxLabel>
                                        </td>
                                        <td>
                                               <dx:ASPxCheckBox ID="dxckEuroInsert" runat="server" CheckState="Unchecked" 
                                                   ClientInstanceName="ckEuroInsert" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" 
                                                   SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                   Value="<%# Bind('EU') %>">
                                               </dx:ASPxCheckBox>
                                        </td>
                                      </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblIsoInsert" runat="server" ClientInstanceName="lblIsoInsert" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="ISO code">
                                             </dx:ASPxLabel>
                                          </td>
                                        <td>
                                             <dx:ASPxTextBox ID="dxtxtISOInsert" runat="server" 
                                                 ClientInstanceName="txtISOInsert" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" 
                                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css"  
                                                 Width="150px" MaxLength="10" Text="<%# Bind('ISOCode') %>">
                                             </dx:ASPxTextBox>
                                          </td>
                                      </tr> 
                                      <tr class="row_divider">
                                        <td>
                                             &nbsp;</td>
                                          <td>
                                              &nbsp;</td>
                                      </tr> 
                                  </table>
                </InsertItemTemplate>
            </asp:FormView>
        </div> 
        <!-- right column for centering form -->
        <div class="grid_3 pad_bottom">
        </div>    
        <div class="clear"></div> 
        <!-- custom command buttons for formview -->
        <!-- left column for centering menu -->
        <div class="grid_3 pad_bottom"></div>    
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <!-- command menu -->
         <div class="grid_10 pad_bottom">
             <dx:ASPxMenu ID="dxmnuFormView" runat="server" 
                ClientInstanceName="mnuFormView" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" onitemclick="dxmnuFormView_ItemClick" 
                 onitemdatabound="dxmnuFormView_ItemDataBound" RenderMode="Lightweight" 
                 ShowAsToolbar="True">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
        </div>    
        <!-- seperate menu for client side commands -->
        <!-- end menu -->
        <!-- right column for centering menu -->
        <div class="grid_3 pad_bottom"></div>  
        <div class="clear"></div>   
        <!-- left column for centering leaving dates -->
        <div class="grid_16 pad_bottom">
           
        </div>    
        <!-- right column for centering leaving dates -->
        <div class="clear"></div> 
        <!-- left column centering arrival dates -->
        <!--- right column centering arraival dates -->
        <div class="clear"></div>
        <!-- end hiddenfield --> 
        <div class="clear"></div>
    </div><!-- end container -->
   
    
  
</asp:Content>


