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
    public partial class CSApproveStaffLoanApplication : System.Web.UI.Page
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
                    lblNoInst.Visible = false;
                    txtNoInstl.Visible = false;
                    btnApproved.Visible = false;

                    DivSelect.Visible = false;
                    DivReject.Visible = false;

                    txtNoInstl.ReadOnly = true;

                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;
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
                        DataTable dt = new DataTable();

                        
                        string CheckQuery = "SELECT MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanApplicationAmt from A2ZSTAFFLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                        
                        
                        if (dt.Rows.Count <= 0)
                        {
                            string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                               "&txtTwo=" + "No Record Found for Approve" +
                                               "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                            Server.Transfer("Notify.aspx" + notifyMsg);

                        }
                        
                            gv1Verify();
                        
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
            string sqlquery3 = "SELECT MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanApplicationAmt,LoanApplicationAmt from A2ZSTAFFLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
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
                    Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblappno");
                    int ap = Converter.GetInteger(AppNo.Text);

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ap);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStaffApproveLoanReport");

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


            string input2 = Converter.GetString(lblMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(lblNewAccNo.Text).Length.ToString();


            string result2 = "";
            string result3 = "";

            if (input2 == "1")
            {
                result2 = "000";
            }
            if (input2 == "2")
            {
                result2 = "00";
            }
            if (input2 == "3")
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

            if (input2 != "4" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + result2 + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }
            if (input2 != "4" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + result2 + lblMemNo.Text + lblNewAccNo.Text;
            }
            if (input2 == "4" && input3 != "4")
            {
                lblAccNo.Text = lblAType.Text + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }
            if (input2 == "4" && input3 == "4")
            {
                lblAccNo.Text = lblAType.Text + lblMemNo.Text + lblNewAccNo.Text;
            }


        }



        protected void BtnApproved_Click(object sender, EventArgs e)
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


                string CheckUp = "UPDATE A2ZSTAFFLOAN SET LoanProcFlag=13,LoanSancDate='" + Procdt + "', LoanSancAmount='" + SancAmt + "', ApprovBy='" + ApprovBy + "', ApprovByDate='" + ApprovByDate + "' where CuType=0 and CuNo=0 and LoanApplicationNo='" + hdnAppNo.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {
                    string qry1 = "SELECT Id,LoanIntRate,LoanGrace,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,LoanFirstInstlDt,LoanExpiryDate FROM A2ZSTAFFLOAN WHERE LoanApplicationNo='" + hdnAppNo.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                    if (dt1.Rows.Count > 0)
                    {
                        lblIntRate.Text = Converter.GetString(dt1.Rows[0]["LoanIntRate"]);
                        lblGrace.Text = Converter.GetString(dt1.Rows[0]["LoanGrace"]);
                        lblInstlAmt.Text = Converter.GetString(dt1.Rows[0]["LoanInstlAmt"]);
                        lblLastInstlAmt.Text = Converter.GetString(dt1.Rows[0]["LoanLastInstlAmt"]);
                        lblNoInstl.Text = Converter.GetString(dt1.Rows[0]["LoanNoInstl"]);
                        DateTime FirstDt = Converter.GetDateTime(dt1.Rows[0]["LoanFirstInstlDt"]);
                        lblFirstInstlDt.Text = Converter.GetString(FirstDt);
                        DateTime ExpDt = Converter.GetDateTime(dt1.Rows[0]["LoanExpiryDate"]);
                        lblExpDate.Text = Converter.GetString(ExpDt);

                    }


                   
                    GenerateNewAccNo();

                    
                  
                    string accqry = "SELECT * FROM A2ZACCOUNT WHERE AccType='" + lblAType.Text + "' and CuType=0 and CuNo=0 and MemNo='" + lblMemNo.Text + "' AND AccStatus < 98";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(accqry, "A2ZCSMCUS");
                    if (dt2.Rows.Count == 0)
                    {
                        string qry = "INSERT INTO A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccLoanAppNo,AccLoanSancAmt,AccLoanSancDate,AccIntRate,AccLoanGrace,AccLoanInstlAmt,AccLoanLastInstlAmt,AccNoInstl,AccLoanExpiryDate,AccStatus,AccAtyClass,AccTodaysOpBalance)VALUES('" + lblAType.Text + "','" + lblAccNo.Text + "',0,0,'" + lblMemNo.Text + "','" + Procdt + "','" + hdnAppNo.Text + "','" + SancAmt + "','" + Procdt + "','" + lblIntRate.Text + "','" + lblGrace.Text + "','" + txtInstallmentAmount.Text + "','" + txtLastInstallmentAmount.Text + "','" + lblNoInstl.Text + "','" + lblExpDate.Text + "',1,'" + lblAtyClass.Text + "',0)";
                        int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                    }
                    else 
                    {
                        lblAccNo.Text = Converter.GetString(dt2.Rows[0]["AccNo"]);
                        lblLastSancAmt.Text = Converter.GetString(dt2.Rows[0]["AccLoanSancAmt"]);

                        double lastsancamt = Converter.GetDouble(lblLastSancAmt.Text);
                        double totsancamt = (lastsancamt + SancAmt);


                        string accqry1 = "UPDATE A2ZACCOUNT SET AccLoanSancDate='" + Procdt + "', AccLoanSancAmt='" + totsancamt + "', AccIntRate='" + lblIntRate.Text + "', AccLoanInstlAmt='" + txtInstallmentAmount.Text + "',AccLoanLastInstlAmt='" + txtLastInstallmentAmount.Text + "',AccNoInstl='" + lblNoInstl.Text + "',AccLoanExpiryDate='" + lblExpDate.Text + "',AccLoanAppNo='" + hdnAppNo.Text + "',AccOpenDate='" + Procdt + "' where CuType=0 and CuNo=0 and MemNo='" + lblMemNo.Text + "' and AccType='" + lblAType.Text + "' and AccNo='" + lblAccNo.Text + "' ";
                        int result1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(accqry1, "A2ZCSMCUS"));
                    }
                    


                    //string accqry = "SELECT * FROM A2ZACCOUNT WHERE AccType='" + lblAType.Text + "' and AccNo='" + lblAccNo.Text + "' and CuType=0 and CuNo=0 and MemNo='" + lblMemNo.Text + "'";
                    //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(accqry, "A2ZCSMCUS"));
                    //if (result < 0)
                    //{
                    //    string qry = "INSERT INTO A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccLoanAppNo,AccLoanSancAmt,AccLoanSancDate,AccIntRate,AccLoanGrace,AccLoanInstlAmt,AccLoanLastInstlAmt,AccNoInstl,AccLoanExpiryDate,AccStatus,AccAtyClass,AccTodaysOpBalance)VALUES('" + lblAType.Text + "','" + lblAccNo.Text + "',0,0,'" + lblMemNo.Text + "','" + Procdt + "','" + hdnAppNo.Text + "','" + SancAmt + "','" + Procdt + "','" + lblIntRate.Text + "','" + lblGrace.Text + "','" + txtInstallmentAmount.Text + "','" + txtLastInstallmentAmount.Text + "','" + lblNoInstl.Text + "','" + lblExpDate.Text + "',1,'" + lblAtyClass.Text + "',0)";
                    //    int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                    //}

                    UpdatedMSG();
                    
                    DataTable dt = new DataTable();
                   
                        gv1Verify();
                        string CheckQuery = "SELECT AccType,LoanApplicationNo,LoanApplicationDate,LoanApplicationAmt,LoanApplicationAmt from A2ZSTAFFLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                   

                   

                    lblInstallmentAmount.Visible = false;
                    txtInstallmentAmount.Visible = false;
                    lblLastInstallmentAmount.Visible = false;
                    txtLastInstallmentAmount.Visible = false;
                    lblSancAmt.Visible = false;
                    txtSancAmount.Visible = false;
                    lblNoInst.Visible = false;
                    txtNoInstl.Visible = false;
                    btnApproved.Visible = false;

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

                

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime Procdt = Converter.GetDateTime(dto.ProcessDate);

                string CheckUp = "UPDATE A2ZSTAFFLOAN SET LoanStatus=99,LoanStatNote = '" + txtRejectNote.Text + "' where CuType=0 and CuNo=0 and LoanApplicationNo='" + hdnAppNo.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                    
                   
                    DataTable dt = new DataTable();
                    
                        gv1Verify();
                        string CheckQuery = "SELECT MemNo,AccType,LoanApplicationNo,LoanApplicationDate,LoanApplicationAmt,LoanApplicationAmt from A2ZSTAFFLOAN where LoanProcFlag='11' and LoanStatus !='99' and AccTypeMode !='1'";
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");
                   

                    

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

                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    
                    Label memNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[3].FindControl("lblMemNo");
                    Label LoanType = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblAccType");
                    Label AppNo = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblappno");
                    //Label AppDate = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[9].FindControl("LoanApplicationDate");

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


                    //BtnSelect = "SELECTED";

                    //TextBox txtLoanSancAmt = (TextBox)gvLoanInfo.Rows[r.RowIndex].Cells[10].FindControl("txtLoanSancAmt");

                    //txtSancAmount.Text = txtLoanSancAmt.Text;

                    //double Amount = Converter.GetDouble(txtLoanSancAmt.Text);
                    //double SancAmt = Converter.GetDouble(Amount);


                    //DateTime date = Converter.GetDateTime(AppDate);
                    //lblAppDate.Text = Converter.GetString(date);



                   
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
                    }

                    int lApp = Converter.GetInteger(ap);
                    hdnAppNo.Text = Converter.GetString(lApp);

                   

                    int MemNo = Converter.GetInteger(d);
                    lblMemNo.Text = Converter.GetString(MemNo);

                    string qry1 = "SELECT Id,LoanApplicationAmt,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl FROM A2ZSTAFFLOAN WHERE LoanApplicationNo='" + lApp + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                    if (dt1.Rows.Count > 0)
                    {
                        DivSelect.Visible = true;
                        btnApproved.Visible = true;

                        lblSancAmt.Visible = true;
                        txtSancAmount.Visible = true;

                        if (lblAtyClass.Text == "6")
                        {
                            lblNoInst.Visible = true;
                            txtNoInstl.Visible = true;
                            lblInstallmentAmount.Visible = true;
                            txtInstallmentAmount.Visible = true;
                            lblLastInstallmentAmount.Visible = true;
                            txtLastInstallmentAmount.Visible = true;
                        }

                        txtSancAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanApplicationAmt"]));

                        txtInstallmentAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanInstlAmt"]));

                        txtLastInstallmentAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt1.Rows[0]["LoanLastInstlAmt"]));

                        txtNoInstl.Text = Converter.GetString(dt1.Rows[0]["LoanNoInstl"]);
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
                    
                    Label memNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[3].FindControl("lblMemNo");
                    Label LoanTypeR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[4].FindControl("lblAccType");
                    Label AppNoR = (Label)gvLoanInfo.Rows[r.RowIndex].Cells[5].FindControl("lblappno");

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
                TextBox txtLoanSancAmt = (TextBox)gvLoanInfo.Rows[r.RowIndex].Cells[10].FindControl("txtLoanSancAmt");

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


    }

}