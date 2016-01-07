<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="downloads.aspx.cs" Inherits="user_downloads" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxFileManager" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    <div class="formcenter650">
        <!-- embeds folder -->
        <table id="tblDownloads" width="100%" cellpadding="5px">
            <tbody>
                <tr>
                    <td >
                        <dx:ASPxLabel ID="dxlblTitle" runat="server" 
                             ClientInstanceName="lblTitle" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                Text="Downloads">
                         </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                   <td>
                    <iframe src="https://onedrive.live.com/embed?cid=7F09563CA13C8785&resid=7F09563CA13C8785%21139&authkey=AOFl3w0nOgZDcRM" width="100%" height="500px" frameborder="0" scrolling="no"></iframe>
                   </td>
                </tr>
            </tbody> 
        </table>
        <!-- <iframe src="https://onedrive.live.com/embed?cid=7F09563CA13C8785&resid=7F09563CA13C8785%21139&authkey=AOFl3w0nOgZDcRM" width="760" height="500" frameborder="0" scrolling="no"></iframe> -->
        <!-- embeds actual file -->
        <!-- <iframe src="https://onedrive.live.com/embed?cid=7F09563CA13C8785&resid=7F09563CA13C8785%21140&authkey=AH3EKTfiZN443s0&em=2" width="650" height="500" frameborder="0" scrolling="no"></iframe> -->
       
    </div>
    </asp:Content>

