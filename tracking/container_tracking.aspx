<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="container_tracking.aspx.cs" Inherits="container_tracking" %>

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
    function onGridInit(s, e) {
        gridContainer.PerformCallback('getdata');
    }
    
    //when checkbox in menu template it clicked
    function onCheckChanged(s, e) {
        hfMethod.Set("win", s.GetChecked());
    }
    
    function onTabClick(e) {
        //if tab = advanced search show popup otherrwise ignore
        //operatyed same was as btnMore_click
        if (e.tab.name == "tabOtherFilters" || e.tab.name == "tabReporting") {
            if (!gridContainer.InCallback()) {

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
            if (gridContainer.IsCustomizationWindowVisible())
            { gridContainer.HideCustomizationWindow(); }
            else
            { gridContainer.ShowCustomizationWindow(); }
            mnuGrid1.GetItemByName(e.item.name).SetText(UpdateMenuText());
        }
    }
    
    function UpdateMenuText() {
        var text = gridContainer.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " field chooser";
        return text;
    }
    //****
    
    function onDateRangeChanged() {
            if (!gridContainer.InCallback()) {
            gridContainer.PerformCallback(' ');
            //09/03/2011 history removed from this page
            //requestHistory();
            }
    }
        
    function submit_query(s) {
            if (!gridContainer.InCallback()) {
                hfMethod.Set("mode", s);
                gridContainer.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
    }

    function submit_mode(s) {
            if (!gridContainer.InCallback()) {
                hfMethod.Set("mode", s);
            }
    }

    function submit_batch_request() {
            if (!gridContainer.InCallback()) {
                gridContainer.PerformCallback('batchupdate');
            }
    }

    function submit_company_id(s, e) {
            if (!gridContainer.InCallback()) {
                hfMethod.Set("mode", 2);  //set mode to search so company filter can be applied
                gridContainer.PerformCallback(' ');
            }
    }
        
    function btnmore_click(s, e) {

            if (!gridContainer.InCallback()) {

                user = verify_user();
                //src=containers
                if (user != 'You are not signed in') {
                    var window1 = popDefault.GetWindowByName('filterform');
                    popDefault.SetWindowContentUrl(window1, '');
                    popDefault.SetWindowContentUrl(window1, '../Popupcontrol/order_tracking_filter.aspx?src=container');
                    
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
                if (gridContainer.IsCustomizationWindowVisible())
                    gridContainer.HideCustomizationWindow();
                else
                    gridContainer.ShowCustomizationWindow();
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
            var text = gridContainer.IsCustomizationWindowVisible() ? "Hide" : "Show";
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
             if (!gridContainer.InCallback()) {
                 user = verify_user();

                 if (user != 'You are not signed in') {
                     hfMethod.Set("mode", s);
                     gridContainer.PerformCallback(' ');
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

             if (!gridContainer.InCallback()) {

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

             if (!gridContainer.InCallback()) {

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
    
    //new function when file upload button is clicked so we can avoid making a callback.postback
    //call web method so we can open new window
    //aspxridview.settingsbehaviour.allowfocusedrow=true MUST be set or can't get row index
    function grid_CustomButtonClick(s, e) {
             e.processOnServer = false;
             
             var user = verify_user();
             if (user != 'You are not signed in') {
                 if (e.buttonID == 'cmdDeliveries') {
                     //var window = popDefault.GetWindowByName('sysna');
                     //popDefault.ShowWindow(window);
                     //just use order number
                     //s.GetRowValues(s.GetFocusedRowIndex(), 'ContainerID;ContainerNumber;DeliveryAddress', onGotValues);
                     s.GetRowValues(s.GetFocusedRowIndex(), 'ContainerNumber;ContainerIdx', onGotValues);
                 }
             }
             else {
                 //var window = popDefault.GetWindowByName('sysna');
                 //popDefault.ShowWindow(window);
                 var window = popDefault.GetWindowByName('msgform');
                 popDefault.ShowWindow(window);
             }
    }

   
    //using webmethod in code behind
    function onGotValues(values) {
        //alert(values[0]);
        //alert(values[1]);
        //can pass values as iList<string> or concatenated using values.toString() method
        PageMethods.get_secure_url(values, 'cmdDeliveries', onMethodComplete);
    }

    function onMethodComplete(result, userContext, methodName) {
        var checked = hfMethod.Get("win");
        
        if (result != "") {
            if (!checked) {
                var window1 = popDefault.GetWindowByName('ppDeliveries');
                popDefault.SetWindowContentUrl(window1, '');
                popDefault.SetWindowContentUrl(window1, result.toString());
                popDefault.ShowWindow(window1);
            }
            else {
                //opens form in new window
                window.open(result, "_blank");
            }
        }
        else {
            alert('PageMethods.get_secure_url() returned null');
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
                    HeaderText="Container tracking" 
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
                                ValueType="System.Int32" Width="200px" Spacing="0" ClientVisible="False" 
                                Enabled="False" Visible="False">
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
                                                                                         UseSubmitBehavior="False" Width="35px" 
                                                        ClientEnabled="False" ClientVisible="False" Visible="False">
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
                                ToolTip="Additional filters for job status, user and cpmpany" 
                                ClientEnabled="False" ClientVisible="False">
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
                                                                    	
			                                                                    if (!gridContainer.InCallback()) {
                                                                                    gridContainer.PerformCallback(' ');
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
                                                                        	
			                                                                        if (!gridContainer.InCallback()) {
                                                                                        gridContainer.PerformCallback(' ');
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
                            <dx:TabPage Name="tabReporting" Text="Reporting" ClientEnabled="False" 
                                ClientVisible="False">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server">
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
                                     <dx:MenuItem Name="mnuDetails" Text="Details in new window">
                                         <Template>
                                             <dx:ASPxCheckBox ID="dxckDetails" runat="server" CheckState="Unchecked" 
                                                ClientInstanceName="ckDetails" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                Text="Details in new window" TextAlign="Left">
                                                <ClientSideEvents CheckedChanged="onCheckChanged" />
                                            </dx:ASPxCheckBox>   
                                         </Template>
                                     </dx:MenuItem>
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
              <dx:ASPxGridView ID="gridContainer" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" width="100%" 
                    ClientInstanceName="gridContainer" DataSourceID="LinqServerModeContainers" 
                     oncustomcallback="gridContainer_CustomCallback" 
                    KeyFieldName="ContainerIdx" >
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
                        CustomButtonClick="grid_CustomButtonClick" Init="onGridInit" />
                    <SettingsCookies Enabled="True" CookiesID="cnrtracking" />
                    <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" PopupEditFormWidth="600px" />
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Deliveries" Name="colCustom" VisibleIndex="0" 
                        Width="70px" ButtonType="Image">
                        <ClearFilterButton Visible="true">
                        </ClearFilterButton>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="cmdDeliveries" Text="Deliveries" 
                                Visibility="AllDataRows">
                                <Image AlternateText="Delivery details" ToolTip="Delivery details" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/details18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Container number" 
                            FieldName="ContainerNumber" Name="colContainerNumber" VisibleIndex="1" ExportWidth="100"
                            Width="150px" ReadOnly="True">
                            <Settings ShowInFilterControl="True" ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Container type" FieldName="ContainerType" ExportWidth="100"
                            Name="colContainerType" VisibleIndex="2" Width="120px" ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Container status" 
                            FieldName="ContainerStatus" ExportWidth="100"
                            Name="colContainerType" VisibleIndex="3" Width="130px" ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Package type" FieldName="MaxPackageType" 
                            Name="colPackageType" VisibleIndex="4" Width="100px" ExportWidth="80"
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="# Packages" FieldName="SumPackages" 
                            Name="colSumPackages" VisibleIndex="5" Width="90px" ExportWidth="85" 
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total Weight" FieldName="SumWeight" 
                            Name="colSumWeight" VisibleIndex="6" Width="115px" ExportWidth="60" 
                            ReadOnly="True">
                            <PropertiesTextEdit DisplayFormatString="F">
                            </PropertiesTextEdit>
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total Cbm" FieldName="SumCbm" 
                            Name="colSumCbm" VisibleIndex="7" Width="115px" ExportWidth="60" 
                            ReadOnly="True">
                            <PropertiesTextEdit DisplayFormatString="F">
                            </PropertiesTextEdit>
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Vessel name" FieldName="Joined" 
                            Name="colJoined" VisibleIndex="8" Width="275px" ExportWidth="200" 
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Voyage number" FieldName="VoyageNumber" 
                            Name="colVoyageNumber" VisibleIndex="9" Width="100px" ExportWidth="90" 
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="ETS" FieldName="MaxETS" 
                            Name="colEts" VisibleIndex="10" ExportWidth="75" ReadOnly="True" 
                            Width="110px">
                            <PropertiesDateEdit Spacing="0" DisplayFormatString="{0:d}">
                            </PropertiesDateEdit>
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataDateColumn>
                         <dx:GridViewDataDateColumn Caption="ETA" FieldName="MaxETA" 
                            Name="colEta" VisibleIndex="11" ExportWidth="75" ReadOnly="True" 
                            Width="110px">
                            <PropertiesDateEdit Spacing="0" DisplayFormatString="{0:d}">
                            </PropertiesDateEdit>
                             <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn Caption="Loaded on board" FieldName="LoadedOnBoard" 
                            Name="colLoadedOnBoard" VisibleIndex="12" Width="125px" ExportWidth="40" 
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn Caption="Delivered" FieldName="Delivered" 
                            Name="colDelivered" VisibleIndex="13" Width="100px" ExportWidth="40" 
                            ReadOnly="True">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn Caption="Devanned" FieldName="Devanned" 
                            Name="colDevanned" VisibleIndex="14" ExportWidth="40" ReadOnly="True" 
                            Width="100px">
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataDateColumn Caption="Delivery date" FieldName="DeliveryDate" 
                            Name="colDeliveryDate" VisibleIndex="15" Width="100px" ExportWidth="75" 
                            ReadOnly="True">
                            <PropertiesDateEdit Spacing="0">
                            </PropertiesDateEdit>
                            <Settings ShowFilterRowMenu="True" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="Origin port" FieldName="OriginPort" 
                             Name="colOriginPort" ReadOnly="True" VisibleIndex="16" Width="125px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Destination port" FieldName="DestinationPort" 
                             Name="colDestinationPort" ReadOnly="True" VisibleIndex="17" Width="125px">
                        </dx:GridViewDataTextColumn>
                    </Columns> 
                    <StylesPager>
                        <PageNumber ForeColor="#3E4846">
                        </PageNumber>
                        <Summary ForeColor="#1E395B">
                        </Summary>
                    </StylesPager>
                   <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="dxgridMeasures" runat="server" AutoGenerateColumns="False" 
                                    ClientInstanceName="gridMeasures" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" KeyFieldName="ContainerSubID" 
                                    onbeforeperformdataselect="dxgridMeasures_BeforePerformDataSelect" Width="100%">
                                    <SettingsBehavior AllowGroup="False" EnableRowHotTrack="True" />
                                    <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                        <LoadingPanel ImageSpacing="5px">
                                        </LoadingPanel>
                                    </Styles>
                                    <SettingsPager PageSize="20" Position="TopAndBottom">
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
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Index key" FieldName="ContainerSubID" 
                                            Name="colContainerSubID" ReadOnly="True" ShowInCustomizationForm="False" 
                                            Visible="False" VisibleIndex="9" Width="0px">
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                                AllowHeaderFilter="False" AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Packages" FieldName="NumberOfPackages" 
                                            Name="colPackages" ReadOnly="True" VisibleIndex="1" Width="70px">
                                            <PropertiesTextEdit DisplayFormatString="F">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Weight" FieldName="Weight" Name="colWeight" 
                                            ReadOnly="True" VisibleIndex="2" Width="75px">
                                            <PropertiesTextEdit DisplayFormatString="F">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Cbm" FieldName="Cbm" Name="colCbm" 
                                            ReadOnly="True" VisibleIndex="3" Width="75px">
                                            <PropertiesTextEdit DisplayFormatString="F">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Ex-Works" FieldName="ExWorksDate" 
                                            Name="colExWorksDate" ReadOnly="True" VisibleIndex="4" Width="105px">
                                            <PropertiesDateEdit Spacing="0" DisplayFormatString="{0:d}">
                                                </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Caption="Cargo ready" FieldName="CargoReady" 
                                            Name="colCargoReady" ReadOnly="True" VisibleIndex="5" Width="105px">
                                            <PropertiesDateEdit Spacing="0" DisplayFormatString="{0:d}">
                                                </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Caption="Booking received" 
                                            FieldName="BookingReceived" Name="colBookingReceived" ReadOnly="True" 
                                            VisibleIndex="6" Width="110px">
                                            <PropertiesDateEdit Spacing="0" DisplayFormatString="{0:d}">
                                                </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="Client" FieldName="Client" Name="colClient" 
                                            ReadOnly="True" VisibleIndex="7" Width="150px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Printer" FieldName="Printer" 
                                            Name="colPrinter" ReadOnly="True" VisibleIndex="8" Width="150px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Order Number" FieldName="OrderNumber" 
                                            Name="colOrderNumber" ReadOnly="True" VisibleIndex="0" Width="90px">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <StylesPager>
                                        <PageNumber ForeColor="#3E4846">
                                        </PageNumber>
                                        <Summary ForeColor="#1E395B">
                                        </Summary>
                                    </StylesPager>
                                    <Settings ShowHorizontalScrollBar="True" />
                                    <StylesEditors ButtonEditCellSpacing="0">
                                        <ProgressBar Height="21px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </DetailRow> 
                   </Templates> 
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
                    <SettingsDetail ShowDetailRow="True" />
                </dx:ASPxGridView>
         </div>
         <!-- end grid wrapper -->  

     </div>  <!-- end content div -->
        
     
           <dx:ASPxPopupControl ID="dxpopDefault" 
        ClientInstanceName="popDefault" runat="server" AllowDragging="True" 
        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css"  
        AllowResize="True" SaveStateToCookies="True" 
                SaveStateToCookiesID="ppDeliveries" DragElement="Header"  
        ShowSizeGrip="True" CloseAction="CloseButton" MinHeight="200px" 
        MinWidth="200px">
        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="ppControlContentControlContainer" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Windows>
            <dx:PopupWindow ContentUrl="../Popupcontrol/container_contents.aspx" 
                 Name="ppDeliveries" CloseAction="CloseButton" 
                 HeaderText="Container details" Height="80%" Modal="True" 
                 Width="80%">
                 <ContentCollection>
                     <dx:PopupControlContentControl ID="ppContentControlDeliveries" runat="server">
                     </dx:PopupControlContentControl>
                 </ContentCollection>
             </dx:PopupWindow>
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
        </Windows>
    </dx:ASPxPopupControl>
             
            <dx:ASPxHiddenField ID="dxhfMethod" runat="server" 
                              ClientInstanceName="hfMethod">
                          </dx:ASPxHiddenField>
                
                <dx:LinqServerModeDataSource ID="LinqServerModeContainers" runat="server" 
                    ContextTypeName="linq.linq_view_aggregate_containersDataContext" 
                    TableName="aggregate_containers_by_ets_and_filters" />
                    
                 <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                            
    OldValuesParameterFormatString="original" SelectMethod="FetchByContainers" 
                                            
                TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
                  </asp:ObjectDataSource>
                                        
                 <dx:ASPxGridViewExporter ID="containerGridExport" runat="server" 
                    GridViewID="gridContainer" ReportHeader="Exported search results" 
                    Landscape="True">
                </dx:ASPxGridViewExporter>

           
</asp:Content> 


