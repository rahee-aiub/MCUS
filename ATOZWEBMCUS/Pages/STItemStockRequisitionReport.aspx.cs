using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
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
    public partial class STItemStockRequisitionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string SalDate = dt.ToString("dd/MM/yyyy");
                lblProcDate.Text = SalDate;

                //dt = dt.AddMonths(-1);
                //string date = dt.ToString("dd/MM/yyyy");
                //lblProcDate.Text = date;



                FromWareHouseDropdown();
                ddlWareHouse.SelectedValue = Converter.GetString(lblCashCode.Text);

                GroupTypeDropdown();



            }
        }

        protected void FromWareHouseDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlWareHouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWareHouse, "A2ZGLMCUS");

        }
        private void GroupTypeDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroupType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroupType, "A2ZSTMCUS");
        }

        private void CategoryDropdown()
        {
            string sqlquery = "SELECT SubGrpCode,SubGrpDescription from A2ZSUBGROUP Where GrpCode = '" + ddlGroupType.SelectedValue + "'";
            ddlCategory = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCategory, "A2ZSTMCUS");
        }


        //private void ItemNoDropdown()
        //{
        //    string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlItemType.SelectedValue + "'";
        //    ddlItemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemNo, "A2ZSTMCUS");
        //}




        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

       

       

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));

            int code1 = Converter.GetInteger(txtReqNo.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, code1);

            int code2 = Converter.GetInteger(ddlWareHouse.SelectedValue);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, code2);

            int code3 = Converter.GetInteger(ddlGroupType.SelectedValue);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, code3);

            int code4 = Converter.GetInteger(ddlCategory.SelectedValue);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, code4);




            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptSTItemStockRequsitionList");

            Response.Redirect("ReportServer.aspx", false);


        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvDetails.Visible = false;
        }

        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryDropdown();
        }

        protected void txtWareHouse_TextChanged(object sender, EventArgs e)
        {
            int GLCode = Converter.GetInteger(txtWareHouse.Text);
            A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

            if (getDTO.GLAccNo > 0)
            {
                lblRecType.Text = Converter.GetString(getDTO.GLRecType);
                lblSubHead.Text = Converter.GetString(getDTO.GLSubHead);

                if (lblRecType.Text != "2")
                {
                    txtWareHouse.Text = string.Empty;
                    txtWareHouse.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
                    return;
                }

                if (lblSubHead.Text != "10101000")
                {
                    txtWareHouse.Text = string.Empty;
                    txtWareHouse.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Warehouse Code');", true);
                    return;
                }

                ddlWareHouse.SelectedValue = txtWareHouse.Text;

            }
        }

               


    }
}