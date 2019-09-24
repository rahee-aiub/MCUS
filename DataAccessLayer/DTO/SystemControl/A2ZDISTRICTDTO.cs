using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZDISTRICTDTO
    {
        #region Propertise
        public int DivisionCode { set; get; }
        public int DistrictCode { set; get; }
        public int DivisionOrgCode { set; get; }
        public int DistrictOrgCode { set; get; }
        public String DistrictDescription { set; get; }
        #endregion

        public static int InsertInformation(A2ZDISTRICTDTO dto)
        {
            dto.DistrictDescription = (dto.DistrictDescription != null) ? dto.DistrictDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZDISTRICT(DiviCode,DistCode,DiviOrgCode,DistOrgCode,DistDescription)values('" + dto.DivisionCode + "','" + dto.DistrictCode + "','" + dto.DivisionOrgCode + "','" + dto.DistrictOrgCode + "','" + dto.DistrictDescription + "')";
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

        public static A2ZDISTRICTDTO GetInformation(int DivCode,int DisCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDISTRICT WHERE DiviCode = '" + DivCode + "' and DistCode='" + DisCode + "'", "A2ZHKMCUS");


            var p = new A2ZDISTRICTDTO();
            if (dt.Rows.Count > 0)
            {

                p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
                p.DistrictCode = Converter.GetInteger(dt.Rows[0]["DistCode"]);
                p.DivisionOrgCode = Converter.GetInteger(dt.Rows[0]["DiviOrgCode"]);
                p.DistrictOrgCode = Converter.GetInteger(dt.Rows[0]["DistOrgCode"]);
                p.DistrictDescription = Converter.GetString(dt.Rows[0]["DistDescription"]);
                return p;
            }
            p.DivisionCode = 0;
            p.DistrictCode = 0;
            return p;

        }

        public static A2ZDISTRICTDTO GetInfo(int DisCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDISTRICT WHERE DistOrgCode = '" + DisCode + "'", "A2ZHKMCUS");


            var p = new A2ZDISTRICTDTO();
            if (dt.Rows.Count > 0)
            {

                p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
                p.DistrictCode = Converter.GetInteger(dt.Rows[0]["DistCode"]);
                p.DivisionOrgCode = Converter.GetInteger(dt.Rows[0]["DiviOrgCode"]);
                p.DistrictOrgCode = Converter.GetInteger(dt.Rows[0]["DistOrgCode"]);
                p.DistrictDescription = Converter.GetString(dt.Rows[0]["DistDescription"]);
                return p;
            }
            p.DivisionCode = 0;
            p.DistrictCode = 0;
            return p;

        }

        public static int UpdateInformation(A2ZDISTRICTDTO dto)
        {
            dto.DistrictDescription = (dto.DistrictDescription != null) ? dto.DistrictDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZDISTRICT set DiviCode='" + dto.DivisionCode + "',DistCode='" + dto.DistrictCode + "',DiviOrgCode='" + dto.DivisionOrgCode + "',DistOrgCode='" + dto.DistrictOrgCode + "',DistDescription='" + dto.DistrictDescription + "' where DiviCode ='" + dto.DivisionCode + "'and DistCode='" + dto.DistrictCode + "'";
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
