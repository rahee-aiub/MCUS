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

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAutoMonthlyBenefit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    ddlAccountType();
                    ValidationProcess();
                    lblTotalAmt.Visible = false;
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void ValidationProcess()
        {
            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);

                string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSMONTHLYBENEFIT where Id = 1";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);
                    CtrlVchNo.Text = Converter.GetString(dt1.Rows[0]["VoucherNo"]);

                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
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
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeClass='3'";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }
        protected void gvPreview()
        {
            string sqlquery3 = "SELECT CuNumber,MemNo,AccNo,MemName,CalBenefitDate,AccFixedAmt,CalBenefit FROM WFCSMONTHLYBENEFIT Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void gvSumValue()
        {
            try
            {

                Decimal sumfdAmt = 0;
                Decimal sumBenfitAmt = 0;

                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {

                    sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[5].Text));
                    sumBenfitAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[6].Text));


                }

                txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                txtTotalBenfitAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumBenfitAmt));

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
                //throw ex;
            }
        }

        protected void UpdateMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "Benefit Posting Sucessfully Done";


            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //-----------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Benefit Posting Sucessfully Done";


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
            //    String cstext1 = "alert('Benefit Posting Deleted');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Benefit Posting Deleted');", true);
            return;
        }

        protected void InvalidVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher No. Not Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher No. Not Exist');", true);
            return;

        }

        protected void EmptyVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
            return;

        }




        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var prm = new object[1];
                prm[0] = hdnID.Text;
                if (ddlAccType.SelectedValue == "17")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateMonthlyBenefit", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();
                    }
                }
                //else if (ddlAccType.SelectedValue == "16")
                //{
                //    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSRenewalFDR", "A2ZCSMCUS"));
                //    if (result == 0)
                //    {
                //        gvPreview();
                //    }
                //}
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCalculate_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void DuplicateVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher Already Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

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

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Vch.No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }


                TrnVchDeplicate();
                if (hdnMsgFlag.Text == "1")
                {
                    return;
                }

                //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                //int GLCode = Converter.GetInteger(dto.CashCode);


                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;

                if (ddlAccType.SelectedValue == "17")
                {

                    var prm = new object[3];
                    prm[0] = CtrlVchNo.Text;
                    prm[1] = txtVchNo.Text.Trim();
                    prm[2] = hdnCashCode.Text;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateMonthlyBenefit", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                      
                        BtnCalculate.Visible = false;
                        BtnPost.Visible = false;
                        BtnPrint.Visible = true;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                        UpdateMSG();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPost_Click Problem');</script>");
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

                if (ddlAccType.SelectedValue == "17")
                {

                    var prm = new object[2];


                    prm[0] = ddlAccType.SelectedValue;
                    prm[1] = "16";

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteMonthlyBenefit", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                     
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                        DeleteMSG();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReverse_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
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

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

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
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoMonthlyBenefit");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

    }
}

