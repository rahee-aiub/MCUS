using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.Inventory
{
   public class A2ZUNITDTO
   {
       #region Propertise
       public int UnitNo { set; get; }
       public String UnitDesc { set; get; }
       #endregion

       public static int InsertInformation(A2ZUNITDTO dto)
       {
           dto.UnitDesc = (dto.UnitDesc != null) ? dto.UnitDesc.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZUNITCODE(UnitNo,UnitDesc)values('" + dto.UnitNo + "','" + dto.UnitDesc + "')";
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

       public static A2ZUNITDTO GetInformation(int UnitNo)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZUNITCODE WHERE UnitNo = " + UnitNo, "A2ZSTMCUS");

           var p = new A2ZUNITDTO();
           if (dt.Rows.Count > 0)
           {

               p.UnitNo = Converter.GetInteger(dt.Rows[0]["UnitNo"]);

               p.UnitDesc = Converter.GetString(dt.Rows[0]["UnitDesc"]);
               return p;
           }
           p.UnitNo = 0;

           return p;

       }

       public static int UpdateInformation(A2ZUNITDTO dto)
       {
           dto.UnitDesc = (dto.UnitDesc != null) ? dto.UnitDesc.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZUNITCODE set UnitNo='" + dto.UnitNo + "',UnitDesc='" + dto.UnitDesc + "' where UnitNo ='" + dto.UnitNo + "'";
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
