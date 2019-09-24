using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSViewGetImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string cuType = (string)Session["CuType"];
                lblhideCutype.Text = cuType;
                string cuno = (string)Session["CuNo"];
                lblhideCuNo.Text = cuno;
                string memno = (string)Session["MemNo"];
                lblhideMemNo.Text = memno;


                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZIMAGEMCUS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT CuType,CuNo,MemNo,Image FROM uploadImg WHERE CuType='" + lblhideCutype.Text + "' and CuNo='" + lblhideCuNo.Text + "' and MemNo='" + lblhideMemNo.Text + "'", conn))
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
                }
            }
        }
    }
}