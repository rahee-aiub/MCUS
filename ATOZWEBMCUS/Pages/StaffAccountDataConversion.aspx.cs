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
    public partial class StaffAccountDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlAccType.Focus();

        }






        protected void BtnProceed_Click(object sender, EventArgs e)
        {



            if (ddlAccType.Text == "1" || ddlAccType.Text == "0")
            {
                A2ZACC55();
                OA2ZACC55();
                O5000A2ZACC55();
                TrfServiceLoanUpd();

            }

            if (ddlAccType.Text == "2" || ddlAccType.Text == "0")
            {
                A2ZACC58();
                OA2ZACC58();
                O5000A2ZACC58();
                TrfComputerLoanUpd();

            }

            if (ddlAccType.Text == "3" || ddlAccType.Text == "0")
            {
                TrfMotorCycleLoanUpd();
            }

            if (ddlAccType.Text == "4" || ddlAccType.Text == "0")
            {
                TrfByCycleLoanUpd();
            }



            //GenerateAccNo();

            Successful();


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

                lblProcessing.Text = "A2ZACC52()";

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + CuNo + "' AND (MEMNO BETWEEN 9001 AND 9197)", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {



                        //string str1Query = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 55 AND CuNo='" + CuNo + "'";
                        //int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));

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

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO BETWEEN 9001 AND 9197)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 55 AND CuNo='" + OCuNo + "' ";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {
                        //        var CT = dr2["CuType"].ToString();
                        //        var CN = dr2["CuNo"].ToString();
                        //        var MemNo = dr2["MemNo"].ToString();
                        //        var OldMemNo = dr2["MemOld1MemNo"].ToString();

                        //        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 55 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        //        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //    }
                        //}
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


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 55,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO BETWEEN 9001 AND 9197)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 55 AND CuNo='" + OCuNo + "' ";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {
                        //        var CT = dr2["CuType"].ToString();
                        //        var CN = dr2["CuNo"].ToString();
                        //        var MMemNo = dr2["MemNo"].ToString();
                        //        var OldMemNo = dr2["MemOld2MemNo"].ToString();

                        //        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MMemNo + "' WHERE AccType = 55 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        //        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //    }
                        //}
                    }
                    con.Close();
                }
            }
        }


        private void TrfServiceLoanUpd()
        {

            string qry2 = "SELECT Id,AccType,MemNo FROM A2ZACCOUNT WHERE AccType = '55'";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt2.Rows)
                {
                    var ParentId = dr1["Id"].ToString();
                    var AccType = dr1["AccType"].ToString();
                    var MemNo = dr1["MemNo"].ToString();

                    int Id = Converter.GetInteger(ParentId);
                    int acctype = Converter.GetInteger(AccType);
                    int memno = Converter.GetInteger(MemNo);

                    string qry3 = "SELECT NewNo,Amount FROM wfServiceLoan WHERE OldNo = '" + memno + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");
                    if (dt3.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt3.Rows)
                        {
                            var NewNo = dr2["NewNo"].ToString();
                            var Amount = dr2["Amount"].ToString();

                            int newno = Converter.GetInteger(NewNo);
                            decimal amount = Converter.GetDecimal(Amount);

                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = 0, CuNo = 0, MemNo = '" + newno + "', AccLoanInstlAmt = '" + amount + "', AccAtyClass = 6 WHERE  Id='" + Id + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                    }
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

                lblProcessing.Text = "A2ZACC52()";

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + CuNo + "' AND (MEMNO BETWEEN 9701 AND 9749)", con);
                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {



                        //string str1Query = "UPDATE A2ZACCOUNT SET  CuType = '" + CuType + "' WHERE AccType = 58 AND CuNo='" + CuNo + "'";
                        //int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));


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

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO BETWEEN 9701 AND 9749)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 58 AND CuNo='" + OCuNo + "' ";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  MemOld1CuNo='" + OCuNo + "' AND MemNo !=0";
                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld1MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld1CuNo='" + OCuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {
                        //        var CT = dr2["CuType"].ToString();
                        //        var CN = dr2["CuNo"].ToString();
                        //        var MemNo = dr2["MemNo"].ToString();
                        //        var OldMemNo = dr2["MemOld1MemNo"].ToString();

                        //        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MemNo + "' WHERE AccType = 58 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        //        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //    }
                        //}
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


                    SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo,OldMemNo) SELECT 58,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO,MEMNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE CUNO = '" + OCuNo + "' AND (MEMNO BETWEEN 9701 AND 9749)", con);

                    con.Open();

                    int result2 = cmd1.ExecuteNonQuery();
                    if (result2 > 0)
                    {
                        //string strQuery = "UPDATE A2ZACCOUNT SET  CuType = '" + CType + "',CuNo = '" + CNo + "' WHERE  AccType = 58 AND CuNo='" + OCuNo + "' ";
                        //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                        ////string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  MemOld2CuNo='" + OCuNo + "'";
                        //string qry4 = "SELECT CuType,CuNo,MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE  CuType='" + CType + "' AND CuNo='" + CNo + "' AND MemOld2CuNo='" + OCuNo + "'";
                        //DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
                        //if (dt4.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr2 in dt4.Rows)
                        //    {
                        //        var CT = dr2["CuType"].ToString();
                        //        var CN = dr2["CuNo"].ToString();
                        //        var MMemNo = dr2["MemNo"].ToString();
                        //        var OldMemNo = dr2["MemOld2MemNo"].ToString();

                        //        string str1Query = "UPDATE A2ZACCOUNT SET  MemNo = '" + MMemNo + "' WHERE AccType = 58 AND CuType='" + CT + "' AND CuNo='" + CN + "' AND OldCuNo='" + OCuNo + "' AND OldMemNo='" + OldMemNo + "'";
                        //        int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                        //    }
                        //}
                    }
                    con.Close();
                }
            }
        }


        private void TrfComputerLoanUpd()
        {

            //string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=58";
            //int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry2 = "SELECT Id,AccType,MemNo FROM A2ZACCOUNT WHERE AccType = '58'";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt2.Rows)
                {
                    var ParentId = dr1["Id"].ToString();
                    var AccType = dr1["AccType"].ToString();
                    var MemNo = dr1["MemNo"].ToString();

                    int Id = Converter.GetInteger(ParentId);
                    int acctype = Converter.GetInteger(AccType);
                    int memno = Converter.GetInteger(MemNo);

                    string qry3 = "SELECT NewNo,Amount FROM wfComputerLoan WHERE OldNo = '" + memno + "'";
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");
                    if (dt3.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt3.Rows)
                        {
                            var NewNo = dr2["NewNo"].ToString();
                            var Amount = dr2["Amount"].ToString();

                            int newno = Converter.GetInteger(NewNo);
                            decimal amount = Converter.GetDecimal(Amount);

                            string strQuery = "UPDATE A2ZACCOUNT SET  CuType = 0, CuNo = 0, MemNo = '" + newno + "', AccLoanInstlAmt = '" + amount + "', AccAtyClass = 6 WHERE  Id='" + Id + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                        }
                    }
                }
            }

        }


        private void TrfMotorCycleLoanUpd()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=56";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));



            string qry = "SELECT GLACCNO,GLCALBAL FROM A2ZCGLMST WHERE (GLACCNO BETWEEN 101911 AND 102040)  AND GLACCNO!=101950  AND GLACCNO!=102000";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var GLAccNo = dr["GLACCNO"].ToString();
                    var GLBalance = dr["GLCALBAL"].ToString();
                    decimal accbalance = Converter.GetDecimal(GLBalance);
                    if (accbalance > 0)
                    {
                        accbalance = (0 - accbalance);
                    }
                    else
                    {
                        accbalance = Math.Abs(accbalance);
                    }


                    string qry2 = "SELECT NewNo,Amount,OpenDate,NoInstl FROM wfMotorCycleLoan WHERE OldNo = '" + GLAccNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            var NewNo = dr2["NewNo"].ToString();
                            var Amount = dr2["Amount"].ToString();
                            var Opendate = dr2["OpenDate"].ToString();
                            var Noinstl = dr2["NoInstl"].ToString();

                            int newno = Converter.GetInteger(NewNo);
                            decimal amount = Converter.GetDecimal(Amount);
                            int noinstl = Converter.GetInteger(Noinstl);

                            //DateTime opdate = DateTime.ParseExact(Opendate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                            string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,AccType,AccNo,MemNo,AccBalance,AccLoanInstlAmt,AccAtyClass,AccStatus,AccOpenDate,AccNoInstl) VALUES (0, 0, 56, 0, '" + newno + "', '" + accbalance + "','" + amount + "',6,1,'" + Opendate + "','" + noinstl + "')";
                            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                        }
                    }
                }
            }

        }

        private void TrfByCycleLoanUpd()
        {

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=57";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry = "SELECT GLACCNO,GLCALBAL FROM A2ZCGLMST WHERE (GLACCNO BETWEEN 101401 AND 101499) or (GLACCNO BETWEEN 199001 AND 199019)";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var GLAccNo = dr["GLACCNO"].ToString();
                    var GLBalance = dr["GLCALBAL"].ToString();
                    decimal accbalance = Converter.GetDecimal(GLBalance);
                    if (accbalance > 0)
                    {
                        accbalance = (0 - accbalance);
                    }
                    else
                    {
                        accbalance = Math.Abs(accbalance);
                    }

                    string qry2 = "SELECT NewNo,Amount,OpenDate,NoInstl  FROM wfByCycleLoan WHERE OldNo = '" + GLAccNo + "'";
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            var NewNo = dr2["NewNo"].ToString();
                            var Amount = dr2["Amount"].ToString();
                            var Opendate = dr2["OpenDate"].ToString();
                            var Noinstl = dr2["NoInstl"].ToString();

                            int newno = Converter.GetInteger(NewNo);
                            decimal amount = Converter.GetDecimal(Amount);
                            int noinstl = Converter.GetInteger(Noinstl);

                            //DateTime opdate = DateTime.ParseExact(Opendate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                            string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,AccType,AccNo,MemNo,AccBalance,AccLoanInstlAmt,AccAtyClass,AccStatus,AccOpenDate,AccNoInstl) VALUES (0, 0, 57, 0, '" + newno + "', '" + accbalance + "','" + amount + "',6,1,'" + Opendate + "','" + noinstl + "')";
                            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                        }
                    }
                }
            }

        }



        private void BalanceOpening55()
        {


           
            string delqry = "DELETE FROM A2ZCSOPBALANCE WHERE AccType=55";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUST2015"));

            DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZCSOPBALANCE(CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT CuType, CuNo, MemNo, AccType, AccNo, AccBalance, OldCuNo FROM A2ZCSMCUS.dbo.A2ZACCOUNT WHERE AccType=55";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if (result > 0)
            {
                string strqry = "UPDATE dbo.A2ZCSOPBALANCE SET TrnDate='" + opdate + "'";
                int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUST2015"));
                if (result2 > 0)
                {
                }

            }
        }


        private void BalanceOpening56()
        {



            string delqry = "DELETE FROM A2ZCSOPBALANCE WHERE AccType=56";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUST2015"));

            DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZCSOPBALANCE(CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT CuType, CuNo, MemNo, AccType, AccNo, AccBalance, OldCuNo FROM A2ZCSMCUS.dbo.A2ZACCOUNT WHERE AccType=56";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if (result > 0)
            {
                string strqry = "UPDATE dbo.A2ZCSOPBALANCE SET TrnDate='" + opdate + "'";
                int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUST2015"));
                if (result2 > 0)
                {
                }

            }
        }


        private void BalanceOpening57()
        {



            string delqry = "DELETE FROM A2ZCSOPBALANCE WHERE AccType=57";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUST2015"));

            DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZCSOPBALANCE(CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT CuType, CuNo, MemNo, AccType, AccNo, AccBalance, OldCuNo FROM A2ZCSMCUS.dbo.A2ZACCOUNT WHERE AccType=57";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if (result > 0)
            {
                string strqry = "UPDATE dbo.A2ZCSOPBALANCE SET TrnDate='" + opdate + "'";
                int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUST2015"));
                if (result2 > 0)
                {
                }

            }
        }


        private void BalanceOpening58()
        {



            string delqry = "DELETE FROM A2ZCSOPBALANCE WHERE AccType=58";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUST2015"));

            DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZCSOPBALANCE(CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT CuType, CuNo, MemNo, AccType, AccNo, AccBalance, OldCuNo FROM A2ZCSMCUS.dbo.A2ZACCOUNT WHERE AccType=58";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if (result > 0)
            {
                string strqry = "UPDATE dbo.A2ZCSOPBALANCE SET TrnDate='" + opdate + "'";
                int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUST2015"));
                if (result2 > 0)
                {
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


        protected void BtnGenerateAccNo_Click(object sender, EventArgs e)
        {
            GenerateAccNo();

            BalanceOpening55();
            BalanceOpening56();
            BalanceOpening57();
            BalanceOpening58();

            Successful();
        }


        private void GenerateAccNo()
        {
            string qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT group by CuType,CuNo,MemNo,AccType";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            if (dt1.Rows.Count > 0)
            {
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

                    if (lblAcType.Text == "55" ||
                        lblAcType.Text == "56" ||
                        lblAcType.Text == "57" ||
                        lblAcType.Text == "58")
                    {

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
                                string strQuery = "UPDATE A2ZACCOUNT SET  AccNo = '" + txtAccNo.Text + "' WHERE  Id='" + lblId.Text + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                            }
                        }
                    }
                }
            }
        }

        protected void GenerateNewAccNo()
        {


            string input2 = Converter.GetString(lblMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(lblAccNo.Text).Length.ToString();


            string result2 = "";
            string result3 = "";



            if (input2 == "1")
            {
                result2 = "000";
            }
            if (input2 == "2")
            {
                result2 = "00";
            }
            if (input2 == "3")
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

            if (input2 != "4" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + result2 + lblMemNo.Text + result3 + lblAccNo.Text;
            }
            if (input2 != "4" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + result2 + lblMemNo.Text + lblAccNo.Text;
            }
            if (input2 == "4" && input3 != "4")
            {
                txtAccNo.Text = lblAcType.Text + lblMemNo.Text + result3 + lblAccNo.Text;
            }
            if (input2 == "4" && input3 == "4")
            {
                txtAccNo.Text = lblAcType.Text + lblMemNo.Text + lblAccNo.Text;
            }

        }



    }
}