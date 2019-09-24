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
namespace ATOZWEBMCUS.Pages
{
    public partial class CSCMemberMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                ThanaInfo();
                PerDistrictInFo();
                PerThanaInfo();
                OccupationDropdown();
                NationalityDropdown();
                NatureDropdown();
                ReligionDropdown();

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtOpenDate.Text = date;

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
            ddlNationality = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNationality, "A2ZCSMCUS");

        }

        private void NatureDropdown()
        {

            string sqlquery = "SELECT NatureCode,NatureDescription from A2ZNATURE";
            ddlNature = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNature, "A2ZCSMCUS");

        }


        private void ReligionDropdown()
        {

            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZCSMCUS");

        }


        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZCSMCUS");

        }

        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' or DiviCode = '0'";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZCSMCUS");

        }

        private void ThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' or DistCode = '0'";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZCSMCUS");

        }

        private void PerDivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlPerDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPerDivision, "A2ZCSMCUS");

        }

        private void PerDistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlPerDivision.SelectedValue + "' or DiviCode = '0'";
            ddlPerDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerDistrict, "A2ZCSMCUS");

        }

        private void PerThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlPerDivision.SelectedValue + "' and DistCode='" + ddlPerDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPerThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerThana, "A2ZCSMCUS");

        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }

      

        public void clearInfo()
        {

            txtCULBMemName.Text = string.Empty;
            //txtCULBMemNo.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtMotherName.Text = string.Empty;
            txtSpouseName.Text = string.Empty;
            txtOpenDate.Text = string.Empty;
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
                    lblCUType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCUNumber.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    if (getDTO.CreditUnionNo > 0)
                    {
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
                        if (CtrlModule.Text == "1")
                        {
                            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                            ddlCreditUNo.SelectedValue = Converter.GetString(txtCreditUNo.Text);
                        }
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                        txtCreditUNo.Text = (c + "-" + d);
                        txtCULBMemNo.Focus();
                        clearInfo();
                        if (CtrlModule.Text == "1")
                        {
                            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                            ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                        }
                    }
                    else
                    {
                        clearInfo();
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
                throw ex;
            }
        }
        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
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
                    lblCUType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCUNumber.Text = Converter.GetString(CNo);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='"+CuType+"' GROUP BY MemNo,MemName";

                    ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = (c + "-" + d);
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
                throw ex;
            }
        }


        protected void ddlCULBMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCULBMemNo.SelectedItem.Text == "-Select-")
            {
                txtCULBMemNo.Text = string.Empty;
                clearInfo();
                txtCULBMemNo.Focus();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
      
            }

            try
            {

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
                        DateTime dt = Converter.GetDateTime(getDTO.OpenDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        DateTime dt1 = Converter.GetDateTime(getDTO.DateOfBirth);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        txtDateOfBirth.Text = date1;
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
                        ddlThana.SelectedValue = Converter.GetString(getDTO.preThana);
                        txtTelNo.Text = Converter.GetString(getDTO.PreTelephone);
                        txtMobileNo.Text = Converter.GetString(getDTO.PreMobile);
                        txtEmail.Text = Converter.GetString(getDTO.PreEmail);
                        txtPerAddL1.Text = Converter.GetString(getDTO.PerAddressLine1);
                        txtPerAddL2.Text = Converter.GetString(getDTO.PerAddressLine2);
                        txtPerAddL3.Text = Converter.GetString(getDTO.PerAddressLine3);
                        ddlPerDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
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
                        DateTime dt2 = Converter.GetDateTime(getDTO.PassportIssueDate);
                        string date2 = dt2.ToString("dd/MM/yyyy");
                        txtPassportIssueDate.Text = date2;
                        txtPassportIssuePlace.Text = Converter.GetString(getDTO.PassportIssuePlace);
                        txtNationalID.Text = Converter.GetString(getDTO.NationalIdNo);
                        DateTime dt4 = Converter.GetDateTime(getDTO.PassportExpiryDate);
                        string date4 = dt4.ToString("dd/MM/yyyy");
                        txtPassportExpdate.Text = date4;
                        txtTIN.Text = Converter.GetString(getDTO.TIN);
                        DateTime dt3 = Converter.GetDateTime(getDTO.LastTaxPayDate);
                        string date3 = dt3.ToString("dd/MM/yyyy");
                        txtLastTaxPdate.Text = date3;
                        txtCULBMemName.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        clearInfo();
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        ddlCULBMemNo.SelectedIndex = 0;
                        txtCULBMemName.Focus();
               
                    }


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void txtCULBMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
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

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        if (CtrlModule.Text == "1")
                        {
                            ddlCULBMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        }
                        txtCULBMemName.Text = getDTO.MemberName;
                        txtFatherName.Text = Converter.GetString(getDTO.FatherName);
                        txtMotherName.Text = Converter.GetString(getDTO.MotherName);
                        txtSpouseName.Text = Converter.GetString(getDTO.SpouseName);
                        DateTime dt = Converter.GetDateTime(getDTO.OpenDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        DateTime dt1 = Converter.GetDateTime(getDTO.DateOfBirth);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        txtDateOfBirth.Text = date1;
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
                        ddlThana.SelectedValue = Converter.GetString(getDTO.preThana);
                        txtTelNo.Text = Converter.GetString(getDTO.PreTelephone);
                        txtMobileNo.Text = Converter.GetString(getDTO.PreMobile);
                        txtEmail.Text = Converter.GetString(getDTO.PreEmail);
                        txtPerAddL1.Text = Converter.GetString(getDTO.PerAddressLine1);
                        txtPerAddL2.Text = Converter.GetString(getDTO.PerAddressLine2);
                        txtPerAddL3.Text = Converter.GetString(getDTO.PerAddressLine3);
                        ddlPerDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
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
                        DateTime dt2 = Converter.GetDateTime(getDTO.PassportIssueDate);
                        string date2 = dt2.ToString("dd/MM/yyyy");
                        txtPassportIssueDate.Text = date2;
                        txtPassportIssuePlace.Text = Converter.GetString(getDTO.PassportIssuePlace);
                        txtNationalID.Text = Converter.GetString(getDTO.NationalIdNo);
                        DateTime dt4 = Converter.GetDateTime(getDTO.PassportExpiryDate);
                        string date4 = dt4.ToString("dd/MM/yyyy");
                        txtPassportExpdate.Text = date4;
                        txtTIN.Text = Converter.GetString(getDTO.TIN);
                        DateTime dt3 = Converter.GetDateTime(getDTO.LastTaxPayDate);
                        string date3 = dt3.ToString("dd/MM/yyyy");
                        txtLastTaxPdate.Text = date3;
                        txtCULBMemName.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        clearInfo();
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtCULBMemName.Focus();
                        if (CtrlModule.Text == "1")
                        {
                            ddlCULBMemNo.SelectedIndex = 0;
                        }
                 
                    }




                }

            }


            catch (Exception ex)
            {
                throw ex;
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
            if (ddlDivision.SelectedValue == "-Select-")
            {
                clearInfo();
                //txtcode.Text = string.Empty;
                //txtDistcode.Focus();
                ddlDistrict.SelectedIndex = 0;
                return;
            }


            DistrictDropdown();

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
                    else
                    {
                          //ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ThanaDropdown();
        }

        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        

        private void DistrictInFo()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZCSMCUS");

        }

        private void ThanaInfo()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZCSMCUS");

        }

        private void PerDistrictInFo()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT";
            ddlPerDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerDistrict, "A2ZCSMCUS");

        }

        private void PerThanaInfo()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA";

            ddlPerThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerThana, "A2ZCSMCUS");

        }



        protected void BtnSubmit_Click(object sender, EventArgs e)
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
                objDTO.OpenDate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());

                //Nullable < DateTime > nullDateTime;
                //DateTime? nullDateTime;
                //objDTO.OpenDate = Converter.GetDateTime(nullDateTime);
                //System.DateTime? MyDateTime;
                //MyDateTime = null;
                //var defaultDateTimeValue = default(DateTime?);
                //objDTO.OpenDate = Converter.GetDateTime(defaultDateTimeValue);

            }
            if (txtDateOfBirth.Text != string.Empty)
            {
                DateTime datebirth = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objDTO.DateOfBirth = datebirth;
            }
            else
            {
                objDTO.DateOfBirth = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }
            objDTO.PlaceofBirth = Converter.GetString(txtPlaceOfBirth.Text);
            objDTO.PreAddressLine1 = Converter.GetString(txtAddressL1.Text);
            objDTO.PreAddressLine2 = Converter.GetString(txtAddressL2.Text);
            objDTO.PreAddressLine3 = Converter.GetString(txtAddressL3.Text);
            objDTO.PreDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            objDTO.PreDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            objDTO.preThana = Converter.GetInteger(ddlThana.SelectedValue);
            objDTO.PreTelephone = Converter.GetString(txtTelNo.Text);
            objDTO.PreMobile = Converter.GetString(txtMobileNo.Text);
            objDTO.PreEmail = Converter.GetString(txtEmail.Text);
            objDTO.PerAddressLine1 = Converter.GetString(txtPerAddL1.Text);
            objDTO.PerAddressLine2 = Converter.GetString(txtPerAddL2.Text);
            objDTO.PerAddressLine3 = Converter.GetString(txtPerAddL3.Text);
            objDTO.PerDivision = Converter.GetInteger(ddlPerDivision.SelectedValue);
            objDTO.PerDistrict = Converter.GetInteger(ddlPerDistrict.SelectedValue);
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
                objDTO.PassportIssueDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
            }
            if (txtPassportExpdate.Text != string.Empty)
            {
                DateTime Expdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objDTO.PassportExpiryDate = Expdate;
            }
            else
            {
                objDTO.PassportExpiryDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
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
                objDTO.LastTaxPayDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
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
                ddlPerDivision.SelectedValue = "-Select-";
                ddlPerDistrict.SelectedValue = "-Select-";
                ddlPerThana.SelectedValue = "-Select-";
            }
            //SqlDateTime sqldatenull;
            //string sqlStmt = "insert into A2ZMEMBER (MemOpenDate) Values () ";
            //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlStmt, "A2ZCSMCUS"));
            //SqlCommand cmd = new SqlCommand();
            //cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
            //sqldatenull = SqlDateTime.Null;
            //if(txtOpenDate.Text=="")
            //{
            //    cmd.Parameters["@Date"].Value = sqldatenull;
            //}

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
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
                UpDTO.OpenDate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }
            if (txtDateOfBirth.Text != string.Empty)
            {
                DateTime datebirth = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpDTO.DateOfBirth = datebirth;
            }
            else
            {
                UpDTO.DateOfBirth = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }
            UpDTO.PlaceofBirth = Converter.GetString(txtPlaceOfBirth.Text);
            UpDTO.PreAddressLine1 = Converter.GetString(txtAddressL1.Text);
            UpDTO.PreAddressLine2 = Converter.GetString(txtAddressL2.Text);
            UpDTO.PreAddressLine3 = Converter.GetString(txtAddressL3.Text);
            UpDTO.PreDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.PreDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.preThana = Converter.GetInteger(ddlThana.SelectedValue);
            UpDTO.PreTelephone = Converter.GetString(txtTelNo.Text);
            UpDTO.PreMobile = Converter.GetString(txtMobileNo.Text);
            UpDTO.PreEmail = Converter.GetString(txtEmail.Text);
            UpDTO.PerAddressLine1 = Converter.GetString(txtPerAddL1.Text);
            UpDTO.PerAddressLine2 = Converter.GetString(txtPerAddL2.Text);
            UpDTO.PerAddressLine3 = Converter.GetString(txtPerAddL3.Text);
            UpDTO.PerDivision = Converter.GetInteger(ddlPerDivision.SelectedValue);
            UpDTO.PerDistrict = Converter.GetInteger(ddlPerDistrict.SelectedValue);
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
                UpDTO.PassportIssueDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
            }
            if (txtPassportExpdate.Text != string.Empty)
            {
                DateTime Expdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpDTO.PassportExpiryDate = Expdate;
            }
            else
            {
                UpDTO.PassportExpiryDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
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
                UpDTO.LastTaxPayDate = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
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
                txtCULBMemNo.Focus();
                clearInfo();
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
    
            }
            else
            {
                clearInfo();
             }

        }

        protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int a = Convert.ToInt16(dt.Subtract(dt1).TotalDays);
            int b = a / 365;
            txtAge.Text = Converter.GetString(b);
        }

        protected void ddlOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOccupation.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlOccupation.SelectedIndex = 0;
                return;
            }

            try
            {

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
                throw ex;
            }


        }

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNationality.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlNationality.SelectedIndex = 0;
                return;
            }

            try
            {

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
                throw ex;
            }

        }

        protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNature.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlNature.SelectedIndex = 0;
                return;
            }

            try
            {

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
                throw ex;
            }

        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReligion.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlReligion.SelectedIndex = 0;
                return;
            }

            try
            {

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
                throw ex;
            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlPerDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPerDivision.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlPerDistrict.SelectedIndex = 0;
                return;
            }


            PerDistrictDropdown();

         
        }

        protected void ddlPerDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
               
            PerThanaDropdown();
        
        }

        protected void ddlPerThana_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }





    }
}
