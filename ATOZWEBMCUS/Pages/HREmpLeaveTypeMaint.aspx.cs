using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmpLeaveTypeMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtcode.Focus();
                BtnUpdate.Visible = false;     
                dropdown();
            }
        }

        private void dropdown()
        {
            string sqlquery = "SELECT EmpleaveCode,EmpleaveName from A2ZEMPLEAVETYPE";
            ddlLType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlLType, "A2ZHRMCUS");
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT EmpleaveCode,EmpleaveName,TotalDays,EffectSalary,Status from A2ZEMPLEAVETYPE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
        }
        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlLType.SelectedValue == "-Select-")
            {
                txtLName.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtcode.Text);
                    
                    A2ZEMPLEAVETYPEDTO getDTO = (A2ZEMPLEAVETYPEDTO.GetInformation(MainCode));


                    if (getDTO.LeaveCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LeaveCode);
                        txtLName.Text = Converter.GetString(getDTO.LeaveName);
                        txtTotalDays.Text = Converter.GetString(getDTO.TotalDays);
                        ChkSalEffect.Checked = Converter.GetBoolean(getDTO.EffectSalary);

                        LeaveStatus.Text = Converter.GetString(getDTO.Status);
                        if (LeaveStatus.Text == "False")
                        {
                            ddlLstatus.SelectedValue = "0";
                        }
                        else
                        {
                            ddlLstatus.SelectedValue = "1";
                        }

                        
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlLType.SelectedValue = Converter.GetString(getDTO.LeaveCode);
                        txtLName.Focus();
                    }
                    else
                    {
                        txtLName.Text = string.Empty; 
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtLName.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLType.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtLName.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlLType.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlLType.SelectedValue);
                    A2ZEMPLEAVETYPEDTO getDTO = (A2ZEMPLEAVETYPEDTO.GetInformation(MainCode));
                    if (getDTO.LeaveCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LeaveCode);
                        txtLName.Text = Converter.GetString(getDTO.LeaveName);
                        txtTotalDays.Text = Converter.GetString(getDTO.TotalDays);
                        ChkSalEffect.Checked = Converter.GetBoolean(getDTO.EffectSalary);
                        LeaveStatus.Text = Converter.GetString(getDTO.Status);
                        if (LeaveStatus.Text == "False")
                        {
                            ddlLstatus.SelectedValue = "0";
                        }
                        else 
                        {
                            ddlLstatus.SelectedValue = "1";
                        }


                        
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtLName.Focus();


                    }
                    else
                    {
                        txtcode.Focus();
                        txtcode.Text = string.Empty;
                        txtLName.Text = string.Empty;
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
                A2ZEMPLEAVETYPEDTO objDTO = new A2ZEMPLEAVETYPEDTO();

                objDTO.LeaveCode = Converter.GetInteger(txtcode.Text);
                objDTO.LeaveName = Converter.GetString(txtLName.Text);
                objDTO.TotalDays = Converter.GetDecimal(txtTotalDays.Text);
                objDTO.EffectSalary = Converter.GetBoolean(ChkSalEffect.Checked);

                if (ddlLstatus.SelectedValue == "1")
                {
                    objDTO.Status = Converter.GetBoolean(true);
                }
                else
                {
                    objDTO.Status = Converter.GetBoolean(false);
                }
             

                int roweffect = A2ZEMPLEAVETYPEDTO.InsertInformation(objDTO);
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

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtTotalDays.Text = string.Empty;
            ChkSalEffect.Checked = false;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZEMPLEAVETYPEDTO objDTO = new A2ZEMPLEAVETYPEDTO();

                objDTO.LeaveCode = Converter.GetInteger(txtcode.Text);
                objDTO.LeaveName = Converter.GetString(txtLName.Text);
                objDTO.TotalDays = Converter.GetDecimal(txtTotalDays.Text);
                objDTO.EffectSalary = Converter.GetBoolean(ChkSalEffect.Checked);

                if (ddlLstatus.SelectedValue == "1")
                {
                    objDTO.Status = Converter.GetBoolean(true);
                }
                else
                {
                    objDTO.Status = Converter.GetBoolean(false);
                }
                

                int roweffect = A2ZEMPLEAVETYPEDTO.UpdateInformation(objDTO);
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

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        

        
    }
}