<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barcode.aspx.cs" Inherits="Popupcontrol_barcode" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxImage ID="dximg" ClientInstanceName="img" runat="server">
        </dx:ASPxImage>
    </div>
    </form>
</body>
</html>
