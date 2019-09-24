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
    public partial class CSTransactionControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
             FuncOptiondropdown();
             ddlFunctionOpt.Focus();
             BtnUpdate.Visible = false;
             //TranCodedropdown();

            
           }
        }

        private void FuncOptiondropdown()
        {
            string sqlquery = "SELECT FuncOpt,FuncOptDesc from A2ZFUNCOPT GROUP BY FuncOpt,FuncOptDesc";
            ddlFunctionOpt = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFunctionOpt, "A2ZCSMCUS");
        }
              
        private void PayTypedropdown()
        {
            string sqlquery = "SELECT PAYTYPE,PAYTYPEDES from A2ZPAYTYPE where ATYCLASS='" + lblClass.Text + "'";
            ddlPayType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPayType, "A2ZCSMCUS");
        }

        private void TranCodedropdown()
        {
            string sqlquery = "SELECT TrnCode,TrnCodeDesc from A2ZFUNCOPT where FuncOpt='" + ddlFunctionOpt.SelectedValue + "' GROUP BY TrnCode,TrnCodeDesc";
            ddlTranCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTranCode, "A2ZCSMCUS");
        }

        // private void TranCodedropdown()
        //{
        //    string sqlquery = "SELECT TrnCode,TrnDes from A2ZTRNCODE";
        //    ddlTranCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTranCode, "A2ZCSMCUS");
        //}

         protected void txtTrncode_TextChanged(object sender, EventArgs e)
         {
             if (ddlTranCode.SelectedValue == "-Select-")
             {
                 txtTrncode.Focus();

             }
             try
             {

                 if (txtTrncode.Text != string.Empty)
                 {
                     int MainCode = Converter.GetInteger(txtTrncode.Text);
                     A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));

                     if (getDTO.TrnCode > 0)
                     {
                         lblAccType.Text = Converter.GetString(getDTO.AccType);
                         lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                         txtTrncode.Text = Converter.GetString(getDTO.TrnCode);
                         ddlTranCode.SelectedValue = Converter.GetString(getDTO.TrnCode);

                         Int16 AccType = Converter.GetSmallInteger(lblAccType.Text);
                         A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                         if (get1DTO.AccTypeCode > 0)
                         {            
                             lblClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                             PayTypedropdown();
                         }
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

             try
             {


                 if (ddlTranCode.SelectedValue != "-Select-")
                 {

                     int MainCode = Converter.GetInteger(ddlTranCode.SelectedValue);
                     A2ZTRNCODEDTO getDTO = (A2ZTRNCODEDTO.GetInformation(MainCode));
                     if (getDTO.TrnCode > 0)
                     {
                         lblAccType.Text = Converter.GetString(getDTO.AccType);
                         lblAccTypeMode.Text = Converter.GetString(getDTO.AccTypeMode);
                         txtTrncode.Text = Converter.GetString(getDTO.TrnCode);

                         Int16 AccType = Converter.GetSmallInteger(lblAccType.Text);
                         A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
                         if (get1DTO.AccTypeCode > 0)
                         {
                             lblClass.Text = Converter.GetString(get1DTO.AccTypeClass);
                             PayTypedropdown();
                         }

                     }

                 }

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         protected void getcontrolinfo()
         {
             int funcopt = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
             int TranCode = Converter.GetInteger(txtTrncode.Text);
             int paytype = Converter.GetInteger(ddlPayType.SelectedValue);
             int TranType = Converter.GetInteger(ddlTrnType.SelectedValue);
             int TranMode = Converter.GetInteger(lblTrnMod.Text);

             A2ZTRNCTRLDTO getDTO = (A2ZTRNCTRLDTO.GetInformation(funcopt, TranCode, paytype, TranType, TranMode));
             if (getDTO.Record > 0)
             {
                 ddlTranAmtLogic.SelectedValue = Converter.GetString(getDTO.TranLogic);
                 ddlShowInt.SelectedValue = Converter.GetString(getDTO.ShowInterest);
                 ddlRecMode.SelectedValue = Converter.GetString(getDTO.RecMode);
                 txtTrnRecDesc.Text = Converter.GetString(getDTO.TrnRecDesc);
                 txtGLAccNoCR.Text = Converter.GetString(getDTO.GLAccNoCr);
                 txtGLAccNoDR.Text = Converter.GetString(getDTO.GLAccNoDr);
                 lblPayMode.Text = Converter.GetString(getDTO.TrnPayment);

                 lblClass.Text = Converter.GetString(getDTO.AccTypeClass);

                 BtnSubmit.Visible = false;
                 BtnUpdate.Visible = true;

             }
             else
             {
                 txtGLAccNoCR.Text = string.Empty;
                 txtGLAccNoDR.Text = string.Empty;
                 ddlTranAmtLogic.SelectedIndex = 0;
                 BtnSubmit.Visible = true;
                 BtnUpdate.Visible = false;
             }


         }
        

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZTRNCTRLDTO objDTO = new A2ZTRNCTRLDTO();
                objDTO.FuncOpt = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
                objDTO.AccType = Converter.GetInteger(lblAccType.Text);
                objDTO.AccTypeClass = Converter.GetSmallInteger(lblClass.Text);
                objDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
                objDTO.TranCode = Converter.GetInteger(txtTrncode.Text);
                objDTO.TranDes = Converter.GetString(ddlTranCode.SelectedItem.Text);
                objDTO.PayType = Converter.GetInteger(ddlPayType.SelectedValue);
                objDTO.TrnType = Converter.GetInteger(ddlTrnType.SelectedValue);
                objDTO.TrnMode = Converter.GetInteger(lblTrnMod.Text);
                objDTO.TrnRecDesc = Converter.GetString(txtTrnRecDesc.Text);

                objDTO.TranLogic = Converter.GetInteger(ddlTranAmtLogic.SelectedValue);
                objDTO.ShowInterest = Converter.GetInteger(ddlShowInt.SelectedValue);
                objDTO.RecMode = Converter.GetInteger(ddlRecMode.SelectedValue);
                objDTO.GLAccNoCr = Converter.GetInteger(txtGLAccNoCR.Text);
                objDTO.GLAccNoDr = Converter.GetInteger(txtGLAccNoDR.Text);
                objDTO.TrnPayment = Converter.GetSmallInteger(lblPayMode.Text);
               
                int roweffect = A2ZTRNCTRLDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    
                    clearinfo();
                    lblClass.Text = string.Empty;
                    ddlFunctionOpt.Focus();
                    ChkDebit.Checked = false;
                    ChkCredit.Checked = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void clearinfo()
        {
            ddlFunctionOpt.SelectedIndex = 0;
            ddlPayType.SelectedValue = "-Select-";
            ddlTrnType.SelectedIndex = 0;
            
            ddlTranAmtLogic.SelectedIndex = 0;
            ddlTranCode.SelectedValue = "-Select-";
            txtTrncode.Text = string.Empty;
            ddlShowInt.SelectedIndex = 0;
            ddlRecMode.SelectedIndex = 0;
            txtTrnRecDesc.Text = string.Empty;
            txtGLAccNoCR.Text = string.Empty;
            txtGLAccNoDR.Text = string.Empty;
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            A2ZTRNCTRLDTO UpDTO = new A2ZTRNCTRLDTO();
            UpDTO.FuncOpt = Converter.GetInteger(ddlFunctionOpt.SelectedValue);
            UpDTO.AccType = Converter.GetInteger(lblAccType.Text);
            UpDTO.AccTypeClass = Converter.GetSmallInteger(lblClass.Text);
            UpDTO.AccTypeMode = Converter.GetSmallInteger(lblAccTypeMode.Text);
            UpDTO.TranCode = Converter.GetInteger(txtTrncode.Text);
            UpDTO.TranDes = Converter.GetString(ddlTranCode.SelectedItem.Text);
            UpDTO.PayType = Converter.GetInteger(ddlPayType.SelectedValue);
            UpDTO.TrnType = Converter.GetInteger(ddlTrnType.SelectedValue);
            UpDTO.TrnMode = Converter.GetInteger(lblTrnMod.Text);
            UpDTO.TrnRecDesc = Converter.GetString(txtTrnRecDesc.Text);
            UpDTO.TranLogic = Converter.GetInteger(ddlTranAmtLogic.SelectedValue);
            UpDTO.ShowInterest = Converter.GetInteger(ddlShowInt.SelectedValue);
            UpDTO.RecMode = Converter.GetInteger(ddlRecMode.SelectedValue);
            UpDTO.GLAccNoCr = Converter.GetInteger(txtGLAccNoCR.Text);
            UpDTO.GLAccNoDr = Converter.GetInteger(txtGLAccNoDR.Text);
            UpDTO.TrnPayment = Converter.GetSmallInteger(lblPayMode.Text);

            int roweffect = A2ZTRNCTRLDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                clearinfo();
                
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                
                lblClass.Text = string.Empty;
                ddlFunctionOpt.Focus();
                ChkDebit.Checked = false;
                ChkCredit.Checked = false;
            }


        }

        
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT FuncOpt,TrnCode,AccType,PayType,TrnType,TrnMode,TrnLogic,ShowInt,RecMode,GLAccNoDR,GLAccNoCR FROM A2ZTRNCTRL";
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

        protected void ChkDebit_CheckedChanged(object sender, EventArgs e)
        {
            lblTrnMod.Text = "0";
            getcontrolinfo();
            ChkCredit.Checked = false;
        }

        protected void ChkCredit_CheckedChanged(object sender, EventArgs e)
        {
            lblTrnMod.Text = "1";
            getcontrolinfo();
            ChkDebit.Checked = false;
        }


       

        protected void gvDetailInfo_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                   e.Row.Style.Add("top","-1500px");
            }
        }

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (ddlPayType.SelectedValue != "-Select-")
                {

                    Int16 ClassCode = Converter.GetSmallInteger(ddlPayType.SelectedValue);
                    int TypeCode = Converter.GetInteger(ddlPayType.SelectedValue);
                    A2ZPAYTYPEDTO getDTO = (A2ZPAYTYPEDTO.GetInformation(ClassCode,TypeCode));
                    if (getDTO.PayTypeCode > 0)
                    {
                        lblPayMode.Text = Converter.GetString(getDTO.PayMode);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlFunctionOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            TranCodedropdown();
        }

        
    
        
      







    }
}