<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HvacDetail.aspx.cs" Inherits="bepas.HvacDetail" %>

<asp:Content ID="HvacDetail" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">HVAC Detail</div>

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
                    <asp:TextBox ID="siteName" CssClass="form-control" runat="server" placeholder="Site Name"></asp:TextBox>
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
                    <asp:TextBox ID="buildingName" CssClass="form-control" runat="server" placeholder="Building Name"></asp:TextBox>
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

            <!-- HVAC ID / Name * -->
            <div class="form-group">
                <label class="col-md-4 control-label">HVAC ID / Name *</label>
                <div class="col-md-2">
                    <asp:TextBox ID="hvacId" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="hvacName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <!-- Trigger the modal with a button -->
                    <button type="button" class="btn btn-info btn-md" data-toggle="modal"
                        data-target="#hvacListModal">
                        HVAC List</button>
                </div>

                <!-- Modal -->
                <div id="hvacListModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title">Building List</h3>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvHvacList" UseAccessibleHeader="true" CssClass="table table-striped table-hover clearfix"
                                    GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gvHvacListOnRowCommandSelect">
                                    <Columns>
                                        <asp:BoundField DataField="hvacIdByUser" HeaderText="HVAC ID" />
                                        <asp:BoundField DataField="hvacName" HeaderText="Name" />
                                        <asp:BoundField DataField="unitTypeText" HeaderText="Type" />
                                        <asp:BoundField DataField="unitMake" HeaderText="Make" />
                                        <asp:BoundField DataField="unitModel" HeaderText="Model" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="hvacSelectButton" runat="server" CausesValidation="false" CommandName="SelectHvac"
                                                    Text="Select" CommandArgument='<%#Eval("uid")+";"+Eval("hvacIdByUser")+";"+Eval("hvacName")%>' />
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
                <label class="col-md-4 control-label" for="ddlLocalOrDucted">Local or Ducted System? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlLocalOrDucted" runat="server" name="ddlLocalOrDucted" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlUnitType">Unit Type *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlUnitType" runat="server" name="ddlUnitType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>            

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="unitMake">Unit Make *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="unitMake" name="unitMake" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="unitModel">Unit Model *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="unitModel" name="unitModel" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="serialNumber">Serial Number</label>
                <div class="col-md-4">
                    <asp:TextBox ID="serialNumber" name="serialNumber" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="unitAge">Unit Age</label>
                <div class="col-md-4">
                    <asp:TextBox ID="unitAge" name="unitAge" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlEconomizer">Economizer? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlEconomizer" runat="server" name="ddlEconomizer" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlServiceProvided">Service Provided? *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlServiceProvided" runat="server" name="ddlServiceProvided" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div> 
            
            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>Cooling Information</strong></h5>
            </div>             

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlCondenserType">Condenser Type</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCondenserType" runat="server" name="ddlCondenserType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>  

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="coolingCapacity">Cooling Capacity (in tons or BTUh)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="coolingCapacity" name="coolingCapacity" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="coolingEfficiency">Cooling Efficiency (1 ~ 35 Range)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="coolingEfficiency" name="coolingEfficiency" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <h5 class="col-md-4 control-label"><strong>Heating Information</strong></h5>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlFuelType">Fuel Type</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFuelType" runat="server" name="ddlFuelType" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div> 

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="gasInput">BTH/h Input (Gas)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="gasInput" name="gasInput" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="heatingEfficiency">Heating Efficiency (72 ~ 99% range)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="heatingEfficiency" name="heatingEfficiency" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Textarea -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="notes">HVAC Notes</label>
                <div class="col-md-4">
                    <textarea id="notes" name="notes" class="form-control" runat="server"></textarea>
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="hvacPhoto">Unit HVAC Photo</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="hvacPhoto" name="hvacPhoto" class="input-file" runat="server" />
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="infoPlatePhoto">Info Plate Photo</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="infoPlatePhoto" name="infoPlatePhoto" class="input-file" runat="server" />
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
