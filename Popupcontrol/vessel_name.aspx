<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vessel_name.aspx.cs" Inherits="Popupcontrol_vessel_name" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

    
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vessel names</title>
     <link rel="stylesheet" href="../App_Themes/Custom.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- make sure the combobox on this form is 'dropdown' not 'dropdownlist' or you will not be able to type in and retain new items -->
    <div>
        <table id="tblVesselName" width="500" align="left" cellpadding="5px">
            <tbody>
                <tr>
                    <td colspan="3">
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
                    </td>
                </tr>
                <tr>
                    <td width="75px"> 
                        &nbsp;</td>
                    
                    <td> 
                        <dx:ASPxLabel ID="dxlblVesselNameCap" runat="server" 
                             ClientInstanceName="dxlblVesselNameCap" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Font-Size="X-Large" 
                                Text="Add vessel name">
                         </dx:ASPxLabel>
                    </td>
                    
                    <td> 
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td>
                          &nbsp;</td>
                    <td>
                        <!-- can't use OnItemsRequestedByFilterCondition and OnItemRequestedByValue on server-side processing makes the search case sensitive -->
                        <dx:ASPxComboBox ID="dxcboVesselID" ClientInstanceName="cboVesselID" 
                              runat="server" Width="250px"
                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                              CssPostfix="Office2010Blue" 
                              SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                              CallbackPageSize="25" EnableCallbackMode="True"  
                              IncrementalFilteringMode="StartsWith" TextField="VesselName" 
                              ValueField="VesselID" ValueType="System.Int32" DropDownStyle="DropDown" 
                                Spacing="0" DataSourceID="linqVessel">
                              <ButtonStyle Width="13px">
                              </ButtonStyle>
                              <LoadingPanelStyle ImageSpacing="5px">
                              </LoadingPanelStyle>
                              <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                              </LoadingPanelImage>
                         </dx:ASPxComboBox>      
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                          &nbsp;</td>
                    <td colspan="2">
                          <dx:ASPxLabel ID="dxlblVesselNameCap1" runat="server" 
                              ClientInstanceName="lblVesselNameCap1" 
                              CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                              CssPostfix="Office2010Blue" 
                              Text="Enter just the name of the ship - NOT the voyage number">
                          </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="dxbtnComplete" runat="server" 
                            ClientInstanceName="bntComplete" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Save &amp; exit" onclick="dxbtnComplete_Click" Width="125px" 
                            Wrap="False" HorizontalAlign="Left">
                            <Image AlternateText="Save &amp; exit" Height="26px" 
                                Url="~/Images/icons/metro/save.png" Width="26px">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="dxbtnAdd" runat="server" ClientInstanceName="btnAdd" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            Text="Add another" onclick="dxbtnAdd_Click" Width="125px" Wrap="False" 
                            HorizontalAlign="Left">
                            <Image Height="26px" Url="~/Images/icons/metro/new.png" Width="23px">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
    <asp:LinqDataSource ID="linqVessel" runat="server" 
        ContextTypeName="linq.linq_vesselDataContext" Select="new (VesselID, VesselName)" 
        TableName="VesselTables">
    </asp:LinqDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </tbody> 
        </table> 
    </div>
    </form>
</body>
</html>
