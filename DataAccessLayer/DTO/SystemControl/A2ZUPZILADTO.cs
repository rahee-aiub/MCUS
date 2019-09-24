using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.SystemControl
{
    public class A2ZUPZILADTO
    {

        #region Propertise
        public int DivisionCode { set; get; }
        public int DistrictCode { set; get; }
        public int UpzilaCode { set; get; }
        public int DivisionOrgCode { set; get; }
        public int DistrictOrgCode { set; get; }
        public int UpzilaOrgCode { set; get; }
        public String UpzilaDescription { set; get; }
        #endregion

        public static int InsertInformation(A2ZUPZILADTO dto)
        {
            dto.UpzilaDescription = (dto.UpzilaDescription != null) ? dto.UpzilaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZUPZILA(DiviCode,DistCode,UpzilaCode,DiviOrgCode,DistOrgCode,UpzilaOrgCode,UpzilaDescription)values('" + dto.DivisionCode + "','" + dto.DistrictCode + "','" + dto.UpzilaCode + "','" + dto.DivisionOrgCode + "','" + dto.DistrictOrgCode + "','" + dto.UpzilaOrgCode + "','" + dto.UpzilaDescription + "')";
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

        public static A2ZUPZILADTO GetInformation(int DivCode, int DisCode, int UpzilaCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZUPZILA WHERE DiviCode = '" + DivCode + "' and DistCode='" + DisCode + "'and UpzilaCode='" + UpzilaCode + "'", "A2ZHKMCUS");


            var p = new A2ZUPZILADTO();
            if (dt.Rows.Count > 0)
            {

                p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
                p.DistrictCode = Converter.GetInteger(dt.Rows[0]["DistCode"]);
                p.UpzilaCode = Converter.GetInteger(dt.Rows[0]["UpzilaCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["DiviOrgCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["DistOrgCode"]);
                p.UpzilaOrgCode = Converter.GetInteger(dt.Rows[0]["UpzilaOrgCode"]);
                p.UpzilaDescription = Converter.GetString(dt.Rows[0]["UpzilaDescription"]);

                return p;
            }
            p.DivisionCode = 0;
            p.DistrictCode = 0;
            p.UpzilaCode = 0;
            return p;

        }

        public static int UpdateInformation(A2ZUPZILADTO dto)
        {
            dto.UpzilaDescription = (dto.UpzilaDescription != null) ? dto.UpzilaDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZUPZILA set DiviCode='" + dto.DivisionCode + "',DistCode='" + dto.DistrictCode + "',UpzilaCode='" + dto.UpzilaCode + "',DiviOrgCode='" + dto.DivisionOrgCode + "',DistOrgCode='" + dto.DistrictOrgCode + "',UpzilaOrgCode='" + dto.UpzilaOrgCode + "',UpzilaDescription='" + dto.UpzilaDescription + "' where DiviCode ='" + dto.DivisionCode + "'and DistCode='" + dto.DistrictCode + "'and UpzilaCode='" + dto.UpzilaCode + "'";
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
