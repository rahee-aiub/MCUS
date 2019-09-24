using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
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
    public partial class CSEditStaffLoanApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblModule.Text = Request.QueryString["a%b"];             

                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(6));
                lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);
                txtLoanAppNo.Focus();
                BtnUpdate.Visible = false;

                ddlAccType.Enabled = false;
               
                
                
                
                LoanPurposeDdl();
                
                
                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                //TruncateWF();
                Hideinfo();

                
                 MemberDropdown();
                 
                 AccTypedropdown();      

            }
            else
            {

            }
        }    
        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo = 0 and CuType = 0 GROUP BY MemNo,MemName";
            ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
        }
        
        private void AccTypedropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass BETWEEN 5 AND 6 AND AccTypeMode !=1";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }
        private void LoanPurposeDdl()
        {
            string sqlquery = "SELECT LPurposeCode,LPurposeDescription from A2ZLPURPOSE ";
            ddlLoanPurpose = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanPurpose, "A2ZCSMCUS");
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
        private void clearinfo()
        {
            ddlAccType.SelectedValue = "-Select-";
            txtLoanAppDate.Text = string.Empty;
            txtLoanInterestRate.Text = string.Empty;
            txtLoanAppAmount.Text = string.Empty;
           
            txtLoanInstallmentAmount.Text = string.Empty;
            txtLoanLastInstlAmount.Text = string.Empty;
            txtNoInstallment.Text = string.Empty;
         

            ddlLoanPurpose.SelectedValue = "0";
            ddlLoanCategory.SelectedValue = "0";
            txtSuretyMemNo.Text = string.Empty;
            txtLoanExpDate.Text = string.Empty;
            
            
            if (txtLoanMemNo.Text != string.Empty)
            {
                txtLoanMemNo.Text = string.Empty;
                ddlLoanMemNo.SelectedValue = "-Select-";
            }

        }
        protected void txtLoanAppNo_TextChanged(object sender, EventArgs e)
        {

            if (txtLoanAppNo.Text != string.Empty)
            {   
                A2ZSTAFFLOANDTO getDTO = new A2ZSTAFFLOANDTO();

                Int16 AppNumber = Converter.GetSmallInteger(txtLoanAppNo.Text);
                getDTO = (A2ZSTAFFLOANDTO.GetInformation(AppNumber));

                if (getDTO.LoanApplicationNo > 0)
                {
                    if (getDTO.LoanStatus == 99)
                    {
                        AppCancelMSG();
                        txtLoanAppNo.Text = string.Empty;
                        txtLoanAppNo.Focus();
                        return;
                    }

                    if (getDTO.LoanProcFlag == 13)
                    {
                        AppApproveMSG();
                        txtLoanAppNo.Text = string.Empty;
                        txtLoanAppNo.Focus();
                        return;
                    }
                   
                    DateTime dt = Converter.GetDateTime(getDTO.LoanApplicationDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtLoanAppDate.Text = date;
                    ddlAccType.SelectedValue = Converter.GetString(getDTO.LoanAccountType);
                    
                    txtLoanMemNo.Text = Converter.GetString(getDTO.LoanMemberNo);
                    ddlLoanMemNo.SelectedValue = Converter.GetString(txtLoanMemNo.Text);
                    Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                    A2ZACCTYPEDTO gDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (gDTO.AccTypeCode > 0)
                    {
                        lblTypeCls.Text = Converter.GetString(gDTO.AccTypeClass);
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

                    txtLoanAppAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanApplicationAmount));
                    txtLoanInterestRate.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanInterestRate));
                  
                    txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanInstallmentAmount));
                    txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanLastInstallmentAmount));
                    txtNoInstallment.Text = Converter.GetString(getDTO.LoanNoInstallment);
                    
                   

                    ddlLoanPurpose.SelectedValue = Converter.GetString(getDTO.LoanPurpose);
                    ddlLoanCategory.SelectedValue = Converter.GetString(getDTO.LoanCategory);
                    txtSuretyMemNo.Text = Converter.GetString(getDTO.LoanSuretyMemberNo);
                    //txtLoanStatus.Text = Converter.GetString(getDTO.LoanStatus);
                    DateTime dt2 = Converter.GetDateTime(getDTO.LoanExpDate);
                    string date2 = dt2.ToString("dd/MM/yyyy");
                    txtLoanExpDate.Text = date2;
                    // txtLoanStatNote.Text = Converter.GetString(getDTO.LoanStatusNote);

                    BtnUpdate.Visible = true;
                    txtLoanAppDate.Focus();

                }
                else
                {
                    AppInvalidMSG();
                    clearinfo();
                    BtnUpdate.Visible = false;
                    txtLoanAppNo.Focus();
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

                }
                else
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Depositor No does not exist in file');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //    txtLoanMemNo.Focus();
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Depositor No does not exist in file');", true);
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
            if (ddlLoanMemNo.SelectedValue != "-Select-")
            {

                int MemNumber = Converter.GetInteger(ddlLoanMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(0, 0, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtLoanMemNo.Text = Converter.GetString(getDTO.MemberNo);
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                A2ZSTAFFLOANDTO UpDTO = new A2ZSTAFFLOANDTO();
                UpDTO.LoanApplicationNo = Converter.GetInteger(txtLoanAppNo.Text);
                UpDTO.LoanAccountType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                
                if (txtLoanAppDate.Text != string.Empty)
                {
                    DateTime apdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.LoanApplicationDate = apdate;
                }
                else
                {
                    UpDTO.LoanApplicationDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                }
                UpDTO.LoanMemberNo = Converter.GetInteger(ddlLoanMemNo.SelectedValue);
                UpDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
                UpDTO.LoanInterestRate = Converter.GetDecimal(txtLoanInterestRate.Text);
                
                
                UpDTO.LoanInstallmentAmount = Converter.GetDecimal(txtLoanInstallmentAmount.Text);
                UpDTO.LoanLastInstallmentAmount = Converter.GetDecimal(txtLoanLastInstlAmount.Text);
                UpDTO.LoanNoInstallment = Converter.GetInteger(txtNoInstallment.Text);

                if (txtLoanExpDate.Text != string.Empty)
                {
                    DateTime Expdate = DateTime.ParseExact(txtLoanExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.LoanExpDate = Expdate;
                }
                else
                {
                    UpDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                }
                UpDTO.LoanPurpose = Converter.GetSmallInteger(ddlLoanPurpose.SelectedValue);
                UpDTO.LoanCategory = Converter.GetSmallInteger(ddlLoanCategory.SelectedValue);

                
                int roweffect = A2ZSTAFFLOANDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {


                    txtLoanAppNo.Text = string.Empty;
                    clearinfo();
                    BtnUpdate.Visible = false;
                    txtLoanAppNo.Focus();
                    
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InvalidCUNo()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }
        protected void AppCancelMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Loan Application Already Canceled');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Application Already Canceled');", true);
            return;

        }

        protected void AppInvalidMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Loan Application No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Loan Application No.');", true);
            return;

        }

        protected void AppApproveMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Loan Application Already Approved');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Application Already Approved');", true);
            return;

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

            double d = Math.Abs((c * (b - 1)) - a);

            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
            
        }

        protected void txtLoanAppAmount_TextChanged(object sender, EventArgs e)
        {
            A2ZSTAFFLOANDTO getDTO = new A2ZSTAFFLOANDTO();
            getDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
            txtLoanAppAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LoanApplicationAmount));
            txtNoInstallment.Focus();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
  
        

        protected void txtLoanInstallmentAmount_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            double b = Converter.GetDouble(txtNoInstallment.Text);
            double c = Converter.GetDouble(txtLoanInstallmentAmount.Text);

            double d = Math.Abs((c * (b - 1)) - a);

            txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));
            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }      

    }
}