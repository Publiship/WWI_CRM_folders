<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="voyage.aspx.cs" Inherits="voyage" %>

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
        function menu_click(s, e) {
            var window1 = ppcDefault.GetWindowByName('vessel_name');
            //do we need this if url is defined in window item?
            //ppcDefault.SetWindowContentUrl(window1, '');
            //ppcDefault.SetWindowContentUrl(window1, '../Popupcontrol/vessel_name.aspx');
            ppcDefault.ShowWindow(window1);
        }
        
        function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }

        var postponedCallbackRequired = false;
        function OnOrderEntered(s, e) {
            if (cbpOrder.InCallback())
                postponedCallbackRequired = true;
            else
                cbpOrder.PerformCallback();
        }
        function OnEndCallback(s, e) {
            if (postponedCallbackRequired) {
                cbpOrder.PerformCallback();
                postponedCallbackRequired = false;
            }
        }

        function onVesselChanged(s, e) {
            var s1 = s.GetValue();
            //needed for updating records on ets/eta grid
            hfOrder.Set('vessel', s1);  
            //this is done when ports are changed
            //var d1 = s.GetSelectedItem().GetColumnText('ETS');
            //lblEtsSub.SetText(d1);
            //var d2 = s.GetSelectedItem().GetColumnText('ETA');
            //lblEtaSub.SetText(d2);       
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
            
           <dx:ASPxPanel ID="dxpnlErr" ClientInstanceName="pnlErr" ClientVisible ="False" 
                    runat="server" Visible="False">
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
            <dx:ASPxPanel ID="dxpnlMsg" ClientInstanceName="pnlMsg" ClientVisible ="False" 
                    runat="server" Visible="False">
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
                      ImageWidth="26px" NavigateUrl="~/shipment_voyage/voyage_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblBolDetails" runat="server" 
                             ClientInstanceName="lblBolDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Voyage details |">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
                        <dx:ASPxLabel ID="dxlblID" runat="server" 
                            ClientInstanceName="lblID" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Joined" >
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
            <asp:FormView ID="fmvVoyage" runat="server" Width="100%" 
                ondatabound="fmvVoyage_DataBound" Height="59px" 
                onmodechanging="fmvVoyage_ModeChanging">
                 <EditItemTemplate>
                    <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                       <colgroup>
                          <col class="caption8" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVesselID" runat="server" 
                                       ClientInstanceName="lblVesselID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Vessel">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <!-- can't use OnItemsRequestedByFilterCondition and OnItemRequestedByValue on server-side processing makes the search case sensitive -->
                                  <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="25" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="VesselName" Value="<%# Bind('VesselID') %>" ValueField="VesselID" 
                                    ValueType="System.Int32" Width="250px" DropDownWidth="250px" 
                                    IncrementalFilteringMode="StartsWith"
                                    onitemrequestedbyvalue="dxcboVesselID_ItemRequestedByValue" 
                                    onitemsrequestedbyfiltercondition="dxcboVesselID_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                 </dx:ASPxComboBox>
                            </td>
                            <td>
                                  &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyageNumber" runat="server" 
                                       ClientInstanceName="lblVoyageNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Voyage number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxTextBox ID="dxtxtVoyageNumber" runat="server" 
                                       ClientInstanceName="txtVoyageNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" 
                                       SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                       Width="150px" Value="<%# Bind('VoyageNumber') %>">
                                       <ValidationSettings>
                                           <RegularExpression ErrorText="Only the number is required" 
                                               ValidationExpression="^[0-9]{1,45}$" />
                                       </ValidationSettings>
                                   </dx:ASPxTextBox>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblVoyageNumberMsg" runat="server" 
                                       ClientInstanceName="lblVoyageNumberMsg" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" 
                                       Text="Just enter the number i.e 160 not 160W or V.160">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAddedBy" runat="server" ClientInstanceName="lblAddedBy" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Added by">
                                 </dx:ASPxLabel>
                              </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblAddedByName" runat="server" 
                                      ClientInstanceName="lblAddedByName" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("AddedBy") %>'>
                                  </dx:ASPxLabel> 
                              </td>
                            <td>
                                &nbsp;</td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDateAdded" runat="server" 
                                     ClientInstanceName="lblDateAdded" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Date added">
                                 </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDateAddedView" runat="server" 
                                      ClientInstanceName="lblDateAddedView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("DateAdded") %>'>
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr> 
                          <tr>
                            <td>
                                &nbsp;</td>
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
                                 <dx:ASPxLabel ID="dxlblVesselID" runat="server" 
                                       ClientInstanceName="lblVesselID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Vessel">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblVesselIDView" runat="server" 
                                    ClientInstanceName="lblVesselIDView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("VesselID") %>'>
                                </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyageNumber" runat="server" 
                                       ClientInstanceName="lblVoyageNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Voyage number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblVoyageNumberView" runat="server" 
                                       ClientInstanceName="lblVoyageNumberView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('VoyageNumber') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAddedBy" runat="server" ClientInstanceName="lblAddedBy" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Added by">
                                 </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblAddedByView" runat="server" 
                                    ClientInstanceName="lblAddedByView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("AddedBy") %>'>
                                </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDateAdded" runat="server" 
                                     ClientInstanceName="lblDateAdded" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Date added">
                                 </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDateAddedView" runat="server" 
                                      ClientInstanceName="lblDateAddedView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("DateAdded") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                          </tr> 
                      </table>      
                </ItemTemplate>   
                <InsertItemTemplate>
                    <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                       <colgroup>
                         <col class="caption8" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVesselID" runat="server" 
                                       ClientInstanceName="lblVesselID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Vessel">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                  <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="25" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="VesselName" Value="<%# Bind('VesselID') %>" ValueField="VesselID" 
                                    ValueType="System.Int32" Width="250px" DropDownWidth="250px" 
                                      IncrementalFilteringMode="StartsWith" 
                                      onitemrequestedbyvalue="dxcboVesselID_ItemRequestedByValue" 
                                      onitemsrequestedbyfiltercondition="dxcboVesselID_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                      <ClientSideEvents SelectedIndexChanged="onVesselChanged" />
                                    </dx:ASPxComboBox>
                            </td>
                            <td>
                                  &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyageNumber" runat="server" 
                                       ClientInstanceName="lblVoyageNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Voyage number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxTextBox ID="dxtxtVoyageNumber" runat="server" 
                                       ClientInstanceName="txtVoyageNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" 
                                       SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                       Width="220px" Value="<%# Bind('VoyageNumber') %>">
                                       <ValidationSettings>
                                           <RegularExpression ErrorText="Only the number is required" 
                                               ValidationExpression="^[0-9]{1,45}$" />
                                       </ValidationSettings>
                                   </dx:ASPxTextBox>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblVoyageNumberMsg" runat="server" 
                                       ClientInstanceName="lblVoyageNumberMsg" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" 
                                       Text="Just enter the number i.e 160 not 160W or V.160">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAddedBy" runat="server" ClientInstanceName="lblAddedBy" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Added by">
                                 </dx:ASPxLabel>
                              </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblAddedByName" runat="server" 
                                      ClientInstanceName="lblAddedByName" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("AddedBy") %>'>
                                  </dx:ASPxLabel> 
                              </td>
                            <td>
                                &nbsp;</td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDateAdded" runat="server" 
                                     ClientInstanceName="lblDateAdded" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Date added">
                                 </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDateAddedView" runat="server" 
                                      ClientInstanceName="lblDateAddedView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("DateAdded") %>'>
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr> 
                          <tr>
                            <td>
                                &nbsp;</td>
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
         <div class="grid_8 pad_bottom">
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
        <div class="grid_2 pad_bottom">
              <dx:ASPxMenu ID="dxmnuClient" runat="server" 
                ClientInstanceName="mnucClient" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" RenderMode="Lightweight" 
                 ShowAsToolbar="True" Width="100%">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <Items>
                                <dx:MenuItem Name="mnuAddVessel" Text="New Vessel">
                                    <Image AlternateText="Add new vessel" Height="26px" 
                                        Url="~/Images/icons/metro/cargo_ship.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                            </Items>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <ClientSideEvents ItemClick="menu_click" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
        </div> 
        <!-- end menu -->
        <!-- right column for centering menu -->
        <div class="grid_3 pad_bottom"></div>  
        <div class="clear"></div>   
        <!-- left column for centering leaving dates -->
        <div class="grid_3 pad_bottom"></div>    
        <div class="grid_10 pad_bottom">
            <dx:ASPxGridView ID="dxgrdETS" runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" Width="100%" ClientInstanceName="grdETS" 
                KeyFieldName="VoyageETSSubID" oncustomcallback="dxgrdETS_CustomCallback" 
                onrowinserting="dxgrdETS_RowInserting" 
                onrowupdating="dxgrdETS_RowUpdating" 
                oncelleditorinitialize="dxgrdETS_CellEditorInitialize" 
                oncustomcolumndisplaytext="dxgrdETS_CustomColumnDisplayText" 
                onrowdeleting="dxgrdETS_RowDeleting">
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsPager Position="TopAndBottom">
                </SettingsPager>
                <ImagesFilterControl>
                    <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanel>
                </ImagesFilterControl>
                <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanelOnStatusBar>
                    <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanel>
                </Images>
                <SettingsEditing Mode="Inline" />
                <SettingsText Title="Sailing dates" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options" Width="70px" 
                        ButtonType="Image">
                        <NewButton Visible="True">
                              <Image AlternateText="View" ToolTip="New" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                </Image>
                        </NewButton>
                        <EditButton Visible="True">
                             <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                </Image>
                        </EditButton>
                        <DeleteButton Visible="True">
                            <Image AlternateText="View" ToolTip="Remove" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                </Image>
                        </DeleteButton>
                        <UpdateButton Visible="True">
                              <Image AlternateText="Update" ToolTip="Update" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                </Image>
                        </UpdateButton> 
                         <CancelButton Visible="True">
                              <Image AlternateText="Cancel" ToolTip="Cancel" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                                </Image>
                        </CancelButton> 
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="VoyageETSSubID" VisibleIndex="1" 
                        Visible="False" Width="0px" Name="colVoyageETSSubID">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="OriginPortID" VisibleIndex="2" 
                        Name="colOriginPortID" Caption="Origin Port" Width="210px">
                        <PropertiesComboBox ValueType="System.Int32" Spacing="0"  
                            DropDownRows="15" EnableCallbackMode="True" 
                            IncrementalFilteringMode="StartsWith" TextField="PortName" ValueField="PortID"></PropertiesComboBox>
                     </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="ETS" VisibleIndex="3" Name="colEts" 
                        Caption="ETS" Width="80px">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                </Columns>
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowGroupButtons="False" ShowTitlePanel="True" 
                    ShowVerticalScrollBar="True" VerticalScrollableHeight="125" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </div>
        <!-- right column for centering leaving dates -->
        <div class="grid_3 pad_bottom"></div>    
        <div class="clear"></div> 
        <!-- left column centering arrival dates -->
        <div class="grid_3 pad_bottom"></div>
        <div class="grid_10 pad_bottom">
          <dx:ASPxGridView ID="dxgrdETA" runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" Width="100%" ClientInstanceName="grdETA" 
                KeyFieldName="VoyageETASubID" oncustomcallback="dxgrdETA_CustomCallback" 
                onrowinserting="dxgrdETA_RowInserting" 
                onrowupdating="dxgrdETA_RowUpdating" 
                oncelleditorinitialize="dxgrdETA_CellEditorInitialize" 
                oncustomcolumndisplaytext="dxgrdETA_CustomColumnDisplayText" 
                onrowdeleting="dxgrdETA_RowDeleting">
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsPager Position="TopAndBottom">
                </SettingsPager>
                <ImagesFilterControl>
                    <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanel>
                </ImagesFilterControl>
                <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanelOnStatusBar>
                    <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                    </LoadingPanel>
                </Images>
                <SettingsEditing Mode="Inline" />
                <SettingsText Title="Arrival dates" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="70px" ButtonType="Image">
                        <NewButton Visible="True">
                              <Image AlternateText="View" ToolTip="New" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                </Image>
                        </NewButton>
                        <EditButton Visible="True">
                             <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                </Image>
                        </EditButton>
                        <DeleteButton Visible="True">
                            <Image AlternateText="View" ToolTip="Remove" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                </Image>
                        </DeleteButton>
                         <UpdateButton Visible="True">
                              <Image AlternateText="Update" ToolTip="Update" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                </Image>
                        </UpdateButton> 
                         <CancelButton Visible="True">
                              <Image AlternateText="Cancel" ToolTip="Cancel" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                                </Image>
                        </CancelButton> 
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="VoyageETASubID" VisibleIndex="1" 
                        Visible="False" Width="0px" Name="colVoyageETASubID">
                    </dx:GridViewDataTextColumn>
                   <dx:GridViewDataComboBoxColumn FieldName="DestinationPortID" VisibleIndex="2" 
                        Name="colDestinationPortID" Caption="Destination Port" Width="210px">
                        <PropertiesComboBox ValueType="System.Int32" Spacing="0" 
                            DropDownRows="15" EnableCallbackMode="True" 
                            IncrementalFilteringMode="StartsWith" TextField="PortName" ValueField="PortID"></PropertiesComboBox>
                     </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="ETA" VisibleIndex="3" Name="colEta" 
                        Caption="ETA" Width="80px">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                </Columns>
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowGroupButtons="False" ShowTitlePanel="True" 
                    ShowVerticalScrollBar="True" VerticalScrollableHeight="125" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </div>
        <!--- right column centering arraival dates -->
        <div class="grid_3 pad_bottom"></div>
        <div class="clear"></div>
        <!-- end hiddenfield --> 
        <div class="clear"></div>
    </div><!-- end container -->
   
    <div>
    <!-- popup control for adding vessel -->
    <dx:ASPxPopupControl ID="ppcDefault" ClientInstanceName="ppcDefault" 
        runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" MinHeight="500px" 
        MinWidth="580px" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server"></dx:PopupControlContentControl>
</ContentCollection>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Windows>
            <dx:PopupWindow ContentUrl="../Popupcontrol/vessel_name.aspx" 
                HeaderText="Add a new vessel" Height="450px" MinHeight="450px" MinWidth="600px" 
                Modal="True" Width="600px" Name="vessel_name">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
        </Windows>
    </dx:ASPxPopupControl>
    <!-- end popup -->     
   </div> 
  
</asp:Content>


