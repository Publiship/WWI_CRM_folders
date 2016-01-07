<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_name_address.aspx.cs" Inherits="Popupcontrol_order_name_address" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
   <link rel="stylesheet" href="../App_Themes/960gs12col_fixed.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    
    <script type="text/javascript">
        // <![CDATA[
        function onMenuItemClick(s, e) {
            if (e.item.name == "miClose") {
                window.parent.pcPodEdit.HideWindow(window.parent.pcPodEdit.GetWindowByName('CompanyDetails'));
                //do we need to do force a callback - it causes problems if parent form is in insert mode
                //window.parent.onWindowCallBack();
            } //end if  
        }
        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container_12">
        <div class="grid_12">
          <asp:FormView ID="fmvAddressBook" runat="server" BorderWidth="0px" 
                BorderStyle="None" width="100%" Height="100px" 
                onmodechanging="fmvAddressBook_ModeChanging" 
                oniteminserting="fmvAddressBook_ItemInserting" DataKeyNames="CompanyID" 
                onitemupdating="fmvAddressBook_ItemUpdating" 
                ondatabound="fmvAddressBook_DataBound">
            <EditItemTemplate>
                <table id="editAddress" cellpadding="4px" cellspacing="0px" width="100%">
                <tbody>
                <tr>
                    <td>   
                        <dx:ASPxLabel ID="dxlbleditAddressBook" runat="server" 
                                 ClientInstanceName="lbleditOrderDetails" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                Text="Company No.">
                             </dx:ASPxLabel></td>
                    <td>  
                        <dx:ASPxLabel ID="dxlbleditCompanyID" runat="server" 
                                 ClientInstanceName="lbleditCompanyID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                                 Value='<%# Bind("CompanyID") %>' Text="company ID">
                             </dx:ASPxLabel></td>
                    <td>&nbsp;</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>  
                        <dx:ASPxLabel ID="dxlbleditName" runat="server" ClientInstanceName="lbleditName" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company name">
                            </dx:ASPxLabel></td>
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
                <tr><td>
                            <dx:ASPxLabel ID="dxlbleditAddress1" runat="server" 
                                ClientInstanceName="lbleditAddress1" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 1">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress1" runat="server" 
                                ClientInstanceName="txtAddress1" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address1") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
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
                 <tr><td>
                            <dx:ASPxLabel ID="dxlbleditAddress2" runat="server" 
                                ClientInstanceName="lbleditAddress2" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 2">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress2" runat="server" 
                                ClientInstanceName="txtAddress2" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address2") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
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
                  <tr><td>
                            <dx:ASPxLabel ID="dxlbleditAddress3" runat="server" 
                                ClientInstanceName="lbleditAddress3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 3">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress3" runat="server" 
                                ClientInstanceName="txtAddress3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address3") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
                            &nbsp;</td>
                      <td>
                          &nbsp;</td>
                    </tr>
                   <tr><td>
                            <dx:ASPxLabel ID="dxlbleditCountry" runat="server" ClientInstanceName="lbleditCountry" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Country">
                            </dx:ASPxLabel>
                        </td><td>
                             <dx:ASPxComboBox ID="dxcboCountry" runat="server" CallbackPageSize="15" 
                            ClientInstanceName="cboCountry" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                            IncrementalFilteringMode="StartsWith" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                            ValueType="System.Int32" Width="200px"  
                                 Spacing="0">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                             <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                        </dx:ASPxComboBox>
                        </td><td>
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
                    <tr><td>
                            <dx:ASPxLabel ID="dxlbleditPostCode" runat="server" 
                                ClientInstanceName="lbleditPostCode" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Post code">
                            </dx:ASPxLabel>
                        </td><td>
                            <dx:ASPxTextBox ID="dxtxtPostCode" runat="server" 
                                ClientInstanceName="txtPostCode" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("PostCode") %>' Width="100px">
                            </dx:ASPxTextBox>
                        </td><td>&nbsp;</td>
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
                     <tr><td>
                            <dx:ASPxLabel ID="dxlbleditTelNo" runat="server" ClientInstanceName="lbleditTelNo" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Telephone">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtTelNo" runat="server" ClientInstanceName="txtTelNo" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("TelNo") %>' Width="150px">
                            </dx:ASPxTextBox>
                         </td><td>&nbsp;</td>
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
                      <tr><td>
                            <dx:ASPxLabel ID="dxlbleditFax" runat="server" ClientInstanceName="lbleditFax" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Fax">
                            </dx:ASPxLabel>
                          </td><td>
                            <dx:ASPxTextBox ID="dxtxtFax" runat="server" ClientInstanceName="txtFax" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("FaxNo") %>' Width="150px">
                            </dx:ASPxTextBox>
                          </td><td>&nbsp;</td>
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
                       <tr><td>
                            <dx:ASPxLabel ID="dxlbleditEmail" runat="server" ClientInstanceName="lbleditEmail" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Main email">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxTextBox ID="dxtxtEmail" runat="server" ClientInstanceName="txtEmail" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("MainEmail") %>' Width="200px">
                            </dx:ASPxTextBox>
                           </td><td>&nbsp;</td>
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
                       <tr><td>
                            <dx:ASPxLabel ID="dxlbleditType" runat="server" ClientInstanceName="lbleditType" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company type">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxComboBox ID="dxcboCompanyType" runat="server" CallbackPageSize="25" 
                                ClientInstanceName="cboCompanyType" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" DropDownRows="20" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("TypeID") %>' ValueType="System.Int32" Width="200px" 
                                TextField="TypeName" ValueField="TypeID" Spacing="0">
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                </LoadingPanelImage>
                            </dx:ASPxComboBox>
                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                       <tr><td>
                            <dx:ASPxLabel ID="dxlbleditDelivery" runat="server" ClientInstanceName="lbleditDelivery" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Special delivery instructions">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxMemo ID="dxmemoDelivery" runat="server" 
                                ClientInstanceName="memoDelivery" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Height="71px" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("SpecialDeliveryInstructions") %>' Width="200px">
                            </dx:ASPxMemo>
                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                           <dx:ASPxLabel ID="dxlbleditContacts" runat="server" 
                                 ClientInstanceName="lbleditContacts" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Large" 
                                    Text="Contacts">
                             </dx:ASPxLabel></td>
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
                <tbody>
                <tr>
                    <td>   
                        <dx:ASPxLabel ID="dxlblinsertAddressBook" runat="server" 
                                 ClientInstanceName="lblinsertOrderDetails" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                Text="Company No.">
                             </dx:ASPxLabel></td>
                    <td>  
                        <dx:ASPxLabel ID="dxlblinsertCompanyID" runat="server" 
                                 ClientInstanceName="lblinsertCompanyID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                                 Value='<%# Bind("CompanyID") %>' Text="New">
                             </dx:ASPxLabel></td>
                    <td>&nbsp;</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>  
                        <dx:ASPxLabel ID="dxlblinsertName" runat="server" ClientInstanceName="lblinsertName" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company name">
                            </dx:ASPxLabel></td>
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
                            <dx:ASPxLabel ID="dxlblinsertPalletSpec" runat="server" 
                                ClientInstanceName="lblinsertPalletSpec" 
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
                <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertAddress1" runat="server" 
                                ClientInstanceName="lblinsertAddress1" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 1">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress1" runat="server" 
                                ClientInstanceName="txtAddress1" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address1") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
                            <dx:ASPxLabel ID="dxlblinsertMaxWeight" runat="server" 
                                ClientInstanceName="lblinsertMaxWeight" 
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
                 <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertAddress2" runat="server" 
                                ClientInstanceName="lblinsertAddress2" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 2">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress2" runat="server" 
                                ClientInstanceName="txtAddress2" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address2") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
                            <dx:ASPxLabel ID="dxlblinsertMaxHeight" runat="server" 
                                ClientInstanceName="lblinsertMaxHeight" 
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
                  <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertAddress3" runat="server" 
                                ClientInstanceName="lblinsertAddress3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 3">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtAddress3" runat="server" 
                                ClientInstanceName="txtAddress3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("Address3") %>' Width="200px">
                            </dx:ASPxTextBox>
                         </td><td>
                            &nbsp;</td>
                      <td>
                          &nbsp;</td>
                    </tr>
                   <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertCountry" runat="server" ClientInstanceName="lblinsertCountry" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Country">
                            </dx:ASPxLabel>
                        </td><td>
                             <dx:ASPxComboBox ID="dxcboCountry" runat="server" CallbackPageSize="15" 
                            ClientInstanceName="cboCountry" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                            IncrementalFilteringMode="StartsWith" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                            ValueType="System.Int32" Width="200px"  
                                 Spacing="0">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                             <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                        </dx:ASPxComboBox>
                        </td><td>
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
                    <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertPostCode" runat="server" 
                                ClientInstanceName="lblinsertPostCode" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Post code">
                            </dx:ASPxLabel>
                        </td><td>
                            <dx:ASPxTextBox ID="dxtxtPostCode" runat="server" 
                                ClientInstanceName="txtPostCode" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("PostCode") %>' Width="100px">
                            </dx:ASPxTextBox>
                        </td><td>&nbsp;</td>
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
                     <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertTelNo" runat="server" ClientInstanceName="lblinsertTelNo" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Telephone">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxTextBox ID="dxtxtTelNo" runat="server" ClientInstanceName="txtTelNo" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("TelNo") %>' Width="150px">
                            </dx:ASPxTextBox>
                         </td><td>&nbsp;</td>
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
                      <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertFax" runat="server" ClientInstanceName="lblinsertFax" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Fax">
                            </dx:ASPxLabel>
                          </td><td>
                            <dx:ASPxTextBox ID="dxtxtFax" runat="server" ClientInstanceName="txtFax" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("FaxNo") %>' Width="150px">
                            </dx:ASPxTextBox>
                          </td><td>&nbsp;</td>
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
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertEmail" runat="server" ClientInstanceName="lblinsertEmail" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Main email">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxTextBox ID="dxtxtEmail" runat="server" ClientInstanceName="txtEmail" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("MainEmail") %>' Width="200px">
                            </dx:ASPxTextBox>
                           </td><td>&nbsp;</td>
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
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertType" runat="server" ClientInstanceName="lblinsertType" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company type">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxComboBox ID="dxcboCompanyType" runat="server" CallbackPageSize="25" 
                                ClientInstanceName="cboCompanyType" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" DropDownRows="20" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("TypeID") %>' ValueType="System.Int32" Width="200px" 
                                TextField="TypeName" ValueField="TypeID" Spacing="0">
                                <LoadingPanelStyle ImageSpacing="5px">
                                </LoadingPanelStyle>
                                <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                </LoadingPanelImage>
                            </dx:ASPxComboBox>
                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblinsertDelivery" runat="server" ClientInstanceName="lblinsertDelivery" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Special delivery instructions">
                            </dx:ASPxLabel>
                           </td><td>
                            <dx:ASPxMemo ID="dxmemoDelivery" runat="server" 
                                ClientInstanceName="memoDelivery" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Height="71px" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Value='<%# Bind("SpecialDeliveryInstructions") %>' Width="200px">
                            </dx:ASPxMemo>
                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                    </tbody>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
               <table id="itemAddress" cellpadding="4px" cellspacing="0px" width="100%">
                <tbody>
                <tr>
                    <td>   
                        <dx:ASPxLabel ID="dxlblitemAddressBook" runat="server" 
                                 ClientInstanceName="lblitemOrderDetails" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                Text="Company No.">
                             </dx:ASPxLabel></td>
                    <td>  
                        <dx:ASPxLabel ID="dxlblitemCompanyID" runat="server" 
                                 ClientInstanceName="lblitemCompanyID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Font-Size="Larger" 
                                 Value='<%# Bind("CompanyID") %>' Text="">
                             </dx:ASPxLabel></td>
                    <td>&nbsp;</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>  
                        <dx:ASPxLabel ID="dxlblitemName" runat="server" ClientInstanceName="lblitemName" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company name">
                            </dx:ASPxLabel></td>
                    <td>
				 <dx:ASPxLabel ID="dxlblitemCompanyName" runat="server" 
                                 ClientInstanceName="lblitemCompanyName" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("CompanyName") %>' Text="">
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
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("PalletDims") %>' Text="">
                             </dx:ASPxLabel>
                    </td>
                </tr>
                <tr><td>
                            <dx:ASPxLabel ID="dxlblitemAddr1" runat="server" 
                                ClientInstanceName="lblitemAddr1" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 1">
                            </dx:ASPxLabel>
                         </td><td>
					<dx:ASPxLabel ID="dxlblitemAddress1" runat="server" 
                                 ClientInstanceName="lblitemAddress1" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("Address1") %>' Text="">
                             </dx:ASPxLabel>
                         </td><td>
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
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("MaxPalletWeight") %>' Text="">
                             </dx:ASPxLabel>
			  </td>
                    </tr>
                 <tr><td>
                            <dx:ASPxLabel ID="dxlblitemAddr2" runat="server" 
                                ClientInstanceName="lblitemAddr2" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 2">
                            </dx:ASPxLabel>
                         </td><td>
                            <dx:ASPxLabel ID="dxlblitemAddress2" runat="server" 
                                 ClientInstanceName="lblitemAddress2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("Address2") %>' Text="">
                             </dx:ASPxLabel>

                         </td><td>
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
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("MaxPalletHeight") %>' Text="">
                             </dx:ASPxLabel>
                     </td>
                    </tr>
                  <tr><td>
                            <dx:ASPxLabel ID="dxlblitemAddr3" runat="server" 
                                ClientInstanceName="lblitemAddr3" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Address 3">
                            </dx:ASPxLabel>
                         </td><td>
                           <dx:ASPxLabel ID="dxlblitemAddress3" runat="server" 
                                 ClientInstanceName="lblitemAddress3" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("Address3") %>' Text="">
                             </dx:ASPxLabel>

                         </td><td>
                            &nbsp;</td>
                      <td>
                          &nbsp;</td>
                    </tr>
                   <tr><td>
                            <dx:ASPxLabel ID="dxlblitemCountry" runat="server" ClientInstanceName="lblitemCountry" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Country">
                            </dx:ASPxLabel>
                        </td><td>
                           
				<dx:ASPxLabel ID="dxlblitemCountryID" runat="server" 
                                 ClientInstanceName="lblitemCountryID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("CountryID") %>' Text="">
                             </dx:ASPxLabel>
                        </td><td>
                            &nbsp;</td>
                       <td>
                       <dx:ASPxCheckBox ID="dxckitemExporter" runat="server" 
                            CheckState="Unchecked" ReadOnly="True"
                            ClientInstanceName="ckitemExporter"
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                               Text="Exporter" Value='<%# Bind("Exporter") %>'>
                        </dx:ASPxCheckBox>
	                 </td>
                    </tr>
                    <tr><td>
                            <dx:ASPxLabel ID="dxlblitemPost" runat="server" 
                                ClientInstanceName="lblitemPost" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Post code">
                            </dx:ASPxLabel>
                        </td><td>
                            <dx:ASPxLabel ID="dxlblitemPostCode" runat="server" 
                                 ClientInstanceName="lblitemPostCode" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("PostCode") %>' Text="">
                             </dx:ASPxLabel>

                        </td><td>&nbsp;</td>
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
                     <tr><td>
                            <dx:ASPxLabel ID="dxlblitemTel" runat="server" ClientInstanceName="lblitemTel" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Telephone">
                            </dx:ASPxLabel>
                         </td><td>
					<dx:ASPxLabel ID="dxlblitemTelNo" runat="server" 
                                 ClientInstanceName="lblitemTelNo" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("TelNo") %>' Text="">
                             </dx:ASPxLabel>
                         </td><td>&nbsp;</td>
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
                      <tr><td>
                            <dx:ASPxLabel ID="dxlblitemFax" runat="server" ClientInstanceName="lblitemFax" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Fax">
                            </dx:ASPxLabel>
                          </td><td>
					<dx:ASPxLabel ID="dxlblitemFaxNo" runat="server" 
                                 ClientInstanceName="lblitemFaxNo" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("FaxNo") %>' Text="">
                             </dx:ASPxLabel>
                          </td><td>&nbsp;</td>
                          <td>
                              <dx:ASPxCheckBox ID="dxckitemExporter2" runat="server" CheckState="Unchecked" 
                                  ClientInstanceName="ckitemExporter" 
                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                  CssPostfix="Office2010Blue" ReadOnly="True" 
                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Publiship to arrange marine insurance" 
                                  Value='<%# Bind("Insurance") %>'>
                              </dx:ASPxCheckBox>
                          </td>
                    </tr>
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblitemEmail" runat="server" ClientInstanceName="lblitemEmail" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Main email">
                            </dx:ASPxLabel>
                           </td><td>
					<dx:ASPxLabel ID="dxlblitemMainEmail" runat="server" 
                                 ClientInstanceName="lblitemMainEmail" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("MainEmail") %>' Text="">
                             </dx:ASPxLabel>
                           </td><td>&nbsp;</td>
                           <td>
				               <dx:ASPxCheckBox ID="dxckitemSalesModule" runat="server" CheckState="Unchecked" 
                                   ClientInstanceName="ckitemSalesModule" 
                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                   CssPostfix="Office2010Blue" ReadOnly="True" 
                                   SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Text="Sales target" 
                                   Value='<%# Bind("SalesModule") %>'>
                               </dx:ASPxCheckBox>
                           </td>
                    </tr>
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblitemType" runat="server" ClientInstanceName="lblitemType" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Company type">
                            </dx:ASPxLabel>
                           </td><td>
					<dx:ASPxLabel ID="dxlblitemTypeID" runat="server" 
                                 ClientInstanceName="lblitemTypeID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("TypeID") %>' Text="">
                             </dx:ASPxLabel>

                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                       <tr><td>
                            <dx:ASPxLabel ID="dxlblitemDelivery" runat="server" ClientInstanceName="lblitemDelivery" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Special delivery instructions">
                            </dx:ASPxLabel>
                           </td><td>
					<dx:ASPxLabel ID="dxlblitemSpecialDeliveryInstructions" runat="server" 
                                 ClientInstanceName="lblitemSpecialDeliveryInstructions" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 Value='<%# Bind("SpecialDeliveryInstructions") %>' Text="">
                             </dx:ASPxLabel>
                           </td><td>&nbsp;</td>
                           <td>
                               &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblitemContacts" runat="server" 
                                ClientInstanceName="lblitemContacts" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Font-Size="Large" Text="Contacts">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <dx:ASPxGridView ID="dxgridCompanyContacts" runat="server" 
                                AutoGenerateColumns="False" ClientInstanceName="gridCompanyContacts" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" KeyFieldName="ContactID" Width="100%" 
                                onrowdeleting="dxgridCompanyContacts_RowDeleting" 
                                onrowinserting="dxgridCompanyContacts_RowInserting" 
                                onrowupdating="dxgridCompanyContacts_RowUpdating">
                                <Columns>
                                   <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="70px">
                                   <EditButton Visible="True">
                                        <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                        </Image>
                                   </EditButton>
                                    <NewButton Visible="True">
                                        <Image AlternateText="New" ToolTip="New" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image AlternateText="Delete" ToolTip="Delete" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                        </Image>
                                    </DeleteButton>
                                    <UpdateButton Visible="True">
                                         <Image AlternateText="Update" ToolTip="Update" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                         </Image>
                                    </UpdateButton> 
                                    <CancelButton Visible="True">
                                       <Image AlternateText="Cancel" ToolTip="Cancel" Height="18px" 
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
                                        VisibleIndex="5" Width="0px" Visible="false">
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
         <div class="clear"></div> 
        <!-- grid for contact names -->
        <div class="clear"></div> 
        <div class="grid_12">
             <dx:ASPxMenu ID="dxmenuFormView" runat="server" 
                ClientInstanceName="menuFormView" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                onitemclick="dxeditFormMenu_ItemClick" AutoSeparators="RootOnly">
                           <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                           <LoadingPanelStyle ImageSpacing="5px">
                           </LoadingPanelStyle>
                           <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                           </LoadingPanelImage>
                           <Items>
                                <dx:MenuItem Text="New" Name="miNew" >
                                    <Image Height="26px" Url="~/Images/icons/metro/new.png" Width="23px">
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Edit" Name="miEdit" >
                                    <Image Height="26px" Url="~/Images/icons/metro/edit.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Delete" Name="miDelete">
                                    <Image Height="26px" Url="~/Images/icons/metro/delete.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Update" Name="miUpdate" BeginGroup="true" >
                                    <Image Height="26px" Url="~/Images/icons/metro/save.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Save" Name="miSave" >
                                    <Image Height="26px" Url="~/Images/icons/metro/save.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Cancel" Name="miCancel">
                                    <Image Height="26px" Url="~/Images/icons/metro/cancel.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                                   <dx:MenuItem Text="Close" Name="miClose">
                                    <Image Height="26px" Url="~/Images/icons/metro/close.png" Width="26px">
                                    </Image>
                                </dx:MenuItem>
                            </Items>
                      <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                           <ClientSideEvents ItemClick="onMenuItemClick" />
                      <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
            </dx:ASPxMenu>
        </div>
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
        </dx:ASPxHiddenField>
    </div> 
    </form>
</body>
</html>
