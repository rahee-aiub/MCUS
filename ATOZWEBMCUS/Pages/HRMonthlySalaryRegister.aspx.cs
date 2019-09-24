using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.HumanResource;
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
    public partial class HRMonthlySalaryRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;


                A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                DateTime dt0 = Converter.GetDateTime(dto.ProcessDate);
                hdnPeriod.Text = Converter.GetString(dt0);

                int Month = dt0.Month;
                int Year = dt0.Year;

                ddlPeriodMM.SelectedValue = Converter.GetString(Month);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(Year);

                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);

                hdnToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dt0));

                Areadropdown();
                Locationdropdown();
                Religiondropdown();
                RepColumndropdown();


                if (CtrlProgFlag.Text != "1")
                {

                    ChkAllArea.Visible = false;
                    ddlArea.Visible = false;
                    lblArea.Visible = false;

                    ChkAllLocation.Visible = false;
                    ddlLocation.Visible = false;
                    lblLocation.Visible = false;

                    ChkAllProject.Visible = false;
                    ddlProject.Visible = false;
                    lblProject.Visible = false;
                    ChkAllReligion.Visible = false;
                    ddlReligion.Visible = false;
                    lblReligion.Visible = false;
                    ChkAllGender.Visible = false;
                    ddlGender.Visible = false;
                    lblGender.Visible = false;

                    ChkAllStatus.Visible = false;
                    ddlStatus.Visible = false;
                    lblStatus.Visible = false;

                    ChkAllZero.Visible = false;
                    lblZero.Visible = false;


                    rbtAllowance.Visible = false;
                    rbtDeduction.Visible = false;
                    ddlMode.Visible = false;
                    lblMode.Visible = false;
                    lblRepColumn.Visible = false;
                    ddlRepColumn.Visible = false;
                }
                else
                {
                    string RrbtDetail = (string)Session["SrbtDetail"];
                    if (RrbtDetail == "1")
                    {
                        rbtDetail.Checked = true;
                    }
                    else
                    {
                        rbtDetail.Checked = false;
                    }

                    string RrbtSummary = (string)Session["SrbtSummary"];
                    if (RrbtSummary == "1")
                    {
                        rbtSummary.Checked = true;
                    }
                    else
                    {
                        rbtSummary.Checked = false;
                    }

                    string RrbtBrackUpRepColumn = (string)Session["SrbtBrackUpRepColumn"];
                    if (RrbtBrackUpRepColumn == "1")
                    {
                        rbtBrackUpRepColumn.Checked = true;
                    }
                    else
                    {
                        rbtBrackUpRepColumn.Checked = false;
                    }

                    string RrbtBrackUpDtl = (string)Session["SrbtBrackUpDtl"];
                    if (RrbtBrackUpDtl == "1")
                    {
                        rbtBrackUpDtl.Checked = true;
                    }
                    else
                    {
                        rbtBrackUpDtl.Checked = false;
                    }

                    string RrbtBrackUpSum = (string)Session["SrbtBrackUpSum"];
                    if (RrbtBrackUpSum == "1")
                    {
                        rbtBrackUpSum.Checked = true;
                    }
                    else
                    {
                        rbtBrackUpSum.Checked = false;
                    }

                    //----------------------------------------------------

                    string RChkAllArea = (string)Session["SChkAllArea"];
                    string RddlArea = (string)Session["SddlArea"];
                    if (RChkAllArea == "1")
                    {
                        ChkAllArea.Checked = true;
                        ddlArea.Enabled = false;
                    }
                    else
                    {
                        ChkAllArea.Checked = false;
                        ddlArea.Enabled = true;
                        ddlArea.SelectedValue = RddlArea;
                    }


                    string RChkAllLocation = (string)Session["SChkAllLocation"];
                    string RddlLocation = (string)Session["SddlLocation"];
                    if (RChkAllLocation == "1")
                    {
                        ChkAllLocation.Checked = true;
                        ddlLocation.Enabled = false;
                    }
                    else
                    {
                        ChkAllLocation.Checked = false;
                        ddlLocation.Enabled = true;
                        ddlLocation.SelectedValue = RddlLocation;
                    }


                    string RChkAllProject = (string)Session["SChkAllProject"];
                    string RddlProject = (string)Session["SddlProject"];
                    if (RChkAllProject == "1")
                    {
                        ChkAllProject.Checked = true;
                        ddlProject.Enabled = false;
                    }
                    else
                    {
                        ChkAllProject.Checked = false;
                        ddlProject.Enabled = true;
                        ddlProject.SelectedValue = RddlProject;
                    }


                    string RChkAllReligion = (string)Session["SChkAllReligion"];
                    string RddlReligion = (string)Session["SddlReligion"];
                    if (RChkAllReligion == "1")
                    {
                        ChkAllReligion.Checked = true;
                        ddlReligion.Enabled = false;
                    }
                    else
                    {
                        ChkAllReligion.Checked = false;
                        ddlReligion.Enabled = true;
                        ddlReligion.SelectedValue = RddlReligion;
                    }


                    string RChkAllGender = (string)Session["SChkAllGender"];
                    string RddlGender = (string)Session["SddlGender"];
                    if (RChkAllGender == "1")
                    {
                        ChkAllGender.Checked = true;
                        ddlGender.Enabled = false;
                    }
                    else
                    {
                        ChkAllGender.Checked = false;
                        ddlGender.Enabled = true;
                        ddlGender.SelectedValue = RddlGender;
                    }

                    string RChkAllStatus = (string)Session["SChkAllStatus"];
                    string RddlStatus = (string)Session["SddlStatus"];
                    if (RChkAllStatus == "1")
                    {
                        ChkAllStatus.Checked = true;
                        ddlStatus.Enabled = false;
                    }
                    else
                    {
                        ChkAllStatus.Checked = false;
                        ddlStatus.Enabled = true;
                        ddlStatus.SelectedValue = RddlStatus;
                    }


                    string RddlMode = (string)Session["SddlMode"];
                    string RddlRepColumn = (string)Session["SddlRepColumn"];

                    string RrbtAllowance = (string)Session["SrbtAllowance"];
                    if (RrbtAllowance == "1")
                    {
                        rbtAllowance.Checked = true;
                        Allowancedropdown();
                        lblMode.Text = "Allowance Code";
                    }
                    else
                    {
                        rbtAllowance.Checked = false;
                    }

                    string RrbtDeduction = (string)Session["SrbtDeduction"];
                    if (RrbtDeduction == "1")
                    {
                        rbtDeduction.Checked = true;
                        Deductiondropdown();
                        lblMode.Text = "Deduction Code";
                    }
                    else
                    {
                        rbtDeduction.Checked = false;
                    }


                    string RChkAllZero = (string)Session["SChkAllZero"];
                    if (RChkAllZero == "1")
                    {
                        ChkAllZero.Checked = true;
                    }
                    else
                    {
                        ChkAllZero.Checked = false;
                    }



                    if (rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true)
                    {
                        rbtAllowance.Visible = true;
                        rbtDeduction.Visible = true;
                        ddlMode.Visible = true;
                        lblMode.Visible = true;
                        ddlMode.SelectedValue = RddlMode;
                    }
                    else
                    {
                        rbtAllowance.Visible = false;
                        rbtDeduction.Visible = false;
                        ddlMode.Visible = false;
                        lblMode.Visible = false;
                    }


                    if (rbtBrackUpRepColumn.Checked == true)
                    {
                        lblRepColumn.Visible = true;
                        ddlRepColumn.Visible = true;
                        ddlRepColumn.SelectedValue = RddlRepColumn;
                    }
                    else
                    {
                        lblRepColumn.Visible = false;
                        ddlRepColumn.Visible = false;
                    }


                    if (rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true || rbtBrackUpRepColumn.Checked == true)
                    {
                        ChkAllZero.Visible = true;
                        lblZero.Visible = true;
                    }
                    else
                    {
                        ChkAllZero.Visible = false;
                        lblZero.Visible = false;
                    }


                }

            }

        }


        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["SrbtDetail"] = string.Empty;
            Session["SrbtSummary"] = string.Empty;
            Session["SrbtBrackUpRepColumn"] = string.Empty;
            Session["SrbtBrackUpDtl"] = string.Empty;
            Session["SrbtBrackUpSum"] = string.Empty;

            Session["SChkAllArea"] = string.Empty;
            Session["SddlArea"] = string.Empty;
            Session["SChkAllLocation"] = string.Empty;
            Session["SddlLocation"] = string.Empty;
            Session["SChkAllProject"] = string.Empty;
            Session["SddlProject"] = string.Empty;
            Session["SChkAllReligion"] = string.Empty;
            Session["SddlReligion"] = string.Empty;
            Session["SChkAllGender"] = string.Empty;
            Session["SddlGender"] = string.Empty;
            Session["SChkAllStatus"] = string.Empty;
            Session["SddlStatus"] = string.Empty;

            Session["SddlMode"] = string.Empty;
            Session["SrbtAllowance"] = string.Empty;
            Session["SrbtDeduction"] = string.Empty;

            Session["SddlRepColumn"] = string.Empty;

            Session["SChkAllZero"] = string.Empty;

        }

        private void Areadropdown()
        {
            string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT ORDER BY DistDescription";
            ddlArea = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlArea, "A2ZHKMCUS");

        }

        private void Locationdropdown()
        {
            string sqlquery = "SELECT AreaCode,AreaDescription from A2ZAREA ORDER BY AreaDescription";
            ddlLocation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLocation, "A2ZHRMCUS");
        }


        private void Religiondropdown()
        {
            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZHKMCUS");
        }

        private void Allowancedropdown()
        {
            WFA2ZALLOWANCE();

            string sqlquery = "SELECT Code,Description from WFA2ZALLOWANCE";
            ddlMode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMode, "A2ZHRMCUS");
        }

        private void Deductiondropdown()
        {
            WFA2ZDEDUCTION();

            string sqlquery = "SELECT Code,Description from WFA2ZDEDUCTION";
            ddlMode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMode, "A2ZHRMCUS");
        }

        private void RepColumndropdown()
        {
            WFA2ZREPORTLAYOUT();

            string sqlquery = "SELECT DISTINCT RepColumn,RepColumnName from WFA2ZREPORTLAYOUT";
            ddlRepColumn = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlRepColumn, "A2ZHRMCUS");
        }

        protected void rbtDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDetail.Checked == true)
            {
                ChkAllArea.Visible = true;

                ChkAllLocation.Visible = true;

                ChkAllProject.Visible = true;

                ChkAllReligion.Visible = true;
                ChkAllGender.Visible = true;
                ChkAllStatus.Visible = true;

                ddlArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Visible = true;

                ddlLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Visible = true;

                ddlProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Visible = true;

                ddlReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Visible = true;

                ddlGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Visible = true;

                ddlStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Visible = true;

                rbtAllowance.Visible = false;
                rbtDeduction.Visible = false;
                ddlMode.Visible = false;
                lblMode.Visible = false;

                lblRepColumn.Visible = false;
                ddlRepColumn.Visible = false;

                rbtSummary.Checked = false;
                rbtBrackUpRepColumn.Checked = false;
                rbtBrackUpDtl.Checked = false;
                rbtBrackUpSum.Checked = false;

                ChkAllZero.Visible = false;
                lblZero.Visible = false;
            }
        }

        protected void rbtSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSummary.Checked == true)
            {
                ChkAllArea.Visible = true;
                ChkAllLocation.Visible = true;

                ChkAllProject.Visible = true;
                ChkAllReligion.Visible = true;
                ChkAllGender.Visible = true;
                ChkAllStatus.Visible = true;

                ddlArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Visible = true;

                ddlLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Visible = true;

                ddlProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Visible = true;

                ddlReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Visible = true;

                ddlGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Visible = true;

                ddlStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Visible = true;


                rbtAllowance.Visible = false;
                rbtDeduction.Visible = false;
                ddlMode.Visible = false;
                lblMode.Visible = false;

                lblRepColumn.Visible = false;
                ddlRepColumn.Visible = false;

                rbtDetail.Checked = false;
                rbtBrackUpRepColumn.Checked = false;
                rbtBrackUpDtl.Checked = false;
                rbtBrackUpSum.Checked = false;

                ChkAllZero.Visible = false;
                lblZero.Visible = false;
            }
        }

        protected void rbtBrackUpRepColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBrackUpRepColumn.Checked == true)
            {
                ChkAllArea.Visible = true;
                ChkAllLocation.Visible = true;

                ChkAllProject.Visible = true;
                ChkAllReligion.Visible = true;
                ChkAllGender.Visible = true;
                ChkAllStatus.Visible = true;


                ddlArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Visible = true;

                ddlLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Visible = true;

                ddlProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Visible = true;

                ddlReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Visible = true;

                ddlGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Visible = true;

                ddlStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Visible = true;

                rbtAllowance.Visible = false;
                rbtDeduction.Visible = false;
                ddlMode.Visible = false;
                lblMode.Visible = false;

                lblRepColumn.Visible = true;
                ddlRepColumn.Visible = true;

                ChkAllZero.Visible = true;
                lblZero.Visible = true;

                rbtDetail.Checked = false;
                rbtSummary.Checked = false;
                rbtBrackUpDtl.Checked = false;
                rbtBrackUpSum.Checked = false;
            }
        }
        protected void rbtBrackUpDtl_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBrackUpDtl.Checked == true)
            {
                ChkAllArea.Visible = true;
                ChkAllLocation.Visible = true;

                ChkAllProject.Visible = true;
                ChkAllReligion.Visible = true;
                ChkAllGender.Visible = true;
                ChkAllStatus.Visible = true;


                ddlArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Visible = true;

                ddlLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Visible = true;

                ddlProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Visible = true;

                ddlReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Visible = true;

                ddlGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Visible = true;

                ddlStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Visible = true;

                rbtAllowance.Visible = true;
                rbtDeduction.Visible = true;

                ChkAllZero.Visible = true;
                lblZero.Visible = true;

                ddlMode.Visible = false;
                lblMode.Visible = false;

                lblRepColumn.Visible = false;
                ddlRepColumn.Visible = false;

                rbtDetail.Checked = false;
                rbtSummary.Checked = false;
                rbtBrackUpRepColumn.Checked = false;
                rbtBrackUpSum.Checked = false;
            }
        }

        protected void rbtBrackUpSum_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBrackUpSum.Checked == true)
            {
                ChkAllArea.Visible = true;
                ChkAllLocation.Visible = true;

                ChkAllProject.Visible = true;
                ChkAllReligion.Visible = true;
                ChkAllGender.Visible = true;
                ChkAllStatus.Visible = true;

                ddlArea.Visible = true;
                ddlArea.Enabled = false;
                lblArea.Visible = true;

                ddlLocation.Visible = true;
                ddlLocation.Enabled = false;
                lblLocation.Visible = true;

                ddlProject.Visible = true;
                ddlProject.Enabled = false;
                lblProject.Visible = true;

                ddlReligion.Visible = true;
                ddlReligion.Enabled = false;
                lblReligion.Visible = true;

                ddlGender.Visible = true;
                ddlGender.Enabled = false;
                lblGender.Visible = true;

                ddlStatus.Visible = true;
                ddlStatus.Enabled = false;
                lblStatus.Visible = true;

                rbtAllowance.Visible = true;
                rbtDeduction.Visible = true;

                ChkAllZero.Visible = true;
                lblZero.Visible = true;

                ddlMode.Visible = false;
                lblMode.Visible = false;
                rbtDetail.Checked = false;
                rbtSummary.Checked = false;
                rbtBrackUpDtl.Checked = false;
                rbtBrackUpRepColumn.Checked = false;
            }
        }


        protected void rbtAllowance_CheckedChanged(object sender, EventArgs e)
        {
            Allowancedropdown();
            lblMode.Text = "Allowance Code";
            ddlMode.Visible = true;
            lblMode.Visible = true;
            rbtDeduction.Checked = false;
        }

        protected void rbtDeduction_CheckedChanged(object sender, EventArgs e)
        {
            Deductiondropdown();
            lblMode.Text = "Deduction Code";
            ddlMode.Visible = true;
            lblMode.Visible = true;
            rbtAllowance.Checked = false;
        }

        private void InvalidDateMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {

            string dt0 = (ddlPeriodMM.SelectedValue + "/" + 01 + "/" + ddlPeriodYYYY.SelectedValue);
            DateTime date = Converter.GetDateTime(dt0);

            DateTime date1 = Converter.GetDateTime(hdnPeriod.Text);

            if (date > date1)
            {
                InvalidDateMSG();
                ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Text);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Text);
                return;
            }


            hdnToDaysDate.Text = ddlPeriodMM.SelectedItem.Text + ',' + ddlPeriodYYYY.SelectedValue;


            if ((rbtDetail.Checked == true || rbtSummary.Checked == true) && ChkAllArea.Checked == false)
            {
                if (ddlArea.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input District/Area Code');", true);
                    return;
                }
            }

            if ((rbtDetail.Checked == true || rbtSummary.Checked == true) && ChkAllLocation.Checked == false)
            {
                if (ddlLocation.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Posting/Location Code');", true);
                    return;
                }
            }

            if ((rbtDetail.Checked == true || rbtSummary.Checked == true) && ChkAllProject.Checked == false)
            {
                if (ddlProject.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Project Code');", true);
                    return;
                }
            }

            if ((rbtDetail.Checked == true || rbtSummary.Checked == true) && ChkAllReligion.Checked == false)
            {
                if (ddlReligion.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Religion Code');", true);
                    return;
                }
            }

            if ((rbtDetail.Checked == true || rbtSummary.Checked == true) && ChkAllGender.Checked == false)
            {
                if (ddlGender.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Gender Code');", true);
                    return;
                }
            }

            if ((rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true) && rbtAllowance.Checked == false && rbtDeduction.Checked == false)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Select Allowance or Deduction');", true);
                return;

            }

            if ((rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true) && rbtAllowance.Checked == true)
            {
                if (ddlMode.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Allowance Code');", true);
                    return;
                }
            }

            if ((rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true) && rbtDeduction.Checked == true)
            {
                if (ddlMode.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Deduction Code');", true);
                    return;
                }
            }

            if (rbtBrackUpRepColumn.Checked == true)
            {
                if (ddlRepColumn.SelectedValue == "-Select-")
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Input Rep.Column');", true);
                    return;
                }
            }

            //-------------------------------------------------------------------------------

            Session["ProgFlag"] = "1";


            if (rbtDetail.Checked == true)
            {
                Session["SrbtDetail"] = "1";
            }
            else
            {
                Session["SrbtDetail"] = "0";
            }

            if (rbtSummary.Checked == true)
            {
                Session["SrbtSummary"] = "1";
            }
            else
            {
                Session["SrbtSummary"] = "0";
            }

            if (rbtBrackUpRepColumn.Checked == true)
            {
                Session["SrbtBrackUpRepColumn"] = "1";
            }
            else
            {
                Session["SrbtBrackUpRepColumn"] = "0";
            }

            if (rbtBrackUpDtl.Checked == true)
            {
                Session["SrbtBrackUpDtl"] = "1";
            }
            else
            {
                Session["SrbtBrackUpDtl"] = "0";
            }

            if (rbtBrackUpSum.Checked == true)
            {
                Session["SrbtBrackUpSum"] = "1";
            }
            else
            {
                Session["SrbtBrackUpSum"] = "0";
            }

            //------------------------------------------------------------------
            if (ChkAllArea.Checked == true)
            {
                Session["SChkAllArea"] = "1";
            }
            else
            {
                Session["SChkAllArea"] = "0";
            }
            Session["SddlArea"] = ddlArea.SelectedValue;


            if (ChkAllLocation.Checked == true)
            {
                Session["SChkAllLocation"] = "1";
            }
            else
            {
                Session["SChkAllLocation"] = "0";
            }
            Session["SddlLocation"] = ddlLocation.SelectedValue;


            if (ChkAllProject.Checked == true)
            {
                Session["SChkAllProject"] = "1";
            }
            else
            {
                Session["SChkAllProject"] = "0";
            }
            Session["SddlProject"] = ddlProject.SelectedValue;


            if (ChkAllReligion.Checked == true)
            {
                Session["SChkAllReligion"] = "1";
            }
            else
            {
                Session["SChkAllReligion"] = "0";
            }
            Session["SddlReligion"] = ddlReligion.SelectedValue;

            if (ChkAllGender.Checked == true)
            {
                Session["SChkAllGender"] = "1";
            }
            else
            {
                Session["SChkAllGender"] = "0";
            }
            Session["SddlGender"] = ddlGender.SelectedValue;

            if (ChkAllStatus.Checked == true)
            {
                Session["SChkAllStatus"] = "1";
            }
            else
            {
                Session["SChkAllStatus"] = "0";
            }
            Session["SddlStatus"] = ddlStatus.SelectedValue;


            if (rbtAllowance.Checked == true)
            {
                Session["SrbtAllowance"] = "1";
            }
            else
            {
                Session["SrbtAllowance"] = "0";
            }

            if (rbtDeduction.Checked == true)
            {
                Session["SrbtDeduction"] = "1";
            }
            else
            {
                Session["SrbtDeduction"] = "0";
            }

            Session["SddlMode"] = ddlMode.SelectedValue;
            Session["SddlRepColumn"] = ddlRepColumn.SelectedValue;

            if (ChkAllZero.Checked == true)
            {
                Session["SChkAllZero"] = "1";
            }
            else
            {
                Session["SChkAllZero"] = "0";
            }




            //-------------------------------------------------------------------------------


            if (rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true)
            {
                lblDesc1.Text = ddlMode.SelectedItem.Text;
            }
            else
            {


            }


            if (rbtBrackUpDtl.Checked == true || rbtBrackUpSum.Checked == true || rbtBrackUpRepColumn.Checked == true)
            {
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblDesc1.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, hdnToDaysDate.Text);
                if (rbtAllowance.Checked == true)
                {

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                }
                else

                //if (rbtDeduction.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 2);
                }

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");
            }
            else
            {
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, hdnToDaysDate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlArea.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");
            }


            if (rbtBrackUpRepColumn.Checked == true)
            {

                var prm = new object[9];

                prm[0] = date;

                prm[1] = ddlRepColumn.SelectedValue;

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

                if (ChkAllZero.Checked == true)
                {
                    prm[8] = 0;
                }
                else
                {
                    prm[8] = 1;
                }


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateMonthlyLayoutBrackUp", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    FINDCOLUMNREC();

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlRepColumn.SelectedItem.Text);
                    


                    if (CtrlColumnRec.Text == "1")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport1");
                    }
                    else if (CtrlColumnRec.Text == "2")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport2");
                    }
                    else if (CtrlColumnRec.Text == "3")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport3");
                    }
                    else if (CtrlColumnRec.Text == "4")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport4");
                    }
                    else if (CtrlColumnRec.Text == "5")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport5");
                    }
                    else if (CtrlColumnRec.Text == "6")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryLayoutBrackUpReport6");
                    }
                    Response.Redirect("ReportServer.aspx", false);

                }
            }



            if (rbtBrackUpDtl.Checked == true)
            {

                var prm = new object[10];

                prm[0] = date;

                if (rbtAllowance.Checked == true)
                {
                    prm[1] = 1;
                }
                else
                {
                    prm[1] = 2;
                }
                prm[2] = ddlMode.SelectedValue;

                if (ChkAllArea.Checked == true)
                {
                    prm[3] = 0;
                }
                else
                {
                    prm[3] = ddlArea.SelectedValue;
                }

                if (ChkAllLocation.Checked == true)
                {
                    prm[4] = 0;
                }
                else
                {
                    prm[4] = ddlLocation.SelectedValue;
                }

                if (ChkAllProject.Checked == true)
                {
                    prm[5] = 0;
                }
                else
                {
                    prm[5] = ddlProject.SelectedValue;
                }

                if (ChkAllReligion.Checked == true)
                {
                    prm[6] = 0;
                }
                else
                {
                    prm[6] = ddlReligion.SelectedValue;
                }
                if (ChkAllGender.Checked == true)
                {
                    prm[7] = 0;
                }
                else
                {
                    prm[7] = ddlGender.SelectedValue;
                }
                if (ChkAllStatus.Checked == true)
                {
                    prm[8] = 0;
                }
                else
                {
                    prm[8] = ddlStatus.SelectedValue;
                }

                if (ChkAllZero.Checked == true)
                {
                    prm[9] = 0;
                }
                else
                {
                    prm[9] = 1;
                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateMonthlyBrackUp", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryBrackUpReport");
                    // SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");
                    Response.Redirect("ReportServer.aspx", false);
                }
            }

            if (rbtDetail.Checked == true)
            {
                var prm = new object[7];

                prm[0] = date;

                if (ChkAllArea.Checked == true)
                {
                    prm[1] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All");
                }
                else
                {
                    prm[1] = ddlArea.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlArea.SelectedItem.Text);
                }

                if (ChkAllLocation.Checked == true)
                {
                    prm[2] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All");
                }
                else
                {
                    prm[2] = ddlLocation.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlLocation.SelectedItem.Text);
                }

                if (ChkAllProject.Checked == true)
                {
                    prm[3] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "All");
                }
                else
                {
                    prm[3] = ddlProject.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlProject.SelectedItem.Text);
                }

                if (ChkAllReligion.Checked == true)
                {
                    prm[4] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, "All");
                }
                else
                {
                    prm[4] = ddlReligion.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME5, ddlReligion.SelectedItem.Text);
                }
                if (ChkAllGender.Checked == true)
                {
                    prm[5] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, "All");
                }
                else
                {
                    prm[5] = ddlGender.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME6, ddlGender.SelectedItem.Text);
                }
                if (ChkAllStatus.Checked == true)
                {
                    prm[6] = 0;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME7, "All");   
                }
                else
                {
                    prm[6] = ddlStatus.SelectedValue;
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME7, ddlStatus.SelectedItem.Text);
                }


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateMonthlyRegister", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryRegisterReport");
                    Response.Redirect("ReportServer.aspx", false);
                }
            }

            if (rbtBrackUpSum.Checked == true)
            {
                var prm = new object[10];

                prm[0] = date;

                if (rbtAllowance.Checked == true)
                {
                    prm[1] = 1;
                }
                else
                {
                    prm[1] = 2;
                }
                prm[2] = ddlMode.SelectedValue;

                if (ChkAllArea.Checked == true)
                {
                    prm[3] = 0;
                }
                else
                {
                    prm[3] = ddlArea.SelectedValue;
                }

                if (ChkAllLocation.Checked == true)
                {
                    prm[4] = 0;
                }
                else
                {
                    prm[4] = ddlLocation.SelectedValue;
                }

                if (ChkAllProject.Checked == true)
                {
                    prm[5] = 0;
                }
                else
                {
                    prm[5] = ddlProject.SelectedValue;
                }

                if (ChkAllReligion.Checked == true)
                {
                    prm[6] = 0;
                }
                else
                {
                    prm[6] = ddlReligion.SelectedValue;
                }
                if (ChkAllGender.Checked == true)
                {
                    prm[7] = 0;
                }
                else
                {
                    prm[7] = ddlGender.SelectedValue;
                }
                if (ChkAllStatus.Checked == true)
                {
                    prm[8] = 0;
                }
                else
                {
                    prm[8] = ddlStatus.SelectedValue;
                }

                if (ChkAllZero.Checked == true)
                {
                    prm[9] = 0;
                }
                else
                {
                    prm[9] = 1;
                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateMonthlySummaryBrackUp", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalaryBrackupSummaryReport");
                    Response.Redirect("ReportServer.aspx", false);
                }
            }


            if (rbtSummary.Checked == true)
            {
                var prm = new object[7];

                prm[0] = date;

                if (ChkAllArea.Checked == true)
                {
                    prm[1] = 0;
                }
                else
                {
                    prm[1] = ddlArea.SelectedValue;
                }

                if (ChkAllLocation.Checked == true)
                {
                    prm[2] = 0;
                }
                else
                {
                    prm[2] = ddlLocation.SelectedValue;
                }

                if (ChkAllProject.Checked == true)
                {
                    prm[3] = 0;
                }
                else
                {
                    prm[3] = ddlProject.SelectedValue;
                }

                if (ChkAllReligion.Checked == true)
                {
                    prm[4] = 0;
                }
                else
                {
                    prm[4] = ddlReligion.SelectedValue;
                }
                if (ChkAllGender.Checked == true)
                {
                    prm[5] = 0;
                }
                else
                {
                    prm[5] = ddlGender.SelectedValue;
                }
                if (ChkAllStatus.Checked == true)
                {
                    prm[6] = 0;
                }
                else
                {
                    prm[6] = ddlStatus.SelectedValue;
                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRCalculateMonthlySummaryRegister", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMonthlySalarySummaryRegisterReport");
                    Response.Redirect("ReportServer.aspx", false);
                }
            }
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
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

        private void WFA2ZALLOWANCE()
        {
            string sqlquery4 = "Truncate table dbo.WFA2ZALLOWANCE";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZHRMCUS"));

            string qry = "SELECT Code,Description from A2ZALLOWANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["Code"].ToString();
                    var Desc = dr["Description"].ToString();
                    string strQuery = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES ('" + code + "','" + Desc + "')";
                    int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }

            string strQry = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES (97,'Gross')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZHRMCUS"));
        }

        private void WFA2ZDEDUCTION()
        {
            string sqlquery4 = "Truncate table dbo.WFA2ZDEDUCTION";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZHRMCUS"));

            string qry = "SELECT Code,Description from A2ZDEDUCTION";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["Code"].ToString();
                    var Desc = dr["Description"].ToString();
                    string strQuery = "INSERT INTO WFA2ZDEDUCTION(Code,Description) VALUES ('" + code + "','" + Desc + "')";
                    int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }

            string strQry = "INSERT INTO WFA2ZDEDUCTION(Code,Description) VALUES (98,'Total Ded.')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZHRMCUS"));

            string sQry = "INSERT INTO WFA2ZDEDUCTION(Code,Description) VALUES (99,'Net Pay')";
            int rEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sQry, "A2ZHRMCUS"));
        }

        private void WFA2ZREPORTLAYOUT()
        {
            string sqlquery4 = "Truncate table dbo.WFA2ZREPORTLAYOUT";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZHRMCUS"));

            string qry = "SELECT RepColumn,RepColumnName from A2ZREPORTLAYOUT";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["RepColumn"].ToString();
                    var Desc = dr["RepColumnName"].ToString();
                    string strQuery = "INSERT INTO WFA2ZREPORTLAYOUT(RepColumn,RepColumnName) VALUES ('" + code + "','" + Desc + "')";
                    int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }

            string strQry = "INSERT INTO WFA2ZREPORTLAYOUT(RepColumn,RepColumnName) VALUES (99,'Gross/TD/Net')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZHRMCUS"));


        }

        private void FINDCOLUMNREC()
        {

            string qry = "SELECT TOP (1) Id,Code1,Code2,Code3,Code4,Code5,Code6 from WFSalaryLayoutBrackUp";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code1 = dr["Code1"].ToString();
                    var code2 = dr["Code2"].ToString();
                    var code3 = dr["Code3"].ToString();
                    var code4 = dr["Code4"].ToString();
                    var code5 = dr["Code5"].ToString();
                    var code6 = dr["Code6"].ToString();

                    if (code6 == "0")
                    {
                        if (code5 == "0")
                        {
                            if (code4 == "0")
                            {
                                if (code3 == "0")
                                {
                                    if (code2 == "0")
                                    {
                                        CtrlColumnRec.Text = "1";
                                    }
                                    else
                                    {
                                        CtrlColumnRec.Text = "2";
                                    }
                                }
                                else
                                {
                                    CtrlColumnRec.Text = "3";
                                }
                            }
                            else
                            {
                                CtrlColumnRec.Text = "4";
                            }
                        }
                        else
                        {
                            CtrlColumnRec.Text = "5";
                        }
                    }
                    else
                    {
                        CtrlColumnRec.Text = "6";
                    }
                }
            }




        }
    }
}