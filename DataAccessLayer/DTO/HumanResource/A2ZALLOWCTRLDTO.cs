using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.HumanResource
{
   public class A2ZALLOWCTRLDTO
   {
       #region Propertise
       public int AllowCode { set; get; }
       public string AllowDesc { set; get; }
       public int DependsOn { set; get; }
       public Boolean Location { set; get; }
       public Boolean Percentage { set; get; }
       public Boolean ServType { set; get; }
       public Boolean Religion { set; get; }
      
      
    
      #endregion


       public static int InsertInformation(A2ZALLOWCTRLDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZALLOWCTRL(AllowCode,AllowDesc,DependsOn,Location, Percentage,ServType,Religion) 
                     values('" + dto.AllowCode + "','" + dto.AllowDesc + "','" + dto.DependsOn + "','" + dto.Location + "','" +
                                 dto.Percentage + "','" + dto.ServType + "','" + dto.Religion + "')";
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
       public static A2ZALLOWCTRLDTO GetInformation(int AllowCode)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZALLOWCTRL WHERE AllowCode = '" + AllowCode + "'", "A2ZHRMCUS");


           var p = new A2ZALLOWCTRLDTO();
           if (dt.Rows.Count > 0)
           {
               p.AllowCode = Converter.GetInteger(dt.Rows[0]["AllowCode"]);
               p.AllowDesc = Converter.GetString(dt.Rows[0]["AllowDesc"]);
               p.DependsOn = Converter.GetInteger(dt.Rows[0]["DependsOn"]);
               p.Location = Converter.GetBoolean(dt.Rows[0]["Location"]);
               p.Percentage = Converter.GetBoolean(dt.Rows[0]["Percentage"]);
               p.ServType = Converter.GetBoolean(dt.Rows[0]["ServType"]);
               p.Religion = Converter.GetBoolean(dt.Rows[0]["Religion"]);
               
               return p;
           }
           p.AllowCode = 0;
           return p;
       }

       
       public static int UpdateInformation(A2ZALLOWCTRLDTO dto)
       {

           int rowEffect = 0;
           string strQuery = "UPDATE A2ZALLOWCTRL set dependsOn='" + dto.DependsOn + "', Location='" + dto.Location + "', Percentage='" + dto.Percentage + "', ServType='" + dto.ServType + "', Religion='" + dto.Religion + "' where AllowCode='" + dto.AllowCode + "'";
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
