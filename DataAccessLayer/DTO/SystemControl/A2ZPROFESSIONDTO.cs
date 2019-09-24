using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
  public  class A2ZPROFESSIONDTO
    {
        #region Propertise

        public int ProfessionCode { set; get; }
        public String ProfessionDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZPROFESSIONDTO dto)
        {
            dto.ProfessionDescription = (dto.ProfessionDescription != null) ? dto.ProfessionDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZPROFESSION(ProfessionCode,ProfessionDescription)values('" + dto.ProfessionCode + "','" + dto.ProfessionDescription + "')";
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

        public static A2ZPROFESSIONDTO GetInformation(Int16 Professioncode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZPROFESSION WHERE ProfessionCode = " + Professioncode, "A2ZCSMCUS");


            var p = new A2ZPROFESSIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.ProfessionCode = Converter.GetSmallInteger(dt.Rows[0]["ProfessionCode"]);
                p.ProfessionDescription = Converter.GetString(dt.Rows[0]["ProfessionDescription"]);
                return p;
            }
            p.ProfessionCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZPROFESSIONDTO dto)
        {
            dto.ProfessionDescription = (dto.ProfessionDescription != null) ? dto.ProfessionDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZPROFESSION set ProfessionCode='" + dto.ProfessionCode + "',ProfessionDescription='" + dto.ProfessionDescription + "' where ProfessionCode='" + dto.ProfessionCode + "'";
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
