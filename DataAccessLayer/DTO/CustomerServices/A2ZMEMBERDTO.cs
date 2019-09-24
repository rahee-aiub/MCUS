using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataAccessLayer.Conn;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZMEMBERDTO
    {
        #region Propertise

        public int ID { set; get; }
        public Int16 CuType { set; get; }
        public int CreditUnionNo { set; get; }
        public int MemberNo { set; get; }
        public String MemberName { set; get; }
        public String FatherName { set; get; }
        public String MotherName { set; get; }
        public String SpouseName { set; get; }
        public Int16 Occupation { set; get; }
        public Int16 Nationality { set; get; }
        public Int16 Gender { set; get; }
        public Int16 Religion { set; get; }
        public Int16 Nature { set; get; }
        public Int16 MaritalStatus { set; get; }
        public DateTime OpenDate { set; get; }
        public DateTime DateOfBirth { set; get; }
        public String PlaceofBirth { set; get; }
        public String PreAddressLine1 { set; get; }
        public String PreAddressLine2 { set; get; }
        public String PreAddressLine3 { set; get; }
        public int PreDivision { set; get; }
        public int PreDistrict { set; get; }
        public int preUpzila { set; get; }
        public int preThana { set; get; }
        public String PreTelephone { set; get; }
        public String PreMobile { set; get; }
        public String PreEmail { set; get; }
        public String PerAddressLine1 { set; get; }
        public String PerAddressLine2 { set; get; }
        public String PerAddressLine3 { set; get; }
        public int PerDivision { set; get; }
        public int PerDistrict { set; get; }
        public int PerUpzila { set; get; }
        public int PerThana { set; get; }
        public String PerTelephone { set; get; }
        public String PerMobile { set; get; }
        public String PerEmail { set; get; }
        public String EmployerName { set; get; }
        public String EmployerAddress { set; get; }
        public String IntroducerNo1 { set; get; }
        public String IntroducerName1 { set; get; }
        public String IntroducerNo2 { set; get; }
        public String IntroducerName2 { set; get; }
        public String NationalIdNo { set; get; }
        public String PassportNo { set; get; }
        public String PassportIssuePlace { set; get; }
        public DateTime PassportIssueDate { set; get; }
        public DateTime PassportExpiryDate { set; get; }
        public String TIN { set; get; }
        public DateTime LastTaxPayDate { set; get; }
        public Int16 MemType { set; get; }
        public int OldMemNo { set; get; }
        public short NoRecord { set; get; }

        public String OpenNullDate { set; get; }
        public String DOBNullDate { set; get; }
        public String PPIssueNullDate { set; get; }
        public String PPExpNullDate { set; get; }
        public String LTaxPayNullDate { set; get; }
        // 1= Record Found, 0=No Record Found


        #endregion

        public static int InsertInformation(A2ZMEMBERDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@MemOpenDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MemDateOfBirth", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@MemPPIssDt", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@MemPPExpDt", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@MemLastTaxDt", DBNull.Value);

            if (dto.OpenNullDate == "")
            {

                param1 = new SqlParameter("@MemOpenDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@MemOpenDate", dto.OpenDate);
            }

            if (dto.DOBNullDate == "")
            {
                param2 = new SqlParameter("@MemDateOfBirth", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@MemDateOfBirth", dto.DateOfBirth);

            }


            if (dto.PPIssueNullDate == "")
            {
                param3 = new SqlParameter("@MemPPIssDt", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@MemPPIssDt", dto.PassportIssueDate);
            }

            if (dto.PPExpNullDate == "")
            {
                param4 = new SqlParameter("@MemPPExpDt", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@MemPPExpDt", dto.PassportExpiryDate);
            }
            if (dto.LTaxPayNullDate == "")
            {
                param5 = new SqlParameter("@MemLastTaxDt", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@MemLastTaxDt", dto.LastTaxPayDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSMemberDataInsert", new object[] { dto.CuType, dto.CreditUnionNo, dto.MemberNo, dto.MemberName, dto.FatherName, dto.MotherName, dto.SpouseName, dto.Occupation, dto.Nationality, dto.Gender, dto.Religion, dto.Nature, dto.MaritalStatus, param1, param2, dto.PlaceofBirth, dto.PreAddressLine1, dto.PreAddressLine2, dto.PreAddressLine3, dto.PreDivision, dto.PreDistrict, dto.preUpzila, dto.preThana, dto.PreTelephone, dto.PreMobile, dto.PreEmail, dto.PerAddressLine1, dto.PerAddressLine2, dto.PerAddressLine3, dto.PerDivision, dto.PerDistrict, dto.PerUpzila, dto.PerThana, dto.PerTelephone, dto.PerMobile, dto.PerEmail, dto.EmployerName, dto.EmployerAddress, dto.IntroducerNo1, dto.IntroducerName1, dto.IntroducerNo2, dto.IntroducerName2, dto.NationalIdNo, dto.PassportNo, param3, param4, dto.PassportIssuePlace, dto.TIN, param5, dto.MemType });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }




        public static int Insert(A2ZMEMBERDTO dto)
        {
            dto.MemberName = (dto.MemberName != null) ? dto.MemberName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemType)values('" +
            dto.CuType + "','" + dto.CreditUnionNo + "','" + dto.MemberNo + "','" + dto.MemberName + "','" + dto.OpenDate + "','" + dto.MemType + "')";
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


        public static int Update(A2ZMEMBERDTO dto)
        {
            dto.MemberName = (dto.MemberName != null) ? dto.MemberName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZMEMBER SET MemName='" + dto.MemberName + "',CuType ='" + dto.CuType + "',CuNo ='" + dto.CreditUnionNo + "' where  Id='" + dto.ID + "' ";
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

        public static int Update1(A2ZMEMBERDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZMEMBER SET CuType ='" + dto.CuType + "',CuNo ='" + dto.CreditUnionNo + "' where  Id='" + dto.ID + "' ";
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

        public static int Update2(A2ZMEMBERDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZMEMBER SET MemType ='" + dto.MemType + "' where  Id='" + dto.ID + "' ";
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

        public static A2ZMEMBERDTO GetInfoOldMember(int CUNo, int MemNo)
        {
                       
                        
            var prm = new object[2];

            prm[0] = CUNo;
            prm[1] = MemNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoOldMem", prm, "A2ZCSMCUS");

            //DataTable dt = new DataTable();
            //string strQuery = "SELECT * FROM A2ZMEMBER WHERE CuType = '" + CuType + "' and CuNo = '" + CUNo + "' and MemNo='" + MemNo + "'";
            //dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");

            var p = new A2ZMEMBERDTO();
            if (dt.Rows.Count > 0)
            {

                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CreditUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.MemberName = Converter.GetString(dt.Rows[0]["MemName"]);
                p.FatherName = Converter.GetString(dt.Rows[0]["MemFName"]);
                p.MotherName = Converter.GetString(dt.Rows[0]["MemMName"]);
                p.SpouseName = Converter.GetString(dt.Rows[0]["MemSpouseName"]);
                p.Occupation = Converter.GetSmallInteger(dt.Rows[0]["MemOccupation"]);
                p.Nationality = Converter.GetSmallInteger(dt.Rows[0]["MemNationality"]);
                p.Gender = Converter.GetSmallInteger(dt.Rows[0]["MemGender"]);
                p.Religion = Converter.GetSmallInteger(dt.Rows[0]["MemReligion"]);
                p.Nature = Converter.GetSmallInteger(dt.Rows[0]["MemNature"]);
                p.MaritalStatus = Converter.GetSmallInteger(dt.Rows[0]["MemMaritalStatus"]);
                p.OpenDate = Converter.GetDateTime(dt.Rows[0]["MemOpenDate"]);
                p.DateOfBirth = Converter.GetDateTime(dt.Rows[0]["MemDOB"]);
                p.PlaceofBirth = Converter.GetString(dt.Rows[0]["MemPOB"]);
                p.PreAddressLine1 = Converter.GetString(dt.Rows[0]["MemPreAdd1"]);
                p.PreAddressLine2 = Converter.GetString(dt.Rows[0]["MemPreAdd2"]);
                p.PreAddressLine3 = Converter.GetString(dt.Rows[0]["MemPreAdd3"]);
                p.PreDivision = Converter.GetInteger(dt.Rows[0]["MemPreDivi"]);
                p.PreDistrict = Converter.GetInteger(dt.Rows[0]["MemPreDist"]);
                p.preUpzila = Converter.GetInteger(dt.Rows[0]["MemPreUpzila"]);
                p.preThana = Converter.GetInteger(dt.Rows[0]["MemPreThana"]);
                p.PreTelephone = Converter.GetString(dt.Rows[0]["MemPreTelephone"]);
                p.PreMobile = Converter.GetString(dt.Rows[0]["MemPreMobile"]);
                p.PreEmail = Converter.GetString(dt.Rows[0]["MemPreEmail"]);
                p.PerAddressLine1 = Converter.GetString(dt.Rows[0]["MemPerAdd1"]);
                p.PerAddressLine2 = Converter.GetString(dt.Rows[0]["MemPerAdd2"]);
                p.PerAddressLine3 = Converter.GetString(dt.Rows[0]["MemPerAdd3"]);
                p.PerDivision = Converter.GetInteger(dt.Rows[0]["MemPerDivi"]);
                p.PerDistrict = Converter.GetInteger(dt.Rows[0]["MemPerDist"]);
                p.PerUpzila = Converter.GetInteger(dt.Rows[0]["MemPerUpzila"]);
                p.PerThana = Converter.GetInteger(dt.Rows[0]["MemPerThana"]);
                p.PerTelephone = Converter.GetString(dt.Rows[0]["MemPerTelephone"]);
                p.PerMobile = Converter.GetString(dt.Rows[0]["MemPerMobile"]);
                p.PerEmail = Converter.GetString(dt.Rows[0]["MemPerEmail"]);
                p.EmployerName = Converter.GetString(dt.Rows[0]["MemEmpNm"]);
                p.EmployerAddress = Converter.GetString(dt.Rows[0]["MemEmpAdd"]);
                p.IntroducerName1 = Converter.GetString(dt.Rows[0]["MemIntroMNo1"]);
                p.IntroducerNo1 = Converter.GetString(dt.Rows[0]["MemIntroNm1"]);
                p.IntroducerName2 = Converter.GetString(dt.Rows[0]["MemIntroMNo2"]);
                p.IntroducerNo2 = Converter.GetString(dt.Rows[0]["MemIntroNm2"]);
                p.NationalIdNo = Converter.GetString(dt.Rows[0]["MemNationalId"]);
                p.PassportNo = Converter.GetString(dt.Rows[0]["MemPPNo"]);
                p.PassportIssueDate = Converter.GetDateTime(dt.Rows[0]["MemPPIssDt"]);
                p.PassportExpiryDate = Converter.GetDateTime(dt.Rows[0]["MemPPExpDt"]);
                p.PassportIssuePlace = Converter.GetString(dt.Rows[0]["MemPPIssPlace"]);
                p.TIN = Converter.GetString(dt.Rows[0]["MemTin"]);
                p.LastTaxPayDate = Converter.GetDateTime(dt.Rows[0]["MemLastTaxDt"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
                p.OldMemNo = Converter.GetInteger(dt.Rows[0]["MemOldMemNo"]);
                p.NoRecord = 1;
                return p;
            }
            p.CreditUnionNo = 0;
            p.MemberNo = 0;
            p.NoRecord = 0;

            return p;
        }


        public static A2ZMEMBERDTO GetInformation(Int16 CuType, int CUNo, int MemNo)
        {
            var prm = new object[3];

            prm[0] = CuType;
            prm[1] = CUNo;
            prm[2] = MemNo;
           

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoMember", prm, "A2ZCSMCUS");
            
            //DataTable dt = new DataTable();
            //string strQuery = "SELECT * FROM A2ZMEMBER WHERE CuType = '" + CuType + "' and CuNo = '" + CUNo + "' and MemNo='" + MemNo + "'";
            //dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");
            
            var p = new A2ZMEMBERDTO();
            if (dt.Rows.Count > 0)
            {
                                
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CreditUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.MemberName = Converter.GetString(dt.Rows[0]["MemName"]);
                p.FatherName = Converter.GetString(dt.Rows[0]["MemFName"]);
                p.MotherName = Converter.GetString(dt.Rows[0]["MemMName"]);
                p.SpouseName = Converter.GetString(dt.Rows[0]["MemSpouseName"]);
                p.Occupation = Converter.GetSmallInteger(dt.Rows[0]["MemOccupation"]);
                p.Nationality = Converter.GetSmallInteger(dt.Rows[0]["MemNationality"]);
                p.Gender = Converter.GetSmallInteger(dt.Rows[0]["MemGender"]);
                p.Religion = Converter.GetSmallInteger(dt.Rows[0]["MemReligion"]);
                p.Nature = Converter.GetSmallInteger(dt.Rows[0]["MemNature"]);
                p.MaritalStatus = Converter.GetSmallInteger(dt.Rows[0]["MemMaritalStatus"]);
                p.OpenDate = Converter.GetDateTime(dt.Rows[0]["MemOpenDate"]);
                p.DateOfBirth = Converter.GetDateTime(dt.Rows[0]["MemDOB"]);
                p.PlaceofBirth = Converter.GetString(dt.Rows[0]["MemPOB"]);
                p.PreAddressLine1 = Converter.GetString(dt.Rows[0]["MemPreAdd1"]);
                p.PreAddressLine2 = Converter.GetString(dt.Rows[0]["MemPreAdd2"]);
                p.PreAddressLine3 = Converter.GetString(dt.Rows[0]["MemPreAdd3"]);
                p.PreDivision = Converter.GetInteger(dt.Rows[0]["MemPreDivi"]);
                p.PreDistrict = Converter.GetInteger(dt.Rows[0]["MemPreDist"]);
                p.preUpzila = Converter.GetInteger(dt.Rows[0]["MemPreUpzila"]);
                p.preThana = Converter.GetInteger(dt.Rows[0]["MemPreThana"]);
                p.PreTelephone = Converter.GetString(dt.Rows[0]["MemPreTelephone"]);
                p.PreMobile = Converter.GetString(dt.Rows[0]["MemPreMobile"]);
                p.PreEmail = Converter.GetString(dt.Rows[0]["MemPreEmail"]);
                p.PerAddressLine1 = Converter.GetString(dt.Rows[0]["MemPerAdd1"]);
                p.PerAddressLine2 = Converter.GetString(dt.Rows[0]["MemPerAdd2"]);
                p.PerAddressLine3 = Converter.GetString(dt.Rows[0]["MemPerAdd3"]);
                p.PerDivision = Converter.GetInteger(dt.Rows[0]["MemPerDivi"]);
                p.PerDistrict = Converter.GetInteger(dt.Rows[0]["MemPerDist"]);
                p.PerUpzila = Converter.GetInteger(dt.Rows[0]["MemPerUpzila"]);
                p.PerThana = Converter.GetInteger(dt.Rows[0]["MemPerThana"]);
                p.PerTelephone = Converter.GetString(dt.Rows[0]["MemPerTelephone"]);
                p.PerMobile = Converter.GetString(dt.Rows[0]["MemPerMobile"]);
                p.PerEmail = Converter.GetString(dt.Rows[0]["MemPerEmail"]);
                p.EmployerName = Converter.GetString(dt.Rows[0]["MemEmpNm"]);
                p.EmployerAddress = Converter.GetString(dt.Rows[0]["MemEmpAdd"]);
                p.IntroducerName1 = Converter.GetString(dt.Rows[0]["MemIntroMNo1"]);
                p.IntroducerNo1 = Converter.GetString(dt.Rows[0]["MemIntroNm1"]);
                p.IntroducerName2 = Converter.GetString(dt.Rows[0]["MemIntroMNo2"]);
                p.IntroducerNo2 = Converter.GetString(dt.Rows[0]["MemIntroNm2"]);
                p.NationalIdNo = Converter.GetString(dt.Rows[0]["MemNationalId"]);
                p.PassportNo = Converter.GetString(dt.Rows[0]["MemPPNo"]);
                p.PassportIssueDate = Converter.GetDateTime(dt.Rows[0]["MemPPIssDt"]);
                p.PassportExpiryDate = Converter.GetDateTime(dt.Rows[0]["MemPPExpDt"]);
                p.PassportIssuePlace = Converter.GetString(dt.Rows[0]["MemPPIssPlace"]);
                p.TIN = Converter.GetString(dt.Rows[0]["MemTin"]);
                p.LastTaxPayDate = Converter.GetDateTime(dt.Rows[0]["MemLastTaxDt"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
                p.OldMemNo = Converter.GetInteger(dt.Rows[0]["MemOldMemNo"]);
                p.NoRecord = 1;
                return p;
            }
            p.CreditUnionNo = 0;
            p.MemberNo = 0;
            p.NoRecord = 0;

            return p;

        }

        public static A2ZMEMBERDTO GetOldInformation(Int16 CuType, int CUNo, int MemNo)
        {
            var prm = new object[4];

            prm[0] = CuType;
            prm[1] = CUNo;
            prm[2] = MemNo;
            prm[3] = 1;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoMember", prm, "A2ZCSMCUS");

            //DataTable dt = new DataTable();
            //string strQuery = "SELECT * FROM A2ZMEMBER WHERE CuType = '" + CuType + "' and CuNo = '" + CUNo + "' and MemNo='" + MemNo + "'";
            //dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZCSMCUS");

            var p = new A2ZMEMBERDTO();
            if (dt.Rows.Count > 0)
            {

                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CreditUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.MemberName = Converter.GetString(dt.Rows[0]["MemName"]);
                p.FatherName = Converter.GetString(dt.Rows[0]["MemFName"]);
                p.MotherName = Converter.GetString(dt.Rows[0]["MemMName"]);
                p.SpouseName = Converter.GetString(dt.Rows[0]["MemSpouseName"]);
                p.Occupation = Converter.GetSmallInteger(dt.Rows[0]["MemOccupation"]);
                p.Nationality = Converter.GetSmallInteger(dt.Rows[0]["MemNationality"]);
                p.Gender = Converter.GetSmallInteger(dt.Rows[0]["MemGender"]);
                p.Religion = Converter.GetSmallInteger(dt.Rows[0]["MemReligion"]);
                p.Nature = Converter.GetSmallInteger(dt.Rows[0]["MemNature"]);
                p.MaritalStatus = Converter.GetSmallInteger(dt.Rows[0]["MemMaritalStatus"]);
                p.OpenDate = Converter.GetDateTime(dt.Rows[0]["MemOpenDate"]);
                p.DateOfBirth = Converter.GetDateTime(dt.Rows[0]["MemDOB"]);
                p.PlaceofBirth = Converter.GetString(dt.Rows[0]["MemPOB"]);
                p.PreAddressLine1 = Converter.GetString(dt.Rows[0]["MemPreAdd1"]);
                p.PreAddressLine2 = Converter.GetString(dt.Rows[0]["MemPreAdd2"]);
                p.PreAddressLine3 = Converter.GetString(dt.Rows[0]["MemPreAdd3"]);
                p.PreDivision = Converter.GetInteger(dt.Rows[0]["MemPreDivi"]);
                p.PreDistrict = Converter.GetInteger(dt.Rows[0]["MemPreDist"]);
                p.preUpzila = Converter.GetInteger(dt.Rows[0]["MemPreUpzila"]);
                p.preThana = Converter.GetInteger(dt.Rows[0]["MemPreThana"]);
                p.PreTelephone = Converter.GetString(dt.Rows[0]["MemPreTelephone"]);
                p.PreMobile = Converter.GetString(dt.Rows[0]["MemPreMobile"]);
                p.PreEmail = Converter.GetString(dt.Rows[0]["MemPreEmail"]);
                p.PerAddressLine1 = Converter.GetString(dt.Rows[0]["MemPerAdd1"]);
                p.PerAddressLine2 = Converter.GetString(dt.Rows[0]["MemPerAdd2"]);
                p.PerAddressLine3 = Converter.GetString(dt.Rows[0]["MemPerAdd3"]);
                p.PerDivision = Converter.GetInteger(dt.Rows[0]["MemPerDivi"]);
                p.PerDistrict = Converter.GetInteger(dt.Rows[0]["MemPerDist"]);
                p.PerUpzila = Converter.GetInteger(dt.Rows[0]["MemPerUpzila"]);
                p.PerThana = Converter.GetInteger(dt.Rows[0]["MemPerThana"]);
                p.PerTelephone = Converter.GetString(dt.Rows[0]["MemPerTelephone"]);
                p.PerMobile = Converter.GetString(dt.Rows[0]["MemPerMobile"]);
                p.PerEmail = Converter.GetString(dt.Rows[0]["MemPerEmail"]);
                p.EmployerName = Converter.GetString(dt.Rows[0]["MemEmpNm"]);
                p.EmployerAddress = Converter.GetString(dt.Rows[0]["MemEmpAdd"]);
                p.IntroducerName1 = Converter.GetString(dt.Rows[0]["MemIntroMNo1"]);
                p.IntroducerNo1 = Converter.GetString(dt.Rows[0]["MemIntroNm1"]);
                p.IntroducerName2 = Converter.GetString(dt.Rows[0]["MemIntroMNo2"]);
                p.IntroducerNo2 = Converter.GetString(dt.Rows[0]["MemIntroNm2"]);
                p.NationalIdNo = Converter.GetString(dt.Rows[0]["MemNationalId"]);
                p.PassportNo = Converter.GetString(dt.Rows[0]["MemPPNo"]);
                p.PassportIssueDate = Converter.GetDateTime(dt.Rows[0]["MemPPIssDt"]);
                p.PassportExpiryDate = Converter.GetDateTime(dt.Rows[0]["MemPPExpDt"]);
                p.PassportIssuePlace = Converter.GetString(dt.Rows[0]["MemPPIssPlace"]);
                p.TIN = Converter.GetString(dt.Rows[0]["MemTin"]);
                p.LastTaxPayDate = Converter.GetDateTime(dt.Rows[0]["MemLastTaxDt"]);
                p.MemType = Converter.GetSmallInteger(dt.Rows[0]["MemType"]);
                p.OldMemNo = Converter.GetInteger(dt.Rows[0]["MemOldMemNo"]);
                p.NoRecord = 1;
                return p;
            }
            p.CreditUnionNo = 0;
            p.MemberNo = 0;
            p.NoRecord = 0;

            return p;

        }

        public static int UpdateInformation(A2ZMEMBERDTO dto)
        {
           
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@MemOpenDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MemDateOfBirth", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@MemPPIssDt", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@MemPPExpDt", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@MemLastTaxDt", DBNull.Value);

            if (dto.OpenNullDate == "")
            {

                param1 = new SqlParameter("@MemOpenDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@MemOpenDate", dto.OpenDate);
            }

            if (dto.DOBNullDate == "")
            {
                param2 = new SqlParameter("@MemDateOfBirth", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@MemDateOfBirth", dto.DateOfBirth);

            }


            if (dto.PPIssueNullDate == "")
            {
                param3 = new SqlParameter("@MemPPIssDt", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@MemPPIssDt", dto.PassportIssueDate);
            }

            if (dto.PPExpNullDate == "")
            {
                param4 = new SqlParameter("@MemPPExpDt", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@MemPPExpDt", dto.PassportExpiryDate);
            }
            if (dto.LTaxPayNullDate == "")
            {
                param5 = new SqlParameter("@MemLastTaxDt", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@MemLastTaxDt", dto.LastTaxPayDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSMemberDataUpdate", new object[] { dto.CuType, dto.CreditUnionNo, dto.MemberNo, dto.MemberName, dto.FatherName, dto.MotherName, dto.SpouseName, dto.Occupation, dto.Nationality, dto.Gender, dto.Religion, dto.Nature, dto.MaritalStatus, param1, param2, dto.PlaceofBirth, dto.PreAddressLine1, dto.PreAddressLine2, dto.PreAddressLine3, dto.PreDivision, dto.PreDistrict, dto.preUpzila, dto.preThana, dto.PreTelephone, dto.PreMobile, dto.PreEmail, dto.PerAddressLine1, dto.PerAddressLine2, dto.PerAddressLine3, dto.PerDivision, dto.PerDistrict, dto.PerUpzila, dto.PerThana, dto.PerTelephone, dto.PerMobile, dto.PerEmail, dto.EmployerName, dto.EmployerAddress, dto.IntroducerNo1, dto.IntroducerName1, dto.IntroducerNo2, dto.IntroducerName2, dto.NationalIdNo, dto.PassportNo, param3, param4, dto.PassportIssuePlace, dto.TIN, param5, dto.MemType });

            if (result == 0)
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
