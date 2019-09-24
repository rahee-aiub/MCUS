using System;

namespace ATOZWEBMCUS.MasterPages
{
    public partial class ReportMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (!(SessionStore.ContainsKey(Params.WORKING_DATE_KEY)
                    //         && SessionStore.ContainsKey(Params.USER_ID_KEY)
                    //         && SessionStore.ContainsKey(Params.COMPANY_ID_KEY)
                    //         && SessionStore.ContainsKey(Params.MENU_STRING_KEY)
                    //         && SessionStore.ContainsKey(Params.PERMISSION_KEY)
                    //         && SessionStore.ContainsKey(Params.STORE_ID_KEY)))
                    //{
                    //    Response.Redirect("UserLogin.aspx", false);
                    //}

                    //if (CheckIsValidUrl())
                    //{
                    //    Response.Redirect("StartUp.aspx", false);
                    //}
                }
                else
                {
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
            }
            catch (Exception ex)
            {
                //Logger.Write(ex);
            }
        }

        protected bool CheckIsValidUrl()
        {
            try
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;

                if (sRet != "ReportServer.aspx")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //Logger.Write(ex);
                return true;
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
