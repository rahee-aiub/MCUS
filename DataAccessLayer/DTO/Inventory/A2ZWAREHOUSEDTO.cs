using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.Inventory
{
   public class A2ZWAREHOUSEDTO
   {
       #region Propertise
       public int WaseCode { set; get; }
       public String WaseDescription { set; get; }
       #endregion

       public static int InsertInformation(A2ZWAREHOUSEDTO dto)
       {
           dto.WaseDescription = (dto.WaseDescription != null) ? dto.WaseDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZWAREHOUSE(WaseCode,WaseDescription)values('" + dto.WaseCode + "','" + dto.WaseDescription + "')";
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

       public static A2ZWAREHOUSEDTO GetInformation(int WaseCode)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZWAREHOUSE WHERE WaseCode = " + WaseCode, "A2ZSTMCUS");


           var p = new A2ZWAREHOUSEDTO();
           if (dt.Rows.Count > 0)
           {

               p.WaseCode = Converter.GetInteger(dt.Rows[0]["WaseCode"]);
            
               p.WaseDescription = Converter.GetString(dt.Rows[0]["WaseDescription"]);
               return p;
           }
           p.WaseCode = 0;

           return p;

       }

       public static int UpdateInformation(A2ZWAREHOUSEDTO dto)
       {
           dto.WaseDescription = (dto.WaseDescription != null) ? dto.WaseDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZWAREHOUSE set WaseCode='" + dto.WaseCode + "',WaseDescription='" + dto.WaseDescription + "' where WaseCode ='" + dto.WaseCode + "'";
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
