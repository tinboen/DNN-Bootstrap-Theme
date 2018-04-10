<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewTheme.ascx.cs" Inherits="DBH.BootstrapTheme.ViewTheme" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>

<ul id="NavigationBar" runat="server"></ul>

<div class="container-fluid">
    <asp:RadioButtonList ID="optTheme" CssClass="dnnFormRadioButtons" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="optTheme_SelectedIndexChanged" />
<%--    <div class="dnnFormMessage dnnFormInfo">
        <dnn:label id="plTheme" controlname="optTheme" runat="server" />
    </div>
    <div class="dnnFormMessage dnnFormInfo">
        <asp:Label ID="lblIntro" runat="server" resourceKey="lblIntro" />
    </div>--%>
</div>

<div class="container-fluid">
    <asp:Localize id="ctlContent" runat="server" />
</div>
