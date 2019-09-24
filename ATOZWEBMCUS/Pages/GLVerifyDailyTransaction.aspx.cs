using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLVerifyDailyTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string Flag = (string)Session["Flag"];
                    string voucher = (string)Session["CVch"];
                    string CModule = (string)Session["CModule"];
                    CtrlFlag.Text = Flag;
                    lblVch.Text = voucher;
                   

                    if (CtrlFlag.Text != "1")
                    {
                        CtrlPrmValue.Text = Request.QueryString["a%b"];
                        string b = CtrlPrmValue.Text;
                        CtrlModule.Text = b;
                    }
                    else
                    {
                        CtrlModule.Text = CModule;
                    }

                    
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;
                    if (CtrlModule.Text == "2")
                    {
                        gv2Info();
                    }
                    else
                    {
                        gv6Info();
                    }
                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void RemoveSession()
        {
            Session["Flag"] = string.Empty;
            Session["CVch"] = string.Empty;
            Session["CModule"] = string.Empty;

        }
        private void gv2Info()
        {

            string sqlquery3 = "SELECT VchNo,TrnDesc,SUM(Abs(GLAmount)) AS 'Amount' FROM A2ZTRANSACTION WHERE TrnProcStat='1' AND TrnFlag=0 AND TrnCSGL=1 GROUP BY VchNo,TrnDesc";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }

        private void gv6Info()
        {

            string sqlquery3 = "SELECT VchNo,TrnDesc,SUM(Abs(GLAmount)) AS 'Amount' FROM A2ZTRANSACTION WHERE TrnProcStat='1' AND TrnFlag=0 AND TrnCSGL=1 AND FromCashCode='" + hdnCashCode.Text + "' GROUP BY VchNo,TrnDesc";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {


                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label voucher = (Label)gvCUInfo.Rows[r.RowIndex].Cells[1].FindControl("lblVoucherNo");
                Label lblTrnDesc = (Label)gvCUInfo.Rows[r.RowIndex].Cells[2].FindControl("TrnDesc");
                Label TrnAmount = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("Amount");
                lblTrnAmount.Text = Converter.GetString(TrnAmount.Text);

                string VNo = Converter.GetString(voucher.Text);

                double TotalAmt = Converter.GetDouble(lblTrnAmount.Text);
                int Ids = Converter.GetInteger(hdnID.Text);
                A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

                if (getDTO.IdsNo > 0)
                {

                    if (TotalAmt > getDTO.LIdsCashCredit)
                    {
                        OverAccessLimitMSG();
                        return;
                    }



                    if (TotalAmt > getDTO.LIdsCashDebit)
                    {
                        OverAccessLimitMSG();
                        return;
                    }


                }

                Session["VouNo"] = VNo;
                Session["Module"] = CtrlModule.Text;
              


                if (CtrlModule.Text == "2")
                {
                    gv2Info();
                }
                else
                {
                    gv6Info();
                }


                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('GLViewDailyTransaction.aspx','_blank');</script>", false);

                //Page.ClientScript.RegisterStartupScript(
                //this.GetType(), "OpenWindow", "window.open('GLViewDailyTransaction.aspx','_newtab');", true);

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSelect_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void OverAccessLimitMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Access Denied for Transaction Accessbility Limit');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Access Denied for Transaction Accessbility Limit');", true);
            return;
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}