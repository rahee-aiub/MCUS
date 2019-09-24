using ATOZWEBMCUS.WebSessionStore;
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
using DataAccessLayer.BLL;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLTransactionReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PFlag = (string)Session["ProgFlag"];

                CtrlProgFlag.Text = PFlag;

                Tellerdropdown();
                GLCodeDropdown();

                if (CtrlProgFlag.Text != "1")
                {

                    lblModule.Text = Request.QueryString["a%b"];
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    int GLCode = Converter.GetInteger(hdnCashCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        hdnCashCodeDesc.Text = Converter.GetString(getDTO.GLAccDesc);
                    }


                    A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);
                    rbtGLDetail.Checked = true;

                    ChkAllTeller.Checked = true;

                    if (lblModule.Text == "1")
                    {
                        GLCashCodeDropdown();
                        ChkAllFCashCode.Checked = true;
                        ChkAllFCashCode.Visible = true;
                        ChkAllTrnType.Checked = true;
                        ChkAllTrnType.Visible = true;
                        ChkAllTrnMode.Checked = true;
                        ChkAllTrnMode.Visible = true;
                        ChkAllVchNo.Checked = true;
                        ChkAllVchNo.Visible = true;
                    }

                    ChkAllAmount.Checked = true;
                    txtAmount.Enabled = false;

                    ChkAllGLCode.Checked = true;
                    txtGLCode.Enabled = false;
                    ddlGLCode.Enabled = false;

                    ChkAllTranMode.Checked = true;

                    ChkAllTrans.Checked = true;

                    ddlTranMode.Enabled = false;

                    txtFCashCode.Enabled = false;
                    ddlFCashCode.Enabled = false;
                    txtTrnType.Enabled = false;
                    ddlTrnType.Enabled = false;
                    txtTrnMode.Enabled = false;
                    ddlTrnMode.Enabled = false;
                    txtVchNo.Enabled = false;
                    txtTeller.Enabled = false;
                    ddlTeller.Enabled = false;

                    ddlTrans.Enabled = false;



                    if (lblModule.Text == "6" || lblModule.Text == "7" || lblModule.Text == "4")
                    {
                        lblFCashCode.Visible = false;
                        txtFCashCode.Visible = false;
                        ddlFCashCode.Visible = false;
                        ChkAllFCashCode.Checked = false;
                        ChkAllFCashCode.Visible = false;

                        ChkAllTrnType.Checked = true;
                        ChkAllTrnType.Visible = true;
                        ChkAllTrnMode.Checked = true;
                        ChkAllTrnMode.Visible = true;
                        ChkAllVchNo.Checked = true;
                        ChkAllVchNo.Visible = true;
                    }

                }
                else
                {
                    string RChkAllAmount = (string)Session["SChkAllAmount"];
                    string RtxtAmount = (string)Session["StxtAmount"];

                    string RChkAllFCashCode = (string)Session["SChkAllFCashCode"];
                    string RtxtFCashCode = (string)Session["StxtFCashCode"];
                    string RddlFCashCode = (string)Session["SddlFCashCode"];

                    string RChkAllGLCode = (string)Session["SChkAllGLCode"];
                    string RtxtGLCode = (string)Session["StxtGLCode"];
                    string RddlGLCode = (string)Session["SddlGLCode"];

                    string RChkAllTrnType = (string)Session["SChkAllTrnType"];
                    string RtxtTrnType = (string)Session["StxtTrnType"];
                    string RddlTrnType = (string)Session["SddlTrnType"];

                    string RChkAllTrnMode = (string)Session["SChkAllTrnMode"];
                    string RtxtTrnMode = (string)Session["StxtTrnMode"];
                    string RddlTrnMode = (string)Session["SddlTrnMode"];

                    string RChkAllVchNo = (string)Session["SChkAllVchNo"];
                    string RtxtVchNo = (string)Session["StxtVchNo"];

                    string RChkAllTranMode = (string)Session["SChkAllTranMode"];
                    string RddlTranMode = (string)Session["SddlTranMode"];

                    string RChkAllTeller = (string)Session["SChkAllTeller"];
                    string RtxtTeller = (string)Session["StxtTeller"];
                    string RddlTeller = (string)Session["SddlTeller"];

                    string RChkAllTrans = (string)Session["SChkAllTrans"];
                    string RddlTrans = (string)Session["SddlTrans"];

                    string Rtxtfdate = (string)Session["Stxtfdate"];
                    string Rtxttdate = (string)Session["Stxttdate"];

                    string RrbtGLDetail = (string)Session["SrbtGLDetail"];
                    string RrbtSummary = (string)Session["SrbtSummary"];

                    string RChkValueDate = (string)Session["SChkValueDate"];
                    string RChkVchWise = (string)Session["SChkVchWise"];

                    string RlblModule = (string)Session["SlblModule"];
                    string RhdnCashCode = (string)Session["ShdnCashCode"];
                    string RhdnCashCodeDesc = (string)Session["ShdnCashCodeDesc"];
                    string RhdnID = (string)Session["ShdnID"];



                    lblModule.Text = RlblModule;

                    if (lblModule.Text == "1")
                    {
                        GLCashCodeDropdown();

                    }

                    Tellerdropdown();

                    if (RChkAllAmount == "1")
                    {
                        ChkAllAmount.Checked = true;
                        txtAmount.Text = string.Empty;
                        txtAmount.Enabled = false;
                    }
                    else
                    {
                        ChkAllAmount.Checked = false;
                        txtAmount.Text = RtxtAmount;
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
                        ChkAllFCashCode.Checked = false;
                        txtFCashCode.Text = RtxtFCashCode;
                        ddlFCashCode.SelectedValue = RddlFCashCode;
                    }


                    if (RChkAllGLCode == "1")
                    {
                        ChkAllGLCode.Checked = true;
                        txtGLCode.Text = string.Empty;
                        ddlGLCode.SelectedValue = "-Select-";
                        txtGLCode.Enabled = false;
                        ddlGLCode.Enabled = false;
                    }
                    else
                    {
                        ChkAllGLCode.Checked = false;
                        txtGLCode.Text = RtxtGLCode;
                        ddlGLCode.SelectedValue = RddlGLCode;
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

                    if (RChkAllVchNo == "1")
                    {
                        ChkAllVchNo.Checked = true;
                        txtVchNo.Text = string.Empty;
                        txtVchNo.Enabled = false;
                    }
                    else
                    {
                        ChkAllVchNo.Checked = false;
                        txtVchNo.Text = RtxtTrnType;
                    }

                    if (RChkAllTranMode == "1")
                    {
                        ChkAllTranMode.Checked = true;
                        ddlTranMode.SelectedValue = "-Select-";
                        ddlTranMode.Enabled = false;
                    }
                    else
                    {
                        ChkAllTranMode.Checked = false;
                        ddlTranMode.SelectedValue = RddlTranMode;
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
                    }

                    if (RChkAllTrans == "1")
                    {
                        ChkAllTrans.Checked = true;
                        ddlTrans.SelectedValue = "-Select-";
                        ddlTrans.Enabled = false;
                    }
                    else
                    {
                        ChkAllTrans.Checked = false;
                        ddlTrans.SelectedValue = RddlTrans;
                    }

                    txtfdate.Text = Rtxtfdate;
                    txttdate.Text = Rtxttdate;

                    if (RrbtGLDetail == "1")
                    {
                        rbtGLDetail.Checked = true;
                    }
                    else
                    {
                        rbtGLDetail.Checked = false;
                    }

                    if (RrbtSummary == "1")
                    {
                        rbtSummary.Checked = true;
                    }
                    else
                    {
                        rbtSummary.Checked = false;
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


                    hdnCashCode.Text = RhdnCashCode;
                    hdnCashCodeDesc.Text = RhdnCashCodeDesc;
                    hdnID.Text = RhdnID;

                    if (lblModule.Text == "6" || lblModule.Text == "7" || lblModule.Text == "4")
                    {
                        lblFCashCode.Visible = false;
                        txtFCashCode.Visible = false;
                        ddlFCashCode.Visible = false;
                        ChkAllFCashCode.Checked = false;
                        ChkAllFCashCode.Visible = false;
                    }

                }

            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;

            Session["SChkAllAmount"] = string.Empty;
            Session["StxtAmount"] = string.Empty;

            Session["SChkAllFCashCode"] = string.Empty;
            Session["SChkAllGLCode"] = string.Empty;
            Session["StxtFCashCode"] = string.Empty;
            Session["SddlFCashCode"] = string.Empty;
            Session["SChkAllTrnType"] = string.Empty;
            Session["StxtTrnType"] = string.Empty;
            Session["SddlTrnType"] = string.Empty;

            Session["SChkAllTrnMode"] = string.Empty;
            Session["StxtTrnMode"] = string.Empty;
            Session["SddlTrnMode"] = string.Empty;

            Session["SChkAllVchNo"] = string.Empty;
            Session["StxtVchNo"] = string.Empty;
            Session["SChkAllTranMode"] = string.Empty;
            Session["SddlTranMode"] = string.Empty;
            Session["SChkAllTeller"] = string.Empty;
            Session["StxtTeller"] = string.Empty;
            Session["SddlTeller"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
            Session["SlblModule"] = string.Empty;
            Session["ShdnCashCode"] = string.Empty;
            Session["ShdnCashCodeDesc"] = string.Empty;
            Session["ShdnID.Text"] = string.Empty;
            Session["SrbtGLDetail"] = string.Empty;
            Session["SrbtSummary"] = string.Empty;

            Session["SChkValueDate"] = string.Empty;
            Session["SChkVchWise"] = string.Empty;


        }


        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC ";
            ddlFCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFCashCode, "A2ZGLMCUS");

        }

        protected void GLCodeDropdown()
        {
            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 ORDER BY GLAccDesc ASC";
            ddlGLCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCode, "A2ZGLMCUS");

        }
        private void Tellerdropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS";
            ddlTeller = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTeller, "A2ZCSMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkAllAmount.Checked == false)
                {
                    if (txtAmount.Text == string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction Amount');", true);
                        return;
                    }
                }


                if (ChkAllFCashCode.Checked == false)
                {
                    if (txtFCashCode.Text == string.Empty && txtFCashCode.Enabled == true && lblModule.Text == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Cash Code');", true);
                        return;
                    }
                }


                if (ChkAllGLCode.Checked == false)
                {
                    if (txtGLCode.Text == string.Empty && txtGLCode.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input GL Code');", true);
                        return;
                    }
                }


                if (ChkAllTrnType.Checked == false)
                {
                    if (txtTrnType.Text == string.Empty && txtTrnType.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Trn. Type');", true);
                        return;
                    }

                    if (txtFCashCode.Text == string.Empty && txtFCashCode.Enabled == true && lblModule.Text == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input From Cash Code or Mark Checkbox (For All Cash Code)');", true);
                        return;
                    }

                }


                if (ChkAllTrnMode.Checked == false)
                {
                    if (txtTrnMode.Text == string.Empty && txtTrnMode.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Trn. Mode');", true);
                        return;
                    }
                }

                if (ChkAllTeller.Checked == false)
                {
                    if (txtTeller.Text == string.Empty && txtTeller.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input User Id.');", true);
                        return;
                    }
                }


                if (ChkAllTranMode.Checked == false)
                {
                    if (ddlTranMode.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Transaction func');", true);
                        return;
                    }
                }


                InitializationInputValue();
                SessionStoreValue();

                if (ChkAllFCashCode.Checked)
                {
                    ShowMultiTransactionList();
                    return;
                }
                if (!ChkAllFCashCode.Checked)
                {
                    ShowMultiTransactionList();
                    return;
                }             

                Session["ProgFlag"] = "1";

                //---------------------------------------------------

                if (ChkAllAmount.Checked == true)
                {
                    Session["SChkAllAmount"] = "1";
                }
                else
                {
                    Session["SChkAllAmount"] = "0";
                }

                Session["StxtAmount"] = txtAmount.Text;

                //----------------------------------------------------

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

                //--------------------------------------------------

                if (ChkAllGLCode.Checked == true)
                {
                    Session["SChkAllGLCode"] = "1";
                }
                else
                {
                    Session["SChkAllGLCode"] = "0";
                }

                Session["StxtGLCode"] = txtGLCode.Text;
                Session["SddlGLCode"] = ddlGLCode.SelectedValue;

                //-----------------------------------------------

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

                //-----------------------------------------------

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

                //--------------------------------------------

                if (ChkAllVchNo.Checked == true)
                {
                    Session["SChkAllVchNo"] = "1";
                }
                else
                {
                    Session["SChkAllVchNo"] = "0";
                }

                Session["StxtVchNo"] = txtVchNo.Text;

                //-------------------------------------------------

                if (ChkAllTranMode.Checked == true)
                {
                    Session["SChkAllTranMode"] = "1";
                }
                else
                {
                    Session["SChkAllTranMode"] = "0";
                }

                Session["SddlTranMode"] = ddlTranMode.SelectedValue;

                //----------------------------------------------------

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

                //------------------------------------------

                if (ChkAllTrans.Checked == true)
                {
                    Session["SChkAllTrans"] = "1";
                }
                else
                {
                    Session["SChkAllTrans"] = "0";
                }

                Session["SddlTrans"] = ddlTrans.SelectedValue;

                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                Session["SlblModule"] = lblModule.Text;
                Session["ShdnCashCode"] = hdnCashCode.Text;
                Session["ShdnCashCodeDesc"] = hdnCashCodeDesc.Text;

                Session["ShdnID.Text"] = hdnID.Text;


                //---------------------------------------------

                if (rbtGLDetail.Checked == true)
                {
                    Session["SrbtGLDetail"] = "1";
                }
                else
                {
                    Session["SrbtGLDetail"] = "0";
                }

                if (rbtSummary.Checked == true)
                {
                    Session["SrbtSummary"] = "1";
                }
                else
                {
                    Session["SrbtSummary"] = "0";
                }

                if (ChkValueDate.Checked == true)
                {
                    Session["SChkValueDate"] = "1";
                }
                else
                {
                    Session["SChkValueDate"] = "0";
                }

                if (ChkVchWise.Checked == true)
                {
                    Session["SChkVchWise"] = "1";
                }
                else
                {
                    Session["SChkVchWise"] = "0";
                }


                DateTime fdate = DateTime.ParseExact(txtfdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime tdate = DateTime.ParseExact(txttdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //if (lblModule.Text == "6" || lblModule.Text == "7")
                //{
                //    var prm = new object[4];
                //    prm[0] = hdnCashCode.Value;
                //    prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                //    prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                //    prm[3] = 0;

                //    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateTransactionSingle", prm, "A2ZGLMCUS"));
                //}
                //else
                //{
                var prm = new object[5];
                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                prm[2] = 0;

                if (ChkAllTeller.Checked == true)
                {
                    prm[3] = 0;
                }
                else
                {
                    prm[3] = txtTeller.Text;
                }

                if (ChkAllAmount.Checked == true)
                {
                    prm[4] = 0;
                }
                else
                {
                    prm[4] = txtAmount.Text;
                }
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GlGenerateTransaction", prm, "A2ZGLMCUS"));
                //}


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                int Cashcode;
                if (lblModule.Text == "6" || lblModule.Text == "7")
                {
                    Cashcode = Converter.GetInteger(hdnCashCode.Text);
                }
                else
                {
                    Cashcode = Converter.GetInteger(txtFCashCode.Text);
                }
                int TrnType = Converter.GetInteger(txtTrnType.Text);

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, tdate);


                int GLCode;
                GLCode = Converter.GetInteger(txtGLCode.Text);

                // For Trn Type

                if (ChkAllTrnType.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All ");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                }
                else if (ChkAllTrnType.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlTrnType.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, TrnType);
                }

                // For Vch.No

                if (ChkAllVchNo.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, "All Voucher ");

                }
                else if (ChkAllVchNo.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, txtVchNo.Text);

                }


                if (ChkAllTranMode.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 99);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "ALL Transaction Mode");


                }
                else if (ChkAllTranMode.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, ddlTranMode.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlTranMode.SelectedItem.Text);

                }

                //---------------------------------------  Filtering Data From WFTRANSACTIONLIST According to UI Option Value -------------------------


                if (ChkAllFCashCode.Checked == false)
                {
                    //  Deleted Cashcode record from WFTRANSACTIONLIST has not matching txtFCashCode.Text
                    string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE FromCashCode <> '" + Cashcode + "' AND GLAccNo <> 0";
                    int status = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                }

                if (ChkAllGLCode.Checked == false)
                {
                    //  Deleted Cashcode record from WFTRANSACTIONLIST has not matching txtFCashCode.Text
                    string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE GLAccNo <> '" + GLCode + "'";
                    int status = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                }

                if (ChkAllTrnType.Checked == false)
                {
                    if (ddlTrnType.SelectedValue == "1")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnType <> 1 OR TrnVchType <> 'C'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                    else if (ddlTrnType.SelectedValue == "2")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnType <> 3 OR TrnVchType <> 'C'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                    else if (ddlTrnType.SelectedValue == "3")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnType <> 1 OR TrnVchType = 'C'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                    else if (ddlTrnType.SelectedValue == "4")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnType <> 1";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                    else if (ddlTrnType.SelectedValue == "5")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnType <> 3";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                }


                if (ChkAllTrnMode.Checked == false)
                {
                    if (ddlTrnMode.SelectedValue == "1")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnDrCr = 1";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                    if (ddlTrnMode.SelectedValue == "2")
                    {
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnDrCr = 0";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }
                }


                if (ChkAllVchNo.Checked == false)
                {
                    //  Deleted Voucher No. record from WFTRANSACTIONLIST has not matching  txtVchNo.Text
                    string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE VchNo <> '" + txtVchNo.Text.Trim() + "' AND GLAccNo <> 0";
                    int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                }


                if (ChkAllTranMode.Checked == false)
                {
                    Int16 nTrnMode = Converter.GetSmallInteger(ddlTranMode.SelectedValue);

                    if (nTrnMode == 1)
                    {
                        //  Deleted Transacton Mode record from WFTRANSACTIONLIST has not matching  ddlTranMode.SelectedValue
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnCSGL <> '" + 0 + "'  AND GLAccNo <> 0";
                        int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }

                    if (nTrnMode == 2)
                    {
                        //  For All Part : Deleted  record which code has no Transacion  Amount including Openning Balance
                        string sqlQueryDelete = "DELETE FROM  WFTRANSACTIONLIST  WHERE TrnCSGL <> '" + 1 + "' AND GLAccNo <> 0";
                        int status3 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQueryDelete, "A2ZGLMCUS"));
                    }

                }


                if (ChkAllTrans.Checked)
                {
                    lblTrans.Text = "0";
                }
                else
                {
                    lblTrans.Text = ddlTrans.SelectedValue;
                }

                int Trans = Converter.GetInteger(lblTrans.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, Trans);


                //----------------------------------------------------------------------------------------------------------------------------------------

                if (ChkAllFCashCode.Checked && (rbtGLDetail.Checked) && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionDetailList");
                }

                else if (ChkAllFCashCode.Checked == false && (rbtGLDetail.Checked) && lblModule.Text == "1" && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlFCashCode.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, Cashcode);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionDetailList");
                }
                else if (ChkAllFCashCode.Checked == false && (rbtGLDetail.Checked) && (lblModule.Text == "6" || lblModule.Text == "7" || lblModule.Text == "4") && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
                {

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, hdnCashCodeDesc.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, hdnCashCode.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionDetailList");
                }
                else if ((rbtGLDetail.Checked) && ChkValueDate.Checked == true && ChkVchWise.Checked == false)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionValueDtDetailList");
                }

                else if ((rbtGLDetail.Checked) && ChkValueDate.Checked == false && ChkVchWise.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionDetailListByVch");
                }


                else if (rbtSummary.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionSummaryList");
                }

                else if (rbtConsolidated.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionSummaryListByGLCode");
                }

                else if (ChkAllFCashCode.Checked == false && rbtSummary.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 3);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlFCashCode.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, Cashcode);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionSummaryListByCashCode");
                }

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void ChkAllAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllAmount.Checked)
            {
                txtAmount.Enabled = false;
                txtAmount.Text = string.Empty;
            }
            else
            {
                txtAmount.Enabled = true;
                txtAmount.Focus();
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

        protected void ChkAllGLCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllGLCode.Checked)
            {
                txtGLCode.Enabled = false;
                ddlGLCode.Enabled = false;
            }
            else
            {
                txtGLCode.Enabled = true;
                ddlGLCode.Enabled = true;
                txtGLCode.Focus();
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


        protected void txtGLCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLCode.Text != string.Empty)
                {
                    int GLCode;
                    A2ZCGLMSTDTO getDTO = new A2ZCGLMSTDTO();
                    string input1 = Converter.GetString(txtGLCode.Text).Length.ToString();
                    if (input1 == "6")
                    {
                        GLCode = Converter.GetInteger(txtGLCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                    }
                    else
                    {
                        GLCode = Converter.GetInteger(txtGLCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));
                    }

                    if (getDTO.GLAccNo > 0)
                    {
                        if (getDTO.GLRecType == 2)
                        {
                            txtGLCode.Text = Converter.GetString(getDTO.GLAccNo);
                            ddlGLCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                        }
                        else
                        {
                            txtGLCode.Text = string.Empty;
                            txtGLCode.Focus();
                            InvalidGLCodeMsg();
                            return;
                        }
                    }
                    else
                    {
                        txtGLCode.Text = string.Empty;
                        txtGLCode.Focus();
                        InvalidGLCodeMsg();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGLCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlGLCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCode.Text = Converter.GetString(getDTO.GLAccNo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void txtTrnType_TextChanged(object sender, EventArgs e)
        {


            try
            {
                ddlTrnType.SelectedValue = txtTrnType.Text;
            }
            catch (Exception ex)
            {
                ddlTrnType.SelectedIndex = 0;
                txtTrnType.Text = string.Empty;

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

        protected void ChkAllTranMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTranMode.Checked)
            {

                ddlTranMode.Enabled = false;
            }
            else
            {
                ddlTranMode.Enabled = true;
                ddlTranMode.Enabled = true;
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

        protected void ChkAllTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllTrans.Checked)
            {
                ddlTrans.Enabled = false;
            }
            else
            {
                ddlTrans.Enabled = true;
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
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ChkValueDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkValueDate.Checked == true)
            {
                ChkVchWise.Checked = false;
            }
        }

        protected void ChkVchWise_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVchWise.Checked == true)
            {
                ChkValueDate.Checked = false;
            }
        }

        protected void SessionStoreValue()
        {
            Session["ProgFlag"] = "1";

            //---------------------------------------------------

            if (ChkAllAmount.Checked == true)
            {
                Session["SChkAllAmount"] = "1";
            }
            else
            {
                Session["SChkAllAmount"] = "0";
            }

            Session["StxtAmount"] = txtAmount.Text;

            //----------------------------------------------------

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

            //--------------------------------------------------

            if (ChkAllGLCode.Checked == true)
            {
                Session["SChkAllGLCode"] = "1";
            }
            else
            {
                Session["SChkAllGLCode"] = "0";
            }

            Session["StxtGLCode"] = txtGLCode.Text;
            Session["SddlGLCode"] = ddlGLCode.SelectedValue;

            //-----------------------------------------------

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

            //-----------------------------------------------

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

            //--------------------------------------------

            if (ChkAllVchNo.Checked == true)
            {
                Session["SChkAllVchNo"] = "1";
            }
            else
            {
                Session["SChkAllVchNo"] = "0";
            }

            Session["StxtVchNo"] = txtVchNo.Text;

            //-------------------------------------------------

            if (ChkAllTranMode.Checked == true)
            {
                Session["SChkAllTranMode"] = "1";
            }
            else
            {
                Session["SChkAllTranMode"] = "0";
            }

            Session["SddlTranMode"] = ddlTranMode.SelectedValue;

            //----------------------------------------------------

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

            //------------------------------------------

            if (ChkAllTrans.Checked == true)
            {
                Session["SChkAllTrans"] = "1";
            }
            else
            {
                Session["SChkAllTrans"] = "0";
            }

            Session["SddlTrans"] = ddlTrans.SelectedValue;



            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;

            Session["SlblModule"] = lblModule.Text;
            Session["ShdnCashCode"] = hdnCashCode.Text;
            Session["ShdnCashCodeDesc"] = hdnCashCodeDesc.Text;

            Session["ShdnID.Text"] = hdnID.Text;


            //---------------------------------------------

            if (rbtGLDetail.Checked == true)
            {
                Session["SrbtGLDetail"] = "1";
            }
            else
            {
                Session["SrbtGLDetail"] = "0";
            }

            if (rbtSummary.Checked == true)
            {
                Session["SrbtSummary"] = "1";
            }
            else
            {
                Session["SrbtSummary"] = "0";
            }

            if (ChkValueDate.Checked == true)
            {
                Session["SChkValueDate"] = "1";
            }
            else
            {
                Session["SChkValueDate"] = "0";
            }

            if (ChkVchWise.Checked == true)
            {
                Session["SChkVchWise"] = "1";
            }
            else
            {
                Session["SChkVchWise"] = "0";
            }

            return;
        }

        protected void ShowMultiTransactionList()
        {
            int Cashcode;
            if (lblModule.Text == "6" || lblModule.Text == "7")
            {
                Cashcode = Converter.GetInteger(hdnCashCode.Text);
            }
            else
            {
                Cashcode = Converter.GetInteger(txtFCashCode.Text);
            }

            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;

            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));
            
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 1);

            if (ChkAllFCashCode.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CASH_CODE, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CASH_CODE, Cashcode);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 2);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlFCashCode.SelectedItem.Text);
            }

            if (ChkAllGLCode.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCOUNTCODE, 0);   
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCOUNTCODE, txtGLCode.Text);   
            }

            if (ChkAllTrnType.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_TYPE, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All");
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_TYPE, txtTrnType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 2);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlTrnType.SelectedItem.Text);
            }

            if (ChkAllTrnMode.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_MODE, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_MODE, txtTrnMode.Text);
            }

            if (ChkAllVchNo.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, "All Voucher ");
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, txtVchNo.Text);
            }

            if (ChkAllTranMode.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CS_GL_TRN, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "ALL Transaction Mode");
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_CS_GL_TRN, ddlTranMode.SelectedIndex);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlTranMode.SelectedItem.Text);
            }

            if (ChkAllTeller.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_USER_ID, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_USER_ID, txtTeller.Text);
            }

            if (ChkAllTrans.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_AUTO_TRN, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_AUTO_TRN, ddlTrans.SelectedIndex);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, lblTrans.Text);
            }

            if (ChkAllAmount.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_AMT, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRAN_AMT, txtAmount.Text);
            }

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

            //......................................Multiuser Store Procedure...........................

            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            //----------------------------------------------------------------------------------------------------------------------------------------

            if (ChkAllFCashCode.Checked && (rbtGLDetail.Checked) && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionDetailList");
            }

            else if (ChkAllFCashCode.Checked == false && (rbtGLDetail.Checked) && lblModule.Text == "1" && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionDetailList");
            }
            else if (ChkAllFCashCode.Checked == false && (rbtGLDetail.Checked) && (lblModule.Text == "6" || lblModule.Text == "7" || lblModule.Text == "4") && ChkValueDate.Checked == false && ChkVchWise.Checked == false)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionDetailList");
            }
            else if ((rbtGLDetail.Checked) && ChkValueDate.Checked == true && ChkVchWise.Checked == false)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionValueDtDetailList");
            }
            else if ((rbtGLDetail.Checked) && ChkValueDate.Checked == false && ChkVchWise.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionDetailListByVch");
            }
            else if (rbtSummary.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionSummaryList");
            }
            else if (rbtConsolidated.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 2);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionSummaryListByGLCode");
            }

            else if (ChkAllFCashCode.Checked == false && rbtSummary.Checked)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 3);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMGLTransactionSummaryListByCashCode");

            }

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

            Response.Redirect("ReportServer.aspx", false);
        }

        protected void InitializationInputValue()
        {
            if(ChkAllFCashCode.Checked)
            {
                txtFCashCode.Text = string.Empty;
            }

            if(ChkAllGLCode.Checked)
            {
                txtGLCode.Text = string.Empty;
            }

            if(ChkAllTrnType.Checked)
            {
                txtTrnType.Text = string.Empty;
            }

            if(ChkAllTrnMode.Checked)
            {
                txtTrnMode.Text = string.Empty;
            }

            if(ChkAllVchNo.Checked)
            {
                txtVchNo.Text = string.Empty;
            }

            if(ChkAllTeller.Checked)
            {
                txtTeller.Text = string.Empty;
            }

            if(ChkAllAmount.Checked)
            {
                txtAmount.Text = string.Empty;
            }

        }

       
    }


}