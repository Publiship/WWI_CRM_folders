<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_and_quote.aspx.cs" Inherits="register_and_quote" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPager" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Ver sion=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register with Publiship</title>
    <link href="http://www.publiship-online.dtemp.net/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="../App_Themes/custom.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/dropdown_one.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/menus.css" type="text/css" />
    <link rel="stylesheet" href="../App_Themes/typography.css" type="text/css" />
    
    <script type="text/javascript">
        function onGroupValidation(s, e) {
            var v = e.value;
            alert(v);
            if (v == null || v == "") {
                e.isValid = false;
                btnquote0.SetEnabled(false);
            }
            else {
                btnquote0.SetEnabled(true);
            }
        }

        function noenter(s, e) {
            return !(window.event && window.event.keyCode == 13);
        }

        function ondimensionschanged(rbl) {
            //save value to hidden field
            hfpricer.Set("dims", rbl.GetValue().toString());
            //alert(hfpricer.Get("dims"));
        }

        function oncurrencychanged(cbo) {
            //save value to hidden field use text not value
            hfpricer.Set("crnc", cbo.GetText().toString());
            //alert(hfpricer.Get("crnc"));
        }

        function onpalletchanged(cbo) {
            //save value to hidden field
            hfpricer.Set("pall", cbo.GetValue().toString());
            //alert(hfpricer.Get("pall"));
        }

        function ongetquote(s, e) {
            if (!cbquote.InCallback()) {
                cbquote.PerformCallback();
            }
        }

        function onbuttonPrice(s, e) {
            var pricetab = hfpricer.Get("tabb");
            pagepricer.SetActiveTab(pagepricer.GetTab(pricetab));
            rndbuttons(pricetab);
            rndinputpanels();
        }

        function onbuttonEmail(s, e) {
            pagepricer.SetActiveTab(pagepricer.GetTab(7));
            rndbuttons(7);
        }

        function onbuttonPrevious(s, e) {

            //user = verify_user();
            //if (user != 'You are not logged in') {
            //var indexTab = (pagepricer.GetActiveTab()).index;
            //pagepricer.SetActiveTab(pagepricer.GetTab(indexTab - 1));
            //rndheadertext(indexTab - 1);
            var indexTab = (pagepricer.GetActiveTab()).index;
            if (indexTab == 1) {
                pagepricer.SetActiveTab(pagepricer.GetTab(0));
                rndbuttons(0);
            }
            else {
                pagepricer.SetActiveTab(pagepricer.GetTab(1));
                rndbuttons(1);
                rndinputpanels();
            }
            //}
            //else {
            //    var window = popWindows.GetWindowByName('loginform');
            //    popWindows.ShowWindow(window); 
            //}
        }

        function onbuttonNext(s, e) {
            if (validatePage() == true) {
                //user = verify_user();
                //if (user != 'You are not logged in') {
                var indexTab = (pagepricer.GetActiveTab()).index;
                pagepricer.SetActiveTab(pagepricer.GetTab(indexTab + 1));
                rndbuttons(indexTab + 1); //sets visible buttons
                if (indexTab + 1 == 2 || indexTab + 1 == 6) { rndinputpanels(); }
                //}
                //else {
                //    var window = popWindows.GetWindowByName('loginform');
                //    popWindows.ShowWindow(window);
                //}
            }
        }

        function rndinputpanels() {
            var intype = hfpricer.Get("dims").toString();

            switch (intype) {
                case "1":
                    panelbook.SetVisible(true);
                    panelcarton.SetVisible(false);
                    panelpaper.SetVisible(false);
                    break;
                case "2":
                    panelbook.SetVisible(false);
                    panelcarton.SetVisible(true);
                    panelpaper.SetVisible(false);
                    break;
                case "3":
                    panelbook.SetVisible(false);
                    panelcarton.SetVisible(false);
                    panelpaper.SetVisible(true);
                    break;
                default:
                    alert('error!');
                    break;
            }
        }

        function rndbuttons(indextab) {

            switch (indextab) {
                case 0:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(false);
                    btnext0.SetVisible(true);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Choose dimensions or paper type</div>');
                    break;
                case 1:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(true );
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 2:
                    btnquote0.SetVisible(true);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 3:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 4:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 5:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 6:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 7:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                default:
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Choose dimensions or paper type</div>');
                    break;
            }
        }

        function onOriginChanged(cborigin) {
            //cbcountry.PerformCallback(cborigin.GetValue().toString());
            cbcountry.SetSelectedIndex(-1);
            cbfinal.SetSelectedIndex(-1);
            cbkgroup.PerformCallback(0);
        }

        function onCountryChanged(cbcountry) {
            //cbfinal.PerformCallback(cbcountry.GetValue().toString());
            cbfinal.SetSelectedIndex(-1);
            cbkgroup.PerformCallback(1);
        }

        function validatePage() {
            var isValid = false;
            var indexTab = (pagepricer.GetActiveTab()).index;
            var editorsContainerId = "ContentControl" + indexTab;

            if (ASPxClientEdit.ValidateEditorsInContainerById(editorsContainerId)) {
                isValid = true;
            }
            return isValid;
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
     <div class="formcenter580">
  
    <dx:ASPxPageControl ID="dxpagepricer" ClientInstanceName="pagepricer" 
        runat="server" ActiveTabIndex="0" ShowTabs="False" Width="500px" 
             Border-BorderStyle="None"  >
<Border BorderStyle="None"></Border>
        <TabPages>
            <dx:TabPage Text="-1. Registration details">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl0">
                       <div style="width: 475px; margin: 0 Auto;"> 
                                <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxstep0h0"  TabIndex="-1" runat="server" ClientInstanceName="step0h0" 
                                        Text="Step 1 of 3: Your contact details" /></div>
                                </div> 
                        <!-- end header -->
                        <dl class="dl4">
                            <dt><dx:ASPxLabel ID="dxreg1" ClientInstanceName="reg1" runat="server" Text="First name">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtname1" ClientInstanceName="txtname1" runat="server" 
                                    Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxTextBox>
                            </dd> 
                            <dt><dx:ASPxLabel ID="dxreg2" ClientInstanceName="reg2" runat="server" Text="Surname">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtname2" ClientInstanceName="txtname2" runat="server" 
                                    Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxTextBox>
                            </dd>
                            <dt><dx:ASPxLabel ID="dxreg3" ClientInstanceName="reg3" runat="server" Text="Company">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtcompany" ClientInstanceName="txtcompany" 
                                    runat="server" Width="170px" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                          <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value"  />
                                            <RequiredField ErrorText="Required" IsRequired="true" /> 
                                     </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <dt><dx:ASPxLabel ID="dxreg4" ClientInstanceName="reg4" runat="server" Text="Phone number">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtphone" ClientInstanceName="txtphone" runat="server" 
                                    Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxTextBox>
                            </dd>
                            <dt><dx:ASPxLabel ID="dxreg5" ClientInstanceName="reg5" runat="server" Text="Email address">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtemail" ClientInstanceName="txtemail" runat="server" 
                                    Width="250px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxTextBox>
                            </dd>
                              <dt><dx:ASPxLabel ID="dexreg9" ClientInstanceName="reg9" runat="server" Text="Country">
                                </dx:ASPxLabel></dt>
                                <dd><dx:ASPxComboBox ID="dxcboregcountry" ClientInstanceName="cboregcountry" runat="server" 
                                    ValueType="System.String" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    IncrementalFilteringMode="StartsWith" TextField="name" ValueField="name">
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ValidationSettings>
                                        <RequiredField ErrorText="Required" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox></dd>
                            <dt><dx:ASPxLabel ID="dxreg6" ClientInstanceName="reg6" runat="server" Text="Additional information">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxMemo ID="dxmemoadd" ClientInstanceName="memoadd" runat="server" 
                                    Height="71px" Width="250px" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxMemo>
                            </dd>
                            <dt><dx:ASPxLabel ID="dxreg7" ClientInstanceName="reg7" runat="server" Text="Include me in mailing list">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxCheckBox ID="dxckmailing" ClientInstanceName="ckmailing" Checked="true" 
                                    runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                </dx:ASPxCheckBox>
                            </dd>
                            <dt><dx:ASPxLabel ID="dxreg8" ClientInstanceName="reg8" runat="server" Text="Where did you hear about us?">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxComboBox ID="dxcbowhere" ClientInstanceName="cbowhere" runat="server" 
                                    ValueType="System.String" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <Items>
                                        <dx:ListEditItem Text="First shipment invite" Value="First shipment" />
                                        <dx:ListEditItem Text="Web search" Value="Web search" />
                                        <dx:ListEditItem Text="Other" Value="Other" />
                                    </Items>
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ButtonStyle Width="11px">
                                    </ButtonStyle>
                                </dx:ASPxComboBox>
                            </dd>
                            </dl>
                           
                           </div> 
                    </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="0. Dimensions or paper type">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl1" runat="server">
                            <div style="width: 475px; margin: 0 Auto;"> 
                                <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblstep1h1"  
                                            TabIndex="-1" runat="server" ClientInstanceName="step1h1" 
                                        Text="Step 2 of 3: Input type and currency" /></div>
                                </div>
                           <!-- end header --> 
                            <dl class="dl2">
                                    <dt>
                                         <dx:ASPxLabel ID="dxlblinput" runat="server" ClientInstanceName="lblinput" TabIndex="-1"
                                            Text="Choose the type of input data" 
                                            ToolTip="Type of input data">
                                        </dx:ASPxLabel>
                                    </dt>
                                    <dd>
                                    <dx:ASPxRadioButtonList ID="dxrblintpe" runat="server" TabIndex="1"
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            ClientInstanceName="rblintype" Width="300px" OnInit="dxrblintpe_Init" 
                                            TextField="name" ValueField="value">
                                             <ClientSideEvents ValueChanged="function(s, e) { ondimensionschanged(s); }" />
                                        </dx:ASPxRadioButtonList>
                                    </dd>  
                                   <dt>
                                    <dx:ASPxLabel ID="dxcurrency" runat="server" TabIndex="-1"
                                    ClientInstanceName="lblcurrency" Text="Currency">
                                </dx:ASPxLabel>
                                    </dt>    
                                    <dd>
                                    <dx:ASPxComboBox ID="dxcbocurrency" runat="server"  TabIndex="1"
                                    ClientInstanceName="cbocurrency" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    ValueType="System.String" TextField="name" ValueField="value">
                                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                            oncurrencychanged(s);
                                            }" />
                                    </dx:ASPxComboBox>
                                </dd> 
                                  <dt>
                                    <dx:ASPxLabel ID="dxlbltypepallet" runat="server" TabIndex="-1"
                                    ClientInstanceName="lbltypepallet" Text="Pallet Type">
                                </dx:ASPxLabel>
                                    </dt>    
                                    <dd>
                                    <dx:ASPxComboBox ID="dxcbotypepallet" runat="server"  TabIndex="1"
                                    ClientInstanceName="cbotypepallet" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    ValueType="System.String" TextField="name" ValueField="value">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                            onpalletchanged(s);
                                            }" />
                                    </dx:ASPxComboBox>
                                </dd> 
                            </dl>
                            <!-- end input  form -->
                        </div>
                        <!-- end input data type -->  
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="1. Book details">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl2" runat="server">
                       <div style="width: 450px; margin: 0 Auto;">
                           <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblstep2h1" 
                                            runat="server" TabIndex="-1" ClientInstanceName="step2h1"  Width="475px"
                                        Text="Step 3 of 3: Input dimensions and copies" /></div>
                            </div>
                           <!-- end header --> 
                           <dx:ASPxPanel ID="dxpanelbook" ClientInstanceName="panelbook" TabIndex="-1" runat="server" Width="450px">
                           <PanelCollection>
                           <dx:PanelContent> 
                            <dl class="dl2">
                             <dt>
                                <dx:ASPxLabel ID="dxtitle" runat="server" 
                                    ClientInstanceName="lbltitle" Text="Title or description" TabIndex="-1">
                                </dx:ASPxLabel>
                                 </dt>
                            <dd>
                               <dx:ASPxTextBox ID="dxtxttitle" runat="server" 
                                    ClientInstanceName="txttitle" Width="200px" TabIndex="2"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css"
                                    NullText="optional">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value" />
                                    </ValidationSettings>
                               </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated pallets -->
                            <dt>
                                <dx:ASPxLabel ID="dxlength" runat="server" TabIndex="-1" ClientInstanceName="lbllength" 
                                    Text="Length (mm)">
                                </dx:ASPxLabel>
                                </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtlength" runat="server" 
                                    ClientInstanceName="txtlength" Width="130px"  TabIndex="3"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated width -->
                            <dt>
                                <dx:ASPxLabel ID="dxwidth" runat="server" Text="Width (mm)" TabIndex="-1"
                                    ClientInstanceName="lblwidth">
                                </dx:ASPxLabel></dt> 
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtwidth" runat="server" ClientInstanceName="txtwidth" 
                                    Width="130px"  TabIndex="4" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated volume -->
                            <dt>
                                <dx:ASPxLabel ID="dxlbldepth" runat="server" TabIndex="-1" ClientInstanceName="lbldepth" 
                                    Text="Depth (mm)">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtdepth" runat="server" ClientInstanceName="txtdepth" 
                                    Width="130px"  TabIndex="5" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            <!-- weight -->
                            <dt>
                                <dx:ASPxLabel ID="dxweight" runat="server" TabIndex="-1" ClientInstanceName="lblweight" 
                                    Text="Weight (grams)">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtweight" runat="server" ClientInstanceName="txtweight" 
                                    Width="130px"  TabIndex="6" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            <!--  copies per carton -->
                            <dt>
                                <dx:ASPxLabel ID="dxcarton" runat="server" TabIndex="-1" ClientInstanceName="lblcarton" 
                                    Text="Copies per carton"></dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtcarton" runat="server" ClientInstanceName="txtcarton" 
                                    Width="130px"  TabIndex="7" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    NullText="optional">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Invalid value" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                &nbsp;</dd>  
                            </dl>
                            </dx:PanelContent>
                            </PanelCollection>
                            </dx:ASPxPanel>
                            <!-- end book panel -->
                          <dx:ASPxPanel ID="dxpanelcarton" ClientInstanceName="panelcarton" runat="server" Width="450px">
                           <PanelCollection>
                           <dx:PanelContent> 
                            <dl class="dl2">
                             <dt>
                                <dx:ASPxLabel ID="dxlblcarttitle" runat="server" TabIndex="-1"
                                    ClientInstanceName="lblcarttitle" Text="Title or description">
                                </dx:ASPxLabel>
                                 </dt>
                            <dd>
                               <dx:ASPxTextBox ID="dxtxtcarttitle" runat="server" 
                                    ClientInstanceName="txtcarttitle" Width="200px"  TabIndex="8"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    NullText="optional">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated pallets -->
                            <dt>
                                 <dx:ASPxLabel ID="dxlblside1" runat="server" TabIndex="-1" ClientInstanceName="lblside1" 
                                    Text="Longest side (mm)">
                                </dx:ASPxLabel></dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtside1" runat="server" 
                                    ClientInstanceName="txtside1" Width="130px"  TabIndex="9"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated width -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblside2" runat="server" TabIndex="-1" Text="Next side (mm)" 
                                    ClientInstanceName="lblside2">
                                </dx:ASPxLabel>
                            </dt> 
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtside2" runat="server" ClientInstanceName="txtside2" 
                                    Width="130px"  TabIndex="10" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated volume -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblcartdepth" runat="server" TabIndex="-1" ClientInstanceName="lblcartdepth" 
                                    Text="Depth (mm)">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtcartdepth" runat="server" ClientInstanceName="txtcartdepth" 
                                    Width="130px"  TabIndex="11" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            <!-- weight -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblcartweight" runat="server" TabIndex="-1" ClientInstanceName="lblcartweight" 
                                    Text="Weight (kg)">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtcartweight" runat="server" ClientInstanceName="txtcartweight" 
                                    Width="130px"  TabIndex="12" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            <!--  copies per carton -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblcartcopy" runat="server" TabIndex="-1" ClientInstanceName="lblcartcopy" 
                                    Text="Copies per carton"></dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtcartcopy" runat="server" ClientInstanceName="txtcartcopy" 
                                    Width="130px"  TabIndex="13" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            </dl>
                            </dx:PanelContent>
                            </PanelCollection>
                            </dx:ASPxPanel>
                            <!-- end carton panel -->      
                           <dx:ASPxPanel ID="dxpanelpaper" ClientInstanceName="panelpaper" runat="server" Width="450px">
                           <PanelCollection>
                           <dx:PanelContent> 
                            <dl class="dl2">
                             <dt>
                                <dx:ASPxLabel ID="dxlbltitle3" runat="server" TabIndex="-1"
                                    ClientInstanceName="lbltitle3" Text="Title or description">
                                </dx:ASPxLabel>
                                 &nbsp;</dt>
                            <dd>
                               <dx:ASPxTextBox ID="dxtxttitle3" runat="server" 
                                    ClientInstanceName="txttitle3" Width="200px"  TabIndex="14"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css"
                                    NullText="optional">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value"  />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <dt>
                                <dx:ASPxLabel ID="dxlblblock1" runat="server" TabIndex="-1"
                                    ClientInstanceName="lblblock1" Text="Block - longest">
                                </dx:ASPxLabel>
                            </dt> 
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtblock1" runat="server" 
                                    ClientInstanceName="txtblock1" Width="130px"  TabIndex="15"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated width -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblblock2" runat="server" TabIndex="-1" Text="Block - other side" 
                                    ClientInstanceName="lblblock2">
                                </dx:ASPxLabel>
                            </dt> 
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtblock2" runat="server" ClientInstanceName="txtblock2" 
                                    Width="130px"  TabIndex="16" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated volume -->
                            <dt>
                                <dx:ASPxLabel ID="dxlblextent" runat="server" TabIndex="-1" ClientInstanceName="lblextent" 
                                    Text="Extent - pages">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtextent" runat="server" ClientInstanceName="txtextent" 
                                    Width="130px"  TabIndex="17" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            <dt>
                                <dx:ASPxLabel ID="dxlblpaper" runat="server" TabIndex="-1" ClientInstanceName="lblpaper" 
                                    Text="Paper type gsm">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                 <dx:ASPxSpinEdit runat="server" 
                                    Increment="5" MaxValue="170" MinValue="5" NullText="Type number" 
                                    NumberType="Integer" Number="5" Width="60px"  TabIndex="18"
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    CssPostfix="Office2003Blue" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    ClientInstanceName="spinpaper" ID="dxspinpaper">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings> 
                                </dx:ASPxSpinEdit>
                            </dd>  
                            <dt>
                                <dx:ASPxLabel ID="dxlblcover" runat="server" TabIndex="-1" ClientInstanceName="lblcover" 
                                    Text="Hardback"></dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxCheckBox ID="dxckcover" ClientInstanceName="ckcover" runat="server" TabIndex="19">
                                </dx:ASPxCheckBox> 
                            </dd>  
                            </dl>
                            </dx:PanelContent>
                            </PanelCollection>
                            </dx:ASPxPanel>
                            <!-- end paper size panel -->
                            <!-- enclose cascading combos in updatepanel -->
                            <dx:ASPxCallbackPanel ID="dxcbkgroup" ClientInstanceName="cbkgroup" 
                                    runat="server" OnCallback="dxcbkgroup_Callback" 
                               CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                               CssPostfix="Office2003Blue">
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                            <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                            <!-- port of origin -->
                            <dl class="dl2">
                            <dt>
                             <dx:ASPxLabel ID="dxorigin" runat="server" TabIndex="-1" ClientInstanceName="lblorigin" 
                                            Text="Origin">
                                        </dx:ASPxLabel>
                            </dt>
                            <dd>
                             <dx:ASPxComboBox ID="dxcborigin" ClientInstanceName="cborigin" runat="server"  TabIndex="20"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        ValueType="System.Int32"  
                                        IncrementalFilteringMode="StartsWith" >
                                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Required" /> 
                                            </ValidationSettings> 
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { onOriginChanged(s); }" ></ClientSideEvents>
                               </dx:ASPxComboBox>
                            </dd> 
                            
                            <!-- country -->
                            <dt>
                                <dx:ASPxLabel ID="dxdest" runat="server" TabIndex="-1" ClientInstanceName="lbldest" 
                                    Text="Destination country">
                                </dx:ASPxLabel>
                            </dt> 
                            <dd>
                                 <dx:ASPxComboBox ID="dxcbcountry" ClientInstanceName="cbcountry" runat="server"  TabIndex="21"
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            OnCallback="dxcbcountry_Callback" ValueType="System.Int32" 
                                             IncrementalFilteringMode="StartsWith">
                                            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                            </LoadingPanelImage>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Required" /> 
                                            </ValidationSettings> 
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { onCountryChanged(s); }" />
                                 </dx:ASPxComboBox>
                            </dd> 
                            <!-- final destination -->
                            <dt>
                                 <dx:ASPxLabel ID="dxlblfinal" runat="server" TabIndex="-1" ClientInstanceName="lblfinal" 
                                    Text="Final destination">
                                </dx:ASPxLabel>
                            </dt> 
                            <dd>
                                  <dx:ASPxComboBox ID="dxcbfinal" ClientInstanceName="cbfinal" runat="server"  TabIndex="22"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    OnCallback="dxcbfinal_Callback" ValueType="System.Int32"  
                                      IncrementalFilteringMode="StartsWith">
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Required" /> 
                                            </ValidationSettings> 
                                </dx:ASPxComboBox>
                            </dd> 
                            </dl> 
                            </dx:PanelContent>
                            </PanelCollection>
                            </dx:ASPxCallbackPanel>
                            <!-- end update panel -->
                            <!-- total copies -->
                            <dl class="dl2">
                            <dt>
                                <dx:ASPxLabel ID="dxcopies" runat="server" TabIndex="-1" ClientInstanceName="lblcopies" 
                                    Text="Total copies">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtcopies" runat="server" ClientInstanceName="txtcopies" 
                                    Width="130px"  TabIndex="23" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    EnableViewState="False">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Number required" /> 
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>  
                            </dl> 
                            <!-- captcha -->
                              <div style="padding:  10px 10px 45px 125px; border-bottom: 5px; height: auto;">
                                    <dx:ASPxCaptcha ID="dxcapt1" ClientInstanceName="capt1" runat="server" 
                                        Height="133px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                    <ChallengeImage BackgroundColor="#DDECFE" BorderColor="#002D96" BorderWidth="1"></ChallengeImage>
                                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                        </LoadingPanelImage>
                                        <TextBox Position="Bottom" />
                                    </dx:ASPxCaptcha>
                                </div> 
                              <div style="padding:  10px; margin: 10px">
                              <asp:Panel ID="pnlmsg1" runat="server"  
                                        Visible="False" EnableViewState="False">
                                            <table>
                                                <tr>
                                                <td><img src="../Images/typography/box_alert.png" title = "Account not verified" 
                                                        alt="" align="top"/></td> 
                                                <td><div>Sorry, we are unable to process your details. Please check you email 
                                                    address and security code above.</div></td>
                                                </tr>
                                            </table> 
                                     </asp:Panel>
                                     
                                    <asp:Panel ID="pnlmsg2" runat="server"  Visible="False" EnableViewState="False">
                                            <table>
                                                <tr>
                                                <td><img src="../Images/typography/list_check.png" title = "Reminder email sent" 
                                                        alt="" align="top"/></td> 
                                                <td><div class="level3">
                                                    Your details have been sent to Publiship,&nbsp; we will contact you shortly. Thank 
                                                    you for your interest.</div><div style="padding: 0px 0px 0px 155px;">
                                                 <dx:ASPxButton ID="btnCancel2" runat="server" OnClick="btnCancel2_Click"
                                                      CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                                      SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css" Text="Ok" 
                                                      Width="80px" CausesValidation="False" UseSubmitBehavior="false" Native="True">
                                                
                                                      <ClientSideEvents Click="hideReminderWindow" />
                                                  </dx:ASPxButton>
                                                </div></td>
                                                </tr>
                                            </table> 
                                     </asp:Panel>
                            </div>          
                            <!-- end input form --> 
                        </div> 
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="6. Error!">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl7" runat="server">
                         <div class="alert">
                            <dx:ASPxLabel ID="dxlblerr" ClientInstanceName="lblerr" runat="server" 
                            Text="Error Message">
                          </dx:ASPxLabel>
                         
                        </div> 
                        <div style="margin: 0px Auto; width: auto; height: auto">
                           
                             <dx:ASPxButton ID="dxbtnback1" runat="server" AutoPostBack="False" 
                                 ClientInstanceName="btnback1" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                 Text="Try again" CausesValidation="False" UseSubmitBehavior="False">
                                 <ClientSideEvents Click="function(s, e) {
	                                onbuttonPrevious(s,e);
                                }" />
                             </dx:ASPxButton>
                        </div>
                     </dx:ContentControl>
                </ContentCollection> 
            </dx:TabPage>  
        </TabPages> 
                                              
        <ClientSideEvents EndCallback="function(s, e) {
	alert('ended');
}" />
                                              
    </dx:ASPxPageControl>
    
    
    <div style="width: 500px; float: left; margin: 0px Auto">

           <div style="float: left; width: auto; padding: 8px 8px 8px 0px">
                                    <dx:ASPxButton ID="dxbtnend0" runat="server" 
                                        ClientInstanceName="btnend0"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Text="Cancel" OnClick="dxbtnend0_Click" TabIndex="-1" 
                                        CausesValidation="False" UseSubmitBehavior="False" Native="true" 
                                        ClientEnabled="False" ClientVisible="False" Visible="False" >
                                  </dx:ASPxButton>
           </div>
           
           <div style="float: left; width: auto; padding:  8px 8px 8px 0px">
                                    <dx:ASPxButton ID="dxbtnback0" runat="server" AutoPostBack="False" 
                                        ClientInstanceName="btnback0"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Text="Back" TabIndex="-1" CausesValidation="False" 
                                        UseSubmitBehavior="False" Native="true">
                                        <ClientSideEvents Click="function(s, e) {
	onbuttonPrevious(s,e);
}" />
                                  </dx:ASPxButton>
           </div>

          <div style="float: right; width: auto; padding: 8px 0px 8px 8px">
                                    <dx:ASPxButton ID="dxbtnquote0" runat="server" 
                                    ClientInstanceName="btnquote0" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text="Request Quote" onclick="dxbtnget_Click" TabIndex="-1" 
                                        CausesValidation="True" UseSubmitBehavior="False" Native="true">
                                </dx:ASPxButton>
             </div>            
          <div style="float: right; width: auto; padding: 8px 0px 8px 8px">
                                    <dx:ASPxButton ID="dxbtnext0" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnext0" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text="Next" TabIndex="-1" UseSubmitBehavior="False" Native="true">
                                    <ClientSideEvents Click="function(s, e) {
	onbuttonNext(s,e);
}" />
                                </dx:ASPxButton>
             </div> 
             
   </div>     
   <input type="text" name="enterkeybugfix" style="display: none;" />
    <!-- end pages -->
    </div> 
    <!-- end formcenter580 -->
   
        <!-- end pager -->
    
    <!-- end results -->  
    <dx:ASPxHiddenField ID="dxhfpricer" runat="server" 
        ClientInstanceName="hfpricer">
    </dx:ASPxHiddenField>
          <!-- end datasource -->

      
</form>
</body> 
</html>  