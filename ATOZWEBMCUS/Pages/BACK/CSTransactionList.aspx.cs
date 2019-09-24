using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSTransactionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    string PFlag = (string)Session["ProgFlag"];

                    CtrlProgFlag.Text = PFlag;

                    Tellerdropdown();

                    if (CtrlProgFlag.Text != "1")
                    {
                        ddlSysTrans.Visible = false;
                        lblModule.Text = Request.QueryString["a%b"];

                        ChkAll99Func.Visible = false;
                        lbl99Func.Visible = false;
                        txt99Func.Visible = false;
                        ddl99Func.Visible = false;


                        if (lblModule.Text == "4")
                        {
                            Acc2dropdown();
                            lblCreditUnion.Visible = false;
                            txtCreditUNo.Visible = false;
                            ddlCreditUNo.Visible = false;

                            ChkAllCrUnion.Checked = false;
                            ChkAllCrUnion.Visible = false;

                            lblFCashCode.Visible = false;
                            txtFCashCode.Visible = false;
                            ddlFCashCode.Visible = false;

                            ChkAllFCashCode.Checked = false;
                            ChkAllFCashCode.Visible = false;

                            ChkAllTrans.Visible = false;
                            Label2.Visible = false;
                            ddlTrans.Visible = false;
                            ddlSysTrans.Visible = false;

                            ChkValueDate.Visible = false;
                            ChkVchWise.Visible = false;

                        }

                        if (lblModule.Text == "1")
                        {
                            CreditUnionDropdown();
                            Acc1dropdown();
                            GLCashCodeDropdown();
                            Tellerdropdown();
                            ChkAllCrUnion.Checked = true;
                            ChkAllFCashCode.Checked = true;
                        }

                        if (lblModule.Text == "6" || lblModule.Text == "7")
                        {
                            CreditUnionDropdown();
                            Acc1dropdown();

                            lblFCashCode.Visible = false;
                            txtFCashCode.Visible = false;
                            ddlFCashCode.Visible = false;

                            ChkAllFCashCode.Checked = false;
                            ChkAllFCashCode.Visible = false;
                            ChkAllCrUnion.Checked = true;
                        }

                        ChkAllTrnType.Checked = true;
                        ChkAllVchNo.Checked = true;
                        ChkAllTeller.Checked = true;
                        ChkAllTrnMode.Checked = true;
                        ChkAllTrnNature.Checked = true;
                        chkAllAccType.Checked = true;
                        ChkAllTrans.Checked = true;

                        txtAccType.Focus();

                        txtAccType.Enabled = false;
                        ddlAcType.Enabled = false;


                        txtCreditUNo.Enabled = false;
                        ddlCreditUNo.Enabled = false;

                        txtFCashCode.Enabled = false;
                        ddlFCashCode.Enabled = false;

                        txtTrnType.Enabled = false;
                        ddlTrnType.Enabled = false;

                        txtTeller.Enabled = false;
                        ddlTeller.Enabled = false;

                        txtTrnMode.Enabled = false;
                        ddlTrnMode.Enabled = false;

                        txtTrnNature.Enabled = false;
                        ddlTrnNature.Enabled = false;

                        ddlTrans.Enabled = false;

                        txtVchNo.Enabled = false;

                        lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                        hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                        int GLCode = Converter.GetInteger(hdnCashCode.Text);
                        A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                        if (getDTO.GLAccNo > 0)
                        {
                            hdnCashCodeDesc.Text = Converter.GetString(getDTO.GLAccDesc);
                        }

                        A2ZERPSYSPRMDTO dto1 = A2ZERPSYSPRMDTO.GetParameterValue();
                        lblBegFinYear.Text = Converter.GetString(dto1.PrmBegFinYear);

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtfdate.Text = Converter.GetString(date);
                        txttdate.Text = Converter.GetString(date);
                        lblProcDate.Text = Converter.GetString(date);

                        FunctionName();
                    }
                    else
                    {
                        string RchkAllAccType = (string)Session["SchkAllAccType"];
                        string RtxtAccType = (string)Session["StxtAccType"];
                        string RddlAcType = (string)Session["SddlAcType"];
                        string RlblAccClass = (string)Session["SlblAccClass"];

                        string RChkAll99Func = (string)Session["SChkAll99Func"];
                        string Rtxt99Func = (string)Session["Stxt99Func"];
                        string Rddl99Func = (string)Session["Sddl99Func"];

                        string RChkAllCrUnion = (string)Session["SChkAllCrUnion"];
                        string RtxtCreditUNo = (string)Session["StxtCreditUNo"];
                        string RddlCreditUNo = (string)Session["SddlCreditUNo"];

                        string RlblCuType = (string)Session["SlblCuType"];
                        string RlblCuNo = (string)Session["SlblCuNo"];

                        string RChkAllFCashCode = (string)Session["SChkAllFCashCode"];
                        string RtxtFCashCode = (string)Session["StxtFCashCode"];
                        string RddlFCashCode = (string)Session["SddlFCashCode"];

                        string RChkAllTrnType = (string)Session["SChkAllTrnType"];
                        string RtxtTrnType = (string)Session["StxtTrnType"];
                        string RddlTrnType = (string)Session["SddlTrnType"];


                        string RChkAllTrnMode = (string)Session["SChkAllTrnMode"];
                        string RtxtTrnMode = (string)Session["StxtTrnMode"];
                        string RddlTrnMode = (string)Session["SddlTrnMode"];

                        string RChkAllTrnNature = (string)Session["SChkAllTrnNature"];
                        string RtxtTrnNature = (string)Session["StxtTrnNature"];
                        string RddlTrnNature = (string)Session["SddlTrnNature"];


                        string RChkAllTeller = (string)Session["SChkAllTeller"];
                        string RtxtTeller = (string)Session["StxtTeller"];
                        string RddlTeller = (string)Session["SddlTeller"];


                        string RChkAllVchNo = (string)Session["SChkAllVchNo"];
                        string RtxtVchNo = (string)Session["StxtVchNo"];

                        string RChkAllTrans = (string)Session["SChkAllTrans"];
                        string RddlTrans = (string)Session["SddlTrans"];

                        string RddlSysTrans = (string)Session["SddlSysTrans"];


                        string RChkValueDate = (string)Session["SChkValueDate"];

                        string RChkVchWise = (string)Session["SChkVchWise"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string RlblModule = (string)Session["SlblModule"];
                        string RhdnCashCode = (string)Session["ShdnCashCode"];
                        string RhdnCashCodeDesc = (string)Session["ShdnCashCodeDesc"];
                        string RlblBegFinYear = (string)Session["SlblBegFinYear"];
                        string RlblProcDate = (string)Session["SlblProcDate"];

                        string RlblID = (string)Session["SlblID"];

                        lblID.Text = RlblID;

                        lblModule.Text = RlblModule;

                        if (lblModule.Text == "4")
                        {
                            Acc2dropdown();
                            lblCreditUnion.Visible = false;
                            txtCreditUNo.Visible = false;
                            ddlCreditUNo.Visible = false;

                            ChkAllCrUnion.Checked = false;
                            ChkAllCrUnion.Visible = false;

                            lblFCashCode.Visible = false;
                            txtFCashCode.Visible = false;
                            ddlFCashCode.Visible = false;

                            ChkAllFCashCode.Checked = false;
                            ChkAllFCashCode.Visible = false;

                            ChkAllTrans.Visible = false;
                            Label2.Visible = false;
                            ddlTrans.Visible = false;
                            ddlSysTrans.Visible = false;
                        }

                        if (lblModule.Text == "1")
                        {
                            CreditUnionDropdown();
                            Acc1dropdown();
                            GLCashCodeDropdown();
                            ChkAllCrUnion.Checked = true;
                            ChkAllFCashCode.Checked = true;
                        }

                        if (lblModule.Text == "6" || lblModule.Text == "7")
                        {
                            CreditUnionDropdown();
                            Acc1dropdown();

                            lblFCashCode.Visible = false;
                            txtFCashCode.Visible = false;
                            ddlFCashCode.Visible = false;

                            ChkAllFCashCode.Checked = false;
                            ChkAllFCashCode.Visible = false;
                            ChkAllCrUnion.Checked = true;
                        }


                        if (RchkAllAccType == "1")
                        {
                            chkAllAccType.Checked = true;
                            txtAccType.Text = string.Empty;
                            ddlAcType.SelectedValue = "-Select-";
                            txtAccType.Enabled = false;
                            ddlAcType.Enabled = false;
                        }
                        else
                        {
                            chkAllAccType.Checked = false;
                            txtAccType.Text = RtxtAccType;
                            ddlAcType.SelectedValue = RddlAcType;
                            lblAccClass.Text = RlblAccClass;

                           
                            if (lblModule.Text == "4")
                            {
                                Acc2dropdown();
                            }
                            else
                            {
                                Acc1dropdown();
                            }
                        }

                        if (txtAccType.Text == "99")
                        {
                            if (RChkAll99Func == "1")
                            {
                                ChkAll99Func.Checked = true;
                                txt99Func.Text = string.Empty;
                                ddl99Func.SelectedValue = "-Select-";
                                txt99Func.Enabled = false;
                                ddl99Func.Enabled = false;
                            }
                            else
                            {
                                ChkAll99Func.Checked = false;
                                txt99Func.Text = Rtxt99Func;
                                ddl99Func.SelectedValue = Rddl99Func;
                                Func99dropdown();
                            }
                        }
                        else 
                        {
                            ChkAll99Func.Visible = false;
                            lbl99Func.Visible = false;
                            txt99Func.Visible = false;
                            ddl99Func.Visible = false;   
                        }


                        if (RChkAllCrUnion == "1")
                        {
                            ChkAllCrUnion.Checked = true;
                            txtCreditUNo.Text = string.Empty;
                            ddlCreditUNo.SelectedValue = "-Select-";
                            txtCreditUNo.Enabled = false;
                            ddlCreditUNo.Enabled = false;
                        }
                        else
                        {
                            if (lblModule.Text == "1")
                            {
                                ChkAllCrUnion.Checked = false;
                                txtCreditUNo.Text = RtxtCreditUNo;
                                ddlCreditUNo.SelectedValue = RddlCreditUNo;
                                lblCuType.Text = RlblCuType;
                                lblCuNo.Text = RlblCuNo;
                               
                                CreditUnionDropdown();
                            }
                        }

                        if (RChkAllFCashCode == "1")
                        {
                            ChkAllFCashCode.Checked = true;
                            txtFCashCode.Text = string.Empty;
                            ddlFCashCode.SelectedValue = "-Select-";
                            txtFCashCode.Enabled = false;
                            ddlFCashCode.Enabled = false;
                        }
                        else
                        {
                            if (lblModule.Text == "1")
                            {
                                ChkAllFCashCode.Checked = false;
                                txtFCashCode.Text = RtxtFCashCode;
                                ddlFCashCode.SelectedValue = RddlFCashCode;
                                if (lblModule.Text == "1")
                                {
                                    GLCashCodeDropdown();
                                }
                            }
                        }

                        if (RChkAllTrnType == "1")
                        {
                            ChkAllTrnType.Checked = true;
                            txtTrnType.Text = string.Empty;
                            ddlTrnType.SelectedValue = "-Select-";
                            txtTrnType.Enabled = false;
                            ddlTrnType.Enabled = false;
                        }
                        else
                        {
                            ChkAllTrnType.Checked = false;
                            txtTrnType.Text = RtxtTrnType;
                            ddlTrnType.SelectedValue = RddlTrnType;
                        }

                        if (RChkAllTrnMode == "1")
                        {
                            ChkAllTrnMode.Checked = true;
                            txtTrnMode.Text = string.Empty;
                            ddlTrnMode.SelectedValue = "-Select-";
                            txtTrnMode.Enabled = false;
                            ddlTrnMode.Enabled = false;
                        }
                        else
                        {
                            ChkAllTrnMode.Checked = false;
                            txtTrnMode.Text = RtxtTrnMode;
                            ddlTrnMode.SelectedValue = RddlTrnMode;
                        }


                        if (RChkAllTrnNature == "1")
                        {
                            ChkAllTrnNature.Checked = true;
                            txtTrnNature.Text = string.Empty;
                            ddlTrnNature.SelectedValue = "-Select-";
                            txtTrnNature.Enabled = false;
                            ddlTrnNature.Enabled = false;
                        }
                        else
                        {
                            ChkAllTrnNature.Checked = false;
                            txtTrnNature.Text = RtxtTrnNature;
                            ddlTrnNature.SelectedValue = RddlTrnNature;
                        }

                        if (RChkAllTeller == "1")
                        {
                            ChkAllTeller.Checked = true;
                            txtTeller.Text = string.Empty;
                            ddlTeller.SelectedValue = "-Select-";
                            txtTeller.Enabled = false;
                            ddlTeller.Enabled = false;
                        }
                        else
                        {
                            ChkAllTeller.Checked = false;
                            txtTeller.Text = RtxtTeller;
                            ddlTeller.SelectedValue = RddlTeller;

                            Tellerdropdown();
                        }

                        if (RChkAllVchNo == "1")
                        {
                            ChkAllVchNo.Checked = true;
                            txtVchNo.Text = string.Empty;
                            txtVchNo.Enabled = false;
                        }
                        else
                        {
                            ChkAllVchNo.Checked = false;
                            txtVchNo.Text = RtxtVchNo;
                        }

                        if (RChkValueDate == "1")
                        {
                            ChkValueDate.Checked = true;
                        }
                        else
                        {
                            ChkValueDate.Checked = false;
                        }

                        if (RChkVchWise == "1")
                        {
                            ChkVchWise.Checked = true;
                        }
                        else
                        {
                            ChkVchWise.Checked = false;
                        }

                        if (RChkAllTrans == "1")
                        {
                            ChkAllTrans.Checked = true;
                            ddlTrans.SelectedValue = "-Select-";
                            ddlTrans.Enabled = false;
                            ddlSysTrans.Visible = false;
                        }
                        else
                        {
                            ChkAllTrans.Checked = false;
                            ddlTrans.Enabled = true;
                            ddlTrans.SelectedValue = RddlTrans;
                            if (ddlTrans.SelectedValue == "1")
                            {
                                ddlSysTrans.Visible = true;
                                ddlSysTrans.SelectedValue = RddlSysTrans;
                            }
                            else
                            {
                                ddlSysTrans.Visible = false;
                            }
                        }




                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;

                        hdnCashCode.Text = RhdnCashCode;
                        hdnCashCodeDesc.Text = RhdnCashCodeDesc;
                        lblBegFinYear.Text = RlblBegFinYear;
                        lblProcDate.Text = RlblProcDate;

                        FunctionName();

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
            Session["SchkAllAccType"] = string.Empty;
            Session["StxtAccType"] = string.Empty;
            Session["SddlAcType"] = string.Empty;
            Session["SlblAccClass"] = string.Empty;

            Session["SChkAll99Func"] = string.Empty;
            Session["Stxt99Func"] = string.Empty;
            Session["Sddl99Func"] = string.Empty;

            Session["SChkAllCrUnion"] = string.Empty;
            Session["StxtCreditUNo"] = string.Empty;
            Session["SddlCreditUNo"] = string.Empty;

            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;

            Session["SChkAllFCashCode"] = string.Empty;
            Session["StxtFCashCode"] = string.Empty;
            Session["SddlFCashCode"] = string.Empty;
            Session["SChkAllTrnType"] = string.Empty;
            Session["StxtTrnType"] = string.Empty;
            Session["SddlTrnType"] = string.Empty;
            Session["SChkAllTrnMode"] = string.Empty;
            Session["SChkAllTrnMode"] = string.Empty;
            Session["StxtTrnMode"] = string.Empty;
            Session["SChkAllTrnNature"] = string.Empty;
            Session["SChkAllTrnNature"] = string.Empty;
            Session["StxtTrnNature"] = string.Empty;
            Session["SddlTrnMode"] = string.Empty;
            Session["SChkAllTeller"] = string.Empty;
            Session["StxtTeller"] = string.Empty;
            Session["SddlTeller"] = string.Empty;
            Session["SChkAllVchNo"] = string.Empty;
            Session["StxtVchNo"] = string.Empty;
            Session["SChkValueDate"] = string.Empty;

            Session["SChkVchWise"] = string.Empty;

            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SlblModule"] = string.Empty;
            Session["ShdnCashCode"] = string.Empty;
            Session["ShdnCashCodeDesc"] = string.Empty;
            Session["SlblBegFinYear"] = string.Empty;
            Session["SlblProcDate"] = string.Empty;

            Session["SlblID"] = string.Empty;

            Session["SChkAllTrans"] = string.Empty;
            Session["SddlTrans"] = string.Empty;
            Session["SddlSysTrans"] = string.Empty;
        }


        protected void FunctionName()
        {
            if (lblModule.Text == "4")
            {
                lblTransFunction.Text = "Staff Daily Transaction Reports";
            }
            else
            {
                lblTransFunction.Text = "CS Daily Transaction Reports";
            }
        }

        private void Func99dropdown()
        {
            string sqlquery = "SELECT PayType,PayTypeDes from A2ZPAYTYPE WHERE AtyClass ='7' ORDER BY PayType";
            ddl99Func = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddl99Func, "A2ZCSMCUS");
        }

        private void Acc1dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode !='2' ORDER BY AccTypeClass";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }


        private void Acc2dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode !='1'";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlFCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFCashCode, "A2ZGLMCUS");

        }

        private void Tellerdropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlTeller = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTeller, "A2ZCSMCUS");
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
                        lblAccClass.Text = Converter.GetString(getDTO.AccTypeClass);

                        if (txtAccType.Text == "99")
                        {
                            ChkAll99Func.Visible = true;
                            lbl99Func.Visible = true;
                            txt99Func.Visible = true;
                            ddl99Func.Visible = true;
                            ChkAll99Func.Checked = true;
                            txt99Func.Enabled = false;
                            ddl99Func.Enabled = false;
                            Func99dropdown();
                        }
                        else
                        {
                            ChkAll99Func.Visible = false;
                            lbl99Func.Visible = false;
                            txt99Func.Visible = false;
                            ddl99Func.Visible = false;
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


        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccType.Text == string.Empty && txtAccType.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Account Type or Mark Checkbox (For All Account Type)');", true);
                    return;
                }

                if (txtCreditUNo.Text == string.Empty && txtCreditUNo.Enabled == true && lblModule.Text != "4")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union or Mark Checkbox (For All Credit Union');", true);
                    return;
                }

                if (txtFCashCode.Text == string.Empty && txtFCashCode.Enabled == true && lblModule.Text == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input From Cash Code or Mark Checkbox (For All Cash Code)');", true);
                    return;
                }

                if (txtTrnType.Text == string.Empty && txtTrnType.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Trn.Type or Select Trn.Type');", true);
                    return;
                }
                if (txtTrnMode.Text == string.Empty && txtTrnMode.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Trn.Mode or Select Trn.Mode');", true);
                    return;
                }
                if (txtTrnNature.Text == string.Empty && txtTrnNature.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Trn.Nature or Select Trn.Nature');", true);
                    return;
                }
                if (txtTeller.Text == string.Empty && txtTeller.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input User Id. or Select User Id.');", true);
                    return;
                }
                if (txtVchNo.Text == string.Empty && txtVchNo.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
                    return;
                }

                if (ddlTrans.SelectedIndex == 0 && ddlTrans.Enabled == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Normal Transaction');", true);
                    return;
                }

                if (ddlSysTrans.SelectedIndex == 0 && ddlTrans.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Auto System Transaction');", true);
                    return;
                }



                Session["ProgFlag"] = "1";

                if (chkAllAccType.Checked == true)
                {
                    Session["SchkAllAccType"] = "1";
                }
                else
                {
                    Session["SchkAllAccType"] = "0";
                }

                Session["StxtAccType"] = txtAccType.Text;
                Session["SddlAcType"] = ddlAcType.SelectedValue;
                Session["SlblAccClass"] = lblAccClass.Text;
                //------------------------------------

                if (ChkAll99Func.Checked == true)
                {
                    Session["SChkAll99Func"] = "1";
                }
                else
                {
                    Session["SChkAll99Func"] = "0";
                }

                Session["Stxt99Func"] = txt99Func.Text;
                Session["Sddl99Func"] = ddl99Func.SelectedValue;
                //------------------------------------


                if (ChkAllCrUnion.Checked == true)
                {
                    Session["SChkAllCrUnion"] = "1";
                }
                else
                {
                    Session["SChkAllCrUnion"] = "0";
                }

                Session["StxtCreditUNo"] = txtCreditUNo.Text;
                Session["SddlCreditUNo"] = ddlCreditUNo.SelectedValue;

                Session["SlblCuType"] = lblCuType.Text;
                Session["SlblCuNo"] = lblCuNo.Text;

                //----------------------------------------------
                if (ChkAllFCashCode.Checked == true)
                {
                    Session["SChkAllFCashCode"] = "1";
                }
                else
                {
                    Session["SChkAllFCashCode"] = "0";
                }

                Session["StxtFCashCode"] = txtFCashCode.Text;
                Session["SddlFCashCode"] = ddlFCashCode.SelectedValue;
                //----------------------------------------------
                if (ChkAllTrnType.Checked == true)
                {
                    Session["SChkAllTrnType"] = "1";
                }
                else
                {
                    Session["SChkAllTrnType"] = "0";
                }
                Session["StxtTrnType"] = txtTrnType.Text;
                Session["SddlTrnType"] = ddlTrnType.SelectedValue;
                //-----------------------------------------

                if (ChkAllTrnMode.Checked == true)
                {
                    Session["SChkAllTrnMode"] = "1";
                }
                else
                {
                    Session["SChkAllTrnMode"] = "0";
                }
                Session["StxtTrnMode"] = txtTrnMode.Text;
                Session["SddlTrnMode"] = ddlTrnMode.SelectedValue;
                //---------------------------------------

                if (ChkAllTrnNature.Checked == true)
                {
                    Session["SChkAllTrnNature"] = "1";
                }
                else
                {
                    Session["SChkAllTrnNature"] = "0";
                }
                Session["StxtTrnNature"] = txtTrnNature.Text;
                Session["SddlTrnNature"] = ddlTrnNature.SelectedValue;
                //---------------------------------------
                if (ChkAllTeller.Checked == true)
                {
                    Session["SChkAllTeller"] = "1";
                }
                else
                {
                    Session["SChkAllTeller"] = "0";
                }
                Session["StxtTeller"] = txtTeller.Text;
                Session["SddlTeller"] = ddlTeller.SelectedValue;
                //---------------------------------------
                if (ChkAllVchNo.Checked == true)
                {
                    Session["SChkAllVchNo"] = "1";
                }
                else
                {
                    Session["SChkAllVchNo"] = "0";
                }
                Session["StxtVchNo"] = txtVchNo.Text;

                //---------------------------------------
                if (ChkValueDate.Checked == true)
                {
                    Session["SChkValueDate"] = "1";
                }
                else
                {
                    Session["SChkValueDate"] = "0";
                }

                if (ChkAllTrans.Checked == true)
                {
                    Session["SChkAllTrans"] = "1";
                }
                else
                {
                    Session["SChkAllTrans"] = "0";
                }

                Session["SddlTrans"] = ddlTrans.SelectedValue;

                if (ChkVchWise.Checked == true)
                {
                    Session["SChkVchWise"] = "1";
                }
                else
                {
                    Session["SChkVchWise"] = "0";
                }

                Session["SddlSysTrans"] = ddlSysTrans.SelectedValue;



                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                Session["SlblModule"] = lblModule.Text;

                Session["ShdnCashCode"] = hdnCashCode.Text;
                Session["ShdnCashCodeDesc"] = hdnCashCodeDesc.Text;
                Session["SlblBegFinYear"] = lblBegFinYear.Text;
                Session["SlblProcDate"] = lblProcDate.Text;

                Session["SlblID"] = lblID.Text;


                if (ddlTrans.SelectedValue == "1")
                {
                    if (ddlSysTrans.SelectedValue == "1")
                    {
                        lblAutoVchNo.Text = "ProvisionCPS";
                    }
                    else if (ddlSysTrans.SelectedValue == "2")
                    {
                        lblAutoVchNo.Text = "Provision6YR";
                    }
                    else if (ddlSysTrans.SelectedValue == "3")
                    {
                        lblAutoVchNo.Text = "Provision6YR";
                    }
                    else if (ddlSysTrans.SelectedValue == "4")
                    {
                        lblAutoVchNo.Text = "AnniversaryCPS";
                    }
                    else if (ddlSysTrans.SelectedValue == "5")
                    {
                        lblAutoVchNo.Text = "AnniversaryFDR";
                    }
                    else if (ddlSysTrans.SelectedValue == "6")
                    {
                        lblAutoVchNo.Text = "Anniversary6YR";
                    }
                    else if (ddlSysTrans.SelectedValue == "7")
                    {
                        lblAutoVchNo.Text = "RenewalFDR";
                    }
                    else if (ddlSysTrans.SelectedValue == "8")
                    {
                        lblAutoVchNo.Text = "Renewal6YR";
                    }
                    else if (ddlSysTrans.SelectedValue == "9")
                    {
                        lblAutoVchNo.Text = "BenefitMSplus";
                    }
                    else if (ddlSysTrans.SelectedValue == "10")
                    {
                        lblAutoVchNo.Text = "InterestPost";
                    }
                    else if (ddlSysTrans.SelectedValue == "11")
                    {
                        lblAutoVchNo.Text = "DevidentPost";
                    }
                }



                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                int fYear = fdate.Year;
                int bYear = Converter.GetInteger(lblBegFinYear.Text);

                if (fYear < bYear)
                {
                    txtfdate.Text = lblProcDate.Text;
                    txtfdate.Focus();
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Invalid From Date Input');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid From Date Input');", true);
                    return;
                }

                var prm = new object[17];
                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                prm[2] = 0;

                if (chkAllAccType.Checked == true)
                {
                    prm[3] = 0;
                    prm[4] = 0;
                }
                else
                {
                    prm[3] = txtAccType.Text;
                    prm[4] = lblAccClass.Text;
                }

                if (txtAccType.Text == "99")
                {
                    if (ChkAll99Func.Checked == true)
                    {
                        prm[5] = 0;
                    }
                    else
                    {
                        prm[5] = txt99Func.Text;
                    }
                }
                else
                {
                    prm[5] = 0;
                }


                if (ChkAllCrUnion.Checked == true || lblModule.Text == "4")
                {
                    prm[6] = 0;
                    prm[7] = 0;
                }
                else
                {
                    prm[6] = lblCuType.Text;
                    prm[7] = lblCuNo.Text;
                }
                if (ChkAllFCashCode.Checked == true)
                {
                    prm[8] = 0;
                }
                else
                {
                    if (lblModule.Text == "4" || lblModule.Text == "6" || lblModule.Text == "7")
                    {
                        prm[8] = hdnCashCode.Text;
                    }
                    else
                    {
                        prm[8] = txtFCashCode.Text;
                    }
                }
                if (ChkAllTrnType.Checked == true)
                {
                    prm[9] = 0;
                }
                else
                {
                    prm[9] = txtTrnType.Text;
                }
                if (ChkAllVchNo.Checked == true)
                {
                    prm[10] = "0";
                }
                else
                {
                    prm[10] = txtVchNo.Text;
                }

                if (ChkAllTrans.Checked == false && ddlSysTrans.Visible == true)
                {
                    prm[10] = lblAutoVchNo.Text;
                }


                if (ChkAllTeller.Checked == true)
                {
                    prm[11] = 0;
                }
                else
                {
                    prm[11] = txtTeller.Text;
                }
                if (ChkAllTrnMode.Checked == true)
                {
                    prm[12] = 0;
                }
                else
                {
                    prm[12] = txtTrnMode.Text;
                }

                if (ChkAllTrnNature.Checked == true)
                {
                    prm[13] = 0;
                }
                else
                {
                    prm[13] = txtTrnNature.Text;
                }

                prm[14] = lblModule.Text;

                if (ChkAllTrans.Checked == true)
                {
                    prm[15] = 0;
                }
                else
                if (ddlTrans.SelectedValue == "1")
                {
                    prm[15] = 1;
                }
                

                prm[16] = Converter.GetInteger(lblID.Text);
               

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateTransactionList", prm, "A2ZCSMCUS"));


                int accType = Converter.GetSmallInteger(txtAccType.Text);
                int CreditUNo = Converter.GetSmallInteger(lblCuNo.Text);

                int Cashcode;

                if (lblModule.Text == "4" || lblModule.Text == "6" || lblModule.Text == "7")
                {
                    Cashcode = Converter.GetInteger(hdnCashCode.Text);
                    ddlFCashCode.SelectedItem.Text = hdnCashCodeDesc.Text;

                }
                else
                {
                    Cashcode = Converter.GetInteger(txtFCashCode.Text);
                }


                int TrnType = Converter.GetInteger(txtTrnType.Text);
                // string txt = "All Account Type";
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);    

                if (ChkAllTrans.Checked)
                {
                    lblTrans.Text = "0";
                }
                else
                {
                    lblTrans.Text = ddlTrans.SelectedValue;
                }


                if ((ChkAllTrnNature.Checked == false || ChkAllVchNo.Checked == false) && ChkAllTrans.Checked == true)
                {
                    lblTrans.Text = "2";
                }

                int Trans = Converter.GetInteger(lblTrans.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, Trans);


                if (chkAllAccType.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All Account Type ");
                    //  SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);
                }
                else
                {

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlAcType.SelectedItem.Text);

                }

                if (ChkAllCrUnion.Checked && lblModule.Text != "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All Credit Unioin ");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, CreditUNo);
                }
                else if (ChkAllCrUnion.Checked == false && lblModule.Text != "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, CreditUNo);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlCreditUNo.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, CreditUNo);
                }
                // For All Code Only
                if (ChkAllFCashCode.Checked && lblModule.Text != "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All");

                }
                else if (ChkAllFCashCode.Checked == false && lblModule.Text != "4")
                // For Cash Code Only
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, Cashcode);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlFCashCode.SelectedItem.Text);

                }
                // For Trn Type

                if (ChkAllTrnType.Checked && lblModule.Text != "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "All ");

                }
                else if (ChkAllTrnType.Checked == false && lblModule.Text != "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, TrnType);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlTrnType.SelectedItem.Text);

                }
                // For Vch.No

                if (ddlSysTrans.Visible == true)
                {
                    //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, lblAutoVchNo.Text);

                }
                else
                    if (ChkAllVchNo.Checked && lblModule.Text != "4")
                    {
                        //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, "All Voucher ");

                    }
                    else if (ChkAllVchNo.Checked == false && lblModule.Text != "4")
                    {
                        //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 2);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, txtVchNo.Text);

                    }




                if (lblModule.Text != "4")
                {

                    // FOR Credit Union Type
                    int CuType = Converter.GetSmallInteger(lblCuType.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUPTYE, CuType);
                }

                int UId = Converter.GetInteger(lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, UId);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");

                if (lblModule.Text != "4" && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, ddlSysTrans.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTransactionDetailList");
                }

                if (lblModule.Text != "4" && ChkValueDate.Checked == false && ChkVchWise.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, ddlSysTrans.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTransactionDetailListByVch");
                }

                if (lblModule.Text != "4" && ChkValueDate.Checked == true && ChkVchWise.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSTransactionValueDtDetailList");
                }

                if (lblModule.Text == "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStaffTransactionDetailList");
                }
                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }

        private void InvalidAccTypeMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
            return;
        }


        private void InvalidCreditUnionMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;
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
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                        lblAccClass.Text = Converter.GetString(getDTO.AccTypeClass);

                        if (txtAccType.Text == "99")
                        {
                            ChkAll99Func.Visible = true;
                            lbl99Func.Visible = true;
                            txt99Func.Visible = true;
                            ddl99Func.Visible = true;
                            ChkAll99Func.Checked = true;
                            txt99Func.Enabled = false;
                            ddl99Func.Enabled = false;
                            txt99Func.Text = string.Empty;
                            Func99dropdown();
                        }
                        else
                        {
                            ChkAll99Func.Visible = false;
                            lbl99Func.Visible = false;
                            txt99Func.Visible = false;
                            ddl99Func.Visible = false;
                        }
                    }
                    else 
                    {
                        txtAccType.Text = string.Empty;
                        txtAccType.Focus();
                        InvalidAccTypeMsg();
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




        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' ORDER BY CuName ASC";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
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
                    if (getDTO.CreditUnionNo > 0)
                    {
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "' ORDER BY CuName ASC";
                        ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(txtCreditUNo.Text);
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                    }
                    else
                    {
                        txtCreditUNo.Text = string.Empty;
                        ddlCreditUNo.SelectedValue = "-Select-";
                        txtCreditUNo.Focus();
                        InvalidCreditUnionMsg();
                        return;
                        

                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCreditUNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCreditUNo.SelectedValue == "-Select-")
                {

                    txtCreditUNo.Text = string.Empty;
                    return;
                }


                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
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

                    if (getDTO.CreditUnionNo > 0)
                    {

                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);

                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;


                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCreditUNo_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void chkAllAccType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllAccType.Checked)
            {
                txtAccType.Enabled = false;
                ddlAcType.Enabled = false;
            }
            else
            {
                txtAccType.Enabled = true;
                ddlAcType.Enabled = true;
                ddlAcType.Focus();
            }
        }


        protected void ChkAll99Func_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll99Func.Checked)
            {
                txt99Func.Enabled = false;
                ddl99Func.Enabled = false;
            }
            else
            {
                txt99Func.Enabled = true;
                ddl99Func.Enabled = true;
                txt99Func.Focus();
            }
        }

        protected void ChkAllCrUnion_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCrUnion.Checked)
            {
                txtCreditUNo.Enabled = false;
                ddlCreditUNo.Enabled = false;


            }
            else
            {
                txtCreditUNo.Enabled = true;
                ddlCreditUNo.Enabled = true;
                txtCreditUNo.Focus();
            }
        }

        private void InvalidGLCodeMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Cash Code');", true);
            return;
        }
        protected void txtFCashCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtFCashCode.Text != string.Empty)
                {
                    
                    int GLCode;
                    A2ZCGLMSTDTO getDTO = new A2ZCGLMSTDTO();
                    string input1 = Converter.GetString(txtFCashCode.Text).Length.ToString();
                    if (input1 == "6")
                    {
                        GLCode = Converter.GetInteger(txtFCashCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                    }
                    else
                    {
                        GLCode = Converter.GetInteger(txtFCashCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));
                    }

                    
                    if (getDTO.GLAccNo > 0)
                    {

                        if (getDTO.GLRecType == 2 && getDTO.GLSubHead == 10101000)
                        {
                            txtFCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                            ddlFCashCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                        }
                        else
                        {
                            txtFCashCode.Text = string.Empty;
                            txtFCashCode.Focus();
                            InvalidGLCodeMsg();
                            return;
                        }
                    }
                    else
                    {
                        txtFCashCode.Text = string.Empty;
                        txtFCashCode.Focus();
                        InvalidGLCodeMsg();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddl99Func_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt99Func.Text = ddl99Func.SelectedValue;
        }

        protected void ddlFCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlFCashCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlFCashCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtFCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ChkAllFCashCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllFCashCode.Checked)
            {
                txtFCashCode.Enabled = false;
                ddlFCashCode.Enabled = false;
            }
            else
            {
                txtFCashCode.Enabled = true;
                ddlFCashCode.Enabled = true;
                txtFCashCode.Focus();
            }

        }

        protected void ChkAllTrnType_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTrnType.Checked)
            {
                txtTrnType.Enabled = false;
                ddlTrnType.Enabled = false;

            }
            else
            {
                txtTrnType.Enabled = true;
                ddlTrnType.Enabled = true;
                txtTrnType.Focus();
            }


        }

        private void InvalidTrnTypeMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Transaction Type');", true);
            return;
        }


        protected void txtTrnType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                ddlTrnType.SelectedValue = txtTrnType.Text;


            }
            catch (Exception ex)
            {
                
                InvalidTrnTypeMsg();
                ddlTrnType.SelectedIndex = 0;
                txtTrnType.Text = string.Empty;
            }
        }

        protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTrnType.SelectedValue == "-Select-")
            {
                txtTrnType.Focus();

                txtTrnType.Text = string.Empty;

            }

            try
            {


                if (ddlTrnType.SelectedValue != "-Select-")
                {


                    {
                        //txtTrnType.Text = Converter.GetString(getDTO.AccTypeCode);
                        txtTrnType.Text = Converter.GetString(ddlTrnType.SelectedValue);

                        //Converter.GetSmallInteger(ddlAcType.SelectedValue)
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ChkAllVchNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllVchNo.Checked)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Enabled = false;

            }
            else
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Enabled = true;
            }

        }

        protected void ChkAllTeller_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTeller.Checked)
            {
                txtTeller.Enabled = false;
                ddlTeller.Enabled = false;
            }
            else
            {
                txtTeller.Enabled = true;
                ddlTeller.Enabled = true;
                txtTeller.Focus();
            }
        }

        protected void ddlTeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlTeller.SelectedValue != "-Select-")
                {
                    txtTeller.Text = Converter.GetString(ddlTeller.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InvalidTellerMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Teller No.');", true);
            return;
        }
        protected void txtTeller_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtTeller.Text != string.Empty)
                {

                    string qry1 = "SELECT IdsNo,IdsName from A2ZSYSIDS WHERE IdsNo = '" + txtTeller.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        ddlTeller.SelectedValue = Converter.GetString(txtTeller.Text);
                    }
                    else
                    {
                        txtTeller.Text = string.Empty;
                        txtTeller.Focus();
                        InvalidTellerMsg();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ChkAllTrnMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTrnMode.Checked)
            {
                txtTrnMode.Enabled = false;
                ddlTrnMode.Enabled = false;
            }
            else
            {
                txtTrnMode.Enabled = true;
                ddlTrnMode.Enabled = true;
                txtTrnMode.Focus();
            }
        }

        protected void ChkAllTrnNature_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTrnNature.Checked)
            {
                txtTrnNature.Enabled = false;
                ddlTrnNature.Enabled = false;
                txtTrnNature.Text = string.Empty;
                ddlTrnNature.SelectedIndex = 0;
            }
            else
            {
                txtTrnNature.Enabled = true;
                ddlTrnNature.Enabled = true;
                txtTrnNature.Focus();
            }
        }
        protected void ChkAllTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTrans.Checked)
            {
                ddlTrans.Enabled = false;
                ddlTrans.SelectedIndex = 0;
                ddlSysTrans.Visible = false;
            }
            else
            {
                ddlTrans.Enabled = true;
            }
        }

        private void InvalidTrnModeMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Transaction Mode');", true);
            return;
        }
        protected void txtTrnMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ddlTrnMode.SelectedValue = txtTrnMode.Text;
            }
            catch (Exception ex)
            {
                ddlTrnMode.SelectedIndex = 0;
                txtTrnMode.Text = string.Empty;
                InvalidTrnModeMsg();
            }
        }

        protected void ddlTrnMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrnMode.SelectedValue == "-Select-")
            {
                txtTrnMode.Focus();
                txtTrnMode.Text = string.Empty;
            }

            try
            {

                if (ddlTrnMode.SelectedValue != "-Select-")
                {
                    txtTrnMode.Text = Converter.GetString(ddlTrnMode.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InvalidTrnNatureMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Transaction Nature');", true);
            return;
        }
        protected void txtTrnNature_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ddlTrnNature.SelectedValue = txtTrnMode.Text;
            }
            catch (Exception ex)
            {
                ddlTrnNature.SelectedIndex = 0;
                txtTrnNature.Text = string.Empty;
                txtTrnNature.Focus();
                InvalidTrnNatureMsg();
            }
        }

        protected void ddlTrnNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrnNature.SelectedValue == "-Select-")
            {
                txtTrnNature.Focus();
                txtTrnNature.Text = string.Empty;
            }

            try
            {

                if (ddlTrnNature.SelectedValue != "-Select-")
                {
                    txtTrnNature.Text = Converter.GetString(ddlTrnNature.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrans.SelectedValue == "1" && ddlTrans.Enabled == true)
            {
                ddlSysTrans.Visible = true;
            }
            else
            {
                ddlSysTrans.Visible = false;
            }
        }




    }
}