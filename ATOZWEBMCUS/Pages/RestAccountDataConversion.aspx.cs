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
    public partial class RestAccountDataConversion : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlAccType.Focus();

        }






        protected void BtnProceed_Click(object sender, EventArgs e)
        {

            if (ddlAccType.Text == "4")
            {
                CPSMEMBER();
                InsertRestCPSAcc();
            }

            Successful();


        }


        private void CPSMEMBER()
        {
            string qry1 = "SELECT OldMem,MemName,OpenDate,NewCuType,NewCuNo FROM WFRESTCPSMEMBER";
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

                            string strQuery = "UPDATE WFRESTCPSMEMBER SET  NewMemNo = '" + MNo + "' WHERE  NewCuType = '" + CType + "' AND NewCuNo = '" + CNo + "' AND OldMem = '" + oldmem + "'";
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

                                string str1Query = "UPDATE WFRESTCPSMEMBER SET  NewMemNo = '" + M + "' WHERE  NewCuType = '" + newcutype + "' AND NewCuNo = '" + newcuno + "'  AND OldMem = '" + oldmem + "'";
                                int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZCSMCUS"));
                            }

                        }



                    }

                }
            }

        }


        private void InsertRestCPSAcc()
        {

            string qry = "SELECT NewCuType,NewCuNo,NewMemNo,OpenDate,LastTrnDate,DepositAmt,Period,Balance FROM WFRESTCPSMEMBER";
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

        private void GenerateAccNo()
        {
            int OrgAccType = 0;
            string qry1;
            DataTable dt1;

            //if (ddlAccType.Text == "1")
            //{
            //    OrgAccType = 11;
            //}
            //else if (ddlAccType.Text == "2")
            //{
            //    OrgAccType = 12;
            //}
            //else if (ddlAccType.Text == "3")
            //{
            //    OrgAccType = 13;
            //}
            //else if (ddlAccType.Text == "4")
            //{
            //    OrgAccType = 14;
            //}
            //else if (ddlAccType.Text == "5")
            //{
            //    OrgAccType = 15;
            //}
            //else if (ddlAccType.Text == "6")
            //{
            //    OrgAccType = 16;
            //}
            //else if (ddlAccType.Text == "7")
            //{
            //    OrgAccType = 17;
            //}
            //else if (ddlAccType.Text == "8")
            //{
            //    OrgAccType = 18;
            //}
            //else if (ddlAccType.Text == "10")
            //{
            //    OrgAccType = 20;
            //}
            //else if (ddlAccType.Text == "11")
            //{
            //    OrgAccType = 21;
            //}
            //else if (ddlAccType.Text == "12")
            //{
            //    OrgAccType = 23;
            //}
            //else if (ddlAccType.Text == "13")
            //{
            //    OrgAccType = 24;
            //}
            //else if (ddlAccType.Text == "14")
            //{
            //    OrgAccType = 99;
            //}
            //else if (ddlAccType.Text == "15")
            //{
            //    OrgAccType = 52;
            //}
            //else if (ddlAccType.Text == "16")
            //{
            //    OrgAccType = 53;
            //}
            //else if (ddlAccType.Text == "18")
            //{
            //    OrgAccType = 55;
            //}
            //else if (ddlAccType.Text == "19")
            //{
            //    OrgAccType = 58;
            //}


            OrgAccType = 14;

            //if (ddlAccType.Text == "0")
            //{
            //    qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT group by CuType,CuNo,MemNo,AccType";
            //    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            //}
            //else if (ddlAccType.Text == "14")
            //{
            //    qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT group by CuType,CuNo,MemNo,AccType WHERE AccType=51 or AccType=54 or AccType=64";
            //    dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            //}
            //else
            //{
                qry1 = "SELECT CuType,CuNo,MemNo,AccType FROM A2ZACCOUNT WHERE AccType='" + OrgAccType + "' group by CuType,CuNo,MemNo,AccType";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            //}


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