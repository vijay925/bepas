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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadDropdownItems();

            } //if

        } //Page_Load()

        /*
        private void OldLoadDropdownItems()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "spLoadDropdownItems";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    connection.Open();
                    command.Parameters.AddWithValue("@uid", 36);
                    ddlFixtureUse.DataSource = command.ExecuteReader();
                    ddlFixtureUse.DataTextField = "value";
                    ddlFixtureUse.DataValueField = "uid";
                    ddlFixtureUse.DataBind();

                    command.Parameters.AddWithValue("@uid", 37);
                    ddlMountingType.DataSource = command.ExecuteReader();
                    ddlMountingType.DataTextField = "value";
                    ddlMountingType.DataValueField = "uid";
                    ddlMountingType.DataBind();
                } //using SqlCommand
            } //using SqlConnection

            ddlFixtureUse.Items.Insert(0, new ListItem("Please Select", "-1"));
        }
        */


        

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
                    command.Parameters.AddWithValue("@uid", 36);
                    adapter.Fill(dataSet);
                    ddlFixtureUse.DataSource = dataSet;
                    ddlFixtureUse.DataTextField = "value";
                    ddlFixtureUse.DataValueField = "uid";
                    ddlFixtureUse.DataBind();

                    command.Parameters["@uid"].Value = 37;
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

            ddlFixtureUse.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlMountingType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlLampType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeLength.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlTubeDiameter.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlBallastType.Items.Insert(0, new ListItem("Please Select", "-1"));
            ddlFixtureControl.Items.Insert(0, new ListItem("Please Select", "-1"));
            
        } //LoadDropdownItems()

        protected void saveButton_Click(object sender, EventArgs e)
        {

        }



        
        protected void buildingId_TextChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "spLoadBuildingExLighting";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        command.Parameters.AddWithValue("@buildingUid", 1);
                        adapter.Fill(dataSet);
                    } //using SqlDataAdapter
                }//using SqlCommand
            } //using SqlConnection 
             
            if(dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                ddlFixtureUse.SelectedValue = dr["fixtureId"].ToString();
                numberOfFixtures.Text = dr["numberOfFixtures"].ToString();
                ddlMountingType.SelectedValue = dr["mountingTypeId"].ToString();
                lampsPerFixture.Text = dr["lampsPerFixture"].ToString();
                ddlLampType.SelectedValue = dr["lampTypeId"].ToString();
                lampWattage.Text = dr["lampWattage"].ToString();
                baseType.Text = dr["lampBaseType"].ToString();
                ddlTubeLength.SelectedValue = dr["tubeLengthId"].ToString();
                int radioValue = Convert.ToInt32(dr["straightOrCurvedId"]);
                if (radioValue == 1)
                    radioStraight.Checked = true;
                else if(radioValue == 2)
                    radioCurved.Checked = true;
                ddlTubeDiameter.SelectedValue = dr["tubeDiameterId"].ToString();
                ddlBallastType.SelectedValue = dr["ballastTypeId"].ToString();
                ballastsPerFixture.Text = dr["ballastsPerFixture"].ToString();
                ddlFixtureControl.SelectedValue = dr["fixtureControlId"].ToString();
                notes.Value = dr["notes"].ToString();
            }
    } //buildingId_TextChanged()
    } //Webform
} //namespace bepas