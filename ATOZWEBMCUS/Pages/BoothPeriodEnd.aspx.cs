using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ATOZWEBMCUS.Pages
{
    public partial class BoothPeriodEnd : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("master"));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    int checkAllUser = DataAccessLayer.DTO.A2ZSYSIDSDTO.CountForSingleUserPurpose(Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID)), "A2ZGLMCUS");

                    if (checkAllUser > 0)
                    {
                        //string strQuery = "SELECT IdsNo, IdsName, EmpCode, IdsFlag, IdsLogInFlag FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0";
                        //gvUserInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserInfo, "A2ZGLMCUS");


                        btnProcess.Enabled = false;


                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not Process Period End - System Not In Single User');", true);
                        return;
                    }

                    lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    var dt = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime processDate = dt.ProcessDate;
                    string date = processDate.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date;


                    lblNewYear.Text = Converter.GetString(dt.CurrentYear);

                    txtDayEnd.Focus();


                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:D}", processDate));

                    A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                    hdndatapath.Text = Converter.GetString(dto.PrmDataPath);

                    lblEndOfMonth.Visible = false;
                    //lblYearEnd.Visible = false;

                    //int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                    //if (processDate.Day == lastDay)
                    //{
                    //    lblEndOfMonth.Visible = true;

                    //    if (dt.ProcessDate.Month == 12)
                    //    {
                    //        lblYearEnd.Visible = true;
                    //    }
                    //}

                    //A2ZGLPARAMETERDTO.UpdateSingleUserFlag(1);

                    CheckDebitCreditTransaction();


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        protected void CheckDebitCreditTransaction()
        {
            try
            {
                DateTime prodate = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string query = "SELECT SUM(GLDebitAmt) as TrnDebit,SUM(GLCreditAmt) as TrnCredit FROM A2ZTRANSACTION WHERE TrnDate='" + prodate + "' AND FromCashCode='" + lblCashCode.Text + "'";
                DataTable dat = CommonManager.Instance.GetDataTableByQuery(query, "A2ZCSMCUS");
                int totrec1 = dat.Rows.Count;

                if (dat.Rows.Count > 0)
                {
                    foreach (DataRow dar in dat.Rows)
                    {
                        var DebitAmt = dar["TrnDebit"].ToString();
                        var CreditAmt = dar["TrnCredit"].ToString();
                        decimal debit = Converter.GetDecimal(DebitAmt);
                        decimal credit = Converter.GetDecimal(CreditAmt);
                        lblTotalDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", debit));
                        lblTotalCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", credit));

                        if (debit == 0 && credit == 0)
                        {
                            ddlProcessType.SelectedValue = "2";
                        }


                        if (debit != credit)
                        {
                            txtDayEnd.ReadOnly = true;
                            DebitCreditMSG();
                            return;
                        }
                        else
                        {
                            txtDayEnd.ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.CheckUnPostTransaction Problem');</script>");
                //throw ex;
            }
        }

        protected void CheckUnPostTransaction()
        {
            try
            {

                string qry1 = "SELECT TrnProcStat FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND FromCashCode='" + lblCashCode.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    UnpostMSG();
                    CtrlTranStat.Text = "1";
                }
                else
                {
                    CtrlTranStat.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.CheckUnPostTransaction Problem');</script>");
                //throw ex;
            }
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            A2ZGLPARAMETERDTO.UpdateSingleUserFlag(0);
            if (CtrlProcFlag.Text == "0" || CtrlProcFlag.Text == "")
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
            else
            {
                Response.Redirect("A2ZStartUp.aspx");
            }
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {

        }



        protected void UpdateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Day End Process Sucessfully Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Process Done Sucessfully');", true);
            return;

        }
        protected void UnpostMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Have a Un-Post Transaction');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Have a Un-Post Transaction');", true);
            return;

        }


        protected void DebitCreditMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Have a Un-Post Transaction');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Total Debit & Credit Not Match');", true);
            return;

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtDayEnd.Text == string.Empty)
                {
                    txtDayEnd.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input PROCESS DONE');", true);
                    return;

                }

                if (txtDayEnd.Text == "PROCESS DONE")
                {
                    CtrlProcFlag.Text = "0";

                    CheckUnPostTransaction();
                    if (CtrlTranStat.Text == "1")
                    {
                        return;
                    }


                    ProcessEOD();
                }
                else
                {
                    txtDayEnd.Text = string.Empty;
                    txtDayEnd.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input PROCESS DONE');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnProcess_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ProcessEOD()
        {

            btnProcess.Enabled = false;
            UpdateEODStat();

            CtrlProcFlag.Text = "1";
            txtDayEnd.Text = string.Empty;
            UpdateMSG();

            //Response.Redirect("A2ZERPModule.aspx");
            //Response.Redirect("A2ZStartUp.aspx");

        }


        protected void UpdateEODStat()
        {
            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime Procdt = Converter.GetDateTime(dto.ProcessDate);

                string strqry1 = "UPDATE A2ZBTRNCTRL SET Status='" + ddlProcessType.SelectedValue + "',StatusName='" + ddlProcessType.SelectedItem + "' WHERE ProcessDate='" + Procdt + "' AND CashCodeNo='" + lblCashCode.Text + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry1, "A2ZCSMCUS"));
                if (rowEffect1 > 0)
                {
                    string strqry2 = "UPDATE A2ZUSERCASHCODE SET Status='" + ddlProcessType.SelectedValue + "' WHERE IDSNO='" + lblID.Text + "' AND FromCashCode='" + lblCashCode.Text + "'";
                    int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry2, "A2ZCSMCUS"));
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }




    }
}