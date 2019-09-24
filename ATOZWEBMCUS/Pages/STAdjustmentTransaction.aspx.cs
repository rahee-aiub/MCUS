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
    public partial class STAdjustmentTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                GroupDropdown();
            }
        }

        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }

        protected void SubGroupDropdown()
        {
            string sqquery = @"SELECT SubGrpCode,SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode='" + ddlGroup.SelectedValue + "' OR GrpCode='0'";
            ddlCategory = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlCategory, "A2ZSTMCUS");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                SubGroupDropdown();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

    

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if(ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Item Group');", true);
                return;
            }

            if(ddlCategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Item Sub Group');", true);
                return;
            }
            try
            {
                if (txtItemCode.Text != string.Empty)
                {
                    A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                    string date1 = dt2.ToString("dd/MM/yyyy");

                    var prm = new object[4];
                    prm[0] = txtItemCode.Text;
                    prm[1] = Converter.GetDateToYYYYMMDD(date1);
                    prm[2] = lblCashCode.Text;
                    prm[3] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateSingleItemBalance]", prm, "A2ZSTMCUS"));

                    ddlItemName.SelectedValue = txtItemCode.Text;
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT A2ZUNITCODE.UnitDesc, A2ZSTMST.STKUnit, A2ZSTMST.STKUnitQty FROM A2ZUNITCODE INNER JOIN A2ZSTMST ON A2ZUNITCODE.UnitNo = A2ZSTMST.STKUnit WHERE STKItemCode = " + txtItemCode.Text + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + ddlCategory.SelectedValue + "", "A2ZSTMCUS");

                    if (dt.Rows.Count > 0)
                    {
                        txtBalanceQty.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);
                        lblBalUnit.Text = Converter.GetString(dt.Rows[0]["UnitDesc"]);
                        lblBalUnit2.Text = Converter.GetString(dt.Rows[0]["UnitDesc"]);
                    }
                }
            }
            catch (Exception ex)
            {
                txtItemCode.Text = string.Empty;
                txtItemCode.Focus();
                ddlItemName.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Item Code');", true);
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Voucher No.');", true);
                return;
            }

            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group');", true);
                return;
            }

            if (ddlCategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Sub Group');", true);
                return;
            }

            if (ddlItemName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select stock Item');", true);
                return;
            }

            //if (ddlTrnType.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Transaction Type');", true);
            //    return;
            //}

            if (txtQty.Text == string.Empty || txtQty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter item Quantity');", true);
                return;
            }

            try
            {
                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[12];
                prm[0] = txtVchNo.Text;
                prm[1] = lblCashCode.Text;
                prm[2] = 50;
                prm[3] = Converter.GetDateToYYYYMMDD(date1);
                prm[4] = ddlGroup.SelectedValue;              
                prm[5] = ddlCategory.SelectedValue;             
                prm[6] = txtItemCode.Text;
                prm[7] = ddlItemName.SelectedItem.Text;
                prm[8] = txtQty.Text;
                prm[9] = ddlTrnType.SelectedValue;
                prm[10] = lblID.Text;
                prm[11] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STAdjustmentTransaction", prm, "A2ZSTMCUS"));

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


        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex != 0)
            {

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[4];
                prm[0] = ddlItemName.SelectedValue;
                prm[1] = Converter.GetDateToYYYYMMDD(date1);
                prm[2] = lblCashCode.Text;
                prm[3] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateSingleItemBalance]", prm, "A2ZSTMCUS"));

                txtItemCode.Text = ddlItemName.SelectedValue;
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT A2ZUNITCODE.UnitDesc, A2ZSTMST.STKUnit, A2ZSTMST.STKUnitQty FROM A2ZUNITCODE INNER JOIN A2ZSTMST ON A2ZUNITCODE.UnitNo = A2ZSTMST.STKUnit WHERE STKItemCode = " + ddlItemName.SelectedValue + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + ddlCategory.SelectedValue + "", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    txtBalanceQty.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);
                    lblBalUnit.Text = Converter.GetString(dt.Rows[0]["UnitDesc"]);
                    lblBalUnit2.Text = Converter.GetString(dt.Rows[0]["UnitDesc"]);
                }

            }
            else
            {
                txtItemCode.Text = string.Empty;
                txtItemCode.Focus();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                ItemDropdown();
            }
        }

        protected void ItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlCategory.SelectedValue + "'";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZSTMCUS");

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

      
    }
}
