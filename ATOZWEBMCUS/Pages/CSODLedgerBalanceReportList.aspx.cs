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
    public partial class CSODLedgerBalanceReportList : System.Web.UI.Page
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
                            txtCrUnion.Focus();
                            ChkAllCrUnion.Checked = true;
                            ChkAllMemNo.Checked = true;


                            //ChkAllContract.Visible = false;
                        }




                        txtCrUnion.Enabled = false;


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
                        string RlblCuName = (string)Session["SlblCuName"];

                                             

                        string RlblCuType = (string)Session["SlblCuType"];
                        string RlblCuNo = (string)Session["SlblCuNo"];

                        string RChkAllMemNo = (string)Session["SChkAllMemNo"];
                        string RtxtMemNo = (string)Session["StxtMemNo"];
                        string RlblMemName = (string)Session["SlblMemName"];



                        string RlblModule = (string)Session["SlblModule"];

                        string RlblAccTypeClass = (string)Session["SlblAccTypeClass"];

                        string RlblAccAccessFlag = (string)Session["SlblAccAccessFlag"];

                        lblModule.Text = RlblModule;

                        lblAccTypeClass.Text = RlblAccTypeClass;
                        lblAccAccessFlag.Text = RlblAccAccessFlag;


                        if (lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7")
                        {
                            Acc1dropdown();

                        }


                        txtDate.Text = RtxtDate;
                        txtAccType.Text = RtxtAccType;
                        ddlAcType.SelectedValue = RddlAcType;



                        if (RChkAllCrUnion == "1")
                        {
                            ChkAllCrUnion.Checked = true;
                            txtCrUnion.Enabled = false;
                            txtCrUnion.Text = string.Empty;
                            lblCuName.Text = string.Empty;

                        }
                        else
                        {
                            ChkAllCrUnion.Checked = false;
                            txtCrUnion.Enabled = true;

                            txtCrUnion.Text = RtxtCrUnion;
                            lblCuName.Text = RlblCuName;
                           
                            lblCuType.Text = RlblCuType;
                            lblCuNo.Text = RlblCuNo;
                        }

                        if (RChkAllMemNo == "1")
                        {
                            ChkAllMemNo.Checked = true;
                            txtMemNo.Enabled = false;
                            txtMemNo.Text = string.Empty;
                            lblMemName.Text = string.Empty;
                        }
                        else
                        {
                            ChkAllMemNo.Checked = false;
                            txtMemNo.Enabled = true;
                            txtMemNo.Text = RtxtMemNo;
                            lblMemName.Text = RlblMemName;
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

            Session["SChkAllMemNo"] = string.Empty;
            Session["StxtMemNo"] = string.Empty;

            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;



            Session["SlblModule"] = string.Empty;
        }


        private void Acc1dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode !='2' AND AccTypeClass = 5 ORDER BY AccTypeClass";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
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
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);


                        if (lblModule.Text == "6" && hdnCashCode.Text != lblCashCode.Text)
                        {
                            txtCrUnion.Text = string.Empty;
                            txtCrUnion.Focus();
                            InvalidCuNoMSG();
                            return;
                        }

                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuType.Text = Converter.GetString(getDTO.CuType);



                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCrUnion_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemNo.Text != string.Empty)
                {
                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(txtMemNo.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        lblMemName.Text = getDTO.MemberName;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCrUnion_TextChanged Problem');</script>");
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

                        if (lblAccTypeClass.Text != "5")
                        {
                            InvalidAccTypeMSG();
                            txtAccType.Focus();
                            return;
                        }
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);


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


                if (txtCrUnion.Text == string.Empty && ChkAllCrUnion.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark Credit Union Check Box / Input Credit Union No.');", true);
                    return;
                }

                if (txtMemNo.Text == string.Empty && ChkAllMemNo.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark Depositor Check Box / Input Depositor No.');", true);
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
                Session["SlblCuName"] = lblCuName.Text;

                



                Session["SlblCuType"] = lblCuType.Text;
                Session["SlblCuNo"] = lblCuNo.Text;

                if (ChkAllMemNo.Checked == true)
                {
                    Session["SChkAllMemNo"] = "1";
                }
                else
                {
                    Session["SChkAllMemNo"] = "0";
                }

                Session["StxtMemNo"] = txtMemNo.Text;


                Session["SlblModule"] = lblModule.Text;
                Session["SlblMemName"] = lblMemName.Text;

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);



                int cutype = Converter.GetInteger(lblCuType.Text);
                int CUNO = Converter.GetInteger(lblCuNo.Text);


                int Module = Converter.GetInteger(lblModule.Text);
                int CashCode = Converter.GetInteger(lblCashCode.Text);

                if (ChkOnlyBalance.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 1);
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                }

                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCTYPE, txtAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtDate.Text));



                // Credit Unin
                if (ChkAllCrUnion.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CUNO);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblCuName.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, lblCuType.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, lblCuNo.Text);
                }


                if (ChkAllMemNo.Checked)
                {
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, 0);
                }
                else
                {
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CUNO);
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblCuName.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, txtMemNo.Text);
                }


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlAcType.SelectedValue);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, Converter.GetDateToYYYYMMDD(txtDate.Text));
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4,ddlAccStatus.SelectedValue);
                // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4,ddlAccStatus.SelectedItem.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO9, Module);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO10, CashCode);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMCSODLedgerBalance");


                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void ChkAllCrUnion_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCrUnion.Checked)
            {
                txtCrUnion.Enabled = false;
            }
            else
            {
                txtCrUnion.Enabled = true;

                txtCrUnion.Focus();
            }
        }

        protected void ChkAllMemNo_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkAllMemNo.Checked)
            {
                txtMemNo.Enabled = false;
            }
            else
            {
                txtMemNo.Enabled = true;
                txtMemNo.Focus();
            }
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }







    }
}


