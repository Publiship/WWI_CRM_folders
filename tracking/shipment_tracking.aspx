<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shipment_tracking.aspx.cs" MasterPageFile="~/WWI_m1.master" Inherits="shipment_tracking" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSplitter" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>

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
    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTitleIndex" tagprefix="dx" %>
     
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTimer" tagprefix="dx" %>


<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" runat="server">


<script type="text/javascript">
    // <![CDATA[
    function onTabClick(e) {
        //if tab = advanced search show popup otherrwise ignore
        //operatyed same was as btnMore_click
        if (e.tab.name == "tabOtherFilters" || e.tab.name == "tabReporting") {
            if (!grdOrder.InCallback()) {

                user = verify_user();

                if (user == 'You are not signed in') {
                    pageFilters.SetActiveTab(pageFilters.GetTab(0));
                    var window2 = popDefault.GetWindowByName('msgform');
                    popDefault.ShowWindow(window2);
                }
            } //end if in callback
        } //end if  tab
    }
   
    function onGridClientSideClick(s, e) {
        if (e.item.name == "mnuFields") {
            //show hide customisation window
            if (grdOrder.IsCustomizationWindowVisible())
            { grdOrder.HideCustomizationWindow(); }
            else
            { grdOrder.ShowCustomizationWindow(); }
            mnuGrid1.GetItemByName(e.item.name).SetText(UpdateMenuText());
        }
    }
    
    function UpdateMenuText() {
        var text = grdOrder.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " field chooser";
        return text;
    }
    //****
    
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
                //09/03/2011 history removed from this page
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

    function submit_company_id(s, e) {
            if (!grdOrder.InCallback()) {
                hfMethod.Set("mode", 2);  //set mode to search so company filter can be applied
                grdOrder.PerformCallback(' ');
            }
    }
        
    function btnmore_click(s, e) {

            if (!grdOrder.InCallback()) {

                user = verify_user();
                //src=tracking
                if (user != 'You are not signed in') {
                    var window1 = popDefault.GetWindowByName('filterform');
                    popDefault.SetWindowContentUrl(window1, '');
                    popDefault.SetWindowContentUrl(window1, '../Popupcontrol/order_tracking_filter.aspx?src=shipment');
                    
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
            //var lockcolumns = btnColfix.GetText();

            ////if button text is lock columns the grid is unlocked so we can use customisation window 
            //if (lockcolumns == "Lock columns") {
                if (grdOrder.IsCustomizationWindowVisible())
                    grdOrder.HideCustomizationWindow();
                else
                    grdOrder.ShowCustomizationWindow();
                UpdateButtonText();
            //}
            //else {
            //    alert("Field chooser is locked, you must click 'Unlock columns' before you can use it"); 
            //}
    }
        
    function grid_CustomizationWindowCloseUp(s, e) {
                 UpdateButtonText();
    }
        
    function UpdateButtonText() {
            var text = grdOrder.IsCustomizationWindowVisible() ? "Hide" : "Show";
                 text += " field chooser";
                 //btnCols.SetText(text); no longer defined
    }

    function textboxKeyDown() {
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

                 if (user != 'You are not signed in') {
                     hfMethod.Set("mode", s);
                     grdOrder.PerformCallback(' ');
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

             if (!grdOrder.InCallback()) {

                 user = verify_user();
                 if (user != 'You are not signed in') {
                     self.location = "shipment_search_history.aspx"; 
                 }
                 else {
                     var window = popDefault.GetWindowByName('msgform');
                     popDefault.ShowWindow(window);
                 }
             }
    }
    
    //new function when file upload button is clicked so we can avoid making a postback
    //call web method so we can open new window
    //aspxridview.settingsbehaviour.allowfocusedrow=true MUST be set or can't get row index
    function grid_CustomButtonClick(s, e) {
             e.processOnServer = false;
             
             var user = verify_user();
             if (user != 'You are not signed in') {
                 if (e.buttonID == 'cmdFiles') {
                     //var window = popDefault.GetWindowByName('sysna');
                     //popDefault.ShowWindow(window);
                     //just use order number
                     s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber;document_folder;HouseBLNUmber', onGotUploadValues);
                 }
             }
             else {
                 //var window = popDefault.GetWindowByName('sysna');
                 //popDefault.ShowWindow(window);
                 var window = popDefault.GetWindowByName('msgform');
                 popDefault.ShowWindow(window);
             }
    }

    function ongridCommandFiles(result) {
             //alert(result);  
             var window = popDefault.GetWindowByName('podfiles');
             popDefault.SetWindowContentUrl(window, '');
             //popDefault.SetWindowContentUrl(window, 'Popupcontrol/Pod_File_Manager.aspx?or=' + s.GetRowKey(e.visibleIndex)); 
             popDefault.SetWindowContentUrl(window, '../Popupcontrol/order_file_manager.aspx?or=' + result.toString());
             popDefault.ShowWindow(window);
    }
   
    
    function onViewDocuments(result) {
             var user = verify_user();
             if (user != 'You are not signed in') {
                 var window1 = popDefault.GetWindowByName('podfiles');
                 popDefault.SetWindowContentUrl(window1, '');
                 popDefault.SetWindowContentUrl(window1, '../Popupcontrol/order_file_manager.aspx?or=' + result.toString());
                 popDefault.ShowWindow(window1);
             }
             else {
                 //var window = popDefault.GetWindowByName('sysna');
                 //popDefault.ShowWindow(window);
                 var window2 = popDefault.GetWindowByName('msgform');
                 popDefault.ShowWindow(window2);
             }
    }

    //using webmethod in code behind
    function onGotUploadValues(values) {
        //alert(values[0]);
        //alert(values[1]);
        //can pass values as iList<string> or concatenated using values.toString() method
        PageMethods.get_secure_url(values, 'cmdFiles', onMethodComplete);
    }

    function onMethodComplete(result, userContext, methodName) {
        if (result != "") {
            if (result != "denied") {
                //open window as pop up
                var window1 = ppUploadManager.GetWindowByName('ppUpload');
                ppUploadManager.SetWindowContentUrl(window1, '');
                ppUploadManager.SetWindowContentUrl(window1, result.toString());
                ppUploadManager.ShowWindow(window1);
                //opens form in new window
                //window.open(result, "_blank");
            }
            else {
                alert('You do not have access to this option');
            }
        }
        else {
            alert('PageMethods.get_secure_url() returned null');
        }
    }

    //fired when upload manager popuis closed
    function onUploadCloseUp(s, e) {
        window.ppUploadManager.HideWindow(window.ppUploadManager.GetWindowByName('ppUpload'));
        if (!grdOrder.InCallback()) {
            grdOrder.PerformCallback(' ');
        }
    }
    // ]]>
    </script>

    
        <div class="innertube">  <!-- just a container div --> 
            <div class="grid_wrap">
                <!-- panel conainer for all filters options -->
                <dx:ASPxRoundPanel ID="dxpnlAllFilters" ClientInstanceName="pnlAllFilters" 
                    runat="server" Width="100%" 
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" EnableDefaultAppearance="False" Font-Bold="False" 
                    Font-Names="Arial,Helvetica,sans-serif;" Font-Size="X-Large" 
                    GroupBoxCaptionOffsetX="6px" GroupBoxCaptionOffsetY="-19px" 
                    HeaderText="Shipment tracking" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    <ContentPaddings PaddingBottom="10px" PaddingLeft="9px" PaddingRight="11px" 
                        PaddingTop="10px" />
                    <HeaderStyle>
                    <Paddings PaddingBottom="6px" PaddingLeft="9px" PaddingRight="11px" 
                        PaddingTop="3px" />
                    </HeaderStyle>
                    <PanelCollection>
                        <dx:PanelContent>
                        <!-- combo for age range in months -->
                        <div class="row pad_bottom">
                            <span class="left250">
                            <dx:ASPxComboBox ID="dxcboMonths" runat="server" ClientInstanceName="cboMonths" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                ValueType="System.Int32" Width="200px" Spacing="0">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                onDateRangeChanged();
                                }" />
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                </LoadingPanelImage>
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dx:ASPxComboBox>
                            </span>
                            <!-- error message label -->
                            <span class="left">
                                <dx:ASPxLabel ID="lblmsgboxdiv" runat="server" ClientInstanceName="lblmsgbox"></dx:ASPxLabel>
                            </span>
                        </div>
                     <!-- tab pages for filter controls -->
                     <div class="pad_bottom">
                     <dx:ASPxPageControl ID="dxpageFilters" ClientInstanceName="pageFilters" 
                        runat="server" ActiveTabIndex="0" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TabSpacing="0px" 
                        Width="100%" Font-Names="Arial,Helvetica,sans-serif;" Font-Size="Medium" 
                             RenderMode="Lightweight">
                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                        </LoadingPanelImage>
                        <ContentStyle>
                            <Paddings Padding="12px" />
                            <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <ClientSideEvents TabClick="function(s, e) { onTabClick(e); }" />
                        <TabPages>
                            <dx:TabPage Name="tabQuickFilter" Text="Quick search" 
                                ToolTip="Track using simple criteria">
                                <ContentCollection>
                                    <dx:ContentControl>
                                            <!-- container div -->
                                            <div style="overflow:hidden; width: 100%">
                                                <!-- 1st row description -->
                                               <div class="row">
                                                    <dx:ASPxLabel ID="dxlblQuickSearch2" runat="server" 
                                                        ClientInstanceName="lblQuickSearch2" 
                                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" 
                                                        Text="Finds exact matches or closest matches if you are signed in">
                                                    </dx:ASPxLabel>
                                                </div>
                                                <!-- 2nd row quick input -->
                                                <div class="row minheight35">
                                                    <!-- search in label -->
                                                    <span class="left80 pad">
                                                        <dx:ASPxLabel ID="dxlblBasic1" runat="server" ClientInstanceName="lblBasic1" 
                                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                            CssPostfix="Office2010Blue" Text="Search in">
                                                        </dx:ASPxLabel>
                                                    </span>
                                                    <!-- fields dll -->
                                                    <span class="left195 pad">
                                                        <dx:ASPxComboBox ID="dxcbofields" runat="server" ClientInstanceName="cbofields" 
                                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" DataSourceID="ObjectDataSourceFields" 
                                                        DropDownRows="10" Spacing="0" 
                                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TabIndex="1" 
                                                        ToolTip="Pick what you want to search for here e.g. ISBN number" 
                                                        Width="180px">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Search in" FieldName="FilterCaption" 
                                                                Name="fieldcaption" />
                                                            <dx:ListBoxColumn FieldName="FieldName" Name="fieldname" Visible="False" />
                                                            <dx:ListBoxColumn FieldName="ColumnType" Name="columntype" Visible="False" />
                                                        </Columns>
                                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                        </LoadingPanelImage>
                                                        <LoadingPanelStyle ImageSpacing="5px">
                                                        </LoadingPanelStyle>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                        </dx:ASPxComboBox>
                                                    </span>
                                                    <!-- search for label -->
                                                    <span class="left35 pad">
                                                          <dx:ASPxLabel ID="dxlblBasic2" runat="server" ClientInstanceName="lblBasic1" 
                                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                            CssPostfix="Office2010Blue" Text="for">
                                                        </dx:ASPxLabel>
                                                    </span>
                                                    <!-- input text box -->
                                                    <span class="left195 pad">
                                                         <dx:ASPxTextBox ID="txtQuickSearch" runat="server" 
                                                                    ClientInstanceName="txtQuick" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                    CssPostfix="Office2010Blue" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                    Width="180px" TabIndex="2" 
                                                                          NullText="Your reference here" EnableClientSideAPI="True" 
                                                                   ToolTip="Enter the reference you are looking for e.g. the ISBN number or title" 
                                                                    EnableViewState="False">
                                                                    <ValidationSettings>
                                                                        <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' '\/\-]{1,100}$" 
                                                                            ErrorText="Invalid value" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents KeyDown="function(s, e) {textboxKeyDown}" />
                                                                    </dx:ASPxTextBox> 
                                                    </span>
                                                    <!-- search button -->
                                                    <span class="left120 pad">
                                                        <dx:ASPxButton ID="dxbtnFilter" runat="server" AutoPostBack="False" 
                                                            CausesValidation="False" ClientInstanceName="btnFilter" 
                                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                            CssPostfix="Office2010Blue" EnableClientSideAPI="True" EnableTheming="False" 
                                                            HorizontalAlign="Left" RightToLeft="False" 
                                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TabIndex="3" 
                                                            Text="Search" ToolTip="Begin search" UseSubmitBehavior="False" 
                                                            VerticalAlign="Middle">
                                                            <ClientSideEvents Click="function(s, e) {
	                                                                            //window.lblmsgbox.SetText(txtQuick.GetText());
	                                                                            submit_query(1);
                                                                            }" />
                                                            <Image AlternateText="Begin search" Height="32px" Width="32px">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </span>
                                                    
                                                    <!-- advanced search button -->
                                                    <span class="left pad">
                                                      <dx:ASPxButton ID="dxbtnMore" runat="server" 
                                                                TabIndex="4" 
                                                                ClientInstanceName="btnMore" EnableClientSideAPI="True" 
                                                                AutoPostBack="False" CausesValidation="False" 
                                                                ToolTip="Advanced search" 
                                                                EnableTheming="False" 
                                                                RightToLeft="False" 
                                                                UseSubmitBehavior="False" 
                                                                ClientVisible="True" 
                                                        Text="Advanced search" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                        CssPostfix="Office2010Blue" 
                                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                                                    <ClientSideEvents Click="btnmore_click" />
                                                                                      </dx:ASPxButton>
                                                    </span>
                                                    <!-- help button -->
                                                    <span class="left pad">
                                                        <dx:ASPxButton ID="aspxbtnInfo" runat="server" 
                                                                                                TabIndex="5" 
                                                                                                ClientInstanceName="btninfo" EnableClientSideAPI="True" 
                                                                                                AutoPostBack="False" CausesValidation="False" 
                                                                                                ToolTip="More information" 
                                                                                                EnableTheming="False" 
                                                                                         RightToLeft="False" Cursor="pointer" EnableDefaultAppearance="False" 
                                                                                         UseSubmitBehavior="False" Width="35px">
                                                                                               <Image Height="24px" Width="24px" AlternateText="Help" 
                                                                                                   Url="~/Images/icons/24x24/help.png">
                                                                                               </Image>
                                                                                               <ClientSideEvents Click="function(s, e) {
	                                                                                        btndemo_Click(1);
                                                                                        }" />
                                                                                      </dx:ASPxButton>
                                                    </span>
                                                </div> 
                                            </div>
                                   </dx:ContentControl> 
                                </ContentCollection> 
                            </dx:TabPage> 
                            <dx:TabPage Name="tabOtherFilters" Text="Other search filters" 
                                ToolTip="Additional filters for job status, user and cpmpany">
                                <ContentCollection>
                                    <dx:ContentControl>
                                         <div style="overflow:hidden; width: 100%;">
                                             <!-- 1st row description -->
                                             <div class="row">
                                                  <dx:ASPxLabel ID="dxlblOtherFilters2" ClientInstanceName="lblOtherFilters2" 
                                                          runat="server" 
                                                          Text="Filter search results by status, user name or company" 
                                                          CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                          CssPostfix="Office2010Blue">
                                                      </dx:ASPxLabel>
                                             </div> 
                                             <!-- 2nd row filter dlls -->
                                             <div class="row minheight45">
                                                <!-- label for job status filter -->
                                                <span class="left pad">
                                                        <dx:ASPxLabel ID="dxlblMoreFilters1" runat="server" 
                                                                ClientInstanceName="lblMoreFilters1" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" Text="Job status">
                                                            </dx:ASPxLabel>
                                                </span> 
                                                <!-- job status dll -->
                                                <span class="left195 pad">
                                                      <dx:ASPxComboBox ID="dxcboclosedyn" runat="server" AutoPostBack="True" 
                                                                ClientInstanceName="cboclosedyn" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" 
                                                                OnSelectedIndexChanged="dxcboclosedyn_SelectedIndexChanged" SelectedIndex="2" 
                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TabIndex="6" 
                                                                ToolTip="Limit search results by job status" ValueType="System.Boolean" 
                                                                Width="150px" Spacing="0">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Search active jobs" Value="False" />
                                                                    <dx:ListEditItem Text="Search closed jobs" Value="True" />
                                                                    <dx:ListEditItem Selected="True" Text="Search all jobs" />
                                                                </Items>
                                                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                                </LoadingPanelImage>
                                                                <LoadingPanelStyle ImageSpacing="5px">
                                                                </LoadingPanelStyle>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </dx:ASPxComboBox>
                                                </span>
                                                <!-- label for user filter -->
                                                <span class="left pad">
                                                      <dx:ASPxLabel ID="dxlblMoreFilters2" runat="server" 
                                                                ClientInstanceName="lblMoreFilters2" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" Text="User name">
                                                            </dx:ASPxLabel>
                                                </span>
                                                <!-- user filter dll -->
                                                <span class="left250 pad">
                                                   <dx:ASPxComboBox ID="cboName" runat="server" AutoPostBack="True" 
                                                                ClientInstanceName="aspxName" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" 
                                                                OnSelectedIndexChanged="cboName_SelectedIndexChanged" 
                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TabIndex="7" 
                                                                ToolTip="Limit search results by user (you must be signed in)" 
                                                                Width="220px" Spacing="0">
                                                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                                </LoadingPanelImage>
                                                                <LoadingPanelStyle ImageSpacing="5px">
                                                                </LoadingPanelStyle>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                                <Buttons>
                                                                    <dx:EditButton Text="Clear">
                                                                    </dx:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
	                                                                    if(e.buttonIndex == 0){
                                                                                s.SetText('(All users)');
			                                                                    s.SetSelectedIndex(-1);
                                                                    	
			                                                                    if (!grdOrder.InCallback()) {
                                                                                    grdOrder.PerformCallback(' ');
                                                                                }
	                                                                    }
                                                                    }" />
                                                            </dx:ASPxComboBox>
                                                </span>
                                                <!-- label for company filter -->
                                                <span class="left pad">
                                                    <dx:ASPxLabel ID="dxlblMoreFilters3" runat="server" 
                                                                ClientInstanceName="lblMoreFilters3" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" Text="Company name">
                                                            </dx:ASPxLabel>
                                                </span>
                                                <!-- company filter -->
                                                <span class="left250 pad">
                                                             <dx:ASPxComboBox ID="dxcbocompany" runat="server" CallbackPageSize="20" 
                                                                ClientInstanceName="cbocompany" 
                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                CssPostfix="Office2010Blue" DropDownRows="10" DropDownWidth="300px" 
                                                                EnableCallbackMode="True" Enabled="False" EnableSynchronization="False" 
                                                                IncrementalFilteringMode="Contains" 
                                                                OnItemRequestedByValue="dxcbocompany_ItemRequestedByValue" 
                                                                OnItemsRequestedByFilterCondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                TextField="CompanyName" ToolTip="You only need to type part of a company name" 
                                                                ValueField="CompanyID" Width="250px" Spacing="0" TabIndex="8">
                                                                <ClientSideEvents ButtonClick="function(s, e) {
	                                                                        if(e.buttonIndex == 0){
                                                                                    s.SetText('');
			                                                                        s.SetSelectedIndex(-1);
                                                                        	
			                                                                        if (!grdOrder.InCallback()) {
                                                                                        grdOrder.PerformCallback(' ');
                                                                                    }
	                                                                        }
                                                                        }" SelectedIndexChanged="function(s, e) {
	                                                                        submit_company_id(s, e);	
                                                                        }" />
                                                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                                </LoadingPanelImage>
                                                                <LoadingPanelStyle ImageSpacing="5px">
                                                                </LoadingPanelStyle>
                                                                <Buttons>
                                                                    <dx:EditButton Text="Clear">
                                                                    </dx:EditButton>
                                                                </Buttons>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RegularExpression ErrorText="invalid text" 
                                                                        ValidationExpression="^[\d_0-9a-zA-Z' '\/]{1,100}$" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                </span>
                                                <!-- help button -->
                                                <span class="left pad">
                                                  <dx:ASPxButton ID="aspxbtnInfo0" runat="server" AutoPostBack="False" 
                                                                CausesValidation="False" ClientInstanceName="btnFilter" Cursor="pointer" 
                                                                EnableClientSideAPI="True" EnableDefaultAppearance="False" 
                                                                EnableTheming="False" RightToLeft="False" TabIndex="9" 
                                                                ToolTip="More information" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	                                                                btndemo_Click(1);
                                                                }" />
                                                                <Image AlternateText="Begin search" Height="24px" 
                                                                    Url="~/Images/icons/24x24/help.png" Width="24px">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                </span>
                                             </div> <!-- end 2nd row -->
                                       </div>
                                            <!-- end container div for additional filters -->    
                                    </dx:ContentControl> 
                                </ContentCollection> 
                            </dx:TabPage> 
                            <dx:TabPage Name="tabReporting" Text="Reporting">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <!-- container div for reporting -->
                                        <div style="overflow:hidden; width: 100%;">
                                            <!-- 1st row description -->
                                             <div class="row">
                                                  <dx:ASPxLabel ID="dxlblReportingFilters" ClientInstanceName="lblReportingFilters" 
                                                          runat="server" 
                                                          Text="One-click reporting options" 
                                                          CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                          CssPostfix="Office2010Blue">
                                                      </dx:ASPxLabel>
                                             </div> 
                                             <!-- 2nd row reporting commands -->
                                            <div class="row minheight45">
                                                <!-- status report -->
                                                <span class="left195 pad">
                                                   <dx:ASPxButton ID="btnReport" runat="server" Text="Status report" UseSubmitBehavior="False" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="150px" 
                                                          ToolTip="Click to see all your active jobs" 
                                                          AutoPostBack="False" CausesValidation="False" EnableTheming="False" 
                                                          CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                          CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                                    VerticalAlign="Middle" TabIndex="10" >
                                                          <Image Url="~/Images/icons/24x24/lorry_go.png">
                                                          </Image>
                                                      <ClientSideEvents Click="function(s, e) {
	                                                        btnmyreport_Click(3);
                                                        }" />
                                                    </dx:ASPxButton>
                                                </span>
                                                <!-- search history -->
                                                <span class="left195 pad">
                                                     <dx:ASPxButton ID="aspxbtnlog" runat="server" 
                                                            TabIndex="11" 
                                                            ClientInstanceName="btnLogView" EnableClientSideAPI="True" 
                                                            AutoPostBack="False" CausesValidation="False" 
                                                            ToolTip="More information" 
                                                            EnableTheming="False" 
                                                     Text="History" 
                                                     RightToLeft="False" 
                                                     UseSubmitBehavior="False" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                    Width="150px">
                                                           <Image Height="24px" Width="24px" AlternateText="History" 
                                                               Url="~/Images/icons/24x24/calendar_link.png">
                                                           </Image>
                                                           <ClientSideEvents Click="btnredirect_click" />
                                                  </dx:ASPxButton>
                                                </span> 
                                            </div>
                                        </div> 
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                        <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                        <LoadingPanelStyle ImageSpacing="5px">
                        </LoadingPanelStyle>
                    </dx:ASPxPageControl>
                    </div> 
                    <!-- end tab pages -->
                    <!-- end search options --> 
                        </dx:PanelContent> 
                    </PanelCollection> 
                    <HeaderImage Height="50px" Width="50px">
                    </HeaderImage>
                </dx:ASPxRoundPanel>
               <!-- end panel container -->
            </div> 
            <!-- end grid wrap for panels -->
            <!-- grid wrap for toolbars and grid -->
            <div>
               <div class="row">
                         <!-- 1st menu grid commands that can run client side do not hide overflow on this one as we have a drop down! -->
                         <!-- do not change overflow here or use css divs it stops drop down from working -->
                        <div style="float: left">
                         <dx:ASPxMenu ID="dxmnuGrid1"  ClientInstanceName="mnuGrid1" runat="server" AutoSeparators="RootOnly" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                RenderMode="Lightweight" ShowAsToolbar="True">
                             <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                             <LoadingPanelStyle ImageSpacing="5px">
                             </LoadingPanelStyle>
                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                             </LoadingPanelImage>
                             <Items>
                                     <dx:MenuItem Name="mnuFields" Text="Show field chooser">
                                        <Image Height="16px" Url="~/Images/icons/16x16/attributes_display.png" 
                                            Width="16px">
                                        </Image>
                                    </dx:MenuItem>
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
                             </Items>
                             <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                             <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
                             <ClientSideEvents ItemClick="onGridClientSideClick" />
                         </dx:ASPxMenu>
                       </div> <!-- end 1st menu -->
                       <!-- 2nd menu posts back -->
                       <div>
                        <dx:ASPxMenu ID="dxmnuGrid2" ClientInstanceName="mnuGrid2" runat="server" 
                                AutoSeparators="RootOnly" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                onitemclick="dxmnuGrid2_ItemClick" AllowSelectItem="True" 
                               RenderMode="Lightweight" ShowAsToolbar="True">
                                <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                                <Items>
                                    <dx:MenuItem Name="mnuExport" Text="Export">
                                        <Image Height="16px" Url="~/Images/icons/16x16/download.png" Width="16px">
                                        </Image>
                                    </dx:MenuItem>
                                    <dx:MenuItem Name="mnuUngroup" Text="Ungroup">
                                        <Image Height="16px" Url="~/Images/icons/16x16/shape_ungroup.png" Width="16px">
                                        </Image>
                                    </dx:MenuItem>
                                    <dx:MenuItem Name="mnuClear" Text="Clear search">
                                        <Image Height="16px" Url="~/Images/icons/16x16/arrow_refresh.png" Width="16px">
                                        </Image>
                                    </dx:MenuItem>
                                    <dx:MenuItem ClientEnabled="False" ClientVisible="False" Enabled="False" 
                                        Name="mnuDetail" Text="Show detail" Visible="False">
                                    </dx:MenuItem>
                                    <dx:MenuItem ClientEnabled="False" ClientVisible="False" Enabled="False" 
                                        Name="mnuLock" Text="Lock columns" Visible="False">
                                    </dx:MenuItem>
                                </Items>
                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
                            </dx:ASPxMenu>
                       </div>
                       <!-- end 2nd menu -->
                </div>
            </div>
            <!-- end row div -->
            <!-- end toolbar -->
            <div class="grid_wrap">       
            <!-- grid --->
              <dx:ASPxGridView ID="gridOrder" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" width="100%" 
                    ClientInstanceName="grdOrder" DataSourceID="LinqServerModeOrders" 
                     oncustomcallback="gridOrder_CustomCallback" 
                ondatabound="gridOrder_DataBound" 
                    oncustomunboundcolumndata="gridOrder_CustomUnboundColumnData" 
                    onhtmlrowcreated="gridOrder_HtmlRowCreated" KeyFieldName="OrderIx" >
                    <SettingsBehavior AutoExpandAllGroups="True" ColumnResizeMode="Control" 
                        EnableRowHotTrack="True" AllowFocusedRow="True" />
                    <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                        <LoadingPanel ImageSpacing="5px">
                        </LoadingPanel>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="50" Position="TopAndBottom" AlwaysShowPager="True">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
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
                    <SettingsText Title="Search results" EmptyDataRow="No records found" />
                    <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" 
                        CustomButtonClick="grid_CustomButtonClick" />
                         
                    <SettingsCookies Enabled="True" CookiesID="ord_tracking" />
                    <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" PopupEditFormWidth="600px" />
                    <Columns>
                             <dx:GridViewDataTextColumn Caption="orderid" FieldName="OrderID" 
                             Name="col_orderid" ReadOnly="True" ShowInCustomizationForm="False" 
                             UnboundType="Integer" Visible="False" VisibleIndex="0" Width="0px">
                             <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                                     ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Index Key" FieldName="OrderIx" 
                             Name="col_orderix" ReadOnly="True" VisibleIndex="41" Visible="False"  
                             Width="0px" ShowInCustomizationForm="False">
                             <Settings ShowFilterRowMenu="False" AllowAutoFilter="False" 
                                 AllowAutoFilterTextInputTimer="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Title_ID" FieldName="TitleID" 
                             Name="col_titleid" ReadOnly="True" ShowInCustomizationForm="False" 
                             Visible="False" VisibleIndex="40" Width="0px">
                             <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                 AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                 AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="Documents" FieldName="document_folder" 
                                 Name="col_documents" ReadOnly="True" ShowInCustomizationForm="False" 
                                 VisibleIndex="39" Width="0px" Visible="False" >
                                 <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                     AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                     AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                             </dx:GridViewDataTextColumn> 
		                <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="1" 
                            Caption="Order #" ReadOnly="True" Width="90px" Name="col_ordernumber">
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataColumn Caption="Files" VisibleIndex="2" Width="45px" ExportWidth="0"  
                                 ShowInCustomizationForm="False" Name="col_files">
                                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                    AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False" 
                                    AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False" />
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="dxbtnDocs" AutoPostBack="false" CausesValidation="false" ClientInstanceName="btnDocs" runat="server" 
                                        Text="" Image-Url="~/Images/icons/16x16/download.png" 
                                        Image-AlternateText="Click here to see documents" Cursor="pointer" EnableDefaultAppearance="False">
                                    </dx:ASPxButton>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="5" 
                            Caption="Title" ReadOnly="True" Width="250px" Name="col_title" 
                               ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" ShowInFilterControl="True" 
                                AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataMemoColumn Caption="Remarks" ExportWidth="150" 
                               FieldName="RemarksToCustomer" ReadOnly="True" VisibleIndex="6" 
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
                        <dx:GridViewDataTextColumn FieldName="OtherRef" VisibleIndex="10" 
                            Caption="Other Ref" ReadOnly="True" Width="110px" Name="col_otherref" 
                                 ExportWidth="110">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="Vessel Name" ExportWidth="75" 
                                 FieldName="vessel_name" Name="col_vesselname" ReadOnly="True" VisibleIndex="9" 
                                 Width="120px">
                                 <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                             </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="origin_port" VisibleIndex="11" 
                            Caption="Origin Port" ReadOnly="True" Width="100px" Name="col_origin" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETS" VisibleIndex="12" 
                            Caption="ETS" ReadOnly="True" Width="90px" Name="col_ets" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_port" VisibleIndex="13" 
                            Caption="Destination Port" ReadOnly="True" Width="120px" Name="col_dest" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETA" VisibleIndex="14" Caption="ETA" 
                            ReadOnly="True" Width="90px" Name="col_eta" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_place" VisibleIndex="15" 
                            Caption="Final Destination" Width="190px" Name="col_final" ExportWidth="90">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ContainerNumber" VisibleIndex="16" Caption="Container #" 
                             Width="200px" ReadOnly="True" Name="col_container" ExportWidth="160" >
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ex-Works date" FieldName="ExWorksDate" ReadOnly="True" 
                            VisibleIndex="17" Width="110px" Name="col_exworks" ExportWidth="75">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ISBN" FieldName="ISBN" ReadOnly="True" 
                            VisibleIndex="18" Width="100px" Name="col_isbn" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
				 <dx:GridViewDataTextColumn Caption="Status" ExportWidth="80" 
                               FieldName="current_status" Name="col_status" ReadOnly="True" VisibleIndex="19" 
                               Width="80px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="On" ExportWidth="90" 
                               FieldName="CurrentStatusDate" Name="col_currentstatusdate" ReadOnly="True" 
                               VisibleIndex="20" Width="90px">
                               <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                           <dx:GridViewDataDateColumn Caption="Last updated" FieldName="StatusDate" 
                               Name="col_last_updated" ReadOnly="True" VisibleIndex="21" Width="95px" 
                               ExportWidth="90">
                               <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                               </PropertiesDateEdit>
                           </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="22" 
                             Caption="Contact Name" Name="col_contact" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PONumber" ReadOnly="True" ExportWidth="100" Name="col_ponumber" 
                                 Width="100px" Caption="PO Number" VisibleIndex="23">
