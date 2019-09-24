using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZTRNVALIDDTO
    {
        #region Propertise

        public decimal BalAmount { set; get; }
        public decimal InterestRate { set; get; }
        public decimal LogicAmount { set; get; }
        public short NoRecord { set; get; }   // 1= Record Found, 0=No Record Found


        #endregion

        public static A2ZTRNVALIDDTO GetPensionDepositAmt(Int16 CuType, int CreditNo, int MemberNo, Int16 AccType, int AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZTRNVALIDDTO();
            if (dt.Rows.Count > 0)
            {

                p.LogicAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }
        public static A2ZTRNVALIDDTO GetShareMinAmt(Int16 AccType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSMCUS");


            var p = new A2ZTRNVALIDDTO();
            if (dt.Rows.Count > 0)
            {

                p.LogicAmount = Converter.GetDecimal(dt.Rows[0]["PrmMinDeposit"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }
        public static A2ZTRNVALIDDTO GetLoanReturnAmt(Int16 CuType, int CreditNo, int MemberNo, Int16 AccType, int AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZTRNVALIDDTO();
            if (dt.Rows.Count > 0)
            {

                p.LogicAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }

        public static A2ZTRNVALIDDTO GetIntReturnAmt(Int16 CuType, int CreditNo, int MemberNo, Int16 AccType, int AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZTRNVALIDDTO();
            if (dt.Rows.Count > 0)
            {
                p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);

                p.LogicAmount = (p.BalAmount * p.InterestRate) / 1200;
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }
        public static A2ZTRNVALIDDTO GetLoanDisbursementAmt(Int16 CuType, int CreditNo, int MemberNo, Int16 AccType, int AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZTRNVALIDDTO();
            if (dt.Rows.Count > 0)
            {
                p.LogicAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanAmt"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

    }
}

