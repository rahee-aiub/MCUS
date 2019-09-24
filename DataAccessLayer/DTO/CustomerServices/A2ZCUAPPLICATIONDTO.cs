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
    public class A2ZCUAPPLICATIONDTO
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
        public Int16 CuNewCuType { set; get; }
        public String CuNewCuTypeName { set; get; }
        public int CuNewCuNo { set; get; }
        public Int16 CuPrevCuType { set; get; }
        public String CuPrevCuTypeName { set; get; }
        public int CuPrevCuNo { set; get; }
        public int CuOldCuNo { set; get; }

        public int GLCashCode { set; get; }
        public Int16 InputBy { set; get; }
        public Int16 VerifyBy { set; get; }
        public Int16 ApprovBy { set; get; }
        public DateTime InputByDate { set; get; }
        public DateTime VerifyByDate { set; get; }
        public DateTime ApprovByDate { set; get; }
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


//        public static int InsertInformation(A2ZCUAPPLICATIONDTO dto)
//        {
//            dto.CreditUnionName = (dto.CreditUnionName != null) ? dto.CreditUnionName.Trim().Replace("'", "''") : "";
//            int rowEffect = 0;
//            string strQuery = @"INSERT into A2ZCUAPPLICATION(CuNo,CuName,CuOpDt,CuFlag,CuType,CuTypeName,CuCertNo,CuAddL1,CuAddL2,
//                                CuAddL3,CuTel,CuMobile,CuFax,CuEmail,CuDivi,CuDist,CuUpzila,CuThana,CuProcFlag,CuProcDesc,CuStatus,CuStatusDate,
//                                CuPrevCuType,CuPrevCuTypeName,CuPrevCuNo,CuNewCuType,CuNewCuTypeName,CuNewCuNo,InputBy,VerifyBy,
//                       ApprovBy,InputByDate,VerifyByDate,ApprovByDate,GLCashCode)values('" + dto.CreditUnionNo + "','" + dto.CreditUnionName + "','" + dto.opendate +
//                                 "','" + dto.MemberFlag + "','" + dto.CuType + "','" + dto.CuTypeName + "','" + dto.CertificateNo + "','" + dto.AddressL1 +
//                                 "','" + dto.AddressL2 + "','" + dto.AddressL3 + "','" + dto.TelephoneNo + "','" + dto.MobileNo +
//                                 "','" + dto.Fax + "','" + dto.email + "','" + dto.Division + "','" + dto.District +
//                                 "','" + dto.Upzila + "','" + dto.Thana + "','" + dto.CuProcFlag + "','" + dto.CuProcDesc + "','" + dto.CuStatus + "','" + dto.CuStatusDate +
//                                 "','" + dto.CuPrevCuType + "','" + dto.CuPrevCuTypeName + "','" + dto.CuPrevCuNo + "','" + dto.CuNewCuType + 
//                                 "','" + dto.CuNewCuTypeName + "','" + dto.CuNewCuNo + "','" + dto.InputBy + "','" + dto.VerifyBy + 
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

        public static int InsertInformation(A2ZCUAPPLICATIONDTO dto)
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


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSCUApplicationDataInsert", new object[] { dto.CuType, dto.CuTypeName, dto.CreditUnionNo, dto.CreditUnionName, param1, dto.MemberFlag, dto.CertificateNo, dto.AddressL1, dto.AddressL2, dto.AddressL3, dto.TelephoneNo, dto.MobileNo, dto.Fax, dto.email, dto.Division, dto.District, dto.Upzila, dto.Thana, dto.CuProcFlag, dto.CuProcDesc, dto.CuStatus, param2, dto.CuPrevCuType, dto.CuPrevCuTypeName, dto.CuPrevCuNo, dto.CuNewCuType, dto.CuNewCuTypeName, dto.CuNewCuNo, dto.CuOldCuNo, dto.GLCashCode, dto.InputBy, dto.VerifyBy, dto.ApprovBy, param3, param4, param5, param6, dto.UserId, param7 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }




        public static A2ZCUAPPLICATIONDTO GetInformation(Int16 CUType,int CUNo)
        {

            var prm = new object[2];
                        
            prm[0] = CUType;
            prm[1] = CUNo;
            


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoCuApplication", prm, "A2ZCSMCUS");
            
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCUAPPLICATION WHERE CuType = '" + CUType + "' and CUNO = " + CUNo, "A2ZCSMCUS");


            var p = new A2ZCUAPPLICATIONDTO();
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

                p.ValueDate = Converter.GetDateTime(dt.Rows[0]["ValueDate"]);
                p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);

                //p.CuPrevCuType = Converter.GetSmallInteger(dt.Rows[0]["CuPrevCuType"]);
                //p.CuPrevCuTypeName = Converter.GetString(dt.Rows[0]["CuPrevCuTypeName"]);
                //p.CuPrevCuNo = Converter.GetInteger(dt.Rows[0]["CuPrevCuNo"]);
                
                //p.CuNewCuType = Converter.GetSmallInteger(dt.Rows[0]["CuNewCuType"]);
                //p.CuNewCuTypeName = Converter.GetString(dt.Rows[0]["CuNewCuTypeName"]);
                //p.CuNewCuNo = Converter.GetInteger(dt.Rows[0]["CuNewCuNo"]);

                p.InputBy = Converter.GetSmallInteger(dt.Rows[0]["InputBy"]);
                p.VerifyBy = Converter.GetSmallInteger(dt.Rows[0]["VerifyBy"]);
                p.ApprovBy = Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);

                p.InputByDate = Converter.GetDateTime(dt.Rows[0]["InputByDate"]);
                p.VerifyByDate = Converter.GetDateTime(dt.Rows[0]["VerifyByDate"]);
                p.ApprovByDate = Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);

                p.GLCashCode = Converter.GetInteger(dt.Rows[0]["GLCashCode"]);


                return p;
            }
            p.CreditUnionNo = 0;

            return p;

        }


        //public static int UpdateInformation(A2ZCUAPPLICATIONDTO dto)
        //{
        //    dto.CreditUnionName = (dto.CreditUnionName != null) ? dto.CreditUnionName.Trim().Replace("'", "''") : "";
        //    int rowEffect = 0;
        //    string strQuery = "UPDATE A2ZCUAPPLICATION set CuType='" + dto.CuType + "',CuTypeName='" + dto.CuTypeName + "',CuNo='" + dto.CreditUnionNo + "',CuName='" + dto.CreditUnionName +
        //                    "',CuFlag='" + dto.MemberFlag + "',CuCertNo='" + dto.CertificateNo +
        //                    "',CuAddL1='" + dto.AddressL1 + "',CuAddL2='" + dto.AddressL2 + "',CuAddL3='" + dto.AddressL3 +
        //                    "',CuTel='" + dto.TelephoneNo + "',CuMobile='" + dto.MobileNo + "',CuFax='" + dto.Fax +
        //                    "',CuEmail='" + dto.email + "',CuDivi='" + dto.Division + "',CuDist='" + dto.District +
        //                    "',CuUpzila='" + dto.Upzila + "',CuThana='" + dto.Thana + "',CuProcFlag='" + dto.CuProcFlag + "',CuProcDesc='" + dto.CuProcDesc + 
        //                    "',CuStatus='" + dto.CuStatus +
        //                    "',CuStatusDate='" + dto.CuStatusDate +
        //                    "',CuPrevCuType='" + dto.CuPrevCuType + "',CuPrevCuTypeName='" + dto.CuPrevCuTypeName + 
        //                    "',CuPrevCuNo='" + dto.CuPrevCuNo + "',CuNewCuType='" + dto.CuNewCuType + 
        //                    "',CuNewCuTypeName='" + dto.CuNewCuTypeName + "',CuNewCuNo='" + dto.CuNewCuNo + 
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

        public static int UpdateInformation(A2ZCUAPPLICATIONDTO dto)
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


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSCUApplicationDataUpdate", new object[] { dto.CuType, dto.CuTypeName, dto.CreditUnionNo, dto.CreditUnionName, param1, dto.MemberFlag, dto.CertificateNo, dto.AddressL1, dto.AddressL2, dto.AddressL3, dto.TelephoneNo, dto.MobileNo, dto.Fax, dto.email, dto.Division, dto.District, dto.Upzila, dto.Thana, dto.CuProcFlag, dto.CuProcDesc, dto.CuStatus, param2, dto.CuPrevCuType, dto.CuPrevCuTypeName, dto.CuPrevCuNo, dto.CuNewCuType, dto.CuNewCuTypeName, dto.CuNewCuNo, dto.CuOldCuNo, dto.GLCashCode, dto.InputBy, dto.VerifyBy, dto.ApprovBy, param3, param4, param5, param6, dto.UserId, param7 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public static int UpdateInformation1(A2ZCUAPPLICATIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCUAPPLICATION set CuType='" + dto.CuType + "', CuTypeName='" + dto.CuTypeName + "', CuNo='" + dto.CreditUnionNo + "', CuProcFlag='" + dto.CuProcFlag + "', CuStatusDate='" + dto.CuStatusDate + "', InputByDate='" + dto.InputByDate + "', VerifyByDate='" + dto.VerifyByDate + "', ApprovByDate='" + dto.ApprovByDate + "' where  Id='" + dto.ID + "' ";

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

        public static int UpdateInformation2(A2ZCUAPPLICATIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCUAPPLICATION set CuProcFlag='" + dto.CuProcFlag + "',CuProcDesc='" + dto.CuProcDesc + "',VerifyBy='" + dto.VerifyBy + "',VerifyByDate='" + dto.VerifyByDate + "',ApprovBy='" + dto.ApprovBy + "',ApprovByDate='" + dto.ApprovByDate + "'  where  CuType='" + dto.CuType + "' AND CuNo='" + dto.CreditUnionNo + "'";

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

        public static int Update1(A2ZCUAPPLICATIONDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "UPDATE A2ZCUAPPLICATION set GLCashCode='" + dto.GLCashCode + "' where  Id='" + dto.ID + "' ";

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
