using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;






namespace ATOZWEBMCUS.Pages
{
    public partial class CSLedgerBalanceReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    if (CtrlProgFlag.Text != "1")
                    {

                        lblModule.Text = Request.QueryString["a%b"];

                        if (lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7")
                        {
                            Acc1dropdown();
                            CreditUnionDropdown();
                            txtCrUnion.Focus();
                            ChkAllCrUnion.Checked = true;

                            ChkAccStatus.Checked = true;
                            ddlAccStatus.Enabled = false;
                            AccountStatusDropdown();

                            ChkAllBalance.Checked = true;
                            ddlBalance.Enabled = false;

                            //ChkAllContract.Visible = false;
                        }

                        if (lblModule.Text == "4")
                        {
                            ChkAllCrUnion.Checked = false;
                            ChkAllCrUnion.Visible = false;
                            lblCu.Visible = false;
                            txtCrUnion.Visible = false;
                            ddlCrUnion.Visible = false;
                            Acc2dropdown();
                            txtAccType.Focus();

                            ChkAccStatus.Checked = true;
                            ddlAccStatus.Enabled = false;
                            AccountStatusDropdown();

                            ChkAllBalance.Checked = true;
                            ddlBalance.Enabled = false;
                        }


                        lblFromBalance.Visible = false;
                        txtFromBalance.Visible = false;
                        lblTillBalance.Visible = false;
                        txtTillBalance.Visible = false;
                        BtnAmountSign.Visible = false;

                        ChkAllCode.Checked = false;
                        ChkAllCode.Visible = false;
                        lblCodeType.Visible = false;
                        txtCodeType.Visible = false;
                        ddlCodeType.Visible = false;



                        txtCrUnion.Enabled = false;
                        ddlCrUnion.Enabled = false;

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtDate.Text = date;
                    }
                    else
                    {
                        string RtxtDate = (string)Session["StxtDate"];
                        string RtxtAccType = (string)Session["StxtAccType"];
                        string RddlAcType = (string)Session["SddlAcType"];

                        string RChkAllCode = (string)Session["SChkAllCode"];
                        string RtxtCodeType = (string)Session["StxtCodeType"];
                        string RddlCodeType = (string)Session["SddlCodeType"];
                        string RChkAllCrUnion = (string)Session["SChkAllCrUnion"];
                        string RtxtCrUnion = (string)Session["StxtCrUnion"];
                        string RddlCrUnion = (string)Session["SddlCrUnion"];

                        string RlblCuType = (string)Session["SlblCuType"];
                        string RlblCuNo = (string)Session["SlblCuNo"];

                        string RChkAccStatus = (string)Session["SChkAccStatus"];
                        string RddlAccStatus = (string)Session["SddlAccStatus"];
                        string RChkAllBalance = (string)Session["SChkAllBalance"];
                        string RddlBalance = (string)Session["SddlBalance"];
                        string RtxtFromBalance = (string)Session["StxtFromBalance"];
                        string RtxtTillBalance = (string)Session["StxtTillBalance"];

                        string RBtnAmountSign = (string)Session["SBtnAmountSign"];
                        string RlblAmtSign = (string)Session["SlblAmtSign"];

                       
                        string RlblModule = (string)Session["SlblModule"];

                        string RlblAccTypeClass = (string)Session["SlblAccTypeClass"];

                        string RlblAccAccessFlag = (string)Session["SlblAccAccessFlag"];

                        lblModule.Text = RlblModule;

                        lblAccTypeClass.Text = RlblAccTypeClass;
                        lblAccAccessFlag.Text = RlblAccAccessFlag;


                        if (lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7")
                        {
                            Acc1dropdown();
                            CreditUnionDropdown();
                            AccountStatusDropdown();
                        }

                        if (lblModule.Text == "4")
                        {
                            ChkAllCrUnion.Checked = false;
                            ChkAllCrUnion.Visible = false;
                            lblCu.Visible = false;
                            txtCrUnion.Visible = false;
                            ddlCrUnion.Visible = false;
                            Acc2dropdown();
                            AccountStatusDropdown();
                            ChkAccStatus.Checked = true;
                            ddlAccStatus.Enabled = false;
                            AccountStatusDropdown();

                            ChkAllBalance.Checked = true;
                            ddlBalance.Enabled = false;

                            ChkAllCode.Checked = true;
                            ChkAllCode.Visible = false;
                            lblCodeType.Visible = false;
                            txtCodeType.Visible = false;
                            ddlCodeType.Visible = false;
                        }

                        txtDate.Text = RtxtDate;
                        txtAccType.Text = RtxtAccType;
                        ddlAcType.SelectedValue = RddlAcType;

                        if (lblAccTypeClass.Text != "7")
                        {
                            ChkAllCode.Checked = false;
                            ChkAllCode.Visible = false;
                            lblCodeType.Visible = false;
                            txtCodeType.Visible = false;
                            ddlCodeType.Visible = false;
                        }
                        
                        
                        if (RChkAllCode == "1")
                        {
                            ChkAllCode.Checked = true;
                            txtCodeType.Text = string.Empty;
                            ddlCodeType.SelectedValue = "-Select-";
                            txtCodeType.Enabled = false;
                            ddlCodeType.Enabled = false;

                        }
                        else
                        {
                            ChkAllCode.Checked = false;
                            txtCodeType.Enabled = true;
                            ddlCodeType.Enabled = true;
                            txtCodeType.Text = RtxtCodeType;
                            CodeTypedropdown();
                            ddlCodeType.SelectedValue = RddlCodeType;
                        }

                        if (RChkAllCrUnion == "1")
                        {
                            ChkAllCrUnion.Checked = true;
                            txtCrUnion.Enabled = false;
                            ddlCrUnion.Enabled = false;
                            txtCrUnion.Text = string.Empty;
                            ddlCrUnion.SelectedValue = "-Select-";
                        }
                        else
                        {
                            ChkAllCrUnion.Checked = false;
                            txtCrUnion.Enabled = true;
                            ddlCrUnion.Enabled = true;
                            txtCrUnion.Text = RtxtCrUnion;
                            ddlCrUnion.SelectedValue = RddlCrUnion;
                            lblCuType.Text = RlblCuType;
                            lblCuNo.Text = RlblCuNo;
                        }

                        if (RChkAccStatus == "1")
                        {
                            ChkAccStatus.Checked = true;
                            ddlAccStatus.Enabled = false;
                            ddlAccStatus.SelectedValue = "-Select-";
                        }
                        else
                        {
                            ChkAccStatus.Checked = false;
                            ddlAccStatus.Enabled = true;
                            ddlAccStatus.SelectedValue = RddlAccStatus;
                        }

                        if (RChkAllBalance == "1")
                        {
                            ChkAllBalance.Checked = true;
                            ddlBalance.Enabled = false;
                            ddlBalance.SelectedValue = "-Select-";
                            txtFromBalance.Text = string.Empty;
                            txtTillBalance.Text = string.Empty;
                            lblFromBalance.Visible = false;
                            txtFromBalance.Visible = false;
                            lblTillBalance.Visible = false;
                            txtTillBalance.Visible = false;

                            BtnAmountSign.Visible = false;

                        }
                        else
                        {
                            if (RddlBalance == "1")
                            {
                                ChkAllBalance.Checked = false;
                                ddlBalance.Enabled = true;
                                ddlBalance.SelectedValue = RddlBalance;
                                txtFromBalance.Text = RtxtFromBalance;
                                txtTillBalance.Text = RtxtTillBalance;
                                BtnAmountSign.Text = RBtnAmountSign;
                                lblAmtSign.Text = RlblAmtSign;
                 
                                if (lblAmtSign.Text == "1")
                                {
                                    BtnAmountSign.BackColor = Color.Green;
                                }
                                else 
                                {
                                    BtnAmountSign.BackColor = Color.Red;
                                }                                                            

                                lblFromBalance.Visible = true;
                                txtFromBalance.Visible = true;
                                lblTillBalance.Visible = true;
                                txtTillBalance.Visible = true;

                                BtnAmountSign.Visible = true;
                            }
                            else 
                            {
                                ChkAllBalance.Checked = false;
                                ddlBalance.Enabled = true;
                                ddlBalance.SelectedValue = RddlBalance;
                                txtFromBalance.Text = RtxtFromBalance;
                                txtTillBalance.Text = RtxtTillBalance;
                                BtnAmountSign.Text = RBtnAmountSign;

                                lblFromBalance.Visible = false;
                                txtFromBalance.Visible = false;
                                lblTillBalance.Visible = false;
                                txtTillBalance.Visible = false;

                                BtnAmountSign.Visible = false;
                            }
                        }

                        
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }



        }


