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
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class STSupplierStatementReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                SupplierDropdown();


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtfdate.Text = Converter.GetString(date);
                txttdate.Text = Converter.GetString(date);


            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (txtSupplierCode.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Supplier Code');", true);
                return;
            }


            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, txtSupplierCode.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlSupplierName.SelectedItem.Text);


            lblPreAddressLine.Text = lblAddressl1.Text.Trim() + "," + lblAddressl2.Text.Trim() + "," + lblAddressl3.Text.Trim();
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblPreAddressLine.Text);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblMobile.Text);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMSTSupplierStatement");

            Response.Redirect("ReportServer.aspx", false);
        }



        private void SupplierDropdown()
        {
            string sqlquery = "SELECT SuppCode,SuppName from A2ZSUPPLIER";
            ddlSupplierName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSupplierName, "A2ZSTMCUS");
        }

        protected void txtSupplierCode_TextChanged(object sender, EventArgs e)
        {
            if (txtSupplierCode.Text != string.Empty)
            {
                int MainCode = Converter.GetInteger(txtSupplierCode.Text);
                A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(MainCode));

                if (getDTO.SuppCode > 0)
                {
                    ddlSupplierName.SelectedValue = Converter.GetString(getDTO.SuppCode);
                    lblAddressl1.Text = Converter.GetString(getDTO.SuppAddL1);
                    lblAddressl2.Text = Converter.GetString(getDTO.SuppAddL2);
                    lblAddressl3.Text = Converter.GetString(getDTO.SuppAddL3);
                    lblMobile.Text = Converter.GetString(getDTO.SuppMobile);
                }
                else
                {
                    ddlSupplierName.SelectedIndex = 0;
                    txtSupplierCode.Text = string.Empty;
                    txtSupplierCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Supplier Code');", true);
                    return;
                }
            }
        }

        protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplierName.SelectedIndex != 0)
            {
                txtSupplierCode.Text = ddlSupplierName.SelectedValue;

                int MainCode = Converter.GetInteger(txtSupplierCode.Text);
                A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(MainCode));

                if (getDTO.SuppCode > 0)
                {
                    ddlSupplierName.SelectedValue = Converter.GetString(getDTO.SuppCode);
                    lblAddressl1.Text = Converter.GetString(getDTO.SuppAddL1);
                    lblAddressl2.Text = Converter.GetString(getDTO.SuppAddL2);
                    lblAddressl3.Text = Converter.GetString(getDTO.SuppAddL3);
                    lblMobile.Text = Converter.GetString(getDTO.SuppMobile);
                }
                else
                {
                    ddlSupplierName.SelectedIndex = 0;
                    txtSupplierCode.Text = string.Empty;
                    txtSupplierCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Supplier Code');", true);
                    return;
                }

            }
        }



    }
}
