<%@ Control Language="C#" ClassName="OrderLastNWidget" AutoEventWireup="true" CodeFile="Pod_Order_Top.ascx.cs" Inherits="usercontrols_Pod_Order_Top" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>
<div>
<dx:ASPxGridView ID="dxgridordertop" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="gridordertop" 
    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
    CssPostfix="Office2010Blue" DataSourceID="LinqServerModePodTop" 
    KeyFieldName="OrderIx" onrowcommand="dxgridordertop_RowCommand" 
    PreviewFieldName="CargoReady">
    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" 
        ColumnResizeMode="Control" />
    <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue">
        <Header ImageSpacing="5px" SortingImageSpacing="5px">
        </Header>
        <LoadingPanel ImageSpacing="5px">
        </LoadingPanel>
    </Styles>
    <SettingsPager RenderMode="Lightweight">
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
    <Border BorderStyle="None" />
    <Columns>
        <dx:GridViewDataTextColumn Name="colview" VisibleIndex="0" Width="0px" 
            ShowInCustomizationForm="False" Visible="False">
                     <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                             AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnview" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnview" Cursor="pointer" 
                                     ToolTip="Click here to view details" EnableDefaultAppearance="False" EnableTheming="False"  ClientVisible="true"      
                                     Width="20px" CommandArgument="viewdetails" Image-Height="14px" Image-Width="7px" ImagePosition="Right">
                                     <Image AlternateText="Click here to view details" Height="14px"  
                                         Url="../Images/icons/16x16/49-play.png" Width="7px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate> 
        </dx:GridViewDataTextColumn>
                    
        <dx:GridViewDataTextColumn FieldName="OrderID" Visible="False" 
            VisibleIndex="23">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="OrderNumber" Visible="False" 
            VisibleIndex="13">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="House B/L" FieldName="HouseBLNUmber" 
            Name="colHousBL" ReadOnly="True" VisibleIndex="3" Width="90px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Customers Ref" FieldName="CustomersRef" 
            Name="colRef" ReadOnly="True" VisibleIndex="2" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="ETS" FieldName="ETS" Name="colEts" 
            ReadOnly="True" VisibleIndex="8" Width="75px">
            <PropertiesDateEdit Spacing="0">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="ETA" FieldName="ETA" Name="colEta" 
            ReadOnly="True" VisibleIndex="9" Width="75px">
            <PropertiesDateEdit Spacing="0">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Title" FieldName="Title" Name="colTitle" 
            ReadOnly="True" VisibleIndex="1" Width="300px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="OrderIx" ReadOnly="True" Visible="False" 
            VisibleIndex="30">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="ETW" FieldName="ETW" Name="colEtw" 
            ReadOnly="True" VisibleIndex="10" Width="75px">
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
    <Settings GridLines="Horizontal" ShowGroupButtons="False" />
    <StylesEditors ButtonEditCellSpacing="0">
        <ProgressBar Height="21px">
        </ProgressBar>
    </StylesEditors>
</dx:ASPxGridView>

</div>
   <div>
    <dx:ASPxLabel ID="dxlblerr4" ClientInstanceName="lblerr4" runat="server" 
           Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" 
           Wrap="True">
    </dx:ASPxLabel>
    </div>
<dx:LinqServerModeDataSource ID="LinqServerModePodTop" runat="server" 
    ContextTypeName="linq.linq_classesDataContext" TableName="view_orders" />