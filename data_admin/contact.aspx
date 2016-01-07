<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

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
        function onCompanySelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('CompanyName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblCompanyAddress.SetText(s1);
       } 
        
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
                      ImageWidth="26px" NavigateUrl="~/data_admin/contact_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
            <div class="divleft">
                <dx:ASPxLabel ID="dxlblDetails" runat="server" 
                             ClientInstanceName="lblDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Contact information">
                         </dx:ASPxLabel>
            </div> 
        </div>
        <div class="grid_3 pad_bottom"></div>    
        <!-- images and text -->
        
        <!-- tabs -->
        <div class="clear"></div>
        
        <!-- left column for centering form put some padding in it as empty elements are not rendered! -->
        <div class="grid_3 pad_bottom">
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" 
                ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
       
        </div>    
        <!-- view form columns -->
        <div class="grid_10"> 
            <asp:FormView ID="fmvContact" runat="server" Width="100%" 
                Height="59px" 
                onmodechanging="fmvContact_ModeChanging" 
                ondatabound="fmvContact_DataBound">
                 <EditItemTemplate>
                     <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                   <colgroup>
                                      <col class="caption16" />
                                      <col />
                                  </colgroup>
                                      <tr class="row_divider">
                                          <td>
                                              <dx:ASPxLabel ID="dxlblContactEdit" runat="server" 
                                                  ClientInstanceName="lblContactEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Contact">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtContactEdit" runat="server" 
                                                  ClientInstanceName="txtContactEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('ContactName') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                          </td>
                                    </tr>
                                    <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblEmailEdit" runat="server" 
                                                   ClientInstanceName="lblCountry" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" Text="Email">
                                               </dx:ASPxLabel>
                                        </td>
                                        <td>
                                               <dx:ASPxTextBox ID="dxtxtEmailEdit" runat="server" 
                                                  ClientInstanceName="txtEmailEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Email') %>" Width="300px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                        </td>
                                      </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblCompanyEdit" runat="server" 
                                                 ClientInstanceName="lblCompanyEdit" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="Company">
                                             </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="dxcboCompany" ClientInstanceName="dxcboCompany" 
                                                runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                IncrementalFilteringMode="StartsWith" 
                                                TextField="CompanyName" ValueField="CompanyID" ValueType="System.Int32"  
                                                Width="300px" CallbackPageSize="25" 
                                                EnableTheming="True"
                                                Value='<%# Bind("CompanyID") %>' 
                                                onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                                onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                                DropDownRows="10">
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
                                                 <ClientSideEvents SelectedIndexChanged="function(s, e) { onCompanySelected(s, e); }" />
                                            </dx:ASPxComboBox>
                                        </td>
                                      </tr>
                                        <tr class="row_divider">
                                            <td>
                                                &nbsp;</td>
                                            <td rowspan="2">
                                                <dx:ASPxLabel ID="dxlblCompanyAddress" runat="server" ClientInstanceName="lblCompanyAddress" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue">
                                                </dx:ASPxLabel>
                                            </td>
                                   </tr>
                                   <tr class="row_divider">
                                       <td>
                                           &nbsp;</td>
                                   </tr>
                                        <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblControllingOfficeEdit" runat="server" 
                                                       ClientInstanceName="lblControllingOfficeEdit" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Controlling office">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                  <dx:ASPxComboBox ID="dxcboOffice" ClientInstanceName="cboOffice" 
                                                    runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                    IncrementalFilteringMode="StartsWith" 
                                                    TextField="OfficeName" ValueField="OfficeID" ValueType="System.Int32"  
                                                    Width="200px" CallbackPageSize="25" 
                                                    EnableTheming="True"
                                                    Value='<%# Bind("ControllingOfficeID") %>' DropDownRows="20">
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                    </LoadingPanelStyle>
                                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                    </LoadingPanelImage>
                                                </dx:ASPxComboBox>
                                            </td>
                                          </tr>
                                       <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblOrderAckEdit" runat="server" 
                                                   ClientInstanceName="dxlblOrderAck" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" Text="Order acknowledgement on">
                                               </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxCheckBox ID="dxckOrderAckEdit" ClientInstanceName="ckOrderAckEdit" 
                                                runat="server" CheckState="Unchecked" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                Value="<%# Bind('OrderAck') %>">
                                            </dx:ASPxCheckBox>
                                        </td>
                                      </tr>
                                        <tr class="row_divider">
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblWebSiteEdit" runat="server" 
                                                       ClientInstanceName="lblWebSiteEdit" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Web site access">
                                                   </dx:ASPxLabel>
                                              </td>
                                        </tr>
                                           <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblLiveEdit" runat="server" 
                                                       ClientInstanceName="lblLiveEdit" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Account live?">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="dxckLiveEdit" ClientInstanceName="ckLiveEdit" 
                                                    runat="server" CheckState="Unchecked" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                    Value="<%# Bind('Live') %>">
                                                </dx:ASPxCheckBox>
                                            </td>
                                          </tr>
                                        <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblLoginNameEdit" runat="server" 
                                                       ClientInstanceName="lblLoginNameEdit" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Log-in name">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                               <dx:ASPxTextBox ID="dxtxtLoginEdit" runat="server" 
                                                  ClientInstanceName="txtLoginEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Name') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                            </td>
                                          </tr>
                                           <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblPassEdit" runat="server" 
                                                       ClientInstanceName="lblPassEdit" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Password">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                   <dx:ASPxTextBox ID="dxtxtPassEdit" runat="server" 
                                                  ClientInstanceName="dxtxtpassEdit" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Password') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                            </td>
                                          </tr>
                                          <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblPermissionEdit" runat="server" 
                                                       ClientInstanceName="lblPermission" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Permission level">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="dxcboPermission" ClientInstanceName="cboPermission" 
                                                    runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                    IncrementalFilteringMode="StartsWith" 
                                                    TextField="name" ValueField="value" ValueType="System.Int32"  
                                                    Width="150px" CallbackPageSize="25" 
                                                    EnableTheming="True"
                                                    Value='<%# Bind("Permission") %>'>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                    </LoadingPanelStyle>
                                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                    </LoadingPanelImage>
                                                </dx:ASPxComboBox>
                                            </td>
                                          </tr>
                                                  <tr class="row_divider">
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
                          <col class="caption16" />
                          <col />
                      </colgroup>
                             <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblContact" runat="server" 
                                       ClientInstanceName="lblContact" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Contact">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblContactView" runat="server" 
                                    ClientInstanceName="lblContactView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("ContactName") %>'>
                                </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblEmail" runat="server" 
                                       ClientInstanceName="lblCountry" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Email">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblEmailView" runat="server" 
                                       ClientInstanceName="lblCountryView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('Email') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                           <tr class="row_divider">
                               <td>
                                   <dx:ASPxLabel ID="dxlblCompany" runat="server" ClientInstanceName="lblCompany" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Company">
                                   </dx:ASPxLabel>
                               </td>
                               <td rowspan="2">
                                   <dx:ASPxLabel ID="dxlblCompanyView" runat="server" 
                                       ClientInstanceName="lblCompanyView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('CompanyID') %>">
                                   </dx:ASPxLabel>
                               </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                        </tr>
                           <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblControllingOffice" runat="server" 
                                       ClientInstanceName="lblControllingOffice" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Controlling office">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblControllingOfficeView" runat="server" 
                                       ClientInstanceName="lblControllingOffice" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('ControllingOfficeID') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                           <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblOrderAck" runat="server" 
                                       ClientInstanceName="dxlblOrderAck" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Order acknowledgement on">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckOrderAckView" ClientInstanceName="ckOrderAckView" 
                                    runat="server" CheckState="Unchecked" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value="<%# Bind('OrderAck') %>">
                                </dx:ASPxCheckBox>
                            </td>
                          </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                 <dx:ASPxLabel ID="dxlblWebSite" runat="server" 
                                       ClientInstanceName="lblWebSite" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Web site access">
                                   </dx:ASPxLabel>
                              </td>
                        </tr>
                           <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblLive" runat="server" 
                                       ClientInstanceName="lblLive" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Account live?">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckLiveView" ClientInstanceName="ckLiveView" 
                                    runat="server" CheckState="Unchecked" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value="<%# Bind('Live') %>">
                                </dx:ASPxCheckBox>
                            </td>
                          </tr>
                        <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblLoginName" runat="server" 
                                       ClientInstanceName="lblLoginName" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Log-in name">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblLoginNameView" runat="server" 
                                       ClientInstanceName="lblLoginNameView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('Name') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                           <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblPass" runat="server" 
                                       ClientInstanceName="lblPass" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Password">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblPassView" runat="server" 
                                       ClientInstanceName="lblPassView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('Password') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                                 <dx:ASPxLabel ID="dxlblPermission" runat="server" 
                                       ClientInstanceName="lblPermission" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Text="Permission level">
                                   </dx:ASPxLabel>
                            </td>
                            <td>
                                   <dx:ASPxLabel ID="dxlblPermissionView" runat="server" 
                                       ClientInstanceName="dxlblPermissionView" 
                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                       CssPostfix="Office2010Blue" Value="<%# Bind('Permission') %>">
                                   </dx:ASPxLabel>
                            </td>
                          </tr>
                          <tr class="row_divider">
                            <td>
                              </td>
                            <td>
                            </td>
                          </tr>
                      </table>      
                </ItemTemplate>   
                <InsertItemTemplate>
                      <table id="tblInsert" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                                   <colgroup>
                                      <col class="caption16" />
                                      <col />
                                  </colgroup>
                                      <tr class="row_divider">
                                          <td>
                                              <dx:ASPxLabel ID="dxlblContactInsert" runat="server" 
                                                  ClientInstanceName="lblContactInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" Text="Contact">
                                              </dx:ASPxLabel>
                                          </td>
                                          <td>
                                              <dx:ASPxTextBox ID="dxtxtContactInsert" runat="server" 
                                                  ClientInstanceName="txtContactInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('ContactName') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                          </td>
                                    </tr>
                                    <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblEmailInsert" runat="server" 
                                                   ClientInstanceName="lblCountry" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" Text="Email">
                                               </dx:ASPxLabel>
                                        </td>
                                        <td>
                                               <dx:ASPxTextBox ID="dxtxtEmailInsert" runat="server" 
                                                  ClientInstanceName="txtEmailInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Email') %>" Width="300px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                        </td>
                                      </tr>
                                      <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblCompanyInsert" runat="server" 
                                                 ClientInstanceName="lblCompanyInsert" 
                                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                 CssPostfix="Office2010Blue" Text="Company">
                                             </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="dxcboCompany" ClientInstanceName="dxcboCompany" 
                                                runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                IncrementalFilteringMode="StartsWith" 
                                                TextField="CompanyName" ValueField="CompanyID" ValueType="System.Int32"  
                                                Width="300px" CallbackPageSize="25" 
                                                EnableTheming="True"
                                                Value='<%# Bind("CompanyID") %>' 
                                                onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                                onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                                DropDownRows="10">
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
                                                 <ClientSideEvents SelectedIndexChanged="function(s, e) { onCompanySelected(s, e); }" />
                                            </dx:ASPxComboBox>
                                        </td>
                                      </tr>
                                        <tr class="row_divider">
                                            <td>
                                                &nbsp;</td>
                                            <td rowspan="2">
                                                <dx:ASPxLabel ID="dxlblCompanyAddress" runat="server" ClientInstanceName="lblCompanyAddress" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue">
                                                </dx:ASPxLabel>
                                            </td>
                                   </tr>
                                   <tr class="row_divider">
                                       <td>
                                           &nbsp;</td>
                                   </tr>
                                        <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblControllingOfficeInsert" runat="server" 
                                                       ClientInstanceName="lblControllingOfficeInsert" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Controlling office">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                  <dx:ASPxComboBox ID="dxcboOffice" ClientInstanceName="cboOffice" 
                                                    runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                    IncrementalFilteringMode="StartsWith" 
                                                    TextField="OfficeName" ValueField="OfficeID" ValueType="System.Int32"  
                                                    Width="175px" CallbackPageSize="25" 
                                                    EnableTheming="True"
                                                    Value='<%# Bind("ControllingOfficeID") %>' DropDownRows="20">
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                    </LoadingPanelStyle>
                                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                    </LoadingPanelImage>
                                                </dx:ASPxComboBox>
                                            </td>
                                          </tr>
                                       <tr class="row_divider">
                                        <td>
                                             <dx:ASPxLabel ID="dxlblOrderAckInsert" runat="server" 
                                                   ClientInstanceName="dxlblOrderAck" 
                                                   CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                   CssPostfix="Office2010Blue" Text="Order acknowledgement on">
                                               </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxCheckBox ID="dxckOrderAckInsert" ClientInstanceName="ckOrderAckInsert" 
                                                runat="server" CheckState="Unchecked" 
                                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" 
                                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                Value="<%# Bind('OrderAck') %>">
                                            </dx:ASPxCheckBox>
                                        </td>
                                      </tr>
                                        <tr class="row_divider">
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblWebSiteInsert" runat="server" 
                                                       ClientInstanceName="lblWebSiteInsert" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Web site access">
                                                   </dx:ASPxLabel>
                                              </td>
                                        </tr>
                                           <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblLiveInsert" runat="server" 
                                                       ClientInstanceName="lblLiveInsert" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Account live?">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="dxckLiveInsert" ClientInstanceName="ckLiveInsert" 
                                                    runat="server" CheckState="Unchecked" 
                                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" 
                                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                    Value="<%# Bind('Live') %>">
                                                </dx:ASPxCheckBox>
                                            </td>
                                          </tr>
                                        <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblLoginNameInsert" runat="server" 
                                                       ClientInstanceName="lblLoginNameInsert" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Log-in name">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                               <dx:ASPxTextBox ID="dxtxtLoginInsert" runat="server" 
                                                  ClientInstanceName="txtLoginInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Name') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                            </td>
                                          </tr>
                                           <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblPassInsert" runat="server" 
                                                       ClientInstanceName="lblPassInsert" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Password">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                   <dx:ASPxTextBox ID="dxtxtPassInsert" runat="server" 
                                                  ClientInstanceName="dxtxtpassInsert" 
                                                  CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                  CssPostfix="Office2010Blue" 
                                                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                                  Text="<%# Bind('Password') %>" Width="200px" MaxLength="50">
                                              </dx:ASPxTextBox>
                                            </td>
                                          </tr>
                                          <tr class="row_divider">
                                            <td>
                                                 <dx:ASPxLabel ID="dxlblPermissionInsert" runat="server" 
                                                       ClientInstanceName="lblPermission" 
                                                       CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                       CssPostfix="Office2010Blue" Text="Permission level">
                                                   </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="dxcboPermission" ClientInstanceName="cboPermission" 
                                                    runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                    CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                                    IncrementalFilteringMode="StartsWith" 
                                                    TextField="name" ValueField="value" ValueType="System.Int32"  
                                                    Width="175px" CallbackPageSize="25" 
                                                    EnableTheming="True"
                                                    Value='<%# Bind("Permission") %>'>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                    <LoadingPanelStyle ImageSpacing="5px">
                                                    </LoadingPanelStyle>
                                                    <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                                    </LoadingPanelImage>
                                                </dx:ASPxComboBox>
                                            </td>
                                          </tr>
                                                  <tr class="row_divider">
                                                    <td>
                                                           &nbsp;</td>
                                                      <td>
                                                          &nbsp;</td>
                                                  </tr> 
                                  </table>
                </InsertItemTemplate>
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
        <!-- left column for centering leaving dates -->
        <div class="grid_16 pad_bottom">
           
        </div>    
        <!-- right column for centering leaving dates -->
        <div class="clear"></div> 
    </div><!-- end container -->
   
    
  
</asp:Content>


