using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLChartOfAccountReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (IsPostBack)
                {

                }
                else
                {
                    lblBankAccType.Visible = false;
                    ddlBankAccType.Visible = false;

                    ddlHeader.Enabled = false;
                    ddlMainHead.Enabled = false;
                    ddlSubHead.Enabled = false;

                    ChkAllHeader.Checked = true;
                    ChkAllMainHead.Checked = true;
                    ChkAllSubHead.Checked = true;

                    string sqlquery = @"SELECT GLHead, GLHeadDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ";
                    ddlHeader = CommonManager.Instance.FillDropDownList(sqlquery, ddlHeader, "A2ZGLMCUS");

                    MainHeadDropDown();
                    SubHeadDropDown();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MainHeadDropDown()
        {
            string sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLPrtPos = 1 ";
            ddlMainHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlMainHead, "A2ZGLMCUS");
        }

        private void SubHeadDropDown()
        {
            string sqlquery2 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE GLRecType = 1 AND GLPrtPos = 2 ";
            ddlSubHead = CommonManager.Instance.FillDropDownList(sqlquery2, ddlSubHead, "A2ZGLMCUS");
        }
        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert(' Chart Of Account Add completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Chart Of Account Add Completed');", true);
            return;

        }


        private void ClearInfo()
        {


            ddlMainHead.SelectedIndex = 0;

            txthidesubhead.Text = string.Empty;
            txtMainHead.Text = string.Empty;

            lblsubHeadDesc.Text = string.Empty;
            lblMainHeaddesc.Text = string.Empty;



        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }




        protected void ddlHeader_SelectedIndexChanged(object sender, EventArgs e)
        {

            string input2 = ddlHeader.SelectedValue;

            lblHeaderDesc.Text = Converter.GetString(ddlHeader.SelectedItem.Text);

            string sub3 = input2.Substring(0, 1);

            string sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 1 AND GLAccType = '" + sub3 + "' ";
            ddlMainHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlMainHead, "A2ZGLMCUS");

            if (ddlHeader.SelectedValue != "-Select-")
            {
                string inputt = ddlHeader.SelectedValue;

                string subb = inputt.Substring(0, 1);

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLAccNo)AS GLACCNO FROM A2ZCGLMST WHERE GLAccType ='" + subb + "' AND GLRecType =1 AND GLPrtPos =2 ", "A2ZGLMCUS");
                if (dt.Rows.Count > 0)
                {
                    txtMainHead.Text = Converter.GetString(dt.Rows[0]["GLACCNO"]);



                    string input = txtMainHead.Text;
                    string subtract = input.Substring(0, 3);
                    string sub2 = input.Substring(Math.Max(0, input.Length - 8));
                    string hide = sub2;
                    int a = Converter.GetInteger(subtract);
                    int b = 1;

                    int c = (a + b);

                    hide = Converter.GetString(c);
                    string result = hide + "00000";



                }
            }


        }
        protected void ddlMainHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            string inputMHead = ddlMainHead.SelectedValue;
            lblMainHeaddesc.Text = Converter.GetString(ddlMainHead.SelectedItem.Text);

            string subMHead = inputMHead.Substring(0, 1);
            string sub4 = inputMHead.Substring(0, 3);
            string sqlquery1 = @"SELECT GLAccNo, GLAccDesc FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' ";
            ddlSubHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlSubHead, "A2ZGLMCUS");


            if (ddlMainHead.SelectedValue != "-Select-")
            {
                string input2 = ddlMainHead.SelectedValue;

                string sub3 = input2.Substring(0, 1);

                string inputsubhead = ddlMainHead.SelectedValue;
                string subinputsubhead = inputsubhead.Substring(0, 4);

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLAccNo)AS GLACCNO FROM A2ZCGLMST WHERE GLAccType ='" + sub3 + "' AND GLRecType =1 AND GLPrtPos =1 AND LEFT(GLAccNo,4)='" + subinputsubhead + "'", "A2ZGLMCUS");
                if (dt.Rows.Count > 0)
                {
                    txthidesubhead.Text = Converter.GetString(dt.Rows[0]["GLACCNO"]);
                    string input = txthidesubhead.Text;
                    string subtract = input.Substring(0, 5);
                    string sub2 = input.Substring(Math.Max(0, input.Length - 8));
                    string hide = sub2;
                    int a = Converter.GetInteger(subtract);
                    int b = 1;

                    int c = (a + b);

                    hide = Converter.GetString(c);
                    string result = hide + "000";



                }
            }


        }

        protected void ddlSubHead_SelectedIndexChanged(object sender, EventArgs e)
        {



        }


        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {



                if (ChkAllHeader.Checked == false && ddlHeader.SelectedItem.Text == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please - Tick at All Head Box / Select Head ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Tick at All Head Box / Select Head');", true);
                    return;

                }


                if (ChkAllMainHead.Checked == false && ddlMainHead.SelectedItem.Text == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please - Tick at All Main Head Box / Select Main Head ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Tick at All Main Head Box / Select Main Head');", true);
                    return;

                }

                if (ChkAllSubHead.Checked == false && ddlSubHead.SelectedItem.Text == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please  -  Tick at All Sub Head Box / Select Sub Head  ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please  -  Tick at All Sub Head Box / Select Sub Head');", true);
                    return;

                }






                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);


                if (ChkAllHeader.Checked == true)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " [ All ] ");
                }
                else
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, ddlHeader.SelectedValue);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, (ddlHeader.SelectedItem.Text));

                }

                if (ChkAllMainHead.Checked == true)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME2, " [ All ] ");
                }

                else
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO2, ddlMainHead.SelectedValue);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME2, (ddlMainHead.SelectedItem.Text));

                }


                if (ChkAllSubHead.Checked == true)
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO3, 0);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME3, " [ All ] ");
                }

                else
                {

                    SessionStore.SaveToCustomStore(Params.COMMON_NO3, ddlSubHead.SelectedValue);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME3, (ddlSubHead.SelectedItem.Text));

                }

                SessionStore.SaveToCustomStore(Params.COMMON_NO5, ddlBankAccType.SelectedValue);

               
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLChartOfAccountReport");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ChkAllHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllHeader.Checked == true)
            {
                ddlHeader.Enabled = false;
                ddlHeader.SelectedIndex = 0;
            }
            else
            {
                ddlHeader.Enabled = true;
            }
        }

        protected void ChkAllMainHead_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllMainHead.Checked == false)
            {
                if (ChkAllHeader.Checked == true || ddlHeader.SelectedItem.Text == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please - Removed Tick Sign From  All Head Box / Select Header  ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        
                    //}
                    ChkAllMainHead.Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Removed Tick Sign From  All Head Box / Select Header');", true);
                    return;

                }
            }



            if (ChkAllMainHead.Checked == true)
            {
                ddlMainHead.Enabled = false;
                ddlMainHead.SelectedIndex = 0;

            }
            else
            {
                ddlMainHead.Enabled = true;
            }
        }

        protected void ChkAllSubHead_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkAllSubHead.Checked == false)
            {

                if (ChkAllMainHead.Checked == true || ddlMainHead.SelectedItem.Text == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please - Removed Tick Sign From  All Main Head Box / Select Main Header  ');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                       
                    //}
                    ChkAllSubHead.Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please - Removed Tick Sign From  All Main Head Box / Select Main Header');", true);
                    return;

                }

            }


                if (ChkAllSubHead.Checked == true)
                {
                    ddlSubHead.Enabled = false;
                    ddlSubHead.SelectedIndex = 0;
                    
                }

                else
                {
                    ddlSubHead.Enabled = true;

                }
            






        }

        protected void ddlSubHead_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlSubHead.SelectedValue == "10106000")
            {
                lblBankAccType.Visible = true;
                ddlBankAccType.Visible = true;
            }
        }



    }
}
