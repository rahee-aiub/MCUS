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
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class STNonSaleableToSaleable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                GroupDropdown();
                GroupDropdown2();
                txtItemCode.Focus();
            }
        }

        private void LoadNonSaleableItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = 2";
            ddlNonSaleableItem = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNonSaleableItem, "A2ZSTMCUS");
        }

        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }

        private void GroupDropdown2()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup2 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup2, "A2ZSTMCUS");
        }

        private void LoadSaleableItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup2.SelectedValue + "' AND STKSubGroup = 1";
            ddlSaleableItem = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSaleableItem, "A2ZSTMCUS");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                LoadNonSaleableItemDropdown();
            }
            else
            {
                txtItemCode.Text = string.Empty;
                ddlNonSaleableItem.SelectedIndex = 0;
                txtNonSBalQty.Text = string.Empty;
            }
        }

        protected void ddlNonSaleableItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNonSaleableItem.SelectedIndex != 0)
            {
                txtItemCode.Text = ddlNonSaleableItem.SelectedValue;
                CalculateItemQty(2);
            }
        }

        private void CalculateItemQty(int x)
        {
            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm = new object[4];
            if (x == 1)
            {
                prm[0] = txtSItemCode.Text;
            }
            else
            {
                prm[0] = txtItemCode.Text;
            }
            prm[1] = Converter.GetDateToYYYYMMDD(date1);
            prm[2] = lblCashCode.Text;
            prm[3] = 0;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("SpM_STGenerateSingleItemBalance", prm, "A2ZSTMCUS"));
            string sqlquery;
            if (x == 1)
            {
                sqlquery = "SELECT STKUnit,STKUnitQty,STKUnitAvgCost,STKUnitSalePrice FROM A2ZSTMST WHERE STKItemCode = " + ddlSaleableItem.SelectedValue + " AND STKGroup = " + ddlGroup2.SelectedValue + " AND STKSubGroup = " + x + "";
            }
            else
            {
                sqlquery = "SELECT STKUnit,STKUnitQty,STKUnitAvgCost,STKUnitSalePrice FROM A2ZSTMST WHERE STKItemCode = " + ddlNonSaleableItem.SelectedValue + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + x + "";
            }

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZSTMCUS");

            if (dt.Rows.Count > 0)
            {
                if (x == 1)
                {
                    txtSItemBal.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);
                }
                else
                {
                    txtNonSBalQty.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);
                }
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0 || txtItemCode.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Item Code');", true);
                return;
            }

            if (ddlGroup2.SelectedIndex == 0 || txtSItemCode.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Item Code');", true);
                return;
            }
            if(ddlGroup.SelectedValue != ddlGroup2.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not transfer to another group');", true);
                return;
            }

            TransferTransaction();

        }

        protected void TransferTransaction()
        {
            try
            {

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[16];
                prm[0] = txtVchNo.Text;
                prm[1] = lblCashCode.Text;
                prm[2] = 20;
                prm[3] = Converter.GetDateToYYYYMMDD(date1);
                prm[4] = ddlGroup.SelectedValue;
                prm[5] = ddlGroup2.SelectedValue;
                prm[6] = 2;
                prm[7] = 1;
                prm[8] = txtItemCode.Text;
                prm[9] = ddlNonSaleableItem.SelectedItem.Text;
                prm[10] = txtSItemCode.Text;
                prm[11] = ddlSaleableItem.SelectedItem.Text;
                prm[12] = txtTransferQty.Text;
                prm[13] = 3;
                prm[14] = lblID.Text;
                prm[15] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STInternalTransfer", prm, "A2ZSTMCUS"));

                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void ddlGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup2.SelectedIndex != 0)
            {
                LoadSaleableItemDropdown();
            }
            else
            {
                txtSItemCode.Text = string.Empty;
                ddlSaleableItem.SelectedIndex = 0;
                txtSItemBal.Text = string.Empty;
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text != string.Empty)
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKItemName,STKItemCode,STKGroup FROM A2ZSTMST WHERE STKItemCode = '" + txtItemCode.Text + "' AND STKSubGroup = 2", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    ddlGroup.SelectedValue = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    LoadNonSaleableItemDropdown();
                    ddlNonSaleableItem.SelectedValue = Converter.GetString(dt.Rows[0]["STKItemCode"]);
                    CalculateItemQty(2);
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Code');", true);
                    return;
                }
            }
        }

        protected void txtSItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtSItemCode.Text != string.Empty)
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKItemName,STKItemCode,STKGroup FROM A2ZSTMST WHERE STKItemCode = '" + txtSItemCode.Text + "' AND STKSubGroup = 1", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    ddlGroup2.SelectedValue = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    LoadSaleableItemDropdown();
                    ddlSaleableItem.SelectedValue = Converter.GetString(dt.Rows[0]["STKItemCode"]);
                    CalculateItemQty(1);
                }
                else
                {
                    txtSItemCode.Text = string.Empty;
                    txtSItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Code');", true);
                    return;
                }
            }
        }

        protected void ddlSaleableItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSaleableItem.SelectedIndex != 0)
            {
                txtSItemCode.Text = ddlSaleableItem.SelectedValue;
                CalculateItemQty(1);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void txtVchNo_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT UserId,FuncOpt FROM A2ZSTTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZSTMCUS");
            if (dt.Rows.Count > 0)
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher No. Already Exist');", true);
                return;

            }
        }

        protected void txtTransferQty_TextChanged(object sender, EventArgs e)
        {
            int Balance = 0;
            int TrnQty = 0;

            Balance = Converter.GetInteger(txtNonSBalQty.Text);
            TrnQty = Converter.GetInteger(txtTransferQty.Text);
            
            if(TrnQty > Balance)
            {
                txtTransferQty.Text = string.Empty;
                txtTransferQty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item out of stock');", true);
                return;
            }
        }
    }
}
