using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.SystemControl
{
    public class A2ZSECTIONDTO
    {
        #region Propertise

        public Int16 SectionCode { set; get; }
        public String SectionName { set; get; }
        #endregion


        public static int InsertInformation(A2ZSECTIONDTO dto)
        {
            dto.SectionName = (dto.SectionName != null) ? dto.SectionName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZSECTION(SectionCode,SectionName)values('" + dto.SectionCode + "','" + dto.SectionName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZSECTIONDTO GetInformation(Int16 Seccode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSECTION WHERE   SectionCode = " + Seccode, "A2ZHKMCUS");


            var p = new A2ZSECTIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.SectionCode = Converter.GetSmallInteger(dt.Rows[0]["SectionCode"]);
                p.SectionName = Converter.GetString(dt.Rows[0]["SectionName"]);
                return p;
            }
            p.SectionCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZSECTIONDTO dto)
        {
            dto.SectionName = (dto.SectionName != null) ? dto.SectionName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSECTION set SectionCode='" + dto.SectionCode + "',SectionName='" + dto.SectionName + "' where SectionCode='" + dto.SectionCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
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
