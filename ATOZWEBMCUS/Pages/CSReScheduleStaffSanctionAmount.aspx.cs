using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSReScheduleStaffSanctionAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AccTypedropdown1();
                MemberDropdown();

                ddlAccNo.Visible = false;

                ItxtMemNo.Focus();

                               

                
                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                string Ctrflag = (string)Session["flag"];
                lblCtrlFlag.Text = Ctrflag;

                           

             
            }
        }


        private void AccTypedropdown1()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE where AccTypeClass =6 AND AccTypeMode ='2'";
            ddlIAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIAccType, "A2ZCSMCUS");
        }

        private void MemberDropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER WHERE CuNo = 0 and CuType = 0 GROUP BY MemNo,MemName";
            ddlLoanMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqquery, ddlLoanMemNo, "A2ZCSMCUS");
        }


        //protected void gvDetals()
        //{
        //    string sqlquery3 = "SELECT SanctionDate,NewSanctionAmt,NewIntRate,Note FROM A2ZLOANHST WHERE CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' AND DistbursmentAmt = 0";

        //    gvDetailsInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailsInfo, "A2ZCSMCUS");
        //}


        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZACGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZCSMCUS"));

            string ShareQry = "DELETE  dbo.WFA2ZSHGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ShareQry, "A2ZCSMCUS"));

            string PropertyQry = "DELETE dbo.WFA2ZPRGUAR WHERE UserId='" + hdnID.Text + "'";
            int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(PropertyQry, "A2ZCSMCUS"));

        }

        protected void SessionRemove()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["ViewFlag"] = string.Empty;

            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;


            Session["SSPflag"] = string.Empty;
            //Session["SAccTypeGuaranty"] = string.Empty;


            //Session["SpnlDeposit"] = string.Empty;
            //Session["SpnlShare"] = string.Empty;

            //Session["SlblTotalResult"] = string.Empty;
            //Session["StxtTotalAmt"] = string.Empty;
            //Session["SlblShareTotalAmt"] = string.Empty;
            //Session["SlblSumProperty"] = string.Empty;


        }


        protected void ddlLoanMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoanMemNo.SelectedValue != "-Select-")
            {

                int MemNumber = Converter.GetInteger(ddlLoanMemNo.SelectedValue);

                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                getDTO = (A2ZMEMBERDTO.GetInformation(0, 0, MemNumber));

                if (getDTO.NoRecord > 0)
                {
                    ItxtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    txtAccType.Focus();
                }



            }
        }


        protected void ItxtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (ItxtMemNo.Text != string.Empty)
                {

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(ItxtMemNo.Text);
                    int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        ItxtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        ddlLoanMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        txtAccType.Focus();
                       

                    }
                    else
                    {
                        ItxtMemNo.Text = string.Empty;
                        ddlLoanMemNo.SelectedIndex = 0;
                        ItxtMemNo.Focus();
                        InvalidMemMSG();
                    }

                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtMemNo_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            if (txtAccType.Text != string.Empty)
            {
                Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                    lblAccTypeClass.Text = Converter.GetString(getDTO.AccTypeClass);
                    lblIAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);

                    if (lblAccTypeClass.Text == "6" && lblIAccTypeMode.Text == "2")
                    {
                        ddlIAccType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);

                        FindAccountNo();
                        if (MSGFlag.Text == "1")
                        {
                            AccountNotExistsMSG();

                            txtAccType.Text = string.Empty;
                            ddlIAccType.SelectedIndex = 0;

                            txtAccType.Focus();
                            return;
                        }
                    }
                    else 
                    {
                        txtAccType.Text = string.Empty;
                        txtAccType.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
                        return;
                    }
                }
                else
                {
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
                    return;
                }
            }
        }


        protected void ddlIAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Int16 AccType = Converter.GetSmallInteger(ddlIAccType.SelectedValue);
                A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                if (get1DTO.AccTypeCode > 0)
                {
                    txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
                    lblAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                   

                    FindAccountNo();
                    if (MSGFlag.Text == "1")
                    {
                        AccountNotExistsMSG();

                        txtAccType.Text = string.Empty;
                        ddlIAccType.SelectedIndex = 0;

                        txtAccType.Focus();
                        return;
                    }

                    

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        private void FindAccountNo()
        {
            MSGFlag.Text = "0";

            string qry = "SELECT AccNo FROM A2ZACCOUNT where CuType=0 and CuNo=0 and MemNo='" + ItxtMemNo.Text + "' and AccType='" + ddlIAccType.SelectedValue + "' AND AccStatus < 98";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ItxtAccNo.Visible = true;
                        ddlAccNo.Visible = false;

                        ItxtAccNo.Text = dr["AccNo"].ToString();
                        ItxtAccNo_TextChanged(this, EventArgs.Empty);
                    }
                }
                else
                {
                    ItxtAccNo.Visible = false;
                    ddlAccNo.Visible = true;

                    string sqlquery = "SELECT Id,AccNo from A2ZACCOUNT WHERE CuType=0 and CuNo=0 and MemNo='" + ItxtMemNo.Text + "' and AccType='" + ddlIAccType.SelectedValue + "' AND AccStatus < 98 ORDER BY AccNo";
                    ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccNo, "A2ZCSMCUS");
                }
            }
            else
            {
                MSGFlag.Text = "1";
            }


        }


        protected void ItxtAccNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetInfo();

                if (MSGFlag.Text == "1")
                {
                    return;
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccountNo_TextChanged Problem');</script>");


            }
        }

        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ItxtAccNo.Text = Converter.GetString(ddlAccNo.SelectedItem);
                GetInfo();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccountNo_TextChanged Problem');</script>");


            }
        }

        protected void GetInfo()
        {
            try
            {
                MSGFlag.Text = "0";

                Int64 AccNumber = Converter.GetLong(ItxtAccNo.Text);
                A2ZACCOUNTDTO getDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));

                if (getDTO.a > 0)
                {
                    if (getDTO.AccStatus == 99)
                    {
                        ItxtAccNo.Text = string.Empty;
                        ItxtAccNo.Focus();
                        MSGFlag.Text = "1";

                        AccClosedMSG();
                        return;
                    }
                    else
                    {
                        lblExLoanBal.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccBalance));
                        decimal balance = Converter.GetDecimal(lblExLoanBal.Text);

                        ////if (balance < 0)
                        ////{
                        ////    ItxtAccNo.Text = string.Empty;
                        ////    ItxtAccNo.Focus();
                        ////    MSGFlag.Text = "1";
                        ////    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Loan Balance Not Zero');", true);
                        ////    return;
                        ////}


                        IlblExSancAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LoanAmount));
                        lblExDisbAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccDisbAmt));

                        IlblExNoInstl.Text = Converter.GetString(getDTO.NoInstallment);
                        ItxtNewNoInstal.Text = Converter.GetString(getDTO.NoInstallment);
                        lblExistInstlAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MonthlyInstallment));
                        lblExistLastInstlAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LastInstallment));
                        IlblExIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));
                        ItxtNewInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.InterestRate));
                        ItxtIncreaseSanctionAmount.Focus();


                    }

                }
                else
                {
                    ItxtAccNo.Text = string.Empty;
                    ItxtAccNo.Focus();
                    MSGFlag.Text = "1";

                    AccountNotExistsMSG();
                    return;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetInfo Problem');</script>");


            }
        }
        private void InvalidAccTypeMSG()
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
        private void InvalidCUMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        private void InvalidMemMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        private void InvalidSancAmtMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid New Sanction Amount');", true);
            return;
        }

        private void InvalidIntRateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid New Interest Rate');", true);
            return;
        }

        private void AccountNotExistsMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not In File');", true);
            return;

        }

        private void AccClosedMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Already Closed');", true);
            return;

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove();
            Response.Redirect("A2ZERPModule.aspx");

        }

        protected void Success()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('New Sanction Amount Added Successfully');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('New Sanction Amount Added Successfully.');", true);

        }

        protected void ClearInfo()
        {
 
            ItxtMemNo.Text = string.Empty;
            ddlLoanMemNo.SelectedIndex = 0;
            txtAccType.Text = string.Empty;
            ddlIAccType.SelectedIndex = 0;
            ItxtAccNo.Text = string.Empty;

         
            ItxtIncreaseSanctionAmount.Text = string.Empty;
            IlblExNoInstl.Text = string.Empty;
            lblExistInstlAmt.Text = string.Empty;
            lblExistLastInstlAmt.Text = string.Empty;
            IlblExSancAmt.Text = string.Empty;
            IlblExIntRate.Text = string.Empty;
           
            ItxtIncreaseSanctionAmount.Text = string.Empty;
            ItxtNewNoInstal.Text = string.Empty;
            ItxtNewInstalAmt.Text = string.Empty;
            ItxtNewLastInstalAmt.Text = string.Empty;
            ItxtNewInterestRate.Text = string.Empty;

            lblNewLoanAmt.Text = string.Empty;

        }

        protected void ItxtIncreaseSanctionAmount_TextChanged(object sender, EventArgs e)
        {
            double ESamt = Converter.GetDouble(IlblExSancAmt.Text);
            double Damt = Converter.GetDouble(lblExDisbAmt.Text);


            double Iamt = Converter.GetDouble(ItxtIncreaseSanctionAmount.Text);
            double Eamt = Math.Abs((Converter.GetDouble(lblExLoanBal.Text)));

            //double Xamt = (Eamt + Iamt) + (ESamt - Damt);

            //double NSamt = (ESamt + Iamt);


            double NSamt = (Iamt - Eamt);

            lblNewSanctionAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", NSamt));

            ItxtIncreaseSanctionAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", Iamt));

            lblNewLoanAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", NSamt));

            lblDisbAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", Eamt));


            GenerateInstallmentAmt();



        }

        protected void GenerateInstallmentAmt()
        {
            Int16 RoundFlag = 0;
            double a = Converter.GetDouble(ItxtIncreaseSanctionAmount.Text);
            double b = Converter.GetDouble(ItxtNewNoInstal.Text);
            double c = a / b;

            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + ddlIAccType.SelectedValue + "'", "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                if (RoundFlag == 1)
                {
                    c = Math.Round(c);

                }

                if (RoundFlag == 2)
                {
                    c = Math.Ceiling(c);
                }

            }

            

            //int TrnAmt = Converter.GetInteger(c);

            //int DepRoundingBy = 100;


            //int mod = TrnAmt % DepRoundingBy;

            //if (mod != 0)
            //{
            //    if (mod < 50)
            //    {
            //        c = (c - mod);
            //    }
            //    else
            //    {
            //        c = ((c - mod) + 100);
            //    }
            //}

            //------------------


            ItxtNewInstalAmt.Text = Converter.GetString(string.Format("{0:0,0.00}", c));

            lblPrevInstlAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", c));



            double d = Math.Abs((c * (b - 1)) - a);

            ItxtNewLastInstalAmt.Text = Converter.GetString(string.Format("{0:0,0.00}", d));
        }

        protected void BtnNewSanction_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItxtMemNo.Text == string.Empty)
                {
                    ItxtMemNo.Text = string.Empty;
                    ItxtMemNo.Focus();
                    InvalidMemMSG();
                    return;
                }

                if (ddlIAccType.SelectedValue == "-Select-")
                {
                    InvalidAccTypeMSG();
                    return;
                }

                if (ItxtIncreaseSanctionAmount.Text == string.Empty)
                {
                    ItxtIncreaseSanctionAmount.Text = string.Empty;
                    ItxtIncreaseSanctionAmount.Focus();
                    InvalidSancAmtMSG();
                    return;
                }

                if (ItxtNewInterestRate.Text == string.Empty)
                {
                    ItxtNewInterestRate.Text = string.Empty;
                    ItxtNewInterestRate.Focus();
                    InvalidIntRateMSG();
                    return;
                }

                A2ZLOANHSTDTO objDTO = new A2ZLOANHSTDTO();
                objDTO.AccType = Converter.GetInteger(ddlIAccType.SelectedValue);
                objDTO.AccNo = Converter.GetLong(ItxtAccNo.Text);
                objDTO.CuType = Converter.GetSmallInteger(0);
                objDTO.CuNo = Converter.GetInteger(0);
                objDTO.MemberNo = Converter.GetInteger(ItxtMemNo.Text);
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.SanctionDate = Converter.GetDateTime(dto.ProcessDate);
                objDTO.PrevSanctionAmount = Converter.GetDecimal(IlblExSancAmt.Text);
                objDTO.PrevIntRate = Converter.GetDecimal(IlblExIntRate.Text);

                objDTO.PrevNoInstl = Converter.GetInteger(IlblExNoInstl.Text);
                objDTO.PrevLoanInstlAmt = Converter.GetDecimal(lblExistInstlAmt.Text);
                objDTO.PrevLoanLastInstlAmt = Converter.GetDecimal(lblExistLastInstlAmt.Text);

                objDTO.ApproveBy = Converter.GetSmallInteger(hdnID.Text);


                int result = A2ZLOANHSTDTO.InsertRSInformation(objDTO);
                if (result > 0)
                {
                    string strQuery = "UPDATE A2ZACCOUNT SET  AccLoanSancAmt = '" + ItxtIncreaseSanctionAmount.Text + "',AccDisbAmt = '" + lblDisbAmt.Text + "', AccLoanInstlAmt= '" + ItxtNewInstalAmt.Text + "',AccLoanLastInstlAmt= '" + ItxtNewLastInstalAmt.Text + "',AccNoInstl= '" + ItxtNewNoInstal.Text + "',AccIntRate= '" + ItxtNewInterestRate.Text + "' WHERE  CuType=0 AND CuNo=0 AND MemNo='" + ItxtMemNo.Text + "' AND  AccType='" + ddlIAccType.SelectedValue + "' AND AccNo='" + ItxtAccNo.Text + "'";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    Success();

                  
                    ClearInfo();

                    ItxtMemNo.Focus();

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnNewSanction_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void gvDetailsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void ItxtNewInterestRate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            Session["ExFlag"] = "7";
            Session["ViewFlag"] = "1";

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }

       

        
       

        

        protected void ItxtNewNoInstl_TextChanged(object sender, EventArgs e)
        {
            GenerateInstallmentAmt();
        }


        protected void ItxtNewInstalAmt_TextChanged(object sender, EventArgs e)
        {
            double a = Converter.GetDouble(ItxtIncreaseSanctionAmount.Text);
            double b = Converter.GetDouble(ItxtNewInstalAmt.Text);
            double c = Converter.GetDouble(ItxtNewNoInstal.Text);

            //double d = Math.Abs((b * (c - 1)) - a);
            double d = ((b * (c - 1)) - a);

            if (d > 0)
            {
                ItxtNewInstalAmt.Text = string.Empty;
                ItxtNewInstalAmt.Text = lblPrevInstlAmount.Text;
                ItxtNewInstalAmt.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid First Installment Amount');", true);
                return;
            }
            else 
            {
                d = Math.Abs(d);
            }


            ItxtNewLastInstalAmt.Text = Converter.GetString(string.Format("{0:0,0.00}", d));

        }

        
        


    }
}