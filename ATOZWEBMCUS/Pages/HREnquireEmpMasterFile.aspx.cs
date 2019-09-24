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
    public partial class HREnquireEmpMasterFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                gvInfo();
            }
        }


        private void gvInfo()
        {
            string sqlquery3 = "SELECT Id,EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,EmpPayLabel FROM A2ZHRMCUS..A2ZEMPLOYEE";
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetails, "A2ZHRMCUS");
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if(txtEmpID.Text=="" && txtEmpName.Text=="")
            {
                string qry1 = "SELECT Id,EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,EmpPayLabel FROM A2ZHRMCUS..A2ZEMPLOYEE";
                gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry1, gvDetails, "A2ZHRMCUS");
            }
            else
            {
                string qry2 = "SELECT Id,EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,EmpPayLabel FROM A2ZHRMCUS..A2ZEMPLOYEE  where EmpCode like '" + txtEmpID.Text + "%' or EmpName like '" + txtEmpName.Text + "%'";
                gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(qry2, gvDetails, "A2ZHRMCUS");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDetails.SelectedRow;
            Label lblempcode = (Label)gvDetails.Rows[row.RowIndex].Cells[1].FindControl("lblempcode");
            string  EmpCode = Converter.GetString(lblempcode.Text);
             Session["EmpCode"] = EmpCode;

             ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.open('HRViewEmployeeMaint.aspx','_blank');</script>", false);


            //Page.ClientScript.RegisterStartupScript(
            //this.GetType(), "OpenWindow", "window.open('HRViewEmployeeMaint.aspx','_newtab');", true);

        }
    }
}