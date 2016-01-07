<%@ Control Language="C#" ClassName="SearchLastNWidget" AutoEventWireup="true" CodeFile="Pod_Search_Top.ascx.cs" Inherits="usercontrols_Pod_Search_Top" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>
<div>
<dx:ASPxGridView ID="dxgridsearchtop" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="gridsearchtop" 
    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
    CssPostfix="Office2010Blue" DataSourceID="LinqServerModeSearchTop" 
    KeyFieldName="qry_ID" onrowcommand="dxgridsearchtop_RowCommand" 
    Width="522px">
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
        <dx:GridViewDataTextColumn Name="colqryid" VisibleIndex="6" 
                        Caption="" FieldName="qry_ID" ReadOnly="True" Visible="False">
        </dx:GridViewDataTextColumn>
                    
        <dx:GridViewDataTextColumn FieldName="qry_desc" 
            VisibleIndex="0" Caption="Search description" Name="colsearchname" 
            ReadOnly="True" Width="200px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Search date" FieldName="log_qry_date" Name="colsearchdate" 
            ReadOnly="True" VisibleIndex="5" Width="75px">
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
    <dx:ASPxLabel ID="dxlblerr5" ClientInstanceName="lblerr5" runat="server" 
           Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" Width="250px" 
           Wrap="True">
    </dx:ASPxLabel>
    </div>
<dx:LinqServerModeDataSource ID="LinqServerModeSearchTop" runat="server" 
    ContextTypeName="linq.linq_classesDataContext" TableName="db_query_logs" />