<Settings AutoFilterCondition="Contains"></Settings>
</dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="24" 
                             Caption="Customer" Name="col_company" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="25" 
                             Caption="Order Controller" Name="col_ordercontroller" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Printer" FieldName="printer_name" 
                             ReadOnly="True" VisibleIndex="26" Width="150px" ExportWidth="90" 
                               Name="col_printer">
                             <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                             <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                         </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="ETW" FieldName="ETW" Name="col_etw" 
                             ReadOnly="True" VisibleIndex="27" Width="90px" ExportWidth="75">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                       
                         <dx:GridViewDataDateColumn Caption="Due Warehouse" FieldName="WarehouseDate" 
                             ReadOnly="True" VisibleIndex="28" Width="145px" Name="col_due_wh" 
                                 ExportWidth="120">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         
                         <dx:GridViewDataTextColumn Caption="Unit price per copy" FieldName="UnitPricePerCopy" 
                             ReadOnly="True" VisibleIndex="29" Width="150px" 
                                 ExportWidth="50" Name="col_unitppc">
                         </dx:GridViewDataTextColumn>
                         
                         <dx:GridViewDataTextColumn FieldName="consignee_name" VisibleIndex="30" 
                             Caption="Consignee" Name="col_consignee" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        
                           <dx:GridViewDataTextColumn Caption="Impression" FieldName="Impression" ReadOnly="True" 
                            VisibleIndex="31" Width="100px" Name="col_impression" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="Date order created" 
                               FieldName="DateOrderCreated" ReadOnly="True" 
                            VisibleIndex="32" Width="135px" Name="col_datecreated" ExportWidth="100">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery copies" ExportWidth="100" 
                               FieldName="Copies" Name="col_copies" ReadOnly="True" VisibleIndex="33" 
                               Width="110px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery to" ExportWidth="130" 
                               FieldName="delivery_to" Name="col_delivery_to" ReadOnly="True" 
                               VisibleIndex="34" Width="150px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery address" ExportWidth="130" 
                               FieldName="delivery_addr1" Name="col_addr1" ReadOnly="True" VisibleIndex="35" 
                               Width="150px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Post code" ExportWidth="90" 
                               FieldName="delivery_postcode" Name="col_delivery_postcode" ReadOnly="True" 
                               VisibleIndex="36" Width="90px">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Tel." ExportWidth="120" 
                               FieldName="delivery_telno" Name="col_delivery_telno" ReadOnly="True" 
                               VisibleIndex="37" Width="120px">
                           </dx:GridViewDataTextColumn>
					 <dx:GridViewDataCheckColumn FieldName="JobClosed" VisibleIndex="38" 
                            Caption="Closed" ReadOnly="True" Width="70px" Name="col_closed" 
                                 ExportWidth="50">
                            <PropertiesCheckEdit DisplayTextChecked="Y" 
                                DisplayTextUnchecked="N" UseDisplayImages="False">
                            </PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                             <dx:GridViewCommandColumn Caption="Upload" VisibleIndex="4" ButtonType="Image" 
                                 ShowInCustomizationForm="False" ToolTip="Manage files" Width="48px" 
                                 Name="col_upload">
                                 <ClearFilterButton Visible="True">
                                 </ClearFilterButton>
                                 <CustomButtons>
                                     <dx:GridViewCommandColumnCustomButton ID="cmdFiles" Text="">
                                         <Image AlternateText="Manage documents" Height="16px" 
                                             Url="~/Images/icons/16x16/file_manager.png" Width="16px">
                                         </Image>
                                     </dx:GridViewCommandColumnCustomButton>
                                 </CustomButtons>
                             </dx:GridViewCommandColumn>
                    </Columns> 
                   
                    <StylesPager>
                        <PageNumber ForeColor="#3E4846">
                        </PageNumber>
                        <Summary ForeColor="#1E395B">
                        </Summary>
                    </StylesPager>
                   
             <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Visible" 
                        ShowFilterRow="True" ShowHorizontalScrollBar="True" 
                        ShowGroupedColumns="True" VerticalScrollableHeight="450" 
                        VerticalScrollBarStyle="Standard" ShowVerticalScrollBar="True" 
                        ShowTitlePanel="True"/>
             <SettingsCustomizationWindow Enabled="True" PopupVerticalAlign="Above"  />
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Auto" />
                    <StylesEditors ButtonEditCellSpacing="0">
                        <ProgressBar Height="21px">
                        </ProgressBar>
                    </StylesEditors>
                    <SettingsDetail ShowDetailButtons="False" />
                </dx:ASPxGridView>
         </div>
         <!-- end grid wrapper -->       
     </div>  <!-- end content div -->
    
    <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
    
    <dx:LinqServerModeDataSource ID="LinqServerModeOrders" runat="server" 
        ContextTypeName="linq.linq_view_orders_udfDataContext" 
        TableName="view_orders_by_age" />
        
     <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="original" SelectMethod="FetchByActiveAnonymous" 
                                TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
                            
     <dx:ASPxGridViewExporter ID="orderGridExport" runat="server" 
        GridViewID="gridOrder" ReportHeader="Exported search results" 
        Landscape="True">
    </dx:ASPxGridViewExporter>

        <dx:ASPxPopupControl ID="popDefault" runat="server" AppearAfter="800" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="popDefault" 
        CloseAction="CloseButton" HeaderText="" Height="100px" Width="100px" 
        PopupAction="None" EnableHotTrack="False" AllowDragging="True" 
        EnableHierarchyRecreation="True" AllowResize="True">
            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
            </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
            <LoadingPanelStyle ImageSpacing="5px">
            </LoadingPanelStyle>
        <Windows>
             <dx:PopupWindow CloseAction="CloseButton" 
                ContentUrl="../Popupcontrol/order_tracking_filter.aspx" HeaderText="Advanced search"
                Height="500px" Modal="True" Name="filterform" PopupAction="None" 
                Width="640px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
             <dx:PopupWindow HeaderText="You are not signed in" Name="msgform" 
                 Height="150px" Width="250px" Modal="true" 
                 PopupElementID="dxbtnmore" ShowCloseButton="True" ShowFooter="False"
                 ShowHeader="True">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                         <asp:Panel ID="pnlmsg" runat="server">
                            <div>
                            <div class="alert"> 
                            <asp:Label ID="lblmsg" runat="server" Text="You need to log in to use this option"></asp:Label></div><div style="width:100%; height:60px; padding: 10px"> 
                                 <div style="float: left; width: 25%"></div> 
                                 <div style="float: right; width: 75%">
                                        <dx:ASPxButton ID="dxbtmmsg" runat="server"  
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Ok" 
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
             <dx:PopupWindow ContentUrl="../Popupcontrol/Ord_Edit_Pallet.aspx" 
                 Name="editpalletform" CloseAction="CloseButton" 
                 HeaderText="Update pallet information" Height="500px" Modal="True" 
                 Width="520px">
                 <ContentCollection>
                     <dx:PopupControlContentControl runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>

             <dx:PopupWindow ContentUrl="../Popupcontrol/Op_Tracking_Demo1.htm" 
                 Name="demotracking1" CloseAction="CloseButton" 
                 HeaderText="Demonstration : how to perform a quick search" Height="670px" Modal="True" 
                 Width="840px">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
        
                 
            <dx:PopupWindow ContentUrl="../Popupcontrol/order_file_manager.aspx" 
                 Name="podfiles" CloseAction="CloseButton" 
                 HeaderText="File Manager" Height="640px" Modal="True" 
                 Width="760px">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
             <dx:PopupWindow CloseAction="CloseButton" 
                 ContentUrl="~/Popupcontrol/Sys_Not_Available.aspx" HeaderText="Not Available" 
                 Height="280px" Modal="True" Name="sysna" Width="340px">
                 <ContentCollection>
                     <dx:PopupControlContentControl runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
        </Windows>
     </dx:ASPxPopupControl>
     <!-- seperate popup for upload manager so we can trap onCloseUp client-side event and update recordset -->
    <dx:ASPxPopupControl ID="dxppUploadManager" 
        ClientInstanceName="ppUploadManager" runat="server" AllowDragging="True" 
        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
         <ClientSideEvents CloseUp="onUploadCloseUp" />
        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Windows>
            <dx:PopupWindow ContentUrl="../Popupcontrol/order_upload_manager.aspx" 
                 Name="ppUpload" CloseAction="CloseButton" 
                 HeaderText="File Manager" Height="660px" Modal="True" 
                 Width="760px">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
        </Windows>
    </dx:ASPxPopupControl>
</asp:Content> 


