using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZTHANADTO
    {
        #region Propertise
        public int DivisionCode { set; get; }
        public int DistrictCode { set; get; }
        public int UpzilaCode { set; get; }
        public int ThanaCode { set; get; }
        public int DivisionOrgCode { set; get; }
        public int DistrictOrgCode { set; get; }
        public int UpzilaOrgCode { set; get; }
        public int ThanaOrgCode { set; get; }
        public String ThanaDescription { set; get; }
        #endregion

        public static int InsertInformation(A2ZTHANADTO dto)
        {
            dto.ThanaDescription = (dto.ThanaDescription != null) ? dto.ThanaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZTHANA(DiviCode,DistCode,UpzilaCode,ThanaCode,DiviOrgCode,DistOrgCode,UpzilaOrgCode,ThanaOrgCode,ThanaDescription)values('" + dto.DivisionCode + "','" + dto.DistrictCode + "','" + dto.UpzilaCode + "','" + dto.ThanaCode + "','" + dto.DivisionOrgCode + "','" + dto.DistrictOrgCode + "','" + dto.UpzilaOrgCode + "','" + dto.ThanaOrgCode + "','" + dto.ThanaDescription + "')";
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

        public static A2ZTHANADTO GetInformation(int DivCode, int DisCode, int UpzilaCode, int ThanaCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTHANA WHERE DiviCode = '" + DivCode + "' and DistCode='" + DisCode + "' and UpzilaCode='" + UpzilaCode + "' and ThanaCode='" + ThanaCode + "'", "A2ZHKMCUS");


            var p = new A2ZTHANADTO();
            if (dt.Rows.Count > 0)
            {

                p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
                p.DistrictCode = Converter.GetInteger(dt.Rows[0]["DistCode"]);
                p.UpzilaCode = Converter.GetInteger(dt.Rows[0]["UpzilaCode"]);
                p.ThanaCode = Converter.GetInteger(dt.Rows[0]["ThanaCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["DiviOrgCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["DistOrgCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["UpzilaOrgCode"]);
                p.ThanaOrgCode = Converter.GetInteger(dt.Rows[0]["ThanaOrgCode"]);
                p.ThanaDescription = Converter.GetString(dt.Rows[0]["ThanaDescription"]);

                return p;
            }
            p.DivisionCode = 0;
            p.DistrictCode = 0;
            p.UpzilaCode = 0;
            p.ThanaCode = 0;
            return p;

        }

        public static int UpdateInformation(A2ZTHANADTO dto)
        {
            dto.ThanaDescription = (dto.ThanaDescription != null) ? dto.ThanaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZTHANA set DiviCode='" + dto.DivisionCode + "',DistCode='" + dto.DistrictCode + "',UpzilaCode='" + dto.UpzilaCode + "',ThanaCode='" + dto.ThanaCode + "',DiviOrgCode='" + dto.DivisionOrgCode + "',DistOrgCode='" + dto.DistrictOrgCode + "',UpzilaOrgCode='" + dto.UpzilaOrgCode + "',ThanaOrgCode='" + dto.ThanaOrgCode + "',ThanaDescription='" + dto.ThanaDescription + "' where DiviCode ='" + dto.DivisionCode + "'and DistCode='" + dto.DistrictCode + "'and ThanaCode='" + dto.ThanaCode + "'";
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
