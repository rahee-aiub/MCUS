using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;
using System.Web.UI.WebControls;

namespace DataAccessLayer.DTO
{
    public class A2ZDEDUCTIONDTO
    {
        #region Didaction
        public int Id { set; get; }
        public int Code { set; get; }
        public String Description { set; get; }
        public Boolean Status { set; get; }
        public int RepColumn { set; get; }

        #endregion

        public static A2ZDEDUCTIONDTO GetInformation(int DidCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDEDUCTION WHERE Code = " + DidCode, "A2ZHRMCUS");


            var p = new A2ZDEDUCTIONDTO();

            if (dt.Rows.Count > 0)
            {
                p.Id = Converter.GetInteger(dt.Rows[0]["ID"]);
                p.Code = Converter.GetInteger(dt.Rows[0]["Code"]);
                p.Description = Converter.GetString(dt.Rows[0]["Description"]);
                p.Status = Converter.GetBoolean(dt.Rows[0]["Status"]);


                return p;
            }

            p.Code = 0;

            return p;
        }

        public static int InsertInformation(A2ZDEDUCTIONDTO objIds)
        {
            int rowwEffect = 0;

            string strQuery = "INSERT INTO A2ZDEDUCTION(Code,Description,Status) VALUES ('" + objIds.Code + "','" + objIds.Description + "','" + objIds.Status + "')";


            rowwEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            return rowwEffect;

        }

        public static DropDownList GetDropDownList(DropDownList ddlCode)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT Code,Description,Status FROM A2ZDEDUCTION", ddlCode, "A2ZHRMCUS");
        }

        public static int UpdateInformation(A2ZDEDUCTIONDTO dto)
        {
            string strQuery = "UPDATE A2ZDEDUCTION SET Description = '" + dto.Description + "', Status='" + dto.Status + "' WHERE Code ='" + dto.Code + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

        }

        public static int UpdateRepColumn(A2ZDEDUCTIONDTO dto)
        {
            string strQuery = "UPDATE A2ZDEDUCTION SET RepColumn = '" + dto.RepColumn + "' WHERE Code ='" + dto.Code + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

        }


       

    }
}
