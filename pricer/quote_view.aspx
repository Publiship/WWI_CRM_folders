<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="quote_view.aspx.cs" Inherits="quote_view" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

<script type="text/javascript">

    function loadActiveTab(s, e) {
        var indexTab = (pagepriceview.GetActiveTab()).index;
        
        switch (indexTab) {
            case 0:
                cbkquotev1.PerformCallback();
                break;
            case 1:
                cbkcostingprev1.PerformCallback();
                break;
            case 2:
                cbkcostingloosev1.PerformCallback();
                break
            case 3:
                cbkshipv1.PerformCallback();
                break;
            default:
                break;
        }
    }
    
    function tryClose() {
        try {

            window.top.close();
            alert('window');
            
        } catch (e) {

        this.close();
        alert('mozilla');
        }
    }
</script> 

<div class="formcenter580">
    <dx:ASPxPageControl ID="dxpagepriceview" ClientInstanceName="pagepriceview" 
        runat="server" ActiveTabIndex="0"  Width="580px"
        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentStyle BackColor="White">
            <Paddings Padding="5px" />
        </ContentStyle>
        <TabPages>
            <dx:TabPage Text="Quote details" >
                <ContentCollection>
                    <dx:ContentControl ID="content1" runat="server">
                        <dx:ASPxCallbackPanel runat="server" ID="dxcbkquotev1" 
                            ClientInstanceName="cbkquotev1" OnCallback="dxcbkquotev1_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                        </LoadingPanelImage>
                        <PanelCollection>
                        <dx:PanelContent>     
                            <!-- end header --> 
                            <asp:Repeater ID="rptpricevalue1" runat="server" >
                             <ItemTemplate>
                             <div class="formcenter580">
                             <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblquoteh1" 
                                            runat="server" ClientInstanceName="quoteh1" 
                                        Text='<%# string.Format("[{0}] {1}",Eval("quote_id"), nullValue(Eval("book_title")) ) %>' Font-Bold="True" Font-Size="Medium" /></div>
                                </div>
                            <!-- end header -->
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblqot1" runat="server" ClientInstanceName="lblqot1" Text='<%# inputDimsTitle(Eval("in_dimensions"), 0) %>' /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblqot2" runat="server" ClientInstanceName="lblqot2" Text='<%# inputDimsTitle(Eval("in_dimensions"), 1) %>' /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblsize" runat="server" ClientInstanceName="lblsize" Text='<%# inputDimsValue(Eval("out_length"), Eval("out_width"), Eval("out_depth")) %>' /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblweight" runat="server" 
                                    ClientInstanceName="lblweight" Text='<%# nullValue(Eval("out_weight")) %>' /></div>
                            <!-- end book info -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot3" runat="server" ClientInstanceName="lblqot3" Text="From" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot4" runat="server" ClientInstanceName="lblqot4" Text="To" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblqot5" runat="server" ClientInstanceName="lblqot5" Text="Shipping Via" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblfrom" runat="server" ClientInstanceName="lblfrom" Text='<%# nullValue(Eval("origin_name")) %>' /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblto" runat="server" ClientInstanceName="lblto" Text='<%# nullValue(Eval("final_name")) %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvia" runat="server" ClientInstanceName="lblvia" Text='<%# nullValue(Eval("ship_via")) %>' /></div>
                            <!-- end origin/destination -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblcopies" runat="server" ClientInstanceName="lblcopies" Text='<%# string.Format("{0} copies", nullValue(Eval("tot_copies"))) %>' /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblppc" runat="server" ClientInstanceName="lblppc" Text='<%# string.Format("{0} per copy",Eval("in_currency")) %>' Font-Bold="True" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblqot6" runat="server" ClientInstanceName="lblqot6" Text="Pallet type" /></div>
                            <!-- end copies -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot7" runat="server" ClientInstanceName="lblqot7" Text="Pre-Palletised" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblpricepre" runat="server" 
                                    ClientInstanceName="lblpricepre" Text='<%# Eval("price_pallet", "{0:0.00}") %>' Font-Bold="True" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlbltype" runat="server" ClientInstanceName="lbltype" Text='<%# nullValue(Eval("in_pallet")) %>' /></div>
                            <!-- end pre-palletised -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblshiploose" runat="server" ClientInstanceName="lblshiploose" Text="shipped as loose" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblpriceloose" runat="server" 
                                    ClientInstanceName="lblpriceloose" Text='<%# Eval("price_loose", "{0:0.00}") %>' Font-Bold="True" /></div>
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtnship" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnship" Text="Shipment" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    CausesValidation="False" UseSubmitBehavior="False">
                                    <ClientSideEvents 
                                        Click="function(s, e) { 
                                        pagepriceview.SetActiveTab(pagepriceview.GetTab(3)); }" />
                                </dx:ASPxButton>
                              </div>
                            <!-- end loose -->
                            <div class="cell580_80" style="background-color: #E4EBF1"><dx:ASPxLabel ID="dxlbllclname" runat="server" ClientInstanceName="lbllclname" Text='<%# nullValue(Eval("lcl_name")) %>' /></div>
                            <div class="cell580_20" style="background-color: #E4EBF1"></div>
                            <div class="cell580_20">
                                </div>
                            <div class="cell580_20">40&#39; HC</div>
                            <div class="cell580_20">40&#39;</div>
                            <div class="cell580_20">20&#39;</div>
                            <div class="cell580_20">LCL</div>
                            <!-- end result value headers -->
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtncosting1" runat="server" AutoPostBack="False" 
                                    CausesValidation="False" ClientInstanceName="btncosting1" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Costing" 
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) { 
                                        pagepriceview.SetActiveTab(pagepriceview.GetTab(1)); }" />
                                </dx:ASPxButton>
                              </div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv40hc" runat="server" ClientInstanceName="lblv40hc" Text='<%# Eval("lcl_v40hc", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv40" runat="server" ClientInstanceName="lblv40" Text='<%# Eval("lcl_v40", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv20" runat="server" ClientInstanceName="lblv20" Text='<%# Eval("lcl_v20", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv" runat="server" ClientInstanceName="lblv" Text='<%# Eval("lcl_v", "{0:0.00}") %>' /></div>
                            <!-- end all LCL values -->
                            <div class="cell580_80" style="background-color: #E4EBF1"><dx:ASPxLabel ID="dxlblloosename" ClientInstanceName="lblloosename" runat="server" Text='<%# nullValue(Eval("loose_name")) %>' /></div>
                            <div class="cell580_20" style="background-color: #E4EBF1">
                              </div>
                            <!-- end shipped loose headers -->
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtncosting2" runat="server" AutoPostBack="False" 
                                    CausesValidation="False" ClientInstanceName="btncosting1" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Costing" 
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) { 
                                        pagepriceview.SetActiveTab(pagepriceview.GetTab(2)); }" />
                                </dx:ASPxButton>
                              </div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose40hc" ClientInstanceName="lblvloose40hc" runat="server" Text='<%# Eval("lcl_vloose40hc", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose40" ClientInstanceName="lblvloose40" runat="server" Text='<%# Eval("lcl_vloose40", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose20" ClientInstanceName="lblvloose20" runat="server" Text='<%# Eval("lcl_vloose20", "{0:0.00}") %>' /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose" ClientInstanceName="lblvloose" runat="server" Text='<%# Eval("lcl_vloose", "{0:0.00}") %>' /></div>
                            <!-- end all shipped loose values --> 
                            </div>
                        </ItemTemplate> 
                        </asp:Repeater> 
                        </dx:PanelContent> 
                        </PanelCollection> 
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Costing (pre-palletised)" >
                <ContentCollection>
                    <dx:ContentControl  ID="content2" runat="server">
                        <dx:ASPxCallbackPanel runat="server" ID="dxcbkcostingprev1" 
                            ClientInstanceName="cbkcostingprev1" OnCallback="dxcbkcostingprev1_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                            <PanelCollection>
                            <dx:PanelContent>     
                                <asp:Repeater ID="rptcosting1" runat="server" >
                                <ItemTemplate>
                                <div class="info2">
                                        <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblcostpallet2" runat="server" ClientInstanceName="lblcostpallet2" 
                                                Text='<%# string.Format("{0} copies {1}",Eval("tot_copies") ,nullValue(Eval("lcl_name"))) %>' /></div>
                                  </div>
                                 <div style="padding: 8px; float: left; width:200px;">
                                  
                                 <dl class="dl2">
                                     <dt>Pre-Carriage</dt>
                                     <dd></dd>
                                     <dt>Part Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre1a" runat="server" ClientInstanceName="lblpre1a" 
                                             Text='<%# Eval("pre_part", "{0:0.00}") %>' /></dd>
                                     <dt>Full Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre2c" runat="server" ClientInstanceName="lblpre2c" Text='<%# Eval("pre_full", "{0:0.00}") %>' /></dd>
                                     <dt>20' TCH</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre3c" runat="server" ClientInstanceName="lblpre3c" Text='<%# Eval("pre_thc20", "{0:0.00}") %>' /></dd>
                                     <dt>40' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre4c" runat="server" ClientInstanceName="lblpre4c" Text='<%# Eval("pre_thc40", "{0:0.00}") %>' /></dd>
                                     <dt>LCL TCH</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre5c" runat="server" ClientInstanceName="lblpre5c" Text='<%# Eval("pre_thclcl", "{0:0.00}") %>' /></dd>
                                     <dt>Documentation</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre6c" runat="server" ClientInstanceName="lblpre6c" Text='<%# Eval("pre_docs", "{0:0.00}") %>' /></dd>
                                     <dt>Other Origin</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre7c" runat="server" ClientInstanceName="lblpre7c" Text='<%# Eval("pre_origin", "{0:0.00}") %>' /></dd>
                                     <dt>20' FCL Haulage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre8c" runat="server" ClientInstanceName="lblpre8c" Text='<%# Eval("pre_haul20", "{0:0.00}") %>' /></dd>
                                     <dt>40' FCL Haulage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre9c" runat="server" ClientInstanceName="lblpre9c" Text='<%# Eval("pre_haul40", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div>
                                  <div style="float: left; width:135px; padding-top: 8px;">
                                 <dl class="dl3">
                                     <dt>Freight</dt>
                                     <dd></dd>
                                     <dt>LCL</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre1c" runat="server" ClientInstanceName="dxlblfre1c" Text='<%# Eval("freight_lcl", "{0:0.00}") %>' /></dd>
                                     <dt>20'</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre2c" runat="server" ClientInstanceName="dxlblfre2c" Text='<%# Eval("freight_20", "{0:0.00}") %>' /></dd>
                                     <dt>40'</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre3c" runat="server" ClientInstanceName="dxlblfre3c" Text='<%# Eval("freight_40", "{0:0.00}") %>' /></dd>
                                     <dt>40' HQC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre4c" runat="server" ClientInstanceName="dxlblfre4c" Text='<%# Eval("freight_40hq", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div>
                                 <div style="padding: 8px; float: right; width:200px;">
                                 <dl class="dl2">
                                     <dt>On-Carriage</dt>
                                     <dd></dd>
                                     <dt>Dest LCL THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc1c" runat="server" ClientInstanceName="lblonc1c" Text='<%# Eval("on_dest_lcl", "{0:0.00}") %>' /></dd>
                                     <dt>Pier Loading etc</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc2c" runat="server" ClientInstanceName="lblonc2c" Text='<%# Eval("on_pier_etc", "{0:0.00}") %>' /></dd>
                                     <dt>Dest 20' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc3c" runat="server" ClientInstanceName="lblonc3c" Text='<%# Eval("on_dest_20", "{0:0.00}") %>' /></dd>
                                     <dt>Dest 40' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc4c" runat="server" ClientInstanceName="lblonc4c" Text='<%# Eval("on_dest_40", "{0:0.00}") %>' /></dd>
                                     <dt>Documentation</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc5c" runat="server" ClientInstanceName="lblonc5c" Text='<%# Eval("on_docs", "{0:0.00}") %>' /></dd>
                                     <dt>Customs</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc6c" runat="server" ClientInstanceName="lblonc6c" Text='<%# Eval("on_customs", "{0:0.00}") %>' /></dd>
                                     <dt>Part Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc7c" runat="server" ClientInstanceName="lblonc7c" Text='<%# Eval("on_part", "{0:0.00}") %>' /></dd>
                                     <dt>Full Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc8c" runat="server" ClientInstanceName="lblonc8c" Text='<%# Eval("on_full", "{0:0.00}") %>' /></dd>
                                     <dt>20' FCL Haualage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc9c" runat="server" ClientInstanceName="lblonc9c" Text='<%# Eval("on_haul20", "{0:0.00}") %>' /></dd>
                                     <dt>40' FCL Haualage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc10c" runat="server" ClientInstanceName="lblonc10c" Text='<%# Eval("on_haul40", "{0:0.00}") %>' /></dd>
                                     <dt>20' Shunt and Devan</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc11c" runat="server" ClientInstanceName="lblonc11c" Text='<%# Eval("on_shunt20", "{0:0.00}") %>' /></dd>
                                     <dt>40' Shunt and Devan</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc12c" runat="server" ClientInstanceName="lblonc12c" Text='<%# Eval("on_shunt40", "{0:0.00}") %>' /></dd>
                                     <dt>Pallets</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc13c" runat="server" ClientInstanceName="lblonc13c" Text='<%# Eval("on_pallets", "{0:0.00}") %>' /></dd>
                                     <dt>Other Dest Charges</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc14c" runat="server" ClientInstanceName="lblonc14c" Text='<%# Eval("on_other", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div> 
                        </ItemTemplate>
                        </asp:Repeater>
                      </dx:PanelContent> 
                      </PanelCollection> 
                    </dx:ASPxCallbackPanel> 
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Costing (loose)">
                <ContentCollection>
                    <dx:ContentControl ID="content3" runat="server">
                         <dx:ASPxCallbackPanel runat="server" ID="dxcbkcostingloosev1" 
                            ClientInstanceName="cbkcostingloosev1" OnCallback="dxcbkcostingloosev1_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                        </LoadingPanelImage>
                        <PanelCollection>
                        <dx:PanelContent>
                            <asp:Repeater ID="rptcosting2" runat="server" >
                            <ItemTemplate> 
                            <div class="info2">    
                             <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblcostpallet2" runat="server" ClientInstanceName="lblcostpallet2" 
                                                Text='<%# string.Format("{0} copies {1}",Eval("tot_copies") ,nullValue(Eval("loose_name"))) %>' /></div>
                            </div>
                            </div> 
                            <div style="padding: 8px; float: left; width:200px;">
                                        <dl class="dl2">
                                     <dt>Pre-Carriage</dt>
                                     <dd></dd>
                                     <dt>Part Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre1a" runat="server" ClientInstanceName="lblpre1a" 
                                             Text='<%# Eval("pre_part", "{0:0.00}") %>' /></dd>
                                     <dt>Full Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre2c" runat="server" ClientInstanceName="lblpre2c" Text='<%# Eval("pre_full", "{0:0.00}") %>' /></dd>
                                     <dt>20' TCH</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre3c" runat="server" ClientInstanceName="lblpre3c" Text='<%# Eval("pre_thc20", "{0:0.00}") %>' /></dd>
                                     <dt>40' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre4c" runat="server" ClientInstanceName="lblpre4c" Text='<%# Eval("pre_thc40", "{0:0.00}") %>' /></dd>
                                     <dt>LCL TCH</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre5c" runat="server" ClientInstanceName="lblpre5c" Text='<%# Eval("pre_thclcl", "{0:0.00}") %>' /></dd>
                                     <dt>Documentation</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre6c" runat="server" ClientInstanceName="lblpre6c" Text='<%# Eval("pre_docs", "{0:0.00}") %>' /></dd>
                                     <dt>Other Origin</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre7c" runat="server" ClientInstanceName="lblpre7c" Text='<%# Eval("pre_origin", "{0:0.00}") %>' /></dd>
                                     <dt>20' FCL Haulage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre8c" runat="server" ClientInstanceName="lblpre8c" Text='<%# Eval("pre_haul20", "{0:0.00}") %>' /></dd>
                                     <dt>40' FCL Haulage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblpre9c" runat="server" ClientInstanceName="lblpre9c" Text='<%# Eval("pre_haul40", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div>
                                  <div style="float: left; width:135px; padding-top: 8px;">
                                 <dl class="dl3">
                                     <dt>Freight</dt>
                                     <dd></dd>
                                     <dt>LCL</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre1c" runat="server" ClientInstanceName="dxlblfre1c" Text='<%# Eval("freight_lcl", "{0:0.00}") %>' /></dd>
                                     <dt>20'</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre2c" runat="server" ClientInstanceName="dxlblfre2c" Text='<%# Eval("freight_20", "{0:0.00}") %>' /></dd>
                                     <dt>40'</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre3c" runat="server" ClientInstanceName="dxlblfre3c" Text='<%# Eval("freight_40", "{0:0.00}") %>' /></dd>
                                     <dt>40' HQC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblfre4c" runat="server" ClientInstanceName="dxlblfre4c" Text='<%# Eval("freight_40hq", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div>
                                  <div style="padding: 8px; float: right; width:200px;">
                                 <dl class="dl2">
                                     <dt>On-Carriage</dt>
                                     <dd></dd>
                                     <dt>Dest LCL THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc1c" runat="server" ClientInstanceName="lblonc1c" Text='<%# Eval("on_dest_lcl", "{0:0.00}") %>' /></dd>
                                     <dt>Pier Loading etc</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc2c" runat="server" ClientInstanceName="lblonc2c" Text='<%# Eval("on_pier_etc", "{0:0.00}") %>' /></dd>
                                     <dt>Dest 20' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc3c" runat="server" ClientInstanceName="lblonc3c" Text='<%# Eval("on_dest_20", "{0:0.00}") %>' /></dd>
                                     <dt>Dest 40' THC</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc4c" runat="server" ClientInstanceName="lblonc4c" Text='<%# Eval("on_dest_40", "{0:0.00}") %>' /></dd>
                                     <dt>Documentation</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc5c" runat="server" ClientInstanceName="lblonc5c" Text='<%# Eval("on_docs", "{0:0.00}") %>' /></dd>
                                     <dt>Customs</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc6c" runat="server" ClientInstanceName="lblonc6c" Text='<%# Eval("on_customs", "{0:0.00}") %>' /></dd>
                                     <dt>Part Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc7c" runat="server" ClientInstanceName="lblonc7c" Text='<%# Eval("on_part", "{0:0.00}") %>' /></dd>
                                     <dt>Full Load</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc8c" runat="server" ClientInstanceName="lblonc8c" Text='<%# Eval("on_full", "{0:0.00}") %>' /></dd>
                                     <dt>20' FCL Haualage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc9c" runat="server" ClientInstanceName="lblonc9c" Text='<%# Eval("on_haul20", "{0:0.00}") %>' /></dd>
                                     <dt>40' FCL Haualage</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc10c" runat="server" ClientInstanceName="lblonc10c" Text='<%# Eval("on_haul40", "{0:0.00}") %>' /></dd>
                                     <dt>20' Shunt and Devan</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc11c" runat="server" ClientInstanceName="lblonc11c" Text='<%# Eval("on_shunt20", "{0:0.00}") %>' /></dd>
                                     <dt>40' Shunt and Devan</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc12c" runat="server" ClientInstanceName="lblonc12c" Text='<%# Eval("on_shunt40", "{0:0.00}") %>' /></dd>
                                     <dt>Pallets</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc13c" runat="server" ClientInstanceName="lblonc13c" Text='<%# Eval("on_pallets", "{0:0.00}") %>' /></dd>
                                     <dt>Other Dest Charges</dt>
                                     <dd><dx:ASPxLabel ID="dxlblonc14c" runat="server" ClientInstanceName="lblonc14c" Text='<%# Eval("on_other", "{0:0.00}") %>' /></dd>
                                 </dl>
                                 </div>
                                </ItemTemplate>
                                </asp:Repeater>
                        </dx:PanelContent> 
                        </PanelCollection> 
                        </dx:ASPxCallbackPanel> 
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Shipment size">
                <ContentCollection>
                    <dx:ContentControl ID="content4" runat="server">
                    <dx:ASPxCallbackPanel runat="server" ID="dxcbkshipv1" 
                            ClientInstanceName="cbkshipv1" OnCallback="dxcbkshipv1_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                        </LoadingPanelImage>
                    <PanelCollection>
                    <dx:PanelContent>   
                    <asp:Repeater ID="rptshipment1" runat="server" >
                    <ItemTemplate>   
                    <div class="info2">
                        <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblshiph2" runat="server" ClientInstanceName="shiph1" 
                        Text="Shipment size" /></div>
                            
                    </div>
                    <div style="padding: 8px; float: left; width:250px;">
                        <dl class="dl1">
                              <dt>Copies per carton</dt>
                              <dd><dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblship2b" Text='<%# Eval("calc_copiescarton") %>' /></dd>
                              <dt>Total Cartons</dt>
                              <dd><dx:ASPxLabel ID="dxlblship2b" runat="server" ClientInstanceName="lblship2b" Text='<%# Eval("tot_cartons") %>' /></dd>
                              <dt>Cartons On pallet</dt>
                              <dd><dx:ASPxLabel ID="dxlblship3b" runat="server" ClientInstanceName="lblship3b" Text='<%# Eval("pal_cartons") %>' /></dd>
                         </dl>
                      </div>
                      <div style="padding: 8px; float: right; width:250px;">
                      <dl class="dl1">
                              <dt>Carton Height mm</dt>
                              <dd><dx:ASPxLabel ID="dxlblship12b" runat="server" ClientInstanceName="lblship12b" Text='<%# Eval("ctn_hgt") %>' /></dd>
                              <dt>Carton Length mm</dt>
                              <dd><dx:ASPxLabel ID="dxlblship13b" runat="server" ClientInstanceName="lblship13b" Text='<%# Eval("ctn_len") %>' /></dd>
                              <dt>Carton Width mm</dt>
                              <dd><dx:ASPxLabel ID="dxlblship14b" runat="server" ClientInstanceName="lblship14b" Text='<%# Eval("ctn_wid") %>' /></dd>
                              <dt>Carton Weight kgs</dt>
                              <dd><dx:ASPxLabel ID="dxlblship15b" runat="server" ClientInstanceName="lblship15b" Text='<%# Eval("ctn_wt") %>' /></dd>
                         </dl>
                       </div>
                       <div style="clear: both"></div> 
                       <div style="padding: 8px; float: left; width:250px;">
                         <dl class="dl1">
                              <dt>Full Pallets</dt>
                              <dd><dx:ASPxLabel ID="dxlblship4b" runat="server" ClientInstanceName="lblship4b" Text='<%# Eval("pal_full") %>' /></dd>
                              <dt>Full Pallet Weight</dt>
                              <dd><dx:ASPxLabel ID="dxlblship5b" runat="server" ClientInstanceName="lblship5b" Text='<%# Eval("pal_full_wt") %>' /></dd>
                              <dt>Full Pallet Cube</dt>
                              <dd><dx:ASPxLabel ID="dxlblship6b" runat="server" ClientInstanceName="lblship6b" Text='<%# Eval("pal_full_cu") %>' /></dd>
                              <dt>Max Per Layer</dt>
                              <dd><dx:ASPxLabel ID="dxlblship7b" runat="server" ClientInstanceName="lblship7b" Text='<%# Eval("pal_layers") %>' /></dd>
                              <dt>Number Of Layers</dt>
                              <dd><dx:ASPxLabel ID="dxlblship8b" runat="server" ClientInstanceName="lblship8b" Text='<%# Eval("pal_layer_count") %>' /></dd>
                         </dl>
                        </div>
                         <div style="padding: 8px; float: right; width:250px;">
                       <dl class="dl1">
                              <dt>Part Pallets</dt>
                              <dd><dx:ASPxLabel ID="dxlblship16b" runat="server" ClientInstanceName="lblship16b" Text='<%# Eval("par_count") %>' /></dd>
                              <dt>Remaining Cartons</dt>
                              <dd><dx:ASPxLabel ID="dxlblship17b" runat="server" ClientInstanceName="lblship17b" Text='<%# Eval("ctn_remaining") %>' /></dd>
                              <dt>Residue Pallet Cube</dt>
                              <dd><dx:ASPxLabel ID="dxlblship18b" runat="server" ClientInstanceName="lblship18b" Text='<%# Eval("residue_cu") %>' /></dd>
                              <dt>Residue Pallet Weight</dt>
                              <dd><dx:ASPxLabel ID="dxlblship19b" runat="server" ClientInstanceName="lblship19b" Text='<%# Eval("residue_wt") %>' /></dd>
                         </dl>
                      </div>
                      <div style="clear: both"></div> 
                        <div style="padding: 8px; float: left; width:250px;">
                         <dl class="dl1">  
                              <dt>Total Palletised Weight</dt>
                              <dd><dx:ASPxLabel ID="dxlblship9b" runat="server" ClientInstanceName="lblship9b" Text='<%# Eval("pal_total_wt") %>' /></dd>
                              <dt>Total Palletised Cube</dt>
                              <dd><dx:ASPxLabel ID="dxlblship10b" runat="server" ClientInstanceName="lblship10b" Text='<%# Eval("pal_total_cu") %>' /></dd>
                              <dt>Pallet Weight:Cube Ratio</dt>
                              <dd><dx:ASPxLabel ID="dxlblship11b" runat="server" ClientInstanceName="lblship11b" Text='<%# Eval("pal_ratio") %>' /></dd>
                         </dl>
                      </div>
                  
                     <div style="padding: 8px; float: right; width:250px;">
                       <dl class="dl1">
                              <dt>Total Carton Weight</dt>
                              <dd><dx:ASPxLabel ID="dxlblship20b" runat="server" ClientInstanceName="lblship20b" Text='<%# Eval("ctn_total_wt") %>' /></dd>
                              <dt>Total Carton Cube</dt>
                              <dd><dx:ASPxLabel ID="dxlblship21b" runat="server" ClientInstanceName="lblship21b" Text='<%# Eval("ctn_total_cu") %>' /></dd>
                              <dt>Carton Weight:Cube Ratio</dt>
                              <dd><dx:ASPxLabel ID="dxlblship22b" runat="server" ClientInstanceName="lblship22b" Text='<%# Eval("ctn_ratio") %>' /></dd>
                         </dl>
                      </div>
                      </ItemTemplate> 
                      </asp:Repeater>   
                      </dx:PanelContent> 
                      </PanelCollection> 
                    </dx:ASPxCallbackPanel> 
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ClientSideEvents TabClick="function(s, e) {

}" ActiveTabChanged="function(s, e) {
		    loadActiveTab(s, e);
}" />
        <LoadingPanelStyle ImageSpacing="6px" HorizontalAlign="Center" 
            VerticalAlign="Middle">
        </LoadingPanelStyle>
    </dx:ASPxPageControl>
</div>
 <div style="width: 580px; padding: 4px; margin: 0 Auto;">
        <dx:ASPxButton ID="dxbtnclosebrowser" ClientInstanceName="btnclosebrowser" 
            runat="server" Text="Close Window" 
            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
            CssPostfix="Office2003Blue" 
            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
            AutoPostBack="False" CausesValidation="False" ClientEnabled="False" 
            ClientVisible="False" Visible="False">
            <ClientSideEvents Click="function(s, e) {
	tryClose();
}" />
            

        </dx:ASPxButton>
        <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
            Text="ASPxButton" ClientEnabled="False" ClientVisible="False" 
            Visible="False">
        </dx:ASPxButton>
     <dx:ASPxHiddenField ID="dxhfpriceview" ClientInstanceNam="hfpriceview" runat="server">
     </dx:ASPxHiddenField>
    </div> 
</asp:Content>

