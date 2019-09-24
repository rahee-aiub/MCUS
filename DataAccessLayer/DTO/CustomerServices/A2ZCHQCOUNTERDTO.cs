using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO
{
  public  class A2ZCHQCOUNTERDTO
    {
        #region Propertise
        public String CuType { set; get; } 
        public int CreditUNo { set; get; }
        public int MemberNo { set; get; }
        public Int16 AccType { set; get; }
        public int AccNo { set; get; }
        public String ChqPrefix { set; get; }
        public int ChqSerialNo { set; get; }
        public Int16 ChqPage { set; get; }
        public String ChqPStatus { set; get; }
        public String ChqPDate { set; get; }
        public Int16 ChqBStatus { set; get; }
        public DateTime ChqBStatusDate { set; get; }
        public DateTime ChqBIssueDate { set; get; }
        #endregion

        public static int InsertInformation(A2ZCHQCOUNTERDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCHQCOUNTER(CuType,CuNo,MemNo,AccType,AccNo,ChqeFx,ChqSlNo,ChqbPage,ChqPStat,ChqPDt,ChqBStat,ChqBStatDt,ChqBIssDt)values('" + dto.CuType + "','" + dto.CreditUNo + "','" + dto.MemberNo + "','" + dto.AccType + "','" + dto.AccNo + "','" + dto.ChqPrefix + "','" + dto.ChqSerialNo + "','" + dto.ChqPage + "','" + dto.ChqPStatus + "','" + dto.ChqPDate + "','" + dto.ChqBStatus + "','" + dto.ChqBStatusDate + "','" + dto.ChqBIssueDate + "')";
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
        public static int UpdateInformation(A2ZCHQCOUNTERDTO dto)
        {
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCHQCOUNTER set ChqPStat='" + dto.ChqPStatus + "',ChqBStat='"+dto.ChqBStatus+"' where MemNo='" + dto.MemberNo + "' and AccType='" + dto.AccType + "' and AccNo='" + dto.AccNo + "'";
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
