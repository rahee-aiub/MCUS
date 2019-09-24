using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZTRNOPTDTO
    {

        #region Propertise

        public int AccType { set; get; }
        public int FunctionOption { set; get; }
        public int PayType { set; get; }
        public int TrnType { set; get; }
        public int TrnMode { set; get; }
        public int TrnAmtLogic { set; get; }
        public int TrnValidation { set; get; }
        public String AccTypeDes { set; get; }
        public int Record { set; get; }

        #endregion


        public static int InsertInformation(A2ZTRNOPTDTO dto)
        {

            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZTRNOPT(AccType,AccTypeDes,FuncOpt,PayType,TrnType,TrnMode,TrnLogic,TrnValidation)values('" + dto.AccType + "','" + dto.AccTypeDes + "','" +
            dto.FunctionOption + "','" + dto.PayType + "','" + dto.TrnType + "','" + dto.TrnMode + "','" + dto.TrnAmtLogic + "','" + dto.TrnValidation + "')";
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

        public static A2ZTRNOPTDTO GetInformation(int Typecode, int functOpt,int PayType)
        {
            var prm = new object[3];

            prm[0] = Typecode;
            prm[1] = functOpt;
            prm[2] = PayType;
            


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTrnOpt", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNOPT WHERE AccType='" + Typecode + "' and PayType='" + PayType + "' and FuncOpt='" + functOpt + "'", "A2ZCSMCUS");


            var p = new A2ZTRNOPTDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.FunctionOption = Converter.GetInteger(dt.Rows[0]["FuncOpt"]);
                p.PayType = Converter.GetInteger(dt.Rows[0]["PayType"]);
                p.TrnType = Converter.GetInteger(dt.Rows[0]["TrnType"]);
                p.TrnMode = Converter.GetInteger(dt.Rows[0]["TrnMode"]);
                p.TrnAmtLogic = Converter.GetInteger(dt.Rows[0]["TrnLogic"]);
                p.TrnValidation = Converter.GetInteger(dt.Rows[0]["TrnValidation"]);
                p.AccTypeDes = Converter.GetString(dt.Rows[0]["AccTypeDes"]);
                p.Record = Converter.GetInteger(1);
                return p;
            }
            p.AccType = 0;
            p.Record = 0;
            return p;

        }
        public static int UpdateInformation(A2ZTRNOPTDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZTRNOPT set AccType='" + dto.AccType + "', AccTypeDes='" + dto.AccTypeDes + "', FuncOpt='" + dto.FunctionOption + "',PayType='" +
                dto.PayType + "',TrnType='" + dto.TrnType + "',TrnMode='" + dto.TrnMode + "',TrnLogic='" + 
                dto.TrnAmtLogic + "',TrnValidation='" + dto.TrnValidation + "' where AccType='" + 
                dto.AccType + "' and funcOpt='" + dto.FunctionOption + "' and PayType='" + dto.PayType + "'";
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
