<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="shipment_search_history.aspx.cs" Inherits="shipment_search_history" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>


<asp:Content ID="content_history" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

    <div class="innertube"> 
    
         <div class="formcenter">
             <img src="../Images/icons/world.png" 
                        title = "Search history" alt="" 
                        class="h1image" /><h1>Search history</h1>
                        
                    <p>
                    <asp:Image ID="imgundo" runat="server" ImageUrl="~/Images/icons/16x16/application_go.png" />
                    <asp:LinkButton ID="lnkBack" runat="server" onclick="lnkBack_Click" Text="Go back to shipment tracking   ">
                    </asp:LinkButton>
                    </p>
         </div> 
        
        <div>
        <dx:ASPxGridView ID="dxgrdquerylog" runat="server" 
            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
            CssPostfix="Office2010Blue" DataSourceID="linqservermodequerylog" 
            AutoGenerateColumns="False" KeyFieldName="qry_ID" width="100%" 
            ClientInstanceName="grdquerylog" onrowcommand="dxgrdquerylog_RowCommand">
            <SettingsBehavior AllowGroup="False" ColumnResizeMode="Control" 
                EnableRowHotTrack="True" AllowFocusedRow="True" 
                AllowSelectSingleRowOnly="True" />
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
                <dx:GridViewDataTextColumn Caption="Search #" FieldName="qry_ID" 
                    ReadOnly="True" VisibleIndex="0" Width="50px" Name="colid">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Date of search" FieldName="log_qry_date" 
                    ReadOnly="True" UnboundType="DateTime" VisibleIndex="1" Width="120px" 
                    Name="coldate">
                    <PropertiesDateEdit DisplayFormatString="{0:g}" Spacing="0">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Search" FieldName="qry_desc" 
                    ReadOnly="True" VisibleIndex="2" Width="525px" Name="colname">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="by_employeeID" ReadOnly="True" 
                    VisibleIndex="5" Width="0px" Name="colemployeeid" 
                    ShowInCustomizationForm="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="by_contactID" ReadOnly="True" 
                    Visible="False" VisibleIndex="6" Width="0px" Name="colcontactid" 
                    ShowInCustomizationForm="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="qry_source" ReadOnly="True" 
                    VisibleIndex="3" Width="125px" Name="colemployeeid" 
                    ShowInCustomizationForm="False" Caption="Source" ExportWidth="125">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="qry_text" ReadOnly="True" 
                    VisibleIndex="3" Width="0px" Name="colquery" 
                    ShowInCustomizationForm="False" Visible="False">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataColumn Caption="" VisibleIndex="7" Width="90px" 
                    Name="colapply" ShowInCustomizationForm="False" ReadOnly="True" 
                    UnboundType="String">
                     <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                         AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                         AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                     <DataItemTemplate>
                         <asp:LinkButton ID="lnkApply" runat="server">Go...</asp:LinkButton>
                     </DataItemTemplate>
                 </dx:GridViewDataColumn>
            </Columns>
            <StylesPager>
                <PageNumber ForeColor="#3E4846">
                </PageNumber>
                <Summary ForeColor="#1E395B">
                </Summary>
            </StylesPager>
            <Settings ShowFilterRow="True" 
                VerticalScrollableHeight="400" ShowFilterRowMenu="True" 
                ShowHeaderFilterButton="True" ShowHorizontalScrollBar="True" 
                ShowFilterBar="Visible" />
            <StylesEditors ButtonEditCellSpacing="0">
                <ProgressBar Height="21px">
                </ProgressBar>
            </StylesEditors>
        </dx:ASPxGridView>
        <dx:LinqServerModeDataSource ID="linqservermodequerylog" runat="server" 
            ContextTypeName="linq.linq_query_logDataContext" TableName="db_query_logs" />
        </div> 
     </div>
     
</asp:Content>

