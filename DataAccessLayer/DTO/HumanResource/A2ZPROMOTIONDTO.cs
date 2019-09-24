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
     
    public class A2ZPROMOTIONDTO
    {
        #region Properties

        public int Id { set; get; }
        public int EmpCode { set; get; }
        public DateTime EmpPromotionDate { set; get; }
        public int EmpOldArea { set; get; }
        public String EmpOldAreaDesc { set; get; }

        public int EmpOldLocation { set; get; }
        public String EmpOldLocationDesc { set; get; }
        public Int16 EmpOldSection { set; get; }
        public String EmpOldSectionDesc { set; get; }
        public Int16 EmpOldDepartment { set; get; }
        public String EmpOldDepartmentDesc { set; get; }
        public Int16 EmpOldProject { set; get; }
        public String EmpOldProjectDesc { set; get; }
        public Int16 EmpOldDesignation { set; get; }
        public String EmpOldDesigDesc { set; get; }
        public int EmpOldCashCode { set; get; }
        public Int16 EmpOldServiceType { set; get; }
        public String EmpOldSTypeDesc { set; get; }
        public Int16 EmpOldBaseGrade { get; set; }
        public String EmpOldBaseGradeDesc { set; get; }
        public int EmpOldGrade { get; set; }
        public String EmpOldGradeDesc { set; get; }
        public int EmpOldPayScale { set; get; }
        public String EmpOldPayScaleDesc { set; get; }
        public int EmpOldPayLabel { set; get; }
        public Decimal EmpOldBasic { set; get; }

        public int EmpNewArea { set; get; }
        public String EmpNewAreaDesc { set; get; }
        public int EmpNewLocation { set; get; }
        public String EmpNewLocationDesc { set; get; }
        public Int16 EmpNewSection { set; get; }
        public String EmpNewSectionDesc { set; get; }
        public Int16 EmpNewDepartment { set; get; }
        public String EmpNewDepartmentDesc { set; get; }
        public Int16 EmpNewProject { set; get; }
        public String EmpNewProjectDesc { set; get; }
        public Int16 EmpNewDesignation { set; get; }
        public String EmpNewDesigDesc { set; get; }
        public int EmpNewCashCode { set; get; }
        public Int16 EmpNewServiceType { set; get; }
        public String EmpNewSTypeDesc { set; get; }
        public Int16 EmpNewBaseGrade { get; set; }
        public String EmpNewBaseGradeDesc { set; get; }
        public int EmpNewGrade { get; set; }
        public String EmpNewGradeDesc { set; get; }
        public int EmpNewPayScale { set; get; }
        public String EmpNewPayScaleDesc { set; get; }
        public int EmpNewPayLabel { set; get; }
        public Decimal EmpNewBasic { set; get; }
        public DateTime EmpLastPromotionDate { set; get; }
        public String PromotionNullDate { set; get; }
        public String LPromotionNullDate { set; get; }
        
        

        #endregion


        public static int InsertInformation(A2ZPROMOTIONDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@PromotionDate", DBNull.Value);
            

            if (dto.PromotionNullDate == "")
            {

                param1 = new SqlParameter("@PromotionDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@PromotionDate", dto.EmpPromotionDate);
            }

            SqlParameter param2 = new SqlParameter("@LastPromotionDate", DBNull.Value);


            if (dto.LPromotionNullDate == "")
            {

                param2 = new SqlParameter("@LastPromotionDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@LastPromotionDate", dto.EmpLastPromotionDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_HRPromotionInsert", new object[] { dto.EmpCode, dto.EmpOldArea, dto.EmpOldAreaDesc, dto.EmpOldLocation, dto.EmpOldLocationDesc, dto.EmpOldSection, dto.EmpOldSectionDesc, dto.EmpOldDepartment, dto.EmpOldDepartmentDesc, dto.EmpOldProject, dto.EmpOldProjectDesc, dto.EmpOldDesignation, dto.EmpOldDesigDesc, dto.EmpOldCashCode, dto.EmpOldServiceType, dto.EmpOldSTypeDesc, dto.EmpOldBaseGrade, dto.EmpOldBaseGradeDesc, dto.EmpOldGrade, dto.EmpOldGradeDesc, dto.EmpOldPayScaleDesc, dto.EmpOldPayLabel, dto.EmpOldBasic, dto.EmpNewArea, dto.EmpNewAreaDesc, dto.EmpNewLocation, dto.EmpNewLocationDesc, dto.EmpNewSection, dto.EmpNewSectionDesc, dto.EmpNewDepartment, dto.EmpNewDepartmentDesc, dto.EmpNewProject, dto.EmpNewProjectDesc, dto.EmpNewDesignation, dto.EmpNewDesigDesc, dto.EmpNewCashCode, dto.EmpNewServiceType, dto.EmpNewSTypeDesc, dto.EmpNewBaseGrade, dto.EmpNewBaseGradeDesc, dto.EmpNewGrade, dto.EmpNewGradeDesc, dto.EmpNewPayScaleDesc, dto.EmpNewPayLabel, dto.EmpNewBasic, param1, param2 });

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





