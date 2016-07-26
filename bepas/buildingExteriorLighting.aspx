<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="buildingExteriorLighting.aspx.cs" Inherits="bepas.WebForm1" %>

<asp:Content ID="buildingExteriorLighting" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="well">
            <h4>Building Exterior Lighting</h4>
            <fieldset>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="fixtureUse">Fixture Use? *</label>
                    <div class="col-md-4">
                        <select id="fixtureUse" name="fixtureUse" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-sm-4 control-label" for="textinput">Number of Fixtures *</label>
                    <div class="col-sm-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-sm">
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="selectbasic">Mounting Type *</label>
                    <div class="col-md-4">
                        <select id="selectbasic" name="selectbasic" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">Number of Lamps per Fixture *</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="selectbasic">Lamp Type? *</label>
                    <div class="col-md-4">
                        <select id="selectbasic" name="selectbasic" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">Lamp Wattage *</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">Lamp Base Type (See Reference)</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">FL Lamp Tube Length</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
                    </div>
                </div>

                <!-- Multiple Radios (inline) -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="radios">Straight/Curved?</label>
                    <div class="col-md-4">
                        <label class="radio-inline" for="radios-0">
                            <input type="radio" name="radios" id="radios-0" value="1" checked="checked">
                            Straight
                        </label>
                        <label class="radio-inline" for="radios-1">
                            <input type="radio" name="radios" id="radios-1" value="2">
                            Curved
                        </label>
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="selectbasic">FL Lamp Tube Diameter</label>
                    <div class="col-md-4">
                        <select id="selectbasic" name="selectbasic" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="selectbasic">Ballast Type</label>
                    <div class="col-md-4">
                        <select id="selectbasic" name="selectbasic" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Text input-->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">Number of Ballasts per Fixture</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
                    </div>
                </div>

                <!-- Select Basic -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="selectbasic">Fixture Control *</label>
                    <div class="col-md-4">
                        <select id="selectbasic" name="selectbasic" class="form-control" style="width: auto">
                            <option value="-1">Please Select</option>
                            <option value="1">Option one</option>
                            <option value="2">Option two</option>
                        </select>
                    </div>
                </div>

                <!-- Textarea -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textarea">Exterior Lighting Notes</label>
                    <div class="col-md-4">
                        <textarea class="form-control" id="textarea" name="textarea"></textarea>
                    </div>
                </div>

                <!-- File Button -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="filebutton">Fixture Photo *</label>
                    <div class="col-md-4">
                        <input id="filebutton" name="filebutton" class="input-file" type="file">
                    </div>
                </div>

                <!-- Button (Double) -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="save"></label>
                    <div class="col-md-8">
                        <button id="save" name="save" class="btn btn-success">Save</button>
                        <button id="cancel" name="cancel" class="btn btn-danger">Cancel</button>
                    </div>
                </div>

            </fieldset>

        </div>
    </div>

</asp:Content>
