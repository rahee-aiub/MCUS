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
namespace ATOZWEBMCUS.Pages
{
    public partial class CSPMemberMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OccupationDropdown();
                NationalityDropdown();
                NatureDropdown();
                ReligionDropdown();
                DivisionDropdown();
                PrimaryMemDropdown();
                txtPMemNo.Focus();
                DistrictInFo();
                ThanaInfo();
                pnlPermanent.Visible = false;
                pnlOtherInfo.Visible = false;
                BtnUpdate.Visible = false;
            }

        }
        private void PrimaryMemDropdown()
        {

            string sqlquery = "SELECT MemNo,MemName from A2ZMEMBER";
            ddlPMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlPMemNo, "A2ZCSMCUS");

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

        public void clearInfo()
        {

            txtPMemName.Text = string.Empty;
            //txtCULBMemNo.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtMotherName.Text = string.Empty;
            txtSpouseName.Text = string.Empty;
            txtOpenDate.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtPlaceOfBirth.Text = string.Empty;
            ddlOccupation.SelectedValue = "0";
            ddlNationality.SelectedValue = "0";
            ddlGender.SelectedValue = "0";
            ddlMemType.SelectedValue = "0";
            ddlReligion.SelectedValue = "0";
            ddlNature.SelectedValue = "0";
            ddlMaritalStatus.SelectedValue = "0";
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

        protected void txtPMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPMemNo.SelectedValue == "-Select-")
                {
                    txtPMemName.Focus();

                }

                if (txtPMemNo.Text != string.Empty)
                {
                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    Int16 CuType = Converter.GetSmallInteger(0);
                    int CreditNumber = Converter.GetInteger(0);
                    int MemNumber = Converter.GetInteger(txtPMemNo.Text);
                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CreditNumber, MemNumber));

                    if (getDTO.CreditUnionNo > 0 || getDTO.MemberNo > 0)
                    {

                        txtPMemName.Text = getDTO.MemberName;
                        ddlPMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        txtFatherName.Text = Converter.GetString(getDTO.FatherName);
                        txtMotherName.Text = Converter.GetString(getDTO.MotherName);
                        txtSpouseName.Text = Converter.GetString(getDTO.SpouseName);
                        DateTime dt = Converter.GetDateTime(getDTO.OpenDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        DateTime dt1 = Converter.GetDateTime(getDTO.DateOfBirth);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        txtDateOfBirth.Text = date1;
                        //txtOpenDate.Text = Converter.GetString(getDTO.OpenDate);
                        //txtDateOfBirth.Text = Converter.GetString(getDTO.DateOfBirth);
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
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.PerThana);
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
                        //txtPassportIssueDate.Text = Converter.GetString(getDTO.PassportIssueDate);
                        //txtPassportIssuePlace.Text = Converter.GetString(getDTO.PassportExpiryDate);
                        txtNationalID.Text = Converter.GetString(getDTO.NationalIdNo);
                        DateTime dt4 = Converter.GetDateTime(getDTO.PassportExpiryDate);
                        string date4 = dt4.ToString("dd/MM/yyyy");
                        txtPassportExpdate.Text = date4;
                        //txtPassportExpdate.Text = Converter.GetString(getDTO.PassportExpiryDate);
                        txtTIN.Text = Converter.GetString(getDTO.TIN);
                        DateTime dt3 = Converter.GetDateTime(getDTO.LastTaxPayDate);
                        string date3 = dt3.ToString("dd/MM/yyyy");
                        txtLastTaxPdate.Text = date3;
                        //txtLastTaxPdate.Text = Converter.GetString(getDTO.LastTaxPayDate);
                        txtPMemName.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        clearInfo();
                        txtPMemName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        ddlPMemNo.SelectedValue = "-Select-";
                        txtPMemName.Focus();
           
                    }



                }

            }


            catch (Exception ex)
            {
                throw ex;
            }




        }

        protected void ddlPMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMemNo.SelectedItem.Text == "-Select-")
            {
                txtPMemNo.Text = string.Empty;
                clearInfo();
                txtPMemNo.Focus();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
     
            }

            try
            {

                if (ddlPMemNo.SelectedValue != "-Select-")
                {
                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    Int16 CuType = Converter.GetSmallInteger(0);
                    int CreditNumber = Converter.GetInteger(0);
                    int MemNumber = Converter.GetInteger(ddlPMemNo.SelectedValue);
                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CreditNumber, MemNumber));

                    if (getDTO.CreditUnionNo > 0 || getDTO.MemberNo > 0)
                    {
                        txtPMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        txtPMemName.Text = getDTO.MemberName;
                        ddlPMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
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
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.PerDivision);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.PerDistrict);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.PerThana);
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
                        txtPMemName.Focus();
                    }
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = true;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "-Select-")
            {
                clearInfo();
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
                       
                    }
                    else
                    {
                        
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
            if (ddlDistrict.SelectedItem.Text == "-Select-")
            {

            }
            ThanaDropdown();
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZDISTRICTDTO getDTO = new A2ZDISTRICTDTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    getDTO = (A2ZDISTRICTDTO.GetInformation(code, distcode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0)
                    {
                       
                    }
                  


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlThana.SelectedValue != "-Select-")
                {
                    A2ZTHANADTO getDTO = new A2ZTHANADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int thanacode = Converter.GetInteger(ddlThana.SelectedValue);
                    getDTO = (A2ZTHANADTO.GetInformation(code, distcode, thanacode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.ThanaCode > 0)
                    {
                       
                    }
                   

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            A2ZMEMBERDTO objDTO = new A2ZMEMBERDTO();
            objDTO.MemberNo = Converter.GetInteger(txtPMemNo.Text);
            objDTO.MemberName = Converter.GetString(txtPMemName.Text);
            objDTO.FatherName = Converter.GetString(txtFatherName.Text);
            objDTO.MotherName = Converter.GetString(txtMotherName.Text);
            objDTO.SpouseName = Converter.GetString(txtSpouseName.Text);
            objDTO.Occupation = Converter.GetSmallInteger(ddlOccupation.SelectedValue);
            objDTO.Nationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
            objDTO.Gender = Converter.GetSmallInteger(ddlGender.SelectedValue);
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
            objDTO.PerDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            objDTO.PerDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            objDTO.PerThana = Converter.GetInteger(ddlThana.SelectedValue);
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
                objDTO.PassportIssueDate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }
            if (txtPassportExpdate.Text != string.Empty)
            {
                DateTime Expdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objDTO.PassportExpiryDate = Expdate;
            }
            else
            {
                objDTO.PassportExpiryDate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
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
                objDTO.LastTaxPayDate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }


            int roweffect = A2ZMEMBERDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                txtPMemNo.Text = string.Empty;
                clearInfo();
                txtPMemNo.Focus();
                PrimaryMemDropdown();
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZMEMBERDTO UpDTO = new A2ZMEMBERDTO();
            UpDTO.CreditUnionNo = Converter.GetInteger(0);
            UpDTO.MemberNo = Converter.GetInteger(txtPMemNo.Text);
            UpDTO.MemberName = Converter.GetString(txtPMemName.Text);
            UpDTO.FatherName = Converter.GetString(txtFatherName.Text);
            UpDTO.MotherName = Converter.GetString(txtMotherName.Text);
            UpDTO.SpouseName = Converter.GetString(txtSpouseName.Text);
            UpDTO.Occupation = Converter.GetSmallInteger(ddlOccupation.SelectedValue);
            UpDTO.Nationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
            UpDTO.Gender = Converter.GetSmallInteger(ddlGender.SelectedValue);
            UpDTO.Religion = Converter.GetSmallInteger(ddlReligion.SelectedValue);
            UpDTO.Nature = Converter.GetSmallInteger(ddlNature.SelectedValue);
            UpDTO.MaritalStatus = Converter.GetSmallInteger(ddlMaritalStatus.SelectedValue);
            DateTime opdate = Converter.GetDateTime(txtOpenDate.Text);
            UpDTO.OpenDate = opdate;
            DateTime Birthdate = Converter.GetDateTime(txtDateOfBirth.Text);
            UpDTO.DateOfBirth = Birthdate;
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
            UpDTO.PerDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.PerDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.PerThana = Converter.GetInteger(ddlThana.SelectedValue);
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
            DateTime Issuedate = Converter.GetDateTime(txtPassportIssueDate.Text);
            UpDTO.PassportIssueDate = Issuedate;
            DateTime Expdate = Converter.GetDateTime(txtPassportExpdate.Text);
            UpDTO.PassportExpiryDate = Expdate;
            UpDTO.PassportIssuePlace = Converter.GetString(txtPassportIssuePlace.Text);
            UpDTO.TIN = Converter.GetString(txtTIN.Text);
            DateTime LTpaydate = Converter.GetDateTime(txtLastTaxPdate.Text);
            UpDTO.LastTaxPayDate = LTpaydate;



            int roweffect = A2ZMEMBERDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                PrimaryMemDropdown();
                txtPMemNo.Focus();
                clearInfo();
                ddlPMemNo.SelectedValue = "-Select-";
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
                txtPMemNo.Text = string.Empty;

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
    }
}
