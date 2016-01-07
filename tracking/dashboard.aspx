<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="tracking_dashboard" %>
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

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxLoadingPanel" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

     <script type="text/javascript">
         // <![CDATA[
         function onCallBackInit(s, e) {
             if (!callInitialise.InCallback()) {
                 callInitialise.PerformCallback('getdata');
             }
         }
         //********************
         // ]]>
    </script>    
    
    <!-- container div centered 960px? -->
    <div style="width: 960px; margin: 0px Auto">
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
            <!-- 4 columns for digital gauges -->
            <div class="grid_4 pad_bottom">
            <div>
                <dx:ASPxLabel ID="dxlblContainers" ClientInstanceName="lblContainers" runat="server" Text="Containers ETS today" 
                    Font-Size="Large" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div>
            <dx:ASPxGaugeControl ID="dxGaugeContainers" 
                ClientInstanceName="gaugeContainers" runat="server" Height="110px" 
                Width="200px" BackColor="White" Value="00,000">
<LayoutPadding All="0" Left="0" Top="0" Right="0" Bottom="0"></LayoutPadding>
                <Gauges>
                    <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#00FFFFFF&quot;/&gt;" 
                        AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:DimGray&quot;/&gt;" 
                        Bounds="0, 0, 200, 110" DigitCount="5" Name="Gauge0" Padding="20, 20, 20, 20" 
                        Text="00,000" 
                        AppearanceOn-BorderBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                        AppearanceOn-BorderWidth="2">
                        <backgroundlayers>
                            <dx:DigitalBackgroundLayerComponent BottomRight="259.8125, 99.9625" Name="digitalBackgroundLayerComponent11" 
                                ShapeType="Style9" TopLeft="20, 0" ZOrder="1000" />
                        </backgroundlayers>
                    </dx:DigitalGauge>
                </Gauges>
            </dx:ASPxGaugeControl>
            </div>
        </div>
        <div class="grid_4 pad_bottom">
            <div>
                <dx:ASPxLabel ID="dxlblPallets" ClientInstanceName="lblPallets" runat="server" Text="Pallets ETS today" 
                    Font-Size="Large" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div>   
            <dx:ASPxGaugeControl ID="dxGaugePallets" 
                    ClientInstanceName="gaugePallets" runat="server" Height="110px" 
                    Width="200px" BackColor="White" Value="00,000">
<LayoutPadding All="0" Left="0" Top="0" Right="0" Bottom="0"></LayoutPadding>
                    <Gauges>
                        <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#00FFFFFF&quot;/&gt;" 
                            AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:DimGray&quot;/&gt;" 
                            Bounds="0, 0, 200, 110" DigitCount="5" Name="Gauge0" Padding="20, 20, 20, 20" 
                            Text="00,000" 
                            AppearanceOn-BorderBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                            AppearanceOn-BorderWidth="2">
                            <backgroundlayers>
                                <dx:DigitalBackgroundLayerComponent BottomRight="259.8125, 99.9625" Name="digitalBackgroundLayerComponent11" 
                                    ShapeType="Style9" TopLeft="20, 0" ZOrder="1000" />
                            </backgroundlayers>
                        </dx:DigitalGauge>
                    </Gauges>
                </dx:ASPxGaugeControl>
            </div> 
        </div>
        <div class="grid_4 pad_bottom">
             <div>
                <dx:ASPxLabel ID="dxlblWeight" ClientInstanceName="lblWeight" runat="server" Text="Weight ETS today" 
                    Font-Size="Large" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div>   
            <dx:ASPxGaugeControl ID="dxgaugeWeight" 
                ClientInstanceName="gaugeWeight" runat="server" Height="110px" 
                Width="200px" BackColor="White" Value="00,000">
