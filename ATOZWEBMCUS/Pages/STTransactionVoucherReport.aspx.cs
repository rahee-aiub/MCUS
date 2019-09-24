using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class STTransactionVoucherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtProcessDate.Text = date;
                lblProcDate.Text = date;

                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }
            }
        }



        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void InvalidVoucherMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Transaction Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Not Avaiable');", true);
            return;

        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            if (txtProcessDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please fill up the process date!' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill up the Process Date');", true);

                return;
            }

            if (txtVchNo.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Voucher No.' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
                return;
            }

            DateTime fdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            if (lblProcDate.Text != txtProcessDate.Text)
            {
                lblVchFlag.Text = "1";
                lblVchYear.Text = Converter.GetString(fdate.Year);
            }
            else
            {
                lblVchFlag.Text = "0";
                lblVchYear.Text = Converter.GetString(0);
            }

            string qry;
            DataTable dt;

          

            if (ddlFuncType.SelectedValue == "2")
            {
                if (lblVchFlag.Text == "0")
                {
                    if (CtrlModule.Text == "1")
                    {
                        qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND TrnFlag= 0 AND TrnCSGL= 1";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    }
                    else
                    {
                        qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND FromCashCode='" + lblCashCode.Text + "' AND TrnFlag= 0 AND TrnCSGL= 1";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    }
                }
                else
                {
                    if (CtrlModule.Text == "1")
                    {
                        qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZCSMCUST" + lblVchYear.Text + "..A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND TrnFlag= 0 AND TrnCSGL= 1";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");

                    }
                    else
                    {
                        qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZCSMCUST" + lblVchYear.Text + "..A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND FromCashCode='" + lblCashCode.Text + "' AND TrnFlag= 0 AND TrnCSGL= 1";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    }
                }
            }
            else
            {
                if (lblVchFlag.Text == "0")
                {
                    if (CtrlModule.Text == "1")
                    {
                        qry = "SELECT Id,FuncOpt,TransactionType,UserID,TrnWarehouseNo FROM A2ZSTTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "'";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
                    }
                    else
                    {
                        qry = "SELECT Id,FuncOpt,TransactionType,UserID,TrnWarehouseNo FROM A2ZSTTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnWarehouseNo='" + lblCashCode.Text + "'";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
                    }
                }
                else
                {
                    if (CtrlModule.Text == "1")
                    {
                        qry = "SELECT Id,FuncOpt,TransactionType,UserID,TrnWarehouseNo FROM A2ZSTMCUST" + lblVchYear.Text + "..A2ZSTTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TransactionDate='" + fdate + "'";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");

                    }
                    else
                    {
                        qry = "SELECT Id,FuncOpt,TransactionType,UserID,TrnWarehouseNo FROM A2ZSTMCUST" + lblVchYear.Text + "..A2ZSTTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TransactionDate='" + fdate + "' AND TrnWarehouseNo='" + lblCashCode.Text + "'";
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
                    }
                }
            }


            if (dt.Rows.Count > 0)
            {
                lblFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                lblUserID.Text = Converter.GetString(dt.Rows[0]["UserID"]);

                if (ddlFuncType.SelectedValue == "2")
                {
                    lblTrnType.Text = Converter.GetString(dt.Rows[0]["TrnType"]);
                    lblFromCashCode.Text = Converter.GetString(dt.Rows[0]["FromCashCode"]);
                }
                else
                {
                    lblTrnType.Text = Converter.GetString(dt.Rows[0]["TransactionType"]);
                    lblFromCashCode.Text = Converter.GetString(dt.Rows[0]["TrnWarehouseNo"]);
                }


                string qry1 = "SELECT IdsNo,IdsName from A2ZSYSIDS WHERE IdsNo = '" + lblUserID.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblUserIDName.Text = Converter.GetString(dt1.Rows[0]["IdsName"]);
                }

                string qry3 = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblFromCashCode.Text + "'";
                DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZGLMCUS");
                if (dt3.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblFromCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt3.Rows[0]["GLAccDesc"]);
                }
            }
            else
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                InvalidVoucherMSG();
                return;
            }


            if (lblTrnType.Text == "1")
            {
                lblTrnTypeTitle.Text = "CASH";
            }
            else if (lblTrnType.Text == "3")
            {
                lblTrnTypeTitle.Text = "TRANSFER";
            }
            else if (lblTrnType.Text == "48")
            {
                lblTrnTypeTitle.Text = "BANK";
            }


            if (ddlFuncType.SelectedValue == "1")
            {

                PrintTrnVoucher2();
            }
            else
            {
                PrintTrnVoucher1();
            }


        }

        protected void PrintTrnVoucher1()
        {
            try
            {

                DateTime Pdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtProcessDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);
                
                Int32 CSGL = Converter.GetSmallInteger(1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptSTTransactionVch");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }


        protected void PrintTrnVoucher2()
        {
            try
            {

                DateTime Pdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtProcessDate.Text));

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

                if (lblFuncOpt.Text == "1")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkPurchaseInvoiceReport");
                }
                else if (lblFuncOpt.Text == "11")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkSalesInvoiceReport");
                }
                else if (lblFuncOpt.Text == "12")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkTransferInvoiceReport");
                }
                else if (lblFuncOpt.Text == "13")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkUseInvoiceReport");
                }
                else if (lblFuncOpt.Text == "61" || lblFuncOpt.Text == "62" || lblFuncOpt.Text == "63")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkPaymentInvoiceReport");
                }
                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }








    }
}
