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
    public partial class CSCreateImage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZIMAGEMCUS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                ScriptManager.GetCurrent(this).RegisterPostBackControl(this.ibtnUpload);
                ScriptManager.GetCurrent(this).RegisterPostBackControl(this.BtnUpdate);
                if (!IsPostBack)
                {

                    string Ctrflag = (string)Session["flag"];
                    string Nflag = (string)Session["NFlag"];
                    //lblFlag.Text = Ctrflag;
                    if (Ctrflag == "1" && Nflag == "1")
                    {
                        string CuNo = (string)Session["RCreditUNo"];
                        txtCuNo.Text = CuNo;
                        string MemNo = (string)Session["RMemNo"];
                        txtMemNo.Text = MemNo;
                        txtCuNo_TextChanged(this, EventArgs.Empty);
                        txtMemNo_TextChanged(this, EventArgs.Empty);
                        SessionRemove1();
                    }
                    else 
                    {
                        txtCuNo.Focus();

                        BtnUpdate.Visible = false;
                    }


                    
                    //string CNumber = (string)Session["SCuNumber"];
                    //string CType = (string)Session["SCuType"];
                    //string CNo = (string)Session["SCuNo"];
                    //string MNo = (string)Session["SMemNo"];

                    
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void SessionRemove1()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["NFlag"] = string.Empty;

        }
        protected void ibtnUpload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile)
                {
                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                    {

                        SqlCommand cmd = new SqlCommand("insertimage", con);
                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                        cmd.Parameters.AddWithValue("@CuType", lblhideCutype.Text);
                        cmd.Parameters.AddWithValue("@CuNo", lblhideCuNo.Text);
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
                            txtCuNo.Focus();

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ibtnUpload_Click Problem');</script>");
                //throw ex;
            }
        }

        private void CheckCU()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('This Credit Union already upload image..');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Credit Union already upload image..');", true);
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

        protected void txtCuNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCuNo.Text != string.Empty)
                {

                    
                    ibtnUpload.Visible = true;
                    BtnUpdate.Visible = false;
                    ImgPicture.ImageUrl = "~/Images/index.jpg";
                   

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
                        lblCuName.Text = Converter.GetString(getDTO.CreditUnionName);
                        
                        txtCuNo.Text = (c + "-" + d);
                        txtMemNo.Focus();
                        
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCuNo_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        
        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            try
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
                        lblMemName.Text = Converter.GetString(getDTO.MemberName);
                        string qry = "SELECT Image,MemNo FROM uploadImg WHERE CuType='" + lblhideCutype.Text + "' and CuNo='" + lblhideCuNo.Text + "' and MemNo='" + txtMemNo.Text + "'";
                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZIMAGEMCUS");
                        if (dt.Rows.Count > 0)
                        {
                            CheckCU();
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='" + lblhideCutype.Text + "' and CuNo='" + lblhideCuNo.Text + "' and MemNo='" + txtMemNo.Text + "'", con))
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
                        txtCuNo.Text = string.Empty;
                        txtCuNo.Focus();
                        txtMemNo.Text = string.Empty;
                        ImgPicture.ImageUrl = "~/Images/index.jpg";
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtMemNo_TextChanged Problem');</script>");
                //throw ex;
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
                    cmd.Parameters.AddWithValue("@CuType", lblhideCutype.Text);
                    cmd.Parameters.AddWithValue("@CuNo", lblhideCuNo.Text);
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
                        txtCuNo.Focus();
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;

                    }

                }

            }
        }

        protected void BtnHelp_Click(object sender, EventArgs e)
        {
            Session["NFlag"] = "1";
            Session["ExFlag"] = "6";

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
 "click", @"<script>window.open('CSGetDepositorNo.aspx','_blank');</script>", false);
        }
    }
}