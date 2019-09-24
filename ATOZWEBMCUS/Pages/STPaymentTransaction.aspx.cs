using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class STPaymentTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblLedgerBalance.Visible = false;
                txtPayableAmt.Visible = false;
                lblVatBalance.Visible = false;
                txtPayableVATAmt.Visible = false;
                lblTaxBalance.Visible = false;
                txtPayableTAXAmt.Visible = false;

                lblGLBankCode.Visible = false;
                txtGLBankCode.Visible = false;
                ddlGLBankCode.Visible = false;
                lblChqNo.Visible = false;
                txtChqNo.Visible = false;



                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                SupplierDropdown();

                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblCashCodeName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }


            }
        }

        protected void ddlGLBankCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLBankCode.SelectedValue != "-Select-")
                {
                    int GLCode = Converter.GetInteger(ddlGLBankCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);

                        hdnGLSubHead.Text = Converter.GetString(getDTO.GLSubHead);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }


        protected void GLBankCodeDropdown()
        {
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 and (GLSubHead = 10106000 OR GLSubHead = 20801000) AND Status=1 ORDER BY GLAccDesc ASC";
            ddlGLBankCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLBankCode, "A2ZGLMCUS");
        }

        private void SupplierDropdown()
        {
            string sqlquery = "SELECT SuppCode,SuppName from A2ZSUPPLIER";
            ddlSupplier = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSupplier, "A2ZSTMCUS");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlSupplier.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Supplier No.');", true);
                return;
            }

            if (ddlPaymentOptn.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Payment Type');", true);
                return;
            }

            if (ddlTrnType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Transaction Type');", true);
                return;
            }

            if (txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Voucher No.');", true);
                return;
            }

            if (txtAmt.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Amount');", true);
                return;
            }


            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblTrnDate.Text = date;
               
                string qry = String.Empty;

                DateTime opdate = DateTime.ParseExact(lblTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[17];

                prm[0] = txtVchNo.Text;
                prm[1] = ddlSupplier.SelectedValue;
                prm[2] = ddlSupplier.SelectedItem.Text;
                prm[3] = txtTrnNote.Text;
                prm[4] = ddlPaymentOptn.SelectedValue;
                prm[5] = ddlPaymentOptn.SelectedItem.Text;

                if (ddlTrnType.SelectedValue == "48")
                {
                    prm[6] = txtGLBankCode.Text;
                    prm[7] = ddlGLBankCode.SelectedItem.Text;
                    prm[8] = txtChqNo.Text;
                }
                else
                {
                    prm[6] = "0";
                    prm[7] = "0";
                    prm[8] = "0";
                }

                prm[9] = opdate;
                prm[10] = ddlTrnType.SelectedValue;
                prm[11] = ddlTrnType.SelectedItem.Text;
                prm[12] = lblID.Text;
                prm[13] = txtAmt.Text;
                prm[14] = "0";
                prm[15] = lblCashCode.Text;
                prm[16] = lblCashCodeName.Text; 
                
                


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STSupplierPayment", prm, "A2ZSTMCUS"));

                if (result == 0)
                {
                    PrintTrnVoucher2();

                    //Response.Redirect(Request.RawUrl);
                }






                //if (ddlPaymentOptn.SelectedValue == "1") // Supplier Payment
                //{
                //    qry = "INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,UserId) VALUES('" + lblTrnDate.Text + "','" + txtVchNo.Text + "','61','Supplier Payment','0','0','" + txtAmt.Text + "','" + txtAmt.Text + "','" + ddlSupplier.SelectedValue + "','" + ddlSupplier.SelectedItem.Text + "','" + ddlTrnType.SelectedValue + "','" + ddlTrnType.SelectedItem.Text + "','" + lblID.Text + "')";
                //}
                //if (ddlPaymentOptn.SelectedValue == "2") // VAT Payment
                //{
                //    qry = "INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,UserId) VALUES('" + lblTrnDate.Text + "','" + txtVchNo.Text + "','62','VAT Payment','" + txtAmt.Text + "','0','0','" + txtAmt.Text + "','" + ddlSupplier.SelectedValue + "','" + ddlSupplier.SelectedItem.Text + "','" + ddlTrnType.SelectedValue + "','" + ddlTrnType.SelectedItem.Text + "','" + lblID.Text + "')";
                //}
                //if (ddlPaymentOptn.SelectedValue == "3") // TAX Payment
                //{
                //    qry = "INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,UserId) VALUES('" + lblTrnDate.Text + "','" + txtVchNo.Text + "','63','TAX Payment','0','" + txtAmt.Text + "','0','" + txtAmt.Text + "','" + ddlSupplier.SelectedValue + "','" + ddlSupplier.SelectedItem.Text + "','" + ddlTrnType.SelectedValue + "','" + ddlTrnType.SelectedItem.Text + "','" + lblID.Text + "')";
                //}

                //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZSTMCUS"));
                //if (rowEffect > 0)
                //{
                //    ClearInfo();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Insert Succesfully');", true);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Not Inserted');", true);
                return;
            }
        }


        protected void PrintTrnVoucher2()
        {
            try
            {

                DateTime Pdate = DateTime.ParseExact(lblTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(lblTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblTrnDate.Text));

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "TRANSFER");

                //Int32 CSGL = Converter.GetSmallInteger(1);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkPaymentInvoiceReport");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        private void ClearInfo()
        {
            ddlSupplier.SelectedIndex = 0;
            ddlPaymentOptn.SelectedIndex = 0;
            ddlTrnType.SelectedIndex = 0;

            txtPayableAmt.Text = string.Empty;
            txtPayableVATAmt.Text = string.Empty;
            txtPayableTAXAmt.Text = string.Empty;
            txtAmt.Text = string.Empty;
            txtVchNo.Text = string.Empty;
        }
        protected void txtVchNo_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT UserId,FuncOpt FROM A2ZSTTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher No. Already Exist');", true);
                return;
            }
        }

        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            lblLedgerBalance.Visible = true;
            txtPayableAmt.Visible = true;
            lblVatBalance.Visible = true;
            txtPayableVATAmt.Visible = true;
            lblTaxBalance.Visible = true;
            txtPayableTAXAmt.Visible = true;


            int Code = Converter.GetInteger(txtSupplier.Text);
            A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(Code));

            if (getDTO.SuppCode > 0)
            {
                ddlSupplier.SelectedValue = Converter.GetString(getDTO.SuppCode);
                txtPayableAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppBalance));
                txtPayableVATAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppVATAmt));
                txtPayableTAXAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppTAXAmt));
            }
        }
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                lblLedgerBalance.Visible = true;
                txtPayableAmt.Visible = true;
                lblVatBalance.Visible = true;
                txtPayableVATAmt.Visible = true;
                lblTaxBalance.Visible = true;
                txtPayableTAXAmt.Visible = true;


                int Code = Converter.GetInteger(ddlSupplier.SelectedValue);
                A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(Code));

                if (getDTO.SuppCode > 0)
                {
                    txtSupplier.Text = Converter.GetString(getDTO.SuppCode);
                    txtPayableAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppBalance));
                    txtPayableVATAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppVATAmt));
                    txtPayableTAXAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.SuppTAXAmt));
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrnType.SelectedValue == "48")
            {
                lblGLBankCode.Visible = true;
                txtGLBankCode.Visible = true;
                ddlGLBankCode.Visible = true;
                lblChqNo.Visible = true;
                txtChqNo.Visible = true;

                GLBankCodeDropdown();
            }
            else
            {
                lblGLBankCode.Visible = false;
                txtGLBankCode.Visible = false;
                ddlGLBankCode.Visible = false;
                lblChqNo.Visible = false;
                txtChqNo.Visible = false;
            }
        }

        protected void txtGLBankCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLBankCode.Text != string.Empty)
                {
                    int GLCode;
                    A2ZCGLMSTDTO getDTO = new A2ZCGLMSTDTO();
                    string input1 = Converter.GetString(txtGLBankCode.Text).Length.ToString();
                    if (input1 == "6")
                    {
                        GLCode = Converter.GetInteger(txtGLBankCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                    }
                    else
                    {
                        GLCode = Converter.GetInteger(txtGLBankCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));
                    }

                    if (getDTO.GLAccNo > 0)
                    {
                        //lblStatus.Text = Converter.GetString(getDTO.Status);
                        //CtrlRecType.Text = Converter.GetString(getDTO.GLRecType);
                        hdnGLSubHead.Text = Converter.GetString(getDTO.GLSubHead);
                        //CtrlContraAType.Text = Converter.GetString(getDTO.GLAccType);

                        //if (lblStatus.Text == "99")
                        //{
                        //    txtGLBankCode.Text = string.Empty;
                        //    txtGLBankCode.Focus();
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Closed GL Code');", true);
                        //    return;
                        //}


                        //if (CtrlRecType.Text != "2")
                        //{
                        //    txtGLBankCode.Text = string.Empty;
                        //    txtGLBankCode.Focus();
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
                        //    return;
                        //}
                        if (ddlTrnType.SelectedValue == "2" && (hdnGLSubHead.Text != "10106000" && hdnGLSubHead.Text != "20801000"))
                        {
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Bank Code');", true);
                            return;
                        }


                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLBankCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                    }
                    else
                    {
                        txtGLBankCode.Text = string.Empty;
                        txtGLBankCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGLCashCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }


    }
}
