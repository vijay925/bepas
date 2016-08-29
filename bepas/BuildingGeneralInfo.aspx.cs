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
    public partial class WebForm3 : System.Web.UI.Page
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
                    command.Parameters.AddWithValue("@uid", 8);
                    adapter.Fill(dataSet);
                    ddlBuildingEndUse.DataSource = dataSet;
                    ddlBuildingEndUse.DataTextField = "value";
                    ddlBuildingEndUse.DataValueField = "uid";
                    ddlBuildingEndUse.DataBind();
                } //using SqlDataAdapter
            } //using SqlCommand

            ddlBuildingEndUse.Items.Insert(0, new ListItem("Please Select", "-1"));
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
            buildingId.Text = String.Empty;
            buildingName.Text = String.Empty;
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

        private void LoadBuildingList(int siteUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadBuildings", "@siteUid", siteUid);
            gvBuildingList.DataSource = dataSet;
            gvBuildingList.DataBind();
            gvBuildingList.HeaderRow.TableSection = TableRowSection.TableHeader;
        } //LoadBuildingList()

        protected void gvBuildingListOnRowCommandSelect(object sender, GridViewCommandEventArgs e)
        {
            string[] argument = new string[3];
            argument = e.CommandArgument.ToString().Split(';');
            SuccessPanel.Visible = false;

            string buildingUidLocal = argument[0];
            string buildingIdByUserLocal = argument[1];
            string buildingNameLocal = argument[2];

            ViewState["buildingUid"] = buildingUidLocal;
            buildingId.Text = buildingIdByUserLocal;
            buildingName.Text = buildingNameLocal;
            LoadInputFields(Convert.ToInt32(buildingUidLocal));
        }

        private void LoadInputFields(int buildingUid)
        {
            DataSet dataSet = GetDataUsingSp("spLoadBuildingGeneralInfo", "@buildingUid", buildingUid);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataSet.Tables[0].Rows[0];
                yearBuilt.Text = dr["yearBuilt"].ToString(); ;
                ddlBuildingEndUse.SelectedValue = dr["buildingEndUseId"].ToString();
                radioBoxedShape.SelectedValue = dr["boxedShapeId"].ToString();
                grossAreaPerFloor.Text = dr["grossAreaPerFloor"].ToString();
                buildingHeight.Text = dr["buildingHeight"].ToString();
                buildingWidth.Text = dr["buildingWidth"].ToString();
                buildingLength.Text = dr["buildingLength"].ToString();
                numberOfHVAC.Text = dr["numberOfHVAC"].ToString();
                radioOwnedOrLeased.SelectedValue = dr["ownedOrLeasedId"].ToString();
                numberOfFloors.Text = dr["numberOfFloors"].ToString();
                radioPreviousAudit.SelectedValue = dr["previousAuditId"].ToString();
                previousAuditDate.Text = dr["previousAuditDate"].ToString();
                meterId.Text = dr["meterId"].ToString();
                radioMeteredIndividually.SelectedValue = dr["meteredIndividuallyId"].ToString();
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
                    command.CommandText = "spUpdateBuildingInfo";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@buildingUid", Convert.ToInt32(ViewState["buildingUid"]));
                    command.Parameters.AddWithValue("@buildingIdByUser", buildingId.Text);
                    command.Parameters.AddWithValue("@buildingName", buildingName.Text);
                    command.Parameters.Add("@birdsEyeSketch", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@birdsEyeSketchFileName", DBNull.Value);
                    command.Parameters.Add("@profileSketch", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@profileSketchFileName", DBNull.Value);
                    command.Parameters.Add("@floorPlanSketch", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@floorPlanSketchFileName", DBNull.Value);
                    command.Parameters.Add("@buildingPhoto", SqlDbType.VarBinary).Value = DBNull.Value;
                    command.Parameters.AddWithValue("@buildingPhotoFileName", DBNull.Value);
                    command.Parameters.AddWithValue("@yearBuilt", Convert.ToInt32(yearBuilt.Text));
                    command.Parameters.AddWithValue("@buildingEndUseId", ddlBuildingEndUse.SelectedValue);
                    command.Parameters.AddWithValue("@buildingEndUseText", ddlBuildingEndUse.SelectedItem.Text);
                    command.Parameters.AddWithValue("@boxedShapeId", Convert.ToInt32(radioBoxedShape.SelectedValue));
                    command.Parameters.AddWithValue("@boxedShapeText", radioBoxedShape.SelectedItem.Text);
                    command.Parameters.AddWithValue("@grossAreaPerFloor", grossAreaPerFloor.Text);
                    command.Parameters.AddWithValue("@buildingHeight", buildingHeight.Text);
                    command.Parameters.AddWithValue("@buildingWidth", buildingWidth.Text);
                    command.Parameters.AddWithValue("@buildingLength", buildingLength.Text);
                    command.Parameters.AddWithValue("@numberOfHVAC", Convert.ToInt32(numberOfHVAC.Text));
                    command.Parameters.AddWithValue("@ownedOrLeasedId", Convert.ToInt32(radioOwnedOrLeased.SelectedValue));
                    command.Parameters.AddWithValue("@ownedOrLeasedText", radioOwnedOrLeased.SelectedItem.Text);
                    command.Parameters.AddWithValue("@numberOfFloors", Convert.ToInt32(numberOfFloors.Text));
                    command.Parameters.AddWithValue("@previousAuditId", Convert.ToInt32(radioPreviousAudit.SelectedValue));
                    command.Parameters.AddWithValue("@previousAuditText", radioPreviousAudit.SelectedItem.Text);
                    command.Parameters.AddWithValue("@previousAuditDate", previousAuditDate.Text);
                    command.Parameters.AddWithValue("@meterId", meterId.Text);
                    command.Parameters.AddWithValue("@meteredIndividuallyId", Convert.ToInt32(radioMeteredIndividually.SelectedValue));
                    command.Parameters.AddWithValue("@meteredIndividuallyText", radioMeteredIndividually.SelectedItem.Text);
                    command.Parameters.AddWithValue("@notes", notes.InnerText);
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
            } // if(page valid)

        } //saveButton_Click()

        private void ClearInputFields()
        {
            yearBuilt.Text = String.Empty;
            ddlBuildingEndUse.SelectedValue = "-1";
            radioBoxedShape.SelectedIndex = -1;
            grossAreaPerFloor.Text = String.Empty;
            buildingHeight.Text = String.Empty;
            buildingWidth.Text = String.Empty;
            buildingLength.Text = String.Empty;
            numberOfHVAC.Text = String.Empty;
            radioOwnedOrLeased.SelectedIndex = -1;
            numberOfFloors.Text = String.Empty;
            radioPreviousAudit.SelectedIndex = -1;
            previousAuditDate.Text = String.Empty;
            meterId.Text = String.Empty;
            radioMeteredIndividually.SelectedIndex = -1;
            notes.Value = String.Empty;
        }
    } //Webform
} //namespace bepas