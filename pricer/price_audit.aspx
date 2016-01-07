<%@ Page Language="C#" AutoEventWireup="true" CodeFile="price_audit.aspx.cs" MasterPageFile="~/WWI_m1.master" Inherits="price_audit" %>

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
        function submit_query(s) {
            if (!dxgridviewPrices1.InCallback()) {
                hfMethod.Set("mode", s);
                dxgridviewPrices1.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
        }

        function submit_mode(s) {
            if (!dxgridviewPrices1.InCallback()) {
                hfMethod.Set("mode", s);
            }
        }

        function submit_callback_request(s) {
            if (!dxgridviewPrices1.InCallback()) {
                //dxgridviewPrices1.PerformCallback('batchupdate');
                dxgridviewPrices1.PerformCallback(' ');
            }
        }

        //custom button on grid
        function grdprice_custombuttonclick(s, e) {
            e.processOnServer = false;
            
            if (!dxgridviewPrices1.InCallback()) {
                
                user = verify_user();

                if (user != 'You are not logged in') {
                    if (e.buttonID == 'btnorder') {
                        var window = popDefault.GetWindowByName('filterform');
                        popDefault.SetWindowContentUrl(window, '');
                        popDefault.SetWindowContentUrl(window, 'Popupcontrol/Pod_Search.aspx?qr=' + s.GetRowKey(e.visibleIndex));
                        //popDefault.SetWindowContentUrl(window, 'Popupcontrol/Pod_Search.aspx?');

                        //popDefault.RefreshWindowContentUrl(window); don't use - this causes IE7 "resend" problem
                        popDefault.ShowWindow(window);
                    }
                }
                else {

                    var window = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window);
                }
            }
        }

        function btncols_Click(s, e) {
            if (dxgridviewPrices1.IsCustomizationWindowVisible())
                dxgridviewPrices1.HideCustomizationWindow();
            else
                dxgridviewPrices1.ShowCustomizationWindow();
            UpdateButtonText();
        }

        function grid_CustomizationWindowCloseUp(s, e) {
            UpdateButtonText();
        }

        function UpdateButtonText() {
            var text = dxgridviewPrices1.IsCustomizationWindowVisible() ? "Hide" : "Show";
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

            if (!gridviewPrices1.InCallback()) {

                if (s == 1) {
                    var window = popDefault.GetWindowByName('demotracking1');
                    popDefault.SetWindowContentUrl(window, '');
                    popDefault.SetWindowContentUrl(window, 'Popupcontrol/Ord_Demo_Tracking.aspx');
                    popDefault.ShowWindow(window);
                }
            }
        }
    </script>

    
        <div class="innertube">  <!-- just a container div --> 
                
                <!-- centered box for header -->
                <div class="formcenter"><img src="Images/icons/world.png"
                        title = "Shipment Tracking" alt="" 
                        class="h1image" /><h1> Search price quotes</h1></div> 
                
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
                     
                        </div> 
                        <div class="cell580_40">
                            <dx:ASPxLabel ID="dxlblsearch2" ClientInstanceName="lblsearch2" runat="server" Text="Limit search results by company">
                            </dx:ASPxLabel>
                            </div> 
                        <div class="cell580_40">
                            <dx:ASPxComboBox ID="dxcbocompany" ClientInstanceName="cbocompany" runat="server" 
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" ValueType="System.String" 
                                EnableCallbackMode="True" IncrementalFilteringMode="Contains"  
                                Width="200px" CallbackPageSize="20"  
                                
                                onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                DropDownRows="10" EnableSynchronization="False" 
                                ValueField="CompanyID" 
                                onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                ToolTip="You only need to type part of a company name">
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                            
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="invalid text" 
                                        ValidationExpression="^[\d_0-9a-zA-Z' '\/]{1,100}$" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                    
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
                              CssPostfix="Office2003Blue">
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
      
            <dx:ASPxGridView ID="gridviewPrices1" runat="server" 
                AutoGenerateColumns="False"  width="100%"
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" DataSourceID="LinqServerModePricer" 
                ClientInstanceName="dxgridviewPrices1" KeyFieldName="quote_Id" 
                oncustomcallback="gridviewPrices1_CustomCallback" 
                onrowcommand="gridviewPrices1_RowCommand" 
                onhtmlrowcreated="gridviewPrices1_HtmlRowCreated">
                <SettingsCustomizationWindow Enabled="True" PopupVerticalAlign="Above" />
                <SettingsBehavior ColumnResizeMode="Control" EnableRowHotTrack="True" />
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsEditing EditFormColumnCount="1" Mode="EditFormAndDisplayRow" PopupEditFormWidth="600px" />
                <SettingsPager PageSize="25">
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
                <SettingsCookies Enabled="True" />
                <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" CustomButtonClick="function(s, e) {
	grdprice_custombuttonclick(s,e);
}" />
                <Columns>
               
                    <dx:GridViewDataTextColumn Caption="Quote #" FieldName="quote_Id" 
                             Name="colquoteid" ReadOnly="false"  VisibleIndex="0" Width="90px" ExportWidth="50">
                             <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                                 AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <EditFormSettings Visible="False" />    
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnquote" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnquote" Cursor="pointer" 
                                     ToolTip="Click here to use this quote again" EnableDefaultAppearance="False" EnableTheming="False"     
                                     Width="50px" CommandArgument="editpricer" Image-Height="16px" Image-Width="16px" ImagePosition="Right"  Text='<%# Eval("quote_Id") %>'>
                                     <Image AlternateText="Click here to use this quote again" Height="16px"  
                                         Url="~/Images/icons/16x16/calculator.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate>
                         </dx:GridViewDataTextColumn>
                   
                       <dx:GridViewCommandColumn ButtonType="Image" Caption="Link" Width="40px" ToolTip="Click to link to an order"  ExportWidth="0"  >
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton  ID="btnorder" Text="" Image-Height="16px" Image-Width="16px" Image-Url="Images/icons/16x16/link_add.png" 
                                Image-AlternateText="Click to link to an order"   >
