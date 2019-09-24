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
    public partial class STDailyReverseTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

            var prm = new object[2];

            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;



            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STGetRevTransaction", prm, "A2ZSTMCUS"));
            if (result == 0)
            {
                string qry = "SELECT Id,TransactionDate,FuncOpt FROM WF_REVA2ZSTTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnWarehouseNo='" + lblCashCode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
                if (dt.Rows.Count > 0)
                {
                    lblFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);

                    if (lblFuncOpt.Text == "12")
                    {
                        string qry1 = "SELECT Id,TransactionDate,FuncOpt FROM A2ZSTTRANSFER  WHERE VchNo = '" + txtVoucherNo.Text + "' AND IssWarehouseNo='" + lblCashCode.Text + "' AND TrnProcFlag = 1";
                        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZSTMCUS");
                        if (dt1.Rows.Count == 0)
                        {
                            btnDelete.Visible = false;
                            btnCancel.Visible = false;
                            txtVoucherNo.Text = string.Empty;
                            txtVoucherNo.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Approved');", true);
                            return;
                        }
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
            string Qry = "SELECT Id,TransactionDate,FuncOptDesc,TrnAmtDr,TrnAmtCr FROM WF_REVA2ZSTTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnWarehouseNo='" + lblCashCode.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZSTMCUS");
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


            }
        }




        protected void btnDelete_Click(object sender, EventArgs e)
        {

            DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var prm = new object[4];

            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;
            prm[2] = lblFuncOpt.Text;
            prm[3] = lblCashCode.Text;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STDeleteTransaction", prm, "A2ZSTMCUS"));
            if (result == 0)
            {

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