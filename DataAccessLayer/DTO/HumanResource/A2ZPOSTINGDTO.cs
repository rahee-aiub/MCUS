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
     
    public class A2ZPOSTINGDTO
    {
        #region Properties

        public int Id { set; get; }
        public int EmpCode { set; get; }
        public DateTime PostingDate { set; get; }
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
        public DateTime LastPostingDate { set; get; }
        public String LPostingNullDate { set; get; }
        public String PostingNullDate { set; get; }
        
        

        #endregion


        public static int InsertInformation(A2ZPOSTINGDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@PostingDate", DBNull.Value);
            
            if (dto.PostingNullDate == "")
            {

                param1 = new SqlParameter("@PostingDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@PostingDate", dto.PostingDate);
            }

            SqlParameter param2 = new SqlParameter("@LastPostingDate", DBNull.Value);

            if (dto.LPostingNullDate == "")
            {

                param2 = new SqlParameter("@LastPostingDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@LastPostingDate", dto.LastPostingDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_HRPostingInsert", new object[] { dto.EmpCode, dto.EmpOldArea, dto.EmpOldAreaDesc, dto.EmpOldLocation, dto.EmpOldLocationDesc, dto.EmpOldSection, dto.EmpOldSectionDesc, dto.EmpOldDepartment, dto.EmpOldDepartmentDesc, dto.EmpOldProject, dto.EmpOldProjectDesc, dto.EmpOldDesignation, dto.EmpOldDesigDesc, dto.EmpOldCashCode, dto.EmpNewArea, dto.EmpNewAreaDesc, dto.EmpNewLocation, dto.EmpNewLocationDesc, dto.EmpNewSection, dto.EmpNewSectionDesc, dto.EmpNewDepartment, dto.EmpNewDepartmentDesc, dto.EmpNewProject, dto.EmpNewProjectDesc, dto.EmpNewDesignation, dto.EmpNewDesigDesc, dto.EmpNewCashCode, param1, param2 });

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





