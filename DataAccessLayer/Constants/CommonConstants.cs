using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;

namespace DataAccessLayer.Constants
{
    internal class CommonConstants
    {
        public const string KEY_LOCAL_DOWNLOAD_DIRECTORY = "localDownloadDirectory";

        public const string LOG_FILENAME = "A2ZERPDataAccessLayerLogger";

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
                    return null;
                    throw ex;
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
                    return null;
                    throw ex;
                }
            }
        }
    }
}
