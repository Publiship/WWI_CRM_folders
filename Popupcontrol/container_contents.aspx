<%@ Page Language="C#" AutoEventWireup="true" CodeFile="container_contents.aspx.cs" Inherits="container_contents" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxObjectContainer" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPager" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>
    
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Container details</title>
    <link rel="stylesheet" href="../App_Themes/Custom.css" type="text/css" />
    
    <script type="text/javascript">
    // <![CDATA[
    function onGridInit(s, e) {
        grdContainer.PerformCallback('getdata');
    }
    // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div>
                <dx:ASPxLabel ID="dxlblTitle" ClientInstanceName="lblTitle" runat="server" 
                   Text="" Font-Size="X-Large" ForeColor="#666666" 
                   Wrap="True">
                        </dx:ASPxLabel>
            </div>
            <!-- error message -->
            <div class="pad_bottom"> 
             <dx:ASPxLabel ID="dxlblerr1" ClientInstanceName="lblerr1" runat="server" 
                   Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" Width="250px" 
                   Wrap="True" ClientVisible="False" Visible="False">
            </dx:ASPxLabel>
            </div> 
            <!-- deliveries grid -->
            <!-- if you want to disalbe the grup panel you must first set AllowSort="false" then set ShowGroupPanel="false" -->
            <div class="pad_bottom">
             <dx:ASPxGridView ID="gridContainer" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" width="100%" 
                    ClientInstanceName="grdContainer" DataSourceID="LinqServerModeContainer" 
                     oncustomcallback="gridContainer_CustomCallback" 
                    KeyFieldName="OrderIx" >
                    <SettingsBehavior AutoExpandAllGroups="false" ColumnResizeMode="Control" 
                        EnableRowHotTrack="True" AllowFocusedRow="True" AllowGroup="False" AllowSort="true" />
                    <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                        <LoadingPanel ImageSpacing="5px">
                        </LoadingPanel>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="50" Position="TopAndBottom" AlwaysShowPager="True">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
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
                    <SettingsText Title="Search results" EmptyDataRow="No records found" />
                    <ClientSideEvents Init="onGridInit" />
                         
                    <SettingsCookies CookiesID="dlvpop" StoreFiltering="False" 
                        StoreGroupingAndSorting="False" />
                    <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" PopupEditFormWidth="600px" />
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="SubDeliveryID" VisibleIndex="1" 
                            Caption="Publiship ID" Width="100px" Name="col_subdeliveryid" ExportWidth="80" 
                             ReadOnly="True">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="2" 
                            Caption="Title" Width="250px" Name="col_title" ExportWidth="80" 
                             ReadOnly="True">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="ISBN" VisibleIndex="3" Caption="ISBN" 
                             Width="100px" ReadOnly="True" Name="col_isbn" ExportWidth="70" >
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
                                 FieldName="CartonsPerFullPallet" Name="col_cfp" ReadOnly="True" VisibleIndex="6" 
                                 Width="120px">
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Part pallets" ExportWidth="100" 
                                 FieldName="PartPallets" Name="col_partpallets" ReadOnly="True" VisibleIndex="7" 
                                 Width="110px">
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cartons/Part pallet" ExportWidth="100" 
                                 FieldName="CartonsPerPartPallet" Name="col_cpp" ReadOnly="True" VisibleIndex="8" 
                                 Width="120px">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Total weight" ExportWidth="100" 
                                 FieldName="TotalConsignmentWeight" Name="col_totalweight" 
                             ReadOnly="True" VisibleIndex="9" 
                                 Width="95px">
                                   <PropertiesTextEdit DisplayFormatString="F">
                                </PropertiesTextEdit>
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Total cube" ExportWidth="100" 
                                 FieldName="TotalConsignmentCube" Name="col_totalcube" ReadOnly="True" VisibleIndex="10" 
                                 Width="95px">
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
                            <dx:GridViewDataTextColumn FieldName="BookingReceived" VisibleIndex="12" 
                            Caption="Booking Received" ReadOnly="True" Width="110px" Name="col_bookingreceived" 
                                 ExportWidth="75">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Equals" />
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataDateColumn Caption="Cargo Ready" ExportWidth="75" 
                             FieldName="CargoReady" Name="col_cargoready" ReadOnly="True" VisibleIndex="13" 
                             Width="110px">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                             </PropertiesDateEdit>
                             <Settings AutoFilterCondition="Equals" ShowFilterRowMenu="True" />
                         </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="vessel_name" VisibleIndex="14" 
                            Caption="Vessel Name" ReadOnly="True" Width="120px" Name="col_vesselname" 
                                 ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="ETS" ExportWidth="75" 
                               FieldName="ETS" Name="col_ets" ReadOnly="True" 
                               VisibleIndex="15" Width="90px">
                               <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                           <dx:GridViewDataDateColumn Caption="ETA" FieldName="ETA" 
                               Name="col_eta" ReadOnly="True" VisibleIndex="16" Width="90px" 
                               ExportWidth="75">
                               <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="current_status" VisibleIndex="17" 
                            Caption="Status" ReadOnly="True" Width="80px" 
                             Name="col_status" ExportWidth="80">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="status_on" VisibleIndex="18" 
                            Caption="On" ReadOnly="True" Width="90px" Name="col_currentstatusdate" 
                                 ExportWidth="90">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="StatusDate" VisibleIndex="19" Caption="Last updated" 
                            ReadOnly="True" Width="95px" Name="col_last_updated" ExportWidth="90">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                             <dx:GridViewDataTextColumn Caption="Customer" FieldName="client_name" 
                                 Name="col_company" ReadOnly="True" 
                                 VisibleIndex="20" Width="150px" ExportWidth="75" >
                                 <Settings ShowFilterRowMenu="True" ShowInFilterControl="True" 
                                     AutoFilterCondition="Contains" />
                             </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="Order Number" FieldName="OrderNumber" 
                             Name="col_ordernumber" ReadOnly="True" VisibleIndex="15" Width="100px">
                         </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn FieldName="ContainerNumber" VisibleIndex="21" 
                            Caption="Container" ReadOnly="True" Width="130px" Name="col_container" 
                                 ExportWidth="100">
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
                             Name="col_orderix" ReadOnly="True" VisibleIndex="23" Visible="False"  
                             Width="0px" ShowInCustomizationForm="False">
                             <Settings ShowFilterRowMenu="False" AllowAutoFilter="False" 
                                 AllowAutoFilterTextInputTimer="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                    </Columns> 
                    <StylesPager>
                        <PageNumber ForeColor="#3E4846">
                        </PageNumber>
                        <Summary ForeColor="#1E395B">
                        </Summary>
                    </StylesPager>
                   
             <Settings ShowHorizontalScrollBar="True" VerticalScrollableHeight="450" 
                        VerticalScrollBarStyle="Standard" ShowVerticalScrollBar="True" GridLines="Horizontal" 
                        ShowHeaderFilterBlankItems="False" ShowGroupButtons="False" />
             <SettingsCustomizationWindow PopupVerticalAlign="Above"  />
                    <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFilterBar="Hidden"  />
                    <StylesEditors ButtonEditCellSpacing="0">
                        <ProgressBar Height="21px">
                        </ProgressBar>
                    </StylesEditors>
                    <SettingsDetail ShowDetailButtons="False" />
                </dx:ASPxGridView>
            
            </div>
        <div>
              <dx:LinqServerModeDataSource ID="LinqServerModeContainer" runat="server" 
        ContextTypeName="linq.linq_container_contentsDataContext" 
        TableName="view_container_contents" />
        </div> 

    </form>
</body>
</html>
