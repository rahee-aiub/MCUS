using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.SystemControl
{
  public  class A2ZRELIGIONDTO
    {
     #region Propertise

      public int RelegionCode { set; get; }
      public String RelegionDescription { set; get; }
      #endregion

      public static int InsertInformation(A2ZRELIGIONDTO dto)
      {
          dto.RelegionDescription = (dto.RelegionDescription != null) ? dto.RelegionDescription.Trim().Replace("'", "''") : "";
          int rowEffect = 0;
          string strQuery = @"INSERT into A2ZRELIGION(RelegionCode,RelegionDescription)values('" + dto.RelegionCode + "','" + dto.RelegionDescription + "')";
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

      public static A2ZRELIGIONDTO GetInformation(Int16 Religioncode)
      {
          DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZRELIGION WHERE RelegionCode = " + Religioncode, "A2ZHKMCUS");


          var p = new A2ZRELIGIONDTO();
          if (dt.Rows.Count > 0)
          {

              p.RelegionCode = Converter.GetSmallInteger(dt.Rows[0]["RelegionCode"]);
              p.RelegionDescription = Converter.GetString(dt.Rows[0]["RelegionDescription"]);
              return p;
          }
          p.RelegionCode = 0;

          return p;

      }
      public static int UpdateInformation(A2ZRELIGIONDTO dto)
      {
          dto.RelegionDescription = (dto.RelegionDescription != null) ? dto.RelegionDescription.Trim().Replace("'", "''") : "";
          int rowEffect = 0;
          string strQuery = "UPDATE A2ZRELIGION set RelegionCode='" + dto.RelegionCode + "',RelegionDescription='" + dto.RelegionDescription + "' where RelegionCode='" + dto.RelegionCode + "'";
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
