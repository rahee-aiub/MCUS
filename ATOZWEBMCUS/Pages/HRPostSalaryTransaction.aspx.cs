using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.Utility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRPostSalaryTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    //lblTotalAmt.Visible = false;


                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    var dt = A2ZHRPARAMETERDTO.GetParameterValue();

                    DateTime processDate = dt.ProcessDate;

                    string date = processDate.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;


                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", processDate));

                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        protected void InvalidMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Function Select');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Function Select');", true);
            return;

        }

        protected void PostedMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "Salary Posted Sucessfully Done";
            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            
            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            
            
            
            //------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Salary Posted Sucessfully Done";


            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVchNo.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append(c);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void ReverseMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Salary Reverse Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Salary Reverse Done');", true);
            return;

        }

        private void InvalidPostMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Salary Already Posted');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Monthly Salary Prepared Not Done');", true);
            return;
        }

        private void InvalidReverseMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Salary Not Posted');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Salary Not Posted');", true);
            return;
        }

        private void InvalidVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Input Voucher No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Voucher No.');", true);
            return;
        }

        protected void DuplicateVchMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Voucher Already Exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;

        }

        protected void TrnVchDeplicate()
        {
            VerifyFlag.Text = "0";
            
            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                VerifyFlag.Text = "1";
                DuplicateVchMSG();
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                return;
            }
        }
        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtVchNo.Text == string.Empty && ddlFuncProceed.SelectedValue == "1")
                {
                    InvalidVchMSG();
                    txtVchNo.Focus();
                    return;
                }
                
               
                if (ddlFuncProceed.SelectedValue == "0")
                {
                    InvalidMSG();
                    return;
                }

                if (ddlFuncProceed.SelectedValue == "1")
                {
                    TrnVchDeplicate();
                    if (VerifyFlag.Text == "1")
                    {
                        return;
                    }
                }


                A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                CtrlSalPostStat.Text = Converter.GetString(dto.PrmSalPostStat);

                if (CtrlSalPostStat.Text == "0" && ddlFuncProceed.SelectedValue == "1")
                {
                    ddlFuncProceed.SelectedIndex = 0;
                    InvalidPostMSG();
                    return;
                }

                if (ddlFuncProceed.SelectedValue == "2")
                {
                    VerifyReverseSalary();

                    if (VerifyFlag.Text == "1")
                    {
                        ddlFuncProceed.SelectedIndex = 0;
                        return;
                    }
                }

                if (ddlFuncProceed.SelectedValue == "1")
                {
                    VerifyPostSalary();

                    if (VerifyFlag.Text == "1")
                    {
                        ddlFuncProceed.SelectedIndex = 0;
                        return;
                    }
                }
                
                if (ddlFuncProceed.SelectedValue == "1")
                {
                    int GLCode = Converter.GetInteger(hdnCashCode.Text);
                    Int16 RecType = Converter.GetSmallInteger(1);
                    A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                    CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;
                }
                var prm = new object[4];

                prm[0] = CtrlVchNo.Text;
                prm[1] = txtVchNo.Text;
                prm[2] = hdnID.Text;
                prm[3] = hdnCashCode.Text;

                if (ddlFuncProceed.SelectedValue == "1")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRPostSalaryTransaction", prm, "A2ZHRMCUS"));
                    if (result == 0)
                    {
                        UpdateSalPostStat();
                        PostedMSG();
                        //return;
                    }
                }

                if (ddlFuncProceed.SelectedValue == "2")
                {
                    
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRReverseSalaryTransaction", "A2ZHRMCUS"));
                    if (result == 0)
                    {
                        ReverseSalPostStat();
                        ReverseMSG();
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnProcess_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void UpdateSalPostStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateSalPostStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateSalPostStat Problem');</script>");
                //throw ex;
            }

        }

        protected void ReverseSalPostStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateSalPostStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateSalPostStat Problem');</script>");
                //throw ex;
            }

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlFuncProceed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFuncProceed.SelectedValue == "1")
            {
                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);              
                int lastDay = DateTime.DaysInMonth(opdate.Year, opdate.Month);
                opdate = new DateTime(opdate.Year, opdate.Month, lastDay);
                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", opdate));
                   
                lblVchNo.Visible = true;
                txtVchNo.Visible = true;
                txtVchNo.Focus();
            }
            else 
            {
                DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                opdate = opdate.AddMonths(-1);
                int lastDay = DateTime.DaysInMonth(opdate.Year, opdate.Month);
                opdate = new DateTime(opdate.Year, opdate.Month, lastDay);
                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", opdate));

                lblVchNo.Visible = false;
                txtVchNo.Visible = false;
            }
        }



        private void VerifyReverseSalary()
        {
            VerifyFlag.Text = "0";

            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            opdate = opdate.AddMonths(-1);
            int lastDay = DateTime.DaysInMonth(opdate.Year, opdate.Month);
            opdate = new DateTime(opdate.Year, opdate.Month, lastDay);
           

            string qry = "SELECT Id,SalDate FROM A2ZTRANSACTION WHERE SalDate = '" + opdate + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                
            }
            else 
            {
                DateTime opdate1 = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int lastDay1 = DateTime.DaysInMonth(opdate1.Year, opdate1.Month);
                opdate = new DateTime(opdate1.Year, opdate1.Month, lastDay1);
                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", opdate1));

                VerifyFlag.Text = "1";
                InvalidReverseMSG();
                return;
            }
        }


        private void VerifyPostSalary()
        {
            VerifyFlag.Text = "0";

            DateTime opdate = DateTime.ParseExact(CtrlProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT Id,EmpCode,EmpName,SalDate,NetPayment FROM A2ZEMPFSALARY WHERE SalDate = '" + opdate + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var id = dr["Id"].ToString();
                    var empcode = dr["EmpCode"].ToString();
                    var empname = dr["EmpName"].ToString();
                    var saldate = dr["SalDate"].ToString();
                    var netpayment = dr["NetPayment"].ToString();
                    double netpay = Converter.GetDouble(netpayment);

                    lblPerNo.Text = empcode;
                    lblPerName.Text = empname;

                    if (netpay < 0)
                    {
                        VerifyFlag.Text = "1";
                        VerifyMSG();
                        return;
                    }

                }
            }
            else
            {
                VerifyFlag.Text = "1";
                InvalidPostMSG();
                return;
            }
        }

        protected void VerifyMSG()
        {
            string Msg = "";

            string a = "NET PAY AMOUNT LESS THEN GROSS AMOUNT";
            string b = "Per No. :";
            string c = lblPerNo.Text;
            string d = lblPerName.Text;

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c + " " + d;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;


        }

    }
}