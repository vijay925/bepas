using System;
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
    public partial class NewThermostat : System.Web.UI.Page
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
            /*
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
                    command.Parameters.AddWithValue("@uid", 25);
                    adapter.Fill(dataSet);
                    ddlFixtureType.DataSource = dataSet;
                    ddlFixtureType.DataTextField = "value";
                    ddlFixtureType.DataValueField = "uid";
                    ddlFixtureType.DataBind();

                    command.Parameters["@uid"].Value = 26;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlMountingType.DataSource = dataSet;
                    ddlMountingType.DataTextField = "value";
                    ddlMountingType.DataValueField = "uid";
                    ddlMountingType.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand
            */

            ddlControlledZone.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlControlledZone.Items.Insert(1, new ListItem("Zone 1", "1"));
            ddlControlledHvac.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlControlledHvac.Items.Insert(1, new ListItem("Hvac 1", "1"));
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
                    command.CommandText = "spInsertThermostat";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@thermostatIdByUser", thermostatId.Text);
                    command.Parameters.AddWithValue("@thermostatName", thermostatName.Text);
                    command.Parameters.AddWithValue("@controlledZoneUid", Convert.ToInt32(ddlControlledZone.SelectedValue));
                    command.Parameters.AddWithValue("@controlledHvacUid", Convert.ToInt32(ddlControlledHvac.SelectedValue));
                    command.Parameters.AddWithValue("@programmableId", Convert.ToInt32(radioProgrammable.SelectedValue));
                    command.Parameters.AddWithValue("@programmableText", radioProgrammable.SelectedItem.Text);
                    command.Parameters.AddWithValue("@accessibleId", Convert.ToInt32(radioAccessible.SelectedValue));
                    command.Parameters.AddWithValue("@accessibleText", radioAccessible.SelectedItem.Text);
                    command.Parameters.AddWithValue("@occupancyCoolingSetpoint", occupancyCoolingSetpoint.Text);
                    command.Parameters.AddWithValue("@nonOccupancyCoolingSetpoint", nonOccupancyCoolingSetpoint.Text);
                    command.Parameters.AddWithValue("@occupancyHeatingSetpoint", occupancyHeatingSetpoint.Text);
                    command.Parameters.AddWithValue("@nonOccupancyHeatingSetpoint", nonOccupancyHeatingSetpoint.Text);
                    command.Parameters.Add("@thermostatPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@thermostatPhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;

                }
            } // if(page valid)

        } //addButton_Click()

    } //Webform
} //namespace bepas