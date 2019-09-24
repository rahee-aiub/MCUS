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
    public partial class MissAccountData : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {

            string sqlquery4 = "Truncate table dbo.WFMISSMEM";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));

        }






        protected void BtnProceed_Click(object sender, EventArgs e)
        {

            A2ZACC11();
            DELA2ZACC11();
            A2ZACC12();
            DELA2ZACC12();
            A2ZACC13();
            DELA2ZACC13();
            A2ZACC14();
            DELA2ZACC14();
            A2ZACC15();
            DELA2ZACC15();
            A2ZACC16();
            DELA2ZACC16();
            A2ZACC17();
            DELA2ZACC17();
            A2ZACC18();
            DELA2ZACC18();
            A2ZACC20();
            DELA2ZACC20();
            A2ZACC21();
            DELA2ZACC21();
            A2ZACC23();
            DELA2ZACC23();
            A2ZACC24();
            DELA2ZACC24();
            A2ZACC51();
            DELA2ZACC51();
            A2ZACC52();
            DELA2ZACC52();
            A2ZACC53();
            DELA2ZACC53();
            A2ZACC54();
            DELA2ZACC54();

        }

        private void A2ZACC11()
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


        private void A2ZACC12()
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

        private void A2ZACC13()
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

        private void A2ZACC14()
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

        private void A2ZACC15()
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
        private void A2ZACC16()
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

        private void A2ZACC17()
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

        private void A2ZACC18()
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

        private void A2ZACC20()
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

        private void A2ZACC21()
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

        private void A2ZACC23()
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

        private void A2ZACC24()
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
        private void A2ZACC51()
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

        private void A2ZACC52()
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

        private void A2ZACC53()
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

        private void A2ZACC54()
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





    }
}
