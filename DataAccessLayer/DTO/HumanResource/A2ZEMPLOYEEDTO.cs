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
namespace DataAccessLayer.DTO.HumanResource
{
     
    public class A2ZEMPLOYEEDTO
    {
        #region Properties

        public int Id { set; get; }
        public int EmployeeID { set; get; }
        public String EmployeeName { set; get; }
        public Int16 EmpBaseGrade { get; set; }
        public Int16 EmpGrade { get; set; }
        public String EmpGradeDesc { set; get; }
        public Int16 EmpDesignation { set; get; }
        public String EmpDesigDesc { set; get; }
        public Int16 ServiceType { set; get; }
        public String EmpSTypeDesc { set; get; }
        public int EmpArea { set; get; }
        public String EmpAreaDesc { set; get; }
        public int EmpLocation { set; get; }
        public String EmpLocationDesc { set; get; }
        public Int16 EmpDepartment { set; get; }
        public String EmpDepartmentDesc { set; get; }
        public Int16 EmpSection { set; get; }
        public String EmpSectionDesc { set; get; }
        public Int16 EmpProject { set; get; }
        public String EmpProjectDesc { set; get; }
        public int EmpCashCode { set; get; }
        public DateTime EmpJoinDate { set; get; }
        public DateTime EmpPerDate { set; get; }
        public DateTime EmpLstPSIDate { set; get; }
        public DateTime EmpNxtPSIDate { set; get; }
        public DateTime EmpLastPostingDate { set; get; }
        public DateTime EmpLastPromotionDate { set; get; }
        public DateTime EmpLastIncrementDate { set; get; }
        public Int16 EmpBank { set; get; }
        public String EmpAccNo { set; get; }
        public String EmpFatherName { set; get; }
        public String EmpMotherName { set; get; }
        public DateTime EmpDOB { set; get; }
        public String EmpSpouseName { set; get; }
        public Int16 EmpNationality { set; get; }
        public String EmpNationalityDesc { set; get; }
        public Int16 EmpRelagion { set; get; }
        public String EmpRelagionDesc { set; get; }
        public Int16 EmpGender { set; get; }
        public String EmpGenderDesc { set; get; }
        public Int16 EmpMaritalStat { set; get; }
        public String EmpMaritalStatDesc { set; get; }
        public String EmpBloodGrp { set; get; }

        public String EmpHeight { set; get; }
        public String EmpNationalID { set; get; }
        public String TIN { set; get; }
        public String EmpPPNo { set; get; }
        public DateTime EmpIssueDate { set; get; }
        public DateTime EmpPExpireDate { set; get; }
        public String EmpPlaceofIssue { set; get; }
        public String EmpLicenseNo { set; get; }
        public DateTime EmpLExpiryDate { set; get; }

        public String EmpJoinNullDate { set; get; }
        public String EmpPerNullDate { set; get; }
        public String EmpLastPostingNullDate { set; get; }
        public String EmpLastPromotionNullDate { set; get; }
        public String EmpLastIncrementNullDate { set; get; }
        public String EmpDOBNullDate { set; get; }
        public String EmpIssueNullDate { set; get; }
        public String EmpPExpireNullDate { set; get; }
        public String EmpLExpiryNullDate { set; get; }
        public Int16 EmpPayLabel { set; get; }
        public Decimal EmpConsolidatedAmt { get; set; }
        public String EmpConsolidatedDesc { get; set; }
        public Int16 Status { set; get; }
        public DateTime StatusDate { set; get; }
        public String StatusNullDate { set; get; }
        public String StatusDesc { set; get; }

        #endregion


