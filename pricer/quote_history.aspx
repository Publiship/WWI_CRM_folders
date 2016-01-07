<%@ Page Language="C#" AutoEventWireup="true" CodeFile="quote_history.aspx.cs" MasterPageFile="~/WWI_m1.master" Inherits="quote_history" %>

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

        function submit_batch_request() {
            if (!dxgridviewPrices1.InCallback()) {
                dxgridviewPrices1.PerformCallback('batchupdate');
            }
        }
        
        function btnmore_click(s, e) {

            if (!grdOrder.InCallback()) {

                user = verify_user();

                if (user != 'You are not logged in') {
                    var window1 = popDefault.GetWindowByName('filterform');
                    popDefault.SetWindowContentUrl(window1, '');
                    popDefault.SetWindowContentUrl(window1, '../Popupcontrol/order_tracking_filter.aspx');
                    
                    //popDefault.RefreshWindowContentUrl(window); don't use - this causes IE7 "resend" problem
                    popDefault.ShowWindow(window1);
                }
                else {

                    var window2 = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window2);
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
             if(e.htmlEvent.keyCode == ASPxKey.Enter) {
                 btnFilter.Focus();
                }
            }

         function requestHistory() {
             callbackHistory.PerformCallback();
         }

         function btnmyreport_Click(s) {
             if (!dxgridviewPrices1.InCallback()) {
                 user = verify_user();

                 if (user != 'You are not logged in') {
                     hfMethod.Set("mode", s);
                     dxgridviewPrices1.PerformCallback(' ');
                     //09/03/2011 history removed from this page anyway
                     //05/11/2010 no point in getting history - this search mode is not saved to it
                     //requestHistory();
                 }
                 else {

                     var window = popDefault.GetWindowByName('msgform');
                     popDefault.ShowWindow(window);
                 }
             }
         }

         //16/03/2011 open demo popup
         function btndemo_Click(s, e) {

             if (!grdOrder.InCallback()) {

                 if (s == 1) {
                     var window = popDefault.GetWindowByName('demotracking1');
                     popDefault.SetWindowContentUrl(window, '');
                     popDefault.SetWindowContentUrl(window, '../Popupcontrol/order_tracking_demo.aspx');
                     popDefault.ShowWindow(window);
                 } 
             }
         }

         //redirect
         function btnredirect_click(s, e) {

             if (!dxgridviewPrices1.InCallback()) {

                 user = verify_user();
                 if (user != 'You are not logged in') {
                     self.location = "price_history.aspx"; 
                 }
                 else {
                     var window = popDefault.GetWindowByName('msgform');
                     popDefault.ShowWindow(window);
                 }
             }
         }
    </script>

    
        <div class="innertube">  <!-- just a container div --> 
                
                <!-- centered box for header -->
                <div class="formcenter">
                    <img src="../Images/icons/world.png"
                        title = "Shipment Tracking" alt="" 
                        class="h1image" /><h1> Your price quotes</h1></div> 
                
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
                                 ClientVisible="False" UseSubmitBehavior="False">
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
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" DataSourceID="LinqServerModePricer" 
                ClientInstanceName="dxgridviewPrices1" KeyFieldName="quote_Id" 
                oncustomcallback="gridviewPrices1_CustomCallback" 
                onrowcommand="gridviewPrices1_RowCommand">
                <SettingsCustomizationWindow Enabled="True" PopupVerticalAlign="Above" />
                <SettingsBehavior ColumnResizeMode="Control" EnableRowHotTrack="True" />
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
                <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="quote_Id" Name="colquoteid" 
                        VisibleIndex="0" Caption="Quote #" ReadOnly="true" width="90px" ExportWidth="50">
                         <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                             AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                         <DataItemTemplate>
                                <dx:ASPxButton ID="dxbtnquote" ClientInstanceName="btnquote" runat="server" 
                                Text='<%# Eval("quote_Id") %>' EnableTheming="False" AutoPostBack="true"  ImagePosition="Right" Cursor="pointer" 
                                CausesValidation="False" Height="18px" Width="70px"  CommandArgument="editpricer"
                                EnableDefaultAppearance="False" ToolTip="Click here to try this quote again" >
                              <Image Height="16px" Width="16px" AlternateText="re-use" 
                                           Url="~/Images/icons/16x16/calculator.png">
                                       </Image>
                             </dx:ASPxButton>
                         </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Name="colcopy" VisibleIndex="1" Width="30px" 
                        Caption="Copy" ShowInCustomizationForm="False" ExportWidth="0">
                     <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                             AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtncopy" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btncopy" Cursor="pointer" CommandArgument="copypricer" 
                                     ToolTip="Click here to copy details to a new quote" EnableDefaultAppearance="False" EnableTheming="False" ClientVisible="true"      
                                     Width="20px" Image-Height="16px" Image-Width="16px" ImagePosition="Right">
                                     <Image AlternateText="copy" Height="16px"  
                                         Url="~/Images/icons/16x16/application_double.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate> 
                    </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Name="colremove" VisibleIndex="1" Width="30px" 
                        Caption="Remove" ShowInCustomizationForm="False" ExportWidth="0">
                     <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                             AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"  />
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtnhide" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btnhide" Cursor="pointer" 
                                     ToolTip="Click here to remove quote" EnableDefaultAppearance="False" EnableTheming="False"  ClientVisible="true"      
                                     Width="20px" CommandArgument="hidequote" Image-Height="16px" Image-Width="16px" ImagePosition="Right">
                                     <Image AlternateText="Click here to remove quote" Height="16px"  
                                         Url="~/Images/icons/16x16/cross.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="book_title" Name="booktitle" 
                        VisibleIndex="2" Caption="Title or description" ReadOnly="true" Width="162px" ExportWidth="75">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="request_date" Name="colrequestdate" 
                        VisibleIndex="3" Caption="Date" ReadOnly="true" Width="85px" ExportWidth="75">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="in_description" Name="colindescription" 
                        VisibleIndex="4" Caption="Input type" ReadOnly="true" Width="150px" ExportWidth="75">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_length" VisibleIndex="5" 
                        Caption="Length" ReadOnly="true" Width="80px" Name="col_length" ExportWidth="50">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_width" VisibleIndex="6" 
                        Caption="Width" ReadOnly="true" Width="80px" Name="colwidth" ExportWidth="50">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_depth" VisibleIndex="7" 
                        Caption="Depth" ReadOnly="true" Width="80px" Name="coldepth" ExportWidth="50">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_weight" VisibleIndex="8" 
                        Caption="Weight" ReadOnly="true" Width="80px" Name="colweight" ExportWidth="50">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_extent" VisibleIndex="9" 
                        Caption="Paper type and extent" ReadOnly="true" Width="140px" ExportWidth="50" 
                        Name="colextent">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="in_papergsm" VisibleIndex="10" 
                        Caption="Paper type gsm" ReadOnly="true" Width="100px"  ExportWidth="50" Name="colgsm">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="in_hardback" VisibleIndex="11" 
                        Caption="Hardback" ReadOnly="true" Width="80px" Name="colhardback" ExportWidth="25">
                              <PropertiesCheckEdit DisplayTextChecked="Yes" DisplayTextUnchecked="No"
                            UseDisplayImages="False">
                         </PropertiesCheckEdit>
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="copies_carton" VisibleIndex="12" 
                        Caption="Copies per carton" ReadOnly="true" Width="110px" ExportWidth="50" Name="colcarton">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="origin_name" VisibleIndex="13" 
                        Caption="Origin" ReadOnly="true" Width="150px" Name="colorigin" ExportWidth="75">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="country_name" VisibleIndex="14" 
                        Caption="Country" ReadOnly="true" Width="120px" Name="colcountry" ExportWidth="75">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="final_name" VisibleIndex="16" 
                        Caption="Final destination" ReadOnly="true" Width="120px" Name="colfinal" ExportWidth="80">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="tot_copies" VisibleIndex="17" 
                        Caption="Total copies" ReadOnly="true" Width="100px" Name="coltotcopies" ExportWidth="50">
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="in_currency" Name="incurrency" 
                        VisibleIndex="18" Caption="Currency" ReadOnly="true" Width="125px" ExportWidth="80">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ship_via" VisibleIndex="19" 
                        Caption="Shipping via" ReadOnly="true" Width="120px" Name="colvia" ExportWidth="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="pallet_type" VisibleIndex="20"  
                        Caption="Pallet type" ReadOnly="true" Width="100px" Name="colpallet" ExportWidth="0" >
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="True" 
                    ShowVerticalScrollBar="false" VerticalScrollableHeight="400" 
                    ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
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
                            
     <dx:ASPxGridViewExporter ID="gridExport" runat="server" 
        GridViewID="gridviewPrices1" ReportHeader="Exported search results">
    </dx:ASPxGridViewExporter>

        </asp:Content> 


