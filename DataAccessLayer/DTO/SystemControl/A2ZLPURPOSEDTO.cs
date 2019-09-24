using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZLPURPOSEDTO
    {
        #region Propertise

        public int LPurposeCode { set; get; }
        public String LPurposeDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZLPURPOSEDTO dto)
        {
            dto.LPurposeDescription = (dto.LPurposeDescription != null) ? dto.LPurposeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZLPURPOSE(LPurposeCode,LPurposeDescription)values('" + dto.LPurposeCode + "','" + dto.LPurposeDescription + "')";
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

        public static A2ZLPURPOSEDTO GetInformation(Int16 Purposecode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZLPURPOSE WHERE LPurposeCode = " + Purposecode, "A2ZCSMCUS");


            var p = new A2ZLPURPOSEDTO();
            if (dt.Rows.Count > 0)
            {

                p.LPurposeCode = Converter.GetSmallInteger(dt.Rows[0]["LPurposeCode"]);
                p.LPurposeDescription = Converter.GetString(dt.Rows[0]["LPurposeDescription"]);
                return p;
            }
            p.LPurposeCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZLPURPOSEDTO dto)
        {
            dto.LPurposeDescription = (dto.LPurposeDescription != null) ? dto.LPurposeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZLPURPOSE set LPurposeCode='" + dto.LPurposeCode + "',LPurposeDescription='" + dto.LPurposeDescription + "' where LPurposeCode='" + dto.LPurposeCode + "'";
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
