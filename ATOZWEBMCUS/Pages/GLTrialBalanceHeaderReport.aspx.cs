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
    public partial class GLTrialBalanceHeaderReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GLCashCodeDropdown();
                GLCodeDropdown();


                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);

                    ChkAllFCashCode.Checked = true;
                    txtFCashCode.Enabled = false;
                    ddlFCashCode.Enabled = false;
                }
                else
                {

                    string Rtxtfdate = (string)Session["Stxtfdate"];
                    string Rtxttdate = (string)Session["Stxttdate"];
                    string RtxtGLCode = (string)Session["StxtGLCode"];

                    string RChkAllFCashCode = (string)Session["SChkAllFCashCode"];
                    string RtxtFCashCode = (string)Session["StxtFCashCode"];
                    string RddlFCashCode = (string)Session["SddlFCashCode"];



                    txtfdate.Text = Rtxtfdate;
                    txttdate.Text = Rtxttdate;
                    txtGLCode.Text = RtxtGLCode;

                    if (RChkAllFCashCode == "1")
                    {
                        ChkAllFCashCode.Checked = true;
                        txtFCashCode.Text = string.Empty;
                        ddlFCashCode.SelectedValue = "-Select-";
                        txtFCashCode.Enabled = false;
                        ddlFCashCode.Enabled = false;
                    }
                    else
                    {
                        ChkAllFCashCode.Checked = false;
                        txtFCashCode.Text = RtxtFCashCode;
                        ddlFCashCode.SelectedValue = RddlFCashCode;
                    }



                }
            }


        }


        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC ";
            ddlFCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFCashCode, "A2ZGLMCUS");

        }

        protected void GLCodeDropdown()
        {
            string sqlquery = "SELECT GLSubHead,GLSubHeadDesc from A2ZCGLMST GROUP BY GLSubHead,GLSubHeadDesc ORDER BY GLSubHeadDesc ASC";
            ddlGLCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCode, "A2ZGLMCUS");

        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["StxtGLCode"] = string.Empty;
            Session["SChkAllFCashCode"] = string.Empty;
            Session["StxtFCashCode"] = string.Empty;
            Session["SddlFCashCode"] = string.Empty;

        }

        protected void InvalidGLCodeMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input GL Code');", true);
            return;
        }

        protected void InvalidCashCodeMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Cash Code');", true);
            return;
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGLCode.Text == string.Empty)
                {
                    txtGLCode.Focus();
                    InvalidGLCodeMSG();
                    return;
                }

                if (ChkAllFCashCode.Checked == false && txtFCashCode.Text == string.Empty)
                {
                    txtFCashCode.Focus();
                    InvalidCashCodeMSG();
                    return;
                }


                Session["ProgFlag"] = "1";


                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;
                Session["StxtGLCode"] = txtGLCode.Text;

                if (ChkAllFCashCode.Checked == true)
                {
                    Session["SChkAllFCashCode"] = "1";
                }
                else
                {
                    Session["SChkAllFCashCode"] = "0";
                }

                Session["StxtFCashCode"] = txtFCashCode.Text;
                Session["SddlFCashCode"] = ddlFCashCode.SelectedValue;




                //// Run Store Procedure -  Sp_GlGenerateAccountBalance   [ For Update WFINCEXPREP Table ]

                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Call Sub functon  ---  GetDateToYYYYMMDD  for    Sp_GlGenerateAccountBalance

                var prm = new object[4];
                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                prm[2] = 0;
                prm[3] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalance", prm, "A2ZGLMCUS"));
                //


                string GLPLCodeSub = (txtGLCode.Text);
                string GLPLCodeSub5Digit = GLPLCodeSub.Substring(0, 5);
                string GLPLCodeSub5Digit000 = GLPLCodeSub5Digit + "000";


                string sqlQueryDelete = "DELETE FROM  WFINCEXPREP  WHERE Left(GLACCNO,5) != '" + GLPLCodeSub5Digit + "'";
                int status0 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));

                if (ChkZeroBalanceShow.Checked == false)
                {
                    string sql3QueryDelete = "DELETE FROM  WFINCEXPREP  WHERE GLOpBal = 0 AND GLDrSumC = 0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 AND GLClBal = 0";
                    int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sql3QueryDelete, "A2ZGLMCUS"));
                }



                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);

               
                // For Report Parameter Value


                SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);  //   Trial Balance  

                SessionStore.SaveToCustomStore(Params.COMMON_NO2, 0);

                

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptTrialBalanceHeader");





                //  Report

                //if (rbtOpt1stLayer.Checked) 

                //     {
                //         SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptTrialBalance1st");
                //     }

                //if (rbtOpt2ndLayer.Checked)
                //     {
                //         SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptTrialBalance2nd");

                //     }
                // if (rbtOpt3rdLayer.Checked)
                //     {
                //         SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptIncomeExpenses3rd");

                //     }
                // if (rbtOpt4thLayer.Checked)
                //     {
                //         SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptIncomeExpenses4th");
                //     }



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);


            }



            catch (Exception ex)
            {
                throw ex;
            }



        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtGLCode_TextChanged(object sender, EventArgs e)
        {
            ddlGLCode.SelectedValue = txtGLCode.Text;
        }

        protected void ddlGLCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGLCode.Text = ddlGLCode.SelectedValue;
        }

        protected void txtFCashCode_TextChanged(object sender, EventArgs e)
        {
            ddlFCashCode.SelectedValue = txtFCashCode.Text;
        }
        protected void ddlFCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFCashCode.Text = ddlFCashCode.SelectedValue;
        }

        protected void ChkAllFCashCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllFCashCode.Checked)
            {
                txtFCashCode.Enabled = false;
                ddlFCashCode.Enabled = false;
            }
            else
            {
                txtFCashCode.Enabled = true;
                ddlFCashCode.Enabled = true;
                txtFCashCode.Text = string.Empty;
                txtFCashCode.Focus();
            }
        }
    }
}