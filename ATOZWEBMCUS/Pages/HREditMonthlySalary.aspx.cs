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
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREditMonthlySalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpNo.Focus();
                DivAllowance.Visible = false;
                divDeduction.Visible = false;

                var dt = A2ZHRPARAMETERDTO.GetParameterValue();

                DateTime processDate = dt.ProcessDate;

                hdnPeriod.Text = Converter.GetString(processDate);

                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", processDate));

                int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                var A = new DateTime(processDate.Year, processDate.Month, lastDay);

                hdnSalDate.Text = Converter.GetString(A);
                hdnNoDays.Text = Converter.GetString(lastDay);


                int Month = processDate.Month;
                int Year = processDate.Year;

                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);

            }
        }

        protected void gvAllowanceDetails()
        {
            WFA2ZALLOWANCE();

            string sqlquery3 = @"SELECT Code,Description FROM dbo.WFA2ZALLOWANCE";
            gvAllowanceInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvAllowanceInfo, "A2ZHRMCUS");
        }
        protected void gvDeductionDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZDEDUCTION Where status='True'";
            gvDeductionInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDeductionInfo, "A2ZHRMCUS");
        }


        protected void MoveAllowAmount()
        {

            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            for (int i = 0; i < gvAllowanceInfo.Rows.Count; i++)
            {
                Label lblAllowcode = (Label)gvAllowanceInfo.Rows[i].Cells[0].FindControl("lblAllowanceHead");
                Int16 Acode = Converter.GetSmallInteger(lblAllowcode.Text);
                TextBox txtAmount = (TextBox)gvAllowanceInfo.Rows[i].Cells[2].FindControl("txtAllowanceAmt");

                CheckBox lblChkT1Select = (CheckBox)gvAllowanceInfo.Rows[i].Cells[3].FindControl("chkT1Select");
                CheckBox lblChkR1Select = (CheckBox)gvAllowanceInfo.Rows[i].Cells[4].FindControl("chkR1Select");



                string sqlquery1 = "SELECT EmpCode,CodeHead,CodeNo,Amount,StatusT,StatusR FROM  dbo.A2ZEMPTSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND CodeHead=1 AND CodeNo='" + Acode + "' AND SalDate='" + hdnPeriod.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZHRMCUS");
                if (dt1.Rows.Count > 0)
                {
                    txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Amount"]));
                    EffectFlag1.Text = Converter.GetString(dt1.Rows[0]["StatusT"]);
                    EffectFlag2.Text = Converter.GetString(dt1.Rows[0]["StatusR"]);

                    if (EffectFlag1.Text == "1")
                    {
                        lblChkT1Select.Checked = true;
                    }
                    else
                    {
                        lblChkT1Select.Checked = false;
                    }

                    if (EffectFlag2.Text == "1")
                    {
                        lblChkR1Select.Checked = true;
                    }
                    else
                    {
                        lblChkR1Select.Checked = false;
                    }
                }
                else
                {
                    string strQuery = "INSERT INTO A2ZHRMCUS.dbo.A2ZEMPTSALARY(SalDate,EmpCode,CodeHead,CodeNo) VALUES('" + hdnPeriod.Text + "','" + txtEmpNo.Text + "',1,'" + Acode + "')";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }
        }

        protected void MoveDeductAmount()
        {

            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {
                Label lblDedcode = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("lblDeductionHead");
                Int16 Dcode = Converter.GetSmallInteger(lblDedcode.Text);
                TextBox txtAmount = (TextBox)gvDeductionInfo.Rows[i].Cells[2].FindControl("txtDeductionAmt");

                CheckBox lblChkT2Select = (CheckBox)gvDeductionInfo.Rows[i].Cells[3].FindControl("chkT2Select");
                CheckBox lblChkR2Select = (CheckBox)gvDeductionInfo.Rows[i].Cells[4].FindControl("chkR2Select");


                string sqlquery1 = "SELECT EmpCode,CodeHead,CodeNo,Amount,StatusT,StatusR FROM  dbo.A2ZEMPTSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND CodeHead=2 AND CodeNo='" + Dcode + "' AND SalDate='" + hdnPeriod.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZHRMCUS");
                if (dt1.Rows.Count > 0)
                {
                    txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Amount"]));
                    EffectFlag1.Text = Converter.GetString(dt1.Rows[0]["StatusT"]);
                    EffectFlag2.Text = Converter.GetString(dt1.Rows[0]["StatusR"]);


                    if (EffectFlag1.Text == "1")
                    {
                        lblChkT2Select.Checked = true;
                       
                    }
                    else
                    {
                        lblChkT2Select.Checked = false;
                       
                    }

                    if (EffectFlag2.Text == "1")
                    {
                        lblChkR2Select.Checked = true;
                        
                    }
                    else
                    {
                        lblChkR2Select.Checked = false;
                        
                    }
                }
                else
                {
                    string strQuery = "INSERT INTO A2ZHRMCUS.dbo.A2ZEMPTSALARY(SalDate,EmpCode,CodeHead,CodeNo) VALUES('" + hdnPeriod.Text + "','" + txtEmpNo.Text + "',2,'" + Dcode + "')";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
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


        protected void gvAllowanceInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); 

            GridViewRow row = gvAllowanceInfo.Rows[e.RowIndex];
            Label lblAllowanceCode = (Label)row.FindControl("lblAllowanceHead");
            TextBox txtAllowanceAmt = (TextBox)row.FindControl("txtAllowanceAmt");

            CheckBox lblChkT1Select = (CheckBox)row.FindControl("chkT1Select");
            CheckBox lblChkR1Select = (CheckBox)row.FindControl("chkR1Select");


            if (txtAllowanceAmt.Text != "00.00" && lblChkT1Select.Checked == false && lblChkR1Select.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Effect T / Effect R');", true);
                return;
            }



            if (lblChkT1Select.Checked == true)
            {
                EffectFlag1.Text = "1";
            }
            else
            {
                EffectFlag1.Text = "0";
            }

            if (lblChkR1Select.Checked == true)
            {
                EffectFlag2.Text = "1";
            }
            else
            {
                EffectFlag2.Text = "0";
            }
            string strqry1 = "UPDATE  A2ZEMPTSALARY SET Amount='" + txtAllowanceAmt.Text + "' , StatusT='" + EffectFlag1.Text + "',StatusR='" + EffectFlag2.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND CodeHead=1 AND CodeNo='" + lblAllowanceCode.Text + "' AND SalDate='" + hdnPeriod.Text + "'";
            int rowefect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry1, "A2ZHRMCUS"));

            gvAllowanceInfo.EditIndex = -1;
            gvAllowanceDetails();
            MoveAllowAmount();
            txtAllowanceAmt.Enabled = false;
            lblChkT1Select.Enabled = false;
            lblChkR1Select.Enabled = false;

            UpdateSalPostStat();
        }

        protected void gvAllowanceInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAllowanceInfo.EditIndex = e.NewEditIndex;
            gvAllowanceDetails();
            MoveAllowAmount();
            GridViewRow row = gvAllowanceInfo.Rows[gvAllowanceInfo.EditIndex];
            TextBox txtAllowanceAmt = (TextBox)row.FindControl("txtAllowanceAmt");
            CheckBox lblChkT1Select = (CheckBox)row.FindControl("chkT1Select");
            CheckBox lblChkR1Select = (CheckBox)row.FindControl("chkR1Select");

            gvAllowanceInfo.EditRowStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            txtAllowanceAmt.Enabled = true;
            lblChkT1Select.Enabled = true;
            lblChkR1Select.Enabled = true;
        }

        protected void gvAllowanceInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAllowanceInfo.EditIndex = -1;
            gvAllowanceDetails();
            MoveAllowAmount();
        }

        protected void gvDeductionInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

           

            GridViewRow row = gvDeductionInfo.Rows[e.RowIndex];
            Label lblDeductionCode = (Label)row.FindControl("lblDeductionHead");
            TextBox txtDeductionAmt = (TextBox)row.FindControl("txtDeductionAmt");

            CheckBox lblChkT2Select = (CheckBox)row.FindControl("chkT2Select");
            CheckBox lblChkR2Select = (CheckBox)row.FindControl("chkR2Select");

            if (txtDeductionAmt.Text != "00.00" && lblChkT2Select.Checked == false && lblChkR2Select.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Effect T / Effect R');", true);
                return;
            }
            

            if (lblChkT2Select.Checked == true)
            {
                EffectFlag1.Text = "1";
            }
            else
            {
                EffectFlag1.Text = "0";
            }

            if (lblChkR2Select.Checked == true)
            {
                EffectFlag2.Text = "1";
            }
            else
            {
                EffectFlag2.Text = "0";
            }

            string strqry1 = "UPDATE  A2ZEMPTSALARY SET Amount='" + txtDeductionAmt.Text + "' , StatusT='" + EffectFlag1.Text + "',StatusR='" + EffectFlag2.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND CodeHead=2 AND CodeNo='" + lblDeductionCode.Text + "' AND SalDate='" + hdnPeriod.Text + "'";
            int rowefect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry1, "A2ZHRMCUS"));


            gvDeductionInfo.EditIndex = -1;
            gvDeductionDetails();
            MoveDeductAmount();
            txtDeductionAmt.Enabled = false;
            lblChkT2Select.Enabled = false;
            lblChkR2Select.Enabled = false;

            UpdateSalPostStat();

        }

        protected void gvDeductionInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDeductionInfo.EditIndex = e.NewEditIndex;
            gvDeductionDetails();
            MoveDeductAmount();
            GridViewRow row = gvDeductionInfo.Rows[gvDeductionInfo.EditIndex];
            TextBox txtDeductionAmt = (TextBox)row.FindControl("txtDeductionAmt");
            CheckBox lblChkT2Select = (CheckBox)row.FindControl("chkT2Select");
            CheckBox lblChkR2Select = (CheckBox)row.FindControl("chkR2Select");


            gvDeductionInfo.EditRowStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            txtDeductionAmt.Enabled = true;
            lblChkT2Select.Enabled = true;
            lblChkR2Select.Enabled = true;

        }

        protected void gvDeductionInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDeductionInfo.EditIndex = -1;
            gvDeductionDetails();
            MoveDeductAmount();
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

                //InsertTSalaryRec();

                DivAllowance.Visible = true;
                divDeduction.Visible = true;
                gvAllowanceDetails();
                MoveAllowAmount();
                gvDeductionDetails();
                MoveDeductAmount();

            }
        }


        protected void UpdateSalPostStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateSalPostStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateSalPostStat Problem');</script>");
                //throw ex;
            }

        }
        protected void InsertTSalaryRec()
        {
            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string sqlquery15 = "SELECT EmpCode FROM dbo.A2ZEMPTSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND SalDate='" + hdnPeriod.Text + "'";
            DataTable dt15 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery15, "A2ZHRMCUS");
            if (dt15.Rows.Count == 0)
            {
                string strQuery = "INSERT INTO A2ZHRMCUS.dbo.A2ZEMPTSALARY(SalDate,EmpCode) VALUES('" + hdnPeriod.Text + "','" + txtEmpNo.Text + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
            }
        }


        private void WFA2ZALLOWANCE()
        {
            string sqlquery4 = "Truncate table dbo.WFA2ZALLOWANCE";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZHRMCUS"));

            string strQry = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES (98,'Basic')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZHRMCUS"));

            string strQry1 = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES (99,'Consolidated')";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry1, "A2ZHRMCUS"));


            string qry = "SELECT Code,Description from A2ZALLOWANCE Where Status='True'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["Code"].ToString();
                    var Desc = dr["Description"].ToString();
                    string strQuery = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES ('" + code + "','" + Desc + "')";
                    int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }

           
        }

        protected void chkT1Select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
               
                CheckBox T1select = (CheckBox)gvAllowanceInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT1Select");
                CheckBox R1select = (CheckBox)gvAllowanceInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR1Select");

                if (T1select.Checked)
                {
                    R1select.Checked = false;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }

        protected void chkR1Select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                CheckBox T1select = (CheckBox)gvAllowanceInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT1Select");
                CheckBox R1select = (CheckBox)gvAllowanceInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR1Select");

                if (R1select.Checked)
                {
                    T1select.Checked = false;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }


        protected void chkT2Select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                CheckBox T2select = (CheckBox)gvDeductionInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT2Select");
                CheckBox R2select = (CheckBox)gvDeductionInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR2Select");

                if (T2select.Checked)
                {
                    R2select.Checked = false;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }

        protected void chkR2Select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                CheckBox T2select = (CheckBox)gvDeductionInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT2Select");
                CheckBox R2select = (CheckBox)gvDeductionInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR2Select");

                if (R2select.Checked)
                {
                    T2select.Checked = false;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }



    }
}