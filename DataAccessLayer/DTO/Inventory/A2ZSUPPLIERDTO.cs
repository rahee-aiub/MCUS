using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataAccessLayer.DTO.CustomerServices;

namespace DataAccessLayer.DTO.Inventory
{
   public class A2ZSUPPLIERDTO
   {
       #region Propertise
       public int SuppCode { set; get; }
       public String SuppName { set; get; }
       public String SuppAddL1 { set; get; }
       public String SuppAddL2 { set; get; }
       public String SuppAddL3 { set; get; }
       public String SuppTel { set; get; }
       public String SuppMobile { set; get; }
       public String SuppFax { set; get; }
       public String SuppEmail { set; get; }
       public DateTime SuppStatDate { set; get; }
       public string NullSuppStatDate { set; get; }
       public decimal SuppBalance { set; get; }
       public decimal SuppVATAmt { set; get; }
       public decimal SuppTAXAmt { set; get; }
       
       #endregion

       public static int InsertInformation(A2ZSUPPLIERDTO dto)
       {

           SqlDateTime sqldatenull;
           sqldatenull = SqlDateTime.Null;

           SqlParameter param1 = new SqlParameter("@SuppStatDate", DBNull.Value);


           if (dto.NullSuppStatDate == "")
           {

               param1 = new SqlParameter("@SuppStatDate", DBNull.Value);
           }
           else
           {
               param1 = new SqlParameter("@SuppStatDate", dto.SuppStatDate);
           }

           dto.SuppName = (dto.SuppName != null) ? dto.SuppName.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZSUPPLIER(SuppCode,SuppName,SuppAddL1,SuppAddL2,SuppAddL3,SuppTel,SuppMobile,SuppFax,SuppEmail,SuppStatDate,SuppStatus,SuppStatDesc)values('" + dto.SuppCode + "','" + dto.SuppName + "','" + dto.SuppAddL1 + "','" + dto.SuppAddL2 + "','" + dto.SuppAddL3 + "','" + dto.SuppTel + "','" + dto.SuppMobile + "','" + dto.SuppFax + "','" + dto.SuppEmail + "','" + param1.Value + "', '1', 'Active')";
           rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));

           if (rowEffect == 0)
           {
               return 0;
           }
           else
           {
               return 1;
           }
       }

       public static A2ZSUPPLIERDTO GetInformation(int SuppCode)
       {

           A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
           DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
           string date1 = dt2.ToString("dd/MM/yyyy");

           var prm1 = new object[3];
           prm1[0] = SuppCode;
           prm1[1] = Converter.GetDateToYYYYMMDD(date1);
           prm1[2] = 0;

           DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_STGenerateSingleLedgerBalance", prm1, "A2ZSTMCUS");



           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSUPPLIER WHERE SuppCode = " + SuppCode, "A2ZSTMCUS");


           var p = new A2ZSUPPLIERDTO();
           if (dt.Rows.Count > 0)
           {

               p.SuppCode = Converter.GetInteger(dt.Rows[0]["SuppCode"]);
               p.SuppName = Converter.GetString(dt.Rows[0]["SuppName"]);
               p.SuppAddL1 = Converter.GetString(dt.Rows[0]["SuppAddL1"]);
               p.SuppAddL2 = Converter.GetString(dt.Rows[0]["SuppAddL2"]);
               p.SuppAddL3 = Converter.GetString(dt.Rows[0]["SuppAddL3"]);
               p.SuppTel = Converter.GetString(dt.Rows[0]["SuppTel"]);
               p.SuppMobile = Converter.GetString(dt.Rows[0]["SuppMobile"]);
               p.SuppFax = Converter.GetString(dt.Rows[0]["SuppFax"]);
               p.SuppEmail = Converter.GetString(dt.Rows[0]["SuppEmail"]);

               p.SuppBalance = Converter.GetDecimal(dt.Rows[0]["SuppBalance"]);
               p.SuppVATAmt = Converter.GetDecimal(dt.Rows[0]["SuppVATAmt"]);
               p.SuppTAXAmt = Converter.GetDecimal(dt.Rows[0]["SuppTAXAmt"]);
               return p;
           }
           p.SuppCode = 0;

           return p;

       }

       public static int UpdateInformation(A2ZSUPPLIERDTO dto)
       {
           dto.SuppName = (dto.SuppName != null) ? dto.SuppName.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZSUPPLIER set SuppCode='" + dto.SuppCode + "',SuppName='" + dto.SuppName + "',SuppAddL1='" + dto.SuppAddL1 + "',SuppAddL2='" + dto.SuppAddL2 + "',SuppAddL3='" + dto.SuppAddL3 + "',SuppTel='" + dto.SuppTel + "',SuppMobile='" + dto.SuppMobile + "',SuppFax='" + dto.SuppFax + "',SuppEmail='" + dto.SuppEmail + "' where SuppCode ='" + dto.SuppCode + "'";
           rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZSTMCUS"));
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
