<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ord_Edit_Pallet.aspx.cs" Inherits="Ord_Edit_Pallet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../App_Themes/3columnfluid.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/fluid_blue.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
    <title>Pallet information</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="formcenter580">
        <div style="padding: 8px; clear: both;">
            <dx:ASPxLabel ID="dxlblsearchbox" runat="server" 
                Text="Update your pallet information here" ForeColor="#333333" 
                ToolTip="Quick reference searches">
            </dx:ASPxLabel>
        </div> 
        <!--  cargo announced ready -->
        <dl class="dl1">
            <dt> <dx:ASPxLabel ID="dxlblcargoready" runat="server" 
                ClientInstanceName="lblcargoready" Text="Cargo ready">
            </dx:ASPxLabel>
            &nbsp;date</dt>
            <dd>
             <dx:ASPxDateEdit ID="dxdtcargoready" runat="server" 
                ClientInstanceName="dtcargoready" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                Width="130px">
                <ButtonStyle Width="13px">
                </ButtonStyle>
            </dx:ASPxDateEdit>
            </dd>
             <!--  estimated pallets -->
        <dt>
            <dx:ASPxLabel ID="dxlblpallets" runat="server" ClientInstanceName="lblpallets" 
                Text="Estimated pallets">
            </dx:ASPxLabel>
        </dt>
        <dd>
            <dx:ASPxTextBox ID="dxtxtpallets" runat="server" 
                ClientInstanceName="txtpallets" Width="130px" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                <ValidationSettings>
                    <RegularExpression ValidationExpression="^[0-9]+$" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </dd>
         <!--  estimated weight -->
        <dt>
            <dx:ASPxLabel ID="dxlblweight" runat="server" Text="Estimated weight">
            </dx:ASPxLabel>
        </dt> 
        <dd>
            <dx:ASPxTextBox ID="dxtxtweight" runat="server" ClientInstanceName="txtweight" 
                Width="130px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                <ValidationSettings>
                    <RegularExpression ValidationExpression="^[0-9]+$" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </dd>
         <!--  estimated volume -->
        <dt>
            <dx:ASPxLabel ID="dxlblvolume" runat="server" ClientInstanceName="lblvolume" 
                Text="Estimated volume">
            </dx:ASPxLabel>
        </dt>
        <dd>
            <dx:ASPxTextBox ID="dxtxtvolume" runat="server" ClientInstanceName="txtvolume" 
                Width="130px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                <ValidationSettings>
                    <RegularExpression ValidationExpression="^[0-9.]+$" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </dd> 
           <!--  command buttons edit/cancel -->         
        <dt>
            <dx:ASPxButton ID="dxbtnupdate" runat="server" ClientInstanceName="dbnupdate" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                Text="Update" onclick="dxbtnupdate_Click" UseSubmitBehavior="False">
            </dx:ASPxButton>
        </dt>
        <dd>
        <dx:ASPxButton ID="btnCancel" runat="server" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Cancel" 
                            Width="100px" CausesValidation="False" 
                            AutoPostBack="False">
                            <ClientSideEvents 
                                Click="function(s, e) {
window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('editpalletform'));}" />
                         </dx:ASPxButton>
        </dd> 
        </dl>
         
    </div> 
    </form>
</body>
</html>
