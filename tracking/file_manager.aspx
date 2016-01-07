<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="file_manager.aspx.cs" Inherits="file_manager" %>

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
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">

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
    
    <!-- panels -->
     <div style="width: 760px; margin: 0px Auto 10px; padding: 10px">
        <div class="shadowPanel">
            <div style="float: left; padding-left: 10px; width: 100px">
             <dx:ASPxLabel ID="dxlblinfo1" runat="server" ClientInstanceName="lblinfo1" 
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                CssPostfix="Office2003Blue" 
                    Text=" Order number(s) ">
            </dx:ASPxLabel>
            </div> 
            
               <div style="float: left; padding-right: 10px; width: 220px">
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
            
            <div style="float: right; padding-right: 10px; width: 120px">
                      <dx:ASPxButton ID="dxbtnfolders" runat="server" 
                                        TabIndex="3" 
                                        ClientInstanceName="btnfolders" 
                                        ToolTip="Click here to see all folders" 
                                        EnableTheming="False"  
                                        Height="24px" 
                                 Text="All folders" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            UseSubmitBehavior="False" AutoPostBack="False" 
                          CausesValidation="False" onclick="dxbtnfolders_Click" Wrap="False">
                                       <Image Height="16px" Width="16px" AlternateText="Begin search" 
                                           Url="~/Images/icons/16x16/folders_explorer.png">
                                       </Image>
                                      
                              </dx:ASPxButton>
            </div> 
            
            <div style="clear: both">
  
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
         </div>  
         
              <div style="width: 760px; margin-bottom: 10px; margin-top: 15px">
                     <!-- hidden fields -->
                   <dx:ASPxHiddenField ID="dxhfmanager" ClientInstanceName="hfmanager" runat="server">
                       </dx:ASPxHiddenField>
                  <dx:ASPxCallbackPanel ID="dxcbpodfiles" runat="server" 
                      ClientInstanceName="cbpodfiles" Width="760px" 
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
                <div style="clear:both"></div> 
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
                          <!-- uploader -->
           <div style="margin-bottom: 50px">
                
                        <dx:ASPxUploadControl ID="dxuploadpodfiles" runat="server" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" ShowAddRemoveButtons="True" 
                            ShowProgressPanel="True" ShowUploadButton="True" Width="760px" 
                            ClientInstanceName="uploadpodfiles" 
                            onfileuploadcomplete="dxuploadpodfiles_FileUploadComplete" 
                            UploadMode="Advanced">
                                                        <AddButton Image-Height="16px" Image-Url="Images/icons/16x16/page_white_add.png" Image-Width="16px" Text=" Add file">
<Image Height="16px" Width="16px" Url="~/Images/icons/16x16/page_white_add.png"></Image>
                            </AddButton>
                            <RemoveButton Image-Height="16px" Image-Url="Images/icons/16x16/page_white_delete.png" Image-Width="16px" Text=" Remove file">
<Image Height="16px" Width="16px" Url="~/Images/icons/16x16/page_white_delete.png"></Image>
                            </RemoveButton> 
                            <UploadButton  Image-Height="16px" Image-Url="Images/icons/16x16/arrow_up.png" Image-Width="16px" Text=" Upload">
<Image Height="16px" Width="16px" Url="~/Images/icons/16x16/arrow_up.png"></Image>
                            </UploadButton> 
                            <ClientSideEvents FilesUploadComplete="function(s, e) {
	onfilesuploadcomplete(s,e);
}" />
                        </dx:ASPxUploadControl>    
        <!-- end upload control -->
              </div>
         
                
                       </dx:PanelContent>
                    </PanelCollection>
                  </dx:ASPxCallbackPanel>
              </div>
              

        </div>
        <!-- end centered div -->
              
        <div class="bottom-panel"></div>
     
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
        <dx:PopupControlContentControl runat="server">
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
     <dx:ASPxPopupControl ClientInstanceName="popDefault" ID="dxpopDefault" 
        runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
        CssPostfix="Office2010Blue" EnableHotTrack="False" Height="700px" 
        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
        Width="960px" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter">
         <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
         </LoadingPanelImage>
         <ContentCollection>
             <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
             </dx:PopupControlContentControl>
         </ContentCollection>
         <LoadingPanelStyle ImageSpacing="5px">
         </LoadingPanelStyle>
         <Windows>
     <dx:PopupWindow CloseAction="CloseButton" 
                HeaderText="Add an order number"
                Height="700px" Modal="True" Name="filterform" PopupAction="None" 
                Width="960px" PopupElementID="dxbtnmore">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
            
            </Windows> 
    </dx:ASPxPopupControl>
</asp:Content>

