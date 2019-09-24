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
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class STRcvPurchaseTransaction : System.Web.UI.Page
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



                A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(lblID.Text), "A2ZHKMCUS");
                if (sysobj.VPrintflag == false)
                {
                    lblVPrintFlag.Text = "0";
                }
                else
                {
                    lblVPrintFlag.Text = "1";
                }


                GroupDropdown();
                UnitDropdown();
                SupplierDropdown();

                GLCashCodeDropdown();

                ddlWarehouse.SelectedValue = Converter.GetString(lblCashCode.Text);

                TrancateWFTRN();



            }

        }

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWarehouse, "A2ZGLMCUS");

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
        private void SupplierDropdown()
        {
            string sqlquery = "SELECT SuppCode,SuppName from A2ZSUPPLIER";
            ddlSupplier = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSupplier, "A2ZSTMCUS");
        }
        protected void SubGroupDropdown()
        {
            string sqquery = @"SELECT SubGrpCode,SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode='" + ddlGroup.SelectedValue + "' OR GrpCode='0'";
            ddlCategory = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlCategory, "A2ZSTMCUS");
        }

        protected void ItemDropdown()
        {
            string sqlquery = "SELECT STKItemCode,STKItemName from A2ZSTMST Where STKGroup = '" + ddlGroup.SelectedValue + "' AND STKSubGroup = '" + ddlCategory.SelectedValue + "'";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZSTMCUS");

        }
        private void WarehouseDropdown()
        {

            string sqlquery = "SELECT WaseCode,WaseDescription from A2ZWAREHOUSE";
            ddlWarehouse = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWarehouse, "A2ZSTMCUS");
        }

        private void UnitDropdown()
        {
            string sqlquery = "SELECT UnitNo,UnitDesc from A2ZUNITCODE";
            ddlUnit = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUnit, "A2ZSTMCUS");
        }

        private void gvItemDetailsInfo()
        {
            string sqlquery3 = "SELECT Id,ItemGroupNo,ItemCategoryNo,ItemCode,ItemName,ItemUnit,ItemQty,ItemUnitPrice,ItemTotalPrice,TrnFlag,ItemVATAmt,ItemTAXAmt FROM WFTRN Where UserId = '" + lblID.Text + "'";
            gvItemDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvItemDetails, "A2ZSTMCUS");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            if (gvItemDetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records added');", true);
                return;
            }
            if (txtOrderNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Order No');", true);
                return;
            }

            if (txtVchNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Voucher No');", true);
                return;
            }

            if (txtChalanNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Chalan No');", true);
                return;
            }

            if (txtChalanNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Chalan No');", true);
                return;
            }

            if (ddlSupplier.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Supplier');", true);
                return;
            }

            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Group');", true);
                return;
            }



            if (ddlWarehouse.SelectedIndex == 0)
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



                UpdateAvgCost();


                var prm = new object[14];

                prm[0] = txtVchNo.Text;
                prm[1] = txtOrderNo.Text;
                prm[2] = txtChalanNo.Text;
                prm[3] = ddlSupplier.SelectedValue;
                prm[4] = ddlSupplier.SelectedItem.Text;
                prm[5] = ddlWarehouse.SelectedValue;
                prm[6] = ddlWarehouse.SelectedItem.Text;
                prm[7] = txtTrnNote.Text;
                prm[8] = "1";
                prm[9] = "Purchase";
                prm[10] = opdate;
                prm[11] = "3";
                prm[12] = lblID.Text;
                prm[13] = "0";





                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STRcvTransaction", prm, "A2ZSTMCUS"));

                if (result == 0)
                {
                    //if (lblVPrintFlag.Text == "1")
                    //{
                    PrintTrnVoucher2();
                    //}
                    //else
                    //{
                    //    UpdatedMSG();
                    //}



                    //Response.Redirect(Request.RawUrl);

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
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "TRANSFER");

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
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "TRANSFER");

                //Int32 CSGL = Converter.GetSmallInteger(1);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStkPurchaseInvoiceReport");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }


        protected void UpdateAvgCost()
        {
            DateTime opdate = DateTime.ParseExact(lblProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            for (int i = 0; i < gvItemDetails.Rows.Count; ++i)
            {
                var prm = new object[4];
                prm[0] = gvItemDetails.Rows[i].Cells[3].Text.ToString();
                prm[1] = gvItemDetails.Rows[i].Cells[6].Text.ToString();
                prm[2] = gvItemDetails.Rows[i].Cells[7].Text.ToString();
                prm[3] = opdate;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_STCalculateAvgCost", prm, "A2ZSTMCUS"));

                if (result == 0)
                {

                }
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
                SubGroupDropdown();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
            {
                ItemDropdown();

            }

        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex != 0)
            {
                txtItemCode.Text = ddlItemName.SelectedValue;
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKUnit FROM A2ZSTMST WHERE STKItemCode = " + ddlItemName.SelectedValue + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + ddlCategory.SelectedValue + "", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {
                    ddlUnit.SelectedValue = Converter.GetString(dt.Rows[0]["STKUnit"]);
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
                    ddlItemName.SelectedValue = txtItemCode.Text;
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKUnit FROM A2ZSTMST WHERE STKItemCode = " + txtItemCode.Text + " AND STKGroup = " + ddlGroup.SelectedValue + " AND STKSubGroup = " + ddlCategory.SelectedValue + "", "A2ZSTMCUS");

                    if (dt.Rows.Count > 0)
                    {
                        ddlUnit.SelectedValue = Converter.GetString(dt.Rows[0]["STKUnit"]);
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
                txtUnitPrice.Focus();
                CalculateVATTAX();
                CalculateTotalPrice();
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateVATTAX();
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            if (txtQuantity.Text != string.Empty && txtUnitPrice.Text != string.Empty)
            {
                Decimal Q = Converter.GetDecimal(txtQuantity.Text);
                Decimal U = Converter.GetDecimal(txtUnitPrice.Text);
                Decimal NC = (Q * U);
                Decimal R = (Q * U) + Converter.GetDecimal(txtVatAmt.Text) + Converter.GetDecimal(txtTaxAmt.Text);
                //txtTotalPrice.Text = R.ToString("0.00");

                lblNetCostPrice.Text = Converter.GetString(String.Format("{0:0,0.00}", (NC)));

                txtTotalPrice.Text = Converter.GetString(String.Format("{0:0,0.00}", (R)));
            }
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
                String txtTotal = gvItemDetails.Rows[i].Cells[10].Text.ToString();

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
            if (ddlCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtItemCode.Text != string.Empty && txtQuantity.Text != string.Empty && txtTotalPrice.Text != string.Empty)
            {
                string statment = "INSERT INTO  WFTRN (ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemTotalPrice,VchNo,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,UserId)VALUES('" + ddlGroup.SelectedValue + "','" + ddlGroup.SelectedItem.Text + "','" + ddlCategory.SelectedValue + "','" + ddlCategory.SelectedItem.Text + "','" + txtItemCode.Text + "','" + ddlItemName.SelectedItem.Text + "','" + ddlUnit.SelectedValue + "','" + ddlUnit.SelectedItem.Text + "','" + txtQuantity.Text + "','" + txtUnitPrice.Text + "', '" + txtTotalPrice.Text + "','" + txtVchNo.Text + "','" + txtVatAmt.Text + "','" + txtTaxAmt.Text + "','" + lblNetCostPrice.Text + "','" + lblID.Text + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZSTMCUS"));

                if (rowEffect > 0)
                {
                    ddlCategory.SelectedIndex = 0;
                    txtItemCode.Text = string.Empty;
                    ddlItemName.SelectedIndex = 0;
                    ddlUnit.SelectedIndex = 0;
                    txtQuantity.Text = string.Empty;
                    txtUnitPrice.Text = string.Empty;
                    txtTotalPrice.Text = string.Empty;
                    txtVatAmt.Text = string.Empty;
                    txtTaxAmt.Text = string.Empty;
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

        private void CalculateVATTAX()
        {
            Decimal VATAmt = 0;
            Decimal TAXAmt = 0;
            Decimal Qty = Converter.GetDecimal(txtQuantity.Text);
            Decimal UP = Converter.GetDecimal(txtUnitPrice.Text);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKVat,STKTax FROM A2ZSTCHRG", "A2ZSTMCUS");

            if (dt.Rows.Count > 0)
            {
                VATAmt = Converter.GetDecimal(dt.Rows[0]["STKVat"]);
                TAXAmt = Converter.GetDecimal(dt.Rows[0]["STKTax"]);

                txtVatAmt.Text = (((Qty * UP) * VATAmt) / 100).ToString();
                txtTaxAmt.Text = (((Qty * UP) * TAXAmt) / 100).ToString();
            }

        }

        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            int SuppCode = Converter.GetInteger(txtSupplier.Text);
            A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(SuppCode));

            if (getDTO.SuppCode > 0)
            {
                ddlSupplier.SelectedValue = txtSupplier.Text;
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSupplier.Text = ddlSupplier.SelectedValue;
        }

        protected void txtVatAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        protected void txtTaxAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }
    }
}
