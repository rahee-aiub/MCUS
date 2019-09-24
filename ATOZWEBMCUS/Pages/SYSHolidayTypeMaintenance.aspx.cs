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
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;



namespace ATOZWEBMCUS.Pages
{
    public partial class SYSHolidayTypeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtHolType.Focus();
                BtnUpdate.Visible = false;
                dropdown();
            }

        }

        private void dropdown()
        {
            string sqlquery = "SELECT HolType,HolTypeDescription from A2ZHOLIDAYTYPE";
            ddlHolType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlHolType, "A2ZHKMCUS");
        }

        protected void txtHolType_TextChanged(object sender, EventArgs e)
        {
            if (ddlHolType.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtHolType.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtHolType.Text);
                    A2ZHOLIDAYTYPEDTO getDTO = (A2ZHOLIDAYTYPEDTO.GetInformation(MainCode));

                    if (getDTO.HolType > 0)
                    {
                        txtHolType.Text = Converter.GetString(getDTO.HolType);
                        txtDescription.Text = Converter.GetString(getDTO.HolTypeDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlHolType.SelectedValue = Converter.GetString(getDTO.HolType);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
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

        protected void ddlHolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHolType.SelectedValue == "-Select-")
            {
                txtHolType.Focus();
                txtHolType.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlHolType.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlHolType.SelectedValue);
                    A2ZHOLIDAYTYPEDTO getDTO = (A2ZHOLIDAYTYPEDTO.GetInformation(MainCode));
                    if (getDTO.HolType > 0)
                    {
                        txtHolType.Text = Converter.GetString(getDTO.HolType);
                        txtDescription.Text = Converter.GetString(getDTO.HolTypeDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtDescription.Focus();


                    }
                    else
                    {
                        txtHolType.Focus();
                        txtHolType.Text = string.Empty;
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
        private void clearinfo()
        {
            txtHolType.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZHOLIDAYTYPEDTO objDTO = new A2ZHOLIDAYTYPEDTO();

                objDTO.HolType = Converter.GetInteger(txtHolType.Text);
                objDTO.HolTypeDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZHOLIDAYTYPEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtHolType.Focus();
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZHOLIDAYTYPEDTO UpDTO = new A2ZHOLIDAYTYPEDTO();
            UpDTO.HolType = Converter.GetInteger(txtHolType.Text);
            UpDTO.HolTypeDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZHOLIDAYTYPEDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtHolType.Focus();
                gvDetail();

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT HolType,HolTypeDescription FROM A2ZHOLIDAYTYPE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
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