<LayoutPadding All="0" Left="0" Top="0" Right="0" Bottom="0"></LayoutPadding>
                <Gauges>
                    <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#00FFFFFF&quot;/&gt;" 
                        AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:DimGray&quot;/&gt;" 
                        Bounds="0, 0, 200, 110" DigitCount="5" Name="Gauge0" Padding="20, 20, 20, 20" 
                        Text="00,000" 
                        AppearanceOn-BorderBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                        AppearanceOn-BorderWidth="2">
                        <backgroundlayers>
                            <dx:DigitalBackgroundLayerComponent BottomRight="259.8125, 99.9625" Name="digitalBackgroundLayerComponent11" 
                                ShapeType="Style9" TopLeft="20, 0" ZOrder="1000" />
                        </backgroundlayers>
                    </dx:DigitalGauge>
                </Gauges>
            </dx:ASPxGaugeControl>
            </div> 
        </div>
        <div class="grid_4 pad_bottom">
             <div>
                <dx:ASPxLabel ID="dxlblCbm" ClientInstanceName="lblCbm" runat="server" Text="Cube ETS today" 
                    Font-Size="Large" Width="100%">
                </dx:ASPxLabel>
            </div>
            <div>   
            <dx:ASPxGaugeControl ID="dxgaugeCbm" 
                ClientInstanceName="gaugeCbm" runat="server" Height="110px" 
                Width="200px" BackColor="White" Value="00,000">
