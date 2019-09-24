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
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class STIssueSellTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }

                GroupDropdown();
                UnitDropdown();
                GLBankCodeDropdown();

                FromWareHouseDropdown();

                ddlFromWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);

                TrancateWFTRN();

                lblGLBankCode.Visible = false;
                txtGLBankCode.Visible = false;
                ddlGLBankCode.Visible = false;
                lblChqNo.Visible = false;
                txtChqNo.Visible = false;

            }

        }

        protected void GLBankCodeDropdown()
        {
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 and (GLSubHead = 10106000 OR GLSubHead = 20801000) AND Status=1 ORDER BY GLAccDesc ASC";
            ddlGLBankCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLBankCode, "A2ZGLMCUS");
        }
        protected void FromWareHouseDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlFromWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFromWarehouse, "A2ZGLMCUS");

        }
        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }


        private void TrancateWFTRN()
        {
            string sqlquery;
            sqlquery = @"DELETE dbo.WFTRN WHERE UserID='" + lblID.Text + "'";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZSTMCUS"));

            if (result > 0)
            {

            }
        }



        protected void ItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = 1";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZSTMCUS");

        }
        private void FromWarehouseDropdown()
        {
            string sqlquery = "SELECT WaseCode,WaseDescription from A2ZWAREHOUSE";
            ddlFromWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFromWarehouse, "A2ZSTMCUS");
        }

        private void ToWarehouseDropdown()
        {
            string sqlquery = "SELECT WaseCode,WaseDescription from A2ZWAREHOUSE";
            ddlToWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlToWarehouse, "A2ZSTMCUS");
        }

        private void UnitDropdown()
        {
            string sqlquery = "SELECT UnitNo,UnitDesc from A2ZUNITCODE";
            ddlUnit = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUnit, "A2ZSTMCUS");
        }

        private void gvItemDetailsInfo()
        {
            string sqlquery3 = "SELECT Id,ItemGroupNo,ItemCategoryNo,ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemTotalPrice,ItemSellPrice,TrnFlag FROM WFTRN Where UserId = '" + lblID.Text + "'";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvItemDetails, "A2ZSTMCUS");
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            if (ddlTrnType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Transaction Type');", true);
                return;
            }
            if (gvItemDetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records added');", true);
                return;
            }

            if (txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Voucher No');", true);
                return;
            }




            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Group');", true);
                return;
            }



            if (ddlFromWarehouse.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Warehouse');", true);
                return;
            }

            try
            {

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");
                lblProcessDate.Text = date1;


                DateTime opdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[17];
                prm[0] = "0";
                prm[1] = txtVchNo.Text;
                prm[2] = "0";
                prm[3] = "0";
                prm[4] = "No Supplier";
                prm[5] = ddlFromWarehouse.SelectedValue;
                prm[6] = ddlFromWarehouse.SelectedItem.Text;
                prm[7] = txtTrnNote.Text;
                prm[8] = "11";
                prm[9] = "Issue Sell";
                prm[10] = opdate;
                prm[11] = ddlTrnType.SelectedValue;
                if (ddlTrnType.SelectedValue == "48")
                {
                    prm[12] = txtGLBankCode.Text;
                    prm[13] = ddlGLBankCode.SelectedItem.Text;
                    prm[14] = txtChqNo.Text;
                }
                else 
                {
                    prm[12] = "0";
                    prm[13] = "0";
                    prm[14] = "0";
                }
                prm[15] = lblID.Text;
                prm[16] = "0";


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STIssSaleTransaction", prm, "A2ZSTMCUS"));

                if (result == 0)
                {
                    PrintTrnVoucher2();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void PrintTrnVoucher1()
        {
            try
            {

                if (ddlTrnType.SelectedValue == "1")
                {
                    lblTranTypeTitle.Text = "CASH";
                }
                else 
                {
                    lblTranTypeTitle.Text = "BANK";
                }


                DateTime Pdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTranTypeTitle.Text);

                Int32 CSGL = Converter.GetSmallInteger(1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLTransactionVch");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        protected void PrintTrnVoucher2()
        {
            try
            {

                if (ddlTrnType.SelectedValue == "1")
                {
                    lblTranTypeTitle.Text = "CASH";
                }
                else
                {
                    lblTranTypeTitle.Text = "BANK";
                }


                DateTime Pdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(lblProcessDate.Text));

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblVchMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblVchMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTranTypeTitle.Text);

                //Int32 CSGL = Converter.GetSmallInteger(1);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkSalesInvoiceReport");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnView_Click(object sender, EventArgs e)
        {

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                // SubGroupDropdown();
                ItemDropdown();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                ItemDropdown();
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
                prm[2] = ddlFromWarehouse.SelectedValue;
                prm[3] = 0;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateSingleItemBalance]", prm, "A2ZSTMCUS"));

                txtItemCode.Text = ddlItemName.SelectedValue;
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKUnit,STKUnitQty,STKUnitAvgCost,STKUnitSalePrice FROM A2ZSTMST WHERE STKItemCode = " + ddlItemName.SelectedValue + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = 1", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    ddlUnit.SelectedValue = Converter.GetString(dt.Rows[0]["STKUnit"]);

                    lblAvailQty.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);

                    ItemAvailQty();



                    txtUnitSalePrice.Text = Converter.GetString(String.Format("{0:0,0.00}", (dt.Rows[0]["STKUnitSalePrice"])));

                    lblUnitAvgCost.Text = Converter.GetString(dt.Rows[0]["STKUnitAvgCost"]);

                    txtQuantity.Focus();
                }

            }
            else
            {
                txtItemCode.Text = string.Empty;
                txtItemCode.Focus();
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
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
                    prm[2] = ddlFromWarehouse.SelectedValue;
                    prm[3] = 0;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_STGenerateSingleItemBalance]", prm, "A2ZSTMCUS"));



                    ddlItemName.SelectedValue = txtItemCode.Text;
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKUnit,STKUnitQty FROM A2ZSTMST WHERE STKItemCode = " + txtItemCode.Text + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = 1", "A2ZSTMCUS");

                    if (dt.Rows.Count > 0)
                    {
                        ddlUnit.SelectedValue = Converter.GetString(dt.Rows[0]["STKUnit"]);

                        lblAvailQty.Text = Converter.GetString(dt.Rows[0]["STKUnitQty"]);

                        ItemAvailQty();



                        txtQuantity.Focus();
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

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != string.Empty)
            {
                if (Converter.GetInteger(txtBalanceQty.Text) < Converter.GetInteger(txtQuantity.Text) || Converter.GetInteger(txtBalanceQty.Text) == 0)
                {
                    txtQuantity.Text = string.Empty;
                    txtQuantity.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Out of Stock Qty.');", true);
                    return;
                }


                txtUnitSalePrice.Focus();
                CalculateTotalPrice();
            }
        }



        private void CalculateTotalPrice()
        {
            if (txtQuantity.Text != string.Empty && txtUnitSalePrice.Text != string.Empty)
            {
                Decimal Q = Converter.GetDecimal(txtQuantity.Text);
                Decimal U = Converter.GetDecimal(txtUnitSalePrice.Text);
                Decimal A = Converter.GetDecimal(lblUnitAvgCost.Text);
                Decimal R = Q * U;

                Decimal N = (Q * Math.Round(A,2));


                txtTotalPrice.Text = Converter.GetString(String.Format("{0:0,0.00}", (R)));
                lblNetCostPrice.Text = Converter.GetString(String.Format("{0:0,0.00}", (N)));
            }
        }

        private void ItemAvailQty()
        {
            int Qty = 0;
            int AQty = 0;
            int FQty = Converter.GetInteger(lblAvailQty.Text);

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                String itmCode = gvItemDetails.Rows[i].Cells[3].Text.ToString();
                String itmQty = gvItemDetails.Rows[i].Cells[5].Text.ToString();

                if (txtItemCode.Text == itmCode)
                {
                    Qty += Converter.GetInteger(itmQty);
                }
            }

            AQty = (FQty - Qty);

            txtBalanceQty.Text = Converter.GetString(AQty);

        }
        protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Decimal Price = 0;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.RowIndex == 0)
            //        e.Row.Style.Add("height", "60px");
            //    e.Row.Style.Add("top", "-1500px");
            //}


            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                String txtTotal = gvItemDetails.Rows[i].Cells[8].Text.ToString();

                Price += Converter.GetDecimal(txtTotal);

                txtTotalAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", (Price)));


            }
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Voucher No');", true);
                return;
            }


            if (ddlItemName.SelectedIndex != 0 && txtItemCode.Text != string.Empty && txtQuantity.Text != string.Empty && txtTotalPrice.Text != string.Empty)
            {
                string statment = "INSERT INTO  WFTRN (ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemUnitPrice,ItemQty,ItemSellPrice,ItemTotalPrice,VchNo,ItemNetCostPrice,UserId)VALUES('" + ddlGroup.SelectedValue + "','" + ddlGroup.SelectedItem.Text + "','1','Saleable','" + txtItemCode.Text + "','" + ddlItemName.SelectedItem.Text + "','" + ddlUnit.SelectedValue + "','" + ddlUnit.SelectedItem.Text + "','" + lblUnitAvgCost.Text + "','" + txtQuantity.Text + "','" + txtUnitSalePrice.Text + "', '" + txtTotalPrice.Text + "','" + txtVchNo.Text + "','" + lblNetCostPrice.Text + "','" + lblID.Text + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZSTMCUS"));

                if (rowEffect > 0)
                {

                    txtItemCode.Text = string.Empty;
                    ddlItemName.SelectedIndex = 0;
                    ddlUnit.SelectedIndex = 0;
                    txtQuantity.Text = string.Empty;
                    txtUnitSalePrice.Text = string.Empty;
                    txtTotalPrice.Text = string.Empty;
                    txtBalanceQty.Text = string.Empty;

                    gvItemDetailsInfo();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Mandetory Fields');", true);
                return;
            }

        }

        protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Label IdNo = (Label)gvItemDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFTRN WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZSTMCUS"));
                gvItemDetailsInfo();

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        protected void txtGLBankCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLBankCode.Text != string.Empty)
                {
                    int GLCode;
                    A2ZCGLMSTDTO getDTO = new A2ZCGLMSTDTO();
                    string input1 = Converter.GetString(txtGLBankCode.Text).Length.ToString();
                    if (input1 == "6")
                    {
                        GLCode = Converter.GetInteger(txtGLBankCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetOldCodeInformation(GLCode));
                    }
                    else
                    {
                        GLCode = Converter.GetInteger(txtGLBankCode.Text);
                        getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));
                    }

                    if (getDTO.GLAccNo > 0)
                    {
                        lblStatus.Text = Converter.GetString(getDTO.Status);
                        CtrlRecType.Text = Converter.GetString(getDTO.GLRecType);
                        hdnGLSubHead.Text = Converter.GetString(getDTO.GLSubHead);
                        //CtrlContraAType.Text = Converter.GetString(getDTO.GLAccType);

                        if (lblStatus.Text == "99")
                        {
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Closed GL Code');", true);
                            return;
                        }


                        if (CtrlRecType.Text != "2")
                        {
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Trans. Header Record');", true);
                            return;
                        }
                        if (ddlTrnType.SelectedValue == "2" && (hdnGLSubHead.Text != "10106000" && hdnGLSubHead.Text != "20801000"))
                        {
                            txtGLBankCode.Text = string.Empty;
                            txtGLBankCode.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Bank Code');", true);
                            return;
                        }


                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLBankCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                    }
                    else
                    {
                        txtGLBankCode.Text = string.Empty;
                        txtGLBankCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGLCashCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlGLBankCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLBankCode.SelectedValue != "-Select-")
                {
                    int GLCode = Converter.GetInteger(ddlGLBankCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLBankCode.Text = Converter.GetString(getDTO.GLAccNo);

                        hdnGLSubHead.Text = Converter.GetString(getDTO.GLSubHead);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrnType.SelectedValue == "1")
            {
                lblGLBankCode.Visible = false;
                txtGLBankCode.Visible = false;
                ddlGLBankCode.Visible = false;
                lblChqNo.Visible = false;
                txtChqNo.Visible = false;
            }
            else
            {
                lblGLBankCode.Visible = true;
                txtGLBankCode.Visible = true;
                ddlGLBankCode.Visible = true;
                lblChqNo.Visible = true;
                txtChqNo.Visible = true;
            }
        }



    }
}
