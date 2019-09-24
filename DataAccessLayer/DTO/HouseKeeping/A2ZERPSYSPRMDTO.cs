using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HouseKeeping
{
    public class A2ZERPSYSPRMDTO
    {
        #region Propertuis

        public Int16 ID { set; get; }
        public Int16 PrmUnitNo { set; get; }
        public String PrmUnitName { set; get; }
        public String PrmUnitAdd1 { set; get; }
        public String PrmUnitAdd2 { set; get; }
        public String PrmUnitAdd3 { set; get; }
        public String PrmUnitPhone { set; get; }
        public Int16 PrmUnitFlag { set; get; }
        public Int16 PrmUnitStatus { set; get; }
        public DateTime PrmInstallDate { set; get; }
        public String PrmSystemPath { set; get; }
        public String PrmDataPath { set; get; }
        public String PrmBackUpPath { set; get; }
        public Int16 PrmBackUpStat { set; get; }
        public int PrmTimeOut { set; get; }
        public String PrmOutDataPath { set; get; }
        public String PrmInDataPath { set; get; }
        public Int16 PrmBegFinYear { set; get; }
        public Int16 PrmEODStat { set; get; }
        public Int16 PrmSalPostStat { set; get; }
        public Int16 PrmYearEndStat { set; get; }
        public DateTime PrmYearEndDate { set; get; }

        #endregion

        public static A2ZERPSYSPRMDTO GetParameterValue()
        {
            A2ZERPSYSPRMDTO p = new A2ZERPSYSPRMDTO();

            DataTable dt = CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZERPSYSPRM", "A2ZHKMCUS");

            p.PrmUnitNo = Utility.Converter.GetSmallInteger(dt.Rows[0]["PrmUnitNo"]);
            p.PrmUnitName = Utility.Converter.GetString(dt.Rows[0]["PrmUnitName"]);
            p.PrmUnitAdd1 = Utility.Converter.GetString(dt.Rows[0]["PrmUnitAdd1"]);
            p.PrmUnitAdd2 = Utility.Converter.GetString(dt.Rows[0]["PrmUnitAdd2"]);
            p.PrmUnitAdd3 = Utility.Converter.GetString(dt.Rows[0]["PrmUnitAdd3"]);
            p.PrmUnitPhone = Utility.Converter.GetString(dt.Rows[0]["PrmUnitPhone"]);
            p.PrmUnitFlag = Utility.Converter.GetSmallInteger(dt.Rows[0]["PrmUnitFlag"]);
            p.PrmUnitStatus = Utility.Converter.GetSmallInteger(dt.Rows[0]["PrmUnitStatus"]);
            p.PrmInstallDate = Utility.Converter.GetDateTime(dt.Rows[0]["PrmInstallDate"]);
            p.PrmDataPath = Utility.Converter.GetString(dt.Rows[0]["PrmDataPath"]);
            p.PrmBackUpPath = Utility.Converter.GetString(dt.Rows[0]["PrmBackUpPath"]);
            p.PrmBackUpStat = Utility.Converter.GetSmallInteger(dt.Rows[0]["PrmBackUpStat"]);
            p.PrmTimeOut = Utility.Converter.GetInteger(dt.Rows[0]["PrmTimeOut"]);
            p.PrmOutDataPath = Utility.Converter.GetString(dt.Rows[0]["PrmOutDataPath"]);
            p.PrmInDataPath = Utility.Converter.GetString(dt.Rows[0]["PrmInDataPath"]);
            p.PrmBegFinYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["PrmBegFinYear"]);
            p.PrmEODStat = Converter.GetSmallInteger(dt.Rows[0]["PrmEODStat"]);
            p.PrmSalPostStat = Converter.GetSmallInteger(dt.Rows[0]["PrmSalPostStat"]);
            p.PrmYearEndStat = Converter.GetSmallInteger(dt.Rows[0]["PrmYearEndStat"]);
            p.PrmYearEndDate = Utility.Converter.GetDateTime(dt.Rows[0]["PrmYearEndDate"]);

            return p;
        }

        public static int UpdateBackUpStat(Int16 bFlag)
        {
            string sqlQuery = "UPDATE A2ZERPSYSPRM SET PrmBackUpStat = " + bFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
        }

        public static int UpdateEODStat(Int16 bFlag)
        {
            string sqlQuery = "UPDATE A2ZERPSYSPRM SET PrmEODStat = " + bFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
        }

        public static int UpdateYearEndStat(Int16 bFlag)
        {
            string sqlQuery = "UPDATE A2ZERPSYSPRM SET PrmYearEndStat = " + bFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
        }
        public static int UpdateSalPostStat(Int16 bFlag)
        {
            string sqlQuery = "UPDATE A2ZERPSYSPRM SET PrmSalPostStat = " + bFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
        }
        public static int UpdateInformation(A2ZERPSYSPRMDTO dto)
        {
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZERPSYSPRM SET PrmUnitAdd1='" + dto.PrmUnitAdd1 + "',PrmUnitAdd2='" + dto.PrmUnitAdd2 + "',PrmUnitAdd3='" + dto.PrmUnitAdd3 + "',PrmUnitPhone='" + dto.PrmUnitPhone + "',PrmSystemPath='" + dto.PrmSystemPath + "',PrmDataPath='" + dto.PrmDataPath + "',PrmBackUpPath='" + dto.PrmBackUpPath + "',PrmTimeOut='" + dto.PrmTimeOut + "',PrmInDataPath='" + dto.PrmInDataPath + "',PrmOutDataPath='" + dto.PrmOutDataPath + "',PrmBegFinYear='" + dto.PrmBegFinYear + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
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


