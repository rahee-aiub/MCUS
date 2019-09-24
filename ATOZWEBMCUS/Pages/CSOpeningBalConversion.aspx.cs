using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSOpeningBalConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZMCUST2015"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlAccType.Focus();
        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            if (ddlTrunTable.Text == "1")
            {
                string sqlquery4 = "Truncate table dbo.A2ZCSOPBALANCE ";
                int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZMCUST2015"));
            }


            if (ddlAccType.Text == "1" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE11();
            }

            if (ddlAccType.Text == "2" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE12();
            }

            if (ddlAccType.Text == "3" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE13();
            }

            if (ddlAccType.Text == "4" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE14();
            }

            if (ddlAccType.Text == "5" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE15();
            }

            if (ddlAccType.Text == "6" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE16();
            }

            if (ddlAccType.Text == "7" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE17();
            }

            if (ddlAccType.Text == "8" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE18();
            }

            if (ddlAccType.Text == "9" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE19();
            }

            if (ddlAccType.Text == "10" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE20();
            }

            if (ddlAccType.Text == "11" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE21();
            }


            if (ddlAccType.Text == "12" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE23();
            }

            if (ddlAccType.Text == "13" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE24();
            }

            if (ddlAccType.Text == "14" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE51();
            }

            if (ddlAccType.Text == "15" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE52();
            }

            if (ddlAccType.Text == "16" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE53();
            }

            if (ddlAccType.Text == "17" || ddlAccType.Text == "0")
            {
                A2ZOPBALANCE54();
            }

            string qry = "SELECT Id,CuType,CuNo,CuOldCuNo FROM A2ZCUNION ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var CType = dr["CuType"].ToString();
                    var CNo = dr["CuNo"].ToString();
                    var CuOldCuNo = dr["CuOldCuNo"].ToString();

                    int code = Converter.GetInteger(CuOldCuNo);
                    string qry1 = "SELECT Id,AccType,CuType,CuNo,CuOldNo FROM A2ZCSOPBALANCE WHERE CuOldNo='" + code + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZMCUST2015");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            var AccType = dr1["AccType"].ToString();
                            int Id = Converter.GetInteger(ParentId);
                            int CuType = Converter.GetInteger(CType);
                            int CuNo = Converter.GetInteger(CNo);

                           // if (AccType == "11" || AccType == "12" || AccType == "13" || AccType == "18" || AccType == "19" || AccType == "20" || AccType == "21" || AccType == "23" || AccType == "24")
                           // {
                           //     objDTO.AccAtyClass = 1;
                           // }
                           // if (AccType == "14")
                           // {
                           //     objDTO.AccAtyClass = 4;
                           // }
                           // if (AccType == "15" || AccType == "16")
                           // {
                           //     objDTO.AccAtyClass = 2;
                           // }
                           // if (AccType == "17")
                           // {
                           //     objDTO.AccAtyClass = 3;
                           // }
                           // if (AccType == "51" || AccType == "52" || AccType == "53" || AccType == "54")
                           // {
                           //     objDTO.AccAtyClass = 5;
                           // }
                           //// int row2 = A2ZACCOUNTDTO.Update1(objDTO);
                            string strQuery = "UPDATE A2ZCSOPBALANCE SET  CuType ='" +CuType + "',CuNo ='" + CuNo + "' where  Id='" + Id + "' ";
                            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZMCUST2015"));


                        }
                    }
                }

                //InsertAcc99();
                Successful();
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
        private void A2ZOPBALANCE11()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE11", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE12()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE12", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE13()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE13", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE14()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE14", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE15()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE15", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE16()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE16", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE17()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE17", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE18()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE18", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE19()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE19", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE20()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE20", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE21()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE21", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE23()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE23", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE24()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE24", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE51()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE51", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE52()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE52", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE53()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE53", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        private void A2ZOPBALANCE54()
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO A2ZMCUST2015.dbo.A2ZCSOPBALANCE(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT TRNDATE,CUNO,CUNO,MEMNO, ACCTYPE,ACCNO,TRNAMOUNT,CUNO FROM A2ZCCULBT2015.dbo.A2ZCSOPBALANCE54", con);
            con.Open();

            int result2 = cmd1.ExecuteNonQuery();
            if (result2 > 0)
            {
            }
            con.Close();
        }

        protected void ddlAccTupe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrunTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}