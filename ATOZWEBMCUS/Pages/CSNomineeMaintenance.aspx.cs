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
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;
using ATOZWEBMCUS.WebSessionStore;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSNomineeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                txtName.Focus();
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                DivisionDropdown();
                DistrictInFo();
                UpzilaInfo();
                ThanaInfo();

                string cuType = (string)Session["CuType"];
                lblCuType.Text = cuType;
                string cuno = (string)Session["CrNo"];
                lblCuNo.Text = cuno;
                string memno = (string)Session["NewMemNo"];
                lblmemno.Text = memno;
                
                string acctype = (string)Session["AccType"];
                lblType.Text = acctype;

                string accno = (string)Session["AccNo"];
                lblAccNo.Text = accno;

                string PFlag = (string)Session["ProgFlag"];
                ProgFlag.Text = PFlag;

                //lblAccNo.Text = "0";

                HoldPerc.Text = "0";
                gvDetail();
                gvTotalPer();

            }
        }
        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZHKMCUS");

        }
        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' or DiviCode = '0'";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }
        private void UpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilaCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' or DistCode = '0'";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }

        private void ThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' and UpzilaCode='" + ddlUpzila.SelectedValue + "' or UpzilaCode = '0'";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }
        private void DistrictInFo()
        {

            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }

        private void UpzilaInfo()
        {
            string sqquery = @"SELECT UpzilaCode,UpzilaDescription FROM A2ZUPZILA";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }
        private void ThanaInfo()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT Id,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer from WFCSA2ZACCNOM WHERE AccType='" + lblType.Text + "' and AccNo='" + lblAccNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "' and MemNo='" + lblmemno.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "-Select-")
            {

                ddlDistrict.SelectedIndex = 0;
                return;
            }


            DistrictDropdown();

            try
            {


                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    else
                    {
                        ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clearInfo()
        {
            txtName.Text = string.Empty;
            txtAddressL1.Text = string.Empty;
            txtAddressL2.Text = string.Empty;
            txtAddressL3.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtRelation.Text = string.Empty;
            txtNomSharePer.Text = string.Empty;
            txtEmail.Text = string.Empty;

            ddlDivision.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlThana.SelectedIndex = 0;


            //if (ddlDivision.SelectedItem.Text != "-Select-")
            //{
            //    ddlDivision.SelectedIndex = 0;
            //}

            //if (ddlDistrict.SelectedItem.Text != "-Select-")
            //{
            //    ddlDistrict.SelectedIndex = 0;
            //}

            //if (ddlThana.SelectedItem.Text != "-Select-")
            //{
            //    ddlThana.SelectedIndex = 0;
            //}

        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedItem.Text == "-Select-")
            {
                return;
            }
            UpzilaDropdown();
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZDISTRICTDTO getDTO = new A2ZDISTRICTDTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    getDTO = (A2ZDISTRICTDTO.GetInformation(code, distcode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0)
                    {
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUpzila.SelectedItem.Text == "-Select-")
            {
                return;
            }
            ThanaDropdown();
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-")
                {
                    A2ZUPZILADTO getDTO = new A2ZUPZILADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int Upzilacode = Converter.GetInteger(ddlUpzila.SelectedValue);
                    getDTO = (A2ZUPZILADTO.GetInformation(code, distcode, Upzilacode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.UpzilaCode > 0)
                    {
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlThana.SelectedItem.Text == "-Select-")
            {
                return;
            }

            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-" && ddlThana.SelectedValue != "-Select-")
                {
                    A2ZTHANADTO getDTO = new A2ZTHANADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int thanacode = Converter.GetInteger(ddlThana.SelectedValue);
                    int Upzilacode = Converter.GetInteger(ddlUpzila.SelectedValue);
                    getDTO = (A2ZTHANADTO.GetInformation(code, distcode, Upzilacode, thanacode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.UpzilaCode > 0 && getDTO.ThanaCode > 0)
                    {

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtNomSharePer.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Share Purcentage' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Share Purcentage');", true);
                return;
            }




            //if (txtNo.Text == string.Empty)
            //{
            //    InvalidUpdate();
            //    txtNo.Focus();
            //    return;
            //}

            A2ZACCNOMINEEDTO objDTO = new A2ZACCNOMINEEDTO();
            objDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
            objDTO.CUnionNo = Converter.GetInteger(lblCuNo.Text);
            objDTO.MemberNo = Converter.GetInteger(lblmemno.Text);
            objDTO.AccType = Converter.GetSmallInteger(lblType.Text);
            objDTO.AccountNo = Converter.GetLong(lblAccNo.Text);
            // objDTO.NomineeNo = Converter.GetInteger(txtNo.Text);
            objDTO.NomineeName = Converter.GetString(txtName.Text);
            objDTO.NomAddress1 = Converter.GetString(txtAddressL1.Text);
            objDTO.NomAddress2 = Converter.GetString(txtAddressL2.Text);
            objDTO.NomAddress3 = Converter.GetString(txtAddressL3.Text);
            objDTO.NomTelephoneNo = Converter.GetString(txtTelNo.Text);
            objDTO.NomMobileNo = Converter.GetString(txtMobileNo.Text);
            objDTO.NomEmail = Converter.GetString(txtEmail.Text);
            objDTO.NomDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            objDTO.NomDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            objDTO.NomUpzila = Converter.GetInteger(ddlUpzila.SelectedValue);
            objDTO.NomThana = Converter.GetInteger(ddlThana.SelectedValue);
            objDTO.NomRelation = Converter.GetString(txtRelation.Text);
            objDTO.NomSharePercentage = Converter.GetInteger(txtNomSharePer.Text);
            objDTO.UserId = Converter.GetInteger(hdnID.Text);
            int roweffect = A2ZACCNOMINEEDTO.WFInsertInformation(objDTO);
            if (roweffect > 0)
            {
                clearInfo();
                //txtNo.Focus();
                //txtNo.Text = string.Empty;
                gvDetail();
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (txtNomSharePer.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please Input Share Purcentage' );";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Share Purcentage');", true);
                return;
            }


            A2ZACCNOMINEEDTO UpDTO = new A2ZACCNOMINEEDTO();
            // UpDTO.NomineeNo = Converter.GetInteger(txtNo.Text);
            UpDTO.ID = Converter.GetInteger(lblID.Text);
            UpDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
            UpDTO.CUnionNo = Converter.GetInteger(lblCuNo.Text);
            UpDTO.MemberNo = Converter.GetInteger(lblmemno.Text);
            UpDTO.AccType = Converter.GetSmallInteger(lblType.Text);
            UpDTO.AccountNo = Converter.GetLong(lblAccNo.Text);

            UpDTO.NomineeName = Converter.GetString(txtName.Text);
            UpDTO.NomAddress1 = Converter.GetString(txtAddressL1.Text);
            UpDTO.NomAddress2 = Converter.GetString(txtAddressL2.Text);
            UpDTO.NomAddress3 = Converter.GetString(txtAddressL3.Text);
            UpDTO.NomTelephoneNo = Converter.GetString(txtTelNo.Text);
            UpDTO.NomMobileNo = Converter.GetString(txtMobileNo.Text);
            UpDTO.NomEmail = Converter.GetString(txtEmail.Text);
            UpDTO.NomDivision = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.NomDistrict = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.NomUpzila = Converter.GetInteger(ddlUpzila.SelectedValue);
            UpDTO.NomThana = Converter.GetInteger(ddlThana.SelectedValue);
            UpDTO.NomRelation = Converter.GetString(txtRelation.Text);
            UpDTO.NomSharePercentage = Converter.GetInteger(txtNomSharePer.Text);
            UpDTO.UserId = Converter.GetInteger(hdnID.Text);
            int roweffect = A2ZACCNOMINEEDTO.WFUpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                gvDetail();
                clearInfo();
                //txtNo.Focus();
                //txtNo.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
            }
        }

        //protected void txtNo_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtNo.Text != string.Empty)
        //    {
        //        Int16 CuType = Converter.GetSmallInteger(lblCuType.Text);
        //        int CuNo = Converter.GetInteger(lblCuNo.Text);
        //        int MemNo = Converter.GetInteger(lblmemno.Text);
        //        Int16 AType = Converter.GetSmallInteger(lblType.Text);
        //        int ANo = Converter.GetInteger(lblAccNo.Text);

        //        int NomCode = Converter.GetInteger(txtNo.Text);
        //        A2ZACCNOMINEEDTO getDTO = new A2ZACCNOMINEEDTO();
        //        getDTO = (A2ZACCNOMINEEDTO.GetInformation(NomCode,AType,MemNo,ANo,CuType,CuNo));
        //        if (getDTO.Record > 0)
        //        {
        //            txtName.Text = Converter.GetString(getDTO.NomineeName);
        //            txtAddressL1.Text = Converter.GetString(getDTO.NomAddress1);
        //            txtAddressL2.Text = Converter.GetString(getDTO.NomAddress2);
        //            txtAddressL3.Text = Converter.GetString(getDTO.NomAddress3);
        //            txtTelNo.Text = Converter.GetString(getDTO.NomTelephoneNo);
        //            txtMobileNo.Text = Converter.GetString(getDTO.NomMobileNo);
        //            txtEmail.Text = Converter.GetString(getDTO.NomEmail);
        //            ddlDivision.SelectedValue = Converter.GetString(getDTO.NomDivision);
        //            ddlDistrict.SelectedValue = Converter.GetString(getDTO.NomDistrict);
        //            ddlThana.SelectedValue = Converter.GetString(getDTO.NomThana);
        //            txtRelation.Text = Converter.GetString(getDTO.NomRelation);
        //            txtNomSharePer.Text = Converter.GetString(getDTO.NomSharePercentage);
        //            BtnSubmit.Visible = false;
        //            BtnUpdate.Visible = true;
        //            txtName.Focus();
        //        }
        //        else
        //        {
        //            clearInfo();
        //            BtnSubmit.Visible = true;
        //            BtnUpdate.Visible = false;
        //            txtName.Focus();
        //        }

        //    }
        //}

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

            DivisionDropdown();
            DistrictInFo();
            UpzilaInfo();
            ThanaInfo();

            //foreach (GridViewRow r in gvDetailInfo.Rows)
            //{
            GridViewRow row = gvDetailInfo.SelectedRow;
            Label lblId = (Label)gvDetailInfo.Rows[row.RowIndex].Cells[0].FindControl("lblId");
            int ID = Converter.GetInteger(lblId.Text);
            lblID.Text = Converter.GetString(ID);

            A2ZACCNOMINEEDTO DTO = (A2ZACCNOMINEEDTO.WFGetInformation(ID));

            txtName.Text = Converter.GetString(DTO.NomineeName);

            txtAddressL1.Text = Converter.GetString(DTO.NomAddress1);
            txtAddressL2.Text = Converter.GetString(DTO.NomAddress2);
            txtAddressL3.Text = Converter.GetString(DTO.NomAddress3);
            txtTelNo.Text = Converter.GetString(DTO.NomTelephoneNo);
            txtMobileNo.Text = Converter.GetString(DTO.NomMobileNo);

            txtRelation.Text = Converter.GetString(DTO.NomRelation);
            txtNomSharePer.Text = Converter.GetString(DTO.NomSharePercentage);
            HoldPerc.Text = Converter.GetString(DTO.NomSharePercentage);


            txtEmail.Text = Converter.GetString(DTO.NomEmail);


            ddlDivision.SelectedValue = Converter.GetString(DTO.NomDivision);


            ddlDistrict.SelectedValue = Converter.GetString(DTO.NomDistrict);

            ddlUpzila.SelectedValue = Converter.GetString(DTO.NomUpzila);

            ddlThana.SelectedValue = Converter.GetString(DTO.NomThana);



            //txtName.Text = row.Cells[1].Text;
            //txtAddressL1.Text = row.Cells[2].Text;
            //txtAddressL2.Text = row.Cells[3].Text;
            //txtAddressL3.Text = row.Cells[4].Text;
            //txtTelNo.Text = row.Cells[5].Text;
            //txtMobileNo.Text = row.Cells[6].Text;

            //txtRelation.Text = row.Cells[7].Text;
            //txtNomSharePer.Text = row.Cells[8].Text;
            //HoldPerc.Text = row.Cells[8].Text;

            //Label lblNomEmail = (Label)gvDetailInfo.Rows[row.RowIndex].Cells[9].FindControl("lblNomEmail");
            //txtEmail.Text = lblNomEmail.Text;

            //Label lblNomDivi = (Label)gvDetailInfo.Rows[row.RowIndex].Cells[10].FindControl("lblNomDivi");
            //int divi = Converter.GetInteger(lblNomDivi.Text);
            //ddlDivision.SelectedValue = Converter.GetString(divi);

            //Label lblNomDist = (Label)gvDetailInfo.Rows[row.RowIndex].Cells[11].FindControl("lblNomDist");
            //int dist = Converter.GetInteger(lblNomDist.Text);
            //ddlDistrict.SelectedValue = Converter.GetString(dist);

            //Label lblNomThana = (Label)gvDetailInfo.Rows[row.RowIndex].Cells[12].FindControl("lblNomThana");
            //int thana = Converter.GetInteger(lblNomThana.Text);
            //ddlThana.SelectedValue = Converter.GetString(thana);



            BtnUpdate.Visible = true;
            BtnDelete.Visible = true;
            BtnSubmit.Visible = false;


        }
        protected void gvTotalPer()
        {
            string sqlquery3 = "SELECT Id,NomName,NomAdd1,NomAdd2,NomAdd3,NomTel,NomMobile,NomEmail,NomDivi,NomDist,NomUpzila,NomThana,NomRela,NomSharePer from WFCSA2ZACCNOM WHERE AccType='" + lblType.Text + "' and AccNo='" + lblAccNo.Text + "' and CuType='" + lblCuType.Text + "' and CuNo='" + lblCuNo.Text + "'and MemNo='" + lblmemno.Text + "' ";
            gvPerTotal = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvPerTotal, "A2ZCSMCUS");
        }
        protected void SumValue()
        {
            int sum = 0;

            for (int i = 0; i < gvPerTotal.Rows.Count; ++i)
            {

                sum += Convert.ToInt16(gvPerTotal.Rows[i].Cells[0].Text);

            }

            lblTotal.Text = Convert.ToString(sum);

        }





        protected void txtNomSharePer_TextChanged(object sender, EventArgs e)
        {

            gvTotalPer();
            SumValue();
            int a = Convert.ToInt32(lblTotal.Text);
            int b = Convert.ToInt32(txtNomSharePer.Text);
            int d = Convert.ToInt32(HoldPerc.Text);
            int c = a + b;
            if (c > 100)
            {



                txtNomSharePer.Text = string.Empty;
                txtNomSharePer.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Share Percentage should not over 100%');", true);
                return;




            }
        }

        protected void InvalidUpdate()
        {

            txtNomSharePer.Text = string.Empty;
            txtNomSharePer.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Share Percentage should not over 100%');", true);
            return;

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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string delqry = "DELETE FROM WFCSA2ZACCNOM WHERE Id='" + lblID.Text + "'";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
            if (rowEffect > 0)
            {
                gvDetail();
                clearInfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            
            Session["CtrlFlag"] = "1";


            if (ProgFlag.Text == "1")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.opener.location.href='CSAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }
            if (ProgFlag.Text == "2")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.opener.location.href='CSInstantAccountOpeningMaintenance.aspx'; self.close();</script>", false);
            }
            if (ProgFlag.Text == "3")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
           "click", @"<script>window.opener.location.href='CSAccountEditMaintenance.aspx'; self.close();</script>", false);
            }
        }




    }
}
