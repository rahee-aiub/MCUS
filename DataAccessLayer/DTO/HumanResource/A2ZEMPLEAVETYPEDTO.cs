using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.HumanResource
{
  public  class A2ZEMPLEAVETYPEDTO
    {
        #region Propertise

        public int LeaveCode { set; get; }
        public String LeaveName { set; get; }
        public decimal TotalDays { set; get; }
        public Boolean EffectSalary { set; get; }
        public Boolean Status { set; get; }
        #endregion

        public static int InsertInformation(A2ZEMPLEAVETYPEDTO dto)
        {
            dto.LeaveName = (dto.LeaveName != null) ? dto.LeaveName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZEMPLEAVETYPE(EmpleaveCode,EmpleaveName,TotalDays,EffectSalary,Status)values('" + dto.LeaveCode + "','" + dto.LeaveName + "','" + dto.TotalDays + "','" + dto.EffectSalary + "','" + dto.Status + "')";
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

        public static A2ZEMPLEAVETYPEDTO GetInformation(int Lcode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZEMPLEAVETYPE WHERE EmpleaveCode = " + Lcode, "A2ZHRMCUS");


            var p = new A2ZEMPLEAVETYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.LeaveCode = Converter.GetInteger(dt.Rows[0]["EmpleaveCode"]);
                p.LeaveName = Converter.GetString(dt.Rows[0]["EmpleaveName"]);
                p.TotalDays = Converter.GetDecimal(dt.Rows[0]["TotalDays"]);
                p.EffectSalary = Converter.GetBoolean(dt.Rows[0]["EffectSalary"]);
                p.Status = Converter.GetBoolean(dt.Rows[0]["Status"]);
                return p;
            }
            p.LeaveCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZEMPLEAVETYPEDTO dto)
        {
            dto.LeaveName = (dto.LeaveName != null) ? dto.LeaveName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZEMPLEAVETYPE set EmpleaveCode='" + dto.LeaveCode + "',EmpleaveName='" + dto.LeaveName + "',TotalDays='" + dto.TotalDays + "',EffectSalary='" + dto.EffectSalary + "',Status='"+dto.Status+"' where EmpleaveCode='" + dto.LeaveCode + "'";
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
