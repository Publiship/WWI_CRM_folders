<%@ Control Language="C#" ClassName="CalendarWidget" AutoEventWireup="true" CodeFile="Sys_Calendar.ascx.cs" Inherits="usercontrols_Sys_Calendar" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<dx:ASPxCalendar runat="server" ID="Calendar" ShowClearButton="false" ShowHeader="false"
    ShowTodayButton="false" ShowWeekNumbers="false" HighlightToday="false" 
    width="100%">
    <Border BorderStyle="None" />
</dx:ASPxCalendar>

