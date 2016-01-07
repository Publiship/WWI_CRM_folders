<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="clone_order.aspx.cs" Inherits="clone_order" %>

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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function onCompanySelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblCompanyIDView.SetText(s1);
            //requery client contact
            requeryContact(s.GetValue().toString());
        }

        function requeryContact(companyId) {
            cboContactID.PerformCallback(companyId);
            cboContactID.SetSelectedIndex(-1);
            //var s1 = cboClientContact.GetSelectedItem().GetColumnText('Email');
            //lblContactEmail.SetText('');
        }
        
        function onConsigneeSelected(s, e) {
            
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblConsigneeIDView.SetText(s1);
        }

        function onNotifySelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblNotifyPartyIDView.SetText(s1);
        }

        function onClearingAgentSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblClearingAgentIDView.SetText(s1);
        }

        function onCarriageSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblOnCarriageIDView.SetText(s1);
        }

        function onPrinterSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('PrinterName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterAdd2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterAdd3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterCountry');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterTel');

            lblPrinterIDView.SetText(s1);
        }

        function onOriginAgentSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('OriginAgentAddress1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentAddress2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentAddress3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentCountry');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentTel');

            lblAgentAtOriginIDView.SetText(s1);
            requeryOriginController(s.GetValue().toString());
        }

        function requeryOriginController(originAgentId) {
            cboOriginPortControllerID.PerformCallback(originAgentId);
            cboContactID.SetSelectedIndex(-1);
        }

        function onDestinationAgentSelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblAgentAtDestinationIDView.SetText(s1);
            requeryDestinationController(s.GetSelectedItem().GetColumnText('CompanyID'));
        }

        function requeryDestinationController(destinationAgentId) {
            cboDestinationPortControllerID.PerformCallback(destinationAgentId);
        }

        function onCountryChanged(s, e) {
            cboOriginPointID.SetSelectedIndex(-1);
            cboPortID.SetSelectedIndex(-1);
            cboOriginPointID.PerformCallback(s.GetValue());
        }

        function onOriginChanged(cborigin) {
            cboPortID.SetSelectedIndex(-1);
            //cbkOriginGroup.PerformCallback(2);
            cboPortID.PerformCallback();
        }
        
        
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
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
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/system_templates/order_template_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderTemplate" runat="server" 
                             ClientInstanceName="lblOrderTemplate" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Clone order|">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblOrderNumber" runat="server" ClientInstanceName="lblOrderNumber" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <dx:ASPxLabel ID="dxlblOffice" runat="server" ClientInstanceName="lblOffice" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
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
        
            <asp:FormView ID="fmvTemplate" runat="server" width="100%" 
                DataKeyNames="OrderTemplateID" ondatabound="fmvTemplate_DataBound">
                <EditItemTemplate>
                       <!-- don't use a grid with 'table-layout: fixed' here as comboboxes are rendered as tables and inherit css from style sheets which might break the table layout.
                        doesn't seem to matter until you have combox next to each other in columns then the columns are messed up -->
                         <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable">
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
                                        <dx:ASPxLabel ID="dxlblTemplateNameView" runat="server" 
                                            AssociatedControlID="dxtxtTemplateName" 
                                            ClientInstanceName="lblTemplateNameView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Template name">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                         <dx:ASPxTextBox ID="dxtxtTemplateName" runat="server" 
                                             ClientInstanceName="txtTemplateName" 
                                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                             CssPostfix="Office2010Blue" 
                                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                             Value='<%# Bind("TemplateName") %>' Width="170px">
                                         </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblOfficeIndicatorView" runat="server" 
                                            AssociatedControlID="dxcboOfficeIndicator" 
                                            ClientInstanceName="lblOfficeIndicatorView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Office indicator">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      
                                        <dx:ASPxComboBox ID="dxcboOfficeIndicator" runat="server" CallbackPageSize="15" 
                                            ClientInstanceName="cboOfficeIndicator" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" DropDownWidth="150px" EnableCallbackMode="True" 
                                            IncrementalFilteringMode="StartsWith" Spacing="0" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                            Value='<%# Bind("OfficeIndicator") %>' ValueField="EmployeeID" 
                                            ValueType="System.Int32" Width="150px">
                                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCustomersRef" runat="server" 
                                            AssociatedControlID="dxtxtCustomersRef" ClientInstanceName="lblCustomersRef" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Customers ref">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                       
                                             <dx:ASPxTextBox ID="dxtxtCustomersRef" runat="server" 
                                                 ClientInstanceName="txtCustomersRef" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" 
                                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                 Value='<%# Bind("CustomersRef") %>' Width="170px">
                                             </dx:ASPxTextBox>
                                    </td>
                                </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOrderControllerView" runat="server" 
                                           ClientInstanceName="lblOrderControllerView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Order controller" 
                                           AssociatedControlID="dxcboOrderControllerID">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                        <dx:ASPxComboBox ID="dxcboOrderControllerID" runat="server" 
                                            CallbackPageSize="15" ClientInstanceName="cboOrderControllerID" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                             IncrementalFilteringMode="StartsWith" 
                                            Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            TextField="Name" Value='<%# Bind("OrderControllerID") %>' 
                                            ValueField="EmployeeID" ValueType="System.Int32" Width="200px">
                                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                        </dx:ASPxComboBox>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOperationsControllerView" runat="server" 
                                           ClientInstanceName="lblOperationsControllerView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Operations controller" 
                                           AssociatedControlID="dxcboOperationsControllerID">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxComboBox ID="dxcboOperationsControllerID" runat="server" 
                                           CallbackPageSize="15" ClientInstanceName="cboOperationsControllerID" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                            IncrementalFilteringMode="StartsWith" 
                                           Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                           TextField="Name" Value='<%# Bind("OperationsControllerID") %>' 
                                           ValueField="EmployeeID" ValueType="System.Int32" Width="200px">
                                           <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                           </LoadingPanelImage>
                                           <LoadingPanelStyle ImageSpacing="5px">
                                           </LoadingPanelStyle>
                                       </dx:ASPxComboBox>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactView" runat="server" 
                                           ClientInstanceName="lblContactView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Client contact" 
                                           AssociatedControlID="dxcboContactID">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxComboBox ID="dxcboContactID" runat="server" CallbackPageSize="15" 
                                           ClientInstanceName="cboContactID" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                            IncrementalFilteringMode="StartsWith" 
                                           oncallback="dxcboContactID_Callback" Spacing="0" 
                                           SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="ContactName" 
                                           Value='<%# Bind("ContactID") %>' ValueField="ContactID" 
                                           ValueType="System.Int32" Width="200px">
                                           <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                           </LoadingPanelImage>
                                           <LoadingPanelStyle ImageSpacing="5px">
                                           </LoadingPanelStyle>
                                             <Columns>
                                                 <dx:ListBoxColumn Caption="ContactID(Hidden)" FieldName="ContactID" 
                                                     Name="colContactID" Visible="false" />
                                                 <dx:ListBoxColumn Caption="Name" FieldName="ContactName" Name="colContactName" 
                                                     Width="180px" />
                                                 <dx:ListBoxColumn Caption="Email Address" FieldName="Email" Name="colEmail" 
                                                     Width="175px" />
                                             </Columns>
                                       </dx:ASPxComboBox>
                                   </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblCountryView" runat="server" 
                                        ClientInstanceName="lblCountryView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin country" 
                                        AssociatedControlID="dxcboCountryID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                       <dx:ASPxComboBox ID="dxcboCountryID" runat="server" CallbackPageSize="15" 
                                         ClientInstanceName="cboCountryID" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                          IncrementalFilteringMode="StartsWith" Spacing="0" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                                         ValueType="System.Int32" Width="200px"  >
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                         <ClientSideEvents SelectedIndexChanged="function(s, e) { onCountryChanged(s, e); }" />
                                     </dx:ASPxComboBox>
                                 </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblDestinationPortView" runat="server" 
                                        ClientInstanceName="lblDestinationPortView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Destination port" 
                                        AssociatedControlID="dxcboDestinationPortID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                      <dx:ASPxComboBox ID="dxcboDestinationPortID" runat="server" CallbackPageSize="15" 
                                         ClientInstanceName="cboDestinationPortID" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                         Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         TextField="PortName" Value='<%# Bind("DestinationPortID") %>' 
                                         ValueField="PortID" ValueType="System.Int32" Width="200px" 
                                          >
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                     </dx:ASPxComboBox>
                                    </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblCompanyView" runat="server" 
                                        ClientInstanceName="lblCompanyView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Client name" 
                                        AssociatedControlID="dxcboCompanyID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboCompanyID" runat="server" CallbackPageSize="15" 
                                 ClientInstanceName="cboCompanyID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="CompanyName" Value='<%# Bind("CompanyID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="200px">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                    onCompanySelected(s, e);
                                    }" />
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
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPointView" runat="server" 
                                        ClientInstanceName="lblOriginPointView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin point" 
                                        AssociatedControlID="dxcboOriginPointID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                     <dx:ASPxComboBox ID="dxcboOriginPointID" runat="server" CallbackPageSize="15" 
                                         ClientInstanceName="cboOriginPointID" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                          IncrementalFilteringMode="StartsWith" 
                                         OnCallback="dxcboOriginPointID_Callback" Spacing="0" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         TextField="PlaceName" Value='<%# Bind("OriginPointID") %>' ValueField="PlaceID" 
                                         ValueType="System.Int32" Width="200px">
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                         <ClientSideEvents SelectedIndexChanged="function(s, e) { onOriginChanged(s); }" />
                                     </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblFinalDestinationView" runat="server" 
                                        ClientInstanceName="lblFinalDestinationView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Final destination" 
                                        AssociatedControlID="dxcboFinalDestinationID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboFinalDestinationID" runat="server" CallbackPageSize="15" 
                                         ClientInstanceName="cboFinalDestinationID" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" Spacing="0" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         TextField="PlaceName" Value='<%# Bind("FinalDestinationID") %>' 
                                         ValueField="PlaceID" ValueType="System.Int32" Width="200px" 
                                         >
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                    </dx:ASPxComboBox>
                                    </td>
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblCompanyIDView" runat="server" 
                                        ClientInstanceName="lblCompanyIDView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblPortView" runat="server" 
                                        ClientInstanceName="lblPortView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin port" 
                                        AssociatedControlID="dxcboPortID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                     <dx:ASPxComboBox ID="dxcboPortID" runat="server" CallbackPageSize="15" 
                                         ClientInstanceName="cboPortID" 
                                         CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                          IncrementalFilteringMode="StartsWith" 
                                         OnCallback="dxcboPortID_Callback" Spacing="0" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         TextField="PortName" Value='<%# Bind("PortID") %>' ValueField="PortID" 
                                         ValueType="System.Int32" Width="200px">
                                         <LoadingPanelStyle ImageSpacing="5px">
                                         </LoadingPanelStyle>
                                         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                         </LoadingPanelImage>
                                     </dx:ASPxComboBox>
                                    </td>
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
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblAgentAtOriginView" runat="server" 
                                        ClientInstanceName="lblAgentAtOriginView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin agent" 
                                        AssociatedControlID="dxcboAgentAtOriginID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                 <dx:ASPxComboBox ID="dxcboAgentAtOriginID" runat="server" CallbackPageSize="15" 
                                 ClientInstanceName="cboAgentAtOriginID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboagentatorigin_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboagentatorigin_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="OriginAgent" Value='<%# Bind("AgentAtOriginID") %>' 
                                 ValueField="OriginAgentID" ValueType="System.Int32" Width="200px">
                                             <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                    onOriginAgentSelected(s, e);
                                    }" />
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <Columns>
                                     <dx:ListBoxColumn Caption="OriginAgentID(Hidden)" FieldName="OriginAgentID" 
                                         Name="colOriginAgentID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="OriginAgent" Name="CompanyName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="OriginAgentAddress1" 
                                         Name="Address1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="OriginAgentAddress2" 
                                         Name="Address2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="OriginAgentAddress3" 
                                         Name="Address3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="OriginAgentCountry" 
                                         Name="CountryName" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="OriginAgentTel" 
                                         Name="TelNo" Width="100px" />
                                 </Columns>
                             </dx:ASPxComboBox>     
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblDestAgentView" runat="server" 
                                        ClientInstanceName="lblDestAgentView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Destination agent" 
                                        AssociatedControlID="dxcboAgentAtDestinationID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboAgentAtDestinationID" runat="server" 
                                        CallbackPageSize="15" ClientInstanceName="cboAgentAtDestinationID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                        onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                        onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                        Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CompanyName" Value='<%# Bind("AgentAtDestinationID") %>' 
                                        ValueField="CompanyID" ValueType="System.Int32" Width="200px" 
                                        >
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                    onDestinationAgentSelected(s, e);
                                    }" />
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="OriginAgentID(Hidden)" FieldName="CompanyID" 
                                                Name="colOriginAgentID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="CompanyName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="Address1" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="Address2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="Address3" Width="150px" />
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="CountryName" Width="150px" />
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" 
                                                Name="TelNo" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblPrinterView" runat="server" 
                                        ClientInstanceName="lblPrinterView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Printer" 
                                        AssociatedControlID="dxcboPrinterID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                  <dx:ASPxComboBox ID="dxcboPrinterID" runat="server" CallbackPageSize="15" 
                                 ClientInstanceName="cboPrinterID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboPrinterID_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboPrinterID_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PrinterName" Value='<%# Bind("PrinterID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="200px"  >
                                 <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                onPrinterSelected(s, e);
                                }" />
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <Columns>
                                     <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                         Name="colPrinterID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="PrinterName" Name="CompanyName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="PrinterAdd1" 
                                         Name="Address1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="PrinterAdd2" 
                                         Name="Address2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="PrinterAdd3" 
                                         Name="Address3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="PrinterCountry" 
                                         Name="CountryName" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="PrinterTel" Name="TelNo" 
                                         Width="100px" />
                                 </Columns>
                             </dx:ASPxComboBox>    
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblAgentAtOriginIDView" runat="server" 
                                        ClientInstanceName="lblAgentAtOriginIDView" 
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
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblPrinterIDView" runat="server" 
                                        ClientInstanceName="lblPrinterIDView" 
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
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPortControllerView" runat="server" 
                                        ClientInstanceName="lblOriginPortControllerView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin controller" 
                                        AssociatedControlID="dxcboOriginPortControllerID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboOriginPortControllerID" runat="server" 
                                        CallbackPageSize="15" ClientInstanceName="cboOriginPortControllerID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                        IncrementalFilteringMode="StartsWith" 
                                        oncallback="dxcboOriginPortControllerID_Callback" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                        Value='<%# Bind("OriginPortControllerID") %>' ValueField="EmployeeID" 
                                        ValueType="System.Int32" Width="200px">
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblDestControlView" runat="server" 
                                        ClientInstanceName="lblDestControlView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Destination controller" 
                                        AssociatedControlID="dxcboDestinationPortControllerID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboDestinationPortControllerID" runat="server" 
                                        CallbackPageSize="15" ClientInstanceName="cboDestinationPortControllerID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                        oncallback="dxcboDestinationPortControllerID_Callback" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                        Value='<%# Bind("DestinationPortControllerID") %>' ValueField="EmployeeID" 
                                        ValueType="System.Int32" Width="200px">
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblConsigneeView" runat="server" 
                                        ClientInstanceName="lblConsigneeView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Consignee" 
                                        AssociatedControlID="dxcboConsigneeID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboConsigneeID" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboConsigneeID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                        IncrementalFilteringMode="StartsWith" 
                                        onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                        onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                        Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CompanyName" Value='<%# Bind("ConsigneeID") %>' ValueField="CompanyID" 
                                        ValueType="System.Int32" Width="200px"  >
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                onConsigneeSelected(s, e);
                                }" />
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colPrinterID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="CompanyName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="Address1" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="Address2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="Address3" Width="150px" />
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="ContryName" Width="150px" />
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="TelNo" 
                                                Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblClearingView" runat="server" 
                                        ClientInstanceName="lblClearingView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Clearing agent" 
                                        AssociatedControlID="dxcboClearingAgentID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboClearingAgentID" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboClearingAgentID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                        onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                        onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                        Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CompanyName" Value='<%# Bind("ClearingAgentID") %>' 
                                        ValueField="CompanyID" ValueType="System.Int32" Width="200px" 
                                        >
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                onClearingAgentSelected(s, e);
                                }" />
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                         <Columns>
                                            <dx:ListBoxColumn Caption="ClearingAgentID(Hidden)" FieldName="CompanyID" 
                                                Name="colOriginAgentID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="CompanyName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="Address1" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="Address2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="Address3" Width="150px" />
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="CountryName" Width="150px" />
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" 
                                                Name="TelNo" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblNotifyView" runat="server" 
                                        ClientInstanceName="lblNotifyView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Notify party" 
                                        AssociatedControlID="dxcboNotifyPartyID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboNotifyPartyID" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboNotifyPartyID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                        onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                        onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                        Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CompanyName" Value='<%# Bind("NotifyPartyID") %>' 
                                        ValueField="CompanyID" ValueType="System.Int32" Width="200px" 
                                        >
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                onNotifySelected(s, e);
                                }" />
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                Name="colPrinterID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="CompanyName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="Address1" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="Address2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="Address3" Width="150px" />
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="CountryName" Width="150px" />
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" Name="TelNo" 
                                                Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
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
                                    <dx:ASPxLabel ID="dxlblNotifyPartyIDView" runat="server" 
                                        ClientInstanceName="lblNotifyPartyIDView" 
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
                                    </td>
                                <td>
                                    </td>
                                <td>
                                    </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblCarriageView" runat="server" 
                                        ClientInstanceName="lblCarriageView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="On-carriage arranged by" 
                                        AssociatedControlID="dxcboOnCarriageID">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboOnCarriageID" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboOnCarriageID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                         IncrementalFilteringMode="StartsWith" 
                                        onitemrequestedbyvalue="dxcboCompanyID_ItemRequestedByValue" 
                                        onitemsrequestedbyfiltercondition="dxcboCompanyID_ItemsRequestedByFilterCondition" 
                                        Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CompanyName" Value='<%# Bind("OnCarriageID") %>' 
                                        ValueField="CompanyID" ValueType="System.Int32" Width="200px" 
                                        >
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                onCarriageSelected(s, e);
                                }" />
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                          <Columns>
                                            <dx:ListBoxColumn Caption="OnCarriageID(Hidden)" FieldName="CompanyID" 
                                                Name="colOriginAgentID" Visible="false" />
                                            <dx:ListBoxColumn Caption="Name" FieldName="CompanyName" Name="CompanyName" 
                                                Width="190px" />
                                            <dx:ListBoxColumn Caption="Address 1" FieldName="Address1" 
                                                Name="Address1" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 2" FieldName="Address2" 
                                                Name="Address2" Width="150px" />
                                            <dx:ListBoxColumn Caption="Address 3" FieldName="Address3" 
                                                Name="Address3" Width="150px" />
                                            <dx:ListBoxColumn Caption="Country" FieldName="CountryName" 
                                                Name="CountryName" Width="150px" />
                                            <dx:ListBoxColumn Caption="Phone" FieldName="TelNo" 
                                                Name="TelNo" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </td>
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
                                        <dx:ASPxLabel ID="dxlblOfficeIndicatorView" runat="server" 
                                            ClientInstanceName="lblOfficeIndicatorView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Office indicator">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblOfficeIndicatorName" runat="server" 
                                            ClientInstanceName="lblOfficeIndicatorName" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="dxlblCustomersRefView" runat="server" 
                                            ClientInstanceName="lblCustomersRefView" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" Text="Customers ref">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                      
                                        <dx:ASPxLabel ID="dxlblCustomersRefName" runat="server" 
                                            ClientInstanceName="lblCustomersRefName" 
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
                                       <dx:ASPxLabel ID="dxlblOrderControllerView" runat="server" 
                                           ClientInstanceName="lblOrderControllerView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Order controller">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOrderControllerIDName" runat="server" 
                                           ClientInstanceName="lblOrderControllerIDName" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOperationsControllerView" runat="server" 
                                           ClientInstanceName="lblOperationsControllerView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Operations controller">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOperationsControllerIDName" runat="server" 
                                           ClientInstanceName="lblOperationsControllerIDName" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactView" runat="server" 
                                           ClientInstanceName="lblContactView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Client contact">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactIDName" runat="server" 
                                           ClientInstanceName="lblContactIDName" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblCountryView" runat="server" 
                                        ClientInstanceName="lblCountryView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin country">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblCountryIDName" runat="server" 
                                        ClientInstanceName="lblCountryIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblDestinationPortView" runat="server" 
                                        ClientInstanceName="lblDestinationPortView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Destination port">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblDestinationPortIDName" runat="server" 
                                        ClientInstanceName="lblDestinationPortIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblCompanyView" runat="server" 
                                        ClientInstanceName="lblCompanyView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Client name">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblCompanyIDName" runat="server" 
                                        ClientInstanceName="lblCompanyIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPointView" runat="server" 
                                        ClientInstanceName="lblOriginPointView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin point">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPointIDName" runat="server" 
                                        ClientInstanceName="lblOriginPointIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblFinalDestinationView" runat="server" 
                                        ClientInstanceName="lblFinalDestinationView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Final destination">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblFinalDestinationIDName" runat="server" 
                                        ClientInstanceName="lblFinalDestinationIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblCompanyIDView" runat="server" 
                                        ClientInstanceName="lblCompanyIDView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblPortView" runat="server" 
                                        ClientInstanceName="lblPortView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin port">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblPortIDName" runat="server" 
                                        ClientInstanceName="lblPortIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
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
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblAgentAtOriginView" runat="server" 
                                        ClientInstanceName="lblAgentAtOriginView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin agent">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblAgentAtOriginIDName" runat="server" 
                                        ClientInstanceName="lblAgentAtOriginIDName" 
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
                                <td>
                                    <dx:ASPxLabel ID="dxlblPrinterView" runat="server" 
                                        ClientInstanceName="lblPrinterView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Printer">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblPrinterIDName" runat="server" 
                                        ClientInstanceName="lblPrinterIDName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblAgentAtOriginIDView" runat="server" 
                                        ClientInstanceName="lblAgentAtOriginIDView" 
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
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
                                    <dx:ASPxLabel ID="dxlblPrinterIDView" runat="server" 
                                        ClientInstanceName="lblPrinterIDView" 
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
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPortControllerView" runat="server" 
                                        ClientInstanceName="lblOriginPortControllerView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Origin controller">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblOriginPortControllerIDName" runat="server" 
                                        ClientInstanceName="lblOriginPortControllerIDName" 
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
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
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
                                    <dx:ASPxLabel ID="dxlblNotifyView" runat="server" 
                                        ClientInstanceName="lblNotifyView" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Notify party">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblNotifyIDName" runat="server" 
                                        ClientInstanceName="lblNotifyIDName" 
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
                                    <dx:ASPxLabel ID="dxlblNotifyIDView" runat="server" 
                                        ClientInstanceName="dxlblNotifyIDView" 
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
                            <tr class="row_divider">
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
                                <td rowspan="3">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3">
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
                         </table> 
                </ItemTemplate>  
            </asp:FormView>
         </div> 
         
       
         <!-- custom command butons for formview -->
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="clear"></div>
        <div class="grid_16 pad_bottom">
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
    </div><!-- end container -->
    
    
    <dx1:ASPxHiddenField ID="dxhfTemplate" runat="server" 
        ClientInstanceName="hfTemplate">
    </dx1:ASPxHiddenField>
    
     </asp:Content>
