using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmpStatusChangeMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpNo.Focus();
                ddlExsitStatus.Enabled = false;
                

            }
        }


        protected void gvDetail()
        {
            string sqlquery4 = "SELECT EmpCode,CreateDate,EffectDate,StatusDesc FROM A2ZHRSTATUSHST WHERE EmpCode= '" + txtEmpNo.Text + "'";

            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo, "A2ZHRMCUS");
        }
       
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

       
        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            int EmpID = Converter.GetInteger(txtEmpNo.Text);
            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
            if (getDTO.EmployeeID > 0)
            {
                lblName.Text = Converter.GetString(getDTO.EmployeeName);
                CtrlDesignation.Text = Converter.GetString(getDTO.EmpDesignation);
                CtrlGrade.Text = Converter.GetString(getDTO.EmpGrade);
                ddlExsitStatus.SelectedValue = Converter.GetString(getDTO.Status);
                if(getDTO.StatusDate==DateTime.MinValue)
                {
                    txtExistDate.Text = string.Empty;
                }
                else
                {
                    DateTime dt = Converter.GetDateTime(getDTO.StatusDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtExistDate.Text = date;
                }

                Int16 Desig = Converter.GetSmallInteger(CtrlDesignation.Text);
                A2ZDESIGNATIONDTO get1DTO = (A2ZDESIGNATIONDTO.GetInformation(Desig));
                if (get1DTO.DesignationCode > 0)
                {
                    lblDesign.Text = Converter.GetString(get1DTO.DesignationDescription);
                }

                Int16 Grade = Converter.GetSmallInteger(CtrlGrade.Text);
                A2ZGRADEDTO get2DTO = (A2ZGRADEDTO.GetGradeInformation(Grade));
                if (get2DTO.ID > 0)
                {
                    lblGrade.Text = Converter.GetString(get2DTO.GradeDesc);
                }
                
  
            }
        }

        protected void clearinfo()
        {
            txtEmpStatDate.Text = string.Empty;
            txtExistDate.Text = string.Empty;
            ddlExsitStatus.SelectedIndex = 0;
            ddlNewStatus.SelectedIndex = 0;
            lblName.Text = string.Empty;
            lblDesign.Text = string.Empty;
            lblGrade.Text = string.Empty;
            txtEmpNo.Text = string.Empty;
        }

        private void InvalidNewStatusMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select New Status');", true);
            return;
        }

        private void InvalidNewStatDateMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select New Status Date');", true);
            return;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlNewStatus.SelectedValue == "0")
            {
                InvalidNewStatusMSG();
                return;
            }

            if (txtEmpStatDate.Text == string.Empty)
            {
                InvalidNewStatDateMSG();
                return;
            }
            
            
            A2ZEMPLOYEEDTO UpDTO = new A2ZEMPLOYEEDTO();

            UpDTO.EmployeeID = Converter.GetInteger(txtEmpNo.Text);
            UpDTO.Status = Converter.GetSmallInteger(ddlNewStatus.SelectedValue);
            UpDTO.StatusDesc = Converter.GetString(ddlNewStatus.SelectedItem.Text);
            if (txtEmpStatDate.Text != string.Empty)
            {
                DateTime statusdate = DateTime.ParseExact(txtEmpStatDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpDTO.StatusDate = statusdate;

            }
            else
            {
                string CheckstatusDtNull = "";
                UpDTO.StatusNullDate = CheckstatusDtNull;

            }

            int roweffect = A2ZEMPLOYEEDTO.UpdateEmpStatus(UpDTO);
            if (roweffect > 0)
            {
                txtEmpNo.Focus();
                clearinfo();

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

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

           

    }
}