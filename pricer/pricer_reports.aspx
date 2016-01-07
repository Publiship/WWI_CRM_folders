<%@ Page Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="pricer_reports.aspx.cs" Inherits="pricer_reports" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTimer" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
    <script type="text/javascript">

        function onReportClicked(s, e) {
            barReport.SetPosition(0);
            cbreport.PerformCallback();
            tmrprogress.SetEnabled(true);
        }

        function onInitTick(s, e) {
            //carries out same process as button for automatic processing
            alert('1');
            tmrinit.SetEnabled(false);
            barReport.SetPosition(0);
            cbreport.PerformCallback();
            tmrprogress.SetEnabled(true);
            alert('2');
        }
        
        function onTimerTick(s, e) {
            //cbtimer checks progress
            cbtimer.PerformCallback();
        }

        function onCallBackTimerComplete(s, e) {
            if (e.result != null) {
                barReport.SetPosition(e.result);
            }
        }

        function onCallBackReportComplete(s, e) {
            tmrprogress.SetEnabled(false);
        }
    </script>
        
    <div class="innertube">  <!-- just a container div --> 
                
                <!-- centered box as not much on this screen! -->
                <div class="formcenter">
                <div class="info">
                    <dx:ASPxLabel ID="dxlblinfo" ClientInstanceName="lblinfo" runat="server" 
                        Text="Publiship Reporting">
                    </dx:ASPxLabel>
                    <br /><br />
                    <dx:ASPxLabel ID="dxlblinfo2" ClientInstanceName="lblinfo2" runat="server" 
                        Text="Click button to run selected report">
                    </dx:ASPxLabel>
                    </div> 
                <!-- end alert -->
                <div>
                    
                <dx:ASPxPanel ID="dxpnlmsg" ClientInstanceName="pnlmsg" runat="server" ClientVisible="false">
                <PanelCollection>
                    <dx:PanelContent>
                         <dx:ASPxLabel ID="lblmsg" runat="server" 
                                        Text="Error Message" Visible="true"  
                                        ClientInstanceName="dxlblmsg">
                                   </dx:ASPxLabel>
                    </dx:PanelContent> 
                </PanelCollection> 
                </dx:ASPxPanel>   
                
                <!-- called when button is clicked so we can switch timer off once processing is complete -->
                <dx:ASPxCallback ID="dxcbreport" ClientInstanceName="cbreport" runat="server" 
                        oncallback="dxcbreport_Callback">
                 <ClientSideEvents 
                        CallbackComplete="onCallBackReportComplete" />       
                 </dx:ASPxCallback> 
                <!-- progress is updated on client by cbtimer callback -->
                <dx:ASPxProgressBar ID="dxbarReport" ClientInstanceName="barReport" 
                                    runat="server" Height="25px" width="100%" 
                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                        CssPostfix="Office2003Blue">
                </dx:ASPxProgressBar>
                
                </div>
                <table style="margin: 0 Auto; height:150px;width:250px;border:0">
                <tbody>
                <tr>
                    <td style="width: 85px"></td>
                    <td style="width: 90px">
                                <!-- on button click enable timer which runs callback every tick to update progress bar -->
                                <dx:ASPxButton ID="btnReport" ClientInstanceName="dxReport" runat="server" AutoPostBack="false"  
                                  CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" CssPostfix="Office2003Olive" 
                                  SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                                  Text="Run report" Width="80px" Height="9px" CausesValidation="False" 
                                    UseSubmitBehavior="False">
                                  <ClientSideEvents Click="onReportClicked" /> 
                              </dx:ASPxButton></td>
                    <td style="width: 75px"></td>
                </tr>
                </tbody> 
                </table>
    
                 <dx:ASPxCallback ID="dxcbtimer" ClientInstanceName="cbtimer" runat="server" 
                        oncallback="dxcbtimer_Callback">
                    <ClientSideEvents 
                        CallbackComplete="onCallBackTimerComplete" />
                </dx:ASPxCallback>
                <dx:ASPxTimer ID="dxtmrprogress" ClientInstanceName="tmrprogress" runat="server" Interval="1000">
                    <ClientSideEvents 
                        Tick="onTimerTick" />
                </dx:ASPxTimer>
                <dx:ASPxTimer ID="dxtmrinit" ClientInstanceName="tmrinit" runat="server" Interval="500">
                    <ClientSideEvents 
                        Tick="onInitTick" />
                </dx:ASPxTimer> 
         </div> 
        <!-- end form center -->
     </div> 
     <!-- end inner tube -->
</asp:Content> 
