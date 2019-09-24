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
    public partial class GLDailyTransaction : System.Web.UI.Page
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
                    hdnCashCode.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    
                    CtrlPrmValue.Text = Request.QueryString["a%b"];
                    string b = CtrlPrmValue.Text;
                    CtrlTrnType.Text = b.Substring(0, 1);
                    CtrlTrnMode.Text = b.Substring(1, 1);
                    CtrlModule.Text = b.Substring(2, 1);

                    
                    FunctionName();
                    BtnUpdate.Visible = false;


                    if (CtrlTrnType.Text == "1")
                    {
                        lblContra.Visible = false;
                        txtContra.Visible = false;
                        ddlContra.Visible = false;

                    }

                    if (CtrlTrnType.Text == "1")
                    {
                        txtVchNo.Focus();
                    }
                    else
                    {
                        lblContra.Text = "Debit Code :";
                        lblAccount.Text = "Credit Code :";
                        txtVchNo.Focus();
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
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
            try
            {

                double TotalAmt = 0;
                CtrlProcStat.Text = "0";

                TotalAmount();

                TotalAmt = Converter.GetDouble(CtrlTotAmount.Text);

                if (CtrlTrnMode.Text == "0" || CtrlTrnMode.Text == "1")
                {
                    lblTrnMode.Text = CtrlTrnMode.Text;
                }

                if (CtrlTrnMode.Text == "2")
                {
                    lblTrnMode.Text = "1";
                }



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
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnLimitValidity Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            try
            {

                hdnCashCode.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                int GLCode = Converter.GetInteger(hdnCashCode.Value);
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

                    UnPostValue();
                    gvDebit();

                    clearInfo();

                    if (CtrlTrnType.Text == "1")
                    {
                        txtVchNo.Focus();
                    }
                    else
                    {
                        lblContra.Text = "Debit Code :";
                        lblAccount.Text = "Credit Code :";
                        txtVchNo.Focus();
                    }

                    UpdatedMSG();

                    BtnUpdate.Visible = false;

                    txtContra.ReadOnly = false;
                    txtTrnsactionCode.ReadOnly = false;

                    txtVchDate.Text = CtrlProcDate.Text;

                    //string strQuery1 = "DELETE FROM WFGLTrannsaction" WHERE UserID='" + hdnID.Value + "'";

                    string strQuery1 = @"DELETE dbo.WFGLTrannsaction WHERE UserID='" + hdnID.Value + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
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
            try
            {

                if (ddlTrnsactionCode.SelectedValue == "-Select-")
                {
                    txtTrnsactionCode.Text = string.Empty;
                    return;
                }

                txtTrnsactionCode.Text = Converter.GetString(ddlTrnsactionCode.SelectedValue);

                if (txtTrnsactionCode.Text == txtContra.Text)
                {
                    DuplicateGLCode();
                    txtTrnsactionCode.Text = string.Empty;
                    ddlTrnsactionCode.SelectedIndex = 0;
                    txtTrnsactionCode.Focus();
                    return;
                }


                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnsactionCode.Text));
                if (glObj.GLAccNo > 0)
                {
                    CtrlRecType.Text = Converter.GetString(glObj.GLRecType);
                    CtrlAccType.Text = Converter.GetString(glObj.GLAccType);
                    txtDescription.Focus();

                    if (CtrlRecType.Text != "2")
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlTrnsactionCode_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtTrnsactionCode_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtTrnsactionCode.Text == txtContra.Text)
                {
                    DuplicateGLCode();
                    txtTrnsactionCode.Text = string.Empty;
                    txtTrnsactionCode.Focus();
                    return;
                }


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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnsactionCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        private void DuplicateGLCode()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Duplicate GL Code');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
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
            try
            {

                if (ddlContra.SelectedValue == "-Select-")
                {
                    //ClearInfoAdd();
                    txtContra.Text = string.Empty;
                    return;
                }

                //refresh();

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlContra_SelectedIndexChanged Problem');</script>");
                //throw ex;
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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtContra_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void clearInfo()
        {

            txtAmount.Text = string.Empty;
            txtVchNo.Text = string.Empty;
            //txtVchDate.Text = string.Empty;

            txtTrnsactionCode.Text = string.Empty;
            ddlTrnsactionCode.SelectedValue = "-Select-";
            txtDescription.Text = string.Empty;

            ddlContra.SelectedValue = "-Select-";
            txtContra.Enabled = true;
            txtContra.Text = string.Empty;
            ddlContra.Enabled = true;
            gvDebitInfo.Visible = false;
            if (CtrlTrnType.Text == "3")
            {
                //ddlTrnMode.SelectedIndex = 0;
            }



        }
     
        protected void UnPostValue()
        {
            txtHoldCredit.Text = string.Empty;
            txtHoldDebit.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLDebitAmt) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            
            if (dt.Rows.Count > 0)
            {
                txtHoldDebit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountDr"]));
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(GLCreditAmt) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 1 AND TrnFlag = 0 AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                txtHoldCredit.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountCr"]));
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

        protected void InvalidVchNoMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Voucher No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtVchNo.Text == string.Empty)
                {
                    InvalidVchNoMSG();
                    return;
                }
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

                if ((txtAmount.Text == "00.00" || txtAmount.Text == string.Empty))
                {
                    InvalidGLAmtMSG();
                    return;
                }

                gvDebitInfo.Visible = true;

                BtnUpdate.Visible = true;

                hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnCashCode.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                lblTrnMode.Text = CtrlTrnMode.Text;


                if (CtrlTrnType.Text == "1")
                {
                    txtContra.Text = hdnCashCode.Value;
                    CtrlContraAType.Text = "1";
                }

                if (lblTrnMode.Text == "2")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm = new object[20];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtContra.Text;
                    prm[5] = Converter.GetDouble(txtAmount.Text);
                    prm[6] = 0;
                    prm[7] = txtDescription.Text;
                    prm[8] = 1;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 0;
                    prm[11] = txtContra.Text;
                    prm[12] = txtTrnsactionCode.Text;
                    prm[13] = 0;

                    if (CtrlAccType.Text == "2" || CtrlAccType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm[15] = 1;
                    prm[16] = hdnCashCode.Value;

                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Value;
                    prm[19] = valuedate;

                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm2 = new object[20];

                    prm2[0] = opdate1;
                    prm2[1] = 0;
                    prm2[2] = txtVchNo.Text;
                    prm2[3] = 0;
                    prm2[4] = txtTrnsactionCode.Text;
                    prm2[5] = 0;
                    prm2[6] = Converter.GetDouble(txtAmount.Text);
                    prm2[7] = txtDescription.Text;
                    prm2[8] = 0;
                    prm2[9] = CtrlTrnType.Text;
                    prm2[10] = 1;
                    prm2[11] = txtContra.Text;
                    prm2[12] = txtTrnsactionCode.Text;
                    prm2[13] = 0;

                    if (CtrlContraAType.Text == "1" || CtrlContraAType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm2[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm2[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm2[15] = 1;
                    prm2[16] = hdnCashCode.Value;
                    prm2[17] = CtrlModule.Text;
                    prm2[18] = hdnID.Value;
                    prm2[19] = valuedate1;


                    int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm2, "A2ZGLMCUS"));

                    if (result2 == 0)
                    {
                        //txtTrnsactionCode.Text = string.Empty;
                        //txtTrnsactionCode.Focus();
                        //ddlTrnsactionCode.SelectedValue = "-Select-";
                        //txtDescription.Text = string.Empty;
                        //txtAmount.Text = string.Empty;

                        txtContra.ReadOnly = true;
                        txtTrnsactionCode.ReadOnly = true;

                        gvDebit();
                        UnPostValue();

                    }

                }




                if (lblTrnMode.Text == "0")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm = new object[20];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtTrnsactionCode.Text;
                    prm[5] = 0;
                    prm[6] = Converter.GetDouble(txtAmount.Text);
                    prm[7] = txtDescription.Text;
                    prm[8] = 1;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 1;
                    prm[11] = txtContra.Text;
                    prm[12] = txtTrnsactionCode.Text;
                    prm[13] = 0;

                    if (CtrlAccType.Text == "1" || CtrlAccType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm[15] = 1;
                    prm[16] = hdnCashCode.Value;

                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Value;
                    prm[19] = valuedate;

                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate1 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm2 = new object[20];

                    prm2[0] = opdate1;
                    prm2[1] = 0;
                    prm2[2] = txtVchNo.Text;
                    prm2[3] = 0;
                    prm2[4] = txtContra.Text;
                    prm2[5] = Converter.GetDouble(txtAmount.Text);
                    prm2[6] = 0;
                    prm2[7] = txtDescription.Text;
                    prm2[8] = 0;
                    prm2[9] = CtrlTrnType.Text;
                    prm2[10] = 0;
                    prm2[11] = txtContra.Text;
                    prm2[12] = txtTrnsactionCode.Text;
                    prm2[13] = 1;

                    if (CtrlContraAType.Text == "2" || CtrlContraAType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm2[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm2[14] = Converter.GetDouble(txtAmount.Text);
                    }

                    prm2[15] = 1;
                    prm2[16] = hdnCashCode.Value;
                    prm2[17] = CtrlModule.Text;
                    prm2[18] = hdnID.Value;
                    prm2[19] = valuedate1;


                    int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm2, "A2ZGLMCUS"));

                    if (result2 == 0)
                    {
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                        ddlTrnsactionCode.SelectedValue = "-Select-";
                        txtDescription.Text = string.Empty;
                        txtAmount.Text = string.Empty;

                        gvDebit();
                        UnPostValue();

                    }

                }

                if (lblTrnMode.Text == "1")
                {

                    DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                    var prm = new object[20];

                    prm[0] = opdate;
                    prm[1] = 0;
                    prm[2] = txtVchNo.Text;
                    prm[3] = 0;
                    prm[4] = txtTrnsactionCode.Text;
                    prm[5] = Converter.GetDouble(txtAmount.Text);
                    prm[6] = 0;
                    prm[7] = txtDescription.Text;
                    prm[8] = 0;
                    prm[9] = CtrlTrnType.Text;
                    prm[10] = 0;
                    prm[11] = txtTrnsactionCode.Text;
                    prm[12] = txtContra.Text;
                    prm[13] = 0;

                    if (CtrlAccType.Text == "2" || CtrlAccType.Text == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm[14] = Converter.GetDouble(txtAmount.Text);
                    }


                    prm[15] = 1;

                    prm[16] = hdnCashCode.Value;
                    prm[17] = CtrlModule.Text;
                    prm[18] = hdnID.Value;
                    prm[19] = valuedate;


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm, "A2ZGLMCUS"));

                    DateTime opdate3 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime valuedate3 = DateTime.ParseExact(txtVchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    var prm3 = new object[20];

                    prm3[0] = opdate3;
                    prm3[1] = 0;
                    prm3[2] = txtVchNo.Text;
                    prm3[3] = 0;
                    prm3[4] = txtContra.Text;
                    prm3[5] = 0;
                    prm3[6] = Converter.GetDouble(txtAmount.Text);
                    prm3[7] = txtDescription.Text;
                    prm3[8] = 1;
                    prm3[9] = CtrlTrnType.Text;
                    prm3[10] = 1;
                    prm3[11] = txtTrnsactionCode.Text;
                    prm3[12] = txtContra.Text;
                    prm3[13] = 1;

                    if (CtrlContraAType.Text == "1" || CtrlContraAType.Text == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount.Text);
                        prm3[14] = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        prm3[14] = Converter.GetDouble(txtAmount.Text);
                    }


                    prm3[15] = 1;

                    prm3[16] = hdnCashCode.Value;
                    prm3[17] = CtrlModule.Text;
                    prm3[18] = hdnID.Value;
                    prm3[19] = valuedate3;


                    int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLAddTransaction", prm3, "A2ZGLMCUS"));

                    if (result3 == 0)
                    {
                        //ddlContra.Enabled = false;
                        //txtContra.Enabled = false;
                        txtTrnsactionCode.Text = string.Empty;
                        txtTrnsactionCode.Focus();
                        ddlTrnsactionCode.SelectedValue = "-Select-";
                        txtDescription.Text = string.Empty;
                        txtAmount.Text = string.Empty;

                        gvDebit();
                        UnPostValue();
                        txtAmount.ReadOnly = false;

                    }
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnAdd_Click Problem');</script>");
                //throw ex;
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
            try
            {

                Label lblId = (Label)gvDebitInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");

                int ID = Converter.GetInteger(lblId.Text);
                int idincrement = ID + 1;
                string strQuery1 = "DELETE FROM WFGLTrannsaction WHERE Id between '" + ID + "' and '" + idincrement + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));
                //string strQuery2 = "DELETE FROM WFGLTrannsaction WHERE TrnFlag=1";
                //int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));

                gvDebit();
                UnPostValue();
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDebitInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            try
            {
               Page.ClientScript.RegisterStartupScript(
               this.GetType(), "OpenWindow", "window.open('GLDailyReverseTransaction.aspx','_newtab');", true);
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnDelete_Click Problem');</script>");
                //throw ex;
            }
        }



        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            BtnAdd.Focus();
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

            UnPostValue();
        }




    }
}