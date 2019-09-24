using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;
using System.Collections.Generic;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSStaffLoanApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblModule.Text = Request.QueryString["a%b"];

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtLoanAppDate.Text = date;

                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(6));
                lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                LoanPurposeDdl();
                Hideinfo();
                int lappno = Converter.GetInteger(lblLastAppNo.Text);
                int LoanAppNo = lappno + 1;
                lblApplicationNo.Text = Converter.GetString(LoanAppNo);

                MemberDropdown();

                AccTypedropdown();
                txtLoanAppDate.Focus();

            }

        }


        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo = 0 and CuType = 0 GROUP BY MemNo,MemName";
            ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
        }

        private void AccTypedropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass BETWEEN 5 AND 6 AND AccTypeMode !='1'";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }
        private void LoanPurposeDdl()
        {
            string sqlquery = "SELECT LPurposeCode,LPurposeDescription from A2ZLPURPOSE ";
            ddlLoanPurpose = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanPurpose, "A2ZCSMCUS");
        }




        protected void DisplayMessage()
        {

            string Msg = "";

            string a = "";
            string b = "";

            a = "Generated New Application No.";
            b = string.Format(lblNewAppNo.Text);

            Msg += a + b;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


        }

        private void clearinfo()
        {
            ddlAccType.SelectedValue = "-Select-";
            //txtLoanAppDate.Text = string.Empty;
            txtLoanInterestRate.Text = string.Empty;
            txtLoanAppAmount.Text = string.Empty;

            txtLoanInstallmentAmount.Text = string.Empty;
            txtLoanLastInstlAmount.Text = string.Empty;
            txtNoInstallment.Text = string.Empty;
            txtLoanMemNo.Text = string.Empty;

            ddlLoanMemNo.SelectedIndex = 0;

            ddlLoanPurpose.SelectedIndex = 0;
            ddlLoanCategory.SelectedIndex = 0;
            txtSuretyMemNo.Text = string.Empty;
            txtLoanExpDate.Text = string.Empty;

            if (txtLoanMemNo.Text != string.Empty)
            {
                txtLoanMemNo.Text = string.Empty;
                ddlLoanMemNo.SelectedValue = "-Select-";
            }


        }
        protected void Hideinfo()
        {
            txtNoInstallment.Visible = false;

            txtLoanInstallmentAmount.Visible = false;
            txtLoanLastInstlAmount.Visible = false;

            lblNoInstallment.Visible = false;

            lblLoanInstallmentAmount.Visible = false;
            lblLoanLastInstlAmount.Visible = false;

            lblLoanStatDate.Visible = false;
            txtLoanExpDate.Visible = false;
        }
        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtLoanMemNo.Text = string.Empty;
            ddlLoanMemNo.SelectedIndex = 0;


            if (ddlAccType.SelectedValue != "-Select-")
            {
                Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    lblTypeCls.Text = Converter.GetString(getDTO.AccTypeClass);
                    lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                    txtLoanMemNo.Focus();
                }

                if (lblTypeCls.Text == "5")
                {
                    txtNoInstallment.Visible = false;

                    txtLoanInstallmentAmount.Visible = false;
                    txtLoanLastInstlAmount.Visible = false;

                    lblNoInstallment.Visible = false;

                    lblLoanInstallmentAmount.Visible = false;
                    lblLoanLastInstlAmount.Visible = false;

                    lblLoanStatDate.Visible = false;
                    txtLoanExpDate.Visible = false;
                }
                else
                {

                    txtNoInstallment.Visible = true;

                    txtLoanInstallmentAmount.Visible = true;
                    txtLoanLastInstlAmount.Visible = true;

                    lblNoInstallment.Visible = true;

                    lblLoanInstallmentAmount.Visible = true;
                    lblLoanLastInstlAmount.Visible = true;

                    lblLoanStatDate.Visible = true;
                    txtLoanExpDate.Visible = true;
                }
            }
        }

        protected void txtLoanMemNo_TextChanged(object sender, EventArgs e)
        {
            if (ddlLoanMemNo.SelectedValue == "-Select-")
            {
                txtLoanMemNo.Focus();

            }

            if (txtLoanMemNo.Text != string.Empty)
            {

                int MemNumber = Converter.GetInteger(txtLoanMemNo.Text);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(0, 0, MemNumber));
                if (getDTO.NoRecord > 0)
                {
                    ddlLoanMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);

                    AccountOpenCheck();
                    if (MsgFlag.Text == "1")
                    {
                        txtLoanMemNo.Text = string.Empty;
                        ddlLoanMemNo.SelectedIndex = 0;
                        txtLoanMemNo.Focus();
                        return;
                    }
                    else
                    {
                        txtLoanAppAmount.Focus();
                    }
                }
                else
                {
                    
                    txtLoanMemNo.Focus();
      
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Staff No. does not exist in file');", true);
                    return;
                }
            }

        }

        protected void ddlLoanMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlLoanMemNo.SelectedItem.Text == "-Select-")
            {
                txtLoanMemNo.Text = string.Empty;
            }
            else 
            {
                txtLoanMemNo.Text = ddlLoanMemNo.SelectedValue;
                AccountOpenCheck();
                if (MsgFlag.Text == "1")
                {
                    txtLoanMemNo.Text = string.Empty;
                    ddlLoanMemNo.SelectedIndex = 0;
                    txtLoanMemNo.Focus();
                    return;
                }
                else
                {
                    txtLoanAppAmount.Focus();
                }
            
            }

        }


        protected void AccountOpenCheck()
        {
            MsgFlag.Text = string.Empty;

            string qry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType = 0 AND CuNo = 0 AND MemNo = '" + txtLoanMemNo.Text + "' AND AccType = '" + ddlAccType.SelectedValue + "' AND AccStatus < 98";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblAccountNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);


                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm1 = new object[3];
                prm1[0] = lblAccountNo.Text;
                prm1[1] = Converter.GetDateToYYYYMMDD(date1);
                prm1[2] = 0;

                DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_CSGenerateSingleAccountBalance", prm1, "A2ZCSMCUS");

                string qry1 = "SELECT AccOpBal FROM A2ZACCOUNT WHERE CuType = 0 AND CuNo = 0 AND MemNo = '" + txtLoanMemNo.Text + "' AND AccType = '" + ddlAccType.SelectedValue + "' AND AccNo = '" + lblAccountNo.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    lblAccBalance.Text = Converter.GetString(dt1.Rows[0]["AccOpBal"]);
                    decimal balance = Converter.GetDecimal(lblAccBalance.Text);
                    if (balance < 0)
                    {
                        MsgFlag.Text = "1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Already Exist');", true);
                        return;
                    }

                }
                
            }
        }
        protected void txtNoInstallment_TextChanged(object sender, EventArgs e)
        {
            Int16 RoundFlag = 0;
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            double b = Converter.GetDouble(txtNoInstallment.Text);
            double c = a / b;

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + ddlAccType.SelectedValue + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                if (RoundFlag == 1)
                {
                    c = Math.Round(c);
                }

                if (RoundFlag == 2)
                {
                    c = Math.Ceiling(c);
                }
                //if (RoundFlag == 3)
                //{
                //    c = c;
                //}
            }

            txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

            lblPrevInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

            double d = Math.Abs((c * (b - 1)) - a);

            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));

            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtNoInstallment.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtLoanExpDate.Text = date;


            txtLoanInterestRate.Focus();


        }

        //protected void txtLoanAppAmount_TextChanged(object sender, EventArgs e)
        //{
        //    A2ZLOANDTO getDTO = new A2ZLOANDTO();
        //    getDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
        //    txtLoanAppAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LoanApplicationAmount));
        //    txtNoInstallment.Focus();
        //}

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoanMemNo.SelectedValue != "-Select-" && ddlAccType.SelectedValue != "-Select-")
                {
                    A2ZSTAFFLOANDTO objDTO = new A2ZSTAFFLOANDTO();
                    A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(6));
                    objDTO.LoanApplicationNo = Converter.GetInteger(getDTO.CtrlRecLastNo);
                    lblNewAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);
                    if (txtLoanAppDate.Text != string.Empty)
                    {
                        DateTime apdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objDTO.LoanApplicationDate = apdate;
                    }
                    else
                    {
                        objDTO.LoanApplicationDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    }
                    objDTO.LoanAccountType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                    objDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
                    objDTO.LoanMemberNo = Converter.GetInteger(ddlLoanMemNo.SelectedValue);
                    objDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
                    objDTO.LoanInterestRate = Converter.GetDecimal(txtLoanInterestRate.Text);

                    objDTO.LoanInstallmentAmount = Converter.GetDecimal(txtLoanInstallmentAmount.Text);
                    objDTO.LoanLastInstallmentAmount = Converter.GetDecimal(txtLoanLastInstlAmount.Text);
                    objDTO.LoanNoInstallment = Converter.GetInteger(txtNoInstallment.Text);


                    objDTO.LoanPurpose = Converter.GetSmallInteger(ddlLoanPurpose.SelectedValue);
                    objDTO.LoanCategory = Converter.GetSmallInteger(ddlLoanCategory.SelectedValue);
                    objDTO.LoanSuretyMemberNo = Converter.GetInteger(txtSuretyMemNo.Text);
                    if (txtLoanExpDate.Text != string.Empty)
                    {
                        DateTime Expdate = DateTime.ParseExact(txtLoanExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objDTO.LoanExpDate = Expdate;
                    }
                    else
                    {
                        objDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    }


                    objDTO.LoanProcFlag = 11;


                    int roweffect = A2ZSTAFFLOANDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        // A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(4));
                        lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                        clearinfo();
                        DisplayMessage();

                    }

                }
                else
                {
                    //InvalidInputMSG();
                    ddlAccType.Focus();
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        protected void txtLoanInstallmentAmount_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            double b = Converter.GetDouble(txtNoInstallment.Text);
            double c = Converter.GetDouble(txtLoanInstallmentAmount.Text);

            double d = ((c * (b - 1)) - a);

            if (d > 0)
            {
                txtLoanInstallmentAmount.Text = string.Empty;
                txtLoanInstallmentAmount.Text = lblPrevInstlAmount.Text;
                txtLoanInstallmentAmount.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid First Installment Amount');", true);
                return;
            }
            else
            {
                d = Math.Abs(d);
            }


            txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));
            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }

        protected void txtLoanAppAmount_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            txtLoanAppAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", a));
            txtNoInstallment.Focus();

            if (txtNoInstallment.Text != string.Empty && lblTypeCls.Text == "6")
            {
                txtNoInstallment_TextChanged(this, EventArgs.Empty);
            }
        }



    }
}
