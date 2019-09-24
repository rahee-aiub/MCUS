using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRSalaryGLControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblcode.Visible = false;
                    ddlCode.Visible = false;
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }
        private void Allowdropdown()
        {
            WFA2ZALLOWANCE();

            string sqlquery = "SELECT Code,Description from WFA2ZALLOWANCE";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZHRMCUS");
        }

        private void Deddropdown()
        {
            string sqlquery = "SELECT Code,Description from A2ZDEDUCTION WHERE Code < 16";
            ddlCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCode, "A2ZHRMCUS");
        }

        protected void gvGLControl()
        {
            string sqlquery3 = "SELECT Id,SalCtrlHead,SalCtrlCode,SalCtrlPayType,SalCtrlPayTypeDesc,SalCtrlGLCode FROM A2ZSALARYCTRL WHERE SalCtrlHead='" + ddlCtrlHead.SelectedValue + "' AND SalCtrlCode='" + ddlCode.SelectedValue + "'";

            gvGLControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvGLControlInfo, "A2ZHRMCUS");
        }
       
        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlCode.SelectedValue != "-Select-")
                {

                    InsertSalaryControl();

                    gvGLControlInfo.Visible = true;
                    gvGLControl();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        private void InsertSalaryControl()
        {
            string qry = "SELECT ProjectCode,ProjectDescription FROM A2ZPROJECT";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var PCode = dr["ProjectCode"].ToString();
                    var PDesc = dr["ProjectDescription"].ToString();

                    string qry1 = "SELECT SalCtrlHead,SalCtrlCode,SalCtrlPayType,SalCtrlGLCode FROM A2ZSALARYCTRL  WHERE SalCtrlHead='" + ddlCtrlHead.SelectedValue + "' AND SalCtrlCode='" + ddlCode.SelectedValue + "' AND SalCtrlPayType='" + PCode + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");

                    if (dt1.Rows.Count == 0)
                    {
                        string strQuery = "INSERT INTO A2ZSALARYCTRL(SalCtrlHead,SalCtrlCode,SalCtrlPayType,SalCtrlPayTypeDesc,SalCtrlGLCode) VALUES('" + ddlCtrlHead.SelectedValue + "','" + ddlCode.SelectedValue + "','" + PCode + "','" + PDesc + "',0)";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                    }
                }

            }
        }


        protected void UpdateSalaryControl()
        {
            string delqry = "DELETE FROM A2ZSALARYCTRL WHERE SalCtrlHead= '" + ddlCtrlHead.SelectedValue + "' AND SalCtrlCode='" + ddlCode.SelectedValue + "'";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));

            for (int i = 0; i < gvGLControlInfo.Rows.Count; ++i)
            {
                Label CtrlHead = (Label)gvGLControlInfo.Rows[i].Cells[1].FindControl("lblCtrlHead");
                Label CtrlCode = (Label)gvGLControlInfo.Rows[i].Cells[2].FindControl("lblCtrlCode");
                Label Project = (Label)gvGLControlInfo.Rows[i].Cells[3].FindControl("lblProject");
                Label ProjectDesc = (Label)gvGLControlInfo.Rows[i].Cells[4].FindControl("lblProjectDesc");
                TextBox GLCode = (TextBox)gvGLControlInfo.Rows[i].Cells[5].FindControl("txtGLCode");
                

                int CHead = Converter.GetInteger(CtrlHead.Text);
                int CCode = Converter.GetInteger(CtrlCode.Text);
                int Proj = Converter.GetInteger(Project.Text);
                String ProjDesc = Converter.GetString(ProjectDesc.Text);

                string GLCODE = GLCode.Text;
               

                string strQuery = "INSERT INTO A2ZSALARYCTRL(SalCtrlHead,SalCtrlCode,SalCtrlPayType,SalCtrlPayTypeDesc,SalCtrlGLCode) VALUES('" + CHead + "','" + CCode + "','" + Proj + "','" + ProjDesc + "','" + GLCODE + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            }

        }
                
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {    
                UpdateSalaryControl();
                gvGLControlInfo.Visible = false;
                lblcode.Visible = false;
                ddlCode.Visible = false;
                ddlCtrlHead.SelectedIndex = 0; 
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        

        protected void gvGLControlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowIndex == 0)
                //    e.Row.Style.Add("height", "60px");
                //e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void ddlCtrlHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCtrlHead.SelectedValue == "1")
            {
                lblcode.Text = "Allowance Code:";
                Allowdropdown();
                lblcode.Visible = true;
                ddlCode.Visible = true;
            }
            else if (ddlCtrlHead.SelectedValue == "2")
            {
                lblcode.Text = "Deduction Code:";
                Deddropdown();
                lblcode.Visible = true;
                ddlCode.Visible = true;
            }
            else if (ddlCtrlHead.SelectedValue == "99")
            {          
                lblcode.Visible = false;
                ddlCode.Visible = false;
                InsertSalaryControl();

                gvGLControlInfo.Visible = true;
                gvGLControl();
            }
            
        }

        private void WFA2ZALLOWANCE()
        {
            string sqlquery4 = "Truncate table dbo.WFA2ZALLOWANCE";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZHRMCUS"));

            string strQry = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES (98,'Basic')";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry, "A2ZHRMCUS"));

            string strQry1 = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES (99,'Consolidated')";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQry1, "A2ZHRMCUS"));

            string qry = "SELECT Code,Description from A2ZALLOWANCE";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["Code"].ToString();
                    var Desc = dr["Description"].ToString();
                    string strQuery = "INSERT INTO WFA2ZALLOWANCE(Code,Description) VALUES ('" + code + "','" + Desc + "')";
                    int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                }
            }

            
        }
        

    }
}