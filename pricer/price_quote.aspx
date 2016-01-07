<%@ Page Language="C#" AutoEventWireup="true" CodeFile="price_quote.aspx.cs" MasterPageFile="~/WWI_m1.master" Inherits="price_quote" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

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


 
<asp:Content ID="content_default" ContentPlaceHolderID="ContentPlaceHolderM1" runat="server">
    
<script type="text/javascript">
        function onGroupValidation(s, e) {
            var v = e.value;
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
            setCookie("dims", rbl.GetValue().toString(), 5);
        }

        //not being used we do this when pricer dll is changed
        function onTabChanged(s, e) {
            //make sure ddls are updated according to pricer selected
            if (e.tab.index == 1) {
                cboPaper.PerformCallback('');
                //cbkorigin.PerformCallback(0);
                cbkgroup.PerformCallback(-1);
            }
        }
        //***

        function onPricerChanged(s, e) {
            //cbkgsm.PerformCallback(0);
            cboPaper.PerformCallback('');
            //cbkorigin.PerformCallback(0);
            cbkgroup.PerformCallback(-1);
        }
        
        function oncurrencychanged(cbo) {
             //save value to hidden field use text not value
            hfpricer.Set("crnc", cbo.GetText().toString() + ',' + cbo.GetValue().toString());
            //alert(hfpricer.Get("crnc"));
            setCookie("crnc", cbo.GetText().toString() + "," + cbo.GetValue().toString(), 5);
        }

        function onpalletchanged(cbo) {
            //save value to hidden field
            hfpricer.Set("pall", cbo.GetValue().toString());
            //alert(hfpricer.Get("pall"));
            setCookie("pall", cbo.GetValue().toString(), 5);
        }

        // Create a cookie with the specified name and value.
        //function setCookie(sName, sValue) {
        function setCookie(sName, sValue, sExpires) {
            document.cookie = sName + "=" + escape(sValue);

            if (sExpires != 0) {
                // Expires the cookie in N years!
                var date = new Date();
                date.setFullYear(date.getFullYear() + sExpires);
                document.cookie += ("; expires=" + date.toUTCString());
            }
            else {
                //force cookie to delete
                document.cookie += ("; expires=Thu, 01-Jan-1970 00:00:01 GMT"); 
            }
        }

        // Retrieve the value of the cookie with the specified name.
        function GetCookie(sName) {
            // cookies are separated by semicolons
            var aCookie = document.cookie.split("; ");
            for (var i = 0; i < aCookie.length; i++) {
                // a name/value pair (a crumb) is separated by an equal sign
                var aCrumb = aCookie[i].split("=");
                if (sName == aCrumb[0])
                    return unescape(aCrumb[1]);
            }
            // a cookie with the requested name does not exist
            return null;
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

        function onbuttonPrint(s, e) {
            //window.print();
            //open popup of todays prices for export
            var window = popPricer.GetWindowByName('printpricer');
            popPricer.ShowWindow(window);
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
                //120811 reload ddls?
                cbkgroup.PerformCallback(-1);
            }
            //}
            //else {
            //    var window = popWindows.GetWindowByName('loginform');
            //    popWindows.ShowWindow(window); 
            //}
        }
        
        function onbuttonNext(s, e) {
            //user = verify_user();
            //if (user != 'You are not logged in') {
                var indexTab = (pagepricer.GetActiveTab()).index;
                   
                pagepricer.SetActiveTab(pagepricer.GetTab(indexTab + 1));
                rndbuttons(indexTab + 1); //sets visible buttons

                if (indexTab + 1 == 1 || indexTab + 1 == 5) { rndinputpanels(); }
                
                 
            //}
            //else {
            //    var window = popWindows.GetWindowByName('loginform');
            //    popWindows.ShowWindow(window);
            //}
        }
        
        function trackInput()
        {
            var intype = hfpricer.Get("dims").toString();
            var svalues = "";
            
            switch (intype) {
                case "1": //book details
                    svalues = txtlength.GetText() + ";" +
                        txtwidth.GetText() + ";" + txtdepth.GetText() + ";" + txtweight.GetText();
                    
                    break;
                case "2": //carton details
                    svalues = txtside1.GetText() + ";" + txtside2.GetText() + ";" +
                        txtcartdepth.GetText() + ";" + txtcartweight.GetText();
                    break;
                case "3": //paper size
                
                    svalues = txtblock1.GetText() + ";" + txtblock2.GetText() + ";" +
                        txtextent.GetText() + ";" + cboPaper.GetText(); //spinpaper.GetNumber();
                    break;
                default:
                    break;
            }//end switch

            hfpricer.Set("inputs", svalues);
        }

        function trackTitle(s, e) {
            var svalues = s.GetText();
            hfpricer.Set("title", svalues);
        }

        function trackCartons(s, e) {
            var svalues = s.GetText();
            hfpricer.Set("cartons", svalues);
        }
        
        function rndinputpanels() {
                       
            var intype = hfpricer.Get("dims").toString();
             
            switch (intype) {
                case "1":
                    panelbook.SetVisible(true);
                    panelcarton.SetVisible(false);
                    panelpaper.SetVisible(false);
                    pnlclientbook.SetVisible(true);
                    pnlclientbook.SetVisible(false);
                    pnlclientbook.SetVisible(false);
                    break;
                case "2":
                    panelbook.SetVisible(false);
                    panelcarton.SetVisible(true);
                    panelpaper.SetVisible(false);
                    pnlclientbook.SetVisible(false);
                    pnlclientbook.SetVisible(true);
                    pnlclientbook.SetVisible(false);
                    break;
                case "3":
                    panelbook.SetVisible(false);
                    panelcarton.SetVisible(false);
                    panelpaper.SetVisible(true);
                    pnlclientbook.SetVisible(false);
                    pnlclientbook.SetVisible(false);
                    pnlclientbook.SetVisible(true);
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
                    btnprint0.SetVisible(false);
                    btnprice0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Choose dimensions or paper type</div>');
                    break;
                case 1:
                    btnquote0.SetVisible(true);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(false);
                    btnprice0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 2:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(true);
                    btnprice0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 3:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(true);
                    btnprice0.SetVisible(true);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 4:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(true);
                    btnprice0.SetVisible(true);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 5:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(true);
                    btnprice0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 6:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(false);
                    btnprice0.SetVisible(false);
                    //rpnpricer.SetHeaderText('<div class="date"><div class="month">Step</div><div class="day">1</div></div><div>Enter your book details</div>');
                    break;
                case 7:
                    btnquote0.SetVisible(false);
                    btnback0.SetVisible(true);
                    btnext0.SetVisible(false);
                    btnprint0.SetVisible(false);
                    btnprice0.SetVisible(true);
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

       
        function validbooksize(s, e) {
            var var1 = txtlength.GetValue();
            var var2 = txtwidth.GetValue();
            e.isValid = Number(var1) > 0;
            if (e.isValid) {
                e.isValid = Number(var1) >= Number(var2);
                if (!e.isValid) {
                    e.errorText = 'Length less than width';
                }
            }
            else {
                e.errorText = 'Dimension required';
            }
        }

        function validbookweight(s, e) {
            e.isValid = Number(txtweight.GetValue()) > 0; //50;
            if (!e.isValid) {
                e.errorText = 'Weight required';
            }
        }

        function validcartonsize(s, e) {
            var var1 = txtside1.GetValue();
            var var2 = txtside2.GetValue();
            e.isValid = Number(var1) > 0;
            if (e.isValid) {
                e.isValid = Number(var1) >= Number(var2);
                if (!e.isValid) {
                    e.errorText = 'Length less than width';
                }
            }
            else {
                e.errorText = 'Longest side required';
            }
        }
    
    </script> 
    
    <div class="formcenter580">
    
    <dx:ASPxPageControl ID="dxpagepricer" ClientInstanceName="pagepricer" 
        runat="server" ActiveTabIndex="0" ShowTabs="False" 
            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
            CssPostfix="Office2010Blue" 
            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
            TabSpacing="0px" Width="580px" Border-BorderStyle="None"   >
        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <ContentStyle>
            <Paddings Padding="5px" />
        </ContentStyle>

        <Border BorderStyle="None"></Border>
        <TabPages>
            <dx:TabPage Text="0. Dimensions or paper type">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl1" runat="server">
                            <div style="width: 450px; margin: 0 Auto;">
                                <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblstep1h1"  TabIndex="-1" runat="server" ClientInstanceName="step1h1" 
                                        Text="Step 1 of 2: Input type and currency" /></div>
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
                                    <!-- radio button to select input type book,carton or paper size -->
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
                                    <dx:ASPxComboBox ID="dxcbocurrency" runat="server"  TabIndex="2"
                                    ClientInstanceName="cbocurrency" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    ValueType="System.String" TextField="name" ValueField="value" Width="200px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
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
                                    <dx:ASPxComboBox ID="dxcbotypepallet" runat="server"  TabIndex="3"
                                    ClientInstanceName="cbotypepallet" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    ValueType="System.String" TextField="name" ValueField="value" Width="200px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                            onpalletchanged(s);
                                            }" />
                                    </dx:ASPxComboBox>
                                </dd> 
                                <!-- pricer to use - only for office use -->
                                <dt>
                                <dx:ASPxLabel ID="dxlblspreadsheet" runat="server" TabIndex="-1"
                                    ClientInstanceName="lblspreadsheet" Text="Pricer">
                                    </dx:ASPxLabel>
                                </dt>
                                <dd>
                                    <dx:ASPxComboBox ID="dxcbospreadsheet"  ClientInstanceName="cbospreadsheet" 
                                        runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Width="250px">
                                        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                        </LoadingPanelImage>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                         <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                            onPricerChanged(s, e);
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
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblstep2h1" runat="server" TabIndex="-1" ClientInstanceName="step2h1" 
                                        Text="Step 2 of 2: Input dimensions and copies" /></div>
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
                                    NullText="optional" MaxLength="150">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value" />
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackTitle(s,e);}" />
                               </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated pallets -->
                            <dt>
                                <dx:ASPxLabel ID="dxlength" runat="server" TabIndex="-1" ClientInstanceName="lbllength" 
                                    Text="Longest side (mm)">
                                </dx:ASPxLabel>
                                </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtlength" runat="server" 
                                    ClientInstanceName="txtlength" Width="130px"  TabIndex="3"
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings EnableCustomValidation="false" >
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required"  />
                                        <RequiredField IsRequired="true" ErrorText="Dimension required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e) {validbooksize(s,e);}" ValueChanged="function(s, e) {trackInput();}" />
                                </dx:ASPxTextBox>
                            </dd>
                            <!--  estimated width -->
                            <dt>
                                <dx:ASPxLabel ID="dxwidth" runat="server" Text="Other side (mm)" TabIndex="-1"
                                    ClientInstanceName="lblwidth">
                                </dx:ASPxLabel></dt> 
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtwidth" runat="server" ClientInstanceName="txtwidth" 
                                    Width="130px"  TabIndex="4" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings EnableCustomValidation="false" >
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Dimension required" /> 
                                    </ValidationSettings>
                                     <ClientSideEvents ValueChanged="function(s, e) {trackInput();}" />
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
                                    <ValidationSettings EnableCustomValidation="false">
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Depth required" />
                                        <RequiredField IsRequired="true" ErrorText="Depth required" /> 
                                    </ValidationSettings>
                                     <ClientSideEvents ValueChanged="function(s, e) {trackInput();}" />
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
                                    <ValidationSettings EnableCustomValidation="False">
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Weight required" />
                                        <RequiredField IsRequired="true" ErrorText="Weight required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e) {validbookweight(s,e);}" ValueChanged="function(s, e) {trackInput();}" />
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
                                    <ClientSideEvents ValueChanged="function(s, e) {trackCartons(s, e);}" />
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
                                    <ClientSideEvents ValueChanged="function(s, e) {trackTitle(s, e);}" />
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
                                    <ValidationSettings EnableCustomValidation="true">
                                        <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Longest side required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e) {validcartonsize(s,e);}" ValueChanged="function(s, e) {trackInput();}"/>
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
                                        <RequiredField IsRequired="true" ErrorText="Next side required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
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
                                        <RequiredField IsRequired="true" ErrorText="Depth required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
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
                                        <RequiredField IsRequired="true" ErrorText="Weight required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
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
                                    <ClientSideEvents ValueChanged="function(s, e) {trackCartons(s, e);}"/>
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
                             </dt>
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
                                     <ClientSideEvents ValueChanged="function(s, e) {trackTitle(s, e);}"/>
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
                                        <RequiredField IsRequired="true" ErrorText="Block required" /> 
                                    </ValidationSettings>
                                     <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
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
                                        <RequiredField IsRequired="true" ErrorText="Block required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
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
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Height="19px">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number required" />
                                        <RequiredField IsRequired="true" ErrorText="Extent required" /> 
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e) {trackInput();}"/>
                                </dx:ASPxTextBox>
                            </dd>  
                            <dt>
                                <dx:ASPxLabel ID="dxlblpaper" runat="server" TabIndex="-1" ClientInstanceName="lblpaper" 
                                    Text="Paper type gsm">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                 <dx:ASPxComboBox ID="dxcboPaper" runat="server" ClientInstanceName="cboPaper" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" 
                                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     ValueType="System.String" Width="100px" OnCallback="dxcboPaper_Callback">
                                     <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                     </LoadingPanelImage>
                                     <ButtonStyle Width="13px">
                                     </ButtonStyle>
                                 </dx:ASPxComboBox>
                            </dd>  
                            <dt>
                                <dx:ASPxLabel ID="dxlblcover" runat="server" TabIndex="-1" ClientInstanceName="lblcover" 
                                    Text="Hardback"></dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxCheckBox ID="dxckcover" ClientInstanceName="ckcover" runat="server" 
                                    TabIndex="19" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
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
                               CssPostfix="Office2003Blue" EnableViewState="False" 
                               LoadingPanelImagePosition="Top">
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                            <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                            
                            <dl class="dl2">
                            <!-- origin -->
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
                                    Text="Destination Country">
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
                            <!-- end input form --> 
                        </div> 
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="2. Quote">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl3" runat="server">
                           <div class="info2">
                                    <div style="padding: 8px; width: 440px; clear: both">
                                        <dx:ASPxLabel ID="dxlblquoteh1" 
                                            runat="server" ClientInstanceName="quoteh1" 
                                        Text="Quote details " Font-Bold="True" Font-Size="Medium" />
                                        <dx:ASPxLabel ID="dxlblquote" runat="server" 
                                    ClientInstanceName="lblquote" Text="" /></div>
                            </div>
                           <!-- end header --> 
                          <div class="formcenter580">
                            <div class="cell580_80"><dx:ASPxLabel ID="dxlblbookname" runat="server" 
                                    ClientInstanceName="lblbookname" Text="title" Font-Bold="True" /></div>
                            <div class="cell580_20">   
                                <dx:ASPxButton ID="dxbtnclient" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnclient" Text="Client" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    CausesValidation="False" UseSubmitBehavior="False" ClientVisible="False">
                                    <ClientSideEvents 
                                        Click="function(s, e) { 
                                        cbkshipment.PerformCallback(''); 
                                        pagepricer.SetActiveTab(pagepricer.GetTab(5));
                                        rndbuttons(5); }" />
                                </dx:ASPxButton></div>
                            <!-- end header -->
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblqot1" runat="server" ClientInstanceName="lblqot1" Text="Book size (mm)" /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblqot2" runat="server" ClientInstanceName="lblqot2" Text="Book weight" /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblsize" runat="server" ClientInstanceName="lblsize" Text="{0} x {1} x {2}" /></div>
                            <div class="cell580_50"><dx:ASPxLabel ID="dxlblweight" runat="server" 
                                    ClientInstanceName="lblweight" Text="{0}" /></div>
                            <!-- end book info -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot3" runat="server" ClientInstanceName="lblqot3" Text="From" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot4" runat="server" ClientInstanceName="lblqot4" Text="To" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblqot5" runat="server" ClientInstanceName="lblqot5" Text="Shipping Via" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblfrom" runat="server" ClientInstanceName="lblfrom" Text="from" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblto" runat="server" ClientInstanceName="lblto" Text="to" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvia" runat="server" ClientInstanceName="lblvia" Text="via" /></div>
                            <!-- end origin/destination -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblcopies" runat="server" ClientInstanceName="lblcopies" Text="{0} copies" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblppc" runat="server" ClientInstanceName="lblppc" Text="{0} per copy" Font-Bold="True" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblqot6" runat="server" ClientInstanceName="lblqot6" Text="Pallet type" /></div>
                            <!-- end copies -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblqot7" runat="server" ClientInstanceName="lblqot7" Text="Pre-Palletised" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblpricepre" runat="server" 
                                    ClientInstanceName="lblpricepre" Text="ppc prepalletised" Font-Bold="True" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlbltype" runat="server" ClientInstanceName="lbltype" Text="pallet type" /></div>
                            <!-- end pre-palletised -->
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblshiploose" runat="server" ClientInstanceName="lblshiploose" Text="shipped as loose" /></div>
                            <div class="cell580_40"><dx:ASPxLabel ID="dxlblpriceloose" runat="server" 
                                    ClientInstanceName="lblpriceloose" Text="ppc loose" Font-Bold="True" /></div>
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtnship" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnship" Text="Shipment" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    CausesValidation="False" UseSubmitBehavior="False">
                                    <ClientSideEvents 
                                        Click="function(s, e) { 
                                        cbkshipment.PerformCallback(''); 
                                        pagepricer.SetActiveTab(pagepricer.GetTab(4));
                                        rndbuttons(4); }" />
                                </dx:ASPxButton>
                              </div>
                            <!-- end loose -->
                            <div class="cell580_80" style="background-color: #E4EBF1"><dx:ASPxLabel ID="dxlbllclname" runat="server" ClientInstanceName="lbllclname" Text="All LCL Pre-palletised" /></div>
                            <div class="cell580_20" style="background-color: #E4EBF1"></div>
                            <div class="cell580_20">
                                </div>
                            <div class="cell580_20">40&#39; HC</div>
                            <div class="cell580_20">40&#39;</div>
                            <div class="cell580_20">20&#39;</div>
                            <div class="cell580_20">LCL</div>
                            <!-- end result value headers -->
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtncosting1" runat="server" AutoPostBack="False" 
                                    CausesValidation="False" ClientInstanceName="btncosting1" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Costing" 
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) { 
                                        cbkcosting.PerformCallback('pre-palletised'); 
                                        pagepricer.SetActiveTab(pagepricer.GetTab(3));
                                        rndbuttons(3); }" />
                                </dx:ASPxButton>
                              </div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv40hc" runat="server" ClientInstanceName="lblv40hc" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv40" runat="server" ClientInstanceName="lblv40" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv20" runat="server" ClientInstanceName="lblv20" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblv" runat="server" ClientInstanceName="lblv" Text="0" /></div>
                            <!-- end all LCL values -->
                            <div class="cell580_80" style="background-color: #E4EBF1"><dx:ASPxLabel ID="dxlblloosename" ClientInstanceName="lblloosename" runat="server" Text="All LCL Shipped Loose inc Pallet Cost" /></div>
                            <div class="cell580_20" style="background-color: #E4EBF1">
                              </div>
                            <!-- end shipped loose headers -->
                            <div class="cell580_20">
                                <dx:ASPxButton ID="dxbtncosting2" runat="server" AutoPostBack="False" 
                                    CausesValidation="False" ClientInstanceName="btncosting1" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Text="Costing" 
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) { 
                                        cbkcosting.PerformCallback('loose'); 
                                        pagepricer.SetActiveTab(pagepricer.GetTab(3));
                                        rndbuttons(3); }" />
                                </dx:ASPxButton>
                              </div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose40hc" ClientInstanceName="lblvloose40hc" runat="server" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose40" ClientInstanceName="lblvloose40" runat="server" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose20" ClientInstanceName="lblvloose20" runat="server" Text="0" /></div>
                            <div class="cell580_20"><dx:ASPxLabel ID="dxlblvloose" ClientInstanceName="lblvloose" runat="server" Text="0" /></div>
                            <!-- end all shipped loose values -->
                        </div>
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
              <dx:TabPage Text="3. Costing Summary">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl4" runat="server">
                          <dx:ASPxCallbackPanel ID="dxcbkcosting" ClientInstanceName="cbkcosting" 
                              runat="server" width="100%" OnCallback="dxcbkcosting_Callback" 
                              CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                              CssPostfix="Office2003Blue">
                              <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                              </LoadingPanelImage>
                          <PanelCollection>
                          <dx:PanelContent>

                          <div> 
                            <div class="info2">
                            <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblcostingh1" runat="server" ClientInstanceName="costingh1" 
                                        Text="Costing summary" /></div>
                            <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblcostingh2" runat="server" ClientInstanceName="costingh2" 
                                        Text="{0} Copies {1}" /></div>
                            </div>
                            <!-- end header -->            
                            <div style="padding: 8px; float: left; width:200px;">
                            <dl class="dl2">
                                <dt>Pre-Carriage</dt>
                                <dd></dd>
                                <dt>Part Load</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre1" runat="server" ClientInstanceName="lblpre1" 
                                        Text="0" /></dd>
                                <dt>Full Load</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre2" runat="server" ClientInstanceName="lblpre2" Text="0" /></dd>
                                <dt>20' TCH</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre3" runat="server" ClientInstanceName="lblpre3" Text="0" /></dd>
                                <dt>40' THC</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre4" runat="server" ClientInstanceName="lblpre4" Text="0" /></dd>
                                <dt>LCL TCH</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre5" runat="server" ClientInstanceName="lblpre5" Text="0" /></dd>
                                <dt>Documentation</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre6" runat="server" ClientInstanceName="lblpre6" Text="0" /></dd>
                                <dt>Other Origin</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre7" runat="server" ClientInstanceName="lblpre7" Text="0" /></dd>
                                <dt>20' FCL Haulage</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre8" runat="server" ClientInstanceName="lblpre8" Text="0" /></dd>
                                <dt>40' FCL Haulage</dt>
                                <dd><dx:ASPxLabel ID="dxlblpre9" runat="server" ClientInstanceName="lblpre9" Text="0" /></dd>
                            </dl>
                            </div>
                             <div style="float: left; width:135px; padding-top: 8px;">
                            <dl class="dl3">
                                <dt>Freight</dt>
                                <dd></dd>
                                <dt>LCL</dt>
                                <dd><dx:ASPxLabel ID="dxlblfre1" runat="server" ClientInstanceName="dxlblfre1" Text="0" /></dd>
                                <dt>20'</dt>
                                <dd><dx:ASPxLabel ID="dxlblfre2" runat="server" ClientInstanceName="dxlblfre2" Text="0" /></dd>
                                <dt>40'</dt>
                                <dd><dx:ASPxLabel ID="dxlblfre3" runat="server" ClientInstanceName="dxlblfre3" Text="0" /></dd>
                                <dt>40' HQC</dt>
                                <dd><dx:ASPxLabel ID="dxlblfre4" runat="server" ClientInstanceName="dxlblfre4" Text="0" /></dd>
                            </dl>
                            </div>
                             <div style="padding: 8px; float: right; width:200px;">
                            <dl class="dl2">
                                <dt>On-Carriage</dt>
                                <dd></dd>
                                <dt>Dest LCL THC</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc1" runat="server" ClientInstanceName="lblonc1" 
                                        Text="0" /></dd>
                                <dt>Pier Loading etc</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc2" runat="server" ClientInstanceName="lblonc2" Text="0" /></dd>
                                <dt>Dest 20' THC</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc3" runat="server" ClientInstanceName="lblonc3" Text="0" /></dd>
                                <dt>Dest 40' THC</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc4" runat="server" ClientInstanceName="lblonc4" Text="0" /></dd>
                                <dt>Documentation</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc5" runat="server" ClientInstanceName="lblonc5" Text="0" /></dd>
                                <dt>Customs</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc6" runat="server" ClientInstanceName="lblonc6" Text="0" /></dd>
                                <dt>Part Load</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc7" runat="server" ClientInstanceName="lblonc7" Text="0" /></dd>
                                <dt>Full Load</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc8" runat="server" ClientInstanceName="lblonc8" Text="0" /></dd>
                                <dt>20' FCL Haualage</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc9" runat="server" ClientInstanceName="lblonc9" Text="0" /></dd>
                                <dt>40' FCL Haualage</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc10" runat="server" ClientInstanceName="lblonc10" Text="0" /></dd>
                                <dt>20' Shunt and Devan</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc11" runat="server" ClientInstanceName="lblonc11" Text="0" /></dd>
                                <dt>40' Shunt and Devan</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc12" runat="server" ClientInstanceName="lblonc12" Text="0" /></dd>
                                <dt>Pallets</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc13" runat="server" ClientInstanceName="lblonc13" Text="0" /></dd>
                                <dt>Other Dest Charges</dt>
                                <dd><dx:ASPxLabel ID="dxlblonc14" runat="server" ClientInstanceName="lblonc14" Text="0" /></dd>
                            </dl>
                           </div>
                           </div>
                           </dx:PanelContent> 
                           </PanelCollection> 
                           </dx:ASPxCallbackPanel>
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="4. Shipment size">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl5" runat="server">
                          <dx:ASPxCallbackPanel ID="dxcbkshipment" ClientInstanceName="cbkshipment" 
                              runat="server" Width="580px" OnCallback="dxcbkshipment_Callback" 
                              CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                              CssPostfix="Office2003Blue">
                              <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                              </LoadingPanelImage>
                          <PanelCollection>
                          <dx:PanelContent>
                            <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblshiph1" runat="server" ClientInstanceName="shiph1" 
                                        Text="Shipment size" /></div>
                            
                            </div>
                            <!-- end header --> 
                                <div style="padding: 8px; float: left; width:250px;">
                                   <dl class="dl1">
                                        <dt>Copies per Carton</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship1" runat="server" ClientInstanceName="lblship1" Text="0" /></dd>
                                        <dt>Total Cartons</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship2" runat="server" ClientInstanceName="lblship2" Text="0" /></dd>
                                        <dt>Cartons On pallet</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship3" runat="server" ClientInstanceName="lblship3" Text="0" /></dd>
                                   </dl>
                                </div>
                                <div style="padding: 8px; float: right; width:250px;">
                                <dl class="dl1">
                                        <dt>Carton Height mm</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship12" runat="server" ClientInstanceName="lblship12" Text="0" /></dd>
                                        <dt>Carton Length mm</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship13" runat="server" ClientInstanceName="lblship13" Text="0" /></dd>
                                        <dt>Carton Width mm</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship14" runat="server" ClientInstanceName="lblship14" Text="0" /></dd>
                                        <dt>Carton Weight kgs</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship15" runat="server" ClientInstanceName="lblship15" Text="0" /></dd>
                                   </dl>
                                 </div>
                                 <div style="clear: both"></div> 
                                 <div style="padding: 8px; float: left; width:250px;">
                                   <dl class="dl1">
                                        <dt>Full Pallets</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship4" runat="server" ClientInstanceName="lblship4" Text="0" /></dd>
                                        <dt>Full Pallet Weight</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship5" runat="server" ClientInstanceName="lblship5" Text="0" /></dd>
                                        <dt>Full Pallet Cube</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship6" runat="server" ClientInstanceName="lblship6" Text="0" /></dd>
                                        <dt>Max Per Layer</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship7" runat="server" ClientInstanceName="lblship7" Text="0" /></dd>
                                        <dt>Number Of Layers</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship8" runat="server" ClientInstanceName="lblship8" Text="0" /></dd>
                                   </dl>
                                  </div>
                                   <div style="padding: 8px; float: right; width:250px;">
                                 <dl class="dl1">
                                        <dt>Part Pallets</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship16" runat="server" ClientInstanceName="lblship16" Text="0" /></dd>
                                        <dt>Remaining Cartons</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship17" runat="server" ClientInstanceName="lblship17" Text="0" /></dd>
                                        <dt>Residue Pallet Cube</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship18" runat="server" ClientInstanceName="lblship18" Text="0" /></dd>
                                        <dt>Residue Pallet Weight</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship19" runat="server" ClientInstanceName="lblship19" Text="0" /></dd>
                                   </dl>
                                </div>
                                <div style="clear: both"></div> 
                                  <div style="padding: 8px; float: left; width:250px;">
                                   <dl class="dl1">  
                                        <dt>Total Palletised Weight</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship9" runat="server" ClientInstanceName="lblship9" Text="0" /></dd>
                                        <dt>Total Palletised Cube</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship10" runat="server" ClientInstanceName="lblship10" Text="0" /></dd>
                                        <dt>Pallet Weight:Cube Ratio</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship11" runat="server" ClientInstanceName="lblship11" Text="0" /></dd>
                                   </dl>
                                </div>
                            
                               <div style="padding: 8px; float: right; width:250px;">
                                 <dl class="dl1">
                                        <dt>Total Carton Weight</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship20" runat="server" ClientInstanceName="lblship20" Text="0" /></dd>
                                        <dt>Total Carton Cube</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship21" runat="server" ClientInstanceName="lblship21" Text="0" /></dd>
                                        <dt>Carton Weight:Cube Ratio</dt>
                                        <dd><dx:ASPxLabel ID="dxlblship22" runat="server" ClientInstanceName="lblship22" Text="0" /></dd>
                                   </dl>
                                </div> 
                           </dx:PanelContent> 
                           </PanelCollection> 
                           </dx:ASPxCallbackPanel>
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
             <dx:TabPage Text="5. Client Quote">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl6" runat="server">
                          <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="dxlblclienth1" 
                                            runat="server" ClientInstanceName="lblclienth1" 
                                        Text="Quote details" /></div>
                            </div>
                           <!-- end header --> 
                          <div class="cell580_80"><dx:ASPxLabel ID="dxlblbookname2" runat="server" ClientInstanceName="lblbookname2" Text="title" /></div>
                          <div class="cell580_5"><dx:ASPxLabel ID="dxlblquote2" runat="server" 
                                  ClientInstanceName="lblquote2" Text="" Visible="False" /></div>
                          <div style="padding: 8px; float: left; width:250px;">
                          
                           <dx:ASPxPanel ID="dxpnlclientbook" ClientInstanceName="pnlclientbook" runat="server" Width="250px">
                           <PanelCollection>
                           <dx:PanelContent> 
                           <dl class="dl1">
                                <dt>Length</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient1" runat="server" ClientInstanceName="lblclient1" Text="{0} mm" /></dd>
                                <dt>Width</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient2" runat="server" ClientInstanceName="lblclient2" Text="{0} mm" /></dd>
                                <dt>Depth</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient3" runat="server" ClientInstanceName="lblclient3" Text="{0} mm" /></dd>
                                <dt>Weight</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient4" runat="server" ClientInstanceName="lblclient4" Text="{0} gms" /></dd>
                                <dt>Copies/carton</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient5" runat="server" ClientInstanceName="lblclient5" Text="" /></dd>
                            </dl> 
                            </dx:PanelContent> 
                            </PanelCollection> 
                            </dx:ASPxPanel>
                             
                             <dx:ASPxPanel ID="dxpnlclientcarton" ClientInstanceName="pnlclientcarton" runat="server" Width="250px">
                           <PanelCollection>
                           <dx:PanelContent>
                                <dl class="dl1">
                                <dt>Length</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient6" runat="server" ClientInstanceName="lblclient7" Text="{0} mm" /></dd>
                                <dt>Width</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient7" runat="server" ClientInstanceName="lblclient7" Text="{0} mm" /></dd>
                                <dt>Height</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient8" runat="server" ClientInstanceName="lblclient8" Text="{0} mm" /></dd>
                                 <dt>Weight</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient9" runat="server" ClientInstanceName="lblclient9" Text="{0} kgs" /></dd>
                                <dt>Copies/carton</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient10" runat="server" ClientInstanceName="lblclient10" Text="" /></dd>
                                </dl> 
                           </dx:PanelContent> 
                           </PanelCollection> 
                           </dx:ASPxPanel> 
                          
                           <dx:ASPxPanel ID="dxpnlclientpaper" ClientInstanceName="pnlclientpaper" runat="server" Width="250px">
                           <PanelCollection>
                           <dx:PanelContent> 
                            <dl class="dl1">
                                <dt>Length</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient11" runat="server" ClientInstanceName="lblclient11" Text="{0} mm" /></dd>
                                <dt>Width</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient12" runat="server" ClientInstanceName="lblclient12" Text="{0} mm" /></dd>
                                <dt>Extent</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient13" runat="server" ClientInstanceName="lblclient13" Text="{0} pp" /></dd>
                                <dt>Paper Weight</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient14" runat="server" ClientInstanceName="lblclient14" Text="{0} gsm" /></dd>
                                <dt>Hardback</dt>
                                <dd><dx:ASPxLabel ID="dxlblclient15" runat="server" ClientInstanceName="lblclient15" Text="" /></dd>
                                </dl>
                           </dx:PanelContent> 
                           </PanelCollection> 
                           </dx:ASPxPanel>
                          </div>
                           <!-- end book info -->
                         
                          <div style="padding: 8px; float: right; width:250px;">
                            <dl class="dl1">
                                <dt>Origin</dt>
                                <dd><dx:ASPxLabel ID="dxlblfrom2" runat="server" ClientInstanceName="lblfrom2" Text="from" /></dd>
                                <dt>Final Destination</dt>
                                <dd><dx:ASPxLabel ID="dxlblto2" runat="server" ClientInstanceName="lblto2" Text="to" /></dd>
                                <dt>Shipping Via</dt>
                                <dd><dx:ASPxLabel ID="dxlblvia2" runat="server" ClientInstanceName="lblvia2" Text="via" /></dd>
                            </dl> 
                          </div>
                          <!-- end origin/destination -->
                          <div style="clear: both; width: 375px; margin: 0 Auto">
                             <dx:ASPxRoundPanel ID="dxrndprice" runat="server" Width="375px" 
                                  CssFilePath="~/App_Themes/Aqua/{0}/styles.css" 
                                  CssPostfix="Aqua" GroupBoxCaptionOffsetY="-28px" 
                                  HeaderText="Your quote (subject to agreed validity terms)" 
                                  SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css">
                                 <ContentPaddings Padding="14px" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                       <dl class="dl4">
                                            <dt>Number Of Copies</dt>
                                            <dd><dx:ASPxLabel ID="dxlblcopies2" runat="server" ClientInstanceName="lblcopies2" 
                                                    Text="{0} copies" Font-Bold="True" /></dd>
                                            <dt><dx:ASPxLabel ID="dxlblppc2" runat="server" ClientInstanceName="lblppc2" 
                                                    Text="{0} per copy" Font-Bold="false" /></dt>
                                            <dd><dx:ASPxLabel ID="dxlblppc3" runat="server" ClientInstanceName="lblppc3" 
                                                    Text="{0}" Font-Bold="True" /></dd>
                                            <dt>Total Shipment Price</dt>
                                            <dd><dx:ASPxLabel ID="dxlbltotprice2" runat="server" 
                                                    ClientInstanceName="lbltotprice2" Text="{0}" Font-Bold="True" 
                                                    /></dd>
                                            <dt><dx:ASPxLabel ID="dxlblexprice0" runat="server" 
                                                    ClientInstanceName="lblexprice0" Text="Sterling (pence) per copy" 
                                                    Font-Bold="False" ClientVisible="False" 
                                                    /></dt>
                                            <dd><dx:ASPxLabel ID="dxlblexprice2" runat="server" 
                                                    ClientInstanceName="lblexprice2" Text="{0}" Font-Bold="True" ClientVisible="False" 
                                                    /></dd>
                                            <dt><dx:ASPxLabel ID="dxlblexprice1" runat="server" 
                                                    ClientInstanceName="lblexprice1" Text="Sterling total shipment price" 
                                                    Font-Bold="False" ClientVisible="False" 
                                                    /></dt>
                                            <dd><dx:ASPxLabel ID="dxlblexprice3" runat="server" 
                                                    ClientInstanceName="lblexprice3" Text="{0}" Font-Bold="True" ClientVisible="False" 
                                                    /></dd>
                                         </dl>
                                        <div style="margin: 0px Auto; width: 300px; height: auto">
                                            <dx:ASPxButton ID="dxbtnemailform" runat="server" AutoPostBack="false" UseSubmitBehavior="false" 
                                                    ClientInstanceName="btnemailform" 
                                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                    CssPostfix="Office2003Blue"
                                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" TabIndex="-1" 
                                                    Text="Email" >
                                                    <ClientSideEvents Click="function(s, e) { onbuttonEmail(s,e); }" />
                                                </dx:ASPxButton>
                                         </div>
                                     </dx:PanelContent> 
                                </PanelCollection> 
                              </dx:ASPxRoundPanel>
                        </div>
                     
                     </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="6. Error!">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl7" runat="server">
                         <div class="info">
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
            <dx:TabPage Text="7. Email">
                <ContentCollection>
                    <dx:ContentControl>
                         <div style="width: 450px; margin: 0 Auto;">
                           <div class="info2">
                                    <div style="padding: 8px; clear: both;"><dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="step2h1" 
                                        Text="Email order details and price to Publiship" /></div>
                            </div>
                           <!-- end header -->
                           <dl class="dl2">
                                <dt><dx:ASPxLabel ID="dxlblponum" ClientInstanceName="lblponum" TabIndex="-1" runat="server" Text="P.O. Number">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtponum" ClientInstanceName="txtponum" runat="server" TabIndex="24"
                                        Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                <dt><dx:ASPxLabel ID="dxlblisbn" ClientInstanceName="lblisbn" TabIndex="-1" runat="server" Text="ISBN">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtisbn" ClientInstanceName="txtisbn" runat="server" TabIndex="25"
                                        Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                <dt><dx:ASPxLabel ID="dxlblimpression" ClientInstanceName="lblimpression" TabIndex="-1" runat="server" Text="Impression">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtimpression" ClientInstanceName="txtimpression" 
                                        runat="server" TabIndex="26"
                                        Width="170px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                
                                <dt><dx:ASPxLabel ID="dxlblprinter" ClientInstanceName="lblprinter" TabIndex="-1" runat="server" Text="Printer">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtprinter" ClientInstanceName="txtprinter" TabIndex="27"
                                        runat="server" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                <dt><dx:ASPxLabel ID="dxlblexworks" ClientInstanceName="lblexworks" TabIndex="-1" runat="server" Text="Ex-works date">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxDateEdit ID="dxdtexworks"  ClientInstanceName="dtexworks" TabIndex="28"
                                        runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxDateEdit></dd>
                                <dt><dx:ASPxLabel ID="dxlbldue" ClientInstanceName="lbldue" runat="server" TabIndex="-1" Text="Delivery due date">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxDateEdit ID="dxdtdue"  ClientInstanceName="dtdue" runat="server" TabIndex="29"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxDateEdit></dd>
                                <dt><dx:ASPxLabel ID="dxlblcomment" ClientInstanceName="lblcomment" TabIndex="-1" runat="server" Text="Comments">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxMemo ID="dxmemocomment" ClientInstanceName="memocomment" runat="server" TabIndex="30"
                                        Height="71px" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxMemo></dd>
                                <dt><dx:ASPxLabel ID="dxlblcontact" ClientInstanceName="lblcontact" TabIndex="-1" runat="server" Text="Publiship contact">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxComboBox ID="dxcbocontact" ClientInstanceName="cbocontact" TabIndex="31"
                                        runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        IncrementalFilteringMode="StartsWith">
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ValidationSettings>
                                        <RequiredField ErrorText="Required" IsRequired="True" />
                                    </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </dd>
                                <dt><dx:ASPxLabel ID="dxlblyourname" ClientInstanceName="lblyourname" TabIndex="-1" runat="server" Text="Your name">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtusername" ClientInstanceName="txtusername" TabIndex="32"
                                        runat="server" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>    
                                <dt><dx:ASPxLabel ID="dxlblusercomp" ClientInstanceName="lblusercomp" TabIndex="-1"
                                        runat="server" Text="Company">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtusercomp" ClientInstanceName="txtusercomp" TabIndex="33"
                                        runat="server" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                <dt><dx:ASPxLabel ID="dxlblusertel" ClientInstanceName="lblusertel" TabIndex="-1" runat="server" Text="Contact Tel">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtusertel" ClientInstanceName="txtusertel" TabIndex="34"
                                        runat="server" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                                <dt><dx:ASPxLabel ID="dxlbluseremail" ClientInstanceName="lbluseremail" TabIndex="-1"  runat="server" Text="Email address">
                                    </dx:ASPxLabel></dt>
                                <dd><dx:ASPxTextBox ID="dxtxtuseremail" ClientInstanceName="txtuseremail" TabIndex="35"
                                        runat="server" Width="170px" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    </dx:ASPxTextBox></dd>
                           <dt></dt>
                           <dd><dx:ASPxButton ID="dxbtnemailsend" runat="server" 
                                                    ClientInstanceName="btnemailsend" 
                                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                    CssPostfix="Office2003Blue" OnClick="dxbtnemail_Click" 
                                                    
                                   SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" TabIndex="-1" 
                                                    Text="Send email" 
                                   UseSubmitBehavior="False">
                                                </dx:ASPxButton>
                           </dd>
                           </dl> 
                           <div>
                            
                           </div> 
                           <!-- end form --> 
                         </div>    
                    </dx:ContentControl> 
                </ContentCollection> 
            </dx:TabPage> 
            <dx:TabPage Text="1. Carton details">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="1. Paper size and extent">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages> 
                                              
        <Paddings PaddingLeft="0px" PaddingRight="0px" />
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
                                               
    </dx:ASPxPageControl>
    
    <div style="width: 580px; margin: 0 Auto;">

           <div style="float: left; width: auto; padding: 8px 8px 8px 0px">
                                    <dx:ASPxButton ID="dxbtnend0" runat="server" 
                                        ClientInstanceName="btnend0"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Text="Cancel" OnClick="dxbtnend0_Click" TabIndex="-1" 
                                        CausesValidation="False" UseSubmitBehavior="False">
                                  </dx:ASPxButton>
           </div>
           
           <div style="float: left; width: auto; padding:  8px 8px 8px 0px">
                                    <dx:ASPxButton ID="dxbtnback0" runat="server" AutoPostBack="False" 
                                        ClientInstanceName="btnback0"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Text="Back" TabIndex="-1" CausesValidation="False" 
                                        UseSubmitBehavior="False">
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
                                    Text="Get Quote" onclick="dxbtnget_Click" TabIndex="-1" 
                                        UseSubmitBehavior="False">
                                </dx:ASPxButton>
             </div>            
          <div style="float: right; width: auto; padding: 8px 0px 8px 8px">
                                    <dx:ASPxButton ID="dxbtnext0" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnext0" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Text="Next" TabIndex="-1" CausesValidation="False" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	onbuttonNext(s,e);
}" />
                                </dx:ASPxButton>
             </div> 
             
           <div style="float: right; width: auto;padding: 8px 0px 8px 8px">
                 <dx:ASPxButton ID="dxbtnprint0" runat="server" AutoPostBack="False" 
                     ClientInstanceName="btnprint0"
                     Text="Print" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                     CssPostfix="Office2003Blue" 
                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                     TabIndex="-1" CausesValidation="False" UseSubmitBehavior="False">
                     <ClientSideEvents Click="function(s, e) {
	onbuttonPrint(s,e);
}" />
                 </dx:ASPxButton>
           </div>
           
           <div style="float: left; width: auto; padding:  8px 8px 8px 0px">
                                    <dx:ASPxButton ID="dxbtnprice0" runat="server" AutoPostBack="False" 
                                        ClientInstanceName="btnprice0"
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                        Text="View Quote" TabIndex="-1" CausesValidation="False" 
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	onbuttonPrice(s,e);
}" />
                                  </dx:ASPxButton>
           </div>

   </div>   
   <!-- prevent postback on enter key -->  
   <input type="text" name="enterkeybugfix" style="display: none;" />
    <!-- end pages -->
   </div> 
    
   
        <!-- end pager -->
    
    <!-- end results -->  
    <dx:ASPxHiddenField ID="dxhfpricer" runat="server" 
        ClientInstanceName="hfpricer">
    </dx:ASPxHiddenField>
          <!-- end datasource -->

             <dx:ASPxPopupControl ID="dxpopPricer" runat="server" AppearAfter="800" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="popPricer" 
        CloseAction="CloseButton" HeaderText="" Height="100px" Width="100px" 
        PopupAction="None" EnableHotTrack="False" AllowDragging="True" 
        EnableHierarchyRecreation="True" Modal="True">
            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
            </LoadingPanelImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
            <HeaderStyle>
            <Paddings PaddingRight="6px" />
            </HeaderStyle>
        <Windows>
            
            <dx:PopupWindow CloseAction="CloseButton" 
                ContentUrl="~/Popupcontrol/Wbs_Pricer_Print.aspx" Height="600px" 
                Width="600px" Name="printpricer" HeaderText="Print todays quotes" 
                Modal="True">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:PopupWindow>
            
        </Windows>
     </dx:ASPxPopupControl>
 
</asp:Content>