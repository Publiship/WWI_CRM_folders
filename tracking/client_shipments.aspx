<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="client_shipments.aspx.cs" Inherits="tracking_client_shipments" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

<script type="text/javascript">
    // <![CDATA[
    function onSearchClicked(s, e) {
        if (dt1.GetDate() != null && dt2.GetDate() != null) {
            if (!gridShipments.InCallback()) {
                gridShipments.PerformCallback('getdata');
            }
        }
        else {
            alert('Please check your start and end dates'); 
        }
    }
        
    function onGridInit(s, e) {
        gridShipments.PerformCallback('getdata');
    }
 
    //new function when file upload button is clicked so we can avoid making a callback.postback
    //call web method so we can open new window
    //aspxridview.settingsbehaviour.allowfocusedrow=true MUST be set or can't get row index
    function grid_CustomButtonClick(s, e) {
             e.processOnServer = false;
             
             var user = verify_user();
             if (user != 'You are not signed in') {
                 if (e.buttonID == 'cmdDeliveries') {
                     //var window = popDefault.GetWindowByName('sysna');
                     //popDefault.ShowWindow(window);
                     //just use order number
                     s.GetRowValues(s.GetFocusedRowIndex(), 'ContainerID;ContainerNumber;DeliveryAddress', onGotValues);
                 }
             }
             else {
                 //var window = popDefault.GetWindowByName('sysna');
                 //popDefault.ShowWindow(window);
                 var window = popDefault.GetWindowByName('msgform');
                 popDefault.ShowWindow(window);
             }
    }

   
    //using webmethod in code behind
    function onGotValues(values) {
        //alert(values[0]);
        //alert(values[1]);
        //can pass values as iList<string> or concatenated using values.toString() method
        PageMethods.get_secure_url(values, 'cmdDeliveries', onMethodComplete);
    }

    function onMethodComplete(result, userContext, methodName) {
        var checked = hfMethod.Get("win");
        
        if (result != "") {
            if (!checked) {
                var window1 = ppContainer.GetWindowByName('ppDeliveries');
                ppContainer.SetWindowContentUrl(window1, '');
                ppContainer.SetWindowContentUrl(window1, result.toString());
                ppContainer.ShowWindow(window1);
            }
            else {
                //opens form in new window
                window.open(result, "_blank");
            }
        }
        else {
            alert('PageMethods.get_secure_url() returned null');
        }
    }
    // ]]>
    </script>

    <div class="container_16">
        <!-- container for search range -->
        <div class="grid_16">
            <div class="container960 center">
                <dx:ASPxRoundPanel ID="dxpnlCritria" ClientInstanceName="pnlCriteria" 
                    runat="server" Width="100%" 
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" EnableDefaultAppearance="False" 
                    GroupBoxCaptionOffsetX="6px" GroupBoxCaptionOffsetY="-19px" 
                    HeaderText="Search weights, cube and pallets shipped by client" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                    Font-Names="Arial,Helvetica,sans-serif;" Font-Size="X-Large">
                     <ContentPaddings PaddingBottom="10px" PaddingLeft="9px" PaddingRight="11px" 
                         PaddingTop="10px" />
                     <HeaderStyle>
                     <Paddings PaddingBottom="6px" PaddingLeft="9px" PaddingRight="11px" 
                         PaddingTop="3px" />
                     </HeaderStyle>
                     <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table id="tblCriteria" cellpadding="3px">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="dxlbldt1" ClientInstanceName="lbldt1" runat="server" 
                                                Text="Enter start ETS" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue">
                                            </dx:ASPxLabel>   
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="dxdt1" ClientInstanceName="dt1" runat="server" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Spacing="0" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                                <CalendarProperties>
                                                    <HeaderStyle Spacing="1px" />
                                                </CalendarProperties>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </dx:ASPxDateEdit>
                                        </td>
                                          <td>
                                            <dx:ASPxLabel ID="dxlbldt2" ClientInstanceName="lbldt2" runat="server" 
                                                  Text="Enter end ETS" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue">
                                            </dx:ASPxLabel>   
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="dxdt2" ClientInstanceName="dt2" runat="server" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Spacing="0" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                                <CalendarProperties>
                                                    <HeaderStyle Spacing="1px" />
                                                </CalendarProperties>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="dxbtnSearch" runat="server" ClientInstanceName="btnSearch" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Search" 
                                                VerticalAlign="Middle" AutoPostBack="False" CausesValidation="False" 
                                                UseSubmitBehavior="False">
                                                <Image AlternateText="Begin search" Height="22px" ToolTip="Begin search" 
                                                    Url="~/Images/icons/metro/22x18/search18.png" Width="22px">
                                                </Image>
                                                <ClientSideEvents Click="onSearchClicked" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody> 
                            </table>
                        </dx:PanelContent>
                     </PanelCollection> 
                </dx:ASPxRoundPanel>
            </div> 
        </div> 
        <div class="clear"></div>
        <!-- container for grid -->
        <div class="grid_16 pad_bottom">  
            <div class="container960 center">
            <!-- set groupindex value on a column to default grouping on load -->
            <dx:ASPxGridView ID="dxgridShipments" runat="server" 
                ClientInstanceName="gridShipments" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" DataSourceID="linqAggregates" 
                AutoGenerateColumns="False" KeyFieldName="ContainerIdx" 
                oncustomcallback="dxgridShipments_CustomCallback" Width="100%">
                <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" 
                    EnableRowHotTrack="True" AutoExpandAllGroups="True" />
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsPager PageSize="50" Position="TopAndBottom" AlwaysShowPager="True">
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
                <SettingsCookies CookiesID="clship" Enabled="True" StoreColumnsWidth="False" 
                    Version="1" />
                <SettingsText Title="Search results for weight, cube and pallets shipped by client" />
                <ClientSideEvents Init="onGridInit" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ContainerIdx" Name="colContainerIdx" 
                        Visible="False" VisibleIndex="0" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Containers" ExportWidth="100" 
                        FieldName="ContainerCount" Name="colContainerCount" ReadOnly="True" 
                        VisibleIndex="1" Width="110px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Total Weight" ExportWidth="100" 
                        FieldName="SumWeight" Name="colSumWeight" ReadOnly="True" VisibleIndex="2" 
                        Width="110px">
                        <PropertiesTextEdit DisplayFormatString="F">
                       </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Total cube" ExportWidth="100" 
                        FieldName="SumCbm" Name="colSumCbm" ReadOnly="True" VisibleIndex="3" 
                        Width="110px">
                        <PropertiesTextEdit DisplayFormatString="F">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Total pallets" ExportWidth="100" 
                        FieldName="SumPackages" Name="colSumPackages" ReadOnly="True" VisibleIndex="4" 
                        Width="110px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Client" ExportWidth="125" 
                        FieldName="ClientName" Name="colClientName" ReadOnly="True" VisibleIndex="5"
                        Width="125px">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" 
                    ShowTitlePanel="True" ShowVerticalScrollBar="True" 
                    VerticalScrollableHeight="400" ShowGroupPanel="True" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
           </div> <!-- end container -->
        </div> <!-- end grid16 -->
        <div class="clear"></div>
        <div class="grid_16"> 
            <dx:LinqServerModeDataSource ID="linqAggregates" runat="server" 
                    ContextTypeName="linq.linq_aggegate_containers_udfDataContext" 
                    TableName="aggregate_containers_by_ets" />
        </div>  
    </div> 
</asp:Content>

