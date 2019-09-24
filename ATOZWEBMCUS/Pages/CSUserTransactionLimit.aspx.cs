using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSUserTransactionLimit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtIdsNo.Focus();
                BtnUpdate.Visible = false;
                IdsDropdown();
            }
        }



        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZCSMCUS");
        }

        private void clearinfo()
        {

            txtLIdsCashCredit.Text = string.Empty;
            txtLIdsCashDebit.Text = string.Empty;
            txtLIdsTrfCredit.Text = string.Empty;
            txtLIdsTrfDebit.Text = string.Empty;
            //ddlIdsNo.SelectedValue = "-Select-";
        }
        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtIdsNo.Text != string.Empty)
                {
                    // DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZSYSIDS ", "");
                    int idno = Converter.GetInteger(txtIdsNo.Text);
                    A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
                    dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZCSMCUS");
                    if (dto.IdsNo > 0)
                    {
                        ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);
                        txtLIdsCashCredit.Focus();
                    }
                    else
                    {
                        IDsNotFoundMsg();
                        ddlIdsNo.SelectedValue = "-Select-";
                        txtIdsNo.Text = string.Empty;
                        txtIdsNo.Focus();
                    }


                    A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(idno));
                    if (getDTO.IdsNo > 0)
                    {
                        txtIdsNo.Text = Converter.GetString(getDTO.IdsNo);
                        txtLIdsCashCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsCashCredit));
                        txtLIdsCashDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsCashDebit));
                        txtLIdsTrfCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsTrfCredit));
                        txtLIdsTrfDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsTrfDebit));

                        ddlIdsNo.SelectedValue = Converter.GetString(getDTO.IdsNo);
                        txtLIdsCashCredit.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                    }
                    else
                    {
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;

                        clearinfo();
                    }



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdsNo.SelectedValue == "-Select-")
                {
                    txtIdsNo.Text = string.Empty;
                    txtIdsNo.Focus();
                    clearinfo();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    return;
                }

                if (ddlIdsNo.SelectedValue != "-Select-")
                {
                    int idno = Converter.GetInteger(ddlIdsNo.SelectedValue);
                    A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
                    dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZCSMCUS");
                    if (dto.IdsNo > 0)
                    {
                        txtIdsNo.Text = Converter.GetString(dto.IdsNo);
                        txtLIdsCashCredit.Focus();
                    }



                    A2ZTRNLIMITDTO getDTO = (A2ZTRNLIMITDTO.GetInformation(idno));
                    if (getDTO.IdsNo > 0)
                    {
                        txtIdsNo.Text = Converter.GetString(getDTO.IdsNo);
                        txtLIdsCashCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsCashCredit));
                        txtLIdsCashDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsCashDebit));
                        txtLIdsTrfCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsTrfCredit));
                        txtLIdsTrfDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.LIdsTrfDebit));

                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;

                    }
                    else
                    {
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        clearinfo();
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
            A2ZTRNLIMITDTO objDTO = new A2ZTRNLIMITDTO();
            objDTO.IdsNo = Converter.GetInteger(txtIdsNo.Text);
            objDTO.LIdsCashCredit = Converter.GetDouble(txtLIdsCashCredit.Text);
            objDTO.LIdsCashDebit = Converter.GetDouble(txtLIdsCashDebit.Text);
            objDTO.LIdsTrfCredit = Converter.GetDouble(txtLIdsTrfCredit.Text);
            objDTO.LIdsTrfDebit = Converter.GetDouble(txtLIdsTrfDebit.Text);

            int roweffect = A2ZTRNLIMITDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                txtIdsNo.Text = string.Empty;
                clearinfo();
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Focus();
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZTRNLIMITDTO objDTO = new A2ZTRNLIMITDTO();
            objDTO.IdsNo = Converter.GetInteger(txtIdsNo.Text);
            objDTO.LIdsCashCredit = Converter.GetDouble(txtLIdsCashCredit.Text);
            objDTO.LIdsCashDebit = Converter.GetDouble(txtLIdsCashDebit.Text);
            objDTO.LIdsTrfCredit = Converter.GetDouble(txtLIdsTrfCredit.Text);
            objDTO.LIdsTrfDebit = Converter.GetDouble(txtLIdsTrfDebit.Text);


            int roweffect = A2ZTRNLIMITDTO.UpdateInformation(objDTO);
            if (roweffect > 0)
            {
                txtIdsNo.Text = string.Empty;
                clearinfo();
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Focus();
            }
        }

        protected void BtnAll_Click(object sender, EventArgs e)
        {
            string delqry = "DELETE FROM A2ZTRNLIMIT";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            //if (row1Effect > 0)
            //{

            string qry = "SELECT IdsNo,IdsLevel FROM A2ZSYSIDS";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var ids = dr["IdsNo"].ToString();
                    var level = dr["IdsLevel"].ToString();

                    txtIdsNo.Text = ids;
                    txtLIdsCashCredit.Text = "99999999999.99";
                    txtLIdsCashDebit.Text = "99999999999.99";
                    txtLIdsTrfCredit.Text = "99999999999.99";
                    txtLIdsTrfDebit.Text = "99999999999.99";

                    A2ZTRNLIMITDTO objDTO = new A2ZTRNLIMITDTO();
                    objDTO.IdsNo = Converter.GetInteger(txtIdsNo.Text);
                    objDTO.LIdsCashCredit = Converter.GetDouble(txtLIdsCashCredit.Text);
                    objDTO.LIdsCashDebit = Converter.GetDouble(txtLIdsCashDebit.Text);
                    objDTO.LIdsTrfCredit = Converter.GetDouble(txtLIdsTrfCredit.Text);
                    objDTO.LIdsTrfDebit = Converter.GetDouble(txtLIdsTrfDebit.Text);

                    int roweffect = A2ZTRNLIMITDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                    }

                }

                DoneMsg();
                
            }
        }

        private void IDsNotFoundMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Ids Does not exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }

        private void DoneMsg()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Ids Does not exist');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data saved successfully');", true);
            return;

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtLIdsCashCredit_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLIdsCashCredit.Text);
            txtLIdsCashCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtLIdsCashDebit.Focus();
        }

        protected void txtLIdsCashDebit_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLIdsCashDebit.Text);
            txtLIdsCashDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtLIdsTrfCredit.Focus();
        }

        protected void txtLIdsTrfCredit_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLIdsTrfCredit.Text);
            txtLIdsTrfCredit.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtLIdsTrfDebit.Focus();
        }

        protected void txtLIdsTrfDebit_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtLIdsTrfDebit.Text);
            txtLIdsTrfDebit.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            BtnSubmit.Focus();
        }
    }
}