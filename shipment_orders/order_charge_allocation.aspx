<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_charge_allocation.aspx.cs" Inherits="order_charge_allocation" %>

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

        function onButtonClick(s, e) {
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
       <div class="grid_10">
            <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
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
                    Text="| Charge allocations">
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <div class="divright">
              <dx:ASPxImage ID="dximgJobPubliship" runat="server" 
                            AlternateText="Publiship Job" ClientInstanceName="imgJobPubliship" 
                            Height="26px" ImageAlign="Top" ImageUrl="~/Images/icons/metro/checked_checkbox.png" 
                            Width="26px" ClientVisible="False">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobPubliship" runat="server" 
                            ClientInstanceName="lblJobPubliship" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Publiship job" 
                            ClientVisible="False">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
              <dx:ASPxImage ID="dximgJobHot" runat="server" AlternateText="Hot Job" 
                            ClientInstanceName="imgJobHot" Height="26px" 
                            ImageUrl="~/Images/icons/metro/matches.png" Width="26px" 
                            ImageAlign="Top" ClientVisible="False">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobHot" runat="server" ClientInstanceName="lblJobHot" 
                            ClientVisible="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Hot job" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
                         <dx:ASPxImage ID="dximgJobClosed" runat="server" AlternateText="Job Closed" 
                            ClientInstanceName="imgJobClosed" Height="26px" 
                            ImageUrl="~/Images/icons/metro/lock.png" Width="26px" 
                ImageAlign="Top" ClientVisible="False">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobClosed" runat="server" 
                            ClientInstanceName="lblJobClosed" ClientVisible="False" 
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
        <div class="grid_16 pad_bottom">
            <asp:FormView ID="fmvCharge" runat="server" width="100%" 
                onmodechanging="fmvCharge_ModeChanging" ondatabound="fmvCharge_DataBound">
                <EditItemTemplate>
                  <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		                <colgroup>
                            <col class="caption16" />
                            <col/>
                            <col class="caption16" />
                            <col/>
		                </colgroup>
                        <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblTermsEdit" runat="server" 
                                ClientInstanceName="lblTermsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Clients Terms">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboClientsTerms" runat="server" ClientInstanceName="cboClientsTerms" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                ValueType="System.Int32" Width="245px"  
                                Value='<%# Bind("ClientsTerms") %>' TextField="name" ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" /> 
                            </dx:ASPxComboBox>
                        </td>
                        <td rowspan="3">
                            <dx:ASPxLabel ID="dxlblNotesEdit" runat="server" 
                                ClientInstanceName="lblNotesEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                Text="Please note any comments concerning rates agreed with co-loaders etc.">
                            </dx:ASPxLabel>
                        </td>
                        <td rowspan="3">
                            <dx:ASPxMemo ID="dxmemCoLoaderComments" runat="server" ClientInstanceName="memCoLoaderComments" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Height="75px" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="240px" Value='<%# Bind("CoLoaderComments") %>' TabIndex="9">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblTruckingEdit" runat="server" 
                                ClientInstanceName="lblTruckingEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Trucking - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOriginTrucking" runat="server" 
                                ClientInstanceName="cboOriginTrucking" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("OriginTrucking") %>' TabIndex="1" TextField="name" 
                                ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" /> 
                            </dx:ASPxComboBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblThcOriginEdit" runat="server" 
                                ClientInstanceName="lblThcOriginEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="THC - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOrignTHC" runat="server" 
                                ClientInstanceName="cboOrignTHC" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("OrignTHC") %>' TabIndex="2" 
                                TextField="name" ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" /> 
                            </dx:ASPxComboBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblDocsEdit" runat="server" 
                                ClientInstanceName="lblDocsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Documentation - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOriginDocs" runat="server" ClientInstanceName="cboOriginDocs" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("OriginDocs") %>' TabIndex="3" TextField="name" 
                                ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblFreightEdit" runat="server" 
                                ClientInstanceName="lblFreightEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Freight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboFreight" runat="server" 
                                ClientInstanceName="cboFreight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("Freight") %>' TabIndex="4" 
                                TextField="name" ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblThcDestEdit" runat="server" 
                                ClientInstanceName="lblThcDestEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="THC - Destination">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDestTHC" runat="server" 
                                ClientInstanceName="cboDestTHC" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("DestTHC") %>' TabIndex="5" 
                                TextField="name" ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblPalletDestEdit" runat="server" 
                                ClientInstanceName="lblPalletDestEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Palletisation - Destination">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDestPalletisation" runat="server" 
                                ClientInstanceName="cboDestPalletisation" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("DestPalletisation") %>' TabIndex="6" TextField="name" 
                                ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblCustomsEdit" runat="server" 
                                ClientInstanceName="lblCustomsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Customs clearance">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboCustomsClearance" runat="server" 
                                ClientInstanceName="cboCustomsClearance" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("CustomsClearance") %>' TabIndex="7" TextField="name" 
                                ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblDeliveryEdit" runat="server" 
                                ClientInstanceName="lblDeliveryEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Delivery charges">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDeliveryCharges" runat="server" 
                                ClientInstanceName="cboDeliveryCharges" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("DeliveryCharges") %>' TabIndex="8" TextField="name" 
                                ValueField="value">
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                </table> 
                </EditItemTemplate> 
                <InsertItemTemplate>
                 <table id="tblInsert" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		                <colgroup>
                            <col class="caption16" />
                            <col/>
                            <col class="caption16" />
                            <col/>
		                </colgroup>
                        <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblTermsEdit" runat="server" 
                                ClientInstanceName="lblTermsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Clients Terms">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboClientsTerms" runat="server" ClientInstanceName="cboClientsTerms" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                ValueType="System.Int32" Width="245px"  
                                Value='<%# Bind("ClientsTerms") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                        </td>
                        <td rowspan="3">
                            <dx:ASPxLabel ID="dxlblNotesEdit" runat="server" 
                                ClientInstanceName="lblNotesEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                Text="Please note any comments concerning rates agreed with co-loaders etc.">
                            </dx:ASPxLabel>
                        </td>
                        <td rowspan="3">
                            <dx:ASPxMemo ID="dxmemCoLoaderComments" runat="server" ClientInstanceName="memCoLoaderComments" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Height="75px" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="240px" Value='<%# Bind("CoLoaderComments") %>'>
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblTruckingEdit" runat="server" 
                                ClientInstanceName="lblTruckingEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Trucking - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOriginTrucking" runat="server" 
                                ClientInstanceName="cboOriginTrucking" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("OriginTrucking") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblThcOriginEdit" runat="server" 
                                ClientInstanceName="lblThcOriginEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="THC - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOrignTHC" runat="server" 
                                ClientInstanceName="cboOrignTHC" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("OrignTHC") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblDocsEdit" runat="server" 
                                ClientInstanceName="lblDocsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Documentation - Origin">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboOriginDocs" runat="server" ClientInstanceName="cboOriginDocs" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("OriginDocs") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblFreightEdit" runat="server" 
                                ClientInstanceName="lblFreightEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Freight">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboFreight" runat="server" 
                                ClientInstanceName="cboFreight" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("Freight") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr> 
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblThcDestEdit" runat="server" 
                                ClientInstanceName="lblThcDestEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="THC - Destination">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDestTHC" runat="server" 
                                ClientInstanceName="cboDestTHC" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  Value='<%# Bind("DestTHC") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblPalletDestEdit" runat="server" 
                                ClientInstanceName="lblPalletDestEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Palletisation - Destination">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDestPalletisation" runat="server" 
                                ClientInstanceName="cboDestPalletisation" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("DestPalletisation") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr class="row_divider">
                        <td>
                            <dx:ASPxLabel ID="dxlblCustomsEdit" runat="server" 
                                ClientInstanceName="lblCustomsEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Customs clearance">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboCustomsClearance" runat="server" 
                                ClientInstanceName="cboCustomsClearance" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("CustomsClearance") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                     <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblDeliveryEdit" runat="server" 
                                ClientInstanceName="lblDeliveryEdit" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" Text="Delivery charges">
                            </dx:ASPxLabel>
                         </td>
                        <td>
                            <dx:ASPxComboBox ID="dxcboDeliveryCharges" runat="server" 
                                ClientInstanceName="cboDeliveryCharges" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue"  
                                IncrementalFilteringMode="StartsWith" Spacing="0" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                Width="200px"  
                                Value='<%# Bind("DeliveryCharges") %>'>
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
                                <ClientSideEvents ButtonClick="onButtonClick" />
                            </dx:ASPxComboBox>
                         </td>
                        <td></td>
                        <td></td>
                    </tr>  
                </table> 
                </InsertItemTemplate> 
                <ItemTemplate>
                    <table id="tblView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
		            <colgroup>
                        <col class="caption16" />
                        <col/>
                        <col class="caption16" />
                        <col/>
		            </colgroup>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblTermsView" runat="server" 
                                    ClientInstanceName="lblTermsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Clients Terms">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblClientsTermsView" runat="server" 
                                    ClientInstanceName="lblClientsTermsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("ClientsTerms") %>'>
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblNotesView" runat="server" 
                                    ClientInstanceName="lblNotesView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    Text="Please note any comments concerning rates agreed with co-loaders etc.">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblCoLoaderCommentsView" runat="server" 
                                    ClientInstanceName="lblCoLoaderCommentsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("CoLoaderComments") %>'>
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblTruckingView" runat="server" 
                                    ClientInstanceName="lblTruckingView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Trucking - Origin">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginTruckingView" runat="server" 
                                    ClientInstanceName="lblOriginTruckingView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("OriginTrucking") %>'>
                                </dx:ASPxLabel>
                             </td>
                        </tr> 
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblThcOriginView" runat="server" 
                                    ClientInstanceName="lblThcOriginView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="THC - Origin">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOrignTHCView" runat="server" 
                                    ClientInstanceName="lblOrignTHCView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("OrignTHC") %>'>
                                </dx:ASPxLabel>
                             </td>
                        </tr> 
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblDocsView" runat="server" 
                                    ClientInstanceName="lblDocsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Documentation - Origin">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginDocsView" runat="server" 
                                    ClientInstanceName="lblOriginDocsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("OriginDocs") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr> 
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblFreightTextView" runat="server" 
                                    ClientInstanceName="lblFreightTextView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Freight">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblFreightView" runat="server" 
                                    ClientInstanceName="lblFreightView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("Freight") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr> 
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblThcDestView" runat="server" 
                                    ClientInstanceName="lblThcDestView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="THC - Destination">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDestTHCView" runat="server" 
                                    ClientInstanceName="lblDestTHCView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DestTHC") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr>  
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblPalletDestView" runat="server" 
                                    ClientInstanceName="lblPalletDestView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Palletisation - Destination">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDestPalletisationView" runat="server" 
                                    ClientInstanceName="lblDestPalletisationView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DestPalletisation") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr>  
                         <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblCustomsView" runat="server" 
                                    ClientInstanceName="lblCustomsView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Customs clearance">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblCustomsClearanceView" runat="server" 
                                    ClientInstanceName="lblCustomsClearanceView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("CustomsClearance") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr>  
                         <tr>
                            <td>
                                <dx:ASPxLabel ID="dxlblDeliveryView" runat="server" 
                                    ClientInstanceName="lblDeliveryView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Delivery charges">
                                </dx:ASPxLabel>
                             </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDeliveryChargesView" runat="server" 
                                    ClientInstanceName="lblDeliveryChargesView" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text='<%# Bind("DeliveryCharges") %>'>
                                </dx:ASPxLabel>
                             </td>
                            <td></td>
                            <td></td>
                        </tr>  
                    </table> 
                </ItemTemplate> 
            </asp:FormView>
            <div>
                <!-- custom command butons for formview -->
                <dx:ASPxMenu ID="dxmnuData" runat="server" 
                ClientInstanceName="mnuData" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" onitemclick="dxmnuData_ItemClick" 
                 onitemdatabound="dxmnuData_ItemDataBound">
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
        
        <div class="clear"></div>
         
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
    </div><!-- end container -->
        
    
  
</asp:Content>


