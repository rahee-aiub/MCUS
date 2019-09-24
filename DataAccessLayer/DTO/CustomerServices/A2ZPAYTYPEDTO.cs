using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZPAYTYPEDTO
    {
        #region Propertise

        public int AccTypeClass { set; get; }
        public int PayTypeCode { set; get; }
        public String PayTypeDescription { set; get; }
        public Int16 PayMode { set; get; }
        public Int16 record { set; get; }

        #endregion


        public static int InsertInformation(A2ZPAYTYPEDTO dto)
        {
            dto.PayTypeDescription = (dto.PayTypeDescription != null) ? dto.PayTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZPAYTYPE(AtyClass,PayType,PayTypeDes,PayMode)values('" + dto.AccTypeClass + "','" + dto.PayTypeCode + "','" + dto.PayTypeDescription + "','" + dto.PayMode + "')";
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

        public static A2ZPAYTYPEDTO GetInformation(int ClassCode,int Typecode)
        {

            var prm = new object[2];

            prm[0] = ClassCode;
            prm[1] = Typecode;
            

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoPayType", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZPAYTYPE WHERE AtyClass = '" + ClassCode + "' and PayType = " + Typecode, "A2ZCSMCUS");


            var p = new A2ZPAYTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.PayTypeCode = Converter.GetSmallInteger(dt.Rows[0]["PayType"]);
                p.PayTypeDescription = Converter.GetString(dt.Rows[0]["PayTypeDes"]);
                p.AccTypeClass = Converter.GetSmallInteger(dt.Rows[0]["AtyClass"]);
                p.PayMode = Converter.GetSmallInteger(dt.Rows[0]["PayMode"]);
                p.record = Converter.GetSmallInteger(1);
                return p;
            }
            p.PayTypeCode = 0;
            p.record = 0;

            return p;

        }
        public static int UpdateInformation(A2ZPAYTYPEDTO dto)
        {
            dto.PayTypeDescription = (dto.PayTypeDescription != null) ? dto.PayTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZPAYTYPE set PayType='" + dto.PayTypeCode + "',PayTypeDes='" + dto.PayTypeDescription + "',PayMode='" + dto.PayMode + "',AtyClass='" + dto.AccTypeClass
                + "' where AtyClass ='" + dto.AccTypeClass + "'and PayType='" + dto.PayTypeCode + "'";
                     
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
