<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewWindow.aspx.cs" Inherits="bepas.NewWindow" %>

<asp:Content ID="NewWindow" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">Add New Window</div>

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

            <!-- Room/Space ID & Name * -->
            <div class="form-group">
                <label class="col-md-4 control-label">Room/Space ID & Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="roomId" CssClass="form-control" runat="server" placeholder="Room ID" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="roomName" CssClass="form-control" runat="server" placeholder="Room Name" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <!-- Trigger the modal with a button -->
                    <button type="button" class="btn btn-info btn-md" data-toggle="modal"
                        data-target="#roomListModal">
                        Room List</button>
                </div>

                <!-- Modal -->
                <div id="roomListModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title">Room List</h3>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvRoomList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix"
                                    GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvRoomListOnRowCommandSelect">
                                    <Columns>
                                        <asp:BoundField DataField="roomIdByUser" HeaderText="Room ID" />
                                        <asp:BoundField DataField="roomName" HeaderText="Room Name" />
                                        <asp:BoundField DataField="roomTypeText" HeaderText="Type" />
                                        <asp:BoundField DataField="boxedShapeText" HeaderText="Boxed Shape" />
                                        <asp:BoundField DataField="totalSqFootage" HeaderText="Total Sq. Footage" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="roomSelectButton" runat="server" CausesValidation="false" CommandName="SelectRoom"
                                                    Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("roomIdByUser")+";"+Eval("roomName")%>' />
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

            <!-- site id/name -->
            <div class="form-group">
                <label class="col-md-4 control-label">Window ID / Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="windowId" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="windowName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlWindowOrientation">Window Orientation *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWindowOrientation" runat="server" name="ddlWindowOrientation" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlWindowType">Window Type *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWindowType" runat="server" name="ddlWindowType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="windowHeight">Height (in feet) *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="windowHeight" name="windowHeight" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="windowWidth">Width (in feet) *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="windowWidth" name="windowWidth" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlGlazing">Glazing *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlGlazing" runat="server" name="ddlGlazing" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlCoating">Coating *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCoating" runat="server" name="ddlCoating" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlInteriorShading">Interior Shading *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlInteriorShading" runat="server" name="ddlInteriorShading" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlExteriorShading">Exterior Shading *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlExteriorShading" runat="server" name="ddlExteriorShading" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="windowPhoto">Window Photo *</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="windowPhoto" name="windowPhoto" class="input-file" runat="server" />
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>O&M Issues</strong></h5>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioDamaged">Windows Damaged or Cracked? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioDamaged" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioPoorCaulking">Poor or Missing Caulking? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioPoorCaulking" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioPoorAlignment">Poor Alignment or Excess Gaps? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioPoorAlignment" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioPoorSeals">Poor or Missing Weather Seals? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioPoorSeals" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">Window Notes or Concerns (If 1 or more types of glazing found make note here)</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label" for="submitButtons"></label>
                <div class="col-md-8">
                    <asp:Button ID="addButton" name="addButton" CssClass="btn btn-success" runat="server" Text="Add" OnClick="addButton_Click" />
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
