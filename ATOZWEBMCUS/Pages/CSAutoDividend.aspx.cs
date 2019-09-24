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
    public partial class CSAutoDividend : System.Web.UI.Page
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
                    lblTotalProd.Visible = false;
                    lblTotalInt.Visible = false;


                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;
                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

                    lblBegYear.Text = Converter.GetString(dto.FinancialBegYear);
                    lblEndYear.Text = Converter.GetString(dto.FinancialEndYear);

                    lblCurrMth.Text = Converter.GetString(dto.CurrentMonth);


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
                gvDetailInfo1.Visible = false;

                lblTotalProd.Visible = false;
                lblTotalInt.Visible = false;

                txtTotalProd.Visible = false;
                txtTotalInt.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);


                DataTable dt1 = new DataTable();


                string qry1 = "SELECT Id,TrnDate,AccIntRate,ProcStat FROM WFCSSHAREINT where AccType ='" + ddlAccType.SelectedValue + "'";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Decimal IRate = Converter.GetDecimal(dt1.Rows[0]["AccIntRate"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);

                    txtIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", IRate));


                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;

                        txtIntRate.ReadOnly = true;

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
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeClass = 1 and AccTypeMode = 1 AND AccTypeCode =11";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 AccType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
            A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
            if (get1DTO.AccTypeCode > 0)
            {
                hdnAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);

                Int16 AType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                A2ZCSPARAMDTO get2DTO = (A2ZCSPARAMDTO.GetInformation(AType));
                if (get2DTO.AccType > 0)
                {
                    txtIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));

                }


            }

            ValidationProcess();

        }
        protected void gvPreview()
        {
            try
            {
                string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,AmtJul,AmtAug,AmtSep,AmtOct,AmtNov,AmtDec,AmtJan,AmtFeb,AmtMar,AmtApr,AmtMay,AmtJun,AmtProduct,AmtInterest FROM WFCSSHAREINT Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
                gvDetailInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo1, "A2ZCSMCUS");
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

                Decimal sumProdAmt = 0;
                Decimal sumIntAmt = 0;


                lblTotalProd.Visible = true;
                txtTotalProd.Visible = true;

                lblTotalInt.Visible = true;
                txtTotalInt.Visible = true;


                for (int i = 0; i < gvDetailInfo1.Rows.Count; ++i)
                {

                    sumProdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo1.Rows[i].Cells[12].Text));
                    sumIntAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo1.Rows[i].Cells[13].Text));
                }


                txtTotalProd.Text = Convert.ToString(String.Format("{0:0,0.00}", sumProdAmt));
                txtTotalInt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntAmt));


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
                //throw ex;
            }
        }



        protected void gvDetailInfo1_RowDataBound(object sender, GridViewRowEventArgs e)
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

            a = "Interest Posting Sucessfully Done";


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

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Interest Posting Deleted');", true);
            return;
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
                if (ddlAccType.SelectedValue == "-Select-")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Account Type');", true);
                    return;

                }



                var prm = new object[5];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = "2016-07-01";
                prm[2] = 12;
                prm[3] = txtIntRate.Text;
                prm[4] = 0;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateDividendAmount", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    gvDetailInfo1.Visible = true;
                    gvPreview();
                    gvSumValue();

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

                //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                //int GLCode = Converter.GetInteger(dto.CashCode);

                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;

                if (lblCurrMth.Text == "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "December" + " " + lblBegYear.Text;
                }
                else if (lblCurrMth.Text == "6")
                {
                    txtToDaysDate.Text = "January" + "-" + "June" + " " + lblEndYear.Text;
                }

                var prm = new object[6];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = CtrlVchNo.Text;
                prm[2] = txtVchNo.Text.Trim();
                prm[3] = hdnCashCode.Text;
                prm[4] = txtDesc.Text;
                prm[5] = hdnID.Text;




                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateDividendAmount", prm, "A2ZCSMCUS"));
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
                prm[1] = "18";


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteDividendAmount", prm, "A2ZCSMCUS"));
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
                if (lblCurrMth.Text == "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "December" + " " + lblBegYear.Text;
                }
                else if (lblCurrMth.Text == "6")
                {
                    txtToDaysDate.Text = "January" + "-" + "June" + " " + lblEndYear.Text;
                }

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                int accType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);

                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlAccType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoDividendAmount");


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