using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSViewDailyTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                BtnVerify.Attributes.Add("onClick", "closePopup();");
                if (!IsPostBack)
                {
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    string Voucher = (string)Session["VouNo"];
                    string Func = (string)Session["FuncOpt"];
                    string Module = (string)Session["Module"];
                    string FuncDesc = (string)Session["FuncTitle"];

                    txtVoucherNo.Text = Voucher;
                    lblFuncOpt.Text = Func;
                    lblModule.Text = Module;
                    lblFuncDesc.Text = FuncDesc;

                    if (lblModule.Text == "4")
                    {
                        lblCUNum.Visible = false;
                        txtCreditUNo.Visible = false;
                        lblMemNo.Text = "Staff Code";
                    }

                    if (lblFuncOpt.Text == "3")
                    {
                        BtnViewImage.Visible = true;
                    }
                    else
                    {
                        BtnViewImage.Visible = false;
                    }

                    if (lblFuncOpt.Text == "1" || lblFuncOpt.Text == "2" || lblFuncOpt.Text == "11")
                    {
                        lblTotalAmt.Text = "NET AMOUNT RECEIVED";
                    }
                    else
                    {
                        lblTotalAmt.Text = "NET AMOUNT PAID";
                    }

                    DataTable dt = new DataTable();
                    dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,TrnDate, MemNo FROM  dbo.A2ZTRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "'", "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        txtCreditUNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);
                        txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dte = Converter.GetDateTime(dto.ProcessDate);
                        string date = dte.ToString("dd/MM/yyyy");
                        txtTranDate.Text = date;

                        string c = "";
                        int a = txtCreditUNo.Text.Length;

                        string b = txtCreditUNo.Text;
                        c = b.Substring(0, 1);
                        int re = Converter.GetSmallInteger(c);
                        int dd = a - 1;
                        string d = b.Substring(1, dd);
                        int re1 = Converter.GetSmallInteger(d);
                        Int16 CuType = Converter.GetSmallInteger(re);
                        int CNo = Converter.GetSmallInteger(re1);

                        lblCuNo.Text = Converter.GetString(CNo);
                        lblCuType.Text = Converter.GetString(CuType);
                        txtCreditUNo.Text = Converter.GetString(c + "-" + d);
                        string sqlquery = "SELECT CuName,GLCashCode from A2ZCUNION WHERE CuStatus !='9' and CuNo='" + CNo + "' and CuType='" + CuType + "'";
                        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");

                        if (dt1.Rows.Count > 0)
                        {
                            lblCUName.Text = Converter.GetString(dt1.Rows[0]["CuName"]);
                        }

                        string sqlquery1 = "SELECT MemName from A2ZMEMBER WHERE  CuNo='" + CNo + "' and CuType='" + CuType + "' and MemNo='" + txtMemNo.Text + "'";
                        DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZCSMCUS");
                        if (dt2.Rows.Count > 0)
                        {
                            lblMemName.Text = Converter.GetString(dt2.Rows[0]["MemName"]);
                        }


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
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string sqlquery3 = "SELECT AccType,AccNo,TrnDesc,SUM(Abs(GLAmount)) AS 'GLAmount',TrnType,TrnPayment FROM A2ZTRANSACTION WHERE TrnFlag !=1 AND VchNo='" + txtVoucherNo.Text + "' and TrnDate ='" + opdate + "' GROUP BY AccType,AccNo,TrnDesc,TrnType,TrnPayment";

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
                Label lblTrnPayment = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("TrnPayment");
                string TrnPayment = Converter.GetString(lblTrnPayment.Text);
                if (TrnPayment == "1")
                {
                    sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[3].Text));
                }
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
                DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string qry = "UPDATE A2ZTRANSACTION SET TrnProcStat=0,VerifyUserID='" + hdnID.Text + "' WHERE VchNo='" + txtVoucherNo.Text + "' and TrnDate ='" + opdate + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                    Session["Flag"] = "1";
                    Session["CModule"] = lblModule.Text;

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSVerifyDailyTransaction.aspx'; self.close();</script>", false);


                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSVerifyDailyTransaction.aspx'; self.close();</script>");


                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnVerify_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnViewImage_Click(object sender, EventArgs e)
        {
            {

                Session["CuNo"] = lblCuNo.Text;
                Session["CuType"] = lblCuType.Text;
                Session["MemNo"] = txtMemNo.Text;

                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
"click", @"<script>window.open('CSGetImage.aspx','_blank');</script>", false);

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session["Flag"] = "1";
            Session["CModule"] = lblModule.Text;

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSVerifyDailyTransaction.aspx'; self.close();</script>", false);
            
            
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSVerifyDailyTransaction.aspx';self.close();</script>");
        }




    }
}