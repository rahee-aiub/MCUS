using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;
using ATOZWEBMCUS.WebLogger;
using System.Reflection;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.WebSessionStore
{
    /// <summary>
    ///     Utility that manages the ASP.net session into a web farm
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public class SessionStore
    {
        /// <summary>
        ///     Saves a value into the session
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public static void SaveToCustomStore(string key, object value)
        {
            try
            {
                //StackFrame caller = new StackFrame(1, true);
                //Logger.Write("Keeping data to session. key = " + key + ", value = " + value + "\r\nMethod: " + caller.GetMethod().Name + "\r\nFile: " + Assembly.GetCallingAssembly().GetName().Name);
                //Logger.WriteLog("Keeping data to session. key = " + key + " value " + value);
                // Apparently Keep it into the session
                // Later change this implementing the cache/viewstate
                HttpContext.Current.Session[key] = value;
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        ///     If the given key contains into the session 
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns><c>true</c> if the key contains into the session, <c>false</c> otherwise.</returns>
        public static bool ContainsKey(string key)
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();

            int CtrlTimeOut = Converter.GetInteger(dto.PrmTimeOut);

            HttpContext.Current.Session.Timeout = 3000;

            if (HttpContext.Current.Session[key] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Removes a key from the session
        /// </summary>
        /// <param name="key">The key to be removed</param>
        /// <exception cref="T:System.ApplicationException">if the key does not exists into the session</exception> 
        public static void RemoveFromCustomStore(string key)
        {
            try
            {
                if (!ContainsKey(key))
                    throw new ApplicationException("Key does not exists into the cache.");
                
                HttpContext.Current.Session.Remove(key);

            }
            catch (Exception ex)
            {


                throw ex;
            }

        }

        
        /// <summary>
        ///     Get the value for the given key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        /// <exception cref="T:System.ApplicationException">if the key does not exists into the session</exception> 
        public static object GetValue(string key)
        {
            if (!ContainsKey(key))
                throw new ApplicationException("Key does not exists into the cache.");
            //Logger.Write("Getting the value for key = " + key);
            return HttpContext.Current.Session[key];
        }

    }
}
