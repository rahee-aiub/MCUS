using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ATOZWEBMCUS.Pages
{
    public partial class GLDailyBoothTransaction : System.Web.UI.Page
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
                    hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    SumValue();
                    CtrlPrmValue.Text = Request.QueryString["a%b"];
                    string b = CtrlPrmValue.Text;
                    CtrlTrnType.Text = b.Substring(0, 1);
                    CtrlTrnMode.Text = b.Substring(1, 1);
                    CtrlModule.Text = b.Substring(2, 1);

                    FunctionName();
                    BtnUpdate.Visible = false;


                    if (CtrlTrnType.Text == "1" )
                    {
                        lblContra.Visible = false;
                        txtContra.Visible = false;
                        ddlContra.Visible = false;
                        lblMode.Visible = false;
                        ddlTrnMode.Visible = false;
                    }


                    if (CtrlTrnMode.Text == "0")
                    {
                        lblDebit.Visible = false;
                        txtDrAmount.Visible = false;
                    }

                    if (CtrlTrnMode.Text == "1")
                    {
                        lblCredit.Visible = false;
                        txtCrAmount.Visible = false;
                    }

                    if (CtrlTrnType.Text == "1")
                    {
                        txtTrnsactionCode.Focus();
                    }
                    else 
                    {
                        DisableInfo();
                        ddlTrnMode.Focus();
                    }


                    string sqlQueryAcc = "SELECT GLAccNo,GLAccDesc FROM dbo.A2ZCGLMST WHERE GLPrtPos = 6";
                    ddlTrnsactionCode = CommonManager.Instance.FillDropDownList(sqlQueryAcc, ddlTrnsactionCode, "A2ZGLMCUS");

                    string sqlQueryContra = "SELECT GLAccNo,GLAccDesc FROM dbo.A2ZCGLMST WHERE GLPrtPos = 6";
                    ddlContra = CommonManager.Instance.FillDropDownList(sqlQueryContra, ddlContra, "A2ZGLMCUS");

                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtVchDate.Text = date;
                    CtrlProcDate.Text = date;

                    string strQuery1 = @"DELETE FROM dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Value + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void FunctionName()
        {
            if (CtrlTrnMode.Text == "0")
            {
                lblTransFunction.Text = "GL Daily Cash In Transaction";
            }
            if (CtrlTrnMode.Text == "1")
            {
                lblTransFunction.Text = "GL Daily Cash Out Transaction";
            }
            if (CtrlTrnMode.Text == "2")
            {
                lblTransFunction.Text = "GL Daily Transfer Transaction";
            }
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }



        //protected void txtDrAmount_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtDrAmount.Text);
        //    txtDrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //}

        //protected void txtCrAmount_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtCrAmount.Text);
        //    txtCrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //}

        protected void TotalAmount()
        {
            CtrlTotAmount.Text = string.Empty;

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(Abs(GLAmount)) AS 'Amount' FROM WFGLTrannsaction WHERE TrnFlag=0 AND UserID='" + hdnID.Value + "'", "A2ZGLMCUS");


            if (dt.Rows.Count > 0)
            {
                CtrlTotAmount.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["Amount"]));
            }

        }

        
        protected void TrnLimitValidity()
        {
            double TotalAmt = 0;
            CtrlProcStat.Text = "0";

            TotalAmount();

            TotalAmt = Converter.GetDouble(CtrlTotAmount.Text);

            //if (CtrlTrnMode.Text == "0" || CtrlTrnMode.Text == "1")
            //{
                lblTrnMode.Text = CtrlTrnMode.Text;
            //}

            //if (CtrlTrnMode.Text == "2")
            //{
            //    if (txtCrAmount.Text == "")
            //    {
            //        lblTrnMode.Text = "1";
            //    }
            //    else
            //    {
            //        lblTrnMode.Text = "0";
            //    }

            //}  

            int Ids = Converter.GetInteger(hdnID.Value);
            A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

            if (getDTO.IdsNo > 0)
            {
                if (lblTrnMode.Text == "0" && CtrlTrnType.Text == "1")
                {
                    if (TotalAmt > getDTO.LIdsCashCredit)
                    {
                        CtrlProcStat.Text = "1";
                    }
                    else
                    {
                        CtrlProcStat.Text = "0";
                    }
                }

                if (lblTrnMode.Text == "1" && CtrlTrnType.Text == "1")
                {
                    if (TotalAmt > getDTO.LIdsCashDebit)
                    {
                        CtrlProcStat.Text = "1";
                    }
                    else
                    {
                        CtrlProcStat.Text = "0";
                    }
                }

                if (lblTrnMode.Text == "0" && CtrlTrnType.Text == "3")
                {
                    if (TotalAmt > getDTO.LIdsTrfDebit)
                    {
                        CtrlProcStat.Text = "1";
                    }
                    else
                    {
                        CtrlProcStat.Text = "0";
                    }
                }

                if (lblTrnMode.Text == "1" && CtrlTrnType.Text == "3")
                {
                    if (TotalAmt > getDTO.LIdsTrfCredit)
                    {
                        CtrlProcStat.Text = "1";
                    }
                    else
                    {
                        CtrlProcStat.Text = "0";
                    }
                }

            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
            int GLCode = Converter.GetInteger(hdnCashCode.Text);
            Int16 RecType = Converter.GetSmallInteger(2);
            A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
            CtrlVchNo.Text = "G" + GLCode + "-" + getDTO.RecLastNo;

            TrnLimitValidity();

            var prm = new object[3];
            prm[0] = hdnID.Value;
            prm[1] = CtrlVchNo.Text;
            prm[2] = CtrlProcStat.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlUpdateTransaction", prm, "A2ZGLMCUS"));
            if (result == 0)
            {

                SumValue();
                gvDebit();
                
                clearInfo();

                if (CtrlTrnType.Text == "1")
                {
                    txtTrnsactionCode.Focus();
                }
                else
                {
                    DisableInfo();
                    ddlTrnMode.Focus();
                }

                UpdatedMSG();

                BtnUpdate.Visible = false;

                txtVchDate.Text = CtrlProcDate.Text;
                
                //string strQuery1 = "DELETE FROM WFGLTrannsaction" WHERE UserID='" + hdnID.Value + "'";

                string strQuery1 = @"DELETE dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Value + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

            }
        }
        protected void UpdatedMSG()
        {
            string a = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (CtrlProcStat.Text == "0")
            {
                a = "  GL Transaction Sucessfully Done";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "   GL Transaction Input Done";
            }
            string b = "Generated New Voucher No.";
            string c = string.Format(CtrlVchNo.Text);

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(a);
            sb.Append("\\n");
            sb.Append("\\n");
            sb.Append(b);
            sb.Append(c);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }


        protected void gvDebit()
        {
            string sqlquery3 = "SELECT Id, GLAccNo,Abs(GLAmount) as GLAmount,TrnDesc FROM WFGLTrannsaction WHERE TrnFlag=0";

            gvDebitInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDebitInfo, "A2ZGLMCUS");
        }
        

        protected void ddlTrnsactionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrnsactionCode.SelectedValue == "-Select-")
            {
                txtTrnsactionCode.Text = string.Empty;
                return;
            }
            try
            {
                txtTrnsactionCode.Text = Converter.GetString(ddlTrnsactionCode.SelectedValue);

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnsactionCode.Text));
                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    CtrlAccType.Text = Converter.GetString(glObj.GLAccType);
                    txtDescription.Focus();

                    if (CtrlRecType.Text !="2")
                    {
                        InvalidGlCode();
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                    }
                    
                }
                else 
                {
                    Validity();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtTrnsactionCode_TextChanged(object sender, EventArgs e)
        {

            try
            {
                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnsactionCode.Text));

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                    }
                    else
                    {
                        txtTrnsactionCode.Text = Converter.GetString(glObj.GLAccNo);
                        CtrlAccType.Text = Converter.GetString(glObj.GLAccType);

                        ddlTrnsactionCode.SelectedValue = Converter.GetString(glObj.GLAccNo);

                        ddlTrnsactionCode_SelectedIndexChanged(this, EventArgs.Empty);
                        txtDescription.Focus();
                    }
                }
                else
                {
                    Validity();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Validity()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('GL Code - Does Not Exists  ');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        private void InvalidGlCode()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Not Trans. Header Record');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }
        protected void ddlContra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContra.SelectedValue == "-Select-")
            {
                //ClearInfoAdd();
                txtContra.Text = string.Empty;
                return;
            }

            //refresh();
            try
            {
                txtContra.Text = Converter.GetString(ddlContra.SelectedValue);

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtContra.Text));
                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    CtrlContraAType.Text = Converter.GetString(glObj.GLAccType);
                    txtTrnsactionCode.Focus();
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtContra.Text = string.Empty;
                        txtContra.Focus();
                    }
                }
                else
                {
                    Validity();
                    txtContra.Text = string.Empty;
                    txtContra.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtContra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtContra.Text));

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        txtContra.Text = string.Empty;
                        txtContra.Focus();
                    }
                    else
                    {
                        txtContra.Text = Converter.GetString(glObj.GLAccNo);
                        CtrlContraAType.Text = Converter.GetString(glObj.GLAccType);

                        ddlContra.SelectedValue = Converter.GetString(glObj.GLAccNo);
                        ddlContra_SelectedIndexChanged(this, EventArgs.Empty);
                        txtTrnsactionCode.Focus();
                    }
                }
                else
                {

                    Validity();
                    txtContra.Text = string.Empty;
                    txtContra.Focus();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        protected void clearInfo()
        {

            txtDrAmount.Text = string.Empty;
            txtCrAmount.Text = string.Empty;
            //txtVchDate.Text = string.Empty;
            

            lblContra.Text = "Contra Code";
            ddlContra.SelectedValue = "-Select-";
            txtContra.Enabled = true;
            txtContra.Text = string.Empty;
            ddlContra.Enabled = true;
            gvDebitInfo.Visible = false;
            if(CtrlTrnType.Text == "3")
            {
                ddlTrnMode.SelectedIndex = 0;
            }
            
            

        }

        protected void SumValue()
        {
            txtTotCredit.Text = string.Empty;
            txtTotDebit.Text = string.Empty;

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLDebitAmt) AS 'AmountDr' FROM A2ZTRANSACTION WHERE FromCashCode ='" + hdnCashCode.Text +"'", "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                txtTotDebit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountDr"]));
            }
            
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLCreditAmt) AS 'AmountCr' FROM A2ZTRANSACTION WHERE FromCashCode ='" + hdnCashCode.Text +"'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                txtTotCredit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountCr"]));
            }            
        }

       

        protected void DuplicateVchMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Voucher Already Exist');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

         protected void InvalidGLCodeMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Transaction Code');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
       
        protected void InvalidGLContraMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Contra Code');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void InvalidGLDescMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Voucher Description');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void InvalidGLAmtMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Voucher Amount');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void VerifyContraCode()
        {
            try
            {
                CtrlMsgFlag.Text = "0";

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtContra.Text));

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        CtrlMsgFlag.Text = "1";
                        return;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Validity();
                    CtrlMsgFlag.Text = "1";
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        protected void VerifyTransactionCode()
        {
            try
            {
                CtrlMsgFlag.Text = "0";
                
                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnsactionCode.Text));

                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    if (CtrlRecType.Text != "2")
                    {
                        InvalidGlCode();
                        CtrlMsgFlag.Text = "1";
                        return;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    Validity();
                    CtrlMsgFlag.Text = "1";
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtContra.Text == string.Empty && CtrlTrnType.Text != "1")
            {
                InvalidGLContraMSG();
                return;
            }
            if (txtTrnsactionCode.Text == string.Empty)
            {
                InvalidGLCodeMSG();
                return;
            }
               
            if (txtDescription.Text == string.Empty)
            {
                InvalidGLDescMSG();
                return;
            }

            VerifyTransactionCode();
            if(CtrlMsgFlag.Text == "1")
            {
                txtTrnsactionCode.Text = string.Empty;
                txtTrnsactionCode.Focus();
                return;
            }

            VerifyContraCode();
            if (CtrlMsgFlag.Text == "1")
            {
                txtContra.Text = string.Empty;
                txtContra.Focus();
                return;
            }

            if ((txtDrAmount.Text == "00.00" || txtDrAmount.Text == string.Empty) && (txtCrAmount.Text == "00.00" || txtCrAmount.Text == string.Empty)) 
            {
                InvalidGLAmtMSG();
                return;
            }
               
            gvDebitInfo.Visible = true;

            BtnUpdate.Visible = true;
            
            hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
            hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

            if (CtrlTrnMode.Text == "0" || CtrlTrnMode.Text == "1")
            {
                lblTrnMode.Text = CtrlTrnMode.Text;
            }

            if (CtrlTrnMode.Text == "2")
            {
                if (txtCrAmount.Text == "")
                {
                    lblTrnMode.Text = "1";
                }
                else 
                {
                    lblTrnMode.Text = "0";
                }

            }

            if (CtrlTrnType.Text == "1")
            {
                txtContra.Text = hdnCashCode.Text;
                CtrlContraAType.Text = "1";
            }

            if (lblTrnMode.Text == "0")
            {
                
                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[19];

                prm[0] = opdate;
                prm[1] = 0;
                prm[2] = 0;
                prm[3] = txtTrnsactionCode.Text;
                prm[4] = 0;
                prm[5] = Converter.GetDouble(txtCrAmount.Text); 
                prm[6] = txtDescription.Text;
                prm[7] = 1;
                prm[8] = CtrlTrnType.Text;
                prm[9] = 1;
                prm[10] = txtContra.Text;
                prm[11] = txtTrnsactionCode.Text;
                prm[12] = 0;

                if (CtrlAccType.Text == "1" || CtrlAccType.Text == "5")
                {
                    double Amount = Converter.GetDouble(txtCrAmount.Text);
                    prm[13] = Converter.GetString(0 - Amount);
                }
                else 
                {
                    prm[13] = Converter.GetDouble(txtCrAmount.Text);
                }

                prm[14] = 1;
                prm[15] = hdnCashCode.Text;

                prm[16] = CtrlModule.Text;
                prm[17] = hdnID.Value;
                prm[18] = valuedate;
                                
                int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm2 = new object[19];

                prm2[0] = opdate1;
                prm2[1] = 0;
                prm2[2] = 0;
                prm2[3] = txtContra.Text;
                prm2[4] = Converter.GetDouble(txtCrAmount.Text);
                prm2[5] = 0;
                prm2[6] = txtDescription.Text;
                prm2[7] = 0;
                prm2[8] = CtrlTrnType.Text;
                prm2[9] = 0;
                prm2[10] = txtContra.Text;
                prm2[11] = txtTrnsactionCode.Text;
                prm2[12] = 1;

                if (CtrlContraAType.Text == "2" || CtrlContraAType.Text == "4")
                {
                    double Amount = Converter.GetDouble(txtCrAmount.Text);
                    prm2[13] = Converter.GetString(0 - Amount);
                }
                else
                {
                    prm2[13] = Converter.GetDouble(txtCrAmount.Text);
                }

                prm2[14] = 1;
                prm2[15] = hdnCashCode.Text;
                prm2[16] = CtrlModule.Text;
                prm2[17] = hdnID.Value;
                prm2[18] = valuedate1;
                

                int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm2, "A2ZGLMCUS"));

                if (result2 == 0)
                {
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                    ddlTrnsactionCode.SelectedValue = "-Select-";
                    txtDescription.Text = string.Empty;
                    txtDrAmount.Text = string.Empty;
                    txtCrAmount.Text = string.Empty;
                    gvDebit();
                    SumValue();
                    
                }

            }

            if (lblTrnMode.Text == "1")
            {

                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                var prm = new object[19];

                prm[0] = opdate;
                prm[1] = 0;
                prm[2] = 0;
                prm[3] = txtTrnsactionCode.Text;
                prm[4] = Converter.GetDouble(txtDrAmount.Text); 
                prm[5] = 0;
                prm[6] = txtDescription.Text;
                prm[7] = 0;
                prm[8] = CtrlTrnType.Text;
                prm[9] = 0;
                prm[10] = txtTrnsactionCode.Text;
                prm[11] = txtContra.Text;
                prm[12] = 0;

                if (CtrlAccType.Text == "2" || CtrlAccType.Text == "4")
                {
                    double Amount = Converter.GetDouble(txtDrAmount.Text);
                    prm[13] = Converter.GetString(0 - Amount);
                }
                else
                {
                    prm[13] = Converter.GetDouble(txtDrAmount.Text);
                }

                
                prm[14] = 1;

                prm[15] = hdnCashCode.Text;
                prm[16] = CtrlModule.Text;
                prm[17] = hdnID.Value;
                prm[18] = valuedate;
                

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                DateTime opdate3 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime valuedate3 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm3 = new object[19];

                prm3[0] = opdate3;
                prm3[1] = 0;
                prm3[2] = 0;
                prm3[3] = txtContra.Text;
                prm3[4] = 0;
                prm3[5] = Converter.GetDouble(txtDrAmount.Text);
                prm3[6] = txtDescription.Text;
                prm3[7] = 1;
                prm3[8] = CtrlTrnType.Text;
                prm3[9] = 1;
                prm3[10] = txtTrnsactionCode.Text;
                prm3[11] = txtContra.Text;
                prm3[12] = 1;

                if (CtrlContraAType.Text == "1" || CtrlContraAType.Text == "5")
                {
                    double Amount = Converter.GetDouble(txtDrAmount.Text);
                    prm3[13] = Converter.GetString(0 - Amount);
                }
                else
                {
                    prm3[13] = Converter.GetDouble(txtDrAmount.Text);
                }

                
                prm3[14] = 1;

                prm3[15] = hdnCashCode.Text;
                prm3[16] = CtrlModule.Text;
                prm3[17] = hdnID.Value;
                prm3[18] = valuedate3;
                

                int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm3, "A2ZGLMCUS"));

                if (result3 == 0)
                {
                    //ddlContra.Enabled = false;
                    //txtContra.Enabled = false;
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                    ddlTrnsactionCode.SelectedValue = "-Select-";
                    txtDescription.Text = string.Empty;
                    txtDrAmount.Text = string.Empty;
                    txtCrAmount.Text = string.Empty;
                    gvDebit();
                    SumValue();
                    txtCrAmount.ReadOnly = false;
                    txtDrAmount.ReadOnly = false;
                }
            }
        }

        protected void gvDebitInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        

        protected void gvDebitInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblId = (Label)gvDebitInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");

            int ID = Converter.GetInteger(lblId.Text);
            int idincrement = ID + 1;
            string strQuery1 = "DELETE FROM WFGLTrannsaction WHERE Id between '" + ID + "' and '" + idincrement + "'";
            int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));
            //string strQuery2 = "DELETE FROM WFGLTrannsaction WHERE TrnFlag=1";
            //int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));

            gvDebit();
            SumValue();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            
            Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('GLDailyReverseTransaction.aspx','_newtab');", true);
        }

        

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (CtrlTrnType.Text == "1")
            {
                if (CtrlTrnMode.Text == "0")
                {
                    txtCrAmount.Focus();
                }
                else
                {
                    txtDrAmount.Focus();
                }
            }
            else 
            {
                if (CtrlTrnMode.Text == "1")
                {
                    txtCrAmount.Focus();
                }
                else
                {
                    txtDrAmount.Focus();
                }
            }
        }

        protected void txtDrAmount_TextChanged(object sender, EventArgs e)
        {
            //if (txtDrAmount.Text == "0.00" || txtDrAmount.Text == ".00")
            //{
            //    txtCrAmount.ReadOnly = false;
            //    txtDrAmount.ReadOnly = true;
            //}
            //else 
            //{
            //    txtDrAmount.ReadOnly = false;
            //    txtCrAmount.ReadOnly = true;
            //}
            double ValueConvert = Converter.GetDouble(txtDrAmount.Text);
            txtDrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnAdd.Focus();
            
        }

        protected void txtCrAmount_TextChanged(object sender, EventArgs e)
        {
            //if (txtCrAmount.Text == "0.00" || txtCrAmount.Text == ".00")
            //{
            //    txtDrAmount.ReadOnly = false;
            //    txtCrAmount.ReadOnly = true;
            //}
            //else
            //{
            //    txtDrAmount.ReadOnly = true;
            //    txtCrAmount.ReadOnly = false;
            //}
            
            double ValueConvert = Converter.GetDouble(txtCrAmount.Text);
            txtCrAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnAdd.Focus();
        }

        protected void ddlTrnMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            EnableInfo();
            
            if (ddlTrnMode.SelectedValue == "1")
            {
                lblContra.Text = "Credit Code :";
            }
            else 
            {
                lblContra.Text = "Debit Code :";   
            }
        }


        protected void DisableInfo()
        {
            lblContra.Visible = false;
            txtContra.Visible = false;
            ddlContra.Visible = false;
            lblAccount.Visible = false;
            txtTrnsactionCode.Visible = false;
            ddlTrnsactionCode.Visible = false;
            lblDescription.Visible = false;
            txtDescription.Visible = false;
            lblDebit.Visible = false;
            txtDrAmount.Visible = false;
            lblCredit.Visible = false;
            txtCrAmount.Visible = false;
            BtnAdd.Visible = false;

        }

        protected void EnableInfo()
        {
            lblContra.Visible = true;
            txtContra.Visible = true;
            ddlContra.Visible = true;
            lblAccount.Visible = true;
            txtTrnsactionCode.Visible = true;
            ddlTrnsactionCode.Visible = true;
            lblDescription.Visible = true;
            txtDescription.Visible = true;
            BtnAdd.Visible = true;

            if (ddlTrnMode.SelectedValue == "1")
            {
                lblDebit.Visible = true;
                txtDrAmount.Visible = true;
                lblCredit.Visible = false;
                txtCrAmount.Visible = false;
                //CtrlTrnMode.Text = "0";
            }
            else
            {
                lblDebit.Visible = false;
                txtDrAmount.Visible = false;
                lblCredit.Visible = true;
                txtCrAmount.Visible = true;
                //CtrlTrnMode.Text = "1";
            }

        }

        private void InvalidDateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Future Date');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void txtVchDate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtVchDate.Text = CtrlProcDate.Text;
                txtVchDate.Focus();
            }

            if (opdate1 < opdate2)
            {
                lblVchDate.Text = "Back Value Date:";
            }
            else
            {
                lblVchDate.Text = "Voucher Date:";
            }


        }

        
    }
}