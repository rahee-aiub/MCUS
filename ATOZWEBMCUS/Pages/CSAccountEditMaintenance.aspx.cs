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
    public partial class CSAccountEditMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["CtrlFlag"];
                    lblflag.Text = flag;

                    string Module = (string)Session["SModule"];

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    if (lblflag.Text == string.Empty)
                    {
                        CtrlPrmValue.Text = Request.QueryString["a%b"];
                        string b = CtrlPrmValue.Text;
                        CtrlModule.Text = b.Substring(0, 1);
                        CtrlFunc.Text = b.Substring(1, 1);
                        txtAccountNo.Focus();

                        txtRenewalAmt.Visible = false;
                        lblRenewalAmt.Visible = false;

                        txtTotalDep.Visible = false;
                        lblTotalDep.Visible = false;

                        lblDepositFlag.Text = "0";
                    }
                    else
                    {
                        CtrlModule.Text = Module;
                        CtrlFunc.Text = "2";

                        //string StxtAccountNo = (string)Session["NewAccNo"];

                        string StxtInterestRate = (string)Session["HtxtInterestRate"];
                        string StxtLoanPaymentSchdule = (string)Session["HtxtLoanPaymentSchdule"];
                        string StxtCertificate = (string)Session["HtxtCertificate"];
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

                        string StxtOrgAmt = (string)Session["HtxtOrgAmt"];
                        string StxtPrincipalAmt = (string)Session["HtxtPrincipalAmt"];
                        string StxtRenewalAmt = (string)Session["HtxtRenewalAmt"];
                        string StxtAnniDate = (string)Session["HtxtAnniDate"];

                        string StxtTotalDep = (string)Session["HtxtTotalDep"];
                        string SlblDepositFlag = (string)Session["HlblDepositFlag"];


                        txtAccountNo.Text = NewAccNo;

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

                        txtOrgAmt.Text = StxtOrgAmt;
                        txtPrincipalAmt.Text = StxtPrincipalAmt;
                        txtRenewalAmt.Text = StxtRenewalAmt;
                        txtAnniDate.Text = StxtAnniDate;

                        txtTotalDep.Text = StxtTotalDep;
                        lblDepositFlag.Text = SlblDepositFlag;

                        if (SChkContraInt == "1")
                        {
                            ChkContraInt.Checked = true;
                        }
                        else
                        {
                            ChkContraInt.Checked = false;
                        }

                        txtAccountNo_TextChanged(this, EventArgs.Empty);

                    }


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



                    //AccountTypeDropdown();
                    //txtAccType.Focus();

                    string sqlquery = "SELECT ProductCode, RecordCode,RecordFlag,FuncFlag from A2ZACCCTRL";
                    gvHidden = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvHidden, "A2ZCSMCUS");
                    gvHidden.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnNominee.Visible = false;

                    txtAccType.ReadOnly = true;
                    txtCuNo.ReadOnly = true;
                    txtMemberNo.ReadOnly = true;


                    txtOpenDate.ReadOnly = true;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtOpenDate.Text = date;
                    ProcDate.Text = date;

                    if (lblflag.Text == "1" && NewAccNo != "")
                    {
                        txtAccountNo.Text = NewAccNo;
                        GetInfo();
                    }
                    else
                    {
                        Visible();
                        DeleteWfNomineedata();


                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");

                //throw ex;
            }

        }

        protected void NomineeDataToWF()
        {
            if (lblflag.Text == string.Empty)
            {
                string statment = "INSERT INTO  WFCSA2ZACCNOM (CuType,CuNo,AccType,AccNo,MemNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer,UserId) SELECT CuType,CuNo,AccType,AccNo,MemNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer,'" + hdnID.Text + "' FROM A2ZACCNOM WHERE AccType='" + txtAccType.Text + "' and AccNo='" + txtAccountNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            }
            //string strQuery = "UPDATE WFCSA2ZACCNOM SET  UserId = '" + hdnID.Text + "' WHERE  AccType='" + txtAccType.Text + "' and AccNo='" + txtAccountNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNum.Text + "' and MemNo='" + txtMemberNo.Text + "'";
            //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;
            Session["RTranDate"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;

            Session["CtrlFlag"] = string.Empty;

            Session["HtxtInterestRate"] = string.Empty;
            Session["HtxtLoanPaymentSchdule"] = string.Empty;
            Session["HtxtCertificate"] = string.Empty;
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

            Session["HtxtOrgAmt"] = string.Empty;
            Session["HtxtPrincipalAmt"] = string.Empty;
            Session["HtxtRenewalAmt"] = string.Empty;
            Session["HtxtAnniDate"] = string.Empty;

            Session["AccType"] = string.Empty;
            Session["AccNo"] = string.Empty;
            Session["Cuno"] = string.Empty;
            Session["Cuname"] = string.Empty;
            Session["CuType"] = string.Empty;
            Session["CrNo"] = string.Empty;
            Session["NewMemNo"] = string.Empty;
            Session["NewMemName"] = string.Empty;
            Session["TrnDate"] = string.Empty;
            Session["ProgFlag"] = string.Empty;
        }

        protected void Visible()
        {

            txtAccountNo.Visible = true;
            lblAccountNo.Visible = true;

            lblAccType.Visible = false;
            txtAccType.Visible = false;

            txtCuNo.Visible = false;
            lblCuNumber.Visible = false;

            txtMemberNo.Visible = false;
            lblMemberNo.Visible = false;



            txtOpenDate.Visible = false;
            lblOpendate.Visible = false;

            txtInterestRate.Visible = false;
            lblInterestRate.Visible = false;
            txtLoanPaymentSchdule.Visible = false;
            lblLoanPaymentSchdule.Visible = false;
            txtCertificate.Visible = false;
            lblCertificate.Visible = false;
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

            txtOrgAmt.Visible = false;
            lblOrgAmt.Visible = false;
            txtPrincipalAmt.Visible = false;
            lblPrincipalAmt.Visible = false;


            txtDueInt.Visible = false;
            lblDueInt.Visible = false;

            txtODIntDate.Visible = false;
            lblODIntDate.Visible = false;

            //txtAnniDate.Visible = false;
            //lblAnniDate.Visible = false;


        }


        protected void HeadFocus()
        {
            txtAccountNo.Focus();
        }


        //protected void txtAccType_TextChanged(object sender, EventArgs e)
        //{


        //}



        private void clearInfo()
        {
            //txtBranchNo.Text=string.Empty;
            //txtAccType.Text=string.Empty;
            txtAccountNo.Text = string.Empty;


            lblAccTitle.Text = string.Empty;
            txtAccType.Text = string.Empty;

            txtCuNo.Text = string.Empty;
            lblCuType.Text = string.Empty;
            lblCuNum.Text = string.Empty;
            lblCuName.Text = string.Empty;

            txtMemberNo.Text = string.Empty;
            lblMemName.Text = string.Empty;
            txtInterestRate.Text = string.Empty;
            txtCertificate.Text = string.Empty;
            txtSpInstruction.Text = string.Empty;

            txtLastRenewalDt.Text = string.Empty;


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

            txtOrgAmt.Text = string.Empty;
            txtPrincipalAmt.Text = string.Empty;
            txtRenewalAmt.Text = string.Empty;
            txtAnniDate.Text = string.Empty;
        }

        protected void GetNomineeInfo()
        {
            try
            {

                if (CtrlFunc.Text == "2")
                {
                    //DeleteWfNomineedata();
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
                            txtCuNo.Text = string.Empty;
                            txtCuNo.Focus();
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
                //GetNomineeInfo();


                CtrlMsgFlag.Text = "0";

                Int64 AccNumber = Converter.GetLong(txtAccountNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));

                if (getDTO.a > 0)
                {
                    CtrlAccStatus.Text = Converter.GetString(getDTO.AccStatus);
                    txtAtyClass.Text = Converter.GetString(getDTO.AccAtyClass);

                    if (CtrlAccStatus.Text == "98")
                    {
                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        CtrlMsgFlag.Text = "1";
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        BtnNominee.Visible = false;

                        txtLastRenewalDt.Visible = false;
                        lblLastRenewalDt.Visible = false;

                        clearInfo();
                        Visible();
                        HeadFocus();

                        AccTransferedMSG();
                        return;
                    }

                    if (CtrlAccStatus.Text == "99")
                    {
                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        CtrlMsgFlag.Text = "1";
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        BtnNominee.Visible = false;

                        txtLastRenewalDt.Visible = false;
                        lblLastRenewalDt.Visible = false;

                        clearInfo();
                        Visible();
                        HeadFocus();

                        AccClosedMSG();
                        return;
                    }

                    lblCuType.Text = Converter.GetString(getDTO.CuType);
                    lblCuNum.Text = Converter.GetString(getDTO.CuNo);
                    txtCuNo.Text = (lblCuType.Text + "-" + lblCuNum.Text);
                    txtMemberNo.Text = Converter.GetString(getDTO.MemberNo);
                    txtAccType.Text = Converter.GetString(getDTO.AccType);

                    lblcls.Text = Converter.GetString(getDTO.AccAtyClass);

                    if (lblcls.Text == "2" || lblcls.Text == "3")
                    {
                        txtOpenDate.ReadOnly = false;
                    }
                    else
                    {
                        txtOpenDate.ReadOnly = true;
                    }


                    if (lblcls.Text == "7")
                    {
                        txtLastRenewalDt.Visible = false;
                        lblLastRenewalDt.Visible = false;

                        clearInfo();
                        Visible();
                        HeadFocus();

                        txtAccountNo.Text = string.Empty;
                        txtAccountNo.Focus();
                        InvalidAccRecords();
                        return;
                    }
                    else
                    {
                        Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
                        A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get3DTO.AccTypeCode > 0)
                        {
                            lblAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                            lblCorrAccType.Text = Converter.GetString(get3DTO.AccCorrType);
                        }
                    }


                    Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNum.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                        DateTime dt12 = Converter.GetDateTime(get5DTO.opendate);
                        string date12 = dt12.ToString("dd/MM/yyyy");
                        lblCuOpenDate.Text = date12;
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNum.Text);
                    int MNo = Converter.GetInteger(txtMemberNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                        txtMemType.Text = Converter.GetString(get6DTO.MemType);
                    }

                    lblAccountBal.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));

                    txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositAmount));
                    txtFixedDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.FixedDepositAmount));
                    txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.FixedMthInt));

                    CtrlOrgAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccOrgAmt));
                    txtOrgAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccOrgAmt));
                    txtPrincipalAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccPrincipal));
                    txtRenewalAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccRenwlAmt));

                    txtTotalDep.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.TotDepositAmount));



                    if (CtrlOrgAmt.Text != "00.00")
                    {
                        txtFixedDepositAmount.ReadOnly = true;
                        //txtFixedMthInt.ReadOnly = true;
                    }

                    DateTime dt = Converter.GetDateTime(getDTO.Opendate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtOpenDate.Text = date;
                    GetOpenDate.Text = date;

                    //txtOpenDate.Text = Converter.GetString( getDTO.Opendate.ToShortDateString());
                    txtPeriod.Text = Converter.GetString(getDTO.Period);
                    OrgAccPeriod.Text = Converter.GetString(getDTO.Period);
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
                        CtrlMatrutiyDate.Text = date1;
                    }


                    if (getDTO.AccRenwlDate == DateTime.MinValue)
                    {
                        CtrlRenwlDate.Text = string.Empty;
                        txtLastRenewalDt.Visible = false;
                        lblLastRenewalDt.Visible = false;
                        txtRenewalAmt.Visible = false;
                        lblRenewalAmt.Visible = false;
                    }
                    else
                    {
                        DateTime dt1 = Converter.GetDateTime(getDTO.AccRenwlDate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        CtrlRenwlDate.Text = date1;

                        txtLastRenewalDt.Text = date1;
                        txtLastRenewalDt.Visible = true;
                        lblLastRenewalDt.Visible = true;
                        txtRenewalAmt.Visible = true;
                        lblRenewalAmt.Visible = true;
                        txtOpenDate.ReadOnly = true;
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

                    if (txtAtyClass.Text == "3")
                    {
                        txtBenefitDt.Text = CtrlBenefitDate.Text;
                        lblBenefitDt.Visible = true;
                        txtBenefitDt.Visible = true;
                    }
                    else
                    {
                        lblBenefitDt.Visible = false;
                        txtBenefitDt.Visible = false;

                    }

                    CtrlNoAnni.Text = Converter.GetString(getDTO.AccNoAnni);

                    if (getDTO.AccAnniDate == DateTime.MinValue)
                    {
                        CtrlAnniDate.Text = string.Empty;
                    }
                    else
                    {
                        DateTime dt2 = Converter.GetDateTime(getDTO.AccAnniDate);
                        string date2 = dt2.ToString("dd/MM/yyyy");
                        CtrlAnniDate.Text = date2;
                    }

                    txtAnniDate.Text = CtrlAnniDate.Text;

                    if (txtAtyClass.Text == "2")
                    {
                        lblAnniDate.Visible = true;
                        txtAnniDate.Visible = true;
                    }
                    else
                    {
                        lblAnniDate.Visible = false;
                        txtAnniDate.Visible = false;
                    }

                    if (txtAtyClass.Text == "4")
                    {
                        txtTotalDep.Visible = true;
                        lblTotalDep.Visible = true;
                    }
                    else
                    {
                        txtTotalDep.Visible = false;
                        lblTotalDep.Visible = false;
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
                    txtCertificate.Text = Converter.GetString(getDTO.AccCertNo);

                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = true;
                    txtOpenDate.Focus();
                    AccFieldsEnable();

                    if (txtAtyClass.Text == "2")
                    {
                        lblInterestRate.Visible = true;
                        txtInterestRate.Visible = true;
                        txtInterestRate.ReadOnly = true;
                    }


                    txtDueInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccDueIntAmt));

                    if (getDTO.LastODIntDate == DateTime.MinValue)
                    {
                        txtODIntDate.Text = string.Empty;
                    }
                    else
                    {
                        DateTime dt2 = Converter.GetDateTime(getDTO.LastODIntDate);
                        string date2 = dt2.ToString("dd/MM/yyyy");
                        txtODIntDate.Text = date2;
                    }


                    if (txtAtyClass.Text == "5")
                    {
                        txtDueInt.Visible = true;
                        lblDueInt.Visible = true;

                        txtODIntDate.Visible = true;
                        lblODIntDate.Visible = true;
                    }


                    if (txtAtyClass.Text == "6")
                    {
                        txtDueInt.Visible = true;
                        lblDueInt.Visible = true;


                        DateTime TrnDate = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuNum.Text = Converter.GetString(getDTO.CuNo);

                        Int16 cType = Converter.GetSmallInteger(lblCuType.Text);
                        int cNo = Converter.GetInteger(lblCuNum.Text);
                        int mNo = Converter.GetInteger(txtMemberNo.Text);
                        int aType = Converter.GetInteger(txtAccType.Text);
                        Int64 aNo = Converter.GetLong(txtAccountNo.Text);

                        A2ZLOANDEFAULTERDTO get10DTO = (A2ZLOANDEFAULTERDTO.GetLoanInformation(TrnDate, cType, cNo, mNo, aType, aNo));

                        if (get10DTO.CuType > 0)
                        {

                            txtDueInt.Text = Converter.GetString(String.Format("{0:0,0.00}", get10DTO.CurrDueIntAmt));
                        }

                    }

                }
                else
                {
                    txtLastRenewalDt.Visible = false;
                    lblLastRenewalDt.Visible = false;

                    clearInfo();
                    Visible();
                    HeadFocus();

                    txtAccountNo.Text = string.Empty;
                    txtAccountNo.Focus();
                    CtrlMsgFlag.Text = "1";
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnNominee.Visible = false;
                    AccountNotExistsMSG();
                    return;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
        }


        private void AccClosedMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account already Closed');", true);
            return;



        }

        private void AccTransferedMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account already Transfered');", true);
            return;



        }
        private void InvalidRecords()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Open an Account');", true);
            return;



        }

        private void InvalidAccRecords()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Open an Account');", true);
            return;


        }

        private void AccountExistsMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Exists');", true);
            return;



        }

        private void AccountNotExistsMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not In File');", true);
            return;

        }

        private void InvalidDateMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;



        }

        private void InvalidPeriodMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Period Accept');", true);
            return;



        }

        private void BeforeDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Before CU Open Date');", true);
            return;

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

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Credit Union does not exist');", true);
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




        protected void txtAccountNo_TextChanged(object sender, EventArgs e)
        {

            try
            {
                GetInfo();
                NomineeDataToWF();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccountNo_TextChanged Problem');</script>");


            }
        }

        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "    Account Open Sucessfully Done";
            b = "Generated New Account No.";
            c = string.Format(txtAccountNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


            //----------------------------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "    Account Open Sucessfully Done";

            //string b = "Generated New Account No.";
            //string c = string.Format(txtAccountNo.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append(c);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

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
                result2 = "000";
            }
            if (input2 == "2")
            {
                result2 = "00";
            }
            if (input2 == "3")
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

            if (input1 != "4" && input2 != "4" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 != "4" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "4" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "4" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + result2 + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }
            if (input1 != "4" && input2 == "4" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "4" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + result1 + lblCuNum.Text + txtMemberNo.Text + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 == "4" && input3 != "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + txtMemberNo.Text + result3 + hdnNewAccNo.Text;
            }
            if (input1 == "4" && input2 != "4" && input3 == "4")
            {
                txtAccountNo.Text = lblAccType.Text + lblCuType.Text + lblCuNum.Text + result2 + txtMemberNo.Text + hdnNewAccNo.Text;
            }
            if (input1 == "4" && input2 == "4" && input3 == "4")
            {
                txtAccountNo.Text = txtAccType.Text + lblCuType.Text + lblCuNum.Text + txtMemberNo.Text + hdnNewAccNo.Text;
            }


        }



        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuNo.Text == string.Empty)
                {
                    txtCuNo.Focus();


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemberNo.Text == string.Empty)
                {
                    txtMemberNo.Focus();



                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                    return;
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

                int roweffect = A2ZACCOUNTDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    InsertNomineedata();
                    InsertPensionData();
                    UpdatedMSG();
                    clearInfo();
                    HeadFocus();
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;



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
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemberNo.Text == string.Empty)
                {
                    txtMemberNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                    return;
                }

                if (lblcls.Text == "2" || lblcls.Text == "3" || lblcls.Text == "4")
                {
                    if (txtPeriod.Text == string.Empty || txtPeriod.Text == "0")
                    {
                        txtPeriod.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Period');", true);
                        return;
                    }
                }

                if (lblcls.Text == "2")
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


                A2ZACCOUNTDTO UpDTO = new A2ZACCOUNTDTO();

                A2ZLOANDEFAULTERDTO UpdDTO = new A2ZLOANDEFAULTERDTO();

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
                UpDTO.AccCertNo = Converter.GetString(txtCertificate.Text);
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

                if (txtLastRenewalDt.Text != string.Empty)
                {
                    DateTime renwldate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.AccRenwlDate = renwldate;
                }
                else
                {
                    string CheckrenwlDtNull = "";
                    UpDTO.AccRenwlNullDate = CheckrenwlDtNull;
                }


                if (txtAnniDate.Text != string.Empty)
                {
                    DateTime Annidate = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.AccAnniDate = Annidate;
                }
                else
                {
                    string CheckanniDtNull = "";
                    UpDTO.AccAnniNullDate = CheckanniDtNull;
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

                UpDTO.AccOrgAmt = Converter.GetDecimal(txtOrgAmt.Text);
                UpDTO.AccPrincipal = Converter.GetDecimal(txtPrincipalAmt.Text);
                UpDTO.AccRenwlAmt = Converter.GetDecimal(txtRenewalAmt.Text);

                UpDTO.TotDepositAmount = Converter.GetDecimal(txtTotalDep.Text);

                UpDTO.lblDepFlage = Converter.GetSmallInteger(lblDepositFlag.Text);

                UpDTO.AccDueIntAmt = Converter.GetDecimal(txtDueInt.Text);

                if (txtODIntDate.Text != string.Empty)
                {
                    DateTime odintdate = DateTime.ParseExact(txtODIntDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.LastODIntDate = odintdate;
                }
                else
                {
                    string CheckodintdtNull = "";
                    UpDTO.AccODIntNullDate = CheckodintdtNull;
                }


                DateTime TrnDate = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpdDTO.TrnDate = TrnDate;

                UpdDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
                UpdDTO.CuNo = Converter.GetInteger(lblCuNum.Text);
                UpdDTO.MemNo = Converter.GetInteger(txtMemberNo.Text);

                UpdDTO.AccType = Converter.GetInteger(txtAccType.Text);
                UpdDTO.AccNo = Converter.GetLong(txtAccountNo.Text);
                UpdDTO.CurrDueIntAmt = Converter.GetDecimal(txtDueInt.Text);
                
                


                //UpDTO.AccStatus = Converter.GetInteger(91);

                int roweffect = A2ZACCOUNTDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    InsertNomineedata();

                    int roweff = A2ZLOANDEFAULTERDTO.UpdateInformation01(UpdDTO);


                    clearInfo();
                    Visible();
                    HeadFocus();

                    txtLastRenewalDt.Visible = false;
                    lblLastRenewalDt.Visible = false;

                    txtRenewalAmt.Visible = false;
                    lblRenewalAmt.Visible = false;

                    lblBenefitDt.Visible = false;
                    txtBenefitDt.Visible = false;

                    txtAnniDate.Visible = false;
                    lblAnniDate.Visible = false;

                    txtTotalDep.Visible = false;
                    lblTotalDep.Visible = false;

                    txtDueInt.Visible = false;
                    lblDueInt.Visible = false;

                    txtODIntDate.Visible = false;
                    lblODIntDate.Visible = false;

                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = false;
                    RemoveSession();




                    //      txtCuNo.Focus();


                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");


                //throw ex;
            }
        }

        protected void txtDepositAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositAmount.Text != "0")
                {
                    Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);
                    double depAmount = Converter.GetDouble(txtDepositAmount.Text);
                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

                    if (getDTO.NoRecord > 0)
                    {
                        txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));


                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;
                        txtFixedDepositAmount.Focus();


                    }
                    else
                    {
                        {
                            //String csname1 = "PopupScript";
                            //Type cstype = GetType();
                            //ClientScriptManager cs = Page.ClientScript;

                            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                            //{
                            //String cstext1 = "alert('Deposit Amount not Assign in Parameter');";
                            //cs.RegisterStartupScript(cstype, csname1, cstext1, true);


                            txtDepositAmount.Text = string.Empty;
                            txtDepositAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Deposit Amount not Assign in Parameter');", true);

                            //}
                            return;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtDepositAmount_TextChanged Problem');</script>");


                //throw ex;
            }
        }


        protected void txtFixedDepositAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFixedDepositAmount.Text != "0")
                {
                    Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(txtMemType.Text);
                    double depAmount = Converter.GetDouble(txtFixedDepositAmount.Text);
                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

                    if (getDTO.NoRecord > 0)
                    {
                        txtFixedDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));

                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;

                        DateTime Benefitdate = new DateTime();
                        Benefitdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Benefitdate = Benefitdate.AddMonths(Converter.GetSmallInteger(1));
                        DateTime dt1 = Converter.GetDateTime(Benefitdate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        CtrlBenefitDate.Text = date1;
                        txtPeriod.Focus();


                    }
                    else
                    {
                        {
                            //String csname1 = "PopupScript";
                            //Type cstype = GetType();
                            //ClientScriptManager cs = Page.ClientScript;

                            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                            //{
                            //    String cstext1 = "alert('Fixed Deposit Amount not Assign in Parameter');";
                            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);


                            txtFixedDepositAmount.Text = string.Empty;
                            txtFixedDepositAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fixed Deposit Amount not Assign in Parameter');", true);

                            //}
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtFixedDepositAmount_TextChanged Problem');</script>");


                //throw ex;
            }
        }


        protected void PeriodSlab0()
        {
            try
            {

                DateTime Matdate = new DateTime();

                if (CtrlRenwlDate.Text == string.Empty)
                {
                    Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime opdate2 = DateTime.ParseExact(CtrlRenwlDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    if (opdate2 > opdate1)
                    {
                        Matdate = DateTime.ParseExact(CtrlRenwlDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }

                }
                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                ChkMatrutiyDate.Text = date;

                if (CtrlNoAnni.Text != "0")
                {
                    DateTime opdate1 = DateTime.ParseExact(ChkMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime opdate2 = DateTime.ParseExact(CtrlAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    if (opdate1 <= opdate2)
                    {
                        txtPeriod.Text = string.Empty;
                        txtPeriod.Text = OrgAccPeriod.Text;
                        txtPeriod.Focus();
                        InvalidPeriodMSG();
                        return;
                    }
                }

                txtMatrutiyDate.Text = date;
                ddlWithdrawalAC.Focus();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PeriodSlab2 Problem');</script>");


                //throw ex;
            }
        }



        protected void PeriodSlab1()
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
                        txtDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtMatruityAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));

                        if (ChkContraInt.Checked == false)
                        {
                            txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));
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

                        if (ChkContraInt.Checked == false)
                        {
                            txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));
                        }

                        DateTime Matdate = new DateTime();

                        if (txtLastRenewalDt.Text == string.Empty)
                        {
                            Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime opdate2 = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            if (opdate2 > opdate1)
                            {
                                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }

                        }
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        ChkMatrutiyDate.Text = date;

                        if (CtrlNoAnni.Text != "0")
                        {
                            DateTime opdate1 = DateTime.ParseExact(ChkMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime opdate2 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            if (opdate1 <= opdate2)
                            {
                                txtPeriod.Text = string.Empty;
                                txtPeriod.Text = OrgAccPeriod.Text;
                                txtPeriod.Focus();
                                InvalidPeriodMSG();
                                return;
                            }
                        }

                        txtMatrutiyDate.Text = date;
                        ddlWithdrawalAC.Focus();


                        if (CtrlAnniDate.Text != string.Empty)
                        {
                            DateTime opdate6 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime opdate7 = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            if (opdate7 < opdate6)
                            {
                                txtOpenDate.Text = GetOpenDate.Text;

                                DateTime Matdate1 = new DateTime();

                                if (CtrlRenwlDate.Text == string.Empty)
                                {
                                    Matdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    Matdate1 = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                }

                                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                                DateTime dt1 = Converter.GetDateTime(Matdate);
                                string date1 = dt1.ToString("dd/MM/yyyy");
                                txtMatrutiyDate.Text = date1;

                                txtPeriod.Text = string.Empty;
                                txtPeriod.Text = OrgAccPeriod.Text;
                                txtPeriod.Focus();
                                InvalidPeriodMSG();

                                return;

                            }
                        }




                    }
                    else
                    {
                        {
                            txtPeriod.Text = string.Empty;
                            txtPeriod.Text = OrgAccPeriod.Text;

                            DateTime Matdate = new DateTime();

                            if (txtLastRenewalDt.Text == string.Empty)
                            {
                                Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }

                            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                            DateTime dt = Converter.GetDateTime(Matdate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtMatrutiyDate.Text = date;


                            if (txtAnniDate.Text != string.Empty)
                            {
                                DateTime opdate6 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                DateTime opdate7 = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                                if (opdate7 < opdate6)
                                {
                                    txtAnniDate.Text = CtrlAnniDate.Text;
                                }
                            }



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

                        //------------------------------------

                        DateTime Matdate = new DateTime();

                        if (CtrlRenwlDate.Text == string.Empty)
                        {
                            Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime opdate2 = DateTime.ParseExact(CtrlRenwlDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            if (opdate2 > opdate1)
                            {
                                Matdate = DateTime.ParseExact(CtrlRenwlDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }

                        }
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;







                        ////
                        //DateTime Matdate = new DateTime();
                        //Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        //DateTime dt = Converter.GetDateTime(Matdate);
                        //string date = dt.ToString("dd/MM/yyyy");
                        //txtMatrutiyDate.Text = date;


                        ddlWithdrawalAC.Focus();

                        CalculateBenefitDate();


                        //DateTime Benefitdate = new DateTime();
                        //Benefitdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        //Benefitdate = Benefitdate.AddMonths(Converter.GetSmallInteger(1));
                        //DateTime dt1 = Converter.GetDateTime(Benefitdate);
                        //string date1 = dt1.ToString("dd/MM/yyyy");
                        //CtrlBenefitDate.Text = date1;
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


                            txtPeriod.Text = string.Empty;
                            txtPeriod.Text = OrgAccPeriod.Text;
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

        protected void CalculateBenefitDate()
        {

            DateTime OrgOpenDt = new DateTime();
            OrgOpenDt = DateTime.ParseExact(GetOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double Oday = OrgOpenDt.Day;
            double Omth = OrgOpenDt.Month;
            double Oyear = OrgOpenDt.Year;

            DateTime InpOpenDt = new DateTime();
            InpOpenDt = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double Iday = InpOpenDt.Day;
            double Imth = InpOpenDt.Month;
            double Iyear = InpOpenDt.Year;

            double adjday = Iday - Oday;
            double adjmth = Imth - Omth;
            double adjyear = Iyear - Oyear;

            DateTime BenefitDt = new DateTime();
            BenefitDt = DateTime.ParseExact(CtrlBenefitDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double Bday = BenefitDt.Day;
            double Bmth = BenefitDt.Month;
            double Byear = BenefitDt.Year;

            double Fday = (Bday + adjday);
            double Fmth = (Bmth + adjmth);
            double Fyear = 0;

            if (Fmth == 0)
            {
                Fmth = 12;
                Fyear = (Byear - 1);
            }
            else
                if (Fmth > 12)
                {
                    Fmth = (Fmth - 12);
                    Fyear = (Byear - 1);
                }
                else
                {
                    Fyear = (Byear + adjyear);
                }

            string day = Converter.GetString(Iday);
            string mth = Converter.GetString(Fmth);
            string year = Converter.GetString(Fyear);


            string D = Converter.GetString(day).Length.ToString();
            string M = Converter.GetString(mth).Length.ToString();
            string Y = Converter.GetString(year).Length.ToString();

            string R1 = "";
            string R2 = "";
            string Bdate = "";

            if (M == "1")
            {
                R1 = "0";
            }

            if (D == "1")
            {
                R2 = "0";
            }



            if (D == "1" && M == "1")
            {
                Bdate = (R1 + mth + "/" + R2 + day + "/" + year);
            }
            else
                if (D == "1" && M != "1")
                {
                    Bdate = (mth + "/" + R2 + day + "/" + year);
                }
                else
                    if (D != "1" && M == "1")
                    {
                        Bdate = (R1 + mth + "/" + day + "/" + year);
                    }
                    else
                        if (D != "1" && M != "1")
                        {
                            Bdate = (mth + "/" + day + "/" + year);
                        }
            DateTime dt = Converter.GetDateTime(Bdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtBenefitDt.Text = date;

            CtrlBenefitDate.Text = date;

            lblBenefitDt.Visible = true;
            txtBenefitDt.Visible = true;


        }
        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            if (ChkContraInt.Checked && txtAccType.Text == "16")
            {
                PeriodSlab0();
            }
            else
            {
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
                    if (txtLastRenewalDt.Text != string.Empty)
                    {
                        txtOpenDate.Text = GetOpenDate.Text;
                        return;
                    }


                    DateTime opdate1 = new DateTime();
                    DateTime opdate2 = new DateTime();
                    DateTime opdate3 = new DateTime();
                    DateTime opdate4 = new DateTime();


                    if (txtOpenDate.Text != string.Empty)
                    {
                        opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    if (lblCuOpenDate.Text != string.Empty)
                    {
                        opdate3 = DateTime.ParseExact(lblCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    if (ProcDate.Text != string.Empty)
                    {
                        opdate2 = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }


                    if (opdate1 > opdate2)
                    {
                        InvalidDateMSG();

                        txtOpenDate.Text = GetOpenDate.Text;
                        txtBenefitDt.Text = CtrlBenefitDate.Text;

                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;

                        return;
                    }

                    if (opdate1 < opdate3)
                    {
                        BeforeDateMSG();

                        txtOpenDate.Text = GetOpenDate.Text;
                        txtBenefitDt.Text = CtrlBenefitDate.Text;

                        DateTime Matdate = new DateTime();
                        Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMatrutiyDate.Text = date;

                        return;

                    }

                    if (txtAnniDate.Text != string.Empty)
                    {
                        opdate4 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }


                    if (txtAnniDate.Text != string.Empty)
                    {
                        if (opdate1 > opdate4)
                        {
                            txtOpenDate.Text = GetOpenDate.Text;

                            DateTime Matdate = new DateTime();
                            Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                            DateTime dt = Converter.GetDateTime(Matdate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtMatrutiyDate.Text = date;

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Open Date');", true);
                            return;
                        }
                    }


                    DateTime Matdate1 = new DateTime();
                    Matdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    Matdate1 = Matdate1.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                    DateTime dt1 = Converter.GetDateTime(Matdate1);
                    string date1 = dt1.ToString("dd/MM/yyyy");
                    txtMatrutiyDate.Text = date1;


                    if (CtrlAnniDate.Text != string.Empty)
                    {
                        DateTime opdate6 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime opdate7 = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        if (opdate7 < opdate6)
                        {
                            txtOpenDate.Text = GetOpenDate.Text;

                            DateTime Matdate = new DateTime();
                            Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                            DateTime dt = Converter.GetDateTime(Matdate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtMatrutiyDate.Text = date;

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Open Date');", true);
                            return;

                        }
                    }


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

            Session["SModule"] = CtrlModule.Text;
            Session["Func"] = CtrlFunc.Text;

            Session["AccType"] = txtAccType.Text;
            Session["AccNo"] = txtAccountNo.Text;
            Session["NewAccNo"] = txtAccountNo.Text;
            Session["Cuno"] = txtCuNo.Text;
            Session["Cuname"] = lblCuName.Text;
            Session["CuType"] = lblCuType.Text;
            Session["CrNo"] = lblCuNum.Text;
            Session["NewMemNo"] = txtMemberNo.Text;
            Session["NewMemName"] = lblMemName.Text;
            Session["TrnDate"] = txtOpenDate.Text;
            Session["ProgFlag"] = "3";


            Session["HtxtInterestRate"] = txtInterestRate.Text;
            Session["HtxtLoanPaymentSchdule"] = txtLoanPaymentSchdule.Text;
            Session["HtxtCertificate"] = txtCertificate.Text;
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

            Session["HtxtOrgAmt"] = txtOrgAmt.Text;
            Session["HtxtPrincipalAmt"] = txtPrincipalAmt.Text;
            Session["HtxtRenewalAmt"] = txtRenewalAmt.Text;

            Session["HtxtTotalDep"] = txtTotalDep.Text;
            Session["HlblDepositFlag"] = lblDepositFlag.Text;


            Session["CtrlFlag"] = "1";

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








        //protected void BtnNominee_Click(object sender, EventArgs e)
        //{

        //    Session["CUType"] = lblCuType.Text;
        //    Session["CUNo"] = lblCuNum.Text;
        //    Session["TypeCode"] = txtAccType.Text;
        //    Session["MemNo"] = txtMemberNo.Text;
        //    Session["AccNo"] = txtAccountNo.Text;
        //    Page.ClientScript.RegisterStartupScript(
        //     this.GetType(), "OpenWindow", "window.open('CSNomineeMaintenance.aspx','_newtab');", true);

        //}

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
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
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


        protected void AccFieldsEnable()
        {
            try
            {

                Visible();

                txtAccType.Visible = true;
                lblAccType.Visible = true;

                txtCuNo.Visible = true;
                lblCuNumber.Visible = true;

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
                                if (Label5.Text == "1" && ChkContraInt.Checked == false)
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
                                if (CtrlFunc.Text == "2")
                                {
                                    txtCertificate.Visible = true;
                                    lblCertificate.Visible = true;
                                }
                                if (Label5.Text == "1")
                                {
                                    txtCertificate.ReadOnly = true;
                                }
                                break;
                            case 21:
                                txtSpInstruction.Visible = true;
                                lblSpInstruction.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtSpInstruction.ReadOnly = true;
                                }
                                break;
                            case 22:
                                txtCorrAccType.Visible = true;
                                txtCorrAccNo.Visible = true;
                                lblCorrAccount.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtCorrAccType.ReadOnly = true;
                                    txtCorrAccNo.ReadOnly = true;
                                }
                                break;
                            case 23:
                                ddlAutoTransferSaving.Visible = true;
                                lblAutoTransferSaving.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    ddlAutoTransferSaving.Enabled = false;
                                }
                                break;
                            case 24:
                                txtOldAccNo.Visible = true;
                                lblOldAccNo.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtOldAccNo.ReadOnly = true;
                                }
                                break;
                            case 25:
                                txtOrgAmt.Visible = true;
                                lblOrgAmt.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtOrgAmt.ReadOnly = true;
                                }
                                break;
                            case 26:
                                txtPrincipalAmt.Visible = true;
                                lblPrincipalAmt.Visible = true;
                                if (Label5.Text == "1")
                                {
                                    txtPrincipalAmt.ReadOnly = true;
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



        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SModule"] = CtrlModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "2";
            Session["CtrlFlag"] = "1";


            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('CSGetAccountNo.aspx','_blank');</script>", false);
        }

        protected void ChkContraInt_CheckedChanged(object sender, EventArgs e)
        {
            if (txtAtyClass.Text == "3")
            {
                if (ChkContraInt.Checked == true)
                {
                    txtFixedMthInt.ReadOnly = false;
                }
                else
                {
                    txtFixedMthInt.ReadOnly = true;
                }
            }

            if (txtAtyClass.Text == "2")
            {
                if (ChkContraInt.Checked == true)
                {
                    txtInterestRate.ReadOnly = false;
                }
                else
                {
                    txtInterestRate.ReadOnly = true;
                }
            }

        }

        protected void txtLastRenewalDt_TextChanged(object sender, EventArgs e)
        {
            DateTime Matdate = new DateTime();
            DateTime opdate2 = new DateTime();
            DateTime opdate3 = new DateTime();

            DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (txtLastRenewalDt.Text != string.Empty)
            {
                opdate2 = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (txtAnniDate.Text != string.Empty)
            {
                opdate3 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (opdate2 < opdate1)
            {
                txtLastRenewalDt.Text = CtrlRenwlDate.Text;

                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                txtMatrutiyDate.Text = date;

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Last Renewal Date');", true);
                return;

            }



            if (txtAnniDate.Text != string.Empty)
            {
                if (opdate2 > opdate3)
                {
                    txtLastRenewalDt.Text = CtrlRenwlDate.Text;
                    Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                    DateTime dt = Converter.GetDateTime(Matdate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtMatrutiyDate.Text = date;

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Last Renewal Date');", true);
                    return;
                }
            }
            else if (opdate2 > opdate1)
            {
                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                txtMatrutiyDate.Text = date;
                return;
            }
            else
            {
                txtLastRenewalDt.Text = CtrlRenwlDate.Text;
                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                txtMatrutiyDate.Text = date;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Last Renewal Date');", true);
                return;
            }


            DateTime opdate4 = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (opdate4 < opdate3)
            {
                txtLastRenewalDt.Text = CtrlRenwlDate.Text;
                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                txtMatrutiyDate.Text = date;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Last Renewal Date');", true);

            }
            else
            {
                Matdate = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                DateTime dt = Converter.GetDateTime(Matdate);
                string date = dt.ToString("dd/MM/yyyy");
                txtMatrutiyDate.Text = date;
                return;
            }

        }

        protected void txtTotalDep_TextChanged(object sender, EventArgs e)
        {
            lblDepositFlag.Text = "0";

            double balavail = Converter.GetDouble(lblAccountBal.Text);
            double period = Converter.GetDouble(txtPeriod.Text);
            double deposit = Converter.GetDouble(txtDepositAmount.Text);
            double Itotdep = Converter.GetDouble(txtTotalDep.Text);
            double Ctotdep = (deposit * period);

            if (Itotdep == Ctotdep)
            {
                lblDepositFlag.Text = "1";
            }
            else if (Itotdep > Ctotdep)
            {
                txtTotalDep.Text = string.Empty;
                txtTotalDep.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Excess Total Deposit Amount');", true);
                return;
            }
            else if (Itotdep > balavail)
            {
                txtTotalDep.Text = string.Empty;
                txtTotalDep.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Bal. < Total Deposit Amt.');", true);
                return;
            }


        }

        protected void txtAnniDate_TextChanged(object sender, EventArgs e)
        {

            if (CtrlRenwlDate.Text != string.Empty)
            {
                DateTime opdate1 = DateTime.ParseExact(txtLastRenewalDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime opdate2 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (opdate2 < opdate1)
                {
                    if (lblPrevAnniDate.Text != string.Empty)
                    {
                        txtAnniDate.Text = lblPrevAnniDate.Text;
                    }
                    else
                    {
                        txtAnniDate.Text = CtrlAnniDate.Text;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Anniversary Date');", true);
                    return;

                }
            }


            DateTime opdate3 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate4 = DateTime.ParseExact(txtMatrutiyDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (opdate3 > opdate4)
            {
                if (lblPrevAnniDate.Text != string.Empty)
                {
                    txtAnniDate.Text = lblPrevAnniDate.Text;
                }
                else
                {
                    txtAnniDate.Text = CtrlAnniDate.Text;
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Anniversary Date');", true);
                return;

            }

            DateTime opdate5 = DateTime.ParseExact(txtAnniDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate6 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (CtrlRenwlDate.Text == string.Empty)
            {
                if (opdate5 < opdate6)
                {
                    if (lblPrevAnniDate.Text != string.Empty)
                    {
                        txtAnniDate.Text = lblPrevAnniDate.Text;
                    }
                    else
                    {
                        txtAnniDate.Text = CtrlAnniDate.Text;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Anniversary Date');", true);
                    return;
                }
            }


            lblPrevAnniDate.Text = txtAnniDate.Text;

        }

        protected void ddlAutoTransferSaving_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAutoTransferSaving.SelectedValue == "1")
            {
                int AType = Converter.GetInteger(lblCorrAccType.Text);
                Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetInteger(lblCuNum.Text);
                int MNo = Converter.GetInteger(txtMemberNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfo(AType, CType, CNo, MNo));
                if (getDTO.a == 0)
                {
                    ddlAutoTransferSaving.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Corresponding A/C not Available');", true);
                    return;
                }
            }
        }


    }

}
