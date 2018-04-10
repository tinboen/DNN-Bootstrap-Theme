<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditTheme.ascx.cs" Inherits="DBH.BootstrapTheme.EditTheme" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke.WebControls" %>

<%-- <link href='<%=ResolveUrl("~/bootstrap/css/bootstrap.min.css") %>' rel="stylesheet" type="text/css" /> --%>


<link href='<%=ResolveUrl("~/bootstrap/datatables/css/jquery.dataTables.min.css") %>' rel="stylesheet" type="text/css" />
<script src='<%=ResolveUrl("~/bootstrap/datatables/js/jquery.dataTables.min.js") %>' type="text/javascript"></script>

<div class="container-fluid">
    <div class="dnnFormMessage dnnFormInfo">
        <asp:Label ID="lblIntro" runat="server" resourceKey="lblIntro" />
    </div>

    <div class="dnnForm dnnEditExtension dnnClear" id="dnnEditExtension">
        <fieldset>
            <div class="dnnFormItem">
                <dnn:Label runat="server" ID="lblFileUpload" />
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div>
                <ul class="dnnActions dnnClear">
                    <li><asp:LinkButton runat="server" CssClass="dnnPrimaryAction" ID="cmdSubmit" Resourcekey="cmdSubmit" OnClick="cmdSubmit_Click" /></li>
                    <li><asp:LinkButton runat="server" CssClass="dnnSecondaryAction" ID="cmdCancel" Resourcekey="cmdCancel" OnClick="cmdCancel_Click" CausesValidation="False" /></li>
                </ul>
            </div>
        </fieldset>
    </div>

    <dnn:Label ID="lblExistingTheme" runat="server" />
    <asp:ListBox ID="lstExistingTheme" runat="server" />

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvZip" runat="server" AutoGenerateColumns="false" OnRowCommand="gvZip_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Click to Download">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Bind("Name") %>' CommandName="Download"
                                CommandArgument='<%# Bind("Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

<%--<script>
    $(document).ready(function () {
        $('#<%= gvDepartment.ClientID %>').dataTable({
            "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
            "iDisplayLength": -1,
            "order": [[2, "asc"]],
            stateSave: true,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data));
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance));
            }
        });
    });
</script>--%>

