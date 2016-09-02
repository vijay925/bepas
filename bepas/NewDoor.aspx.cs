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
    public partial class NewDoor : System.Web.UI.Page
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
                    ddlDoorOrientation.DataSource = dataSet;
                    ddlDoorOrientation.DataTextField = "value";
                    ddlDoorOrientation.DataValueField = "uid";
                    ddlDoorOrientation.DataBind();

                    command.Parameters["@uid"].Value = 32;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlDoorType.DataSource = dataSet;
                    ddlDoorType.DataTextField = "value";
                    ddlDoorType.DataValueField = "uid";
                    ddlDoorType.DataBind();

                    command.Parameters["@uid"].Value = 33;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlDoorMaterial.DataSource = dataSet;
                    ddlDoorMaterial.DataTextField = "value";
                    ddlDoorMaterial.DataValueField = "uid";
                    ddlDoorMaterial.DataBind();

                    command.Parameters["@uid"].Value = 34;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlDoorInsulation.DataSource = dataSet;
                    ddlDoorInsulation.DataTextField = "value";
                    ddlDoorInsulation.DataValueField = "uid";
                    ddlDoorInsulation.DataBind();

                    command.Parameters["@uid"].Value = 35;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlInteriorShading.DataSource = dataSet;
                    ddlInteriorShading.DataTextField = "value";
                    ddlInteriorShading.DataValueField = "uid";
                    ddlInteriorShading.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand


            ddlDoorOrientation.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlDoorType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlDoorMaterial.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlDoorInsulation.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlInteriorShading.Items.Insert(0, new ListItem("Please Select", "-1"));

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
                    command.CommandText = "spInsertDoor";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@doorIdByUser", doorId.Text);
                    command.Parameters.AddWithValue("@doorName", doorName.Text);
                    command.Parameters.AddWithValue("@doorOrientationId", Convert.ToInt32(ddlDoorOrientation.SelectedValue));
                    command.Parameters.AddWithValue("@doorOrientationText", ddlDoorOrientation.SelectedItem.Text);
                    command.Parameters.AddWithValue("@doorTypeId", Convert.ToInt32(ddlDoorType.SelectedValue));
                    command.Parameters.AddWithValue("@doorTypeText", ddlDoorType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@doorHeight", doorHeight.Text);
                    command.Parameters.AddWithValue("@doorWidth", doorWidth.Text);
                    command.Parameters.AddWithValue("@doorMaterialId", Convert.ToInt32(ddlDoorMaterial.SelectedValue));
                    command.Parameters.AddWithValue("@doorMaterialText", ddlDoorMaterial.SelectedItem.Text);
                    command.Parameters.AddWithValue("@doorInsulationId", Convert.ToInt32(ddlDoorInsulation.SelectedValue));
                    command.Parameters.AddWithValue("@doorInsulationText", ddlDoorInsulation.SelectedItem.Text);
                    command.Parameters.AddWithValue("@glassPercentage", glassPercentage.Text);
                    command.Parameters.AddWithValue("@interiorShadingId", Convert.ToInt32(ddlInteriorShading.SelectedValue));
                    command.Parameters.AddWithValue("@interiorShadingText", ddlInteriorShading.SelectedItem.Text);
                    command.Parameters.Add("@doorPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@doorPhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@airGapsId", Convert.ToInt32(radioAirGaps.SelectedValue));
                    command.Parameters.AddWithValue("@airGapsText", radioAirGaps.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorAlignmentId", Convert.ToInt32(radioPoorAlignment.SelectedValue));
                    command.Parameters.AddWithValue("@poorAlignmentText", radioPoorAlignment.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorStrippingId", Convert.ToInt32(radioPoorStripping.SelectedValue));
                    command.Parameters.AddWithValue("@poorStrippingText", radioPoorStripping.SelectedItem.Text);
                    command.Parameters.AddWithValue("@poorCaulkingId", Convert.ToInt32(radioPoorCaulking.SelectedValue));
                    command.Parameters.AddWithValue("@poorCaulkingText", radioPoorCaulking.SelectedItem.Text);
                    command.Parameters.AddWithValue("@doorCloserWorkingId", Convert.ToInt32(radioDoorCloserWorking.SelectedValue));
                    command.Parameters.AddWithValue("@doorCloserWorkingText", radioDoorCloserWorking.SelectedItem.Text);
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