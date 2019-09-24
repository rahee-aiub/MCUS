using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLFinalAccountsView : System.Web.UI.Page
    {
        TextBox tb = new TextBox();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);

            if (!IsPostBack)
            {
                lblRepFlag.Text = (string)Session["SlblRepFlag"];

                if (lblRepFlag.Text != "1" && lblRepFlag.Text != "2" && lblRepFlag.Text != "3" && lblRepFlag.Text != "4")
                {

                    txtfdate.Text = (string)Session["Stxtfdate"];
                    txttdate.Text = (string)Session["Stxttdate"];

                    lblReportOpt.Text = (string)Session["SReportOpt"];


                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    gvHeader();

                    gvHeaderInfo.Visible = true;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = false;

                    BtnBack.Visible = false;
                    //BtnPrint.Visible = false;

                    gvHead1.Visible = false;
                    gvHead2.Visible = false;
                    gvHead3.Visible = false;


                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();

                    int iMonthNo = dto.CurrentMonth;
                    DateTime dtDate = new DateTime(dto.CurrentYear, iMonthNo, 1);

                }
                else
                {
                    txtfdate.Text = (string)Session["Stxtfdate"];
                    txttdate.Text = (string)Session["Stxttdate"];
                    lblReportOpt.Text = (string)Session["SReportOpt"];

                    lblGLHeadNumber.Text = (string)Session["SlblGLHeadNumber"];
                    lblGLSubHeadNumber.Text = (string)Session["SlblGLSubHeadNumber"];
                    lblGLStatement.Text = (string)Session["SlblGLStatement"];

                    lblGLDesc.Text = (string)Session["SlblGLDesc"];

                    lblGLHead.Text = (string)Session["SlblGLHead"];
                    lblGLSubHead.Text = (string)Session["SlblGLSubHead"];
                }

                if (lblRepFlag.Text == "1")
                {
                    gvHead1.Visible = false;
                    gvHead1Detail();
                    gvHead2.Visible = false;
                   
                    gvHead3.Visible = false;
                   

                    gvHeaderInfo.Visible = true;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = false;

                    BtnBack.Visible = false;

                    gvHeader();
                }


                if (lblRepFlag.Text == "2")
                {
                    gvHead1.Visible = true;
                    gvHead1Detail();
                    gvHead2.Visible = false;
                    
                    gvHead3.Visible = false;
                   

                    gvHeaderInfo.Visible = false;
                    gvSubHeaderInfo.Visible = true;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = false;

                    BtnBack.Visible = true;

                    gvSubHeader();
                }

                if (lblRepFlag.Text == "3")
                {
                    gvHead1.Visible = true;
                    gvHead1Detail();
                    gvHead2.Visible = true;
                    gvHead2Detail();

                    gvHead3.Visible = false;

                    gvHeaderInfo.Visible = false;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = true;
                    gvStatementInfo.Visible = false;

                    BtnBack.Visible = true;

                    gvSubHeaderDetail();
                }

                if (lblRepFlag.Text == "4")
                {
                    gvHead1.Visible = true;
                    gvHead1Detail();
                    gvHead2.Visible = true;
                    gvHead2Detail();
                    gvHead3.Visible = true;
                    gvHead3Detail();

                    gvHeaderInfo.Visible = false;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = true;

                    GenerateGLStatement();
                }


            }


        }

        protected void SessionRemove()
        {

            Session["SlblRepFlag"] = string.Empty;
            //Session["Stxtfdate"] = string.Empty;
            //Session["Stxttdate"] = string.Empty;
            Session["SReportOpt"] = string.Empty;
            Session["SlblGLHeadNumber"] = string.Empty;
            Session["SlblGLSubHeadNumber"] = string.Empty;
            Session["SlblGLStatement"] = string.Empty;

            Session["SlblGLDesc"] = string.Empty;

            Session["SlblGLHead"] = string.Empty;
            Session["SlblGLSubHead"] = string.Empty;

        }

        protected void gvHeader()
        {
            if (lblReportOpt.Text == "1")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE CodeFlag=1 ORDER BY GLAccNo";
                gvHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHeaderInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "2")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 4 or GLAccType = 5) AND CodeFlag=1 ORDER BY GLAccNo";
                gvHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHeaderInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "3")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 1 or GLAccType = 2) AND CodeFlag=1 ORDER BY GLAccNo";
                gvHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHeaderInfo, "A2ZGLMCUS");
            }
        }

        protected void gvSubHeader()
        {
            if (lblReportOpt.Text == "1")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE Left(GLAccNo,3)='" + lblGLHead.Text + "' AND CodeFlag=2 ORDER BY GLAccNo";
                gvSubHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "2")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 4 or GLAccType = 5) AND Left(GLAccNo,3)='" + lblGLHead.Text + "' AND CodeFlag=2 ORDER BY GLAccNo";
                gvSubHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "3")
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 1 or GLAccType = 2) AND Left(GLAccNo,3)='" + lblGLHead.Text + "' AND CodeFlag=2 ORDER BY GLAccNo";
                gvSubHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderInfo, "A2ZGLMCUS");
            }
        }

        protected void gvSubHeaderDetail()
        {
            if (lblReportOpt.Text == "1")
            {
                string sqlquery3 = "SELECT GLAccNo,LTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE Left(GLAccNo,5)='" + lblGLSubHead.Text + "' AND CodeFlag=3 ORDER BY GLAccNo";
                gvSubHeaderDtlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderDtlInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "2")
            {
                string sqlquery3 = "SELECT GLAccNo,LTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 4 or GLAccType = 5) AND Left(GLAccNo,5)='" + lblGLSubHead.Text + "' AND CodeFlag=3 ORDER BY GLAccNo";
                gvSubHeaderDtlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderDtlInfo, "A2ZGLMCUS");
            }
            else if (lblReportOpt.Text == "3")
            {
                string sqlquery3 = "SELECT GLAccNo,LTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE (GLAccType = 1 or GLAccType = 2) AND  Left(GLAccNo,5)='" + lblGLSubHead.Text + "' AND CodeFlag=3 ORDER BY GLAccNo";
                gvSubHeaderDtlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderDtlInfo, "A2ZGLMCUS");
            }
        }

        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }

        protected void gvHead1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
        }

        protected void gvHead2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
        }


        protected void gvHead3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
        }
        protected void gvHeaderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
        }

        protected void gvSubHeaderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }


        protected void gvSubHeaderDtlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }


        protected void gvStatementInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
            try
            {
                SessionRemove();
                Response.Redirect("GLFinalAccountsReport.aspx");


                //ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                //  "click", @"<script>window.opener.location.href='GLFinalAccountsReport.aspx'; self.close();</script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSubHeaderInfo.Visible == true)
                {
                    BtnBack.Visible = false;

                    gvHead1.Visible = false;
                    gvHead2.Visible = false;
                    gvHead3.Visible = false;

                    gvHeaderInfo.Visible = true;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = false;

                    gvHeader();
                }

                if (gvSubHeaderDtlInfo.Visible == true)
                {
                    gvHead1.Visible = true;
                    gvHead2.Visible = false;
                    gvHead3.Visible = false;

                    gvHeaderInfo.Visible = false;
                    gvSubHeaderInfo.Visible = true;
                    gvSubHeaderDtlInfo.Visible = false;
                    gvStatementInfo.Visible = false;

                    gvSubHeader();


                    gvHead1Detail();

                }


                if (gvStatementInfo.Visible == true)
                {
                    //BtnPrint.Visible = false;

                    gvHead1.Visible = true;
                    gvHead2.Visible = true;
                    gvHead3.Visible = false;

                    gvHeaderInfo.Visible = false;
                    gvSubHeaderInfo.Visible = false;
                    gvSubHeaderDtlInfo.Visible = true;
                    gvStatementInfo.Visible = false;

                    gvSubHeaderDetail();

                    gvHead2Detail();

                    Session["SlblRepFlag"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnHdrSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lGLAccNo = (Label)gvHeaderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblGLAccNo");


                lblGLHeadNumber.Text = Converter.GetString(lGLAccNo.Text);
                lblGLHead.Text = lblGLHeadNumber.Text.Substring(0, 3);

                BtnBack.Visible = true;

                gvHeaderInfo.Visible = false;
                gvSubHeaderInfo.Visible = true;
                gvSubHeaderDtlInfo.Visible = false;


                gvSubHeader();


                gvHead1.Visible = true;
                gvHead2.Visible = false;
                gvHead1Detail();


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void BtnSubHdrSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lGLAccNo = (Label)gvSubHeaderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblGLAccNo");


                lblGLSubHeadNumber.Text = Converter.GetString(lGLAccNo.Text);
                lblGLSubHead.Text = lblGLSubHeadNumber.Text.Substring(0, 5);

                BtnBack.Visible = true;

                gvHeaderInfo.Visible = false;
                gvSubHeaderInfo.Visible = false;
                gvSubHeaderDtlInfo.Visible = true;

                gvSubHeaderDetail();

                gvHead2.Visible = true;
                gvHead2Detail();




            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }



        protected void BtnSubHdrDtlSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lGLAccNo = (Label)gvSubHeaderDtlInfo.Rows[r.RowIndex].Cells[0].FindControl("lblGLAccNo");
                Label lGLDesc = (Label)gvSubHeaderDtlInfo.Rows[r.RowIndex].Cells[1].FindControl("lblDesc");


                lblGLStatement.Text = Converter.GetString(lGLAccNo.Text);
                lblGLDesc.Text = Converter.GetString(lGLDesc.Text);

                //lblGLSubHead.Text = lblGLSubHeadNumber.Text.Substring(0, 5);

                BtnBack.Visible = true;

                BtnPrint.Visible = true;

                gvHeaderInfo.Visible = false;
                gvSubHeaderInfo.Visible = false;
                gvSubHeaderDtlInfo.Visible = false;
                gvStatementInfo.Visible = true;

                GenerateGLStatement();

                gvHead3.Visible = true;
                gvHead3Detail();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }




        private void GenerateGLStatement()
        {
            var prm = new object[4];

            prm[0] = Converter.GetInteger(lblGLStatement.Text);
            prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);
            prm[3] = 0;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAccountStatement", prm, "A2ZGLMCUS"));


            string sqlquery3 = "SELECT TrnDate,VchNo,LTRIM(TrnDesc) AS TrnDesc,GLDebitAmt,GLCreditAmt,GLClosingBal FROM WFGLSTATEMENT WHERE GLAccNo='" + lblGLStatement.Text + "' AND GLAmount !=0 ORDER BY GLAccNo, TrnDate, VchNo";
            gvStatementInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvStatementInfo, "A2ZGLMCUS");

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                Session["SlblRepFlag"] = "1";

                Session["SlblGLHeadNumber"] = lblGLHeadNumber.Text;
                Session["SlblGLSubHeadNumber"] = lblGLSubHeadNumber.Text;
                Session["SlblGLStatement"] = lblGLStatement.Text;
                Session["SlblGLDesc"] = lblGLDesc.Text;


                Session["SlblGLHead"] = lblGLHead.Text;
                Session["SlblGLSubHead"] = lblGLSubHead.Text;


                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);

                if (gvHeaderInfo.Visible == true)
                {
                    Session["SlblRepFlag"] = "1";

                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  

                    if (lblReportOpt.Text == "1")
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [2nd Layer]");
                    }
                    else if (lblReportOpt.Text == "2")
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NO1, 2);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [2nd Layer]");
                    }
                    else if (lblReportOpt.Text == "3")
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NO1, 3);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [2nd Layer]");
                    }
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptViewTrialBalance2nd");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");
                }


                if (gvSubHeaderInfo.Visible == true)
                {
                    Session["SlblRepFlag"] = "2";

                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 5);  //   Trial Balance

                    int head = Converter.GetInteger(lblGLHead.Text);

                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, head);  


                    if (lblReportOpt.Text == "1")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [3rd Layer]");
                    }
                    else if (lblReportOpt.Text == "2")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 2);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [3rd Layer]");
                    }
                    else if (lblReportOpt.Text == "3")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 3);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [3rd Layer]");
                    }


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptViewIncomeExpenses3rd");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");
                }


                if (gvSubHeaderDtlInfo.Visible == true)
                {
                    Session["SlblRepFlag"] = "3";

                    
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 5);  //   Trial Balance

                    int head = Converter.GetInteger(lblGLHead.Text);
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, head);

                    int subhead = Converter.GetInteger(lblGLSubHead.Text);
                    SessionStore.SaveToCustomStore(Params.COMMON_NO3, subhead);  


                    //SessionStore.SaveToCustomStore(Params.COMMON_NO2, lblGLHead.Text);  //   Trial Balance  

                    if (lblReportOpt.Text == "1")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [4th Layer]");
                    }
                    else if (lblReportOpt.Text == "2")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 2);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [4th Layer]");
                    }
                    else if (lblReportOpt.Text == "3")
                    {
                        //SessionStore.SaveToCustomStore(Params.COMMON_NO1, 3);  //   Trial Balance  
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [4th Layer]");
                    }


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptViewIncomeExpenses4th");


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");
                }

                

                if (gvStatementInfo.Visible == true)
                {
                    Session["SlblRepFlag"] = "4";

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, Converter.GetInteger(lblGLStatement.Text));
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "GENERAL LEDGER ACCOUNT STATEMENT ");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblGLDesc.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLAccountStatement");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");
                }

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }
        private void gvHead1Detail()
        {
            try
            {

                string sqlquery1 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE GLAccNo = '" + lblGLHeadNumber.Text + "' AND CodeFlag=1 ORDER BY GLAccNo";
                gvHead1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery1, gvHead1, "A2ZGLMCUS");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gvHead2Detail()
        {
            try
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE GLAccNo = '" + lblGLSubHeadNumber.Text + "' AND CodeFlag=2 ORDER BY GLAccNo";
                gvHead2 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHead2, "A2ZGLMCUS");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void gvHead3Detail()
        {
            try
            {
                string sqlquery3 = "SELECT GLAccNo,RTRIM(GLAccDesc) AS GLAccDesc,GLOpBal,(GLDrSumC + GLDrSumT) AS GLDrSum,(GLCrSumC + GLCrSumT) AS GLCrSum,GLClBal FROM WFINCEXPREP WHERE GLAccNo = '" + lblGLStatement.Text + "' AND CodeFlag=3 ORDER BY GLAccNo";
                gvHead3 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHead3, "A2ZGLMCUS");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}