        protected void RemoveSession()
        {

            Session["ProgFlag"] = string.Empty;

            Session["StxtDate"] = string.Empty;
            Session["StxtAccType"] = string.Empty;
            Session["SddlAcType"] = string.Empty;

            Session["SChkAllCode"] = string.Empty;
            Session["StxtCodeType"] = string.Empty;
            Session["SddlCodeType"] = string.Empty;
            Session["SChkAllCrUnion"] = string.Empty;
            Session["StxtCrUnion"] = string.Empty;
            Session["SddlCrUnion"] = string.Empty;

            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;
           
            Session["SChkAccStatus"] = string.Empty;
            Session["SddlAccStatus"] = string.Empty;
            Session["SChkAllBalance"] = string.Empty;
            Session["SddlBalance"] = string.Empty;
            Session["StxtFromBalance"] = string.Empty;
            Session["StxtTillBalance"] = string.Empty;

            Session["SBtnAmountSign"] = string.Empty;
            Session["SlblAmtSign"] = string.Empty;

            Session["SlblModule"] = string.Empty;
        }

        private void CreditUnionDropdown()
        {
            if (lblModule.Text == "1")
            {
                string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' ORDER BY CuName ASC";
                ddlCrUnion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCrUnion, "A2ZCSMCUS");
            }
            else
            {
                string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' AND GLCashCode='" + lblCashCode.Text + "' ORDER BY CuName ASC";
                ddlCrUnion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCrUnion, "A2ZCSMCUS");
            }
        }
        private void Acc1dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode !='2' ORDER BY AccTypeClass";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void Acc2dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeMode !='1'";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void AccountStatusDropdown()
        {

            string sqlquery = "SELECT AccStatusCode,AccStatusDescription from A2ZACCSTATUS";
            ddlAccStatus = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccStatus, "A2ZCSMCUS");

        }


        private void CodeTypedropdown()
        {
            string sqlquery = "SELECT PayType,PayTypeDes from A2ZPAYTYPE WHERE AtyClass=7";
            ddlCodeType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCodeType, "A2ZCSMCUS");
        }
        protected void txtCrUnion_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCrUnion.Text != string.Empty)
                {
                    string c = "";
                    int a = txtCrUnion.Text.Length;

                    string b = txtCrUnion.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);



                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        hdnCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        if (lblModule.Text == "6" && hdnCashCode.Text != lblCashCode.Text)
                        {
                            txtCrUnion.Text = string.Empty;
                            txtCrUnion.Focus();
                            InvalidCuNoMSG();
                            return;
                        }

                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "' ORDER BY CuName ASC";
                        ddlCrUnion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCrUnion, "A2ZCSMCUS");
                        ddlCrUnion.SelectedValue = Converter.GetString(txtCrUnion.Text);


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCrUnion_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void ddlCrUnion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlCrUnion.SelectedValue != "-Select-")
                {
                    txtHidden.Text = Converter.GetString(ddlCrUnion.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        txtCrUnion.Text = Converter.GetString(txtHidden.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCrUnion_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }


        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtAccType.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                        lblAccTypeClass.Text = Converter.GetString(getDTO.AccTypeClass);
                        lblAccAccessFlag.Text = Converter.GetString(getDTO.AccAccessFlag);
                        if ((lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7") && lblAccTypeMode.Text == "2")
                        {
                            InvalidAccTypeMSG();
                            txtAccType.Focus();
                            return;
                        }

                        if (lblModule.Text == "4" && lblAccTypeMode.Text == "1")
                        {
                            InvalidAccTypeMSG();
                            txtAccType.Focus();
                            return;
                        }
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);

                        if (lblAccTypeClass.Text == "7")
                        {
                            ChkAllCode.Visible = true;
                            lblCodeType.Visible = true;
                            txtCodeType.Visible = true;
                            ddlCodeType.Visible = true;
                            ChkAllCode.Checked = true;
                            txtCodeType.Enabled = false;
                            ddlCodeType.Enabled = false;
                            CodeTypedropdown();
                        }
                        else
                        {
                            ChkAllCode.Visible = false;
                            lblCodeType.Visible = false;
                            txtCodeType.Visible = false;
                            ddlCodeType.Visible = false;
                        }


                        if (lblAccTypeClass.Text == "5" || lblAccTypeClass.Text == "6")
                        {
                            BtnAmountSign.Text = "Negative";
                            lblAmtSign.Text = "2";
                            BtnAmountSign.BackColor = Color.Red;
                        }
                        else 
                        {
                            BtnAmountSign.Text = "Positive";
                            lblAmtSign.Text = "1";
                            BtnAmountSign.BackColor = Color.Green;
                        }


                        //if(txtAccType.Text == "15")
                        //{
                        //    ChkAllContract.Visible = true;
                        //}
                        //else 
                        //{
                        //    ChkAllContract.Visible = false;
                        //}

                    }

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcType.SelectedValue == "-Select-")
                {
                    txtAccType.Focus();
                    txtAccType.Text = string.Empty;
                }

                if (ddlAcType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        lblAccTypeClass.Text = Converter.GetString(getDTO.AccTypeClass);
                        lblAccAccessFlag.Text = Converter.GetString(getDTO.AccAccessFlag);

                        if (lblAccTypeClass.Text == "7")
                        {
                            ChkAllCode.Visible = true;
                            lblCodeType.Visible = true;
                            txtCodeType.Visible = true;
                            ddlCodeType.Visible = true;
                            ChkAllCode.Checked = true;
                            txtCodeType.Enabled = false;
                            ddlCodeType.Enabled = false;
                            CodeTypedropdown();
                        }
                        else
                        {
                            ChkAllCode.Visible = false;
                            lblCodeType.Visible = false;
                            txtCodeType.Visible = false;
                            ddlCodeType.Visible = false;
                        }


                        if (lblAccTypeClass.Text == "5" || lblAccTypeClass.Text == "6")
                        {
                            BtnAmountSign.Text = "Negative";
                            lblAmtSign.Text = "2";
                            BtnAmountSign.BackColor = Color.Red;
                        }
                        else
                        {
                            BtnAmountSign.Text = "Positive";
                            lblAmtSign.Text = "1";
                            BtnAmountSign.BackColor = Color.Green;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAcType_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void InvalidCuNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;

        }
        protected void InvalidAccTypeMSG()
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
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");


                if (txtCrUnion.Text == string.Empty && ChkAllCrUnion.Checked == false && lblModule.Text != "4")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Mark Credit Union Check Box / Input Credit Union No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark Credit Union Check Box / Input Credit Union No.');", true);
                    return;
                }



                if (txtAccType.Text == string.Empty && txtAccType.Enabled == true)
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account Type');", true);
                    return;
                }

                Session["ProgFlag"] = "1";

                Session["StxtDate"] = txtDate.Text;
                Session["StxtAccType"] = txtAccType.Text;
                Session["SddlAcType"] = ddlAcType.SelectedValue;

                if (ChkAllCode.Checked == true)
                {
                    Session["SChkAllCode"] = "1";
                }
                else
                {
                    Session["SChkAllCode"] = "0";
                }

                Session["StxtCodeType"] = txtCodeType.Text;
                Session["SddlCodeType"] = ddlCodeType.SelectedValue;

                Session["SlblAccTypeClass"] = lblAccTypeClass.Text;

                Session["SlblAccAccessFlag"] = lblAccAccessFlag.Text;
                


                if (ChkAllCrUnion.Checked == true)
                {
                    Session["SChkAllCrUnion"] = "1";
                }
                else
                {
                    Session["SChkAllCrUnion"] = "0";
                }

                Session["StxtCrUnion"] = txtCrUnion.Text;
                Session["SddlCrUnion"] = ddlCrUnion.SelectedValue;

                if (ChkAccStatus.Checked == true)
                {
                    Session["SChkAccStatus"] = "1";
                }
                else
                {
                    Session["SChkAccStatus"] = "0";
                }

                Session["SlblCuType"] = lblCuType.Text;
                Session["SlblCuNo"] = lblCuNo.Text;

                Session["SddlAccStatus"] = ddlAccStatus.SelectedValue;

                if (ChkAllBalance.Checked == true)
                {
                    Session["SChkAllBalance"] = "1";
                }
                else
                {
                    Session["SChkAllBalance"] = "0";
                }

                Session["SddlBalance"] = ddlBalance.SelectedValue;
                Session["StxtFromBalance"] = txtFromBalance.Text;
                Session["StxtTillBalance"] = txtTillBalance.Text;

                Session["SBtnAmountSign"] = BtnAmountSign.Text;
                Session["SlblAmtSign"] = lblAmtSign.Text;
              
                Session["SlblModule"] = lblModule.Text;

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                //DateTime fdate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //var prm = new object[10];

                //prm[0] = Converter.GetSmallInteger(txtAccType.Text);    // @AccType
                //prm[1] = Converter.GetDateToYYYYMMDD(txtDate.Text);     // @fDate
                //prm[2] = 0;         // @CuType
                //prm[3] = 0;         // @CuNo
                //prm[4] = 0;         // @AccStatFlag
                //prm[5] = 0;         // @BalanceFlag
                //prm[6] = 0;         // @fBalance
                //prm[7] = 0;         // @tBalance
                //prm[8] = 0;
                //prm[9] = 0;

                //if (!ChkAllCrUnion.Checked && lblModule.Text != "4")
                //{
                //    prm[2] = lblCuType.Text;        // @CuType
                //    prm[3] = lblCuNo.Text;          // @CuNo
                //}

                //if (!ChkAccStatus.Checked)
                //{
                //    prm[4] = ddlAccStatus.SelectedValue;        // @AccStatFlag
                //}

                //if (!ChkAllBalance.Checked)
                //{
                //    prm[5] = ddlBalance.SelectedValue;
                //    prm[6] = Converter.GetDecimal(txtFromBalance.Text);
                //    prm[7] = Converter.GetDecimal(txtTillBalance.Text);
                //}

                //if (!ChkAllCode.Checked && lblAccTypeClass.Text == "7")
                //{
                //    prm[9] = ddlCodeType.SelectedValue;        // Code Type
                //}
                //else 
                //{
                //    prm[9] = 0;        // Code Type
                //}

                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateLedgerBalance", prm, "A2ZCSMCUS"));


                //int CUNO = Converter.GetInteger(txtCrUnion.Text);

                int cutype = Converter.GetInteger(lblCuType.Text);
                int CUNO = Converter.GetInteger(lblCuNo.Text);

                decimal FromBalanceAmt = 0;
                decimal ToBalanceAmt = 0;
                
                if(lblAmtSign.Text == "1")
                {
                    FromBalanceAmt = Converter.GetDecimal(txtFromBalance.Text);
                    ToBalanceAmt = Converter.GetDecimal(txtTillBalance.Text);
                }
                else
                {
                    FromBalanceAmt = Converter.GetDecimal(txtFromBalance.Text);
                    ToBalanceAmt = Converter.GetDecimal(txtTillBalance.Text);
                    FromBalanceAmt = (0 - FromBalanceAmt);
                    ToBalanceAmt = (0 - ToBalanceAmt);
                    txtFromBalance.Text = Converter.GetString(FromBalanceAmt);
                    txtTillBalance.Text = Converter.GetString(ToBalanceAmt);
                }

                int Module = Converter.GetInteger(lblModule.Text);
                int CashCode = Converter.GetInteger(lblCashCode.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCTYPE,txtAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE,Converter.GetDateToYYYYMMDD(txtDate.Text));

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, lblCuType.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, lblCuNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_ACC_STAT_FLAG, ddlAccStatus.SelectedValue);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_BALANCE_FLAG, ddlBalance.SelectedValue);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_F_BALANCE, txtFromBalance.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_T_BALANCE, txtTillBalance.Text);                
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CODE_TYPE, ddlCodeType.SelectedValue);

                //if (ChkSkipZeroBalance.Checked)
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 1);
                //}
                //else
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                //}

                // Credit Unin
                if (ChkAllCrUnion.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlCrUnion.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CUNO);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlCrUnion.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, lblCuType.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, lblCuNo.Text);
                }

                // Account Status
                if (ChkAccStatus.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_ACC_STAT_FLAG, 0);
                }

                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, ddlAccStatus.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlAccStatus.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_ACC_STAT_FLAG, ddlAccStatus.SelectedValue);
                }

                // All Balance/Only Balance/Zero Balance 
                if (ChkAllBalance.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, FromBalanceAmt);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, ToBalanceAmt);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_BALANCE_FLAG, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_F_BALANCE, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_T_BALANCE, 0);
                }
                else
                {
                    if (ddlBalance.SelectedValue == "1")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, ddlBalance.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, ddlBalance.SelectedItem.Text);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, FromBalanceAmt);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, ToBalanceAmt);

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_BALANCE_FLAG, ddlBalance.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_F_BALANCE, txtFromBalance.Text);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_T_BALANCE, txtTillBalance.Text);
                    }
                    else 
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, ddlBalance.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, ddlBalance.SelectedItem.Text);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, FromBalanceAmt);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, ToBalanceAmt);

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_BALANCE_FLAG, ddlBalance.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_F_BALANCE, 0);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_T_BALANCE, 0);         
                    }
                }

                if (ChkAllCode.Checked || lblAccTypeClass.Text != "7")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, ddlCodeType.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CODE_TYPE, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, ddlCodeType.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, ddlCodeType.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CODE_TYPE, ddlCodeType.SelectedValue);
                }


                //if (ChkAllContract.Checked == true)
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CONTRACT_INT, 1);
                //}
                //else 
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CONTRACT_INT, 0);
                //}
                

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlAcType.SelectedValue);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, Converter.GetDateToYYYYMMDD(txtDate.Text));
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4,ddlAccStatus.SelectedValue);
                // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4,ddlAccStatus.SelectedItem.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO9, Module);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO10, CashCode);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                if ((lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7") && ChkDtlList.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCSLedgerBalance");
                }

                if (lblModule.Text == "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, 0);
                }


                if (lblModule.Text == "4" && ChkDtlList.Checked == false)
                {
                    
                    if (lblAccAccessFlag.Text == "1")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMStaffPFLedgerBalance");
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMStaffLedgerBalance");
                    }
                   
                }


                if (ChkDtlList.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCSUptoDateLedgerBalance");
                }

                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }

        }

        
        protected void chkAllCrUnion_CheckedChanged1(object sender, EventArgs e)
        {

        }

        protected void ChkAllCrUnion_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCrUnion.Checked)
            {

                txtCrUnion.Enabled = false;
                ddlCrUnion.Enabled = false;

            }
            else
            {
                txtCrUnion.Enabled = true;
                ddlCrUnion.Enabled = true;
                txtCrUnion.Focus();
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }



        protected void ChkAccStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAccStatus.Checked)
            {
                ddlAccStatus.Enabled = false;
            }
            else
            {
                ddlAccStatus.Enabled = true;
            }
        }

        protected void ChkAllBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllBalance.Checked)
            {
                ddlBalance.SelectedIndex = 0;
                ddlBalance.Enabled = false;
                lblFromBalance.Visible = false;
                txtFromBalance.Visible = false;
                lblTillBalance.Visible = false;
                txtTillBalance.Visible = false;
                BtnAmountSign.Visible = false;
            }
            else
            {
                ddlBalance.Enabled = true;
            }

        }

        protected void ddlBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBalance.SelectedValue == "1")
            {
                lblFromBalance.Visible = true;
                txtFromBalance.Visible = true;
                lblTillBalance.Visible = true;
                txtTillBalance.Visible = true;
                BtnAmountSign.Visible = true;
                
                txtFromBalance.Text = "1.00";
                txtTillBalance.Text = "99,999,999,999.00";
                

                txtFromBalance.Focus();
            }
            else
            {
                lblFromBalance.Visible = false;
                txtFromBalance.Visible = false;
                lblTillBalance.Visible = false;
                txtTillBalance.Visible = false;
                BtnAmountSign.Visible = false;

                txtFromBalance.Text = string.Empty;
                txtTillBalance.Text = string.Empty;


            }

        }



        protected void ChkAllCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCode.Checked)
            {
                txtCodeType.Enabled = false;
                ddlCodeType.Enabled = false;
            }
            else
            {
                txtCodeType.Enabled = true;
                ddlCodeType.Enabled = true;
                txtCodeType.Focus();
            }
        }

        protected void ddlCodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlCodeType.SelectedValue != "-Select-")
                {
                    txtCodeType.Text = Converter.GetString(ddlCodeType.SelectedValue);


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCrUnion_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtCodeType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCodeType.Text != string.Empty)
                {
                    int AccClass = Converter.GetInteger(lblAccTypeClass.Text);
                    int CodeType = Converter.GetInteger(txtCodeType.Text);

                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(AccClass, CodeType));
                    if (getDTO.record > 0)
                    {
                        ddlCodeType.SelectedValue = Converter.GetString(getDTO.PayTypeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCrUnion_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnAmountSign_Click(object sender, EventArgs e)
        {
            if (lblAmtSign.Text == "1")
            {
                BtnAmountSign.Text = "Negative";
                lblAmtSign.Text = "2";
                BtnAmountSign.BackColor = Color.Red;
            }
            else 
            {
                BtnAmountSign.Text = "Positive";
                lblAmtSign.Text = "1";
                BtnAmountSign.BackColor = Color.Green;
            }
        }
    }
}


