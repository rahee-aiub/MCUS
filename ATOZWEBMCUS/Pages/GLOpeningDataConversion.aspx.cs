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
    public partial class GLOpeningDataConversion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOpBalDate.Focus();
                CtrlServiceLoanAmt.Text = string.Empty;
                CtrlComputerLoanAmt.Text = string.Empty;

            }
        }


        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Data Conversion successfully completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Conversion successfully completed');", true);
            return;


        }
        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            if (txtOpBalDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please input Opening Balance Date.');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtOpBalDate.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Opening Balance Date');", true);

                return;
            }

            GLBalUpd();
            GLSavingsUpd();
            GLTCU18Upd();
            GLTCU23Upd();
            GLTCU54Upd();
            GLTCU61Upd();
            GLTCU51Upd();
            GLByCycleUpd();
            GLByMotorCycleUpd();

            TrfServiceLoanUpd();
            TrfComputerLoanUpd();
            TrfMotorCycleLoanUpd();
            TrfByCycleLoanUpd();
            //AddStaffMemberUpd();
            //InterestofGeneralLoan();
            //InterestofOverdraft();
            TrfStaffSecurityDeposit();
            StaffSalaryPayable();
            StaffPFLoan();

            StaffPFAccount();
            UpdateStaffPFAccount();
            StaffGratuityAccount();


            GLByServiceLoanUpd();
            GLByComputerLoanUpd();
            GLODLoanUpd();

            GenerateAccNo();

            //string trucate = "TRUNCATE TABLE dbo.A2ZGLOPBALANCE";
            //int re = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(trucate, "A2ZCSMCUST2015"));

            //DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZGLOPBALANCE(GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLOpBal) SELECT GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLOpBal FROM A2ZGLMCUS.dbo.A2ZCGLMST WHERE GlPrtPos = 6";
            //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            //if (result > 0)
            //{

            //    Successful();



            //}
        }


        private void GLBalUpd()
        {

            string strQuery = "UPDATE A2ZCGLMST SET  GlOpBal = 0, GLClBal = 0";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));


            string qry = "SELECT GLACCNO,GLCALBAL FROM A2ZCGLMST";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var OldGLCode = dr["GLACCNO"].ToString();
                    var GLCalBal = dr["GLCALBAL"].ToString();

                    int code = Converter.GetInteger(OldGLCode);
                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLOldAccNo='" + code + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLSavingsUpd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=12";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 20201001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLTCU18Upd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=18";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 20206001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLTCU23Upd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=23";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 20206002";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLTCU54Upd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=54";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();

                    decimal accbalance = Converter.GetDecimal(GLCalBal);
                    accbalance = Math.Abs(accbalance);

                    net54balance.Text = Converter.GetString(accbalance);
                    
                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10501002";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(accbalance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLTCU61Upd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=61";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();

                    decimal accbalance = Converter.GetDecimal(GLCalBal);
                    accbalance = Math.Abs(accbalance);

                    net61balance.Text = Converter.GetString(accbalance);


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10501004";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(accbalance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }


        private void GLTCU51Upd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType=51";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();

                    decimal accbalance = Converter.GetDecimal(GLCalBal);
                    accbalance = Math.Abs(accbalance);

                    net61balance.Text = Converter.GetString(accbalance);


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10501001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(accbalance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }
        private void GLByCycleUpd()
        {
            string qry = "SELECT SUM(GLCALBAL) AS 'Amount' FROM A2ZCGLMST WHERE (GLACCNO BETWEEN 101401 AND 101499) OR (GLACCNO BETWEEN 199001 AND 199019)";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10504003";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLByMotorCycleUpd()
        {
            string qry = "SELECT SUM(GLCALBAL) AS 'Amount' FROM A2ZCGLMST WHERE (GLACCNO BETWEEN 101911 AND 102040) AND GLACCNO!=101950 AND GLACCNO!=102000";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10504002";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


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

        private void TrfServiceLoanUpd()
        {
           
            //string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=55";
            //int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            

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

        //private void TrfServiceLoanUpd()
        //{
        //    //string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE MemNo BETWEEN 9001 AND 9196";
        //    //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

        //    string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=55";
        //    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


        //    string qry2 = "SELECT Id,AccType,OldMemNo FROM A2ZACCOUNT WHERE AccType = '52' and OldMemNo BETWEEN 9001 AND 9196";
        //    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
        //    if (dt2.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr1 in dt2.Rows)
        //        {
        //            var ParentId = dr1["Id"].ToString();
        //            var AccType = dr1["AccType"].ToString();
        //            var MemNo = dr1["OldMemNo"].ToString();

        //            int Id = Converter.GetInteger(ParentId);
        //            int acctype = Converter.GetInteger(AccType);
        //            int memno = Converter.GetInteger(MemNo);

        //            string qry3 = "SELECT NewNo,Amount FROM wfServiceLoan WHERE OldNo = '" + memno + "'";
        //            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");
        //            if (dt3.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr2 in dt3.Rows)
        //                {
        //                    var NewNo = dr2["NewNo"].ToString();
        //                    var Amount = dr2["Amount"].ToString();

        //                    int newno = Converter.GetInteger(NewNo);
        //                    decimal amount = Converter.GetDecimal(Amount);

        //                    string strQuery = "UPDATE A2ZACCOUNT SET  CuType = 0, CuNo = 0, MemNo = '" + newno + "', AccType = 55, AccLoanInstlAmt = '" + amount + "', AccAtyClass = 6 WHERE  Id='" + Id + "' ";
        //                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                }
        //            }
        //        }
        //    }

        //}
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

        //private void TrfComputerLoanUpd()
        //{
        //    //string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccODIntDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,AccLoanGrace,AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate,OldCuNo) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCLASTDT,ACCINSTNO,ACCINSTAMT,ACCLASTAMT,ACCINTRATE,ACCMORTMM,ACCDISBAMT,ACCDISBDT,ACCDISBAMT,ACCDISBDT,CUNO FROM A2ZCCULB.dbo.A2ZACC52 WHERE MemNo BETWEEN 9701 AND 9741";
        //    //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

        //    string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=58";
        //    int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


        //    string qry2 = "SELECT Id,AccType,OldMemNo FROM A2ZACCOUNT WHERE AccType = '52' and OldMemNo BETWEEN 9701 AND 9745";
        //    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
        //    if (dt2.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr1 in dt2.Rows)
        //        {
        //            var ParentId = dr1["Id"].ToString();
        //            var AccType = dr1["AccType"].ToString();
        //            var MemNo = dr1["OldMemNo"].ToString();

        //            int Id = Converter.GetInteger(ParentId);
        //            int acctype = Converter.GetInteger(AccType);
        //            int memno = Converter.GetInteger(MemNo);

        //            string qry3 = "SELECT NewNo,Amount FROM wfComputerLoan WHERE OldNo = '" + memno + "'";
        //            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");
        //            if (dt3.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr2 in dt3.Rows)
        //                {
        //                    var NewNo = dr2["NewNo"].ToString();
        //                    var Amount = dr2["Amount"].ToString();

        //                    int newno = Converter.GetInteger(NewNo);
        //                    decimal amount = Converter.GetDecimal(Amount);

        //                    string strQuery = "UPDATE A2ZACCOUNT SET  CuType = 0, CuNo = 0, MemNo = '" + newno + "', AccType = 58, AccLoanInstlAmt = '" + amount + "', AccAtyClass = 6 WHERE  Id='" + Id + "' ";
        //                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
        //                }
        //            }
        //        }
        //    }

        //}

        private void InterestofGeneralLoan()
        {
            string qry = "SELECT SUM(GLCALBAL) AS 'Amount' FROM A2ZCGLMST WHERE GLACCNO BETWEEN 190001 AND 190416";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10603001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }


        private void InterestofOverdraft()
        {
            string qry = "SELECT SUM(GLCALBAL) AS 'Amount' FROM A2ZCGLMST WHERE GLACCNO BETWEEN 195001 AND 195013";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCCULB");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10603003";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(GLCalBal);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }


        private void TrfStaffSecurityDeposit()
        {
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=19";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            

                string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccSpecialNote,AccStatus,AccStatusDate,AccStatusP,AccBalance,AccOpeningBal,AccLastTrnDateU,AccIntRate) SELECT ACCTYPE,ACCNO,CUNO,CUNO,MEMNO,ACCOPENDT,ACCSPDESC,ACCSTAT,ACCSTATDT,ACCPSTAT,ACCBALANCE,ACCOPBAL,ACCLASTDT,ACCINTRATE FROM A2ZCCULB.dbo.A2ZACC19 WHERE MemNo !=0";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));


                string qry2 = "SELECT Id,AccType,MemNo FROM A2ZACCOUNT WHERE AccType = '19'";
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

                        string strQuery = "UPDATE A2ZACCOUNT SET  CuType = 0, CuNo = 0, AccAtyClass = 1 WHERE  Id='" + Id + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    }
                }
            
        }

        private void StaffSalaryPayable()
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt = Converter.GetDateTime(dto.ProcessDate);
            string date = dt.ToString("dd/MM/yyyy");
            lblOpenDate.Text = date;

            DateTime opdate = DateTime.ParseExact(lblOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=25";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry2 = "SELECT MemNo,MemName FROM wfStaffMember";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    var MemNo = dr2["MemNo"].ToString();
                    var MemName = dr2["MemName"].ToString();

                    int memno = Converter.GetInteger(MemNo);


                    string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES (0, 0,'" + memno + "',25,0,'" + opdate + "',1,1)";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                }
            }


        }

        private void StaffPFAccount()
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt = Converter.GetDateTime(dto.ProcessDate);
            string date = dt.ToString("dd/MM/yyyy");
            lblOpenDate.Text = date;

            DateTime opdate = DateTime.ParseExact(lblOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=26";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry2 = "SELECT MemNo,MemName FROM wfStaffMember";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    var MemNo = dr2["MemNo"].ToString();
                    var MemName = dr2["MemName"].ToString();

                    int memno = Converter.GetInteger(MemNo);


                    string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES (0, 0,'" + memno + "',26,0,'" + opdate + "',1,1)";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                }
            }


        }


        private void UpdateStaffPFAccount()
        {
            
            string qry2 = "SELECT Ids,PerNo,AccBalance FROM WFPFACC";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt2.Rows)
                {
                    var ParentId = dr1["Ids"].ToString();
                    var perno = dr1["PerNo"].ToString();
                    var accbalance = dr1["AccBalance"].ToString();

                    int Id = Converter.GetInteger(ParentId);
                    int memno = Converter.GetInteger(perno);
                    Decimal balance = Converter.GetDecimal(accbalance);

                    string strQuery = "UPDATE A2ZACCOUNT SET  AccBalance = '" + balance + "'  WHERE  AccType = 26 AND CuType=0 AND CuNo=0 AND MemNo='" + memno + "' ";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                }
            }

        }

        private void StaffGratuityAccount()
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt = Converter.GetDateTime(dto.ProcessDate);
            string date = dt.ToString("dd/MM/yyyy");
            lblOpenDate.Text = date;

            DateTime opdate = DateTime.ParseExact(lblOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=27";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry2 = "SELECT MemNo,MemName FROM wfStaffMember";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    var MemNo = dr2["MemNo"].ToString();
                    var MemName = dr2["MemName"].ToString();

                    int memno = Converter.GetInteger(MemNo);


                    string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass) VALUES (0, 0,'" + memno + "',27,0,'" + opdate + "',1,1)";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                }
            }


        }
        private void StaffPFLoan()
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt = Converter.GetDateTime(dto.ProcessDate);
            string date = dt.ToString("dd/MM/yyyy");
            lblOpenDate.Text = date;

            DateTime opdate = DateTime.ParseExact(lblOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string delqry = "DELETE FROM A2ZACCOUNT WHERE AccType=63";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));


            string qry2 = "SELECT PerNo,InstlAmount,AccBalance,NoInstl,IntRate FROM WFPFLOan";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    var perno = dr2["PerNo"].ToString();
                    var instlamt = dr2["InstlAmount"].ToString();
                   
                    var accbalance = dr2["AccBalance"].ToString();
                    var noinstl = dr2["NoInstl"].ToString();
                    var intrate = dr2["IntRate"].ToString();

                    double balance = Converter.GetDouble(accbalance);

                    balance = (0 - balance);

                    string qry1 = "INSERT INTO A2ZCSMCUS.dbo.A2ZACCOUNT(CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccStatus,AccAtyClass,AccNoInstl,AccLoanInstlAmt,AccBalance,AccIntRate) VALUES (0, 0,'" + perno + "',63,0,'" + opdate + "',1,6,'" + noinstl + "','" + instlamt + "','" + balance + "','" + intrate + "')";
                    int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZCSMCUS"));

                }
            }


        }
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

        private void GLByServiceLoanUpd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType = 55";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();

                    decimal accbalance = Converter.GetDecimal(GLCalBal);
                    accbalance = Math.Abs(accbalance);

                    CtrlServiceLoanAmt.Text = Converter.GetString(accbalance);

                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10504001";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(accbalance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLByComputerLoanUpd()
        {
            string qry = "SELECT SUM(AccBalance) AS 'Amount' FROM A2ZACCOUNT WHERE AccType = 58";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {

                    var GLCalBal = dr["Amount"].ToString();

                    decimal accbalance = Converter.GetDecimal(GLCalBal);
                    accbalance = Math.Abs(accbalance);

                    CtrlComputerLoanAmt.Text = Converter.GetString(accbalance);


                    string qry1 = "SELECT Id FROM A2ZCGLMST WHERE GLAccNo= 10504004";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZGLMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            var ParentId = dr1["Id"].ToString();
                            A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                            objDTO.Id = Converter.GetSmallInteger(ParentId);
                            objDTO.GLOpBal = Converter.GetDecimal(accbalance);
                            int row2 = A2ZCGLMSTDTO.Update1(objDTO);


                        }
                    }
                }
            }
        }

        private void GLODLoanUpd()
        {

            string qry = "SELECT Id,GLOpBal FROM A2ZCGLMST WHERE GLAccNo = 10505001";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var ParentId = dr["Id"].ToString();
                    var GLOpBal = dr["GLOpBal"].ToString();

                    decimal OLoan = Converter.GetDecimal(GLOpBal);
                    decimal SLoan = Converter.GetDecimal(CtrlServiceLoanAmt.Text);
                    decimal CLoan = Converter.GetDecimal(CtrlComputerLoanAmt.Text);

                    decimal NLoan = (OLoan - (SLoan + CLoan));

                    A2ZCGLMSTDTO objDTO = new A2ZCGLMSTDTO();
                    objDTO.Id = Converter.GetSmallInteger(ParentId);
                    objDTO.GLOpBal = Converter.GetDecimal(NLoan);
                    int row2 = A2ZCGLMSTDTO.Update1(objDTO);

                }
            }
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

                    if (lblAcType.Text == "19" ||
                        lblAcType.Text == "25" ||
                        lblAcType.Text == "26" ||
                        lblAcType.Text == "27" ||
                        lblAcType.Text == "55" ||
                        lblAcType.Text == "56" ||
                        lblAcType.Text == "57" ||
                        lblAcType.Text == "58" ||
                        lblAcType.Text == "62" ||
                        lblAcType.Text == "63")
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
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}