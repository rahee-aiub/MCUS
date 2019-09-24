using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCCTRLDTO
   {
       #region Propertise
       public Int16 ProductCode { set; get; }
       public Int16 ControlCode { set; get; }
       public Int16 RecordCode { set; get; }
       public Int16 RecordFlag { set; get; }
       public String Description { set; get; }
       public Int16 FuncFlag { set; get; }
    
      #endregion


       public static int InsertInformation(A2ZACCCTRLDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZACCCTRL(ProductCode,ControlCode,RecordCode, Description,RecordFlag,FuncFlag) 
                     values('" + dto.ProductCode + "','" + dto.ControlCode + "','" + dto.RecordCode + "','" +
                                 dto.Description + "','" + dto.RecordFlag + "','" + dto.FuncFlag + "')";
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
       public static A2ZACCCTRLDTO GetInformation(Int16 MainCode, Int16 CtrlCode, Int16 Rcode)
       {
           var prm = new object[3];

           prm[0] = MainCode;
           prm[1] = CtrlCode;
           prm[2] = Rcode;
           


           DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccCtrl", prm, "A2ZCSMCUS");
           
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCCTRL WHERE ProductCode = '" + MainCode + "'and ControlCode = '" + CtrlCode + "' and RecordCode='" + Rcode + "'", "A2ZCSMCUS");


           var p = new A2ZACCCTRLDTO();
           if (dt.Rows.Count > 0)
           {
               p.ControlCode = Converter.GetSmallInteger(dt.Rows[0]["ControlCode"]);
               p.ProductCode = Converter.GetSmallInteger(dt.Rows[0]["ProductCode"]);
               p.RecordCode = Converter.GetSmallInteger(dt.Rows[0]["RecordCode"]);
               p.RecordFlag = Converter.GetSmallInteger(dt.Rows[0]["RecordFlag"]);
               p.FuncFlag = Converter.GetSmallInteger(dt.Rows[0]["FuncFlag"]);
               p.Description = Converter.GetString(dt.Rows[0]["Description"]);
               return p;
           }
           p.ProductCode = 0;
           p.RecordCode = 0;

           return p;

       }
       public static int UpdateInformation(A2ZACCCTRLDTO dto)
       {

           int rowEffect = 0;
           string strQuery = "UPDATE A2ZACCCTRL set RecordFlag='" + dto.RecordFlag + "', FuncFlag='" + dto.FuncFlag + "' where ProductCode='" + dto.ProductCode + "' and RecordCode='" + dto.RecordCode + "'";
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
