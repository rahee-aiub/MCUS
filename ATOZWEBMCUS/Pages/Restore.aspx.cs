using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class Restore : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("master"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                //txtFrom.Text = Converter.GetString(dto.PrmBackUpPath);
                txtTo.Text = Converter.GetString(dto.PrmDataPath);
                CtrlFrom.Text = Converter.GetString(dto.PrmBackUpPath);

                string sourcePath = txtTo.Text;
                string targetPath = CtrlFrom.Text;

                if (!Directory.Exists(targetPath))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('BackUp Path Not Not Assign In Parameter');", true);

                    BtnRestore.Enabled = false;
                    return;
                }
                if (!Directory.Exists(sourcePath))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Path Not Not Assign In Parameter');", true);

                    BtnRestore.Enabled = false;
                    return;
                }

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date = dt2.DayOfWeek.ToString();

                txtFrom.ReadOnly = true;
                txtTo.ReadOnly = true;
                txtFrom.Text = string.Empty;
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,DatabaseName FROM A2ZDATABASE";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }
        protected void rbtSunday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSunday.Checked)
            {
                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Sun", "");

                rbtMonday.Checked = false;
                rbtTuesday.Checked = false;
                rbtWednesday.Checked = false;
                rbtThursday.Checked = false;
                rbtFriday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtMonday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMonday.Checked)
            {
                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Mon", "");
                rbtSunday.Checked = false;
                rbtTuesday.Checked = false;
                rbtWednesday.Checked = false;
                rbtThursday.Checked = false;
                rbtFriday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtTuesday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTuesday.Checked)
            {

                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Tue", "");
                rbtSunday.Checked = false;
                rbtMonday.Checked = false;
                rbtWednesday.Checked = false;
                rbtThursday.Checked = false;
                rbtFriday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtWednesday_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtWednesday.Checked)
            {

                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Wed", "");
                rbtSunday.Checked = false;
                rbtMonday.Checked = false;
                rbtTuesday.Checked = false;
                rbtThursday.Checked = false;
                rbtFriday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtThursday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtThursday.Checked)
            {

                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Thu", "");
                rbtSunday.Checked = false;
                rbtMonday.Checked = false;
                rbtTuesday.Checked = false;
                rbtWednesday.Checked = false;
                rbtFriday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtFriday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtFriday.Checked)
            {


                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Fri", "");
                rbtSunday.Checked = false;
                rbtMonday.Checked = false;
                rbtTuesday.Checked = false;
                rbtWednesday.Checked = false;
                rbtThursday.Checked = false;
                rbtSaturday.Checked = false;
            }
        }

        protected void rbtSaturday_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaturday.Checked)
            {

                txtFrom.Text = string.Format(@"{0}{1}\{2}", CtrlFrom.Text, "Sat", "");
                rbtSunday.Checked = false;
                rbtMonday.Checked = false;
                rbtTuesday.Checked = false;
                rbtWednesday.Checked = false;
                rbtThursday.Checked = false;
                rbtFriday.Checked = false;
            }
        }

        protected void DetachAttach()
        {
            con.Open();
            string DataItems = "";
            string BackItems = "";

            string targetPath = txtFrom.Text;
                       
            gvDetail();

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {

                Label lblDatabaseName = (Label)gvDetailInfo.Rows[i].Cells[1].FindControl("lblDatabase");

                DataItems = Converter.GetString(lblDatabaseName.Text);
                BackItems = DataItems + ".bak";

                string path = targetPath + BackItems;

                if (!Directory.Exists(targetPath) || !File.Exists(path))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('BackUp Does Not Exists');", true);
                  
                    return;
                }
                else
                {// 

                    var prm = new object[3];
                    prm[0] = DataItems;
                    prm[1] = txtFrom.Text;
                    prm[2] = BackItems;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_Restore", prm, "master"));
                    if (result == 0)
                    {

                    }
                
                }
            }
            con.Dispose();
            con.Close();
            SqlConnection.ClearAllPools();

            divRestore.Visible = false;

            Sucessfull();
        }

        protected void Sucessfull()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Database Restore Sucessfully Done');", true);
            return;

        }

        protected void InvalidInput()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select the Day');", true);
            return;

        }

        protected void BtnRestore_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == string.Empty)
            {
                InvalidInput();
            }
            else
            {
                DetachAttach();
                RestoreFlag.Text = "1";
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            if (RestoreFlag.Text == string.Empty)
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
            else
            {
                Response.Redirect("A2ZStartUp.aspx");
            }

        }


    }
}