﻿using ATOZWEBMCUS.WebSessionStore;
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
    public partial class HREmployeeIncomeTaxRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bankdropdown();
                //Areadropdown();
                //Locationdropdown();


                //ChkAllArea.Visible = true;
                //ddlArea.Enabled = false;
                //lblArea.Enabled = false;

                //ChkAllLocation.Visible = true;
                //ddlLocation.Enabled = false;
                //lblLocation.Enabled = false;


                A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                hdnPeriod.Text = Converter.GetString(dt);

                int Month = dt.Month;
                int Year = dt.Year;

                ddlPeriodMM.SelectedValue = Converter.GetString(Month);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(Year);

                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);



            }

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


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        //private void Areadropdown()
        //{
        //    string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT";
        //    ddlArea = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlArea, "A2ZHKMCUS");

        //}

        //private void Locationdropdown()
        //{
        //    string sqlquery = "SELECT AreaCode,AreaDescription from A2ZAREA";
        //    ddlLocation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLocation, "A2ZHRMCUS");
        //}
        //private void Bankdropdown()
        //{
        //    string sqlquery = "SELECT BankCode,BankName from A2ZBANK";
        //    ddlBank = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlBank, "A2ZHRMCUS");
        //}


        protected void BtnView_Click(object sender, EventArgs e)
        {

            string dt = (ddlPeriodMM.SelectedValue + "/" + 01 + "/" + ddlPeriodYYYY.SelectedValue);
            DateTime fdate = Converter.GetDateTime(dt);

            DateTime date1 = Converter.GetDateTime(hdnPeriod.Text);

            if (fdate > date1)
            {
                InvalidDateMSG();
                ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Text);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Text);
                return;
            }


            hdnToDaysDate.Text = ddlPeriodMM.SelectedItem.Text + ',' + ddlPeriodYYYY.SelectedValue;


            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

          

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, hdnToDaysDate.Text);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptEmplyeeIncomeTaxRegister");
            Response.Redirect("ReportServer.aspx", false);

        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        //protected void ChkAllArea_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ChkAllArea.Checked == true)
        //    {
        //        ddlArea.Enabled = false;
        //    }
        //    else
        //    {
        //        ddlArea.Enabled = true;
        //    }
        //}

        //protected void ChkAllLocation_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ChkAllLocation.Checked == true)
        //    {
        //        ddlLocation.Enabled = false;
        //    }
        //    else
        //    {
        //        ddlLocation.Enabled = true;
        //    }
        //}

    }
}