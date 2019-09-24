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

namespace ATOZWEBMCUS.Pages
{
    public partial class STItemStatementReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                GroupDropdown();
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
                    ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);
                }
                else
                {
                    ddlWarehouse.Enabled = false;
                    ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);

                    
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

            if (txtItemCode.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Item Code');", true);
                return;
            }


            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlWarehouse.SelectedValue);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlWarehouse.SelectedItem.Text);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, txtItemCode.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlItemName.SelectedItem.Text);
           

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMSTItemStatement");

            Response.Redirect("ReportServer.aspx", false);
        }

        protected void WarehouseDropdown()
        {
            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWarehouse, "A2ZGLMCUS");
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

        private void ItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlCategory.SelectedValue + "'";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZSTMCUS");
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text != string.Empty)
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKItemName,STKItemCode,STKGroup,STKSubGroup FROM A2ZSTMST WHERE STKItemCode = '" + txtItemCode.Text + "'", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    ddlGroup.SelectedValue = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    SubgroupDropdown();
                    ddlCategory.SelectedValue = Converter.GetString(dt.Rows[0]["STKSubGroup"]);
                    ItemDropdown();
                    ddlItemName.SelectedValue = Converter.GetString(dt.Rows[0]["STKItemCode"]);
                }
                else
                {
                    ddlItemName.SelectedIndex = 0;
                    txtItemCode.Text = string.Empty;
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Code');", true);
                    return;
                }
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex != 0)
            {
                txtItemCode.Text = ddlItemName.SelectedValue;
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                ItemDropdown();
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                SubgroupDropdown();
            }
        }
    }
}
