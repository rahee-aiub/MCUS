using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZLOCATIONDTO
    {
        #region Prpperties

        public Int16 ID { set; get; }
        public Int16 LocationCode { set; get; }
        public String LocationName { set; get; }
        public Int16 LocationFlag { set; get; }
        public Int16 LocationType { set; get; }
        public Int16 LocationStatus { set; get; }
        public Int16 UserId { set; get; }
        public DateTime CreateDate { set; get; }

        #endregion

        public static A2ZLOCATIONDTO GetLocationInformation(Int16 locationCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(
                "SELECT * FROM A2ZLOCATION WHERE LocationCode = " + locationCode, "A2ZHRMCUS");

            var p = new A2ZLOCATIONDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.LocationCode = Converter.GetSmallInteger(dt.Rows[0]["LocationCode"]);
                p.LocationName = Converter.GetString(dt.Rows[0]["LocationName"]);

                p.LocationFlag = Converter.GetSmallInteger(dt.Rows[0]["LocationFlag"]);
                p.LocationType = Converter.GetSmallInteger(dt.Rows[0]["LocationType"]);
                p.LocationStatus = Converter.GetSmallInteger(dt.Rows[0]["LocationStatus"]);
                p.UserId = Converter.GetSmallInteger(dt.Rows[0]["UserId"]);
                p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);


                return p;
            }

            p.LocationCode = 0;

            return p;            
        }

        public static DropDownList GetDropDownList(DropDownList ddlLocation)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT LocationCode,LocationName FROM A2ZLOCATION", ddlLocation, "A2ZHRMCUS");
        }

        public static int GetLastSerialNo()
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(LocationCode) FROM A2ZLOCATION", "A2ZHRMCUS");

            if (dt.Rows.Count > 0)
            {
                return (Converter.GetSmallInteger(dt.Rows[0].ItemArray[0]) + 1);
            }

            return 1;

        }

        public static int InsertLocationInformation(A2ZLOCATIONDTO dto)
        {
            int nSrlNo = GetLastSerialNo();
            int rowEffect = 0;


            string strQuery = "INSERT INTO A2ZLOCATION(LocationCode,LocationName,UserId)VALUES('" + nSrlNo + "','" + dto.LocationName + "','" + dto.UserId + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            return nSrlNo;
        }

        public static int UpdateLocationInformation(A2ZLOCATIONDTO dto)
        {
            string strQuery = "UPDATE A2ZLOCATION SET LocationName ='" + dto.LocationName + "',UserId='" + dto.UserId + "' WHERE LocationCode='" + dto.LocationCode + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZERPHR"));

        }

    }
}
