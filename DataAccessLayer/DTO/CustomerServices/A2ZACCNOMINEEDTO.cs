using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCNOMINEEDTO
   {
       #region Propertise
       public int ID { set; get; }
       public Int16 CuType { set; get; }
       public int CUnionNo { set; get; }
       public int MemberNo { set; get; }
       public Int16 AccType { set; get; }
       public Int64 AccountNo { set; get; }
       public int NomineeNo { set; get; }
       public string NomineeName { set; get; }
       public string NomAddress1 { set; get; }
       public string NomAddress2 { set; get; }
       public string NomAddress3 { set; get; }
       public string NomTelephoneNo { set; get; }
       public string NomMobileNo { set; get; }
       public string NomEmail { set; get; }
       public int NomDivision { set; get; }
       public int NomDistrict { set; get; }
       public int NomUpzila { set; get; }
       public int NomThana { set; get; }
       public string NomRelation { set; get; }
       public int NomSharePercentage { set; get; }
       public int UserId { set; get; }
       public Int16 Record { set; get; }
       #endregion


       public static int InsertInformation(A2ZACCNOMINEEDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZACCNOM(CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,
                                 NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer) values('" + dto.CuType + "','" + dto.CUnionNo + "','" + dto.MemberNo + "','" + 
                                 dto.AccType + "','" + dto.AccountNo + "','" + dto.NomineeNo + "','"+ dto.NomineeName+"','"+ dto.NomAddress1+"','"+ 
                                 dto.NomAddress2+"','"+ dto.NomAddress3+"','"+ dto.NomTelephoneNo+"','"+ dto.NomMobileNo+"','"+ dto.NomEmail+"','"+
                                 dto.NomDivision + "','" + dto.NomDistrict + "','" + dto.NomUpzila + "','" + dto.NomThana + "','" + dto.NomRelation + "','" + dto.NomSharePercentage + "')";
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

       public static A2ZACCNOMINEEDTO GetInformation(int NomNo,Int16 acctype,int MemNo,Int64 AccNo,Int16 CuType,int CuNo)
       {
           var prm = new object[6];

           prm[0] = NomNo;
           prm[1] = acctype;
           prm[2] = MemNo;
           prm[3] = AccNo;
           prm[4] = CuType;
           prm[5] = CuNo;



           DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccNominee", prm, "A2ZCSMCUS");
           
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCNOM WHERE NomNo = '" + NomNo + "' and AccType='" +
           //    acctype + "' and MemNo='" + MemNo + "' and AccNo='" + AccNo + "' and CuType='" + CuType + "' and CuNo='" + CuNo + "'", "A2ZCSMCUS");


           var p = new A2ZACCNOMINEEDTO();
           if (dt.Rows.Count > 0)
           {
               p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
               p.CUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
               p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
               p.AccType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
               p.AccountNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
               p.NomineeNo = Converter.GetInteger(dt.Rows[0]["NomNo"]);
               p.NomineeName = Converter.GetString(dt.Rows[0]["NomName"]);
               p.NomAddress1 = Converter.GetString(dt.Rows[0]["NomAdd1"]);
               p.NomAddress2 = Converter.GetString(dt.Rows[0]["NomAdd2"]);
               p.NomAddress3 = Converter.GetString(dt.Rows[0]["NomAdd3"]);
               p.NomTelephoneNo = Converter.GetString(dt.Rows[0]["NomTel"]);
               p.NomMobileNo = Converter.GetString(dt.Rows[0]["NomMobile"]);
               p.NomEmail = Converter.GetString(dt.Rows[0]["NomEmail"]);
               p.NomDivision = Converter.GetInteger(dt.Rows[0]["NomDivi"]);
               p.NomDistrict = Converter.GetInteger(dt.Rows[0]["NomDist"]);
               p.NomUpzila = Converter.GetInteger(dt.Rows[0]["NomUpzila"]);
               p.NomThana = Converter.GetInteger(dt.Rows[0]["NomThana"]);
               p.NomRelation = Converter.GetString(dt.Rows[0]["NomRela"]);
               p.NomSharePercentage = Converter.GetInteger(dt.Rows[0]["NomSharePer"]);
               p.Record = 1;
               return p;
           }
           p.NomineeNo = 0;
           p.Record = 0;

           return p;

       }

       public static int UpdateInformation(A2ZACCNOMINEEDTO dto)
       {

           int rowEffect = 0;
           string strQuery = "UPDATE A2ZACCNOM set NomNo='" + dto.NomineeNo + "', NomName='" + dto.NomineeName + 
               "', NomAdd1='" + dto.NomAddress1 + "', NomAdd2='" + dto.NomAddress2 + "',NomAdd3='" + dto.NomAddress3 + 
               "',NomTel='" + dto.NomTelephoneNo + "',NomMobile='" + dto.NomMobileNo + "',NomEmail='" + dto.NomEmail + 
               "',NomDivi='" + dto.NomDivision + "',NomDist='" + dto.NomDistrict + "',NomUpzila='" + dto.NomUpzila +
               "',NomThana='" + dto.NomThana +
               "',NomRela='" + dto.NomRelation + "',NomSharePer='" + dto.NomSharePercentage + "'where NomNo='" + dto.NomineeNo + 
               "'and AccType='" + dto.AccType + "' and MemNo='" + dto.MemberNo + "' and AccNo='" + dto.AccountNo + 
               "' and CuType='" + dto.CuType + "' and CuNo='" + dto.CUnionNo + "'";
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


       public static int WFInsertInformation(A2ZACCNOMINEEDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into WFCSA2ZACCNOM(CuType,CuNo,MemNo,AccType,AccNo,NomNo,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,
                                 NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer,UserId) values('" + dto.CuType + "','" + dto.CUnionNo + "','" + dto.MemberNo + "','" +
                                 dto.AccType + "','" + dto.AccountNo + "','" + dto.NomineeNo + "','" + dto.NomineeName + "','" + dto.NomAddress1 + "','" +
                                 dto.NomAddress2 + "','" + dto.NomAddress3 + "','" + dto.NomTelephoneNo + "','" + dto.NomMobileNo + "','" + dto.NomEmail + "','" +
                                 dto.NomDivision + "','" + dto.NomDistrict + "','" + dto.NomUpzila + "','" + dto.NomThana + "','" + dto.NomRelation + "','" + dto.NomSharePercentage + "','" + dto.UserId + "')";
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

       public static A2ZACCNOMINEEDTO WFGetInformation(int ID)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM WFCSA2ZACCNOM WHERE Id = '" + ID + "'", "A2ZCSMCUS");


           var p = new A2ZACCNOMINEEDTO();
           if (dt.Rows.Count > 0)
           {
               p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
               p.CUnionNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
               p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
               p.AccType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
               p.AccountNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
               p.NomineeNo = Converter.GetInteger(dt.Rows[0]["NomNo"]);
               p.NomineeName = Converter.GetString(dt.Rows[0]["NomName"]);
               p.NomAddress1 = Converter.GetString(dt.Rows[0]["NomAdd1"]);
               p.NomAddress2 = Converter.GetString(dt.Rows[0]["NomAdd2"]);
               p.NomAddress3 = Converter.GetString(dt.Rows[0]["NomAdd3"]);
               p.NomTelephoneNo = Converter.GetString(dt.Rows[0]["NomTel"]);
               p.NomMobileNo = Converter.GetString(dt.Rows[0]["NomMobile"]);
               p.NomEmail = Converter.GetString(dt.Rows[0]["NomEmail"]);
               p.NomDivision = Converter.GetInteger(dt.Rows[0]["NomDivi"]);
               p.NomDistrict = Converter.GetInteger(dt.Rows[0]["NomDist"]);
               p.NomUpzila = Converter.GetInteger(dt.Rows[0]["NomUpzila"]);
               p.NomThana = Converter.GetInteger(dt.Rows[0]["NomThana"]);
               p.NomRelation = Converter.GetString(dt.Rows[0]["NomRela"]);
               p.NomSharePercentage = Converter.GetInteger(dt.Rows[0]["NomSharePer"]);
               p.UserId = Converter.GetInteger(dt.Rows[0]["UserId"]);
               p.Record = 1;
               return p;
           }
           p.NomineeNo = 0;
           p.Record = 0;

           return p;

       }

       public static int WFUpdateInformation(A2ZACCNOMINEEDTO dto)
       {

           int rowEffect = 0;
           string strQuery = "UPDATE WFCSA2ZACCNOM set NomNo='" + dto.NomineeNo + "', NomName='" + dto.NomineeName +
               "', NomAdd1='" + dto.NomAddress1 + "', NomAdd2='" + dto.NomAddress2 + "',NomAdd3='" + dto.NomAddress3 +
               "',NomTel='" + dto.NomTelephoneNo + "',NomMobile='" + dto.NomMobileNo + "',NomEmail='" + dto.NomEmail +
               "',NomDivi='" + dto.NomDivision + "',NomDist='" + dto.NomDistrict + "',NomUpzila='" + dto.NomUpzila +
               "',NomThana='" + dto.NomThana +
               "',NomRela='" + dto.NomRelation + "',NomSharePer='" + dto.NomSharePercentage + "'where Id='" + dto.ID + "'";
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
