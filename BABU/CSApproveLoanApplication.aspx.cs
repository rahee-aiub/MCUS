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
    public partial class CSApproveLoanApplication : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblModule.Text = Request.QueryString["a%b"];

                    lblInstallmentAmount.Visible = false;
                    txtInstallmentAmount.Visible = false;
                    lblLastInstallmentAmount.Visible = false;
                    txtLastInstallmentAmount.Visible = false;
                    lblSancAmt.Visible = false;
                    txtSancAmount.Visible = false;
                    lblOpenDate.Visible = false;
                    txtOpenDate.Visible = false;

                    lblNoInst.Visible = false;
                    txtNoInstl.Visible = false;
                    btnApproved.Visible = false;
                    btnCanApproved.Visible = false;

                    DivSelect.Visible = false;
                    DivReject.Visible = false;

                    txtNoInstl.ReadOnly = true;

                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtOpenDate.Text = date;
                    lblProcDate.Text = date;


                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));


                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    //if (userPermission != 30)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                    //                       "&txtTwo=" + "You Don't Have Permission for Approve" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}
                    //else
                    {
                        DataTable dt1 = new DataTable();

                        if (lblModule.Text == "1")
                        {
                            string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2'";
                            dt1 = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                        }
                        if (lblModule.Text == "6")
                        {
                            string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2' and FromCashCode='" + hdnCashCode.Text + "'";
                            dt1 = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                        }
                        if (lblModule.Text == "4")
                        {
                            string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                            dt1 = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                        }

                        if (dt1.Rows.Count == 1)
                        {
                            SingleRec.Text = "1";
                        }


                        if (dt1.Rows.Count <= 0)
                        {
                            string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                               "&txtTwo=" + "No Record Found for Approve" +
                                               "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                            Server.Transfer("Notify.aspx" + notifyMsg);

                        }
                        if (lblModule.Text == "1")
                        {
                            gv1Verify();
                        }
                        if (lblModule.Text == "6")
                        {
                            gv3Verify();
                        }
                        if (lblModule.Text == "4")
                        {
                            gv2Verify();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        private void gv1Verify()
        {
            string sqlquery3 = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2'";
            gvLoanInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvLoanInfo, "A2ZCSMCUS");
        }

        private void gv3Verify()
        {
            string sqlquery3 = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2' and FromCashCode='" + hdnCashCode.Text + "'";
            gvLoanInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvLoanInfo, "A2ZCSMCUS");
        }
        private void gv2Verify()
        {
            string sqlquery3 = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
            gvLoanInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvLoanInfo, "A2ZCSMCUS");
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {

                    Button c = (Button)sender;
                    GridViewRow r = (GridViewRow)c.NamingContainer;
                    Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");
                    int ap = Converter.GetInteger(AppNo.Text);

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ap);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptApproveLoanReport");

                    Response.Redirect("ReportServer.aspx", false);

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void UpdatedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "Acc Type : " + lblAccTypeDesc.Text;

            b = "Generated New Account No.";
            c = string.Format(lblAccNo.Text);


            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //----------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Acc Type : " + lblAccTypeDesc.Text;

            //string b = "Generated New Account No.";
            //string c = string.Format(lblAccNo.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append(c);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + lblMemNo.Text + "' and AccType='" + lblAType.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            lblNewAccNo.Text = Converter.GetString(newaccno);
        }


        protected void GenerateNewAccNo()
        {
            GetAccountCount();

            string input1 = Converter.GetString(lblCuNo.Text).Length.ToString();
            string input2 = Converter.GetString(lblMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(lblNewAccNo.Text).Length.ToString();

            string result1 = "";
            string result2 = "";
            string result3 = "";

            if (input1 == "1")
            {
                result1 = "000";
            }
            if (input1 == "2")
            {
                result1 = "00";
            }
            if (input1 == "3")
            {
                result1 = "0";
            }

            if (input2 == "1")
            {
                result2 = "0000";
            }
            if (input2 == "2")
            {
                result2 = "000";
            }
            if (input2 == "3")
            {
                result2 = "00";
            }
            if (input2 == "4")
            {
                result2 = "0";
            }

            if (input3 == "1")
            {
                result3 = "000";
            }
            if (input3 == "2")
            {
                result3 = "00";
            }
            if (input3 == "3")
            {
                result3 = "0";
            }

            if (input1 != "4" && input2 != "5" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + result1 + lblCuNo.Text + result2 + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 != "5" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + result1 + lblCuNo.Text + result2 + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + result1 + lblCuNo.Text + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + result1 + lblCuNo.Text + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + lblCuNo.Text + result2 + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + lblCuNo.Text + result2 + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 == "5" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + lblCuNo.Text + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }
            if (input1 == "4" && input2 == "5" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + lblCuType.Text + lblCuNo.Text + lblMemNo.Text + lblNewAccNo.Text;
            }

        }


        protected void BtnApproved_Click(object sender, EventArgs e)
        {
            try
            {


                if (lblAType.Text == "53")
                {
                    string qry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCuNo.Text + "' AND MemNo = '" + lblMemNo.Text + "' AND AccType = '" + lblAType.Text + "' AND AccStatus < 98 AND AccOpenDate > '" + "2017-06-30" + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        MsgFlag.Text = "1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Already Exist');", true);
                        return;
                    }
                }





                //Button b = (Button)sender;
                //GridViewRow r = (GridViewRow)b.NamingContainer;
                //Label CTyNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcutype");
                //Label CrNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcno");
                //Label memNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[6].FindControl("lblMemNo");
                //Label LoanType = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[7].FindControl("lblAccType");
                //Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");

                ////TextBox txtLoanSancAmt = (TextBox)gvLoanInfo.Rows[r.RowIndex].Cells[10].FindControl("txtLoanSancAmt");




                //double Amount = Converter.GetDouble(txtSancAmount.Text);

                double SancAmt = Converter.GetDouble(txtSancAmount.Text);

                //int a = Converter.GetSmallInteger(CTyNo.Text);
                //int c = Converter.GetInteger(CrNo.Text);
                //int d = Converter.GetInteger(memNo.Text);
                //int ap = Converter.GetInteger(AppNo.Text);
                //int loantype = Converter.GetInteger(LoanType.Text);

                //int type = Converter.GetInteger(loantype);

                //int lApp = Converter.GetInteger(ap);
                //Int16 CuType = Converter.GetSmallInteger(a);
                //lblCuType.Text = Converter.GetString(CuType);


                //int CNo = Converter.GetInteger(c);
                //lblCuNo.Text = Converter.GetString(CNo);

                //int MemNo = Converter.GetInteger(d);
                //lblMemNo.Text = Converter.GetString(MemNo);

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime Procdt = Converter.GetDateTime(dto.ProcessDate);


                Int16 ApprovBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                DateTime ApprovByDate = Converter.GetDateTime(dto.ProcessDate);


                string CheckUp = "UPDATE A2ZLOAN SET LoanProcFlag=13,LoanSancDate='" + Procdt + "', LoanSancAmount='" + SancAmt + "', ApprovBy='" + ApprovBy + "', ApprovByDate='" + ApprovByDate + "' where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and LoanApplicationNo='" + hdnAppNo.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {
                    string qry1 = "SELECT Id,LoanApplicationDate,LoanIntRate,LoanGrace,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,LoanFirstInstlDt,LoanExpiryDate,AccPeriod FROM A2ZLOAN WHERE LoanApplicationNo='" + hdnAppNo.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                    if (dt1.Rows.Count > 0)
                    {
                        DateTime AppDt = Converter.GetDateTime(dt1.Rows[0]["LoanApplicationDate"]);
                        lblAppDate.Text = Converter.GetString(AppDt);

                        lblIntRate.Text = Converter.GetString(dt1.Rows[0]["LoanIntRate"]);
                        lblGrace.Text = Converter.GetString(dt1.Rows[0]["LoanGrace"]);
                        lblInstlAmt.Text = Converter.GetString(dt1.Rows[0]["LoanInstlAmt"]);
                        lblLastInstlAmt.Text = Converter.GetString(dt1.Rows[0]["LoanLastInstlAmt"]);
                        lblNoInstl.Text = Converter.GetString(dt1.Rows[0]["LoanNoInstl"]);
                        DateTime FirstDt = Converter.GetDateTime(dt1.Rows[0]["LoanFirstInstlDt"]);
                        lblFirstInstlDt.Text = Converter.GetString(FirstDt);
                        DateTime ExpDt = Converter.GetDateTime(dt1.Rows[0]["LoanExpiryDate"]);
                        lblExpDate.Text = Converter.GetString(ExpDt);

                        lblAccPeriod.Text = Converter.GetString(dt1.Rows[0]["AccPeriod"]);



                    }


                    //Int16 MainCode = Converter.GetSmallInteger(lblAType.Text);
                    //A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    //if (getDTO.AccTypeCode > 0)
                    //{
                    //    lblAtyClass.Text = Converter.GetString(getDTO.AccTypeClass);
                    //    lblAccFlag.Text = Converter.GetString(getDTO.AccFlag);
                    //    lblAccTypeDesc.Text = Converter.GetString(getDTO.AccTypeDescription);
                    //}


                    //int GLCode = Converter.GetInteger(hdnCashCode.Text);
                    //Int16 RecType = Converter.GetSmallInteger(LoanType.Text);
                    //lblAType.Text = Converter.GetString(LoanType.Text);

                    //if (lblAccFlag.Text == "2")
                    //{
                    GenerateNewAccNo();

                    //A2ZRECCTRLNODTO get1DTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                    //lblAccNo.Text = hdnCashCode.Value + get1DTO.RecLastNo;
                    UpdatedMSG();
                    //}
                    //else
                    //{
                    //    lblAccNo.Text = "0";
                    //}


                    double IntRate = Converter.GetDouble(lblIntRate.Text);
                    double Grace = Converter.GetDouble(lblGrace.Text);
                    double InstAmt = Converter.GetDouble(lblInstlAmt.Text);
                    double LInstAmt = Converter.GetDouble(lblLastInstlAmt.Text);

                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    DateTime expdate = DateTime.ParseExact(txtLoanExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                    string accqry = "SELECT * FROM A2ZACCOUNT WHERE AccType='" + lblAType.Text + "' and AccNo='" + lblAccNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + lblMemNo.Text + "'";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(accqry, "A2ZCSMCUS"));
                    if (result < 0)
                    {
                        string qry = "INSERT INTO A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccLoanAppNo,AccLoanSancAmt,AccLoanSancDate,AccIntRate,AccLoanGrace,AccLoanInstlAmt,AccLoanLastInstlAmt,AccNoInstl,AccLoanExpiryDate,AccStatus,AccAtyClass,AccTodaysOpBalance,AccPeriod)VALUES('" + lblAType.Text + "','" + lblAccNo.Text + "','" + lblCuType.Text + "','" + lblCuNo.Text + "','" + lblMemNo.Text + "','" + opdate + "','" + hdnAppNo.Text + "','" + SancAmt + "','" + lblAppDate.Text + "','" + IntRate + "','" + Grace + "','" + InstAmt + "','" + LInstAmt + "','" + lblNoInstl.Text + "','" + expdate + "',1,'" + lblAtyClass.Text + "',0,'" + lblAccPeriod.Text + "')";
                        int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                    }


                    string strQuery1 = "UPDATE A2ZACGUAR SET  LOanAccNo= '" + lblAccNo.Text + "' WHERE  LoanApplicationNo='" + hdnAppNo.Text + "'";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string strQuery2 = "UPDATE A2ZSHGUAR SET  LoanAccNo= '" + lblAccNo.Text + "' WHERE  LoanApplicationNo='" + hdnAppNo.Text + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));


                    DataTable dt = new DataTable();
                    if (lblModule.Text == "1")
                    {
                        gv1Verify();
                        string CheckQuery = "SELECT CuType,CuNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }

                    if (lblModule.Text == "6")
                    {
                        gv3Verify();
                        string CheckQuery = "SELECT CuType,CuNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2' and FromCashCode='" + hdnCashCode.Text + "'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }

                    if (lblModule.Text == "4")
                    {
                        gv2Verify();
                        string CheckQuery = "SELECT CuType,CuNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }

                    lblInstallmentAmount.Visible = false;
                    txtInstallmentAmount.Visible = false;
                    lblLastInstallmentAmount.Visible = false;
                    txtLastInstallmentAmount.Visible = false;
                    lblSancAmt.Visible = false;
                    txtSancAmount.Visible = false;
                    lblNoInst.Visible = false;
                    txtNoInstl.Visible = false;
                    btnApproved.Visible = false;
                    btnCanApproved.Visible = false;

                    DivSelect.Visible = false;

                    if (dt.Rows.Count == 1)
                    {
                        SingleRec.Text = "1";
                    }

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;
                        lblmsg1.Visible = true;
                        lblmsg2.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnApprove_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {

                //Button b = (Button)sender;
                //GridViewRow r = (GridViewRow)b.NamingContainer;
                //Label CTyNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcutype");
                //Label CrNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcno");
                //Label memNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[6].FindControl("lblMemNo");
                //Label LoanType = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[7].FindControl("lblAccType");
                //Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");

                //int a = Converter.GetSmallInteger(CTyNo.Text);
                //int c = Converter.GetInteger(CrNo.Text);
                //int d = Converter.GetInteger(memNo.Text);
                //int lApp = Converter.GetInteger(AppNo.Text);
                //int loantype = Converter.GetInteger(LoanType.Text);
                //int type = Converter.GetInteger(loantype);
                //lblApplicationNo.Text = Converter.GetString(lApp);
                //Int16 CuType = Converter.GetSmallInteger(a);
                //int CNo = Converter.GetInteger(c);
                //int MemNo = Converter.GetInteger(d);

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime Procdt = Converter.GetDateTime(dto.ProcessDate);

                string CheckUp = "UPDATE A2ZLOAN SET LoanStatus=99,LoanStatNote = '" + txtRejectNote.Text + "' where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and LoanApplicationNo='" + hdnAppNo.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                    string strQuery1 = "UPDATE A2ZACGUAR SET  AccStat = '99', AccStatDate= '" + Procdt + "' WHERE  LoanApplicationNo='" + hdnAppNo.Text + "'";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string strQuery2 = "UPDATE A2ZSHGUAR SET  AccStat = '99', AccStatDate= '" + Procdt + "' WHERE  LoanApplicationNo='" + hdnAppNo.Text + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));

                    string strQuery3 = "UPDATE A2ZPRGUAR SET  AccStat = '99', AccStatDate= '" + Procdt + "' WHERE  LoanApplicationNo='" + hdnAppNo.Text + "'";
                    int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery3, "A2ZCSMCUS"));


                    UpdateAccDepositGarantor();
                    UpdateAccShareGarantor();

                    DataTable dt = new DataTable();
                    if (lblModule.Text == "1")
                    {
                        gv1Verify();
                        string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }

                    if (lblModule.Text == "6")
                    {
                        gv3Verify();
                        string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='2' and FromCashCode='" + hdnCashCode.Text + "'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }
                    if (lblModule.Text == "4")
                    {
                        gv2Verify();
                        string CheckQuery = "SELECT CuType,CuNo,MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanIntRate,LoanApplicationAmt,LoanApplicationAmt,LoanTotGuarantorAmt from A2ZLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                    }

                    if (dt.Rows.Count == 1)
                    {
                        SingleRec.Text = "1";
                    }

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;
                        lblmsg1.Visible = true;
                        lblmsg2.Visible = true;
                    }

                    DivReject.Visible = false;
                    BtnReject.Visible = false;



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReject_Click Problem');</script>");
                //throw ex;
            }

        }



        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {

                    DivSelect.Visible = true;
                    Div1.Visible = true;
                    btnApproved.Visible = true;
                    btnCanApproved.Visible = true;


                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label CTyNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcutype");
                    Label CrNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcno");
                    Label memNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[6].FindControl("lblMemNo");
                    Label LoanType = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[7].FindControl("lblAccType");
                    Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");
                    Label AppDate = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[9].FindControl("lblADate");

                    DateTime opdate1 = DateTime.ParseExact(AppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    string date = opdate1.ToString("dd/MM/yyyy");
                    lAppDate.Text = date;


                    //if (SingleRec.Text != "1")
                    //{
                    //    if (Session["PreviousRowIndex"] != null)
                    //    {
                    //        var previousRowIndex = (int)Session["PreviousRowIndex"];
                    //        GridViewRow PreviousRow = gvLoanInfo.Rows[previousRowIndex];
                    //        PreviousRow.ForeColor = Color.Black;
                    //    }

                    //    GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                    //    row.ForeColor = Color.Blue;
                    //    Session["PreviousRowIndex"] = row.RowIndex;
                    //}



                    int a = Converter.GetSmallInteger(CTyNo.Text);
                    int c = Converter.GetInteger(CrNo.Text);
                    int d = Converter.GetInteger(memNo.Text);
                    int ap = Converter.GetInteger(AppNo.Text);
                    int loantype = Converter.GetInteger(LoanType.Text);

                    int type = Converter.GetInteger(loantype);

                    lblAType.Text = Converter.GetString(type);

                    Int16 MainCode = Converter.GetSmallInteger(lblAType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        lblAtyClass.Text = Converter.GetString(getDTO.AccTypeClass);
                        lblAccFlag.Text = Converter.GetString(getDTO.AccFlag);
                        lblAccTypeDesc.Text = Converter.GetString(getDTO.AccTypeDescription);
                        lblchk1Hide.Text = Converter.GetString(getDTO.AccessT1);
                        lblchk2Hide.Text = Converter.GetString(getDTO.AccessT2);
                        lblchk3Hide.Text = Converter.GetString(getDTO.AccessT3);
                    }

                    int lApp = Converter.GetInteger(ap);
                    hdnAppNo.Text = Converter.GetString(lApp);

                    Int16 CuType = Converter.GetSmallInteger(a);
                    lblCuType.Text = Converter.GetString(CuType);


                    int CNo = Converter.GetInteger(c);
                    lblCuNo.Text = Converter.GetString(CNo);

                    int MemNo = Converter.GetInteger(d);
                    lblMemNo.Text = Converter.GetString(MemNo);

                    if ((lblCuType.Text == "1" && lblchk1Hide.Text != "1") ||
                       (lblCuType.Text == "2" && lblchk2Hide.Text != "1") ||
                       (lblCuType.Text == "3" && lblchk3Hide.Text != "1"))
                    {
                        MsgFlag.Text = "1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Open an Account');", true);
                        return;
                    }

                    if (lblAccFlag.Text != "3")
                    {
                        string qry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCuNo.Text + "' AND MemNo = '" + lblMemNo.Text + "' AND AccType = '" + lblAType.Text + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            MsgFlag.Text = "1";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Already Exist');", true);
                            return;
                        }

                    }

                    string qry1 = "SELECT Id,LoanApplicationAmt,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,AccPeriod FROM A2ZLOAN WHERE LoanApplicationNo='" + lApp + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                    if (dt1.Rows.Count > 0)
                    {
                        DivSelect.Visible = true;
                        btnApproved.Visible = true;
                        btnCanApproved.Visible = true;

                        lblSancAmt.Visible = true;
                        txtSancAmount.Visible = true;

                        lblOpenDate.Visible = true;
                        txtOpenDate.Visible = true;

                        if (lblAtyClass.Text == "6")
                        {
                            lblNoInst.Visible = true;
                            txtNoInstl.Visible = true;
                            lblInstallmentAmount.Visible = true;
                            txtInstallmentAmount.Visible = true;
                            lblLastInstallmentAmount.Visible = true;
                            txtLastInstallmentAmount.Visible = true;

                            lblPeriod.Visible = false;
                            txtPeriod.Visible = false;
                        }

                        txtSancAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanApplicationAmt"]));
                        lblOrgSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanApplicationAmt"]));

                        txtInstallmentAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanInstlAmt"]));

                        txtLastInstallmentAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanLastInstlAmt"]));

                        txtNoInstl.Text = Converter.GetString(dt1.Rows[0]["LoanNoInstl"]);

                        txtPeriod.Text = Converter.GetString(dt1.Rows[0]["AccPeriod"]);

                        LoanExpiryDate();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void BtnRejectSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {
                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label CTyNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcutype");
                    Label CrNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcno");
                    Label memNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[6].FindControl("lblMemNo");
                    Label LoanTypeR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[7].FindControl("lblAccType");
                    Label AppNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");

                    if (SingleRec.Text != "1")
                    {
                        if (Session["PreviousRowIndex"] != null)
                        {
                            var previousRowIndex = (int)Session["PreviousRowIndex"];
                            GridViewRow PreviousRow = gvLoanInfo.Rows[previousRowIndex];
                            PreviousRow.ForeColor = Color.Black;
                        }

                        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                        row.ForeColor = Color.Blue;
                        Session["PreviousRowIndex"] = row.RowIndex;
                    }


                    int lAppR = Converter.GetInteger(AppNoR.Text);
                    hdnAppNo.Text = Converter.GetString(lAppR);

                    Int16 CuTypeR = Converter.GetSmallInteger(CTyNoR.Text);
                    lblCuType.Text = Converter.GetString(CuTypeR);


                    int CNoR = Converter.GetInteger(CrNoR.Text);
                    lblCuNo.Text = Converter.GetString(CNoR);

                    int MemNoR = Converter.GetInteger(memNoR.Text);
                    lblMemNo.Text = Converter.GetString(MemNoR);

                    DivReject.Visible = true;
                    BtnReject.Visible = true;
                    txtRejectNote.Focus();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void txtInstallmentAmount_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(txtSancAmount.Text);
            double b = Converter.GetDouble(txtNoInstl.Text);
            double c = Converter.GetDouble(txtInstallmentAmount.Text);

            double d = Math.Abs((c * (b - 1)) - a);

            txtInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));
            txtLastInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }


        //---------------------------------------------------------------------------------------------
        private void UpdateAccDepositGarantor()
        {
            try
            {

                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, MemNo, AccType,AccNo,AccAmount From A2ZACGUAR Where LoanApplicationNo='" + hdnAppNo.Text + "'", "A2ZCSMCUS");

                for (int i = 0; i < dt1.Rows.Count; ++i)
                {
                    int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                    int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                    int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                    int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                    Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                    double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);

                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccLienAmt From A2ZACCOUNT WHERE AccType='" + AccType + "' and AccNo='" + AccNo + "' and CuType='" + Cutype + "' and CuNo='" + Cuno + "' and MemNo='" + Memno + "'", "A2ZCSMCUS");
                    LienAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccLienAmt"]));
                    double TotalLienAmt = Converter.GetDouble(LienAmount.Text);

                    double subLienAmt = (TotalLienAmt - LienAmt);
                    int Astatus = Converter.GetInteger(50);
                    if (subLienAmt == 0)
                    {
                        Astatus = Converter.GetInteger(1);
                    }
                    string strQuery1 = "UPDATE A2ZACCOUNT SET  AccStatus = '" + Astatus + "', AccLienAmt= '" + subLienAmt + "' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateAccDepositGarantor Problem');</script>");
                //throw ex;
            }
        }

        private void UpdateAccShareGarantor()
        {

            try
            {

                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, MemNo, AccType,AccNo,AccAmount From A2ZSHGUAR Where LoanApplicationNo='" + hdnAppNo.Text + "'", "A2ZCSMCUS");


                for (int i = 0; i < dt1.Rows.Count; ++i)
                {
                    int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                    int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                    int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                    int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                    Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);

                    string strQuery1 = "UPDATE A2ZACCOUNT SET  AccStatus = '1' WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateAccShareGarantor Problem');</script>");
                //throw ex;
            }
        }
        private void AccountNotFoundMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account does not created!Account has been created Automatically???');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account has been created Automatically');", true);
            return;

        }
        private void AccountCrMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account has been created Automatically');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account has been created Automatically');", true);
            return;

        }

        private void InvalidSancAmtMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Sanction Amount');", true);
            return;

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtLoanSancAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox b = (TextBox)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                TextBox txtLoanSancAmt = (TextBox)gvLoanInfo.Rows[r.RowIndex].Cells[11].FindControl("txtLoanSancAmt");

                double SancAmount = Converter.GetDouble(txtLoanSancAmt.Text);

                txtLoanSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", SancAmount));

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtLoanSancAmt_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtSancAmount_TextChanged(object sender, EventArgs e)
        {
            double SancAmount = Converter.GetDouble(txtSancAmount.Text);
            double OSancAmount = Converter.GetDouble(lblOrgSancAmt.Text);

            if (SancAmount == 0 || SancAmount > OSancAmount)
            {
                txtSancAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", lblOrgSancAmt.Text));
                txtSancAmount.Focus();
                InvalidSancAmtMsg();
                return;
            }



            txtSancAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", SancAmount));


            Int16 RoundFlag = 0;
            double a = Converter.GetDouble(txtSancAmount.Text);
            double b = Converter.GetDouble(txtNoInstl.Text);
            double c = a / b;

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + lblAType.Text + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                if (RoundFlag == 1)
                {
                    c = Math.Round(c);

                }

                if (RoundFlag == 2)
                {
                    c = Math.Ceiling(c);
                }

            }

            txtInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

            double d = Math.Abs((c * (b - 1)) - a);

            txtLastInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }


        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidInputDate()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Input Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void txtOpenDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtOpenDate.Text.Length.ToString();
            if (date != "10")
            {
                InvalidInputDate();
                txtOpenDate.Text = lblProcDate.Text;
                txtOpenDate.Focus();
                return;
            }

            DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate3 = DateTime.ParseExact(lAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Month1 = opdate1.Month;
            int Month2 = opdate2.Month;

            if (opdate1 > opdate2 || Month1 != Month2 || opdate1 < opdate3)
            {
                InvalidDateMSG();
                txtOpenDate.Text = lblProcDate.Text;
                txtOpenDate.Focus();
                return;
            }
        }

        //protected void txtNoInstl_TextChanged(object sender, EventArgs e)
        //{
        //    Int16 RoundFlag = 0;
        //    double a = Converter.GetDouble(txtSancAmount.Text);
        //    double b = Converter.GetDouble(txtNoInstl.Text);
        //    double c = a / b;



        //    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + ddlAccType.SelectedValue + "'", "A2ZCSMCUS");
        //    if (dt1.Rows.Count > 0)
        //    {
        //        RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

        //        if (RoundFlag == 1)
        //        {
        //            c = Math.Round(c);

        //        }

        //        if (RoundFlag == 2)
        //        {
        //            c = Math.Ceiling(c);
        //        }
        //        //if (RoundFlag == 3)
        //        //{
        //        //    c = c;
        //        //}
        //    }

        //    txtInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

        //    double d = Math.Abs((c * (b - 1)) - a);

        //    txtLastInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));

        //    DateTime Matdate = new DateTime();
        //    Matdate = DateTime.ParseExact(lblAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtNoInstl.Text));
        //    DateTime dt = Converter.GetDateTime(Matdate);
        //    string date = dt.ToString("dd/MM/yyyy");
        //    lblLoanExpiryDt.Text = date;
        //}


        private void LoanExpiryDate()
        {
            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (lblAtyClass.Text == "5")
            {
                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
            }
            else 
            {
                Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtNoInstl.Text));
            }
           
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtLoanExpDate.Text = date;
        }

        protected void BtnCanReject_Click(object sender, EventArgs e)
        {
            DivReject.Visible = false;
        }

        protected void BtnCanApproved_Click(object sender, EventArgs e)
        {
            DivSelect.Visible = false;
          
            Div1.Visible = false;
        }

    }

}