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
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSCollectorMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DivisionDropdown();
                CollectorDropdown();
                txtNo.Focus();
                BtnUpdate.Visible = false;
                DistrictInFo();
                ThanaInfo();
            }
        }

        private void CollectorDropdown()
        {

            string sqlquery = "SELECT COLNO,COLNAME from A2ZCOLLECTOR";
            ddlCollectorNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCollectorNo, "A2ZCSMCUS");

        }
        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZCSMCUS");

        }
        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' or DiciCode = '0'";
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
            txtName.Text = string.Empty;
            txtOpenDate.Text = string.Empty;
            txtNationalIDNo.Text = string.Empty;
            txtAddressL1.Text = string.Empty;
            txtAddressL2.Text = string.Empty;
            txtAddressL3.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtStatNote.Text = string.Empty;
            txtStatDate.Text = string.Empty;
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
            if (ddlThana.SelectedItem.Text == "-Select-")
            {
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
                        
                    }
                    


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtNo.Text != string.Empty)
                {
                    int CNo = Converter.GetInteger(txtNo.Text);
                    A2ZCOLLECTORDTO getDTO = (A2ZCOLLECTORDTO.GetInformation(CNo));

                    if (getDTO.CollectorNo > 0)
                    {
                        txtNo.Text = Converter.GetString(getDTO.CollectorNo);
                        txtName.Text = Converter.GetString(getDTO.CollectorName);
                        ddlCollectorNo.SelectedValue = Converter.GetString(getDTO.CollectorNo);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        txtNationalIDNo.Text = Converter.GetString(getDTO.NationalIdNo);
                        txtAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtFax.Text = Converter.GetString(getDTO.Fax);
                        txtEmail.Text = Converter.GetString(getDTO.email);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                        txtStatus.Text = Converter.GetString(getDTO.Status);
                        DateTime dt1 = Converter.GetDateTime(getDTO.Statusdate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        txtStatDate.Text = date1;
                        txtStatNote.Text = Converter.GetString(getDTO.StatusNote);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtName.Focus();
                    }
                    else
                    {
                        clearInfo();
                        ddlCollectorNo.SelectedValue = "-Select-";
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtName.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCollectorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCollectorNo.SelectedValue == "-Select-")
            {
                clearInfo();
                txtNo.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtNo.Focus();
            }

            try
            {


                if (ddlCollectorNo.SelectedValue != "-Select-")
                {

                    int CNo = Converter.GetInteger(ddlCollectorNo.SelectedValue);
                    A2ZCOLLECTORDTO getDTO = (A2ZCOLLECTORDTO.GetInformation(CNo));
                    if (getDTO.CollectorNo > 0)
                    {
                        txtNo.Text = Converter.GetString(getDTO.CollectorNo);
                        txtName.Text = Converter.GetString(getDTO.CollectorName);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = date;
                        txtNationalIDNo.Text = Converter.GetString(getDTO.NationalIdNo);
                        txtAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtFax.Text = Converter.GetString(getDTO.Fax);
                        txtEmail.Text = Converter.GetString(getDTO.email);
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                        txtStatus.Text = Converter.GetString(getDTO.Status);
                        DateTime dt1 = Converter.GetDateTime(getDTO.Statusdate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        txtStatDate.Text = date1;
                        txtStatNote.Text = Converter.GetString(getDTO.StatusNote);

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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCOLLECTORDTO objDTO = new A2ZCOLLECTORDTO();
                objDTO.CollectorNo = Converter.GetInteger(txtNo.Text);
                objDTO.CollectorName = Converter.GetString(txtName.Text);
                //DateTime date = DateTime.ParseExact(txtCuOpenDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (txtOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.opendate = opdate;
                }
                else
                {
                    objDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
                }
                objDTO.NationalIdNo = Converter.GetInteger(txtNationalIDNo.Text);
                objDTO.AddressL1 = Converter.GetString(txtAddressL1.Text);
                objDTO.AddressL2 = Converter.GetString(txtAddressL2.Text);
                objDTO.AddressL3 = Converter.GetString(txtAddressL3.Text);
                objDTO.TelephoneNo = Converter.GetString(txtTelNo.Text);
                objDTO.MobileNo = Converter.GetString(txtMobileNo.Text);
                objDTO.Fax = Converter.GetString(txtFax.Text);
                objDTO.email = Converter.GetString(txtEmail.Text);
                objDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
                objDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
                objDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);
                objDTO.Status = Converter.GetSmallInteger(txtStatus.Text);
                objDTO.StatusNote = Converter.GetString(txtStatNote.Text);
                if (txtStatDate.Text != string.Empty)
                {
                    DateTime stdate = DateTime.ParseExact(txtStatDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.Statusdate = stdate;
                }
                else
                {
                    objDTO.Statusdate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
                }
                int roweffect = A2ZCOLLECTORDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    CollectorDropdown();
                    txtNo.Text = string.Empty;
                    clearInfo();
                    txtNo.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZCOLLECTORDTO UpDTO = new A2ZCOLLECTORDTO();
            UpDTO.CollectorNo = Converter.GetInteger(txtNo.Text);
            UpDTO.CollectorName = Converter.GetString(txtName.Text);
            UpDTO.NationalIdNo = Converter.GetInteger(txtNationalIDNo.Text);
            UpDTO.AddressL1 = Converter.GetString(txtAddressL1.Text);
            UpDTO.AddressL2 = Converter.GetString(txtAddressL2.Text);
            UpDTO.AddressL3 = Converter.GetString(txtAddressL3.Text);
            UpDTO.TelephoneNo = Converter.GetString(txtTelNo.Text);
            UpDTO.MobileNo = Converter.GetString(txtMobileNo.Text);
            UpDTO.Fax = Converter.GetString(txtFax.Text);
            UpDTO.email = Converter.GetString(txtEmail.Text);
            UpDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);
            UpDTO.Status = Converter.GetSmallInteger(txtStatus.Text);
            DateTime date = DateTime.ParseExact(txtStatDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            UpDTO.Statusdate = date;
            int roweffect = A2ZCOLLECTORDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                CollectorDropdown();
                clearInfo();
                ddlCollectorNo.SelectedValue = "-Select-";
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
                txtNo.Focus();

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}
