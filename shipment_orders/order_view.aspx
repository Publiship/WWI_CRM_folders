<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_view.aspx.cs" Inherits="order_view" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
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
            //var cid = cboCompany.GetValue();
            //var winEdit = pcPodEdit.GetWindowByName('CompanyDetails');
            //pcPodEdit.SetWindowContentUrl(winEdit, '');
            //pcPodEdit.SetWindowContentUrl(winEdit, 'Popupcontrol/Pod_NameAndAddress.aspx?cid=' + cid);
            //
            //pcPodEdit.ShowWindow(winEdit);
        }

        function onNewCompany(s, e) {

            //var winEdit = popDefault.GetWindowByName('CompanyDetails');
            //pcPodEdit.SetWindowContentUrl(winEdit, '');
            //pcPodEdit.SetWindowContentUrl(winEdit, 'Popupcontrol/Pod_NameAndAddress.aspx?cid=' + "new");
            //
            //pcPodEdit.ShowWindow(winEdit);
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
       
        <!-- Using tables for layout. Semantically not a good choice but other options e.g. definition lists
             do not render properly in older versions of internet explorer < IE7. Also multi-column combos do not
             render correctly when placed in definition lists in < IE7  -->
        <div class="grid_10">
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
                    Text="| Order details">
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <div class="divright">
              <dx:ASPxImage ID="dximgJobPubliship" runat="server" 
                            AlternateText="Publiship Job" ClientInstanceName="imgJobPubliship" 
                            Height="26px" ImageAlign="Top" ImageUrl="~/Images/icons/metro/ticked.png" 
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
                            ImageUrl="~/Images/icons/metro/explosion_red.png" Width="26px" 
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
                            ActiveTabIndex="0" TabSpacing="0px" 
                ondatabound="dxtabOrder_DataBound">
                            <ContentStyle>
                                <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                            </ContentStyle>
                            <Tabs>
                                <dx:Tab NavigateUrl="~/Pod_Edit.aspx" Target="_self" Text="Order details" 
                                    ToolTip="Order details">
                                </dx:Tab>
                                <dx:Tab Target="Titles and copies" ToolTip="Titles and copies">
                                </dx:Tab>
                            </Tabs>
                            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                        </dx:ASPxTabControl>
        </div> 
        <div class="clear"></div>
        <div class="grid_16 pad_bottom">
        <!-- order details -->
        <table id="viewOrder" cellpadding="5px" border="0" width="100%" class="viewTable fixed_layout">
            <col class="caption10" />
            <col/>
            <col class="caption12" />
            <col/>
            <col class="caption12" />
            <col/>
            <tr class="row_divider">
                <td>
                        <dx:ASPxLabel ID="dxlblPubliship" runat="server" 
                            AssociatedControlID="dxckEditPubliship" ClientInstanceName="lblPubliship" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Publiship order" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxCheckBox ID="dxckEditJobPubliship" runat="server" 
                            CheckState="Unchecked" ReadOnly="True"  
                            ClientInstanceName="ckJobPubliship"
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                        </dx:ASPxCheckBox>
                        </td>
                <td>
                        <dx:ASPxCheckBox ID="dxckJobClosed" runat="server" CheckState="Unchecked" 
                            ClientInstanceName="ckJobClosed" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            ReadOnly="true" Text="Job closed" TextAlign="Left">
                        </dx:ASPxCheckBox>
                         </td>
                <td>
                        <dx:ASPxLabel ID="dxlblOfficeIndicator" runat="server" 
                            ClientInstanceName="lblOfficeIndicator" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxLabel ID="dxlblDateCreatedTitle" runat="server" 
                            ClientInstanceName="lblDateCreatedTitle" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Date created" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxLabel ID="dxlblDateCreated" runat="server" 
                            ClientInstanceName="lblDateCreated" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
            </tr>
            <tr class="row_divider">
                <td>
                        <dx:ASPxLabel ID="dxlblController" runat="server" 
                            AssociatedControlID="dxcboController" ClientInstanceName="lblController" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Order controller" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
                <td>
				 <dx:ASPxLabel ID="dxlblViewController" runat="server" 
                            ClientInstanceName="lblViewController" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True" >
                        </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxLabel ID="dxlblContact" runat="server" 
                            AssociatedControlID="dxcboClientContact" ClientInstanceName="lblContact" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Client contact" Wrap="True" >
                        </dx:ASPxLabel>
                         </td>
                <td>
                        <dx:ASPxLabel ID="dxlblViewContact" runat="server" 
                            ClientInstanceName="lblViewContact" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True" >
                        </dx:ASPxLabel>

                         </td>
                <td>
                        <dx:ASPxLabel ID="dxlblIssueDl" runat="server" 
                            AssociatedControlID="dxckEditIssueDl" ClientInstanceName="lblIssueDl" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Issue express D/L" Wrap="True" >
                        </dx:ASPxLabel>
                         </td>
                <td>
                        <dx:ASPxCheckBox ID="dxckEditIssueDl" runat="server" ClientInstanceName="ckIssueDl" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                           Width="100px" CheckState="Unchecked" ReadOnly="True">
                        </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblOps" ClientInstanceName="lblOps" runat="server" 
                        Text="Ops controller" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" EnableTheming="True" 
                        AssociatedControlID="dxcboOps" Wrap="True" >
                    </dx:ASPxLabel>
                        </td>
                <td>
                    <dx:ASPxLabel ID="dxlblViewOps" ClientInstanceName="lblViewOps" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" EnableTheming="True" Wrap="True" >
                    </dx:ASPxLabel>

                        </td>
                <td></td>
                <td>
                     <dx:ASPxLabel ID="dxlblContactEmail" ClientInstanceName="lblContactEmail" 
                            runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
                <td><dx:ASPxLabel ID="dxlblFumigation" runat="server" 
                            AssociatedControlID="dxckEditFumigation" ClientInstanceName="lblFumigation" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Fumigation certificate">
                        </dx:ASPxLabel></td>
                <td>
                     <dx:ASPxCheckBox ID="dxckEditFumigation" ClientInstanceName="ckFumigation" ReadOnly="True"  
                            runat="server" Width="100px"  
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            CheckState="Unchecked">
                    </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblCompany" ClientInstanceName="lblCompany" runat="server" 
                        Text="Client name" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" AssociatedControlID="dxcboCompany">
                    </dx:ASPxLabel>
                        </td>
                <td>
                    <dx:ASPxLabel ID="dxlblViewCompany" ClientInstanceName="lblViewCompany" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>

                        </td>
                <td>
                    <dx:ASPxLabel ID="dxlblPrinter" ClientInstanceName="lblPrinter" runat="server" 
                        Text="Printer" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"  AssociatedControlID="dxcboPrinter"
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
                <td>
				 <dx:ASPxLabel ID="dxlblViewPrinter" ClientInstanceName="lblViewPrinter" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>

                         </td>
                <td><dx:ASPxLabel ID="dxlblGSP" runat="server" 
                            AssociatedControlID="dxckEditGSP" ClientInstanceName="lblGSP" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="GSP certificate">
                        </dx:ASPxLabel></td>
                <td>
                     <dx:ASPxCheckBox ID="dxckEditGSP" ClientInstanceName="ckGSP" runat="server" 
                            Width="100px"
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            CheckState="Unchecked" ReadOnly="True" >
                    </dx:ASPxCheckBox>
                         </td>
            </tr>
           <tr class="row_divider">
                <td>
                        &nbsp;</td>
                <td rowspan="3">
                   
                                    <dx:ASPxLabel ID="dxlblCompanyAddress" ClientInstanceName="lblCompanyAddress" 
                                     runat="server" Text="Contact address"
                                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                                CssPostfix="Office2010Blue" Wrap="True">
                             </dx:ASPxLabel> 
                        </td>
                <td></td>
                <td rowspan="3">
                       <dx:ASPxLabel ID="dxlblPrinterAddress" ClientInstanceName="lblPrinterAddress" 
                            runat="server" Text="Printer address"
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True">
                        </dx:ASPxLabel>  
                         </td>
                <td>
                        <dx:ASPxLabel ID="dxlblPacking" runat="server" 
                            AssociatedControlID="dxckEditPacking" ClientInstanceName="lblPacking" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Packing declaration">
                        </dx:ASPxLabel></td>
                <td>
                    <dx:ASPxCheckBox ID="dxckEditPacking" ClientInstanceName="ckPacking" runat="server" 
                            Width="100px"  
                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" 
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            CheckState="Unchecked" ReadOnly="True" >
                    </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td></td>
                <td></td>
                <td rowspan="4">
                        <dx:ASPxLabel ID="dxlblDocs" 
                            ClientInstanceName="lblDocs" runat="server" AssociatedControlID="dxmemoDocs"
                        Text="Other documents required" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
                <td rowspan="4">
                        <dx:ASPxLabel ID="dxlblViewDocs" 
                            ClientInstanceName="lblViewDocs" runat="server" 
                            AssociatedControlID="dxmemoDocs" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
            </tr>
            <tr class="row_divider">
                <td></td>
                <td></td>
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
                        <dx:ASPxLabel ID="dxlblHotJob" runat="server" AssociatedControlID="dxckEditHotjob" 
                            ClientInstanceName="lblhotJob" 
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
                            ReadOnly="True">
                         </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                                <dx:ASPxLabel ID="dxlblOrigin" ClientInstanceName="lblOrigin" runat="server" 
                                    Text="Origin point" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" AssociatedControlID="dxcboOrigin">
                                </dx:ASPxLabel>
                        </td>
                <td>
					 <dx:ASPxLabel ID="dxlblViewOrigin" ClientInstanceName="lblViewOrigin" runat="server" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxLabel ID="dxlblHotJobNB1" 
                     ClientInstanceName="lblhotJobNB1" runat="server" 
                        Text="Note:" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel></td>
                <td>
                <dx:ASPxLabel ID="dxlblHotJobNB2" 
                        ClientInstanceName="lblhotJobNB2" runat="server" 
                        Text="will initiate detailed tracking" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                                <dx:ASPxLabel ID="dxlblOriginPort" ClientInstanceName="lblOriginPort" runat="server" 
                                    Text="Origin port" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" AssociatedControlID="dxcboOriginPort">
                                </dx:ASPxLabel>
                        </td>
                <td>
     					<dx:ASPxLabel ID="dxlblViewOriginPort" ClientInstanceName="lblViewOriginPort" runat="server" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" Wrap="True">
                                </dx:ASPxLabel>

                        </td>
                <td>
                    <dx:ASPxLabel ID="dxlblAgentAtOrigin" ClientInstanceName="lblAgentAtOrigin" runat="server" 
                        Text="Origin agent" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" AssociatedControlID="dxcboAgentAtOrigin"
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
                <td>
				 <dx:ASPxLabel ID="dxlblViewAgentAtOrigin" 
                        ClientInstanceName="lblViewAgentAtOrigin" runat="server" 
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
                        <dx:ASPxCheckBox ID="dxckEditPalletised" runat="server" 
                            ClientInstanceName="ckPalletised" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Width="100px" CheckState="Unchecked" ReadOnly="True">
                        </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblDestPort" ClientInstanceName="lblDestPort" runat="server" 
                        Text="Destination port" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" AssociatedControlID="dxcboDestPort"
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                        </td>
                <td>
                            <dx:ASPxLabel ID="dxlblViewDestPort" ClientInstanceName="lblViewDestPort" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                        </td>
                <td>&nbsp;</td>
                <td rowspan="3">
                    <dx:ASPxLabel ID="dxlblOriginAgentAddress" ClientInstanceName="lblOriginAgentAddress" 
                        runat="server" Text="Origin agent address"
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>  
                         </td>
                <td rowspan="2">
                    <dx:ASPxLabel ID="dxlblBookingReceived" ClientInstanceName="lblBookingReceived" runat="server" 
                        AssociatedControlID="dxdtBookingReceived"
                        Text="Booking received from printer" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
                <td>
                    <dx:ASPxLabel ID="dxlblViewBookingReceived" 
                            ClientInstanceName="lblViewBookingReceived" runat="server" 
                        AssociatedControlID="dxdtBookingReceived" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblFinal" ClientInstanceName="lblFinal" runat="server" 
                        Text="Final destination" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" AssociatedControlID="dxcboFinal"
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                        </td>
                <td>
				<dx:ASPxLabel ID="dxlblViewFinal" ClientInstanceName="lblViewFinal" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>

                        </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblexWorks" ClientInstanceName="lblexWorks" runat="server" 
                        Text="Ex-works date" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" AssociatedControlID="dxdtExWorks"
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                        </td>
                <td>
                        <dx:ASPxLabel ID="dxlblViewExWorks" runat="server" 
                            ClientInstanceName="lblViewExWorks" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True">
                        </dx:ASPxLabel>
                        </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="dxlblCargoReady" ClientInstanceName="lblCargoReady" 
                        runat="server" AssociatedControlID="dxdtCargoReady"
                        Text="Cargo ready date" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
                <td>
                    <dx:ASPxLabel ID="dxlblViewCargoReady" ClientInstanceName="lblViewCargoReady" 
                        runat="server" AssociatedControlID="dxdtCargoReady" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>
                    <dx:ASPxLabel ID="dxlblCustomerRef" runat="server" Text="Customers ref" 
                        ClientInstanceName="lblCustomerRef" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                    </td>
                <td>  
                    <dx:ASPxLabel ID="dxlblViewCustomerRef" runat="server" 
                        ClientInstanceName="lblViewCustomerRef" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel></td>
                <td>  <dx:ASPxLabel ID="dxlblOriginController" runat="server" 
                        Text="Origin controller" ClientInstanceName="lblOriginController" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel></td>
                <td>  <dx:ASPxLabel ID="dxlblViewOriginController" runat="server" 
                        ClientInstanceName="lblViewOriginController" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel></td>
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
                        CssPostfix="Office2010Blue" Wrap="True">
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
                            AssociatedControlID="dxmemoRemarksToAgent" ClientInstanceName="lblViewRemarksAgent" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Wrap="True">
                        </dx:ASPxLabel>
                        </td>
                <td rowspan="3">
                    <dx:ASPxLabel ID="dxlblRemarksToCustomer" 
                        ClientInstanceName="lblRemarksToCustomer" runat="server"  AssociatedControlID="dxmemoRemarksToCustomer"
                        Text="Remarks to customer" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
                <td rowspan="3">
                    <dx:ASPxLabel ID="dxlblViewRemarksToCustomer" 
                        ClientInstanceName="lblViewRemarksToCustomer" runat="server"  
                            AssociatedControlID="dxmemoRemarksToCustomer" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                         </td>
                <td>  <dx:ASPxLabel ID="dxlblDocsApproved" 
                        ClientInstanceName="lblDocsApproved" runat="server" AssociatedControlID="dxckEditDocsApproved" 
                        Text="Documents approved" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
                <td>
                 <dx:ASPxCheckBox ID="dxckEditDocsAppr" ClientInstanceName="ckDocsAppr" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            CheckState="Unchecked" ReadOnly="True" >
                 </dx:ASPxCheckBox>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>&nbsp;</td>
                <td><dx:ASPxLabel ID="dxlblDocsApprovedDate" 
                        ClientInstanceName="lblDocsApprovedDate" runat="server" AssociatedControlID="dxdtDocsApproved" 
                        Text="Approved date" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
                <td>
                        <dx:ASPxLabel ID="dxlblViewDocsApprovedDate" 
                        ClientInstanceName="lblViewDocsApprovedDate" runat="server" 
                            AssociatedControlID="dxdtDocsApproved" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue">
                    </dx:ASPxLabel>
                         </td>
            </tr>
            <tr class="row_divider">
                <td>&nbsp;</td>
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
                            CssPostfix="Office2010Blue" Wrap="True">
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
                        CssPostfix="Office2010Blue" Wrap="True">
                    </dx:ASPxLabel>
                </td>
            </tr>
        </table> 
        </div> 
        <div class="clear"></div>
        <!-- commnand menu -->
         <!-- individual form menus -->
        <div class="grid_16 pad_bottom">
           <dx:ASPxMenu ID="dxmnuOrder" runat="server" 
                ClientInstanceName="mnuOrder" width="100%" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" AutoSeparators="RootOnly" 
                ItemAutoWidth="False" 
                onitemdatabound="dxmnuOrder_ItemDataBound">
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
    </div><!-- end container -->
    
    <dx1:ASPxHiddenField ID="dxhfOrder" runat="server">
    </dx1:ASPxHiddenField>
    
    <dx:ASPxPopupControl ID="dxpcPodEdit" ClientInstanceName="pcPodEdit" 
        runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" EnableHotTrack="False" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
        PopupAction="None" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter">
        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <HeaderStyle>
        <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <Windows>
             <dx:PopupWindow CloseAction="CloseButton" 
                ContentUrl="~/Popupcontrol/Pod_NameAndAddress.aspx" HeaderText="Company details"
                Height="820px" Modal="True" Name="CompanyDetails" PopupAction="None" 
                Width="1000px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
        
        </Windows>
    </dx:ASPxPopupControl>
</asp:Content>

