<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuildingGeneralInfo.aspx.cs" Inherits="bepas.WebForm3" %>

<asp:Content ID="BuildingGeneralInfo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">Building General Info</div>

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

            <!-- building id/name -->
            <div class="form-group">
                <label class="col-md-4 control-label">Building ID / Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="buildingId" CssClass="form-control" runat="server" placeholder="Building ID" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="buildingName" CssClass="form-control" runat="server" placeholder="Building Name" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-4">
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
                            <div class="modal-body">
                                <asp:GridView ID="gvBuildingList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix"
                                    GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvBuildingListOnRowCommandSelect">
                                    <Columns>
                                        <asp:BoundField DataField="buildingIdByUser" HeaderText="Building ID" />
                                        <asp:BoundField DataField="buildingName" HeaderText="Building Name" />
                                        <asp:BoundField DataField="yearBuilt" HeaderText="Year Built" />
                                        <asp:BoundField DataField="buildingEndUseText" HeaderText="End Use" />
                                        <asp:BoundField DataField="boxedShapeText" HeaderText="Boxed Shape" />
                                        <asp:BoundField DataField="numberOfFloors" HeaderText="# of Floors" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="buildingSelectButton" runat="server" CausesValidation="false" CommandName="SelectBuilding"
                                                    Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("buildingIdByUser")+";"+Eval("buildingName")%>' />
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

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="birdsEyeSketchFile">Building Exterior Sketch (Birds Eye)</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="birdsEyeSketchFile" name="birdsEyeSketchFile" CssClass="input-file" runat="server" />
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="profileSketchFile">Building Exterior Sketch (Profile)</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="profileSketchFile" name="profileSketchFile" CssClass="input-file" runat="server" />
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="floorPlanSketchFile">Building Sketch Interior (Floor Plan) *</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="floorPlanSketchFile" name="floorPlanSketchFile" CssClass="input-file" runat="server" />
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="buildingPhotoFile">Building Picture (Photo) *</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="buildingPhotoFile" name="buildingPhotoFile" CssClass="input-file" runat="server" />
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="yearBuilt">Year Built *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="yearBuilt" name="yearBuilt" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlBuildingEndUse">Building End Use *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBuildingEndUse" runat="server" name="ddlBuildingEndUse" CssClass="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioBoxedShape">Boxed Shape Building *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioBoxedShape" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="grossAreaPerFloor">Gross Area Per Floor (Story) *<br />
                    (If other floors are different put in notes)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="grossAreaPerFloor" name="grossAreaPerFloor" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="buildingHeight">Building Height *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="buildingHeight" name="buildingHeight" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="buildingWidth">Building Width *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="buildingWidth" name="buildingWidth" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="buildingLength">Building Length *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="buildingLength" name="buildingLength" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="numberOfHVAC">Number of Exterior HVAC Units *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="numberOfHVAC" name="numberOfHVAC" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioOwnedOrLeased">Building Owned or Leased? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioOwnedOrLeased" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Owned"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="Leased"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="numberOfFloors">Number of Floors *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="numberOfFloors" name="numberOfFloors" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioPreviousAudit">Previous Audit Done? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioPreviousAudit" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="previousAuditDate">Date of previous Audit</label>
                <div class="col-md-4">
                    <asp:TextBox ID="previousAuditDate" name="previousAuditDate" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="meterId">Meter ID</label>
                <div class="col-md-4">
                    <asp:TextBox ID="meterId" name="meterId" CssClass="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioMeteredIndividually">Metered Individually?</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioMeteredIndividually" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="3" Text="N/A"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">Building Notes</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label" for="loginButtons"></label>
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
