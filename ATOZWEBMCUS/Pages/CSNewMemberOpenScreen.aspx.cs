using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSNewMemberOpenScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                BtnOkay.Attributes.Add("onClick", "closePopup();");
                if (!IsPostBack)
                {
                    string TranDate = (string)Session["STranDate"];
                    string Func = (string)Session["SFuncOpt"];
                    string Module = (string)Session["SModule"];
                    string VchNo = (string)Session["SVchNo"];
                    string CUNo = (string)Session["SCUNo"];

                    string CType = (string)Session["SCType"];
                    string CNo = (string)Session["SCNo"];
                    string CuName = (string)Session["SCuName"];

                    string MemNo = (string)Session["SNewMemNo"];
                    string MemName = (string)Session["SNewMemName"];

                    string GLCashCode = (string)Session["SGLCashCode"];

                    //int FuncValue = Converter.GetInteger(Opt);

                    DateTime tdt = Converter.GetDateTime(TranDate);
                    string tdate = tdt.ToString("dd/MM/yyyy");
                    hdnTranDate.Value = tdate;
                    
                    hdnFunc.Value = Func;
                    hdnModule.Value = Module;
                    hdnVchNo.Value = VchNo;
                    hdnCuNo.Value = CUNo;

                    hdnCType.Value = CType;
                    hdnCNo.Value = CNo;
                    hdnCuName.Value = CuName;

                    hdnNewMemberNo.Value = MemNo;
                    txtNewMemNo.Text = MemNo;
                    hdnNewMemberName.Value = MemName;



                    hdnGLCashCode.Value = GLCashCode;

                    txtNewMemNo.ReadOnly = true;
                    txtNewMemName.Focus();

                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnOkay_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewMemName.Text == string.Empty)
                {
                    txtNewMemName.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input New Depositor Name' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }

                if (ddlMemType.SelectedValue == "0")
                {
                    ddlMemType.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input Depositor Type');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }


                Session["RTranDate"] = hdnTranDate.Value;
                Session["MemFlag"] = "1";
                Session["RFuncOpt"] = hdnFunc.Value;
                Session["RModule"] = hdnModule.Value;
                Session["RVchNo"] = hdnVchNo.Value;

                Session["RCUNo"] = hdnCuNo.Value;

                Session["RCType"] = hdnCType.Value;
                Session["RCNo"] = hdnCNo.Value;
                Session["RCuName"] = hdnCuName.Value;

                Session["RNewMemNo"] = txtNewMemNo.Text;
                Session["RNewMemName"] = txtNewMemName.Text;
                Session["RMemType"] = ddlMemType.SelectedValue;

                Session["RGLCashCode"] = hdnGLCashCode.Value;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSDailyBoothTransaction.aspx';self.close();</script>");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnOkay_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {

                Session["RTranDate"] = hdnTranDate.Value;
                Session["MemFlag"] = "1";
                Session["RFuncOpt"] = hdnFunc.Value;
                Session["RModule"] = hdnModule.Value;
                Session["RVchNo"] = hdnVchNo.Value;

                Session["RCUNo"] = hdnCuNo.Value;

                Session["RCType"] = hdnCType.Value;
                Session["RCNo"] = hdnCNo.Value;
                Session["RCuName"] = hdnCuName.Value;

                Session["RNewMemNo"] = hdnNewMemberNo.Value;
                Session["RNewMemName"] = "";
                //Session["RMemType"] = ddlMemType.SelectedValue;

                Session["RGLCashCode"] = hdnGLCashCode.Value;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSDailyBoothTransaction.aspx';self.close();</script>");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnExit_Click Problem');</script>");
                //throw ex;
            }

        }








    }
}