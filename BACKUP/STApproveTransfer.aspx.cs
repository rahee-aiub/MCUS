using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;
using System.Drawing;

namespace ATOZWEBMCUS.Pages
{
    public partial class STApproveTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                ShowTransferGrid();
                DivReject.Visible = false;
                DivSelect.Visible = false;
                divApprove.Visible = false;
                lblmsg1.Visible = false;
                lblmsg2.Visible = false;

                

                GetNoOfVoucher();
            }

        }

        protected  void GetNoOfVoucher()
        {
           
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select VchNo from A2ZSTTRANSACTION Where TrnProcFlag = 0", "A2ZSTMCUS");

            if (dt.Rows.Count == 0)
            {
                gvTransferDetails.Visible = false;
                lblmsg1.Visible = true;
                lblmsg2.Visible = true;
               
            }
            
        }


        private void ShowTransferGrid()
        {
            string sqlquery = "SELECT Distinct VchNo,ItemCategoryNo,ItemGroupDesc,OrderNo,ChalanNo,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TransactionDate from A2ZSTTRANSACTION Where TrnProcFlag = 1 AND TrnWarehouseNo = " + lblCashCode.Text + " ";
            gvTransferDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvTransferDetails, "A2ZSTMCUS");

        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            decimal Price = 0;
            int TotalQty = 0;
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label VchNo = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[3].FindControl("lblVchNo");

            lblSelectedVchNo.Text = Converter.GetString(VchNo.Text);

            gvItemDetailsInfo();
            divApprove.Visible = true;
            DivSelect.Visible = true;
            DivReject.Visible = false;

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                String txtTotal = gvItemDetails.Rows[i].Cells[8].Text.ToString();
                String QtyTotal = gvItemDetails.Rows[i].Cells[5].Text.ToString();

                //Price += Converter.GetDecimal(txtTotal);
                //txtTotalAmt.Text = Price.ToString("0.00");

                TotalQty += Converter.GetInteger(QtyTotal);
                txtTotalItemQty.Text = TotalQty.ToString();
                txtRcvItmQty.Text = TotalQty.ToString();

                txtMissingItemQty.Focus();
            }
        }

        protected void BtnRejectSelect_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label VchNo = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[3].FindControl("lblVchNo");
            lblSelectedVchNo.Text = VchNo.Text;

            gvItemDetailsInfo();
            Div2.Visible = true;
            DivReject.Visible = true;
            divApprove.Visible = false;
            DivSelect.Visible = false;


        }

        private void gvItemDetailsInfo()
        {
            string sqlquery3 = "SELECT Id,ItemGroupNo,ItemCategoryNo,ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemTotalPrice,ItemSellPrice FROM A2ZSTTRANSACTION Where VchNo = '" + lblSelectedVchNo.Text + "' AND TrnProcFlag = 1 AND TrnWarehouseNo = " + lblCashCode.Text + " ";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvItemDetails, "A2ZSTMCUS");
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            string strQuery = "UPDATE A2ZSTTRANSACTION set TrnProcFlag = 99 Where VchNo = " + lblSelectedVchNo.Text + "";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
            if (rowEffect > 0)
            {
                ShowTransferGrid();
                Div2.Visible = false;
                divApprove.Visible = false;
                DivSelect.Visible = false;
                DivReject.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The transaction is rejected');", true);
                return;
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if (txtMissingItemQty.Text == string.Empty)
            {
                txtMissingItemQty.Text = "0";
            }


            string strQuery = "UPDATE A2ZSTTRANSACTION set TrnProcFlag = 0, TrnQtyCr = " + txtRcvItmQty.Text + ", TrnMissQtyCr = " + txtMissingItemQty.Text + " Where VchNo = '" + lblSelectedVchNo.Text + "' AND TrnProcFlag = 1";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
            if (rowEffect > 0)
            {

                ShowTransferGrid();
                Div2.Visible = false;
                divApprove.Visible = false;
                DivSelect.Visible = false;
                DivReject.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The transaction is Approved');", true);
                return;
            }
        }

        protected void btnCanApproved_Click(object sender, EventArgs e)
        {
            Div2.Visible = false;
            divApprove.Visible = false;
            DivSelect.Visible = false;
            DivReject.Visible = false;
        }

        protected void BtnCanReject_Click(object sender, EventArgs e)
        {
            Div2.Visible = false;
            divApprove.Visible = false;
            DivSelect.Visible = false;
            DivReject.Visible = false;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtMissingItemQty_TextChanged(object sender, EventArgs e)
        {
            decimal totqty = Converter.GetDecimal(txtTotalItemQty.Text);
            decimal missqty = Converter.GetDecimal(txtMissingItemQty.Text);

            decimal rcvqty = (totqty - missqty);

            txtRcvItmQty.Text = rcvqty.ToString();



        }




    }
}