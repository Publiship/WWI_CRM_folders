<%@ Control Language="C#" ClassName="DateTimeWidget" AutoEventWireup="true" CodeFile="Sys_DateTime.ascx.cs" Inherits="usercontrols_Sys_DateTime" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTimer" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<script type="text/javascript" language="javascript">
// <![CDATA[
    function PrepareTimeValue(value) {
        if (value < 10)
            value = "0" + value;
        return value;
    }
    function UpdateTime(s, e) {
        var dateTime = new Date();
        var timeString = PrepareTimeValue(dateTime.getHours()) + ":" + PrepareTimeValue(dateTime.getMinutes()) + ":" +
            PrepareTimeValue(dateTime.getSeconds());
        timeLabel.SetText(timeString);
    }
// ]]>
</script>
<dx:ASPxTimer runat="server" ID="Timer" ClientInstanceName="timer" Interval="1000">
    <ClientSideEvents Init="UpdateTime" Tick="UpdateTime" />
</dx:ASPxTimer>
<div>
    <div style="margin: 0px Auto">
        <dx:ASPxLabel runat="server" ID="TimeLabel" ClientInstanceName="timeLabel" Font-Bold="true"
            Font-Size="X-Large">
        </dx:ASPxLabel>
    </div>
    <div style="margin: 0px Auto">
        <dx:ASPxLabel runat="server" ID="DateLabel" ClientInstanceName="dateLabel" 
            Font-Size="14px">
        </dx:ASPxLabel>
    </div>
</div>

