using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.HouseKeeping

{
   public class A2ZDATABASEDTO
   {
       #region Propertise
       public String DatabaseName { set; get; }
       
    
      #endregion


       public static int InsertInformation(A2ZDATABASEDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZDATABASE(DatabaseName,TransDatabase) values('" + dto.DatabaseName + "',1)";
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
