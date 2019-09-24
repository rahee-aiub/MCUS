using System;

using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO
{
    public class A2ZAUDITDTO
    {
        #region Properties

        public DateTime AudSystemDate { set; get; }
        public Int16 AudRecordNo { set; get; }
        public Int16 ModuleNo { set; get; }
        public Int16 MenuNo { set; get; }
        public DateTime AudProcessDate { set; get; }
        public String AudOldX { set; get; }
        public String AudNewX { set; get; }
        public Double AudOldN { set; get; }
        public Double AudNewN { set; get; }
        public DateTime AudOldDate { set; get; }
        public DateTime AudNewDate { set; get; }
        public String AudRemarks { set; get; }

        public int UserId { set; get; }
        public Int16 EmpCode { set; get; }
        public string UserIP { set; get; }
        public string UserServerIP { set; get; }
        public string UserServerName { set; get; }

        #endregion


        public static int InsertAuditInformation(A2ZAUDITDTO objAudit)
        {
            int rowwEffect = 0;

            string strQuery =
                "INSERT INTO A2ZAUDIT(AudRecordNo,ModuleNo,MenuNo,AudProcessDate,AudOldX,AudNewX,AudOldN,AudNewN,AudOldDate,AudNewDate,AudRemarks," +
                "UserId,EmpCode,UserIP,UserServerIP,UserServerName) " +
                "VALUES (" + objAudit.AudRecordNo + "," + objAudit.ModuleNo + "," + objAudit.MenuNo + ",'" + objAudit.AudProcessDate + "','" + objAudit.AudOldX +
                "','" + objAudit.AudNewX + "'," + objAudit.AudOldN + "," + objAudit.AudNewN + ",'" + objAudit.AudOldDate + "','" + objAudit.AudNewDate + "','" +
                objAudit.AudRemarks + "'," + objAudit.UserId + "," + objAudit.EmpCode + ",'" + objAudit.UserIP + "','" +
                objAudit.UserServerIP + "','" + objAudit.UserServerName + "')";
            
            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
            
            return rowwEffect;
        }

    }
}
