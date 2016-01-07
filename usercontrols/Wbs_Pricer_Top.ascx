<%@ Control Language="C#" ClassName="PricerLastNWidget" AutoEventWireup="true" CodeFile="Wbs_Pricer_Top.ascx.cs" Inherits="usercontrols_Wbs_Pricer_Top" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>
<div>
<dx:ASPxGridView ID="dxgridpricertop" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="gridpricertop" 
    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
    CssPostfix="Office2010Blue" DataSourceID="LinqServerModePricerTop" 
    onrowcommand="dxgridpricertop_RowCommand" Width="522px" 
        KeyFieldName="quote_Id" onhtmlrowcreated="dxgridpricertop_HtmlRowCreated">
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
                    
        <dx:GridViewDataTextColumn Name="colquoteid" 
                        VisibleIndex="2" Caption="Quote" width="90px" 
            FieldName="quote_Id">
                    </dx:GridViewDataTextColumn>
           
<dx:GridViewDataTextColumn Name="colredo" Width="35px" Caption="Calc" VisibleIndex="0" 
            ReadOnly="True">
<Settings AllowAutoFilter="False" ShowFilterRowMenu="False" 
        AllowHeaderFilter="False" ShowInFilterControl="False" AllowSort="False" 
        AllowGroup="False"></Settings>
<DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnquote" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnquote" Cursor="pointer" CommandArgument="editpricer" 
                                     ToolTip="Click here to try this quote again" 
                                     EnableDefaultAppearance="False" EnableTheming="False"      
                                     ImagePosition="Right">
                                     <Image AlternateText="re-use" Height="16px"  
                                         Url="../Images/icons/16x16/200-calculator.png" Width="13px">
                                     </Image>
                                 </dx:ASPxButton>
                             
</DataItemTemplate>
</dx:GridViewDataTextColumn>
           
           <dx:GridViewDataTextColumn Name="colcopy" VisibleIndex="1" Width="37px" 
                        Caption="Copy" ShowInCustomizationForm="False">
                     <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                             AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtncopy" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btncopy" Cursor="pointer" CommandArgument="copypricer" 
                                     ToolTip="Click here to copy details to a new quote" EnableDefaultAppearance="False" EnableTheming="False" ClientVisible="true"      
                                     ImagePosition="Right">
                                     <Image AlternateText="copy" Height="18px"  
                                         Url="../Images/icons/16x16/272-windows.png" Width="20px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate> 
            </dx:GridViewDataTextColumn>
                    

        <dx:GridViewDataTextColumn FieldName="book_title" VisibleIndex="2" Caption="Title" 
            Name="coltitle" ReadOnly="True" Width="200px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_description" VisibleIndex="3" 
            Caption="Input type" Name="colinput" ReadOnly="True" Width="125px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_currency" VisibleIndex="4" 
            Caption="Currency" Name="colcurrency" ReadOnly="True" Width="155px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_length" VisibleIndex="5" 
            Caption="Length" Name="coldims1" ReadOnly="True" Width="60px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="6" 
                        Caption="Width" FieldName="in_width" Name="coldims2" 
            ReadOnly="True" Width="60px">
        </dx:GridViewDataTextColumn>
                    
        <dx:GridViewDataTextColumn FieldName="in_depth" VisibleIndex="7" 
            Caption="Depth" Name="coldims3" ReadOnly="True" Width="60px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_weight" VisibleIndex="8" 
            Caption="Weight" Name="coldims4" ReadOnly="True" Width="60px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_extent" VisibleIndex="9" 
            Caption="Paper extent" Name="colextent" ReadOnly="True" Width="82px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="in_papergsm" VisibleIndex="10" 
            Caption="Paper gsm" Name="colgsm" ReadOnly="True" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataCheckColumn FieldName="in_hardback" VisibleIndex="11" 
            Caption="Hardback" Name="colhardback" ReadOnly="True" Width="62px">
            <PropertiesCheckEdit DisplayTextChecked="Yes" DisplayTextUnchecked="No">
            </PropertiesCheckEdit>
        </dx:GridViewDataCheckColumn>
        <dx:GridViewDataTextColumn FieldName="copies_carton" VisibleIndex="13" 
            Caption="Copies/carton" Name="colcopycarton" ReadOnly="True" Width="90px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="tot_copies" VisibleIndex="14" 
            Caption="Total copies" Name="coltotcopy" ReadOnly="True" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="pallet_type" VisibleIndex="15" 
            Caption="Pallet" Width="65px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="order_no" VisibleIndex="16" 
            ReadOnly="True">
        </dx:GridViewDataTextColumn>
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
    <dx:ASPxLabel ID="dxlblerr6" ClientInstanceName="lblerr6" runat="server" 
           Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" Width="250px" 
           Wrap="True">
    </dx:ASPxLabel>
    </div>
<dx:LinqServerModeDataSource ID="LinqServerModePricerTop" runat="server" 
    ContextTypeName="linq.linq_pricer_view1DataContext" 
    TableName="view_price_clients" />
