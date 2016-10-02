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
    public partial class HvacDetail : System.Web.UI.Page
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
                    command.Parameters.AddWithValue("@uid", 18);
                    adapter.Fill(dataSet);
                    ddlLocalOrDucted.DataSource = dataSet;
                    ddlLocalOrDucted.DataTextField = "value";
                    ddlLocalOrDucted.DataValueField = "uid";
                    ddlLocalOrDucted.DataBind();

                    command.Parameters["@uid"].Value = 19;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlUnitType.DataSource = dataSet;
                    ddlUnitType.DataTextField = "value";
                    ddlUnitType.DataValueField = "uid";
                    ddlUnitType.DataBind();

                    command.Parameters["@uid"].Value = 20;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlEconomizer.DataSource = dataSet;
                    ddlEconomizer.DataTextField = "value";
                    ddlEconomizer.DataValueField = "uid";
                    ddlEconomizer.DataBind();

                    command.Parameters["@uid"].Value = 21;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlServiceProvided.DataSource = dataSet;
                    ddlServiceProvided.DataTextField = "value";
                    ddlServiceProvided.DataValueField = "uid";
                    ddlServiceProvided.DataBind();

                    command.Parameters["@uid"].Value = 22;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlCondenserType.DataSource = dataSet;
                    ddlCondenserType.DataTextField = "value";
                    ddlCondenserType.DataValueField = "uid";
                    ddlCondenserType.DataBind();

                    command.Parameters["@uid"].Value = 23;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlFuelType.DataSource = dataSet;
                    ddlFuelType.DataTextField = "value";
                    ddlFuelType.DataValueField = "uid";
                    ddlFuelType.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand

            ddlLocalOrDucted.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlUnitType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlEconomizer.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlServiceProvided.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlCondenserType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFuelType.Items.Insert(0, new ListItem("Please Select", "-1"));
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

        private void LoadHvacList(int buildingUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadHvacList", "@buildingUid", buildingUid);
            gvHvacList.DataSource = dataSet;
            gvHvacList.DataBind();
            gvHvacList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadSkylightList()

        protected void gvSiteListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
            hvacId.Text = String.Empty;
            hvacName.Text = String.Empty;

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
            hvacId.Text = String.Empty;
            hvacName.Text = String.Empty;

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string buildingUidLocal = argument[0];
            string buildingIdByUserLocal = argument[1];
            string buildingNameLocal = argument[2];

            buildingId.Text = buildingIdByUserLocal;
            buildingName.Text = buildingNameLocal;
            LoadHvacList(Convert.ToInt32(buildingUidLocal));
        }

        protected void gvHvacListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            SuccessPanel.Visible = false;

            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string hvacUidLocal = argument[0];
            string hvacIdByUserLocal = argument[1];
            string hvacNameLocal = argument[2];

            ViewState["hvacUid"] = hvacUidLocal;
            hvacId.Text = hvacIdByUserLocal;
            hvacName.Text = hvacNameLocal;
            //loadinputs(Convert.ToInt32(roomUidLocal));
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

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) //checks validation again in case javascript disabled <-- havent tested this yet
            {
                string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))

                using (SqlCommand command = new SqlCommand())
                {
                    int UserUid = 1;
                    command.CommandText = "spInsertHvac";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@buildingUid", Convert.ToInt32(ViewState["buildingUid"]));
                    command.Parameters.AddWithValue("@hvacIdByUser", hvacId.Text);
                    command.Parameters.AddWithValue("@hvacName", hvacName.Text);
                    command.Parameters.AddWithValue("@localOrDuctedId", Convert.ToInt32(ddlLocalOrDucted.SelectedValue));
                    command.Parameters.AddWithValue("@localOrDuctedText", ddlLocalOrDucted.SelectedItem.Text);
                    command.Parameters.AddWithValue("@unitTypeId", Convert.ToInt32(ddlUnitType.SelectedValue));
                    command.Parameters.AddWithValue("@unitTypeText", ddlUnitType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@unitMake", unitMake.Text);
                    command.Parameters.AddWithValue("@unitModel", unitModel.Text);
                    command.Parameters.AddWithValue("@serialNumber", serialNumber.Text);
                    command.Parameters.AddWithValue("@unitAge", Convert.ToInt32(unitAge.Text));
                    command.Parameters.AddWithValue("@economizerId", Convert.ToInt32(ddlEconomizer.SelectedValue));
                    command.Parameters.AddWithValue("@economizerText", ddlEconomizer.SelectedItem.Text);
                    command.Parameters.AddWithValue("@serviceProvidedId", Convert.ToInt32(ddlServiceProvided.SelectedValue));
                    command.Parameters.AddWithValue("@serviceProvidedText", ddlServiceProvided.SelectedItem.Text);
                    command.Parameters.AddWithValue("@condenserTypeId", Convert.ToInt32(ddlCondenserType.SelectedValue));
                    command.Parameters.AddWithValue("@condenserTypeText", ddlCondenserType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@coolingCapacity", Convert.ToDouble(coolingCapacity.Text));
                    command.Parameters.AddWithValue("@coolingEfficiency", Convert.ToDouble(coolingEfficiency.Text));
                    command.Parameters.AddWithValue("@fuelTypeId", Convert.ToInt32(ddlFuelType.SelectedValue));
                    command.Parameters.AddWithValue("@fuelTypeText", ddlFuelType.SelectedItem.Text);
                    command.Parameters.AddWithValue("@gasBtuh", Convert.ToDouble(gasInput.Text));
                    command.Parameters.AddWithValue("@heatingEfficiency", Convert.ToDouble(heatingEfficiency.Text));
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
                    command.Parameters.AddWithValue("@userId", UserUid);
                    connection.Open();
                    command.ExecuteNonQuery();
                    SuccessPanel.Visible = true;
                }
            } // if(page valid)

        } //saveButton_Click()
    } //Webform
} //namespace bepas