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
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HumanResource;

namespace ATOZWEBMCUS.Pages
{
    public partial class SYSGradeCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcode.Focus();
                BtnUpdate.Visible = false;
                dropdown();
            }
        }

        
        private void dropdown()
        {
            string sqlquery = "SELECT Grade,GradeDesc from A2ZGRADE";
            ddlGrade = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGrade, "A2ZHRMCUS");
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Grade,GradeDesc FROM A2ZGRADE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
        }

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlGrade.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZGRADEDTO getDTO = (A2ZGRADEDTO.GetGradeInformation(MainCode));

                    if (getDTO.ID > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Grade);
                        txtDescription.Text = Converter.GetString(getDTO.GradeDesc);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlGrade.SelectedValue = Converter.GetString(getDTO.Grade);
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

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrade.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlGrade.SelectedValue != "-Select-")
                {

                    Int16  MainCode = Converter.GetSmallInteger(ddlGrade.SelectedValue);
                    A2ZGRADEDTO getDTO = (A2ZGRADEDTO.GetGradeInformation(MainCode));
                    if (getDTO.ID > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Grade);
                        txtDescription.Text = Converter.GetString(getDTO.GradeDesc);
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
                A2ZGRADEDTO objDTO = new A2ZGRADEDTO();

                objDTO.Grade = Converter.GetSmallInteger(txtcode.Text);
                objDTO.GradeDesc = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZGRADEDTO.InsertGradeInformation(objDTO);
                if (roweffect > 0)
                {
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZGRADEDTO UpDTO = new A2ZGRADEDTO();
            UpDTO.Grade = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.GradeDesc = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZGRADEDTO.UpdateGradeInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
                //     ddlNature.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                gvDetail();
                txtcode.Focus();

            }
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