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

namespace ATOZWEBMCUS.Pages
{
    public partial class HRDeductionCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT Code,Description from A2ZDEDUCTION";
            ddlDeduction = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDeduction, "A2ZHRMCUS");
        }

       
        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlDeduction.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtcode.Text);
                    A2ZDEDUCTIONDTO getDTO = (A2ZDEDUCTIONDTO.GetInformation(MainCode));

                    if (getDTO.Code > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Code);
                        txtDescription.Text = Converter.GetString(getDTO.Description);
                        ddlStatus.SelectedValue = Converter.GetString(getDTO.Status);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlDeduction.SelectedValue = Converter.GetString(getDTO.Code);
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

        protected void ddlDeduction_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlDeduction.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlDeduction.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlDeduction.SelectedValue);
                    A2ZDEDUCTIONDTO getDTO = (A2ZDEDUCTIONDTO.GetInformation(MainCode));
                    if (getDTO.Code > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Code);
                        txtDescription.Text = Converter.GetString(getDTO.Description);
                        ddlStatus.SelectedValue = Converter.GetString(getDTO.Status);
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


        

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZDEDUCTIONDTO objDTO = new A2ZDEDUCTIONDTO();

                objDTO.Code = Converter.GetInteger(txtcode.Text);
                objDTO.Description = Converter.GetString(txtDescription.Text);

                if (ddlStatus.SelectedValue == "1")
                {
                    objDTO.Status = Convert.ToBoolean("True");
                }
                if (ddlStatus.SelectedValue == "2")
                {
                    objDTO.Status = Convert.ToBoolean("False");
                }


                int roweffect = A2ZDEDUCTIONDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {

                    
                    
                    string qry = "SELECT EmpCode FROM A2ZEMPLOYEE";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
                    int totrec = dt.Rows.Count;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var EmpCode = dr["EmpCode"].ToString();

                            string strQuery = "INSERT INTO A2ZEMPWISEDEDCODE(EmpCode,Code,Description,Status) VALUES ('" + EmpCode + "','" + txtcode.Text + "','" + txtDescription.Text + "','" + "True" + "')";
                            int rowwEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                        }
                    }     
                    txtcode.Focus();
                    clearinfo();
                    dropdown();
                    gvDetail();
                    ddlStatus.SelectedValue = "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZDEDUCTIONDTO UpDTO = new A2ZDEDUCTIONDTO();
            UpDTO.Code = Converter.GetInteger(txtcode.Text);
            UpDTO.Description = Converter.GetString(txtDescription.Text);

            if (ddlStatus.SelectedValue == "1")
            {
                UpDTO.Status = Convert.ToBoolean("True");

            }
            if (ddlStatus.SelectedValue == "2")
            {
                UpDTO.Status = Convert.ToBoolean("False");
            }
           
            int roweffect = A2ZDEDUCTIONDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                string qry = "SELECT EmpCode FROM A2ZEMPLOYEE";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHRMCUS");
                int totrec = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var EmpCode = dr["EmpCode"].ToString();


                        string strQuery = "UPDATE A2ZEMPWISEDEDCODE SET  Description = '" + txtDescription.Text + "' WHERE EmpCode = '" + EmpCode + "' AND Code = '" + txtcode.Text + "' ";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));
                    }
                }
                
                
                dropdown();
                clearinfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();
                ddlStatus.SelectedValue = "1";

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Code,Description,Status FROM A2ZDEDUCTION";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
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