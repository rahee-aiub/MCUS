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
   public  class A2ZDEPARTMENTDTO
    {
        #region Propertise

       public Int16 DepartmentCode { set; get; }
       public String DepartmentName { set; get; }
        #endregion


       public static int InsertInformation(A2ZDEPARTMENTDTO dto)
        {
            dto.DepartmentName = (dto.DepartmentName != null) ? dto.DepartmentName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZDEPARTMENT(DepartmentCode,DepartmentName)values('" + dto.DepartmentCode + "','" + dto.DepartmentName + "')";
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

       public static A2ZDEPARTMENTDTO GetInformation(Int16 Deptcode)
        {
            var prm = new object[1];

            prm[0] = Deptcode;



            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_HKGetInfoDepartment", prm, "A2ZHKMCUS");
 
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDEPARTMENT WHERE DepartmentCode = " + Deptcode, "A2ZHKMCUS");


            var p = new A2ZDEPARTMENTDTO();
            if (dt.Rows.Count > 0)
            {

                p.DepartmentCode = Converter.GetSmallInteger(dt.Rows[0]["DepartmentCode"]);
                p.DepartmentName = Converter.GetString(dt.Rows[0]["DepartmentName"]);
                return p;
            }
            p.DepartmentCode = 0;

            return p;

        }
       public static int UpdateInformation(A2ZDEPARTMENTDTO dto)
        {
            dto.DepartmentName = (dto.DepartmentName != null) ? dto.DepartmentName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZDEPARTMENT set DepartmentCode='" + dto.DepartmentCode + "',DepartmentName='" + dto.DepartmentName + "' where DepartmentCode='" + dto.DepartmentCode + "'";
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
