using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;
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
    public partial class HRSalaryCertificateControl : System.Web.UI.Page
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

                    //Iniatialized();
                    

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
            string sqlquery3 = "SELECT EmpCode,EmpName from A2ZEMPLOYEE ORDER BY EmpCode ASC";
            gvCertiDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCertiDetailInfo, "A2ZHRMCUS");
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

                sqlQuery = @"DELETE  FROM dbo.A2ZHRCERTCTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
                rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));


                i = 0;
                foreach (GridViewRow gv in gvCertiDetailInfo.Rows)
                {
                    Boolean m = ((CheckBox)gvCertiDetailInfo.Rows[i].FindControl("chkSelect")).Checked;

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

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select any one Emp. Code');", true);
                    return;
                }


                i = 0;
                foreach (GridViewRow gv in gvCertiDetailInfo.Rows)
                {
                    Boolean m = ((CheckBox)gvCertiDetailInfo.Rows[i].FindControl("chkSelect")).Checked;
                                      

                    if (m)
                    {
                        Label empcode = (Label)gvCertiDetailInfo.Rows[i].Cells[1].FindControl("lblEmpCode");


                        sqlQuery = "INSERT INTO A2ZHRCERTCTRL (IdsNo,EmpCode) VALUES ('" + ddlIdsNo.SelectedValue + "','" + empcode.Text + "')";
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
                    }
                    i++;

                }

                //Iniatialized();

                gvCertiDetailInfo.Visible = false;
                ddlIdsNo.SelectedIndex = 0;
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCertiDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }


        private void ShowGridViewWithValue()
        {
            int i = 0;

            foreach (GridViewRow gv in gvCertiDetailInfo.Rows)
            {
               

                Label ECode = (Label)gvCertiDetailInfo.Rows[i].Cells[1].FindControl("lblEmpCode");
                int empcode = Converter.GetInteger(ECode.Text);

                int empno = Converter.GetInteger(lblEmpNo.Text);

                if (empcode == empno)
                {
                    ((CheckBox)gvCertiDetailInfo.Rows[i].FindControl("chkSelect")).Checked = true;
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
                //Iniatialized();
                               
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

                gvCertiDetailInfo.Visible = true;
                Iniatialized();

                ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);

                string sqlQuery = "SELECT * FROM dbo.A2ZHRCERTCTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHRMCUS");

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        var empcode = dr["EmpCode"].ToString();

                        lblEmpNo.Text = empcode;

                        ShowGridViewWithValue();
                    }
                }
                else
                {
                    //Iniatialized();
                    
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

                gvCertiDetailInfo.Visible = true;
                Iniatialized();
                
                txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);

                string sqlQuery = "SELECT * FROM dbo.A2ZHRCERTCTRL  WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHRMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var empcode = dr["EmpCode"].ToString();

                        lblEmpNo.Text = empcode;

                        ShowGridViewWithValue();
                    }
             
                }
                else
                {
                    //Iniatialized();
                    
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}