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
    public partial class STItemStockMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GroupDropdown();

                UnitOfMeasurementDropdown();
                txtGrpCode.Text = string.Empty;
                txtGrpCode.Focus();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
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
            ddlItemType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemType, "A2ZSTMCUS");
        }

        private void ItemNoDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlItemType.SelectedValue + "'";
            ddlItemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemNo, "A2ZSTMCUS");
        }

        private void UnitOfMeasurementDropdown()
        {
            string sqlquery = "SELECT UnitNo,UnitDesc from A2ZUNITCODE";
            ddlUnitMeasure = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUnitMeasure, "A2ZSTMCUS");
        }



        public void clearInfo()
        {
            ddlGroup.SelectedIndex = 0;
            ddlItemType.SelectedIndex = 0;
            txtGrpCode.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            ddlUnitMeasure.SelectedIndex = 0;
            txtReorderLvl.Text = string.Empty;
        }

        protected void txtGrpCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGrpCode.Text != string.Empty)
                {
                    int code = Converter.GetInteger(txtGrpCode.Text);
                    A2ZGROUPDTO getDTO = (A2ZGROUPDTO.GetInformation(code));

                    if (getDTO.GrpCode > 0)
                    {
                        ddlGroup.SelectedValue = Converter.GetString(getDTO.GrpCode);
                        hdnGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                        SubgroupDropdown();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insert Problem');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedValue != "-Select-")
                {
                    //ddlItemType.SelectedIndex = 0;
                    //ddlItemNo.SelectedIndex = 0;
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    ddlUnitMeasure.SelectedIndex = 0;
                    txtItemDesc.Text = string.Empty;
                    txtReorderLvl.Text = string.Empty;

                    SubgroupDropdown();
                    int code = Converter.GetInteger(ddlGroup.SelectedValue);
                    A2ZGROUPDTO getDTO = (A2ZGROUPDTO.GetInformation(code));
                    if (getDTO.GrpCode > 0)
                    {
                        txtGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                        hdnGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                    }
                }
                else
                {
                    txtGrpCode.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue != "-Select-" && ddlItemType.SelectedValue != "-Select-")
            {
                int LR = 0;
                int GrpCode = Converter.GetInteger(ddlGroup.SelectedValue);
                int SubGrpCode = Converter.GetInteger(ddlItemType.SelectedValue);
                A2ZSUBGROUPDTO getDTO = (A2ZSUBGROUPDTO.GetInformation(GrpCode, SubGrpCode));
                if (getDTO.LastRec >= 0)
                {
                    LR = Converter.GetInteger(getDTO.LastRec);
                    LR += 1;
                    hdnNewSTKNo.Text = SubGrpCode.ToString("0") + GrpCode.ToString("00") + LR.ToString("0000");
                }

                A2ZSTOCKDTO objDTO = new A2ZSTOCKDTO();
                objDTO.STKItemCode = Converter.GetInteger(hdnNewSTKNo.Text);
                objDTO.STKItemName = Converter.GetString(txtItemDesc.Text);
                objDTO.STKUnit = Converter.GetInteger(ddlUnitMeasure.SelectedValue);
                objDTO.STKGroup = Converter.GetInteger(ddlGroup.SelectedValue);
                objDTO.STKSubGroup = Converter.GetInteger(ddlItemType.SelectedValue);
                objDTO.STKStatus = 1;
                objDTO.STKStatusDesc = "Active";
                objDTO.STKReOrderLevel = Converter.GetInteger(txtReorderLvl.Text);

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.STKStatusDate = Converter.GetDateTime(dto.ProcessDate.ToLongDateString());

                int roweffect = A2ZSTOCKDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    A2ZSUBGROUPDTO SubGrpDTO = new A2ZSUBGROUPDTO();
                    SubGrpDTO.GrpCode = GrpCode;
                    SubGrpDTO.SubGrpCode = SubGrpCode;
                    SubGrpDTO.LastRec = LR;
                    int roweffect1 = A2ZSUBGROUPDTO.UpdateLastRec(SubGrpDTO);
                    if (roweffect1 > 0)
                    {
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group and Sub Group');", true);
                return;
            }
        }

        private void ItemCodeGenerate()
        {

            string ItemGroup = txtGrpCode.Text;

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0 && ddlItemType.SelectedIndex != 0 && ddlItemNo.SelectedIndex != 0)
            {
                A2ZSTOCKDTO objDTO = new A2ZSTOCKDTO();
                objDTO.STKItemCode = Converter.GetInteger(ddlItemNo.SelectedValue);
                objDTO.STKItemName = Converter.GetString(txtItemDesc.Text);
                objDTO.STKGroup = Converter.GetInteger(ddlGroup.SelectedValue);
                objDTO.STKSubGroup = Converter.GetInteger(ddlItemType.SelectedValue);
                objDTO.STKUnit = Converter.GetInteger(ddlUnitMeasure.SelectedValue);
                objDTO.STKReOrderLevel = Converter.GetInteger(txtReorderLvl.Text);

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.STKStatusDate = Converter.GetDateTime(dto.ProcessDate.ToLongDateString());

                int roweffect = A2ZSTOCKDTO.UpdateInformation(objDTO);
                if (roweffect > 0)
                {
                    clearInfo();
                   
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT STKItemCode,STKItemName FROM A2ZSTMST WHERE STKItemCode != 0";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZSTMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetailInfo.Visible = true;
            gvDetail();
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

    
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void ddlItemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemNo.SelectedIndex == 0)
            {
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;

                txtItemDesc.Text = string.Empty;
                txtReorderLvl.Text = string.Empty;
                ddlUnitMeasure.SelectedIndex = 0;
            }
            else
            {
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSTMST WHERE STKItemCode = " + ddlItemNo.SelectedValue + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + ddlItemType.SelectedValue + "", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    hdnSubGroupCode.Text = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    ddlGroup.SelectedValue = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    ddlItemType.SelectedValue = Converter.GetString(dt.Rows[0]["STKSubGroup"]);
                    txtGrpCode.Text = Converter.GetString(dt.Rows[0]["STKGroup"]);
                    txtItemDesc.Text = Converter.GetString(dt.Rows[0]["STKItemName"]);
                    ddlUnitMeasure.SelectedValue = Converter.GetString(dt.Rows[0]["STKUnit"]);
                    txtReorderLvl.Text = Converter.GetString(dt.Rows[0]["STKReOrderLevel"]);
                    BtnUpdate.Visible = true;
                    BtnSubmit.Visible = false;
                }
                else
                {
                    txtItemDesc.Text = string.Empty;
                    ddlUnitMeasure.SelectedIndex = 0;
                    txtReorderLvl.Text = string.Empty;
                    txtItemDesc.Focus();
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                }
            }

        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0 && ddlItemType.SelectedIndex != 0)
            {
                ItemNoDropdown();
            }

        }




    }
}
