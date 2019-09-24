using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
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
    public partial class CSStaffAccountBalanceTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    string RSearchflag = (string)Session["SSearchflag"];

                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;

                    string Module = (string)Session["SModule"];

                    txtStaffCode.ReadOnly = true;
                    txtTrnStaffCode.ReadOnly = true;

                    lblIntAmt.Visible = false;
                    txtIntAmt.Visible = false;
                    lblPrincAmt.Visible = false;
                    txtPrincAmt.Visible = false;

                    if (lblflag.Text == string.Empty)
                    {
                        hdnPrmValue.Text = Request.QueryString["a%b"];
                        string b = hdnPrmValue.Text;
                        hdnFuncOpt.Text = b.Substring(0, 2);
                        hdnModule.Text = b.Substring(2, 1);
                        txtAccNo.Focus();

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtTranDate.Text = date;
                        lblProcdate.Text = date;

                        lblLimit.Visible = false;
                        lblLimitBalance.Visible = false;

                    }
                    else
                    {
                        hdnModule.Text = Module;


                        string RtxtTranDate = (string)Session["StxtTranDate"];
                        string RlblProcdate = (string)Session["SlblProcdate"];

                        string RCtrlAccType = (string)Session["SCtrlAccType"];
                        string RtxtAccNo = (string)Session["StxtAccNo"];
                        string RlblAccTitle = (string)Session["SlblAccTitle"];
                        string RtxtStaffCode = (string)Session["StxtStaffCode"];

                       
                        string Rlblcls = (string)Session["Slblcls"];
                        string RlblPayType = (string)Session["SlblPayType"];

                        string RlblStaffName = (string)Session["SlblStaffName"];
                       
                        string RlblAccBalance = (string)Session["SlblAccBalance"];

                        string RlblLimitBalance = (string)Session["SlblLimitBalance"];

                        txtTranDate.Text = RtxtTranDate;
                        lblProcdate.Text = RlblProcdate;


                        CtrlAccType.Text = RCtrlAccType;
                        lblcls.Text = Rlblcls;
                        lblPayType.Text = RlblPayType;
                        txtAccNo.Text = RtxtAccNo;
                        lblAccTitle.Text = RlblAccTitle;
                        txtStaffCode.Text = RtxtStaffCode;

                        lblStaffName.Text = RlblStaffName;
                       
                        lblAccBalance.Text = RlblAccBalance;
                        lblLimitBalance.Text = RlblLimitBalance;

                    }




                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    //string date = dt.ToString("dd/MM/yyyy");
                    //txtTranDate.Text = date;
                    //lblProcdate.Text = date;

                    hdnUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));



                    string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + hdnCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        lblBoothNo.Text = hdnCashCode.Text;
                        lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                    }

                    if (lblflag.Text == "1" && RSearchflag == "1" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        GetAccInfo1();
                    }

                    if (lblflag.Text == "1" && RSearchflag == "2" && NewAccNo != "")
                    {
                        txtTrnAccNo.Text = NewAccNo;
                        GetAccInfo2();
                    }



                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;

            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;

        }

        protected void RemoveSession1()
        {
            
            Session["SSearchflag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;

            Session["SCtrlAccType"] = string.Empty;
            Session["StxtAccNo"] = string.Empty;
            Session["SlblAccTitle"] = string.Empty;
            Session["StxtStaffCode"] = string.Empty;
           
            Session["Slblcls"] = string.Empty;
            Session["SlblStaffName"] = string.Empty;
           
            Session["SlblAccBalance"] = string.Empty;
            Session["SlblLimitBalance"] = string.Empty;

            ClearScreen();
        }

        protected void UnPostValue()
        {
            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnCredit) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType =0 AND CuNo =0 AND MemNo ='" + txtStaffCode.Text + "' AND AccType ='" + CtrlAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblUnPostDataCr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountCr"]));
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnDebit) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType =0 AND CuNo =0 AND MemNo ='" + txtStaffCode.Text + "' AND AccType ='" + CtrlAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblUnPostDataDr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountDr"]));
            }

            double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Text);
            double AmtCredit = Converter.GetDouble(lblUnPostDataCr.Text);
            double AmtDebit = Converter.GetDouble(lblUnPostDataDr.Text);
            double LienAmt = Converter.GetDouble(CtrlLienAmt.Text);

            lblAccBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - (AmtCredit + LienAmt))));

            if (lblcls.Text == "5" || lblcls.Text == "6")
            {
                lblLimit.Visible = true;
                lblLimitBalance.Visible = true;

                double limitamt = 0;
                double Amount = 0;
                double sancAmount = 0;

                Amount = Converter.GetDouble(lblAccBalance.Text);
                sancAmount = Converter.GetDouble(CtrlLoanSancAmt.Text);

                if (Amount > 0)
                {
                    limitamt = (sancAmount + Amount);
                }
                else
                {
                    limitamt = (sancAmount - Math.Abs(Amount));
                }

                if (limitamt < 0)
                {
                    limitamt = 0;
                }

                lblLimitBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (limitamt)));

            }


        }



        protected void UpdatedSucessfullyMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            if (CtrlProcStat.Text == "0")
            {
                a = "    TRANSACTION SUCESSFULLY DONE";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "    TRANSACTION INPUT DONE";
            }
            b = "Generated New Voucher No.";
            c = string.Format(CtrlVoucherNo.Text);




            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


            //----------------
            //string a = "";


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (CtrlProcStat.Text == "0")
            //{
            //    a = "    TRANSACTION SUCESSFULLY DONE";
            //}
            //if (CtrlProcStat.Text == "1")
            //{
            //    a = "    TRANSACTION INPUT DONE";
            //}
            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVoucherNo.Text);



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


        protected void TrnLimitValidity()
        {
            try
            {
                double TotalAmt = Converter.GetDouble(txtTrnAmount.Text);
                int Ids = Converter.GetInteger(hdnUserId.Text);
                A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

                CtrlProcStat.Text = "0";

                if (getDTO.IdsNo > 0)
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

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnLimitValidity Problem');</script>");
                //throw ex;
            }
        }

        protected void AccessAmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Insufficent Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);
            return;

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
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                DuplicateVchMSG();
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                return;
            }
        }

        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                TrnVchDeplicate();

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

                if (txtTrnAmount.Text == string.Empty)
                {
                    txtTrnAmount.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Transaction Amount' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction Amount');", true);
                    return;
                }

                if (txtDescription.Text == string.Empty)
                {
                    txtDescription.Focus();

                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Transaction Description');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction Description');", true);
                    return;
                }

                //BalanceChaeck();

                //if (ValidityFlag.Text == "1")
                //{
                //    txtTrnAmount.Text = string.Empty;
                //    txtTrnAmount.Focus();
                //    AccessAmountMSG();
                //    return;
                //}

                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVoucherNo.Text = "C" + hdnCashCode.Text + "-" + getDTO.RecLastNo;

                TrnLimitValidity();

                DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[19];

                prm[0] = hdnUserId.Text;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = CtrlProcStat.Text;
                prm[3] = opdate;
                prm[4] = txtVchNo.Text;    
                prm[5] = txtStaffCode.Text;
                prm[6] = CtrlAccType.Text;
                prm[7] = txtAccNo.Text;   
                prm[8] = txtTrnStaffCode.Text;
                prm[9] = CtrlTrnAccType.Text;
                prm[10] = txtTrnAccNo.Text;
                prm[11] = Converter.GetDouble(txtTrnAmount.Text);
                prm[12] = txtDescription.Text;
                prm[13] = hdnCashCode.Text;
                prm[14] = lblPayType.Text;
                prm[15] = lblTrnPayType.Text;

                if (lblTrnAtyClass.Text == "5" || lblTrnAtyClass.Text == "6")
                {
                    prm[16] = lblTrnAtyClass.Text;
                    if (txtIntAmt.Text == string.Empty)
                    {
                        prm[17] = "0";
                    }
                    else
                    {
                        prm[17] = txtIntAmt.Text;
                    }

                    if (txtPrincAmt.Text == string.Empty)
                    {
                        prm[18] = "0";
                    }
                    else
                    {
                        prm[18] = txtPrincAmt.Text;
                    }
                }
                else
                {
                    prm[16] = "0";
                    prm[17] = "0";
                    prm[18] = "0";
                }



                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddStaffBalanceTransfer", prm, "A2ZCSMCUS"));

                if (result == 0)
                {
                    PrintTrnVoucher();

                    //UpdatedSucessfullyMSG();
                   
                    
                    RemoveSession1();
                    //ClearScreen();


                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BalanceChaeck()
        {

            ValidityFlag.Text = "0";

            double limitamt = 0;
            double Amount = 0;
            double sancAmount = 0;
            double TrnAmount = 0;

            Amount = Converter.GetDouble(lblAccBalance.Text);
            sancAmount = Converter.GetDouble(CtrlLoanSancAmt.Text);
            TrnAmount = Converter.GetDouble(txtTrnAmount.Text);
            limitamt = Converter.GetDouble(lblLimitBalance.Text);

            //if (Amount > 0)
            //{
            //    limitamt = (sancAmount + Amount);
            //}
            //else
            //{
            //    limitamt = (sancAmount - Math.Abs(Amount));
            //}

            if (lblAtyClass.Text == "5" || lblAtyClass.Text == "6")
            {
                if (limitamt < TrnAmount)
                {
                    ValidityFlag.Text = "1";
                    return;
                }
            }
            else
            {
                if (Amount < TrnAmount)
                {
                    ValidityFlag.Text = "1";
                    return;
                }
            }

        }





        protected void PrintTrnVoucher()
        {
            try
            {

                lblTrnTypeTitle.Text = "TRANSFER";
                lblFuncTitle.Text = "WITHDRAWAL";


                DateTime Pdate = DateTime.ParseExact(lblProcdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text.Trim());
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblStaffName.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, hdnUserId.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTrfTransactionVch");



                //Session["STranDate"] = txtTranDate.Text;
                //Session["SFuncOpt"] = lblFuncOpt.Text;
                //Session["SModule"] = CtrlModule.Text;
                //Session["flag"] = "2";

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }


        protected void ClearScreen()
        {
            txtVchNo.Text = string.Empty;
            txtStaffCode.Text = string.Empty;
            lblStaffName.Text = string.Empty;
            
            txtAccNo.Text = string.Empty;
            lblAccTitle.Text = string.Empty;
            txtTrnStaffCode.Text = string.Empty;
            lblTrnStaffName.Text = string.Empty;
            

            txtTrnAccNo.Text = string.Empty;
            lblTrnAccTitle.Text = string.Empty;

            txtTrnAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;
            lblAccBalance.Text = string.Empty;
            


            txtVchNo.Focus();


        }


        protected void InvalidAccount()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account');", true);
            return;
        }

        protected void TransferedAccount()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }

        protected void ClosedAccount()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Closed');", true);
            return;
        }
        protected void InvalidInput()
        {
           
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Transfer');", true);
            return;
        }
        protected void Successfull()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfer Successfully Done');", true);
            return;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        private void InvalidDateMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(lblProcdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            int Month1 = opdate1.Month;
            int Month2 = opdate2.Month;

            if (opdate1 > opdate2 || Month1 != Month2)
            {
                InvalidDateMSG();
                txtTranDate.Text = lblProcdate.Text;
                txtTranDate.Focus();
            }

            if (opdate1 < opdate2)
            {
                //lblTranDate.Text = "Back Value Date:";
                lblTranDate.Text = "Back Log Date:";
            }
            else
            {
                lblTranDate.Text = "Transaction Date:";
            }

        }


        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {

            if (txtAccNo.Text != string.Empty)
            {
                GetAccInfo1();
            }
        }

        protected void txtTrnAccNo_TextChanged(object sender, EventArgs e)
        {

            if (txtTrnAccNo.Text != string.Empty)
            {
                GetAccInfo2();
            }
        }


        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            Session["StxtTranDate"] = txtTranDate.Text;
            Session["SlblProcdate"] = lblProcdate.Text;

            Session["SSearchflag"] = "1";

            Session["SModule"] = "04";
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "6";


            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('CSGetStaffAccountNo.aspx','_blank');</script>", false);

        }

        protected void BtnTrnSearch_Click(object sender, EventArgs e)
        {
            Session["StxtTranDate"] = txtTranDate.Text;
            Session["SlblProcdate"] = lblProcdate.Text;


            Session["SCtrlAccType"] = CtrlAccType.Text;
            Session["StxtAccNo"] = txtAccNo.Text;
            Session["SlblAccTitle"] = lblAccTitle.Text;
            Session["StxtStaffCode"] = txtStaffCode.Text;

          

            Session["SlblStaffName"] = lblStaffName.Text;
            
            Session["SlblAccBalance"] = lblAccBalance.Text;

            Session["SlblLimitBalance"] = lblLimitBalance.Text;

            Session["Slblcls"] = lblcls.Text;
            Session["SlblPayType"] = lblPayType.Text;





            Session["SSearchflag"] = "2";

            Session["SModule"] = "04";
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "6";


            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('CSGetStaffAccountNo.aspx','_blank');</script>", false);

        }

        public void GetAccInfo1()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccount();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    CtrlAccStatus.Text = Converter.GetString(accgetDTO.AccStatus);

                    if (CtrlAccStatus.Text == "98")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        TransferedAccount();
                        return;
                    }

                    if (CtrlAccStatus.Text == "99")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        ClosedAccount();
                        return;
                    }


                    CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    CtrlLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    CtrlLoanSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.LoanAmount));

                    CtrlAccType.Text = Converter.GetString(accgetDTO.AccType);
                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);


                    UnPostValue();



                    txtStaffCode.Text = Converter.GetString(accgetDTO.MemberNo);

                    lblAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);

                    if (lblAtyClass.Text == "7")
                    {
                        string input = txtAccNo.Text;
                        lblPayType.Text = input.Substring(13, 3);
                        int paytype = Converter.GetInteger(lblPayType.Text);
                        A2ZTRNCODEDTO get3DTO = (A2ZTRNCODEDTO.GetInformation99(paytype));
                        if (get3DTO.TrnCode > 0)
                        {
                            lblTrnCode.Text = Converter.GetString(get3DTO.TrnCode);
                            lblAccTitle.Text = Converter.GetString(get3DTO.TrnDescription);
                        }
                    }
                    else
                    {
                        lblPayType.Text = "0";
                        Int16 AccType = Converter.GetSmallInteger(CtrlAccType.Text);
                        A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get3DTO.AccTypeCode > 0)
                        {
                            lblAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                        }

                        int AType = Converter.GetInteger(CtrlAccType.Text);
                        A2ZTRNCODEDTO get4DTO = (A2ZTRNCODEDTO.GetInformation01(AType));
                        if (get4DTO.TrnCode > 0)
                        {
                            lblTrnCode.Text = Converter.GetString(get4DTO.TrnCode);
                        }
                    }

                   
                    int MNo = Converter.GetInteger(txtStaffCode.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(0,0, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblStaffName.Text = Converter.GetString(get6DTO.MemberName);
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }

        public void GetAccInfo2()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtTrnAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccount();
                    txtTrnAccNo.Text = string.Empty;
                    txtTrnAccNo.Focus();
                    return;
                }
                else
                {
                    CtrlAccStatus.Text = Converter.GetString(accgetDTO.AccStatus);

                    if (CtrlAccStatus.Text == "98")
                    {
                        txtTrnAccNo.Text = string.Empty;
                        txtTrnAccNo.Focus();
                        TransferedAccount();
                        return;
                    }

                    if (CtrlAccStatus.Text == "99")
                    {
                        txtTrnAccNo.Text = string.Empty;
                        txtTrnAccNo.Focus();
                        ClosedAccount();
                        return;
                    }


                    txtTrnStaffCode.Text = Converter.GetString(accgetDTO.MemberNo);


                    CtrlTrnAccType.Text = Converter.GetString(accgetDTO.AccType);

                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);
                    lblTrnAtyClass.Text = Converter.GetString(accgetDTO.AccAtyClass);


                    if (lblTrnAtyClass.Text == "5" || lblTrnAtyClass.Text == "6")
                    {
                        lblIntAmt.Visible = true;
                        txtIntAmt.Visible = true;
                        lblPrincAmt.Visible = true;
                        txtPrincAmt.Visible = true;
                        txtTrnAmount.ReadOnly = true;
                    }


                    if (lblAtyClass.Text == "7")
                    {
                        string input = txtTrnAccNo.Text;
                        lblTrnPayType.Text = input.Substring(13, 3);
                        int paytype = Converter.GetInteger(lblTrnPayType.Text);
                        A2ZTRNCODEDTO get3DTO = (A2ZTRNCODEDTO.GetInformation99(paytype));
                        if (get3DTO.TrnCode > 0)
                        {
                            lblTrnAccTitle.Text = Converter.GetString(get3DTO.TrnDescription);
                        }
                    }
                    else
                    {
                        lblTrnPayType.Text = "0";
                        Int16 AccType = Converter.GetSmallInteger(CtrlTrnAccType.Text);
                        A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get3DTO.AccTypeCode > 0)
                        {
                            lblTrnAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                        }
                    }


                   

                    int MNo = Converter.GetInteger(txtTrnStaffCode.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(0,0, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblTrnStaffName.Text = Converter.GetString(get6DTO.MemberName);
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }

        protected void txtTrnAmount_TextChanged(object sender, EventArgs e)
        {
            txtTrnAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", txtTrnAmount.Text));
        }

        protected void txtIntAmt_TextChanged(object sender, EventArgs e)
        {
            txtIntAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", txtIntAmt.Text));

            decimal intamt = Converter.GetDecimal(txtIntAmt.Text);
            decimal Princamt = Converter.GetDecimal(txtPrincAmt.Text);

            decimal sumamt = (intamt + Princamt);
            txtTrnAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", sumamt));
        }

        protected void txtPrincAmt_TextChanged(object sender, EventArgs e)
        {
            txtPrincAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", txtPrincAmt.Text));

            decimal intamt = Converter.GetDecimal(txtIntAmt.Text);
            decimal Princamt = Converter.GetDecimal(txtPrincAmt.Text);

            decimal sumamt = (intamt + Princamt);
            txtTrnAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", sumamt));
        }

    }
}




