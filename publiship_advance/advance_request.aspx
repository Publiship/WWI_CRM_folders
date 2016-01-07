<%@ Page Title="" Language="C#" MasterPageFile="~/WWI_m1.master" AutoEventWireup="true" CodeFile="advance_request.aspx.cs"  Inherits="advance_request" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderM1" Runat="Server">
 
 <script type="text/javascript">
 // <![CDATA[
     function SetMaxLength(memo, maxLength) {
         if (!memo)
             return;
         if (typeof (maxLength) != "undefined" && maxLength >= 0) {
             memo.maxLength = maxLength;
             memo.maxLengthTimerToken = window.setInterval(function() {
                 var text = memo.GetText();
                 if (text && text.length > memo.maxLength)
                 memo.SetText(text.substr(0, memo.maxLength));
             }, 10);
         } else if (memo.maxLengthTimerToken) {
             window.clearInterval(memo.maxLengthTimerToken);
             delete memo.maxLengthTimerToken;
             delete memo.maxLength;
         }
     }
     
     function noenter(s, e) {
         return !(window.event && window.event.keyCode == 13);
     }
            
    function onbuttonNext(buttonIndex) {
        //user = verify_user();
        //if (user != 'You are not logged in') {
        callorder.PerformCallback(buttonIndex);
        var indexTab = (pageorder.GetActiveTab()).index;
        pageorder.SetActiveTab(pageorder.GetTab(indexTab + 1));

        switch (buttonIndex) {
            case 1:
                grdcurrentcartons.PerformCallback();
                break;
        } 
        //}
        //else {
        //    var window = popWindows.GetWindowByName('loginform');
        //    popWindows.ShowWindow(window);
         //}
    }
    
    function onbuttonTab(tabIndex) {
        //user = verify_user();
        //if (user != 'You are not logged in') {

        //var indexTab = (pageorder.GetActiveTab()).index;
        //callorder.PerformCallback(buttonIndex);
        pageorder.SetActiveTab(pageorder.GetTab(tabIndex));
        //}
        //else {
        //    var window = popWindows.GetWindowByName('loginform');
        //    popWindows.ShowWindow(window);
        //}
    }

    function onbuttonNew(buttonIndex) {
        //user = verify_user();
        //if (user != 'You are not logged in') {
        //var indexTab = (pageorder.GetActiveTab()).index;
        //alert(buttonIndex);
         
        switch(buttonIndex)
        {
            case 3: //new title 
                hforder.Set('titleid', '');
                cbotitle.SetText('');
                callorder.PerformCallback(buttonIndex);
                pageorder.SetActiveTab(pageorder.GetTab(1));
                break;
            case 4: //new order
                hforder.Set('orderid', '');
                hforder.Set('titleid', '');
                cbotitle.SetText('');
                txtpayee.SetText('');
                memaddress.SetText('');
                txtfao.SetText('');
                callorder.PerformCallback(buttonIndex);
                pageorder.SetActiveTab(pageorder.GetTab(0));
                break;
            case 5: //reprint labels deprecated need to do full postback
                //callorder.PerformCallback(buttonIndex);
                break;
        }  
        //}
        //else {
        //    var window = popWindows.GetWindowByName('loginform');
        //    popWindows.ShowWindow(window);
        //}
    }

    function onTitleChanged(cbo) {
        callorder.PerformCallback(1); //pass title to to carton caption
        //lblcartons.SetText('Enter carton details for ' + cbo.GetText().toString());
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

    function oncartonadd(buttonIndex) {
        callorder.PerformCallback(buttonIndex);
        if (!grdcurrentcartons.InCallback()) {
            grdcurrentcartons.PerformCallback(' ');
        }
    }
    function oncartonadd_deprecated(s, e) {
      cbcarton.PerformCallback('new');
    }


    function setaddresspanel(s, e) {

        if (!gridAddress.InCallback()) {
                var window = popAdvanceEdit.GetWindowByName('addresslist');
                popAdvanceEdit.ShowWindow(window);
                gridAddress.PerformCallback(' ');
        }
    }

    function onAddressDoubleclick(s, e) {
        gridAddress.GetRowValues(e.visibleIndex, 'DeliveryAddress;DestinationCountry', OnGetRowValues);
        closepopupWindow('addresslist');
    }
    function OnGetRowValues(values) {
        var strr = values.toString();
        var strn = strr.split(','); //should be address,country
        cbocountry.SetValue(strn.pop()); //pop removes the last element in array
        memaddress.SetText(strn); //once popped return the rest of address as a string
    }

    function closepopupWindow(value) {
        window.popAdvanceEdit.HideWindow(window.popAdvanceEdit.GetWindowByName(value));
    }
      // ]]>
  </script>

    <div class="formcenter580">
                  
        <!-- tab pages showtabs=false -->
        <dx:ASPxPageControl ID="dxpageorder" ClientInstanceName="pageorder" 
            runat="server" Width="580px" 
            ActiveTabIndex="0" Height="475px" 
            CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" 
            SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px" 
            ShowTabs="False" TabIndex="-1">
            <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
            </LoadingPanelImage>
            <ContentStyle>
                <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
            </ContentStyle>
            <TabPages>
                <dx:TabPage Text="Order details" Name="taborder">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl0" runat="server">
                            <!-- header -->
                            <div class="row minheight45 outline_bottom bg_blue">
                                <span class="left195 pad">
                                <dx:ASPxLabel ID="dxlbltaborder" ClientInstanceName="lbltaborder" 
                                    runat="server" Text="Enter your order details" Font-Size="Medium">
                                </dx:ASPxLabel>
                                </span>
                                <span class="left pad">
                                    <a href="Publiship_Advance.pdf" target="_blank">
                                    <img src="../Images/icons/24x24/help.png" border="0" align ="middle" 
                                    alt="Open user guide in new window"/> View the user guide (opens in new window) </a>
                                    </span>
                            </div>
                            <div>
                            <dl class="dl2">
                            <!-- order no -->
                            <dt><dx:ASPxLabel ID="dxlblorderno1" ClientInstanceName="lblorderno1"  
                                     runat="server" Text="Order number" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd><dx:ASPxLabel ID="dxlblorderno2" ClientInstanceName="lblorderno2" 
                                    runat="server" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" Width="250px" TabIndex="-1">
                                </dx:ASPxLabel>
                             </dd>
                             <!-- payee -->
                             <dt><dx:ASPxLabel ID="dxlblpayee" ClientInstanceName="lblpayee" runat="server" 
                                     Text="Payee" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxTextBox ID="dxtxtpayee" ClientInstanceName="txtpayee" runat="server" 
                                    Width="250px" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    NullText="Enter payee name here" MaxLength="50" TabIndex="1">
                                     <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Right">
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value"  />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!-- delivery address -->
                            <dt>
                            <dx:ASPxLabel ID="dxlbladdress1" ClientInstanceName="lbladdress1" runat="server" 
                                     Text="Delivery address"     
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                            </dx:ASPxLabel>
                            </dt>
                             <dd>
                                <dx:ASPxLabel ID="dxlbladdress2" ClientInstanceName="lbladdress2" runat="server" 
                                     Text="Enter a new address or click 'find address'" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dd>
                            <dt>
                            <!-- find previous addresses used -->
                            <dx:ASPxButton ID="dxbtnfindaddress" ClientInstanceName="dtnfindaddress" 
                                    runat="server" Text="Find address" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Width="100px" AutoPostBack="False" CausesValidation="False" 
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="setaddresspanel" />
                                </dx:ASPxButton>
                            </dt>
                            <dd>
                                <dx:ASPxMemo ID="dxmemaddress" ClientInstanceName="memaddress" runat="server" 
                                    Height="200" Width="250px" TabIndex="1" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" NullText="Enter delivery address" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <ValidationSettings EnableCustomValidation="True">
                                        <RegularExpression ErrorText="200 characters limit" 
                                            ValidationExpression="^[\s\S]{0,200}$" />
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </dd>
                            <!-- destination country -->
                            <dt><dx:ASPxLabel ID="dxlblcountry" ClientInstanceName="lblcountry" runat="server" 
                                     Text="Destination country" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                            <dx:ASPxComboBox ID="dxcbocountry" ClientInstanceName="cbocountry" runat="server" 
                                ValueType="System.String" MaxLength="50"
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" TabIndex="3" 
                                SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                IncrementalFilteringMode="StartsWith" TextField="name" ValueField="name" 
                                    DropDownStyle="DropDown" Width="250px">
                                <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                </LoadingPanelImage>
                                <ValidationSettings>
                                    <RequiredField ErrorText="Required" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                            </dd>
                             <!-- cargo ready -->
                            <dt><dx:ASPxLabel ID="dxlblcaroready" ClientInstanceName="lblcargoready" runat="server" 
                                     Text="Cargo ready date" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxDateEdit ID="dxdtcargoready" ClientInstanceName="dtcargoready" 
                                    runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="4"
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Width="125px">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </dx:ASPxDateEdit>
                            </dd>
                            <!-- fao -->
                            <dt><dx:ASPxLabel ID="dxlblfao" ClientInstanceName="lblfao" runat="server" 
                                     Text="Attention" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                             <dx:ASPxTextBox ID="dxtxtfao" ClientInstanceName="txtfao" 
                                    runat="server" Width="250px" Text="" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    NullText="For the attention of" TabIndex="5" MaxLength="100">
                                     <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Right">
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dd>
                            <!-- sender -->
                            <dt><dx:ASPxLabel ID="dxlblsender1" ClientInstanceName="lblsender1" 
                                    ClientVisible="true"  runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" Text="Sender" TabIndex="-1">
                                </dx:ASPxLabel>
                             </dt>
                             <dd><dx:ASPxLabel ID="dxlblsender2" ClientInstanceName="lblsender2" 
                                     ClientVisible="true" runat="server" Text="" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dd>
                        </dl>  
                        </div>
                        <!-- commands -->
                        <div class="row minheight45">
                            <span class="left195 pad">
                                   <dx:ASPxButton ID="dxbtnorder" ClientInstanceName="btnorder" runat="server"
                                    Text="Next: Add a title" 
                                                         CausesValidation="False" 
                                                         CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                         CssPostfix="Office2003Blue" 
                                                         SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                                         Width="120px" TabIndex="6" Height="22px" 
                                OnClick="dxbtnorder_Click" UseSubmitBehavior="False">
                                </dx:ASPxButton>
                            </span>
                        </div>
                        <!-- hidden info -->
                        <div class="row">
                            <span class="left120">
                            <dx:ASPxLabel ID="dxlblprinter1" ClientInstanceName="lblprinter1" 
                                    ClientVisible="false"  runat="server" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel></span>
                            <span class="left120"><dx:ASPxLabel ID="dxlblprinter2" 
                                ClientInstanceName="lblprinter2" ClientVisible="false" runat="server" Text="" 
                                TabIndex="-1">
                                </dx:ASPxLabel></span>
                            <span class="left120">
                            <dx:ASPxLabel ID="dxlbltitles1" ClientInstanceName="lbltitles1" 
                                    ClientVisible="false" runat="server" Text="" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel></span>
                            <span class="left120">
                            <dx:ASPxLabel ID="dxlblcartons1" 
                                ClientInstanceName="lblcartons1" ClientVisible="false" runat="server" Text="" 
                                TabIndex="-1">
                                </dx:ASPxLabel></span>
                        </div>
                        </dx:ContentControl>
                    </ContentCollection> 
                </dx:TabPage> 
                <dx:TabPage Text="Title" Name="tabtitle">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <div class="row minheight45 outline_bottom bg_blue">
                                <span class="left250 pad">
                                <dx:ASPxLabel ID="dxlbltabtitle" ClientInstanceName="lbltabtitle" 
                                    runat="server" Text="Enter a title or description" Font-Size="Medium">
                                </dx:ASPxLabel>
                                </span>
                            </div>
                            <div>
                            <!-- title combo -->
                             <dl class="dl2">
                            <dt><dx:ASPxLabel ID="dxlbltitle" ClientInstanceName="lbltitle"  runat="server" 
                                    Text="Title" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" TabIndex="-1">
                                </dx:ASPxLabel>
                            </dt>
                            <dd>
                                <dx:ASPxComboBox ID="dxcbotitle" ClientInstanceName="cbotitle" runat="server" 
                                    DropDownStyle="DropDown" 
                                    OnItemRequestedByValue="dxcbotitle_ItemRequestedByValue" 
                                    
                                    OnItemsRequestedByFilterCondition="dxcbotitle_ItemsRequestedByFilterCondition" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    Width="300px" TabIndex="7" >
                                    <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
                                    </LoadingPanelImage>
                                    <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Right">
                                        <RegularExpression ValidationExpression="^[0-9a-zA-Z''-'\s]+$" ErrorText="Invalid value" />
                                    </ValidationSettings> 
                                </dx:ASPxComboBox> 
                            </dd>
                            </dl>
                            </div> 
                            <!-- commands -->
                            <div class="row minheight45">
                                <span class="left195 pad">
                                    <dx:ASPxButton ID="dxbtnupdateorder" runat="server" ClientInstanceName="btnupdateorder" 
                                      Text="Previous: Change order" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Width="155px" AutoPostBack="False" CausesValidation="False" 
                                    UseSubmitBehavior="False" TabIndex="8">
                                        <ClientSideEvents Click="function(s, e) {
	                                        onbuttonTab(0);
                                        }" />
                                     </dx:ASPxButton>
                                </span> 
                                <span class="right195 pad">
                                    <dx:ASPxButton ID="dxbtntitle" ClientInstanceName="btntitle" 
                                                    runat="server" Text="Next: Add Cartons" CausesValidation="False" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" TabIndex="9" 
                                    Width="141px" OnClick="dxbtntitle_Click" UseSubmitBehavior="False">
                                     </dx:ASPxButton>
                                </span> 
                            </div>  
                        </dx:ContentControl> 
                     </ContentCollection> 
                </dx:TabPage>
                <dx:TabPage Text="Cartons" Name="tabcartons">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl2" runat="server">
                            <div class="row minheight45 outline_bottom bg_blue">
                                <span class="left470 pad">
                                <dx:ASPxLabel ID="dxlbltabcartons" ClientInstanceName="lbltabcartons" runat="server" Text="Cartons" Font-Size="Medium">
                                </dx:ASPxLabel>
                                </span> 
                            </div>
                            <div>
                            <!-- carton details -->
                            <div class="row minheight45">
                                 <dx:ASPxCallbackPanel ID="dxpnltitle" runat="server" 
                                    OnCallback="dxcallorder_Callback">
                            <PanelCollection>
                            <dx:PanelContent>
                                   <span class="left470">
                                 <dx:ASPxLabel ID="dxlblcartons" runat="server" TabIndex="-1" ClientInstanceName="lblcartons" 
                                        Text="Enter carton details">
                                    </dx:ASPxLabel>
                                </span> 
                               </dx:PanelContent> 
                               </PanelCollection> 
                               </dx:ASPxCallbackPanel>
                            </div>
                            <div class="row">
                                <table style="width: 560px; table-layout: fixed;">
                                    <!-- headings row -->
                                    <tr>
                                    <td style="padding:5px 0px 5px 2px;">
                                        <asp:Label ID="lblcol1" runat="server" TabIndex="-1" Text="Enter length"></asp:Label></td> 
                                    <td style="padding:5px 0px 5px 2px;">
                                        <asp:Label ID="lblcol2" runat="server" TabIndex="-1" Text="Enter width"></asp:Label></td> 
                                    <td style="padding:5px 0px 5px 2px;">
                                        <asp:Label ID="lblcol3" runat="server" TabIndex="-1" Text="Enter height"></asp:Label></td> 
                                    <td style="padding:5px 0px 5px 2px;">
                                        <asp:Label ID="lblcol4" runat="server" TabIndex="-1" Text="Enter weight"></asp:Label></td> 
                                    <td style="padding:5px 0px 5px 2px;">
                                        <asp:Label ID="lblcol5" runat="server" TabIndex="-1" Text="No. of cartons"></asp:Label></td> 
                                    <td style="padding:5px 0px 5px 2px;"></td> 
                                    </tr>
                                    <tr>
                                    <!-- length -->
                                    <td style="padding:20px 5px 5px 0px;">
                                     <dx:ASPxTextBox ID="dxtxtlength" ClientInstanceName="txtlength" 
                                    runat="server" Width="80px" NullText="Length" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" TabIndex="10">
                                    <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Top">
                                        <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number" />
                                        <RequiredField IsRequired="true" ErrorText="Number" /> 
                                    </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    </td>
                                    <!-- width -->
                                    <td style="padding:20px 5px 5px 0px;">
                                       <dx:ASPxTextBox ID="dxtxtwidth" ClientInstanceName="txtwidth" runat="server" 
                                            Width="80px" NullText="Width" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            TabIndex="11">
                                          <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Top">
                                                <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number" />
                                                <RequiredField IsRequired="true" ErrorText="Number" /> 
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <!-- height -->
                                    <td style="padding:20px 5px 5px 0px;">
                                     <dx:ASPxTextBox ID="dxtxtheight" ClientInstanceName="txtheight" 
                                            runat="server" Width="80px" NullText="Height" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            TabIndex="12">
                                            <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Top">
                                                <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number" />
                                                <RequiredField IsRequired="true" ErrorText="Number" /> 
                                            </ValidationSettings>
                                            </dx:ASPxTextBox>
                                    </td>
                                    <!-- weight -->
                                    <td style="padding:20px 5px 5px 0px;">
                                      <dx:ASPxTextBox ID="dxtxtweight" ClientInstanceName="txtweight" 
                                            runat="server" Width="80px" NullText="Weight" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            TabIndex="13">
                                         <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Top">
                                                <RegularExpression ValidationExpression="^[0-9.]+$" ErrorText="Number" />
                                                <RequiredField IsRequired="true" ErrorText="Number" /> 
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <!-- copies -->
                                    <td style="padding:20px 5px 5px 0px;">
                                        <dx:ASPxTextBox ID="dxtxtcount" ClientInstanceName="txtcount" runat="server" 
                                            Width="80px" NullText="Cartons" 
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" 
                                            SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                            TabIndex="14">
                                          <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Top">
                                                <RegularExpression ValidationExpression="^[0-9]+$" ErrorText="Number" />
                                                <RequiredField IsRequired="true" ErrorText="Number" /> 
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <!-- add carton button -->
                                    <td style="padding: 20px 0px 5px 5px; text-align: right">
                                      <dx:ASPxButton ID="dxbtnadd" ClientInstanceName="btnadd" runat="server" 
                                        Text="Add" AutoPostBack="False" CausesValidation="False" 
                                        CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue" 
                                        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" Width="80px" 
                                            UseSubmitBehavior="False" TabIndex="15">
                                        <ClientSideEvents Click="function(s, e) {
	                                        oncartonadd(2);
                                        }" />
                                        </dx:ASPxButton>
                                     </td>
                                     </tr>
                                </table>
                            </div>
                            <!-- summary grid for current cartons -->                           
                            <div>
                                <dx:ASPxGridView ID="dxgrdcurrentcartons" ClientInstanceName="grdcurrentcartons" 
                                    runat="server" AutoGenerateColumns="False" 
                                    Width="560px" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    OnCustomCallback="dxgrdcurrentcartons_CustomCallback">
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options">
                                            <EditButton Visible="True">
                                            </EditButton>
                                            <DeleteButton Visible="True">
                                            </DeleteButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="PubAdvCartonID" VisibleIndex="5" 
                                            Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PATitleID" VisibleIndex="6" 
                                            Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CartonLength" VisibleIndex="1" 
                                            Width="90px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CartonWidth" VisibleIndex="2" 
                                            Width="90px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CartonHeight" VisibleIndex="3" 
                                            Width="90px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CartonWeight" VisibleIndex="4" 
                                            Width="90px">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowTitlePanel="true" />
                                    <SettingsPager Position="TopAndBottom" PageSize="50">
                                    </SettingsPager>
                                    <SettingsText Title="Current cartons" />
                                    <SettingsEditing Mode="Inline" />
                                    <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                        <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                        </LoadingPanelOnStatusBar>
                                        <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                        </LoadingPanel>
                                    </Images>
                                    <ImagesFilterControl>
                                        <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                        </LoadingPanel>
                                    </ImagesFilterControl>
                                    <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                        CssPostfix="Office2003Blue">
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                        <TitlePanel Paddings-Padding="8px">
<Paddings Padding="8px"></Paddings>
                                        </TitlePanel>
                                        <LoadingPanel ImageSpacing="10px">
                                        </LoadingPanel>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </div>
                            <!-- end grid -->
                            <div class="row minheight45">
                                 <span class="left pad">
                                    <dx:ASPxButton ID="dxbtnupdatetitle" runat="server" ClientInstanceName="btnupdatetitle" 
                                      Text="Previous: Change title" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Width="145px" AutoPostBack="False" CausesValidation="False" 
                                     UseSubmitBehavior="False" TabIndex="16">
                                        <ClientSideEvents Click="function(s, e) {
	                                        onbuttonTab(1);
                                        }" />
                                     </dx:ASPxButton>
                                </span> 
                                <span class="right pad">
                                    <dx:ASPxButton ID="dxbtnsummary" runat="server" ClientInstanceName="btnsummary" 
                                         onclick="dxbtnsummary_Click" Text="Next: View summary" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Width="145px" UseSubmitBehavior="False" TabIndex="17">
                                     </dx:ASPxButton>
                                </span>
                               
                            </div>
                        </div>
                        </dx:ContentControl> 
                     </ContentCollection> 
                </dx:TabPage> 
                <dx:TabPage Text="Order summary" Name="tabsummary">
                    <ContentCollection>
                        <dx:ContentControl>
                             <div class="row minheight45 outline_bottom bg_blue">
                                <span class="left470 pad">
                                <dx:ASPxLabel ID="dxlbltabsummary" ClientInstanceName="lbltabsummary" 
                                     runat="server" Text="Summary" Font-Size="Medium" TabIndex="-1">
                                </dx:ASPxLabel>
                                </span> 
                            </div>
                            <!-- spacer -->
                            <div class="row"> 
                                 <dl class="dl2">
                                    <!-- payee -->
                                    <dt><dx:ASPxLabel ID="dxlblsumm1" ClientInstanceName="lblsumm1"  
                                             runat="server" Text="Payee" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" TabIndex="-1">
                                        </dx:ASPxLabel>
                                    </dt>
                                    <dd><dx:ASPxLabel ID="dxlblsumm2" ClientInstanceName="lblsumm2" 
                                            runat="server" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Width="300px" TabIndex="-1">
                                        </dx:ASPxLabel>
                                     </dd>
                                     <!-- delivery address -->
                                     <dt><dx:ASPxLabel ID="dxlblsumm3" ClientInstanceName="lblsumm3"  
                                             runat="server" Text="Delivery to" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" TabIndex="-1">
                                        </dx:ASPxLabel>
                                    </dt>
                                    <dd><dx:ASPxLabel ID="dxlblsumm4" ClientInstanceName="lblsumm4" 
                                            runat="server" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Width="300px" Wrap="True" TabIndex="-1">
                                        </dx:ASPxLabel>
                                     </dd>
                                     <!-- country -->
                                      <dt><dx:ASPxLabel ID="dxlblsumm7" ClientInstanceName="lblsumm7"  
                                             runat="server" Text="Country" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" TabIndex="-1">
                                        </dx:ASPxLabel>
                                    </dt>
                                    <dd><dx:ASPxLabel ID="dxlblsumm8" ClientInstanceName="lblsumm8" 
                                            runat="server" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Width="300px" Wrap="True" TabIndex="-1">
                                        </dx:ASPxLabel>
                                     </dd>
                                     <!-- fao -->
                                     <dt><dx:ASPxLabel ID="dxlblsumm5" ClientInstanceName="lblsumm5"  
                                             runat="server" Text="Atttention" 
                                             CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                             CssPostfix="Office2003Blue" TabIndex="-1">
                                        </dx:ASPxLabel>
                                    </dt>
                                    <dd><dx:ASPxLabel ID="dxlblsumm6" ClientInstanceName="lblsumm6" 
                                            runat="server" Text="" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" Width="300px" TabIndex="-1">
                                        </dx:ASPxLabel>
                                     </dd>
                             </dl>
                             </div> 
                            <div>
                            <!-- titles in parent, cartons in detail grid -->
                            <dx:ASPxGridView ID="dxgrdtitles" ClientInstanceName="grdsummary" 
                                runat="server" AutoGenerateColumns="False"
                                Width="550px" 
                                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue" DataSourceID="objdsTitles" 
                                KeyFieldName="PATitleID" 
                                OnDetailRowGetButtonVisibility="dxgrdtitles_DetailRowGetButtonVisibility"
                                OnRowInserting="dxgrdtitles_RowInserting">
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options" >
                                        <EditButton Visible="True">
                                        </EditButton>
                                        <NewButton Visible="True">
                                        </NewButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="PATitleID" VisibleIndex="2" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PAOrderID" VisibleIndex="3" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Title" VisibleIndex="1" Width="350px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" 
                                    AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                <SettingsPager Position="TopAndBottom" PageSize="50">
                                </SettingsPager>
                                <SettingsText Title="Titles and cartons" />
                                <SettingsEditing Mode="Inline" />
                                <Settings ShowGroupButtons="False" ShowTitlePanel="true" />
                                <SettingsDetail ExportMode="All" ShowDetailRow="True" 
                                    ShowDetailButtons="False" />
                                <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                    <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue">
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                    <TitlePanel Paddings-Padding="8px">
<Paddings Padding="8px"></Paddings>
                                    </TitlePanel>
                                    <LoadingPanel ImageSpacing="10px">
                                    </LoadingPanel>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                               <Templates>
                                    <DetailRow>
                                          <dx:ASPxGridView ID="dxgrdcartons" ClientInstanceName="grdcartons" runat="server" AutoGenerateColumns="False"  
                                            KeyFieldName="PubAdvCartonID"
                                            OnDetailRowGetButtonVisibility="dxgrdcartons_DetailRowGetButtonVisibility"
                                            OnBeforePerformDataSelect="grdcartons_BeforePerformDataSelect" OnRowInserting="dxgrdcartons_RowInserting"
                                            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                            CssPostfix="Office2003Blue" DataSourceID="objdsCartons">
                                            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                                CssPostfix="Office2003Blue">
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                            </Styles>
                                            <ImagesFilterControl>
                                                <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                                </LoadingPanel>
                                            </ImagesFilterControl>
                                            <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                </LoadingPanelOnStatusBar>
                                                <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                </LoadingPanel>
                                            </Images>
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0" Caption="Options">
                                                    <EditButton Visible="True">
                                                    </EditButton>
                                                    <NewButton Visible="True">
                                                    </NewButton>
                                                    <DeleteButton Visible="True">
                                                    </DeleteButton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="PubAdvCartonID" VisibleIndex="5" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PATitleID" VisibleIndex="6" Visible="false" EditFormSettings-Visible="False" ReadOnly="true" Width="10px">
<EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonLength" VisibleIndex="0" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonWidth" VisibleIndex="1" Width="85px" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonHeight" VisibleIndex="2" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CartonWeight" VisibleIndex="3" Width="85px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                              <SettingsBehavior AllowGroup="False" AutoExpandAllGroups="True" />
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsDetail ExportMode="All" ShowDetailRow="True" />
                                                <SettingsPager Position="TopAndBottom" PageSize="50">
                                            </SettingsPager>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>      
                                    </DetailRow> 
                               </Templates> 
                            </dx:ASPxGridView>
                            </div> 
                            <!-- previous, finish, add new title, cancel -->
                            <div class="row minheight45">
                                <span class="left195 pad">
                                   <dx:ASPxButton ID="dxbtnupdatecartons" runat="server" ClientInstanceName="btnupdatecartons" 
                                      Text="Previous: Change last cartons" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Width="185px" AutoPostBack="False" CausesValidation="False" Wrap="False" 
                                    UseSubmitBehavior="False" TabIndex="18">
                                        <ClientSideEvents Click="function(s, e) {
	                                        grdcurrentcartons.PerformCallback();
	                                        onbuttonTab(2);
                                        }" />
                                     </dx:ASPxButton>
                                </span>
                               
                                <span class="left120 pad">
                                    <dx:ASPxButton ID="dxbtnaddtitle" ClientInstanceName="btnaddtitle" 
                                        runat="server" Text="Add a title" CausesValidation="false"  
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    AutoPostBack="False" UseSubmitBehavior="False" TabIndex="16">
                                    <ClientSideEvents Click="function(s, e) {
	                                        onbuttonNew(3);
                                        }" />
                                    </dx:ASPxButton>
                                </span>   
             
                                <span class="right120 pad">
                                    <dx:ASPxButton ID="dxbtnsave" ClientInstanceName="btnsave" 
                                        runat="server" Text="Finish" onclick="dxbtnsave_Click" Width="100px" 
                                    AutoPostBack="true" CausesValidation="false" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                    UseSubmitBehavior="False" TabIndex="19">
                                    </dx:ASPxButton>
                                </span>   
                                </div>   
                            <!-- end commands --> 
                        </dx:ContentControl> 
                    </ContentCollection> 
                </dx:TabPage> 
                <dx:TabPage Text="Error!" Name="taberror">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl7" runat="server">
                         <!-- alert message -->
                         <div class="alert">
                            <dx:ASPxLabel ID="dxlblerr" ClientInstanceName="lblerr" runat="server" 
                            Text="Error Message" TabIndex="-1">
                          </dx:ASPxLabel>
                         
                        </div> 
                        <!-- commands -->
                        <div class="row minheight45">
                            <span class="right120 pad"> 
                             <dx:ASPxButton ID="dxbtnback1" runat="server" AutoPostBack="False" 
                                 ClientInstanceName="btnback1" 
                                 CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                 CssPostfix="Office2003Blue" 
                                 SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                 Text="Try again" CausesValidation="False" UseSubmitBehavior="False">
                                 <ClientSideEvents Click="function(s, e) {
	                                onbuttonTab(0);
                                }" />
                             </dx:ASPxButton>
                             </span>
                        </div>
                     </dx:ContentControl>
                </ContentCollection> 
                </dx:TabPage>  
                <dx:TabPage Text="Response" Name="tabresponse">
                <ContentCollection>
                     <dx:ContentControl ID="ContentControl3" runat="server">
                         <!-- mesage -->
                         <div class="info">
                            <dx:ASPxLabel ID="dxlblmsg" ClientInstanceName="lblmsg" runat="server" 
                            Text="Message">
                          </dx:ASPxLabel>
                         
                        </div> 
                        <!-- commands -->
                        <div class="row minheight45">
                           
                            <span class="left120 pad">
                                   <dx:ASPxButton ID="dxbtnpdf" runat="server" 
                                     ClientInstanceName="btnpdf" 
                                     CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" 
                                     CssPostfix="Office2003Olive" 
                                     SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css" 
                                     Text="Print labels" AutoPostBack="true" CausesValidation="false"  
                                UseSubmitBehavior="False" OnClick="dxbtnpdf_Click" Width="100px">
                                 </dx:ASPxButton>
                              
                            </span>  
                             <span class="left120 pad">
                                   <dx:ASPxButton ID="dxbtnreview" runat="server" 
                                     ClientInstanceName="btnreview" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" 
                                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Text="Back" AutoPostBack="false" CausesValidation="false"  
                                    UseSubmitBehavior="False" Width="100px" ClientEnabled="False" 
                                ClientVisible="False">
                                    <ClientSideEvents Click="function(s, e) {
	                                onbuttonTab(3);
                                }" />
                                 </dx:ASPxButton>
                              
                            </span>  
                            <span class="left120 pad"> 
                             <dx:ASPxButton ID="dxbtnaddorder" runat="server" 
                                     ClientInstanceName="bntaddorder" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" 
                                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Text="New order" CausesValidation="False" UseSubmitBehavior="False" 
                                AutoPostBack="False" Width="100px" OnClick="dxbtnaddorder_Click">
                                 </dx:ASPxButton>
                             </span> 
                             <span class="left120 pad">
                                   <dx:ASPxButton ID="dxbtnexit" runat="server" 
                                     ClientInstanceName="btnexit" 
                                     CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                     CssPostfix="Office2003Blue" 
                                     SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
                                     Text="Exit" AutoPostBack="true" CausesValidation="False" UseSubmitBehavior="False" 
                                     OnClick="dxbtnexit_Click" Width="100px">
                                 </dx:ASPxButton>
                            </span>
                        </div>
                     </dx:ContentControl>
                </ContentCollection> 
                </dx:TabPage>  
            </TabPages>
            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
        </dx:ASPxPageControl>
        <!-- end tab pages -->
        <!-- prevent postback on enter key -->  
        <input type="text" name="enterkeybugfix" style="display: none;" />
    </div>

    <!-- callback for tracking orderid, titleid, etc -->
       <dx:ASPxCallbackPanel ID="dxcallorder" ClientInstanceName="callorder"  Width="200px"
            runat="server" oncallback="dxcallorder_Callback" 
            CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
            CssPostfix="Office2003Blue" ShowLoadingPanel="False">
            <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
            </LoadingPanelImage>
            <PanelCollection>
            <dx:PanelContent>
        <dx:ASPxHiddenField ID="dxhforder" ClientInstanceName="hforder" runat="server">
        </dx:ASPxHiddenField>
        </dx:PanelContent> 
        </PanelCollection> 
        </dx:ASPxCallbackPanel> 
        <!-- end callback -->  
 
    <div>   
    
                
      <asp:ObjectDataSource ID="objdsTitles" runat="server" 
                    DeleteMethod="TitleDelete" InsertMethod="TitleInsert" 
                    SelectMethod="TitlesFetchByPaOrderID" 
                    TypeName="DAL.Logistics.PublishipAdvanceTitleTableCustomcontroller" 
                    UpdateMethod="TitleUpdate" onselecting="objdsTitles_Selecting">
                    <DeleteParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="PAOrderID" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Ts" Type="Object" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                
        <asp:ObjectDataSource ID="objdsCartons" runat="server" 
                    DeleteMethod="CartonDelete" InsertMethod="CartonInsert" 
                    SelectMethod="CartonsFetchByPATitleID" 
                    TypeName="DAL.Logistics.PublishipAdvanceCartonTableCustomcontroller" 
                    UpdateMethod="CartonUpdate">
                    <DeleteParameters>
                        <asp:Parameter Name="PubAdvCartonID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="CartonLength" Type="Decimal" />
                        <asp:Parameter Name="CartonWidth" Type="Decimal" />
                        <asp:Parameter Name="CartonHeight" Type="Decimal" />
                        <asp:Parameter Name="CartonWeight" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="PubAdvCartonID" Type="Int32" />
                        <asp:Parameter Name="PATitleID" Type="Int32" />
                        <asp:Parameter Name="CartonLength" Type="Decimal" />
                        <asp:Parameter Name="CartonWidth" Type="Decimal" />
                        <asp:Parameter Name="CartonHeight" Type="Decimal" />
                        <asp:Parameter Name="CartonWeight" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>

  
                            
       <dx:ASPxGridViewExporter ID="dxgrdexport" runat="server" 
        GridViewID="dxgrdtitles">
    </dx:ASPxGridViewExporter>
  </div>
  
    <dx:ASPxPopupControl ID="dxpopAdvanceEdit" ClientInstanceName="popAdvanceEdit" 
        runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
        CssPostfix="Office2003Blue" EnableHotTrack="False" 
        SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css" 
        AllowDragging="True" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" PopupAction="None" 
        PopupElementID="dxtxtpayee">
        <LoadingPanelImage Url="~/App_Themes/Office2003Blue/Web/Loading.gif">
        </LoadingPanelImage>
        <HeaderStyle>
        <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
        <Windows>
            <dx:PopupWindow Name="addresslist" 
                HeaderText="Double-click to select an address"  
                Modal="True" Width="500px" Height="500px" >
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <div style="height: 445px">
                        <dx:ASPxGridView ID="dxgridAddress" ClientInstanceName="gridAddress" 
                            runat="server" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                            CssPostfix="Office2003Blue" KeyFieldName="OrderID" Width="480px" >
                             <ClientSideEvents RowDblClick="function(s, e) {onAddressDoubleclick(s,e)}" />
                            <Columns>
                                <dx:GridViewDataMemoColumn Caption="Address" FieldName="DeliveryAddress">
                                </dx:GridViewDataMemoColumn> 
                                <dx:GridViewDataTextColumn Caption="Country" FieldName="DestinationCountry" 
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                            </Columns> 
                             <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                 EnableRowHotTrack="True" />
                             <SettingsPager PageSize="5"></SettingsPager> 
                             <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" GridLines="Horizontal" />
                            <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                </LoadingPanelOnStatusBar>
                                <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                </LoadingPanel>
                            </Images>
                            <ImagesFilterControl>
                                <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                </LoadingPanel>
                            </ImagesFilterControl>
                            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                CssPostfix="Office2003Blue">
                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                </Header>
                                <LoadingPanel ImageSpacing="10px">
                                </LoadingPanel>
                            </Styles>
                            <StylesEditors>
                                <ProgressBar Height="25px">
                                </ProgressBar>
                            </StylesEditors>
                        </dx:ASPxGridView>
                        </div>
                        <div class="row pad_bottom">
                            <div class="right">
                                <dx:ASPxButton ID="dxbtnCloseAddress" ClientInstanceName="btnCloseAddress" 
                                    AutoPostBack="false" UseSubmitBehavior="false" runat="server" Text="Cancel" 
                                    CausesValidation="False" 
                                    CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" 
                                    CssPostfix="Office2003Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                     <ClientSideEvents Click="function(s, e) {
	                                               closepopupWindow('addresslist'); }" />
                                </dx:ASPxButton>
                            </div> 
                        </div>
                    </dx:PopupControlContentControl> 
                </ContentCollection>
            </dx:PopupWindow> 
        </Windows>
    </dx:ASPxPopupControl>
      
 
   </asp:Content>

