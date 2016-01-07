<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ord_Filter_Advanced_2.aspx.cs" Inherits="Ord_Filter_Advanced_2" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDataView" tagprefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
    
    <script type="text/javascript">
        function onfieldnamechanged(cboadvfield) {
            //save field and force criteria combo to refresh
            hfparam.Set("fname", cboadvfield.GetValue().toString());
            cbocrits.PerformCallback(cboadvfield.GetValue().toString());
           
        }

        function oncriteriachanged(cbocrits) {
            //force update when user selects a criteria
            hfparam.Set("crits", cbocrits.GetValue().toString());
            cbvalues.PerformCallback(cbocrits.GetValue().toString());
        }

        function critendcallback(cbocrits) {
            //force update when criteria is first refreshed after field has been selected
            hfparam.Set("crits", cbocrits.GetValue().toString());
            cbvalues.PerformCallback(cbocrits.GetValue().toString());
        }

        function updatefilters() {
            cbrepeater.PerformCallback();
        }

        function killfilters() {
            cbrepeater.PerformCallback(1);
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scm_filter" runat="server" />
             <div class="formcentershaded">
             <table>
                <tr><td colspan="3"><h5>Enter your search criteria and click the add button to include in your search</h5></td></tr> 
                <tr>
                    <td style="width: 150px" >
                       <dx:ASPxComboBox ID="dxcboadvfields" runat="server" ClientInstanceName="cboadvfields" 
                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue"  Width="144px"
                            DataSourceID="ObjectDataSourceAdvFields" SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" 
                            ValueType="System.String" 
                            ToolTip="Pick what you want to search for here e.g. ISBN number" 
                            TabIndex="1" TextField="FilterCaption" 
                            ValueField="FieldName">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <LoadingPanelImage Url="../App_Themes/Office2003Blue/Web/Loading.gif">
                                            </LoadingPanelImage>
                                             <ClientSideEvents SelectedIndexChanged="function(s, e) {
  onfieldnamechanged(s);
}" />
                                             <Columns>
                                                    <dx:ListBoxColumn FieldName="FilterCaption" Name="fieldcaption" 
                                                    Caption="Search in" />
                                                    <dx:ListBoxColumn FieldName="FieldName" Name="fieldname" Visible="False" />
                                                    <dx:ListBoxColumn FieldName="ColumnType" Name="fieldtype" Visible="False" />
                                             </Columns>
                       </dx:ASPxComboBox>   
                    </td> <!-- end fields combo -->
                    <td style="width: 150px" >
                        <dx:ASPxComboBox ID="dxcbocrits" runat="server" ClientInstanceName="cbocrits" 
                            oncallback="dxcbocrits_Callback" ValueType="System.String" 
                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" 
                            TextField="CAPTION" ValueField="LINQ" Width="145px" 
                            EnableSynchronization="False">
                            <ButtonStyle Width="13px">
                            </ButtonStyle>
                            <LoadingPanelImage Url="../App_Themes/Office2003Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	oncriteriachanged(s);
}" EndCallback="function(s, e) {
	critendcallback(s);
}" />
                            <Columns>
                                <dx:ListBoxColumn FieldName="CAPTION" Caption="Criteria" />
                                <dx:ListBoxColumn FieldName="LINQ" Visible="False" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td> <!-- end criteria combo -->
                    <td>
                        <!-- panels for text/date values -->
                        <dx:ASPxCallbackPanel ID="dxcbvalues" runat="server" 
                            ClientInstanceName="cbvalues" 
                            oncallback="dxcbvalues_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server" ID="pnltext"> 
                                <table><tr>
                                <!-- 1st text box -->
                                <td>
                                <dx:ASPxPanel ID="dxpanel1" runat="server" Width="130px" 
                                        ClientInstanceName="pnlvalue1">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                     <dx:ASPxTextBox ID="dxtxvalue1" ClientInstanceName="txvalue1" runat="server" 
                                        Width="125px" NullText="Enter a value" 
                                        CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css">
                                            <ValidationSettings Display="Dynamic">
                                            <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' ']{1,100}$" 
                                                ErrorText="Invalid name" />
                                            </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel> 
                                </td><td>
                                <!-- 2nd text box -->
                                <dx:ASPxPanel ID="dxpanel2" runat="server" Width="130px" ClientInstanceName="pnlvalue2">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent2" runat="server">
                                     <dx:ASPxTextBox ID="dxtxvalue2" ClientInstanceName="txvalue2" runat="server" 
                                        Width="125px" NullText="And another value" 
                                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css">
                                            <ValidationSettings Display="Dynamic">
                                            <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' ']{1,100}$" 
                                                ErrorText="Invalid name" />
                                            </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                                </dx:ASPxPanel>
                                </td><td> 
                                <!-- 1st datetime -->
                                <dx:ASPxPanel ID="dxpanel3" runat="server" Width="115px" ClientInstanceName="pnlvalue3">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent3" runat="server">
                                    <dx:ASPxDateEdit ID="dxdtvalue1" ClientInstanceName="dtvalue1" runat="server" 
                                        NullText="Enter a date" 
                                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" 
                                            Width="112px" DisplayFormatString="{0:d}">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                                </dx:ASPxPanel>
                                </td><td> 
                                <!-- 2nd datetime -->
                                <dx:ASPxPanel ID="dxpanel4" runat="server" Width="130px" ClientInstanceName="pnlvalue4">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent4" runat="server">
                                    <dx:ASPxDateEdit ID="dxdtvalue2" ClientInstanceName="dtvalue2" runat="server" 
                                        NullText="And another date" Width="122px" 
                                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" 
                                            DisplayFormatString="{0:d}">
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                                </dx:ASPxPanel>
                                </td><td> 
                                <!-- yes/no -->
                                <dx:ASPxPanel ID="dxpanel5" runat="server" Width="90px" ClientInstanceName="pnlvalue5">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent5" runat="server">
                                       <dx:ASPxComboBox ID="dxcboyesno" ClientInstanceName="cboyesno" runat="server" 
                                            Width="80px" CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css">
                                           <LoadingPanelImage Url="../App_Themes/Office2003Blue/Web/Loading.gif">
                                           </LoadingPanelImage>
                                           <ButtonStyle Width="13px">
                                           </ButtonStyle>
                                    </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                                </dx:ASPxPanel>
                                </td>
                                <td>
                                  <!-- next/previous 7, 14, N days -->
                                 <dx:ASPxPanel ID="dxpanel6" runat="server" Width="100px" ClientInstanceName="pnlvalue6">
                                    <PanelCollection>
                                    <dx:PanelContent ID="PanelContent6" runat="server">
                                       <div style="float: left; width: 70px">
                                        <dx:ASPxSpinEdit ID="dxspindays" ClientInstanceName="spindays" runat="server" Number="7" Increment="7" NumberType="Integer"
                                            MinValue="1" MaxValue="371" NullText="Type number" 
                                               CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                               CssPostfix="Office2010Blue" Spacing="0" 
                                               SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" Width="60px">
                                            <SpinButtons HorizontalSpacing="0">
                                            </SpinButtons>
                                        </dx:ASPxSpinEdit>
                                        </div>
                                        <div style="float: left">
                                            <dx:ASPxLabel ID="dxlbldays" ClientInstanceName="lbldays" runat="server" 
                                                Text=" days" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                CssPostfix="Office2003Blue">
                                            </dx:ASPxLabel>
                                        </div>
                                    </dx:PanelContent> 
                                </PanelCollection>
                                </dx:ASPxPanel>
                                </td>
                                <!-- end panels -->
                                <td> 
                                   <dx:ASPxButton ID="dxbtnadd" ClientInstanceName="btnadd" runat="server" 
                                            Image-Height="16px" Image-Width="16px" Height="16px" 
                                            HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" EnableDefaultAppearance="False"
                                            CausesValidation="False" 
                                            Width="16px" AutoPostBack="False" Cursor="pointer">
                                        <ClientSideEvents Click="function(s, e) {updatefilters();}" />
                                        <Image Url="../Images/add-icon.png"></Image>
                                         <Paddings Padding="1px" />
                                    <Border BorderStyle="None" /> 
                                        </dx:ASPxButton>
                                </td> 
                                </tr> 
                                </table> 
                                </dx:PanelContent>
                            </PanelCollection>
                    </dx:ASPxCallbackPanel>
                    <!-- end panels -->
                    </td>
                 </tr> 
             </table>
             </div>
          
             
             <div class="formcenter">
               
                        <dx:ASPxCallbackPanel ID="dxcbrepeater" ClientInstanceName="cbrepeater" 
                            runat="server" oncallback="dxcbrepeater_Callback" 
                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue">
                            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                            </LoadingPanelImage>
                        <PanelCollection>
                        <dx:PanelContent runat="server" ID="pnlrepeater">
                        <asp:Repeater ID="rptfilter" runat="server" 
                                OnItemCommand="rptfilter_ItemCommand"> 
                            <ItemTemplate>
                                <table> <!-- forces data into rows -->
                                <tr>
                                <td style="width: 200px">
                                <dx:ASPxLabel ID="dxlblfield" ForeColor="#000066"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"fieldcaption").ToString() %>'>
                                </dx:ASPxLabel>
                                </td> 
                                <td style="width: 200px">
                                <dx:ASPxLabel ID="dxlblcriteria" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"criteriacaption").ToString() %>' ForeColor="#336600">
                                </dx:ASPxLabel>
                                </td> 
                                <td style="width: 300px">
                                <dx:ASPxLabel ID="dxlblvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"values").ToString() %>'>
                                </dx:ASPxLabel>
                                </td> 
                                <td style="width: 15px">
                                <dx:ASPxLabel ID="dxtxtfilter" ClientVisible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"criteria").ToString() %>'>
                                </dx:ASPxLabel>
                                </td> 
                                <td>
                                <dx:ASPxButton ID="dxbtnremove" runat="server" BackColor="White" Image-Height="16px" Image-Width="16px" AutoPostBack="true"  Cursor="pointer"  
                                         Height="15px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" EnableDefaultAppearance="False" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"criteria").ToString() %>'>
                                    <Image Url="../Images/delete-icon.png">
                                    </Image>
                                    <Paddings Padding="3px" />
                                    <Border BorderStyle="None" />
                                </dx:ASPxButton>   
                                </td>
                                </tr>
                                </table>
                            </ItemTemplate> 
                        </asp:Repeater>
                        
                        
                        </dx:PanelContent> 
                        </PanelCollection> 
                        </dx:ASPxCallbackPanel>
                   
        </div>
   


        <div class="formcenter580"> 
             <div class="cell580_40">
                    <dx:ASPxLabel ID="aspxlblName" runat="server" Text="Name this search for future reference">
                    </dx:ASPxLabel> 
                    </div>
             <div class="cell580_40">
                            <dx:ASPxTextBox ID="aspxtxtName" ClientInstanceName="txtName" runat="server" 
                        Width="160px" NullText="Type a name here">
                                <ValidationSettings Display="Dynamic">
                                    <RegularExpression ValidationExpression="^[\d_0-9a-zA-Z' ']{1,50}$" 
                                        ErrorText="Invalid name" />
                                </ValidationSettings>
                    </dx:ASPxTextBox>
                    </div>
             <div class="cell580_20">
                 <dx:ASPxCheckBox ID="dxckReport" runat="server" 
                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                     CssPostfix="Office2003Blue" 
                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                     Text="Add to reports" ClientEnabled="False" ClientVisible="False" 
                     Visible="False">
                 </dx:ASPxCheckBox>
             </div>
             <div class="cell580_20">
                          <dx:ASPxButton ID="btnCancel" runat="server" 
                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" Text="Cancel" 
                            Width="100px" CausesValidation="False" 
                            AutoPostBack="False">
                            <ClientSideEvents 
                                Click="function(s, e) {
