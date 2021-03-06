<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order.aspx.cs" Inherits="order" %>

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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        //onWindowCallBack is called by popupwindows so we can update formview
        function onWindowCallBack(s) {
            window.location.reload(true);
        }
        
        function onCompanySelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblCompanyAddress.SetText(s1);
            //requery client contact
            requeryContact(s.GetValue().toString()); 
        }

        function requeryContact(companyId) {
            cboClientContact.PerformCallback(companyId);
            cboClientContact.SetSelectedIndex(-1);
            //var s1 = cboClientContact.GetSelectedItem().GetColumnText('Email');
            lblContactEmail.SetText('');
         }
        
        function onPrinterSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('PrinterAdd1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterAdd2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterAdd3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterCountry');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('PrinterTel');

            lblPrinterAddress.SetText(s1);
        }

        function onContactSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Email');

            lblContactEmail.SetText(s1);
        }

        function onOriginAgentSelected(s, e) { 
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('OriginAgentAddress1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentAddress2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentAddress3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentCountry');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('OriginAgentTel');

            lblOriginAgentAddress.SetText(s1);
            requeryOriginController(s.GetValue().toString());
        }

        function requeryOriginController(originAgentId) {
            cboOriginController.PerformCallback(originAgentId);
            cboOriginController.SetSelectedIndex(-1);
        }

        function onCountryChanged(cbcountry) {
            cboOrigin.SetSelectedIndex(-1);
            cboOriginPort.SetSelectedIndex(-1);
            //cbkOriginGroup.PerformCallback(1);
            cboOrigin.PerformCallback();
        }
        
        function onOriginChanged(cborigin) {
            cboOriginPort.SetSelectedIndex(-1);
            //cbkOriginGroup.PerformCallback(2);
            cboOriginPort.PerformCallback();
        }

        function OnDocsValueChanged(s, e) {
            //var checkState = ckPalletised.GetCheckState();
            var checked = s.GetChecked();
            //checkStateLabel.SetText("CheckState = " + checkState);
            //checkedLabel.SetText("Checked = " + checked);
            if (checked == true) {
                var d = new Date();
                dtDocsApproved.SetDate(d);
            }
            else {
                dtDocsApproved.SetDate(null); 
            }
        }

        function onPublishipJobValueChanged(s, e) {
            var checked = s.GetChecked();
            imgJobPubliship.SetVisible(checked);
            lblJobPubliship.SetVisible(checked)
        }
        
        function onHotJobValueChanged(s, e) {
            var checked = s.GetChecked();
            imgJobHot.SetVisible(checked);
            lblJobHot.SetVisible(checked)
        }

        function onJobClosedValueChanged(s, e) {
            var checked = s.GetChecked();
            imgJobClosed.SetVisible(checked);
            lblJobClosed.SetVisible(checked);
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

        function onEditCompany(s, e) {
            var pid = cboCompany.GetValue();
            if (pid != null) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);

                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert('You have not selected a client');
            }
        }

        function onNewCompany(s, e) {

            var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
            pcPodEdit.SetWindowContentUrl(winEdit, '');
            pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Insert&pid=' + "new");

            pcPodEdit.ShowWindow(winEdit);
        }

        function onEditPrinter(s, e) {
            var pid = cboPrinter.GetValue();
            if (pid != null) {
                var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                pcPodEdit.SetWindowContentUrl(winEdit, '');
                pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Edit&pid=' + pid);

                pcPodEdit.ShowWindow(winEdit);
            }
            else {
                alert('You have not selected a printer');
            }
        }

        function onNewPrinter(s, e) {

            var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
            pcPodEdit.SetWindowContentUrl(winEdit, '');
            pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=Insert&pid=' + "new&type=Printer");

            pcPodEdit.ShowWindow(winEdit);
        }
        //generic function on combo button clicks
        function onComboButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text 
                s.SetText('');
                s.SetSelectedIndex(-1);
            }
        }

        //company combo button click 
        function onCompanyButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and clear client contact
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblCompanyAddress.SetText('');
                cboClientContact.SetText('');
                cboClientContact.SetSelectedIndex(-1);
                lblContactEmail.SetText('');
            }
        }

        //company combo button click 
        function onPrinterButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblPrinterAddress.SetText('');
            }
        }

        //company combo button click 
        function onAgentAtOriginButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblOriginAgentAddress.SetText('');
                cboOriginController.SetText('');
                cboOriginController.SetSelectedIndex(-1);
            }
        }

        //client contact buttons
        function onClientContactButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text 
                s.SetText('');
                s.SetSelectedIndex(-1);
            }
            else {
                //clear text in case contact is deleted from name and address book
                s.SetText('');
                s.SetSelectedIndex(-1);
                
                //new contact open name and address book in readonly mode
                var pid = cboCompany.GetValue();
                if (pid != null) {
                    var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
                    pcPodEdit.SetWindowContentUrl(winEdit, '');
                    pcPodEdit.SetWindowContentUrl(winEdit, '../Popupcontrol/order_name_address.aspx?mode=ReadOnly&pid=' + pid);

                    pcPodEdit.ShowWindow(winEdit);
                }
                else {
                    alert('You must select a client first'); 
                }
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
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="Order No|">
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
                    Text="| Office">
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
        
        <!-- edit/insert/view order details -->
        <div class="grid_16 pad bottom"> 
        <asp:FormView ID="formOrder" runat="server" DataKeyNames="OrderID" width="100%" 
                 ondatabound="formOrder_DataBound"> 
          <ItemTemplate>
                <table id="viewOrder" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                    <colgroup>
                        <col class="caption10" />
                        <col />
                        <col class="caption12" />
                        <col />
                        <col class="caption12" />
                        <col />
                    </colgroup>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblPubliship" runat="server" 
                                    AssociatedControlID="dxckEditPubliship" ClientInstanceName="lblPubliship" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Publiship order" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditJobPubliship" runat="server" 
                                    CheckState="Unchecked" ClientInstanceName="ckJobPubliship" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("PublishipOrder") %>'>
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckJobClosed" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Text="Job closed" TextAlign="Left" Value='<%# Bind("JobClosed") %>' 
                                    ClientVisible="False">
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOfficeIndicator" runat="server" 
                                    ClientInstanceName="lblOfficeIndicator" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("OfficeIndicator") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDateCreatedTitle" runat="server" 
                                    ClientInstanceName="lblDateCreatedTitle" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Date created" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDateCreated" runat="server" 
                                    ClientInstanceName="lblDateCreated" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("DateOrderCreated") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblController" runat="server" 
                                    AssociatedControlID="dxcboController" ClientInstanceName="lblController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Order controller" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewController" runat="server" 
                                    ClientInstanceName="lblViewController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblContact" runat="server" 
                                    AssociatedControlID="dxcboClientContact" ClientInstanceName="lblContact" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Client contact" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewContact" runat="server" 
                                    ClientInstanceName="lblViewContact" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblIssueDl" runat="server" 
                                    AssociatedControlID="dxckEditIssueDl" ClientInstanceName="lblIssueDl" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Issue express D/L" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditIssueDl" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckIssueDl" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("ExpressBL") %>' Width="100px">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblOps" runat="server" AssociatedControlID="dxcboOps" 
                                    ClientInstanceName="lblOps" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableTheming="True" Text="Ops controller" 
                                    Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewOps" runat="server" ClientInstanceName="lblViewOps" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" EnableTheming="True" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblContactEmail" runat="server" 
                                    ClientInstanceName="lblContactEmail" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblFumigation" runat="server" 
                                    AssociatedControlID="dxckEditFumigation" ClientInstanceName="lblFumigation" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Fumigation certificate">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditFumigation" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckFumigation" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("FumigationCert") %>' Width="100px">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblCompany" runat="server" 
                                    AssociatedControlID="dxcboCompany" ClientInstanceName="lblCompany" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Client name">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewCompany" runat="server" 
                                    ClientInstanceName="lblViewCompany" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblPrinter" runat="server" 
                                    AssociatedControlID="dxcboPrinter" ClientInstanceName="lblPrinter" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Printer">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewPrinter" runat="server" 
                                    ClientInstanceName="lblViewPrinter" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblGSP" runat="server" AssociatedControlID="dxckEditGSP" 
                                    ClientInstanceName="lblGSP" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="GSP certificate">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditGSP" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckGSP" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("GSPCert") %>' Width="100px">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblCompanyAddress" runat="server" 
                                    ClientInstanceName="lblCompanyAddress" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Contact address" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblPrinterAddress" runat="server" 
                                    ClientInstanceName="lblPrinterAddress" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Printer address" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblPacking" runat="server" 
                                    AssociatedControlID="dxckEditPacking" ClientInstanceName="lblPacking" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Packing declaration">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditPacking" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckPacking" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("PackingDeclaration") %>' Width="100px">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td rowspan="4">
                                <dx:ASPxLabel ID="dxlblDocs" runat="server" AssociatedControlID="dxmemoDocs" 
                                    ClientInstanceName="lblDocs" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Other documents required" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="4">
                                <dx:ASPxLabel ID="dxlblViewDocs" runat="server" 
                                    AssociatedControlID="dxmemoDocs" ClientInstanceName="lblViewDocs" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("OtherDocsRequired") %>' 
                                    Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblCountry" runat="server" 
                                    AssociatedControlID="dxcboCountry" ClientInstanceName="lblCountry" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin country">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewCountry" runat="server" 
                                    ClientInstanceName="lblViewCountry" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblHotJob" runat="server" 
                                    AssociatedControlID="dxckEditHotjob" ClientInstanceName="lblhotJob" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Hot Job">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditJobHot" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckJobHot" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("HotJob") %>'>
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblOrigin" runat="server" AssociatedControlID="dxcboOrigin" 
                                    ClientInstanceName="lblOrigin" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin point">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewOrigin" runat="server" 
                                    ClientInstanceName="lblViewOrigin" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblHotJobNB1" runat="server" 
                                    ClientInstanceName="lblhotJobNB1" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Note:">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblHotJobNB2" runat="server" 
                                    ClientInstanceName="lblhotJobNB2" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="will initiate detailed tracking" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                    AssociatedControlID="dxcboOriginPort" ClientInstanceName="lblOriginPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin port">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewOriginPort" runat="server" 
                                    ClientInstanceName="lblViewOriginPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblAgentAtOrigin" runat="server" 
                                    AssociatedControlID="dxcboAgentAtOrigin" ClientInstanceName="lblAgentAtOrigin" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin agent">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewAgentAtOrigin" runat="server" 
                                    ClientInstanceName="lblViewAgentAtOrigin" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblPalletised" runat="server" 
                                    AssociatedControlID="dxckEditPalletised" ClientInstanceName="lblPalletised" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Ship pre-palletised">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditPalletised" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckPalletised" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("Palletise") %>' Width="100px">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                    AssociatedControlID="dxcboDestPort" ClientInstanceName="lblDestPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Destination port">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewDestPort" runat="server" 
                                    ClientInstanceName="lblViewDestPort" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblOriginAgentAddress" runat="server" 
                                    ClientInstanceName="lblOriginAgentAddress" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin agent address" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="2">
                                <dx:ASPxLabel ID="dxlblBookingReceived" runat="server" 
                                    AssociatedControlID="dxdtBookingReceived" 
                                    ClientInstanceName="lblBookingReceived" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Booking received from printer" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewBookingReceived" runat="server" 
                                    AssociatedControlID="dxdtBookingReceived" 
                                    ClientInstanceName="lblViewBookingReceived" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("BookingReceived") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblFinal" runat="server" AssociatedControlID="dxcboFinal" 
                                    ClientInstanceName="lblFinal" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Final destination">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewFinal" runat="server" 
                                    ClientInstanceName="lblViewFinal" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblexWorks" runat="server" 
                                    AssociatedControlID="dxdtExWorks" ClientInstanceName="lblexWorks" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Ex-works date">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewExWorks" runat="server" 
                                    ClientInstanceName="lblViewExWorks" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("ExWorksDate") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <dx:ASPxLabel ID="dxlblCargoReady" runat="server" 
                                    AssociatedControlID="dxdtCargoReady" ClientInstanceName="lblCargoReady" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Cargo ready date">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewCargoReady" runat="server" 
                                    AssociatedControlID="dxdtCargoReady" ClientInstanceName="lblViewCargoReady" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("CargoReady") %>'>
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblCustomerRef" runat="server" 
                                    ClientInstanceName="lblCustomerRef" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Customers ref">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewCustomerRef" runat="server" 
                                    ClientInstanceName="lblViewCustomerRef" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("CustomersRef") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                    ClientInstanceName="lblOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Origin controller">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewOriginController" runat="server" 
                                    ClientInstanceName="lblViewOriginController" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDueWarehouse" runat="server" 
                                    ClientInstanceName="lbldueWarehouse" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Due warehouse date">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewDueWarehouse" runat="server" 
                                    ClientInstanceName="ViewDueWarehouse" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("WarehouseDate") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                <dx:ASPxLabel ID="dxlblRemarksAgent" runat="server" 
                                    AssociatedControlID="dxmemoRemarksToAgent" ClientInstanceName="lblRemarksAgent" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Remarks to agent">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblViewRemarksAgent" runat="server" 
                                    AssociatedControlID="dxmemoRemarksToAgent" 
                                    ClientInstanceName="lblViewRemarksAgent" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("Remarks") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblRemarksToCustomer" runat="server" 
                                    AssociatedControlID="dxmemoRemarksToCustomer" 
                                    ClientInstanceName="lblRemarksToCustomer" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Remarks to customer" Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td rowspan="3">
                                <dx:ASPxLabel ID="dxlblViewRemarksToCustomer" runat="server" 
                                    AssociatedControlID="dxmemoRemarksToCustomer" 
                                    ClientInstanceName="lblViewRemarksToCustomer" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("RemarksToCustomer") %>' 
                                    Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDocsApproved" runat="server" 
                                    AssociatedControlID="dxckEditDocsApproved" ClientInstanceName="lblDocsApproved" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Documents approved">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="dxckEditDocsAppr" runat="server" CheckState="Unchecked" 
                                    ClientInstanceName="ckDocsAppr" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" ReadOnly="True" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Value='<%# Bind("DocsRcdAndApproved") %>'>
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                <dx:ASPxLabel ID="dxlblDocsApprovedDate" runat="server" 
                                    AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblDocsApprovedDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Approved date">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewDocsApprovedDate" runat="server" 
                                    AssociatedControlID="dxdtDocsApproved" 
                                    ClientInstanceName="lblViewDocsApprovedDate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("DocsApprovedDate") %>'>
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr class="row_divider">
                            <td>
                                &nbsp;</td>
                            <td>
                                <dx:ASPxLabel ID="dxlblSellingRate" runat="server" 
                                    ClientInstanceName="lblSellingRate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Selling rate">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewSellingRate" runat="server" 
                                    ClientInstanceName="lblViewSellingRate" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("Sellingrate") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblSellingAgent" runat="server" 
                                    AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblSellingAgent" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Text="Selling rate for agent">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="dxlblViewSellingAgent" runat="server" 
                                    ClientInstanceName="lblViewSellingAgent" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Value='<%# Bind("SellingrateAgent") %>' Wrap="True">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
             </table> 
          </ItemTemplate> 
          <EditItemTemplate> 
             <table id="editOrder" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                 <colgroup>
                     <col class="caption10" />
                     <col />
                     <col class="caption12" />
                     <col />
                     <col class="caption12" />
                     <col />
                 </colgroup>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOrderID" runat="server" 
                                 AssociatedControlID="dxckEditPubliship" ClientInstanceName="lblOrderID" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Order ID" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtOrderId" runat="server" ClientEnabled="False" 
                                 ClientInstanceName="txtOrderId" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value="<%# Bind('OrderID') %>" Width="150px">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblOrderNumber" runat="server" 
                                 AssociatedControlID="dxckEditPubliship" ClientInstanceName="ddxlblOrderNumber" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Order number" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtOrderId0" runat="server" ClientEnabled="False" 
                                 ClientInstanceName="txtOrderId" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value="<%# Bind('OrderNumber') %>" Width="150px" TabIndex="1">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr> 
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblPubliship" runat="server" 
                                 AssociatedControlID="dxckEditPubliship" ClientInstanceName="lblPubliship" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Publiship order" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditJobPubliship" runat="server" 
                                 CheckState="Unchecked" ClientInstanceName="ckJobPubliship" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("PublishipOrder") %>' TabIndex="2">
                                 <ClientSideEvents ValueChanged="onPublishipJobValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckJobClosed" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Text="Job closed" TextAlign="Left" Value='<%# Bind("JobClosed") %>' 
                                 TabIndex="14" ClientVisible="False">
                                 <ClientSideEvents ValueChanged="onJobClosedValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblOfficeIndicator" runat="server" 
                                 ClientInstanceName="lblOfficeIndicator" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDateCreatedTitle" runat="server" 
                                 ClientInstanceName="lblDateCreatedTitle" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Date created" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDateCreated" runat="server" 
                                 ClientInstanceName="lblDateCreated" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True" 
                                 Value='<%# Bind("DateOrderCreated") %>'>
                             </dx:ASPxLabel>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblController" runat="server" 
                                 AssociatedControlID="dxcboController" ClientInstanceName="lblController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Order controller" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboController" runat="server" callbackpagesize="50" 
                                 ClientInstanceName="cboController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Name" Value='<%# Bind("OrderControllerID") %>' 
                                 ValueField="EmployeeID" ValueType="System.Int32" Width="210px" 
                                 TabIndex="3">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton Position="Left" ToolTip="Click to remove name">
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblContact" runat="server" 
                                 AssociatedControlID="dxcboClientContact" ClientInstanceName="lblContact" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Client contact" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboClientContact" runat="server" 
                                 ClientInstanceName="cboClientContact" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" oncallback="dxcboContact_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="ContactName" Value='<%# Bind("ContactID") %>' ValueField="ContactID" 
                                 ValueType="System.Int32" Width="200px" CallbackPageSize="50" 
                                 EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
                                 TabIndex="15">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onClientContactButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onContactSelected(s, e);
                                    }" />
                                 <Columns>
                                     <dx:ListBoxColumn Caption="ContactID(Hidden)" FieldName="ContactID" 
                                         Name="colContactID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="ContactName" Name="colContactName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Email Address" FieldName="Email" Name="colEmail" 
                                         Width="175px" />
                                 </Columns>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                        <dx:EditButton>
                                            <Image Height="12px" ToolTip="Click to add new contact" Url="~/Images/icons/metro/plus2.png" 
                                                Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                            </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblIssueDl" runat="server" 
                                 AssociatedControlID="dxckEditIssueDl" ClientInstanceName="lblIssueDl" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Issue express D/L" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditIssueDl" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckIssueDl" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("ExpressBL") %>' TabIndex="21">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOps" runat="server" AssociatedControlID="dxcboOps" 
                                 ClientInstanceName="lblOps" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableTheming="True" Text="Ops controller" 
                                 Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOps" runat="server" callbackpagesize="50"
                                 ClientInstanceName="cboOps" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                 Value='<%# Bind("OperationsControllerID") %>' ValueField="EmployeeID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="4">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblContactEmail" runat="server" 
                                 ClientInstanceName="lblContactEmail" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFumigation" runat="server" 
                                 AssociatedControlID="dxckEditFumigation" ClientInstanceName="lblFumigation" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Fumigation certificate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditFumigation" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckFumigation" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("FumigationCert") %>' TabIndex="22">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCompany" runat="server" 
                                 AssociatedControlID="dxcboCompany" ClientInstanceName="lblCompany" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Client name">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboCompany" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboCompany" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="CompanyName" Value='<%# Bind("CompanyID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="5">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onCompanyButtonClick" SelectedIndexChanged="function(s, e) {
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
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons>
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblPrinter" runat="server" 
                                 AssociatedControlID="dxcboPrinter" ClientInstanceName="lblPrinter" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Printer">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboPrinter" runat="server" callbackpagesize="50" 
                                 ClientInstanceName="cboPrinter" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboprinter_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboprinter_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PrinterName" Value='<%# Bind("PrinterID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="200px" TabIndex="16">
                                 <ClientSideEvents ButtonClick="onPrinterButtonClick" SelectedIndexChanged="function(s, e) {
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
                                     <dx:ListBoxColumn Caption="Name" FieldName="PrinterName" Name="colPrinterName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="PrinterAdd1" 
                                         Name="colPrinterAdd1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="PrinterAdd2" 
                                         Name="colPrinterAdd2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="PrinterAdd3" 
                                         Name="colPrinterAdd3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="PrinterCountry" 
                                         Name="colPrinterCountry" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="PrinterTel" Name="colPrinterTel" 
                                         Width="100px" />
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
                             <dx:ASPxLabel ID="dxlblGSP" runat="server" AssociatedControlID="dxckEditGSP" 
                                 ClientInstanceName="lblGSP" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="GSP certificate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditGSP" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckGSP" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("GSPCert") %>' TabIndex="23">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxHyperLink ID="dxlinkNewCompany" runat="server" 
                                 ClientInstanceName="linkNewCompany" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                 <ClientSideEvents Click="onNewCompany" />
                             </dx:ASPxHyperLink>
                             <dx:ASPxHyperLink ID="dxlinkEditCompany" runat="server" 
                                 ClientInstanceName="linkEditCompany" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px"
                                 ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                 <ClientSideEvents Click="onEditCompany" />
                             </dx:ASPxHyperLink>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblCompanyAddress" runat="server" 
                                 ClientInstanceName="lblCompanyAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Contact address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxHyperLink ID="dxlinkNewPrinter" runat="server" 
                                 ClientInstanceName="linkNewPrinterEditForm" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                 <ClientSideEvents Click="onNewPrinter" />
                             </dx:ASPxHyperLink>
                             <dx:ASPxHyperLink ID="dxlinkEditPrinter" runat="server" 
                                 ClientInstanceName="linkEditPrinter" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                 <ClientSideEvents Click="onEditPrinter" />
                             </dx:ASPxHyperLink>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblPrinterAddress" runat="server" 
                                 ClientInstanceName="lblPrinterAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Printer address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblPacking" runat="server" 
                                 AssociatedControlID="dxckEditPacking" ClientInstanceName="lblPacking" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Packing declaration">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditPacking" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckPacking" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("PackingDeclaration") %>' TabIndex="24">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                         </td>
                         <td>
                         </td>
                         <td rowspan="4">
                             <dx:ASPxLabel ID="dxlblDocs" runat="server" AssociatedControlID="dxmemoDocs" 
                                 ClientInstanceName="lblDocs" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Other documents required" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="4">
                             <dx:ASPxMemo ID="dxmemoDocs" runat="server" ClientInstanceName="memoDocs" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="90px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("OtherDocsRequired") %>' Width="200px" TabIndex="25">
                             </dx:ASPxMemo>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                         </td>
                         <td>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCountry" runat="server" 
                                 AssociatedControlID="dxcboCountry" ClientInstanceName="lblCountry" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin country">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboCountry" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboCountry" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOrigin_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="6">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onComboButtonClick" SelectedIndexChanged="function(s, e) { onCountryChanged(s); }" />
                                 <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                 </Buttons> 
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJob" runat="server" 
                                 AssociatedControlID="dxckEditHotjob" ClientInstanceName="lblhotJob" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Hot Job">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditJobHot" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckJobHot" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("HotJob") %>' TabIndex="17">
                                 <ClientSideEvents ValueChanged="onHotJobValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOrigin" runat="server" AssociatedControlID="dxcboOrigin" 
                                 ClientInstanceName="lblOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin point">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOrigin" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOrigin_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PlaceName" Value='<%# Bind("OriginPointID") %>' ValueField="PlaceID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="7">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onComboButtonClick" SelectedIndexChanged="function(s, e) { onOriginChanged(s); }" />
                                 <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                 </Buttons> 
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJobNB1" runat="server" 
                                 ClientInstanceName="lblhotJobNB1" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Note:">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJobNB2" runat="server" 
                                 ClientInstanceName="lblhotJobNB2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="will initiate detailed tracking" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                 AssociatedControlID="dxcboOriginPort" ClientInstanceName="lblOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin port">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOriginPort" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOriginPort_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("PortID") %>' ValueField="PortID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="8">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblAgentAtOrigin" runat="server" 
                                 AssociatedControlID="dxcboAgentAtOrigin" ClientInstanceName="lblAgentAtOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin agent">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboAgentAtOrigin" runat="server" callbackpagesize="50" 
                                 ClientInstanceName="cboAgentAtOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboagentatorigin_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboagentatorigin_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="OriginAgent" Value='<%# Bind("AgentAtOriginID") %>' 
                                 ValueField="OriginAgentID" ValueType="System.Int32" Width="200px" 
                                 TabIndex="18">
                                 <ClientSideEvents ButtonClick="onAgentAtOriginButtonClick" SelectedIndexChanged="function(s, e) {
	                        onOriginAgentSelected(s, e);
                        }" />
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <Columns>
                                     <dx:ListBoxColumn Caption="OriginAgentID(Hidden)" FieldName="OriginAgentID" 
                                         Name="colOriginAgentID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="OriginAgent" Name="colOriginAgent" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="OriginAgentAddress1" 
                                         Name="colOriginAgentAdd1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="OriginAgentAddress2" 
                                         Name="colOriginAgentAdd2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="OriginAgentAddress3" 
                                         Name="colOriginAgentAdd3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="OriginAgentCountry" 
                                         Name="colOriginAgentCountry" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="OriginAgentTel" 
                                         Name="colOriginAgentTel" Width="100px" />
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
                             <dx:ASPxLabel ID="dxlblPalletised" runat="server" 
                                 AssociatedControlID="dxckEditPalletised" ClientInstanceName="lblPalletised" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Ship pre-palletised">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditPalletised" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckPalletised" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("Palletise") %>' TabIndex="26">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                 AssociatedControlID="dxcboDestPort" ClientInstanceName="lblDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Destination port">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboDestPort" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("DestinationPortID") %>' 
                                 ValueField="PortID" ValueType="System.Int32" Width="210px" TabIndex="9">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblOriginAgentAddress" runat="server" 
                                 ClientInstanceName="lblOriginAgentAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin agent address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="2">
                             <dx:ASPxLabel ID="dxlblBookingReceived" runat="server" 
                                 AssociatedControlID="dxdtBookingReceived" 
                                 ClientInstanceName="lblBookingReceived" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Booking received from printer" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtBookingReceived" runat="server" 
                                 ClientInstanceName="dtBookingReceived" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("BookingReceived") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="27">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblFinal" runat="server" AssociatedControlID="dxcboFinal" 
                                 ClientInstanceName="lblFinal" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Final destination">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboFinal" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboFinal" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PlaceName" Value='<%# Bind("FinalDestinationID") %>' 
                                 ValueField="PlaceID" ValueType="System.Int32" Width="210px" TabIndex="10">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblexWorks" runat="server" 
                                 AssociatedControlID="dxdtExWorks" ClientInstanceName="lblexWorks" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Ex-works date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtExWorks" runat="server" ClientInstanceName="dtExWorks" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("ExWorksDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="11">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblCargoReady" runat="server" 
                                 AssociatedControlID="dxdtCargoReady" ClientInstanceName="lblCargoReady" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Cargo ready date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtCargoReady" runat="server" 
                                 ClientInstanceName="dtCargoReady" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("CargoReady") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="28">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCustomerRef" runat="server" 
                                 ClientInstanceName="lblCustomerRef" 
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
                                 Value='<%# Bind("CustomersRef") %>' Width="170px" TabIndex="12">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                 ClientInstanceName="lblOriginController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin controller">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOriginController" runat="server" 
                                 callbackpagesize="50" ClientInstanceName="cboOriginController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 oncallback="dxcboOriginController_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                 Value='<%# Bind("OriginPortControllerID") %>' ValueField="EmployeeID" 
                                 ValueType="System.Int32" Width="200px" TabIndex="19" >
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDueWarehouse" runat="server" 
                                 ClientInstanceName="lbldueWarehouse" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Due warehouse date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtWarehouse" runat="server" 
                                 ClientInstanceName="dtWarehouse" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("WarehouseDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="29">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblRemarksAgent" runat="server" 
                                 AssociatedControlID="dxmemoRemarksToAgent" ClientInstanceName="lblRemarksAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Remarks to agent">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxMemo ID="dxmemoRemarksToAgent" runat="server" 
                                 ClientInstanceName="memoRemarksToAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="71px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("Remarks") %>' Width="210px" TabIndex="13">
                             </dx:ASPxMemo>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblRemarksToCustomer" runat="server" 
                                 AssociatedControlID="dxmemoRemarksToCustomer" 
                                 ClientInstanceName="lblRemarksToCustomer" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Remarks to customer" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxMemo ID="dxmemoRemarksToCustomer" runat="server" 
                                 ClientInstanceName="memoRemarksToCustomer" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="71px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("RemarksToCustomer") %>' Width="200px" TabIndex="20">
                             </dx:ASPxMemo>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDocsApproved" runat="server" 
                                 AssociatedControlID="dxckEditDocsApproved" ClientInstanceName="lblDocsApproved" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Documents approved">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditDocsAppr" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckDocsAppr" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("DocsRcdAndApproved") %>' TabIndex="30">
                                 <ClientSideEvents ValueChanged="OnDocsValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDocsApprovedDate" runat="server" 
                                 AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblDocsApprovedDate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Approved date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtDocsApproved" runat="server" 
                                 ClientInstanceName="dtDocsApproved" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("DocsApprovedDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="31">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblSellingRate" runat="server" 
                                 AssociatedControlID="dxtxtSellingRate" ClientInstanceName="lblSellingRate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Selling rate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtSellingRate" runat="server" 
                                 ClientInstanceName="txtSellingRate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("Sellingrate") %>' Width="125px" TabIndex="32">
                             </dx:ASPxTextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                         </td>
                         <td>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblSellingAgent" runat="server" 
                                 AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblSellingAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Selling rate for agent">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtSellingAgent" runat="server" 
                                 ClientInstanceName="txtSellingAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("SellingrateAgent") %>' Width="125px" TabIndex="33">
                             </dx:ASPxTextBox>
                         </td>
                     </tr>
            </table> 
          </EditItemTemplate>
          <InsertItemTemplate>
            <table id="insertOrder" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
              <colgroup>
                     <col class="caption10" />
                     <col />
                     <col class="caption12" />
                     <col />
                     <col class="caption12" />
                     <col />
               </colgroup>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblPubliship" runat="server" 
                                 AssociatedControlID="dxckEditPubliship" ClientInstanceName="lblPubliship" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Publiship order" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditJobPubliship" runat="server" 
                                 CheckState="Unchecked" ClientInstanceName="ckJobPubliship" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("PublishipOrder") %>' TabIndex="2">
                                 <ClientSideEvents ValueChanged="onPublishipJobValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckJobClosed" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Text="Job closed" TextAlign="Left" Value='<%# Bind("JobClosed") %>' 
                                 TabIndex="14" ClientVisible="False">
                                 <ClientSideEvents ValueChanged="onJobClosedValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblOfficeIndicator" runat="server" 
                                 ClientInstanceName="lblOfficeIndicator" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True" ClientVisible="False">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDateCreatedTitle" runat="server" 
                                 ClientInstanceName="lblDateCreatedTitle" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Date created" Wrap="True" 
                                 ClientVisible="False">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDateCreated" runat="server" 
                                 ClientInstanceName="lblDateCreated" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True" ClientVisible="False">
                             </dx:ASPxLabel>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblController" runat="server" 
                                 AssociatedControlID="dxcboController" ClientInstanceName="lblController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Order controller" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboController" runat="server" callbackpagesize="50" 
                                 ClientInstanceName="cboController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="Name" Value='<%# Bind("OrderControllerID") %>' 
                                 ValueField="EmployeeID" ValueType="System.Int32" Width="210px" 
                                 TabIndex="3">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblContact" runat="server" 
                                 AssociatedControlID="dxcboClientContact" ClientInstanceName="lblContact" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Client contact" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboClientContact" runat="server" 
                                 ClientInstanceName="cboClientContact" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" oncallback="dxcboContact_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="ContactName" Value='<%# Bind("ContactID") %>' ValueField="ContactID" 
                                 ValueType="System.Int32" Width="200px" CallbackPageSize="50" 
                                 EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
                                 TabIndex="15">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onClientContactButtonClick" SelectedIndexChanged="function(s, e) {
	                                    onContactSelected(s, e);
                                    }" />
                                 <Columns>
                                     <dx:ListBoxColumn Caption="ContactID(Hidden)" FieldName="ContactID" 
                                         Name="colContactID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="ContactName" Name="colContactName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Email Address" FieldName="Email" Name="colEmail" 
                                         Width="175px" />
                                 </Columns>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                        <dx:EditButton ToolTip="Click to add a new contact">
                                            <Image Height="12px" Url="~/Images/icons/metro/plus2.png" Width="12px">
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                            </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblIssueDl" runat="server" 
                                 AssociatedControlID="dxckEditIssueDl" ClientInstanceName="lblIssueDl" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Issue express D/L" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditIssueDl" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckIssueDl" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("ExpressBL") %>' TabIndex="21">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOps" runat="server" AssociatedControlID="dxcboOps" 
                                 ClientInstanceName="lblOps" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableTheming="True" Text="Ops controller" 
                                 Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOps" runat="server" callbackpagesize="50"
                                 ClientInstanceName="cboOps" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                 Value='<%# Bind("OperationsControllerID") %>' ValueField="EmployeeID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="4">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblContactEmail" runat="server" 
                                 ClientInstanceName="lblContactEmail" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblFumigation" runat="server" 
                                 AssociatedControlID="dxckEditFumigation" ClientInstanceName="lblFumigation" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Fumigation certificate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditFumigation" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckFumigation" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("FumigationCert") %>' TabIndex="22">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCompany" runat="server" 
                                 AssociatedControlID="dxcboCompany" ClientInstanceName="lblCompany" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Client name">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboCompany" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboCompany" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcbocompany_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="CompanyName" Value='<%# Bind("CompanyID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="5">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onCompanyButtonClick" SelectedIndexChanged="function(s, e) {
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
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons>
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblPrinter" runat="server" 
                                 AssociatedControlID="dxcboPrinter" ClientInstanceName="lblPrinter" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Printer">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboPrinter" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboPrinter" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboprinter_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboprinter_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PrinterName" Value='<%# Bind("PrinterID") %>' ValueField="CompanyID" 
                                 ValueType="System.Int32" Width="200px" TabIndex="16">
                                 <ClientSideEvents ButtonClick="onPrinterButtonClick" SelectedIndexChanged="function(s, e) {
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
                                     <dx:ListBoxColumn Caption="Name" FieldName="PrinterName" Name="colPrinterName" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="PrinterAdd1" 
                                         Name="colPrinterAdd1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="PrinterAdd2" 
                                         Name="colPrinterAdd2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="PrinterAdd3" 
                                         Name="colPrinterAdd3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="PrinterCountry" 
                                         Name="colPrinterCountry" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="PrinterTel" Name="colPrinterTel" 
                                         Width="100px" />
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
                             <dx:ASPxLabel ID="dxlblGSP" runat="server" AssociatedControlID="dxckEditGSP" 
                                 ClientInstanceName="lblGSP" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="GSP certificate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditGSP" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckGSP" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("GSPCert") %>' TabIndex="23">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxHyperLink ID="dxlinkNewCompany" runat="server" 
                                 ClientInstanceName="linkNewCompany" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                 <ClientSideEvents Click="onNewCompany" />
                             </dx:ASPxHyperLink>
                             <dx:ASPxHyperLink ID="dxlinkEditCompany" runat="server" 
                                 ClientInstanceName="linkEditCompany" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                 <ClientSideEvents Click="onEditCompany" />
                             </dx:ASPxHyperLink>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblCompanyAddress" runat="server" 
                                 ClientInstanceName="lblCompanyAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Contact address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxHyperLink ID="dxlinkNewPrinter" runat="server" 
                                 ClientInstanceName="linkNewPrinter" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/new.png" ImageWidth="23px">
                                 <ClientSideEvents Click="onNewPrinter" />
                             </dx:ASPxHyperLink>
                             <dx:ASPxHyperLink ID="dxlinkEditPrinter" runat="server" 
                                 ClientInstanceName="linkEditPrinter" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" Cursor="pointer" ImageHeight="26px" 
                                 ImageUrl="~/Images/icons/metro/edit.png" ImageWidth="26px" Text="New">
                                 <ClientSideEvents Click="onEditPrinter" />
                             </dx:ASPxHyperLink>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblPrinterAddress" runat="server" 
                                 ClientInstanceName="lblPrinterAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Printer address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblPacking" runat="server" 
                                 AssociatedControlID="dxckEditPacking" ClientInstanceName="lblPacking" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Packing declaration">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditPacking" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckPacking" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("PackingDeclaration") %>' TabIndex="24">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                         </td>
                         <td>
                         </td>
                         <td rowspan="4">
                             <dx:ASPxLabel ID="dxlblDocs" runat="server" AssociatedControlID="dxmemoDocs" 
                                 ClientInstanceName="lblDocs" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Other documents required" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="4">
                             <dx:ASPxMemo ID="dxmemoDocs" runat="server" ClientInstanceName="memoDocs" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="90px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("OtherDocsRequired") %>' Width="200px" TabIndex="25">
                             </dx:ASPxMemo>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                         </td>
                         <td>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCountry" runat="server" 
                                 AssociatedControlID="dxcboCountry" ClientInstanceName="lblCountry" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin country">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboCountry" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboCountry" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOrigin_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="CountryName" Value='<%# Bind("CountryID") %>' ValueField="CountryID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="6">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onComboButtonClick" SelectedIndexChanged="function(s, e) { onCountryChanged(s); }" />
                                 <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                 </Buttons> 
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJob" runat="server" 
                                 AssociatedControlID="dxckEditHotjob" ClientInstanceName="lblhotJob" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Hot Job">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditJobHot" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckJobHot" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("HotJob") %>' TabIndex="17">
                                 <ClientSideEvents ValueChanged="onHotJobValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOrigin" runat="server" AssociatedControlID="dxcboOrigin" 
                                 ClientInstanceName="lblOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin point">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOrigin" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOrigin_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PlaceName" Value='<%# Bind("OriginPointID") %>' ValueField="PlaceID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="7">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <ClientSideEvents ButtonClick="onComboButtonClick" SelectedIndexChanged="function(s, e) { onOriginChanged(s); }" />
                                 <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                 </Buttons> 
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJobNB1" runat="server" 
                                 ClientInstanceName="lblhotJobNB1" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Note:">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblHotJobNB2" runat="server" 
                                 ClientInstanceName="lblhotJobNB2" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="will initiate detailed tracking" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblOriginPort" runat="server" 
                                 AssociatedControlID="dxcboOriginPort" ClientInstanceName="lblOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin port">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOriginPort" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboOriginPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 OnCallback="dxcboOriginPort_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("PortID") %>' ValueField="PortID" 
                                 ValueType="System.Int32" Width="210px" TabIndex="8">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblAgentAtOrigin" runat="server" 
                                 AssociatedControlID="dxcboAgentAtOrigin" ClientInstanceName="lblAgentAtOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin agent">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboAgentAtOrigin" runat="server" callbackpagesize="50" 
                                 ClientInstanceName="cboAgentAtOrigin" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="890px" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" 
                                 onitemrequestedbyvalue="dxcboagentatorigin_ItemRequestedByValue" 
                                 onitemsrequestedbyfiltercondition="dxcboagentatorigin_ItemsRequestedByFilterCondition" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="OriginAgent" Value='<%# Bind("AgentAtOriginID") %>' 
                                 ValueField="OriginAgentID" ValueType="System.Int32" Width="200px" 
                                 TabIndex="18">
                                 <ClientSideEvents ButtonClick="onAgentAtOriginButtonClick" SelectedIndexChanged="function(s, e) {
	                        onOriginAgentSelected(s, e);
                        }" />
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                 <Columns>
                                     <dx:ListBoxColumn Caption="OriginAgentID(Hidden)" FieldName="OriginAgentID" 
                                         Name="colOriginAgentID" Visible="false" />
                                     <dx:ListBoxColumn Caption="Name" FieldName="OriginAgent" Name="colOriginAgent" 
                                         Width="190px" />
                                     <dx:ListBoxColumn Caption="Address 1" FieldName="OriginAgentAddress1" 
                                         Name="colOriginAgentAdd1" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 2" FieldName="OriginAgentAddress2" 
                                         Name="colOriginAgentAdd2" Width="150px" />
                                     <dx:ListBoxColumn Caption="Address 3" FieldName="OriginAgentAddress3" 
                                         Name="colOriginAgentAdd3" Width="150px" />
                                     <dx:ListBoxColumn Caption="Country" FieldName="OriginAgentCountry" 
                                         Name="colOriginAgentCountry" Width="150px" />
                                     <dx:ListBoxColumn Caption="Phone" FieldName="OriginAgentTel" 
                                         Name="colOriginAgentTel" Width="100px" />
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
                             <dx:ASPxLabel ID="dxlblPalletised" runat="server" 
                                 AssociatedControlID="dxckEditPalletised" ClientInstanceName="lblPalletised" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Ship pre-palletised">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditPalletised" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckPalletised" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Width="100px" Value='<%# Bind("Palletise") %>' TabIndex="26">
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblDestPort" runat="server" 
                                 AssociatedControlID="dxcboDestPort" ClientInstanceName="lblDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Destination port">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboDestPort" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboDestPort" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PortName" Value='<%# Bind("DestinationPortID") %>' 
                                 ValueField="PortID" ValueType="System.Int32" Width="210px" TabIndex="9">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblOriginAgentAddress" runat="server" 
                                 ClientInstanceName="lblOriginAgentAddress" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin agent address" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="2">
                             <dx:ASPxLabel ID="dxlblBookingReceived" runat="server" 
                                 AssociatedControlID="dxdtBookingReceived" 
                                 ClientInstanceName="lblBookingReceived" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Booking received from printer" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtBookingReceived" runat="server" 
                                 ClientInstanceName="dtBookingReceived" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("BookingReceived") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="27">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblFinal" runat="server" AssociatedControlID="dxcboFinal" 
                                 ClientInstanceName="lblFinal" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Final destination">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboFinal" runat="server" callbackpagesize="25" 
                                 ClientInstanceName="cboFinal" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" EnableCallbackMode="True" 
                                 IncrementalFilteringMode="StartsWith" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 TextField="PlaceName" Value='<%# Bind("FinalDestinationID") %>' 
                                 ValueField="PlaceID" ValueType="System.Int32" Width="210px" TabIndex="10">
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblexWorks" runat="server" 
                                 AssociatedControlID="dxdtExWorks" ClientInstanceName="lblexWorks" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Ex-works date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtExWorks" runat="server" ClientInstanceName="dtExWorks" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("ExWorksDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="11">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblCargoReady" runat="server" 
                                 AssociatedControlID="dxdtCargoReady" ClientInstanceName="lblCargoReady" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Cargo ready date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtCargoReady" runat="server" 
                                 ClientInstanceName="dtCargoReady" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("CargoReady") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="28">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblCustomerRef" runat="server" 
                                 ClientInstanceName="lblCustomerRef" 
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
                                 Value='<%# Bind("CustomersRef") %>' Width="170px" TabIndex="12">
                             </dx:ASPxTextBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                                 ClientInstanceName="lblOriginController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Origin controller">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxComboBox ID="dxcboOriginController" runat="server" 
                                 callbackpagesize="50" ClientInstanceName="cboOriginController" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" DropDownWidth="200px" EnableCallbackMode="True" 
                                  IncrementalFilteringMode="StartsWith" 
                                 oncallback="dxcboOriginController_Callback" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" TextField="Name" 
                                 Value='<%# Bind("OriginPortControllerID") %>' ValueField="EmployeeID" 
                                 ValueType="System.Int32" Width="200px" TabIndex="19" >
                                 <LoadingPanelStyle ImageSpacing="5px">
                                 </LoadingPanelStyle>
                                 <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                 </LoadingPanelImage>
                                    <Buttons>
                                        <dx:EditButton ToolTip="Click to remove name" Position="Left" >
                                            <Image Height="12px" Url="~/Images/icons/metro/delete2.png" Width="12px" >
                                            </Image>
                                        </dx:EditButton>
                                    </Buttons> 
                                 <ClientSideEvents ButtonClick="onComboButtonClick" />
                             </dx:ASPxComboBox>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDueWarehouse" runat="server" 
                                 ClientInstanceName="lbldueWarehouse" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Due warehouse date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtWarehouse" runat="server" 
                                 ClientInstanceName="dtWarehouse" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("WarehouseDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="29">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             <dx:ASPxLabel ID="dxlblRemarksAgent" runat="server" 
                                 AssociatedControlID="dxmemoRemarksToAgent" ClientInstanceName="lblRemarksAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Remarks to agent">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxMemo ID="dxmemoRemarksToAgent" runat="server" 
                                 ClientInstanceName="memoRemarksToAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="71px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("Remarks") %>' Width="210px" TabIndex="13">
                             </dx:ASPxMemo>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxLabel ID="dxlblRemarksToCustomer" runat="server" 
                                 AssociatedControlID="dxmemoRemarksToCustomer" 
                                 ClientInstanceName="lblRemarksToCustomer" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Remarks to customer" Wrap="True">
                             </dx:ASPxLabel>
                         </td>
                         <td rowspan="3">
                             <dx:ASPxMemo ID="dxmemoRemarksToCustomer" runat="server" 
                                 ClientInstanceName="memoRemarksToCustomer" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Height="71px" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("RemarksToCustomer") %>' Width="200px" TabIndex="20">
                             </dx:ASPxMemo>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDocsApproved" runat="server" 
                                 AssociatedControlID="dxckEditDocsApproved" ClientInstanceName="lblDocsApproved" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Documents approved">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxCheckBox ID="dxckEditDocsAppr" runat="server" CheckState="Unchecked" 
                                 ClientInstanceName="ckDocsAppr" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("DocsRcdAndApproved") %>' TabIndex="30">
                                 <ClientSideEvents ValueChanged="OnDocsValueChanged" />
                             </dx:ASPxCheckBox>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblDocsApprovedDate" runat="server" 
                                 AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblDocsApprovedDate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Approved date">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxDateEdit ID="dxdtDocsApproved" runat="server" 
                                 ClientInstanceName="dtDocsApproved" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Spacing="0" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("DocsApprovedDate") %>' Width="110px" DateOnError="Null" 
                                 TabIndex="31">
                                 <ButtonStyle Width="13px">
                                 </ButtonStyle>
                                 <CalendarProperties><HeaderStyle Spacing="1px" /></CalendarProperties>
                             </dx:ASPxDateEdit>
                         </td>
                     </tr>
                     <tr class="row_divider">
                         <td>
                             &nbsp;</td>
                         <td>
                             <dx:ASPxLabel ID="dxlblSellingRate" runat="server" 
                                 AssociatedControlID="dxtxtSellingRate" ClientInstanceName="lblSellingRate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Selling rate">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtSellingRate" runat="server" 
                                 ClientInstanceName="txtSellingRate" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("Sellingrate") %>' Width="125px" TabIndex="32">
                             </dx:ASPxTextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>
                         </td>
                         <td>
                             &nbsp;</td>
                         <td>
                         </td>
                         <td>
                         </td>
                         <td>
                             <dx:ASPxLabel ID="dxlblSellingAgent" runat="server" 
                                 AssociatedControlID="dxdtDocsApproved" ClientInstanceName="lblSellingAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" Text="Selling rate for agent">
                             </dx:ASPxLabel>
                         </td>
                         <td>
                             <dx:ASPxTextBox ID="dxtxtSellingAgent" runat="server" 
                                 ClientInstanceName="txtSellingAgent" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                 Value='<%# Bind("SellingrateAgent") %>' Width="125px" TabIndex="33">
                             </dx:ASPxTextBox>
                         </td>
                     </tr>
             </table> 
          </InsertItemTemplate> 
         </asp:FormView> 
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
        <div class="clear"></div>
        <div class="grid_16">
            <dx:ASPxHiddenField ID="dxhfOrder" ClientInstanceName="hfOrder" runat="server">
            </dx:ASPxHiddenField>
        </div>
        <div class="grid_16">
            <dx:ASPxHiddenField ID="dxhfOfficeID" ClientInstanceName="hfOfficeID" runat="server">
            </dx:ASPxHiddenField>
        </div> 
        <div class="clear"></div>
        <div class="grid_16">
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
                        Height="720px" Modal="True" Name="CompanyDetails" PopupAction="None" 
                        Width="1000px" PopupElementID="dxbtnmore">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                
                </Windows>
            </dx:ASPxPopupControl>
        </div>  
    </div><!-- end container -->
   
</asp:Content>

