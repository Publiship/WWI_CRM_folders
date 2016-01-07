<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_search.aspx.cs" Inherits="order_search" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onGridInit(s, e) {
            gridOrders.PerformCallback('getdata');
        }
        
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
                        hfOrder.Set("orders", "user")
                        gridOrders.PerformCallback(' ');
                        lblSearch.SetText('My orders');
                    }
                    break;
                case "cmdOrdersAll":
                    if (!gridOrders.InCallback()) {
                        hfOrder.Remove("orders");
                        gridOrders.PerformCallback(' ');
                        lblSearch.SetText('All orders');
                    }
                    break;
                case "cmdOrdersByOffice":
                    if (!gridOrders.InCallback()) {
                        hfOrder.Set("orders", "office")
                        gridOrders.PerformCallback(' ');
                        lblSearch.SetText('Office orders');
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

        //call web method so we can open new window
        //aspxridview.settingsbehaviour.allowfocusedrow=true MUST be set or can't get row index
        function onCustomButtonClick(s, e) {
            switch(e.buttonID){
                case 'cmdView':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber', onGetViewValues);
                    break;
                case 'cmdEdit':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber', onGetEditValues);
                    break;
                case 'cmdOrderSheet':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber', onGetOrderSheetValues);
                    break;
                case 'cmdTemplate':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber', onGetTemplateValues);
                    break;
                case 'cmdClone':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber', onGetCloneValues);
                    break;
                case 'cmdFiles':
                    s.GetRowValues(s.GetFocusedRowIndex(), 'OrderID;OrderNumber;document_folder;HouseBLNUmber', onGetFileValues);
                    break;
            }//end switch
        }

        function onGetFileValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdFiles', onFileMethodComplete);
        }
        
        function onGetViewValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdView', onMethodComplete);
        }

        function onGetEditValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdEdit', onMethodComplete);
        }

        function onGetOrderSheetValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdOrderSheet', onMethodComplete);
        }

        function onGetTemplateValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdTemplate', onMethodComplete);
        }

        function onGetCloneValues(values) {
            //alert(values[0]);
            //alert(values[1]);
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_secure_url(values, 'cmdClone', onMethodComplete);
        }
        
        function onMethodComplete(result, userContext, methodName) {
            if (result != "") {
                window.open(result, "_blank");
            }
            else {
                //do we need an alert? just stay in current window
                //alert('PageMethods.get_secure_url() returned null');
            }
        }

        function onFileMethodComplete(result, userContext, methodName) {
            if (result != "") {
                if (result != "denied") {
                    //open window as pop up
                    var window1 = ppFileManager.GetWindowByName('ppFiles');
                    ppFileManager.SetWindowContentUrl(window1, '');
                    ppFileManager.SetWindowContentUrl(window1, result.toString());
                    ppFileManager.ShowWindow(window1);
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
            window.ppFileManager.HideWindow(window.ppFileManager.GetWindowByName('ppFiles'));
            if (!gridOrders.InCallback()) {
                gridOrders.PerformCallback(' ');
            }
        }
        // ]]>
    </script>    

    <div class="container_16">
    
        <!-- messages -->
        <div class="grid_16">
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
        <div class="grid_10 pad_bottom">
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Search orders |">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblSearch" runat="server" ClientInstanceName="lblSearch" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
                <dx:ASPxComboBox ID="dxcboRange" runat="server" ClientInstanceName="cboRange" 
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" Spacing="0" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                    ValueType="System.String" Width="225px">
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
        </div>
        <!-- images and text -->
        <div class="grid_6">
         </div>              
        <div class="clear"></div>
        
        <!-- search options -->
        <div class="grid_16">
        
            <dx:ASPxMenu ID="dxmnuToolbar" ClientInstanceName="mnuToolbar" runat="server" AutoSeparators="RootOnly" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                ItemAutoWidth="False" Width="100%">
                <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanelImage>
                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                <ClientSideEvents ItemClick="function(s, e) { OnMenuItemClick(s, e); }" />
                <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
            </dx:ASPxMenu>
        
        </div>
        <div class="clear"></div>
        <!-- individual form menus -->
        <div class="grid_16 pad_bottom">
        <!-- search grid -->
             <dx:ASPxGridView ID="dxgridOrders" ClientInstanceName="gridOrders" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" width="100%" 
                oncustomcallback="dxgridOrders_CustomCallback" 
                KeyFieldName="OrderIx" DataSourceID="linqOrders">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior AllowGroup="False" ColumnResizeMode="Control" 
                    EnableRowHotTrack="True" AllowFocusedRow="True" />
                    <SettingsCookies CookiesID="dxgridOrders" Enabled="True" 
                     StoreFiltering="False" Version="1" />
                    <ClientSideEvents CustomButtonClick="onCustomButtonClick" 
                    Init="onGridInit" />
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsPager PageSize="25" Position="TopAndBottom">
                </SettingsPager>
                <Columns>
                      <dx:GridViewCommandColumn Caption="Options" Name="colCustom" VisibleIndex="0"  ButtonType="Image" 
                        Width="150px">
                        <ClearFilterButton Visible="true">
                        </ClearFilterButton>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="cmdView" 
                                Visibility="AllDataRows">
                                <Image AlternateText="View" ToolTip="View" Height="18px" 
                                    Url="~/Images/icons/metro/22x18/details18.png" Width="22px">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                               <dx:GridViewCommandColumnCustomButton ID="cmdEdit"  
                                    Visibility="AllDataRows">
                                    <Image Height="18px" ToolTip="Edit" AlternateText="Edit" 
                                        Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                             <dx:GridViewCommandColumnCustomButton ID="cmdOrderSheet" 
                                    Visibility="AllDataRows">
                                      <Image AlternateText="Order sheet" Height="18px" ToolTip="Order sheet" 
                                          Url="../Images/icons/metro/22x18/print18.png" Width="22px">
                                      </Image>
                                </dx:GridViewCommandColumnCustomButton>
                               <dx:GridViewCommandColumnCustomButton ID="cmdTemplate"
                                Visibility="AllDataRows">
                                 <Image AlternateText="Template" Height="18px" ToolTip="Template" 
                                      Url="~/Images/icons/metro/22x18/save_as18.png" Width="22px">
                                  </Image>
                            </dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="cmdClone"
                                Visibility="AllDataRows">
                                <Image AlternateText="Clone" Height="18px" ToolTip="Clone" 
                                      Url="../Images/icons/metro/22x18/copy18.png" Width="22px">
                                  </Image>
                            </dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="cmdFiles"
                                Visibility="AllDataRows">
                                <Image AlternateText="Documents" Height="18px" ToolTip="Documents" 
                                      Url="../Images/icons/metro/22x18/documents.png" Width="18px">
                                  </Image>
                            </dx:GridViewCommandColumnCustomButton>    
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="orderid" FieldName="OrderID" 
                             Name="col_orderid" ReadOnly="True" ShowInCustomizationForm="False" 
                             UnboundType="Integer" Visible="False" VisibleIndex="-1" Width="0px">
                             <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                                     ShowInFilterControl="False" AllowAutoFilterTextInputTimer="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Index Key" FieldName="OrderIx" 
                             Name="col_orderix" ReadOnly="True" VisibleIndex="38" Visible="False"  
                             Width="0px" ShowInCustomizationForm="False">
                             <Settings ShowFilterRowMenu="False" AllowAutoFilter="False" 
                                 AllowAutoFilterTextInputTimer="False" AllowDragDrop="False" AllowGroup="False" 
                                 AllowHeaderFilter="False" AllowSort="False" ShowInFilterControl="False" />
                         </dx:GridViewDataTextColumn>
		                <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="3" 
                            Caption="Order Number" ReadOnly="True" Width="105px" 
                          Name="col_ordernumber">
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="4" 
                            Caption="Title" ReadOnly="True" Width="250px" Name="col_title" 
                               ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" ShowInFilterControl="True" 
                                AutoFilterCondition="Contains" AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataMemoColumn Caption="Remarks" ExportWidth="150" 
                               FieldName="RemarksToCustomer" ReadOnly="True" VisibleIndex="36" 
                               Width="150px" Name="col_remarks">
                             <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataMemoColumn>
				<dx:GridViewDataTextColumn FieldName="HouseBLNUmber" VisibleIndex="5" 
                            Caption="House B/L" ReadOnly="True" Width="100px" Name="col_housebl" 
                                 ExportWidth="60">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CustomersRef" VisibleIndex="6" 
                            Caption="Customers Ref" ReadOnly="True" Width="110px" 
                             Name="col_customersref" ExportWidth="70">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="vessel_name" VisibleIndex="7" 
                            Caption="Vessel Name" ReadOnly="True" Width="120px" Name="col_vesselname" 
                                 ExportWidth="75">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="origin_port" VisibleIndex="8" 
                            Caption="Origin Port" ReadOnly="True" Width="100px" Name="col_origin" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETS" VisibleIndex="9" 
                            Caption="ETS" ReadOnly="True" Width="90px" Name="col_ets" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_port" VisibleIndex="10" 
                            Caption="Destination Port" ReadOnly="True" Width="120px" Name="col_dest" 
                                 ExportWidth="80">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="ETA" VisibleIndex="11" Caption="ETA" 
                            ReadOnly="True" Width="90px" Name="col_eta" ExportWidth="75">
                            <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="destination_place" VisibleIndex="12" 
                            Caption="Final Destination" Width="190px" Name="col_final" ExportWidth="90">
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ContainerNumber" VisibleIndex="13" Caption="Container" 
                             Width="130px" ReadOnly="True" Name="col_container" ExportWidth="100" >
                            <Settings ShowFilterRowMenu="True" AutoFilterCondition="Contains" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ex-Works date" FieldName="ExWorksDate" ReadOnly="True" 
                            VisibleIndex="14" Width="110px" Name="col_exworks" ExportWidth="75">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ISBN" FieldName="ISBN" ReadOnly="True" 
                            VisibleIndex="15" Width="100px" Name="col_isbn" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
				 <dx:GridViewDataTextColumn Caption="Status" ExportWidth="80" 
                               FieldName="current_status" Name="col_status" ReadOnly="True" VisibleIndex="16" 
                               Width="80px">
                           <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="On" ExportWidth="90" 
                               FieldName="CurrentStatusDate" Name="col_currentstatusdate" ReadOnly="True" 
                               VisibleIndex="17" Width="90px">
                               <PropertiesDateEdit DisplayFormatString="{0:d}">
                               </PropertiesDateEdit>
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataDateColumn>
                           <dx:GridViewDataDateColumn Caption="Last updated" FieldName="StatusDate" 
                               Name="col_last_updated" ReadOnly="True" VisibleIndex="18" Width="95px" 
                               ExportWidth="90">
                               <PropertiesDateEdit DisplayFormatString="{0:d}">
                               </PropertiesDateEdit>
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="19" 
                             Caption="Contact Name" Name="col_contact" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="20" 
                             Caption="Customer" Name="col_company" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="21" 
                             Caption="Order Controller" Name="col_ordercontroller" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Printer" FieldName="printer_name" 
                             ReadOnly="True" VisibleIndex="22" Width="150px" ExportWidth="90" 
                               Name="col_printer">
                             <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                 AllowAutoFilterTextInputTimer="False" />
                         </dx:GridViewDataTextColumn>
                           <dx:GridViewDataDateColumn Caption="ETW" FieldName="ETW" Name="col_etw" 
                             ReadOnly="True" VisibleIndex="23" Width="90px" ExportWidth="75">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                             </PropertiesDateEdit>
                               <Settings AllowAutoFilterTextInputTimer="False" />
                         </dx:GridViewDataDateColumn>
                       
                         <dx:GridViewDataDateColumn Caption="Due Warehouse" FieldName="WarehouseDate" 
                             ReadOnly="True" VisibleIndex="24" Width="145px" Name="col_due_wh" 
                                 ExportWidth="120">
                             <PropertiesDateEdit DisplayFormatString="{0:d}" Spacing="0">
                            </PropertiesDateEdit>
                             <Settings AllowAutoFilterTextInputTimer="False" />
                         </dx:GridViewDataDateColumn>
                         
                         <dx:GridViewDataTextColumn Caption="Unit price per copy" FieldName="UnitPricePerCopy" 
                             ReadOnly="True" VisibleIndex="25" Width="150px" 
                                 ExportWidth="50" Name="col_unitppc">
                             <Settings AllowAutoFilterTextInputTimer="False" />
                         </dx:GridViewDataTextColumn>
                         
                         <dx:GridViewDataTextColumn FieldName="consignee_name" VisibleIndex="26" 
                             Caption="Consignee" Name="col_consignee" ReadOnly="True" 
                             Width="150px" ExportWidth="75">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                 AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        
                           <dx:GridViewDataTextColumn Caption="Impression" FieldName="Impression" ReadOnly="True" 
                            VisibleIndex="27" Width="100px" Name="col_impression" ExportWidth="70">
                            <Settings AutoFilterCondition="Contains" ShowFilterRowMenu="True" 
                                   AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="Date order created" 
                               FieldName="DateOrderCreated" ReadOnly="True" 
                            VisibleIndex="28" Width="135px" Name="col_datecreated" ExportWidth="100">
                            <PropertiesTextEdit DisplayFormatString="{0:d}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery copies" ExportWidth="100" 
                               FieldName="Copies" Name="col_copies" ReadOnly="True" VisibleIndex="29" 
                               Width="110px">
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery to" ExportWidth="130" 
                               FieldName="delivery_to" Name="col_delivery_to" ReadOnly="True" 
                               VisibleIndex="30" Width="150px">
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Delivery address" ExportWidth="130" 
                               FieldName="delivery_addr1" Name="col_addr1" ReadOnly="True" VisibleIndex="31" 
                               Width="150px">
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Post code" ExportWidth="90" 
                               FieldName="delivery_postcode" Name="col_delivery_postcode" ReadOnly="True" 
                               VisibleIndex="32" Width="90px">
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Tel." ExportWidth="120" 
                               FieldName="delivery_telno" Name="col_delivery_telno" ReadOnly="True" 
                               VisibleIndex="33" Width="120px">
                               <Settings AllowAutoFilterTextInputTimer="False" />
                           </dx:GridViewDataTextColumn>
					        <dx:GridViewDataCheckColumn FieldName="JobClosed" VisibleIndex="34" 
                            Caption="Closed" ReadOnly="True" Width="70px" Name="col_closed" 
                                 ExportWidth="50">
                            <PropertiesCheckEdit DisplayTextChecked="Y" 
                                DisplayTextUnchecked="N" UseDisplayImages="False">
                            </PropertiesCheckEdit>
                            <Settings AllowAutoFilterTextInputTimer="False" />
                        </dx:GridViewDataCheckColumn>
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
    

    <dx:LinqServerModeDataSource ID="linqOrders" runat="server" 
        ContextTypeName="linq.linq_view_orders_udfDataContext" 
        TableName="view_orders_by_age" />
    
      <!-- seperate popup for upload manager so we can trap onCloseUp client-side event and update recordset -->
    <dx:ASPxPopupControl ID="dxppFileManager" 
        ClientInstanceName="ppFileManager" runat="server" AllowDragging="True" 
        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
         <ClientSideEvents CloseUp="onUploadCloseUp" />
        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <Windows>
            <dx:PopupWindow ContentUrl="../Popupcontrol/order_upload_manager.aspx" 
                 Name="ppFiles" CloseAction="CloseButton" 
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

