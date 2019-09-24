using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;


namespace ATOZWEBMCUS.Pages
{
    public partial class RecordsControlMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtGLCashCode.Focus();
                GLCashCodeDropdown();
            }
        }



        protected void gvDetail()
        {
            string sqlquery3 = "SELECT CtrlGLCashCode,CtrlRecType,CtrlRecLastNo FROM A2ZRECCTRLNO";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";
            ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLMCUS");

        }

       

        
        private void clearinfo()
        {
            txtGLCashCode.Text = string.Empty;
            ddlGLCashCode.SelectedValue = "-Select-";
            txtLastRecordNo.Text = string.Empty;
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

        protected void txtRecType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    int GLCode = Converter.GetInteger(txtGLCashCode.Text);
                    Int16 RecType = Converter.GetSmallInteger(txtRecType.Text);
                    A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetInformation(GLCode,RecType));

                    if (getDTO.Record > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);
                        ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLCashCode);
                        txtRecType.Text = Converter.GetString(getDTO.RecType);
                        txtLastRecordNo.Text = Converter.GetString(getDTO.RecLastNo);
                        txtLastRecordNo.Focus();
                    }
                    else
                    {
                        //txtGLCashCode.Text = string.Empty;
                        txtRecType.Focus();
                        txtLastRecordNo.Text = string.Empty;
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
                A2ZRECCTRLNODTO objDTO = new A2ZRECCTRLNODTO();
                objDTO.GLCashCode = Converter.GetInteger(txtGLCashCode.Text);
                objDTO.RecType = Converter.GetSmallInteger(txtRecType.Text);
                objDTO.RecLastNo = Converter.GetInteger(txtLastRecordNo.Text);

                int roweffect = A2ZRECCTRLNODTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    clearinfo();
                    txtGLCashCode.Focus();
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
            A2ZRECCTRLNODTO objDTO = new A2ZRECCTRLNODTO();



            objDTO.GLCashCode = Converter.GetInteger(txtGLCashCode.Text);
            objDTO.RecType = Converter.GetSmallInteger(txtRecType.Text);
            objDTO.RecLastNo = Converter.GetInteger(txtLastRecordNo.Text);

            int roweffect = A2ZRECCTRLNODTO.UpdateInformation(objDTO);
            if (roweffect > 0)
            {
                clearinfo();
                txtGLCashCode.Focus();
                gvDetail();

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtGLCashCode_TextChanged(object sender, EventArgs e)
        {
            ddlGLCashCode.SelectedValue = Converter.GetString(txtGLCashCode.Text);
            txtRecType.Focus();
        }

        protected void ddlGLCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGLCashCode.Text = Converter.GetString(ddlGLCashCode.SelectedValue);
            txtRecType.Focus();
        }

    }
}