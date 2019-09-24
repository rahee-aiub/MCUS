using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
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
    public partial class CSBoothTransactionControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProcessDate.Focus();
                BtnProcess.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtProcessDate.Text = date;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtProcessDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please insert date!');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtProcessDate.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Insert Date');", true);
                return;

            }

            if (ddlCtrlMode.SelectedValue == "0")
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Select Control Mode');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                ddlCtrlMode.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Control Mode');", true);
                return;

            }

            string strqry = null;
            int rowEffect = 0;


            DateTime prdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //strqry = "TRUNCATE TABLE dbo.A2ZBTRNCTRL";
            //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));

            //strqry = "INSERT INTO A2ZCSMCUS.dbo.A2ZBTRNCTRL(CashCodeNo,CashCodeName) SELECT GLAccNo,GLAccDesc FROM A2ZGLMCUS.dbo.A2ZCGLMST WHERE GLRecType = 2 and GLSubHead = 10101000";
            //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
            //if (rowEffect > 0)
            //{
            //    strqry = "UPDATE A2ZBTRNCTRL SET ProcessDate='" + prdate + "'";
            //    rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));

                DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                decimal trans=0;

                string strqry1 = "SELECT CashCodeNo,ProcessDate FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "'";
                DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry1, "A2ZCSMCUS");
                int totrec = dt.Rows.Count;
                lblTotalBooth.Text = Converter.GetString(totrec);
                if (dt.Rows.Count > 0)
                {                                   
                    foreach (DataRow dr in dt.Rows)
                    {

                        var CaCode = dr["CashCodeNo"].ToString();
                        var PDate = dr["ProcessDate"].ToString();
                        string query = "SELECT SUM(GLDebitAmt) as TrnDebit,SUM(GLCreditAmt) as TrnCredit FROM A2ZTRANSACTION WHERE TrnDate='" + PDate + "' AND FromCashCode='" + CaCode + "'";
                        DataTable dat = CommonManager.Instance.GetDataTableByQuery(query, "A2ZCSMCUS");
                        int totrec1 = dat.Rows.Count;
                        lblTotTrnBooth.Text = Converter.GetString(totrec1);

                        if (dat.Rows.Count > 0)
                        {
                            foreach (DataRow dar in dat.Rows)
                            {
                                var DebitAmt = dar["TrnDebit"].ToString();
                                var CreditAmt = dar["TrnCredit"].ToString();
                                decimal debit = Converter.GetDecimal(DebitAmt);
                                decimal credit = Converter.GetDecimal(CreditAmt);


                                if (debit != 0 && credit !=0)
                                {
                                    trans = (trans + 1);
                                }

                                strqry = "UPDATE A2ZBTRNCTRL SET TrnDebit='" + DebitAmt + "', TrnCredit='" + CreditAmt + "' WHERE ProcessDate='" + PDate + "' AND CashCodeNo='" + CaCode + "'";
                                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));

                            }
                        }


                    }

                    double TotalBooth = Converter.GetDouble(lblTotalBooth.Text);
                    lblTotTrnBooth.Text = Converter.GetString(trans);


                    double TotalTrnBooth = Converter.GetDouble(lblTotTrnBooth.Text);

                    double TotalNoTrnBooth = (TotalBooth - TotalTrnBooth);
                    lblTotNoTrnBooth.Text = Converter.GetString(TotalNoTrnBooth);


                    if (ddlCtrlMode.SelectedValue == "1")
                    {
                        gvDetail1();
                    }
                    else
                        if (ddlCtrlMode.SelectedValue == "2")
                        {
                            gvDetail2();
                        }
                        else
                            if (ddlCtrlMode.SelectedValue == "3")
                            {
                                gvDetail3();
                            }
                            else
                                if (ddlCtrlMode.SelectedValue == "4")
                                {
                                    gvDetail4();
                                }
                                else
                                    if (ddlCtrlMode.SelectedValue == "5")
                                    {
                                        gvDetail5();
                                    }
                                    else
                                        if (ddlCtrlMode.SelectedValue == "6")
                                        {
                                            gvDetail6();
                                        }
                                        else
                                            if (ddlCtrlMode.SelectedValue == "7")
                                            {
                                                gvDetail7();
                                            }
                       
                    //ColorStatus();
                    //Successful();
                

            }

            BtnProcess.Visible = true;

        }
        protected void gvDetail1()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "'";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }

        protected void gvDetail2()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "' AND (TrnDebit !=0 OR TrnCredit !=0)";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }

        protected void gvDetail3()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "'  AND (TrnDebit =0 AND TrnCredit =0)";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }

        protected void gvDetail4()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "' AND Status=0";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }

        protected void gvDetail5()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "' AND Status !=0";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }

        protected void gvDetail6()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "' AND Status =1";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }
        protected void gvDetail7()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,TrnDebit,TrnCredit,StatusName FROM A2ZBTRNCTRL WHERE ProcessDate='" + prodate + "'  AND Status=2";
            gvBoothControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvBoothControlInfo, "A2ZCSMCUS");
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            if (txtProcessDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please fill up the process date!' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill up the Process Date');", true);
                return;
            }

            var p = A2ZERPSYSPRMDTO.GetParameterValue();
            string comName = p.PrmUnitName;
            string comAddress1 = p.PrmUnitAdd1;
            SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
            SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
            SessionStore.SaveToCustomStore(Params.COMMON_NO1, ddlCtrlMode.SelectedValue);
            SessionStore.SaveToCustomStore(Params.COMMON_NAME1, ddlCtrlMode.SelectedItem.Text);
            DateTime prdate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, prdate);
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
            SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptBoothTrnControlReport");

            Response.Redirect("ReportServer.aspx", false);

        }

        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert(' Process successfully Done!.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Process Successfully Done');", true);
            return;
        }

        protected void gvBoothControlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }


    }
}
