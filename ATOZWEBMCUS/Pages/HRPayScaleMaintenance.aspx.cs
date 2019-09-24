using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HumanResource;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRPayScaleMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlBase.Focus();
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;

                lblConsulted.Visible = false;
                txtconsulted.Visible = false;
            }
        }

        protected void gvDetail()
        {
            try
            {
                string sqlquery3 = "SELECT Base,Payscale,StartBasic,Bar1,label1,End1Basic,Bar2,label2,End2Basic,Consolidated FROM A2ZPAYSCALE";
                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetail Problem');</script>");

                //throw ex;
            }
        }

        protected void txtNolabel1_TextChanged(object sender, EventArgs e)
        {
            double startbasic = Converter.GetDouble(txtStartBasic.Text);
            double bar1 = Converter.GetDouble(txtBar1.Text);
            int label1 = Converter.GetInteger(txtNolabel1.Text);
            double label = (label1-1);
            double end1basic = (startbasic + (bar1 * label));
            txtEndBasic1.Text = Converter.GetString(string.Format("{0:0,0.00}", end1basic));
            txtBar2.Focus();
        }

        protected void txtNolabel2_TextChanged(object sender, EventArgs e)
        {
            double Endbasic1 = Converter.GetDouble(txtEndBasic1.Text);
            double bar2 = Converter.GetDouble(txtBar2.Text);
            int label2 = Converter.GetInteger(txtNolabel2.Text);
            double label = (label2 - 1);
            double end2basic = (Endbasic1 + (bar2 * label));
            txtEndBasic2.Text = Converter.GetString(string.Format("{0:0,0.00}", end2basic));
        }
        private void Empty()
        {
            txtStartBasic.Text = string.Empty;
            txtBar1.Text = string.Empty;
            txtBar2.Text = string.Empty;
            txtNolabel1.Text = string.Empty;
            txtNolabel2.Text = string.Empty;
            txtEndBasic1.Text = string.Empty;
            txtEndBasic2.Text = string.Empty;
            lblShow.Visible = false;
        }
        protected void clearinfo()
        {
            ddlBase.SelectedIndex = 0;
            txtPayscale.Text = string.Empty;
            txtStartBasic.Text = string.Empty;
            txtBar1.Text = string.Empty;
            txtBar2.Text = string.Empty;
            txtNolabel1.Text = string.Empty;
            txtNolabel2.Text = string.Empty;
            txtEndBasic1.Text = string.Empty;
            txtEndBasic2.Text = string.Empty;
            txtconsulted.Text = string.Empty;

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "INSERT INTO A2ZPAYSCALE(Base,Payscale,StartBasic,Bar1,label1,End1Basic,Bar2,label2,End2Basic,Consolidated)VALUES('" + ddlBase.SelectedValue + "','" + txtPayscale.Text + "','" + txtStartBasic.Text + "','" + txtBar1.Text + "','" + txtNolabel1.Text + "','" + txtEndBasic1.Text + "','" + txtBar2.Text + "','" + txtNolabel2.Text + "','" + txtEndBasic2.Text + "','" + txtconsulted.Text + "')";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZHRMCUS"));
                if (result > 0)
                {
                    gvDetail();
                    clearinfo();
                    ddlBase.Focus();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE A2ZPAYSCALE SET StartBasic='" + txtStartBasic.Text + "', Bar1='" + txtBar1.Text + "', label1='" + txtNolabel1.Text + "', End1Basic='" + txtEndBasic1.Text + "', Bar2='" + txtBar2.Text + "', label2='" + txtNolabel2.Text + "', End2Basic='" + txtEndBasic2.Text + "', Consolidated='" + txtconsulted.Text + "' Where Base='" + ddlBase.SelectedValue + "' And PayScale='" + txtPayscale.Text + "' ";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(query, "A2ZHRMCUS"));
                if (result > 0)
                {
                    gvDetail();
                    clearinfo();
                    ddlBase.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string delqry = "DELETE FROM A2ZPAYSCALE Where Base='" + ddlBase.SelectedValue + "' And PayScale='" + txtPayscale.Text + "'";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZHRMCUS"));
                if (result > 0)
                {
                    gvDetail();
                    clearinfo();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnDelete_Click Problem');</script>");

                //throw ex;
            }
        }

        protected void txtPayscale_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qry1 = "Select Base,Payscale,StartBasic,Bar1,label1,End1Basic,Bar2,label2,End2Basic,Consolidated FROM A2ZPAYSCALE Where Base='" + ddlBase.SelectedValue + "' And PayScale='" + txtPayscale.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                if (dt1.Rows.Count > 0)
                {
                    txtStartBasic.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["StartBasic"]));
                    string stbasic = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["StartBasic"]));
                    txtBar1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar1"]));
                    string bar1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar1"]));
                    txtNolabel1.Text = Converter.GetString(dt1.Rows[0]["label1"]);
                    txtEndBasic1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End1Basic"]));
                    string Endbasic1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End1Basic"]));
                    txtBar2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar2"]));
                    string bar2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar2"]));
                    txtNolabel2.Text = Converter.GetString(dt1.Rows[0]["label2"]);
                    txtEndBasic2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End2Basic"]));
                    txtconsulted.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Consolidated"]));
                    string Endbasic2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End2Basic"]));
                    lblShow.Text = stbasic + "-" + bar1 + "*" + txtNolabel1.Text + "-" + Endbasic1 + "-" + bar2 + "*" + txtNolabel2.Text + "-" + Endbasic2;
                    lblShow.Visible = true;
                    BtnUpdate.Visible = true;
                    BtnDelete.Visible = true;
                    BtnSubmit.Visible = false;
                    gvDetail();
                }
                else
                {
                    Empty();
                    txtStartBasic.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtPayscale_TextChanged Problem');</script>");

                //throw ex;
            }

        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetailInfo_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDetailInfo1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }



        double StartBasic = 0;
        double Bar1 = 0;
        int Label1 = 0;
        double End1Basic = 0;
        double Bar2 = 0;
        int Label2 = 0;
        double End2Basic = 0;

        double Basic = 0;
        double Bar = 0;
        double B = 0;
        int Label = 1;
        int lbl = 0;




        double sub = 0;
        int i = 0;
        public int no = 0;


        protected void BtnView_Click(object sender, EventArgs e)
        {

            try
            {
                if (ddlBase.SelectedValue == "-Select-")
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Base');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Base');", true);
                    return;
                }

                if (txtPayscale.Text == string.Empty)
                {
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Please Input Pay Scale');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Pay Scale');", true);
                    return;
                }

                string sqlquery3 = "Truncate table dbo.WFA2ZPAYSCALE ";
                int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZHRMCUS"));

                string qry1 = "SELECT Id,StartBasic,Bar1,Label1,End1Basic,Bar2,Label2,End2Basic FROM A2ZPAYSCALE WHERE Base='" + ddlBase.SelectedValue + "' AND Payscale='" + txtPayscale.Text + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                if (dt1.Rows.Count > 0)
                {
                    StartBasic = Converter.GetDouble(dt1.Rows[0]["StartBasic"]);
                    Basic = Converter.GetDouble(dt1.Rows[0]["StartBasic"]);

                    Bar1 = Converter.GetDouble(dt1.Rows[0]["Bar1"]);
                    Label1 = Converter.GetInteger(dt1.Rows[0]["Label1"]);
                    End1Basic = Converter.GetDouble(dt1.Rows[0]["End1Basic"]);
                    Bar2 = Converter.GetDouble(dt1.Rows[0]["Bar2"]);
                    Label2 = Converter.GetInteger(dt1.Rows[0]["Label2"]);
                    End2Basic = Converter.GetDouble(dt1.Rows[0]["End2Basic"]);

                    no = Convert.ToInt32(Label1);
                    lbl = Label1;
                    no = (no + 1);
                    Bar = Converter.GetDouble(dt1.Rows[0]["Bar1"]);

                    for (i = 1; i <= no; i++)
                    {
                        if (i != 1)
                        {
                            Basic = (Basic + Bar);
                            B = Bar;
                        }
                        else
                        {
                            B = 0;
                        }

                        Basic = Convert.ToDouble(string.Format("{0:0,0.00}", Math.Round(Basic)));


                        int rowEffect = 0;
                        string strQuery = @"INSERT into WFA2ZPAYSCALE(Base,Payscale,Paylabel,Bar,Basic)values('" + ddlBase.SelectedValue + "','" + txtPayscale.Text + "','" + Label + "','" + B + "','" + Basic + "')";
                        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                        Label = (Label + 1);


                        if (Label > lbl)
                        {
                            lbl = (Label1 + Label2);
                            no = (lbl - 1);
                            Bar = Bar2;
                        }

                    }
                }
                gv1Detail();
                gvDetailInfo.Visible = false;

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void gv1Detail()
        {
            try
            {
                string sqlquery4 = "SELECT Base,Payscale,PayLabel,Bar,Basic FROM WFA2ZPAYSCALE";
                gvDetailInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo1, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gv1Detail Problem');</script>");
                //throw ex;
            }
        }











    }
}