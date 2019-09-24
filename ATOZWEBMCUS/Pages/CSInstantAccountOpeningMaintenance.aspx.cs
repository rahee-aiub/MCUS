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
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSInstantAccountOpeningMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnNewMem.Visible = false;
                    BtnCUHelp.Visible = false;
                    BtnMemHelp.Visible = false;

                    string flag = (string)Session["flag"];
                    string Nflag = (string)Session["NFlag"];

                    lblflag.Text = flag;
                    if (lblflag.Text == "1" && Nflag == "1")
                    {
                        string CuNo = (string)Session["RCreditUNo"];
                        string OCuNo = (string)Session["RtxtOldCuNo"];

                        string acctype = (string)Session["AccType"];
                        txtAccType.Text = acctype;
                        
                        txtCuNo.Text = CuNo;
                        lblOldCuNo.Text = OCuNo;

                        
                        btnNewMem.Visible = true;
                        BtnCUHelp.Visible = true;
                        BtnMemHelp.Visible = true;
                        txtAccType_TextChanged(this, EventArgs.Empty);
                        txtCuNo_TextChanged(this, EventArgs.Empty);
                        BtnSubmit.Visible = true;
                        txtMemberNo.Focus();


                        SessionRemove1();
                    }

                    if (lblflag.Text == "1" && Nflag == "2")
                    {
                        string acctype = (string)Session["AccType"];
                        txtAccType.Text = acctype;

                        string CuNo = (string)Session["RCreditUNo"];
                        txtCuNo.Text = CuNo;
                        string MemNo = (string)Session["RMemNo"];
                        txtMemberNo.Text = MemNo;

                        string cuno = (string)Session["Cuno"];
                        txtCuNo.Text = cuno;

                        string cutype = (string)Session["CuType"];
                        lblCuType.Text = cutype;
                        string crno = (string)Session["CrNo"];
                        lblCuNum.Text = crno;

                        string cuname = (string)Session["Cuname"];
                        lblCuName.Text = cuname;


                        txtAccType_TextChanged(this, EventArgs.Empty);
                        //txtCuNo_TextChanged(this, EventArgs.Empty);
                        txtMemberNo_TextChanged(this, EventArgs.Empty);
                        SessionRemove1();
                    }

                    string TranDate = (string)Session["STranDate"];
                    string Func = (string)Session["SFuncOpt"];
                    string Module = (string)Session["SModule"];
                    string VchNo = (string)Session["SVchNo"];



                    CtrlFunc.Text = "1";
                    CtrlModule.Text = Module;

                    lblFuncOpt.Text = Func;
                    lblModule.Text = Module;

                    //DateTime tdt = Converter.GetDateTime(TranDate);
                    //string tdate = tdt.ToString("dd/MM/yyyy");

                    lblTranDate.Text = TranDate;
                    txtOpenDate.Text = TranDate;
                    lblVchNo.Text = VchNo;

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    if (CtrlFunc.Text == "1")
                    {
                        lblFuncTitle.Text = "Account Openining Maintenance";
                    }

                    if (CtrlFunc.Text == "2")
                    {
                        lblFuncTitle.Text = "Account Edit Maintenance";
                    }

                    lblCuName.Visible = true;
                    lblMemName.Visible = true;

                    BtnSubmit.Visible = true;

                    txtOpenDate.ReadOnly = true;


                   
                    string ctrlflag = (string)Session["CtrlFlag"];
                    lblflag.Text = ctrlflag;
                    if (lblflag.Text == "1" && Nflag == "")
                    {
                        string memno = (string)Session["NewMemNo"];
                        txtMemberNo.Text = memno;
                        string memname = (string)Session["NewMemName"];
                        lblMemName.Text = memname;

                        string acctype = (string)Session["AccType"];
                        txtAccType.Text = acctype;
                        string cuno = (string)Session["Cuno"];
                        txtCuNo.Text = cuno;

                        string cutype = (string)Session["CuType"];
                        lblCuType.Text = cutype;
                        string crno = (string)Session["CrNo"];
                        lblCuNum.Text = crno;

                        string cuname = (string)Session["Cuname"];
                        lblCuName.Text = cuname;
                    }

                    if (lblflag.Text == "1")
                    {

                        //string Nflag = (string)Session["Nflag"];


                        string StxtInterestRate = (string)Session["HtxtInterestRate"];
                        string StxtLoanPaymentSchdule = (string)Session["HtxtLoanPaymentSchdule"];
                        string StxtSpInstruction = (string)Session["HtxtSpInstruction"];
                        string StxtDepositAmount = (string)Session["HtxtDepositAmount"];
                        string StxtFixedDepositAmount = (string)Session["HtxtFixedDepositAmount"];
                        string StxtPeriod = (string)Session["HtxtPeriod"];
                        string SddlWithdrawalAC = (string)Session["HddlWithdrawalAC"];
                        string SddlInterestCalculation = (string)Session["HddlInterestCalculation"];
                        string StxtMatrutiyDate = (string)Session["HtxtMatrutiyDate"];
                        string StxtMatruityAmount = (string)Session["HtxtMatruityAmount"];
                        string SddlInterestWithdraw = (string)Session["HddlInterestWithdraw"];
                        string StxtFixedMthInt = (string)Session["HtxtFixedMthInt"];
                        string SddlAutoRenewal = (string)Session["HddlAutoRenewal"];
                        string StxtLoanAmount = (string)Session["HtxtLoanAmount"];
                        string StxtNoOfInstallment = (string)Session["HtxtNoOfInstallment"];
                        string StxtMonthlyInstallment = (string)Session["HtxtMonthlyInstallment"];
                        string StxtGracePeriod = (string)Session["HtxtGracePeriod"];
                        string StxtLoaneeACType = (string)Session["HtxtLoaneeACType"];
                        string StxtLoaneeMemberNo = (string)Session["HtxtLoaneeMemberNo"];
                        string SChkContraInt = (string)Session["HChkContraInt"];


                        txtInterestRate.Text = StxtInterestRate;
                        txtLoanPaymentSchdule.Text = StxtLoanPaymentSchdule;
                        txtSpInstruction.Text = StxtSpInstruction;
                        txtDepositAmount.Text = StxtDepositAmount;
                        txtFixedDepositAmount.Text = StxtFixedDepositAmount;
                        txtPeriod.Text = StxtPeriod;
                        ddlWithdrawalAC.SelectedValue = SddlWithdrawalAC;
                        ddlInterestCalculation.SelectedValue = SddlInterestCalculation;
                        txtMatrutiyDate.Text = StxtMatrutiyDate;
                        txtMatruityAmount.Text = StxtMatruityAmount;
                        ddlInterestWithdraw.SelectedValue = SddlInterestWithdraw;
                        txtFixedMthInt.Text = StxtFixedMthInt;
                        ddlAutoRenewal.SelectedValue = SddlAutoRenewal;
                        txtLoanAmount.Text = StxtLoanAmount;
                        txtNoOfInstallment.Text = StxtNoOfInstallment;
                        txtMonthlyInstallment.Text = StxtMonthlyInstallment;
                        txtGracePeriod.Text = StxtGracePeriod;
                        txtLoaneeACType.Text = StxtLoaneeACType;
                        txtLoaneeMemberNo.Text = StxtLoaneeMemberNo;

                        if (SChkContraInt == "1")
                        {
                            ChkContraInt.Checked = true;
                        }
                        else
                        {
                            ChkContraInt.Checked = false;
                        }


                        if (Nflag == "")
                        {
                            btnNewMem.Visible = true;
                            BtnCUHelp.Visible = true;
                            BtnMemHelp.Visible = true;
                            txtAccType_TextChanged(this, EventArgs.Empty);
                            BtnSubmit.Visible = true;
                            txtMemberNo.Focus();

                            SessionRemove();
                        }
                    }
                    else
                    {
                        DeleteWfNomineedata();
                        Visible();
                        txtAccType.Focus();
                    }

                    AccountTypeDropdown();


                    string sqlquery = "SELECT ProductCode, RecordCode,RecordFlag,FuncFlag from A2ZACCCTRL";
                    gvHidden = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvHidden, "A2ZCSMCUS");
                    gvHidden.Visible = false;
                    // BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    ProcDate.Text = date;


                    //DeleteWfNomineedata();


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");

                //throw ex;
            }

        }

        protected void AccountTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode!=2 AND AccTypeClass!=7 AND AccTypeClass!=5 AND AccTypeClass!=6";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");

        }

        protected void SessionRemove()
        {
            Session["NewMemNo"] = string.Empty;
            Session["NewMemName"] = string.Empty;
            Session["AccType"] = string.Empty;
            Session["Cuno"] = string.Empty;
            Session["Cuname"] = string.Empty;
            Session["CtrlFlag"] = string.Empty;

            Session["ProgCtrl"] = string.Empty;

            Session["HtxtInterestRate"] = string.Empty;
            Session["HtxtLoanPaymentSchdule"] = string.Empty;
            Session["HtxtSpInstruction"] = string.Empty;
            Session["HtxtDepositAmount"] = string.Empty;
            Session["HtxtFixedDepositAmount"] = string.Empty;
            Session["HtxtPeriod"] = string.Empty;
            Session["HddlWithdrawalAC"] = string.Empty;
            Session["HddlInterestCalculation"] = string.Empty;
            Session["HtxtMatrutiyDate"] = string.Empty;
            Session["HtxtMatruityAmount"] = string.Empty;
            Session["HddlInterestWithdraw"] = string.Empty;
            Session["HtxtFixedMthInt"] = string.Empty;
            Session["HddlAutoRenewal"] = string.Empty;
            Session["HtxtLoanAmount"] = string.Empty;
            Session["HtxtNoOfInstallment"] = string.Empty;
            Session["HtxtMonthlyInstallment"] = string.Empty;
            Session["HtxtGracePeriod"] = string.Empty;
            Session["HtxtLoaneeACType"] = string.Empty;
            Session["HtxtLoaneeMemberNo"] = string.Empty;
            Session["HChkContraInt"] = string.Empty;
        }
        protected void SessionRemove1()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["RtxtOldCuNo"] = string.Empty;
           
            Session["AccType"] = string.Empty;

            Session["Cuno"] = string.Empty;
            Session["Cuname"] = string.Empty;

            Session["RlblAccFlag"] = string.Empty;
            Session["Rlblchk1Hide"] = string.Empty;
            Session["Rlblchk2Hide"] = string.Empty;
            Session["Rlblchk3Hide"] = string.Empty;

            Session["flag"] = string.Empty;
            Session["NFlag"] = string.Empty;

        }
        protected void Visible()
        {
            lblAccType.Enabled = true;
            txtAccountNo.Visible = false;

            txtCuNo.Visible = false;
            lblCuNo.Visible = false;

            txtMemberNo.Visible = false;
            lblMemberNo.Visible = false;
            txtOpenDate.Visible = false;
            lblOpendate.Visible = false;



            txtInterestRate.Visible = false;
            lblInterestRate.Visible = false;

            txtLoanPaymentSchdule.Visible = false;
            lblLoanPaymentSchdule.Visible = false;
            txtSpInstruction.Visible = false;
            lblSpInstruction.Visible = false;
            ValidDepositAmount.Visible = false;
            txtDepositAmount.Visible = false;
            lblDepositAmount.Visible = false;
            ValidFixedDepositAmount.Visible = false;
            txtFixedDepositAmount.Visible = false;
            lblFixedDepositAmount.Visible = false;
            ValidPeriod.Visible = false;
            txtPeriod.Visible = false;
            lblPeriod.Visible = false;
            ddlWithdrawalAC.Visible = false;
            lblWithdrawalAC.Visible = false;
            ddlInterestCalculation.Visible = false;
            lblInterestCalculation.Visible = false;
            txtMatrutiyDate.Visible = false;
            lblMatruityDate.Visible = false;
            txtMatruityAmount.Visible = false;
            lblMatruityAmount.Visible = false;
            ddlInterestWithdraw.Visible = false;
            lblInterestWithdraw.Visible = false;
            txtFixedMthInt.Visible = false;
            lblFixedMthInt.Visible = false;
            ddlAutoRenewal.Visible = false;
            lblAutoRenewal.Visible = false;
            txtLoanAmount.Visible = false;
            lblLoanAmount.Visible = false;
            txtNoOfInstallment.Visible = false;
            lblNoOfInstallment.Visible = false;
            txtMonthlyInstallment.Visible = false;
            lblMonthlyInstallment.Visible = false;
            ChkContraInt.Visible = false;
            lblContractInt.Visible = false;
            txtGracePeriod.Visible = false;
            lblGracePeriod.Visible = false;
            txtLoaneeACType.Visible = false;
            lblLoaneeACType.Visible = false;
            txtLoaneeMemberNo.Visible = false;
            lblLoaneeMemberNo.Visible = false;
            txtCorrAccType.Visible = false;
            txtCorrAccNo.Visible = false;
            lblCorrAccount.Visible = false;
            ddlAutoTransferSaving.Visible = false;
            lblAutoTransferSaving.Visible = false;
            txtOldAccNo.Visible = false;
            lblOldAccNo.Visible = false;
            txtLastInstallment.Visible = false;
            lblLastInstallment.Visible = false;

        }


        protected void HeadFocus()
        {
            if (txtCuNo.Visible == true && txtMemberNo.Visible == true && txtAccountNo.Visible == true)
            {
                txtCuNo.Focus();
            }
            else if (txtCuNo.Visible == false && txtMemberNo.Visible == true)
            {
                txtMemberNo.Focus();
            }
            else if (txtCuNo.Visible == true)
            {
                txtCuNo.Focus();
            }

        }


        //protected void txtAccType_TextChanged(object sender, EventArgs e)
        //{


        //}



        private void clearInfo()
        {
            //txtBranchNo.Text=string.Empty;
            //txtAccType.Text=string.Empty;
            txtAccountNo.Text = string.Empty;

            txtCuNo.Text = string.Empty;
            lblCuType.Text = string.Empty;
            lblCuNum.Text = string.Empty;

            lblMemName.Text = string.Empty;
            lblCuName.Text = string.Empty;

            txtMemberNo.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtSpInstruction.Text = string.Empty;
            //txtOpenDate.Text = string.Empty;
            txtDepositAmount.Text = string.Empty;
            txtFixedDepositAmount.Text = string.Empty;
            txtPeriod.Text = string.Empty;
            ddlWithdrawalAC.SelectedValue = "0";
            ddlInterestCalculation.SelectedValue = "0";
            txtMatrutiyDate.Text = string.Empty;
            txtMatruityAmount.Text = string.Empty;
            ddlInterestWithdraw.SelectedValue = "0";
            txtFixedMthInt.Text = string.Empty;
            ddlAutoRenewal.SelectedValue = "0";
            txtLoanAmount.Text = string.Empty;
            txtLoaneeACType.Text = string.Empty;
            txtLoaneeMemberNo.Text = string.Empty;
            txtNoOfInstallment.Text = string.Empty;
            txtMonthlyInstallment.Text = string.Empty;
            txtLastInstallment.Text = string.Empty;
            ChkContraInt.Checked = false;
            txtGracePeriod.Text = string.Empty;
            txtLoaneeACType.Text = string.Empty;
            txtLoaneeMemberNo.Text = string.Empty;
            txtCorrAccType.Text = string.Empty;
            txtCorrAccNo.Text = string.Empty;
            ddlAutoTransferSaving.SelectedValue = "0";
            txtOldAccNo.Text = string.Empty;
        }

        protected void GetNomineeInfo()
        {
            try
            {

                if (CtrlFunc.Text == "2")
                {
                    DeleteWfNomineedata();
                    string statment = "INSERT INTO WFCSA2ZACCNOM(CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomThana,NomRela, NomSharePer)SELECT CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomThana,NomRela, NomSharePer FROM A2ZACCNOM WHERE AccType='" + txtAccType.Text + "' and AccNo= '" + txtAccountNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "' ";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                    if (rowEffect > 0)
                    {
                        string qry = "UPDATE WFCSA2ZACCNOM SET UserId = '" + hdnID.Text + "' WHERE UserId = 0";
                        int row1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetNomineeInfo Problem');</script>");


            }

        }


        protected void GetAccountInfo1()
        {
            try
            {

                CtrlMsgFlag.Text = "0";

                int Acctype = Converter.GetInteger(txtAccType.Text);
                Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                int CUnumber = Converter.GetInteger(lblCuNum.Text);
                int MemNumber = Converter.GetInteger(txtMemberNo.Text);

                lblOrgAccType.Text = Converter.GetString(txtAccType.Text);

                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfo(Acctype, CUType, CUnumber, MemNumber));

                if (getDTO.a > 0)
                {
                    if (CtrlFunc.Text == "1")
                    {
                        if (lblAccFlag.Text == "1")
                        {
                            CtrlMsgFlag.Text = "1";
                            BtnSubmit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnNominee.Visible = false;
                            AccountExistsMSG();
                            return;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
        }

        protected void GetAccountInfo2()
        {
            try
            {

                CtrlMsgFlag.Text = "0";

                if (lblAccFlag.Text == "3" && txtMemberNo.Text != "0")
                {
                    txtMemberNo.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    txtMemberNo.Focus();

                    CtrlMsgFlag.Text = "1";
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnNominee.Visible = false;
                    InvalidRecords();
                    return;
                }



                int Acctype = Converter.GetInteger(txtAccType.Text);
                Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                int CUnumber = Converter.GetInteger(lblCuNum.Text);
                int MemNumber = Converter.GetInteger(txtMemberNo.Text);

                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfo(Acctype, CUType, CUnumber, MemNumber));

                if (getDTO.a > 0)
                {
                    if (CtrlFunc.Text == "1")
                    {
                        if (lblAccFlag.Text == "2")
                        {
                            txtMemberNo.Text = string.Empty;
                            lblMemName.Text = string.Empty;
                            txtMemberNo.Focus();
                           
                            CtrlMsgFlag.Text = "1";
                            BtnSubmit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnNominee.Visible = false;
                            AccountExistsMSG();
                            return;
                        }


                    }
                }
                
                
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
        }



        protected void GetInfo()
        {
            try
            {
                GetNomineeInfo();


                CtrlMsgFlag.Text = "0";

                int Acctype = Converter.GetInteger(txtAccType.Text);
                Int64 AccNumber = Converter.GetLong(txtAccountNo.Text);
                Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                int CUnumber = Converter.GetInteger(lblCuNum.Text);
                int MemNumber = Converter.GetInteger(txtMemberNo.Text);

                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInformation(Acctype, AccNumber, CUType, CUnumber, MemNumber));

                if (getDTO.a > 0)
                {
                    CtrlAccStatus.Text = Converter.GetString(getDTO.AccStatus);
                    if (CtrlAccStatus.Text == "99")
                    {
                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        CtrlMsgFlag.Text = "1";
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        BtnNominee.Visible = false;
                        AccClosedMSG();
                        return;
                    }

                    if (CtrlFunc.Text == "1")
                    {
                        if (lblAccFlag.Text == "1")
                        {
                            txtAccountNo.Text = string.Empty;
                            txtAccountNo.Focus();
                            CtrlMsgFlag.Text = "1";
                            BtnSubmit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnNominee.Visible = false;
                            AccountExistsMSG();
                            return;
                        }
                        else
                        {
                            BtnSubmit.Visible = true;
                            BtnUpdate.Visible = false;
                            txtOpenDate.Focus();
                            return;
                        }
                    }

                    else
                    {
                        txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositAmount));
                        txtFixedDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.FixedDepositAmount));
                        txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.FixedMthInt));

                        CtrlOrgAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccOrgAmt));

                        if (CtrlOrgAmt.Text != "00.00")
                        {
                            txtFixedDepositAmount.ReadOnly = true;
                            txtFixedMthInt.ReadOnly = true;
                        }

                        DateTime dt = Converter.GetDateTime(getDTO.Opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        GetOpenDate.Text = date;

                        //txtOpenDate.Text = Converter.GetString( getDTO.Opendate.ToShortDateString());
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        ddlWithdrawalAC.SelectedValue = Converter.GetString(getDTO.WithDrawalAC);
                        ddlInterestCalculation.SelectedValue = Converter.GetString(getDTO.InterestCalculation);

                        if (getDTO.MatruityDate == DateTime.MinValue)
                        {
                            txtMatrutiyDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt1 = Converter.GetDateTime(getDTO.MatruityDate);
                            string date1 = dt1.ToString("dd/MM/yyyy");
                            txtMatrutiyDate.Text = date1;
                        }


                        if (getDTO.AccBenefitDate == DateTime.MinValue)
                        {
                            CtrlBenefitDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt2 = Converter.GetDateTime(getDTO.AccBenefitDate);
                            string date2 = dt2.ToString("dd/MM/yyyy");
                            CtrlBenefitDate.Text = date2;
                        }

                        //txtMatrutiyDate.Text = Converter.GetString(getDTO.MatruityDate);
                        txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MatruityAmount));

                        ddlInterestWithdraw.SelectedValue = Converter.GetString(getDTO.InterestWithdrawal);
                        ddlAutoRenewal.SelectedValue = Converter.GetString(getDTO.AutoRenewal);
                        txtLoanAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LoanAmount));

                        txtNoOfInstallment.Text = Converter.GetString(getDTO.NoInstallment);
                        txtMonthlyInstallment.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MonthlyInstallment));

                        txtLastInstallment.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LastInstallment));
                        txtContractInt.Text = Converter.GetString(getDTO.ContractInt);
                        if (txtContractInt.Text == "1")
                        {
                            ChkContraInt.Checked = true;
                        }
                        else
                        {
                            ChkContraInt.Checked = false;
                        }
                        txtGracePeriod.Text = Converter.GetString(getDTO.GracePeriod);
                        txtLoaneeACType.Text = Converter.GetString(getDTO.LoaneeACType);
                        txtLoaneeMemberNo.Text = Converter.GetString(getDTO.LoaneeMemberNo);
                        txtCorrAccType.Text = Converter.GetString(getDTO.CorrAccType);
                        txtCorrAccNo.Text = Converter.GetString(getDTO.CorrAccNo);
                        ddlAutoTransferSaving.SelectedValue = Converter.GetString(getDTO.AutoTransferSavings);
                        txtOldAccNo.Text = Converter.GetString(getDTO.OldAccountNo);
                        txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));
                        txtSpInstruction.Text = Converter.GetString(getDTO.SpInstruction);
                        txtAtyClass.Text = Converter.GetString(getDTO.AccAtyClass);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtOpenDate.Focus();
                    }
                }
                else
                {
                    if (CtrlFunc.Text == "2")
                    {
                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        CtrlMsgFlag.Text = "1";
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        BtnNominee.Visible = false;
                        AccountNotExistsMSG();
                        return;
                    }
                    else
                    {
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtOpenDate.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
        }





        protected void DisplayMessage()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";

            a = "Credit Union No. Already Transfered";
            e = "New Credit Union No.";
            b = string.Format("New Credit Union Type : {0}", lblCuTypeName.Text);
            c = string.Format(lblCuNum.Text);
            d = string.Format(lblCuType.Text);
            X = "-";

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e;
            Msg += d;
            Msg += X;
            Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }

        protected void txtCuNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCuNo.Text == string.Empty)
                {
                    txtCuNo.Focus();
                    return;
                }

                txtMemberNo.Text = string.Empty;
                lblMemName.Text = string.Empty;

                int CN = Converter.GetInteger(txtCuNo.Text);

                hdnCuNumber.Text = Converter.GetString(CN);

                string c = "";
                int a = txtCuNo.Text.Length;

                string b = txtCuNo.Text;
                c = b.Substring(0, 1);
                int re = Converter.GetSmallInteger(c);
                int dd = a - 1;
                string d = b.Substring(1, dd);
                int re1 = Converter.GetSmallInteger(d);


                Int16 CuType = Converter.GetSmallInteger(re);
                lblCuType.Text = Converter.GetString(CuType);
                int CNo = Converter.GetInteger(re1);
                lblCuNum.Text = Converter.GetString(CNo);

                A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                //if (getDTO.CreditUnionNo > 0)

                //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                if (getDTO.NoRecord > 0)
                {
                    lblCuType.Text = Converter.GetString(getDTO.CuType);
                    lblCuNum.Text = Converter.GetString(getDTO.CreditUnionNo);
                    lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                    lblCuNumber.Text = lblCuType.Text + lblCuNum.Text;


                    lblstat.Text = Converter.GetString(getDTO.CuStatus);
                    DateTime dt = Converter.GetDateTime(getDTO.opendate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblCuOpenDate.Text = date;

                    if (getDTO.CuStatus == 9)
                    {
                        if (getDTO.CuReguCuType == 0)
                        {
                            lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                            lblCuNum.Text = Converter.GetString(getDTO.CuAssoCuNo);
                            lblCuType.Text = Converter.GetString(getDTO.CuAssoCuType);
                        }
                        else
                        {
                            lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                            lblCuNum.Text = Converter.GetString(getDTO.CuReguCuNo);
                            lblCuType.Text = Converter.GetString(getDTO.CuReguCuType);
                        }


                        txtCuNo.Text = string.Empty;
                        txtCuNo.Focus();
                        DisplayMessage();
                        return;
                    }


                    if (CtrlModule.Text == "06")
                    {
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                    }
                    if (lblCuType.Text == "1" && lblchk1Hide.Text == "1")
                    {
                        txtMemberNo.Focus();
                        txtCuNo.Text = (c + "-" + d);
                    }

                    else if (lblCuType.Text == "2" && lblchk2Hide.Text == "1")
                    {
                        txtMemberNo.Focus();
                        txtCuNo.Text = (lblCuType.Text + "-" + lblCuNum.Text);
                    }
                    else if (lblCuType.Text == "3" && lblchk3Hide.Text == "1")
                    {
                        txtMemberNo.Focus();
                        txtCuNo.Text = (lblCuType.Text + "-" + lblCuNum.Text);
                    }
                    else
                    {
                        txtCuNo.Text = string.Empty;
                        txtCuNo.Focus();
                        InvalidRecords();
                        return;
                    }
                }
                else
                {
                    txtCuNo.Text = string.Empty;
                    txtCuNo.Focus();
                    CUInvMsg();
                    return;
                }

                GetAccountInfo1();
                if (CtrlMsgFlag.Text == "1")
                {
                    txtCuNo.Text = string.Empty;
                    txtCuNo.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCuNo_TextChanged Problem');</script>");

                //throw ex;
            }
        }



        //protected void ddlCuNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlCuNo.SelectedValue == "-Select-")
        //        {
        //            clearInfo();
        //            txtCuNo.Text = string.Empty;
        //            txtMemberNo.Focus();
        //            return;
        //        }



        //        if (ddlCuNo.SelectedValue != "-Select-")
        //        {

        //            string c = "";
        //            int a = ddlCuNo.SelectedValue.Length;

        //            string b = ddlCuNo.SelectedValue;
        //            c = b.Substring(0, 1);
        //            int re = Converter.GetSmallInteger(c);
        //            int dd = a - 1;
        //            string d = b.Substring(1, dd);
        //            int re1 = Converter.GetSmallInteger(d);


        //            Int16 CuType = Converter.GetSmallInteger(re);
        //            lblCuType.Text = Converter.GetString(CuType);
        //            int CNo = Converter.GetInteger(re1);
        //            lblCuNum.Text = Converter.GetString(CNo);

        //            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
        //            if (getDTO.CreditUnionNo > 0)
        //            {
        //                lblstat.Text = Converter.GetString(getDTO.CuStatus);
        //                DateTime dt = Converter.GetDateTime(getDTO.opendate);
        //                string date = dt.ToString("dd/MM/yyyy");
        //                lblCuOpenDate.Text = date;

        //                if (lblstat.Text == "9")
        //                {
        //                    CUTrnsfersms();
        //                    txtCuNo.Text = string.Empty;
        //                    txtCuNo.Focus();
        //                    return;
        //                }
        //                if (lblCuType.Text == "1" && lblchk1Hide.Text == "1")
        //                {
        //                    txtCuNo.Text = Converter.GetString(ddlCuNo.SelectedValue);
        //                    txtCuNo.Text = (c + "-" + d);
        //                }
        //                else if (lblCuType.Text == "2" && lblchk2Hide.Text == "1")
        //                {
        //                    txtCuNo.Text = Converter.GetString(ddlCuNo.SelectedValue);
        //                    txtCuNo.Text = (c + "-" + d);
        //                }
        //                else if (lblCuType.Text == "3" && lblchk3Hide.Text == "1")
        //                {
        //                    txtCuNo.Text = Converter.GetString(ddlCuNo.SelectedValue);
        //                    txtCuNo.Text = (c + "-" + d);
        //                }
        //                else
        //                {
        //                    InvalidRecords();
        //                    txtCuNo.Text = string.Empty;
        //                    txtCuNo.Focus();
        //                    return;
        //                }

        //                if (txtMemberNo.Visible == true)
        //                {
        //                    CMemberDropdown();
        //                    txtMemberNo.Focus();
        //                }
        //            }
        //            else
        //            {
        //                CUInvMsg();
        //                txtCuNo.Text = string.Empty;
        //                txtCuNo.Focus();
        //                ddlCuNo.SelectedValue = "-Select-";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCuNo_SelectedIndexChanged Problem');</script>");

        //        //throw ex;
        //    }
        //}

        private void AccClosedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account already Closed');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account already Closed');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Allowed Open an Account');", true);
        }
        private void InvalidRecords()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Allowed Open an Account');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Open an Account');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Allowed Open an Account');", true);
        }


        
        private void InvalidAccType()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Open an Account');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void AccountExistsMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account Exists');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Exists');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void AccountNotExistsMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account Not In File');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not In File');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Future Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void BeforeDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Before CU Open Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Before CU Open Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        private void CUTrnsfersms()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Credit Union already Transfer');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Credit Union already Transfer');", true);
            return;

        }
        private void CUInvMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Credit Union does not exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Credit Union does not Exist');", true);
            return;


        }

        private void MemInvMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Depositor does not exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Depositor does not exist');", true);
            return;


        }

        private void AbnormalMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Depositor does not exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Abnormal Operation');", true);
            return;


        }


        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberNo.Text == string.Empty)
                {
                    txtMemberNo.Focus();
                    return;
                }

                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CreditNumber = Converter.GetInteger(lblCuNum.Text);
                int MemNumber = Converter.GetInteger(txtMemberNo.Text);

                int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CreditNumber, CuNumber, MemNumber));

                A2ZMEMBERDTO getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    //txtMemberNo.Text = string.Empty;

                    txtMemberNo.Text = Converter.GetString(getDTO.MemberNo);

                    txtMemType.Text = Converter.GetString(getDTO.MemType);
                    lblMemName.Text = Converter.GetString(getDTO.MemberName);

                    if (CtrlModule.Text == "01")
                    {
                        //ddlMemberNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                    }
                    if (CtrlModule.Text == "06")
                    {

                    }
                    //txtMemberNo.Text = Converter.GetString(ddlMemberNo.SelectedValue);

                    if (txtAccountNo.Visible == false)
                    {

                        GetAccountInfo2();
                        if (CtrlMsgFlag.Text == "1")
                        {
                            return;
                        }
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtOpenDate.Focus();

                        SelectPeriod();
                    }
                    else
                    {
                        txtAccountNo.Focus();
                    }


                }
                else
                {
                    txtMemberNo.Text = string.Empty;
                    txtMemberNo.Focus();
                    MemInvMsg();
                }


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtMemberNo_TextChanged Problem');</script>");


                //throw ex;
            }
        }





        protected void txtAccountNo_TextChanged(object sender, EventArgs e)
        {

            try
            {
                GetInfo();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccountNo_TextChanged Problem');</script>");


            }
        }

        protected void UpdatedMSG()
        {
            string a = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            a = "    Account Open Sucessfully Done";

            string b = "Generated New Account No.";
            string c = string.Format(txtAccountNo.Text);

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(a);
            sb.Append("\\n");
            sb.Append("\\n");
            sb.Append(b);
            sb.Append(c);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "' and AccType='" + txtAccType.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);
        }

        protected void GenerateNewAccNo()
        {
            GetAccountCount();

            string input1 = Converter.GetString(lblCuNum.Text).Length.ToString();
            string input2 = Converter.GetString(txtMemberNo.Text).Length.ToString();
            string input3 = Converter.GetString(hdnNewAccNo.Text).Length.ToString();

            string result1 = "";
            string result2 = "";
            string result3 = "";

            if (input1 == "1")
            {
                result1 = "000";
            }
            if (input1 == "2")
            {
                result1 = "00";
            }
            if (input1 == "3")
            {
                result1 = "0";
            }

            if (input2 == "1")
            {
                result2 = "0000";
            }
            if (input2 == "2")
            {
                result2 = "000";
            }
            if (input2 == "3")
            {
                result2 = "00";
            }
            if (input2 == "4")
            {
                result2 = "0";
            }

            if (input3 == "1")
            {
                result3 = "000";
            }
            if (input3 == "2")
            {
                result3 = "00";
            }
            if (input3 == "3")
            {
                result3 = "0";
            }

            if (input1 != "4" && input2 != "5" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 != "5" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + result2 + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + result2 + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 == "5" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }
            if (input1 == "4" && input2 == "5" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + txtMemberNo.Text + hdnNewAccNo.Text;
            }

        }



        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtAccType.Text !=lblOrgAccType.Text)
                //{
                //    clearInfo();
                //    AbnormalMsg();
                //    return;
                //}



                if (txtCuNo.Text == string.Empty)
                {
                    txtCuNo.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Credit Union No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemberNo.Text == string.Empty)
                {
                    txtMemberNo.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Depositor No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                    return;
                }


                if (txtAtyClass.Text == "2" || txtAtyClass.Text == "3" || txtAtyClass.Text == "4")
                {
                    if (txtPeriod.Text == string.Empty || txtPeriod.Text == "0")
                    {
                        txtPeriod.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Period');", true);
                        return;
                    }
                }

                if (txtAtyClass.Text == "2")
                {
                    decimal rate = Converter.GetDecimal(txtInterestRate.Text);

                    if (txtInterestRate.Text == string.Empty || rate < 1)
                    {
                        txtInterestRate.Text = string.Empty;
                        txtInterestRate.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Interest Rate');", true);
                        return;
                    }
                }



                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(txtAccType.Text);
                //if (lblAccFlag.Text == "2")
                //{
                GenerateNewAccNo();
                //}
                //else
                //{
                //    txtAccountNo.Text = "0";
                //}

                A2ZACCOUNTDTO objDTO = new A2ZACCOUNTDTO();
                // objDTO.BranchNo = Converter.GetSmallInteger(txtBranchNo.Text);
                objDTO.AccType = Converter.GetInteger(txtAccType.Text);
                objDTO.AccNo = Converter.GetLong(txtAccountNo.Text);
                objDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
                objDTO.CuNo = Converter.GetInteger(lblCuNum.Text);
                objDTO.MemberNo = Converter.GetInteger(txtMemberNo.Text);

                if (txtOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.Opendate = opdate;
                }
                else
                {
                    string CheckopenDtNull = "";
                    objDTO.OpenNulldate = CheckopenDtNull;
                }
                objDTO.DepositAmount = Converter.GetDecimal(txtDepositAmount.Text);
                objDTO.FixedDepositAmount = Converter.GetDecimal(txtFixedDepositAmount.Text);
                objDTO.FixedMthInt = Converter.GetDecimal(txtFixedMthInt.Text);
                objDTO.Period = Converter.GetSmallInteger(txtPeriod.Text);
                objDTO.WithDrawalAC = Converter.GetSmallInteger(ddlWithdrawalAC.SelectedValue);
                objDTO.InterestCalculation = Converter.GetSmallInteger(ddlInterestCalculation.SelectedValue);
                if (txtMatrutiyDate.Text != string.Empty)
                {
                    DateTime Matdate = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.MatruityDate = Matdate;
                }
                else
                {
                    string CheckmaturityDtNull = "";
                    objDTO.MatruityNullDate = CheckmaturityDtNull;
                }

                if (CtrlBenefitDate.Text != string.Empty)
                {
                    DateTime benefitdate = DateTime.ParseExact(CtrlBenefitDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.AccBenefitDate = benefitdate;
                }
                else
                {
                    string CheckbenefitDtNull = "";
                    objDTO.AccBenefitNullDate = CheckbenefitDtNull;
                }

                objDTO.MatruityAmount = Converter.GetDecimal(txtMatruityAmount.Text);
                objDTO.InterestWithdrawal = Converter.GetSmallInteger(ddlInterestWithdraw.SelectedValue);
                objDTO.AutoRenewal = Converter.GetSmallInteger(ddlAutoRenewal.SelectedValue);
                objDTO.LoanAmount = Converter.GetDecimal(txtLoanAmount.Text);
                objDTO.NoInstallment = Converter.GetSmallInteger(txtNoOfInstallment.Text);
                objDTO.MonthlyInstallment = Converter.GetDecimal(txtMonthlyInstallment.Text);
                objDTO.LastInstallment = Converter.GetDecimal(txtLastInstallment.Text);
                objDTO.InterestRate = Converter.GetDecimal(txtInterestRate.Text);
                if (ChkContraInt.Checked == true)
                {
                    objDTO.ContractInt = Converter.GetSmallInteger(1);
                }
                else
                {
                    objDTO.ContractInt = Converter.GetSmallInteger(0);
                }

                objDTO.GracePeriod = Converter.GetSmallInteger(txtGracePeriod.Text);
                objDTO.LoaneeACType = Converter.GetInteger(txtLoaneeACType.Text);
                objDTO.LoaneeMemberNo = Converter.GetInteger(txtLoaneeMemberNo.Text);
                objDTO.CorrAccType = Converter.GetSmallInteger(txtCorrAccType.Text);
                objDTO.CorrAccNo = Converter.GetInteger(txtCorrAccNo.Text);
                objDTO.SpInstruction = Converter.GetString(txtSpInstruction.Text);
                objDTO.AutoTransferSavings = Converter.GetSmallInteger(ddlAutoTransferSaving.SelectedValue);
                objDTO.OldAccountNo = Converter.GetString(txtOldAccNo.Text);
                objDTO.AccStatus = Converter.GetSmallInteger(1);
                objDTO.AccAtyClass = Converter.GetSmallInteger(txtAtyClass.Text);

                objDTO.InputBy = Converter.GetSmallInteger(hdnID.Text);
                DateTime inputdate = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objDTO.InputByDate = inputdate;

                int roweffect = A2ZACCOUNTDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    InsertNomineedata();
                    InsertPensionData();
                    //UpdatedMSG();

                    HeadFocus();
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;

                    Session["NewAccNo"] = txtAccountNo.Text;
                    Session["flag"] = "1";
                    Session["RTranDate"] = lblTranDate.Text;
                    Session["RFuncOpt"] = lblFuncOpt.Text;
                    Session["RModule"] = lblModule.Text;
                    Session["RVchNo"] = lblVchNo.Text;

                    Session["Rflag"] = "1";

                    lblCuNumber.Text = (lblCuType.Text + lblCuNum.Text);

                    Session["RCreditUNo"] = lblCuNumber.Text;
                    Session["RMemNo"] = txtMemberNo.Text;

                    Session["CFlag"] = "1";

                    //Session["CuType"] = lblCuType.Text;
                    //Session["CrNo"] = lblCuNum.Text;
                    //Response.Redirect("A2ZERPModule.aspx");


                    if (lblFuncOpt.Text == "01")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByDeposit.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "02")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanSettlement.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "03" || lblFuncOpt.Text == "04")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawal.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "05" || lblFuncOpt.Text == "06")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawalInt.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "07" || lblFuncOpt.Text == "08")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanDisbursement.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "09" || lblFuncOpt.Text == "10")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByEncashment.aspx'; self.close();</script>", false);
                    }
                    else if (lblFuncOpt.Text == "11" || lblFuncOpt.Text == "12")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                          "click", @"<script>window.opener.location.href='CSDailyTransactionByAdjCrDr.aspx'; self.close();</script>", false);
                    }

                    SessionRemove();
                    clearInfo();

                    if (CtrlModule.Text == "1")
                    {
                        //ddlCuNo.SelectedIndex = 0;
                        //ddlMemberNo.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");


            }

        }


        protected void InsertPensionData()
        {
            if (txtAtyClass.Text == "4")
            {
                var prm = new object[6];

                prm[0] = lblCuType.Text;
                prm[1] = lblCuNum.Text;
                prm[2] = txtMemberNo.Text;
                prm[3] = txtAccType.Text;
                prm[4] = txtAccountNo.Text;
                prm[5] = txtDepositAmount.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSPensionDefaulterDataInsert", prm, "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            }

        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuNo.Text == string.Empty)
                {
                    txtCuNo.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Credit Union No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemberNo.Text == string.Empty)
                {
                    txtMemberNo.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Depositor No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                    return;
                }




                A2ZACCOUNTDTO UpDTO = new A2ZACCOUNTDTO();
                UpDTO.AccType = Converter.GetInteger(txtAccType.Text);
                UpDTO.AccNo = Converter.GetLong(txtAccountNo.Text);
                UpDTO.InterestCalculation = Converter.GetSmallInteger(ddlInterestCalculation.SelectedValue);
                UpDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
                UpDTO.CuNo = Converter.GetInteger(lblCuNum.Text);
                UpDTO.MemberNo = Converter.GetInteger(txtMemberNo.Text);
                UpDTO.DepositAmount = Converter.GetDecimal(txtDepositAmount.Text);
                UpDTO.FixedDepositAmount = Converter.GetDecimal(txtFixedDepositAmount.Text);
                UpDTO.FixedMthInt = Converter.GetDecimal(txtFixedMthInt.Text);
                UpDTO.Period = Converter.GetSmallInteger(txtPeriod.Text);
                UpDTO.WithDrawalAC = Converter.GetSmallInteger(ddlWithdrawalAC.SelectedValue);
                UpDTO.InterestCalculation = Converter.GetSmallInteger(ddlInterestCalculation.SelectedValue);
                UpDTO.InterestRate = Converter.GetSmallInteger(txtInterestRate.Text);
                UpDTO.SpInstruction = Converter.GetString(txtSpInstruction.Text);
                UpDTO.MatruityAmount = Converter.GetDecimal(txtMatruityAmount.Text);
                UpDTO.InterestWithdrawal = Converter.GetSmallInteger(ddlInterestWithdraw.SelectedValue);
                UpDTO.AutoRenewal = Converter.GetSmallInteger(ddlAutoRenewal.SelectedValue);
                UpDTO.LoanAmount = Converter.GetDecimal(txtLoanAmount.Text);
                UpDTO.NoInstallment = Converter.GetSmallInteger(txtNoOfInstallment.Text);
                UpDTO.MonthlyInstallment = Converter.GetDecimal(txtMonthlyInstallment.Text);
                UpDTO.LastInstallment = Converter.GetDecimal(txtLastInstallment.Text);
                UpDTO.InterestRate = Converter.GetDecimal(txtInterestRate.Text);

                if (txtOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.Opendate = opdate;
                }
                else
                {
                    string CheckopenDtNull = "";
                    UpDTO.OpenNulldate = CheckopenDtNull;
                }

                if (txtMatrutiyDate.Text != string.Empty)
                {
                    DateTime Matdate = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.MatruityDate = Matdate;
                }
                else
                {
                    string CheckmaturityDtNull = "";
                    UpDTO.MatruityNullDate = CheckmaturityDtNull;
                }

                if (CtrlBenefitDate.Text != string.Empty)
                {
                    DateTime benefitdate = DateTime.ParseExact(CtrlBenefitDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.AccBenefitDate = benefitdate;
                }
                else
                {
                    string CheckbenefitDtNull = "";
                    UpDTO.AccBenefitNullDate = CheckbenefitDtNull;
                }

                if (ChkContraInt.Checked == true)
                {
                    UpDTO.ContractInt = Converter.GetSmallInteger(1);
                }
                else
                {
                    UpDTO.ContractInt = Converter.GetSmallInteger(0);
                }

                UpDTO.GracePeriod = Converter.GetSmallInteger(txtGracePeriod.Text);
                UpDTO.LoaneeACType = Converter.GetInteger(txtLoaneeACType.Text);
                UpDTO.LoaneeMemberNo = Converter.GetInteger(txtLoaneeMemberNo.Text);
                UpDTO.CorrAccType = Converter.GetSmallInteger(txtCorrAccType.Text);
                UpDTO.CorrAccNo = Converter.GetInteger(txtCorrAccNo.Text);
                UpDTO.SpInstruction = Converter.GetString(txtSpInstruction.Text);
                UpDTO.AutoTransferSavings = Converter.GetSmallInteger(ddlAutoTransferSaving.SelectedValue);
                UpDTO.OldAccountNo = Converter.GetString(txtOldAccNo.Text);
                UpDTO.AccAtyClass = Converter.GetSmallInteger(txtAtyClass.Text);
                int roweffect = A2ZACCOUNTDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    InsertNomineedata();
                    clearInfo();
                    HeadFocus();
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;

                    if (CtrlModule.Text == "1")
                    {
                        //ddlCuNo.SelectedValue = "-Select-";
                        //ddlMemberNo.SelectedValue = "-Select-";
                    }


                    //      txtCuNo.Focus();


                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");


                //throw ex;
            }
        }

        //protected void txtDepositAmount_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtDepositAmount.Text != "0")
        //        {
        //            Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
        //            Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);
        //            double depAmount = Converter.GetDouble(txtDepositAmount.Text);
        //            Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
        //            A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

        //            if (getDTO.NoRecord > 0)
        //            {
        //                txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
        //                txtPeriod.Text = Converter.GetString(getDTO.Period);
        //                txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));


        //                DateTime Matdate = new DateTime();
        //                Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
        //                DateTime dt = Converter.GetDateTime(Matdate);
        //                string date = dt.ToString("dd/MM/yyyy");
        //                txtMatrutiyDate.Text = date;
        //                txtFixedDepositAmount.Focus();


        //            }
        //            else
        //            {

        //                //String csname1 = "PopupScript";
        //                //Type cstype = GetType();
        //                //ClientScriptManager cs = Page.ClientScript;

        //                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //                //{
        //                //    String cstext1 = "alert('Deposit Amount not Assign in Parameter');";
        //                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);


        //                //}
        //                txtDepositAmount.Text = string.Empty;
        //                txtDepositAmount.Focus();
        //                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Deposit Amount not Assign in Parameter');", true);
        //                return;



        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtDepositAmount_TextChanged Problem');</script>");


        //        //throw ex;
        //    }
        //}

        protected void DepositAmtValidity()
        {
            CtrlFlag.Text = "0";
            NoRoundingBy.Text = "0";
            RoundingByFlag.Text = "1";
            double A = 0;
            double B = 0;

            A = Converter.GetDouble(txtDepositAmount.Text);
            int TrnAmt = Converter.GetInteger(A);

            int DepRoundingBy = 0;

            B = Converter.GetDouble(lblAccDepRoundingBy.Text);
            DepRoundingBy = Converter.GetInteger(B);


            if (DepRoundingBy != 0)
            {
                int mod = TrnAmt % DepRoundingBy;

                if (mod != 0)
                {
                    CtrlFlag.Text = "1";
                }
                else
                {
                    NoRoundingBy.Text = Converter.GetString(A / B);
                }
            }

        }
        protected void PeriodSlab1()
        {
            try
            {
                if (txtPeriod.Text != "0" && txtDepositAmount.Text != "0")
                {
                    RoundingByFlag.Text = "0";
                    double b = Converter.GetDouble(lblAccDepRoundingBy.Text);
                    double depAmount = Converter.GetDouble(txtDepositAmount.Text);
                    if (depAmount > b)
                    {
                        DepositAmtValidity();
                        if (CtrlFlag.Text == "1")
                        {
                            txtDepositAmount.Text = string.Empty;
                            txtDepositAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Deposit Amount');", true);
                            return;
                        }
                        else
                        {
                            depAmount = b;
                        }
                    }

                    int accType = Converter.GetInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);

                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

                    if (getDTO.NoRecord > 0)
                    {
                        //txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));
                        txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));

                        if (RoundingByFlag.Text == "1")
                        {
                            double NoRBy = Converter.GetDouble(NoRoundingBy.Text);
                            double MAmount = Converter.GetDouble(txtMatruityAmount.Text);
                            double NMAmount = Converter.GetDouble(MAmount * NoRBy);
                            txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", NMAmount));
                        }

                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;
                        ddlWithdrawalAC.Focus();
                    }
                    else
                    {
                        {
                            //String csname1 = "PopupScript";
                            //Type cstype = GetType();
                            //ClientScriptManager cs = Page.ClientScript;

                            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                            //{
                            //    String cstext1 = "alert('Period not Assign in Parameter');";
                            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);


                            txtDepositAmount.Text = string.Empty;
                            txtPeriod.Text = string.Empty;
                            txtDepositAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Assign in Parameter Slab');", true);


                            //}
                            return;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PeriodSlab1 Problem');</script>");


                //throw ex;
            }
        }


        protected void PeriodSlab2()
        {
            try
            {
                if (txtPeriod.Text != "0")
                {
                    int accType = Converter.GetInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);
                    double depAmount = Converter.GetDouble(txtDepositAmount.Text);
                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

                    if (getDTO.NoRecord > 0)
                    {
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));
                        txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));

                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;
                        ddlWithdrawalAC.Focus();
                    }
                    else
                    {

                        txtPeriod.Text = string.Empty;
                        txtPeriod.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Period not Assign in Parameter');", true);
                        return;


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PeriodSlab2 Problem');</script>");


                //throw ex;
            }
        }


        protected void PeriodSlab3()
        {
            try
            {
                if (txtPeriod.Text != "0")
                {
                    int accType = Converter.GetInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);
                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.Get3Information(accType, SlabFlag, period));

                    if (getDTO.NoRecord > 0)
                    {
                        PrmDepositAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        double PrmdepAmount = Converter.GetDouble(PrmDepositAmt.Text);

                        double depAmount = Converter.GetDouble(txtFixedDepositAmount.Text);

                        if (depAmount < PrmdepAmount)
                        {
                            txtFixedDepositAmount.Text = string.Empty;
                            txtPeriod.Text = string.Empty;
                            txtFixedDepositAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Fixed Deposit Amount');", true);
                            return;
                        }

                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));
                        double PrmdMthInt = Converter.GetDouble(txtFixedMthInt.Text);

                        double MthInt = ((PrmdMthInt / PrmdepAmount) * depAmount);

                        txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", MthInt));



                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;
                        ddlWithdrawalAC.Focus();

                        DateTime Benefitdate = new DateTime();
                        Benefitdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Benefitdate = Benefitdate.AddMonths(Converter.GetSmallInteger(1));
                        DateTime dt1 = Converter.GetDateTime(Benefitdate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        CtrlBenefitDate.Text = date1;
                    }
                    else
                    {
                        {
                            //String csname1 = "PopupScript";
                            //Type cstype = GetType();
                            //ClientScriptManager cs = Page.ClientScript;

                            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                            //{
                            //    String cstext1 = "alert('Period not Assign in Parameter');";
                            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                            txtFixedDepositAmount.Text = string.Empty;
                            txtPeriod.Text = string.Empty;
                            txtPeriod.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Period not Assign in Parameter');", true);

                            //}
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PeriodSlab3 Problem');</script>");


                //throw ex;
            }
        }

        protected void SelectPeriod()
        {
            string qry = "SELECT Id,AtyPeriod FROM A2ZATYSLAB  WHERE AtyAccType = '" + txtAccType.Text + "' AND AtyFlag='" + txtMemType.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {

                    txtPeriod.Text = Converter.GetString(dt.Rows[0]["AtyPeriod"]);

                    txtPeriod_TextChanged(this, EventArgs.Empty);
                }
            }
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CreditNumber = Converter.GetInteger(lblCuNum.Text);
            int MemNumber = Converter.GetInteger(txtMemberNo.Text);
            A2ZMEMBERDTO getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

            if (getDTO.NoRecord > 0)
            {
                txtMemType.Text = Converter.GetString(getDTO.MemType);
            }

            if (txtAtyClass.Text == "2")
            {
                PeriodSlab2();
            }

            if (txtAtyClass.Text == "3")
            {
                PeriodSlab3();
            }

            if (txtAtyClass.Text == "4")
            {
                PeriodSlab1();
            }


        }

        //protected void txtInterestRate_TextChanged(object sender, EventArgs e)
        //{

        //    double ValueConvert = Converter.GetDouble(txtInterestRate.Text);
        //    txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
        //   // txtContractInt.Focus();

        //}

        protected void txtLoanAmount_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLoanAmount.Text);
            txtLoanAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtNoOfInstallment.Focus();

        }

        protected void txtOpenDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtOpenDate.Text != "")
                {
                    DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime opdate3 = DateTime.ParseExact(lblCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime opdate2 = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (opdate1 > opdate2)
                    {
                        InvalidDateMSG();
                        if (CtrlFunc.Text == "1")
                        {
                            txtOpenDate.Text = ProcDate.Text;
                        }
                        else
                        {
                            txtOpenDate.Text = GetOpenDate.Text;
                        }

                        txtOpenDate.Focus();
                        return;
                    }

                    if (opdate1 < opdate3)
                    {
                        BeforeDateMSG();
                        if (CtrlFunc.Text == "1")
                        {
                            txtOpenDate.Text = ProcDate.Text;
                        }
                        else
                        {
                            txtOpenDate.Text = GetOpenDate.Text;
                        }

                        txtOpenDate.Focus();
                        return;

                    }

                    txtDepositAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtOpenDate_TextChanged Problem');</script>");


                //throw ex;
            }
        }

        protected void BtnNominee_Click(object sender, EventArgs e)
        {


            Session["AccType"] = txtAccType.Text;
            Session["Cuno"] = txtCuNo.Text;
            Session["Cuname"] = lblCuName.Text;
            Session["CuType"] = lblCuType.Text;
            Session["CrNo"] = lblCuNum.Text;
            Session["NewMemNo"] = txtMemberNo.Text;
            Session["NewMemName"] = lblMemName.Text;
            Session["TrnDate"] = txtOpenDate.Text;
            Session["ProgFlag"] = "2";

            Session["CtrlFlag"] = "1";



            Session["HtxtInterestRate"] = txtInterestRate.Text;
            Session["HtxtLoanPaymentSchdule"] = txtLoanPaymentSchdule.Text;
            Session["HtxtSpInstruction"] = txtSpInstruction.Text;
            Session["HtxtDepositAmount"] = txtDepositAmount.Text;
            Session["HtxtFixedDepositAmount"] = txtFixedDepositAmount.Text;
            Session["HtxtPeriod"] = txtPeriod.Text;
            Session["HddlWithdrawalAC"] = ddlWithdrawalAC.SelectedValue;
            Session["HddlInterestCalculation"] = ddlInterestCalculation.SelectedValue;
            Session["HtxtMatrutiyDate"] = txtMatrutiyDate.Text;
            Session["HtxtMatruityAmount"] = txtMatruityAmount.Text;
            Session["HddlInterestWithdraw"] = ddlInterestWithdraw.SelectedValue;
            Session["HtxtFixedMthInt"] = txtFixedMthInt.Text;
            Session["HddlAutoRenewal"] = ddlAutoRenewal.SelectedValue;
            Session["HtxtLoanAmount"] = txtLoanAmount.Text;
            Session["HtxtNoOfInstallment"] = txtNoOfInstallment.Text;
            Session["HtxtMonthlyInstallment"] = txtMonthlyInstallment.Text;
            Session["HtxtGracePeriod"] = txtGracePeriod.Text;
            Session["HtxtLoaneeACType"] = txtLoaneeACType.Text;
            Session["HtxtLoaneeMemberNo"] = txtLoaneeMemberNo.Text;

            if (ChkContraInt.Checked == true)
            {
                Session["HChkContraInt"] = "1";
            }
            else
            {
                Session["HChkContraInt"] = "0";
            }





            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
             "click", @"<script>window.open('CSNomineeMaintenance.aspx','_blank');</script>", false);

            //Page.ClientScript.RegisterStartupScript(
            // this.GetType(), "OpenWindow", "window.open('CSNomineeMaintenance.aspx','_newtab');", true);

        }

        protected void txtNoOfInstallment_TextChanged(object sender, EventArgs e)
        {
            double instlAmt = 0;
            int no = Convert.ToInt32(txtNoOfInstallment.Text);
            double loanAmt = Convert.ToDouble(txtLoanAmount.Text);//loan amount

            instlAmt = (loanAmt / no);
            double real = instlAmt;
            double fraction = real - Math.Floor(real);
            double linstl = (no * fraction);
            double sub = (instlAmt - linstl);
            txtMonthlyInstallment.Text = Convert.ToString(string.Format("{0:0,0.00}", Math.Round(instlAmt)));
            txtLastInstallment.Text = Convert.ToString(string.Format("{0:0,0.00}", Math.Round(sub)));
            txtMonthlyInstallment.Focus();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            //Session["NewAccNo"] = hdnNewAccNo.Value;
            //Response.Redirect("A2ZERPModule.aspx");


            Session["flag"] = "1";
            Session["RTranDate"] = lblTranDate.Text;
            Session["RFuncOpt"] = lblFuncOpt.Text;
            Session["RModule"] = lblModule.Text;
            Session["RVchNo"] = lblVchNo.Text;
            Session["NewAccNo"] = string.Empty;
            Session["CtrlFlag"] = "1";

            SessionRemove();

            if (lblFuncOpt.Text == "01")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByDeposit.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "02")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanSettlement.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "03" || lblFuncOpt.Text == "04")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawal.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "05" || lblFuncOpt.Text == "06")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawalInt.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "07" || lblFuncOpt.Text == "08")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanDisbursement.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "09" || lblFuncOpt.Text == "10")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByEncashment.aspx'; self.close();</script>", false);
            }
            else if (lblFuncOpt.Text == "11" || lblFuncOpt.Text == "12")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByAdjCrDr.aspx'; self.close();</script>", false);
            }

        }


        protected void DeleteWfNomineedata()
        {
            try
            {
                string delqry = "DELETE FROM  WFCSA2ZACCNOM  WHERE UserId='" + hdnID.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.DeleteWfNomineedata Problem');</script>");


                //throw ex;
            }
        }

        protected void InsertNomineedata()
        {
            try
            {
                string qry = "SELECT CuType,CuNo FROM WFCSA2ZACCNOM WHERE UserId='" + hdnID.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    string delqry = "DELETE FROM A2ZACCNOM WHERE AccType='" + txtAccType.Text + "' and AccNo= '" + txtAccountNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "'";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                    if (rowEffect > 0)
                    {

                    }

                    string qry1 = "UPDATE WFCSA2ZACCNOM SET AccNo = '" + txtAccountNo.Text + "' WHERE UserId='" + hdnID.Text + "'";
                    int row1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));


                    string statment = "INSERT INTO A2ZACCNOM(CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomThana,NomRela, NomSharePer)SELECT CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomThana,NomRela, NomSharePer FROM WFCSA2ZACCNOM WHERE AccType='" + txtAccType.Text + "' and AccNo = '" + txtAccountNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "' ";
                    int row = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.InsertNomineedata Problem');</script>");


                //throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtAccType_TextChanged(this, EventArgs.Empty);
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtAccType.Text != string.Empty)
                {
                    if (lblflag.Text != "1")
                    {
                        clearInfo();
                    }

                    lblflag.Text = "0";

                    


                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                        txtAtyClass.Text = Converter.GetString(getDTO.AccTypeClass);
                        lblAccDepRoundingBy.Text = Converter.GetString(getDTO.AccDepRoundingBy);

                        if (txtAtyClass.Text == "5" || txtAtyClass.Text == "6" || txtAtyClass.Text == "7")
                        {
                            txtAccType.Text = string.Empty;
                            txtAccType.Focus();
                            InvalidRecords();
                            return;
                        }

                        if (lblAccTypeMode.Text == "1")
                        {
                            //lblAccTypeDesc.Text = Converter.GetString(getDTO.AccTypeDescription);
                            ddlAccType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                            lblAccFlag.Text = Converter.GetString(getDTO.AccFlag);
                            lblchk1Hide.Text = Converter.GetString(getDTO.AccessT1);
                            lblchk2Hide.Text = Converter.GetString(getDTO.AccessT2);
                            lblchk3Hide.Text = Converter.GetString(getDTO.AccessT3);

                            BtnSubmit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnNominee.Visible = false;

                            btnNewMem.Visible = true;
                            BtnCUHelp.Visible = true;
                            BtnMemHelp.Visible = true;

                        }
                        else
                        {
                            txtAccType.Text = string.Empty;
                            txtAccType.Focus();
                            InvalidRecords();
                            return;
                        }
                    }
                    else
                    {
                        Visible();
                        txtAccType.Text = string.Empty;
                        txtAccType.Focus();
                        InvalidAccType();
                        return;
                    }
                }


                Visible();

                txtCuNo.Visible = true;
                lblCuNo.Visible = true;

                txtMemberNo.Visible = true;
                lblMemberNo.Visible = true;
                txtOpenDate.Visible = true;
                lblOpendate.Visible = true;


                Int16 code = Converter.GetSmallInteger(txtAccType.Text);
                string sqlquery = "SELECT ProductCode, RecordCode,RecordFlag,FuncFlag from A2ZACCCTRL where ProductCode='" + code + "'and ControlCode='1'";
                gvHidden = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvHidden, "A2ZCSMCUS");
                A2ZACCCTRLDTO get1DTO = new A2ZACCCTRLDTO();
                for (int i = 0; i < gvHidden.Rows.Count; i++)
                {

                    Int16 rcode = Converter.GetSmallInteger(gvHidden.Rows[i].Cells[1].Text);
                    Int16 ccode = 1;
                    get1DTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                    if (get1DTO.ProductCode > 0 && get1DTO.RecordCode > 0)
                    {
                        Label4.Text = Converter.GetString(get1DTO.RecordFlag);
                        Label5.Text = Converter.GetString(get1DTO.FuncFlag);

                    }
                    else
                    {
                        Visible();
                        txtAccType.Text = string.Empty;
                        txtAccType.Focus();

                    }
                    if (Label4.Text == "1")
                    {
                        switch (rcode)
                        {

                            case 1:
                                ValidDepositAmount.Visible = true;
                                txtDepositAmount.Visible = true;
                                lblDepositAmount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtDepositAmount.ReadOnly = true;
                                }
                                break;
                            case 2:
                                ValidFixedDepositAmount.Visible = true;
                                txtFixedDepositAmount.Visible = true;
                                lblFixedDepositAmount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtFixedDepositAmount.ReadOnly = true;
                                }
                                break;
                            case 3:
                                ValidPeriod.Visible = true;
                                txtPeriod.Visible = true;
                                lblPeriod.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtPeriod.ReadOnly = true;
                                }
                                break;
                            case 4:
                                ddlWithdrawalAC.Visible = true;
                                lblWithdrawalAC.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlWithdrawalAC.Enabled = false;
                                }
                                break;
                            case 5:
                                ddlInterestCalculation.Visible = true;
                                lblInterestCalculation.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlInterestCalculation.Enabled = false;
                                }
                                break;
                            case 6:
                                txtMatrutiyDate.Visible = true;
                                lblMatruityDate.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtMatrutiyDate.ReadOnly = true;
                                }
                                break;
                            case 7:
                                txtMatruityAmount.Visible = true;
                                lblMatruityAmount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtMatruityAmount.ReadOnly = true;
                                }
                                break;
                            case 8:
                                ddlInterestWithdraw.Visible = true;
                                lblInterestWithdraw.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlInterestWithdraw.Enabled = false;
                                }
                                break;
                            case 9:
                                txtFixedMthInt.Visible = true;
                                lblFixedMthInt.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtFixedMthInt.ReadOnly = true;
                                }
                                break;
                            case 10:
                                ddlAutoRenewal.Visible = true;
                                lblAutoRenewal.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlAutoRenewal.Enabled = false;
                                }
                                break;
                            case 11:
                                txtLoanAmount.Visible = true;
                                lblLoanAmount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtLoanAmount.ReadOnly = true;
                                }
                                break;
                            case 12:
                                txtNoOfInstallment.Visible = true;
                                lblNoOfInstallment.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtNoOfInstallment.ReadOnly = true;
                                }
                                break;
                            case 13:
                                txtMonthlyInstallment.Visible = true;
                                lblMonthlyInstallment.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtMonthlyInstallment.ReadOnly = true;
                                }
                                break;
                            case 14:
                                txtLastInstallment.Visible = true;
                                lblLastInstallment.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtLastInstallment.ReadOnly = true;
                                }
                                break;
                            case 15:
                                txtInterestRate.Visible = true;
                                lblInterestRate.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtInterestRate.ReadOnly = true;
                                }
                                break;
                            case 16:
                                ChkContraInt.Visible = true;
                                lblContractInt.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ChkContraInt.Enabled = false;
                                }
                                break;
                            case 17:
                                txtGracePeriod.Visible = true;
                                lblGracePeriod.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtGracePeriod.ReadOnly = true;
                                }
                                break;
                            case 18:
                                txtLoaneeACType.Visible = true;
                                lblLoaneeACType.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtLoaneeACType.ReadOnly = true;
                                }
                                break;
                            case 19:
                                txtLoaneeMemberNo.Visible = true;
                                lblLoaneeMemberNo.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtLoaneeMemberNo.ReadOnly = true;
                                }
                                break;
                            case 20:
                                txtSpInstruction.Visible = true;
                                lblSpInstruction.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtSpInstruction.ReadOnly = true;
                                }
                                break;
                            case 21:
                                txtCorrAccType.Visible = true;
                                txtCorrAccNo.Visible = true;
                                lblCorrAccount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtCorrAccType.ReadOnly = true;
                                    txtCorrAccNo.ReadOnly = true;
                                }
                                break;
                            case 22:
                                ddlAutoTransferSaving.Visible = true;
                                lblAutoTransferSaving.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlAutoTransferSaving.Enabled = false;
                                }
                                break;
                            case 23:
                                txtOldAccNo.Visible = true;
                                lblOldAccNo.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtOldAccNo.ReadOnly = true;
                                }
                                break;
                            case 91:
                                BtnNominee.Visible = true;
                                break;

                        }
                    }

                }

                HeadFocus();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");


            }
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearInfo();
            txtAccType.Text = ddlAccType.SelectedValue;
            txtAccType_TextChanged(this, EventArgs.Empty);
        }

        protected void btnNewMem_Click(object sender, EventArgs e)
        {
            if (txtCuNo.Text != "")
            {
                Session["AccType"] = txtAccType.Text;
                Session["Cuno"] = txtCuNo.Text;
                Session["Cuname"] = lblCuName.Text;
                Session["CuType"] = lblCuType.Text;
                Session["CrNo"] = lblCuNum.Text;
                Session["TrnDate"] = txtOpenDate.Text;
                Session["flag"] = "1";
                Session["ProgCtrl"] = "0";
                Session["NFlag"] = "2";

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSInstantOpenMemberMaintenance.aspx','_blank');</script>", false);
            }

        }

        protected void BtnCUHelp_Click(object sender, EventArgs e)
        {
            Session["AccType"] = txtAccType.Text;
            Session["CtrlFlag"] = "1";
            Session["NFlag"] = "1";
            Session["ExFlag"] = "0";

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetCreditUnionNo.aspx','_blank');</script>", false);

        }

        protected void BtnMemHelp_Click(object sender, EventArgs e)
        {
            Session["AccType"] = txtAccType.Text;

            Session["Cuno"] = txtCuNo.Text;
            Session["Cuname"] = lblCuName.Text;
            Session["CuType"] = lblCuType.Text;
            Session["CrNo"] = lblCuNum.Text;

            Session["RtxtOldCuNo"] = lblOldCuNo.Text;

            Session["CtrlFlag"] = "1";
            Session["NFlag"] = "2";
            Session["ExFlag"] = "5";

            Session["RCreditUNo"] = lblCuNumber.Text;
            Session["RCuName"] = lblCuName.Text;
                   
            Session["CFlag"] = "1";
            
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }

                
    }

}
