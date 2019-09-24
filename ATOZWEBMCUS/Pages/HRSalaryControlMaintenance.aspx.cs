using System;
using DataAccessLayer.Utility;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRSalaryControlMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    GvInfoAllowence();
                    GvInfoDidaction();
                    GvInfoSlaary();

                    Allowence();
                    Didaction();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void GvInfoAllowence()
        {
            try
            {
                DivGridViewAllowence.Visible = true;
                string strQuery = string.Empty;
                strQuery = "SELECT Code,Description FROM A2ZALLOWANCE ";
                gvInformationAllowence = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvInformationAllowence, "A2ZHRMCUS");

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GvInfoAllowence Problem');</script>");
                //throw ex;
            }
        }

        protected void GvInfoDidaction()
        {
            try
            {
                DivGridViewDidaction.Visible = true;
                string strQuery = string.Empty;
                strQuery = "SELECT Code,Description FROM A2ZDEDUCTION";
                gvInformationDidaction = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvInformationDidaction, "A2ZHRMCUS");

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GvInfoDidaction Problem');</script>");
                //throw ex;
            }
        }

        protected void GvInfoSlaary()
        {
            try
            {
                DivGridViewDidaction.Visible = true;
                string strQuery = string.Empty;
                //strQuery = "SELECT * FROM  A2ZSALARYCONTROL WHERE  (CodeNo > 1)";
                strQuery = "SELECT * FROM  A2ZSALARYCONTROL";
                GridView1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, GridView1, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GvInfoSlaary Problem');</script>");
                //throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            try
            {
                string sqlQuery;
                int rowEffiect;
                int i = 0;
                int nCountA = 0;
                int nCountD = 0;

                //---------------------------------------------- For Allowence ---------------------------------------------------------------------

                sqlQuery = @"DELETE  FROM A2ZSALARYCONTROL";
                rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));


                i = 0;
                foreach (GridViewRow gv in gvInformationAllowence.Rows)
                {
                    Boolean m = ((CheckBox)gvInformationAllowence.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        nCountA++;
                    }
                    i++;
                }

                if (nCountA == 0 & nCountD == 0)
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Select any one module');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }

                i = 0;
                foreach (GridViewRow gv in gvInformationAllowence.Rows)
                {
                    Boolean m = ((CheckBox)gvInformationAllowence.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        sqlQuery = "INSERT INTO A2ZSALARYCONTROL (CodeHead,CodeNo) VALUES ('" + 1 + "','" + gvInformationAllowence.Rows[i].Cells[1].Text + "')";
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
                    }
                    i++;

                }

                //----------------------------------------------- For Didaction ---------------------------------------------------------------------


                i = 0;
                foreach (GridViewRow gv in gvInformationDidaction.Rows)
                {
                    Boolean m = ((CheckBox)gvInformationDidaction.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        nCountD++;
                    }
                    i++;
                }

                if (nCountA == 0 & nCountD == 0)
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Select any one module');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }


                i = 0;
                foreach (GridViewRow gv in gvInformationDidaction.Rows)
                {
                    Boolean m = ((CheckBox)gvInformationDidaction.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        sqlQuery = "INSERT INTO A2ZSALARYCONTROL (CodeHead,CodeNo) VALUES ('" + 2 + "','" + gvInformationDidaction.Rows[i].Cells[1].Text + "')";
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
                    }
                    i++;

                }

                GvInfoAllowence();
                GvInfoDidaction();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void btnExit_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }

        protected void Allowence()
        {
            try
            {

                string sqlQuery = "SELECT * FROM  A2ZSALARYCONTROL  WHERE CodeHead = '" + 1 + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHRMCUS");

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {

                        int Code = Converter.GetInteger(row["CodeNo"].ToString()) - 1;
                        //int Code = Converter.GetInteger(row["CodeNo"].ToString());
                        ((CheckBox)gvInformationAllowence.Rows[Code].FindControl("chkSelect")).Checked = true;
                    }
                }
                else
                {
                    GvInfoAllowence();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Allowence Problem');</script>");
                //throw ex;
            }

        }

        protected void Didaction()
        {
            try
            {

                string sqlQuery = "SELECT * FROM  A2ZSALARYCONTROL  WHERE CodeHead = '" + 2 + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHRMCUS");

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {
                        int Code = Converter.GetInteger(row["CodeNo"].ToString()) - 1;
                        ((CheckBox)gvInformationDidaction.Rows[Code].FindControl("chkSelect")).Checked = true;
                    }
                }
                else
                {
                    GvInfoDidaction();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Didaction Problem');</script>");
                //throw ex;
            }

        }

        protected void gvInformationAllowence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (gvInformationAllowence.Rows.Count == 1)
            //    gvInformationAllowence.Rows[0].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }

        }

        protected void gvInformationDidaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }
    }
}