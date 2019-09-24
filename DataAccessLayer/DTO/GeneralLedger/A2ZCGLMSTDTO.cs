using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.GeneralLedger
{
    public class A2ZCGLMSTDTO
    {
        #region Properties
        public Int16 Id { set; get; }
        public Int16 GLCoNo { set; get; }
        public Int16 GLAccType { set; get; }
        public int GLAccNo { set; get; }
        public Int16 GLRecType { set; get; }
        public Int16 GLPrtPos { set; get; }
        public String GLAccDesc { set; get; }
        public Int16 GLAccMode { set; get; }
        public Byte GLBgtType { set; get; }
        public Decimal GLOpBal { set; get; }
        public Decimal GLDrSumC { set; get; }
        public Decimal GLDrSumT { set; get; }
        public Decimal GLCrSumC { set; get; }
        public Decimal GLCtSumT { set; get; }
        public Decimal GLClBal { set; get; }
        public int GLHead { set; get; }
        public int GLMainHead { set; get; }
        public int GLSubHead { set; get; }
        public String GLHeadDesc { set; get; }
        public String GLMainHeadDesc { set; get; }
        public String GLSubHeadDesc { set; get; }
        public int LastVoucherNo { set; get; }
        public String TrnDate { set; get; }
        public Int16 GLBalanceType { set; get; }
        public int Status { set; get; }



        #endregion

        public static A2ZCGLMSTDTO GetInformation( int glAccNo)
        {
            var prm = new object[1];

            prm[0] = glAccNo;
           


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GLGetInfoGLMaster", prm, "A2ZGLMCUS");
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCGLMST WHERE GLAccNo = '" + glAccNo+"'", "A2ZGLMCUS");
               

            var p = new A2ZCGLMSTDTO();

            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetSmallInteger(dt.Rows[0]["Id"]);
                p.GLCoNo = Converter.GetSmallInteger(dt.Rows[0]["GLCoNo"]);
                p.GLAccType = Converter.GetSmallInteger(dt.Rows[0]["GLAccType"]);
                p.GLAccNo = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                p.GLRecType = Converter.GetSmallInteger(dt.Rows[0]["GLRecType"]);
                p.GLPrtPos = Converter.GetSmallInteger(dt.Rows[0]["GLPrtPos"]);
                p.GLAccDesc = Converter.GetString(dt.Rows[0]["GLAccDesc"]);
                p.GLBgtType = Converter.GetByteValue(dt.Rows[0]["GLBgtType"]);

                p.GLOpBal = Converter.GetDecimal(dt.Rows[0]["GLOpBal"]);
                p.GLDrSumC = Converter.GetDecimal(dt.Rows[0]["GLDrSumC"]);
                p.GLDrSumT = Converter.GetDecimal(dt.Rows[0]["GLDrSumT"]);
                p.GLCrSumC = Converter.GetDecimal(dt.Rows[0]["GLCrSumC"]);
                p.GLCtSumT = Converter.GetDecimal(dt.Rows[0]["GLCrSumT"]);
                p.GLClBal = Converter.GetDecimal(dt.Rows[0]["GLClBal"]);
                p.GLHead = Converter.GetInteger(dt.Rows[0]["GLHead"]);
                p.GLMainHead = Converter.GetInteger(dt.Rows[0]["GLMainHead"]);
                p.GLSubHead = Converter.GetInteger(dt.Rows[0]["GLSubHead"]);
                p.GLHeadDesc = Converter.GetString(dt.Rows[0]["GLHeadDesc"]);
                p.GLMainHeadDesc = Converter.GetString(dt.Rows[0]["GLMainHeadDesc"]);
                p.GLSubHeadDesc = Converter.GetString(dt.Rows[0]["GLSubHeadDesc"]);
                p.LastVoucherNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.GLBalanceType = Converter.GetSmallInteger(dt.Rows[0]["GLBalanceType"]);

                p.GLAccMode = Converter.GetSmallInteger(dt.Rows[0]["GLAccMode"]);
                p.Status = Converter.GetInteger(dt.Rows[0]["Status"]);

                return p;
            }

            p.GLAccNo = 0;

            return p;
        }

        public static A2ZCGLMSTDTO GetOldCodeInformation(int glAccNo)
        {
            var prm = new object[1];

            prm[0] = glAccNo;



            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GLGetInfoGLOldCode", prm, "A2ZGLMCUS");

            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCGLMST WHERE GLAccNo = '" + glAccNo+"'", "A2ZGLMCUS");


            var p = new A2ZCGLMSTDTO();

            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetSmallInteger(dt.Rows[0]["Id"]);
                p.GLCoNo = Converter.GetSmallInteger(dt.Rows[0]["GLCoNo"]);
                p.GLAccType = Converter.GetSmallInteger(dt.Rows[0]["GLAccType"]);
                p.GLAccNo = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                p.GLRecType = Converter.GetSmallInteger(dt.Rows[0]["GLRecType"]);
                p.GLPrtPos = Converter.GetSmallInteger(dt.Rows[0]["GLPrtPos"]);
                p.GLAccDesc = Converter.GetString(dt.Rows[0]["GLAccDesc"]);
                p.GLBgtType = Converter.GetByteValue(dt.Rows[0]["GLBgtType"]);

                p.GLOpBal = Converter.GetDecimal(dt.Rows[0]["GLOpBal"]);
                p.GLDrSumC = Converter.GetDecimal(dt.Rows[0]["GLDrSumC"]);
                p.GLDrSumT = Converter.GetDecimal(dt.Rows[0]["GLDrSumT"]);
                p.GLCrSumC = Converter.GetDecimal(dt.Rows[0]["GLCrSumC"]);
                p.GLCtSumT = Converter.GetDecimal(dt.Rows[0]["GLCrSumT"]);
                p.GLClBal = Converter.GetDecimal(dt.Rows[0]["GLClBal"]);
                p.GLHead = Converter.GetInteger(dt.Rows[0]["GLHead"]);
                p.GLMainHead = Converter.GetInteger(dt.Rows[0]["GLMainHead"]);
                p.GLSubHead = Converter.GetInteger(dt.Rows[0]["GLSubHead"]);
                p.GLHeadDesc = Converter.GetString(dt.Rows[0]["GLHeadDesc"]);
                p.GLMainHeadDesc = Converter.GetString(dt.Rows[0]["GLMainHeadDesc"]);
                p.GLSubHeadDesc = Converter.GetString(dt.Rows[0]["GLSubHeadDesc"]);
                p.LastVoucherNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.GLBalanceType = Converter.GetSmallInteger(dt.Rows[0]["GLBalanceType"]);

                p.GLAccMode = Converter.GetSmallInteger(dt.Rows[0]["GLAccMode"]);
                p.Status = Converter.GetInteger(dt.Rows[0]["Status"]);

                return p;
            }

            p.GLAccNo = 0;

            return p;
        }
       
        public static A2ZCGLMSTDTO GetInformationMainHead(int sub3, int subinputsubhead)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLSubHead)AS GLSubHead FROM A2ZCGLMST WHERE LEFT(GLSubHead,3)='" + subinputsubhead + "'", "A2ZGLMCUS");

            var p = new A2ZCGLMSTDTO();

            if (dt.Rows.Count > 0)
            {

                p.GLAccNo = Converter.GetInteger(dt.Rows[0]["GLSubHead"]);

                return p;
            }

            p.GLAccNo = 0;

            return p;
        }


        public static A2ZCGLMSTDTO GetInformationtEST(int glAccNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLAccNo) as GLAccNo FROM A2ZCGLMST WHERE LEFT(GLAccNo,5) = '" + glAccNo + "' ", "A2ZGLMCUS");


            var p = new A2ZCGLMSTDTO();

            if (dt.Rows.Count > 0)
            {
               
                p.GLAccNo = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
               



                return p;
            }

            p.GLAccNo = 0;

            return p;
        }

        public static int InsertInformation(A2ZCGLMSTDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCGLMST(GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,GLBgtType,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,
                                 GLCrSumT,GLClBal,GLHead,GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,GLBalanceType,GLAccMode,Status,GLTodaysOpBalance) values('" + dto.GLCoNo + "','" + dto.GLAccType + "','" + dto.GLAccNo + "','" +
                                  dto.GLRecType + "','" + dto.GLPrtPos + "','" + dto.GLAccDesc + "','" + dto.GLBgtType + "','" + dto.GLOpBal + "','" +
                                  dto.GLDrSumC + "','" + dto.GLDrSumT + "','" + dto.GLCrSumC + "','" + dto.GLCtSumT + "','" + dto.GLClBal + "','" +
                                  dto.GLHead + "','" + dto.GLMainHead + "','" + dto.GLSubHead + "','" + dto.GLHeadDesc + "','" + dto.GLMainHeadDesc + "','" + dto.GLSubHeadDesc + "','" + dto.GLBalanceType + "','" + dto.GLAccMode + "','" + dto.Status + "',0)";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int Update1(A2ZCGLMSTDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCGLMST set GLOpBal='" + dto.GLOpBal + "' where  Id='" + dto.Id + "' ";

            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }



        public static A2ZCGLMSTDTO GetInformationMainHead(int p)
        {
            throw new NotImplementedException();
        }
    }
}
