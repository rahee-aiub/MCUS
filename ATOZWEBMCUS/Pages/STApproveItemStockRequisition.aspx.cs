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
    public partial class STApproveItemStockRequisition : System.Web.UI.Page
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
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select ReqNo from A2ZITEMREQUIRE Where ReqProcStat = 2", "A2ZSTMCUS");

            if (dt.Rows.Count == 0)
            {
                gvTransferDetails.Visible = false;
                lblmsg1.Visible = true;
                lblmsg2.Visible = true;
            }
        }

        private void ShowTransferGrid()
        {
            string sqlquery = "SELECT Distinct ReqDate,ReqWarehouseNo,ReqWarehouseName,ReqNo,ReqItemGroupNo,ReqItemGroupDesc,ReqItemCategoryNo,ReqItemCategoryDesc from A2ZITEMREQUIRE Where ReqProcStat = 2 GROUP BY ReqDate,ReqWarehouseNo,ReqWarehouseName,ReqNo,ReqItemGroupNo,ReqItemGroupDesc,ReqItemCategoryNo,ReqItemCategoryDesc ORDER BY ReqDate,ReqWarehouseNo";
            gvTransferDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvTransferDetails, "A2ZSTMCUS");
        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnApproved.Visible == false)
                {

                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label lwarehouse = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[3].FindControl("lblWareHouseNo");
                    Label lreqno = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[5].FindControl("lblReqNo");
                    Label lgroup = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[6].FindControl("lblGroupNo");
                    Label lcategory = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[8].FindControl("lblCategory");

                    CtrlWarehouse.Text = Converter.GetString(lwarehouse.Text);
                    CtrlReqNo.Text = Converter.GetString(lreqno.Text);
                    CtrlGroup.Text = Converter.GetString(lgroup.Text);
                    CtrlCategory.Text = Converter.GetString(lcategory.Text);

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));

                    int code1 = Converter.GetInteger(CtrlReqNo.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, code1);

                    int code2 = Converter.GetInteger(CtrlWarehouse.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, code2);

                    int code3 = Converter.GetInteger(CtrlGroup.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, code3);

                    int code4 = Converter.GetInteger(CtrlCategory.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, code4);




                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptSTItemStockRequsitionList");

                    Response.Redirect("ReportServer.aspx", false);

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void BtnSelect_Click(object sender, EventArgs e)
        {
           
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label lwarehouse = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[3].FindControl("lblWareHouseNo");
            Label lreqno = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[5].FindControl("lblReqNo");
            Label lgroup = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[6].FindControl("lblGroupNo");
            Label lcategory = (Label)gvTransferDetails.Rows[r.RowIndex].Cells[8].FindControl("lblCategory");

            CtrlWarehouse.Text = Converter.GetString(lwarehouse.Text);
            CtrlReqNo.Text = Converter.GetString(lreqno.Text);
            CtrlGroup.Text = Converter.GetString(lgroup.Text);
            CtrlCategory.Text = Converter.GetString(lcategory.Text);


            gvItemDetailsInfo();
            Div2.Visible = true;
            divApprove.Visible = true;

        }


        private void gvItemDetailsInfo()
        {
            string sqlquery3 = "SELECT Id,ReqItemCode,ReqItemName,ReqUnitQtyBalance,ReqReqUnitQty,ReqNote FROM A2ZITEMREQUIRE Where ReqWarehouseNo = '" + CtrlWarehouse.Text + "' AND ReqNo = '" + CtrlReqNo.Text + "' AND ReqItemGroupNo = '" + CtrlGroup.Text + "' AND ReqItemCategoryNo = '" + CtrlCategory.Text + "' AND ReqProcStat = 2";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvItemDetails, "A2ZSTMCUS");
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            string strQuery = "UPDATE A2ZITEMREQUIRE set ReqProcStat = 3 Where ReqWarehouseNo = '" + CtrlWarehouse.Text + "' AND ReqNo = '" + CtrlReqNo.Text + "' AND ReqItemGroupNo = '" + CtrlGroup.Text + "' AND ReqItemCategoryNo = '" + CtrlCategory.Text + "' AND ReqProcStat = 2";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));

            ShowTransferGrid();
            Div2.Visible = false;
            divApprove.Visible = false;

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The Requisition is Approved');", true);

        }

        protected void btnCanApproved_Click(object sender, EventArgs e)
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