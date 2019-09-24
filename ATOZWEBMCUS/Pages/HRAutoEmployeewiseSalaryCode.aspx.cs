using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRAutoEmployeewiseSalaryCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoPostMark();

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                       "click", @"<script>window.opener.location.href='HREmployeewiseSalaryCode.aspx'; self.close();</script>", false);

                //Response.Redirect("HREmployeewiseSalaryCode.aspx");             
            }
        }

        protected void gvAllowanceDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZALLOWANCE";
            gvAllowanceInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvAllowanceInfo, "A2ZHRMCUS");
        }
        protected void gvDeductionDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZDEDUCTION";
            gvDeductionInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDeductionInfo, "A2ZHRMCUS");
        }

       
       
        

        protected void gvAllowanceInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDeductionInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

       
        protected void AutoPostMark()
        {
             gvAllowanceDetails();
             gvDeductionDetails();
            
            
            string qry = "SELECT Id,EmpCode FROM A2ZEMPLOYEE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var id = dr["Id"].ToString();
                    var empcode = dr["EmpCode"].ToString();

                    lblEmpCode.Text = Converter.GetString(empcode);

                    UpdateAllowChkMark();
                    UpdateDedChkMark();
                }
            }
        }


        protected void UpdateAllowChkMark()
        {
            string sqlquery;
            sqlquery = @"DELETE dbo.A2ZEMPWISEALLOWCODE WHERE EmpCode='" + lblEmpCode.Text + "'";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZHRMCUS"));

            for (int i = 0; i < gvAllowanceInfo.Rows.Count; i++)
            {
                Label lblAllowcode = (Label)gvAllowanceInfo.Rows[i].Cells[0].FindControl("lblAllowanceHead");
                Int16 Acode = Converter.GetSmallInteger(lblAllowcode.Text);
                string desc = Converter.GetString(gvAllowanceInfo.Rows[i].Cells[1].Text);
                CheckBox chk = (CheckBox)gvAllowanceInfo.Rows[i].Cells[0].FindControl("chk1Status");

                chk.Checked = true;

                Boolean A = Converter.GetBoolean(chk.Checked);

                string strQuery = "INSERT INTO A2ZEMPWISEALLOWCODE(EmpCode,Code,Description,Status) VALUES ('" + lblEmpCode.Text + "','" + Acode + "','" + desc + "','" + A + "')";
                int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));


            }
        }





        protected void UpdateDedChkMark()
        {
            string sqlquery;
            sqlquery = @"DELETE dbo.A2ZEMPWISEDEDCODE WHERE EmpCode='" + lblEmpCode.Text + "'";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZHRMCUS"));

            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {

                Label lblDedcode = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("lblDeductionHead");
                Int16 Dcode = Converter.GetSmallInteger(lblDedcode.Text);
                string desc = Converter.GetString(gvDeductionInfo.Rows[i].Cells[1].Text);
                CheckBox chk = (CheckBox)gvDeductionInfo.Rows[i].Cells[0].FindControl("chk2Status");

                chk.Checked = true;

                Boolean A = Converter.GetBoolean(chk.Checked);

                string strQuery = "INSERT INTO A2ZEMPWISEDEDCODE(EmpCode,Code,Description,Status) VALUES ('" + lblEmpCode.Text + "','" + Dcode + "','" + desc + "','" + A + "')";
                int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));


            }
        }

    }
}