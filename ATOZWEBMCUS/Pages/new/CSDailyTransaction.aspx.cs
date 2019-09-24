using ATOZWEBMCUS.WebSessionStore;

using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSDailyTransaction : System.Web.UI.Page
    {
        public double Principal = 0;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CtrlPrmValue.Value = Request.QueryString["a%b"];
                string b = CtrlPrmValue.Value;
                lblFuncOpt.Value = b.Substring(0, 1);
                CtrlModule.Value = b.Substring(1, 1);

                CtrlIntFlag.Value = "0";
                CtrlBenefitFlag.Value = "0";
                CtrlInterestAmt.Value = "0";
                CtrlMsgFlag.Value = "0";
                ClearHstInfo();



                if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                {
                    GLContraCodeDropdown();
                    lblGLCashCode.Visible = false;
                    txtGLCashCode.Visible = false;
                    ddlGLCashCode.Visible = false;
                }
                else
                {
                    GLCashCodeDropdown();
                    lblGLContraCode.Visible = false;
                    txtGLContraCode.Visible = false;
                    ddlGLContraCode.Visible = false;
                }

                if (CtrlModule.Value == "1")
                {
                    lblCuName.Visible = false;
                    lblMemName.Visible = false;
                    lblTrnCodeName.Visible = false;
                    CreditUnionDropdown();
                    TransactionCode1Dropdown();
                    txtCreditUNo.Focus();
                }

                if (CtrlModule.Value == "4")
                {
                    lblCuName.Visible = false;
                    lblMemName.Visible = false;
                    lblTrnCodeName.Visible = false;
                    lblOldCuNo.Visible = false;
                    txtOldCuNo.Visible = false;
                    lblCUNum.Visible = false;
                    txtCreditUNo.Visible = false;
                    ddlCreditUNo.Visible = false;
                    lblMemNo.Text = "Staff Code";
                    MemberDropdown();
                    TransactionCode2Dropdown();
                    txtMemNo.Focus();
                }

                if (CtrlModule.Value == "6")
                {
                    lblCuName.Visible = true;
                    lblMemName.Visible = true;
                    ddlCreditUNo.Visible = false;
                    ddlMemNo.Visible = false;
                    TransactionCode1Dropdown();
                    lblTrnCodeName.Visible = false;
                    //ddlTrnCode.Visible = false;
                    lblGLCashCode.Visible = false;
                    txtGLCashCode.Visible = false;
                    ddlGLCashCode.Visible = false;
                    lblGLContraCode.Visible = false;
                    txtGLContraCode.Visible = false;
                    ddlGLContraCode.Visible = false;
                    txtCreditUNo.Focus();
                }
                FunctionName();
                BtnUpdate.Visible = false;

                txtCreditUNo.ReadOnly = false;
                ddlCreditUNo.Enabled = true;
                txtMemNo.ReadOnly = false;
                ddlMemNo.Enabled = true;

                BtnAdd.Visible = false;
                BtnCancel.Visible = false;


                lblTotalAmt.Visible = false;

                txtTrnType1.ReadOnly = true;
                txtTrnType2.ReadOnly = true;
                txtTrnType3.ReadOnly = true;
                txtTrnType4.ReadOnly = true;


                VisibleFalse();

                if (lblFuncOpt.Value == "3")
                {
                    BtnViewImage.Visible = true;
                }
                else
                {
                    BtnViewImage.Visible = false;
                }

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtTranDate.Text = date;
                CtrlProcDate.Value = date;

                //txtTranDate.ReadOnly = true;

                hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnCashCode.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                //txtGLCashCode.Text = hdnCashCode.Value;
                //ddlGLCashCode.SelectedValue = Converter.GetString(hdnCashCode.Value);

                //txtIdNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                string sqlquery;
                sqlquery = @"DELETE dbo.WF_Transaction WHERE UserID='" + hdnID.Value + "'";

                int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));

                if (result > 0)
                {

                }

            }
        }

        protected void FunctionName()
        {
            if (lblFuncOpt.Value == "1")
            {
                lblTransFunction.Text = "Cash In Transaction - DEPOSIT";
            }
            if (lblFuncOpt.Value == "2")
            {
                lblTransFunction.Text = "Cash In Transaction - LOAN SETTLEMENT";
            }
            if (lblFuncOpt.Value == "3")
            {
                lblTransFunction.Text = "Cash Out Transaction - WITHDRAWAL";
            }
            if (lblFuncOpt.Value == "4")
            {
                lblTransFunction.Text = "Cash Out Transaction - INTEREST/BENEFIT WITHDRAWAL";
            }
            if (lblFuncOpt.Value == "5")
            {
                lblTransFunction.Text = "Cash Out Transaction - LOAN DISBURSEMENT";
            }
            if (lblFuncOpt.Value == "6")
            {
                lblTransFunction.Text = "Cash Out Transaction - ENCASHMENT";
            }
            if (lblFuncOpt.Value == "7")
            {
                lblTransFunction.Text = "Transfer Credit - ADJUSTMENT";
            }
            if (lblFuncOpt.Value == "8")
            {
                lblTransFunction.Text = "Transfer Debit - ADJUSTMENT";
            }
        }
        protected void VisibleFalse()
        {
            txtTrnType1.Visible = true;
            lblPayDesc1.Visible = true;
            txtAmount1.Visible = true;

            txtTrnType2.Visible = false;
            lblPayDesc2.Visible = false;
            txtAmount2.Visible = false;

            txtTrnType3.Visible = false;
            lblPayDesc3.Visible = false;
            txtAmount3.Visible = false;

            txtTrnType4.Visible = false;
            lblPayDesc4.Visible = false;
            txtAmount4.Visible = false;

        }
        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

            

        }

        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + "0" + "'and CuType='" + "0" + "' GROUP BY MemNo,MemName";
            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");
        }

        private void DepositorDropdown()
        {

            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCuNo.Value + "'and CuType='" + lblCuType.Value + "' GROUP BY MemNo,MemName";
            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");
        }


        protected void AccountNoDropdown()
        {

            string sqlquery = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccStatus !=99";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccNo, "A2ZCSMCUS");


        }
        protected void TransactionCode1Dropdown()
        {

            //string sqlquery = "SELECT distinct TrnCode,TrnDesc from A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' AND AccTypeMode !='2'";

            string sqlquery = @"SELECT distinct TrnCode,+ CAST (TrnCode AS VARCHAR(100))+ '-' + LTRIM(RTRIM(TrnDesc)) from A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' AND AccTypeMode !='2'";
            ddlTrnCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTrnCode, "A2ZCSMCUS");

        }

        protected void TransactionCode2Dropdown()
        {

            //string sqlquery = "SELECT distinct TrnCode,TrnDesc from A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Text + "' AND AccTypeMode !='1'";

            string sqlquery = @"SELECT distinct TrnCode,+ CAST (TrnCode AS VARCHAR(100))+ '-' + LTRIM(RTRIM(TrnDesc)) from A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' AND AccTypeMode !='1'";

            ddlTrnCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTrnCode, "A2ZCSMCUS");

        }
        protected void GLCashCodeDropdown()
        {

            //string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";

            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";

            ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLMCUS");

        }

        protected void GLContraCodeDropdown()
        {

            // string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2";
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2";
            ddlGLContraCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLContraCode, "A2ZGLMCUS");

        }
        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    InitializedRecords();

                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Value = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Value = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    if (getDTO.CreditUnionNo > 0)
                    {
                        lblCuStatus.Value = Converter.GetString(getDTO.CuStatus);
                        if (lblCuStatus.Value == "9")
                        {
                            TransferCuNoMSG();
                            ddlMemNo.SelectedIndex = 0;
                            txtMemNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }

                        if (CtrlModule.Value == "1")
                        {
                            ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Value + lblCuNo.Value);
                        }

                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        txtOldCuNo.Text = Converter.GetString(getDTO.OldCuNo);

                        txtCreditUNo.Text = (c + "-" + d);
                        //txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        if (CtrlModule.Value == "6" && lblFuncOpt.Value != "1" && txtGLCashCode.Text != hdnCashCode.Value)
                        {
                            InvalidCuNoMSG();

                            ddlMemNo.SelectedIndex = 0;
                            txtMemNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }

                        string qry = "SELECT MemNo,MemName FROM A2ZMEMBER where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count == 1)
                            {
                                if (CtrlModule.Value == "1")
                                {
                                    DepositorDropdown();
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    ddlMemNo.SelectedValue = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtGLCashCode.Focus();
                                    }
                                }
                                else
                                {
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    lblMemName.Text = Converter.GetString(dt.Rows[0]["MemName"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtTrnCode.Focus();
                                    }
                                }
                            }
                            else
                            {
                                DepositorDropdown();
                                txtMemNo.Text = string.Empty;
                                ddlMemNo.SelectedValue = "-Select-";
                                txtMemNo.Focus();
                            }

                        }
                        else
                        {
                            InvalidCuNoMSG();
                            txtMemNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            if (CtrlModule.Value == "1")
                            {
                                ddlCreditUNo.SelectedValue = "-Select-";
                                ddlMemNo.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        InvalidCuNoMSG();
                        txtMemNo.Text = string.Empty;
                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
                        if (CtrlModule.Value == "1")
                        {
                            ddlCreditUNo.SelectedValue = "-Select-";
                            ddlMemNo.SelectedIndex = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCreditUNo.SelectedValue == "-Select-")
            {

                txtCreditUNo.Text = string.Empty;
                txtMemNo.Focus();
                ddlMemNo.SelectedIndex = 0;
                return;
            }

            try
            {

                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    InitializedRecords();

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
                    lblCuType.Value = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Value = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = (c + "-" + d);
                        //txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        string qry = "SELECT MemNo,MemName FROM A2ZMEMBER where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count == 1)
                            {
                                if (CtrlModule.Value == "1")
                                {
                                    DepositorDropdown();
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    ddlMemNo.SelectedValue = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtGLCashCode.Focus();
                                    }
                                }
                                else
                                {
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    lblMemName.Text = Converter.GetString(dt.Rows[0]["MemName"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtTrnCode.Focus();
                                    }
                                }
                            }
                            else
                            {
                                DepositorDropdown();
                                txtMemNo.Text = string.Empty;
                                ddlMemNo.SelectedValue = "-Select-";
                                txtMemNo.Focus();
                            }

                        }

                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMemNo.SelectedValue == "-Select-")
                {

                }

                if (txtMemNo.Text != string.Empty && ddlCreditUNo.SelectedValue != "-Select-")
                {

                    //ddlMemNo.SelectedValue = txtMemNo.Text;



                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Value);
                    int MemNumber = Converter.GetInteger(txtMemNo.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        if (CtrlModule.Value != "6")
                        {
                            ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        }
                        lblMemName.Text = Converter.GetString(getDTO.MemberName);

                        txtGLCashCode.Focus();
                    }
                    else
                    {
                        InvalidMemberMSG();
                        txtMemNo.Text = string.Empty;
                        txtMemNo.Focus();
                        if (CtrlModule.Value != "6")
                        {
                            ddlMemNo.SelectedValue = "-Select-";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemNo.SelectedValue != "-Select-" && ddlCreditUNo.SelectedValue != "-Select-")
            {
                //txtMemNo.Text = ddlMemNo.SelectedValue;

                Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
                int CNo = Converter.GetSmallInteger(lblCuNo.Value);
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    txtGLCashCode.Focus();
                }
            }

        }

        private void InvalidGlCode()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Not Trans. Header Record');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        protected void txtGLCashCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLCashCode.Text != string.Empty)
                {

                    //ddlGLCashCode.SelectedValue = txtGLCashCode.Text;

                    int GLCode = Converter.GetInteger(txtGLCashCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        CtrlRecType.Value = Converter.GetString(getDTO.GLRecType);
                        if (CtrlRecType.Value != "2")
                        {
                            InvalidGlCode();
                            txtGLCashCode.Text = string.Empty;
                            txtGLCashCode.Focus();
                            return;
                        }
                        else
                        {
                            txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                            ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                            txtTrnCode.Focus();
                        }
                    }
                    else
                    {
                        txtGLCashCode.Text = string.Empty;
                        txtGLCashCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGLCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLCashCode.SelectedValue != "-Select-")
                {
                    //txtGLCashCode.Text = ddlGLCashCode.SelectedValue;
                    //txtTrnCode.Focus();


                    int GLCode = Converter.GetInteger(ddlGLCashCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                        txtTrnCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void txtGLContraCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLContraCode.Text != string.Empty)
                {
                    int GLCode = Converter.GetInteger(txtGLContraCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLContraCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLContraCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                        txtTrnCode.Focus();
                    }
                    else
                    {
                        txtGLContraCode.Text = string.Empty;
                        txtGLContraCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGLContraCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLContraCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlGLContraCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLContraCode.Text = Converter.GetString(getDTO.GLAccNo);
                        txtTrnCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtTrnCode_TextChanged(object sender, EventArgs e)
        {
            if (ddlTrnCode.SelectedValue == "-Select-")
            {
                txtTrnCode.Focus();

            }
            try
            {
                if (txtTrnCode.Text != string.Empty)
                {
                    VerifyDuplicateRec();
                    if (CtrlMsgFlag.Value == "1")
                    {
                        txtTrnCode.Focus();
                        return;
                    }


                    int MainCode = Converter.GetInteger(txtTrnCode.Text);
                    A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));

                    if (getDTO.TrnCode > 0)
                    {
                        lblATypeMode.Value = Converter.GetString(getDTO.AccTypeMode);
                        if ((CtrlModule.Value == "1" || CtrlModule.Value == "6") && lblATypeMode.Value == "2")
                        {
                            InvalidTranCodeMSG();
                            txtTrnCode.Text = string.Empty;
                            ddlTrnCode.SelectedValue = "-Select-";
                            txtTrnCode.Focus();
                            return;
                        }

                        if (CtrlModule.Value == "4" && lblATypeMode.Value == "1")
                        {
                            InvalidTranCodeMSG();
                            txtTrnCode.Text = string.Empty;
                            ddlTrnCode.SelectedValue = "-Select-";
                            txtTrnCode.Focus();
                            return;
                        }

                        lblAType.Value = Converter.GetString(getDTO.AccType);
                        txtTrnCode.Text = Converter.GetString(getDTO.TrnCode);
                        ddlTrnCode.SelectedValue = Converter.GetString(getDTO.TrnCode);
                        lblTrnCodeName.Text = Converter.GetString(getDTO.TrnDescription);

                        Int16 AccType = Converter.GetSmallInteger(lblAType.Value);
                        A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get1DTO.AccTypeCode > 0)
                        {
                            txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
                            lblcls.Value = Converter.GetString(get1DTO.AccTypeClass);
                        }
                        string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccStatus !=99";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {

                            if (dt.Rows.Count == 1)
                            {
                                AccountNoDropdown();
                                txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                                ddlAccNo.SelectedValue = Converter.GetString(dt.Rows[0]["AccNo"]);
                                AccGetInfo();
                            }
                            else
                            {
                                AccountNoDropdown();
                                txtAccNo.Text = string.Empty;
                                ddlAccNo.SelectedValue = "-Select-";
                                txtAccNo.Focus();
                            }
                        }
                        else
                        {
                            InvalidAccountNoMSG();
                            txtTrnCode.Text = string.Empty;
                            txtAccType.Text = string.Empty;
                            ddlTrnCode.SelectedValue = "-Select-";
                            txtTrnCode.Focus();
                            return;
                        }
                    }
                    else
                    {
                        InvalidTranCodeMSG();
                        txtTrnCode.Text = string.Empty;
                        ddlTrnCode.SelectedValue = "-Select-";
                        txtTrnCode.Focus();
                        return;
                    }


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlTrnCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlTrnCode.SelectedValue != "-Select-")
                {
                    txtTrnCode.Text = Converter.GetString(ddlTrnCode.SelectedValue);
                    VerifyDuplicateRec();
                    if (CtrlMsgFlag.Value == "1")
                    {
                        txtTrnCode.Focus();
                        return;
                    }

                    int MainCode = Converter.GetInteger(ddlTrnCode.SelectedValue);
                    A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));
                    if (getDTO.TrnCode > 0)
                    {
                        lblAType.Value = Converter.GetString(getDTO.AccType);
                        txtTrnCode.Text = Converter.GetString(getDTO.TrnCode);

                        Int16 AccType = Converter.GetSmallInteger(lblAType.Value);
                        A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                        if (get1DTO.AccTypeCode > 0)
                        {
                            txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
                            lblcls.Value = Converter.GetString(get1DTO.AccTypeClass);
                        }

                        string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccStatus !=99";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {

                            if (dt.Rows.Count == 1)
                            {
                                AccountNoDropdown();
                                txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                                ddlAccNo.SelectedValue = Converter.GetString(dt.Rows[0]["AccNo"]);
                                AccGetInfo();
                            }
                            else
                            {
                                AccountNoDropdown();
                                txtAccNo.Text = string.Empty;
                                ddlAccNo.SelectedValue = "-Select-";
                                txtAccNo.Focus();
                            }
                        }
                        else
                        {
                            InvalidAccountNoMSG();
                            txtTrnCode.Text = string.Empty;
                            txtAccType.Text = string.Empty;
                            ddlTrnCode.SelectedValue = "-Select-";
                            txtTrnCode.Focus();
                            return;
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            //ddlAccNo.SelectedValue = Converter.GetString(txtAccNo.Text);
            AccGetInfo();
        }


        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccNo.Text = Converter.GetString(ddlAccNo.SelectedValue);
            if (ddlTrnCode.SelectedValue != "-Select-")
            {
                AccGetInfo();
            }
        }
        protected void AccGetInfo()
        {
            //txtCreditUNo.ReadOnly = true;
            //ddlCreditUNo.Enabled = false;
            //txtMemNo.ReadOnly = true;
            //ddlMemNo.Enabled = false;

            ClearHstInfo();

            CtrlMsgFlag.Value = "0";
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);


            A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInformation(Acctype, AccNumber, CuType, CNo, MemNumber));

            if (accgetDTO.a == 0)
            {
                InvalidAccountNoMSG();

                txtTrnType1.Text = string.Empty;
                txtTrnType2.Text = string.Empty;
                txtTrnType3.Text = string.Empty;
                txtTrnType4.Text = string.Empty;

                lblPayDesc1.Text = string.Empty;
                lblPayDesc2.Text = string.Empty;
                lblPayDesc3.Text = string.Empty;
                lblPayDesc4.Text = string.Empty;

                txtAmount1.Text = string.Empty;
                txtAmount2.Text = string.Empty;
                txtAmount3.Text = string.Empty;
                txtAmount4.Text = string.Empty;

                txtAccNo.Text = string.Empty;
                txtAccNo.Focus();
                return;
            }
            else
            {
                CtrlAccStatus.Value = Converter.GetString(accgetDTO.AccStatus);
                CtrlAccAtyClass.Value = Converter.GetString(accgetDTO.AccAtyClass);
                if (CtrlAccStatus.Value == "98" || CtrlAccStatus.Value == "99" || (CtrlAccStatus.Value == "50" && CtrlAccAtyClass.Value != "1"))
                {
                    AccStatusMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();

                }
                else
                {
                    CtrlLadgerBalance.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Value);
                    CtrlLienAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    double LienAmt = Converter.GetDouble(CtrlLienAmt.Value);

                    CtrlAvailBal.Value = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - LienAmt)));

                    CtrlSancAmount.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.LoanAmount));
                    double SancAmt = Converter.GetDouble(CtrlSancAmount.Value);

                    CtrlAvailLimit.Value = Converter.GetString(String.Format("{0:0,0.00}", (SancAmt + LadgerBalance)));

                    CtrlPrincipal.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccPrincipal));
                    Principal = Converter.GetDouble(CtrlPrincipal.Value);

                    CtrlOrgAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccOrgAmt));
                    double OrgAmt = Converter.GetDouble(CtrlOrgAmt.Value);

                    CtrlAvailInterest.Value = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - OrgAmt)));

                    CtrlAvailBenefit.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccProvBalance));

                    CtrlIntRate.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.InterestRate));


                    DateTime dt = Converter.GetDateTime(accgetDTO.LastTrnDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlLastTrnDate.Value = date;
                    CtrlTotalDeposit.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.TotDepositAmount));
                    CtrlMthDeposit.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.DepositAmount));

                    CtrlInstlAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.MonthlyInstallment));

                    CtrlDisbAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccDisbAmt));

                    CtrlDueIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccDueIntAmt));


                    DateTime dt1 = Converter.GetDateTime(accgetDTO.Opendate);
                    string date1 = dt1.ToString("dd/MM/yyyy");
                    CtrlOpenDate.Value = date1;

                    DateTime dt2 = Converter.GetDateTime(accgetDTO.MatruityDate);
                    string date2 = dt2.ToString("dd/MM/yyyy");
                    CtrlMaturityDate.Value = date2;

                    CtrlPeriod.Value = Converter.GetString(accgetDTO.Period);

                    ddlAccNo.SelectedValue = Converter.GetString(txtAccNo.Text);

                    QryTransControl();

                    DisplayHistoryInfo();

                    if (CtrlMsgFlag.Value == "1")
                    {
                        txtTrnType1.Text = string.Empty;
                        txtTrnType2.Text = string.Empty;
                        txtTrnType3.Text = string.Empty;
                        txtTrnType4.Text = string.Empty;

                        lblPayDesc1.Text = string.Empty;
                        lblPayDesc2.Text = string.Empty;
                        lblPayDesc3.Text = string.Empty;
                        lblPayDesc4.Text = string.Empty;

                        txtAmount1.Text = string.Empty;
                        txtAmount2.Text = string.Empty;
                        txtAmount3.Text = string.Empty;
                        txtAmount4.Text = string.Empty;
                        return;
                    }

                    BtnAdd.Visible = true;
                    BtnCancel.Visible = true;

                    if (txtAmount1.Visible == true)
                    {
                        txtAmount1.Focus();
                    }
                    else
                    {
                        txtAmount2.Focus();
                    }


                }
            }

        }


        protected void UnPostValue()
        {
            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnCredit) AS 'AmountCr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Value + "' AND CuNo ='" + lblCuNo.Value + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + txtAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblUnPostDataCr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountCr"]));
            }

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnDebit) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Value + "' AND CuNo ='" + lblCuNo.Value + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + txtAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblUnPostDataDr.Text = Convert.ToString(String.Format("{0:0,0.00}", dt1.Rows[0]["AmountDr"]));
            }

            double LadgerBalance = Converter.GetDouble(CtrlLadgerBalance.Value);
            double AmtCredit = Converter.GetDouble(lblUnPostDataCr.Text);
            double AmtDebit = Converter.GetDouble(lblUnPostDataDr.Text);

            CtrlLadgerBalance.Value = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - AmtCredit)));

            CtrlAvailBal.Value = CtrlLadgerBalance.Value;

            //CtrlLadgerBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - (AmtCredit - AmtDebit))));

        }
        protected void DisplayHistoryInfo()
        {

            lblData8.Visible = true;
            lblBalData.Visible = true;
            lblUnPostDataCr.Visible = true;
            lblUnPostDataDr.Visible = true;

            UnPostValue();

            lblUnPostCr.Text = "UnPost Cr. :";
            lblUnPostDr.Text = "UnPost Dr. :";




            if (CtrlAccAtyClass.Value == "1")
            {

                lblBalRec.Text = "Ledger Balance :";

                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Value;
                lblRec2.Text = "Interest Rate :";
                lblData2.Text = CtrlIntRate.Value;
                lblRec3.Text = "Last Transaction Date :";
                lblData3.Text = CtrlLastTrnDate.Value;


                lblData4.Enabled = false;
                lblData5.Enabled = false;
                lblData6.Enabled = false;
                lblData7.Enabled = false;
                lblData8.Enabled = false;

            }
            if (CtrlAccAtyClass.Value == "2")
            {
                lblBalRec.Text = "Ledger Balance :";

                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Value;
                lblRec2.Text = "Maturity Date  :";
                lblData2.Text = CtrlMaturityDate.Value;
                lblRec3.Text = "Period(Month)  :";
                lblData3.Text = CtrlPeriod.Value;
                lblRec4.Text = "Orginal Amount :";
                lblData4.Text = CtrlOrgAmt.Value;
                lblData5.Visible = false;
                lblData6.Visible = false;
                lblData7.Visible = false;
                lblData8.Visible = false;


            }
            if (CtrlAccAtyClass.Value == "3")
            {
                lblBalRec.Text = "Ledger Balance :";
                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Value;
                lblRec2.Text = "Maturity Date  :";
                lblData2.Text = CtrlMaturityDate.Value;
                lblRec3.Text = "Orginal Amount :";
                lblData3.Text = CtrlOrgAmt.Value;
                lblRec4.Text = "Avaiable Interest :";
                lblData4.Text = CtrlAvailBenefit.Value;
                lblData5.Visible = false;
                lblData6.Visible = false;
                lblData7.Visible = false;
                lblData8.Visible = false;



            }
            if (CtrlAccAtyClass.Value == "4")
            {
                lblBalRec.Text = "Ledger Balance :";
                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Last Transaction Date :";
                lblData1.Text = CtrlLastTrnDate.Value;
                lblRec2.Text = "Monthly Deposit Amt. :";
                lblData2.Text = CtrlMthDeposit.Value;
                lblRec3.Text = "Total Deposit Amt. :";
                lblData3.Text = CtrlTotalDeposit.Value;
                lblRec4.Text = "Maturity Date :";
                lblData4.Text = CtrlMaturityDate.Value;
                lblData5.Visible = false;
                lblData6.Visible = false;
                lblData7.Visible = false;
                lblData8.Visible = false;

            }
            if (CtrlAccAtyClass.Value == "5")
            {

                lblBalRec.Text = "Outstanding Bal. :";
                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Value;
                lblRec2.Text = "Sanction Amount :";
                lblData2.Text = CtrlSancAmount.Value;
                lblRec3.Text = "Avaiable Limit Balance :";
                lblData3.Text = CtrlAvailLimit.Value;

                lblRec4.Text = "Interest Rate :";
                lblData4.Text = CtrlIntRate.Value;
                lblRec5.Text = "Last Transaction Date :";
                lblData5.Text = CtrlLastTrnDate.Value;

                lblData6.Visible = false;

                lblRec7.Text = "Curr.Interest Amt. :";
                lblData7.Text = CtrlCurrIntAmt.Value;

                lblRec8.Text = "Due Interest Amt. :";
                lblData8.Text = CtrlDueIntAmt.Value;


            }
            if (CtrlAccAtyClass.Value == "6")
            {

                lblBalRec.Text = "Outstanding Bal. :";
                lblBalData.Text = CtrlLadgerBalance.Value;

                lblRec1.Text = "Open Date  :";
                lblData1.Text = CtrlOpenDate.Value;
                lblRec2.Text = "Sanction Amount :";
                lblData2.Text = CtrlSancAmount.Value;
                lblRec3.Text = "Disbursement Amount :";
                lblData3.Text = CtrlDisbAmt.Value;
                lblRec4.Text = "Interest Rate :";
                lblData4.Text = CtrlIntRate.Value;
                lblRec5.Text = "Installment Amount :";
                lblData5.Text = CtrlInstlAmt.Value;
                lblRec6.Text = "Last Transaction Date :";
                lblData6.Text = CtrlLastTrnDate.Value;

                lblRec7.Text = "Curr.Interest Amt. :";
                lblData7.Text = CtrlCurrIntAmt.Value;

                lblRec8.Text = "Due Interest Amt. :";
                lblData8.Text = CtrlDueIntAmt.Value;


            }
        }
        protected void QryTransControl()
        {
            CtrlRow.Value = "1";
            int CRow = 1;
            string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode,TrnLogic,RecMode,ShowInt FROM A2ZTRNCTRL where TrnCode='" + txtTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Value + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var ParentId = dr["Id"].ToString();
                    //    var AccType = dr["AccType"].ToString();
                    //    var FuncOpt = dr["FuncOpt"].ToString();
                    var PayType = dr["PayType"].ToString();
                    var TrnType = dr["TrnType"].ToString();
                    var TrnMode = dr["TrnMode"].ToString();
                    var TrnLogic = dr["TrnLogic"].ToString();
                    var RecMode = dr["RecMode"].ToString();
                    var RecShowInt = dr["ShowInt"].ToString();

                    // trntype = Converter.GetSmallInteger(TranType);
                    CtrlPayType.Value = Converter.GetString(PayType);
                    CtrlTrnType.Value = Converter.GetString(TrnType);
                    CtrlTrnMode.Value = Converter.GetString(TrnMode);
                    CtrlTrnLogic.Value = Converter.GetString(TrnLogic);
                    CtrlRecMode.Value = Converter.GetString(RecMode);
                    CtrlShowInt.Value = Converter.GetString(RecShowInt);

                    ReadPayType();
                    ReadTranType();
                    ReadTranMode();
                    ReadTranLogic();

                    if (CtrlMsgFlag.Value == "1")
                    {
                        return;
                    }

                    CRow = CRow + 1;
                    CtrlRow.Value = Converter.GetString(CRow);
                }

            }

        }
        private void ReadPayType()
        {
            int TypeClass = Converter.GetInteger(lblcls.Value);
            int PayType = Converter.GetInteger(CtrlPayType.Value);
            A2ZPAYTYPEDTO gdto = (A2ZPAYTYPEDTO.GetInformation(TypeClass, PayType));
            if (gdto.record > 0)
            {
                if (CtrlRow.Value == "1")
                {
                    CtrlPayType1.Value = Converter.GetString(CtrlPayType.Value);
                    lblPayDesc1.Text = Converter.GetString(gdto.PayTypeDescription);
                    CtrlRecMode1.Value = Converter.GetString(CtrlRecMode.Value);
                }
                if (CtrlRow.Value == "2")
                {
                    CtrlPayType2.Value = Converter.GetString(CtrlPayType.Value);
                    lblPayDesc2.Text = Converter.GetString(gdto.PayTypeDescription);
                    CtrlRecMode2.Value = Converter.GetString(CtrlRecMode.Value);
                }
                if (CtrlRow.Value == "3")
                {
                    CtrlPayType3.Value = Converter.GetString(CtrlPayType.Value);
                    lblPayDesc3.Text = Converter.GetString(gdto.PayTypeDescription);
                    CtrlRecMode3.Value = Converter.GetString(CtrlRecMode.Value);
                }
                if (CtrlRow.Value == "4")
                {
                    CtrlPayType4.Value = Converter.GetString(CtrlPayType.Value);
                    lblPayDesc4.Text = Converter.GetString(gdto.PayTypeDescription);
                    CtrlRecMode4.Value = Converter.GetString(CtrlRecMode.Value);
                }
            }

        }

        private void ReadTranType()
        {
            if (CtrlTrnType.Value != "99")
            {
                int TrType = Converter.GetSmallInteger(CtrlTrnType.Value);
                A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
                if (Trndto.record > 0)
                {
                    if (CtrlRow.Value == "1")
                    {
                        txtTrnType1.Text = Converter.GetString(CtrlTrnType.Value);
                        CtrlTrnType1.Value = Converter.GetString(txtTrnType1.Text);
                        txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    }
                    if (CtrlRow.Value == "2")
                    {
                        txtTrnType2.Text = Converter.GetString(CtrlTrnType.Value);
                        CtrlTrnType2.Value = Converter.GetString(txtTrnType2.Text);
                        txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    }
                    if (CtrlRow.Value == "3")
                    {
                        txtTrnType3.Text = Converter.GetString(CtrlTrnType.Value);
                        CtrlTrnType3.Value = Converter.GetString(txtTrnType3.Text);
                        txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    }
                    if (CtrlRow.Value == "4")
                    {
                        txtTrnType4.Text = Converter.GetString(CtrlTrnType.Value);
                        CtrlTrnType4.Value = Converter.GetString(txtTrnType4.Text);
                        txtTrnType4.Text = Converter.GetString(Trndto.TrnTypeDescription);
                    }
                }

            }
        }

        private void ReadTranMode()
        {
            if (CtrlRow.Value == "1")
            {
                CtrlTrnMode1.Value = Converter.GetString(CtrlTrnMode.Value);
                if (CtrlTrnMode1.Value == "0")
                {
                    CtrlTrnContraMode1.Value = "1";
                }
                else
                {
                    CtrlTrnContraMode1.Value = "0";
                }

            }
            if (CtrlRow.Value == "2")
            {
                CtrlTrnMode2.Value = Converter.GetString(CtrlTrnMode.Value);
                if (CtrlTrnMode2.Value == "0")
                {
                    CtrlTrnContraMode2.Value = "1";
                }
                else
                {
                    CtrlTrnContraMode2.Value = "0";
                }
            }
            if (CtrlRow.Value == "3")
            {
                CtrlTrnMode3.Value = Converter.GetString(CtrlTrnMode.Value);
                if (CtrlTrnMode3.Value == "0")
                {
                    CtrlTrnContraMode3.Value = "1";
                }
                else
                {
                    CtrlTrnContraMode3.Value = "0";
                }
            }
            if (CtrlRow.Value == "4")
            {
                CtrlTrnMode4.Value = Converter.GetString(CtrlTrnMode.Value);
                if (CtrlTrnMode4.Value == "0")
                {
                    CtrlTrnContraMode4.Value = "1";
                }
                else
                {
                    CtrlTrnContraMode4.Value = "0";
                }
            }
        }
        private void ReadTranLogic()
        {
            if (CtrlTrnLogic.Value == "0")
            {
                CtrlLogicAmt.Value = "0";
                VisibleTranAmt();
            }
            if (CtrlTrnLogic.Value == "1")
            {
                ShareMinAmt();
            }
            if (CtrlTrnLogic.Value == "2")
            {
                PensionAmount();
            }
            if (CtrlTrnLogic.Value == "3")
            {
                FixedDepositAmt();
            }
            if (CtrlTrnLogic.Value == "4")
            {
                LoanDisbursementAmt();
            }
            if (CtrlTrnLogic.Value == "5")
            {
                ODLoanWithdrawal();
            }

            if (CtrlTrnLogic.Value == "6")
            {
                if (CtrlAccAtyClass.Value == "5")
                {
                    ODIntReturnAmt();
                }
                else
                {
                    IntReturnAmt();
                }
            }
            if (CtrlTrnLogic.Value == "7")
            {
                if (CtrlAccAtyClass.Value == "6")
                {
                    LoanReturnAmt();
                }
                else
                {
                    CtrlLogicAmt.Value = "0";
                    VisibleTranAmt();
                }
            }

            if (CtrlTrnLogic.Value == "8")
            {
                IntWithdrawal();
            }
            if (CtrlTrnLogic.Value == "9")
            {
                BenefitWithdrawal();
            }
            if (CtrlTrnLogic.Value == "10")
            {
                InterestAmountFDR();
            }
            if (CtrlTrnLogic.Value == "11")
            {
                InterestAdjustmentFDR();
            }
            if (CtrlTrnLogic.Value == "12")
            {
                NetEncashmentIntFDR();
            }
            if (CtrlTrnLogic.Value == "13")
            {
                NetEncashmentPrincFDR();
            }
            if (CtrlTrnLogic.Value == "14")
            {
                InterestAmount6YR();
            }
            if (CtrlTrnLogic.Value == "15")
            {
                InterestAdjustment6YR();
            }
            if (CtrlTrnLogic.Value == "16")
            {
                NetEncashmentInt6YR();
            }
            if (CtrlTrnLogic.Value == "17")
            {
                NetEncashmentPrinc6YR();
            }
            if (CtrlTrnLogic.Value == "18")
            {
                BenefitAmountMSplus();
            }
            if (CtrlTrnLogic.Value == "19")
            {
                BenefitAdjustmentMSplus();
            }
            if (CtrlTrnLogic.Value == "20")
            {
                NetEncashmentBenefitMSplus();
            }
            if (CtrlTrnLogic.Value == "21")
            {
                NetEncashmentPrincMSplus();
            }
            if (CtrlTrnLogic.Value == "22")
            {
                NetInterestReceived();
            }
            if (CtrlTrnLogic.Value == "23")
            {
                NetLoanAmtReceived();
            }
        }
        // ----------------- 1 No. Logic ------------------------------
        private void ShareMinAmt()
        {
            Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);

            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetShareMinAmt(AccType));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));
                VisibleTranAmt();
            }
        }

        // ----------------- 2 No. Logic ------------------------------
        private void PensionAmount()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetPensionDepositAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));
                VisibleTranAmt();
            }

        }

        private void FixedDepositAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetFixedDepositAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.NoMsg > 0)
                {
                    AccFixedDepositMSG();
                    txtAccNo.Focus();
                    CtrlMsgFlag.Value = "1";
                    return;
                }
                else
                {
                    if (CtrlAccAtyClass.Value != "3")
                    {
                        getDTO.LogicAmount = 0;
                    }

                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    VisibleTranAmt();
                }
            }
        }

        // ----------------- 3 No. Logic ------------------------------
        private void LoanDisbursementAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetLoanDisbursementAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.NoMsg > 0)
                {
                    AccDisbMSG();
                    txtAccNo.Focus();
                    CtrlMsgFlag.Value = "1";
                    return;
                }
                else
                {
                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    VisibleTranAmt();
                }
            }
        }
        // ----------------- 4 No. Logic ------------------------------
        private void ODLoanWithdrawal()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetODLoanWithdrawal(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.NoMsg > 0)
                {
                    AccODWithdrawalMSG();
                    txtAccNo.Focus();
                    CtrlMsgFlag.Value = "1";
                    return;
                }
                else
                {
                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));


                    TotalwithdrawalValue();
                    double TWV = Converter.GetDouble(TotalWithdrawal.Value);
                    double ValidAmt = Converter.GetDouble(CtrlValidAmt.Value);
                    ValidAmt = (ValidAmt - TWV);
                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValidAmt));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValidAmt));


                    VisibleTranAmt();
                }
            }
        }


        // ----------------- 5A No. Logic ------------------------------
        private void ODIntReturnAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetODInterestAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));
                CtrlCurrIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.AccCurrIntAmt))));
                CtrlDueIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.AccDueIntAmt))));
                CtrlIntFlag.Value = CtrlShowInt.Value;
                VisibleTranAmt();
            }
        }
        // ----------------- 5B No. Logic ------------------------------
        private void IntReturnAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetIntReturnAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlCurrIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.AccCurrIntAmt))));
                CtrlDueIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.AccDueIntAmt))));
                CtrlIntFlag.Value = CtrlShowInt.Value;
                VisibleTranAmt();
            }
        }
        // ----------------- 6 No. Logic ------------------------------
        private void LoanReturnAmt()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetLoanReturnAmt(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LogicAmount));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));
                VisibleTranAmt();
            }
        }

        // ----------------- 7 No. Logic ------------------------------
        private void IntWithdrawal()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetIntWithdrawal(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.LogicAmount == 0)
                {
                    IntWithdMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    CtrlMsgFlag.Value = "1";
                    return;
                }
                else
                {
                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));

                    VisibleTranAmt();
                }
            }
        }

        // ----------------- 8 No. Logic ------------------------------
        private void BenefitWithdrawal()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetBenefitWithdrawal(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                if (getDTO.LogicAmount == 0)
                {
                    BenefitWithdMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    CtrlMsgFlag.Value = "1";
                    return;
                }
                else
                {
                    CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                    CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(0))));
                    CtrlBenefitFlag.Value = "1";
                    VisibleTranAmt();
                }
            }
        }

        // ----------------- 9 No. Logic ------------------------------
        private void InterestAmountFDR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetInterestAmtFDR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlProvAdjCr.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.CalProvAdjCr))));
                CtrlProvAdjDr.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.CalProvAdjDr))));
                VisibleTranAmt();

            }
        }
        // ----------------- 10 No. Logic ------------------------------
        private void InterestAdjustmentFDR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetInterestAdjFDR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }

        // ----------------- 11 No. Logic ------------------------------
        private void NetEncashmentIntFDR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetEncashmentIntFDR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlMainAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }

        // ----------------- 12 No. Logic ------------------------------
        private void NetEncashmentPrincFDR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetEncashmentPrincFDR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlMainAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }
        // ----------------- 13 No. Logic ------------------------------
        private void InterestAmount6YR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetInterestAmt6YR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlProvAdjCr.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.CalProvAdjCr))));
                CtrlProvAdjDr.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.CalProvAdjDr))));
                VisibleTranAmt();

            }
        }
        // ----------------- 14 No. Logic ------------------------------
        private void InterestAdjustment6YR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetInterestAdj6YR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }

        // ----------------- 15 No. Logic ------------------------------
        private void NetEncashmentInt6YR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetEncashmentInt6YR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlMainAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }

        }
        // ----------------- 16 No. Logic ------------------------------
        private void NetEncashmentPrinc6YR()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetEncashmentPrinc6YR(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                //CtrlValidAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }

        // ----------------- 17 No. Logic ------------------------------
        private void BenefitAmountMSplus()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetBenefitAmtMSplus(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }
        // ----------------- 18 No. Logic ------------------------------
        private void BenefitAdjustmentMSplus()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetBenefitAdjMSplus(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }

        // ----------------- 19 No. Logic ------------------------------
        private void NetEncashmentBenefitMSplus()
        {

            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetEncashmentBenefitMSplus(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlMainAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }
        // ----------------- 20 No. Logic ------------------------------
        private void NetEncashmentPrincMSplus()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetEncashmentPrincMSplus(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }
        // ----------------- 21 No. Logic ------------------------------
        private void NetInterestReceived()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetInterestReceived(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlIntFlag.Value = CtrlShowInt.Value;
                VisibleTranAmt();

            }
        }

        // ----------------- 22 No. Logic ------------------------------
        private void NetLoanAmtReceived()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
            int CuNo = Converter.GetInteger(lblCuNo.Value);
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
            int AccNumber = Converter.GetInteger(txtAccNo.Text);
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            A2ZTRNLOGICDTO getDTO = (A2ZTRNLOGICDTO.GetNetLoanAmtReceived(CuType, CuNo, MemNumber, Acctype, AccNumber));
            if (getDTO.NoRecord > 0)
            {
                CtrlLogicAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                CtrlValidAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(getDTO.LogicAmount))));
                VisibleTranAmt();

            }
        }
        private void VisibleTranAmt()
        {
            if (CtrlRow.Value == "1")
            {
                CtrlValidAmt1.Value = CtrlValidAmt.Value;
                VisibleTrue();
                if (CtrlLogicAmt.Value == "0" || CtrlLogicAmt.Value == "00.00")
                {
                    txtAmount1.Text = string.Empty;
                }
                else
                {
                    txtAmount1.Text = CtrlLogicAmt.Value;
                }
            }

            if (CtrlRow.Value == "2")
            {
                CtrlValidAmt2.Value = CtrlValidAmt.Value;
                VisibleTrue();
                if (CtrlLogicAmt.Value == "0" || CtrlLogicAmt.Value == "00.00")
                {
                    txtAmount2.Text = string.Empty;
                }
                else
                {
                    txtAmount2.Text = CtrlLogicAmt.Value;
                }
            }

            if (CtrlRow.Value == "3")
            {
                CtrlValidAmt3.Value = CtrlValidAmt.Value;
                VisibleTrue();
                if (CtrlLogicAmt.Value == "0" || CtrlLogicAmt.Value == "00.00")
                {
                    txtAmount3.Text = string.Empty;
                }
                else
                {
                    txtAmount3.Text = CtrlLogicAmt.Value;
                }

            }
            if (CtrlRow.Value == "4")
            {
                CtrlValidAmt4.Value = CtrlValidAmt.Value;
                VisibleTrue();
                if (CtrlLogicAmt.Value == "0" || CtrlLogicAmt.Value == "00.00")
                {
                    txtAmount4.Text = string.Empty;
                }
                else
                {
                    txtAmount4.Text = CtrlLogicAmt.Value;
                }
            }
        }
        private void VisibleTrue()
        {
            if (CtrlRow.Value == "1")
            {
                if ((CtrlLogicAmt.Value == "0" || CtrlLogicAmt.Value == "00.00") && (CtrlTrnLogic.Value == "10" || CtrlTrnLogic.Value == "14" || CtrlTrnLogic.Value == "18" || CtrlTrnLogic.Value == "22"))
                {
                    txtTrnType1.Visible = false;
                    lblPayDesc1.Visible = false;
                    txtAmount1.Visible = false;
                }
                else
                {
                    txtTrnType1.Visible = true;
                    lblPayDesc1.Visible = true;
                    txtAmount1.Visible = true;
                    if (CtrlRecMode1.Value == "1")
                    {
                        txtAmount1.ReadOnly = true;
                    }

                }

            }
            if (CtrlRow.Value == "2")
            {
                if ((CtrlLogicAmt.Value != "0" && CtrlLogicAmt.Value != "00.00") || (CtrlTrnLogic.Value == "7"))
                {
                    txtTrnType2.Visible = true;
                    lblPayDesc2.Visible = true;
                    txtAmount2.Visible = true;
                    if (CtrlRecMode2.Value == "1")
                    {
                        txtAmount2.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType2.Visible = false;
                    lblPayDesc2.Visible = false;
                    txtAmount2.Visible = false;
                }

            }
            if (CtrlRow.Value == "3")
            {
                if (CtrlLogicAmt.Value != "0" && CtrlLogicAmt.Value != "00.00")
                {
                    txtTrnType3.Visible = true;
                    lblPayDesc3.Visible = true;
                    txtAmount3.Visible = true;
                    if (CtrlRecMode3.Value == "1")
                    {
                        txtAmount3.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType3.Visible = false;
                    lblPayDesc3.Visible = false;
                    txtAmount3.Visible = false;
                }

            }
            if (CtrlRow.Value == "4")
            {
                if (CtrlLogicAmt.Value != "0" && CtrlLogicAmt.Value != "00.00")
                {
                    txtTrnType4.Visible = true;
                    lblPayDesc4.Visible = true;
                    txtAmount4.Visible = true;
                    if (CtrlRecMode4.Value == "1")
                    {
                        txtAmount4.ReadOnly = true;
                    }
                }
                else
                {
                    txtTrnType4.Visible = false;
                    lblPayDesc4.Visible = false;
                    txtAmount4.Visible = false;
                }

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,AccType,AccNo,PayTypeDes,Abs(GLAmount) as Amount,TrnTypeCode,TrnCode FROM WF_Transaction WHERE TrnFlag !=1 AND UserID='" + hdnID.Value + "'";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void VerifyDuplicateRec()
        {
            CtrlMsgFlag.Value = "0";

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                Label lblTrnCode = (Label)gvDetailInfo.Rows[i].Cells[6].FindControl("TrnCode");
                string TrnCode = Converter.GetString(lblTrnCode.Text);

                if (txtTrnCode.Text == TrnCode)
                {
                    DuplicateAccTypeMSG();
                    CtrlMsgFlag.Value = "1";
                    txtTrnCode.Text = string.Empty;
                    ddlTrnCode.SelectedValue = "-Select-";
                    return;
                }
            }
        }
        protected void SumValue()
        {
            Decimal sumCr = 0;
            //Decimal sumDr = 0;

            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                Label lblTrnType = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("TrnTypeCode");
                //Label lblTrnAmt = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("GLAmount");
                string TrnType = Converter.GetString(lblTrnType.Text);

                if (TrnType != "3" || (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8"))
                {

                    sumCr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[4].Text));



                }
                //sumDr += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[5].Text));

            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumCr));
            //CtrlTrnDrTotal.Text = Convert.ToString(String.Format("{0:0,0.00}", sumDr));
        }


        protected void TotalwithdrawalValue()
        {
            Decimal TotWithdrawal = 0;


            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {


                Label lblAType = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("AccType");
                Label lblANo = (Label)gvDetailInfo.Rows[i].Cells[1].FindControl("AccNo");

                Int16 AType = Converter.GetSmallInteger(lblAType.Text);
                int ANo = Converter.GetInteger(lblANo.Text);

                Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
                int AccNumber = Converter.GetInteger(txtAccNo.Text);


                if (Acctype == AType && AccNumber == ANo)
                {
                    TotWithdrawal += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo.Rows[i].Cells[4].Text));
                }
            }

            TotalWithdrawal.Value = Convert.ToString(String.Format("{0:0,0.00}", TotWithdrawal));

        }

        protected void txtTrnType1_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType1.Text);
            CtrlTrnType1.Value = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType1.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc1.Focus();
            }
        }
        protected void txtTrnType2_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType2.Text);
            CtrlTrnType2.Value = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType2.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc2.Focus();
            }
        }

        protected void txtTrnType3_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType3.Text);
            CtrlTrnType3.Value = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType3.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc3.Focus();
            }
        }

        protected void txtTrnType4_TextChanged(object sender, EventArgs e)
        {
            int TrType = Converter.GetSmallInteger(txtTrnType4.Text);
            CtrlTrnType4.Value = Converter.GetString(TrType);
            A2ZTRANTYPEDTO Trndto = (A2ZTRANTYPEDTO.GetInformation(TrType));
            if (Trndto.record > 0)
            {
                txtTrnType4.Text = Converter.GetString(Trndto.TrnTypeDescription);
                lblPayDesc4.Focus();
            }
        }
        protected void AddNormal()
        {
            if (CtrlModule.Value == "6")
            {
                txtGLCashCode.Text = hdnCashCode.Value;
            }

            if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
            {
                txtGLCashCode.Text = txtGLContraCode.Text;
            }


            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime valuedate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            CtrlTrnCSGL.Value = "0";
            CtrlTrnFlag.Value = "0";


            if (CtrlRow.Value == "1")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType1.Value + "' and TrnType='" + CtrlTrnType1.Value + "' and TrnMode='" + CtrlTrnMode1.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode1.Value == "0")
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);


                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount1.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount1.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount1.Text;
                    }

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);

                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount1.Text;
                    CtrlGLDebitAmt.Value = "0";


                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount1.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount1.Text;
                    }

                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }

                if (CtrlBenefitFlag.Value == "1")
                {
                    double Amount = Converter.GetDouble(txtAmount1.Text);
                    CtrlInterestAmt.Value = Converter.GetString(0 - Amount);
                }
                else
                {
                    CtrlInterestAmt.Value = "0";
                }



                if (CtrlBenefitFlag.Value == "1")
                {
                    string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,TrnInterestAmt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType1.Value + "','" + txtTrnType1.Text + "','" + CtrlTrnMode1.Value + "','" + CtrlPayType1.Value + "','" + lblPayDesc1.Text + "','" + "0" + "','" + "0" + "','" + CtrlShowInt.Value + "','" + CtrlInterestAmt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));
                    if (rowEffect1 > 0)
                    {
                        gvDetail();
                        SumValue();
                    }
                }
                else
                {
                    string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,CalProvAdjCr,CalProvAdjDr,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType1.Value + "','" + txtTrnType1.Text + "','" + CtrlTrnMode1.Value + "','" + CtrlPayType1.Value + "','" + lblPayDesc1.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + CtrlProvAdjCr.Value + "','" + CtrlProvAdjDr.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));

                    if (rowEffect1 > 0)
                    {
                        gvDetail();
                        SumValue();
                    }
                }

            }
            if (CtrlRow.Value == "2")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType2.Value + "' and TrnType='" + CtrlTrnType2.Value + "' and TrnMode='" + CtrlTrnMode2.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }
                if (CtrlTrnMode2.Value == "0")
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }


                    CtrlGLDebitAmt.Value = txtAmount2.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount2.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount2.Text;
                    }

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount2.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount2.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount2.Text;
                    }



                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }

                if (CtrlIntFlag.Value == "1")
                {
                    CtrlInterestAmt.Value = txtAmount1.Text;
                }
                else
                {
                    CtrlInterestAmt.Value = "0";
                }


                if (CtrlIntFlag.Value == "1")
                {
                    double a = Converter.GetDouble(txtAmount1.Text);
                    double b = Converter.GetDouble(CtrlValidAmt1.Value);
                    double c = (b - a);
                    if (c > 0)
                    {
                        CtrlDueIntAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", c));
                    }
                    else
                    {
                        CtrlDueIntAmt.Value = "0";
                    }
                }




                string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,TrnInterestAmt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,TrnDueIntAmt,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType2.Value + "','" + txtTrnType2.Text + "','" + CtrlTrnMode2.Value + "','" + CtrlPayType2.Value + "','" + lblPayDesc2.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlInterestAmt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + CtrlDueIntAmt.Value + "','" + valuedate + "','" + hdnID.Value + "')";
                int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                if (rowEffect2 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Value == "3")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType3.Value + "' and TrnType='" + CtrlTrnType3.Value + "' and TrnMode='" + CtrlTrnMode3.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode3.Value == "0")
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount3.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount3.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount3.Text;
                    }


                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }
                    CtrlGLCreditAmt.Value = txtAmount3.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount3.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount3.Text;
                    }



                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }

                string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType3.Value + "','" + txtTrnType3.Text + "','" + CtrlTrnMode3.Value + "','" + CtrlPayType3.Value + "','" + lblPayDesc3.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";

                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                if (rowEffect3 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Value == "4")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType4.Value + "' and TrnType='" + CtrlTrnType4.Value + "' and TrnMode='" + CtrlTrnMode4.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode4.Value == "0")
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount4.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount4.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount4.Text;
                    }


                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }
                else
                {
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }
                    CtrlGLCreditAmt.Value = txtAmount4.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount4.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount4.Text;
                    }



                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                }

                string sqlquery4 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType4.Value + "','" + txtTrnType4.Text + "','" + CtrlTrnMode4.Value + "','" + CtrlPayType4.Value + "','" + lblPayDesc4.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";

                int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));
                if (rowEffect4 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
        }
        protected void AddContra()
        {
            if (CtrlModule.Value == "6")
            {
                txtGLCashCode.Text = hdnCashCode.Value;
            }

            if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
            {
                txtGLCashCode.Text = txtGLContraCode.Text;
            }

            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime valuedate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            CtrlTrnCSGL.Value = "0";
            CtrlTrnFlag.Value = "1";


            if (CtrlRow.Value == "1")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType1.Value + "' and TrnType='" + CtrlTrnType1.Value + "' and TrnMode='" + CtrlTrnMode1.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode1.Value == "0")
                {

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //    CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount1.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount1.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount1.Text;
                    }

                }
                else
                {

                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount1.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount1.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount1.Text;
                    }

                }
                string sqlquery1 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType1.Value + "','" + txtTrnType1.Text + "','" + CtrlTrnContraMode1.Value + "','" + CtrlPayType1.Value + "','" + lblPayDesc1.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));
                if (rowEffect1 > 0)
                {
                    gvDetail();
                    SumValue();
                }

            }
            if (CtrlRow.Value == "2")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType2.Value + "' and TrnType='" + CtrlTrnType2.Value + "' and TrnMode='" + CtrlTrnMode2.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }
                if (CtrlTrnMode2.Value == "0")
                {

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount2.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount2.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount2.Text;
                    }

                }
                else
                {

                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount2.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount2.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount2.Text;
                    }

                }
                string sqlquery2 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType2.Value + "','" + txtTrnType2.Text + "','" + CtrlTrnContraMode2.Value + "','" + CtrlPayType2.Value + "','" + lblPayDesc2.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";
                int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery2, "A2ZCSMCUS"));
                if (rowEffect2 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Value == "3")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType3.Value + "' and TrnType='" + CtrlTrnType3.Value + "' and TrnMode='" + CtrlTrnMode3.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode3.Value == "0")
                {

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount3.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount3.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount3.Text;
                    }

                }
                else
                {

                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount3.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount3.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount3.Text;
                    }

                }

                string sqlquery3 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType3.Value + "','" + txtTrnType3.Text + "','" + CtrlTrnContraMode3.Value + "','" + CtrlPayType3.Value + "','" + lblPayDesc3.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";

                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
                if (rowEffect3 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
            if (CtrlRow.Value == "4")
            {
                string qry = "SELECT Id,ShowInt,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL where FuncOpt='" + lblFuncOpt.Value + "' and TrnCode='" + txtTrnCode.Text + "' and PayType='" + CtrlPayType4.Value + "' and TrnType='" + CtrlTrnType4.Value + "' and TrnMode='" + CtrlTrnMode4.Value + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    CtrlShowInt.Value = Converter.GetString(dt.Rows[0]["ShowInt"]);
                    CtrlGLAccNoDR.Value = Converter.GetString(dt.Rows[0]["GLAccNoDR"]);
                    CtrlGLAccNoCR.Value = Converter.GetString(dt.Rows[0]["GLAccNoCR"]);
                }

                if (CtrlTrnMode4.Value == "0")
                {

                    if (CtrlGLAccNoCR.Value == "0")
                    {
                        //CtrlGLAccNoCR.Text = hdnCashCode.Value;
                        CtrlGLAccNoCR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoCR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLCreditAmt.Value = txtAmount4.Text;
                    CtrlGLDebitAmt.Value = "0";

                    if (CtrlGLAType.Value == "1" || CtrlGLAType.Value == "5")
                    {
                        double Amount = Converter.GetDouble(txtAmount4.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount4.Text;
                    }

                }
                else
                {

                    if (CtrlGLAccNoDR.Value == "0")
                    {
                        //CtrlGLAccNoDR.Text = hdnCashCode.Value;
                        CtrlGLAccNoDR.Value = Converter.GetString(txtGLCashCode.Text);
                    }
                    CtrlGLAccNo.Value = Converter.GetString(CtrlGLAccNoDR.Value);
                    A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(CtrlGLAccNo.Value));
                    if (glObj.GLAccNo > 0)
                    {
                        CtrlGLAType.Value = Converter.GetString(glObj.GLAccType);
                    }

                    CtrlGLDebitAmt.Value = txtAmount4.Text;
                    CtrlGLCreditAmt.Value = "0";

                    if (CtrlGLAType.Value == "2" || CtrlGLAType.Value == "4")
                    {
                        double Amount = Converter.GetDouble(txtAmount4.Text);
                        CtrlGLAmount.Value = Converter.GetString(0 - Amount);
                    }
                    else
                    {
                        CtrlGLAmount.Value = txtAmount4.Text;
                    }

                }

                string sqlquery4 = "INSERT INTO  WF_Transaction(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,TrnTypeCode,TrnTypeDes,TrnMode,PayTypeCode,PayTypeDes,Credit,Debit,ShowInt,GLAccNoDR,GLAccNoCR,TrnCSGL,TrnFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,UserID)VALUES('" + opdate + "','" + lblCuType.Value + "','" + lblCuNo.Value + "','" + txtMemNo.Text + "','" + txtAccType.Text + "','" + txtAccNo.Text + "','" + txtTrnCode.Text + "','" + lblFuncOpt.Value + "','" + lblTransFunction.Text + "','" + CtrlTrnType4.Value + "','" + txtTrnType4.Text + "','" + CtrlTrnContraMode4.Value + "','" + CtrlPayType4.Value + "','" + lblPayDesc4.Text + "','" + CtrlGLCreditAmt.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlShowInt.Value + "','" + CtrlGLAccNoDR.Value + "','" + CtrlGLAccNoCR.Value + "','" + CtrlTrnCSGL.Value + "','" + CtrlTrnFlag.Value + "','" + CtrlGLAccNo.Value + "','" + CtrlGLAmount.Value + "','" + CtrlGLDebitAmt.Value + "','" + CtrlGLCreditAmt.Value + "','" + hdnCashCode.Value + "','" + CtrlModule.Value + "','" + valuedate + "','" + hdnID.Value + "')";

                int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));
                if (rowEffect4 > 0)
                {
                    gvDetail();
                    SumValue();
                }
            }
        }

        protected void ClearHstInfo()
        {
            lblRec1.Text = string.Empty;
            lblRec2.Text = string.Empty;
            lblRec3.Text = string.Empty;
            lblRec4.Text = string.Empty;
            lblRec5.Text = string.Empty;
            lblRec6.Text = string.Empty;
            lblRec7.Text = string.Empty;
            lblRec8.Text = string.Empty;
            lblBalRec.Text = string.Empty;

            lblData1.Text = string.Empty;
            lblData2.Text = string.Empty;
            lblData3.Text = string.Empty;
            lblData4.Text = string.Empty;
            lblData5.Text = string.Empty;
            lblData6.Text = string.Empty;
            lblData7.Text = string.Empty;
            lblData8.Text = string.Empty;

            lblUnPostDataCr.Text = string.Empty;
            lblUnPostDataDr.Text = string.Empty;

            lblUnPostCr.Text = string.Empty;
            lblUnPostDr.Text = string.Empty;

            lblData1.Visible = true;
            lblData2.Visible = true;
            lblData3.Visible = true;
            lblData4.Visible = true;
            lblData5.Visible = true;
            lblData6.Visible = true;
            lblData7.Visible = true;

            lblData8.Visible = false;
            lblBalData.Visible = false;
            lblUnPostDataCr.Visible = false;
            lblUnPostDataDr.Visible = false;

        }
        protected void ClearInfo()
        {

            txtCreditUNo.Text = string.Empty;
            txtOldCuNo.Text = string.Empty;

            txtMemNo.Text = string.Empty;

            //txtVoucherNo.Text = string.Empty;
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtTrnCode.Text = string.Empty;

            gvDetailInfo.Visible = false;

            ClearHstInfo();

            txtTotalAmt.Text = string.Empty;

            if (CtrlModule.Value == "1")
            {
                ddlCreditUNo.SelectedValue = "-Select-";
                ddlMemNo.SelectedValue = "-Select-";
                ddlTrnCode.SelectedValue = "-Select-";
                ddlAccNo.SelectedValue = "-Select-";
            }
            if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
            {
                ddlGLContraCode.SelectedValue = "-Select-";
                txtGLContraCode.Text = string.Empty;
            }
            else
            {
                ddlGLCashCode.SelectedValue = "-Select-";
                txtGLCashCode.Text = string.Empty;
            }
        }

        //   --------- DISPLAY MASSAGE SCREEN ----------------------- 

        protected void DuplicateAccTypeMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Duplicate Transaction Code');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void AccessAmountMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Insufficent Balance');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void AccODWithdrawalMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Withdrawal Access Over Limit');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void AccFixedDepositMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Not A New Account');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void AccDisbMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Account Already Disbursed');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void InvalidCuNoMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Credit Union No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void TransferCuNoMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Transfered Credit Union No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void InvalidMemberMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Member No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void InvalidTranCodeMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Transaction Code');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void InvalidAccountNoMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Account Does Not Exist');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void IntWithdMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('No Accrued Interest');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void BenefitWithdMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('No Benefit Interest');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void UpdatedSucessfullyMSG()
        {
            string a = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (CtrlProcStat.Value == "0")
            {
                a = "    TRANSACTION SUCESSFULLY DONE";
            }
            if (CtrlProcStat.Value == "1")
            {
                a = "    TRANSACTION INPUT DONE";
            }
            string b = "Generated New Voucher No.";
            string c = string.Format(CtrlVoucherNo.Value);

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(a);
            sb.Append("\\n");
            sb.Append("\\n");
            sb.Append(b);
            sb.Append(c);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void UpdatedInputMSG()
        {
            string a = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (CtrlProcStat.Value == "0")
            {
                a = "    Transaction Sucessfully Done";
            }
            if (CtrlProcStat.Value == "1")
            {
                a = "    Transaction Input Done";
            }
            string b = "Generated New Voucher No.";
            string c = string.Format(CtrlVoucherNo.Value);

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(a);
            sb.Append("\\n");
            sb.Append("\\n");
            sb.Append(b);
            sb.Append(c);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void RecordsAddedMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                //String cstext1 = "confirm('Records Already Added');";
                String cstext1 = "alert('Records Already Added');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void DuplicateVchMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Voucher Already Exist');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void VchInputMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Voucher No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;
        }

        protected void VchAmtMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Please Input Amount');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;
        }

        protected void AccStatusMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Account not Active');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;
        }


        //------------------------- END DISPLAY MASSAGE SCREEN ----------------------


        protected void BtnViewImage_Click(object sender, EventArgs e)
        {
            Session["CuNo"] = lblCuNo.Value;
            Session["CuType"] = lblCuType.Value;
            Session["MemNo"] = txtMemNo.Text;
            Page.ClientScript.RegisterStartupScript(
            this.GetType(), "OpenWindow", "window.open('CSGetImage.aspx','_newtab');", true);
        }
        protected void txtAmount1_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount1.Text);
            txtAmount1.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            CtrlValidAmt.Value = CtrlValidAmt1.Value;

            if (CtrlTrnLogic.Value == "4" || CtrlTrnLogic.Value == "5")
            {
                TrnValidity();
                if (CtrlFlag.Value == "1")
                {
                    AccessAmountMSG();
                    txtAmount1.Text = CtrlValidAmt.Value;
                    txtAmount1.Focus();
                }
            }

            if (CtrlTrnLogic.Value == "0" && (lblFuncOpt.Value == "3" || lblFuncOpt.Value == "8"))
            {
                CtrlValidAmt.Value = CtrlAvailBal.Value;
                TrnValidity();
                if (CtrlFlag.Value == "1")
                {
                    AccessAmountMSG();
                    txtAmount1.Text = string.Empty;
                    txtAmount1.Focus();
                }

            }


            if (CtrlRecMode2.Value == "1" && CtrlMainAmt.Value != "")
            {
                double a = Converter.GetDouble(txtAmount1.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Value);
                double c = Converter.GetDouble(CtrlMainAmt.Value);
                txtAmount2.Text = Converter.GetString(String.Format("{0:0,0.00}", c - (b - a)));
            }

            if (CtrlRecMode3.Value == "1" && CtrlMainAmt.Value != "")
            {
                double a = Converter.GetDouble(txtAmount1.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Value);
                double c = Converter.GetDouble(CtrlMainAmt.Value);
                txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", a));
            }


            //if (CtrlRecMode3.Text == "1" && CtrlMainAmt.Text != "")
            //{
            //    double a = Converter.GetDouble(txtAmount1.Text);
            //    double b = Converter.GetDouble(CtrlValidAmt.Text);
            //    double c = Converter.GetDouble(CtrlMainAmt.Text);
            //    txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", c - (b - a)));
            //}

            BtnAdd.Focus();
        }


        protected void txtAmount2_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount2.Text);
            txtAmount2.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            CtrlValidAmt.Value = CtrlValidAmt2.Value;
            if (CtrlRecMode4.Value == "1" && CtrlMainAmt.Value != "")
            {
                double a = Converter.GetDouble(txtAmount2.Text);
                double b = Converter.GetDouble(CtrlValidAmt.Value);
                double c = Converter.GetDouble(CtrlMainAmt.Value);
                txtAmount4.Text = Converter.GetString(String.Format("{0:0,0.00}", c + (b - a)));
            }
            BtnAdd.Focus();

        }


        protected void txtAmount3_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount3.Text);
            txtAmount3.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            BtnAdd.Focus();

        }

        protected void txtAmount4_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtAmount4.Text);
            txtAmount4.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            CtrlTranAmt.Value = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            BtnAdd.Focus();

        }


        //  ------------- ADD TRANSACTIONS ------------------------------- 
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtCreditUNo.Text == string.Empty && CtrlModule.Value != "4")
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please Input Credit Union No.' );";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                return;
            }

            if (txtMemNo.Text == string.Empty)
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please Input Depositor No.' );";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                return;
            }

            if (txtGLCashCode.Text == string.Empty && lblFuncOpt.Value != "7" && lblFuncOpt.Value != "8")
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please Input GL Cash Code' );";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                return;
            }

            if (txtGLContraCode.Text == string.Empty && (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8"))
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please Input GL Contra Code' );";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                return;
            }

            if ((txtAmount1.Text == "" || txtAmount1.Text == "00.00") && (txtAmount2.Text == "" || txtAmount2.Text == "00.00") &&
                (txtAmount3.Text == "" || txtAmount3.Text == "00.00") && (txtAmount4.Text == "" || txtAmount4.Text == "00.00"))
            {
                VchAmtMSG();
                return;
            }

            gvDetailInfo.Visible = true;

            int CRow = 0;
            string qry = "SELECT Id,AccType,FuncOpt,PayType,TrnType,TrnMode FROM A2ZTRNCTRL where TrnCode='" + txtTrnCode.Text + "' and FuncOpt='" + lblFuncOpt.Value + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CRow = CRow + 1;
                    CtrlRow.Value = Converter.GetString(CRow);

                    if (CtrlRow.Value == "1")
                    {
                        if (txtAmount1.Text != "" && txtAmount1.Text != "00.00")
                        {
                            AddNormal();
                            AddContra();
                        }
                    }

                    if (CtrlRow.Value == "2")
                    {
                        if (txtAmount2.Text != "" && txtAmount2.Text != "00.00")
                        {
                            AddNormal();
                            AddContra();
                        }
                    }

                    if (CtrlRow.Value == "3")
                    {
                        if (txtAmount3.Text != "" && txtAmount3.Text != "00.00")
                        {
                            AddNormal();
                            AddContra();
                        }
                    }
                    if (CtrlRow.Value == "4")
                    {
                        if (txtAmount4.Text != "" && txtAmount4.Text != "00.00")
                        {
                            AddNormal();
                            AddContra();
                        }
                    }
                }

                VisibleFalse();
                txtAccType.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtTrnType1.Text = string.Empty;
                txtTrnType2.Text = string.Empty;
                txtTrnType3.Text = string.Empty;
                txtTrnType4.Text = string.Empty;

                lblPayDesc1.Text = string.Empty;
                lblPayDesc2.Text = string.Empty;
                lblPayDesc3.Text = string.Empty;
                lblPayDesc4.Text = string.Empty;

                txtAmount1.Text = string.Empty;
                txtAmount2.Text = string.Empty;
                txtAmount3.Text = string.Empty;
                txtAmount4.Text = string.Empty;

                CtrlLogicAmt.Value = string.Empty;
                txtTrnCode.Text = string.Empty;
                ddlTrnCode.SelectedValue = "-Select-";
                ddlAccNo.SelectedValue = "-Select-";
                txtTrnCode.Focus();
                BtnUpdate.Visible = true;

                txtOldCuNo.ReadOnly = true;
                txtCreditUNo.ReadOnly = true;
                ddlCreditUNo.Enabled = false;
                txtMemNo.ReadOnly = true;
                ddlMemNo.Enabled = false;

                txtGLCashCode.ReadOnly = true;
                ddlGLCashCode.Enabled = false;
                txtGLContraCode.ReadOnly = true;
                ddlGLContraCode.Enabled = false;


                if (lblFuncOpt.Value != "1")
                {
                    txtGLCashCode.ReadOnly = true;
                    ddlGLCashCode.Enabled = false;
                    txtGLContraCode.ReadOnly = true;
                    ddlGLContraCode.Enabled = false;

                    txtTrnCode.ReadOnly = true;
                    ddlTrnCode.Enabled = false;
                    txtAccType.ReadOnly = true;
                    txtAccNo.ReadOnly = true;
                    ddlAccNo.Enabled = false;
                }

                BtnAdd.Visible = false;
                BtnCancel.Visible = false;

            }
        }

        // ---------------- CANCEL TRANSACTIONS ------------------------
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            VisibleFalse();
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtTrnType1.Text = string.Empty;
            txtTrnType2.Text = string.Empty;
            txtTrnType3.Text = string.Empty;
            txtTrnType4.Text = string.Empty;

            lblPayDesc1.Text = string.Empty;
            lblPayDesc2.Text = string.Empty;
            lblPayDesc3.Text = string.Empty;
            lblPayDesc4.Text = string.Empty;

            txtAmount1.Text = string.Empty;
            txtAmount2.Text = string.Empty;
            txtAmount3.Text = string.Empty;
            txtAmount4.Text = string.Empty;

            CtrlLogicAmt.Value = string.Empty;
            txtTrnCode.Text = string.Empty;
            ddlTrnCode.SelectedValue = "-Select-";

            //ddlAccNo.SelectedValue = "-Select-";
            txtTrnCode.Focus();
        }



        //  -------------- UPDATE TRANSACTIONS ------------------------------
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
            {
                txtGLCashCode.Text = hdnCashCode.Value;
            }

            int GLCode = Converter.GetInteger(txtGLCashCode.Text);
            Int16 RecType = Converter.GetSmallInteger(1);
            A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
            CtrlVoucherNo.Value = "C" + txtGLCashCode.Text + "-" + getDTO.RecLastNo;

            TrnLimitValidity();

            var prm = new object[3];

            prm[0] = hdnID.Value;
            prm[1] = CtrlVoucherNo.Value;
            prm[2] = CtrlProcStat.Value;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddTransaction", prm, "A2ZCSMCUS"));

            if (result == 0)
            {
                UpdatedSucessfullyMSG();

                ClearInfo();
                txtTotalAmt.Text = string.Empty;
                lblTotalAmt.Visible = false;
                BtnUpdate.Visible = false;

                txtOldCuNo.ReadOnly = false;
                txtCreditUNo.ReadOnly = false;
                ddlCreditUNo.Enabled = true;
                txtMemNo.ReadOnly = false;
                ddlMemNo.Enabled = true;

                txtGLCashCode.ReadOnly = false;
                ddlGLCashCode.Enabled = true;
                txtGLContraCode.ReadOnly = false;
                ddlGLContraCode.Enabled = true;
                txtTrnCode.ReadOnly = false;
                ddlTrnCode.Enabled = true;
                txtAccType.ReadOnly = false;
                txtAccNo.ReadOnly = false;
                ddlAccNo.Enabled = true;
                txtTranDate.Text = CtrlProcDate.Value;

                txtCreditUNo.Focus();
                gvDetail();

            }
        }

        // ------------ EXIT PROGRAMS ---------------------------

        protected void InitializedRecords()
        {
            VisibleFalse();
            ClearHstInfo();
            CtrlMsgFlag.Value = "0";
            CtrlIntFlag.Value = "0";
            CtrlBenefitFlag.Value = "0";
            CtrlInterestAmt.Value = "0";
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtTrnType1.Text = string.Empty;
            txtTrnType2.Text = string.Empty;
            txtTrnType3.Text = string.Empty;
            txtTrnType4.Text = string.Empty;

            lblPayDesc1.Text = string.Empty;
            lblPayDesc2.Text = string.Empty;
            lblPayDesc3.Text = string.Empty;
            lblPayDesc4.Text = string.Empty;

            txtAmount1.Text = string.Empty;
            txtAmount2.Text = string.Empty;
            txtAmount3.Text = string.Empty;
            txtAmount4.Text = string.Empty;

            CtrlLogicAmt.Value = string.Empty;
            txtTrnCode.Text = string.Empty;
            ddlTrnCode.SelectedValue = "-Select-";
            //txtGLCashCode.Text = string.Empty;

            txtGLCashCode.Text = hdnCashCode.Value;
            ddlGLCashCode.SelectedValue = Converter.GetString(hdnCashCode.Value);
        }

        protected void TrnValidity()
        {
            CtrlFlag.Value = "0";
            double TrnAmt = Converter.GetDouble(CtrlTranAmt.Value);

            TotalwithdrawalValue();
            double TWV = Converter.GetDouble(TotalWithdrawal.Value);
            double ValidAmt = Converter.GetDouble(CtrlValidAmt.Value);
            ValidAmt = (ValidAmt - TWV);
            //CtrlValidAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Math.Abs(ValidAmt))));

            if (TrnAmt > ValidAmt)
            {
                CtrlFlag.Value = "1";
            }

        }

        protected void TrnLimitValidity()
        {
            double TotalAmt = Converter.GetDouble(txtTotalAmt.Text);
            int Ids = Converter.GetInteger(hdnID.Value);
            A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

            CtrlProcStat.Value = "0";

            if (getDTO.IdsNo > 0)
            {
                if (lblFuncOpt.Value == "1")
                {
                    if (TotalAmt > getDTO.LIdsCashCredit)
                    {
                        CtrlProcStat.Value = "1";
                    }
                    else
                    {
                        CtrlProcStat.Value = "0";
                    }
                }
                if (lblFuncOpt.Value == "2" || lblFuncOpt.Value == "3" || lblFuncOpt.Value == "4" || lblFuncOpt.Value == "5" || lblFuncOpt.Value == "6")
                {
                    if (TotalAmt > getDTO.LIdsCashDebit)
                    {
                        CtrlProcStat.Value = "1";
                    }
                    else
                    {
                        CtrlProcStat.Value = "0";
                    }
                }

                if (lblFuncOpt.Value == "7")
                {
                    if (TotalAmt > getDTO.LIdsTrfCredit)
                    {
                        CtrlProcStat.Value = "1";
                    }
                    else
                    {
                        CtrlProcStat.Value = "0";
                    }
                }

                if (lblFuncOpt.Value == "8")
                {
                    if (TotalAmt > getDTO.LIdsTrfDebit)
                    {
                        CtrlProcStat.Value = "1";
                    }
                    else
                    {
                        CtrlProcStat.Value = "0";
                    }
                }
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            if (txtTotalAmt.Text != "00.00" && txtTotalAmt.Text != "")
            {
                RecordsAddedMSG();
            }
            else
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Session["FuncOpt"] = lblFuncOpt.Value;
            Session["Module"] = CtrlModule.Value;


            Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('CSDailyReverseTransaction.aspx','_newtab');", true);
        }

        protected void gvDetailInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblId = (Label)gvDetailInfo.Rows[e.RowIndex].Cells[0].FindControl("lblId");

            int ID = Converter.GetInteger(lblId.Text);
            int idincrement = ID + 1;
            string strQuery1 = "DELETE FROM WF_Transaction WHERE Id between '" + ID + "' and '" + idincrement + "'";
            int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
            //string strQuery2 = "DELETE FROM WFGLTrannsaction WHERE TrnFlag=1";
            //int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZGLMCUS"));

            gvDetail();
            SumValue();

            if (txtTotalAmt.Text == "00.00")
            {
                txtOldCuNo.ReadOnly = false;
                txtCreditUNo.ReadOnly = false;
                ddlCreditUNo.Enabled = true;
                txtMemNo.ReadOnly = false;
                ddlMemNo.Enabled = true;

                txtGLCashCode.ReadOnly = false;
                ddlGLCashCode.Enabled = true;
                txtGLContraCode.ReadOnly = false;
                ddlGLContraCode.Enabled = true;
            }

        }

        protected void txtOldCuNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtOldCuNo.Text != string.Empty)
                {
                    InitializedRecords();

                    int OldCuNo = Converter.GetInteger(txtOldCuNo.Text);
                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetOldInfo(OldCuNo));
                    if (getDTO.CreditUnionNo > 0)
                    {
                        lblCuType.Value = Converter.GetString(getDTO.CuType);
                        lblCuNo.Value = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        Int16 CuType = Converter.GetSmallInteger(lblCuType.Value);
                        int CNo = Converter.GetSmallInteger(lblCuNo.Value);


                        if (CtrlModule.Value == "1")
                        {
                            ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Value + lblCuNo.Value);
                        }

                        txtCreditUNo.Text = (lblCuType.Value + "-" + lblCuNo.Value);
                        //txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        if (CtrlModule.Value == "6" && lblFuncOpt.Value != "1" && txtGLCashCode.Text != hdnCashCode.Value)
                        {
                            InvalidCuNoMSG();

                            ddlMemNo.SelectedIndex = 0;
                            txtMemNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtOldCuNo.Text = string.Empty;
                            txtOldCuNo.Focus();
                        }

                        //string qry = "SELECT MemNo,MemName FROM A2ZMEMBER where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.                        

                        string qry = "SELECT MemNo,MemName FROM A2ZMEMBER where CuType='" + lblCuType.Value + "' and CuNo='" + lblCuNo.Value + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count == 1)
                            {
                                if (CtrlModule.Value == "1")
                                {
                                    DepositorDropdown();
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    ddlMemNo.SelectedValue = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtGLCashCode.Focus();
                                    }
                                }
                                else
                                {
                                    txtMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                                    lblMemName.Text = Converter.GetString(dt.Rows[0]["MemName"]);
                                    if (lblFuncOpt.Value == "7" || lblFuncOpt.Value == "8")
                                    {
                                        txtGLContraCode.Focus();
                                    }
                                    else
                                    {
                                        txtTrnCode.Focus();
                                    }
                                }
                            }
                            else
                            {
                                DepositorDropdown();
                                txtMemNo.Text = string.Empty;
                                ddlMemNo.SelectedValue = "-Select-";
                                txtMemNo.Focus();
                            }

                        }
                        else
                        {
                            InvalidCuNoMSG();
                            txtMemNo.Text = string.Empty;
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            if (CtrlModule.Value == "1")
                            {
                                ddlCreditUNo.SelectedValue = "-Select-";
                                ddlMemNo.SelectedIndex = 0;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InvalidDateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Future Date');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime opdate2 = DateTime.ParseExact(CtrlProcDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtTranDate.Text = CtrlProcDate.Value;
                txtTranDate.Focus();
            }

            if (opdate1 < opdate2)
            {
                lblTranDate.Text = "Back Value Date:";
            }
            else
            {
                lblTranDate.Text = "Transaction Date:";
            }


        }

    }

}