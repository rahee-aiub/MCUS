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



namespace ATOZWEBMCUS.Pages
{
    public partial class SYSWeekHolidayMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlHolWeekDay1.Focus();  
                BtnUpdate.Visible = false;
                GetWeekHolidayInfo();
               
            }

        }

        public void GetWeekHolidayInfo()
        {
            A2ZWEEKHOLIDAYDTO getDTO = (A2ZWEEKHOLIDAYDTO.GetInformation());

            if (getDTO.Record > 0)
            {
                ddlHolWeekDay1.SelectedValue = Converter.GetString(getDTO.HolWeekDay1);
                ddlHolWeekDay2.SelectedValue = Converter.GetString(getDTO.HolWeekDay2);
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
                
            }
            else
            {
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZWEEKHOLIDAYDTO objDTO = new A2ZWEEKHOLIDAYDTO();

                objDTO.HolWeekDay1 = Converter.GetSmallInteger(ddlHolWeekDay1.SelectedValue);
                objDTO.HolWeekDay2 = Converter.GetSmallInteger(ddlHolWeekDay2.SelectedValue);
                objDTO.HolWeekDayName1 = Converter.GetString(ddlHolWeekDay1.SelectedItem);
                objDTO.HolWeekDayName2 = Converter.GetString(ddlHolWeekDay2.SelectedItem);
               
                int roweffect = A2ZWEEKHOLIDAYDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    Response.Redirect("A2ZERPModule.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZWEEKHOLIDAYDTO UpDTO = new A2ZWEEKHOLIDAYDTO();

            UpDTO.HolWeekDay1 = Converter.GetSmallInteger(ddlHolWeekDay1.SelectedValue);
            UpDTO.HolWeekDay2 = Converter.GetSmallInteger(ddlHolWeekDay2.SelectedValue);
            UpDTO.HolWeekDayName1 = Converter.GetString(ddlHolWeekDay1.SelectedItem);
            UpDTO.HolWeekDayName2 = Converter.GetString(ddlHolWeekDay2.SelectedItem);
            
            int roweffect = A2ZWEEKHOLIDAYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                Response.Redirect("A2ZERPModule.aspx");
                
            }
        }
        
        protected void BtnView_Click(object sender, EventArgs e)
        {
            
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        

    }
}
