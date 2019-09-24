using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZLSECURITYDTO
    {
        #region Propertise

        public int LSecurityCode { set; get; }
        public String LSecurityDescription { set; get; }
        #endregion


        public static int InsertInformation(A2ZLSECURITYDTO dto)
        {
            dto.LSecurityDescription = (dto.LSecurityDescription != null) ? dto.LSecurityDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZLSECURITY(LSecurityCode,LSecurityDescription)values('" + dto.LSecurityCode + "','" + dto.LSecurityDescription + "')";
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

        public static A2ZLSECURITYDTO GetInformation(Int16 Lscode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZLSECURITY WHERE LSecurityCode = " + Lscode, "A2ZCSMCUS");


            var p = new A2ZLSECURITYDTO();
            if (dt.Rows.Count > 0)
            {

                p.LSecurityCode = Converter.GetSmallInteger(dt.Rows[0]["LSecurityCode"]);
                p.LSecurityDescription = Converter.GetString(dt.Rows[0]["LSecurityDescription"]);
                return p;
            }
            p.LSecurityCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZLSECURITYDTO dto)
        {
            dto.LSecurityDescription = (dto.LSecurityDescription != null) ? dto.LSecurityDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZLSECURITY set LSecurityCode='" + dto.LSecurityCode + "',LSecurityDescription='" + dto.LSecurityDescription + "' where LSecurityCode='" + dto.LSecurityCode + "'";
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
