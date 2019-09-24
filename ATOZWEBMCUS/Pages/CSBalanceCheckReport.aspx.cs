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
    public partial class CSBalanceCheckReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                  
                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    
                        lblModule.Text = Request.QueryString["a%b"];

                        if (lblModule.Text == "1")
                        {
                            Acc1dropdown();
                            
                        }

                        if (lblModule.Text == "4")
                        {
                            
                            Acc2dropdown();
                            txtAccType.Focus();

                        }

                        BtnPrint.Visible = false;

                        gvBalanceCheck.Visible = false;

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtDate.Text = date;

                        //txtDate.Enabled = false;

                        lblAccType.Visible = false;
                        txtAccType.Visible = false;
                        ddlAcType.Visible = false;
                    
                        
                

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }



        }


        

       
        private void Acc1dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeMode !='2' ORDER BY AccTypeClass";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void Acc2dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeMode !='1'";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
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
                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                        lblAccTypeClass.Text = Converter.GetString(getDTO.AccTypeClass);
                        if ((lblModule.Text == "1" || lblModule.Text == "6" || lblModule.Text == "7") && lblAccTypeMode.Text == "2")
                        {
                            InvalidAccTypeMSG();
                            txtAccType.Focus();
                            return;
                        }

                        if (lblModule.Text == "4" && lblAccTypeMode.Text == "1")
                        {
                            InvalidAccTypeMSG();
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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcType.SelectedValue == "-Select-")
                {
                    txtAccType.Focus();
                    txtAccType.Text = string.Empty;
                }

                if (ddlAcType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        lblAccTypeClass.Text = Converter.GetString(getDTO.AccTypeClass);

                       
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAcType_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void InvalidCuNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union');", true);
            return;

        }
        protected void InvalidAccTypeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
            return;

        }


        protected void gvPreview()
        {
            try
            {

                string sqlquery3 = "SELECT AccType,TrnDes,LedgerBalance,JournalBalance,DefferlBalance FROM A2ZBALANCECHECK";
                gvBalanceCheck = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBalanceCheck, "A2ZCSMCUS");

            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                throw ex;
            }
        }

        protected void gvBalanceCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");




               

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                DateTime fdate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[1];

                prm[0] = Converter.GetDateToYYYYMMDD(txtDate.Text);     // @fDate
                

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateBalanceCheck", prm, "A2ZCSMCUS"));

                gvBalanceCheck.Visible = true;
                gvPreview();

                BtnView.Visible = false;
                BtnPrint.Visible = true;
                           

                //int cutype = Converter.GetInteger(lblCuType.Text);
                //int CUNO = Converter.GetInteger(lblCuNo.Text);
                

                //int Module = Converter.GetInteger(lblModule.Text);
                //int CashCode = Converter.GetInteger(lblCashCode.Text);

               



                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlAcType.SelectedValue);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, fdate);
   
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO9, Module);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO10, CashCode);


                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                //if (lblModule.Text == "1")
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSLedgerBalance");
                //}
                //if (lblModule.Text == "4")
                //{
                //    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptStaffLedgerBalance");
                //}
                //Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                DateTime fdate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                


                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlAcType.SelectedValue);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, fdate);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO9, Module);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO10, CashCode);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSBalanceCheck");


                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }

        }

        
        
        protected void BtnExit_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}


