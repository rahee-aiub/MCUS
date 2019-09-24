using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSPeriodEnd : System.Web.UI.Page
    {
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

                    int checkAllUser = DataAccessLayer.DTO.A2ZSYSIDSDTO.CountForSingleUserPurpose(Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID)), "A2ZCSMCUS");

                    if (checkAllUser > 0)
                    {
                        string strQuery = "SELECT IdsNo, IdsName, EmpCode, IdsFlag, IdsLogInFlag FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0";
                        gvUserInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserInfo, "A2ZCSMCUS");

                        //btnProcess.BackColor = Color.Black;
                        btnProcess.Enabled = false;

                        //string msg = "Can not Process Period End - No. of " + checkAllUser + " User is Using System";
                        string msg = "Can not Process Period End - System Not In Single User";

                        String csname1 = "PopupScript";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;

                        if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        {
                            String cstext1 = "alert('" + msg + "');";
                            cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        }
                    }


                    var dt = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime processDate = dt.ProcessDate;

                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:D}", processDate));

                    lblEndOfMonth.Visible = false;
                    //lblYearEnd.Visible = false;

                    int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                    if (processDate.Day == lastDay)
                    {
                        lblEndOfMonth.Visible = true;

                        //if (dt.ProcessDate.Month == 6)
                        //{
                        //    lblYearEnd.Visible = true;
                        //}
                    }

                    A2ZCSPARAMETERDTO.UpdateSingleUserFlag(1);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO.UpdateSingleUserFlag(0);
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {

        }
        protected void BackUpMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Database Backup Not Done');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();

            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);

            if (CtrlBackUpStat.Text == "0")
            {
                BackUpMSG();
            }
            else
            {
                int periodFlag = 1;

                if (lblEndOfMonth.Visible)
                {
                    periodFlag = 2;
                }
                //if (lblYearEnd.Visible)
                //{
                //    periodFlag = 3;
                //}

                var prm = new object[2];
                prm[0] = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                prm[1] = periodFlag;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSPeriodEnd", prm, "A2ZCSMCUS"));

                if (result == 0)
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('End of Process Completed');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }

                    //btnProcess.BackColor = Color.Black;
                    btnProcess.Enabled = false;
                    UpdateBackUpStat();
                    //Response.Redirect("A2ZERPModule.aspx");
                }
            }

        }

        protected void UpdateBackUpStat()
        {

            Int16 BStat = 0;

            int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
            if (roweffect > 0)
            {
              
            }

        }
    }
}