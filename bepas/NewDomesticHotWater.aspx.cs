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
    public partial class NewDomesticHotWater : System.Web.UI.Page
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
                    command.Parameters.AddWithValue("@uid", 38);
                    adapter.Fill(dataSet);
                    ddlHeaterType.DataSource = dataSet;
                    ddlHeaterType.DataTextField = "value";
                    ddlHeaterType.DataValueField = "uid";
                    ddlHeaterType.DataBind();

                    command.Parameters["@uid"].Value = 23;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlFuelType.DataSource = dataSet;
                    ddlFuelType.DataTextField = "value";
                    ddlFuelType.DataValueField = "uid";
                    ddlFuelType.DataBind();

                    command.Parameters["@uid"].Value = 39;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlControlType.DataSource = dataSet;
                    ddlControlType.DataTextField = "value";
                    ddlControlType.DataValueField = "uid";
                    ddlControlType.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand


            ddlHeaterType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFuelType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlControlType.Items.Insert(0, new ListItem("Please Select", "-1"));

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
                    command.CommandText = "spInsertHotWater";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@waterIdByUser", waterId.Text);
                    command.Parameters.AddWithValue("@waterName", waterName.Text);
                    command.Parameters.AddWithValue("@heaterTypeId", Convert.ToInt32(ddlHeaterType.SelectedValue));
                    command.Parameters.AddWithValue("@heaterTypeText", ddlHeaterType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@storageSize", storageSize.Text);
                    command.Parameters.AddWithValue("@fuelTypeId", Convert.ToInt32(ddlFuelType.SelectedValue));
                    command.Parameters.AddWithValue("@fuelTypeText", ddlFuelType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@electricWattage", electricWattage.Text);
                    command.Parameters.AddWithValue("@gasBtuh", gasBtuh.Text);
                    command.Parameters.AddWithValue("@efficiencyRating", efficiencyRating.Text);
                    command.Parameters.AddWithValue("@insulationJacketId", Convert.ToInt32(radioInsulationJacket.SelectedValue));
                    command.Parameters.AddWithValue("@insulationJacketText", radioInsulationJacket.SelectedItem.Text);
                    command.Parameters.AddWithValue("@insulationRValue", insulationRValue.Text);
                    command.Parameters.AddWithValue("@averageTemperature", averageTemperature.Text);
                    command.Parameters.AddWithValue("@pipesInsulatedId", Convert.ToInt32(radioPipesInsulated.SelectedValue));
                    command.Parameters.AddWithValue("@pipesInsulatedText", radioPipesInsulated.SelectedItem.Text);
                    command.Parameters.AddWithValue("@recirculationPumpId", Convert.ToInt32(radioRecirculationPump.SelectedValue));
                    command.Parameters.AddWithValue("@recirculationPumpText", radioRecirculationPump.SelectedItem.Text);
                    command.Parameters.AddWithValue("@controlTypeId", Convert.ToInt32(ddlControlType.SelectedValue));
                    command.Parameters.AddWithValue("@controlTypeText", ddlControlType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@setpointTemperature", setpointTemperature.Text);
                    command.Parameters.AddWithValue("@avgRecirculationTime", radioRecirculationPump.Text);
                    command.Parameters.Add("@unitPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@unitPhotoFileName", DBNull.Value);
                    command.Parameters.Add("@infoPlatePhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@infoPlatePhotoFileName", DBNull.Value);
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