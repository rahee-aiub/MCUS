using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZTRANTYPEDTO
    {
        #region Propertise

        public int TrnTypeCode { set; get; }
        public String TrnTypeDescription { set; get; }
        public Int16 record { set; get; }

        #endregion


        public static int InsertInformation(A2ZTRANTYPEDTO dto)
        {
            dto.TrnTypeDescription = (dto.TrnTypeDescription != null) ? dto.TrnTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZTRNTYPE(TrnType,TrnTypeDes)values('" + dto.TrnTypeCode + "','" + dto.TrnTypeDescription + "')";
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

        public static A2ZTRANTYPEDTO GetInformation(int Typecode)
        {
            var prm = new object[1];

            prm[0] = Typecode;
            

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTranType", prm, "A2ZCSMCUS");
            
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNTYPE WHERE TrnType = " + Typecode, "A2ZCSMCUS");


            var p = new A2ZTRANTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.TrnTypeCode = Converter.GetSmallInteger(dt.Rows[0]["TrnType"]);
                p.TrnTypeDescription = Converter.GetString(dt.Rows[0]["TrnTypeDes"]);
                p.record = Converter.GetSmallInteger(1);
                return p;
            }
            p.TrnTypeCode = 0;
            p.record = 0;

            return p;

        }

        public static int UpdateInformation(A2ZTRANTYPEDTO dto)
        {
            dto.TrnTypeDescription = (dto.TrnTypeDescription != null) ? dto.TrnTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZTRNTYPE set TrnType='" + dto.TrnTypeCode + "', TrnTypeDes='" + dto.TrnTypeDescription + "' where TrnType='" + dto.TrnTypeCode + "'";
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
