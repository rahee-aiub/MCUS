using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZRECCTRLNODTO
    {
        #region Propertise

        public int GLCashCode { set; get; }
        public Int16 RecType { set; get; }
        public int RecLastNo { set; get; }
        public int Record { set; get; }

        #endregion

        public static int InsertInformation(A2ZRECCTRLNODTO dto)
        {

            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZRECCTRLNO(CtrlGLCashCode,CtrlRecType,CtrlRecLastNo)values('" + dto.GLCashCode + "','" + dto.RecType + "','" + dto.RecLastNo + "')";
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
        public static A2ZRECCTRLNODTO GetInformation(int GLCashCode, Int16 RecType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZRECCTRLNO WHERE CtrlGLCashCode='" + GLCashCode + "' and  CtrlRecType='" + RecType + "' ", "A2ZCSMCUS");


            var p = new A2ZRECCTRLNODTO();
            if (dt.Rows.Count > 0)
            {
                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["CtrlGLCashCode"]);
                p.RecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.Record = Converter.GetInteger(1);
                return p;
            }
            p.Record = 0;
            return p;

        }

        public static int UpdateInformation(A2ZRECCTRLNODTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZRECCTRLNO set CtrlGLCashCode='" + dto.GLCashCode + "', CtrlRecType='" + dto.RecType + "',CtrlRecLastNo='" + dto.RecLastNo + "' WHERE CtrlGLCashCode ='" + dto.GLCashCode + "'and CtrlRecType='" + dto.RecType + "'";
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

        //---------------------------  VOUCHER'S INFORMATIONS ------------------------

        public static A2ZRECCTRLNODTO GetLastVoucherNo(int CashCode,Int16 RecType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZRECCTRLNO WHERE CtrlGLCashCode ='" + CashCode + "' and  CtrlRecType='" + RecType + "'", "A2ZCSMCUS");


            var p = new A2ZRECCTRLNODTO();
            if (dt.Rows.Count > 0)
            {

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["CtrlGLCashCode"]);
                p.RecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                int rowEffect = 0;
                string strQuery = "UPDATE A2ZRECCTRLNO set CtrlGLCashCode='" + p.GLCashCode + "', CtrlRecType='" + p.RecType + "',CtrlRecLastNo='" + p.RecLastNo + "' WHERE CtrlGLCashCode ='" + p.GLCashCode + "'and CtrlRecType='" + p.RecType + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                return p;
            }
            p.GLCashCode = 0;
            return p;

        }

        public static A2ZRECCTRLNODTO ReadLastVoucherNo(int CashCode, Int16 RecType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZRECCTRLNO WHERE CtrlGLCashCode ='" + CashCode + "' and  CtrlRecType='" + RecType + "' ", "A2ZCSMCUS");


            var p = new A2ZRECCTRLNODTO();
            if (dt.Rows.Count > 0)
            {

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["CtrlGLCashCode"]);
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);


                return p;
            }
            p.GLCashCode = 0;
            return p;

        }


    }
}
