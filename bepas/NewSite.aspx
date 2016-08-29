<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSite.aspx.cs" Inherits="bepas.NewSite" %>

<asp:Content ID="NewSite" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading h4">New Site</div>

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
                    <asp:TextBox ID="siteId" CssClass="form-control" runat="server" placeholder="Site ID"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="siteName" CssClass="form-control" runat="server" placeholder="Site Name"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="contactName">On-site Contact Name *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="contactName" name="contactName" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="contactNumber">Contact Number *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="contactNumber" name="contactNumber" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="contactEmail">Contact Email *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="contactEmail" name="contactEmail" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="address1">Address 1 *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="address1" name="address1" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="address2">Address 2</label>
                <div class="col-md-4">
                    <asp:TextBox ID="address2" name="address2" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="city">City</label>
                <div class="col-md-4">
                    <asp:TextBox ID="city" name="city" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Dropdown -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="ddlState">State *</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlState" runat="server" name="ddlState" class="form-control" Style="width: auto"></asp:DropDownList>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="zipCode">Zip code *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="zipCode" name="zipCode" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="surveyDate">Survey Date *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="surveyDate" name="surveyDate" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Text input-->
            <div class="form-group">
                <label class="col-md-4 control-label" for="frpmAmount">FRPM Amount (%) *</label>
                <div class="col-md-4">
                    <asp:TextBox ID="frpmAmount" name="frpmAmount" class="form-control input-md" runat="server"></asp:TextBox>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioDuringOrAfter">During or After Office Hours *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioDuringOrAfter" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="During"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="After"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="3" Text="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- Radios (inline) -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="radioKeyAccess">Can Access Keys be Issued to Crew Supervisor? *</label>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="radioKeyAccess" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="2" Text="No"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="3" Text="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <!-- File Button -->
            <div class="form-group">
                <label class="col-md-4 control-label" for="mapFile">Site and Building Map File Upload</label>
                <div class="col-md-4">
                    <asp:FileUpload ID="mapFile" name="mapFile" class="input-file" runat="server" />
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
