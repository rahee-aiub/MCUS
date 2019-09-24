using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
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
    public partial class CSGetStaffAccountNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvDetailInfo.Visible = false;
                string TranDate = (string)Session["STranDate"];
                string Func = (string)Session["SFuncOpt"];
                string Module = (string)Session["SModule"];
                string VchNo = (string)Session["SVchNo"];
                string Cflag = (string)Session["CFlag"];
                string Ctrlflag = (string)Session["SControlFlag"];
                string SelectAccNo = (string)Session["SCtrlSelectAccNo"];


                lblFuncOpt.Text = Func;
                lblModule.Text = Module;
                CtrlSelectAccNo.Text = SelectAccNo;

                CFlag.Text = Cflag;
                lblCtrlFlag.Text = Ctrlflag;

                //DateTime tdt = Converter.GetDateTime(TranDate);
                //string tdate = tdt.ToString("dd/MM/yyyy");

                lblTranDate.Text = TranDate;
                lblVchNo.Text = VchNo;


                gvSearchMEMInfo.Visible = false;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                if (CFlag.Text == "1")
                {
                    string RCuNo = (string)Session["RCreditUNo"];
                    lblCuNumber.Text = RCuNo;
                    txtCreditUNo.Text = Converter.GetString(lblCuNumber.Text);
                    string RMemNo = (string)Session["RMemNo"];
                    txtMemNo.Text = RMemNo;
                    BtnSearch_Click(this, EventArgs.Empty);
                }


                if (lblModule.Text == "04")
                {
                    lblCUNum.Visible = false;
                    txtCreditUNo.Visible = false;
                    lblCuName.Visible = false;
                    lblOldCuNo.Visible = false;
                    txtOldCuNo.Visible = false;
                    lblOldMemNo.Visible = false;
                    txtOldMemNo.Visible = false;
                    lblMemNo.Text = "Staff Code";
                    txtMemNo.Focus();
                }
                else
                {
                    txtCreditUNo.Focus();
                }

            }
        }


        protected void gvDetail()
        {
            GenerateTransactionCode();

            //if (CtrlSelectAccNo.Text == string.Empty)
            //{
                string sqlquery3 = "SELECT distinct AccType,TrnCodeDesc,AccNo,AccOldNumber FROM WFCSGROUPACCOUNT WHERE UserId='" + lblID.Text + "' ORDER BY AccNo";
                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
            //}
            //else
            //{
            //    string sqlquery3 = "SELECT distinct AccType,TrnCodeDesc,AccNo,AccOldNumber FROM WFCSGROUPACCOUNT WHERE AccType='" + CtrlSelectAccNo.Text + "' AND UserId='" + lblID.Text + "' ORDER BY AccNo";
            //    gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
            //}
        }


        protected void gvSearchMEMDetail()
        {

            string sqlquery3 = "SELECT MemName, MemNo,MemOldMemNo,MemOld1MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE CuType = 0 AND CuNo = 0";
            gvSearchMEMInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSearchMEMInfo, "A2ZCSMCUS");
        }

        private void GenerateTransactionCode()
        {
            var prm = new object[7];

            if (lblModule.Text == "04")
            {
                prm[0] = 0;
                prm[1] = 0;
            }
            else
            {
                prm[0] = lblCuType.Text;
                prm[1] = lblCuNo.Text;
            }
            prm[2] = txtMemNo.Text;
            prm[3] = lblFuncOpt.Text;
            prm[4] = lblID.Text;
            prm[5] = lblModule.Text;
            prm[6] = lblCtrlFlag.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetGroupAccountInfo", prm, "A2ZCSMCUS"));
            if (result == 0)
            {
                string qry = "SELECT Id,TrnCode FROM WFCSGROUPACCOUNT  WHERE UserId='" + lblID.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count == 0)
                {
                    InvalidAcc();
                    txtCreditUNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    txtMemNo.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    return;
                }
            }

        }

        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }

        private void InvalidCuNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        private void InvalidMemNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        private void InvalidStaffNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Staff No.');", true);
            return;
        }

        protected void DisplayMessage()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";

            a = "Credit Union No. Already Transfered";
            e = "New Credit Union No.";
            b = string.Format("New Credit Union Type : {0}", lblCTypeName.Text);
            c = string.Format(lblCNo.Text);
            d = string.Format(lblCType.Text);
            X = "-";

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e;
            Msg += d;
            Msg += X;
            Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            if (txtMemNo.Text != string.Empty)
            {
                lblCuType.Text = "0";
                lblCuNo.Text = "0";

                Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                int CUNo = Converter.GetInteger(lblCuNo.Text);
                int MNo = Converter.GetInteger(txtMemNo.Text);
                A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                if (get6DTO.NoRecord > 0)
                {
                    lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                    gvDetailInfo.Visible = true;
                    gvDetail();
                    //MoveAccDescription();
                }
                else
                {
                    InvalidStaffNo();
                    txtMemNo.Text = string.Empty;
                    txtMemNo.Focus();
                    return;
                }

            }

        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }



        protected void gvSearchMEMInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session["flag"] = "1";
            Session["RTranDate"] = lblTranDate.Text;
            Session["RFuncOpt"] = lblFuncOpt.Text;
            Session["RModule"] = lblModule.Text;
            Session["RVchNo"] = lblVchNo.Text;
            Session["NewAccNo"] = string.Empty;

            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["CFlag"] = string.Empty;


            if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && lblFuncOpt.Text == "01")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByDeposit.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && lblFuncOpt.Text == "02")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanSettlement.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "03" || lblFuncOpt.Text == "04"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawal.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "05" || lblFuncOpt.Text == "06"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawalInt.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "07" || lblFuncOpt.Text == "08"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanDisbursement.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "09" || lblFuncOpt.Text == "10"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByEncashment.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "11" || lblFuncOpt.Text == "12"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByAdjCrDr.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "1")
            {
                //Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSAccountStatement.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "6")
            {
                Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSStaffAccountBalanceTransfer.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "7")
            {
                Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSAccountStatusChange.aspx'; self.close();</script>", false);
            }



        }

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvDetailInfo.SelectedRow;
            lblAccNo.Text = row.Cells[2].Text;
            Session["NewAccNo"] = lblAccNo.Text;
            Session["flag"] = "1";
            Session["RTranDate"] = lblTranDate.Text;
            Session["RFuncOpt"] = lblFuncOpt.Text;
            Session["RModule"] = lblModule.Text;
            Session["RVchNo"] = lblVchNo.Text;

            Session["Rflag"] = "1";
            Session["RCreditUNo"] = lblCuNumber.Text;
            Session["RMemNo"] = txtMemNo.Text;
            Session["CFlag"] = "1";

            
            if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && lblFuncOpt.Text == "01")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByDeposit.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && lblFuncOpt.Text == "02")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanSettlement.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "03" || lblFuncOpt.Text == "04"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawal.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "05" || lblFuncOpt.Text == "06"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByWithdrawalInt.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "07" || lblFuncOpt.Text == "08"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByLoanDisbursement.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "09" || lblFuncOpt.Text == "10"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByEncashment.aspx'; self.close();</script>", false);
            }
            else if ((lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty) && (lblFuncOpt.Text == "11" || lblFuncOpt.Text == "12"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSDailyTransactionByAdjCrDr.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "1")
            {
                //Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSAccountStatement.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "6")
            {
                Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSStaffAccountBalanceTransfer.aspx'; self.close();</script>", false);
            }
            else if (lblCtrlFlag.Text == "7")
            {
                Session["CFlag"] = string.Empty;
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                    "click", @"<script>window.opener.location.href='CSAccountStatusChange.aspx'; self.close();</script>", false);
            }
        }




        protected void gvSearchMEMInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvSearchMEMInfo.SelectedRow;
            txtMemNo.Text = row.Cells[1].Text;
            lblMemName.Text = row.Cells[0].Text;

            gvSearchMEMInfo.Visible = false;

            txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

            gvDetailInfo.Visible = true;
            gvDetail();


            //BtnSearch_Click(this, EventArgs.Empty);


        }
        protected void txtSearchCuName_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT  MemName, MemNo FROM A2ZMEMBER where  CuType = 0 AND CuNo = 0 AND MemName like '" + txtSearchCuName.Text + "%'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            gvSearchMEMInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry, gvSearchMEMInfo, "A2ZCSMCUS");

            gvSearchMEMInfo.Visible = true;

            //if(dt.Rows.Count>0)
            //{
            //    gvSearchDetail();
            //}

        }



        //protected void MoveAccDescription()
        //{
        //    try
        //    {

        //          foreach (GridViewRow r in gvDetailInfo.Rows)
        //          {
        //            //Int16 Acctype = Converter.GetSmallInteger(gvDetailInfo.Rows[i].Cells[1].Text);
        //            Label AccType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("AccType");
        //            lblAccType.Text = Converter.GetString(AccType.Text);
        //            string sqlquery = "SELECT AccTypeDescription from A2ZACCTYPE WHERE AccTypeCode='" + lblAccType.Text + "'";
        //            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
        //            if (dt.Rows.Count > 0)
        //            {
        //                Label AccTypeDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("AccTypeDesc");
        //                AccTypeDesc.Text = Converter.GetString(dt.Rows[0]["AccTypeDescription"]);
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.MoveAccTypeDesc Problem');</script>");
        //        //throw ex;
        //    }
        //}



    }
}