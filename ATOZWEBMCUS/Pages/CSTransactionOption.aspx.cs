using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSTransactionOption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAccType.Focus();
                BtnUpdate.Visible = false;
                Accdropdown();
   

            }
        }

        private void Accdropdown()
        {

            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }
      
        private void PayTypedropdown()
        {
            string sqlquery = "SELECT PayType,PayTypeDes from A2ZPAYTYPE where AtyClass='" + lblclass.Text + "'";
            ddlPayType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPayType, "A2ZCSMCUS");
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
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                        lblclass.Text = Converter.GetString(getDTO.AccTypeClass);
                        lblTypeDes.Text = Converter.GetString(getDTO.AccTypeDescription);
                        PayTypedropdown();
                    }
                    else
                    {
                        ddlFunctionOpt.SelectedValue = "0";
                     //   ddlPayType.SelectedValue = "-Select-";
                        ddlTrnType.SelectedValue = "0";
                        ddlTrnMode.SelectedValue = "0";
                        ddlTranAmtLogic.SelectedIndex = 0;
                        ddlAcType.SelectedValue = "-Select-";
                        lblclass.Text = string.Empty;

                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAcType.SelectedValue == "-Select-")
            {
                txtAccType.Focus();
                clearinfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }
            if (ddlAcType.SelectedValue != "-Select-")
            {

                Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                    lblclass.Text = Converter.GetString(getDTO.AccTypeClass);
                    lblTypeDes.Text = Converter.GetString(getDTO.AccTypeDescription);
                    PayTypedropdown();
                }
                else
                {
                    txtAccType.Focus();
                    txtAccType.Text = string.Empty;
                    lblclass.Text = string.Empty;
                }


            }

        }

        private void clearinfo()
        {
            ddlFunctionOpt.SelectedValue = "0";
            ddlAcType.SelectedValue = "0"; 
            ddlPayType.SelectedIndex = 0;
            ddlTrnType.SelectedValue = "0";
       
            ddlTrnMode.SelectedValue = "0";
            ddlTranAmtLogic.SelectedIndex = 0;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZTRNOPTDTO objDTO = new A2ZTRNOPTDTO();

                objDTO.AccType = Converter.GetInteger(txtAccType.Text);
                objDTO.AccTypeDes = Converter.GetString(lblTypeDes.Text);
                objDTO.FunctionOption = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
                objDTO.PayType = Converter.GetInteger(ddlPayType.SelectedValue);
                objDTO.TrnType = Converter.GetInteger(ddlTrnType.SelectedValue);
                objDTO.TrnMode = Converter.GetInteger(ddlTrnMode.SelectedValue);
                objDTO.TrnAmtLogic = Converter.GetInteger(ddlTranAmtLogic.SelectedValue);
                objDTO.TrnValidation = Converter.GetInteger(ddlTranValidation.SelectedValue);


                int roweffect = A2ZTRNOPTDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();
                    clearinfo();
                    lblclass.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZTRNOPTDTO UpDTO = new A2ZTRNOPTDTO();
            UpDTO.AccType = Converter.GetInteger(txtAccType.Text);
            UpDTO.FunctionOption = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
            UpDTO.PayType = Converter.GetInteger(ddlPayType.SelectedValue);
            UpDTO.TrnType = Converter.GetInteger(ddlTrnType.SelectedValue);
            UpDTO.TrnMode = Converter.GetInteger(ddlTrnMode.SelectedValue);
            UpDTO.TrnAmtLogic = Converter.GetInteger(ddlTranAmtLogic.SelectedValue);
            UpDTO.TrnValidation = Converter.GetInteger(ddlTranValidation.SelectedValue);
            UpDTO.AccTypeDes = Converter.GetString(lblTypeDes.Text);

            int roweffect = A2ZTRNOPTDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                clearinfo();
                ddlAcType.SelectedValue = "0";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtAccType.Focus();
                txtAccType.Text = string.Empty;
                lblclass.Text = string.Empty;
            }


        }

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayType.SelectedValue != "-Select-")
            {
                Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                int functopt = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
                int PayType = Converter.GetInteger(ddlPayType.SelectedValue);

                A2ZTRNOPTDTO TrnDTO = (A2ZTRNOPTDTO.GetInformation(MainCode, functopt,PayType));

                if (TrnDTO.Record > 0)
                {
                    
                    ddlTrnType.SelectedValue = Converter.GetString(TrnDTO.TrnType);
                    ddlTrnMode.SelectedValue = Converter.GetString(TrnDTO.TrnMode);
                    ddlTranAmtLogic.SelectedValue = Converter.GetString(TrnDTO.TrnAmtLogic);
                    ddlTranValidation.SelectedValue = Converter.GetString(TrnDTO.TrnValidation);
                    BtnUpdate.Visible = true;
                    BtnSubmit.Visible = false;
                }
                else
                {

                    ddlPayType.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                }



            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT AccType,FuncOpt,PayType,TrnType,TrnMode,TrnLogic,TrnValidation FROM A2ZTRNOPT group by AccType,FuncOpt,PayType,TrnType,TrnMode,TrnLogic,TrnValidation";
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

    }
}