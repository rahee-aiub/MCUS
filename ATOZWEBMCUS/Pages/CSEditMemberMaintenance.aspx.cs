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
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccessLayer.BLL;
namespace ATOZWEBMCUS.Pages
{
    public partial class CSEditMemberMaintenance : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    CtrlModule.Text = Request.QueryString["a%b"];

                    if (CtrlModule.Text == "1")
                    {
                        lblCuName.Visible = false;
                        CreditUnionDropdown();
                    }

                    if (CtrlModule.Text == "6")
                    {
                        lblCuName.Visible = true;
                        ddlCreditUNo.Visible = false;
                        ddlCULBMemNo.Visible = false;
                    }


                    DivisionDropdown();
                    PerDivisionDropdown();

                    BtnUpdate.Visible = false;
                    txtCreditUNo.Focus();
                    pnlPermanent.Visible = false;
                    pnlOtherInfo.Visible = false;
                    DistrictInFo();
                    UpzilaInfo();
                    ThanaInfo();
                    PerDistrictInFo();
                    PerUpzilaInfo();
                    PerThanaInfo();
                    OccupationDropdown();
                    NationalityDropdown();
                    NatureDropdown();
                    ReligionDropdown();

                    BtnSubmit.Visible = false;
                    txtAge.ReadOnly = true;

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtOpenDate.Text = date;
                    ProcDate.Text = date;

