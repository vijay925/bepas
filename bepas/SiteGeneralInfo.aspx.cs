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
    public partial class SiteGeneralInfo : System.Web.UI.Page
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
                    command.Parameters.AddWithValue("@uid", 17);
                    adapter.Fill(dataSet);
                    ddlState.DataSource = dataSet;
                    ddlState.DataTextField = "value";
                    ddlState.DataValueField = "uid";
                    ddlState.DataBind();

                } //using SqlDataAdapter
            } //using SqlCommand

            ddlState.Items.Insert(0, new ListItem("Please Select", "-1"));
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
            DataSet dataSet = GetDataUsingSp("spLoadSiteGeneralInfo", "@siteUid", siteUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                siteId.Text = dr["siteIdByUser"].ToString();
                siteName.Text = dr["siteName"].ToString();
                contactName.Text = dr["contactName"].ToString();
                contactNumber.Text = dr["contactNumber"].ToString();
                contactEmail.Text = dr["contactEmail"].ToString();
                address1.Text = dr["address1"].ToString();
                address2.Text = dr["address2"].ToString();
                city.Text = dr["city"].ToString();
                ddlState.SelectedValue = dr["stateId"].ToString();
                zipCode.Text = dr["zipCode"].ToString();
                surveyDate.Text = dr["surveyDate"].ToString();
                frpmAmount.Text = dr["frpmAmount"].ToString();
                radioDuringOrAfter.SelectedValue = dr["duringOrAfterId"].ToString();
                radioKeyAccess.SelectedValue = dr["keyAccessId"].ToString();
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
                command.CommandText = "spUpdateSiteInfo";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("@siteUid", Convert.ToInt32(ViewState["siteUid"]));
                command.Parameters.AddWithValue("@siteIdByUser", siteId.Text);
                command.Parameters.AddWithValue("@siteName", siteName.Text);
                command.Parameters.AddWithValue("@contactName", contactName.Text);
                command.Parameters.AddWithValue("@contactNumber", contactNumber.Text);
                command.Parameters.AddWithValue("@contactEmail", contactEmail.Text);
                command.Parameters.AddWithValue("@address1", address1.Text);
                command.Parameters.AddWithValue("@address2", address2.Text);
                command.Parameters.AddWithValue("@city", city.Text);
                command.Parameters.AddWithValue("@stateId", Convert.ToInt32(ddlState.SelectedValue));
                command.Parameters.AddWithValue("@stateText", ddlState.SelectedItem.Text);
                command.Parameters.AddWithValue("@zipCode", zipCode.Text);
                command.Parameters.AddWithValue("@surveyDate", surveyDate.Text);
                command.Parameters.AddWithValue("@frpmAmount", Convert.ToDouble(frpmAmount.Text));
                command.Parameters.AddWithValue("@duringOrAfterId", Convert.ToInt32(radioDuringOrAfter.SelectedValue));
                command.Parameters.AddWithValue("@duringOrAfterText", radioDuringOrAfter.SelectedItem.Text);
                command.Parameters.AddWithValue("@keyAccessId", Convert.ToInt32(radioKeyAccess.SelectedValue));
                command.Parameters.AddWithValue("@keyAccessText", radioKeyAccess.SelectedItem.Text);
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
            siteId.Text = String.Empty;
            siteName.Text = String.Empty;
            contactName.Text = String.Empty;
            contactNumber.Text = String.Empty;
            contactEmail.Text = String.Empty;
            address1.Text = String.Empty;
            address2.Text = String.Empty;
            siteName.Text = String.Empty;
            city.Text = String.Empty;
            ddlState.SelectedValue = "-1";
            zipCode.Text = String.Empty;
            surveyDate.Text = String.Empty;
            frpmAmount.Text = String.Empty;
            radioDuringOrAfter.SelectedIndex = -1;
            radioKeyAccess.SelectedIndex = -1;
        }


    }
}