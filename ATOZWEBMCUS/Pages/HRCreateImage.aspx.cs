using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRCreateImage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZIMAGEMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.ibtnUpload);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.BtnUpdate);
            
            if (!IsPostBack)
            {
                txtMemNo.Focus();
                Memdropdown();
                BtnUpdate.Visible = false;
            }

        }

        //private void CUdropdown()
        //{

        //    string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9'";
        //    ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
        //}
        private void Memdropdown()
        {

            string sqlquery = "SELECT MemNo,MemName from A2ZMEMBER WHERE CuType='0' and CuNo='0'";
            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlMemNo, "A2ZCSMCUS");
        }
        protected void ibtnUpload_Click(object sender, ImageClickEventArgs e)
        {


            if (FileUpload1.HasFile)
            {
                if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                {

                    SqlCommand cmd = new SqlCommand("insertimage", con);
                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                    cmd.Parameters.AddWithValue("@CuType", 0);
                    cmd.Parameters.AddWithValue("@CuNo", 0);
                    cmd.Parameters.AddWithValue("@MemNo", txtMemNo.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        string filename = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                        ImgPicture.ImageUrl = "~/Profile Pic/" + filename;
                        //txtCuNo.Text = string.Empty;
                        //txtMemNo.Text = string.Empty;
                        //ddlCreditUNo.SelectedValue = "-Select-";
                        //ddlMemNo.SelectedValue = "-Select-";
                        txtMemNo.Focus();

                    }

                }

            }
        }

        private void CheckCU()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('This Staff already upload image..');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Staff already upload image..');", true);
            return;

        }

        protected bool IsImageFile(HttpPostedFile httpPostedFile)
        {
            bool isImage = false;
            string fullPath = Server.MapPath("~/Profile Pic/" + FileUpload1.FileName);
            FileUpload1.SaveAs(fullPath);
            ImgPicture.ImageUrl = "~/Profile Pic/@" + FileUpload1.FileName;
            System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer = br.ReadByte();
            fileclass = buffer.ToString();
            buffer = br.ReadByte();
            fileclass += buffer.ToString();
            br.Close();
            fs.Close();

            // only allow images    jpg       gif     bmp     png      
            String[] fileType = { "255216", "7173", "6677", "13780" };
            for (int i = 0; i < fileType.Length; i++)
            {
                if (fileclass == fileType[i])
                {
                    isImage = true;
                    break;
                }
            }
            return isImage;
        }

        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImgPicture.ImageUrl = "~/Images/index.jpg";
        }

    
        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            if (txtMemNo.Text != string.Empty)
            {
                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                Int16 CuType = Converter.GetSmallInteger(0);
                int CreditNumber = Converter.GetInteger(0);
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                    string qry = "SELECT Image,MemNo FROM uploadImg WHERE CuType='0' and CuNo='0' and MemNo = '" + ddlMemNo.SelectedValue + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZIMAGEMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CheckCU();
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='0' and CuNo='0' and MemNo = '" + ddlMemNo.SelectedValue + "'", con))
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] picData = reader["Image"] as byte[] ?? null;

                                if (picData != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(picData))
                                    {
                                        string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                                        ImgPicture.ImageUrl = "data:image/png;base64," + base64String;


                                    }
                                }
                            }
                        }

                        ibtnUpload.Visible = false;
                        BtnUpdate.Visible = true;
                        return;
                    }
                    else 
                    {
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;
                        ImgPicture.ImageUrl = "~/Images/index.jpg";
                        return;
                    }
                }
                else
                {
                   
                    ddlMemNo.SelectedValue = "-Select-";
                    txtMemNo.Text = string.Empty;
                    ImgPicture.ImageUrl = "~/Images/index.jpg";
                    ibtnUpload.Visible = true;
                    BtnUpdate.Visible = false;
                }
            }
            else
            {
                ddlMemNo.SelectedValue = "-Select-";
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

                Int16 CuType = Converter.GetSmallInteger(0);
                int CreditNumber = Converter.GetInteger(0);
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType, CreditNumber, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    txtMemNo.Text = Converter.GetString(ddlMemNo.SelectedValue);
                    string qry = "SELECT Image,MemNo FROM uploadImg WHERE CuType='0' and CuNo='0' and MemNo = '" + txtMemNo.Text + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZIMAGEMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        CheckCU();
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='0' and CuNo='0' and MemNo = '" + txtMemNo.Text + "'", con))
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] picData = reader["Image"] as byte[] ?? null;

                                if (picData != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(picData))
                                    {
                                        string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                                        ImgPicture.ImageUrl = "data:image/png;base64," + base64String;
                                    }
                                }
                            }
                        }
                        ibtnUpload.Visible = false;
                        BtnUpdate.Visible = true;
                        return;
                    }
                    else
                    {
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;
                        ImgPicture.ImageUrl = "~/Images/index.jpg";
                        return;
                    }
                }
                else
                {
                   
                    ddlMemNo.SelectedValue = "-Select-";
                    txtMemNo.Text = string.Empty;
                    ImgPicture.ImageUrl = "~/Images/index.jpg";
                    ibtnUpload.Visible = true;
                    BtnUpdate.Visible = false;
                }
            }
            else
            {
                txtMemNo.Text = string.Empty;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                {

                    SqlCommand cmd = new SqlCommand("Updateimage", con);
                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                    cmd.Parameters.AddWithValue("@CuType", 0);
                    cmd.Parameters.AddWithValue("@CuNo", 0);
                    cmd.Parameters.AddWithValue("@MemNo", txtMemNo.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        string filename = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/Profile Pic") + filename);
                        ImgPicture.ImageUrl = "~/Profile Pic" + filename;
                        //txtCuNo.Text = string.Empty;
                        //txtMemNo.Text = string.Empty;
                        //ddlCreditUNo.SelectedValue = "-Select-";
                        //ddlMemNo.SelectedValue = "-Select-";
                        txtMemNo.Focus();
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;

                    }

                }

            }
        }
    }
}