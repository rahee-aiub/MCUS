using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmpLeaveMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpNo.Focus();
                dropdown();
                ddllType.Enabled = false;
                txtStartDate.Enabled = false;
                txtEndDate.Enabled = false;
                BtnView.Visible = false;
            }
        }

        private void dropdown()
        {
            string sqlquery = "SELECT EmpleaveCode,EmpleaveName from A2ZEMPLEAVETYPE WHERE Status = 1";
            ddllType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddllType, "A2ZHRMCUS");
        }


        protected void gvDetail()
        {
            try
            {
                string sqlquery4 = "SELECT Id,LStartDate,LEndDate,LApplyDays,LBalance FROM A2ZEMPLEAVE WHERE EmpCode= '" + txtEmpNo.Text + "' AND EmpLeaveCode= '" + ddllType.SelectedValue + "'";

                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetail Problem');</script>");
                //throw ex;
            }
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

                lblArea.Text = Converter.GetString(getDTO.EmpAreaDesc);
                lblLocation.Text = Converter.GetString(getDTO.EmpLocationDesc);



                BtnView.Visible = true;

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
                ddllType.Enabled = true;
                txtStartDate.Enabled = true;
                txtEndDate.Enabled = true;
                txtTotDay.Text = string.Empty;
                txtBalDay.Text = string.Empty;
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                ddllType.SelectedIndex = 0;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtStartDate.Text = date;
                txtEndDate.Text = date;
                lblProcDate.Text = date;
            }
            else
            {
                txtEmpNo.Text = string.Empty;
                txtEmpNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Employee No');", true);
                return;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {


            DateTime dt = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (dt1 < dt)
            {
                txtEndDate.Text = string.Empty;
                txtEndDate.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Date Input');", true);
                return;
            }


            int a = Convert.ToInt16(dt1.Subtract(dt).TotalDays + 1);
            decimal avalday = Converter.GetDecimal(txtBalDay.Text);


            if (a > avalday && ddllType.SelectedValue != "8")
            {
                txtEndDate.Text = string.Empty;
                txtEndDate.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Excess Leave Days');", true);
                return;
            }

            A2ZEMPLEAVEDTO dto = new A2ZEMPLEAVEDTO();
            dto.EmpCode = Converter.GetInteger(txtEmpNo.Text);
            dto.EmpLeaveCode = Converter.GetInteger(ddllType.SelectedValue);
            dto.EmpLBalance = Converter.GetDecimal(lblBalDays.Text);

            dto.LPurpose = Converter.GetString(txtLPurpose.Text);
            dto.LNote = Converter.GetString(txtLNote.Text);
            dto.LProcStat = Converter.GetSmallInteger(1);


            if (txtStartDate.Text != string.Empty)
            {
                DateTime startdate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dto.LStartDate = startdate;

            }
            else
            {
                string CheckstartDtNull = "";
                dto.NullLStartDate = CheckstartDtNull;

            }
            if (txtEndDate.Text != string.Empty)
            {
                DateTime enddate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dto.LEndDate = enddate;

            }
            else
            {
                string CheckEndDtNull = "";
                dto.NullLEndDate = CheckEndDtNull;

            }
            dto.EmpApplyDays = Converter.GetInteger(txtDay.Text);
            int roweffect = A2ZEMPLEAVEDTO.InsertInformation(dto);
            if (roweffect > 0)
            {
                UpdLeaveBalance();
                txtEmpNo.Focus();

                clearinfo();

            }



        }

        protected void UpdLeaveBalance()
        {
            A2ZEMPLEAVEBALANCEDTO objDto = new A2ZEMPLEAVEBALANCEDTO();
            objDto.EmpCode = Converter.GetInteger(txtEmpNo.Text);
            objDto.LeaveYear = Converter.GetInteger(lblprocYear.Text);
            objDto.LeaveCode = Converter.GetInteger(ddllType.SelectedValue);
            objDto.LeaveDays = Converter.GetDecimal(lblUsedDays.Text);
            objDto.LeaveBalDays = Converter.GetDecimal(lblBalDays.Text);
            int result = A2ZEMPLEAVEBALANCEDTO.UpdateInformation(objDto);
        }

        private void clearinfo()
        {
            txtEmpNo.Text = string.Empty;
            txtBalDay.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtDay.Text = string.Empty;
            ddllType.SelectedIndex = 0;
            lblName.Text = string.Empty;
            lblDesign.Text = string.Empty;
            lblGrade.Text = string.Empty;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddllType_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZEMPLEAVETYPE WHERE  EmpleaveCode = '" + ddllType.SelectedValue + "'", "A2ZHRMCUS");
            if (dt.Rows.Count > 0)
            {

                lblEffectSalary.Text = Converter.GetString(dt.Rows[0]["EffectSalary"]);
                txtTotDay.Text = Converter.GetString(dt.Rows[0]["TotalDays"]);
            }

            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            int year = date.Year;
            lblprocYear.Text = Converter.GetString(year);
            int empcode = Converter.GetInteger(txtEmpNo.Text);
            int lyear = Converter.GetInteger(year);
            int lcode = Converter.GetInteger(ddllType.SelectedValue);

            A2ZEMPLEAVEBALANCEDTO getDTO = (A2ZEMPLEAVEBALANCEDTO.GetInformation(empcode, lyear, lcode));
            if (getDTO.norecord > 0)
            {
                txtBalDay.Text = Converter.GetString(getDTO.LeaveBalDays);
                lblUsedDays.Text = Converter.GetString(getDTO.LeaveDays);
            }
            else
            {
                txtBalDay.Text = string.Empty;
                lblUsedDays.Text = string.Empty;
            }
        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {

            if (txtStartDate.Text != string.Empty)
            {
                DateTime dt = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt1 = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (dt1 < dt)
                {
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Date Input');", true);
                    return;
                }


                int a = Convert.ToInt16(dt1.Subtract(dt).TotalDays + 1);
                decimal avalday = Converter.GetDecimal(txtBalDay.Text);

                if (lblEffectSalary.Text == "False")
                {
                    if (a > avalday && ddllType.SelectedValue != "8")
                    {

                        txtEndDate.Text = string.Empty;
                        txtEndDate.Focus();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Excess Leave Days');", true);
                        return;
                    }

                    else
                    {
                        txtDay.Text = Converter.GetString(a);

                        decimal calbalday = 0;
                        decimal usedday = 0;
                        decimal Totaldays = Converter.GetDecimal(txtBalDay.Text);
                        decimal ldays = Converter.GetDecimal(txtDay.Text);
                        decimal Udays = Converter.GetDecimal(lblUsedDays.Text);

                        calbalday = (Totaldays - ldays);

                        usedday = (Udays + ldays);

                        lblBalDays.Text = Converter.GetString(calbalday);
                        lblUsedDays.Text = Converter.GetString(usedday);
                    }
                }
                else
                {
                    txtDay.Text = Converter.GetString(a);
                    return;
                }
            }
            else
            {
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                txtStartDate.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Start Date Not inputed');", true);
                return;
            }


        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (txtEmpNo.Text != string.Empty && ddllType.SelectedValue != "-Select-")
            {
                gvDetail();
                gvDetailInfo.Visible = true;
            }
            else
            {
                ddllType.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Leave Type For View History');", true);
                return;


            }
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            DateTime inputDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string input = inputDate.ToString("MM/dd/yyyy");
            string qry = @"SELECT LStartDate,LEndDate FROM	A2ZEMPLEAVE WHERE EmpCode='" + txtEmpNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            if (dt.Rows.Count > 0)
            {
                DateTime lstardate = Converter.GetDateTime(dt.Rows[0]["LStartDate"]);
                string stdt = lstardate.ToString("MM/dd/yyyy");
                DateTime lenddate = Converter.GetDateTime(dt.Rows[0]["LEndDate"]);
                string enddt = lenddate.ToString("MM/dd/yyyy");
                int a = Convert.ToInt16(lenddate.Subtract(lstardate).TotalDays + 1);
                string qry1 = @"SELECT LStartDate FROM A2ZEMPLEAVE WHERE LStartDate BETWEEN '" + stdt + "' AND '" + input + "' AND LEndDate BETWEEN '" + input + "' AND '" + enddt + "' ";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");

                if (dt1.Rows.Count > 0)
                {
                    txtStartDate.Text = string.Empty;
                    txtStartDate.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Employee has already been taken leave by this Date');", true);
                    return;
                }
            }
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");

                string qry = @"SELECT LStartDate,LEndDate FROM	A2ZEMPLEAVE WHERE Id='" + lId.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    DateTime lstardate = Converter.GetDateTime(dt.Rows[0]["LStartDate"]);
                    string stdt = lstardate.ToString("MM/dd/yyyy");
                    DateTime lenddate = Converter.GetDateTime(dt.Rows[0]["LEndDate"]);
                    string enddt = lenddate.ToString("MM/dd/yyyy");

                    DateTime procdt = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    if (lstardate.Month < procdt.Month || lstardate.Year < procdt.Year)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Delete');", true);
                        return;
                    }
                }


                string depositQry = "DELETE dbo.A2ZEMPLEAVE WHERE Id='" + lId.Text + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZHRMCUS"));


                gvDetail();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

    }
}