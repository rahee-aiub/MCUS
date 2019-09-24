using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;






namespace ATOZWEBMCUS.Pages
{
    public partial class STLedgerBalanceReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    if (CtrlProgFlag.Text != "1")
                    {



                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtDate.Text = date;
                    }
                    else
                    {
                        string RtxtDate = (string)Session["StxtDate"];



                        string RlblModule = (string)Session["SlblModule"];



                        txtDate.Text = RtxtDate;

                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                //throw ex;
            }



        }


        protected void RemoveSession()
        {

            Session["ProgFlag"] = string.Empty;

            Session["StxtDate"] = string.Empty;


            Session["SlblModule"] = string.Empty;
        }





        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");



                Session["ProgFlag"] = "1";

                Session["StxtDate"] = txtDate.Text;


              
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);




                int Module = Converter.GetInteger(lblModule.Text);
                int CashCode = Converter.GetInteger(lblCashCode.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtDate.Text));


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, Converter.GetDateToYYYYMMDD(txtDate.Text));


               
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZSTMCUS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMSTSupplierLedgerBalance");


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
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}


