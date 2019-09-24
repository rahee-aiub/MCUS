using DataAccessLayer.DTO.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.HouseKeeping;
namespace ATOZWEBMCUS.Pages
{
    public partial class CSGroupSummaryStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCreditUNo.Focus();

                string Cflag = (string)Session["flag"];
                CtrlFlag.Text = Cflag;

                if (CtrlFlag.Text == "1")
                {
                    string CuNo = (string)Session["RCreditUNo"];
                    txtCreditUNo.Text = CuNo;
                    string MemNo = (string)Session["RMemNo"];
                    txtMemNo.Text = MemNo;
                    string memname = (string)Session["MemName"];
                    lblMemName.Text = memname;
                    txtCreditUNo_TextChanged(this, EventArgs.Empty);
                    txtMemNo_TextChanged(this, EventArgs.Empty);
                }
                else
                {
                    CtrlFlag.Text = string.Empty;
                }



            }

        }


        protected void SessionRemove()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["MemName"] = string.Empty;

            Session["flag"] = string.Empty;
            Session["ProgCtrl"] = string.Empty;
        }

        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {

                    A2ZCUNIONDTO getDTO = new A2ZCUNIONDTO();

                    if (chkOldSearch.Checked == true)
                    {
                        int CN = Converter.GetInteger(txtCreditUNo.Text);
                        getDTO = (A2ZCUNIONDTO.GetOldInfo(CN));

                        hdnCuNumber.Text = txtCreditUNo.Text;
                    }
                    else
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

                        getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    }

                    if (getDTO.CreditUnionNo > 0)
                    {
                        lblCuStatus.Text = Converter.GetString(getDTO.CuStatus);
                        if (lblCuStatus.Text == "9")
                        {
                            TransferCuNoMSG();

                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }

                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                        //string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
                        //ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        //ddlCreditUNo.SelectedValue = Converter.GetString(lblCuType.Text + lblCuNo.Text);
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        txtMemNo.Focus();



                    }
                    else
                    {
                        InvalidCuNoMSG();

                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
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


                if (txtMemNo.Text != string.Empty)
                {
                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    if (chkOldSearch.Checked == true)
                    {
                        int MemNumber = Converter.GetInteger(txtMemNo.Text);
                        int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                        getDTO = (A2ZMEMBERDTO.GetInfoOldMember(CuNumber, MemNumber));
                    }
                    else
                    {
                        Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                        int CUNo = Converter.GetInteger(lblCuNo.Text);
                        int MNo = Converter.GetInteger(txtMemNo.Text);

                        getDTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));

                    }




                    //Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    //int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    //int MemNumber = Converter.GetInteger(txtMemNo.Text);

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    //getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        lblMemName.Text = Converter.GetString(getDTO.MemberName);

                    }
                    else
                    {
                        txtMemNo.Text = string.Empty;
                        lblMemName.Text = string.Empty;
                        txtMemNo.Focus();
                        InvalidMemNoMSG();
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void InvalidMemNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void InvalidCuNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        private void TransferCuNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Transfered Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transfered Credit Union No.');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCreditUNo.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Credit Union No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                    return;
                }

                if (txtMemNo.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Depositor No.' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Depositor No.');", true);
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
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, cutype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cuno);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, memno);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, ddlAccStatus.SelectedIndex);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblCuName.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblMemName.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlAccStatus.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGroupSummaryStatement");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            //Session["TrnDate"] = txtLoanAppDate.Text;
            //Session["LAcType"] = ddlAccType.SelectedValue;
            //Session["Module"] = lblModule.Text;
            Session["ExFlag"] = "3";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }


    }
}