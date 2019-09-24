using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSDailyTransactionByLoanSettlement : System.Web.UI.Page
    {
        public double Principal = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;
                    //string CuType = (string)Session["CuType"];
                    //string CrNo = (string)Session["CrNo"];
                    string TranDate = (string)Session["STranDate"];
                    string FOption = (string)Session["SFuncOpt"];
                    string Module = (string)Session["SModule"];

                    string SelectAccNo = (string)Session["SCtrlSelectAccNo"];

                    string IModule = (string)Session["SInputModule"];


                    string LDate = (string)Session["LDateTitle"];

                    string VNo = (string)Session["SVchNo"];
                    lblVNo.Text = VNo;

                    string Cflag = (string)Session["CFlag"];
                    CFlag.Text = Cflag;

                    string cuNo = (string)Session["CuNo"];

                    string cuType = (string)Session["CuType"];

                    string memNo = (string)Session["MemNo"];

                    string mNo = (string)Session["SlblVchMemName"];
                    string mName = (string)Session["SlblVchMemName"];

                    string trnmode = (string)Session["SlblTransactionMode"];

                    string RtxtGLContraCode = (string)Session["StxtGLContraCode"];
                    string RddlGLContraCode = (string)Session["SddlGLContraCode"];

                    string RlblCuName = (string)Session["SlblCuName"];
                    string RlblMemName = (string)Session["SlblMemName"];
                    string RtxtCreditUNo = (string)Session["StxtCreditUNo"];

                    string RCuType = (string)Session["SCuType"];
                    string RCuNo = (string)Session["SCuNo"];
                    string RMemNo = (string)Session["SMemNo"];



                    string RlblFrontHelp = (string)Session["SlblFrontHelp"];


                    
                    BtnLV.Enabled = false;


                    Dtl2.Visible = false;

                    //DateTime dt0 = Converter.GetDateTime(TranDate);
                    //string date0 = dt0.ToString("dd/MM/yyyy");

                    lblTranDt.Text = TranDate;
                    txtTranDate.Text = TranDate;

                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));


                    if (lblflag.Text == string.Empty)
                    {
                        CtrlPrmValue.Text = Request.QueryString["a%b"];
                        string b = CtrlPrmValue.Text;
                        lblFuncOpt.Text = b.Substring(0, 2);
                        lblOrgFuncOpt.Text = lblFuncOpt.Text;
                        CtrlModule.Text = b.Substring(2, 2);

                        if (CtrlModule.Text == "19" || CtrlModule.Text == "25")
                        {
                            CtrlSelectAccNo.Text = CtrlModule.Text;
                            CtrlModule.Text = "04";
                            InputModule.Text = "06";
                        }
                        else
                        {
                            CtrlSelectAccNo.Text = string.Empty;
                        }



                        //lblTrfAcc.Visible = false;
                        //txtTrfAcc.Visible = false;


                        lblTotalAmt.Visible = false;
                        txtVchNo.Focus();
                        BtnUpdate.Visible = false;
                        BtnAdd.Visible = false;
                        BtnCancel.Visible = false;

                        
                        lblData1.ReadOnly = true;
                        lblData2.ReadOnly = true;
                        lblData3.ReadOnly = true;
                        lblData4.ReadOnly = true;
                        lblData5.ReadOnly = true;
                        lblData6.ReadOnly = true;
                        lblData7.ReadOnly = true;
                        lblData8.ReadOnly = true;
                        lblData9.ReadOnly = true;
                        lblData10.ReadOnly = true;
                        lblData11.ReadOnly = true;

                        A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                        string date1 = dt2.ToString("dd/MM/yyyy");
                        CtrlProcDate.Text = date1;
                        txtTranDate.Text = date1;
                    }
                    else
                    {
                        if (LDate != "" && LDate != null)
                        {
                            lblTranDate.Text = LDate;
                        }
                        lblFuncOpt.Text = FOption;
                        lblOrgFuncOpt.Text = lblFuncOpt.Text;
                        CtrlModule.Text = Module;

                        CtrlSelectAccNo.Text = SelectAccNo;

                        InputModule.Text = IModule;

                        txtTranDate.Text = lblTranDt.Text;
                        txtVchNo.Text = lblVNo.Text;

                        lblCuNo.Text = cuNo;
                        lblCuType.Text = cuType;
                        txtMemNo.Text = memNo;

                        lblVchMemNo.Text = mNo;
                        lblVchMemName.Text = mName;

                        lblTransactionMode.Text = trnmode;

                        lblFrontHelp.Text = RlblFrontHelp;


                        lblSkipFlag.Text = string.Empty;


                        RemoveSession();
                        txtAccNo.Focus();
                        gvDetailInfo.Visible = true;
                        gvDetail();
                        SumValue();
                    }


                    if (lblFrontHelp.Text == "1")
                    {
                        //Dtl1.Visible = false;
                        //Dtl2.Visible = true;
                        BtnSearch.Enabled = false;

                        txtCreditUNo.ReadOnly = true;
                        txtMemNo.ReadOnly = true;
                    }

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;



                    if (lblTransactionMode.Text == string.Empty)
                    {
                        BtnLV.Text = "LIVE TRANSACTION";
                        BtnLV.BackColor = Color.Blue;
                        txtTranDate.Text = CtrlProcDate.Text;
                        //txtTranDate.ReadOnly = true;
                    }
                    else
                        if (lblTransactionMode.Text == "1")
                        {
                            BtnLV.Text = "BACK VALUE TRANSACTION";
                            BtnLV.BackColor = Color.Green;
                            txtTranDate.Text = lblTranDt.Text;
                            txtTranDate.ReadOnly = false;
                        }
                        else
                            if (lblTransactionMode.Text == "2")
                            {
                                BtnLV.Text = "BACK LOG TRANSACTION";
                                BtnLV.BackColor = Color.Red;
                                txtTranDate.Text = lblTranDt.Text;
                                txtTranDate.ReadOnly = false;
                            }


                    if (txtTotalAmt.Text == string.Empty || txtTotalAmt.Text == "00.00")
                    {
                        BtnUpdate.Visible = false;
                    }
                    else
                    {
                        txtVchNo.ReadOnly = true;
                    }


                    if (lblflag.Text == "1")
                    {
                        txtAccNo.Focus();
                    }
                    else if (lblflag.Text == "2")
                    {
                        txtVchNo.Focus();
                    }


                    CtrlIntFlag.Text = "0";
                    CtrlBenefitFlag1.Text = "0";
                    CtrlBenefitFlag2.Text = "0";
                    CtrlBenefitFlag3.Text = "0";
                    CtrlBenefitFlag4.Text = "0";

                    CtrlInterestAmt.Text = "0";
                    CtrlMsgFlag.Text = "0";
                    ClearHstInfo();



                    if (CtrlModule.Text == "04")
                    {
                        //lblOldCuNo.Visible = false;
                        //txtOldCuNo.Visible = false;
                        //lblOldMemNo.Visible = false;
                        //txtOldMemNo.Visible = false;



                        //lblOldCuNo.Visible = false;
                        //txtOldCuNo.Visible = false;
                        lblCUNum.Visible = false;
                        txtCreditUNo.Visible = false;
                        chkOldSearch.Visible = false;

                        lblMemNo.Text = "Staff Code";


                    }




                    lblCuName.Visible = true;
                    lblMemName.Visible = true;



                    if (CtrlModule.Text == "06" || CtrlModule.Text == "07")
                    {

                        txtVchNo.Focus();
                    }


                    FunctionName();

                    //txtCreditUNo.ReadOnly = true;

                    //txtMemNo.ReadOnly = true;



                    txtTrnType1.ReadOnly = true;
                    txtTrnType2.ReadOnly = true;
                    txtTrnType3.ReadOnly = true;
                    txtTrnType4.ReadOnly = true;


                    VisibleFalse();





                    //txtTranDate.ReadOnly = true;



                    A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(lblID.Text), "A2ZHKMCUS");

                    if (sysobj.VPrintflag == false)
                    {
                        lblVPrintFlag.Text = "0";
                    }
                    else
                    {
                        lblVPrintFlag.Text = "1";
                    }


                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    txtGLCashCode.Text = lblCashCode.Text;

                    string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        lblBoothNo.Text = lblCashCode.Text;
                        lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                    }


                    if (lblflag.Text == "1" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        AccGetInfo();
                    }
                    else if (lblflag.Text == "1" && lblFrontHelp.Text == "1" && NewAccNo == "")
                    {

                        lblCuType.Text = RCuType;
                        lblCuNo.Text = RCuNo;
                        txtMemNo.Text = RMemNo;

                        lblCuName.Text = RlblCuName;
                        lblMemName.Text = RlblMemName;
                        txtCreditUNo.Text = RtxtCreditUNo;

                        Dtl1.Visible = false;
                        Dtl2.Visible = true;
                        BtnSearch.Enabled = false;

                        txtCreditUNo.ReadOnly = true;
                        txtMemNo.ReadOnly = true;
                        gvGroupDetail();
                    }

                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    //string date = dt.ToString("dd/MM/yyyy");
                    //CtrlProcDate.Text = date;


                    if (lblflag.Text != "1")
                    {
                        string sqlquery;
                        sqlquery = @"DELETE dbo.WF_Transaction WHERE UserID='" + lblID.Text + "'";

                        int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));

                        if (result > 0)
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }
        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;
            Session["STranDate"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SCtrlSelectAccNo"] = string.Empty;
            Session["SInputModule"] = string.Empty;

            Session["SVchNo"] = string.Empty;
            Session["LDateTitle"] = string.Empty;
            Session["CFlag"] = string.Empty;
            Session["CuNo"] = string.Empty;
            Session["CuType"] = string.Empty;
            Session["MemNo"] = string.Empty;
            Session["SControlFlag"] = string.Empty;
            Session["SlblVchMemNo"] = string.Empty;
            Session["SlblVchMemName"] = string.Empty;
            Session["SlblFrontHelp"] = string.Empty;

            Session["StxtGLContraCode"] = string.Empty;
            Session["SddlGLContraCode"] = string.Empty;

        }

        protected void FunctionName()
        {
            lblTotalAmt.ForeColor = Color.Blue;
            //lblTotalAmt.BackColor = Color.Blue;
            txtTotalAmt.ForeColor = Color.Blue;
            //txtTotalAmt.BackColor = Color.Blue;

            lblTransFunction.Text = "Cash In Transaction - LOAN SETTLEMENT";
            lblFuncTitle.Text = "DEPOSIT";
            lblTotalAmt.Text = "NET AMOUNT RECEIVED";


            lblTrnTypeTitle.Text = "CASH";


        }
        protected void VisibleFalse()
        {
            txtTrnType1.Visible = true;
            txtPayDesc1.Visible = true;
            txtAmount1.Visible = true;

            txtTrnType2.Visible = false;
            txtPayDesc2.Visible = false;
            txtAmount2.Visible = false;

            txtTrnType3.Visible = false;
            txtPayDesc3.Visible = false;
            txtAmount3.Visible = false;

            txtTrnType4.Visible = false;
            txtPayDesc4.Visible = false;
            txtAmount4.Visible = false;

        }

        protected void InitializedRecords()
        {
            VisibleFalse();
            ClearHstInfo();
            CtrlMsgFlag.Text = "0";
            CtrlIntFlag.Text = "0";
            CtrlBenefitFlag1.Text = "0";
            CtrlBenefitFlag2.Text = "0";
            CtrlBenefitFlag3.Text = "0";
            CtrlBenefitFlag4.Text = "0";
            CtrlInterestAmt.Text = "0";

            txtAccNo.Text = string.Empty;
            txtTrnType1.Text = string.Empty;
            txtTrnType2.Text = string.Empty;
            txtTrnType3.Text = string.Empty;
            txtTrnType4.Text = string.Empty;

            txtPayDesc1.Text = string.Empty;
            txtPayDesc2.Text = string.Empty;
            txtPayDesc3.Text = string.Empty;
            txtPayDesc4.Text = string.Empty;

            txtAmount1.Text = string.Empty;
            txtAmount2.Text = string.Empty;
            txtAmount3.Text = string.Empty;
            txtAmount4.Text = string.Empty;

            CtrlLogicAmt.Text = string.Empty;

            //txtGLCashCode.Text = string.Empty;

        }

        private void InvalidNonGlCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Trans. Header Record');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Non GL Account');", true);
            return;
        }
        private void InvalidGlCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Trans. Header Record');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
            return;
        }

        private void ClosedGlCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Trans. Header Record');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Closed GL Code');", true);
            return;
        }
        private void InvalidGlCashCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid GL Cash Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Cash Code');", true);
            return;
        }

        private void InvalidGLBankCode()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Bank Code');", true);
            return;
        }


        private void Validity()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Code - Does Not Exists');", true);
            return;
        }


        private void InvalidCode()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
            return;
        }

        protected void VerifyAccountSelect()
        {
            CtrlMsgFlag.Text = "0";

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                Label lblAccType = (Label)gvDetailInfo.Rows[i].Cells[1].FindControl("AccType");
                Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[2].FindControl("AccNo");


                string AcType = "";
                string AcNo = "";
                string AmNo = "";
                string BcType = "";
                string BcNo = "";
                string BmNo = "";


                string AccNo = Converter.GetString(lblAccNo.Text);

                string AccNum = Converter.GetString(txtAccNo.Text);


                if (lblAccTypeMode.Text == "1")
                {
                    AcType = AccNo.Substring(2, 1);
                    AcNo = AccNo.Substring(3, 4);
                    AmNo = AccNo.Substring(7, 5);

                    BcType = AccNum.Substring(2, 1);
                    BcNo = AccNum.Substring(3, 4);
                    BmNo = AccNum.Substring(7, 5);
                }
                else
                {
                    AcType = "0";
                    AcNo = "0";
                    AmNo = AccNo.Substring(2, 4);

                    BcType = "0";
                    BcNo = "0";
                    BmNo = AccNum.Substring(2, 4);
                }

                if (AcType != BcType || AcNo != BcNo || AmNo != BmNo)
                {
                    CtrlMsgFlag.Text = "1";
                    txtCreditUNo.Text = string.Empty;
                    txtMemNo.Text = string.Empty;
                    lblAccTitle.Text = string.Empty;
                    CFlag.Text = string.Empty;
                    InvalidAccountMSG();
                    return;
                }
            }

            string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR,TrnPayment FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count == 0)
            {
                CtrlMsgFlag.Text = "1";
                txtCreditUNo.Text = string.Empty;
                txtMemNo.Text = string.Empty;
                lblAccTitle.Text = string.Empty;
                CFlag.Text = string.Empty;
                InvalidFunctionAccountMSG();
                return;
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {

            //VerifyDuplicateRec();
            //if (CtrlMsgFlag.Text == "1")
            //{
            //    txtAccNo.Text = string.Empty;

            //    txtAccNo.Focus();
            //    return;
            //}


            Dtl2.Visible = false;
            Dtl1.Visible = true;

            AccGetInfo();

        }



        protected void AccGetInfo()
        {
            try
            {
                VerifyDuplicateRec();
                if (CtrlMsgFlag.Text == "1")
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }


                ClearHstInfo();
                VisibleFalse();

                CtrlMsgFlag.Text = "0";


                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                txtGLCashCode.Text = lblCashCode.Text;



                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);


                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));

                if (accgetDTO.a == 0)
                {
                    InvalidAccountNoMSG();

                    txtTrnType1.Text = string.Empty;
                    txtTrnType2.Text = string.Empty;
                    txtTrnType3.Text = string.Empty;
                    txtTrnType4.Text = string.Empty;

                    txtPayDesc1.Text = string.Empty;
                    txtPayDesc2.Text = string.Empty;
                    txtPayDesc3.Text = string.Empty;
                    txtPayDesc4.Text = string.Empty;

                    txtAmount1.Text = string.Empty;
                    txtAmount2.Text = string.Empty;
                    txtAmount3.Text = string.Empty;
                    txtAmount4.Text = string.Empty;

                    lblAccTitle.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    txtCreditUNo.Text = string.Empty;
                    txtMemNo.Text = string.Empty;

                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    lblAccountType.Text = Converter.GetString(accgetDTO.AccType);

                    //if (CtrlSelectAccNo.Text != string.Empty && lblAccountType.Text != CtrlSelectAccNo.Text)
                    //{
                    //    txtAccNo.Text = string.Empty;
                    //    txtAccNo.Focus();
                    //    return;
                    //}

                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);


                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);




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
                        Int16 AccType = Converter.GetSmallInteger(lblAccountType.Text);
                        A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get3DTO.AccTypeCode > 0)
                        {
                            lblAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                            lblAccTypeMode.Text = Converter.GetString(get3DTO.AccTypeMode);
                            lblAccDepRoundingBy.Text = Converter.GetString(get3DTO.AccDepRoundingBy);
                        }

                        int AType = Converter.GetInteger(lblAccountType.Text);
                        A2ZTRNCODEDTO get4DTO = (A2ZTRNCODEDTO.GetInformation01(AType));
                        if (get4DTO.TrnCode > 0)
                        {
                            lblTrnCode.Text = Converter.GetString(get4DTO.TrnCode);
                        }
                    }

                    VerifyAccountSelect();
                    if (CtrlMsgFlag.Text == "1")
                    {
                        if (lblFrontHelp.Text == "1")
                        {
                            Dtl1.Visible = false;
                            Dtl2.Visible = true;
                            BtnSearch.Enabled = false;

                            txtCreditUNo.ReadOnly = true;
                            txtMemNo.ReadOnly = true;
                            return;
                        }
                        else
                        {
                            txtAccNo.Text = string.Empty;
                            txtAccNo.Focus();
                            return;
                        }
                    }

                    Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNo.Text);
                    int MNo = Converter.GetInteger(txtMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                        lblMemType.Text = Converter.GetString(get6DTO.MemType);
                    }

                    lblVchMemNo.Text = txtMemNo.Text;
                    lblVchMemName.Text = lblMemName.Text;


                    CtrlAccStatus.Text = Converter.GetString(accgetDTO.AccStatus);
                    CtrlAccAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);
                    if (CtrlAccStatus.Text == "98" || CtrlAccStatus.Text == "99" || CtrlAccStatus.Text == "91" || (CtrlAccStatus.Text == "50" && lblFuncOpt.Text != "01" && lblFuncOpt.Text != "05"))
                    {
                        if (CtrlAccStatus.Text == "98")
                        {
                            AccTransferedMSG();
                        }
                        else if (CtrlAccStatus.Text == "99")
                        {
                            AccClosedMSG();
                        }
                        //else if (CtrlAccStatus.Text == "91")
                        //{
                        //    AccHoldMSG();
                        //}
                        else if (CtrlAccStatus.Text == "50")
                        {
                            AccLienMSG();
                        }
                        lblAccTitle.Text = string.Empty;
                        lblCuName.Text = string.Empty;
                        lblMemName.Text = string.Empty;
                        txtCreditUNo.Text = string.Empty;
                        txtMemNo.Text = string.Empty;
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();

                    }
                    else
                    {
                        CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                        double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Text);
                        CtrlLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                        double LienAmt = Converter.GetDouble(CtrlLienAmt.Text);

                        //CtrlAvailBal.Value = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - LienAmt)));

                        CtrlSancAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.LoanAmount));
                        double SancAmt = Converter.GetDouble(CtrlSancAmount.Text);

                        CtrlAvailLimit.Text = Converter.GetString(String.Format("{0:0,0.00}", (SancAmt + LadgerBalance)));

                        CtrlPrincipal.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccPrincipal));
                        Principal = Converter.GetDouble(CtrlPrincipal.Text);

                        CtrlOrgAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccOrgAmt));
                        double OrgAmt = Converter.GetDouble(CtrlOrgAmt.Text);

                        CtrlAvailInterest.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - OrgAmt)));

                        CtrlProvBenefit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccProvBalance));

                        CtrlProvAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccProvBalance));

                        CtrlIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.InterestRate));

                        lblMatureAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.MatruityAmount));

                        DateTime dt = Converter.GetDateTime(accgetDTO.AccRenwlDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        CtrlRenwlDate.Text = date;
                        CtrlRenwlAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccRenwlAmt));


                        DateTime dt0 = Converter.GetDateTime(accgetDTO.LastTrnDate);
                        string date0 = dt0.ToString("dd/MM/yyyy");
                        CtrlLastTrnDate.Text = date0;

                        if (CtrlLastTrnDate.Text == "01/01/0001")
                        {
                            CtrlLastTrnDate.Text = string.Empty;
                        }

                        CtrlTotalDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.TotDepositAmount));
                        CtrlMthDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.DepositAmount));

                        CtrlFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.FixedMthInt));

                        CtrlInstlAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.MonthlyInstallment));

                        CtrlDisbAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccDisbAmt));

                        CtrlDueIntAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccDueIntAmt));


                        DateTime dt1 = Converter.GetDateTime(accgetDTO.Opendate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        CtrlOpenDate.Text = date1;

                        DateTime dt2 = Converter.GetDateTime(accgetDTO.MatruityDate);
                        string date2 = dt2.ToString("dd/MM/yyyy");
                        CtrlMaturityDate.Text = date2;

                        CtrlPeriod.Text = Converter.GetString(accgetDTO.Period);

                        CtrlCertNo.Text = Converter.GetString(accgetDTO.AccCertNo);


                        //TrfCorrAccount();

                        QryTransControl();

                        NetTotal();



                        double AvailBenefit = Converter.GetDouble(hdnAvailBenefit.Text);
                        double AvailProv = Converter.GetDouble(CtrlProvBenefit.Text);

                        hdnAdjProvBenefit.Text = Converter.GetString(String.Format("{0:0,0.00}", (AvailProv - AvailBenefit)));


                        if (CtrlMsgFlag.Text == "1")
                        {
                            txtTrnType1.Text = string.Empty;
                            txtTrnType2.Text = string.Empty;
                            txtTrnType3.Text = string.Empty;
                            txtTrnType4.Text = string.Empty;

                            txtPayDesc1.Text = string.Empty;
                            txtPayDesc2.Text = string.Empty;
                            txtPayDesc3.Text = string.Empty;
                            txtPayDesc4.Text = string.Empty;

                            txtAmount1.Text = string.Empty;
                            txtAmount2.Text = string.Empty;
                            txtAmount3.Text = string.Empty;
                            txtAmount4.Text = string.Empty;

                            lblAccTitle.Text = string.Empty;
                            lblCuName.Text = string.Empty;
                            lblMemName.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtMemNo.Text = string.Empty;

                            txtVchNo.Text = string.Empty;

                            txtAccNo.Text = string.Empty;
                            txtAccNo.Focus();
                            return;
                        }

                        DisplayHistoryInfo();

                        if (lblDepositFlag.Text == "1")
                        {
                            txtAmount1.ReadOnly = true;
                            txtAmount2.ReadOnly = true;
                            //BtnCorrection.Visible = false;
                            CtrlMsgFlag.Text = "1";
                            UptoDepositMSG();
                        }

                        //MsgVerify();

                        //if (CtrlMsgFlag.Text == "1")
                        //{
                        //    BtnCancel_Click(this, EventArgs.Empty);
                        //    return;
                        //}

                        BtnAdd.Visible = true;
                        BtnCancel.Visible = true;

                        if (txtAmount1.Visible == true)
                        {
                            txtAmount1.Focus();
                        }
                        else
                        {
                            txtAmount2.Focus();
                        }



                    }
                }
            }


            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.AccGetInfo Problem');</script>");
                //throw ex;
            }

        }

        //protected void TrfCorrAccount()
        //{
        //    if (CtrlModule.Text == "04" && lblFuncOpt.Text == "08")
        //    {
        //        lblTrfAcc.Visible = true;
        //        txtTrfAcc.Visible = true;

        //        int memno = Converter.GetInteger(txtMemNo.Text);


        //        A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInforbyAccType(0,0,memno,25));

        //        if (accgetDTO.AccNo > 0)
        //        {
        //            txtTrfAcc.Text = Converter.GetString(accgetDTO.AccNo);
        //        }
        //    }

        //}
        protected void MsgVerify()
        {
            if (CtrlMsgFlag.Text == "1")
            {
                BtnCancel_Click(this, EventArgs.Empty);
                return;
            }
        }
        protected void UnPostValue()
        {
            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnCredit) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Text + "' AND CuNo ='" + lblCuNo.Text + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + lblAccountType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblUnPostDataCr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountCr"]));
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnDebit) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Text + "' AND CuNo ='" + lblCuNo.Text + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + lblAccountType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblUnPostDataDr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountDr"]));
            }

            double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Text);
            double AmtCredit = Converter.GetDouble(lblUnPostDataCr.Text);
            double AmtDebit = Converter.GetDouble(lblUnPostDataDr.Text);
            double LienAmt = Converter.GetDouble(CtrlLienAmt.Text);

            CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - AmtCredit)));

            CtrlAvailBal.Text = CtrlLadgerBalance.Text;

            //CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - (AmtCredit - AmtDebit))));

        }
        protected void DisplayHistoryInfo()
        {


            lblUnPostDataCr.Visible = true;
            lblUnPostDataDr.Visible = true;

            //lblUnPostDataCr.Style.Add("visibility", "none");
            //lblUnPostDataDr.Style.Add("visibility", "none");


            UnPostValue();

            lblUnPostCr.Text = "UnPost Cr. :";
            lblUnPostDr.Text = "UnPost Dr. :";


            if (CtrlAccAtyClass.Text == "6" || CtrlAccAtyClass.Text == "8")
            {

                lblBalRec.Text = "Outstanding Bal. :";
                lblBalData.Text = CtrlLadgerBalance.Text;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Text;
                lblRec2.Text = "Sanction Amount :";
                lblData2.Text = CtrlSancAmount.Text;
                lblRec3.Text = "Disbursement Amount :";
                lblData3.Text = CtrlDisbAmt.Text;
                lblRec4.Text = "Interest Rate :";
                lblData4.Text = CtrlIntRate.Text;

                lblRec5.Text = "Cal.Principal Amt.:";
                lblData5.Text = CtrlCalPrincAmt.Text;
                lblRec9.Text = "Cal.Interest Amt.:";
                lblData9.Text = CtrlCalIntAmt.Text;

                lblRec6.Text = "Prev.Due Prin.Amt.:";
                lblData6.Text = CtrlPrevDuePrincAmt.Text;
                lblRec10.Text = "Prev.Due Int.Amt.:";
                lblData10.Text = CtrlPrevDueIntAmt.Text;

                lblRec7.Text = "Paid Princ.Amt.:";
                lblData7.Text = CtrlPaidPrincAmt.Text;

                lblRec8.Text = "Paid Int.Amt.:";
                lblData8.Text = CtrlPaidIntAmt.Text;


                //lblBalData.Style.Add("visibility", "none");
                //lblData1.Style.Add("visibility", "none");
                //lblData2.Style.Add("visibility", "none");
                //lblData3.Style.Add("visibility", "none");
                //lblData4.Style.Add("visibility", "none");
                //lblData5.Style.Add("visibility", "none");
                //lblData6.Style.Add("visibility", "none");
                //lblData8.Style.Add("visibility", "none");
                //lblData9.Style.Add("visibility", "none");
                //lblData10.Style.Add("visibility", "none");
                //lblData7.Style.Add("visibility", "none");

                //lblData7.Style.Add("visibility", "hidden");

                lblBalData.Visible = true;
                lblData1.Visible = true;
                lblData2.Visible = true;
                lblData3.Visible = true;
                lblData4.Visible = true;
                lblData5.Visible = true;

                lblData6.Visible = true;
                lblData8.Visible = true;
                lblData9.Visible = true;
                lblData10.Visible = true;
                lblData7.Visible = true;

                lblData11.Visible = false;

            }
        }
        protected void QryTransControl()
        {
            try
            {

                CtrlRow.Text = "1";
                int CRow = 1;
                string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode,TrnRecDesc,TrnLogic,RecMode,ShowInt,TrnPayment FROM A2ZTRNCTRL where TrnCode='" + lblTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var ParentId = dr["Id"].ToString();
                        //    var AccType = dr["AccType"].ToString();
                        //    var FuncOpt = dr["FuncOpt"].ToString();
                        var PayType = dr["PayType"].ToString();
                        var TrnType = dr["TrnType"].ToString();
                        var TrnMode = dr["TrnMode"].ToString();
                        var TrnLogic = dr["TrnLogic"].ToString();
                        var RecMode = dr["RecMode"].ToString();
                        var TrnRecDesc = dr["TrnRecDesc"].ToString();
                        var RecShowInt = dr["ShowInt"].ToString();
                        var tPayment = dr["TrnPayment"].ToString();


                        // trntype = Converter.GetSmallInteger(TranType);
                        CtrlPayType.Text = Converter.GetString(PayType);
                        CtrlTrnType.Text = Converter.GetString(TrnType);
                        CtrlTrnMode.Text = Converter.GetString(TrnMode);
                        CtrlTrnRecDesc.Text = Converter.GetString(TrnRecDesc);
                        CtrlTrnLogic.Text = Converter.GetString(TrnLogic);
                        CtrlRecMode.Text = Converter.GetString(RecMode);
                        CtrlShowInt.Text = Converter.GetString(RecShowInt);
                        CtrlTrnPayment.Text = Converter.GetString(tPayment);

                        ReadPayType();
                        ReadTranType();
                        ReadTranMode();
                        ReadTranLogic();

                        if (CtrlMsgFlag.Text == "1")
                        {
                            return;
                        }

                        CRow = CRow + 1;
                        CtrlRow.Text = Converter.GetString(CRow);
                    }

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.QryTransControl Problem');</script>");
                //throw ex;
            }

        }
        private void ReadPayType()
        {
            try
            {
                //int TypeClass = Converter.GetInteger(lblcls.Value);
                //int PayType = Converter.GetInteger(CtrlPayType.Value);
                //A2ZPAYTYPEDTO gdto = (A2ZPAYTYPEDTO.GetInformation(TypeClass, PayType));
                //if (gdto.record > 0)
                //{
                if (CtrlRow.Text == "1")
                {
                    CtrlPayType1.Text = Converter.GetString(CtrlPayType.Text);
                    txtPayDesc1.Text = Converter.GetString(CtrlTrnRecDesc.Text);
                    CtrlRecMode1.Text = Converter.GetString(CtrlRecMode.Text);
                    CtrlTrnPayment1.Text = Converter.GetString(CtrlTrnPayment.Text);
                }
                if (CtrlRow.Text == "2")
                {
                    CtrlPayType2.Text = Converter.GetString(CtrlPayType.Text);
                    txtPayDesc2.Text = Converter.GetString(CtrlTrnRecDesc.Text);
                    CtrlRecMode2.Text = Converter.GetString(CtrlRecMode.Text);
                    CtrlTrnPayment2.Text = Converter.GetString(CtrlTrnPayment.Text);
                }
                if (CtrlRow.Text == "3")
                {
                    CtrlPayType3.Text = Converter.GetString(CtrlPayType.Text);
                    txtPayDesc3.Text = Converter.GetString(CtrlTrnRecDesc.Text);
                    CtrlRecMode3.Text = Converter.GetString(CtrlRecMode.Text);
                    CtrlTrnPayment3.Text = Converter.GetString(CtrlTrnPayment.Text);
                }
                if (CtrlRow.Text == "4")
                {
                    CtrlPayType4.Text = Converter.GetString(CtrlPayType.Text);
                    txtPayDesc4.Text = Converter.GetString(CtrlTrnRecDesc.Text);
                    CtrlRecMode4.Text = Converter.GetString(CtrlRecMode.Text);
                    CtrlTrnPayment4.Text = Converter.GetString(CtrlTrnPayment.Text);
                }
                //}

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ReadPayType Problem');</script>");
                //throw ex;
            }

        }

        private void ReadTranType()
        {
            try
            {
                if (CtrlTrnType.Text != "99")
                {
                    int TrType = Converter.GetSmallInteger(CtrlTrnType.Text);
                    A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                    if (Trndto.record > 0)
                    {
                        if (CtrlRow.Text == "1")
                        {
                            txtTrnType1.Text = Converter.GetString(CtrlTrnType.Text);
                            CtrlTrnType1.Text = Converter.GetString(txtTrnType1.Text);
                            txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
                        }
                        if (CtrlRow.Text == "2")
                        {
                            txtTrnType2.Text = Converter.GetString(CtrlTrnType.Text);
                            CtrlTrnType2.Text = Converter.GetString(txtTrnType2.Text);
                            txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
                        }
                        if (CtrlRow.Text == "3")
                        {
                            txtTrnType3.Text = Converter.GetString(CtrlTrnType.Text);
                            CtrlTrnType3.Text = Converter.GetString(txtTrnType3.Text);
                            txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
                        }
                        if (CtrlRow.Text == "4")
                        {
                            txtTrnType4.Text = Converter.GetString(CtrlTrnType.Text);
                            CtrlTrnType4.Text = Converter.GetString(txtTrnType4.Text);
                            txtTrnType4.Text = Converter.GetString(Trndto.TrnTypeDescription);
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ReadTranType Problem');</script>");
                //throw ex;
            }
        }

        private void ReadTranMode()
        {
            if (CtrlRow.Text == "1")
            {
                CtrlTrnMode1.Text = Converter.GetString(CtrlTrnMode.Text);
                if (CtrlTrnMode1.Text == "0")
                {
                    CtrlTrnContraMode1.Text = "1";
                }
                else
                {
                    CtrlTrnContraMode1.Text = "0";
                }

            }
            if (CtrlRow.Text == "2")
            {
                CtrlTrnMode2.Text = Converter.GetString(CtrlTrnMode.Text);
                if (CtrlTrnMode2.Text == "0")
                {
                    CtrlTrnContraMode2.Text = "1";
                }
                else
                {
                    CtrlTrnContraMode2.Text = "0";
                }
            }
            if (CtrlRow.Text == "3")
            {
                CtrlTrnMode3.Text = Converter.GetString(CtrlTrnMode.Text);
                if (CtrlTrnMode3.Text == "0")
                {
                    CtrlTrnContraMode3.Text = "1";
                }
                else
                {
                    CtrlTrnContraMode3.Text = "0";
                }
            }
            if (CtrlRow.Text == "4")
            {
                CtrlTrnMode4.Text = Converter.GetString(CtrlTrnMode.Text);
                if (CtrlTrnMode4.Text == "0")
                {
                    CtrlTrnContraMode4.Text = "1";
                }
                else
                {
                    CtrlTrnContraMode4.Text = "0";
                }
            }
        }
        private void ReadTranLogic()
        {
            if (CtrlTrnLogic.Text == "0")
            {
                CtrlLogicAmt.Text = "0";
                VisibleTranAmt();
            }

            if (CtrlTrnLogic.Text == "22")
            {
                NetInterestReceived();
            }
            if (CtrlTrnLogic.Text == "23")
            {
                NetLoanAmtReceived();
            }

        }
        
        private void NetInterestReceived()
        {
            try
            {

                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CuNo = Converter.GetInteger(lblCuNo.Text);
                Int16 Acctype = Converter.GetSmallInteger(lblAccountType.Text);
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetInterestReceived(CuType, CuNo, MemNumber, Acctype, AccNumber));
                if (getDTO.NoRecord > 0)
                {
                    CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlIntFlag.Text = CtrlShowInt.Text;
                    VisibleTranAmt();

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.NetInterestReceived Problem');</script>");
                //throw ex;
            }
        }

        // ----------------- 22 No. Logic ------------------------------
        private void NetLoanAmtReceived()
        {
            try
            {

                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CuNo = Converter.GetInteger(lblCuNo.Text);
                Int16 Acctype = Converter.GetSmallInteger(lblAccountType.Text);
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetLoanAmtReceived(CuType, CuNo, MemNumber, Acctype, AccNumber));
                if (getDTO.NoRecord > 0)
                {
                    CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    VisibleTranAmt();

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.NetLoanAmtReceived Problem');</script>");
                //throw ex;
            }
        }



        private void VisibleTranAmt()
        {
            if (CtrlRow.Text == "1")
            {
                CtrlValidAmt1.Text = CtrlValidAmt.Text;
                VisibleTrue();
                if (CtrlLogicAmt.Text == "0" || CtrlLogicAmt.Text == "00.00")
                {
                    txtAmount1.Text = string.Empty;
                }
                else
                {
                    txtAmount1.Text = CtrlLogicAmt.Text;
                }
            }

            if (CtrlRow.Text == "2")
            {
                CtrlValidAmt2.Text = CtrlValidAmt.Text;
                VisibleTrue();
                if (CtrlLogicAmt.Text == "0" || CtrlLogicAmt.Text == "00.00")
                {
                    txtAmount2.Text = string.Empty;
                }
                else
                {
                    txtAmount2.Text = CtrlLogicAmt.Text;
                }
            }

            if (CtrlRow.Text == "3")
            {
                CtrlValidAmt3.Text = CtrlValidAmt.Text;
                VisibleTrue();
                if (CtrlLogicAmt.Text == "0" || CtrlLogicAmt.Text == "00.00")
                {
                    txtAmount3.Text = string.Empty;
                }
                else
                {
                    txtAmount3.Text = CtrlLogicAmt.Text;
                }

            }
            if (CtrlRow.Text == "4")
            {
                CtrlValidAmt4.Text = CtrlValidAmt.Text;
                VisibleTrue();
                if (CtrlLogicAmt.Text == "0" || CtrlLogicAmt.Text == "00.00")
                {
                    txtAmount4.Text = string.Empty;
                }
                else
                {
                    txtAmount4.Text = CtrlLogicAmt.Text;
                }
            }
        }
        private void VisibleTrue()
        {
            if (CtrlRow.Text == "1")
            {
                if ((CtrlLogicAmt.Text == "0" || CtrlLogicAmt.Text == "00.00") && (CtrlTrnLogic.Text == "10" || CtrlTrnLogic.Text == "14" || CtrlTrnLogic.Text == "18" || CtrlTrnLogic.Text == "22" || CtrlTrnLogic.Text == "26" || CtrlTrnLogic.Text == "30"))
                {
                    txtTrnType1.Visible = false;
                    txtPayDesc1.Visible = false;
                    txtAmount1.Visible = false;
                }
                else
                {
                    txtTrnType1.Visible = true;
                    txtPayDesc1.Visible = true;
                    txtAmount1.Visible = true;
                    if (CtrlRecMode1.Text == "1")
                    {
                        txtAmount1.ReadOnly = true;
                    }

                }

            }
            if (CtrlRow.Text == "2")
            {
                if ((CtrlLogicAmt.Text != "0" && CtrlLogicAmt.Text != "00.00") || (CtrlTrnLogic.Text == "7" || CtrlTrnLogic.Text == "25"))
                {
                    txtTrnType2.Visible = true;
                    txtPayDesc2.Visible = true;
                    txtAmount2.Visible = true;
                    if (CtrlRecMode2.Text == "1")
                    {
                        txtAmount2.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType2.Visible = false;
                    txtPayDesc2.Visible = false;
                    txtAmount2.Visible = false;
                }

            }
            if (CtrlRow.Text == "3")
            {
                if (CtrlLogicAmt.Text != "0" && CtrlLogicAmt.Text != "00.00" || CtrlTrnLogic.Text == "12" || CtrlTrnLogic.Text == "25" || CtrlTrnLogic.Text == "16" || CtrlTrnLogic.Text == "20")
                {
                    txtTrnType3.Visible = true;
                    txtPayDesc3.Visible = true;
                    txtAmount3.Visible = true;
                    if (CtrlRecMode3.Text == "1")
                    {
                        txtAmount3.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType3.Visible = false;
                    txtPayDesc3.Visible = false;
                    txtAmount3.Visible = false;
                }

            }
            if (CtrlRow.Text == "4")
            {
                if (CtrlLogicAmt.Text != "0" && CtrlLogicAmt.Text != "00.00")
                {
                    txtTrnType4.Visible = true;
                    txtPayDesc4.Visible = true;
                    txtAmount4.Visible = true;
                    if (CtrlRecMode4.Text == "1")
                    {
                        txtAmount4.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType4.Visible = false;
                    txtPayDesc4.Visible = false;
                    txtAmount4.Visible = false;
                }

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,AccType,AccNo,PayTypeDes,Abs(GLAmount) as Amount,TrnTypeCode,TrnCode,TrnPayment FROM WF_Transaction WHERE TrnFlag !=1 AND UserID='" + lblID.Text + "'";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void VerifyDuplicateRec()
        {
            CtrlMsgFlag.Text = "0";

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                Label lblAccType = (Label)gvDetailInfo.Rows[i].Cells[1].FindControl("AccType");
                Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[2].FindControl("AccNo");
                Label lblTrnCode = (Label)gvDetailInfo.Rows[i].Cells[6].FindControl("TrnCode");
                string TrnCode = Converter.GetString(lblTrnCode.Text);
                string AccNo = Converter.GetString(lblAccNo.Text);

                if (txtAccNo.Text == AccNo)
                {

                    CtrlMsgFlag.Text = "1";

                    DuplicateAccTypeMSG();

                    if (lblFrontHelp.Text == "1")
                    {
                        Dtl1.Visible = false;
                        Dtl2.Visible = true;
                        BtnSearch.Enabled = false;

                        txtCreditUNo.ReadOnly = true;
                        txtMemNo.ReadOnly = true;
                        return;
                    }
                    else
                    {
                        txtCreditUNo.Text = string.Empty;
                        txtMemNo.Text = string.Empty;
                        lblAccTitle.Text = string.Empty;
                        return;
                    }

                }
            }
        }
        protected void SumValue()
        {
            Decimal sumCr = 0;
            //Decimal sumDr = 0;

            int totrec = gvDetailInfo.Rows.Count;
            lblTotRec.Text = Converter.GetString(totrec);

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                Label lblTrnType = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("TrnTypeCode");
                Label lblTrnPayment = (Label)gvDetailInfo.Rows[i].Cells[7].FindControl("TrnPayment");
                //Label lblTrnAmt = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("GLAmount");
                string TrnType = Converter.GetString(lblTrnType.Text);
                string TrnPayment = Converter.GetString(lblTrnPayment.Text);

                if (TrnPayment == "1")
                {
                    sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[4].Text));
                }

            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            //CtrlTrnDrTotal.Text = Convert.ToString(String.Format("{0:0,0.00}", sumDr));
        }

        protected void NetTotal()
        {
            Decimal sumCr = 0;
            //Decimal sumDr = 0;

            if (CtrlTrnPayment1.Text == "1")
            {
                decimal amt = Converter.GetDecimal(txtAmount1.Text);
                sumCr += amt;
            }
            if (CtrlTrnPayment2.Text == "1")
            {
                decimal amt = Converter.GetDecimal(txtAmount2.Text);
                sumCr += amt;

            }
            if (CtrlTrnPayment3.Text == "1")
            {
                decimal amt = Converter.GetDecimal(txtAmount3.Text);
                sumCr += amt;

            }
            if (CtrlTrnPayment4.Text == "1")
            {
                decimal amt = Converter.GetDecimal(txtAmount4.Text);
                sumCr += amt;

            }


            lbltotAmt.Visible = true;
            txttotAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            //CtrlTrnDrTotal.Text = Convert.ToString(String.Format("{0:0,0.00}", sumDr));
        }



        protected void txtTrnType1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int TrType = Converter.GetSmallInteger(txtTrnType1.Text);
                CtrlTrnType1.Text = Converter.GetString(TrType);
                A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                if (Trndto.record > 0)
                {
                    txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    txtPayDesc1.Focus();
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnType1_TextChanged Problem');</script>");
                //throw ex;
            }
        }
        protected void txtTrnType2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int TrType = Converter.GetSmallInteger(txtTrnType2.Text);
                CtrlTrnType2.Text = Converter.GetString(TrType);
                A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                if (Trndto.record > 0)
                {
                    txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    txtPayDesc2.Focus();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnType2_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtTrnType3_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int TrType = Converter.GetSmallInteger(txtTrnType3.Text);
                CtrlTrnType3.Text = Converter.GetString(TrType);
                A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                if (Trndto.record > 0)
                {
                    txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    txtPayDesc3.Focus();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnType3_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtTrnType4_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int TrType = Converter.GetSmallInteger(txtTrnType4.Text);
                CtrlTrnType4.Text = Converter.GetString(TrType);
                A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                if (Trndto.record > 0)
                {
                    txtTrnType4.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    txtPayDesc4.Focus();
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnType4_TextChanged Problem');</script>");
                //throw ex;
            }
        }

       
        protected void AddNormal()
        {
            try
            {



                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                CtrlTrnCSGL.Text = "0";
                CtrlTrnFlag.Text = "0";
                CtrlDueIntFlag.Text = "0";

                ////if (lblTransactionMode.Text == "2")
                ////{
                ////    opdate = valuedate;
                ////}


                if (CtrlSelectAccNo.Text == string.Empty)
                {
                    InputModule.Text = CtrlModule.Text;
                }

                if (CtrlRow.Text == "1")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR,TrnPayment,AccTypeMode FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType1.Text + "' and TrnType='" + CtrlTrnType1.Text + "' and TrnMode='" + CtrlTrnMode1.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                        CtrlTrnPayment.Text = Converter.GetString(dt.Rows[0]["TrnPayment"]);
                        lblATypeMode.Text = Converter.GetString(dt.Rows[0]["AccTypeMode"]);
                    }

                    if (CtrlTrnMode1.Text == "0")
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);


                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount1.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount1.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount1.Text;
                        }

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }
                    else
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);

                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount1.Text;
                        CtrlGLDebitAmt.Text = "0";


                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount1.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount1.Text;
                        }

                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }


                    CtrlDueIntAmt.Text = "0";

                    if (CtrlIntFlag.Text == "1")
                    {
                        CtrlDueIntFlag.Text = "1";
                        double a = Converter.GetDouble(txtAmount1.Text);
                        double b = Converter.GetDouble(CtrlValidAmt1.Text);
                        double c = (b - a);
                        if (c > 0)
                        {
                            CtrlDueIntAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", c));
                        }
                        else
                        {
                            CtrlDueIntAmt.Text = "0";
                        }
                    }


                    if (CtrlIntFlag.Text == "1" && (txtAmount2.Text == "00.00" || txtAmount2.Text == ""))
                    {
                        CtrlInterestAmt.Text = txtAmount1.Text;
                        CtrlShowInt.Text = "2";
                    }
                    else
                    {
                        CtrlInterestAmt.Text = "0";
                    }


                    
                    CtrlPenalAmt.Text = "0";

                    string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,TrnInterestAmt,TrnPenalAmt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,TrnDueIntAmt,ValueDate,TrnPayment,UserID,CuName,MemName,ProvAdjFlag,AccTypeMode)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType1.Text + "','" + txtTrnType1.Text + "','" + CtrlTrnMode1.Text + "','" + CtrlPayType1.Text + "','" + txtPayDesc1.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlInterestAmt.Text + "','" + CtrlPenalAmt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + CtrlDueIntAmt.Text + "','" + valuedate + "','" + CtrlTrnPayment1.Text + "','" + lblID.Text + "','" + lblCuName.Text + "','" + lblMemName.Text + "','" + ProvAdjFlag.Text + "','" + lblATypeMode.Text + "' )";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));

                    if (rowEffect1 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }


                }
                if (CtrlRow.Text == "2")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR,TrnPayment,AccTypeMode FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType2.Text + "' and TrnType='" + CtrlTrnType2.Text + "' and TrnMode='" + CtrlTrnMode2.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                        CtrlTrnPayment.Text = Converter.GetString(dt.Rows[0]["TrnPayment"]);
                        lblATypeMode.Text = Converter.GetString(dt.Rows[0]["AccTypeMode"]);
                    }
                    if (CtrlTrnMode2.Text == "0")
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }


                        CtrlGLDebitAmt.Text = txtAmount2.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount2.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount2.Text;
                        }

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }
                    else
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount2.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount2.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount2.Text;
                        }



                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }

                    if (CtrlIntFlag.Text == "1")
                    {
                        CtrlInterestAmt.Text = txtAmount1.Text;
                    }
                    else
                    {
                        CtrlInterestAmt.Text = "0";
                    }


                    
                    CtrlPenalAmt.Text = "0";
                    CtrlDueIntAmt.Text = "0";

                    if (CtrlIntFlag.Text == "1" && CtrlDueIntFlag.Text == "0")
                    {
                        double a = Converter.GetDouble(txtAmount1.Text);
                        double b = Converter.GetDouble(CtrlValidAmt1.Text);
                        double c = (b - a);
                        if (c > 0)
                        {
                            CtrlDueIntAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", c));
                        }
                        else
                        {
                            CtrlDueIntAmt.Text = "0";
                        }
                    }



                    string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,TrnInterestAmt,TrnPenalAmt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,TrnDueIntAmt,ValueDate,TrnPayment,UserID,CuName,MemName,AccTypeMode)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType2.Text + "','" + txtTrnType2.Text + "','" + CtrlTrnMode2.Text + "','" + CtrlPayType2.Text + "','" + txtPayDesc2.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlInterestAmt.Text + "','" + CtrlPenalAmt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + CtrlDueIntAmt.Text + "','" + valuedate + "','" + CtrlTrnPayment2.Text + "','" + lblID.Text + "','" + lblCuName.Text + "','" + lblMemName.Text + "','" + lblATypeMode.Text + "')";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                    if (rowEffect2 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }


                }
                if (CtrlRow.Text == "3")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR,TrnPayment,AccTypeMode FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType3.Text + "' and TrnType='" + CtrlTrnType3.Text + "' and TrnMode='" + CtrlTrnMode3.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                        CtrlTrnPayment.Text = Converter.GetString(dt.Rows[0]["TrnPayment"]);
                        lblATypeMode.Text = Converter.GetString(dt.Rows[0]["AccTypeMode"]);
                    }


                    if (CtrlTrnMode3.Text == "0")
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount3.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount3.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount3.Text;
                        }


                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }
                    else
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }
                        CtrlGLCreditAmt.Text = txtAmount3.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount3.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount3.Text;
                        }



                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }



                    CtrlInterestAmt.Text = "0";




                    string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TRnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,AccTypeMode)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType3.Text + "','" + txtTrnType3.Text + "','" + CtrlTrnMode3.Text + "','" + CtrlPayType3.Text + "','" + txtPayDesc3.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + CtrlTrnPayment3.Text + "','" + lblID.Text + "','" + lblCuName.Text + "','" + lblMemName.Text + "','" + lblATypeMode.Text + "')";

                    int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                    if (rowEffect3 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }

                }
                if (CtrlRow.Text == "4")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR,TrnPayment,AccTypeMode FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType4.Text + "' and TrnType='" + CtrlTrnType4.Text + "' and TrnMode='" + CtrlTrnMode4.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                        CtrlTrnPayment.Text = Converter.GetString(dt.Rows[0]["TrnPayment"]);
                        lblATypeMode.Text = Converter.GetString(dt.Rows[0]["AccTypeMode"]);
                    }

                    if (CtrlTrnMode4.Text == "0")
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount4.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount4.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount4.Text;
                        }


                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }
                    else
                    {
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }
                        CtrlGLCreditAmt.Text = txtAmount4.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount4.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount4.Text;
                        }



                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                    }


                   
                    string sqlquery4 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,CalProvAdjCr,CalProvAdjDr,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,AccTypeMode)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType4.Text + "','" + txtTrnType4.Text + "','" + CtrlTrnMode4.Text + "','" + CtrlPayType4.Text + "','" + txtPayDesc4.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlProvAdjCr.Text + "','" + CtrlProvAdjDr.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + CtrlTrnPayment4.Text + "','" + lblID.Text + "','" + lblCuName.Text + "','" + lblMemName.Text + "','" + lblATypeMode.Text + "')";

                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));
                    if (rowEffect4 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.AddNormal Problem');</script>");
                //throw ex;
            }
        }
        protected void AddContra()
        {
            try
            {

                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                CtrlTrnCSGL.Text = "0";
                CtrlTrnFlag.Text = "1";

                ////if (lblTransactionMode.Text == "2")
                ////{
                ////    opdate = valuedate;
                ////}


                if (CtrlSelectAccNo.Text == string.Empty)
                {
                    InputModule.Text = CtrlModule.Text;
                }

                if (CtrlRow.Text == "1")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType1.Text + "' and TrnType='" + CtrlTrnType1.Text + "' and TrnMode='" + CtrlTrnMode1.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                    }

                    if (CtrlTrnMode1.Text == "0")
                    {

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //    CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount1.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount1.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount1.Text;
                        }

                    }
                    else
                    {

                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount1.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount1.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount1.Text;
                        }

                    }

                    string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType1.Text + "','" + txtTrnType1.Text + "','" + CtrlTrnContraMode1.Text + "','" + CtrlPayType1.Text + "','" + txtPayDesc1.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + lblID.Text + "')";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));
                    if (rowEffect1 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }

                }
                if (CtrlRow.Text == "2")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType2.Text + "' and TrnType='" + CtrlTrnType2.Text + "' and TrnMode='" + CtrlTrnMode2.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                    }
                    if (CtrlTrnMode2.Text == "0")
                    {

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount2.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount2.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount2.Text;
                        }

                    }
                    else
                    {

                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount2.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount2.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount2.Text;
                        }

                    }
                    string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType2.Text + "','" + txtTrnType2.Text + "','" + CtrlTrnContraMode2.Text + "','" + CtrlPayType2.Text + "','" + txtPayDesc2.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + lblID.Text + "')";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                    if (rowEffect2 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }
                }
                if (CtrlRow.Text == "3")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType3.Text + "' and TrnType='" + CtrlTrnType3.Text + "' and TrnMode='" + CtrlTrnMode3.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                    }

                    
                    if (CtrlTrnMode3.Text == "0")
                    {

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount3.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount3.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount3.Text;
                        }

                    }
                    else
                    {

                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount3.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount3.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount3.Text;
                        }

                    }

                    string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType3.Text + "','" + txtTrnType3.Text + "','" + CtrlTrnContraMode3.Text + "','" + CtrlPayType3.Text + "','" + txtPayDesc3.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + lblID.Text + "')";

                    int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                    if (rowEffect3 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }
                }
                if (CtrlRow.Text == "4")
                {
                    string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + lblTrnCode.Text + "' and PayType='" + CtrlPayType4.Text + "' and TrnType='" + CtrlTrnType4.Text + "' and TrnMode='" + CtrlTrnMode4.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                        CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                        CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                    }

                    if (CtrlTrnMode4.Text == "0")
                    {

                        if (CtrlGLAccNoCR.Text == "0")
                        {
                            //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                            CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLCreditAmt.Text = txtAmount4.Text;
                        CtrlGLDebitAmt.Text = "0";

                        if (CtrlGLAType.Text == "1" || CtrlGLAType.Text == "5")
                        {
                            double Amount = Converter.GetDouble(txtAmount4.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount4.Text;
                        }

                    }
                    else
                    {

                        if (CtrlGLAccNoDR.Text == "0")
                        {
                            //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                            CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                        }
                        CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Text));
                        if (glObj.GLAccNo > 0)
                        {
                            CtrlGLAType.Text = Converter.GetString(glObj.GLAccType);
                        }

                        CtrlGLDebitAmt.Text = txtAmount4.Text;
                        CtrlGLCreditAmt.Text = "0";

                        if (CtrlGLAType.Text == "2" || CtrlGLAType.Text == "4")
                        {
                            double Amount = Converter.GetDouble(txtAmount4.Text);
                            CtrlGLAmount.Text = Converter.GetString(0 - Amount);
                        }
                        else
                        {
                            CtrlGLAmount.Text = txtAmount4.Text;
                        }

                    }

                    string sqlquery4 = "INSERT INTO  WF_Transaction(TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + txtVchNo.Text.Trim() + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + lblAccountType.Text + "','" + txtAccNo.Text + "','" + lblTrnCode.Text + "','" + lblFuncOpt.Text + "','" + lblTransFunction.Text + "','" + CtrlTrnType4.Text + "','" + txtTrnType4.Text + "','" + CtrlTrnContraMode4.Text + "','" + CtrlPayType4.Text + "','" + txtPayDesc4.Text + "','" + lblVchType.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + lblCashCode.Text + "','" + InputModule.Text + "','" + valuedate + "','" + lblID.Text + "')";

                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));
                    if (rowEffect4 > 0)
                    {
                        //gvDetail();
                        //SumValue();
                    }
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.AddContra Problem');</script>");
                //throw ex;
            }
        }

        protected void ClearHstInfo()
        {
            lblRec1.Text = string.Empty;
            lblRec2.Text = string.Empty;
            lblRec3.Text = string.Empty;
            lblRec4.Text = string.Empty;
            lblRec5.Text = string.Empty;
            lblRec6.Text = string.Empty;
            lblRec7.Text = string.Empty;
            lblRec8.Text = string.Empty;
            lblRec9.Text = string.Empty;
            lblRec10.Text = string.Empty;
            lblRec11.Text = string.Empty;

            lblBalRec.Text = string.Empty;

            lblData1.Text = string.Empty;
            lblData2.Text = string.Empty;
            lblData3.Text = string.Empty;
            lblData4.Text = string.Empty;
            lblData5.Text = string.Empty;
            lblData6.Text = string.Empty;
            lblData7.Text = string.Empty;
            lblData8.Text = string.Empty;
            lblData9.Text = string.Empty;
            lblData10.Text = string.Empty;
            lblData11.Text = string.Empty;

            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            lblUnPostCr.Text = string.Empty;
            lblUnPostDr.Text = string.Empty;




            //lblData1.Style.Add("visibility", "hidden");
            //lblData2.Style.Add("visibility", "hidden");
            //lblData3.Style.Add("visibility", "hidden");
            //lblData4.Style.Add("visibility", "hidden");
            //lblData5.Style.Add("visibility", "hidden");
            //lblData6.Style.Add("visibility", "hidden");
            //lblData7.Style.Add("visibility", "hidden");
            //lblData8.Style.Add("visibility", "hidden");
            //lblData9.Style.Add("visibility", "hidden");
            //lblData10.Style.Add("visibility", "hidden");
            //lblData11.Style.Add("visibility", "hidden");

            //lblBalData.Style.Add("visibility", "hidden");
            //lblUnPostDataCr.Style.Add("visibility", "hidden");
            //lblUnPostDataDr.Style.Add("visibility", "hidden");


            lblData1.Visible = false;
            lblData2.Visible = false;
            lblData3.Visible = false;
            lblData4.Visible = false;
            lblData5.Visible = false;
            lblData6.Visible = false;
            lblData7.Visible = false;
            lblData8.Visible = false;
            lblData9.Visible = false;
            lblData10.Visible = false;
            lblData11.Visible = false;

            lblBalData.Visible = false;
            lblUnPostDataCr.Visible = false;
            lblUnPostDataDr.Visible = false;

        }
        protected void ClearInfo()
        {
            lblMemName.Text = string.Empty;

            txtVchNo.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;

            lblCuName.Text = string.Empty;

            txtMemNo.Text = string.Empty;

            lblMemName.Text = string.Empty;

            //txtVoucherNo.Text = string.Empty;

            txtAccNo.Text = string.Empty;


            gvDetailInfo.Visible = false;

            ClearHstInfo();

            txtTotalAmt.Text = string.Empty;


        }


        protected void CrearScreen()
        {
            lblMemName.Text = string.Empty;

            txtVchNo.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;


            txtMemNo.Text = string.Empty;

            //txtVoucherNo.Text = string.Empty;

            txtAccNo.Text = string.Empty;


        }


        //   --------- DISPLAY MASSAGE SCREEN ----------------------- 

        protected void InvalidAccountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Duplicate Account Number');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Number');", true);
            return;

        }

        protected void InvalidFunctionAccountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Duplicate Account Number');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Function Account');", true);
            return;

        }

        protected void DuplicateAccTypeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Duplicate Account Number');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Duplicate Account Number');", true);
            return;

        }

        protected void AccessAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Insufficent Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);
            return;

        }

        protected void InvalidAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Amount Accept');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Amount Accept');", true);
            return;

        }

        protected void ExcessAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Amount Accept');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Excess Deposit Amount');", true);
            return;

        }
        protected void AccODWithdrawalMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Withdrawal Access Over Limit');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Withdrawal Access Over Limit');", true);

            return;

        }
        protected void AccFixedDepositMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not A New Account');", true);
            return;

        }

        protected void AccDisbMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Already Disbursed');", true);
            return;

        }
        protected void InvalidCuNoMSG()
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

        protected void TransferCuNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Transfered Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transfered Credit Union No.');", true);
            return;

        }

        protected void InvalidMemberMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Member No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Member No.');", true);
            return;

        }

        protected void InvalidTranCodeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Transaction Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Transaction Code');", true);
            return;

        }

        protected void InvalidAccountNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account Does Not Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Does Not Exist');", true);

            return;

        }
        protected void IntWithdMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('No Accrued Interest');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Accrued Interest');", true);
            return;

        }

        protected void ExpIntWithdMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('No Accrued Interest');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Expired Interest Withdrwal');", true);
            return;

        }
        protected void BenefitWithdMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('No Benefit Interest');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Benefit Interest');", true);
            return;

        }


        protected void ExcessBenefitWithdMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('No Benefit Interest');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Excess Benefit Interest');", true);
            return;

        }
        protected void UpdatedSucessfullyMSG()
        {
            string Msg = "";

            string a = "";
            string d = "";
            string e = "";

            if (CtrlProcStat.Text == "0")
            {
                a = "    TRANSACTION SUCESSFULLY DONE";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "    TRANSACTION INPUT DONE";
            }

            string b = "Generated New Voucher No.";
            string c = string.Format(CtrlVoucherNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


        }





        //protected void UpdatedSucessfullyMSG()
        //{
        //    string a = "";
        //    string d = "";
        //    string e = "";

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    if (CtrlProcStat.Value == "0")
        //    {
        //        a = "    TRANSACTION SUCESSFULLY DONE";
        //    }
        //    if (CtrlProcStat.Value == "1")
        //    {
        //        a = "    TRANSACTION INPUT DONE";
        //    }
        //    string b = "Generated New Voucher No.";
        //    string c = string.Format(CtrlVoucherNo.Value);

        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload=function(){");
        //    sb.Append("alert('");
        //    sb.Append(a);
        //    sb.Append("\\n");
        //    sb.Append("\\n");
        //    sb.Append(b);
        //    sb.Append(c);
        //    sb.Append("\\n");
        //    sb.Append("\\n");
        //    sb.Append(d);
        //    sb.Append(e);


        //    sb.Append("')};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        //}

        protected void UpdatedInputMSG()
        {
            string a = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (CtrlProcStat.Text == "0")
            {
                a = "    Transaction Sucessfully Done";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "    Transaction Input Done";
            }
            string b = "Generated New Voucher No.";
            string c = string.Format(CtrlVoucherNo.Text);

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

        protected void RecordsAddedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    //String cstext1 = "confirm('Records Already Added');";
            //    String cstext1 = "alert('Records Already Added');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Records Already Added');", true);
            return;

        }

        protected void DuplicateVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher Already Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;

        }
        protected void VchInputMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
            return;
        }

        protected void VchAmtMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Amount');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Amount');", true);
            return;
        }

        protected void AccTransferedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }


        protected void AccHoldMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Hold for Verify');", true);
            return;
        }
        protected void AccClosedMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Closed');", true);
            return;
        }
        protected void AccLienMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Lien');", true);
            return;
        }

        protected void UptoDepositMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upto Deposit Made');", true);
            return;
        }

        //protected void UptoDepositMSG()
        //{
        //    Page.ClientScript.RegisterStartupScript(typeof(Page), "alertMessage",
        //            "<script type='text/javascript'>alert('Upto Deposit Made');</script>");
        //}


        //------------------------- END DISPLAY MASSAGE SCREEN ----------------------


        protected void txtAmount1_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Math.Abs(Converter.GetDouble(txtAmount1.Text));
            txtAmount1.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            CtrlValidAmt.Text = CtrlValidAmt1.Text;


            if (CtrlRecMode2.Text == "1" && CtrlMainAmt.Text != "")
            {
                double a = Converter.GetDouble(txtAmount1.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Text);
                double c = Converter.GetDouble(CtrlMainAmt.Text);
                txtAmount2.Text = Converter.GetString(String.Format("{0:0,0.00}", c - (b - a)));
            }



            if (CtrlRecMode3.Text == "1" && CtrlMainAmt.Text != "")
            {
                double a = Converter.GetDouble(txtAmount1.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Text);
                double c = Converter.GetDouble(CtrlMainAmt.Text);
                txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", a));
            }


            if (CtrlRecMode4.Text == "1" && CtrlMainAmt.Text != "" && lblcls.Text == "4")
            {
                double a = Converter.GetDouble(txtAmount1.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Text);
                //double c = Converter.GetDouble(CtrlMainAmt.Text);
                double c = Converter.GetDouble(txtAmount4.Text);
                txtAmount4.Text = Converter.GetString(String.Format("{0:0,0.00}", c - (b - a)));
            }




            NetTotal();
            BtnAdd.Focus();
        }


        protected void txtAmount2_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Math.Abs(Converter.GetDouble(txtAmount2.Text));
            txtAmount2.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            CtrlValidAmt.Text = CtrlValidAmt2.Text;
            if (CtrlRecMode4.Text == "1" && CtrlMainAmt.Text != "")
            {
                double a = Converter.GetDouble(txtAmount2.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Text);
                double c = Converter.GetDouble(CtrlMainAmt.Text);
                txtAmount4.Text = Converter.GetString(String.Format("{0:0,0.00}", c + (b - a)));
            }
            NetTotal();
            BtnAdd.Focus();

        }


        protected void txtAmount3_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Math.Abs(Converter.GetDouble(txtAmount3.Text));
            txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            CtrlValidAmt.Text = CtrlValidAmt3.Text;

            NetTotal();
            BtnAdd.Focus();

        }

        protected void txtAmount4_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Math.Abs(Converter.GetDouble(txtAmount4.Text));
            txtAmount4.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            NetTotal();
            BtnAdd.Focus();

        }

        protected void TrnVchDeplicate()
        {
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "' and TrnDate ='" + opdate + "'";
            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                DuplicateVchMSG();
                return;
            }
        }






        //  ------------- ADD TRANSACTIONS ------------------------------- 
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                TrnVchDeplicate();

                if (txtCreditUNo.Text == string.Empty)
                {
                    txtCreditUNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemNo.Text == string.Empty)
                {
                    txtMemNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                    return;
                }

                if (txtAccNo.Text == string.Empty)
                {
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account No.');", true);
                    return;
                }

                if (txtVchNo.Text.Trim() == string.Empty)
                {
                    txtVchNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }




                if ((txtAmount1.Text == "" || txtAmount1.Text == "00.00") && (txtAmount2.Text == "" || txtAmount2.Text == "00.00") &&
                    (txtAmount3.Text == "" || txtAmount3.Text == "00.00") && (txtAmount4.Text == "" || txtAmount4.Text == "00.00"))
                {
                    VchAmtMSG();
                    return;
                }

                if (lblTransactionMode.Text != string.Empty && txtTranDate.Text == CtrlProcDate.Text)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Date Accept');", true);
                    return;
                }



                lblVchType.Text = "V";




                gvDetailInfo.Visible = true;


                lblCuName.Text = lblCuName.Text.Trim().Replace("'", "''");
                lblMemName.Text = lblMemName.Text.Trim().Replace("'", "''");


                int CRow = 0;
                string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode FROM A2ZTRNCTRL where TrnCode='" + lblTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CRow = CRow + 1;
                        CtrlRow.Text = Converter.GetString(CRow);

                        if (CtrlRow.Text == "1")
                        {
                            if (txtAmount1.Text != "" && txtAmount1.Text != "00.00")
                            {
                                AddNormal();
                                AddContra();
                            }
                        }

                        if (CtrlRow.Text == "2")
                        {
                            if (txtAmount2.Text != "" && txtAmount2.Text != "00.00")
                            {
                                AddNormal();
                                AddContra();
                            }
                        }

                        if (CtrlRow.Text == "3")
                        {
                            if (txtAmount3.Text != "" && txtAmount3.Text != "00.00")
                            {
                                AddNormal();
                                AddContra();
                            }
                        }
                        if (CtrlRow.Text == "4")
                        {
                            if (txtAmount4.Text != "" && txtAmount4.Text != "00.00")
                            {
                                AddNormal();
                                AddContra();
                            }
                        }
                    }

                    gvDetail();
                    SumValue();

                    lbltotAmt.Visible = false;
                    txttotAmt.Text = string.Empty;

                    VisibleFalse();

                    lblAccTitle.Text = string.Empty;

                    //lblCuName.Text = string.Empty;
                    //lblMemName.Text = string.Empty;
                    //txtCreditUNo.Text = string.Empty;
                    //txtMemNo.Text = string.Empty;


                    txtAccNo.Text = string.Empty;
                    txtTrnType1.Text = string.Empty;
                    txtTrnType2.Text = string.Empty;
                    txtTrnType3.Text = string.Empty;
                    txtTrnType4.Text = string.Empty;

                    txtPayDesc1.Text = string.Empty;
                    txtPayDesc2.Text = string.Empty;
                    txtPayDesc3.Text = string.Empty;
                    txtPayDesc4.Text = string.Empty;

                    txtAmount1.Text = string.Empty;
                    txtAmount2.Text = string.Empty;
                    txtAmount3.Text = string.Empty;
                    txtAmount4.Text = string.Empty;

                    CtrlLogicAmt.Text = string.Empty;



                    BtnUpdate.Visible = true;

                    //txtOldCuNo.ReadOnly = true;



                    txtTranDate.ReadOnly = true;


                    txtVchNo.ReadOnly = true;

                    //txtGLCashCode.ReadOnly = true;
                    //ddlGLCashCode.Enabled = false;
                    //txtGLContraCode.ReadOnly = true;
                    //ddlGLContraCode.Enabled = false;

                    //ddlTrnType.Enabled = false;
                    //txtChqNo.ReadOnly = true;


                    txtTranDate.ReadOnly = true;

                    txtAmount1.ReadOnly = false;
                    txtAmount2.ReadOnly = false;
                    txtAmount3.ReadOnly = false;
                    txtAmount4.ReadOnly = false;



                    CtrlMsgFlag.Text = "0";
                    CtrlIntFlag.Text = "0";
                    CtrlBenefitFlag1.Text = "0";
                    CtrlBenefitFlag2.Text = "0";
                    CtrlBenefitFlag3.Text = "0";
                    CtrlBenefitFlag4.Text = "0";
                    CtrlInterestAmt.Text = "0";

                    BtnAdd.Visible = false;
                    BtnCancel.Visible = false;
                    ClearHstInfo();
                    txtAccNo.Focus();


                    if (lblFrontHelp.Text == "1")
                    {
                        Dtl1.Visible = false;
                        Dtl2.Visible = true;
                        BtnSearch.Enabled = false;

                        txtCreditUNo.ReadOnly = true;
                        txtMemNo.ReadOnly = true;
                        gvGroupDetail();
                    }

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnAdd_Click Problem');</script>");
                //throw ex;
            }
        }

        // ---------------- CANCEL TRANSACTIONS ------------------------
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearHstInfo();
            VisibleFalse();

            txtAccNo.Text = string.Empty;
            txtTrnType1.Text = string.Empty;
            txtTrnType2.Text = string.Empty;
            txtTrnType3.Text = string.Empty;
            txtTrnType4.Text = string.Empty;

            txtPayDesc1.Text = string.Empty;
            txtPayDesc2.Text = string.Empty;
            txtPayDesc3.Text = string.Empty;
            txtPayDesc4.Text = string.Empty;

            txtAmount1.Text = string.Empty;
            txtAmount2.Text = string.Empty;
            txtAmount3.Text = string.Empty;
            txtAmount4.Text = string.Empty;

            txtAmount1.ReadOnly = false;
            txtAmount2.ReadOnly = false;

            CtrlLogicAmt.Text = string.Empty;
            CtrlIntFlag.Text = string.Empty;


            if (lblFrontHelp.Text == "1")
            {
                Dtl1.Visible = false;
                Dtl2.Visible = true;
                txtAccNo.Focus();
                return;
            }



            lblAccTitle.Text = string.Empty;
            lblCuName.Text = string.Empty;
            lblMemName.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;
            txtMemNo.Text = string.Empty;

            lbltotAmt.Visible = false;
            txttotAmt.Text = string.Empty;
            txtAccNo.Focus();



            //ddlAccNo.SelectedValue = "-Select-";

        }


        protected void AccessCashAmountMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Cash Balance');", true);
            return;
        }

        protected void AccessBankAmountMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Bank Balance');", true);
            return;
        }



        //  -------------- UPDATE TRANSACTIONS ------------------------------
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                int GLCode = Converter.GetInteger(lblCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVoucherNo.Text = "C" + lblCashCode.Text + "-" + getDTO.RecLastNo;

                TrnLimitValidity();

                var prm = new object[7];

                prm[0] = lblID.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = CtrlProcStat.Text;
                if (hdnAccFlag.Text == "")
                {
                    prm[3] = 0;
                }
                else
                {
                    prm[3] = hdnAccFlag.Text;
                }

                prm[4] = 0;

                prm[5] = "0";

                prm[6] = CtrlModule.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddTransaction", prm, "A2ZCSMCUS"));

                if (result == 0)
                {
                    //UpdatedSucessfullyMSG();


                    if (lblVPrintFlag.Text == "1")
                    {
                        PrintTrnVoucher();
                    }
                    else
                    {
                        UpdatedSucessfullyMSG();
                    }

                    ClearInfo();

                    txtTotalAmt.Text = string.Empty;
                    lblTotalAmt.Visible = false;
                    BtnUpdate.Visible = false;

                    CFlag.Text = string.Empty;

                    //txtOldCuNo.ReadOnly = false;


                    Dtl2.Visible = false;
                    Dtl1.Visible = true;


                    txtCreditUNo.ReadOnly = false;
                    txtMemNo.ReadOnly = false;

                    txtTranDate.ReadOnly = false;
                    txtVchNo.ReadOnly = false;



                    txtAccNo.ReadOnly = false;
                    BtnSearch.Enabled = true;


                    txtTranDate.ReadOnly = false;
                    //txtTranDate.Text = CtrlProcDate.Text;

                    lblFuncOpt.Text = lblOrgFuncOpt.Text;




                    lblFrontHelp.Text = "0";

                    txtVchNo.Focus();
                    gvDetail();
                    UpdateBackUpStat();


                    if (CtrlModule.Text != "06" && CtrlModule.Text != "07")
                    {
                        lblMemName.Text = string.Empty;
                        lblMemName.Visible = false;

                    }

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        // ------------ EXIT PROGRAMS ---------------------------


        protected void PrintTrnVoucher()
        {
            try
            {

                lblTrnTypeTitle.Text = "CASH";



                DateTime Pdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (lblTransactionMode.Text == "2")
                {
                    Pdate = Vdate;
                }


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text.Trim());
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTransactionVch");



                Session["STranDate"] = txtTranDate.Text;
                Session["SFuncOpt"] = lblFuncOpt.Text;
                Session["SModule"] = CtrlModule.Text;
                Session["flag"] = "2";

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }


        protected void UpdateBackUpStat()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);

            if (CtrlBackUpStat.Text == "1")
            {
                Int16 BStat = 0;
                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }
            }
        }

        
        protected void TrnLimitValidity()
        {
            try
            {
                double TotalAmt = Converter.GetDouble(txtTotalAmt.Text);
                int Ids = Converter.GetInteger(lblID.Text);
                A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

                CtrlProcStat.Text = "0";

                if (getDTO.IdsNo > 0)
                {

                    if (TotalAmt > getDTO.LIdsCashCredit)
                    {
                        CtrlProcStat.Text = "1";
                    }
                    else
                    {
                        CtrlProcStat.Text = "0";
                    }

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnLimitValidity Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            if (txtTotalAmt.Text != "00.00" && txtTotalAmt.Text != "")
            {
                RecordsAddedMSG();

            }
            else
            {
                RemoveSession();
                Session["CFlag"] = string.Empty;
                Session["SlblTransactionMode"] = string.Empty;
                CFlag.Text = string.Empty;
                Response.Redirect("A2ZERPModule.aspx");
            }
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }


        protected void gvDetailInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //if (lblFuncOpt.Text == "09" || lblFuncOpt.Text == "10")
                //{
                //    string strQuery1 = "DELETE FROM WF_Transaction WHERE UserID= '" + lblID.Text + "'";
                //    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                //}
                //else
                //{
                Label lblId = (Label)gvDetailInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                Label lblAType = (Label)gvDetailInfo.Rows[e.RowIndex].Cells[1].FindControl("AccType");
                Label lblANo = (Label)gvDetailInfo.Rows[e.RowIndex].Cells[2].FindControl("AccNo");

                int ID = Converter.GetInteger(lblId.Text);
                int AType = Converter.GetInteger(lblAType.Text);
                int ANo = Converter.GetInteger(lblANo.Text);

                string strQuery1 = "DELETE FROM WF_Transaction WHERE UserID= '" + lblID.Text + "' AND AccType= '" + lblAType.Text + "' AND AccNo= '" + lblANo.Text + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                //int idincrement = ID + 1;
                //string strQuery1 = "DELETE FROM WF_Transaction WHERE Id between '" + ID + "' and '" + idincrement + "'";
                //int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                //}
                gvDetail();
                SumValue();

                if (txtTotalAmt.Text == "00.00")
                {
                    //txtOldCuNo.ReadOnly = false;

                    txtCreditUNo.ReadOnly = false;
                    txtMemNo.ReadOnly = false;


                    txtAccNo.ReadOnly = false;

                    txtTranDate.ReadOnly = false;
                    txtVchNo.ReadOnly = false;
                    BtnSearch.Enabled = true;


                    BtnUpdate.Visible = false;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }



        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidInputDate()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Input Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidDepositAmt()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Deposit Amount');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidPeriodMSG()
        {
            

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Period');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtTranDate.Text.Length.ToString();
            if (date != "10")
            {
                InvalidInputDate();
                txtTranDate.Text = CtrlProcDate.Text;
                lblTransactionMode.Text = string.Empty;
                BtnLV.Text = "LIVE TRANSACTION";
                BtnLV.BackColor = Color.Blue;
                txtTranDate.Text = CtrlProcDate.Text;
                //txtTranDate.ReadOnly = true;
                Session["SlblTransactionMode"] = lblTransactionMode.Text;
                txtTranDate.Focus();
                return;
            }


            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Month1 = opdate1.Month;
            int Month2 = opdate2.Month;

            if (opdate1 > opdate2 || Month1 != Month2)
            {
                InvalidDateMSG();

                lblTransactionMode.Text = string.Empty;
                BtnLV.Text = "LIVE TRANSACTION";
                BtnLV.BackColor = Color.Blue;
                txtTranDate.Text = CtrlProcDate.Text;
                //txtTranDate.ReadOnly = true;
                Session["SlblTransactionMode"] = lblTransactionMode.Text;
                txtTranDate.Focus();
                return;
            }

            //if (opdate1 < opdate2)
            //{
            //    lblTranDate.Text = "Back Value Date:";
            //}
            //else
            //{
            //    lblTranDate.Text = "Transaction Date:";
            //}

            //Session["LDateTitle"] = lblTranDate.Text;

            if (opdate1 < opdate2)
            {
                lblTransactionMode.Text = "2";
                BtnLV.Text = "BACK VALUE TRANSACTION";
                BtnLV.BackColor = Color.Red;
                //txtTranDate.ReadOnly = false;
                Session["SlblTransactionMode"] = lblTransactionMode.Text;
            }
            else
            {
                lblTransactionMode.Text = string.Empty;
                BtnLV.Text = "LIVE TRANSACTION";
                BtnLV.BackColor = Color.Blue;
                txtTranDate.Text = CtrlProcDate.Text;
                //txtTranDate.ReadOnly = true;
                Session["SlblTransactionMode"] = lblTransactionMode.Text;
            }


        }

        private void GenerateTransactionCode()
        {
            var prm = new object[6];

            if (CtrlModule.Text == "04")
            {
                prm[0] = 0;
                prm[1] = 0;
            }
            else
            {
                prm[0] = lblCuType.Text;
                prm[1] = lblCuNo.Text;
            }
            prm[2] = txtMemNo.Text;
            prm[3] = lblFuncOpt.Text;
            prm[4] = lblID.Text;
            prm[5] = CtrlModule.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateGroupAccount", prm, "A2ZCSMCUS"));

            if (result == 0)
            {
                string qry = "SELECT Id,TrnCode FROM WFCSGROUPACCOUNT where UserId='" + lblID.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    //string sqlquery = @"SELECT distinct TrnCode,+ CAST (TrnCode AS VARCHAR(100))+ '-' + LTRIM(RTRIM(TrnCodeDesc)) from WFCSGROUPACCOUNT where UserId='" + hdnID.Value + "'";
                    //ddlTrnCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTrnCode, "A2ZCSMCUS");

                    //ddlTrnCode.SelectedIndex = 0;
                }
                else
                {
                    InvalidAccountNoMSG();
                    return;
                }

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SlblVchMemNo"] = lblVchMemNo.Text;
            Session["SlblVchMemName"] = lblVchMemName.Text;

            Session["STranDate"] = txtTranDate.Text;
            Session["SFuncOpt"] = lblFuncOpt.Text;
            Session["SModule"] = CtrlModule.Text;
            Session["SCtrlSelectAccNo"] = CtrlSelectAccNo.Text;
            Session["SInputModule"] = InputModule.Text;


            Session["SVchNo"] = txtVchNo.Text;
            Session["SControlFlag"] = "0";
            Session["flag"] = "1";
            if (lblflag.Text == "2")
            {
                Session["CFlag"] = "0";
            }
            else
            {
                Session["CFlag"] = CFlag.Text;
            }

            if (CtrlModule.Text == "04")
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

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            Session["SlblVchMemNo"] = lblVchMemNo.Text;
            Session["SlblVchMemName"] = lblVchMemName.Text;

            Session["STranDate"] = txtTranDate.Text;
            Session["SFuncOpt"] = lblFuncOpt.Text;
            Session["SModule"] = CtrlModule.Text;
            Session["SCtrlSelectAccNo"] = CtrlSelectAccNo.Text;
            Session["SInputModule"] = InputModule.Text;


            Session["SVchNo"] = txtVchNo.Text;


            Session["SlblCuName"] = lblCuName.Text;
            Session["SlblMemName"] = lblMemName.Text;
            Session["StxtCreditUNo"] = txtCreditUNo.Text;

            Session["SCuType"] = lblCuType.Text;
            Session["SCuNo"] = lblCuNo.Text;
            Session["SMemNo"] = txtMemNo.Text;

            Session["SlblFrontHelp"] = lblFrontHelp.Text;


            Session["flag"] = "1";


            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('CSInstantAccountOpeningMaintenance.aspx','_blank');</script>", false);

        }


        
        protected void BtnLV_Click(object sender, EventArgs e)
        {
            if (lblTransactionMode.Text == "2")
            {
                lblTransactionMode.Text = string.Empty;
                BtnLV.Text = "LIVE TRANSACTION";
                BtnLV.BackColor = Color.Blue;
                txtTranDate.Text = CtrlProcDate.Text;
                txtTranDate.ReadOnly = true;
                Session["SlblTransactionMode"] = lblTransactionMode.Text;
            }
            else
                if (lblTransactionMode.Text == string.Empty)
                {
                    lblTransactionMode.Text = "1";
                    BtnLV.Text = "BACK VALUE TRANSACTION";
                    BtnLV.BackColor = Color.Green;
                    txtTranDate.ReadOnly = false;
                    Session["SlblTransactionMode"] = lblTransactionMode.Text;
                }
                else
                    if (lblTransactionMode.Text == "1")
                    {
                        lblTransactionMode.Text = "2";
                        BtnLV.Text = "BACK LOG TRANSACTION";
                        BtnLV.BackColor = Color.Red;
                        txtTranDate.ReadOnly = false;
                        Session["SlblTransactionMode"] = lblTransactionMode.Text;
                    }
        }


        protected void BtnHelpBankCode_Click(object sender, EventArgs e)
        {


        }



        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            if (txtCreditUNo.Text != string.Empty)
            {
                gvGroupAccInfo.Visible = false;


                txtMemNo.Text = string.Empty;
                lblMemName.Text = string.Empty;

                A2ZCUNIONDTO getDTO = new A2ZCUNIONDTO();

                if (chkOldSearch.Checked == true)
                {
                    int CN = Converter.GetInteger(txtCreditUNo.Text);
                    hdnCuNumber.Text = Converter.GetString(CN);

                    getDTO = (A2ZCUNIONDTO.GetOldInfo(CN));
                }
                else
                {
                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);

                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(CType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(CNo);

                    getDTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));

                }

                if (getDTO.NoRecord > 0)
                {
                    lblCuType.Text = Converter.GetString(getDTO.CuType);
                    lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                    lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

                    txtAccNo.Text = string.Empty;
                    txtMemNo.Focus();
                }
                else
                {
                    txtAccNo.Text = string.Empty;
                    txtCreditUNo.Text = string.Empty;
                    txtCreditUNo.Focus();
                    return;
                }
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

            if (chkOldSearch.Checked == true)
            {
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                getDTO = (A2ZMEMBERDTO.GetInfoOldMember(CuNumber, MemNumber));
            }
            else
            {
                Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                int CUNo = Converter.GetInteger(lblCuNo.Text);
                int MNo = Converter.GetInteger(txtMemNo.Text);

                getDTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));

            }

            if (getDTO.NoRecord > 0)
            {
                txtAccNo.Text = string.Empty;
                txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                lblMemName.Text = Converter.GetString(getDTO.MemberName);

                gvGroupDetail();
            }
            else
            {
                txtAccNo.Text = string.Empty;
                txtMemNo.Text = string.Empty;
                txtMemNo.Focus();
                return;
            }

        }

        protected void gvGroupDetail()
        {

            if (lblCuType.Text != string.Empty && lblCuNo.Text != string.Empty && txtMemNo.Text != string.Empty)
            {
                GenerateGroupAccounts();

                Dtl1.Visible = false;
                Dtl2.Visible = true;

                gvGroupAccInfo.Visible = true;

                string sqlquery3 = "SELECT distinct AccType,TrnCodeDesc,AccNo,AccOldNumber,AccOrgInstlAmt,AccTrfAccNo FROM WFCSGROUPACCOUNT WHERE UserId='" + lblID.Text + "' ORDER BY AccNo";
                gvGroupAccInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvGroupAccInfo, "A2ZCSMCUS");
            }
        }

        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }

        private void GenerateGroupAccounts()
        {
            int result = 0;

            var prm = new object[7];

            prm[0] = lblCuType.Text;
            prm[1] = lblCuNo.Text;
            prm[2] = txtMemNo.Text;
            prm[3] = lblFuncOpt.Text;
            prm[4] = lblID.Text;
            //prm[5] = lblModule.Text;
            prm[5] = 1;
            prm[6] = 0;


            result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetGroupAccountInfo", prm, "A2ZCSMCUS"));


            if (result == 0)
            {
                string qry = "SELECT Id,TrnCode FROM WFCSGROUPACCOUNT  WHERE UserId='" + lblID.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count == 0)
                {
                    InvalidAcc();
                    txtCreditUNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    txtMemNo.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    return;
                }
                else
                {
                    lblFrontHelp.Text = "1";
                }
            }

        }


        protected void gvGroupAccInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }



        protected void gvGroupAccInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvGroupAccInfo.SelectedRow;
            lblAccountNo.Text = row.Cells[2].Text;
            lblAccTrfAccNo.Text = row.Cells[5].Text.Replace("&nbsp;", "");

            if (lblAccTrfAccNo.Text != "0" && lblAccTrfAccNo.Text != string.Empty)
            {
                txtAccNo.Text = lblAccTrfAccNo.Text;
            }
            else
            {
                txtAccNo.Text = lblAccountNo.Text;
            }


            Dtl1.Visible = true;
            Dtl2.Visible = false;


            AccGetInfo();
        }


    }

}