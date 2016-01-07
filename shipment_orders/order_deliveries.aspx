<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_deliveries.aspx.cs" Inherits="order_deliveries" %>

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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx1" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onCompanySelected(s, e) {
            
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblDeliveryAddressSub.SetText(s1);
            //pallet dims
            s1 = s.GetSelectedItem().GetColumnText('PalletDims');
            lblPalletSpecSub.SetText(s1);
            //pallet weight            
            s1 = s.GetSelectedItem().GetColumnText('MaxPalletWeight');
            lblPalletWeightSub.SetText(s1);
            //pallet height
            s1 = s.GetSelectedItem().GetColumnText('MaxPalletHeight');
            lblPalletHeightSub.SetText(s1);
            //delivery instructions
            s1 = s.GetSelectedItem().GetColumnText('SpecialDeliveryInstructions');
            lblInstructionsSub.SetText(s1);
        }

        function onEditCompany(s, e) {
            
            var pid = cboAddress.GetValue();
            if (pid != null) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);

                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert('You have not selected a delivery address');
            }
        }

        function onNewCompany(s, e) {
            
            var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
            pcPodEdit.SetWindowContentUrl(winEdit, '');
            pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Insert&pid=' + "new");

            pcPodEdit.ShowWindow(winEdit);
        }

        function onCurrentStatusChanged(s, e) {
            //change date of status last changed
            dtStatusDate.SetDate(new Date());

        }
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }

        function onCompanyButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblDeliveryAddressSub.SetText('');
                //pallet dims
                lblPalletSpecSub.SetText('');
                //pallet weight
                lblPalletWeightSub.SetText('');
                //pallet height
                lblPalletHeightSub.SetText('');
                //delivery instructions
                lblInstructionsSub.SetText('');
            }
        }

        function AddTitles_click(s, visibleindex, e) {
            gridDeliveries.PerformCallback('AddTitles|' + visibleindex.toString() );
        }
        
        //not operational - using AddTitles_click
        function OnMenuItemClick(s, e) {
            switch (e.item.name) {
                case "jsAddTitles": 
                    gridDeliveries.PerformCallback('AddTitles');
                    break;
            }
        }
        //********************
        // ]]>
    </script>    

    
    <div class="container_16">
        <!-- messages -->
        <div class="grid_16">
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
       
        <!-- Using tables for dataform layout. Semantically not a good choice but other options e.g. definition lists
             do not render properly in older versions of internet explorer < IE7. Also multi-column combos do not
             render correctly when placed in definition lists in < IE7  -->
        
        <div class="grid_10">
            <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/shipment_orders/order_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" Text="Order No|">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblOrderNo" runat="server" ClientInstanceName="lblOrderNo" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlbOrderDetails1" runat="server" 
                            ClientInstanceName="lblOrderDetails1" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="| Delivery details">
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <div class="divright">
              <dx:ASPxImage ID="dximgJobPubliship" runat="server" 
                            AlternateText="Publiship Job" ClientInstanceName="imgJobPubliship" 
                            Height="26px" ImageAlign="Top" ImageUrl="~/Images/icons/metro/checked_checkbox.png" 
                            Width="26px">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobPubliship" runat="server" 
                            ClientInstanceName="lblJobPubliship" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Publiship job">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
              <dx:ASPxImage ID="dximgJobHot" runat="server" AlternateText="Hot Job" 
                            ClientInstanceName="imgJobHot" Height="26px" 
                            ImageUrl="~/Images/icons/metro/matches.png" Width="26px" 
                            ImageAlign="Top">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobHot" runat="server" 
                    ClientInstanceName="lblJobHot" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Hot job" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
                         <dx:ASPxImage ID="dximgJobClosed" runat="server" AlternateText="Job Closed" 
                            ClientInstanceName="imgJobClosed" Height="26px" 
                            ImageUrl="~/Images/icons/metro/lock.png" Width="26px" 
                ImageAlign="Top">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobClosed" runat="server" 
                            ClientInstanceName="lblJobClosed" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="closed" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div> 
         </div>              
        <div class="clear"></div>
        
        <!-- tabs -->
        <div class="grid_16">
         <dx:ASPxTabControl ID="dxtabOrder" runat="server" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                TabSpacing="0px" ondatabound="dxtabOrder_DataBound">
                            <ContentStyle>
                                <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                            </ContentStyle>
                            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                        </dx:ASPxTabControl>
        </div> 
        <div class="clear"></div>
       
        <!-- edit/insert delivery details -->
        <div class="grid_16 pad bottom">
            <dx:ASPxGridView ID="dxgridDeliveries" runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" 
                ClientInstanceName="gridDeliveries" KeyFieldName="DeliveryID" Width="100%" 
                onhtmlrowprepared="dxgridDeliveries_HtmlRowPrepared" 
                onhtmleditformcreated="dxgridDeliveries_HtmlEditFormCreated"
                OnDataBound="dxgridDeliveries_OnDataBound"
                onrowinserting="dxgridDeliveries_RowInserting" 
                onrowupdating="dxgridDeliveries_RowUpdating" 
                onrowdeleting="dxgridDeliveries_RowDeleting" 
                oncustomcallback="dxgridDeliveries_CustomCallback">
                 <SettingsBehavior ConfirmDelete="True" />
                 <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager PageSize="1" Position="TopAndBottom">
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
                <SettingsEditing Mode="EditForm" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image">
                       <EditButton Visible="True">
                            <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                            </Image>
                       </EditButton>
                        <NewButton Visible="True">
                            <Image AlternateText="New" ToolTip="New" Height="18px" 
                                Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                            </Image>
                        </NewButton>
                        <DeleteButton Visible="True">
                            <Image AlternateText="Delete" ToolTip="Delete" Height="18px" 
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
                    <dx:GridViewDataTextColumn FieldName="DeliveryID" VisibleIndex="1" 
                        Caption="Delivery ID" Name="colDeliveryID" ReadOnly="True">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DeliveryNoteID" VisibleIndex="2" 
                        Caption="Delivery note ID" Name="colDeliveryNoteID" ReadOnly="True">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="3" 
                        Caption="Order number" Name="colOrderNumber" ReadOnly="True">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="StatusDate" VisibleIndex="4" 
                        Caption="Status date" Name="colStatusDate">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="CurrentStatusID" 
                        Name="colCurrentStatusID" VisibleIndex="5">
                        <PropertiesComboBox Spacing="0" ValueType="System.Int32">
                            <ClientSideEvents SelectedIndexChanged="onCurrentStatusChanged" />
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="CurrentStatusDate" VisibleIndex="6" 
                        Caption="Current status date" Name="colCurrentStatusDate">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="DeliveryAddress" VisibleIndex="7" 
                        Caption="Delivery address" Name="colDeliveryAddress">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="Delivered" VisibleIndex="8" 
                        Caption="Delivered" Name="colDelivered">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn FieldName="PODRequired" VisibleIndex="9" 
                        Caption="POD required" Name="colPODRequired">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="SpecialInstructions" VisibleIndex="10" 
                        Name="colSpecialInstructions" ReadOnly="True">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PODurl" VisibleIndex="11" 
                        Caption="POD url" Name="colPODurl">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="Added" VisibleIndex="12" Caption="Added" 
                        Name="colAdded" ReadOnly="True">
                    </dx:GridViewDataCheckColumn>
                </Columns>
                <Templates>
                    <DataRow>
                       <div class="pad_bottom"> 
                       <table id="tblDeliveriesView" cellpadding="5px" border="0" width="100%" class="viewTableNoBorder">
                             <colgroup>
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                            </colgroup> 
                            <tbody>
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblDeliveryRef" ClientInstanceName="lblDeliveryRef" 
                                            runat="server" Text="Delivery ref"  
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryID" ClientInstanceName="lblDeliveryID" 
                                            runat="server"  Text='<%# Bind("DeliveryID") %>' 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryAddressView" ClientInstanceName="lblDeliveryAddressView" 
                                            runat="server" Text="Delivery address" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                       <dx:ASPxLabel ID="dxlblDeliveryName" ClientInstanceName="lblDeliveryName" 
                                            runat="server" Text="" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                       <dx:ASPxLabel ID="dxlblCurrentStatusIDView" ClientInstanceName="blCurrentStatusIDView" 
                                            runat="server" Text="Current status" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblCurrentStatusID" ClientInstanceName="lblCurrentStatusID" 
                                            runat="server" Text="" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryNoteRef" ClientInstanceName="lblDeliveryNoteRef" 
                                            runat="server" Text="Delivery note ref" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryNoteID" ClientInstanceName="lblDeliveryNoteID" 
                                            runat="server"  Text='<%# Bind("DeliveryNoteID") %>'
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td></td>
                                    <td rowspan="3">
                                          <dx:ASPxLabel ID="dxlblDeliveryAddress" ClientInstanceName="lblDeliveryAddress" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblSpecialInstructionsView" ClientInstanceName="lblSpecialInstructionsView" 
                                            runat="server" Text="Delivery instructions" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td rowspan="3">
                                          <dx:ASPxLabel ID="dxlblInstructions" ClientInstanceName="lblInstructions" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text='<%# Bind("SpecialInstructions") %>'>
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                          &nbsp;</td>
                                </tr>
                                <tr class="row_divider">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          &nbsp;</td>
                                </tr>
                                <tr class="row_divider">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletSpecView" ClientInstanceName="lblPalletSpecView" 
                                            runat="server" Text="Pallet spec" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletSpec" ClientInstanceName="lblPalletSpec" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblCurrentStatusDateView" ClientInstanceName="lblCurrentStatusDateView" 
                                            runat="server" Text="Status date" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblCurrentStatusDate" ClientInstanceName="lblCurrentStatusDate" 
                                            runat="server"  Text='<%# Bind("CurrentStatusDate") %>' 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletWeightView" ClientInstanceName="lblPalletWeightView" 
                                            runat="server" Text="Max pallet weight" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletWeight" ClientInstanceName="lblPalletWeight" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPodRequired" runat="server" 
                                              ClientInstanceName="lblPodRequired" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="POD required">
                                          </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxCheckBox ID="dxdckPODRequired" runat="server" CheckState="Unchecked" 
                                              ClientEnabled="False" ClientInstanceName="ckPODRequired" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Value='<%# Bind("PODRequired") %>'>
                                          </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletHeightView" ClientInstanceName="lblPalletHeightView" 
                                            runat="server" Text="Max pallet height" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletHeight" ClientInstanceName="lblPalletHeight" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryCompleted" runat="server" 
                                              ClientInstanceName="lblDeliveryCompleted" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Delivery completed">
                                          </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxCheckBox ID="dxckDelivered" runat="server" CheckState="Unchecked" 
                                            ClientEnabled="False" ClientInstanceName="ckDelivered" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            Value='<%# Bind("Delivered") %>'>
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblStatusDateView" runat="server" 
                                            ClientInstanceName="lblStatusDateView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Status last updated">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblStatusDate" runat="server" 
                                            ClientInstanceName="lblStatusDate" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text='<%# Bind("StatusDate") %>'>
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody> 
                        </table>   
                        </div>
                         <div>
                        <table>
                            <tbody>
                            <tr>
                                <td> 
                                    <dx:ASPxButton ID="dxbtnEdit" ClientInstanceName="btnEdit" runat="server" AutoPostBack="False"  
                                ClientSideEvents-Click='<%# "function(s, e) { gridDeliveries.StartEditRow(" + Container.VisibleIndex + "); }" %>' 
                                CausesValidation="False" Cursor="pointer" EnableDefaultAppearance="False" 
                                EnableTheming="False" Font-Underline="True" ForeColor="#333435">
                                        <HoverStyle ForeColor="#0076E8">
                                        </HoverStyle>
                                        <Image AlternateText="Edit" ToolTip="Edit" Url="~/Images/icons/metro/22x18/edit18.png">
                                        </Image>
                                </dx:ASPxButton>
                                </td>
                                <td> 
                                    <dx:ASPxButton ID="dxbtnNew" runat="server" AutoPostBack="False" 
                                CausesValidation="False" ClientInstanceName="btnNew" 
                                EnableDefaultAppearance="False" Font-Underline="True" ForeColor="#333435" 
                                Cursor="pointer">
                                        <HoverStyle ForeColor="#0076E8">
                                        </HoverStyle>
                                         <Image AlternateText="New" ToolTip="New" Url="~/Images/icons/metro/22x18/add_row18.png">
                                        </Image>
                                <ClientSideEvents Click="function(s, e) { gridDeliveries.AddNewRow(); }" />
                            </dx:ASPxButton>
                                </td>
                                <td>
                                  <dx:ASPxButton ID="dxbtnDelete" runat="server" AutoPostBack="False" 
                                   ClientSideEvents-Click='<%# "function(s, e) {  gridDeliveries.DeleteRow(" + Container.VisibleIndex + "); }" %>' 
                                    CausesValidation="False" ClientInstanceName="btnDelete" 
                                    EnableDefaultAppearance="False" Font-Underline="True" ForeColor="#333435" 
                                    Cursor="pointer">
                                         <Image AlternateText="Remove" ToolTip="Remove" Url="~/Images/icons/metro/22x18/delete_row18.png">
                                        </Image>
                                        <HoverStyle ForeColor="#0076E8">
                                        </HoverStyle>
                                 </dx:ASPxButton> 
                                </td>
                                  <td>
                                  <dx:ASPxButton ID="dxbtnAddTitles" runat="server" AutoPostBack="False" 
                                    ClientSideEvents-Click='<%# "function(s, e) {  AddTitles_click(s," + Container.VisibleIndex + ",e); }" %>'  
                                    CausesValidation="False" ClientInstanceName="btnAddTitles" 
                                    EnableDefaultAppearance="False" Font-Underline="True" ForeColor="#333435" 
                                    Cursor="pointer">
                                         <Image AlternateText="Add Titles" ToolTip="Add Titles" Url="~/Images/icons/metro/22x18/book18.png">
                                        </Image>
                                        <HoverStyle ForeColor="#0076E8">
                                        </HoverStyle>
                                 </dx:ASPxButton> 
                                </td>
                            </tr>
                            </tbody>
                        </table>
                        </div>
                    </DataRow> 
                    <EditForm>
                        <div>
                         <table id="tblDeliveriesEdit" cellpadding="5px" border="0" width="100%" class="viewTableNoBorder">
                             <colgroup>
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                            </colgroup> 
                            <tbody>
                                <tr class="row_divider">
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryRef" ClientInstanceName="lblDeliveryRef" 
                                            runat="server" Text="Delivery ref" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                       <dx:ASPxGridViewTemplateReplacement ID="dxrpCompany" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colDeliveryID" runat="server" 
                                        Enabled="false" TabIndex="0">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDeliveryAddressTitle" ClientInstanceName="lblDeliveryAddress" 
                                            runat="server" Text="Delivery address" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="dxcboAddress" 
                                            ClientInstanceName="cboAddress" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("DeliveryAddress") %>' 
                                        onitemrequestedbyvalue="dxcboAddress_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcboAddress_ItemsRequestedByFilterCondition" 
                                             Spacing="0" DropDownRows="10" TabIndex="2" >
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                         <ClientSideEvents ButtonClick="onCompanyButtonClick" SelectedIndexChanged="function(s, e) {
	                                            onCompanySelected(s, e); }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="120px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="120px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="120px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="120px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
                                            <dx:ListBoxColumn Caption="Pallet spec" FieldName="PalletDims" 
                                                Name="colPalletDims" Width="75px" />
                                            <dx:ListBoxColumn Caption="Max plt weight" FieldName="MaxPalletWeight" 
                                                Name="colMaxPalletWeight" Width="75px" />
                                            <dx:ListBoxColumn Caption="Max plt height" FieldName="MaxPalletHeight" 
                                                Name="colMaxPalletHeight" Width="75px" />
                                            <dx:ListBoxColumn Caption="Instructions" FieldName="SpecialDeliveryInstructions" 
                                                Name="colSpecialDeliveryInstructions" Width="75px" />
                                        </Columns>
                                         <Buttons>
                                            <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                                <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                                </Image>
                                            </dx:EditButton>
                                        </Buttons> 
                                        </dx:ASPxComboBox> 
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblStatusID" ClientInstanceName="lblStatusID" 
                                            runat="server" Text="Current status" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" 
                                            ReplacementType="EditFormCellEditor" ColumnID="colCurrentStatusID" runat="server" 
                                            Enabled="true" TabIndex="3">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <dx:ASPxHyperLink ID="dxlinkNewCompany" runat="server" 
                                            ClientInstanceName="linkNewCompany"  
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                            <ClientSideEvents Click="onNewCompany" />
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="dxlinkEditCompany" runat="server" 
                                            ClientInstanceName="linkEditCompany" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                            <ClientSideEvents Click="onEditCompany" />
                                        </dx:ASPxHyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                       <dx:ASPxLabel ID="dxlblDeliveryNoteRef" ClientInstanceName="lblDeliveryNoteRef" 
                                            runat="server" Text="Delivery note ref" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                         <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colDeliveryNoteID" runat="server" 
                                        Enabled="true" TabIndex="1">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                    <td></td>
                                    <td rowspan="3">
                                          <dx:ASPxLabel ID="dxlblDeliveryAddressSub" ClientInstanceName="lblDeliveryAddressSub" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblSpecialInstructionsTitle" ClientInstanceName="lblSpecialInstructionsTitle" 
                                            runat="server" Text="Delivery instructions" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td rowspan="3">
                                          <dx:ASPxLabel ID="dxlblInstructionsSub" ClientInstanceName="lblInstructionsSub" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text='<%# Bind("SpecialInstructions") %>'>
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                          &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletSpecTitle" ClientInstanceName="lblPalletSpecTitle" 
                                            runat="server" Text="Pallet spec" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletSpecSub" ClientInstanceName="lblPalletSpecSub" 
                                            runat="server"
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblCurrentStatusDateTitle" ClientInstanceName="lblCurrentStatusDateTitle" 
                                            runat="server" Text="Status date" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement3"
                                        ReplacementType="EditFormCellEditor" ColumnID="colCurrentStatusDate" runat="server" 
                                        Enabled="true" TabIndex="4">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletWeightTitle" ClientInstanceName="lblPalletWeightTitle" 
                                            runat="server" Text="Max pallet weight" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPalletWeightSub" ClientInstanceName="lblPalletWeightSub" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblPodRequired" runat="server" 
                                              ClientInstanceName="lblPodRequired" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="POD required">
                                          </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement4" 
                                            runat="server" ColumnID="colPODRequired" Enabled="true" 
                                            ReplacementType="EditFormCellEditor" TabIndex="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>&nbsp;<dx:ASPxLabel ID="dxlblPalletHeightTitle" ClientInstanceName="lblPalletHeightTitle" 
                                            runat="server" Text="Max pallet height" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>&nbsp;<dx:ASPxLabel ID="dxlblPalletHeightSub" ClientInstanceName="lblPalletHeightSub" 
                                            runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                          <dx:ASPxLabel ID="dxlblDelivered" runat="server" 
                                              ClientInstanceName="lblDelivered" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Delivered">
                                          </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement5" 
                                            runat="server" ColumnID="colDelivered" Enabled="true" 
                                            ReplacementType="EditFormCellEditor" TabIndex="6" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblStatusDateTitle" runat="server" 
                                            ClientInstanceName="lblStatusDateTitle" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Status last updated">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dxdtStatusDate" runat="server" 
                                            ClientInstanceName="dtStatusDate" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Spacing="0" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            Value='<%# Bind("StatusDate") %>' TabIndex="7">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </tbody> 
                        </table> 
                        </div>
                        <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormUpdateButton" />
                        <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormCancelButton" />
                      </div> 
                    </EditForm>
                    <DetailRow>
                            <dx:ASPxGridView ID="dxgridTitles" runat="server" AutoGenerateColumns="False" 
                                ClientInstanceName="gridTitles" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"
                                KeyFieldName="SubDeliveryID" 
                                onbeforeperformdataselect="dxgridTitles_BeforePerformDataSelect" 
                                onrowinserting="dxgridTitles_RowInserting" 
                                onrowupdating="dxgridTitles_RowUpdating" OnRowDeleting="dxgridTitles_RowDeleting"  
                                Width="100%" 
                                onstartrowediting="dxgridTitles_StartRowEditing">
                                <SettingsBehavior ConfirmDelete="True" />
                                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                    <LoadingPanel ImageSpacing="5px">
                                    </LoadingPanel>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
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
                                <SettingsText Title="Titles" />
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Width="125px" ButtonType="Image">
                                        <EditButton Visible="True">
                                            <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                                Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                            </Image>
                                        </EditButton>
                                        <NewButton Visible="True">
                                            <Image AlternateText="New" ToolTip="New" Height="18px" 
                                                Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                            </Image>
                                        </NewButton>
                                        <DeleteButton Visible="True">
                                            <Image AlternateText="Delete" ToolTip="Delete" Height="18px" 
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
                                    <dx:GridViewDataTextColumn FieldName="SubDeliveryID" Name="colSubDeliveryID" 
                                        Visible="False" VisibleIndex="1" Width="0px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Title" FieldName="Title" 
                                        Name="colTitle" VisibleIndex="4" Width="260px">
                                        <PropertiesComboBox Spacing="0" ValueType="System.String" >
                                        </PropertiesComboBox>
                                        <EditItemTemplate>
                                            <dx:ASPxComboBox ID="dxcbotitle" runat="server" Width="225px"
                                            OnItemRequestedByValue="dxcbotitle_ItemRequestedByValue" 
                                            OnItemsRequestedByFilterCondition="dxcbotitle_ItemsRequestedByFilterCondition" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            CallbackPageSize="30" EnableCallbackMode="True"  
                                            IncrementalFilteringMode="StartsWith" TextField="Title" 
                                            ValueField="TitleID"
                                            ValueType="System.Int32" Spacing="0"
                                            ondatabound="dxcbotitle_DataBound">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataTextColumn Caption="Copies" FieldName="Copies" Name="ColCopies" 
                                        VisibleIndex="7" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Remarks" FieldName="Remarks" 
                                        Name="colRemarks" VisibleIndex="22" Width="200px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Copies per carton" 
                                        FieldName="CopiesPerCarton" Name="colCopiesPerCarton" VisibleIndex="8" 
                                        Width="120px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Total weight" 
                                        FieldName="TotalConsignmentWeight" Name="colTotalConsignmentWeight" 
                                        VisibleIndex="20" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Total cube" 
                                        FieldName="TotalConsignmentCube" Name="colTotalConsignmentCube" 
                                        VisibleIndex="21" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ctn length" FieldName="CartonLength" 
                                        Name="CartonLength" VisibleIndex="9" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ctn depth" FieldName="CartonDepth" 
                                        Name="colCartonDepth" VisibleIndex="10" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ctn height" FieldName="CartonHeight" 
                                        Name="colCartonHeight" VisibleIndex="11" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Total full cartons" 
                                        FieldName="TotalCartons" Name="colTotalCartons" VisibleIndex="12" 
                                        Width="110px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ctn weight" FieldName="CartonWeight" 
                                        Name="colCartonWeight" VisibleIndex="13" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Copies last carton" FieldName="LastCarton" 
                                        Name="colLastCarton" VisibleIndex="14" Width="120px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Jackets" FieldName="Jackets" 
                                        Name="colJackets" VisibleIndex="18" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Full plts" FieldName="FullPallets" 
                                        Name="colFullPallets" VisibleIndex="16" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cartons full pallet" 
                                        FieldName="CartonsPerFullPallet" Name="colCartonsPerFullPallet" 
                                        VisibleIndex="15" Width="120px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Part pallets" FieldName="PartPallets" 
                                        Name="colPartPallets" VisibleIndex="19" Width="90px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cartons part pallet" 
                                        FieldName="CartonsPerPartPallet" Name="colCartonsPerPartPallet" 
                                        VisibleIndex="17" Width="120px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Actual PPC" FieldName="ActualPPC" 
                                        Name="colActualPPC" VisibleIndex="5" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn Caption="Estimated PPC" FieldName="EstimatedPPC" 
                                        Name="colEstimatedPPC" VisibleIndex="18" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                   <dx:GridViewDataTextColumn Caption="Book length" FieldName="BookLength" 
                                        Name="colBookLength" VisibleIndex="19" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Book width" FieldName="BookWidth" 
                                        Name="colBookWidth" VisibleIndex="20" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Book depth" FieldName="BookDepth" 
                                        Name="colBookDepth" VisibleIndex="21" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Book weight" FieldName="BookWeight" 
                                        Name="colBookWeight" VisibleIndex="22" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <StylesPager>
                                    <PageNumber ForeColor="#3E4846">
                                    </PageNumber>
                                    <Summary ForeColor="#1E395B">
                                    </Summary>
                                </StylesPager>
                                <Settings ShowHorizontalScrollBar="True" ShowTitlePanel="True" />
                                <StylesEditors ButtonEditCellSpacing="0">
                                    <ProgressBar Height="21px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                    </DetailRow> 
                </Templates>
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowColumnHeaders="False" ShowGroupButtons="False" 
                    ShowStatusBar="Hidden" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
                <SettingsDetail ShowDetailRow="True" ShowDetailButtons="False" />
            </dx:ASPxGridView>
        </div> 
        <div class="clear"></div>
        <!-- custom command butons for formview -->
        <div class="grid_16 pad_bottom">
           <dx:ASPxMenu ID="dxmnuCommand" runat="server" 
                ClientInstanceName="mnuCommand" width="100%" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                AutoSeparators="RootOnly" 
                ItemAutoWidth="False" onitemdatabound="dxmnuCommand_ItemDataBound">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <ClientSideEvents ItemClick="OnMenuItemClick" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
        </div>
        <div class="clear"></div>
        <div class="grid_16 pad_bottom">
              <dx1:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
              </dx1:ASPxHiddenField>
        </div>
        <div>
             <dx:ASPxPopupControl ID="dxpcPodEdit" ClientInstanceName="pcPodEdit" 
                runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" EnableHotTrack="False" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                PopupAction="None" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter">
                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanelImage>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <Windows>
                     <dx:PopupWindow CloseAction="CloseButton" 
                        ContentUrl="../Popupcontrol/order_name_address.aspx" HeaderText="Company details"
                        Height="720px" Modal="True" Name="CompanyDetails" PopupAction="None" 
                        Width="1000px" PopupElementID="dxbtnmore">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                </Windows>
            </dx:ASPxPopupControl>
        </div>
    </div><!-- end container -->
 </asp:Content>
