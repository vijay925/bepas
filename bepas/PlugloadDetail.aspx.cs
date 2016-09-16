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
    public partial class PlugloadDetail : System.Web.UI.Page
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

                    command.Parameters.AddWithValue("@uid", 40);
                    adapter.Fill(dataSet);
                    ddlPlugloadType.DataSource = dataSet;
                    ddlPlugloadType.DataTextField = "value";
                    ddlPlugloadType.DataValueField = "uid";
                    ddlPlugloadType.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand


            ddlPlugloadType.Items.Insert(0, new ListItem("Please Select", "-1"));

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

        private void LoadPlugloadList(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadPlugloadList", "@roomUid", roomUid);
            gvPlugloadList.DataSource = dataSet;
            gvPlugloadList.DataBind();
            gvPlugloadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadSkylightList()

        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            plugloadName.Text = String.Empty;
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
            plugloadName.Text = String.Empty;
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
            plugloadName.Text = String.Empty;
            ClearInputFields();

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string roomUidLocal = argument[0];
            string roomIdByUserLocal = argument[1];
            string roomNameLocal = argument[2];

            roomId.Text = roomIdByUserLocal;
            roomName.Text = roomNameLocal;
            LoadPlugloadList(Convert.ToInt32(roomUidLocal));
        }

        protected void gvPlugloadListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;

            string[] argument = new string[2];
            argument = e.CommandArgument.ToString().Split(';');

            string plugloadUidLocal = argument[0];
            string plugloadNameLocal = argument[1];

            ViewState["roomUid"] = plugloadUidLocal;
            plugloadName.Text = plugloadNameLocal;
            LoadInputFields(Convert.ToInt32(plugloadUidLocal));
        }

        private void LoadInputFields(int plugloadUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadPlugloadDetail", "@plugloadUid", plugloadUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                ddlPlugloadType.SelectedValue = dr["plugloadTypeId"].ToString();
                quantity.Text = dr["quantity"].ToString();
                wattage.Text = dr["wattage"].ToString();
                radioOnStandby.SelectedValue = dr["onStandbyId"].ToString();
                controls.Text = dr["controls"].ToString();
                notes.Value = dr["notes"].ToString();
                radioPlugloadOperating.SelectedValue = dr["plugloadOperatingId"].ToString();
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
                    command.CommandText = "spInsertPlugload";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@roomUid", Convert.ToInt32(ViewState["roomUid"]));
                    command.Parameters.AddWithValue("@plugloadName", plugloadName.Text);
                    command.Parameters.AddWithValue("@plugloadTypeId", Convert.ToInt32(ddlPlugloadType.SelectedValue));
                    command.Parameters.AddWithValue("@plugloadTypeText", ddlPlugloadType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@quantity", quantity.Text);
                    command.Parameters.AddWithValue("@wattage", wattage.Text);
                    command.Parameters.AddWithValue("@onStandbyId", Convert.ToInt32(radioOnStandby.SelectedValue));
                    command.Parameters.AddWithValue("@onStandbyText", radioOnStandby.SelectedItem.Text);
                    command.Parameters.AddWithValue("@controls", controls.Text);
                    command.Parameters.Add("@plugloadPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@plugloadPhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
                    command.Parameters.AddWithValue("@plugloadOperatingId", Convert.ToInt32(radioPlugloadOperating.SelectedValue));
                    command.Parameters.AddWithValue("@plugloadOperatingText", radioPlugloadOperating.SelectedItem.Text);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;
                }
            } // if(page valid)

        } //addButton_Click()

        private void ClearInputFields()
        {
            ddlPlugloadType.SelectedValue = "-1";
            quantity.Text = String.Empty;
            wattage.Text = String.Empty;
            radioOnStandby.SelectedIndex = -1;
            controls.Text = String.Empty;
            notes.Value = String.Empty;
            radioPlugloadOperating.SelectedIndex = -1;
        }

    } //Webform
} //namespace bepas