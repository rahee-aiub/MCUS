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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ATOZWEBMCUS.Pages
{
    public partial class GLDailyTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (IsPostBack)
                {

                }
                else
                {
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));



                    string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + hdnCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        lblBoothNo.Text = hdnCashCode.Text;
                        lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                    }



                    CtrlPrmValue.Text = Request.QueryString["a%b"];
                    string b = CtrlPrmValue.Text;
                    CtrlTrnType.Text = b.Substring(0, 1);
                    CtrlTrnMode.Text = b.Substring(1, 1);
                    CtrlModule.Text = b.Substring(2, 1);

                    FunctionName();
                    BtnUpdate.Visible = false;


                    lblContra.Visible = false;
                    txtContra.Visible = false;
                    ddlContra.Visible = false;

                    lblChqNo.Visible = false;
                    txtChqNo.Visible = false;

                    lblGLBankCode.Visible = false;
                    txtGLBankCode.Visible = false;
                    ddlGLBankCode.Visible = false;
                    lblBalance.Visible = false;
                    txtBalance.Visible = false;




                    if (CtrlTrnType.Text == "1")
                    {
                        lblGLCode.Text = hdnCashCode.Text;
                        GetGLAccBalance();
                        txtBalance.ReadOnly = true;

                        txtVchNo.Focus();
                    }
                    else
                    {
                        lblTrnType.Visible = false;
                        ddlTrnType.Visible = false;


                        lblContra.Visible = true;
                        txtContra.Visible = true;
                        ddlContra.Visible = true;

                        lblContra.Text = "Debit Code :";
                        lblAccount.Text = "Credit Code :";
                        txtVchNo.Focus();
                    }

                    HeaderDropdown();

                    //string sqlQueryAcc = "SELECT GLAccNo,GLAccDesc FROM dbo.A2ZCGLMST WHERE GLPrtPos = 6";
                    //ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlQueryAcc, ddlTrnsactionCode, "A2ZGLMCUS");

                    //string sqlQueryContra = "SELECT GLAccNo,GLAccDesc FROM dbo.A2ZCGLMST WHERE GLPrtPos = 6";
                    //ddlContra = CommonManager.Instance.FillDropDownList(sqlQueryContra, ddlContra, "A2ZGLMCUS");

                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtVchDate.Text = date;
                    CtrlProcDate.Text = date;

                    string strQuery1 = @"DELETE FROM dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Text + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }
        }


        protected void GLBankCodeDropdown()
        {

            //string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";

            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10106000";

            ddlGLBankCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLBankCode, "A2ZGLMCUS");

        }

        protected void HeaderDropdown()
        {
            hdnContraHead1.Text = "0";
            hdnContraHead2.Text = "0";
            hdnContraHead3.Text = "0";
            hdnContraHead4.Text = "0";

            hdnTranHead1.Text = "0";
            hdnTranHead2.Text = "0";
            hdnTranHead3.Text = "0";
            hdnTranHead4.Text = "0";

            string sqlQueryAcc = @"SELECT GLHead, LTRIM (GLHeadDesc) as GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC";
            ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlQueryAcc, ddlTrnsactionCode, "A2ZGLMCUS");
            ddlTrnsactionCode.SelectedItem.Text = "-Select Header Code-";

            string sqlQueryContra = @"SELECT GLHead, LTRIM (GLHeadDesc) as GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC ";
            ddlContra = CommonManager.Instance.FillDropDownList(sqlQueryContra, ddlContra, "A2ZGLMCUS");
            ddlContra.SelectedItem.Text = "-Select Header Code-";


            hdnContraHead1.Text = "1";
            hdnTranHead1.Text = "1";

            btnBack1.Visible = false;
            btnBack2.Visible = false;
        }
        protected void FunctionName()
        {
            if (CtrlTrnMode.Text == "0")
            {
                lblTransFunction.Text = "GL Daily Cash In Transaction";
            }
            if (CtrlTrnMode.Text == "1")
            {
                lblTransFunction.Text = "GL Daily Cash Out Transaction";
            }
            if (CtrlTrnMode.Text == "2")
            {
                lblTransFunction.Text = "GL Daily Transfer Transaction";
            }
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
        protected void SumValue()
        {
            Decimal sumCr = 0;
            lbltotAmt.Text = "0";
            //Decimal sumDr = 0;

            for (int i = 0; i < gvDebitInfo.Rows.Count; ++i)
            {
                sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDebitInfo.Rows[i].Cells[3].Text));
            }
            lbltotAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SumValue();

            if (lbltotAmt.Text != "00.00" && lbltotAmt.Text != "")
            {
                RecordsAddedMSG();
            }
            else
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
        }



        //protected void txtDrAmount_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtDrAmount.Text);
        //    txtDrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //}

        //protected void txtCrAmount_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtCrAmount.Text);
        //    txtCrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //}

        protected void TotalAmount()
        {
            CtrlTotAmount.Text = string.Empty;

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(Abs(GLAmount)) AS 'Amount' FROM WFGLTrannsaction WHERE TrnFlag=0 AND UserID='" + hdnID.Text + "'", "A2ZGLMCUS");


            if (dt.Rows.Count > 0)
            {
                CtrlTotAmount.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["Amount"]));
            }

        }


        protected void TrnLimitValidity()
        {
            try
            {

                double TotalAmt = 0;
                CtrlProcStat.Text = "0";

                TotalAmount();

                TotalAmt = Converter.GetDouble(CtrlTotAmount.Text);

                if (CtrlTrnMode.Text == "0" || CtrlTrnMode.Text == "1")
                {
                    lblTrnMode.Text = CtrlTrnMode.Text;
                }

                if (CtrlTrnMode.Text == "2")
                {
                    lblTrnMode.Text = "1";
                }



                int Ids = Converter.GetInteger(hdnID.Text);
                A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

                if (getDTO.IdsNo > 0)
                {
                    if (lblTrnMode.Text == "0" && CtrlTrnType.Text == "1")
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

                    if (lblTrnMode.Text == "1" && CtrlTrnType.Text == "1")
                    {
                        if (TotalAmt > getDTO.LIdsCashDebit)
                        {
                            CtrlProcStat.Text = "1";
                        }
                        else
                        {
                            CtrlProcStat.Text = "0";
                        }
                    }

                    if (lblTrnMode.Text == "0" && CtrlTrnType.Text == "3")
                    {
                        if (TotalAmt > getDTO.LIdsTrfDebit)
                        {
                            CtrlProcStat.Text = "1";
                        }
                        else
                        {
                            CtrlProcStat.Text = "0";
                        }
                    }

                    if (lblTrnMode.Text == "1" && CtrlTrnType.Text == "3")
                    {
                        if (TotalAmt > getDTO.LIdsTrfCredit)
                        {
                            CtrlProcStat.Text = "1";
                        }
                        else
                        {
                            CtrlProcStat.Text = "0";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnLimitValidity Problem');</script>");
                //throw ex;
            }
        }

        protected void AccessCashAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Insufficent Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Cash Balance');", true);
            return;

        }

        protected void AccessBankAmountMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Bank Balance');", true);
            return;
        }
        protected void BankGLAccBalance()
        {

            ErrMsg.Text = "0";
            int Code = 0;

            SumValue();

            var prm = new object[4];

            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            string Tdate = date.ToString("dd/MM/yyyy");

            if (CtrlTrnMode.Text == "0" && hdnGLSubHead2.Text == "10106000")
            {
                Code = Converter.GetInteger(txtTrnsactionCode.Text);
                MsgFlag.Text = "2";
            }

            //if (CtrlTrnMode.Text == "1" && ddlTrnType.SelectedValue == "1")
            //{
            //    Code = Converter.GetInteger(hdnCashCode.Text);
            //    MsgFlag.Text = "1";
            //}

            //if (CtrlTrnMode.Text == "1" && ddlTrnType.SelectedValue == "2")
            //{
            //    Code = Converter.GetInteger(txtGLBankCode.Text);
            //    MsgFlag.Text = "2";
            //}

            if (CtrlTrnMode.Text == "2" && hdnGLSubHead2.Text == "10106000")
            {
                Code = Converter.GetInteger(txtTrnsactionCode.Text);
                MsgFlag.Text = "2";
            }

            if (Code == 0)
            {
                return;
            }

            int UId = Converter.GetInteger(hdnID.Text);

            prm[0] = Code;
            prm[1] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[2] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[3] = UId;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalanceSingle", prm, "A2ZGLMCUS"));

            A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);

            if (glObj.GLAccNo > 0)
            {

                lblGLAccBal.Text = Converter.GetString(String.Format("{0:0,0.00}", glObj.GLClBal));
                lblGLBalanceType.Text = Converter.GetString(glObj.GLBalanceType);
                double AvailAmt = Converter.GetDouble(lblGLAccBal.Text);
                double InputAmt = Converter.GetDouble(txtAmount.Text);

                if (lblGLBalanceType.Text != "1" && InputAmt > AvailAmt)
                {
                    ErrMsg.Text = "1";
                    if (MsgFlag.Text == "1")
                    {
                        AccessCashAmountMSG();
                    }
                    if (MsgFlag.Text == "2")
                    {
                        AccessBankAmountMSG();
                    }
                    return;
                }

            }

        }


        protected void CashGLAccBalance()
        {

            ErrMsg.Text = "0";
            int Code = 0;

            SumValue();

            var prm = new object[4];

            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            string Tdate = date.ToString("dd/MM/yyyy");

            //if (CtrlTrnMode.Text == "0" && hdnGLSubHead2.Text == "10106000")
            //{
            //    Code = Converter.GetInteger(txtTrnsactionCode.Text);
            //    MsgFlag.Text = "2";
            //}

            if (CtrlTrnMode.Text == "1" && ddlTrnType.SelectedValue == "1")
            {
                Code = Converter.GetInteger(hdnCashCode.Text);
                MsgFlag.Text = "1";
            }

            if (CtrlTrnMode.Text == "1" && ddlTrnType.SelectedValue == "2")
            {
                Code = Converter.GetInteger(txtGLBankCode.Text);
                MsgFlag.Text = "2";
            }

            //if (CtrlTrnMode.Text == "2" && hdnGLSubHead2.Text == "10106000")
            //{
            //    Code = Converter.GetInteger(txtTrnsactionCode.Text);
            //    MsgFlag.Text = "2";
            //}


            if (Code == 0)
            {
                return;
            }

            int UId = Converter.GetInteger(hdnID.Text);

            prm[0] = Code;
            prm[1] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[2] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[3] = UId;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalanceSingle", prm, "A2ZGLMCUS"));

            A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);

            if (glObj.GLAccNo > 0)
            {

                lblGLAccBal.Text = Converter.GetString(String.Format("{0:0,0.00}", glObj.GLClBal));
                lblGLBalanceType.Text = Converter.GetString(glObj.GLBalanceType);
                double AvailAmt = Converter.GetDouble(lblGLAccBal.Text);
                double InputAmt = Converter.GetDouble(lbltotAmt.Text);

                if (lblGLBalanceType.Text != "1" && InputAmt > AvailAmt)
                {
                    ErrMsg.Text = "1";
                    if (MsgFlag.Text == "1")
                    {
                        AccessCashAmountMSG();
                    }
                    if (MsgFlag.Text == "2")
                    {
                        AccessBankAmountMSG();
                    }
                    return;
                }

            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                CashGLAccBalance();
                if (ErrMsg.Text == "1")
                {
                    ClearScreen();
                    return;
                }

                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(2);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVchNo.Text = "G" + GLCode + "-" + getDTO.RecLastNo;

                TrnLimitValidity();

                var prm = new object[3];
                prm[0] = hdnID.Text;
                prm[1] = CtrlVchNo.Text;
                prm[2] = CtrlProcStat.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlUpdateTransaction", prm, "A2ZGLMCUS"));
                if (result == 0)
                {
                    if (CtrlTrnType.Text == "1")
                    {
                        txtVchNo.Focus();
                    }
                    else
                    {
                        lblContra.Text = "Debit Code :";
                        lblAccount.Text = "Credit Code :";
                        txtVchNo.Focus();
                    }

                    UpdatedMSG();

                    PrintTrnVoucher();

                    clearInfo();

                    BtnUpdate.Visible = false;

                    txtContra.ReadOnly = false;
                    txtTrnsactionCode.ReadOnly = false;

                    txtVchDate.Text = CtrlProcDate.Text;

                    //string strQuery1 = "DELETE FROM WFGLTrannsaction" WHERE UserID='" + hdnID.Value + "'";

                    string strQuery1 = @"DELETE dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Text + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

                    UnPostValue();
                    gvDebit();
                    UpdateBackUpStat();

                    lbltotAmt.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ClearScreen()
        {
            clearInfo();

            BtnUpdate.Visible = false;

            txtContra.ReadOnly = false;
            txtTrnsactionCode.ReadOnly = false;

            txtVchDate.Text = CtrlProcDate.Text;

            //string strQuery1 = "DELETE FROM WFGLTrannsaction" WHERE UserID='" + hdnID.Value + "'";

            string strQuery1 = @"DELETE dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Text + "'";
            int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));


            gvDebit();


            lbltotAmt.Text = string.Empty;

        }


        protected void PrintTrnVoucher()
        {
            try
            {
                if (CtrlTrnMode.Text == "0")
                {
                    lblTrnTypeTitle.Text = "CASH - In";
                }
                else if (CtrlTrnMode.Text == "1")
                {
                    lblTrnTypeTitle.Text = "CASH - Out";
                }
                else
                {
                    lblTrnTypeTitle.Text = "TRANSFER";
                }


                DateTime Pdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                Int32 CSGL = Converter.GetSmallInteger(1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, hdnID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionVch");

                Response.Redirect("ReportServer.aspx", false);

                Session["RTranDate"] = txtVchDate.Text;
                //Session["RFuncOpt"] = lblFuncOpt.Text;
                Session["RModule"] = CtrlModule.Text;
                Session["flag"] = "2";

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
        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            if (CtrlProcStat.Text == "0")
            {
                a = "  GL Transaction Sucessfully Done";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "   GL Transaction Input Done";
            }
            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


            //------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (CtrlProcStat.Text == "0")
            //{
            //    a = "  GL Transaction Sucessfully Done";
            //}
            //if (CtrlProcStat.Text == "1")
            //{
            //    a = "   GL Transaction Input Done";
            //}
            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVchNo.Text);

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


        protected void gvDebit()
        {
            string sqlquery3 = "SELECT Id, GLAccNo,TrnDesc,Abs(GLAmount) as GLAmount,TrnType,TrnDrCr FROM WFGLTrannsaction WHERE TrnFlag=0 AND UserID='" + hdnID.Text + "'";

            gvDebitInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDebitInfo, "A2ZGLMCUS");
        }



        protected void txtTrnsactionCode_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtTrnsactionCode.Text == txtContra.Text)
                {
                    DuplicateGLCode();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                    return;
                }

                //ddlTrnsactionCode.SelectedValue = Converter.GetString(txtTrnsactionCode.Text);

                int Code = Converter.GetInteger(txtTrnsactionCode.Text);

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    hdnGLSubHead2.Text = Converter.GetString(glObj.GLSubHead);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                    }

                    if (hdnGLSubHead2.Text == "10101000")
                    {
                        InvalidGLCode();
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                        return;
                    }
                    else
                    {
                        txtTrnsactionCode.Text = Converter.GetString(glObj.GLAccNo);
                        CtrlAccType.Text = Converter.GetString(glObj.GLAccType);

                        hdnTranHead1.Text = "1";
                        hdnTranHead2.Text = "0";
                        hdnTranHead3.Text = "0";
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtTrnsactionCode.Text);
                        TranCodeDropDown();


                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = "0";
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtTrnsactionCode.Text);
                        TranCodeDropDown();

                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = hdnTranCode.Text;
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtTrnsactionCode.Text);
                        TranCodeDropDown();

                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = hdnTranCode.Text;
                        hdnTranHead4.Text = hdnTranCode.Text;

                        ddlTrnsactionCode.SelectedValue = Converter.GetString(txtTrnsactionCode.Text);
                        TranCodeDropDown();


                        //ddlContra.SelectedValue = Converter.GetString(glObj.GLAccNo);
                        ddlTrnsactionCode_SelectedIndexChanged(this, EventArgs.Empty);

                        txtDescription.Focus();
                    }
                }
                else
                {
                    Validity();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnsactionCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }


        private void DuplicateGLCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Duplicate GL Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Duplicate GL Code');", true);
            return;
        }


        private void Validity()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('GL Code - Does Not Exists  ');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Code - Does Not Exists');", true);
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

        private void InvalidGLBankCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid GL Bank Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Bank Code');", true);
            return;
        }

        private void InvalidGLCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid GL Bank Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
            return;
        }

        private void ContraDropDown()
        {
            try
            {
                if (hdnContraHead1.Text == "0")
                {
                    btnBack1.Visible = false;
                }
                else
                {
                    btnBack1.Visible = true;
                }


                if (hdnContraHead1.Text == "0")
                {
                    hdnContraHead1.Text = Converter.GetString(hdnContraCode.Text);
                    string sqlQueryContra = @"SELECT GLHead, GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC";
                    ddlContra = CommonManager.Instance.FillDropDownList(sqlQueryContra, ddlContra, "A2ZGLMCUS");
                    ddlContra.SelectedItem.Text = "-Select Header Code-";
                    //txtContra.Text = string.Empty;

                }
                else if (hdnContraHead2.Text == "0")
                {
                    hdnContraHead2.Text = Converter.GetString(hdnContraCode.Text);
                    string input2 = hdnContraCode.Text;
                    string sub3 = input2.Substring(0, 1);
                    string sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 1 AND GLAccType = '" + sub3 + "' GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC";
                    ddlContra = CommonManager.Instance.FillDropDownList(sqlquery1, ddlContra, "A2ZGLMCUS");
                    ddlContra.SelectedItem.Text = "-Select Main Head Code-";
                    //txtContra.Text = string.Empty;


                }
                else if (hdnContraHead3.Text == "0")
                {
                    string sqlquery1;

                    hdnContraHead3.Text = Converter.GetString(hdnContraCode.Text);
                    string inputMHead = hdnContraCode.Text;
                    string subMHead = hdnContraCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 3);

                    //if (CtrlTrnType.Text == "1")
                    //{
                    //    sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC ";
                    //}
                    //else
                    //{
                    sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' AND GLSubHead != 10101000 GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC ";
                    //}
                    ddlContra = CommonManager.Instance.FillDropDownList(sqlquery1, ddlContra, "A2ZGLMCUS");
                    ddlContra.SelectedItem.Text = "-Select Sub Head Code-";
                    //txtContra.Text = string.Empty;

                }
                else if (hdnContraHead4.Text == "0")
                {
                    hdnContraHead4.Text = Converter.GetString(hdnContraCode.Text);
                    string inputMHead = hdnContraCode.Text;
                    string subMHead = hdnContraCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 5);
                    string sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 6 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,5)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC ";
                    ddlContra = CommonManager.Instance.FillDropDownList(sqlquery1, ddlContra, "A2ZGLMCUS");
                    ddlContra.SelectedItem.Text = "-Select Details Code-";
                    //txtContra.Text = string.Empty;


                }
                else
                {
                    txtContra.Text = Converter.GetString(ddlContra.SelectedValue);

                    int Code = Converter.GetInteger(ddlContra.SelectedValue);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);
                    if (glObj.GLAccNo > 0)
                    {

                        CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                        CtrlContraAType.Text = Converter.GetString(glObj.GLAccType);
                        hdnGLSubHead1.Text = Converter.GetString(glObj.GLSubHead);
                        txtTrnsactionCode.Focus();
                        if (CtrlRecType.Text != "2")
                        {
                            InvalidGlCode();
                            txtContra.Text = string.Empty;
                            txtContra.Focus();
                        }
                    }
                    else
                    {
                        Validity();
                        txtContra.Text = string.Empty;
                        txtContra.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlContra_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }
        protected void ddlContra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContra.SelectedValue == "-Select-")
            {
                //ClearInfoAdd();
                txtContra.Text = string.Empty;
                return;
            }


            hdnContraCode.Text = Converter.GetString(ddlContra.SelectedValue);

            ContraDropDown();
        }

        private void TranCodeDropDown()
        {
            try
            {
                if (hdnTranHead1.Text == "0")
                {
                    btnBack2.Visible = false;
                }
                else
                {
                    btnBack2.Visible = true;
                }


                if (hdnTranHead1.Text == "0")
                {
                    hdnTranHead1.Text = Converter.GetString(hdnTranCode.Text);
                    string sqlQueryTran = @"SELECT GLHead, LTRIM (GLHeadDesc) as GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC ";
                    ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlQueryTran, ddlTrnsactionCode, "A2ZGLMCUS");
                    ddlTrnsactionCode.SelectedItem.Text = "-Select Header Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead2.Text == "0")
                {
                    hdnTranHead2.Text = Converter.GetString(hdnTranCode.Text);
                    string input2 = hdnTranCode.Text;
                    string sub3 = input2.Substring(0, 1);
                    string sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 1 AND GLAccType = '" + sub3 + "' GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC ";
                    ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlTrnsactionCode, "A2ZGLMCUS");
                    ddlTrnsactionCode.SelectedItem.Text = "-Select Main Head Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead3.Text == "0")
                {
                    string sqlquery1;
                    hdnTranHead3.Text = Converter.GetString(hdnTranCode.Text);
                    string inputMHead = hdnTranCode.Text;
                    string subMHead = hdnTranCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 3);

                    //if (CtrlTrnType.Text == "1")
                    //{
                    //    sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc Order By GLAccDesc ASC ";
                    //}
                    //else
                    //{
                    sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' AND GLSubHead != 10101000 GROUP BY GLAccNo,GLAccDesc Order By GLAccDesc ASC ";
                    //}
                    ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlTrnsactionCode, "A2ZGLMCUS");
                    ddlTrnsactionCode.SelectedItem.Text = "-Select Sub Head Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead4.Text == "0")
                {
                    hdnTranHead4.Text = Converter.GetString(hdnTranCode.Text);
                    string inputMHead = hdnTranCode.Text;
                    string subMHead = hdnTranCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 5);
                    string sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 6 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,5)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc Order By GLAccDesc ASC";
                    ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlTrnsactionCode, "A2ZGLMCUS");
                    ddlTrnsactionCode.SelectedItem.Text = "-Select Details Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else
                {
                    txtTrnsactionCode.Text = Converter.GetString(ddlTrnsactionCode.SelectedValue);

                    int Code = Converter.GetInteger(ddlTrnsactionCode.SelectedValue);

                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);
                    if (glObj.GLAccNo > 0)
                    {

                        CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                        CtrlAccType.Text = Converter.GetString(glObj.GLAccType);
                        hdnGLSubHead2.Text = Converter.GetString(glObj.GLSubHead);

                        txtDescription.Focus();
                        if (CtrlRecType.Text != "2")
                        {
                            InvalidGlCode();
                            txtTrnsactionCode.Text = string.Empty;
                            txtTrnsactionCode.Focus();
                            return;
                        }
                        else
                        {
                            //if (CtrlTrnType.Text == "3" && ((hdnGLSubHead1.Text != "10106000" && hdnGLSubHead2.Text == "10106000") ||
                            //    (hdnGLSubHead1.Text == "10106000" && hdnGLSubHead2.Text != "10106000")))
                            //{
                            //    InvalidGLCode();
                            //    txtTrnsactionCode.Text = string.Empty;
                            //    txtTrnsactionCode.Focus();
                            //    return;
                            //}
                        }
                    }
                    else
                    {
                        Validity();
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TranCodeDropDown Problem');</script>");
                //throw ex;
            }
        }


        protected void ddlTrnsactionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlTrnsactionCode.SelectedValue == "-Select-")
                {
                    txtTrnsactionCode.Text = string.Empty;
                    return;
                }


                hdnTranCode.Text = Converter.GetString(ddlTrnsactionCode.SelectedValue);

                TranCodeDropDown();




                //txtDescription.Focus();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlContra_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }


        protected void txtContra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //ddlContra.SelectedValue = Converter.GetString(txtContra.Text);

                int Code = Converter.GetInteger(txtContra.Text);

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    hdnGLSubHead1.Text = Converter.GetString(glObj.GLSubHead);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtContra.Text = string.Empty;
                        txtContra.Focus();
                    }


                    if (hdnGLSubHead1.Text == "10101000")
                    {
                        InvalidGLCode();
                        txtContra.Text = string.Empty;
                        txtContra.Focus();
                        return;
                    }
                    else
                    {
                        txtContra.Text = Converter.GetString(glObj.GLAccNo);
                        CtrlContraAType.Text = Converter.GetString(glObj.GLAccType);

                        hdnContraHead1.Text = "1";
                        hdnContraHead2.Text = "0";
                        hdnContraHead3.Text = "0";
                        hdnContraHead4.Text = "0";

                        hdnContraCode.Text = Converter.GetString(txtContra.Text);
                        ContraDropDown();


                        hdnContraHead2.Text = hdnContraCode.Text;
                        hdnContraHead3.Text = "0";
                        hdnContraHead4.Text = "0";

                        hdnContraCode.Text = Converter.GetString(txtContra.Text);
                        ContraDropDown();

                        hdnContraHead2.Text = hdnContraCode.Text;
                        hdnContraHead3.Text = hdnContraCode.Text;
                        hdnContraHead4.Text = "0";

                        hdnContraCode.Text = Converter.GetString(txtContra.Text);
                        ContraDropDown();

                        hdnContraHead2.Text = hdnContraCode.Text;
                        hdnContraHead3.Text = hdnContraCode.Text;
                        hdnContraHead4.Text = hdnContraCode.Text;

                        ddlContra.SelectedValue = Converter.GetString(txtContra.Text);
                        ContraDropDown();


                        //ddlContra.SelectedValue = Converter.GetString(glObj.GLAccNo);
                        ddlContra_SelectedIndexChanged(this, EventArgs.Empty);
                        txtTrnsactionCode.Focus();
                    }
                }
                else
                {

                    Validity();
                    txtContra.Text = string.Empty;
                    txtContra.Focus();
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtContra_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void clearInfo()
        {

            HeaderDropdown();

            txtAmount.Text = string.Empty;
            txtVchNo.Text = string.Empty;
            //txtVchDate.Text = string.Empty;

            txtTrnsactionCode.Text = string.Empty;
            //ddlTrnsactionCode.SelectedValue = "-Select-";
            txtDescription.Text = string.Empty;

            //ddlContra.SelectedValue = "-Select-";
            txtContra.Enabled = true;
            txtContra.Text = string.Empty;
            ddlContra.Enabled = true;
            gvDebitInfo.Visible = false;
            if (CtrlTrnType.Text == "3")
            {
                //ddlTrnMode.SelectedIndex = 0;
            }



        }

        protected void UnPostValue()
        {
            txtHoldCredit.Text = string.Empty;
            txtHoldDebit.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (CtrlModule.Text == "2")
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLDebitAmt) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "'", "A2ZCSMCUS");

                if (dt.Rows.Count > 0)
                {
                    txtHoldDebit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountDr"]));
                }
            }
            else
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLDebitAmt) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "' AND FromCashCode='" + hdnCashCode.Text + "'", "A2ZCSMCUS");

                if (dt.Rows.Count > 0)
                {
                    txtHoldDebit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountDr"]));
                }

            }
            if (CtrlModule.Text == "2")
            {
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLCreditAmt) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    txtHoldCredit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountCr"]));
                }
            }
            else
            {
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLCreditAmt) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "' AND FromCashCode='" + hdnCashCode.Text + "'", "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    txtHoldCredit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountCr"]));
                }
            }
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

        protected void InvalidGLCodeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Transaction Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction Code');", true);
            return;

        }

        protected void InvalidGLContraMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Contra Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Contra Code');", true);
            return;

        }

        protected void InvalidGLDescMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher Description');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher Description');", true);
            return;

        }

        protected void InvalidGLAmtMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher Amount');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher Amount');", true);
            return;

        }

        protected void InvalidVchNoMSG()
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


        protected void TrnVchDeplicate()
        {
            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                DuplicateVchMSG();
                return;
            }
        }


        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                TrnVchDeplicate();

                if (txtVchNo.Text == string.Empty)
                {
                    InvalidVchNoMSG();
                    return;
                }
                if (txtContra.Text == string.Empty && CtrlTrnType.Text != "1")
                {
                    InvalidGLContraMSG();
                    return;
                }
                if (txtTrnsactionCode.Text == string.Empty)
                {
                    InvalidGLCodeMSG();
                    return;
                }

                if (txtDescription.Text == string.Empty)
                {
                    InvalidGLDescMSG();
                    return;
                }

                if ((txtAmount.Text == "00.00" || txtAmount.Text == string.Empty))
                {
                    InvalidGLAmtMSG();
                    return;
                }



                if (CtrlTrnType.Text == "3" && ((hdnGLSubHead1.Text == "10101000") ||
                            (hdnGLSubHead1.Text != "10106000" && hdnGLSubHead2.Text == "10106000") ||
                            (hdnGLSubHead1.Text == "10106000" && hdnGLSubHead2.Text != "10106000")))
                {
                    InvalidGLCode();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                    return;
                }

                if ((CtrlTrnMode.Text == "0" || CtrlTrnMode.Text == "2") && hdnGLSubHead2.Text == "10106000")
                {
                    BankGLAccBalance();
                    if (ErrMsg.Text == "1")
                    {
                        ClearScreen();
                        return;
                    }
                }


                gvDebitInfo.Visible = true;

                BtnUpdate.Visible = true;

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                lblTrnMode.Text = CtrlTrnMode.Text;


                if (CtrlTrnType.Text == "1")
                {
                    if (ddlTrnType.SelectedValue == "1")
                    {
                        txtContra.Text = hdnCashCode.Text;
                        CtrlContraAType.Text = "1";
                    }
                    else
                    {
                        txtContra.Text = txtGLBankCode.Text;
                        CtrlContraAType.Text = "1";
                    }
                }

                if (lblTrnMode.Text == "2")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm = new object[22];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtContra.Text;
                    prm[5] = Converter.GetDouble(txtAmount.Text);
                    prm[6] = 0;
                    prm[7] = txtDescription.Text;
                    prm[8] = 1;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 0;
                    prm[11] = txtContra.Text;
                    prm[12] = txtTrnsactionCode.Text;
                    prm[13] = 0;

                    if (CtrlContraAType.Text == "2" || CtrlContraAType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm[15] = 1;
                    prm[16] = hdnCashCode.Text;

                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Text;
                    prm[19] = valuedate;
                    prm[20] = lblTrnMode.Text;
                    prm[21] = "";

                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm2 = new object[22];

                    prm2[0] = opdate1;
                    prm2[1] = 0;
                    prm2[2] = txtVchNo.Text;
                    prm2[3] = 0;
                    prm2[4] = txtTrnsactionCode.Text;
                    prm2[5] = 0;
                    prm2[6] = Converter.GetDouble(txtAmount.Text);
                    prm2[7] = txtDescription.Text;
                    prm2[8] = 0;
                    prm2[9] = CtrlTrnType.Text;
                    prm2[10] = 1;
                    prm2[11] = txtContra.Text;
                    prm2[12] = txtTrnsactionCode.Text;
                    prm2[13] = 0;

                    if (CtrlAccType.Text == "1" || CtrlAccType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm2[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm2[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm2[15] = 1;
                    prm2[16] = hdnCashCode.Text;
                    prm2[17] = CtrlModule.Text;
                    prm2[18] = hdnID.Text;
                    prm2[19] = valuedate1;
                    prm2[20] = lblTrnMode.Text;
                    prm2[21] = "";



                    int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm2, "A2ZGLMCUS"));

                    if (result2 == 0)
                    {
                        //txtTrnsactionCode.Text = string.Empty;
                        //txtTrnsactionCode.Focus();
                        //ddlTrnsactionCode.SelectedValue = "-Select-";
                        //txtDescription.Text = string.Empty;
                        //txtAmount.Text = string.Empty;

                        txtContra.ReadOnly = true;
                        txtTrnsactionCode.ReadOnly = true;

                        gvDebit();
                        UnPostValue();
                        lblTotalAmt.Visible = true;
                        SumValue();


                    }

                }




                if (lblTrnMode.Text == "0")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm = new object[22];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtTrnsactionCode.Text;
                    prm[5] = 0;
                    prm[6] = Converter.GetDouble(txtAmount.Text);
                    prm[7] = txtDescription.Text;
                    prm[8] = 1;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 1;
                    prm[11] = txtContra.Text;
                    prm[12] = txtTrnsactionCode.Text;
                    prm[13] = 0;

                    if (CtrlAccType.Text == "1" || CtrlAccType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm[15] = 1;
                    prm[16] = hdnCashCode.Text;

                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Text;
                    prm[19] = valuedate;
                    prm[20] = lblTrnMode.Text;
                    prm[21] = txtChqNo.Text;

                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm2 = new object[22];

                    prm2[0] = opdate1;
                    prm2[1] = 0;
                    prm2[2] = txtVchNo.Text;
                    prm2[3] = 0;
                    prm2[4] = txtContra.Text;
                    prm2[5] = Converter.GetDouble(txtAmount.Text);
                    prm2[6] = 0;
                    prm2[7] = txtDescription.Text;
                    prm2[8] = 0;
                    prm2[9] = CtrlTrnType.Text;
                    prm2[10] = 0;
                    prm2[11] = txtContra.Text;
                    prm2[12] = txtTrnsactionCode.Text;
                    prm2[13] = 1;

                    if (CtrlContraAType.Text == "2" || CtrlContraAType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm2[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm2[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm2[15] = 1;
                    prm2[16] = hdnCashCode.Text;
                    prm2[17] = CtrlModule.Text;
                    prm2[18] = hdnID.Text;
                    prm2[19] = valuedate1;
                    prm2[20] = lblTrnMode.Text;
                    prm2[21] = txtChqNo.Text;

                    int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm2, "A2ZGLMCUS"));

                    if (result2 == 0)
                    {
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                        //ddlTrnsactionCode.SelectedValue = "-Select-";
                        txtDescription.Text = string.Empty;
                        txtAmount.Text = string.Empty;

                        gvDebit();
                        UnPostValue();
                        lblTotalAmt.Visible = true;
                        SumValue();


                    }

                }

                if (lblTrnMode.Text == "1")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                    var prm = new object[22];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtTrnsactionCode.Text;
                    prm[5] = Converter.GetDouble(txtAmount.Text);
                    prm[6] = 0;
                    prm[7] = txtDescription.Text;
                    prm[8] = 0;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 0;
                    prm[11] = txtTrnsactionCode.Text;
                    prm[12] = txtContra.Text;
                    prm[13] = 0;

                    if (CtrlAccType.Text == "2" || CtrlAccType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }


                    prm[15] = 1;

                    prm[16] = hdnCashCode.Text;
                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Text;
                    prm[19] = valuedate;
                    prm[20] = lblTrnMode.Text;
                    prm[21] = txtChqNo.Text;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate3 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate3 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm3 = new object[22];

                    prm3[0] = opdate3;
                    prm3[1] = 0;
                    prm3[2] = txtVchNo.Text;
                    prm3[3] = 0;
                    prm3[4] = txtContra.Text;
                    prm3[5] = 0;
                    prm3[6] = Converter.GetDouble(txtAmount.Text);
                    prm3[7] = txtDescription.Text;
                    prm3[8] = 1;
                    prm3[9] = CtrlTrnType.Text;
                    prm3[10] = 1;
                    prm3[11] = txtTrnsactionCode.Text;
                    prm3[12] = txtContra.Text;
                    prm3[13] = 1;

                    if (CtrlContraAType.Text == "1" || CtrlContraAType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm3[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm3[14] = Converter.GetDouble(txtAmount.Text);
                    }


                    prm3[15] = 1;

                    prm3[16] = hdnCashCode.Text;
                    prm3[17] = CtrlModule.Text;
                    prm3[18] = hdnID.Text;
                    prm3[19] = valuedate3;
                    prm3[20] = lblTrnMode.Text;
                    prm3[21] = txtChqNo.Text;

                    int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm3, "A2ZGLMCUS"));

                    if (result3 == 0)
                    {
                        HeaderDropdown();

                        //ddlContra.Enabled = false;
                        //txtContra.Enabled = false;
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                        //ddlTrnsactionCode.SelectedValue = "-Select-";
                        txtDescription.Text = string.Empty;
                        txtAmount.Text = string.Empty;

                        gvDebit();
                        UnPostValue();

                        lblTotalAmt.Visible = true;
                        SumValue();

                        txtAmount.ReadOnly = false;

                    }
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnAdd_Click Problem');</script>");
                //throw ex;
            }
        }




        protected void gvDebitInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDebitInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idincrement = 0;
                Label lblId = (Label)gvDebitInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                Label lblType = (Label)gvDebitInfo.Rows[e.RowIndex].Cells[4].FindControl("lblTrnType");
                Label lblDrCr = (Label)gvDebitInfo.Rows[e.RowIndex].Cells[5].FindControl("lblTrnDrCr");

                int ID = Converter.GetInteger(lblId.Text);

                if (lblType.Text == "3" && lblDrCr.Text == "1")
                {
                    idincrement = ID - 1;
                }
                else
                {
                    idincrement = ID + 1;
                }

                string strQuery1 = "DELETE FROM WFGLTrannsaction WHERE Id = '" + ID + "' OR Id = '" + idincrement + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));
                //string strQuery2 = "DELETE FROM WFGLTrannsaction WHERE TrnFlag=1";
                //int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));

                gvDebit();
                UnPostValue();
                lblTotalAmt.Visible = true;
                SumValue();

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDebitInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
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

        protected void txtVchDate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtVchDate.Text = CtrlProcDate.Text;
                txtVchDate.Focus();
            }

            if (opdate1 < opdate2)
            {
                lblVchDate.Text = "Back Value Date:";
            }
            else
            {
                lblVchDate.Text = "Voucher Date:";
            }

            UnPostValue();

        }

        protected void btnBack1_Click(object sender, EventArgs e)
        {
            txtContra.Text = string.Empty;
            if (hdnContraHead4.Text != "0")
            {
                hdnContraCode.Text = hdnContraHead4.Text;
                hdnContraHead3.Text = "0";
                hdnContraHead4.Text = "0";
            }
            else if (hdnContraHead3.Text != "0")
            {
                hdnContraCode.Text = hdnContraHead3.Text;
                hdnContraHead2.Text = "0";
                hdnContraHead3.Text = "0";
                hdnContraHead4.Text = "0";
            }
            else
            {
                hdnContraCode.Text = hdnContraHead2.Text;
                hdnContraHead1.Text = "0";
                hdnContraHead2.Text = "0";
                hdnContraHead3.Text = "0";
                hdnContraHead4.Text = "0";
            }



            ContraDropDown();
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {
            txtTrnsactionCode.Text = string.Empty;
            if (hdnTranHead4.Text != "0")
            {
                hdnTranCode.Text = hdnTranHead4.Text;
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }
            else if (hdnTranHead3.Text != "0")
            {
                hdnTranCode.Text = hdnTranHead3.Text;
                hdnTranHead2.Text = "0";
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }
            else
            {
                hdnTranCode.Text = hdnTranHead2.Text;
                hdnTranHead1.Text = "0";
                hdnTranHead2.Text = "0";
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }



            TranCodeDropDown();
        }


        protected void GetGLAccBalance()
        {
            ErrMsg.Text = "0";

            var prm = new object[4];

            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            string Tdate = date.ToString("dd/MM/yyyy");

            int Code = Converter.GetInteger(lblGLCode.Text);
            int UId = Converter.GetInteger(hdnID.Text);

            prm[0] = Code;
            prm[1] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[2] = Converter.GetDateToYYYYMMDD(Tdate);
            prm[3] = UId;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalanceSingle", prm, "A2ZGLMCUS"));

            A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);

            if (glObj.GLAccNo > 0)
            {
                txtBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", glObj.GLClBal));
            }

        }

        protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTrnType.SelectedValue == "2")
            {
                GLBankCodeDropdown();

                txtChqNo.Text = string.Empty;
                lblChqNo.Visible = true;
                txtChqNo.Visible = true;

                lblGLBankCode.Visible = true;
                txtGLBankCode.Visible = true;
                ddlGLBankCode.Visible = true;

                lblBalance.Text = "GL Bank Balance :";

                txtGLBankCode.Text = string.Empty;
                ddlGLBankCode.SelectedIndex = 0;
                txtBalance.Text = string.Empty;


                txtBalance.ReadOnly = true;
                txtGLBankCode.Focus();
            }

            if (ddlTrnType.SelectedValue == "1")
            {
                //lblGLCashCode.Visible = false;
                //txtGLCashCode.Visible = false;
                //ddlGLCashCode.Visible = false;
                lblChqNo.Visible = false;
                txtChqNo.Visible = false;

                lblGLBankCode.Visible = false;
                txtGLBankCode.Visible = false;
                ddlGLBankCode.Visible = false;

                lblBalance.Text = "GL Cash Balance :";


                lblGLCode.Text = hdnCashCode.Text;
                GetGLAccBalance();
                txtBalance.ReadOnly = true;

            }



            //if (lblTrnCode.Text != string.Empty)
            //{
            //    AccGetInfo();
            //}


        }

        protected void txtGLBankCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLBankCode.Text != string.Empty)
                {
                    int GLCode = Converter.GetInteger(txtGLBankCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        CtrlRecType.Text = Converter.GetString(getDTO.GLRecType);
                        hdnGLSubHead.Text = Converter.GetString(getDTO.GLSubHead);
                        if (CtrlRecType.Text != "2")
                        {
                            InvalidGlCode();
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            return;
                        }
                        if (ddlTrnType.SelectedValue == "2" && hdnGLSubHead.Text != "10106000")
                        {
                            InvalidGLBankCode();
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            return;
                        }
                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLBankCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);

                        lblBalance.Visible = true;
                        txtBalance.Visible = true;
                        lblBalance.Text = "GL Bank Balance :";
                        lblGLCode.Text = txtGLBankCode.Text;
                        GetGLAccBalance();
                        txtBalance.ReadOnly = true;
                    }
                    else
                    {
                        txtGLBankCode.Text = string.Empty;
                        txtGLBankCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGLCashCode_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlGLBankCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLBankCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlGLBankCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);

                        lblBalance.Visible = true;
                        txtBalance.Visible = true;
                        lblBalance.Text = "GL Bank Balance :";
                        lblGLCode.Text = txtGLBankCode.Text;
                        GetGLAccBalance();
                        txtBalance.ReadOnly = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }


    }
}