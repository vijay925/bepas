<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteExteriorLighting.aspx.cs" Inherits="bepas.SiteExteriorLighting" %>

<asp:Content ID="SiteExteriorLighting" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">Site Level Exterior Lighting</div>

        <div class="panel-body">
            <asp:Panel ID="SuccessPanel" runat="server" CssClass="alert alert-success fade in" Visible="False">
                <i class="fa-lg fa fa-bullhorn"></i>
                <strong>Success!</strong> The Data has been saved.
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            </asp:Panel>

            <!-- site id/name -->
            <div class="form-group">
                <label class="col-md-4 control-label">Site ID / Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="siteId" CssClass="form-control" runat="server" placeholder="Site ID" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="siteName" CssClass="form-control" runat="server" placeholder="Site Name" ReadOnly="true"></asp:TextBox>
                </div>
                <!-- site list button -->
                <div class="col-md-4">
                    <!-- Trigger the modal with a button -->
                    <button type="button" class="btn btn-info btn-md" data-toggle="modal"
                        data-target="#siteListModal">
                        Site List</button>
                </div>
                <!-- Modal -->
                <div id="siteListModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title">Sites List</h3>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvSiteList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix nowrap" CellSpacing="0" Width="100%"
                                    GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvSiteListOnRowCommandSelect">
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

                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlExternalLocation">External Location *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlExternalLocation" runat="server" name="ddlExternalLocation" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlFixtureUse">Fixture Use? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFixtureUse" runat="server" name="ddlFixtureUse" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="numberOfFixtures">Number of Fixtures *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="numberOfFixtures" name="numberOfFixtures" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlMountingType">Mounting Type *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlMountingType" runat="server" name="ddlMountingType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="lampsPerFixture">Number of Lamps per Fixture *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="lampsPerFixture" name="lampsPerFixture" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlLampType">Lamp Type? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlLampType" runat="server" name="ddlLampType" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="lampWattage">Lamp Wattage *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="lampWattage" name="lampWattage" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="baseType">Lamp Base Type (See Reference)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="baseType" name="baseType" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>Fluorescent detail section</strong></h5>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlTubeLength">FL Lamp Tube Length</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTubeLength" runat="server" name="ddlTubeLength" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioStraightCurved">Straight/Curved?</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioStraightCurved" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Straight"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="Curved"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlTubeDiameter">FL Lamp Tube Diameter</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTubeDiameter" runat="server" name="ddlTubeDiameter" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlBallastType">Ballast Type</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBallastType" runat="server" name="ddlBallastType" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="numberOfBallasts">Number of Ballasts per Fixture</label>
                <div class="col-md-4">
                    <asp:TextBox ID="numberOfBallasts" name="numberOfBallasts" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlFixtureControl">Fixture Control *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFixtureControl" runat="server" name="ddlFixtureControl" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="fixturePhoto">Fixture Photo *</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="fixturePhoto" name="fixturePhoto" class="input-file" runat="server" />
                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">Exterior Lighting Notes</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>O&M Issues</strong></h5>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioFixturesOn">Fixtures are on during daylight hours? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioFixturesOn" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label" for="submitButtons"></label>
                <div class="col-md-8">
                    <asp:Button ID="saveButton" name="saveButton" CssClass="btn btn-success" runat="server" Text="Save" OnClick="saveButton_Click" />
                    <asp:Button ID="cancelButton" name="cancelButton" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="cancelButton_Click" />
                </div>
            </div>
        </div>
    </div>


    <script>
        function HideLabel() {
            $('#<%= SuccessPanel.ClientID %>').slideUp();
        }
        setTimeout("HideLabel();", 2000);
    </script>










</asp:Content>
