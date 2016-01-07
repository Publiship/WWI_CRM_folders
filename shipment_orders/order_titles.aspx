<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="order_titles.aspx.cs" Inherits="order_titles" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    
    <script type="text/javascript">
        // <![CDATA[
        function TextBoxKeyUp(s, e) {
            if (editorsValues[s.name] != s.GetValue())
                StartEdit();
        }
        
        function EditorValueChanged(s, e) {
            StartEdit();
        }

        function OnMenuItemClick(s, e) {
            switch (e.item.name) {
                case "jsUpdate": grid.UpdateEdit(); break;
                case "jsEdit":
                    StartEdit();
                    break;
                case "jsNew":
                    gridTitles.AddNewRow(); break;
                //grid.AddNewRow(); break;    
                case "jsDelete":
                    if (confirm('Are you sure to delete this record?'))
                        grid.DeleteRow(grid.GetTopVisibleIndex());
                    break;
                case "jsRefresh": 
                    break;
                case "jsCancel": grid.Refresh();
                    break;
            }
        }

        //generic function on combo button clicks
        function onComboButtonClick(s, e) {
            if (e.buttonIndex == 0) {
                //clear text 
                s.SetText('');
                s.SetSelectedIndex(-1);
            }
        }
        //********************
        // ]]>
    </script>    

    <div class="container_16">
        <!-- messages -->
        <div class="grid_16 pad_bottom">
           <dx:ASPxPanel ID="dxpnlErr" ClientInstanceName="pnlErr" ClientVisible ="false" 
                    runat="server">
                <PanelCollection>
                    <dx:PanelContent ID="pnlErr">
                        <div class="alert"> 
                            <dx:ASPxLabel ID="dxlblErr" ClientInstanceName="lblErr" runat="server" Text="" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                            </dx:ASPxLabel> 
                        </div>
                    </dx:PanelContent> 
                </PanelCollection> 
            </dx:ASPxPanel>
            <dx:ASPxPanel ID="dxpnlMsg" ClientInstanceName="pnlMsg" ClientVisible ="false" 
                    runat="server">
                <PanelCollection>
                 <dx:PanelContent ID="pnlMsg">
                        <div class="info"> 
                            <dx:ASPxLabel ID="dxlblInfo" ClientInstanceName="lblInfo" runat="server" 
                                Text="" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                            </dx:ASPxLabel> 
                        </div>
                    </dx:PanelContent> 
                </PanelCollection> 
            </dx:ASPxPanel> 
        </div> 
        <div class="clear"></div>
       <div class="grid_10">
            <div class="divleft">
                   <dx:ASPxHyperLink ID="dxlnkReturn" runat="server" 
                      ClientInstanceName="lnkReturn" EnableViewState="False" Height="26px" 
                      ImageHeight="26px" ImageUrl="~/Images/icons/metro/left_round.png" 
                      ImageWidth="26px" NavigateUrl="~/shipment_orders/order_search.aspx" 
                      Target="_self" Text="Back to search form" 
                      ToolTip="Click to return to search page" Width="26px" />
            </div>
           <div class="divleft">
                <dx:ASPxLabel ID="dxlblOrderDetails" runat="server" 
                             ClientInstanceName="lblOrderDetails" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" Text="Order No|">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlblOrderNo" runat="server" ClientInstanceName="lblOrderNo" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large">
                         </dx:ASPxLabel>
            </div> 
            <div class="divleft">
            <dx:ASPxLabel ID="dxlbOrderDetails1" runat="server" 
                            ClientInstanceName="lblOrderDetails1" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="X-Large" 
                    Text="| Titles and copies">
                        </dx:ASPxLabel>
            </div>
        </div>
        <!-- images and text -->
        <div class="grid_6">
            <div class="divright">
              <dx:ASPxImage ID="dximgJobPubliship" runat="server" 
                            AlternateText="Publiship Job" ClientInstanceName="imgJobPubliship" 
                            Height="26px" ImageAlign="Top" ImageUrl="~/Images/icons/metro/checked_checkbox.png" 
                            Width="26px" ClientVisible="False">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobPubliship" runat="server" 
                            ClientInstanceName="lblJobPubliship" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Font-Size="12pt" Text="Publiship job" 
                            ClientVisible="False">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
              <dx:ASPxImage ID="dximgJobHot" runat="server" AlternateText="Hot Job" 
                            ClientInstanceName="imgJobHot" Height="26px" 
                            ImageUrl="~/Images/icons/metro/matches.png" Width="26px" 
                            ImageAlign="Top" ClientVisible="False">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobHot" runat="server" ClientInstanceName="lblJobHot" 
                            ClientVisible="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="Hot job" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div>
            <div class="divright">
                         <dx:ASPxImage ID="dximgJobClosed" runat="server" AlternateText="Job Closed" 
                            ClientInstanceName="imgJobClosed" Height="26px" 
                            ImageUrl="~/Images/icons/metro/lock.png" Width="26px" 
                            ImageAlign="Top">
                        </dx:ASPxImage>
                        <dx:ASPxLabel ID="dxlblJobClosed" runat="server" 
                            ClientInstanceName="lblJobClosed" ClientVisible="False" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Text="closed" Font-Size="12pt">
                        </dx:ASPxLabel>
            </div> 
         </div>              
        <div class="clear"></div>
        <!-- tabs -->
        <div class="grid_16 pad_bottom">
         <dx:ASPxTabControl ID="dxtabOrder" runat="server" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                TabSpacing="0px" ondatabound="dxtabOrder_DataBound">
                            <ContentStyle>
                                <Border BorderColor="#859EBF" BorderStyle="Solid" BorderWidth="1px" />
                            </ContentStyle>
                            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                        </dx:ASPxTabControl>
        </div> 
        <div class="clear"></div>
            <!-- data grid make sure the titles combobox is dropdownstyle=dropdown or you won't be able to insert new values -->
            <div class="grid_16">
                 <dx:ASPxGridView ID="dxgridTitles" runat="server" AutoGenerateColumns="False" 
                            ClientInstanceName="gridTitles" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            KeyFieldName="TitleID" width="100%" 
                     onrowinserting="dxgridTitles_RowInserting" 
                     onrowupdating="dxgridTitles_RowUpdating" 
                     onafterperformcallback="dxgridTitles_AfterPerformCallback" 
                     onstartrowediting="dxgridTitles_StartRowEditing" 
                     onrowdeleting="dxgridTitles_RowDeleting">
                            <SettingsBehavior EnableRowHotTrack="True" ConfirmDelete="True" />
                            <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue">
                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                </Header>
                                <LoadingPanel ImageSpacing="5px">
                                </LoadingPanel>
                            </Styles>
                            <SettingsPager NumericButtonCount="25" PageSize="25">
                            </SettingsPager>
                            <ImagesFilterControl>
                                <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                </LoadingPanel>
                            </ImagesFilterControl>
                            <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                </LoadingPanelOnStatusBar>
                                <LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
                                </LoadingPanel>
                            </Images>
                            <SettingsEditing Mode="Inline" />
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Name="colCommand" 
                                    ButtonType="Image">
                                    <EditButton Visible="True">
                                          <Image AlternateText="Edit" ToolTip="Edit" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/edit18.png" Width="22px">
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                          <Image AlternateText="New" ToolTip="New" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/add_row18.png" Width="22px">
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                          <Image AlternateText="Delete" ToolTip="Delete" Height="18px" 
                                            Url="~/Images/icons/metro/22x18/delete_row18.png" Width="22px">
                                        </Image>
                                    </DeleteButton>
                                    <UpdateButton Visible="True">
                                          <Image AlternateText="Update" ToolTip="Update" Height="18px" 
                                                Url="~/Images/icons/metro/22x18/save18.png" Width="22px">
                                            </Image>
                                    </UpdateButton> 
                                     <CancelButton Visible="True">
                                          <Image AlternateText="Cancel" ToolTip="Cancel" Height="18px" 
                                                Url="~/Images/icons/metro/22x18/cancel18.png" Width="22px">
                                            </Image>
                                    </CancelButton> 
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="TitleID" ReadOnly="True" Visible="False" 
                                    VisibleIndex="1" Width="0px" Caption="ID" Name="colID">
                                    <Settings AllowAutoFilter="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OrderNumber" VisibleIndex="26" 
                                    Caption="Order no." Visible="False" Width="0px" Name="colOrderNo">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Title" VisibleIndex="2" Width="225px" 
                                    Caption="Title" Name="colTitle" >
                                <PropertiesComboBox Spacing="0"></PropertiesComboBox>
                                     <EditItemTemplate>
                                        <dx:ASPxComboBox ID="dxcbotitle" runat="server" Width="225px" DropDownStyle="DropDown" 
                                            OnItemRequestedByValue="dxcbotitle_ItemRequestedByValue" 
                                            OnItemsRequestedByFilterCondition="dxcbotitle_ItemsRequestedByFilterCondition" 
                                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                            CssPostfix="Office2010Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                                            CallbackPageSize="30" EnableCallbackMode="True"  
                                            IncrementalFilteringMode="StartsWith" TextField="Title"  
                                            ValueField="TitleID" ValueType="System.Int32" 
                                            ondatabound="dxcbotitle_DataBound" 
                                            Spacing="0">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ClientSideEvents ButtonClick="onComboButtonClick" />
                                            <Buttons>
                                            </Buttons> 
                                            <LoadingPanelStyle ImageSpacing="5px">
                                            </LoadingPanelStyle>
                                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                                            </LoadingPanelImage>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="ISBN" VisibleIndex="4" Width="150px" 
                                    Caption="ISBN" Name="colISBN">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SSRNo" VisibleIndex="9" Caption="SSR no." 
                                    Width="100px" Name="colSSRNo">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="SSRDate" VisibleIndex="10" 
                                    Caption="SSR date" Width="110px" Name="colSSRDate">
                                    <PropertiesDateEdit Spacing="0">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="Impression" VisibleIndex="5" 
                                    Width="125px" Caption="Impression" Name="colImpression">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PONumber" VisibleIndex="6" 
                                    Caption="PO Number" Width="125px" Name="colPONumber">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OtherRef" VisibleIndex="7" 
                                    Caption="Other ref" Width="125px" Name="colOtherRef">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Copies" VisibleIndex="3" Width="75px" 
                                    Caption="Copies" Name="colCopies">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PerCopy" VisibleIndex="11" 
                                    Caption="Per copy" Width="100px" Name="colPerCopy">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TotalValue" VisibleIndex="8" 
                                    Caption="Total value" Width="100px" Name="colTotalValue">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <StylesPager>
                                <PageNumber ForeColor="#3E4846">
                                </PageNumber>
                                <Summary ForeColor="#1E395B">
                                </Summary>
                            </StylesPager>
                            <Settings ShowFilterRow="True" ShowHorizontalScrollBar="True" ShowHeaderFilterButton="True" 
                                VerticalScrollableHeight="350" />
                            <StylesEditors ButtonEditCellSpacing="0">
                                <ProgressBar Height="21px">
                                </ProgressBar>
                            </StylesEditors>
                        </dx:ASPxGridView>
            </div> 
             <div class="clear"></div>
            <!-- commands for title grid -->
            <div class="grid_16 pad_bottom">
            <dx:ASPxMenu ID="dxmnuCommand" runat="server" 
                ClientInstanceName="mnuCommand" EnableClientSideAPI="True" 
                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" ShowPopOutImages="True" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                AutoSeparators="RootOnly" ItemAutoWidth="False" Width="100%" 
                    onitemdatabound="dxmnuCommand_ItemDataBound">
                            <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px" />
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                            <SubMenuStyle GutterImageSpacing="9px" GutterWidth="13px" />
                             <ClientSideEvents ItemClick="OnMenuItemClick" />
                        </dx:ASPxMenu>
        </div>   
       
            <!-- custom command butons for formview -->
        <!-- <ClientSideEvents ItemClick="OnMenuItemClick" /> no point in client side as we need to call back to server anyway to process data -->
        <div class="grid_16">
          <!-- popup to create a new title -->
             <dx:ASPxPopupControl ID="dxpcPodEdit" ClientInstanceName="pcPodEdit" 
                runat="server" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                CssPostfix="Office2010Blue" EnableHotTrack="False" 
                SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                PopupAction="None" PopupHorizontalAlign="WindowCenter" 
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
                        ContentUrl="~/Popupcontrol/Pod_Title.aspx" HeaderText="New title"
                        Height="820px" Modal="True" Name="InsertTitle" PopupAction="None" 
                        Width="1000px" PopupElementID="dxbtnmore">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                
                </Windows>
            </dx:ASPxPopupControl>
        </div> 
        <div class="clear"></div>
        <div class="grid_16">  
            <dx:ASPxHiddenField ID="dxhfOrder" runat="server" ClientInstanceName="hfOrder">
            </dx:ASPxHiddenField>
         </div>
    </div><!-- end container -->
        
  
    
</asp:Content>


