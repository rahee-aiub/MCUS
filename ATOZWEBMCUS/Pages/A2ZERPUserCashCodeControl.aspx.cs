using System;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Drawing;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using System.Web.UI;

namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPUserCashCodeControl : System.Web.UI.Page
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
                    CtrlModule.Text = Request.QueryString["a%b"];

                    DivGridView.Visible = true;
                    Div1.Visible = true;
                    IdsDropdown();

                    txtUserId.Focus();
                    

                    //if (CtrlModule.Text == "1")
                    //{
                    //    lblSentMode.Visible = false;
                    //    ddlSentMode.Visible = false;
                    //}
                    //else 
                    //{
                    //    lblSentMode.Visible = true;
                    //    ddlSentMode.Visible = true;
                    //}


                    //ddlUserId = A2ZSYSIDSDTO.GetDropDownList(ddlUserId, "IdsNo <> " + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID)));

                    Iniatialized();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlUserId = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUserId, "A2ZHKMCUS");
        }

        
        private void Iniatialized()
        {
            string sqlquery = "SELECT GLAccNo as CashCode,LTRIM(RTRIM(GLAccDesc)) as Name from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";
            gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvModule, "A2ZGLMCUS");
        }


        private void FindUserCashCode()
        {
            string sqlquery = "SELECT FromCashCode,FromCashCodeDesc from A2ZUSERCASHCODE where IdsNo = '" + txtUserId.Text + "'";
            gvModule1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvModule1, "A2ZCSMCUS");

            MoveChkMark1();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                string sqlQuery;
                int rowEffiect;
                int i = 0;
                //int nCount = 0;


                string sqlquery;
                sqlquery = @"DELETE dbo.A2ZUSERCASHCODE WHERE IdsNo='" + txtUserId.Text + "'";

                int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));

                if (result > 0)
                {

                }

                i = 0;
                foreach (GridViewRow gv in gvModule.Rows)
                {
                    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        //lblFromIdsName.Text = (lblFromIdsName.Text != null) ? lblFromIdsName.Text.Trim().Replace("'", "''") : "";

                        //string A = string.Empty;

                        //A = ((TextBox)gvModule.Rows[i].Cells[2].Controls[0]).Text.TrimEnd(); //TextBox(gvModule.Rows[i].Cells[2].Controls



                        gvModule.Rows[i].Cells[2].Text = (gvModule.Rows[i].Cells[2].Text != null) ? gvModule.Rows[i].Cells[2].Text.Trim().Replace("'", "''") : "";

                        sqlQuery = "INSERT INTO A2ZUSERCASHCODE (IdsNo,FromCashCode,FromCashCodeDesc) VALUES ('" + txtUserId.Text + "','" + gvModule.Rows[i].Cells[1].Text + "','" + gvModule.Rows[i].Cells[2].Text.Trim() + "')";
                        rowEffiect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                    }
                    i++;

                }

                Iniatialized();

                Div1.Visible = false;

               

                ddlUserId.SelectedValue = "-Select-";
                txtUserId.Text = string.Empty;
                txtUserId.Focus();
                UpdateMsg();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }


        private void ShowGridViewWithValue()
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                int userId = Converter.GetSmallInteger(gvModule.Rows[i].Cells[1].Text);

                if (userId > 0)
                {
                    ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;
                }

                i++;

            }

        }

        private void UpdateMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Sucessfully Updated');", true);
            return;
        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }


        protected void btnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                i++;
            }
        }

        protected void btnUnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = false;

                i++;
            }
        }


        protected void MoveChkMark()
        {
 
            for (int i = 0; i < gvModule.Rows.Count; i++)
            {
                int cashcode = Converter.GetInteger(gvModule.Rows[i].Cells[1].Text);
               
               
                string sqlquery = "SELECT IdsNo,FromCashCode from A2ZUSERCASHCODE WHERE IdsNo='" + txtUserId.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CheckBox chk = (CheckBox)gvModule.Rows[i].Cells[0].FindControl("chkSelect");

                        var Ids = dr["IdsNo"].ToString();
                        var CCode = dr["FromCashCode"].ToString();
                        int CashCode = Converter.GetInteger(CCode);

                        if (CashCode == cashcode)
                        {
                            chk.Checked = true;
                        }

                    }
                    
                }

            }
        }

        protected void MoveChkMark1()
        {

            for (int i = 0; i < gvModule1.Rows.Count; i++)
            {
                int cashcode = Converter.GetInteger(gvModule1.Rows[i].Cells[1].Text);


                string sqlquery = "SELECT IdsNo,FromCashCode from A2ZUSERCASHCODE WHERE IdsNo='" + txtUserId.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CheckBox chk = (CheckBox)gvModule1.Rows[i].Cells[0].FindControl("chkSelect");

                        var Ids = dr["IdsNo"].ToString();
                        var CCode = dr["FromCashCode"].ToString();
                        int CashCode = Converter.GetInteger(CCode);

                        if (CashCode == cashcode)
                        {
                            chk.Checked = true;
                            chk.Enabled = false;
                        }

                    }

                }

            }
        }
        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtUserId.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
            if (dto.IdsNo > 0)
            {
                ddlUserId.SelectedValue = Converter.GetString(dto.IdsNo);
            }
            else
            {
                IDsNotFoundMsg();
                ddlUserId.SelectedValue = "-Select-";
                txtUserId.Text = string.Empty;
                txtUserId.Focus();
                return;
            }


            ddlUserId.SelectedValue = Converter.GetString(txtUserId.Text);
                  
            MoveChkMark();

            Div1.Visible = true;
            FindUserCashCode();

        }

        protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserId.Text = Converter.GetString(ddlUserId.SelectedValue);
            
            MoveChkMark();

            Div1.Visible = true;
            FindUserCashCode();

        }

    }
}