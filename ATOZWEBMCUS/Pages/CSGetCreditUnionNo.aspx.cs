using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSGetCreditUnionNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string TranDate = (string)Session["STranDate"];
                //string Func = (string)Session["SFuncOpt"];
                //string Module = (string)Session["SModule"];
                //string VchNo = (string)Session["SVchNo"];
                string Cflag = (string)Session["CFlag"];
                string Sflag = (string)Session["SFlag"];
                lblIncraseLoan.Text = Sflag;

                string module = (string)Session["Module"];
                lblModule.Text = module;

                string CNflag = (string)Session["ExFlag"];
                lblNflag.Text = CNflag;
                //string CTrnflag = (string)Session["TrnFlag"];
                //lblCtrlTrnFlag.Text = CTrnflag;
                //lblFuncOpt.Text = Func;
                //lblModule.Text = Module;

                //CFlag.Text = Cflag;
                //lblCtrlFlag.Text = Ctrlflag;

                ////DateTime tdt = Converter.GetDateTime(TranDate);
                ////string tdate = tdt.ToString("dd/MM/yyyy");

                //lblTranDate.Text = TranDate;
                //lblVchNo.Text = VchNo;

                txtSearchMemName.Visible = false;
                lblMemNo.Visible = false;
                txtMemNo.Visible = false;
                lblMemName.Visible = false;
                lblOldMemNo.Visible = false;
                txtOldMemNo.Visible = false;

                lblCUNum.Visible = false;
                txtCreditUNo.Visible = false;
                lblCuName.Visible = false;
                txtSearchCuName.Visible = false;


                gvSearchCUInfo.Visible = false;
                gvSearchMEMInfo.Visible = false;

                txtSearchMemName.ReadOnly = true;

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
                    txtOldCuNo.Focus();
                }

            }
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

        protected void gvGetMEMDetail()
        {

            string sqlquery3 = "SELECT MemName, MemNo,MemOldMemNo,MemOld1MemNo,MemOld2MemNo FROM A2ZMEMBER WHERE CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCuNo.Text + "' AND MemNo = '" + txtMemNo.Text + "'";
            gvSearchMEMInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSearchMEMInfo, "A2ZCSMCUS");
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
            if (txtSearchCuName.Text != string.Empty)
            {
                txtSearchCuName_TextChanged(this, EventArgs.Empty);
                return;
            }

            if (txtSearchMemName.Text != string.Empty)
            {
                txtSearchMemName_TextChanged(this, EventArgs.Empty);
                return;
            }
            
            
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
                        gvGetMEMDetail();
                        gvSearchMEMInfo.Visible = true;
                        txtSearchMemName.ReadOnly = false;
                        
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
                               
                                gvGetMEMDetail();
                                gvSearchMEMInfo.Visible = true;
                                txtSearchMemName.ReadOnly = false;
                               
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

                            lblCuNumber.Text = lblCuType.Text + lblCuNo.Text;

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
                                
                                gvGetMEMDetail();
                                gvSearchMEMInfo.Visible = true;
                                txtSearchMemName.ReadOnly = false;
                               
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

        protected void gvSearchMEMBERInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Session["flag"] = "1";
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["CFlag"] = string.Empty;
            Session["ExFlag"] = string.Empty;
            Session["SFlag"] = string.Empty;   
            
            if (lblNflag.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }
            
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

                //GridViewRow row = gvSearchCUInfo.SelectedRow;
                //txtCreditUNo.Text = row.Cells[1].Text;
                //lblCuNumber.Text = row.Cells[1].Text;
                //lblCuName.Text = row.Cells[0].Text;

                gvSearchCUInfo.Visible = false;
                Session["RCreditUNo"] = lblCuNumber.Text;
                Session["RtxtOldCuNo"] = txtOldCuNo.Text;
                //Session["RMemNo"] = txtMemNo.Text;
                //Session["MemName"] = lblMemName.Text;
                Session["flag"] = "1";
                Session["ExFlag"] = string.Empty;
                Session["SFlag"] = string.Empty;
                //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
                if (lblNflag.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
                }



                //gvSearchMEMDetail();
                //gvSearchMEMInfo.Visible = true;
                //txtSearchMemName.ReadOnly = false;
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
                lblCuNumber.Text = lblCuType.Text + lblCuNo.Text;

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

                gvSearchCUInfo.Visible = false;
                Session["RCreditUNo"] = lblCuNumber.Text;
                Session["RtxtOldCuNo"] = txtOldCuNo.Text;
                
                //Session["RMemNo"] = txtMemNo.Text;
                //Session["MemName"] = lblMemName.Text;
                Session["flag"] = "1";
                Session["ExFlag"] = string.Empty;
                Session["SFlag"] = string.Empty;
                //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
                if (lblNflag.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
                }


                //gvSearchMEMDetail();
                //gvSearchMEMInfo.Visible = true;
                //txtSearchMemName.ReadOnly = false;
            }
        }
        protected void gvSearchCUInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvSearchCUInfo.SelectedRow;
            txtCreditUNo.Text = row.Cells[1].Text;
            lblCuNumber.Text = row.Cells[1].Text;
            lblCuName.Text = row.Cells[0].Text;

            gvSearchCUInfo.Visible = false;
            Session["RCreditUNo"] = lblCuNumber.Text;
            //Session["RMemNo"] = txtMemNo.Text;
            //Session["MemName"] = lblMemName.Text;
            Session["flag"] = "1";
            Session["ExFlag"] = string.Empty;
            Session["SFlag"] = string.Empty; 
            //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
            if (lblNflag.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }
        }

        protected void gvSearchMEMBERInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvSearchMEMBERInfo.SelectedRow;
            lblMemName.Text = row.Cells[0].Text;
            txtMemNo.Text = row.Cells[1].Text;
            lblCuNumber.Text = row.Cells[2].Text;

            gvSearchMEMBERInfo.Visible = false;
            Session["RCreditUNo"] = lblCuNumber.Text;
            //Session["RMemNo"] = txtMemNo.Text;
            //Session["MemName"] = lblMemName.Text;
            Session["flag"] = "1";
            Session["ExFlag"] = string.Empty;
            Session["SFlag"] = string.Empty; 
            //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
            if (lblNflag.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }


           
        }

        protected void gvSearchMEMInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = gvSearchMEMInfo.SelectedRow;
            txtMemNo.Text = row.Cells[1].Text;
            lblMemName.Text = row.Cells[0].Text;

            gvSearchMEMInfo.Visible = false;
            Session["RCreditUNo"] = lblCuNumber.Text;
            //Session["RMemNo"] = txtMemNo.Text;
            //Session["MemName"] = lblMemName.Text;
            Session["flag"] = "1";
            Session["ExFlag"] = string.Empty;
            Session["SFlag"] = string.Empty; 
            //txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);
            if (lblNflag.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSAccountTransfer.aspx'; self.close();</script>", false);
            }

            else if (lblNflag.Text == "1")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSIncreaseSanctionAmount.aspx'; self.close();</script>", false);
            }
            else if (lblNflag.Text == "2")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSLoanApplication.aspx'; self.close();</script>", false);
            }
            else if (lblNflag.Text == "3")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSGroupSummaryStatement.aspx'; self.close();</script>", false);
            }
            else if (lblNflag.Text == "4")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSEditLoanApplication.aspx'; self.close();</script>", false);
            }
            else if (lblNflag.Text == "5")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }

        }
        protected void txtSearchCuName_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT  CuName, lTrim(str(CuType)+lTrim(str(CuNo))) As CuNo,CuOldCuNo,CuOld1CuNo,CuOld2CuNo FROM A2ZCUNION where  CuStatus = 0 AND CuName like '" + txtSearchCuName.Text + "%'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            gvSearchCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry, gvSearchCUInfo, "A2ZCSMCUS");

            gvSearchMEMInfo.Visible = false;
            gvSearchCUInfo.Visible = true;

         

        }

        protected void txtSearchMemName_TextChanged(object sender, EventArgs e)
        {
            string qry = "SELECT  MemName, MemNo, lTrim(str(CuType)+lTrim(str(CuNo))) As CuNo,MemOldCuNo,MemOld1CuNo,MemOld2CuNo,MemOldMemNo,MemOld1MemNo,MemOld2MemNo FROM A2ZMEMBER where CuType = '" + lblCuType.Text + "' AND CuNo = '" + lblCuNo.Text + "' AND MemName like '" + txtSearchMemName.Text + "%'";
            //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
            gvSearchMEMBERInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry, gvSearchMEMBERInfo, "A2ZCSMCUS");

            gvSearchMEMInfo.Visible = false;
            
            gvSearchCUInfo.Visible = false;

            gvSearchMEMBERInfo.Visible = true;
        }

        



    }
}