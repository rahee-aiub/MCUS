using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.BLL;
using System.Data;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO
{
   public class A2ZGROUPTRNDTO
   {
       #region Propertise
       public int ID { set; get; }
       public int MEMNO { set; get; }
       public Int16 ACCTYPE { set; get; }
       public Int64 ACCNO { set; get; }
       public int GLACCNO { set; get; }
       public Int16 TrnPayType { set; get; }
       public int InterestCode { set; get; }
       public Int16 InterestPayType { set; get; }
       public int PenaltyCode { set; get; }
       public Int16 PenaltyPayType { set; get; }
       public int LRFCode { set; get; }
       public Int16 LRFPayType { set; get; }
       public int OtherCode { set; get; }
       public Int16 OtherPayType { set; get; }
       public String GRPNAME { set; get; }
       public Int16 AtyClass { set; get; }
       public int BRNO { set; get; }
       public int REGNO { set; get; }
       public int SUBREGNO { set; get; }
       public int COLCODE { set; get; }
       public Int16 ACCSTAT { set; get; }
       public Decimal ACCBALANCE { set; get; }
       public DateTime ACCOPENDT { set; get; }
       public DateTime ACCLASTDT { set; get; }
       public Decimal ACCINTRATE { set; get; }
       public int ACCINSTNO { set; get; }
       public Decimal ACCINSTAMT { set; get; }
       public Decimal ACC2NDINSTAMT { set; get; }
       public Decimal ACCLASTAMT { set; get; }
       public Decimal ACCMTHDEP { set; get; }
       public Decimal CalDeposit { set; get; }
       public Decimal CalInterest { set; get; }
       public Decimal CalPenalty { set; get; }
       public Decimal CalOther { set; get; }
       public Decimal InputDeposit { set; get; }
       public Decimal InputInterest { set; get; }
       public Decimal InputPenalty { set; get; }
       public Decimal InputOther { set; get; }
       public Int16 UserId { set; get; }
       public Int16 BoothCode { set; get; }
       public DateTime CreateDate { set; get; }

       #endregion

       public static A2ZGROUPTRNDTO GetInformation(int MemNo, Int16 AccType, Int64 AccNo)
       {
           DataTable dt = new DataTable();
           string strQuery = "SELECT * FROM A2ZGROUPTRN WHERE  MEMNO='" + MemNo + "' AND  ACCTYPE='" + AccType + "' AND ACCNO='" + AccNo + "'";
           dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZMCUS");
           var p = new A2ZGROUPTRNDTO();
           if (dt.Rows.Count > 0)
           {
               p.ID = Converter.GetInteger(dt.Rows[0]["ID"]);
               p.MEMNO = Converter.GetInteger(dt.Rows[0]["MEMNO"]);
               p.ACCTYPE = Converter.GetSmallInteger(dt.Rows[0]["ACCTYPE"]);
               p.ACCNO = Converter.GetLong(dt.Rows[0]["ACCNO"]);
               p.GLACCNO = Converter.GetInteger(dt.Rows[0]["GLACCNO"]);
               p.TrnPayType = Converter.GetSmallInteger(dt.Rows[0]["TrnPayType"]);
               p.InterestCode = Converter.GetInteger(dt.Rows[0]["InterestCode"]);
               p.InterestPayType = Converter.GetSmallInteger(dt.Rows[0]["InterestPayType"]);
               p.PenaltyCode = Converter.GetInteger(dt.Rows[0]["PenaltyCode"]);
               p.PenaltyPayType = Converter.GetSmallInteger(dt.Rows[0]["PenaltyPayType"]);
               p.LRFCode = Converter.GetInteger(dt.Rows[0]["LRFCode"]);
               p.LRFPayType = Converter.GetSmallInteger(dt.Rows[0]["LRFPayType"]);
               p.OtherCode = Converter.GetInteger(dt.Rows[0]["OtherCode"]);
               p.OtherPayType = Converter.GetSmallInteger(dt.Rows[0]["OtherPayType"]);
               p.GRPNAME = Converter.GetString(dt.Rows[0]["GRPNAME"]);
               p.AtyClass = Converter.GetSmallInteger(dt.Rows[0]["AtyClass"]);
               p.BRNO = Converter.GetInteger(dt.Rows[0]["BRNO"]);
               p.REGNO = Converter.GetInteger(dt.Rows[0]["REGNO"]);
               p.SUBREGNO = Converter.GetInteger(dt.Rows[0]["SUBREGNO"]);
               p.COLCODE = Converter.GetInteger(dt.Rows[0]["COLCODE"]);
               p.ACCSTAT = Converter.GetSmallInteger(dt.Rows[0]["ACCSTAT"]);
               p.ACCBALANCE = Converter.GetDecimal(dt.Rows[0]["ACCBALANCE"]);
               p.ACCOPENDT = Converter.GetDateTime(dt.Rows[0]["ACCOPENDT"]);
               p.ACCLASTDT = Converter.GetDateTime(dt.Rows[0]["ACCLASTDT"]);
               p.ACCINTRATE = Converter.GetDecimal(dt.Rows[0]["ACCINTRATE"]);
               p.ACCINSTNO = Converter.GetInteger(dt.Rows[0]["ACCINSTNO"]);
               p.ACCINSTAMT = Converter.GetDecimal(dt.Rows[0]["ACCINSTAMT"]);
               p.ACC2NDINSTAMT = Converter.GetDecimal(dt.Rows[0]["ACC2NDINSTAMT"]);
               p.ACCLASTAMT = Converter.GetDecimal(dt.Rows[0]["ACCLASTAMT"]);
               p.ACCMTHDEP = Converter.GetDecimal(dt.Rows[0]["ACCMTHDEP"]);
               p.CalDeposit = Converter.GetDecimal(dt.Rows[0]["CalDeposit"]);
               p.CalInterest = Converter.GetDecimal(dt.Rows[0]["CalInterest"]);
               p.CalPenalty = Converter.GetDecimal(dt.Rows[0]["CalPenalty"]);
               p.CalOther = Converter.GetDecimal(dt.Rows[0]["CalOther"]);
               p.InputDeposit = Converter.GetDecimal(dt.Rows[0]["InputDeposit"]);
               p.InputInterest = Converter.GetDecimal(dt.Rows[0]["InputInterest"]);
               p.InputPenalty = Converter.GetDecimal(dt.Rows[0]["InputPenalty"]);
               p.InputOther = Converter.GetDecimal(dt.Rows[0]["InputOther"]);
               p.UserId = Converter.GetSmallInteger(dt.Rows[0]["UserId"]);
               p.BoothCode = Converter.GetSmallInteger(dt.Rows[0]["BoothCode"]);
               p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);
               return p;
           }

           p.MEMNO = 0;
           p.ACCTYPE = 0;
           p.ACCNO = 0;

           return p;

       }


       public static A2ZGROUPTRNDTO GetInformationByID(int id)
       {
           DataTable dt = new DataTable();
           string strQuery = "SELECT * FROM A2ZGROUPTRN WHERE ID='" + id;
           dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZMCUS");
           var p = new A2ZGROUPTRNDTO();
           if (dt.Rows.Count > 0)
           {
               p.ID = Converter.GetInteger(dt.Rows[0]["ID"]);
               p.MEMNO = Converter.GetInteger(dt.Rows[0]["MEMNO"]);
               p.ACCTYPE = Converter.GetSmallInteger(dt.Rows[0]["ACCTYPE"]);
               p.ACCNO = Converter.GetLong(dt.Rows[0]["ACCNO"]);
               p.GLACCNO = Converter.GetInteger(dt.Rows[0]["GLACCNO"]);
               p.TrnPayType = Converter.GetSmallInteger(dt.Rows[0]["TrnPayType"]);
               p.InterestCode = Converter.GetInteger(dt.Rows[0]["InterestCode"]);
               p.InterestPayType = Converter.GetSmallInteger(dt.Rows[0]["InterestPayType"]);
               p.PenaltyCode = Converter.GetInteger(dt.Rows[0]["PenaltyCode"]);
               p.PenaltyPayType = Converter.GetSmallInteger(dt.Rows[0]["PenaltyPayType"]);
               p.LRFCode = Converter.GetInteger(dt.Rows[0]["LRFCode"]);
               p.LRFPayType = Converter.GetSmallInteger(dt.Rows[0]["LRFPayType"]);
               p.OtherCode = Converter.GetInteger(dt.Rows[0]["OtherCode"]);
               p.OtherPayType = Converter.GetSmallInteger(dt.Rows[0]["OtherPayType"]);
               p.GRPNAME = Converter.GetString(dt.Rows[0]["GRPNAME"]);
               p.AtyClass = Converter.GetSmallInteger(dt.Rows[0]["AtyClass"]);
               p.BRNO = Converter.GetInteger(dt.Rows[0]["BRNO"]);
               p.REGNO = Converter.GetInteger(dt.Rows[0]["REGNO"]);
               p.SUBREGNO = Converter.GetInteger(dt.Rows[0]["SUBREGNO"]);
               p.COLCODE = Converter.GetInteger(dt.Rows[0]["COLCODE"]);
               p.ACCSTAT = Converter.GetSmallInteger(dt.Rows[0]["ACCSTAT"]);
               p.ACCBALANCE = Converter.GetDecimal(dt.Rows[0]["ACCBALANCE"]);
               p.ACCOPENDT = Converter.GetDateTime(dt.Rows[0]["ACCOPENDT"]);
               p.ACCLASTDT = Converter.GetDateTime(dt.Rows[0]["ACCLASTDT"]);
               p.ACCINTRATE = Converter.GetDecimal(dt.Rows[0]["ACCINTRATE"]);
               p.ACCINSTNO = Converter.GetInteger(dt.Rows[0]["ACCINSTNO"]);
               p.ACCINSTAMT = Converter.GetDecimal(dt.Rows[0]["ACCINSTAMT"]);
               p.ACC2NDINSTAMT = Converter.GetDecimal(dt.Rows[0]["ACC2NDINSTAMT"]);
               p.ACCLASTAMT = Converter.GetDecimal(dt.Rows[0]["ACCLASTAMT"]);
               p.ACCMTHDEP = Converter.GetDecimal(dt.Rows[0]["ACCMTHDEP"]);
               p.CalDeposit = Converter.GetDecimal(dt.Rows[0]["CalDeposit"]);
               p.CalInterest = Converter.GetDecimal(dt.Rows[0]["CalInterest"]);
               p.CalPenalty = Converter.GetDecimal(dt.Rows[0]["CalPenalty"]);
               p.CalOther = Converter.GetDecimal(dt.Rows[0]["CalOther"]);
               p.InputDeposit = Converter.GetDecimal(dt.Rows[0]["InputDeposit"]);
               p.InputInterest = Converter.GetDecimal(dt.Rows[0]["InputInterest"]);
               p.InputPenalty = Converter.GetDecimal(dt.Rows[0]["InputPenalty"]);
               p.InputOther = Converter.GetDecimal(dt.Rows[0]["InputOther"]);
               p.UserId = Converter.GetSmallInteger(dt.Rows[0]["UserId"]);
               p.BoothCode = Converter.GetSmallInteger(dt.Rows[0]["BoothCode"]);
               p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);
               return p;
           }

           p.MEMNO = 0;
           p.ACCTYPE = 0;
           p.ACCNO = 0;

           return p;

       }

       public static int UpdateGroupTransaction(A2ZGROUPTRNDTO grpTrnDto)
       {
           int result = 0;
           string strQuery = "UPDATE A2ZGROUPTRN SET InputDeposit = " + grpTrnDto.InputDeposit + ",InputInterest = " + grpTrnDto.InputInterest +
              ",InputPenalty = " + grpTrnDto.InputPenalty + " WHERE ID = " + grpTrnDto.ID;
           result = Converter.GetInteger(CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZMCUS"));

           return result;
       }

       public static A2ZGROUPTRNDTO GetSummaryInformation(int memNo, Int16 userId)
       {
           DataTable dt = new DataTable();
           string strQuery = @"SELECT SUM(InputDeposit) AS 'totDeposit',SUM(InputInterest) AS 'totInterest',
                            SUM(InputPenalty) AS 'totPenalty' FROM A2ZGROUPTRN WHERE MEMNO = " + memNo + 
                            " AND UserId = " + userId;
           dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZMCUS");
           var p = new A2ZGROUPTRNDTO();
           if (dt.Rows.Count > 0)

           {
               p.InputDeposit = Converter.GetDecimal(dt.Rows[0]["totDeposit"]);
               p.InputInterest = Converter.GetDecimal(dt.Rows[0]["totInterest"]);
               p.InputPenalty = Converter.GetDecimal(dt.Rows[0]["totPenalty"]);
               return p;
           }

           p.MEMNO = 0;
           p.ACCTYPE = 0;
           p.ACCNO = 0;

           return p;
       }


   }
}
