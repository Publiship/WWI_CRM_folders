<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="container.aspx.cs" Inherits="container"  %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

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

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onWarehouseSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblWarehouseAddress.SetText(s1);
        }
        
        function onOrderMenuItemCick(s, e) {
            if (e.item.name == "cmdAllocateOrder") {
                showAllocationWindow();
            }
        }

        function beginSave(s, e) {
            cbpOrder.PerformCallback('save');
            hideAllocationWindow();
        }
        
        function showAllocationWindow() {
            var window1 = ppcContainer.GetWindowByName('ppcAllocateOrder');
            ppcContainer.ShowWindow(window1);
        }

        function hideAllocationWindow() {
            //popup.Hide();
            window.ppcContainer.HideWindow(window.ppcContainer.GetWindowByName('ppcAllocateOrder'));
            //refesh allocation grid
            if (!grdContainerOrders.InCallback()) {
                grdContainerOrders.PerformCallback(' ');
            }
        }

        function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }

        var postponedCallbackRequired = false;
        function OnOrderEntered(s, e) {
            if (cbpOrder.InCallback())
                postponedCallbackRequired = true;
            else
                cbpOrder.PerformCallback();
        }
        function OnEndCallback(s, e) {
            if (postponedCallbackRequired) {
                cbpOrder.PerformCallback();
                postponedCallbackRequired = false;
            }
        }

        function onVoyageChanged(cbvoyage) {
            var svalue = cbvoyage.GetValue();
            cboOriginPort.SetSelectedIndex(-1);
            cboDestPort.SetSelectedIndex(-1);
            cboOriginPort.PerformCallback(svalue);
            cboDestPort.PerformCallback(svalue);
        }
      

        function OnDeliveredValueChanged(s, e) {
            //var checkState = ckPalletised.GetCheckState();
            var checked = s.GetChecked();
            //checkStateLabel.SetText("CheckState = " + checkState);
            //checkedLabel.SetText("Checked = " + checked);
            if (checked == true) {
                var d = new Date();
                dtDeliveryDate.SetDate(d);
            }
            else {
                dtDeliveryDate.SetDate(null);
            }
        }

        function OnDevannedValueChanged(s, e) {
            //var checkState = ckPalletised.GetCheckState();
            var checked = s.GetChecked();
            //checkStateLabel.SetText("CheckState = " + checkState);
            //checkedLabel.SetText("Checked = " + checked);
            if (checked == true) {
                var d = new Date();
                dtDevanDate.SetDate(d);
            }
            else {
                dtDevanDate.SetDate(null);
            }
        }

        function SetMaxLength(memo, maxLength) {
            if (!memo)
                return;
            if (typeof (maxLength) != "undefined" && maxLength >= 0) {
                memo.maxLength = maxLength;
                memo.maxLengthTimerToken = window.setInterval(function() {
                    var text = memo.GetText();
                    if (text && text.length > memo.maxLength)
                        memo.SetText(text.substr(0, memo.maxLength));
                }, 10);
            } else if (memo.maxLengthTimerToken) {
                window.clearInterval(memo.maxLengthTimerToken);
                delete memo.maxLengthTimerToken;
                delete memo.maxLength;
            }

        }

        //****** not in use - we might as well post back ******
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }
        
        function StartNew() {
            editFormMenu.GetItemByName("miInsert").SetEnabled(true);
            editFormMenu.GetItemByName("miCancel").SetEnabled(true);
            editFormMenu.GetItemByName("miNew").SetEnabled(false);
            editFormMenu.GetItemByName("miEdit").SetEnabled(false);
            editFormMenu.GetItemByName("miDelete").SetEnabled(false);
            editFormMenu.GetItemByName("miUpdate").SetEnabled(false);
        }

        function OnMenuItemClick(s, e) {
            switch (e.item.name) {
                case "miUpdate": grid.UpdateEdit(); break;
                case "miEdit":
                    StartEdit();
                    break;
                case "miNew":
                    StartNew(); break;
                //grid.AddNewRow(); break;   
                case "miDelete":
                    if (confirm('Are you sure to delete this record?'))
                        grid.DeleteRow(grid.GetTopVisibleIndex());
                    break;
                case "miRefresh":
                case "miCancel": grid.Refresh(); break;
            }
        }
        //********************
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
          
       <div class="grid_16">
            <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/shipment_containers/container_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblContainerDetails" runat="server" 
                             ClientInstanceName="lblContainerDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Container information |">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
                        <dx:ASPxLabel ID="dxlblStatus" runat="server" 
                            ClientInstanceName="lblStatus" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Container status" >
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- tabs -->
        <div class="clear"></div>
        <!-- panels for mark loaded and move container -->
        <!-- view form 2 columns -->
        <div class="grid_16"> 
            <asp:FormView ID="fmvContainer" runat="server" Width="100%" 
                ondatabound="fmvContainer_DataBound" Height="59px" 
                onmodechanging="fmvContainer_ModeChanging">
                <EditItemTemplate>
                    <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption16" />
                          <col />
                          <col class="caption12" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblContainerNo" runat="server" 
                                       ClientInstanceName="lblContainerNo" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Container number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxTextBox ID="dxtxtContainerNo" runat="server" ClientInstanceName="txtContainerNo" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      Width="245px" Text='<%# Bind("ContainerNumber") %>'>
                                  </dx:ASPxTextBox>
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblStatusEdit" runat="server" 
                                     ClientInstanceName="lblStatusEdit" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Container status">
                                 </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboContainerStatus" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboContainerStatus" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="ContainerStatus" Value='<%# Bind("ContainerStatusID") %>' 
                                    ValueField="ContainerStatusID" ValueType="System.Int32" Width="120px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                              <td>
                                  <dx:ASPxLabel ID="dxlblContainerType" runat="server" 
                                      ClientInstanceName="lblContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Container type">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboContainerType" runat="server" CallbackPageSize="15" 
                                      ClientInstanceName="cboContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="ContainerType" Value='<%# Bind("SizeTypeID") %>' 
                                      ValueField="ContainerSizeID" ValueType="System.Int32" Width="210px">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                  </dx:ASPxComboBox>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                              </td>
                        </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyage" runat="server" 
                                 ClientInstanceName="lblVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Voyage">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                                <!-- dxcboVoyage_ItemRequestedByValue and dxcboVoyage_ItemsRequestedByFilterCondition disabled until we change
                                     voyagetable case sensitivity from CS to CI as itemrequested events are CS because of it -->
                                <dx:ASPxComboBox ID="dxcboVoyage" runat="server" CallbackPageSize="25" 
                                 ClientInstanceName="cboVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Joined" Value='<%# Bind("VoyageID") %>' ValueField="VoyageID" 
                                 ValueType="System.Int32" Width="210px" 
                                    onitemrequestedbyvalue="dxcboVoyage_ItemRequestedByValue" 
                                    onitemsrequestedbyfiltercondition="dxcboVoyage_ItemsRequestedByFilterCondition">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents SelectedIndexChanged="function(s, e) { onVoyageChanged(s); }" />
                              </dx:ASPxComboBox> 
                            </td>
                            <td></td>
                            <td></td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                 ClientInstanceName="lblOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxComboBox ID="dxcboOriginPort" runat="server" CallbackPageSize="15" 
                                 ClientInstanceName="cboOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("OriginPortID") %>' ValueField="PortID" 
                                 ValueType="System.Int32" Width="210px" oncallback="dxcboOriginPort_Callback">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                              </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                    ClientInstanceName="lblOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at origin">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboOriginController" runat="server" 
                                    CallbackPageSize="15" ClientInstanceName="cboOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("OriginControllerID") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="210px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                 ClientInstanceName="lblDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Destination port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboDestPort" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboDestPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="PortName" Value='<%# Bind("DestinationPortID") %>' ValueField="PortID" 
                                    ValueType="System.Int32" Width="210px" oncallback="dxcbDestPort_Callback">
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                </dx:ASPxComboBox>
                              </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDestController" runat="server" 
                                    ClientInstanceName="lblDestController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at destination">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboDestController" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboDestController"  
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("DestinationControllerID") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="210px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                            <dx:ASPxLabel ID="dxlblTerms" runat="server" 
                                    ClientInstanceName="lblTerms" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Terms">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                             <dx:ASPxComboBox ID="dxcboTerms" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboTerms"
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("CYCFS") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="120px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDevan" runat="server" 
                                    ClientInstanceName="lblDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Devan notes">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxMemo ID="dxmemDevan" runat="server" ClientInstanceName="memDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Height="71px" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Width="210px" Text='<%# Bind("DevanNotes") %>'>
                                </dx:ASPxMemo>
                            </td>
                          </tr> 
                         <tr class="row_divider">
                             <td>
                                 <dx:ASPxLabel ID="dxlblWarehouse" runat="server" 
                                     ClientInstanceName="lblWarehouse" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Devan warehouse delivered">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 <dx:ASPxComboBox ID="dxcboWarehouse" runat="server" CallbackPageSize="15" 
                                     ClientInstanceName="cboWarehouse" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                     onitemrequestedbyvalue="dxcboWarehouse_ItemRequestedByValue" 
                                     onitemsrequestedbyfiltercondition="dxcboWarehouse_ItemsRequestedByFilterCondition" 
                                     Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                     TextField="CompanyName" Value='<%# Bind("DevanWarehouseID") %>' 
                                     ValueField="DevanWarehouseID" ValueType="System.Int32" Width="210px">
                                     <ButtonStyle Width="13px">
                                     </ButtonStyle>
                                     <LoadingPanelStyle ImageSpacing="5px">
                                     </LoadingPanelStyle>
                                     <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                     </LoadingPanelImage>
                                     <ClientSideEvents SelectedIndexChanged="onWarehouseSelected" />
                                     <Columns>
                                         <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                             Name="colCompanyID" Visible="false" />
                                         <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colCompanyNanme" 
                                             Width="190px" />
                                         <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" Name="colAddress1" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" Name="colAddress2" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" Name="colAddress3" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Country" FieldName="CountryName" Name="colCountry" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colPhone" 
                                             Width="100px" />
                                         <dx:ListBoxColumn Caption="Customer(Hidden)" FieldName="Customer" 
                                             Name="colCustomer" Visible="False" />
                                     </Columns>
                                 </dx:ASPxComboBox>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                             <td>
                                 &nbsp;</td>
                             <td rowspan="3">
                                 <dx:ASPxLabel ID="dxlblWarehouseAddress" runat="server" 
                                     ClientInstanceName="lblWarehouseAddress" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                            <td>
                                  &nbsp;</td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckDelivered" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDelivered" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Delivered" 
                                    TextAlign="Left" Value='<%# Bind("Delivered") %>' Width="100%">
                                    <ClientSideEvents Init="OnDeliveredValueChanged" ValueChanged="OnDeliveredValueChanged" />
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtDeliveryDate" runat="server" 
                                    ClientInstanceName="dtDeliveryDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("DeliveryDate") %>' Width="125px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                         </tr> 
                         <tr>
                            <td></td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckDevanned" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDevanned" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Devanned" 
                                    TextAlign="Left" Value='<%# Bind("Devanned") %>' Width="100%">
                                    <ClientSideEvents Init="OnDevannedValueChanged" ValueChanged="OnDevannedValueChanged" />
                                </dx:ASPxCheckBox>
                             </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtDevanDate" runat="server" 
                                    ClientInstanceName="dtDevanDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("DevanDate") %>' Width="125px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                             </td>
                         </tr> 
                      </table>      
                </EditItemTemplate>
                <InsertItemTemplate>
                     <table id="tbl" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption16" />
                          <col />
                          <col class="caption12" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblContainerNo" runat="server" 
                                       ClientInstanceName="lblContainerNo" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Container number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxTextBox ID="dxtxtContainerNo" runat="server" ClientInstanceName="txtContainerNo" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      Width="245px" Text='<%# Bind("ContainerNumber") %>'>
                                  </dx:ASPxTextBox>
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblStatusInsert" runat="server" 
                                     ClientInstanceName="lblStatusInsert" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Container status">
                                 </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboContainerStatus" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboContainerStatus" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="ContainerStatus" Value='<%# Bind("ContainerStatusID") %>' 
                                    ValueField="ContainerStatusID" ValueType="System.Int32" Width="120px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                              <td>
                                  <dx:ASPxLabel ID="dxlblContainerType" runat="server" 
                                      ClientInstanceName="lblContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Container type">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboContainerType" runat="server" CallbackPageSize="15" 
                                      ClientInstanceName="cboContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="ContainerType" Value='<%# Bind("SizeTypeID") %>' 
                                      ValueField="ContainerSizeID" ValueType="System.Int32" Width="210px">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                  </dx:ASPxComboBox>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                              </td>
                         </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyage" runat="server" 
                                 ClientInstanceName="lblVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Voyage">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboVoyage" runat="server" CallbackPageSize="25" 
                                 ClientInstanceName="cboVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Joined" Value='<%# Bind("VoyageID") %>' ValueField="VoyageID" 
                                 ValueType="System.Int32" Width="210px" 
                                    onitemrequestedbyvalue="dxcboVoyage_ItemRequestedByValue" 
                                    onitemsrequestedbyfiltercondition="dxcboVoyage_ItemsRequestedByFilterCondition">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents SelectedIndexChanged="function(s, e) { onVoyageChanged(s); }" />
                              </dx:ASPxComboBox>
                            </td>
                            <td></td>
                            <td></td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                 ClientInstanceName="lblOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxComboBox ID="dxcboOriginPort" runat="server" CallbackPageSize="15" 
                                 ClientInstanceName="cboOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("OriginPortID") %>' ValueField="PortID" 
                                 ValueType="System.Int32" Width="210px" oncallback="dxcboOriginPort_Callback">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                              </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                    ClientInstanceName="lblOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at origin">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboOriginController" runat="server" 
                                    CallbackPageSize="15" ClientInstanceName="cboOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("OriginControllerID") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="210px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                 ClientInstanceName="lblDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Destination port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboDestPort" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboDestPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="PortName" Value='<%# Bind("DestinationPortID") %>' ValueField="PortID" 
                                    ValueType="System.Int32" Width="210px" oncallback="dxcbDestPort_Callback">
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                </dx:ASPxComboBox>
                              </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDestController" runat="server" 
                                    ClientInstanceName="lblDestController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at destination">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboDestController" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboDestController"  
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("DestinationControllerID") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="210px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                            <dx:ASPxLabel ID="dxlblTerms" runat="server" 
                                    ClientInstanceName="lblTerms" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Terms">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                             <dx:ASPxComboBox ID="dxcboTerms" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboTerms"
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Name" Value='<%# Bind("CYCFS") %>' 
                                    ValueField="EmployeeID" ValueType="System.Int32" Width="120px">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDevan" runat="server" 
                                    ClientInstanceName="lblDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Devan notes">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxMemo ID="dxmemDevan" runat="server" ClientInstanceName="memDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Height="71px" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Width="210px" Text='<%# Bind("DevanNotes") %>'>
                                </dx:ASPxMemo>
                            </td>
                          </tr> 
                         <tr class="row_divider">
                             <td>
                                 <dx:ASPxLabel ID="dxlblWarehouse" runat="server" 
                                     ClientInstanceName="lblWarehouse" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Devan warehouse delivered">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 <dx:ASPxComboBox ID="dxcboWarehouse" runat="server" CallbackPageSize="15" 
                                     ClientInstanceName="cboWarehouse" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                     onitemrequestedbyvalue="dxcboWarehouse_ItemRequestedByValue" 
                                     onitemsrequestedbyfiltercondition="dxcboWarehouse_ItemsRequestedByFilterCondition" 
                                     Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                     TextField="CompanyName" Value='<%# Bind("DevanWarehouseID") %>' 
                                     ValueField="DevanWarehouseID" ValueType="System.Int32" Width="210px">
                                     <ButtonStyle Width="13px">
                                     </ButtonStyle>
                                     <LoadingPanelStyle ImageSpacing="5px">
                                     </LoadingPanelStyle>
                                     <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                     </LoadingPanelImage>
                                     <ClientSideEvents SelectedIndexChanged="onWarehouseSelected" />
                                     <Columns>
                                         <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                             Name="colCompanyID" Visible="false" />
                                         <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colCompanyNanme" 
                                             Width="190px" />
                                         <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" Name="colAddress1" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" Name="colAddress2" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" Name="colAddress3" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Country" FieldName="CountryName" Name="colCountry" 
                                             Width="150px" />
                                         <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colPhone" 
                                             Width="100px" />
                                         <dx:ListBoxColumn Caption="Customer(Hidden)" FieldName="Customer" 
                                             Name="colCustomer" Visible="False" />
                                     </Columns>
                                 </dx:ASPxComboBox>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                             <td>
                                 &nbsp;</td>
                             <td rowspan="3">
                                 <dx:ASPxLabel ID="dxlblWarehouseAddress" runat="server" 
                                     ClientInstanceName="lblWarehouseAddress" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                            <td>
                                  &nbsp;</td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckDelivered" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDelivered" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Delivered" 
                                    TextAlign="Left" Value='<%# Bind("Delivered") %>' Width="100%">
                                    <ClientSideEvents Init="OnDeliveredValueChanged" ValueChanged="OnDeliveredValueChanged" />
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtDeliveryDate" runat="server" 
                                    ClientInstanceName="dtDeliveryDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("DeliveryDate") %>' Width="125px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                         </tr> 
                         <tr>
                            <td></td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckDevanned" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDevanned" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Devanned" 
                                    TextAlign="Left" Value='<%# Bind("Devanned") %>' Width="100%">
                                    <ClientSideEvents Init="OnDevannedValueChanged" ValueChanged="OnDevannedValueChanged" />
                                </dx:ASPxCheckBox>
                             </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtDevanDate" runat="server" 
                                    ClientInstanceName="dtDevanDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("DevanDate") %>' Width="125px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                             </td>
                         </tr> 
                      </table>      
                </InsertItemTemplate>
                <ItemTemplate>
                    <table id="tblView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption16" />
                          <col />
                          <col class="caption12" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblContainerNo" runat="server" 
                                       ClientInstanceName="lblContainerNo" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Container number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
					         <dx:ASPxLabel ID="dxlblViewContainerNo" runat="server" 
                                       ClientInstanceName="lblViewContainerNo" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text='<%# Bind("ContainerNumber") %>'>
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblStatusView" runat="server" 
                                     ClientInstanceName="lblStatusView" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Container status">
                                 </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblviewContainerStatus" runat="server" 
                                    ClientInstanceName="lblviewContainerStatus" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                              <td>
                                  <dx:ASPxLabel ID="dxlblContainerType" runat="server" 
                                      ClientInstanceName="lblContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Container type">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblViewContainerType" runat="server" 
                                      ClientInstanceName="lblviewContainerType" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                              </td>
                        </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblVoyage" runat="server" 
                                 ClientInstanceName="lblVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Voyage">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxLabel ID="dxlblViewVoyage" runat="server" 
                                ClientInstanceName="lblViewVoyage" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="">
                             </dx:ASPxLabel>
                           </td>
                            <td></td>
                            <td></td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                 ClientInstanceName="lblOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                               <dx:ASPxLabel ID="dxlblViewOriginPort" runat="server" 
                                 ClientInstanceName="lblViewOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="">
                             </dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                    ClientInstanceName="lblOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at origin">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblViewOriginController" runat="server" 
                                    ClientInstanceName="lblviewOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="">
                                </dx:ASPxLabel>

                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                 ClientInstanceName="lblDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Destination port">
                             </dx:ASPxLabel>
                            </td>
                            <td>
                                    <dx:ASPxLabel ID="dxlblViewDestPort" runat="server" 
                                 ClientInstanceName="lblViewDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="">
                             </dx:ASPxLabel>

                              </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDestController" runat="server" 
                                    ClientInstanceName="lblDestController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Controller at destination">
                                </dx:ASPxLabel>
                              </td>
                            <td>
                                  <dx:ASPxLabel ID="dxlblViewDestController" runat="server" 
                                    ClientInstanceName="lblViewDestController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="">
                                </dx:ASPxLabel>

                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                            <dx:ASPxLabel ID="dxlblTerms" runat="server" 
                                    ClientInstanceName="lblTerms" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Terms">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxLabel ID="dxlblViewTerms" runat="server" 
                                    ClientInstanceName="lblViewTerms" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="">
                                </dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDevan" runat="server" 
                                    ClientInstanceName="lblDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Devan notes">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblViewDevan" runat="server" 
                                    ClientInstanceName="lblViewDevan" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DevanNotes") %>'>
                                </dx:ASPxLabel>

                            </td>
                          </tr> 
                         <tr class="row_divider">
                             <td>
                                 <dx:ASPxLabel ID="dxlblWarehouse" runat="server" 
                                     ClientInstanceName="lblWarehouse" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="Devan warehouse delivered">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 <dx:ASPxLabel ID="dxlblViewWarehouseName" runat="server" 
                                     ClientInstanceName="lblViewWarehouseName" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                             <td>
                                 &nbsp;</td>
                             <td rowspan="3">
                                 <dx:ASPxLabel ID="dxlblViewWarehouseAddress" runat="server" 
                                     ClientInstanceName="lblViewWarehouseAddress" 
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" Text="">
                                 </dx:ASPxLabel>
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr class="row_divider">
                            <td>
                                  &nbsp;</td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckViewDelivered" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="View" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Delivered" 
                                    TextAlign="Left" Value='<%# Bind("Delivered") %>' Width="100%" ReadOnly="True">
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
					            <dx:ASPxLabel ID="dxlblViewDeliveryDate" runat="server" 
                                    ClientInstanceName="lblViewDeliveryDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DeliveryDate") %>'>
                                </dx:ASPxLabel>
                            </td>
                         </tr> 
                         <tr>
                            <td></td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckDevanned" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDevanned" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Devanned" 
                                    TextAlign="Left" Value='<%# Bind("Devanned") %>' Width="100%" ReadOnly="True">
                                </dx:ASPxCheckBox>
                             </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblViewDevanDate" runat="server" 
                                    ClientInstanceName="lblViewDevanDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DevanDate") %>'>
                                </dx:ASPxLabel>
                             </td>
                         </tr> 
                      </table>   
                </ItemTemplate>   
            </asp:FormView>
        </div>       
        <!-- custom command buttons for formview -->
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="clear"></div>
         <!-- commands for formview -->
            <div class="grid_13">
                 <dx:ASPxMenu ID="dxmnuContainer" runat="server" 
                            ClientInstanceName="mnuContainer" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                             AutoSeparators="RootOnly" ShowAsToolbar="True"
                             onitemclick="dxmnuContainer_ItemClick" OnItemDataBound="dxmnuContainer_DataBound">
                                        <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                            </dx:ASPxMenu>
            </div>
              <!-- custom command buttons for grid -->
            <div class="grid_3">
                        <dx:ASPxMenu ID="dxmnuOrder" runat="server" 
                            ClientInstanceName="mnuOrder" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                             AutoSeparators="RootOnly" ShowAsToolbar="True" HorizontalAlign="Right">
                                        <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ClientSideEvents ItemClick="onOrderMenuItemCick" />
                                        <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                            </dx:ASPxMenu>
               </div>
        <div class="clear"></div>   
               <!--end menu for grid -->
        <!-- gridview for associated orders -->
        <div class="grid_16 pad_bottom">
                <dx:ASPxGridView ID="dxgrdContainerOrders" runat="server" 
                        AutoGenerateColumns="False" ClientInstanceName="grdContainerOrders" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" KeyFieldName="ContainerSubID" Width="100%" 
                    onrowdeleting="dxgrdContainerOrders_RowDeleting" 
                    onafterperformcallback="dxgrdContainerOrders_AfterPerformCallback">
                        <SettingsBehavior ConfirmDelete="True" EnableRowHotTrack="True" />
                        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="5px">
                            </LoadingPanel>
                        </Styles>
                        <Settings ShowFooter="true" VerticalScrollableHeight="150" /> 
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
                        <SettingsEditing Mode="PopupEditForm" 
                            PopupEditFormHorizontalAlign="WindowCenter" 
                            PopupEditFormVerticalAlign="WindowCenter" />
                        <SettingsText Title="Orders for this container" />
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ButtonType="Image">
                                <DeleteButton Text="Remove" Visible="True">
                                    <Image AlternateText="Remove" ToolTip="Remove" Height="18px" 
                                        Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                     </Image>
                                </DeleteButton>
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Order #" FieldName="OrderNumber" 
                                Name="colOrderNumber" ReadOnly="True" VisibleIndex="1" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Packages" FieldName="Packages" 
                                Name="colPackages" VisibleIndex="2" Width="100px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Package type" FieldName="PackageType" 
                                Name="colPackageType" VisibleIndex="3" Width="150px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Weight" FieldName="Weight" Name="colWeight" 
                                VisibleIndex="4" Width="100px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Cbm" FieldName="Cbm" Name="colCbm" 
                                VisibleIndex="5" Width="100px">
                                <EditFormSettings Visible="False" />
                                <PropertiesTextEdit DisplayFormatString="n2"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <StylesPager>
                            <PageNumber ForeColor="#3E4846">
                            </PageNumber>
                            <Summary ForeColor="#1E395B">
                            </Summary>
                        </StylesPager>
                        <Settings ShowGroupButtons="False" ShowHorizontalScrollBar="True" 
                            ShowTitlePanel="True" ShowVerticalScrollBar="True" />
                        <StylesEditors ButtonEditCellSpacing="0">
                            <ProgressBar Height="21px">
                            </ProgressBar>
                        </StylesEditors>
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="Cbm" SummaryType="Sum" />
                        </TotalSummary>
                    </dx:ASPxGridView>
                    <!-- end orders grid -->
        </div> 
        <!-- end grid -->
        <div class="clear"></div>
        <!-- hidden field -->
        <div class="grid_8">
        <dx:ASPxHiddenField ID="dxhfContainer" runat="server" 
                ClientInstanceName="hfContainer">
            </dx:ASPxHiddenField>
        </div>
        <!-- linq -->
        <div class="grid_8">
            <asp:LinqDataSource ID="linqVoyage" runat="server" 
                ContextTypeName="linq.linq_voyageDataContext" Select="new (VoyageID, Joined)" 
                TableName="VoyageTables">
            </asp:LinqDataSource>
        </div> 
        <!-- end linq --> 
        <div class="clear"></div> 
        <div class="grid_6"></div>
        <div class="grid_10">
        <!-- popup control for appending order to container -->
        <dx:ASPxPopupControl ID="dxppcContainer" runat="server" ClientInstanceName="ppcContainer" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" EnableHotTrack="False" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
            Height="410px" Width="560px" PopupHorizontalAlign="WindowCenter" Modal="true"  
                PopupVerticalAlign="WindowCenter">
                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanelImage>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl0" runat="server"></dx:PopupControlContentControl>
                    </ContentCollection>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
                <Windows>
                    <dx:PopupWindow Modal="true"  Name="ppcAllocateOrder" ShowCloseButton="True"
                        ShowFooter="False" ShowHeader="True" HeaderText="Allocate to container">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <table id="tblAppendOrder" cellpadding="5px" border="0" width="100%">
                                    <tr>
                                        <td colspan="3">
                                            <dx:ASPxLabel ID="dxlblInstruction" runat="server" 
                                                ClientInstanceName="lblInstruction" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Text="Enter Order Number and click Search">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="dxlblOrderNo" runat="server" ClientInstanceName="lblOrderNo" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Text="Order Number">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="dxtxtOrderNo" runat="server" Enabled="true" ClientEnabled="true" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="120px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="dxbtnOrderNo" runat="server" AutoPostBack="False" 
                                                CausesValidation="False" ClientInstanceName="btnOrderNo" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" HorizontalAlign="Left" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Search" 
                                                VerticalAlign="Middle">
                                                <ClientSideEvents Click="OnOrderEntered" />
                                                <Image AlternateText="Search" Height="26px" 
                                                    Url="~/Images/icons/metro/search.png" Width="26px">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                     <td colspan="3">
                                         <dx:ASPxCallbackPanel ID="dxcbpOrder" runat="server" 
                                             ClientInstanceName="cbpOrder" 
                                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                             CssPostfix="Office2010Blue" Width="100%" OnCallback="dxcbpOrder_Callback">
                                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                             </LoadingPanelImage>
                                             <LoadingPanelStyle ImageSpacing="5px">
                                             </LoadingPanelStyle>
                                             <PanelCollection>
                                                 <dx:PanelContent ID="pncOrder" runat="server">
                                                     <dx:ASPxPanel ID="dxpnlAlert" ClientInstanceName="pnlAlert" runat="server" ClientVisible="false"  
                                                         Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent>
                                                                <div class="alert">
                                                                    <dx:ASPxLabel ID="dxlblAlert" ClientInstanceName="lblAlert" runat="server" 
                                                                        Text="" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                        CssPostfix="Office2010Blue">
                                                                    </dx:ASPxLabel>
                                                                </div> 
                                                            </dx:PanelContent> 
                                                        </PanelCollection> 
                                                     </dx:ASPxPanel>
                                                     <dx:ASPxPanel ID="dxpnlOrder"  ClientInstanceName="pnlOrder" runat="server" 
                                                         Width="100%" Height="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent>
                                                                <table id ="tblAllocate" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                                                    <colgroup>
                                                                         <col class="caption10" />
                                                                         <col />
                                                                         <col class="caption10" /> 
                                                                         <col />
                                                                    </colgroup> 
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAvailable1" ClientInstanceName="lblOrderAvailable1" runat="server" 
                                                                                Text="Order Number" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAvailable2" runat="server" 
                                                                                ClientInstanceName="lblOrderAvailable2" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAllocate1" runat="server" 
                                                                                ClientInstanceName="lblOrderAllocate1" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Container">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAllocate2" runat="server" 
                                                                                ClientInstanceName="lblOrderAllocate2" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAvailable3" runat="server" 
                                                                                ClientInstanceName="lblOrderAvailable2" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Available">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderAllocate3" runat="server" 
                                                                                ClientInstanceName="lblOrderAllocate3" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="How much to allocate?">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="row_divider">
                                                                        <td><dx:ASPxLabel ID="dxlblOrderKgs" ClientInstanceName="lblOrderKgs" runat="server" 
                                                                                Text="Kgs" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderWeight" runat="server" 
                                                                                ClientInstanceName="lblOrderWeight" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderKgs0" runat="server" 
                                                                                ClientInstanceName="lblOrderKgs" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Kgs">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="dxtxtAllocateWeight" runat="server" 
                                                                                ClientInstanceName="txtAllocateWeight" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="110px">
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                    </tr>    
                                                                     <tr class="row_divider">
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderM3" runat="server" ClientInstanceName="lblOrderM3" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="m3">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td><dx:ASPxLabel ID="dxlblOrderCbm" ClientInstanceName="lblOrderCbm" runat="server" 
                                                                                Text="" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderM4" runat="server" ClientInstanceName="lblOrderM3" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="m3">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="dxtxtAllocateCbm" runat="server" 
                                                                                ClientInstanceName="txtAllocateCbm" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="110px">
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                    </tr>    
                                                                    <tr class="row_divider">
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderNumber" runat="server" 
                                                                                ClientInstanceName="lblOrderNumber" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Number">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderPackages" runat="server" 
                                                                                ClientInstanceName="lblOrderPackages" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderNumber0" runat="server" 
                                                                                ClientInstanceName="lblOrderNumber" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Number">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="dxtxtAllocatePackages" runat="server" 
                                                                                ClientInstanceName="txtAllocatePackages" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="110px">
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="row_divider">
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderPackageType" runat="server" 
                                                                                ClientInstanceName="lblOrderPackageType" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Package type">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderPackageTypeId" runat="server" 
                                                                                ClientInstanceName="lblOrderPackageTypeId" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="dxlblOrderPackageType0" runat="server" 
                                                                                ClientInstanceName="lblOrderPackageType" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Text="Package type">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxComboBox ID="dxcboAllocatePackageType" runat="server" 
                                                                                ClientInstanceName="cboAllocatePackageType" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" Spacing="0" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                                Width="135px">
                                                                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                                                </LoadingPanelImage>
                                                                                <LoadingPanelStyle ImageSpacing="5px">
                                                                                </LoadingPanelStyle>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table id ="tblCommands" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                                                    <colgroup>
                                                                         <col />
                                                                         <col />
                                                                         <col /> 
                                                                         <col />
                                                                    </colgroup> 
                                                                    <tr>
                                                                    <td>
                                                                    <dx:ASPxButton ID="dxbtnSave" runat="server" ClientInstanceName="btnSave" 
                                                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                                CssPostfix="Office2010Blue" 
                                                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                                Text="Save" HorizontalAlign="Left" 
                                                                            AutoPostBack="False" CausesValidation="False">
                                                                            <ClientSideEvents Click="beginSave" />
                                                                            <Image Height="26px" ToolTip="Save" Url="~/Images/icons/metro/save.png" 
                                                                                Width="26px">
                                                                            </Image>
                                                                            </dx:ASPxButton> 
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="dxbtnCancel" runat="server" ClientInstanceName="btnCancel" 
                                                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                                            CssPostfix="Office2010Blue" 
                                                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                                            Text="Cancel" AutoPostBack="False" CausesValidation="False" 
                                                                            HorizontalAlign="Left">
                                                                            <ClientSideEvents Click="hideAllocationWindow" />
                                                                            <Image Height="26px" Url="~/Images/icons/metro/cancel.png" Width="26px">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                    
                                                                        <dx:ASPxHiddenField ID="dxhfOrderId" runat="server" 
                                                                            ClientInstanceName="hfOrderId">
                                                                        </dx:ASPxHiddenField>
                                                                    
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    </tr> 
                                                                </table> 
                                                                
                                                                <div class="row">
                                                                     <div class="left">
                                                                      
                                                                     </div>  
                                                                     <div class="left">
                                                                      
                                                                     </div>
                                                                     <div class="right">
                                                                      
                                                                     </div> 
                                                                </div>
                                                            </dx:PanelContent> 
                                                        </PanelCollection> 
                                                     </dx:ASPxPanel>
                                                 </dx:PanelContent>
                                             </PanelCollection>
                                         </dx:ASPxCallbackPanel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                </Windows>
            </dx:ASPxPopupControl>

        <!-- end pop up -->
        </div> 
    </div><!-- end container -->
       
  
</asp:Content>


