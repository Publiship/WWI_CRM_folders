<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="package_type.aspx.cs" Inherits="package_type" %>

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
                      ImageWidth="26px" NavigateUrl="~/data_admin/Place_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblDetails" runat="server" 
                             ClientInstanceName="lblDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Package type information">
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
            <asp:FormView ID="fmvPackage" runat="server" Width="100%" 
                Height="59px" 
                onmodechanging="fmvPackage_ModeChanging" >
                 <EditItemTemplate>
                     <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                   <colgroup>
                                      <col class="caption8" />
                                      <col />
                                  </colgroup>
                                      <tr class="row_divider">
                                          <td>
                                              <dx:ASPxLabel ID="dxlblPackageEdit" runat="server" 
                                                  ClientInstanceName="lblPlaceEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Place">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtPackageEdit" runat="server" 
                                                  ClientInstanceName="txtPackageEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('PackageType') %>" Width="250px" MaxLength="50">
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
                                 <dx:ASPxLabel ID="dxlblPackage" runat="server" 
                                       ClientInstanceName="lblPackage" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Package">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblPackageView" runat="server" 
                                    ClientInstanceName="lblPackageView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("PackageType") %>'>
                                </dx:ASPxLabel>
                            </td>
                          </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
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
                                              <dx:ASPxLabel ID="dxlblPackageInsert" runat="server" 
                                                  ClientInstanceName="lblPackageInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Package">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtPackageInsert" runat="server" 
                                                  ClientInstanceName="txtPackageInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('PackageType') %>" Width="250px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                          </td>
                                    </tr>
                                      <tr class="row_divider">
                                        <td>
                                            </td>
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
    </div><!-- end container -->
   
    
  
</asp:Content>


