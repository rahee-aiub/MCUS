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
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountOpenControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    btnAccountUpdate.Visible = false;
                    txtAccType.Focus();
                    gridview();
                    AccountTypeDropdown();
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        protected void AccountTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlAccount = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccount, "A2ZCSMCUS");

        }

        protected void gridview()
        {
            string sqlquery = "SELECT Code, Description from A2ZACCFIELDS where FieldsFlag='1'";
            gvDescription = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDescription, "A2ZCSMCUS");


        }
        private void clearinfo()
        {
            txtAccType.Text = string.Empty;
        }

        protected void BtnAccountOpenExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        //protected void dropdown()
        //{
        //    string sqlquery = "SELECT ProductCode,ProductCode from A2ZACCCTRL group by ProductCode";
        //    ddlAccount = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccount, "A2ZCSMCUS");

        //}
        protected void BtnAccountOpenSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZACCCTRLDTO objDTO = new A2ZACCCTRLDTO();

                objDTO.ProductCode = Converter.GetSmallInteger(txtAccType.Text);
                objDTO.ControlCode = Converter.GetSmallInteger(ddlFlagCheck.SelectedValue);

                int roweffect = 0;
                CheckBox chk = new CheckBox();
                CheckBox chk1 = new CheckBox();
                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {

                    chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                    chk1 = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chk1Description");
                    int flag = 1;
                    int flag1 = 1;
                    if (chk.Checked)
                    {
                        Label2.Text = Convert.ToString(flag);
                        Label5.Text = Convert.ToString(flag1);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        Label3.Text = Converter.GetString(gvDescription.Rows[i].Cells[1].Text);
                        objDTO.Description = Converter.GetString(Label3.Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                        chk.Checked = false;

                        if (chk1.Checked)
                        {
                            objDTO.FuncFlag = Converter.GetSmallInteger(Label5.Text);
                            chk1.Checked = false;

                        }

                        roweffect = A2ZACCCTRLDTO.InsertInformation(objDTO);

                    }

                    else
                    {
                        flag = 0;
                        flag1 = 0;
                        Label2.Text = Convert.ToString(flag);
                        Label5.Text = Convert.ToString(flag1);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        Label3.Text = Converter.GetString(gvDescription.Rows[i].Cells[1].Text);
                        objDTO.Description = Converter.GetString(Label3.Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                        objDTO.FuncFlag = Converter.GetSmallInteger(Label5.Text);
                        roweffect = A2ZACCCTRLDTO.InsertInformation(objDTO);
                        chk.Checked = false;
                        chk1.Checked = false;
                    }
                }


                if (roweffect > 0)
                {
                    txtAccType.Focus();

                    clearinfo();



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnAccountOpenSubmit_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void chkDescription_CheckedChanged1(object sender, EventArgs e)
        {

        }


        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAccount.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        txtAccType.Focus();
                        gvDescription.Visible = true;
                        gvDetails.Visible = false;
                        BtnAccountOpenSubmit.Visible = true;
                        btnAccountUpdate.Visible = false;

                    }

                }

                if (ddlAccount.SelectedValue == "-Select-")
                {
                    gvDescription.Visible = true;
                    gvDetails.Visible = false;
                    BtnAccountOpenSubmit.Visible = true;
                    btnAccountUpdate.Visible = false;
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                }

                if (ddlAccount.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlAccount.SelectedValue);

                    string sqlquery = "SELECT ProductCode, RecordCode,Description from A2ZACCCTRL where ProductCode='" + code + "' and ControlCode='1'";
                    gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZCSMCUS");
                    for (int i = 0; i < gvDetails.Rows.Count; i++)
                    {
                        A2ZACCCTRLDTO getDTO = new A2ZACCCTRLDTO();
                        //Int16 pcode = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                        Int16 rcode = Converter.GetSmallInteger(gvDetails.Rows[i].Cells[0].Text);
                        Int16 ccode = 1;
                        getDTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                        if (getDTO.ProductCode > 0 && getDTO.RecordCode > 0)
                        {
                            Label4.Text = Converter.GetString(getDTO.RecordFlag);
                            Label5.Text = Converter.GetString(getDTO.FuncFlag);
                            txtAccType.Text = Converter.GetString(code);
                            gvDescription.Visible = false;
                            gvDetails.Visible = true;
                            BtnAccountOpenSubmit.Visible = false;
                            btnAccountUpdate.Visible = true;

                        }


                        CheckBox chk = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chkUpdate");
                        CheckBox chk1 = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chk1Update");

                        if (Label4.Text == "1")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                        if (Label5.Text == "1")
                        {
                            chk1.Checked = true;
                        }
                        else
                        {
                            chk1.Checked = false;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAccount_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void btnAccountUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZACCCTRLDTO objDTO = new A2ZACCCTRLDTO();

                objDTO.ProductCode = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                objDTO.ProductCode = Converter.GetSmallInteger(txtAccType.Text);
                int roweffect = 0;
                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {

                    CheckBox chk = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chkUpdate");
                    CheckBox chk1 = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chk1Update");
                    int flag = 1;
                    int flag1 = 1;
                    if (chk.Checked)
                    {
                        Label2.Text = Convert.ToString(flag);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);

                        if (chk1.Checked)
                        {
                            Label5.Text = Convert.ToString(flag1);
                            objDTO.FuncFlag = Converter.GetSmallInteger(Label5.Text);
                            chk1.Checked = false;
                        }
                        else
                        {
                            flag1 = 0;
                            Label5.Text = Convert.ToString(flag1);
                            objDTO.FuncFlag = Converter.GetSmallInteger(Label5.Text);
                        }

                        roweffect = A2ZACCCTRLDTO.UpdateInformation(objDTO);
                        chk.Checked = false;
                        chk1.Checked = false;
                    }

                    else
                    {
                        flag = 0;
                        flag1 = 0;
                        Label2.Text = Convert.ToString(flag);
                        Label5.Text = Convert.ToString(flag1);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                        objDTO.FuncFlag = Converter.GetSmallInteger(Label5.Text);
                        roweffect = A2ZACCCTRLDTO.UpdateInformation(objDTO);
                        chk.Checked = false;
                        chk1.Checked = false;

                    }
                }


                if (roweffect > 0)
                {
                    txtAccType.Focus();
                    //            ddlAccount.SelectedValue = "-Select-";
                    clearinfo();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnAccountUpdate_Click Problem');</script>");
                
                //throw ex;
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAccType.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAccount.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                        txtAccType.Focus();
                    }
                    else
                    {
                        ddlAccount.SelectedValue = "-Select-";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Account Code Does not Exist in file');", true);

                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('This Account Code Does not Exist in file');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                            txtAccType.Focus();
                            gvDescription.Visible = true;
                            gvDetails.Visible = false;
                            BtnAccountOpenSubmit.Visible = true;
                            btnAccountUpdate.Visible = false;

                        //}
                        return;


                    }

                }


                if (txtAccType.Text != string.Empty)
                {
                    Int16 code = Converter.GetSmallInteger(txtAccType.Text);
                    string sqlquery = "SELECT ProductCode, RecordCode,Description from A2ZACCCTRL where ProductCode='" + code + "'and ControlCode='1'";
                    gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZCSMCUS");
                    A2ZACCCTRLDTO getDTO = new A2ZACCCTRLDTO();
                    for (int i = 0; i < gvDetails.Rows.Count; i++)
                    {

                        //Int16 pcode = Converter.GetSmallInteger(txtProductCode.Text);
                        Int16 rcode = Converter.GetSmallInteger(gvDetails.Rows[i].Cells[0].Text);
                        Int16 ccode = 1;
                        getDTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                        if (getDTO.ProductCode > 0 && getDTO.RecordCode > 0)
                        {
                            Label4.Text = Converter.GetString(getDTO.RecordFlag);
                            Label5.Text = Converter.GetString(getDTO.FuncFlag);
                            ddlAccount.SelectedValue = Converter.GetString(getDTO.ProductCode);
                            BtnAccountOpenSubmit.Visible = false;
                            btnAccountUpdate.Visible = true;
                            gvDescription.Visible = false;
                            gvDetails.Visible = true;
                            ddlAccount.Focus();
                        }
                        //else
                        //{

                        //    ddlAccount.SelectedValue = "-Select-";
                        //    gvDescription.Visible = true;
                        //    gvDetails.Visible = false;
                        //    BtnAccountOpenSubmit.Visible = true;
                        //    btnAccountUpdate.Visible = false;


                        //}


                        CheckBox chk = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chkUpdate");
                        CheckBox chk1 = (CheckBox)gvDetails.Rows[i].Cells[0].FindControl("chk1Update");

                        if (Label4.Text == "1")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                        if (Label5.Text == "1")
                        {
                            chk1.Checked = true;
                        }
                        else
                        {
                            chk1.Checked = false;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtAccType_TextChanged Problem');</script>");
                
                //throw ex;
            }

        }

        protected void chk1Description_CheckedChanged(object sender, EventArgs e)
        {

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

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

    }
}