<LayoutPadding All="0" Left="0" Top="0" Right="0" Bottom="0"></LayoutPadding>
                <Gauges>
                    <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#00FFFFFF&quot;/&gt;" 
                        AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:DimGray&quot;/&gt;" 
                        Bounds="0, 0, 200, 110" DigitCount="5" Name="Gauge0" Padding="20, 20, 20, 20" 
                        Text="00,000" 
                        AppearanceOn-BorderBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                        AppearanceOn-BorderWidth="2">
                        <backgroundlayers>
                            <dx:DigitalBackgroundLayerComponent BottomRight="259.8125, 99.9625" Name="digitalBackgroundLayerComponent11" 
                                ShapeType="Style9" TopLeft="20, 0" ZOrder="1000" />
                        </backgroundlayers>
                    </dx:DigitalGauge>
                </Gauges>
            </dx:ASPxGaugeControl>
            </div> 
        </div>
       <div class="clear"></div>
            <!-- 2 columns for charts -->
                <div class="grid_8 pad_bottom">
                             <dxchartsui:WebChartControl ID="dxchartPalletsWeightWW" runat="server" 
                        ClientInstanceName="dxchartPalletsWeightWW" Height="275px" PaletteName="Office" 
                                        Width="415px">
                        <SeriesTemplate><ViewSerializable>
                            <cc1:StackedBar3DSeriesView>
                            </cc1:StackedBar3DSeriesView>
                        </ViewSerializable>
                        <LabelSerializable>
                            <cc1:StackedBar3DSeriesLabel Visible="True">
                                <fillstyle>
                                    <optionsserializable>
                                        <cc1:SolidFillOptions />
                                    </optionsserializable>
                                </fillstyle>
                            </cc1:StackedBar3DSeriesLabel>
                        </LabelSerializable>
                        <PointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </PointOptionsSerializable>
                        <LegendPointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </LegendPointOptionsSerializable>
                        </SeriesTemplate>

                                        <titles>
                                            <cc1:ChartTitle Text="Pallets/Weight ETA next 7 days" />
                                        </titles>
        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
                        <seriesserializable>
                            <cc1:Series Name="Pallets">
                                <viewserializable>
                                    <cc1:SideBySideBarSeriesView>
                                    </cc1:SideBySideBarSeriesView>
                                </viewserializable>
                                <labelserializable>
                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                        <fillstyle>
                                            <optionsserializable>
                                                <cc1:SolidFillOptions />
                                            </optionsserializable>
                                        </fillstyle>
                                    </cc1:SideBySideBarSeriesLabel>
                                </labelserializable>
                                <pointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </pointoptionsserializable>
                                <legendpointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </legendpointoptionsserializable>
                            </cc1:Series>
                            <cc1:Series Name="Weight">
                                <viewserializable>
                                    <cc1:SideBySideBarSeriesView>
                                    </cc1:SideBySideBarSeriesView>
                                </viewserializable>
                                <labelserializable>
                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                        <fillstyle>
                                            <optionsserializable>
                                                <cc1:SolidFillOptions />
                                            </optionsserializable>
                                        </fillstyle>
                                    </cc1:SideBySideBarSeriesLabel>
                                </labelserializable>
                                <pointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </pointoptionsserializable>
                                <legendpointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </legendpointoptionsserializable>
                            </cc1:Series>
                        </seriesserializable>
                        <diagramserializable>
                            <cc1:XYDiagram>
                                <axisx visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisx>
                                <axisy visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisy>
                            </cc1:XYDiagram>
                        </diagramserializable>
                    </dxchartsui:WebChartControl> 
                    </div>
                <div class="grid_8 pad_bottom">
                     
                    <dxchartsui:WebChartControl ID="dxchartContainersWW" runat="server" 
                        ClientInstanceName="chartContainersWW" Height="275px" PaletteName="Office" 
                        Width="415px">
                        <titles>
                            <cc1:ChartTitle Text="Containers ETA next 7 days" />
                        </titles>
        <SeriesTemplate><ViewSerializable>
        <cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
        </ViewSerializable>
        <LabelSerializable>
        <cc1:SideBySideBarSeriesLabel LineVisible="True">
        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
        </cc1:SideBySideBarSeriesLabel>
        </LabelSerializable>
        <PointOptionsSerializable>
        <cc1:PointOptions></cc1:PointOptions>
        </PointOptionsSerializable>
        <LegendPointOptionsSerializable>
        <cc1:PointOptions></cc1:PointOptions>
        </LegendPointOptionsSerializable>
        </SeriesTemplate>

        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
                        <seriesserializable>
                            <cc1:Series Name="Containers">
                                <viewserializable>
                                    <cc1:PieSeriesView RuntimeExploding="False">
                                    </cc1:PieSeriesView>
                                </viewserializable>
                                <labelserializable>
                                    <cc1:PieSeriesLabel LineVisible="True">
                                        <fillstyle>
                                            <optionsserializable>
                                                <cc1:SolidFillOptions />
                                            </optionsserializable>
                                        </fillstyle>
                                    </cc1:PieSeriesLabel>
                                </labelserializable>
                                <pointoptionsserializable>
                                    <cc1:PiePointOptions>
                                    </cc1:PiePointOptions>
                                </pointoptionsserializable>
                                <legendpointoptionsserializable>
                                    <cc1:PiePointOptions>
                                    </cc1:PiePointOptions>
                                </legendpointoptionsserializable>
                            </cc1:Series>
                        </seriesserializable>
                        <diagramserializable>
                            <cc1:SimpleDiagram>
                            </cc1:SimpleDiagram>
                        </diagramserializable>
                    </dxchartsui:WebChartControl>
                     
                </div>
                <div class="clear"></div> 
                 <div class="grid_8 pad_bottom">
                
                    <dxchartsui:WebChartControl ID="dxchartPalletsMM" runat="server" 
                        ClientInstanceName="chartPalletsMM" Height="275px" PaletteName="Office" 
                                        Width="415px">
                        <SeriesTemplate><ViewSerializable>
                            <cc1:StackedBar3DSeriesView>
                            </cc1:StackedBar3DSeriesView>
                        </ViewSerializable>
                        <LabelSerializable>
                            <cc1:StackedBar3DSeriesLabel Visible="True">
                                <fillstyle>
                                    <optionsserializable>
                                        <cc1:SolidFillOptions />
                                    </optionsserializable>
                                </fillstyle>
                            </cc1:StackedBar3DSeriesLabel>
                        </LabelSerializable>
                        <PointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </PointOptionsSerializable>
                        <LegendPointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </LegendPointOptionsSerializable>
                        </SeriesTemplate>

                                        <titles>
                                            <cc1:ChartTitle Text="Pallets ETS last 30 days" />
                                        </titles>
        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
                        <seriesserializable>
                            <cc1:Series Name="Pallets">
                                <viewserializable>
                                    <cc1:SideBySideBarSeriesView>
                                    </cc1:SideBySideBarSeriesView>
                                </viewserializable>
                                <labelserializable>
                                    <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                        <fillstyle>
                                            <optionsserializable>
                                                <cc1:SolidFillOptions />
                                            </optionsserializable>
                                        </fillstyle>
                                    </cc1:SideBySideBarSeriesLabel>
                                </labelserializable>
                                <pointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </pointoptionsserializable>
                                <legendpointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </legendpointoptionsserializable>
                            </cc1:Series>
                        </seriesserializable>
                        <diagramserializable>
                            <cc1:XYDiagram>
                                <axisx visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisx>
                                <axisy visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisy>
                            </cc1:XYDiagram>
                        </diagramserializable>
                    </dxchartsui:WebChartControl>
                
                </div> 
                   <div class="grid_8 pad_bottom">
                
                    <dxchartsui:WebChartControl ID="dxchartWeightMM" runat="server" 
                        ClientInstanceName="chartWeightMM" Height="275px" PaletteName="Office" 
                                                    Width="415px">
                                    <SeriesTemplate><ViewSerializable>
                                        <cc1:SideBySideBarSeriesView>
                                        </cc1:SideBySideBarSeriesView>
                                    </ViewSerializable>
                                    <LabelSerializable>
                                        <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                        </cc1:SideBySideBarSeriesLabel>
                                    </LabelSerializable>
                                    <PointOptionsSerializable>
                                    <cc1:PointOptions></cc1:PointOptions>
                                    </PointOptionsSerializable>
                                    <LegendPointOptionsSerializable>
                                    <cc1:PointOptions></cc1:PointOptions>
                                    </LegendPointOptionsSerializable>
                                    </SeriesTemplate>

                                                    <titles>
                                                        <cc1:ChartTitle Text="Weight ETS last 30 days" />
                                                    </titles>
                    <FillStyle><OptionsSerializable>
                    <cc1:SolidFillOptions></cc1:SolidFillOptions>
                    </OptionsSerializable>
                    </FillStyle>
                                    <seriesserializable>
                                        <cc1:Series Name="Weight">
                                            <viewserializable>
                                                <cc1:SideBySideBarSeriesView>
                                                </cc1:SideBySideBarSeriesView>
                                            </viewserializable>
                                            <labelserializable>
                                                <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                                    <fillstyle>
                                                        <optionsserializable>
                                                            <cc1:SolidFillOptions />
                                                        </optionsserializable>
                                                    </fillstyle>
                                                </cc1:SideBySideBarSeriesLabel>
                                            </labelserializable>
                                            <pointoptionsserializable>
                                                <cc1:PointOptions>
                                                </cc1:PointOptions>
                                            </pointoptionsserializable>
                                            <legendpointoptionsserializable>
                                                <cc1:PointOptions>
                                                </cc1:PointOptions>
                                            </legendpointoptionsserializable>
                                        </cc1:Series>
                                    </seriesserializable>
                                    <diagramserializable>
                                        <cc1:XYDiagram>
                                            <axisx visibleinpanesserializable="-1">
                                                <range sidemarginsenabled="True" />
                                            </axisx>
                                            <axisy visibleinpanesserializable="-1">
                                                <range sidemarginsenabled="True" />
                                            </axisy>
                                        </cc1:XYDiagram>
                                    </diagramserializable>
                    </dxchartsui:WebChartControl>
                
                </div> 
                <!-- end bar charts -->
               <div class="clear"></div>
               <div class="grid_8 pad_bottom">
                            <dxchartsui:WebChartControl ID="dxchartContainersMM" runat="server" 
                        ClientInstanceName="chartContainersMM" Height="275px" PaletteName="Office" 
                                                    Width="415px">
                                    <SeriesTemplate><ViewSerializable>
                                        <cc1:Pie3DSeriesView>
                                        </cc1:Pie3DSeriesView>
                                    </ViewSerializable>
                                    <LabelSerializable>
                                        <cc1:Pie3DSeriesLabel LineVisible="True">
                                            <fillstyle>
                                                <optionsserializable>
                                                    <cc1:SolidFillOptions />
                                                </optionsserializable>
                                            </fillstyle>
                                        </cc1:Pie3DSeriesLabel>
                                    </LabelSerializable>
                                    <PointOptionsSerializable>
                                        <cc1:PiePointOptions>
                                        </cc1:PiePointOptions>
                                    </PointOptionsSerializable>
                                    <LegendPointOptionsSerializable>
                                        <cc1:PiePointOptions>
                                        </cc1:PiePointOptions>
                                    </LegendPointOptionsSerializable>
                                    </SeriesTemplate>
                    
                                                    <titles>
                                                        <cc1:ChartTitle Text="Containers ETS last 30 days" />
                                                    </titles>
                    <FillStyle><OptionsSerializable>
                    <cc1:SolidFillOptions></cc1:SolidFillOptions>
                    </OptionsSerializable>
                    </FillStyle>
                                    <seriesserializable>
                                        <cc1:Series Name="Containers">
                                            <viewserializable>
                                                <cc1:PieSeriesView RuntimeExploding="False">
                                                </cc1:PieSeriesView>
                                            </viewserializable>
                                            <labelserializable>
                                                <cc1:PieSeriesLabel LineVisible="True">
                                                    <fillstyle>
                                                        <optionsserializable>
                                                            <cc1:SolidFillOptions />
                                                        </optionsserializable>
                                                    </fillstyle>
                                                </cc1:PieSeriesLabel>
                                            </labelserializable>
                                            <pointoptionsserializable>
                                                <cc1:PiePointOptions>
                                                </cc1:PiePointOptions>
                                            </pointoptionsserializable>
                                            <legendpointoptionsserializable>
                                                <cc1:PiePointOptions>
                                                </cc1:PiePointOptions>
                                            </legendpointoptionsserializable>
                                        </cc1:Series>
                                    </seriesserializable>
                                    <diagramserializable>
                                        <cc1:SimpleDiagram>
                                        </cc1:SimpleDiagram>
                                    </diagramserializable>
                    </dxchartsui:WebChartControl>
               </div>
               <div class="grid_8 pad_bottom">
               <dxchartsui:WebChartControl ID="dxchartCubeMM" runat="server" 
                        ClientInstanceName="chartCubeMM" Height="275px" PaletteName="Office" 
                                        Width="415px">
                        <SeriesTemplate><ViewSerializable>
                            <cc1:StackedBar3DSeriesView>
                            </cc1:StackedBar3DSeriesView>
                        </ViewSerializable>
                        <LabelSerializable>
                            <cc1:StackedBar3DSeriesLabel Visible="True">
                                <fillstyle>
                                    <optionsserializable>
                                        <cc1:SolidFillOptions />
                                    </optionsserializable>
                                </fillstyle>
                            </cc1:StackedBar3DSeriesLabel>
                        </LabelSerializable>
                        <PointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </PointOptionsSerializable>
                        <LegendPointOptionsSerializable>
                        <cc1:PointOptions></cc1:PointOptions>
                        </LegendPointOptionsSerializable>
                        </SeriesTemplate>

                                        <titles>
                                            <cc1:ChartTitle Text="Cube ETS last 30 days" />
                                        </titles>
        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
                        <seriesserializable>
                            <cc1:Series Name="Cube">
                                <viewserializable>
                                    <cc1:SideBySideBarSeriesView>
                                    </cc1:SideBySideBarSeriesView>
                                </viewserializable>
                                <labelserializable>
                                    <cc1:SideBySideBarSeriesLabel LineVisible="True" Visible="False">
                                        <fillstyle>
                                            <optionsserializable>
                                                <cc1:SolidFillOptions />
                                            </optionsserializable>
                                        </fillstyle>
                                    </cc1:SideBySideBarSeriesLabel>
                                </labelserializable>
                                <pointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </pointoptionsserializable>
                                <legendpointoptionsserializable>
                                    <cc1:PointOptions>
                                    </cc1:PointOptions>
                                </legendpointoptionsserializable>
                            </cc1:Series>
                        </seriesserializable>
                        <diagramserializable>
                            <cc1:XYDiagram>
                                <axisx visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisx>
                                <axisy visibleinpanesserializable="-1">
                                    <range sidemarginsenabled="True" />
                                </axisy>
                            </cc1:XYDiagram>
                        </diagramserializable>
                    </dxchartsui:WebChartControl>
               </div> 
               <div class="clear"></div>
               <div class="grid_16 pad_bottom">
                   <dxchartsui:WebChartControl ID="dxchartMetrics" runat="server" 
                       ClientInstanceName="chartMetrics" Height="275px" PaletteName="Office" 
                       Width="855px">
                       <titles>
                           <cc1:ChartTitle Text="Pallets ETS last 30 days" />
                       </titles>
        <SeriesTemplate><ViewSerializable>
        <cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
        </ViewSerializable>
        <LabelSerializable>
        <cc1:SideBySideBarSeriesLabel LineVisible="True">
        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
        </cc1:SideBySideBarSeriesLabel>
        </LabelSerializable>
        <PointOptionsSerializable>
        <cc1:PointOptions></cc1:PointOptions>
        </PointOptionsSerializable>
        <LegendPointOptionsSerializable>
        <cc1:PointOptions></cc1:PointOptions>
        </LegendPointOptionsSerializable>
        </SeriesTemplate>

        <FillStyle><OptionsSerializable>
        <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
        </FillStyle>
                       <seriesserializable>
                           <cc1:Series Name="Lowest">
                               <viewserializable>
                                   <cc1:LineSeriesView>
                                   </cc1:LineSeriesView>
                               </viewserializable>
                               <labelserializable>
                                   <cc1:PointSeriesLabel Angle="-30" Antialiasing="True" LineVisible="True">
                                       <fillstyle>
                                           <optionsserializable>
                                               <cc1:SolidFillOptions />
                                           </optionsserializable>
                                       </fillstyle>
                                   </cc1:PointSeriesLabel>
                               </labelserializable>
                               <pointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </pointoptionsserializable>
                               <legendpointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </legendpointoptionsserializable>
                           </cc1:Series>
                           <cc1:Series Name="Average">
                               <viewserializable>
                                   <cc1:LineSeriesView>
                                   </cc1:LineSeriesView>
                               </viewserializable>
                               <labelserializable>
                                   <cc1:PointSeriesLabel Angle="-30" LineVisible="True">
                                       <fillstyle>
                                           <optionsserializable>
                                               <cc1:SolidFillOptions />
                                           </optionsserializable>
                                       </fillstyle>
                                   </cc1:PointSeriesLabel>
                               </labelserializable>
                               <pointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </pointoptionsserializable>
                               <legendpointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </legendpointoptionsserializable>
                           </cc1:Series>
                           <cc1:Series Name="Highest">
                               <viewserializable>
                                   <cc1:LineSeriesView>
                                   </cc1:LineSeriesView>
                               </viewserializable>
                               <labelserializable>
                                   <cc1:PointSeriesLabel Angle="-30" LineVisible="True">
                                       <fillstyle>
                                           <optionsserializable>
                                               <cc1:SolidFillOptions />
                                           </optionsserializable>
                                       </fillstyle>
                                   </cc1:PointSeriesLabel>
                               </labelserializable>
                               <pointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </pointoptionsserializable>
                               <legendpointoptionsserializable>
                                   <cc1:PointOptions>
                                   </cc1:PointOptions>
                               </legendpointoptionsserializable>
                           </cc1:Series>
                       </seriesserializable>
                       <diagramserializable>
                           <cc1:XYDiagram>
                               <axisx visibleinpanesserializable="-1">
                                   <range sidemarginsenabled="True" />
                               </axisx>
                               <axisy visibleinpanesserializable="-1">
                                   <range sidemarginsenabled="True" />
                               </axisy>
                           </cc1:XYDiagram>
                       </diagramserializable>
                   </dxchartsui:WebChartControl>
	    </div>
       <div class="clear"></div> 
       <!-- data grid make sure the titles combobox is dropdownstyle=dropdown or you won't be able to insert new values -->
        <div class="grid_16">  
            <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
        </div>
             <div class="clear"></div>
       </div>      
    </div>
        
</asp:Content>

