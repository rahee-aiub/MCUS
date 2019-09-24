using System;

namespace DataAccessLayer.Utility
{
    public static class Converter
    {
        /// <summary>
        /// It converts a null value to empty string
        /// and returns a object value to string
        /// </summary>
        /// <param name="fieldValue">object</param>
        /// <returns>string</returns>
        public static string GetString(object fieldValue)
        {
            if (System.Convert.IsDBNull(fieldValue) || fieldValue == null)
            {
                return "";
            }
            else
            {
                return fieldValue.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static Char[] GetChar(object fieldValue)
        {
            if (System.Convert.IsDBNull(fieldValue) || fieldValue == null)
            {
                return "".ToCharArray();
            }
            else
            {
                return (char[])fieldValue;
            }
        }

        /// <summary>
        /// Get a boolean value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBoolean(object obj)
        {
            if (obj.Equals(true))
            {
                obj = "1";
            }
            else
            {
                obj = "0";
            }
            if (obj != null)
            {
                if (obj.ToString().Trim() == "1")
                {
                    return true;
                }
                return false;
            }

            return false;
        }

        /// <summary>
        ///  Get a DateTime value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            DateTime dt = new DateTime();
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString(), out dt);
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string day, string month, string year)
        {
            try
            {
                return Convert.ToDateTime(string.Format("{0} {1}, {2}", day, month, year));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int16 GetSmallInteger(object obj)
        {
            Int16 intValue = 0;
            if (obj != null)
            {
                Int16.TryParse(obj.ToString(), out intValue);
            }
            return intValue;
        }

        /// <summary>
        ///  Get a Integer value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInteger(object obj)
        {
            int intValue = 0;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out intValue);
            }
            return intValue;
        }

        /// <summary>
        ///  Get a long value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetLong(object obj)
        {
            long longValue = 0;
            if (obj != null)
            {
                long.TryParse(obj.ToString(), out longValue);
            }
            return longValue;
        }

        /// <summary>
        ///  Get a Integer value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            decimal decValue = 0;
            if (obj != null)
            {
                decimal.TryParse(obj.ToString(), out decValue);
            }
            return decValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Guid GetGuid(object obj)
        {
            Guid Roguid = Guid.Empty;
            if (obj != null)
            {
                Roguid = (Guid)obj;
            }
            return Roguid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetByte(object obj)
        {
            byte[] BB = null;
            if (obj != null)
            {
                BB = (byte[])obj;
            }
            return BB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte GetByteValue(object obj)
        {
            byte BB = new byte();
            if (obj != null)
            {
                //BB = (byte)obj;
                //SAAIF:20130211
                byte.TryParse(obj.ToString(), out BB);
            }
            return BB;
        }

        /// <summary>
        ///  Get a Double value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDouble(object obj)
        {
            double dblValue = 0;
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out dblValue);
            }
            return dblValue;
        }

        /// <summary>
        ///  Generate DO Number
        /// </summary>
        /// <returns></returns>
        public static string GetDONumber(string LastNumber)
        {
            string dONumber = "";
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            if (strMonth.Length < 2)
            {
                strMonth = "0" + strMonth;
            }

            if (LastNumber == "0")
            {
                dONumber = strMonth + strYear + "-1";
            }
            else
            {
                string[] arrDOParts = LastNumber.Split('-');
                dONumber = strYear + strMonth + "-" + (Convert.ToInt32(arrDOParts[1]) + 1).ToString();
            }

            return dONumber;
        }

        /// <summary>
        ///  Generate UserID
        /// </summary>
        /// <returns></returns>
        public static string GenerateUserID()
        {
            string prefix = string.Empty;

            string userID;
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            //string strMSecond = DateTime.Now.Millisecond.ToString();
            string strHours = DateTime.Now.Hour.ToString();
            if (strHours.Length < 2)
            {
                strHours = "0" + strHours;
            }
            if (strMonth.Length < 2)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = DateTime.Now.Day.ToString();
            if (strDay.Length < 2)
            {
                strDay = "0" + strDay;
            }
            Random random = new Random();
            userID = prefix + random.Next(1000, 9999).ToString() + strYear.Substring(2) + strMonth + strDay + strHours;
            return userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateSPSessionID()
        {
            string prefix = string.Empty;
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strHours = DateTime.Now.Hour.ToString();
            if (strHours.Length < 2)
            {
                strHours = "0" + strHours;
            }
            if (strMonth.Length < 2)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = DateTime.Now.Day.ToString();
            if (strDay.Length < 2)
            {
                strDay = "0" + strDay;
            }
            Random random = new Random();
            return (prefix + strYear.Substring(2) + strMonth + strDay + strHours + random.Next(0x3e8, 0x270f).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateVoucherNumber()
        {
            string prefix = string.Empty;
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            if (strMonth.Length < 2)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = DateTime.Now.Day.ToString();
            if (strDay.Length < 2)
            {
                strDay = "0" + strDay;
            }
            Random random = new Random();
            return (prefix + strYear.Substring(2) + strMonth + strDay + random.Next(0x3e8, 0x270f).ToString());
        }     

        /// <summary>
        /// strDate must be (dd/mm/yyyy) or (dd-mm-yyyy) Format 15-12-2013
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string GetDateToYYYYMMDD(string strDate)
        {
            string strDay = strDate.Substring(0, 2);
            string strMonth = strDate.Substring(3, 2);
            string strYear = strDate.Substring(6, 4);

            return strYear + "-" + strMonth + "-" + strDay;
        }

        public static int CheckFromDateToDateByDate(DateTime fDate, DateTime tDate)
        {
            //  If fDate = tDate then Return 0
            //  If fDate > tDate then Return 1
            //  If fDate < tDate then Return -1

            return fDate.CompareTo(tDate);
        }

    }
}
