<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuildingExteriorLighting.aspx.cs" Inherits="bepas.WebForm1" %>

<asp:Content ID="BuildingExteriorLighting" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-default">
        <div class="panel-heading h4">Building Exterior Lighting</div>

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
                <div class="col-md-3">
                    <asp:TextBox ID="siteName" CssClass="form-control" runat="server" placeholder="Site Name" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="reqSiteName"
                        ControlToValidate="siteName"
                        CssClass="validationError"
                        Display="Dynamic"
                        ErrorMessage="Please select an option from the site list"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
                <!-- site list button -->
                <div class="col-md-3">
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
                                <div class="table-responsive">
                                    <asp:GridView ID="gvSiteList" UseAccessibleHeader="true" CssClass="table table-bordered table-hover nowrap" CellSpacing="0" Width="100%"
                                        GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvSiteListOnRowCommandSelect">
                                        <Columns>
                                            <asp:BoundField DataField="uid" HeaderText="Site ID" />
                                            <asp:BoundField DataField="siteIdByUser" HeaderText="Site IDBU" />
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
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- building id/name -->
            <div class="form-group">
                <label class="col-md-4 control-label">Building ID / Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="buildingId" CssClass="form-control" runat="server" placeholder="Building ID" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="buildingName" CssClass="form-control" runat="server" placeholder="Building Name" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="reqBuildingName"
                        ControlToValidate="buildingName"
                        ErrorMessage="Please select a building from the list"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <!-- Trigger the modal with a button -->
                    <button type="button" class="btn btn-info btn-md" data-toggle="modal"
                        data-target="#buildingListModal">
                        Building List</button>
                </div>

                <!-- Modal -->
                <div id="buildingListModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title">Building List</h3>
                            </div>
                            <div class="divLayerContainer">
                                <div class="modal-body">
                                    <asp:GridView ID="gvBuildingList" UseAccessibleHeader="true" CssClass="table table-bordered table-hover nowrap"
                                        GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvBuildingListOnRowCommandSelect" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="uid" HeaderText="Building ID" />
                                            <asp:BoundField DataField="buildingIdByUser" HeaderText="Building IDBU" />
                                            <asp:BoundField DataField="buildingName" HeaderText="Building Name" />
                                            <asp:BoundField DataField="yearBuilt" HeaderText="Year Built" />
                                            <asp:BoundField DataField="buildingEndUseText" HeaderText="End Use" />
                                            <asp:BoundField DataField="boxedShapeText" HeaderText="Boxed Shape" />
                                            <asp:BoundField DataField="numberOfFloors" HeaderText="# of Floors" />
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="buildingSelectButton" runat="server" CausesValidation="false" CommandName="SelectBuilding"
                                                        Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("buildingIdByUser")+";"+Eval("buildingName")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="cursor-pointer" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- fixture use --------------------------------------------------------->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlFixtureUse">Fixture Use? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFixtureUse" runat="server" name="ddlFixtureUse" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="reqDdlFixtureUse"
                        ControlToValidate="ddlFixtureUse"
                        InitialValue="-1"
                        ErrorMessage="Selection required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- num of fixtures -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="numberOfFixtures">Number of Fixtures *</label>
                <div class="col-md-8">
                    <asp:TextBox ID="numberOfFixtures" name="numberOfFixtures" CssClass="form-control input-md" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="reqNumberOfFixtures"
                        ControlToValidate="numberOfFixtures"
                        ErrorMessage="Entry required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="reqIntNumberOfFixtures"
                        ControlToValidate="numberOfFixtures"
                        runat="server"
                        ErrorMessage="Positive integers only"
                        CssClass="validationError"
                        Display="Dynamic"
                        ValidationExpression="\d+">
                    </asp:RegularExpressionValidator>
                </div>

            </div>

            <!-- mounting type -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlMountingType">Mounting Type *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlMountingType" runat="server" name="ddlMountingType" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="reqDdlMountingType"
                        ControlToValidate="ddlMountingType"
                        InitialValue="-1"
                        ErrorMessage="Selection required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- lamps per fixture -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="lampsPerFixture">Number of Lamps per Fixture *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="lampsPerFixture" name="lampsPerFixture" CssClass="form-control input-md" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="reqLampsPerFixture"
                        ControlToValidate="lampsPerFixture"
                        ErrorMessage="Selection required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="reqIntLampsPerFixture"
                        ControlToValidate="lampsPerFixture"
                        runat="server"
                        ErrorMessage="Positive integers only"
                        CssClass="validationError"
                        Display="Dynamic"
                        ValidationExpression="\d+">
                    </asp:RegularExpressionValidator>
                </div>
            </div>

            <!-- lamp type -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlLampType">Lamp Type? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlLampType" runat="server" name="ddlLampType" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="reqDdlLampType"
                        ControlToValidate="ddlLampType"
                        InitialValue="-1"
                        ErrorMessage="Selection required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- lamp watt-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="lampWattage">Lamp Wattage *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="lampWattage" name="lampWattage" CssClass="form-control input-md" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="reqLampWattage"
                        ControlToValidate="lampWattage"
                        ErrorMessage="Entry required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator
                        ID="reqNumLampWattage"
                        runat="server"
                        ControlToValidate="lampWattage"
                        Type="Double"
                        Operator="DataTypeCheck"
                        CssClass="validationError"
                        Display="Dynamic"
                        ErrorMessage="Numbers only">

                    </asp:CompareValidator>

                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="baseType">Lamp Base Type (See Reference)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="baseType" name="baseType" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>Fluorescent detail section</strong></h5>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlTubeLength">FL Lamp Tube Length</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTubeLength" runat="server" name="ddlTubeLength" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Multiple Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioListStraightCurved">Straight/Curved?</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioListStraightCurved" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Straight"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="Curved"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>



            <!-- Select Basic -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlTubeDiameter">FL Lamp Tube Diameter</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTubeDiameter" runat="server" name="ddlTubeDiameter" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Select Basic -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlBallastType">Ballast Type</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBallastType" runat="server" name="ddlBallastType" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ballastsPerFixture">Number of Ballasts per Fixture</label>
                <div class="col-md-4">
                    <asp:TextBox ID="ballastsPerFixture" name="ballastsPerFixture" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Select Basic -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlFixtureControl">Fixture Control *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFixtureControl" runat="server" name="ddlFixtureControl" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="reqDdlFixtureControl"
                        ControlToValidate="ddlFixtureControl"
                        InitialValue="-1"
                        ErrorMessage="Selection required"
                        CssClass="validationError"
                        Display="Dynamic"
                        runat="server">
                    </asp:RequiredFieldValidator>

                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">Exterior Lighting Notes</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="fixturePhoto">Fixture Photo *</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="fixturePhoto" name="filebutton" CssClass="input-file" runat="server" />
                </div>
            </div>

            <!-- Button (Double) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="loginButtons"></label>
                <div class="col-md-8">
                    <asp:Button ID="saveButton" name="saveButton" CssClass="btn btn-success" runat="server" Text="Save" OnClick="saveButton_Click" />
                    <asp:Button ID="cancelButton" name="cancelButton" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="cancelButton_Click" />
                </div>
            </div>
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

            $('#siteListModal').on('show.bs.modal', function () {
                $(this).find('.modal-dialog').css({
                    height: 'auto',
                    'min-width': '1000px'
                });
                var dataTable = $('#<%=gvSiteList.ClientID%>').DataTable();
                dataTable.columns.adjust().draw();
            });
        });

    </script>

    <script>
        function HideLabel() {
            $('#<%= SuccessPanel.ClientID %>').slideUp();
        }
        setTimeout("HideLabel();", 3000);




       
    </script>


</asp:Content>
