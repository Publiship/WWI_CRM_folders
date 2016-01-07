<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_tracking_demo.aspx.cs" Inherits="Popupcontrol_order_tracking_demo" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxObjectContainer" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxObjectContainer ID="ASPxObjectContainer1" runat="server" Height="550px" 
            ObjectType="Flash" ObjectUrl="~/Include/ord_tracking_basic.swf" Width="750px" >
        </dx:ASPxObjectContainer>
    </div>
    </form>
</body>
</html>
