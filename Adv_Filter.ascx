<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adv_Filter.ascx.cs" Inherits="Adv_Filter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<dx:ASPxComboBox ID="ASPxComboBox1" runat="server" 
    CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" 
    DataSourceID="objfieldsdatasource" LoadingPanelImagePosition="Top" 
    ShowShadow="False" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" 
    ValueType="System.String">
    <LoadingPanelImage Url="~/App_Themes/Aqua/Editors/Loading.gif">
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
        </Image>
    </DropDownButton>
    <Columns>
        <dx:ListBoxColumn FieldName="FilterCaption" Name="fcaption" />
        <dx:ListBoxColumn FieldName="ColumnType" Name="coltype" />
        <dx:ListBoxColumn FieldName="FieldName" Name="fname" />
    </Columns>
    <ValidationSettings>
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px" />
        </ErrorFrameStyle>
    </ValidationSettings>
</dx:ASPxComboBox>
<asp:ObjectDataSource ID="objfieldsdatasource" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="FetchAll" 
    TypeName="DAL.Logistics.DbFilterFieldController" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="FilterFieldId" Type="Object" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="FilterFieldId" Type="Int32" />
        <asp:Parameter Name="FilterCaption" Type="String" />
        <asp:Parameter Name="FieldName" Type="String" />
        <asp:Parameter Name="FieldSource" Type="String" />
        <asp:Parameter Name="ColumnType" Type="String" />
        <asp:Parameter Name="CriteriaString" Type="String" />
        <asp:Parameter Name="IsActive" Type="Boolean" />
        <asp:Parameter Name="IsQuickFilter" Type="Boolean" />
        <asp:Parameter Name="CreatedBy" Type="String" />
        <asp:Parameter Name="CreatedOn" Type="DateTime" />
        <asp:Parameter Name="MsTimestamp" Type="Object" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="FilterCaption" Type="String" />
        <asp:Parameter Name="FieldName" Type="String" />
        <asp:Parameter Name="FieldSource" Type="String" />
        <asp:Parameter Name="ColumnType" Type="String" />
        <asp:Parameter Name="CriteriaString" Type="String" />
        <asp:Parameter Name="IsActive" Type="Boolean" />
        <asp:Parameter Name="IsQuickFilter" Type="Boolean" />
        <asp:Parameter Name="CreatedBy" Type="String" />
        <asp:Parameter Name="CreatedOn" Type="DateTime" />
        <asp:Parameter Name="MsTimestamp" Type="Object" />
    </InsertParameters>
</asp:ObjectDataSource>
<dx:ASPxComboBox ID="ASPxComboBox2" runat="server" 
    CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" 
    LoadingPanelImagePosition="Top" ShowShadow="False" 
    SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" ValueType="System.String">
    <Items>
        <dx:ListEditItem Text="exact match" Value="=={0}" />
        <dx:ListEditItem Text="begins with" Value=".Contains({0})" />
        <dx:ListEditItem Text="ends with" Value=".Contains({0})" />
        <dx:ListEditItem Text="less than or equals" Value="&lt;= {0}" />
        <dx:ListEditItem Text="more than or equals" Value="&gt;= {0}" />
        <dx:ListEditItem Text="between" Value="Between {0} and {1}" />
    </Items>
    <LoadingPanelImage Url="~/App_Themes/Aqua/Editors/Loading.gif">
    </LoadingPanelImage>
    <DropDownButton>
        <Image>
            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
        </Image>
    </DropDownButton>
    <Columns>
        <dx:ListBoxColumn Name="critname" />
        <dx:ListBoxColumn Name="critstr" />
    </Columns>
    <ValidationSettings>
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px" />
        </ErrorFrameStyle>
    </ValidationSettings>
</dx:ASPxComboBox>
