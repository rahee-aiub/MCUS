using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using DataAccessLayer.Constants;
using ATOZWEBMCUS.WebSessionStore;
using System.Data;
using DataAccessLayer.BLL;
using System.Data.SqlClient;

using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSVerifyAccountEditMaintenance : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmsg1.Visible = false;
                lblmsg2.Visible = false;
                userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                //if (userPermission != 20)
                //{
                //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                //                       "&txtTwo=" + "You Don't Have Permission for Verify" +
                //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                //    Server.Transfer("Notify.aspx" + notifyMsg);
                //}
                //else
                {

                    string CheckQuery = "SELECT lTrim(str(dbo.A2ZACCOUNT.CuType)+lTrim(str(dbo.A2ZACCOUNT.CuNo))) As CuNo,dbo.A2ZACCOUNT.MemNo,dbo.A2ZMEMBER.MemName,dbo.A2ZACCOUNT.AccNo FROM A2ZACCOUNT INNER JOIN  dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo  Where AccStatus=91";
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                    if (dt.Rows.Count <= 0)
                    {
                        string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                           "&txtTwo=" + "No Record Found for Verify" +
                                           "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                        Server.Transfer("Notify.aspx" + notifyMsg);

                    }
                    gvVerify();
                }

            }
        }


        protected void gvVerify()
        {
            string sqlquery3 = "SELECT lTrim(str(dbo.A2ZACCOUNT.CuType)+lTrim(str(dbo.A2ZACCOUNT.CuNo))) As CuNo,dbo.A2ZACCOUNT.MemNo,dbo.A2ZMEMBER.MemName,dbo.A2ZACCOUNT.AccNo FROM A2ZACCOUNT INNER JOIN  dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo  Where AccStatus=91";
            gvAccInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvAccInfo, "A2ZCSMCUS");
        }

        

        protected void BtnVerify_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label accno = (Label)gvAccInfo.Rows[r.RowIndex].Cells[4].FindControl("lblaccno");


            A2ZACCOUNTDTO objDTO = new A2ZACCOUNTDTO();

            objDTO.AccNo = Converter.GetLong(accno.Text);
            objDTO.AccStatus = Converter.GetInteger(1);

            int roweffect = A2ZACCOUNTDTO.UpdateAccStatus(objDTO);
            if (roweffect > 0)
            {
                gvVerify();
                string CheckQuery = "SELECT lTrim(str(dbo.A2ZACCOUNT.CuType)+lTrim(str(dbo.A2ZACCOUNT.CuNo))) As CuNo,dbo.A2ZACCOUNT.MemNo,dbo.A2ZMEMBER.MemName,dbo.A2ZACCOUNT.AccNo FROM A2ZACCOUNT INNER JOIN  dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo  Where AccStatus=91";
             
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                if (dt.Rows.Count <= 0)
                {
                    DivGridViewCancle.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg2.Visible = true;

                }
            }




        }

        
        private void Successful()
        {
            

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Verify Successfully Completed');", true);

            return;

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}