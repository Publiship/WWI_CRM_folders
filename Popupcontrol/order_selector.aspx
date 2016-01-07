<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_selector.aspx.cs" Inherits="Popupcontrol_order_selector" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxObjectContainer" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
    
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSiteMapControl" tagprefix="dx1" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search orders</title>
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/680gs12col_fixed.css" type="text/css" />
    
    <script type="text/javascript">
        // <![CDATA[
        function onDateRangeChanged() {
            if (!gdvOrder.InCallback()) {
                gdvOrder.PerformCallback(' ');
                //09/03/2011 history removed from this page
                //requestHistory();
            }
        }
        
        function submit_query(s) {
            if (!gdvOrder.InCallback()) {
                hfsource.Set("mode", s);
                gdvOrder.PerformCallback(' ');
            }
        }

        function submit_save_request(s) {
            if (!gdvOrder.InCallback()) {
                gdvOrder.PerformCallback(s);
            }
        }
        
        function btnadvanced_click(s, e) {

            if (!gdvOrder.InCallback()) {

                //user = verify_user(); //no need, must be logged in to get to this page in the 1st place

                //if (user != 'You are not logged in') {
                var window = popDefault.GetWindowByName('filterform');
                popDefault.SetWindowContentUrl(window, '');
                popDefault.SetWindowContentUrl(window, 'order_tracking_filter.aspx?src=pod_search');

                    //popDefault.RefreshWindowContentUrl(window); don't use - this causes IE7 "resend" problem
                popDefault.ShowWindow(window);
                //}
               
            }
        }

        function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }

        function SetAllCheckBoxes(FormName, FieldName, CheckValue) {
            var objCheckBoxes = document.forms[FormName].elements[FieldName];
            if (!objCheckBoxes)
                return;
            var countCheckBoxes = objCheckBoxes.length;
            alert(countCheckBoxes); 
            if (!countCheckBoxes)
                objCheckBoxes.checked = CheckValue;
            else
            // set the check value for all check boxes
                for (var i = 0; i < countCheckBoxes; i++)
                objCheckBoxes[i].checked = CheckValue;
        }
 
        function rowTicked(key1, key2) {
            //only ever track last row ticked
            //var itemkey = "key" + key.toString();
                         
            var itemkey = "orderidkey";
            var itemvalue = key1 + ";" + key2;
            
            if (!hfsource.Contains(itemkey)) {
                hfsource.Add(itemkey, itemvalue);
            }
            else {
                hfsource.Set(itemkey, itemvalue);
            }
        }
        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="scm_pod" runat="server" />
    <div class="container_12">
        <!-- message panel -->
        <div class="grid_12">
        <dx:ASPxPanel ID="dxpnlerr" ClientInstanceName="pnlerr" runat="server" 
                Visible="false" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent> 
                        <div class="alert"> 
                            <dx:ASPxLabel ID="lblerr" ClientInstanceName="dxlblerr" runat="server" 
                                BackColor="#FFE6DF" 
                                            EnableDefaultAppearance="False" Font-Names="Arial,Helvetica,Sans-serif" 
                                            Font-Size="Small" ForeColor="#CC0000" Height="40px" width="100%"
                                            Text=" Error message" >
                                            <Border BorderColor="#FFD9CF" BorderStyle="Solid" BorderWidth="1px" />
                                        </dx:ASPxLabel>
                           </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
        </div>
        <div class="clear"></div>
        <!-- filters -->
        <div class="grid_3 pad_bottom">
            <dx:ASPxComboBox ID="dxcbofields" 
                                    runat="server" ClientInstanceName="cbofields" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue"  Width="160px"
                                                DataSourceID="ObjectDataSourceFields" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         ToolTip="Pick what you want to search for here e.g. ISBN number" 
                                         TabIndex="1" Spacing="0">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                    </LoadingPanelStyle>
                                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                </LoadingPanelImage>
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="FilterCaption" Name="fieldcaption" 
                                                        Caption="Search in" />
                                                    <dx:ListBoxColumn FieldName="FieldName" Name="fieldname" Visible="False" />
                                                    <dx:ListBoxColumn FieldName="ColumnType" Name="columntype" Visible="False" />
                                                </Columns>
                                            </dx:ASPxComboBox>
        </div> 
        <div class="grid_3">
              <dx:ASPxTextBox ID="txtQuickSearch" runat="server" 
                                                    ClientInstanceName="txtQuick" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                    Width="150px" TabIndex="2" 
                                              NullText="Your reference here" EnableClientSideAPI="True" 
                                       ToolTip="Enter the reference you are looking for e.g. the ISBN number or title" 
                                        EnableViewState="False">
                                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' '\/]{1,100}$" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents KeyUp="function(s, e) {textboxKeyup}" />
                                                    </dx:ASPxTextBox> 
        </div> 
        <div class="grid_3">
            <dx:ASPxComboBox ID="dxcboRange" runat="server" ClientInstanceName="cboRange" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         ValueType="System.Int32" Width="160px" Spacing="0">
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                             <LoadingPanelStyle ImageSpacing="5px">
                                             </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                         <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                onDateRangeChanged();
                                                }" />
                                     </dx:ASPxComboBox>
        </div> 
        <div class="grid_3">
             <dx:ASPxButton ID="aspxbtnFilter" runat="server" 
                                                TabIndex="3" 
                                                ClientInstanceName="btnFilter" EnableClientSideAPI="True" 
                                                AutoPostBack="False" CausesValidation="False" 
                                                ToolTip="Click here to begin your search" 
                                                EnableTheming="False"  
                                                Height="26px" 
                                                Text="Search" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                UseSubmitBehavior="False">
                                                           <Image Height="16px" Width="16px" AlternateText="Begin search" 
                                                   Url="~/Images/icons/16x16/magnifier.png">
                                               </Image>
                                               <ClientSideEvents Click="function(s, e) {
	                                                submit_query(1);
                                                }" />
                                      </dx:ASPxButton>
        </div> 
        <div class="clear"></div>
        <div class="grid_3 pad_bottom">
              <dx:ASPxButton ID="btnEndFilter" runat="server" 
                                    Text="Clear search" UseSubmitBehavior="False"
                                        OnClick="btnEndFilter_Click" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="130px" 
                                            ToolTip="Click to clear your current search results and start again" EnableTheming="False" 
                                              Height="26px" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" >
                                              <Image AlternateText="Clear search" Height="16px" 
                                                  Url="~/Images/icons/16x16/arrow_refresh.png" Width="16px">
                                              </Image>
                                          </dx:ASPxButton>
        </div> 
        <div class="grid_3">
             <dx:ASPxButton ID="dxbtnadvanced" runat="server" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                       Text="Advanced" Width="130px" AutoPostBack="False" 
                                                       ClientInstanceName="btnadvanced" Height="26px" 
                                                       TabIndex="8" CausesValidation="False" 
                                                       ToolTip="Click here to use the advanced search builder (You must be logged in)" 
                                                       UseSubmitBehavior="False" 
                                                       EnableTheming="False" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue">
                                                           
                                                            <Image Height="16px" Width="16px" Url="~/Images/icons/16x16/filter_add.png">
                                                            </Image>
                                                           
                                                            <ClientSideEvents Click="btnadvanced_click" />
                                                         </dx:ASPxButton>
        </div> 
        <div class="grid_6 pad_bottom">
        </div> 
        <div class="clear"></div>
        <!-- search results -->
        <div class="grid_12 pad_bottom">
        <dx:ASPxGridView ID="dxgdvOrder" ClientInstanceName="gdvOrder" runat="server" 
              AutoGenerateColumns="False" 
              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
              CssPostfix="Office2010Blue" DataSourceID="LinqServerModePod" 
              KeyFieldName="OrderIx" width="100%" oncustomcallback="dxgdvOrder_CustomCallback" 
                     onhtmlrowcreated="dxgdvOrder_HtmlRowCreated" 
                     onrowcommand="dxgdvOrder_RowCommand">
                     <SettingsBehavior AllowGroup="False" 
                  EnableRowHotTrack="True" ColumnResizeMode="Control" AllowFocusedRow="True" 
                  AllowSelectSingleRowOnly="True" />
                     <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                  CssPostfix="Office2010Blue">
                         <Header ImageSpacing="5px" SortingImageSpacing="5px">
                         </Header>
                         <LoadingPanel ImageSpacing="5px">
                         </LoadingPanel>
                     </Styles>
                     <SettingsPager Position="TopAndBottom">
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
                     <SettingsCookies Enabled="True" StoreColumnsVisiblePosition="False" 
                  StoreFiltering="False" StoreGroupingAndSorting="False" StorePaging="False" />
                     <SettingsText Title="Search results" />
                     <Columns>
                         <dx:GridViewDataTextColumn Caption="Select" FieldName="OrderID" 
                             Name="colquoteid" ReadOnly="false"  VisibleIndex="0" Width="50px">
                             <Settings AllowAutoFilter="False" AllowGroup="False" AllowHeaderFilter="False" 
                                 AllowSort="False" ShowFilterRowMenu="False" ShowInFilterControl="False"   />
                             <EditFormSettings Visible="False" />
                             <DataItemTemplate>
                                 <dx:ASPxButton ID="dxbtntick" runat="server" AutoPostBack="true" 
                                     CausesValidation="False" ClientInstanceName="btntick" Cursor="pointer" 
                                     ToolTip="Click here to select this order" EnableDefaultAppearance="False" EnableTheming="False"     
                                     Width="40px" CommandArgument="selectpod" ImagePosition="Left" Text="">
                                     <Image AlternateText="Click here to use select this order" Height="16px"  
                                         Url="~/Images/icons/16x16/tick.png" Width="16px">
                                     </Image>
                                 </dx:ASPxButton>
                             </DataItemTemplate>
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataCheckColumn Caption="#" Name="chkcustom" 
                      ShowInCustomizationForm="False" UnboundType="Boolean" VisibleIndex="1" 
                      Width="35px">
                             <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                          AllowDragDrop="False" AllowHeaderFilter="False" />
                             <DataItemTemplate>
                                 <dx:ASPxCheckBox ID="dxchktick" ClientInstanceName="chktick" runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                        CssPostfix="Office2003Blue" SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" >
                                 </dx:ASPxCheckBox>
                             </DataItemTemplate>
                         </dx:GridViewDataCheckColumn>
                         <dx:GridViewDataTextColumn FieldName="OrderID" Visible="False" VisibleIndex="2">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="3" 
                      Caption="Order#">
                             <Settings AutoFilterCondition="BeginsWith" FilterMode="DisplayText" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="HouseBLNUmber" VisibleIndex="5" 
                      Caption="House B/L">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="CustomersRef" VisibleIndex="6" 
                      Caption="Customers Ref">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataDateColumn FieldName="DateOrderCreated" VisibleIndex="7" 
                      Caption="Order Created">
                             <PropertiesDateEdit Spacing="0">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         <dx:GridViewDataTextColumn FieldName="origin_port" VisibleIndex="8" 
                      Caption="Origin Port">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="destination_place" VisibleIndex="9" 
                      Caption="Final destination">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="4" Width="220px">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataCheckColumn FieldName="JobClosed" VisibleIndex="10" 
                      Caption="Closed">
                         </dx:GridViewDataCheckColumn>
                         <dx:GridViewDataTextColumn FieldName="ContainerNumber" VisibleIndex="11" 
                      Caption="Container">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="ISBN" VisibleIndex="12">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataDateColumn FieldName="ExWorksDate" VisibleIndex="13" 
                      Caption="Ex-works date">
                             <PropertiesDateEdit Spacing="0">
                             </PropertiesDateEdit>
                         </dx:GridViewDataDateColumn>
                         <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="14"  Width="150px"
                      Caption="Contact name">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="15" Width="150px"
                      Caption="Company">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="OrderIx" ReadOnly="True" 
                      VisibleIndex="16" Visible="False">
                         </dx:GridViewDataTextColumn>
                     </Columns>
                     <StylesPager>
                         <PageNumber ForeColor="#3E4846">
                         </PageNumber>
                         <Summary ForeColor="#1E395B">
                         </Summary>
                     </StylesPager>
                     <Settings ShowFilterBar="Auto" ShowFilterRow="True" 
                  ShowFilterRowMenu="True" ShowHorizontalScrollBar="True" 
                  VerticalScrollableHeight="250" ShowHeaderFilterButton="True" 
                  ShowVerticalScrollBar="True" ShowTitlePanel="True" GridLines="Horizontal" />
                     <StylesEditors ButtonEditCellSpacing="0">
                         <ProgressBar Height="21px">
                         </ProgressBar>
                     </StylesEditors>
                 </dx:ASPxGridView>
        </div>
        <div class="clear"></div>
        <!-- save and cancel buttons -->
        <div class="grid_2">
            <dx:ASPxButton ID="dxbtnsave" ClientInstanceName="btnsave" runat="server" 
                  Text="Save" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                  CssPostfix="Office2010Blue" 
                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                Height="26px" AutoPostBack="False" CausesValidation="False" 
                UseSubmitBehavior="False" EnableTheming="False" onclick="dxbtnsave_Click">
                <Image Height="16px" Url="~/Images/icons/16x16/disk.png" Width="16px">
                </Image>
            </dx:ASPxButton>     
        </div>
        <div class="grid_8">
              <dx:ASPxHiddenField ID="dxhfsource" runat="server" 
              ClientInstanceName="hfsource">
          </dx:ASPxHiddenField>
        </div>
        <div class="grid_2">
        <dx:ASPxButton ID="dxbtnclose" ClientInstanceName="btnclose" runat="server" 
                  Text="Cancel" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                  CssPostfix="Office2010Blue" 
                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                Height="26px" AutoPostBack="False" CausesValidation="False" 
                 UseSubmitBehavior="False" Width="110px">
                <Image Height="16px" Url="~/Images/icons/16x16/cross.png" Width="16px">
                </Image>
                 <ClientSideEvents Click="function(s, e) {
	                    window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));
                    }" />
            </dx:ASPxButton>   
        </div> 
        <div class="clear"></div>
        <!-- datasources-->
        <div class="grid_6">
        <dx:linqservermodedatasource ID="LinqServerModePod" runat="server" 
            ContextTypeName="linq.linq_view_orders_udfDataContext" TableName="view_orders_by_age" />
        </div>
        <div class="grid_6">
         <asp:ObjectDataSource ID="ObjectDataSourceFields" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FetchByActive" 
                                TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
      </asp:ObjectDataSource>
       </div>
       <!-- pop up for advanced search -->
       <div class="grid_12">
        <dx:ASPxPopupControl ID="popDefault" ClientInstanceName="popDefault" 
        runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
        AllowDragging="True" CloseAction="CloseButton" PopupElementID="btnadvanced" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
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
                ContentUrl="Ord_Filter_Advanced.aspx" HeaderText="Advanced search"
                Height="500px" Modal="True" Name="filterform" PopupAction="None" 
                Width="640px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
       </Windows>
    </dx:ASPxPopupControl>
       </div> 
    </div>
    <!-- end container -->
    
    </form>
    
</body>
</html>
