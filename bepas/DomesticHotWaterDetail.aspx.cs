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
    public partial class DomesticHotWaterDetail : System.Web.UI.Page
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

        private void LoadHotWaterList(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadHotWaterList", "@roomUid", roomUid);
            gvHotWaterList.DataSource = dataSet;
            gvHotWaterList.DataBind();
            gvHotWaterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadRoomList()

        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            waterId.Text = String.Empty;
            waterName.Text = String.Empty;
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
            waterId.Text = String.Empty;
            waterName.Text = String.Empty;
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
            waterId.Text = String.Empty;
            waterName.Text = String.Empty;
            ClearInputFields();

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            
            string roomUidLocal = argument[0];
            string roomIdByUserLocal = argument[1];
            string roomNameLocal = argument[2];

            roomId.Text = roomIdByUserLocal;
            roomName.Text = roomNameLocal;
            LoadHotWaterList(Convert.ToInt32(roomUidLocal));
        }

        protected void gvHotWaterListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            
            string waterUidLocal = argument[0];
            string wateIdByUserLocal = argument[1];
            string waterNameLocal = argument[2];

            ViewState["waterUidLocal"] = waterUidLocal;
            waterId.Text = wateIdByUserLocal;
            waterName.Text = waterNameLocal;

            LoadInputFields(Convert.ToInt32(waterUidLocal));
        }

        private void LoadInputFields(int waterUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadHotWaterDetail", "@waterUid", waterUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                ddlHeaterType.SelectedValue = dr["heaterTypeId"].ToString();
                storageSize.Text = dr["storageSize"].ToString();
                ddlFuelType.SelectedValue = dr["fuelTypeId"].ToString();
                electricWattage.Text = dr["electricWattage"].ToString();
                gasBtuh.Text = dr["gasBtuh"].ToString();
                efficiencyRating.Text = dr["efficiencyRating"].ToString();
                radioInsulationJacket.SelectedValue = dr["insulationJacketId"].ToString();
                insulationRValue.Text = dr["insulationRValue"].ToString();
                averageTemperature.Text = dr["averageTemperature"].ToString();
                radioPipesInsulated.SelectedValue = dr["pipesInsulatedId"].ToString();
                radioRecirculationPump.SelectedValue = dr["recirculationPumpId"].ToString();
                ddlControlType.SelectedValue = dr["controlTypeId"].ToString();
                setpointTemperature.Text = dr["setpointTemperature"].ToString();
                avgRecirculationTime.Text = dr["avgRecirculationTime"].ToString();
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

        private void ClearInputFields()
        {
            ddlHeaterType.SelectedValue = "-1";
            storageSize.Text = String.Empty;
            ddlFuelType.SelectedValue = "-1";
            electricWattage.Text = String.Empty;
            gasBtuh.Text = String.Empty;
            efficiencyRating.Text = String.Empty;
            radioInsulationJacket.SelectedIndex = -1;
            insulationRValue.Text = String.Empty;
            averageTemperature.Text = String.Empty;
            radioPipesInsulated.SelectedIndex = -1;
            radioRecirculationPump.SelectedIndex = -1;
            ddlControlType.SelectedValue = "-1";
            setpointTemperature.Text = String.Empty;
            avgRecirculationTime.Text = String.Empty;
            notes.Value = String.Empty;
        }

    } //Webform
} //namespace bepas