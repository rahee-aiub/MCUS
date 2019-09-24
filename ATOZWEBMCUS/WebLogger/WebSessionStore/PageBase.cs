using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ATOZWEBMCUS
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            AutoRedirect();
        }

        //protected void RemoveUserId()
        //{
        //    string qry = "UPDATE A2ZSYSIDS SET IdsLogInFlag=0 Where IdsNo=1";
        //    int roweffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));
        //}
        public void AutoRedirect()
        {     
            int int_MilliSecondsTimeOut = (this.Session.Timeout * 60000);
            string str_Script = @"
        <script type='text/javascript'> 
        intervalset = window.setInterval('Redirect()'," + int_MilliSecondsTimeOut.ToString() + @");
        function Redirect()
        { 
         window.location.href='\A2ZStartUp.aspx'; 
        }
       </script>";
           ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);
           //RemoveUserId();
           UserIdInitial();
        }

        public void UserIdInitial()
        {
            int UserId = DataAccessLayer.Utility.Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID));

            A2ZSYSIDSDTO.UpdateUserCSLoginFlag(UserId, 0);
            A2ZSYSIDSDTO.UpdateUserGLLoginFlag(UserId, 0);
            A2ZSYSIDSDTO.UpdateUserHKLoginFlag(UserId, 0);
            A2ZSYSIDSDTO.UpdateUserHRLoginFlag(UserId, 0);
            A2ZSYSIDSDTO.UpdateUserBTLoginFlag(UserId, 0);
            //A2ZSYSIDSDTO.UpdateUserOBTLoginFlag(UserId, 0);
        }
    }
}