<Image AlternateText="Click to link to an order" Height="16px" Width="16px" 
                                    Url="../Images/icons/16x16/link_add.png"></Image>
                            </dx:GridViewCommandColumnCustomButton> 
                        </CustomButtons>
                       </dx:GridViewCommandColumn>  
                      
                   <dx:GridViewDataTextColumn FieldName="order_no" VisibleIndex="2" 
                             Caption="Order #" ReadOnly="true" Width="90px" Name="colorderno" ExportWidth="50" >
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnbreak" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnbreak" Cursor="pointer" 
                                     ToolTip="Click here to remove order link" EnableDefaultAppearance="False" EnableTheming="False"  ClientVisible="false"     
                                     Width="50px" CommandArgument="removepod" Image-Height="16px" Image-Width="16px" ImagePosition="Right" Text='<%# Eval("order_no") %>'>
                                     <Image AlternateText="Click here to remove order link" Height="16px"  
                                         Url="~/Images/icons/16x16/link_break.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate> 
                    </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Title or description" 
                        FieldName="book_title" Name="booktitle" ReadOnly="True" VisibleIndex="3" 
                        Width="135px" ExportWidth="75">
                             <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Per copy price" FieldName="price_loose" 
                             ReadOnly="True" VisibleIndex="4" Width="100px" ExportWidth="50">
                             <PropertiesTextEdit DisplayFormatString="{0:0.##}">
                             </PropertiesTextEdit>
                             <EditFormSettings Visible="False" />    
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Total price" FieldName="price_total" 
                             ReadOnly="True" VisibleIndex="5" Width="100px" ExportWidth="50">
                             <PropertiesTextEdit DisplayFormatString="{0:0.##}">
                             </PropertiesTextEdit>
                             <EditFormSettings Visible="False" />    
                         </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="in_currency" Name="incurrency" 
                             VisibleIndex="6" Caption="Currency" ReadOnly="true" Width="140px" ExportWidth="80">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="request_date" Name="colrequestdate" 
                             VisibleIndex="7" Caption="Date" ReadOnly="true" Width="85px" ExportWidth="70">
                             <EditFormSettings Visible="False" />    
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="in_description" Name="indescription" 
                             VisibleIndex="8" Caption="Input type" ReadOnly="true" Width="125px" ExportWidth="75">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_length" VisibleIndex="9" Caption="Length" 
                             ReadOnly="true" Width="80px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_width" VisibleIndex="10" Caption="Width" 
                             ReadOnly="true" Width="80px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_depth" VisibleIndex="11" Caption="Depth" 
                             ReadOnly="true" Width="80px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_weight" VisibleIndex="12" Caption="Weight" 
                             ReadOnly="true" Width="100px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_extent" VisibleIndex="13" 
                             Caption="Paper type and extent" ReadOnly="true" Width="150px" ExportWidth="75">
                        <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_papergsm" VisibleIndex="14" 
                             Caption="Paper type gsm" ReadOnly="true" Width="110px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="in_hardback" VisibleIndex="15" 
                             Caption="Hardback" ReadOnly="true" Width="100px" ExportWidth="25">
                             <PropertiesCheckEdit DisplayTextChecked="Yes" DisplayTextUnchecked="No"
                            UseDisplayImages="False">
                         </PropertiesCheckEdit>
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="copies_carton" VisibleIndex="16" 
                             Caption="Copies per carton" ReadOnly="true" Width="110px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="origin_name" VisibleIndex="17" 
                             Caption="Origin" ReadOnly="true" Width="150px" ExportWidth="75">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="country_name" VisibleIndex="18" 
                             Caption="Country" ReadOnly="true" Width="130px" ExportWidth="75">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="final_name" VisibleIndex="19" 
                             Caption="Final destination" ReadOnly="true" Width="130px" ExportWidth="75">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="tot_copies" VisibleIndex="20" 
                             Caption="Total copies" ReadOnly="true" Width="100px" ExportWidth="50">
                             <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ship_via" VisibleIndex="21" 
                        Caption="Shipping via" ReadOnly="true" Width="120px" ExportWidth="80">
                         <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="pallet_type" VisibleIndex="22"  
                        Caption="Pallet type" ReadOnly="true" Width="130px" ExportWidth="0" >
                            <EditFormSettings Visible="False" />    
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn Caption="Visible" FieldName="client_visible"  
                        Name="colclientvisible" ReadOnly="True" VisibleIndex="23" Width="90px" 
                        ExportWidth="0">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn Caption="ISBN" ExportWidth="75" FieldName="ISBN" 
                        ReadOnly="True" VisibleIndex="24" Width="75px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Printer" ExportWidth="100" 
                        FieldName="printer" ReadOnly="True" VisibleIndex="25" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="Ex-Works Date" ExportWidth="70" 
                        FieldName="exworks_date" ReadOnly="True" VisibleIndex="26" Width="95px">
                        <EditFormSettings Visible="False" />
                        <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="Delivery due" ExportWidth="70" 
                        FieldName="due_date" ReadOnly="True" VisibleIndex="27" Width="95px">
                        <EditFormSettings Visible="False" />
                        <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Caption="Email from" ExportWidth="80" 
                        FieldName="sent_by" ReadOnly="True" VisibleIndex="28" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Company" ExportWidth="100" 
                        FieldName="company" ReadOnly="True" VisibleIndex="29" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Contact email" ExportWidth="100" 
                        FieldName="contact_email" ReadOnly="True" VisibleIndex="30" Width="125px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Contact tel" ExportWidth="90" 
                        FieldName="contact_tel" ReadOnly="True" VisibleIndex="31" Width="90px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Publiship contact" ExportWidth="100" 
                        FieldName="publiship_contact" ReadOnly="True" VisibleIndex="33" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                  <Templates>
                    <DetailRow>
                    <div style="padding: 3px 3px 2px 3px">
                        <dx:ASPxPageControl runat="server" ID="dxpagedetail" ClientInstanceName="pagedetail" width="100%" EnableCallBacks="true"  CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                               <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                               </LoadingPanelImage>
                            <TabPages>
                                
                                 <dx:TabPage Text="Costing (Pre-palletised)" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <div style="padding: 8px; float: left; width:250px;">
                                            <div style="padding-bottom: 8px"><dx:ASPxLabel ID="dxlblname1" runat="server" ClientInstanceName="lblname1" 
                                                                                Text='<%# Eval("lcl_name") %>' /></div>
                                            <dx:ASPxGridView ID="dxcosting1" ClientInstanceName="costing1" runat="server" AutoGenerateColumns="False" 
                                                     KeyFieldName="quote_id" OnBeforePerformDataSelect="costing1_BeforePerformDataSelect"
                                                     Width="70%" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                   
                                                     <Templates>
                                                        <DataRow>
                                                            <div class="formcenter580">
                                                                <div style="padding: 8px; float: left; width:200px;">
                                                                    <dl class="dl2">
                                                                        <dt>Pre-Carriage</dt>
                                                                        <dd></dd>
                                                                        <dt>Part Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre1a" runat="server" ClientInstanceName="lblpre1a" 
                                                                                Text='<%# Eval("pre_part") %>' /></dd>
                                                                        <dt>Full Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre2a" runat="server" ClientInstanceName="lblpre2a" Text='<%# Eval("pre_full") %>' /></dd>
                                                                        <dt>20' TCH</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre3a" runat="server" ClientInstanceName="lblpre3a" Text='<%# Eval("pre_thc20") %>' /></dd>
                                                                        <dt>40' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre4a" runat="server" ClientInstanceName="lblpre4a" Text='<%# Eval("pre_thc40") %>' /></dd>
                                                                        <dt>LCL TCH</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre5a" runat="server" ClientInstanceName="lblpre5a" Text='<%# Eval("pre_thclcl") %>' /></dd>
                                                                        <dt>Documentation</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre6a" runat="server" ClientInstanceName="lblpre6a" Text='<%# Eval("pre_docs") %>' /></dd>
                                                                        <dt>Other Origin</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre7a" runat="server" ClientInstanceName="lblpre7a" Text='<%# Eval("pre_origin") %>' /></dd>
                                                                        <dt>20' FCL Haulage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre8a" runat="server" ClientInstanceName="lblpre8a" Text='<%# Eval("pre_haul20") %>' /></dd>
                                                                        <dt>40' FCL Haulage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre9a" runat="server" ClientInstanceName="lblpre9a" Text='<%# Eval("pre_haul40") %>' /></dd>
                                                                    </dl>
                                                                    </div>
                                                                     <div style="float: left; width:135px; padding-top: 8px;">
                                                                    <dl class="dl3">
                                                                        <dt>Freight</dt>
                                                                        <dd></dd>
                                                                        <dt>LCL</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre1a" runat="server" ClientInstanceName="dxlblfre1a" Text='<%# Eval("freight_lcl") %>' /></dd>
                                                                        <dt>20'</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre2a" runat="server" ClientInstanceName="dxlblfre2a" Text='<%# Eval("freight_20") %>' /></dd>
                                                                        <dt>40'</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre3a" runat="server" ClientInstanceName="dxlblfre3a" Text='<%# Eval("freight_40") %>' /></dd>
                                                                        <dt>40' HQC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre4a" runat="server" ClientInstanceName="dxlblfre4a" Text='<%# Eval("freight_40hq") %>' /></dd>
                                                                    </dl>
                                                                    </div>
                                                                     <div style="padding: 8px; float: right; width:200px;">
                                                                    <dl class="dl2">
                                                                        <dt>On-Carriage</dt>
                                                                        <dd></dd>
                                                                        <dt>Dest LCL THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc1a" runat="server" ClientInstanceName="lblonc1a" Text='<%# Eval("on_dest_lcl") %>' /></dd>
                                                                        <dt>Pier Loading etc</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc2a" runat="server" ClientInstanceName="lblonc2a" Text='<%# Eval("on_pier_etc") %>' /></dd>
                                                                        <dt>Dest 20' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc3a" runat="server" ClientInstanceName="lblonc3a" Text='<%# Eval("on_dest_20") %>' /></dd>
                                                                        <dt>Dest 40' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc4a" runat="server" ClientInstanceName="lblonc4a" Text='<%# Eval("on_dest_40") %>' /></dd>
                                                                        <dt>Documentation</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc5a" runat="server" ClientInstanceName="lblonc5a" Text='<%# Eval("on_docs") %>' /></dd>
                                                                        <dt>Customs</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc6a" runat="server" ClientInstanceName="lblonc6a" Text='<%# Eval("on_customs") %>' /></dd>
                                                                        <dt>Part Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc7a" runat="server" ClientInstanceName="lblonc7a" Text='<%# Eval("on_part") %>' /></dd>
                                                                        <dt>Full Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc8a" runat="server" ClientInstanceName="lblonc8a" Text='<%# Eval("on_full") %>' /></dd>
                                                                        <dt>20' FCL Haualage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc9a" runat="server" ClientInstanceName="lblonc9a" Text='<%# Eval("on_haul20") %>' /></dd>
                                                                        <dt>40' FCL Haualage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc10a" runat="server" ClientInstanceName="lblonc10a" Text='<%# Eval("on_haul40") %>' /></dd>
                                                                        <dt>20' Shunt and Devan</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc11a" runat="server" ClientInstanceName="lblonc11a" Text='<%# Eval("on_shunt20") %>' /></dd>
                                                                        <dt>40' Shunt and Devan</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc12a" runat="server" ClientInstanceName="lblonc12a" Text='<%# Eval("on_shunt40") %>' /></dd>
                                                                        <dt>Pallets</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc13a" runat="server" ClientInstanceName="lblonc13a" Text='<%# Eval("on_pallets") %>' /></dd>
                                                                        <dt>Other Dest Charges</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc14a" runat="server" ClientInstanceName="lblonc14a" Text='<%# Eval("on_other") %>' /></dd>
                                                                    </dl>
                                                            
                                                            </div>
                                                        </DataRow> 
                                                     </Templates> 
                                                     <SettingsBehavior AllowDragDrop="false" /> 
                                                     <Settings ShowColumnHeaders="False" />
                                                      <SettingsLoadingPanel ImagePosition="Top" />
                                                     
                                                       <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                         <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                         </LoadingPanelOnStatusBar>
                                                         <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                         </LoadingPanel>
                                                     </Images>
                                            </dx:ASPxGridView> 
                                            </div> 
                                        </dx:ContentControl> 
                                    </ContentCollection> 
                                </dx:TabPage> 
                                
                                <dx:TabPage Text="Costing (Loose)" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                           
                                            <div style="padding: 8px; float: left; width:250px;">
                                             <div style="padding-bottom: 8px"><dx:ASPxLabel ID="dxlblname2" runat="server" ClientInstanceName="lblname2" 
                                                                                Text='<%# Eval("loose_name") %>' /></div>  
                                            <dx:ASPxGridView ID="dxcosting2" ClientInstanceName="costing2" runat="server" AutoGenerateColumns="False" 
                                                     KeyFieldName="quote_id" OnBeforePerformDataSelect="costing2_BeforePerformDataSelect" 
                                                     Width="70%" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                     
                                                     <Templates>
                                                        <DataRow>
                                                            <div class="formcenter580">
                                                                <div style="padding: 8px; float: left; width:200px;">
                                                                    <dl class="dl2">
                                                                        <dt>Pre-Carriage</dt>
                                                                        <dd></dd>
                                                                        <dt>Part Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre1b" runat="server" ClientInstanceName="lblpre1b" 
                                                                                Text='<%# Eval("pre_part") %>' /></dd>
                                                                        <dt>Full Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre2b" runat="server" ClientInstanceName="lblpre2b" Text='<%# Eval("pre_full") %>' /></dd>
                                                                        <dt>20' TCH</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre3b" runat="server" ClientInstanceName="lblpre3b" Text='<%# Eval("pre_thc20") %>' /></dd>
                                                                        <dt>40' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre4b" runat="server" ClientInstanceName="lblpre4b" Text='<%# Eval("pre_thc40") %>' /></dd>
                                                                        <dt>LCL TCH</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre5b" runat="server" ClientInstanceName="lblpre5b" Text='<%# Eval("pre_thclcl") %>' /></dd>
                                                                        <dt>Documentation</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre6b" runat="server" ClientInstanceName="lblpre6b" Text='<%# Eval("pre_docs") %>' /></dd>
                                                                        <dt>Other Origin</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre7b" runat="server" ClientInstanceName="lblpre7b" Text='<%# Eval("pre_origin") %>' /></dd>
                                                                        <dt>20' FCL Haulage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre8b" runat="server" ClientInstanceName="lblpre8b" Text='<%# Eval("pre_haul20") %>' /></dd>
                                                                        <dt>40' FCL Haulage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblpre9b" runat="server" ClientInstanceName="lblpre9b" Text='<%# Eval("pre_haul40") %>' /></dd>
                                                                    </dl>
                                                                    </div>
                                                                     <div style="float: left; width:135px; padding-top: 8px;">
                                                                    <dl class="dl3">
                                                                        <dt>Freight</dt>
                                                                        <dd></dd>
                                                                        <dt>LCL</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre1a" runat="server" ClientInstanceName="dxlblfre1b" Text='<%# Eval("freight_lcl") %>' /></dd>
                                                                        <dt>20'</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre2a" runat="server" ClientInstanceName="dxlblfre2b" Text='<%# Eval("freight_20") %>' /></dd>
                                                                        <dt>40'</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre3a" runat="server" ClientInstanceName="dxlblfre3b" Text='<%# Eval("freight_40") %>' /></dd>
                                                                        <dt>40' HQC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblfre4a" runat="server" ClientInstanceName="dxlblfre4b" Text='<%# Eval("freight_40hq") %>' /></dd>
                                                                    </dl>
                                                                    </div>
                                                                     <div style="padding: 8px; float: right; width:200px;">
                                                                    <dl class="dl2">
                                                                        <dt>On-Carriage</dt>
                                                                        <dd></dd>
                                                                        <dt>Dest LCL THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc1a" runat="server" ClientInstanceName="lblonc1b" Text='<%# Eval("on_dest_lcl") %>' /></dd>
                                                                        <dt>Pier Loading etc</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc2a" runat="server" ClientInstanceName="lblonc2b" Text='<%# Eval("on_pier_etc") %>' /></dd>
                                                                        <dt>Dest 20' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc3a" runat="server" ClientInstanceName="lblonc3b" Text='<%# Eval("on_dest_20") %>' /></dd>
                                                                        <dt>Dest 40' THC</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc4a" runat="server" ClientInstanceName="lblonc4b" Text='<%# Eval("on_dest_40") %>' /></dd>
                                                                        <dt>Documentation</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc5a" runat="server" ClientInstanceName="lblonc5b" Text='<%# Eval("on_docs") %>' /></dd>
                                                                        <dt>Customs</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc6a" runat="server" ClientInstanceName="lblonc6b" Text='<%# Eval("on_customs") %>' /></dd>
                                                                        <dt>Part Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc7a" runat="server" ClientInstanceName="lblonc7b" Text='<%# Eval("on_part") %>' /></dd>
                                                                        <dt>Full Load</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc8a" runat="server" ClientInstanceName="lblonc8b" Text='<%# Eval("on_full") %>' /></dd>
                                                                        <dt>20' FCL Haualage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc9a" runat="server" ClientInstanceName="lblonc9b" Text='<%# Eval("on_haul20") %>' /></dd>
                                                                        <dt>40' FCL Haualage</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc10a" runat="server" ClientInstanceName="lblonc10b" Text='<%# Eval("on_haul40") %>' /></dd>
                                                                        <dt>20' Shunt and Devan</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc11a" runat="server" ClientInstanceName="lblonc11b" Text='<%# Eval("on_shunt20") %>' /></dd>
                                                                        <dt>40' Shunt and Devan</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc12a" runat="server" ClientInstanceName="lblonc12b" Text='<%# Eval("on_shunt40") %>' /></dd>
                                                                        <dt>Pallets</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc13a" runat="server" ClientInstanceName="lblonc13b" Text='<%# Eval("on_pallets") %>' /></dd>
                                                                        <dt>Other Dest Charges</dt>
                                                                        <dd><dx:ASPxLabel ID="dxlblonc14a" runat="server" ClientInstanceName="lblonc14b" Text='<%# Eval("on_other") %>' /></dd>
                                                                    </dl>
                                                            
                                                            </div>
                                                        </DataRow> 
                                                     </Templates> 
                                                     <SettingsBehavior AllowDragDrop="false" /> 
                                                     <Settings ShowColumnHeaders="False" />
                                                      <SettingsLoadingPanel ImagePosition="Top" />
                                                     
                                                       <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                         <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                         </LoadingPanelOnStatusBar>
                                                         <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                         </LoadingPanel>
                                                     </Images>
                                            </dx:ASPxGridView> 
                                            </div> 
                                        </dx:ContentControl> 
                                    </ContentCollection> 
                                </dx:TabPage> 
                                
                                <dx:TabPage Text="Shipment size" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                                   <div style="padding: 8px; float: left; width:250px;">
                                                   
                                          <dx:ASPxGridView ID="dxshipment" ClientInstanceName="shipment" runat="server" AutoGenerateColumns="False" 
                                                     KeyFieldName="quote_id"  OnBeforePerformDataSelect="shipment_BeforePerformDataSelect"
                                                     Width="70%" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                     <Settings ShowGroupPanel="false"   />
                                                     <Templates>
                                                        <DataRow>
                                                            <div class="formcenter580">
                                                            <div style="padding: 8px; float: left; width:250px;">
                                                              <dl class="dl1">
                                                                    <dt>Total Cartons</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship2" runat="server" ClientInstanceName="lblship2" Text='<%# Eval("tot_cartons") %>' /></dd>
                                                                    <dt>Cartons On pallet</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship3" runat="server" ClientInstanceName="lblship3" Text='<%# Eval("pal_cartons") %>' /></dd>
                                                               </dl>
                                                            </div>
                                                            <div style="padding: 8px; float: right; width:250px;">
                                                            <dl class="dl1">
                                                                    <dt>Carton Height mm</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship12" runat="server" ClientInstanceName="lblship12" Text='<%# Eval("ctn_hgt") %>' /></dd>
                                                                    <dt>Carton Length mm</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship13" runat="server" ClientInstanceName="lblship13" Text='<%# Eval("ctn_len") %>' /></dd>
                                                                    <dt>Carton Width mm</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship14" runat="server" ClientInstanceName="lblship14" Text='<%# Eval("ctn_wid") %>' /></dd>
                                                                    <dt>Carton Weight kgs</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship15" runat="server" ClientInstanceName="lblship15" Text='<%# Eval("ctn_wt") %>' /></dd>
                                                               </dl>
                                                             </div>
                                                             <div style="clear: both"></div> 
                                                             <div style="padding: 8px; float: left; width:250px;">
                                                               <dl class="dl1">
                                                                    <dt>Full Pallets</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship4a" runat="server" ClientInstanceName="lblship4a" Text='<%# Eval("pal_full") %>' /></dd>
                                                                    <dt>Full Pallet Weight</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship5a" runat="server" ClientInstanceName="lblship5a" Text='<%# Eval("pal_full_wt") %>' /></dd>
                                                                    <dt>Full Pallet Cube</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship6a" runat="server" ClientInstanceName="lblship6a" Text='<%# Eval("pal_full_cu") %>' /></dd>
                                                                    <dt>Max Per Layer</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship7a" runat="server" ClientInstanceName="lblship7a" Text='<%# Eval("pal_layers") %>' /></dd>
                                                                    <dt>Number Of Layers</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship8a" runat="server" ClientInstanceName="lblship8a" Text='<%# Eval("pal_layer_count") %>' /></dd>
                                                               </dl>
                                                              </div>
                                                               <div style="padding: 8px; float: right; width:250px;">
                                                             <dl class="dl1">
                                                                    <dt>Part Pallets</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship16a" runat="server" ClientInstanceName="lblship16a" Text='<%# Eval("par_count") %>' /></dd>
                                                                    <dt>Remaining Cartons</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship17a" runat="server" ClientInstanceName="lblship17a" Text='<%# Eval("ctn_remaining") %>' /></dd>
                                                                    <dt>Residue Pallet Cube</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship18a" runat="server" ClientInstanceName="lblship18a" Text='<%# Eval("residue_cu") %>' /></dd>
                                                                    <dt>Residue Pallet Weight</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship19a" runat="server" ClientInstanceName="lblship19a" Text='<%# Eval("residue_wt") %>' /></dd>
                                                               </dl>
                                                            </div>
                                                            <div style="clear: both"></div> 
                                                              <div style="padding: 8px; float: left; width:250px;">
                                                               <dl class="dl1">  
                                                                    <dt>Total Palletised Weight</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship9a" runat="server" ClientInstanceName="lblship9a" Text='<%# Eval("pal_total_wt") %>' /></dd>
                                                                    <dt>Total Palletised Cube</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship10a" runat="server" ClientInstanceName="lblship10a" Text='<%# Eval("pal_total_cu") %>' /></dd>
                                                                    <dt>Pallet Weight:Cube Ratio</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship11a" runat="server" ClientInstanceName="lblship11a" Text='<%# Eval("pal_ratio") %>' /></dd>
                                                               </dl>
                                                            </div>
                                                        
                                                           <div style="padding: 8px; float: right; width:250px;">
                                                             <dl class="dl1">
                                                                    <dt>Total Carton Weight</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship20a" runat="server" ClientInstanceName="lblship20a" Text='<%# Eval("ctn_total_wt") %>' /></dd>
                                                                    <dt>Total Carton Cube</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship21a" runat="server" ClientInstanceName="lblship21a" Text='<%# Eval("ctn_total_cu") %>' /></dd>
                                                                    <dt>Carton Weight:Cube Ratio</dt>
                                                                    <dd><dx:ASPxLabel ID="dxlblship22a" runat="server" ClientInstanceName="lblship22a" Text='<%# Eval("ctn_ratio") %>' /></dd>
                                                               </dl>
                                                            </div> 
                                                            </div>
                                                        </DataRow> 
                                                     </Templates> 
                                                     <SettingsBehavior AllowDragDrop="false" /> 
                                                     <Settings ShowColumnHeaders="False" />
                                                     <SettingsLoadingPanel ImagePosition="Top" />
                                                     
                                                       <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                         <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                         </LoadingPanelOnStatusBar>
                                                         <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                         </LoadingPanel>
                                                     </Images>
                                                   </dx:ASPxGridView> 
                                        </dx:ContentControl> 
                                    </ContentCollection> 
                                </dx:TabPage> 
                            </TabPages> 
                        </dx:ASPxPageControl>
                     </div>  
                     </DetailRow> 
                 </Templates> 
                 
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                 
                <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="True" 
                    ShowVerticalScrollBar="false" VerticalScrollableHeight="400" 
                    ShowFilterBar="Visible" ShowFilterRow="True" 
                    ShowHeaderFilterButton="True" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
                <SettingsDetail ShowDetailRow="True" />
            </dx:ASPxGridView>

     </div>  <!-- end content div -->
    
    <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
    
    <dx:LinqServerModeDataSource ID="LinqServerModePricer" runat="server" 
        ContextTypeName="linq.linq_pricer_view1DataContext" 
        TableName="view_price_clients" />
        
     <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FetchByActivePricer" 
                                
        TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
      
       <dx:LinqServerModeDataSource ID="LinqServerModePod" runat="server" 
        ContextTypeName="linq.linq_classesDataContext" TableName="view_orders" />
        
     <dx:ASPxGridViewExporter ID="gridExport" runat="server" 
        GridViewID="gridviewPrices1" ReportHeader="Exported search results">
    </dx:ASPxGridViewExporter>

    <dx:ASPxPopupControl ID="popDefault" runat="server" AppearAfter="500" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="popDefault" 
        CloseAction="CloseButton" HeaderText="" 
        PopupAction="None" AllowDragging="True" 
        EnableHierarchyRecreation="True" AllowResize="True" MinHeight="500px" 
        MinWidth="800px" ShowSizeGrip="True">
            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
            </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
            <HeaderStyle>
            <Paddings PaddingRight="6px" />
            </HeaderStyle>
            <Windows>
                 <dx:PopupWindow CloseAction="CloseButton" 
                HeaderText="Link price quote to an order"
                Height="800px" Modal="True" Name="filterform" PopupAction="None" 
                Width="800px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
            
            </Windows> 
       </dx:ASPxPopupControl>      

        </asp:Content> 



