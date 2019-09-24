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
  public  class A2ZHOLIDAYDTO
    {
        #region Propertise

        public DateTime HolDate { set; get; }  
        public int HolType { set; get; }
        public String HolTypeDesc { set; get; }
        public String HolNote { set; get; }
        public String HolDayName { set; get; }
        
        #endregion


        public static int InsertInformation(A2ZHOLIDAYDTO dto)
        {
            dto.HolNote = (dto.HolNote != null) ? dto.HolNote.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZHOLIDAY(HolDate,HolType,HolTypeDesc,HolNote,HolDayName)values('" + dto.HolDate + "','" + dto.HolType + "','" + dto.HolTypeDesc + "','" + dto.HolNote + "','"+dto.HolDayName+"')";
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


        public static A2ZHOLIDAYDTO GetInformation(DateTime HolDate)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZHOLIDAY WHERE HolDate = '" + HolDate + "'", "A2ZHKMCUS");


            var p = new A2ZHOLIDAYDTO();
            if (dt.Rows.Count > 0)
            {
                p.HolDate = Converter.GetDateTime(dt.Rows[0]["HolDate"]);
                p.HolType = Converter.GetInteger(dt.Rows[0]["HolType"]);
                p.HolTypeDesc = Converter.GetString(dt.Rows[0]["HolTypeDesc"]);
                p.HolNote = Converter.GetString(dt.Rows[0]["HolNote"]);
                p.HolDayName = Converter.GetString(dt.Rows[0]["HolDayName"]);

                
                return p;
            }
            p.HolType = 0;

            return p;

        }

        public static int UpdateInformation(A2ZHOLIDAYDTO dto)
        {
            dto.HolNote = (dto.HolNote != null) ? dto.HolNote.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZHOLIDAY set HolType='" + dto.HolType + "',HolTypeDesc='" + dto.HolTypeDesc + "',HolNote='" + dto.HolNote + "', HolDayName='" + dto.HolDayName + "' where HolDate='" + dto.HolDate + "'";
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
