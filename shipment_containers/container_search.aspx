<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="container_search.aspx.cs" Inherits="container_search" %>

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

        function onCustomButtonclick(s, e) {
            s.GetRowValues(s.GetFocusedRowIndex(), 'OrderNumber', ongridCommand);
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
                <dx:ASPxLabel ID="dxlblContainerDetails" runat="server" 
                             ClientInstanceName="lblContainerDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Search containers">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblSearch" runat="server" ClientInstanceName="lblSearch" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
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
                ItemAutoWidth="False" width="100%" 
                onitemdatabound="dxmnuSearchTools_DataBound" ShowAsToolbar="True">
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
            <dx:ASPxGridView ID="dxgridContainer" ClientInstanceName="gridContainer" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" width="100%" 
                oncustombuttoncallback="dxgridContainer_CustomButtonCallback" 
                DataSourceID="linqContainer" KeyFieldName="ContainerID" 
                onafterperformcallback="dxgridContainer_AfterPerformCallback">
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
                <SettingsPager PageSize="25" Position="TopAndBottom">
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
                        Width="125px" ButtonType="Image">
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
                            <dx:GridViewCommandColumnCustomButton ID="cmdMarkLOB" Text="Loaded on board" 
                                Visibility="AllDataRows">
                                <Image AlternateText="Mark loaded" ToolTip="Mark loaded on board" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/fork_truck18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="cmdMove" Text="Move" 
                                Visibility="AllDataRows">
                                <Image AlternateText="Move" ToolTip="Move container" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/cargo_ship18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ContainerID" Name="colContainerID" 
                        ReadOnly="True" Visible="False" VisibleIndex="0" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Container number" 
                        FieldName="ContainerNumber" Name="colContainerNumber" VisibleIndex="1" 
                        Width="150px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Container status" FieldName="ContainerStatus" 
                        Name="colContainerStatus" VisibleIndex="3" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Container type" FieldName="ContainerType" 
                        Name="colContainerType" VisibleIndex="4" Width="125px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Vessel name" FieldName="VesselName" 
                        Name="colVesselName" VisibleIndex="5" Width="250px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Voyage number" FieldName="VoyageNumber" 
                        Name="colVoyageNumber" VisibleIndex="2" Width="100px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="Date added" FieldName="DateAdded" 
                        Name="colDateAdded" VisibleIndex="6">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataCheckColumn Caption="Loaded on board" FieldName="LoadedOnBoard" 
                        Name="colLoadedOnBoard" VisibleIndex="7" Width="125px">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn Caption="Delivered" FieldName="Delivered" 
                        Name="colDelivered" VisibleIndex="8" Width="100px">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn Caption="Devanned" FieldName="Devanned" 
                        Name="colDevanned" VisibleIndex="9">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataDateColumn Caption="Delivery date" FieldName="DeliveryDate" 
                        Name="colDeliveryDate" VisibleIndex="10" Width="100px">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Caption="Devan warehouse" FieldName="DevanWarehouse" 
                        Name="colDevanWarehouse" VisibleIndex="11" Width="125px">
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
                    ShowHorizontalScrollBar="True" ShowVerticalScrollBar="True" 
                    VerticalScrollableHeight="300" ShowTitlePanel="True" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </div> 
        <div class="clear"></div> 
    </div><!-- end container -->
    
    <dx1:ASPxHiddenField ID="dxhfContainer" ClientInstanceName="hfContainer" runat="server">
    </dx1:ASPxHiddenField>
    

    <dx:LinqServerModeDataSource ID="linqContainer" runat="server" 
        ContextTypeName="linq.linq_containersDataContext" 
        TableName="view_containers" />
    

</asp:Content>