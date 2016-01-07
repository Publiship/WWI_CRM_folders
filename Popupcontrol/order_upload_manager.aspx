<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_upload_manager.aspx.cs" Inherits="Popupcontrol_order_upload_manager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
        
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>
    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDocking" tagprefix="dx" %>
    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document manager</title>
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/Widget.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/720gs12col_fixed.css" type="text/css" />
    
     <script type="text/javascript">
        // <![CDATA[
        
        function ShowWidgetPanel(widgetPanelUID) {
            var panel = dockmanagefiles.GetPanelByUID(widgetPanelUID);
            panel.Show();
        }
        function SetWidgetButtonVisible(widgetName, visible) {
            var button = ASPxClientControl.GetControlCollection().GetByName('widgetButton_' + widgetName);
            button.GetMainElement().className = visible ? '' : 'disabled';
        }

        function onfilesuploadcomplete(s, e) {
            hfmanager.Set("upld", true); //flag to confirm folder create
            cbpodfiles.PerformCallback(s, e);
        }

        function onconfirmcreatefolder(s, e) {
            hfmanager.Set("cdir", 1); //flag to confirm folder create
            popmsgbox.Hide(); //close message
            cbpodfiles.PerformCallback(' '); //force postback
        }

        function ondenycreatefolder(s, e) { 
            hfmanager.Set("cdir", 2); //flag so we don't ask to create folder again
	        popmsgbox.Hide();
        }
        
        function onshowfolders(s, e) {
            var tx = btnfolders.GetText();
            hfmanager.Set("view", tx.toString());
            
            //alert(tx);
            if (tx == "All folders") {
                tx = "Hide folders";
                            }
            else {
                tx = "All folders";
            }

            btnfolders.SetText(tx);
            
            cbpodfiles.PerformCallback(' '); //force postback
        }

    
        function select_ordernos(s, e) {
            e.processOnServer = false;
            var window = popDefault.GetWindowByName('filterform');
            popDefault.SetWindowContentUrl(window, '');
            popDefault.SetWindowContentUrl(window, '../Popupcontrol/order_selector.aspx?qr=fm'); //qr=filemanager
            //popDefault.SetWindowContentUrl(window, 'Popupcontrol/Pod_Search.aspx?');

            popDefault.ShowWindow(window);
        }

        function submit_order_callback(s) {
            cborder.PerformCallback(s);
            cbpodfiles.PerformCallback(); //have to call this or upload control doesn't work properly til you add/remove a file!
        }
        
        // ]]>
    </script> 
