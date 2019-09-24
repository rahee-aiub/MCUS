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
    public partial class CreateDataBase : System.Web.UI.Page
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
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Database Backup Not Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Database Backup Not Done');", true);
            return;

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

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Day End Process Sucessfully Done');", true);
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



            //CreateA2ZCSCUBS();
            //CreateA2ZGLCUBS();
            //CreateA2ZHKCUBS();
            //CreateA2ZHRCUBS();
            //CreateA2ZCUBST2016();
            CreateA2ZIMAGECUBS();

        }



        public void CreateA2ZCSCUBS()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZCSCUBS";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }


        public void CreateA2ZGLCUBS()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZGLCUBS";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }

        public void CreateA2ZHKCUBS()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZHKCUBS";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }

        public void CreateA2ZHRCUBS()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZHRCUBS";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }


        public void CreateA2ZCUBST2016()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZCUBST2016";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }


        public void CreateA2ZIMAGECUBS()
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZIMAGECUBS";
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


                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();



                con.Dispose();
                con.Close();



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }

    }
}