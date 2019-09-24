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
    public partial class MemberDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {

            string sqlquery4 = "Truncate table dbo.A2ZMEMBER ";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));


            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOldCuNo,MemOldMemNo) SELECT MEMTYPE,CUNO,MEMNO,MEMNAME,MEMOPDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZMEMBER WHERE CUNO = '" + CuNo + "'", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "' WHERE  CuNo='" + CuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        Successful();

                    }
                    con.Close();
                }
            }

            OLDLASTMEMBERNO();
            OLDMEMBER();
            OLD5000LASTMEMBERNO();
            OLD5000MEMBER();

            AddStaffMemberUpd();

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

            string strQry = "DELETE A2ZMEMBER WHERE  CuType= 3 AND CuNo = 534 AND MemNo = 1 AND MemOldMemNo = 1";
            int rowEff = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZCSMCUS"));

        }

        private void OLDLASTMEMBERNO()
        {

            string qry1 = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            int totrec = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dt1.Rows)
                {
                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var OCuNo = dr1["OCuNo"].ToString();
                    int lastMemNo = 0;

                    string qry3 = "SELECT Id,CuType,CuNo,MemNo FROM A2ZMEMBER WHERE CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow dr3 in dt3.Rows)
                        {
                            var ParentId = dr3["Id"].ToString();
                            var Ctype = dr3["CuType"].ToString();
                            var CNo = dr3["CuNo"].ToString();
                            var MemNo = dr3["MemNo"].ToString();


                            int MNo = Converter.GetInteger(MemNo);

                            if (MNo > lastMemNo)
                            {
                                lastMemNo = MNo;
                                string strQuery = "UPDATE WFOCUNO SET  LastMemNo = '" + MNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }

                        }
                    }
                }
            }

        }
        private void OLDMEMBER()
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

                    double LastMemNo = Converter.GetInteger(LastMem);

                    //string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "' AND MEMNO !=0";
                    string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "'";

                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCCULB");

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow dr3 in dt3.Rows)
                        {

                            var CNo = dr3["CuNo"].ToString();
                            var MemNo = dr3["MemNo"].ToString();
                            var MemName = dr3["MemName"].ToString();
                            var MemOpDate = dr3["MemOpDt"].ToString();

                            int MNo = Converter.GetInteger(MemNo);

                            MemName = (MemName != null) ? MemName.Trim().Replace("'", "''") : "";

                            if (MNo == 0)
                            {
                                string qry2 = "SELECT * FROM A2ZMEMBER WHERE MemNo=0 AND CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                                if (dt2.Rows.Count > 0)
                                {
                                    string strQuery = "UPDATE A2ZMEMBER SET MemOld1CuNo = '" + OCuNo + "', MemOld1MemNo = '" + MemNo + "' WHERE MemNo=0 AND CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                }
                                else
                                {
                                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld1CuNo,MemOld1MemNo) VALUES('" + 0 + "','" + CNo + "','" + 0 + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                    string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld1CuNo='" + OCuNo + "' ";
                                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                                }
                            }
                            else
                            {
                                LastMemNo = LastMemNo + 1;
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld1CuNo,MemOld1MemNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld1CuNo='" + OCuNo + "' ";
                                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                            }




                        }
                    }

                }
            }

        }

        private void OLD5000LASTMEMBERNO()
        {

            string qry1 = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            int totrec = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dt1.Rows)
                {
                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var OCuNo = dr1["OCuNo"].ToString();
                    int lastMemNo = 0;

                    string qry3 = "SELECT Id,CuType,CuNo,MemNo FROM A2ZMEMBER WHERE CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow dr3 in dt3.Rows)
                        {
                            var ParentId = dr3["Id"].ToString();
                            var Ctype = dr3["CuType"].ToString();
                            var CNo = dr3["CuNo"].ToString();
                            var MemNo = dr3["MemNo"].ToString();


                            int MNo = Converter.GetInteger(MemNo);

                            if (MNo > lastMemNo)
                            {
                                lastMemNo = MNo;
                                string strQuery = "UPDATE WFO5000CUNO SET  LastMemNo = '" + MNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }

                        }
                    }
                }
            }

        }

        private void OLD5000MEMBER()
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

                    double LastMemNo = Converter.GetInteger(LastMem);

                    string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "' AND MEMNO !=0";
                    //string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "'";

                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCCULB");

                    if (dt3.Rows.Count > 0)
                    {

                        foreach (DataRow dr3 in dt3.Rows)
                        {

                            var CNo = dr3["CuNo"].ToString();
                            var MemNo = dr3["MemNo"].ToString();
                            var MemName = dr3["MemName"].ToString();
                            var MemOpDate = dr3["MemOpDt"].ToString();

                            int MNo = Converter.GetInteger(MemNo);

                            MemName = (MemName != null) ? MemName.Trim().Replace("'", "''") : "";

                            if (MNo == 0)
                            {
                                string qry2 = "SELECT * FROM A2ZMEMBER WHERE MemNo=0 AND CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                                if (dt2.Rows.Count > 0)
                                {
                                    string strQuery = "UPDATE A2ZMEMBER SET  MemOld2CuNo = '" + OCuNo + "',MemOld2MemNo = '" + MemNo + "' WHERE MemNo=0 AND CuType = '" + CuType + "' AND CuNo = '" + CuNo + "'";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                }
                                else
                                {
                                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld2CuNo,MemOld2MemNo) VALUES('" + 0 + "','" + CNo + "','" + 0 + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                    string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld2CuNo='" + OCuNo + "' ";
                                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                                }
                            }
                            else
                            {
                                LastMemNo = LastMemNo + 1;
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld2CuNo,MemOld2MemNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld2CuNo='" + OCuNo + "' ";
                                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                            }




                        }
                    }

                }
            }

        }

        //private void OLDMEMBER()
        //{
        //    string qry1 = "SELECT CuType,CuNo,OCuNo,LastMemNo FROM WFOCUNO";
        //    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
        //    int totrec = dt1.Rows.Count;
        //    if (dt1.Rows.Count > 0)
        //    {

        //        foreach (DataRow dr1 in dt1.Rows)
        //        {
        //            var CuType = dr1["CuType"].ToString();
        //            var CuNo = dr1["CuNo"].ToString();
        //            var OCuNo = dr1["OCuNo"].ToString();
        //            var LastMem = dr1["LastMemNo"].ToString();

        //            double LastMemNo = Converter.GetInteger(LastMem);

        //            //string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "' AND MEMNO !=0";
        //            string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "'";

        //            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCCULB");

        //            if (dt3.Rows.Count > 0)
        //            {

        //                foreach (DataRow dr3 in dt3.Rows)
        //                {

        //                    var CNo = dr3["CuNo"].ToString();
        //                    var MemNo = dr3["MemNo"].ToString();
        //                    var MemName = dr3["MemName"].ToString();
        //                    var MemOpDate = dr3["MemOpDt"].ToString();

        //                    int MNo = Converter.GetInteger(MemNo);

        //                    if (MNo == 0)
        //                    {
        //                        string strQuery = "UPDATE A2ZMEMBER SET  MemOld1MemNo = '" + MemNo + "' WHERE MemNo=0 AND AND CuNo = '" + OCuNo + "'";
        //                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                    }

        //                    LastMemNo = LastMemNo + 1;

        //                    MemName = (MemName != null) ? MemName.Trim().Replace("'", "''") : "";

        //                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE MemNo=0 AND CuNo = '" + OCuNo + "'";
        //                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
        //                    if (dt2.Rows.Count > 0)
        //                    {
        //                        string strQuery = "UPDATE A2ZMEMBER SET  MemOld1MemNo = '" + MemNo + "' WHERE MemNo=0 AND AND CuNo = '" + OCuNo + "'";
        //                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                    }
        //                    else
        //                    {
        //                        string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld1CuNo,MemOld1MemNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
        //                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

        //                        if (rowEffect > 0)
        //                        {
        //                            string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld1CuNo='" + OCuNo + "' ";
        //                            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

        //                            string str1Query = "UPDATE A2ZCUNION SET  CuOld1CuNo = '" + OCuNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' ";
        //                            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
        //                        }
        //                    }

        //                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,OldCuNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + CNo + "')", con);

        //                    //con.Open();

        //                    //int result2 = cmd1.ExecuteNonQuery();
        //                    //if (result2 > 0)
        //                    //{
        //                    //    string strQuery = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  OldCuNo='" + OCuNo + "' ";
        //                    //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                    //}
        //                    //con.Close();
        //                }
        //            }

        //        }
        //    }

        //}



        //private void OLDMEMBER()
        //{
        //    string qry1 = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
        //    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
        //    int totrec = dt1.Rows.Count;
        //    if (dt1.Rows.Count > 0)
        //    {

        //        foreach (DataRow dr1 in dt1.Rows)
        //        {
        //            var CuType = dr1["CuType"].ToString();
        //            var CuNo = dr1["CuNo"].ToString();
        //            var OCuNo = dr1["OCuNo"].ToString();

        //            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld1CuNo,MemOld1MemNo) SELECT MEMTYPE,CUNO,MEMNO,MEMNAME,MEMOPDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZMEMBER WHERE CUNO = '" + OCuNo + "' AND MEMNO !=0", con);
        //            con.Open();

        //            int result2 = cmd1.ExecuteNonQuery();
        //            if (result2 > 0)
        //            {
        //                string strQuery = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld1CuNo='" + OCuNo + "' ";
        //                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


        //                string str1Query = "UPDATE A2ZCUNION SET  CuOld1CuNo = '" + OCuNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' ";
        //                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));


        //            }
        //            con.Close();
        //        }
        //    }

        //}










        //private void OLD5000MEMBER()
        //{
        //    string qry1 = "SELECT CuType,CuNo,OCuNo,LastMemNo FROM WFO5000CUNO";
        //    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
        //    int totrec = dt1.Rows.Count;
        //    if (dt1.Rows.Count > 0)
        //    {

        //        foreach (DataRow dr1 in dt1.Rows)
        //        {
        //            var CuType = dr1["CuType"].ToString();
        //            var CuNo = dr1["CuNo"].ToString();
        //            var OCuNo = dr1["OCuNo"].ToString();
        //            var LastMem = dr1["LastMemNo"].ToString();

        //            double LastMemNo = Converter.GetInteger(LastMem);

        //            string qry3 = "SELECT CuNo,MemNo,MemName,MemOpDt FROM A2ZMEMBER WHERE CuNo = '" + OCuNo + "' AND MEMNO !=0";
        //            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCCULB");

        //            if (dt3.Rows.Count > 0)
        //            {

        //                foreach (DataRow dr3 in dt3.Rows)
        //                {

        //                    var CNo = dr3["CuNo"].ToString();
        //                    var MemNo = dr3["MemNo"].ToString();
        //                    var MemName = dr3["MemName"].ToString();
        //                    var MemOpDate = dr3["MemOpDt"].ToString();

        //                    LastMemNo = LastMemNo + 1;

        //                    MemName = (MemName != null) ? MemName.Trim().Replace("'", "''") : "";

        //                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld2CuNo,MemOld2MemNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + OCuNo + "','" + MemNo + "')";
        //                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

        //                    if (rowEffect > 0)
        //                    {
        //                        string strQuery1 = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  MemOld2CuNo='" + OCuNo + "' ";
        //                        int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

        //                        string str1Query = "UPDATE A2ZCUNION SET  CuOld2CuNo = '" + OCuNo + "' WHERE  CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' ";
        //                        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
        //                    }


        //                    //SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,OldCuNo) VALUES('" + 0 + "','" + CNo + "','" + LastMemNo + "','" + MemName + "','" + MemOpDate + "','" + CNo + "')", con);

        //                    //con.Open();

        //                    //int result2 = cmd1.ExecuteNonQuery();
        //                    //if (result2 > 0)
        //                    //{
        //                    //    string strQuery = "UPDATE A2ZMEMBER SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  OldCuNo='" + OCuNo + "' ";
        //                    //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                    //}
        //                    //con.Close();
        //                }
        //            }

        //        }
        //    }

        //}

        private void AddStaffMemberUpd()
        {
            string qry2 = "SELECT MemNo,MemName FROM wfStaffMember";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    var MemNo = dr2["MemNo"].ToString();
                    var MemName = dr2["MemName"].ToString();

                    int memno = Converter.GetInteger(MemNo);


                    string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName) VALUES (0, 0, '" + memno + "', '" + MemName + "')";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

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

                            string strQuery = "UPDATE WFCPSMEMBER SET  NewMemNo = '" + MNo + "' WHERE  NewCuType = '" + CType + "' AND NewCuNo = '" + CNo + "' AND OldMem = '" + oldmem + "'";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                            string strQry = "UPDATE A2ZCSMCUS.dbo.A2ZMEMBER SET  MemOld1CuNo = '" + newcuno + "', MemOld1MemNo = '" + oldmem + "' WHERE  CuType = '" + CType + "' AND  CuNo = '" + CNo + "' AND MemNo = '" + MNo + "'";
                            int rowEff = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZCSMCUS"));
                        }
                    }
                    else
                    {
                        string qry4 = "SELECT Max(MemNo) AS MemNo FROM A2ZMEMBER WHERE CuType = '" + newcutype + "' AND  CuNo = '" + newcuno + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");

                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr4 in dt4.Rows)
                            {

                                var mno = dr4["MemNo"].ToString();

                                int M = Converter.GetInteger(mno);

                                M = (M + 1);

                                memname = (memname != null) ? memname.Trim().Replace("'", "''") : "";

                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBER(CuType,CuNo,MemNo,MemName,MemOpenDate,MemOld2CuNo,MemOld2MemNo) VALUES('" + newcutype + "','" + newcuno + "','" + M + "','" + memname + "','" + opendate + "','" + newcuno + "','" + oldmem + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string str1Query = "UPDATE WFCPSMEMBER SET  NewMemNo = '" + M + "' WHERE  NewCuType = '" + newcutype + "' AND NewCuNo = '" + newcuno + "'  AND OldMem = '" + oldmem + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }

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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}