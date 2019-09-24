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
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;
using System.Collections.Generic;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Nflag = (string)Session["NFlag"];
                string Cflag = (string)Session["flag"];
                CtrlFlag.Text = Cflag;
                lblNFlag.Text = Nflag;


                if (CtrlFlag.Text == "1" && lblNFlag.Text == "1")
                {
                    string CuNo = (string)Session["RCreditUNo"];
                    txtCreditUNo.Text = CuNo;
                    string MemNo = (string)Session["RMemNo"];
                    txtLoanMemNo.Text = MemNo;
                }


                txtLoanFees.Enabled = false;


                txtVchNo.Enabled = false;





                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtLoanAppDate.Text = date;
                lblProcDate.Text = date;

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));


                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }


                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(4));
                lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                LoanPurposeDdl();
                pnlDeposit.Visible = false;
                pnlProperty.Visible = false;
                pnlShare.Visible = false;
                //lblTotalAmt.Visible = false;
                lbltotalshare.Visible = false;
                lblTotalProprty.Visible = false;
                txtShareAmount.ReadOnly = true;
                Hideinfo();


                if (CtrlFlag.Text == "1")
                {
                    string mod = (string)Session["Module"];
                    lblModule.Text = mod;
                }
                else
                {
                    lblModule.Text = Request.QueryString["a%b"];
                    MsgFlag.Text = string.Empty;
                    CtrlFlag.Text = string.Empty;
                    TruncateWF();
                }


                AccTypedropdown1();

                if (CtrlFlag.Text == "1" && lblNFlag.Text == "1")
                {
                    string CuNo = (string)Session["RCreditUNo"];
                    txtCreditUNo.Text = CuNo;
                    string MemNo = (string)Session["RMemNo"];
                    txtLoanMemNo.Text = MemNo;
                }


                if (CtrlFlag.Text == "1")
                {
                    string dat = (string)Session["TrnDate"];
                    txtLoanAppDate.Text = dat;

                    if (lblPCtrl.Text == "1")
                    {
                        string memno = (string)Session["NewMemNo"];
                        txtLoanMemNo.Text = memno;
                        string memname = (string)Session["NewMemName"];
                        lblMemName.Text = memname;
                    }
                    else
                        if (lblNFlag.Text == "1")
                        {
                            string CuNo = (string)Session["RCreditUNo"];
                            txtCreditUNo.Text = CuNo;
                            string MemNo = (string)Session["RMemNo"];
                            txtLoanMemNo.Text = MemNo;
                        }
                        else
                        {
                            string RtxtCreditUNo = (string)Session["StxtCreditUNo"];
                            txtCreditUNo.Text = RtxtCreditUNo;

                            string RlblCuType = (string)Session["SlblCuType"];
                            lblCuType.Text = RlblCuType;

                            string RlblCu = (string)Session["SlblCu"];
                            lblCu.Text = RlblCu;

                            string RtxtLoanMemNo = (string)Session["StxtLoanMemNo"];
                            txtLoanMemNo.Text = RtxtLoanMemNo;

                            string memname = (string)Session["MemName"];
                            lblMemName.Text = memname;

                            string RlblCuName = (string)Session["SlblCuName"];
                            lblCuName.Text = RlblCuName;

                            string RlblMemName = (string)Session["SlblMemName"];
                            lblMemName.Text = RlblMemName;
                        }

                    string LType = (string)Session["LAcType"];
                    ddlAccType.SelectedValue = LType;

                    string RlblTypeCls = (string)Session["SlblTypeCls"];
                    lblTypeCls.Text = RlblTypeCls;

                    string RAccTypeGuaranty = (string)Session["SAccTypeGuaranty"];
                    lblAccTypeGuaranty.Text = RAccTypeGuaranty;

                    string NewAccNo = (string)Session["NewAccNo"];
                    txtAccNo.Text = NewAccNo;

                    string RtxtLoanAppAmount = (string)Session["StxtLoanAppAmount"];
                    txtLoanAppAmount.Text = RtxtLoanAppAmount;

                    string RtxtNoInstallment = (string)Session["StxtNoInstallment"];
                    txtNoInstallment.Text = RtxtNoInstallment;

                    string RtxtLoanInterestRate = (string)Session["StxtLoanInterestRate"];
                    txtLoanInterestRate.Text = RtxtLoanInterestRate;

                    string RtxtLoanInstallmentAmount = (string)Session["StxtLoanInstallmentAmount"];
                    txtLoanInstallmentAmount.Text = RtxtLoanInstallmentAmount;

                    string RtxtLoanLastInstlAmount = (string)Session["StxtLoanLastInstlAmount"];
                    txtLoanLastInstlAmount.Text = RtxtLoanLastInstlAmount;

                    string RtxtLoanExpDate = (string)Session["StxtLoanExpDate"];
                    txtLoanExpDate.Text = RtxtLoanExpDate;


                    string RtxtODPeriod = (string)Session["StxtODPeriod"];
                    txtODPeriod.Text = RtxtODPeriod;

                    string RtxtODExpDate = (string)Session["StxtODExpDate"];
                    txtODExpDate.Text = RtxtODExpDate;

                    



                    string RddlLoanCategory = (string)Session["SddlLoanCategory"];
                    ddlLoanCategory.SelectedValue = RddlLoanCategory;

                    string RddlLoanPurpose = (string)Session["SddlLoanPurpose"];
                    ddlLoanPurpose.SelectedValue = RddlLoanPurpose;

                    string RtxtLoanFees = (string)Session["StxtLoanFees"];
                    txtLoanFees.Text = RtxtLoanFees;

                    string RtxtVchNo = (string)Session["StxtVchNo"];
                    txtVchNo.Text = RtxtVchNo;

                    string RlblTotalResult = (string)Session["SlblTotalResult"];
                    lblTotalResult.Text = RlblTotalResult;

                    string RtxtTotalAmt = (string)Session["StxtTotalAmt"];
                    txtTotalAmt.Text = RtxtTotalAmt;

                    if (txtTotalAmt.Text != string.Empty)
                    {
                        gvInfo();
                    }


                    if (lblTypeCls.Text == "5")
                    {
                        BtnShare.Visible = false;
                        BtnDeposit.Visible = true;
                    }
                    else
                    {
                        BtnShare.Visible = true;
                        BtnDeposit.Visible = false;
                    }

                    string RlblShareTotalAmt = (string)Session["SlblShareTotalAmt"];
                    lblShareTotalAmt.Text = RlblShareTotalAmt;

                    if (lblShareTotalAmt.Text != string.Empty)
                    {
                        gvShareInfo();
                    }

                    string RlblSumProperty = (string)Session["SlblSumProperty"];
                    lblSumProperty.Text = RlblSumProperty;

                    if (lblSumProperty.Text != string.Empty)
                    {
                        gvPropertyInfo();
                    }


                    string RpnlDeposit = (string)Session["SpnlDeposit"];
                    if (RpnlDeposit == "1")
                    {
                        pnlDeposit.Visible = true;
                    }
                    else
                    {
                        pnlDeposit.Visible = false;
                    }

                    string RpnlShare = (string)Session["SpnlShare"];
                    if (RpnlShare == "1")
                    {
                        pnlShare.Visible = true;
                    }
                    else
                    {
                        pnlShare.Visible = false;
                    }

                    if (pnlDeposit.Visible == true)
                    {
                        pnlDeposit.Visible = true;
                        pnlProperty.Visible = false;
                        pnlShare.Visible = false;
                        pnlLoanApplication.Visible = false;

                        GetAccInfo();
                        gvInfo();
                        SumValue();
                        TotalGuarantor();

                    }
                    else
                        if (pnlShare.Visible == true)
                        {
                            pnlDeposit.Visible = false;
                            pnlProperty.Visible = false;
                            pnlShare.Visible = true;
                            pnlLoanApplication.Visible = false;

                            string CuNo = (string)Session["RCreditUNo"];
                            txtShareCuNo.Text = CuNo;
                            txtShareCuNo_TextChanged(this, EventArgs.Empty);

                            gvShareInfo();
                            SumShareValue();
                            TotalGuarantor();

                        }
                        else
                        {
                            if (lblTypeCls.Text == "5")
                            {
                                Hidetrue();
                            }
                            ddlAccType_SelectedIndexChanged(this, EventArgs.Empty);
                            txtCreditUNo_TextChanged(this, EventArgs.Empty);
                            txtLoanMemNo_TextChanged(this, EventArgs.Empty);
                            if (MsgFlag.Text == "1")
                            {
                                //txtCreditUNo.Text = string.Empty;
                                //lblCuName.Text = string.Empty;
                                txtLoanMemNo.Text = string.Empty;
                                lblMemName.Text = string.Empty;
                                txtLoanMemNo.Focus();
                                return;
                            }
                            else
                            {
                                if (txtLoanMemNo.Text == string.Empty)
                                {
                                    txtLoanMemNo.Focus();
                                }
                                else
                                {
                                    txtLoanAppAmount.Focus();
                                }
                            }
                        }
                    SessionRemove();


                }
                //int lappno = Converter.GetInteger(lblLastAppNo.Text);
                //int LoanAppNo = lappno + 1;
                //lblApplicationNo.Text = Converter.GetString(LoanAppNo);
                else
                {

                    ddlAccType.Focus();
                }


            }

        }


        #region Loan Application
        ///------------ Loan Application------------------------------------------



        protected void SessionRemove()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            //Session["date"] = string.Empty;
            //Session["Module"] = string.Empty;
            Session["MemName"] = string.Empty;
            Session["LAcType"] = string.Empty;
            Session["flag"] = string.Empty;

            Session["SlblTypeCls"] = string.Empty;

            Session["SSPflag"] = string.Empty;
            Session["SAccTypeGuaranty"] = string.Empty;

            Session["SpnlDeposit"] = string.Empty;
            Session["SpnlShare"] = string.Empty;
            Session["NFlag"] = string.Empty;

            Session["SlblTotalResult"] = string.Empty;
            Session["StxtTotalAmt"] = string.Empty;
            Session["SlblShareTotalAmt"] = string.Empty;
            Session["SlblSumProperty"] = string.Empty;

            CtrlFlag.Text = string.Empty;
        }
        private void AccTypedropdown1()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where (AccTypeClass BETWEEN 5 AND 6) AND AccTypeMode !='2' ORDER BY AccTypeClass";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        private void AccTypedropdown2()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass BETWEEN 5 AND 6 AND AccTypeMode !='1'";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }
        private void LoanPurposeDdl()
        {
            string sqlquery = "SELECT LPurposeCode,LPurposeDescription from A2ZLPURPOSE ";
            ddlLoanPurpose = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLoanPurpose, "A2ZCSMCUS");
        }

        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZACGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZCSMCUS"));

            string ShareQry = "DELETE  dbo.WFA2ZSHGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ShareQry, "A2ZCSMCUS"));

            string PropertyQry = "DELETE dbo.WFA2ZPRGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(PropertyQry, "A2ZCSMCUS"));

        }



        protected void CuTransferMessage()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";

            a = "Credit Union No. Already Transfered";
            e = "New Credit Union No.";
            b = string.Format("New Credit Union Type : {0}", lblCuTypeName.Text);
            c = string.Format(lblCu.Text);
            d = string.Format(lblCuType.Text);
            X = "-";

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e;
            Msg += d;
            Msg += X;
            Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    //int CN = Converter.GetInteger(txtCreditUNo.Text);

                    //hdnCuNumber.Value = Converter.GetString(CN);


                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);

                    lblCu.Text = Converter.GetString(CNo);
                    lblCuType.Text = Converter.GetString(CuType);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    //if (getDTO.CreditUnionNo > 0)

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblCu.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        if (CtrlFlag.Text != "1")
                        {
                            txtLoanMemNo.Text = string.Empty;
                            lblMemName.Text = string.Empty;
                        }


                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblCu.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblCu.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }

                            CuTransferMessage();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }



                        //string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + lblCuType.Text + "'";
                        //ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        //ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCu.Text);
                        //txtLoanMemNo.Focus();
                        //string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCu.Text + "'and CuType='" + lblCuType.Text + "' GROUP BY MemNo,MemName";
                        //ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
                        txtCreditUNo.Text = (lblCuType.Text + "-" + lblCu.Text);

                    }
                    else
                    {
                        InvalidCUNo();
                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void DisplayMessage()
        {

            string Msg = "";

            string a = "";
            string b = "";

            a = "Generated New Application No.";
            b = string.Format(lblNewAppNo.Text);

            Msg += a + b;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


            //string a = "Generated New Application No.";
            //string b = string.Format(lblNewAppNo.Text);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append(b);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        private void clearinfo()
        {
            ddlAccType.SelectedValue = "-Select-";
            //txtLoanAppDate.Text = string.Empty;
            txtLoanInterestRate.Text = string.Empty;
            txtLoanAppAmount.Text = string.Empty;

            txtLoanInstallmentAmount.Text = string.Empty;
            txtLoanLastInstlAmount.Text = string.Empty;
            txtNoInstallment.Text = string.Empty;
            txtLoanMemNo.Text = string.Empty;
            txtCreditUNo.Text = string.Empty;

            ddlLoanPurpose.SelectedValue = "0";
            ddlLoanCategory.SelectedValue = "0";
            txtSuretyMemNo.Text = string.Empty;
            txtLoanExpDate.Text = string.Empty;

            lblCuName.Text = string.Empty;
            lblMemName.Text = string.Empty;


            if (txtCreditUNo.Text != string.Empty)
            {
                txtCreditUNo.Text = string.Empty;
                //ddlCreditUNo.SelectedValue = "-Select-";
            }

            if (txtLoanMemNo.Text != string.Empty)
            {
                txtLoanMemNo.Text = string.Empty;
                //ddlLoanMemNo.SelectedValue = "-Select-";
            }


        }
        protected void Hideinfo()
        {
            txtNoInstallment.Visible = false;

            txtLoanInstallmentAmount.Visible = false;
            txtLoanLastInstlAmount.Visible = false;

            lblNoInstallment.Visible = false;

            lblLoanInstallmentAmount.Visible = false;
            lblLoanLastInstlAmount.Visible = false;

            lblLoanStatDate.Visible = false;
            txtLoanExpDate.Visible = false;
        }
        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccType.SelectedValue != "-Select-")
            {
                Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    lblTypeCls.Text = Converter.GetString(getDTO.AccTypeClass);
                    lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                    lblAccTypeGuaranty.Text = Converter.GetString(getDTO.AccTypeGuaranty);

                    if (CtrlFlag.Text != "1")
                    {
                        txtCreditUNo.Text = string.Empty;
                        lblCuName.Text = string.Empty;
                        txtLoanMemNo.Text = string.Empty;
                        lblMemName.Text = string.Empty;
                    }

                    Int16 Code = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                    A2ZCSPARAMDTO get1DTO = (A2ZCSPARAMDTO.GetInformation(Code));

                    if (get1DTO.AccType > 0)
                    {
                        txtODPeriod.Text = Converter.GetString(get1DTO.Period);

                        if (txtODPeriod.Text != string.Empty && txtODPeriod.Text != "0")
                        {
                            DateTime Matdate = new DateTime();
                            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtODPeriod.Text));
                            DateTime dt = Converter.GetDateTime(Matdate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtODExpDate.Text = date;
                        }

                    }

                }

                if (lblTypeCls.Text == "5")
                {
                    BtnShare.Visible = false;
                    BtnDeposit.Visible = true;

                    txtNoInstallment.Visible = false;

                    txtLoanInstallmentAmount.Visible = false;
                    txtLoanLastInstlAmount.Visible = false;

                    lblNoInstallment.Visible = false;

                    lblLoanInstallmentAmount.Visible = false;
                    lblLoanLastInstlAmount.Visible = false;

                    lblLoanStatDate.Visible = false;
                    txtLoanExpDate.Visible = false;

                    lblODPeriod.Visible = true;
                    txtODPeriod.Visible = true;
                    lblODExpiryDate.Visible = true;
                    txtODExpDate.Visible = true;


                    txtCreditUNo.Focus();
                }
                else
                {
                    BtnShare.Visible = true;
                    BtnDeposit.Visible = false;

                    txtNoInstallment.Visible = true;

                    txtLoanInstallmentAmount.Visible = true;
                    txtLoanLastInstlAmount.Visible = true;

                    lblNoInstallment.Visible = true;

                    lblLoanInstallmentAmount.Visible = true;
                    lblLoanLastInstlAmount.Visible = true;

                    lblLoanStatDate.Visible = true;
                    txtLoanExpDate.Visible = true;

                    lblODPeriod.Visible = false;
                    txtODPeriod.Visible = false;
                    lblODExpiryDate.Visible = false;
                    txtODExpDate.Visible = false;

                    txtCreditUNo.Focus();
                }
            }
        }

        protected void Hidetrue()
        {
            txtNoInstallment.Visible = true;

            txtLoanInstallmentAmount.Visible = true;
            txtLoanLastInstlAmount.Visible = true;

            lblNoInstallment.Visible = true;

            lblLoanInstallmentAmount.Visible = true;
            lblLoanLastInstlAmount.Visible = true;

            lblLoanStatDate.Visible = true;
            txtLoanExpDate.Visible = true;
        }
        protected void AccountOpenCheck()
        {
            MsgFlag.Text = string.Empty;

            Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
            A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

            if (getDTO.AccTypeCode > 0)
            {
                lblAccFlag.Text = Converter.GetString(getDTO.AccFlag);
                lblchk1Hide.Text = Converter.GetString(getDTO.AccessT1);
                lblchk2Hide.Text = Converter.GetString(getDTO.AccessT2);
                lblchk3Hide.Text = Converter.GetString(getDTO.AccessT3);
            }


            if ((lblCuType.Text == "1" && lblchk1Hide.Text != "1") ||
               (lblCuType.Text == "2" && lblchk2Hide.Text != "1") ||
               (lblCuType.Text == "3" && lblchk3Hide.Text != "1"))
            {
                MsgFlag.Text = "1";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Open an Account');", true);
                return;
            }


            if ((lblAccFlag.Text == "1" || lblAccFlag.Text == "3") && txtLoanMemNo.Text != "0")
            {
                MsgFlag.Text = "1";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Open an Account');", true);
                return;
            }


            if (lblAccFlag.Text == "1" || lblAccFlag.Text == "2")
            {
                string qry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCu.Text + "' AND MemNo = '" + txtLoanMemNo.Text + "' AND AccType = '" + ddlAccType.SelectedValue + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    MsgFlag.Text = "1";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Already Exist');", true);
                    return;
                }

            }



            if (ddlAccType.SelectedValue == "53")
            {
                string qry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCu.Text + "' AND MemNo = '" + txtLoanMemNo.Text + "' AND AccType = '" + ddlAccType.SelectedValue + "' AND AccStatus < 98 AND AccOpenDate > '" + "2017-06-30" + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    MsgFlag.Text = "1";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Account Already Exist');", true);
                    return;
                }
            }



        }
        protected void txtLoanMemNo_TextChanged(object sender, EventArgs e)
        {
            //if (ddlLoanMemNo.SelectedValue == "-Select-")
            //{
            //    txtLoanMemNo.Focus();

            //}

            if (txtLoanMemNo.Text != string.Empty)
            {
                //string c = "";
                //int a = txtCreditUNo.Text.Length;

                //string b = txtCreditUNo.Text;
                //c = b.Substring(0, 1);
                //int re = Converter.GetSmallInteger(c);
                //int dd = a - 1;
                //string d = b.Substring(1, dd);
                //int re1 = Converter.GetSmallInteger(d);


                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetSmallInteger(lblCu.Text);

                int MemNumber = Converter.GetInteger(txtLoanMemNo.Text);

                int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtLoanMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    lblMemName.Text = Converter.GetString(getDTO.MemberName);
                    AccountOpenCheck();
                    if (MsgFlag.Text == "1")
                    {
                        txtLoanMemNo.Text = string.Empty;
                        lblMemName.Text = string.Empty;
                        txtLoanMemNo.Focus();
                        return;
                    }
                    else
                    {
                        //  ddlLoanMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        txtLoanAppAmount.Focus();
                    }
                }
                else
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Member No does not exist in file');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                    //}
                    txtLoanMemNo.Text = string.Empty;
                    txtLoanMemNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Depositor No does not exist in file');", true);
                    return;
                }
            }

        }


        protected void txtNoInstallment_TextChanged(object sender, EventArgs e)
        {
            Int16 RoundFlag = 0;
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            double b = Converter.GetDouble(txtNoInstallment.Text);
            double c = a / b;



            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + ddlAccType.SelectedValue + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                if (RoundFlag == 1)
                {
                    c = Math.Round(c);

                }

                if (RoundFlag == 2)
                {
                    c = Math.Ceiling(c);
                }
                //if (RoundFlag == 3)
                //{
                //    c = c;
                //}
            }

            txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

            double d = Math.Abs((c * (b - 1)) - a);

            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));

            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtNoInstallment.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtLoanExpDate.Text = date;

            txtLoanInterestRate.Focus();


        }

        //protected void txtLoanAppAmount_TextChanged(object sender, EventArgs e)
        //{
        //    A2ZLOANDTO getDTO = new A2ZLOANDTO();
        //    getDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
        //    txtLoanAppAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LoanApplicationAmount));
        //    txtNoInstallment.Focus();
        //}


        protected void txtLoanAppAmount_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            txtLoanAppAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", a));
            txtNoInstallment.Focus();

            if (txtNoInstallment.Text != string.Empty && lblTypeCls.Text == "6")
            {
                txtNoInstallment_TextChanged(this, EventArgs.Empty);
            }
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove();
            Response.Redirect("A2ZERPModule.aspx");
        }



        protected void BtnLoanApplication_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = false;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = true;

            if (CtrlFlag.Text == "1")
            {
                ddlAccType_SelectedIndexChanged(this, EventArgs.Empty);
            }



        }
        ////--------------------End Loan Application---------------------------------
        #endregion

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblTypeCls.Text == "5" && (lblTotalResult.Text == string.Empty || lblTotalResult.Text == "00.00"))
                {
                    GuarantorMSG();
                    return;
                }



                if (txtLoanAppAmount.Text == string.Empty)
                {
                    txtLoanAppAmount.Focus();
                    return;

                }



                if (txtLoanInterestRate.Text == string.Empty)
                {
                    txtLoanInterestRate.Focus();
                    return;
                }


                if (txtLoanFees.Text != string.Empty)
                {
                    if (txtVchNo.Text == string.Empty)
                    {
                        VchInputMSG();
                        txtVchNo.Focus();
                        return;
                    }
                    else
                    {
                        TrnVchDeplicate();

                    }
                }


                if (txtCreditUNo.Text != string.Empty && txtLoanMemNo.Text != string.Empty && ddlAccType.SelectedValue != "-Select-")
                {
                    A2ZLOANDTO objDTO = new A2ZLOANDTO();
                    A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(4));
                    objDTO.LoanApplicationNo = Converter.GetInteger(getDTO.CtrlRecLastNo);
                    lblNewAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);
                    if (txtLoanAppDate.Text != string.Empty)
                    {
                        DateTime apdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objDTO.LoanApplicationDate = apdate;
                    }
                    else
                    {
                        objDTO.LoanApplicationDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    }
                    objDTO.LoanAccountType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                    objDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
                    objDTO.CuType = Converter.GetInteger(lblCuType.Text);
                    objDTO.CuNo = Converter.GetInteger(lblCu.Text);
                    objDTO.LoanMemberNo = Converter.GetInteger(txtLoanMemNo.Text);
                    objDTO.LoanApplicationAmount = Converter.GetDecimal(txtLoanAppAmount.Text);
                    objDTO.LoanInterestRate = Converter.GetDecimal(txtLoanInterestRate.Text);
                    //objDTO.LoanGracePeriod = Converter.GetSmallInteger(txtLoanGracePeriod.Text);
                    objDTO.LoanInstallmentAmount = Converter.GetDecimal(txtLoanInstallmentAmount.Text);
                    objDTO.LoanLastInstallmentAmount = Converter.GetDecimal(txtLoanLastInstlAmount.Text);
                    objDTO.LoanNoInstallment = Converter.GetInteger(txtNoInstallment.Text);
                    objDTO.AccPeriod = Converter.GetInteger(txtODPeriod.Text);

                    objDTO.LoanTotGuarantorAmt = Converter.GetDecimal(lblTotalResult.Text);



                    //if (txtLoanFirstInstlDate.Text != string.Empty)
                    //{
                    //    DateTime FInstldate = DateTime.ParseExact(txtLoanFirstInstlDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //    objDTO.LoanFirstInstallmentdate = FInstldate;
                    //}
                    //else
                    //{
                    //    objDTO.LoanFirstInstallmentdate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                    //}

                    objDTO.LoanPurpose = Converter.GetSmallInteger(ddlLoanPurpose.SelectedValue);
                    objDTO.LoanCategory = Converter.GetSmallInteger(ddlLoanCategory.SelectedValue);
                    objDTO.LoanSuretyMemberNo = Converter.GetInteger(txtSuretyMemNo.Text);

                    if (lblTypeCls.Text == "5")
                    {
                        if (txtODExpDate.Text != string.Empty)
                        {
                            DateTime Expdate = DateTime.ParseExact(txtODExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            objDTO.LoanExpDate = Expdate;
                        }
                        else
                        {
                            objDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                        }
                    }


                    if (lblTypeCls.Text == "6")
                    {
                        if (txtLoanExpDate.Text != string.Empty)
                        {
                            DateTime Expdate = DateTime.ParseExact(txtLoanExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            objDTO.LoanExpDate = Expdate;
                        }
                        else
                        {
                            objDTO.LoanExpDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
                        }
                    }

                    objDTO.FromCashCode = Converter.GetInteger(lblCashCode.Text);

                    objDTO.LoanProcFlag = 11;


                    objDTO.InputBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    objDTO.InputByDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);

                    int roweffect = A2ZLOANDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        UpdateLoanFeesTrans();


                        // A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(4));
                        lblLastAppNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                        clearinfo();
                        SubmitDepositData();
                        SubmitShareData();
                        SubmitPropertyData();
                        lblTotalResult.Text = "0";

                        txtTotalAmt.Text = "0";
                        lblShareTotalAmt.Text = "0";
                        lblSumProperty.Text = "0";

                        TruncateWF();
                        DisplayMessage();


                    }

                }
                else
                {
                    InvalidInputMSG();
                    ddlAccType.Focus();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                //throw ex;

            }
        }
        protected void BtnDeposit_Click(object sender, EventArgs e)
        {
            txtCrUNo.Focus();
            pnlDeposit.Visible = true;
            pnlProperty.Visible = false;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = false;

            Session["SpnlShare"] = string.Empty;
        }

        protected void BtnShare_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = false;
            pnlShare.Visible = true;
            pnlLoanApplication.Visible = false;
            Session["SpnlDeposit"] = string.Empty;

            txtShareCuNo.Focus();
        }

        protected void BtnProperty_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = true;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = false;
            txtSerialNo.Focus();
        }

        #region Deposit Gaurantor
        //--------------Deposit Gaurantor---------------------------------
        protected void txtCrUNo_TextChanged(object sender, EventArgs e)
        {

            if (txtCrUNo.Text == string.Empty)
            {
                txtDepositMemNo.Text = string.Empty;
                txtAccType.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtCrUNo.Focus();
                return;
            }

            string c = "";
            int a = txtCrUNo.Text.Length;

            string b = txtCrUNo.Text;
            c = b.Substring(0, 1);
            int re = Converter.GetSmallInteger(c);
            int dd = a - 1;
            string d = b.Substring(1, dd);
            int re1 = Converter.GetSmallInteger(d);

            Int16 CuType = Converter.GetSmallInteger(re);
            int CNo = Converter.GetSmallInteger(re1);
            lblDepositCuType.Text = Converter.GetString(CuType);
            lblDepositCuNo.Text = Converter.GetString(CNo);

            A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
            if (objDTO.CreditUnionNo > 0)
            {
                txtCrUNo.Text = Converter.GetString(CuType + "-" + CNo);
                lblCrName.Text = Converter.GetString(objDTO.CreditUnionName);

                txtDepositMemNo.Text = string.Empty;
                txtAccType.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtLionAmt.Text = string.Empty;
                txtTotalLienAmt.Text = string.Empty;
                txtLedgerBalance.Text = string.Empty;

                txtDepositMemNo.Focus();
            }
            else
            {
                InvalidCUNo();
                txtCrUNo.Focus();
            }



        }

        protected void DepositMemNo_TextChanged(object sender, EventArgs e)
        {
            Int16 CuType = Converter.GetSmallInteger(lblDepositCuType.Text);
            int CuNo = Converter.GetInteger(lblDepositCuNo.Text);
            int MemNo = Converter.GetInteger(txtDepositMemNo.Text);
            A2ZMEMBERDTO ObjDTO = (A2ZMEMBERDTO.GetInformation(CuType, CuNo, MemNo));
            if (ObjDTO.NoRecord > 0)
            {
                txtDepositMemNo.Text = Converter.GetString(ObjDTO.MemberNo);
                lblMemberName.Text = Converter.GetString(ObjDTO.MemberName);

                txtAccType.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtLionAmt.Text = string.Empty;
                txtTotalLienAmt.Text = string.Empty;
                txtLedgerBalance.Text = string.Empty;

                txtAccType.Focus();
            }
            else
            {
                txtDepositMemNo.Text = string.Empty;
                txtDepositMemNo.Focus();
                InvalidMemberNo();
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
            A2ZACCTYPEDTO ObjDTO = (A2ZACCTYPEDTO.GetInformation(AccType));
            if (ObjDTO.AccTypeCode > 0)
            {
                txtAccType.Text = Converter.GetString(ObjDTO.AccTypeCode);
                lblAccountTypeName.Text = Converter.GetString(ObjDTO.AccTypeDescription);

                if (txtAccType.Text != lblAccTypeGuaranty.Text)
                {
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                    return;
                }
                else
                {
                    txtAccNo.Text = string.Empty;
                    txtLionAmt.Text = string.Empty;
                    txtTotalLienAmt.Text = string.Empty;
                    txtLedgerBalance.Text = string.Empty;

                    txtAccNo.Focus();
                }
            }
            else
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            GetAccInfo();



            //Int16 CuType = Converter.GetSmallInteger(lblDepositCuType.Text);
            //int CuNo = Converter.GetInteger(lblDepositCuNo.Text);
            //int MemNo = Converter.GetInteger(txtDepositMemNo.Text);
            //Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
            //Int64 AccNo = Converter.GetLong(txtAccNo.Text);
            //A2ZACCOUNTDTO ObjDTO = (A2ZACCOUNTDTO.GetInformation(AccType, AccNo, CuType, CuNo, MemNo));
            //if (ObjDTO.a > 0)
            //{
            //    txtAccNo.Text = Converter.GetString(ObjDTO.AccNo);
            //    txtLedgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccBalance));
            //    txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccLienAmt));
            //    lblStatus.Text = Converter.GetString(ObjDTO.AccStatus);
            //    if (lblStatus.Text == "99")
            //    {
            //        InvalidAccMSG();
            //        txtAccNo.Focus();
            //    }
            //    else
            //    {
            //        txtLionAmt.Focus();
            //    }
            //}


        }
        protected void ClearDeposit()
        {
            txtCrUNo.Text = string.Empty;
            txtDepositMemNo.Text = string.Empty;
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtLionAmt.Text = string.Empty;
            txtLedgerBalance.Text = string.Empty;
            lblCrName.Text = string.Empty;
            lblMemberName.Text = string.Empty;
            lblAccountTypeName.Text = string.Empty;
            txtTotalLienAmt.Text = string.Empty;
        }

        private void gvInfo()
        {

            string sqlquery3 = "SELECT Id, lTrim(str(CuType)+'-' +lTrim(str(CuNo))) As CuNo,MemNo,AccType,AccNo,AccAmount FROM WFA2ZACGUAR Where UserId='" + hdnID.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void SumValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {

                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[5].Text));


            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }


        protected void BtnAddDeposit_Click(object sender, EventArgs e)
        {

            if (txtCrUNo.Text == string.Empty || txtDepositMemNo.Text == string.Empty || txtAccType.Text == string.Empty || txtAccNo.Text == string.Empty || txtLionAmt.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtCrUNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);
                return;

            }
            try
            {
                string qry4 = "SELECT LoanApplicationNo, AccAmount FROM WFA2ZACGUAR WHERE  LoanApplicationNo='" + lblApplicationNo.Text + "' AND CuType='" + lblDepositCuType.Text + "' AND CuNo='" + lblDepositCuNo.Text + "' AND MemNo='" + txtDepositMemNo.Text + "' AND AccType='" + txtAccType.Text + "' AND AccNo='" + txtAccNo.Text + "'";
                DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                if (dt4.Rows.Count > 0)
                {
                    foreach (DataRow dr2 in dt4.Rows)
                    {
                        var LApp = dr2["LoanApplicationNo"].ToString();
                        var AAmount = dr2["AccAmount"].ToString();
                        decimal LAmount = Converter.GetDecimal(AAmount);
                        decimal IAmount = Converter.GetDecimal(txtLionAmt.Text);
                        LAmount = (LAmount + IAmount);
                        string strQuery = "UPDATE WFA2ZACGUAR SET  AccAmount = '" + LAmount + "' WHERE LoanApplicationNo='" + lblApplicationNo.Text + "' AND CuType='" + lblDepositCuType.Text + "' AND CuNo='" + lblDepositCuNo.Text + "' AND MemNo='" + txtDepositMemNo.Text + "' AND AccType='" + txtAccType.Text + "' AND AccNo='" + txtAccNo.Text + "'";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                }
                else
                {
                    string statment = "INSERT INTO  WFA2ZACGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId) VALUES('" + lblApplicationNo.Text + "','" + lblDepositCuType.Text + "','" + lblDepositCuNo.Text + "','" + txtDepositMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtLionAmt.Text + "',1,0, '" + hdnID.Text + "')";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                }

                gvInfo();
                SumValue();
                TotalGuarantor();
                ClearDeposit();
                txtCrUNo.Focus();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void txtLionAmt_TextChanged(object sender, EventArgs e)
        {
            double amt = Math.Abs(Converter.GetDouble(txtLionAmt.Text));
            txtLionAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
            double Ledgeramt = Converter.GetDouble(txtLedgerBalance.Text);
            double TotalLienAmt = Converter.GetDouble(txtTotalLienAmt.Text);
            double result = (amt + TotalLienAmt);
            if (result > Ledgeramt)
            {
                CheckLedgerBalValidation();
                txtLionAmt.Text = string.Empty;
                txtLionAmt.Focus();
            }
            else
            {
                BtnAddDeposit.Focus();
            }

        }

        private void InvalidInputMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Fields Input Not Completed');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fields Input Not Completed');", true);
            return;
        }

        private void InvalidAccMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account is not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account is not Active');", true);

            return;
        }

        private void DuplicateAccNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account is not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Duplicate Account No.');", true);

            return;
        }
        private void CheckLedgerBalValidation()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Lien Amount+Total Lien Amount) Should be Less than of Ledger Balance');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Lien Amount+Total Lien Amount) Should be Less than of Ledger Balance');", true);

            return;
        }

        private void InvalidCUNo()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);

            return;
        }

        private void InvalidMemberNo()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Depositor No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);

            return;
        }

        private void SubmitDepositData()
        {
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowMode,UserId From WFA2ZACGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);
                int RowMode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                string statment = "INSERT INTO  A2ZACGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) VALUES('" + lblNewAppNo.Text + "','" + Cutype + "','" + Cuno + "','" + Memno + "','" + AccType + "','" + AccNo + "','" + LienAmt + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccLienAmt From A2ZACCOUNT WHERE AccType='" + AccType + "' and AccNo='" + AccNo + "' and CuType='" + Cutype + "' and CuNo='" + Cuno + "' and MemNo='" + Memno + "'", "A2ZCSMCUS");
                txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccLienAmt"])); double TotalLienAmt = Converter.GetDouble(txtTotalLienAmt.Text);
                double result = (LienAmt + TotalLienAmt);

                string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = '50', AccLienAmt= '" + result + "' WHERE CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                gvDetailInfo.Visible = false;
                lblTotalAmt.Visible = false;
                txtTotalAmt.Visible = false;

            }

        }

        //---------------------End Of Deposit Garantor----------------------------
        #endregion


        #region Share Gaurantor
        //----------------Share Gaurantor------------------------------------------
        protected void txtShareCuNo_TextChanged(object sender, EventArgs e)
        {
            if (txtShareCuNo.Text != string.Empty)
            {

                string c = "";
                int a = txtShareCuNo.Text.Length;

                string b = txtShareCuNo.Text;
                c = b.Substring(0, 1);
                int re = Converter.GetSmallInteger(c);
                int dd = a - 1;
                string d = b.Substring(1, dd);
                int re1 = Converter.GetSmallInteger(d);

                Int16 CuType = Converter.GetSmallInteger(re);
                int CNo = Converter.GetSmallInteger(re1);
                lblShareCType.Text = Converter.GetString(CuType);
                lblShareCNo.Text = Converter.GetString(CNo);

                A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                if (objDTO.CreditUnionNo > 0)
                {
                    txtShareCuNo.Text = Converter.GetString(CuType + "-" + CNo);
                    txtShareCuName.Text = Converter.GetString(objDTO.CreditUnionName);
                    //Int16 AccType = Converter.GetSmallInteger(11);
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccNo,AccBalance,AccStatus From A2ZACCOUNT WHERE AccType=11 and CuType='" + lblShareCType.Text + "' and CuNo='" + lblShareCNo.Text + "'", "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        hdnAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                        lblStatus.Text = Converter.GetString(dt.Rows[0]["AccStatus"]);
                        txtShareAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccBalance"]));
                        if (lblStatus.Text != "1")
                        {
                            InvalidAccMSG();
                            txtShareCuNo.Focus();
                        }
                        else
                        {
                            DuplicateShareAccSelect();
                            if (CtrlMsgFlag.Text == "1")
                            {
                                return;
                            }
                            BtnAddShare.Focus();
                        }
                    }

                }
            }

        }

        //protected void txtShareAccType_TextChanged(object sender, EventArgs e)
        //{
        //    Int16 AccType = Converter.GetSmallInteger(txtShareAccType.Text);
        //    if (AccType == 11)
        //    {
        //        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccBalance From A2ZACCOUNT WHERE AccType=11 and CuType='" + lblShareCType.Text + "' and CuNo='" + lblShareCNo.Text + "'", "A2ZCSMCUS");
        //        if (dt.Rows.Count > 0)
        //        {
        //            txtShareAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccBalance"]));
        //            BtnAddShare.Focus();
        //        }
        //    }
        //    else
        //    {
        //        String csname1 = "PopupScript";
        //        Type cstype = GetType();
        //        ClientScriptManager cs = Page.ClientScript;

        //        if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //        {
        //            //String cstext1 = "confirm('Records Already Added');";
        //            String cstext1 = "alert('Only Share(11) Account Allowed');";
        //            cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //        }
        //        txtShareAmount.Text = string.Empty;
        //        txtShareAccType.Text = string.Empty;
        //        txtShareAccType.Focus();
        //        return;
        //    }

        //}

        private void gvShareInfo()
        {

            string sqlquery3 = "SELECT Id,lTrim(str(CuType)+'-' +lTrim(str(CuNo))) As CuNo,MemNo,AccType,AccNo,AccAmount FROM WFA2ZSHGUAR Where UserId='" + hdnID.Text + "'";
            gvShareDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvShareDetails, "A2ZCSMCUS");
        }
        protected void SumShareValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvShareDetails.Rows.Count; ++i)
            {

                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvShareDetails.Rows[i].Cells[5].Text));


            }
            lbltotalshare.Visible = true;
            lblShareTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }
        protected void ClearShare()
        {
            txtShareAmount.Text = string.Empty;
            txtShareAccType.Text = string.Empty;
            txtShareCuNo.Text = string.Empty;
            txtShareCuName.Text = string.Empty;
        }



        protected void BtnAddShare_Click(object sender, EventArgs e)
        {
            if (txtShareCuNo.Text == string.Empty || txtShareAmount.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtShareCuNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);

                return;
            }

            try
            {
                string statment = "INSERT INTO  WFA2ZSHGUAR (LoanApplicationNo ,CuType, CuNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId) VALUES('" + lblApplicationNo.Text + "','" + lblShareCType.Text + "','" + lblShareCNo.Text + "','" + 11 + "','" + hdnAccNo.Text + "','" + txtShareAmount.Text + "',1,0,'" + hdnID.Text + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
                gvShareInfo();
                SumShareValue();
                TotalGuarantor();
                ClearShare();
                txtShareCuNo.Focus();


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        private void SubmitShareData()
        {
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowMode,UserId From WFA2ZSHGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);
                int RowMode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                string statment = "INSERT INTO  A2ZSHGUAR (LoanApplicationNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) VALUES('" + lblNewAppNo.Text + "','" + Cutype + "','" + Cuno + "','" + Memno + "','" + AccType + "','" + AccNo + "','" + LienAmt + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT AccLienAmt From A2ZACCOUNT WHERE AccType='" + AccType + "' and AccNo='" + AccNo + "' and CuType='" + Cutype + "' and CuNo='" + Cuno + "' and MemNo='" + Memno + "'", "A2ZCSMCUS");
                txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", dt.Rows[0]["AccLienAmt"])); double TotalLienAmt = Converter.GetDouble(txtTotalLienAmt.Text);
                double result = (LienAmt + TotalLienAmt);

                string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = '50', AccLienAmt= '" + result + "' WHERE CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                gvShareDetails.Visible = false;
                lbltotalshare.Visible = false;
                lblShareTotalAmt.Visible = false;



            }

        }



        //protected void Delete()
        //{

        //    try
        //    {
        //        string sqlQuery = string.Empty;
        //        int rowEffect;

        //        sqlQuery = @"DELETE  FROM A2ZACGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
        //        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

        //        sqlQuery = @"DELETE  FROM A2ZSHGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
        //        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

        //        sqlQuery = @"DELETE  FROM A2ZPRGUAR WHERE  LoanApplicationNo = '" + txtLoanAppNo.Text + "'";
        //        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //---------------End of Share Garantor--------------------------------------------

        #endregion

        #region Property Gaurantor

        //-------------------Property Gaurantor----------------------------------------------------
        private void gvPropertyInfo()
        {

            string sqlquery3 = "SELECT Id,LoanApplicationNo, PrSRL,PrName,FileNo,PrAmount,PrDesc FROM WFA2ZPRGUAR Where UserId='" + hdnID.Text + "'";
            gvPropertyDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvPropertyDetails, "A2ZCSMCUS");
        }

        protected void SumPropertyValue()
        {
            Decimal sum = 0;


            for (int i = 0; i < gvPropertyDetails.Rows.Count; ++i)
            {

                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvPropertyDetails.Rows[i].Cells[5].Text));


            }
            lblTotalProprty.Visible = true;
            lblSumProperty.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));

        }
        protected void ClearProperty()
        {
            txtSerialNo.Text = string.Empty;
            txtNameProperty.Text = string.Empty;
            txtFileNo.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtProprertyAmt.Text = string.Empty;
        }

        protected void BtnAddProperty_Click(object sender, EventArgs e)
        {
            if (txtSerialNo.Text == string.Empty || txtNameProperty.Text == string.Empty || txtFileNo.Text == string.Empty || txtDescription.Text == string.Empty || txtProprertyAmt.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    //String cstext1 = "confirm('Records Already Added');";
                //    String cstext1 = "alert('Should Be filled up Empty Fields');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtSerialNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Should Be filled up Empty Fields');", true);

                return;
            }

            string statment = "INSERT INTO  WFA2ZPRGUAR (LoanApplicationNo, PrSRL,PrName,FileNo,PrDesc,PrAmount,RowType,RowMode,UserId) VALUES('" + lblApplicationNo.Text + "','" + txtSerialNo.Text + "','" + txtNameProperty.Text + "','" + txtFileNo.Text + "','" + txtDescription.Text + "','" + txtProprertyAmt.Text + "',1,0, '" + hdnID.Text + "')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            gvPropertyInfo();
            SumPropertyValue();
            TotalGuarantor();
            ClearProperty();
            txtSerialNo.Focus();
        }

        private void SubmitPropertyData()
        {
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select PrSRL,PrName,FileNo,PrDesc,PrAmount From WFA2ZPRGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                int PrSRL = Converter.GetInteger(dt1.Rows[i]["PrSRL"]);
                string PrName = Converter.GetString(dt1.Rows[i]["PrName"]);
                string FileNo = Converter.GetString(dt1.Rows[i]["FileNo"]);
                string PrDesc = Converter.GetString(dt1.Rows[i]["PrDesc"]);
                double PrAmount = Converter.GetDouble(dt1.Rows[i]["PrAmount"]);


                string statment = "INSERT INTO  A2ZPRGUAR (LoanApplicationNo,PrSRL,PrName,FileNo,PrDesc,PrAmount) VALUES('" + lblNewAppNo.Text + "','" + PrSRL + "','" + PrName + "','" + FileNo + "','" + PrDesc + "','" + PrAmount + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                gvPropertyDetails.Visible = false;
                lblTotalProprty.Visible = false;
                lblSumProperty.Visible = false;

            }

        }

        //protected void txtProprertyAmt_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtProprertyAmt.Text);
        //    txtProprertyAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //    BtnAddProperty.Focus();
        //}

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvShareDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvPropertyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDetailInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvDetailInfo.Rows[e.RowIndex];

                Label IdNo = (Label)gvDetailInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;

                sqlQuery = @"DELETE  FROM WFA2ZACGUAR WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                gvInfo();
                SumValue();
                TotalGuarantor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvShareDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Label IdNo = (Label)gvShareDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZSHGUAR WHERE   Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                gvShareInfo();
                SumShareValue();
                TotalGuarantor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPropertyDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Label IdNo = (Label)gvPropertyDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZPRGUAR WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                gvPropertyInfo();
                SumPropertyValue();
                TotalGuarantor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNameProperty.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtNameProperty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtFileNo.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtFileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDescription.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtProprertyAmt.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------End Of Property Garantor---------------------------------------
        #endregion

        private void TotalGuarantor()
        {
            double totalDeposit = Converter.GetDouble(txtTotalAmt.Text);
            double totalShare = Converter.GetDouble(lblShareTotalAmt.Text);
            double totalProperty = Converter.GetDouble(lblSumProperty.Text);
            double Garantor = (totalDeposit + totalShare + totalProperty);
            lblTotalResult.Text = Converter.GetString(String.Format("{0:0,0.00}", Garantor));
        }



        protected void txtLoanInstallmentAmount_TextChanged(object sender, EventArgs e)
        {

            double a = Converter.GetDouble(txtLoanAppAmount.Text);
            double b = Converter.GetDouble(txtNoInstallment.Text);
            double c = Converter.GetDouble(txtLoanInstallmentAmount.Text);

            double d = Math.Abs((c * (b - 1)) - a);

            txtLoanInstallmentAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));
            txtLoanLastInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            Session["TrnDate"] = txtLoanAppDate.Text;
            Session["LAcType"] = ddlAccType.SelectedValue;
            Session["Module"] = lblModule.Text;
            Session["ExFlag"] = "2";
            Session["NFlag"] = "1";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

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
        protected void VchInputMSG()
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

        protected void GuarantorMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Voucher No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Guarantor Information');", true);
            return;
        }

        protected void TrnVchDeplicate()
        {
            DateTime opdate = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "' and TrnDate ='" + opdate + "'";
            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                DuplicateVchMSG();
                return;
            }
        }

        private void UpdateLoanFeesTrans()
        {
            try
            {
                int GLCode = Converter.GetInteger(lblCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVoucherNo.Text = "C" + lblCashCode.Text + "-" + getDTO.RecLastNo;


                var prm = new object[10];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = hdnID.Text;
                prm[2] = txtVchNo.Text.Trim();
                prm[3] = CtrlVoucherNo.Text;
                prm[4] = lblCashCode.Text;
                prm[5] = txtLoanFees.Text;
                prm[6] = lblCuType.Text;
                prm[7] = lblCu.Text;
                prm[8] = txtLoanMemNo.Text;
                prm[9] = lblMemName.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddLoanFeesTransaction", prm, "A2ZCSMCUS"));



                if (result == 0)
                {
                    PrintTrnVoucher();

                    UpdateBackUpStat();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateLoanFeesTran Problem');</script>");
                //throw ex;
            }
        }

        // ------------ EXIT PROGRAMS ---------------------------

        protected void PrintTrnVoucher()
        {
            try
            {

                lblFuncTitle.Text = "DEPOSIT";
                lblTrnTypeTitle.Text = "CASH";


                DateTime Pdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, txtLoanMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblMemName.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, hdnID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, hdnIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTransactionVch");

                Response.Redirect("ReportServer.aspx", false);

                DisplayMessage();


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlDeposit"] = "1";
            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "3";

            Session["SSPflag"] = "1";
            Session["SAccTypeGuaranty"] = lblAccTypeGuaranty.Text;

            Session["Module"] = lblModule.Text;

            Session["TrnDate"] = txtLoanAppDate.Text;

            Session["StxtCreditUNo"] = txtCreditUNo.Text;

            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCu"] = lblCu.Text;
            Session["StxtLoanMemNo"] = txtLoanMemNo.Text;

            Session["SlblCuName"] = lblCuName.Text;
            Session["SlblMemName"] = lblMemName.Text;


            Session["LAcType"] = ddlAccType.SelectedValue;

            Session["SlblTypeCls"] = lblTypeCls.Text;


            Session["StxtLoanAppAmount"] = txtLoanAppAmount.Text;
            Session["StxtNoInstallment"] = txtNoInstallment.Text;
            Session["StxtLoanInterestRate"] = txtLoanInterestRate.Text;
            Session["StxtLoanInstallmentAmount"] = txtLoanInstallmentAmount.Text;
            Session["StxtLoanLastInstlAmount"] = txtLoanLastInstlAmount.Text;
            Session["StxtLoanExpDate"] = txtLoanExpDate.Text;

            Session["StxtODPeriod"] = txtODPeriod.Text;
            Session["StxtODExpDate"] = txtODExpDate.Text;


            Session["SddlLoanCategory"] = ddlLoanCategory.SelectedValue;
            Session["SddlLoanPurpose"] = ddlLoanPurpose.SelectedValue;
            Session["StxtLoanFees"] = txtLoanFees.Text;
            Session["StxtVchNo"] = txtVchNo.Text;

            Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;



            if (lblModule.Text == "4")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
     "click", @"<script>window.open('CSGetStaffAccountNo.aspx','_blank');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSGetAccountNo.aspx','_blank');</script>", false);
            }
        }


        protected void btnSSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlShare"] = "1";
            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "3";

            Session["Module"] = lblModule.Text;

            Session["TrnDate"] = txtLoanAppDate.Text;

            Session["StxtCreditUNo"] = txtCreditUNo.Text;

            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCu"] = lblCu.Text;
            Session["StxtLoanMemNo"] = txtLoanMemNo.Text;

            Session["SlblCuName"] = lblCuName.Text;
            Session["SlblMemName"] = lblMemName.Text;

            Session["LAcType"] = ddlAccType.SelectedValue;

            Session["SlblTypeCls"] = lblTypeCls.Text;


            Session["StxtLoanAppAmount"] = txtLoanAppAmount.Text;
            Session["StxtNoInstallment"] = txtNoInstallment.Text;
            Session["StxtLoanInterestRate"] = txtLoanInterestRate.Text;
            Session["StxtLoanInstallmentAmount"] = txtLoanInstallmentAmount.Text;
            Session["StxtLoanLastInstlAmount"] = txtLoanLastInstlAmount.Text;
            Session["StxtLoanExpDate"] = txtLoanExpDate.Text;

            Session["StxtODPeriod"] = txtODPeriod.Text;
            Session["StxtODExpDate"] = txtODExpDate.Text;



            Session["SddlLoanCategory"] = ddlLoanCategory.SelectedValue;
            Session["SddlLoanPurpose"] = ddlLoanPurpose.SelectedValue;
            Session["StxtLoanFees"] = txtLoanFees.Text;
            Session["StxtVchNo"] = txtVchNo.Text;


            Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;


            Session["ExFlag"] = "2";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);


        }


        protected void DuplicateAccountSelect()
        {
            CtrlMsgFlag.Text = "0";

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {

                Int64 lblAccNo;

                lblAccNo = Convert.ToInt64(gvDetailInfo.Rows[i].Cells[4].Text);

                //Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("AccNo");


                string AccNo = Converter.GetString(lblAccNo);

                string AccNum = Converter.GetString(txtAccNo.Text);

                if (AccNum == AccNo)
                {
                    CtrlMsgFlag.Text = "1";

                    txtAccNo.Text = string.Empty;
                    txtLionAmt.Text = string.Empty;
                    txtTotalLienAmt.Text = string.Empty;
                    txtLedgerBalance.Text = string.Empty;
                    txtAccNo.Focus();
                    DuplicateAccNoMSG();
                    return;
                }
            }
        }

        protected void DuplicateShareAccSelect()
        {
            CtrlMsgFlag.Text = "0";

            for (int i = 0; i < gvShareDetails.Rows.Count; ++i)
            {

                Int64 lblAccNo;

                lblAccNo = Convert.ToInt64(gvShareDetails.Rows[i].Cells[4].Text);

                //Label lblAccNo = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("AccNo");


                string AccNo = Converter.GetString(lblAccNo);

                string AccNum = Converter.GetString(hdnAccNo.Text);

                if (AccNum == AccNo)
                {
                    CtrlMsgFlag.Text = "1";

                    hdnAccNo.Text = string.Empty;
                    txtShareCuNo.Text = string.Empty;
                    txtShareCuName.Text = string.Empty;
                    txtShareAccType.Text = string.Empty;
                    txtShareAmount.Text = string.Empty;
                    txtShareCuNo.Focus();
                    DuplicateAccNoMSG();
                    return;
                }
            }
        }


        public void GetAccInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    //InvalidAccountNoMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    DuplicateAccountSelect();
                    if (CtrlMsgFlag.Text == "1")
                    {
                        return;
                    }

                    txtAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                    hdnAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                    txtLedgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    lblStatus.Text = Converter.GetString(accgetDTO.AccStatus);
                    if (lblStatus.Text == "99")
                    {
                        InvalidAccMSG();
                        txtAccNo.Focus();
                    }
                    else
                    {
                        txtLionAmt.Focus();
                    }



                    lblDepositCuType.Text = Converter.GetString(accgetDTO.CuType);

                    lblDepositCuNo.Text = Converter.GetString(accgetDTO.CuNo);


                    txtCrUNo.Text = (lblDepositCuType.Text + "-" + lblDepositCuNo.Text);



                    txtDepositMemNo.Text = Converter.GetString(accgetDTO.MemberNo);


                    txtAccType.Text = Converter.GetString(accgetDTO.AccType);



                    Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                    if (get3DTO.AccTypeCode > 0)
                    {
                        lblAccountTypeName.Text = Converter.GetString(get3DTO.AccTypeDescription);

                        if (txtAccType.Text != lblAccTypeGuaranty.Text)
                        {
                            txtAccNo.Text = string.Empty;
                            txtAccNo.Focus();
                            return;
                        }

                    }


                    Int16 CType = Converter.GetSmallInteger(lblDepositCuType.Text);
                    int CNo = Converter.GetInteger(lblDepositCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCrName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblDepositCuType.Text);
                    int CUNo = Converter.GetInteger(lblDepositCuNo.Text);
                    int MNo = Converter.GetInteger(txtDepositMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemberName.Text = Converter.GetString(get6DTO.MemberName);
                    }
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


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidInputDate()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Input Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void txtLoanAppDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtLoanAppDate.Text.Length.ToString();
            if (date != "10")
            {
                InvalidInputDate();
                txtLoanAppDate.Text = lblProcDate.Text;
                txtLoanAppDate.Focus();
                return;
            }

            DateTime opdate1 = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Month1 = opdate1.Month;
            int Month2 = opdate2.Month;

            if (opdate1 > opdate2 || Month1 != Month2)
            {
                InvalidDateMSG();
                txtLoanAppDate.Text = lblProcDate.Text;
                txtLoanAppDate.Focus();
                return;
            }


        }

        protected void txtODPeriod_TextChanged(object sender, EventArgs e)
        {
            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(txtLoanAppDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtODPeriod.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            txtODExpDate.Text = date;
        }



    }
}
