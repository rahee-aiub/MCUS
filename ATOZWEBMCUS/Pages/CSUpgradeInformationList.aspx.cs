using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSUpgradeInformationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");

                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

                    lblPdate.Text = date;

                    CashCodeDropdown();

                    chkAllCashCode.Checked = true;
                    ddlCashCode.Enabled = false;

                    ChkAllOldCuNo.Checked = true;
                    ChkAllOldMemNo.Checked = true;
                    ChkAllOldAccNo.Checked = true;


                    txtOldCuNo.Enabled = false;
                    txtOldMemNo.Enabled = false;
                    txtOldAccType.Enabled = false;
                    txtOldAccNo.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void CashCodeDropdown()
        {
            string sqlquery = @"SELECT GLAccNo,+ CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";

            ddlCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCashCode, "A2ZGLMCUS");
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fdate = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                int OldCuNo = Converter.GetInteger(txtOldCuNo.Text);


                if (chkAllCashCode.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO11, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO11, ddlCashCode.SelectedValue);
                }

                if (ChkAllOldCuNo.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, OldCuNo);
                }

                int OldMemNo = Converter.GetInteger(txtOldMemNo.Text);

                if (ChkAllOldMemNo.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, OldMemNo);
                }


                int OldAccType = Converter.GetInteger(txtOldAccType.Text);
                int OldAccNo = Converter.GetInteger(txtOldAccNo.Text);

                if (ChkAllOldAccNo.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, 1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, OldAccType);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, OldAccNo);
                }

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSUpgradeInformationReport");



                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ChkAllOldAccNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllOldAccNo.Checked)
            {
                txtOldAccType.Text = string.Empty;
                txtOldAccType.Enabled = false;
                txtOldAccNo.Text = string.Empty;
                txtOldAccNo.Enabled = false;
            }
            else
            {
                txtOldAccType.Enabled = true;
                txtOldAccNo.Enabled = true;
            }
        }
        protected void chkAllCashCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllOldAccNo.Checked == true)
            {
                if (chkAllCashCode.Checked == false)
                {
                    ddlCashCode.Enabled = true;
                }
                else
                {
                    ddlCashCode.Enabled = false;
                }
            }
            else 
            {
                chkAllCashCode.Checked = true;
            }
        }
        protected void ChkAllOldCuNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllOldAccNo.Checked == true)
            {
                if (ChkAllOldCuNo.Checked)
                {
                    txtOldCuNo.Text = string.Empty;
                    txtOldCuNo.Enabled = false;
                }
                else
                {
                    txtOldCuNo.Enabled = true;
                }
            }
            else
            {
                ChkAllOldCuNo.Checked = true;
            }
        }

        protected void ChkAllOldMemNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllOldAccNo.Checked == true)
            {
                if (ChkAllOldMemNo.Checked)
                {
                    txtOldMemNo.Text = string.Empty;
                    txtOldMemNo.Enabled = false;
                }
                else
                {
                    txtOldMemNo.Enabled = true;
                }
            }
            else
            {
                ChkAllOldMemNo.Checked = true;
            }
        }

       
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}