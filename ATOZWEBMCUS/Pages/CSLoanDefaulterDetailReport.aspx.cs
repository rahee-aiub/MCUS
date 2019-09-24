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
    public partial class CSLoanDefaulterDetailReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string cutype = (string)Session["CuType"];
                hdnCuType.Text = cutype;
                string cuno = (string)Session["CuNo"];
                hdnCuNo.Text = cuno;

                string cunumber = (string)Session["CuNumber"];
                txtCreditUNo.Text = cunumber;
                string acctype = (string)Session["AccType"];
                txtAcctitle.Text = acctype;
                string accno = (string)Session["AccNo"];
                txtAccNo.Text = accno;
                string memno = (string)Session["MemNo"];
                txtMemNo.Text = memno;
                string memname = (string)Session["MemName"];
                lblMemName.Text = memname;

                string trndate = (string)Session["TrnDate"];
                hdnDate.Text = trndate;
                

                string sqlqry = "SELECT AccTypeDescription from A2ZACCTYPE WHERE AccTypeCode='"+txtAcctitle.Text+"'";
                DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlqry, "A2ZCSMCUS");
                if(dt4.Rows.Count>0)
                {
                    lblAccTypeName.Text = Converter.GetString(dt4.Rows[0]["AccTypeDescription"]);

                }

                
                string sqlquery = "SELECT CuName from A2ZCUNION WHERE CuStatus !='9' and CuNo='" + hdnCuNo.Text + "' and CuType='" + hdnCuType.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");

                if (dt1.Rows.Count > 0)
                {

                    lblCUName.Text = Converter.GetString(dt1.Rows[0]["CuName"]);
                    //string sqlquery1 = "SELECT MemName from A2ZMEMBER WHERE  CuNo='" + CNo + "' and CuType='" + CuType + "' and MemNo='" + txtMemNo.Text + "'";
                    //DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZCSMCUS");
                    //if (dt2.Rows.Count > 0)
                    //{
                    //    lblMemName.Text = Converter.GetString(dt2.Rows[0]["MemName"]);
                    //}
                }

                gvPreview();
            }
        }

        protected void gvLoanDefaulterDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }
        protected void gvPreview()
        {
            try
            {
                DateTime date = Converter.GetDateTime(hdnDate.Text);

                string sqlquery3 = @"SELECT TrnDate, CalPrincAmt,CalIntAmt,UptoDuePrincAmt,UptoDueIntAmt,PayablePrincAmt,PayableIntAmt,PayablePenalAmt,PaidPrincAmt,PaidIntAmt,PaidPenalAmt,CurrDuePrincAmt,CurrDueIntAmt,NoDueInstalment FROM A2ZLOANDEFAULTER where CuType='" + hdnCuType.Text + "' AND CuNo='" + hdnCuNo.Text + "' AND MemNo='" + txtMemNo.Text + "' AND AccType='" + txtAcctitle.Text + "' AND AccNo='" + txtAccNo.Text + "' AND MONTH(TrnDate) !> MONTH('" + date + "') AND YEAR(TrnDate) !> YEAR('" + date + "')";
                gvLoanDefaulterDetail = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvLoanDefaulterDetail, "A2ZCSMCUS");

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session["Flag"] = "1";
            
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
             "click", @"<script>window.opener.location.href='CSLoanDefaulterReport.aspx'; self.close();</script>", false);
        }
    }
}