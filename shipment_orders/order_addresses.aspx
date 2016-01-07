<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_addresses.aspx.cs" Inherits="order_addresses" %>

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

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onConsigneeSelected(s, e) {
            
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblConsigneeIDEdit.SetText(s1);
        }

        function onNotifySelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblNotifyPartyIDEdit.SetText(s1);
        }

        function onClearingSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblClearingAgentIDEdit.SetText(s1);
        }

        function onCarriageSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblOnCarriageIDEdit.SetText(s1);
        }

        function onDestAgentSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblAgentAtDestinationIDEdit.SetText(s1);
            requeryDestinationController(s.GetSelectedItem().GetColumnText('CountryID'));
        }


        function requeryDestinationController(destinationAgentId) {
            cboDestinationPortControllerIDEdit.PerformCallback(destinationAgentId);
        }
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
        }

        function onEditConsignee(s, e) {
            var pid = cboConsigneeIDEdit.GetValue();
            if (pid != null && pid > 0) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);
                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert("You have not selected a consignee"); 
            }
        }

        function onEditClearingAgent(s, e) {
            var pid = cboClearingAgentIDEdit.GetValue();
            if (pid != null && pid > 0) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);
                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert("You have not selected a clearing agent");
            }
        }

        function onEditDestinationAgent(s, e) {
            var pid = cboAgentAtDestinationIDEdit.GetValue();
            if (pid != null && pid > 0) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);
                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert("You have not selected a destination agent");
            }
        }

        function onEditNotifyParty(s, e) {
            var pid = cboNotifyPartyIDEdit.GetValue();
            if (pid != null && pid > 0) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);
                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert("You have not selected a notify party");
            }
        }

        function onEditCarriage(s, e) {
            var pid = cboOnCarriageIDEdit.GetValue();
            if (pid != null && pid > 0) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);
                pcPodEdit.ShowWindow(winEdit);
            } else {
                alert("You have not selected a carriage arranged by");
            }
        }
        
        function onNewCompany(s, e) {

            var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
            pcPodEdit.SetWindowContentUrl(winEdit, '');
            pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Insert&pid=' + "new");

            pcPodEdit.ShowWindow(winEdit);
        }

        function onConsigneeButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblConsigneeIDEdit.SetText('');
            }
        }

        function onClearingAgentButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblClearingAgentIDEdit.SetText('');
            }
        }

        function onAgentAtDestinationButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblAgentAtDestinationIDEdit.SetText('');
            }
        }

        function onNotifyPartyButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblNotifyPartyIDEdit.SetText('');
            }
        }

        function onCarriageButtonclick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblOnCarriageIDEdit.SetText('');
            }
        }

        function onDestinationPortContollerButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
            }
        }
        //********************
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
       
        <!-- Using tables for dataform layout. Semantically not a good choice but other options e.g. definition lists
             do not render properly in older versions of internet explorer < IE7. Also multi-column combos do not
             render correctly when placed in definition lists in < IE7  -->
        <div class="grid_10">
                <div class="divleft">
                   <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/shipment_orders/order_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" Text="Order No|">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblOrderNo" runat="server" ClientInstanceName="lblOrderNo" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlbOrderDetails1" runat="server" 
                            ClientInstanceName="lblOrderDetails1" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="| Addresses">
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <div class="divright">
              <dx:ASPxImage ID="dximgJobPubliship" runat="server" 
                            AlternateText="Publiship Job" ClientInstanceName="imgJobPubliship" 
                            Height="26px" ImageAlign="Top" ImageUrl="~/Images/icons/metro/checked_checkbox.png" 
                            Width="26px">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobPubliship" runat="server" 
                            ClientInstanceName="lblJobPubliship" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Publiship job">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
              <dx:ASPxImage ID="dximgJobHot" runat="server" AlternateText="Hot Job" 
                            ClientInstanceName="imgJobHot" Height="26px" 
                            ImageUrl="~/Images/icons/metro/matches.png" Width="26px" 
                            ImageAlign="Top">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobHot" runat="server" 
                    ClientInstanceName="lblJobHot" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Hot job" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
                         <dx:ASPxImage ID="dximgJobClosed" runat="server" AlternateText="Job Closed" 
                            ClientInstanceName="imgJobClosed" Height="26px" 
                            ImageUrl="~/Images/icons/metro/lock.png" Width="26px" 
                ImageAlign="Top">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobClosed" runat="server" 
                            ClientInstanceName="lblJobClosed" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="closed" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div> 
         </div>              
        <div class="clear"></div>
        
        <!-- tabs -->
        <div class="grid_16">
         <dx:ASPxTabControl ID="dxtabOrder" runat="server" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                TabSpacing="0px" ondatabound="dxtabOrder_DataBound">
                            <ContentStyle>
                                <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                            </ContentStyle>
                            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                        </dx:ASPxTabControl>
        </div> 
        <div class="clear"></div>
        
        <!-- edit/insert addresses -->
        <div class="grid_16 pad bottom">
        
            <asp:FormView ID="fmvAddresses" runat="server" width="100%" 
                DataKeyNames="OrderID" ondatabound="fmvAddresses_DataBound">
                <EditItemTemplate>
                       <!-- don't use a grid with 'table-layout: fixed' here as comboboxes are rendered as tables and inherit css from style sheets which might break the table layout.
                        doesn't seem to matter until you have combox next to each other in columns then the columns are messed up -->
                        <table id="tblAddressesEdit" cellpadding="5px" border="0" width="100%" class="viewTable">
                             <colgroup>
                                <col class="caption8" />
                                <col />
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                            </colgroup> 
                            <tbody>
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblConsigneeEdit" runat="server" 
                                            ClientInstanceName="lblConsigneeEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Consignee">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboConsigneeIDEdit" 
                                            ClientInstanceName="cboConsigneeIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("ConsigneeID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0" DropDownRows="10" >
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                         <ClientSideEvents ButtonClick="onConsigneeButtonClick" SelectedIndexChanged="function(s, e) {
	                                            onConsigneeSelected(s, e); }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblClearingEdit" runat="server" 
                                            ClientInstanceName="lblClearingEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Clearing agent">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboClearingAgentIDEdit" 
                                            ClientInstanceName="cboClearingAgentIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("ClearingAgentID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" DropDownRows="10" TabIndex="2" >
                                         <ClientSideEvents ButtonClick="onClearingAgentButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onClearingSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblDestAgentEdit" runat="server" 
                                            ClientInstanceName="lblDestAgentEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination agent" width="100%" 
                                            Wrap="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboAgentAtDestinationIDEdit" 
                                            ClientInstanceName="cboAgentAtDestinationIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("AgentAtDestinationID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0" DropDownRows="10" TabIndex="4">
                                         <ClientSideEvents ButtonClick="onAgentAtDestinationButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onDestAgentSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
                                             <dx:ListBoxColumn FieldName="CountryID" 
                                                Name="colCountryID" Width="1px" />
                                        </Columns>
                                         <Buttons>
                                            <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                                <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                                </Image>
                                            </dx:EditButton>
                                        </Buttons> 
                                        </dx:ASPxComboBox> 
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkNewConsignee" runat="server" 
                                         ClientInstanceName="linkNewConsignee" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkEditConsignee" runat="server" 
                                         ClientInstanceName="linkEditConsignee" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditConsignee" />
                                     </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblConsigneeIDEdit" runat="server" 
                                            ClientInstanceName="lblConsigneeIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxHyperLink ID="dxlinkClearingNew" runat="server" 
                                            ClientInstanceName="linkClearingNew" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                            <ClientSideEvents Click="onNewCompany" />
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="dxlinkClearingEdit" runat="server" 
                                            ClientInstanceName="linkClearingEdit" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                            <ClientSideEvents Click="onEditClearingAgent" />
                                        </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblClearingAgentIDEdit" runat="server" 
                                            ClientInstanceName="lblClearingAgentIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxHyperLink ID="dxlinkAgentNew" runat="server" 
                                             ClientInstanceName="linkAgentNew" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                             ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                             <ClientSideEvents Click="onNewCompany" />
                                         </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="dxlinkAgentEdit" runat="server" 
                                             ClientInstanceName="linkAgentEdit" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                             ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                             <ClientSideEvents Click="onEditDestinationAgent" />
                                         </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblAgentAtDestinationIDEdit" runat="server" 
                                            ClientInstanceName="lblAgentAtDestinationIDEdit" 
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
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblNotifyEdit" runat="server" 
                                            ClientInstanceName="lblNotifyEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Notify party">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboNotifyPartyIDEdit" 
                                            ClientInstanceName="cboNotifyPartyIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("NotifyPartyID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" DropDownRows="10" TabIndex="1" >
                                         <ClientSideEvents ButtonClick="onNotifyPartyButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onNotifySelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblCarriageEdit" runat="server" 
                                            ClientInstanceName="lblCarriageEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="On-carriage arranged by">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboOnCarriageIDEdit" 
                                            ClientInstanceName="cboOnCarriageIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("OnCarriageID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" DropDownRows="10" TabIndex="3" >
                                         <ClientSideEvents ButtonClick="onCarriageButtonclick" SelectedIndexChanged="function(s, e) {
	                                        onCarriageSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblDestControlEdit" runat="server" 
                                            ClientInstanceName="lblDestControlEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination controller">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboDestinationPortControllerIDEdit" 
                                            ClientInstanceName="cboDestinationPortControllerIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="200px" CallbackPageSize="20" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="Name" ValueField="CompanyID" Value='<%# Bind("DestinationPortControllerID") %>' 
                                             Spacing="0" 
                                            oncallback="dxcboDestinationPortControllerIDEdit_Callback" 
                                            DropDownRows="10" TabIndex="5">
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="EmployeeID(Hidden)" FieldName="EmployeeID" 
                                                Name="colEmployeeID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="Name" Name="colName" 
                                                Width="190px"/>
                                        </Columns>
                                         <Buttons>
                                            <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                                <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                                </Image>
                                            </dx:EditButton>
                                        </Buttons> 
                                        <ClientSideEvents ButtonClick="onDestinationPortContollerButtonClick" />
                                        </dx:ASPxComboBox> 
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkNotifyNew" runat="server" 
                                         ClientInstanceName="linkNotifyNew" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkNotifyEdit" runat="server" 
                                         ClientInstanceName="linkNotifyEdit" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditNotifyParty" />
                                     </dx:ASPxHyperLink>    
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblNotifyPartyIDEdit" runat="server" 
                                            ClientInstanceName="lblNotifyPartyIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkCarriageNew" runat="server" 
                                         ClientInstanceName="linkCarriageNew" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkCarriageEdit" runat="server" 
                                         ClientInstanceName="linkCarriageEdit" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditCarriage" />
                                     </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblOnCarriageIDEdit" runat="server" 
                                            ClientInstanceName="lblOnCarriageIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                </tr>
                                <tr>
                                    <td>
                                        </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </tbody> 
                        </table> 
                </EditItemTemplate>
                <InsertItemTemplate>
                      <!-- don't use a grid with 'table-layout: fixed' here as comboboxes are rendered as tables and inherit css from style sheets which might break the table layout.
                        doesn't seem to matter until you have combox next to each other in columns then the columns are messed up -->
                        <table id="tblAddressesInsert" cellpadding="5px" border="0" width="100%" class="viewTable">
                             <colgroup>
                                <col class="caption8" />
                                <col />
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                            </colgroup> 
                            <tbody>
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblConsigneeEdit" runat="server" 
                                            ClientInstanceName="lblConsigneeEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Consignee">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboConsigneeIDEdit" 
                                            ClientInstanceName="cboConsigneeIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("ConsigneeID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0" >
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                         <ClientSideEvents ButtonClick="onConsigneeButtonClick" SelectedIndexChanged="function(s, e) {
	                                            onConsigneeSelected(s, e); }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblClearingEdit" runat="server" 
                                            ClientInstanceName="lblClearingEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Clearing agent">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboClearingAgentIDEdit" 
                                            ClientInstanceName="cboClearingAgentIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("ClearingAgentID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" >
                                         <ClientSideEvents ButtonClick="onClearingAgentButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onClearingSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblDestAgentEdit" runat="server" 
                                            ClientInstanceName="lblDestAgentEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination agent" width="100%" 
                                            Wrap="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboAgentAtDestinationIDEdit" 
                                            ClientInstanceName="cboAgentAtDestinationIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("AgentAtDestinationID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0" >
                                         <ClientSideEvents ButtonClick="onAgentAtDestinationButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onDestAgentSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
                                             <dx:ListBoxColumn Caption="CountryID(Hidden)" FieldName="CountryID" 
                                                Name="colCountryID" Visible="false" />
                                        </Columns>
                                         <Buttons>
                                            <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                                <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                                </Image>
                                            </dx:EditButton>
                                        </Buttons> 
                                        </dx:ASPxComboBox> 
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkNewConsignee" runat="server" 
                                         ClientInstanceName="linkNewConsignee" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkEditConsignee" runat="server" 
                                         ClientInstanceName="linkEditConsignee" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditConsignee" />
                                     </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblConsigneeIDEdit" runat="server" 
                                            ClientInstanceName="lblConsigneeIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxHyperLink ID="dxlinkClearingNew" runat="server" 
                                            ClientInstanceName="linkClearingNew" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                            <ClientSideEvents Click="onNewCompany" />
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="dxlinkClearingEdit" runat="server" 
                                            ClientInstanceName="linkClearingEdit" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                            ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                            <ClientSideEvents Click="onEditClearingAgent" />
                                        </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblClearingAgentIDEdit" runat="server" 
                                            ClientInstanceName="lblClearingAgentIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxHyperLink ID="dxlinkAgentNew" runat="server" 
                                             ClientInstanceName="linkAgentNew" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                             ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                             <ClientSideEvents Click="onNewCompany" />
                                         </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="dxlinkAgentEdit" runat="server" 
                                             ClientInstanceName="linkAgentEdit" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                             ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                             <ClientSideEvents Click="onEditDestinationAgent" />
                                         </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblAgentAtDestinationIDEdit" runat="server" 
                                            ClientInstanceName="lblAgentAtDestinationIDEdit" 
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
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblNotifyEdit" runat="server" 
                                            ClientInstanceName="lblNotifyEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Notify party">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboNotifyPartyIDEdit" 
                                            ClientInstanceName="cboNotifyPartyIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("NotifyPartyID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" >
                                         <ClientSideEvents ButtonClick="onNotifyPartyButtonClick" SelectedIndexChanged="function(s, e) {
	                                        onNotifySelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblCarriageEdit" runat="server" 
                                            ClientInstanceName="lblCarriageEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="On-carriage arranged by">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboOnCarriageIDEdit" 
                                            ClientInstanceName="cboOnCarriageIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="CompanyName" ValueField="CompanyID" Value='<%# Bind("OnCarriageID") %>' 
                                        onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                            onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                             Spacing="0"  ClientVisible="True" >
                                         <ClientSideEvents ButtonClick="onCarriageButtonclick" SelectedIndexChanged="function(s, e) {
	                                        onCarriageSelected(s, e); }" />
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colCompanyID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="colName" 
                                                Width="190px"/>
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="colAddress1" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="colAddress2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="colAddress3" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="colCountryName" Width="150px"/>
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="colTelNo" 
                                                Width="100px"/>
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
                                        <dx:ASPxLabel ID="dxlblDestControlEdit" runat="server" 
                                            ClientInstanceName="lblDestControlEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination controller">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      <dx:ASPxComboBox ID="dxcboDestinationPortControllerIDEdit" 
                                            ClientInstanceName="cboDestinationPortControllerIDEdit" runat="server"
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        ValueType="System.Int32" Width="200px" DropDownWidth="890px" CallbackPageSize="15" 
                                        EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                        TextField="Name" ValueField="CompanyID" Value='<%# Bind("DestinationPortControllerID") %>' 
                                             Spacing="0" PopupVerticalAlign="WindowCenter" 
                                            oncallback="dxcboDestinationPortControllerIDEdit_Callback">
                                         <ButtonStyle Width="13px">
                                         </ButtonStyle>
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="EmployeeID(Hidden)" FieldName="EmployeeID" 
                                                Name="colEmployeeID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="Name" Name="colName" 
                                                Width="120px"/>
                                        </Columns>
                                         <Buttons>
                                            <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                                <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                                </Image>
                                            </dx:EditButton>
                                        </Buttons> 
                                        <ClientSideEvents ButtonClick="onDestinationPortContollerButtonClick" />
                                        </dx:ASPxComboBox> 
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkNotifyNew" runat="server" 
                                         ClientInstanceName="linkNotifyNew" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkNotifyEdit" runat="server" 
                                         ClientInstanceName="linkNotifyEdit" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditNotifyParty" />
                                     </dx:ASPxHyperLink>    
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblNotifyPartyIDEdit" runat="server" 
                                            ClientInstanceName="lblNotifyPartyIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                    <dx:ASPxHyperLink ID="dxlinkCarriageNew" runat="server" 
                                         ClientInstanceName="linkCarriageNew" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                         <ClientSideEvents Click="onNewCompany" />
                                     </dx:ASPxHyperLink>
                                     <dx:ASPxHyperLink ID="dxlinkCarriageEdit" runat="server" 
                                         ClientInstanceName="linkCarriageEdit" 
                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                         CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                         ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                         <ClientSideEvents Click="onEditCarriage" />
                                     </dx:ASPxHyperLink>
                                    </td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblOnCarriageIDEdit" runat="server" 
                                            ClientInstanceName="lblOnCarriageIDEdit" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                </tr>
                                <tr>
                                    <td>
                                        </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </tbody> 
                        </table> 
                </InsertItemTemplate> 
                <ItemTemplate>
                <table id="tblAddressesView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                            <colgroup>
                                <col class="caption8" />
                                <col />
                                <col class="caption12" />
                                <col />
                                <col class="caption12" />
                                <col />
                            </colgroup> 
                            <tbody>
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblConsigneeView" runat="server" 
                                            ClientInstanceName="lblConsigneeView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Consignee">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblConsigneeIDName" runat="server" 
                                            ClientInstanceName="lblConsigneeIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblClearingView" runat="server" 
                                            ClientInstanceName="lblClearingView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Clearing agent">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblClearingAgentIDName" runat="server" 
                                            ClientInstanceName="lblClearingAgentIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblDestAgentView" runat="server" 
                                            ClientInstanceName="lblDestAgentView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination agent">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblAgentAtDestinationIDName" runat="server" 
                                            ClientInstanceName="lblAgentAtDestinationIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblConsigneeIDView" runat="server" 
                                            ClientInstanceName="lblConsigneeIDView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblClearingAgentIDView" runat="server" 
                                            ClientInstanceName="lblClearingAgentIDView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblAgentAtDestinationIDView" runat="server" 
                                            ClientInstanceName="lblAgentAtDestinationIDView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        <asp:HiddenField ID="hfConsigneeID" runat="server" 
                                            Value="<%# Bind('ConsigneeID') %>" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfClearingAgentID" runat="server" 
                                            Value="<%# Bind('ClearingAgentID') %>" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfAgentAtDestinationID" runat="server" 
                                            Value="<%# Bind('AgentAtDestinationID') %>" />
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                                <tr class="row_divider">
                                    <td>
                                        <dx:ASPxLabel ID="dxlblNotifyView" runat="server" 
                                            ClientInstanceName="lblNotifyView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Notify party">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblNotifyPartyIDName" runat="server" 
                                            ClientInstanceName="lblNotifyPartyIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCarriageView" runat="server" 
                                            ClientInstanceName="lblCarriageView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="On-carriage arranged by">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblOnCarriageIDName" runat="server" 
                                            ClientInstanceName="lblOnCarriageIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblDestControlView" runat="server" 
                                            ClientInstanceName="lblDestControlView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Destination controller">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblDestinationPortControllerIDName" runat="server" 
                                            ClientInstanceName="lblDestinationPortControllerIDName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblNotifyPartyIDView" runat="server" 
                                            ClientInstanceName="dxlblNotifyPartyIDView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="3">
                                        <dx:ASPxLabel ID="dxlblOnCarriageIDView" runat="server" 
                                            ClientInstanceName="lblOnCarriageIDView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr class="row_divider">
                                    <td>
                                        <asp:HiddenField ID="hfNotifyPartyID" runat="server" 
                                            Value="<%# Bind('NotifyPartyID') %>" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfOnCarriageID" runat="server" 
                                            Value="<%# Bind('OnCarriageID') %>" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hfDestinationPortControllerID" runat="server" 
                                            Value="<%# Bind('DestinationPortControllerID') %>" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
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
                                </tr>
                            </tbody>
                         </table> 
                </ItemTemplate>  
            </asp:FormView>
            <!-- custom command butons for formview -->
            <div>
                  <dx:ASPxMenu ID="dxmnuCommand" runat="server" 
                ClientInstanceName="mnuCommand" width="100%" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                onitemclick="dxmnuCommand_ItemClick" AutoSeparators="RootOnly" 
                ItemAutoWidth="False" onitemdatabound="dxmnuCommand_ItemDataBound">
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
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="clear"></div>
    </div><!-- end container -->
    
    
    <dx1:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
    </dx1:ASPxHiddenField>
    
     <dx:ASPxPopupControl ID="dxpcPodEdit" ClientInstanceName="pcPodEdit" 
        runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
        PopupAction="None" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter">
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
                ContentUrl="../Popupcontrol/order_name_address.aspx" HeaderText="Company details"
                Height="820px" Modal="True" Name="CompanyDetails" PopupAction="None" 
                Width="1000px">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
        
        </Windows>
    </dx:ASPxPopupControl>
    
    </asp:Content>
