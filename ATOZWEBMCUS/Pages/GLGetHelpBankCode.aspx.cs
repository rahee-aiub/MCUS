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
    public partial class GLGetHelpBankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvSearchBankInfo.Visible = false;
              
            }
        }



        protected void gvGetBankCodeDetail()
        {

            string sqlquery3 = "SELECT TBFGLCODEOLD,TBFGLCODENEW,TBFCACCOUNTDESC FROM WF_HelpBankTrans WHERE TBFAMOUNT = '" + txtVchAmount.Text + "' ORDER BY TBFCACCOUNTDESC";
            gvSearchBankInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSearchBankInfo, "A2ZGLMCUS");
        }


        
       

     
        protected void BtnSearch_Click(object sender, EventArgs e)
        {


            if (txtVchAmount.Text != string.Empty)
            {
                gvGetBankCodeDetail();
                gvSearchBankInfo.Visible = true;
                
           
            }
        }


        protected void gvSearchBankInfo_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        
            
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='GLDailyTransaction.aspx'; self.close();</script>", false);
            
            
        }

   
       
       
        protected void gvSearchBankInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvSearchBankInfo.SelectedRow;

            lblBankCode.Text = row.Cells[1].Text;
           
            gvSearchBankInfo.Visible = false;
            Session["RBankCode"] = lblBankCode.Text;
            
            Session["flag"] = "1";
            
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
            "click", @"<script>window.opener.location.href='GLDailyTransaction.aspx'; self.close();</script>", false);
           
        }

       
        

    }
}