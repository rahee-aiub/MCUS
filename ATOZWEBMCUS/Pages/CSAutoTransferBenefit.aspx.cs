using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using System.Drawing;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAutoTransferBenefit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    ddlAccountType();
                    ddlAccType.SelectedValue = "17";

                    ValidationProcess();

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;

                    A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(hdnID.Text), "A2ZHKMCUS");

                    if (sysobj.VPrintflag == false)
                    {
                        lblVPrintFlag.Text = "0";
                    }
                    else
                    {
                        lblVPrintFlag.Text = "1";
                    }


                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text == string.Empty)
                    {
                        //BtnCalculate.Visible = false;
                        //BtnPrint.Visible = false;
                        //lblVchNo.Visible = false;
                        //txtVchNo.Visible = false;
                        //BtnPost.Visible = false;
                        //BtnReverse.Visible = false;
                        //BtnExit.Visible = true;
                        lblTotalAmt.Visible = false;
                    }
                    else
                    {
                        string RddlAccType = (string)Session["SddlAccType"];

                        string RlblAtyClass = (string)Session["SlblAtyClass"];

                        string RBtnCalculate = (string)Session["SBtnCalculate"];
                        string RBtnPrint = (string)Session["SBtnPrint"];
                        string RBtnPost = (string)Session["SBtnPost"];
                        string RBtnReverse = (string)Session["SBtnReverse"];

                        string RBtnExit = (string)Session["SBtnExit"];

                        ddlAccType.SelectedValue = RddlAccType;

                        lblAtyClass.Text = RlblAtyClass;

                        if (RBtnCalculate == "1")
                        {
                            BtnCalculate.Visible = true;
                        }
                        else
                        {
                            BtnCalculate.Visible = false;
                        }

                        if (RBtnPrint == "1")
                        {
                            BtnPrint.Visible = true;
                        }
                        else
                        {
                            BtnPrint.Visible = false;
                        }

                        if (RBtnPost == "1")
                        {
                            BtnPost.Visible = true;
                        }
                        else
                        {
                            BtnPost.Visible = false;
                        }

                        if (RBtnReverse == "1")
                        {
                            BtnReverse.Visible = true;
                        }
                        else
                        {
                            BtnReverse.Visible = false;
                        }

                        if (RBtnExit == "1")
                        {
                            BtnExit.Visible = true;
                        }
                        else
                        {
                            BtnExit.Visible = false;
                        }

                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();

                        if (txtTotalBenefit.Text != string.Empty && txtTotalBenefit.Text != "0" && txtTotalBenefit.Text != "00.00")
                        {
                            if (CtrlProgFlag.Text == "1")
                            {
                                txtVchNo.Text = string.Empty;
                                lblVchNo.Visible = true;
                                txtVchNo.Visible = true;
                                txtVchNo.Focus();
                                if (RBtnReverse != "1")
                                {
                                    BtnPost.Visible = true;
                                }
                            }
                            else
                            {
                                txtVchNo.Text = string.Empty;
                                lblVchNo.Visible = true;
                                txtVchNo.Visible = true;
                                BtnPost.Visible = false;
                                BtnCalculate.Visible = false;
                                BtnReverse.Visible = true;
                            }
                        }
                        else
                        {
                            lblVchNo.Visible = false;
                            txtVchNo.Visible = false;
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


        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;

            Session["SddlAccType"] = string.Empty;

            Session["SlblAtyClass"] = string.Empty;

            Session["SBtnCalculate"] = string.Empty;

            Session["SBtnPrint"] = string.Empty;

            Session["SBtnPost"] = string.Empty;

            Session["SBtnReverse"] = string.Empty;


            Session["SBtnExit"] = string.Empty;

        }
        protected void ValidationProcess()
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);

                DataTable dt1 = new DataTable();


                string qry1 = "SELECT Id,TrnDate,ProcStat,VoucherNo FROM WFCSMONTHLYBENEFITCREDIT where AccType='" + ddlAccType.SelectedValue + "'";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");


                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);
                    CtrlVchNo.Text = Converter.GetString(dt1.Rows[0]["VoucherNo"]);

                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = true;
                        txtVchNo.Visible = true;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                    }
                    if (TrnDate == ProcDt && ProcStat == 2)
                    {
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = true;
                        txtVchNo.Visible = true;
                        BtnPost.Visible = true;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                    }
                    if (TrnDate == ProcDt && ProcStat == 1)
                    {
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                    }
                    if (TrnDate != ProcDt)
                    {
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                    }
                }
                else
                {
                    BtnCalculate.Visible = true;
                    BtnPrint.Visible = true;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ValidationProcess Problem');</script>");
                //throw ex;
            }
        }
        protected void ddlAccountType()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeCode=17";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }


        protected void gvPreview()
        {
            try
            {

                string sqlquery3 = "SELECT CuNumber,MemNo,AccNo,MemName,AccMthBenefitAmt,NoMonths,AccTotalBenefitAmt,AccCorrAccNo FROM WFCSMONTHLYBENEFITCREDIT Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuNumber";
                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }


        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");

            }
        }



        protected void UpdateMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "Auto Transfer Benefit Posting Sucessfully Done";


            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //---------------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Renewal Posting Sucessfully Done";


            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVchNo.Text);

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
        protected void DeleteMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Renewal Posting Deleted');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Auto Transfer Benefit Posting Deleted');", true);
            return;
        }
        protected void gvSumValue()
        {
            try
            {


                Decimal sumNetBenefitAmt = 0;


                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {

                    sumNetBenefitAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[6].Text));


                }



                txtTotalBenefit.Text = Convert.ToString(String.Format("{0:0,0.00}", sumNetBenefitAmt));


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
                //throw ex;
            }
        }

        protected void InvalidVchMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher No. Not Exist');", true);
            return;

        }

        protected void EmptyVchMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
            return;

        }



        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {

                var prm = new object[2];
                prm[0] = hdnID.Text;
                prm[1] = ddlAccType.SelectedValue;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateTransferMonthlyBenefit", prm, "A2ZCSMCUS"));
                if (result == 0)
                {

                    gvDetailInfo.Visible = true;

                    gvPreview();
                    lblTotalAmt.Visible = true;
                    gvSumValue();
                }



            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCalculate Problem');</script>");
                //throw ex;
            }

        }

        protected void DuplicateVchMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;

        }

        protected void TrnVchDeplicate()
        {
            try
            {

                hdnMsgFlag.Text = "0";

                DateTime opdate = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    hdnMsgFlag.Text = "1";

                    txtVchNo.Text = string.Empty;
                    txtVchNo.Focus();
                    DuplicateVchMSG();
                    return;
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnVchDeplicate Problem');</script>");
                //throw ex;
            }
        }
        protected void BtnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVchNo.Text == string.Empty)
                {
                    txtVchNo.Focus();


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }


                TrnVchDeplicate();
                if (hdnMsgFlag.Text == "1")
                {
                    return;
                }


                VerifyBenefitTransferPost();

                if (lblPostFlag.Text == "0")
                {
                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //int GLCode = Converter.GetInteger(dto.CashCode);

                    int GLCode = Converter.GetInteger(hdnCashCode.Text);
                    Int16 RecType = Converter.GetSmallInteger(1);
                    A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                    CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;

                    int result = 0;

                    var prm = new object[4];
                    prm[0] = ddlAccType.SelectedValue;
                    prm[1] = CtrlVchNo.Text;
                    prm[2] = txtVchNo.Text.Trim();
                    prm[3] = hdnCashCode.Text;



                    result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateTransferMonthlyBenefit", prm, "A2ZCSMCUS"));


                    if (result == 0)
                    {

                        if (lblVPrintFlag.Text == "1")
                        {
                            PrintTrnVoucher();
                        }
                        else
                        {
                            UpdateMSG();
                        }

                        BtnCalculate.Visible = false;
                        txtVchNo.Text = string.Empty;
                        lblVchNo.Visible = true;
                        txtVchNo.Visible = true;
                        BtnPost.Visible = false;
                        BtnPrint.Visible = true;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();

                    }
                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPost_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void VerifyBenefitTransferPost()
        {
            try
            {
                
                lblPostFlag.Text = "0";
                int CRow = 1;
                string qry = "SELECT CuType,CuNo,MemNo,AccType,AccNo,AccTotalBenefitAmt,AccCorrAccNo FROM WFCSMONTHLYBENEFITCREDIT";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var cutype = dr["CuType"].ToString();
                        var cuno = dr["CuNo"].ToString();
                        var memno = dr["MemNo"].ToString();
                        var acctype = dr["AccType"].ToString();
                        var accno = dr["AccNo"].ToString();
                        var totbenefitamt = dr["AccTotalBenefitAmt"].ToString();
                        var corraccno = dr["AccCorrAccNo"].ToString();

                        ChkBenefitAmt.Text = Converter.GetString(totbenefitamt);

                        DataTable dt1 = new DataTable();
                        string qry1 = "SELECT Id,AccProvBalance,AccAutoTrfFlag,AccAdjProvBalance FROM A2ZACCOUNT where AccNo='" + accno + "'";
                        dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                        if (dt1.Rows.Count > 0)
                        {
                            Decimal AccProvBal = Converter.GetDecimal(dt1.Rows[0]["AccProvBalance"]);
                            Decimal AdjProvBal = Converter.GetDecimal(dt1.Rows[0]["AccAdjProvBalance"]);
                            Int16 AutoFlag = Converter.GetSmallInteger(dt1.Rows[0]["AccAutoTrfFlag"]);

                            Decimal NetProvBal = (AccProvBal + AdjProvBal);

                            decimal wfProvBal = Converter.GetDecimal(ChkBenefitAmt.Text);

                            if (wfProvBal != NetProvBal)
                            {
                                lblPostFlag.Text = "1";
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Re-Calculate Auto Benefit Process');", true);
                                return;
                            }

                            if (AutoFlag == 0 || AutoFlag == 2)
                            {
                                lblPostFlag.Text = "1";
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Re-Calculate Auto Benefit Process');", true);
                                return;
                            }

                        }

                    }

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.QryTransControl Problem');</script>");
                //throw ex;
            }

        }


        protected void PrintTrnVoucher()
        {
            try
            {
                SessionStoreValue();

                Session["ProgFlag"] = "2";


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                int accType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblPdate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAccType.SelectedItem.Text);



                DateTime Pdate = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Pdate);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "TRANSFER");

                Int32 CSGL = Converter.GetSmallInteger(0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, hdnID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSSumTransactionVch");

                Response.Redirect("ReportServer.aspx", false);



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdateBackUpStat()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);

            if (CtrlBackUpStat.Text == "1")
            {
                Int16 BStat = 0;
                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }
            }
        }
        protected void BtnReverse_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVchNo.Text == string.Empty)
                {
                    txtVchNo.Focus();


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }


                string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where AccType ='" + ddlAccType.SelectedValue + "' AND FuncOpt = 21 AND VchNo ='" + txtVchNo.Text.Trim() + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    int result = 0;

                    var prm = new object[2];

                    prm[0] = ddlAccType.SelectedValue;
                    prm[1] = txtVchNo.Text;


                    result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteTransferMonthlyBenefit", prm, "A2ZCSMCUS"));


                    if (result == 0)
                    {

                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        gvDetailInfo.Visible = false;
                        lblTotalAmt.Visible = false;
                        UpdateBackUpStat();
                        DeleteMSG();
                    }
                }
                else
                {
                    txtVchNo.Text = string.Empty;
                    txtVchNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Reverse Transaction');", true);
                    return;
                }


            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReverse_Click Problem');</script>");
                //throw ex;
            }


        }

        protected void SessionStoreValue()
        {
            Session["ProgFlag"] = "1";

            Session["SddlAccType"] = ddlAccType.SelectedValue;

            Session["SlblAtyClass"] = lblAtyClass.Text;



            if (BtnCalculate.Visible == true)
            {
                Session["SBtnCalculate"] = "1";
            }
            else
            {
                Session["SBtnCalculate"] = "0";
            }

            if (BtnPrint.Visible == true)
            {
                Session["SBtnPrint"] = "1";
            }
            else
            {
                Session["SBtnPrint"] = "0";
            }

            if (BtnPost.Visible == true)
            {
                Session["SBtnPost"] = "1";
            }
            else
            {
                Session["SBtnPost"] = "0";
            }

            if (BtnReverse.Visible == true)
            {
                Session["SBtnReverse"] = "1";
            }
            else
            {
                Session["SBtnReverse"] = "0";
            }

            if (BtnExit.Visible == true)
            {
                Session["SBtnExit"] = "1";
            }
            else
            {
                Session["SBtnExit"] = "0";
            }

        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                SessionStoreValue();

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                int accType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblPdate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAccType.SelectedItem.Text);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoTransferMonthlyBenefit");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}