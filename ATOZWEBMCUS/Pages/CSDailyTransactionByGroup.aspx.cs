﻿using ATOZWEBMCUS.WebSessionStore;
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
    public partial class CSDailyTransactionByGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvDetailInfo.Visible = false;
                string TranDate = (string)Session["STranDate"];
                string Func = (string)Session["SFuncOpt"];
                string Module = (string)Session["SModule"];
                string VchNo = (string)Session["SVchNo"];
                string Cflag = (string)Session["CFlag"];
                string Ctrlflag = (string)Session["SControlFlag"];

                lblFuncOpt.Text = Func;
                lblModule.Text = Module;

                CFlag.Text = Cflag;
                lblCtrlFlag.Text = Ctrlflag;

                lblTotalAmt.Visible = false;
                txtTotalAmt.Visible = false;

                //DateTime tdt = Converter.GetDateTime(TranDate);
                //string tdate = tdt.ToString("dd/MM/yyyy");

                lblTranDate.Text = TranDate;
                lblVchNo.Text = VchNo;

                gvSearchCUInfo.Visible = false;
                gvSearchMEMInfo.Visible = false;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                gvSearchCUDetail();
                if (CFlag.Text == "1")
                {
                    string RCuNo = (string)Session["RCreditUNo"];
                    lblCuNumber.Text = RCuNo;
                    txtCreditUNo.Text = Converter.GetString(lblCuNumber.Text);
                    string RMemNo = (string)Session["RMemNo"];
                    txtMemNo.Text = RMemNo;

                    txtCreditUNo.ReadOnly = true;
                    txtMemNo.ReadOnly = true;
                    txtSearchCuName.ReadOnly = true;

                    BtnSearch_Click(this, EventArgs.Empty);
                }
                else
                {
                    txtCreditUNo.ReadOnly = false;
                    txtMemNo.ReadOnly = false;
                    txtSearchCuName.ReadOnly = false;
                }


                if (lblModule.Text == "4")
                {
                    lblCUNum.Visible = false;
                    txtCreditUNo.Visible = false;
                    lblCuName.Visible = false;
                    lblOldCuNo.Visible = false;
                    txtOldCuNo.Visible = false;
                    lblOldMemNo.Visible = false;
                    txtOldMemNo.Visible = false;
                    lblMemNo.Text = "Staff Code";
                    txtMemNo.Focus();
                }
                else
                {
                    txtCreditUNo.Focus();
                }

            }
        }
        protected void gvDetail()
        {
            GenerateTransactionCode();

            string sqlquery3 = "SELECT distinct AccType,TrnCodeDesc,AccNo,AccOldNumber FROM WFCSGROUPACCOUNT WHERE UserId='" + lblID.Text + "' ORDER BY AccNo";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void gvSearchCUDetail()
        {

            string sqlquery3 = "SELECT CuName, lTrim(str(CuType)+lTrim(str(CuNo))) As CuNo,CuOldCuNo,CuOld1CuNo,CuOld2CuNo FROM A2ZCUNION";
            gvSearchCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSearchCUInfo, "A2ZCSMCUS");
        }

        protected void gvSearchMEMDetail()
        {

            string sqlquery3 = "SELECT MemName, MemNo,MemOldMemNo,MemOld1MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCuNo.Text + "' ";
            gvSearchMEMInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSearchMEMInfo, "A2ZCSMCUS");
        }

        private void GenerateTransactionCode()
        {
            var prm = new object[7];

            if (lblModule.Text == "4")
            {
                prm[0] = 0;
                prm[1] = 0;
            }
            else
            {
                prm[0] = lblCuType.Text;
                prm[1] = lblCuNo.Text;
            }
            prm[2] = txtMemNo.Text;
            prm[3] = 1; //lblfunc;
            prm[4] = lblID.Text;
            prm[5] = 1; //lblmodule

            prm[6] = 1;// lblCtrlFlag.Text;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetGroupAccountInfo", prm, "A2ZCSMCUS"));
            if (result == 0)
            {
                string qry = "SELECT Id,TrnCode FROM WFCSGROUPACCOUNT  WHERE UserId='" + lblID.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count == 0)
                {
                    InvalidAcc();
                    txtCreditUNo.Text = string.Empty;
                    lblCuName.Text = string.Empty;
                    txtMemNo.Text = string.Empty;
                    lblMemName.Text = string.Empty;
                    return;
                }
            }

        }

        private void InvalidAcc()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }

        private void InvalidCuNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        private void InvalidMemNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Depositor No.');", true);
            return;
        }

        private void InvalidStaffNo()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Staff No.');", true);
            return;
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
            b = string.Format("New Credit Union Type : {0}", lblCTypeName.Text);
            c = string.Format(lblCNo.Text);
            d = string.Format(lblCType.Text);
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
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtCreditUNo.Text != string.Empty && txtMemNo.Text == string.Empty)
            {
                NewMemberInfoHelp();
                return;
            }
            else if (txtOldCuNo.Text != string.Empty && txtOldMemNo.Text == string.Empty)
            {
                OldMemberInfoHelp();
                return;
            }
            else
            {
                if (lblModule.Text == "4" && txtMemNo.Text != string.Empty)
                {
                    lblCuType.Text = "0";
                    lblCuNo.Text = "0";

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNo.Text);
                    int MNo = Converter.GetInteger(txtMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                        gvDetail();
                        //MoveAccDescription();
                    }
                    else
                    {
                        InvalidStaffNo();
                        txtMemNo.Text = string.Empty;
                        txtMemNo.Focus();
                        return;
                    }

                }
                else
                {
                    if (txtCreditUNo.Text != string.Empty)
                    {

                        string c = "";
                        int a = txtCreditUNo.Text.Length;

                        string b = txtCreditUNo.Text;
                        c = b.Substring(0, 1);
                        int re = Converter.GetSmallInteger(c);
                        int dd = a - 1;
                        string d = b.Substring(1, dd);
                        int re1 = Converter.GetSmallInteger(d);

                        Int16 CType = Converter.GetSmallInteger(re);
                        lblCuType.Text = Converter.GetString(CType);
                        int CNo = Converter.GetSmallInteger(re1);
                        lblCuNo.Text = Converter.GetString(CNo);

                        A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                        if (get5DTO.NoRecord > 0)
                        {
                            lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                            lblCuNumber.Text = lblCuType.Text + lblCuNo.Text;
                            txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

                            lblCType.Text = Converter.GetString(get5DTO.CuType);
                            lblCTypeName.Text = Converter.GetString(get5DTO.CreditUnionName);
                            lblCNo.Text = Converter.GetString(get5DTO.CreditUnionNo);

                            if (get5DTO.CuStatus == 9)
                            {
                                if (get5DTO.CuReguCuType == 0)
                                {
                                    lblCTypeName.Text = Converter.GetString(get5DTO.CuAssoCuTypeName);
                                    lblCNo.Text = Converter.GetString(get5DTO.CuAssoCuNo);
                                    lblCType.Text = Converter.GetString(get5DTO.CuAssoCuType);
                                }
                                else
                                {
                                    lblCTypeName.Text = Converter.GetString(get5DTO.CuReguCuTypeName);
                                    lblCNo.Text = Converter.GetString(get5DTO.CuReguCuNo);
                                    lblCType.Text = Converter.GetString(get5DTO.CuReguCuType);
                                }

                                DisplayMessage();
                                txtCreditUNo.Text = string.Empty;
                                txtCreditUNo.Focus();
                                return;
                            }

                            Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                            int CUNo = Converter.GetInteger(lblCuNo.Text);
                            int MNo = Converter.GetInteger(txtMemNo.Text);
                            A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                            if (get6DTO.NoRecord > 0)
                            {
                                lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                                gvSearchCUInfo.Visible = false;
                                gvSearchMEMInfo.Visible = false;
                                gvDetailInfo.Visible = true;
                                gvDetail();
                                //MoveAccDescription();
                            }
                            else
                            {
                                InvalidMemNo();
                                txtMemNo.Text = string.Empty;
                                txtMemNo.Focus();
                                return;
                            }
                        }
                        else
                        {
                            InvalidCuNo();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }


                    }

                    if (txtOldCuNo.Text != string.Empty)
                    {
                        int CN = Converter.GetInteger(txtOldCuNo.Text);

                        hdnCuNumber.Text = Converter.GetString(CN);

                        A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetOldInfo(CN));

                        //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                        if (getDTO.NoRecord > 0)
                        {
                            lblCuType.Text = Converter.GetString(getDTO.CuType);
                            lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                            lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                            txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

                            lblCType.Text = Converter.GetString(getDTO.CuType);
                            lblCTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                            lblCNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                            if (getDTO.CuStatus == 9)
                            {
                                if (getDTO.CuReguCuType == 0)
                                {
                                    lblCTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                    lblCNo.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                    lblCType.Text = Converter.GetString(getDTO.CuAssoCuType);
                                }
                                else
                                {
                                    lblCTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                    lblCNo.Text = Converter.GetString(getDTO.CuReguCuNo);
                                    lblCType.Text = Converter.GetString(getDTO.CuReguCuType);
                                }

                                DisplayMessage();
                                txtCreditUNo.Text = string.Empty;
                                txtCreditUNo.Focus();
                                return;
                            }



                            int MemNumber = Converter.GetInteger(txtOldMemNo.Text);
                            int CuNumber = Converter.GetInteger(hdnCuNumber.Text);
                            A2ZMEMBERDTO get1DTO = new A2ZMEMBERDTO();
                            get1DTO = (A2ZMEMBERDTO.GetInfoOldMember(CuNumber, MemNumber));

                            if (get1DTO.NoRecord > 0)
                            {
                                txtMemNo.Text = Converter.GetString(get1DTO.MemberNo);
                                lblMemName.Text = Converter.GetString(get1DTO.MemberName);
                                gvSearchCUInfo.Visible = false;
                                gvSearchMEMInfo.Visible = false;
                                gvDetailInfo.Visible = true;
                                gvDetail();
                                //MoveAccDescription();
                            }
                            else
                            {
                                InvalidMemNo();
                                txtMemNo.Text = string.Empty;
                                txtMemNo.Focus();
                                return;
                            }

                        }
                        else
                        {
                            InvalidCuNo();
                            txtOldCuNo.Text = string.Empty;
                            txtOldCuNo.Focus();
                            return;
                        }

                    }
                }
            }
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }

        protected void gvSearchCUInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }

        protected void gvSearchMEMInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1600px");
            }
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            //Session["flag"] = "1";
            //Session["RTranDate"] = lblTranDate.Text;
            //Session["RFuncOpt"] = lblFuncOpt.Text;
            //Session["RModule"] = lblModule.Text;
            //Session["RVchNo"] = lblVchNo.Text;
            //Session["NewAccNo"] = string.Empty;

            //Session["RCreditUNo"] = string.Empty;
            //Session["RMemNo"] = string.Empty;
            //Session["CFlag"] = string.Empty;


            //if (lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //      "click", @"<script>window.opener.location.href='CSDailyTransactionByAccount.aspx'; self.close();</script>", false);
            //}
            //else if (lblCtrlFlag.Text == "1")
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //         "click", @"<script>window.opener.location.href='CSAccountStatement.aspx'; self.close();</script>", false);
            //}
            //else if (lblCtrlFlag.Text == "2")
            //{
            //    Session["CFlag"] = string.Empty;
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //        "click", @"<script>window.opener.location.href='CSAccountEditMaintenance.aspx'; self.close();</script>", false);
            //}

            Response.Redirect("A2ZERPModule.aspx");

        }

        protected void SumValue()
        {
            double sum = 0;
           
            for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
            {
                TextBox txtDepositeAmt = (TextBox)gvDetailInfo.Rows[i].Cells[4].FindControl("txtDeposit");
                double deposit = Converter.GetDouble(txtDepositeAmt.Text);
                sum += deposit;
            }
            lblTotalAmt.Visible = true;
            txtTotalAmt.Visible = false;
            txtTotalAmt.Text = Convert.ToString(String.Format("{0:0,0.00}", sum));
           
        }
        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvDetailInfo.SelectedRow;
            lblAccNo.Text = row.Cells[2].Text;
            SumValue();
            //Session["NewAccNo"] = lblAccNo.Text;
            //Session["flag"] = "1";
            //Session["RTranDate"] = lblTranDate.Text;
            //Session["RFuncOpt"] = lblFuncOpt.Text;
            //Session["RModule"] = lblModule.Text;
            //Session["RVchNo"] = lblVchNo.Text;

            //Session["Rflag"] = "1";
            //Session["RCreditUNo"] = lblCuNumber.Text;
            //Session["RMemNo"] = txtMemNo.Text;
            //Session["CFlag"] = "1";

            //if (lblCtrlFlag.Text == "0" || lblCtrlFlag.Text == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //    "click", @"<script>window.opener.location.href='CSDailyTransactionByAccount.aspx'; self.close();</script>", false);
            //}
            //else if (lblCtrlFlag.Text == "1")
            //{
            //    Session["CFlag"] = string.Empty;
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //        "click", @"<script>window.opener.location.href='CSAccountStatement.aspx'; self.close();</script>", false);
            //}
            //else if (lblCtrlFlag.Text == "2")
            //{
            //    Session["CFlag"] = string.Empty;
            //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            //        "click", @"<script>window.opener.location.href='CSAccountEditMaintenance.aspx'; self.close();</script>", false);
            //}

        }

        private void NewMemberInfoHelp()
        {
            string c = "";
            int a = txtCreditUNo.Text.Length;

            string b = txtCreditUNo.Text;
            c = b.Substring(0, 1);
            int re = Converter.GetSmallInteger(c);
            int dd = a - 1;
            string d = b.Substring(1, dd);
            int re1 = Converter.GetSmallInteger(d);

            Int16 CType = Converter.GetSmallInteger(re);
            lblCuType.Text = Converter.GetString(CType);
            int CNo = Converter.GetSmallInteger(re1);
            lblCuNo.Text = Converter.GetString(CNo);

            A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
            if (get5DTO.NoRecord > 0)
            {
                lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                lblCuNumber.Text = lblCuType.Text + lblCuNo.Text;
                txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

                lblCType.Text = Converter.GetString(get5DTO.CuType);
                lblCTypeName.Text = Converter.GetString(get5DTO.CreditUnionName);
                lblCNo.Text = Converter.GetString(get5DTO.CreditUnionNo);

                if (get5DTO.CuStatus == 9)
                {
                    if (get5DTO.CuReguCuType == 0)
                    {
                        lblCTypeName.Text = Converter.GetString(get5DTO.CuAssoCuTypeName);
                        lblCNo.Text = Converter.GetString(get5DTO.CuAssoCuNo);
                        lblCType.Text = Converter.GetString(get5DTO.CuAssoCuType);
                    }
                    else
                    {
                        lblCTypeName.Text = Converter.GetString(get5DTO.CuReguCuTypeName);
                        lblCNo.Text = Converter.GetString(get5DTO.CuReguCuNo);
                        lblCType.Text = Converter.GetString(get5DTO.CuReguCuType);
                    }

                    DisplayMessage();
                    txtCreditUNo.Text = string.Empty;
                    txtCreditUNo.Focus();
                    return;
                }

                gvSearchMEMDetail();
                gvSearchMEMInfo.Visible = true;
            }

        }

        private void OldMemberInfoHelp()
        {
            int CN = Converter.GetInteger(txtOldCuNo.Text);

            hdnCuNumber.Text = Converter.GetString(CN);

            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetOldInfo(CN));
            if (getDTO.NoRecord > 0)
            {
                lblCuType.Text = Converter.GetString(getDTO.CuType);
                lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

                lblCType.Text = Converter.GetString(getDTO.CuType);
                lblCTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                lblCNo.Text = Converter.GetString(getDTO.CreditUnionNo);

                if (getDTO.CuStatus == 9)
                {
                    if (getDTO.CuReguCuType == 0)
                    {
                        lblCTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                        lblCNo.Text = Converter.GetString(getDTO.CuAssoCuNo);
                        lblCType.Text = Converter.GetString(getDTO.CuAssoCuType);
                    }
                    else
                    {
                        lblCTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                        lblCNo.Text = Converter.GetString(getDTO.CuReguCuNo);
                        lblCType.Text = Converter.GetString(getDTO.CuReguCuType);
                    }

                    DisplayMessage();
                    txtCreditUNo.Text = string.Empty;
                    txtCreditUNo.Focus();
                    return;
                }

                gvSearchMEMDetail();
                gvSearchMEMInfo.Visible = true;
            }
        }
        protected void gvSearchCUInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvSearchCUInfo.SelectedRow;
            txtCreditUNo.Text = row.Cells[1].Text;
            lblCuNumber.Text = row.Cells[1].Text;
            lblCuName.Text = row.Cells[0].Text;

            gvSearchCUInfo.Visible = false;
            txtSearchCuName.Text = string.Empty;

            string c = "";
            int a = txtCreditUNo.Text.Length;

            string b = txtCreditUNo.Text;
            c = b.Substring(0, 1);
            int re = Converter.GetSmallInteger(c);
            int dd = a - 1;
            string d = b.Substring(1, dd);
            int re1 = Converter.GetSmallInteger(d);

            Int16 CType = Converter.GetSmallInteger(re);
            lblCuType.Text = Converter.GetString(CType);
            int CNo = Converter.GetSmallInteger(re1);
            lblCuNo.Text = Converter.GetString(CNo);

            //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

            gvSearchMEMDetail();

            gvSearchMEMInfo.Visible = true;


            //BtnSearch_Click(this, EventArgs.Empty);


        }

        protected void gvSearchMEMInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvSearchMEMInfo.SelectedRow;
            txtMemNo.Text = row.Cells[1].Text;
            lblMemName.Text = row.Cells[0].Text;

            gvSearchMEMInfo.Visible = false;

            txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);

            gvDetailInfo.Visible = true;
            gvDetail();


            //BtnSearch_Click(this, EventArgs.Empty);


        }
        protected void txtSearchCuName_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT  CuName, lTrim(str(CuType)+lTrim(str(CuNo))) As CuNo,CuOldCuNo,CuOld1CuNo,CuOld2CuNo FROM A2ZCUNION where  CuStatus = 0 AND CuName like '" + txtSearchCuName.Text + "%'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            gvSearchCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry, gvSearchCUInfo, "A2ZCSMCUS");

            gvSearchMEMInfo.Visible = false;
            gvDetailInfo.Visible = false;

            gvSearchCUInfo.Visible = true;

            //if(dt.Rows.Count>0)
            //{
            //    gvSearchDetail();
            //}

        }

      

    }
}