using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class TransactionCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcode.Focus();
                BtnUpdate.Visible = false;
                dropdown();
                Paydropdown();

                lblPayType.Visible = false;
                txtPaytype.Visible = false;
                ddlpaytype.Visible = false;
            }
        }
        private void dropdown()
        {
            string sqlquery = "SELECT TrnCode,TrnDes from A2ZTRNCODE";
            ddlTranCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTranCode, "A2ZCSMCUS");
        }

        private void Paydropdown()
        {
            string sqlquery = "SELECT PayType,PayTypeDes from A2ZPAYTYPE WHERE AtyClass=7";
            ddlpaytype = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlpaytype, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlTranCode.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtcode.Text);
                    A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));

                    if (getDTO.TrnCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.TrnCode);
                        txtDescription.Text = Converter.GetString(getDTO.TrnDescription);
                        txtAccType.Text = Converter.GetString(getDTO.AccType);
                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);

                        if (txtAccType.Text == "99")
                        {
                            lblPayType.Visible = true;
                            txtPaytype.Visible = true;
                            ddlpaytype.Visible = true;
                            txtPaytype.Text = Converter.GetString(getDTO.PayType);
                            if (txtPaytype.Text != "0")
                            {
                                ddlpaytype.SelectedValue = Converter.GetString(getDTO.PayType);
                            }
                        }



                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlTranCode.SelectedValue = Converter.GetString(getDTO.TrnCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        //  ddlNature.SelectedValue = "-Select-";
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtDescription.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlTranCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTranCode.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlTranCode.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlTranCode.SelectedValue);
                    A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));
                    if (getDTO.TrnCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.TrnCode);
                        txtDescription.Text = Converter.GetString(getDTO.TrnDescription);
                        txtAccType.Text = Converter.GetString(getDTO.AccType);
                        lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);

                        if (txtAccType.Text == "99")
                        {
                            lblPayType.Visible = true;
                            txtPaytype.Visible = true;
                            ddlpaytype.Visible = true;
                            txtPaytype.Text = Converter.GetString(getDTO.PayType);
                            if (txtPaytype.Text != "0")
                            {
                                ddlpaytype.SelectedValue = Converter.GetString(getDTO.PayType);
                            }
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
                        txtAccType.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtAccType.Text = string.Empty;

            lblPayType.Visible = false;
            txtPaytype.Visible = false;
            ddlpaytype.Visible = false;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZTRNCODEDTO objDTO = new A2ZTRNCODEDTO();

                objDTO.TrnCode = Converter.GetInteger(txtcode.Text);
                objDTO.TrnDescription = Converter.GetString(txtDescription.Text);
                objDTO.AccType = Converter.GetInteger(txtAccType.Text);
                objDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
                objDTO.PayType = Converter.GetSmallInteger(txtPaytype.Text);
                int roweffect = A2ZTRNCODEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    AddMiscRecord();
                    txtcode.Focus();
                    clearinfo();
                    dropdown();
                    gvDetail();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void AddMiscRecord()
        {
            if (txtAccType.Text == "99")
            {
                var prm = new object[1];

                prm[0] = txtPaytype.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSAddMiscellaneousAccount", prm, "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            }


        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZTRNCODEDTO UpDTO = new A2ZTRNCODEDTO();
            UpDTO.TrnCode = Converter.GetInteger(txtcode.Text);
            UpDTO.TrnDescription = Converter.GetString(txtDescription.Text);
            UpDTO.AccType = Converter.GetInteger(txtAccType.Text);
            UpDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
            UpDTO.PayType = Converter.GetSmallInteger(txtPaytype.Text);
            int roweffect = A2ZTRNCODEDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                dropdown();
                clearinfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT TrnCode,TrnDes,AccType FROM A2ZTRNCODE WHERE TrnCode !=0";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
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


        private void InvalidAccType()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Account Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Account Type');", true);
            return;


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
        }
        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
            A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

            if (getDTO.AccTypeCode > 0)
            {
                lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);

                if (txtAccType.Text == "99")
                {
                    lblPayType.Visible = true;
                    txtPaytype.Visible = true;
                    ddlpaytype.Visible = true;
                }


            }
            else
            {
                InvalidAccType();
                txtAccType.Text = string.Empty;
                txtAccType.Focus();
                return;
            }
        }

        protected void txtPaytype_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtPaytype.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtPaytype.Text);

                    int aclass = 7;
                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(aclass, MainCode));

                    if (getDTO.record > 0)
                    {
                        txtPaytype.Text = Converter.GetString(getDTO.PayTypeCode);
                        ddlpaytype.SelectedValue = Converter.GetString(getDTO.PayTypeCode);

                    }
                    else
                    {

                        ddlpaytype.SelectedIndex = 0;


                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlpaytype.SelectedValue != "-Select-")
                {
                    int MainCode = Converter.GetInteger(ddlpaytype.SelectedValue);

                    int aclass = 7;
                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(aclass, MainCode));

                    if (getDTO.record > 0)
                    {
                        txtPaytype.Text = Converter.GetString(getDTO.PayTypeCode);

                    }
                    else
                    {

                        txtPaytype.Text = string.Empty;
                        txtPaytype.Focus();


                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}