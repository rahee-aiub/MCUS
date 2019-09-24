using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
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
    public partial class CSManualTransactionTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    Accdropdown();
                    gvDetail();
                    string RSearchflag = (string)Session["SSearchflag"];

                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;

                    string Module = (string)Session["SModule"];

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                   
                    lblProcdate.Text = date;

                    if (lblflag.Text == string.Empty)
                    {
                        
                        hdnFuncOpt.Text = "1";
                        hdnModule.Text = "1";
                        txtAccNo.Focus();
                    }
                    else
                    {
                        hdnModule.Text = Module;


                        string RtxtTranDate = (string)Session["StxtTranDate"];
                        string RlblProcdate = (string)Session["SlblProcdate"];

                        string RCtrlAccType = (string)Session["SCtrlAccType"];
                        string RtxtAccNo = (string)Session["StxtAccNo"];
                        string RlblAccTitle = (string)Session["SlblAccTitle"];
                        string RtxtCreditUNo = (string)Session["StxtCreditUNo"];

                        string RlblCuType = (string)Session["SlblCuType"];
                        string RlblCuNo = (string)Session["SlblCuNo"];

                        string Rlblcls = (string)Session["Slblcls"];
                        string RlblPayType = (string)Session["SlblPayType"];

                        string RlblCuName = (string)Session["SlblCuName"];
                        string RtxtMemNo = (string)Session["StxtMemNo"];
                        string RlblMemName = (string)Session["SlblMemName"];
                        string RlblAccBalance = (string)Session["SlblAccBalance"];

                        string RlblLimitBalance = (string)Session["SlblLimitBalance"];

                       
                       

                        CtrlAccType.Text = RCtrlAccType;
                        lblcls.Text = Rlblcls;
                        lblPayType.Text = RlblPayType;
                        txtAccNo.Text = RtxtAccNo;
                        lblAccTitle.Text = RlblAccTitle;
                        txtCreditUNo.Text = RtxtCreditUNo;

                        lblCuType.Text = RlblCuType;
                        lblCuNo.Text = RlblCuNo;

                        lblCuName.Text = RlblCuName;
                        txtMemNo.Text = RtxtMemNo;
                        lblMemName.Text = RlblMemName;
                    }

                    hdnUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + hdnCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        lblBoothNo.Text = hdnCashCode.Text;
                        lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                    }

                    if (lblflag.Text == "1" && RSearchflag == "1" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        GetAccInfo1();
                    }

                    if (lblflag.Text == "1" && RSearchflag == "2" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        GetAccInfo1();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        private void Accdropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeClass = 1 AND AccTypeMode = 1 ORDER BY AccTypeCode";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }
        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;
        }

        protected void RemoveSession1()
        {

            Session["SSearchflag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;

            Session["SCtrlAccType"] = string.Empty;
            Session["StxtAccNo"] = string.Empty;
            Session["SlblAccTitle"] = string.Empty;
            Session["StxtCreditUNo"] = string.Empty;
            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;
            Session["Slblcls"] = string.Empty;
            Session["SlblCuName"] = string.Empty;
            Session["StxtMemNo"] = string.Empty;
            Session["SlblMemName"] = string.Empty;
            Session["SlblAccBalance"] = string.Empty;
            Session["SlblLimitBalance"] = string.Empty;

            ClearScreen();
        }

        protected void UnPostValue()
        {
            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(lblProcdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnCredit) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Text + "' AND CuNo ='" + lblCuNo.Text + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + CtrlAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblUnPostDataCr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountCr"]));
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnDebit) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Text + "' AND CuNo ='" + lblCuNo.Text + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + CtrlAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblUnPostDataDr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountDr"]));
            }
            double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Text);
            double AmtCredit = Converter.GetDouble(lblUnPostDataCr.Text);
            double AmtDebit = Converter.GetDouble(lblUnPostDataDr.Text);
            double LienAmt = Converter.GetDouble(CtrlLienAmt.Text);
        }



        protected void UpdatedSucessfullyMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            if (CtrlProcStat.Text == "0")
            {
                a = "    TRANSACTION SUCESSFULLY DONE";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "    TRANSACTION INPUT DONE";
            }
            b = "Generated New Voucher No.";
            c = string.Format(CtrlVoucherNo.Text);


            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }

        protected void AccessAmountMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);
            return;
        }

        protected void DuplicateVchMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;
        }

        protected void TrnVchDeplicate()
        {
          //  DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblDuplicateVch.Text = "1";
                DuplicateVchMSG();
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                return;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblDuplicateVch.Text = "0";

                if (txtVchNo.Text == string.Empty)
                {
                    txtVchNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }

                TrnVchDeplicate();

                if( lblDuplicateVch.Text == "1")
                {
                    return;
                }

                if (txtDescription.Text == string.Empty)
                {
                    txtDescription.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction Description');", true);
                    return;
                }

                BalanceChaeck();

                if (ValidityFlag.Text == "1")
                {
                    txtAmount.Text = string.Empty;
                    txtAmount.Focus();
                    AccessAmountMSG();
                    return;
                }

                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVoucherNo.Text = "C" + hdnCashCode.Text + "-" + getDTO.RecLastNo;

              

               // DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[7];
                prm[0] = Converter.GetDateToYYYYMMDD(lblProcdate.Text);
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = txtVchNo.Text;                             
                prm[3] = ddlTranMode.SelectedValue;
                prm[4] = txtDescription.Text;
                prm[5] = txtGLContraCode.Text;
                prm[6] = hdnCashCode.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSManualTransactionTransfer", prm, "A2ZCSMCUS"));

                if (result == 0)
                {
                    Clear();

                    gvDetail();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transfar Transaction Successfully Done');", true);
                    return;
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
            }
        }

        protected void BalanceChaeck()
        {

            ValidityFlag.Text = "0";

            double limitamt = 0;
            double Amount = 0;
            double sancAmount = 0;
            double TrnAmount = 0;

            //Amount = Converter.GetDouble(lblAccBalance.Text);
            //sancAmount = Converter.GetDouble(CtrlLoanSancAmt.Text);
            //TrnAmount = Converter.GetDouble(txtTrnAmount.Text);
            //limitamt = Converter.GetDouble(lblLimitBalance.Text);

            //if (Amount > 0)
            //{
            //    limitamt = (sancAmount + Amount);
            //}
            //else
            //{
            //    limitamt = (sancAmount - Math.Abs(Amount));
            //}

            if (lblAtyClass.Text == "5" || lblAtyClass.Text == "6")
            {
                if (limitamt < TrnAmount)
                {
                    ValidityFlag.Text = "1";
                    return;
                }
            }
            else
            {
                if (Amount < TrnAmount)
                {
                    ValidityFlag.Text = "1";
                    return;
                }
            }

        }



        protected void Clear()
        {
            txtAccType.Text = string.Empty;
            ddlAcType.SelectedIndex = 0;
            txtGLContraCode.Text = string.Empty;
            lblGLCodeDesc.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtVchNo.Text = string.Empty;

        }


        protected void ClearScreen()
        {
            txtVchNo.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;
            lblCuName.Text = string.Empty;
            txtMemNo.Text = string.Empty;
            lblMemName.Text = string.Empty;

            txtAccNo.Text = string.Empty;
            lblAccTitle.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;
            lblCuName.Text = string.Empty;
            txtMemNo.Text = string.Empty;
            lblMemName.Text = string.Empty;

            txtAccNo.Text = string.Empty;
            lblAccTitle.Text = string.Empty;

            txtAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;



            txtVchNo.Focus();


        }


        protected void InvalidAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account');", true);
            return;
        }

        protected void TransferedAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }

        protected void ClosedAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Closed');", true);
            return;
        }
        protected void InvalidInput()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Transfer');", true);
            return;
        }
        protected void Successfull()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfer Successfully Done');", true);
            return;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }







        public void GetAccInfo1()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccount();
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
                        TransferedAccount();
                        return;
                    }

                    if (CtrlAccStatus.Text == "99")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        ClosedAccount();
                        return;
                    }


                    CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    CtrlLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    CtrlLoanSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.LoanAmount));

                    CtrlAccType.Text = Converter.GetString(accgetDTO.AccType);
                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);


                    UnPostValue();


                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);


                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);

                    lblAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);

                    if (lblAtyClass.Text == "7")
                    {
                        string input = txtAccNo.Text;
                        lblPayType.Text = input.Substring(13, 3);
                        int paytype = Converter.GetInteger(lblPayType.Text);
                        A2ZTRNCODEDTO get3DTO = (A2ZTRNCODEDTO.GetInformation99(paytype));
                        if (get3DTO.TrnCode > 0)
                        {
                            lblTrnCode.Text = Converter.GetString(get3DTO.TrnCode);
                            lblAccTitle.Text = Converter.GetString(get3DTO.TrnDescription);
                        }
                    }
                    else
                    {
                        lblPayType.Text = "0";
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

                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNo.Text);
                    int MNo = Converter.GetInteger(txtMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }


        protected void txtGLContraCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int GLCode;
                A2ZCGLMSTDTO glObj = new A2ZCGLMSTDTO();
                string input1 = Converter.GetString(txtGLContraCode.Text).Length.ToString();
                if (input1 == "6")
                {
                    GLCode = Converter.GetInteger(txtGLContraCode.Text);
                    glObj = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                }
                else
                {
                    GLCode = Converter.GetInteger(txtGLContraCode.Text);
                    glObj = (A2ZCGLMSTDTO.GetInformation(GLCode));
                }

                if (glObj.GLAccNo > 0)
                {
                    lblStatus.Text = Converter.GetString(glObj.Status);
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    hdnGLSubHead2.Text = Converter.GetString(glObj.GLSubHead);
                    lblAccMode.Text = Converter.GetString(glObj.GLAccMode);


                    if (lblStatus.Text == "99")
                    {
                        txtGLContraCode.Text = string.Empty;
                        txtGLContraCode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Closed GL Code');", true);
                        return;
                    }

                    if (CtrlRecType.Text != "2")
                    {
                        txtGLContraCode.Text = string.Empty;
                        txtGLContraCode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
                        return;
                    }

                    if (lblAccMode.Text == "1")
                    {
                        txtGLContraCode.Text = string.Empty;
                        txtGLContraCode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Non GL Account');", true);
                        return;
                    }


                    if (hdnGLSubHead2.Text == "10101000")
                    {
                        txtGLContraCode.Text = string.Empty;
                        txtGLContraCode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
                        return;
                    }
                    else
                    {
                        lblGLCodeDesc.Text = Converter.GetString(glObj.GLAccDesc);
                        txtDescription.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Code - Does Not Exists');", true);
                    txtGLContraCode.Text = string.Empty;
                    txtGLContraCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnsactionCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {

            if (txtAccNo.Text != string.Empty)
            {
                //GetAccInfo1();
                AccGetInfo();
            }
        }

        protected void AccGetInfo()
        {
            try
            {               

                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);

                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));

                if (accgetDTO.a == 0 || accgetDTO.AccStatus > 97)
                {
                    txtAccType.Text = string.Empty;
                    ddlAcType.SelectedIndex = 0;
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
                    txtAccType.Text = Converter.GetString(accgetDTO.AccType);
                    if (txtAccType.Text != "0")
                    {
                        ddlAcType.SelectedValue = txtAccType.Text;                 
                    }
  
                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);
                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);
                    lblMemName.Text = Converter.GetString(accgetDTO.MemberName);
                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);               

                    Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }
                    
                }
            }


            catch (Exception ex)
            {

                ClearText();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.AccGetInfo Problem');</script>");
                //throw ex;
            }

        }
        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            if (txtCreditUNo.Text != string.Empty)
            {

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
            A2ZACCOUNTDTO GetAccount = new A2ZACCOUNTDTO();

            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int AccType = Converter.GetInteger(ddlAcType.SelectedValue);
            int CreditNo = Converter.GetInteger(lblCuNo.Text);
            int MemberNo = Converter.GetInteger(txtMemNo.Text);

            GetAccount = (A2ZACCOUNTDTO.GetInfo(AccType, CuType, CreditNo, MemberNo));

            if (GetAccount.AccNo > 0 && GetAccount.AccStatus < 98)
            {
                txtAccNo.Text = Converter.GetString(GetAccount.AccNo);
            }

            else
            {
                txtAccNo.Text = string.Empty;
                txtMemNo.Text = string.Empty;
                txtMemNo.Focus();
                return;
            }


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
                lblMemName.Text = Converter.GetString(getDTO.MemberName);
                txtAmount.Focus();
            }
            else
            {
                lblMemName.Text = string.Empty;
                txtMemNo.Focus();
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            txtAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", txtAmount.Text));
            BtnAdd.Focus();
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAccType.Text != string.Empty)
                {
                    ddlAcType.SelectedValue = txtAccType.Text;
                }
            }
            catch (Exception Ex)
            {
                ddlAcType.SelectedIndex = 0;
                txtAccType.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
                return;
                
            }
           
        }

        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAcType.SelectedIndex != 0)
            {
                txtAccType.Text = ddlAcType.SelectedValue.ToString();
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtAccNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Account No.');", true);
                return;
            }
            if (txtCreditUNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Credit Union No.');", true);
                return;
            }
            if (txtMemNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Depositor No.');", true);
                return;
            }
            if (txtAmount.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Amount');", true);
                return;
            }



            int rowEffect = 0;
            string strQuery = @"INSERT into WFTRANSFERTRN(AccNo,CuType,CreditUnionNo,DepositorNo,TrnAmount,UserId,CreditUnionName,DepositorName)values('" + txtAccNo.Text + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtAmount.Text + "','" + hdnUserId.Text + "','" + lblCuName.Text + "','" + lblMemName.Text + "')";
            rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            if (rowEffect == 0)
            {

            }
            else
            {

                ClearText();
                gvDetail();
            }
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label Id = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");

                string qry = "SELECT Id,AccNo,CreditUnionNo,DepositorNo,TrnAmount FROM WFTRANSFERTRN";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label Id = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");

                string sqlQuery1 = @"DELETE FROM WFTRANSFERTRN WHERE Id = " + Id.Text + "";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                    gvDetail();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted');", true);
                    return;
                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,AccNo,CreditUnionNo,DepositorNo,TrnAmount FROM WFTRANSFERTRN";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        private void ClearText()
        {
            txtAccNo.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;
            lblCuName.Text = string.Empty;
            txtMemNo.Text = string.Empty;
            lblMemName.Text = string.Empty;
            txtAmount.Text = string.Empty;
            chkOldSearch.Checked = false;
        }

        protected void ShowReport()
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime Pdate = Converter.GetDateTime(dto.ProcessDate);


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

              //  SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text.Trim());
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);

               
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, ddlTranMode.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, txtGLContraCode.Text);
                

               
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSMultiTransaction");



   
                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //if(txtGLContraCode.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input GL Contra Code');", true);
            //    return;
            //}

            ShowReport();
        }
    }
}




