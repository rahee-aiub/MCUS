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

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountTransactionTransfer : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    hdnPrmValue.Text = Request.QueryString["a%b"];
                    string b = hdnPrmValue.Text;
                    hdnFuncOpt.Text = b.Substring(0, 2);
                    hdnModule.Text = b.Substring(2, 1);

                    txtVchNo.Focus();


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    CtrlProcDate.Text = date;

                    txtTranDate.Text = date;


                    //txtTranDate.Text = string.Empty;

                    hdnUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    //divTrnfer.Visible = false;
                    gvTrnferInfo.Visible = false;
                    BtnTransfer.Visible = false;

                    BtnTrfSearch.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }



        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtTranDate.Text != string.Empty && txtVchNo.Text != string.Empty)
            {

                DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                gvDetailInfo.Visible = true;
                var prm = new object[4];

                prm[0] = txtVchNo.Text;
                prm[1] = hdnUserId.Text;
                prm[2] = 1;
                prm[3] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetTrfTransaction", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    string qry = "SELECT Id,AccNo,PayType FROM WF_TRFTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "' AND TrnDate='" + opdate + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        gvPreview();

                        for (int i = 0; i < gvDetailInfo.Rows.Count; i++)
                        {
                            Label lblacctype = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("AccType");
                            OrgAccType.Text = Converter.GetString(lblacctype.Text);
                        }

                    }
                    else
                    {
                        InvalidVchTrn();
                        txtVchNo.Text = string.Empty;
                        txtVchNo.Focus();
                    }

                }
                else
                {
                    InvalidVchTrn();
                    txtVchNo.Text = string.Empty;
                    txtVchNo.Focus();
                }
            }

        }


        protected void UnPostValue()
        {
            lblUnPostDataDr.Text = string.Empty;

            //DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT SUM(TrnDebit) AS 'AmountDr' FROM A2ZTRANSACTION WHERE TrnProcStat = 1 AND TrnCSGL = 0 AND TrnFlag = 0 AND CuType ='" + lblCuType.Text + "' AND CuNo ='" + lblCuNo.Text + "' AND MemNo ='" + txtMemNo.Text + "' AND AccType ='" + txtAccType.Text + "' AND AccNo ='" + txtAccNo.Text + "' AND TrnDate='" + opdate + "'", "A2ZCSMCUS");
            //if (dt.Rows.Count > 0)
            //{
            //    lblUnPostDataDr.Value = Convert.ToString(String.Format("{0:0,0.00}", dt.Rows[0]["AmountDr"]));
            //}



            double LadgerBalance = Converter.GetDouble(CtrlBalance.Text);
            double AmtDebit = Converter.GetDouble(lblUnPostDataDr.Text);


            CtrlAvailBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", (LadgerBalance - AmtDebit)));
        }

        protected void txtTrnCuNo_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtTrnCuNo.Text != string.Empty)
                {
                    BtnTransfer.Visible = false;


                    A2ZCUNIONDTO getDTO = new A2ZCUNIONDTO();

                    if (chkOldSearch.Checked == true)
                    {
                        int CN = Converter.GetInteger(txtTrnCuNo.Text);
                        hdnCuNumber.Text = Converter.GetString(CN);

                        getDTO = (A2ZCUNIONDTO.GetOldInfo(CN));
                    }
                    else
                    {
                        string c = "";
                        int a = txtTrnCuNo.Text.Length;

                        string b = txtTrnCuNo.Text;
                        c = b.Substring(0, 1);
                        int re = Converter.GetSmallInteger(c);
                        int dd = a - 1;
                        string d = b.Substring(1, dd);

                        int re1 = Converter.GetSmallInteger(d);

                        Int16 CType = Converter.GetSmallInteger(re);
                        lblCuType.Text = Converter.GetString(CType);
                        int CNo = Converter.GetSmallInteger(re1);
                        lblCuNo.Text = Converter.GetString(CNo);

                        getDTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));

                    }

                    if (getDTO.NoRecord > 0)
                    {
                        lblTrnferCuType.Text = Converter.GetString(getDTO.CuType);
                        lblTrnferCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);


                        txtTrnMemNo.Focus();
                        txtTrnCuNo.Text = lblTrnferCuType.Text + "-" + lblTrnferCuNo.Text;
                    }
                    else
                    {
                        InvalidCuNo();
                        txtTrnCuNo.Text = string.Empty;
                        txtTrnCuNo.Focus();
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

                if (txtTrnMemNo.Text != string.Empty)
                {
                    BtnTransfer.Visible = false;

                    A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                    if (chkOldSearch.Checked == true)
                    {
                        int MemNumber = Converter.GetInteger(txtTrnMemNo.Text);
                        int CuNumber = Converter.GetInteger(hdnCuNumber.Text);

                        getDTO = (A2ZMEMBERDTO.GetInfoOldMember(CuNumber, MemNumber));
                    }
                    else
                    {
                        Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                        int CUNo = Converter.GetInteger(lblCuNo.Text);
                        int MNo = Converter.GetInteger(txtTrnMemNo.Text);

                        getDTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));

                    }

                    if (getDTO.NoRecord > 0)
                    {
                        txtTrnMemNo.Text = Converter.GetString(getDTO.MemberNo);
                        lblMemName.Text = Converter.GetString(getDTO.MemberName);

                        BtnTrfSearch.Visible = true;
                    }
                    else
                    {
                        InvalidMemNo();
                        BtnTrfSearch.Visible = false;
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




        protected void UpdatedSucessfullyMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            if (CtrlProcStat.Text == "0")
            {
                a = "    TRANSACTION SUCESSFULLY DONE";
            }
            if (CtrlProcStat.Text == "1")
            {
                a = "    TRANSACTION INPUT DONE";
            }
            b = "Generated New Voucher No.";
            c = string.Format(CtrlVoucherNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

            //--------------------------
            //string a = "";


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (CtrlProcStat.Text == "0")
            //{
            //    a = "    TRANSACTION SUCESSFULLY DONE";
            //}
            //if (CtrlProcStat.Text == "1")
            //{
            //    a = "    TRANSACTION INPUT DONE";
            //}
            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVoucherNo.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append(c);



            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }


        protected void AccessAmountMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);
            return;

        }

        protected void DuplicateVchMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;

        }

        protected void TrnVchDeplicate()
        {
            DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text + "' and TrnDate ='" + opdate + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
            {
                DuplicateVchMSG();
                txtVchNo.Text = string.Empty;
                txtVchNo.Focus();
                return;
            }
        }

        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime opdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var prm = new object[3];


                //for (int i = 0; i < gvTrnferInfo.Rows.Count; i++)
                //{
                //    Label fromCuType = (Label)gvTrnferInfo.Rows[i].Cells[8].FindControl("FrmCuType");
                //    lblCuType.Text = fromCuType.Text;
                //    Label fromCuNo = (Label)gvTrnferInfo.Rows[i].Cells[9].FindControl("FrmCuNo");
                //    lblCuNo.Text = fromCuNo.Text;
                //    Label fromMemNo = (Label)gvTrnferInfo.Rows[i].Cells[10].FindControl("FrmMemNo");
                //    hdnfrMemno.Text = fromMemNo.Text;
                //    Label fromAccNo = (Label)gvTrnferInfo.Rows[i].Cells[11].FindControl("FrmAccNo");
                //    hdnfrAccNo.Text = fromAccNo.Text;

                //    Label TranAmt = (Label)gvTrnferInfo.Rows[i].Cells[7].FindControl("Amount");
                //    CtrlBalance.Text = TranAmt.Text;


                //    Label TrCuType = (Label)gvTrnferInfo.Rows[i].Cells[1].FindControl("CuType");
                //    lblTrnferCuType.Text = TrCuType.Text;
                //    Label TrCuNo = (Label)gvTrnferInfo.Rows[i].Cells[2].FindControl("CuNo");
                //    lblTrnferCuNo.Text = TrCuNo.Text;
                //    Label TrMemNo = (Label)gvTrnferInfo.Rows[i].Cells[3].FindControl("MemNo");
                //    hdnToMemNo.Text = TrMemNo.Text;
                //    TextBox TrAccNo = (TextBox)gvTrnferInfo.Rows[i].Cells[4].FindControl("AccNo");
                //    hdnToAccNo.Text = TrAccNo.Text;
                //    Label trAccType = (Label)gvTrnferInfo.Rows[i].Cells[5].FindControl("AccType");
                //    hdnToAccType.Text = trAccType.Text;

                //    Label patype = (Label)gvTrnferInfo.Rows[i].Cells[13].FindControl("Paytype");
                //    lblPayType.Text = patype.Text;

                    prm[0] = hdnUserId.Text;
                    prm[1] = Converter.GetDateToYYYYMMDD(txtTranDate.Text);
                    prm[2] = txtVchNo.Text;

                    //prm[3] = lblCuType.Text;
                    //prm[4] = lblCuNo.Text;
                    //prm[5] = hdnfrMemno.Text;
                    //prm[6] = hdnToAccType.Text;
                    //prm[7] = hdnfrAccNo.Text;

                    //prm[8] = lblTrnferCuType.Text;
                    //prm[9] = lblTrnferCuNo.Text;
                    //prm[10] = hdnToMemNo.Text;
                    //prm[11] = hdnToAccNo.Text;
                    //prm[12] = CtrlBalance.Text;

                    //prm[13] = lblPayType.Text;
                   


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAccTranTransfer", prm, "A2ZCSMCUS"));

                    if (result == 0)
                    {
                        //UpdatedSucessfullyMSG();


                    }

                //}

                ClearScreen();
                Successfull();


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ClearScreen()
        {
            txtTranDate.Text = string.Empty;
            txtVchNo.Text = string.Empty;
            txtTrnCuNo.Text = string.Empty;
            txtTrnMemNo.Text = string.Empty;

            lblCuName.Text = string.Empty;
            lblMemName.Text = string.Empty;


            gvDetailInfo.Visible = false;
            gvTrnferInfo.Visible = false;


            txtVchNo.Focus();


        }
        protected void InvalidCuNo()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        protected void InvalidMemNo()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        protected void InvalidAccNo()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account not Available');", true);
            return;
        }

        protected void InvalidAccount()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account');", true);
            return;
        }
        protected void InvalidInput()
        {
           
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Transfer');", true);
            return;
        }

        protected void InvalidAccountMatch()
        {
           
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Available');", true);
            return;
        }

        protected void InvalidVchTrn()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Voucher Transaction');", true);
            return;
        }
        protected void Successfull()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('A/c Transaction Transfer Successfully Done');", true);
            return;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        private void InvalidFinDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Financial Year');", true);
            return;

        }
        private void InvalidDateMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;

        }

        private void InvalidCurrentMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Current Date');", true);
            return;
        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime opdate2 = Converter.GetDateTime(dto.ProcessDate);
            int Begyear = Converter.GetInteger(dto.FinancialBegYear);


            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Iyear = opdate1.Year;
            int Imonth = opdate1.Month;

            if (Iyear < Begyear)
            {
                InvalidFinDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlProcDate.Text;
                txtTranDate.Focus();
            }

            if (Iyear == Begyear && Imonth < 7)
            {
                InvalidFinDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlProcDate.Text;
                txtTranDate.Focus();
            }

            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlProcDate.Text;
                txtTranDate.Focus();
            }

            if (opdate1 == opdate2)
            {
                InvalidCurrentMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlProcDate.Text;
                txtTranDate.Focus();
            }

        }


        private void gvPreview()
        {
            string Qry = "SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDesc,Abs(GLAmount) as GLAmount FROM WF_TRFTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");
        }

        private void gvTrnferPreview()
        {
            string Qry = "SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDesc,Abs(GLAmount) as GLAmount,CuType,CuNo,MemNo,AccType,AccNo,AccNo,PayType,Id FROM WF_TRFTRANSACTION  WHERE VchNo = '" + txtVchNo.Text + "'";
            gvTrnferInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvTrnferInfo, "A2ZCSMCUS");
        }


        protected void AccNo_TextChanged(object sender, EventArgs e)
        {
            string strqry;
            TextBox b = (TextBox)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label Tctype = (Label)gvTrnferInfo.Rows[r.RowIndex].Cells[1].FindControl("CuType");
            Label Tcno = (Label)gvTrnferInfo.Rows[r.RowIndex].Cells[2].FindControl("CuNo");
            Label Tmno = (Label)gvTrnferInfo.Rows[r.RowIndex].Cells[3].FindControl("MemNo");
            Label Taty = (Label)gvTrnferInfo.Rows[r.RowIndex].Cells[4].FindControl("AccType");
            TextBox accountno = (TextBox)gvTrnferInfo.Rows[r.RowIndex].Cells[5].FindControl("AccNo");
            Label orgAccNo = (Label)gvTrnferInfo.Rows[r.RowIndex].Cells[11].FindControl("OrgAccNo");
            OrgAccNo.Text = orgAccNo.Text;


            int a = accountno.Text.Length;

            if (a != 16)
            {
                accountno.Text = OrgAccNo.Text;

                InvalidAccNo();
                return;
            }

            string aty = accountno.Text.Substring(0, 2);
            string ctype = accountno.Text.Substring(2, 1);
            string cno = accountno.Text.Substring(3, 4);
            string mno = accountno.Text.Substring(7, 5);

            int a1 = Converter.GetInteger(aty);
            int a2 = Converter.GetInteger(ctype);
            int a3 = Converter.GetInteger(cno);
            int a4 = Converter.GetInteger(mno);

            int b1 = Converter.GetInteger(Taty.Text);
            int b2 = Converter.GetInteger(Tctype.Text);
            int b3 = Converter.GetInteger(Tcno.Text);
            int b4 = Converter.GetInteger(Tmno.Text);

            if (a1 != b1 || a2 != b2 || a3 != b3 || a4 != b4)
            {
                InvalidAccNo();
                accountno.Text = OrgAccNo.Text;
            }

            if (Taty.Text == "99")
            {
                accountno.Text = OrgAccNo.Text;
            }
            else
            {
                strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE AccNo='" + accountno.Text + "'";
                DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                if (dt.Rows.Count == 0)
                {
                    InvalidAccNo();
                    accountno.Text = OrgAccNo.Text;
                }
                else
                {
                    for (int i = 0; i < gvTrnferInfo.Rows.Count; i++)
                    {
                        TextBox TrAccNo = (TextBox)gvTrnferInfo.Rows[i].Cells[4].FindControl("AccNo");
                        Label CorgAccNo = (Label)gvTrnferInfo.Rows[i].Cells[11].FindControl("OrgAccNo");

                        if (OrgAccNo.Text == CorgAccNo.Text)
                        {
                            TrAccNo.Text = accountno.Text;
                            CorgAccNo.Text = accountno.Text;
                        }

                    }

                }
            }

        }

        protected void BtnTrfSearch_Click(object sender, EventArgs e)
        {

            BtnTransfer.Visible = false;

            for (int i = 0; i < gvDetailInfo.Rows.Count; i++)
            {
                Label ChkTrnDate = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("TrnDate");
                Label ChkAccType = (Label)gvDetailInfo.Rows[i].Cells[4].FindControl("AccType");
                Label FromAccNo = (Label)gvDetailInfo.Rows[i].Cells[5].FindControl("AccNo");

                DateTime Copdate = DateTime.ParseExact(ChkTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (ChkAccType.Text == "99")
                {
                    //string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND AccNo != '" + FromAccNo.Text + "' AND AccStatus < 98";
                    string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND AccStatus < 98";
                    dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                }
                else
                {
                    //string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND AccNo != '" + FromAccNo.Text + "' AND AccOpenDate < '" + Copdate + "' AND AccStatus < 98";
                    string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND AccOpenDate < '" + Copdate + "' AND AccStatus < 98";
                    dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                }


                if (dt.Rows.Count == 0)
                {
                    gvTrnferInfo.Visible = false;
                    BtnTransfer.Visible = false;

                    txtTrnCuNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    txtTrnMemNo.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    BtnTrfSearch.Visible = false;
                    InvalidAccountMatch();
                    return;
                }
            }

            gvTrnferInfo.Visible = true;
            BtnTransfer.Visible = true;
            gvTrnferPreview();
            for (int i = 0; i < gvTrnferInfo.Rows.Count; i++)
            {
                Label lblId = (Label)gvTrnferInfo.Rows[i].Cells[13].FindControl("Id");
               

                Label lblCuType = (Label)gvTrnferInfo.Rows[i].Cells[1].FindControl("CuType");
                lblCuType.Text = lblTrnferCuType.Text;
                Label lblCuNo = (Label)gvTrnferInfo.Rows[i].Cells[2].FindControl("CuNo");
                lblCuNo.Text = lblTrnferCuNo.Text;
                Label lblmemno = (Label)gvTrnferInfo.Rows[i].Cells[3].FindControl("MemNo");
                lblmemno.Text = txtTrnMemNo.Text;
                Label ChkAccType = (Label)gvTrnferInfo.Rows[i].Cells[4].FindControl("AccType");
                TextBox TrAccNo = (TextBox)gvTrnferInfo.Rows[i].Cells[5].FindControl("AccNo");

                Label FromAccNo = (Label)gvTrnferInfo.Rows[i].Cells[10].FindControl("FrmAccNo");

                Label orgAccNo = (Label)gvTrnferInfo.Rows[i].Cells[11].FindControl("OrgAccNo");
                OrgAccNo.Text = orgAccNo.Text;

                string paytype = OrgAccNo.Text.Substring(13, 3);


                if (ChkAccType.Text == "99")
                {
                    string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND RIGHT(AccNo,3) = '" + paytype + "' AND AccStatus < 98";
                    DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        TrAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                        orgAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                        OrgAccNo.Text = orgAccNo.Text;
                    }
                }
                else
                {
                    if (TrAccNo.Text == lblLastAccNo.Text)
                    {
                        TrAccNo.Text = lblOrgAccNo.Text;
                        orgAccNo.Text = lblOrgAccNo.Text;
                        OrgAccNo.Text = lblOrgAccNo.Text;
                    }
                    else
                    {
                        string strqry = "SELECT AccNo FROM A2ZACCOUNT WHERE CuType='" + lblTrnferCuType.Text + "' AND CuNo='" + lblTrnferCuNo.Text + "' AND MemNo='" + txtTrnMemNo.Text + "' AND AccType= '" + ChkAccType.Text + "' AND AccNo != '" + FromAccNo.Text + "' AND AccStatus < 98";
                        DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            TrAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);

                            lblLastAccNo.Text = OrgAccNo.Text;

                            orgAccNo.Text = Converter.GetString(dt.Rows[0]["AccNo"]);
                            OrgAccNo.Text = orgAccNo.Text;
                            lblOrgAccNo.Text = orgAccNo.Text;
                        }
                    }
                }

                string sqlquery1 = "UPDATE WF_TRFTRANSACTION SET ToCuType= '" + lblTrnferCuType.Text + "',ToCuNo= '" + lblTrnferCuNo.Text + "',ToMemNo= '" + txtTrnMemNo.Text + "',ToAccType= '" + ChkAccType.Text + "',ToAccNo= '" + TrAccNo.Text + "' WHERE Id='" + lblId.Text + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery1, "A2ZCSMCUS"));

                if (rowEffect1 > 0)
                {
                    //gvDetail();
                    //SumValue();
                }



            }


        }



    }
}




