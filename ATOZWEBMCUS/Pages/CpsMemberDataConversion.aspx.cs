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
    public partial class CpsMemberDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {

            CPSMEMBER();



            string qry3 = "SELECT Id,MemNo,MemType FROM A2ZMEMBER ";
            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");

            if (dt3.Rows.Count > 0)
            {

                foreach (DataRow dr3 in dt3.Rows)
                {
                    var ParentId = dr3["Id"].ToString();
                    var MemNo = dr3["MemNo"].ToString();
                    A2ZMEMBERDTO objDTO = new A2ZMEMBERDTO();
                    objDTO.ID = Converter.GetSmallInteger(ParentId);
                    if (MemNo == "0")
                    {
                        objDTO.MemType = Converter.GetSmallInteger(1);
                    }
                    else
                    {
                        objDTO.MemType = Converter.GetSmallInteger(2);
                    }

                    int row2 = A2ZMEMBERDTO.Update2(objDTO);

                }
            }

        }

        
        private void CPSMEMBER()
        {
            string qry1 = "SELECT OldMem,MemName,OpenDate,NewCuType,NewCuNo FROM WFCPSMEMBER";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            int totrec = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dt1.Rows)
                {
                    var oldmem = dr1["OldMem"].ToString();
                    var memname = dr1["MemName"].ToString();
                    var opendate = dr1["OpenDate"].ToString();
                    var newcutype = dr1["NewCuType"].ToString();
                    var newcuno = dr1["NewCuNo"].ToString();

                 
                    string qry3 = "SELECT CuType,CuNo,MemNo,MemOld2CuNo,MemOld2MemNo FROM A2ZMEMBER WHERE CuType = '" + newcutype + "' AND  CuNo = '" + newcuno + "' AND MemOld2MemNo = '" + oldmem + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow dr3 in dt3.Rows)
                        {

                            var CType = dr3["CuType"].ToString();
                            var CNo = dr3["CuNo"].ToString();
                            var MNo = dr3["MemNo"].ToString();
                            var Mold2CNo = dr3["MemOld2CuNo"].ToString();
                            var Mold2MNo = dr3["MemOld2MemNo"].ToString();

                            string strQuery = "UPDATE WFCPSMEMBER SET  NewMemNo = '" + MNo + "' WHERE  CuType = '" + CType + "' AND CuNo = '" + CNo + "'";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                    }
                    else
                    {
                        //string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld2CuNo,MemOld2MemNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
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
                String cstext1 = "alert('Cps Data Conversion successfully completed.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}