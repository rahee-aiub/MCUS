using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSViewImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.ibtnView);
            if(!IsPostBack)
            {
                txtCuNo.Focus();
                CUdropdown();
            }
            
        }



        private void CUdropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
        }
        private void Memdropdown()
        {

            string sqlquery = "SELECT MemNo,MemName from A2ZMEMBER WHERE CuType='" + lblhideCutype.Text + "' and CuNo='" + lblhideCuNo.Text + "'";
            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMemNo, "A2ZCSMCUS");
        }
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZIMAGEMCUS")))
            {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='" + lblhideCutype.Text + "' and CuNo='" + lblhideCuNo.Text + "' and MemNo='" + txtMemNo.Text + "'", conn))
           using (SqlDataReader reader = cmd.ExecuteReader())
           {
           if (reader.Read())
           {
               byte[] picData = reader["Image"] as byte[] ?? null;

            if (picData!= null)
            {
                using (MemoryStream ms = new MemoryStream(picData))
                {
                    string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                    ImgPicture.ImageUrl = "data:image/png;base64," + base64String;
                    //txtCuNo.Text = string.Empty;
                    //txtMemNo.Text = string.Empty;
                    //ddlCreditUNo.SelectedValue = "-Select-";
                    //ddlMemNo.SelectedValue = "-Select-";
                    txtCuNo.Focus();
                  
                }
            }
        }
         }
         }
    }

        protected void txtCuNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCuNo.Text != string.Empty)
                {

                    string c = "";
                    int a = txtCuNo.Text.Length;

                    string b = txtCuNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblhideCuNo.Text = Converter.GetString(CNo);
                    lblhideCutype.Text = Converter.GetString(CuType);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        //string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "'";
                        //ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(lblhideCutype.Text + lblhideCuNo.Text);
                        txtCuNo.Text = (c + "-" + d);
                        Memdropdown();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {

            if (txtMemNo.Text != string.Empty)
            {
                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                Int16 CuType = Converter.GetSmallInteger(lblhideCutype.Text);
                int CreditNumber = Converter.GetInteger(lblhideCuNo.Text);
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                    txtMemNo.Focus();
                }
                else
                {
                    ddlMemNo.SelectedValue = "-Select-";
                }

            }
           
        }

        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlCreditUNo.SelectedValue != "-Select-")
                {
                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);
                    lblhideCuNo.Text = Converter.GetString(CNo);
                    lblhideCutype.Text = Converter.GetString(CuType);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        txtCuNo.Text = Converter.GetString(txtHidden.Text);
                        txtCuNo.Text = (c + "-" + d);
                        Memdropdown();
                        txtMemNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemNo.SelectedValue == "-Select-")
            {
                txtMemNo.Text = string.Empty;
                txtMemNo.Focus();
            }
            if (ddlMemNo.SelectedValue != "-Select-")
            {
                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                Int16 CuType = Converter.GetSmallInteger(lblhideCutype.Text);
                int CreditNumber = Converter.GetInteger(lblhideCuNo.Text);
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    //txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    txtMemNo.Text = Converter.GetString(ddlMemNo.SelectedValue);
                }
                else
                {
                    txtMemNo.Text = string.Empty;
                }
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}