using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRSalaryCertificateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

              
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                hdnPeriod.Value = Converter.GetString(dt);

                int Month = dt.Month;
                int Year = dt.Year;

                ddlPeriodMM.SelectedValue = Converter.GetString(Month);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(Year);

                hdnMonth.Value = Converter.GetString(Month);
                hdnYear.Value = Converter.GetString(Year);
                ChkAllEmp.Checked = true;
                ddlEmp.Enabled = false;

                txtEmpID.Enabled = false;

                ddlArea.Enabled = false;
                ddlLocation.Enabled = false;
                ddlProject.Enabled = false;
                ddlReligion.Enabled = false;
                ddlGender.Enabled = false;
                ddlStatus.Enabled = false;


                Areadropdown();
                Locationdropdown();
                Religiondropdown();

            }
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


        private void Religiondropdown()
        {
            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZHKMCUS");
        }

        private void Empdropdown()
        {
            string sqlquery = "SELECT EmpCode,EmpName from A2ZEMPLOYEE";
            ddlEmp = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlEmp, "A2ZHRMCUS");
        }


        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;
        }

        private void InvalidSCertificateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Salary Not Posted');", true);
            return;
        }
        
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

         

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ddlPeriodMM.SelectedValue == "0")
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please - Select Month' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }

                if (ddlPeriodYYYY.SelectedValue == "0")
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please - Select Year' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }

                if (ChkAllEmp.Checked== false && ddlEmp.SelectedValue == "0")
                  
                  {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please - Select Employee's Name' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;

                }




                string dt = (ddlPeriodMM.SelectedValue + "/" + 01 + "/" + ddlPeriodYYYY.SelectedValue);
                DateTime date = Converter.GetDateTime(dt);

                hdnDate.Value = Converter.GetString(dt);

                DateTime date1 = Converter.GetDateTime(hdnPeriod.Value);


                if (date.Month == date1.Month && date.Year == date1.Year)
                {
                    InvalidSCertificateMSG();
                    ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Value);
                    ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Value);
                    return;
                }

                if (date > date1)
                {
                    InvalidDateMSG();
                    ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Value);
                    ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Value);
                    return;
                }
    
                    hdnToDaysDate.Value = ddlPeriodMM.SelectedItem.Text + ',' + ddlPeriodYYYY.SelectedValue;
                    
                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;

                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                   // SessionStore.SaveToCustomStore(Params.COMMON_NAME, hdnToDaysDate.Value);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME, ddlPeriodMM.SelectedItem.Text +  ','  + ddlPeriodYYYY.SelectedItem.Text);
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, ddlPeriodMM.SelectedValue);
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, ddlPeriodYYYY.SelectedValue);
                    if (ChkAllEmp.Checked == true)
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NO3, 0);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(Params.COMMON_NO3,ddlEmp.SelectedValue);
                    }


                    SessionStore.SaveToCustomStore(Params.COMMON_NO4, lblID.Text);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, lblIDName.Text);
                  
                    var prm = new object[8];
                    prm[0] = date;

                    if (ChkAllEmp.Checked == true)
                    {
                        prm[1] = 0; 
                    }
                    else
                    {
                        prm[1] = ddlEmp.SelectedValue;
                    }

                    if (ChkAllArea.Checked == true)
                    {
                        prm[2] = 0;
                    }
                    else
                    {
                        prm[2] = ddlArea.SelectedValue;
                    }

                    if (ChkAllLocation.Checked == true)
                    {
                        prm[3] = 0;
                    }
                    else
                    {
                        prm[3] = ddlLocation.SelectedValue;
                    }

                    if (ChkAllProject.Checked == true)
                    {
                        prm[4] = 0;
                    }
                    else
                    {
                        prm[4] = ddlProject.SelectedValue;
                    }

                    if (ChkAllReligion.Checked == true)
                    {
                        prm[5] = 0;
                    }
                    else
                    {
                        prm[5] = ddlReligion.SelectedValue;
                    }
                    if (ChkAllGender.Checked == true)
                    {
                        prm[6] = 0;
                    }
                    else
                    {
                        prm[6] = ddlGender.SelectedValue;
                    }
                    if (ChkAllStatus.Checked == true)
                    {
                        prm[7] = 0;
                    }
                    else
                    {
                        prm[7] = ddlStatus.SelectedValue;
                    }


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateSalCertificate", prm, "A2ZHRMCUS"));
                    if (result == 0)
                    {
                        
                    }


                    
                  
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptSalaryCertificateReport");

                    Response.Redirect("ReportServer.aspx", false);
             

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }


        }

        

        protected void InvalidEmpMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Record Not Found');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employee's Not Found');", true);
            return;

        }

        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            if (txtEmpID.Text != string.Empty)
            {
                int EmpID = Converter.GetInteger(txtEmpID.Text);
                A2ZEMPLOYEEDTO getDTO = new A2ZEMPLOYEEDTO();
                getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
                if (getDTO.EmployeeID > 0)
                {
                    ddlEmp.SelectedValue = Converter.GetString(getDTO.EmployeeID);
                }
                else 
                {
                    InvalidEmpMSG();
                    txtEmpID.Text = string.Empty;
                    txtEmpID.Focus();
                }
            }
        }

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEmpID.Text  = ddlEmp.SelectedValue;
        }

        protected void ChkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllEmp.Checked == true)
            {

                ddlEmp.SelectedIndex = 0;
                ddlEmp.Enabled = false;
                txtEmpID.Enabled = false;
            }

            else
            {
                ddlEmp.Enabled = true;
                txtEmpID.Enabled = true;
                Empdropdown();
            }

        }
        protected void ChkAllArea_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllArea.Checked == true)
            {
                ddlArea.Enabled = false;
            }
            else
            {
                ddlArea.Enabled = true;
            }
        }

        protected void ChkAllLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllLocation.Checked == true)
            {
                ddlLocation.Enabled = false;
            }
            else
            {
                ddlLocation.Enabled = true;
            }
        }

        protected void ChkAllProject_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllProject.Checked == true)
            {
                ddlProject.Enabled = false;
            }
            else
            {
                ddlProject.Enabled = true;
            }
        }

        protected void ChkAllReligion_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllReligion.Checked == true)
            {
                ddlReligion.Enabled = false;
            }
            else
            {
                ddlReligion.Enabled = true;
            }
        }

        protected void ChkAllGender_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllGender.Checked == true)
            {
                ddlGender.Enabled = false;
            }
            else
            {
                ddlGender.Enabled = true;
            }
        }

        protected void ChkAllStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllStatus.Checked == true)
            {
                ddlStatus.Enabled = false;
            }
            else
            {
                ddlStatus.Enabled = true;
            }
        }
    }
}