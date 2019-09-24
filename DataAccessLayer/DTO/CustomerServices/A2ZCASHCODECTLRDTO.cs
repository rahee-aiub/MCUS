using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.DTO.CustomerServices;

namespace DataAccessLayer.DTO
{
  public  class A2ZCASHCODECTLRDTO
    {
        #region Propertise

        public int CashCode { set; get; }
        public int LastVoucherNo { set; get; }
        #endregion


        //public static int InsertInformation(A2ZCASHCODECTLRDTO dto)
        //{
        //    int rowEffect = 0;
        //    string strQuery = @"INSERT into A2ZCGLMST(AccStatusCode,AccStatusDescription)values('" + dto.AccStatusCode + "','" + dto.AccStatusDescription + "')";
        //    rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

        //    if (rowEffect == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        public static A2ZCASHCODECTLRDTO GetInformation(Int16 cashcode)
        {
            var prm = new object[1];

            prm[0] = cashcode;
            


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GLGetInfoCashCodeCtrl", prm, "A2ZGLMCUS");
            
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCGLMST WHERE GLAccNo = " + cashcode, "A2ZGLMCUS");


            var p = new A2ZCASHCODECTLRDTO();
            if (dt.Rows.Count > 0)
            {

                p.CashCode = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                p.LastVoucherNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                return p;
            }
            p.CashCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZCASHCODECTLRDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCGLMST set GLAccNo='" + dto.CashCode + "', LastVoucherNo='" + dto.LastVoucherNo + "' where GLAccNo='" + dto.CashCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
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
