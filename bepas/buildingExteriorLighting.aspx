<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="buildingExteriorLighting.aspx.cs" Inherits="bepas.WebForm1" %>

<asp:Content ID="buildingExteriorLighting" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <br />
        <br />
        <br />
        <br />
        <div class="well">
            <h4>Building Exterior Lighting</h4>
            <fieldset>

                <!-- site id/name -->
                <div class="form-group">
                    <label class="col-md-4 control-label">Site ID / Name *</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="siteId" class="form-control" runat="server" placeholder="Site ID"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="siteName" class="form-control" runat="server" placeholder="Site Name"></asp:TextBox>
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
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Site List</h4>
                                </div>
                                <div class="modal-body">
                                    <p>Some text in the modal.</p>
                                    <table id="sites-list" class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th>Site ID</th>
                                                <th>Site Name</th>
                                                <th>Survey Date</th>
                                                <th>Contact Name</th>
                                                <th>City</th>
                                                <th>State</th>
                                                <th>Select</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1</td>
                                                <td>Site Name 1</td>
                                                <td>03/11/2015</td>
                                                <td>Michael Rock</td>
                                                <td>Davis</td>
                                                <td>CA</td>
                                                <td>
                                                    <input class="select-button" type="button" value="Select"></td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Site Name 2</td>
                                                <td>04/20/2015</td>
                                                <td>Thomas Butler</td>
                                                <td>San Francisco</td>
                                                <td>CA</td>
                                                <td>
                                                    <input class="select-button" type="button" value="Select"></td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Site Name 3</td>
                                                <td>04/23/2015</td>
                                                <td>Darren Brown</td>
                                                <td>San Francisco</td>
                                                <td>CA</td>
                                                <td>
                                                    <input class="select-button" type="button" value="Select"></td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Site Name 4</td>
                                                <td>06/15/2015</td>
                                                <td>Tony Green</td>
                                                <td>Davis</td>
                                                <td>CA</td>
                                                <td>
                                                    <input class="select-button" type="button" value="Select"></td>
                                            </tr>
                                        </tbody>
                                    </table>
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
                        <asp:TextBox ID="buildingId" class="form-control" runat="server" placeholder="Building ID" OnTextChanged="buildingId_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="buildingName" class="form-control" runat="server" placeholder="Building Name"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <button type="submit" class="btn btn-info">Building List</button>
                    </div>
                </div>
                <!-- fixture use -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlFixtureUse">Fixture Use? *</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlFixtureUse" runat="server" name="ddlFixtureUse" class="form-control" Style="width: auto"></asp:DropDownList>
                    </div>
                </div>

                <!-- num of fixtures -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="numberOfFixtures">Number of Fixtures *</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="numberOfFixtures" name="numberOfFixtures" class="form-control input-md" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Select Basic -->
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

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlLampType">Lamp Type? *</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlLampType" runat="server" name="ddlLampType" class="form-control" Style="width: auto"></asp:DropDownList>
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

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlTubeLength">FL Lamp Tube Length</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlTubeLength" runat="server" name="ddlTubeLength" class="form-control" Style="width: auto"></asp:DropDownList>
                    </div>
                </div>

                <!-- Multiple Radios (inline) -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="radios">Straight/Curved?</label>
                    <div class="col-md-4">
                        <asp:RadioButton ID="radioStraight" Text="Straight" value="1" class="radio-inline" runat="server" GroupName="straightCurvedRadios" />
                        <asp:RadioButton ID="radioCurved" Text="Curved" value="2" class="radio-inline" runat="server" GroupName="straightCurvedRadios" />
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlTubeDiameter">FL Lamp Tube Diameter</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlTubeDiameter" runat="server" name="ddlTubeDiameter" class="form-control" Style="width: auto"></asp:DropDownList>
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlBallastType">Ballast Type</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlBallastType" runat="server" name="ddlBallastType" class="form-control" Style="width: auto"></asp:DropDownList>
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ballastsPerFixture">Number of Ballasts per Fixture</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="ballastsPerFixture" name="ballastsPerFixture" class="form-control input-md" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="ddlFixtureControl">Fixture Control *</label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlFixtureControl" runat="server" name="ddlFixtureControl" class="form-control" Style="width: auto"></asp:DropDownList>
                    </div>
                </div>

                <!-- Textarea -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="notes">Exterior Lighting Notes</label>
                    <div class="col-md-4">
                        <textarea  id="notes" name="notes" class="form-control" runat="server"></textarea>
                    </div>
                </div>

                <!-- File Button -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="fixturePhoto">Fixture Photo *</label>
                    <div class="col-md-4">
                        <asp:FileUpload ID="fixturePhoto" name="filebutton" class="input-file" runat="server" />
                    </div>
                </div>

                <!-- Button (Double) -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="loginButtons"></label>
                    <div class="col-md-8">
                        <asp:Button ID="saveButton" name="saveButton" class="btn btn-success" runat="server" Text="Save" OnClick="saveButton_Click" />
                        <asp:Button ID="cancelButton" name="cancelButton" class="btn btn-danger" runat="server" Text="Cancel" />
                    </div>
                </div>

            </fieldset>

        </div>
    </div>



</asp:Content>
