<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cargo_updates.aspx.cs" MasterPageFile="~/WWI_m1.master" Inherits="cargo_updates" %>

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


<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx1" %>



      
<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

<script type="text/javascript">
    // <![CDATA[
        function onDateRangeChanged() {
            if (!grdOrder.InCallback()) {
                grdOrder.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
        }
        
        function submit_query(s) {
            if (!grdOrder.InCallback()) {
                hfMethod.Set("mode", s);
                grdOrder.PerformCallback(' ');
                //09/03/2011 do not get update history here
                //requestHistory();
            }
        }

        function submit_mode(s) {
            if (!grdOrder.InCallback()) {
                hfMethod.Set("mode", s);
            }
        }

        function submit_batch_request() {
            if (!grdOrder.InCallback()) {
                grdOrder.PerformCallback('batchupdate');
            }
        }
        
        function btnmore_click(s, e) {

            if (!grdOrder.InCallback()) {

                user = verify_user();

                if (user != 'You are not logged in') {
                    var window = popDefault.GetWindowByName('filterform');
                    popDefault.SetWindowContentUrl(window, '');
                    popDefault.SetWindowContentUrl(window, 'Popupcontrol/Ord_Filter_Advanced.aspx');
                    
                    //popDefault.RefreshWindowContentUrl(window); this causes IE7 "resend" problem
                    popDefault.ShowWindow(window);
                }
                else {

                    var window = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window);
                }
            }
        }
       
        function btncols_Click(s, e) {
             if (grdOrder.IsCustomizationWindowVisible())
                        grdOrder.HideCustomizationWindow();
                 else
                        grdOrder.ShowCustomizationWindow();
                 UpdateButtonText();
             }
        
        function grid_CustomizationWindowCloseUp(s, e) {
                 UpdateButtonText();
        }

        function grid_CustomButtonClick(s, e) {
            e.processOnServer = false;
            var user = verify_user();

            if (user != 'You are not logged in') {
                if (e.buttonID == 'customedit') {
                    var window = popDefault.GetWindowByName('editpalletform');
                    popDefault.SetWindowContentUrl(window, '');
                    popDefault.SetWindowContentUrl(window, 'Popupcontrol/Ord_Edit_Pallet.aspx?or=' + s.GetRowKey(e.visibleIndex));
                    popDefault.ShowWindow(window);
                }
                else {
                    var window = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window);
                }

            }
        }
        
        function UpdateButtonText() {
            var text = grdOrder.IsCustomizationWindowVisible() ? "Hide" : "Show";
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
             if (!grdOrder.InCallback()) {
                 user = verify_user();

                 if (user != 'You are not logged in') {
                     hfMethod.Set("mode", s);
                     hfMethod.Set("hfparam", "today");
                     
                     grdOrder.PerformCallback(' ');
                     //09/03/2011 do not get update history here
                     //05/11/2010 no point in getting history - this search mode is not saved to it
                     //requestHistory();
                 }
                 else {

                     var window = popDefault.GetWindowByName('msgform');
                     popDefault.ShowWindow(window);
                 }
             }
         }

         function ProcessValueChanged(key, id, val1, val2, val3, val4) {

             var itemkey = "key" + key.toString();
             var itemvalue = id + ";" + val1 + ";" + val2 + ";" + val3 + ";" + val4;
               
             if (!hfeditor.Contains(itemkey)) {
                 hfeditor.Add(itemkey, itemvalue);
             }
             else {
                 hfeditor.Set(itemkey, itemvalue);
             }
         }

         //redirect
         function btnredirect_click(s, e) {

             if (!grdOrder.InCallback()) {

                 user = verify_user();
                 if (user != 'You are not logged in') {
                     self.location = "Ord_View_Cargo_Audit.aspx";
                 }
                 else {
                     var window = popDefault.GetWindowByName('msgform');
                     popDefault.ShowWindow(window);
                 }
             }
         }
         // ]]>
    </script>
    
        <div class="innertube"> <!-- just a container div --> 
                
                <!-- centered box for header -->
                <div class="formcenter"><img src="Images/icons/application_form_edit.png" 
                        title = "Shipment Tracking" alt="" 
                        class="h1image" /><h1>Cargo Updates</h1>
                </div> 
                
                   <div class="formcenter580note">
                       <div class="cell580_100">
                         <dx:ASPxComboBox ID="dxcboRange" runat="server" ClientInstanceName="cboRange" 
                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                         CssPostfix="Office2003Blue" 
                         SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                         ValueType="System.Int32" Width="225px">
                         <ButtonStyle Width="13px">
                         </ButtonStyle>
                         <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                         </LoadingPanelImage>
                         <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                onDateRangeChanged();
                                }" />
                     </dx:ASPxComboBox>
                     </div> 
            <div class="cell580_80"><strong>Quick search: </strong>Finds exact matches (or closest matches if you are logged in)</div>
            <div class="cell580_20">
                             <dx:ASPxButton ID="aspxbtnInfo" runat="server" 
                                        TabIndex="99" 
                                        ClientInstanceName="btninfo" EnableClientSideAPI="True" 
                                        AutoPostBack="False" CausesValidation="False" 
                                        ToolTip="Click here to begin your search" 
                                        EnableTheming="False" Width="70px" 
                                 Text="Help" 
                                 RightToLeft="False" ClientEnabled="False" EnableDefaultAppearance="False" 
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
                                
                                
                                
                                
                ToolTip="Enter the reference you are looking for e.g. the ISBN number or title">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                               <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' '\/\-]{1,100}$" 
                                                    ErrorText="Invalid value" />
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
                                        Height="23px" Width="23px" 
                                 Text="Search" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" UseSubmitBehavior="False">
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
    
     <div class="formcenter580">
        <div class="cell580_100"><strong>Other search options</strong></div>
        <div class="cell580_80">You can create more complex searches and update your cargo 
            in batches</div>
        <div class="cell580_20">
                             <dx:ASPxButton ID="aspxbtnInfo0" runat="server" 
                                        TabIndex="99" 
                                        ClientInstanceName="btnFilter" EnableClientSideAPI="True" 
                                        AutoPostBack="False" CausesValidation="False" 
                                        ToolTip="Click here to begin your search" 
                                        EnableTheming="False" Width="70px" 
                                 Text="Help" 
                                 RightToLeft="False" ClientEnabled="False" EnableDefaultAppearance="False" 
                                 UseSubmitBehavior="False">
                                       <Image Height="24px" Width="24px" AlternateText="Help" 
                                           Url="~/Images/icons/24x24/help.png">
                                       </Image>
                                       <ClientSideEvents Click="function(s, e) {
	btndemo_Click(1);
}" />
                              </dx:ASPxButton>
                        </div> 
        <div class="cell580_40">Limit search results by job status </div>
        <div class="cell580_40">
                                 <dx:ASPxComboBox ID="dxcboclosedyn" runat="server"
                                    ClientInstanceName="cboclosedyn" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                SelectedIndex="0" SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    ValueType="System.Boolean" Width="200px" TabIndex="4" 
                                         
                                         
                ToolTip="You can search in all your jobs, your active jobs or closed jobs " 
                                     onselectedindexchanged="dxcboclosedyn_SelectedIndexChanged" AutoPostBack="True" 
                                         >
                                    <Items>
                                        <dx:ListEditItem Text="Search active jobs" Value="False" />
                                        <dx:ListEditItem Text="Search closed jobs" Value="True" />
                                        <dx:ListEditItem Text="Search all jobs" />
                                    </Items>
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dx:ASPxComboBox>
                        </div>
        <div class="cell580_20"></div> 
        
        <div class="cell580_40">Limit search results by user </div>
        <div class="cell580_40">
                                    <dx:ASPxComboBox ID="cboName" runat="server" ClientInstanceName="aspxName" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" ValueType="System.String" 
                                            Width="200px" AutoPostBack="True"  
                                            onselectedindexchanged="cboName_SelectedIndexChanged" 
                                        
                                        
                                
                                        
                                        
                                        ToolTip="You can restrict your search to only include records for a particular user by selecting a name here (you must be logged in)" 
                                        TabIndex="5">
                                            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                            </LoadingPanelImage>
                                        </dx:ASPxComboBox>
                            </div>
        <div class="cell580_20"></div> 
        
        <div class="cell580_40"></div>
        <div class="cell580_40"></div>
        <div class="cell580_20"></div> 
        
        <div class="cell580_40">
                                           <dx:ASPxButton ID="dxbtnmore" runat="server" SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                                    Text="Advanced search" Width="150px" AutoPostBack="False" 
                                                    ClientInstanceName="btnmore" 
                Height="10px" TabIndex="8" CausesValidation="False" 
                                               
                                               
                                               ToolTip="Click here to use the advanced search builder (You must be logged in)" 
                                               UseSubmitBehavior="False" 
                                               EnableTheming="False" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue">
                                                   
                                                    <Image Height="24px" Width="24px" Url="~/Images/icons/24x24/filter_add.png">
                                                    </Image>
                                                   
                                                    <ClientSideEvents Click="btnmore_click" />
                                                 </dx:ASPxButton>    
                            </div>
        <div class="cell580_40">
		                          <dx:ASPxButton ID="btnReport" runat="server" 
                Text="today's updates" UseSubmitBehavior="False" 
                                
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="160px" 
                                      ToolTip="Click to see the cargo you have updated today" 
                                      AutoPostBack="False" CausesValidation="False" 
                EnableTheming="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" >
                                      <Image Width="24px" Height="24px" Url="~/Images/icons/24x24/date_link.png">
                                      </Image>
                                      <ClientSideEvents Click="function(s, e) {
	btnmyreport_Click(3);
}" />
                                  </dx:ASPxButton>
                         </div>
        <div class="cell580_20">
                             <dx:ASPxButton ID="aspxbtncargolog" runat="server" 
                                        TabIndex="99" 
                                        ClientInstanceName="btnCargoLogView" EnableClientSideAPI="True" 
                                        AutoPostBack="False" CausesValidation="False" 
                                        ToolTip="More information" 
                                        EnableTheming="False" 
                                 Text="History" 
                                 RightToLeft="False" Cursor="pointer" EnableDefaultAppearance="False" 
                                 UseSubmitBehavior="False">
                                       <Image Height="24px" Width="24px" AlternateText="History" 
                                           Url="~/Images/icons/24x24/calendar_link.png">
                                       </Image>
                                       <ClientSideEvents Click="btnredirect_click" />
                              </dx:ASPxButton>

                            </div> 
        
        <div class="formfooter"></div> 
    </div> 
    <!-- end centered search form -->
    
                <!-- centered box for search options -->
            <!-- end centered box -->
                
                <div class="bottom-separator"></div>
                 <div class="top-panel">
                      <dx:ASPxLabel ID="lblmsgboxdiv" runat="server" ClientInstanceName="lblmsgbox" 
                              Text="Search Results"></dx:ASPxLabel>
                 </div> 
                
                <!-- start grid options -->
                <div class="top-panel-shaded">
                     <div class="cell100">
                            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="Show detail" UseSubmitBehavior="False" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                      onclick="btnExpandAll_Click" Width="110px" Height="24px" 
                                ToolTip="Click to see more information about your shipments" 
                                EnableTheming="True" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue">
                                <Image Height="16px" Url="~/Images/icons/16x16/table_multiple.png" Width="16px">
                                </Image>
                            </dx:ASPxButton>
                     </div>  <!-- end expand all command -->
                     <div class="cell100">
                            <dx:ASPxButton ID="btnEndGroup" runat="server" Text="Ungroup" UseSubmitBehavior="False" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                            onclick="btnEndGroup_Click" Width="110px" Height="27px" 
                                ToolTip="Click to remove all column groupingas from your search results" 
                                EnableTheming="True" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
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
                                      ToolTip="Click to clear your current search results and start again" EnableTheming="True" 
                                      Height="24px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
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
                                          Height="24px" Width="150px" 
                              ToolTip="Click to pick the columns you want to see in your search results" 
                              EnableTheming="True" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                              CssPostfix="Office2003Blue">
                                        <Image Height="16px" Url="~/Images/icons/16x16/attributes_display.png" 
                                            Width="16px">
                                        </Image>
                                        <ClientSideEvents Click="btncols_Click" />
                                        </dx:ASPxButton>
                      </div> <!-- end field chooser command -->
                         <div class="cell100">
		                          <dx:ASPxButton ID="btnUpdate" runat="server" Text="Save changes" UseSubmitBehavior="False"
                                OnClick="btnUpdate_Click" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="120px" 
                                      ToolTip="Click to save all changes" EnableTheming="False" 
                                      Height="24px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                      CssPostfix="Office2003Blue" >
                                      <Image AlternateText="Save changes" Height="16px" Url="~/Images/icons/16x16/disk.png" 
                                          Width="16px">
                                      </Image>
                                  </dx:ASPxButton>
                     </div>  <!-- end update command --->
                </div> <!-- end grid options -->
            
            <!-- grid --->
            <!-- <div style="width: 100%; margin: 0 Auto; padding-bottom: 10px"> -->
                <dx:ASPxGridView ID="gridOrder" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                    CssPostfix="Office2003Blue" width="100%" KeyFieldName="OrderIx" 
                    ClientInstanceName="grdOrder" DataSourceID="LinqServerModeOrders" 
                     oncustomcallback="gridOrder_CustomCallback" 
                ondatabound="gridOrder_DataBound" 
                onhtmlrowcreated="gridOrder_HtmlRowCreated">
                    <SettingsBehavior AutoExpandAllGroups="True" ColumnResizeMode="Control" 
                        EnableRowHotTrack="True" />
                    <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                        CssPostfix="Office2003Blue">
                        <LoadingPanel ImageSpacing="10px">
                        </LoadingPanel>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="25">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
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
                    <SettingsText Title="Order Search" />
                    <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" 
                        CustomButtonClick="grid_CustomButtonClick" />
                    <Columns>
                         <dx:GridViewDataTextColumn Caption="orderid" FieldName="OrderID" 
                             Name="col_orderid" ReadOnly="True" ShowInCustomizationForm="False" 
                             UnboundType="Integer" Visible="False" VisibleIndex="-1" Width="0px">
                             <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                                     ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Index Key" FieldName="OrderIx" 
                             Name="col_orderix" ReadOnly="True" VisibleIndex="38"  
                             Width="0px" ShowInCustomizationForm="False">
                             <Settings ShowFilterRowMenu="False" AllowAutoFilter="False" 
                                 AllowAutoFilterTextInputTimer="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                          <dx:GridViewCommandColumn VisibleIndex="0"
                             Caption="Edit" ShowInCustomizationForm="False" ShowSelectCheckbox="True">
                             <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="customedit" Text="batch" Image-Height="27px" Image-Width="27px" Image-ToolTip="Tick to include in batch" Image-Url="~/Images/processing.jpg">
                            <Image ToolTip="Tick to include in batch" Height="27px" Width="27px" Url="~/Images/processing.jpg"></Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons> 
                         </dx:GridViewCommandColumn> 

		                <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="1" 
                            Caption="Order #" ReadOnly="True" Width="90px" Name="col_ordernumber">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="2" 
                            Caption="Title" ReadOnly="True" Width="250px" Name="col_title" 
                               ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" ShowInFilterControl="True" 
                                AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataDateColumn FieldName="CargoReady" VisibleIndex="3" 
                            Caption="Cargo Ready" Width="120px" Name="col_cargoready">
                            <PropertiesDateEdit DisplayFormatString="{0:d}">
                            </PropertiesDateEdit>
                            <DataItemTemplate>
                                <dx:ASPxDateEdit ID="dxdtcargoready" runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" OnInit="dxdtcargoready_Init" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Value='<%# Eval("CargoReady") %>' Width="110px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </dx:ASPxDateEdit>
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="EstPallets" VisibleIndex="4" 
                             Caption="Est. Pallets" Name="col_estpallets" 
                             Width="110px">
                            <PropertiesTextEdit>
                                <ValidationSettings CausesValidation="True" Display="Dynamic">
                                    <RegularExpression ErrorText="Please enter a number" 
                                        ValidationExpression="^[0-9]$" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="dxtxestpallets" runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" oninit="dxtxestpallets_Init" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text='<%# Eval("EstPallets") %>' Width="90px">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EstWeight" VisibleIndex="5" 
                            Caption="Est. Weight" Width="110px" Name="col_estweight">
                            <PropertiesTextEdit>
                                <ValidationSettings>
                                    <RegularExpression ErrorText="Please enter a number" 
                                        ValidationExpression="^[0-9]$" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="dxtxestweight" runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" oninit="dxtxestweight_Init" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text='<%# Eval("EstWeight") %>' Width="90px">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EstVolume" VisibleIndex="6" Caption="Est. Volume" 
                             Width="110px" Name="col_estvolume" >
                            <PropertiesTextEdit>
                                <ValidationSettings>
                                    <RegularExpression ErrorText="Please enter a number" 
                                        ValidationExpression="^[0-9]$" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="dxtxestvolume" runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" oninit="dxtxestvolume_Init" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text='<%# Eval("EstVolume") %>' Width="90px">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>

                         <dx:GridViewDataMemoColumn Caption="Remarks" ExportWidth="150" 
                               FieldName="RemarksToCustomer" ReadOnly="True" VisibleIndex="37" 
                               Width="150px" Name="col_remarks">
                           </dx:GridViewDataMemoColumn>
				<dx:GridViewDataTextColumn FieldName="HouseBLNUmber" VisibleIndex="7" 
                            Caption="House B/L" ReadOnly="True" Width="100px" Name="col_housebl" 
                                 ExportWidth="60">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CustomersRef" VisibleIndex="8" 
                            Caption="Customers Ref" ReadOnly="True" Width="110px" 
                             Name="col_customersref" ExportWidth="70">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="vessel_name" VisibleIndex="9" 
                            Caption="Vessel Name" ReadOnly="True" Width="120px" Name="col_vesselname" 
                                 ExportWidth="75">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="origin_port" VisibleIndex="10" 
                            Caption="Origin Port" ReadOnly="True" Width="100px" Name="col_origin" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETS" VisibleIndex="11" 
                            Caption="ETS" ReadOnly="True" Width="90px" Name="col_ets" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_port" VisibleIndex="12" 
                            Caption="Destination Port" ReadOnly="True" Width="120px" Name="col_dest" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETA" VisibleIndex="13" Caption="ETA" 
                            ReadOnly="True" Width="90px" Name="col_eta" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_place" VisibleIndex="14" 
                            Caption="Final Destination" Width="190px" Name="col_final" ExportWidth="90">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ContainerNumber" VisibleIndex="15" Caption="Container" 
                             Width="130px" ReadOnly="True" Name="col_container" ExportWidth="100" >
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ex-Works date" FieldName="ExWorksDate" ReadOnly="True" 
                            VisibleIndex="16" Width="110px" Name="col_exworks" ExportWidth="75">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ISBN" FieldName="ISBN" ReadOnly="True" 
                            VisibleIndex="17" Width="100px" Name="col_isbn" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
				 <dx:GridViewDataTextColumn Caption="Status" ExportWidth="80" 
                               FieldName="current_status" Name="col_status" ReadOnly="True" VisibleIndex="18" 
                               Width="80px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="On" ExportWidth="90" 
                               FieldName="CurrentStatusDate" Name="col_currentstatusdate" ReadOnly="True" 
                               VisibleIndex="19" Width="90px">
                               <PropertiesDateEdit DisplayFormatString="{0:d}">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                           <dx:GridViewDataDateColumn Caption="Last updated" FieldName="StatusDate" 
                               Name="col_last_updated" ReadOnly="True" VisibleIndex="20" Width="95px" 
                               ExportWidth="90">
                               <PropertiesDateEdit DisplayFormatString="{0:d}">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="21" 
                             Caption="Contact Name" Name="col_contact" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="22" 
                             Caption="Customer" Name="col_company" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="23" 
                             Caption="Order Controller" Name="col_ordercontroller" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Printer" FieldName="printer_name" 
                             ReadOnly="True" VisibleIndex="24" Width="150px" ExportWidth="90" 
                               Name="col_printer">
                             <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                         </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="ETW" FieldName="ETW" Name="col_etw" 
                             ReadOnly="True" VisibleIndex="25" Width="90px" ExportWidth="75">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                       
                         <dx:GridViewDataDateColumn Caption="Due Warehouse" FieldName="WarehouseDate" 
                             ReadOnly="True" VisibleIndex="26" Width="145px" Name="col_due_wh" 
                                 ExportWidth="120">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         
                         <dx:GridViewDataTextColumn Caption="Unit price per copy" FieldName="UnitPricePerCopy" 
                             ReadOnly="True" VisibleIndex="27" Width="150px" 
                                 ExportWidth="50" Name="col_unitppc">
                         </dx:GridViewDataTextColumn>
                         
                         <dx:GridViewDataTextColumn FieldName="consignee_name" VisibleIndex="28" 
                             Caption="Consignee" Name="col_consignee" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        
                           <dx:GridViewDataTextColumn Caption="Impression" FieldName="Impression" ReadOnly="True" 
                            VisibleIndex="29" Width="100px" Name="col_impression" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="Date order created" 
                               FieldName="DateOrderCreated" ReadOnly="True" 
                            VisibleIndex="30" Width="135px" Name="col_datecreated" ExportWidth="100">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery copies" ExportWidth="100" 
                               FieldName="Copies" Name="col_copies" ReadOnly="True" VisibleIndex="31" 
                               Width="110px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery to" ExportWidth="130" 
                               FieldName="delivery_to" Name="col_delivery_to" ReadOnly="True" 
                               VisibleIndex="32" Width="150px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery address" ExportWidth="130" 
                               FieldName="delivery_addr1" Name="col_addr1" ReadOnly="True" VisibleIndex="33" 
                               Width="150px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Post code" ExportWidth="90" 
                               FieldName="delivery_postcode" Name="col_delivery_postcode" ReadOnly="True" 
                               VisibleIndex="34" Width="90px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Tel." ExportWidth="120" 
                               FieldName="delivery_telno" Name="col_delivery_telno" ReadOnly="True" 
                               VisibleIndex="35" Width="120px">
                           </dx:GridViewDataTextColumn>
					 <dx:GridViewDataCheckColumn FieldName="JobClosed" VisibleIndex="36" 
                            Caption="Closed" ReadOnly="True" Width="70px" Name="col_closed" 
                                 ExportWidth="50">
                            <PropertiesCheckEdit DisplayTextChecked="Y" 
                                DisplayTextUnchecked="N" UseDisplayImages="False">
                            </PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                     </Columns>
                    <SettingsCookies Enabled="True" CookiesID="ord_cargo" />
                    <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" PopupEditFormWidth="600px" />

                    <Templates>
                         <DetailRow>
                         <div style="padding:3px 3px 2px 3px">
                                 <dx:ASPxPageControl runat="server" ID="pageControl" width="100%" 
                                     EnableCallBacks="True" ActiveTabIndex="0" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue"
                                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css"
                                     TabSpacing="3px">
                                     <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                                     </LoadingPanelImage>
                                     <ContentStyle>
                                         <Border BorderWidth="1px" BorderColor="#AECAF0" BorderStyle="Solid" />
                                     </ContentStyle>
                                 <TabPages>
                                     <dx:TabPage Text="Deliveries"  Visible="true">
                                         <ContentCollection>
                                             <dx:ContentControl ID="ContentControl1" runat="server">
                                                 <dx:ASPxGridView ID="detailDelivery" runat="server" AutoGenerateColumns="False" 
                                                     OnBeforePerformDataSelect="detailOrder_BeforePerformDataSelect"
                                                     Width="95%" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                     <Columns>
                                                         <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="0" Caption="Title" 
                                                          Width="150px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="Field1" VisibleIndex="1" Caption="Status" 
                                                          Width="100px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataDateColumn FieldName="CurrentStatusDate" VisibleIndex="2" Caption="On" 
                                                          Width="50px" ReadOnly="true" >
                                                         </dx:GridViewDataDateColumn> 
                                                         <dx:GridViewDataDateColumn FieldName="StatusDate" VisibleIndex="3" Caption="Last updated" 
                                                          Width="50px" ReadOnly="true" >
                                                         </dx:GridViewDataDateColumn>
                                                         <dx:GridViewDataTextColumn FieldName="Copies" VisibleIndex="4" Caption="Copies" 
                                                          Width="50px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="5" Caption="Company" 
                                                          Width="150px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="Address1" VisibleIndex="6" Caption="Address" 
                                                          Width="150px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="7" Caption="Post Code" 
                                                          Width="50px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="TelNo" VisibleIndex="8" Caption="Phone" 
                                                          Width="75px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                          <dx:GridViewDataTextColumn FieldName="SpecialInstructions" VisibleIndex="9" Caption="Special Instructions" 
                                                          Width="100px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                     </Columns>
                                                     <SettingsLoadingPanel ImagePosition="Top" />
                                                    
                                                     <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                         <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                         </LoadingPanelOnStatusBar>
                                                         <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                         </LoadingPanel>
                                                     </Images>
                                                     <ImagesEditors>
                                                         <DropDownEditDropDown>
                                                             <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                                                                 PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                         </DropDownEditDropDown>
                                                         <SpinEditIncrement>
                                                             <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua" 
                                                                 PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                         </SpinEditIncrement>
                                                         <SpinEditDecrement>
                                                             <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua" 
                                                                 PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                         </SpinEditDecrement>
                                                         <SpinEditLargeIncrement>
                                                             <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua" 
                                                                 PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                         </SpinEditLargeIncrement>
                                                         <SpinEditLargeDecrement>
                                                             <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua" 
                                                                 PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                         </SpinEditLargeDecrement>
                                                     </ImagesEditors>
                                                     <ImagesFilterControl>
                                                         <LoadingPanel Url="~/App_Themes/Aqua/Editors/Loading.gif">
                                                         </LoadingPanel>
                                                     </ImagesFilterControl>
                                                     <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                         <LoadingPanel ImageSpacing="8px">
                                                         </LoadingPanel>
                                                     </Styles>
                                                     <StylesEditors>
                                                         <CalendarHeader Spacing="1px">
                                                         </CalendarHeader>
                                                         <ProgressBar Height="25px">
                                                         </ProgressBar>
                                                     </StylesEditors>
                                                 </dx:ASPxGridView>
                                             </dx:ContentControl>
                                         </ContentCollection>
                                     </dx:TabPage>
                                     <dx:TabPage Text="Additional" Visible="true">
                                         <ContentCollection>
                                             <dx:ContentControl ID="ContentControl2" runat="server">
                                             </dx:ContentControl>
                                         </ContentCollection>
                                     </dx:TabPage>
                                     <dx:TabPage Text="Invoicing"  Visible="true">
                                         <ContentCollection>
                                             <dx:ContentControl ID="ContentControl3" runat="server">
                                                  <dx:ASPxGridView ID="detailInvoice" runat="server" AutoGenerateColumns="False" 
                                                     OnBeforePerformDataSelect="detailInvoice_BeforePerformDataSelect" 
                                                      Width="95%" 
                                                      CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css"  CssPostfix="Office2003Blue">
                                                     <Columns>
                                                         <dx:GridViewDataTextColumn FieldName="InvoiceNumber" VisibleIndex="0" Caption="Invoice Number" 
                                                          Width="75px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataDateColumn FieldName="Department" VisibleIndex="1" Caption="Department" 
                                                          Width="150px" ReadOnly="true" >
                                                         </dx:GridViewDataDateColumn>
                                                         <dx:GridViewDataDateColumn FieldName="CompanyName" VisibleIndex="2" Caption="Invoice to" 
                                                          Width="150px" ReadOnly="true" >
                                                         </dx:GridViewDataDateColumn> 
                                                         <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Caption="Description" 
                                                          Width="200px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Contact" VisibleIndex="4" Caption="Contact" 
                                                          Width="100px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                         <dx:GridViewDataTextColumn FieldName="PaymentDueDate" VisibleIndex="5" Caption="Payment Due" 
                                                          Width="50px" ReadOnly="true" >
                                                         </dx:GridViewDataTextColumn>
                                                     </Columns>
                                                      <SettingsLoadingPanel ImagePosition="Top" />
                                                     
                                                      <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                          <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                          </LoadingPanelOnStatusBar>
                                                          <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                          </LoadingPanel>
                                                      </Images>
                                                      <ImagesEditors>
                                                          <DropDownEditDropDown>
                                                              <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                                                                  PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                          </DropDownEditDropDown>
                                                          <SpinEditIncrement>
                                                              <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua" 
                                                                  PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                                          </SpinEditIncrement>
                                                          <SpinEditDecrement>
                                                              <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua" 
                                                                  PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                                          </SpinEditDecrement>
                                                          <SpinEditLargeIncrement>
                                                              <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua" 
                                                                  PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                                          </SpinEditLargeIncrement>
                                                          <SpinEditLargeDecrement>
                                                              <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua" 
                                                                  PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                                          </SpinEditLargeDecrement>
                                                      </ImagesEditors>
                                                      <ImagesFilterControl>
                                                          <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                                          </LoadingPanel>
                                                      </ImagesFilterControl>
                                                      <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                          <LoadingPanel ImageSpacing="8px">
                                                          </LoadingPanel>
                                                      </Styles>
                                                      <StylesEditors>
                                                          <CalendarHeader Spacing="1px">
                                                          </CalendarHeader>
                                                          <ProgressBar Height="25px">
                                                          </ProgressBar>
                                                      </StylesEditors>
                                                 </dx:ASPxGridView>
                                             </dx:ContentControl>
                                         </ContentCollection>
                                     </dx:TabPage>
                                 </TabPages>
                                     <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                             </dx:ASPxPageControl>
                         </div>
                    </DetailRow>	
                </Templates>
             <SettingsDetail ShowDetailRow="true" ExportMode="Expanded"/>
             <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Visible" 
                        ShowFilterRow="True" ShowHorizontalScrollBar="True" 
                        ShowGroupedColumns="True" VerticalScrollableHeight="400"/>
             <SettingsCustomizationWindow Enabled="True" PopupVerticalAlign="Above"  />
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Auto" />
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
         <!-- </div> --> <!-- end grid wrapper -->       
    </div> <!-- end content div -->
    
    <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
                          
    <dx:LinqServerModeDataSource ID="LinqServerModeOrders" runat="server" 
        ContextTypeName="linq.linq_view_orders_udfDataContext" 
        TableName="view_orders_by_age" />
        
     <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FetchByActiveAnonymous" 
                                TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
                            
     <dx:ASPxGridViewExporter ID="orderGridExport" runat="server" 
        GridViewID="gridOrder" ReportHeader="Exported search results">
    </dx:ASPxGridViewExporter>

        <dx:ASPxHiddenField ID="dxhfeditor" runat="server" 
        ClientInstanceName="hfeditor">
    </dx:ASPxHiddenField>

        <dx:ASPxPopupControl ID="popDefault" runat="server" AppearAfter="800" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="popDefault" 
        CloseAction="CloseButton" HeaderText="" Height="100px" Width="100px" 
        PopupAction="None" EnableHotTrack="False" AllowDragging="True" 
        EnableHierarchyRecreation="True">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
            <HeaderStyle>
            <Paddings PaddingRight="6px" />
            </HeaderStyle>
        <Windows>
             <dx:PopupWindow CloseAction="CloseButton" 
                ContentUrl="~/Popupcontrol/Ord_Filter_Advanced.aspx" HeaderText="Advanced search"
                Height="500px" Modal="False" Name="filterform" PopupAction="None" 
                Width="640px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
             <dx:PopupWindow HeaderText="You are not logged in" Name="msgform" 
                 Height="150px" Width="250px" Modal="true" 
                 PopupElementID="dxbtnmore" ShowCloseButton="True" ShowFooter="False"
                 ShowHeader="True">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                         <asp:Panel ID="pnlmsg" runat="server">
                            <div>
                            <div class="alert"> 
                            <asp:Label ID="lblmsg" runat="server" Text="You need to log in to use this option"></asp:Label>
                            </div>
                            <div style="width:100%; height:60px; padding: 10px"> 
                                 <div style="float: left; width: 25%"></div> 
                                 <div style="float: right; width: 75%">
                                    <dx:ASPxButton ID="dxbtmmsg" runat="server"  
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Ok" 
                                        Width="100px" CausesValidation="False" 
                                        AutoPostBack="False">
                                        <ClientSideEvents 
                                            Click="function(s, e) {
	                                        window.popDefault.HideWindow(window.popDefault.GetWindowByName('msgform'));}" />
                                     </dx:ASPxButton>
                                   </div> 
                                   </div>
                            </div> 
                         </asp:Panel> 
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
             <dx:PopupWindow ContentUrl="~/Popupcontrol/Ord_Edit_Pallet.aspx" 
                 Name="editpalletform" CloseAction="CloseButton" 
                 HeaderText="Update pallet information" Height="500px" Modal="True" 
                 Width="540px">
                 <ContentCollection>
                     <dx:PopupControlContentControl runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
        </Windows>
     </dx:ASPxPopupControl>

</asp:Content> 


