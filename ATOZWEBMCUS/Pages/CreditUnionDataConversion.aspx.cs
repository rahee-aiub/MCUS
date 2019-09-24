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

namespace ATOZWEBMCUS.Pages
{
    public partial class CreditUnionDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            string sqlquery3 = "Truncate table dbo.A2ZCUNION ";
         //   string sqlquery4 = "Truncate table dbo.A2ZMEMBER ";

            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZCSMCUS"));
        //    int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));

            SqlCommand cmd = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZCUNION(CuOldCuNo,CuName,CuOpDt,CuFlag,CuType,CuCertNo,CuAddL1,CuAddL2,CuAddL3,CuEmail,CuTel,CuMobile,CuFax,CuDivi,CuDist,CuThana,GLCashCode) SELECT CUNO,CUNAME,CUOPDT,CUMEMFLAG,CUTYPE,CUCERTNO,CUADDL1,CUADDL2,CUADDL3,CUEMAIL,CUTEL,CUMOBILE,CUFAX,CUDIVI,CUDIST,CUTHAN,GLCASHCODE FROM A2ZCCULB.dbo.A2ZCRUNION", con);
        //    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemName,MemOpenDate) SELECT CUTYPE,CUNO,CUNAME,CUOPDT FROM A2ZCCULB.dbo.A2ZCRUNION", con);
            con.Open();
            int result1 = cmd.ExecuteNonQuery();
       //     int result2 = cmd1.ExecuteNonQuery();
            if (result1 > 0)
            {
                Successful();

            }

            string qry = "SELECT Id,CuType,CuNo,CuName,CuOpDt FROM A2ZCUNION ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var ParentId = dr["Id"].ToString();

                    //if (ParentId <= totrec.text)   
                    //{

                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var CName = dr["CuName"].ToString();
                    var COpDt = dr["CuOpDt"].ToString();
                    int a = Converter.GetSmallInteger(CType);
                    int b = Converter.GetSmallInteger(CNo);

                    A2ZCUNIONDTO objDTO = new A2ZCUNIONDTO();
                    objDTO.ID = Converter.GetSmallInteger(ParentId);
                    if (CType == "0")
                    {
                        objDTO.CuType = 1;
                        objDTO.CuTypeName = "Affiliate";
                    }
                    else if (CType == "1")
                    {
                        objDTO.CuType = 2;
                        objDTO.CuTypeName = "Associate";
                    }
                    else if (CType == "2")
                    {
                        objDTO.CuType = 3;
                        objDTO.CuTypeName = "Regular";
                    }

                    objDTO.CuProcFlag = 13;
                    if (COpDt == "")
                    {
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        string strQuery = "UPDATE A2ZCUNION set CuOpDt='" + dto.ProcessDate + "' where  Id='" + objDTO.ID + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        if (rowEffect > 0)
                        {
                             DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT CuOpDt FROM A2ZCUNION WHERE Id='" + objDTO.ID + "'","A2ZCSMCUS");
                            if(dt1.Rows.Count>0)
                            {
                            var OpDt1=Converter.GetString(dt1.Rows[0]["CuOpDt"]);
                            objDTO.CuStatusDate = Converter.GetDateTime(OpDt1);
                            objDTO.InputByDate = Converter.GetDateTime(OpDt1);
                            objDTO.VerifyByDate = Converter.GetDateTime(OpDt1);
                            objDTO.ApprovByDate = Converter.GetDateTime(OpDt1);
                             }

                        }

                    }
                    else
                    {
                        objDTO.CuStatusDate = Converter.GetDateTime(COpDt);
                        objDTO.InputByDate = Converter.GetDateTime(COpDt);
                        objDTO.VerifyByDate = Converter.GetDateTime(COpDt);
                        objDTO.ApprovByDate = Converter.GetDateTime(COpDt);
                    }


                  
                   
                    int row = A2ZCUNIONDTO.UpdateInformation1(objDTO);
                    if (row > 0)
                    {

                    }

                    Int16 rcytype = Converter.GetSmallInteger(objDTO.CuType);
                    A2ZRECCTRLDTO RDTO = (A2ZRECCTRLDTO.GetLastRecords(rcytype));
                    objDTO.CreditUnionNo = Converter.GetSmallInteger(RDTO.CtrlRecLastNo);
                    

                    int row1 = A2ZCUNIONDTO.UpdateInformation1(objDTO);
                    if (row1 > 0)
                    {
                                             
                   //     A2ZMEMBERDTO MemDTO = new A2ZMEMBERDTO();
                   //     MemDTO.MemberNo = 0;
                  //      MemDTO.ID = Converter.GetSmallInteger(objDTO.ID);
                  //      MemDTO.CuType = Converter.GetSmallInteger(objDTO.CuType);
                  //      MemDTO.CreditUnionNo = Converter.GetInteger(objDTO.CreditUnionNo);
                  //      MemDTO.MemberName = Converter.GetString(CName);
                  //      int row2 = A2ZMEMBERDTO.Update(MemDTO);
                        
                       
                    }

                }
            }

            GLCashCode();

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