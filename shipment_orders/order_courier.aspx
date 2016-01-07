<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_courier.aspx.cs" Inherits="order_courier" %>

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
        function onRowEditButtonClick(s, e) {
              grdCourier.StartEditRow(e.visibleIndex);
        }
        
        function onCompanySelected(s, e) {

            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Address1');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address2');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('Address3');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('CountryName');
            s1 = s1 + "<br/>" + s.GetSelectedItem().GetColumnText('TelNo');

            lblAddress2.SetText(s1);
            //requery client contact
            requeryContact(s.GetValue().toString()); 
        }

        function requeryContact(companyId) {
            cboClientContact.PerformCallback(companyId);
            cboClientContact.SetSelectedIndex(-1);
        }

        function onContactSelected(s, e) {
            //don't use \r\n for line break in label doesn't work
            var s1 = s.GetSelectedItem().GetColumnText('Email');
            txtEmail.SetText(s1);
        }

        function onDocsDespatchButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text and address
                s.SetText('');
                s.SetSelectedIndex(-1);
                lblAddress2.SetText('');
            }
        }
        function onOriginalValueChanged(s, e) {
            //_original <= 2 ? "Despatch Date" : "Date Emailed";
            var original = parseInt(s.GetValue(), "10"); //parse using base 10
            var caption = "Date Emailed";
            if (original <= 2) {
                caption = "Despatch Date";
            }

            lblDespatchDate.SetText(caption);
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

        function OnMenuItemClick(s, e) {
             switch (e.item.name) {
                case "jsUpdate": grid.UpdateEdit(); break;
                case "jsEdit":
                    StartEdit();
                    break;
                case "jsNew":
                    grdCourier.AddNewRow(); ; break;
                //grid.AddNewRow(); break;   
                case "jsDelete":
                    if (confirm('Are you sure to delete this record?'))
                        grid.DeleteRow(grid.GetTopVisibleIndex());
                    break;
                case "jsRefresh":
                case "jsCancel": grid.Refresh(); break;
            }
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
                    Text="| Courier details">
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
                ImageAlign="Top">
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
         <dx:ASPxTabControl ID="dxtabOrder" ClientInstanceName="tabOrder" runat="server" 
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
        <!-- data form 2 columns -->
        <div class="grid_16"> 
            <dx:ASPxGridView ID="dxgrdCourier" ClientInstanceName="grdCourier" 
                runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" Width="100%" 
                onrowinserting="dxgrdCourier_RowInserting" 
                onrowupdating="dxgrdCourier_RowUpdating" 
                onhtmlrowcreated="dxgrdCourier_HtmlRowCreated">
                <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="5px">
                    </LoadingPanel>
                </Styles>
                <SettingsPager Position="TopAndBottom">
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
                <SettingsEditing Mode="EditForm" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image">
                        <EditButton Visible="True">
                        </EditButton>
                        <NewButton Visible="True">
                            <Image AlternateText="New" Height="18px" ToolTip="New" 
                                Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                            </Image>
                        </NewButton>
                        <CancelButton Visible="True">
                            <Image AlternateText="Cancel" Height="18px" ToolTip="Cancel" 
                                Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                            </Image>
                        </CancelButton>
                        <UpdateButton Visible="True">
                            <Image AlternateText="Update" Height="18px" ToolTip="Update" 
                                Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                            </Image>
                        </UpdateButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="CourierDetailID" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DocsDespatchID" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Original" VisibleIndex="10">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DocumentationDespatched" VisibleIndex="7">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="DocsLastUpdated" VisibleIndex="8">
                        <PropertiesDateEdit Spacing="0">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="CourierName" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AWBNumber" VisibleIndex="9">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactID" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                        <DataRow>
                            <div>
                            <table id="tblView" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                            <colgroup>
                               <col class="caption12" />
                               <col />
                               <col class="caption12" />
                               <col />
                            </colgroup>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblIdCaption" runat="server" 
                                           ClientInstanceName="lblIdCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Courier ID">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblCourierDetailIDView" runat="server" 
                                           ClientInstanceName="lblCourierDetailIDView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Font-Bold="True" 
                                           Text='<%# Bind("CourierDetailID", "{0}") %>'>
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblCompanyNameCaption" runat="server" 
                                           ClientInstanceName="lblCompanyNameCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Company name">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblDocsDespatchIDView" runat="server" 
                                           ClientInstanceName="lblCompanyNameView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblAddess1Caption" runat="server" 
                                           ClientInstanceName="lblAddress1Caption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Address">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td rowspan="3">
                                       <dx:ASPxLabel ID="dxlblDocsDespatchIDView2" runat="server" 
                                           ClientInstanceName="lblDocsDespatchIDView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblOriginalCaption" runat="server" 
                                           ClientInstanceName="lblOriginalCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Despatched">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td rowspan="3">
                                       <dx:ASPxRadioButtonList ID="dxrblOriginal" runat="server" ClientInstanceName="dxrblOriginal" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" 
                                           SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                           Width="175px" Value='<%# Bind("Original") %>' 
                                           DataSourceID="xmldsOriginals" TextField="name" ValueField="value" 
                                           ReadOnly="True" ValueType="System.Int32">
                                       </dx:ASPxRadioButtonList>
                                   </td>
                               </tr>
                               <tr class="row_divider">
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
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactCaption" runat="server" 
                                           ClientInstanceName="lblContactCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Contact name">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactIDView" runat="server" 
                                           ClientInstanceName="lblContactView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblCourierCaption" runat="server" 
                                           ClientInstanceName="lblCourierCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Courier name">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblCourierView" runat="server" 
                                           ClientInstanceName="lblCourierView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text='<%# Bind("CourierName") %>'>
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblEmailCaption" runat="server" 
                                           ClientInstanceName="lblEmailCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Email address">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblContactIDView2" runat="server" 
                                           ClientInstanceName="lblContactIDView2" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblDespatchDateCaption" runat="server" 
                                           ClientInstanceName="lblDespatchDateCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="Despatch date">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblDespatchDateView" runat="server" 
                                           ClientInstanceName="lblDespatchDateView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text='<%# Bind("DocumentationDespatched") %>'>
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                   </td>
                               </tr>
                               <tr class="row_divider">
                                   <td>
                                       <dx:ASPxLabel ID="dxlblAWBnoCaption" runat="server" 
                                           ClientInstanceName="lblAWBnoCaption" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text="AWB number">
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                       <dx:ASPxLabel ID="dxlblAWBnoView" runat="server" 
                                           ClientInstanceName="lblAWBnoView" 
                                           CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                           CssPostfix="Office2010Blue" Text='<%# Bind("AWBNumber") %>'>
                                       </dx:ASPxLabel>
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                       <asp:HiddenField ID="hfDocsDespatchID" runat="server" 
                                           Value='<%# Eval("DocsDespatchID") %>' />
                                   </td>
                               </tr>
                               <tr>
                                   <t>
                                       <td>
                                           &nbsp;</td>
                                       <td>
                                           &nbsp;</td>
                                       <td>
                                       </td>
                                       <td>
                                           <asp:HiddenField ID="hfContactID" runat="server" 
                                               Value='<%# Eval("ContactID") %>' />
                                       </td>
                                   </t>
                           </tr>
                        </table> 
                        </div>
                            <dx:ASPxButton ID="dxbtnEditRow" ClientInstanceName="dbtEditRow" runat="server" 
                                AutoPostBack="False" CausesValidation="False"  
                                ClientSideEvents-Click='<%# "function(s, e) { grdCourier.StartEditRow(" + Container.VisibleIndex + "); }" %>' 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" 
                                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                BackColor="White" Cursor="pointer" HorizontalAlign="Left">
                                <HoverStyle Font-Underline="True">
                                </HoverStyle>
                                <Image AlternateText="Edit" ToolTip="Edit" Height="18px" Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                </Image>
                                <Border BorderStyle="None" />
                            </dx:ASPxButton>                  
                        </div> 
                    </DataRow> 
                    <EditForm>
                        <div>
                             <table id="tblEdit" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
                              <colgroup>
                                  <col class="caption12" />
                                  <col />
                                  <col class="caption12" />
                                  <col />
                              </colgroup>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblCompanyName" runat="server" 
                                              ClientInstanceName="lblCompanyName" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Company name">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxComboBox ID="dxcboDocsDespatchID" runat="server" CallbackPageSize="20" 
                                              ClientInstanceName="cboDocsDespatchID" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" DropDownWidth="890px" DropDownRows="10" EnableCallbackMode="True" 
                                              IncrementalFilteringMode="StartsWith" 
                                              OnItemRequestedByValue="dxcbocompany_ItemRequestedByValue" 
                                              OnItemsRequestedByFilterCondition="dxcbocompany_ItemsRequestedByFilterCondition" 
                                              Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              TextField="CompanyName" ValueField="CompanyID" ValueType="System.Int32" 
                                              Width="245px"  
                                              Value='<%# Bind("DocsDespatchID") %>'>
                                              <ClientSideEvents ButtonClick="onDocsDespatchButtonClick" SelectedIndexChanged="function(s, e) {
	                                                    onCompanySelected(s, e);
                                                    }" />
                                              <Columns>
                                                  <dx:ListBoxColumn Caption="CompanyID(Hidden)" FieldName="CompanyID" 
                                                      Name="colCompanyID" Visible="False" />
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
                                              <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                              </LoadingPanelImage>
                                              <LoadingPanelStyle ImageSpacing="5px">
                                              </LoadingPanelStyle>
                                          </dx:ASPxComboBox>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblAddress1" runat="server" 
                                              ClientInstanceName="lblAddress1" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Address">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td rowspan="3">
                                          <dx:ASPxLabel ID="dxlblAddress2" runat="server" 
                                              ClientInstanceName="lblAddress2" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="dxlblOriginal" runat="server" 
                                              ClientInstanceName="lblOriginal" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Despatched">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td rowspan="3">
                                          <dx:ASPxRadioButtonList ID="dxrblOriginal" runat="server" 
                                              ClientInstanceName="rblOriginal" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Width="175px" Value='<%# Bind("Original") %>' 
                                              DataSourceID="xmldsOriginals" TextField="name" ValueField="value" 
                                              ValueType="System.Int32">
                                              <ClientSideEvents ValueChanged="onOriginalValueChanged" />
                                          </dx:ASPxRadioButtonList>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
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
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblContact" runat="server" ClientInstanceName="lblContact" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Contact name">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxComboBox ID="dxcboClientContact" runat="server" 
                                              ClientInstanceName="cboClientContact" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" OnCallback="dxcboContact_Callback" Spacing="0" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              TextField="ContactName" ValueField="ContactID" ValueType="System.Int32" 
                                              Width="245px" Value='<%# Bind("ContactID") %>'>
                                              <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                onContactSelected(s, e);
                                                }" />
                                              <ClientSideEvents EndCallback="function(s, e) {
	                                               s.SetSelectedIndex(-1);
	                                               txtEmail.SetText('');
	                                               //or use this to set 1st item email
	                                               //var s1 = s.GetSelectedItem().GetColumnText('Email');
                                                   //txtEmail.SetText(s1);
                                                }" /> 
                                              <Columns>
                                                  <dx:ListBoxColumn Caption="ContactID(Hidden)" FieldName="ContactID" 
                                                      Name="colContactID" Visible="False" />
                                                  <dx:ListBoxColumn Caption="Name" FieldName="ContactName" Name="colContactName" 
                                                      Width="190px" />
                                                  <dx:ListBoxColumn Caption="Email Address" FieldName="Email" Name="colEmail" 
                                                      Width="175px" />
                                              </Columns>
                                              <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                              </LoadingPanelImage>
                                              <LoadingPanelStyle ImageSpacing="5px">
                                              </LoadingPanelStyle>
                                          </dx:ASPxComboBox>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblCourier" runat="server" ClientInstanceName="lblCourier" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Courier name">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtCourierName" runat="server" 
                                              ClientInstanceName="txtCourierName" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Width="245px" Text='<%# Bind("CourierName") %>'>
                                          </dx:ASPxTextBox>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblEmail" runat="server" ClientInstanceName="lblEmail" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Email address">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtEmail" runat="server" ClientInstanceName="txtEmail" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Width="245px" Text='<%# Bind("EmailAddress") %>'>
                                          </dx:ASPxTextBox>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblDespatchDate" runat="server" 
                                              ClientInstanceName="lblDespatchDate" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="Despatch date">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxDateEdit ID="dxdteDespatchDate" runat="server" 
                                              ClientInstanceName="dteDespatchDate" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Spacing="0" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Width="125px" Value='<%# Bind("DocumentationDespatched") %>'>
                                              <CalendarProperties>
                                                  <HeaderStyle Spacing="1px" />
                                              </CalendarProperties>
                                          </dx:ASPxDateEdit>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                                  <tr class="row_divider">
                                      <td>
                                          <dx:ASPxLabel ID="dxlblAWBno" runat="server" ClientInstanceName="lblAWBno" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" Text="AWB number">
                                          </dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="dxtxtAWBno" runat="server" ClientInstanceName="txtAWBno" 
                                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                              CssPostfix="Office2010Blue" 
                                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                              Text='<%# Bind("AWBNumber") %>' Width="245px">
                                          </dx:ASPxTextBox>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                  </tr>
                        </table> 
                        </div>
                        <div style="text-align: left; padding: 5px 2px 2px 2px">
                                <dx:ASPxGridViewTemplateReplacement ID="dxSaveButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                <dx:ASPxGridViewTemplateReplacement ID="dxCancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                          </div>   
                    </EditForm> 
                </Templates> 
                <StylesPager>
                    <PageNumber ForeColor="#3E4846">
                    </PageNumber>
                    <Summary ForeColor="#1E395B">
                    </Summary>
                </StylesPager>
                <Settings ShowColumnHeaders="False" />
                <StylesEditors ButtonEditCellSpacing="0">
                    <ProgressBar Height="21px">
                    </ProgressBar>
                </StylesEditors>
                <SettingsDetail ShowDetailButtons="False" />
            </dx:ASPxGridView>
        </div> 
        <div class="clear"></div>
        <!-- command menu -->
        <div class="grid_16">
             <dx:ASPxMenu ID="dxmnuCommand" runat="server" 
                ClientInstanceName="mnuCommand" width="100%" EnableClientSideAPI="True"  ItemAutoWidth="False" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                 AutoSeparators="RootOnly" onitemdatabound="dxmnuCommand_ItemDataBound">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                            <ClientSideEvents ItemClick="OnMenuItemClick" />
                        </dx:ASPxMenu>
        </div>   
        <div class="clear"></div>
        
        <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
         <asp:XmlDataSource ID="xmldsOriginals" runat="server" 
             DataFile="~/xml/radio_items.xml" 
             XPath="radioitems/radioitem[@radio='courier_original']"></asp:XmlDataSource>
    </div><!-- end container -->
        
    
  
</asp:Content>



