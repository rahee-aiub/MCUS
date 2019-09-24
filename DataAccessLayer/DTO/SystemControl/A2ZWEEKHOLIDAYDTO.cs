using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;
namespace DataAccessLayer.DTO.SystemControl
{
  public  class A2ZWEEKHOLIDAYDTO
    {
        #region Propertise

        public int Record { set; get; }
        public Int16 HolWeekDay1 { set; get; }
        public String HolWeekDayName1 { set; get; }
        public Int16 HolWeekDay2 { set; get; }
        public String HolWeekDayName2 { set; get; }
        
        #endregion


        public static int InsertInformation(A2ZWEEKHOLIDAYDTO dto)
        {
            dto.HolWeekDayName1 = (dto.HolWeekDayName1 != null) ? dto.HolWeekDayName1.Trim().Replace("'", "''") : "";
            dto.HolWeekDayName2 = (dto.HolWeekDayName2 != null) ? dto.HolWeekDayName2.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZWEEKHOLIDAY(HolWeekDay1,HolWeekDay2,HolWeekDayName1,HolWeekDayName2)values('" + dto.HolWeekDay1 + "','" + dto.HolWeekDay2 + "', '" + dto.HolWeekDayName1 + "','" + dto.HolWeekDayName2 + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static A2ZWEEKHOLIDAYDTO GetInformation()
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZWEEKHOLIDAY", "A2ZHKMCUS");


            var p = new A2ZWEEKHOLIDAYDTO();
            if (dt.Rows.Count > 0)
            {
                p.HolWeekDay1 = Converter.GetSmallInteger(dt.Rows[0]["HolWeekDay1"]);
                p.HolWeekDay2 = Converter.GetSmallInteger(dt.Rows[0]["HolWeekDay2"]);
                p.HolWeekDayName1 = Converter.GetString(dt.Rows[0]["HolWeekDayName1"]);
                p.HolWeekDayName2 = Converter.GetString(dt.Rows[0]["HolWeekDayName2"]);
                p.Record = 1;
                return p;
            }
            p.Record = 0;

            return p;

        }

        public static int UpdateInformation(A2ZWEEKHOLIDAYDTO dto)
        {
            dto.HolWeekDayName1 = (dto.HolWeekDayName1 != null) ? dto.HolWeekDayName1.Trim().Replace("'", "''") : "";
            dto.HolWeekDayName2 = (dto.HolWeekDayName2 != null) ? dto.HolWeekDayName2.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZWEEKHOLIDAY set HolWeekDay1='" + dto.HolWeekDay1 + "',HolWeekDay2='" + dto.HolWeekDay2 + "',HolWeekDayName1='" + dto.HolWeekDayName1 + "',HolWeekDayName2='" + dto.HolWeekDayName2 + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }







    }
}
