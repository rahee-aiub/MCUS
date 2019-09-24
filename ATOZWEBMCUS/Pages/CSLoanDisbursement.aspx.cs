using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
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
    public partial class CSLoanDistbursment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFuncOpt.Text ="4";
                // CreditUnionDropdown();
                
                txtLoanAppNo.Focus();
                // TransactionCodeDropdown();
                GLCashCodeDropdown();
                //FunctionName();
                //BtnUpdate.Visible = false;

                lblTotalAmt.Visible = false;

                VisibleFalse();
                //if (lblFuncOpt.Text == "2")
                //{
                //    BtnViewImage.Visible = true;
                //}
                //else
                //{
                //    BtnViewImage.Visible = false;
                //}

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtTranDate.Text = date;

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                //txtIdNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string sqlquery;
                sqlquery = @"DELETE dbo.WF_Transaction WHERE UserID='" + hdnID.Text + "'";

                int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));

                if (result > 0)
                {

                }
            }
        }


        //protected void FunctionName()
        //{
        //    if (lblFuncOpt.Text == "1")
        //    {
        //        lblTransFunction.Text = "DEPOSIT";
        //    }
        //    if (lblFuncOpt.Text == "2")
        //    {
        //        lblTransFunction.Text = "WITHDRAWAL";
        //    }
        //    if (lblFuncOpt.Text == "3")
        //    {
        //        lblTransFunction.Text = "INTEREST/BENEFIT WITHDRAWAL";
        //    }
        //    if (lblFuncOpt.Text == "4")
        //    {
        //        lblTransFunction.Text = "LOAN DISBURSEMENT";
        //    }
        //    if (lblFuncOpt.Text == "5")
        //    {
        //        lblTransFunction.Text = "ENCASHMENT";
        //    }
        //}
        protected void VisibleFalse()
        {
            //txtTrnType1.Visible = false;
            //lblPayDesc1.Visible = false;
            //txtAmountCr1.Visible = false;
            //txtAmountDr1.Visible = false;
            txtTrnType2.Visible = false;
            lblPayDesc2.Visible = false;
            txtAmount2.Visible = false;
            txtTrnType3.Visible = false;
            lblPayDesc3.Visible = false;
            txtAmount3.Visible = false;

        }
        //private void CreditUnionDropdown()
        //{

        //    string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
        //    ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        //}

        //protected void AccountNoDropdown()
        //{

        //    string sqlquery = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
        //    ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccNo, "A2ZCSMCUS");


        //}
        //protected void TransactionCodeDropdown()
        //{

        //    string sqlquery = "SELECT distinct TrnCode,TrnDesc from A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "'";
        //    ddlTrnCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTrnCode, "A2ZCSMCUS");

        //}

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 101000";
            ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLMCUS");

        }
        //protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        //{

        //    try
        //    {

        //        if (txtCreditUNo.Text != string.Empty)
        //        {

        //            string c = "";
        //            int a = txtCreditUNo.Text.Length;

        //            string b = txtCreditUNo.Text;
        //            c = b.Substring(0, 1);
        //            int re = Converter.GetSmallInteger(c);
        //            int dd = a - 1;
        //            string d = b.Substring(1, dd);
        //            int re1 = Converter.GetSmallInteger(d);

        //            Int16 CuType = Converter.GetSmallInteger(re);
        //            lblCuType.Text = Converter.GetString(CuType);
        //            int CNo = Converter.GetSmallInteger(re1);
        //            lblCuNo.Text = Converter.GetString(CNo);

        //            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
        //            if (getDTO.CreditUnionNo > 0)
        //            {
        //                string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
        //                ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
        //                ddlCreditUNo.SelectedValue = Converter.GetString(txtCreditUNo.Text);
        //                txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

        //                int GLCode = Converter.GetInteger(txtGLCashCode.Text);
        //                A2ZCGLMSTDTO get1DTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

        //                if (get1DTO.GLAccNo > 0)
        //                {
        //                    ddlGLCashCode.SelectedValue = Converter.GetString(get1DTO.GLAccNo);
        //                }
        //                else
        //                {
        //                    txtGLCashCode.Text = string.Empty;
        //                    txtGLCashCode.Focus();
        //                }

        //                txtMemNo.Focus();

        //                string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
        //                ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");

        //            }
        //            else
        //            {
        //                InvalidCuNoMSG();
        //                ddlCreditUNo.SelectedValue = "-Select-";
        //                ddlMemNo.SelectedIndex = 0;
        //                txtMemNo.Text = string.Empty;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCreditUNo.SelectedValue == "-Select-")
        //    {

        //        txtCreditUNo.Text = string.Empty;
        //        txtMemNo.Focus();
        //        ddlMemNo.SelectedIndex = 0;
        //        return;
        //    }

        //    try
        //    {

        //        if (ddlCreditUNo.SelectedValue != "-Select-")
        //        {

        //            txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

        //            string c = "";
        //            int a = txtHidden.Text.Length;

        //            string b = txtHidden.Text;
        //            c = b.Substring(0, 1);
        //            int re = Converter.GetSmallInteger(c);
        //            int dd = a - 1;
        //            string d = b.Substring(1, dd);
        //            int re1 = Converter.GetSmallInteger(d);

        //            Int16 CuType = Converter.GetSmallInteger(re);
        //            lblCuType.Text = Converter.GetString(CuType);
        //            int CNo = Converter.GetSmallInteger(re1);
        //            lblCuNo.Text = Converter.GetString(CNo);

        //            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";

        //            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");


        //            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

        //            if (getDTO.CreditUnionNo > 0)
        //            {

        //                txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
        //                txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);
        //                int GLCode = Converter.GetInteger(txtGLCashCode.Text);
        //                A2ZCGLMSTDTO get1DTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

        //                if (get1DTO.GLAccNo > 0)
        //                {
        //                    ddlGLCashCode.SelectedValue = Converter.GetString(get1DTO.GLAccNo);
        //                }
        //                else
        //                {
        //                    txtGLCashCode.Text = string.Empty;
        //                    txtGLCashCode.Focus();
        //                }

        //                txtMemNo.Focus();

        //            }
        //            else
        //            {
        //                ddlCreditUNo.SelectedValue = "-Select-";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void txtMemNo_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlMemNo.SelectedValue == "-Select-")
        //        {

        //        }

        //        if (txtMemNo.Text != string.Empty && ddlCreditUNo.SelectedValue != "-Select-")
        //        {

        //            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //            int CNo = Converter.GetSmallInteger(lblCuNo.Text);
        //            int MemNumber = Converter.GetInteger(txtMemNo.Text);

        //            A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

        //            getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

        //            if (getDTO.NoRecord > 0)
        //            {
        //                ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
        //                txtVoucherNo.Focus();
        //            }
        //            else
        //            {
        //                ddlMemNo.SelectedValue = "-Select-";
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //protected void ddlMemNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlMemNo.SelectedValue != "-Select-" && ddlCreditUNo.SelectedValue != "-Select-")
        //    {
        //        Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //        int CNo = Converter.GetSmallInteger(lblCuNo.Text);
        //        int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);

        //        A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

        //        getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

        //        if (getDTO.NoRecord > 0)
        //        {
        //            txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
        //            txtVoucherNo.Focus();
        //        }
        //    }

        //}


        protected void AccGetInfo()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CNo = Converter.GetInteger(lblCuNo.Text);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);


            A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInformation(Acctype, AccNumber, CuType, CNo, MemNumber));

            if (accgetDTO.a == 0)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Account Does Not Exist');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtAccNo.Text = string.Empty;
                txtAccNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Does Not Exist');", true);
                return;
            }
            else
            {
                if (CtrlAccStatus.Text == "98" || CtrlAccStatus.Text == "99")
                {
                    AccStatusMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();

                }
                else
                {
                    CtrlAccAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);
                    CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Text);

                    CtrlPrincipal.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccPrincipal));
                    double Principal = Converter.GetDouble(CtrlPrincipal.Text);

                    CtrlOrgAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccOrgAmt));
                    double OrgAmt = Converter.GetDouble(CtrlOrgAmt.Text);

                    CtrlAvailInterest.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - OrgAmt)));

                    DateTime dt = Converter.GetDateTime(accgetDTO.LastTrnDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlLastTrnDate.Text = date;
                    CtrlTotalDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.TotDepositAmount));
                    CtrlMthDeposit.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.DepositAmount));
                    DateTime dt1 = Converter.GetDateTime(accgetDTO.MatruityDate);
                    string date1 = dt.ToString("dd/MM/yyyy");
                    CtrlMaturityDate.Text = date1;

                    DisplayHistoryInfo();

                    QryTransControl();
                }
            }

        }
        protected void QryTransControl()
        {
            CtrlRow.Text = "1";
            int CRow = 1;
            string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode,TrnLogic FROM A2ZTRNCTRL where TrnCode='" + txtTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Text + "'";
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
                    // trntype = Converter.GetSmallInteger(TranType);
                    CtrlPayType.Text = Converter.GetString(PayType);
                    CtrlTrnType.Text = Converter.GetString(TrnType);
                    CtrlTrnMode.Text = Converter.GetString(TrnMode);
                    CtrlTrnLogic.Text = Converter.GetString(TrnLogic);

                    ReadPayType();
                    ReadTranType();
                    ReadTranMode();
                    ReadTranLogic();


                    CRow = CRow + 1;
                    CtrlRow.Text = Converter.GetString(CRow);
                }

            }

        }
        private void ReadPayType()
        {
            int TypeClass = Converter.GetInteger(lblcls.Text);
            int PayType = Converter.GetInteger(CtrlPayType.Text);
            A2ZPAYTYPEDTO gdto = (A2ZPAYTYPEDTO.GetInformation(TypeClass, PayType));
            if (gdto.record > 0)
            {
                if (CtrlRow.Text == "1")
                {
                    CtrlPayType1.Text = Converter.GetString(CtrlPayType.Text);
                    lblPayDesc1.Text = Converter.GetString(gdto.PayTypeDescription);
                }
                if (CtrlRow.Text == "2")
                {
                    CtrlPayType2.Text = Converter.GetString(CtrlPayType.Text);
                    lblPayDesc2.Text = Converter.GetString(gdto.PayTypeDescription);
                }
                if (CtrlRow.Text == "3")
                {
                    CtrlPayType3.Text = Converter.GetString(CtrlPayType.Text);
                    lblPayDesc3.Text = Converter.GetString(gdto.PayTypeDescription);
                }
            }

        }

        private void ReadTranType()
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
                }

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
        }
        private void ReadTranLogic()
        {
            
            if (CtrlTrnLogic.Text == "3")
            {
                LoanDisbursementAmt();
            }
            
        }
        //private void ShareMinAmt()
        //{
        //    Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);

        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetShareMinAmt(AccType));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
        //        VisibleTranAmt();
        //    }
        //}
        //private void PensionAmount()
        //{
        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CuNo = Converter.GetInteger(lblCuNo.Text);
        //    Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
        //    int AccNumber = Converter.GetInteger(txtAccNo.Text);
        //    int MemNumber = Converter.GetInteger(txtMemNo.Text);
        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetPensionDepositAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
        //        VisibleTranAmt();
        //    }

        //}

        private void LoanDisbursementAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CuNo = Converter.GetInteger(lblCuNo.Text);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetLoanDisbursementAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.NoMsg > 0)
                {
                    AccDisbMSG();
                    txtAccNo.Focus();

                }
                else
                {
                    CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    VisibleTranAmt();
                }
            }
        }
        //private void LoanReturnAmt()
        //{
        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CuNo = Converter.GetInteger(lblCuNo.Text);
        //    Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
        //    int AccNumber = Converter.GetInteger(txtAccNo.Text);
        //    int MemNumber = Converter.GetInteger(txtMemNo.Text);
        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetLoanReturnAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
        //        VisibleTranAmt();
        //    }
        //}
        //private void IntReturnAmt()
        //{
        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CuNo = Converter.GetInteger(lblCuNo.Text);
        //    Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
        //    int AccNumber = Converter.GetInteger(txtAccNo.Text);
        //    int MemNumber = Converter.GetInteger(txtMemNo.Text);
        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetIntReturnAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
        //        VisibleTranAmt();
        //    }
        //}

        //private void IntWithdrawal()
        //{
        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CuNo = Converter.GetInteger(lblCuNo.Text);
        //    Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
        //    int AccNumber = Converter.GetInteger(txtAccNo.Text);
        //    int MemNumber = Converter.GetInteger(txtMemNo.Text);
        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetIntWithdrawal(CuType, CuNo, MemNumber, Acctype, AccNumber));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        if (getDTO.LogicAmount == 0)
        //        {
        //            IntWithdMSG();
        //            txtAccNo.Text = string.Empty;
        //            txtAccNo.Focus();
        //        }
        //        else
        //        {
        //            CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
        //            VisibleTranAmt();
        //        }
        //    }
        //}

        //private void BenefitWithdrawal()
        //{
        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CuNo = Converter.GetInteger(lblCuNo.Text);
        //    Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
        //    int AccNumber = Converter.GetInteger(txtAccNo.Text);
        //    int MemNumber = Converter.GetInteger(txtMemNo.Text);
        //    A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetBenefitWithdrawal(CuType, CuNo, MemNumber, Acctype, AccNumber));
        //    if (getDTO.NoRecord > 0)
        //    {
        //        if (getDTO.LogicAmount == 0)
        //        {
        //            BenefitWithdMSG();
        //            txtAccNo.Text = string.Empty;
        //            txtAccNo.Focus();
        //        }
        //        else
        //        {
        //            CtrlLogicAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
        //            VisibleTranAmt();
        //        }
        //    }
        //}
        private void VisibleTranAmt()
        {
            if (CtrlRow.Text == "1")
            {
                VisibleTrue();
                if (CtrlTrnMode1.Text == "0")
                {
                    txtAmount1.Text = CtrlLogicAmt.Text;

                }
                else
                {
                    txtAmount1.Text = CtrlLogicAmt.Text;

                }
            }

            if (CtrlRow.Text == "2")
            {
                VisibleTrue();
                if (CtrlTrnMode2.Text == "0")
                {
                    txtAmount2.Text = CtrlLogicAmt.Text;

                }
                else
                {
                    txtAmount2.Text = CtrlLogicAmt.Text;

                }
            }

            if (CtrlRow.Text == "3")
            {
                VisibleTrue();
                if (CtrlTrnMode3.Text == "0")
                {
                    txtAmount3.Text = CtrlLogicAmt.Text;

                }
                else
                {
                    txtAmount3.Text = CtrlLogicAmt.Text;

                }
            }
        }
        private void VisibleTrue()
        {
            if (CtrlRow.Text == "1")
            {
                txtTrnType1.Visible = true;
                lblPayDesc1.Visible = true;
                txtAmount1.Visible = true;

            }
            if (CtrlRow.Text == "2")
            {
                txtTrnType2.Visible = true;
                lblPayDesc2.Visible = true;
                txtAmount2.Visible = true;

            }
            if (CtrlRow.Text == "3")
            {
                txtTrnType3.Visible = true;
                lblPayDesc3.Visible = true;
                txtAmount3.Visible = true;

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT AccType,AccNo,PayTypeDes,GLAmount FROM WF_Transaction WHERE TrnFlag !=1 AND UserID='" + hdnID.Text + "'";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }


        protected void SumValue()
        {
            Decimal sumCr = 0;
            Decimal sumDr = 0;

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {

                sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[3].Text));
                //sumDr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[5].Text));

            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            //CtrlTrnDrTotal.Text = Convert.ToString(String.Format("{0:0,0.00}", sumDr));
        }
        protected void txtTrnType1_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType1.Text);
            CtrlTrnType1.Text = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc1.Focus();
            }
        }
        protected void txtTrnType2_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType2.Text);
            CtrlTrnType2.Text = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc2.Focus();
            }
        }

        protected void txtTrnType3_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType3.Text);
            CtrlTrnType3.Text = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc3.Focus();
            }
        }
        protected void AddNormal()
        {
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            CtrlTrnCSGL.Text = "0";
            CtrlTrnFlag.Text = "0";


            if (CtrlRow.Text == "1")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType1.Text + "' and TrnType='" + CtrlTrnType1.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode1.Text == "0")
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount1.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount1.Text;
                    if (CtrlTrnType1.Text == "1")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount1.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount1.Text;
                    if (CtrlTrnType1.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType1.Text + "','" + txtTrnType1.Text + "','" + CtrlTrnMode1.Text + "','" + CtrlPayType1.Text + "','" + lblPayDesc1.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));
                if (rowEffect1 > 0)
                {
                    gvDetail();
                    SumValue();
                }

            }
            if (CtrlRow.Text == "2")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType2.Text + "' and TrnType='" + CtrlTrnType2.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }
                if (CtrlTrnMode2.Text == "0")
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount2.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount2.Text;
                    if (CtrlTrnType2.Text == "1")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount2.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount2.Text;
                    if (CtrlTrnType2.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType2.Text + "','" + txtTrnType2.Text + "','" + CtrlTrnMode2.Text + "','" + CtrlPayType2.Text + "','" + lblPayDesc2.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";
                int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                if (rowEffect2 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Text == "3")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType3.Text + "' and TrnType='" + CtrlTrnType3.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode3.Text == "0")
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount3.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount3.Text;
                    if (CtrlTrnType3.Text == "1")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount3.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount3.Text;
                    if (CtrlTrnType3.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType3.Text + "','" + txtTrnType3.Text + "','" + CtrlTrnMode3.Text + "','" + CtrlPayType3.Text + "','" + lblPayDesc3.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";

                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                if (rowEffect3 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
        }
        protected void AddContra()
        {
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            CtrlTrnCSGL.Text = "0";
            CtrlTrnFlag.Text = "1";


            if (CtrlRow.Text == "1")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType1.Text + "' and TrnType='" + CtrlTrnType1.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode1.Text == "0")
                {
                    if (CtrlTrnType1.Text == "1")
                    {
                        //    CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount1.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount1.Text;

                }
                else
                {
                    if (CtrlTrnType1.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount1.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount1.Text;
                }
                string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType1.Text + "','" + txtTrnType1.Text + "','" + CtrlTrnContraMode1.Text + "','" + CtrlPayType1.Text + "','" + lblPayDesc1.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));
                if (rowEffect1 > 0)
                {
                    gvDetail();
                    SumValue();
                }

            }
            if (CtrlRow.Text == "2")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType2.Text + "' and TrnType='" + CtrlTrnType2.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }
                if (CtrlTrnMode2.Text == "0")
                {
                    if (CtrlTrnType2.Text == "1")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount2.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount2.Text;
                }
                else
                {
                    if (CtrlTrnType2.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount2.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount2.Text;
                }
                string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType2.Text + "','" + txtTrnType2.Text + "','" + CtrlTrnContraMode2.Text + "','" + CtrlPayType2.Text + "','" + lblPayDesc2.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";
                int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                if (rowEffect2 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Text == "3")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType3.Text + "' and TrnType='" + CtrlTrnType3.Text + "' and TrnMode='" + CtrlTrnMode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Text = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Text = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Text = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode3.Text == "0")
                {
                    if (CtrlTrnType3.Text == "1")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoCR.Text);
                    CtrlGLCreditAmt.Text = txtAmount3.Text;
                    CtrlGLDebitAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount3.Text;
                }
                else
                {
                    if (CtrlTrnType3.Text == "1")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Text = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Text = Converter.GetString(CtrlGLAccNoDR.Text);
                    CtrlGLDebitAmt.Text = txtAmount3.Text;
                    CtrlGLCreditAmt.Text = "0";
                    CtrlGLAmount.Text = txtAmount3.Text;
                }
                string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,VoucherNo,AccType,AccNo,FuncOpt,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,UserID)VALUES('" + opdate + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + txtMemNo.Text + "','" + txtVoucherNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + lblFuncOpt.Text + "','" + CtrlTrnType3.Text + "','" + txtTrnType3.Text + "','" + CtrlTrnContraMode3.Text + "','" + CtrlPayType3.Text + "','" + lblPayDesc3.Text + "','" + CtrlGLCreditAmt.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlShowInt.Text + "','" + CtrlGLAccNoDR.Text + "','" + CtrlGLAccNoCR.Text + "','" + CtrlTrnCSGL.Text + "','" + CtrlTrnFlag.Text + "','" + CtrlGLAccNo.Text + "','" + CtrlGLAmount.Text + "','" + CtrlGLDebitAmt.Text + "','" + CtrlGLCreditAmt.Text + "','" + hdnID.Text + "')";

                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                if (rowEffect3 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
        }



        //protected void ClearInfo()
        //{

        //    txtCreditUNo.Text = string.Empty;
        //    ddlCreditUNo.SelectedValue = "-Select-";
        //    txtMemNo.Text = string.Empty;
        //    ddlMemNo.SelectedValue = "-Select-";
        //    txtVoucherNo.Text = string.Empty;
        //    txtAccType.Text = string.Empty;
        //    txtAccNo.Text = string.Empty;
        //    txtTrnCode.Text = string.Empty;
        //    ddlTrnCode.SelectedValue = "-Select-";
        //    ddlAccNo.SelectedValue = "-Select-";
        //    gvDetailInfo.Visible = false;
        //    lblData1.Text = string.Empty;
        //    lblData2.Text = string.Empty;
        //    lblData3.Text = string.Empty;
        //    lblData4.Text = string.Empty;
        //    lblData5.Text = string.Empty;
        //    txtTotalAmt.Text = string.Empty;
        //}

        ////   --------- DISPLAY MASSAGE SCREEN ----------------------- 
        protected void AccDisbMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account Already Disbursed');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Already Disbursed');", true);
            return;

        }

        //protected void InvalidCuNoMSG()
        //{
        //    String csname1 = "PopupScript";
        //    Type cstype = GetType();
        //    ClientScriptManager cs = Page.ClientScript;

        //    if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //    {
        //        String cstext1 = "alert('Invalid Credit Union No.');";
        //        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //    }

        //    return;

        //}
        //protected void IntWithdMSG()
        //{
        //    String csname1 = "PopupScript";
        //    Type cstype = GetType();
        //    ClientScriptManager cs = Page.ClientScript;

        //    if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //    {
        //        String cstext1 = "alert('No Accrued Interest');";
        //        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //    }

        //    return;

        //}

        //protected void BenefitWithdMSG()
        //{
        //    String csname1 = "PopupScript";
        //    Type cstype = GetType();
        //    ClientScriptManager cs = Page.ClientScript;

        //    if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //    {
        //        String cstext1 = "alert('No Benefit Interest');";
        //        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //    }

        //    return;

        //}
        protected void UpdatedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Transaction Sucessfully Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Sucessfully Done');", true);
            return;

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

        protected void AccStatusMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account not Active');", true);
            return;
        }
        protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT Id,VoucherNo FROM A2ZTRANSACTION where VoucherNo='" + txtVoucherNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                DuplicateVchMSG();
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
            }
            else
            {
                txtTrnCode.Focus();
            }

        }
        protected void DisplayHistoryInfo()
        {
            if (CtrlAccAtyClass.Text == "1")
            {
                lblRec1.Text = "Ledger Balance :";
                lblData1.Text = CtrlLadgerBalance.Text;
                lblRec2.Text = "Last Transaction Date :";
                lblData2.Text = CtrlLastTrnDate.Text;
                lblData3.Visible = false;
                lblData4.Visible = false;
                lblData5.Visible = false;
            }
            if (CtrlAccAtyClass.Text == "2" || CtrlAccAtyClass.Text == "3")
            {
                lblRec1.Text = "Orginal Amount :";
                lblData1.Text = CtrlOrgAmt.Text;
                lblRec2.Text = "Ledger Balance :";
                lblData2.Text = CtrlLadgerBalance.Text;
                lblRec3.Text = "Avaiable Interest :";
                lblData3.Text = CtrlAvailInterest.Text;

                lblData4.Visible = false;
                lblData5.Visible = false;
            }
            if (CtrlAccAtyClass.Text == "4")
            {
                lblRec1.Text = "Ledger Balance :";
                lblData1.Text = CtrlLadgerBalance.Text;

                lblRec2.Text = "Last Transaction Date :";
                lblData2.Text = CtrlLastTrnDate.Text;
                lblRec3.Text = "Monthly Deposit Amt. :";
                lblData3.Text = CtrlMthDeposit.Text;
                lblRec5.Text = "Total Deposit Amt. :";
                lblData5.Text = CtrlTotalDeposit.Text;
                lblRec5.Text = "Maturity Date :";
                lblData5.Text = CtrlMaturityDate.Text;
            }
            if (CtrlAccAtyClass.Text == "5")
            {
                lblRec1.Text = "Outstanding Balance :";
                lblData1.Text = CtrlLadgerBalance.Text;

                lblRec2.Text = "Last Transaction Date :";
                lblData2.Text = CtrlLastTrnDate.Text;
                lblData3.Visible = false;
                lblData4.Visible = false;
                lblData5.Visible = false;
            }

        }
        //protected void BtnViewImage_Click(object sender, EventArgs e)
        //{
        //    Session["CuNo"] = lblCuNo.Text;
        //    Session["CuType"] = lblCuType.Text;
        //    Session["MemNo"] = txtMemNo.Text;
        //    Page.ClientScript.RegisterStartupScript(
        //    this.GetType(), "OpenWindow", "window.open('CSGetImage.aspx','_newtab');", true);
        //}

        //protected void txtTrnCode_TextChanged(object sender, EventArgs e)
        //{
        //    if (ddlTrnCode.SelectedValue == "-Select-")
        //    {
        //        txtTrnCode.Focus();

        //    }
        //    try
        //    {

        //        if (txtTrnCode.Text != string.Empty)
        //        {
        //            int MainCode = Converter.GetInteger(txtTrnCode.Text);
        //            A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));

        //            if (getDTO.TrnCode > 0)
        //            {
        //                lblAType.Text = Converter.GetString(getDTO.AccType);
        //                txtTrnCode.Text = Converter.GetString(getDTO.TrnCode);
        //                ddlTrnCode.SelectedValue = Converter.GetString(getDTO.TrnCode);

        //                Int16 AccType = Converter.GetSmallInteger(lblAType.Text);
        //                A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
        //                if (get1DTO.AccTypeCode > 0)
        //                {
        //                    txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
        //                    lblcls.Text = Converter.GetString(get1DTO.AccTypeClass);
        //                }
        //            }
        //            string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
        //            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
        //            if (dt.Rows.Count > 0)
        //            {
        //                AccountNoDropdown();

        //                //txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
        //                txtAccNo.Focus();
        //                //AccGetInfo();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void ddlTrnCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (ddlTrnCode.SelectedValue != "-Select-")
        //        {

        //            int MainCode = Converter.GetInteger(ddlTrnCode.SelectedValue);
        //            A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));
        //            if (getDTO.TrnCode > 0)
        //            {
        //                lblAType.Text = Converter.GetString(getDTO.AccType);
        //                txtTrnCode.Text = Converter.GetString(getDTO.TrnCode);

        //                Int16 AccType = Converter.GetSmallInteger(lblAType.Text);
        //                A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
        //                if (get1DTO.AccTypeCode > 0)
        //                {
        //                    txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
        //                    lblcls.Text = Converter.GetString(get1DTO.AccTypeClass);
        //                }

        //            }

        //            string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
        //            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
        //            if (dt.Rows.Count > 0)
        //            {
        //                AccountNoDropdown();
        //                //txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
        //                txtAccNo.Focus();
        //                //AccGetInfo();
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void txtAccNo_TextChanged(object sender, EventArgs e)
        //{
            
        //    AccGetInfo();
        //}


        //protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtAccNo.Text = Converter.GetString(ddlAccNo.SelectedValue);
        //    AccGetInfo();
        //}
        protected void txtGLCashCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLCashCode.Text != string.Empty)
                {
                    int GLCode = Converter.GetInteger(txtGLCashCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                    }
                    else
                    {
                        txtGLCashCode.Text = string.Empty;
                        txtGLCashCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGLCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLCashCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlGLCashCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtAmount1_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount1.Text);
            txtAmount1.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnAdd.Focus();
        }


        protected void txtAmount2_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount2.Text);
            txtAmount2.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnAdd.Focus();
        }


        protected void txtAmount3_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount3.Text);
            txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnAdd.Focus();
        }

        //protected void txtTrnType1_TextChanged(object sender, EventArgs e)
        //{
        //    int TrType = Converter.GetSmallInteger(txtTrnType1.Text);
        //    CtrlTrnType1.Text = Converter.GetString(TrType);
        //    A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
        //    if (Trndto.record > 0)
        //    {
        //        txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
        //        lblPayDesc1.Focus();
        //    }
        //}
        //protected void txtTrnType2_TextChanged(object sender, EventArgs e)
        //{
        //    int TrType = Converter.GetSmallInteger(txtTrnType2.Text);
        //    CtrlTrnType2.Text = Converter.GetString(TrType);
        //    A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
        //    if (Trndto.record > 0)
        //    {
        //        txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
        //        lblPayDesc2.Focus();
        //    }
        //}

        //protected void txtTrnType3_TextChanged(object sender, EventArgs e)
        //{
        //    int TrType = Converter.GetSmallInteger(txtTrnType3.Text);
        //    CtrlTrnType3.Text = Converter.GetString(TrType);
        //    A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
        //    if (Trndto.record > 0)
        //    {
        //        txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
        //        lblPayDesc3.Focus();
        //    }
        //}




        //  ------------- ADD TRANSACTIONS ------------------------------- 
        protected void BtnAdd_Click(object sender, EventArgs e)
        {


            //            DateTime fDate = new DateTime();
            //            DateTime tDate = new DateTime();
            //            fDate = Convert.ToDateTime(txtfDate.Text);
            //            tDate = Convert.ToDateTime(txteDate.Text);
            //            int noOfDay = tDate.Subtract(fDate).Days;
            //            TimeSpan timespan = Convert.ToDateTime(tDate).Subtract(Convert.ToDateTime(fDate));


            if (txtVoucherNo.Text == "")
            {
                //VchInputMSG();
                txtVoucherNo.Focus();
                return;
            }


            if ((txtAmount1.Text == "" || txtAmount1.Text == "0") && (txtAmount2.Text == "" || txtAmount2.Text == "0") &&
                (txtAmount3.Text == "" || txtAmount3.Text == "0"))
            {
                //VchAmtMSG();
                return;
            }


            int CRow = 0;
            string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode FROM A2ZTRNCTRL where TrnCode='" + txtTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CRow = CRow + 1;
                    CtrlRow.Text = Converter.GetString(CRow);
                   // AddNormal();
                    //AddContra();

                }

                VisibleFalse();
                txtAccType.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtTrnType1.Text = string.Empty;
                txtTrnType2.Text = string.Empty;
                txtTrnType3.Text = string.Empty;
                lblPayDesc1.Text = string.Empty;
                lblPayDesc2.Text = string.Empty;
                lblPayDesc3.Text = string.Empty;
                txtAmount1.Text = string.Empty;
                txtAmount2.Text = string.Empty;
                txtAmount3.Text = string.Empty;

                CtrlLogicAmt.Text = string.Empty;
                txtTrnCode.Text = string.Empty;
                //ddlTrnCode.SelectedValue = "-Select-";
                //ddlAccNo.SelectedValue = "-Select-";
                txtTrnCode.Focus();
                BtnUpdate.Visible = true;

            }
        }

        // ---------------- CANCEL TRANSACTIONS ------------------------
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            VisibleFalse();
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtTrnType1.Text = string.Empty;
            txtTrnType2.Text = string.Empty;
            txtTrnType3.Text = string.Empty;
            lblPayDesc1.Text = string.Empty;
            lblPayDesc2.Text = string.Empty;
            lblPayDesc3.Text = string.Empty;
            txtAmount1.Text = string.Empty;
            txtAmount2.Text = string.Empty;
            txtAmount3.Text = string.Empty;

            CtrlLogicAmt.Text = string.Empty;
            txtTrnCode.Text = string.Empty;
            //ddlTrnCode.SelectedValue = "-Select-";
           // ddlAccNo.SelectedValue = "-Select-";
            txtTrnCode.Focus();
        }



        //  -------------- UPDATE TRANSACTIONS ------------------------------
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            var prm = new object[1];

            prm[0] = hdnID.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddTransaction", prm, "A2ZCSMCUS"));

            if (result == 0)
            {
               // UpdatedMSG();
              //  ClearInfo();
                txtTotalAmt.Text = string.Empty;
                lblTotalAmt.Visible = false;
                BtnUpdate.Visible = false;
                txtCreditUNo.Focus();
            }
        }

        // ------------ EXIT PROGRAMS ---------------------------
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            if (txtTotalAmt.Text != "0" && txtTotalAmt.Text != "")
            {
               // RecordsAddedMSG();
            }
            else
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('CSDailyReverseTransaction.aspx','_newtab');", true);
        }

        protected void txtLoanAppNo_TextChanged(object sender, EventArgs e)
        {
            if (txtLoanAppNo.Text != string.Empty)
            {
                A2ZLOANDTO getDTO = new A2ZLOANDTO();

                Int16 AppNumber = Converter.GetSmallInteger(txtLoanAppNo.Text);
                getDTO = (A2ZLOANDTO.GetInformation(AppNumber));

                if (getDTO.LoanApplicationNo > 0)
                {

                    txtCreditUNo.Text = Converter.GetString(getDTO.CuNo);
                    int MemNo = Converter.GetInteger(getDTO.LoanMemberNo);
                    txtAccType.Text = Converter.GetString(getDTO.LoanAccountType);
                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(re);
          
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(re1);

                    string sqlquery = "SELECT CuName,GLCashCode from A2ZCUNION WHERE CuStatus !='9' and CuNo='" + CNo + "' and CuType='"+CuType+"'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");

                    if(dt.Rows.Count>0)
                    {
                        
                        lblCUName.Text = Converter.GetString(dt.Rows[0]["CuName"]);
                        txtGLCashCode.Text = Converter.GetString(dt.Rows[0]["GLCashCode"]);
                        string sqlquery1 = "SELECT MemName from A2ZMEMBER WHERE  CuNo='" + CNo + "' and CuType='" + CuType + "' and MemNo='"+MemNo+"'";
                        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZCSMCUS");
                        if(dt1.Rows.Count>0)
                        {
                            txtMemNo.Text = Converter.GetString(MemNo);
                            lblMemName.Text = Converter.GetString(dt1.Rows[0]["MemName"]);
                           
                        }

                    }
                
                    int GLCode = Converter.GetInteger(txtGLCashCode.Text);
                    A2ZCGLMSTDTO get1DTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (get1DTO.GLAccNo > 0)
                    {
                        ddlGLCashCode.SelectedValue = Converter.GetString(get1DTO.GLAccNo);
                    }
                    else
                    {
                        txtGLCashCode.Text = string.Empty;
                        txtGLCashCode.Focus();
                    }
                    string sqlquery2 = "SELECT TrnCode,TrnDes from A2ZTRNCODE WHERE AccType='"+txtAccType.Text+"'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery2, "A2ZCSMCUS");
                    if(dt2.Rows.Count>0)
                    {
                        txtTrnCode.Text = Converter.GetString(dt2.Rows[0]["TrnCode"]);
                        lblTrnCodeName.Text = Converter.GetString(dt2.Rows[0]["TrnDes"]);
                    }

                    string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + CuType + "' and CuNo='" + CNo + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt3.Rows.Count > 0)
                    {

                        txtAccNo.Text = Converter.GetString(dt3.Rows[0]["AccNo"]);
                        AccGetInfo();
                    }
                }

           
            }
        }




    }
}