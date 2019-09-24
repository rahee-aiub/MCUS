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
    public partial class GLPeriodEnd : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("master"));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    //if (userPermission != 30)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) + "&txtTwo=" + "You Don't Have Permission for Approve" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}

                    int checkAllUser = DataAccessLayer.DTO.A2ZSYSIDSDTO.CountForSingleUserPurpose(Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID)), "A2ZGLMCUS");

                    if (checkAllUser > 0)
                    {
                        string strQuery = "SELECT IdsNo, IdsName, EmpCode, IdsFlag, IdsLogInFlag FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0";
                        gvUserInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserInfo, "A2ZGLMCUS");

                        //btnProcess.BackColor = Color.Black;
                        btnProcess.Enabled = false;


                        //string msg = "Can not Process Period End - System Not In Single User";

                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('" + msg + "');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not Process Period End - System Not In Single User');", true);
                        return;
                    }


                    var dt = A2ZGLPARAMETERDTO.GetParameterValue();
                    DateTime processDate = dt.ProcessDate;
                    string date1 = processDate.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date1;
                  
                    lblNewYear.Text = Converter.GetString(dt.CurrentYear);

                    txtDayEnd.Focus();


                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:D}", processDate));

                   
                    A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                    hdndatapath.Text = Converter.GetString(dto.PrmDataPath);

                    lblEndOfMonth.Visible = false;
                    //lblYearEnd.Visible = false;

                    lblYearClose.Text = "0";

                    int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                    if (processDate.Day == lastDay)
                    {
                        lblEndOfMonth.Visible = true;

                        if (dt.ProcessDate.Month == 12)
                        {
                            lblYearEnd.Visible = true;
                        }
                        if (dt.ProcessDate.Month == 6)
                        {
                            lblYearClose.Text = "1";
                        }
                    }

                    A2ZGLPARAMETERDTO.UpdateSingleUserFlag(1);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void CheckUnPostTransaction()
        {
            try
            {

                string qry1 = "SELECT TrnProcStat FROM A2ZTRANSACTION WHERE TrnProcStat = 1";
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

        protected void BackUpMSG()
        {
            

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Database Backup Not Done');", true);
            return;

        }

        protected void UpdateMSG()
        {
            

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Day End Process Sucessfully Done');", true);
            return;

        }
        protected void UnpostMSG()
        {
            

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Have a Un-Post Transaction');", true);
            return;

        }

        protected void UnpostSalMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Current Month Salary Not Posted');", true);
            return;

        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                               
                if (txtDayEnd.Text == string.Empty)
                {
                    txtDayEnd.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
                    return;

                }

                if (txtDayEnd.Text == "END OF DAY")
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
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
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
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();

            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);
            CtrlSalPostStat.Text = Converter.GetString(dto.PrmSalPostStat);

            if (CtrlBackUpStat.Text == "0")
            {
                BackUpMSG();
                return;
            }
            else
            {
                int periodFlag = 1;

                if (lblEndOfMonth.Visible)
                {
                    periodFlag = 2;
                }
                if (lblYearEnd.Visible)
                {
                    periodFlag = 3;
                }

                //if (CtrlSalPostStat.Text == "0" && periodFlag != 1)
                //{
                //    UnpostSalMSG();
                //    return;
                //}

                var prm = new object[2];
                prm[0] = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                prm[1] = periodFlag;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLPeriodEnd", prm, "A2ZGLMCUS"));

                if (result == 0)
                {
                    //if (periodFlag == 2)
                    //{
                    //    InitialSalPostStat();
                    //}
                    
                    if (periodFlag == 3)
                    {
                        CreateNewYearDatabase();
                    }
                    //btnProcess.BackColor = Color.Black;
                    btnProcess.Enabled = false;
                    UpdateBackUpStat();
                    UpdateEODStat();

                    if (lblYearClose.Text == "1")
                    {
                        UpdateYearEndStat();
                    }



                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = 0 WHERE IdsNo = " + hdnID.Text;
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZGLMCUS"));
                    if (rowEffect > 0)
                    {
                        CtrlProcFlag.Text = "1";
                        txtDayEnd.Text = string.Empty;
                        UpdateMSG();

                        //Response.Redirect("A2ZERPModule.aspx");
                        //Response.Redirect("A2ZStartUp.aspx");
                    }


                }

            }
        }


        protected void InitialSalPostStat()
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

        protected void UpdateBackUpStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateBackUpStat Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdateEODStat()
        {
            try
            {
                Int16 BStat = 1;

                int roweffect = A2ZERPSYSPRMDTO.UpdateEODStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdateYearEndStat()
        {
            try
            {
                Int16 BStat = 1;

                int roweffect = A2ZERPSYSPRMDTO.UpdateYearEndStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }
        protected void UpdateDatabseFile()
        {
            try
            {
                A2ZDATABASEDTO objDTO = new A2ZDATABASEDTO();

                objDTO.DatabaseName = Converter.GetString(hdndbname.Text);

                int roweffect = A2ZDATABASEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    
                }



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        public void CreateNewYearDatabase()
        //protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZCSMCUST" + financialYr;
                hdndbname.Text = DatabaseName;
                //string datapath = "E:/";
                string str = "CREATE DATABASE " + DatabaseName + " ON PRIMARY " +
                         "(NAME = MyDataBaseName, " +
                         "FILENAME = '" + hdndatapath.Text + "" + DatabaseName + ".mdf', " +
                         "SIZE = 3MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10%) " +
                         "LOG ON (NAME = MyDataBaseName_log, " +
                        "FILENAME = '" + hdndatapath.Text + "" + DatabaseName + "_log.ldf', " +
                        "SIZE = 1MB, " +
                        "MAXSIZE = UNLIMITED, " +
                        "FILEGROWTH = 10%)";

                //string str = "CREATE DATABASE " + DatabaseName + "";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();


                //var prm = new object[1];
                //prm[0] = DatabaseName;
                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLYearEnd",prm, "A2ZGLMCUS"));
                con.Dispose();
                con.Close();
                string CSOpBaltblename = "A2ZCSOPBALANCE";
                CSOPCreate(CSOpBaltblename);
                string GLOpBaltblename = "A2ZGLOPBALANCE";
                GLOPCreate(GLOpBaltblename);
                string Trntblename = "A2ZTRANSACTION";
                TrnCreate(Trntblename);

                UpdateDatabseFile();


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }

        public void CSOPCreate(string TName)
        {
            SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString(hdndbname.Text));

            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[" + TName + "]("
                                + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                                + "[TrnDate] [smalldatetime] NOT NULL,"
                                + "[CuType] [int] NULL,"
                                + "[CuNo] [int] NULL,"
                                + "[MemNo]  [int] NULL,"
                                + "[AccType] [tinyint] NULL,"
                                + "[AccNo] [bigint] NULL,"
                                + "[TrnCode] [int] NULL,"
                                + "[TrnAmount] [money] NULL,"
                                + "[CuOldNo] [int] NULL,"
                                + "[TrnOwnAmt] [money] NULL,"
                                + "[TrnOfficeAmt] [money] NULL,"
                                + "[TrnInterestAmt] [money] NULL,"
                                + "[AccOpenDate] [smalldatetime] NOT NULL,"
                                + "[NewAccNo] [bigint] NULL,"
                                + "CONSTRAINT [PK_" + TName + "] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", conn))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GLOPCreate(string TName)
        {
            SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString(hdndbname.Text));

            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[" + TName + "]("
                                + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                                + "[GLCoNo]  [tinyint] NULL,"
                                + "[GLAccType] [tinyint] NULL,"
                                + "[GLAccNo] [int] NULL,"
                                + "[GLRecType]  [tinyint] NULL,"
                                + "[GLPrtPos] [tinyint] NULL,"
                                + "[GLOpBal] [money] NULL,"
                                + "[GLDrSumC] [money] NULL,"
                                + "[GLDrSumT] [money] NULL,"
                                + "[GLCrSumC] [money] NULL,"
                                + "[GLCrSumT] [money] NULL,"
                                + "[GLClBal]  [money] NULL,"
                                + "[GLOldAccNo] [int] NULL,"
                                + "CONSTRAINT [PK_" + TName + "] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", conn))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void TrnCreate(string TName)
        {
            SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString(hdndbname.Text));

            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[" + TName + "]("
                                + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                                + "[BatchNo]  [int] NULL,"
                                + "[TrnDate] [smalldatetime] NULL,"
                                + "[VchNo] [nvarchar](20) NULL,"
                                + "[VoucherNo]  [nvarchar](20) NULL,"
                                + "[CuType] [smallint] NULL,"
                                + "[CuNo] [int] NULL,"
                                + "[MemNo][int] NULL,"
                                + "[AccType] [int] NULL,"
                                + "[AccNo] [bigint] NULL,"
                                + "[TrnCode] [int] NULL,"
                                + "[FuncOpt]  [smallint] NULL,"
                                + "[FuncOptDesc] [nvarchar](100) NULL,"
                                + "[PayType] [int] NULL,"
                                + "[TrnType] [tinyint] NULL,"
                                + "[TrnDrCr] [tinyint] NULL,"
                                + "[TrnDebit] [money] NULL ,"
                                + "[TrnCredit] [money] NULL,"
                                + "[TrnDesc] [nvarchar](100) NULL,"
                                + "[TrnVchType] [nvarchar](1) NULL,"
                                + "[TrnChqPrx] [nvarchar](7) NULL,"
                                + "[TrnChqNo] [nvarchar](15) NULL,"
                                + "[ShowInterest] [tinyint] NULL,"
                                + "[TrnInterestAmt] [money] NULL,"
                                + "[TrnPenalAmt] [money] NULL,"
                                + "[TrnChargeAmT] [money] NULL,"
                                + "[TrnDueIntAmt] [money] NULL,"
                                + "[TrnODAmount] [money] NULL,"
                                + "[BranchNo] [int] NULL,"
                                + "[TrnCSGL] [tinyint] NULL,"
                                + "[TrnGLAccNoDr] [int] NULL,"
                                + "[TrnGLAccNoCr] [int] NULL,"
                                + "[TrnGLFlag] [tinyint] NULL,"
                                + "[GLAccNo] [int] NULL,"
                                + "[GLAmount] [money] NULL,"
                                + "[GLDebitAmt] [money] NULL,"
                                + "[GLCreditAmt] [money] NULL,"
                                + "[TrnFlag] [tinyint] NULL,"
                                + "[TrnStatus] [tinyint] NULL,"
                                + "[FromCashCode] [int] NULL,"
                                + "[TrnProcStat] [tinyint] NULL,"
                                + "[TrnSysUser] [tinyint] NULL,"
                                + "[TrnModule] [tinyint] NULL,"
                                + "[ValueDate] [smalldatetime] NULL,"
                                + "[GLSlmCode] [int] NULL,"
                                + "[TrnPayment] [tinyint] NULL,"
                                + "[UserIP] [nvarchar](50) NULL,"
                                + "[UserID] [int] NULL,"
                                + "[VerifyUserID] [int] NULL,"
                                + "[SalDate] [smalldatetime] NULL,"
                                + "[CreateDate] [datetime] NULL,"
                                + "[CuName] [nvarchar](50) NULL,"
                                + "[MemName] [nvarchar](50) NULL,"
                                + "[NewCU] [tinyint] NULL,"
                                + "[NewMem] [tinyint] NULL,"
                                + "[NewAcc] [tinyint] NULL,"
                                + "[NewMemType]  [smallint] NULL,"
                                + "[NewAccPeriod]  [smallint] NULL,"
                                + "[NewAccIntRate]  [smallmoney] NULL,"
                                + "[NewAccContractIntFlag] [tinyint] NULL,"
                                + "[NewOpenDate] [smalldatetime] NULL,"
                                + "[NewMaturityDate] [smalldatetime] NULL,"
                                + "[NewFixedAmt] [money] NULL,"
                                + "[NewFixedMthInt] [money] NULL,"
                                + "[NewBenefitDate] [smalldatetime] NULL,"
                                + "[ProvAdjFlag] [int] NULL,"
                                + "[AccTypeMode] [smallint] NULL,"
                                + "[ReTrnId] [int] NULL,"
                                + "[ReTrnDate] [datetime] NULL,"
                                + "[TrnEditFlag] [smallint] NULL,"
                                + "[TrnOrgAmt] [money] NULL,"
                                + "CONSTRAINT [PK_" + TName + "] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", conn))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}