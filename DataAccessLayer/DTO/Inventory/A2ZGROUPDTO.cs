using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.Inventory
{
   public class A2ZGROUPDTO
   {
       #region Propertise
       public int GrpCode { set; get; }
       public String GrpDescription { set; get; }
       #endregion

       public static int InsertInformation(A2ZGROUPDTO dto)
       {
           dto.GrpDescription = (dto.GrpDescription != null) ? dto.GrpDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZGROUP(GrpCode,GrpDescription)values('" + dto.GrpCode + "','" + dto.GrpDescription + "')";
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

       public static A2ZGROUPDTO GetInformation(int GrpCode)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZGROUP WHERE GrpCode = " + GrpCode, "A2ZSTMCUS");


           var p = new A2ZGROUPDTO();
           if (dt.Rows.Count > 0)
           {

               p.GrpCode = Converter.GetInteger(dt.Rows[0]["GrpCode"]);
             
               p.GrpDescription = Converter.GetString(dt.Rows[0]["GrpDescription"]);
               return p;
           }
           p.GrpCode = 0;

           return p;

       }

       public static int UpdateInformation(A2ZGROUPDTO dto)
       {
           dto.GrpDescription = (dto.GrpDescription != null) ? dto.GrpDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZGROUP set GrpCode='" + dto.GrpCode + "',GrpDescription='" + dto.GrpDescription + "' where GrpCode ='" + dto.GrpCode + "'";
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
