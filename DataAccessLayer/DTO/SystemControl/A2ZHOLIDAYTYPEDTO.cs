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
  public  class A2ZHOLIDAYTYPEDTO
    {
        #region Propertise

        public int HolType { set; get; }
        public String HolTypeDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZHOLIDAYTYPEDTO dto)
        {
            dto.HolTypeDescription = (dto.HolTypeDescription != null) ? dto.HolTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZHOLIDAYTYPE(HolType,HolTypeDescription)values('" + dto.HolType + "','" + dto.HolTypeDescription + "')";
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


        public static A2ZHOLIDAYTYPEDTO GetInformation(int HolType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZHOLIDAYTYPE WHERE HolType = " + HolType, "A2ZHKMCUS");


            var p = new A2ZHOLIDAYTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.HolType = Converter.GetInteger(dt.Rows[0]["HolType"]);
                p.HolTypeDescription = Converter.GetString(dt.Rows[0]["HolTypeDescription"]);
                return p;
            }
            p.HolType = 0;

            return p;

        }

        public static int UpdateInformation(A2ZHOLIDAYTYPEDTO dto)
        {
            dto.HolTypeDescription = (dto.HolTypeDescription != null) ? dto.HolTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZHOLIDAYTYPE set HolType='" + dto.HolType + "',HolTypeDescription='" + dto.HolTypeDescription + "' where HolType='" + dto.HolType + "'";
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
