using System;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSProvisionBalanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
                txtAccType.Focus();
                Accdropdown();
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

            }
           
                   }

        private void Accdropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode!=2";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }


        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");

                            
                if (txtAccType.Text == string.Empty )
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Account Type  Not Abailabe' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Type  Not Abailabe');", true);
                    return;
                }


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, txtToDaysDate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);
                if (ChkZeroBalanceShow.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 1); 
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2,"With Zero Balance ");
                }
              
                else
                {
                  SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                  SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2,"Only Balance " );
              
                }
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlAcType.SelectedValue);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlAcType.SelectedItem.Text);
             
              
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSProvisionBalance");
              

                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlAcType.SelectedValue == "-Select-")
            //{
            //    txtAccType.Focus();
            //    txtAccType.Text = string.Empty;
            //}
            try
            {


                if (ddlAcType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtAccType.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        if (getDTO.AccTypeMode == 2)
                        {
                            txtAccType.Text = string.Empty;
                            txtAccType.Focus();
                            return;
                        }
                        
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}