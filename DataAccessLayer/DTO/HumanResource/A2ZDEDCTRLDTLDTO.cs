using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.HumanResource
{
   public class A2ZDEDCTRLDTLDTO
   {
       #region Propertise
       public int ID { set; get; }
       public int DedCode { set; get; }
       public string DedDesc { set; get; }
       public Int16 BaseGrade { get; set; }
       public String BaseGradeDesc { set; get; }
       public int GradeCode { set; get; }
       public string GradeDesc { set; get; }
       public int DesignationCode { set; get; }
       public string DesigDesc { set; get; }
       public int LocationCode { set; get; }
       public string LocationDesc { set; get; }
       public int ServTypeCode { set; get; }
       public string ServTypeDesc { set; get; }
       public int ReligionCode { set; get; }
       public string ReligionDesc { set; get; }
       public decimal Amount { set; get; }
       public Boolean Status { set; get; }
      
    
      #endregion


       public static int InsertInformation(A2ZDEDCTRLDTLDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZDEDCTRLDTL(DedCode,DedDesc,BaseGrade,BaseGradeDesc,GradeCode,GradeDesc,DesignationCode,DesigDesc,LocationCode,LocationDesc,ServTypeCode,ServTypeDesc,ReligionCode,ReligionDesc,Amount,Status) 
                     values('" + dto.DedCode + "','" + dto.DedDesc + "','" + dto.BaseGrade + "','" + dto.BaseGradeDesc + "','" + dto.GradeCode + "','" + dto.GradeDesc + "','" + dto.DesignationCode + "','" + dto.DesigDesc + "','" +
                                 dto.LocationCode + "','" + dto.LocationDesc + "','" + dto.ServTypeCode + "','" + dto.ServTypeDesc + "','" + dto.ReligionCode + "','" + dto.ReligionDesc + "','" + dto.Amount + "','" + dto.Status + "')";
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
       public static A2ZDEDCTRLDTLDTO GetInformation(int DedCode)
       {
           DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDEDCTRLDTL WHERE DedCode = '" + DedCode + "'", "A2ZHRMCUS");


           var p = new A2ZDEDCTRLDTLDTO();
           if (dt.Rows.Count > 0)
           {
               p.ID = Converter.GetInteger(dt.Rows[0]["Id"]);
               p.DedCode = Converter.GetInteger(dt.Rows[0]["DedCode"]);
               p.DedDesc = Converter.GetString(dt.Rows[0]["DedDesc"]);
               p.BaseGrade = Converter.GetSmallInteger(dt.Rows[0]["BaseGrade"]);
               p.BaseGradeDesc = Converter.GetString(dt.Rows[0]["BaseGradeDesc"]);
               p.GradeCode = Converter.GetInteger(dt.Rows[0]["GradeCode"]);
               p.GradeDesc = Converter.GetString(dt.Rows[0]["GradeDesc"]);
               p.DesignationCode = Converter.GetInteger(dt.Rows[0]["DesignationCode"]);
               p.DesigDesc = Converter.GetString(dt.Rows[0]["DesigDesc"]);
               p.LocationCode = Converter.GetInteger(dt.Rows[0]["LocationCode"]);
               p.LocationDesc = Converter.GetString(dt.Rows[0]["LocationDesc"]);
               p.ServTypeCode = Converter.GetInteger(dt.Rows[0]["ServTypeCode"]);
               p.ServTypeDesc = Converter.GetString(dt.Rows[0]["ServTypeDesc"]);
               p.ReligionCode = Converter.GetInteger(dt.Rows[0]["ReligionCode"]);
               p.ReligionDesc = Converter.GetString(dt.Rows[0]["ReligionDesc"]);
               p.Amount = Converter.GetDecimal(dt.Rows[0]["Amount"]);
               p.Status = Converter.GetBoolean(dt.Rows[0]["Status"]);
               return p;
           }
           p.DedCode = 0;
           return p;
       }

       
       public static int UpdateInformation(A2ZDEDCTRLDTLDTO dto)
       {

           int rowEffect = 0;
           string strQuery = "UPDATE A2ZDEDCTRLDTL set BaseGrade='" + dto.BaseGrade + "', BaseGradeDesc='" + dto.BaseGradeDesc + "',GradeCode='" + dto.GradeCode + "', GradeDesc='" + dto.GradeDesc + "', DesignationCode='" + dto.DesignationCode + "',DesigDesc='" + dto.DesigDesc + "', LocationCode='" + dto.LocationCode + "',LocationDesc='" + dto.LocationDesc + "', ServTypeCode='" + dto.ServTypeCode + "',ServTypeDesc='" + dto.ServTypeDesc + "',ReligionCode='" + dto.ReligionCode + "',ReligionDesc='" + dto.ReligionDesc + "', Amount='" + dto.Amount + "', Status='" + dto.Status + "' where Id='" + dto.ID + "'";
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
