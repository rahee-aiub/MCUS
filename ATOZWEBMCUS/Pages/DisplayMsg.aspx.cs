using System;


namespace ATOZWEBMCUS.Pages
{
    public partial class DisplayMsg : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblOne.Text = Request.QueryString["txtOne"];
                    lblTwo.Text = Request.QueryString["txtTwo"];
                    lblThree.Text = Request.QueryString["txtThree"];
                    lblPreviousPage.Text = Request.QueryString["PreviousMenu"];
                }
            }
            catch (Exception ex)
            {
                lblThree.Text = ex.ToString();
            }
        }
    
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblPreviousPage.Text);
        }
    }
}
