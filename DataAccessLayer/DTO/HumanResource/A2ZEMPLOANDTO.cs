using System;
using System.Data;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public  class A2ZEMPLOANDTO
    {
        #region Properties

        public Int16 ID { get; set; }
        public Int16 EmpCode { get; set; }
        public DateTime LoanDate { get; set; }
        public Decimal LoanAmount { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime LoanEndDate { get; set; }
        public Decimal LoanReturnAmt { get; set; }
        public Int16 LoanFlag { get; set; }
        public Int16 LoanType { get; set; }
        public Int16 LoanStatus { get; set; }
        public Int16 UserId { get; set; }
        public DateTime CreateDate { get; set; }
         
        #endregion

        public static A2ZEMPLOANDTO GetEmpLoanInformation(Int16 EmpCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZEMPLOAN WHERE EmpCode = " + EmpCode, "A2ZERPHR");

            var p = new A2ZEMPLOANDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.EmpCode = Converter.GetSmallInteger(dt.Rows[0]["EmpCode"]);
                p.LoanDate = Converter.GetDateTime(dt.Rows[0]["LoanDate"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["LoanAmount"]);
                p.LoanStartDate = Converter.GetDateTime(dt.Rows[0]["LoanStartDate"]);
                p.LoanEndDate = Converter.GetDateTime(dt.Rows[0]["LoanEndDate"]);
                p.LoanReturnAmt = Converter.GetDecimal(dt.Rows[0]["LoanReturnAmt"]);
                p.LoanFlag = Converter.GetSmallInteger(dt.Rows[0]["LoanFlag"]);
                p.LoanType = Converter.GetSmallInteger(dt.Rows[0]["LoanType"]);
                p.LoanStatus = Converter.GetSmallInteger(dt.Rows[0]["LoanStatus"]);
                p.UserId = Converter.GetSmallInteger(dt.Rows[0]["UserId"]);
                p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);

                return p;
            }

            p.EmpCode = 0;

            return p;
        }

        public static int InsertEmpLoanInformation(A2ZEMPLOANDTO empLoan)
        {
            int rowEffect = 0;

            string strQuery =
                "INSERT INTO A2ZEMPLOAN (EmpCode,LoanDate,LoanAmount,LoanStartDate,LoanEndDate,LoanReturnAmt,LoanFlag,LoanType,LoanStatus,UserId)VALUES('" +
                empLoan.EmpCode + "','" + empLoan.LoanDate + "','" + empLoan.LoanAmount+ "','" + empLoan.LoanStartDate + "','" + empLoan.LoanEndDate +
                "','" + empLoan.LoanReturnAmt + "','" + empLoan.LoanFlag + "','" + empLoan.LoanType + "','" +
                empLoan.LoanStatus + "','" + empLoan.UserId + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZERPHR"));

            if (rowEffect == 0)
            {
                return 0;
            }
            return rowEffect;
        }

        public static int UpdateEmpLoanInformation(A2ZEMPLOANDTO empLoan)
        {
            string strQuery = "UPDATE A2ZEMPLOAN SET LoanDate ='" + empLoan.LoanDate + "',LoanAmount ='" +
                              empLoan.LoanAmount + "',LoanStartDate ='" + empLoan.LoanStartDate + "',LoanEndDate ='" +
                              empLoan.LoanEndDate + "',LoanReturnAmt ='" + empLoan.LoanReturnAmt + "',UserId='" +
                              empLoan.UserId + "' WHERE EmpCode ='" + empLoan.EmpCode + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZERPHR"));
        }
    }
}
