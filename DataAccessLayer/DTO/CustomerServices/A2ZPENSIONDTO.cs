using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZPENSIONDTO
    {
        #region Propertise
        public int ID { set; get; }
        public int AccType { set; get; }
        public DateTime Date { set; get; }
        public Int16 SlabFlag { set; get; }
        public Decimal DepositeAmount { set; get; }
        public int Period { set; get; }
        public Decimal MaturedAmount { set; get; }
        public Decimal InterestRate { set; get; }
        public Decimal PenalAmount { set; get; }
        public Decimal BonusAmount { set; get; }
        public short NoRecord { set; get; }   // 1= Record Found, 0=No Record Found
        #endregion

        public static int InsertInformation(A2ZPENSIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery1 = @"INSERT into A2ZATYSLAB(AtyAccType,AtyFlag,AtyRecords,AtyPeriod,AtyMaturedAmt,AtyIntRate,AtyPenalAmt,AtyBonusAmt)
                                 values('" + dto.AccType + "','" + dto.SlabFlag + "','" + dto.DepositeAmount + "','" + dto.Period + "','" + 
                                           dto.MaturedAmount + "','" + dto.InterestRate + "','" + dto.PenalAmount + "','" + dto.BonusAmount + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
           

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static A2ZPENSIONDTO Get3Information(int accType, Int16 SlabFlag, Int16 period)
        {
            DataTable dt = new DataTable();

            string strQuery = "SELECT * FROM A2ZATYSLAB WHERE AtyAccType = '" + accType + "' AND AtyFlag='" + SlabFlag + "' AND AtyPeriod = '" + period + "'";
            
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");
            var p = new A2ZPENSIONDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccType = Converter.GetInteger(dt.Rows[0]["AtyAccType"]);
                p.DepositeAmount = Converter.GetDecimal(dt.Rows[0]["AtyRecords"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AtyPeriod"]);
                p.MaturedAmount = Converter.GetDecimal(dt.Rows[0]["AtyMaturedAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AtyIntRate"]);
                p.NoRecord = 1;
                return p;
            }
            p.AccType = 0;
            p.DepositeAmount = 0;
            p.Period = 0;
            p.MaturedAmount = 0;
            p.InterestRate = 0;
            p.NoRecord = 0;

            return p;

        }

        public static A2ZPENSIONDTO GetInformation(int accType, Int16 SlabFlag, double depAmount, Int16 period)
        {
            DataTable dt = new DataTable();

            string strQuery = "SELECT * FROM A2ZATYSLAB WHERE AtyAccType = '" + accType + "' and AtyFlag = '" + SlabFlag + "' AND AtyRecords='" + depAmount + "'";
            if (period != 0)
            {
                strQuery = strQuery + " AND AtyPeriod = " + period;
            }

            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");
            var p = new A2ZPENSIONDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccType = Converter.GetInteger(dt.Rows[0]["AtyAccType"]);
                p.DepositeAmount = Converter.GetDecimal(dt.Rows[0]["AtyRecords"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AtyPeriod"]);
                p.MaturedAmount = Converter.GetDecimal(dt.Rows[0]["AtyMaturedAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AtyIntRate"]);
                p.NoRecord = 1;
                return p;
            }
            p.AccType = 0;
            p.DepositeAmount = 0;
            p.Period = 0;
            p.MaturedAmount = 0;
            p.InterestRate = 0;
            p.NoRecord = 0;

            return p;

        }
        public static A2ZPENSIONDTO PGetInformation(int AccType, Int16 SlabFlag, double Record, Int16 period)
        {
            string strQuery = "SELECT * FROM A2ZATYSLAB WHERE AtyAccType='" + AccType + "' and AtyFlag = '" + SlabFlag + "' and AtyRecords = '" + Record + "' AND AtyPeriod='" + period + "'";
         DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");
         var p = new A2ZPENSIONDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccType = Converter.GetInteger(dt.Rows[0]["AtyAccType"]);
                //p.Date = Converter.GetDateTime(dt.Rows[0]["AtyDate"]);
                p.SlabFlag = Converter.GetSmallInteger(dt.Rows[0]["AtyFlag"]);
                p.DepositeAmount = Converter.GetDecimal(dt.Rows[0]["AtyRecords"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AtyPeriod"]);
                p.MaturedAmount = Converter.GetDecimal(dt.Rows[0]["AtyMaturedAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AtyIntRate"]);
                p.BonusAmount = Converter.GetDecimal(dt.Rows[0]["AtyBonusAmt"]);
                p.PenalAmount = Converter.GetDecimal(dt.Rows[0]["AtyPenalAmt"]);
                p.NoRecord = 1;
                return p;
            }
            p.AccType = 0;
            p.SlabFlag = 0;
            p.DepositeAmount = 0;
            p.Period = 0;
            p.MaturedAmount = 0;
            p.InterestRate = 0;
            p.NoRecord = 0;
            return p;

        }

        public static int UpdateInformation(A2ZPENSIONDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZATYSLAB set AtyFlag='" + dto.SlabFlag + "',AtyRecords='" + dto.DepositeAmount + "',AtyPeriod='" + dto.Period + "',AtyMaturedAmt='" + dto.MaturedAmount + "',AtyIntRate='" + dto.InterestRate + "',AtyPenalAmt='" + dto.PenalAmount + "',AtyBonusAmt='" + dto.BonusAmount + "' where AtyAccType='" + dto.AccType + "' and  AtyRecords='" + dto.DepositeAmount + "' and AtyPeriod='" + dto.Period + "'";
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
