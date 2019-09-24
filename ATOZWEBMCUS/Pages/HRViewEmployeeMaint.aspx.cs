using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.SystemControl;
using System.Data.SqlClient;
using System.IO;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRViewEmployeeMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Empdropdown();
                ServiceTypedropdown();
                Areadropdown();
                Locationdropdown();
                Designdropdown();
                Bankdropdown();
                Departmentndropdown();
                Sectiondropdown();
                Nationalitydropdown();
                Religiondropdown();
                PreDivisionDropdown();
                PerDivisionDropdown();
                PreDistrictInFo();
                PreUpzilaInfo();
                PreThanaInfo();
                PerDistrictInFo();
                PerUpzilaInfo();
                PerThanaInfo();
                pnlPersonal.Visible = true;
                pnlAddress.Visible = false;
                pnlEducation.Visible = false;
                pnlNominee.Visible = false;
                string EmpCode = (string)Session["EmpCode"];
                txtEmpID.Text = EmpCode;
                GetEmployeeInfo();

            }
        }


        private void gvEducationInfo()
        {

            string sqlquery3 = "SELECT Id,Degree,Result,PassYear,Board FROM WFEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "' ";
            gvEducation = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvEducation, "A2ZHRMCUS");
        }
        private void gvNomineeInfo()
        {

            string sqlquery3 = "SELECT Id,NomineeName,Relation,Address,ContactNo FROM WFNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "' ";
            gvNominee = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvNominee, "A2ZHRMCUS");
        }

        private void Bankdropdown()
        {
            string sqlquery = "SELECT BankCode,BankName from A2ZBANK";
            ddlBank = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlBank, "A2ZHRMCUS");
        }
        //private void Gradedropdown()
        //{
        //    string sqlquery = "SELECT Grade,GradeDesc from A2ZGRADE";
        //    ddlGrade = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGrade, "A2ZHRMCUS");
        //}
        private void ServiceTypedropdown()
        {
            string sqlquery = "SELECT ServiceType,ServiceName from A2ZSERVICETYPE";
            ddlServiceType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlServiceType, "A2ZHRMCUS");
        }
        private void Areadropdown()
        {
            string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT";
            ddlArea = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlArea, "A2ZHKMCUS");
        }

        private void Locationdropdown()
        {
            string sqlquery = "SELECT AreaCode,AreaDescription from A2ZAREA";
            ddlLocation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLocation, "A2ZHRMCUS");
        }


        private void Designdropdown()
        {
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlDesignation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDesignation, "A2ZHRMCUS");
        }
        private void Departmentndropdown()
        {
            string sqlquery = "SELECT DepartmentCode,DepartmentName from A2ZDEPARTMENT";
            ddlDepartment = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDepartment, "A2ZHKMCUS");
        }
        private void Sectiondropdown()
        {
            string sqlquery = "SELECT SectionCode,SectionName from A2ZSECTION";
            ddlSection = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSection, "A2ZHKMCUS");
        }

        private void Nationalitydropdown()
        {
            string sqlquery = "SELECT NationalityCode,NationalityDescription from A2ZNATIONALITY";
            ddlNationality = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNationality, "A2ZHKMCUS");
        }
        private void Religiondropdown()
        {
            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZHKMCUS");
        }

        //protected void DeleteWfNomInfo()
        //{
        //    string delqry = "DELETE FROM WFNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));
        //}

        //protected void DeleteNomInfo()
        //{
        //    string delqry1 = "DELETE FROM A2ZEMPNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int row1Effect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry1, "A2ZHRMCUS"));
        //}
        //protected void GetNomineeInfo()
        //{
        //    DeleteWfNomInfo();
        //    string statment = "INSERT INTO WFNOMINEE (EmployeeID,NomineeName,Relation,Address,ContactNo)SELECT EmployeeID,NomineeName,Relation,Address,ContactNo FROM A2ZEMPNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZHRMCUS"));
        //    if (rowEffect2 > 0)
        //    {

        //    }
        //}

        //protected void DeleteWfEduInfo()
        //{
        //    string delqry = "DELETE FROM WFEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));
        //}

        //protected void DeleteEduInfo()
        //{
        //    string delqry1 = "DELETE FROM A2ZEMPEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int row1Effect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry1, "A2ZHRMCUS"));
        //}

        //protected void GetEduInfo()
        //{
        //    DeleteWfEduInfo();
        //    string statment2 = "INSERT INTO WFEDUCATION (EmployeeID,Degree,Result,PassYear,Board)SELECT EmployeeID,Degree,Result,PassYear,Board FROM A2ZEMPEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
        //    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment2, "A2ZHRMCUS"));
        //    if (rowEffect2 > 0)
        //    {

        //    }
        //}
        protected void ClearInfo()
        {
            // txtEmpID.Text=string.Empty;
            txtEmpName.Text = string.Empty;
            txtJoiningDate.Text = string.Empty;
            txtPerDate.Text = string.Empty;
            txtLastPostingDt.Text = string.Empty;
            txtLastPromotionDt.Text = string.Empty;
            txtLastIncrementDt.Text = string.Empty;

            ddlBaseGrade.SelectedIndex = 0;
            txtGrade.Text = string.Empty;
            txtStep.Text = string.Empty;

            txtAccNo.Text = string.Empty;
            txtFName.Text = string.Empty;
            txtMName.Text = string.Empty;
            txtSpouseName.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtBloodGrp.Text = string.Empty;
            txtNationalId.Text = string.Empty;
            txtHeight.Text = string.Empty;
            txtTin.Text = string.Empty;
            txtPassportNo.Text = string.Empty;
            txtPassportIssueDate.Text = string.Empty;
            txtPassportIssuePlace.Text = string.Empty;
            txtPassportExpdate.Text = string.Empty;
            txtLicenseNo.Text = string.Empty;
            txtLicenseExpDate.Text = string.Empty;
            txtPreAdd.Text = string.Empty;
            txtPreTelNo.Text = string.Empty;
            txtPreMobNo.Text = string.Empty;
            txtPreEmail.Text = string.Empty;
            txtPerAdd.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlPreDivision.SelectedIndex = 0;
            ddlPreDistrict.SelectedIndex = 0;
            ddlPreThana.SelectedIndex = 0;
            ddlPerDivision.SelectedIndex = 0;
            ddlPerDistrict.SelectedIndex = 0;
            ddlPerThana.SelectedIndex = 0;
            //ddlGrade.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlServiceType.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlBank.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
            ddlMaritalStatus.SelectedIndex = 0;




        }
        private void Empdropdown()
        {
            string sqlquery = "SELECT EmpCode,EmpName from A2ZEMPLOYEE";
            ddlEmpID = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlEmpID, "A2ZHRMCUS");
        }


        private void PreDivisionDropdown()
        {

            string sqlquery = "SELECT DiviOrgCode,DiviDescription from A2ZDIVISION";
            ddlPreDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPreDivision, "A2ZHKMCUS");

        }

        private void PreDistrictDropdown()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT WHERE DiviOrgCode='" + ddlPreDivision.SelectedValue + "' or DiviCode = '0'";
            ddlPreDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreDistrict, "A2ZHKMCUS");

        }

        private void PreUpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviOrgCode='" + ddlPreDivision.SelectedValue + "' and DistOrgCode='" + ddlPreDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPreUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreUpzila, "A2ZHKMCUS");

        }
        private void PreThanaDropdown()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA WHERE DiviOrgCode='" + ddlPreDivision.SelectedValue + "' and DistOrgCode='" + ddlPreDistrict.SelectedValue + "' and UpzilaOrgCode='" + ddlPreUpzila.SelectedValue + "' or DistCode = '0'";

            ddlPreThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreThana, "A2ZHKMCUS");

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
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviOrgCode='" + ddlPerDivision.SelectedValue + "' and DistOrgCode='" + ddlPerDistrict.SelectedValue + "' or DistCode = '0'";

            ddlPerUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerUpzila, "A2ZHKMCUS");

        }
        private void PerThanaDropdown()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA WHERE DiviOrgCode='" + ddlPerDivision.SelectedValue + "' and DistOrgCode='" + ddlPerDistrict.SelectedValue + "' and UpzilaOrgCode='" + ddlPerUpzila.SelectedValue + "' or DistCode = '0'";

            ddlPerThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPerThana, "A2ZHKMCUS");

        }

        private void PreDistrictInFo()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT";
            ddlPreDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreDistrict, "A2ZHKMCUS");

        }

        private void PreUpzilaInfo()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA";

            ddlPreUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreUpzila, "A2ZHKMCUS");

        }
        private void PreThanaInfo()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA";

            ddlPreThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlPreThana, "A2ZHKMCUS");

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


        protected void ddlPreDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreDistrictDropdown();
        }

        protected void ddlPreDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreThanaDropdown();
        }

        protected void ddlPerDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerDistrictDropdown();
        }

        protected void ddlPerDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerThanaDropdown();
        }

        protected void btnPersonal_Click(object sender, EventArgs e)
        {

            pnlPersonal.Visible = true;
            pnlAddress.Visible = false;
            pnlNominee.Visible = false;
            pnlEducation.Visible = false;
        }

        protected void btnAddress_Click(object sender, EventArgs e)
        {

            pnlPersonal.Visible = false;
            pnlAddress.Visible = true;
            pnlEducation.Visible = false;
            pnlNominee.Visible = false;
            txtPreAdd.Focus();
        }

        protected void BtnEducation_Click(object sender, EventArgs e)
        {

            pnlPersonal.Visible = false;
            pnlAddress.Visible = false;
            pnlEducation.Visible = true;
            pnlNominee.Visible = false;

        }

        protected void gvEducation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }
        protected void BtnNominee_Click(object sender, EventArgs e)
        {

            pnlPersonal.Visible = false;
            pnlAddress.Visible = false;
            pnlEducation.Visible = false;
            pnlNominee.Visible = true;

        }

        protected void gvNominee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }


        private void ShowImage()
        {
            using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZIMAGEMCUS")))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='0' and CuNo='0' and MemNo='" + txtEmpID.Text + "'", conn))
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
                }
            }
        }

        private void GetEmployeeInfo()
        {
            int EmpID = Converter.GetInteger(txtEmpID.Text);
            A2ZEMPLOYEEDTO getDTO = new A2ZEMPLOYEEDTO();
            getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
            if (getDTO.EmployeeID > 0)
            {
                btnPersonal.Visible = true;
                btnAddress.Visible = true;
                BtnNominee.Visible = true;
                BtnEducation.Visible = true;
                pnlPersonal.Visible = true;
                txtEmpName.Text = Converter.GetString(getDTO.EmployeeName);
                ddlEmpID.SelectedValue = Converter.GetString(getDTO.EmployeeID);
                ddlBaseGrade.SelectedValue = Converter.GetString(getDTO.EmpBaseGrade);
                

                if (ddlBaseGrade.SelectedValue == "3")
                {
                    txtGrade.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.EmpConsolidatedAmt));
                    
                    txtStep.Text = Converter.GetString(getDTO.EmpConsolidatedDesc);
                    lblGrade.Text = "Amount:";
                    lblSteps.Text = "Desc:";
                }
                else
                {
                    txtGrade.Text = Converter.GetString(getDTO.EmpGrade);
                    txtStep.Text = Converter.GetString(getDTO.EmpPayLabel);
                    lblGrade.Text = "Grade:";
                    lblSteps.Text = "Steps:";
                }


                
                if (getDTO.EmpDesignation == 0)
                {
                    ddlDesignation.SelectedIndex = 0;
                }
                else
                {
                    ddlDesignation.SelectedValue = Converter.GetString(getDTO.EmpDesignation);

                }
                if (getDTO.ServiceType == 0)
                {
                    ddlServiceType.SelectedIndex = 0;
                }
                else
                {
                    ddlServiceType.SelectedValue = Converter.GetString(getDTO.ServiceType);

                }
                if (getDTO.EmpArea == 0)
                {
                    ddlArea.SelectedIndex = 0;
                }
                else
                {
                    ddlArea.SelectedValue = Converter.GetString(getDTO.EmpArea);
                }

                if (getDTO.EmpLocation == 0)
                {
                    ddlLocation.SelectedIndex = 0;
                }
                else
                {
                    ddlLocation.SelectedValue = Converter.GetString(getDTO.EmpLocation);
                }

                if (getDTO.EmpDepartment == 0)
                {
                    ddlDepartment.SelectedIndex = 0;
                }
                else
                {
                    ddlDepartment.SelectedValue = Converter.GetString(getDTO.EmpDepartment);
                }
                if (getDTO.EmpSection == 0)
                {
                    ddlSection.SelectedIndex = 0;
                }
                else
                {
                    ddlSection.SelectedValue = Converter.GetString(getDTO.EmpSection);

                }
                if (getDTO.EmpProject == 0)
                {
                    ddlProject.SelectedIndex = 0;
                }
                else
                {
                    ddlProject.SelectedValue = Converter.GetString(getDTO.EmpProject);
                }
                if (getDTO.EmpJoinDate == DateTime.MinValue)
                {
                    txtJoiningDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt1 = Converter.GetDateTime(getDTO.EmpJoinDate);
                    string date1 = dt1.ToString("dd/MM/yyyy");
                    txtJoiningDate.Text = date1;
                }
                if (getDTO.EmpPerDate == DateTime.MinValue)
                {
                    txtPerDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt2 = Converter.GetDateTime(getDTO.EmpPerDate);
                    string date2 = dt2.ToString("dd/MM/yyyy");
                    txtPerDate.Text = date2;
                }
                if (getDTO.EmpLastPostingDate == DateTime.MinValue)
                {
                    txtLastPostingDt.Text = string.Empty;
                }
                else
                {
                    DateTime dt3 = Converter.GetDateTime(getDTO.EmpLastPostingDate);
                    string date3 = dt3.ToString("dd/MM/yyyy");
                    txtLastPostingDt.Text = date3;
                }
                if (getDTO.EmpLastPromotionDate == DateTime.MinValue)
                {
                    txtLastPromotionDt.Text = string.Empty;
                }
                else
                {
                    DateTime dt4 = Converter.GetDateTime(getDTO.EmpLastPromotionDate);
                    string date4 = dt4.ToString("dd/MM/yyyy");
                    txtLastPromotionDt.Text = date4;
                }

                if (getDTO.EmpLastIncrementDate == DateTime.MinValue)
                {
                    txtLastIncrementDt.Text = string.Empty;
                }
                else
                {
                    DateTime dt5 = Converter.GetDateTime(getDTO.EmpLastIncrementDate);
                    string date5 = dt5.ToString("dd/MM/yyyy");
                    txtLastIncrementDt.Text = date5;
                }


                lblStatus.Text = Converter.GetString(getDTO.Status);
                if(lblStatus.Text == "1")
                {
                    lblEmpStatusDesc.Text = "Active";
                }
                else if(lblStatus.Text == "2")
                {
                    lblEmpStatusDesc.Text = "Resigned";
                }
                else if(lblStatus.Text == "3")
                {
                    lblEmpStatusDesc.Text = "Retired";
                }
                else if(lblStatus.Text == "4")
                {
                    lblEmpStatusDesc.Text = "LPR";
                }
                else if(lblStatus.Text == "5")
                {
                    lblEmpStatusDesc.Text = "Terminated";
                }
                else if(lblStatus.Text == "6")
                {
                    lblEmpStatusDesc.Text = "Dismissed";
                }
                else if(lblStatus.Text == "7")
                {
                    lblEmpStatusDesc.Text = "Discharged";
                }
                else if(lblStatus.Text == "8")
                {
                    lblEmpStatusDesc.Text = "Suspended";
                }
                else if(lblStatus.Text == "9")
                {
                    lblEmpStatusDesc.Text = "Transfer";
                }
                else if(lblStatus.Text == "10")
                {
                    lblEmpStatusDesc.Text = "Death";
                }
                
                if (getDTO.StatusDate == DateTime.MinValue)
                {
                    lblEmpStatusDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt5 = Converter.GetDateTime(getDTO.StatusDate);
                    string date5 = dt5.ToString("dd/MM/yyyy");
                    lblEmpStatusDate.Text = date5;
                }





                if (getDTO.EmpBank == 0)
                {
                    ddlBank.SelectedIndex = 0;
                }
                else
                {
                    ddlBank.SelectedValue = Converter.GetString(getDTO.EmpBank);

                }

                txtAccNo.Text = Converter.GetString(getDTO.EmpAccNo);
                txtFName.Text = Converter.GetString(getDTO.EmpFatherName);
                txtMName.Text = Converter.GetString(getDTO.EmpMotherName);
                if (getDTO.EmpDOB == DateTime.MinValue)
                {
                    txtDateOfBirth.Text = string.Empty;
                }
                else
                {
                    DateTime dt5 = Converter.GetDateTime(getDTO.EmpDOB);
                    string date5 = dt5.ToString("dd/MM/yyyy");
                    txtDateOfBirth.Text = date5;
                }
                txtSpouseName.Text = Converter.GetString(getDTO.EmpSpouseName);
                if (getDTO.EmpNationality == 0)
                {
                    ddlNationality.SelectedIndex = 0;
                }
                else
                {
                    ddlNationality.SelectedValue = Converter.GetString(getDTO.EmpNationality);

                }
                if (getDTO.EmpRelagion == 0)
                {
                    ddlReligion.SelectedIndex = 0;
                }
                else
                {
                    ddlReligion.SelectedValue = Converter.GetString(getDTO.EmpRelagion);
                }
                ddlGender.SelectedValue = Converter.GetString(getDTO.EmpGender);
                ddlMaritalStatus.SelectedValue = Converter.GetString(getDTO.EmpMaritalStat);
                txtBloodGrp.Text = Converter.GetString(getDTO.EmpBloodGrp);
                txtHeight.Text = Converter.GetString(getDTO.EmpHeight);
                txtNationalId.Text = Converter.GetString(getDTO.EmpNationalID);
                txtTin.Text = Converter.GetString(getDTO.TIN);
                txtPassportNo.Text = Converter.GetString(getDTO.EmpPPNo);
                if (getDTO.EmpIssueDate == DateTime.MinValue)
                {
                    txtPassportIssueDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt6 = Converter.GetDateTime(getDTO.EmpIssueDate);
                    string date6 = dt6.ToString("dd/MM/yyyy");
                    txtPassportIssueDate.Text = date6;
                }
                if (getDTO.EmpPExpireDate == DateTime.MinValue)
                {
                    txtPassportExpdate.Text = string.Empty;
                }
                else
                {
                    DateTime dt7 = Converter.GetDateTime(getDTO.EmpPExpireDate);
                    string date7 = dt7.ToString("dd/MM/yyyy");
                    txtPassportExpdate.Text = date7;
                }
                txtPassportIssuePlace.Text = Converter.GetString(getDTO.EmpPlaceofIssue);
                txtLicenseNo.Text = Converter.GetString(getDTO.EmpLicenseNo);
                if (getDTO.EmpLExpiryDate == DateTime.MinValue)
                {
                    txtLicenseExpDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt8 = Converter.GetDateTime(getDTO.EmpLExpiryDate);
                    string date8 = dt8.ToString("dd/MM/yyyy");
                    txtLicenseExpDate.Text = date8;
                }

                ShowImage();
                GetEmployeeAddress();
                gvNomineeInfo();
                gvEducationInfo();

            }
            else
            {
                btnPersonal.Visible = true;
                btnAddress.Visible = true;
                BtnNominee.Visible = true;
                BtnEducation.Visible = true;
                pnlPersonal.Visible = true;
                ClearInfo();
                //ddlEmpID.SelectedIndex = 0;
                txtEmpName.Focus();

            }
        }
        protected void GetEmployeeAddress()
        {
            CtrlAddRec.Text = "0";
            int EmpID = Converter.GetInteger(txtEmpID.Text);
            A2ZEMPADDRESSDTO getDTO = new A2ZEMPADDRESSDTO();
            getDTO = (A2ZEMPADDRESSDTO.GetAddressInformation(EmpID));
            if (getDTO.EmployeeID > 0)
            {
                CtrlAddRec.Text = "1";
                txtPreAdd.Text = Converter.GetString(getDTO.EmpPresentAddress);
                ddlPreDivision.SelectedValue = Converter.GetString(getDTO.EmpPreDivision);
                ddlPreDistrict.SelectedValue = Converter.GetString(getDTO.EmpPreDistrict);
                ddlPreUpzila.SelectedValue = Converter.GetString(getDTO.EmpPreUpzila);
                ddlPreThana.SelectedValue = Converter.GetString(getDTO.EmpPreThana);
                txtPreTelNo.Text = Converter.GetString(getDTO.EmpPreTelNo);
                txtPreMobNo.Text = Converter.GetString(getDTO.EmpPreMobileNo);
                txtPreEmail.Text = Converter.GetString(getDTO.EmpPreEmail);
                txtPerAdd.Text = Converter.GetString(getDTO.EmpPermanentAddress);
                ddlPerDivision.SelectedValue = Converter.GetString(getDTO.EmpPerDivision);
                ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.EmpPerDistrict);
                ddlPerUpzila.SelectedValue = Converter.GetString(getDTO.EmpPerUpzila);
                ddlPerThana.SelectedValue = Converter.GetString(getDTO.EmpPerThana);
                txtTelNo.Text = Converter.GetString(getDTO.EmpPerTelNo);
                txtMobileNo.Text = Converter.GetString(getDTO.EmpPerMobileNo);
                txtEmail.Text = Converter.GetString(getDTO.EmpPerEmail);


            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
             "click", @"<script>window.opener.location.href='HREnquireEmpMasterFile.aspx'; self.close();</script>", false);
        }


    }
}