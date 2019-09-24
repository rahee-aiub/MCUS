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
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class STUnitCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                UnitDropdown();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                
                txtDescription.Text = string.Empty;
                txtDescription.Focus();
            }

        }
        private void UnitDropdown()
        {
            string sqlquery = "SELECT UnitNo,UnitDesc from A2ZUNITCODE";
            ddlUnit = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUnit, "A2ZSTMCUS");
        }
        public void clearInfo()
        {
            txtDescription.Text = string.Empty;

        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnit.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlUnit.Focus();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlUnit.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlUnit.SelectedValue);
                    A2ZUNITDTO getDTO = (A2ZUNITDTO.GetInformation(code));
                    if (getDTO.UnitNo > 0)
                    {
                        hdnGrpCode.Text = Converter.GetString(getDTO.UnitNo);

                        txtDescription.Text = Converter.GetString(getDTO.UnitDesc);
                        txtDescription.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
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
                if (txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    
                    int totrec = gvDetailInfo.Rows.Count;

                    int lastGrpCode = (totrec + 1);


                    if (lastGrpCode < 10)
                    {
                        hdnGrpCode.Text = Converter.GetString("0" + lastGrpCode);
                    }

                    A2ZUNITDTO objDTO = new A2ZUNITDTO();

                    objDTO.UnitNo = Converter.GetInteger(hdnGrpCode.Text);

                    objDTO.UnitDesc = Converter.GetString(txtDescription.Text);

                    int roweffect = A2ZUNITDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        UnitDropdown();
                        clearInfo();
                        BtnUpdate.Visible = false;
                        BtnSubmit.Visible = true;
                        gvDetail();
                        gvDetailInfo.Visible = false;
                        txtDescription.Text = string.Empty;
                        txtDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlUnit.SelectedValue != "-Select-")
            {

                A2ZUNITDTO UpDTO = new A2ZUNITDTO();
                UpDTO.UnitNo = Converter.GetInteger(hdnGrpCode.Text);

                UpDTO.UnitDesc = Converter.GetString(txtDescription.Text);
                int roweffect = A2ZUNITDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    UnitDropdown();
                    clearInfo();
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    txtDescription.Text = string.Empty;
                    txtDescription.Focus();
                }
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT UnitNo,UnitDesc FROM A2ZUNITCODE WHERE UnitNo!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZSTMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetailInfo.Visible = true;
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
