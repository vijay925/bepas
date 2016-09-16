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
    public partial class KitchenEquipmentDetail : System.Web.UI.Page
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
                    ddlEquipmentType.DataSource = dataSet;
                    ddlEquipmentType.DataTextField = "value";
                    ddlEquipmentType.DataValueField = "uid";
                    ddlEquipmentType.DataBind();

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
                    ddlControlledBy.DataSource = dataSet;
                    ddlControlledBy.DataTextField = "value";
                    ddlControlledBy.DataValueField = "uid";
                    ddlControlledBy.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand


            ddlEquipmentType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFuelType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlControlledBy.Items.Insert(0, new ListItem("Please Select", "-1"));

        } //LoadDropdownItems()

        private void LoadSiteList()
        {
            DataSet dataSet = GetDataUsingSp("spLoadSites", null, null);
            gvSiteList.DataSource = dataSet;
            gvSiteList.DataBind();
            gvSiteList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadSiteList()

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
        } //LoadRoomList()

        private void LoadEquipmentList(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadKitchenEquipmentList", "@roomUid", roomUid);
            gvEquipmentList.DataSource = dataSet;
            gvEquipmentList.DataBind();
            gvEquipmentList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadEquipmentList()


        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            equipmentId.Text = String.Empty;
            equipmentName.Text = String.Empty;
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

        protected void gvBuildingListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            equipmentId.Text = String.Empty;
            equipmentName.Text = String.Empty;
            ClearInputFields();

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            
            string buildingUidLocal = argument[0];
            string buildingIdByUserLocal = argument[1];
            string buildingNameLocal = argument[2];

            buildingId.Text = buildingIdByUserLocal;
            buildingName.Text = buildingNameLocal;
            LoadRoomList(Convert.ToInt32(buildingUidLocal));
        }

        protected void gvRoomListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            equipmentId.Text = String.Empty;
            equipmentName.Text = String.Empty;
            ClearInputFields();

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            
            string roomUidLocal = argument[0];
            string roomIdByUserLocal = argument[1];
            string roomNameLocal = argument[2];

            roomId.Text = roomIdByUserLocal;
            roomName.Text = roomNameLocal;
            LoadEquipmentList(Convert.ToInt32(roomUidLocal));
        }

        protected void gvEquipmentListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string equipmentUidLocal = argument[0];
            string equipmentIdByUserLocal = argument[1];
            string equipmentNameLocal = argument[2];

            ViewState["equipmentUid"] = equipmentUidLocal;
            equipmentId.Text = equipmentIdByUserLocal;
            equipmentName.Text = equipmentNameLocal;

            LoadInputFields(Convert.ToInt32(equipmentUidLocal));
        }

        private void LoadInputFields(int equipmentUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadKitchenEquipmentDetail", "@equipmentUid", equipmentUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                ddlEquipmentType.SelectedValue = dr["equipmentTypeId"].ToString();
                ddlFuelType.SelectedValue = dr["fuelTypeId"].ToString();
                electricWattage.Text = dr["electricWattage"].ToString();
                gasBtuh.Text = dr["gasBtuh"].ToString();
                ddlControlledBy.SelectedValue = dr["controlledById"].ToString();
                hoursOn.Text = dr["hoursOn"].ToString();
                radioEquipmentSeals.SelectedValue = dr["equipmentSealsId"].ToString();
                radioDoorCloser.SelectedValue = dr["doorCloserId"].ToString();
                radioFiltersClean.SelectedValue = dr["filtersCleanId"].ToString();
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //checks validation again in case javascript disabled <-- havent tested this yet
            {

                string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                using (SqlCommand command = new SqlCommand())
                {
                    int UserUid = 1;
                    command.CommandText = "spInsertKitchenEquipment";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@equipmentIdByUser", equipmentId.Text);
                    command.Parameters.AddWithValue("@equipmentName", equipmentName.Text);
                    command.Parameters.AddWithValue("@equipmentTypeId", Convert.ToInt32(ddlEquipmentType.SelectedValue));
                    command.Parameters.AddWithValue("@equipmentTypeText", ddlEquipmentType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@fuelTypeId", Convert.ToInt32(ddlFuelType.SelectedValue));
                    command.Parameters.AddWithValue("@fuelTypeText", ddlFuelType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@electricWattage", electricWattage.Text);
                    command.Parameters.AddWithValue("@gasBtuh", gasBtuh.Text);
                    command.Parameters.AddWithValue("@controlledById", Convert.ToInt32(ddlControlledBy.SelectedValue));
                    command.Parameters.AddWithValue("@controlledByText", ddlControlledBy.SelectedItem.Text);
                    command.Parameters.AddWithValue("@hoursOn", hoursOn.Text);
                    command.Parameters.Add("@equipmentPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@equipmentPhotoFileName", DBNull.Value);
                    command.Parameters.Add("@infoPlatePhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@infoPlatePhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@equipmentSealsId", Convert.ToInt32(radioEquipmentSeals.SelectedValue));
                    command.Parameters.AddWithValue("@equipmentSealsText", radioEquipmentSeals.SelectedItem.Text);
                    command.Parameters.AddWithValue("@doorCloserId", Convert.ToInt32(radioDoorCloser.SelectedValue));
                    command.Parameters.AddWithValue("@doorCloserText", radioDoorCloser.SelectedItem.Text);
                    command.Parameters.AddWithValue("@filtersCleanId", Convert.ToInt32(radioFiltersClean.SelectedValue));
                    command.Parameters.AddWithValue("@filtersCleanText", radioFiltersClean.SelectedItem.Text);
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;
                }
            } // if(page valid)

        } //addButton_Click()

        private void ClearInputFields()
        {
            ddlEquipmentType.SelectedValue = "-1";
            ddlFuelType.SelectedValue = "-1";
            electricWattage.Text = String.Empty;
            gasBtuh.Text = String.Empty;
            ddlControlledBy.SelectedValue = "-1";
            hoursOn.Text = String.Empty;
            radioEquipmentSeals.SelectedIndex = -1;
            radioDoorCloser.SelectedIndex = -1;
            radioFiltersClean.SelectedIndex = -1;
            notes.Value = String.Empty;
        }

    } //Webform
} //namespace bepas