        public static int InsertInformation(A2ZEMPLOYEEDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@EmpJoinDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@EmpPerDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@EmpLstPostingDate ", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@EmpLastPromotionDate ", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@EmpLastIncrementDate ", DBNull.Value);

            SqlParameter param6 = new SqlParameter("@EmpDOB ", DBNull.Value);
            SqlParameter param7 = new SqlParameter("@EmpIssueDate", DBNull.Value);
            SqlParameter param8 = new SqlParameter("@EmpPExpireDate", DBNull.Value);
            SqlParameter param9 = new SqlParameter("@EmpLExpiryDate", DBNull.Value);
            SqlParameter param10 = new SqlParameter("@StatusDate", DBNull.Value);
            

            if (dto.EmpJoinNullDate == "")
            {

                param1 = new SqlParameter("@EmpJoinDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@EmpJoinDate", dto.EmpJoinDate);
            }

            if (dto.EmpPerNullDate == "")
            {
                param2 = new SqlParameter("@EmpPerDate", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@EmpPerDate", dto.EmpPerDate);

            }


            if (dto.EmpLastPostingNullDate == "")
            {
                param3 = new SqlParameter("@EmpLstPostingDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@EmpLstPostingDate", dto.EmpLastPostingDate);
            }

            if (dto.EmpLastPromotionNullDate == "")
            {
                param4 = new SqlParameter("@EmpLastPromotionDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@EmpLastPromotionDate", dto.EmpLastPromotionDate);
            }
            if (dto.EmpLastIncrementNullDate == "")
            {
                param5 = new SqlParameter("@EmpLastIncrementDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@EmpLastIncrementDate", dto.EmpLastIncrementDate);
            }

            if (dto.EmpDOBNullDate == "")
            {
                param6 = new SqlParameter("@EmpDOB", DBNull.Value);
            }
            else
            {
                param6 = new SqlParameter("@EmpDOB", dto.EmpDOB);
            }
            if (dto.EmpIssueNullDate == "")
            {
                param7 = new SqlParameter("@EmpIssueDate", DBNull.Value);
            }
            else
            {
                param7 = new SqlParameter("@EmpIssueDate", dto.EmpIssueDate);
            }
            if (dto.EmpPExpireNullDate == "")
            {
                param8 = new SqlParameter("@EmpPExpireDate", DBNull.Value);
            }
            else
            {
                param8 = new SqlParameter("@EmpPExpireDate", dto.EmpPExpireDate);
            }
            if (dto.EmpLExpiryNullDate == "")
            {
                param9 = new SqlParameter("@EmpLExpiryDate", DBNull.Value);
            }
            else
            {
                param9 = new SqlParameter("@EmpLExpiryDate", dto.EmpLExpiryDate);
            }
            if (dto.StatusNullDate == "")
            {
                param10 = new SqlParameter("@StatusDate", DBNull.Value);
            }
            else
            {
                param10 = new SqlParameter("@StatusDate", dto.StatusDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmployeeDataInsert", new object[] { dto.EmployeeID, dto.EmployeeName, dto.EmpBaseGrade, dto.EmpGrade, dto.EmpGradeDesc, dto.EmpDesignation, dto.EmpDesigDesc, dto.ServiceType, dto.EmpSTypeDesc, dto.EmpArea, dto.EmpAreaDesc, dto.EmpLocation, dto.EmpLocationDesc, dto.EmpDepartment, dto.EmpDepartmentDesc, dto.EmpSection, dto.EmpSectionDesc, dto.EmpProject, dto.EmpProjectDesc, param1, param2, param3, param4, param5, dto.EmpBank, dto.EmpAccNo, dto.EmpFatherName, dto.EmpMotherName, param6, dto.EmpSpouseName, dto.EmpNationality, dto.EmpNationalityDesc, dto.EmpRelagion, dto.EmpRelagionDesc, dto.EmpGender, dto.EmpGenderDesc, dto.EmpMaritalStat, dto.EmpMaritalStatDesc, dto.EmpBloodGrp, dto.EmpHeight, dto.EmpNationalID, dto.TIN, dto.EmpPPNo, param7, param8, dto.EmpPlaceofIssue, dto.EmpLicenseNo, param9, dto.EmpPayLabel, dto.EmpConsolidatedAmt, dto.EmpConsolidatedDesc, dto.Status, param10 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZEMPLOYEEDTO GetInformation( int EmpID)
        {
            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZEMPLOYEE WHERE EmpCode = '" + EmpID + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZHRMCUS");
            var p = new A2ZEMPLOYEEDTO();
            if (dt.Rows.Count > 0)
            {
                p.EmployeeID = Converter.GetInteger(dt.Rows[0]["EmpCode"]);
                p.EmployeeName = Converter.GetString(dt.Rows[0]["EmpName"]);
                p.EmpBaseGrade = Converter.GetSmallInteger(dt.Rows[0]["EmpBaseGrade"]);
                p.EmpGrade = Converter.GetSmallInteger(dt.Rows[0]["EmpGrade"]);
                p.EmpGradeDesc = Converter.GetString(dt.Rows[0]["EmpGradeDesc"]);
                p.EmpDesignation = Converter.GetSmallInteger(dt.Rows[0]["EmpDesignation"]);
                p.EmpDesigDesc = Converter.GetString(dt.Rows[0]["EmpDesigDesc"]);
                p.ServiceType = Converter.GetSmallInteger(dt.Rows[0]["EmpServiceType"]);
                p.EmpSTypeDesc = Converter.GetString(dt.Rows[0]["EmpSTypeDesc"]);
                p.EmpArea = Converter.GetInteger(dt.Rows[0]["EmpArea"]);
                p.EmpAreaDesc = Converter.GetString(dt.Rows[0]["EmpAreaDesc"]);
                p.EmpLocation = Converter.GetInteger(dt.Rows[0]["EmpLocation"]);
                p.EmpLocationDesc = Converter.GetString(dt.Rows[0]["EmpLocationDesc"]);
                p.EmpSection = Converter.GetSmallInteger(dt.Rows[0]["EmpSection"]);
                p.EmpSectionDesc = Converter.GetString(dt.Rows[0]["EmpSectionDesc"]);
                p.EmpDepartment = Converter.GetSmallInteger(dt.Rows[0]["EmpDepartment"]);
                p.EmpDepartmentDesc = Converter.GetString(dt.Rows[0]["EmpDepartmentDesc"]);
                p.EmpProject = Converter.GetSmallInteger(dt.Rows[0]["EmpProject"]);
                p.EmpProjectDesc = Converter.GetString(dt.Rows[0]["EmpProjectDesc"]);

                p.EmpCashCode = Converter.GetInteger(dt.Rows[0]["EmpCashCode"]);

                p.EmpJoinDate = Converter.GetDateTime(dt.Rows[0]["EmpJoinDate"]);
                p.EmpPerDate = Converter.GetDateTime(dt.Rows[0]["EmpPerDate"]);
                p.EmpLastPostingDate = Converter.GetDateTime(dt.Rows[0]["EmpLastPostingDate"]);
                p.EmpLastPromotionDate = Converter.GetDateTime(dt.Rows[0]["EmpLastPromotionDate"]);
                p.EmpLastIncrementDate = Converter.GetDateTime(dt.Rows[0]["EmpLastIncrementDate"]);
                p.EmpBank = Converter.GetSmallInteger(dt.Rows[0]["EmpBank"]);
                p.EmpAccNo = Converter.GetString(dt.Rows[0]["EmpAccNo"]);
                p.EmpFatherName = Converter.GetString(dt.Rows[0]["EmpFName"]);
                p.EmpMotherName = Converter.GetString(dt.Rows[0]["EmpMName"]);
                p.EmpDOB = Converter.GetDateTime(dt.Rows[0]["EmpDOB"]);
                p.EmpSpouseName = Converter.GetString(dt.Rows[0]["EmpSpouseName"]);
                p.EmpNationality = Converter.GetSmallInteger(dt.Rows[0]["EmpNationality"]);
                p.EmpNationalityDesc = Converter.GetString(dt.Rows[0]["EmpNationalityDesc"]);

                p.EmpRelagion = Converter.GetSmallInteger(dt.Rows[0]["EmpRelagion"]);
                p.EmpRelagionDesc = Converter.GetString(dt.Rows[0]["EmpRelagionDesc"]);

                p.EmpGender = Converter.GetSmallInteger(dt.Rows[0]["EmpGender"]);
                p.EmpGenderDesc = Converter.GetString(dt.Rows[0]["EmpGenderDesc"]);

                p.EmpMaritalStat = Converter.GetSmallInteger(dt.Rows[0]["EmpMaritalStat"]);
                p.EmpMaritalStatDesc = Converter.GetString(dt.Rows[0]["EmpMaritalStatDesc"]);

                p.EmpBloodGrp = Converter.GetString(dt.Rows[0]["EmpBloodGrp"]);
                p.EmpHeight = Converter.GetString(dt.Rows[0]["EmpHeight"]);
                p.EmpNationalID = Converter.GetString(dt.Rows[0]["EmpNationalID"]);
                p.TIN = Converter.GetString(dt.Rows[0]["EmpTIN"]);
                p.EmpPPNo = Converter.GetString(dt.Rows[0]["EmpPPNo"]);
                p.EmpIssueDate = Converter.GetDateTime(dt.Rows[0]["EmpIssueDate"]);
                p.EmpPExpireDate = Converter.GetDateTime(dt.Rows[0]["EmpPExpireDate"]);
                p.EmpPlaceofIssue = Converter.GetString(dt.Rows[0]["EmpPlaceofIssue"]);
                p.EmpLicenseNo = Converter.GetString(dt.Rows[0]["EmpLicenseNo"]);
                p.EmpLExpiryDate = Converter.GetDateTime(dt.Rows[0]["EmpLExpiryDate"]);
                p.EmpPayLabel = Converter.GetSmallInteger(dt.Rows[0]["EmpPayLabel"]);

                p.EmpConsolidatedAmt = Converter.GetDecimal(dt.Rows[0]["EmpConsolidatedAmt"]);
                p.EmpConsolidatedDesc = Converter.GetString(dt.Rows[0]["EmpConsolidatedDesc"]);

                p.Status = Converter.GetSmallInteger(dt.Rows[0]["Status"]);
                p.StatusDate = Converter.GetDateTime(dt.Rows[0]["StatusDate"]);
                return p;


            }
            p.EmployeeID = 0;
            return p;
        }

        public static int UpdateInformation(A2ZEMPLOYEEDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@EmpJoinDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@EmpPerDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@EmpLastPostingDate ", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@EmpLastPromotionDate ", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@EmpLastIncrementDate ", DBNull.Value);

            SqlParameter param6 = new SqlParameter("@EmpDOB ", DBNull.Value);
            SqlParameter param7 = new SqlParameter("@EmpIssueDate", DBNull.Value);
            SqlParameter param8 = new SqlParameter("@EmpPExpireDate", DBNull.Value);
            SqlParameter param9 = new SqlParameter("@EmpLExpiryDate", DBNull.Value);
            SqlParameter param10 = new SqlParameter("@StatusDate", DBNull.Value);

            if (dto.EmpJoinNullDate == "")
            {

                param1 = new SqlParameter("@EmpJoinDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@EmpJoinDate", dto.EmpJoinDate);
            }

            if (dto.EmpPerNullDate == "")
            {
                param2 = new SqlParameter("@EmpPerDate", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@EmpPerDate", dto.EmpPerDate);

            }


            if (dto.EmpLastPostingNullDate == "")
            {
                param3 = new SqlParameter("@EmpLastPostingDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@EmpLstPostingDate", dto.EmpLastPostingDate);
            }

            if (dto.EmpLastPromotionNullDate == "")
            {
                param4 = new SqlParameter("@EmpLastPromotionDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@EmpLastPromotionDate", dto.EmpLastPromotionDate);
            }
            if (dto.EmpLastIncrementNullDate == "")
            {
                param5 = new SqlParameter("@EmpLastIncrementDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@EmpLastIncrementDate", dto.EmpLastIncrementDate);
            }
            
                        
            if (dto.EmpDOBNullDate == "")
            {
                param6 = new SqlParameter("@EmpDOB", DBNull.Value);
            }
            else
            {
                param6 = new SqlParameter("@EmpDOB", dto.EmpDOB);
            }
            if (dto.EmpIssueNullDate == "")
            {
                param7 = new SqlParameter("@EmpIssueDate", DBNull.Value);
            }
            else
            {
                param7 = new SqlParameter("@EmpIssueDate", dto.EmpIssueDate);
            }
            if (dto.EmpPExpireNullDate == "")
            {
                param8 = new SqlParameter("@EmpPExpireDate", DBNull.Value);
            }
            else
            {
                param8 = new SqlParameter("@EmpPExpireDate", dto.EmpPExpireDate);
            }
            if (dto.EmpLExpiryNullDate == "")
            {
                param9 = new SqlParameter("@EmpLExpiryDate", DBNull.Value);
            }
            else
            {
                param9 = new SqlParameter("@EmpLExpiryDate", dto.EmpLExpiryDate);
            }



            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmployeeDataUpdate", new object[] { dto.EmployeeID, dto.EmployeeName, dto.EmpBaseGrade, dto.EmpGrade, dto.EmpGradeDesc, dto.EmpDesignation, dto.ServiceType, dto.EmpSTypeDesc, dto.EmpArea, dto.EmpLocation, dto.EmpDepartment, dto.EmpSection, dto.EmpProject, param1, param2, param3, param4, param5, param6, dto.EmpBank, dto.EmpAccNo, dto.EmpFatherName, dto.EmpMotherName, dto.EmpSpouseName, dto.EmpNationality, dto.EmpNationalityDesc, dto.EmpRelagion, dto.EmpRelagionDesc, dto.EmpGender, dto.EmpGenderDesc, dto.EmpMaritalStat, dto.EmpMaritalStatDesc, dto.EmpBloodGrp, dto.EmpHeight, dto.EmpNationalID, dto.TIN, dto.EmpPPNo, param7, param8, dto.EmpPlaceofIssue, dto.EmpLicenseNo, param9, dto.EmpPayLabel, dto.EmpConsolidatedAmt, dto.EmpConsolidatedDesc });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static int UpdateEmpStatus(A2ZEMPLOYEEDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;
            SqlParameter param1 = new SqlParameter("@EmpStatusDate", DBNull.Value);
            if (dto.StatusNullDate == "")
            {
                param1 = new SqlParameter("@EmpStatusDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@EmpStatusDate", dto.StatusDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmpStatusChangeUpdate", new object[] { dto.EmployeeID, dto.Status, dto.StatusDesc, param1 });
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





