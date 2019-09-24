using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.SystemControl
{
   public class A2ZDIVISIONDTO
   {
       #region Propertise
       public int DivisionCode { set; get; }
       public int DivisionOrgCode { set; get; }
       public String DivisionDescription { set; get; }
       #endregion

       public static int InsertInformation(A2ZDIVISIONDTO dto)
       {
           dto.DivisionDescription = (dto.DivisionDescription != null) ? dto.DivisionDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZDIVISION(DiviCode,DiviOrgCode,DiviDescription)values('" + dto.DivisionCode + "','" + dto.DivisionOrgCode + "','" + dto.DivisionDescription + "')";
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

       public static A2ZDIVISIONDTO GetInformation(int DivCode)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDIVISION WHERE DiviCode = " + DivCode, "A2ZHKMCUS");


           var p = new A2ZDIVISIONDTO();
           if (dt.Rows.Count > 0)
           {

               p.DivisionCode = Converter.GetInteger(dt.Rows[0]["DiviCode"]);
               p.DivisionOrgCode = Converter.GetInteger(dt.Rows[0]["DiviOrgCode"]);
               p.DivisionDescription = Converter.GetString(dt.Rows[0]["DiviDescription"]);
               return p;
           }
           p.DivisionCode = 0;

           return p;

       }

       public static int UpdateInformation(A2ZDIVISIONDTO dto)
       {
           dto.DivisionDescription = (dto.DivisionDescription != null) ? dto.DivisionDescription.Trim().Replace("'", "''") : "";
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZDIVISION set DiviCode='" + dto.DivisionCode + "',DiviOrgCode='" + dto.DivisionOrgCode + "',DiviDescription='" + dto.DivisionDescription + "' where DiviCode ='" + dto.DivisionCode + "'";
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
