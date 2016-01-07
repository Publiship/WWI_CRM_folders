<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="advance_history.aspx.cs" Inherits="advance_history" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSiteMapControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxObjectContainer" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPager" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>


     
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDataView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHeadline" tagprefix="dx" %>


     
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxNavBar" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTitleIndex" tagprefix="dx" %>


     
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTimer" tagprefix="dx" %>

 
<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" runat="server">

    <script type="text/javascript">
        // <![CDATA[
        function submit_query(s) {
            if (!grdorders.InCallback()) {
                hfMethod.Set("mode", s);
                grdorders.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
        }

        function submit_mode(s) {
            if (!grdorders.InCallback()) {
                hfMethod.Set("mode", s);
            }
        }

        function submit_callback_request(s) {
            if (!grdorders.InCallback()) {
                //dxgridviewPrices1.PerformCallback('batchupdate');
                grdorders.PerformCallback(' ');
            }
        }

        //custom button on grid
        function grdprice_custombuttonclick(s, e) {
            e.processOnServer = false;

            if (!grdorders.InCallback()) {
                
                user = verify_user();

                if (user != 'You are not logged in') {
                    if (e.buttonID == 'btnorder') {
                        var window1 = popDefault.GetWindowByName('filterform');
                        popDefault.SetWindowContentUrl(window1, '');
                        popDefault.SetWindowContentUrl(window1, 'Popupcontrol/Pod_Search.aspx?qr=' + s.GetRowKey(e.visibleIndex));
                        //popDefault.SetWindowContentUrl(window, 'Popupcontrol/Pod_Search.aspx?');

                        //popDefault.RefreshWindowContentUrl(window); don't use - this causes IE7 "resend" problem
                        popDefault.ShowWindow(window1);
                    }
                }
                else {

                    var window2 = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window2);
                }
            }
        }

        function btncols_Click(s, e) {
            if (grdorders.IsCustomizationWindowVisible())
                grdorders.HideCustomizationWindow();
            else
                grdorders.ShowCustomizationWindow();
            UpdateButtonText();
        }

        function grid_CustomizationWindowCloseUp(s, e) {
            UpdateButtonText();
        }

        function UpdateButtonText() {
            var text = grdorders.IsCustomizationWindowVisible() ? "Hide" : "Show";
            text += " field chooser";
            btnCols.SetText(text);
        }

        function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }


        //16/03/2011 open demo popup
        function btndemo_Click(s, e) {

            if (!grdorders.InCallback()) {

                if (s == 1) {
                    var window = popDefault.GetWindowByName('demotracking1');
                    popDefault.SetWindowContentUrl(window, '');
                    popDefault.SetWindowContentUrl(window, 'Popupcontrol/Ord_Demo_Tracking.aspx');
                    popDefault.ShowWindow(window);
                }
            }
        }
        //********************
        // ]]>
    </script>

    
        <div class="innertube">  <!-- just a container div --> 
                
                <!-- centered box for header -->
                <div class="formcenter"><img src="Images/icons/world.png"
                        title = "Shipment Tracking" alt="" 
                        class="h1image" /><h1> Search advance orders</h1></div> 
                
                 <div class="formcenter580note">
                    <div class="cell580_100"><strong>Quick search</strong></div>
                    <div class="cell580_80"><dx:ASPxLabel ID="dxlblsearch1" 
                            ClientInstanceName="lblsearch1" runat="server" 
                            Text="Finds exact matches and the closest matches for e.g. Title or description"></dx:ASPxLabel></div>
                    <div class="cell580_20">
                             <dx:ASPxButton ID="aspxbtnInfo" runat="server" 
                                        TabIndex="99" 
                                        ClientInstanceName="btninfo" EnableClientSideAPI="True" 
                                        AutoPostBack="False" CausesValidation="False" 
                                        ToolTip="More information" 
                                        EnableTheming="False" 
                                 Text="Help" 
                                 RightToLeft="False" Cursor="pointer" EnableDefaultAppearance="False" 
                                 ClientVisible="False" ClientEnabled="False" Visible="False" 
                                 UseSubmitBehavior="False">
                                       <Image Height="24px" Width="24px" AlternateText="Help" 
                                           Url="~/Images/icons/24x24/help.png">
                                       </Image>
                                       <ClientSideEvents Click="function(s, e) {
	                                        btndemo_Click(1);
                                        }" />
                              </dx:ASPxButton>
                        </div> 
                    <div class="cell580_40">
                        
                             <dx:ASPxComboBox ID="dxcbofields" runat="server" ClientInstanceName="cbofields" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue"  Width="200px"
                                        DataSourceID="ObjectDataSourceFields" SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        ValueType="System.String" 
                                 ToolTip="Pick what you want to search for here e.g. ISBN number" 
                                 TabIndex="1">
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="FilterCaption" Name="fieldcaption" 
                                                Caption="Search in" />
                                            <dx:ListBoxColumn FieldName="FieldName" Name="fieldname" Visible="False" />
                                            <dx:ListBoxColumn FieldName="ColumnType" Name="columntype" Visible="False" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                        
                    </div>
                    <div class="cell580_40">
                            
                            <dx:ASPxTextBox ID="txtQuickSearch" runat="server" 
                                            ClientInstanceName="txtQuick" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            Width="200px" TabIndex="2" 
                                      NullText="Your reference here" EnableClientSideAPI="True" 
                                ToolTip="Enter the reference you are looking for e.g. the ISBN number or title" 
                                EnableViewState="False">
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' '\/]{1,100}$" />
                                            </ValidationSettings>
                                            <ClientSideEvents KeyUp="function(s, e) {textboxKeyup}" />
                                            </dx:ASPxTextBox> 
                        
                        </div>
                    <div class="cell580_20">
                     
                         <dx:ASPxButton ID="aspxbtnFilter" runat="server" 
                                        TabIndex="3" 
                                        ClientInstanceName="btnFilter" EnableClientSideAPI="True" 
                                        AutoPostBack="False" CausesValidation="False" 
                                        ToolTip="Click here to begin your search" 
                                        EnableTheming="False"  
                                        Height="24px" 
                                 Text="Search" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                UseSubmitBehavior="False">
                                       <Image Height="24px" Width="24px" AlternateText="Begin search" 
                                           Url="~/Images/icons/24x24/magnifier.png">
                                       </Image>
                                       <ClientSideEvents Click="function(s, e) {
	                                        window.lblmsgbox.SetText(txtQuick.GetText());
	                                        submit_query(1);
                                        }" />
                              </dx:ASPxButton>
                     
                        </div> 
                    <div class="formfooter"></div> 
                </div>
                <!-- end centered basic search form -->
                
                <!-- end centered advanced search form -->
    
                <!-- centered box for search options -->
            <!-- end centered box -->
            <div class="bottom-separator"></div>
                 <div class="top-panel">
                      <dx:ASPxLabel ID="lblmsgboxdiv" runat="server" ClientInstanceName="lblmsgbox" 
                              Text="Search Results"></dx:ASPxLabel>
                 </div> 
                
                
                <!-- start grid options -->
                <div class="top-panel-shaded">
                     <div class="cell100" >
	                
		             <dx:ASPxComboBox ID="aspxcboExport" runat="server" 
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                ValueType="System.String" Width="130px" 
                             ToolTip="Select a format in which to export your search results" 
                             SelectedIndex="4">
                                <Items>
                                    <dx:ListEditItem Text="Export to CSV" Value="3" 
                                        ImageUrl="~/Images/icons/16x16/text_csv_16.png" />
                                    <dx:ListEditItem Text="Export to PDF" Value="0" 
                                        ImageUrl="~/Images/icons/16x16/file_extension_pdf.png" />
                                    <dx:ListEditItem Text="Export to RTF" Value="4" 
                                        ImageUrl="~/Images/icons/16x16/file_extension_rtf.png" />
                                    <dx:ListEditItem Text="Export to Excel 2007" Value="2" 
                                        ImageUrl="~/Images/icons/16x16/export_excel.png" />
                                    <dx:ListEditItem ImageUrl="~/Images/icons/16x16/file_extension_xls.png" Text="Export to Excel" 
                                        Value="1" Selected="True" />
                                </Items>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                            </dx:ASPxComboBox>

                     </div> <!-- end export drop down -->
                     <div class="cell100">
                         <dx:ASPxButton ID="btnExport" runat="server" UseSubmitBehavior="False"
                                OnClick="btnExport_Click" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                             Width="35px" Text="Export" 
                             ToolTip="Click here to display your search results in pdf, excel or text formats" 
                             EnableTheming="True" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                             CssPostfix="Office2003Blue" >
                             <Image Height="16px" Url="~/Images/icons/16x16/download.png" Width="16px">
                             </Image>
                         </dx:ASPxButton>
                     </div> 
                     <!-- end export command -->
                        <div class="cell100">
                            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="Show detail" UseSubmitBehavior="False" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                      onclick="btnExpandAll_Click" Width="110px" 
                                ToolTip="Click to see more information about your shipments" 
                                EnableTheming="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue">
                                <Image Height="16px" Width="16px" Url="~/Images/icons/16x16/table_multiple.png">
                                </Image>
                            </dx:ASPxButton>
                     </div> 
                     <!-- end expand all -->
                     <div class="cell100">
                            <dx:ASPxButton ID="btnEndGroup" runat="server" Text="Ungroup" UseSubmitBehavior="False" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                            onclick="btnEndGroup_Click" Width="110px" Height="27px" 
                                ToolTip="Click to remove all column groupingas from your search results" 
                                EnableTheming="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" >
                                 <Image AlternateText="Ungroup" Height="16px" 
                                     Url="~/Images/icons/16x16/shape_ungroup.png" Width="16px">
                                 </Image>
                             </dx:ASPxButton>
                     </div> <!-- end group command -->
                     <div class="cell100">
		                 <dx:ASPxButton ID="btnEndFilter" runat="server" Text="Clear search" UseSubmitBehavior="False"
                                OnClick="btnEndFilter_Click" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="110px" 
                                      ToolTip="Click to clear your current search results and start again" EnableTheming="False" 
                                      Height="27px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                      CssPostfix="Office2003Blue" >
                                      <Image AlternateText="Clear search" Height="16px" 
                                          Url="~/Images/icons/16x16/arrow_refresh.png" Width="16px">
                                      </Image>
                         </dx:ASPxButton>
                     </div>  <!-- end undo filter command --->
                      <div class="cell100">
                        <dx:ASPxButton runat="server" ID="btnColumns" ClientInstanceName="btnCols"
                                        Text="Show field chooser"
                                        UseSubmitBehavior="False" AutoPostBack="False" 
                                          SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                          Height="24px" Width="159px" 
                              ToolTip="Click to pick the columns you want to see in your search results" 
                              EnableTheming="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                              CssPostfix="Office2003Blue" ClientEnabled="False" ClientVisible="False" 
                              Visible="False">
                                        <Image Height="16px" Url="~/Images/icons/16x16/attributes_display.png" 
                                            Width="16px">
                                        </Image>
                                        <ClientSideEvents Click="btncols_Click" />
                                        </dx:ASPxButton>
                      </div> <!-- end field chooser command -->
                </div> <!-- end grid options -->
            
            <!-- grid --->
            <!-- <div> -->
         <!-- </div> --> <!-- end grid wrapper -->     
         <dx:ASPxGridView ID="dxgrdorders" ClientInstanceName="grdorders" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" DataSourceID="objdsOrders" 
                KeyFieldName="OrderID" onrowcommand="dxgrdorders_RowCommand" width="100%" 
                onrowupdated="dxgrdorders_RowUpdated" 
                OnCustomButtonCallback="grid_CustomButtonCallback" 
                oncustombuttoninitialize="dxgrdorders_CustomButtonInitialize" 
                ondatabound="dxgrdorders_DataBound">
                <SettingsBehavior ColumnResizeMode="Control" EnableRowHotTrack="True" />
                <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue">
                    <LoadingPanel ImageSpacing="10px">
                    </LoadingPanel>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager Position="TopAndBottom">
                </SettingsPager>
                <SettingsText PopupEditFormCaption="Edit Order" />
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
                <SettingsEditing PopupEditFormHeight="500px" 
                    PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormModal="True" 
                    PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="500px" 
                    Mode="PopupEditForm" EditFormColumnCount="1" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options" Visible="True" 
                        Width="125px" >
                                        <ClearFilterButton Visible="false"></ClearFilterButton>
                                        <CancelButton Visible="true"></CancelButton>
                                        <EditButton Visible="true"></EditButton>
                                        <UpdateButton Visible="true"></UpdateButton> 
                                        <CustomButtons> 
                                         <dx:GridViewCommandColumnCustomButton ID="cmdRemove" Text="Cancel order">
                                        </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                                    
                <dx:GridViewDataTextColumn Caption="Print" FieldName="" 
                             Name="colquoteid" ReadOnly="false"  VisibleIndex="1" Width="50px" 
                        ExportWidth="0">
                             <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                                 AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <EditFormSettings Visible="False" />    
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnlabels" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnlabels" Cursor="pointer" 
                                     ToolTip="Print labels" EnableDefaultAppearance="False" EnableTheming="False"     
                                     Width="50px" CommandArgument="print_labels" Image-Height="16px" Image-Width="16px" ImagePosition="Right" Text="">
                                     <Image AlternateText="click to print labels" Height="16px"  
                                         Url="~/Images/icons/16x16/printer.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate>
                   </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="OrderID" VisibleIndex="1" Width="100px" 
                        ExportWidth="100" ReadOnly="True" Name="colOrderID">
                        <EditFormSettings Visible="False" VisibleIndex="-1" /> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="2" 
                        ExportWidth="100" Width="100px" Caption="Order No." ReadOnly="True" 
                        Name="colOrderNumber">
                        <EditFormSettings Visible="False" VisibleIndex="-1" CaptionLocation="Near" ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="Date order received" ExportWidth="130" 
                        FieldName="DateOrderReceived" VisibleIndex="0" Width="130px" Name="colDateOrderReceived"
                        ReadOnly="True">
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                        <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Near" ColumnSpan="1"/>  
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Payee" VisibleIndex="4" ExportWidth="120" 
                        Width="120px" Caption="Payee" Name="colPayee">
                        <PropertiesTextEdit Width="250px">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" VisibleIndex="4" CaptionLocation="Near" ColumnSpan="1"/>  
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataMemoColumn Caption="Delivery to" FieldName="DeliveryAddress" 
                        ToolTip="200" VisibleIndex="5" Width="200px" Name="colDeliveryAddress">
                        <PropertiesMemoEdit Width="250px" Height="200px">
                        </PropertiesMemoEdit>
                        <EditFormSettings Visible="True" VisibleIndex="8" CaptionLocation="Near" ColumnSpan="1" /> 
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Destination country" ExportWidth="120" 
                        FieldName="DestinationCountry" VisibleIndex="6" Width="120px" 
                        Name="colDestinationCountry">
                        <PropertiesComboBox ValueType="System.String" Width="250px">
                        </PropertiesComboBox>
                        <EditFormSettings Visible="True" VisibleIndex="12" CaptionLocation="Near" ColumnSpan="1" />
                    </dx:GridViewDataComboBoxColumn>
                     <dx:GridViewDataDateColumn FieldName="CargoReadyDate" VisibleIndex="7" 
                        ExportWidth="120" Width="120px" Caption="Cargo ready date" 
                        Name="colCargoReadyDate">
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                        <EditFormSettings Visible="True" VisibleIndex="16" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Fao" VisibleIndex="8" ExportWidth="100" 
                        Width="100px" Caption="Attention of" Name="colFao">
                        <EditFormSettings Visible="True" VisibleIndex="14" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CustomerRef" VisibleIndex="9" 
                        ExportWidth="100" Width="100px" Caption="Customer ref." ReadOnly="True" 
                        Name="colCustomerRef">
                        <EditFormSettings Visible="False" VisibleIndex="1" CaptionLocation="Near" ColumnSpan="1"/>  
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PrintersRef" VisibleIndex="10" 
                        ExportWidth="100" Width="100px" Caption="Printers ref." ReadOnly="True" 
                        Name="colPrintersRef">
                        <EditFormSettings Visible="False" VisibleIndex="5" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="CargoReceivedDate" VisibleIndex="11" 
                        ExportWidth="130" Width="130px" Caption="Cargo received date" 
                        Name="colCargoReceivedDate">
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                        <EditFormSettings Visible="False" VisibleIndex="9" CaptionLocation="Near"  ColumnSpan="1"/>  
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="Etd" 
                        VisibleIndex="12" ExportWidth="75" 
                        Width="75px" Caption="ETD" ReadOnly="True" Name="colEtd">
                        <EditFormSettings Visible="False" VisibleIndex="13" CaptionLocation="Near"  ColumnSpan="1"/>  
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="Eta" VisibleIndex="13" ExportWidth="75" 
                        Width="75px" Caption="ETA" ReadOnly="True" Name="colEta">
                        <EditFormSettings Visible="False" VisibleIndex="17" CaptionLocation="Near"  ColumnSpan="1"/>  
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="HAWBno" VisibleIndex="14" 
                        ExportWidth="80" Width="80px" Caption="HAWB No." ReadOnly="True" 
                        Name="colHAWBno">
                        <EditFormSettings Visible="False" VisibleIndex="2" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="HAWBAdded" VisibleIndex="15" 
                        ExportWidth="80" Width="80px" Caption="HAWB added" ReadOnly="True" 
                        Name="colHAWBAdded">
                        <EditFormSettings Visible="False" VisibleIndex="6" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn FieldName="ShippedonBoard" VisibleIndex="16" 
                        ExportWidth="110" Width="110px" Caption="Shipped on board" ReadOnly="True" 
                        Name="colShippedonBoard">
                        <EditFormSettings Visible="False" VisibleIndex="10" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="Titles" VisibleIndex="17" 
                        ExportWidth="50" Width="50px" Caption="Titles" ReadOnly="True" 
                        Name="colTitles">
                        <EditFormSettings Visible="False" /> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Cartons" VisibleIndex="18" 
                        ExportWidth="50" Width="50px" Caption="Cartons" ReadOnly="True" 
                        Name="colCartons">
                        <EditFormSettings Visible="False" /> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ActualWeight" VisibleIndex="19" 
                        ExportWidth="50" Width="70px" Caption="Actual Wt." ReadOnly="True" 
                        Name="colActualWeight">
                        <EditFormSettings Visible="False" VisibleIndex="14" CaptionLocation="Near"  ColumnSpan="1"/>  
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ActualVolume" VisibleIndex="20" 
                        ExportWidth="50" Width="70px" Caption="Actual Vol." ReadOnly="True" 
                        Name="colActualVolume">
                        <EditFormSettings Visible="False" VisibleIndex="18" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataMemoColumn Caption="Remarks to customer" FieldName="RemarkstoCust" 
                        ToolTip="200" VisibleIndex="21" Width="200px" Name="colRemarkstoCust">
                        <PropertiesMemoEdit Width="250px" Height="200px">
                        </PropertiesMemoEdit>
                        <EditFormSettings Visible="False" VisibleIndex="22" CaptionLocation="Near" ColumnSpan="1" /> 
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataTextColumn FieldName="CompositeInvRef" VisibleIndex="23" 
                        ExportWidth="75" Width="75px" Caption="Invoice Ref." ReadOnly="True" 
                        Name="colCompositeInvRef">
                        <EditFormSettings Visible="False" VisibleIndex="3" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="InsuranceValue" VisibleIndex="24" 
                        ExportWidth="75" Width="75px" Caption="Insurance" ReadOnly="True" 
                        Name="colInsuranceValue">
                        <EditFormSettings Visible="False" VisibleIndex="7" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="Cancel Request" 
                        FieldName="CancelRequestRcd" ReadOnly="True" VisibleIndex="25" 
                        Name="colCancelRequestRcd">
                        <EditFormSettings Visible="False" VisibleIndex="11" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="Cancelled" FieldName="CancelDate" 
                        VisibleIndex="26" Name="colCancelDate">
                        <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:d}">
                        </PropertiesDateEdit>
                        <EditFormSettings Visible="False" VisibleIndex="15" CaptionLocation="Near"  ColumnSpan="1"/> 
                    </dx:GridViewDataDateColumn>
                </Columns>
                 <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="dxgrdtitles" ClientInstanceName="grdsummary" 
                                runat="server" AutoGenerateColumns="False"
                                Width="550px" 
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" DataSourceID="objdsTitles" 
                                KeyFieldName="PATitleID" OnBeforePerformDataSelect="grdtitles_BeforePerformDataSelect" 
                            ondatabound="dxgrdtitles_DataBound" onrowupdated="dxgrdtitles_RowUpdated" 
                            onrowdeleted="dxgrdtitles_RowDeleted" 
                            onrowinserted="dxgrdtitles_RowInserted" 
                            onrowinserting="dxgrdtitles_RowInserting">
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options" Visible="True" 
                                        Width="100px" >
                                        <ClearFilterButton Visible="false"></ClearFilterButton>
                                        <NewButton Visible="True">
                                        </NewButton>
                                        <CancelButton Visible="true"></CancelButton>
                                        <EditButton Visible="true"></EditButton>
                                        <UpdateButton Visible="true"></UpdateButton> 
                                        <DeleteButton Visible="true"></DeleteButton>     
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="PATitleID" VisibleIndex="2" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PAOrderID" VisibleIndex="3" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="1" Width="350px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" 
                                    AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                <SettingsPager Position="TopAndBottom" PageSize="50">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Settings ShowGroupButtons="False" ShowFilterRow="false"  />
                                <SettingsDetail ExportMode="All" ShowDetailRow="True" 
                                    ShowDetailButtons="True" />
                                <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue">
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                    <TitlePanel Paddings-Padding="8px">
<Paddings Padding="8px"></Paddings>
                                    </TitlePanel>
                                    <LoadingPanel ImageSpacing="10px">
                                    </LoadingPanel>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                                 <SettingsDetail ShowDetailRow="True" /> 
                               <Templates>
                                    <DetailRow>
                                        <dx:ASPxGridView ID="dxgrdcartons" ClientInstanceName="grdcartons" 
                                            runat="server" AutoGenerateColumns="False"  
                                            KeyFieldName="PubAdvCartonID"  DataSourceID="objdsCartons"
                                            
                                            OnBeforePerformDataSelect="grdcartons_BeforePerformDataSelect" onrowupdated="dxgrdcartons_RowUpdated"
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" onrowdeleted="dxgrdcartons_RowDeleted" 
                                            onrowinserted="dxgrdcartons_RowInserted" 
                                            onrowinserting="dxgrdcartons_RowInserting">
                                            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                CssPostfix="Office2003Blue">
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                            </Styles>
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
                                            <Columns>
                                                  <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options" Visible="True" 
                                                        Width="100px" >
                                                        <ClearFilterButton Visible="false"></ClearFilterButton>
                                                        <NewButton Visible="True"></NewButton>
                                                        <CancelButton Visible="true"></CancelButton>
                                                        <EditButton Visible="true"></EditButton>
                                                        <UpdateButton Visible="true"></UpdateButton> 
                                                        <DeleteButton Visible="true"></DeleteButton>     
                                                    </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="PubAdvCartonID" VisibleIndex="5" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PATitleID" VisibleIndex="6" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonLength" VisibleIndex="0" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonWidth" VisibleIndex="1" Width="85px" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonHeight" VisibleIndex="2" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonWeight" VisibleIndex="3" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                              <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" />
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsDetail ExportMode="All" ShowDetailRow="True" />
                                                <SettingsPager Position="TopAndBottom" PageSize="50">
                                            </SettingsPager>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>      
                                    </DetailRow> 
                               </Templates> 
                            </dx:ASPxGridView>
                </DetailRow>
                </Templates>
                <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="True" />
                <SettingsDetail ShowDetailRow="True" /> 
          </dx:ASPxGridView> 
      
     </div>  <!-- end content div -->
    
   
                                        
    <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
    
      
     <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FetchByActiveAdvance" 
                                
        TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
  
       <asp:ObjectDataSource ID="objdsOrders" runat="server" 
                    SelectMethod="FetchByQuery" 
                    
        TypeName="DAL.Logistics.PublishipAdvanceOrderTableController" 
        onselecting="objdsOrders_Selecting" DeleteMethod="Delete" 
        InsertMethod="Insert" 
        UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="OrderID" Type="Object" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="OrderNumber" Type="String" />
                        <asp:Parameter Name="DateOrderReceived" Type="DateTime" />
                        <asp:Parameter Name="Payee" Type="String" />
                        <asp:Parameter Name="DeliveryAddress" Type="String" />
                        <asp:Parameter Name="DestinationCountry" Type="String" />
                        <asp:Parameter Name="CompanyID" Type="Int32" />
                        <asp:Parameter Name="ConsigneeID" Type="Int32" />
                        <asp:Parameter Name="PrinterID" Type="Int32" />
                        <asp:Parameter Name="CustomerRef" Type="String" />
                        <asp:Parameter Name="PrintersRef" Type="String" />
                        <asp:Parameter Name="ContactID" Type="Int32" />
                        <asp:Parameter Name="CargoReadyDate" Type="DateTime" />
                        <asp:Parameter Name="CargoReceivedDate" Type="DateTime" />
                        <asp:Parameter Name="OriginID" Type="Int32" />
                        <asp:Parameter Name="DestID" Type="Int32" />
                        <asp:Parameter Name="FinalDestID" Type="Int32" />
                        <asp:Parameter Name="DeliveryAddressID" Type="Int32" />
                        <asp:Parameter Name="AttentionOfID" Type="Int32" />
                        <asp:Parameter Name="FlightID" Type="Int32" />
                        <asp:Parameter Name="Etd" Type="DateTime" />
                        <asp:Parameter Name="Eta" Type="DateTime" />
                        <asp:Parameter Name="HAWBno" Type="String" />
                        <asp:Parameter Name="HAWBAdded" Type="Boolean" />
                        <asp:Parameter Name="ShippedonBoard" Type="Boolean" />
                        <asp:Parameter Name="Titles" Type="Int32" />
                        <asp:Parameter Name="Cartons" Type="Int32" />
                        <asp:Parameter Name="ActualWeight" Type="Decimal" />
                        <asp:Parameter Name="ActualVolume" Type="Decimal" />
                        <asp:Parameter Name="RemarkstoAgent" Type="String" />
                        <asp:Parameter Name="RemarkstoCust" Type="String" />
                        <asp:Parameter Name="JobClosed" Type="Boolean" />
                        <asp:Parameter Name="JobClosureDate" Type="DateTime" />
                        <asp:Parameter Name="CompositeInvRef" Type="String" />
                        <asp:Parameter Name="InsuranceValue" Type="Decimal" />
                        <asp:Parameter Name="CancelRequestRcd" Type="DateTime" />
                        <asp:Parameter Name="CancelRequestByID" Type="Int32" />
                        <asp:Parameter Name="OrderCancelled" Type="Boolean" />
                        <asp:Parameter Name="CancelDate" Type="DateTime" />
                        <asp:Parameter Name="CancelledByID" Type="Int32" />
                        <asp:Parameter Name="Fao" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="OrderID" Type="Int32" />
                        <asp:Parameter Name="OrderNumber" Type="String" />
                        <asp:Parameter Name="DateOrderReceived" Type="DateTime" />
                        <asp:Parameter Name="Payee" Type="String" />
                        <asp:Parameter Name="DeliveryAddress" Type="String" />
                        <asp:Parameter Name="DestinationCountry" Type="String" />
                        <asp:Parameter Name="CompanyID" Type="Int32" />
                        <asp:Parameter Name="ConsigneeID" Type="Int32" />
                        <asp:Parameter Name="PrinterID" Type="Int32" />
                        <asp:Parameter Name="CustomerRef" Type="String" />
                        <asp:Parameter Name="PrintersRef" Type="String" />
                        <asp:Parameter Name="ContactID" Type="Int32" />
                        <asp:Parameter Name="CargoReadyDate" Type="DateTime" />
                        <asp:Parameter Name="CargoReceivedDate" Type="DateTime" />
                        <asp:Parameter Name="OriginID" Type="Int32" />
                        <asp:Parameter Name="DestID" Type="Int32" />
                        <asp:Parameter Name="FinalDestID" Type="Int32" />
                        <asp:Parameter Name="DeliveryAddressID" Type="Int32" />
                        <asp:Parameter Name="AttentionOfID" Type="Int32" />
                        <asp:Parameter Name="FlightID" Type="Int32" />
                        <asp:Parameter Name="Etd" Type="DateTime" />
                        <asp:Parameter Name="Eta" Type="DateTime" />
                        <asp:Parameter Name="HAWBno" Type="String" />
                        <asp:Parameter Name="HAWBAdded" Type="Boolean" />
                        <asp:Parameter Name="ShippedonBoard" Type="Boolean" />
                        <asp:Parameter Name="Titles" Type="Int32" />
                        <asp:Parameter Name="Cartons" Type="Int32" />
                        <asp:Parameter Name="ActualWeight" Type="Decimal" />
                        <asp:Parameter Name="ActualVolume" Type="Decimal" />
                        <asp:Parameter Name="RemarkstoAgent" Type="String" />
                        <asp:Parameter Name="RemarkstoCust" Type="String" />
                        <asp:Parameter Name="JobClosed" Type="Boolean" />
                        <asp:Parameter Name="JobClosureDate" Type="DateTime" />
                        <asp:Parameter Name="CompositeInvRef" Type="String" />
                        <asp:Parameter Name="InsuranceValue" Type="Decimal" />
                        <asp:Parameter Name="CancelRequestRcd" Type="DateTime" />
                        <asp:Parameter Name="CancelRequestByID" Type="Int32" />
                        <asp:Parameter Name="OrderCancelled" Type="Boolean" />
                        <asp:Parameter Name="CancelDate" Type="DateTime" />
                        <asp:Parameter Name="CancelledByID" Type="Int32" />
                        <asp:Parameter Name="Fao" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:Parameter Name="qry" Type="Object" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
    <asp:ObjectDataSource ID="objdsTitles" runat="server" 
                    DeleteMethod="TitleDelete" InsertMethod="TitleInsert" 
                    SelectMethod="TitlesFetchByPaOrderID" 
                    TypeName="DAL.Logistics.PublishipAdvanceTitleTableCustomcontroller" 
                    UpdateMethod="TitleUpdate">
                    <DeleteParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                
        <asp:ObjectDataSource ID="objdsCartons" runat="server" 
                    DeleteMethod="CartonDelete" InsertMethod="CartonInsert" 
                    SelectMethod="CartonsFetchByPATitleID" 
                    TypeName="DAL.Logistics.PublishipAdvanceCartonTableCustomcontroller" 
                    UpdateMethod="CartonUpdate">
                    <DeleteParameters>
                        <asp:Parameter Name="PubAdvCartonID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="CartonLength" Type="Decimal" />
                        <asp:Parameter Name="CartonWidth" Type="Decimal" />
                        <asp:Parameter Name="CartonHeight" Type="Decimal" />
                        <asp:Parameter Name="CartonWeight" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="PubAdvCartonID" Type="Int32" />
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="CartonLength" Type="Decimal" />
                        <asp:Parameter Name="CartonWidth" Type="Decimal" />
                        <asp:Parameter Name="CartonHeight" Type="Decimal" />
                        <asp:Parameter Name="CartonWeight" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
 
                            
       <dx:ASPxGridViewExporter ID="dxgrdexport" runat="server" 
        GridViewID="dxgrdtitles">
    </dx:ASPxGridViewExporter>
    
 </asp:Content> 




