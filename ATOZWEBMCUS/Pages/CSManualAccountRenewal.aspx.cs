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
    public partial class CSManualAccountRenewal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    txtAccNo.Focus();
                    BtnCalculate.Visible = false;
                    //lblVchNo.Visible = false;
                    //txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;
                    //lblTotalAmt.Visible = false;

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;
                    txtTranDate.Text = date;

                    
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

                DataTable dt1 = new DataTable();


                string qry1 = "SELECT Id,TrnDate,ProcStat,VoucherNo FROM WFCSMANUALRENEWFDR WHERE AccNo = '" + txtAccNo.Text + "'";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");    

                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);
                    CtrlVchNo.Text = Converter.GetString(dt1.Rows[0]["VoucherNo"]);

                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        gvDetailInfoFDR.Visible = true;
                        gvPreview();
                        
                        BtnCalculate.Visible = false;
                        txtTranDate.ReadOnly = true;
                       
                        //lblVchNo.Visible = false;
                        //txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                    }
                    if (TrnDate == ProcDt && ProcStat == 2)
                    {
                        gvDetailInfoFDR.Visible = true;
                        gvPreview();
                        
                        BtnCalculate.Visible = true;
                        txtTranDate.ReadOnly = false;
                        
                        //lblVchNo.Visible = true;
                        //txtVchNo.Visible = true;
                        BtnPost.Visible = true;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                    }
                    
                    if (TrnDate != ProcDt)
                    {
                        BtnCalculate.Visible = true;
                        txtTranDate.ReadOnly = false;
                       
                        //lblVchNo.Visible = false;
                        //txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                    }
                }
                else
                {
                    BtnCalculate.Visible = true;
                    txtTranDate.ReadOnly = false;
                   
                    //lblVchNo.Visible = false;
                    //txtVchNo.Visible = false;
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
        protected void gvPreview()
        {
            try
            {


             string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,FDAmount,AccIntRate,NoDays,CalInterest FROM WFCSMANUALRENEWFDR WHERE AccNo = '" + txtAccNo.Text + "' ORDER BY CuType,CuNo";
             gvDetailInfoFDR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoFDR, "A2ZCSMCUS");
                

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

            a = "Renewal Posting Sucessfully Done";


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
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Renewal Posting Deleted');", true);
            return;
        }
        //protected void gvSumValue()
        //{
        //    try
        //    {

        //        Decimal sumfdAmt = 0;

        //        Decimal sumIntAmt = 0;


               
        //            for (int i = 0; i < gvDetailInfoFDR.Rows.Count; ++i)
        //            {

        //                sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[5].Text));

        //                sumIntAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[8].Text));

        //            }

        //            txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumfdAmt));
        //            txtTotalIntRate.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntRate));


               
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
        //        //throw ex;
        //    }
        //}

              
        protected void InvalidCalculateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher Already Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Renewaled');", true);
            return;

        }
        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccNo.Text == string.Empty)
                {
                    txtAccNo.Focus();       
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account No.');", true);
                    return;
                }
                
                    var prm = new object[4];
                    prm[0] = hdnID.Text;
                    prm[1] = CtrlAccType.Text;
                    prm[2] = Converter.GetLong(txtAccNo.Text);
                    prm[3] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);

                    int result = 0;

                    if (CtrlAccType.Text == "15")
                    {
                        result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateManualRenewalFDR", prm, "A2ZCSMCUS"));
                    }
                    else 
                    {
                        result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateManualRenewal6YR", prm, "A2ZCSMCUS"));
                    }

                    if (result == 0)
                    {
                        string qry = "SELECT Id,AccNo FROM WFCSMANUALRENEWFDR  WHERE AccNo = '" + txtAccNo.Text + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            BtnCalculate.Visible = true;
                            BtnPost.Visible = true;
                            BtnExit.Visible = true;
                            gvDetailInfoFDR.Visible = true;
                            gvPreview();
                            //lblTotalAmt.Visible = true;
                            //gvSumValue();
                        }
                        else
                        {
                            txtAccNo.Text = string.Empty;
                            txtAccNo.Focus();
                            InvalidCalculateMSG();
                        }
                    }


               
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCalculate Problem');</script>");
                //throw ex;
            }

        }

                
        protected void BtnPost_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtAccNo.Text == string.Empty)
                {
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account No.');", true);
                    return;
                }
                    

                //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                //int GLCode = Converter.GetInteger(dto.CashCode);

                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;


                var prm = new object[4];
                prm[0] = CtrlVchNo.Text;
                prm[1] = hdnCashCode.Text;
                prm[2] = CtrlAccType.Text;
                prm[3] = Converter.GetLong(txtAccNo.Text);
               

                //if (ddlAccType.SelectedValue == "15")
                //{
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateManualRenewalFDR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                      
                        BtnCalculate.Visible = false;
                        //lblVchNo.Visible = false;
                        //txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                       
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                        UpdateMSG();
                    }
                //}
               
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

                if (txtAccNo.Text == string.Empty)
                {
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account No.');", true);
                    return;
                }


                var prm = new object[2];

                prm[0] = Converter.GetLong(txtAccNo.Text);
                prm[1] = "15";

                //if (ddlAccType.SelectedValue == "15")
                //{
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteManualRenewalFDR", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                       
                        BtnCalculate.Visible = true;
                       
                        //lblVchNo.Visible = false;
                        //txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = false;
                        BtnExit.Visible = true;
                        gvDetailInfoFDR.Visible = false;
                       
                        UpdateBackUpStat();
                        DeleteMSG();
                    }
                //}
                
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


        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {

            if (txtAccNo.Text != string.Empty)
            {
                GetAccInfo();
            }
        }

        protected void InvalidAccountNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account Does Not Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Does Not Exist');", true);

            return;

        }

        public void GetAccInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccountNoMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    lblAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);

                    if (lblAtyClass.Text != "2")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                        return;
                    }


                    CtrlAccType.Text = txtAccNo.Text.Substring(0, 2);

                    ValidationProcess();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }


        private void InvalidDateMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;
        }

        private void InvalidInputDate()
        {
           
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Input Date');", true);
            return;
        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtTranDate.Text.Length.ToString();
            if (date != "10")
            {
                InvalidInputDate();
                txtTranDate.Text = lblPdate.Text;
                txtTranDate.Focus();
                return;
            }


            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Month1 = opdate1.Month;
            int Month2 = opdate2.Month;

            if (opdate1 > opdate2 || Month1 != Month2)
            {
                InvalidDateMSG();
                txtTranDate.Text = lblPdate.Text;
                txtTranDate.Focus();
                return;
            }


        }

    }
}