//cbcancel.PerformCallback();	
window.parent.popDefault.HideWindow(window.parent.popDefault.GetWindowByName('filterform'));}" />
                         </dx:ASPxButton>
                          </div>
             <div class="cell580_20">
                          <dx:ASPxButton ID="btnReset" runat="server" 
                            CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                            SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" Text="Reset all" 
                            Width="100px" CausesValidation="False" 
                            AutoPostBack="False">
                            <ClientSideEvents 
                                Click="function(s, e) {
killfilters();
}" />
                         </dx:ASPxButton>
                          </div>
             <div class="cell580_20">

                                     <dx:ASPxTextBox ID="txtQueryResult" runat="server" 
                        ClientInstanceName="txtQuery" ReadOnly="True" Width="100px" 
                        ClientVisible="False">
                    </dx:ASPxTextBox>
     
             </div>
             <div class="cell580_20">
                          </div>
             <div class="cell580_20">
                         <dx:ASPxButton ID="btnApply" runat="server" Text="Apply"
                        CssFilePath="../App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue" 
                        SpriteCssFilePath="../App_Themes/Office2003Blue/{0}/sprite.css" 
                        UseSubmitBehavior="False" onclick="btnApply_Click">
                        </dx:ASPxButton>
                    </div>
                    <div class="formfooter"></div>
        </div>
        
        <div>
                          <dx:ASPxCallback ID="dxcbcancel" runat="server" ClientInstanceName="cbcancel" 
                              oncallback="dxcbcancel_Callback">
                          </dx:ASPxCallback>
                          <dx:ASPxHiddenField ID="dxhfparam" runat="server" ClientInstanceName="hfparam">
                                </dx:ASPxHiddenField>
                  <asp:ObjectDataSource ID="ObjectDataSourceAdvFields" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="FetchByAdvanced" 
                                TypeName="DAL.Logistics.dbcustomfilterfieldcontroller">
                   </asp:ObjectDataSource>

     
     
        </div>
    </form>
</body>
</html>
