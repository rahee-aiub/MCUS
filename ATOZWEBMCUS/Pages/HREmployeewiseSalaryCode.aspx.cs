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
    public partial class HREmployeewiseSalaryCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpNo.Focus();
                gvAllowanceInfo.Visible = false;
                gvDeductionInfo.Visible = false;
                DivAllowance.Visible = false;
                divDeduction.Visible = false;
                
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

        protected void MoveAllowChkMark()
        {

            for (int i = 0; i < gvAllowanceInfo.Rows.Count; i++)
            {
                Label lblAllowcode = (Label)gvAllowanceInfo.Rows[i].Cells[0].FindControl("lblAllowanceHead");
                Int16 Acode = Converter.GetSmallInteger(lblAllowcode.Text);
                string sqlquery = "SELECT EmpCode,Status from A2ZEMPWISEALLOWCODE WHERE Code='" + Acode + "' AND EmpCode='" + txtEmpNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    string stat = Converter.GetString(dt.Rows[0]["Status"]);
                    CheckBox chk = (CheckBox)gvAllowanceInfo.Rows[i].Cells[0].FindControl("chk1Status");

                    chk.Checked = true;

                    //if (stat == "True")
                    //{
                    //    chk.Checked = true;
                    //}
                    //else
                    //{
                    //    chk.Checked = false;
                    //}
                }

            }
        }

        protected void MoveDedChkMark()
        {

            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {
                Label lblDedcode = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("lblDeductionHead");
                Int16 Dcode = Converter.GetSmallInteger(lblDedcode.Text);
                string sqlquery = "SELECT EmpCode,Status from A2ZEMPWISEDEDCODE WHERE Code='" + Dcode + "' AND EmpCode='" + txtEmpNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    string stat = Converter.GetString(dt.Rows[0]["Status"]);
                    CheckBox chk = (CheckBox)gvDeductionInfo.Rows[i].Cells[0].FindControl("chk2Status");

                    chk.Checked = true;

                    //if (stat == "True")
                    //{
                    //    chk.Checked = true;
                    //}
                    //else
                    //{
                    //    chk.Checked = false;
                    //}
                    
                }

            }
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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void UpdateAllowChkMark()
        {
            string sqlquery;
            sqlquery = @"DELETE dbo.A2ZEMPWISEALLOWCODE WHERE EmpCode='" + txtEmpNo.Text + "'";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZHRMCUS"));
            
            for (int i = 0; i < gvAllowanceInfo.Rows.Count; i++)
            {
                Label lblAllowcode = (Label)gvAllowanceInfo.Rows[i].Cells[0].FindControl("lblAllowanceHead");
                Int16 Acode = Converter.GetSmallInteger(lblAllowcode.Text);
                string desc = Converter.GetString(gvAllowanceInfo.Rows[i].Cells[1].Text);
                CheckBox chk = (CheckBox)gvAllowanceInfo.Rows[i].Cells[0].FindControl("chk1Status");
                Boolean A = Converter.GetBoolean(chk.Checked);
                
                string strQuery = "INSERT INTO A2ZEMPWISEALLOWCODE(EmpCode,Code,Description,Status) VALUES ('" + txtEmpNo.Text + "','" + Acode + "','" + desc + "','" + A + "')";
                int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                

            }
        }

        protected void UpdateDedChkMark()
        {
            string sqlquery;
            sqlquery = @"DELETE dbo.A2ZEMPWISEDEDCODE WHERE EmpCode='" + txtEmpNo.Text + "'";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZHRMCUS"));
           
            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {

                Label lblDedcode = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("lblDeductionHead");
                Int16 Dcode = Converter.GetSmallInteger(lblDedcode.Text);
                string desc = Converter.GetString(gvDeductionInfo.Rows[i].Cells[1].Text);
                CheckBox chk = (CheckBox)gvDeductionInfo.Rows[i].Cells[0].FindControl("chk2Status");
                Boolean A = Converter.GetBoolean(chk.Checked);
               
                string strQuery = "INSERT INTO A2ZEMPWISEDEDCODE(EmpCode,Code,Description,Status) VALUES ('" + txtEmpNo.Text + "','" + Dcode + "','" + desc + "','" + A + "')";
                int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                

            }
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAllowChkMark();
            UpdateDedChkMark();
            gvAllowanceInfo.Visible = false;
            gvDeductionInfo.Visible = false;
            DivAllowance.Visible = false;
            divDeduction.Visible = false;
            
            lblName.Text = string.Empty;
            lblDesign.Text = string.Empty;
            lblGrade.Text = string.Empty;
            txtEmpNo.Text = string.Empty;
            txtEmpNo.Focus();

        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            int EmpID = Converter.GetInteger(txtEmpNo.Text);
            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
            if (getDTO.EmployeeID > 0)
            {
                lblName.Text = Converter.GetString(getDTO.EmployeeName);
                CtrlDesignation.Text = Converter.GetString(getDTO.EmpDesignation);
                CtrlGrade.Text = Converter.GetString(getDTO.EmpGrade);

                Int16 Desig = Converter.GetSmallInteger(CtrlDesignation.Text);
                A2ZDESIGNATIONDTO get1DTO = (A2ZDESIGNATIONDTO.GetInformation(Desig));
                if (get1DTO.DesignationCode > 0)
                {
                    lblDesign.Text = Converter.GetString(get1DTO.DesignationDescription);
                }

                Int16 Grade = Converter.GetSmallInteger(CtrlGrade.Text);
                A2ZGRADEDTO get2DTO = (A2ZGRADEDTO.GetGradeInformation(Grade));
                if (get2DTO.ID > 0)
                {
                    lblGrade.Text = Converter.GetString(get2DTO.GradeDesc);
                }

                gvAllowanceInfo.Visible = true;
                gvDeductionInfo.Visible = true;
                DivAllowance.Visible = true;
                divDeduction.Visible = true;
                gvAllowanceDetails();
                gvDeductionDetails();
                MoveAllowChkMark();
                MoveDedChkMark();

            }
        }

        protected void BtnPost_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
         "click", @"<script>window.open('HRAutoEmployeewiseSalaryCode.aspx','_blank');</script>", false);

        }

    }
}