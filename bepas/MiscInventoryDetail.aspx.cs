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
    public partial class MiscInventoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadSiteList();

            } //if

        } //Page_Load()


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

        private void LoadInventoryList(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadMiscInventoryList", "@roomUid", roomUid);
            gvMiscInventoryList.DataSource = dataSet;
            gvMiscInventoryList.DataBind();
            gvMiscInventoryList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadInventoryList()

        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            inventoryName.Text = String.Empty;
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
            inventoryName.Text = String.Empty;
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
            inventoryName.Text = String.Empty;
            ClearInputFields();

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            
            string roomUidLocal = argument[0];
            string roomIdByUserLocal = argument[1];
            string roomNameLocal = argument[2];

            roomId.Text = roomIdByUserLocal;
            roomName.Text = roomNameLocal;
            LoadInventoryList(Convert.ToInt32(roomUidLocal));
        }

        protected void gvInventoryListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            string[] argument = new string[2];
            argument = e.CommandArgument.ToString().Split(';');
            SuccessPanel.Visible = false;

            string inventoryUidLocal = argument[0];
            string inventoryNameLocal = argument[1];

            ViewState["inventoryUid"] = inventoryUidLocal;
            inventoryName.Text = inventoryNameLocal;
            LoadInputFields(Convert.ToInt32(inventoryUidLocal));
        }

        private void LoadInputFields(int inventoryUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadMiscInventoryDetail", "@inventoryUid", inventoryUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                inventoryName.Text = dr["name"].ToString();
                make.Text = dr["make"].ToString();
                model.Text = dr["model"].ToString();
                quantity.Text = dr["quantity"].ToString();
                quantity.Text = dr["wattage"].ToString();
                purpose.Value = dr["purpose"].ToString();
                runTime.Text = dr["runTime"].ToString();
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
                    command.CommandText = "spUpdateMiscInventory";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@inventoryUid", Convert.ToInt32(ViewState["inventoryUid"]));
                    //command.Parameters.AddWithValue("@name", name.Text);
                    command.Parameters.AddWithValue("@make", make.Text);
                    command.Parameters.AddWithValue("@model", model.Text);
                    command.Parameters.AddWithValue("@quantity", quantity.Text);
                    command.Parameters.AddWithValue("@wattage", wattage.Text);
                    command.Parameters.AddWithValue("@purpose", purpose.InnerText);
                    command.Parameters.AddWithValue("@runTime", runTime.Text);
                    command.Parameters.Add("@unitPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@unitPhotoFileName", DBNull.Value);
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
            inventoryName.Text = String.Empty;
            make.Text = String.Empty;
            model.Text = String.Empty;
            quantity.Text = String.Empty;
            wattage.Text = String.Empty;
            purpose.Value = String.Empty;
            runTime.Text = String.Empty;
            notes.Value = String.Empty;
        }

    } //Webform
} //namespace bepas