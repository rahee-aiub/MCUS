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
    public partial class CSInstantOpenMemberMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    //CtrlModule.Text = Request.QueryString["a%b"];

                    //if (CtrlModule.Text == "1")
                    //{
                    //    lblCuName.Visible = false;
                    //    CreditUnionDropdown();
                    //}

                    //if (CtrlModule.Text == "6")
                    //{
                    //    lblCuName.Visible = true;
                    //    ddlCreditUNo.Visible = false;
                    //    //ddlCULBMemNo.Visible = false;
                    //}


                    DivisionDropdown();
                    PerDivisionDropdown();

                   
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

                    txtCreditUNo.ReadOnly = true;

                    txtOpenDate.ReadOnly = true;

                    txtAge.ReadOnly = true;

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    //txtOpenDate.Text = date;
                    ProcDate.Text = date;

                    string PCtrl = (string)Session["ProgCtrl"];
                    ProgCtrl.Text = PCtrl;

                    string flag = (string)Session["flag"];
                    if(flag=="1")
                    {
                        string cutype = (string)Session["CuType"];
                        string cuno = (string)Session["CrNo"];
                        string TranDate = (string)Session["TrnDate"];
                        lblCUType.Text = cutype;
                        lblCUNumber.Text = cuno;
                        txtCreditUNo.Text = cutype+cuno;
                        txtCreditUNo_TextChanged(this, EventArgs.Empty);
                        //DateTime tdt = Converter.GetDateTime(TranDate);
                        //string tdate = tdt.ToString("dd/MM/yyyy");
                        txtOpenDate.Text = TranDate;

                       
                    }
                    ////
                    ////string cuname = (string)Session["Cuname"];
                    //lblCuName.Text = cuname;
                    //int lblAge = Convert.Int32(ProcDate.Text - txtDateOfBirth.Text);


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }
        protected void SessionRemove()
        {
            Session["NewMemNo"] = string.Empty;
            Session["NewMemName"] = string.Empty;

      

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

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {
                    int CN = Converter.GetInteger(txtCreditUNo.Text);



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
                        lblCUNumber.Text = Converter.GetString(getDTO.CreditUnionNo);

                        lblCrNo.Text = (lblCUType.Text + lblCUNumber.Text);
                        txtCreditUNo.Text = (lblCUType.Text + "-" + lblCUNumber.Text);

                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        lblCuOpenDate.Text = date;


                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + lblCUType.Text + "'";
                        
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        //txtCULBMemNo.Focus();
                        clearInfo();
                        //if (CtrlModule.Text == "1")
                        //{
                        //    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                        //    ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                        //}
                    }
                    else
                    {
                        InvalidCUMSG();

                        clearInfo();
                        txtCreditUNo.Focus();
                        txtCreditUNo.Text = string.Empty;
                        //txtCULBMemNo.Text = string.Empty;
                        txtCULBMemName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                       
                        
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCreditUNo_TextChanged Problem');</script>");
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

        private void MemlastRecord()
        {
            string strquery = "SELECT TOP(1)MemNo FROM A2ZMEMBER WHERE CuType='" + lblCUType.Text + "' and CuNo='" + lblCUNumber.Text + "'  ORDER BY Id DESC";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strquery, "A2ZCSMCUS");

            if (dt.Rows.Count > 0)
            {
                int mem = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                int lastMemNo = (mem + 1);
                lbllastMemno.Text = Converter.GetString(lastMemNo);
            }
        }

        protected void MemnoMSG()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string b = "Generated New Depositor No.";
            string c = string.Format(lbllastMemno.Text);

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append("\\n");
            sb.Append(b);
            sb.Append(c);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
               if ( ddlMemType.SelectedValue == "0")
                {
                    ddlMemType.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Depositor Type');", true);
                    return;

                }
                
                if ( txtCULBMemName.Text == string.Empty)
                {
                    txtCULBMemName.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor Name');", true);
                    return;

                }


                
                             
                MemlastRecord();
                A2ZMEMBERDTO objDTO = new A2ZMEMBERDTO();
                objDTO.CuType = Converter.GetSmallInteger(lblCUType.Text);
                objDTO.CreditUnionNo = Converter.GetInteger(lblCUNumber.Text);
                objDTO.MemberNo = Converter.GetInteger(lbllastMemno.Text);
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
                    //txtCULBMemNo.Text = string.Empty;
                  
                    //txtCULBMemNo.Focus();
                    //if (CtrlModule.Text == "1")
                    //{
                    //    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + lblCUNumber.Text + "'and CuType='" + lblCUType.Text + "' GROUP BY MemNo,MemName";
                    //    ddlCULBMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlCULBMemNo, "A2ZCSMCUS");
                    //}


                    InsertMiscRecord();


                    ddlDivision.SelectedValue = "-Select-";
                    ddlDistrict.SelectedValue = "-Select-";
                    ddlThana.SelectedValue = "-Select-";
                    ddlUpzila.SelectedValue = "-Select-";
                    ddlPerDivision.SelectedValue = "-Select-";
                    ddlPerDistrict.SelectedValue = "-Select-";
                    ddlPerThana.SelectedValue = "-Select-";
                    ddlPerUpzila.SelectedValue = "-Select-";
                    MemnoMSG();
                    

                    Session["CuType"] = lblCUType.Text;
                    Session["CrNo"] = lblCUNumber.Text;

                    Session["RCreditUNo"] = lblCrNo.Text;

                   
                    Session["NewMemNo"] = lbllastMemno.Text;
                    Session["RMemNo"] = lbllastMemno.Text;

                    Session["NewMemName"] = txtCULBMemName.Text;
                    Session["CtrlFlag"] = "1";

                    if (ProgCtrl.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                   "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
                    }

                    if (ProgCtrl.Text == "1")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                   "click", @"<script>window.opener.location.href='CSLoanApplication.aspx'; self.close();</script>", false);
                    }

                    //SessionRemove();
                    clearInfo();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                //throw ex;
            }

        }


        private void InsertMiscRecord()
        {

            var prm = new object[3];

            prm[0] = lblCUType.Text;
            prm[1] = lblCUNumber.Text;
            prm[2] = lbllastMemno.Text;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSInsertMiscellaneousAccount1", prm, "A2ZCSMCUS"));
            if (result == 0)
            {

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
            Session["CuType"] = lblCUType.Text;
            Session["CrNo"] = lblCUNumber.Text;
            Session["RCreditUNo"] = lblCrNo.Text;
            Session["CtrlFlag"] = "1";
            SessionRemove();

            if (ProgCtrl.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }
            if (ProgCtrl.Text == "1")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                  "click", @"<script>window.opener.location.href='CSLoanApplication.aspx'; self.close();</script>", false);
            }
           

           // Response.Redirect("A2ZERPModule.aspx");
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
                    
                    txtOpenDate.Text = ProcDate.Text;
                    

                    txtOpenDate.Focus();
                    return;
                }

                if (opdate1 < opdate3)
                {
                    BeforeDateMSG();
                    txtOpenDate.Text = ProcDate.Text;
                    

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