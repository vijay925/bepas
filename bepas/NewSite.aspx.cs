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
    public partial class NewSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadDropdownItems();

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

        protected void addButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bepas"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand())
            {
                int UserUid = 1;
                command.CommandText = "spInsertSite";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
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
                command.Parameters.Add("@mapFile", SqlDbType.VarBinary).Value = DBNull.Value;
                command.Parameters.AddWithValue("@mapFileName", DBNull.Value);
                command.Parameters.AddWithValue("@creatorId", UserUid);
                command.Parameters.AddWithValue("@creatorName", DBNull.Value);
                command.Parameters.AddWithValue("@creationTime", DBNull.Value);
                command.Parameters.AddWithValue("@lastModifierId", UserUid);
                command.Parameters.AddWithValue("@lastModifierName", DBNull.Value);
                command.Parameters.AddWithValue("@lastModifiedTime", DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
                SuccessPanel.Visible = true;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }
    }
}