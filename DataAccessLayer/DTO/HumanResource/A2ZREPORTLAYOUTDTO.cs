using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZREPORTLAYOUTDTO
    {
        public int ID { set; get; }
        public int RepColumn { set; get; }
        public String RepColumnName { set; get; }
        public Int16 RepColumnFlag { set; get; }
        public int RepColumnCode { set; get; }
        public int Code { set; get; }
        public DateTime RepDate { set; get; }
        public Int16 Status { set; get; }


        public static A2ZREPORTLAYOUTDTO GetInformation(int RepColumn, DateTime RepDate)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(
                "SELECT * FROM A2ZREPORTLAYOUT WHERE RepColumn = '" + RepColumn+"' AND RepDate='"+RepDate+"'", "A2ZHRMCUS");

            var p = new A2ZREPORTLAYOUTDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.RepColumn = Converter.GetInteger(dt.Rows[0]["RepColumn"]);
                p.RepColumnName = Converter.GetString(dt.Rows[0]["RepColumnName"]);
                p.RepColumnFlag = Converter.GetSmallInteger(dt.Rows[0]["RepColumnFlag"]);
                p.Status = Converter.GetSmallInteger(dt.Rows[0]["Status"]);
                p.RepColumnCode = Converter.GetInteger(dt.Rows[0]["RepColumnCode"]);


                return p;
            }

            p.RepColumn = 0;

            return p;
        }

        public static int InsertInformation(A2ZREPORTLAYOUTDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "INSERT INTO A2ZREPORTLAYOUT(RepDate,RepColumn,RepColumnName,RepColumnFlag,RepColumnCode,Status)VALUES('" + dto.RepDate + "','" + dto.RepColumn + "','" + dto.RepColumnName + "','" + dto.RepColumnFlag + "','" + dto.RepColumnCode + "','" + dto.Status + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect== 0) 
            {
                return 0;
            }
            return 1;
        }

         
       

    }

}