using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZEMPADDRESSDTO
    {
        #region Properties

        public Int16 ID { set; get; }
        public int EmployeeID { set; get; }
        public String EmpPresentAddress { set; get; }
        public int EmpPreDivision { set; get; }
        public int EmpPreDistrict { set; get; }
        public int EmpPreUpzila { set; get; }
        public int EmpPreThana { set; get; }
        public String EmpPreTelNo { set; get; }
        public String EmpPreMobileNo { set; get; }
        public String EmpPreEmail { set; get; }
        public String EmpPermanentAddress { set; get; }
        public int EmpPerDivision { set; get; }
        public int EmpPerDistrict { set; get; }
        public int EmpPerUpzila { set; get; }
        public int EmpPerThana { set; get; }
        public String EmpPerTelNo { set; get; }
        public String EmpPerMobileNo { set; get; }
        public String EmpPerEmail { set; get; }
   


        #endregion

        public static A2ZEMPADDRESSDTO GetAddressInformation(int empCode)
        {
            DataTable dt = CommonManager.Instance.GetDataTableByQuery(
                "SELECT * FROM A2ZEMPADDRESS WHERE EmployeeID =  '" + empCode + "'", "A2ZHRMCUS");

            var p = new A2ZEMPADDRESSDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.EmployeeID = Converter.GetSmallInteger(dt.Rows[0]["EmployeeID"]);
                p.EmpPresentAddress = Converter.GetString(dt.Rows[0]["EmpPresentAddress"]);
                p.EmpPreDivision = Converter.GetInteger(dt.Rows[0]["EmpPreDivision"]);
                p.EmpPreDistrict = Converter.GetInteger(dt.Rows[0]["EmpPreDistrict"]);
                p.EmpPreUpzila = Converter.GetInteger(dt.Rows[0]["EmpPreUpzila"]);
                p.EmpPreThana = Converter.GetInteger(dt.Rows[0]["EmpPreThana"]);
                p.EmpPreTelNo = Converter.GetString(dt.Rows[0]["EmpPreTelNo"]);
                p.EmpPreMobileNo = Converter.GetString(dt.Rows[0]["EmpPreMobileNo"]);
                p.EmpPreEmail = Converter.GetString(dt.Rows[0]["EmpPreEmail"]);
                p.EmpPermanentAddress = Converter.GetString(dt.Rows[0]["EmpPermanentAddress"]);
                p.EmpPerDivision = Converter.GetInteger(dt.Rows[0]["EmpPerDivision"]);
                p.EmpPerDistrict = Converter.GetInteger(dt.Rows[0]["EmpPerDistrict"]);
                p.EmpPerUpzila = Converter.GetInteger(dt.Rows[0]["EmpPerUpzila"]);
                p.EmpPerThana = Converter.GetInteger(dt.Rows[0]["EmpPerThana"]);
                p.EmpPerTelNo = Converter.GetString(dt.Rows[0]["EmpPerTelNo"]);
                p.EmpPerMobileNo = Converter.GetString(dt.Rows[0]["EmpPerMobileNo"]);
                p.EmpPerEmail = Converter.GetString(dt.Rows[0]["EmpPerEmail"]);
             
                return p;
            }

            p.EmployeeID = 0;

            return p;
        }

        public static int InsertAddressInformation(A2ZEMPADDRESSDTO dto)
        {


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmployeeAddressDataInsert", new object[] { dto.EmployeeID, dto.EmpPresentAddress, dto.EmpPreDivision, dto.EmpPreDistrict, dto.EmpPreUpzila, dto.EmpPreThana, dto.EmpPreTelNo, dto.EmpPreMobileNo, dto.EmpPreEmail, dto.EmpPermanentAddress, dto.EmpPerDivision, dto.EmpPerDistrict, dto.EmpPerUpzila, dto.EmpPerThana, dto.EmpPerTelNo, dto.EmpPerMobileNo, dto.EmpPerEmail });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int UpdateAddressInformation(A2ZEMPADDRESSDTO dto)
        {

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmployeeAddressDataUpdate", new object[] { dto.EmployeeID, dto.EmpPresentAddress, dto.EmpPreDivision, dto.EmpPreDistrict, dto.EmpPreUpzila, dto.EmpPreThana, dto.EmpPreTelNo, dto.EmpPreMobileNo, dto.EmpPreEmail, dto.EmpPermanentAddress, dto.EmpPerDivision, dto.EmpPerDistrict, dto.EmpPerUpzila, dto.EmpPerThana, dto.EmpPerTelNo, dto.EmpPerMobileNo, dto.EmpPerEmail });

            if (result == 0)
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
