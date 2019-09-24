using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZSERVICETYPEDTO
    {
        public Int16 ID { set; get; }
        public Int16 ServiceType { set; get; }
        public String ServiceName { set; get; }
        public Int16 ServiceFlag { set; get; }
        public Int16 ServiceTypeType { set; get; }
        public Int16 ServiceStatus { set; get; }
        public Int16 UserId { set; get; }
        public DateTime CreateDate { set; get; }


        public static A2ZSERVICETYPEDTO GetServiceInformation(Int16 serviceType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(
                "SELECT * FROM A2ZSERVICETYPE WHERE ServiceType = " + serviceType, "A2ZHRMCUS");

            var p = new A2ZSERVICETYPEDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.ServiceType = Converter.GetSmallInteger(dt.Rows[0]["ServiceType"]);
                p.ServiceName = Converter.GetString(dt.Rows[0]["ServiceName"]);

                p.ServiceFlag = Converter.GetSmallInteger(dt.Rows[0]["ServiceFlag"]);
                p.ServiceTypeType = Converter.GetSmallInteger(dt.Rows[0]["ServiceTypeType"]);
                p.ServiceStatus = Converter.GetSmallInteger(dt.Rows[0]["ServiceStatus"]);

                return p;
            }

            p.ServiceType = 0;

            return p;
        }

        public static DropDownList GetDropDownList(DropDownList ddlServiceType)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT ServiceType,ServiceName FROM A2ZSERVICETYPE", ddlServiceType, "A2ZHRMCUS");
        }

        public static int GetLastSerialNo()
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(ServiceType) FROM A2ZSERVICETYPE", "A2ZHRMCUS");

            if (dt.Rows.Count > 0)
            {
                return (Converter.GetSmallInteger(dt.Rows[0].ItemArray[0]) + 1);
            }

            return 1;

        }


        public static int InsertServiceInformation(A2ZSERVICETYPEDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "INSERT INTO A2ZSERVICETYPE(ServiceType,ServiceName,UserId)VALUES('" + dto.ServiceType + "','" + dto.ServiceName + "','" + dto.UserId + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect== 0) 
            {
                return 0;
            }
            return 1;
        }

        public static int UpdateServiceInformation(A2ZSERVICETYPEDTO dto)
        {
            string strQuery = "UPDATE A2ZSERVICETYPE SET ServiceName ='" + dto.ServiceName + "',UserId='" + dto.UserId + "' WHERE ServiceType='" + dto.ServiceType + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
            
        }

    

       

    }

}