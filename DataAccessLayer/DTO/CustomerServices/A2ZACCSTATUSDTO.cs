using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCSTATUSDTO
    {

        #region Propertise

       public int AccStatusCode { set; get; }
       public String AccStatusDescription { set; get; }
        #endregion


       public static int InsertInformation(A2ZACCSTATUSDTO dto)
        {
            dto.AccStatusDescription = (dto.AccStatusDescription != null) ? dto.AccStatusDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZACCSTATUS(AccStatusCode,AccStatusDescription)values('" + dto.AccStatusCode + "','" + dto.AccStatusDescription + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

       public static A2ZACCSTATUSDTO GetInformation(Int16 Statuscode)
        {
            var prm = new object[1];

            prm[0] = Statuscode;
           


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccStatus", prm, "A2ZCSMCUS"); 
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCSTATUS WHERE AccStatusCode = " + Statuscode, "A2ZCSMCUS");


            var p = new A2ZACCSTATUSDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccStatusCode = Converter.GetSmallInteger(dt.Rows[0]["AccStatusCode"]);
                p.AccStatusDescription = Converter.GetString(dt.Rows[0]["AccStatusDescription"]);
                return p;
            }
            p.AccStatusCode = 0;

            return p;

        }
       public static int UpdateInformation(A2ZACCSTATUSDTO dto)
        {
            dto.AccStatusDescription = (dto.AccStatusDescription != null) ? dto.AccStatusDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCSTATUS set AccStatusCode='" + dto.AccStatusCode + "',AccStatusDescription='" + dto.AccStatusDescription + "' where AccStatusCode='" + dto.AccStatusCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
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
