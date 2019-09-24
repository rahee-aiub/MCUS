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
   public  class A2ZBANKDTO
    {
        #region Propertise

        public Int16 BankCode { set; get; }
        public String BankName { set; get; }
        #endregion


        public static int InsertInformation(A2ZBANKDTO dto)
        {
            dto.BankName = (dto.BankName != null) ? dto.BankName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZBANK(BankCode,BankName)values('" + dto.BankCode + "','" + dto.BankName + "')";
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

        public static A2ZBANKDTO GetInformation(Int16 bankcode)
        {
            var prm = new object[1];

            prm[0] = bankcode;
           


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_HRGetInfoBank", prm, "A2ZHRMCUS");
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZBANK WHERE BankCode = " + bankcode, "A2ZHRMCUS");


            var p = new A2ZBANKDTO();
            if (dt.Rows.Count > 0)
            {

                p.BankCode = Converter.GetSmallInteger(dt.Rows[0]["BankCode"]);
                p.BankName = Converter.GetString(dt.Rows[0]["BankName"]);
                return p;
            }
            p.BankCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZBANKDTO dto)
        {
            dto.BankName = (dto.BankName != null) ? dto.BankName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZBANK set BankCode='" + dto.BankCode + "',BankName='" + dto.BankName + "' where BankCode='" + dto.BankCode + "'";
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
