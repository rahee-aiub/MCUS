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
using DataAccessLayer.DTO.HumanResource;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRReportLayoutMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //txtReportDate.Focus();
                    BtnUpdate.Visible = false;
                    dropdown();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }
        private void dropdown()
        {
            string sqlquery = "SELECT DISTINCT RepColumn,RepColumnName from A2ZREPORTLAYOUT";
            ddlRepColumn = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlRepColumn, "A2ZHRMCUS");
        }

        protected void Allowgridview()
        {
            string sqlquery = "SELECT Code, Description from A2ZALLOWANCE";
            gvDescription = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDescription, "A2ZHRMCUS");


        }

        protected void Dedgridview()
        {
            string sqlquery = "SELECT Code, Description from A2ZDEDUCTION";
            gvDescription = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDescription, "A2ZHRMCUS");


        }

        private void InvalidMSGy()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Report Column');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Report Column');", true);
            return;
        }
        protected void txtRepColumn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRepColumn.SelectedValue == "-Select-")
                {
                    txtRepColumnName.Focus();

                }

                if (txtRepColumn.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtRepColumn.Text);
                    if (MainCode > 14)
                    {
                        InvalidMSGy();
                        txtRepColumn.Text = string.Empty;
                        txtRepColumnName.Text = string.Empty;
                        lblRepColFlag.Text = string.Empty;
                        gvDescription.Visible = false;
                        txtRepColumn.Focus();
                        return;
                    }

                    if (MainCode < 8)
                    {
                        lblRepColFlag.Text = "Allowance";
                        gvDescription.Visible = true;
                        Allowgridview();
                        MoveAllowChkMark();
                    }
                    else
                    {
                        lblRepColFlag.Text = "Deduction";
                        gvDescription.Visible = true;
                        Dedgridview();
                        MoveDedChkMark();
                    }


                    DateTime Repdate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    A2ZREPORTLAYOUTDTO getDTO = (A2ZREPORTLAYOUTDTO.GetInformation(MainCode, Repdate));

                    if (getDTO.RepColumn > 0)
                    {
                        txtRepColumn.Text = Converter.GetString(getDTO.RepColumn);
                        txtRepColumnName.Text = Converter.GetString(getDTO.RepColumnName);


                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlRepColumn.SelectedValue = Converter.GetString(getDTO.RepColumn);
                        txtRepColumnName.Focus();
                        gvDescription.Visible = true;
                    }
                    else
                    {
                        txtRepColumnName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtRepColumnName.Focus();
                        gvDescription.Visible = true;


                        //if (ddlRepColumn.SelectedIndex != 0)
                        //{
                        //    ddlRepColumn.SelectedIndex = 0;
                        //}
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtRepColumn_TextChanged Problem');</script>");
                //throw ex;
            }

        }

        protected void ddlRepColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlRepColumn.SelectedValue == "-Select-")
                {
                    txtRepColumn.Focus();
                    txtRepColumn.Text = string.Empty;
                    txtRepColumnName.Text = string.Empty;
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                }


                if (ddlRepColumn.SelectedValue != "-Select-")
                {
                    DateTime Repdate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    int MainCode = Converter.GetInteger(ddlRepColumn.SelectedValue);
                    A2ZREPORTLAYOUTDTO getDTO = (A2ZREPORTLAYOUTDTO.GetInformation(MainCode, Repdate));
                    if (getDTO.RepColumn > 0)
                    {
                        txtRepColumn.Text = Converter.GetString(getDTO.RepColumn);
                        txtRepColumnName.Text = Converter.GetString(getDTO.RepColumnName);

                        if (MainCode < 8)
                        {
                            lblRepColFlag.Text = "Allowance";
                            Allowgridview();
                            MoveAllowChkMark();
                        }
                        else
                        {
                            lblRepColFlag.Text = "Deduction";
                            Dedgridview();
                            MoveDedChkMark();
                        }

                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtRepColumnName.Focus();
                        gvDescription.Visible = true;


                    }
                    else
                    {
                        txtRepColumn.Focus();
                        txtRepColumn.Text = string.Empty;
                        txtRepColumnName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;


                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlRepColumn_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }


        private void clearinfo()
        {
            txtRepColumn.Text = string.Empty;
            txtRepColumnName.Text = string.Empty;

            //ddlRepColumn.SelectedIndex = 0;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                   int nflag = 0;
                   A2ZREPORTLAYOUTDTO objDTO = new A2ZREPORTLAYOUTDTO();
                   int MainCode = Converter.GetInteger(txtRepColumn.Text);
                   DateTime Repdate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                   objDTO.RepDate = Converter.GetDateTime(Repdate);
                   objDTO.RepColumn = Converter.GetInteger(txtRepColumn.Text);
                   objDTO.RepColumnName = Converter.GetString(txtRepColumnName.Text);
                   if (MainCode < 8)
                   {
                       objDTO.RepColumnFlag = Converter.GetSmallInteger(1);
                   }
                   else
                   {
                       objDTO.RepColumnFlag = Converter.GetSmallInteger(2);

                   }

                   for (int i = 0; i < gvDescription.Rows.Count; i++)
                   {
                       int Code = Converter.GetInteger(gvDescription.Rows[i].Cells[0].Text);

                       CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");

                       if (chk.Checked)
                       {
                           objDTO.Code = Converter.GetInteger(Code);
                           objDTO.RepColumnCode = Converter.GetInteger(Code);
                           int row = A2ZREPORTLAYOUTDTO.InsertInformation(objDTO);
                           nflag = 1;
                       }
                      
                   }
                   
                    if (nflag == 0)
                    {
                        int row = A2ZREPORTLAYOUTDTO.InsertInformation(objDTO);
                    }
                    txtRepColumn.Focus();
                    clearinfo();
                    dropdown();

                    gvDescription.Visible = false;


                
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                A2ZREPORTLAYOUTDTO objDTO = new A2ZREPORTLAYOUTDTO();
                int MainCode = Converter.GetInteger(txtRepColumn.Text);
                DateTime Repdate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string strqry = "DELETE A2ZREPORTLAYOUT Where RepColumn='" + MainCode + "'";
                int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZHRMCUS"));
                if (rowwEffect > 0)
                {

                    for (int i = 0; i < gvDescription.Rows.Count; i++)
                    {
                        int Code = Converter.GetInteger(gvDescription.Rows[i].Cells[0].Text);

                        CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");

                        if (chk.Checked)
                        {
                            objDTO.Code = Converter.GetInteger(Code);
                            objDTO.RepColumn = Converter.GetInteger(txtRepColumn.Text);
                            objDTO.RepColumnName = Converter.GetString(txtRepColumnName.Text);
                            objDTO.RepColumnCode = Converter.GetInteger(Code);
                            objDTO.RepDate = Converter.GetDateTime(Repdate);

                            if (MainCode < 8)
                            {
                                objDTO.RepColumnFlag = Converter.GetSmallInteger(1);
                            }
                            else
                            {
                                objDTO.RepColumnFlag = Converter.GetSmallInteger(2);
                            }

                            int row = A2ZREPORTLAYOUTDTO.InsertInformation(objDTO);
                        }

                    }
                }
                     
                    dropdown();
                    clearinfo();

                    gvDescription.Visible = false;


                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtRepColumn.Focus();



                

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        protected void gvDescription_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }


        protected void MoveAllowChkMark()
        {
            try
            {

                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {
                    Int16 rcode = Converter.GetSmallInteger(gvDescription.Rows[i].Cells[0].Text);
                    string sqlquery = "SELECT RepColumn from A2ZREPORTLAYOUT WHERE RepColumn='" + txtRepColumn.Text + "' AND RepColumnCode='" + rcode + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                        chk.Checked = true;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.MoveAllowChkMark Problem');</script>");
                //throw ex;
            }
        }

        protected void MoveDedChkMark()
        {
            try
            {

                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {
                    Int16 rcode = Converter.GetSmallInteger(gvDescription.Rows[i].Cells[0].Text);
                    string sqlquery = "SELECT RepColumn from A2ZREPORTLAYOUT WHERE RepColumn='" + txtRepColumn.Text + "' AND RepColumnCode='" + rcode + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                        chk.Checked = true;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.MoveDedChkMark Problem');</script>");
                //throw ex;
            }
        }


    }
}