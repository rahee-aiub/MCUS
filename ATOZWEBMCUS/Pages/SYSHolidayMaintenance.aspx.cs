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
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.CustomerServices;



namespace ATOZWEBMCUS.Pages
{
    public partial class SYSHolidayMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtHolDate.Focus();
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                dropdown();
            }

        }

        private void dropdown()
        {
            string sqlquery = "SELECT HolType,HolTypeDescription from A2ZHOLIDAYTYPE WHERE HolType > 1";
            ddlHolType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlHolType, "A2ZHKMCUS");
        }


        protected void txtHolDate_TextChanged(object sender, EventArgs e)
        {
            var dat = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime processDate = dat.ProcessDate;

            DateTime HolDate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            A2ZHOLIDAYDTO getDTO = (A2ZHOLIDAYDTO.GetInformation(HolDate));
            
            lblDayName.Text = HolDate.DayOfWeek.ToString();

            if (HolDate < processDate)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Previous Date not accepted!.');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtHolDate.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Previous Date Not Accepted');", true);
                return;
            }

            if (getDTO.HolType > 0)
            {
                //DateTime dt = Converter.GetDateTime(getDTO.HolDate);
                //string date = dt.ToString("dd/MM/yyyy");
                //txtHolDate.Text = date;
                ddlHolType.SelectedValue = Converter.GetString(getDTO.HolType);
                lblHolTypeDesc.Text = Converter.GetString(getDTO.HolTypeDesc);  
                txtHolNote.Text = Converter.GetString(getDTO.HolNote);
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
                BtnDelete.Visible = true;
            }
            else
            {
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                ddlHolType.SelectedIndex = 0;
                txtHolNote.Text = string.Empty;


            }
        }



    

       
        private void clearinfo()
        {
            txtHolDate.Text = string.Empty;
            ddlHolType.SelectedIndex = 0;
            txtHolNote.Text = string.Empty;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
              
                 if (ddlHolType.SelectedValue != "-Select-")
                 {
                     A2ZHOLIDAYDTO objDTO = new A2ZHOLIDAYDTO();
                     //DateTime opdate = Converter.GetDateTime(txtHolDate.Text);
                     DateTime opdate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                     objDTO.HolDate = opdate;
                     objDTO.HolType = Converter.GetInteger(ddlHolType.SelectedValue);
                     objDTO.HolTypeDesc = Converter.GetString(ddlHolType.SelectedItem.Text);
                     objDTO.HolNote = Converter.GetString(txtHolNote.Text);
                     objDTO.HolDayName = Converter.GetString(lblDayName.Text);

                     int roweffect = A2ZHOLIDAYDTO.InsertInformation(objDTO);
                     if (roweffect > 0)
                     {
                         clearinfo();
                         txtHolDate.Focus();
                         BtnDelete.Visible = false;
                         dropdown();
                         gvDetail();
                         lblDayName.Text = string.Empty;
                         ddlHolType.SelectedIndex = 0;
                     }

                 }    
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZHOLIDAYDTO UpDTO = new A2ZHOLIDAYDTO();

            DateTime opdate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            UpDTO.HolDate = opdate;
            UpDTO.HolType = Converter.GetInteger(ddlHolType.SelectedValue);
            UpDTO.HolTypeDesc = Converter.GetString(ddlHolType.SelectedItem.Text);
            UpDTO.HolNote = Converter.GetString(txtHolNote.Text);
            UpDTO.HolDayName = Converter.GetString(lblDayName.Text);

            int roweffect = A2ZHOLIDAYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                clearinfo();
                dropdown();
                
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                txtHolDate.Focus();
                gvDetail();

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT HolDate,HolType,HolTypeDesc,HolNote,HolDayName FROM A2ZHOLIDAY ORDER BY HolDate";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DateTime deldate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "DELETE FROM A2ZHOLIDAY Where HolDate='" + deldate + "' ";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZHKMCUS"));
            if(rowEffect>0)
            {
                gvDetail();
                clearinfo();
                txtHolDate.Focus();
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
                BtnDelete.Visible = false;
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Holiday Deleted');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Holiday Deleted');", true);
                return;
              
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

    }
}
