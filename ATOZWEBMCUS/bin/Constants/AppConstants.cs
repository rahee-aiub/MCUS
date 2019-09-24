using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ATOZWEBMCUS.Constants
{
    internal class AppConstants
    {
        /// <summary>
        /// Don't Change this key.If you change this key please change to everywhere where use this
        /// encrypt decrypt key including encryptdecryptTools.
        /// Or please contact me
        /// saifulislamsaaif@gmail.com
        /// 01678127175
        /// </summary>
        public const string EncryptDecryptKey = "SAAIF";

        public enum EncryptDecrypt
        {
            Decrypt = 0,
            Encrypt = -1
        }
    }
}
