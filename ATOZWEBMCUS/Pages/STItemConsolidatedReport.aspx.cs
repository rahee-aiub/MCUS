using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;

namespace ATOZWEBMCUS.Pages
{
    public partial class STItemConsolidatedReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                
                WarehouseDropdown();
                GroupDropdown();


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");

                int Month = dt.Month;
                int Year = dt.Year;

                ddlPeriodMM.SelectedValue = Converter.GetString(Month);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(Year);

                hdnToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dt));


                //txtfdate.Text = Converter.GetString(date);
                //txttdate.Text = Converter.GetString(date);

                A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(lblID.Text), "A2ZHKMCUS");

                if (sysobj.IdsCWarehouseflag == false)
                {
                    lblCWarehouseflag.Text = "0";
                }
                else
                {
                    lblCWarehouseflag.Text = "1";
                }


                if (lblCWarehouseflag.Text == "0")
                {
                    ddlWarehouse.Enabled = false;
                    txtWarehouse.Enabled = false;
                    chkWarehouse.Visible = false;
                    chkWarehouse.Checked = false;
                    txtWarehouse.Text = Converter.GetString(lblCashCode.Text);
                    ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);
                }
                else
                {
                    ddlWarehouse.Enabled = false;
                    txtWarehouse.Enabled = false;
                    chkWarehouse.Visible = true;
                    chkWarehouse.Checked = true;
                    txtWarehouse.Text = string.Empty;
                    ddlWarehouse.SelectedIndex = 0;
                }
            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void BtnView_Click(object sender, EventArgs e)
        {

            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Group');", true);
                return;
            }

            if (ddlCategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Category');", true);
                return;
            }


            string dt0 = (ddlPeriodMM.SelectedValue + "/" + 01 + "/" + ddlPeriodYYYY.SelectedValue);
            DateTime date = Converter.GetDateTime(dt0);

            hdnToDaysDate.Text = ddlPeriodMM.SelectedItem.Text + ',' + ddlPeriodYYYY.SelectedValue;

            //DateTime date1 = Converter.GetDateTime(hdnPeriod.Text);

            //if (date > date1)
            //{
            //    InvalidDateMSG();
            //    ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Text);
            //    ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Text);
            //    return;
            //}


            var prm = new object[6];

            if (chkWarehouse.Checked == true)
            {
                prm[0] = 0;
            }
            else
            {
                prm[0] = ddlWarehouse.SelectedValue;
            }

            prm[1] = ddlGroup.SelectedValue;
            prm[2] = ddlCategory.SelectedValue;
            prm[3] = date;
            prm[4] = lblID.Text;
            prm[5] = 0;



            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STGenerateConsolidated", prm, "A2ZSTMCUS"));
            if (result == 0)
            {
                
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

                int user = Converter.GetInteger(lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, user);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlGroup.SelectedItem.Text + " - " + ddlCategory.SelectedItem.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, hdnToDaysDate.Text);


                if (chkWarehouse.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All Warehouse");
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, ddlWarehouse.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlWarehouse.SelectedItem.Text);
                }



                if (ddlCategory.SelectedValue == "1")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkConsolidatedSaleable");
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkConsolidatedNonSaleable");
                }
                

                Response.Redirect("ReportServer.aspx", false);
            }
        }


        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }

        private void SubgroupDropdown()
        {
            string sqlquery = "SELECT SubGrpCode,SubGrpDescription from A2ZSUBGROUP Where GrpCode = '" + ddlGroup.SelectedValue + "'";
            ddlCategory = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCategory, "A2ZSTMCUS");
        }

        protected void WarehouseDropdown()
        {
            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWarehouse, "A2ZGLMCUS");

        }

        protected void chkWarehouse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWarehouse.Checked)
            {
                txtWarehouse.Text = string.Empty;
                ddlWarehouse.SelectedIndex = 0;
                ddlWarehouse.Enabled = false;
                txtWarehouse.Enabled = false;
            }
            else
            {
                ddlWarehouse.Enabled = true;
                txtWarehouse.Enabled = true;
                txtWarehouse.Text = Converter.GetString(lblCashCode.Text);
                ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubgroupDropdown();
        }

        protected void txtWarehouse_TextChanged(object sender, EventArgs e)
        {
            int GLCode = Converter.GetInteger(txtWarehouse.Text);
            A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

            if (getDTO.GLAccNo > 0)
            {
                lblRecType.Text = Converter.GetString(getDTO.GLRecType);
                lblSubHead.Text = Converter.GetString(getDTO.GLSubHead);

                if (lblRecType.Text != "2")
                {
                    txtWarehouse.Text = string.Empty;
                    txtWarehouse.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
                    return;
                }

                if (lblSubHead.Text != "10101000")
                {
                    txtWarehouse.Text = string.Empty;
                    txtWarehouse.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Warehouse Code');", true);
                    return;
                }

                ddlWarehouse.SelectedValue = txtWarehouse.Text;

            }
        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWarehouse.Text = ddlWarehouse.SelectedValue;
        }




    }
}
