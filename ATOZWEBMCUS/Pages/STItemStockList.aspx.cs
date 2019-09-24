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
using DataAccessLayer.DTO.HouseKeeping;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class STItemStockList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CtrlProgFlag.Text = (string)Session["ProgFlag"];
                lblRepFlag.Text = (string)Session["SlblRepFlag"];


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                WareHouseDropdown();

                DivGV.Visible = false;
                BtnBack.Visible = false;
                gvHead1.Visible = false;


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                lblProcDate.Text = Converter.GetString(date);


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
                    chkWarehouse.Visible = false;
                    chkWarehouse.Checked = false;
                    ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);
                }
                else
                {
                    ddlWarehouse.Enabled = false;
                    chkWarehouse.Visible = true;
                    chkWarehouse.Checked = true;
                    ddlWarehouse.SelectedIndex = 0;
                }

                GroupDropdown();


                txtGrpCode.Text = string.Empty;
                txtGrpCode.Focus();

                if (CtrlProgFlag.Text == "1")
                {
                    string RchkWarehouse = (string)Session["SchkWarehouse"];
                    string RddlWarehouse = (string)Session["SddlWarehouse"];
                    string RtxtGrpCode = (string)Session["StxtGrpCode"];
                    string RddlGroup = (string)Session["SddlGroup"];
                    string RddlItemType = (string)Session["SddlItemType"];


                    if (RchkWarehouse == "1")
                    {
                        chkWarehouse.Checked = true;
                    }
                    else
                    {
                        chkWarehouse.Checked = false;
                    }

                    ddlWarehouse.SelectedValue = RddlWarehouse;

                    txtGrpCode.Text = RtxtGrpCode;
                    ddlGroup.SelectedValue = RddlGroup;

                    if (txtGrpCode.Text != string.Empty)
                    {
                        SubgroupDropdown();
                        ddlItemType.SelectedValue = RddlItemType;
                    }


                    if (chkWarehouse.Checked == true && lblRepFlag.Text == "1")
                    {
                        DivGV.Visible = true;
                        gvHeader();
                    }

                }



            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["SlblRepFlag"] = string.Empty;
            Session["SchkWarehouse"] = string.Empty;
            Session["SddlWarehouse"] = string.Empty;
            Session["StxtGrpCode"] = string.Empty;
            Session["SddlGroup"] = string.Empty;
            Session["SddlItemType"] = string.Empty;

        }

        protected void WareHouseDropdown()
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
            ddlItemType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemType, "A2ZSTMCUS");
        }



        protected void gvHeaderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
        }

        protected void gvSubHeaderInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.RowIndex == 0)
            //        e.Row.Style.Add("height", "60px");
            //    e.Row.Style.Add("top", "-1600px");

            //}
        }

        protected void gvHead1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");

            }
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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect(Request.RawUrl);
        }



        protected void SessionStoreValue()
        {
            Session["ProgFlag"] = "1";

            Session["SlblRepFlag"] = "1";

            if (chkWarehouse.Checked == true)
            {
                Session["SchkWarehouse"] = "1";
            }
            else
            {
                Session["SchkWarehouse"] = "0";
            }

            Session["SddlWarehouse"] = ddlWarehouse.SelectedValue;

            Session["StxtGrpCode"] = txtGrpCode.Text;
            Session["SddlGroup"] = ddlGroup.SelectedValue;

            Session["SddlItemType"] = ddlItemType.SelectedValue;


        }



        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[3];
                prm[0] = Converter.GetDateToYYYYMMDD(date1);

                if (chkWarehouse.Checked == true)
                {
                    prm[1] = "0";
                }
                else
                {
                    prm[1] = ddlWarehouse.SelectedValue;
                }

                prm[2] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateAllItemBalance]", prm, "A2ZSTMCUS"));


                if (chkWarehouse.Checked == true && DivGV.Visible == false)
                {
                    DivGV.Visible = true;

                    gvHeader();
                }
                else if (chkWarehouse.Checked == true && gvHeaderInfo.Visible == true)
                {
                    SessionStoreValue();

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                    int grp = Converter.GetInteger(ddlGroup.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, grp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlGroup.SelectedItem.Text);

                    int subgrp = Converter.GetInteger(ddlItemType.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, subgrp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlItemType.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                    if (chkWarehouse.Checked == true)
                    {
                        int warehouse = Converter.GetInteger(0);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, warehouse);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All Warehouse");
                    }
                    else
                    {
                        int warehouse = Converter.GetInteger(ddlWarehouse.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, warehouse);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlWarehouse.SelectedItem.Text);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkItemList");
                    Response.Redirect("ReportServer.aspx", false);
                }
                else if (chkWarehouse.Checked == true && gvSubHeaderInfo.Visible == true)
                {
                    SessionStoreValue();

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                    int grp = Converter.GetInteger(ddlGroup.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, grp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlGroup.SelectedItem.Text);

                    int subgrp = Converter.GetInteger(ddlItemType.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, subgrp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlItemType.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

                    int ItemCode = Converter.GetInteger(CtrlItemCode.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, ItemCode);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, CtrlItemCodeName.Text);


                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkItemListByWarehouse");
                    Response.Redirect("ReportServer.aspx", false);
                }
                else
                {
                    SessionStoreValue();

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcDate.Text));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                    int grp = Converter.GetInteger(ddlGroup.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, grp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlGroup.SelectedItem.Text);

                    int subgrp = Converter.GetInteger(ddlItemType.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, subgrp);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlItemType.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                    if (chkWarehouse.Checked == true)
                    {
                        int warehouse = Converter.GetInteger(0);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, warehouse);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All Warehouse");
                    }
                    else
                    {
                        int warehouse = Converter.GetInteger(ddlWarehouse.SelectedValue);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, warehouse);
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlWarehouse.SelectedItem.Text);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkItemList");
                    Response.Redirect("ReportServer.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        protected void chkWarehouse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWarehouse.Checked == true)
            {
                ddlWarehouse.Enabled = false;
                chkWarehouse.Checked = true;
                ddlWarehouse.SelectedIndex = 0;
            }
            else
            {
                ddlWarehouse.Enabled = true;
                ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);
            }

        }

        private void gvHead1Detail()
        {
            try
            {
                string sqlquery3 = "SELECT STKItemCode,STKItemName,STKUnitQty,STKTPUnitQty,STKUnitAvgCost,CalUnitCost,(STKUnitQty + STKTPUnitQty) AS TotalQty  FROM A2ZSTMST WHERE STKItemCode = '" + CtrlItemCode.Text + "'";
                gvHead1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHead1, "A2ZSTMCUS");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvHeader()
        {

            string sqlquery3 = "SELECT STKItemCode,STKItemName,STKUnitQty,STKTPUnitQty,STKUnitAvgCost,CalUnitCost,(STKUnitQty + STKTPUnitQty) AS TotalQty FROM A2ZSTMST WHERE STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlItemType.SelectedValue + "'";
            gvHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvHeaderInfo, "A2ZSTMCUS");

        }


        protected void gvSubHeader()
        {

            string sqlquery3 = "SELECT STKWareHouse,STKWareHouseName,STKUnitQty,STKTPUnitQty,STKUnitAvgCost,STKUnitCost,(STKUnitQty + STKTPUnitQty) AS TotalQty FROM WFDTLSTMST WHERE STKUnitQty > 0 OR STKTPUnitQty > 0 ORDER BY STKWareHouse ASC;";
            gvSubHeaderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSubHeaderInfo, "A2ZSTMCUS");

        }
        protected void BtnHdrSelect_Click(object sender, EventArgs e)
        {
            try
            {
                BtnBack.Visible = true;

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lItemCode = (Label)gvHeaderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblItemCode");
                Label lItemCodeName = (Label)gvHeaderInfo.Rows[r.RowIndex].Cells[1].FindControl("lblItemName");

                CtrlItemCode.Text = lItemCode.Text;
                CtrlItemCodeName.Text = lItemCodeName.Text;


                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[3];
                prm[0] = lItemCode.Text;
                prm[1] = Converter.GetDateToYYYYMMDD(date1);
                prm[2] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateSingleItemBalanceAll]", prm, "A2ZSTMCUS"));

                DivGV.Visible = true;

                gvHeaderInfo.Visible = false;
                gvSubHeaderInfo.Visible = true;



                gvSubHeader();

                gvHead1.Visible = true;
                gvHead1Detail();



                //lblGLHeadNumber.Text = Converter.GetString(lGLAccNo.Text);
                //lblGLHead.Text = lblGLHeadNumber.Text.Substring(0, 3);

                //BtnBack.Visible = true;

                //gvHeaderInfo.Visible = false;
                //gvSubHeaderInfo.Visible = true;
                //gvSubHeaderDtlInfo.Visible = false;


                //gvSubHeader();


                //gvHead1.Visible = true;
                //gvHead2.Visible = false;
                //gvHead1Detail();


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSubHeaderInfo.Visible == true)
                {
                    BtnBack.Visible = false;

                    gvHead1.Visible = false;


                    gvHeaderInfo.Visible = true;
                    gvSubHeaderInfo.Visible = false;

                    gvHeader();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
