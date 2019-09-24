using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Spell;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    txtCreditUNo.Focus();

                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    string Ctrflag = (string)Session["flag"];
                    string Nflag = (string)Session["NFlag"];
                    lblFlag.Text = Ctrflag;
                    if (lblFlag.Text == "1" && Nflag == "1")
                    {
                        string CuNo = (string)Session["RCreditUNo"];
                        txtCreditUNo.Text = CuNo;
                        string MemNo = (string)Session["RMemNo"];
                        txtMemNo.Text = MemNo;
                        txtCreditUNo_TextChanged(this, EventArgs.Empty);
                        txtMemNo_TextChanged(this, EventArgs.Empty);
                        SessionRemove1();

                    }

                    string CtrTrflag = (string)Session["TrnFlag"];

                    CtrTrlFlag.Text = CtrTrflag;

                    if (lblFlag.Text == "1" && CtrTrlFlag.Text == "1")
                    {
                        string RCuNo = (string)Session["RCreditUNo"];
                        txtTrnCuNo.Text = RCuNo;

                        string Ctype = (string)Session["CType"];
                        lblCuType.Text = Ctype;
                        string Cno = (string)Session["CNo"];
                        lblCuNo.Text = Cno;

                        string RMemNo = (string)Session["RMemNo"];
                        txtTrnMemNo.Text = RMemNo;
                        txtTrnCuNo_TextChanged(this, EventArgs.Empty);
                        txtTrnMemNo_TextChanged(this, EventArgs.Empty);

                        string CuNo = (string)Session["CuNo"];
                        txtCreditUNo.Text = CuNo;
                        string CuName = (string)Session["CuName"];
                        lblCuName.Text = CuName;
                        string MemNo = (string)Session["MemNo"];
                        txtMemNo.Text = MemNo;
                        string MemName = (string)Session["MemName"];
                        lblMemName.Text = MemName;
                        string AccType = (string)Session["AccType"];
                        txtAccType.Text = AccType;
                        string AccNo = (string)Session["AccNo"];
                        txtAccNo.Text = AccNo;
                        string dAccNo = (string)Session["ddlAccNo"];
                        ddlAccNo.SelectedValue = dAccNo;
                        SessionRemove2();



                    }



                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        protected void SessionRemove1()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["NFlag"] = string.Empty;

        }
        protected void SessionRemove2()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["TrnFlag"] = string.Empty;
            Session["CuNo"] = string.Empty;
            Session["CType"] = string.Empty;
            Session["CNo"] = string.Empty;
            Session["CuName"] = string.Empty;
            Session["MemNo"] = string.Empty;
            Session["MemName"] = string.Empty;
            Session["AccType"] = string.Empty;
            Session["AccNo"] = string.Empty;
            Session["ddlAccNo"] = string.Empty;
        }
        protected void AccountNoDropdown()
        {

            string sqlquery = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccStatus < 98";
            ddlAccNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccNo, "A2ZCSMCUS");


        }

        protected void DisplayMessage()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";

            a = "Credit Union No. Already Transfered";
            e = "New Credit Union No.";
            b = string.Format("New Credit Union Type : {0}", lblCuTypeName.Text);
            c = string.Format(lblCuNo.Text);
            d = string.Format(lblCuType.Text);
            X = "-";

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e;
            Msg += d;
            Msg += X;
            Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }
        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {
                    //int CN = Converter.GetInteger(txtCreditUNo.Text);

                    //hdnCuNumber.Value = Converter.GetString(CN);

                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblCuType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblCuNo.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    //if (getDTO.CreditUnionNo > 0)

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblCuNo.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblCuNo.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }

                            DisplayMessage();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }



                        txtMemNo.Focus();
                        txtCreditUNo.Text = lblCuType.Text + "-" + lblCuNo.Text;

                    }
                    else
                    {
                        InvalidCuNo();
                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCreditUNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtMemNo.Text != string.Empty && txtCreditUNo.Text != string.Empty)
                {

                    Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblCuNo.Text);
                    int MemNumber = Converter.GetInteger(txtMemNo.Text);
                    int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        lblMemName.Text = Converter.GetString(getDTO.MemberName);
                        //ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                        txtAccType.Focus();

                    }
                    else
                    {

                        InvalidCuNo();
                        txtMemNo.Text = string.Empty;
                        txtMemNo.Focus();

                    }
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtMemNo_TextChanged Problem');</script>");
                //throw ex;
            }

        }


        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                Int16 AccType = Converter.GetSmallInteger(txtAccType.Text);
                A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                if (get1DTO.AccTypeCode > 0)
                {
                    txtAccType.Text = Converter.GetString(get1DTO.AccTypeCode);
                    lblAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                    lblAccFlag.Text = Converter.GetString(get1DTO.AccFlag);
                    
                    lblAccessType1.Text = Converter.GetString(get1DTO.AccessT1);
                    lblAccessType2.Text = Converter.GetString(get1DTO.AccessT2);
                    lblAccessType3.Text = Converter.GetString(get1DTO.AccessT3);
                }
                string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccStatus < 98";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    ddlAccNo.SelectedValue = Converter.GetString(dt.Rows[0]["AccNo"]);
                    AccountNoDropdown();
                    txtTrnCuNo.Focus();
                }
                else
                {
                    InvalidAccount();
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string qry = "SELECT Id,AccNo,AccStatus FROM A2ZACCOUNT where AccNo='" + txtAccNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    lblAccStatus.Text = Converter.GetString(dt.Rows[0]["AccStatus"]);
                    if (lblAccStatus.Text == "99")
                    {
                        ClosedAccount();
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        return;
                    }

                    if (lblAccStatus.Text == "98")
                    {
                        TransferedAccount();
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        return;
                    }
                    
                    txtAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                    ddlAccNo.SelectedValue = Converter.GetString(dt.Rows[0]["AccNo"]);
                }
                else
                {
                    InvalidAccount();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                //throw ex;
            }

        }
        protected void ddlAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAccNo.Text = Converter.GetString(ddlAccNo.SelectedValue);
            txtTrnCuNo.Focus();

        }


        protected void txtTrnCuNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtTrnCuNo.Text != string.Empty)
                {

                    int CN = Converter.GetInteger(txtTrnCuNo.Text);

                    hdnCuNumber.Text = Converter.GetString(CN);

                    string c = "";
                    int a = txtTrnCuNo.Text.Length;

                    string b = txtTrnCuNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);

                    Int16 CuType = Converter.GetSmallInteger(re);
                    lblTrnferCuType.Text = Converter.GetString(CuType);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblTrnferCuNo.Text = Converter.GetString(CNo);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));
                    //if (getDTO.CreditUnionNo > 0)

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));
                    if (getDTO.NoRecord > 0)
                    {
                        lblTrnferCuType.Text = Converter.GetString(getDTO.CuType);
                        lblCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblTrnferCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblTrfCuName.Text = Converter.GetString(getDTO.CreditUnionName);

                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblCuNo.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else
                            {
                                lblCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblCuNo.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblCuType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }

                            DisplayMessage();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }


                        txtTrnMemNo.Focus();
                        txtTrnCuNo.Text = lblTrnferCuType.Text + "-" + lblTrnferCuNo.Text;

                    }
                    else
                    {
                        InvalidCuNo();
                        txtCreditUNo.Text = string.Empty;
                        txtCreditUNo.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnCuNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void txtTrnMemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (txtTrnMemNo.Text != string.Empty && txtTrnCuNo.Text != string.Empty)
                {

                    Int16 CuType = Converter.GetSmallInteger(lblTrnferCuType.Text);
                    int CNo = Converter.GetSmallInteger(lblTrnferCuNo.Text);
                    int MemNumber = Converter.GetInteger(txtTrnMemNo.Text);
                    int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CNo, MemNumber));

                    //A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();
                    //getDTO = (A2ZMEMBERDTO.GetInfoMember(CuType, CNo, CuNumber, MemNumber));

                    if (getDTO.NoRecord > 0)
                    {
                        txtTrnMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        lblTrfMemName.Text = Converter.GetString(getDTO.MemberName);
                        //ddlTrnMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);


                        string a = txtAccNo.Text;
                        string b = a.Substring(15, 1);

                        if (txtAccType.Text == "53")
                        {
                            string qry1 = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblTrnferCuType.Text + "' and CuNo='" + lblTrnferCuNo.Text + "' and MemNo='" + txtTrnMemNo.Text + "' and AccType='" + txtAccType.Text + "' and Right(AccNo,1) = '" + b + "'";
                            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                            if (dt1.Rows.Count > 0)
                            {
                                NotAllowAccount();
                                txtTrnMemNo.Text = string.Empty;
                                txtTrnMemNo.Focus();
                                return;
                            }
                            else 
                            {
                                return;
                            }
                        }


                        string qry = "SELECT Id,AccNo FROM A2ZACCOUNT where CuType='" + lblTrnferCuType.Text + "' and CuNo='" + lblTrnferCuNo.Text + "' and MemNo='" + txtTrnMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            if (lblAccFlag.Text == "1" || lblAccFlag.Text == "2")
                            {
                                MultiAccount();
                                txtTrnMemNo.Text = string.Empty;
                                txtTrnMemNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (lblAccessType1.Text == "0" && lblTrnferCuType.Text == "1")
                            {
                                NotAllowAccount();
                                txtTrnMemNo.Text = string.Empty;
                                txtTrnMemNo.Focus();
                                return;
                            }
                            if (lblAccessType2.Text == "0" && lblTrnferCuType.Text == "2")
                            {
                                NotAllowAccount();
                                txtTrnMemNo.Text = string.Empty;
                                txtTrnMemNo.Focus();
                                return;
                            }
                            if (lblAccessType3.Text == "0" && lblTrnferCuType.Text == "3")
                            {
                                NotAllowAccount();
                                txtTrnMemNo.Text = string.Empty;
                                txtTrnMemNo.Focus();
                                return;
                            }
                        }
                        

                    }
                    else
                    {
                        InvalidMemNo();

                        txtTrnMemNo.Text = string.Empty;
                        txtTrnMemNo.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtTrnMemNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }




        protected void GetAccountCount()
        {
            string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT where CuType='" + lblTrnferCuType.Text + "' and CuNo='" + lblTrnferCuNo.Text + "' and MemNo='" + txtTrnMemNo.Text + "' and AccType='" + txtAccType.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            int totrec = dt.Rows.Count;
            int newaccno = (totrec + 1);
            hdnNewAccNo.Text = Converter.GetString(newaccno);
        }


        protected void GenerateNewAccNo()
        {
            Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
            A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
            if (getDTO.AccTypeCode > 0)
            {
                hdnAccFlag.Text = Converter.GetString(getDTO.AccFlag);
            }


            //if (hdnAccFlag.Text == "2")
            //{

            GetAccountCount();

            string input1 = Converter.GetString(lblTrnferCuNo.Text).Length.ToString();
            string input2 = Converter.GetString(txtTrnMemNo.Text).Length.ToString();
            string input3 = Converter.GetString(hdnNewAccNo.Text).Length.ToString();

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
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + result1 + lblTrnferCuNo.Text + result2 + txtTrnMemNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 != "5" && input3 == "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + result1 + lblTrnferCuNo.Text + result2 + txtTrnMemNo.Text + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 != "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + result1 + lblTrnferCuNo.Text + txtTrnMemNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 != "4" && input2 == "5" && input3 == "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + result1 + lblTrnferCuNo.Text + txtTrnMemNo.Text + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 != "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + lblTrnferCuNo.Text + result2 + txtTrnMemNo.Text + result3 + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 != "5" && input3 == "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + lblTrnferCuNo.Text + result2 + txtTrnMemNo.Text + hdnNewAccNo.Text;
            }

            if (input1 == "4" && input2 == "5" && input3 != "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + lblTrnferCuNo.Text + txtTrnMemNo.Text + result3 + hdnNewAccNo.Text;
            }
            if (input1 == "4" && input2 == "5" && input3 == "4")
            {
                lblTrnferAccNo.Text = txtAccType.Text + lblTrnferCuType.Text + lblTrnferCuNo.Text + txtTrnMemNo.Text + hdnNewAccNo.Text;
            }

            //}
            //else 
            //{
            //    lblTrnferAccNo.Text = txtAccNo.Text;
            //}

        }

        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {

                if (lblCuType.Text == lblTrnferCuType.Text && lblCuNo.Text == lblTrnferCuNo.Text && txtMemNo.Text == txtTrnMemNo.Text)
                {
                    InvalidInput();
                    txtTrnCuNo.Text = string.Empty;
                    txtTrnMemNo.Text = string.Empty;
                    txtTrnCuNo.Focus();
                }
                else
                {

                    GenerateNewAccNo();


                    var prm = new object[10];

                    prm[0] = lblCuType.Text;
                    prm[1] = lblCuNo.Text;
                    prm[2] = txtMemNo.Text;
                    prm[3] = txtAccType.Text;
                    prm[4] = txtAccNo.Text;

                    prm[5] = lblTrnferCuType.Text;
                    prm[6] = lblTrnferCuNo.Text;
                    prm[7] = txtTrnMemNo.Text;
                    prm[8] = lblTrnferAccNo.Text;
                    prm[9] = lblID.Text;

                    
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAccountTransferData", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        ClearScreen();
                        Successfull();
                        SessionRemove1();
                    }



                    //string upqry = "UPDATE A2ZACCOUNT SET CuType='" + lblTrnferCuType.Text + "',CuNo='" + lblTrnferCuNo.Text + "' , MemNo='" + txtTrnMemNo.Text + "' WHERE  CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' And AccNo='" + txtAccNo.Text + "'";
                    //int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(upqry, "A2ZCSMCUS"));

                    //string Upqry = "UPDATE A2ZTRANSACTION SET CuType='" + lblTrnferCuType.Text + "',CuNo='" + lblTrnferCuNo.Text + "' , MemNo='" + txtTrnMemNo.Text + "'  WHERE  CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' And AccNo='" + txtAccNo.Text + "'";
                    //int row = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(Upqry, "A2ZCSMCUS"));

                    //string Upqry1 = "UPDATE A2ZCSOPBALANCE SET CuType='" + lblTrnferCuType.Text + "',CuNo='" + lblTrnferCuNo.Text + "' , MemNo='" + txtTrnMemNo.Text + "'  WHERE  CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' And AccNo='" + txtAccNo.Text + "'";
                    //int row1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(Upqry1, "A2ZCSMCUST2014"));


                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnTransfer_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ClearScreen()
        {
            txtCreditUNo.Text = string.Empty;
            txtMemNo.Text = string.Empty;
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtTrnCuNo.Text = string.Empty;
            txtTrnMemNo.Text = string.Empty;
            txtCreditUNo.Focus();


        }
        protected void InvalidCuNo()
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

        protected void InvalidMemNo()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        protected void InvalidAccount()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account');", true);
            return;
        }

        protected void MultiAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Multi Account');", true);
            return;
        }

        protected void NotAllowAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Allowed Account Transfer');", true);
            return;
        }


        protected void ClosedAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Closed Account');", true);
            return;
        }

        protected void TransferedAccount()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Already Transfered');", true);
            return;
        }


        protected void Successfull()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
           

            a = "Account Transfer Successfully Done";
            b = "New Account No. ";
            c = string.Format(lblTrnferAccNo.Text);
            

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;
            

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
        }


        protected void InvalidInput()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Transfer');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Transfer');", true);
            return;
        }
        //protected void Successfull()
        //{
        //    //String csname1 = "PopupScript";
        //    //Type cstype = GetType();
        //    //ClientScriptManager cs = Page.ClientScript;

        //    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //    //{
        //    //    String cstext1 = "alert('Account Transfer Successfully Done');";
        //    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //    //}
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfer Successfully Done');", true);
        //    return;
        //}

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            SessionRemove1();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            Session["NFlag"] = "1";
            Session["ExFlag"] = "0";

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }

        protected void BtnTrfHelp_Click(object sender, EventArgs e)
        {
            Session["CuNo"] = txtCreditUNo.Text;
            Session["CType"] = lblCuType.Text;
            Session["CNo"] = lblCuNo.Text;
            Session["CuName"] = lblCuName.Text;
            Session["MemNo"] = txtMemNo.Text;
            Session["MemName"] = lblMemName.Text;
            Session["AccType"] = txtAccType.Text;
            Session["AccNo"] = txtAccNo.Text;
            Session["ddlAccNo"] = ddlAccNo.SelectedValue;
            Session["TrnFlag"] = "1";
            Session["ExFlag"] = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);

        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Label1.Text = Spell.SpellAmount.InWrods(Convert.ToDecimal(TextBox1.Text));
        //}

    }
}