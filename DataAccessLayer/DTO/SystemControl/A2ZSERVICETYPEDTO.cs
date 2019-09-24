using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.DTO.HumanResource;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZSERVICETYPEDTO
    {
        #region Propertise

       public Int16 ServiceType { set; get; }
       public String ServiceName { set; get; }
        #endregion


       public static int InsertInformation(A2ZSERVICETYPEDTO dto)
        {
            dto.ServiceName = (dto.ServiceName != null) ? dto.ServiceName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZSERVICETYPE(ServiceType,ServiceName)values('" + dto.ServiceType + "','" + dto.ServiceName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

       public static A2ZSERVICETYPEDTO GetInformation(Int16 STypecode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSERVICETYPE WHERE ServiceType = " + STypecode, "A2ZHRMCUS");


            var p = new A2ZSERVICETYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.ServiceType = Converter.GetSmallInteger(dt.Rows[0]["ServiceType"]);
                p.ServiceName = Converter.GetString(dt.Rows[0]["ServiceName"]);
                return p;
            }
            p.ServiceType = 0;

            return p;

        }
       public static int UpdateInformation(A2ZSERVICETYPEDTO dto)
        {
            dto.ServiceName = (dto.ServiceName != null) ? dto.ServiceName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSERVICETYPE set ServiceType='" + dto.ServiceType + "',ServiceName='" + dto.ServiceName + "' where ServiceType='" + dto.ServiceType + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
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
