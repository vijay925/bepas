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
                        <input type="text" class="form-control" placeholder="Site ID"></div>
                    <div class="col-md-3">
                        <input type="text" class="form-control" placeholder="Site Name"></div>
                    <!-- site list button -->
                    <div class="col-md-3">
                        <!-- Trigger the modal with a button -->
                        <button type="button" class="btn btn-info btn-md" data-toggle="modal" 
                            data-target="#siteListModal">Site List</button>
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
  <td><input class="select-button" type="button"  value="Select"></td>
</tr>
<tr>
  <td>2</td>
  <td>Site Name 2</td>
  <td>04/20/2015</td>
  <td>Thomas Butler</td>
  <td>San Francisco</td>
  <td>CA</td>
  <td><input class="select-button" type="button"  value="Select"></td>
</tr>
<tr>
  <td>3</td>
  <td>Site Name 3</td>
  <td>04/23/2015</td>
  <td>Darren Brown</td>
  <td>San Francisco</td>
  <td>CA</td>
  <td><input class="select-button" type="button"  value="Select"></td>
</tr>
<tr>
  <td>4</td>
  <td>Site Name 4</td>
  <td>06/15/2015</td>
  <td>Tony Green</td>
  <td>Davis</td>
  <td>CA</td>
  <td><input class="select-button" type="button" value="Select"></td>
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
                        <input type="text" class="form-control" placeholder="Building ID"></div>
                    <div class="col-md-3">
                        <input type="text" class="form-control" placeholder="Building Name"></div>
                    <div class="col-md-3">
                        <button type="submit" class="btn btn-info">Building List</button>
                    </div>
                </div>
                <!-- fixture use -->
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

                <!-- num of fixtures -->
                <div class="form-group">
                    <label class="col-md-4 control-label" for="textinput">Number of Fixtures *</label>
                    <div class="col-md-4">
                        <input id="textinput" name="textinput" type="text" placeholder="" class="form-control input-md">
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
                    <label class="col-md-5 col-sm-4 col-xs-4 control-label" for="save"></label>
                    <div class="row">       
                          <button id="cancel" name="cancel" class="btn btn-danger">
                                <i class="fa fa-trash"></i>&nbsp Cancel</button>

                          <button id="save" name="save" class="btn btn-primary">
                                <i class="fa fa-paper-plane"></i>&nbsp Save</button>                    
                    </div>
                </div>

            </fieldset>

        </div>
    </div>



</asp:Content>
