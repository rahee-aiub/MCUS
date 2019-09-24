using System;
using ATOZWEBMCUS.WebSessionStore;
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
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLReceiptPaymentReport : System.Web.UI.Page
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

                    if (CtrlModule.Text == "6")
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
                    string RrbtOpt4thLayer = (string)Session["SrbtOpt4thLayer"];
                    string RrbtOpt3rdLayer = (string)Session["SrbtOpt3rdLayer"];

                    string RCtrlModule = (string)Session["SCtrlModule"];
                    string RhdnCashCode = (string)Session["ShdnCashCode"];
                    string RlblCashCodeName = (string)Session["SlblCashCodeName"];
                    string RtxtGLPLCode = (string)Session["StxtGLPLCode"];
                    string RChkShowZero = (string)Session["SChkShowZero"];

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

                    if (RrbtOpt4thLayer == "1")
                    {
                        rbtOpt4thLayer.Checked = true;
                    }
                    else
                    {
                        rbtOpt4thLayer.Checked = false;
                    }

                    if (RrbtOpt3rdLayer == "1")
                    {
                        rbtOpt3rdLayer.Checked = true;
                    }
                    else
                    {
                        rbtOpt3rdLayer.Checked = false;
                    }
                    if (RChkShowZero == "1")
                    {
                        ChkShowZero.Checked = true;
                    }
                    else
                    {
                        ChkShowZero.Checked = false;
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

            Session["SCtrlModule"] = string.Empty;
            Session["ShdnCashCode"] = string.Empty;
            Session["SlblCashCodeName"] = string.Empty;
            Session["StxtGLPLCode"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;           
            Session["SChkAllCashCode"] = string.Empty;
            Session["StxtCashCode"] = string.Empty;
            Session["SlblCodeDesc"] = string.Empty;         
            Session["SrbtOpt4thLayer"] = string.Empty;            
            Session["SrbtOpt3rdLayer"] = string.Empty;
            Session["SChkShowZero"] = string.Empty;
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
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

                if (rbtOpt3rdLayer.Checked == false && rbtOpt4thLayer.Checked == false)
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

                if (rbtOpt4thLayer.Checked == true)
                {
                    Session["SrbtOpt4thLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt4thLayer"] = "0";
                }

                if (rbtOpt3rdLayer.Checked == true)
                {
                    Session["SrbtOpt3rdLayer"] = "1";
                }
                else
                {
                    Session["SrbtOpt3rdLayer"] = "0";
                }

                if (ChkShowZero.Checked == true)
                {
                    Session["SChkShowZero"] = "1";
                }
                else
                {
                    Session["SChkShowZero"] = "0";
                }

                

                // Run Store Procedure -  Sp_GlGenerateAccountBalance   [ For Update WFINCEXPREP Table ]              

                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Call Sub functon  ---  GetDateToYYYYMMDD  for    Sp_GlGenerateAccountBalance
               
                if (ChkAllCashCode.Checked)
                {
                    var prm = new object[4];
                    prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                    prm[2] = 0;
                    if (ChkWithSysTran.Checked == true)
                    {
                        prm[3] = 0;
                    }
                    else 
                    {
                        prm[3] = 1;
                    }

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateAccountBalance", prm, "A2ZGLMCUS"));
                }
                else
                {
                    var prm = new object[4];
                    prm[0] = txtCashCode.Text;
                    prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                    prm[3] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlCashBookSingle", prm, "A2ZGLMCUS"));
                }



                string sqlQueryDel = "DELETE FROM WFINCEXPREP";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDel, "A2ZGLMCUS"));


                if (ChkShowZero.Checked == false)
                {
                    string sqlQueryDelA2ZCGLMSTLD = "DELETE FROM A2ZCGLMSTLD WHERE GLOPBAL = 0 AND GLDRSUMC=0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0 AND GLCLBAL = 0 ";
                    int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelA2ZCGLMSTLD, "A2ZGLMCUS"));

                }

                if (ChkAllCashCode.Checked == false)
                {
                    string sqlQuery1DelA2ZCGLMSTLD = "DELETE FROM A2ZCGLMSTLD WHERE GLDRSUMC=0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0";
                    int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery1DelA2ZCGLMSTLD, "A2ZGLMCUS"));
                }


                // For DETIAL RECORD                
                string sqlQueryINSERT = "INSERT INTO WFINCEXPREP (GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL," +
                                         "GLACCTYPE,CODEFLAG ) SELECT GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL,GLACCTYPE,2 AS CODEFLAG FROM A2ZCGLMSTLD ";
                int status4 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryINSERT, "A2ZGLMCUS"));

                string sqlQueryUpdateGLAccType = "UPDATE WFINCEXPREP SET GLACCTYPE = 1";
                int status5 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdateGLAccType, "A2ZGLMCUS"));

                // For Opening Balnce Record
                string sqlQueryUpdate = "UPDATE WFINCEXPREP SET GLACCTYPE = 2 WHERE Left(GLACCNO,5)=10101  OR Left(GLACCNO,5)=10106 ";
                int status6 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdate, "A2ZGLMCUS"));


                // For HEADER RECORD

                if (rbtOpt3rdLayer.Checked && ChkAllCashCode.Checked)
                {
                    string sqlQueryINSERT1 = "INSERT INTO WFINCEXPREP (GLACCNO,GLACCDESC,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL," +
                                           "GLACCTYPE,CODEFLAG ) SELECT GLSubHead,GLSubHeadDesc,SUM(GLOPBAL) AS GLOPBAL,SUM(GLDRSUMC) AS GLDRSUMC,SUM(GLDRSUMT) AS GLDRSUMT,SUM(GLCRSUMC) AS GLCRSUMC,SUM(GLCRSUMT) AS GLCRSUMT,SUM(GLCLBAL) AS GLCLBAL,GLACCTYPE,1 AS CODEFLAG FROM A2ZCGLMSTLD GROUP BY GLSubHead,GLSubHeadDesc,GLACCTYPE";
                    int status11 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryINSERT1, "A2ZGLMCUS"));

                    string sqlQueryUpdateGLAccType1 = "UPDATE WFINCEXPREP SET GLACCTYPE = 1 WHERE  CODEFLAG = 1";
                    int status41 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdateGLAccType1, "A2ZGLMCUS"));
                }
                // For Opening Balnce Record
                string sqlQueryUpdate1 = "UPDATE WFINCEXPREP SET GLACCTYPE = 2 WHERE (Left(GLACCNO,5)=10101 OR Left(GLACCNO,5)=10106) AND  CODEFLAG = 1 ";
                int status51 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryUpdate1, "A2ZGLMCUS"));
                                                                           
                //---------- 01-12-2015 ---
                if (ChkShowZero.Checked == false)
                //{
                //    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE  GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 ";
                //    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
                //}

                {
                    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE  GLOPBAL = 0 AND GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 ";
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
                }

                if (ChkAllCashCode.Checked == false)
                {
                    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE  GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 ";
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                }


                // - NEW CODING TODAY  01-12-2015
                // For Transaction Part : Deleted all records which code has no Transacion  Amount
                string sqlQueryDelete2 = "DELETE FROM  WFINCEXPREP  WHERE GLACCTYPE = 1  AND GLDRSUMC = 0 AND GLDRSUMT = 0 AND GLCRSUMC = 0 AND GLCRSUMT = 0 ";
                int status7 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete2, "A2ZGLMCUS"));



                // 10101999 code is not Cash Code at Opening Part   
                string sqlQueryDelete3 = "DELETE FROM  WFINCEXPREP  WHERE GLAccNo  = 10101999 ";
                int status8 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete3, "A2ZGLMCUS"));


                int GLPLCode = Converter.GetInteger(txtGLPLCode.Text);
                string sqlQueryDeleteLSPRMGLPLCODE = "DELETE FROM  WFINCEXPREP  WHERE GLACCNO = '" + GLPLCode + "'";
                int status9 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDeleteLSPRMGLPLCODE, "A2ZGLMCUS"));


                // Delete -  Left(GLPLCode),5 code with 000
                string GLPLCodeSub = (txtGLPLCode.Text);
                string GLPLCodeSub5Digit = GLPLCodeSub.Substring(0, 5);
                string GLPLCodeSub5Digit000 = GLPLCodeSub5Digit + "000";
                string sqlQueryDeleteLSPRMGLPLCODE000 = "DELETE FROM  WFINCEXPREP  WHERE GLACCNO =' " + GLPLCodeSub5Digit000 + "' ";
                int status10 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDeleteLSPRMGLPLCODE000, "A2ZGLMCUS"));
                
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
                    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE  GLOPBAL = 0 AND GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 ";
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
                }

                if (ChkAllCashCode.Checked == false)
                {
                    string strQuery = @"DELETE FROM A2ZGLMCUS.DBO.WFINCEXPREP WHERE  GLDrSumC =  0 AND GLDrSumT = 0 AND GLCrSumC = 0 AND GLCrSumT = 0 ";
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                }


                               
                    if (rbtOpt3rdLayer.Checked)
                       {
                           if (ChkAllCashCode.Checked)
                           {
                               SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Receipts & Payments - Summary");

                           }
                           else
                           {
                               SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Receipts & Payments (By Cash Code) - Summary");

                               string TempCashCode = Converter.GetString(txtCashCode.Text).Trim();
                               //  For Cash Code Description 

                               SessionStore.SaveToCustomStore(Params.COMMON_NAME2, TempCashCode);
                               SessionStore.SaveToCustomStore(Params.COMMON_NAME3, lblCodeDesc.Text);
                           }


                           
                       }


                    else if (rbtOpt4thLayer.Checked)
                      {
                          if (ChkAllCashCode.Checked)
                          {
                              SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Receipts & Payments - Details");

                           }
                        else
                          {
                              SessionStore.SaveToCustomStore(Params.COMMON_NAME1, "Receipts & Payments (By Cash Code) - Details");
                              string TempCashCode = Converter.GetString(txtCashCode.Text).Trim();
                      
                              //  For Cash Code Description 
                           
                              SessionStore.SaveToCustomStore(Params.COMMON_NAME2, TempCashCode);
                              SessionStore.SaveToCustomStore(Params.COMMON_NAME3, lblCodeDesc.Text);
                          }

                      }

                
                 //  Report
                             
                if (rbtOpt3rdLayer.Checked)
                   {
                       if (ChkAllCashCode.Checked)
                       {
                           SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptReceivedPayment3rd");
                       }

                       else
                       {
                           SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptReceivedPayment3rdByAccCode");
                       }
                                                          
                   }
                




                if (rbtOpt4thLayer.Checked)
                {
                    if (ChkAllCashCode.Checked)
                    
                      {
                         SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptReceivedPayment4th");
                      }
                    
                     else


                      {
                          SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptReceivedPayment4thByAccCode");
                      }

                    
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

        protected void txtCashCode_TextChanged(object sender, EventArgs e)
        {
            // Cheaking 8 digit - GL Account Code
            if (txtCashCode.Text.Length != 8)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{

                //    String cstext1 = "alert('Invalid - GL Account Code');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtCashCode.Text = string.Empty;
                txtCashCode.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Account Code');", true);
                return;

            }
            // Cheaking - GL  Cash Account Code
            string glno = txtCashCode.Text;
            string glAccno = glno.Substring(0, 5);
            string qry = "SELECT GLAccNo FROM A2ZCGLMST WHERE Left(GLAccNo,5)='" + glAccno + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
            if (dt.Rows.Count == 0)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{

                //    String cstext1 = "alert('Invalid - GL Cash Code');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtCashCode.Text = string.Empty;
                txtCashCode.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid - GL Cash Code');", true);
                return;

            }

            string qry1 = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + txtCashCode.Text + "'";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblCodeDesc.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                txtCashCode.Focus();
            }
            
          
        }

        protected void ChkAllCashCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCashCode.Checked == true)
            {
                txtCashCode.Enabled = false;
                lblCodeDesc.Visible = false;



            }
            else
            {
                txtCashCode.Enabled = true;
                lblCodeDesc.Visible = true;
                txtCashCode.Focus();

            }
        }

       

        
    }
}