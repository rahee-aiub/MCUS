using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSViewGroupAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                //BtnOkay.Attributes.Add("onClick", "closePopup();");
                if (!IsPostBack)
                {
                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    
                    string Func = (string)Session["SFuncOpt"];
                    string Module = (string)Session["SModule"];

                    string OpenDate = (string)Session["SOpenDate"];
                    string VchNo = (string)Session["SVchNo"];

                    string CUNo = (string)Session["SCUNo"];
                    string CType = (string)Session["SCType"];
                    string CNo = (string)Session["SCNo"];
                    string MemNo = (string)Session["SNewMemNo"];
                    
                    string GLCashCode = (string)Session["SGLCashCode"];

                    hdnFuncOpt.Text = Func;
                    hdnModule.Text = Module;

                    DateTime dt = Converter.GetDateTime(OpenDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    hdnProcDate.Text = date;

                    hdnVchNo.Text = VchNo;
                    hdnCuNo.Text = CUNo;

                    hdnCuType.Text = CType;
                    hdnCuNo.Text = CNo;

                    hdnMemNo.Text = MemNo;

                    hdnGLCashCode.Text = GLCashCode;

                    GenerateGroupAccountInfo();

                    gvGroupInfo();


                    //    txtMaturityDate.ReadOnly = true;


                    //    //int FuncValue = Converter.GetInteger(Opt);
                    //    hdnFunc.Value = Func;
                    //    hdnModule.Value = Module;

                    //    DateTime dt = Converter.GetDateTime(OpenDate);
                    //    string date = dt.ToString("dd/MM/yyyy");
                    //    hdnProcDate.Value = date;

                    //    hdnVchNo.Value = VchNo;
                    //    hdnCuNo.Value = CUNo;

                    //    hdnCType.Value = CType;
                    //    hdnCNo.Value = CNo;

                    //    string a = hdnCType.Value;
                    //    string b = hdnCNo.Value;
                    //    txtCuNumber.Text = (a + "-" + b);


                    //    hdnCuName.Value = CuName;
                    //    lblCuName.Text = hdnCuName.Value;

                    //    hdnNewMemberNo.Value = MemNo;
                    //    txtMemNo.Text = hdnNewMemberNo.Value;
                    //    hdnNewMemberName.Value = MemName;
                    //    lblMemName.Text = hdnNewMemberName.Value;

                    //    hdnGLCashCode.Value = GLCashCode;

                    //    hdnTrnCode.Value = TrnCode;

                    //    hdnAccType.Value = AccType;
                    //    hdnlblcls.Value = lblcls;

                    //    hdnmemType.Value = memType;

                    //    txtAccType.Text = hdnAccType.Value;

                    //    string sqlquery2 = "SELECT AccTypeDescription from A2ZACCTYPE WHERE  AccTypeCode='" + hdnAccType.Value + "'";
                    //    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery2, "A2ZCSMCUS");
                    //    if (dt3.Rows.Count > 0)
                    //    {
                    //        lblAccName.Text = Converter.GetString(dt3.Rows[0]["AccTypeDescription"]);
                    //    }

                    //    if (hdnlblcls.Value == "3")
                    //    {
                    //        lblIntRate.Visible = false;
                    //        txtIntRate.Visible = false;
                    //        lblContractInt.Visible = false;
                    //        txtContractInt.Visible = false;
                    //        ChkContraInt.Visible = false;
                    //    }

                    //    txtAccType.ReadOnly = true;
                    //    txtCuNumber.ReadOnly = true;
                    //    txtMemNo.ReadOnly = true;


                    //    txtPeriod.Focus();


                    //}
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }
            
            
        //    -----------------------------------------------------
        //    try
        //    {

        //        if (!IsPostBack)
        //        {

        //            string Flag = (string)Session["Flag"];
        //            string CModule = (string)Session["CModule"];
        //            CtrlFlag.Text = Flag;
        //            CtrlModule.Text = CModule;

        //            if (CtrlFlag.Text != "1")
        //            {
        //                lblModule.Text = Request.QueryString["a%b"];
        //            }
        //            else
        //            {
        //                lblModule.Text = CtrlModule.Text;
        //            }
        //            hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
        //            lblmsg1.Visible = false;
        //            lblmsg2.Visible = false;
        //            gvInfo();


        //        }
        //        //else
        //        //{
        //        //    gvInfo();

        //        //}

        //    }
        //    catch (Exception ex)
        //    {

        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
        //        //throw ex;
        //    }
        //}


       

        private void gvGroupInfo()
        {

            string sqlquery3 = "SELECT TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance FROM WFCSGROUPACCOUNT WHERE UserId= '" + hdnID.Text +"'";
            gvGrpInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvGrpInfo, "A2ZCSMCUS");
        }


        private void GenerateGroupAccountInfo()
        {
            var prm = new object[5];
            prm[0] = hdnCuType.Text;
            prm[1] = hdnCuNo.Text;
            prm[2] = hdnMemNo.Text;
            prm[3] = hdnFuncOpt.Text;
            prm[4] = hdnID.Text;
            
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateGroupAccount", prm, "A2ZCSMCUS"));


        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label TrnCode = (Label)gvGrpInfo.Rows[r.RowIndex].Cells[1].FindControl("lblTrnCode");
                Label AccType = (Label)gvGrpInfo.Rows[r.RowIndex].Cells[3].FindControl("lblAccType");
                Label AccNo = (Label)gvGrpInfo.Rows[r.RowIndex].Cells[4].FindControl("lblAccNo");

                string TCode = Converter.GetString(TrnCode.Text);
                string AType = Converter.GetString(AccType.Text);
                string ANo = Converter.GetString(AccNo.Text);

                

                Session["GrpFlag"] = "1";

                Session["RFuncOpt"] = hdnFuncOpt.Text;
                Session["RModule"] = hdnModule.Text;

                Session["ROpenDate"] = hdnProcDate.Text;
                
                Session["RVchNo"] = hdnVchNo.Text;

                Session["RCUNo"] = hdnCuNo.Text;

                Session["RCType"] = hdnCuType.Text;
                Session["RCNo"] = hdnCuNo.Text;
                

                Session["RNewMemNo"] = hdnMemNo.Text;
                

                Session["RGLCashCode"] = hdnGLCashCode.Text;

                Session["RTrnCode"] = TCode;
                Session["RAccType"] = AType;
                Session["RAccNo"] = ANo;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSDailyTransaction.aspx';self.close();</script>");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnOkay_Click Problem');</script>");
                //throw ex;
            }
            
            
            
            
            
            //-------------------------------------------------------------------------------------
            //try
            //{

            //    Button b = (Button)sender;
            //    GridViewRow r = (GridViewRow)b.NamingContainer;
            //    Label voucher = (Label)gvCUInfo.Rows[r.RowIndex].Cells[1].FindControl("lblVoucherNo");
            //    Label Func = (Label)gvCUInfo.Rows[r.RowIndex].Cells[2].FindControl("lblFuncOpt");
            //    Label FuncDesc = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblFuncOptDesc");
            //    Label TrnAmount = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("Amount");
            //    lblTrnAmount.Text = Converter.GetString(TrnAmount.Text);

            //    lblFuncDesc.Text = Converter.GetString(FuncDesc.Text);

            //    string VNo = Converter.GetString(voucher.Text);
            //    string FOpt = Converter.GetString(Func.Text);
            //    lblFuncopt.Text = Converter.GetString(Func.Text);

            //    double TotalAmt = Converter.GetDouble(lblTrnAmount.Text);
            //    int Ids = Converter.GetInteger(hdnID.Value);
            //    A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(Ids));

            //    if (getDTO.IdsNo > 0)
            //    {
            //        if (lblFuncopt.Text == "1" ||
            //            lblFuncopt.Text == "11")
            //        {
            //            if (TotalAmt > getDTO.LIdsCashCredit)
            //            {
            //                OverAccessLimitMSG();
            //                return;
            //            }
            //        }

            //        if (lblFuncopt.Text == "2" ||
            //            lblFuncopt.Text == "3" ||
            //            lblFuncopt.Text == "4" ||
            //            lblFuncopt.Text == "5" ||
            //            lblFuncopt.Text == "6" ||
            //            lblFuncopt.Text == "7" ||
            //            lblFuncopt.Text == "8" ||
            //            lblFuncopt.Text == "9" ||
            //            lblFuncopt.Text == "10" ||
            //            lblFuncopt.Text == "12" ||
            //            lblFuncopt.Text == "20")
            //        {
            //            if (TotalAmt > getDTO.LIdsCashDebit)
            //            {
            //                OverAccessLimitMSG();
            //                return;
            //            }

            //        }
            //    }

            //    Session["VouNo"] = VNo;
            //    Session["FuncOpt"] = FOpt;
            //    Session["Module"] = lblModule.Text;
            //    Session["FuncTitle"] = lblFuncDesc.Text;

            //    Page.ClientScript.RegisterStartupScript(
            //    this.GetType(), "OpenWindow", "window.open('CSViewDailyTransaction.aspx','_newtab');", true);
            //}
            //catch (Exception ex)
            //{

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSelect_Click Problem');</script>");
            //    //throw ex;
            //}

        }
        protected void OverAccessLimitMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Access Denied for Transaction Accessbility Limit');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Access Denied for Transaction Accessbility Limit');", true);
            return;
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GrpFlag"] = "1";

                Session["RFuncOpt"] = hdnFuncOpt.Text;
                Session["RModule"] = hdnModule.Text;

                Session["ROpenDate"] = hdnProcDate.Text;

                Session["RVchNo"] = hdnVchNo.Text;

                Session["RCUNo"] = hdnCuNo.Text;

                Session["RCType"] = hdnCuType.Text;
                Session["RCNo"] = hdnCuNo.Text;


                Session["RNewMemNo"] = hdnMemNo.Text;


                Session["RGLCashCode"] = hdnGLCashCode.Text;

                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSDailyTransaction.aspx';self.close();</script>");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnExit_Click Problem');</script>");
                //throw ex;
            }
        }


    }
}