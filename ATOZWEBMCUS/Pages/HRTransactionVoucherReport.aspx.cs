using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRTransactionVoucherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtProcessDate.Text = date;
                lblProcDate.Text = date;


                A2ZHRPARAMETERDTO dto0 = A2ZHRPARAMETERDTO.GetParameterValue();
                DateTime dt0 = Converter.GetDateTime(dto0.ProcessDate);
                hdnToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dt0));


                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }


                lblProcessDate.Visible = false;
                txtProcessDate.Visible = false;
                lblVchNo.Visible = false;
                txtVchNo.Visible = false;

            }
        }



        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void InvalidVoucherMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Transaction Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Not Avaiable');", true);
            return;

        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            if (ddlFuncProceed.SelectedValue == "1")
            {
                PrintTrnVoucher1();
                return;
            }


            if (txtProcessDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please fill up the process date!' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill up the Process Date');", true);

                return;
            }



            DateTime fdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (lblProcDate.Text != txtProcessDate.Text)
            {
                lblVchFlag.Text = "1";
                lblVchYear.Text = Converter.GetString(fdate.Year);
            }
            else
            {
                lblVchFlag.Text = "0";
            }

            string qry;

            if (lblVchFlag.Text == "0")
            {

                qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND TrnFlag= 0 AND TrnCSGL= 0";
            }
            else
            {

                qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,ValueDate,UserID,FromCashCode FROM A2ZCSMCUST" + lblVchYear.Text + "..A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND TrnFlag= 0 AND TrnCSGL= 0";

            }


            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");

            if (dt.Rows.Count > 0)
            {
                lblCuType.Text = Converter.GetString(dt.Rows[0]["CuType"]);
                lblCuNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);
                lblMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                lblFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                lblTrnType.Text = Converter.GetString(dt.Rows[0]["TrnType"]);
                lblValueDate.Text = Converter.GetString(dt.Rows[0]["ValueDate"]);
                lblUserID.Text = Converter.GetString(dt.Rows[0]["UserID"]);
                lblFromCashCode.Text = Converter.GetString(dt.Rows[0]["FromCashCode"]);

                string qry1 = "SELECT IdsNo,IdsName from A2ZSYSIDS WHERE IdsNo = '" + lblUserID.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblUserIDName.Text = Converter.GetString(dt1.Rows[0]["IdsName"]);
                }

                string qry3 = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblFromCashCode.Text + "'";
                DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZGLMCUS");
                if (dt3.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblFromCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt3.Rows[0]["GLAccDesc"]);
                }
            }
            else
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                InvalidVoucherMSG();
                return;
            }


            lblTrnTypeTitle.Text = "TRANSFER";

            PrintTrnVoucher2();

        }

        protected void PrintTrnVoucher1()
        {
            try
            {

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRVerifySalaryTransaction", "A2ZHRMCUS"));
                if (result == 0)
                {
                }


                DateTime Pdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = Converter.GetDateTime(lblValueDate.Text);


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, hdnToDaysDate.Text);

                //Int32 CSGL = Converter.GetSmallInteger(0);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblUserID.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblUserIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZHRMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptViewSalaryTransactionVch");

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
                DateTime Pdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Vdate = Converter.GetDateTime(lblValueDate.Text);




                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Vdate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.MEMNO, lblMemNo.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME, lblMemName.Text);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblFuncTitle.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, lblTrnTypeTitle.Text);

                Int32 CSGL = Converter.GetSmallInteger(0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblBoothNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, lblBoothName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, lblUserID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblUserIDName.Text);

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
        protected void ddlFuncProceed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFuncProceed.SelectedValue == "1")
            {
                lblProcessDate.Visible = false;
                txtProcessDate.Visible = false;
                lblVchNo.Visible = false;
                txtVchNo.Visible = false;
            }
            else 
            {
                lblProcessDate.Visible = true;
                txtProcessDate.Visible = true;
                lblVchNo.Visible = true;
                txtVchNo.Visible = true;   
            }
        }





    }
}
