using System;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLAccountStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (IsPostBack)
                {

                }
                else
                {
                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text != "1")
                    {
                        hdnPrmValue.Text = Request.QueryString["a%b"];
                        string b = hdnPrmValue.Text;
                        HdnModule.Text = b;


                        A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtfdate.Text = Converter.GetString(date);
                        txttdate.Text = Converter.GetString(date);
                        txtGLCode.Focus();

                        HeaderDropdown();
                    }
                    else
                    {
                        string RHdnModule = (string)Session["SHdnModule"];

                        string RtxtGLCode = (string)Session["StxtGLCode"];
                        string RddlGLCode = (string)Session["SddlGLCode"];

                        string RhdnTranCode = (string)Session["ShdnTranCode"];

                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string RrbtGLAccStatement = (string)Session["SrbtGLAccStatement"];
                        string RrbtGLAccStVchConsolated = (string)Session["SrbtGLAccStVchConsolated"];
                        string RrbtGLAccStDailyConsolated = (string)Session["SrbtGLAccStDailyConsolated"];

                        string RhdnTranHead2 = (string)Session["ShdnTranHead2"];
                        string RhdnTranHead3 = (string)Session["ShdnTranHead3"];
                        string RhdnTranHead4 = (string)Session["ShdnTranHead4"];

                        HdnModule.Text = RHdnModule;


                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;

                        if (RrbtGLAccStatement == "1")
                        {
                            rbtGLAccStatement.Checked = true;
                        }
                        else
                        {
                            rbtGLAccStatement.Checked = false;
                        }

                        if (RrbtGLAccStVchConsolated == "1")
                        {
                            rbtGLAccStVchConsolated.Checked = true;
                        }
                        else
                        {
                            rbtGLAccStVchConsolated.Checked = false;
                        }

                        if (RrbtGLAccStDailyConsolated == "1")
                        {
                            rbtGLAccStDailyConsolated.Checked = true;
                        }
                        else
                        {
                            rbtGLAccStDailyConsolated.Checked = false;
                        }

                        if (HdnModule.Text != "6")
                        {
                            hdnTranHead2.Text = RhdnTranHead2;
                            hdnTranHead3.Text = RhdnTranHead3;
                            hdnTranHead4.Text = RhdnTranHead4;

                            txtGLCode.Text = RtxtGLCode;
                            GLCodeDropdown();
                            ddlGLCode.SelectedValue = Converter.GetString(txtGLCode.Text);
                            hdnTranCode.Text = Converter.GetString(ddlGLCode.SelectedValue);
                            TranCodeDropDown();
                        }
                    }


                    if (HdnModule.Text == "6")
                    {
                        lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                        txtGLCode.Text = lblCashCode.Text;
                        GLCodeDropdown();
                        ddlGLCode.SelectedValue = Converter.GetString(txtGLCode.Text);
                        //txtGLCode.Enabled = false;
                        ddlGLCode.Enabled = false;
                        btnBack2.Enabled = false;
                    }



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["StxtGLCode"] = string.Empty;
            Session["SddlGLCode"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SrbtGLAccStatement"] = string.Empty;
            Session["SrbtGLAccStVchConsolated"] = string.Empty;
            Session["SrbtGLAccStDailyConsolated"] = string.Empty;
            Session["ShdnTranHead2"] = string.Empty;
            Session["ShdnTranHead3"] = string.Empty;
            Session["ShdnTranHead4"] = string.Empty;
            Session["SHdnModule"] = string.Empty;

            Session["ShdnTranCode"] = string.Empty;
        }


        protected void GLCodeDropdown()
        {
            string sqlQueryAcc = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 6";
            ddlGLCode = CommonManager.Instance.FillDropDownList(sqlQueryAcc, ddlGLCode, "A2ZGLMCUS");
        }


        protected void HeaderDropdown()
        {

            hdnTranHead1.Text = "0";
            hdnTranHead2.Text = "0";
            hdnTranHead3.Text = "0";
            hdnTranHead4.Text = "0";

            string sqlQueryAcc = @"SELECT GLHead, LTRIM (GLHeadDesc) as GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC";
            ddlGLCode = CommonManager.Instance.FillDropDownList(sqlQueryAcc, ddlGLCode, "A2ZGLMCUS");
            ddlGLCode.SelectedItem.Text = "-Select Header Code-";




            hdnTranHead1.Text = "1";


            btnBack2.Visible = false;
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGLCode.SelectedItem.Text == "-Select-")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Code - Not Abailable');", true);
                    return;
                }

                Session["ProgFlag"] = "1";

                Session["SHdnModule"] = HdnModule.Text;

                Session["StxtGLCode"] = txtGLCode.Text;
                Session["SddlGLCode"] = ddlGLCode.SelectedValue;

                Session["ShdnTranCode"] = hdnTranCode.Text;

                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                if (rbtGLAccStatement.Checked == true)
                {
                    Session["SrbtGLAccStatement"] = "1";
                }
                else
                {
                    Session["SrbtGLAccStatement"] = "0";
                }

                if (rbtGLAccStVchConsolated.Checked == true)
                {
                    Session["SrbtGLAccStVchConsolated"] = "1";
                }
                else
                {
                    Session["SrbtGLAccStVchConsolated"] = "0";
                }

                if (rbtGLAccStDailyConsolated.Checked == true)
                {
                    Session["SrbtGLAccStDailyConsolated"] = "1";
                }
                else
                {
                    Session["SrbtGLAccStDailyConsolated"] = "0";
                }

                Session["ShdnTranHead2"] = hdnTranHead2.Text;
                Session["ShdnTranHead3"] = hdnTranHead3.Text;
                Session["ShdnTranHead4"] = hdnTranHead4.Text;

                //DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //  For Company Name and Address

                //var prm = new object[4];

                //prm[0] = Converter.GetInteger(txtGLCode.Text);
                //prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                //prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                //prm[3] = 0;

                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAccountStatement", prm, "A2ZGLMCUS"));


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCOUNTCODE, Converter.GetInteger(txtGLCode.Text));

                // GL Account Code
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, Converter.GetInteger(txtGLCode.Text));

                // Report Header               
                if (rbtGLAccStatement.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "GENERAL LEDGER ACCOUNT STATEMENT ");
                }

                else if (rbtGLAccStVchConsolated.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "GENERAL LEDGER ACCOUNT STATEMENT [ By Voucher wise  Consolidated ]");
                }

                else if (rbtGLAccStDailyConsolated.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "GENERAL LEDGER ACCOUNT STATEMENT [ By Day wise  Consolidated ]");
                }

                // GL Code Description
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlGLCode.SelectedItem.Text);


                // Call Report

                if (rbtGLAccStatement.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLAccountStatement");
                }
                else if (rbtGLAccStVchConsolated.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLAccountStatementByVchWiseConsolidated");
                }

                else if (rbtGLAccStDailyConsolated.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLAccountStatementByDateWiseConsolidated");
                }

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        protected void ddlGLCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLCode.SelectedValue == "-Select-")
                {
                    txtGLCode.Text = string.Empty;
                    return;
                }


                hdnTranCode.Text = Converter.GetString(ddlGLCode.SelectedValue);

                TranCodeDropDown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlContra_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        //protected void ddlGLCode_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (ddlGLCode.SelectedValue == "-Select-")
        //    {
        //        //ClearInfoAdd();
        //        txtGLCode.Text = string.Empty;
        //        return;
        //    }

        //    //refresh();
        //    try
        //    {
        //        txtGLCode.Text = Converter.GetString(ddlGLCode.SelectedValue);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        private void InvalidCode()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
            return;
        }

        protected void txtGLCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int GLCode;
                A2ZCGLMSTDTO glObj = new A2ZCGLMSTDTO();
                string input1 = Converter.GetString(txtGLCode.Text).Length.ToString();
                if (input1 == "6")
                {
                    GLCode = Converter.GetInteger(txtGLCode.Text);
                    glObj = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                }
                else
                {
                    GLCode = Converter.GetInteger(txtGLCode.Text);
                    glObj = (A2ZCGLMSTDTO.GetInformation(GLCode));
                }

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtGLCode.Text = string.Empty;
                        txtGLCode.Focus();
                    }
                    else
                    {
                        txtGLCode.Text = Converter.GetString(glObj.GLAccNo);
                        CtrlAccType.Text = Converter.GetString(glObj.GLAccType);
                        glMainHead.Text = Converter.GetString(glObj.GLMainHead);
                        glSubHead.Text = Converter.GetString(glObj.GLSubHead);

                        if (HdnModule.Text == "6")
                        {
                            if (glSubHead.Text == "10101000" && txtGLCode.Text != lblCashCode.Text)
                            {
                                txtGLCode.Text = string.Empty;
                                txtGLCode.Text = lblCashCode.Text;
                                txtGLCode.Focus();
                                InvalidCode();
                                return;
                            }
                            else
                                if (glSubHead.Text != "10106000" && glSubHead.Text != "20801000")
                                {
                                    txtGLCode.Text = string.Empty;
                                    txtGLCode.Text = lblCashCode.Text;
                                    txtGLCode.Focus();
                                    InvalidCode();
                                    return;
                                }
                                else 
                                {
                                    ddlGLCode.SelectedValue = Converter.GetString(txtGLCode.Text);
                                    return;               
                                }
                        }


                        hdnTranHead1.Text = "1";
                        hdnTranHead2.Text = "0";
                        hdnTranHead3.Text = "0";
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtGLCode.Text);
                        TranCodeDropDown();


                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = "0";
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtGLCode.Text);
                        TranCodeDropDown();

                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = hdnTranCode.Text;
                        hdnTranHead4.Text = "0";

                        hdnTranCode.Text = Converter.GetString(txtGLCode.Text);
                        TranCodeDropDown();

                        hdnTranHead2.Text = hdnTranCode.Text;
                        hdnTranHead3.Text = hdnTranCode.Text;
                        hdnTranHead4.Text = hdnTranCode.Text;

                        ddlGLCode.SelectedValue = Converter.GetString(txtGLCode.Text);
                        TranCodeDropDown();


                        //ddlContra.SelectedValue = Converter.GetString(glObj.GLAccNo);
                        ddlGLCode_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                }
                else
                {
                    Validity();
                    txtGLCode.Text = string.Empty;
                    txtGLCode.Focus();
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnsactionCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }




        //protected void txtGLCode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtGLCode.Text));

        //        if (glObj.GLAccNo > 0)
        //        {
        //            CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
        //            if (CtrlRecType.Text != "2")
        //            {
        //                InvalidGL();
        //                txtGLCode.Text = string.Empty;
        //                txtGLCode.Focus();
        //            }
        //            else
        //            {
        //                txtGLCode.Text = Converter.GetString(glObj.GLAccNo);
        //                ddlGLCode.SelectedValue = Converter.GetString(glObj.GLAccNo);
        //                ddlGLCode_SelectedIndexChanged(this, EventArgs.Empty);
        //            }

        //        }
        //        else
        //        {

        //            Validity();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        private void Validity()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('GL Code - Does Not Exist  ');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Code - Does Not Exist');", true);
            return;
        }

        private void InvalidGlCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Not Trans. Header Record');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
            return;
        }
        private void InvalidGL()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid GL Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
            return;
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {

            txtGLCode.Text = string.Empty;
            if (hdnTranHead4.Text != "0")
            {
                hdnTranCode.Text = hdnTranHead4.Text;
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }
            else if (hdnTranHead3.Text != "0")
            {
                hdnTranCode.Text = hdnTranHead3.Text;
                hdnTranHead2.Text = "0";
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }
            else
            {
                hdnTranCode.Text = hdnTranHead2.Text;
                hdnTranHead1.Text = "0";
                hdnTranHead2.Text = "0";
                hdnTranHead3.Text = "0";
                hdnTranHead4.Text = "0";
            }



            TranCodeDropDown();

        }

        private void TranCodeDropDown()
        {
            try
            {
                if (hdnTranHead1.Text == "0")
                {
                    btnBack2.Visible = false;
                }
                else
                {
                    btnBack2.Visible = true;
                }


                if (hdnTranHead1.Text == "0")
                {
                    hdnTranHead1.Text = Converter.GetString(hdnTranCode.Text);
                    string sqlQueryTran = @"SELECT GLHead, LTRIM (GLHeadDesc) as GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ORDER BY GLHeadDesc ASC ";
                    ddlGLCode = CommonManager.Instance.FillDropDownList(sqlQueryTran, ddlGLCode, "A2ZGLMCUS");
                    ddlGLCode.SelectedItem.Text = "-Select Header Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead2.Text == "0")
                {
                    hdnTranHead2.Text = Converter.GetString(hdnTranCode.Text);
                    string input2 = hdnTranCode.Text;
                    string sub3 = input2.Substring(0, 1);
                    string sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 1 AND GLAccType = '" + sub3 + "' GROUP BY GLAccNo,GLAccDesc ORDER BY GLAccDesc ASC ";
                    ddlGLCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlGLCode, "A2ZGLMCUS");
                    ddlGLCode.SelectedItem.Text = "-Select Main Head Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead3.Text == "0")
                {
                    hdnTranHead3.Text = Converter.GetString(hdnTranCode.Text);
                    string inputMHead = hdnTranCode.Text;
                    string subMHead = hdnTranCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 3);
                    string sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc Order By GLAccDesc ASC ";
                    ddlGLCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlGLCode, "A2ZGLMCUS");
                    ddlGLCode.SelectedItem.Text = "-Select Sub Head Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else if (hdnTranHead4.Text == "0")
                {
                    hdnTranHead4.Text = Converter.GetString(hdnTranCode.Text);
                    string inputMHead = hdnTranCode.Text;
                    string subMHead = hdnTranCode.Text.Substring(0, 1);
                    string sub4 = inputMHead.Substring(0, 5);
                    string sqlquery1 = @"SELECT GLAccNo, LTRIM (GLAccDesc) as GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 6 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,5)='" + sub4 + "' GROUP BY GLAccNo,GLAccDesc Order By GLAccDesc ASC";
                    ddlGLCode = CommonManager.Instance.FillDropDownList(sqlquery1, ddlGLCode, "A2ZGLMCUS");
                    ddlGLCode.SelectedItem.Text = "-Select Details Code-";
                    //txtTrnsactionCode.Text = string.Empty;
                }
                else
                {
                    txtGLCode.Text = Converter.GetString(ddlGLCode.SelectedValue);

                    int Code = Converter.GetInteger(ddlGLCode.SelectedValue);

                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Code);
                    if (glObj.GLAccNo > 0)
                    {

                        CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                        CtrlAccType.Text = Converter.GetString(glObj.GLAccType);

                        if (CtrlRecType.Text != "2")
                        {
                            InvalidGlCode();
                            txtGLCode.Text = string.Empty;
                            txtGLCode.Focus();
                        }
                    }
                    else
                    {
                        Validity();
                        txtGLCode.Text = string.Empty;
                        txtGLCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TranCodeDropDown Problem');</script>");
                //throw ex;
            }
        }

    }




}
