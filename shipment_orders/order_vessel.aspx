<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_vessel.aspx.cs" Inherits="order_vessel" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onVesselButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //create a new vessel name only from popup
                var window1 = ppcDefault.GetWindowByName('vessel_name');
                //do we need this if url is defined in window item?
                //ppcDefault.SetWindowContentUrl(window1, '');
                //ppcDefault.SetWindowContentUrl(window1, '../Popupcontrol/vessel_name.aspx');
                ppcDefault.ShowWindow(window1);

            }
            else {
                //open full voyage form in new tab
                var link1 = "../shipment_voyage/voyage.aspx?mode=Insert";
                //var link = "../Popupcontrol/vessel_name.aspx";
                window.open(link1, "_blank");
            }
        }
        
        function onVesselSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var ets = s.GetSelectedItem().GetColumnText('ETS');
            var eta = s.GetSelectedItem().GetColumnText('ETA');

            dtETS.SetText(ets);
            dtETA.SetText(eta);
        }

        function onJobClosedValueChanged(s, e) {
            var checked = s.GetChecked();
            imgJobClosed.SetVisible(checked);
            lblJobClosed.SetVisible(checked);
            if (checked) { dtJobClosureDate.SetDate(new Date()); } else {dtJobClosureDate.SetDate(null); }
        }
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }
        
        function EditorValueChanged(s, e) {
            StartEdit();
        }

        function OnMenuItemClick(s, e) {
            switch (e.item.name) {
                case "miUpdate": grid.UpdateEdit(); break;
                case "miEdit":
                    StartEdit();
                    break;
                case "miNew":
                    StartNew(); break;
                    //grid.AddNewRow(); break;
                case "miDelete":
                    if (confirm('Are you sure to delete this record?'))
                        grid.DeleteRow(grid.GetTopVisibleIndex());
                    break;
                case "miRefresh":
                case "miCancel": grid.Refresh(); break;
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
                    Text="| Vessel &amp; shipment size">
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
        
        <!-- edit/insert order vessel and shipment size -->
        <div class="grid_16 pad bottom">
            <asp:FormView ID="fmvShipment" runat="server" width="100%" 
                DataKeyNames="OrderID" ondatabound="fmvShipment_DataBound">
                <EditItemTemplate>
                <!-- single row table for vessel -->
                <div>
                    <table id="tblEditVessel" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                        <colgroup>
                            <col class="caption16" />
                            <col /> 
                        </colgroup> 
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="dxlblEditCaption1" runat="server" 
                                    ClientInstanceName="lblEdit1" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Vessel name">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="30" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    OnItemRequestedByValue="dxcboVesselID_ItemRequestedByValue" 
                                    OnItemsRequestedByFilterCondition="dxcboVesselID_ItemsRequestedByFilterCondition" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Joined" Value="<%# Bind('VesselID') %>" ValueField="VoyageID" 
                                    ValueType="System.Int32" Width="400px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Vessel name" FieldName="Joined" Name="colVessel" 
                                            ToolTip="Vessel name" Width="200px" />
                                        <dx:ListBoxColumn Caption="ETS" FieldName="ETS" Name="colETS" ToolTip="ETS" 
                                            Width="80px" />
                                        <dx:ListBoxColumn Caption="ETA" FieldName="ETA" Name="colETA" ToolTip="ETA" 
                                            Width="80px" />
                                    </Columns>
                                    <Buttons>
                                         <dx:EditButton Position="Right">
                                            <Image Height="12px"  ToolTip="Click to add new vessel" Url="~/Images/icons/metro/plus2.png" 
                                                Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                        <dx:EditButton Position="Left">
                                            <Image Height="12px" ToolTip="Click to add new voyage" Url="~/Images/icons/metro/plus2.png" 
                                                Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                    <ClientSideEvents ButtonClick="onVesselButtonClick" SelectedIndexChanged="function(s, e) {onVesselSelected(s, e);}" />
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                        </tr>
                    </table>
                </div>
                <!-- 2nd table for shipment details -->
                <div>
                 <table id="tblEditShipment" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		            <colgroup>
                        <col class="caption16" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
		           </colgroup>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption2" runat="server" ClientInstanceName="lblEdit2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Estimated">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption3" runat="server" ClientInstanceName="lblEdit3" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Actual">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td> 
                            <dx:ASPxLabel ID="dxlblEditCaption5" runat="server" ClientInstanceName="lblEdit5" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETS">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtETS" runat="server" ClientInstanceName="dtETS" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('ETS') %>" TabIndex="1">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption6" runat="server" 
                                ClientInstanceName="lblEdit3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Pallets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstPallets" runat="server" 
                                ClientInstanceName="txtEstPallets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstPallets') %>" TabIndex="11">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption7" runat="server" 
                                ClientInstanceName="lblEdit7" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Packages">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtNumberOfPackages" runat="server" 
                                ClientInstanceName="txtNumberOfPackages" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('NumberOfPackages') %>" TabIndex="15">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption4" runat="server" ClientInstanceName="lblEdit4" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Job closed">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckJobClosed" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('JobClosed') %>" TabIndex="28">
                                 <ClientSideEvents ValueChanged="onJobClosedValueChanged" />
                            </dx:ASPxCheckBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption9" runat="server" ClientInstanceName="lblEdit9" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETA">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtETA" runat="server" ClientInstanceName="dtETA" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('ETA') %>" TabIndex="2">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption10" runat="server" 
                                ClientInstanceName="lblEdit10" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Cartons">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstCartons" runat="server" 
                                ClientInstanceName="txtEstCartons" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstCartons') %>" TabIndex="12">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption11" runat="server" 
                                ClientInstanceName="lblEdit11" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Type">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboPackageTypeID" runat="server" 
                                ClientInstanceName="cboPackageTypeID" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                ValueType="System.Int32" Width="135px" 
                                Value="<%# Bind('PackageTypeID') %>" TextField="PackageType" 
                                ValueField="PackageTypeID" TabIndex="16">
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                            </dx:ASPxComboBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption8" runat="server" ClientInstanceName="lblEdit8" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Closed on">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtJobClosureDate" runat="server" 
                                ClientInstanceName="dtJobClosureDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('JobClosureDate') %>" 
                                TabIndex="29">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption13" runat="server" 
                                ClientInstanceName="lblEdit13" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Warehouse due date">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtWarehouseDate" runat="server" 
                                ClientInstanceName="dtWarehouseDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('WarehouseDate') %>" 
                                TabIndex="3">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption14" runat="server" 
                                ClientInstanceName="lblEdit14" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstWeight" runat="server" 
                                ClientInstanceName="txtEstWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstWeight') %>" TabIndex="13">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption15" runat="server" 
                                ClientInstanceName="lblEdit15" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActualWeight" runat="server" 
                                ClientInstanceName="txtActualWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualWeight') %>" TabIndex="17">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption12" runat="server" ClientInstanceName="lblEdit12" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="House B/L">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHouseBLNUmber" runat="server" 
                                ClientInstanceName="txtHouseBLNUmber" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HouseBLNUmber') %>" ClientEnabled="False" 
                                TabIndex="30">
                            </dx:ASPxTextBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption16" runat="server" 
                                ClientInstanceName="lblEdit16" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Confirmed on board">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckOnBoard" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckShippedOnBoard" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('ShippedOnBoard') %>" TabIndex="4">
                            </dx:ASPxCheckBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption17" runat="server" 
                                ClientInstanceName="lblEdit17" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstVolume" runat="server" 
                                ClientInstanceName="txtEstVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstVolume') %>" TabIndex="14">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption18" runat="server" 
                                ClientInstanceName="lblEdit18" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActualVolume" runat="server" 
                                ClientInstanceName="txtActualVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualVolume') %>" TabIndex="18">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption37" runat="server" ClientInstanceName="lblEdit37" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Container">
                            </dx:ASPxLabel>
                         </td>
                        <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblContainerListEdit" runat="server" ClientInstanceName="lblContainerListEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="">
                            </dx:ASPxLabel>
        
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption36" runat="server" BackColor="#8BA0BC" 
                                ClientInstanceName="lblEdit36" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                Text="Moves job onto progress report">
                            </dx:ASPxLabel>
                         </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption20" runat="server" 
                                ClientInstanceName="lblEdit20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Jackets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtJackets" runat="server" 
                                ClientInstanceName="txtJackets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Jackets') %>" TabIndex="19">
                            </dx:ASPxTextBox>
                         </td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption38" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblEdit38" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="The shipment is costed on the following basis">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption39" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblEdit39" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="Please check if any significant difference actual  to estimate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption25" runat="server" 
                                ClientInstanceName="lblEdit25" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Estimated">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption26" runat="server" 
                                ClientInstanceName="lblEdit26" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Actual">
                            </dx:ASPxLabel>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption19" runat="server" 
                                 ClientInstanceName="lblEdit19" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Consolidation Invoice Ref">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtConsolNumber" runat="server" 
                                 ClientInstanceName="txtConsolNumber" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('ConsolNumber') %>" TabIndex="5">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption28" runat="server" 
                                 ClientInstanceName="lblEdit28" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtEstLCLWt" runat="server" 
                                 ClientInstanceName="txtEstLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('EstLCLWt') %>" TabIndex="20">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption29" runat="server" 
                                 ClientInstanceName="lblEdit29" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtLCLWt" runat="server" ClientInstanceName="txtLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('LCLWt') %>" TabIndex="21">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption23" runat="server" 
                                 ClientInstanceName="lblEdit23" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL or FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboFCLLCL" runat="server" ClientInstanceName="cboFCLLCL" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 ValueType="System.Int32" Width="135px" Value="<%# Bind('FCLLCL') %>" 
                                 TextField="name" ValueField="value" TabIndex="31">
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                             </dx:ASPxComboBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption21" runat="server" 
                                ClientInstanceName="lblEdit21" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Price per copy">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHodderPricePerCopy" runat="server" 
                                ClientInstanceName="txtHodderPricePerCopy" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HodderPricePerCopy') %>" TabIndex="6">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption30" runat="server" 
                                ClientInstanceName="lblEdit30" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstLCLVol" runat="server" 
                                ClientInstanceName="txtEstLCLVol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstLCLVol') %>" TabIndex="22">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption31" runat="server" 
                                ClientInstanceName="dxlblEditCaption31" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtLCLvol" runat="server" ClientInstanceName="txtLCLvol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('LCLVol') %>" TabIndex="23">
                            </dx:ASPxTextBox>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption22" runat="server" 
                                ClientInstanceName="lblEdit22" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="HarperCollins Composite Ref">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHCCompositeRef" runat="server" 
                                ClientInstanceName="txtHCCompositeRef" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HCCompositeRef') %>" TabIndex="7">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption32" runat="server" 
                                ClientInstanceName="lblEdit32" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEst20" runat="server" ClientInstanceName="txtEst20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Est20') %>" TabIndex="24">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblEditCaption33" runat="server" 
                                ClientInstanceName="lblEdit33" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActual20" runat="server" 
                                ClientInstanceName="txtActual20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('No20') %>" TabIndex="25">
                            </dx:ASPxTextBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption24" runat="server" 
                                 ClientInstanceName="lblEdit24" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="HarperCollins Invoice Amount">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtHCInvoiceAmount2" runat="server" 
                                 ClientInstanceName="txtHCInvoiceAmount2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('HCInvoiceAmount2') %>" TabIndex="8">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption34" runat="server" 
                                 ClientInstanceName="lblEdit34" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtEst40" runat="server" ClientInstanceName="txtEst40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Est40') %>" TabIndex="26">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption35" runat="server" 
                                 ClientInstanceName="lblEdit35" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtActual40" runat="server" 
                                 ClientInstanceName="txtActual40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('No40') %>" TabIndex="27">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption27" runat="server" 
                                 ClientInstanceName="lblEdit27" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Insurance Value">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtInsuranceValue" runat="server" 
                                 ClientInstanceName="txtInsuranceValue" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('InsuranceValue') %>" TabIndex="9">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             <dx:ASPxLabel ID="dxlblEditCaption40" runat="server" 
                                 ClientInstanceName="lblEdit40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Impression">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtImpression" runat="server" 
                                 ClientInstanceName="txtImpression" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Impression') %>" TabIndex="10">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                </table> 
                </div> 
                </EditItemTemplate>
                <InsertItemTemplate>
                  <table id="tblInsertVessel" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                        <colgroup>
                            <col class="caption16" />
                            <col /> 
                        </colgroup> 
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="dxlblInsertCaption1" runat="server" 
                                    ClientInstanceName="lblInsert1" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Vessel name">
                                </dx:ASPxLabel>
                                
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="30" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    OnItemRequestedByValue="dxcboVesselID_ItemRequestedByValue" 
                                    OnItemsRequestedByFilterCondition="dxcboVesselID_ItemsRequestedByFilterCondition" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Joined" Value="<%# Bind('VesselID') %>" ValueField="VoyageID" 
                                    ValueType="System.Int32" Width="400px" >
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Vessel name" FieldName="Joined" Name="colVessel" 
                                            ToolTip="Vessel name" Width="200px" />
                                        <dx:ListBoxColumn Caption="ETS" FieldName="ETS" Name="colETS" ToolTip="ETS" 
                                            Width="80px" />
                                        <dx:ListBoxColumn Caption="ETA" FieldName="ETA" Name="colETA" ToolTip="ETA" 
                                            Width="80px" />
                                    </Columns>
                                     <Buttons>
                                         <dx:EditButton Position="Right">
                                            <Image Height="12px"  ToolTip="Click to add new voyage" Url="~/Images/icons/metro/plus2.png" 
                                                Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                        <dx:EditButton Position="Left">
                                            <Image Height="12px" ToolTip="Click to add new vessel" Url="~/Images/icons/metro/plus2.png" 
                                                Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                    <ClientSideEvents ButtonClick="onVesselButtonClick" SelectedIndexChanged="function(s, e) {onVesselSelected(s, e);}" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 2nd table for shipment details -->
                <div>
                 <table id="tblInsertShipment" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		            <colgroup>
                        <col class="caption16" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
		           </colgroup>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption2" runat="server" ClientInstanceName="lblInsert2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Estimated">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption3" runat="server" ClientInstanceName="lblInsert3" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Actual">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td> 
                            <dx:ASPxLabel ID="dxlblInsertCaption5" runat="server" ClientInstanceName="lblInsert5" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETS">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtETS" runat="server" ClientInstanceName="dtETS" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('ETS') %>">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption6" runat="server" 
                                ClientInstanceName="lblInsert3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Pallets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstPallets" runat="server" 
                                ClientInstanceName="txtEstPallets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstPallets') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption7" runat="server" 
                                ClientInstanceName="lblInsert7" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Packages">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtNumberOfPackages" runat="server" 
                                ClientInstanceName="txtNumberOfPackages" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('NumberOfPackages') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption4" runat="server" ClientInstanceName="lblInsert4" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Job closed">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckJobClosed" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('JobClosed') %>">
                                 <ClientSideEvents ValueChanged="onJobClosedValueChanged" />
                            </dx:ASPxCheckBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption9" runat="server" ClientInstanceName="lblInsert9" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETA">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtETA" runat="server" ClientInstanceName="dtETA" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('ETA') %>">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption10" runat="server" 
                                ClientInstanceName="lblInsert10" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Cartons">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstCartons" runat="server" 
                                ClientInstanceName="txtEstCartons" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstCartons') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption11" runat="server" 
                                ClientInstanceName="lblInsert11" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Type">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboPackageTypeID" runat="server" 
                                ClientInstanceName="cboPackageTypeID" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                ValueType="System.Int32" Width="135px" 
                                Value="<%# Bind('PackageTypeID') %>" TextField="PackageType" 
                                ValueField="PackageTypeID">
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                            </dx:ASPxComboBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption8" runat="server" ClientInstanceName="lblInsert8" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Closed on">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtJobClosureDate" runat="server" 
                                ClientInstanceName="dtJobClosureDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('JobClosureDate') %>">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption13" runat="server" 
                                ClientInstanceName="lblInsert13" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Warehouse due date">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxDateEdit ID="dxdtWarehouseDate" runat="server" 
                                ClientInstanceName="dtWarehouseDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="125px" Spacing="0" Value="<%# Bind('WarehouseDate') %>">
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                </CalendarProperties>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxDateEdit>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption14" runat="server" 
                                ClientInstanceName="lblInsert14" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstWeight" runat="server" 
                                ClientInstanceName="txtEstWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstWeight') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption15" runat="server" 
                                ClientInstanceName="lblInsert15" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActualWeight" runat="server" 
                                ClientInstanceName="txtActualWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualWeight') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption12" runat="server" ClientInstanceName="lblInsert12" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="House B/L">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHouseBLNUmber" runat="server" 
                                ClientInstanceName="txtHouseBLNUmber" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HouseBLNUmber') %>">
                            </dx:ASPxTextBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption16" runat="server" 
                                ClientInstanceName="lblInsert16" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Confirmed on board">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckOnBoard" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckShippedOnBoard" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('ShippedOnBoard') %>">
                            </dx:ASPxCheckBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption17" runat="server" 
                                ClientInstanceName="lblInsert17" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstVolume" runat="server" 
                                ClientInstanceName="txtEstVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstVolume') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption18" runat="server" 
                                ClientInstanceName="lblInsert18" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActualVolume" runat="server" 
                                ClientInstanceName="txtActualVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualVolume') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption37" runat="server" ClientInstanceName="lblInsert37" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Container">
                            </dx:ASPxLabel>
                         </td>
                        <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblContainerListEdit" runat="server" ClientInstanceName="lblContainerListEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="">
                            </dx:ASPxLabel>
        
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption36" runat="server" BackColor="#8BA0BC" 
                                ClientInstanceName="lblInsert36" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                Text="Moves job onto progress report">
                            </dx:ASPxLabel>
                         </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption20" runat="server" 
                                ClientInstanceName="lblInsert20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Jackets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtJackets" runat="server" 
                                ClientInstanceName="txtJackets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Jackets') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption38" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblInsert38" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="The shipment is costed on the following basis">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption39" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblInsert39" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="Please check if any significant difference actual  to estimate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption25" runat="server" 
                                ClientInstanceName="lblInsert25" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Estimated">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption26" runat="server" 
                                ClientInstanceName="lblInsert26" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Actual">
                            </dx:ASPxLabel>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption19" runat="server" 
                                 ClientInstanceName="lblInsert19" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Consolidation Invoice Ref">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtConsolNumber" runat="server" 
                                 ClientInstanceName="txtConsolNumber" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('ConsolNumber') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption28" runat="server" 
                                 ClientInstanceName="lblInsert28" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtEstLCLWt" runat="server" 
                                 ClientInstanceName="txtEstLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('EstLCLWt') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption29" runat="server" 
                                 ClientInstanceName="lblInsert29" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtLCLWt" runat="server" ClientInstanceName="txtLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('LCLWt') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption23" runat="server" 
                                 ClientInstanceName="lblInsert23" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL or FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboFCLLCL" runat="server" ClientInstanceName="cboFCLLCL" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 ValueType="System.Int32" Width="135px" Value="<%# Bind('FCLLCL') %>" 
                                 TextField="name" ValueField="value">
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                             </dx:ASPxComboBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption21" runat="server" 
                                ClientInstanceName="lblInsert21" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Price per copy">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHodderPricePerCopy" runat="server" 
                                ClientInstanceName="txtHodderPricePerCopy" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HodderPricePerCopy') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption30" runat="server" 
                                ClientInstanceName="lblInsert30" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEstLCLVol" runat="server" 
                                ClientInstanceName="txtEstLCLVol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstLCLVol') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption31" runat="server" 
                                ClientInstanceName="dxlblInsertCaption31" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtLCLvol" runat="server" ClientInstanceName="txtLCLvol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('LCLVol') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption22" runat="server" 
                                ClientInstanceName="lblInsert22" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="HarperCollins Composite Ref">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtHCCompositeRef" runat="server" 
                                ClientInstanceName="txtHCCompositeRef" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HCCompositeRef') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption32" runat="server" 
                                ClientInstanceName="lblInsert32" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtEst20" runat="server" ClientInstanceName="txtEst20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Est20') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblInsertCaption33" runat="server" 
                                ClientInstanceName="lblInsert33" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxTextBox ID="dxtxtActual20" runat="server" 
                                ClientInstanceName="txtActual20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('No20') %>">
                            </dx:ASPxTextBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption24" runat="server" 
                                 ClientInstanceName="lblInsert24" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="HarperCollins Invoice Amount">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtHCInvoiceAmount2" runat="server" 
                                 ClientInstanceName="txtHCInvoiceAmount2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('HCInvoiceAmount2') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption34" runat="server" 
                                 ClientInstanceName="lblInsert34" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtEst40" runat="server" ClientInstanceName="txtEst40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Est40') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption35" runat="server" 
                                 ClientInstanceName="lblInsert35" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtActual40" runat="server" 
                                 ClientInstanceName="txtActual40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('No40') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption27" runat="server" 
                                 ClientInstanceName="lblInsert27" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Insurance Value">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtInsuranceValue" runat="server" 
                                 ClientInstanceName="txtInsuranceValue" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('InsuranceValue') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             <dx:ASPxLabel ID="dxlblInsertCaption40" runat="server" 
                                 ClientInstanceName="lblInsert40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Impression">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtImpression" runat="server" 
                                 ClientInstanceName="txtImpression" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Impression') %>">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                </table> 
                </div> 
                </InsertItemTemplate> 
                <ItemTemplate>
                    <!-- single row table for vessel -->
                <div>
                    <table id="tblViewVessel" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                        <colgroup>
                            <col class="caption16" />
                            <col /> 
                        </colgroup> 
                        <tr>
                            <td style="width: 208px">
                                <dx:ASPxLabel ID="dxlblViewCaption1" runat="server" 
                                    ClientInstanceName="lblView1" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Vessel name">
                                </dx:ASPxLabel>
                                
                            </td>
                            <td>
					  <dx:ASPxLabel ID="dxlblFieldVessel" runat="server" 
                                    ClientInstanceName="lblFieldVessel" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                              
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 2nd table for shipment details -->
                <div>
                 <table id="tblViewShipment" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		            <colgroup>
                        <col class="caption16" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
                        <col class="caption6" />
                        <col/>
		           </colgroup>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption2" runat="server" ClientInstanceName="lblView2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Estimated">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption3" runat="server" ClientInstanceName="lblView3" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Actual">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td> 
                            <dx:ASPxLabel ID="dxlblViewCaption5" runat="server" ClientInstanceName="lblView5" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETS">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel  ID="dxlblFieldETS" runat="server" ClientInstanceName="lblFieldETS" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                Width="125px" Value="<%# Bind('ETS') %>">
                            </dx:ASPxLabel >
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption6" runat="server" 
                                ClientInstanceName="lblView3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Pallets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEstPallets" runat="server" 
                                ClientInstanceName="lblFieldEstPallets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstPallets') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption7" runat="server" 
                                ClientInstanceName="lblView7" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Packages">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldNumberOfPackages" runat="server" 
                                ClientInstanceName="lblFieldNumberOfPackages" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('NumberOfPackages') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption4" runat="server" ClientInstanceName="lblView4" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Job closed">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckFieldJobClosed" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckFieldJobClosed" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('JobClosed') %>" ClientEnabled="False">
                            </dx:ASPxCheckBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption9" runat="server" ClientInstanceName="lblView9" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="ETA">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel  ID="dxlblFieldETA" runat="server" ClientInstanceName="lblFieldETA" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                Width="125px" Value="<%# Bind('ETA') %>">
                            </dx:ASPxLabel >
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption10" runat="server" 
                                ClientInstanceName="lblView10" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Cartons">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEstCartons" runat="server" 
                                ClientInstanceName="lblFieldEstCartons" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstCartons') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption11" runat="server" 
                                ClientInstanceName="lblView11" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Type">
                            </dx:ASPxLabel>
                         </td>
                        <td> 
                            <dx:ASPxLabel ID="dxlblFieldPackageTypeID" runat="server" 
                                ClientInstanceName="lblFieldPackageTypeID" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                Value="<%# Bind('PackageTypeID') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption8" runat="server" ClientInstanceName="lblView8" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Closed on">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel  ID="dxlblFieldJobClosureDate" runat="server" 
                                ClientInstanceName="lblFieldJobClosureDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                Width="125px" Value="<%# Bind('JobClosureDate') %>">
                            </dx:ASPxLabel >
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption13" runat="server" 
                                ClientInstanceName="lblView13" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Warehouse due date">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel  ID="dxlblFieldWarehouseDate" runat="server" 
                                ClientInstanceName="lblFieldWarehouseDate" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                Width="125px" Value="<%# Bind('WarehouseDate') %>">
                            </dx:ASPxLabel >
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption14" runat="server" 
                                ClientInstanceName="lblView14" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEstWeight" runat="server" 
                                ClientInstanceName="lblFieldEstWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstWeight') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption15" runat="server" 
                                ClientInstanceName="lblView15" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Weight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldActualWeight" runat="server" 
                                ClientInstanceName="lblFieldActualWeight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualWeight') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption12" runat="server" ClientInstanceName="lblView12" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="House B/L">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldHouseBLNUmber" runat="server" 
                                ClientInstanceName="lblFieldHouseBLNUmber" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HouseBLNUmber') %>">
                            </dx:ASPxLabel>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption16" runat="server" 
                                ClientInstanceName="lblView16" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Confirmed on board">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxCheckBox ID="dxckFieldOnBoard" runat="server" CheckState="Unchecked" 
                                ClientInstanceName="ckFieldOnBoard" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value="<%# Bind('ShippedOnBoard') %>" ClientEnabled="False">
                            </dx:ASPxCheckBox>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption17" runat="server" 
                                ClientInstanceName="lblView17" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEstVolume" runat="server" 
                                ClientInstanceName="lblFieldEstVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstVolume') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption18" runat="server" 
                                ClientInstanceName="lblView18" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldActualVolume" runat="server" 
                                ClientInstanceName="lblFieldActualVolume" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('ActualVolume') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption37" runat="server" ClientInstanceName="lblView37" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Container">
                            </dx:ASPxLabel>
                         </td>
                        <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblContainerListView" runat="server" ClientInstanceName="lblContainerListView" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="">
                            </dx:ASPxLabel>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption36" runat="server" BackColor="#8BA0BC" 
                                ClientInstanceName="lblView36" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                Text="Moves job onto progress report">
                            </dx:ASPxLabel>
                         </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption20" runat="server" 
                                ClientInstanceName="lblView20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Jackets">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldJackets" runat="server" 
                                ClientInstanceName="lblFieldJackets" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Jackets') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption38" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblView38" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="The shipment is costed on the following basis">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption39" runat="server" BackColor="#8BA0BC" 
                                 ClientInstanceName="lblView39" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Bold="False" ForeColor="White" 
                                 Text="Please check if any significant difference actual  to estimate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption25" runat="server" 
                                ClientInstanceName="lblView25" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Estimated">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption26" runat="server" 
                                ClientInstanceName="lblView26" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Actual">
                            </dx:ASPxLabel>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption19" runat="server" 
                                 ClientInstanceName="lblView19" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Consolidation Invoice Ref">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldConsolNumber" runat="server" 
                                 ClientInstanceName="lblFieldConsolNumber" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('ConsolNumber') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption28" runat="server" 
                                 ClientInstanceName="lblView28" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldEstLCLWt" runat="server" 
                                 ClientInstanceName="lblFieldEstLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('EstLCLWt') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption29" runat="server" 
                                 ClientInstanceName="lblView29" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL weight">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldLCLWt" runat="server" ClientInstanceName="lblFieldLCLWt" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('LCLWt') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption23" runat="server" 
                                 ClientInstanceName="lblView23" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="LCL or FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
					<dx:ASPxLabel ID="dxlblFieldFCLLCL" runat="server" 
                                 ClientInstanceName="lblFieldFCLLCL" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Value="<%# Bind('FCLLCL') %>">
                             </dx:ASPxLabel>
                         </td>
                     </tr>
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption21" runat="server" 
                                ClientInstanceName="lblView21" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Price per copy">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldHodderPricePerCopy" runat="server" 
                                ClientInstanceName="lblFieldHodderPricePerCopy" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HodderPricePerCopy') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption30" runat="server" 
                                ClientInstanceName="lblView30" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEstLCLVol" runat="server" 
                                ClientInstanceName="lblFieldEstLCLVol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('EstLCLVol') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption31" runat="server" 
                                ClientInstanceName="dxlblViewCaption31" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="LCL volume">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldLCLvol" runat="server" ClientInstanceName="lblFieldLCLvol" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('LCLVol') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption22" runat="server" 
                                ClientInstanceName="lblView22" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="HarperCollins Composite Ref">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldHCCompositeRef" runat="server" 
                                ClientInstanceName="lblFieldHCCompositeRef" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('HCCompositeRef') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption32" runat="server" 
                                ClientInstanceName="lblView32" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldEst20" runat="server" ClientInstanceName="lblFieldEst20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('Est20') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblViewCaption33" runat="server" 
                                ClientInstanceName="lblView33" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="20' FCL">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblFieldActual20" runat="server" 
                                ClientInstanceName="lblFieldActual20" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="135px" Value="<%# Bind('No20') %>">
                            </dx:ASPxLabel>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption24" runat="server" 
                                 ClientInstanceName="lblView24" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="HarperCollins Invoice Amount">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldHCInvoiceAmount2" runat="server" 
                                 ClientInstanceName="lblFieldHCInvoiceAmount2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('HCInvoiceAmount2') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption34" runat="server" 
                                 ClientInstanceName="lblView34" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldEst40" runat="server" ClientInstanceName="lblFieldEst40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Est40') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption35" runat="server" 
                                 ClientInstanceName="lblView35" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="40' FCL">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldActual40" runat="server" 
                                 ClientInstanceName="lblFieldActual40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('No40') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption27" runat="server" 
                                 ClientInstanceName="lblView27" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Insurance Value">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldInsuranceValue" runat="server" 
                                 ClientInstanceName="lblFieldInsuranceValue" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('InsuranceValue') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             <dx:ASPxLabel ID="dxlblViewCaption40" runat="server" 
                                 ClientInstanceName="lblView40" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Impression">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFieldImpression" runat="server" 
                                 ClientInstanceName="lblFieldImpression" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="135px" Value="<%# Bind('Impression') %>">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                </table> 
                </div> 
                </ItemTemplate>  
            </asp:FormView>
             <!-- custom command butons for formview -->
                <div>
                <dx:ASPxMenu ID="dxmnuCommand" runat="server" 
                ClientInstanceName="mnuCommand" width="100%" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                onitemclick="dxmnuCommand_ItemClick" AutoSeparators="RootOnly" 
                ItemAutoWidth="False" onitemdatabound="dxmnuCommand_ItemDataBound">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
                </div>
         </div> 
        <div class="clear"></div>
        <div class="grid_16">
                <dx1:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx1:ASPxHiddenField>
    
        </div>
        <div class="clear"></div>
        <div class="grid_16">
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
                ContentUrl="~/Popupcontrol/Pod_NameAndAddress.aspx" HeaderText="Company details"
                Height="820px" Modal="True" Name="CompanyDetails" PopupAction="None" 
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
<dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
</ContentCollection>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Windows>
            <dx:PopupWindow ContentUrl="../Popupcontrol/vessel_name.aspx" 
                HeaderText="Add a new vessel" Height="450px" MinHeight="450px" MinWidth="600px" 
                Modal="True" Width="600px" Name="vessel_name">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
        </Windows>
    </dx:ASPxPopupControl>
    <!-- end popup -->     
   </div> 
</asp:Content>

