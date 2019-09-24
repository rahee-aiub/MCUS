using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;


namespace ATOZWEBMCUS.Pages
{
    public partial class HKAreaInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {        
                  ddlDivisionDropdown();
                  
                  ChkAllDivision.Checked = true;
                  ChkAllDistrict.Checked = true;
                  ChkAllUpazila.Checked =true;
                  ChkAllThana.Checked = true;

                  ddlDivision.Enabled = false;
                  ddlDistrict.Enabled = false;
                  ddlUpazila.Enabled = false;
                  ddlThana.Enabled = false;



                }
   

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }


        }

        private void ddlDivisionDropdown()
        {
          
            string sqlquery = "SELECT DiviOrgCode,DiviDescription from A2ZDIVISION WHERE DIVIOrgCODE > 0 ";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlDivision, "A2ZHKMCUS");

        }

        private void ddlDistrictDropdown()
        {

            string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT WHERE DISTOrgCODE > 0 AND DiviOrgCode='" + ddlDivision.SelectedValue + "' ";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlDistrict, "A2ZHKMCUS");

        }


        private void ddlUpazilaDropdown()
        {

            string sqlquery = "SELECT UpzilaOrgCode,UpzilaDescription from A2ZUPZILA WHERE UpzilaOrgCode > 0 AND DiviOrgCode='" + ddlDivision.SelectedValue + "' AND DistOrgCode ='"+ ddlDistrict.SelectedValue +"' ";
            ddlUpazila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlUpazila, "A2ZHKMCUS");

        }

        private void ddlThanaDropdown()
        {

            string sqlquery = "SELECT ThanaOrgCode,ThanaDescription from A2ZTHANA WHERE UpzilaOrgCode > 0 AND DiviOrgCode='" + ddlDivision.SelectedValue + "' AND DistOrgCode ='" + ddlDistrict.SelectedValue + "' AND UpzilaOrgCode ='" + ddlUpazila.SelectedValue + "' ";
            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlThana, "A2ZHKMCUS");

        }
        protected void BtnView_Click(object sender, EventArgs e)
        {

            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");

                
                if ( ChkAllDivision.Checked == false && ddlDivision.SelectedValue == "-Select-")
                {
                   // String csname1 = "PopupScript";
                   //Type cstype = GetType();
                   // ClientScriptManager cs = Page.ClientScript;

                   //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                   // {
                   //     String cstext1 = "alert('Please Mark  All Division Check Box / Select Division Code' );";
                   //     cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                   //}

                   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark  All Division Check Box / Select Division Code');", true);
                    return;
                   }


                if (ChkAllDistrict.Checked == false && ddlDistrict.SelectedIndex == 0)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Mark  All District Check Box / Select District Code' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark  All District Check Box / Select District Code');", true);
                    return;
                }


                if (ChkAllUpazila.Checked == false && ddlUpazila.SelectedIndex == 0)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Mark  All Upazila Check Box / Select Upazila Code' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark  All Upazila Check Box / Select Upazila Code');", true);
                    return;
                }

                if (ChkAllThana.Checked == false && ddlThana.SelectedIndex == 0)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Mark  All Thana Check Box / Select Thana Code' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark  All Thana Check Box / Select Thana Code');", true);
                    return;
                }



                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                             
                
                if (ChkAllDivision.Checked)
                     {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 99);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                     }
                   else
                    {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlDivision.SelectedValue);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlDivision.SelectedItem.Text);
                     }

                  if (ChkAllDistrict.Checked)
                  {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 99);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2,"All");

                  }

                  else
                  {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlDistrict.SelectedValue);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlDistrict.SelectedItem.Text);
                  }
               if (ChkAllUpazila.Checked)
                     {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, 99);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All");
                     }
                else
                  {
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, 0);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, ddlUpazila.SelectedValue);
                      SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlUpazila.SelectedItem.Text);
                  }
               if (ChkAllThana.Checked)
               {
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, 99);
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "All");
               }
               else
               {
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO8, 0);
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, ddlThana.SelectedValue);
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlThana.SelectedItem.Text);
               }
                  
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHKMCUS");
              
                   SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptHkAreaInformationReport");
             
                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }



        }

        protected void ChkAllArea_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 ddlDistrictDropdown();

                
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
                ddlUpazilaDropdown();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAcType_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        

        protected void ddlUpazila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlUpazila_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlThana_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }
        

        protected void ChkAllDivision_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllDivision.Checked == false)
            {
                ddlDivision.Enabled = true;
            }
            else 
            {
                ddlDivision.Enabled = false;
            }
        }

        protected void ChkAllDistrict_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllDistrict.Checked == false)
            {
                ddlDistrict.Enabled = true;
            }
            else
            {
                ddlDistrict.Enabled = false;
            }
        }

        protected void ChkAllUpazila_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllUpazila.Checked == false)
            {
                ddlUpazila.Enabled = true;
            }
            else
            {
                ddlUpazila.Enabled = false;
            }
        }

        protected void ChkAllThana_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllThana.Checked == false)
            {
                ddlThana.Enabled = true;
            }
            else
            {
                ddlThana.Enabled = false;
            }
        }

        



       
    }
}