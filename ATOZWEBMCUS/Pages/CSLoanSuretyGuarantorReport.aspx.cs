using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanSuretyGuarantorReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtCreditUNo.Focus();
                CreditUnionDropdown();
                
            }
        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    if (getDTO.CreditUnionNo > 0)
                    {
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
                        ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCuNo.Text);
                        txtMemNo.Focus();
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                        string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";
                        ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");

                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                        ddlMemNo.SelectedIndex = 0;
                        txtMemNo.Text = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCreditUNo.SelectedValue == "-Select-")
            {

                txtCreditUNo.Text = string.Empty;
                txtMemNo.Focus();
                ddlMemNo.SelectedIndex = 0;
                return;
            }

            try
            {

                if (ddlCreditUNo.SelectedValue != "-Select-")
                {

                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(CNo);

                    string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo='" + CNo + "'and CuType='" + CuType + "' GROUP BY MemNo,MemName";

                    ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlMemNo, "A2ZCSMCUS");


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {

                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                        txtMemNo.Focus();

                    }
                    else
                    {
                        ddlCreditUNo.SelectedValue = "-Select-";
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMemNo.SelectedValue == "-Select-")
                {

                }

                if (txtMemNo.Text != string.Empty && ddlCreditUNo.SelectedValue != "-Select-")
                {

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(txtMemNo.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                       

                    }
                    else
                    {
                        ddlMemNo.SelectedValue = "-Select-";
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemNo.SelectedValue != "-Select-" && ddlCreditUNo.SelectedValue != "-Select-")
            {
                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                   
                }
            }

        }



        protected void rbtShareGuarantor_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtShareGuarantor.Checked==true)
            {
                rbtOtherAccountGuarantor.Checked = false;
            }
        }

        protected void rbtOtherAccountGuarantor_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtOtherAccountGuarantor.Checked==true)
            {
                rbtShareGuarantor.Checked = false;
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            if(rbtShareGuarantor.Checked==false && rbtOtherAccountGuarantor.Checked==false)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Guarantor Option should not be Empty');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Guarantor Option should not be Empty');", true);
                return;
            }

               
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                int cutype = Converter.GetInteger(lblCuType.Text);
                int cuno = Converter.GetInteger(lblCuNo.Text);
                int memno = Converter.GetInteger(txtMemNo.Text);

                // For Credit Union No. and Name
                string CreditUNo = Converter.GetString(txtCreditUNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.CUNO, CreditUNo);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, CreditUNo);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlCreditUNo.SelectedItem.Text);
                //
                // For Member No. and Name
                int MemNo = Converter.GetInteger(txtMemNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, MemNo);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5,MemNo);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlMemNo.SelectedItem.Text);
                if (rbtShareGuarantor.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, cutype);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cuno);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, memno);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rpt_SuretyShareGuarantor");

                }
                if (rbtOtherAccountGuarantor.Checked)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, cutype);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cuno);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, memno);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rpt_SuretyAllGuarantor");

                }
               
                Response.Redirect("ReportServer.aspx", false);
            
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

       
    }
}