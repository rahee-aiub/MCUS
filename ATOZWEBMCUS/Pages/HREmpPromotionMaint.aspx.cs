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
    public partial class HREmpPromotionMaint : System.Web.UI.Page
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
                    ServiceTypedropdown();
                    CashCodedropdown();

                    txtNewPayScale.ReadOnly = true;
                    txtNewBasic.ReadOnly = true;
                    txtEmpNo.ReadOnly = false;

                    A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtNewPromotionDate.Text = date;


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

        private void ServiceTypedropdown()
        {
            string sqlquery = "SELECT ServiceType,ServiceName from A2ZSERVICETYPE";
            ddlNewServiceType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewServiceType, "A2ZHRMCUS");
        }
        //private void PayScaledropdown()
        //{
        //    string sqlquery = "SELECT Payscale,lTrim(str(StartBasic) + '-' + lTrim(str(Bar1)) + '*' + lTrim(str(label1)) + '-' + lTrim(str(End1Basic)) + '-' + lTrim(str(Bar2)) + '*' + lTrim(str(label2)) + '-' + lTrim(str(End2Basic))) as PayScaleDesc from A2ZPAYSCALE WHERE Base=1";
        //    ddlNewPayScale = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewPayScale, "A2ZHRMCUS");

        //}

        private void CashCodedropdown()
        {
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 AND Status=1 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlNewCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNewCashCode, "A2ZGLMCUS");
        }

        protected void gvDetail()
        {
            try
            {
                string sqlquery4 = "SELECT EmpCode,EmpPromotionDate,EmpNewAreaDesc,EmpNewLocationDesc,EmpNewSectionDesc,EmpNewDepartmentDesc,EmpNewProjectDesc,EmpNewDesigDesc,EmpNewSTypeDesc,EmpNewBaseGradeDesc,EmpNewGrade,EmpNewPayLabel,EmpNewBasic FROM A2ZHRPROMOTION WHERE EmpCode= '" + txtEmpNo.Text + "'";

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

        protected void InvalidEmpCodeMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Employee's No.');", true);
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
                    txtNewPromotionDate.Focus();
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

                    lblLastServiceTypeCode.Text = Converter.GetString(getDTO.ServiceType);
                    lblLastServiceType.Text = Converter.GetString(getDTO.EmpSTypeDesc);

                    if (lblLastServiceTypeCode.Text == string.Empty || lblLastServiceTypeCode.Text == "0")
                    {
                        ddlNewServiceType.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNewServiceType.SelectedValue = Converter.GetString(getDTO.ServiceType);
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


                    int cashcode = Converter.GetInteger(getDTO.EmpCashCode);
                    A2ZCGLMSTDTO get5DTO = (A2ZCGLMSTDTO.GetInformation(cashcode));
                    if (get5DTO.GLAccNo > 0)
                    {
                        lblLastCashCode.Text = Converter.GetString(get5DTO.GLAccNo);
                        lblLastCashCodeDesc.Text = Converter.GetString(get5DTO.GLAccDesc);
                        ddlNewCashCode.SelectedValue = Converter.GetString(get5DTO.GLAccNo);
                    }


                    lblLastBaseGradeCode.Text = Converter.GetString(getDTO.EmpBaseGrade);
                    if (lblLastBaseGradeCode.Text == string.Empty || lblLastBaseGradeCode.Text == "0")
                    {
                        ddlNewBaseGrade.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNewBaseGrade.SelectedValue = Converter.GetString(getDTO.EmpBaseGrade);
                    }

                    lblLastBaseGrade.Text = Converter.GetString(ddlNewBaseGrade.SelectedItem.Text);



                    lblLastGrade.Text = Converter.GetString(getDTO.EmpGrade);
                    lblLastGradeDesc.Text = Converter.GetString(getDTO.EmpGradeDesc);
                    txtNewGrade.Text = Converter.GetString(getDTO.EmpGrade);
                    lblNewGradeDesc.Text = Converter.GetString(getDTO.EmpGradeDesc);

                    lblBaseGrade.Text = lblLastBaseGradeCode.Text;
                    lblGrade.Text = lblLastGrade.Text;
                    PayScaleDesc();
                    lblLastPayScale.Text = lblPayScale.Text;

                    lblBaseGrade.Text = ddlNewBaseGrade.SelectedValue;
                    lblGrade.Text = txtNewGrade.Text;
                    PayScaleDesc();
                    txtNewPayScale.Text = lblPayScale.Text;


                    lblLastPayLabel.Text = Converter.GetString(getDTO.EmpPayLabel);
                    txtNewPayLabel.Text = Converter.GetString(getDTO.EmpPayLabel);

                    if (txtNewPayLabel.Text == string.Empty || txtNewPayLabel.Text == "0")
                    {
                        txtNewPayLabel.Text = "1";
                    }

                    lblPayLabel.Text = lblLastPayLabel.Text;
                    PayLabelBasic();
                    lblLastBasic.Text = lblBasic.Text;

                    lblPayLabel.Text = txtNewPayLabel.Text;
                    PayLabelBasic();
                    txtNewBasic.Text = lblBasic.Text;


                    if (getDTO.EmpLastPromotionDate == DateTime.MinValue)
                    {
                        txtLastPromotionDate.Text = string.Empty;
                    }
                    else
                    {
                        DateTime dt = Converter.GetDateTime(getDTO.EmpLastPromotionDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtLastPromotionDate.Text = date;
                    }
                }
                else
                {
                    txtEmpNo.Text = string.Empty;
                    txtEmpNo.Focus();
                    InvalidEmpCodeMSG();
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnSumbit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void clearinfo()
        {
            txtEmpNo.Text = string.Empty;
            lblName.Text = string.Empty;
            txtLastPromotionDate.Text = string.Empty;
            txtNewPromotionDate.Text = string.Empty;
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
            lblLastCashCode.Text = string.Empty;
            ddlNewCashCode.SelectedIndex = 0;
            lblLastServiceType.Text = string.Empty;
            lblLastCashCodeDesc.Text = string.Empty;
            ddlNewServiceType.SelectedIndex = 0;


            lblLastBaseGrade.Text = string.Empty;
            ddlNewBaseGrade.SelectedIndex = 0;

            lblLastGrade.Text = string.Empty;
            txtNewGrade.Text = string.Empty;

            lblLastPayScale.Text = string.Empty;
            txtNewPayScale.Text = string.Empty;

            lblLastPayLabel.Text = string.Empty;
            txtNewPayLabel.Text = string.Empty;

            lblLastBasic.Text = string.Empty;
            txtNewBasic.Text = string.Empty;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZPROMOTIONDTO UpDTO = new A2ZPROMOTIONDTO();

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

                UpDTO.EmpOldCashCode = Converter.GetInteger(lblLastCashCode.Text);

                UpDTO.EmpOldServiceType = Converter.GetSmallInteger(lblLastServiceTypeCode.Text);
                UpDTO.EmpOldSTypeDesc = Converter.GetString(lblLastServiceType.Text);

                UpDTO.EmpOldBaseGrade = Converter.GetSmallInteger(lblLastBaseGrade.Text);
                UpDTO.EmpOldBaseGradeDesc = Converter.GetString(lblLastBaseGrade.Text);

                UpDTO.EmpOldGrade = Converter.GetInteger(lblLastGrade.Text);
                UpDTO.EmpOldGradeDesc = Converter.GetString(lblLastGradeDesc.Text);
                UpDTO.EmpOldPayLabel = Converter.GetSmallInteger(lblLastPayLabel.Text);
                UpDTO.EmpOldPayScaleDesc = Converter.GetString(lblLastPayScale.Text);
                UpDTO.EmpOldBasic = Converter.GetDecimal(lblLastBasic.Text);

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

                UpDTO.EmpNewServiceType = Converter.GetSmallInteger(ddlNewServiceType.SelectedValue);
                UpDTO.EmpNewSTypeDesc = Converter.GetString(ddlNewServiceType.SelectedItem.Text);

                UpDTO.EmpNewBaseGrade = Converter.GetSmallInteger(ddlNewBaseGrade.SelectedValue);
                UpDTO.EmpNewBaseGradeDesc = Converter.GetString(ddlNewBaseGrade.SelectedItem);

                UpDTO.EmpNewGrade = Converter.GetInteger(txtNewGrade.Text);
                UpDTO.EmpNewGradeDesc = Converter.GetString(lblNewGradeDesc.Text);
                UpDTO.EmpNewPayLabel = Converter.GetSmallInteger(txtNewPayLabel.Text);
                UpDTO.EmpNewPayScaleDesc = Converter.GetString(txtNewPayScale.Text);
                UpDTO.EmpNewBasic = Converter.GetDecimal(txtNewBasic.Text);


                if (txtNewPromotionDate.Text != string.Empty)
                {
                    DateTime PDate = DateTime.ParseExact(txtNewPromotionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.EmpPromotionDate = PDate;

                }
                else
                {
                    string CheckPostingDtNull = "";
                    UpDTO.PromotionNullDate = CheckPostingDtNull;

                }

                if (txtLastPromotionDate.Text != string.Empty)
                {
                    DateTime LPDate = DateTime.ParseExact(txtLastPromotionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.EmpLastPromotionDate = LPDate;

                }
                else
                {
                    string CheckLPostingDtNull = "";
                    UpDTO.LPromotionNullDate = CheckLPostingDtNull;

                }
                int roweffect = A2ZPROMOTIONDTO.InsertInformation(UpDTO);
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

                gvDetail();
                gvDetailInfo.Visible = true;

            }
            else
            {
                txtEmpNo.Focus();
            }
        }

        protected void txtNewGrade_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int grade = Converter.GetSmallInteger(txtNewGrade.Text);
                A2ZGRADEDTO get2DTO = (A2ZGRADEDTO.GetGradeInformation(grade));
                if (get2DTO.ID > 0)
                {
                    lblNewGradeDesc.Text = Converter.GetString(get2DTO.GradeDesc);
                    lblGrade.Text = txtNewGrade.Text;
                    PayScaleDesc();
                    txtNewPayScale.Text = lblPayScale.Text;

                    if (txtNewPayLabel.Text != string.Empty)
                    {
                        lblPayLabel.Text = txtNewPayLabel.Text;
                        PayLabelBasic();
                        txtNewBasic.Text = lblBasic.Text;
                    }

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtNewGrade_TextChanged Problem');</script>");
                //throw ex;
            }
        }


        protected void PayScaleDesc()
        {

            string qry1 = "Select Base,Payscale,StartBasic,Bar1,label1,End1Basic,Bar2,label2,End2Basic,Consolidated FROM A2ZPAYSCALE Where Base='" + lblBaseGrade.Text + "' And PayScale='" + lblGrade.Text + "'";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblstbasic.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["StartBasic"]));
                string stbasic = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["StartBasic"]));

                lblbar1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar1"]));
                string bar1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar1"]));

                lbllabel1.Text = Converter.GetString(dt1.Rows[0]["label1"]);
                string label1 = Converter.GetString(dt1.Rows[0]["label1"]);

                lblEndbasic1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End1Basic"]));
                string Endbasic1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End1Basic"]));

                lblbar2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar2"]));
                string bar2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar2"]));

                lbllabel2.Text = Converter.GetString(dt1.Rows[0]["label2"]);
                string label2 = Converter.GetString(dt1.Rows[0]["label2"]);

                lblEndbasic2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End2Basic"]));
                string Endbasic2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End2Basic"]));

                lblconsulted.Text = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Consolidated"]));
                string consulted = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Consolidated"]));

                lblPayScale.Text = stbasic + "-" + bar1 + "*" + label1 + "-" + Endbasic1 + "-" + bar2 + "*" + label2 + "-" + Endbasic2;
            }
        }
        protected void txtNewPayLabel_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPayLabel.Text != "0")
            {
                lblPayLabel.Text = txtNewPayLabel.Text;
                PayLabelBasic();
                txtNewBasic.Text = lblBasic.Text;
            }
            else
            {
                txtNewPayLabel.Text = string.Empty;
                txtNewPayLabel.Focus();
            }
        }

        protected void PayLabelBasic()
        {

            double Basic = 0;
            int newlabel = Converter.GetInteger(lblPayLabel.Text);
            int label1 = Converter.GetInteger(lbllabel1.Text);
            double bar1 = Converter.GetDouble(lblbar1.Text);
            double bar2 = Converter.GetDouble(lblbar2.Text);
            double startbasic = Converter.GetDouble(lblstbasic.Text);
            double end1basic = Converter.GetDouble(lblEndbasic1.Text);

            if (newlabel == 99)
            {
                Basic = Converter.GetDouble(lblconsulted.Text);
            }
            else
                if (newlabel > label1)
                {
                    int restlabel = (newlabel - label1);
                    Basic = (end1basic + (bar2 * (restlabel - 1)));
                }
                else
                {
                    Basic = (startbasic + (bar1 * (newlabel - 1)));
                }

            lblBasic.Text = Converter.GetString(String.Format("{0:0,0.00}", Basic));

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtEmpNo.Focus();
            clearinfo();
            gvDetailInfo.Visible = false;
            txtEmpNo.ReadOnly = false;
        }

        protected void ddlNewBaseGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBaseGrade.Text = ddlNewBaseGrade.SelectedValue;
            lblGrade.Text = txtNewGrade.Text;
            PayScaleDesc();
            txtNewPayScale.Text = lblPayScale.Text;

            lblPayLabel.Text = txtNewPayLabel.Text;
            PayLabelBasic();
            txtNewBasic.Text = lblBasic.Text;
        }


    }
}