using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.HumanResource
{
   public class A2ZAREADTO
    {
        #region Propertise

       public int AreaCode { set; get; }
       public String AreaDescription { set; get; }
        #endregion


       public static int InsertInformation(A2ZAREADTO dto)
        {
            dto.AreaDescription = (dto.AreaDescription != null) ? dto.AreaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZAREA(AreaCode,AreaDescription)values('" + dto.AreaCode + "','" + dto.AreaDescription + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

       public static A2ZAREADTO GetInformation(int Areacode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZAREA WHERE AreaCode = " + Areacode, "A2ZHRMCUS");


            var p = new A2ZAREADTO();
            if (dt.Rows.Count > 0)
            {

                p.AreaCode = Converter.GetInteger(dt.Rows[0]["AreaCode"]);
                p.AreaDescription = Converter.GetString(dt.Rows[0]["AreaDescription"]);
                return p;
            }
            p.AreaCode = 0;

            return p;

        }
       public static int UpdateInformation(A2ZAREADTO dto)
        {
            dto.AreaDescription = (dto.AreaDescription != null) ? dto.AreaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZAREA set AreaCode='" + dto.AreaCode + "',AreaDescription='" + dto.AreaDescription + "' where AreaCode='" + dto.AreaCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
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
