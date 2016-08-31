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
    public partial class InteriorLighting : System.Web.UI.Page
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

                    command.Parameters["@uid"].Value = 3;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlLampType.DataSource = dataSet;
                    ddlLampType.DataTextField = "value";
                    ddlLampType.DataValueField = "uid";
                    ddlLampType.DataBind();

                    command.Parameters["@uid"].Value = 4;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlTubeLength.DataSource = dataSet;
                    ddlTubeLength.DataTextField = "value";
                    ddlTubeLength.DataValueField = "uid";
                    ddlTubeLength.DataBind();

                    command.Parameters["@uid"].Value = 5;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlTubeDiameter.DataSource = dataSet;
                    ddlTubeDiameter.DataTextField = "value";
                    ddlTubeDiameter.DataValueField = "uid";
                    ddlTubeDiameter.DataBind();

                    command.Parameters["@uid"].Value = 6;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlBallastType.DataSource = dataSet;
                    ddlBallastType.DataTextField = "value";
                    ddlBallastType.DataValueField = "uid";
                    ddlBallastType.DataBind();

                    command.Parameters["@uid"].Value = 7;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlfixtureControl.DataSource = dataSet;
                    ddlfixtureControl.DataTextField = "value";
                    ddlfixtureControl.DataValueField = "uid";
                    ddlfixtureControl.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand

            ddlFixtureType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlMountingType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlLampType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeLength.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeDiameter.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlBallastType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlfixtureControl.Items.Insert(0, new ListItem("Please Select", "-1"));
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
            ClearInputFields();

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
            LoadInputFields(Convert.ToInt32(roomUidLocal));
        }



        private void LoadInputFields(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadInteriorLighting", "@roomUid", roomUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                
                ddlFixtureType.SelectedValue = dr["fixtureTypeId"].ToString();
                numberOfFixtures.Text = dr["numberOfFixtures"].ToString();
                ddlMountingType.SelectedValue = dr["mountingTypeId"].ToString();
                numberOfLamps.Text = dr["numberOfLamps"].ToString();
                ddlLampType.SelectedValue = dr["lampTypeId"].ToString();
                lampWattage.Text = dr["lampWattage"].ToString();
                lampBaseType.Text = dr["lampBaseType"].ToString();
                ddlTubeLength.SelectedValue = dr["tubeLengthId"].ToString();
                radioStraightOrCurved.SelectedValue = dr["straightOrCurvedId"].ToString();
                ddlTubeDiameter.SelectedValue = dr["tubeDiameterId"].ToString();
                ddlBallastType.SelectedValue = dr["ballastTypeId"].ToString();
                numberOfBallasts.Text = dr["numberOfBallasts"].ToString();
                ddlfixtureControl.SelectedValue = dr["fixtureControlId"].ToString();
                radioDiffusersDirty.SelectedValue = dr["diffusersDirtyId"].ToString();
                radioFixturesRunning.SelectedValue = dr["fixturesRunningText"].ToString();
                radioLampsStrobing.SelectedValue = dr["lampsStrobingId"].ToString();
                notes.Value = dr["notes"].ToString();
            }
            else
            {
                ClearInputFields();
            }
        } //LoadInputFields()

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

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //checks validation again in case javascript disabled <-- havent tested this yet
            {
                string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                using (SqlCommand command = new SqlCommand())
                {
                    int UserUid = 1;
                    command.CommandText = "spInsertUpdateInteriorLighting";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@fixtureTypeId", Convert.ToInt32(ddlFixtureType.SelectedValue));
                    command.Parameters.AddWithValue("@fixtureTypeText", ddlFixtureType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@numberOfFixtures", Convert.ToInt32(numberOfFixtures.Text));
                    command.Parameters.AddWithValue("@mountingTypeId", Convert.ToInt32(ddlMountingType.SelectedValue));
                    command.Parameters.AddWithValue("@mountingTypeText", ddlMountingType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@numberOfLamps", Convert.ToInt32(numberOfLamps.Text));
                    command.Parameters.AddWithValue("@lampTypeId", Convert.ToInt32(ddlLampType.SelectedValue));
                    command.Parameters.AddWithValue("@lampTypeText", ddlLampType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@lampWattage", Convert.ToInt32(lampWattage.Text));
                    command.Parameters.AddWithValue("@lampBaseType", lampBaseType.Text);
                    command.Parameters.AddWithValue("@tubeLengthId", Convert.ToInt32(ddlTubeLength.SelectedValue));
                    command.Parameters.AddWithValue("@tubeLengthText", ddlTubeLength.SelectedItem.Text);
                    command.Parameters.AddWithValue("@straightOrCurvedId", Convert.ToInt32(radioStraightOrCurved.SelectedValue));
                    command.Parameters.AddWithValue("@straightOrCurvedText", radioStraightOrCurved.SelectedItem.Text);
                    command.Parameters.AddWithValue("@tubeDiameterId", Convert.ToInt32(ddlTubeDiameter.SelectedValue));
                    command.Parameters.AddWithValue("@tubeDiameterText", ddlTubeDiameter.SelectedItem.Text);
                    command.Parameters.AddWithValue("@ballastTypeId", Convert.ToInt32(ddlBallastType.SelectedValue));
                    command.Parameters.AddWithValue("@ballastTypeText", ddlBallastType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@numberOfBallasts", Convert.ToInt32(numberOfBallasts.Text));
                    command.Parameters.AddWithValue("@fixtureControlId", Convert.ToInt32(ddlfixtureControl.SelectedValue));
                    command.Parameters.AddWithValue("@fixtureControlText", ddlfixtureControl.SelectedItem.Text);
                    command.Parameters.Add("@fixturePhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@fixturePhotoFileName", DBNull.Value); 
                    command.Parameters.AddWithValue("@diffusersDirtyId", Convert.ToInt32(radioDiffusersDirty.SelectedValue));
                    command.Parameters.AddWithValue("@diffusersDirtyText", radioDiffusersDirty.SelectedItem.Text);
                    command.Parameters.AddWithValue("@fixturesRunningId", Convert.ToInt32(radioFixturesRunning.SelectedValue));
                    command.Parameters.AddWithValue("@fixturesRunningText", radioFixturesRunning.SelectedItem.Text);
                    command.Parameters.AddWithValue("@lampsStrobingId", Convert.ToInt32(radioLampsStrobing.SelectedValue));
                    command.Parameters.AddWithValue("@lampsStrobingText", radioLampsStrobing.SelectedItem.Text);
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;
                    
                }
            } // if(page valid)

        } //saveButton_Click()

        private void ClearInputFields()
        {

            ddlFixtureType.SelectedValue = "-1";
            numberOfFixtures.Text = String.Empty;
            ddlMountingType.SelectedValue = "-1";
            numberOfLamps.Text = String.Empty;
            ddlLampType.SelectedValue = "-1";
            lampWattage.Text = String.Empty;
            lampBaseType.Text = String.Empty;
            ddlTubeLength.SelectedValue = "-1";
            radioStraightOrCurved.SelectedIndex = -1;
            ddlTubeDiameter.SelectedValue = "-1";
            ddlBallastType.SelectedValue = "-1";
            numberOfBallasts.Text = String.Empty;
            ddlfixtureControl.SelectedValue = "-1";
            radioDiffusersDirty.SelectedIndex = -1;
            radioFixturesRunning.SelectedIndex = -1;
            radioLampsStrobing.SelectedIndex = -1;
            notes.Value = String.Empty;
        }
    } //Webform
} //namespace bepas