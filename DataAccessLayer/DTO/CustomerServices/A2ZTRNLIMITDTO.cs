using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZTRNLIMITDTO
    {
        #region Propertise

        public int IdsNo { set; get; }
        public double LIdsCashCredit { set; get; }
        public double LIdsCashDebit { set; get; }
        public double LIdsTrfCredit { set; get; }
        public double LIdsTrfDebit { set; get; }
        
        
        #endregion


        public static int InsertInformation(A2ZTRNLIMITDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZTRNLIMIT(IdsNo,LIdsCashCredit,LIdsCashDebit,LIdsTrfCredit,LIdsTrfDebit)values('" + dto.IdsNo + "','" +
                dto.LIdsCashCredit + "','" + dto.LIdsCashDebit + "','" + dto.LIdsTrfCredit + "','" + dto.LIdsTrfDebit + "')";
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

        public static A2ZTRNLIMITDTO GetInformation(int idno)
        {
            var prm = new object[1];

            prm[0] = idno;
            


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoTrnLimit", prm, "A2ZCSMCUS");
            
                        
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRNLIMIT WHERE IdsNo = " + idno, "A2ZCSMCUS");


            var p = new A2ZTRNLIMITDTO();
            if (dt.Rows.Count > 0)
            {

                p.IdsNo = Converter.GetInteger(dt.Rows[0]["IdsNo"]);
                p.LIdsCashCredit = Converter.GetDouble(dt.Rows[0]["LIdsCashCredit"]);
                p.LIdsCashDebit = Converter.GetDouble(dt.Rows[0]["LIdsCashDebit"]);
                p.LIdsTrfCredit = Converter.GetDouble(dt.Rows[0]["LIdsTrfCredit"]);
                p.LIdsTrfDebit = Converter.GetDouble(dt.Rows[0]["LIdsTrfDebit"]);
                
              
                return p;
            }
            p.IdsNo = 0;

            return p;

        }
        public static int UpdateInformation(A2ZTRNLIMITDTO dto)
        {
           
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZTRNLIMIT set IdsNo='" + dto.IdsNo + "',LIdsCashCredit='" + dto.LIdsCashCredit + "',LIdsCashDebit='" +
                dto.LIdsCashDebit + "',LIdsTrfCredit='" + dto.LIdsTrfCredit + "',LIdsTrfDebit='" + dto.LIdsTrfDebit + "' where IdsNo='" + dto.IdsNo + "'";
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
