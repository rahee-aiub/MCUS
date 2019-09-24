using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.Inventory
{
    public class A2ZSTOCKDTO
   {
       #region Propertise
      
        public int STKItemCode { set; get; }
        public String STKItemName { set; get; }
        public int STKGroup { set; get; }
        public int STKSubGroup { set; get; }
        public int STKUnit { set; get; }
        public int STKUnitQty { set; get; }
        public Decimal STKUnitAvgCost { set; get; }
        public DateTime STKUnitAvgCostDate { get; set; }
        public int STKStatus { set; get; }
        public String STKStatusDesc { set; get; }
        public DateTime STKStatusDate { get; set; }
        public int STKReOrderLevel { set; get; }

       #endregion

       public static int InsertInformation(A2ZSTOCKDTO dto)
       {
           dto.STKItemName = (dto.STKItemName != null) ? dto.STKItemName.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZSTMST(STKItemCode,STKItemName,STKGroup,STKSubGroup,STKUnit,STKStatus,STKStatusDesc,STKStatusDate,STKReOrderLevel)values('" + dto.STKItemCode + "','" + dto.STKItemName + "','" + dto.STKGroup + "','" + dto.STKSubGroup + "','" + dto.STKUnit + "','" + dto.STKStatus + "','" + dto.STKStatusDesc + "','" + dto.STKStatusDate + "','" + dto.STKReOrderLevel + "')";
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

       //public static A2ZSTOCKDTO GetInformation(int GrpCode)
       //{
       //    DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSTMST WHERE GrpCode = " + GrpCode, "A2ZSTMCUS");


       //    var p = new A2ZSTOCKDTO();
       //    if (dt.Rows.Count > 0)
       //    {

       //        p.GrpCode = Converter.GetInteger(dt.Rows[0]["GrpCode"]);
             
       //        p.GrpDescription = Converter.GetString(dt.Rows[0]["GrpDescription"]);
       //        return p;
       //    }
       //    p.GrpCode = 0;

       //    return p;

       //}

       public static int UpdateInformation(A2ZSTOCKDTO dto)
       {
           dto.STKItemName = (dto.STKItemName != null) ? dto.STKItemName.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZSTMST set STKItemName='" + dto.STKItemName + "',STKGroup='" + dto.STKGroup + "',STKSubGroup='" + dto.STKSubGroup + "',STKUnit='" + dto.STKUnit + "',STKReOrderLevel='" + dto.STKReOrderLevel + "' Where STKItemCode ='" + dto.STKItemCode + "'";
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
