using System;
using ATOZWEBMCUS.WebSessionStore;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountStatement : System.Web.UI.Page
    {

        //public string TmpOpenDate;
        //public string TmpAccMatureDate;
        //public string TmpAccPrevRenwlDate;
        //public Int16 TmpAccPeriod;
        //public Decimal TmpAccOrgAmt;
        //public Decimal TmpAccPrincipal;
        //public Decimal TmpAccIntRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;


                    string cflag = (string)Session["CFlag"];

                    CFlag.Text = cflag;

                    string Module = (string)Session["SModule"];

                    if (lblflag.Text == string.Empty)
                    {
                        lblModule.Text = Request.QueryString["a%b"];
                        txtAccNo.Focus();
                    }
                    else
                    {
                        lblModule.Text = Module;
                    }


                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text != "1")
                    {
                        txtCreditUNo.ReadOnly = true;
                        txtCULBMemNo.ReadOnly = true;


                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtfdate.Text = Converter.GetString(date);
                        txttdate.Text = Converter.GetString(date);
                        lblProcDate.Text = Converter.GetString(date);

                        FunctionName();

                    }
                    else
                    {
                        string RtxtAccNo = (string)Session["StxtAccNo"];
                        string RlblAccTitle = (string)Session["SlblAccTitle"];

                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string RCtrlAccType = (string)Session["SCtrlAccType"];
                        string RlblCuType = (string)Session["SlblCuType"];
                        string RlblCuNo = (string)Session["SlblCuNo"];


                        string RtxtCreditUNo = (string)Session["StxtCreditUNo"];
                        string RlblCuName = (string)Session["SlblCuName"];

                        string RtxtCULBMemNo = (string)Session["StxtCULBMemNo"];
                        string RlblMemName = (string)Session["SlblMemName"];

                        string RChkInterest = (string)Session["SChkInterest"];

                        string RlblProcDate = (string)Session["SlblProcDate"];

                        string RlblBegFinYear = (string)Session["SlblBegFinYear"];


                        string RtClass = (string)Session["StClass"];
                        string RlblTrnCode = (string)Session["SlblTrnCode"];
                        string RtOpenDt = (string)Session["StOpenDt"];
                        string RtMaturityDt = (string)Session["StMaturityDt"];

                        string RtRenewalDt = (string)Session["StRenewalDt"];
                        string RtAccLoanSancDate = (string)Session["StAccLoanSancDate"];
                        string RtAccDisbDate = (string)Session["StAccDisbDate"];
                        string RtAccPeriod = (string)Session["StAccPeriod"];
                        string RtOrgAmt = (string)Session["StOrgAmt"];
                        string RtPrincipleAmt = (string)Session["StPrincipleAmt"];
                        string RtIntRate = (string)Session["StIntRate"];
                        string RtAccBalance = (string)Session["StAccBalance"];
                        string RtAccLoanSancAmt = (string)Session["StAccLoanSancAmt"];
                        string RtAccDisbAmt = (string)Session["StAccDisbAmt"];
                        string RtAccNoInstl = (string)Session["StAccNoInstl"];
                        string RtAccLoanInstlAmt = (string)Session["StAccLoanInstlAmt"];
                        string RtAccLoanLastInstlAmt = (string)Session["StAccLoanLastInstlAmt"];


                        string Cflag = (string)Session["CFlag"];
                        CFlag.Text = Cflag;


                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;

                        txtAccNo.Text = RtxtAccNo;
                        lblAccTitle.Text = RlblAccTitle;

                        CtrlAccType.Text = RCtrlAccType;
                        lblCuType.Text = RlblCuType;
                        lblCuNo.Text = RlblCuNo;

                        txtCreditUNo.Text = RtxtCreditUNo;
                        lblCuName.Text = RlblCuName;

                        txtCULBMemNo.Text = RtxtCULBMemNo;
                        lblMemName.Text = RlblMemName;


                        tClass.Text = RtClass;
                        lblTrnCode.Text = RlblTrnCode;
                        tOpenDt.Text = RtOpenDt;
                        tMaturityDt.Text = RtMaturityDt;

                        tRenewalDt.Text = RtRenewalDt;
                        tAccLoanSancDate.Text = RtAccLoanSancDate;
                        tAccDisbDate.Text = RtAccDisbDate;
                        tAccPeriod.Text = RtAccPeriod;
                        tOrgAmt.Text = RtOrgAmt;
                        tPrincipleAmt.Text = RtPrincipleAmt;
                        tIntRate.Text = RtIntRate;
                        tAccBalance.Text = RtAccBalance;
                        tAccLoanSancAmt.Text = RtAccLoanSancAmt;
                        tAccDisbAmt.Text = RtAccDisbAmt;
                        tAccNoInstl.Text = RtAccNoInstl;
                        tAccLoanInstlAmt.Text = RtAccLoanInstlAmt;
                        tAccLoanLastInstlAmt.Text = RtAccLoanLastInstlAmt;

                        if (RChkInterest == "1")
                        {
                            ChkInterest.Checked = true;
                        }
                        else
                        {
                            ChkInterest.Checked = false;
                        }

                        lblProcDate.Text = RlblProcDate;



                    }

                    if (lblModule.Text == "04")
                    {
                        lblCULBMemNo.Text = "Staff Code";

                        lblCreditUnion.Visible = false;
                        txtCreditUNo.Visible = false;

                        ChkInterest.Visible = true;
                    }


                    A2ZERPSYSPRMDTO dto1 = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblBegFinYear.Text = Converter.GetString(dto1.PrmBegFinYear);

                    if (lblflag.Text == "1" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        GetAccInfo();
                    }

                    FunctionName();

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");


                //throw ex;
            }
        }

        protected void FunctionName()
        {
            if (lblModule.Text == "04")
            {
                lblStatementFunc.Text = "Staff Account Statement";
            }
            else
            {
                lblStatementFunc.Text = "Depositor Account Statement";
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccNo.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Transaction Code not Available');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account No. Not Available');", true);
                    return;
                }

                Session["ProgFlag"] = "1";

                Session["StxtAccNo"] = txtAccNo.Text;
                Session["SlblAccTitle"] = lblAccTitle.Text;

                Session["SCtrlAccType"] = CtrlAccType.Text;
                Session["SlblCuType"] = lblCuType.Text;
                Session["SlblCuNo"] = lblCuNo.Text;


                Session["StClass"] = tClass.Text;
                Session["SlblTrnCode"] = lblTrnCode.Text;

                Session["StOpenDt"] = tOpenDt.Text;
                Session["StMaturityDt"] = tMaturityDt.Text;
                Session["StRenewalDt"] = tRenewalDt.Text;
                Session["StAccLoanSancDate"] = tAccLoanSancDate.Text;
                Session["StAccDisbDate"] = tAccDisbDate.Text;
                Session["StAccPeriod"] = tAccPeriod.Text;
                Session["StOrgAmt"] = tOrgAmt.Text;
                Session["StPrincipleAmt"] = tPrincipleAmt.Text;
                Session["StIntRate"] = tIntRate.Text;
                Session["StAccBalance"] = tAccBalance.Text;
                Session["StAccLoanSancAmt"] = tAccLoanSancAmt.Text;
                Session["StAccDisbAmt"] = tAccDisbAmt.Text;
                Session["StAccNoInstl"] = tAccNoInstl.Text;
                Session["StAccLoanInstlAmt"] = tAccLoanInstlAmt.Text;
                Session["StAccLoanLastInstlAmt"] = tAccLoanLastInstlAmt.Text;


                Session["StxtCreditUNo"] = txtCreditUNo.Text;
                Session["SlblCuName"] = lblCuName.Text;

                Session["StxtCULBMemNo"] = txtCULBMemNo.Text;
                Session["SlblMemName"] = lblMemName.Text;

                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                if (ChkInterest.Checked == true)
                {
                    Session["SChkInterest"] = "1";
                }
                else
                {
                    Session["SChkInterest"] = "0";
                }

                Session["SlblProcDate"] = lblProcDate.Text;



                Session["SModule"] = lblModule.Text;

                Session["flag"] = "1";

                Session["NewAccNo"] = txtAccNo.Text;

                Session["CFlag"] = CFlag.Text;


                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                int fYear = fdate.Year;
                int bYear = Converter.GetInteger(lblBegFinYear.Text);

                if (fYear < bYear)
                {
                    txtfdate.Text = lblProcDate.Text;
                    txtfdate.Focus();
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Invalid From Date Input');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid From Date Input');", true);
                    return;
                }

                //var prm = new object[9];

                //prm[0] = Converter.GetSmallInteger(lblCuType.Text);
                //prm[1] = Converter.GetInteger(lblCuNo.Text);
                //prm[2] = Converter.GetInteger(txtCULBMemNo.Text);
                //prm[3] = Converter.GetInteger(lblTrnCode.Text);
                //prm[4] = Converter.GetInteger(CtrlAccType.Text);
                //prm[5] = Converter.GetLong(txtAccNo.Text);
                //prm[6] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                //prm[7] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                //prm[8] = 0;

                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAccountStatement", prm, "A2ZCSMCUS"));

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;


                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                // FOR From Date and To Date
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                //  For Account Type and Name Desc. 
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CtrlAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblAccTitle.Text);
                // For Credit Union No. and Name
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, lblCuNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblCuName.Text);
                //
                // For Member No. and Name
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, txtCULBMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblMemName.Text);

                // Open Date 
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, tOpenDt.Text);
                // Maturity Date
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, tMaturityDt.Text);
                // Last Renew Date /Annivercery Date
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, tRenewalDt.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME7, tAccLoanSancDate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME8, tAccDisbDate.Text);

                // For Account No.
                //string AccNo = Converter.GetString(txtAccNo.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME9, txtstat.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME10, tOldAccount.Text);






                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, txtAccNo.Text);

                // FOR A/C TYPE Class 
                //int AccTypeClass = Converter.GetSmallInteger(tClass.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, tClass.Text);

                // FOR Credit Union Type
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, lblCuType.Text);
                // Period
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, Converter.GetSmallInteger(tAccPeriod.Text));
                // Org.Amount
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, Converter.GetDecimal(tOrgAmt.Text));
                // Principle Amount
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO9, Converter.GetDecimal(tAccBalance.Text));




                decimal lb = Converter.GetDecimal(tAccBalance.Text);
                decimal Princ = Converter.GetDecimal(tPrincipleAmt.Text);
                decimal interest = (lb - Princ);
                if (interest < 0)
                {
                    interest = 0;
                }

                tInterest.Text = Converter.GetString(interest);




                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO10, Converter.GetDecimal(tIntRate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO11, Converter.GetDecimal(tAccLoanSancAmt.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO12, Converter.GetDecimal(tAccDisbAmt.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO13, Converter.GetSmallInteger(tAccNoInstl.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO14, Converter.GetDecimal(tAccLoanInstlAmt.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO15, Converter.GetDecimal(tAccLoanLastInstlAmt.Text));


                //============== For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, lblCuType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, lblCuNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, txtCULBMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_CODE, lblTrnCode.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCTYPE, CtrlAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, txtAccNo.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");

                if (ChkInterest.Checked && lblModule.Text != "04")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME11, tAccCertNo.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO17, Converter.GetDecimal(tAccFixedMthInt.Text));
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCSAccountStatementIntPenalty");
                }
                else if (ChkInterest.Checked && lblModule.Text == "04")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 3);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMStaffAccountStatementIntPenalty");
                }
                else
                {
                    if ((lblModule.Text == "01" || lblModule.Text == "06" || lblModule.Text == "07") && lblcls.Text != "4")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO16, Converter.GetDecimal(tInterest.Text));
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO17, Converter.GetDecimal(tAccFixedMthInt.Text));
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME11, tAccCertNo.Text);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCsAccountStatement");
                    }
                    else if ((lblModule.Text == "01" || lblModule.Text == "06" || lblModule.Text == "07") && lblcls.Text == "4")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, tLastTrnDate.Text);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME7, tUptoDate.Text);

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO11, Converter.GetDecimal(tTotalDeposit.Text));
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO12, Converter.GetDecimal(tMthDeposit.Text));
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO13, Converter.GetDecimal(tDueDepositAmt.Text));
                        
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCsAccountStatement04");
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 1);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMStaffAccountStatement");
                    }
                }
                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }




        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;
            Session["RTranDate"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;

            Session["ProgFlag"] = string.Empty;

            Session["CFlag"] = string.Empty;

            Session["StxtAccNo"] = string.Empty;
            Session["SlblAccTitle"] = string.Empty;


            Session["SCtrlAccType"] = string.Empty;
            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;


            Session["StxtCreditUNo"] = string.Empty;
            Session["SlblCuName"] = string.Empty;

            Session["StxtCULBMemNo"] = string.Empty;
            Session["SlblMemName"] = string.Empty;

            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SChkInterest"] = string.Empty;

            Session["SlblProcDate"] = string.Empty;


            Session["StClass"] = string.Empty;
            Session["SlblTrnCode"] = string.Empty;
            Session["StOpenDt"] = string.Empty;
            Session["StMaturityDt"] = string.Empty;
            Session["StRenewalDt"] = string.Empty;
            Session["StAccBalance"] = string.Empty;
            Session["StAccLoanSancDate"] = string.Empty;
            Session["StAccDisbDate"] = string.Empty;
            Session["StAccPeriod"] = string.Empty;
            Session["StOrgAmt"] = string.Empty;
            Session["StPrincipleAmt"] = string.Empty;
            Session["StIntRate"] = string.Empty;
            Session["StAccBalance"] = string.Empty;
            Session["StAccLoanSancAmt"] = string.Empty;
            Session["StAccDisbAmt"] = string.Empty;
            Session["StAccNoInstl"] = string.Empty;
            Session["StAccLoanInstlAmt"] = string.Empty;
            Session["StAccLoanLastInstlAmt"] = string.Empty;


        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void InvalidCreditUnionMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;

        }

        protected void InvalidMemberMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;

        }
        protected void InvalidAccountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
            return;

        }


        protected void InvalidAccountNoMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Does Not Exist');", true);

            return;

        }

        protected void InvalidCuNoMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;

        }

        protected void AccessDeniedMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Credit Union');", true);
            return;

        }

        protected void AccTransferedMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }
        public void GetAccInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccountNoMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    CtrlAccStatus.Text = Converter.GetString(accgetDTO.AccStatus);

                    if (CtrlAccStatus.Text == "98")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        AccTransferedMSG();
                        return;
                    }


                    Int16 AccStat = Converter.GetSmallInteger(CtrlAccStatus.Text);
                    A2ZACCSTATUSDTO get0DTO = (A2ZACCSTATUSDTO.GetInformation(AccStat));
                    if (get0DTO.AccStatusCode > 0)
                    {
                        txtstat.Text = Converter.GetString(get0DTO.AccStatusDescription);
                    }




                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);


                    txtCULBMemNo.Text = Converter.GetString(accgetDTO.MemberNo);


                    CtrlAccType.Text = Converter.GetString(accgetDTO.AccType);

                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);

                    if (lblcls.Text == "7")
                    {
                        string input2 = txtAccNo.Text;
                        string paytype = input2.Substring(12, 4);

                        int atyclass = Converter.GetInteger(lblcls.Text);
                        int paycode = Converter.GetInteger(paytype);
                        A2ZPAYTYPEDTO get1DTO = (A2ZPAYTYPEDTO.GetInformation(atyclass, paycode));
                        if (get1DTO.PayTypeCode > 0)
                        {
                            lblAccTitle.Text = Converter.GetString(get1DTO.PayTypeDescription);
                            int pcode = Converter.GetInteger(paytype);
                            A2ZTRNCODEDTO get2DTO = (A2ZTRNCODEDTO.GetInformation99(pcode));
                            if (get2DTO.TrnCode > 0)
                            {
                                lblTrnCode.Text = Converter.GetString(get2DTO.TrnCode);
                            }
                        }
                    }
                    else
                    {
                        Int16 AccType = Converter.GetSmallInteger(CtrlAccType.Text);
                        A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get3DTO.AccTypeCode > 0)
                        {
                            lblAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                        }

                        int AType = Converter.GetInteger(CtrlAccType.Text);
                        A2ZTRNCODEDTO get4DTO = (A2ZTRNCODEDTO.GetInformation01(AType));
                        if (get4DTO.TrnCode > 0)
                        {
                            lblTrnCode.Text = Converter.GetString(get4DTO.TrnCode);
                        }
                    }


                    Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        hdnCashCode.Text = Converter.GetString(get5DTO.GLCashCode);

                        if (lblModule.Text == "06" && hdnCashCode.Text != lblCashCode.Text)
                        {
                            txtAccNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtCULBMemNo.Text = string.Empty;

                            txtAccNo.Focus();

                            AccessDeniedMSG();
                            return;
                        }


                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNo.Text);
                    int MNo = Converter.GetInteger(txtCULBMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                    }


                    if (lblcls.Text == "4")
                    {

                        DateTime dt0 = Converter.GetDateTime(accgetDTO.LastTrnDate);
                        string date0 = dt0.ToString("dd/MM/yyyy");
                        tLastTrnDate.Text = date0;

                        if (tLastTrnDate.Text == "01/01/0001")
                        {
                            tLastTrnDate.Text = string.Empty;
                        }


                        DateTime dt12 = Converter.GetDateTime(accgetDTO.MatruityDate);
                        string date12 = dt12.ToString("dd/MM/yyyy");
                        tMaturityDt.Text = date12;

                        if (tMaturityDt.Text == "01/01/0001")
                        {
                            tMaturityDt.Text = string.Empty;
                        }


                        tAccPeriod.Text = Converter.GetString(accgetDTO.Period);

                        tTotalDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.TotDepositAmount));
                        tMthDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.DepositAmount));


                        DateTime TrnDate = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                        int CuNo = Converter.GetInteger(lblCuNo.Text);
                        Int16 Acctype = Converter.GetSmallInteger(CtrlAccType.Text);
                        Int64 AccNo = Converter.GetLong(txtAccNo.Text);
                        int MemNumber = Converter.GetInteger(txtCULBMemNo.Text);
                        A2ZPENSIONDEFAULTERDTO getDTO = (A2ZPENSIONDEFAULTERDTO.GetPensionDepInformation(TrnDate, CuType, CuNo, MemNumber, Acctype, AccNumber));
                        if (getDTO.CuType > 0)
                        {
                            tDueDepositAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (getDTO.DueDepositAmt)));

                            tUptoDate.Text = Converter.GetString(getDTO.UptoDate);
                        }
                    }

                }

                string qry = "SELECT Id,AccNo,AccOpenDate,AccMatureDate,AccRenwlDate,AccPeriod,AccBalance,AccOrgAmt,AccPrincipal,AccIntRate,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccAtyClass,AccOldNumber,AccCertNo,AccFixedMthInt FROM A2ZACCOUNT where AccType ='" + CtrlAccType.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and  MemNo='" + txtCULBMemNo.Text + "' and  AccNo ='" + txtAccNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    //For Report Header Parameter class wise

                    // Loan Information 

                    tAccBalance.Text = Converter.GetString(dt.Rows[0]["AccBalance"]);
                    tAccLoanSancAmt.Text = Converter.GetString(dt.Rows[0]["AccLoanSancAmt"]);
                    tAccLoanSancDate.Text = Converter.GetString(dt.Rows[0]["AccLoanSancDate"]);
                    tAccDisbAmt.Text = Converter.GetString(dt.Rows[0]["AccDisbAmt"]);
                    tAccDisbDate.Text = Converter.GetString(dt.Rows[0]["AccDisbDate"]);
                    tAccNoInstl.Text = Converter.GetString(dt.Rows[0]["AccNoInstl"]);
                    tAccLoanInstlAmt.Text = Converter.GetString(dt.Rows[0]["AccLoanInstlAmt"]);
                    tAccLoanLastInstlAmt.Text = Converter.GetString(dt.Rows[0]["AccLoanLastInstlAmt"]);
                    tOpenDt.Text = Converter.GetString(dt.Rows[0]["AccOpenDate"]);
                    tClass.Text = Converter.GetString(dt.Rows[0]["AccAtyClass"]);

                    // FOR A/C TYPE Class 
                    int AccTypeClass = Converter.GetSmallInteger(tClass.Text);
                    if (AccTypeClass == 2 || AccTypeClass == 3)
                    {

                        //  tOpenDt.Text = Converter.GetString(dt.Rows[0]["AccOpenDate"]);

                        tMaturityDt.Text = Converter.GetString(dt.Rows[0]["AccMatureDate"]);
                        tRenewalDt.Text = Converter.GetString(dt.Rows[0]["AccRenwlDate"]);

                        tAccPeriod.Text = Converter.GetString(dt.Rows[0]["AccPeriod"]);
                        tOrgAmt.Text = Converter.GetString(dt.Rows[0]["AccOrgAmt"]);
                        tPrincipleAmt.Text = Converter.GetString(dt.Rows[0]["AccPrincipal"]);
                        tOldAccount.Text = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                        tAccCertNo.Text = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                    }

                    if (AccTypeClass == 2)
                    {

                        // Int Rate
                        tIntRate.Text = Converter.GetString(dt.Rows[0]["AccIntRate"]);
                    }


                    if (AccTypeClass == 3)
                    {

                        // Monthly Benefit
                        tAccFixedMthInt.Text = Converter.GetString(dt.Rows[0]["AccFixedMthInt"]);
                    }


                    // decimal b = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);


                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }
        public void clearInfo()
        {
            txtAccNo.Text = string.Empty;

            tOpenDt.Text = string.Empty;
            tMaturityDt.Text = string.Empty;
            tRenewalDt.Text = string.Empty;
            tAccPeriod.Text = string.Empty;
            tOrgAmt.Text = string.Empty;
            tPrincipleAmt.Text = string.Empty;
            tIntRate.Text = string.Empty;
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {

            if (txtAccNo.Text != string.Empty)
            {
                GetAccInfo();
            }
        }





        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "1";
            //Session["CFlag"] = CFlag.Text;

            if (lblflag.Text == "2")
            {
                Session["CFlag"] = "0";
            }
            else
            {
                Session["CFlag"] = CFlag.Text;
            }


            if (lblModule.Text == "04")
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


    }



}
