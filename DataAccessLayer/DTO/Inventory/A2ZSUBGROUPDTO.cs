using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.Inventory
{
   public class A2ZSUBGROUPDTO
    {
        #region Propertise
        public int GrpCode { set; get; }
        public int SubGrpCode { set; get; }
        public String SubGrpDescription { set; get; }
        public int LastRec { set; get; }

        #endregion

        public static int InsertInformation(A2ZSUBGROUPDTO dto)
        {
            dto.SubGrpDescription = (dto.SubGrpDescription != null) ? dto.SubGrpDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZSUBGROUP(GrpCode,SubGrpCode,SubGrpDescription)values('" + dto.GrpCode + "','" + dto.SubGrpCode + "','" + dto.SubGrpDescription + "')";
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

        public static A2ZSUBGROUPDTO GetInformation(int GrpCode, int SubGrpCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSUBGROUP WHERE GrpCode = '" + GrpCode + "' and SubGrpCode='" + SubGrpCode + "'", "A2ZSTMCUS");


            var p = new A2ZSUBGROUPDTO();
            if (dt.Rows.Count > 0)
            {

                p.GrpCode = Converter.GetInteger(dt.Rows[0]["GrpCode"]);
                p.SubGrpCode = Converter.GetInteger(dt.Rows[0]["SubGrpCode"]);
                p.LastRec = Converter.GetInteger(dt.Rows[0]["LastRec"]);               
                p.SubGrpDescription = Converter.GetString(dt.Rows[0]["SubGrpDescription"]);
                return p;
            }
            p.GrpCode = 0;
            p.SubGrpCode = 0;
            return p;

        }

        

        public static int UpdateInformation(A2ZSUBGROUPDTO dto)
        {
            dto.SubGrpDescription = (dto.SubGrpDescription != null) ? dto.SubGrpDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSUBGROUP set GrpCode='" + dto.GrpCode + "',SubGrpCode='" + dto.SubGrpCode + "',SubGrpDescription='" + dto.SubGrpDescription + "' where GrpCode ='" + dto.GrpCode + "'and SubGrpCode='" + dto.SubGrpCode + "'";
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

        public static int UpdateLastRec(A2ZSUBGROUPDTO dto)
        {
            
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSUBGROUP SET LastRec='" + dto.LastRec + "' where GrpCode ='" + dto.GrpCode + "'and SubGrpCode='" + dto.SubGrpCode + "'";
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
