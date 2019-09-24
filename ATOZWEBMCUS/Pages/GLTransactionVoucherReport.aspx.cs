using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
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
    public partial class GLTransactionVoucherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                txtVchNo.Focus();
                //BtnProcess.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtProcessDate.Text = date;

                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblBoothNo.Text = lblCashCode.Text;
                    lblBoothName.Text = Converter.GetString(dt1.Rows[0]["GLAccDesc"]);
                }
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

            if (txtVchNo.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Voucher No.' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
                return;
            }

            DateTime fdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT Id,CuType,CuNo,MemNo,FuncOpt,TrnType,TrnDrCr,ValueDate,UserID FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + fdate + "' AND TrnFlag= 0 AND TrnCSGL= 1";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                lblCuType.Text = Converter.GetString(dt.Rows[0]["CuType"]);
                lblCuNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);
                lblMemNo.Text = Converter.GetString(dt.Rows[0]["MemNo"]);
                lblFuncOpt.Text = Converter.GetString(dt.Rows[0]["FuncOpt"]);
                lblTrnType.Text = Converter.GetString(dt.Rows[0]["TrnType"]);
                lblDrCr.Text = Converter.GetString(dt.Rows[0]["TrnDrCr"]);
                lblValueDate.Text = Converter.GetString(dt.Rows[0]["ValueDate"]);
                lblUserID.Text = Converter.GetString(dt.Rows[0]["UserID"]);

                string qry1 = "SELECT IdsNo,IdsName from A2ZSYSIDS WHERE IdsNo = '" + lblUserID.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblUserIDName.Text = Converter.GetString(dt1.Rows[0]["IdsName"]);
                }
                
            }
            else
            {
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                InvalidVoucherMSG();
                return;
            }

            //Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
            //int CUNo = Converter.GetInteger(lblCuNo.Text);
            //int MNo = Converter.GetInteger(lblMemNo.Text);
            //A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
            //if (get6DTO.NoRecord > 0)
            //{
            //    lblMemName.Text = Converter.GetString(get6DTO.MemberName);
            //}


            if (lblTrnType.Text == "1" && lblDrCr.Text == "1")
            {
                lblTrnTypeTitle.Text = "CASH - In";
            }
            else if (lblTrnType.Text == "1" && lblDrCr.Text == "0")
            {
                lblTrnTypeTitle.Text = "CASH - Out";
            }
            else
            {
                lblTrnTypeTitle.Text = "TRANSFER";
            }

             
            PrintTrnVoucher();

        }

        protected void PrintTrnVoucher()
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

                Int32 CSGL = Converter.GetSmallInteger(1);
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



    }
}
