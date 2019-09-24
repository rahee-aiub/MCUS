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
              
                divApprove.Visible = false;
                lblmsg1.Visible = false;
                lblmsg2.Visible = false;
                GetNoOfVoucher();
            }
        }

        protected void GetNoOfVoucher()
        {
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select VchNo from A2ZSTTRANSFER Where TrnProcFlag = 1", "A2ZSTMCUS");

            if (dt.Rows.Count == 0)
            {
                gvTransferDetails.Visible = false;
                lblmsg1.Visible = true;
                lblmsg2.Visible = true;
            }
        }

        private void ShowTransferGrid()
        {
            string sqlquery = "SELECT Distinct VchNo,ItemCategoryNo,ItemGroupDesc,OrderNo,ChalanNo,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TransactionDate from A2ZSTTRANSFER Where TrnProcFlag = 1 AND TrnWarehouseNo = " + lblCashCode.Text + " ";
            gvTransferDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvTransferDetails, "A2ZSTMCUS");
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            decimal Price = 0;
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label VchNo = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[1].FindControl("lblVchNo");
            lblSelectedVchNo.Text = Converter.GetString(VchNo.Text);
            gvItemDetailsInfo();
            Div2.Visible = true;
            divApprove.Visible = true;
          

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                TextBox TotalPrice = (TextBox)gvItemDetails.Rows[i].Cells[7].FindControl("txtTotalPrice");
                Price += Converter.GetDecimal(TotalPrice.Text);
            }
            txtTotalAmt.Text = Converter.GetString(Price);
        }

        //protected void BtnRejectSelect_Click(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    GridViewRow r = (GridViewRow)b.NamingContainer;
        //    Label VchNo = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[2].FindControl("lblVchNo");
        //    lblSelectedVchNo.Text = VchNo.Text;

        //    gvItemDetailsInfo();
        //    Div2.Visible = true;
        //    DivReject.Visible = true;
        //    divApprove.Visible = false;
        //}

        private void gvItemDetailsInfo()
        {
            string sqlquery3 = "SELECT Id,ItemGroupNo,ItemCategoryNo,ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemTotalPrice,ItemSellPrice,TrnMissQtyCr FROM A2ZSTTRANSFER Where VchNo = '" + lblSelectedVchNo.Text + "' AND TrnProcFlag = 1 AND TrnWarehouseNo = " + lblCashCode.Text + " ";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvItemDetails, "A2ZSTMCUS");
        }

        //protected void BtnReject_Click(object sender, EventArgs e)
        //{
        //    string strQuery = "UPDATE A2ZSTTRANSACTION set TrnProcFlag = 99 Where VchNo = " + lblSelectedVchNo.Text + "";
        //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
        //    if (rowEffect > 0)
        //    {
        //        ShowTransferGrid();
        //        Div2.Visible = false;
        //        divApprove.Visible = false;
        //        DivReject.Visible = false;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The transaction is rejected');", true);
        //        return;
        //    }
        //}

        protected void btnApproved_Click(object sender, EventArgs e)
        {

            int TrnQty = 0;
            int MisQty = 0;
            int OrgQty = 0;

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                TextBox STKItemCode = (TextBox)gvItemDetails.Rows[i].Cells[2].FindControl("txtItemCode");
                TextBox STKItemPurchaseQty = (TextBox)gvItemDetails.Rows[i].Cells[4].FindControl("txtPurchaseQty");
                TextBox MissingQty = (TextBox)gvItemDetails.Rows[i].Cells[8].FindControl("txtMissingQty");

                if (MissingQty.Text == String.Empty)
                {
                    MissingQty.Text = "0";
                }

                TrnQty = Converter.GetInteger(STKItemPurchaseQty.Text);
                MisQty = Converter.GetInteger(MissingQty.Text);
                OrgQty = TrnQty - MisQty;

                string strQuery = "UPDATE A2ZSTTRANSFER set TrnProcFlag = 0, TrnQtyCr = " + OrgQty + ", TrnMissQtyCr = " + MissingQty.Text + " Where VchNo = '" + lblSelectedVchNo.Text + "' AND TrnWarehouseNo = '" + lblCashCode.Text + "' AND ItemCode = " + STKItemCode.Text + " AND TrnProcFlag = 1";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
            }


            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");
            lblProcessDate.Text = date1;


            DateTime opdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            var prm = new object[4];
           
            prm[0] = lblSelectedVchNo.Text;  
            prm[1] = opdate;
            prm[2] = lblCashCode.Text;
            prm[3] = "2";
            

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STApproveTrfTransaction", prm, "A2ZSTMCUS"));

            if (result == 0)
            {
                ShowTransferGrid();
                Div2.Visible = false;
                divApprove.Visible = false;
              
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The transaction is Approved');", true);
            }

            
        }

        protected void btnCanApproved_Click(object sender, EventArgs e)
        {
            Div2.Visible = false;
            divApprove.Visible = false;
           
        }

        protected void BtnCanReject_Click(object sender, EventArgs e)
        {
            Div2.Visible = false;
            divApprove.Visible = false;
          
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}