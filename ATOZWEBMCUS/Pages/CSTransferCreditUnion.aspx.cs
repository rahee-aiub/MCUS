using DataAccessLayer.DTO.CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.BLL;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSTransferCreditUnion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                txtCuNo.Focus();
            }
        }

        protected void DisplayMessage()
        {
            string qry = "SELECT Id,CuAssoCuType,CuAssoCuNo,CuReguCuType,CuReguCuNo FROM A2ZCUNION where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNumber.Text + "' ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                if (lblCuType.Text == "1")
                {
                    lblNewCuTypeName.Text = "Associate";
                    lblNewCuType.Text = Converter.GetString(dt.Rows[0]["CuAssoCuType"]);
                    lblNewCuNo.Text = Converter.GetString(dt.Rows[0]["CuAssoCuNo"]);
                }
                else
                {
                    lblNewCuTypeName.Text = "Regular";
                    lblNewCuType.Text = Converter.GetString(dt.Rows[0]["CuReguCuType"]);
                    lblNewCuNo.Text = Converter.GetString(dt.Rows[0]["CuReguCuNo"]);
                }

            }


            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";

            a = "Transfer New Credit Union No.";
            e = "Credit Union No. :";
            b = string.Format("Credit Union Type : {0}", lblNewCuTypeName.Text);
            c = string.Format(lblNewCuNo.Text);
            X = "-";
            d = string.Format(lblNewCuType.Text);

            Msg += a;
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e + d + X + c;
            //Msg += d;
            //Msg += X;
            //Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

        }

        protected void PrevUpdateInfo()
        {
            A2ZCUNIONDTO objDTO = new A2ZCUNIONDTO();

            objDTO.CuType = Converter.GetSmallInteger(lblNewCuType.Text);

            if (objDTO.CuType == 3)
            {
                Int16 CuType = Converter.GetSmallInteger(lblAffiCuType.Text);
                int CuNo = Converter.GetInteger(lblAffiCuNo.Text);
                objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CuNo));
                if (objDTO.CreditUnionNo > 0)
                {
                    objDTO.CuReguCuType = Converter.GetSmallInteger(lblReguCuType.Text);
                    objDTO.CuReguCuNo = Converter.GetInteger(lblReguCuNo.Text);
                    objDTO.CuReguCuTypeName = Converter.GetString(lblReguCuTypeName.Text);


                    var prm = new object[5];
                    prm[0] = objDTO.CuType;
                    prm[1] = objDTO.CreditUnionNo;
                    prm[2] = objDTO.CuReguCuType;
                    prm[3] = objDTO.CuReguCuTypeName;
                    prm[4] = objDTO.CuReguCuNo;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCUPreviousDataUpdate", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                    }

                    //int roweffect = A2ZCUNIONDTO.UpdateInformation(objDTO);
                }

                Int16 CType = Converter.GetSmallInteger(lblAssoCuType.Text);
                int CNo = Converter.GetInteger(lblAssoCuNo.Text);
                objDTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                if (objDTO.CreditUnionNo > 0)
                {
                    objDTO.CuReguCuType = Converter.GetSmallInteger(lblReguCuType.Text);
                    objDTO.CuReguCuNo = Converter.GetInteger(lblReguCuNo.Text);
                    objDTO.CuReguCuTypeName = Converter.GetString(lblReguCuTypeName.Text);

                    var prm = new object[5];
                    prm[0] = objDTO.CuType;
                    prm[1] = objDTO.CreditUnionNo;
                    prm[2] = objDTO.CuReguCuType;
                    prm[3] = objDTO.CuReguCuTypeName;
                    prm[4] = objDTO.CuReguCuNo;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCUPreviousDataUpdate", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                    }

                    //int roweffect = A2ZCUNIONDTO.UpdateInformation(objDTO);
                }
            }
        }

        protected void PrevMemberfleUpdateInfo()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CNo = Converter.GetInteger(lblCuNumber.Text);

            string qry = "SELECT Id,CuType,CuNo FROM A2ZCUNION where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNumber.Text + "' ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                //lblNewCuType.Text = Converter.GetString(dt.Rows[0]["CuType"]);
                //lblNewCuNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);

                string strQuery = "UPDATE A2ZMEMBER set CuType='" + lblNewCuType.Text + "',CuNo='" + lblNewCuNo.Text + "' where  CuType='" + CuType + "' and CuNo='" + CNo + "' ";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {

                }
            }
        }

        protected void GetAccountCount()
        {
            string a = lblAccNo.Text;

            lblNewAccNo.Text = a.Substring(12, 4);


            //string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblNewCuType.Text + "' and CuNo='" + lblNewCuNo.Text + "' and MemNo='" + 0 + "' and AccType='" + lblAccType.Text + "'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            //int totrec = dt.Rows.Count;
            //int newaccno = (totrec + 1);
            //lblNewAccNo.Text = Converter.GetString(newaccno);
        }


        protected void GenerateNewAccNo()
        {


            GetAccountCount();

            string input1 = Converter.GetString(lblNewCuNo.Text).Length.ToString();
            string input2 = Converter.GetString(lblMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(lblNewAccNo.Text).Length.ToString();

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
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + result1 + lblNewCuNo.Text + result2 + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 != "5" && input3 == "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + result1 + lblNewCuNo.Text + result2 + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 != "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + result1 + lblNewCuNo.Text + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 == "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + result1 + lblNewCuNo.Text + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 != "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + lblNewCuNo.Text + result2 + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 == "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + lblNewCuNo.Text + result2 + lblMemNo.Text + lblNewAccNo.Text;
            }

            if (input1 == "4" && input2 == "5" && input3 != "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + lblNewCuNo.Text + lblMemNo.Text + result3 + lblNewAccNo.Text;
            }
            if (input1 == "4" && input2 == "5" && input3 == "4")
            {
                lblNewAccount.Text = lblAccType.Text + lblNewCuType.Text + lblNewCuNo.Text + lblMemNo.Text + lblNewAccNo.Text;
            }


        }



        protected void PrevAccountfleUpdateInfo()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CNo = Converter.GetInteger(lblCuNumber.Text);

            string qry = "SELECT Id,CuType,CuNo FROM A2ZCUNION where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNumber.Text + "' ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {

                string qry1 = "SELECT Id,AccType,AccNo,CuType,CuNo,MemNo FROM A2ZACCOUNT WHERE CuType = '" + CuType + "' AND CuNo='" + CNo + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        var AccType = dr1["AccType"].ToString();
                        var AccNo = dr1["AccNo"].ToString();
                        var MemNo = dr1["MemNo"].ToString();

                        lblAccType.Text = AccType;
                        lblAccNo.Text = AccNo;
                        lblMemNo.Text = MemNo;



                        GenerateNewAccNo();



                        var prm = new object[10];

                        prm[0] = lblCuType.Text;
                        prm[1] = lblCuNumber.Text;
                        prm[2] = lblMemNo.Text;
                        prm[3] = lblAccType.Text;
                        //prm[4] = Converter.GetLong(lblAccNo.Text);
                        prm[4] = lblAccNo.Text;

                        prm[5] = lblNewCuType.Text;
                        prm[6] = lblNewCuNo.Text;
                        prm[7] = lblMemNo.Text;
                        //prm[8] = Converter.GetLong(lblNewAccount.Text);
                        prm[8] = lblNewAccount.Text;
                        prm[9] = lblID.Text;


                        int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAccountTransferData", prm, "A2ZCSMCUS"));
                        if (result == 0)
                        {

                        }



                    }
                }


            }
        }

        protected void PrevTransactionfleUpdateInfo()
        {
            Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
            int CNo = Converter.GetInteger(lblCuNumber.Text);

            string qry = "SELECT Id,CuType,CuNo FROM A2ZCUNION where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNumber.Text + "' ";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                //lblNewCuType.Text = Converter.GetString(dt.Rows[0]["CuType"]);
                //lblNewCuNo.Text = Converter.GetString(dt.Rows[0]["CuNo"]);

                string strQuery = "UPDATE A2ZTRANSACTION set CuType='" + lblNewCuType.Text + "',CuNo='" + lblNewCuNo.Text + "' where  CuType='" + CuType + "' and CuNo='" + CNo + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                string Upqry1 = "UPDATE A2ZCSOPBALANCE SET CuType='" + lblNewCuType.Text + "',CuNo='" + lblNewCuNo.Text + "' WHERE  CuType='" + CuType + "' and CuNo='" + CNo + "'";
                int row1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(Upqry1, "A2ZCSMCUST2015"));


                if (rowEffect > 0)
                {

                }
            }
        }


        protected void BtnProcess_Click(object sender, EventArgs e)
        {

            if (txtCuNo.Text != string.Empty && lblCuType.Text != string.Empty && lblCuNumber.Text != string.Empty)
            {
                Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                int CNo = Converter.GetInteger(lblCuNumber.Text);
                A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                if (objDTO.CreditUnionNo > 0)
                {
                    if (objDTO.CuProcFlag != 13)
                    {
                        txtCuNo.Text = string.Empty;
                        lblCuName.Text = string.Empty;
                        lblCuType.Text = string.Empty;
                        lblCuNumber.Text = string.Empty;

                        ProcMSG();
                        txtCuNo.Focus();
                        return;

                    }

                    if (objDTO.CuStatus != 0)
                    {
                        txtCuNo.Text = string.Empty;
                        lblCuName.Text = string.Empty;
                        lblCuType.Text = string.Empty;
                        lblCuNumber.Text = string.Empty;

                        StatusMSG();
                        txtCuNo.Focus();
                        return;

                    }

                    var prm = new object[4];

                    prm[0] = lblCuType.Text;
                    prm[1] = lblCuNumber.Text;
                    prm[2] = lblID.Text;
                    prm[3] = 0;

                    int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCUTransfer", prm, "A2ZCSMCUS"));
                    if (result1 == 0)
                    {
                        DisplayMessage();
                        txtCuNo.Text = string.Empty;
                        lblCuName.Text = string.Empty;
                        lblTransferMode.Text = string.Empty;
                        btnSearch.Visible = true;
                        txtCuNo.Enabled = true;


                        txtCuNo.Focus();
                        return;
                    }

                }
                else
                {
                    txtCuNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    lblCuType.Text = string.Empty;
                    lblCuNumber.Text = string.Empty;
                    InvalidMSG();
                    txtCuNo.Focus();
                    return;
                }
            }
            else
            {
                txtCuNo.Text = string.Empty;
                lblCuName.Text = string.Empty;
                lblCuType.Text = string.Empty;
                lblCuNumber.Text = string.Empty;
                txtCuNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                return;
            }

        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtCuNo.Text = string.Empty;
            lblCuType.Text = string.Empty;
            lblCuNumber.Text = string.Empty;
            lblCuName.Text = string.Empty;
            lblTransferMode.Text = string.Empty;
            btnSearch.Visible = true;
            txtCuNo.Enabled = true;

            txtCuNo.Focus();
        }

        //protected void BtnProcess_Click(object sender, EventArgs e)
        //{

        //    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //    int CNo = Converter.GetInteger(lblCuNumber.Text);
        //    A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

        //    if (objDTO.CreditUnionNo > 0)
        //    {
        //        if (objDTO.CuProcFlag != 13)
        //        {
        //            ProcMSG();
        //            txtCuNo.Focus();
        //            return;

        //        }

        //        if (objDTO.CuStatus != 0)
        //        {
        //            StatusMSG();
        //            txtCuNo.Focus();
        //            return;

        //        }

        //        if (CuType == 1)
        //        {
        //            lblAffiCuType.Text = "1";
        //            lblAffiCuTypeName.Text = "Affiliate";
        //            lblAffiCuNo.Text = Converter.GetString(lblCuNumber.Text);

        //            lblAssoCuType.Text = "2";
        //            lblAssoCuTypeName.Text = "Associate";

        //            lblNewCuType.Text = "2";
        //            lblNewCuTypeName.Text = "Associate";
        //        }
        //        else
        //        {
        //            lblAffiCuType.Text = Converter.GetString(objDTO.CuAffiCuType);
        //            lblAffiCuTypeName.Text = Converter.GetString(objDTO.CuAffiCuTypeName);
        //            lblAffiCuNo.Text = Converter.GetString(objDTO.CuAffiCuNo);

        //            lblAssoCuType.Text = "2";
        //            lblAssoCuTypeName.Text = "Associate";
        //            lblAssoCuNo.Text = Converter.GetString(lblCuNumber.Text);

        //            lblReguCuType.Text = "3";
        //            lblReguCuTypeName.Text = "Regular";

        //            lblNewCuType.Text = "3";
        //            lblNewCuTypeName.Text = "Regular";

        //        }

        //        Int16 Code = Converter.GetSmallInteger(lblNewCuType.Text);
        //        A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(Code));
        //        lblNewCuNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

        //        if (CuType == 1)
        //        {
        //            lblAssoCuNo.Text = Converter.GetString(lblNewCuNo.Text);

        //        }
        //        else
        //        {
        //            lblReguCuNo.Text = Converter.GetString(lblNewCuNo.Text);
        //        }


        //        objDTO.CreditUnionNo = Converter.GetInteger(CNo);
        //        objDTO.CuType = Converter.GetSmallInteger(CuType);

        //        objDTO.CuAffiCuType = Converter.GetSmallInteger(lblAffiCuType.Text);
        //        objDTO.CuAffiCuTypeName = Converter.GetString(lblAffiCuTypeName.Text);
        //        objDTO.CuAffiCuNo = Converter.GetInteger(lblAffiCuNo.Text);

        //        objDTO.CuAssoCuType = Converter.GetSmallInteger(lblAssoCuType.Text);
        //        objDTO.CuAssoCuTypeName = Converter.GetString(lblAssoCuTypeName.Text);
        //        objDTO.CuAssoCuNo = Converter.GetInteger(lblAssoCuNo.Text);

        //        objDTO.CuReguCuType = Converter.GetSmallInteger(lblReguCuType.Text);
        //        objDTO.CuReguCuTypeName = Converter.GetString(lblReguCuTypeName.Text);
        //        objDTO.CuReguCuNo = Converter.GetInteger(lblReguCuNo.Text);

        //        lblGLCashCode.Text = Converter.GetString(objDTO.GLCashCode);
        //        lblCuOldCuNo.Text = Converter.GetString(objDTO.CuOldCuNo);


        //        objDTO.CuStatus = 9;
        //        objDTO.CuOldCuNo = 0;

        //        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
        //        objDTO.CuStatusDate = Converter.GetDateTime(dto.ProcessDate);

        //        var prm = new object[14];
        //        prm[0] = objDTO.CuType;
        //        prm[1] = objDTO.CreditUnionNo;
        //        prm[2] = objDTO.CuAffiCuType;
        //        prm[3] = objDTO.CuAffiCuTypeName;
        //        prm[4] = objDTO.CuAffiCuNo;
        //        prm[5] = objDTO.CuAssoCuType;
        //        prm[6] = objDTO.CuAssoCuTypeName;
        //        prm[7] = objDTO.CuAssoCuNo;
        //        prm[8] = objDTO.CuReguCuType;
        //        prm[9] = objDTO.CuReguCuTypeName;
        //        prm[10] = objDTO.CuReguCuNo;

        //        prm[11] = objDTO.CuStatus;
        //        prm[12] = objDTO.CuStatusDate;
        //        prm[13] = objDTO.CuOldCuNo;

        //        int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCUTransferDataUpdate", prm, "A2ZCSMCUS"));
        //        if (result == 0)
        //        {
        //            txtCuNo.Text = string.Empty;
        //        }





        //        objDTO.CreditUnionNo = Converter.GetInteger(lblNewCuNo.Text);
        //        objDTO.CuType = Converter.GetSmallInteger(lblNewCuType.Text);
        //        objDTO.CuTypeName = Converter.GetString(lblNewCuTypeName.Text);

        //        objDTO.CuAffiCuType = Converter.GetSmallInteger(lblAffiCuType.Text);
        //        objDTO.CuAffiCuTypeName = Converter.GetString(lblAffiCuTypeName.Text);
        //        objDTO.CuAffiCuNo = Converter.GetInteger(lblAffiCuNo.Text);

        //        objDTO.CuAssoCuType = Converter.GetSmallInteger(lblAssoCuType.Text);
        //        objDTO.CuAssoCuTypeName = Converter.GetString(lblAssoCuTypeName.Text);
        //        objDTO.CuAssoCuNo = Converter.GetInteger(lblAssoCuNo.Text);

        //        objDTO.CuReguCuType = Converter.GetSmallInteger(lblReguCuType.Text);
        //        objDTO.CuReguCuTypeName = Converter.GetString(lblReguCuTypeName.Text);
        //        objDTO.CuReguCuNo = Converter.GetInteger(lblReguCuNo.Text);

        //        objDTO.CuStatus = 0;

        //        objDTO.CuStatusDate = Converter.GetDateTime(dto.ProcessDate);

        //        objDTO.GLCashCode = Converter.GetInteger(lblGLCashCode.Text);
        //        objDTO.CuOldCuNo = Converter.GetInteger(lblCuOldCuNo.Text);


        //        var prm1 = new object[40];

        //        prm1[0] = objDTO.CuType;
        //        prm1[1] = objDTO.CuTypeName;
        //        prm1[2] = objDTO.CreditUnionNo;
        //        prm1[3] = objDTO.CreditUnionName;
        //        prm1[4] = objDTO.opendate;
        //        prm1[5] = objDTO.MemberFlag;
        //        prm1[6] = objDTO.CertificateNo;
        //        prm1[7] = objDTO.AddressL1;
        //        prm1[8] = objDTO.AddressL2;
        //        prm1[9] = objDTO.AddressL3;
        //        prm1[10] = objDTO.TelephoneNo;
        //        prm1[11] = objDTO.MobileNo;
        //        prm1[12] = objDTO.Fax;
        //        prm1[13] = objDTO.email;
        //        prm1[14] = objDTO.Division;
        //        prm1[15] = objDTO.District;
        //        prm1[16] = objDTO.Upzila;
        //        prm1[17] = objDTO.Thana;
        //        prm1[18] = objDTO.CuProcFlag;
        //        prm1[19] = objDTO.CuProcDesc;
        //        prm1[20] = objDTO.CuStatus;
        //        prm1[21] = objDTO.CuStatusDate;
        //        prm1[22] = objDTO.CuAffiCuType;
        //        prm1[23] = objDTO.CuAffiCuTypeName;
        //        prm1[24] = objDTO.CuAffiCuNo;
        //        prm1[25] = objDTO.CuAssoCuType;
        //        prm1[26] = objDTO.CuAssoCuTypeName;
        //        prm1[27] = objDTO.CuAssoCuNo;
        //        prm1[28] = objDTO.CuReguCuType;
        //        prm1[29] = objDTO.CuReguCuTypeName;
        //        prm1[30] = objDTO.CuReguCuNo;
        //        prm1[31] = objDTO.CuOldCuNo;
        //        prm1[32] = objDTO.GLCashCode;
        //        prm1[33] = objDTO.InputBy;
        //        prm1[34] = objDTO.VerifyBy;
        //        prm1[35] = objDTO.ApprovBy;
        //        prm1[36] = objDTO.InputByDate;
        //        prm1[37] = objDTO.VerifyByDate;
        //        prm1[38] = objDTO.ApprovByDate;

        //        prm1[39] = objDTO.UserId;



        //        int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCUTransferDataInsert", prm1, "A2ZCSMCUS"));
        //        if (result1 == 0)
        //        {
        //            PrevUpdateInfo();
        //            PrevMemberfleUpdateInfo();
        //            PrevAccountfleUpdateInfo();
        //            //PrevTransactionfleUpdateInfo();
        //            DisplayMessage();
        //            lblCuName.Text = string.Empty;
        //            lblTransferMode.Text = string.Empty;
        //            btnSearch.Visible = true;

        //            txtCuNo.Focus();
        //            return;
        //        }



        //    }
        //    else
        //    {
        //        InvalidMSG();
        //        txtCuNo.Focus();
        //        return;
        //    }



        //}

        private void InvalidMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;

        }

        private void ProcMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Credit Union No. not Approved');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Credit Union No. not Approved');", true);
            return;
        }
        private void StatusMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Credit Union No. not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Credit Union No. not Active');", true);
            return;
        }

        //protected void txtCuNo_TextChanged(object sender, EventArgs e)
        //{
        //    string c = "";
        //    int a = txtCuNo.Text.Length;

        //    string b = txtCuNo.Text;
        //    c = b.Substring(0, 1);
        //    Int16 re = Converter.GetSmallInteger(c);
        //    int dd = a - 1;
        //    string d = b.Substring(1, dd);
        //    int re1 = Converter.GetInteger(d);

        //    Int16 CuType = Converter.GetSmallInteger(re);
        //    lblCuType.Text = Converter.GetString(re);

        //    int CNo = Converter.GetInteger(re1);
        //    lblCuNumber.Text = Converter.GetString(re1);

        //    A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

        //    if (objDTO.CreditUnionNo > 0)
        //    {
        //        lblCuName.Text = Converter.GetString(objDTO.CreditUnionName);
        //        txtCuNo.Text = (c + "-" + d);

        //        if (objDTO.CuProcFlag != 13)
        //        {
        //            ProcMSG();
        //            txtCuNo.Text = string.Empty;
        //            txtCuNo.Focus();
        //            return;
        //        }

        //        if (objDTO.CuStatus != 0)
        //        {
        //            StatusMSG();
        //            txtCuNo.Text = string.Empty;
        //            txtCuNo.Focus();
        //            return;
        //        }

        //        if (CuType == 3)
        //        {
        //            InvalidMSG();
        //            txtCuNo.Text = string.Empty;
        //            txtCuNo.Focus();
        //            return;
        //        }

        //        if (CuType == 1)
        //        {
        //            lblTransferMode.Text = "Transfering ... Affiliate to Associate";
        //        }
        //        else
        //        {
        //            lblTransferMode.Text = "Transfering ... Associate to Regular";
        //        }

        //        BtnProcess.Focus();
        //        return;
        //    }

        //    else
        //    {
        //        InvalidMSG();
        //        txtCuNo.Text = string.Empty;
        //        txtCuNo.Focus();
        //    }
        //}

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtCuNo.Text == string.Empty)
            {
                txtCuNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Credit Union No.');", true);
                return;
            }



            string c = "";
            int a = txtCuNo.Text.Length;

            string b = txtCuNo.Text;
            c = b.Substring(0, 1);
            Int16 re = Converter.GetSmallInteger(c);
            int dd = a - 1;
            string d = b.Substring(1, dd);
            int re1 = Converter.GetInteger(d);

            Int16 CuType = Converter.GetSmallInteger(re);
            lblCuType.Text = Converter.GetString(re);

            int CNo = Converter.GetInteger(re1);
            lblCuNumber.Text = Converter.GetString(re1);

            A2ZCUNIONDTO objDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

            if (objDTO.CreditUnionNo > 0)
            {
                lblCuName.Text = Converter.GetString(objDTO.CreditUnionName);
                txtCuNo.Text = (c + "-" + d);

                if (objDTO.CuProcFlag != 13)
                {
                    ProcMSG();
                    txtCuNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    lblCuType.Text = string.Empty;
                    lblCuNumber.Text = string.Empty;
                    txtCuNo.Focus();
                    return;
                }

                if (objDTO.CuStatus != 0)
                {
                    StatusMSG();
                    txtCuNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    lblCuType.Text = string.Empty;
                    lblCuNumber.Text = string.Empty;
                    txtCuNo.Focus();
                    return;
                }

                if (CuType == 3)
                {
                    InvalidMSG();
                    txtCuNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    lblCuType.Text = string.Empty;
                    lblCuNumber.Text = string.Empty;
                    txtCuNo.Focus();
                    return;
                }

                if (CuType == 1)
                {
                    lblTransferMode.Text = "Transfering ... Affiliate to Associate";
                }
                else
                {
                    lblTransferMode.Text = "Transfering ... Associate to Regular";
                }
                btnSearch.Visible = false;
                txtCuNo.Enabled = false;
                BtnProcess.Focus();
                return;
            }

            else
            {
                InvalidMSG();
                txtCuNo.Text = string.Empty;
                lblCuType.Text = string.Empty;
                lblCuNumber.Text = string.Empty;
                lblCuName.Text = string.Empty;
                txtCuNo.Focus();
            }




        }


    }
}