                    //int lblAge = Convert.Int32(ProcDate.Text - txtDateOfBirth.Text);

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        private void OccupationDropdown()
        {

            string sqlquery = "SELECT ProfessionCode,ProfessionDescription from A2ZPROFESSION";
            ddlOccupation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOccupation, "A2ZCSMCUS");

        }

        private void NationalityDropdown()
        {

            string sqlquery = "SELECT NationalityCode,NationalityDescription from A2ZNATIONALITY";
            ddlNationality = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNationality, "A2ZHKMCUS");

        }

        private void NatureDropdown()
        {

            string sqlquery = "SELECT NatureCode,NatureDescription from A2ZNATURE";
            ddlNature = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNature, "A2ZCSMCUS");

        }


        private void ReligionDropdown()
        {

            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZHKMCUS");

        }


        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviOrgCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZHKMCUS");

        }

        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' or DiviCode = '0'";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }

        private void UpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' and DistOrgCode='" + ddlDistrict.SelectedValue + "' or DistCode = '0'";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }

        private void ThanaDropdown()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' and DistOrgCode='" + ddlDistrict.SelectedValue + "' and UpzilaOrgCode='" + ddlUpzila.SelectedValue + "' or DistCode = '0'";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }
        private void PerDivisionDropdown()
        {

            string sqlquery = "SELECT DiviOrgCode,DiviDescription from A2ZDIVISION";
            ddlPerDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPerDivision, "A2ZHKMCUS");

        }

        private void PerDistrictDropdown()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT WHERE DiviOrgCode='" + ddlPerDivision.SelectedValue + "' or DiviCode = '0'";
            ddlPerDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerDistrict, "A2ZHKMCUS");

        }

        private void PerUpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilOrgCode,ThanaDescription FROM A2ZUPZILA WHERE DiviOrgCode='" + ddlPerDivision.SelectedValue + "' and DistOrgCode='" + ddlPerDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPerUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerUpzila, "A2ZHKMCUS");

        }
        private void PerThanaDropdown()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA WHERE DiviOrgCode='" + ddlPerDivision.SelectedValue + "' and DistOrgCode='" + ddlPerDistrict.SelectedValue + "' and UpzilaOrgCode='" + ddlPerUpzila.SelectedValue + "' or DistCode = '0'";

            ddlPerThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerThana, "A2ZHKMCUS");

        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' ORDER BY CuName ASC";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }



        public void clearInfo()
        {

            txtCULBMemName.Text = string.Empty;
            //txtCULBMemNo.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtMotherName.Text = string.Empty;
            txtSpouseName.Text = string.Empty;

            txtDateOfBirth.Text = string.Empty;
            txtPlaceOfBirth.Text = string.Empty;
            ddlOccupation.SelectedValue = "0";
            ddlNationality.SelectedValue = "0";
            ddlGender.SelectedIndex = 0;
            ddlMemType.SelectedIndex = 0;
            ddlReligion.SelectedValue = "0";
            ddlNature.SelectedValue = "0";
            ddlMaritalStatus.SelectedIndex = 0;
            txtAddressL1.Text = string.Empty;
            txtAddressL2.Text = string.Empty;
            txtAddressL3.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPerAddL1.Text = string.Empty;
            txtPerAddL2.Text = string.Empty;
            txtPerAddL3.Text = string.Empty;
            txtPerTelNo.Text = string.Empty;
            txtPerMobNo.Text = string.Empty;
            txtPerEmail.Text = string.Empty;
            txtEmpName.Text = string.Empty;
            txtEmpAddress.Text = string.Empty;
            txtIntroducerMem1.Text = string.Empty;
            txtIntroducerName1.Text = string.Empty;
            txtIntroduceMem2.Text = string.Empty;
            txtIntroducerName2.Text = string.Empty;
            txtPassportNo.Text = string.Empty;
            txtPassportIssueDate.Text = string.Empty;
            txtPassportIssuePlace.Text = string.Empty;
            txtNationalID.Text = string.Empty;
            txtPassportExpdate.Text = string.Empty;
            txtTIN.Text = string.Empty;
            txtLastTaxPdate.Text = string.Empty;


        }

        protected void DisplayMessage()
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
            c = string.Format(lblCUNumber.Text);
            d = string.Format(lblCUType.Text);
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

                    int CN = Converter.GetInteger(txtCreditUNo.Text);

                    hdnCuNumber.Text = Converter.GetString(CN);

                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCUType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCUNumber.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblCUType.Text = Converter.GetString(getDTO.CuType);
                        lblCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblCUNumber.Text = Converter.GetString(getDTO.CreditUnionNo);

                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblCUNumber.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblCUType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblCUNumber.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblCUType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }

                            DisplayMessage();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }



                        txtCreditUNo.Text = (lblCUType.Text + "-" + lblCUNumber.Text);

                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        lblCuOpenDate.Text = date;


                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + lblCUType.Text + "'";
                        if (CtrlModule.Text == "1")
                        {
                            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                            ddlCreditUNo.SelectedValue = Converter.GetString(lblCUType.Text + lblCUNumber.Text);
                        }
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        txtCULBMemNo.Focus();
                        clearInfo();
                        if (CtrlModule.Text == "1")
                        {
                            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCUNumber.Text + "'and CuType='" + lblCUType.Text + "' GROUP BY MemNo,MemName ORDER BY MemName ASC";
                            ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                        }
                    }
                    else
                    {
                        InvalidCUMSG();

                        clearInfo();
                        txtCreditUNo.Focus();
                        txtCreditUNo.Text = string.Empty;
                        txtCULBMemNo.Text = string.Empty;
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        if (CtrlModule.Text == "1")
                        {
                            ddlCreditUNo.SelectedValue = "-Select-";
                            ddlCULBMemNo.SelectedIndex = 0;
                        }
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
                    clearInfo();
                    txtCreditUNo.Text = string.Empty;
                    txtCULBMemNo.Focus();
                    ddlCULBMemNo.SelectedIndex = 0;
                    return;
                }


                // CULBMemberDropdown();


                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    int CN = Converter.GetInteger(ddlCreditUNo.SelectedValue);

                    hdnCuNumber.Text = Converter.GetString(CN);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCUType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCUNumber.Text = Converter.GetString(CNo);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName ORDER BY MemName ASC";

                    ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));

                    if (getDTO.NoRecord > 0)
                    {
                        lblCUType.Text = Converter.GetString(getDTO.CuType);
                        lblCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblCUNumber.Text = Converter.GetString(getDTO.CreditUnionNo);

                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblCUNumber.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblCUType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblCUNumber.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblCUType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }

                            DisplayMessage();
                            txtCreditUNo.Text = string.Empty;
                            ddlCreditUNo.SelectedValue = "-Select-";
                            txtCreditUNo.Focus();
                            return;
                        }
                        


                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        lblCuOpenDate.Text = date;
                        //txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = (lblCUType.Text + "-" + lblCUNumber.Text);
                        txtCULBMemNo.Focus();
                        clearInfo();
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
        private void InvalidMemMSG()
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


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }


        protected void ddlCULBMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCreditUNo.SelectedItem.Text == "-Select-")
                {
                    txtCreditUNo.Focus();
                    InvalidCUMSG();
                    return;
                }


                if (ddlCULBMemNo.SelectedItem.Text == "-Select-")
                {
                    txtCULBMemNo.Text = string.Empty;
                    clearInfo();
                    txtCULBMemNo.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;

                }


                if (ddlCULBMemNo.SelectedValue != "-Select-" && ddlCreditUNo.SelectedValue != "-Select-")
                {

                    Int16 CuType = Converter.GetSmallInteger(lblCUType.Text);
                    int CNo = Converter.GetSmallInteger(lblCUNumber.Text);

                    int MemNumber = Converter.GetInteger(ddlCULBMemNo.SelectedValue);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {

                        txtCULBMemName.Text = getDTO.MemberName;
                        txtCULBMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        ddlCULBMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        txtFatherName.Text = Converter.GetString(getDTO.FatherName);
                        txtMotherName.Text = Converter.GetString(getDTO.MotherName);
                        txtSpouseName.Text = Converter.GetString(getDTO.SpouseName);
                        if (getDTO.OpenDate == DateTime.MinValue)
                        {
                            txtOpenDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt = Converter.GetDateTime(getDTO.OpenDate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtOpenDate.Text = date;
                            GetOpenDate.Text = date;
                        }
                        if (getDTO.DateOfBirth == DateTime.MinValue)
                        {
                            txtDateOfBirth.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt1 = Converter.GetDateTime(getDTO.DateOfBirth);
                            string date1 = dt1.ToString("dd/MM/yyyy");
                            txtDateOfBirth.Text = date1;
                        }

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime fDate = Converter.GetDateTime(dto.ProcessDate);



                        //DateTime tDate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        if (getDTO.DateOfBirth != DateTime.MinValue)
                        {
                            DateTime tDate = Converter.GetDateTime(getDTO.DateOfBirth);
                            int a1 = Convert.ToInt32(fDate.Subtract(tDate).TotalDays);
                            int b1 = a1 / 365;
                            txtAge.Text = Converter.GetString(b1);
                        }
                        txtPlaceOfBirth.Text = Converter.GetString(getDTO.PlaceofBirth);
                        ddlOccupation.SelectedValue = Converter.GetString(getDTO.Occupation);
                        ddlNationality.SelectedValue = Converter.GetString(getDTO.Nationality);
                        ddlGender.SelectedValue = Converter.GetString(getDTO.Gender);
                        ddlMemType.SelectedValue = Converter.GetString(getDTO.MemType);
                        ddlReligion.SelectedValue = Converter.GetString(getDTO.Religion);
                        ddlNature.SelectedValue = Converter.GetString(getDTO.Nature);
                        ddlMaritalStatus.SelectedValue = Converter.GetString(getDTO.MaritalStatus);
                        txtAddressL1.Text = Converter.GetString(getDTO.PreAddressLine1);
                        txtAddressL2.Text = Converter.GetString(getDTO.PreAddressLine2);
                        txtAddressL3.Text = Converter.GetString(getDTO.PreAddressLine3);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.PreDivision);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.PreDistrict);
                        ddlUpzila.SelectedValue = Converter.GetString(getDTO.preUpzila);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.preThana);
                        txtTelNo.Text = Converter.GetString(getDTO.PreTelephone);
                        txtMobileNo.Text = Converter.GetString(getDTO.PreMobile);
                        txtEmail.Text = Converter.GetString(getDTO.PreEmail);
                        txtPerAddL1.Text = Converter.GetString(getDTO.PerAddressLine1);
                        txtPerAddL2.Text = Converter.GetString(getDTO.PerAddressLine2);
                        txtPerAddL3.Text = Converter.GetString(getDTO.PerAddressLine3);
                        ddlPerDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
                        ddlPerUpzila.SelectedValue = Converter.GetString(getDTO.PerUpzila);
                        ddlPerThana.SelectedValue = Converter.GetString(getDTO.PerThana);
                        txtPerTelNo.Text = Converter.GetString(getDTO.PerTelephone);
                        txtPerMobNo.Text = Converter.GetString(getDTO.PerMobile);
                        txtPerEmail.Text = Converter.GetString(getDTO.PerEmail);
                        txtEmpName.Text = Converter.GetString(getDTO.EmployerName);
                        txtEmpAddress.Text = Converter.GetString(getDTO.EmployerAddress);
                        txtIntroducerMem1.Text = Converter.GetString(getDTO.IntroducerNo1);
                        txtIntroducerName1.Text = Converter.GetString(getDTO.IntroducerName1);
                        txtIntroduceMem2.Text = Converter.GetString(getDTO.IntroducerNo2);
                        txtIntroducerName2.Text = Converter.GetString(getDTO.IntroducerName2);
                        txtPassportNo.Text = Converter.GetString(getDTO.PassportNo);
                        if (getDTO.PassportIssueDate == DateTime.MinValue)
                        {
                            txtPassportIssueDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt2 = Converter.GetDateTime(getDTO.PassportIssueDate);
                            string date2 = dt2.ToString("dd/MM/yyyy");
                            txtPassportIssueDate.Text = date2;
                        }
                        txtPassportIssuePlace.Text = Converter.GetString(getDTO.PassportIssuePlace);
                        txtNationalID.Text = Converter.GetString(getDTO.NationalIdNo);
                        if (getDTO.PassportExpiryDate == DateTime.MinValue)
                        {
                            txtPassportExpdate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt4 = Converter.GetDateTime(getDTO.PassportExpiryDate);
                            string date4 = dt4.ToString("dd/MM/yyyy");
                            txtPassportExpdate.Text = date4;
                        }

                        txtTIN.Text = Converter.GetString(getDTO.TIN);
                        if (getDTO.LastTaxPayDate == DateTime.MinValue)
                        {
                            txtLastTaxPdate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt3 = Converter.GetDateTime(getDTO.LastTaxPayDate);
                            string date3 = dt3.ToString("dd/MM/yyyy");
                            txtLastTaxPdate.Text = date3;
                        }
                        txtCULBMemName.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        InvalidMemMSG();
                        clearInfo();
                        txtCULBMemNo.Text = string.Empty;
                        txtCULBMemNo.Focus();
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        ddlCULBMemNo.SelectedIndex = 0;


                    }


                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCULBMemNo_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void txtCULBMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCreditUNo.Text == string.Empty)
                {
                    txtCreditUNo.Focus();
                    InvalidCUMSG();
                    return;
                }


                if (txtCULBMemNo.Text != string.Empty)
                {
                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(lblCUType.Text);
                    int CNo = Converter.GetSmallInteger(lblCUNumber.Text);

                    int MemNumber = Converter.GetInteger(txtCULBMemNo.Text);

                    int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                    //if (getDTO.NoRecord > 0)

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        if (CtrlModule.Text == "1")
                        {
                            ddlCULBMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        }

                        txtCULBMemNo.Text = Converter.GetString(getDTO.MemberNo);

                        txtCULBMemName.Text = getDTO.MemberName;
                        txtFatherName.Text = Converter.GetString(getDTO.FatherName);
                        txtMotherName.Text = Converter.GetString(getDTO.MotherName);
                        txtSpouseName.Text = Converter.GetString(getDTO.SpouseName);
                        if (getDTO.OpenDate == DateTime.MinValue)
                        {
                            txtOpenDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt = Converter.GetDateTime(getDTO.OpenDate);
                            string date = dt.ToString("dd/MM/yyyy");
                            txtOpenDate.Text = date;
                            GetOpenDate.Text = date;
                        }
                        if (getDTO.DateOfBirth == DateTime.MinValue)
                        {
                            txtDateOfBirth.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt1 = Converter.GetDateTime(getDTO.DateOfBirth);
                            string date1 = dt1.ToString("dd/MM/yyyy");
                            txtDateOfBirth.Text = date1;
                        }

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime fDate = Converter.GetDateTime(dto.ProcessDate);

                        //DateTime tDate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        if (getDTO.DateOfBirth != DateTime.MinValue)
                        {
                            DateTime tDate = Converter.GetDateTime(getDTO.DateOfBirth);
                            int a1 = Convert.ToInt32(fDate.Subtract(tDate).TotalDays);
                            int b1 = a1 / 365;
                            txtAge.Text = Converter.GetString(b1);
                        }

                        txtPlaceOfBirth.Text = Converter.GetString(getDTO.PlaceofBirth);
                        ddlOccupation.SelectedValue = Converter.GetString(getDTO.Occupation);
                        ddlNationality.SelectedValue = Converter.GetString(getDTO.Nationality);
                        ddlGender.SelectedValue = Converter.GetString(getDTO.Gender);
                        ddlMemType.SelectedValue = Converter.GetString(getDTO.MemType);
                        ddlReligion.SelectedValue = Converter.GetString(getDTO.Religion);
                        ddlNature.SelectedValue = Converter.GetString(getDTO.Nature);
                        ddlMaritalStatus.SelectedValue = Converter.GetString(getDTO.MaritalStatus);
                        txtAddressL1.Text = Converter.GetString(getDTO.PreAddressLine1);
                        txtAddressL2.Text = Converter.GetString(getDTO.PreAddressLine2);
                        txtAddressL3.Text = Converter.GetString(getDTO.PreAddressLine3);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.PreDivision);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.PreDistrict);
                        ddlUpzila.SelectedValue = Converter.GetString(getDTO.preUpzila);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.preThana);
                        txtTelNo.Text = Converter.GetString(getDTO.PreTelephone);
                        txtMobileNo.Text = Converter.GetString(getDTO.PreMobile);
                        txtEmail.Text = Converter.GetString(getDTO.PreEmail);
                        txtPerAddL1.Text = Converter.GetString(getDTO.PerAddressLine1);
                        txtPerAddL2.Text = Converter.GetString(getDTO.PerAddressLine2);
                        txtPerAddL3.Text = Converter.GetString(getDTO.PerAddressLine3);
                        ddlPerDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
                        ddlPerUpzila.SelectedValue = Converter.GetString(getDTO.PerUpzila);
                        ddlPerThana.SelectedValue = Converter.GetString(getDTO.PerThana);
                        txtPerTelNo.Text = Converter.GetString(getDTO.PerTelephone);
                        txtPerMobNo.Text = Converter.GetString(getDTO.PerMobile);
                        txtPerEmail.Text = Converter.GetString(getDTO.PerEmail);
                        txtEmpName.Text = Converter.GetString(getDTO.EmployerName);
                        txtEmpAddress.Text = Converter.GetString(getDTO.EmployerAddress);
                        txtIntroducerMem1.Text = Converter.GetString(getDTO.IntroducerNo1);
                        txtIntroducerName1.Text = Converter.GetString(getDTO.IntroducerName1);
                        txtIntroduceMem2.Text = Converter.GetString(getDTO.IntroducerNo2);
                        txtIntroducerName2.Text = Converter.GetString(getDTO.IntroducerName2);
                        txtPassportNo.Text = Converter.GetString(getDTO.PassportNo);
                        if (getDTO.PassportIssueDate == DateTime.MinValue)
                        {
                            txtPassportIssueDate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt2 = Converter.GetDateTime(getDTO.PassportIssueDate);
                            string date2 = dt2.ToString("dd/MM/yyyy");
                            txtPassportIssueDate.Text = date2;
                        }

                        txtPassportIssuePlace.Text = Converter.GetString(getDTO.PassportIssuePlace);
                        txtNationalID.Text = Converter.GetString(getDTO.NationalIdNo);

                        if (getDTO.PassportExpiryDate == DateTime.MinValue)
                        {
                            txtPassportExpdate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt4 = Converter.GetDateTime(getDTO.PassportExpiryDate);
                            string date4 = dt4.ToString("dd/MM/yyyy");
                            txtPassportExpdate.Text = date4;
                        }

                        txtTIN.Text = Converter.GetString(getDTO.TIN);
                        if (getDTO.LastTaxPayDate == DateTime.MinValue)
                        {
                            txtLastTaxPdate.Text = string.Empty;
                        }
                        else
                        {
                            DateTime dt3 = Converter.GetDateTime(getDTO.LastTaxPayDate);
                            string date3 = dt3.ToString("dd/MM/yyyy");
                            txtLastTaxPdate.Text = date3;
                        }

                        txtCULBMemName.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        InvalidMemMSG();
                        clearInfo();
                        txtCULBMemNo.Text = string.Empty;
                        txtCULBMemNo.Focus();
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = false;
                        if (CtrlModule.Text == "1")
                        {
                            ddlCULBMemNo.SelectedIndex = 0;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCULBMemNo_TextChanged Problem');</script>");
                //throw ex;
            }

        }


        protected void btnPresentAddress_Click(object sender, EventArgs e)
        {
            pnlPreAddress.Visible = true;
            pnlPermanent.Visible = false;
            pnlOtherInfo.Visible = false;
        }

        protected void btnPerAddress_Click(object sender, EventArgs e)
        {
            pnlPermanent.Visible = true;
            pnlPreAddress.Visible = false;
            pnlOtherInfo.Visible = false;
        }

        protected void btnOtherInfo_Click(object sender, EventArgs e)
        {
            pnlOtherInfo.Visible = true;
            pnlPermanent.Visible = false;
            pnlPreAddress.Visible = false;
        }


        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    ddlDistrict.SelectedIndex = 0;
                    ddlUpzila.SelectedIndex = 0;
                    ddlThana.SelectedIndex = 0;
                    DistrictDropdown();
                    UpzilaDropdown();
                    ThanaDropdown();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDivision_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode FROM A2ZDISTRICT Where DistOrgCode='" + ddlDistrict.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();


                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);


                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                    }
                }

                ddlUpzila.SelectedIndex = 0;
                ddlThana.SelectedIndex = 0;
                UpzilaDropdown();
                ThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDivision_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode FROM A2ZUPZILA Where UpzilaOrgCode='" + ddlUpzila.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);

                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                        ddlUpzila.SelectedValue = Converter.GetString(c);

                    }
                }

                ddlThana.SelectedIndex = 0;
                ThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDistrict_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }
        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode,ThanaOrgCode FROM A2ZTHANA Where ThanaOrgCode='" + ddlThana.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();
                        var ThanaOrg = dr1["ThanaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);
                        int d = Converter.GetInteger(ThanaOrg);

                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                        ddlUpzila.SelectedValue = Converter.GetString(c);
                        ddlThana.SelectedValue = Converter.GetString(d);
                    }
                }

                DivisionDropdown();
                DistrictDropdown();
                UpzilaDropdown();
                ThanaDropdown();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlThana_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }


        private void DistrictInFo()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }

        private void UpzilaInfo()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }

        private void ThanaInfo()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }
        private void PerDistrictInFo()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT";
            ddlPerDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerDistrict, "A2ZHKMCUS");

        }

        private void PerUpzilaInfo()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA";

            ddlPerUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerUpzila, "A2ZHKMCUS");

        }

        private void PerThanaInfo()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA";

            ddlPerThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerThana, "A2ZHKMCUS");

        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZMEMBERDTO objDTO = new A2ZMEMBERDTO();
                objDTO.CuType = Converter.GetSmallInteger(lblCUType.Text);
                objDTO.CreditUnionNo = Converter.GetInteger(lblCUNumber.Text);
                objDTO.MemberNo = Converter.GetInteger(txtCULBMemNo.Text);
                objDTO.MemberName = Converter.GetString(txtCULBMemName.Text);
                objDTO.FatherName = Converter.GetString(txtFatherName.Text);
                objDTO.MotherName = Converter.GetString(txtMotherName.Text);
                objDTO.SpouseName = Converter.GetString(txtSpouseName.Text);
                objDTO.Occupation = Converter.GetSmallInteger(ddlOccupation.SelectedValue);
                objDTO.Nationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                objDTO.Gender = Converter.GetSmallInteger(ddlGender.SelectedValue);
                objDTO.MemType = Converter.GetSmallInteger(ddlMemType.SelectedValue);
                objDTO.Religion = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                objDTO.Nature = Converter.GetSmallInteger(ddlNature.SelectedValue);
                objDTO.MaritalStatus = Converter.GetSmallInteger(ddlMaritalStatus.SelectedValue);

                if (txtOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.OpenDate = opdate;

                }
                else
                {
                    string CheckOpDtNull = "";
                    objDTO.OpenNullDate = CheckOpDtNull;

                }

                if (txtDateOfBirth.Text != string.Empty)
                {
                    DateTime datebirth = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.DateOfBirth = datebirth;
                }
                else
                {
                    string checkDOBNull = "";
                    objDTO.DOBNullDate = checkDOBNull;

                }
                objDTO.PlaceofBirth = Converter.GetString(txtPlaceOfBirth.Text);
                objDTO.PreAddressLine1 = Converter.GetString(txtAddressL1.Text);
                objDTO.PreAddressLine2 = Converter.GetString(txtAddressL2.Text);
                objDTO.PreAddressLine3 = Converter.GetString(txtAddressL3.Text);
                objDTO.PreDivision = Converter.GetInteger(ddlDivision.SelectedValue);
                objDTO.PreDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
                objDTO.preThana = Converter.GetInteger(ddlThana.SelectedValue);
                objDTO.preUpzila = Converter.GetInteger(ddlUpzila.SelectedValue);
                objDTO.PreTelephone = Converter.GetString(txtTelNo.Text);
                objDTO.PreMobile = Converter.GetString(txtMobileNo.Text);
                objDTO.PreEmail = Converter.GetString(txtEmail.Text);
                objDTO.PerAddressLine1 = Converter.GetString(txtPerAddL1.Text);
                objDTO.PerAddressLine2 = Converter.GetString(txtPerAddL2.Text);
                objDTO.PerAddressLine3 = Converter.GetString(txtPerAddL3.Text);
                objDTO.PerDivision = Converter.GetInteger(ddlPerDivision.SelectedValue);
                objDTO.PerDistrict = Converter.GetInteger(ddlPerDistrict.SelectedValue);
                objDTO.PerUpzila = Converter.GetInteger(ddlPerUpzila.SelectedValue);
                objDTO.PerThana = Converter.GetInteger(ddlPerThana.SelectedValue);
                objDTO.PerTelephone = Converter.GetString(txtPerTelNo.Text);
                objDTO.PerMobile = Converter.GetString(txtPerMobNo.Text);
                objDTO.PerEmail = Converter.GetString(txtPerEmail.Text);
                objDTO.EmployerName = Converter.GetString(txtEmpName.Text);
                objDTO.EmployerAddress = Converter.GetString(txtEmpAddress.Text);
                objDTO.IntroducerNo1 = Converter.GetString(txtIntroducerMem1.Text);
                objDTO.IntroducerName1 = Converter.GetString(txtIntroducerName1.Text);
                objDTO.IntroducerNo2 = Converter.GetString(txtIntroduceMem2.Text);
                objDTO.IntroducerName2 = Converter.GetString(txtIntroducerName2.Text);
                objDTO.NationalIdNo = Converter.GetString(txtNationalID.Text);
                objDTO.PassportNo = Converter.GetString(txtPassportNo.Text);
                if (txtPassportIssueDate.Text != string.Empty)
                {
                    DateTime Issuedate = DateTime.ParseExact(txtPassportIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.PassportIssueDate = Issuedate;
                }
                else
                {
                    string checkPPIssDtNull = "";
                    objDTO.PPIssueNullDate = checkPPIssDtNull;

                }
                if (txtPassportExpdate.Text != string.Empty)
                {
                    DateTime Expdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.PassportExpiryDate = Expdate;
                }
                else
                {
                    string checkPPExpDtNull = "";
                    objDTO.PPExpNullDate = checkPPExpDtNull;

                }
                objDTO.PassportIssuePlace = Converter.GetString(txtPassportIssuePlace.Text);
                objDTO.TIN = Converter.GetString(txtTIN.Text);
                if (txtLastTaxPdate.Text != string.Empty)
                {
                    DateTime LTPaydate = DateTime.ParseExact(txtLastTaxPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.LastTaxPayDate = LTPaydate;
                }
                else
                {
                    string checkLastPayDtNull = "";
                    objDTO.LTaxPayNullDate = checkLastPayDtNull;

                }


                int roweffect = A2ZMEMBERDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtCULBMemNo.Text = string.Empty;
                    clearInfo();
                    txtCULBMemNo.Focus();
                    if (CtrlModule.Text == "1")
                    {
                        string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCUNumber.Text + "'and CuType='" + lblCUType.Text + "' GROUP BY MemNo,MemName";
                        ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                    }
                    ddlDivision.SelectedValue = "-Select-";
                    ddlDistrict.SelectedValue = "-Select-";
                    ddlThana.SelectedValue = "-Select-";
                    ddlUpzila.SelectedValue = "-Select-";
                    ddlPerDivision.SelectedValue = "-Select-";
                    ddlPerDistrict.SelectedValue = "-Select-";
                    ddlPerThana.SelectedValue = "-Select-";
                    ddlPerUpzila.SelectedValue = "-Select-";
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZMEMBERDTO UpDTO = new A2ZMEMBERDTO();
                UpDTO.CuType = Converter.GetSmallInteger(lblCUType.Text);
                UpDTO.CreditUnionNo = Converter.GetInteger(lblCUNumber.Text);
                UpDTO.MemberNo = Converter.GetInteger(txtCULBMemNo.Text);
                UpDTO.MemberName = Converter.GetString(ddlCULBMemNo.SelectedItem.Text);
                UpDTO.MemberName = Converter.GetString(txtCULBMemName.Text);
                UpDTO.FatherName = Converter.GetString(txtFatherName.Text);
                UpDTO.MotherName = Converter.GetString(txtMotherName.Text);
                UpDTO.SpouseName = Converter.GetString(txtSpouseName.Text);
                UpDTO.Occupation = Converter.GetSmallInteger(ddlOccupation.SelectedValue);
                UpDTO.Nationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                UpDTO.Gender = Converter.GetSmallInteger(ddlGender.SelectedValue);
                UpDTO.MemType = Converter.GetSmallInteger(ddlMemType.SelectedValue);
                UpDTO.Religion = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                UpDTO.Nature = Converter.GetSmallInteger(ddlNature.SelectedValue);
                UpDTO.MaritalStatus = Converter.GetSmallInteger(ddlMaritalStatus.SelectedValue);
                if (txtOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.OpenDate = opdate;
                }
                else
                {
                    string CheckOpDtNull = "";
                    UpDTO.OpenNullDate = CheckOpDtNull;

                }
                if (txtDateOfBirth.Text != string.Empty)
                {
                    DateTime datebirth = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.DateOfBirth = datebirth;
                }
                else
                {
                    string checkDOBNull = "";
                    UpDTO.DOBNullDate = checkDOBNull;

                }
                UpDTO.PlaceofBirth = Converter.GetString(txtPlaceOfBirth.Text);
                UpDTO.PreAddressLine1 = Converter.GetString(txtAddressL1.Text);
                UpDTO.PreAddressLine2 = Converter.GetString(txtAddressL2.Text);
                UpDTO.PreAddressLine3 = Converter.GetString(txtAddressL3.Text);
                UpDTO.PreDivision = Converter.GetInteger(ddlDivision.SelectedValue);
                UpDTO.PreDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
                UpDTO.preThana = Converter.GetInteger(ddlThana.SelectedValue);
                UpDTO.preUpzila = Converter.GetInteger(ddlUpzila.SelectedValue);
                UpDTO.PreTelephone = Converter.GetString(txtTelNo.Text);
                UpDTO.PreMobile = Converter.GetString(txtMobileNo.Text);
                UpDTO.PreEmail = Converter.GetString(txtEmail.Text);
                UpDTO.PerAddressLine1 = Converter.GetString(txtPerAddL1.Text);
                UpDTO.PerAddressLine2 = Converter.GetString(txtPerAddL2.Text);
                UpDTO.PerAddressLine3 = Converter.GetString(txtPerAddL3.Text);
                UpDTO.PerDivision = Converter.GetInteger(ddlPerDivision.SelectedValue);
                UpDTO.PerDistrict = Converter.GetInteger(ddlPerDistrict.SelectedValue);
                UpDTO.PerUpzila = Converter.GetInteger(ddlPerUpzila.SelectedValue);
                UpDTO.PerThana = Converter.GetInteger(ddlPerThana.SelectedValue);
                UpDTO.PerTelephone = Converter.GetString(txtPerTelNo.Text);
                UpDTO.PerMobile = Converter.GetString(txtPerMobNo.Text);
                UpDTO.PerEmail = Converter.GetString(txtPerEmail.Text);
                UpDTO.EmployerName = Converter.GetString(txtEmpName.Text);
                UpDTO.EmployerAddress = Converter.GetString(txtEmpAddress.Text);
                UpDTO.IntroducerNo1 = Converter.GetString(txtIntroducerMem1.Text);
                UpDTO.IntroducerName1 = Converter.GetString(txtIntroducerName1.Text);
                UpDTO.IntroducerNo2 = Converter.GetString(txtIntroduceMem2.Text);
                UpDTO.IntroducerName2 = Converter.GetString(txtIntroducerName2.Text);
                UpDTO.NationalIdNo = Converter.GetString(txtNationalID.Text);
                UpDTO.PassportNo = Converter.GetString(txtPassportNo.Text);
                if (txtPassportIssueDate.Text != string.Empty)
                {
                    DateTime Issuedate = DateTime.ParseExact(txtPassportIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.PassportIssueDate = Issuedate;
                }
                else
                {
                    string checkPPIssDtNull = "";
                    UpDTO.PPIssueNullDate = checkPPIssDtNull;

                }
                if (txtPassportExpdate.Text != string.Empty)
                {
                    DateTime Expdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.PassportExpiryDate = Expdate;
                }
                else
                {
                    string checkPPExpDtNull = "";
                    UpDTO.PPExpNullDate = checkPPExpDtNull;

                }
                UpDTO.PassportIssuePlace = Converter.GetString(txtPassportIssuePlace.Text);
                UpDTO.TIN = Converter.GetString(txtTIN.Text);
                if (txtLastTaxPdate.Text != string.Empty)
                {
                    DateTime LTPaydate = DateTime.ParseExact(txtLastTaxPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.LastTaxPayDate = LTPaydate;
                }
                else
                {
                    string checkLastPayDtNull = "";
                    UpDTO.LTaxPayNullDate = checkLastPayDtNull;

                }



                int roweffect = A2ZMEMBERDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    if (CtrlModule.Text == "1")
                    {
                        string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCUNumber.Text + "'and CuType='" + lblCUType.Text + "' GROUP BY MemNo,MemName";
                        ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                        ddlCULBMemNo.SelectedValue = "-Select-";
                    }
                    txtCULBMemNo.Text = string.Empty;
                    txtCULBMemNo.Focus();
                    clearInfo();
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = false;

                }
                else
                {
                    clearInfo();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }



        protected void ddlOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlOccupation.SelectedValue == "-Select-")
                {
                    clearInfo();
                    ddlOccupation.SelectedIndex = 0;
                    return;
                }


                if (ddlOccupation.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlOccupation.SelectedValue);
                    A2ZPROFESSIONDTO getDTO = (A2ZPROFESSIONDTO.GetInformation(code));
                    if (getDTO.ProfessionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    else
                    {
                        //  ddlOccupation.SelectedValue = "-Select-";
                        //ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlOccupation_SelectedIndexChanged Problem');</script>");
                throw ex;
            }


        }

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlNationality.SelectedValue == "-Select-")
                {
                    clearInfo();
                    ddlNationality.SelectedIndex = 0;
                    return;
                }


                if (ddlNationality.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                    A2ZNATIONALITYDTO getDTO = (A2ZNATIONALITYDTO.GetInformation(code));
                    if (getDTO.NationalityCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    else
                    {
                        //ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlNationality_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlNature.SelectedValue == "-Select-")
                {
                    clearInfo();
                    ddlNature.SelectedIndex = 0;
                    return;
                }

                if (ddlNature.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlNature.SelectedValue);
                    A2ZNATUREDTO getDTO = (A2ZNATUREDTO.GetInformation(code));
                    if (getDTO.NatureCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    else
                    {
                        //ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlNature_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlReligion.SelectedValue == "-Select-")
                {
                    clearInfo();
                    ddlReligion.SelectedIndex = 0;
                    return;
                }


                if (ddlReligion.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                    A2ZRELIGIONDTO getDTO = (A2ZRELIGIONDTO.GetInformation(code));
                    if (getDTO.RelegionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    else
                    {
                        //ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlReligion_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlPerDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPerDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlPerDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    ddlPerDistrict.SelectedIndex = 0;
                    ddlPerUpzila.SelectedIndex = 0;
                    ddlPerThana.SelectedIndex = 0;
                    PerDistrictDropdown();
                    PerUpzilaDropdown();
                    PerThanaDropdown();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlPerDivision_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlPerDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode FROM A2ZDISTRICT Where DistOrgCode='" + ddlPerDistrict.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();


                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);


                        ddlPerDivision.SelectedValue = Converter.GetString(a);
                        ddlPerDistrict.SelectedValue = Converter.GetString(b);
                    }
                }

                ddlPerUpzila.SelectedIndex = 0;
                ddlPerThana.SelectedIndex = 0;
                PerUpzilaDropdown();
                PerThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDistrict_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }




        protected void ddlPerUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode FROM A2ZUPZILA Where UpzilaOrgCode='" + ddlPerUpzila.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);

                        ddlPerDivision.SelectedValue = Converter.GetString(a);
                        ddlPerDistrict.SelectedValue = Converter.GetString(b);
                        ddlPerUpzila.SelectedValue = Converter.GetString(c);

                    }
                }

                ddlPerThana.SelectedIndex = 0;
                PerThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDistrict_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }


        protected void ddlPerThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode,ThanaOrgCode FROM A2ZTHANA Where ThanaOrgCode='" + ddlPerThana.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();
                        var ThanaOrg = dr1["ThanaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);
                        int d = Converter.GetInteger(ThanaOrg);

                        ddlPerDivision.SelectedValue = Converter.GetString(a);
                        ddlPerDistrict.SelectedValue = Converter.GetString(b);
                        ddlPerUpzila.SelectedValue = Converter.GetString(c);
                        ddlPerThana.SelectedValue = Converter.GetString(d);
                    }
                }

                PerDivisionDropdown();
                PerDistrictDropdown();
                PerUpzilaDropdown();
                PerThanaDropdown();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlThana_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        private void InvalidCUMSG()
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


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Future Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void BeforeDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Before CU Open Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Before CU Open Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        protected void txtOpenDate_TextChanged(object sender, EventArgs e)
        {
            if (txtOpenDate.Text != "")
            {
                DateTime opdate1 = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime opdate2 = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime opdate3 = DateTime.ParseExact(lblCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (opdate1 > opdate2)
                {
                    InvalidDateMSG();
                    if (BtnUpdate.Visible == false)
                    {
                        txtOpenDate.Text = ProcDate.Text;
                    }
                    else
                    {
                        txtOpenDate.Text = GetOpenDate.Text;
                    }

                    txtOpenDate.Focus();
                    return;
                }

                if (opdate1 < opdate3)
                {
                    BeforeDateMSG();
                    if (BtnUpdate.Visible == false)
                    {
                        txtOpenDate.Text = ProcDate.Text;
                    }
                    else
                    {
                        txtOpenDate.Text = GetOpenDate.Text;
                    }

                    txtOpenDate.Focus();
                    return;
                }
                txtDateOfBirth.Focus();
            }

        }

        protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime fDate = Converter.GetDateTime(dto.ProcessDate);

            //DateTime tDate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (txtDateOfBirth.Text != "")
            {

                DateTime tDate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int a1 = Convert.ToInt32(fDate.Subtract(tDate).TotalDays);
                int b1 = a1 / 365;
                txtAge.Text = Converter.GetString(b1);
            }

        }



    }
}
