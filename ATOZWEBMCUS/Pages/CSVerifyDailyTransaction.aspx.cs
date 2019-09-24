using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSVerifyDailyTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    string Flag = (string)Session["Flag"];
                    string CModule = (string)Session["CModule"];
                    CtrlFlag.Text = Flag;
                    CtrlModule.Text = CModule;

                    if (CtrlFlag.Text != "1")
                    {
                        lblModule.Text = Request.QueryString["a%b"];
                    }
                    else
                    {
                        lblModule.Text = CtrlModule.Text;
                    }
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    hdnProcDate.Text = date;


                    gvInfo();


                }
                //else
                //{
                //    gvInfo();

                //}

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
            Session["CModule"] = string.Empty;
            
        }
        private void gvInfo()
        {
            DateTime opdate = DateTime.ParseExact(hdnProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string sqlquery3 = "SELECT VchNo,FuncOpt,FuncOptDesc,SUM(Abs(GLAmount)) AS 'Amount' FROM A2ZTRANSACTION WHERE TrnProcStat='1' AND TrnCSGL = 0 AND TrnFlag=0 AND TrnModule= '" + lblModule.Text + "' and TrnDate ='" + opdate + "' AND TrnPayment = 1 GROUP BY VchNo,FuncOpt,FuncOPtDesc";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {

            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label voucher = (Label)gvCUInfo.Rows[r.RowIndex].Cells[1].FindControl("lblVoucherNo");
                Label Func = (Label)gvCUInfo.Rows[r.RowIndex].Cells[2].FindControl("lblFuncOpt");
                Label FuncDesc = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblFuncOptDesc");
                Label TrnAmount = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("Amount");
                lblTrnAmount.Text = Converter.GetString(TrnAmount.Text);

                lblFuncDesc.Text = Converter.GetString(FuncDesc.Text);

                string VNo = Converter.GetString(voucher.Text);
                string FOpt = Converter.GetString(Func.Text);
                lblFuncopt.Text = Converter.GetString(Func.Text);

                double TotalAmt = Converter.GetDouble(lblTrnAmount.Text);
                int Ids = Converter.GetInteger(hdnID.Text);
                A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

                if (getDTO.IdsNo > 0)
                {
                    if (lblFuncopt.Text == "1" ||
                        lblFuncopt.Text == "11")
                    {
                        if (TotalAmt > getDTO.LIdsCashCredit)
                        {
                            OverAccessLimitMSG();
                            return;
                        }
                    }

                    if (lblFuncopt.Text == "2" ||
                        lblFuncopt.Text == "3" ||
                        lblFuncopt.Text == "4" ||
                        lblFuncopt.Text == "5" ||
                        lblFuncopt.Text == "6" ||
                        lblFuncopt.Text == "7" ||
                        lblFuncopt.Text == "8" ||
                        lblFuncopt.Text == "9" ||
                        lblFuncopt.Text == "10" ||
                        lblFuncopt.Text == "12" ||
                        lblFuncopt.Text == "20")
                    {
                        if (TotalAmt > getDTO.LIdsCashDebit)
                        {
                            OverAccessLimitMSG();
                            return;
                        }

                    }
                }

                Session["VouNo"] = VNo;
                Session["FuncOpt"] = FOpt;
                Session["Module"] = lblModule.Text;
                Session["FuncTitle"] = lblFuncDesc.Text;



                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSViewDailyTransaction.aspx','_blank');</script>", false);


                //Page.ClientScript.RegisterStartupScript(
                //this.GetType(), "OpenWindow", "window.open('CSViewDailyTransaction.aspx','_newtab');", true);
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