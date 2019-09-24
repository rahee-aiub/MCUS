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
    public partial class AccountDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlAccType.Focus();

        }






        protected void BtnProceed_Click(object sender, EventArgs e)
        {

            if (ddlTrunTable.Text == "1")
            {
                string sqlquery4 = "Truncate table dbo.A2ZACCOUNT ";
                int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));
            }


            if (ddlAccType.Text == "1" || ddlAccType.Text == "0")
            {

                A2ZACC11();
                OA2ZACC11();
                O5000A2ZACC11();


            }

            if (ddlAccType.Text == "2" || ddlAccType.Text == "0")
            {

                A2ZACC12();
                OA2ZACC12();
                //O5000A2ZACC12();

            }

            if (ddlAccType.Text == "3" || ddlAccType.Text == "0")
            {

                A2ZACC13();
                OA2ZACC13();
                O5000A2ZACC13();

            }

            if (ddlAccType.Text == "4" || ddlAccType.Text == "0")
            {

                A2ZACC14();
                OA2ZACC14();
                O5000A2ZACC14();   
                InsertCPSAcc();
                XA2ZACC14();

            }

            if (ddlAccType.Text == "5" || ddlAccType.Text == "0")
            {

                A2ZACC15();
                OA2ZACC15();
                O5000A2ZACC15();
                XA2ZACC15();

            }

            if (ddlAccType.Text == "6" || ddlAccType.Text == "0")
            {

                A2ZACC16();
                OA2ZACC16();
                O5000A2ZACC16();
                XA2ZACC16();

            }

            if (ddlAccType.Text == "7" || ddlAccType.Text == "0")
            {

                A2ZACC17();
                OA2ZACC17();
                O5000A2ZACC17();
                XA2ZACC17();

            }

            if (ddlAccType.Text == "8" || ddlAccType.Text == "0")
            {

                A2ZACC18();
                OA2ZACC18();
                O5000A2ZACC18();
                TRFA2ZACC18();
                TRFOA2ZACC18();
                TRFO5000A2ZACC18();

            }



            if (ddlAccType.Text == "10" || ddlAccType.Text == "0")
            {
                A2ZACC20();
                OA2ZACC20();
                O5000A2ZACC20();

            }

            if (ddlAccType.Text == "11" || ddlAccType.Text == "0")
            {
                A2ZACC21();
                OA2ZACC21();
                O5000A2ZACC21();

            }


            if (ddlAccType.Text == "12" || ddlAccType.Text == "0")
            {
                A2ZACC23();
                OA2ZACC23();
                O5000A2ZACC23();
                TRFA2ZACC23();
                TRFOA2ZACC23();
                TRFO5000A2ZACC23();

            }

            if (ddlAccType.Text == "13" || ddlAccType.Text == "0")
            {
                A2ZACC24();
                OA2ZACC24();
                O5000A2ZACC24();

            }

            if (ddlAccType.Text == "14" || ddlAccType.Text == "0")
            {
                A2ZACC51();
                OA2ZACC51();
                O5000A2ZACC51();
                TRFA2ZACC54();
                TRFA2ZACC61();

            }

            if (ddlAccType.Text == "15" || ddlAccType.Text == "0")
            {
                A2ZACC52();
                OA2ZACC52();
                O5000A2ZACC52();

            }

            if (ddlAccType.Text == "16" || ddlAccType.Text == "0")
            {
                //A2ZACC53();
                //OA2ZACC53();
                //O5000A2ZACC53();

                InsertA2ZACC53();
                IntRateA2ZACC53();

            }

            //if (ddlAccType.Text == "17" || ddlAccType.Text == "0")
            //{
            //    A2ZACC54();
            //    OA2ZACC54();
            //    O5000A2ZACC54();

            //}

            if (ddlAccType.Text == "18" || ddlAccType.Text == "0")
            {
                A2ZACC55();
                OA2ZACC55();
                O5000A2ZACC55();

            }

            if (ddlAccType.Text == "19" || ddlAccType.Text == "0")
            {
                A2ZACC58();
                OA2ZACC58();
                O5000A2ZACC58();

            }
       
            Successful();


        }

        private void A2ZACC11()
        {

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=11";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        lblProcessing.Text = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC11 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 11 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC11()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC11 WHERE CUNO = '" + OCuNo + "'", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE AccType = 11 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                    con.Close();
                }
            }
        }

        private void O5000A2ZACC11()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC11 WHERE CUNO = '" + OCuNo + "'", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string qry4 = "SELECT MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "',MemNo = '" + MemNo + "' WHERE AccType = 11 AND CuNo='" + OCuNo + "' AND MemNo='" + OldMemNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }

                string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=11 AND CuType = 0 AND CuNo=0 and MemNo=0";
                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            }
        }



        private void A2ZACC12()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=12";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC12()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        lblProcessing.Text = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC12 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 12 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

       

        private void OA2ZACC12()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC12  WHERE CUNO='" + OCuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }



                    //------------------------------------------------
                    //                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC12 WHERE CUNO = '" + OCuNo + "'", con);
                    //                    con.Open();

                    //                    int result2 = cmd1.ExecuteNonQuery();
                    //                    if (result2 > 0)
                    //                    {
                    //                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  AccType = 12 AND CuNo='" + OCuNo + "' ";
                    //                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    //                    }
                    //                    con.Close();

                }
            }
        }

        private void O5000A2ZACC12()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                lblProcessing.Text = "O5000A2ZACC12()";

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC12 WHERE CUNO = '" + OCuNo + "'", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string qry4 = "SELECT MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "',MemNo = '" + MemNo + "' WHERE AccType = 12 AND CuNo='" + OCuNo + "' AND MemNo='" + OldMemNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }
        private void A2ZACC13()
        {

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=13";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC13()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        lblProcessing.Text = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC13 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 13 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }


        private void OA2ZACC13()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC13 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 13 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 13 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC13()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC13 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE AccType = 13 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 13 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }



       
        private void A2ZACC14()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=14";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    lblProcessing.Text = "A2ZACC14()";


                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccMonthlyDeposit,AccPeriod,AccTotalDep,OldCuNo,AccRaNetInt) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCMTHDEP,ACCPERIOD,ACCTOTDEP,CUNO,ACCTRFINT FROM A2ZCCULB.dbo.A2ZACC14 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 14 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }


        private void OA2ZACC14()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccMonthlyDeposit,AccPeriod,AccTotalDep,OldCuNo,OldMemNo,AccRaNetInt) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCMTHDEP,ACCPERIOD,ACCTOTDEP,CUNO,MEMNO,ACCTRFINT FROM A2ZCCULB.dbo.A2ZACC14 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 14 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 14 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC14()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccMonthlyDeposit,AccPeriod,AccTotalDep,OldCuNo,OldMemNo,AccRaNetInt) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCMTHDEP,ACCPERIOD,ACCTOTDEP,CUNO,MEMNO,ACCTRFINT FROM A2ZCCULB.dbo.A2ZACC14 WHERE CUNO = '" + OCuNo + "'", con);



                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 14 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 14 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        private void InsertCPSAcc()
        {

            string qry = "SELECT NewCuType,NewCuNo,NewMemNo,OpenDate,LastTrnDate,DepositAmt,Period,Balance FROM WFCPSMEMBER";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["NewCuType"].ToString();
                    var CNo = dr["NewCuNo"].ToString();
                    var MNo = dr["NewMemNo"].ToString();

                    var opendate = dr["OpenDate"].ToString();
                    var ltrndate = dr["LastTrnDate"].ToString();
                    var depositamt = dr["DepositAmt"].ToString();
                    var period = dr["Period"].ToString();
                    var balance = dr["Balance"].ToString();

                    DateTime Odate = Converter.GetDateTime(opendate);
                    DateTime Ldate = Converter.GetDateTime(ltrndate);


                    DateTime Matdate = new DateTime();
                    Matdate = Odate.AddMonths(Converter.GetSmallInteger(period));

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccBalance,AccTotalDep,AccLastTrnDateU,AccMonthlyDeposit,AccPeriod,AccAtyClass,AccMatureDate) VALUES (14,'" + CType + "','" + CNo + "','" + MNo + "','" + Odate + "',1,'" + balance + "','" + balance + "','" + Ldate + "','" + depositamt + "','" + period + "',4,'" + Matdate + "')", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {

                    }
                    con.Close();
                }
            }

            

        }


        private void XA2ZACC14()
        {

            string qry1 = "SELECT Id,AccOpenDate,AccBalance,AccTotalDep,AccMonthlyDeposit,AccPeriod,AccRaNetInt FROM A2ZACCOUNT where AccType = 14";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var ParentId = dr1["Id"].ToString();
                    var AccPeriod = dr1["AccPeriod"].ToString();
                    var DAmt = dr1["AccMonthlyDeposit"].ToString();
                    var AccOpenDate = dr1["AccOpenDate"].ToString();

                    var Abalance = dr1["AccBalance"].ToString();
                    var TDbalance = dr1["AccTotalDep"].ToString();
                    var Ibalance = dr1["AccRaNetInt"].ToString();


                    int Id = Converter.GetInteger(ParentId);
                    DateTime Odate = Converter.GetDateTime(AccOpenDate);

                    double WAccPeriod = Converter.GetDouble(AccPeriod);

                    WAccPeriod = (WAccPeriod * 12);

                    double WDepositAmt = Converter.GetDouble(DAmt);

                    double WAbal = Converter.GetDouble(Abalance);
                    double WTDbal = Converter.GetDouble(TDbalance);
                    double WIbal = Converter.GetDouble(Ibalance);

                    double totdeposit = (WAbal - WIbal);


                    RoundingByFlag.Text = "0";

                    if (WDepositAmt > 1000)
                    {
                        RoundingByFlag.Text = "1";
                        NoRoundingBy.Text = Converter.GetString(WDepositAmt / 1000);
                        WDepositAmt = Converter.GetDouble(1000);
                    }


                    DateTime Matdate = new DateTime();
                    //Matdate = DateTime.ParseExact(Odate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    Matdate = Odate.AddMonths(Converter.GetSmallInteger(WAccPeriod));
                    DateTime dt = Converter.GetDateTime(Matdate);


                    int accType = Converter.GetInteger(14);
                    Int16 SlabFlag = Converter.GetSmallInteger(1);

                    Int16 period = Converter.GetSmallInteger(WAccPeriod);
                    A2ZPENSIONDTO getDTO = (A2ZPENSIONDTO.GetInformation(accType, SlabFlag, WDepositAmt, period));

                    if (getDTO.NoRecord > 0)
                    {
                        MatureAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MaturedAmount));

                        if (RoundingByFlag.Text == "1")
                        {
                            double NoRBy = Converter.GetDouble(NoRoundingBy.Text);
                            double MAmount = Converter.GetDouble(MatureAmt.Text);
                            double NMAmount = Converter.GetDouble(MAmount * NoRBy);
                            MatureAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", NMAmount));
                        }
                    }


                    string strQuery = "UPDATE A2ZACCOUNT SET  AccMatureDate= '" + Matdate + "',AccMatureAmt= '" + MatureAmt.Text + "',AccPeriod= '" + WAccPeriod + "',AccTotalDep= '" + totdeposit + "' WHERE  Id='" + Id + "' ";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                }


                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc14", "A2ZCSMCUS"));
                //if (result == 0)
                //{

                //}

            }
        }
        private void A2ZACC15()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=15";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    lblProcessing.Text = "A2ZACC15()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();



                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntType,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTYPE,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCLASTDT,ACCPRINCIPAL,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO FROM A2ZCCULB.dbo.A2ZACC15 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 15 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}

        }

        private void OA2ZACC15()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntType,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTYPE,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCLASTDT,ACCPRINCIPAL,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC15 WHERE CUNO = '" + OCuNo + "'", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 15 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 15 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC15()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntType,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTYPE,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCLASTDT,ACCPRINCIPAL,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC15 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 15 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 15 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }



   
        private void XA2ZACC15()
        {

            string qry1 = "SELECT Id,AccRenwlDate,AccNoAnni,AccNoRenwl,AccPeriod,AccBalance FROM A2ZACCOUNT where AccType = 15";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblProcessing.Text = "XA2ZACC15()";

                foreach (DataRow dr1 in dt1.Rows)
                {

                    var ParentId = dr1["Id"].ToString();
                    var RenwlDate = dr1["AccRenwlDate"].ToString();
                    var accNoAnni = dr1["AccNoAnni"].ToString();
                    var accNoRenwl = dr1["AccNoRenwl"].ToString();
                    var accPeriod = dr1["AccPeriod"].ToString();
                    var accBalance = dr1["AccBalance"].ToString();

                    int Id = Converter.GetInteger(ParentId);

                    int NoAnni = Converter.GetInteger(accNoAnni);
                    int NoRenwl = Converter.GetInteger(accNoRenwl);
                    int Period = Converter.GetInteger(accPeriod);

                    double Balance = Converter.GetDouble(accBalance);


                    if (NoRenwl == 0 && NoAnni == 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  AccRenwlDate = NULL,AccAnniDate = NULL WHERE  Id='" + Id + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }

                    if (NoRenwl == 0 && NoAnni != 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  AccRenwlDate = NULL, AccAnniDate ='" + RenwlDate + "' where  Id='" + Id + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }

                    if ((NoRenwl != 0 && NoAnni == 0) || Period < 13)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  AccAnniDate = NULL,AccNoAnni = 0 where  Id='" + Id + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }

                    if (Balance == 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  AccStatus = 99 where  Id='" + Id + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }

                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc15", "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            }

        }

        private void A2ZACC16()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=16";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC16()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccNoAnni,AccNoRenwl,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCRENWDT,ACCLASTDT,ACCPRINCIPAL,ACCNOFANNI,ACCNOFRENW,CUNO FROM A2ZCCULB.dbo.A2ZACC16 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 16 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}



        }


        private void OA2ZACC16()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccNoAnni,AccNoRenwl,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCRENWDT,ACCLASTDT,ACCPRINCIPAL,ACCNOFANNI,ACCNOFRENW,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC16 WHERE CUNO = '" + OCuNo + "'", con);



                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 16 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 16 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC16()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccCertNo,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccIntFlag,AccMatureDate,AccIntRate,AccPrincipal,AccOrgAmt,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccNoAnni,AccNoRenwl,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCINTFLAG,ACCMATDT,ACCINTRATE,ACCPRINCIPAL,ACCORGAMT,ACCRENWDT,ACCLASTDT,ACCPRINCIPAL,ACCNOFANNI,ACCNOFRENW,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC16 WHERE CUNO = '" + OCuNo + "'", con);




                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 16 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND  CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 16 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

      
        private void XA2ZACC16()
        {

            string qry1 = "SELECT Id,AccOpenDate,AccRenwlDate,AccNoAnni,AccNoRenwl,AccIntRate,AccBalance FROM A2ZACCOUNT where AccType = 16";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblProcessing.Text = "XA2ZACC16()";

                foreach (DataRow dr1 in dt1.Rows)
                {

                    var ParentId = dr1["Id"].ToString();
                    var OpenDate = dr1["AccOpenDate"].ToString();
                    var RenwlDate = dr1["AccRenwlDate"].ToString();
                    var accNoAnni = dr1["AccNoAnni"].ToString();
                    var accNoRenwl = dr1["AccNoRenwl"].ToString();
                    var accIntRate = dr1["AccIntRate"].ToString();
                    var accBalance = dr1["AccBalance"].ToString();


                    int Id = Converter.GetInteger(ParentId);

                    int NoAnni = Converter.GetInteger(accNoAnni);
                    int NoRenwl = Converter.GetInteger(accNoRenwl);
                    int IntRate = Converter.GetInteger(accIntRate);
                    double Balance = Converter.GetDouble(accBalance);


                    string strQuery = "UPDATE A2ZACCOUNT SET  AccIntRate = '12.25' WHERE Id='" + Id + "'and AccOpenDate < '2014-07-01' and AccIntRate = '0' ";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    if (NoRenwl == 0)
                    {
                        string strQuery1 = "UPDATE A2ZACCOUNT SET  AccRenwlDate = NULL WHERE  Id='" + Id + "' ";
                        int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));
                    }

                    if (NoAnni == 0)
                    {

                        string strQuery2 = "UPDATE A2ZACCOUNT SET  AccAnniDate = NULL WHERE  Id='" + Id + "' ";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));

                    }

                    if (Balance == 0)
                    {
                        string strQuery3 = "UPDATE A2ZACCOUNT SET  AccStatus = 99 where  Id='" + Id + "' ";
                        int rowEffect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery3, "A2ZCSMCUS"));
                    }

                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc16A", "A2ZCSMCUS"));
                if (result == 0)
                {
                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc16", "A2ZCSMCUS"));
                }
            }



        }

        private void A2ZACC17()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=17";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC17()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccMatureDate,AccIntType,AccPrincipal,AccOrgAmt,AccFixedAmt,AccFixedMthInt,AccBenefitDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccTotIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCMATDT,ACCINTYPE,ACCPRINCIPAL,ACCORGAMT,ACCORGAMT,ACCINTRATE,ACCINTDT,ACCRENWAMT,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCINTWDRLT,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO FROM A2ZCCULB.dbo.A2ZACC17 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 17 AND  CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }


            //}

        }

        private void OA2ZACC17()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccMatureDate,AccIntType,AccPrincipal,AccOrgAmt,AccFixedAmt,AccFixedMthInt,AccBenefitDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccTotIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCMATDT,ACCINTYPE,ACCPRINCIPAL,ACCORGAMT,ACCORGAMT,ACCINTRATE,ACCINTDT,ACCRENWAMT,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCINTWDRLT,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC17 WHERE CUNO = '" + OCuNo + "'", con);




                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 17 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 17 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC17()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccPeriod,AccMatureDate,AccIntType,AccPrincipal,AccOrgAmt,AccFixedAmt,AccFixedMthInt,AccBenefitDate,AccRenwlAmt,AccPrevRenwlAmt,AccIntWdrawn,AccPrevIntWdrawn,AccTotIntWdrawn,AccNoAnni,AccNoRenwl,AccLastIntCr,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCPERIOD,ACCMATDT,ACCINTYPE,ACCPRINCIPAL,ACCORGAMT,ACCORGAMT,ACCINTRATE,ACCINTDT,ACCRENWAMT,ACCPRENWAMT,ACCINTWDRL,ACCPINTWDRL,ACCINTWDRLT,ACCNOFANNI,ACCNOFRENW,ACCLASTINTCR,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC17 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 17 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 17 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


       
        private void XA2ZACC17()
        {

            string qry1 = "SELECT Id,AccFixedAmt,AccFixedMthInt FROM A2ZACCOUNT where AccType = 17";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblProcessing.Text = "XA2ZACC17()";

                foreach (DataRow dr1 in dt1.Rows)
                {

                    var ParentId = dr1["Id"].ToString();
                    var FixedAmt = dr1["AccFixedAmt"].ToString();
                    var FixedMthInt = dr1["AccFixedMthInt"].ToString();



                    int Id = Converter.GetInteger(ParentId);


                    double WFixedAmt = Converter.GetDouble(FixedAmt);
                    double WFixedMthInt = Converter.GetDouble(FixedMthInt);


                    WFixedMthInt = ((WFixedMthInt * WFixedAmt) / 100000);


                    string strQuery = "UPDATE A2ZACCOUNT SET  AccFixedMthInt= '" + WFixedMthInt + "' WHERE  Id='" + Id + "' ";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                }

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc17", "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            }



        }

        private void A2ZACC18()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=18";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{


                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        lblProcessing.Text = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC18 WHERE CUNO = '" + CuNo + "'", con);

                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 18 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }



        private void OA2ZACC18()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC18 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  AccType = 18 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                    con.Close();
                }
            }
        }

        private void O5000A2ZACC18()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC18 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string qry4 = "SELECT MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "',MemNo = '" + MemNo + "' WHERE AccType = 18 AND CuNo='" + OCuNo + "' AND MemNo='" + OldMemNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void TRFA2ZACC18()
        {
            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblProcessing.Text = "A2ZACC18()";


                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC18 WHERE CUNO='" + CuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=18 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }


                            }
                            //else 
                            //{
                            //    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                            //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));



                            //}


                        }
                    }



                }
            }
        }




        private void TRFOA2ZACC18()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC18  WHERE CUNO='" + OCuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=18 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=18 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                        }



                    }
                }
            }
        }


      
        private void TRFO5000A2ZACC18()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblProcessing.Text = "O5000A2ZACC18()";


                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC18  WHERE CUNO='" + OCuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=18 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }
                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=18 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                        }



                    }
                }
            }
        }
        
        private void A2ZACC20()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=20";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC20()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC20 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 20 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC20()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC20 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 20 AND  CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 20 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC20()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC20 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 20 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 20 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        
        private void A2ZACC21()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=21";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC21()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC21 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 21 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC21()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC21 WHERE CUNO = '" + OCuNo + "'", con);



                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 21 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                       
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemNo !=0";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld1MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 21 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC21()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC21 WHERE CUNO = '" + OCuNo + "'", con);



                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 21 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                       
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "'";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld2MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 21 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }
        
        private void A2ZACC23()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=23";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{


                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        lblProcessing.Text = dr["CuNo"].ToString();


                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC23 WHERE CUNO = '" + CuNo + "'", con);


                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 23 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }



        private void OA2ZACC23()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC23 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "' WHERE  AccType = 23 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }
                    con.Close();
                }
            }
        }

        private void O5000A2ZACC23()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    lblProcessing.Text = dr["CuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC23 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string qry4 = "SELECT MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                       
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "',CuNo = '" + CuNo + "',MemNo = '" + MemNo + "' WHERE AccType = 23 AND CuNo='" + OCuNo + "' AND MemNo='" + OldMemNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        private void TRFA2ZACC23()
        {
            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblProcessing.Text = "A2ZACC23()";

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC23 WHERE CUNO='" + CuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=23 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                            //else
                            //{
                            //    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                            //    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));



                            //}


                        }
                    }



                }
                //    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC23 WHERE CUNO = '" + CuNo + "'", con);
                //    con.Open();

                //    int result2 = cmd1.ExecuteNonQuery();
                //    if (result2 > 0)
                //    {
                //        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  CuNo='" + CuNo + "' ";
                //        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                //    }
                //    con.Close();
                //}
            }
        }


        private void TRFOA2ZACC23()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC23  WHERE CUNO='" + OCuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=23 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=23 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                        }



                    }
                }
            }
        }

        
        private void TRFO5000A2ZACC23()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                lblProcessing.Text = "O5000A2ZACC23()";

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    string qry1 = "SELECT CUNO,MEMNO,ACCBALANCE,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCOPBAL,ACCLASTDT FROM A2ZACC23  WHERE CUNO='" + OCuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var CNo = dr1["CUNO"].ToString();
                            var MNo = dr1["MEMNO"].ToString();
                            var AccBalance = dr1["ACCBALANCE"].ToString();
                            double oldbalance = Converter.GetDouble(AccBalance);
                            var AccOpenDt = dr1["ACCOPENDT"].ToString();
                            var AccSpDesc = dr1["ACCSPDESC"].ToString();
                            var AccStat = dr1["ACCSTAT"].ToString();
                            var AccStatDt = dr1["ACCSTATDT"].ToString();
                            var AccPStat = dr1["ACCPSTAT"].ToString();
                            var AccOpBal = dr1["ACCOPBAL"].ToString();
                            var AcclastDt = dr1["ACCLASTDT"].ToString();

                            string qry2 = "SELECT * FROM A2ZACCOUNT WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                            if (dt2.Rows.Count > 0)
                            {
                                double updatebal = 0;
                                double nbalance = Converter.GetDouble(dt2.Rows[0]["AccBalance"]);
                                updatebal = oldbalance + nbalance;

                                string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "', AccBalance='" + updatebal + "' WHERE AccType=12 AND MemNo=0 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "' ";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=23 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                            else
                            {
                                string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) VALUES(12,0,'" + CuType + "','" + CuNo + "',0,'" + AccOpenDt + "','" + AccSpDesc + "','" + AccStat + "','" + AccStatDt + "','" + AccPStat + "','" + oldbalance + "','" + AccOpBal + "','" + AcclastDt + "','" + CNo + "')";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                                string delqry = "DELETE FROM  A2ZACCOUNT WHERE AccType=23 AND MemNo=0 AND CuType = '" + CuType + "' AND CuNo='" + CuNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
                                if (row1Effect > 0)
                                {

                                }

                            }
                        }


                    }
                }
            }
        }
        private void A2ZACC24()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=24";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC24()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO FROM A2ZCCULB.dbo.A2ZACC24 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 24 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }


        private void OA2ZACC24()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC24 WHERE CUNO = '" + OCuNo + "'", con);




                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 24 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 24 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC24()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC24 WHERE CUNO = '" + OCuNo + "'", con);




                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 24 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 24 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }
        
        private void A2ZACC51()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=51";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC51()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();

                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC51 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 51 AND  CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC51()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC51 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 51 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 51 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC51()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC51 WHERE CUNO = '" + OCuNo + "'", con);





                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 51 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 51 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAcc51", "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            }
        }


        private void TRFA2ZACC54()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=54";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            
            string qry = "SELECT CuType,CuNo FROM WFLIQLOAN";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
               


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();

                    string qry1 = "SELECT CuType,CuNo,AccType FROM A2ZACCOUNT WHERE CuType='" + CType + "' AND CuNo='" + CNo + "' AND AccType=51";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var cutype = dr1["CuType"].ToString();
                            var cuno = dr1["CuNo"].ToString();
                            var acctype = dr1["AccType"].ToString();

                            
                            string strQuery = "UPDATE A2ZACCOUNT SET  AccType = 54 WHERE AccType=51 AND MemNo=0 AND CuType='" + cutype + "' AND CuNo='" + cuno + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        }
                    }
                }
            }
        }

        private void TRFA2ZACC61()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=61";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            
            string qry = "SELECT CuType,CuNo FROM WFHBLOAN";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
               
                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();

                    string qry1 = "SELECT CuType,CuNo,AccType FROM A2ZACCOUNT WHERE CuType='" + CType + "' AND CuNo='" + CNo + "' AND AccType=51";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var cutype = dr1["CuType"].ToString();
                            var cuno = dr1["CuNo"].ToString();
                            var acctype = dr1["AccType"].ToString();

                            string strQuery = "UPDATE A2ZACCOUNT SET  AccType = 61 WHERE AccType=51 AND MemNo=0 AND CuType='" + cutype + "' AND CuNo='" + cuno + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        }
                    }
                }
            }
        }

        private void A2ZACC52()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=52";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{


                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC52()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + CuNo + "' AND (MEMNO NOT BETWEEN 9001 AND 9197) AND (MEMNO NOT BETWEEN 9701 AND 9745)", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            //string qry4 = "SELECT MemNo FROM A2ZACC52 WHERE  CUNO='" + CuNo + "'";
                            //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCCULB");
                            //if (dt4.Rows.Count > 0)
                            //{
                            //    foreach (DataRow dr2 in dt4.Rows)
                            //    {

                            //        var MemNo = dr2["MemNo"].ToString();


                                    string str1Query = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 52 AND CuNo='" + CuNo + "'";
                                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                                //}
                            //}

                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC52()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO NOT BETWEEN 9001 AND 9197) AND (MEMNO NOT BETWEEN 9701 AND 9745)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 52 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 52 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC52()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO NOT BETWEEN 9001 AND 9197) AND (MEMNO NOT BETWEEN 9701 AND 9745)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 52 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MMemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MMemNo + "' WHERE AccType = 52 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        private void A2ZACC55()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=55";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{


            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                
                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + CuNo + "' AND MEMNO BETWEEN 9001 AND 9197", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string qry4 = "SELECT MemNo FROM A2ZACC52 WHERE  CUNO='" + CuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCCULB");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {

                        //        var MemNo = dr2["MemNo"].ToString();


                        string str1Query = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 55 AND CuNo='" + CuNo + "'";
                        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //}
                        //}

                    }
                    con.Close();
                }
            }
            //}
        }

        private void OA2ZACC55()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND MEMNO BETWEEN 9001 AND 9197", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 55 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld1MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 55 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC55()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND MEMNO BETWEEN 9001 AND 9197", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 55 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MMemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld2MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MMemNo + "' WHERE AccType = 55 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }

        private void A2ZACC58()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=58";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{


            string qry = "SELECT CuType,CuNo FROM WFCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + CuNo + "' AND MEMNO BETWEEN 9701 AND 9745", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string qry4 = "SELECT MemNo FROM A2ZACC52 WHERE  CUNO='" + CuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCCULB");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {

                        //        var MemNo = dr2["MemNo"].ToString();


                        string str1Query = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 58 AND CuNo='" + CuNo + "'";
                        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //}
                        //}

                    }
                    con.Close();
                }
            }
            //}
        }

        private void OA2ZACC58()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND MEMNO BETWEEN 9701 AND 9745", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 58 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld1MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 58 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC58()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND MEMNO BETWEEN 9701 AND 9745", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 58 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        ////DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        ////if (dt4.Rows.Count > 0)
                        ////{
                        ////    foreach (DataRow dr2 in dt4.Rows)
                        ////    {
                        ////        var CT = dr2["CuType"].ToString();
                        ////        var CN = dr2["CuNo"].ToString();
                        ////        var MMemNo = dr2["MemNo"].ToString();
                        ////        var OldMemNo = dr2["MemOld2MemNo"].ToString();

                        ////        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MMemNo + "' WHERE AccType = 58 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        ////        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        ////    }
                        ////}
                    }
                    con.Close();
                }
            }
        }



       


        private void A2ZACC53()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=53";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC53()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC53 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 53 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC53()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC53 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 53 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 53 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC53()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC53 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 53 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "' ";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 53 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void InsertA2ZACC53()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=53";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{

            string qry = "SELECT CuType,CuNo,OldCuNo,LoanNo,DisbAmt,DisbDate,LastTranDate,NoInstl,InstlAmt,AccBalance FROM WFTEACHERSLOAN";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCNo = dr["OldCuNo"].ToString();
                    var loanNo = dr["LoanNo"].ToString();
                    var disbAmt = dr["DisbAmt"].ToString();
                    var disbDate = dr["DisbDate"].ToString();
                    var LTranDate = dr["LastTranDate"].ToString();
                    var noInstl = dr["NoInstl"].ToString();
                    var instlAmt = dr["InstlAmt"].ToString();
                    var accBalance = dr["AccBalance"].ToString();

                    double ABalance = Converter.GetDouble(accBalance);

                    double Balance = (0 - ABalance);

                    double a = Converter.GetDouble(disbAmt);
                    double b = Converter.GetDouble(noInstl);
                    double c = a / b;

                    c = Math.Round(c);

                    double d = Math.Abs((c * (b - 1)) - a);

                    
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccBalance,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,AccAtyClass,OldCuNo) VALUES (53,'" + loanNo + "','" + CType + "','" + CNo + "',0,'" + disbDate + "',1,'" + Balance + "','" + LTranDate + "','" + noInstl + "','" + c + "','" + d + "','" + disbAmt + "','" + disbDate + "','" + disbAmt + "','" + disbDate + "',6,'" + OCNo + "')", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        
                    }
                    con.Close();
                }
            }

        }

        private void IntRateA2ZACC53()
        {

            string qry = "SELECT CuNo,IntRate FROM WFTEACHERSLOANINTRATE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CuNo = dr["CuNo"].ToString();
                    var IRate = dr["IntRate"].ToString();

                    string qry2 = "SELECT Id,AccNo FROM A2ZACCOUNT WHERE CuNo='" + CuNo + "' AND AccType=53";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            var ParentId = dr2["Id"].ToString();
                            var OldAcc = dr2["AccNo"].ToString();

                            lblId.Text = Converter.GetString(ParentId);

                            string strQuery = "UPDATE A2ZACCOUNT SET  AccIntRate = '" + IRate + "' WHERE  Id='" + lblId.Text + "'";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        }
                    }
                }
            }
          
        }


        private void A2ZACC54()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=54";
            int row11Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row11Effect > 0)
            //{

                string qry = "SELECT CuType,CuNo FROM WFCUNO";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    lblProcessing.Text = "A2ZACC54()";

                    foreach (DataRow dr in dt.Rows)
                    {
                        var CuType = dr["CuType"].ToString();
                        var CuNo = dr["CuNo"].ToString();
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC54 WHERE CUNO = '" + CuNo + "'", con);
                        con.Open();

                        int result2 = cmd1.ExecuteNonQuery();
                        if (result2 > 0)
                        {
                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE  AccType = 54 AND CuNo='" + CuNo + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                        con.Close();
                    }
                }
            //}
        }

        private void OA2ZACC54()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFOCUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC54 WHERE CUNO = '" + OCuNo + "'", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 54 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld1MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 54 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }


        private void O5000A2ZACC54()
        {
            string qry = "SELECT CuType,CuNo,OCuNo FROM WFO5000CUNO";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var OCuNo = dr["OCuNo"].ToString();


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC54 WHERE CUNO = '" + OCuNo + "'", con);


                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 54 AND CuNo='" + OCuNo + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        if (dt4.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt4.Rows)
                            {
                                var CT = dr2["CuType"].ToString();
                                var CN = dr2["CuNo"].ToString();
                                var MemNo = dr2["MemNo"].ToString();
                                var OldMemNo = dr2["MemOld2MemNo"].ToString();

                                string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 54 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        
        private void InsertAcc99()
        {
            string qry1 = "SELECT Id,CuType,CuNo,MemNo,MemOpenDate FROM A2ZMEMBER WHERE MemNo='" + "0" + "'";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {
                    var ParentId = dr1["Id"].ToString();
                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var OpenDt = dr1["MemOpenDate"].ToString();
                    Int16 AccType = 99;
                    int AccNo = 0;
                    Int16 AccStatus = 1;
                    Int16 AccAtyClass = 7;

                    string qry = "INSERT INTO A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccAtyClass,AccOpenDate,AccStatus)values('" + AccType + "','" + AccNo + "','" + CuType + "','" + CuNo + "','" + MemNo + "','" + AccAtyClass + "','" + OpenDt + "','" + AccStatus + "')";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(qry, con);
                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                    }
                    con.Close();

                    //A2ZACCOUNTDTO objDTO = new A2ZACCOUNTDTO();
                    ////  objDTO.Id = Converter.GetSmallInteger(ParentId);
                    //objDTO.CuType = Converter.GetSmallInteger(CuType);
                    //objDTO.CuNo = Converter.GetInteger(CuNo);
                    //objDTO.MemberNo = Converter.GetInteger(MemNo);
                    //objDTO.AccType = Converter.GetSmallInteger(99);
                    //objDTO.AccNo = Converter.GetInteger(0);
                    //objDTO.Opendate = Converter.GetDateTime(OpenDt);
                    //objDTO.AccAtyClass = 6;

                    //int row2 = A2ZACCOUNTDTO.InsertInformation(objDTO);

                }
            }
        }

        private void GenerateAccNo()
        {
            int OrgAccType = 0;
            string qry1;
            DataTable dt1; 
            
            if (ddlAccType.Text == "1")
            {
                OrgAccType = 11;
            }
            else if (ddlAccType.Text == "2")
            {
                OrgAccType = 12;
            }
            else if (ddlAccType.Text == "3")
            {
                OrgAccType = 13;
            }
            else if (ddlAccType.Text == "4")
            {
                OrgAccType = 14;
            }
            else if (ddlAccType.Text == "5")
            {
                OrgAccType = 15;
            }
            else if (ddlAccType.Text == "6")
            {
                OrgAccType = 16;
            }
            else if (ddlAccType.Text == "7")
            {
                OrgAccType = 17;
            }
            else if (ddlAccType.Text == "8")
            {
                OrgAccType = 18;
            }
            else if (ddlAccType.Text == "10")
            {
                OrgAccType = 20;
            }
            else if (ddlAccType.Text == "11")
            {
                OrgAccType = 21;
            }
            else if (ddlAccType.Text == "12")
            {
                OrgAccType = 23;
            }
            else if (ddlAccType.Text == "13")
            {
                OrgAccType = 24;
            }
            else if (ddlAccType.Text == "14")
            {
                OrgAccType = 99;
            }
            else if (ddlAccType.Text == "15")
            {
                OrgAccType = 52;
            }
            else if (ddlAccType.Text == "16")
            {
                OrgAccType = 53;
            }
            else if (ddlAccType.Text == "18")
            {
                OrgAccType = 55;
            }
            else if (ddlAccType.Text == "19")
            {
                OrgAccType = 58;
            }

            if (ddlAccType.Text == "0")
            {
                qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT group by CuType,CuNo,MemNo,AccType";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            }
            else if (ddlAccType.Text == "14")
            {
                qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT group by CuType,CuNo,MemNo,AccType WHERE AccType=51 or AccType=54 or AccType=64"; 
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            }
            else
            {
                qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT WHERE AccType='" + OrgAccType + "' group by CuType,CuNo,MemNo,AccType";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            }


            if (dt1.Rows.Count > 0)
            {
                lblProcessing.Text = "GenerateAccNo()";

                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuType = dr1["CuType"].ToString();
                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();

                    lblCuType.Text = Converter.GetString(CuType);
                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAcType.Text = Converter.GetString(AccType);

                    int newaccno = 0;

                    string qry2 = "SELECT Id,AccNo FROM A2ZACCOUNT WHERE CuType='" + CuType + "' AND CuNo='" + CuNo + "' AND MemNo='" + MemNo + "' AND AccType='" + AccType + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            var ParentId = dr2["Id"].ToString();
                            var OldAcc = dr2["AccNo"].ToString();

                            lblId.Text = Converter.GetString(ParentId);

                            newaccno = newaccno + 1;
                            lblAccNo.Text = Converter.GetString(newaccno);

                            GenerateNewAccNo();
                            string strQuery = "UPDATE A2ZACCOUNT SET  AccNo = '" + txtAccNo.Text + "',AccOldNumber = '" + OldAcc + "' WHERE  Id='" + lblId.Text + "'";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                        }
                    }

                }
            }
        }

        protected void GenerateNewAccNo()
        {

            string input1 = Converter.GetString(lblCuNum.Text).Length.ToString();
            string input2 = Converter.GetString(lblMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(lblAccNo.Text).Length.ToString();

            string result1 = "";
            string result2 = "";
            string result3 = "";

            if (input1 == "1")
            {
                result1 = "000";
            }
            if (input1 == "2")
            {
                result1 = "00";
            }
            if (input1 == "3")
            {
                result1 = "0";
            }

            if (input2 == "1")
            {
                result2 = "0000";
            }
            if (input2 == "2")
            {
                result2 = "000";
            }
            if (input2 == "3")
            {
                result2 = "00";
            }
            if (input2 == "4")
            {
                result2 = "0";
            }

            if (input3 == "1")
            {
                result3 = "000";
            }
            if (input3 == "2")
            {
                result3 = "00";
            }
            if (input3 == "3")
            {
                result3 = "0";
            }

            if (input1 != "4" && input2 != "5" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + lblMemNo.Text + result3 + lblAccNo.Text;
            }

            if (input1 != "4" && input2 != "5" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + result1 + lblCuNum.Text + result2 + lblMemNo.Text + lblAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + result1 + lblCuNum.Text + lblMemNo.Text + result3 + lblAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + result1 + lblCuNum.Text + lblMemNo.Text + lblAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + lblCuNum.Text + result2 + lblMemNo.Text + result3 + lblAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + lblCuNum.Text + result2 + lblMemNo.Text + lblAccNo.Text;
            }

            if (input1 == "4" && input2 == "5" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + lblCuNum.Text + lblMemNo.Text + result3 + lblAccNo.Text;
            }
            if (input1 == "4" && input2 == "5" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + lblCuType.Text + lblCuNum.Text + lblMemNo.Text + lblAccNo.Text;
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

        private void SearchMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Search Completed');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }

        private void DeleteMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Delete completed');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }

        protected void ddlAccTupe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrunTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            string sqlquery4 = "Truncate table dbo.WFMISSMEM";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));

            SA2ZACC11();

            SA2ZACC12();

            SA2ZACC13();

            SA2ZACC14();

            SA2ZACC15();

            SA2ZACC16();

            SA2ZACC17();

            SA2ZACC18();

            SA2ZACC20();

            SA2ZACC21();

            SA2ZACC23();

            SA2ZACC24();

            SA2ZACC51();

            SA2ZACC52();

            SA2ZACC53();

            SA2ZACC54();

            SearchMSG();

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            DELA2ZACC11();

            DELA2ZACC12();

            DELA2ZACC13();

            DELA2ZACC14();

            DELA2ZACC15();

            DELA2ZACC16();

            DELA2ZACC17();

            DELA2ZACC18();

            DELA2ZACC20();

            DELA2ZACC21();

            DELA2ZACC23();

            DELA2ZACC24();

            DELA2ZACC51();

            DELA2ZACC52();

            DELA2ZACC53();

            DELA2ZACC54();

            DeleteMSG();
        }


        private void SA2ZACC11()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC11";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }


            }

        }

        private void DELA2ZACC11()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=11";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC11 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }


        private void SA2ZACC12()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC12";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC12()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=12";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC12 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC13()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC13";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC13()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=13";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC13 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC14()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC14";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC14()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=14";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC14 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC15()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC15";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }


        private void DELA2ZACC15()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=15";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC15 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }
        private void SA2ZACC16()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC16";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC16()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=16";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC16 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC17()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC17";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC17()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=17";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC17 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC18()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC18";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC18()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=18";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC18 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC20()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC20";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC20()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=20";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC20 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC21()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC21";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC21()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=21";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC21 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC23()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC23";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC23()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=23";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC23 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC24()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC24";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }


        private void DELA2ZACC24()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=24";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC24 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }
        private void SA2ZACC51()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC51";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC51()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=51";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC51 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC52()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC52";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC52()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=52";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC52 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC53()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC53";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC53()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=53";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC53 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

        private void SA2ZACC54()
        {
            string qry1 = "SELECT CUNO,MEMNO,ACCTYPE FROM A2ZACC54";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCCULB");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string qry2 = "SELECT * FROM A2ZMEMBER WHERE CuNo='" + CuNo + "' AND MemNo='" + MemNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCCULB");

                    if (dt2.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        string strQuery = @"INSERT into WFMISSMEM(AccType,CuNo,MemNo)values('" + lblAccType.Text + "','" + lblCuNum.Text + "','" + lblMemNo.Text + "')";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                    }


                }

            }

        }

        private void DELA2ZACC54()
        {
            string qry1 = "SELECT AccType,CuNo,MemNo FROM WFMISSMEM WHERE AccType=54";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {


                foreach (DataRow dr1 in dt1.Rows)
                {

                    var CuNo = dr1["CuNo"].ToString();
                    var MemNo = dr1["MemNo"].ToString();
                    var AccType = dr1["AccType"].ToString();


                    lblCuNum.Text = Converter.GetString(CuNo);
                    lblMemNo.Text = Converter.GetString(MemNo);
                    lblAccType.Text = Converter.GetString(AccType);


                    string delqry = "DELETE FROM A2ZACC54 WHERE AccType='" + AccType + "' AND CuNo='" + CuNo + "' AND  MemNo='" + MemNo + "'";
                    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCCULB"));
                    if (row1Effect > 0)
                    {
                    }
                }
            }
        }

       
        protected void BtnGenerateAccNo_Click(object sender, EventArgs e)
        {
            string qry = "SELECT Id,CuType,CuNo,CuOldCuNo FROM A2ZCUNION ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblProcessing.Text = "Last Process";

                foreach (DataRow dr in dt.Rows)
                {

                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var CuOldCuNo = dr["CuOldCuNo"].ToString();

                    int code = Converter.GetInteger(CuOldCuNo);
                    string qry1 = "SELECT Id,AccType,CuType,CuNo,OldCuNo FROM A2ZACCOUNT WHERE CuNo='" + CNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            var AccType = dr1["AccType"].ToString();
                            A2ZACCOUNTDTO objDTO = new A2ZACCOUNTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);

                            objDTO.CuType = Converter.GetSmallInteger(CType);
                            objDTO.CuNo = Converter.GetInteger(CNo);

                            if (AccType == "11")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "12")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "13")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "18")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "19")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "20")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "21")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "23")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "24")
                            {
                                objDTO.AccAtyClass = 1;
                            }
                            if (AccType == "14")
                            {
                                objDTO.AccAtyClass = 4;
                            }
                            if (AccType == "15")
                            {
                                objDTO.AccAtyClass = 2;
                            }
                            if (AccType == "16")
                            {
                                objDTO.AccAtyClass = 2;
                            }
                            if (AccType == "17")
                            {
                                objDTO.AccAtyClass = 3;
                            }
                            if (AccType == "52")
                            {
                                objDTO.AccAtyClass = 5;
                            }

                            if (AccType == "51")
                            {
                                objDTO.AccAtyClass = 6;
                            }
                            if (AccType == "53")
                            {
                                objDTO.AccAtyClass = 6;
                            }

                            if (AccType == "54")
                            {
                                objDTO.AccAtyClass = 6;
                            }

                            if (AccType == "61")
                            {
                                objDTO.AccAtyClass = 6;
                            }

                            int row2 = A2ZACCOUNTDTO.Update1(objDTO);

                        }
                    }
                }
            }


            GenerateAccNo();
            Successful();
        }



    }
}