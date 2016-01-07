<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_template_search.aspx.cs" Inherits="order_template_search" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

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

<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onDateRangeChanged() {
            if (!gridOrders.InCallback()) {
                gridOrders.PerformCallback(' ');
            }
        }
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }
        
       function OnMenuItemClick(s, e) {
            switch (e.item.name) {
                case "cmdOrdersByUser":
                    if (!gridOrders.InCallback()) {
                        hfOrder.Set("myorders", -1)
                        gridOrders.PerformCallback(' ');
                        lblSearch.SetText('My orders');
                    }
                    break;
                case "cmdOrdersAll":
                    if (!gridOrders.InCallback()) {
                        hfOrder.Remove("myorders");
                        gridOrders.PerformCallback(' ');
                        lblSearch.SetText('All orders');
                    }
                    break;
                //case "cmdNew": //just use url on menu item
                //    gridOrders.PerformCallback('new_order');
                //    break;
                case "miDelete":
                    //if (confirm('Are you sure to delete this record?'))
                    //   grid.DeleteRow(grid.GetTopVisibleIndex());
                    //break;
                case "miRefresh":
                case "miCancel": grid.Refresh(); break;
            }
        }

        //deprecated code do this in code-behind
        function onCustomButtonclick(s, e) {
            s.GetRowValues(s.GetFocusedRowIndex(), 'OrderNumber', ongridCommand);
        }

        function ongridCommand(result) {
            window.open('Order_Output.aspx?pno=' + result.toString());
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
       
        <!-- Using tables for layout. Semantically not a good choice but other options e.g. definition lists
             do not render properly in older versions of internet explorer < IE7. Also multi-column combos do not
             render correctly when placed in definition lists in < IE7  -->
        <div class="grid_10 pad_bottom">
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Search order templates">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <!-- images and text -->
        <div class="grid_6">
         </div>              
        <div class="clear"></div>
        
        <!-- search options -->
        <div class="grid_16">
        
            <dx:ASPxMenu ID="dxmnuToolbar" ClientInstanceName="mnuToolbar" runat="server" AutoSeparators="RootOnly" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                ItemAutoWidth="False" width="100%">
                <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanelImage>
                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                <ClientSideEvents ItemClick="function(s, e) { OnMenuItemClick(s, e); }" />
                <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
            </dx:ASPxMenu>
        
        </div>
        <div class="clear"></div>
        <!-- individual form menus -->
        <div class="grid_16 pad_bottom">
        <!-- search grid -->
            <dx:ASPxGridView ID="dxgridOrders" ClientInstanceName="gridOrders" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" width="100%" 
                oncustombuttoncallback="dxgridOrders_CustomButtonCallback" 
                oncustomcallback="dxgridOrders_CustomCallback" 
                onafterperformcallback="dxgridOrders_AfterPerformCallback" 
                DataSourceID="linqSearch">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior AllowGroup="False" ColumnResizeMode="Control" 
                    EnableRowHotTrack="True" />
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
                    <dx:GridViewCommandColumn Caption="Options" Name="colCustom" VisibleIndex="0" 
                        Width="100px" ButtonType="Image">
                        <ClearFilterButton Visible="true">
                        </ClearFilterButton>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="cmdView" Text="View" 
                                Visibility="AllDataRows">
                                  <Image AlternateText="View" ToolTip="View" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/details18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="cmdEdit" Text="Edit" 
                                Visibility="AllDataRows">
                                  <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="cmdCreateOrder" Text="Create Order" 
                                Visibility="AllDataRows">
                                <Image AlternateText="Create order" ToolTip="Create order" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/copy18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="OrderTemplateID" VisibleIndex="0" 
                        Caption="hidden id" Name="colOrderTemplateID" ShowInCustomizationForm="False" 
                        Visible="False" Width="0px">
                        <Settings AllowAutoFilter="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TemplateName" VisibleIndex="1" 
                        Caption="Template name" Name="colTemplateName" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OfficeIndicator" VisibleIndex="2" 
                        Caption="Office indicator" Name="colOfficeIndicator" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="PublishipOrder" VisibleIndex="3" 
                        Caption="Publiship" Name="colPublishipOrder" Width="90px">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="CustomersRef" VisibleIndex="4" 
                        Caption="Customer ref" Name="colCustomersRef" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OrderController" VisibleIndex="5" 
                        Caption="Order controller" Name="colOrderController" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OpsController" VisibleIndex="6" 
                        Caption="Ops controller" Name="colOpsController">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="7" 
                        Caption="Client contact" Name="colContactName" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OriginPlace" VisibleIndex="9" 
                        Caption="Origin point" Name="colOriginPlace" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OriginPort" VisibleIndex="10" 
                        Caption="Origin port" Name="colOriginPort" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DestinationPort" VisibleIndex="11" 
                        Caption="Destination port" Name="colDestinationPort" Width="120px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FinalDestination" VisibleIndex="12" 
                        Caption="Final destination" Name="colFinalDestination" Width="120px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientName" VisibleIndex="8" 
                        Caption="Client" Name="colClientName" Width="120px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PrinterName" VisibleIndex="13" 
                        Caption="Printer" Name="colPrinterName" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ConsigneeName" VisibleIndex="14" 
                        Caption="Consignee" Name="colConsigneeName" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OriginAgentName" VisibleIndex="15" 
                        Caption="Agent at origin" Name="colOriginAgentName" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NotifyName" VisibleIndex="16" 
                        Caption="Notify party" Name="colNotifyName" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OriginController" VisibleIndex="17" 
                        Caption="Origin controller" Name="colOriginController" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DestAgentName" VisibleIndex="18" 
                        Caption="Agent at destination" Name="colDestAgentName" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DestController" VisibleIndex="19" 
                        Caption="Destination controller" Name="colDestController" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClearingAgentName" VisibleIndex="20" 
                        Caption="Clearing agent" Name="colClearingAgentName" Width="115px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OnCarriageName" VisibleIndex="21" 
                        Caption="On carriage" Name="colOnCarriageName" Width="115px">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowFilterBar="Visible" ShowFilterRow="True" 
                    ShowFilterRowMenu="True" ShowHeaderFilterButton="True" 
                    ShowHorizontalScrollBar="True" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </div> 
        <div class="clear"></div>
        <!-- options? -->
        <div class="clear"></div>
        
        <div class="clear"></div>
        
      
    </div><!-- end container -->
    
    <dx1:ASPxHiddenField ID="dxhfSearch" ClientInstanceName="hfSearch" 
        runat="server">
    </dx1:ASPxHiddenField>
    

    <dx:LinqServerModeDataSource ID="linqSearch" 
    runat="server" ContextTypeName="linq.linq_view_order_templatesDataContext" 
        TableName="view_order_templates" />
    

</asp:Content>

