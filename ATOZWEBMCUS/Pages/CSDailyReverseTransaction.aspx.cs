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
    public partial class CSDailyReverseTransaction : System.Web.UI.Page
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
                btnCancel.Visible = false;

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
                string qry = "SELECT Id,AccType,AccNo,FuncOpt,TrnRevFlag FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnModule='" + HdnModule.Text + "' AND FromCashCode='" + lblCashCode.Text + "' AND TrnSysUser = 0";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);
                    CtrlAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    HdnFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                    lblTrnRevFlag.Text = Converter.GetString(dt.Rows[0]["TrnRevFlag"]);


                    Int16 AType = Converter.GetSmallInteger(CtrlAccType.Text);
                    A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AType));
                    if (get1DTO.AccTypeCode > 0)
                    {
                        lblAtyClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                    }


                    if (lblTrnRevFlag.Text == "1")
                    {
                        AccountClosedMSG();
                        return;
                    }

                    txtVoucherNo.ReadOnly = true;
                    btnDelete.Visible = true;
                    btnCancel.Visible = true;
                    gvDetailInfo.Visible = true;
                    gvPreview();
                }
                else
                {
                    btnDelete.Visible = false;
                    btnCancel.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    VoucherMSG();
                }
            }
            else
            {
                btnDelete.Visible = false;
                btnCancel.Visible = false;

                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                VoucherMSG();
            }

        }

        private void gvPreview()
        {
            string Qry = "SELECT TrnID,TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDesc,Abs(GLAmount) as GLAmount,TrnType,TrnDrCr,TrnSign,PayType,FuncOpt FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");
        }


        private void ValidityReverse()
        {
            ValidityFlag.Text = "0";

            if (HdnFuncOpt.Text == "1" || HdnFuncOpt.Text == "7")
            {
                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {
                    Label lblCuType = (Label)gvDetailInfo.Rows[i].Cells[2].FindControl("CuType");
                    Label lblCuNo = (Label)gvDetailInfo.Rows[i].Cells[3].FindControl("CuNo");
                    Label lblMemNo = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("MemNo");
                    Label lblAccType = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("AccType");
                    Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[6].FindControl("AccNo");
                    Label lblTrnAmt = (Label)gvDetailInfo.Rows[i].Cells[8].FindControl("Amount");
                    Label lblTrnType = (Label)gvDetailInfo.Rows[i].Cells[9].FindControl("TrnType");
                    Label lblTrnDrCr = (Label)gvDetailInfo.Rows[i].Cells[10].FindControl("TrnDrCr");
                    Label lblPayType = (Label)gvDetailInfo.Rows[i].Cells[12].FindControl("PayType");

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
                            if (lblPayType.Text != "352")
                            {
                                string qry = "SELECT Id,AccBalance,AccLoanSancAmt FROM A2ZACCOUNT WHERE CuType = '" + CuType + "' AND CuNo='" + CuNo + "' AND MemNo='" + MemNo + "' AND AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                                if (dt.Rows.Count > 0)
                                {
                                    Decimal limitamt = 0;
                                    Decimal Amount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                                    Decimal sancAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                                    if (Amount > 0)
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

            var prm = new object[3];

            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime date = Converter.GetDateTime(dto.ProcessDate);
            string Tdate = date.ToString("dd/MM/yyyy");


            int Code = Converter.GetInteger(lblCashCode.Text);

            prm[0] = Code;
            prm[1] = Converter.GetDateToYYYYMMDD(Tdate);

            prm[2] = 0;
            //prm[3] = UId;
            DataTable dt = CommonManager.Instance.GetDataTableBySpWithParams("SpM_GlAccountBalanceSingle", prm, "A2ZGLMCUS");
            if (dt.Rows.Count > 0)
            {
                lblGLAccBal.Text = Converter.GetString(String.Format("{0:0,0.00}", Converter.GetDecimal(dt.Rows[0]["GLOpBal"])));

                lblGLBalanceType.Text = Converter.GetString(dt.Rows[0]["GLBalanceType"]);

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


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Reverse Successfully Completed');", true);
            return;

        }

        private void VoucherMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher not Found');", true);
            return;

        }

        private void AccountClosedMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Account Already Closed');", true);
            return;

        }
        private void InvalidReverseMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher Reverse');", true);
        }

        protected void AccessAccAmountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Account Balance');", true);
            return;

        }

        protected void AccessCashAmountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Cash Balance');", true);
            return;

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtVoucherNo.Text = string.Empty;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Focus();
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            gvDetailInfo.Visible = false;
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


                //if (lblChgFlag.Text == "1")
                //{
                //    TextBox lblTrnAmt = (TextBox)e.Row.FindControl("Amount");

                //    Button BtnDel = (Button)e.Row.FindControl("BtnDel");
                //    Button BtnChg = (Button)e.Row.FindControl("BtnChg");
                //    Button BtnUpd = (Button)e.Row.FindControl("BtnUpd");
                //    Button BtnCan = (Button)e.Row.FindControl("BtnCan");

                //    lblTrnAmt.Enabled = true;
                //    BtnDel.Visible = false;
                //    BtnChg.Visible = false;
                //    BtnUpd.Visible = true;
                //    BtnCan.Visible = true;
                //}


            }
        }


        protected void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");
                Label lblAccNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[6].FindControl("AccNo");
                Label lblPayType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[12].FindControl("PayType");
                Label lblFuncOpt = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[13].FindControl("FuncOpt");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;
                int idincrement = 0;

                if (lblFuncOpt.Text == "20")
                {
                    for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                    {
                        Label ClblId = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("lblID");

                        int CID = Converter.GetInteger(ClblId.Text);

                        if (CID == ID)
                        {
                            RecID = i;
                            RecID = RecID + 1;
                        }
                    }

                    int mod = RecID % 2;

                    if (mod != 0)
                    {
                        idincrement = ID + 1;
                        string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + ID + "' and '" + idincrement + "'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    }
                    else
                    {
                        idincrement = ID - 1;
                        string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + idincrement + "' and '" + ID + "'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    }
                }
                else
                {
                    idincrement = ID + 1;
                    string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + ID + "' and '" + idincrement + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                }


                if (lblPayType.Text == "403" || lblPayType.Text == "353")
                {
                    string strQuery2 = "UPDATE A2ZTRANSACTION SET  TrnInterestAmt = TrnCredit,ShowInterest = 2 WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND (PayType = 402 OR PayType = 352) AND TrnCredit > 0";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));

                    string qry = "SELECT Id,TrnCredit FROM A2ZTRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND (PayType = 404 OR PayType = 302) AND TrnCredit > 0";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["TrnCredit"]);

                        string strQuery3 = "UPDATE A2ZTRANSACTION SET  TrnPenalAmt = '" + Amount + "' WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND (PayType = 402 OR PayType = 352) AND TrnCredit > 0";
                        int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery3, "A2ZCSMCUS"));
                    }
                }


                if (lblPayType.Text == "402" || lblPayType.Text == "352")
                {
                    string strQuery4 = "UPDATE A2ZTRANSACTION SET  TrnInterestAmt = 0 WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND TrnInterestAmt > 0";
                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery4, "A2ZCSMCUS"));
                }

                if (lblPayType.Text == "404" || lblPayType.Text == "302")
                {
                    string strQuery5 = "UPDATE A2ZTRANSACTION SET  TrnPenalAmt = 0 WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND TrnPenalAmt > 0";
                    int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery5, "A2ZCSMCUS"));
                }

                string strQuery6 = "DELETE FROM WF_REVERSETRANSACTION WHERE TrnID = '" + ID + "'";
                int status62 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery6, "A2ZCSMCUS"));


                if (lblFuncOpt.Text == "20")
                {
                    string strQuery7 = "DELETE FROM WF_REVERSETRANSACTION WHERE TrnID = '" + idincrement + "'";
                    int status67 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery7, "A2ZCSMCUS"));
                }

                gvPreview();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }


        protected void gvChgDetail()
        {
            string Qry = "SELECT TrnID,TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDesc,Abs(GLAmount) as GLAmount,TrnType,TrnDrCr,TrnSign,PayType,FuncOpt FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnID = '" + lblTrnID.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");

        }

        protected void BtnChg_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;

                lblTrnID.Text = Converter.GetString(lblId.Text);


                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {
                    Label ClblId = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("lblID");

                    int CID = Converter.GetInteger(ClblId.Text);

                    if (CID == ID)
                    {
                        RecID = i;
                        RecID = RecID + 1;
                    }
                }

                lblRecID.Text = Converter.GetString(RecID);


                lblChgFlag.Text = "1";

                gvChgDetail();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }


        protected void BtnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");
                Label lblAccNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[6].FindControl("AccNo");
                Label lblTrnAmt = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[8].FindControl("Amount");
                Label lblTrnType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[9].FindControl("TrnType");
                Label lblTrnDrCr = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[10].FindControl("TrnDrCr");
                Label lblTrnSign = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[11].FindControl("TrnSign");
                Label lblPayType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[12].FindControl("PayType");
                Label lblFuncOpt = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[13].FindControl("FuncOpt");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;
                int idincrement = 0;

                Decimal Tamount = Converter.GetDecimal(lblTrnAmt.Text);

                if (lblTrnSign.Text == "1")
                {
                    Tamount = (0 - Tamount);
                }

                if (lblFuncOpt.Text == "20")
                {

                    RecID = Converter.GetInteger(lblRecID.Text);


                    int mod = RecID % 2;

                    if (mod != 0)
                    {
                        idincrement = ID + 1;
                    }
                    else
                    {
                        idincrement = ID - 1;
                    }
                }
                else
                {
                    idincrement = ID + 1;
                }


                if (lblTrnDrCr.Text == "1")
                {
                    string strQuery1 = "UPDATE A2ZTRANSACTION SET  TrnCredit = '" + lblTrnAmt.Text + "', GLCreditAmt = '" + lblTrnAmt.Text + "',GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string qry = "SELECT Id,GLAmount FROM A2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(lblTrnAmt.Text);

                        if (Amount < 0)
                        {
                            Camount = (0 - Camount);
                        }

                        string strQuery2 = "UPDATE A2ZTRANSACTION SET  TrnDebit = '" + lblTrnAmt.Text + "',GLDebitAmt = '" + lblTrnAmt.Text + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }

                }
                else
                {
                    string strQuery1 = "UPDATE A2ZTRANSACTION SET  TrnDebit = '" + lblTrnAmt.Text + "', GLDebitAmt = '" + lblTrnAmt.Text + "',GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string qry = "SELECT Id,GLAmount FROM A2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(lblTrnAmt.Text);

                        if (Amount < 0)
                        {
                            Camount = (0 - Camount);
                        }

                        string strQuery2 = "UPDATE A2ZTRANSACTION SET  TrnCredit = '" + lblTrnAmt.Text + "',GLCreditAmt = '" + lblTrnAmt.Text + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }
                }

                if (lblPayType.Text == "402" || lblPayType.Text == "352")
                {
                    string strQuery3 = "UPDATE A2ZTRANSACTION SET  TrnInterestAmt = '" + lblTrnAmt.Text + "' WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND TrnInterestAmt > 0";
                    int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery3, "A2ZCSMCUS"));
                }

                if (lblPayType.Text == "404" || lblPayType.Text == "302")
                {
                    string strQuery4 = "UPDATE A2ZTRANSACTION SET  TrnPenalAmt = '" + lblTrnAmt.Text + "' WHERE VchNo = '" + txtVoucherNo.Text + "' AND AccNo = '" + lblAccNo.Text + "' AND TrnPenalAmt > 0";
                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery4, "A2ZCSMCUS"));
                }


                string strQuery5 = "UPDATE WF_REVERSETRANSACTION SET  GLAmount = '" + Tamount + "' WHERE TrnID = '" + ID + "' ";
                int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery5, "A2ZCSMCUS"));

                if (lblFuncOpt.Text == "20")
                {
                    string strQuery6 = "UPDATE WF_REVERSETRANSACTION SET  GLAmount = '" + Tamount + "' WHERE TrnID = '" + idincrement + "' ";
                    int rowEffect6 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery6, "A2ZCSMCUS"));
                }

                lblChgFlag.Text = "0";

                gvPreview();


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnCan_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");

                int ID = Converter.GetInteger(lblId.Text);

                lblChgFlag.Text = "0";
                lblCanFlag.Text = "1";

                gvPreview();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ValidityReverse();
            if (ValidityFlag.Text == "1")
            {
                AccessAccAmountMSG();
                gvDetailInfo.Visible = false;
                btnDelete.Visible = false;
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                return;
            }
            else if (ValidityFlag.Text == "2")
            {
                AccessCashAmountMSG();
                gvDetailInfo.Visible = false;
                btnDelete.Visible = false;
                txtVoucherNo.Text = string.Empty;
                txtVoucherNo.Focus();
                return;
            }
            else
            {

                if (lblAtyClass.Text == "8" && (HdnFuncOpt.Text == "7" || HdnFuncOpt.Text == "8"))
                {
                    DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    var prm = new object[4];

                    prm[0] = CtrlAccNo.Text;
                    prm[1] = hdnID.Text;
                    prm[2] = txtVoucherNo.Text;
                    prm[3] = 0;


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_LoanDisburseWithRescheduleReverse", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        UpdateBackUpStat();
                        gvDetailInfo.Visible = false;
                        btnDelete.Visible = false;
                        btnCancel.Visible = false;
                        txtVoucherNo.ReadOnly = false;
                        txtVoucherNo.Text = string.Empty;
                        txtVoucherNo.ReadOnly = false;
                        txtVoucherNo.Focus();
                        Successful();
                        return;
                    }
                }
                else
                {
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
                        btnCancel.Visible = false;
                        txtVoucherNo.ReadOnly = false;
                        txtVoucherNo.Text = string.Empty;
                        txtVoucherNo.ReadOnly = false;
                        txtVoucherNo.Focus();
                        Successful();
                    }

                }
            }

        }

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}