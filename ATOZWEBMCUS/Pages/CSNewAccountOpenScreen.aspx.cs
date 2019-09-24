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
    public partial class CSNewAccountOpenScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //BtnOkay.Attributes.Add("onClick", "closePopup();");
                if (!IsPostBack)
                {
                    string TranDate = (string)Session["STranDate"];
                    string Func = (string)Session["SFuncOpt"];
                    string Module = (string)Session["SModule"];

                    string OpenDate = (string)Session["SOpenDate"];
                    string VchNo = (string)Session["SVchNo"];

                    string CUNo = (string)Session["SCUNo"];
                    string CType = (string)Session["SCType"];
                    string CNo = (string)Session["SCNo"];
                    string CuName = (string)Session["SCuName"];

                    string MemNo = (string)Session["SNewMemNo"];
                    string MemName = (string)Session["SNewMemName"];

                    string GLCashCode = (string)Session["SGLCashCode"];

                    string TrnCode = (string)Session["STrnCode"];

                    string AccType = (string)Session["SAccType"];
                    string lblcls = (string)Session["Slblcls"];

                    string memType = (string)Session["SmemType"];


                    txtMaturityDate.ReadOnly = true;


                    //int FuncValue = Converter.GetInteger(Opt);
                    hdnFunc.Value = Func;
                    hdnModule.Value = Module;

                    DateTime tdt = Converter.GetDateTime(TranDate);
                    string tdate = tdt.ToString("dd/MM/yyyy");
                    hdnTranDate.Value = tdate;

                    DateTime dt = Converter.GetDateTime(OpenDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    hdnProcDate.Value = date;

                    hdnVchNo.Value = VchNo;
                    hdnCuNo.Value = CUNo;

                    hdnCType.Value = CType;
                    hdnCNo.Value = CNo;

                    string a = hdnCType.Value;
                    string b = hdnCNo.Value;
                    txtCuNumber.Text = (a + "-" + b);


                    hdnCuName.Value = CuName;
                    lblCuName.Text = hdnCuName.Value;

                    hdnNewMemberNo.Value = MemNo;
                    txtMemNo.Text = hdnNewMemberNo.Value;
                    hdnNewMemberName.Value = MemName;
                    lblMemName.Text = hdnNewMemberName.Value;

                    hdnGLCashCode.Value = GLCashCode;

                    hdnTrnCode.Value = TrnCode;

                    hdnAccType.Value = AccType;
                    hdnlblcls.Value = lblcls;

                    hdnmemType.Value = memType;

                    txtAccType.Text = hdnAccType.Value;

                    string sqlquery2 = "SELECT AccTypeDescription from A2ZACCTYPE WHERE  AccTypeCode='" + hdnAccType.Value + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery2, "A2ZCSMCUS");
                    if (dt3.Rows.Count > 0)
                    {
                        lblAccName.Text = Converter.GetString(dt3.Rows[0]["AccTypeDescription"]);
                    }

                    if (hdnlblcls.Value == "3")
                    {
                        lblIntRate.Visible = false;
                        txtIntRate.Visible = false;
                        lblContractInt.Visible = false;
                        txtContractInt.Visible = false;
                        ChkContraInt.Visible = false;
                        txtFixedDepositAmount.Focus();
                    }
                    else
                    {
                        lblFixedDepositAmount.Visible = false;
                        txtFixedDepositAmount.Visible = false;
                        lblFixedMthInt.Visible = false;
                        txtFixedMthInt.Visible = false;
                        txtPeriod.Focus();

                    }


                    txtAccType.ReadOnly = true;
                    txtCuNumber.ReadOnly = true;
                    txtMemNo.ReadOnly = true;



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
                if (txtFixedDepositAmount.Text == string.Empty && hdnlblcls.Value == "3")
                {
                    txtFixedDepositAmount.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input Fixed Deposit Amount' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }
                if (txtPeriod.Text == string.Empty && hdnlblcls.Value == "2")
                {
                    txtPeriod.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input Period' );";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }


                Session["AccFlag"] = "1";

                Session["RTranDate"] = hdnTranDate.Value;
                Session["RFuncOpt"] = hdnFunc.Value;
                Session["RModule"] = hdnModule.Value;
                Session["RVchNo"] = hdnVchNo.Value;

                Session["RCUNo"] = hdnCuNo.Value;

                Session["RCType"] = hdnCType.Value;
                Session["RCNo"] = hdnCNo.Value;
                Session["RCuName"] = hdnCuName.Value;

                Session["RNewMemNo"] = hdnNewMemberNo.Value;
                Session["RNewMemName"] = hdnNewMemberName.Value;

                Session["RGLCashCode"] = hdnGLCashCode.Value;

                Session["RTrnCode"] = hdnTrnCode.Value;

                Session["RAccType"] = hdnAccType.Value;
                Session["Rlblcls"] = hdnlblcls.Value;

                Session["RPeriod"] = txtPeriod.Text;
                Session["RIntRate"] = txtIntRate.Text;
                Session["RMaturityDate"] = txtMaturityDate.Text;
                Session["RMaturityAmt"] = CtrlMatureAmt.Text;
              
                Session["RFixedDepAmt"] = txtFixedDepositAmount.Text;
                Session["RFixedMthInt"] = txtFixedMthInt.Text;
                Session["RBenefitDate"] = CtrlBenefitDate.Text;

                if (ChkContraInt.Checked == true)
                {
                    Session["RContractInt"] = "1";
                }
                else
                {
                    Session["RContractInt"] = "0";
                }

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

                Session["AccFlag"] = "1";

                Session["RTranDate"] = hdnTranDate.Value;
                Session["RFuncOpt"] = hdnFunc.Value;
                Session["RModule"] = hdnModule.Value;
                Session["RVchNo"] = hdnVchNo.Value;

                Session["RCUNo"] = hdnCuNo.Value;

                Session["RCType"] = hdnCType.Value;
                Session["RCNo"] = hdnCNo.Value;
                Session["RCuName"] = hdnCuName.Value;

                Session["RNewMemNo"] = hdnNewMemberNo.Value;
                Session["RNewMemName"] = hdnNewMemberName.Value;

                Session["RGLCashCode"] = hdnGLCashCode.Value;

                Session["RTrnCode"] = hdnTrnCode.Value;

                Session["RAccType"] = hdnAccType.Value;
                Session["Rlblcls"] = hdnlblcls.Value;

                Session["RPeriod"] = "0";
                Session["RIntRate"] = txtIntRate.Text;
                Session["RMaturityDate"] = txtMaturityDate.Text;

                Session["RFixedDepAmt"] = txtFixedDepositAmount.Text;
                Session["RFixedMthInt"] = txtFixedMthInt.Text;
                Session["RBenefitDate"] = CtrlBenefitDate.Text;

                if (ChkContraInt.Checked == true)
                {
                    Session["RContractInt"] = "1";
                }
                else
                {
                    Session["RContractInt"] = "0";
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.href='CSDailyBoothTransaction.aspx';self.close();</script>");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnExit_Click Problem');</script>");
                //throw ex;
            }
        }




        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            if (hdnlblcls.Value == "2")
            {
                Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
                Int16 SlabFlag = Converter.GetSmallInteger(hdnmemType.Value);
                Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, 0, period));

                if (getDTO.NoRecord > 0)
                {
                    txtIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));

                    CtrlMatureAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));

                    DateTime Matdate = Converter.GetDateTime(hdnTranDate.Value);
                    //Matdate = DateTime.ParseExact(hdnTranDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                    DateTime dt = Converter.GetDateTime(Matdate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtMaturityDate.Text = Converter.GetString(date);

                }
                else
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Period not Assign in Parameter');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        txtPeriod.Text = string.Empty;
                        txtPeriod.Focus();

                    }
                    return;


                }
            }

            
        }

        protected void txtFixedDepositAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFixedDepositAmount.Text != "0")
                {
                    Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
                    Int16 SlabFlag = Converter.GetSmallInteger(hdnmemType.Value);
                    double depAmount = Converter.GetDouble(txtFixedDepositAmount.Text);
                    Int16 period = Converter.GetSmallInteger(txtPeriod.Text);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, depAmount, period));

                    if (getDTO.NoRecord > 0)
                    {
                        txtFixedDepositAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.DepositeAmount));
                        txtPeriod.Text = Converter.GetString(getDTO.Period);
                        txtFixedMthInt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));

                        DateTime Matdate = Converter.GetDateTime(hdnTranDate.Value);
                        Matdate = Matdate.AddMonths(Converter.GetSmallInteger(txtPeriod.Text));
                        DateTime dt = Converter.GetDateTime(Matdate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtMaturityDate.Text = Converter.GetString(date);

                        DateTime Benefitdate = Converter.GetDateTime(hdnTranDate.Value);
                        Benefitdate = Benefitdate.AddMonths(Converter.GetSmallInteger(1));
                        DateTime dt1 = Converter.GetDateTime(Benefitdate);
                        string date1 = dt1.ToString("dd/MM/yyyy");
                        CtrlBenefitDate.Text = Converter.GetString(date1);
                        txtPeriod.Focus();


                    }
                    else
                    {
                        {
                            String csname1 = "PopupScript";
                            Type cstype = GetType();
                            ClientScriptManager cs = Page.ClientScript;

                            if (!cs.IsStartupScriptRegistered(cstype, csname1))
                            {
                                String cstext1 = "alert('Fixed Deposit Amount not Assign in Parameter');";
                                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                                txtFixedDepositAmount.Text = string.Empty;
                                txtFixedDepositAmount.Focus();

                            }
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtFixedDepositAmount_TextChanged Problem');</script>");


                //throw ex;
            }
        }


        protected void BtnNominee_Click(object sender, EventArgs e)
        {
            Session["CUType"] = hdnCType.Value;
            Session["CUNo"] = hdnCNo.Value;
            Session["TypeCode"] = hdnAccType.Value;
            Session["MemNo"] = hdnNewMemberNo.Value;
            Session["AccNo"] = "0";
            Page.ClientScript.RegisterStartupScript(
             this.GetType(), "OpenWindow", "window.open('CSBoothNomineeMaint.aspx','_blank');", true);
        }



    }
}