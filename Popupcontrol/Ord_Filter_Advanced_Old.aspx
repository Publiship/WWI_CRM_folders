<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ord_Filter_Advanced_Old.aspx.cs" Inherits="Ord_Filter_Advanced_Old" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="scm_filter" runat="server" />
      <div>  
        <dx:ASPxFilterControl ID="advancedFilter" runat="server" 
            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
            CssPostfix="Office2003Blue">
            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue">
            </Styles>
            <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                <LoadingPanel Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                </LoadingPanel>
            </Images>
           <ClientSideEvents Applied="function(s, e) {  }" />
        </dx:ASPxFilterControl> 
    
        <table> 
            <tr>
                <td>
                    <dx:ASPxLabel ID="aspxlblName" runat="server" Text="Name this filter">
                    </dx:ASPxLabel> 
                </td> 
                <td>
                    <dx:ASPxTextBox ID="aspxtxtName" ClientInstanceName="txtName" runat="server" 
                        Width="160px" NullText="Type a name for your filter">
                    </dx:ASPxTextBox>
                </td> 
            </tr>
            <tr>
                <td style="width:180px">
                        <dx:ASPxButton ID="btnApply" runat="server" Text="Apply"
                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                        UseSubmitBehavior="False" onclick="btnApply_Click">
                        </dx:ASPxButton>
                </td>
                <td style="width:180px">
                        <dx:ASPxButton ID="btnCancel" runat="server" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Cancel" 
                            Width="100px" CausesValidation="False" 
                            AutoPostBack="False">
                            <ClientSideEvents 
                                Click="function(s, e) {
	                            window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));}" />
                         </dx:ASPxButton>
            </td>
            </tr>
        </table>
        
        <dx:ASPxTextBox ID="txtQueryResult" runat="server" 
            ClientInstanceName="txtQuery" ReadOnly="True" Width="170px" 
            ClientVisible="False">
        </dx:ASPxTextBox>
    </div> 

    </form>
</body>
</html>
