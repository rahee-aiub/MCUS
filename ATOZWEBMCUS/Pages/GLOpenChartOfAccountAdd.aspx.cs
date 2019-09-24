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
    public partial class GLOpenChartOfAccountAdd : System.Web.UI.Page
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
                    lblOverDraft.Visible = false;
                    ChkOverDraft.Visible = false;

                    lblDetail.Visible = false;
                    txtDetailNo.Visible = false;
                    lblDesc.Visible = false;
                    txtDesc.Visible = false;

                    lblAccMode.Visible = false;
                    ddlAccMode.Visible = false;

                    btnAdd.Visible = false;

                    ddlHeader.Enabled = false;
                    ddlMainHead.Enabled = false;
                    ddlSubHead.Enabled = false;

                    txtDetailNo.ReadOnly = true;


                    string sqlquery = @"SELECT GLHead, + CAST (GLHead AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLHeadDesc)) FROM A2ZCGLMST WHERE GLRecType = 1 AND GLSubHead = 0 GROUP BY GLHead,GLHeadDesc ";                  
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
            string sqlquery1 = @"SELECT GLAccNo, + CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) FROM A2ZCGLMST WHERE GLRecType = 1 AND GLPrtPos = 1 ";
            ddlMainHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlMainHead, "A2ZGLMCUS");
        }

        private void SubHeadDropDown()
        {
            string sqlquery2 = @"SELECT GLAccNo, + CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) FROM A2ZCGLMST WHERE GLRecType = 1 AND GLPrtPos = 2 ";
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ddlHeader.SelectedValue == "-Select-" && rbtMainHead.Checked)
                { 
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Select Header' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Header');", true);
                    return;
                }

                if (ddlMainHead.SelectedValue == "-Select-" && rbtSubHeader.Checked)
                { 
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Select Main Head' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Main Head');", true);
                    return;
                }


                if (ddlSubHead.SelectedValue == "-Select-" && rbtDeatail.Checked)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Select Sub Head' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Sub Head');", true);
                    return;
                }
                
                if (txtDetailNo.Text == string.Empty)
                {
                    
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Code' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Code');", true);
                    return;
                }

                if (txtDesc.Text == string.Empty)
                {
                    
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Description' );";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Description');", true);
                    return;
                }
                       
                string input2 = ddlHeader.SelectedValue;

                string GlAccType = input2.Substring(0, 1);

                A2ZCGLMSTDTO objMst = new A2ZCGLMSTDTO();


                if (ChkOverDraft.Checked == true)
                {
                    objMst.GLBalanceType = Converter.GetSmallInteger(1);
                }
                else
                {
                    objMst.GLBalanceType = Converter.GetSmallInteger(0);
                }


                if (rbtMainHead.Checked)
                {
                    objMst.GLRecType = 1;
                    objMst.GLPrtPos = Converter.GetSmallInteger(1);
                    objMst.GLMainHead = Converter.GetInteger(txtDetailNo.Text);
                    objMst.GLMainHeadDesc = txtDesc.Text;

                }

                if (rbtSubHeader.Checked)
                {
                    objMst.GLRecType = 1;
                    objMst.GLPrtPos = Converter.GetSmallInteger(2);
                    objMst.GLMainHead = Converter.GetInteger(ddlMainHead.SelectedValue);
                    objMst.GLMainHeadDesc = lblMainHeaddesc.Text;
                    objMst.GLSubHead = Converter.GetInteger(txtDetailNo.Text);
                    objMst.GLSubHeadDesc = txtDesc.Text;

                }

                if (rbtDeatail.Checked)
                {
                    objMst.GLRecType = 2;
                    objMst.GLPrtPos = Converter.GetSmallInteger(6);
                    objMst.GLMainHead = Converter.GetInteger(ddlMainHead.SelectedValue);
                    objMst.GLMainHeadDesc = lblMainHeaddesc.Text;
                    objMst.GLSubHead = Converter.GetInteger(ddlSubHead.SelectedValue);
                    objMst.GLSubHeadDesc = lblsubHeadDesc.Text;
                    objMst.GLAccMode = Converter.GetSmallInteger(ddlAccMode.SelectedValue);


                }

                A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                hdComNo.Value = Converter.GetString(dto.PrmUnitNo);
                objMst.GLCoNo = Converter.GetSmallInteger(hdComNo.Value);
                objMst.GLAccType = Converter.GetSmallInteger(GlAccType);
                objMst.GLAccNo = Converter.GetInteger(txtDetailNo.Text);
                objMst.GLAccDesc = txtDesc.Text;
                objMst.GLHead = Converter.GetInteger(ddlHeader.SelectedValue);
                objMst.GLHeadDesc = lblHeaderDesc.Text;

                objMst.Status = 1;
                
                int result = A2ZCGLMSTDTO.InsertInformation(objMst);

                if (result > 0)
                {
                    ClearInfo();

                    lblDetail.Enabled = false;
                    txtDetailNo.Enabled = false;
                    lblDesc.Enabled = false;
                    txtDesc.Enabled = false;

                    lblAccMode.Visible = false;
                    ddlAccMode.Visible = false;

                    btnAdd.Visible = false;

                    ddlHeader.Enabled = false;
                    ddlMainHead.Enabled = false;
                    ddlSubHead.Enabled = false;

                    rbtMainHead.Checked = false;
                    rbtSubHeader.Checked = false;
                    rbtDeatail.Checked = false;

                    Successful();

                    MainHeadDropDown();
                    SubHeadDropDown();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearInfo()
        {
            
            
            ddlMainHead.SelectedIndex = 0;
            txtDetailNo.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txthidesubhead.Text = string.Empty;
            txtMainHead.Text = string.Empty;
            
            lblsubHeadDesc.Text = string.Empty;
            lblMainHeaddesc.Text = string.Empty;

            lblOverDraft.Visible = false;
            ChkOverDraft.Visible = false;
          
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

            string sqlquery1 = @"SELECT GLAccNo, + CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) FROM A2ZCGLMST WHERE  GLPrtPos = 1 AND GLAccType = '" + sub3 + "' ";
            ddlMainHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlMainHead, "A2ZGLMCUS");

            if (rbtMainHead.Checked && ddlHeader.SelectedValue != "-Select-")
            {
                string inputt = ddlHeader.SelectedValue;

                string subb = inputt.Substring(0, 1);

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLAccNo)AS GLAccNo FROM A2ZCGLMST WHERE GLAccType ='" + subb + "' AND GLRecType =1 AND GLPrtPos =2 ", "A2ZGLMCUS");
                if (dt.Rows.Count > 0)
                {
                    txtMainHead.Text = Converter.GetString(dt.Rows[0]["GLAccNo"]);
                    

                    
                    string input = txtMainHead.Text;
                    string subtract = input.Substring(0, 3);
                    string sub2 = input.Substring(Math.Max(0, input.Length - 8));
                    string hide = sub2;
                    int a = Converter.GetInteger(subtract);
                    int b = 1;

                    int c = (a + b);

                    hide = Converter.GetString(c);
                    string result = hide + "00000";
                    txtDetailNo.Text = Converter.GetString(result);


                }
            }


        }
        protected void ddlMainHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            string inputMHead = ddlMainHead.SelectedValue;
            lblMainHeaddesc.Text = Converter.GetString(ddlMainHead.SelectedItem.Text);

            string subMHead = inputMHead.Substring(0, 1);
            string sub4 = inputMHead.Substring(0, 3);
            string sqlquery1 = @"SELECT GLAccNo, + CAST (GLAccNo AS VARCHAR(100))+ '-' + LTRIM(RTRIM(GLAccDesc)) FROM A2ZCGLMST WHERE  GLPrtPos = 2 AND GLAccType = '" + subMHead + "' AND LEFT(GLAccNo,3)='" + sub4 + "' ";
            ddlSubHead = CommonManager.Instance.FillDropDownList(sqlquery1, ddlSubHead, "A2ZGLMCUS");
          

            if (rbtSubHeader.Checked && ddlMainHead.SelectedValue != "-Select-")
            {
                string input2 = ddlMainHead.SelectedValue;

                string sub3 = input2.Substring(0, 1);

                int a1 = Converter.GetInteger(sub3);

                string inputsubhead=ddlMainHead.SelectedValue;
                string subinputsubhead=inputsubhead.Substring(0, 3);

                int b1 = Converter.GetInteger(subinputsubhead);

                A2ZCGLMSTDTO glObj = (A2ZCGLMSTDTO.GetInformationMainHead(a1, b1));

                //A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformationMainHead(Converter.GetInteger(a,b));
                if (glObj.GLAccNo > 0)
                {
                    txthidesubhead.Text = Converter.GetString(glObj.GLAccNo);

                //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MAX(GLAccNo)AS GLAccNo FROM A2ZCGLMST WHERE GLAccType ='" + sub3 + "' AND GLRecType =1 AND GLPrtPos =1 AND LEFT(GLAccNo,4)='" + subinputsubhead + "'", "A2ZGLMCUS");
                //if (dt.Rows.Count > 0)
                //{
                    //txthidesubhead.Text = Converter.GetInteger(dt.Rows[0]["GLAccNo"]);
                    string input = txthidesubhead.Text;
                    string subtract = input.Substring(0, 5);
                    string sub2 = input.Substring(Math.Max(0, input.Length - 8));
                    string hide = sub2;
                    int a = Converter.GetInteger(subtract);
                    int b = 1;

                    int c = (a + b);

                    hide = Converter.GetString(c);
                    string result = hide + "000";
                    txtDetailNo.Text = Converter.GetString(result);


                }
            }


        }

        protected void ddlSubHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubHead.SelectedValue != "Select")
            {
                string input1 = ddlSubHead.SelectedValue;
                lblsubHeadDesc.Text = Converter.GetString(ddlSubHead.SelectedItem.Text);


                if (ddlSubHead.SelectedValue == "10106000")
                {
                    lblOverDraft.Visible = true;
                    ChkOverDraft.Visible = true;
                }

                
                string sub3 = input1.Substring(0, 5);

                A2ZCGLMSTDTO glObj = A2ZCGLMSTDTO.GetInformationtEST(Converter.GetInteger(sub3));

                if (glObj.GLAccNo > 0)
                {
                    string input = txtDetailNo.Text;

                    input = Converter.GetString(glObj.GLAccNo);

                    txtDetailNo.Text = Converter.GetString(input);

                    string sub2 = input.Substring(Math.Max(0, input.Length - 8));

                    string subhead= Converter.GetString(sub2);

                    int a = Converter.GetInteger(sub2);
                    int b = 1;

                    int c = a + b;

                    subhead = Converter.GetString(c);

                    txtDetailNo.Text = subhead;          
                }

            }
        }

     
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                if (ddlHeader.SelectedItem.Text == "-Select-")
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, " [ All ] ");
                }
                else
                {
                    SessionStore.SaveToCustomStore(Params.COMMON_NO1, ddlHeader.SelectedValue);
                    SessionStore.SaveToCustomStore(Params.COMMON_NAME1, (ddlHeader.SelectedItem.Text));
                }
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptGLChartOfAccountReport");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZGLMCUS");

                Response.Redirect("ReportServer.aspx", false);




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rbtMainHead_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtMainHead.Checked)
            {
                lblDetail.Text = "New Main Head Code";
                lblDetail.Visible = true;
                txtDetailNo.Visible = true;
                lblDesc.Visible = true;
                txtDesc.Visible = true;

                btnAdd.Visible = true;

                ddlHeader.Enabled = true;
                ddlMainHead.Enabled = false;
                ddlSubHead.Enabled = false;
                ClearInfo();
              
            }
        }

        protected void rbtSubHeader_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtSubHeader.Checked)
            {
                lblDetail.Text = "New Sub Head Code";
                lblDetail.Visible = true;
                txtDetailNo.Visible = true;
                lblDesc.Visible = true;
                txtDesc.Visible = true;

                btnAdd.Visible = true;

                ddlHeader.Enabled = true;
                ddlMainHead.Enabled = true;
                ddlSubHead.Enabled = false;
                
                ClearInfo();
            }
        }

        protected void rbtDeatail_CheckedChanged(object sender, EventArgs e)
        {
            lblDetail.Text = "New Detail Code";
            lblDetail.Visible = true;
            txtDetailNo.Visible = true;
            lblDesc.Visible = true;
            txtDesc.Visible = true;

            lblAccMode.Visible = true;
            ddlAccMode.Visible = true;

            btnAdd.Visible = true;

            ddlHeader.Enabled = true;
            ddlMainHead.Enabled = true;
            ddlSubHead.Enabled = true;
            
            
            ClearInfo();
        }

        }

    

    }
