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
    public partial class WindowList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadSiteList();

            } //if
           
        }

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
            DataSet dataSet = GetDataUsingSp("spLoadRooms", "@buildingUid", buildingUid);
            gvRoomList.DataSource = dataSet;
            gvRoomList.DataBind();
            gvRoomList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadRoomList()

        private void LoadWindowList(int roomUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadWindowList", "@roomUid", roomUid);
            gvWindowList.DataSource = dataSet;
            gvWindowList.DataBind();
            gvWindowList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadRoomList()


        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            gvWindowList.DataSource = null;
            gvWindowList.DataBind();
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;

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
            gvWindowList.DataSource = null;
            gvWindowList.DataBind();
            roomId.Text = String.Empty;
            roomName.Text = String.Empty;
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string buildingUid = argument[0];
            buildingId.Text = argument[1];
            buildingName.Text = argument[2];

            LoadRoomList(Convert.ToInt32(buildingUid));
        }

        protected void gvRoomListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string roomUid = argument[0];
            roomId.Text = argument[1];
            roomName.Text = argument[2];

            LoadWindowList(Convert.ToInt32(roomUid));
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

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/NewWindow.aspx");
        }


    }
}