using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;
namespace DataAccessLayer.DTO.SystemControl
{
  public  class A2ZDESIGNATIONDTO
    {
        #region Propertise

        public int DesignationCode { set; get; }
        public String DesignationDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZDESIGNATIONDTO dto)
        {
            dto.DesignationDescription = (dto.DesignationDescription != null) ? dto.DesignationDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZDESIGNATION(DesigCode,DesigDescription)values('" + dto.DesignationCode + "','" + dto.DesignationDescription + "')";
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

        public static A2ZDESIGNATIONDTO GetInformation(Int16 Designcode)
        {
            var prm = new object[1];

            prm[0] = Designcode;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetInfoDesignation", prm, "A2ZHRMCUS");
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDESIGNATION WHERE DesigCode = " + Designcode, "A2ZCSMCUS");


            var p = new A2ZDESIGNATIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.DesignationCode = Converter.GetSmallInteger(dt.Rows[0]["DesigCode"]);
                p.DesignationDescription = Converter.GetString(dt.Rows[0]["DesigDescription"]);
                return p;
            }
            p.DesignationCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZDESIGNATIONDTO dto)
        {
            dto.DesignationDescription = (dto.DesignationDescription != null) ? dto.DesignationDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZDESIGNATION set DesigCode='" + dto.DesignationCode + "',DesigDescription='" + dto.DesignationDescription + "' where DesigCode='" + dto.DesignationCode + "'";
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
