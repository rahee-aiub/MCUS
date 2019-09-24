using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSIncreaseSanctionAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcDate.Text = date;


                AccTypedropdown1();

                ItxtCreditUNo.Focus();
                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                string Ctrflag = (string)Session["flag"];
                lblCtrlFlag.Text = Ctrflag;

                string viewflag = (string)Session["ViewFlag"];


                BtnShare.Visible = false;


                pnlDeposit.Visible = false;
                pnlProperty.Visible = false;
                pnlShare.Visible = false;
                lblTotalAmt.Visible = false;
                lbltotalshare.Visible = false;
                lblTotalProprty.Visible = false;
                txtShareAmount.ReadOnly = true;

                if (lblCtrlFlag.Text == "1" && viewflag == "1")
                {
                    string CuNo = (string)Session["RCreditUNo"];
                    ItxtCreditUNo.Text = CuNo;
                    string MemNo = (string)Session["RMemNo"];
                    ItxtMemNo.Text = MemNo;
                    ItxtCreditUNo_TextChanged(this, EventArgs.Empty);
                    ItxtMemNo_TextChanged(this, EventArgs.Empty);
                    SessionRemove();
                }

                else
                    if (lblCtrlFlag.Text == "1" && viewflag != "1")
                    {
                        string CuNo = (string)Session["RCreditUNo"];
                        lblShareCu.Text = CuNo;
                        SessionRemove();
                    }
                    else
                    {
                        TruncateWF();
                    }

                if (lblCtrlFlag.Text == "1" && viewflag != "1")
                {

                    string RAccTypeGuaranty = (string)Session["SAccTypeGuaranty"];
                    lblAtypeGuaranty.Text = RAccTypeGuaranty;

                    string CuNo = (string)Session["SItxtCreditUNo"];
                    ItxtCreditUNo.Text = CuNo;
                    string CType = (string)Session["SlblCuType"];
                    lblCuType.Text = CType;
                    string CNo = (string)Session["SlblCuNo"];
                    lblCuNo.Text = CNo;
                    string CName = (string)Session["SIlblCuName"];
                    IlblCuName.Text = CName;
                    string MNo = (string)Session["SItxtMemNo"];
                    ItxtMemNo.Text = MNo;
                    string MName = (string)Session["SIlblMemName"];
                    IlblMemName.Text = MName;
                    string AType = (string)Session["SddlIAccType"];
                    ddlIAccType.SelectedValue = AType;
                    string ANo = (string)Session["SItxtAccNo"];
                    ItxtAccNo.Text = ANo;
                    string NewAccNo = (string)Session["NewAccNo"];
                    txtAccNo.Text = NewAccNo;
                    string ESAmt = (string)Session["SIlblExSancAmt"];
                    IlblExSancAmt.Text = ESAmt;
                    string NSAmt = (string)Session["SItxtNewSanctionAmount"];
                    ItxtNewSanctionAmount.Text = NSAmt;
                    string EIRate = (string)Session["SIlblExIntRate"];
                    IlblExIntRate.Text = EIRate;
                    string NIRate = (string)Session["SItxtNewInterestRate"];
                    ItxtNewInterestRate.Text = NIRate;
                    string note = (string)Session["SItxtNote"];
                    ItxtNote.Text = note;

                    string RSlblRenewalDate = (string)Session["SlblRenewalDate"];
                    lblRenewalDate.Text = RSlblRenewalDate;

                    string RSlblNewExpiryDate = (string)Session["SlblNewExpiryDate"];
                    lblNewExpiryDate.Text = RSlblNewExpiryDate;
                   
                    //string RlblTotalResult = (string)Session["SlblTotalResult"];
                    //lblTotalResult.Text = RlblTotalResult;

                    //string RtxtTotalAmt = (string)Session["StxtTotalAmt"];
                    //txtTotalAmt.Text = RtxtTotalAmt;

                    //if (txtTotalAmt.Text != string.Empty)
                    //{
                    //    gvInfo();
                    //}

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
                        gvDepositInfo();
                        SumDepositValue();
                        TotalGuarantor();

                    }
                    else
                        if (pnlShare.Visible == true)
                        {
                            pnlDeposit.Visible = false;
                            pnlProperty.Visible = false;
                            pnlShare.Visible = true;
                            pnlLoanApplication.Visible = false;
                            txtShareCuNo.Text = lblShareCu.Text;
                            txtShareCuNo_TextChanged(this, EventArgs.Empty);

                            gvShareInfo();
                            SumShareValue();
                            TotalGuarantor();
                        }


                }

            }
        }


        private void AccTypedropdown1()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass =5 AND AccTypeMode !='2' AND AccFlag !='3'";
            ddlIAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIAccType, "A2ZCSMCUS");
        }


        //protected void gvDetals()
        //{
        //    string sqlquery3 = "SELECT SanctionDate,NewSanctionAmt,NewIntRate,Note FROM A2ZLOANHST WHERE CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' AND DistbursmentAmt = 0";

        //    gvDetailsInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailsInfo, "A2ZCSMCUS");
        //}


        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZACGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZCSMCUS"));

            string ShareQry = "DELETE  dbo.WFA2ZSHGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ShareQry, "A2ZCSMCUS"));

            string PropertyQry = "DELETE dbo.WFA2ZPRGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(PropertyQry, "A2ZCSMCUS"));

        }

        protected void SessionRemove()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["ViewFlag"] = string.Empty;

            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;


            Session["SSPflag"] = string.Empty;
            //Session["SAccTypeGuaranty"] = string.Empty;


            //Session["SpnlDeposit"] = string.Empty;
            //Session["SpnlShare"] = string.Empty;

            //Session["SlblTotalResult"] = string.Empty;
            //Session["StxtTotalAmt"] = string.Empty;
            //Session["SlblShareTotalAmt"] = string.Empty;
            //Session["SlblSumProperty"] = string.Empty;


        }
        protected void ItxtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (ItxtCreditUNo.Text != string.Empty)
                {

                    //int CN = Converter.GetInteger(txtCreditUNo.Text);

                    //hdnCuNumber.Value = Converter.GetString(CN);

                    string c = "";
                    int a = ItxtCreditUNo.Text.Length;

                    string b = ItxtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    //if (getDTO.CreditUnionNo > 0)

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        IlblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                        ItxtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
                        ItxtMemNo.Focus();
                    }
                    else
                    {
                        ItxtCreditUNo.Text = string.Empty;
                        IlblCuName.Text = string.Empty;
                        ItxtCreditUNo.Focus();
                        InvalidCUMSG();
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCreditUNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void ItxtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (ItxtMemNo.Text != string.Empty && ItxtCreditUNo.Text != string.Empty)
                {

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(ItxtMemNo.Text);
                    int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        ItxtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        IlblMemName.Text = Converter.GetString(getDTO.MemberName);

                    }
                    else
                    {
                        ItxtMemNo.Text = string.Empty;
                        IlblMemName.Text = string.Empty;
                        ItxtMemNo.Focus();
                        InvalidMemMSG();
                    }

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtMemNo_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlIAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Int16 AccType = Converter.GetSmallInteger(ddlIAccType.SelectedValue);
                A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                if (get1DTO.AccTypeCode > 0)
                {

                    lblAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                    lblAtypeGuaranty.Text = Converter.GetString(get1DTO.AccTypeGuaranty);

                    //if (lblAccTypeClass.Text == "5")
                    //{
                    string qry = "SELECT Id,AccNo,AccLoanSancAmt,AccIntRate,AccPeriod FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + ItxtMemNo.Text + "' and AccType='" + ddlIAccType.SelectedValue + "' AND AccStatus < 90";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        ItxtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                        IlblExSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (dt.Rows[0]["AccLoanSancAmt"])));
                        IlblExIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", (dt.Rows[0]["AccIntRate"])));
                        lblIPeriod.Text = Converter.GetString(dt.Rows[0]["AccPeriod"]);

                        GetExpiryDate();

                        ItxtNewSanctionAmount.Focus();

                        DepositDataToWF();
                        gvDepositInfo();
                        SumDepositValue();

                        ShareDataToWF();
                        gvShareInfo();
                        SumShareValue();

                        PropertyDataToWF();
                        gvPropertyInfo();
                        SumPropertyValue();

                        TotalGuarantor();

                    }
                    else
                    {
                        ddlIAccType.SelectedIndex = 0;
                        ItxtAccNo.Text = string.Empty;
                        IlblExSancAmt.Text = string.Empty;
                        IlblExIntRate.Text = string.Empty;
                        lblRenewalDate.Text = string.Empty;
                        ItxtNote.Text = string.Empty;
                        lblNewExpiryDate.Text = string.Empty;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                        return;
                    }
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        private void InvalidAccTypeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
            return;
        }
        private void InvalidCUMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        private void InvalidMemMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        private void InvalidSancAmtMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid New Sanction Amount');", true);
            return;
        }

        private void InvalidIntRateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid New Interest Rate');", true);
            return;
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove();
            Response.Redirect("A2ZERPModule.aspx");

        }

        protected void Success()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('New Sanction Amount Added Successfully');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New Sanction Amount Added Successfully.');", true);

        }

        protected void ClearInfo()
        {

            ItxtCreditUNo.Text = string.Empty;
            ItxtMemNo.Text = string.Empty;

            ItxtAccNo.Text = string.Empty;

            IlblCuName.Text = string.Empty;
            IlblMemName.Text = string.Empty;

            ItxtNewSanctionAmount.Text = string.Empty;
            IlblExSancAmt.Text = string.Empty;
            IlblExIntRate.Text = string.Empty;
            ItxtNewInterestRate.Text = string.Empty;
            ItxtNote.Text = string.Empty;
        }

        protected void ItxtNewSanctionAmount_TextChanged(object sender, EventArgs e)
        {
            double amt = Converter.GetDouble(ItxtNewSanctionAmount.Text);
            ItxtNewSanctionAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
            ItxtNewInterestRate.Focus();

        }

        protected void BtnNewSanction_Click(object sender, EventArgs e)
        {
            try
            {

                if (ItxtCreditUNo.Text == string.Empty)
                {
                    ItxtCreditUNo.Text = string.Empty;
                    ItxtCreditUNo.Focus();
                    InvalidCUMSG();
                    return;
                }

                if (ItxtMemNo.Text == string.Empty)
                {
                    ItxtMemNo.Text = string.Empty;
                    ItxtMemNo.Focus();
                    InvalidMemMSG();
                    return;
                }

                if (ddlIAccType.SelectedValue == "-Select-")
                {
                    InvalidAccTypeMSG();
                    return;
                }

                if (ItxtNewSanctionAmount.Text == string.Empty)
                {
                    ItxtNewSanctionAmount.Text = string.Empty;
                    ItxtNewSanctionAmount.Focus();
                    InvalidSancAmtMSG();
                    return;
                }

                if (ItxtNewInterestRate.Text == string.Empty)
                {
                    ItxtNewInterestRate.Text = string.Empty;
                    ItxtNewInterestRate.Focus();
                    InvalidIntRateMSG();
                    return;
                }


                A2ZLOANHSTDTO objDTO = new A2ZLOANHSTDTO();
                objDTO.AccType = Converter.GetInteger(ddlIAccType.SelectedValue);
                objDTO.AccNo = Converter.GetLong(ItxtAccNo.Text);
                objDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
                objDTO.CuNo = Converter.GetInteger(lblCuNo.Text);
                objDTO.MemberNo = Converter.GetInteger(ItxtMemNo.Text);
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.SanctionDate = Converter.GetDateTime(dto.ProcessDate);
                objDTO.NewSanctionAmount = Converter.GetDecimal(ItxtNewSanctionAmount.Text);
                objDTO.NewIntRate = Converter.GetDecimal(ItxtNewInterestRate.Text);
                objDTO.ApproveBy = Converter.GetSmallInteger(hdnID.Text);
                objDTO.Note = Converter.GetString(ItxtNote.Text);

                int result = A2ZLOANHSTDTO.InsertInformation(objDTO);
                if (result > 0)
                {

                    DateTime Rendate = DateTime.ParseExact(lblRenewalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime Expdate = DateTime.ParseExact(lblNewExpiryDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                    string strQuery = "UPDATE A2ZACCOUNT SET  AccLoanSancAmt = '" + ItxtNewSanctionAmount.Text + "', AccIntRate= '" + ItxtNewInterestRate.Text + "',AccRenwlDate= '" + Rendate + "',AccLoanExpiryDate= '" + Expdate + "' WHERE  CuType='" + lblCuType.Text + "' AND CuNo='" + lblCuNo.Text + "' AND MemNo='" + ItxtMemNo.Text + "' AND  AccType='" + ddlIAccType.SelectedValue + "' AND AccNo='" + ItxtAccNo.Text + "'";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    Success();

                    Delete();

                    SubmitDepositData();
                    SubmitShareData();
                    SubmitPropertyData();
                    TruncateWF();
                    ClearInfo();
                    ItxtCreditUNo.Focus();




                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnNewSanction_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetailsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void ItxtNewInterestRate_TextChanged(object sender, EventArgs e)
        {
            ItxtNote.Focus();
        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            Session["ExFlag"] = "1";
            Session["ViewFlag"] = "1";

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }

        protected void BtnDeposit_Click(object sender, EventArgs e)
        {
            if (ItxtAccNo.Text != string.Empty)
            {
                txtCrUNo.Focus();
                pnlDeposit.Visible = true;
                pnlProperty.Visible = false;
                pnlShare.Visible = false;
                pnlLoanApplication.Visible = false;
                Session["SpnlShare"] = string.Empty;


                //DepositDataToWF();
                gvDepositInfo();
                SumDepositValue();
                TotalGuarantor();

                gvDetailInfo.Visible = true;
                lblTotalAmt.Visible = true;
                txtTotalAmt.Visible = true;
            }
        }

        protected void BtnShare_Click(object sender, EventArgs e)
        {
            if (ItxtAccNo.Text != string.Empty)
            {
                pnlDeposit.Visible = false;
                pnlProperty.Visible = false;
                pnlShare.Visible = true;
                pnlLoanApplication.Visible = false;
                Session["SpnlDeposit"] = string.Empty;
                txtShareCuNo.Focus();

                //ShareDataToWF();
                gvShareInfo();
                SumShareValue();
                TotalGuarantor();

                gvShareDetails.Visible = true;
                lblTotalAmt.Visible = true;
                txtTotalAmt.Visible = true;
            }
        }

        protected void BtnProperty_Click(object sender, EventArgs e)
        {
            if (ItxtAccNo.Text != string.Empty)
            {
                pnlDeposit.Visible = false;
                pnlProperty.Visible = true;
                pnlShare.Visible = false;
                pnlLoanApplication.Visible = false;
                txtSerialNo.Focus();

                //PropertyDataToWF();
                gvPropertyInfo();
                SumPropertyValue();
                TotalGuarantor();

                gvPropertyDetails.Visible = true;
                lblTotalAmt.Visible = true;
                txtTotalAmt.Visible = true;
            }
        }

        #region Deposit Gaurantor
        //--------------Deposit Gaurantor---------------------------------
        protected void txtCrUNo_TextChanged(object sender, EventArgs e)
        {
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
                txtAccType.Focus();
            }
            else
            {

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
                lblIAtypeGuaranty.Text = Converter.GetString(ObjDTO.AccTypeGuaranty);

                if (lblIAtypeGuaranty.Text != lblAtypeGuaranty.Text)
                {
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                    return;
                }
                else
                {
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
            Int16 CuType = Converter.GetSmallInteger(lblDepositCuType.Text);
            int CuNo = Converter.GetInteger(lblDepositCuNo.Text);
            int MemNo = Converter.GetInteger(txtDepositMemNo.Text);
            Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
            Int64 AccNo = Converter.GetLong(txtAccNo.Text);
            A2ZACCOUNTDTO ObjDTO = (A2ZACCOUNTDTO.GetInformation(AccType, AccNo, CuType, CuNo, MemNo));
            if (ObjDTO.a > 0)
            {
                txtAccNo.Text = Converter.GetString(ObjDTO.AccNo);
                txtLedgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccBalance));
                txtTotalLienAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ObjDTO.AccLienAmt));
                lblStatus.Text = Converter.GetString(ObjDTO.AccStatus);
                if (lblStatus.Text == "99")
                {
                    InvalidAccMSG();
                    txtAccNo.Focus();
                }
                else
                {
                    txtLionAmt.Focus();
                }
            }


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

        private void gvDepositInfo()
        {

            string sqlquery3 = "SELECT Id, lTrim(str(CuType)+'-' +lTrim(str(CuNo))) As CuNo,MemNo,AccType,AccNo,AccAmount FROM WFA2ZACGUAR Where UserId='" + hdnID.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void SumDepositValue()
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



            string statment = "INSERT INTO  WFA2ZACGUAR (LoanAccNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId) VALUES('" + ItxtAccNo.Text + "','" + lblDepositCuType.Text + "','" + lblDepositCuNo.Text + "','" + txtDepositMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtLionAmt.Text + "',1,0, '" + hdnID.Text + "')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            gvDepositInfo();
            SumDepositValue();
            TotalGuarantor();
            ClearDeposit();
            txtCrUNo.Focus();





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
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowMode,UserId From WFA2ZACGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                Int64 LoanAccNo = Converter.GetLong(dt1.Rows[i]["LoanAccNo"]);
                int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);
                int RowMode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                string statment = "INSERT INTO  A2ZACGUAR (LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) VALUES('" + LoanAccNo + "','" + Cutype + "','" + Cuno + "','" + Memno + "','" + AccType + "','" + AccNo + "','" + LienAmt + "')";
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


        protected void DepositDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZACGUAR (LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId ) SELECT LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,0,0,'" + hdnID.Text + "' FROM A2ZACGUAR WHERE LoanAccNo='" + ItxtAccNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


        }

        //---------------------End Of Deposit Garantor----------------------------
        #endregion


        #region Share Gaurantor
        //----------------Share Gaurantor------------------------------------------
        protected void txtShareCuNo_TextChanged(object sender, EventArgs e)
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
                        BtnAddShare.Focus();
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

                sum += Convert.ToDecimal(String.Format("{0:0,0.00}", gvShareDetails.Rows[i].Cells[4].Text));


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
                string statment = "INSERT INTO  WFA2ZSHGUAR (LoanAccNo,CuType, CuNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId) VALUES('" + ItxtAccNo.Text + "','" + lblShareCType.Text + "','" + lblShareCNo.Text + "','" + 11 + "','" + hdnAccNo.Text + "','" + txtShareAmount.Text + "',1,0,'" + hdnID.Text + "')";
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
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowMode,UserId From WFA2ZSHGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                Int64 LoanAccNo = Converter.GetLong(dt1.Rows[i]["LoanAccNo"]);
                int Cutype = Converter.GetInteger(dt1.Rows[i]["CuType"]);
                int Cuno = Converter.GetInteger(dt1.Rows[i]["CuNo"]);
                int Memno = Converter.GetInteger(dt1.Rows[i]["MemNo"]);
                int AccType = Converter.GetInteger(dt1.Rows[i]["AccType"]);
                Int64 AccNo = Converter.GetLong(dt1.Rows[i]["AccNo"]);
                double LienAmt = Converter.GetDouble(dt1.Rows[i]["AccAmount"]);
                int RowMode = Converter.GetInteger(dt1.Rows[i]["RowMode"]);

                string statment = "INSERT INTO  A2ZSHGUAR (LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount) VALUES('" + LoanAccNo + "','" + Cutype + "','" + Cuno + "','" + Memno + "','" + AccType + "','" + AccNo + "','" + LienAmt + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = '50'  WHERE  CuType='" + Cutype + "' AND CuNo='" + Cuno + "' AND MemNo='" + Memno + "' AND  AccType='" + AccType + "' AND AccNo='" + AccNo + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                gvShareDetails.Visible = false;
                lbltotalshare.Visible = false;
                lblShareTotalAmt.Visible = false;



            }

        }

        protected void ShareDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZSHGUAR (LoanAccNo ,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,RowType,RowMode,UserId) SELECT LoanAccNo,CuType, CuNo, MemNo, AccType,AccNo,AccAmount,0,0,'" + hdnID.Text + "' FROM A2ZSHGUAR WHERE LoanAccNo='" + ItxtAccNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


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

            string sqlquery3 = "SELECT Id,LoanAccNo, PrSRL,PrName,FileNo,PrAmount,PrDesc FROM WFA2ZPRGUAR Where UserId='" + hdnID.Text + "'";
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

            string statment = "INSERT INTO  WFA2ZPRGUAR (LoanAccNo, PrSRL,PrName,FileNo,PrDesc,PrAmount,RowType,RowMode,UserId) VALUES('" + ItxtAccNo.Text + "','" + txtSerialNo.Text + "','" + txtNameProperty.Text + "','" + txtFileNo.Text + "','" + txtDescription.Text + "','" + txtProprertyAmt.Text + "',1,0, '" + hdnID.Text + "')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            gvPropertyInfo();
            SumPropertyValue();
            TotalGuarantor();
            ClearProperty();
            txtSerialNo.Focus();
        }

        private void SubmitPropertyData()
        {
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select LoanAccNo,PrSRL,PrName,FileNo,PrDesc,PrAmount From WFA2ZPRGUAR Where UserId='" + hdnID.Text + "'", "A2ZCSMCUS");

            for (int i = 0; i < dt1.Rows.Count; ++i)
            {
                Int64 LoanAccNo = Converter.GetLong(dt1.Rows[i]["LoanAccNo"]);
                int PrSRL = Converter.GetInteger(dt1.Rows[i]["PrSRL"]);
                string PrName = Converter.GetString(dt1.Rows[i]["PrName"]);
                string FileNo = Converter.GetString(dt1.Rows[i]["FileNo"]);
                string PrDesc = Converter.GetString(dt1.Rows[i]["PrDesc"]);
                double PrAmount = Converter.GetDouble(dt1.Rows[i]["PrAmount"]);


                string statment = "INSERT INTO  A2ZPRGUAR (LoanAccNo,PrSRL,PrName,FileNo,PrDesc,PrAmount) VALUES('" + LoanAccNo + "','" + PrSRL + "','" + PrName + "','" + FileNo + "','" + PrDesc + "','" + PrAmount + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

                gvPropertyDetails.Visible = false;
                lblTotalProprty.Visible = false;
                lblSumProperty.Visible = false;

            }

        }


        protected void PropertyDataToWF()
        {
            string statment = "INSERT INTO  WFA2ZPRGUAR (LoanAccNo, PrSRL,PrName,FileNo,PrAmount,PrDesc,RowType,RowMode,UserId ) SELECT LoanAccNo, PrSRL,PrName,FileNo,PrAmount,PrDesc,0,0,'" + hdnID.Text + "' FROM A2ZPRGUAR WHERE LoanAccNo='" + ItxtAccNo.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));


        }

        //protected void txtProprertyAmt_TextChanged(object sender, EventArgs e)
        //{
        //    double amt = Converter.GetDouble(txtProprertyAmt.Text);
        //    txtProprertyAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", amt));
        //    BtnAddProperty.Focus();
        //}


        protected void Delete()
        {

            try
            {
                string sqlQuery = string.Empty;
                int rowEffect;

                sqlQuery = @"DELETE  FROM A2ZACGUAR WHERE  LoanAccNo = '" + ItxtAccNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

                sqlQuery = @"DELETE  FROM A2ZSHGUAR WHERE  LoanAccNo = '" + ItxtAccNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

                sqlQuery = @"DELETE  FROM A2ZPRGUAR WHERE  LoanAccNo = '" + ItxtAccNo.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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
                gvDepositInfo();
                SumDepositValue();
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

        protected void BtnLoanApplication_Click(object sender, EventArgs e)
        {
            pnlDeposit.Visible = false;
            pnlProperty.Visible = false;
            pnlShare.Visible = false;
            pnlLoanApplication.Visible = true;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlDeposit"] = "1";
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "5";
            Session["SModule"] = "1";

            Session["SSPflag"] = "1";
            Session["SAccTypeGuaranty"] = lblAtypeGuaranty.Text;


            Session["SItxtCreditUNo"] = ItxtCreditUNo.Text;
            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCuNo"] = lblCuNo.Text;
            Session["SIlblCuName"] = IlblCuName.Text;
            Session["SItxtMemNo"] = ItxtMemNo.Text;
            Session["SIlblMemName"] = IlblMemName.Text;
            Session["SddlIAccType"] = ddlIAccType.SelectedValue;
            Session["SItxtAccNo"] = ItxtAccNo.Text;
            Session["SIlblExSancAmt"] = IlblExSancAmt.Text;
            Session["SItxtNewSanctionAmount"] = ItxtNewSanctionAmount.Text;
            Session["SIlblExIntRate"] = IlblExIntRate.Text;
            Session["SItxtNewInterestRate"] = ItxtNewInterestRate.Text;
            Session["SItxtNote"] = ItxtNote.Text;

            Session["SlblRenewalDate"] = lblRenewalDate.Text;
            Session["SlblNewExpiryDate"] = lblNewExpiryDate.Text;

            //Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSGetAccountNo.aspx','_blank');</script>", false);

        }


        protected void btnSSearch_Click(object sender, EventArgs e)
        {
            Session["SpnlShare"] = "1";
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "5";

            Session["SItxtCreditUNo"] = ItxtCreditUNo.Text;
            Session["SlblCuType"] = lblCuType.Text;
            Session["SlblCuNo"] = lblCuNo.Text;
            Session["SIlblCuName"] = IlblCuName.Text;
            Session["SItxtMemNo"] = ItxtMemNo.Text;
            Session["SIlblMemName"] = IlblMemName.Text;
            Session["SddlIAccType"] = ddlIAccType.SelectedValue;
            Session["SItxtAccNo"] = ItxtAccNo.Text;
            Session["SIlblExSancAmt"] = IlblExSancAmt.Text;
            Session["SItxtNewSanctionAmount"] = ItxtNewSanctionAmount.Text;
            Session["SIlblExIntRate"] = IlblExIntRate.Text;
            Session["SItxtNewInterestRate"] = ItxtNewInterestRate.Text;
            Session["SItxtNote"] = ItxtNote.Text;

            Session["SlblRenewalDate"] = lblRenewalDate.Text;
            Session["SlblNewExpiryDate"] = lblNewExpiryDate.Text;

            //Session["SlblTotalResult"] = lblTotalResult.Text;
            Session["StxtTotalAmt"] = txtTotalAmt.Text;
            Session["SlblShareTotalAmt"] = lblShareTotalAmt.Text;
            Session["SlblSumProperty"] = lblSumProperty.Text;


            Session["ExFlag"] = "1";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);


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

        public void GetExpiryDate()
        {

            lblRenewalDate.Text = lblProcDate.Text;


            DateTime Matdate = new DateTime();
            Matdate = DateTime.ParseExact(lblRenewalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Matdate = Matdate.AddMonths(Converter.GetSmallInteger(lblIPeriod.Text));
            DateTime dt = Converter.GetDateTime(Matdate);
            string date = dt.ToString("dd/MM/yyyy");
            lblNewExpiryDate.Text = date;
        }

        protected void ItxtAccNo_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT Id,CuType,CuNo,MemNo,AccType,AccNo,AccLoanSancAmt,AccIntRate,AccPeriod FROM A2ZACCOUNT where AccNo='" + ItxtAccNo.Text + "' AND AccStatus < 90";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblCuType.Text = Converter.GetString(dt.Rows[0]["CuType"]);
                lblCuNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);
                ItxtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                CtrlAccType.Text = Converter.GetString(dt.Rows[0]["AccType"]);

                Int16 AType = Converter.GetSmallInteger(CtrlAccType.Text);
                A2ZACCTYPEDTO get6DTO = (A2ZACCTYPEDTO.GetInformation(AType));
                if (get6DTO.AccTypeCode > 0)
                {
                    lblAccTypeClass.Text = Converter.GetString(get6DTO.AccTypeClass);
                    lblAtypeGuaranty.Text = Converter.GetString(get6DTO.AccTypeGuaranty);
                    CtrlAccTypeMode.Text = Converter.GetString(get6DTO.AccTypeMode);
                    CtrlAccFlag.Text = Converter.GetString(get6DTO.AccFlag);

                    if (lblAccTypeClass.Text == "5" && CtrlAccTypeMode.Text != "2" && CtrlAccFlag.Text != "3")
                    {
                        ddlIAccType.SelectedValue = CtrlAccType.Text;

                        Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                        int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                        A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                        if (getDTO.NoRecord > 0)
                        {
                            IlblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                            ItxtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
                        }

                        int MemNumber = Converter.GetInteger(ItxtMemNo.Text);
                        A2ZMEMBERDTO get1DTO = new A2ZMEMBERDTO();
                        get1DTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                        if (get1DTO.NoRecord > 0)
                        {
                            IlblMemName.Text = Converter.GetString(get1DTO.MemberName);
                        }


                        IlblExSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (dt.Rows[0]["AccLoanSancAmt"])));
                        IlblExIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", (dt.Rows[0]["AccIntRate"])));
                        lblIPeriod.Text = Converter.GetString(dt.Rows[0]["AccPeriod"]);

                        GetExpiryDate();

                        ItxtNewSanctionAmount.Focus();

                        DepositDataToWF();
                        gvDepositInfo();
                        SumDepositValue();

                        ShareDataToWF();
                        gvShareInfo();
                        SumShareValue();

                        PropertyDataToWF();
                        gvPropertyInfo();
                        SumPropertyValue();

                        TotalGuarantor();
                    }
                    else
                    {
                        ItxtAccNo.Text = string.Empty;
                        IlblExSancAmt.Text = string.Empty;
                        IlblExIntRate.Text = string.Empty;
                        lblRenewalDate.Text = string.Empty;
                        ItxtNote.Text = string.Empty;
                        lblNewExpiryDate.Text = string.Empty;
                        ItxtAccNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                        return;
                    }
                }
            }
            else
            {
                ItxtAccNo.Text = string.Empty;
                IlblExSancAmt.Text = string.Empty;
                IlblExIntRate.Text = string.Empty;
                lblRenewalDate.Text = string.Empty;
                ItxtNote.Text = string.Empty;
                lblNewExpiryDate.Text = string.Empty;
                ItxtAccNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                return;
            }
        }

    }
}