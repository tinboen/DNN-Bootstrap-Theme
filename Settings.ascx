<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DBH.BootstrapTheme.Settings" %>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->
  

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:label id="plTheme" controlname="optTheme" runat="server" />
            <asp:RadioButtonList ID="optTheme" CssClass="dnnFormRadioButtons" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" />
        </div>
    </fieldset>



