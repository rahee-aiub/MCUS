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
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;

namespace ATOZWEBMCUS.Pages
{
    public partial class STRcvPurchaseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                ddlCategory.Enabled = false;
                ddlGroup.Enabled = false;
                ddlSupplier.Enabled = false;
                ddlTrnType.Enabled = false;
                txtVchNo.Enabled = false;

                WarehouseDropdown();

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtfdate.Text = Converter.GetString(date);
                txttdate.Text = Converter.GetString(date);


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
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 1);


            if (chkTrnType.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_TYPE, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MU_TRN_TYPE, ddlTrnType.SelectedValue);
            }


            if (txtVchNo.Text == string.Empty)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
            }


            if (chkWarehouse.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All Warehouse");
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlWarehouse.SelectedValue);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlWarehouse.SelectedItem.Text);
            }

            if (chkGroup.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlGroup.SelectedValue);
            }

            if (chkCategory.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, ddlCategory.SelectedValue);
            }

            if (chkSupplier.Checked == true)
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
            }
            else
            {
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, ddlSupplier.SelectedValue);
            }


            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

            
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMSTRcvPurchaseReport");
            

            Response.Redirect("ReportServer.aspx", false);
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
        private void SupplierDropdown()
        {
            string sqlquery = "SELECT SuppCode,SuppName from A2ZSUPPLIER";
            ddlSupplier = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSupplier, "A2ZSTMCUS");
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
                txtWarehouse .Text = Converter.GetString(lblCashCode.Text);
                ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);   
            }
        }

        protected void chkGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGroup.Checked)
            {
                ddlGroup.SelectedIndex = 0;
                ddlGroup.Enabled = false;
            }
            else
            {
                ddlGroup.Enabled = true;
                GroupDropdown();
            }
        }

        protected void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCategory.Checked)
            {
                ddlCategory.SelectedIndex = 0;
                ddlCategory.Enabled = false;
            }
            else
            {
                if(ddlGroup.SelectedIndex != 0)
                {
                    ddlCategory.Enabled = true;
                    SubgroupDropdown();
                }
                else
                {
                    chkCategory.Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group Dropdown');", true);
                    return;
                }
             
            }
        }

        protected void chkSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSupplier.Checked)
            {
                ddlSupplier.SelectedIndex = 0;
                ddlSupplier.Enabled = false;
            }
            else
            {
                ddlSupplier.Enabled = true;
                SupplierDropdown();
            }
        }

        protected void chkTrnType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTrnType.Checked)
            {
                ddlTrnType.SelectedIndex = 0;
                ddlTrnType.Enabled = false;
            }
            else
            {
                ddlTrnType.Enabled = true;               
            }
        }

        protected void chkVchNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVchNo.Checked)
            {               
                txtVchNo.Enabled = false;
            }
            else
            {
                txtVchNo.Enabled = true;

            }
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
