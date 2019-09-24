using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
  public  class A2ZNATUREDTO
    {
        #region Propertise

        public int NatureCode { set; get; }
        public String NatureDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZNATUREDTO dto)
        {
            dto.NatureDescription = (dto.NatureDescription != null) ? dto.NatureDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZNATURE(NatureCode,NatureDescription)values('" + dto.NatureCode + "','" + dto.NatureDescription + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZNATUREDTO GetInformation(Int16 Naturecode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZNATURE WHERE NatureCode = " + Naturecode, "A2ZCSMCUS");


            var p = new A2ZNATUREDTO();
            if (dt.Rows.Count > 0)
            {

                p.NatureCode = Converter.GetSmallInteger(dt.Rows[0]["NatureCode"]);
                p.NatureDescription = Converter.GetString(dt.Rows[0]["NatureDescription"]);
                return p;
            }
            p.NatureCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZNATUREDTO dto)
        {
            dto.NatureDescription = (dto.NatureDescription != null) ? dto.NatureDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZNATURE set NatureCode='" + dto.NatureCode + "',NatureDescription='" + dto.NatureDescription + "' where NatureCode='" + dto.NatureCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
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
