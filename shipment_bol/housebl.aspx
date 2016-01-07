<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="housebl.aspx.cs" Inherits="housebl" %>

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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
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

        //*********
        //dll item changed functions
        function onVesselChanged(s, e) {
            cboOriginPort.SetSelectedIndex(-1);
            cboDestinationPort.SetSelectedIndex(-1);
            cboOriginPort.PerformCallback(s.GetValue().toString());
            cboDestinationPort.PerformCallback(s.GetValue().toString());
            //this is done when ports are changed
            //var d1 = s.GetSelectedItem().GetColumnText('ETS');
            //lblEtsSub.SetText(d1);
            //var d2 = s.GetSelectedItem().GetColumnText('ETA');
            //lblEtaSub.SetText(d2);       
        }
        
        function onDestinationAgentChanged(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblAgentAtDestinationIDSub.SetText(s1);
        }

        function onConsigneeChanged(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblConsigneeIDSub.SetText(s1);
        }

        function onOriginPortChanged(s, e) {
            //sub desck label
            var s1 = s.GetSelectedItem().GetColumnText('ETS');
            lblEtsSub.SetText(s1);
            //set in hidden field so we can pick it up on server side
            dxhfOrder.Set('ets', s1);
            //we don't need to do a callback for the destination port here
            //cboDestinationPort.SetSelectedIndex(-1);
            //cboDestinationPort.PerformCallback();
            //lblEtaSub.SetText('');
        }
        
        
        function onDestinationPortChanged(s, e) {
            //sub deck label
            var s1 = s.GetSelectedItem().GetColumnText('ETA');
            lblEtaSub.SetText(s1);
            //set in hidden field so we can pick it up on server side
            dxhfOrder.Set('eta', s1);
        }
        //*********

        //*********
        //dll end callback
        function onOriginPortEndCallBack(s, e) {
            //set to current item in dll after callback completed
            if (s.GetSelectedItem() != null) {
                var s1 = s.GetSelectedItem().GetColumnText('ETS');
                lblEtsSub.SetText(s1);
                //set in hidden field so we can pick it up on server side
                dxhfOrder.Set('ets', s1);
            }
        }
        
        function onDestinationPortEndCallBack(s, e) {
            //set to current item in dll after callback completed
            if (s.GetSelectedItem() != null) {
                var s1 = s.GetSelectedItem().GetColumnText('ETA');
                lblEtaSub.SetText(s1);
                //set in hidden field so we can pick it up on server side
                dxhfOrder.Set('eta', s1);
            }
        }
        //*********
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }

        //********
        //dll button clicks
        function onDestinationAgentButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblAgentAtDestinationIDSub.SetText('');
            }
        }

        function onConsigneeButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblConsigneeIDSub.SetText('');
            }
        }

        function onVesselButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                cboOriginPort.SetSelectedIndex(-1);
                cboDestinationPort.SetSelectedIndex(-1);
                //clear dlls until a new vessel is selected
                cboOriginPort.PerformCallback(-1);
                cboDestinationPort.PerformCallback(-1);
                //clear dates
                lblEtsSub.SetText('');
                lblEtaSub.SetText('');
            }
        }
        //*******
        
        //*******
        //functions to move items between listboxes
        function AddSelectedItems() {
            MoveSelectedItems(lstAvailable, lstSelected);
            UpdateButtonState();
        }
        function AddAllItems() {
            MoveAllItems(lstAvailable, lstSelected);
            UpdateButtonState();
        }
        function RemoveSelectedItems() {
            MoveSelectedItems(lstSelected, lstAvailable);
            UpdateButtonState();
        }
        function RemoveAllItems() {
            MoveAllItems(lstSelected, lstAvailable);
            UpdateButtonState();
        }
        function MoveSelectedItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            dstListBox.BeginUpdate();
            var items = srcListBox.GetSelectedItems();
            for (var i = items.length - 1; i >= 0; i = i - 1) {
                dstListBox.AddItem(items[i].text, items[i].value);
                srcListBox.RemoveItem(items[i].index);
            }
            //ensures the addded items are ticked by default
            //dstListBox.SelectItems(dstListBox.Items);
            //
            srcListBox.EndUpdate();
            dstListBox.EndUpdate();
        }
        function MoveAllItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            var count = srcListBox.GetItemCount();
            for (var i = 0; i < count; i++) {
                var item = srcListBox.GetItem(i);
                dstListBox.AddItem(item.text, item.value);
            }
            //ensures the addded items are ticked by default
            //dstListBox.SelectItems(dstListBox.Items);
            //
            srcListBox.EndUpdate();
            srcListBox.ClearItems();
        }
        function UpdateButtonState() {
            btnSelectAll.SetEnabled(lstAvailable.GetItemCount() > 0);
            btnUndoAll.SetEnabled(lstSelected.GetItemCount() > 0);
            btnSelect.SetEnabled(lstAvailable.GetSelectedItems().length > 0);
            btnUndo.SetEnabled(lstSelected.GetSelectedItems().length > 0);
        }
        //******

        //******
        //save orders/remove orders clicked
        var postponedCallbackRequired = false;
        function onSaveOrdersClick(s, e) {
            //use callbackpanel
            if (cbpOrderNumbers.InCallback())
                postponedCallbackRequired = true;
            else
                cbpOrderNumbers.PerformCallback(s.GetText());
            //lstSelected.PerformCallback('');
        }

        function onRemoveOrdersClick(s, e) {
            //use callbackpanel
            if (cbpOrderNumbers.InCallback())
                postponedCallbackRequired = true;
            else
                cbpOrderNumbers.PerformCallback(s.GetText());
            //lstSaved.PerformCallback('');
        }

        function OnEndCallback(s, e) {
            if (postponedCallbackRequired) {
                cbpOrderNumbers.PerformCallback();
                postponedCallbackRequired = false;
            }
        } 
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
                      ImageWidth="26px" NavigateUrl="~/shipment_bol/housebl_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblBolDetails" runat="server" 
                             ClientInstanceName="lblBolDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Bill of Lading information |">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
                        <dx:ASPxLabel ID="dxlblStatus" runat="server" 
                            ClientInstanceName="lblStatus" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="HBL number" >
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        
        <!-- tabs -->
        <div class="clear"></div>
        
        <!-- view form columns -->
        <div class="grid_16"> 
            <asp:FormView ID="fmvBol" runat="server" Width="100%" 
                ondatabound="fmvBOL_DataBound" Height="59px" 
                onmodechanging="fmvBol_ModeChanging">
                 <EditItemTemplate>
                    <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblHouseBLNumber" runat="server" 
                                       ClientInstanceName="lblHouseBLNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="House B/L number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxTextBox ID="dxtxtHouseBLNumber" runat="server" ClientInstanceName="txtHouseBLNumber" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      Width="210px" ClientEnabled="False" 
                                    MaxLength="50" Value='<%# Bind("HouseBLNumber") %>'>
                                  </dx:ASPxTextBox>
                            </td>
                            <td>
                            
                                <dx:ASPxLabel ID="dxlblHouseBLDate" runat="server" 
                                    ClientInstanceName="lblHouseBLDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="House B/L date">
                                </dx:ASPxLabel>
                            
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtHBLdate" runat="server" ClientInstanceName="dtHBLdate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("HBLdate") %>' Width="130px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAgentAtDestinationID" runat="server" 
                                       ClientInstanceName="lblAgentAtDestinationID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Agent at destination">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxComboBox ID="dxcboAgentAtDestinationID" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboAgentAtDestinationID"  
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="CompanyName" Value='<%# Bind("AgentAtDestinationID") %>' 
                                    ValueField="CompanyID" ValueType="System.Int32" Width="210px" 
                                       onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                       
                                       onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ClientSideEvents ButtonClick="onDestinationAgentButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onDestinationAgentChanged(s, e);}" />
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
                                    <Buttons>
                                     <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons>      
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblConsigneeID" runat="server" 
                                       ClientInstanceName="lblConsigneeID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Consignee">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboConsigneeID" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboConsigneeID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="CompanyName" Value='<%# Bind("ConsigneeID") %>' 
                                    ValueField="CompanyID" ValueType="System.Int32" Width="210px" 
                                    onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                    
                                    onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                     <ClientSideEvents ButtonClick="onConsigneeButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onConsigneeChanged(s, e);}" />
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
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                </dx:ASPxComboBox>
                            </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblVoyageID" runat="server" 
                                      ClientInstanceName="lblVoyageID" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Vessel">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="30" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    OnItemRequestedByValue="dxcboVesselID_ItemRequestedByValue" 
                                    OnItemsRequestedByFilterCondition="dxcboVesselID_ItemsRequestedByFilterCondition" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Joined" Value="<%# Bind('VoyageID') %>" ValueField="VoyageID" 
                                    ValueType="System.Int32" Width="210px" DropDownWidth="300px" >
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Vessel name" FieldName="Joined" Name="colVessel" 
                                            ToolTip="Vessel name" Width="170px" />
                                        <dx:ListBoxColumn Caption="ETS" FieldName="ETS" Name="colETS" ToolTip="ETS" 
                                            Width="80px" />
                                        <dx:ListBoxColumn Caption="ETA" FieldName="ETA" Name="colETA" ToolTip="ETA" 
                                            Width="80px" />
                                    </Columns>
                                    <ClientSideEvents ButtonClick="onVesselButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onVesselChanged(s, e);}" />
                                    </dx:ASPxComboBox>
                              </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblAgentAtDestinationIDSub" runat="server" 
                                    ClientInstanceName="lblAgentAtDestinationIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                            </td>
                            <td></td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblConsigneeIDSub" runat="server" 
                                    ClientInstanceName="lblConsigneeIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                              </td>
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
                                      CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="PortName" Value='<%# Bind("OriginPort") %>' 
                                      ValueField="PortID" ValueType="System.Int32" Width="210px" 
                                      oncallback="dxcboOriginPort_Callback">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                      <ClientSideEvents SelectedIndexChanged="onOriginPortChanged" 
                                          EndCallback="onOriginPortEndCallBack" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Port" FieldName="PortName" Name="colPortName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="ETS" FieldName="ETS" 
                                                Name="colETS" Width="100px" />
                                        </Columns>
                                  </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEts" runat="server" ClientInstanceName="lblEts" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Sailing">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                <dx:ASPxLabel ID="dxlblEtsSub" runat="server" ClientInstanceName="lblEtsSub" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Value='<%# Bind("ETS") %>'>
                                    </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDestinationPort" runat="server" 
                                      ClientInstanceName="lblDestinationPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Destination port">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboDestinationPort" runat="server" CallbackPageSize="15" 
                                      ClientInstanceName="cboDestinationPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" DropDownWidth="210px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="PortName" Value='<%# Bind("DestinationPort") %>' 
                                      ValueField="PortID" ValueType="System.Int32" Width="210px" 
                                      oncallback="dxcbDestPort_Callback">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                       <ClientSideEvents SelectedIndexChanged="onDestinationPortChanged" 
                                          EndCallback="onDestinationPortEndCallBack" />
                                      <Columns>
                                            <dx:ListBoxColumn Caption="Port" FieldName="PortName" Name="colPortName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="ETA" FieldName="ETA" 
                                                Name="colETA" Width="100px" />
                                        </Columns>
                                  </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEta" runat="server" ClientInstanceName="lblEta" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Arriving">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEtaSub" runat="server" ClientInstanceName="lblEtaSub" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("ETA") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                         <tr>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                      </table>  
                </EditItemTemplate>
                <ItemTemplate>
                    <table id="tblView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblHouseBLNumber" runat="server" 
                                       ClientInstanceName="lblHouseBLNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="House B/L number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblHouseBLNumberView" runat="server" 
                                    ClientInstanceName="lblHouseBLNumberView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("HouseBLNumber") %>'>
                                </dx:ASPxLabel>
                            </td>
                            <td>
                            
                                <dx:ASPxLabel ID="dxlblHouseBLDate" runat="server" 
                                    ClientInstanceName="lblHouseBLDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="House B/L date">
                                </dx:ASPxLabel>
                            
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblHouseBLDateView" runat="server" 
                                    ClientInstanceName="lblHouseBLDateView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("HBLDate") %>'>
                                </dx:ASPxLabel>
                            </td>
                              <td>
                                  <asp:HiddenField ID="hfVoyageID" runat="server" 
                                      Value='<%# Bind("VoyageID") %>' />
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAgentAtDestinationID" runat="server" 
                                       ClientInstanceName="lblAgentAtDestinationID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Agent at destination">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblAgentAtDestinationIDView" runat="server" 
                                       ClientInstanceName="lblAgentAtDestinationIDView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblConsigneeID" runat="server" 
                                       ClientInstanceName="lblConsigneeID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Consignee">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblConsigneeIDView" runat="server" 
                                    ClientInstanceName="lblConsigneeIDView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                            </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblVoyageID" runat="server" 
                                      ClientInstanceName="lblVoyageID" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Vessel">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblVoyageIDView" runat="server" 
                                      ClientInstanceName="lblVoyageIDView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue">
                                  </dx:ASPxLabel>
                              </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblAgentAtDestinationIDSub" runat="server" 
                                    ClientInstanceName="lblAgentAtDestinationIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                            </td>
                            <td>&nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblConsigneeIDSub" runat="server" 
                                    ClientInstanceName="lblConsigneeIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                      ClientInstanceName="lblOriginPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Origin port">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblOriginPortView" runat="server" 
                                      ClientInstanceName="lblOriginPortView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue">
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEts" runat="server" ClientInstanceName="lblEts" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Sailing">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEtsSub" runat="server" ClientInstanceName="lblEtsSub" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("ETS") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDestinationPort" runat="server" 
                                      ClientInstanceName="lblDestinationPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Destination port">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDestinationPortView" runat="server" 
                                      ClientInstanceName="lblDestinationPortView" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue">
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEta" runat="server" ClientInstanceName="lblEta" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Arriving">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEtaSub" runat="server" ClientInstanceName="lblEtaSub" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("ETA") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                         <tr>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                      </table>      
                </ItemTemplate>   
                <InsertItemTemplate>
                    <table id="tblInsert" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                      </colgroup>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblHouseBLNumber" runat="server" 
                                       ClientInstanceName="lblHouseBLNumber" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="House B/L number">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                              <dx:ASPxTextBox ID="dxtxtHouseBLNumber" runat="server" ClientInstanceName="txtHouseBLNumber" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      Width="210px" 
                                    MaxLength="50" Value='<%# Bind("HouseBLNumber") %>'>
                                  </dx:ASPxTextBox>
                            </td>
                            <td>
                            
                                <dx:ASPxLabel ID="dxlblHouseBLDate" runat="server" 
                                    ClientInstanceName="lblHouseBLDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="House B/L date">
                                </dx:ASPxLabel>
                            
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dxdtHBLdate" runat="server" ClientInstanceName="dtHBLdate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("HBLdate") %>' Width="130px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <CalendarProperties>
                                        <HeaderStyle Spacing="1px" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblAgentAtDestinationID" runat="server" 
                                       ClientInstanceName="lblAgentAtDestinationID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Agent at destination">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxComboBox ID="dxcboAgentAtDestinationID" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboAgentAtDestinationID"  
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="CompanyName" Value='<%# Bind("AgentAtDestinationID") %>' 
                                    ValueField="CompanyID" ValueType="System.Int32" Width="210px" 
                                       onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                       
                                       onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ClientSideEvents ButtonClick="onDestinationAgentButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onDestinationAgentChanged(s, e);}" />
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
                                    <Buttons>
                                     <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons>      
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblConsigneeID" runat="server" 
                                       ClientInstanceName="lblConsigneeID" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Consignee">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="dxcboConsigneeID" runat="server" CallbackPageSize="15" 
                                    ClientInstanceName="cboConsigneeID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                    IncrementalFilteringMode="StartsWith" Spacing="0" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="CompanyName" Value='<%# Bind("ConsigneeID") %>' 
                                    ValueField="CompanyID" ValueType="System.Int32" Width="210px" 
                                    onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                    
                                    onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition">
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                     <ClientSideEvents ButtonClick="onConsigneeButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onConsigneeChanged(s, e);}" />
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
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                </dx:ASPxComboBox>
                            </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblVoyageID" runat="server" 
                                      ClientInstanceName="lblVoyageID" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Vessel">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboVesselID" runat="server" CallbackPageSize="30" 
                                    ClientInstanceName="cboVesselID" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                     IncrementalFilteringMode="StartsWith" 
                                    OnItemRequestedByValue="dxcboVesselID_ItemRequestedByValue" 
                                    OnItemsRequestedByFilterCondition="dxcboVesselID_ItemsRequestedByFilterCondition" 
                                    Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    TextField="Joined" Value="<%# Bind('VoyageID') %>" ValueField="VoyageID" 
                                    ValueType="System.Int32" Width="210px" DropDownWidth="300px" >
                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                    </LoadingPanelImage>
                                    <LoadingPanelStyle ImageSpacing="5px">
                                    </LoadingPanelStyle>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Vessel name" FieldName="Joined" Name="colVessel" 
                                            ToolTip="Vessel name" Width="170px" />
                                        <dx:ListBoxColumn Caption="ETS" FieldName="ETS" Name="colETS" ToolTip="ETS" 
                                            Width="80px" />
                                        <dx:ListBoxColumn Caption="ETA" FieldName="ETA" Name="colETA" ToolTip="ETA" 
                                            Width="80px" />
                                    </Columns>
                                    <ClientSideEvents ButtonClick="onVesselButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onVesselChanged(s, e);}" />
                                    </dx:ASPxComboBox>
                              </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblAgentAtDestinationIDSub" runat="server" 
                                    ClientInstanceName="lblAgentAtDestinationIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                            </td>
                            <td></td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblConsigneeIDSub" runat="server" 
                                    ClientInstanceName="lblConsigneeIDSub" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                              </td>
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
                                      CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="PortName" Value='<%# Bind("OriginPort") %>' 
                                      ValueField="PortID" ValueType="System.Int32" Width="210px" 
                                      oncallback="dxcboOriginPort_Callback">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                      <ClientSideEvents SelectedIndexChanged="onOriginPortChanged" 
                                          EndCallback="onOriginPortEndCallBack" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Port" FieldName="PortName" Name="colPortName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="ETS" FieldName="ETS" 
                                                Name="colETS" Width="100px" />
                                        </Columns>
                                  </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEts" runat="server" ClientInstanceName="lblEts" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Sailing">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEtsSub" runat="server" ClientInstanceName="lblEtsSub" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("ETS") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblDestinationPort" runat="server" 
                                      ClientInstanceName="lblDestinationPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Destination port">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxComboBox ID="dxcboDestinationPort" runat="server" CallbackPageSize="15" 
                                      ClientInstanceName="cboDestinationPort" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" DropDownWidth="210px" EnableCallbackMode="True" 
                                      IncrementalFilteringMode="StartsWith" Spacing="0" 
                                      SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                      TextField="PortName" Value='<%# Bind("DestinationPort") %>' 
                                      ValueField="PortID" ValueType="System.Int32" Width="210px" 
                                      oncallback="dxcbDestPort_Callback">
                                      <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                      </LoadingPanelImage>
                                      <LoadingPanelStyle ImageSpacing="5px">
                                      </LoadingPanelStyle>
                                       <ClientSideEvents SelectedIndexChanged="onDestinationPortChanged" 
                                          EndCallback="onDestinationPortEndCallBack" />
                                      <Columns>
                                            <dx:ListBoxColumn Caption="Port" FieldName="PortName" Name="colPortName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="ETA" FieldName="ETA" 
                                                Name="colETA" Width="100px" />
                                        </Columns>
                                  </dx:ASPxComboBox>
                              </td>
                          </tr> 
                          <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEta" runat="server" ClientInstanceName="lblEta" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Text="Arriving">
                                  </dx:ASPxLabel>
                              </td>
                              <td>
                                  <dx:ASPxLabel ID="dxlblEtaSub" runat="server" ClientInstanceName="lblEtaSub" 
                                      CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                      CssPostfix="Office2010Blue" Value='<%# Bind("ETA") %>'>
                                  </dx:ASPxLabel>
                              </td>
                          </tr> 
                         <tr>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                      </table>              
                </InsertItemTemplate>
            </asp:FormView>
        </div>    
        <!-- custom command buttons for formview -->
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="clear"></div>
        <!-- order number lists -->
        <div class="grid_16 pad_bottom">
           <dx:ASPxCallbackPanel ID="dxcbpOrderNumbers" 
                ClientInstanceName="cbpOrderNumbers" runat="server" 
            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
            CssPostfix="Office2010Blue" Width="100%" 
                oncallback="dxcbpOrderNumbers_Callback">
            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
            </LoadingPanelImage>
             <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
            <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
            <!-- table for arranging lists into 3 columns -->
            <table id="tblOrders" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                      <colgroup>
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                          <col class="caption10" />
                          <col />
                      </colgroup>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblOrdersHeader" ClientInstanceName="lblOrdersHeader" 
                            runat="server" Text="Order numbers" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblAvailable" ClientInstanceName="lblAvailable" 
                            runat="server" Text="Available" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxLabel ID="dxlblSelected" ClientInstanceName="lblSelected" 
                            runat="server" Text="To add" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxLabel ID="dxlblOrderSaved" ClientInstanceName="lblOrderSaved" 
                            runat="server" Text="Already added" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td rowspan="6">
                       
                        <dx:ASPxListBox ID="dxlstAvailable" runat="server" 
                            ClientInstanceName="lstAvailable" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Height="185px" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Width="185px" SelectionMode="CheckColumn">
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                             <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                        </dx:ASPxListBox>
                       
                    </td>
                    <td>
                        &nbsp;</td>
                    <td rowspan="6">
                        <dx:ASPxListBox ID="dxlstSelected" runat="server" 
                            ClientInstanceName="lstSelected" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Height="185px" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Width="185px" SelectionMode="CheckColumn">
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                            </ClientSideEvents>
                        </dx:ASPxListBox>
                       
                    </td>
                    <td>
                        &nbsp;</td>
                    <td rowspan="6">
                    
                        <dx:ASPxListBox ID="dxlstSaved" runat="server" ClientInstanceName="lstSaved" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Height="185px" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Width="185px" SelectionMode="CheckColumn">
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxListBox>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="dxbtnSelect" runat="server" ClientInstanceName="btnSelect" 
                            AutoPostBack="false" CausesValidation="false"  
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Add &gt;" Width="110px">
                            <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="dxbtnSaveOrders" runat="server" 
                            ClientInstanceName="btnSaveOrders" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Save" AutoPostBack="False" CausesValidation="False" Width="110px">
                            <ClientSideEvents Click="onSaveOrdersClick" />
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="dxbtnSelectAll" runat="server" AutoPostBack="false" CausesValidation="false" 
                            ClientInstanceName="btnSelectAll" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Add all &gt;&gt;" Width="110px">
                             <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="dxbtnUndo" runat="server" ClientInstanceName="btnUndo" 
                            AutoPostBack="false" CausesValidation="false" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="&lt; Undo" Width="110px">
                             <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="dxbtnUndoAll" runat="server" ClientInstanceName="btnUndoAll" 
                            AutoPostBack="false" CausesValidation="false"  
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="&lt;&lt; Undo all" Width="110px">
                             <ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="dxbtnRemoveOrders" runat="server" 
                            ClientInstanceName="btnRemoveOrders" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Remove" Width="110px" AutoPostBack="False" CausesValidation="False">
                            <ClientSideEvents Click="onRemoveOrdersClick" />
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table> 
            <!-- end table -->
            </dx:PanelContent>
            </PanelCollection>
            <LoadingPanelStyle ImageSpacing="5px">
            </LoadingPanelStyle>
        </dx:ASPxCallbackPanel>
        <!-- end callback panel -->
        </div>    
        <div class="grid_16 pad_bottom">
            <!-- house bl menu -->
              <dx:ASPxMenu ID="dxmnuFormView" runat="server" 
                ClientInstanceName="mnuFormView" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" onitemclick="dxmnuFormView_ItemClick" 
                 onitemdatabound="dxmnuFormView_ItemDataBound">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
            </div> 
        <div class="clear"></div> 
        <div class="grid_16">
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" 
                ClientInstanceName="dxhfOrder">
            </dx:ASPxHiddenField>
       
        </div>
        <!-- end hiddenfield --> 
    </div><!-- end container -->
        
    
  
</asp:Content>


