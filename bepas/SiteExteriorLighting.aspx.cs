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
    public partial class SiteExteriorLighting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadDropdownItems();
                LoadSiteList();
            } //if
        }

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
                    /* FIX: dropdown list data not present in database */
                    command.Parameters.AddWithValue("@uid", 1);
                    adapter.Fill(dataSet);
                    ddlExternalLocation.DataSource = dataSet;
                    ddlExternalLocation.DataTextField = "value";
                    ddlExternalLocation.DataValueField = "uid";
                    ddlExternalLocation.DataBind();

                    command.Parameters["@uid"].Value = 1;
                    dataSet.Clear();
                    adapter.Fill(dataSet);
                    ddlFixtureUse.DataSource = dataSet;
                    ddlFixtureUse.DataTextField = "value";
                    ddlFixtureUse.DataValueField = "uid";
                    ddlFixtureUse.DataBind();

                    command.Parameters["@uid"].Value = 2;
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
                    ddlFixtureControl.DataSource = dataSet;
                    ddlFixtureControl.DataTextField = "value";
                    ddlFixtureControl.DataValueField = "uid";
                    ddlFixtureControl.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand

            ddlExternalLocation.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFixtureUse.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlMountingType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlLampType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeLength.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeDiameter.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlBallastType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFixtureControl.Items.Insert(0, new ListItem("Please Select", "-1"));
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
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');

            string siteUidLocal = argument[0];
            string siteIdByUserLocal = argument[1];
            string siteNameLocal = argument[2];

            siteId.Text = siteIdByUserLocal;
            siteName.Text = siteNameLocal;
            ViewState["siteUid"] = siteUidLocal;

            LoadInputFields(Convert.ToInt32(siteUidLocal));
        }

        private void LoadInputFields(int siteUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadSiteExLighting", "@siteUid", siteUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                ddlExternalLocation.SelectedValue = dr["externalLocationId"].ToString();
                ddlFixtureUse.SelectedValue = dr["fixtureId"].ToString();
                numberOfFixtures.Text = dr["numberOfFixtures"].ToString();
                ddlMountingType.SelectedValue = dr["mountingTypeId"].ToString();
                lampsPerFixture.Text = dr["lampsPerFixture"].ToString(); ;
                ddlLampType.SelectedValue = dr["lampTypeId"].ToString();
                lampWattage.Text = dr["lampWattage"].ToString(); ;
                baseType.Text = dr["lampBaseType"].ToString(); ;
                ddlTubeLength.SelectedValue = dr["tubeLengthId"].ToString();
                radioStraightCurved.SelectedValue = dr["straightOrCurvedId"].ToString();
                ddlTubeDiameter.SelectedValue = dr["tubeDiameterId"].ToString();
                ddlBallastType.SelectedValue = dr["ballastTypeId"].ToString();
                numberOfBallasts.Text = dr["ballastsPerFixture"].ToString(); ;
                ddlFixtureControl.SelectedValue = dr["fixtureControlId"].ToString();
                notes.InnerText = dr["notes"].ToString();
                radioFixturesOn.SelectedValue = dr["fixturesOnId"].ToString();
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

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand())
            {
                int UserUid = 1;
                command.CommandText = "spInsertUpdateSiteExLighting";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("@siteUid", Convert.ToInt32(ViewState["siteUid"]));
                command.Parameters.AddWithValue("@externalLocationId", Convert.ToInt32(ddlExternalLocation.SelectedValue));
                command.Parameters.AddWithValue("@externalLocationText", ddlExternalLocation.SelectedItem.Text);
                command.Parameters.AddWithValue("@fixtureId", Convert.ToInt32(ddlFixtureUse.SelectedValue));
                command.Parameters.AddWithValue("@fixtureText", ddlFixtureUse.SelectedItem.Text);
                command.Parameters.AddWithValue("@numberOfFixtures", Convert.ToInt32(numberOfFixtures.Text));
                command.Parameters.AddWithValue("@mountingTypeId", Convert.ToInt32(ddlMountingType.SelectedValue));
                command.Parameters.AddWithValue("@mountingTypeText", ddlMountingType.SelectedItem.Text);
                command.Parameters.AddWithValue("@lampsPerFixture", Convert.ToInt32(lampsPerFixture.Text));
                command.Parameters.AddWithValue("@lampTypeId", Convert.ToInt32(ddlLampType.SelectedValue));
                command.Parameters.AddWithValue("@lampTypeText", ddlLampType.SelectedItem.Text);
                command.Parameters.AddWithValue("@lampWattage", Convert.ToInt32(lampWattage.Text));
                command.Parameters.AddWithValue("@lampBaseType", baseType.Text);
                command.Parameters.AddWithValue("@tubeLengthId", Convert.ToInt32(ddlTubeLength.SelectedValue));
                command.Parameters.AddWithValue("@tubeLengthText", ddlTubeLength.SelectedItem.Text);
                command.Parameters.AddWithValue("@straightOrCurvedId", Convert.ToInt32(radioStraightCurved.SelectedValue));
                command.Parameters.AddWithValue("@straightOrCurvedText", radioStraightCurved.SelectedItem.Text);
                command.Parameters.AddWithValue("@tubeDiameterId", Convert.ToInt32(ddlTubeDiameter.SelectedValue));
                command.Parameters.AddWithValue("@tubeDiameterText", ddlTubeDiameter.SelectedItem.Text);
                command.Parameters.AddWithValue("@ballastTypeId", Convert.ToInt32(ddlBallastType.SelectedValue));
                command.Parameters.AddWithValue("@ballastTypeText", ddlBallastType.SelectedItem.Text);
                command.Parameters.AddWithValue("@ballastsPerFixture", Convert.ToInt32(numberOfBallasts.Text));
                command.Parameters.AddWithValue("@fixtureControlId", Convert.ToInt32(ddlFixtureControl.SelectedValue));
                command.Parameters.AddWithValue("@fixtureControlText", ddlFixtureControl.SelectedItem.Text);
                command.Parameters.Add("@fixturePhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                command.Parameters.AddWithValue("@fixturePhotoFileName", DBNull.Value);
                command.Parameters.AddWithValue("@notes", notes.InnerText);
                command.Parameters.AddWithValue("@fixturesOnId", Convert.ToInt32(radioFixturesOn.SelectedValue));
                command.Parameters.AddWithValue("@fixturesOnText", radioFixturesOn.SelectedItem.Text);
                command.Parameters.AddWithValue("@userId", UserUid);
                connection.Open();
                command.ExecuteNonQuery();
                SuccessPanel.Visible = true;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }

        private void ClearInputFields()
        {
            ddlExternalLocation.SelectedValue = "-1";
            ddlFixtureUse.SelectedValue = "-1";
            numberOfFixtures.Text = String.Empty;
            ddlMountingType.SelectedValue = "-1";
            lampsPerFixture.Text = String.Empty;
            ddlLampType.SelectedValue = "-1";
            lampWattage.Text = String.Empty;
            baseType.Text = String.Empty;
            ddlTubeLength.SelectedValue = "-1";
            radioStraightCurved.SelectedIndex = -1;
            ddlTubeDiameter.SelectedValue = "-1";
            ddlBallastType.SelectedValue = "-1";
            numberOfBallasts.Text = String.Empty;
            ddlFixtureControl.SelectedValue = "-1";
            notes.InnerText = String.Empty;
            radioFixturesOn.SelectedIndex = -1;
        }
    }
}