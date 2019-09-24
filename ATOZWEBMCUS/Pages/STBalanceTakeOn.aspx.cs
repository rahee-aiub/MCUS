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

namespace ATOZWEBMCUS.Pages
{
    public partial class STBalanceTakeOn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WarehouseDropdown();
                GroupDropdown();

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }


        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                gvDetail();
                gvDetails.Visible = true;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblDate.Text = Converter.GetString(dto.ProcessDate.ToShortDateString());
                if (gvDetails.Visible == true)
                {
                    for (int i = 0; i < gvDetails.Rows.Count; ++i)
                    {
                        TextBox STKItemCode = (TextBox)gvDetails.Rows[i].Cells[0].FindControl("txtItemCode");
                        TextBox STKItemName = (TextBox)gvDetails.Rows[i].Cells[1].FindControl("txtItemName");
                        TextBox STKUnitBalance = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtBalance");
                        TextBox STKUnitAvgCost = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtAvgCost");


                        string InsertExp = "INSERT INTO A2ZSTOPBALANCE (STKTrnDate,STKItemCode,STKItemName,STKItemGroupNo,STKItemCategoryNo,STKUnitQty,STKUnitAvgCost) VALUES ('" + lblDate.Text + "','" + STKItemCode.Text + "','" + STKItemName.Text + "','" + ddlGroup.SelectedValue + "','" + ddlCategory.SelectedValue + "','" + STKUnitBalance.Text + "','" + STKUnitAvgCost.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(InsertExp, "A2ZSTMCUST2018"));

                        //string UpdateExp = "UPDATE A2ZSTMST SET STKUnitSalePrice = " + STSaleUnitPrice.Text + " WHERE STKItemCode = '" + STKItemCode.Text + "'";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(UpdateExp, "A2ZSTMCUS"));

                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert('Data Update Succesfully Done');", true);
                    gvDetail();
                    gvDetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetail()
        {
            string sqlquery4 = "SELECT STKItemCode,STKItemName,STKUnitAvgCost,STKUnitQty FROM A2ZSTMST WHERE STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlCategory.SelectedValue + "'";
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetails, "A2ZSTMCUS");

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
