using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;


namespace DataAccessLayer.DTO
{
    public class A2ZSYSIDSDTO
    {
        public int IdsNo { set; get; }
        public String IdsPass { set; get; }
        public Int16 IdsLevel { set; get; }
        public Int16 IdsLogInFlag { set; get; }
        public Int16 IdsLockFlag { set; get; }
        public String IdsName { set; get; }
        public Int16 IdsFlag { set; get; }
        public Int16 IdsType { set; get; }
        public Int16 IdsStatus { set; get; }
        public Int32 EmpCode { set; get; }
        public int UserId { set; get; }
        public int GLCashCode { set; get; }
        public DateTime CreateDate { set; get; }

        public Int16 module { set; get; }
        public Boolean SODflag { set; get; }
        public Boolean VPrintflag { set; get; }
        public Boolean IdsCWarehouseflag { set; get; }

        public static A2ZSYSIDSDTO GetUserInformation(int userId, string dbName)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSYSIDS WHERE IdsNo = " + userId, dbName);

            var p = new A2ZSYSIDSDTO();

            if (dt.Rows.Count > 0)
            {
                p.IdsNo = Converter.GetInteger(dt.Rows[0]["IdsNo"]);
                p.IdsPass = Converter.GetString(dt.Rows[0]["IdsPass"]);
                p.IdsLevel = Converter.GetSmallInteger(dt.Rows[0]["IdsLevel"]);
                p.IdsLogInFlag = Converter.GetSmallInteger(dt.Rows[0]["IdsLogInFlag"]);
                p.IdsLockFlag = Converter.GetSmallInteger(dt.Rows[0]["IdsLockFlag"]);
                p.IdsName = Converter.GetString(dt.Rows[0]["IdsName"]);
                p.IdsFlag = Converter.GetSmallInteger(dt.Rows[0]["IdsFlag"]);
                p.IdsType = Converter.GetSmallInteger(dt.Rows[0]["IdsType"]);
                p.IdsStatus = Converter.GetSmallInteger(dt.Rows[0]["IdsStatus"]);
                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLCashCode"]);
                p.EmpCode = Converter.GetInteger(dt.Rows[0]["EmpCode"]);

                p.SODflag = Converter.GetBoolean(dt.Rows[0]["IdsSODFlag"]);
                p.VPrintflag = Converter.GetBoolean(dt.Rows[0]["IdsVPrintFlag"]);
                p.IdsCWarehouseflag = Converter.GetBoolean(dt.Rows[0]["IdsCWarehouseFlag"]);
                return p;
            }

            p.IdsNo = 0;
            return p;

        }

        public static DropDownList GetDropDownList(DropDownList ddlUserId)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT IdsNo,IdsNo FROM A2ZSYSIDS", ddlUserId, "A2ZHKMCUS");
        }

        public static DropDownList GetDropDownList(DropDownList ddlUserId, string whereCondition)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT IdsNo,IdsNo FROM A2ZSYSIDS WHERE " + whereCondition, ddlUserId, "A2ZHKMCUS");
        }


        public static int UpdateUserCSLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
        }

        public static int UpdateUserGLLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZGLMCUS"));
        }
        public static int UpdateUserHKLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
        }

        public static int UpdateUserHRLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
        }
        public static int UpdateUserBTLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZBTMCUS"));
        }
        public static int UpdateUserSTLoginFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = " + nFlag + " WHERE IdsNo = " + userId;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZSTMCUS"));
        }
        public static int UpdateUserLockFlag(int userId, Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLockFlag = " + nFlag + " WHERE IdsNo = " + userId;
            Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
            Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
            Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
            Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZBTMCUS"));
            Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZSTMCUS"));
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZGLMCUS"));
        }

        public static int CountForSingleUserPurpose(int userId, string dbName)
        {
            string strQuery = "SELECT * FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0 AND IdsNo <> " + userId;
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, dbName);
            return dt.Rows.Count;
        }

        public static int InsertNewIdInformation(A2ZSYSIDSDTO objIds)
        {
            int rowwEffect = 0;

            string strQuery = "INSERT INTO A2ZSYSIDS(IdsNo,IdsName,IdsLevel,IdsFlag,EmpCode,UserId,IdsPass,IdsLogInFlag,IdsLockFlag,IdsType,IdsStatus,GLCashCode,IdsSODFlag,IdsVPrintFlag,IdsCWarehouseFlag)" +
                                "VALUES ('" + objIds.IdsNo + "','" + objIds.IdsName + "','" + objIds.IdsLevel + "','" + objIds.IdsFlag + "','" +
                                objIds.EmpCode + "','" + objIds.UserId + "','XXXXXXXX',0,0,0,0,'" + objIds.GLCashCode + "','" + objIds.SODflag + "','" + objIds.VPrintflag + "','" + objIds.IdsCWarehouseflag + "')";

            //rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
            
            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
            

           

            return rowwEffect;

        }

        public static int UpdateNewPassword(A2ZSYSIDSDTO objIds)
        {
            int rowwEffect = 0;

            string strQuery = "UPDATE A2ZSYSIDS SET IdsPass = '" + objIds.IdsPass + "' WHERE IdsNo = '" + objIds.IdsNo + "'";

            //rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));

            return rowwEffect;

        }

        public static int UpdateResetPassword(A2ZSYSIDSDTO objIds)
        {
            int rowwEffect = 0;

            string strQuery = "UPDATE A2ZSYSIDS SET IdsPass = '" + objIds.IdsPass + "' WHERE IdsNo = '" + objIds.IdsNo + "'";

            //rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));

            return rowwEffect;

        }

        public static int Update(A2ZSYSIDSDTO objIds)
        {
            int rowwEffect = 0;

            string strQuery = "UPDATE A2ZSYSIDS SET IdsLevel='" + objIds.IdsLevel + "',IdsName='" + objIds.IdsName + "',EmpCode='" + objIds.EmpCode + "',IdsFlag='" + objIds.IdsFlag + "', GLCashCode = '" + objIds.GLCashCode + "', IdsSODFlag = '" + objIds.SODflag + "', IdsVPrintFlag = '" + objIds.VPrintflag + "',IdsCWarehouseFlag = '" + objIds.IdsCWarehouseflag + "' WHERE IdsNo = '" + objIds.IdsNo + "'";

            //rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));


            return rowwEffect;

        }

    }
}
