using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSTransactionTypeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcode.Focus();
                BtnUpdate.Visible = false;
                Trandropdown();
            }
        }

       protected void Trandropdown()
        {
            string sqlquery = "SELECT TRNTYPE,TRNTYPEDES from A2ZTRNTYPE";
            ddlType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlType, "A2ZCSMCUS");
        }
       private void clearinfo()
       {
           txtcode.Text = string.Empty;
           txtDescription.Text = string.Empty;
       }

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
                   int MainCode = Converter.GetInteger(txtcode.Text);
                   A2ZTRANTYPEDTO getDTO = (A2ZTRANTYPEDTO.GetInformation(MainCode));

                   if (getDTO.record > 0)
                   {
                       txtcode.Text = Converter.GetString(getDTO.TrnTypeCode);
                       txtDescription.Text = Converter.GetString(getDTO.TrnTypeDescription);
                       BtnSubmit.Visible = false;
                       BtnUpdate.Visible = true;
                       ddlType.SelectedValue = Converter.GetString(getDTO.TrnTypeCode);
                       txtDescription.Focus();
                   }
                   else
                   {
                       txtDescription.Text = string.Empty;
                  //     ddlType.SelectedValue = "0";
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

       protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (ddlType.SelectedValue == "-Select-")
           {
               txtcode.Focus();
               txtcode.Text = string.Empty;
               txtDescription.Text = string.Empty;
               BtnSubmit.Visible = true;
               BtnUpdate.Visible = false;
           }

           try
           {


               if (ddlType.SelectedValue != "-Select-")
               {

                   Int16 MainCode = Converter.GetSmallInteger(ddlType.SelectedValue);
                   A2ZTRANTYPEDTO getDTO = (A2ZTRANTYPEDTO.GetInformation(MainCode));
                   if (getDTO.record > 0)
                   {
                       txtcode.Text = Converter.GetString(getDTO.TrnTypeCode);
                       txtDescription.Text = Converter.GetString(getDTO.TrnTypeDescription);
                       BtnSubmit.Visible = false;
                       BtnUpdate.Visible = true;
                       txtDescription.Focus();


                   }
                   else
                   {
                       txtcode.Focus();
                       txtcode.Text = string.Empty;
                       txtDescription.Text = string.Empty;
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
               A2ZTRANTYPEDTO objDTO = new A2ZTRANTYPEDTO();

               objDTO.TrnTypeCode = Converter.GetInteger(txtcode.Text);
               objDTO.TrnTypeDescription = Converter.GetString(txtDescription.Text);

               int roweffect = A2ZTRANTYPEDTO.InsertInformation(objDTO);
               if (roweffect > 0)
               {

                   gvDetail();
                   txtcode.Focus();
                   clearinfo();
                   Trandropdown();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       protected void BtnUpdate_Click(object sender, EventArgs e)
       {
           A2ZTRANTYPEDTO UpDTO = new A2ZTRANTYPEDTO();
           UpDTO.TrnTypeCode = Converter.GetInteger(txtcode.Text);
           UpDTO.TrnTypeDescription= Converter.GetString(txtDescription.Text);

           int roweffect = A2ZTRANTYPEDTO.UpdateInformation(UpDTO);
           if (roweffect > 0)
           {

               gvDetail();
               Trandropdown();
               clearinfo();
              // ddlType.SelectedValue = "0";
               BtnSubmit.Visible = true;
               BtnUpdate.Visible = false;
               txtcode.Focus();

           }
       }

       protected void BtnExit_Click(object sender, EventArgs e)
       {
           Response.Redirect("A2ZERPModule.aspx");
       }

       protected void gvDetail()
       {
           string sqlquery3 = "SELECT TrnType,TrnTypeDes FROM A2ZTRNTYPE";
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


    }
}