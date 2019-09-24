using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZNATIONALITYDTO
    {
        #region Propertise

        public int NationalityCode { set; get; }
        public String NationalityDescription { set; get; }
        #endregion

        public static int InsertInformation(A2ZNATIONALITYDTO dto)
        {
            dto.NationalityDescription = (dto.NationalityDescription != null) ? dto.NationalityDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZNATIONALITY(NationalityCode,NationalityDescription)values('" + dto.NationalityCode + "','" + dto.NationalityDescription + "')";
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

        public static A2ZNATIONALITYDTO GetInformation(Int16 Nationalitycode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZNATIONALITY WHERE NationalityCode = " + Nationalitycode, "A2ZHKMCUS");


            var p = new A2ZNATIONALITYDTO();
            if (dt.Rows.Count > 0)
            {

                p.NationalityCode = Converter.GetSmallInteger(dt.Rows[0]["NationalityCode"]);
                p.NationalityDescription = Converter.GetString(dt.Rows[0]["NationalityDescription"]);
                return p;
            }
            p.NationalityCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZNATIONALITYDTO dto)
        {
            dto.NationalityDescription = (dto.NationalityDescription != null) ? dto.NationalityDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZNATIONALITY set NationalityCode='" + dto.NationalityCode + "',NationalityDescription='" + dto.NationalityDescription + "' where NationalityCode='" + dto.NationalityCode + "'";
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
