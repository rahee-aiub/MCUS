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
    public partial class CSParameterControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnAccountUpdate.Visible = false;
                txtAccType.Focus();
                //dropdown();
                gridview();
                //CheckFlagDropdown();
                AccountTypeDropdown();
            }

        }

        protected void AccountTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlAccount = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccount, "A2ZCSMCUS");

        }

        protected void gridview()
        {
            string sqlquery = "SELECT Code, Description from A2ZACCFIELDS where FieldsFlag='2'";
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
                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {

                    chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                    int flag = 1;
                    if (chk.Checked)
                    {

                        Label2.Text = Convert.ToString(flag);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        Label3.Text = Converter.GetString(gvDescription.Rows[i].Cells[1].Text);
                        objDTO.Description = Converter.GetString(Label3.Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                        roweffect = A2ZACCCTRLDTO.InsertInformation(objDTO);
                        chk.Checked = false;
                    }

                    else
                    {
                        flag = 0;
                        Label2.Text = Convert.ToString(flag);
                        Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                        Label3.Text = Converter.GetString(gvDescription.Rows[i].Cells[1].Text);
                        objDTO.Description = Converter.GetString(Label3.Text);
                        objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                        objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                        roweffect = A2ZACCCTRLDTO.InsertInformation(objDTO);
                        chk.Checked = false;
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
                throw ex;
            }
        }

        protected void chkDescription_CheckedChanged1(object sender, EventArgs e)
        {

        }


        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
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
            try
            {


                if (ddlAccount.SelectedValue != "-Select-")
                {

                    Int16 code = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                    Int16 ccode = 2;

                    //string sqlquery = "SELECT ProductCode, RecordCode,Description from A2ZACCCTRL where ProductCode='" + code + "' and ControlCode='2'";
                    //gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZCSMCUS");



                    for (int i = 0; i < gvDescription.Rows.Count; i++)
                    {
                        A2ZACCCTRLDTO getDTO = new A2ZACCCTRLDTO();
                        //Int16 pcode = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                        Int16 rcode = Converter.GetSmallInteger(gvDescription.Rows[i].Cells[0].Text);
                        getDTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                        if (getDTO.ProductCode > 0 && getDTO.RecordCode > 0)
                        {
                            Label4.Text = Converter.GetString(getDTO.RecordFlag);
                            txtAccType.Text = Converter.GetString(code);
                            //gvDescription.Visible = false;
                            //gvDetails.Visible = true;
                            BtnAccountOpenSubmit.Visible = false;
                            btnAccountUpdate.Visible = true;

                            CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");

                            if (Label4.Text == "1")
                            {

                                chk.Checked = true;

                            }
                            else
                            {
                                chk.Checked = false;
                            }
                        }

                        else
                        {
                            CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                            chk.Checked = false;

                        }

                    }


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAccountUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "DELETE FROM A2ZACCCTRL WHERE ProductCode = '" + txtAccType.Text + "' AND ControlCode = 2";
                int row = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUS"));                
                
                A2ZACCCTRLDTO objDTO = new A2ZACCCTRLDTO();

                //objDTO.ProductCode = Converter.GetSmallInteger(ddlAccount.SelectedValue);
                objDTO.ProductCode = Converter.GetSmallInteger(txtAccType.Text);
                objDTO.ControlCode = Converter.GetSmallInteger(ddlFlagCheck.SelectedValue);
                int roweffect = 0;
                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {

                    CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                    int flag = 1;
                    if (chk.Checked)
                    {
                        Label2.Text = Convert.ToString(flag);
                    }
                    else
                    {
                        flag = 0;
                        Label2.Text = Convert.ToString(flag);
                    }

                    Label1.Text = Converter.GetString(gvDescription.Rows[i].Cells[0].Text);
                    objDTO.RecordCode = Converter.GetSmallInteger(Label1.Text);
                    Label3.Text = Converter.GetString(gvDescription.Rows[i].Cells[1].Text);
                    objDTO.Description = Converter.GetString(Label3.Text);
                    objDTO.RecordFlag = Converter.GetSmallInteger(Label2.Text);
                    roweffect = A2ZACCCTRLDTO.InsertInformation(objDTO);
                    chk.Checked = false;
                }

                if (roweffect > 0)
                {
                    txtAccType.Focus();
                    //ddlAccount.SelectedValue = "-Select-";
                    clearinfo();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
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
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('This Account Code Does not Exist in file');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        txtAccType.Focus();
                        gvDescription.Visible = true;
                        gvDetails.Visible = false;
                        BtnAccountOpenSubmit.Visible = true;
                        btnAccountUpdate.Visible = false;

                    }
                    return;


                }

            }

            if (txtAccType.Text != string.Empty)
            {
                Int16 code = Converter.GetSmallInteger(txtAccType.Text);
                //string sqlquery = "SELECT ProductCode, RecordCode,Description from A2ZACCCTRL where ProductCode='" + code + "'and ControlCode='2'";
                //gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZCSMCUS");

                A2ZACCCTRLDTO getDTO = new A2ZACCCTRLDTO();
                for (int i = 0; i < gvDescription.Rows.Count; i++)
                {

                    //Int16 pcode = Converter.GetSmallInteger(txtProductCode.Text);
                    Int16 rcode = Converter.GetSmallInteger(gvDescription.Rows[i].Cells[0].Text);
                    Int16 ccode = 2;
                    getDTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                    if (getDTO.ProductCode > 0 && getDTO.RecordCode > 0)
                    {
                        Label4.Text = Converter.GetString(getDTO.RecordFlag);
                        ddlAccount.SelectedValue = Converter.GetString(getDTO.ProductCode);
                        BtnAccountOpenSubmit.Visible = false;
                        btnAccountUpdate.Visible = true;
                        //gvDescription.Visible = false;
                        //gvDetails.Visible = true;
                        ddlAccount.Focus();

                        CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");

                        if (Label4.Text == "1")
                        {

                            chk.Checked = true;

                        }
                        else
                        {
                            chk.Checked = false;
                        }

                    }

                    else
                    {
                        CheckBox chk = (CheckBox)gvDescription.Rows[i].Cells[0].FindControl("chkDescription");
                        chk.Checked = false;

                    }

                }
            }


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
