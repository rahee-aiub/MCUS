

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
  public class A2ZTRNCTRLDTO
    {
        #region Propertise

      public int AccType { set; get; }

      public Int16 AccTypeClass { set; get; }
      public Int16 AccTypeMode { set; get; }
      public int PayType { set; get; }
      public int TrnType { set; get; }
      public int TrnMode { set; get; }
      public String TrnRecDesc { set; get; }
      public int ShowInterest { set; get; }
      public int GLAccNoCr { set; get; }
      public int GLAccNoDr { set; get; }

      public int FuncOpt { set; get; }
      public int TranCode { set; get; }
      public String TranDes { set; get; }
      public int TranLogic { set; get; }
      public int RecMode { set; get; }
      public Int16 TrnPayment { set; get; }
      public int Record { set; get; }

        #endregion

      public static int InsertInformation(A2ZTRNCTRLDTO dto)
      {
         
          int rowEffect = 0;
          string strQuery = @"INSERT into A2ZTRNCTRL(FuncOpt,TrnCode,TrnDesc,AccType,AccTypeClass,AccTypeMode,PayType,TrnType,TrnMode,TrnRecDesc,TrnLogic,ShowInt,RecMode,GLAccNoCR,GLAccNoDR,TrnPayment)values('" + dto.FuncOpt + "','" + dto.TranCode + "','" + dto.TranDes + "','" + dto.AccType + "','" + dto.AccTypeClass + "','" + dto.AccTypeMode + "','" +
          +dto.PayType + "','" + dto.TrnType + "','" + dto.TrnMode + "','" + dto.TrnRecDesc + "','" + dto.TranLogic + "','" + dto.ShowInterest + "','" + dto.RecMode + "','" + dto.GLAccNoCr + "','" + dto.GLAccNoDr + "','" + dto.TrnPayment + "')";
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
      public static A2ZTRNCTRLDTO GetInformation(int FuncOpt,int TranCode,int paytype,int trntype,int TranMode)
      {

          var prm = new object[5];

          prm[0] = FuncOpt;
          prm[1] = TranCode;
          prm[2] = paytype;
          prm[3] = trntype;
          prm[4] = TranMode;


          DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTrnCtrl", prm, "A2ZCSMCUS");
          
          
          
          //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNCTRL WHERE FuncOpt='" + FuncOpt + "' and  TrnCode='" + TranCode + "' and PayType='" + paytype + "' and TrnType='" + trntype + "' and TrnMode='"+TranMode+"' ", "A2ZCSMCUS");


          var p = new A2ZTRNCTRLDTO();
          if (dt.Rows.Count > 0)
          {
              p.FuncOpt = Converter.GetInteger(dt.Rows[0]["FuncOpt"]);
              p.TranCode = Converter.GetInteger(dt.Rows[0]["TrnCode"]);
              p.TranDes = Converter.GetString(dt.Rows[0]["TrnDesc"]);
              p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
              p.AccTypeClass = Converter.GetSmallInteger(dt.Rows[0]["AccTypeClass"]);
              p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
             
              p.PayType = Converter.GetInteger(dt.Rows[0]["PayType"]);
              p.TrnType = Converter.GetInteger(dt.Rows[0]["TrnType"]);
              p.TrnMode = Converter.GetInteger(dt.Rows[0]["TrnMode"]);
              p.TrnRecDesc = Converter.GetString(dt.Rows[0]["TrnRecDesc"]);
              p.TranLogic = Converter.GetInteger(dt.Rows[0]["TrnLogic"]);
              p.ShowInterest = Converter.GetInteger(dt.Rows[0]["ShowInt"]);
              p.RecMode = Converter.GetInteger(dt.Rows[0]["RecMode"]);
              p.GLAccNoCr = Converter.GetInteger(dt.Rows[0]["GLAccNoCR"]);
              p.GLAccNoDr = Converter.GetInteger(dt.Rows[0]["GLAccNoDR"]);
              p.TrnPayment = Converter.GetSmallInteger(dt.Rows[0]["TrnPayment"]);
              p.Record = Converter.GetInteger(1);
              return p;
          }
          p.AccType = 0;
          p.Record = 0;
          return p;

      }

      public static int UpdateInformation(A2ZTRNCTRLDTO dto)
      {
         
          int rowEffect = 0;
          string strQuery = "UPDATE A2ZTRNCTRL set FuncOpt='" + dto.FuncOpt + "', TrnCode='" + dto.TranCode + "',TrnDesc='" + dto.TranDes + "', AccType='" + dto.AccType + "',AccTypeClass='" + dto.AccTypeClass + "',AccTypeMode='" + dto.AccTypeMode + "',PayType='" +
              dto.PayType + "',TrnType='" + dto.TrnType + "',TrnMode='" + dto.TrnMode + "',TrnRecDesc='" + dto.TrnRecDesc + "',TrnLogic='" + dto.TranLogic + "', ShowInt='" + dto.ShowInterest + "',RecMode='" + dto.RecMode +
              "',GLAccNoCR='" + dto.GLAccNoCr + "',GLAccNoDR='" + dto.GLAccNoDr + "',TrnPayment='" + dto.TrnPayment + "' where AccType='" + dto.AccType + "' AND PayType='" + dto.PayType + "' AND TrnType='" + dto.TrnType + "'";
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
