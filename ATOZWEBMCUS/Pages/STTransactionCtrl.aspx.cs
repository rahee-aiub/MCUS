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
using DataAccessLayer.DTO.CustomerServices;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;

namespace ATOZWEBMCUS.Pages
{
    public partial class STTransactionCtrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                GroupDropdown();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
        }

        private void GroupDropdown()
        {
            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }

        protected void SubGroupDropdown()
        {
            string sqquery = @"SELECT SubGrpCode,SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode='" + ddlGroup.SelectedValue + "' OR GrpCode='0'";
            ddlCategory = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlCategory, "A2ZSTMCUS");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex != 0)
            {
                SubGroupDropdown();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnHdrSelect_Click(object sender, EventArgs e)
        {
            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");

                lblSelectedId.Text = lblId.Text;

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT FuncNo,FuncDesc,GroupCode,SubGroupCode,PayType,TrnType,TrnMode,GLAccNoDr,GLAccNoCr,TrnRecDesc FROM A2ZSTTRNCTRL Where Id = " + lblId.Text + "", "A2ZSTMCUS");

                if (dt.Rows.Count > 0)
                {

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;

                    ddlFuncNo.SelectedValue = Converter.GetString(dt.Rows[0]["FuncNo"]);
                    ddlGroup.SelectedValue = Converter.GetString(dt.Rows[0]["GroupCode"]);
                    SubGroupDropdown();
                    ddlCategory.SelectedValue = Converter.GetString(dt.Rows[0]["SubGroupCode"]);
                    ddlPayType.SelectedValue = Converter.GetString(dt.Rows[0]["PayType"]);
                    ddlTrnType.SelectedValue = Converter.GetString(dt.Rows[0]["TrnType"]);
                    ddlTrnMode.SelectedValue = Converter.GetString(dt.Rows[0]["TrnMode"]);
                    txtTrnDebitCode.Text = Converter.GetString(dt.Rows[0]["GLAccNoDr"]);
                    txtTrnCreditCode.Text = Converter.GetString(dt.Rows[0]["GLAccNoCr"]);
                    txtTrnRecDesc.Text = Converter.GetString(dt.Rows[0]["TrnRecDesc"]);

                    if (txtTrnDebitCode.Text != string.Empty)
                    {
                        A2ZCGLMSTDTO dto = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnDebitCode.Text));
                        if (dto.GLAccDesc != null)
                        {
                            lblDebitCd.Text = Converter.GetString(dto.GLAccDesc);
                        }
                        
                    }

                    if (txtTrnCreditCode.Text != string.Empty)
                    {
                        A2ZCGLMSTDTO dto = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnCreditCode.Text));
                        if (dto.GLAccDesc != null)
                        {
                            lblCreditCd.Text = Converter.GetString(dto.GLAccDesc);
                        }                 
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlFuncNo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Function');", true);
                return;
            }

            if (ddlGroup.SelectedIndex == 0 && ddlFuncNo.SelectedValue != "61" && ddlFuncNo.SelectedValue != "62" && ddlFuncNo.SelectedValue == "63")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group');", true);
                return;
            }

            if (ddlCategory.SelectedIndex == 0 && ddlFuncNo.SelectedValue != "61" && ddlFuncNo.SelectedValue != "62" && ddlFuncNo.SelectedValue == "63")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Sub Group');", true);
                return;
            }

            if (ddlPayType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Pay Type');", true);
                return;
            }

            if (ddlTrnType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Transaction Type');", true);
                return;
            }

                  
           
            try
            {
                string statment = "INSERT INTO  A2ZSTTRNCTRL (FuncNo,FuncDesc,GroupCode,SubGroupCode,PayType,TrnType,TrnMode,GLAccNoDr,GLAccNoCr,TrnRecDesc)VALUES('" + ddlFuncNo.SelectedValue + "','" + ddlFuncNo.SelectedItem.Text + "','" + ddlGroup.SelectedValue + "','" + ddlCategory.SelectedValue + "','" + ddlPayType.SelectedValue + "','" + ddlTrnType.SelectedValue + "','" + ddlTrnMode.SelectedValue + "','" + txtTrnDebitCode.Text + "','" + txtTrnCreditCode.Text + "','" + txtTrnRecDesc.Text + "')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZSTMCUS"));

                if (rowEffect > 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,FuncNo,FuncDesc,GroupCode,SubGroupCode,PayType,TrnType,TrnMode,GLAccNoDr,GLAccNoCr,TrnRecDesc FROM A2ZSTTRNCTRL";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZSTMCUS");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlFuncNo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Function');", true);
                return;
            }

            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group');", true);
                return;
            }

            if (ddlCategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Sub Group');", true);
                return;
            }

            if (ddlPayType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Pay Type');", true);
                return;
            }

            if (ddlTrnType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Transaction Type');", true);
                return;
            }

           
            try
            {
                string statment = "UPDATE A2ZSTTRNCTRL SET FuncNo = '" + ddlFuncNo.SelectedValue + "',FuncDesc = '" + ddlFuncNo.SelectedItem.Text + "',GroupCode = '" + ddlGroup.SelectedValue + "',SubGroupCode = '" + ddlCategory.SelectedValue + "',PayType = '" + ddlPayType.SelectedValue + "',TrnType = '" + ddlTrnType.SelectedValue + "',TrnMode = '" + ddlTrnMode.SelectedValue + "',GLAccNoDr = '" + txtTrnDebitCode.Text + "',GLAccNoCr = '" + txtTrnCreditCode.Text + "',TrnRecDesc = '" + txtTrnRecDesc.Text + "' WHERE Id = '" + lblSelectedId.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZSTMCUS"));

                if (rowEffect > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Update Successfull');", true);
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void txtTrnDebitCode_TextChanged(object sender, EventArgs e)
        {
            if (txtTrnDebitCode.Text != string.Empty)
            {
                A2ZCGLMSTDTO dto = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnDebitCode.Text));
                if (dto.GLAccDesc != null)
                {
                    lblDebitCd.Text = Converter.GetString(dto.GLAccDesc);
                }
                else
                {
                    txtTrnDebitCode.Text = string.Empty;
                    lblDebitCd.Text = string.Empty;
                    txtTrnDebitCode.Focus();
                }
            }

        }

        protected void txtTrnCreditCode_TextChanged(object sender, EventArgs e)
        {
            if (txtTrnCreditCode.Text != string.Empty)
            {
                A2ZCGLMSTDTO dto = A2ZCGLMSTDTO.GetInformation(Converter.GetInteger(txtTrnCreditCode.Text));
                if (dto.GLAccDesc != null)
                {
                    lblCreditCd.Text = Converter.GetString(dto.GLAccDesc);
                }
                else
                {
                    txtTrnCreditCode.Text = string.Empty;
                    lblCreditCd.Text = string.Empty;
                    txtTrnCreditCode.Focus();
                }
            }
        }

        protected void ddlFuncNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFuncNo.SelectedValue == "61" || ddlFuncNo.SelectedValue == "62" || ddlFuncNo.SelectedValue == "63")
            {
                Label5.Visible = false;
                ddlGroup.Visible = false;
                Label1.Visible = false;
                ddlCategory.Visible = false;

                ddlGroup.SelectedIndex = 0;
                ddlCategory.SelectedIndex = 0;
            }
            else 
            {
                Label5.Visible = true;
                ddlGroup.Visible = true;
                Label1.Visible = true;
                ddlCategory.Visible = true;
            }
        }
    }
}
