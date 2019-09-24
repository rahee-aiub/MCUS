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
    public partial class GLOpenChartOfAccountModify : System.Web.UI.Page
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
                    txtGlCode.Focus();

                    lblOverDraft.Visible = false;
                    ChkOverDraft.Visible = false;
                    btnUpdate.Visible = false;
                 


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert(' Chart Of Account Modify completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Chart Of Account Modify completed');", true);
            return;
        }

        private void InvalidGLCode()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid GL Code');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid GL Code');", true);
            return;

        }
        private void ClearInfo()
        {
            
            txtDesc.Text = string.Empty;
            
        }
        protected void txtGlCode_TextChanged(object sender, EventArgs e)
        {
            string input2 = txtGlCode.Text;
            string sub3 = input2.Substring(0, 1);

            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("Select GLAccNo, GLAccDesc,GLPrtPos,GLSubHead,GLBalanceType,GLAccMode,Status From A2ZCGLMST WHERE GLAccNo ='" + txtGlCode.Text + "'", "A2ZGLMCUS");
            if (dt.Rows.Count > 0)
            {
                txtDesc.Text = Converter.GetString(dt.Rows[0]["GLAccDesc"]);
                lblGLPrtPos.Text = Converter.GetString(dt.Rows[0]["GLPrtPos"]);
                hdnGLSubHead.Text = Converter.GetString(dt.Rows[0]["GLSubHead"]);
                lblBalanceType.Text = Converter.GetString(dt.Rows[0]["GLBalanceType"]);
                ddlAccMode.SelectedValue = Converter.GetString(dt.Rows[0]["GLAccMode"]);
                ddlStatus.SelectedValue = Converter.GetString(dt.Rows[0]["Status"]);
                btnUpdate.Visible = true;
                txtDesc.Focus();

                if (hdnGLSubHead.Text == "10106000")
                {
                    lblOverDraft.Visible = true;
                    ChkOverDraft.Visible = true;
                    if (lblBalanceType.Text == "1")
                    {
                        ChkOverDraft.Checked = true;
                    }
                }

            }
            else
            {
               InvalidGLCode();
               btnUpdate.Visible = false;
               txtGlCode.Text = string.Empty;
               txtGlCode.Focus();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
             if (ChkOverDraft.Checked == true)
             {
                 lblBalanceType.Text = "1";
             }
             else
             {
                 lblBalanceType.Text = "0";
             }
                  
            
            int rowEffect = 0;

            string qry1 = "UPDATE A2ZCGLMST SET GLAccDesc='" + txtDesc.Text + "',GLBalanceType='" + lblBalanceType.Text + "',GLAccMode='" + ddlAccMode.SelectedValue + "',Status='" + ddlStatus.SelectedValue + "' Where GLAccNo='" + txtGlCode.Text + "'";
            rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry1, "A2ZGLMCUS"));

            if (lblGLPrtPos.Text == "1")
            {        
                string qry2 = "UPDATE A2ZCGLMST SET GLMainHeadDesc='" + txtDesc.Text + "' Where GLMainHead='" + txtGlCode.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry2, "A2ZGLMCUS"));
            }

            if (lblGLPrtPos.Text == "2")
            {
                string qry3 = "UPDATE A2ZCGLMST SET GLSubHeadDesc='" + txtDesc.Text + "' Where GLSubHead='" + txtGlCode.Text + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry3, "A2ZGLMCUS"));
            }
            
                     
            if(rowEffect > 0)
            {
                Successful();
                lblOverDraft.Visible = false;
                ChkOverDraft.Visible = false;
                btnUpdate.Visible = false;
                txtGlCode.Text = string.Empty;
                txtDesc.Text = string.Empty;
                ddlAccMode.SelectedIndex = 0;
                txtGlCode.Focus();
                
            }
            
        }
            
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

             
  

    }
}