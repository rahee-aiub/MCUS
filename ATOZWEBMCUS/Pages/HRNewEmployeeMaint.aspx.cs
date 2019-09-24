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
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.BLL;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRNewEmployeeMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    txtEmpID.Focus();

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
                    pnlPersonal.Visible = false;
                    pnlAddress.Visible = false;
                    pnlEducation.Visible = false;
                    pnlNominee.Visible = false;
                    btnPersonal.Visible = false;
                    btnAddress.Visible = false;
                    BtnNominee.Visible = false;
                    BtnEducation.Visible = false;
                    BtnEmpUpdate.Visible = false;
                    BtnEduUpdate.Visible = false;
                    BtnNomUpdate.Visible = false;
                    BtnSubmit.Visible = false;

                    //ddlDesignation.Enabled = false;
                    //ddlArea.Enabled = false;
                    //ddlSection.Enabled = false;
                    //ddlDepartment.Enabled = false;
                    //ddlProject.Enabled = false;

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblProceDate.Text = date;
                    txtJoiningDate.Text = date;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");

                //throw ex;
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
            string sqlquery = "SELECT BankCode,BankName from A2ZBANK WHERE BankCode != 99";
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

        protected void DeleteWfNomInfo()
        {
            string delqry = "DELETE FROM WFNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));
        }

        protected void DeleteNomInfo()
        {
            string delqry1 = "DELETE FROM A2ZEMPNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
            int row1Effect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry1, "A2ZHRMCUS"));
        }
        protected void GetNomineeInfo()
        {
            DeleteWfNomInfo();
            string statment = "INSERT INTO WFNOMINEE (EmployeeID,NomineeName,Relation,Address,ContactNo)SELECT EmployeeID,NomineeName,Relation,Address,ContactNo FROM A2ZEMPNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZHRMCUS"));
            if (rowEffect2 > 0)
            {

            }
        }

        protected void DeleteWfEduInfo()
        {
            string delqry = "DELETE FROM WFEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));
        }

        protected void DeleteEduInfo()
        {
            string delqry1 = "DELETE FROM A2ZEMPEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
            int row1Effect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry1, "A2ZHRMCUS"));
        }

        protected void GetEduInfo()
        {
            DeleteWfEduInfo();
            string statment2 = "INSERT INTO WFEDUCATION (EmployeeID,Degree,Result,PassYear,Board)SELECT EmployeeID,Degree,Result,PassYear,Board FROM A2ZEMPEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment2, "A2ZHRMCUS"));
            if (rowEffect2 > 0)
            {

            }
        }
        protected void ClearInfo()
        {
            // txtEmpID.Text=string.Empty;
            //txtEmpName.Text = string.Empty;
            //txtJoiningDate.Text = string.Empty;
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
            ddlPerUpzila.SelectedIndex = 0;
            ddlPreThana.SelectedIndex = 0;
            ddlPerDivision.SelectedIndex = 0;
            ddlPerDistrict.SelectedIndex = 0;
            ddlPerUpzila.SelectedIndex = 0;
            ddlPerThana.SelectedIndex = 0;
            //ddlGrade.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlServiceType.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlProject.SelectedIndex = 0;
            ddlBank.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
            ddlMaritalStatus.SelectedIndex = 0;

            txtJoiningDate.Text = lblProceDate.Text;

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
            PreUpzilaDropdown();
        }

        protected void ddlPreUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreThanaDropdown();

        }
        protected void ddlPerDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerDistrictDropdown();
        }

        protected void ddlPerDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerUpzilaDropdown();
        }
        protected void ddlPerUpzila_SelectedIndexChanged(object sender, EventArgs e)
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
            txtDegree.Focus();
        }

        protected void BtnNominee_Click(object sender, EventArgs e)
        {

            pnlPersonal.Visible = false;
            pnlAddress.Visible = false;
            pnlEducation.Visible = false;
            pnlNominee.Visible = true;
            txtNomName.Focus();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnEduAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtEmpID.Text != string.Empty)
                {
                    string statment5 = "INSERT INTO  WFEDUCATION (EmployeeID,Degree,Result,PassYear,Board) VALUES('" + txtEmpID.Text + "','" + txtDegree.Text + "','" + txtResult.Text + "','" + txtPassYr.Text + "','" + txtBoard.Text + "')";
                    int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZHRMCUS"));
                    gvEducationInfo();
                    txtDegree.Text = string.Empty;
                    txtDegree.Focus();
                    txtResult.Text = string.Empty;
                    txtPassYr.Text = string.Empty;
                    txtBoard.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEduAdd_Click Problem');</script>");

                //throw ex;
            }
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

        protected void gvEducation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvEducation.SelectedRow;

            Label lblId = (Label)gvEducation.Rows[row.RowIndex].Cells[0].FindControl("lblId");
            int ID = Converter.GetInteger(lblId.Text);
            lblEduId.Text = Converter.GetString(ID);

            txtDegree.Text = row.Cells[1].Text;
            txtResult.Text = row.Cells[2].Text;
            txtPassYr.Text = row.Cells[3].Text;
            txtBoard.Text = row.Cells[4].Text;
            BtnEduUpdate.Visible = true;
            txtDegree.Focus();
            BtnEduAdd.Visible = false;

        }

        protected void BtnEduUpdate_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtEmpID.Text != string.Empty)
                {
                    string statment4 = "UPDATE WFEDUCATION SET Degree='" + txtDegree.Text + "',Result='" + txtResult.Text + "',PassYear='" + txtPassYr.Text + "',Board='" + txtBoard.Text + "' Where Id='" + lblEduId.Text + "'";
                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment4, "A2ZHRMCUS"));
                    gvEducationInfo();
                    txtDegree.Text = string.Empty;
                    txtDegree.Focus();
                    txtResult.Text = string.Empty;
                    txtPassYr.Text = string.Empty;
                    txtBoard.Text = string.Empty;
                    BtnEduAdd.Visible = true;
                    BtnEduUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEduUpdate_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void BtnNomAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtEmpID.Text != string.Empty)
                {
                    string statment5 = "INSERT INTO  WFNOMINEE (EmployeeID,NomineeName,Relation,Address,ContactNo) VALUES('" + txtEmpID.Text + "','" + txtNomName.Text + "','" + txtNomRelation.Text + "','" + txtNomAdress.Text + "','" + txtNomContactNo.Text + "')";
                    int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZHRMCUS"));
                    gvNomineeInfo();
                    txtNomName.Text = string.Empty;
                    txtNomName.Focus();
                    txtNomRelation.Text = string.Empty;
                    txtNomAdress.Text = string.Empty;
                    txtNomContactNo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnNomAdd_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void gvNominee_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvNominee.SelectedRow;

            Label lblId = (Label)gvNominee.Rows[row.RowIndex].Cells[0].FindControl("lblId");
            int ID = Converter.GetInteger(lblId.Text);
            lblNomId.Text = Converter.GetString(ID);

            txtNomName.Text = row.Cells[1].Text;
            txtNomRelation.Text = row.Cells[2].Text;
            txtNomAdress.Text = row.Cells[3].Text;
            txtNomContactNo.Text = row.Cells[4].Text;
            BtnNomUpdate.Visible = true;
            txtNomName.Focus();
            BtnNomAdd.Visible = false;

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

        protected void BtnNomUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtEmpID.Text != string.Empty)
                {
                    string statment4 = "UPDATE WFNOMINEE SET NomineeName='" + txtNomName.Text + "',Relation='" + txtNomRelation.Text + "',Address='" + txtNomAdress.Text + "',ContactNo='" + txtNomContactNo.Text + "' Where Id='" + lblNomId.Text + "'";
                    int rowEffect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment4, "A2ZHRMCUS"));
                    gvNomineeInfo();
                    txtNomName.Text = string.Empty;
                    txtNomName.Focus();
                    txtNomRelation.Text = string.Empty;
                    txtNomAdress.Text = string.Empty;
                    txtNomContactNo.Text = string.Empty;
                    BtnNomAdd.Visible = true;
                    BtnNomUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnNomUpdate_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void gvEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblId = (Label)gvEducation.Rows[e.RowIndex].Cells[0].FindControl("lblId");

                int ID = Converter.GetInteger(lblId.Text);
                string strQuery1 = "DELETE FROM WFEDUCATION WHERE Id = '" + ID + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHRMCUS"));
                gvEducationInfo();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvEducation_RowDeleting Problem');</script>");

                //throw ex;
            }
        }

        protected void gvNominee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblId = (Label)gvNominee.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int ID = Converter.GetInteger(lblId.Text);
                string strQuery1 = "DELETE FROM WFNOMINEE WHERE Id = '" + ID + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHRMCUS"));
                gvNomineeInfo();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvNominee_RowDeleting Problem');</script>");

                //throw ex;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZEMPLOYEEDTO dto = new A2ZEMPLOYEEDTO();
                dto.EmployeeID = Converter.GetInteger(txtEmpID.Text);
                dto.EmployeeName = Converter.GetString(txtEmpName.Text);
                dto.EmpBaseGrade = Converter.GetSmallInteger(ddlBaseGrade.SelectedValue);

                if (ddlBaseGrade.SelectedValue == "3")
                {
                    dto.EmpConsolidatedAmt = Converter.GetDecimal(txtGrade.Text);
                    dto.EmpConsolidatedDesc = Converter.GetString(txtStep.Text);
                }
                else 
                {
                    dto.EmpGrade = Converter.GetSmallInteger(txtGrade.Text);
                    dto.EmpPayLabel = Converter.GetSmallInteger(txtStep.Text);
                    Int16 Grade = Converter.GetSmallInteger(dto.EmpGrade);
                    A2ZGRADEDTO get1DTO = (A2ZGRADEDTO.GetGradeInformation(Grade));
                    if (get1DTO.ID > 0)
                    {
                        dto.EmpGradeDesc = Converter.GetString(get1DTO.GradeDesc);
                    }
                }
                             


                dto.EmpDesignation = Converter.GetSmallInteger(ddlDesignation.SelectedValue);
                dto.EmpDesigDesc = Converter.GetString(ddlDesignation.SelectedItem.Text);
                dto.ServiceType = Converter.GetSmallInteger(ddlServiceType.SelectedValue);
                dto.EmpSTypeDesc = Converter.GetString(ddlServiceType.SelectedItem.Text);
                dto.EmpArea = Converter.GetInteger(ddlArea.SelectedValue);
                dto.EmpAreaDesc = Converter.GetString(ddlArea.SelectedItem.Text);
                dto.EmpLocation = Converter.GetInteger(ddlLocation.SelectedValue);
                dto.EmpLocationDesc = Converter.GetString(ddlLocation.SelectedItem.Text);
                dto.EmpDepartment = Converter.GetSmallInteger(ddlDepartment.SelectedValue);
                dto.EmpDepartmentDesc = Converter.GetString(ddlDepartment.SelectedItem.Text);
                dto.EmpSection = Converter.GetSmallInteger(ddlSection.SelectedValue);
                dto.EmpSectionDesc = Converter.GetString(ddlSection.SelectedItem.Text);
                dto.EmpProject = Converter.GetSmallInteger(ddlProject.SelectedValue);
                dto.EmpProjectDesc = Converter.GetString(ddlProject.SelectedItem.Text);
                if (txtJoiningDate.Text != string.Empty)
                {
                    DateTime joindate = DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpJoinDate = joindate;

                }
                else
                {
                    string CheckjoinDtNull = "";
                    dto.EmpJoinNullDate = CheckjoinDtNull;

                }

                if (txtPerDate.Text != string.Empty)
                {
                    DateTime perdate = DateTime.ParseExact(txtPerDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpPerDate = perdate;

                }
                else
                {
                    string CheckPerDtNull = "";
                    dto.EmpPerNullDate = CheckPerDtNull;

                }
                if (txtLastPostingDt.Text != string.Empty)
                {
                    DateTime LPostingdate = DateTime.ParseExact(txtLastPostingDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastPostingDate = LPostingdate;

                }
                else
                {
                    string CheckLPostDtNull = "";
                    dto.EmpLastPostingNullDate = CheckLPostDtNull;

                }
                if (txtLastPromotionDt.Text != string.Empty)
                {
                    DateTime LPromotiondate = DateTime.ParseExact(txtLastPromotionDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastPromotionDate = LPromotiondate;

                }
                else
                {
                    string CheckLPromDtNull = "";
                    dto.EmpLastPromotionNullDate = CheckLPromDtNull;

                }
                if (txtLastIncrementDt.Text != string.Empty)
                {
                    DateTime LIncrementdate = DateTime.ParseExact(txtLastPromotionDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastIncrementDate = LIncrementdate;

                }
                else
                {
                    string CheckLIncrementDtNull = "";
                    dto.EmpLastIncrementNullDate = CheckLIncrementDtNull;

                }

                dto.EmpBank = Converter.GetSmallInteger(ddlBank.SelectedValue);
                dto.EmpAccNo = Converter.GetString(txtAccNo.Text);
                dto.EmpFatherName = Converter.GetString(txtFName.Text);
                dto.EmpMotherName = Converter.GetString(txtMName.Text);
                if (txtDateOfBirth.Text != string.Empty)
                {
                    DateTime DOBdate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpDOB = DOBdate;

                }
                else
                {
                    string CheckDOBDtNull = "";
                    dto.EmpDOBNullDate = CheckDOBDtNull;

                }

                dto.EmpSpouseName = Converter.GetString(txtSpouseName.Text);
                dto.EmpNationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                dto.EmpNationalityDesc = Converter.GetString(ddlNationality.SelectedItem.Text);
                dto.EmpRelagion = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                dto.EmpRelagionDesc = Converter.GetString(ddlReligion.SelectedItem.Text);
                dto.EmpGender = Converter.GetSmallInteger(ddlGender.SelectedValue);
                dto.EmpGenderDesc = Converter.GetString(ddlGender.SelectedItem.Text);
                dto.EmpMaritalStat = Converter.GetSmallInteger(ddlMaritalStatus.SelectedValue);
                dto.EmpMaritalStatDesc = Converter.GetString(ddlMaritalStatus.SelectedItem.Text);
                dto.EmpBloodGrp = Converter.GetString(txtBloodGrp.Text);
                dto.EmpHeight = Converter.GetString(txtHeight.Text);
                dto.EmpNationalID = Converter.GetString(txtNationalId.Text);
                dto.TIN = Converter.GetString(txtTin.Text);
                dto.EmpPPNo = Converter.GetString(txtPassportNo.Text);
               


                if (txtPassportIssueDate.Text != string.Empty)
                {
                    DateTime PIsdtdate = DateTime.ParseExact(txtPassportIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpIssueDate = PIsdtdate;

                }
                else
                {
                    string CheckPIssueDtNull = "";
                    dto.EmpIssueNullDate = CheckPIssueDtNull;

                }
                if (txtPassportExpdate.Text != string.Empty)
                {
                    DateTime PExdtdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpPExpireDate = PExdtdate;

                }
                else
                {
                    string CheckPExDtNull = "";
                    dto.EmpPExpireNullDate = CheckPExDtNull;

                }
                dto.EmpPlaceofIssue = Converter.GetString(txtPassportIssuePlace.Text);
                dto.EmpLicenseNo = Converter.GetString(txtLicenseNo.Text);
                if (txtLicenseExpDate.Text != string.Empty)
                {
                    DateTime LExdtdate = DateTime.ParseExact(txtLicenseExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLExpiryDate = LExdtdate;

                }
                else
                {
                    string CheckLExDtNull = "";
                    dto.EmpLExpiryNullDate = CheckLExDtNull;

                }

                dto.Status = 1;

                A2ZCSPARAMETERDTO date = A2ZCSPARAMETERDTO.GetParameterValue();
                dto.StatusDate = Converter.GetDateTime(date.ProcessDate);


                int roweffect = A2ZEMPLOYEEDTO.InsertInformation(dto);
                if (roweffect > 0)
                {
                    if (CtrlAddRec.Text == "0" || CtrlAddRec.Text == string.Empty)
                    {
                        EmployeeAddressInsert();
                    }
                    else
                    {
                        EmployeeAddressUpdate();
                    }
                    SubmitEducationData();
                    SubmitNomineeData();
                    gvEducationInfo();
                    gvNomineeInfo();

                    AddStaff19Account();
                    AddStaff25Account();
                    AddStaff26Account();
                    AddStaff27Account();
                    AddStaff28Account();
                    ClearInfo();
                    txtEmpID.Text = string.Empty;
                    txtEmpName.Text = string.Empty;
                    txtEmpID.Focus();
                    //ddlEmpID.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");

                //throw ex;
            }

        }

        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo=0 and AccType=25";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);
        }

        private void AddStaff19Account()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo= '" + txtEmpID.Text + "' AND AccType=19";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);

            lblAccType.Text = "19";
            GenerateNewAccNo();

            DateTime date = DateTime.ParseExact(lblProceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string stat5 = "INSERT INTO  A2ZMEMBER (CuType,CuNo,MemNo,MemName,MemOpenDate,MemType) VALUES(0,0,'" + txtEmpID.Text + "','" + txtEmpName.Text + "','" + date + "',2)";
            int rEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(stat5, "A2ZCSMCUS"));

            string statment5 = "INSERT INTO  A2ZACCOUNT (CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES(0,0,'" + txtEmpID.Text + "',19,'" + lblNewAccNo.Text + "','" + date + "',1,1)";
            int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZCSMCUS"));
        }

        private void AddStaff25Account()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo= '" + txtEmpID.Text + "' AND AccType=25";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);

            lblAccType.Text = "25";
            GenerateNewAccNo();

            DateTime date = DateTime.ParseExact(lblProceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        
            string statment5 = "INSERT INTO  A2ZACCOUNT (CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES(0,0,'" + txtEmpID.Text + "',25,'" + lblNewAccNo.Text + "','" + date + "',1,1)";
            int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZCSMCUS"));
        }

        private void AddStaff26Account()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo= '" + txtEmpID.Text + "' AND AccType=26";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);

            lblAccType.Text = "26";
            GenerateNewAccNo();

            DateTime date = DateTime.ParseExact(lblProceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string statment5 = "INSERT INTO  A2ZACCOUNT (CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES(0,0,'" + txtEmpID.Text + "',26,'" + lblNewAccNo.Text + "','" + date + "',1,1)";
            int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZCSMCUS"));
        }

        private void AddStaff27Account()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo= '" + txtEmpID.Text + "' AND AccType=27";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);

            lblAccType.Text = "27";
            GenerateNewAccNo();

            DateTime date = DateTime.ParseExact(lblProceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string statment5 = "INSERT INTO  A2ZACCOUNT (CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES(0,0,'" + txtEmpID.Text + "',27,'" + lblNewAccNo.Text + "','" + date + "',1,1)";
            int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZCSMCUS"));
        }

        private void AddStaff28Account()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo= '" + txtEmpID.Text + "' AND AccType=28";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);

            lblAccType.Text = "28";
            GenerateNewAccNo();

            DateTime date = DateTime.ParseExact(lblProceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string statment5 = "INSERT INTO  A2ZACCOUNT (CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES(0,0,'" + txtEmpID.Text + "',28,'" + lblNewAccNo.Text + "','" + date + "',1,1)";
            int rowEffect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment5, "A2ZCSMCUS"));
        }
        protected void GenerateNewAccNo()
        {
            string input2 = Converter.GetString(txtEmpID.Text).Length.ToString();
            string input3 = Converter.GetString(hdnNewAccNo.Text).Length.ToString();


            string result2 = "";
            string result3 = "";

            if (input2 == "1")
            {
                result2 = "000";
            }
            if (input2 == "2")
            {
                result2 = "00";
            }
            if (input2 == "3")
            {
                result2 = "0";
            }

            if (input3 == "1")
            {
                result3 = "000";
            }
            if (input3 == "2")
            {
                result3 = "00";
            }
            if (input3 == "3")
            {
                result3 = "0";
            }

            if (input2 != "4" && input3 != "4")
            {
                lblNewAccNo.Text = lblAccType.Text + result2 + txtEmpID.Text + result3 + hdnNewAccNo.Text;
            }
            if (input2 != "4" && input3 == "4")
            {
                lblNewAccNo.Text = lblAccType.Text + result2 + txtEmpID.Text + hdnNewAccNo.Text;
            }
            if (input2 == "4" && input3 != "4")
            {
                lblNewAccNo.Text = lblAccType.Text + txtEmpID.Text + result3 + hdnNewAccNo.Text;
            }
            if (input2 == "4" && input3 == "4")
            {
                lblNewAccNo.Text = lblAccType.Text + txtEmpID.Text + hdnNewAccNo.Text;
            }


        }



        protected void NewEmpMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Record Already Exist.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Already Exist');", true);
            return;

        }
        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtEmpID.Text != string.Empty)
                {
                    int EmpID = Converter.GetInteger(txtEmpID.Text);
                    A2ZEMPLOYEEDTO getDTO = new A2ZEMPLOYEEDTO();
                    getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
                    if (getDTO.EmployeeID > 0)
                    {
                        NewEmpMSG();
                        txtEmpID.Text = string.Empty;
                        txtEmpID.Focus();
                    }
                    else
                    {
                        btnPersonal.Visible = true;
                        btnAddress.Visible = true;
                        BtnNominee.Visible = true;
                        BtnEducation.Visible = true;
                        pnlPersonal.Visible = true;
                        BtnSubmit.Visible = true;
                        BtnEmpUpdate.Visible = false;
                        ClearInfo();
                        //ddlEmpID.SelectedIndex = 0;
                        txtEmpName.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtEmpID_TextChanged Problem');</script>");

                //throw ex;
            }
        }

        protected void BtnEmpUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZEMPLOYEEDTO dto = new A2ZEMPLOYEEDTO();
                dto.EmployeeID = Converter.GetInteger(txtEmpID.Text);
                dto.EmployeeName = Converter.GetString(txtEmpName.Text);
                dto.EmpBaseGrade = Converter.GetSmallInteger(ddlBaseGrade.SelectedValue);
                dto.EmpGrade = Converter.GetSmallInteger(txtGrade.Text);
                dto.EmpDesignation = Converter.GetSmallInteger(ddlDesignation.SelectedValue);
                dto.ServiceType = Converter.GetSmallInteger(ddlServiceType.SelectedValue);
                dto.EmpArea = Converter.GetInteger(ddlArea.SelectedValue);
                dto.EmpLocation = Converter.GetInteger(ddlLocation.SelectedValue);
                dto.EmpDepartment = Converter.GetSmallInteger(ddlDepartment.SelectedValue);
                dto.EmpSection = Converter.GetSmallInteger(ddlSection.SelectedValue);
                dto.EmpProject = Converter.GetSmallInteger(ddlProject.SelectedValue);
                if (txtJoiningDate.Text != string.Empty)
                {
                    DateTime joindate = DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpJoinDate = joindate;

                }
                else
                {
                    string CheckjoinDtNull = "";
                    dto.EmpJoinNullDate = CheckjoinDtNull;

                }

                if (txtPerDate.Text != string.Empty)
                {
                    DateTime perdate = DateTime.ParseExact(txtPerDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpPerDate = perdate;
                }
                else
                {
                    string CheckPerDtNull = "";
                    dto.EmpPerNullDate = CheckPerDtNull;
                }
                if (txtLastPostingDt.Text != string.Empty)
                {
                    DateTime LPostingdate = DateTime.ParseExact(txtLastPostingDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastPostingDate = LPostingdate;
                }
                else
                {
                    string CheckLPostingDtNull = "";
                    dto.EmpLastPostingNullDate = CheckLPostingDtNull;
                }
                if (txtLastPromotionDt.Text != string.Empty)
                {
                    DateTime LPromotiondate = DateTime.ParseExact(txtLastPromotionDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastPromotionDate = LPromotiondate;
                }
                else
                {
                    string CheckLPromotionDtNull = "";
                    dto.EmpLastPromotionNullDate = CheckLPromotionDtNull;
                }
                if (txtLastIncrementDt.Text != string.Empty)
                {
                    DateTime LIncrementdate = DateTime.ParseExact(txtLastIncrementDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLastIncrementDate = LIncrementdate;
                }
                else
                {
                    string CheckLIncrementDtNull = "";
                    dto.EmpLastIncrementNullDate = CheckLIncrementDtNull;
                }

                dto.EmpBank = Converter.GetSmallInteger(ddlBank.SelectedValue);

                dto.EmpAccNo = Converter.GetString(txtAccNo.Text);
                dto.EmpFatherName = Converter.GetString(txtFName.Text);
                dto.EmpMotherName = Converter.GetString(txtMName.Text);
                if (txtDateOfBirth.Text != string.Empty)
                {
                    DateTime DOBdate = DateTime.ParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpDOB = DOBdate;

                }
                else
                {
                    string CheckDOBDtNull = "";
                    dto.EmpDOBNullDate = CheckDOBDtNull;

                }

                dto.EmpSpouseName = Converter.GetString(txtSpouseName.Text);
                dto.EmpNationality = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                dto.EmpRelagion = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                dto.EmpGender = Converter.GetSmallInteger(ddlGender.SelectedValue);
                dto.EmpMaritalStat = Converter.GetSmallInteger(ddlMaritalStatus.SelectedValue);
                dto.EmpBloodGrp = Converter.GetString(txtBloodGrp.Text);
                dto.EmpHeight = Converter.GetString(txtHeight.Text);
                dto.EmpNationalID = Converter.GetString(txtNationalId.Text);
                dto.TIN = Converter.GetString(txtTin.Text);
                dto.EmpPPNo = Converter.GetString(txtPassportNo.Text);
                dto.EmpPayLabel = Converter.GetSmallInteger(txtStep.Text);
                if (txtPassportIssueDate.Text != string.Empty)
                {
                    DateTime PIsdtdate = DateTime.ParseExact(txtPassportIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpIssueDate = PIsdtdate;

                }
                else
                {
                    string CheckPIssueDtNull = "";
                    dto.EmpIssueNullDate = CheckPIssueDtNull;

                }
                if (txtPassportExpdate.Text != string.Empty)
                {
                    DateTime PExdtdate = DateTime.ParseExact(txtPassportExpdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpPExpireDate = PExdtdate;

                }
                else
                {
                    string CheckPExDtNull = "";
                    dto.EmpPExpireNullDate = CheckPExDtNull;

                }
                dto.EmpPlaceofIssue = Converter.GetString(txtPassportIssuePlace.Text);
                dto.EmpLicenseNo = Converter.GetString(txtLicenseNo.Text);
                if (txtLicenseExpDate.Text != string.Empty)
                {
                    DateTime LExdtdate = DateTime.ParseExact(txtLicenseExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dto.EmpLExpiryDate = LExdtdate;

                }
                else
                {
                    string CheckLExDtNull = "";
                    dto.EmpLExpiryNullDate = CheckLExDtNull;

                }
                int roweffect = A2ZEMPLOYEEDTO.UpdateInformation(dto);
                if (roweffect > 0)
                {
                    if (CtrlAddRec.Text == "0")
                    {
                        EmployeeAddressInsert();
                    }
                    else
                    {
                        EmployeeAddressUpdate();
                    }
                    SubmitEducationData();
                    SubmitNomineeData();
                    gvEducationInfo();
                    gvNomineeInfo();
                    ClearInfo();

                    txtEmpID.Text = string.Empty;
                    txtEmpID.Focus();
                    BtnSubmit.Visible = true;
                    BtnEmpUpdate.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEmpUpdate_Click Problem');</script>");

                //throw ex;
            }
        }



        protected void EmployeeAddressInsert()
        {
            try
            {

                A2ZEMPADDRESSDTO dto = new A2ZEMPADDRESSDTO();

                dto.EmployeeID = Converter.GetInteger(txtEmpID.Text);
                dto.EmpPresentAddress = Converter.GetString(txtPreAdd.Text);
                dto.EmpPreDivision = Converter.GetInteger(ddlPreDivision.SelectedValue);
                dto.EmpPreDistrict = Converter.GetInteger(ddlPreDistrict.SelectedValue);
                dto.EmpPreUpzila = Converter.GetInteger(ddlPreUpzila.SelectedValue);
                dto.EmpPreThana = Converter.GetInteger(ddlPreThana.SelectedValue);
                dto.EmpPreTelNo = Converter.GetString(txtPreTelNo.Text);
                dto.EmpPreMobileNo = Converter.GetString(txtPreMobNo.Text);
                dto.EmpPreEmail = Converter.GetString(txtPreEmail.Text);
                dto.EmpPermanentAddress = Converter.GetString(txtPerAdd.Text);
                dto.EmpPerDivision = Converter.GetInteger(ddlPerDivision.SelectedValue);
                dto.EmpPerDistrict = Converter.GetInteger(ddlPerDistrict.SelectedValue);
                dto.EmpPerUpzila = Converter.GetInteger(ddlPerUpzila.SelectedValue);
                dto.EmpPerThana = Converter.GetInteger(ddlPerThana.SelectedValue);
                dto.EmpPerTelNo = Converter.GetString(txtTelNo.Text);
                dto.EmpPerMobileNo = Converter.GetString(txtMobileNo.Text);
                dto.EmpPerEmail = Converter.GetString(txtEmail.Text);

                int roweffect = A2ZEMPADDRESSDTO.InsertAddressInformation(dto);
                if (roweffect > 0)
                {
                    ClearInfo();
                    //ddlPreDivision.SelectedIndex = 0;
                    //ddlPreDistrict.SelectedIndex = 0;
                    //ddlPreThana.SelectedIndex = 0;
                    //ddlPerDivision.SelectedIndex = 0;
                    //ddlPerDistrict.SelectedIndex = 0;
                    //ddlPerThana.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.EmployeeAddressInsert Problem');</script>");

                //throw ex;
            }

        }

        protected void GetEmployeeAddress()
        {
            try
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
                    ddlPreThana.SelectedValue = Converter.GetString(getDTO.EmpPreThana);
                    txtPreTelNo.Text = Converter.GetString(getDTO.EmpPreTelNo);
                    txtPreMobNo.Text = Converter.GetString(getDTO.EmpPreMobileNo);
                    txtPreEmail.Text = Converter.GetString(getDTO.EmpPreEmail);
                    txtPerAdd.Text = Converter.GetString(getDTO.EmpPermanentAddress);
                    ddlPerDivision.SelectedValue = Converter.GetString(getDTO.EmpPerDivision);
                    ddlPerDistrict.SelectedValue = Converter.GetString(getDTO.EmpPerDistrict);
                    ddlPerThana.SelectedValue = Converter.GetString(getDTO.EmpPerThana);
                    txtTelNo.Text = Converter.GetString(getDTO.EmpPerTelNo);
                    txtMobileNo.Text = Converter.GetString(getDTO.EmpPerMobileNo);
                    txtEmail.Text = Converter.GetString(getDTO.EmpPerEmail);


                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetEmployeeAddress Problem');</script>");

                //throw ex;
            }

        }
        protected void EmployeeAddressUpdate()
        {
            try
            {

                A2ZEMPADDRESSDTO dto = new A2ZEMPADDRESSDTO();

                dto.EmployeeID = Converter.GetInteger(txtEmpID.Text);
                dto.EmpPresentAddress = Converter.GetString(txtPreAdd.Text);
                dto.EmpPreDivision = Converter.GetSmallInteger(ddlPreDivision.SelectedValue);
                dto.EmpPreDistrict = Converter.GetSmallInteger(ddlPreDistrict.SelectedValue);
                dto.EmpPreThana = Converter.GetSmallInteger(ddlPreThana.SelectedValue);
                dto.EmpPreTelNo = Converter.GetString(txtPreTelNo.Text);
                dto.EmpPreMobileNo = Converter.GetString(txtPreMobNo.Text);
                dto.EmpPreEmail = Converter.GetString(txtPreEmail.Text);
                dto.EmpPermanentAddress = Converter.GetString(txtPerAdd.Text);
                dto.EmpPerDivision = Converter.GetSmallInteger(ddlPerDivision.SelectedValue);
                dto.EmpPerDistrict = Converter.GetSmallInteger(ddlPerDistrict.SelectedValue);
                dto.EmpPerThana = Converter.GetSmallInteger(ddlPerThana.SelectedValue);
                dto.EmpPerTelNo = Converter.GetString(txtTelNo.Text);
                dto.EmpPerMobileNo = Converter.GetString(txtMobileNo.Text);
                dto.EmpPerEmail = Converter.GetString(txtEmail.Text);

                int roweffect = A2ZEMPADDRESSDTO.UpdateAddressInformation(dto);
                if (roweffect > 0)
                {
                    ClearInfo();
                    //ddlPreDivision.SelectedIndex = 0;
                    //ddlPreDistrict.SelectedIndex = 0;
                    //ddlPreThana.SelectedIndex = 0;
                    //ddlPerDivision.SelectedIndex = 0;
                    //ddlPerDistrict.SelectedIndex = 0;
                    //ddlPerThana.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.EmployeeAddressUpdate Problem');</script>");

                //throw ex;
            }

        }

        protected void SubmitEducationData()
        {
            try
            {

                DeleteEduInfo();

                string statment2 = "INSERT INTO A2ZEMPEDUCATION  (EmployeeID,Degree,Result,PassYear,Board) SELECT EmployeeID,Degree,Result,PassYear,Board FROM WFEDUCATION WHERE EmployeeID='" + txtEmpID.Text + "'";
                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment2, "A2ZHRMCUS"));
                if (rowEffect3 > 0)
                {
                    DeleteWfEduInfo();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.SubmitEducationData Problem');</script>");

                //throw ex;
            }
        }
        protected void SubmitNomineeData()
        {
            try
            {

                DeleteNomInfo();

                string statment2 = "INSERT INTO A2ZEMPNOMINEE (EmployeeID,NomineeName,Relation,Address,ContactNo) SELECT EmployeeID,NomineeName,Relation,Address,ContactNo FROM WFNOMINEE WHERE EmployeeID='" + txtEmpID.Text + "'";
                int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment2, "A2ZHRMCUS"));
                if (rowEffect3 > 0)
                {
                    DeleteWfNomInfo();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.SubmitNomineeData Problem');</script>");

                //throw ex;
            }
        }

        protected void ddlBaseGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBaseGrade.SelectedValue == "3")
            {
                lblGrade.Text = "Amount:";
                lblSteps.Text = "Desc:";
                txtGrade.Text = string.Empty;
                txtStep.Text = string.Empty;
            }
            else 
            {
                lblGrade.Text = "Grade:";
                lblSteps.Text = "Steps:";
                txtGrade.Text = string.Empty;
                txtStep.Text = string.Empty;
            }
        }







    }
}