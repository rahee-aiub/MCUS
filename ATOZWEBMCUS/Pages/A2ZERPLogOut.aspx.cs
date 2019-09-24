using System;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPLogOut : System.Web.UI.Page
    {
        /* © 2013 AtoZ Computer Services */
        //Oni: September 22, 2013
        /// <summary>
        /// Thsi page is Log Out page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Application.Lock();

            if (Application["HitCount"] != null)
            {
                Application["HitCount"] = (int)Application["HitCount"] - 1;
                Application.UnLock();
            }
            else
            {
                Application["HitCount"] = 1;
            }
            Application.UnLock();

            try
            {
                //Put default value from enum
                Enums.ModuleConstant masterPage = Enums.ModuleConstant.HouseKeeping;
                if (SessionStore.ContainsKey(Params.SYS_SELECT_MODULE))//if (Session["LoadSelectedModule"] != null)
                {
                    masterPage = (Enums.ModuleConstant)SessionStore.GetValue(Params.SYS_SELECT_MODULE);//Session["LoadSelectedModule"].ToString();
                }
                switch (masterPage)
                {
                    case Enums.ModuleConstant.CustomerService:
                        AddAuditInformation(1);
                        A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;

                    case Enums.ModuleConstant.GeneralLedger:
                        AddAuditInformation(2);
                        A2ZSYSIDSDTO.UpdateUserGLLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;
                    case Enums.ModuleConstant.HouseKeeping:
                        AddAuditInformation(3);
                        A2ZSYSIDSDTO.UpdateUserHKLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;
                    case Enums.ModuleConstant.Booth:
                        AddAuditInformation(3);
                        A2ZSYSIDSDTO.UpdateUserBTLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;
                    //case Enums.ModuleConstant.OffBooth:
                    //    AddAuditInformation(3);
                    //    A2ZSYSIDSDTO.UpdateUserOBTLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                    //    Session["LogOutFlag"] = "1";
                    //    ClearSession();//Session.Abandon();
                    //    Response.Redirect("A2ZERP.aspx", false);
                    //    break;
                    case Enums.ModuleConstant.HumanResource:
                        AddAuditInformation(4);
                        A2ZSYSIDSDTO.UpdateUserHRLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;
                    case Enums.ModuleConstant.Inventory:
                        AddAuditInformation(5);
                        A2ZSYSIDSDTO.UpdateUserSTLoginFlag(Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID)), 0);
                        Session["LogOutFlag"] = "1";
                        ClearSession();//Session.Abandon();
                        Response.Redirect("A2ZERP.aspx", false);
                        break;
                   
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Load Problem');</script>");
                ClearSession();
                //throw ex;
            }

        }

        /// <summary>
        /// Saaif: 20130924
        /// Use tis method to remove session value from store
        /// </summary>
        private void ClearSession()
        {
            SessionStore.RemoveFromCustomStore(Params.SYS_SELECT_MODULE);
        }


        protected void AddAuditInformation(Int16 moduleNo)
        {
            var dto = new A2ZAUDITDTO();

            dto.UserId = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            dto.EmpCode = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_EMP_CODE));
            dto.UserIP = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_IP));
            dto.UserServerIP = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_SERVER_IP));
            dto.UserServerName = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_SERVER_NAME));

            dto.AudRecordNo = 2;
            dto.ModuleNo = moduleNo;
            dto.AudRemarks = "Log Out";
            dto.AudProcessDate = DateTime.Now.Date;
            dto.AudOldDate = DateTime.Now.Date;
            dto.AudNewDate = DateTime.Now.Date;

            A2ZAUDITDTO.InsertAuditInformation(dto);
        }
    }
}
