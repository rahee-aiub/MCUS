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
    public partial class SYSModuleControl : System.Web.UI.Page
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
                    IdsDropdown();

                    //ddlUserId = A2ZSYSIDSDTO.GetDropDownList(ddlUserId, "IdsNo <> " + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID)));

                    Iniatialized();
                    

                    txtIdsNo.Focus();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Iniatialized()
        {
            string strQuery = "SELECT ModuleNo,ModuleName FROM A2ZERPMODULE";
            gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");
        }
        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZHKMCUS");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery;
                int rowEffiect;
                int i = 0;
                int nCount = 0;

                sqlQuery = @"DELETE  FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
                rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));


                i = 0;
                foreach (GridViewRow gv in gvModule.Rows)
                {
                    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        nCount++;
                    }
                    i++;
                }

                if (nCount == 0)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Select any one module');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select any one Module');", true);
                    return;
                }


                i = 0;
                foreach (GridViewRow gv in gvModule.Rows)
                {
                    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        sqlQuery = "INSERT INTO A2ZSYSMODULECTRL (IdsNo,ModuleNo,ModuleName) VALUES ('" + ddlIdsNo.SelectedValue + "','" + gvModule.Rows[i].Cells[1].Text + "','" + gvModule.Rows[i].Cells[2].Text + "')";
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
                    }
                    i++;

                }

                Iniatialized();
                

                ddlIdsNo.SelectedIndex = 0;
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();



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

        private void IDsNotFoundMsg()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }
        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Iniatialized();
                               
                int idno = Converter.GetInteger(txtIdsNo.Text);
                A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
                dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
                if (dto.IdsNo > 0)
                {
                    ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);
                }
                else
                {
                    IDsNotFoundMsg();
                    ddlIdsNo.SelectedValue = "-Select-";
                    txtIdsNo.Text = string.Empty;
                    txtIdsNo.Focus();
                    return;
                }


                ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);

                string sqlQuery = "SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {
                        int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString()) - 1;
                        //int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString());
                        ((CheckBox)gvModule.Rows[moduleNo].FindControl("chkSelect")).Checked = true;
                    }
                }
                else
                {
                    Iniatialized();
                    
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdsNo.SelectedValue == "-Select-")
                {
                    return;
                }

                Iniatialized();
                
                txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);

                string sqlQuery = "SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {
                        int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString()) - 1;
                        //int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString());
                        ((CheckBox)gvModule.Rows[moduleNo].FindControl("chkSelect")).Checked = true;
                    }
                }
                else
                {
                    Iniatialized();
                    
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}