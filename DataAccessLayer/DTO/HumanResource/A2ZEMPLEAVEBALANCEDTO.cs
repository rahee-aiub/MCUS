using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.HumanResource
{
  public  class A2ZEMPLEAVEBALANCEDTO
    {
        #region Propertise

        public int EmpCode { set; get; }
        public int LeaveCode { set; get; }
        public int LeaveYear { set; get; }
        public decimal LeaveDays { set; get; }
        public decimal LeaveBalDays { set; get; }
        public int norecord { set; get; }
        #endregion

        public static int InsertInformation(A2ZEMPLEAVEBALANCEDTO dto)
        {
            
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZEMPLEAVEBALANCE(EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)values('" + dto.EmpCode + "','" + dto.LeaveYear + "','" + dto.LeaveCode + "','" + dto.LeaveDays + "','" + dto.LeaveBalDays + "')";
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

        public static A2ZEMPLEAVEBALANCEDTO GetInformation(int Ecode, int Lyear, int Lcode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZEMPLEAVEBALANCE WHERE EmpCode='" + Ecode + "' AND LeaveYear='" + Lyear + "' AND LeaveCode='" + Lcode + "'", "A2ZHRMCUS");


            var p = new A2ZEMPLEAVEBALANCEDTO();
            if (dt.Rows.Count > 0)
            {

                p.EmpCode = Converter.GetInteger(dt.Rows[0]["EmpCode"]);
                p.LeaveYear = Converter.GetInteger(dt.Rows[0]["LeaveYear"]);
                p.LeaveCode = Converter.GetInteger(dt.Rows[0]["LeaveCode"]);
                p.LeaveDays = Converter.GetDecimal(dt.Rows[0]["LeaveDays"]);
                p.LeaveBalDays = Converter.GetDecimal(dt.Rows[0]["LeaveBalDays"]);
                p.norecord = 1;
                return p;
            }
            p.LeaveCode = 0;
            p.norecord = 0;
            return p;

        }
        public static int UpdateInformation(A2ZEMPLEAVEBALANCEDTO dto)
        {
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZEMPLEAVEBALANCE set EmpCode='" + dto.EmpCode + "',LeaveYear='" + dto.LeaveYear + "',LeaveCode='" + dto.LeaveCode + "',LeaveDays='" + dto.LeaveDays + "', LeaveBalDays='" + dto.LeaveBalDays + "' where  EmpCode='" + dto.EmpCode + "' AND LeaveYear='" + dto.LeaveYear + "' AND LeaveCode='" + dto.LeaveCode + "'";
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
