using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZCUNIONDTO
    {
        #region Propertise

        public int ID { set; get; }
        public Int16 CuType { set; get; }
        public String CuTypeName { set; get; }
        public int CreditUnionNo { set; get; }
        public String CreditUnionName { set; get; }
        public DateTime opendate { set; get; }
        public Int16 MemberFlag { set; get; }

        public int CertificateNo { set; get; }
        public String AddressL1 { set; get; }
        public String AddressL2 { set; get; }
        public String AddressL3 { set; get; }
        public String TelephoneNo { set; get; }
        public String MobileNo { set; get; }
        public String Fax { set; get; }
        public String email { set; get; }
        public int Division { set; get; }
        public int District { set; get; }
        public int Upzila { set; get; }
        public int Thana { set; get; }
        public Int16 CuProcFlag { set; get; }
        public String CuProcDesc { set; get; }
        public Int16 CuStatus { set; get; }
        public DateTime CuStatusDate { set; get; }
        public Int16 CuAffiCuType { set; get; }
        public String CuAffiCuTypeName { set; get; }
        public int CuAffiCuNo { set; get; }
        public Int16 CuAssoCuType { set; get; }
        public String CuAssoCuTypeName { set; get; }
        public int CuAssoCuNo { set; get; }
        public Int16 CuReguCuType { set; get; }
        public String CuReguCuTypeName { set; get; }
        public int CuReguCuNo { set; get; }
        public Int16 CuPrevCuType { set; get; }
        public String CuPrevCuTypeName { set; get; }
        public int CuPrevCuNo { set; get; }
        public Int16 CuNewCuType { set; get; }
        public String CuNewCuTypeName { set; get; }
        public int CuNewCuNo { set; get; }
        public int CuOldCuNo { set; get; }
        public int GLCashCode { set; get; }
        public Int16 InputBy { set; get; }
        public Int16 VerifyBy { set; get; }
        public Int16 ApprovBy { set; get; }
        public DateTime InputByDate { set; get; }
        public DateTime VerifyByDate { set; get; }
        public DateTime ApprovByDate { set; get; }
        public Int16 NoRecord { set; get; }
        public DateTime ValueDate { set; get; }
        public Int16 UserId { set; get; }
        public DateTime CreateDate { set; get; }

        public String NullOpenDate { set; get; }
        public String NullStatusDate { set; get; }
        public String NullInputByDate { set; get; }
        public String NullVerifyByDate { set; get; }
        public String NullApprovByDate { set; get; }
        public String NullValueDate { set; get; }
        public String NullCreateDate { set; get; }
        #endregion


        //        public static int InsertInformation(A2ZCUNIONDTO dto)
        //        {
        //            dto.CreditUnionName = (dto.CreditUnionName != null) ? dto.CreditUnionName.Trim().Replace("'", "''") : "";
        //            int rowEffect = 0;
        //            string strQuery = @"INSERT into A2ZCUNION(CuNo,CuName,CuOpDt,CuFlag,CuType,CuTypeName,CuCertNo,CuAddL1,CuAddL2,
        //                                CuAddL3,CuTel,CuMobile,CuFax,CuEmail,CuDivi,CuDist,CuUpzila,CuThana,CuProcFlag,CuProcDesc,CuStatus,CuStatusDate,
        //                                CuAffiCuType,CuAffiCuTypeName,CuAffiCuNo,CuAssoCuType,CuAssoCuTypeName,CuAssoCuNo,CuReguCuType,CuReguCuTypeName,CuReguCuNo,
        //                       CuOldCuNo,InputBy,VerifyBy,
        //                       ApprovBy,InputByDate,VerifyByDate,ApprovByDate,GLCashCode)values('" + dto.CreditUnionNo + "','" + dto.CreditUnionName + "','" + dto.opendate +
        //                                 "','" + dto.MemberFlag + "','" + dto.CuType + "','" + dto.CuTypeName + "','" + dto.CertificateNo + "','" + dto.AddressL1 +
        //                                 "','" + dto.AddressL2 + "','" + dto.AddressL3 + "','" + dto.TelephoneNo + "','" + dto.MobileNo +
        //                                 "','" + dto.Fax + "','" + dto.email + "','" + dto.Division + "','" + dto.District +
        //                                 "','" + dto.Upzila + "','" + dto.Thana + "','" + dto.CuProcFlag + "','" + dto.CuProcDesc + "','" + dto.CuStatus + "','" + dto.CuStatusDate +
        //                                 "','" + dto.CuAffiCuType + "','" + dto.CuAffiCuTypeName + "','" + dto.CuAffiCuNo + "','" + dto.CuAssoCuType +
        //                                 "','" + dto.CuAssoCuTypeName + "','" + dto.CuAssoCuNo + "','" + dto.CuReguCuType +
        //                                 "','" + dto.CuReguCuTypeName + "','" + dto.CuReguCuNo + "','" + dto.OldCuNo + "','" + dto.InputBy + "','" + dto.VerifyBy + 
        //                                 "','" + dto.ApprovBy + 
        //                                 "','" + dto.InputByDate + "','" + dto.VerifyByDate + "','" + dto.ApprovByDate + "','" + dto.GLCashCode + "')";
        //            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

        //            if (rowEffect == 0)
        //            {
        //                return 0;
        //            }
        //            else
        //            {
        //                return 1;
        //            }
        //        }

        public static int InsertInformation(A2ZCUNIONDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@CuOpDt", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@CuStatusDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@InputByDate ", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@VerifyByDate ", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@ApprovByDate ", DBNull.Value);
            SqlParameter param6 = new SqlParameter("@ValueDate", DBNull.Value);
            SqlParameter param7 = new SqlParameter("@CreateDate", DBNull.Value);


            if (dto.NullOpenDate == "")
            {

                param1 = new SqlParameter("@CuOpDt", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@CuOpDt", dto.opendate);
            }

            if (dto.NullStatusDate == "")
            {
                param2 = new SqlParameter("@CuStatusDate", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@CuStatusDate", dto.CuStatusDate);

            }


            if (dto.NullInputByDate == "")
            {
                param3 = new SqlParameter("@InputByDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@InputByDate", dto.InputByDate);
            }

            if (dto.NullVerifyByDate == "")
            {
                param4 = new SqlParameter("@VerifyByDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@VerifyByDate", dto.VerifyByDate);
            }
            if (dto.NullApprovByDate == "")
            {
                param5 = new SqlParameter("@ApprovByDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@ApprovByDate", dto.ApprovByDate);
            }
            if (dto.NullValueDate == "")
            {
                param6 = new SqlParameter("@ValueDate", DBNull.Value);
            }
            else
            {
                param6 = new SqlParameter("@ValueDate", dto.ValueDate);
            }
            if (dto.NullCreateDate == "")
            {
                param7 = new SqlParameter("@CreateDate", DBNull.Value);
            }
            else
            {
                param7 = new SqlParameter("@CreateDate", dto.CreateDate);
            }


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSCreditUnionDataInsert", new object[] { dto.CuType, dto.CuTypeName, dto.CreditUnionNo, dto.CreditUnionName, param1, dto.MemberFlag, dto.CertificateNo, dto.AddressL1, dto.AddressL2, dto.AddressL3, dto.TelephoneNo, dto.MobileNo, dto.Fax, dto.email, dto.Division, dto.District, dto.Upzila, dto.Thana, dto.CuProcFlag, dto.CuProcDesc, dto.CuStatus, param2, dto.CuAffiCuType, dto.CuAffiCuTypeName, dto.CuAffiCuNo, dto.CuAssoCuType, dto.CuAssoCuTypeName, dto.CuAssoCuNo, dto.CuReguCuType, dto.CuReguCuTypeName, dto.CuReguCuNo, dto.GLCashCode, dto.InputBy, dto.VerifyBy, dto.ApprovBy, param3, param4, param5, param6, dto.UserId, param7 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static A2ZCUNIONDTO GetOldInfo(int CNo)
        {

            //string c = "";
            //string x = Converter.GetString(CNo);
            //int a = x.Length;
            //string b = x;
            //c = b.Substring(0, 1);
            //int re = Converter.GetSmallInteger(c);
            //int dd = a - 1;
            //string d = b.Substring(1, dd);
            //int re1 = Converter.GetSmallInteger(d);

            //Int16 CuType = Converter.GetSmallInteger(re);
            //int CUNo = Converter.GetSmallInteger(re1);
            
                        
            var prm = new object[1];

            //prm[0] = CuType;
            //prm[1] = CUNo;
            prm[0] = CNo;



            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoOldCU", prm, "A2ZCSMCUS");



            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCUNION WHERE CuType = '" + CUType + "' and CUNO = " + CUNo, "A2ZCSMCUS");


            var p = new A2ZCUNIONDTO();
            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["Id"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuTypeName = Converter.GetString(dt.Rows[0]["CuTypeName"]);
                p.CreditUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.CreditUnionName = Converter.GetString(dt.Rows[0]["CuName"]);
                p.opendate = Converter.GetDateTime(dt.Rows[0]["CuOpDt"]);
                p.MemberFlag = Converter.GetSmallInteger(dt.Rows[0]["CuFlag"]);
                p.CertificateNo = Converter.GetInteger(dt.Rows[0]["CuCertNo"]);
                p.AddressL1 = Converter.GetString(dt.Rows[0]["CuAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["CuAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["CuAddL3"]);
                p.TelephoneNo = Converter.GetString(dt.Rows[0]["CuTel"]);
                p.MobileNo = Converter.GetString(dt.Rows[0]["CuMobile"]);
                p.Fax = Converter.GetString(dt.Rows[0]["CuFax"]);
                p.email = Converter.GetString(dt.Rows[0]["CuEmail"]);
                p.Division = Converter.GetInteger(dt.Rows[0]["CuDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["CuDist"]);
                p.Upzila = Converter.GetInteger(dt.Rows[0]["CuUpzila"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["CuThana"]);

                p.CuProcFlag = Converter.GetSmallInteger(dt.Rows[0]["CuProcFlag"]);
                p.CuProcDesc = Converter.GetString(dt.Rows[0]["CuProcDesc"]);
                p.CuStatus = Converter.GetSmallInteger(dt.Rows[0]["CuStatus"]);
                p.CuStatusDate = Converter.GetDateTime(dt.Rows[0]["CuStatusDate"]);

                p.CuAffiCuType = Converter.GetSmallInteger(dt.Rows[0]["CuAffiCuType"]);
                p.CuAffiCuTypeName = Converter.GetString(dt.Rows[0]["CuAffiCuTypeName"]);
                p.CuAffiCuNo = Converter.GetInteger(dt.Rows[0]["CuAffiCuNo"]);

                p.CuAssoCuType = Converter.GetSmallInteger(dt.Rows[0]["CuAssoCuType"]);
                p.CuAssoCuTypeName = Converter.GetString(dt.Rows[0]["CuAssoCuTypeName"]);
                p.CuAssoCuNo = Converter.GetInteger(dt.Rows[0]["CuAssoCuNo"]);

                p.CuReguCuType = Converter.GetSmallInteger(dt.Rows[0]["CuReguCuType"]);
                p.CuReguCuTypeName = Converter.GetString(dt.Rows[0]["CuReguCuTypeName"]);
                p.CuReguCuNo = Converter.GetInteger(dt.Rows[0]["CuReguCuNo"]);


                //p.CuOldCuNo = Converter.GetInteger(dt.Rows[0]["CuOldCuNo"]);

                p.InputBy = Converter.GetSmallInteger(dt.Rows[0]["InputBy"]);
                p.VerifyBy = Converter.GetSmallInteger(dt.Rows[0]["VerifyBy"]);
                p.ApprovBy = Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);

                p.InputByDate = Converter.GetDateTime(dt.Rows[0]["InputByDate"]);
                p.VerifyByDate = Converter.GetDateTime(dt.Rows[0]["VerifyByDate"]);
                p.ApprovByDate = Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);

                //p.ValueDate = Converter.GetDateTime(dt.Rows[0]["ValueDate"]);
                //p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLCashCode"]);
                p.NoRecord = 1;


                return p;
            }
            p.CreditUnionNo = 0;
            p.NoRecord = 0;

            return p;
            
            

        }

        public static A2ZCUNIONDTO GetInformation(Int16 CUType, int CUNo)
        {
            var prm = new object[2];

            prm[0] = CUType;
            prm[1] = CUNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoCreditUnion", prm, "A2ZCSMCUS");



            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCUNION WHERE CuType = '" + CUType + "' and CUNO = " + CUNo, "A2ZCSMCUS");


            var p = new A2ZCUNIONDTO();
            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["Id"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuTypeName = Converter.GetString(dt.Rows[0]["CuTypeName"]);
                p.CreditUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.CreditUnionName = Converter.GetString(dt.Rows[0]["CuName"]);
                p.opendate = Converter.GetDateTime(dt.Rows[0]["CuOpDt"]);
                p.MemberFlag = Converter.GetSmallInteger(dt.Rows[0]["CuFlag"]);
                p.CertificateNo = Converter.GetInteger(dt.Rows[0]["CuCertNo"]);
                p.AddressL1 = Converter.GetString(dt.Rows[0]["CuAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["CuAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["CuAddL3"]);
                p.TelephoneNo = Converter.GetString(dt.Rows[0]["CuTel"]);
                p.MobileNo = Converter.GetString(dt.Rows[0]["CuMobile"]);
                p.Fax = Converter.GetString(dt.Rows[0]["CuFax"]);
                p.email = Converter.GetString(dt.Rows[0]["CuEmail"]);
                p.Division = Converter.GetInteger(dt.Rows[0]["CuDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["CuDist"]);
                p.Upzila = Converter.GetInteger(dt.Rows[0]["CuUpzila"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["CuThana"]);

                p.CuProcFlag = Converter.GetSmallInteger(dt.Rows[0]["CuProcFlag"]);
                p.CuProcDesc = Converter.GetString(dt.Rows[0]["CuProcDesc"]);
                p.CuStatus = Converter.GetSmallInteger(dt.Rows[0]["CuStatus"]);
                p.CuStatusDate = Converter.GetDateTime(dt.Rows[0]["CuStatusDate"]);

                p.CuAffiCuType = Converter.GetSmallInteger(dt.Rows[0]["CuAffiCuType"]);
                p.CuAffiCuTypeName = Converter.GetString(dt.Rows[0]["CuAffiCuTypeName"]);
                p.CuAffiCuNo = Converter.GetInteger(dt.Rows[0]["CuAffiCuNo"]);

                p.CuAssoCuType = Converter.GetSmallInteger(dt.Rows[0]["CuAssoCuType"]);
                p.CuAssoCuTypeName = Converter.GetString(dt.Rows[0]["CuAssoCuTypeName"]);
                p.CuAssoCuNo = Converter.GetInteger(dt.Rows[0]["CuAssoCuNo"]);

                p.CuReguCuType = Converter.GetSmallInteger(dt.Rows[0]["CuReguCuType"]);
                p.CuReguCuTypeName = Converter.GetString(dt.Rows[0]["CuReguCuTypeName"]);
                p.CuReguCuNo = Converter.GetInteger(dt.Rows[0]["CuReguCuNo"]);


                p.CuOldCuNo = Converter.GetInteger(dt.Rows[0]["CuOldCuNo"]);

                p.InputBy = Converter.GetSmallInteger(dt.Rows[0]["InputBy"]);
                p.VerifyBy = Converter.GetSmallInteger(dt.Rows[0]["VerifyBy"]);
                p.ApprovBy = Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);

                p.InputByDate = Converter.GetDateTime(dt.Rows[0]["InputByDate"]);
                p.VerifyByDate = Converter.GetDateTime(dt.Rows[0]["VerifyByDate"]);
                p.ApprovByDate = Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);

                //p.ValueDate = Converter.GetDateTime(dt.Rows[0]["ValueDate"]);
                //p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLCashCode"]);

                p.NoRecord = 1;


                return p;
            }
            p.CreditUnionNo = 0;
            p.NoRecord = 0;

            return p;

        }
        //public static int UpdateInformation(A2ZCUNIONDTO dto)
        //{
        //    dto.CreditUnionName = (dto.CreditUnionName != null) ? dto.CreditUnionName.Trim().Replace("'", "''") : "";
        //    int rowEffect = 0;
        //    string strQuery = "UPDATE A2ZCUNION set CuType='" + dto.CuType + "',CuTypeName='" + dto.CuTypeName + "',CuNo='" + dto.CreditUnionNo + "',CuName='" + dto.CreditUnionName +
        //                    "',CuOpDt='" + dto.opendate + "',CuFlag='" + dto.MemberFlag + "',CuCertNo='" + dto.CertificateNo +
        //                    "',CuAddL1='" + dto.AddressL1 + "',CuAddL2='" + dto.AddressL2 + "',CuAddL3='" + dto.AddressL3 +
        //                    "',CuTel='" + dto.TelephoneNo + "',CuMobile='" + dto.MobileNo + "',CuFax='" + dto.Fax +
        //                    "',CuEmail='" + dto.email + "',CuDivi='" + dto.Division + "',CuDist='" + dto.District +
        //                    "',CuUpzila='" + dto.Upzila + "',CuThana='" + dto.Thana + "',CuProcFlag='" + dto.CuProcFlag + "',CuProcDesc='" + dto.CuProcDesc + 
        //                    "',CuStatus='" + dto.CuStatus +
        //                    "',CuStatusDate='" + dto.CuStatusDate +
        //                    "',CuAffiCuType='" + dto.CuAffiCuType + "',CuAffiCuTypeName='" + dto.CuAffiCuTypeName + 
        //                    "',CuAffiCuNo='" + dto.CuAffiCuNo + "',CuAssoCuType='" + dto.CuAssoCuType + 
        //                    "',CuAssoCuTypeName='" + dto.CuAssoCuTypeName + "',CuAssoCuNo='" + dto.CuAssoCuNo +
        //                    "',CuReguCuType='" + dto.CuReguCuType +
        //                    "',CuReguCuTypeName='" + dto.CuReguCuTypeName + "',CuReguCuNo='" + dto.CuReguCuNo +
        //                    "',InputBy='" + dto.InputBy + "',VerifyBy='" + dto.VerifyBy + "',ApprovBy='" + dto.ApprovBy + 
        //                    "',InputByDate='" + dto.InputByDate + "',VerifyByDate='" + dto.VerifyByDate +
        //                    "',ApprovByDate='" + dto.ApprovByDate + "',GLCashCode='" + dto.GLCashCode + "' where CuType = '" + dto.CuType + "' and CuNo ='" + dto.CreditUnionNo + "'";
        //    rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //    if (rowEffect == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        public static int UpdateInformation(A2ZCUNIONDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@CuOpDt", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@CuStatusDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@InputByDate ", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@VerifyByDate ", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@ApprovByDate ", DBNull.Value);
            SqlParameter param6 = new SqlParameter("@ValueDate", DBNull.Value);
            SqlParameter param7 = new SqlParameter("@CreateDate", DBNull.Value);


            if (dto.NullOpenDate == "")
            {

                param1 = new SqlParameter("@CuOpDt", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@CuOpDt", dto.opendate);
            }

            if (dto.NullStatusDate == "")
            {
                param2 = new SqlParameter("@CuStatusDate", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@CuStatusDate", dto.CuStatusDate);

            }


            if (dto.NullInputByDate == "")
            {
                param3 = new SqlParameter("@InputByDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@InputByDate", dto.InputByDate);
            }

            if (dto.NullVerifyByDate == "")
            {
                param4 = new SqlParameter("@VerifyByDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@VerifyByDate", dto.VerifyByDate);
            }
            if (dto.NullApprovByDate == "")
            {
                param5 = new SqlParameter("@ApprovByDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@ApprovByDate", dto.ApprovByDate);
            }
            if (dto.NullValueDate == "")
            {
                param6 = new SqlParameter("@ValueDate", DBNull.Value);
            }
            //else
            //{
            //    param6 = new SqlParameter("@ValueDate", dto.ValueDate);
            //}
            //if (dto.NullCreateDate == "")
            //{
            //    param7 = new SqlParameter("@CreateDate", DBNull.Value);
            //}
            //else
            //{
            //    param7 = new SqlParameter("@CreateDate", dto.CreateDate);
            //}


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSCreditUnionDataUpdate", new object[] { dto.CuType, dto.CuTypeName, dto.CreditUnionNo, dto.CreditUnionName, param1, dto.MemberFlag, dto.CertificateNo, dto.AddressL1, dto.AddressL2, dto.AddressL3, dto.TelephoneNo, dto.MobileNo, dto.Fax, dto.email, dto.Division, dto.District, dto.Upzila, dto.Thana, dto.CuProcFlag, dto.CuProcDesc, dto.CuStatus, param2, dto.CuAffiCuType, dto.CuAffiCuTypeName, dto.CuAffiCuNo, dto.CuAssoCuType, dto.CuAssoCuTypeName, dto.CuAssoCuNo, dto.CuReguCuType, dto.CuReguCuTypeName, dto.CuReguCuNo, dto.GLCashCode, dto.InputBy, dto.VerifyBy, dto.ApprovBy, param3, param4, param5, dto.UserId });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static int UpdateInformation1(A2ZCUNIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCUNION set CuType='" + dto.CuType + "', CuTypeName='" + dto.CuTypeName + "', CuNo='" + dto.CreditUnionNo + "', CuProcFlag='" + dto.CuProcFlag + "', CuOpDt='" + dto.opendate + "',CuStatusDate='" + dto.CuStatusDate + "', InputByDate='" + dto.InputByDate + "', VerifyByDate='" + dto.VerifyByDate + "', ApprovByDate='" + dto.ApprovByDate + "' where  Id='" + dto.ID + "' ";

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

        public static int Update1(A2ZCUNIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCUNION set GLCashCode='" + dto.GLCashCode + "' where  Id='" + dto.ID + "' ";

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
