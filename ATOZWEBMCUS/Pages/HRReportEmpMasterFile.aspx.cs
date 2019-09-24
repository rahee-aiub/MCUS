using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRReportEmpMasterFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt0 = Converter.GetDateTime(dto.ProcessDate);
                hdnPeriod.Text = Converter.GetString(dt0);

                int Month = dt0.Month;
                int Year = dt0.Year;


                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);

                hdnToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dt0));

                ChkAllArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Enabled = false;

                ChkAllLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Enabled = false;

                ChkAllProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Enabled = false;
                ChkAllReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Enabled = false;
                ChkAllGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Enabled = false;
                ChkAllDesig.Visible = true;
                ddlDesig.Enabled = false;
                lblDesig.Enabled = false;
                ChkAllServType.Visible = true;
                ddlServType.Enabled = false;
                lblServType.Enabled = false;

                ChkAllStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Enabled = false;

                ChkAllBaseGrade.Visible = true;
                ddlBaseGrade.Enabled = false;
                lblBaseGrade.Enabled = false;

                ChkAllGrade.Visible = false;
                lblGrade.Visible = false;
                txtGrade.Visible = false;

                ChkAllSteps.Visible = false;
                lblSteps.Visible = false;
                txtSteps.Visible = false;


                Areadropdown();
                Locationdropdown();
                Religiondropdown();
                DesigDropdown();
                ServiceTypeDropdown();
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

        private void DesigDropdown()
        {
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlDesig = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlDesig, "A2ZHRMCUS");
        }

        private void ServiceTypeDropdown()
        {
            string sqlquery = "SELECT ServiceType,ServiceName from A2ZSERVICETYPE";
            ddlServType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlServType, "A2ZHRMCUS");
        }

        private void InvalidDateMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {


            if (ChkAllArea.Checked == false)
            {
                if (ddlArea.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Area Code');", true);
                    return;
                }
            }

            if (ChkAllProject.Checked == false)
            {
                if (ddlProject.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Project Code');", true);
                    return;
                }
            }

            if (ChkAllReligion.Checked == false)
            {
                if (ddlReligion.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Religion Code');", true);
                    return;
                }
            }

            if (ChkAllGender.Checked == false)
            {
                if (ddlGender.SelectedValue == "-Select-")
                {
                    
                       ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Gender Code');", true);
                    return;
                }
         }


            if (ChkAllDesig.Checked == false)
            {
                if (ddlDesig.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Designation Code');", true);
                    return;
                }
            }

            if (ChkAllServType.Checked == false)
            {
                if (ddlServType.SelectedValue == "-Select-")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Service Type Code');", true);
                    return;
                }
            }

            if (ChkAllStatus.Checked == false)
            {
                if (ddlStatus.SelectedValue == "-Select-")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Status Code');", true);
                    return;
                }
            }
           
            var prm = new object[11];

            if (ChkAllArea.Checked == true)
            {
                prm[0] = 0;
            }
            else 
            {
                prm[0] = ddlArea.SelectedValue;      
            }


            if (ChkAllLocation.Checked == true)
            {
                prm[1] = 0;
            }
            else
            {
                prm[1] = ddlLocation.SelectedValue;
            }



            if (ChkAllProject.Checked == true)
            {
                prm[2] = 0;
            }
            else 
            {
                prm[2] = ddlProject.SelectedValue;      
            }

            if (ChkAllReligion.Checked == true)
            {
                prm[3] = 0;
            }
            else 
            {
                prm[3] = ddlReligion.SelectedValue;      
            }

            if (ChkAllGender.Checked == true)
            {
                prm[4] = 0;
            }
            else 
            {
                prm[4] = ddlGender.SelectedValue;      
            }

            if (ChkAllDesig.Checked == true)
            {
                prm[5] = 0;
            }
            else 
            {
                prm[5] = ddlDesig.SelectedValue;      
            }

            if (ChkAllServType.Checked == true)
            {
                prm[6] = 0;
            }
            else 
            {
                prm[6] = ddlServType.SelectedValue;      
            }

            if (ChkAllStatus.Checked == true)
            {
                prm[7] = 0;
            }
            else
            {
                prm[7] = ddlStatus.SelectedValue;
            }

            if (ChkAllBaseGrade.Checked == true)
            {
                prm[8] = 0;
            }
            else
            {
                prm[8] = ddlBaseGrade.SelectedValue;
            }

            if (ChkAllGrade.Checked == true || ddlBaseGrade.SelectedValue == "3")
            {
                prm[9] = 0;
            }
            else
            {
                prm[9] = txtGrade.Text;
            }

            if (ChkAllSteps.Checked == true || ddlBaseGrade.SelectedValue == "3")
            {
                prm[10] = 0;
            }
            else
            {
                prm[10] = txtSteps.Text;
            }

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HREmpGeneralInformation", prm, "A2ZHRMCUS"));
            if (result == 0)
            {
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, hdnToDaysDate.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlArea.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptHREmpGeneralInformation");
                Response.Redirect("ReportServer.aspx", false);
            }

        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
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

        protected void ChkAllDesig_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllDesig.Checked == true)
            {
                ddlDesig.Enabled = false;
            }
            else
            {
                ddlDesig.Enabled = true;
            }
        }

        protected void ChkAllServType_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllServType.Checked == true)
            {
                ddlServType.Enabled = false;
            }
            else
            {
                ddlServType.Enabled = true;
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
        protected void ChkAllBaseGrade_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllBaseGrade.Checked == true)
            {
                ddlBaseGrade.Enabled = false;
            }
            else
            {
                ddlBaseGrade.Enabled = true;
            }
        }

        protected void ChkAllGrade_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllGrade.Checked == true)
            {
                txtGrade.Enabled = false;
            }
            else
            {
                txtGrade.Enabled = true;
            }
        }

        protected void ChkAllSteps_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllSteps.Checked == true)
            {
                txtSteps.Enabled = false;
            }
            else
            {
                txtSteps.Enabled = true;
            }
        }
        protected void ddlBaseGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBaseGrade.SelectedValue == "3")
            {
                ChkAllGrade.Visible = false;
                lblGrade.Visible = false;
                txtGrade.Visible = false;
            }
            else 
            {
                ChkAllGrade.Visible = true;
                ChkAllGrade.Enabled = true;
                lblGrade.Visible = true;
                txtGrade.Visible = true;
                txtGrade.Enabled = false;

                ChkAllSteps.Visible = true;
                ChkAllSteps.Enabled = true;
                lblSteps.Visible = true;
                txtSteps.Visible = true;
                txtSteps.Enabled = false;
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
    }
}