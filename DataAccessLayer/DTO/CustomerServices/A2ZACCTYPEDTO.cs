using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCTYPEDTO
    {

       #region Propertise

       public Int16 AccTypeCode { set; get; }
       public Int16 AccTypeGuaranty { set; get; }
       public String AccTypeDescription { set; get; }
       public Int16 AccTypeClass { set; get; }
       public Int16 AccFlag { set; get; }
       public Int16 AccTypeMode { set; get; }
       public Int16 AccCertNo { set; get; }
       public Int16 AccAccessFlag { set; get; }
       public Int16 AccessT1 { set; get; }
       public Int16 AccessT2 { set; get; }
       public Int16 AccessT3 { set; get; }
       public decimal AccDepRoundingBy { set; get; }
       public int AccCorrType { set; get; }

       #endregion


        public static int InsertInformation(A2ZACCTYPEDTO dto)
        {
            dto.AccTypeDescription = (dto.AccTypeDescription != null) ? dto.AccTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZACCTYPE(AccTypeCode,AccTypeDescription,AccTypeClass,AccFlag,AccTypeMode,AccCertNo,AcessType1,AcessType2,AcessType3,AccDepRoundingBy,AccTypeGuaranty,AccAccessFlag,AccCorrType)values('" + dto.AccTypeCode + "','" +
                dto.AccTypeDescription + "','" + dto.AccTypeClass + "','" + dto.AccFlag + "','" + dto.AccTypeMode + "','" + dto.AccCertNo + "','" + dto.AccessT1 + "','" + dto.AccessT2 + "','" + dto.AccessT3 + "','" + dto.AccDepRoundingBy + "','" + dto.AccTypeGuaranty + "','" + dto.AccAccessFlag + "','" + dto.AccCorrType + "')";
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

        public static A2ZACCTYPEDTO GetInformation(Int16 Typecode)
        {
            var prm = new object[1];

            prm[0] = Typecode;
            

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccType", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCTYPE WHERE AccTypeCode = " + Typecode, "A2ZCSMCUS");


            var p = new A2ZACCTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccTypeCode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeCode"]);
                p.AccTypeDescription = Converter.GetString(dt.Rows[0]["AccTypeDescription"]);
                p.AccFlag = Converter.GetSmallInteger(dt.Rows[0]["AccFlag"]);
                p.AccTypeClass = Converter.GetSmallInteger(dt.Rows[0]["AccTypeClass"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                p.AccCertNo = Converter.GetSmallInteger(dt.Rows[0]["AccCertNo"]);
                p.AccAccessFlag = Converter.GetSmallInteger(dt.Rows[0]["AccAccessFlag"]);
                p.AccessT1 = Converter.GetSmallInteger(dt.Rows[0]["AcessType1"]);
                p.AccessT2 = Converter.GetSmallInteger(dt.Rows[0]["AcessType2"]);
                p.AccessT3 = Converter.GetSmallInteger(dt.Rows[0]["AcessType3"]);
                p.AccDepRoundingBy = Converter.GetDecimal(dt.Rows[0]["AccDepRoundingBy"]);
                p.AccTypeGuaranty = Converter.GetSmallInteger(dt.Rows[0]["AccTypeGuaranty"]);
                p.AccCorrType = Converter.GetSmallInteger(dt.Rows[0]["AccCorrType"]);
                return p;
            }
            p.AccTypeCode = 0;

            return p;
        }
        public static int UpdateInformation(A2ZACCTYPEDTO dto)
        {
            dto.AccTypeDescription = (dto.AccTypeDescription != null) ? dto.AccTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCTYPE set AccTypeCode='" + dto.AccTypeCode + "',AccTypeDescription='" + dto.AccTypeDescription + "',AccTypeClass='" +
                dto.AccTypeClass + "',AccFlag='" + dto.AccFlag + "',AccTypeMode='" + dto.AccTypeMode + "',AccCertNo='" + dto.AccCertNo + "',AcessType1='" + 
                dto.AccessT1 + "',AcessType2='" +
                dto.AccessT2 + "',AcessType3='" + dto.AccessT3 + "',AccDepRoundingBy='" + dto.AccDepRoundingBy + "',AccTypeGuaranty='" + dto.AccTypeGuaranty + "',AccAccessFlag='" + dto.AccAccessFlag + "',AccCorrType='" + dto.AccCorrType + "' where AccTypeCode='" + dto.AccTypeCode + "'";
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
