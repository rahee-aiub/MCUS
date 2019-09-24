using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.HumanResource;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREditMonthlySalaryByCode : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    gvCodeDetailInfo.Visible = false;
                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;
                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    var dt = A2ZHRPARAMETERDTO.GetParameterValue();

                    DateTime processDate = dt.ProcessDate;

                    hdnPeriod.Text = Converter.GetString(processDate);

                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", processDate));

                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
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
        protected void ddlAllowanceDetails()
        {
            WFA2ZALLOWANCE();

            string sqlquery3 = @"SELECT Code,Description FROM dbo.WFA2ZALLOWANCE";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery3, ddlCode, "A2ZHRMCUS");

        }
        protected void ddlDeductionDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZDEDUCTION Where status='True'";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery3, ddlCode, "A2ZHRMCUS");

        }



        private void gvVerify()
        {
            string sqlquery3 = "SELECT EmpCode,EmpName from A2ZEMPLOYEE WHERE Status=1 or Status = 99";
            gvCodeDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCodeDetailInfo, "A2ZHRMCUS");
        }




        protected void CertificateNoMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Certificate No.');", true);
            return;

        }

        protected void UpdateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Update Sucessfully Completed');", true);
            return;

        }

        protected void BtnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvCodeDetailInfo.Visible == true)
                {
                    for (int i = 0; i < gvCodeDetailInfo.Rows.Count; i++)
                    {
                        Label lempcode = (Label)gvCodeDetailInfo.Rows[i].Cells[0].FindControl("lblEmpCode");

                        TextBox txtAmt = (TextBox)gvCodeDetailInfo.Rows[i].Cells[2].FindControl("txtAmount");
                        CheckBox lblChkT1Select = (CheckBox)gvCodeDetailInfo.Rows[i].Cells[3].FindControl("chkT1Select");
                        CheckBox lblChkR1Select = (CheckBox)gvCodeDetailInfo.Rows[i].Cells[4].FindControl("chkR1Select");

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


                        if (txtAmt.Text != "00.00" && (EffectFlag1.Text != "0" || EffectFlag2.Text != "0"))
                        {

                            string sqlquery1 = "SELECT EmpCode,Amount,StatusT,StatusR FROM  dbo.A2ZEMPTSALARY WHERE EmpCode='" + lempcode.Text + "' AND CodeHead='" + ddlCodeType.SelectedValue + "' AND CodeNo='" + ddlCode.SelectedValue + "' AND SalDate='" + hdnPeriod.Text + "'";
                            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZHRMCUS");
                            if (dt1.Rows.Count > 0)
                            {
                                string ChkUp = "UPDATE A2ZEMPTSALARY SET Amount = '" + txtAmt.Text + "', StatusT = '" + EffectFlag1.Text + "', StatusR = '" + EffectFlag2.Text + "' WHERE EmpCode='" + lempcode.Text + "' AND CodeHead='" + ddlCodeType.SelectedValue + "' AND CodeNo='" + ddlCode.SelectedValue + "' AND SalDate='" + hdnPeriod.Text + "'";
                                int rEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ChkUp, "A2ZHRMCUS"));
                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZHRMCUS.dbo.A2ZEMPTSALARY(SalDate,EmpCode,CodeHead,CodeNo,Amount,StatusT,StatusR) VALUES('" + hdnPeriod.Text + "','" + lempcode.Text + "','" + ddlCodeType.SelectedValue + "','" + ddlCode.SelectedValue + "','" + txtAmt.Text + "','" + EffectFlag1.Text + "','" + EffectFlag2.Text + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                            }
                        }
                    }

                    UpdateMSG();
                    gvCodeDetailInfo.Visible = false;
                    gvVerify();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnApprove_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void gvCodeDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            if (ddlCode.SelectedValue != "-Select-")
            {


                //var prm = new object[4];
                //prm[0] = lblID.Text;
                //prm[1] = hdnPeriod.Text;
                //prm[2] = ddlCodeType.SelectedValue;
                //prm[3] = ddlCode.SelectedValue;


                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRGenerateEditMonthlySalary", prm, "A2ZHRMCUS"));
                //if (result == 0)
                //{
                gvCodeDetailInfo.Visible = true;
                gvVerify();
                MoveCodeAmount();
                SumValue();
                //}

            }
        }


        protected void MoveCodeAmount()
        {

            //DateTime Pdate = DateTime.ParseExact(hdnPeriod.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            for (int i = 0; i < gvCodeDetailInfo.Rows.Count; i++)
            {
                Label lblEmpNum = (Label)gvCodeDetailInfo.Rows[i].Cells[0].FindControl("lblEmpCode");
                TextBox txtAmount = (TextBox)gvCodeDetailInfo.Rows[i].Cells[2].FindControl("txtAmount");
                CheckBox lblChkT1Select = (CheckBox)gvCodeDetailInfo.Rows[i].Cells[3].FindControl("chkT1Select");
                CheckBox lblChkR1Select = (CheckBox)gvCodeDetailInfo.Rows[i].Cells[4].FindControl("chkR1Select");



                string sqlquery1 = "SELECT EmpCode,Amount,StatusT,StatusR FROM  dbo.A2ZEMPTSALARY WHERE EmpCode='" + lblEmpNum.Text + "' AND CodeHead='" + ddlCodeType.SelectedValue + "' AND CodeNo='" + ddlCode.SelectedValue + "' AND SalDate='" + hdnPeriod.Text + "'";
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
                    txtAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", 0));
                }
            }
        }


        protected void SumValue()
        {
            Decimal sumCr = 0;
            Decimal Amount = 0;
           
            int totrec = gvCodeDetailInfo.Rows.Count;
           
            for (int i = 0; i < gvCodeDetailInfo.Rows.Count; ++i)
            {
                TextBox txtAmount = (TextBox)gvCodeDetailInfo.Rows[i].Cells[2].FindControl("txtAmount");
                Amount = Converter.GetDecimal(txtAmount.Text);

                sumCr = (sumCr + Amount);       
            }
            lblTotalAmt.Visible = true;
            lblTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            SumValue();
        }


        protected void ddlCodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCodeType.SelectedValue == "1")
            {
                lblcode.Text = "Allowance Code :";
                ddlAllowanceDetails();
            }
            else
            {
                lblcode.Text = "Deduction Code :";
                ddlDeductionDetails();
            }
        }

        protected void chkT1Select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox b = (CheckBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                CheckBox T1select = (CheckBox)gvCodeDetailInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT1Select");
                CheckBox R1select = (CheckBox)gvCodeDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR1Select");

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

                CheckBox T1select = (CheckBox)gvCodeDetailInfo.Rows[r.RowIndex].Cells[3].FindControl("chkT1Select");
                CheckBox R1select = (CheckBox)gvCodeDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("chkR1Select");

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

    }
}