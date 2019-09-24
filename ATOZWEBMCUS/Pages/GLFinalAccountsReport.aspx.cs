using ATOZWEBMCUS.WebSessionStore;
using System;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLFinalAccountsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);

                    rbtOptBeforeFunc.Visible = false;
                    rbtOptAfterFunc.Visible = false;

                    BtnGridView.Visible = true;
                }
                else
                {

                    string Rtxtfdate = (string)Session["Stxtfdate"];
                    string Rtxttdate = (string)Session["Stxttdate"];
                    string RrbtOpt1stLayer = (string)Session["SrbtOpt1stLayer"];
                    string RrbtOpt2ndLayer = (string)Session["SrbtOpt2ndLayer"];
                    string RrbtOpt3rdLayer = (string)Session["SrbtOpt3rdLayer"];
                    string RrbtOpt4thLayer = (string)Session["SrbtOpt4thLayer"];
                    string RChkShowZero = (string)Session["SChkShowZero"];
                    string RrbtOptTrialBalance = (string)Session["SrbtOptTrialBalance"];
                    string RrbtOptReceivePayment = (string)Session["SrbtOptReceivePayment"];
                    string RrbtOptIncomeExpenditure = (string)Session["SrbtOptIncomeExpenditure"];
                    string RrbtOptBalanceSheet = (string)Session["SrbtOptBalanceSheet"];

                    string RlblOptFuncFlag = (string)Session["SlblOptFuncFlag"];
                    string RrbtOptAfterFunc = (string)Session["SrbtOptAfterFunc"];
                    string RrbtOptBeforeFunc = (string)Session["SrbtOptBeforeFunc"];

                    txtfdate.Text = Rtxtfdate;
                    txttdate.Text = Rtxttdate;

                    lblOptFuncFlag.Text = RlblOptFuncFlag;

                    if (RlblOptFuncFlag == "1")
                    {
                        rbtOptAfterFunc.Visible = true;
                        rbtOptBeforeFunc.Visible = true;
                    }
                    else
                    {
                        rbtOptAfterFunc.Visible = false;
                        rbtOptBeforeFunc.Visible = false;
                    }

                    if (RrbtOptAfterFunc == "1")
                    {
                        rbtOptAfterFunc.Checked = true;
                    }
                    else
                    {
                        rbtOptAfterFunc.Checked = false;
                    }

                    if (RrbtOptBeforeFunc == "1")
                    {
                        rbtOptBeforeFunc.Checked = true;
                    }
                    else
                    {
                        rbtOptBeforeFunc.Checked = false;
                    }


                    if (RrbtOpt1stLayer == "1")
                    {
                        rbtOpt1stLayer.Checked = true;
                        BtnGridView.Visible = false;
                    }
                    else
                    {
                        rbtOpt1stLayer.Checked = false;
                    }

                    if (RrbtOpt2ndLayer == "1")
                    {
                        rbtOpt2ndLayer.Checked = true;
                       
                    }
                    else
                    {
                        rbtOpt2ndLayer.Checked = false;
                    }

                    if (RrbtOpt3rdLayer == "1")
                    {
                        rbtOpt3rdLayer.Checked = true;
                    }
                    else
                    {
                        rbtOpt3rdLayer.Checked = false;
                    }

                    if (RrbtOpt4thLayer == "1")
                    {
                        rbtOpt4thLayer.Checked = true;
                    }
                    else
                    {
                        rbtOpt4thLayer.Checked = false;
                    }

                    if (RChkShowZero == "1")
                    {
                        ChkShowZero.Checked = true;
                    }
                    else
                    {
                        ChkShowZero.Checked = false;
                    }

                    if (RrbtOptTrialBalance == "1")
                    {
                        rbtOptTrialBalance.Checked = true;
                    }
                    else
                    {
                        rbtOptTrialBalance.Checked = false;
                    }

                    if (RrbtOptReceivePayment == "1")
                    {
                        rbtOptReceivePayment.Checked = true;
                    }
                    else
                    {
                        rbtOptReceivePayment.Checked = false;
                    }

                    if (RrbtOptIncomeExpenditure == "1")
                    {
                        rbtOptIncomeExpenditure.Checked = true;
                    }
                    else
                    {
                        rbtOptIncomeExpenditure.Checked = false;
                    }

                    if (RrbtOptBalanceSheet == "1")
                    {
                        rbtOptBalanceSheet.Checked = true;
                    }
                    else
                    {
                        rbtOptBalanceSheet.Checked = false;
                    }
                }

            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SrbtOpt1stLayer"] = string.Empty;
            Session["SrbtOpt2ndLayer"] = string.Empty;
            Session["SrbtOpt3rdLayer"] = string.Empty;
            Session["SrbtOpt4thLayer"] = string.Empty;
            Session["SChkShowZero"] = string.Empty;
            Session["SrbtOptTrialBalance"] = string.Empty;
            Session["SrbtOptReceivePayment"] = string.Empty;
            Session["SrbtOptIncomeExpenditure"] = string.Empty;
            Session["SrbtOptBalanceSheet"] = string.Empty;

            Session["SlblOptFuncFlag"] = string.Empty;
            Session["SrbtOptAfterFunc"] = string.Empty;
            Session["SrbtOptBeforeFunc"] = string.Empty;
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                Session["ProgFlag"] = "1";


                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;


                Session["SlblOptFuncFlag"] = lblOptFuncFlag.Text;
                if (rbtOptAfterFunc.Checked == true)
                {
                    Session["SrbtOptAfterFunc"] = "1";
                }
                else
                {
                    Session["SrbtOptAfterFunc"] = "0";
                }

                if (rbtOptBeforeFunc.Checked == true)
                {
                    Session["SrbtOptBeforeFunc"] = "1";
                }
                else
                {
                    Session["SrbtOptBeforeFunc"] = "0";
                }



                if (rbtOpt1stLayer.Checked == true)
                {
                    Session["SrbtOpt1stLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt1stLayer"] = "0";
                }

                if (rbtOpt2ndLayer.Checked == true)
                {
                    Session["SrbtOpt2ndLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt2ndLayer"] = "0";
                }

                if (rbtOpt3rdLayer.Checked == true)
                {
                    Session["SrbtOpt3rdLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt3rdLayer"] = "0";
                }

                if (rbtOpt4thLayer.Checked == true)
                {
                    Session["SrbtOpt4thLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt4thLayer"] = "0";
                }

                if (ChkShowZero.Checked == true)
                {
                    Session["SChkShowZero"] = "1";
                }
                else
                {
                    Session["SChkShowZero"] = "0";
                }

                if (rbtOptTrialBalance.Checked == true)
                {
                    Session["SrbtOptTrialBalance"] = "1";
                }
                else
                {
                    Session["SrbtOptTrialBalance"] = "0";
                }

                if (rbtOptReceivePayment.Checked == true)
                {
                    Session["SrbtOptReceivePayment"] = "1";
                }
                else
                {
                    Session["SrbtOptReceivePayment"] = "0";
                }

                if (rbtOptIncomeExpenditure.Checked == true)
                {
                    Session["SrbtOptIncomeExpenditure"] = "1";
                }
                else
                {
                    Session["SrbtOptIncomeExpenditure"] = "0";
                }

                if (rbtOptBalanceSheet.Checked == true)
                {
                    Session["SrbtOptBalanceSheet"] = "1";
                }
                else
                {
                    Session["SrbtOptBalanceSheet"] = "0";
                }
                //// Run Store Procedure -  Sp_GlGenerateAccountBalance   [ For Update WFINCEXPREP Table ]

                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Call Sub functon  ---  GetDateToYYYYMMDD  for    Sp_GlGenerateAccountBalance

                var prm = new object[4];
                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                prm[2] = 0;

                if (rbtOptBeforeFunc.Checked)
                {
                    prm[2] = 8;
                }

                prm[3] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalance", prm, "A2ZGLMCUS"));
                //
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);


                if (ChkShowZero.Checked == false)
                {
                    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE GLOpBal =  0  AND GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 AND GLClBal = 0    ";
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
                }

                // For Report Parameter Value

                if (rbtOptTrialBalance.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  
                }

                if (rbtOptIncomeExpenditure.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 2);  //   Income Expenditure  
                }
                if (rbtOptBalanceSheet.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 3); //    Balance Sheet 
                }



                if (rbtOptTrialBalance.Checked)       //  For Trial Balance Report Header Name
                {
                    if (rbtOpt1stLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [1st Layer]");
                    }
                    else if (rbtOpt2ndLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [2nd Layer]");
                    }
                    else if (rbtOpt3rdLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [3rd Layer]");
                    }
                    else if (rbtOpt4thLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Trial Balance - [4th Layer]");
                    }
                }

                if (rbtOptIncomeExpenditure.Checked) //  For  Income Expenditure Report Header Name
                {





                    if (rbtOpt1stLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [1st Layer]");
                    }
                    else if (rbtOpt2ndLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [2nd Layer]");

                    }
                    else if (rbtOpt3rdLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [3rd Layer]");

                    }
                    else if (rbtOpt4thLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Income & Expenses - [4th Layer]");
                    }
                }

                if (rbtOptBalanceSheet.Checked)    //   Balance Sheet Report Header Name  
                {

                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                    lblPLCode.Text = Converter.GetString(dto.PLCode);

                    string PLCode = lblPLCode.Text.Substring(0, 5);

                    string sqlQueryUpdateZero = "UPDATE WFINCEXPREP SET GLDrSumC = 0,GLDrSumT=0,GLCrSumC=0,GLCrSumT=0 WHERE  Left(GLACCNO,5)='" + PLCode + "'";
                    int status5 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdateZero, "A2ZGLMCUS"));

                    if (rbtOpt1stLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [1st Layer]");
                    }
                    else if (rbtOpt2ndLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [2nd Layer]");

                    }
                    else if (rbtOpt3rdLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [3rd Layer]");

                    }
                    else if (rbtOpt4thLayer.Checked)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " Balance Sheet - [4th Layer]");
                    }

                }


                //  Report

                if (rbtOpt1stLayer.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptTrialBalance1st");
                }

                if (rbtOpt2ndLayer.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptTrialBalance2nd");

                }
                if (rbtOpt3rdLayer.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, 0);  
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptIncomeExpenses3rd");

                }
                if (rbtOpt4thLayer.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(Params.COMMON_NO3, 0);  
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptIncomeExpenses4th");
                }



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);


            }



            catch (Exception ex)
            {
                throw ex;
            }



        }

        protected void txttdate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            lblOptFuncFlag.Text = "0";

            int Month1 = opdate1.Month;
            int Day1 = opdate1.Day;

            if (Day1 == 30 && Month1 == 6)
            {
                rbtOptAfterFunc.Visible = true;
                rbtOptBeforeFunc.Visible = true;
                lblOptFuncFlag.Text = "1";
            }
            else
            {
                rbtOptAfterFunc.Visible = false;
                rbtOptBeforeFunc.Visible = false;
                lblOptFuncFlag.Text = "0";
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnGridView_Click(object sender, EventArgs e)
        {
            Session["ProgFlag"] = "1";


            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;


            Session["SlblOptFuncFlag"] = lblOptFuncFlag.Text;
            if (rbtOptAfterFunc.Checked == true)
            {
                Session["SrbtOptAfterFunc"] = "1";
            }
            else
            {
                Session["SrbtOptAfterFunc"] = "0";
            }

            if (rbtOptBeforeFunc.Checked == true)
            {
                Session["SrbtOptBeforeFunc"] = "1";
            }
            else
            {
                Session["SrbtOptBeforeFunc"] = "0";
            }



            if (rbtOpt1stLayer.Checked == true)
            {
                Session["SrbtOpt1stLayer"] = "1";
            }
            else
            {
                Session["SrbtOpt1stLayer"] = "0";
            }

            if (rbtOpt2ndLayer.Checked == true)
            {
                Session["SrbtOpt2ndLayer"] = "1";
            }
            else
            {
                Session["SrbtOpt2ndLayer"] = "0";
            }

            if (rbtOpt3rdLayer.Checked == true)
            {
                Session["SrbtOpt3rdLayer"] = "1";
            }
            else
            {
                Session["SrbtOpt3rdLayer"] = "0";
            }

            if (rbtOpt4thLayer.Checked == true)
            {
                Session["SrbtOpt4thLayer"] = "1";
            }
            else
            {
                Session["SrbtOpt4thLayer"] = "0";
            }

            if (ChkShowZero.Checked == true)
            {
                Session["SChkShowZero"] = "1";
            }
            else
            {
                Session["SChkShowZero"] = "0";
            }

            if (rbtOptTrialBalance.Checked == true)
            {
                Session["SrbtOptTrialBalance"] = "1";
            }
            else
            {
                Session["SrbtOptTrialBalance"] = "0";
            }

            if (rbtOptReceivePayment.Checked == true)
            {
                Session["SrbtOptReceivePayment"] = "1";
            }
            else
            {
                Session["SrbtOptReceivePayment"] = "0";
            }

            if (rbtOptIncomeExpenditure.Checked == true)
            {
                Session["SrbtOptIncomeExpenditure"] = "1";
            }
            else
            {
                Session["SrbtOptIncomeExpenditure"] = "0";
            }

            if (rbtOptBalanceSheet.Checked == true)
            {
                Session["SrbtOptBalanceSheet"] = "1";
            }
            else
            {
                Session["SrbtOptBalanceSheet"] = "0";
            }
            //// Run Store Procedure -  Sp_GlGenerateAccountBalance   [ For Update WFINCEXPREP Table ]

            DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Call Sub functon  ---  GetDateToYYYYMMDD  for    Sp_GlGenerateAccountBalance

            var prm = new object[4];
            prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
            prm[2] = 0;


            if (rbtOptBeforeFunc.Checked)
            {
                prm[2] = 8;
            }

            prm[3] = 0;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalance", prm, "A2ZGLMCUS"));
            //
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);


            if (ChkShowZero.Checked == false)
            {
                string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE GLOpBal =  0  AND GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 AND GLClBal = 0    ";
                int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
            }


            if (rbtOptTrialBalance.Checked)
            {
                Session["SReportOpt"] = "1";
            }
            if (rbtOptIncomeExpenditure.Checked)
            {
                Session["SReportOpt"] = "2";
            }
            if (rbtOptBalanceSheet.Checked)
            {
                Session["SReportOpt"] = "3";
            }

            Response.Redirect("GLFinalAccountsView.aspx");


            //            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            ////"click", @"<script>window.open('GLFinalAccountsView.aspx','_blank');</script>", false);
            //"click", @"<script>window.open('GLFinalAccountsView.aspx'); self.close();</script>", false);


        }

        protected void rbtOpt1stLayer_CheckedChanged(object sender, EventArgs e)
        {
            BtnGridView.Visible = false;
        }
        protected void rbtOpt2ndLayer_CheckedChanged(object sender, EventArgs e)
        {
            BtnGridView.Visible = true;
        }

        protected void rbtOpt3rdLayer_CheckedChanged(object sender, EventArgs e)
        {
            BtnGridView.Visible = true;
        }

        protected void rbtOpt4thLayer_CheckedChanged(object sender, EventArgs e)
        {
            BtnGridView.Visible = true;
        }


    }
}