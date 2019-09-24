using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using DataAccessLayer.Constants;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;
using System.IO;
using DataAccessLayer.Utility;
using ATOZWEBMCUS.WebSessionStore;
using System.Web.UI.WebControls;
using System.Web;
using System.Data.SqlClient;
using DataAccessLayer.BLL;


namespace ATOZWEBMCUS.Pages
{
    public partial class HREmployeeMaintenance : System.Web.UI.Page
    {
        string fullPath = String.Empty;
        string fileName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txt = new TextBox();

            try
            {
                if (IsPostBack)
                {
                    if (rbUpdate.Checked && chkAddress.Checked)
                    {
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        btnReport.Visible = true;
                        btnExit.Visible = true;
                        DivAddress.Visible = true;

                    }

                    lblMessage.Text = string.Empty;
                    DivMessage.Visible = false;
                }
                else
                {

                    CodeFile();
                    ButtonVisibilityAdd();
                    DivAddress.Visible = false;
                    ddlEmpcode.Visible = false;

                    ddlEmpcode = A2ZEMPLOYEEDTO.GetDropDownList(ddlEmpcode);
                    txtEmpCode.Text = A2ZEMPLOYEEDTO.GetLastSerialNo().ToString();
                    ddlArea = A2ZAREADTO.GetDropDownList(ddlArea);
                    ddlGrade = A2ZGRADEDTO.GetDropDownList(ddlGrade);
                    ddlMarried.SelectedValue = Converter.GetString(2);
                    ibtnUpload.Visible = false;

                    ddlDepartment.Items.Add("-Select-");
                    ddlSection.Items.Add("-Select-");



                }
                txtEmpCode.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddress.Checked)
            {
                DivAddress.Visible = true;
            }
            else
            {
                DivAddress.Visible = false;
            }
        }

