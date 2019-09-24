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
    public partial class CSAutoProvision : System.Web.UI.Page
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
                    lblTotalAmt1.Visible = false;




                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;
                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

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

                gvDetailInfoCPS.Visible = false;
                gvDetailInfoFDR.Visible = false;
                gvDetailInfo6YR.Visible = false;
                lblTotalAmt.Visible = false;
                lblTotalAmt1.Visible = false;
                txtTotalFDAmt.Visible = false;
                txtTotaluptolastmnth.Visible = false;
                txtTotalthisMonth.Visible = false;
                txtTotalUptoMonth.Visible = false;
                txtTotalInt.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);


                DataTable dt1 = new DataTable();
                if (ddlAccType.SelectedValue == "14")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSPROVISIONCPS where Id = 1";
                    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSPROVISIONFDR where Id = 1";
                    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                }
                else if (ddlAccType.SelectedValue == "16")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSPROVISION6YR where Id = 1";
                    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                }
                else if (hdnAccTypeClass.Text == "6")
                {
                    string qry1 = "SELECT Id,TrnDate,VoucherNo,ProcStat FROM WFCSPROVISIONLOAN where CuType = 0 AND AccType ='" + ddlAccType.SelectedValue + "'";
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
                    else
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
                        else
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
                            else
                                if (TrnDate.Month == ProcDt.Month && TrnDate.Year == ProcDt.Year)
                                {
                                    BtnCalculate.Visible = false;
                                    BtnPrint.Visible = true;
                                    lblVchNo.Visible = false;
                                    txtVchNo.Visible = false;
                                    BtnPost.Visible = false;
                                    BtnReverse.Visible = false;
                                    BtnExit.Visible = true;
                                }
                                else
                                    if (TrnDate.Month != ProcDt.Month)
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
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeClass = 2 or AccTypeClass = 4 or (AccTypeClass = 6 and AccTypeMode = 1)";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 AccType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
            A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
            if (get1DTO.AccTypeCode > 0)
            {
                hdnAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);
            }


            ValidationProcess();

        }
        protected void gvPreview()
        {
            try
            {

                if (ddlAccType.SelectedValue == "14")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccOpenDate,AccMatureDate,AccPeriodMonths,CurrMthProduct,AccIntRate,CurrMthProvision FROM WFCSPROVISIONCPS Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfoCPS = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoCPS, "A2ZCSMCUS");
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccFDAmount,AccIntRate,UptoLastMthProvision,UptoMthProvision,CurrMthProvision FROM WFCSPROVISIONFDR Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfoFDR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoFDR, "A2ZCSMCUS");
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccFDAmount,AccIntRate,UptoLastMthProvision,UptoMthProvision,CurrMthProvision FROM WFCSPROVISION6YR Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfo6YR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo6YR, "A2ZCSMCUS");
                }
                else if (hdnAccTypeClass.Text == "6")
                {
                    string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccBalance,AccIntRate,UptoLastMthProvision,UptoMthProvision,CurrMthProvision FROM WFCSPROVISIONLOAN Where CuType !=0 AND AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                    gvDetailInfoLOAN = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoLOAN, "A2ZCSMCUS");
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }

        protected void gvSumValue()
        {
            try
            {

                Decimal sumfdAmt = 0;
                Decimal sumUptoLastMnth = 0;
                Decimal sumThisMnth = 0;
                Decimal sumUptoMnth = 0;

                if (ddlAccType.SelectedValue != "14")
                {
                    lblTotalAmt.Visible = true;
                    txtTotalFDAmt.Visible = true;
                    txtTotaluptolastmnth.Visible = true;
                    txtTotalthisMonth.Visible = true;
                    txtTotalUptoMonth.Visible = true;
                }
                else
                {
                    txtTotalInt.Visible = true;
                }

                if (ddlAccType.SelectedValue == "14")
                {
                    for (int i = 0; i < gvDetailInfoCPS.Rows.Count; ++i)
                    {

                        sumUptoMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoCPS.Rows[i].Cells[7].Text));
                    }


                    txtTotalInt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoMnth));
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    for (int i = 0; i < gvDetailInfoFDR.Rows.Count; ++i)
                    {

                        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[5].Text));
                        sumUptoLastMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[7].Text));
                        sumThisMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[8].Text));
                        sumUptoMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[9].Text));
                    }

                    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                    txtTotaluptolastmnth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoLastMnth));
                    txtTotalthisMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumThisMnth));
                    txtTotalUptoMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoMnth));
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    for (int i = 0; i < gvDetailInfo6YR.Rows.Count; ++i)
                    {

                        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[5].Text));
                        sumUptoLastMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[7].Text));
                        sumThisMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[8].Text));
                        sumUptoMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo6YR.Rows[i].Cells[9].Text));
                    }

                    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                    txtTotaluptolastmnth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoLastMnth));
                    txtTotalthisMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumThisMnth));
                    txtTotalUptoMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoMnth));
                }

                if (hdnAccTypeClass.Text == "6")
                {

                    for (int i = 0; i < gvDetailInfoLOAN.Rows.Count; ++i)
                    {

                        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoLOAN.Rows[i].Cells[5].Text));
                        sumUptoLastMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoLOAN.Rows[i].Cells[7].Text));
                        sumThisMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoLOAN.Rows[i].Cells[8].Text));
                        sumUptoMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoLOAN.Rows[i].Cells[9].Text));
                    }

                    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
                    txtTotaluptolastmnth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoLastMnth));
                    txtTotalthisMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumThisMnth));
                    txtTotalUptoMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoMnth));

                }



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetailInfoCPS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
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

        protected void gvMSPlusInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }

        }

        protected void gvDetailInfoLOAN_RowDataBound(object sender, GridViewRowEventArgs e)
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

            a = "Provision Posting Sucessfully Done";


            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //----------------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Provision Posting Sucessfully Done";


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


                if (ddlAccType.SelectedValue == "14")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateProvisionCPS", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                        gvDetailInfoCPS.Visible = true;

                        gvPreview();
                        lblTotalAmt1.Visible = true;
                        gvSumValue();
                    }
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateProvisionFDR", prm, "A2ZCSMCUS"));
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

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateProvision6YR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                        gvDetailInfo6YR.Visible = true;
                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();
                    }
                }

                if (hdnAccTypeClass.Text == "6")
                {
                    var prm1 = new object[2];
                    prm1[0] = hdnID.Text;
                    prm1[1] = ddlAccType.SelectedValue;
                                       
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateProvisionLOAN", prm1, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                        gvDetailInfoLOAN.Visible = true;
                        gvPreview();
                        lblTotalAmt.Visible = true;
                        gvSumValue();
                    }
                }

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



                var prm = new object[3];

                prm[0] = CtrlVchNo.Text;
                prm[1] = txtVchNo.Text.Trim();
                prm[2] = hdnCashCode.Text;


                if (ddlAccType.SelectedValue == "14")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateProvisionCPS", prm, "A2ZCSMCUS"));
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
                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateProvisionFDR", prm, "A2ZCSMCUS"));
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
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateProvision6YR", prm, "A2ZCSMCUS"));
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

                if (hdnAccTypeClass.Text == "6")
                {
                    var prm1 = new object[4];
                    prm1[0] = CtrlVchNo.Text;
                    prm1[1] = txtVchNo.Text.Trim();
                    prm1[2] = hdnCashCode.Text;
                    prm1[3] = ddlAccType.SelectedValue;
                    
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateProvisionLOAN", prm1, "A2ZCSMCUS"));
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
                prm[1] = "13";

                if (ddlAccType.SelectedValue == "14")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteProvisionCPS", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        DeleteMSG();
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                    }
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteProvisionFDR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        DeleteMSG();
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                    }
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteProvision6YR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        DeleteMSG();
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                    }
                }

                if (hdnAccTypeClass.Text == "6")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteProvisionLOAN", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        DeleteMSG();
                        BtnCalculate.Visible = true;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
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
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, txtToDaysDate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAccType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                if (ddlAccType.SelectedValue == "14")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionCPS");
                }
                if (ddlAccType.SelectedValue == "15")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionFDR");
                }
                if (ddlAccType.SelectedValue == "16")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvision6YR");
                }
                if (hdnAccTypeClass.Text == "6")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionLOAN");
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