<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_file_manager.aspx.cs" Inherits="Popupcontrol_order_file_manager" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search documents</title>
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- panels -->
     <div style="margin: 10px;">
                <dx:ASPxPanel ID="dxpnlerr" ClientInstanceName="pnlerr" runat="server" Visible="false">
                    <PanelCollection>
                        <dx:PanelContent ID="msg1"> 
                        <dx:ASPxLabel ID="dxlblerr" ClientInstanceName="lblerr" runat="server" BackColor="#FFE6DF" CssClass="alert" 
                                            CssFilePath="~/App_Themes/typography.css" CssPostfix="alert" 
                                            EnableDefaultAppearance="False" Font-Names="Arial,Helvetica,Sans-serif" 
                                            Font-Size="Small" ForeColor="#CC0000" Height="40px" width="100%"
                                            Text=" Error message" >
                                            <Border BorderColor="#FFD9CF" BorderStyle="Solid" BorderWidth="1px" />
                                            <BackgroundImage 
                                                ImageUrl="~/Images/typography/box_alert.png" Repeat="NoRepeat" HorizontalPosition="right" 
                                                VerticalPosition="top" />
                                        </dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            <dx:ASPxPanel ID="dxpnlinfo" ClientInstanceName="pnlinfo" runat="server" Visible="false">
                <PanelCollection>
                    <dx:PanelContent ID="msg2"> 
                        <dx:ASPxLabel ID="dxlblinfo" ClientInstanceName="lblinfo" runat="server" BackColor="#E4EBF1" CssClass="info" 
                                            CssFilePath="~/App_Themes/typography.css" CssPostfix="alert" 
                                            EnableDefaultAppearance="False" Font-Names="Arial,Helvetica,Sans-serif" 
                                            Font-Size="Small" ForeColor="#333435" Height="40px" width="100%"
                                            Text=" Info message" >
                                            <Border BorderColor="#D4D9DE" BorderStyle="Solid" BorderWidth="1px" />
                                            <BackgroundImage 
                                                ImageUrl="../Images/typography/box_info.png" Repeat="NoRepeat" HorizontalPosition="right"  
                                                VerticalPosition="top" />
                                        </dx:ASPxLabel>
                        </dx:PanelContent>        
                </PanelCollection> 
           </dx:ASPxPanel>

      <dx:ASPxPanel ID="dxpnlfiles" ClientInstanceName="pnlfiles" runat="server" Visible="true">
      <PanelCollection>
      <dx:PanelContent ID="filemanager"> 
        <div style="margin-top: 10px;">
        <dx:ASPxFileManager ID="dxfmpod" ClientInstanceName="fmpod" runat="server" 
            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
            CssPostfix="Office2010Blue" Height="450px">
            <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                <FolderContainerNodeLoadingPanel Url="~/App_Themes/Office2010Blue/Web/tvNodeLoading.gif">
                </FolderContainerNodeLoadingPanel>
                <LoadingPanel Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                </LoadingPanel>
            </Images>
            <Settings RootFolder="~\documents\" ThumbnailFolder="~\Thumb\" />
            <SettingsToolbar ShowPath="False" ShowDownloadButton="True" />
            <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue">
                <LoadingPanel ImageSpacing="5px">
                </LoadingPanel>
            </Styles>
            <ClientSideEvents SelectedFileOpened="function(s, e) {
	e.file.Download();
    e.processOnServer = false;
}" FileUploaded="function(s, e) {
	//pnlinfo.SetVisible(!pnlinfo.GetVisible());
	pnlinfo.SetVisible(false);
}" />
            <SettingsEditing AllowCreate="True" AllowDelete="True" AllowMove="True" 
                AllowRename="True" />
        </dx:ASPxFileManager>
        </div> 
        </dx:PanelContent>        
        </PanelCollection> 
        </dx:ASPxPanel>
        </div>
        <!-- end panels -->
        <!-- close button -->
        <div class="top-panel-shaded">  
        <div class="cell100right">
             <dx:ASPxButton ID="dxbtnclose" ClientInstanceName="btnclose" runat="server" 
                  Text="Close" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                  CssPostfix="Office2010Blue" 
                  SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                Height="26px" AutoPostBack="False" CausesValidation="False" 
                 UseSubmitBehavior="False" Width="110px">
                <Image Height="16px" Url="~/Images/icons/16x16/cross.png" Width="16px">
                </Image>
                 <ClientSideEvents Click="function(s, e) {
	window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('podfiles'));
}" />
            </dx:ASPxButton>   
        </div>  
    </div>
    <!-- end button div -->
    </form>
</body>
</html>
