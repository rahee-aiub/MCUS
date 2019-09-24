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
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmployeeFinalSettlement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                BtnAutoReverse.Visible = false;
                BtnAutoPost.Visible = true;


                txtEmpNo.Focus();
                gvBenefitInfo.Visible = false;
                gvDeductionInfo.Visible = false;

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");
                CtrlProcDate.Text = date1;


                //DivAllowance.Visible = false;
                //divDeduction.Visible = false;                

            }
        }

        protected void gvBenefitDetails()
        {
            string sqlquery3 = @"SELECT ID,AccountHead,Amount,GLCodeDr FROM WFSETTLEMENT WHERE Flag = 1";
            gvBenefitInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBenefitInfo, "A2ZHRMCUS");

        }
        protected void gvDeductionDetails()
        {
            string sqlquery3 = @"SELECT ID,AccountHead,Amount,GLCodeCr FROM WFSETTLEMENT WHERE Flag = 2"; ;
            gvDeductionInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDeductionInfo, "A2ZHRMCUS");
        }


        protected void gvBenefitInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Style.Add("height", "60px");
                }


                if (e.Row.RowIndex < 3)
                {
                    e.Row.Cells[2].Enabled = false;
                }

                //e.Row.Style.Add("top", "-100px");
            }
        }

        protected void gvDeductionInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Style.Add("height", "60px");
                }

                if (e.Row.RowIndex < 5)
                {
                    e.Row.Cells[2].Enabled = false;
                }

                if (e.Row.RowIndex == 6)
                {
                    e.Row.Cells[2].Enabled = false;
                }

                //e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void btnSumbit_Click(object sender, EventArgs e)
        {

            lblRevFlag.Text = "0";

            BtnAutoReverse.Visible = false;
            BtnAutoPost.Visible = true;

            int EmpID = Converter.GetInteger(txtEmpNo.Text);
            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));


            if (getDTO.EmployeeID > 0)
            {

                if (getDTO.Status == 1)
                {
                    txtEmpNo.Text = string.Empty;
                    txtEmpNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employee Already Active');", true);
                    return;
                }

                if (getDTO.Status == 9)
                {
                    VerifyReverseTransaction();

                    if (lblErrMsg.Text == "1")
                    {
                        txtEmpNo.Text = string.Empty;
                        txtEmpNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Settlement Already Done');", true);
                        return;
                    }

                    lblRevFlag.Text = "1";

                    BtnAutoReverse.Visible = true;
                    BtnAutoPost.Visible = false;

                }

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

                var prm = new object[2];

                prm[0] = EmpID;
                prm[1] = 0;

                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueBySp("Sp_HRGenerateEmployeeSettlement", prm, "A2ZHRMCUS"));

                gvBenefitInfo.DataBind();
                gvDeductionInfo.DataBind();

                lblTotalBenefit.Visible = true;
                txtTotalBenefit.Visible = true;
                lblTotalDeduction.Visible = true;
                txtTotalDeduction.Visible = true;
                lblNetPay.Visible = true;
                txtNetPay.Visible = true;

                gvBenefitInfo.Visible = true;
                gvDeductionInfo.Visible = true;

                btnUpdate_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employee Not in File');", true);
                return;
            }
        }

        protected void BtnPost_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);

            double netPay = Converter.GetDouble(txtNetPay.Text);

            if (netPay < 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Settelement Not Allowed');", true);
                return;
            }

            if (txtVchNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
                return;
            }

            var prm = new object[5];

            prm[0] = txtEmpNo.Text;
            prm[1] = txtVchNo.Text;
            prm[2] = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
            prm[3] = 0;
            prm[4] = lblCashCode.Text;

            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueBySp("Sp_HREmployeeSettlementPost", prm, "A2ZHRMCUS"));

            if (result == 0)
            {

                txtEmpNo.Text = string.Empty;
                lblName.Text = string.Empty;
                lblDesign.Text = string.Empty;
                lblGrade.Text = string.Empty;
                txtVchNo.Text = string.Empty;

                lblTotalBenefit.Visible = false;
                txtTotalBenefit.Visible = false;
                lblTotalDeduction.Visible = false;
                txtTotalDeduction.Visible = false;
                lblNetPay.Visible = false;
                txtNetPay.Visible = false;

                gvBenefitInfo.Visible = false;
                gvDeductionInfo.Visible = false;

                BtnAutoReverse.Visible = false;
                BtnAutoPost.Visible = true;

                txtEmpNo.Focus();


                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Posting Done');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Problem In Posting');", true);
            return;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBenefitRecord();

            gvBenefitDetails();

            UpdateDeductionRecord();

            gvDeductionDetails();

            double totBenefit = Converter.GetDouble(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery("SELECT SUM(Amount) FROM WFSETTLEMENT WHERE Flag = 1", "A2ZHRMCUS"));
            txtTotalBenefit.Text = string.Format("{0:n}", totBenefit);

            double totDeduction = Converter.GetDouble(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery("SELECT SUM(Amount) FROM WFSETTLEMENT WHERE Flag = 2", "A2ZHRMCUS"));
            txtTotalDeduction.Text = string.Format("{0:n}", totDeduction);

            double netPay = (totBenefit - totDeduction);

            txtNetPay.Text = string.Format("{0:n}", netPay);
        }


        protected void VerifyReverseTransaction()
        {
            lblErrMsg.Text = "0";

            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string qry = "SELECT Id,Status,StatusDate FROM A2ZEMPLOYEE  WHERE EmpCode = '" + txtEmpNo.Text + "' AND StatusDate = '" + opdate + "' AND Status = 9";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            if (dt.Rows.Count == 0)
            {
                lblErrMsg.Text = "1";
            }

        }

        protected void UpdateBenefitRecord()
        {
            double amount = 0;
            string sqlQuery = string.Empty;

            for (int i = 0; i < gvBenefitInfo.Rows.Count; i++)
            {
                Label id = (Label)gvBenefitInfo.Rows[i].Cells[0].FindControl("BlblID");
                TextBox txtAmt = (TextBox)gvBenefitInfo.Rows[i].Cells[0].FindControl("BtxtAmount");

                amount = Converter.GetDouble(txtAmt.Text);

                sqlQuery = "UPDATE WFSETTLEMENT SET Amount = " + amount + " WHERE ID = " + id.Text;

                DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS");
            }

        }

        protected void UpdateDeductionRecord()
        {
            double amount = 0;
            string sqlQuery = string.Empty;

            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {
                Label id = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("DlblID");
                TextBox txtAmt = (TextBox)gvDeductionInfo.Rows[i].Cells[0].FindControl("DtxtAmount");

                amount = Converter.GetDouble(txtAmt.Text);

                sqlQuery = "UPDATE WFSETTLEMENT SET Amount = " + amount + " WHERE ID = " + id.Text;

                DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS");
            }
        }

        protected void TrnVchDeplicate()
        {
            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
                return;
            }
        }
        protected void txtVchNo_TextChanged(object sender, EventArgs e)
        {
            if (lblRevFlag.Text == "0")
            {
                TrnVchDeplicate();
            }         

        }

        protected void BtnAutoReverse_Click(object sender, EventArgs e)
        {
            if (txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
                return;
            }

            var prm = new object[2];

            prm[0] = txtEmpNo.Text;
            prm[1] = txtVchNo.Text;
            
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueBySp("Sp_HREmployeeSettlementReverse", prm, "A2ZHRMCUS"));

            if (result == 0)
            {
                txtEmpNo.Text = string.Empty;
                lblName.Text = string.Empty;
                lblDesign.Text = string.Empty;
                lblGrade.Text = string.Empty;
                txtVchNo.Text = string.Empty;

                lblTotalBenefit.Visible = false;
                txtTotalBenefit.Visible = false;
                lblTotalDeduction.Visible = false;
                txtTotalDeduction.Visible = false;
                lblNetPay.Visible = false;
                txtNetPay.Visible = false;

                gvBenefitInfo.Visible = false;
                gvDeductionInfo.Visible = false;

                BtnAutoReverse.Visible = false;
                BtnAutoPost.Visible = true;

                txtEmpNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Reverse Done');", true);
                return;
            }


        }


    }
}