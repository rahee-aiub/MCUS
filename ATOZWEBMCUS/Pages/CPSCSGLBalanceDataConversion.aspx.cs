using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CPSCSGLBalanceDataConversion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CtrlServiceLoanAmt.Text = string.Empty;
                CtrlComputerLoanAmt.Text = string.Empty;

            }
        }


        private void Successful()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Conversion successfully completed');", true);
            return;


        }
        protected void BtnProcess_Click(object sender, EventArgs e)
        {

            CPSCSBalance();
            CPSGLBalance();
            CPSCSGLNetBalance();

            GLCPS0Upd();
            GLCPSBalUpd();

            string trucate = "TRUNCATE TABLE dbo.A2ZGLOPBALANCE";
            int re = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(trucate, "A2ZCSMCUST2015"));

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZGLOPBALANCE(GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLOpBal) SELECT GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLOpBal FROM A2ZGLMCUS.dbo.A2ZCGLMST WHERE GlPrtPos = 6";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if (result > 0)
            {

                Successful();
            }




        }

        private void CPSCSBalance()
        {

            string strQuery1 = "UPDATE WFCPSCSGLBALANCE SET  CSBalance = 0, GLBalance = 0";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));



            string qry = "SELECT CuType,CuNo,OldGlAccNo FROM WFCPSCSGLBALANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                decimal sumbal = 0;
                decimal amount = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OGLAccNo = dr["OldGlAccNo"].ToString();

                    int code = Converter.GetInteger(OGLAccNo);

                    string qry1 = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=14 AND CuType='" + CuType + "' AND CuNo='" + CuNo + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");




                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var csBalance = dr1["Amount"].ToString();
                            amount = Converter.GetDecimal(csBalance);



                            sumbal += Convert.ToDecimal(String.Format("{0:0,0.00}", amount));

                            if (code != 0)
                            {
                                string strQuery = "UPDATE WFCPSCSGLBALANCE SET  CSBalance = '" + sumbal + "' WHERE  OldGlAccNo='" + OGLAccNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                sumbal = 0;
                            }
                        }
                    }

                }

            }
        }


        private void CPSGLBalance()
        {
            string qry = "SELECT CuType,CuNo,OldGlAccNo FROM WFCPSCSGLBALANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OGLAccNo = dr["OldGlAccNo"].ToString();
                    int code = Converter.GetInteger(OGLAccNo);

                    if (code != 0)
                    {
                        string qry1 = "SELECT SUM(GLClBal) AS 'Amount' FROM A2ZCGLMST WHERE GLOldAccNo='" + OGLAccNo + "'";
                        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");

                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                var ABalance = dr1["Amount"].ToString();
                                decimal Balance = Converter.GetDecimal(ABalance);


                                string strQuery = "UPDATE WFCPSCSGLBALANCE SET  GLBalance = '" + ABalance + "' WHERE OldGlAccNo='" + OGLAccNo + "'";
                                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                            }
                        }
                    }

                }

            }
        }


        private void CPSCSGLNetBalance()
        {
            string qry = "SELECT SUM(CSBalance) AS 'CSAmount', SUM(GLBalance) AS 'GLAmount' FROM WFCPSCSGLBALANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CAmount = dr["CSAmount"].ToString();
                    var GAmount = dr["GLAmount"].ToString();

                    string strQuery = "UPDATE WFCPSCSGLBALANCE SET  CSBalance = '" + CAmount + "', GLBalance = '" + GAmount + "' WHERE CuType=0 and CuNo=0 and OldGlAccNo=0";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                }

            }
        }

        private void GLCPS0Upd()
        {
            string qry = "SELECT CuType,CuNo,OldGlAccNo,GLBalance FROM WFCPSCSGLBALANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OGLAccNo = dr["OldGlAccNo"].ToString();
                    var GLBalance = dr["GLBalance"].ToString();
                    int code = Converter.GetInteger(OGLAccNo);

                    if (code != 0)
                    {
                        string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLOldAccNo= '" + code + "'";
                        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                var ParentId = dr1["Id"].ToString();
                                A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                                objDTO.Id = Converter.GetSmallInteger(ParentId);
                                objDTO.GLOpBal = Converter.GetDecimal(0);
                                int row2 = A2ZCGLMSTDTO.Update1(objDTO);
                            }
                        }
                                   
                    }

                }

            }
        }


        private void GLCPSBalUpd()
        {
            string qry = "SELECT CuType,CuNo,OldGlAccNo,GLBalance FROM WFCPSCSGLBALANCE where CuType=0 AND CuNo=0 ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var CuType = dr["CuType"].ToString();
                    var CuNo = dr["CuNo"].ToString();
                    var OGLAccNo = dr["OldGlAccNo"].ToString();
                    var GLBalance = dr["GLBalance"].ToString();
                    int code = Converter.GetInteger(OGLAccNo);
                        
                    string qry1 = "SELECT Id,GLOpBal FROM A2ZCGLMST WHERE GLAccNo= 20205001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {                              
                            var ParentId = dr1["Id"].ToString();
                            var GLOpBal = dr1["GLOpBal"].ToString();

                            decimal A = Converter.GetDecimal(GLOpBal);
                            decimal B = Converter.GetDecimal(GLBalance);
                            decimal balance = (A + B);

                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(balance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);
                        }
                    }

                }

            }
        }



        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}