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

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanGuarantyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcDate.Text = date;

                CreditUnionDropdown();
                AccTypedropdown();

                if (CtrlProgFlag.Text != "1")
                {

                    txtCreditUNo.Focus();

                    ChkAllMemNo.Checked = true;
                    ChkAllAccType.Checked = true;
                    ChkAllAccNo.Checked = true;

                    txtMemNo.Enabled = false;
                    ddlMemNo.Enabled = false;
                    txtAccType.Enabled = false;
                    ddlAccType.Enabled = false;
                    txtAccNo.Enabled = false;
                    ddlAccNo.Enabled = false;
                }
                else
                {

                    string RtxtCreditUNo = (string)Session["StxtCreditUNo"];
                    string RddlCreditUNo = (string)Session["SddlCreditUNo"];

                    string RlblCuType = (string)Session["SlblCuType"];
                    string RlblCuNo = (string)Session["SlblCuNo"];


                    string RChkAllMemNo = (string)Session["SChkAllMemNo"];
                    string RtxtMemNo = (string)Session["StxtMemNo"];
                    string RddlMemNo = (string)Session["SddlMemNo"];

                    string RChkAllAccType = (string)Session["SChkAllAccType"];
                    string RtxtAccType = (string)Session["StxtAccType"];
                    string RddlAccType = (string)Session["SddlAccType"];

                    string RChkAllAccNo = (string)Session["SChkAllAccNo"];
                    string RtxtAccNo = (string)Session["StxtAccNo"];
                    string RddlAccNo = (string)Session["SddlAccNo"];

                    txtCreditUNo.Text = RtxtCreditUNo;
                    ddlCreditUNo.SelectedValue = RddlCreditUNo;

                    lblCuType.Text = RlblCuType;
                    lblCuNo.Text = RlblCuNo;

                    MemberDropdown();
                    AccNodropdown1();


                    if (RChkAllMemNo == "1")
                    {
                        ChkAllMemNo.Checked = true;
                        txtMemNo.Text = string.Empty;
                        ddlMemNo.SelectedValue = "-Select-";
                        txtMemNo.Enabled = false;
                        ddlMemNo.Enabled = false;
                    }
                    else
                    {
                        ChkAllMemNo.Checked = false;
                        txtMemNo.Text = RtxtMemNo;
                        ddlMemNo.SelectedValue = RtxtMemNo;
                    }

                    if (RChkAllAccType == "1")
                    {
                        ChkAllAccType.Checked = true;
                        txtAccType.Text = string.Empty;
                        ddlAccType.SelectedValue = "-Select-";
                        txtAccType.Enabled = false;
                        ddlAccType.Enabled = false;
                    }
                    else
                    {
                        ChkAllAccType.Checked = false;
                        txtAccType.Text = RtxtAccType;
                        ddlAccType.SelectedValue = RddlAccType;
                    }


                    if (RChkAllMemNo == "0" && RChkAllAccType == "1")
                    {
                        AccNodropdown2();
                    }
                    else if (RChkAllMemNo == "0" && RChkAllAccType == "0")
                    {
                        AccNodropdown3();
                    }
                    else if (RChkAllMemNo == "1" && RChkAllAccType == "0")
                    {
                        AccNodropdown4();
                    }

                    if (RChkAllAccNo == "1")
                    {
                        ChkAllAccNo.Checked = true;
                        txtAccNo.Text = string.Empty;
                        ddlAccNo.SelectedValue = "-Select-";
                        txtAccNo.Enabled = false;
                        ddlAccNo.Enabled = false;
                    }
                    else
                    {
                        ChkAllAccNo.Checked = false;
                        txtAccNo.Text = RtxtAccType;
                        ddlAccNo.SelectedValue = RddlAccType;
                    }
                }

            }
        }


        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;

            Session["StxtCreditUNo"] = string.Empty;
            Session["SddlCreditUNo"] = string.Empty;
            Session["SChkAllMemNo"] = string.Empty;
            Session["StxtMemNo"] = string.Empty;
            Session["SddlMemNo"] = string.Empty;

            Session["SChkAllAccType"] = string.Empty;
            Session["StxtAccType"] = string.Empty;
            Session["SddlAccType"] = string.Empty;

            Session["SChkAllAccNo"] = string.Empty;
            Session["StxtAccNo"] = string.Empty;
            Session["SddlAccNo"] = string.Empty;

        }

        private void CreditUnionDropdown()
        {
            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }

        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuType='" + lblCuType.Text + "'and CuNo='" + lblCuNo.Text + "' GROUP BY MemNo,MemName";
            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");
        }
        private void AccTypedropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where (AccTypeClass BETWEEN 5 AND 6) AND AccTypeMode !='2' ORDER BY AccTypeClass";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        private void AccNodropdown0()
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT where CuType !=0 AND CuNo !=0 AND (AccAtyClass BETWEEN 5 AND 6)";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
        }
        private void AccNodropdown1()
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT where CuType='" + lblCuType.Text + "' AND CuNo='" + lblCuNo.Text + "' AND (AccAtyClass BETWEEN 5 AND 6)";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
        }

        private void AccNodropdown2()
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT where CuType='" + lblCuType.Text + "' AND CuNo='" + lblCuNo.Text + "' AND MemNo='" + txtMemNo.Text + "' AND (AccAtyClass BETWEEN 5 AND 6)";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
        }

        private void AccNodropdown3()
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT where CuType='" + lblCuType.Text + "' AND CuNo='" + lblCuNo.Text + "' AND MemNo='" + txtMemNo.Text + "' AND AccType='" + txtAccType.Text + "'";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
        }

        private void AccNodropdown4()
        {
            string sqlquery = "SELECT AccNo,AccNo from A2ZACCOUNT where CuType='" + lblCuType.Text + "' AND CuNo='" + lblCuNo.Text + "' AND AccType='" + txtAccType.Text + "'";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
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
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
                        ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCuNo.Text);
                        txtMemNo.Focus();
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                        string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                        ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");
                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                        ddlMemNo.SelectedIndex = 0;
                        txtMemNo.Text = string.Empty;
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

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                    ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {

                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                        txtMemNo.Focus();

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

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(txtMemNo.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);



                    }
                    else
                    {
                        ddlMemNo.SelectedValue = "-Select-";
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
                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    AccTypedropdown();

                }
            }

        }


        protected void SessionStoreValue()
        {
            Session["ProgFlag"] = "1";

            Session["StxtCreditUNo"] = txtCreditUNo.Text;
            Session["SddlCreditUNo"] = ddlCreditUNo.SelectedValue;

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
            Session["SddlMemNo"] = ddlMemNo.SelectedValue;


            if (ChkAllAccType.Checked == true)
            {
                Session["SChkAllAccType"] = "1";
            }
            else
            {
                Session["SChkAllAccType"] = "0";
            }

            Session["StxtAccType"] = txtAccType.Text;
            Session["SddlAccType"] = ddlAccType.SelectedValue;


            if (ChkAllAccNo.Checked == true)
            {
                Session["SChkAllAccNo"] = "1";
            }
            else
            {
                Session["SChkAllAccNo"] = "0";
            }

            Session["StxtAccNo"] = txtAccNo.Text;
            Session["SddlAccNo"] = ddlAccNo.SelectedValue;

        }


        protected void BtnView_Click(object sender, EventArgs e)
        {

            if (txtCreditUNo.Text == string.Empty)
            {
                txtCreditUNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                return;
            }

            if (ChkAllMemNo.Checked == false && txtMemNo.Text == string.Empty)
            {
                txtCreditUNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
                return;
            }


            if (ChkAllAccType.Checked == false && txtAccType.Text == string.Empty)
            {
                txtCreditUNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input A/c Type');", true);
                return;
            }


            if (ChkAllAccNo.Checked == false && txtAccNo.Text == string.Empty)
            {
                txtCreditUNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input A/c No.');", true);
                return;
            }


            SessionStoreValue();


            var prm = new object[5];

            prm[0] = lblCuType.Text;
            prm[1] = lblCuNo.Text;

            if (ChkAllMemNo.Checked == true)
            {
                prm[2] = 99999;
            }
            else
            {
                prm[2] = txtMemNo.Text;
            }

            if (ChkAllAccType.Checked == true)
            {
                prm[3] = 0;
            }
            else
            {
                prm[3] = txtAccType.Text;
            }
            if (ChkAllAccNo.Checked == true)
            {
                prm[4] = "0";
            }
            else
            {
                prm[4] = txtAccNo.Text;
            }


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSWFGurantorInformation", prm, "A2ZCSMCUS"));



            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));



            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, txtCreditUNo.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlCreditUNo.SelectedItem.Text);

            if (ChkAllMemNo.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 99999);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All");
            }
            else
            {
                int memno = Converter.GetInteger(txtMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, memno);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, txtMemNo.Text + " - " + ddlMemNo.SelectedItem.Text);
            }

            if (ChkAllAccType.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All");
            }
            else
            {
                int acctype = Converter.GetInteger(txtAccType.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, acctype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, txtAccType.Text + " - " + ddlAccType.SelectedItem.Text);
            }




            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCsGurantorInformationReport");



            Response.Redirect("ReportServer.aspx", false);

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ChkAllMemNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllMemNo.Checked)
            {
                txtMemNo.Enabled = false;
                ddlMemNo.Enabled = false;
                txtMemNo.Text = string.Empty;
                ddlMemNo.SelectedIndex = 0;
            }
            else
            {
                txtMemNo.Enabled = true;
                ddlMemNo.Enabled = true;
                txtMemNo.Focus();
            }
        }

        protected void ChkAllAccType_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllAccType.Checked)
            {
                txtAccType.Enabled = false;
                ddlAccType.Enabled = false;
                txtAccType.Text = string.Empty;
                ddlAccType.SelectedIndex = 0;
            }
            else
            {
                txtAccType.Text = string.Empty;
                txtAccType.Enabled = true;
                ddlAccType.Enabled = true;
                txtAccType.Focus();

            }
        }

        protected void ChkAllAccNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllAccNo.Checked)
            {
                txtAccNo.Enabled = false;
                ddlAccNo.Enabled = false;
                txtAccNo.Text = string.Empty;
                ddlAccNo.SelectedIndex = 0;
            }
            else
            {
                txtAccNo.Text = string.Empty;
                txtAccNo.Enabled = true;
                ddlAccNo.Enabled = true;
                txtAccNo.Focus();

                if (txtCreditUNo.Text == string.Empty && ChkAllMemNo.Checked == true && ChkAllAccType.Checked == true)
                {
                    AccNodropdown0();
                }
                else if (ChkAllMemNo.Checked == true && ChkAllAccType.Checked == true)
                {
                    AccNodropdown1();
                }
                else if (ChkAllMemNo.Checked == false && ChkAllAccType.Checked == true)
                {
                    AccNodropdown2();
                }
                else if (ChkAllMemNo.Checked == false && ChkAllAccType.Checked == false)
                {
                    AccNodropdown3();
                }
                else if (ChkAllMemNo.Checked == true && ChkAllAccType.Checked == false)
                {
                    AccNodropdown4();
                }


            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAccType.SelectedValue == "-Select-")
                {

                }

                if (txtAccType.Text != string.Empty && ddlCreditUNo.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        lblTypeCls.Text = Converter.GetString(getDTO.AccTypeClass);

                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);

                      
                        if ((lblTypeCls.Text != "5" && lblTypeCls.Text != "6") || lblAccTypeMode.Text == "2")
                        {
                            txtAccType.Text = string.Empty;
                            txtAccType.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
                            return;
                        }
                        else
                        {

                            ddlAccType.SelectedValue = txtAccType.Text;
                            return;
                        }
                    }
                    else
                    {
                        txtAccType.Text = string.Empty;
                        txtAccType.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Type Not Found');", true);
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccType.Text = Converter.GetString(ddlAccType.SelectedValue);
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
            A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
            if (accgetDTO.a > 0)
            {
                lblTypeCls.Text = Converter.GetString(accgetDTO.AccAtyClass);
                lblChkCuType.Text = Converter.GetString(accgetDTO.CuType);
                
                lblChkCuNo.Text = Converter.GetString(accgetDTO.CuNo);
               
                lblChkMemNo.Text = Converter.GetString(accgetDTO.MemberNo);
               
                if (lblTypeCls.Text != "5" && lblTypeCls.Text != "6")
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (txtCreditUNo.Text != string.Empty && (lblCuType.Text != lblChkCuType.Text || lblCuNo.Text != lblChkCuNo.Text))
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (ChkAllMemNo.Checked == false && txtMemNo.Text != lblChkMemNo.Text)
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (txtCreditUNo.Text == string.Empty)
                {
                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                    ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCuNo.Text);

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                    ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");

                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);
                    ddlMemNo.SelectedValue = Converter.GetString(txtMemNo.Text);

                    txtAccType.Text = Converter.GetString(accgetDTO.AccType);
                    ddlAccType.SelectedValue = Converter.GetString(accgetDTO.AccType);

                    ddlAccNo.SelectedValue = Converter.GetString(txtAccNo.Text);

                    ChkAllMemNo.Checked = false;
                    ChkAllAccType.Checked = false;
                }


            }
        }

        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccNo.Text = Converter.GetString(ddlAccNo.SelectedValue);

            Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
            A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
            if (accgetDTO.a > 0)
            {
                lblTypeCls.Text = Converter.GetString(accgetDTO.AccAtyClass);
                lblChkCuType.Text = Converter.GetString(accgetDTO.CuType);

                lblChkCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                lblChkMemNo.Text = Converter.GetString(accgetDTO.MemberNo);

                if (lblTypeCls.Text != "5" && lblTypeCls.Text != "6")
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (txtCreditUNo.Text != string.Empty && (lblCuType.Text != lblChkCuType.Text || lblCuNo.Text != lblChkCuNo.Text))
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (ChkAllMemNo.Checked == false && txtMemNo.Text != lblChkMemNo.Text)
                {
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account No.');", true);
                    return;
                }

                if (txtCreditUNo.Text == string.Empty)
                {
                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                    ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCuNo.Text);

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                    ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");

                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);
                    ddlMemNo.SelectedValue = Converter.GetString(txtMemNo.Text);

                    txtAccType.Text = Converter.GetString(accgetDTO.AccType);
                    ddlAccType.SelectedValue = Converter.GetString(accgetDTO.AccType);

                    ChkAllMemNo.Checked = false;
                    ChkAllAccType.Checked = false;
                }


            }
        }


    }
}