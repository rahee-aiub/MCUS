using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;

namespace ATOZWEBMCUS.Constants
{
    internal class CommonConstants
    {
        public const string KEY_LOCAL_DOWNLOAD_DIRECTORY = "localDownloadDirectory";
        public const string LOG_FILENAME = "A2Z.Web";
        public static string LocalDownloadDirectory
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings[KEY_LOCAL_DOWNLOAD_DIRECTORY];
                }
                catch (Exception ex)
                {
                    // do nothing...
                    return null;
                }
            }
        }
        public static string LogFileName
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings[LOG_FILENAME];
                }
                catch (Exception ex)
                {
                    // do nothing...
                    return null;
                }
            }
        }
    }
}
