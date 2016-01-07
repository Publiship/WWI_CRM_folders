<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_new_uk.aspx.cs" Inherits="order_new_uk" %>

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

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

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
        <div class="grid_14 pad_bottom">
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="New Orders received for Publiship UK | Summary">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblDate" runat="server" ClientInstanceName="lbldate" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <!-- images and text -->
        <div class="grid_2">
         </div>              
        <div class="clear"></div>
        
        <!-- search options -->
        <div class="grid_16">
        
                         <dx:ASPxMenu ID="dxmnuGrid1"  ClientInstanceName="mnuGrid1" runat="server" AutoSeparators="RootOnly" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                RenderMode="Lightweight" ShowAsToolbar="True" 
                             onitemclick="dxmnuGrid1_ItemClick" Width="100%" AutoPostBack="True" 
                             HorizontalAlign="Left" ItemAutoWidth="False">
                             <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                             <LoadingPanelStyle ImageSpacing="5px">
                             </LoadingPanelStyle>
                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                             </LoadingPanelImage>
                             <Items>
                                        <dx:MenuItem Name="mnuExportRoot" Text="Choose export option">
                                        <Template>
                                            <div style="padding: 1px 5px 1px 5px">
                                             <dx:ASPxComboBox ID="aspxcboExport" runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="130px" 
                                             ToolTip="Select a format in which to export your search results" 
                                             SelectedIndex="4" Spacing="0">
                                            <Items>
                                                <dx:ListEditItem ImageUrl="~/Images/icons/16x16/file_extension_xls.png" Text="Export to Excel" 
                                                    Value="1" Selected="True" />
                                                <dx:ListEditItem Text="Export to Excel 2007" Value="2" 
                                                    ImageUrl="~/Images/icons/16x16/export_excel.png" />
                                                <dx:ListEditItem Text="Export to CSV" Value="3" 
                                                    ImageUrl="~/Images/icons/16x16/text_csv_16.png" />
                                                <dx:ListEditItem Text="Export to PDF" Value="0" 
                                                    ImageUrl="~/Images/icons/16x16/file_extension_pdf.png" />
                                                <dx:ListEditItem Text="Export to RTF" Value="4" 
                                                    ImageUrl="~/Images/icons/16x16/file_extension_rtf.png" />
                                            </Items>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                        </dx:ASPxComboBox>
                                        </div> 
                                        </Template>   
                                    </dx:MenuItem>
                                    <dx:MenuItem Name="mnuExport" Text="Export">
                                        <Image Height="16px" Url="~/Images/icons/metro/download16.png" Width="16px">
                                        </Image>
                                    </dx:MenuItem>

                             </Items>
                             <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                             <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
                         </dx:ASPxMenu>
           
        
        </div>

        <!-- individual form menus -->
        <div class="grid_16 pad_bottom">
        <!-- search grid -->
            <dx:ASPxGridView ID="dxgridOrders" ClientInstanceName="gridOrders" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" width="100%" 
                KeyFieldName="OrderID" DataSourceID="linqOrders">
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
                <SettingsPager PageSize="35" Position="TopAndBottom">
                </SettingsPager>
                <Columns>
                        <dx:GridViewDataTextColumn Caption="orderid" FieldName="OrderID" 
                             Name="col_orderid" ReadOnly="True" ShowInCustomizationForm="False" 
                             UnboundType="Integer" Visible="False" VisibleIndex="-1" Width="0px">
                             <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                                     ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Order number" FieldName="OrderNumber" 
                             Name="col_ordernumber" ReadOnly="True" VisibleIndex="1"  
                             Width="110px">
                         </dx:GridViewDataTextColumn>
		                <dx:GridViewDataTextColumn FieldName="origin_port" VisibleIndex="10" 
                            Caption="Origin Port" ReadOnly="True" Width="120px" Name="col_origin" 
                            ExportWidth="80">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="destination_port" VisibleIndex="12" 
                            Caption="Destination Port" ReadOnly="True" Width="120px" Name="col_dest" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_place" VisibleIndex="14" 
                            Caption="Final Destination" Width="160px" Name="col_final" 
                          ExportWidth="90">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ExWorksDate" VisibleIndex="16" 
                            Caption="Ex-Works date" ReadOnly="True" Width="110px" Name="col_exworks" 
                                 ExportWidth="75">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="5" 
                            Caption="Customer" ReadOnly="True" Width="250px" Name="col_company" 
                                 ExportWidth="75">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="printer_name" VisibleIndex="4" 
                            Caption="Printer" ReadOnly="True" Width="250px" Name="col_printer" 
                                 ExportWidth="90">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                </Columns> 
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
    
    <dx1:ASPxHiddenField ID="dxhfOrder" ClientInstanceName="hfOrder" runat="server">
    </dx1:ASPxHiddenField>
    
     <dx:ASPxGridViewExporter ID="gridExporter" runat="server" 
        GridViewID="dxgridOrders" Landscape="True" PaperKind="A4">
    </dx:ASPxGridViewExporter>
    
    <dx:LinqServerModeDataSource ID="linqOrders" runat="server" 
        ContextTypeName="linq.linq_view_new_orders_udfDataContext" 
        TableName="view_orders_newResult" />
    

</asp:Content>

