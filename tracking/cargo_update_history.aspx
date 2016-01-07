<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="cargo_update_history.aspx.cs" Inherits="cargo_update_history" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>



<asp:Content ID="content_history" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

    <div class="innertube"> 
         <div class="formcenter"><img src="Images/icons/application_form_edit.png"
                        title = "Search history" alt="" 
                        class="h1image" /><h1>Cargo Update History</h1>
                        
                    <p>
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="lnkBack_Click" Text="Go back to cargo updates   ">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icons/16x16/application_go.png" />
                    </asp:LinkButton>
                    </p>
         </div> 
        
        
        <div style="width: 100%; margin: 0 Auto; padding-bottom: 10px">    
         <dx:ASPxGridView ID="dxgrdquerylog" runat="server" 
            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
            CssPostfix="Office2003Blue" DataSourceID="linqservermodecargo" 
            AutoGenerateColumns="False" KeyFieldName="cargoupdateid" width="100%" 
            ClientInstanceName="grdquerylog" onrowcommand="dxgrdquerylog_RowCommand">
            <SettingsBehavior AllowGroup="False" ColumnResizeMode="Control" 
                EnableRowHotTrack="True" />
            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue">
                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                </Header>
                <LoadingPanel ImageSpacing="10px">
                </LoadingPanel>
            </Styles>
            <SettingsPager PageSize="25">
            </SettingsPager>
            <ImagesFilterControl>
                <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                </LoadingPanel>
            </ImagesFilterControl>
            <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                </LoadingPanelOnStatusBar>
                <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                </LoadingPanel>
            </Images>
             <SettingsCookies Enabled="True" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="cargoupdateid" ReadOnly="True" 
                    Visible="False" VisibleIndex="0" Width="0px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Order Id" FieldName="orderid" 
                    VisibleIndex="1" Width="90px">
                     <DataItemTemplate>
                        <asp:LinkButton ID="lnkApply" runat="server" Text='<%# Eval("orderid") %>'></asp:LinkButton>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Order No." FieldName="OrderNumber" 
                    ReadOnly="True" VisibleIndex="2" Width="100px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="House BL" FieldName="HouseBLNUmber" 
                    ReadOnly="True" VisibleIndex="3" Width="100px">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Customers Ref." FieldName="CustomersRef" 
                    ReadOnly="True" VisibleIndex="4" Width="115px">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ISBN" ReadOnly="True" VisibleIndex="5" Width="100px">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Title" ReadOnly="True" VisibleIndex="6" Width="190px">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Old cargo ready" FieldName="pre_cargoready" 
                    ReadOnly="True" VisibleIndex="7" Width="115px">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Old est. pallets" 
                    FieldName="pre_estpallets" ReadOnly="True" VisibleIndex="8"  Width="115px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Old est. weight" FieldName="pre_estweight" 
                    ReadOnly="True" VisibleIndex="9" Width="115px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Old est. volume" FieldName="pre_estvolume" 
                    ReadOnly="True" VisibleIndex="10" Width="115px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="New cargo ready" 
                    FieldName="post_cargoready" ReadOnly="True" VisibleIndex="11" 
                    Width="120px">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="New est. pallets" 
                    FieldName="post_estpallets" ReadOnly="True" VisibleIndex="12" 
                    Width="120px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="New est. weight" FieldName="post_estweight" 
                    ReadOnly="True" VisibleIndex="13" Width="120px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="New est. volume" FieldName="post_estvolume" 
                    ReadOnly="True" VisibleIndex="14" Width="120px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Date updated" FieldName="dtupdated" 
                    ReadOnly="True" VisibleIndex="15" Width="100px">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Updated by" FieldName="ContactName" 
                    ReadOnly="True" VisibleIndex="16" Width="150px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Company" FieldName="CompanyName" 
                    VisibleIndex="19" Width="150px">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="True" GridLines="Horizontal" 
                VerticalScrollableHeight="400" ShowFilterBar="Visible" 
                ShowFilterRowMenu="True" ShowHeaderFilterButton="True" 
                ShowHorizontalScrollBar="True" />
            <StylesEditors>
                <ProgressBar Height="25px">
                </ProgressBar>
            </StylesEditors>
        </dx:ASPxGridView>
        <dx:LinqServerModeDataSource ID="linqservermodecargo" runat="server" 
            ContextTypeName="linq.Linq_Classes_CargoDataContext" 
            TableName="view_cargo_updates" />
        
     </div>
   </div>   
</asp:Content>

