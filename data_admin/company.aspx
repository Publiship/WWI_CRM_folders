<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="company" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
       // <![CDATA[
       function textboxKeyup() {
            if (e.htmlEvent.keyCode == ASPxKey.Enter) {
                btnFilter.Focus();
            }
        }
                
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }

        function EditorValueChanged(s, e) {
            StartEdit();
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
          
        <!-- left column for centering form put some padding in it as empty elements are not rendered! -->
        <div class="grid_3 pad_bottom"></div>    
        <div class="grid_10 pad_bottom">
              <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/data_admin/company_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblDetails" runat="server" 
                             ClientInstanceName="lblDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Company information">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <div class="grid_3 pad_bottom"></div>    
        <!-- images and text -->
        
        <!-- tabs -->
        <div class="clear"></div>
        
        <!-- left column for centering form put some padding in it as empty elements are not rendered! -->
        <div class="grid_3 pad_bottom">
       
        </div>    
        <!-- view form columns -->
        <div class="grid_10"> 
           <asp:FormView ID="fmvAddressBook" runat="server" BorderStyle="None" 
                BorderWidth="0px" DataKeyNames="CompanyID"  
                ondatabound="fmvAddressBook_DataBound" 
                onmodechanging="fmvAddressBook_ModeChanging" width="100%">
                <EditItemTemplate>
                    <table id="editAddress" cellpadding="4px" cellspacing="0px" width="100%">
                        <colgroup>
                        <col class="caption10" />
                        <col />
                        <col class="caption10" />
                        <col />
                    </colgroup>
                        <tbody>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditAddressBook" runat="server" 
                                        ClientInstanceName="lbleditOrderDetails" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="Company No.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditCompanyID" runat="server" 
                                        ClientInstanceName="lbleditCompanyID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="company ID" 
                                        Value='<%# Bind("CompanyID") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditName" runat="server" 
                                        ClientInstanceName="lbleditName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company name">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtName" runat="server" ClientInstanceName="txtName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("CompanyName") %>' Width="200px">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditPalletSpec" runat="server" 
                                        ClientInstanceName="lbleditPalletSpec" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Pallet spec.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtPalletSpec" runat="server" 
                                        ClientInstanceName="txtPalletSpec" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("PalletDims") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditAddress1" runat="server" 
                                        ClientInstanceName="lbleditAddress1" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 1">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress1" runat="server" 
                                        ClientInstanceName="txtAddress1" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address1") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditMaxWeight" runat="server" 
                                        ClientInstanceName="lbleditMaxWeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet weight">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtMaxWeight" runat="server" 
                                        ClientInstanceName="txtMaxWeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MaxPalletWeight") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditAddress2" runat="server" 
                                        ClientInstanceName="lbleditAddress2" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 2">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress2" runat="server" 
                                        ClientInstanceName="txtAddress2" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address2") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditMaxHeight" runat="server" 
                                        ClientInstanceName="lbleditMaxHeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet height">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtMaxHeight" runat="server" 
                                        ClientInstanceName="txtMaxHeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MaxPalletHeight") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditAddress3" runat="server" 
                                        ClientInstanceName="lbleditAddress3" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 3">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress3" runat="server" 
                                        ClientInstanceName="txtAddress3" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address3") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditCountry" runat="server" 
                                        ClientInstanceName="lbleditCountry" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Country">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboCountry" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboCountry" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                        IncrementalFilteringMode="StartsWith" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                                        ValueType="System.Int32" Width="200px">
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckExporter" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckExporter" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Exporter" 
                                        Value='<%# Bind("Exporter") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditPostCode" runat="server" 
                                        ClientInstanceName="lbleditPostCode" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Post code">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtPostCode" runat="server" 
                                        ClientInstanceName="txtPostCode" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("PostCode") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckCustomer" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckCustomer" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Customer" 
                                        Value='<%# Bind("Customer") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditTelNo" runat="server" 
                                        ClientInstanceName="lbleditTelNo" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Telephone">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtTelNo" runat="server" ClientInstanceName="txtTelNo" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("TelNo") %>' Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckConsignee" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckConsignee" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Consignee" 
                                        Value='<%# Bind("Consignee") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditFax" runat="server" ClientInstanceName="lbleditFax" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Fax">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtFax" runat="server" ClientInstanceName="txtFax" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("FaxNo") %>' Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckInsurance" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckInsurance" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Publiship to arrange marine insurance" Value='<%# Bind("Insurance") %>' 
                                        Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditEmail" runat="server" 
                                        ClientInstanceName="lbleditEmail" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Main email">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtEmail" runat="server" ClientInstanceName="txtEmail" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MainEmail") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckSalesTarget" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckSalesTarget" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Sales target" Value='<%# Bind("SalesModule") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlbleditType" runat="server" 
                                        ClientInstanceName="lbleditType" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company type">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboCompanyType" runat="server" CallbackPageSize="25" 
                                        ClientInstanceName="cboCompanyType" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownRows="20" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="TypeName" Value='<%# Bind("TypeID") %>' ValueField="TypeID" 
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
                                    <dx:ASPxLabel ID="dxlbleditDelivery" runat="server" 
                                        ClientInstanceName="lbleditDelivery" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Special delivery instructions">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxMemo ID="dxmemoDelivery" runat="server" 
                                        ClientInstanceName="memoDelivery" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Height="71px" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("SpecialDeliveryInstructions") %>' Width="200px">
                                    </dx:ASPxMemo>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
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
                    <table id="insertAddress" cellpadding="4px" cellspacing="0px" width="100%">
                           <colgroup>
                                <col class="caption10" />
                                <col />
                                <col class="caption10" />
                                <col />
                           </colgroup> 
                        <tbody>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertAddressBook" runat="server" 
                                        ClientInstanceName="lblinsertOrderDetails" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="Company No.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertCompanyID" runat="server" 
                                        ClientInstanceName="lblinsertCompanyID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="New" 
                                        Value='<%# Bind("CompanyID") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertName" runat="server" 
                                        ClientInstanceName="lblinsertName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company name">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtNameInsert" runat="server" ClientInstanceName="txtNameInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("CompanyName") %>' Width="200px">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertPalletSpec" runat="server" 
                                        ClientInstanceName="lblinsertPalletSpec" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Pallet spec.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtPalletSpecInsert" runat="server" 
                                        ClientInstanceName="txtPalletSpecInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("PalletDims") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertAddress1" runat="server" 
                                        ClientInstanceName="lblinsertAddress1" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 1">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress1Insert" runat="server" 
                                        ClientInstanceName="txtAddress1Insert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address1") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertMaxWeight" runat="server" 
                                        ClientInstanceName="lblinsertMaxWeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet weight">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtMaxWeightInsert" runat="server" 
                                        ClientInstanceName="txtMaxWeightInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MaxPalletWeight") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertAddress2" runat="server" 
                                        ClientInstanceName="lblinsertAddress2" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 2">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress2Insert" runat="server" 
                                        ClientInstanceName="txtAddress2Insert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address2") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertMaxHeight" runat="server" 
                                        ClientInstanceName="lblinsertMaxHeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet height">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtMaxHeightInsert" runat="server" 
                                        ClientInstanceName="txtMaxHeightInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MaxPalletHeight") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertAddress3" runat="server" 
                                        ClientInstanceName="lblinsertAddress3" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 3">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtAddress3Insert" runat="server" 
                                        ClientInstanceName="txtAddress3Insert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("Address3") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertCountry" runat="server" 
                                        ClientInstanceName="lblinsertCountry" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Country">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboCountry" runat="server" CallbackPageSize="15" 
                                        ClientInstanceName="cboCountry" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                        IncrementalFilteringMode="StartsWith" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                                        ValueType="System.Int32" Width="200px">
                                        <LoadingPanelStyle ImageSpacing="5px">
                                        </LoadingPanelStyle>
                                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckExporterInsert" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckExporterInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Exporter" 
                                        Value='<%# Bind("Exporter") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertPostCode" runat="server" 
                                        ClientInstanceName="lblinsertPostCode" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Post code">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtPostCodeInsert" runat="server" 
                                        ClientInstanceName="txtPostCodeInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("PostCode") %>' Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckCustomerInsert" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckCustomerInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Customer" 
                                        Value='<%# Bind("Customer") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertTelNo" runat="server" 
                                        ClientInstanceName="lblinsertTelNo" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Telephone">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtTelNoInsert" runat="server" ClientInstanceName="txtTelNoInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("TelNo") %>' Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckConsigneeInsert" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckConsigneeInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Consignee" 
                                        Value='<%# Bind("Consignee") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertFax" runat="server" 
                                        ClientInstanceName="lblinsertFax" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Fax">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtFaxInsert" runat="server" ClientInstanceName="txtFaxInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("FaxNo") %>' Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckInsuranceInsert" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckInsuranceInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Publiship to arrange marine insurance" Value='<%# Bind("Insurance") %>' 
                                        Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertEmail" runat="server" 
                                        ClientInstanceName="lblinsertEmail" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Main email">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="dxtxtEmailInsert" runat="server" ClientInstanceName="txtEmailInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("MainEmail") %>' Width="200px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckSalesTargetInsert" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckSalesTargetInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Sales target" Value='<%# Bind("SalesModule") %>' Width="85%">
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblinsertType" runat="server" 
                                        ClientInstanceName="lblinsertType" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company type">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="dxcboCompanyType" runat="server" CallbackPageSize="25" 
                                        ClientInstanceName="cboCompanyType" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" DropDownRows="20" Spacing="0" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        TextField="TypeName" Value='<%# Bind("TypeID") %>' ValueField="TypeID" 
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
                                    <dx:ASPxLabel ID="dxlblinsertDelivery" runat="server" 
                                        ClientInstanceName="lblinsertDelivery" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Special delivery instructions">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxMemo ID="dxmemoDeliveryInsert" runat="server" 
                                        ClientInstanceName="memoDeliveryInsert" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Height="71px" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Value='<%# Bind("SpecialDeliveryInstructions") %>' Width="200px">
                                    </dx:ASPxMemo>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                </InsertItemTemplate>
                <ItemTemplate>
                    <table id="itemAddress" cellpadding="4px" cellspacing="0px" width="100%">
                            <colgroup>
                                <col class="caption10" />
                                <col />
                                <col class="caption10" />
                                <col />
                           </colgroup> 
                        <tbody>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddressBook" runat="server" 
                                        ClientInstanceName="lblitemOrderDetails" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="Company No.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemCompanyID" runat="server" 
                                        ClientInstanceName="lblitemCompanyID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Larger" Text="" 
                                        Value='<%# Bind("CompanyID") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemName" runat="server" 
                                        ClientInstanceName="lblitemName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company name">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemCompanyName" runat="server" 
                                        ClientInstanceName="lblitemCompanyName" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("CompanyName") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemPalletSpec" runat="server" 
                                        ClientInstanceName="lblitemPalletSpec" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Pallet spec.">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemPalletDims" runat="server" 
                                        ClientInstanceName="lblitemPalletDims" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("PalletDims") %>'>
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddr1" runat="server" 
                                        ClientInstanceName="lblitemAddr1" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 1">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddress1" runat="server" 
                                        ClientInstanceName="lblitemAddress1" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("Address1") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemMaxWeight" runat="server" 
                                        ClientInstanceName="lblitemMaxWeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet weight">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemMaxPalletWeight" runat="server" 
                                        ClientInstanceName="lblitemMaxPalletWeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("MaxPalletWeight") %>'>
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddr2" runat="server" 
                                        ClientInstanceName="lblitemAddr2" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 2">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddress2" runat="server" 
                                        ClientInstanceName="lblitemAddress2" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("Address2") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemMaxHeight" runat="server" 
                                        ClientInstanceName="lblitemMaxHeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Max pallet height">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemMaxPalletHeight" runat="server" 
                                        ClientInstanceName="lblitemMaxPalletHeight" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("MaxPalletHeight") %>'>
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddr3" runat="server" 
                                        ClientInstanceName="lblitemAddr3" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Address 3">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemAddress3" runat="server" 
                                        ClientInstanceName="lblitemAddress3" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("Address3") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemCountry" runat="server" 
                                        ClientInstanceName="lblitemCountry" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Country">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemCountryID" runat="server" 
                                        ClientInstanceName="lblitemCountryID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("CountryID") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckitemExporter" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckitemExporter" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" ReadOnly="True" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Exporter" 
                                        Value='<%# Bind("Exporter") %>'>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemPost" runat="server" 
                                        ClientInstanceName="lblitemPost" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Post code">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemPostCode" runat="server" 
                                        ClientInstanceName="lblitemPostCode" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("PostCode") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckitemCustomer" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckitemCustomer" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" ReadOnly="True" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Customer" 
                                        Value='<%# Bind("Customer") %>'>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemTel" runat="server" ClientInstanceName="lblitemTel" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Telephone">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemTelNo" runat="server" 
                                        ClientInstanceName="lblitemTelNo" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("TelNo") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckitemConsignee" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckitemConsignee" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" ReadOnly="True" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Consignee" 
                                        Value='<%# Bind("Consignee") %>'>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemFax" runat="server" ClientInstanceName="lblitemFax" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Fax">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemFaxNo" runat="server" 
                                        ClientInstanceName="lblitemFaxNo" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("FaxNo") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckitemExporter2" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckitemExporter" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" ReadOnly="True" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Publiship to arrange marine insurance" Value='<%# Bind("Insurance") %>'>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemEmail" runat="server" 
                                        ClientInstanceName="lblitemEmail" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Main email">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemMainEmail" runat="server" 
                                        ClientInstanceName="lblitemMainEmail" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("MainEmail") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dx:ASPxCheckBox ID="dxckitemSalesModule" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="ckitemSalesModule" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" ReadOnly="True" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        Text="Sales target" Value='<%# Bind("SalesModule") %>'>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemType" runat="server" 
                                        ClientInstanceName="lblitemType" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Company type">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemTypeID" runat="server" 
                                        ClientInstanceName="lblitemTypeID" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" Value='<%# Bind("TypeID") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemDelivery" runat="server" 
                                        ClientInstanceName="lblitemDelivery" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="Special delivery instructions">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="dxlblitemSpecialDeliveryInstructions" runat="server" 
                                        ClientInstanceName="lblitemSpecialDeliveryInstructions" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Text="" 
                                        Value='<%# Bind("SpecialDeliveryInstructions") %>'>
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr class="row_divider">
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
                    <table id="tblContacts" cellpadding="4px" cellspacing="0px" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                     <dx:ASPxLabel ID="dxlblitemContacts" runat="server" 
                                        ClientInstanceName="lblitemContacts" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" Font-Size="Large" Text="Contacts">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <dx:ASPxGridView ID="dxgridCompanyContacts" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="gridCompanyContacts" 
                                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" KeyFieldName="ContactID" 
                                        onrowdeleting="dxgridCompanyContacts_RowDeleting" 
                                        onrowinserting="dxgridCompanyContacts_RowInserting" 
                                        onrowupdating="dxgridCompanyContacts_RowUpdating" Width="100%">
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="0" Width="70px">
                                                <EditButton Visible="True">
                                                    <Image AlternateText="Edit" Height="18px" ToolTip="Edit" 
                                                        Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                                    </Image>
                                                </EditButton>
                                                <NewButton Visible="True">
                                                    <Image AlternateText="New" Height="18px" ToolTip="New" 
                                                        Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                                    </Image>
                                                </NewButton>
                                                <DeleteButton Visible="True">
                                                    <Image AlternateText="Delete" Height="18px" ToolTip="Delete" 
                                                        Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                                    </Image>
                                                </DeleteButton>
                                                <UpdateButton Visible="True">
                                                    <Image AlternateText="Update" Height="18px" ToolTip="Update" 
                                                        Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                                    </Image>
                                                </UpdateButton>
                                                <CancelButton Visible="True">
                                                    <Image AlternateText="Cancel" Height="18px" ToolTip="Cancel" 
                                                        Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                                                    </Image>
                                                </CancelButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Contact name" FieldName="ContactName" 
                                                VisibleIndex="1" Width="120px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Initials" FieldName="ContactInitials" 
                                                VisibleIndex="2" Width="50px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Email address" FieldName="EMail" 
                                                VisibleIndex="3" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn Caption="Order ack." FieldName="OrderAck" 
                                                VisibleIndex="4" Width="50px">
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="ContactID" ReadOnly="True" 
                                                Visible="false" VisibleIndex="5" Width="0px">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager NumericButtonCount="5" PageSize="5" Position="TopAndBottom">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsBehavior ConfirmDelete="true" />
                                        <SettingsText ConfirmDelete="Delete this contact?" />
                                        <Settings ShowFilterRow="True" />
                                        <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                            <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanelOnStatusBar>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <ImagesFilterControl>
                                            <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                            </LoadingPanel>
                                        </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                            <LoadingPanel ImageSpacing="5px">
                                            </LoadingPanel>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesPager>
                                            <PageNumber ForeColor="#3E4846">
                                            </PageNumber>
                                            <Summary ForeColor="#1E395B">
                                            </Summary>
                                        </StylesPager>
                                        <StylesEditors ButtonEditCellSpacing="0">
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                
                                </td>
                            </tr>
                        </tbody> 
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </div> 
        <!-- right column for centering form -->
        <div class="grid_3 pad_bottom">
        </div>    
        <div class="clear"></div> 
        <!-- custom command buttons for formview -->
        <!-- left column for centering menu -->
        <div class="grid_3 pad_bottom"></div>    
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <!-- command menu -->
         <div class="grid_10 pad_bottom">
             <dx:ASPxMenu ID="dxmnuFormView" runat="server" 
                ClientInstanceName="mnuFormView" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" onitemclick="dxmnuFormView_ItemClick" 
                 onitemdatabound="dxmnuFormView_ItemDataBound" RenderMode="Lightweight" 
                 ShowAsToolbar="True">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        </dx:ASPxMenu>
        </div>    
        <!-- seperate menu for client side commands -->
        <!-- end menu -->
        <!-- right column for centering menu -->
        <div class="grid_3 pad_bottom"></div>  
        <div class="clear"></div>   
        <!-- left column -->
        <div class="grid_8 pad_bottom">
        </div> 
        <div class="grid_8 pad_bottom">
            <dx:ASPxHiddenField ID="dxhfOrder" runat="server" 
                ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
        </div>    
        <!-- right column for centering leaving dates -->
        <div class="clear"></div> 
    </div><!-- end container -->
</asp:Content>

