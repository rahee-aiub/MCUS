using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.HumanResource
{
  public class A2ZEMPLEAVEDTO
    {
        #region Propertise

        public int EmpCode { set; get; }
        public int EmpLeaveCode { set; get; }
        public DateTime LStartDate { set; get; }
        public DateTime LEndDate { set; get; }
        public decimal EmpApplyDays { set; get; }
        public decimal EmpLBalance { set; get; }
        public string NullLStartDate { set; get; }
        public string NullLEndDate { set; get; }
        public string LPurpose { set; get; }
        public string LNote { set; get; }
        public Int16 LProcStat { get; set; }
        public int InputBy { get; set; }
        public DateTime InputByDate { set; get; }
        public int CheckBy { get; set; }
        public DateTime CheckByDate { set; get; }
        public int VerifyBy { get; set; }
        public DateTime VerifyByDate { set; get; }
        public int ApprovBy { get; set; }
        public DateTime ApprovByDate { set; get; }
        public string NullInputByDate { set; get; }
        public string NullCheckByDate { set; get; }
        public string NullVerifyByDate { set; get; }
        public string NullApprovByDate { set; get; }

        #endregion


        public static int InsertInformation(A2ZEMPLEAVEDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@LStartDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@LEndDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@InputByDate", DBNull.Value);

            if (dto.NullLStartDate == "")
            {

                param1 = new SqlParameter("@LStartDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@LStartDate", dto.LStartDate);
            }

            if (dto.NullLEndDate == "")
            {
                param2 = new SqlParameter("@LEndDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@LEndDate", dto.LEndDate);
            }

            if (dto.NullInputByDate == "")
            {
                param3 = new SqlParameter("@InputByDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@InputByDate", dto.InputByDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmpLeaveMaintenance", new object[] { dto.EmpCode, dto.EmpLeaveCode, param1, param2, dto.EmpApplyDays, dto.EmpLBalance, dto.LPurpose, dto.LNote, dto.LProcStat, dto.InputBy, param3 });

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
