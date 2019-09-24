
using System;
using System.Data;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.MasterPages
{
    public partial class CustomerServices : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblCompanyName.Text = p.PrmUnitName;


                    //    GetMenuData();

                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());


                    string Rdate = (string)Session["date"];

                    if (Rdate == null || Rdate == "")
                    {
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                    }
                    else
                    {
                        lblProcessDate.Text = Rdate;
                    }



                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 10)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Input";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 20)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Checked and Verify";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 30)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Approved";
                    //}
                }
                else
                {
                    //Response.Cache.SetNoStore();
                    //Response.Cache.AppendCacheExtension("no-cache");
                    //Response.Expires = 0;

                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
