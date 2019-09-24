using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;


namespace ATOZWEBMCUS.Pages
{
    public partial class CSPayTypeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlAccTypeClass.Focus();
                BtnUpdate.Visible = false;
            }

        }
        private void PayTypedropdown()
        {
            string sqlquery = "SELECT PayType,PayTypeDes from A2ZPAYTYPE WHERE AtyClass='" + ddlAccTypeClass.SelectedValue + "'";
            ddlType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlType, "A2ZCSMCUS");
        }

        //private void AccTypeClsdropdown()
        //{
        //    string sqlquery = "SELECT AccTypeClass,AccTypeClass from A2ZACCTYPE";
        //    ddlAccTypeClass = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccTypeClass, "A2ZCSMCUS");
        //}


        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    int ClassCode = Converter.GetInteger(ddlAccTypeClass.Text);
                    int MainCode = Converter.GetInteger(txtcode.Text);
                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(ClassCode, MainCode));

                    if (getDTO.record > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.PayTypeCode);
                        txtDescription.Text = Converter.GetString(getDTO.PayTypeDescription);
                        ddlAccTypeClass.SelectedValue = Converter.GetString(getDTO.AccTypeClass);
                        ddlPayMode.SelectedValue = Converter.GetString(getDTO.PayMode);
                        
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlType.SelectedValue = Converter.GetString(getDTO.PayTypeCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        txtDescription.Focus();
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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
         //       ddlAccTypeClass.SelectedValue = "0";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlType.SelectedValue != "-Select-")
                {

                    int ClassCode = Converter.GetInteger(ddlAccTypeClass.Text);
                    int MainCode = Converter.GetInteger(ddlType.SelectedValue);
                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(ClassCode, MainCode));
                    if (getDTO.record > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.PayTypeCode);
                        txtDescription.Text = Converter.GetString(getDTO.PayTypeDescription);
                        ddlAccTypeClass.SelectedValue = Converter.GetString(getDTO.AccTypeClass);
                        ddlPayMode.SelectedValue = Converter.GetString(getDTO.PayMode);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        txtDescription.Focus();
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZPAYTYPEDTO objDTO = new A2ZPAYTYPEDTO();

                objDTO.PayTypeCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.PayTypeDescription = Converter.GetString(txtDescription.Text);
                objDTO.AccTypeClass = Converter.GetSmallInteger(ddlAccTypeClass.SelectedValue);
                objDTO.PayMode = Converter.GetSmallInteger(ddlPayMode.SelectedValue);


                int roweffect = A2ZPAYTYPEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtcode.Focus();
                    clearinfo();
                    PayTypedropdown();
                    gvDetail();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZPAYTYPEDTO UpDTO = new A2ZPAYTYPEDTO();
            UpDTO.PayTypeCode = Converter.GetInteger(txtcode.Text);
            UpDTO.PayTypeDescription = Converter.GetString(txtDescription.Text);
            UpDTO.AccTypeClass = Converter.GetInteger(ddlAccTypeClass.SelectedValue);
            UpDTO.PayMode = Converter.GetSmallInteger(ddlPayMode.SelectedValue);

            int roweffect = A2ZPAYTYPEDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                PayTypedropdown();
                clearinfo();
             //   ddlType.SelectedValue = "0";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

            }

        }
        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
           
        }

        protected void ddlAccTypeClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            PayTypedropdown();
            txtcode.Focus();
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT AtyClass,PayType,PayTypeDes FROM A2ZPAYTYPE";
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
    }


}