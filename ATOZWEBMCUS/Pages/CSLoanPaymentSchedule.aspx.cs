using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanPaymentSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            txtLoanAmt.Focus();
            BtnPrint.Visible = false;
            txtNetPayable.ReadOnly = true;
            txtTotalIntAmt.ReadOnly = true;
            txtLastInstlAmt.ReadOnly = true;
        }

        double CalIntAmt = 0;
        double instlAmt = 0;
        double loanAmt = 0;
        double loanPayable = 0;
        double TotalIntAmt = 0;
        double TotalLoanPayable = 0;
        double sub = 0;
        int i = 0;
        public int no = 0;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoanAmt.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Loan Amount');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Loan Amount');", true);
                    return;
                }

                if (txtNoOfInstl.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input No. of Installment');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input No. of Installment');", true);
                    return;
                }

                if (txtIntRate.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Interest Rate');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Interest Rate');", true);
                    return;
                }

                string sqlquery3 = "Truncate table dbo.A2ZWFLOANSCHEDULE ";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));


                A2ZLOANSCHEDULEDTO objDTO = new A2ZLOANSCHEDULEDTO();
                no = Convert.ToInt32(txtNoOfInstl.Text);
                loanAmt = Convert.ToDouble(txtLoanAmt.Text);//loan amount
                double IntRate = Convert.ToDouble(txtIntRate.Text);


                ////txtLastInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}",Math.Round (sub)));
                //instlAmt = (loanAmt / no);
                //double real = instlAmt;
                //double fraction = real - Math.Floor(real);
                //double linstl = (no * fraction);
                //instlAmt = Math.Round(instlAmt);

                instlAmt = Convert.ToDouble(txtInstlAmt.Text);
                double sub = Convert.ToDouble(txtLastInstlAmt.Text);


                //txtLastInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}",Math.Round (sub)));

                for (i = 1; i <= no; i++)
                {
                    CalIntAmt = (loanAmt * IntRate) / 1200;

                    TotalIntAmt = (TotalIntAmt + CalIntAmt);
                    txtTotalIntAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", Math.Round(TotalIntAmt)));

                    if (i != no)
                    {
                        loanPayable = (instlAmt + CalIntAmt);
                    }
                    else
                    {
                        loanPayable = (sub + CalIntAmt);
                    }

                    TotalLoanPayable = (TotalLoanPayable + loanPayable);
                    txtNetPayable.Text = Convert.ToString(string.Format("{0:0,0.00}", Math.Round(TotalLoanPayable)));
                    objDTO.LoanMth = Converter.GetInteger(i);


                    if (i != no)
                    {
                        objDTO.InstlAmt = Converter.GetDecimal(instlAmt);
                    }
                    else
                    {
                        objDTO.InstlAmt = Converter.GetDecimal(sub);
                    }
                    objDTO.LoanAmt = Converter.GetDecimal(loanAmt);
                    objDTO.IntAmt = Converter.GetDecimal(CalIntAmt);
                    objDTO.LoanPayable = Converter.GetDecimal(loanPayable);
                    objDTO.SchduleLoan = Converter.GetDecimal(txtLoanAmt.Text);
                    objDTO.NoInstl = Converter.GetInteger(txtNoOfInstl.Text);
                    objDTO.IntRate = Converter.GetDecimal(txtIntRate.Text);
                    objDTO.instlAmt = Converter.GetDecimal(txtInstlAmt.Text);
                    objDTO.LastInstlAmt = Converter.GetDecimal(txtLastInstlAmt.Text);
                    objDTO.TotalIntAmount = Converter.GetDecimal(txtTotalIntAmt.Text);
                    objDTO.NetPayable = Converter.GetDecimal(txtNetPayable.Text);

                    int roweffect = A2ZLOANSCHEDULEDTO.InsertInformation(objDTO);
                    loanAmt = (loanAmt - instlAmt);


                }

                string qry = "UPDATE A2ZWFLOANSCHEDULE SET TotalIntAmount='" + txtTotalIntAmt.Text + "',NetPayable = '" + txtNetPayable.Text + "' ";
                int result1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
                gvDetail();
                BtnPrint.Visible = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT LoanMth,InstlAmt,LoanAmt,IntAmt,LoanPayable from A2ZWFLOANSCHEDULE ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void clearinfo()
        {
            txtNoOfInstl.Text = string.Empty;
            txtIntRate.Text = string.Empty;
            txtInstlAmt.Text = string.Empty;
            txtLastInstlAmt.Text = string.Empty;

        }

        protected void txtLoanAmt_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLoanAmt.Text);
            txtLoanAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));

            if (txtNoOfInstl.Text != "")
            {
                no = Convert.ToInt32(txtNoOfInstl.Text);

                loanAmt = Convert.ToDouble(txtLoanAmt.Text);//loan amount

                instlAmt = (loanAmt / no);
                instlAmt = Math.Round(instlAmt);

                double linstl = Math.Abs((instlAmt * (no - 1)) - loanAmt);

                txtInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", instlAmt));
                txtLastInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", linstl));
                BtnSubmit.Focus();
            }
            else
            {
                txtNoOfInstl.Focus();
            }
        }

        //protected void txtIntRate_TextChanged(object sender, EventArgs e)
        //{
        //    double ValueConvert = Converter.GetDouble(txtIntRate.Text);
        //    txtIntRate.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
        //    BtnSubmit.Focus();
        //}



        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtInstlAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtLoanAmt.Text == string.Empty)
            {
                txtInstlAmt.Text = string.Empty;
                txtLoanAmt.Focus();
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Loan Amount');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Loan Amount');", true);
                return;
            }
            
            
            
            double instlAmt1 = Convert.ToDouble(txtInstlAmt.Text);
            loanAmt = Convert.ToDouble(txtLoanAmt.Text);//loan amount
            double no = (loanAmt / instlAmt1);
            int NoInstl = Converter.GetInteger(Math.Round(no));
            txtNoOfInstl.Text = Convert.ToString(NoInstl);

            double linstl = Math.Abs((instlAmt1 * (NoInstl - 1)) - loanAmt);

            txtInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", instlAmt1));
            txtLastInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", linstl));
            BtnSubmit.Focus();
        }

        protected void txtNoOfInstl_TextChanged1(object sender, EventArgs e)
        {
            if (txtLoanAmt.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Loan Amount');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Loan Amount');", true);
                return;
            }
            
            
            no = Convert.ToInt32(txtNoOfInstl.Text);
            loanAmt = Convert.ToDouble(txtLoanAmt.Text);//loan amount

            instlAmt = (loanAmt / no);
            instlAmt = Math.Round(instlAmt);

            double linstl = Math.Abs((instlAmt * (no - 1)) - loanAmt);


            txtInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", instlAmt));
            txtLastInstlAmt.Text = Convert.ToString(string.Format("{0:0,0.00}", linstl));
            txtIntRate.Focus();
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //Decimal loanAmt = Converter.GetDecimal(txtLoanAmt.Text);
                //int Noinstl = Converter.GetInteger(txtNoOfInstl.Text);
                //Decimal Irate = Converter.GetDecimal(txtIntRate.Text);
                //Decimal instlAmt = Converter.GetDecimal(txtInstlAmt.Text);
                //Decimal LastinstlAmt = Converter.GetDecimal(txtLastInstlAmt.Text);
                //Decimal TotallAmt = Converter.GetDecimal(txtTotalIntAmt.Text);
                //Decimal NetPayAmt = Converter.GetDecimal(txtNetPayable.Text);
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, loanAmt);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, Noinstl);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, Irate);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, instlAmt);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO5, LastinstlAmt);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO6, TotallAmt);
                //SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO7, NetPayAmt);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptLoanCalculateReport");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }


    }
}