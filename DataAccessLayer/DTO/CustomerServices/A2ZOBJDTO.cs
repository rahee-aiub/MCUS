using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZOBJDTO
    {
        #region Propertise

              
        public decimal ObjAmount { set; get; }

        public String txtObjAmount { set; get; }
        public short NoMsg { set; get; }
        public short NoRecord { set; get; }   // 1= Record Found, 0=No Record Found


        #endregion

        //public static A2ZOBJDTO GetRoundUpAmt(decimal ObjAmount)
        //{
        //    var p = new A2ZOBJDTO();
        //    p.txtObjAmount = Converter.GetString(ObjAmount);

            
        //    int a = p.txtObjAmount.Length;
        //    int b = a - 2;
        //    int b = a - 2;
        //    string c = p.txtObjAmount.Substring(b, a);
           


        //    string c = "";
        //    int a = txtHidden.Text.Length;

        //    string b = txtHidden.Text;
        //    c = b.Substring(0, 1);
        //    int re = Converter.GetSmallInteger(c);
        //    int dd = a - 1;
        //    string d = b.Substring(1, dd);
        //    int re1 = Converter.GetSmallInteger(d);


        //    Int16 CuType = Converter.GetSmallInteger(re);
        //    int CNo = Converter.GetSmallInteger(re1);
            
            
            
            
        //    DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
        //        AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


        //    var p = new A2ZTRNLOGICDTO();
        //    if (dt.Rows.Count > 0)
        //    {

        //        p.LogicAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
        //        p.NoRecord = Converter.GetSmallInteger(1);
        //        return p;
        //    }
        //    else
        //    {
        //        p.NoRecord = 0;
        //    }


        //    return p;

        //}
       

    }
}

