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
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountTypeCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    lblGAccType.Visible = false;
                    ddlGAccType.Visible = false;

                    txtcode.Focus();
                    BtnUpdate.Visible = false;
                    dropdown();
                    Corrdropdown();

                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        private void Gdropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeClass = 2 OR AccTypeClass = 3 OR AccTypeClass = 4 ORDER BY AccTypeClass";
            ddlGAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGAccType, "A2ZCSMCUS");
        }

        private void dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE ORDER BY AccTypeClass";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void Corrdropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeClass = 1 ORDER BY AccTypeClass";
            ddlCorrAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCorrAccType, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcType.SelectedValue == "-Select-")
                {
                    ddlAccTypeClass.Focus();

                }

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.AccTypeCode);
                        txtDescription.Text = Converter.GetString(getDTO.AccTypeDescription);
                        ddlAccTypeClass.SelectedValue = Converter.GetString(getDTO.AccTypeClass);
                        ddlAccFlag.SelectedValue = Converter.GetString(getDTO.AccFlag);
                        ddlAccTypeMode.SelectedValue = Converter.GetString(getDTO.AccTypeMode);


                        txtDepRoundingBy.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccDepRoundingBy));
                        lblchkCertNo.Text = Converter.GetString(getDTO.AccCertNo);
                        lblchk1Hide.Text = Converter.GetString(getDTO.AccessT1);
                        lblchk2Hide.Text = Converter.GetString(getDTO.AccessT2);
                        lblchk3Hide.Text = Converter.GetString(getDTO.AccessT3);
                        if (lblchkCertNo.Text == "1")
                        {
                            chkAccCertNo.Checked = true;
                        }
                        else
                        {
                            chkAccCertNo.Checked = false;
                        }

                        if (lblchk1Hide.Text == "1")
                        {
                            chkAccess1.Checked = true;
                        }
                        else
                        {
                            chkAccess1.Checked = false;
                        }

                        if (lblchk2Hide.Text == "1")
                        {
                            chkAccess2.Checked = true;
                        }
                        else
                        {
                            chkAccess2.Checked = false;
                        }

                        if (lblchk3Hide.Text == "1")
                        {
                            chkAccess3.Checked = true;
                        }
                        else
                        {
                            chkAccess3.Checked = false;
                        }

                        if (ddlAccTypeClass.SelectedValue == "5")
                        {
                            Gdropdown();
                            lblGAccType.Visible = true;
                            ddlGAccType.Visible = true;
                            if (getDTO.AccTypeGuaranty != 0)
                            {
                                ddlGAccType.SelectedValue = Converter.GetString(getDTO.AccTypeGuaranty);
                            }
                        }
                        else
                        {
                            lblGAccType.Visible = false;
                            ddlGAccType.Visible = false;
                        }


                        if (getDTO.AccCorrType != 0)
                        {
                            ddlCorrAccType.SelectedValue = Converter.GetString(getDTO.AccCorrType);
                        }
                        else
                        {
                            ddlCorrAccType.SelectedIndex = 0;
                        }


                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        txtDepRoundingBy.Text = string.Empty;
                        ddlAcType.SelectedValue = "-Select-";
                        ddlAccFlag.SelectedValue = "-Select-";
                        ddlAccTypeMode.SelectedValue = "-Select-";
                        ddlGAccType.SelectedValue = "-Select-";
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtDescription.Focus();
                        chkAccCertNo.Checked = false;
                        chkAccess1.Checked = false;
                        chkAccess2.Checked = false;
                        chkAccess3.Checked = false;
                        lblchkCertNo.Text = "0";
                        lblchk1Hide.Text = "0";
                        lblchk2Hide.Text = "0";
                        lblchk3Hide.Text = "0";

                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtcode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcType.SelectedValue == "-Select-")
                {
                    txtcode.Focus();
                    txtcode.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtDepRoundingBy.Text = string.Empty;
                    ddlAccTypeClass.SelectedValue = "0";
                    ddlAccFlag.SelectedValue = "0";
                    ddlAccTypeMode.SelectedValue = "0";

                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    chkAccCertNo.Checked = false;
                    chkAccessHR.Checked = false;
                    chkAccess1.Checked = false;
                    chkAccess2.Checked = false;
                    chkAccess3.Checked = false;
                    lblchkCertNo.Text = "0";
                    lblchkAccessHR.Text = "0";
                    lblchk1Hide.Text = "0";
                    lblchk2Hide.Text = "0";
                    lblchk3Hide.Text = "0";
                }

                if (ddlAcType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.AccTypeCode);
                        txtDescription.Text = Converter.GetString(getDTO.AccTypeDescription);
                        ddlAccTypeClass.SelectedValue = Converter.GetString(getDTO.AccTypeClass);
                        ddlAccFlag.SelectedValue = Converter.GetString(getDTO.AccFlag);
                        ddlAccTypeMode.SelectedValue = Converter.GetString(getDTO.AccTypeMode);
                        txtDepRoundingBy.Text = Converter.GetString(getDTO.AccDepRoundingBy);
                        lblchkCertNo.Text = Converter.GetString(getDTO.AccCertNo);
                        lblchkAccessHR.Text = Converter.GetString(getDTO.AccAccessFlag);

                        lblchk1Hide.Text = Converter.GetString(getDTO.AccessT1);
                        lblchk2Hide.Text = Converter.GetString(getDTO.AccessT2);
                        lblchk3Hide.Text = Converter.GetString(getDTO.AccessT3);

                        if (lblchkCertNo.Text == "1")
                        {
                            chkAccCertNo.Checked = true;
                        }
                        else
                        {
                            chkAccCertNo.Checked = false;
                        }

                        if (lblchkAccessHR.Text == "1")
                        {
                            chkAccessHR.Checked = true;
                        }
                        else
                        {
                            chkAccessHR.Checked = false;
                        }

                        if (lblchk1Hide.Text == "1")
                        {
                            chkAccess1.Checked = true;
                        }
                        else
                        {
                            chkAccess1.Checked = false;
                        }

                        if (lblchk2Hide.Text == "1")
                        {
                            chkAccess2.Checked = true;
                        }
                        else
                        {
                            chkAccess2.Checked = false;
                        }

                        if (lblchk3Hide.Text == "1")
                        {
                            chkAccess3.Checked = true;
                        }
                        else
                        {
                            chkAccess3.Checked = false;
                        }

                        if (getDTO.AccCorrType != 0)
                        {
                            ddlCorrAccType.SelectedValue = Converter.GetString(getDTO.AccCorrType);
                        }


                        if (ddlAccTypeClass.SelectedValue == "5")
                        {
                            Gdropdown();
                            lblGAccType.Visible = true;
                            ddlGAccType.Visible = true;
                            if (getDTO.AccTypeGuaranty != 0)
                            {
                                ddlGAccType.SelectedValue = Converter.GetString(getDTO.AccTypeGuaranty);
                            }
                        }
                        else
                        {
                            lblGAccType.Visible = false;
                            ddlGAccType.Visible = false;
                        }

                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtDescription.Focus();


                    }
                    else
                    {
                        txtcode.Focus();
                        txtcode.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        txtDepRoundingBy.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        chkAccCertNo.Checked = false;
                        chkAccessHR.Checked = false;
                        chkAccess1.Checked = false;
                        chkAccess2.Checked = false;
                        chkAccess3.Checked = false;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAcType_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZACCTYPEDTO objDTO = new A2ZACCTYPEDTO();

                objDTO.AccTypeCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.AccTypeDescription = Converter.GetString(txtDescription.Text);
                objDTO.AccTypeClass = Converter.GetSmallInteger(ddlAccTypeClass.SelectedValue);
                objDTO.AccFlag = Converter.GetSmallInteger(ddlAccFlag.SelectedValue);
                objDTO.AccTypeMode = Converter.GetSmallInteger(ddlAccTypeMode.SelectedValue);
                objDTO.AccDepRoundingBy = Converter.GetDecimal(txtDepRoundingBy.Text);
                objDTO.AccCertNo = Converter.GetSmallInteger(lblchkCertNo.Text);
                objDTO.AccAccessFlag = Converter.GetSmallInteger(lblchkAccessHR.Text);

                objDTO.AccessT1 = Converter.GetSmallInteger(lblchk1Hide.Text);
                objDTO.AccessT2 = Converter.GetSmallInteger(lblchk2Hide.Text);
                objDTO.AccessT3 = Converter.GetSmallInteger(lblchk3Hide.Text);


                objDTO.AccTypeGuaranty = Converter.GetSmallInteger(ddlGAccType.SelectedValue);



                objDTO.AccCorrType = Converter.GetInteger(ddlCorrAccType.SelectedValue);


                int roweffect = A2ZACCTYPEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    gvDetail();
                    txtcode.Focus();
                    clearinfo();
                    dropdown();
                    chkAccCertNo.Checked = false;
                    chkAccessHR.Checked = false;
                    chkAccess1.Checked = false;
                    chkAccess2.Checked = false;
                    chkAccess3.Checked = false;
                }
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

                A2ZACCTYPEDTO UpDTO = new A2ZACCTYPEDTO();
                UpDTO.AccTypeCode = Converter.GetSmallInteger(txtcode.Text);
                UpDTO.AccTypeDescription = Converter.GetString(txtDescription.Text);
                UpDTO.AccTypeClass = Converter.GetSmallInteger(ddlAccTypeClass.SelectedValue);
                UpDTO.AccFlag = Converter.GetSmallInteger(ddlAccFlag.SelectedValue);
                UpDTO.AccTypeMode = Converter.GetSmallInteger(ddlAccTypeMode.SelectedValue);
                UpDTO.AccDepRoundingBy = Converter.GetDecimal(txtDepRoundingBy.Text);
                UpDTO.AccCertNo = Converter.GetSmallInteger(lblchkCertNo.Text);
                UpDTO.AccAccessFlag = Converter.GetSmallInteger(lblchkAccessHR.Text);

                UpDTO.AccessT1 = Converter.GetSmallInteger(lblchk1Hide.Text);
                UpDTO.AccessT2 = Converter.GetSmallInteger(lblchk2Hide.Text);
                UpDTO.AccessT3 = Converter.GetSmallInteger(lblchk3Hide.Text);
                UpDTO.AccTypeGuaranty = Converter.GetSmallInteger(ddlGAccType.SelectedValue);

                UpDTO.AccCorrType = Converter.GetInteger(ddlCorrAccType.SelectedValue);

                int roweffect = A2ZACCTYPEDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    gvDetail();
                    dropdown();
                    clearinfo();
                    ddlAcType.SelectedValue = "-Select-";
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtcode.Focus();
                    chkAccCertNo.Checked = false;
                    chkAccessHR.Checked = false;
                    chkAccess1.Checked = false;
                    chkAccess2.Checked = false;
                    chkAccess3.Checked = false;

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }
        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtDepRoundingBy.Text = string.Empty;
            ddlAccTypeClass.SelectedValue = "0";
            ddlAccFlag.SelectedValue = "0";
            ddlAccTypeMode.SelectedValue = "0";
            if (ddlGAccType.SelectedValue != "0")
            {
                ddlGAccType.SelectedValue = "-Select-";
            }
            lblchkCertNo.Text = "0";
            lblchkAccessHR.Text = "0";
            lblchk1Hide.Text = "0";
            lblchk2Hide.Text = "0";
            lblchk3Hide.Text = "0";

            if (ddlCorrAccType.SelectedValue != "0")
            {
                ddlCorrAccType.SelectedValue = "-Select-";
            }
        }

        protected void ddlAccTypeClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }

        protected void chkAccess1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccess1.Checked == true)
            {
                lblchk1Hide.Text = "1";
            }
            else
            {
                lblchk1Hide.Text = "0";
            }
        }

        protected void chkAccess2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccess2.Checked == true)
            {
                lblchk2Hide.Text = "1";
            }
            else
            {
                lblchk2Hide.Text = "0";
            }

        }

        protected void chkAccess3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccess3.Checked == true)
            {
                lblchk3Hide.Text = "1";
            }
            else
            {
                lblchk3Hide.Text = "0";
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT AccTypeCode,AccTypeDescription,AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode !=0";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void chkAccCertNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccCertNo.Checked == true)
            {
                lblchkCertNo.Text = "1";
            }
            else
            {
                lblchkCertNo.Text = "0";
            }
        }

        protected void chkAccessHR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccessHR.Checked == true)
            {
                lblchkAccessHR.Text = "1";
            }
            else
            {
                lblchkAccessHR.Text = "0";
            }
        }
    }
}
