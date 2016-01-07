<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_sheet.aspx.cs" Inherits="order_sheet" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head_order_out" runat="server">
  <title>Order sheet</title>
  <link rel="stylesheet" href="../App_Themes/700gs12col_fixed.css" type="text/css" />
  <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
  <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
</head>
<body>
  <form id="order_out" runat="server">
    <div class="container_12">
        <!-- messages -->
        <div class="grid_12">
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
        <div class="grid_1">
                  <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/shipment_orders/order_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
        </div>
        <div class="grid_11">
        </div> 
        <div class="clear"></div>
        <asp:FormView ID="fmvorder" runat="server" DefaultMode="ReadOnly"  
            onmodechanging="fmvorder_ModeChanging" 
        ondatabound="fmvorder_DataBound" Height="100%">
        <HeaderTemplate>
            <div class="grid_12"> 
                <table id="tblheader" runat="server" cellpadding="0" width="100%">
                    <tr>
                        <td rowspan="2" width="350px">
                            <dx:ASPxLabel ID="dxlblorder0" runat="server" Font-Names="Arial" 
                                Font-Size="48px" Text='<%# Bind("OrderNumber") %>' 
                                ClientInstanceName="lblorder0"> 
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="3" width="350px">
                            <dx:ASPxLabel ID="dxlblorder1" runat="server" ClientInstanceName="lblorder1" 
                                Font-Names="Arial" Font-Size="24px" Text='<%# Bind("CustomerName") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td width="80px">
                            <dx:ASPxLabel ID="dxlblorder2" runat="server" ClientInstanceName="lblorder2" 
                                Font-Names="Arial" Font-Size="10px" Text="Customers Ref:">
                            </dx:ASPxLabel>
                        </td>
                        <td width="95px">
                            <dx:ASPxLabel ID="dxlblorder3" runat="server" ClientInstanceName="lblorder3" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("CustomersRef") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td width="80px">
                            <dx:ASPxLabel ID="dxlblorder4" runat="server" ClientInstanceName="lblorder4" 
                                Font-Names="Arial" Font-Size="10px" Text="Ex works:">
                            </dx:ASPxLabel>
                        </td>
                        <td width="95px">
                            <dx:ASPxLabel ID="dxlblorderExWorks" runat="server" ClientInstanceName="lblorderExWorks" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ExWorksDate") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </HeaderTemplate> 
        <ItemTemplate> 
             <!-- port and controllers -->
            <div class="grid_12 pad_bottom"> 
             <table id="tblports" runat="server" class="orderSheet">
                <tr>
                    <td width="150px">
                        <dx:ASPxLabel ID="dxlblorder6" runat="server" ClientInstanceName="lblorder6" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin point:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="200px">
                        <dx:ASPxLabel ID="dxlblorder14" runat="server" ClientInstanceName="lblorder14" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginPlace") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td width="150px">
                        <dx:ASPxLabel ID="dxlblorder10" runat="server" ClientInstanceName="lblorder10" 
                            Font-Names="Arial" Font-Size="10px" Text="Customer contact:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder18" runat="server" ClientInstanceName="lblorder18" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("ContactName") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder7" runat="server" ClientInstanceName="lblorder7" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin port:">
                        </dx:ASPxLabel>
                     </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder15" runat="server" ClientInstanceName="lblorder15" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginPort") %>'>
                        </dx:ASPxLabel>
                     </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder11" runat="server" ClientInstanceName="lblorder11" 
                            Font-Names="Arial" Font-Size="10px" Text="Order controller:">
                        </dx:ASPxLabel>
                     </td>
                    <td width="200px">
                        <dx:ASPxLabel ID="dxlblorder19" runat="server" ClientInstanceName="lblorder19" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OrderController") %>'>
                        </dx:ASPxLabel>
                     </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder8" runat="server" ClientInstanceName="lblorder8" 
                            Font-Names="Arial" Font-Size="10px" Text="Destination port:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder16" runat="server" ClientInstanceName="lblorder16" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("DestinationPort") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder12" runat="server" ClientInstanceName="lblorder12" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin controller:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="200px">
                        <dx:ASPxLabel ID="dxlblorder20" runat="server" ClientInstanceName="lblorder20" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginController") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder9" runat="server" ClientInstanceName="lblorder9" 
                            Font-Names="Arial" Font-Size="10px" Text="Final destination:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder17" runat="server" ClientInstanceName="lblorder17" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("FinalDestination") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder13" runat="server" ClientInstanceName="lblorder13" 
                            Font-Names="Arial" Font-Size="10px" Text="Destination controller:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="200px">
                        <dx:ASPxLabel ID="dxlblorder21" runat="server" ClientInstanceName="lblorder21" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("DestController") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table> 
            </div>
            <hr />
            <div class="clear"></div> 
            <div class="grid_12"> 
            <!-- printer and agent at origin addresses -->
            <table id="tblorigins" runat="server" class="viewTable">
                <tr>
                    <td width="350px">
                        <dx:ASPxLabel ID="dxlblorder22" runat="server" ClientInstanceName="lblorder22" 
                            Font-Names="Arial" Font-Size="10px" Text="Printer:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="350px">
                        <dx:ASPxLabel ID="dxlblorder23" runat="server" ClientInstanceName="lblorder23" 
                            Font-Names="Arial" Font-Size="10px" Text="Agent at origin:">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder24" runat="server" ClientInstanceName="lblorder24" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("PrinterName") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder25" runat="server" ClientInstanceName="lblorder25" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginAgentName") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder26" runat="server" ClientInstanceName="lblorder26" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("PrinterAddress1") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder30" runat="server" ClientInstanceName="lblorder30" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginAgentAddress1") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder27" runat="server" ClientInstanceName="lblorder27" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("PrinterAddress2") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder31" runat="server" ClientInstanceName="lblorder31" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginAgentAddress2") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder28" runat="server" ClientInstanceName="lblorder28" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("PrinterAddress3") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder32" runat="server" ClientInstanceName="lblorder32" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginAgentAddress3") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder29" runat="server" ClientInstanceName="lblorder29" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("PrinterCountry") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder33" runat="server" ClientInstanceName="lblorder33" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginAgentCountry") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
            </div>
            <hr />
            <!-- consignee, notify agent, clearing agent address -->  
            <div class="grid_12"> 
            <table id="tblagent" runat="server" class="viewTable">
                <tr>
                    <td width="50px">
                        <dx:ASPxLabel ID="dxlblorder34" runat="server" ClientInstanceName="lblorder34" 
                            Font-Names="Arial" Font-Size="10px" Text="Consignee:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="70px">
                        <dx:ASPxLabel ID="dxlblorder35" runat="server" ClientInstanceName="lblorder35" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ConsigneeName") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td width="40px">
                        <dx:ASPxLabel ID="dxlblorder40" runat="server" ClientInstanceName="lblorder40" 
                            Font-Names="Arial" Font-Size="10px" Text="Notify:">
                        </dx:ASPxLabel>
                    </td>
                    <td width="70px">
                        <dx:ASPxLabel ID="dxlblorder41" runat="server" ClientInstanceName="lblorder41" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("NotifyName") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td width="50px">
                        <dx:ASPxLabel ID="dxlblorder46" runat="server" ClientInstanceName="lblorder46" 
                            Font-Names="Arial" Font-Size="10px" Text="Clearing agent:" Width="50px" 
                            Wrap="False">
                        </dx:ASPxLabel>
                    </td>
                    <td width="70px">
                        <dx:ASPxLabel ID="dxlblorder47" runat="server" ClientInstanceName="lblorder47" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ClearingAgentName") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder36" runat="server" ClientInstanceName="lblorder36" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ConsigneeAddress1") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder42" runat="server" ClientInstanceName="lblorder42" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("NotifyAddress1") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder48" runat="server" ClientInstanceName="lblorder48" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ClearingAgentAddress1") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder37" runat="server" ClientInstanceName="lblorder37" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ConsigneeAddress2") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder43" runat="server" ClientInstanceName="lblorder43" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("NotifyAddress2") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder49" runat="server" ClientInstanceName="lblorder49" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ClearingAgentAddress2") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder38" runat="server" ClientInstanceName="lblorder38" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ConsigneeAddress3") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder44" runat="server" ClientInstanceName="lblorder44" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("NotifyAddress3") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder50" runat="server" ClientInstanceName="lblorder50" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ClearingAgentAddress3") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder39" runat="server" ClientInstanceName="lblorder39" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ConsigneeCountry") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder45" runat="server" ClientInstanceName="lblorder45" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("NotifyCountry") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder51" runat="server" ClientInstanceName="lblorder51" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ClearingAgentCountry") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <dx:ASPxLabel ID="dxlblorderPalletise" runat="server" BackColor="Black" 
                            ClientInstanceName="lblorderPalletise" Font-Bold="False" Font-Names="Arial" 
                            Font-Size="14px" ForeColor="White" Text='<%# Bind("Palletise") %>' 
                            Width="100%">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder53" runat="server" ClientInstanceName="lblorder53" 
                            Font-Names="Arial" Font-Size="10px" Text="Estimated cartons:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder65" runat="server" ClientInstanceName="lblorder65" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstCartons") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder54" runat="server" ClientInstanceName="lblorder54" 
                            Font-Names="Arial" Font-Size="10px" Text="Est 20':">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder69" runat="server" ClientInstanceName="lblorder69" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("Est20") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder55" runat="server" ClientInstanceName="lblorder55" 
                            Font-Names="Arial" Font-Size="10px" Text="Intended vessel:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder73" runat="server" ClientInstanceName="lblorder73" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("VesselName") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder56" runat="server" ClientInstanceName="lblorder56" 
                            Font-Names="Arial" Font-Size="10px" Text="Estimated pallets:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder66" runat="server" ClientInstanceName="lblorder66" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstPallets") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder57" runat="server" ClientInstanceName="lblorder57" 
                            Font-Names="Arial" Font-Size="10px" Text="Est 40':">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder70" runat="server" ClientInstanceName="lblorder70" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("Est40") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder58" runat="server" ClientInstanceName="lblorder58" 
                            Font-Names="Arial" Font-Size="10px" Text="ETS:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorderEts" runat="server" ClientInstanceName="lblorderEts" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("ETS") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder59" runat="server" ClientInstanceName="lblorder59" 
                            Font-Names="Arial" Font-Size="10px" Text="Estimated weight:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder67" runat="server" ClientInstanceName="lblorder67" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstWeight") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder60" runat="server" ClientInstanceName="lblorder60" 
                            Font-Names="Arial" Font-Size="10px" Text="Est LCL Wt:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder71" runat="server" ClientInstanceName="lblorder71" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstLCLWt") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder61" runat="server" ClientInstanceName="lblorder61" 
                            Font-Names="Arial" Font-Size="10px" Text="ETA:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorderEta" runat="server" ClientInstanceName="lblorderEta" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("ETA") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder62" runat="server" ClientInstanceName="lblorder62" 
                            Font-Names="Arial" Font-Size="10px" Text="Estimated volume:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder68" runat="server" ClientInstanceName="lblorder68" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstVolume") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder63" runat="server" ClientInstanceName="lblorder63" 
                            Font-Names="Arial" Font-Size="10px" Text="Est LCL m3:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder72" runat="server" ClientInstanceName="lblorder72" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("EstLCLVol") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder64" runat="server" ClientInstanceName="lblorder64" 
                            Font-Names="Arial" Font-Size="10px" Text="House B/L:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblorder76" runat="server" ClientInstanceName="lblorder76" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("HouseBLNUmber") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
            </div>
            <hr />
            <div class="clear"></div> 
            <div class="grid_12"> 
                <dx:ASPxLabel ID="dxlblorderExBL" runat="server" BackColor="Black" 
                            ClientInstanceName="lblorderExBL" Font-Bold="False" Font-Names="Arial" 
                            Font-Size="14px" ForeColor="White" Text='<%# Bind("ExpressBL") %>' 
                            Width="100%">
                        </dx:ASPxLabel>
                        
            </div> 
            <div class="clear"></div> 
            <div class="grid_8"> 
            <!-- courier details and delivery details in repeaters -->
            <!-- do not put an id or runat tag on the table as it will prevent repeater from rendering -->
            <asp:Repeater ID="rptcouriers" runat="server" 
                onitemdatabound="rptcouriers_ItemDataBound">
                <HeaderTemplate>
                    <table class="viewTable">
                    <tr>
                        <th colspan="3">
                            <dx:ASPxLabel ID="dxlblcrcap0" runat="server" ClientInstanceName="lblcrcap0" 
                            Font-Names="Arial" Font-Size="10px" Text="Courier details">
                            </dx:ASPxLabel>
                        </th>
                    </tr>
                </HeaderTemplate>

                <ItemTemplate>
                 <tr>
                    <td><dx:ASPxLabel ID="dxlblcrname" runat="server" ClientInstanceName="lblcrname" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("CompanyName") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td><dx:ASPxLabel ID="dxlblcrcap1" runat="server" ClientInstanceName="lblcrcap1" 
                            Font-Names="Arial" Font-Size="10px" Text="Original/Copy/Email: ">
                            </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblcroriginal" runat="server" ClientInstanceName="lblcroriginal" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Original") %>'>
                        </dx:ASPxLabel>
                    </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblcraddress1" runat="server" ClientInstanceName="lblcraddress1" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Address1") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblcrcap2" runat="server" ClientInstanceName="lblcrcap2" 
                            Font-Names="Arial" Font-Size="10px" Text="Email to: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="dxlblcrmailto" runat="server" ClientInstanceName="lblcrmailto" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("CourierName") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="lblcraddress2" runat="server" ClientInstanceName="lblcraddress2" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Address2") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblcrcap3" runat="server" ClientInstanceName="lblcrcap3" 
                            Font-Names="Arial" Font-Size="10px" Text="AWB number: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="dxlblcrawbno" runat="server" ClientInstanceName="lblcrawbno" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("AWBNumber") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblcraddress3" runat="server" ClientInstanceName="lblcrcountry" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Address3") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlblcrcap4" runat="server" ClientInstanceName="lblcrcap4" 
                            Font-Names="Arial" Font-Size="10px" Text="Docs despatched: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="dxlblcrdocs" runat="server" ClientInstanceName="lblcrdocs" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("DocumentationDespatched") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlblcrcountry" runat="server" ClientInstanceName="lblcrcountry" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("CountryName") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                             <dx:ASPxLabel ID="dxlblcremail" runat="server" ClientInstanceName="lblcremail" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Email") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            
            <!-- delivery details -->
             <asp:Repeater ID="rptdeliveries" runat="server">
                 <HeaderTemplate>
                    <table class="viewTable">
                    <tr>
                        <th colspan="3">
                            <dx:ASPxLabel ID="dxlbldvcap0" runat="server" ClientInstanceName="lbldvcap0" 
                            Font-Names="Arial" Font-Size="10px" Text="Delivery details">
                            </dx:ASPxLabel>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                      <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlbldccap1" runat="server" ClientInstanceName="lbldvcap1" 
                            Font-Names="Arial" Font-Size="10px" Text="Copies: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="dxlbldvcopies" runat="server" ClientInstanceName="lbldvcopies" 
                             Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Copies") %>'>
                            </dx:ASPxLabel>
                       </td>
              
                    </tr> 
                    <tr>
                        <td>
                             <dx:ASPxLabel ID="dxlbldccap2" runat="server" ClientInstanceName="lbldvcap2" 
                            Font-Names="Arial" Font-Size="10px" Text="Title: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                              <dx:ASPxLabel ID="dxlbldvtitle" runat="server" ClientInstanceName="lbldvtitle" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Title") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr class="row_divider_2">
                        <td>
                        <dx:ASPxLabel ID="lbldvcap3" runat="server" ClientInstanceName="lbldvcap3" 
                            Font-Names="Arial" Font-Size="10px" Text="Delivery to: ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                         <dx:ASPxLabel ID="dxlbldvcompany" runat="server" ClientInstanceName="lbldvcompany" 
                            Font-Names="Arial" Font-Size="10px" Text='<%# Bind("CompanyName") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </ItemTemplate> 
                 <FooterTemplate>
                    </table>
                </FooterTemplate>
             </asp:Repeater>
            </div>
            <!-- end left column -->
            <!-- start right column -->
            <div class="grid_4"> 
            <!-- allocation of charges --->
            <table id="tblcharges"  runat="server" class="viewTable">
                <tr>
                    <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder77" runat="server" ClientInstanceName="lblorder77" 
                            Font-Names="Arial" Font-Size="10px" Text="Clients terms:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder78" runat="server" ClientInstanceName="lblorder78" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("ClientsTerms") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder79" runat="server" ClientInstanceName="lblorder79" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin trucking:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder80" runat="server" ClientInstanceName="lblorder80" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginTrucking") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                     <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder81" runat="server" ClientInstanceName="lblorder81" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin THC:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder82" runat="server" ClientInstanceName="lblorder82" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OrignTHC") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder83" runat="server" ClientInstanceName="lblorder83" 
                            Font-Names="Arial" Font-Size="10px" Text="Origin docs:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder84" runat="server" ClientInstanceName="lblorder84" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("OriginDocs") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                 <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder85" runat="server" ClientInstanceName="lblorder85" 
                            Font-Names="Arial" Font-Size="10px" Text="Freight:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder86" runat="server" ClientInstanceName="lblorder86" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("Freight") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                 <tr>
                 <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder87" runat="server" ClientInstanceName="lblorder87" 
                            Font-Names="Arial" Font-Size="10px" Text="Dest THC:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder88" runat="server" ClientInstanceName="lblorder88" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("DestTHC") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                 <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder89" runat="server" ClientInstanceName="lblorder89" 
                            Font-Names="Arial" Font-Size="10px" Text="Dest Palletisation:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder90" runat="server" ClientInstanceName="lblorder90" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("DestPalletisation") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                 <tr>
                 <td width="80px">
                        <dx:ASPxLabel ID="dxlblorder91" runat="server" ClientInstanceName="lblorder91" 
                            Font-Names="Arial" Font-Size="10px" Text="Customs clearance:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder92" runat="server" ClientInstanceName="lblorder92" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("CustomsClearance") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                 <td width="80px">
                        <dx:ASPxLabel ID="dxdxlblorder93" runat="server" ClientInstanceName="dxlblorder93" 
                            Font-Names="Arial" Font-Size="10px" Text="Delivery charges:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                      <dx:ASPxLabel ID="dxlblorder94" runat="server" ClientInstanceName="dxlblorder94" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("DeliveryCharges") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <dx:ASPxLabel ID="dxlblorder95" runat="server" ClientInstanceName="dxlblorder95" 
                            Font-Names="Arial" Font-Size="12px" Text='<%# Bind("Remarks") %>'>
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
            </div> <!-- end right column -->
        <div class="clear"></div>        
        <hr />
        <!-- div for titles -->
        <div class="grid_12">
            <!-- repeater for titles -->
            <asp:Repeater ID="rptordertitles" runat="server">
                 <HeaderTemplate>
                    <table class="orderSheet">
                    <tr>
                        <th colspan="4">
                            <dx:ASPxLabel ID="dxlbltlcap0" runat="server" ClientInstanceName="lbltlcap0" 
                            Font-Names="Arial" Font-Size="10px" Text="Titles">
                            </dx:ASPxLabel>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="dxlbltlcap1" runat="server" ClientInstanceName="lbltlcap1" 
                            Font-Names="Arial" Font-Size="10px" Text="Title">
                            </dx:ASPxLabel>
                        </td>
                         <td>
                            <dx:ASPxLabel ID="dxlbltlcap2" runat="server" ClientInstanceName="lbltlcap2" 
                            Font-Names="Arial" Font-Size="10px" Text="ISBN">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlbltlcap3" runat="server" ClientInstanceName="lbltlcap3" 
                            Font-Names="Arial" Font-Size="10px" Text="Copies">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlbltlcap4" runat="server" ClientInstanceName="lbltlcap4" 
                            Font-Names="Arial" Font-Size="10px" Text="PO Number">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                             <dx:ASPxLabel ID="dxlbltltitle" runat="server" ClientInstanceName="lbltltitle" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Title") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="dxlbltlisbn" runat="server" ClientInstanceName="lbltlisbn" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("ISBN") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                           <dx:ASPxLabel ID="dxlbltlcopies" runat="server" ClientInstanceName="lbltlcopies" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("Copies") %>'>
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="dxlbltlponumber" runat="server" ClientInstanceName="lbltlponumber" 
                                Font-Names="Arial" Font-Size="10px" Text='<%# Bind("PONumber") %>'>
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </ItemTemplate> 
                 <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>   
        </div>
        <hr />
        <!--end titles div -->
        </ItemTemplate>
        
        <FooterTemplate>
            <div class="grid_12"> 
            <table id="tblfooter1" width="100%">
                <tr>
                    <td>
                     <dx:ASPxLabel ID="dxlblfrcap0" runat="server" ClientInstanceName="lblfrcap0" 
                                Font-Names="Arial" Font-Size="10px" Text="Copy docs received">
                            </dx:ASPxLabel>
                    </td>
                    <td>
                    </td>
                    <td>
                         <dx:ASPxLabel ID="dxlblfrcap1" runat="server" ClientInstanceName="lblfrcap1" 
                                Font-Names="Arial" Font-Size="10px" Text="Invoice issued">
                            </dx:ASPxLabel>
                    </td>
                    <td>
                    </td>
                   <td>
                        <dx:ASPxLabel ID="dxlblfrcap2" runat="server" ClientInstanceName="lblfrcap2" 
                                Font-Names="Arial" Font-Size="10px" Text="Delivery Inst. Issued">
                            </dx:ASPxLabel>
                   </td>
                </tr> 
                <tr>
                    <td>
                          <asp:Panel ID="pnl0" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                              BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel>
                    </td>
                    <td>
                    </td>
                    <td>
                            <asp:Panel ID="pnl1" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                                BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel>
                    </td>
                    <td>
                    </td>
                    <td>
                          <asp:Panel ID="pnl2" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                              BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                     <td>
                       <dx:ASPxLabel ID="dxlblfrcap3" runat="server" ClientInstanceName="lblfrcap3" 
                                Font-Names="Arial" Font-Size="10px" Text="Copy docs to client">
                            </dx:ASPxLabel>
                    </td>
                    <td>
                    </td>
                    <td>
                         <dx:ASPxLabel ID="dxlblfrcap4" runat="server" ClientInstanceName="lblfrcap4" 
                                Font-Names="Arial" Font-Size="10px" Text="Invoice number">
                            </dx:ASPxLabel>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="dxlblfrcap5" runat="server" ClientInstanceName="lblfrcap5" 
                                Font-Names="Arial" Font-Size="10px" Text="Saved to INVU">
                            </dx:ASPxLabel>
                    </td>
                </tr>
                
                <tr>
                    <td>
                          <asp:Panel ID="pnl3" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                              BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel></td>
                    <td>
                        </td>
                    <td>
                          <asp:Panel ID="pnl4" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                              BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel></td>
                    <td>
                        </td>
                    <td>
                          <asp:Panel ID="pnl5" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                              BorderColor="#000000" Width="125px" Height="23px">
                        </asp:Panel>
                    </td>
                </tr>
             </table>
             <table id="tblfotter2" width="100%">
                <tr>
                    <td rowspan="2">
                        <dx:ASPxLabel ID="dxlblorder100" runat="server" Font-Names="Arial" 
                            Font-Size="48px" Text="Order Sheet" 
                            ClientInstanceName="lblorder0">
                        </dx:ASPxLabel></td>
                    <td rowspan="2">&nbsp;</td>
                    <td rowspan="2">
                        <dx:ASPxLabel ID="dxlblorderHot" runat="server" BackColor="Black" 
                            ClientInstanceName="lblorderHot" Font-Names="Arial" Font-Size="36px" 
                            ForeColor="White" Text='<%# Bind("HotJob") %>'>
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                     <td>
                         <dx:ASPxLabel ID="dxlblCurrentDate" runat="server" ClientInstanceName="lblCurrentDate" 
                             Font-Names="Arial" Font-Size="10px" Text="=current date">
                         </dx:ASPxLabel>
                     </td>
                 </tr>
             </table> 
            </div> 
        </FooterTemplate> 
        <EmptyDataTemplate>
            <div class="grid_12">
            <dx:ASPxLabel ID="dxlblempty0" runat="server" Font-Names="Arial" 
                            Font-Size="48px" Text="No data has been found" 
                            ClientInstanceName="lblempty0">
                        </dx:ASPxLabel>
            </div>
        </EmptyDataTemplate> 
        </asp:FormView>
    </div>
       
    <!-- hidden field -->
        <div>
            <dx:ASPxHiddenField ID="dxhforder" ClientInstanceName="hforder" runat="server">
            </dx:ASPxHiddenField>
        </div>
  </form>
</body>
</html>