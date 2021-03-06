﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace bepas
{
    public partial class NewWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadDropdownItems();
                LoadSiteList();

            } //if

        } //Page_Load()

        private void LoadDropdownItems()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = "spLoadDropdownItems";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;

                connection.Open();

                using (DataSet dataSet = new DataSet())
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@uid", 14);
                    adapter.Fill(dataSet);
                    ddlWindowOrientation.DataSource = dataSet;
                    ddlWindowOrientation.DataTextField = "value";
                    ddlWindowOrientation.DataValueField = "uid";
                    ddlWindowOrientation.DataBind();

                    command.Parameters["@uid"].Value = 27;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlWindowType.DataSource = dataSet;
                    ddlWindowType.DataTextField = "value";
                    ddlWindowType.DataValueField = "uid";
                    ddlWindowType.DataBind();

                    command.Parameters["@uid"].Value = 28;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlGlazing.DataSource = dataSet;
                    ddlGlazing.DataTextField = "value";
                    ddlGlazing.DataValueField = "uid";
                    ddlGlazing.DataBind();

                    command.Parameters["@uid"].Value = 29;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlCoating.DataSource = dataSet;
                    ddlCoating.DataTextField = "value";
                    ddlCoating.DataValueField = "uid";
                    ddlCoating.DataBind();

                    command.Parameters["@uid"].Value = 30;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlInteriorShading.DataSource = dataSet;
                    ddlInteriorShading.DataTextField = "value";
                    ddlInteriorShading.DataValueField = "uid";
                    ddlInteriorShading.DataBind();

                    command.Parameters["@uid"].Value = 31;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlExteriorShading.DataSource = dataSet;
                    ddlExteriorShading.DataTextField = "value";
                    ddlExteriorShading.DataValueField = "uid";
                    ddlExteriorShading.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand
            

            ddlWindowOrientation.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlWindowType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlGlazing.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlCoating.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlInteriorShading.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlExteriorShading.Items.Insert(0, new ListItem("Please Select", "-1"));

        } //LoadDropdownItems()

        private void LoadSiteList()
        {
            DataSet dataSet = GetDataUsingSp("spLoadSites", null, null);
            gvSiteList.DataSource = dataSet;
            gvSiteList.DataBind();
            gvSiteList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadSiteList()

        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string siteUidLocal = argument[0];
            string siteIdByUserLocal = argument[1];
            string siteNameLocal = argument[2];

            siteId.Text = siteIdByUserLocal;
            siteName.Text = siteNameLocal;
            LoadBuildingList(Convert.ToInt32(siteUidLocal));
        }

        private void LoadBuildingList(int siteUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadBuildings", "@siteUid", siteUid);
            gvBuildingList.DataSource = dataSet;
            gvBuildingList.DataBind();
            gvBuildingList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadBuildingList()

        private void LoadRoomList(int buildingUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadRoomList", "@buildingUid", buildingUid);
            gvRoomList.DataSource = dataSet;
            gvRoomList.DataBind();
            gvRoomList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadBuildingList()

        protected void gvBuildingListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            SuccessPanel.Visible = false;

            string buildingUidLocal = argument[0];
            string buildingIdByUserLocal = argument[1];
            string buildingNameLocal = argument[2];

            buildingId.Text = buildingIdByUserLocal;
            buildingName.Text = buildingNameLocal;
            LoadRoomList(Convert.ToInt32(buildingUidLocal));
        }

        protected void gvRoomListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            SuccessPanel.Visible = false;

            string roomUidLocal = argument[0];
            string roomIdByUserLocal = argument[1];
            string roomNameLocal = argument[2];

            ViewState["roomUid"] = roomUidLocal;
            roomId.Text = roomIdByUserLocal;
            roomName.Text = roomNameLocal;
        }

        private DataSet GetDataUsingSp(string spName, string spParameterName, object spParameter)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = spName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;

                DataSet dataSet = new DataSet();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    if (spParameter != null)
                        command.Parameters.AddWithValue(spParameterName, (int)spParameter);
                    connection.Open();
                    adapter.Fill(dataSet);
                } //using SqlDataAdapter
                return dataSet;
            } //using SqlCommand
        } //GetDataUsingSp()

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //checks validation again in case javascript disabled <-- havent tested this yet
            {

                string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                using (SqlCommand command = new SqlCommand())
                {
                    int UserUid = 1;
                    command.CommandText = "spInsertWindow";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@windowIdByUser", windowId.Text);
                    command.Parameters.AddWithValue("@windowName", windowName.Text);
                    command.Parameters.AddWithValue("@windowOrientationId", Convert.ToInt32(ddlWindowOrientation.SelectedValue));
                    command.Parameters.AddWithValue("@windowOrientationText", ddlWindowOrientation.SelectedItem.Text);
                    command.Parameters.AddWithValue("@windowTypeId", Convert.ToInt32(ddlWindowType.SelectedValue));
                    command.Parameters.AddWithValue("@windowTypeText", ddlWindowType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@windowHeight", windowHeight.Text);
                    command.Parameters.AddWithValue("@windowWidth", windowWidth.Text);
                    command.Parameters.AddWithValue("@glazingId", Convert.ToInt32(ddlGlazing.SelectedValue));
                    command.Parameters.AddWithValue("@glazingText", ddlGlazing.SelectedItem.Text);
                    command.Parameters.AddWithValue("@coatingId", Convert.ToInt32(ddlCoating.SelectedValue));
                    command.Parameters.AddWithValue("@coatingText", ddlCoating.SelectedItem.Text);
                    command.Parameters.AddWithValue("@interiorShadingId", Convert.ToInt32(ddlInteriorShading.SelectedValue));
                    command.Parameters.AddWithValue("@interiorShadingText", ddlInteriorShading.SelectedItem.Text);
                    command.Parameters.AddWithValue("@exteriorShadingId", Convert.ToInt32(ddlExteriorShading.SelectedValue));
                    command.Parameters.AddWithValue("@exteriorShadingText", ddlExteriorShading.SelectedItem.Text);
                    command.Parameters.Add("@windowPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@windowPhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@damagedId", Convert.ToInt32(radioDamaged.SelectedValue));
                    command.Parameters.AddWithValue("@damagedText", radioDamaged.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorCaulkingId", Convert.ToInt32(radioPoorCaulking.SelectedValue));
                    command.Parameters.AddWithValue("@poorCaulkingText", radioPoorCaulking.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorAlignmentId", Convert.ToInt32(radioPoorAlignment.SelectedValue));
                    command.Parameters.AddWithValue("@poorAlignmentText", radioPoorAlignment.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorSealsId", Convert.ToInt32(radioPoorSeals.SelectedValue));
                    command.Parameters.AddWithValue("@poorSealsText", radioPoorSeals.SelectedItem.Text);
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;
                }
            } // if(page valid)

        } //addButton_Click()

    } //Webform
} //namespace bepas