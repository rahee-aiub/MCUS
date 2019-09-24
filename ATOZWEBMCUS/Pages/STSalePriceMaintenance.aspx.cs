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
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class STSalePriceMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GroupDropdown();
            }

        }
        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
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

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm = new object[3];
            prm[0] = Converter.GetDateToYYYYMMDD(date1);


            prm[1] = "0";


            prm[2] = 0;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateAllItemBalance]", prm, "A2ZSTMCUS"));





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
                if (gvDetails.Visible == true)
                {
                    for (int i = 0; i < gvDetails.Rows.Count; ++i)
                    {
                        TextBox STKItemCode = (TextBox)gvDetails.Rows[i].Cells[0].FindControl("txtItemCode");
                        TextBox STKItemName = (TextBox)gvDetails.Rows[i].Cells[1].FindControl("txtItemName");
                        TextBox STKUnitAvgCost = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtAvgCost");
                        TextBox STSaleUnitPrice = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtSaleCost");

                        if (STSaleUnitPrice.Text == string.Empty)
                        {
                            STSaleUnitPrice.Text = "0";
                        }


                        string UpdateExp = "UPDATE A2ZSTMST SET STKUnitSalePrice = " + STSaleUnitPrice.Text + " WHERE STKItemCode = '" + STKItemCode.Text + "'";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(UpdateExp, "A2ZSTMCUS"));

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



            string sqlquery4 = "SELECT STKItemCode,STKItemName,STKUnitAvgCost,STKUnitSalePrice FROM A2ZSTMST WHERE STKGroup = '" + ddlGroup.SelectedValue + "'";
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetails, "A2ZSTMCUS");

        }
    }
}