</head>
<body>
    <form id="form2" runat="server">
    <div class="container_12">
        <!-- order number selection -->
        <div class="grid_2">
        
             <dx:ASPxLabel ID="dxlblinfo1" runat="server" ClientInstanceName="lblinfo1" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                    Text=" Order number(s) ">
            </dx:ASPxLabel>
        
        </div>
        <!-- button to add orders -->
        <div class="grid_4">
        
                      <dx:ASPxButton ID="dxbtnaddorder" ClientInstanceName="btnaddorder" runat="server" 
                                            Image-Height="16px" Image-Width="16px" Height="20px" 
                                            HorizontalAlign="Center" VerticalAlign="Middle" 
                          Wrap="False" EnableDefaultAppearance="False"
                                            CausesValidation="False" 
                                            Width="220px" AutoPostBack="False" Cursor="pointer" 
                          Font-Italic="True" Text="click to add an order number">
                                        <ClientSideEvents Click="function(s, e) {select_ordernos(s, e);}" />
                                        <Image Url="~/Images/add-icon.png"></Image>
                                         <Paddings Padding="1px" />
                                    <Border BorderStyle="None" /> 
                                        </dx:ASPxButton>
        
        </div>
        <!-- button to switch to all folders -->
        <div class="grid_6">
        
                      <dx:ASPxButton ID="dxbtnfolders" runat="server" 
                                        TabIndex="3" 
                                        ClientInstanceName="btnfolders" 
                                        ToolTip="Click here to see all folders" 
                                        EnableTheming="True"  
                                        Height="24px" 
                                 Text="All folders" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            UseSubmitBehavior="False" AutoPostBack="False" 
                          CausesValidation="False" onclick="dxbtnfolders_Click" Wrap="False" 
                          ClientEnabled="False" ClientVisible="False" Enabled="False" Visible="False">
                                       <Image Height="16px" Width="16px" AlternateText="Begin search" 
                                           Url="~/Images/icons/16x16/folders_explorer.png">
                                       </Image>
                                      
                              </dx:ASPxButton>
        
        </div>
        <div class="clear"></div>
        <!-- callback panel holding datalist of selected orders -->
        <div class="grid_12">
        
                <dx:ASPxCallbackPanel ID="dxcborder" ClientInstanceName="cborder" 
                            runat="server" oncallback="dxcborder_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                        <PanelCollection>
                        <dx:PanelContent runat="server" ID="pnlorder">
                            <asp:DataList ID="dlorder" runat="server" RepeatColumns="5"  Width="600px"
                                RepeatDirection="Horizontal" OnItemCommand="dlorder_ItemCommand"> 
                                <ItemTemplate>
                                
                                <div style="width: 120px">
                                    <div style="float: left; margin-left: 10px; width: 70px">
                                        <dx:ASPxLabel ID="dxlblorderno" ClientInstanceName="lblorderno" runat="server"  Text='<%# Container.DataItem.ToString() %>'>
                                        </dx:ASPxLabel>
                                    </div>
                                    <div style="float: right; width: 35px">
                                         <dx:ASPxButton ID="dxbtndelorder" ClientInstanceName="btdelnorder" runat="server" BackColor="White" Image-Height="16px" Image-Width="16px" AutoPostBack="true"  Cursor="pointer"  
                                                     Height="16px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" EnableDefaultAppearance="False" CommandName="delete" CommandArgument='<%# Container.DataItem.ToString() %>'>
                                                <Image Url="~/Images/delete-icon.png">
                                                </Image>
                                                <Paddings Padding="3px" />
                                                <Border BorderStyle="None" />
                                            </dx:ASPxButton>   
                                    </div>
                                </div>
                            </ItemTemplate>     
                            </asp:DataList>
                        </dx:PanelContent> 
                        </PanelCollection> 
                        </dx:ASPxCallbackPanel>
        
        </div>  
        <div class="clear"></div>
        <!-- filemanager and upload control -->
        <div class="grid_12">
                  <!-- error and info panel -->
                  <dx:ASPxCallbackPanel ID="dxcbpodfiles" runat="server" 
                      ClientInstanceName="cbpodfiles" 
                      oncallback="dxcbpodfiles_Callback">
                      <PanelCollection>
                    <dx:PanelContent runat="server">
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
                                <dx:ASPxLabel ID="dxlblinfo2" ClientInstanceName="lblinfo2" runat="server" BackColor="#E4EBF1" CssClass="info" 
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
                   <!-- end info panels -->
                   <!-- begin filemanager -->
                 
                   <div style="margin-top: 10px; margin-bottom: 10px">
                        <dx:ASPxFileManager ID="dxfmpod" runat="server" ClientInstanceName="fmpod" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Height="400px">
                            <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                <FolderContainerNodeLoadingPanel Url="~/App_Themes/Office2010Blue/Web/tvNodeLoading.gif">
                                </FolderContainerNodeLoadingPanel>
                                <LoadingPanel Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                </LoadingPanel>
                            </Images>
                            <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                                <LoadingPanel ImageSpacing="5px">
                                </LoadingPanel>
                            </Styles>
                            <ClientSideEvents FileUploaded="function(s, e) {
	                                    //pnlinfo.SetVisible(!pnlinfo.GetVisible());
	                                    pnlinfo.SetVisible(false);
                                    }" SelectedFileOpened="function(s, e) {
	                                    e.file.Download();
                                        e.processOnServer = false;
                                    }" />
                            <Settings RootFolder="~\documents\" ThumbnailFolder="~\Thumb\" />
                            <SettingsEditing AllowCreate="True" AllowDelete="True" AllowMove="True" 
                                AllowRename="True" />
                            <SettingsToolbar ShowDownloadButton="True" ShowPath="False" />
                        </dx:ASPxFileManager>
                      </div>
        
                        <!-- end filemanager  -->
                          <!-- upload controls -->
                        <div>
                
                                        <dx:ASPxUploadControl ID="dxuploadpodfiles" runat="server" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" ShowAddRemoveButtons="True" 
                                            ShowProgressPanel="True" ShowUploadButton="True" Width="100%" 
                                            ClientInstanceName="uploadpodfiles" 
                                            onfileuploadcomplete="dxuploadpodfiles_FileUploadComplete" 
                                            UploadMode="Advanced">
                                                                        <AddButton Image-Height="16px" Image-Url="../Images/icons/16x16/page_white_add.png" Image-Width="16px" Text=" Add file">
                <Image Height="16px" Width="16px" Url="~/Images/icons/16x16/page_white_add.png"></Image>
                                            </AddButton>
                                            <RemoveButton Image-Height="16px" Image-Url="../Images/icons/16x16/page_white_delete.png" Image-Width="16px" Text=" Remove file">
                <Image Height="16px" Width="16px" Url="~/Images/icons/16x16/page_white_delete.png"></Image>
                                            </RemoveButton> 
                                            <UploadButton  Image-Height="16px" Image-Url="../Images/icons/16x16/arrow_up.png" Image-Width="16px" Text=" Upload">
                <Image Height="16px" Width="16px" Url="~/Images/icons/16x16/arrow_up.png"></Image>
                                            </UploadButton> 
                                            <ClientSideEvents FilesUploadComplete="function(s, e) {
	                onfilesuploadcomplete(s,e);
                }" />
                                        </dx:ASPxUploadControl>    
                       
                    </div>
                     <!-- end upload control -->
                
                       </dx:PanelContent>
                    </PanelCollection>
                  </dx:ASPxCallbackPanel>
                <!-- end callback panel -->
        </div>  
        <!-- end filemanager controls -->
        <div class="clear"></div>
        <!-- popup control for confirmation to create folder -->
        <div class="grid_12">
                <dx:ASPxPopupControl ID="dxpopmsgbox" ClientInstanceName="popmsgbox" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" EnableHotTrack="False" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                        AppearAfter="100" Modal="True" AllowDragging="True" 
                        CloseAction="CloseButton" PopupAction="None" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                        HeaderText="Create folder">
                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                        </LoadingPanelImage>
                        <ContentCollection>     
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <div style="margin: 0px Auto; width: 250px">

                        <div style="margin: 10px; height: 80px">
                        <dx:ASPxLabel ID="dxlblMsgbox" ClientInstanceName="lblMsgbox" runat="server" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Text="Do you want to create this folder now?">
                        </dx:ASPxLabel>
                        </div>
                        
                       <div style="margin: 10px; height: 25px">
                        
                                <div style="float: left; width: 90px">
                                    <dx:ASPxButton ID="dxbtnyes" ClientInstanceName="btnyes"  runat="server" 
                                        Text="Yes" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                        CssPostfix="Office2010Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                        CausesValidation="False" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
	                onconfirmcreatefolder(s,e);
                }" />
                                    </dx:ASPxButton>
                                </div>
                                 <div style="float: right; width: 90px">
                                    <dx:ASPxButton ID="dxbtncancel" ClientInstanceName="btncancel"  runat="server" 
                                         Text="No" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                         CssPostfix="Office2010Blue" 
                                         SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                         AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="function(s, e) {
	                ondenycreatefolder(s, e);
                }" />
                                    </dx:ASPxButton>
                                </div>
                         </div>        
                          
                        </div>
                </dx:PopupControlContentControl>
                </ContentCollection>
                <LoadingPanelStyle ImageSpacing="5px">
                </LoadingPanelStyle>
          </dx:ASPxPopupControl>
        </div>
        <div class="clear"></div> 
        <!-- popup control for order number selection -->
        <div>
          <dx:ASPxPopupControl ClientInstanceName="popDefault" ID="dxpopDefault" 
                    runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                    CssPostfix="Office2010Blue" EnableHotTrack="False" Width="730px"
                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" PopupHorizontalAlign="WindowCenter" 
                    PopupVerticalAlign="WindowCenter" Modal="True" Height="520px" 
                AllowDragging="True" AllowResize="True">
                     <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                     </LoadingPanelImage>
                     <ContentCollection>
                         <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                         </dx:PopupControlContentControl>
                     </ContentCollection>
                     <LoadingPanelStyle ImageSpacing="5px">
                     </LoadingPanelStyle>
                     <Windows>
                 <dx:PopupWindow CloseAction="CloseButton" 
                            HeaderText="Add an order number"
                             Width="730px" Modal="True" Name="filterform" PopupAction="None" 
                             PopupElementID="dxbtnmore" Height="560px">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:PopupWindow>
                        </Windows> 
             </dx:ASPxPopupControl>
        </div>
        <!-- additional controls -->
        <div>
          <!-- hidden fields -->
                   <dx:ASPxHiddenField ID="dxhfmanager" ClientInstanceName="hfmanager" runat="server">
                       </dx:ASPxHiddenField>
        </div>  
    </div> 
    </form>
</body>
</html>
