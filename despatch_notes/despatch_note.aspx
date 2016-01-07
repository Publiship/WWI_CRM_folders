<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="despatch_note.aspx.cs" Inherits="despatch_note" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

 <script type="text/javascript">
     // <![CDATA[
     function onContainerCustomButtonClick(s, e) {
         e.processOnServer = false;
         
         //var user = verify_user();
         //if (user != 'You are not signed in') {
             if (e.buttonID == 'btnSelect') {
                 //var window = popDefault.GetWindowByName('sysna');
                 //popDefault.ShowWindow(window);
                 //just use order number
                 s.GetRowValues(s.GetFocusedRowIndex(), 'SubDeliveryID;ContainerNumber', onGotValues);
             }
         //}
         //else {
         //    //var window = popDefault.GetWindowByName('sysna');
         //    //popDefault.ShowWindow(window);
         //    var window = popDefault.GetWindowByName('msgform');
         //    popDefault.ShowWindow(window);
         //}
     }

     function onGotValues(values) {
         var vals = values.toString().split(',');   
         //var window1 = popDefault.GetWindowByName('Cartons');
         //
         //hfdContainer.Set("idx", vals[0]);
         //lblContainerNo.SetText('Container ' + vals[1]);
         //lblTitleValue.SetText(vals[2]);
         //txtCopies.SetText(vals[3]);
         //popDefault.PerformCallback(vals[0]);
         //popDefault.ShowWindow(window1);
         var despatchnoteid = hfIds.Get('id'); 
         var window1 = popDefault.GetWindowByName('Cartons');
         popDefault.SetWindowContentUrl(window1, '');
         popDefault.SetWindowContentUrl(window1, '../Popupcontrol/despatch_note_item.aspx?ids=' + vals[0] + '&ctr=' + vals[1] + '&idn=' + despatchnoteid);
         popDefault.ShowWindow(window1);
     }

     function update_items(s) {
         //called from popupcontrol despatch_note_item
         if (!grdItemDetails.InCallback()) {
             grdItemDetails.PerformCallback(' ');
         }
     }

     function hideAllocationWindow() {
         //popup.Hide();
         window.popDefault.HideWindow(window.popDefault.GetWindowByName('Cartons'));
         //refesh allocation grid
         //if (!grdContainerOrders.InCallback()) {
         //    grdContainerOrders.PerformCallback(' ');
         //}
     }
     
     function onGridInit(s, e) {
         gridContainer.PerformCallback('getdata');
     
     }
     function onContainerValueChanged(s, e) {
         var id = s.GetValue();
         gridContainer.PerformCallback(id);
     }
     
     function onEditLogClick(s, e) {
         pgeConsigment.SetActiveTab(pgeConsigment.GetTab(2)); //edit reference page
     }

     function onCancelUpdateRefClick(s, e) {
         pgeConsigment.SetActiveTab(pgeConsigment.GetTab(1)); //titles page   
     }
     
     function onCustomButtonClick(s, e) {
         if (e.buttonID == 'cmdUpdate') {
             grdItemDetails.UpdateEdit();
         }
         else if (e.buttonID == 'cmdCancel') {
            grdItemDetails.CancelEdit();
         }
     }

     function OnMenuItemClick(s, e) {
         switch (e.item.name) {
             case "jsUpdate": grid.UpdateEdit(); break;
             case "jsEdit":
                 StartEdit();
                 break;
             case "jsNewItem":
                 grdItemDetails.AddNewRow(); break;
             //grid.AddNewRow(); break;     
             case "jsDelete":
                 if (confirm('Are you sure to delete this record?'))
                     grid.DeleteRow(grid.GetTopVisibleIndex());
                 break;
             case "jsRefresh":
                 break;
             case "jsCancel": grid.Refresh();
                 break;
         }
     } 
     
     function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
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
            <!-- error meaages -->
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
            <!-- other messages -->
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
        <div class="clear"></div> <!-- end message panels -->
        <!-- title -->
        <div class="grid_16 pad_bottom">
            <table id="tblCaption">
                <tbody>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblNoteCaption" runat="server" 
                                                     ClientInstanceName="lblNote" 
                                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                     CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                                     Text="Consignment note : " ForeColor="#333333">
                                                 </dx:ASPxLabel>
                        </td>
                        <td>
                          <dx:ASPxLabel ID="dxlblNoteRef" runat="server" 
                                                     ClientInstanceName="lblNote" 
                                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                     CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                                     Text="" ForeColor="#333333">
                                                 </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxButton ID="dxbtnEditLog" runat="server" Text="Edit" 
                                                    ClientInstanceName="btnEditLog" 
                                                    CssFilePath="~/App_Themes/Office2010Silver/{0}/styles.css" 
                                                    CssPostfix="Office2010Silver" HorizontalAlign="Left" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Silver/{0}/sprite.css" 
                                                    VerticalAlign="Middle" AutoPostBack="False" 
                                CausesValidation="False">
                                                 <Image ToolTip="Click to update the reference" 
                                                     Url="~/Images/icons/metro/22x18/edit18.png">
                                                 </Image>
                                                 <ClientSideEvents Click="onEditLogClick" />
                                            </dx:ASPxButton>
                        </td>
                    </tr>
                </tbody> 
            </table>
        </div>
        <!-- pagecontrol -->
        <div class="grid_16 pad_bottom">
            <dx:ASPxPageControl ID="dxpgeConsigment" ClientInstanceName="pgeConsigment" 
                runat="server" ActiveTabIndex="1" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                TabSpacing="0px" Width="100%">
                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanelImage>
                <ContentStyle>
                    <Paddings Padding="12px" />
                    <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                     <dx:TabPage Name="tabStep0" Text="Step 1: Enter consignment reference" 
                           ToolTip="Enter consignment reference">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <!-- end container grid -->
                                <table id="tblRef" cellpadding="5px" class="divcenter" width="625px">
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxLabel ID="dxlblTitleRef" runat="server" 
                                                ClientInstanceName="lblTitleRef" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Font-Size="X-Large" ForeColor="#333333" 
                                                Text="Step 1 of 2: Consignment details">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="dxlblRef" runat="server" ClientInstanceName="lblRef" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Text="Enter the consignment ref">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="dxtxtRef" runat="server" ClientInstanceName="txtRef" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="200px">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="dxbtnSave" runat="server" ClientInstanceName="btnSave" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" HorizontalAlign="Left" OnClick="dxbtnSave_Click" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                Text="Save and Continue" VerticalAlign="Middle" Wrap="False">
                                                <Image ToolTip="Click to save the reference" 
                                                    Url="~/Images/icons/metro/save.png">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl> 
                         </ContentCollection> 
                       </dx:TabPage> 
                    <dx:TabPage Name="tabStep1" Text="Step 2: Create a pallet" 
                           ToolTip="Enter the consignment ref">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                    <table id="tblContainer">
                                          <tbody>
                                              <tr>
                                                  <td>
                                                      <dx:ASPxLabel ID="dxlblContainer" runat="server" 
                                                          ClientInstanceName="lblContainer" 
                                                          CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                          CssPostfix="Office2010Blue" Text="Container number">
                                                      </dx:ASPxLabel>
                                                  </td>
                                                  <td>
                                                      <dx:ASPxComboBox ID="dxcboContainer" runat="server" CallbackPageSize="25" 
                                                          ClientInstanceName="cboContainer" 
                                                          CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                          CssPostfix="Office2010Blue" DropDownWidth="250px" EnableCallbackMode="True" 
                                                          IncrementalFilteringMode="StartsWith" 
                                                          OnItemRequestedByValue="dxcboContainer_ItemRequestedByValue" 
                                                          OnItemsRequestedByFilterCondition="dxcboContainer_ItemsRequestedByFilterCondition" 
                                                          Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                          TextField="ContainerNumber" Value="<%# Bind('ContainerID') %>" 
                                                          ValueField="ContainerID" ValueType="System.Int32" Width="250px">
                                                          <ClientSideEvents ValueChanged="onContainerValueChanged" />
                                                          <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                          </LoadingPanelImage>
                                                          <LoadingPanelStyle ImageSpacing="5px">
                                                          </LoadingPanelStyle>
                                                          <ButtonStyle Width="13px">
                                                          </ButtonStyle>
                                                      </dx:ASPxComboBox>
                                                  </td>
                                              </tr>
                                          </tbody>
                                    </table>
                                <div class="pad_bottom">
                                    <dx:ASPxGridView ID="dxgridContainer" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="gridContainer" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DataSourceID="linqdsContainer" 
                                        KeyFieldName="OrderIx" OnCustomCallback="dxgridContainer_CustomCallback" 
                                        TabIndex="1" Width="100%">
                                        <SettingsPager AlwaysShowPager="True" PageSize="100" Position="TopAndBottom">
                                        </SettingsPager>
                                        <ClientSideEvents CustomButtonClick="onContainerCustomButtonClick" 
                                            Init="onGridInit" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Select" VisibleIndex="0" 
                                                Width="50px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnSelect">
                                                        <Image Height="18px" ToolTip="Click to select" 
                                                            Url="../Images/icons/metro/22x18/edit18.png" Width="22px">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Publiship Ref" FieldName="OrderNumber" 
                                                Name="col_ordernumber" ReadOnly="True" VisibleIndex="1" Width="100px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title" ExportWidth="80" FieldName="Title" 
                                                Name="col_title" ReadOnly="True" VisibleIndex="2" Width="250px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ISBN" ExportWidth="70" FieldName="ISBN" 
                                                Name="col_isbn" ReadOnly="True" VisibleIndex="3" Width="100px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Delivery copies" ExportWidth="100" 
                                                FieldName="Copies" Name="col_copies" ReadOnly="True" VisibleIndex="4" 
                                                Width="110px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Full pallets" ExportWidth="100" 
                                                FieldName="FullPallets" Name="col_fullpallets" ReadOnly="True" VisibleIndex="5" 
                                                Width="110px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cartons/Full pallet" ExportWidth="100" 
                                                FieldName="CartonsPerFullPallet" Name="col_cfp" ReadOnly="True" 
                                                VisibleIndex="6" Width="120px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Part pallets" ExportWidth="100" 
                                                FieldName="PartPallets" Name="col_partpallets" ReadOnly="True" VisibleIndex="7" 
                                                Width="110px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cartons/Part pallet" ExportWidth="100" 
                                                FieldName="CartonsPerPartPallet" Name="col_cpp" ReadOnly="True" 
                                                VisibleIndex="8" Width="120px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Total weight" ExportWidth="100" 
                                                FieldName="TotalConsignmentWeight" Name="col_totalweight" ReadOnly="True" 
                                                VisibleIndex="9" Width="95px">
                                                <PropertiesTextEdit DisplayFormatString="F">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Total cube" ExportWidth="100" 
                                                FieldName="TotalConsignmentCube" Name="col_totalcube" ReadOnly="True" 
                                                VisibleIndex="10" Width="95px">
                                                <PropertiesTextEdit DisplayFormatString="F">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Ex-Works date" ExportWidth="75" 
                                                FieldName="ExWorksDate" Name="col_exworks" ReadOnly="True" VisibleIndex="11" 
                                                Width="110px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                                <Settings AutoFilterCondition="Equals" ShowFilterRowMenu="True" />
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Booking Received" ExportWidth="75" 
                                                FieldName="BookingReceived" Name="col_bookingreceived" ReadOnly="True" 
                                                VisibleIndex="12" Width="110px">
                                                <PropertiesTextEdit DisplayFormatString="{0:d}">
                                                </PropertiesTextEdit>
                                                <Settings AutoFilterCondition="Equals" ShowFilterRowMenu="True" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Cargo Ready" ExportWidth="75" 
                                                FieldName="CargoReady" Name="col_cargoready" ReadOnly="True" VisibleIndex="13" 
                                                Width="110px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                                <Settings AutoFilterCondition="Equals" ShowFilterRowMenu="True" />
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Vessel Name" ExportWidth="75" 
                                                FieldName="vessel_name" Name="col_vesselname" ReadOnly="True" VisibleIndex="14" 
                                                Width="120px">
                                                <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="ETS" ExportWidth="75" FieldName="ETS" 
                                                Name="col_ets" ReadOnly="True" VisibleIndex="15" Width="90px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="ETA" ExportWidth="75" FieldName="ETA" 
                                                Name="col_eta" ReadOnly="True" VisibleIndex="16" Width="90px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Status" ExportWidth="80" 
                                                FieldName="current_status" Name="col_status" ReadOnly="True" VisibleIndex="17" 
                                                Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="On" ExportWidth="90" FieldName="status_on" 
                                                Name="col_currentstatusdate" ReadOnly="True" VisibleIndex="18" Width="90px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="Last updated" ExportWidth="90" 
                                                FieldName="StatusDate" Name="col_last_updated" ReadOnly="True" 
                                                VisibleIndex="19" Width="95px">
                                                <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Customer" ExportWidth="75" 
                                                FieldName="client_name" Name="col_company" ReadOnly="True" VisibleIndex="20" 
                                                Width="150px">
                                                <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                                    ShowInFilterControl="True" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Container" ExportWidth="100" 
                                                FieldName="ContainerNumber" Name="col_container" ReadOnly="True" 
                                                VisibleIndex="21" Width="130px">
                                                <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="orderid" FieldName="OrderID" 
                                                Name="col_orderid" ReadOnly="True" ShowInCustomizationForm="False" 
                                                UnboundType="Integer" Visible="False" VisibleIndex="22" Width="0px">
                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                                    AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                                                    ShowInFilterControl="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Index Key" FieldName="OrderIx" 
                                                Name="col_orderix" ReadOnly="True" ShowInCustomizationForm="False" 
                                                Visible="False" VisibleIndex="23" Width="0px">
                                                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                                    AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                                    AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                            ColumnResizeMode="Control" />
                                        <Settings ShowFilterRow="false" ShowHorizontalScrollBar="True" 
                                            ShowTitlePanel="True" ShowVerticalScrollBar="True" 
                                            VerticalScrollableHeight="200" />
                                        <SettingsText Title="Titles" />
                                        <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                            <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <ImagesFilterControl>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                            <LoadingPanel ImageSpacing="5px">
                                            </LoadingPanel>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesPager>
                                            <PageNumber ForeColor="#3E4846">
                                            </PageNumber>
                                            <Summary ForeColor="#1E395B">
                                            </Summary>
                                        </StylesPager>
                                        <StylesEditors ButtonEditCellSpacing="0">
                                            <ProgressBar Height="21px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </div>
                                <!-- consignment ref in put -->
                                    <div class="pad_bottom">
                                        <dx:ASPxLabel ID="dxlblPalletItems" runat="server" 
                                            ClientInstanceName="lblPalletItems" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Font-Size="X-Large" ForeColor="#333333" 
                                            Text="Selected items">
                                        </dx:ASPxLabel>
                                </div>
                                <!-- items grid -->
                                <div class="pad_bottom">
                                    <dx:ASPxGridView ID="dxgrdItemDetails" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="grdItemDetails" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" KeyFieldName="SRR_item_id" 
                                        OnCustomCallback="dxgrdItemDetails_CustomCallback" 
                                        OnRowDeleting="dxgrdItemDetails_RowDeleting" 
                                        OnRowUpdating="dxgrdItemDetails_RowUpdating" Width="100%">
                                        <ClientSideEvents CustomButtonClick="onCustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="0">
                                                <EditButton Visible="True">
                                                    <Image AlternateText="Edit" Height="18px" ToolTip="Edit" 
                                                        Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                                    </Image>
                                                </EditButton>
                                                <NewButton Visible="True">
                                                    <Image AlternateText="New" Height="18px" ToolTip="New" 
                                                        Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                                    </Image>
                                                </NewButton>
                                                <DeleteButton Visible="True">
                                                    <Image AlternateText="Delete" Height="18px" ToolTip="Delete" 
                                                        Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                                    </Image>
                                                </DeleteButton>
                                                <CancelButton Visible="True">
                                                    <Image AlternateText="Cancel" Height="18px" ToolTip="Cancel" 
                                                        Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                                                    </Image>
                                                </CancelButton>
                                                <UpdateButton Visible="True">
                                                    <Image AlternateText="Update" Height="18px" ToolTip="Update" 
                                                        Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                                    </Image>
                                                </UpdateButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="item_id" Visible="False" VisibleIndex="1" 
                                                Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ref" FieldName="publiship_ref" 
                                                VisibleIndex="2" Width="100px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ISBN" FieldName="isbn" VisibleIndex="3" 
                                                Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Impression" FieldName="impression" 
                                                VisibleIndex="4" Width="75px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title" FieldName="title" VisibleIndex="5" 
                                                Width="350px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title height" FieldName="height" 
                                                VisibleIndex="7" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title width" FieldName="width" 
                                                VisibleIndex="8" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title depth" FieldName="depth" 
                                                VisibleIndex="9" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Title weight" FieldName="unit_net_weight" 
                                                VisibleIndex="10" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="No. of cartons" FieldName="parcel_count" 
                                                VisibleIndex="11" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Qtr per carton" 
                                                FieldName="units_per_parcel" VisibleIndex="12" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cartons per layer" 
                                                FieldName="parcels_per_layer" VisibleIndex="13" Width="110px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="No. of odds" FieldName="odds_count" 
                                                VisibleIndex="14" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Carton height" FieldName="parcel_height" 
                                                VisibleIndex="15" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Carton width" FieldName="parcel_width" 
                                                VisibleIndex="16" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Carton depth" FieldName="parcel_depth" 
                                                VisibleIndex="17" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Carton weight" 
                                                FieldName="parcel_unitgrossweight" VisibleIndex="18" Width="95px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Full pallets" FieldName="full_pallets" 
                                                VisibleIndex="19" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Books per full pallet" 
                                                FieldName="units_full" VisibleIndex="20" Width="125px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Part pallets" FieldName="part_pallets" 
                                                VisibleIndex="20" Width="80px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Books per part pallet" 
                                                FieldName="units_part" VisibleIndex="21" Width="135px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Pearson ref" 
                                                FieldName="buyers_order_number" VisibleIndex="22" Width="125px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Printers Ref" 
                                                FieldName="printers_job_number" VisibleIndex="23" Width="125px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="24">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="cmdUpdate" Visibility="EditableRow">
                                                        <Image AlternateText="Update" Height="18px" ToolTip="Cancel" 
                                                            Url="../Images/icons/metro/22x18/save18.png" Width="22px">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="cmdCancel" Visibility="EditableRow">
                                                        <Image AlternateText="Cancel" Height="18px" ToolTip="Cancel" 
                                                            Url="../Images/icons/metro/22x18/cancel18.png" Width="22px">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowHorizontalScrollBar="True" ShowVerticalScrollBar="true" VerticalScrollableHeight="350"/>
                                        <SettingsDetail ShowDetailRow="True" />
                                        <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                            <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <ImagesFilterControl>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                            <LoadingPanel ImageSpacing="5px">
                                            </LoadingPanel>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesPager>
                                            <PageNumber ForeColor="#3E4846">
                                            </PageNumber>
                                            <Summary ForeColor="#1E395B">
                                            </Summary>
                                        </StylesPager>
                                        <StylesEditors ButtonEditCellSpacing="0">
                                            <ProgressBar Height="21px">
                                            </ProgressBar>
                                        </StylesEditors>
                                        <Templates>
                                            <DetailRow>
                                                <dx:ASPxGridView ID="dxgridPalletIds" runat="server" 
                                                    AutoGenerateColumns="False" ClientInstanceName="gridPalletIds" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" KeyFieldName="ContainerSubID" 
                                                    onbeforeperformdataselect="dxgridPalletIds_BeforePerformDataSelect" 
                                                    Width="100%">
                                                    <SettingsBehavior AllowGroup="False" EnableRowHotTrack="True" />
                                                    <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue">
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                        <LoadingPanel ImageSpacing="5px">
                                                        </LoadingPanel>
                                                    </Styles>
                                                    <SettingsPager PageSize="20" Position="TopAndBottom">
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
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Index key" FieldName="pallet_id" 
                                                            Name="colPallet_id" ReadOnly="True" ShowInCustomizationForm="False" 
                                                            Visible="False" VisibleIndex="0" Width="0px">
                                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                                                AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Pallet identifier (SSCC)" FieldName="sscc" 
                                                            Name="colsscc" ReadOnly="True" VisibleIndex="1" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Pallet type" FieldName="pallet_type" 
                                                            Name="colpalettype" ReadOnly="True" VisibleIndex="2" Width="80px">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <StylesPager>
                                                        <PageNumber ForeColor="#3E4846">
                                                        </PageNumber>
                                                        <Summary ForeColor="#1E395B">
                                                        </Summary>
                                                    </StylesPager>
                                                    <Settings ShowHorizontalScrollBar="True" />
                                                    <StylesEditors ButtonEditCellSpacing="0">
                                                        <ProgressBar Height="21px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </DetailRow>
                                        </Templates>
                                    </dx:ASPxGridView>
                                </div>
                                <div class="pad">
                                    <div class="divright">
                                        <dx:ASPxButton ID="dxbtnComplete" runat="server" CausesValidation="False" 
                                            ClientInstanceName="btnComplete" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                            OnClick="dxbtnComplete_Click" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            Text="Finish" VerticalAlign="Middle" Wrap="False">
                                            <Image ToolTip="Click to finish despatch note" Url="~/Images/icons/metro/print.png">
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                                </div>
                                <!-- end buttons -->
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    	<dx:TabPage Name="tabStep3" Text="Update the consignment reference">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <!-- consignment ref edit -->
                                    <table id="tblRefEdit" width="625px" cellpadding="5px" class="divcenter">
                                          <tr>
                                             <td colspan="3">
                                                  <dx:ASPxLabel ID="dxlblTitleRefEdit" runat="server" 
                                                     ClientInstanceName="lblTitleRefEdit" 
                                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                     CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                                     Text="Update the consignment reference" ForeColor="#333333">
                                                 </dx:ASPxLabel>
                                             </td>
                                          </tr>
                                          <tr>
                                            <td>
                                             <dx:ASPxLabel ID="dxlblRefEdit" ClientInstanceName="lblRefEdit" runat="server" 
                                                    Text="Enter the consignment ref" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue">
                                            </dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                 <dx:ASPxTextBox ID="dxtxtRefEdit" ClientInstanceName="txtRefEdit" runat="server" 
                                                     Width="200px" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                     CssPostfix="Office2010Blue" 
                                                     SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                            </dx:ASPxTextBox>
                                            </td>
                                           </tr>
                                            <tr> 
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <dx:ASPxButton ID="dxbtnUpdate" runat="server" ClientInstanceName="btnUpdate" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" HorizontalAlign="Left" OnClick="dxbtnUpdate_Click" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Update" 
                                                    VerticalAlign="Middle" Wrap="False">
                                                    <Image ToolTip="Click to update" Url="~/Images/icons/metro/save.png">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                                <td>
                                                    <dx:ASPxButton ID="dxbtnCancelUpdate" runat="server" AutoPostBack="False" 
                                                        CausesValidation="False" ClientInstanceName="btnCancelUpdate" 
                                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Cancel" 
                                                        VerticalAlign="Middle" Wrap="False">
                                                        <ClientSideEvents Click="onCancelUpdateRefClick" />
                                                        <Image ToolTip="Click to update" Url="~/Images/icons/metro/cancel.png">
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                          </tr>  
                                    </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
            </dx:ASPxPageControl>
        </div> 
        <div class="clear"></div>   
        <div class="grid_16">
            
        </div> 
        <div class="clear"></div>   
        <!-- for holding hidden fields or other controls that don't need positioning -->
        <div class="grid_4">
            <dx:ASPxHiddenField ID="dxhfIds" ClientInstanceName="hfIds" runat="server">
            </dx:ASPxHiddenField>
        </div> 
        <div class="grid_4">
            <dx:LinqServerModeDataSource ID="linqdsContainer" runat="server" 
                ContextTypeName="linq.linq_container_contentDataContext" 
                TableName="view_container_contents" />
        </div>
        <div class="grid_8">
        </div>
        <div class="clear"></div>  
        <div class="grid_6"></div> 
        <div class="grid_10">  
        <dx:ASPxPopupControl ID="dxpopDefault" ClientInstanceName="popDefault" 
                runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" EnableHotTrack="False"  AllowDragging="True" 
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                AppearAfter="200" HeaderText="Add item" 
                Modal="True" CloseAction="CloseButton" EnableViewState="False">
            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
            </LoadingPanelImage>
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <LoadingPanelStyle ImageSpacing="5px">
            </LoadingPanelStyle>
            <Windows>
                <dx:PopupWindow Name="Cartons" Height="525px" MinHeight="525px" Modal="true"   
                    MinWidth="645px" Width="645px">
                     <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">
            
                     </dx:PopupControlContentControl>
                 </ContentCollection>
                </dx:PopupWindow> 
            </Windows>
        </dx:ASPxPopupControl>
        </div>
     </div> <!-- end grid 16 -->
</asp:Content>

