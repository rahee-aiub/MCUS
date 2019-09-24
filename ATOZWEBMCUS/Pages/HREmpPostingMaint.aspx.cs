using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmpPostingMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtEmpNo.Focus();
                    Areadropdown();
                    Locationdropdown();
                    Designdropdown();
                    Departmentndropdown();
                    Sectiondropdown();
                    CashCodedropdown();
                    txtEmpNo.ReadOnly = false;

                    A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtNewPostingDate.Text = date;


                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        private void Areadropdown()
        {
            string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT";
            ddlNewArea = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewArea, "A2ZHKMCUS");

        }

        private void Locationdropdown()
        {
            string sqlquery = "SELECT AreaCode,AreaDescription from A2ZAREA";
            ddlNewLocation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewLocation, "A2ZHRMCUS");
        }
        private void Designdropdown()
        {
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlNewDesignation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewDesignation, "A2ZHRMCUS");
        }
        private void Departmentndropdown()
        {
            string sqlquery = "SELECT DepartmentCode,DepartmentName from A2ZDEPARTMENT";
            ddlNewDepartment = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewDepartment, "A2ZHKMCUS");
        }
        private void Sectiondropdown()
        {
            string sqlquery = "SELECT SectionCode,SectionName from A2ZSECTION";
            ddlNewSection = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewSection, "A2ZHKMCUS");
        }


        private void CashCodedropdown()
        {
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 AND Status=1 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlNewCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewCashCode, "A2ZGLMCUS");
        }

        protected void gvDetail()
        {
            try
            {
                string sqlquery4 = "SELECT EmpCode,EmpPostingDate,EmpNewAreaDesc,EmpNewLocationDesc,EmpNewSectionDesc,EmpNewDepartmentDesc,EmpNewProjectDesc,EmpNewDesigDesc,EmpNewCashCode FROM A2ZHRPOSTING WHERE EmpCode= '" + txtEmpNo.Text + "'";

                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetail Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void EditEmpMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Not Found');", true);
            return;

        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Converter.GetInteger(txtEmpNo.Text);
                A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
                if (getDTO.EmployeeID > 0)
                {
                    txtEmpNo.ReadOnly = true;
                    txtNewPostingDate.Focus();
                    lblName.Text = Converter.GetString(getDTO.EmployeeName);


                    int Area = Converter.GetInteger(getDTO.EmpArea);
                    A2ZDISTRICTDTO get1DTO = (A2ZDISTRICTDTO.GetInfo(Area));
                    if (get1DTO.DistrictCode > 0)
                    {
                        lblLastAreaCode.Text = Converter.GetString(get1DTO.DistrictOrgCode);
                        lblLastArea.Text = Converter.GetString(get1DTO.DistrictDescription);
                        ddlNewArea.SelectedValue = Converter.GetString(get1DTO.DistrictOrgCode);
                    }

                    int Location = Converter.GetInteger(getDTO.EmpLocation);
                    A2ZAREADTO get7DTO = (A2ZAREADTO.GetInformation(Location));
                    if (get7DTO.AreaCode > 0)
                    {
                        lblLastLocationCode.Text = Converter.GetString(get7DTO.AreaCode);
                        lblLastLocation.Text = Converter.GetString(get7DTO.AreaDescription);
                        ddlNewLocation.SelectedValue = Converter.GetString(get7DTO.AreaCode);
                    }


                    Int16 Section = Converter.GetSmallInteger(getDTO.EmpSection);
                    A2ZSECTIONDTO get2DTO = (A2ZSECTIONDTO.GetInformation(Section));
                    if (get2DTO.SectionCode > 0)
                    {
                        lblLastSectionCode.Text = Converter.GetString(get2DTO.SectionCode);
                        lblLastSection.Text = Converter.GetString(get2DTO.SectionName);
                        ddlNewSection.SelectedValue = Converter.GetString(get2DTO.SectionCode);

                    }

                    Int16 Department = Converter.GetSmallInteger(getDTO.EmpDepartment);
                    A2ZDEPARTMENTDTO get3DTO = (A2ZDEPARTMENTDTO.GetInformation(Department));
                    if (get3DTO.DepartmentCode > 0)
                    {
                        lblLastDepartmentCode.Text = Converter.GetString(get3DTO.DepartmentCode);
                        lblLastDepartment.Text = Converter.GetString(get3DTO.DepartmentName);
                        ddlNewDepartment.SelectedValue = Converter.GetString(get3DTO.DepartmentCode);
                    }

                    Int16 Desig = Converter.GetSmallInteger(getDTO.EmpDesignation);
                    A2ZDESIGNATIONDTO get4DTO = (A2ZDESIGNATIONDTO.GetInformation(Desig));
                    if (get4DTO.DesignationCode > 0)
                    {
                        lblLastDesignationCode.Text = Converter.GetString(get4DTO.DesignationCode);
                        lblLastDesignation.Text = Converter.GetString(get4DTO.DesignationDescription);
                        ddlNewDesignation.SelectedValue = Converter.GetString(get4DTO.DesignationCode);
                    }


                    lblLastProjectCode.Text = Converter.GetString(getDTO.EmpProject);
                    if (lblLastProjectCode.Text == string.Empty || lblLastProjectCode.Text == "0")
                    {
                        ddlNewProject.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNewProject.SelectedValue = Converter.GetString(getDTO.EmpProject);
                    }

                    lblLastProject.Text = Converter.GetString(ddlNewProject.SelectedItem.Text);

                    int cashcode= Converter.GetInteger(getDTO.EmpCashCode);
                    A2ZCGLMSTDTO get5DTO = (A2ZCGLMSTDTO.GetInformation(cashcode));
                    if (get5DTO.GLAccNo > 0)
                    {
                        lblLastCashCode.Text = Converter.GetString(get5DTO.GLAccNo);
                        lblLastCashCodeDesc.Text = Converter.GetString(get5DTO.GLAccDesc);

                        lblLastCashCodeDesc.Text = (lblLastCashCode.Text + "-" + lblLastCashCodeDesc.Text);


                        ddlNewCashCode.SelectedValue = Converter.GetString(get5DTO.GLAccNo);
                    }

                    if (getDTO.EmpLastPostingDate == DateTime.MinValue)
                    {
                        txtLastPostingDate.Text = string.Empty;
                    }
                    else
                    {
                        DateTime dt = Converter.GetDateTime(getDTO.EmpLastPostingDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtLastPostingDate.Text = date;
                    }

                }
                else
                {

                    InvalidEmpCodeMSG();
                    txtEmpNo.Text = string.Empty;
                    txtEmpNo.Focus();

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnSumbit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void InvalidEmpCodeMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Employee's No.');", true);
            return;

        }

        protected void clearinfo()
        {
            txtEmpNo.Text = string.Empty;
            lblName.Text = string.Empty;
            txtLastPostingDate.Text = string.Empty;
            txtNewPostingDate.Text = string.Empty;
            lblLastArea.Text = string.Empty;
            ddlNewArea.SelectedIndex = 0;

            lblLastLocation.Text = string.Empty;
            ddlNewLocation.SelectedIndex = 0;

            lblLastSection.Text = string.Empty;
            ddlNewSection.SelectedIndex = 0;
            lblLastDepartment.Text = string.Empty;
            ddlNewDepartment.SelectedIndex = 0;
            lblLastProject.Text = string.Empty;
            ddlNewProject.SelectedIndex = 0;
            lblLastDesignation.Text = string.Empty;
            ddlNewDesignation.SelectedIndex = 0;
            lblLastCashCodeDesc.Text = string.Empty;
            lblLastCashCode.Text = string.Empty;
            ddlNewCashCode.SelectedIndex = 0;

        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZPOSTINGDTO UpDTO = new A2ZPOSTINGDTO();

                UpDTO.EmpCode = Converter.GetInteger(txtEmpNo.Text);
                UpDTO.EmpOldArea = Converter.GetInteger(lblLastAreaCode.Text);
                UpDTO.EmpOldAreaDesc = Converter.GetString(lblLastArea.Text);

                UpDTO.EmpOldLocation = Converter.GetInteger(lblLastLocationCode.Text);
                UpDTO.EmpOldLocationDesc = Converter.GetString(lblLastLocation.Text);

                UpDTO.EmpOldSection = Converter.GetSmallInteger(lblLastSectionCode.Text);
                UpDTO.EmpOldSectionDesc = Converter.GetString(lblLastSection.Text);
                UpDTO.EmpOldDepartment = Converter.GetSmallInteger(lblLastDepartmentCode.Text);
                UpDTO.EmpOldDepartmentDesc = Converter.GetString(lblLastDepartment.Text);
                UpDTO.EmpOldProject = Converter.GetSmallInteger(lblLastProjectCode.Text);
                UpDTO.EmpOldProjectDesc = Converter.GetString(lblLastProject.Text);
                UpDTO.EmpOldDesignation = Converter.GetSmallInteger(lblLastDesignationCode.Text);
                UpDTO.EmpOldDesigDesc = Converter.GetString(lblLastDesignation.Text);

                UpDTO.EmpOldDesignation = Converter.GetSmallInteger(lblLastDesignationCode.Text);

                UpDTO.EmpNewArea = Converter.GetInteger(ddlNewArea.SelectedValue);
                UpDTO.EmpNewAreaDesc = Converter.GetString(ddlNewArea.SelectedItem.Text);
                UpDTO.EmpNewLocation = Converter.GetInteger(ddlNewLocation.SelectedValue);
                UpDTO.EmpNewLocationDesc = Converter.GetString(ddlNewLocation.SelectedItem.Text);

                UpDTO.EmpNewSection = Converter.GetSmallInteger(ddlNewSection.SelectedValue);
                UpDTO.EmpNewSectionDesc = Converter.GetString(ddlNewSection.SelectedItem.Text);
                UpDTO.EmpNewDepartment = Converter.GetSmallInteger(ddlNewDepartment.SelectedValue);
                UpDTO.EmpNewDepartmentDesc = Converter.GetString(ddlNewDepartment.SelectedItem.Text);
                UpDTO.EmpNewProject = Converter.GetSmallInteger(ddlNewProject.SelectedValue);
                UpDTO.EmpNewProjectDesc = Converter.GetString(ddlNewProject.SelectedItem.Text);
                UpDTO.EmpNewDesignation = Converter.GetSmallInteger(ddlNewDesignation.SelectedValue);
                UpDTO.EmpNewDesigDesc = Converter.GetString(ddlNewDesignation.SelectedItem.Text);

                UpDTO.EmpNewCashCode = Converter.GetInteger(ddlNewCashCode.SelectedValue);

                if (txtLastPostingDate.Text != string.Empty)
                {
                    DateTime LPDate = DateTime.ParseExact(txtLastPostingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.LastPostingDate = LPDate;

                }
                else
                {
                    string CheckLPostingDtNull = "";
                    UpDTO.LPostingNullDate = CheckLPostingDtNull;

                }

                if (txtNewPostingDate.Text != string.Empty)
                {
                    DateTime PDate = DateTime.ParseExact(txtNewPostingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.PostingDate = PDate;

                }
                else
                {
                    string CheckPostingDtNull = "";
                    UpDTO.PostingNullDate = CheckPostingDtNull;

                }
                int roweffect = A2ZPOSTINGDTO.InsertInformation(UpDTO);
                if (roweffect > 0)
                {
                    txtEmpNo.Focus();
                    clearinfo();
                    gvDetailInfo.Visible = false;
                    txtEmpNo.ReadOnly = false;

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (txtEmpNo.Text != string.Empty)
            {
                gvDetailInfo.Visible = true;
                gvDetail();
            }
            else
            {
                txtEmpNo.Focus();
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtEmpNo.Focus();
            clearinfo();
            gvDetailInfo.Visible = false;
            txtEmpNo.ReadOnly = false;
        }

        private void InvalidInputDate()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Input Date');", true);
            return;
        }

        protected void txtNewPostingDate_TextChanged(object sender, EventArgs e)
        {
            if (txtLastPostingDate.Text != string.Empty)
            {
                DateTime opdate1 = DateTime.ParseExact(txtNewPostingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime opdate2 = DateTime.ParseExact(txtLastPostingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (opdate1 < opdate2)
                {
                    txtNewPostingDate.Text = txtLastPostingDate.Text;
                    txtNewPostingDate.Focus();
                    InvalidInputDate();
                    return;
                }
            }

        }




    }
}