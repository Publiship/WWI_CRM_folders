<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="test_page.aspx.cs" Inherits="test_page" %>

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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register TagPrefix="nbc" Namespace="NBarCodes.WebUI" Assembly="NBarCodes" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.1.Web, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
     
    <script type="text/javascript">
        // <![CDATA[
        function onchartShippingInit(s, e) {
            chartShipping.PerformCallback('getdata');
        }
        //this function is called from guageContainers but causes all the totals to be updated
        function onGuageTotalsInit(s, e) {
            if (!guageTotals.InCallback()) {
                guageTotals.PerformCallback();
            }

        }
        
        function onCheckClick(s, e) {
            var code = txtCode.GetText();
            var valid = 17;

            if (code != null) {
                if (code.length == valid) {
                    cbpBarCode.PerformCallback(code);
                }
                else {
                    alert('Please enter 17 digits in the item reference box'); 
                }
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
                    gridTitles.AddNewRow(); break;
                //grid.AddNewRow(); break;    
                case "jsDelete":
                    if (confirm('Are you sure to delete this record?'))
                        grid.DeleteRow(grid.GetTopVisibleIndex());
                    break;
                case "jsRefresh": 
                    break;
                case "jsCancel": grid.Refresh();
                    break;
            }
        }

        //generic function on combo button clicks
        function onComboButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text 
                s.SetText('');
                s.SetSelectedIndex(-1);
            }
        }
        //********************
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
           <table id="tblAlign">
                <tbody>
                    <tr>
                        <td>
                            <table id="tblInput" width="380px" cellpadding="5px">
                                <tr>
                                    <td>
                                <dx:ASPxLabel ID="dxlblRef" ClientInstanceName="lblRef" runat="server" 
                                    Text="Enter 17 digit item reference" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue">
                                </dx:ASPxLabel>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                 <dx:ASPxTextBox ID="dxtxtCode" ClientInstanceName="txtCode" runat="server" 
                                     Width="200px" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue" 
                                     SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                     MaxLength="17">
                                </dx:ASPxTextBox>
                                    </td>
                                    <td>
                            
                                <dx:ASPxButton ID="dxbtnCheck" runat="server" AutoPostBack="False" 
                                    CausesValidation="False" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                    Text="Calculate &gt;" Wrap="False">
                                    <ClientSideEvents Click="onCheckClick" />
                                </dx:ASPxButton>
                            
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                             <dx:ASPxCallbackPanel ID="dxcbpBarCode" ClientInstanceName="cbpBarCode" 
                            runat="server" Width="100%" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" oncallback="dxcbpBarCode_Callback">
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <PanelCollection>
                                <dx:PanelContent>
                                        <table id="tlbResult" width="400px" cellpadding="5px">
                                        <tr>
                                            <td>
                                        <dx:ASPxLabel ID="dxlblDigit" ClientInstanceName="lblDigit" runat="server" 
                                            Text="Check digit" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                            </td>
                                        <td>
                                        <dx:ASPxLabel ID="dxlblBarcode" ClientInstanceName="lblBarcode" runat="server" 
                                            Text="Bar code" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue">
                                        </dx:ASPxLabel>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                           <dx:ASPxTextBox ID="dxtxtResult" ClientInstanceName="txtResult" runat="server" Width="75px" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                        </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <nbc:BarCodeControl ID="barCodeSSCC" runat="server"/>
                                            </td>
                                        </tr>
                                    </table>
                              </dx:PanelContent> 
                            </PanelCollection> 
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxCallbackPanel> 
                        </td>
                    </tr>     
                </tbody>      
           </table>
        </div> 
        <div class="clear"></div>
      
       <!-- data grid make sure the titles combobox is dropdownstyle=dropdown or you won't be able to insert new values -->
        <div class="grid_16">  
            <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
        </div>
      
    </div><!-- end container -->
        
  </asp:Content>


