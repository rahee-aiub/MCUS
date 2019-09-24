using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
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
    public partial class STItemStockRequisition : System.Web.UI.Page
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlGroupType.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
            {
                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");

                var prm = new object[3];
                prm[0] = Converter.GetDateToYYYYMMDD(date1);
                prm[1] = ddlWareHouse.SelectedValue;
                prm[2] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateAllItemBalance]", prm, "A2ZSTMCUS"));


                gvDetail();
                gvDetails.Visible = true;
            }
            else
            {
                gvDetails.Visible = false;
            }
        }

        protected void gvDetail()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName,STKUnitQty from A2ZSTMST Where STKGroup = '" + ddlGroupType.SelectedValue + "' AND STKSubGroup = '" + ddlCategory.SelectedValue + "'";
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZSTMCUS");

            for (int i = 0; i < gvDetails.Rows.Count; ++i)
            {
                TextBox ItmCode = (TextBox)gvDetails.Rows[i].Cells[0].FindControl("txtItemCode");
                TextBox ReqQty = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtReqQty");
                TextBox Note = (TextBox)gvDetails.Rows[i].Cells[4].FindControl("txtNote");


                string code = Converter.GetString(ItmCode.Text);

                string sqlquery5 = "SELECT ReqItemCode,ReqReqUnitQty,ReqNote FROM A2ZITEMREQUIRE WHERE ReqWarehouseNo = '" + ddlWareHouse.SelectedValue + "' AND ReqNo = '" + txtReqNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery5, "A2ZSTMCUS");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var ReqCode = dr["ReqItemCode"].ToString();
                        string Rcod = Converter.GetString(ReqCode);

                        var RequnitQty = dr["ReqReqUnitQty"].ToString();
                        string Rqty = Converter.GetString(RequnitQty);

                        var ReqNote = dr["ReqNote"].ToString();
                        string Rnote = Converter.GetString(ReqNote);

                        if (code == Rcod)
                        {
                            ReqQty.Text = Converter.GetString(Rqty);
                            Note.Text = Converter.GetString(Rnote);
                        }

                    }
                }
                else
                {
                    ReqQty.Text = "0";
                    Note.Text = string.Empty;
                }
            }
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



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroupType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Group Type');", true);
                    return;
                }


                if (ddlCategory.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Category');", true);
                    return;
                }

                if (txtReqNo.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Requsition No.');", true);
                    return;
                }



                if (gvDetails.Visible == true)
                {
                    for (int i = 0; i < gvDetails.Rows.Count; ++i)
                    {
                        TextBox ItmCode = (TextBox)gvDetails.Rows[i].Cells[0].FindControl("txtItemCode");
                        TextBox ItmName = (TextBox)gvDetails.Rows[i].Cells[1].FindControl("txtItemName");
                        TextBox QtyBal = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtBalQty");
                        TextBox ReqQty = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtReqQty");
                        TextBox Note = (TextBox)gvDetails.Rows[i].Cells[4].FindControl("txtNote");


                        string Sqr1 = "SELECT ReqNo FROM A2ZITEMREQUIRE WHERE ReqWarehouseNo = '" + ddlWareHouse.SelectedValue + "' AND ReqNo = '" + txtReqNo.Text + "' AND ReqItemCode = '" + ItmCode.Text + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(Sqr1, "A2ZSTMCUS");

                        if (dt.Rows.Count > 0)
                        {
                            string UpdateExp = "UPDATE A2ZITEMREQUIRE SET ReqReqUnitQty = " + Converter.GetInteger(ReqQty.Text) + ", ReqNote = " + Note.Text + " WHERE ReqWarehouseNo = " + ddlWareHouse.SelectedValue + " AND ReqNo = " + txtReqNo.Text + " AND ReqItemCode = " + ItmCode.Text + "";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(UpdateExp, "A2ZSTMCUS"));
                        }
                        else
                        {
                            if (ReqQty.Text != "0.00" && ReqQty.Text != "0")
                            {
                                string InsertExp = "INSERT INTO A2ZITEMREQUIRE (ReqDate,ReqNo,ReqWarehouseNo,ReqWarehouseName,ReqItemGroupNo,ReqItemGroupDesc,ReqItemCategoryNo,ReqItemCategoryDesc,ReqItemCode,ReqItemName,ReqUnitQtyBalance,ReqReqUnitQty,ReqNote,ReqProcStat) VALUES ('" + Converter.GetDateToYYYYMMDD(lblProcDate.Text) + "','" + txtReqNo.Text + "','" + ddlWareHouse.SelectedValue + "','" + ddlWareHouse.SelectedItem.Text + "','" + ddlGroupType.SelectedValue + "','" + ddlGroupType.SelectedItem.Text + "','" + ddlCategory.SelectedValue + "','" + ddlCategory.SelectedItem.Text + "','" + ItmCode.Text + "','" + ItmName.Text + "','" + Converter.GetDecimal(QtyBal.Text) + "','" + Converter.GetDecimal(ReqQty.Text) + "','" + Note.Text + "','" + "1" + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(InsertExp, "A2ZSTMCUS"));
                            }
                        }


                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert('Data Update Succesfully Done');", true);
                    gvDetails.Visible = false;
                    //ddlLocation.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvDetails.Visible = false;
        }

        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryDropdown();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string Sqr1 = "SELECT ReqNo FROM A2ZITEMREQUIRE WHERE ReqWarehouseNo = '" + ddlWareHouse.SelectedValue + "' AND ReqNo = '" + txtReqNo.Text + "' AND ReqProcStat = 2";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(Sqr1, "A2ZSTMCUS");

            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert('Requisition Already Sended');", true);
                return;
            }

            string UpdateExp = "UPDATE A2ZITEMREQUIRE SET ReqProcStat = 2 WHERE ReqWarehouseNo = " + ddlWareHouse.SelectedValue + " AND ReqNo = " + txtReqNo.Text + "";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(UpdateExp, "A2ZSTMCUS"));

            gvDetails.Visible = false;

            ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert('Requisition Send');", true);
            return;

        }

        protected void txtReqNo_TextChanged(object sender, EventArgs e)
        {
            string Sqr1 = "SELECT ReqNo FROM A2ZITEMREQUIRE WHERE ReqWarehouseNo = '" + ddlWareHouse.SelectedValue + "' AND ReqNo = '" + txtReqNo.Text + "' AND ReqProcStat = 2";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(Sqr1, "A2ZSTMCUS");

            if (dt.Rows.Count > 0)
            {
                btnSearch.Visible = false;
                btnUpdate.Visible = false;
                btnSend.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Error", "alert('Requisition Already Sended');", true);
                return;
            }
            else 
            {
                btnSearch.Visible = true;
                btnUpdate.Visible = true;
                btnSend.Visible = true;
            }
        }


    }
}