<%@ Control Language="C#" ClassName="TodayShipmentsWidget" AutoEventWireup="true" CodeFile="weights_shipped.ascx.cs" Inherits="usercontrols_weights_shipped" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1.Linq, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Data.Linq" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<script type="text/javascript">
    // <![CDATA[
    //this function is called from guageContainers but causes all the totals to be updated
    function onGuageTotalsInit(s, e) {
        if (!guageTotals.InCallback()) {
            dxgaugeContainers.PerformCallback();
        }
    }
    // ]]>
    </script>

<div>
    <dx:ASPxGaugeControl ID="dxguageTotals" ClientInstanceName="guageTotals" 
        runat="server" Height="250px" Width="250px" BackColor="White" Value="00,000">
        <Gauges>
            <dx:DigitalGauge AppearanceOff-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#C8C8C8&quot;/&gt;" 
                AppearanceOn-ContentBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                Bounds="0, 0, 250, 250" DigitCount="5" Name="Gauge0" Padding="20, 20, 20, 20" 
                Text="00,000">
                <backgroundlayers>
                    <dx:DigitalBackgroundLayerComponent BottomRight="259.8125, 99.9625" 
                        Name="digitalBackgroundLayerComponent13" ShapeType="Style11" TopLeft="20, 0" 
                        ZOrder="1000" />
                </backgroundlayers>
            </dx:DigitalGauge>
        </Gauges>
    </dx:ASPxGaugeControl>
</div>
  <div>
    <dx:ASPxLabel ID="dxlblerr1" ClientInstanceName="lblerr1" runat="server" 
           Text="[Error Message]" Font-Size="Medium" ForeColor="#CC0000" Width="250px" 
           Wrap="True">
    </dx:ASPxLabel>
    </div>
<div>
      
    <dx:LinqServerModeDataSource ID="linqAggregatesToday" runat="server" 
                    ContextTypeName="linq.linq_aggegate_containers_udfDataContext" 
                    TableName="aggregate_containers_by_ets" />
      
</div>



