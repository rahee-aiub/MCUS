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
    public partial class CSAutoTransactionList : System.Web.UI.Page
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

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
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

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, fdate);
               
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                //if (ddlFuncType.SelectedValue == "14")
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionCPS");
                //}
                if (ddlFuncType.SelectedValue == "1")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 15);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "FDR Account");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionFDR");
                }
                if (ddlFuncType.SelectedValue == "2")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 16);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "Six Year Double");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvision6YR");
                }
                if (ddlFuncType.SelectedValue == "3")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 15);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "FDR Account");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoAnniversaryFdrList");
                }
                if (ddlFuncType.SelectedValue == "4")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 16);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "Six Year Double");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoAnniversary6YRList");
                }
                if (ddlFuncType.SelectedValue == "5")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 15);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "FDR Account");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoRenewalFdrList");
                }
                if (ddlFuncType.SelectedValue == "6")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 16);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "Six Year Double");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoRenewal6YRList");
                }
                if (ddlFuncType.SelectedValue == "7")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 17);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "Monthly Savings Plus");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoRenewalMSplusList");
                }
                if (ddlFuncType.SelectedValue == "8")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 17);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "Monthly Savings Plus");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoMonthlyBenefit");
                }


                //if (hdnAccTypeClass.Text == "6")
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoProvisionLOAN");
                //}


                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}