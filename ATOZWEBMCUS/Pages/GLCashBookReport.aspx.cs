using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;
using System.Data;


namespace ATOZWEBMCUS.Pages
{
    public partial class GLCashBookReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {

                    CtrlModule.Text = Request.QueryString["a%b"];
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    string qry1 = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + hdnCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        lblCashCodeName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                    }

                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);
                    txtGLPLCode.Text = Converter.GetString(dto.PLCode);

                    txtGLPLCode.Visible = false;
                    txtCashCode.Enabled = false;

                    rbtOptBeforeFunc.Visible = false;
                    rbtOptAfterFunc.Visible = false;



                    if (CtrlModule.Text == "6" || CtrlModule.Text == "7")
                    {
                        ChkAllCashCode.Checked = false;
                        ChkAllCashCode.Visible = false;
                        txtCashCode.Text = hdnCashCode.Text;
                        lblCodeDesc.Text = lblCashCodeName.Text;
                        txtCashCode.ReadOnly = true;
                    }
                }
                else
                {
                    string Rtxtfdate = (string)Session["Stxtfdate"];
                    string Rtxttdate = (string)Session["Stxttdate"];
                    string RChkAllCashCode = (string)Session["SChkAllCashCode"];
                    string RtxtCashCode = (string)Session["StxtCashCode"];
                    string RlblCodeDesc = (string)Session["SlblCodeDesc"];
                    string RrbtOptDetails = (string)Session["SrbtOptDetails"];
                    string RrbtOptSummary = (string)Session["SrbtOptSummary"];

                    string RlblOptFuncFlag = (string)Session["SlblOptFuncFlag"];
                    string RrbtOptAfterFunc = (string)Session["SrbtOptAfterFunc"];
                    string RrbtOptBeforeFunc = (string)Session["SrbtOptBeforeFunc"];

                    string RCtrlModule = (string)Session["SCtrlModule"];
                    string RhdnCashCode = (string)Session["ShdnCashCode"];
                    string RlblCashCodeName = (string)Session["SlblCashCodeName"];
                    string RtxtGLPLCode = (string)Session["StxtGLPLCode"];

                    CtrlModule.Text = RCtrlModule;

                    if (CtrlModule.Text == "6" || CtrlModule.Text == "7")
                    {
                        ChkAllCashCode.Checked = false;
                        ChkAllCashCode.Visible = false;
                        txtCashCode.Text = hdnCashCode.Text;
                        lblCodeDesc.Text = lblCashCodeName.Text;
                        txtCashCode.ReadOnly = true;
                    }

                    txtfdate.Text = Rtxtfdate;
                    txttdate.Text = Rtxttdate;



                    if (RChkAllCashCode == "1")
                    {
                        ChkAllCashCode.Checked = true;
                    }
                    else
                    {
                        ChkAllCashCode.Checked = false;
                    }
                    txtCashCode.Text = RtxtCashCode;
                    lblCodeDesc.Text = RlblCodeDesc;



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


                    if (RrbtOptDetails == "1")
                    {
                        rbtOptDetails.Checked = true;
                    }
                    else
                    {
                        rbtOptDetails.Checked = false;
                    }

                    if (RrbtOptSummary == "1")
                    {
                        rbtOptSummary.Checked = true;
                    }
                    else
                    {
                        rbtOptSummary.Checked = false;
                    }
                    hdnCashCode.Text = RhdnCashCode;
                    lblCashCodeName.Text = RlblCashCodeName;
                    txtGLPLCode.Text = RtxtGLPLCode;


                }
            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SChkAllCashCode"] = string.Empty;
            Session["StxtCashCode"] = string.Empty;
            Session["SlblCodeDesc"] = string.Empty;
            Session["SrbtOptDetails"] = string.Empty;
            Session["SrbtOptSummary"] = string.Empty;

            Session["SlblOptFuncFlag"] = string.Empty;
            Session["SrbtOptAfterFunc"] = string.Empty;
            Session["SrbtOptBeforeFunc"] = string.Empty;

            Session["SCtrlModule"] = string.Empty;
            Session["ShdnCashCode"] = string.Empty;
            Session["SlblCashCodeName"] = string.Empty;
            Session["StxtGLPLCode"] = string.Empty;
        }


        protected void BtnView_Click(object sender, EventArgs e)
        {

            try
            {

                if (ChkAllCashCode.Checked == false)
                {
                    if (txtCashCode.Text == string.Empty && txtCashCode.Enabled == true)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Cash Code');", true);

                        return;

                    }


                }

                if (rbtOptDetails.Checked == false && rbtOptSummary.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Select Details or Summary');", true);

                    return;

                }




                ViewDetailCashBookSingle();
                return;
            }
            catch
            {

            }

            try
            {
                


                Session["ProgFlag"] = "1";

                Session["SCtrlModule"] = CtrlModule.Text;
                Session["ShdnCashCode"] = hdnCashCode.Text;
                Session["SlblCashCodeName"] = lblCashCodeName.Text;
                Session["StxtGLPLCode"] = txtGLPLCode.Text;

                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                if (ChkAllCashCode.Checked == true)
                {
                    Session["SChkAllCashCode"] = "1";
                    txtCashCode.Text = string.Empty;
                    lblCodeDesc.Text = string.Empty;
                }
                else
                {
                    Session["SChkAllCashCode"] = "0";
                }

                Session["StxtCashCode"] = txtCashCode.Text;
                Session["SlblCodeDesc"] = lblCodeDesc.Text;

                if (rbtOptDetails.Checked == true)
                {
                    Session["SrbtOptDetails"] = "1";
                }
                else
                {
                    Session["SrbtOptDetails"] = "0";
                }

                if (rbtOptSummary.Checked == true)
                {
                    Session["SrbtOptSummary"] = "1";
                }
                else
                {
                    Session["SrbtOptSummary"] = "0";
                }


                //// Run Store Procedure -  Sp_GlGenerateAccountBalance   [ For Update WFINCEXPREP Table ]

                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Call Sub functon  ---  GetDateToYYYYMMDD  for    Sp_GlGenerateAccountBalance


                if (ChkAllCashCode.Checked)
                {
                    var prm = new object[3];
                    prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                    prm[2] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalance", prm, "A2ZGLMCUS"));
                }
                else
                {
                    var prm = new object[4];
                    if (CtrlModule.Text == "6" || CtrlModule.Text == "7")
                    {
                        prm[0] = hdnCashCode.Text;
                    }
                    else
                    {
                        prm[0] = txtCashCode.Text;
                    }
                    prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                    prm[3] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlCashBookSingle", prm, "A2ZGLMCUS"));
                }


                //

                //

                // SA
                string sqlQueryDel = "DELETE FROM WFINCEXPREP";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDel, "A2ZGLMCUS"));


                string sqlQueryDelA2ZCGLMSTLD = "DELETE FROM A2ZCGLMSTLD WHERE GLOPBAL = 0 AND GLDRSUMC=0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0 AND GLCLBAL = 0 ";
                int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelA2ZCGLMSTLD, "A2ZGLMCUS"));

                if (ChkAllCashCode.Checked == false)
                {
                    string sqlQuery1DelA2ZCGLMSTLD = "DELETE FROM A2ZCGLMSTLD WHERE GLOPBAL = 0 AND GLCLBAL = 0 AND  GLDRSUMC=0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0";
                    int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1DelA2ZCGLMSTLD, "A2ZGLMCUS"));
                }


                string sqlQueryINSERT = "INSERT INTO WFINCEXPREP (GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL," +
                                         "GLACCTYPE,CODEFLAG ) SELECT GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL,GLACCTYPE,2 AS CODEFLAG FROM A2ZCGLMSTLD ";
                int status4 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryINSERT, "A2ZGLMCUS"));


                string sqlQueryUpdateZero = "UPDATE WFINCEXPREP SET GLACCTYPE = 1";
                int status5 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdateZero, "A2ZGLMCUS"));

                // For Opening Balnce Record


                // For HEADER RECORD
                if (rbtOptSummary.Checked && ChkAllCashCode.Checked)
                {
                    string sqlQueryINSERT1 = "INSERT INTO WFINCEXPREP (GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL) SELECT GLSubHead,GLSubHeadDesc,SUM(GLOPBAL) AS GLOPBAL,SUM(GLDRSUMC) AS GLDRSUMC,SUM(GLDRSUMT) AS GLDRSUMT,SUM(GLCRSUMC) AS GLCRSUMC,SUM(GLCRSUMT) AS GLCRSUMT,SUM(GLCLBAL) AS GLCLBAL FROM A2ZCGLMSTLD WHERE (GLSubHead = 10101000 OR GLSubHead = 10106000 OR GLSubHead = 20801000) GROUP BY GLSubHead,GLSubHeadDesc";
                    int status20 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryINSERT1, "A2ZGLMCUS"));
                }


                string sqlQueryUpdate = "UPDATE WFINCEXPREP SET GLACCTYPE = 2,CODEFLAG = 2 WHERE Left(GLACCNO,5)=10101 OR Left(GLACCNO,5)=10106 OR Left(GLACCNO,5)=20801";
                int status6 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdate, "A2ZGLMCUS"));


                string sqlQueryBankINSERT = "INSERT INTO WFINCEXPREP (GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL," +
                                         "GLACCTYPE,CODEFLAG ) SELECT GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL,1,1 AS CODEFLAG FROM A2ZCGLMSTLD WHERE Left(GLACCNO,5)=10106 OR Left(GLACCNO,5)=20801";
                int status8 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryBankINSERT, "A2ZGLMCUS"));


                if (ChkAllCashCode.Checked == false)
                {
                    string sqlQuery1DelA2ZCGLMSTLD = "DELETE FROM WFINCEXPREP WHERE GLACCNO != '" + txtCashCode.Text + "' AND GLACCTYPE = 2 AND CODEFLAG = 2";
                    int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1DelA2ZCGLMSTLD, "A2ZGLMCUS"));
                }


                if (rbtOptSummary.Checked && ChkAllCashCode.Checked)
                {
                    string sqlQueryDelete = "DELETE FROM  WFINCEXPREP  WHERE GLACCNO != 10101000 AND GLACCNO != 10106000 AND GLACCNO != 20801000 AND GLACCTYPE!=1";
                    int status0 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                }
                //For All Part : Deleted  record which code has no Transacion  Amount including Openning Balance
                string sqlQueryDelete1 = "DELETE FROM  WFINCEXPREP  WHERE GLOpBal = 0 AND GLDRSUMC = 0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0 ";
                int status7 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete1, "A2ZGLMCUS"));

                // For Transaction Part : Deleted all records which code has no Transacion  Amount
                string sqlQueryDelete2 = "DELETE FROM  WFINCEXPREP  WHERE GLACCTYPE = 1  AND GLDRSUMC = 0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0 ";
                int status10 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete2, "A2ZGLMCUS"));

                // 10101999 code is not Cash Code at Opening Part   
                string sqlQueryDelete3 = "DELETE FROM  WFINCEXPREP  WHERE GLAccNo  = 10101999 ";
                int status9 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete3, "A2ZGLMCUS"));


                int GLPLCode = Converter.GetInteger(txtGLPLCode.Text);
                string sqlQueryDeleteLSPRMGLPLCODE = "DELETE FROM  WFINCEXPREP  WHERE GLACCNO = '" + GLPLCode + "'";
                int status11 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDeleteLSPRMGLPLCODE, "A2ZGLMCUS"));


                // Delete -  Left(GLPLCode),5 code with 000
                string GLPLCodeSub = (txtGLPLCode.Text);
                string GLPLCodeSub5Digit = GLPLCodeSub.Substring(0, 5);
                string GLPLCodeSub5Digit000 = GLPLCodeSub5Digit + "000";
                string sqlQueryDeleteLSPRMGLPLCODE000 = "DELETE FROM  WFINCEXPREP  WHERE GLACCNO =' " + GLPLCodeSub5Digit000 + "' ";
                int status12 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDeleteLSPRMGLPLCODE000, "A2ZGLMCUS"));

                string sqlQueryUpdate13 = "UPDATE WFINCEXPREP SET GLACCTYPE = 0 WHERE GLAccType= 1 AND CodeFlag = 1";
                int status13 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdate13, "A2ZGLMCUS"));

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);

                if (rbtOptSummary.Checked)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book Report - Summary");
                }
                else
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book Report - Details");
                }



                if (ChkAllCashCode.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLCashBook");
                }

                else if (ChkAllCashCode.Checked == false)
                {
                    string TempCashCode;

                    if (CtrlModule.Text == "6" || CtrlModule.Text == "7")
                    {
                        TempCashCode = Converter.GetString(hdnCashCode.Text).Trim();
                    }
                    else
                    {
                        TempCashCode = Converter.GetString(txtCashCode.Text).Trim();
                    }

                    SessionStore.SaveToCustomStore(Params.COMMON_NAME2, TempCashCode);

                    SessionStore.SaveToCustomStore(Params.COMMON_NAME3, lblCodeDesc.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLCashBookByAccCode");
                }

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


        protected void ChkAllCashCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCashCode.Checked == true)
            {
                txtCashCode.Enabled = false;
                lblCodeDesc.Visible = false;
                txtCashCode.Text = "";
            }
            else
            {
                txtCashCode.Enabled = true;
                lblCodeDesc.Visible = true;
                txtCashCode.Focus();
            }

            DateTime opdate1 = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            lblOptFuncFlag.Text = "0";

            int Month1 = opdate1.Month;
            int Day1 = opdate1.Day;

            if (Day1 == 30 && Month1 == 6 && ChkAllCashCode.Checked == true)
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

        protected void txtCashCode_TextChanged(object sender, EventArgs e)
        {
            //// Cheaking 8 digit - GL Account Code
            //if (txtCashCode.Text.Length != 8)
            //{
            //    //String csname1 = "PopupScript";
            //    //Type cstype = GetType();
            //    //ClientScriptManager cs = Page.ClientScript;

            //    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //    //{

            //    //    String cstext1 = "alert('Invalid - GL Account Code');";
            //    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //    //}
            //    txtCashCode.Text = string.Empty;
            //    txtCashCode.Focus();

            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Account Code');", true);
            //    return;

            //}
            //// Cheaking - GL  Cash Account Code
            //string glno = txtCashCode.Text;
            //string glAccno = glno.Substring(0, 5);
            //string qry = "SELECT GLAccNo FROM A2ZCGLMST WHERE Left(GLAccNo,5)='" + glAccno + "'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
            //if (dt.Rows.Count == 0)
            //{
            //    //String csname1 = "PopupScript";
            //    //Type cstype = GetType();
            //    //ClientScriptManager cs = Page.ClientScript;

            //    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //    //{

            //    //    String cstext1 = "alert('Invalid - GL Cash Code');";
            //    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //    //}
            //    txtCashCode.Text = string.Empty;
            //    txtCashCode.Focus();

            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Cash Code');", true);
            //    return;

            //}



            int GLCode;
            A2ZCGLMSTDTO glObj = new A2ZCGLMSTDTO();
            string input1 = Converter.GetString(txtCashCode.Text).Length.ToString();
            if (input1 == "6")
            {
                GLCode = Converter.GetInteger(txtCashCode.Text);
                glObj = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
            }
            else
            {
                GLCode = Converter.GetInteger(txtCashCode.Text);
                glObj = (A2ZCGLMSTDTO.GetInformation(GLCode));
            }

            if (glObj.GLAccNo > 0)
            {
                txtCashCode.Text = Converter.GetString(glObj.GLAccNo);
                lblCodeDesc.Text = Converter.GetString(glObj.GLAccDesc);
                hdnGLSubHead1.Text = Converter.GetString(glObj.GLSubHead);

                if (hdnGLSubHead1.Text != "10101000")
                {
                    txtCashCode.Text = string.Empty;
                    txtCashCode.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Cash Code');", true);
                    return;
                }
            }
            else 
            {
                txtCashCode.Text = string.Empty;
                txtCashCode.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Cash Code');", true);
                return;
            }


            
            ////-----------------------------------------------------
            //string qry1 = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + txtCashCode.Text + "'";
            //DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
            //if (dt1.Rows.Count > 0)
            //{
            //    lblCodeDesc.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
            //    txtCashCode.Focus();
            //}
        }

        protected void ViewDetailCashBookSingle()
        {
            if (ChkAllCashCode.Checked == false)
            {
                if (txtCashCode.Text == string.Empty && txtCashCode.Enabled == true)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please - Input Cash Code ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Cash Code');", true);

                    return;

                }


            }

            if (rbtOptDetails.Checked == false && rbtOptSummary.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Select Details or Summary');", true);

                return;

            }


            Session["ProgFlag"] = "1";

            Session["SCtrlModule"] = CtrlModule.Text;
            Session["ShdnCashCode"] = hdnCashCode.Text;
            Session["SlblCashCodeName"] = lblCashCodeName.Text;
            Session["StxtGLPLCode"] = txtGLPLCode.Text;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;

            if (ChkAllCashCode.Checked == true)
            {
                Session["SChkAllCashCode"] = "1";
                txtCashCode.Text = string.Empty;
                lblCodeDesc.Text = string.Empty;
            }
            else
            {
                Session["SChkAllCashCode"] = "0";
            }

            Session["StxtCashCode"] = txtCashCode.Text;
            Session["SlblCodeDesc"] = lblCodeDesc.Text;

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


            if (rbtOptDetails.Checked == true)
            {
                Session["SrbtOptDetails"] = "1";
            }
            else
            {
                Session["SrbtOptDetails"] = "0";
            }

            if (rbtOptSummary.Checked == true)
            {
                Session["SrbtOptSummary"] = "1";
            }
            else
            {
                Session["SrbtOptSummary"] = "0";
            }


            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string TempCashCode;

            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;

            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));


            if (CtrlModule.Text == "6" || CtrlModule.Text == "7")
            {
                TempCashCode = Converter.GetString(hdnCashCode.Text).Trim();
            }
            else
            {
                TempCashCode = Converter.GetString(txtCashCode.Text).Trim();
            }

            if (TempCashCode == "")
            {
                SessionStore.SaveToCustomStore(Params.ACCOUNTCODE, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(Params.ACCOUNTCODE, TempCashCode);
            }

            SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book - Details");
            SessionStore.SaveToCustomStore(Params.NFLAG, "0");
            if (rbtOptSummary.Checked)
            {
                SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book - Summary");
                SessionStore.SaveToCustomStore(Params.NFLAG, "1");
            }

            if (lblOptFuncFlag.Text == "1" && rbtOptBeforeFunc.Checked && rbtOptDetails.Checked)
            {
                SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book - Details");
                SessionStore.SaveToCustomStore(Params.NFLAG, "8");
            }

            if (lblOptFuncFlag.Text == "1" && rbtOptBeforeFunc.Checked && rbtOptSummary.Checked)
            {
                SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Cash Book - Summary");
                SessionStore.SaveToCustomStore(Params.NFLAG, "9");
            }



            SessionStore.SaveToCustomStore(Params.COMMON_NAME3, lblCodeDesc.Text);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLCashBookByAccCode");

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

            Response.Redirect("ReportServer.aspx", false);
        }

        protected void txttdate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            lblOptFuncFlag.Text = "0";

            int Month1 = opdate1.Month;
            int Day1 = opdate1.Day;

            if (Day1 == 30 && Month1 == 6 && ChkAllCashCode.Checked == true)
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

    }
}