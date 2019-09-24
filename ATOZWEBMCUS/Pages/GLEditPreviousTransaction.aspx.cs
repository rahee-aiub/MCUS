using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.GeneralLedger;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLEditPreviousTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPrmValue.Text = Request.QueryString["a%b"];
                string b = hdnPrmValue.Text;
                HdnModule.Text = b;
                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlTrnDate.Text = date;
                txtTranDate.Text = date;

                txtVoucherNo.Focus();
                BtnUpdate.Visible = false;
                BtnCancel.Visible = false;

                //txtTranDate.Focus();

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                gvDetailInfo.Visible = true;
                var prm = new object[4];

                prm[0] = txtVoucherNo.Text;
                prm[1] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);
                prm[2] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);
                prm[3] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateGetTransactionData", prm, "A2ZGLMCUS"));
                if (result == 0)
                {
                    string qry = "SELECT Id,AccNo FROM WFEDITA2ZTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        txtVoucherNo.ReadOnly = true;
                        gvDetailInfo.Visible = true;
                        BtnUpdate.Visible = true;
                        BtnCancel.Visible = true;
                        gvPreview();
                    }
                    else
                    {
                        VoucherMSG();
                        BtnUpdate.Visible = false;
                        BtnCancel.Visible = false;
                        txtVoucherNo.Text = string.Empty;
                        txtVoucherNo.Focus();
                    }
                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSearch_Click Problem');</script>");
                //throw ex;
            }
        }

        private void gvPreview()
        {
            try
            {
                string Qry = "SELECT Id,TrnDate,TrnGLAccNoDr,GLAccDrDesc,TrnGLAccNoCr,GLAccCrDesc,GLCreditAmt,GLDebitAmt,TrnDrCr,TrnType,TrnGLAccNoDr,TrnGLAccNoCr,'TrnSign' = CASE WHEN GLAmount < 0 THEN '1' END FROM WFEDITA2ZTRANSACTION WHERE VchNo = '" + txtVoucherNo.Text + "'and TrnFlag=0 and TrnCSGL=1";
                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZGLMCUS");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }

        
        private void Successful()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Delete successfully completed');", true);
            return;
        }

        private void VoucherMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Not Found');", true);
            return;

        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtVoucherNo.Text = string.Empty;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Focus();
            BtnUpdate.Visible = false;
            BtnCancel.Visible = false;
            gvDetailInfo.Visible = false;
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
                    e.Row.Style.Add("height", "30px");
                e.Row.Style.Add("top", "-1500px");

                TextBox lblDrCode = (TextBox)e.Row.FindControl("lblGLAccNoDr");
                TextBox lblCrCode = (TextBox)e.Row.FindControl("lblGLAccNoCr");

                TextBox lblCrAmt = (TextBox)e.Row.FindControl("lblCreditAmt");
                TextBox lblDrAmt = (TextBox)e.Row.FindControl("lblDebitAmt");

                decimal CrAmt = Converter.GetDecimal(lblCrAmt.Text);
                decimal DrAmt = Converter.GetDecimal(lblDrAmt.Text);

                if (CrAmt > 0)
                {
                    lblCrAmt.Enabled = true;
                //    lblCrCode.Enabled = true;
                }

                if (DrAmt > 0)
                {
                    lblDrAmt.Enabled = true;
                    //lblDrCode.Enabled = true;
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {

                    Label lblId = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("lblID");
                    TextBox lblDrCode = (TextBox)gvDetailInfo.Rows[i].Cells[2].FindControl("lblGLAccNoDr");
                    TextBox lblCrCode = (TextBox)gvDetailInfo.Rows[i].Cells[4].FindControl("lblGLAccNoCr");

                    TextBox lblCrAmt = (TextBox)gvDetailInfo.Rows[i].Cells[6].FindControl("lblCreditAmt");
                    TextBox lblDrAmt = (TextBox)gvDetailInfo.Rows[i].Cells[7].FindControl("lblDebitAmt");

                    Label lblTrnDrCr = (Label)gvDetailInfo.Rows[i].Cells[8].FindControl("TrnDrCr");
                    Label lblTrnType = (Label)gvDetailInfo.Rows[i].Cells[9].FindControl("TrnType");
                    Label lblTrnSign = (Label)gvDetailInfo.Rows[i].Cells[12].FindControl("TrnSign");

                    int ID = Converter.GetInteger(lblId.Text);

                    int DrCode = Converter.GetInteger(lblDrCode.Text);
                    int CrCode = Converter.GetInteger(lblCrCode.Text);

                    int RecID = 0;
                    int idincrement = 0;

                    Decimal Tamount = 0;
                    Decimal Gamount = 0;
                    Decimal Cramount = Converter.GetDecimal(lblCrAmt.Text);
                    Decimal Dramount = Converter.GetDecimal(lblDrAmt.Text);


                    if (lblTrnDrCr.Text == "1")
                    {
                        string GlAccType = lblCrCode.Text.Substring(0, 1);
                        if (GlAccType == "1" || GlAccType == "5")
                        {
                            Tamount = (0 - Cramount);
                        }
                    }

                    if (lblTrnDrCr.Text == "0")
                    {
                        string GlAccType = lblCrCode.Text.Substring(0, 1);
                        if (GlAccType == "2" || GlAccType == "4")
                        {
                            Tamount = (0 - Dramount);
                        }
                    }

                    if (Cramount != 0)
                    {
                        Gamount = Cramount;
                    }

                    if (Dramount != 0)
                    {
                        Gamount = Dramount;
                    }

                    Tamount = Gamount;


                    //if (lblTrnSign.Text == "1")
                    //{
                    //    Tamount = (0 - Gamount);
                    //}


                    if (lblTrnType.Text == "3")
                    {

                        RecID = Converter.GetInteger(lblRecID.Text);


                        int mod = RecID % 2;

                        if (mod != 0)
                        {
                            idincrement = ID + 1;
                        }
                        else
                        {
                            idincrement = ID - 1;
                        }
                    }
                    else
                    {
                        idincrement = ID + 1;
                    }

                    if (lblTrnDrCr.Text == "1")
                    {
                        string strQuery1 = "UPDATE WFEDITA2ZTRANSACTION SET  TrnGLAccNoDr = '" + DrCode + "',TrnGLAccNoCr = '" + CrCode + "',GLAccNo = '" + CrCode + "',GLCreditAmt = '" + Gamount + "', GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                        int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

                        string qry = "SELECT Id,GLAmount FROM WFEDITA2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                            Decimal Camount = Converter.GetDecimal(Gamount);

                            if (Amount < 0)
                            {
                                Camount = (0 - Gamount);
                            }

                            string strQuery2 = "UPDATE WFEDITA2ZTRANSACTION SET  TrnGLAccNoDr = '" + DrCode + "',TrnGLAccNoCr = '" + CrCode + "',GLAccNo = '" + DrCode + "',GLDebitAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));
                        }
                    }
                    else
                    {
                        string strQuery1 = "UPDATE WFEDITA2ZTRANSACTION SET  TrnGLAccNoDr = '" + DrCode + "',TrnGLAccNoCr = '" + CrCode + "',GLAccNo = '" + DrCode + "',GLDebitAmt = '" + Gamount + "',GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                        int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZGLMCUS"));

                        string qry = "SELECT Id,GLAmount FROM WFEDITA2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                            Decimal Camount = Converter.GetDecimal(Gamount);

                            if (Amount < 0)
                            {
                                Camount = (0 - Gamount);
                            }

                            string strQuery2 = "UPDATE WFEDITA2ZTRANSACTION SET  TrnGLAccNoDr = '" + DrCode + "',TrnGLAccNoCr = '" + CrCode + "',GLAccNo = '" + CrCode + "',GLCreditAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));
                        }

                    }

                }

                var prm = new object[2];
                prm[0] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);
                prm[1] = hdnID.Text;


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLEditTransactionData", prm, "A2ZGLMCUS"));
                if (result == 0)
                {
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnCancel.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    txtVoucherNo.ReadOnly = false;
                    Successful();
                }
                
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }     

        private void InvalidFinDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Financial Year');", true);
            return;

        }
        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;

        }

        private void InvalidCurrentMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Current Date');", true);
            return;
        }

        protected void InvalidVchTrn()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher Transaction');", true);
            return;
        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            DateTime opdate2 = Converter.GetDateTime(dto.ProcessDate);
            int Begyear = Converter.GetInteger(dto.FinancialBegYear);


            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Iyear = opdate1.Year;
            int Imonth = opdate1.Month;

            if (Iyear < Begyear)
            {
                InvalidFinDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlTrnDate.Text;
                txtTranDate.Focus();
            }

            if (Iyear == Begyear && Imonth < 7)
            {
                InvalidFinDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlTrnDate.Text;
                txtTranDate.Focus();
            }

            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlTrnDate.Text;
                txtTranDate.Focus();
            }

            if (opdate1 == opdate2)
            {
                InvalidCurrentMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlTrnDate.Text;
                txtTranDate.Focus();
            }

        }

        private void InvalidGlCode()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
            return;
        }
        protected void lblGLAccNoDr_TextChanged(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            TextBox glaccdr = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("lblGLAccNoDr");
            TextBox glaccdrDesc = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[3].FindControl("lblGLAccNoDrDesc");
            Label Oglaccdr = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[10].FindControl("OrgGLAccNoDr");

            int GLCode;
            A2ZCGLMSTDTO glObj = new A2ZCGLMSTDTO();
            string input1 = Converter.GetString(glaccdr.Text).Length.ToString();
            if (input1 == "6")
            {
                GLCode = Converter.GetInteger(glaccdr.Text);
                glObj = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
            }
            else
            {
                GLCode = Converter.GetInteger(glaccdr.Text);
                glObj = (A2ZCGLMSTDTO.GetInformation(GLCode));
            }

            if (glObj.GLAccNo > 0)
            {

                CtrlRecType.Text = Converter.GetString(glObj.GLRecType);

                if (CtrlRecType.Text != "2")
                {
                    InvalidGlCode();
                    glaccdr.Text = Oglaccdr.Text;
                    glaccdr.Focus();
                    return;
                }


                string GlAccType1 = Oglaccdr.Text.Substring(0, 1);
                string GlAccType2 = glaccdr.Text.Substring(0, 1);

                if (GlAccType1 != GlAccType2)
                {
                    InvalidGlCode();
                    glaccdr.Text = Oglaccdr.Text;
                    glaccdr.Focus();
                    return;
                }

                glaccdr.Text = Converter.GetString(glObj.GLAccNo);
                glaccdrDesc.Text = Converter.GetString(glObj.GLAccDesc);
            }
            else
            {
                InvalidGlCode();
                glaccdr.Text = Oglaccdr.Text;
                glaccdr.Focus();
                return;
            }
        }

        protected void lblGLAccNoCr_TextChanged(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            TextBox glacccr = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblGLAccNoCr");
            TextBox glacccrDesc = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[5].FindControl("lblGLAccNoCrDesc");
            Label Oglacccr = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[11].FindControl("OrgGLAccNoCr");

            int GLCode;
            A2ZCGLMSTDTO glObj = new A2ZCGLMSTDTO();
            string input1 = Converter.GetString(glacccr.Text).Length.ToString();
            if (input1 == "6")
            {
                GLCode = Converter.GetInteger(glacccr.Text);
                glObj = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
            }
            else
            {
                GLCode = Converter.GetInteger(glacccr.Text);
                glObj = (A2ZCGLMSTDTO.GetInformation(GLCode));
            }

            if (glObj.GLAccNo > 0)
            {

                CtrlRecType.Text = Converter.GetString(glObj.GLRecType);

                if (CtrlRecType.Text != "2")
                {
                    InvalidGlCode();
                    glacccr.Text = Oglacccr.Text;
                    glacccr.Focus();
                    return;
                }

                string GlAccType1 = Oglacccr.Text.Substring(0, 1);
                string GlAccType2 = glacccr.Text.Substring(0, 1);

                if (GlAccType1 != GlAccType2)
                {
                    InvalidGlCode();
                    glacccr.Text = Oglacccr.Text;
                    glacccr.Focus();
                    return;
                }


                glacccr.Text = Converter.GetString(glObj.GLAccNo);
                glacccrDesc.Text = Converter.GetString(glObj.GLAccDesc);
            }
            else
            {
                InvalidGlCode();
                glacccr.Text = Oglacccr.Text;
                glacccr.Focus();
                return;
            }
        }

    }
}