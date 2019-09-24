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
    public partial class CSEditLoanApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Cflag = (string)Session["flag"];
                CtrlFlag.Text = Cflag;

                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                if (CtrlFlag.Text == "1")
                {
                    string mod = (string)Session["Module"];
                    lblModule.Text = mod;

                    string memno = (string)Session["NewMemNo"];
                    txtLoanMemNo.Text = memno;
                    string memname = (string)Session["NewMemName"];

                }
                else
                {
                    lblModule.Text = Request.QueryString["a%b"];

                    CtrlFlag.Text = string.Empty;

                    BtnDeposit.Visible = false;
                    BtnShare.Visible = false;
                    BtnProperty.Visible = false;

                    Hideinfo();

                }


                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(4));
                lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);
                txtLoanAppNo.Focus();
                //BtnUpdate.Visible = false;

                ddlAccType.Enabled = false;

                txtCreditUNo.Enabled = false;
                ddlCreditUNo.Enabled = false;
                txtLoanMemNo.Enabled = false;
                ddlLoanMemNo.Enabled = false;


                ////BtnDeposit.Visible = false;
                ////BtnShare.Visible = false;
                ////BtnProperty.Visible = false;


                LoanPurposeDdl();

                pnlDeposit.Visible = false;
                pnlProperty.Visible = false;
                pnlShare.Visible = false;
                lblTotalAmt.Visible = false;
                lbltotalshare.Visible = false;
                lblTotalProprty.Visible = false;
                hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                if (lblModule.Text == "1" || lblModule.Text == "6")
                {
                    AccTypedropdown1();
                    CreditUnionDropdown();
                }
                else
                {
                    lblLoanMemNo.Text = "Staff Code";
                    lblCUNo.Visible = false;
                    txtCreditUNo.Visible = false;
                    ddlCreditUNo.Visible = false;
                    MemberDropdown();
                    BtnLoanApplication.Visible = false;
                    BtnDeposit.Visible = false;
                    BtnShare.Visible = false;
                    BtnProperty.Visible = false;
                    lblTotalGar.Visible = false;
                    lblTotalResult.Visible = false;
                    AccTypedropdown2();
                }

                if (CtrlFlag.Text == "1")
                {
                    string CuNo = (string)Session["RCreditUNo"];
                    txtCreditUNo.Text = CuNo;
                    string MemNo = (string)Session["RMemNo"];
                    txtLoanMemNo.Text = MemNo;
                    string dat = (string)Session["TrnDate"];
                    txtLoanAppDate.Text = dat;


                    string RtxtLoanAppNo = (string)Session["StxtLoanAppNo"];
                    txtLoanAppNo.Text = RtxtLoanAppNo;


                    string RtxtCreditUNo = (string)Session["StxtCreditUNo"];
                    txtCreditUNo.Text = RtxtCreditUNo;

                    string RlblCuType = (string)Session["SlblCuType"];
                    lblCuType.Text = RlblCuType;

                    string RlblCu = (string)Session["SlblCu"];
                    lblCu.Text = RlblCu;

                    string RtxtDepositMemNo = (string)Session["StxtDepositMemNo"];
                    txtDepositMemNo.Text = RtxtDepositMemNo;


                    string LType = (string)Session["LAcType"];
                    ddlAccType.SelectedValue = LType;

                    string RlblTypeCls = (string)Session["SlblTypeCls"];
                    lblTypeCls.Text = RlblTypeCls;

                    string RAccTypeGuaranty = (string)Session["SAccTypeGuaranty"];
                    lblAccTypeGuaranty.Text = RAccTypeGuaranty;

                    string NewAccNo = (string)Session["NewAccNo"];
                    txtAccNo.Text = NewAccNo;

                    string RddlCreditUNo = (string)Session["SddlCreditUNo"];
                    ddlCreditUNo.SelectedValue = RddlCreditUNo;

                    string RddlLoanMemNo = (string)Session["SddlLoanMemNo"];
                    ddlLoanMemNo.SelectedValue = RddlLoanMemNo;


                    string RtxtLoanAppAmount = (string)Session["StxtLoanAppAmount"];
                    txtLoanAppAmount.Text = RtxtLoanAppAmount;

                    string RtxtNoInstallment = (string)Session["StxtNoInstallment"];
                    txtNoInstallment.Text = RtxtNoInstallment;

                    string RtxtLoanInterestRate = (string)Session["StxtLoanInterestRate"];
                    txtLoanInterestRate.Text = RtxtLoanInterestRate;

                    string RtxtLoanInstallmentAmount = (string)Session["StxtLoanInstallmentAmount"];
                    txtLoanInstallmentAmount.Text = RtxtLoanInstallmentAmount;

                    string RtxtLoanLastInstlAmount = (string)Session["StxtLoanLastInstlAmount"];
                    txtLoanLastInstlAmount.Text = RtxtLoanLastInstlAmount;

                    string RtxtLoanExpDate = (string)Session["StxtLoanExpDate"];
                    txtLoanExpDate.Text = RtxtLoanExpDate;

                    string RddlLoanCategory = (string)Session["SddlLoanCategory"];
                    ddlLoanCategory.SelectedValue = RddlLoanCategory;

                    string RddlLoanPurpose = (string)Session["SddlLoanPurpose"];
                    ddlLoanPurpose.SelectedValue = RddlLoanPurpose;


                    string RlblTotalResult = (string)Session["SlblTotalResult"];
                    lblTotalResult.Text = RlblTotalResult;

                    string RtxtTotalAmt = (string)Session["StxtTotalAmt"];
                    txtTotalAmt.Text = RtxtTotalAmt;

                    if (txtTotalAmt.Text != string.Empty)
                    {
                        gvDepositInfo();
                    }

                    if (lblTypeCls.Text == "5")
                    {
                        BtnShare.Visible = false;
                        BtnDeposit.Visible = true;
                    }
                    else 
                    {
                        BtnShare.Visible = true;
                        BtnDeposit.Visible = false;
                    }

                    string RlblShareTotalAmt = (string)Session["SlblShareTotalAmt"];
                    lblShareTotalAmt.Text = RlblShareTotalAmt;

                    if (lblShareTotalAmt.Text != string.Empty)
                    {
                        gvShareInfo();
                    }

                    string RlblSumProperty = (string)Session["SlblSumProperty"];
                    lblSumProperty.Text = RlblSumProperty;

                    if (lblSumProperty.Text != string.Empty)
                    {
                        gvPropertyInfo();
                    }


                    string RpnlDeposit = (string)Session["SpnlDeposit"];
                    if (RpnlDeposit == "1")
                    {
                        pnlDeposit.Visible = true;
                    }
                    else
                    {
                        pnlDeposit.Visible = false;
                    }

                    string RpnlShare = (string)Session["SpnlShare"];
                    if (RpnlShare == "1")
                    {
                        pnlShare.Visible = true;
                    }
                    else
                    {
                        pnlShare.Visible = false;
                    }

                    if (pnlDeposit.Visible == true)
                    {
                        pnlDeposit.Visible = true;
                        pnlProperty.Visible = false;
                        pnlShare.Visible = false;
                        pnlLoanApplication.Visible = false;

                        GetAccInfo();

                        gvDepositInfo();
                        SumDepositValue();
                        TotalGuarantor();

                    }
                    else
                        if (pnlShare.Visible == true)
                        {
                            pnlDeposit.Visible = false;
                            pnlProperty.Visible = false;
                            pnlShare.Visible = true;
                            pnlLoanApplication.Visible = false;
                            txtShareCuNo.Text = CuNo;
                            txtShareCuNo_TextChanged(this, EventArgs.Empty);


                            gvShareInfo();
                            SumShareValue();
                            TotalGuarantor();

                        }

                    if (lblTypeCls.Text == "5")
                    {
                        Hidetrue();
                    }


                }


                SessionRemove();
                //TruncateWF();




            }

        }

        protected void SessionRemove()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            //Session["date"] = string.Empty;
            //Session["Module"] = string.Empty;
            Session["MemName"] = string.Empty;
            Session["LAcType"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["ProgCtrl"] = string.Empty;

            Session["SlblTypeCls"] = string.Empty;

            Session["SSPflag"] = string.Empty;
            Session["SAccTypeGuaranty"] = string.Empty;

            Session["SpnlDeposit"] = string.Empty;
            Session["SpnlShare"] = string.Empty;

            Session["SlblTotalResult"] = string.Empty;
            Session["StxtTotalAmt"] = string.Empty;
            Session["SlblShareTotalAmt"] = string.Empty;
            Session["SlblSumProperty"] = string.Empty;

            Session["SModule"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;

        }
        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }

        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo = 0 and CuType = 0 GROUP BY MemNo,MemName";
            ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
        }
        private void AccTypedropdown1()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where (AccTypeClass BETWEEN 5 AND 6) AND AccTypeMode !=2";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        private void AccTypedropdown2()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass BETWEEN 5 AND 6 AND AccTypeMode !=1";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }
        private void LoanPurposeDdl()
        {
            string sqlquery = "SELECT LPurposeCode,LPurposeDescription from A2ZLPURPOSE ";
            ddlLoanPurpose = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanPurpose, "A2ZCSMCUS");
        }

        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZACGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZCSMCUS"));

            string ShareQry = "DELETE  dbo.WFA2ZSHGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ShareQry, "A2ZCSMCUS"));

            string PropertyQry = "DELETE dbo.WFA2ZPRGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(PropertyQry, "A2ZCSMCUS"));

        }
        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCreditUNo.SelectedValue == "-Select-")
            {

                txtCreditUNo.Text = string.Empty;
                txtLoanMemNo.Focus();
                ddlLoanMemNo.SelectedIndex = 0;
                return;
            }


            try
            {

                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCu.Text = Converter.GetString(CNo);
                    lblCuType.Text = Converter.GetString(CuType);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";

                    ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {

                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = (c + "-" + d);


                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    //int CN = Converter.GetInteger(txtCreditUNo.Text);

                    //hdnCuNumber.Value = Converter.GetString(CN);


                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);

                    lblCu.Text = Converter.GetString(CNo);
                    lblCuType.Text = Converter.GetString(CuType);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    //if (getDTO.CreditUnionNo > 0)

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCu.Text = Converter.GetString(getDTO.CreditUnionNo);

                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + lblCuType.Text + "'";
                        ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCu.Text);
                        txtCreditUNo.Text = (lblCuType.Text + "-" + lblCu.Text);
                        txtLoanMemNo.Focus();
                        string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCu.Text + "'and CuType='" + lblCuType.Text + "' GROUP BY MemNo,MemName";
                        ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");

                    }
                    else
                    {
                        InvalidCUNo();
                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
                        //BtnUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void Hidetrue()
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
        private void clearinfo()
        {
            ddlAccType.SelectedValue = "-Select-";
            txtLoanAppDate.Text = string.Empty;
            txtLoanInterestRate.Text = string.Empty;
            txtLoanAppAmount.Text = string.Empty;

            txtLoanInstallmentAmount.Text = string.Empty;
            txtLoanLastInstlAmount.Text = string.Empty;
            txtNoInstallment.Text = string.Empty;

            txtTotalLienAmt.Text = string.Empty;


            ddlLoanPurpose.SelectedIndex = 0;
            ddlLoanCategory.SelectedIndex = 0;
            txtSuretyMemNo.Text = string.Empty;
            txtLoanExpDate.Text = string.Empty;

            if (txtCreditUNo.Text != string.Empty)
            {
                txtCreditUNo.Text = string.Empty;
                ddlCreditUNo.SelectedIndex = 0;
            }

            if (txtLoanMemNo.Text != string.Empty)
            {
                txtLoanMemNo.Text = string.Empty;
                ddlLoanMemNo.SelectedIndex = 0;
            }


        }
        protected void txtLoanAppNo_TextChanged(object sender, EventArgs e)
        {
            txtTotalAmt.Text = "0";
            lblShareTotalAmt.Text = "0";
            lblSumProperty.Text = "0";


            if (txtLoanAppNo.Text != string.Empty)
            {
                A2ZLOANDTO getDTO = new A2ZLOANDTO();

                Int16 AppNumber = Converter.GetSmallInteger(txtLoanAppNo.Text);
                getDTO = (A2ZLOANDTO.GetInformation(AppNumber));

                if (getDTO.LoanApplicationNo > 0)
                {
                    FromCashCode.Text = Converter.GetString(getDTO.FromCashCode);
                    
                    if (getDTO.LoanStatus == 99)
                    {
                        txtLoanAppNo.Text = string.Empty;
                        txtLoanAppNo.Focus();
                        AppCancelMSG();
                        return;
                    }

                    if (getDTO.LoanProcFlag == 13)
                    {
                        txtLoanAppNo.Text = string.Empty;
                        txtLoanAppNo.Focus();
                        AppApproveMSG();
                        return;
                    }

                    if (lblModule.Text == "6" && FromCashCode.Text != hdnCashCode.Text)
                    {
                        txtLoanAppNo.Text = string.Empty;
                        txtLoanAppNo.Focus();
                        AppInvalidMSG();
                        return;
                    }

                    DateTime dt = Converter.GetDateTime(getDTO.LoanApplicationDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtLoanAppDate.Text = date;
                    ddlAccType.SelectedValue = Converter.GetString(getDTO.LoanAccountType);

                    txtCreditUNo.Text = Converter.GetString(getDTO.CuNo);
                    ddlCreditUNo.SelectedValue = Converter.GetString(txtCreditUNo.Text);
                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);

                    txtCreditUNo.Text = (c + "-" + d);

                    lblCu.Text = Converter.GetString(CNo);
                    lblCuType.Text = Converter.GetString(CuType);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCu.Text + "'and CuType='" + lblCuType.Text + "' GROUP BY MemNo,MemName";
                    ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
                    txtLoanMemNo.Text = Converter.GetString(getDTO.LoanMemberNo);
                    ddlLoanMemNo.SelectedValue = Converter.GetString(txtLoanMemNo.Text);
                    Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                    A2ZACCTYPEDTO gDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (gDTO.AccTypeCode > 0)
                    {
                        lblTypeCls.Text = Converter.GetString(gDTO.AccTypeClass);
                        lblAccTypeGuaranty.Text = Converter.GetString(gDTO.AccTypeGuaranty);
                    }
                    if (lblTypeCls.Text == "5")
                    {
                        BtnShare.Visible = false;
                        BtnDeposit.Visible = true;

                        txtNoInstallment.Visible = false;

                        txtLoanInstallmentAmount.Visible = false;
                        txtLoanLastInstlAmount.Visible = false;

                        lblNoInstallment.Visible = false;

                        lblLoanInstallmentAmount.Visible = false;
                        lblLoanLastInstlAmount.Visible = false;

                        lblLoanStatDate.Visible = false;
                        txtLoanExpDate.Visible = false;

                        lblODPeriod.Visible = true;
                        txtODPeriod.Visible = true;
                        lblODExpiryDate.Visible = true;
                        txtODExpDate.Visible = true;

                    }
                    else
                    {
                        BtnShare.Visible = true;
                        BtnDeposit.Visible = false;

                        txtNoInstallment.Visible = true;

                        txtLoanInstallmentAmount.Visible = true;
                        txtLoanLastInstlAmount.Visible = true;

                        lblNoInstallment.Visible = true;

                        lblLoanInstallmentAmount.Visible = true;
                        lblLoanLastInstlAmount.Visible = true;

                        lblLoanStatDate.Visible = true;
                        txtLoanExpDate.Visible = true;

                        lblODPeriod.Visible = false;
                        txtODPeriod.Visible = false;
                        lblODExpiryDate.Visible = false;
                        txtODExpDate.Visible = false;

                    }

                    txtLoanAppAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanApplicationAmount));
                    txtLoanInterestRate.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanInterestRate));

                    txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanInstallmentAmount));
                    txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", getDTO.LoanLastInstallmentAmount));
                    txtNoInstallment.Text = Converter.GetString(getDTO.LoanNoInstallment);

                    txtODPeriod.Text = Converter.GetString(getDTO.AccPeriod);

                    ddlLoanPurpose.SelectedValue = Converter.GetString(getDTO.LoanPurpose);
                    ddlLoanCategory.SelectedValue = Converter.GetString(getDTO.LoanCategory);
                    txtSuretyMemNo.Text = Converter.GetString(getDTO.LoanSuretyMemberNo);
                    //txtLoanStatus.Text = Converter.GetString(getDTO.LoanStatus);

                    DateTime dt2 = Converter.GetDateTime(getDTO.LoanExpDate);
                    string date2 = dt2.ToString("dd/MM/yyyy");

                     if (lblTypeCls.Text == "5")
                     {
                         txtODExpDate.Text = date2;
                     }
                     else 
                     {
                         txtLoanExpDate.Text = date2;
                     }

                    lblInput.Text = Converter.GetString(getDTO.InputBy);
                    lblApprove.Text = Converter.GetString(getDTO.ApprovBy);


                    DateTime dt3 = Converter.GetDateTime(getDTO.InputByDate);
                    string date3 = dt3.ToString("dd/MM/yyyy");
                    lblInputDate.Text = date3;

                    DateTime dt4 = Converter.GetDateTime(getDTO.ApprovByDate);
                    string date4 = dt4.ToString("dd/MM/yyyy");
                    lblApproveDate.Text = date4;


                    TruncateWF();

                    gvDetailInfo.Visible = true;
                    gvShareDetails.Visible = true;
                    gvPropertyDetails.Visible = true;
                    lblTotalAmt.Visible = true;
                    txtTotalAmt.Visible = true;
                    lbltotalshare.Visible = true;
                    lblShareTotalAmt.Visible = true;
                    lblTotalProprty.Visible = true;
                    lblSumProperty.Visible = true;

                    DepositDataToWF();
                    gvDepositInfo();
                    SumDepositValue();

                    ShareDataToWF();
                    gvShareInfo();
                    SumShareValue();

                    PropertyDataToWF();
                    gvPropertyInfo();
                    SumPropertyValue();

                    TotalGuarantor();
                    BtnUpdate.Visible = true;

                    //BtnDeposit.Visible = true;
                    //BtnShare.Visible = true;
                    BtnProperty.Visible = true;
                    txtLoanAppDate.Focus(); ;
                }
                else
                {
                    AppInvalidMSG();
                    clearinfo();
                    //BtnUpdate.Visible = false;
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
                //string c = "";
                //int a = txtCreditUNo.Text.Length;

                //string b = txtCreditUNo.Text;
                //c = b.Substring(0, 1);
                //int re = Converter.GetSmallInteger(c);
                //int dd = a - 1;
                //string d = b.Substring(1, dd);
                //int re1 = Converter.GetSmallInteger(d);


                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetSmallInteger(lblCu.Text);



                int MemNumber = Converter.GetInteger(txtLoanMemNo.Text);

                int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtLoanMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    ddlLoanMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);

                }
                else
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Depositor No. does not exist in file');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //    txtLoanMemNo.Focus();
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Depositor No. does not exist in file');", true);
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
                //string c = "";
                //int a = txtCreditUNo.Text.Length;

                //string b = txtCreditUNo.Text;
                //c = b.Substring(0, 1);
                //int re = Converter.GetSmallInteger(c);
                //int dd = a - 1;
                //string d = b.Substring(1, dd);
                //int re1 = Converter.GetSmallInteger(d);


                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetSmallInteger(lblCu.Text);



                int MemNumber = Converter.GetInteger(ddlLoanMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtLoanMemNo.Text = Converter.GetString(getDTO.MemberNo);
                }
            }
        }

        protected void GuarantorMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Guarantor Information');", true);
            return;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                if (lblTypeCls.Text == "5" && (lblTotalResult.Text == string.Empty || lblTotalResult.Text == "00.00"))
                {
                    GuarantorMSG();
                    return;
                }


                A2ZLOANDTO UpDTO = new A2ZLOANDTO();
                UpDTO.LoanApplicationNo = Converter.GetInteger(txtLoanAppNo.Text);
                UpDTO.LoanAccountType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                UpDTO.CuType = Converter.GetInteger(lblCuType.Text);
                UpDTO.CuNo = Converter.GetInteger(lblCu.Text);
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
                UpDTO.AccPeriod = Converter.GetInteger(txtODPeriod.Text);

                UpDTO.LoanTotGuarantorAmt = Converter.GetDecimal(lblTotalResult.Text);


                if (lblTypeCls.Text == "5")
                {
                    if (txtODExpDate.Text != string.Empty)
                    {
                        DateTime Expdate = DateTime.ParseExact(txtODExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        UpDTO.LoanExpDate = Expdate;
                    }
                    else
                    {
                        UpDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    }
                }
                else 
                {
                    if (txtLoanExpDate.Text != string.Empty)
                    {
                        DateTime Expdate = DateTime.ParseExact(txtLoanExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        UpDTO.LoanExpDate = Expdate;
                    }
                    else
                    {
                        UpDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    }
                }

               
                UpDTO.LoanPurpose = Converter.GetSmallInteger(ddlLoanPurpose.SelectedValue);
                UpDTO.LoanCategory = Converter.GetSmallInteger(ddlLoanCategory.SelectedValue);

                UpDTO.InputBy = Converter.GetSmallInteger(lblInput.Text);
                UpDTO.ApprovBy = Converter.GetSmallInteger(lblApprove.Text);


                if (lblInputDate.Text != string.Empty)
                {
                    DateTime InputDate = DateTime.ParseExact(lblInputDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.InputByDate = InputDate;
                }
                else
                {
                    UpDTO.InputByDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                }

                if (lblApproveDate.Text != string.Empty)
                {
                    DateTime ApproveDate = DateTime.ParseExact(lblApproveDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.ApprovByDate = ApproveDate;
                }
                else
                {
                    UpDTO.ApprovByDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                }


                Delete();

                SubmitDepositData();
                SubmitShareData();
                SubmitPropertyData();


                int roweffect = A2ZLOANDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {


                    txtLoanAppNo.Text = string.Empty;
                    clearinfo();
                    //BtnUpdate.Visible = false;
                    txtLoanAppNo.Focus();
                    lblTotalResult.Text = "0";

                    txtTotalAmt.Text = "0";
                    lblShareTotalAmt.Text = "0";
                    lblSumProperty.Text = "0";

                    Delete();

                    BtnDeposit.Visible = false;
                    BtnShare.Visible = false;
                    BtnProperty.Visible = false;

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

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Application Already Rejected');", true);
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

            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtNoInstallment.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtLoanExpDate.Text = date;

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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnLoanApplication_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = false;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = true;
        }

        protected void BtnDeposit_Click(object sender, EventArgs e)
        {

            txtCrUNo.Focus();
            pnlDeposit.Visible = true;
            pnlProperty.Visible = false;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = false;

            Session["SpnlShare"] = string.Empty;

            if (gvDetailInfo.DataSource == null)
            {

            }

        }

        protected void BtnShare_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = false;
            pnlShare.Visible = true;
            pnlLoanApplication.Visible = false;

            Session["SpnlDeposit"] = string.Empty;

            txtShareCuNo.Focus();
        }

        protected void BtnProperty_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = true;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = false;
            txtSerialNo.Focus();
        }

        #region Deposit Gaurantor
        //--------------Deposit Gaurantor---------------------------------
        protected void txtCrUNo_TextChanged(object sender, EventArgs e)
        {
            string c = "";
            int a = txtCrUNo.Text.Length;

            string b = txtCrUNo.Text;
            c = b.Substring(0, 1);
            int re = Converter.GetSmallInteger(c);
            int dd = a - 1;
            string d = b.Substring(1, dd);
            int re1 = Converter.GetSmallInteger(d);

            Int16 CuType = Converter.GetSmallInteger(re);
            int CNo = Converter.GetSmallInteger(re1);
            lblDepositCuType.Text = Converter.GetString(CuType);
            lblDepositCuNo.Text = Converter.GetString(CNo);

            A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
            if (objDTO.CreditUnionNo > 0)
            {
                txtCrUNo.Text = Converter.GetString(CuType + "-" + CNo);
                lblCrName.Text = Converter.GetString(objDTO.CreditUnionName);
                txtDepositMemNo.Focus();
            }



        }

        protected void DepositMemNo_TextChanged(object sender, EventArgs e)
        {
            Int16 CuType = Converter.GetSmallInteger(lblDepositCuType.Text);
            int CuNo = Converter.GetInteger(lblDepositCuNo.Text);
            int MemNo = Converter.GetInteger(txtDepositMemNo.Text);
            A2ZMEMBERDTO ObjDTO = (A2ZMEMBERDTO.GetInformation(CuType, CuNo, MemNo));
            if (ObjDTO.NoRecord > 0)
            {
                txtDepositMemNo.Text = Converter.GetString(ObjDTO.MemberNo);
                lblMemberName.Text = Converter.GetString(ObjDTO.MemberName);
                txtAccType.Focus();
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
            A2ZACCTYPEDTO ObjDTO = (A2ZACCTYPEDTO.GetInformation(AccType));
            if (ObjDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(ObjDTO.AccTypeCode);
                lblAccountTypeName.Text = Converter.GetString(ObjDTO.AccTypeDescription);
                lblIAccTypeGuaranty.Text = Converter.GetString(ObjDTO.AccTypeGuaranty);

                if(lblIAccTypeGuaranty.Text != lblAccTypeGuaranty.Text)
                {
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                    return;
                }
                else 
                {
                    txtAccNo.Focus();
                }              
            }
            else 
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            Int16 CuType = Converter.GetSmallInteger(lblDepositCuType.Text);
            int CuNo = Converter.GetInteger(lblDepositCuNo.Text);
            int MemNo = Converter.GetInteger(txtDepositMemNo.Text);
            Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
            Int64 AccNo = Converter.GetLong(txtAccNo.Text);
            A2ZACCOUNTDTO ObjDTO = (A2ZACCOUNTDTO.GetInformation(AccType, AccNo, CuType, CuNo, MemNo));
            if (ObjDTO.a > 0)
            {
                txtAccNo.Text = Converter.GetString(ObjDTO.AccNo);
                txtLedgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccBalance));
                txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccLienAmt));
                lblStatus.Text = Converter.GetString(ObjDTO.AccStatus);
                if (lblStatus.Text == "99")
                {
                    InvalidAccMSG();
                    txtAccNo.Focus();
                }
                else
                {
                    txtLionAmt.Focus();
                }
            }


        }
        protected void ClearDeposit()
        {
            txtCrUNo.Text = string.Empty;
            txtDepositMemNo.Text = string.Empty;
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtLionAmt.Text = string.Empty;
            txtLedgerBalance.Text = string.Empty;
            lblCrName.Text = string.Empty;
            lblMemberName.Text = string.Empty;
            lblAccountTypeName.Text = string.Empty;
            txtTotalLienAmt.Text = string.Empty;
        }

        private void gvDepositInfo()
        {

            string sqlquery3 = "SELECT lTrim(str(CuType)+'-' +lTrim(str(CuNo))) As CuNo, MemNo, AccType, AccNo, AccAmount FROM WFA2ZACGUAR Where LoanApplicationNo='" + txtLoanAppNo.Text + "' AND RowMode =0";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");


        }

        protected void SumDepositValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {


                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[4].Text));


            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }

        protected void DepositDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZACGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode ) SELECT LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,0,0 FROM A2ZACGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


        }

        protected void BtnAddDeposit_Click(object sender, EventArgs e)
        {
            if (txtCrUNo.Text == string.Empty || txtDepositMemNo.Text == string.Empty || txtAccType.Text == string.Empty || txtAccNo.Text == string.Empty || txtLionAmt.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtCrUNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);
                return;
            }
            try
            {
                string qry4 = "SELECT LoanApplicationNo, AccAmount FROM WFA2ZACGUAR WHERE  LoanApplicationNo='" + txtLoanAppNo.Text + "' AND CuType='" + lblDepositCuType.Text + "' AND CuNo='" + lblDepositCuNo.Text + "' AND MemNo='" + txtDepositMemNo.Text + "' AND AccType='" + txtAccType.Text + "' AND AccNo='" + txtAccNo.Text + "'";
                DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                if (dt4.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dt4.Rows)
                    {

                        var LApp = dr2["LoanApplicationNo"].ToString();
                        var AAmount = dr2["AccAmount"].ToString();
                        decimal LAmount = Converter.GetDecimal(AAmount);
                        decimal IAmount = Converter.GetDecimal(txtLionAmt.Text);
                        LAmount = (LAmount + IAmount);
                        string strQuery = "UPDATE WFA2ZACGUAR SET  AccAmount = '" + LAmount + "' WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "' AND CuType='" + lblDepositCuType.Text + "' AND CuNo='" + lblDepositCuNo.Text + "' AND MemNo='" + txtDepositMemNo.Text + "' AND AccType='" + txtAccType.Text + "' AND AccNo='" + txtAccNo.Text + "'";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                }
                else
                {
                    string statment = "INSERT INTO  WFA2ZACGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode) VALUES('" + txtLoanAppNo.Text + "','" + lblDepositCuType.Text + "','" + lblDepositCuNo.Text + "','" + txtDepositMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtLionAmt.Text + "',1,0)";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                }

                gvDepositInfo();
                SumDepositValue();
                TotalGuarantor();
                ClearDeposit();
                txtCrUNo.Focus();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void txtLionAmt_TextChanged(object sender, EventArgs e)
        {
            double amt = Math.Abs(Converter.GetDouble(txtLionAmt.Text));
            txtLionAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
            double Ledgeramt = Converter.GetDouble(txtLedgerBalance.Text);
            double TotalLienAmt = Converter.GetDouble(txtTotalLienAmt.Text);
            double result = (amt + TotalLienAmt);
            if (result > Ledgeramt)
            {
                CheckLedgerBalValidation();
                txtLionAmt.Text = string.Empty;
                txtLionAmt.Focus();
            }
            else
            {
                BtnAddDeposit.Focus();
            }
        }
        private void SubmitDepositData()
        {



            string statment = "INSERT INTO A2ZACGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) SELECT LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount FROM WFA2ZACGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'AND RowMode =0";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            if (rowEffect > 0)
            {
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode From WFA2ZACGUAR Where LoanApplicationNo='" + txtLoanAppNo.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);
                int rowtype = Converter.GetInteger(dt1.Rows[i]["RowType"]);
                int rowmode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccLienAmt From A2ZACCOUNT WHERE AccType='" + AccType + "' and AccNo='" + AccNo + "' and CuType='" + Cutype + "' and CuNo='" + Cuno + "' and MemNo='" + Memno + "'", "A2ZCSMCUS");
                txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccLienAmt"]));
                double TotalLienAmt = Converter.GetDouble(txtTotalLienAmt.Text);
                double result = (LienAmt + TotalLienAmt);

                if (rowtype == 1 && rowmode == 0)
                {
                    string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = '50', AccLienAmt= '" + result + "' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                    int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                }
                if (rowtype == 0 && rowmode == 1)
                {
                    double subLienAmt = (TotalLienAmt - LienAmt);
                    int Astatus = Converter.GetInteger(50);
                    if (subLienAmt == 0)
                    {
                        Astatus = Converter.GetInteger(1);
                    }
                    string strQuery1 = "UPDATE A2ZACCOUNT SET  AccStatus = '" + Astatus + "', AccLienAmt= '" + subLienAmt + "' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                }
            }

            gvDetailInfo.Visible = false;
            lblTotalAmt.Visible = false;
            txtTotalAmt.Visible = false;


        }

        private void InvalidAccMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('((Account is not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account is not Active');", true);
            return;
        }
        private void CheckLedgerBalValidation()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('((Lien Amount+Total Lien Amount) Should be Less than of Ledger Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('((Lien Amount+Total Lien Amount) Should be Less than of Ledger Balance');", true);
            return;
        }

        //---------------------End Of Deposit Garantor----------------------------
        #endregion

        #region Share Gaurantor
        //----------------Share Gaurantor------------------------------------------
        protected void txtShareCuNo_TextChanged(object sender, EventArgs e)
        {
            if (txtShareCuNo.Text != string.Empty)
            {
                string c = "";
                int a = txtShareCuNo.Text.Length;

                string b = txtShareCuNo.Text;
                c = b.Substring(0, 1);
                int re = Converter.GetSmallInteger(c);
                int dd = a - 1;
                string d = b.Substring(1, dd);
                int re1 = Converter.GetSmallInteger(d);

                Int16 CuType = Converter.GetSmallInteger(re);
                int CNo = Converter.GetSmallInteger(re1);
                lblShareCType.Text = Converter.GetString(CuType);
                lblShareCNo.Text = Converter.GetString(CNo);

                A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                if (objDTO.CreditUnionNo > 0)
                {
                    txtShareCuNo.Text = Converter.GetString(CuType + "-" + CNo);
                    txtShareCuName.Text = Converter.GetString(objDTO.CreditUnionName);
                    //Int16 AccType = Converter.GetSmallInteger(11);
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccNo,AccBalance,AccStatus From A2ZACCOUNT WHERE AccType=11 and CuType='" + lblShareCType.Text + "' and CuNo='" + lblShareCNo.Text + "'", "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        lblSharAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                        lblStatus.Text = Converter.GetString(dt.Rows[0]["AccStatus"]);
                        txtShareAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccBalance"]));
                        if (lblStatus.Text != "1")
                        {
                            InvalidAccMSG();
                            txtShareCuNo.Focus();
                        }
                        else
                        {
                            BtnAddShare.Focus();
                        }
                    }

                }
            }

        }
        private void gvShareInfo()
        {

            string sqlquery3 = "SELECT Id,lTrim(str(CuType)+'-' +lTrim(str(CuNo))) As CuNo,MemNo,AccType,AccNo,AccAmount FROM WFA2ZSHGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "' AND RowMode !=1";
            gvShareDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvShareDetails, "A2ZCSMCUS");


        }
        protected void SumShareValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvShareDetails.Rows.Count; ++i)
            {

                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvShareDetails.Rows[i].Cells[5].Text));


            }
            lbltotalshare.Visible = true;
            lblShareTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }
        protected void ClearShare()
        {
            txtShareAmount.Text = string.Empty;
            txtShareAccType.Text = string.Empty;
            txtShareCuNo.Text = string.Empty;
            txtShareCuName.Text = string.Empty;
        }


        protected void ShareDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZSHGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode) SELECT LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,0,0 FROM A2ZSHGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


        }

        protected void BtnAddShare_Click(object sender, EventArgs e)
        {
            if (txtShareCuNo.Text == string.Empty || txtShareAmount.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtShareCuNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);
                return;
            }

            try
            {

                string statment = "INSERT INTO  WFA2ZSHGUAR (LoanApplicationNo ,CuType, CuNo, AccType,AccNo,AccAmount,RowType,RowMode) VALUES('" + txtLoanAppNo.Text + "','" + lblShareCType.Text + "','" + lblShareCNo.Text + "','" + 11 + "','" + lblSharAccNo.Text + "','" + txtShareAmount.Text + "',1,0)";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                gvShareInfo();
                ClearShare();
                txtShareCuNo.Focus();
                SumShareValue();
                TotalGuarantor();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SubmitShareData()
        {

            string statment = "INSERT INTO A2ZSHGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) SELECT LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount FROM WFA2ZSHGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'AND RowMode !=1";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            if (rowEffect > 0)
            {

                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, AccType,AccNo,MemNo,AccAmount,RowType,RowMode From WFA2ZSHGUAR Where LoanApplicationNo='" + txtLoanAppNo.Text + "'", "A2ZCSMCUS");


                for (int i = 0; i < dt1.Rows.Count; ++i)
                {
                    int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                    int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                    int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                    Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                    int MemNo = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                    int rowtype = Converter.GetInteger(dt1.Rows[i]["RowType"]);
                    int rowmode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                    if (rowtype == 1 && rowmode == 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = '50' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + MemNo + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                        int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                    if (rowtype == 0 && rowmode == 1)
                    {

                        string strQuery1 = "UPDATE A2ZACCOUNT SET  AccStatus = '1' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + MemNo + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    }
                    gvShareDetails.Visible = false;
                    lbltotalshare.Visible = false;
                    lblShareTotalAmt.Visible = false;
                }
            }

        }



        //---------------End of Share Garantor--------------------------------------------

        #endregion

        #region Property Gaurantor

        //-------------------Property Gaurantor----------------------------------------------------
        private void gvPropertyInfo()
        {

            string sqlquery3 = "SELECT LoanApplicationNo, PrSRL,PrName,FileNo,PrAmount,PrDesc FROM WFA2ZPRGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'And RowMode!=1";
            gvPropertyDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvPropertyDetails, "A2ZCSMCUS");



        }

        protected void SumPropertyValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvPropertyDetails.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvPropertyDetails.Rows[i].Cells[4].Text));
            }
            lblTotalProprty.Visible = true;
            lblSumProperty.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }
        protected void ClearProperty()
        {
            txtSerialNo.Text = string.Empty;
            txtNameProperty.Text = string.Empty;
            txtFileNo.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtProprertyAmt.Text = string.Empty;
        }

        protected void PropertyDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZPRGUAR (LoanApplicationNo, PrSRL,PrName,FileNo,PrAmount,PrDesc,RowType,RowMode ) SELECT LoanApplicationNo, PrSRL,PrName,FileNo,PrAmount,PrDesc,0,0 FROM A2ZPRGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


        }

        protected void BtnAddProperty_Click(object sender, EventArgs e)
        {
            if (txtSerialNo.Text == string.Empty || txtNameProperty.Text == string.Empty || txtFileNo.Text == string.Empty || txtDescription.Text == string.Empty || txtProprertyAmt.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtSerialNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);
                return;
            }


            try
            {
                string statment = "INSERT INTO  WFA2ZPRGUAR (LoanApplicationNo, PrSRL,PrName,FileNo,PrDesc,PrAmount,RowType,RowMode) VALUES('" + txtLoanAppNo.Text + "','" + txtSerialNo.Text + "','" + txtNameProperty.Text + "','" + txtFileNo.Text + "','" + txtDescription.Text + "','" + txtProprertyAmt.Text + "',1,0)";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                gvPropertyInfo();
                SumPropertyValue();
                TotalGuarantor();
                ClearProperty();
                txtSerialNo.Focus();






            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void SubmitPropertyData()
        {

            string statment = "INSERT INTO A2ZPRGUAR (LoanApplicationNo, PrSRL,PrName,FileNo,PrDesc,PrAmount) SELECT LoanApplicationNo, PrSRL,PrName,FileNo,PrDesc,PrAmount FROM WFA2ZPRGUAR WHERE LoanApplicationNo='" + txtLoanAppNo.Text + "'AND RowMode !=1";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            if (rowEffect > 0)
            {
                gvPropertyDetails.Visible = false;
                lblTotalProprty.Visible = false;
                lblSumProperty.Visible = false;
            }


        }

        protected void txtProprertyAmt_TextChanged(object sender, EventArgs e)
        {
            double amt = Converter.GetDouble(txtProprertyAmt.Text);
            txtProprertyAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
            BtnAddProperty.Focus();
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

        protected void gvShareDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvPropertyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNameProperty.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtNameProperty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtFileNo.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtFileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDescription.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtProprertyAmt.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //------------------------End Of Property Garantor---------------------------------------
        #endregion

        protected void Delete()
        {

            try
            {
                string sqlQuery = string.Empty;
                int rowEffect;

                sqlQuery = @"DELETE  FROM A2ZACGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

                sqlQuery = @"DELETE  FROM A2ZSHGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

                sqlQuery = @"DELETE  FROM A2ZPRGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void gvPropertyDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string SLNo = Converter.GetString(gvPropertyDetails.Rows[e.RowIndex].Cells[0].Text);
                string FileNo = Converter.GetString(gvPropertyDetails.Rows[e.RowIndex].Cells[2].Text);

                string sqlQuery1 = @"UPDATE WFA2ZPRGUAR  SET RowMode=1 WHERE PrSRL='" + SLNo + "' AND FileNo='" + FileNo + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1, "A2ZCSMCUS"));
                gvPropertyInfo();
                SumPropertyValue();
                TotalGuarantor();





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetailInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                string cutypetuno = string.Empty;

                cutypetuno = Converter.GetString(gvDetailInfo.Rows[e.RowIndex].Cells[0].Text);
                int a = cutypetuno.Length;
                string b = cutypetuno;
                string c = b.Substring(0, 1);
                int re = Converter.GetSmallInteger(c);
                string d = b.Substring(2);
                int re1 = Converter.GetSmallInteger(d);
                Int16 CuType = Converter.GetSmallInteger(re);
                int CNo = Converter.GetSmallInteger(re1);

                string MemNo = Converter.GetString(gvDetailInfo.Rows[e.RowIndex].Cells[1].Text);
                string AccType = Converter.GetString(gvDetailInfo.Rows[e.RowIndex].Cells[2].Text);
                string AccNo = Converter.GetString(gvDetailInfo.Rows[e.RowIndex].Cells[3].Text);
                string sqlQuery1 = @"UPDATE WFA2ZACGUAR  SET RowMode=1 WHERE CuType='" + CuType + "'and CuNo='" + CNo + "' and MemNo='" + MemNo + "' and AccType='" + AccType + "' and AccNo='" + AccNo + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1, "A2ZCSMCUS"));
                gvDepositInfo();
                SumDepositValue();
                TotalGuarantor();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvShareDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label IdNo = (Label)gvShareDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZSHGUAR WHERE   Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                gvShareInfo();
                SumShareValue();
                TotalGuarantor();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void TotalGuarantor()
        {
            double totalDeposit = Converter.GetDouble(txtTotalAmt.Text);
            double totalShare = Converter.GetDouble(lblShareTotalAmt.Text);
            double totalProperty = Converter.GetDouble(lblSumProperty.Text);
            double Garantor = (totalDeposit + totalShare + totalProperty);
            lblTotalResult.Text = Converter.GetString(String.Format("{0:0,0.00}", Garantor));
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlDeposit"] = "1";
            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "4";

            Session["SSPflag"] = "1";
            Session["SAccTypeGuaranty"] = lblAccTypeGuaranty.Text;

            Session["Module"] = lblModule.Text;

            Session["StxtLoanAppNo"] = txtLoanAppNo.Text;

            Session["TrnDate"] = txtLoanAppDate.Text;

            Session["StxtCreditUNo"] = txtCreditUNo.Text;
            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCu"] = lblCu.Text;
            Session["StxtDepositMemNo"] = txtDepositMemNo.Text;


            Session["SddlCreditUNo"] = ddlCreditUNo.SelectedValue;
            Session["SddlLoanMemNo"] = ddlLoanMemNo.SelectedValue;

            Session["LAcType"] = ddlAccType.SelectedValue;

            Session["SlblTypeCls"] = lblTypeCls.Text;

            Session["StxtLoanAppAmount"] = txtLoanAppAmount.Text;
            Session["StxtNoInstallment"] = txtNoInstallment.Text;
            Session["StxtLoanInterestRate"] = txtLoanInterestRate.Text;
            Session["StxtLoanInstallmentAmount"] = txtLoanInstallmentAmount.Text;
            Session["StxtLoanLastInstlAmount"] = txtLoanLastInstlAmount.Text;
            Session["StxtLoanExpDate"] = txtLoanExpDate.Text;

            Session["SddlLoanCategory"] = ddlLoanCategory.SelectedValue;
            Session["SddlLoanPurpose"] = ddlLoanPurpose.SelectedValue;

            Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;


            if (lblModule.Text == "4")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
     "click", @"<script>window.open('CSGetStaffAccountNo.aspx','_blank');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSGetAccountNo.aspx','_blank');</script>", false);
            }
        }


        protected void btnSSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlShare"] = "1";
            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "2";

            Session["Module"] = lblModule.Text;



            Session["StxtLoanAppNo"] = txtLoanAppNo.Text;

            Session["TrnDate"] = txtLoanAppDate.Text;

            Session["StxtCreditUNo"] = txtCreditUNo.Text;

            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCu"] = lblCu.Text;
            Session["StxtDepositMemNo"] = txtDepositMemNo.Text;

            Session["SddlCreditUNo"] = ddlCreditUNo.SelectedValue;
            Session["SddlLoanMemNo"] = ddlLoanMemNo.SelectedValue;

            Session["LAcType"] = ddlAccType.SelectedValue;

            Session["StxtLoanAppAmount"] = txtLoanAppAmount.Text;
            Session["StxtNoInstallment"] = txtNoInstallment.Text;
            Session["StxtLoanInterestRate"] = txtLoanInterestRate.Text;
            Session["StxtLoanInstallmentAmount"] = txtLoanInstallmentAmount.Text;
            Session["StxtLoanLastInstlAmount"] = txtLoanLastInstlAmount.Text;
            Session["StxtLoanExpDate"] = txtLoanExpDate.Text;

            Session["SddlLoanCategory"] = ddlLoanCategory.SelectedValue;
            Session["SddlLoanPurpose"] = ddlLoanPurpose.SelectedValue;

            Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;


            Session["ExFlag"] = "4";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);


        }

        public void GetAccInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    //InvalidAccountNoMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    txtAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                    hdnAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                    txtLedgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    lblStatus.Text = Converter.GetString(accgetDTO.AccStatus);
                    if (lblStatus.Text == "99")
                    {
                        InvalidAccMSG();
                        txtAccNo.Focus();
                    }
                    else
                    {
                        txtLionAmt.Focus();
                    }

                    lblDepositCuType.Text = Converter.GetString(accgetDTO.CuType);

                    lblDepositCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCrUNo.Text = (lblDepositCuType.Text + "-" + lblDepositCuNo.Text);

                    txtDepositMemNo.Text = Converter.GetString(accgetDTO.MemberNo);

                    txtAccType.Text = Converter.GetString(accgetDTO.AccType);

                    Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                    if (get3DTO.AccTypeCode > 0)
                    {
                        lblAccountTypeName.Text = Converter.GetString(get3DTO.AccTypeDescription);
                    }


                    Int16 CType = Converter.GetSmallInteger(lblDepositCuType.Text);
                    int CNo = Converter.GetInteger(lblDepositCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCrName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblDepositCuType.Text);
                    int CUNo = Converter.GetInteger(lblDepositCuNo.Text);
                    int MNo = Converter.GetInteger(txtDepositMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemberName.Text = Converter.GetString(get6DTO.MemberName);
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }

        protected void txtODPeriod_TextChanged(object sender, EventArgs e)
        {
            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtODPeriod.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtODExpDate.Text = date;
        }

    }
}