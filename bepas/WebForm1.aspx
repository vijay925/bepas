<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="bepas.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row myform">
        <div class="col-md-12">
            <form name="myform" role="form">

                <div class="form-inline">
                    <div class="form-group">
                        <label>Site ID / Name *</label>
                        <input type="text" name="siteId" id="siteId" class="form-control" placeholder="Site Id" />
                    </div>
                    <div class="form-group">
                        <label class="wb-inv"></label>
                        <input required type="text" name="siteName" id="siteName" class="form-control" placeholder="Site Name" />
                    </div>

                    <button type="submit" class="btn btn-primary">Submit</button>
                </div>

            </form>
        </div>

    </div>


</asp:Content>
