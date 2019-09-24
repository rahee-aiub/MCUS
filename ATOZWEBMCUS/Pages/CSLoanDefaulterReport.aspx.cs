using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanDefaulterReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Flag = (string)Session["Flag"];
                CtrlFlag.Text = Flag;

                ddlAccountType();
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                hdnPeriod.Text = Converter.GetString(dt);

                int Month = dt.Month;
                int Year = dt.Year;
                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);

                if (CtrlFlag.Text == "1")
                {
                    string A = (string)Session["RPeriodMM"];
                    string B = (string)Session["RPeriodYYYY"];
                    string C = (string)Session["RAccType"];
                    string D = (string)Session["RMthFrom"];
                    string E = (string)Session["RMthTill"];

                    string F = (string)Session["TrnDate"];

                  
                    ddlPeriodMM.SelectedValue = A;
                    ddlPeriodYYYY.SelectedValue = B;
                    ddlAccType.SelectedValue = C;
                    txtDueMthFrom.Text = D;
                    if (txtDueMthFrom.Text == "-")
                    {
                        txtDueMthFrom.Text = "0";
                    }
                    txtDueMthTill.Text = E;
                    if (txtDueMthTill.Text == "-")
                    {
                        txtDueMthTill.Text = "0";
                    }

                    hdnDate.Text = F;

                    //RemoveSession();
                    gvLoanDefaulter.Visible = true;
                    gvPreview();
                    //BtnView_Click(this, EventArgs.Empty);

                }                            

                ddlPeriodMM.SelectedValue = Converter.GetString(Month);
                ddlPeriodYYYY.SelectedValue = Converter.GetString(Year);

            }
        }


        protected void RemoveSession()
        {
            Session["flag"] = string.Empty;
            Session["TrnDate"] = string.Empty;
            Session["RPeriodMM"] = string.Empty;
            Session["RPeriodYYYY"] = string.Empty;
            Session["RAccType"] = string.Empty;
            Session["RMthFrom"] = string.Empty;
            Session["RMthTill"] = string.Empty;      
        }

        protected void ddlAccountType()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE Where AccTypeClass='6' AND AccTypeMode = '1'";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }

        private void InvalidDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Future Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }

        protected void ProcessLoanDefaulter()
        {
            try
            {
                string dt = (ddlPeriodMM.SelectedValue + "/" + 01 + "/" + ddlPeriodYYYY.SelectedValue);
                DateTime date = Converter.GetDateTime(dt);

                hdnDate.Text = Converter.GetString(dt);

                DateTime date1 = Converter.GetDateTime(hdnPeriod.Text);

                //SET @lastDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));



                if (date > date1)
                {
                    InvalidDateMSG();
                    ddlPeriodMM.SelectedValue = Converter.GetString(hdnMonth.Text);
                    ddlPeriodYYYY.SelectedValue = Converter.GetString(hdnYear.Text);
                    return;
                }


                var prm = new object[4];

                prm[0] = date;
                prm[1] = ddlAccType.SelectedValue;
                prm[2] = Converter.GetDecimal(txtDueMthFrom.Text);
                prm[3] = Converter.GetDecimal(txtDueMthTill.Text);

                hdnMsg.Text = "0";

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_rptLoanDefaulter", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    hdnMsg.Text = "1";
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void gvPreview()
        {
            try
            {

                string sqlquery3 = "SELECT CuType,CuNo,CuNumber,MemNo,AccType,AccNo,MemName,DueNoInstl,DuePrincAmt,DueIntAmt,AccOpenDate,AccLoanExpiryDate,AccLoanSancAmt,AccIntRate,AccNoInstl,AccBalance FROM WFLOANDEFAULTER";
                gvLoanDefaulter = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvLoanDefaulter, "A2ZCSMCUS");

            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                throw ex;
            }
        }

        protected void gvLoanDefaulter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvLoanDefaulter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label ctype = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[0].FindControl("lblCuType");
            Label cno = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[1].FindControl("lblCuNo");
            Label cnumber = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[2].FindControl("lblCuNumber");
            Label mmno = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[3].FindControl("lblMemNo");
            Label actype = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[4].FindControl("lblAccType");
            Label acno = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[5].FindControl("lblAccNo");
            Label mmname = (Label)gvLoanDefaulter.Rows[r.RowIndex].Cells[6].FindControl("lblMemName");


            string cutype = Converter.GetString(ctype.Text);
            string cuno = Converter.GetString(cno.Text);
            string cunumber = Converter.GetString(cnumber.Text);
            string memno = Converter.GetString(mmno.Text);
            string acctype = Converter.GetString(actype.Text);
            string accno = Converter.GetString(acno.Text);
            string memname = Converter.GetString(mmname.Text);


            Session["CuType"] = cutype;
            Session["CuNo"] = cuno;
            Session["CuNumber"] = cunumber;
            Session["AccType"] = acctype;
            Session["AccNo"] = accno;
            Session["MemNo"] = memno;
            Session["MemName"] = memname;
            Session["TrnDate"] = hdnDate.Text;

            Session["RPeriodMM"] = ddlPeriodMM.SelectedValue;
            Session["RPeriodYYYY"] = ddlPeriodYYYY.SelectedValue;
            Session["RAccType"] = ddlAccType.SelectedValue;
            Session["RMthFrom"] = txtDueMthFrom.Text;
            Session["RMthTill"] = txtDueMthTill.Text;

            Session["Flag"] = "1";

          

           
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
      "click", @"<script>window.open('CSLoanDefaulterDetailReport.aspx','_blank');</script>", false);


            //Page.ClientScript.RegisterStartupScript(
            //this.GetType(), "OpenWindow", "window.open('CSLoanDefaulterDetailReport.aspx','_newtab');", true);


        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            ProcessLoanDefaulter();
            if (hdnMsg.Text == "1")
            {
                gvLoanDefaulter.Visible = true;
                gvPreview();
            }

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessLoanDefaulter();
                if (hdnMsg.Text == "1")
                {

                    hdnToDaysDate.Text = ddlPeriodMM.SelectedItem.Text + ',' + ddlPeriodYYYY.SelectedValue;


                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;

                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME, hdnToDaysDate.Text);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, ddlAccType.SelectedItem.Text);

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSLoanDefaulterList");

                    Response.Redirect("ReportServer.aspx", false);
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }


        }
    }
}