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
    public partial class CSAutoAnniversary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    ddlAccountType();
                    BtnCalculate.Visible = false;
                    BtnPrint.Visible = false;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;
                    lblTotalAmt.Visible = false;
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

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
                gvDetailInfoFDR.Visible = false;
                gvDetailInfo6YR.Visible = false;
                lblTotalAmt.Visible = false;
                txtTotalFDAmt.Visible = false;
                txtTotalIntRate.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);

                DataTable dt1 = new DataTable();
                if (ddlAccType.SelectedValue == "15")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSANNIVERSARYFDR where Id = 1";
                    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSANNIVERSARY6YR where Id = 1";
                    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                }

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
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeClass='2'";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationProcess();
        }
        protected void gvPreview()
        {
            try
            {
                if (ddlAccType.SelectedValue == "15")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,FDAmount,AccIntRate,NoDays,CalInterest FROM WFCSANNIVERSARYFDR Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfoFDR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoFDR, "A2ZCSMCUS");
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,FDAmount,AccIntRate,NoDays,CalInterest FROM WFCSANNIVERSARY6YR Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfo6YR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo6YR, "A2ZCSMCUS");
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetailInfoFDR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDetailInfo6YR_RowDataBound(object sender, GridViewRowEventArgs e)
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

            a = "Anniversary Posting Sucessfully Done";


            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //--------------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Anniversary Posting Sucessfully Done";


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
            //    String cstext1 = "alert('Provision Posting Deleted');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Provision Posting Deleted');", true);
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
        protected void gvSumValue()
        {
            try
            {

                Decimal sumfdAmt = 0;
                Decimal sumIntRate = 0;
                lblTotalAmt.Visible = true;
                txtTotalFDAmt.Visible = true;
                txtTotalIntRate.Visible = true;


                if (ddlAccType.SelectedValue == "15")
                {
                    for (int i = 0; i < gvDetailInfoFDR.Rows.Count; ++i)
                    {

                        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[5].Text));

                        sumIntRate += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[7].Text));

                    }

                    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                    txtTotalIntRate.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntRate));


                }
                if (ddlAccType.SelectedValue == "16")
                {
                    for (int i = 0; i < gvDetailInfo6YR.Rows.Count; ++i)
                    {

                        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[5].Text));

                        sumIntRate += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[7].Text));
                    }

                    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                    txtTotalIntRate.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntRate));

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var prm = new object[1];

                prm[0] = hdnID.Text;

                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateAnniversaryFDR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        gvDetailInfoFDR.Visible = true;
                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();
                    }
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateAnniversary6YR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        gvDetailInfo6YR.Visible = true;
                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();
                    }
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

                var prm = new object[3];
                prm[0] = CtrlVchNo.Text;
                prm[1] = txtVchNo.Text.Trim();
                prm[2] = hdnCashCode.Text;

                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateAnniversaryFDR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        BtnCalculate.Visible = false;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnPrint.Visible = true;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                        UpdateMSG();
                    }
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateAnniversary6YR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        
                        BtnCalculate.Visible = false;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
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

                var prm = new object[2];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = "14";

                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteAnniversaryFDR", prm, "A2ZCSMCUS"));
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
                if (ddlAccType.SelectedValue == "16")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteAnniversary6YR", prm, "A2ZCSMCUS"));
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

                if (ddlAccType.SelectedValue == "15")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoAnniversaryFdrList");
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoAnniversary6YRList");
                }
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
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}