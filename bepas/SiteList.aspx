<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteList.aspx.cs" Inherits="bepas.WebForm5" %>

<asp:Content ID="SiteList" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">Site List</div>

        <div class="panel-body">
            <asp:GridView ID="gvSiteList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix nowrap" CellSpacing="0" Width="100%"
                GridLines="None" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="siteIdByUser" HeaderText="Site ID" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="siteName" HeaderText="Site Name" />
                    <asp:BoundField DataField="surveyDate" HeaderText="Survey Date" />
                    <asp:BoundField DataField="contactName" HeaderText="Contact Name" />
                    <asp:BoundField DataField="city" HeaderText="City" />
                    <asp:BoundField DataField="stateText" HeaderText="State" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="siteSelectButton" runat="server" CausesValidation="false" CommandName="SelectSite"
                                Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("siteIdByUser")+";"+Eval("siteName")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="cursor-pointer" />
            </asp:GridView>
        </div>
    </div>

    <script src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.12/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.1.0/js/dataTables.responsive.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.1.0/js/responsive.bootstrap.min.js"></script>

    <script>
        $(document).ready(function () {
            $('#<%=gvSiteList.ClientID%>').DataTable({
                "aLengthMenu": [[5, 10, 15, -1], [5, 10, 15, "All"]],
                "pageLength": 5,

                /*
                "columns": [{ "sName": "uid" }, { "sName": "siteIdByUser" }, { "sName": "siteName" },
                            { "sName": "surveyDate" }, { "sName": "contactName" }, { "sName": "city" },
                            { "sName": "stateText" }],

                "aoColumnDefs": [{ "visible": true, 'bSortable': false, "orderable": false, "searchable": false, 'aTargets': [-1] }]
                */
            });
        });

    </script>


</asp:Content>
