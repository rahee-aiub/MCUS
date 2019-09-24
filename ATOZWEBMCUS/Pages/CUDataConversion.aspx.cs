using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class CUDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
          CuNo();
          OldCuNo();
          Old5000CuNo();
          GLCashCode();
          Successful();
        }

        private void GLCashCode()
        {

            string qry = "SELECT Ids,NewCashCode,OldCashCode FROM wfCashCode ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {                
                    var NewCashCode = dr["NewCashCode"].ToString();
                    var OldCashCode = dr["OldCashCode"].ToString();

                    int code = Converter.GetInteger(OldCashCode);
                    string qry1 = "SELECT Id FROM A2ZCUNION WHERE GLCASHCODE='" + code + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCUNIONDTO objDTO = new A2ZCUNIONDTO();
                            objDTO.ID = Converter.GetSmallInteger(ParentId);
      
                            objDTO.GLCashCode = Converter.GetInteger(NewCashCode);
                            int row2 = A2ZCUNIONDTO.Update1(objDTO);
                        }
                    }
                }
            }
        }


        private void CuNo()
        {

            string sqlquery3 = "Truncate table dbo.A2ZCUNION ";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
            
            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();

                    int cno = Converter.GetInteger(CuNo);
                    string qry1 = "SELECT CUNO,CUNAME,CUOPDT,CUMEMFLAG,CUTYPE,CUCERTNO,CUADDL1,CUADDL2,CUADDL3,CUEMAIL,CUTEL,CUMOBILE,CUFAX,CUDIVI,CUDIST,CUTHAN,GLCASHCODE FROM A2ZCRUNION WHERE CUNO='" + cno + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");

                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                           var CNo = dr1["CUNO"].ToString();
                           var CName = dr1["CUNAME"].ToString().Trim().Replace("'", "''"); 
                           var COpDt = dr1["CUOPDT"].ToString();
                           var CMemFlag = dr1["CUMEMFLAG"].ToString();
                           var CType = dr1["CUTYPE"].ToString();
                           var CCertNo = dr1["CUCERTNO"].ToString();

                           var CAddl1 = dr1["CUADDL1"].ToString().Trim().Replace("'", "''");
                           var CAddl2 = dr1["CUADDL2"].ToString().Trim().Replace("'", "''");
                           var CAddl3 = dr1["CUADDL3"].ToString().Trim().Replace("'", "''");
                           var CEmail = dr1["CUEMAIL"].ToString();
                           var CTel = dr1["CUTEL"].ToString();
                           var CMobile = dr1["CUMOBILE"].ToString();
                           var CFax = dr1["CUFAX"].ToString();
                           var CDivi = dr1["CUDIVI"].ToString();
                           var CDist = dr1["CUDIST"].ToString();
                           var CThana = dr1["CUTHAN"].ToString();
                           var CGLCashCode = dr1["GLCASHCODE"].ToString();

                           string CuTypeName = "";
                           if (CuType == "1")
                           {
                               CuTypeName = "Affiliate";
                            }
                            else if (CuType == "2")
                            {
                                CuTypeName = "Associate";
                            }
                            else if (CuType == "3")
                            {
                                CuTypeName = "Regular";
                            }

                            Int16 CuProcFlag = 13;
                        
                        
                            if (COpDt == "")
                            {
                                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                                COpDt = Converter.GetString(dto.ProcessDate);
                            }

                            DateTime CuStatusDate = Converter.GetDateTime(COpDt);
                            DateTime InputByDate = Converter.GetDateTime(COpDt);
                            DateTime VerifyByDate = Converter.GetDateTime(COpDt);
                            DateTime ApprovByDate = Converter.GetDateTime(COpDt);

                            String qry2 = "INSERT INTO A2ZCUNION(CuType,CuNo,CuTypeName,CuOldCuNo,CuName,CuOpDt,CuFlag,CuCertNo,CuAddL1,CuAddL2,CuAddL3,CuEmail,CuTel,CuMobile,CuFax,CuDivi,CuDist,CuThana,GLCashCode,CuProcFlag,CuStatusDate,InputByDate,VerifyByDate,ApprovByDate) VALUES('" + CuType + "','" + CuNo + "','" + CuTypeName + "','" + CuNo + "','" + CName + "','" + COpDt + "','" + CMemFlag + "','" + CCertNo + "','" + CAddl1 + "','" + CAddl2 + "','" + CAddl3 + "','" + CEmail + "','" + CTel + "','" + CMobile + "','" + CFax + "','" + CDivi + "','" + CDist + "','" + CThana + "','" + CGLCashCode + "','" + CuProcFlag + "','" + CuStatusDate + "','" + InputByDate + "','" + VerifyByDate + "','" + ApprovByDate + "')";
                            int rowefect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry2, "A2ZCSMCUS"));
                        
                        }
                    }
                }

                int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCuDataConvert", "A2ZCSMCUS"));
                if (result1 == 0)
                {

                }
            }
        }

        private void OldCuNo()
        {
            string qry1 = "SELECT CuType,CuNo,OCuNo,LastMemNo FROM WFOCUNO";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            int totrec = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dt1.Rows)
                {
                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var OCuNo = dr1["OCuNo"].ToString();
                    var LastMem = dr1["LastMemNo"].ToString();

                    string str1Query = "UPDATE A2ZCUNION SET  CuOld1CuNo = '" + OCuNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' ";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                }
            }
        }

        private void Old5000CuNo()
        {
            string qry1 = "SELECT CuType,CuNo,OCuNo,LastMemNo FROM WFO5000CUNO";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            int totrec = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dt1.Rows)
                {
                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var OCuNo = dr1["OCuNo"].ToString();
                    var LastMem = dr1["LastMemNo"].ToString();

                    string str1Query = "UPDATE A2ZCUNION SET  CuOld2CuNo = '" + OCuNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' ";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                }
            }
        }

        private void Successful()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Data Conversion successfully completed.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }




    }
}