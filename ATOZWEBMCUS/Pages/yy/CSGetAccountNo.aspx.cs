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
    public partial class CSGetAccountNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string TranDate = (string)Session["STranDate"];
                string Func = (string)Session["SFuncOpt"];
                string Module = (string)Session["SModule"];
                string VchNo = (string)Session["SVchNo"];
                string Cflag = (string)Session["CFlag"];
                
                lblFuncOpt.Text = Func;
                lblModule.Text = Module;

                CFlag.Text = Cflag;

                DateTime tdt = Converter.GetDateTime(TranDate);
                string tdate = tdt.ToString("dd/MM/yyyy");
                lblTranDate.Text = tdate;
                lblVchNo.Text = VchNo;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                if (CFlag.Text == "1")
                {
                    string RCuNo = (string)Session["RCreditUNo"];
                    lblCuNumber.Text = RCuNo;
                    txtCreditUNo.Text = Converter.GetString(lblCuNumber.Text);
                    string RMemNo = (string)Session["RMemNo"];
                    txtMemNo.Text = RMemNo;
                    BtnSearch_Click(this, EventArgs.Empty);
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


        private void GenerateTransactionCode()
        {
            var prm = new object[6];

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
            prm[3] = lblFuncOpt.Text;
            prm[4] = lblID.Text;
            prm[5] = lblModule.Text;

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
        protected void BtnSearch_Click(object sender, EventArgs e)
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

                        int MemNumber = Converter.GetInteger(txtOldMemNo.Text);
                        int CuNumber = Converter.GetInteger(hdnCuNumber.Text);
                        A2ZMEMBERDTO get1DTO = new A2ZMEMBERDTO();                    
                        get1DTO = (A2ZMEMBERDTO.GetInfoOldMember(CuNumber, MemNumber));
                    
                        if (get1DTO.NoRecord > 0)
                        {
                            txtMemNo.Text = Converter.GetString(get1DTO.MemberNo);
                            lblMemName.Text = Converter.GetString(get1DTO.MemberName);

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

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Session["RTranDate"] = lblTranDate.Text;
            Session["RFuncOpt"] = lblFuncOpt.Text;
            Session["RModule"] = lblModule.Text;
            Session["RVchNo"] = lblVchNo.Text;
            Session["NewAccNo"] = string.Empty;

            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["CFlag"] = string.Empty;

            


            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
              "click", @"<script>window.opener.location.href='CSDailyTransactionByAccount.aspx'; self.close();</script>", false);

        }

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                GridViewRow row = gvDetailInfo.SelectedRow;
                lblAccNo.Text = row.Cells[3].Text;
                Session["NewAccNo"] = lblAccNo.Text;
                Session["flag"] = "1";
                Session["RTranDate"] = lblTranDate.Text;
                Session["RFuncOpt"] = lblFuncOpt.Text;
                Session["RModule"] = lblModule.Text;
                Session["RVchNo"] = lblVchNo.Text;

                Session["Rflag"] = "1";
                Session["RCreditUNo"] = lblCuNumber.Text;
                Session["RMemNo"] = txtMemNo.Text;
                Session["CFlag"] = "1";
                           


                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                "click", @"<script>window.opener.location.href='CSDailyTransactionByAccount.aspx'; self.close();</script>", false);

              
        }

        //protected void MoveAccDescription()
        //{
        //    try
        //    {

        //          foreach (GridViewRow r in gvDetailInfo.Rows)
        //          {
        //            //Int16 Acctype = Converter.GetSmallInteger(gvDetailInfo.Rows[i].Cells[1].Text);
        //            Label AccType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("AccType");
        //            lblAccType.Text = Converter.GetString(AccType.Text);
        //            string sqlquery = "SELECT AccTypeDescription from A2ZACCTYPE WHERE AccTypeCode='" + lblAccType.Text + "'";
        //            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
        //            if (dt.Rows.Count > 0)
        //            {
        //                Label AccTypeDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("AccTypeDesc");
        //                AccTypeDesc.Text = Converter.GetString(dt.Rows[0]["AccTypeDescription"]);
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.MoveAccTypeDesc Problem');</script>");
        //        //throw ex;
        //    }
        //}


      
    }
}