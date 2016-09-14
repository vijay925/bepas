<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlugloadDetail.aspx.cs" Inherits="bepas.PlugloadDetail" %>

<asp:Content ID="PlugloadDetail" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">Plugload Detail</div>

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

            <!-- Plugload Name (Make & Model) * -->
            <div class="form-group">
                <label class="col-md-4 control-label">Plugload Name (Make & Model) *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="plugloadName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <!-- Trigger the modal with a button -->
                    <button type="button" class="btn btn-info btn-md" data-toggle="modal"
                        data-target="#plugloadListModal">
                        Plugload List</button>
                </div>

                <!-- Modal -->
                <div id="plugloadListModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title">Plugload List</h3>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvPlugloadList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix"
                                    GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvPlugloadListOnRowCommandSelect">
                                    <Columns>
                                        <asp:BoundField DataField="plugloadName" HeaderText="Name (Model/Make)" />
                                        <asp:BoundField DataField="plugloadTypeText" HeaderText="Type" />
                                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="wattage" HeaderText="Wattage" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="plugloadSelectButton" runat="server" CausesValidation="false" CommandName="SelectPlugload"
                                                    Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("plugloadName")%>' />
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
                <label class="col-md-4 control-label" for="ddlPlugloadType">Plugload Type *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlPlugloadType" runat="server" name="ddlPlugloadType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="quantity">Quantity *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="quantity" name="quantity" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="wattage">Wattage</label>
                <div class="col-md-4">
                    <asp:TextBox ID="wattage" name="wattage" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioOnStandby">On Standby? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioOnStandby" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="3" Text="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="controls">Any Controls? (Describe)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="controls" name="controls" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>


            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="plugloadPhoto">Plugload Photo</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="plugloadPhoto" name="plugloadPhoto" class="input-file" runat="server" />
                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">Plugload Notes</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>O&M Issues</strong></h5>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioPlugloadOperating">Plugload is operating during times when not in use by personnel or normal shift hours? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioPlugloadOperating" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
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
