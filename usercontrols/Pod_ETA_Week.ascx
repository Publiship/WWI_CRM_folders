<%@ Control Language="C#" ClassName="PodEtaWeekWidget" AutoEventWireup="true" CodeFile="Pod_ETA_Week.ascx.cs" Inherits="usercontrols_Pod_ETA_Week" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTimer" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.1.Web, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<div>
    <dxchartsui:WebChartControl ID="dxchartEta" runat="server" 
        ClientInstanceName="chartEta" Height="285px" Width="285px" 
        AppearanceName="Light" PaletteBaseColorNumber="1" PaletteName="Flow">
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

<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
        <seriesserializable>
            <cc1:Series Name="Series 1">
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
        <borderoptions visible="False" />
    </dxchartsui:WebChartControl>
</div>

   <div>
    <dx:ASPxLabel ID="dxlblerr3" ClientInstanceName="lblerr3" runat="server" 
           Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" Width="250px" 
           Wrap="True">
    </dx:ASPxLabel>
    </div>
