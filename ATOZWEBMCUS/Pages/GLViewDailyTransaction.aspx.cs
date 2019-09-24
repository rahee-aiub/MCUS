using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLViewDailyTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    //lblModule.Text = Request.QueryString["a%b"];

                    string Voucher = (string)Session["VouNo"];
                    string module = (string)Session["Module"];

                    txtVoucherNo.Text = Voucher;
                    lblModule.Text = module;

                    DataTable dt = new DataTable();
                    dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT  TrnDate FROM  dbo.A2ZTRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "' AND TrnCSGL=1", "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dte = Converter.GetDateTime(dto.ProcessDate);
                        string date = dte.ToString("dd/MM/yyyy");
                        txtTranDate.Text = date;

                    }
                    gvDetail();
                    SumValue();
                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT GLAccNo,TrnDesc,GLAmount FROM A2ZTRANSACTION WHERE TrnFlag !=1 AND VchNo='" + txtVoucherNo.Text + "' AND TrnCSGL=1";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
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

        protected void SumValue()
        {
            Decimal sumCr = 0;


            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {

                sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[2].Text));
                //sumDr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[5].Text));

            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            //CtrlTrnDrTotal.Text = Convert.ToString(String.Format("{0:0,0.00}", sumDr));
        }
        protected void BtnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "UPDATE A2ZTRANSACTION SET TrnProcStat=0, VerifyUserID='" + hdnID.Text + "' WHERE VchNo='" + txtVoucherNo.Text + "' AND TrnCSGL=1";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {
                    Session["Flag"] = "1";
                    Session["CVch"] = txtVoucherNo.Text;
                    Session["CModule"] = lblModule.Text;


                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
"click", @"<script>window.opener.location.href='GLVerifyDailyTransaction.aspx'; self.close();</script>", false);


                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='GLVerifyDailyTransaction.aspx';self.close();</script>");

                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnVerify_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Flag"] = "1";
                Session["CVch"] = txtVoucherNo.Text;
                Session["CModule"] = lblModule.Text;
                
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.opener.location.href='GLVerifyDailyTransaction.aspx'; self.close();</script>", false);


                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='GLVerifyDailyTransaction.aspx';self.close();</script>");
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnExit_Click Problem');</script>");
                //throw ex;
            }
        }


    }
}