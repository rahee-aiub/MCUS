using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZTRNCODEDTO
    {
        #region Propertise

        public int TrnCode { set; get; }
        public String TrnDescription { set; get; }
        public int AccType { set; get; }
        public Int16 AccTypeMode { set; get; }
        public int PayType { set; get; }
        #endregion


        public static int InsertInformation(A2ZTRNCODEDTO dto)
        {
            dto.TrnDescription = (dto.TrnDescription != null) ? dto.TrnDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZTRNCODE(TrnCode,TrnDes,AccType,AccTypeMode,PayType)values('" + dto.TrnCode + "','" + dto.TrnDescription + "','" + dto.AccType + "','" + dto.AccTypeMode + "','"+dto.PayType+"')";
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

        public static A2ZTRNCODEDTO GetInformation(int TrnCode)
        {
            var prm = new object[1];

            prm[0] = TrnCode;
            


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTrnCode", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNCODE WHERE TrnCode = " + TrnCode, "A2ZCSMCUS");


            var p = new A2ZTRNCODEDTO();
            if (dt.Rows.Count > 0)
            {

                p.TrnCode = Converter.GetInteger(dt.Rows[0]["TrnCode"]);
                p.TrnDescription = Converter.GetString(dt.Rows[0]["TrnDes"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                p.PayType = Converter.GetSmallInteger(dt.Rows[0]["PayType"]);
                return p;
            }
            p.TrnCode = 0;

            return p;

        }


        public static A2ZTRNCODEDTO GetInformation01(int AType)
        {
            
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNCODE WHERE AccType = " + AType, "A2ZCSMCUS");


            var p = new A2ZTRNCODEDTO();
            if (dt.Rows.Count > 0)
            {

                p.TrnCode = Converter.GetInteger(dt.Rows[0]["TrnCode"]);
                p.TrnDescription = Converter.GetString(dt.Rows[0]["TrnDes"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                return p;
            }
            p.TrnCode = 0;

            return p;

        }
        public static A2ZTRNCODEDTO GetInformation99(int PayType)
        {
            var prm = new object[1];

            prm[0] = PayType;



            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTrnCode99", prm, "A2ZCSMCUS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNCODE WHERE TrnCode = " + TrnCode, "A2ZCSMCUS");


            var p = new A2ZTRNCODEDTO();
            if (dt.Rows.Count > 0)
            {

                p.TrnCode = Converter.GetInteger(dt.Rows[0]["TrnCode"]);
                p.TrnDescription = Converter.GetString(dt.Rows[0]["TrnDes"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                return p;
            }
            p.TrnCode = 0;

            return p;

        }

        public static int UpdateInformation(A2ZTRNCODEDTO dto)
        {
            dto.TrnDescription = (dto.TrnDescription != null) ? dto.TrnDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZTRNCODE set TrnCode='" + dto.TrnCode + "', TrnDes='" + dto.TrnDescription + "',AccType='" + dto.AccType + "',AccTypeMode='" + dto.AccTypeMode + "',PayType='" + dto.PayType + "' where TrnCode='" + dto.TrnCode + "'";
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
