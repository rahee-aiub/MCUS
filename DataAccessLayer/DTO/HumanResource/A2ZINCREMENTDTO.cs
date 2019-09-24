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
     
    public class A2ZINCREMENTDTO
    {
        #region Properties

        public int Id { set; get; }
        public int EmpCode { set; get; }
        public DateTime EmpIncrementDate { set; get; }

        public Int16 EmpOldBaseGrade { get; set; }
        public String EmpOldBaseGradeDesc { set; get; }
        public int EmpOldGrade { get; set; }
        public String EmpOldGradeDesc { set; get; }
        public int EmpOldPayScale { set; get; }
        public String EmpOldPayScaleDesc { set; get; }
        public int EmpOldPayLabel { set; get; }
        public Decimal EmpOldBasic { set; get; }

        public Int16 EmpNewBaseGrade { get; set; }
        public String EmpNewBaseGradeDesc { set; get; }
        public int EmpNewGrade { get; set; }
        public String EmpNewGradeDesc { set; get; }
        public int EmpNewPayScale { set; get; }
        public String EmpNewPayScaleDesc { set; get; }
        public int EmpNewPayLabel { set; get; }
        public Decimal EmpNewBasic { set; get; }
        public DateTime EmpLastIncrementDate { set; get; }
        public String IncrementNullDate { set; get; }
        public String LIncrementNullDate { set; get; }
        
        

        #endregion


        public static int InsertInformation(A2ZINCREMENTDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@IncrementDate", DBNull.Value);
            

            if (dto.IncrementNullDate == "")
            {

                param1 = new SqlParameter("@IncrementDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@IncrementDate", dto.EmpIncrementDate);
            }

            SqlParameter param2 = new SqlParameter("@LastIncrementDate", DBNull.Value);


            if (dto.LIncrementNullDate == "")
            {

                param2 = new SqlParameter("@LastIncrementDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@LastIncrementDate", dto.EmpLastIncrementDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_HRIncrementInsert", new object[] { dto.EmpCode, dto.EmpOldBaseGrade, dto.EmpOldBaseGradeDesc, dto.EmpOldGrade, dto.EmpOldGradeDesc, dto.EmpOldPayScaleDesc, dto.EmpOldPayLabel, dto.EmpOldBasic, dto.EmpNewBaseGrade, dto.EmpNewBaseGradeDesc, dto.EmpNewGrade, dto.EmpNewGradeDesc, dto.EmpNewPayScaleDesc, dto.EmpNewPayLabel, dto.EmpNewBasic, param1, param2 });

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





