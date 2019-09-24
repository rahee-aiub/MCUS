using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.GeneralLedger;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSDailyEditTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPrmValue.Text = Request.QueryString["a%b"];
                string b = hdnPrmValue.Text;
                HdnModule.Text = b;


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlTrnDate.Text = date;

                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                txtVoucherNo.Focus();
                btnDelete.Visible = false;

                ValidityFlag.Text = "0";

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            gvDetailInfo.Visible = true;

            var prm = new object[4];

            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;
            prm[2] = HdnModule.Text;
            prm[3] = Converter.GetDateToYYYYMMDD(CtrlTrnDate.Text);


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetRevTransaction", prm, "A2ZCSMCUS"));
            if (result == 0)
            {
                string qry = "SELECT Id,AccNo,FuncOpt,TrnRevFlag FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnModule='" + HdnModule.Text + "' AND FromCashCode='" + lblCashCode.Text + "' AND TrnSysUser = 0";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    HdnFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                    lblTrnRevFlag.Text = Converter.GetString(dt.Rows[0]["TrnRevFlag"]);

                    if (lblTrnRevFlag.Text == "1")
                    {
                        AccountClosedMSG();
                        return;
                    }

                    btnDelete.Visible = true;
                    gvPreview();
                }
                else
                {
                    btnDelete.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    VoucherMSG();
                }
            }
            else
            {
                btnDelete.Visible = false;

                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                VoucherMSG();
            }

        }

        private void gvPreview()
        {
            string Qry = "SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDesc,Abs(GLAmount) as GLAmount,TrnType,TrnDrCr FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");
        }


        private void ValidityReverse()
        {
            ValidityFlag.Text = "0";

            if (HdnFuncOpt.Text == "1" || HdnFuncOpt.Text == "7")
            {
                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {
                    Label lblCuType = (Label)gvDetailInfo.Rows[i].Cells[1].FindControl("CuType");
                    Label lblCuNo = (Label)gvDetailInfo.Rows[i].Cells[2].FindControl("CuNo");
                    Label lblMemNo = (Label)gvDetailInfo.Rows[i].Cells[3].FindControl("MemNo");
                    Label lblAccType = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("AccType");
                    Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("AccNo");
                    Label lblTrnAmt = (Label)gvDetailInfo.Rows[i].Cells[7].FindControl("Amount");
                    Label lblTrnType = (Label)gvDetailInfo.Rows[i].Cells[8].FindControl("TrnType");
                    Label lblTrnDrCr = (Label)gvDetailInfo.Rows[i].Cells[9].FindControl("TrnDrCr");

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CuNo = Converter.GetInteger(lblCuNo.Text);
                    int MemNo = Converter.GetInteger(lblMemNo.Text);
                    int AccType = Converter.GetInteger(lblAccType.Text);
                    Int64 AccNo = Converter.GetLong(lblAccNo.Text);
                    Decimal TrnAmount = Converter.GetDecimal(lblTrnAmt.Text);

                    lblVchAmt.Text = Converter.GetString(lblTrnAmt.Text);
                    int TrnType = Converter.GetInteger(lblTrnType.Text);
                    lblVchTrnType.Text = Converter.GetString(lblTrnType.Text);

                    int TrnDrCr = Converter.GetInteger(lblTrnDrCr.Text);

                    Int16 AType = Converter.GetSmallInteger(lblAccType.Text);
                    A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AType));
                    if (get1DTO.AccTypeCode > 0)
                    {
                        lblAtyClass.Text = Converter.GetString(get1DTO.AccTypeClass);

                        if (lblAtyClass.Text == "1" || lblAtyClass.Text == "5")
                        {
                            string qry = "SELECT Id,AccBalance,AccLoanSancAmt FROM A2ZACCOUNT WHERE CuType = '" + CuType + "' AND CuNo='" + CuNo + "' AND MemNo='" + MemNo + "' AND AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                            if (dt.Rows.Count > 0)
                            {
                                Decimal limitamt = 0;
                                Decimal Amount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                                Decimal sancAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                                if(Amount > 0)
                                {
                                    limitamt = (sancAmount + Amount);
                                }
                                else 
                                {
                                    limitamt = (sancAmount - Math.Abs(Amount));
                                }

                                //if (limitamt > sancAmount)
                                //{
                                //    limitamt = sancAmount;
                                //}

                                if (lblAtyClass.Text == "1")
                                {
                                    if (Amount < TrnAmount)
                                    {
                                        ValidityFlag.Text = "1";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (limitamt < TrnAmount)
                                    {
                                        ValidityFlag.Text = "1";
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (lblVchTrnType.Text == "1" && TrnDrCr == 1)
                    {
                        CashGLAccBalance();
                    }
                }
            }
        }
        protected void CashGLAccBalance()
        {
            ValidityFlag.Text = "0";

            var prm = new object[4];

            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            string Tdate = date.ToString("dd/MM/yyyy");

            int Code = Converter.GetInteger(lblCashCode.Text);
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
                double InputAmt = Converter.GetDouble(lblVchAmt.Text);

                if (lblGLBalanceType.Text != "1" && InputAmt > AvailAmt)
                {
                    ValidityFlag.Text = "2";
                    return;
                }

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


        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Transaction Reverse Successfully Completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Reverse Successfully Completed');", true);
            return;

        }

        private void VoucherMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher not Found');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher not Found');", true);
            return;

        }

        private void AccountClosedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher not Found');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Account Already Closed');", true);
            return;

        }
        private void InvalidReverseMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Voucher Reverse');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher Reverse');", true);
        }

        protected void AccessAccAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Insufficent Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Account Balance');", true);
            return;

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
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ////ValidityReverse();
            ////if (ValidityFlag.Text == "1")
            ////{
            ////    AccessAccAmountMSG();
            ////    gvDetailInfo.Visible = false;
            ////    btnDelete.Visible = false;
            ////    txtVoucherNo.Text = string.Empty;
            ////    txtVoucherNo.Focus();
            ////    return;
            ////}
            ////else if (ValidityFlag.Text == "2")
            ////{
            ////    AccessCashAmountMSG();
            ////    gvDetailInfo.Visible = false;
            ////    btnDelete.Visible = false;
            ////    txtVoucherNo.Text = string.Empty;
            ////    txtVoucherNo.Focus();
            ////    return;
            ////}
            ////else
            ////{
                DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var prm = new object[3];

                prm[0] = txtVoucherNo.Text;
                prm[1] = hdnID.Text;
                prm[2] = HdnModule.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteTransaction", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    UpdateBackUpStat();
                    gvDetailInfo.Visible = false;
                    btnDelete.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    Successful();
                }
            ////}

        }




    }
}