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
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;
using ATOZWEBMCUS.WebSessionStore;


namespace ATOZWEBMCUS.Pages
{
    public partial class CSCreditUnionMaintenance : System.Web.UI.Page
    {

        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                if (userPermission == 30)
                {
                    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                       "&txtTwo=" + "You Don't Have Permission for Approved" +
                                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    Server.Transfer("Notify.aspx" + notifyMsg);
                }

                else
                {
                    DivisionDropdown();
                    CreditUnionDropdown();
                    txtCreditUNo.Focus();
                    BtnCreUnionUpdate.Visible = false;
                    DistrictInFo();
                    ThanaInfo();
                }
            }

        }

        protected void BtnCreUnionSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCUNIONDTO objDTO = new A2ZCUNIONDTO();
                A2ZMEMBERDTO MemDTO = new A2ZMEMBERDTO();
                MemDTO.CreditUnionNo = Converter.GetInteger(txtCreditUNo.Text);
                MemDTO.MemberName = Converter.GetString(txtCreUName.Text);
                MemDTO.MemType = 1;
                objDTO.CreditUnionNo = Converter.GetInteger(txtCreditUNo.Text);
                objDTO.CreditUnionName = Converter.GetString(txtCreUName.Text);
                //DateTime date = DateTime.ParseExact(txtCuOpenDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (txtCuOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.opendate = opdate;
                    MemDTO.OpenDate = opdate;
                }
                else
                {
                    objDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
                }
                //objDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());


                objDTO.MemberFlag = Converter.GetSmallInteger(ddlCuMemberFlag.SelectedValue);
                objDTO.MemberType = Converter.GetSmallInteger(ddlCuMemberType.SelectedValue);
                objDTO.CertificateNo = Converter.GetInteger(txtCuCertificateNo.Text);
                objDTO.AddressL1 = Converter.GetString(txtCuAddressL1.Text);
                objDTO.AddressL2 = Converter.GetString(txtCuAddressL2.Text);
                objDTO.AddressL3 = Converter.GetString(txtCuAddressL3.Text);
                objDTO.TelephoneNo = Converter.GetString(txtCuTelNo.Text);
                objDTO.MobileNo = Converter.GetString(txtCuMobileNo.Text);
                objDTO.Fax = Converter.GetString(txtCuFax.Text);
                objDTO.email = Converter.GetString(txtCuEmail.Text);
                objDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
                objDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
                objDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);
                int row = A2ZMEMBERDTO.Insert(MemDTO);
                int roweffect = A2ZCUNIONDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    CreditUnionDropdown();
                    txtCreditUNo.Text = string.Empty;
                    ddlCuMemberFlag.SelectedValue = "0";
                    ddlCuMemberType.SelectedValue = "0";
                    clearInfo();
                    txtCreditUNo.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT CUNO,CUNAME from A2ZCUNION";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

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
        public void clearInfo()
        {
            //txtCreditUNo.Text = string.Empty;
            txtCreUName.Text = string.Empty;
            txtCuOpenDate.Text = string.Empty;
            txtCuCertificateNo.Text = string.Empty;
            txtCuAddressL1.Text = string.Empty;
            txtCuAddressL2.Text = string.Empty;
            txtCuAddressL3.Text = string.Empty;
            txtCuEmail.Text = string.Empty;
            txtCuFax.Text = string.Empty;
            txtCuTelNo.Text = string.Empty;
            txtCuMobileNo.Text = string.Empty;
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
        protected void BtnCreUnionUpdate_Click(object sender, EventArgs e)
        {
            A2ZCUNIONDTO UpDTO = new A2ZCUNIONDTO();
            A2ZMEMBERDTO MemDTO = new A2ZMEMBERDTO();
            MemDTO.CreditUnionNo = Converter.GetInteger(txtCreditUNo.Text);
            MemDTO.MemberName = Converter.GetString(txtCreUName.Text);
            MemDTO.MemType = 1;
            UpDTO.CreditUnionNo = Converter.GetInteger(txtCreditUNo.Text);
            UpDTO.CreditUnionName = Converter.GetString(txtCreUName.Text);

            if (txtCuOpenDate.Text != string.Empty)
            {
                DateTime opdate = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpDTO.opendate = opdate;
                MemDTO.OpenDate = opdate;
            }
            else
            {
                UpDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }



            UpDTO.MemberFlag = Converter.GetSmallInteger(ddlCuMemberFlag.SelectedValue);
            UpDTO.MemberType = Converter.GetSmallInteger(ddlCuMemberType.SelectedValue);
            UpDTO.CertificateNo = Converter.GetInteger(txtCuCertificateNo.Text);
            UpDTO.AddressL1 = Converter.GetString(txtCuAddressL1.Text);
            UpDTO.AddressL2 = Converter.GetString(txtCuAddressL2.Text);
            UpDTO.AddressL3 = Converter.GetString(txtCuAddressL3.Text);
            UpDTO.TelephoneNo = Converter.GetString(txtCuTelNo.Text);
            UpDTO.MobileNo = Converter.GetString(txtCuMobileNo.Text);
            UpDTO.Fax = Converter.GetString(txtCuFax.Text);
            UpDTO.email = Converter.GetString(txtCuEmail.Text);
            UpDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);

            int row = A2ZMEMBERDTO.Update(MemDTO);
            int roweffect = A2ZCUNIONDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                CreditUnionDropdown();
                clearInfo();
                txtCreditUNo.Text = string.Empty;
                ddlCuMemberFlag.SelectedValue = "0";
                ddlCuMemberType.SelectedValue = "0";
                ddlCreditUNo.SelectedValue = "-Select-";
                BtnCreUnionUpdate.Visible = false;
                BtnCreUnionSubmit.Visible = true;
                txtCreditUNo.Focus();

            }
        }

        public void ShowRecords()
        {
            int CNo = Converter.GetInteger(txtCreditUNo.Text);
            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CNo));

            if (getDTO.CreditUnionNo > 0)
            {
                txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
                DateTime dt = Converter.GetDateTime(getDTO.opendate);
                string date = dt.ToString("dd/MM/yyyy");
                txtCuOpenDate.Text = date;
                //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);
                ddlCuMemberType.SelectedValue = Converter.GetString(getDTO.MemberType);
                txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                txtCuFax.Text = Converter.GetString(getDTO.Fax);
                txtCuEmail.Text = Converter.GetString(getDTO.email);
                ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                BtnCreUnionSubmit.Visible = false;
                BtnCreUnionUpdate.Visible = true;

                txtCreUName.Focus();
            }
            else
            {
                clearInfo();
                //txtCreUName.Text = string.Empty;
                ddlCreditUNo.SelectedValue = "-Select-";
                ddlCuMemberFlag.SelectedValue = "0";
                ddlCuMemberType.SelectedValue = "0";
                BtnCreUnionSubmit.Visible = true;
                BtnCreUnionUpdate.Visible = false;
                txtCreUName.Focus();

            }

        }



        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {
                    int CNo = Converter.GetInteger(txtCreditUNo.Text);
                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                        ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtCuOpenDate.Text = date;
                        //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                        ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);
                        ddlCuMemberType.SelectedValue = Converter.GetString(getDTO.MemberType);
                        txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                        txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtCuFax.Text = Converter.GetString(getDTO.Fax);
                        txtCuEmail.Text = Converter.GetString(getDTO.email);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                        BtnCreUnionSubmit.Visible = false;
                        BtnCreUnionUpdate.Visible = true;

                        txtCreUName.Focus();
                    }
                    else
                    {
                        clearInfo();
                        //txtCreUName.Text = string.Empty;
                        ddlCreditUNo.SelectedValue = "-Select-";
                        ddlCuMemberFlag.SelectedValue = "0";
                        ddlCuMemberType.SelectedValue = "0";
                        BtnCreUnionSubmit.Visible = true;
                        BtnCreUnionUpdate.Visible = false;
                        txtCreUName.Focus();

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
                ddlCuMemberFlag.SelectedValue = "0";
                ddlCuMemberType.SelectedValue = "0";
                BtnCreUnionSubmit.Visible = true;
                BtnCreUnionUpdate.Visible = false;
                txtCreditUNo.Focus();
            }

            try
            {


                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    int CNo = Converter.GetInteger(ddlCreditUNo.SelectedValue);
                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                        ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtCuOpenDate.Text = date;
                        //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                        ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);
                        ddlCuMemberType.SelectedValue = Converter.GetString(getDTO.MemberType);
                        txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                        txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtCuFax.Text = Converter.GetString(getDTO.Fax);
                        txtCuEmail.Text = Converter.GetString(getDTO.email);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                        BtnCreUnionSubmit.Visible = false;
                        BtnCreUnionUpdate.Visible = true;

                        txtCreUName.Focus();
                    }
                    else
                    {
                        clearInfo();
                        //txtCreUName.Text = string.Empty;
                        ddlCreditUNo.SelectedValue = "-Select-";
                        ddlCuMemberFlag.SelectedValue = "0";
                        ddlCuMemberType.SelectedValue = "0";
                        BtnCreUnionSubmit.Visible = true;
                        BtnCreUnionUpdate.Visible = false;
                        txtCreUName.Focus();

                    }
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
                        ddlDistrict.SelectedIndex = 0;
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
                //clearInfo();
                //txtDistcode.Focus();
                //BtnSubmit.Visible = true;
                //BtnUpdate.Visible = false;
                //txtDistcode.Text = string.Empty;
                return;
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
                        //txtDistcode.Text = Converter.GetString(getDTO.DistrictCode);
                        //txtDistDescription.Text = Converter.GetString(getDTO.DistrictDescription);
                        //txtDistDescription.Focus();
                    }
                    //BtnSubmit.Visible = false;
                    //BtnUpdate.Visible = true;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlThana.SelectedItem.Text == "-Select-")
            {
                //clearInfo();
                //txtThanacode.Focus();
                //BtnSubmit.Visible = true;
                //BtnUpdate.Visible = false;
                return;
            }

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
                        //txtThanacode.Text = Converter.GetString(getDTO.ThanaCode);
                        //txtThanaDescription.Text = Converter.GetString(getDTO.ThanaDescription);
                        //txtThanaDescription.Focus();
                    }
                    //BtnSubmit.Visible = false;
                    //BtnUpdate.Visible = true;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtCuOpenDate_TextChanged(object sender, EventArgs e)
        {
            ddlCuMemberFlag.Focus();
        }
    }
}
