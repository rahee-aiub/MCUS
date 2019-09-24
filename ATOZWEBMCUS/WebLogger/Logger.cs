using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using ATOZWEBMCUS.Constants;
using System.Reflection;

namespace ATOZWEBMCUS.WebLogger
{
    internal class Logger
    {
        private Logger()
        {
            // Blocking instance creation...
        }

        private static object _lockHandle = new object();
        private static string _logFileName = CommonConstants.LOG_FILENAME;
        private static TextWriter _exceptionLogWriter;

        static Logger()
        {
            // Has the writer initialized?
            if (_exceptionLogWriter == null)
            {
                // No. So lets init it. Make sure that another thread do not init 
                // the same writer at teh same time. That would be very rare, but possible
                lock (_lockHandle)
                {
                    // Has the writer been initialized by another thread holding the lock and 
                    // we entered late? If so then abort since that thread already opened the file
                    // otherwise we are the first thread to do so 
                    if (_exceptionLogWriter == null)
                    {
                        int i = 0;
                        string fileToOpen = CommonConstants.LocalDownloadDirectory + "\\" + _logFileName + ".log";
                        //string fileToOpen = CommonConstants.LocalDownloadDirectory + "\\" + LOG_FILENAME + ".log";
                        // We migh have different appdoamins in the IIS worker process. For example
                        // we might have 5 different processes trying to server the thread and they
                        // will all have different app doamins if the worker processes are isolated.
                        // So make the first process will have lock on the file, all calls from the 
                        // subsequent processes will fail. 
                        // In order to solve this, if we cannot aquire write lock on the file then 
                        // we will add an integer to the filename and try to write to that file. If
                        // we fail 10 subsequent tries, we will abort.
                        StreamWriter sw = null;
                        while (sw == null && i < 10)
                        {
                            try
                            {
                                sw = new StreamWriter(fileToOpen, true);
                                sw.AutoFlush = true;
                            }
                            catch
                            {
                                fileToOpen = CommonConstants.LocalDownloadDirectory + "\\" + _logFileName + i.ToString() + ".log";
                                i++;
                            }

                        }
                        _exceptionLogWriter = TextWriter.Synchronized(sw);
                    }
                }
            }
        }



        /// <summary>
        /// Writes a messsage into the log
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>
        ///     Apparently we are using the file logging rather using any well designed 
        /// logging framework.
        /// </remarks>
        public static void Write(string message)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(CommonConstants.LocalDownloadDirectory +
                                                          "\\" + Assembly.GetCallingAssembly().GetName().Name + ".log", true))
                {
                    wr.WriteLine("[" + DateTime.Now.ToString() + "]\t" + message);
                }
            }
            catch
            {
                _exceptionLogWriter.WriteLine("[" + DateTime.Now.ToString() + "]\t" + message);

            }
        }

        public static void Write(Exception ex)
        {

            //Write("Method: " + ex.TargetSite.ToString() + "\nMessage: " + ex.Message +
            //    ".\nStackTrace:" + ex.StackTrace);
            try
            {
                using (StreamWriter wr = new StreamWriter(CommonConstants.LocalDownloadDirectory +
                                                          "\\" + Assembly.GetCallingAssembly().GetName().Name + ".log", true))
                {
                    wr.WriteLine("Method: " + ex.TargetSite.ToString() + "\nMessage: " + ex.Message +
                                 ".\nStackTrace:" + ex.StackTrace);
                }
            }
            catch
            {
                _exceptionLogWriter.WriteLine("Method: " + ex.TargetSite.ToString() + "\nMessage: " + ex.Message +
                                              ".\nStackTrace:" + ex.StackTrace);
            }
        }
    }
}
