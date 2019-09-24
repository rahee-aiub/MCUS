using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCFIELDSDTO
    {
        #region propertise
       public Int16 Flag { set; get; }
        public int Code { set; get; }
        public String Description { set; get; }

        #endregion

        public static int InsertInformation(A2ZACCFIELDSDTO dto)
        {
            dto.Description = (dto.Description != null) ? dto.Description.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZACCFIELDS(FieldsFlag,Code,Description)values('" + dto.Flag + "','" + dto.Code + "','" + dto.Description + "')";
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
        public static A2ZACCFIELDSDTO GetInformation(Int16 MainCode,Int16 flag)
        {

            var prm = new object[2];

            prm[0] = MainCode;
            prm[1] = flag;
           


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccFields", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCFIELDS WHERE Code = '" + MainCode+"'and FieldsFlag='"+flag+"'", "A2ZCSMCUS");


            var p = new A2ZACCFIELDSDTO();
            if (dt.Rows.Count > 0)
            {
                p.Flag = Converter.GetSmallInteger(dt.Rows[0]["FieldsFlag"]);
                p.Code = Converter.GetSmallInteger(dt.Rows[0]["Code"]);
                p.Description = Converter.GetString(dt.Rows[0]["Description"]);
                return p;
            }
            p.Code = 0;

            return p;

        }

        public static int UpdateInformation(A2ZACCFIELDSDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCFIELDS set  Code='" + dto.Code + "',Description='" + dto.Description + "' where Code ='" + dto.Code + "'and FieldsFlag='"+dto.Flag+"'";
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
