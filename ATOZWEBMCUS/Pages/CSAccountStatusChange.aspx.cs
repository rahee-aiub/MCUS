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
    public partial class CSAccountStatusChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;

                    string Module = (string)Session["SModule"];

                    if (lblflag.Text == string.Empty)
                    {
                        lblModule.Text = Request.QueryString["a%b"];
                        txtAccNo.Focus();
                    }
                    else
                    {
                        lblModule.Text = Module;
                    }
                  
                  
                    AccStatusDropdown();

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtAccStatDate.Text = date;

                    lblLienAmt.Visible = false;
                    txtLienAmt.Visible = false;
                    txtLienAmt.Text = "0";

                    lblBalLien.Visible = false;
                    lblBalLienAmount.Visible = false;

                    if (lblflag.Text == "1" && NewAccNo != "")
                    {
                        txtAccNo.Text = NewAccNo;
                        GetAccInfo();
                    }

                    if (lblModule.Text == "04")
                    {
                        lblMemNo.Text = "Staff Code";

                        lblCUNum.Visible = false;
                        txtCreditUNo.Visible = false;
                        lblCuName.Visible = false;

                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;

            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;
        }
        private void AccStatusDropdown()
        {

            string sqlquery = "SELECT AccStatusCode,AccStatusDescription from A2ZACCSTATUS";
            ddlAccStat = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccStat, "A2ZCSMCUS");

        }



        protected void AccountStatusDropdown()
        {

            string sqlquery = "SELECT AccStatus,AccStatus FROM A2ZACCOUNT where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + CtrlAccType.Text + "'";
            ddlAccStat = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAccStat, "A2ZCSMCUS");


        }

        public void GetAccInfo()
        {
            try
            {
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInfoAccNo(AccNumber));
                if (accgetDTO.a == 0)
                {
                    InvalidAccMSG();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    lblAccBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    lblBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccBalance));
                    lblBalLienAmount.Text = Converter.GetString(String.Format("{0:0,0.00}", accgetDTO.AccLienAmt));
                    lblStatP.Text = Converter.GetString(accgetDTO.AccStatus);

                    DateTime dt = Converter.GetDateTime(accgetDTO.AccStatusDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblCurrRef.Text = Converter.GetString(accgetDTO.AccStatusNote);

                    if (lblStatP.Text == "98")
                    {
                        txtAccNo.Text = string.Empty;
                        txtAccNo.Focus();
                        InvalidAccStatusChg();
                        return;
                    }

                    if (lblStatP.Text == "50")
                    {
                        lblBalLien.Visible = true;
                        lblBalLienAmount.Visible = true;
                    }



                    if (date == "01/01/0001")
                    {
                        lblCurrStatDt.Text = string.Empty;
                    }
                    else
                    {
                        lblCurrStatDt.Text = date;
                    }
                    string qry1 = "SELECT AccStatusDescription FROM A2ZACCSTATUS where AccStatusCode='" + lblStatP.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {

                        lblCurrStatus.Text = Converter.GetString(dt1.Rows[0]["AccStatusDescription"]);
                    }

                    
                    lblCuType.Text = Converter.GetString(accgetDTO.CuType);
                    lblCuNo.Text = Converter.GetString(accgetDTO.CuNo);

                    txtCreditUNo.Text = (lblCuType.Text + "-" + lblCuNo.Text);


                    txtMemNo.Text = Converter.GetString(accgetDTO.MemberNo);

                    CtrlAccType.Text = Converter.GetString(accgetDTO.AccType);

                    lblcls.Text = Converter.GetString(accgetDTO.AccAtyClass);


                    Int16 AccType = Converter.GetSmallInteger(CtrlAccType.Text);
                    A2ZACCTYPEDTO get3DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                    if (get3DTO.AccTypeCode > 0)
                    {
                        lblAccTitle.Text = Converter.GetString(get3DTO.AccTypeDescription);
                    }
              
                    Int16 CType = Converter.GetSmallInteger(lblCuType.Text);
                    int CNo = Converter.GetInteger(lblCuNo.Text);
                    A2ZCUNIONDTO get5DTO = (A2ZCUNIONDTO.GetInformation(CType, CNo));
                    if (get5DTO.NoRecord > 0)
                    {
                        lblCuName.Text = Converter.GetString(get5DTO.CreditUnionName);
                    }

                    Int16 CUType = Converter.GetSmallInteger(lblCuType.Text);
                    int CUNo = Converter.GetInteger(lblCuNo.Text);
                    int MNo = Converter.GetInteger(txtMemNo.Text);
                    A2ZMEMBERDTO get6DTO = (A2ZMEMBERDTO.GetInformation(CUType, CUNo, MNo));
                    if (get6DTO.NoRecord > 0)
                    {
                        lblMemName.Text = Converter.GetString(get6DTO.MemberName);
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.GetAccInfo Problem');</script>");
                //throw ex;
            }
        }



       

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            if (txtAccNo.Text != string.Empty)
            {
                GetAccInfo();
            }

        }

        


        protected void ClearInfo()
        {

            txtCreditUNo.Text = string.Empty;

            txtMemNo.Text = string.Empty;
            txtAccNo.Text = string.Empty;

            ddlAccStat.SelectedValue = "-Select-";

            txtAccNo.Text = string.Empty;
            lblCurrStatus.Text = string.Empty;
            lblCurrRef.Text = string.Empty;

            txtNewNote.Text = string.Empty;
            txtAccStatDate.Text = string.Empty;

            txtLienAmt.Text = string.Empty;

            lblLienAmt.Visible = false;
            txtLienAmt.Visible = false;
            lblBalLien.Visible = false;
            lblBalLienAmount.Visible = false;

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                lblStatC.Text = Converter.GetString(ddlAccStat.SelectedValue);

                if (lblStatP.Text == "50" && lblStatC.Text == "99")
                {
                    InvalidLienMSG();
                    return;
                }

                if (lblStatC.Text == "99" && lblAccBalance.Text != "00.00")
                {
                    InvalidCloseMSG();
                    return;
                }



                DateTime date = DateTime.ParseExact(txtAccStatDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);



                string strqry = "UPDATE A2ZACCOUNT SET AccStatus='" + ddlAccStat.SelectedValue + "', AccStatusDate='" + date + "', AccStatusNote='" + txtNewNote.Text + "', AccLienAmt='" + txtLienAmt.Text + "' Where CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + txtMemNo.Text + "' and AccType='" + CtrlAccType.Text + "' And AccNo='" + txtAccNo.Text + "'";
                int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
                if (rowEffect1 > 0)
                {
                    ClearInfo();
                    Successfull();
                    txtCreditUNo.Focus();
                }
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void InvalidAccMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Lien Account, Can Not A/C Close');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Not Found');", true);
            return;
        }
        protected void InvalidLienMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Lien Account, Can Not A/C Close');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Lien Account, Can Not A/C Close');", true);
            return;
        }

        protected void InvalidCloseMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Balance Available, Can Not A/C Close ');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Balance Available, Can Not A/C Close');", true);
            return;
        }

        protected void InvalidAccStatusChg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Transfered Status Change');", true);
            return;
        }

        protected void Successfull()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Status Change Successfully Done');", true);
            return;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlAccStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccStat.SelectedValue == "50")
            {
                lblLienAmt.Visible = true;
                txtLienAmt.Visible = true;
                txtLienAmt.Text = string.Empty;
                txtLienAmt.Focus();
            }
            else
            {
                lblLienAmt.Visible = false;
                txtLienAmt.Visible = false;
                txtLienAmt.Text = string.Empty;
                txtAccStatDate.Focus();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {

            Session["SSearchflag"] = "1";

            Session["SModule"] = lblModule.Text;
            Session["SFuncOpt"] = "0";
            Session["SControlFlag"] = "7";

            if (lblModule.Text == "04")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
     "click", @"<script>window.open('CSGetStaffAccountNo.aspx','_blank');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
               "click", @"<script>window.open('CSGetAccountNo.aspx','_blank');</script>", false);
            }


        }



    }
}