        public void CodeFile()
        {
            try
            {
                PerDivisionDropdown();

                PreDivisionDropdown();
                PreDistrictInFo();
                PreThanaInfo();
                PerDistrictInFo();
                PerThanaInfo();
                ReligionDropdown();
                Designdropdown();
                ddlLocation = A2ZLOCATIONDTO.GetDropDownList(ddlLocation);                        
                ddlServiceType = A2ZSERVICETYPEDTO.GetDropDownList(ddlServiceType);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Designdropdown()
        {
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlDesignation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDesignation, "A2ZHRMCUS");
        }

        private void PerDivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlPermanentDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPermanentDivision, "A2ZCSMCUS");

        }

        private void PerDistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlPermanentDivision.SelectedValue + "' or DiviCode = '0'";
            ddlPermanentDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPermanentDistrict, "A2ZCSMCUS");

        }

        private void PerThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlPermanentDivision.SelectedValue + "' and DistCode='" + ddlPermanentDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPermanentThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPermanentThana, "A2ZCSMCUS");

        }

        private void PreDivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlPresentDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPresentDivision, "A2ZCSMCUS");

        }

        private void PreDistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlPresentDivision.SelectedValue + "' or DiviCode = '0'";
            ddlPresentDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPresentDistrict, "A2ZCSMCUS");

        }

        private void PreThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlPresentDivision.SelectedValue + "' and DistCode='" + ddlPresentDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPresentThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPresentThana, "A2ZCSMCUS");

        }
        private void PreDistrictInFo()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT";
            ddlPresentDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPresentDistrict, "A2ZCSMCUS");

        }

        private void PreThanaInfo()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA";

            ddlPresentThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPresentThana, "A2ZCSMCUS");

        }

        private void PerDistrictInFo()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT";
            ddlPermanentDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPermanentDistrict, "A2ZCSMCUS");

        }

        private void PerThanaInfo()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA";

            ddlPermanentThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPermanentThana, "A2ZCSMCUS");

        }

        private void ReligionDropdown()
        {

            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZCSMCUS");

        }

        private void ButtonVisibilityAdd()
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            txtEmployeMody.Visible = false;
            btnReport.Visible = false;
            btnExit.Visible = true;
            txtEmployeMody.Visible = false;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int noOfRowEffected;

            string[] ddmmyyyy;

            try
            {

                //if(FileUpload1.HasFile==false)
                //{
                //    String csname1 = "PopupScript";
                //    Type cstype = GetType();
                //    ClientScriptManager cs = Page.ClientScript;

                //    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //    {
                //        String cstext1 = "alert('Picture Should Not Be Empty');";
                //        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //    }

                //    ImgPicture.ImageUrl = "~/Images/index.jpg";
                //    return;
                //}

                A2ZEMPLOYEEDTO employeeObj = new A2ZEMPLOYEEDTO();

                employeeObj.EmpName = Converter.GetString(txtEmpName.Text);
                employeeObj.EmpFatherName = Converter.GetString(txtFatherName.Text);
                employeeObj.EmpMotherName = Converter.GetString(txtMotherName.Text);
                employeeObj.EmpSpouseName = Converter.GetString(txtSpouseName.Text);
                ddmmyyyy = txtDOB.Text.Split('/');
                employeeObj.EmpDateOfBirth = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);
                ddmmyyyy = txtJoinDate.Text.Split('/');
                employeeObj.EmpDateOfJoin = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);
                employeeObj.EmpFlag = 0;
                employeeObj.EmpType = 0;
                employeeObj.EmpStatus = 0;
                employeeObj.EmpStatusRef = "0";
                employeeObj.ReligionCode = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                employeeObj.ServiceType = Converter.GetSmallInteger(ddlServiceType.SelectedValue);
                employeeObj.LocationCode = Converter.GetSmallInteger(ddlLocation.SelectedValue);
                employeeObj.DepartmentCode = Converter.GetSmallInteger(ddlDepartment.SelectedValue);
                employeeObj.SectionCode = Converter.GetSmallInteger(ddlSection.SelectedValue);
                employeeObj.DesigCode = Converter.GetSmallInteger(ddlDesignation.SelectedValue);
                employeeObj.ClassCode = 0;

                if (ddlLocation.SelectedValue == "1" || ddlLocation.SelectedValue == "2")
                {
                    employeeObj.AreaCode = 0;
                }
                else
                {
                    employeeObj.AreaCode = Converter.GetSmallInteger(ddlArea.SelectedValue);
                }
                employeeObj.EmpIncrementDate = DateTime.Now;
                employeeObj.EmpIncrementAmt = 0;
                employeeObj.EmpBank = "0";
                employeeObj.EmpNationalID = Converter.GetString(txtNid.Text);
                employeeObj.EmpBloodGroup = Converter.GetString(txtBloodGroup.Text);
                employeeObj.EmpGrade = Converter.GetString(ddlGrade.SelectedItem.Text);
                employeeObj.EmpBankAccount = "0";
                employeeObj.EmpPFYN = 0;
                employeeObj.EmpAITYN = 0;
                employeeObj.EmpHoldAmount = 0;
                employeeObj.EmpGrossSalary = 0;
                employeeObj.EmpBasicPercent = 0;
                employeeObj.EmpBasicAmt = 0;
                employeeObj.EmpBasicAllowanceAmt = 0;
                employeeObj.EmpPFPercent = 0;
                employeeObj.EmpPFAmt = 0;
                employeeObj.EmpAITPercent = 0;
                employeeObj.EmpAITAmt = 0;
                employeeObj.LoanID = 0;
                employeeObj.LoanAmount = 0;
                employeeObj.LoanStartDate = DateTime.Now;
                employeeObj.LoanEndDate = DateTime.Now;
                employeeObj.LoanReturnAmt = 0;

                employeeObj.EmpGender = Converter.GetSmallInteger(ddlGender.SelectedValue);

                if (txtSpouseName.Text == string.Empty)
                {
                    // 2 Unmarried
                    employeeObj.EmpMarried = Converter.GetSmallInteger(2);

                }
                else
                {
                    // 1 Married
                    employeeObj.EmpMarried = Converter.GetSmallInteger(1);
                }
                employeeObj.EmpPrintSrl = Converter.GetSmallInteger(txtPrintSlNo.Text);
                employeeObj.UserId = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                employeeObj.CreateDate = DateTime.Now;


                employeeObj.EmpPicUrl = fileName;
                employeeObj.EmpQualification = txtQualification.Text;

                // Start - Find Employee ID
                string str = "SELECT * FROM A2ZEMPLOYEE WHERE EmployeeID='" + txtEmpFileNo.Text + "'";
                int queryResult = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(str, "A2ZHRMCUS"));
                if (queryResult > 0)
                {
                    ShowMessage("This Employee ID is already exist ", Color.Red);
                    txtEmpFileNo.Text = string.Empty;
                }
                else
                {
                    employeeObj.EmployeeID = txtEmpFileNo.Text;
                }

                // End - Find Employee ID

                {
                    if (chkAddress.Checked)
                    {
                        if (txtConfrimDate.Text == string.Empty && txtPromotionDate.Text != string.Empty)
                        {

                            ddmmyyyy = txtPromotionDate.Text.Split('/');
                            employeeObj.EmpPromotionDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                            noOfRowEffected = A2ZEMPLOYEEDTO.InsertEmployeeInformationConfermdate(employeeObj);
                            if (noOfRowEffected > 0)
                            {
                                if (FileUpload1.HasFile)
                                {
                                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                                    {


                                        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                        SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                        cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        con.Open();
                                        int result = cmd.ExecuteNonQuery();

                                        if (result == 1)
                                        {
                                            string filename = FileUpload1.FileName;
                                            FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                                            ImgPicture.ImageUrl = "~/Profile Pic/" + filename;

                                        }
                                    }

                                }
                                else
                                {
                                    SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                    SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                    cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    con.Open();
                                    int result = cmd.ExecuteNonQuery();
                                }
                                ClearInformationForSave();

                            }
                        }

                        else if (txtPromotionDate.Text == string.Empty && txtConfrimDate.Text != string.Empty)
                        {
                            ddmmyyyy = txtConfrimDate.Text.Split('/');
                            employeeObj.EmpConfirmDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                            noOfRowEffected = A2ZEMPLOYEEDTO.InsertEmployeeInformationLastPdate(employeeObj);
                            if (noOfRowEffected > 0)
                            {
                                if (FileUpload1.HasFile)
                                {
                                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                                    {


                                        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                        SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                        cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        con.Open();
                                        int result = cmd.ExecuteNonQuery();

                                        if (result == 1)
                                        {
                                            string filename = FileUpload1.FileName;
                                            FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                                            ImgPicture.ImageUrl = "~/Profile Pic/" + filename;

                                        }
                                    }

                                }
                                else
                                {
                                    SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                    SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                    cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    con.Open();
                                    int result = cmd.ExecuteNonQuery();
                                }

                                ClearInformationForSave();

                            }
                        }
                        else if (txtPromotionDate.Text == string.Empty && txtPromotionDate.Text == string.Empty)
                        {

                            noOfRowEffected = A2ZEMPLOYEEDTO.InsertEmployeeInformationLastPdateCondate(employeeObj);
                            if (noOfRowEffected > 0)
                            {
                                if (FileUpload1.HasFile)
                                {
                                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                                    {


                                        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                        SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                        cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        con.Open();
                                        int result = cmd.ExecuteNonQuery();

                                        if (result == 1)
                                        {
                                            string filename = FileUpload1.FileName;
                                            FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                                            ImgPicture.ImageUrl = "~/Profile Pic/" + filename;

                                        }
                                    }

                                }
                                else
                                {
                                    SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                    SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                    cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    con.Open();
                                    int result = cmd.ExecuteNonQuery();
                                }


                                ClearInformationForSave();

                            }
                        }

                        else
                        {
                            ddmmyyyy = txtConfrimDate.Text.Split('/');
                            employeeObj.EmpConfirmDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                            ddmmyyyy = txtPromotionDate.Text.Split('/');
                            employeeObj.EmpPromotionDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);


                            noOfRowEffected = A2ZEMPLOYEEDTO.InsertEmployeeInformation(employeeObj);
                            if (noOfRowEffected > 0)
                            {
                                if (FileUpload1.HasFile)
                                {
                                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                                    {
                                        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                        SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                        cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        con.Open();
                                        int result = cmd.ExecuteNonQuery();
                                        if (result == 1)
                                        {
                                            string filename = FileUpload1.FileName;
                                            FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                                            ImgPicture.ImageUrl = "~/Profile Pic/" + filename;

                                        }
                                    }

                                }
                                else
                                {
                                    SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                                    SqlCommand cmd = new SqlCommand("SP_A2ZERPINSERTIMAGE", con);
                                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                                    cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text);

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    con.Open();
                                    int result = cmd.ExecuteNonQuery();
                                }
                                ClearInformationForSave();


                            }
                            else
                            {
                                ShowMessage("Sorry!!Data has not been saved.", Color.Red);
                            }

                        }

                        A2ZEMPADDRESSDTO addressObj = new A2ZEMPADDRESSDTO();

                        addressObj.EmpCode = Converter.GetSmallInteger(txtEmpCode.Text);
                        addressObj.EmpPermanentAddL1 = txtPermanentAdd1.Text.Trim();
                        addressObj.EmpPermanentAddL2 = txtPermanentAdd2.Text.Trim();
                        addressObj.EmpPermanentDiviCode = Converter.GetSmallInteger(ddlPermanentDivision.SelectedValue);
                        addressObj.EmpPermanentDistCode = Converter.GetSmallInteger(ddlPermanentDistrict.SelectedValue);
                        addressObj.EmpPermanentThanaCode = Converter.GetSmallInteger(ddlPermanentThana.SelectedValue);
                        addressObj.EmpPresentAddL1 = txtPresentAdd1.Text.Trim();
                        addressObj.EmpPresentAddL2 = txtPresentAdd2.Text.Trim();
                        addressObj.EmpPresentDiviCode = Converter.GetSmallInteger(ddlPresentDivision.SelectedValue);
                        addressObj.EmpPresentDistCode = Converter.GetSmallInteger(ddlPresentDistrict.SelectedValue);
                        addressObj.EmpPresentThanaCode = Converter.GetSmallInteger(ddlPresentThana.SelectedValue);
                        addressObj.EmpMobileNo = txtMobile.Text.Trim();
                        addressObj.EmpPhoneNo = txtPhone.Text.Trim();
                        addressObj.EmpEmail = txtEmail.Text.Trim();
                        addressObj.EmpPermanentPostCode = Converter.GetSmallInteger(txtEmpPermanentPostCode.Text);
                        addressObj.EmpPresentPostCode = Converter.GetSmallInteger(txtEmpPresentPostCode.Text);
                        addressObj.EmpReference1Name = Converter.GetString(txtEmpReference1Name.Text);
                        addressObj.EmpReference1AddL1 = Converter.GetString(txtEmpReference1AddL1.Text);
                        addressObj.EmpReference1AddL2 = Converter.GetString(txtEmpReference1AddL2.Text);
                        addressObj.EmpReference1Phone = Converter.GetString(txtEmpReference1Phone.Text);
                        addressObj.EmpReference1Relation = Converter.GetString(txtEmpReference1Relation.Text);
                        addressObj.EmpReference2Name = Converter.GetString(txtEmpReference2Name.Text);
                        addressObj.EmpReference2AddL1 = Converter.GetString(txtEmpReference2AddL1.Text);
                        addressObj.EmpReference2AddL2 = Converter.GetString(txtEmpReference2AddL2.Text);
                        addressObj.EmpReference2Phone = Converter.GetString(txtEmpReference2Phone.Text);
                        addressObj.EmpReference2Relation = Converter.GetString(txtEmpReference2Relation.Text);

                        if (chkYes.Checked)
                        {
                            addressObj.EmpConfirmRefYN = 1;
                            addressObj.EmpConfirmRefByEmpID = Converter.GetString(txtConfirmByEmpID.Text);
                            addressObj.EmpConfirmMode = Converter.GetString(txtEmpConfirmMode.Text);
                        }
                        else
                        {
                            addressObj.EmpConfirmRefYN = 2;
                            addressObj.EmpConfirmRefByEmpID = string.Empty;
                            addressObj.EmpConfirmMode = "0";
                        }
                        addressObj.EmpEmergencyName = Converter.GetString(txtEmpEmergencyName.Text);
                        addressObj.EmpEmergencyContactNo = Converter.GetString(txtEmpEmergencyContactNo.Text);
                        addressObj.EmpGuarName = Converter.GetString(txtGuarName.Text);
                        addressObj.EmpGuarFatherName = Converter.GetString(txtGuarFatherName.Text);
                        addressObj.EmpGuarNationalID = Converter.GetString(txtGuarNationalID.Text);
                        addressObj.EmpGuarPresentAdd = Converter.GetString(txtGuarPresentAdd.Text);
                        addressObj.EmpGuarPermanentAdd = Converter.GetString(txtGuarPermanentAdd.Text);
                        addressObj.EmpGuarProfessInfo = Converter.GetString(txtGuarProfessInfo.Text);
                        addressObj.EmpGuarMobile = Converter.GetString(txtGuarMobile.Text);
                        addressObj.EmpHoldMobile = Converter.GetString(txtHoldMobile.Text);
                        addressObj.EmpHoldVehicle = Converter.GetString(txtHoldVehicle.Text);
                        addressObj.EmpHoldComputer = Converter.GetString(txtHoldComputer.Text);
                        addressObj.EmpHoldOther = Converter.GetString(txtHoldOther.Text);
                        addressObj.EmpHoldSecurityDeposit = Converter.GetString(txtHoldSecurityDeposit.Text);
                        addressObj.EmpHoldCertificate = Converter.GetString(txtHoldCertificate.Text);
                        addressObj.EmpHoldCheque = Converter.GetString(txtHoldCheque.Text);
                        addressObj.EmpHoldDeed = Converter.GetString(txtHoldDeed.Text);
                        addressObj.EmpEmergencyName = Converter.GetString(txtEmpEmergencyName.Text);
                        addressObj.EmpEmergencyContactNo = Converter.GetString(txtEmpEmergencyContactNo.Text);

                        noOfRowEffected = A2ZEMPADDRESSDTO.InsertAddressInformation(addressObj);
                        if (noOfRowEffected > 0)
                        {
                            txtEmpCode.Text = A2ZEMPLOYEEDTO.GetLastSerialNo().ToString();
                            ClearInformationForaDDRESSSave();
                            chkAddress.Checked = false;
                            DivAddress.Visible = false;


                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        ShowMessage("Please Fill Your Contact Address or Make Sure Adress chkbox is chaaked !.", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Please Fill All Information Including Address Then Proceed !.", Color.Red);
                throw ex;
            }

        }

        private void ClearInformationForSave()
        {

            ddlEmpcode.SelectedIndex = 0;
            txtEmpName.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            txtMotherName.Text = string.Empty;
            txtSpouseName.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtPrintSlNo.Text = string.Empty;
            txtEmpFileNo.Text = string.Empty;

            ddlDesignation.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
            ddlMarried.SelectedIndex = 2;
            txtConfrimDate.Text = string.Empty;
            txtPromotionDate.Text = string.Empty;

            ddlSection.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlServiceType.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlEmpcode.SelectedIndex = 0;
            txtNid.Text = string.Empty;
            txtGrade.Text = string.Empty;
            txtBloodGroup.Text = string.Empty;

            txtQualification.Text = string.Empty;


        }

        private void ClearInformationForaDDRESSSave()
        {


            txtPermanentAdd1.Text = string.Empty;
            txtPermanentAdd2.Text = string.Empty;
            ddlPermanentDivision.SelectedIndex = 0;
            ddlPermanentDistrict.SelectedIndex = 0;
            ddlPermanentThana.SelectedIndex = 0;
            txtPresentAdd1.Text = string.Empty;
            txtPresentAdd2.Text = string.Empty;
            ddlPresentDivision.SelectedIndex = 0;
            ddlPresentDistrict.SelectedIndex = 0;
            ddlPresentThana.SelectedIndex = 0;
            txtMobile.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlEmpcode.SelectedIndex = 0;

            txtEmpConfirmMode.Text = string.Empty;

            txtEmpPermanentPostCode.Text = string.Empty;
            txtEmpPresentPostCode.Text = string.Empty;
            txtEmpReference1Name.Text = string.Empty;
            txtEmpReference1AddL1.Text = string.Empty;
            txtEmpReference1AddL2.Text = string.Empty;

            txtEmpReference1Phone.Text = string.Empty;

            txtEmpReference1Relation.Text = string.Empty;

            txtEmpReference2Name.Text = string.Empty;

            txtEmpReference2AddL1.Text = string.Empty;

            txtEmpReference2AddL2.Text = string.Empty;

            txtEmpReference2Phone.Text = string.Empty;

            txtEmpReference2Relation.Text = string.Empty;
            txtConfirmByEmpID.Text = string.Empty;


            chkYes.Checked = false;
            chkNo.Checked = false;

            txtGuarName.Text = string.Empty;
            txtGuarFatherName.Text = string.Empty;
            txtGuarNationalID.Text = string.Empty;
            txtGuarPresentAdd.Text = string.Empty;
            txtGuarPermanentAdd.Text = string.Empty;
            txtGuarProfessInfo.Text = string.Empty;
            txtGuarMobile.Text = string.Empty;

            txtHoldMobile.Text = string.Empty;
            txtHoldVehicle.Text = string.Empty;
            txtHoldComputer.Text = string.Empty;
            txtHoldOther.Text = string.Empty;
            txtHoldSecurityDeposit.Text = string.Empty;
            txtHoldCertificate.Text = string.Empty;
            txtHoldCheque.Text = string.Empty;
            txtHoldDeed.Text = string.Empty;
            txtEmpEmergencyName.Text = string.Empty;
            txtEmpEmergencyContactNo.Text = string.Empty;




        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int noOfRowEffected;

            string[] ddmmyyyy;
            try
            {


                A2ZEMPLOYEEDTO employeeObj = new A2ZEMPLOYEEDTO();

                employeeObj.EmpCode = Converter.GetSmallInteger(ddlEmpcode.SelectedValue);
                employeeObj.EmpName = Converter.GetString(txtEmpName.Text);
                employeeObj.EmpFatherName = Converter.GetString(txtFatherName.Text.Trim());
                employeeObj.EmpMotherName = Converter.GetString(txtMotherName.Text.Trim());
                employeeObj.EmpSpouseName = Converter.GetString(txtSpouseName.Text.Trim());

                ddmmyyyy = txtDOB.Text.Split('/');

                employeeObj.EmpDateOfBirth = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                ddmmyyyy = txtJoinDate.Text.Split('/');
                employeeObj.EmpDateOfJoin = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                employeeObj.ReligionCode = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                employeeObj.ServiceType = Converter.GetSmallInteger(ddlServiceType.SelectedValue);
                employeeObj.LocationCode = Converter.GetSmallInteger(ddlLocation.SelectedValue);
                employeeObj.DepartmentCode = Converter.GetSmallInteger(ddlDepartment.SelectedValue);
                employeeObj.SectionCode = Converter.GetSmallInteger(ddlSection.SelectedValue);

                employeeObj.DesigCode = Converter.GetSmallInteger(ddlDesignation.SelectedValue);
                employeeObj.AreaCode = Converter.GetSmallInteger(ddlArea.SelectedValue);
                employeeObj.EmpGender = Converter.GetSmallInteger(ddlGender.SelectedValue);

                if (txtSpouseName.Text == string.Empty)
                {
                    // 2 Unmarried
                    employeeObj.EmpMarried = Converter.GetSmallInteger(2);

                }
                else
                {
                    // 1 Married
                    employeeObj.EmpMarried = Converter.GetSmallInteger(1);
                }

                employeeObj.EmpNationalID = Converter.GetString(txtNid.Text);
                employeeObj.EmpBloodGroup = Converter.GetString(txtBloodGroup.Text);

                employeeObj.EmpGrade = Converter.GetString(ddlGrade.SelectedItem.Text);
                employeeObj.EmpQualification = txtQualification.Text;

                employeeObj.EmpPrintSrl = Converter.GetSmallInteger(txtPrintSlNo.Text);
                ddmmyyyy = txtConfrimDate.Text.Split('/');
                employeeObj.EmpConfirmDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);

                ddmmyyyy = txtPromotionDate.Text.Split('/');
                employeeObj.EmpPromotionDate = Converter.GetDateTime(ddmmyyyy[1], ddmmyyyy[0], ddmmyyyy[2]);


                employeeObj.EmpPicUrl = fileName;

                employeeObj.EmployeeID = txtEmpFileNo.Text;

                noOfRowEffected = A2ZEMPLOYEEDTO.UpdateEmployeeInformation(employeeObj);
                if (noOfRowEffected > 0)
                {

                    ClearInformationForSave();

                    if (chkAddress.Checked == false)
                    {
                        txtEmployeMody.Text = string.Empty;
                    }
                    else
                    {

                    }

                }
                else
                {
                    ShowMessage("Data has not been modified.", Color.Red);
                }


                if (chkAddress.Checked)
                {


                    A2ZEMPADDRESSDTO addressObj = new A2ZEMPADDRESSDTO();

                    addressObj.EmpCode = Converter.GetSmallInteger(txtEmployeMody.Text);
                    addressObj.EmpPermanentAddL1 = Converter.GetString(txtPermanentAdd1.Text.Trim());
                    addressObj.EmpPermanentAddL2 = Converter.GetString(txtPermanentAdd2.Text.Trim());
                    addressObj.EmpPermanentDiviCode = Converter.GetSmallInteger(ddlPermanentDivision.SelectedValue);
                    addressObj.EmpPermanentDistCode = Converter.GetSmallInteger(ddlPermanentDistrict.SelectedValue);
                    addressObj.EmpPermanentThanaCode = Converter.GetSmallInteger(ddlPermanentThana.SelectedValue);
                    addressObj.EmpPresentAddL1 = Converter.GetString(txtPresentAdd1.Text.Trim());
                    addressObj.EmpPresentAddL2 = Converter.GetString(txtPresentAdd2.Text.Trim());
                    addressObj.EmpPresentDiviCode = Converter.GetSmallInteger(ddlPresentDivision.SelectedValue);
                    addressObj.EmpPresentDistCode = Converter.GetSmallInteger(ddlPresentDistrict.SelectedValue);
                    addressObj.EmpPresentThanaCode = Converter.GetSmallInteger(ddlPresentThana.SelectedValue);
                    addressObj.EmpMobileNo = Converter.GetString(txtMobile.Text.Trim());
                    addressObj.EmpPhoneNo = Converter.GetString(txtPhone.Text.Trim());
                    addressObj.EmpEmail = Converter.GetString(txtEmail.Text.Trim());

                    addressObj.EmpPermanentPostCode = Converter.GetSmallInteger(txtEmpPermanentPostCode.Text);
                    addressObj.EmpPresentPostCode = Converter.GetSmallInteger(txtEmpPresentPostCode.Text);
                    addressObj.EmpReference1Name = Converter.GetString(txtEmpReference1Name.Text);
                    addressObj.EmpReference2Name = Converter.GetString(txtEmpReference2Name.Text);
                    addressObj.EmpReference1AddL1 = Converter.GetString(txtEmpReference1AddL1.Text);
                    addressObj.EmpReference2AddL1 = Converter.GetString(txtEmpReference2AddL1.Text);
                    addressObj.EmpReference1AddL2 = Converter.GetString(txtEmpReference1AddL2.Text);
                    addressObj.EmpReference2AddL2 = Converter.GetString(txtEmpReference2AddL2.Text);

                    addressObj.EmpReference1Phone = Converter.GetString(txtEmpReference1Phone.Text);
                    addressObj.EmpReference2Phone = Converter.GetString(txtEmpReference2Phone.Text);
                    addressObj.EmpReference1Relation = Converter.GetString(txtEmpReference1Relation.Text);
                    addressObj.EmpReference2Relation = Converter.GetString(txtEmpReference2Relation.Text);

                    if (chkYes.Checked)
                    {
                        addressObj.EmpConfirmRefYN = 1;
                        addressObj.EmpConfirmRefByEmpID = Converter.GetString(txtConfirmByEmpID.Text);
                        addressObj.EmpConfirmMode = Converter.GetString(txtEmpConfirmMode.Text);
                    }
                    else
                    {
                        addressObj.EmpConfirmRefYN = 2;
                        addressObj.EmpConfirmRefByEmpID = string.Empty;
                        addressObj.EmpConfirmMode = "0";
                    }

                    addressObj.EmpGuarName = Converter.GetString(txtGuarName.Text);
                    addressObj.EmpGuarFatherName = Converter.GetString(txtGuarFatherName.Text);
                    addressObj.EmpGuarNationalID = Converter.GetString(txtGuarNationalID.Text);
                    addressObj.EmpGuarPresentAdd = Converter.GetString(txtGuarPresentAdd.Text);
                    addressObj.EmpGuarPermanentAdd = Converter.GetString(txtGuarPermanentAdd.Text);
                    addressObj.EmpGuarProfessInfo = Converter.GetString(txtGuarProfessInfo.Text);
                    addressObj.EmpGuarMobile = Converter.GetString(txtGuarMobile.Text);
                    // addressObj.EmpGuarNoOfChild = Converter.GetString(txtGuarNoOfChild.Text);
                    addressObj.EmpHoldMobile = Converter.GetString(txtHoldMobile.Text);
                    addressObj.EmpHoldVehicle = Converter.GetString(txtHoldVehicle.Text);
                    addressObj.EmpHoldComputer = Converter.GetString(txtHoldComputer.Text);
                    addressObj.EmpHoldOther = Converter.GetString(txtHoldOther.Text);
                    addressObj.EmpHoldSecurityDeposit = Converter.GetString(txtHoldSecurityDeposit.Text);
                    addressObj.EmpHoldCertificate = Converter.GetString(txtHoldCertificate.Text);
                    addressObj.EmpHoldCheque = Converter.GetString(txtHoldCheque.Text);
                    addressObj.EmpHoldDeed = Converter.GetString(txtHoldDeed.Text);

                    addressObj.EmpEmergencyName = Converter.GetString(txtEmpEmergencyName.Text);
                    addressObj.EmpEmergencyContactNo = Converter.GetString(txtEmpEmergencyContactNo.Text);

                    noOfRowEffected = A2ZEMPADDRESSDTO.UpdateAddressInformation(addressObj);


                    if (noOfRowEffected > 0)
                    {

                        String csname1 = "PopupScript";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;

                        if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        {
                            String cstext1 = "alert('Data Updated Successfully.');";
                            cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                        }

                        ddlEmpcode = ddlEmpcode = A2ZEMPLOYEEDTO.GetDropDownList(ddlEmpcode);
                        ClearInformationForSave();
                        ClearInformationForaDDRESSSave();
                        txtEmployeMody.Text = string.Empty;

                    }
                    else
                    {
                        ShowMessage("Data has not been modified.", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("System error occured.", Color.Red);
                throw ex;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            string sourceDir = Server.MapPath("~/Profile Pic/");
            string[] picList = Directory.GetFiles(sourceDir, "*.jpg");

            foreach (string f in picList)
            {
                File.Delete(f);
            }
          
            Response.Redirect("A2ZERPModule.aspx", false);
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");
            DivButton.Attributes.CssStyle.Add("opacity", "100");
            DivAddress.Attributes.CssStyle.Add("opacity", "100");

            DivMessage.Visible = false;
            DivMain.Visible = true;
        }

        protected void ddlPermanentDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPermanentDivision.SelectedItem.Text == "-Select-")
            {
                ddlPermanentDivision.SelectedIndex = 0;
                return;
            }

            PerDistrictDropdown();
        }

        protected void ddlPermanentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPermanentDistrict.SelectedItem.Text == "-Select-")
            {
                ddlPermanentDistrict.SelectedIndex = 0;
                return;
            }

            PerThanaDropdown();
        }

        protected void rbSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                txtEmpCode.Visible = true;
                ddlEmpcode.Visible = false;
                ClearInformationForSave();
                ClearInformationForaDDRESSSave();
                txtEmployeMody.Visible = false;
                chkAddress.Checked = true;
                ddlMarried.SelectedValue = Converter.GetString(2);
                ibtnUpload.Visible = false;
                ImgPicture.ImageUrl = "~/Images/index.jpg";

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        protected void rbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            ddlEmpcode = ddlEmpcode = A2ZEMPLOYEEDTO.GetDropDownList(ddlEmpcode);
            ddlDepartment = A2ZDEPARTMENTDTO.GetDropDownList(ddlDepartment);
            ddlSection = A2ZSECTIONDTO.GetDropDownList(ddlSection);

            chkAddress.Checked = true;

            if (chkAddress.Checked)
            {
                DivAddress.Visible = true;
            }
            else
            {
                DivAddress.Visible = false;
            }

            ddlEmpcode.Focus();
            btnUpdate.Visible = true;
            chkAddress.Checked = true;
            btnSave.Visible = false;
            btnReport.Visible = true;
            btnExit.Visible = true;
            txtEmpCode.Visible = false;
            ddlEmpcode.Visible = true;
            txtEmployeMody.Visible = true;
            txtEmployeMody.Text = string.Empty;
            ddlEmpcode.SelectedIndex = 0;
            ClearInformationForSave();
            ClearInformationForaDDRESSSave();
            ibtnUpload.Visible = true;

        }

        protected void ShowMessage(string message, Color clr)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = clr;
            lblMessage.Visible = true;
            DivMessage.Visible = true;
            DivMain.Attributes.CssStyle.Add("opacity", "0.1");
            DivButton.Attributes.CssStyle.Add("opacity", "0.1");
            DivAddress.Attributes.CssStyle.Add("opacity", "0.1");


            DivMessage.Style.Add("Top", "250px");
            DivMessage.Style.Add("Right", "500px");
            DivMessage.Style.Add("position", "absolute");
            DivMessage.Attributes.CssStyle.Add("opacity", "100");
        }

        protected void ddlPresentDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlPresentDivision.SelectedValue == "-Select-")
            {

                ddlPresentDistrict.SelectedIndex = 0;
                return;
            }
            PreDistrictDropdown();

            


        }

        protected void ddlPresentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPresentDistrict.SelectedItem.Text == "-Select-")
            {
                ddlPresentDistrict.SelectedIndex = 0;
                return;
            }

            PreThanaDropdown();
          

        }

        public void FindAddress()
        {

            try
            {
                Int16 emp = Converter.GetSmallInteger(txtEmployeMody.Text);

                A2ZEMPADDRESSDTO address = A2ZEMPADDRESSDTO.GetAddressInformation(Converter.GetSmallInteger(emp));

                if (address.EmpCode > 0)
                {

                    txtPermanentAdd1.Text = Converter.GetString(address.EmpPermanentAddL1);
                    txtPermanentAdd2.Text = Converter.GetString(address.EmpPermanentAddL2);

                    if (address.EmpPermanentDiviCode == 0)
                    {
                        ddlPermanentDivision.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPermanentDivision.SelectedValue = Converter.GetString(address.EmpPermanentDiviCode);
                    }


                    if (address.EmpPermanentDistCode == 0)
                    {
                        ddlPermanentDistrict.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPermanentDistrict.SelectedValue = Converter.GetString(address.EmpPermanentDistCode);

                    }

                    if (address.EmpPermanentThanaCode == 0)
                    {
                        ddlPermanentThana.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPermanentThana.SelectedValue = Converter.GetString(address.EmpPermanentThanaCode);
                    }

                    txtPresentAdd1.Text = Converter.GetString(address.EmpPresentAddL1);
                    txtPresentAdd2.Text = Converter.GetString(address.EmpPresentAddL2);

                    if (address.EmpPresentDiviCode == 0)
                    {
                        ddlPresentDivision.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPresentDivision.SelectedValue = Converter.GetString(address.EmpPresentDiviCode);
                    }

                    if (address.EmpPresentDistCode == 0)
                    {
                        ddlPresentDistrict.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPresentDistrict.SelectedValue = Converter.GetString(address.EmpPresentDistCode);
                    }

                    if (address.EmpPresentThanaCode == 0)
                    {
                        ddlPresentThana.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlPresentThana.Text = Converter.GetString(address.EmpPresentThanaCode);
                    }

                    txtMobile.Text = Converter.GetString(address.EmpMobileNo);
                    txtPhone.Text = Converter.GetString(address.EmpPhoneNo);
                    txtEmail.Text = Converter.GetString(address.EmpEmail);
                    txtEmpPermanentPostCode.Text = Converter.GetString(address.EmpPermanentPostCode);
                    txtEmpPresentPostCode.Text = Converter.GetString(address.EmpPresentPostCode);
                    txtEmpReference1Name.Text = Converter.GetString(address.EmpReference1Name);
                    txtEmpReference2Name.Text = Converter.GetString(address.EmpReference2Name);
                    txtEmpReference1AddL1.Text = Converter.GetString(address.EmpReference1AddL1);
                    txtEmpReference2AddL1.Text = Converter.GetString(address.EmpReference2AddL1);
                    txtEmpReference1AddL2.Text = Converter.GetString(address.EmpReference1AddL2);
                    txtEmpReference2AddL2.Text = Converter.GetString(address.EmpReference2AddL2);
                    txtEmpReference1Phone.Text = Converter.GetString(address.EmpReference1Phone);
                    txtEmpReference2Phone.Text = Converter.GetString(address.EmpReference2Phone);
                    txtEmpReference1Relation.Text = Converter.GetString(address.EmpReference1Relation);
                    txtEmpReference2Relation.Text = Converter.GetString(address.EmpReference2Relation);

                    if (address.EmpConfirmRefYN == 1)
                    {

                        txtConfirmByEmpID.Visible = true;
                        txtConfirmByEmpIDName.Visible = true;
                        txtEmpConfirmMode.Visible = true;

                        chkNo.Checked = false;

                        chkYes.Checked = true;

                        txtConfirmByEmpID.Text = Converter.GetString(address.EmpConfirmRefByEmpID);

                        // Find  By ConfirmByIDName
                        string FindConfirmByEmpID = string.Empty;

                        FindConfirmByEmpID = @"SELECT EmpName,EmployeeID FROM A2ZEMPLOYEE WHERE  EmployeeID='" + txtConfirmByEmpID.Text + "'";

                        DataTable dts = new DataTable();

                        dts = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(FindConfirmByEmpID, "A2ZHRMCUS");

                        if (dts.Rows.Count > 0)
                        {
                            txtConfirmByEmpIDName.Text = Converter.GetString(dts.Rows[0]["EmpName"]);

                        }

                        else
                        {
                            txtConfirmByEmpIDName.Text = string.Empty;
                        }


                        txtEmpConfirmMode.Text = Converter.GetString(address.EmpConfirmMode);

                    }
                    else
                    {
                        chkYes.Checked = false;
                        txtConfirmByEmpID.Visible = false;
                        txtConfirmByEmpIDName.Visible = false;
                        txtEmpConfirmMode.Visible = false;
                        chkNo.Checked = true;
                    }


                    txtEmpEmergencyName.Text = Converter.GetString(address.EmpEmergencyName);
                    txtEmpEmergencyContactNo.Text = Converter.GetString(address.EmpEmergencyContactNo);
                    txtGuarName.Text = Converter.GetString(address.EmpGuarName);
                    txtGuarFatherName.Text = Converter.GetString(address.EmpGuarFatherName);
                    txtGuarNationalID.Text = Converter.GetString(address.EmpGuarNationalID);
                    txtGuarPresentAdd.Text = Converter.GetString(address.EmpGuarPresentAdd);
                    txtGuarPermanentAdd.Text = Converter.GetString(address.EmpGuarPermanentAdd);
                    txtGuarProfessInfo.Text = Converter.GetString(address.EmpGuarProfessInfo);
                    txtGuarMobile.Text = Converter.GetString(address.EmpGuarMobile);
                    txtHoldMobile.Text = Converter.GetString(address.EmpHoldMobile);
                    txtHoldVehicle.Text = Converter.GetString(address.EmpHoldVehicle);
                    txtHoldComputer.Text = Converter.GetString(address.EmpHoldComputer);
                    txtHoldOther.Text = Converter.GetString(address.EmpHoldOther);
                    txtHoldSecurityDeposit.Text = Converter.GetString(address.EmpHoldSecurityDeposit);
                    txtHoldCertificate.Text = Converter.GetString(address.EmpHoldCertificate);
                    txtHoldCheque.Text = Converter.GetString(address.EmpHoldCheque);
                    txtHoldDeed.Text = Converter.GetString(address.EmpHoldDeed);


                }

            }
            catch (Exception ex)
            {

                ShowMessage("System Error.", Color.Red);
                throw ex;
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlLocation.SelectedItem.Text == "-Select-")
                {
                    ddlLocation.SelectedIndex = 0;
                    return;
                }

                string strQuery = string.Empty;
                strQuery = "SELECT DepartmentCode,DepartmentName FROM A2ZDEPARTMENT WHERE LocationCode='" + ddlLocation.SelectedValue + "'";
                ddlDepartment = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(strQuery, ddlDepartment, "A2ZHRMCUS");

                strQuery = "SELECT AreaCode,AreaDescription FROM A2ZAREA WHERE LocationCode='" + ddlLocation.SelectedValue + "'";
                ddlArea = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(strQuery, ddlArea, "A2ZHRMCUS");

                if (ddlLocation.SelectedValue == "1" || ddlLocation.SelectedValue == "2")
                {
                    ddlArea.Visible = false;
                }
                else
                {
                    ddlArea.Visible = true;
                }

            }

            catch (Exception ex)
            {
                ShowMessage("System error occured.", Color.Red);
                throw ex;
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //SA STRT
                if (ddlDepartment.SelectedItem.Text == "-Select-")
                {
                    ddlDepartment.SelectedIndex = 0;
                    return;
                }
                ////SA END

                string strQuery = string.Empty;
                strQuery = "SELECT SectionCode,SectionName FROM A2ZSECTION WHERE  DepartmentCode='" + ddlDepartment.SelectedValue + "'";
                ddlSection = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(strQuery, ddlSection, "A2ZHRMCUS");

                if (ddlDepartment.SelectedItem.Text == "-Select-")
                {
                    ddlSection.SelectedIndex = 0;
                    return;
                }



            }
            catch (Exception ex)
            {
                ShowMessage("System error occured.", Color.Red);
                throw ex;
            }
        }

        protected void ddlEmpcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ImgPicture.ImageUrl = "~/Images/index.jpg";

                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT EmpCode,Image FROM uploadImg WHERE EmpCode='" + ddlEmpcode.SelectedValue + "'", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] picData = reader["Image"] as byte[] ?? null;

                            if (picData != null)
                            {
                                using (MemoryStream ms = new MemoryStream(picData))
                                {
                                    string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                                    ImgPicture.ImageUrl = "data:image/png;base64," + base64String;

                                }
                            }
                        }
                        else
                        {
                            ImgPicture.ImageUrl = "~/Images/index.jpg";
                        }
                    }

                }


                A2ZEMPLOYEEDTO employeeObj = A2ZEMPLOYEEDTO.GetEmployeeInformation(Converter.GetSmallInteger(ddlEmpcode.SelectedValue));

                if (employeeObj.EmpCode > 0)
                {
                    txtEmployeMody.Text = Converter.GetString(employeeObj.EmpCode);
                    txtEmpName.Text = Converter.GetString(employeeObj.EmpName);
                    txtFatherName.Text = Converter.GetString(employeeObj.EmpFatherName);
                    txtMotherName.Text = Converter.GetString(employeeObj.EmpMotherName);
                    txtSpouseName.Text = Converter.GetString(employeeObj.EmpSpouseName);
                    txtDOB.Text = Converter.GetString(String.Format("{0:dd/MM/yy}", employeeObj.EmpDateOfBirth));
                    txtJoinDate.Text = Converter.GetString(String.Format("{0:dd/MM/yy}", employeeObj.EmpDateOfJoin));
                    ddlGender.SelectedValue = Converter.GetString(employeeObj.EmpGender);
                    ddlMarried.SelectedValue = Converter.GetString(employeeObj.EmpMarried);

                    if (employeeObj.ReligionCode == 0)
                    {
                        ddlReligion.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlReligion.SelectedValue = Converter.GetString(employeeObj.ReligionCode);

                    }

                    if (employeeObj.ServiceType == 0)
                    {
                        ddlServiceType.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlServiceType.SelectedValue = Converter.GetString(employeeObj.ServiceType);
                    }

                    if (employeeObj.LocationCode == 0)
                    {
                        ddlLocation.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlLocation.SelectedValue = Converter.GetString(employeeObj.LocationCode);
                    }

                    if (employeeObj.DepartmentCode == 0)
                    {
                        ddlDepartment.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlDepartment.SelectedValue = Converter.GetString(employeeObj.DepartmentCode);
                    }

                    txtQualification.Text = Converter.GetString(employeeObj.EmpQualification);

                    if (employeeObj.SectionCode == 0)
                    {
                        ddlSection.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlSection.SelectedValue = Converter.GetString(employeeObj.SectionCode);
                    }

                    if (employeeObj.DesigCode == 0)
                    {
                        ddlDesignation.SelectedIndex = 0;
                    }
                    else
                    {

                        ddlDesignation.SelectedValue = Converter.GetString(employeeObj.DesigCode);

                    }

                    if (employeeObj.AreaCode == 0)
                    {
                        ddlArea.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlArea.SelectedValue = Converter.GetString(employeeObj.AreaCode);
                    }

                    txtEmpFileNo.Text = Converter.GetString(employeeObj.EmployeeID);
                    txtPrintSlNo.Text = Converter.GetString(employeeObj.EmpPrintSrl);
                    txtConfrimDate.Text = Converter.GetString(String.Format("{0:dd/MM/yy}", employeeObj.EmpConfirmDate));
                    txtPromotionDate.Text = Converter.GetString(String.Format("{0:dd/MM/yy}", employeeObj.EmpPromotionDate));
                    txtNid.Text = Converter.GetString(employeeObj.EmpNationalID);
                    txtBloodGroup.Text = Converter.GetString(employeeObj.EmpBloodGroup);
                    ddlGrade.SelectedItem.Text = Converter.GetString(employeeObj.EmpGrade);

                    var grade = A2ZGRADEDTO.GetGradeInformation(Converter.GetString(ddlGrade.SelectedItem.Text));
                    txtGrade.Text = grade.GradeDesc;

                    string fileName = Converter.GetString(employeeObj.EmpPicUrl);

                    FindAddress();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtEmployeMody_TextChanged(object sender, EventArgs e)
        {
            FindAddress();

            try
            {
                A2ZEMPLOYEEDTO employee = A2ZEMPLOYEEDTO.GetEmployeeInformation(Converter.GetSmallInteger(txtEmployeMody.Text));

                if (employee.EmpCode > 0)
                {

                    txtEmployeMody.Text = Converter.GetString(employee.EmpCode);

                    ddlEmpcode.SelectedValue = Converter.GetString(employee.EmpCode);
                    ddlEmpcode_SelectedIndexChanged(this, EventArgs.Empty);
                    btnUpdate.Visible = true;

                }
                else
                {
                    ClearInformationForSave();
                    ClearInformationForaDDRESSSave();
                    Validity();
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("System error occured.", Color.Red);
                throw ex;
            }
        }

        private void Validity()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Employee Code - Does Not Exists ');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            ImgPicture.ImageUrl = "~/Images/index.jpg";
            return;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

            try
            {
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress = p.PrmUnitAdd1;

                if (txtEmployeMody.Text == string.Empty)
                {
                    return;
                }

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress);

                SessionStore.SaveToCustomStore(Params.EMP_NO, txtEmployeMody.Text);
                SessionStore.SaveToCustomStore(Params.REPORT_FILE_NAME_KEY, "rptHREmpMasterFileInfo");
                SessionStore.SaveToCustomStore(Params.REPORT_DATABASE_NAME_KEY, DBConstants.DBName.A2ZHRMCUS);
                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {

                throw ex;
            }




        }

        protected void chkYes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkYes.Checked)
            {

                txtConfirmByEmpID.Visible = true;
                txtConfirmByEmpIDName.Visible = true;

                txtEmpConfirmMode.Visible = true;

                chkNo.Checked = false;


            }
            else
            {

                txtConfirmByEmpID.Visible = false;
                txtConfirmByEmpIDName.Visible = false;
                txtEmpConfirmMode.Visible = false;


            }
        }

        protected void chkNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNo.Checked)
            {

                txtConfirmByEmpID.Text = string.Empty;
                txtConfirmByEmpIDName.Text = string.Empty;
                txtConfirmByEmpID.Visible = false;
                txtConfirmByEmpIDName.Visible = false;
                txtEmpConfirmMode.Visible = false;
                chkYes.Checked = false;

            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {

                A2ZGRADEDTO aGrade = A2ZGRADEDTO.GetGradeInformation(Converter.GetString(ddlGrade.SelectedItem.Text));
                if (aGrade.Grade != "0")
                {
                    txtGrade.Text = aGrade.GradeDesc;
                }
                else
                {
                    txtGrade.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                ShowMessage("System error occured.", Color.Red);
                throw ex;

            }
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SA STRT
            if (ddlSection.SelectedItem.Text == "-Select-")
            {
                ddlSection.SelectedIndex = 0;
                return;
            }
            ////SA END



        }

        protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////SA STRT
            if (ddlServiceType.SelectedItem.Text == "-Select-")
            {
                ddlServiceType.SelectedIndex = 0;
                return;
            }
            ////SA END

        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////SA STRT
            if (ddlReligion.SelectedItem.Text == "-Select-")
            {
                ddlReligion.SelectedIndex = 0;
                return;
            }
            ////SA END


        }

        protected void ddlPermanentThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////SA STRT
            if (ddlPermanentThana.SelectedItem.Text == "-Select-")
            {
                ddlPermanentThana.SelectedIndex = 0;
                return;
            }
            ////SA END
        }

        protected void ddlPresentThana_SelectedIndexChanged(object sender, EventArgs e)
        {////SA STRT
            if (ddlPresentThana.SelectedItem.Text == "-Select-")
            {
                ddlPresentThana.SelectedIndex = 0;
                return;
            }
            ////SA END

        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////SA STRT
            if (ddlDesignation.SelectedItem.Text == "-Select-")
            {
                ddlDesignation.SelectedIndex = 0;
                return;
            }
            ////SA END
        }

        protected void txtSpouseName_TextChanged(object sender, EventArgs e)
        {
            //26/10/2014---ONI

            if (txtSpouseName.Text == string.Empty)
            {
                ddlMarried.SelectedValue = "2";
            }
            else
            {
                ddlMarried.SelectedValue = "1";
            }

            ///END ONI
        }

        protected void txtConfirmByEmpID_TextChanged(object sender, EventArgs e)
        {


            try
            {
                string FindConfirmByEmpID = string.Empty;

                FindConfirmByEmpID = @"SELECT EmpName,EmployeeID FROM A2ZEMPLOYEE WHERE  EmployeeID='" + txtConfirmByEmpID.Text + "'";

                DataTable dts = new DataTable();

                dts = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(FindConfirmByEmpID, "A2ZHRMCUS");

                if (dts.Rows.Count > 0)
                {

                    txtConfirmByEmpIDName.Text = Converter.GetString(dts.Rows[0]["EmpName"]);


                }
                else
                {


                    {

                        Response.Write("<script>alert('This Confirm By Employee ID not exist');</script>");
                        txtConfirmByEmpID.Text = string.Empty;
                        txtConfirmByEmpIDName.Text = string.Empty;

                        txtConfirmByEmpID.Visible = false;
                        txtConfirmByEmpIDName.Visible = false;
                        txtEmpConfirmMode.Visible = false;
                        chkYes.Checked = false;
                        chkNo.Checked = true;

                        return;

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void txtEmpFileNo_TextChanged(object sender, EventArgs e)
        {
            //START ONI 10/29/2014 

            if (btnSave.Visible == true && rbSave.Checked)
            {
                string str = @"SELECT * FROM A2ZEMPLOYEE WHERE EmployeeID='" + txtEmpFileNo.Text + "'";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(str, "A2ZHRMCUS"));

                if (result > 0)
                {
                    Response.Write("<script>alert('This Employee ID is already exist');</script>");

                    txtEmpFileNo.Text = string.Empty;
                    txtEmpFileNo.Focus();

                    return;
                }


            }
            else
            {

                A2ZEMPLOYEEDTO empObj = new A2ZEMPLOYEEDTO();

                empObj.EmpCode = Converter.GetSmallInteger(ddlEmpcode.SelectedValue);
                empObj.EmployeeID = txtEmpFileNo.Text;

                string strQuery = string.Empty;
                strQuery = "SELECT EmpCode,EmployeeID FROM A2ZEMPLOYEE WHERE EmpCode!='" + empObj.EmpCode + "' AND EmployeeID='" + empObj.EmployeeID + "'";
                var dt = new DataTable();
                dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    Response.Write("<script>alert('This Employee ID is already exist by other Employee.');</script>");
                    txtEmpFileNo.Text = string.Empty;

                    ddlEmpcode_SelectedIndexChanged(this, EventArgs.Empty);

                    return;
                }
                else
                {

                }

            }

            //END ONI
        }

        protected void ibtnUpload_Click(object sender, ImageClickEventArgs e)
        {

            System.Threading.Thread.Sleep(3000);


            if (rbUpdate.Checked)
            {
                string strQuery = string.Empty;
                strQuery = "SELECT EmpCode,EmployeeID FROM A2ZEMPLOYEE WHERE EmpCode ='" + txtEmployeMody.Text + "'";
                var dt = new DataTable();
                dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZHRMCUS");
                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script>alert('This Employee Not Added Yet.');</script>");

                    return;
                }
                if (FileUpload1.HasFile)
                {
                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                    {


                        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZERPHRIMAGE"));
                        SqlCommand cmd = new SqlCommand("SP_A2ZERPIUPDATEIMAGE", con);
                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                        cmd.Parameters.AddWithValue("@EmpCode", txtEmployeMody.Text);

                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result == 1)
                        {
                            string filename = FileUpload1.FileName;
                            FileUpload1.SaveAs(Server.MapPath("~/Profile_Pic/") + filename);
                            ImgPicture.ImageUrl = "~/Profile_Pic/" + filename;

                        }



                    }

                }
            }

        }

        protected bool IsImageFile(HttpPostedFile httpPostedFile)
        {
            bool isImage = false;
            string fullPath = Server.MapPath("~/Profile_Pic/" + FileUpload1.FileName);
            FileUpload1.SaveAs(fullPath);
            ImgPicture.ImageUrl = "~/Profile_Pic/@" + FileUpload1.FileName;
            System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer = br.ReadByte();
            fileclass = buffer.ToString();
            buffer = br.ReadByte();
            fileclass += buffer.ToString();

            if (fs.Length > 1049097)
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Picture Size Should be 1 mb');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }

                ImgPicture.ImageUrl = "~/Images/index.jpg";

                br.Close();
                fs.Close();

                return false;
            }

            br.Close();
            fs.Close();
            // only allow images    jpg       gif     bmp     png      
            String[] fileType = { "255216", "7173", "6677", "13780" };
            for (int i = 0; i < fileType.Length; i++)
            {
                if (fileclass == fileType[i])
                {
                    isImage = true;
                    break;
                }
            }
            return isImage;
        }


        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (rbSave.Checked)
            {
                //string sqlquery = @"DELETE  FROM uploadImg where EmpCode= '" + txtEmpCode.Text + "' ";
                //int result = Converter.GetSmallInteger(CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZERPHRIMAGE"));
                ImgPicture.ImageUrl = "~/Images/index.jpg";
            }
            else
            {
                //string sqlquery = @"DELETE  FROM uploadImg where EmpCode= '" + ddlEmpcode.SelectedValue + "' ";
                //int result = Converter.GetSmallInteger(CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZERPHRIMAGE"));
                ImgPicture.ImageUrl = "~/Images/index.jpg";
            }
        }


    }
}


