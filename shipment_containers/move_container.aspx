<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="move_container.aspx.cs" Inherits="move_container" %>

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
        
        function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }

        function onNewVesselChanged(s) {
            var et = ''; 
            //ets to label
            et = s.GetSelectedItem().GetColumnText('ETS');
            lblNewETS.SetText(et);
            //eta to label
            et = s.GetSelectedItem().GetColumnText('ETA');
            lblNewETA.SetText(et);
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
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }

        //not in use - we might as well post back ******
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

        function onVesselClick(s, e) {
            //open vessel search form in new window
            //can pass values as iList<string> or concatenated using values.toString() method
            PageMethods.get_button_response('0', 'cmdVessel', onMethodComplete, onMethodFailed);
        }
        
        function onContainerClick(s, e) {
            //open container details form in new window
            //can pass values as iList<string> or concatenated using values.toString() method
            var id = hfContainer.Get("containerid");
            if (id != "") {
                PageMethods.get_button_response(id, 'cmdContainer', onMethodComplete, onMethodFailed);
           }
            else {
                alert('Container ID not found');
            }
        }

        function onMethodComplete(result) {
            if (result != "") {
                window.open(result, "_blank");
            }
            else {
                alert('PageMethods.get_secure_url() returned null');
            }
        }
        

        function onMethodFailed(result) {
            alert('PageMethods.get_secure_url() failed');
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
                    Text="Update vessel for container |">
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
        <div class="clear"></div>
        <!-- view form 2 columns -->
        <div class="grid_16 pad_bottom"> 
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
                                      IncrementalFilteringMode="StartsWith" 
                                      Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="ContainerType" ValueField="ContainerSizeID" ValueType="System.Int32" 
                                      Width="210px"  
                                    Value='<%# Bind("SizeTypeID") %>'>
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
                                 CssPostfix="Office2010Blue" DropDownWidth="260px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboVoyage_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboVoyage_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Joined" Value='<%# Bind("VoyageID") %>' ValueField="VoyageID" 
                                 ValueType="System.Int32" Width="250px">
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
                                      IncrementalFilteringMode="StartsWith" 
                                      Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="ContainerType" ValueField="ContainerSizeID" ValueType="System.Int32" 
                                      Width="210px"  
                                    Value='<%# Bind("SizeTypeID") %>'>
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
                                 CssPostfix="Office2010Blue" DropDownWidth="260px" EnableCallbackMode="True" 
                                 onitemrequestedbyvalue="dxcboVoyage_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboVoyage_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Joined" Value='<%# Bind("VoyageID") %>' ValueField="VoyageID" 
                                 ValueType="System.Int32" Width="250px" >
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
        <!-- move container -->
        <div class="grid_16">
         <table id="tblMoveContainer" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                           <colgroup>
                                <col class="caption16" />
                                <col />
                                <col />
                           </colgroup>
                           <tr>
                                <td>
                                    <dx:ASPxLabel ID="dxlblNewVessel" runat="server" 
                                        ClientInstanceName="lblNewVessel" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Select new vessel">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboNewVessel" runat="server" 
                                        ClientInstanceName="cboNewVessel" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Width="250px" onitemrequestedbyvalue="dxcboNewVessel_ItemRequestedByValue" 
                                        
                                        onitemsrequestedbyfiltercondition="dxcboNewVessel_ItemsRequestedByFilterCondition" 
                                        DropDownRows="15" DropDownWidth="410px">
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { onNewVesselChanged(s); }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Vessel name" FieldName="Joined" Name="colVesselName" 
                                                Width="180px" />
                                            <dx:ListBoxColumn Caption="ETS" FieldName="ETS" Name="colETS" Width="50px" />
                                            <dx:ListBoxColumn Caption="ETA" FieldName="ETA" Name="colETA" Width="50px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </td>
                                <td colspan="2" rowspan="2">
                                    <dx:ASPxLabel ID="dxlbllblCurrentDest" runat="server" 
                                        ClientInstanceName="lblCurrentDest" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        Text="if the vessel is not listed it is most likely because the port details in the voyage do not match those for this container " 
                                        Wrap="True">
                                    </dx:ASPxLabel>
                                </td>
                           </tr> 
                           <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                           </tr> 
                           <tr>
                                <td>
                                    <dx:ASPxLabel ID="dxlblRevisedETS" runat="server" 
                                        ClientInstanceName="lblRevisedETS" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Revised sailing date">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblNewETS" runat="server" ClientInstanceName="lblNewETS" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="dxbtnVessel" runat="server" 
                                           ClientInstanceName="btnVessel" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue"  
                                           SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Vessel details" Wrap="False" 
                                        AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="onVesselClick" />
                                       </dx:ASPxButton>
                                   </td>
                                <td>
                                    <dx:ASPxButton ID="dxbtnContainer" runat="server" 
                                           ClientInstanceName="btnContainer" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue"  
                                           SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Container details" Wrap="False" 
                                        AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="onContainerClick" />
                                        </dx:ASPxButton>
                                   </td>
                           </tr> 
                           <tr>
                                <td>
                                    <dx:ASPxLabel ID="dxlblRevisedETA" runat="server" 
                                        ClientInstanceName="lblRevisedETA" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Revised arrival date">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblNewETA" runat="server" ClientInstanceName="lblNewETA" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                           </tr> 
                               </table> 

            <div>
            <!-- commands for formview and move container -->
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
        </div>
        <!-- custom command buttons for formview -->
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="clear"></div>
        <!-- command menu -->
        <!-- gridview for associated orders -->
        <!-- end grid -->
        <!-- end pop up -->
        <div>
        <dx:ASPxHiddenField ID="dxhfContainer" runat="server" 
                ClientInstanceName="hfContainer">
            </dx:ASPxHiddenField>
        </div>
        <!-- end hiddenfield --> 
    </div><!-- end container -->
        
    
  
</asp:Content>



