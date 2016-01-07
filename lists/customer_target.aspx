<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="customer_target.aspx.cs" Inherits="customer_target" %>

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
            if (!grdTarget.InCallback()) {
                hfMethod.Set("mode", s);
                grdTarget.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
        }

        function submit_mode(s) {
            if (!grdTarget.InCallback()) {
                hfMethod.Set("mode", s);
            }
        }

        function submit_callback_request(s) {
            if (!grdTarget.InCallback()) {
                //dxgridviewPrices1.PerformCallback('batchupdate');
                grdTarget.PerformCallback(' ');
            }
        }

        function submit_company_id(s, e) {
            if (!grdTarget.InCallback()) {
                //hfMethod.Set("companyid", 1);  //not required here
                grdTarget.PerformCallback(' ');
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
            if (grdTarget.IsCustomizationWindowVisible())
                grdTarget.HideCustomizationWindow();
            else
                grdTarget.ShowCustomizationWindow();
            UpdateButtonText();
        }

        function grid_CustomizationWindowCloseUp(s, e) {
            UpdateButtonText();
        }

        function UpdateButtonText() {
            var text = grdTarget.IsCustomizationWindowVisible() ? "Hide" : "Show";
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

            if (!grdTarget.InCallback()) {

                if (s == 1) {
                    var window = popDefault.GetWindowByName('demotracking1');
                    popDefault.SetWindowContentUrl(window, '');
                    popDefault.SetWindowContentUrl(window, 'Popupcontrol/Ord_Demo_Tracking.aspx');
                    popDefault.ShowWindow(window);
                }
            }
        }

        function btnNew_Click() {
            grdTarget.AddNewRow();
        }
        //********************
        // ]]>
    </script>

    
        <div class="innertube">  <!-- just a container div --> 
            
             <!-- centered box for header -->
                <div class="formcenter"><img src="Images/icons/world.png"
                        title = "Customer Target list" alt="" 
                        class="h1image" /><h1> Customer Target List</h1></div> 
                            
            <dx:ASPxPanel ID="dxpnlMsg" ClientInstanceName="pnlMsg" ClientVisible="false" runat="server" Width="960px">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxLabel ID="lblmsgboxdiv" runat="server" ClientInstanceName="lblmsgbox" 
                              Text="Messages"></dx:ASPxLabel>
                    </dx:PanelContent> 
                </PanelCollection> 
            </dx:ASPxPanel>
                
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
		                 <dx:ASPxButton runat="server" ID="btnColumns" ClientInstanceName="btnCols"
                                        Text="Show field chooser"
                                        UseSubmitBehavior="False" AutoPostBack="False" 
                                          SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                          Height="24px" Width="159px" 
                              ToolTip="Click to pick the columns you want to see in your search results" 
                              EnableTheming="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                              CssPostfix="Office2003Blue" ClientEnabled="True" ClientVisible="True">
                                        <Image Height="16px" Url="~/Images/icons/16x16/attributes_display.png" 
                                            Width="16px">
                                        </Image>
                                        <ClientSideEvents Click="btncols_Click" />
                                        </dx:ASPxButton>
                     </div>  <!-- end field chooser command -->
                     <div class="cell100">
                            <dx:ASPxButton ID="dxbtnNewTarget" ClientInstanceName="btnNewTarget" 
                                runat="server" AutoPostBack="False" 
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="New">
                                <Image Url="~/Images/icons/16x16/disk.png">
                                </Image>
                                <ClientSideEvents Click="btnNew_Click" />
                            </dx:ASPxButton>
                     </div> <!-- end new record button --> 
                      <div class="cell100">
                         <dx:ASPxButton ID="btnEndFilter" runat="server" Text="Clear search" UseSubmitBehavior="False" ClientVisible="false" 
                                OnClick="btnEndFilter_Click" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="110px" 
                                      ToolTip="Click to clear your current search results and start again" EnableTheming="False" 
                                      Height="27px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                      CssPostfix="Office2003Blue" >
                                      <Image AlternateText="Clear search" Height="16px" 
                                          Url="~/Images/icons/16x16/arrow_refresh.png" Width="16px">
                                      </Image>
                         </dx:ASPxButton>
                       
                      </div> <!-- end undo filter command --->
                </div> <!-- end grid options -->
            
         <!-- grid --->
         <!-- <div> -->
         <!-- </div> --> <!-- end grid wrapper -->     
         <dx:ASPxGridView ID="dxgrdTarget" ClientInstanceName="grdTarget" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" DataSourceID="objdsCustomerTarget"
                KeyFieldName="TargetID" width="100%" 
                onrowupdated="dxgrdTarget_RowUpdated" 
                OnCustomButtonCallback="grid_CustomButtonCallback" 
                oncustombuttoninitialize="dxgrdTarget_CustomButtonInitialize" 
                oncelleditorinitialize="dxgrdTarget_CellEditorInitialize" 
                onrowinserting="dxgrdTarget_RowInserting" 
                onrowupdating="dxgrdTarget_RowUpdating" 
                oncustomerrortext="dxgrdTarget_CustomErrorText">
                <SettingsBehavior ColumnResizeMode="Control" EnableRowHotTrack="True"  />
                <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue">
                    <LoadingPanel ImageSpacing="10px">
                    </LoadingPanel>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager Position="TopAndBottom" PageSize="25">
                </SettingsPager>
                <SettingsText PopupEditFormCaption="Edit Customer" />
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
                    PopupEditFormVerticalAlign="NotSet" PopupEditFormWidth="500px" 
                    Mode="EditForm" EditFormColumnCount="4" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="True">
                        </EditButton>
                        <NewButton Visible="false">
                        </NewButton>
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                                    
                <dx:GridViewDataTextColumn Caption="ID" FieldName="TargetID" 
                             Name="colTargetID" ReadOnly="True"  VisibleIndex="1" 
                        ExportWidth="0" Width="50px" ShowInCustomizationForm="False">
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Visible="False" VisibleIndex="-1" />
                    <PropertiesTextEdit Width="90px"></PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="2" Caption="Company Name" 
                        Name="colCompanyName" SortIndex="0" SortOrder="Ascending" Width="200px">
                        <EditFormSettings VisibleIndex="0" />
                        <PropertiesTextEdit  Width="200px" MaxLength="75"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="3" 
                        Caption="Contact Name" Name="colContactName" Width="200px">
                        <EditFormSettings VisibleIndex="1" />
                        <PropertiesTextEdit Width="200px" MaxLength="50"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactEmail" VisibleIndex="4" Caption="Contact Email" 
                        Name="colContactEmail" Width="200px">
                        <EditFormSettings VisibleIndex="2"/>
                        <PropertiesTextEdit Width="200px" MaxLength="50"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactPosition" VisibleIndex="5" Caption="Position" 
                        Name="colContactPosition" Width="125px">
                        <EditFormSettings VisibleIndex="3"/>
                        <PropertiesTextEdit Width="125px" MaxLength="30"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataMemoColumn Caption="Address" FieldName="CompanyAddress" 
                        Name="colCompanyAddress" VisibleIndex="6" Width="200px">
                         <PropertiesMemoEdit Width="300px" Height="75px">
                        </PropertiesMemoEdit>
                        <EditFormSettings VisibleIndex="4"/>
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataTextColumn FieldName="TelNo" VisibleIndex="7" 
                        Caption="Tel. No." Name="colTelNo" Width="125px">
                        <EditFormSettings VisibleIndex="5"/>
                        <PropertiesTextEdit Width="125px" MaxLength="50"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataMemoColumn Caption="Printers Commodity/origin/destination" 
                        FieldName="PrintersInfo" Name="colPrintersInfo" VisibleIndex="8" 
                        Width="250px">
                        <PropertiesMemoEdit Width="300px" Height="75px"></PropertiesMemoEdit>
                        <EditFormSettings VisibleIndex="6"/>
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataMemoColumn FieldName="LastContacted" VisibleIndex="9" Caption="Last Contacted" 
                        Name="colLastContacted" Width="150px">
                        <EditFormSettings VisibleIndex="7"/>
                        <PropertiesMemoEdit Width="200px" Height="75px"></PropertiesMemoEdit>
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataMemoColumn Caption="Shipping Profile" 
                        FieldName="ShippingProfile" Name="colShippingProfile" VisibleIndex="10" 
                        Width="250px">
                         <PropertiesMemoEdit Width="300px" Height="150px">
                        </PropertiesMemoEdit>
                        <EditFormSettings VisibleIndex="8" />
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataMemoColumn Caption="Comments" FieldName="Comments" 
                        Name="colComments" VisibleIndex="11" Width="250px">
                         <PropertiesMemoEdit Width="200px" Height="150px" >
                        </PropertiesMemoEdit>
                        <EditFormSettings VisibleIndex="9"/>
                    </dx:GridViewDataMemoColumn>
                    <dx:GridViewDataTextColumn Caption="Sales Code" FieldName="SalesCode" 
                        Name="colSalesCode" VisibleIndex="12" Width="90px">
                        <EditFormSettings VisibleIndex="10" />
                        <PropertiesTextEdit Width="90px" MaxLength="5"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Priority" FieldName="PriorityCode" 
                        Name="colPriorityCode" VisibleIndex="13" Width="90px">
                        <PropertiesComboBox NullDisplayText="0" ValueType="System.String" 
                            DropDownStyle="DropDown">
                        </PropertiesComboBox>
                        <EditFormSettings VisibleIndex="11"/>
                        <PropertiesComboBox Width="90px"></PropertiesComboBox> 
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn Caption="Last Update" FieldName="UpdateDate" 
                        Name="colUpdateDate" VisibleIndex="14" Width="90px">
                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                        <EditFormSettings VisibleIndex="12"/>
                        <PropertiesDateEdit Width="90px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn Caption="Updated By" FieldName="UpdateUser" 
                        Name="colUpdateUser" VisibleIndex="15" Width="100px">
                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                        <EditFormSettings VisibleIndex="13"/>
                        <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataDateColumn Caption="Created Date" 
                        FieldName="InsertDate" Name="colInsertDate" 
                        VisibleIndex="16" Width="95px">
                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                        <EditFormSettings VisibleIndex="14" />
                        <PropertiesDateEdit Width="95px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="InsertUser" VisibleIndex="17" Caption="Created By" 
                        Name="colInsertUser" Width="100px">
                       <EditFormSettings Visible="False" VisibleIndex="-1" />
                       <EditFormSettings VisibleIndex="15" />
                       <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewCommandColumn VisibleIndex="18">
                        <EditButton Visible="True">
                        </EditButton>
                        <NewButton Visible="false">
                        </NewButton>
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                </Columns>
                <SettingsEditing PopupEditFormHeight="500px" PopupEditFormWidth="600px" PopupEditFormModal="true"  EditFormColumnCount="2"/>
                 <Templates>
                    <EditForm>
                        <div style="padding: 3px 4px 3px 4px">
                         <table style="width: 1024px; padding: 5px; cellpadding:2px; empty-cells: show;">
                        <tbody>
                        <tr valign="top">
                            <td style="width:100px; text-align: right;"><dx:ASPxLabel ID="dxlblrpTargetID" ClientInstanceName="lblrpTargetID" runat="server" Text="ID"></dx:ASPxLabel></td>
                            <td style="width:200px"><dx:ASPxGridViewTemplateReplacement ID="dxrpTargetID" ReplacementType="EditFormCellEditor" ColumnID="colTargetID" runat="server" Enabled="false"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="width:100px; text-align: right;"><dx:ASPxLabel ID="dxlblrpContact" ClientInstanceName="lblrpContact" runat="server" Text="Contact Name"></dx:ASPxLabel></td>
                            <td style="width:200px"><dx:ASPxGridViewTemplateReplacement ID="dxrpContact" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colContactName" runat="server" 
                                        Enabled="true" TabIndex="1"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="width:100px">
                                &nbsp;</td><td style="width:200px">&nbsp;</td></tr>
                        <tr valign="top"><td style="text-align: right"><dx:ASPxLabel ID="dxlblrpCompany" ClientInstanceName="lblrpCompany" runat="server" Text="Company Name"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpCompany" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colCompanyName" runat="server" 
                                        Enabled="true" TabIndex="2"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpEmail" ClientInstanceName="lblrpEmail" runat="server" Text="Contact Email"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpEmail" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colContactEmail" runat="server" 
                                        Enabled="true" TabIndex="3"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpPosition" ClientInstanceName="lblrpPosition" runat="server" Text="Position"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpPosition" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colContactPosition" 
                                        runat="server" Enabled="true" TabIndex="4"></dx:ASPxGridViewTemplateReplacement></td></tr>
                        <tr valign="top"><td style="text-align: right"><dx:ASPxLabel ID="dxlblrpAdresss" ClientInstanceName="lblrpAddress" runat="server" Text="Address"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpAdresss" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colCompanyAddress" 
                                        runat="server" Enabled="true" TabIndex="5"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpTel" ClientInstanceName="lblrpTel" runat="server" Text="Tel. No."></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpTel" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colTelNo" runat="server" 
                                        Enabled="true" TabIndex="6"></dx:ASPxGridViewTemplateReplacement></td><td>&nbsp;</td><td></td></tr>
                        <tr valign="top"><td style="text-align: right"><dx:ASPxLabel ID="dxlblrpPrinter" ClientInstanceName="lblrpPrinter" runat="server" Text="Printers Commodity"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpPrinter" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colPrintersInfo" 
                                        runat="server" TabIndex="7"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpLast" ClientInstanceName="lblrpLast" runat="server" Text="Last Contacted"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpLastContacted" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colLastContacted" 
                                        runat="server" TabIndex="8"></dx:ASPxGridViewTemplateReplacement></td><td>&nbsp;</td><td></td></tr>
                        <tr valign="top"><td style="text-align: right"><dx:ASPxLabel ID="dxlblrpShipping" 
                                        ClientInstanceName="lblrpShipping" runat="server" Text="Shipping Profile"></dx:ASPxLabel></td><td>
                                    <dx:ASPxGridViewTemplateReplacement ID="dxrpShipping" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colShippingProfile" 
                                        runat="server" TabIndex="9"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpComments" 
                                        ClientInstanceName="lblrpComments" runat="server" Text="Comments"></dx:ASPxLabel></td><td>
                                    <dx:ASPxGridViewTemplateReplacement ID="dxrpComments" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colComments" runat="server" 
                                        TabIndex="10"></dx:ASPxGridViewTemplateReplacement></td><td>&nbsp;</td><td></td></tr>
                        <tr valign="top"><td style="text-align: right"><dx:ASPxLabel ID="dxlblrpSales" 
                                        ClientInstanceName="lblrpSales" runat="server" Text="Sales Code"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpSalesCode" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colSalesCode" 
                                        runat="server" TabIndex="11"></dx:ASPxGridViewTemplateReplacement></td>
                            <td style="text-align: right"><dx:ASPxLabel ID="dxlblrpPriority" 
                                        ClientInstanceName="lblrpPriority" runat="server" Text="Priority code"></dx:ASPxLabel></td><td><dx:ASPxGridViewTemplateReplacement ID="dxrpPriorityCode" 
                                        ReplacementType="EditFormCellEditor" ColumnID="colPriorityCode" 
                                        runat="server" TabIndex="12"></dx:ASPxGridViewTemplateReplacement></td><td>&nbsp;</td><td></td></tr>
                        </tbody>
                        </table> 
                        </div>
                         <div style="text-align: left; padding: 5px 2px 2px 2px">
                                <dx:ASPxGridViewTemplateReplacement ID="dxUpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                <dx:ASPxGridViewTemplateReplacement ID="dxCancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                          </div>         
                    </EditForm> 
                    <DetailRow>
                        <dx:ASPxGridView ID="dxgrdHistory" runat="server" 
                                            AutoGenerateColumns="False" ClientInstanceName="grdHistory" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" DataSourceID="objdsTargetHistory" 
                                            onbeforeperformdataselect="grdHistory_BeforePerformDataSelect" 
                                            ondatabound="grdHistory_DataBound">
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
                                                     <dx:GridViewDataTextColumn Caption="IDtarget" FieldName="IDtarget" 
                                                        Name="colIDtarget" VisibleIndex="0" Visible="false">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="History ID" FieldName="HistoryID" 
                                                        Name="colHistoryID" VisibleIndex="1" Visible="false" SortIndex="0" SortOrder="Descending">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Caption="Modified Date " FieldName="LogDate" 
                                                        Name="colLogDate" VisibleIndex="4" Width="95px">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Caption="Modified By" FieldName="LogUser" 
                                                        Name="colLogUser" VisibleIndex="5" Width="100px">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Item" FieldName="FieldName" 
                                                        Name="colFieldName" VisibleIndex="2" Width="100px">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataMemoColumn Caption="Changed From" FieldName="ChangedFrom" 
                                                        Name="colChangedFrom" VisibleIndex="3" Width="250px">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataMemoColumn>
                                                       <dx:GridViewDataMemoColumn Caption="Changed To" FieldName="ChangedTo" 
                                                        Name="colChangedTo" VisibleIndex="3" Width="250px">
                                                        <EditFormSettings Visible="False" VisibleIndex="-1" />
                                                    </dx:GridViewDataMemoColumn>
                                                </Columns>
                                                <StylesEditors>
                                                    <ProgressBar Height="25px">
                                                    </ProgressBar>
                                                </StylesEditors>
                                        </dx:ASPxGridView>
                        </DetailRow>
                </Templates>
                
                <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Visible" 
                        ShowFilterRow="True" ShowHorizontalScrollBar="True" 
                        ShowGroupedColumns="True" VerticalScrollableHeight="400" 
                    ShowFilterRowMenu="True"/>
             <SettingsCustomizationWindow Enabled="True" PopupVerticalAlign="Above"  />
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Auto" />
                <SettingsDetail ShowDetailRow="True" /> 
          </dx:ASPxGridView> 
      
     </div>  <!-- end content div -->
    
   
                                        
    <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
    
      
     <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="{0}" SelectMethod="FetchByActiveAdvance" 
                                
        TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
  
       <asp:ObjectDataSource ID="objdsCustomerTarget" runat="server" 
                    SelectMethod="FetchAll" 
                    
        TypeName="DAL.CustomerTarget.CustomerTargetListController" 
        onselecting="objdsTarget_Selecting" DeleteMethod="Delete"  OldValuesParameterFormatString="{0}"
        InsertMethod="Insert" 
        UpdateMethod="Update" oninserting="objdsCustomerTarget_Inserting">
                    <DeleteParameters>
                        <asp:Parameter Name="TargetID" Type="Object" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="InsertDate" Type="DateTime" />
                        <asp:Parameter Name="InsertUser" Type="String" />
                        <asp:Parameter Name="CompanyName" Type="String" />
                        <asp:Parameter Name="ContactName" Type="String" />
                        <asp:Parameter Name="ContactEmail" Type="String" />
                        <asp:Parameter Name="ContactPosition" Type="String" />
                        <asp:Parameter Name="CompanyAddress" Type="String" />
                        <asp:Parameter Name="TelNo" Type="String" />
                        <asp:Parameter Name="PrintersInfo" Type="String" />
                        <asp:Parameter Name="LastContacted" Type="String" />
                        <asp:Parameter Name="ShippingProfile" Type="String" />
                        <asp:Parameter Name="Comments" Type="String" />
                        <asp:Parameter Name="SalesCode" Type="String" />
                        <asp:Parameter Name="PriorityCode" Type="String" />
                        <asp:Parameter Name="UpdateDate" Type="DateTime" />
                        <asp:Parameter Name="UpdateUser" Type="String" />
                        <asp:Parameter Name="Dbtimestamp" Type="Object" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="TargetID" Type="Int32" />
                        <asp:Parameter Name="InsertDate" Type="DateTime" />
                        <asp:Parameter Name="InsertUser" Type="String" />
                        <asp:Parameter Name="CompanyName" Type="String" />
                        <asp:Parameter Name="ContactName" Type="String" />
                        <asp:Parameter Name="ContactEmail" Type="String" />
                        <asp:Parameter Name="ContactPosition" Type="String" />
                        <asp:Parameter Name="CompanyAddress" Type="String" />
                        <asp:Parameter Name="TelNo" Type="String" />
                        <asp:Parameter Name="PrintersInfo" Type="String" />
                        <asp:Parameter Name="LastContacted" Type="String" />
                        <asp:Parameter Name="ShippingProfile" Type="String" />
                        <asp:Parameter Name="Comments" Type="String" />
                        <asp:Parameter Name="SalesCode" Type="String" />
                        <asp:Parameter Name="PriorityCode" Type="String" />
                        <asp:Parameter Name="UpdateDate" Type="DateTime" />
                        <asp:Parameter Name="UpdateUser" Type="String" />
                        <asp:Parameter Name="Dbtimestamp" Type="Object" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                
    <asp:ObjectDataSource ID="objdsTargetHistory" runat="server" 
                    DeleteMethod="Delete" 
                    SelectMethod="FetchByTargetID"  OldValuesParameterFormatString="{0}"
                    TypeName="DAL.CustomerTarget.TargetHistoryCustomController">
                    <DeleteParameters>
                        <asp:Parameter Name="HistoryID" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:Parameter Name="TargetID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
       
 
                            
       <dx:ASPxGridViewExporter ID="dxgrdExport" runat="server" 
        GridViewID="dxgrdTarget">
    </dx:ASPxGridViewExporter>
    
 </asp:Content> 





