using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.GeneralLedger;
using System.Data;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLReverseYearEndTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPrmValue.Text = Request.QueryString["a%b"];
                string b = hdnPrmValue.Text;
                HdnModule.Text = b;

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                //A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                //DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                //string date = dt.ToString("dd/MM/yyyy");
                //CtrlTrnDate.Text = date;

                txtVoucherNo.Focus();
                BtnDelete.Visible = false;
                BtnCancel.Visible = false;


                A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.PrmYearEndDate);
                string date = dt.ToString("dd/MM/yyyy");
                //txtVchDate.Text = date;
                CtrlTrnDate.Text = date;


                if (CtrlTrnDate.Text == "01/01/0001")
                {
                    Response.Redirect("A2ZERPModule.aspx");
                }


                A2ZERPSYSPRMDTO dto2 = A2ZERPSYSPRMDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.PrmYearEndDate);

                Session["date"] = Converter.GetString(dto2.PrmYearEndDate.ToLongDateString());

            }
        }



        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            gvDetailInfo.Visible = true;

            var prm = new object[3];

            prm[0] = txtVoucherNo.Text;
            prm[1] = hdnID.Text;
            prm[2] = Converter.GetDateToYYYYMMDD(CtrlTrnDate.Text);


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLGetRevYETransaction", prm, "A2ZGLMCUS"));
            if (result == 0)
            {
                string qry = "SELECT Id,AccNo,FuncOpt,TrnRevFlag FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                if (dt.Rows.Count > 0)
                {
                    txtVoucherNo.ReadOnly = true;
                    gvDetailInfo.Visible = true;
                    BtnDelete.Visible = true;
                    BtnCancel.Visible = true;
                    gvPreview();
                }
                else
                {
                    VoucherMSG();
                    BtnDelete.Visible = false;
                    BtnCancel.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                }

            }

        }

        private void gvPreview()
        {
            string Qry = "SELECT TrnId,TrnDate,TrnGLAccNoDr,TrnGLAccNoCr,GLCreditAmt,GLDebitAmt,TrnDrCr,TrnType,'TrnSign' = CASE WHEN GLAmount < 0 THEN '1' END FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'and TrnFlag=0 and TrnCSGL=1";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZGLMCUS");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                var prm = new object[3];

                prm[0] = txtVoucherNo.Text;
                prm[1] = hdnID.Text;
                prm[2] = Converter.GetDateToYYYYMMDD(CtrlTrnDate.Text);


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLDeleteYETransaction", prm, "A2ZGLMCUS"));
                if (result == 0)
                {
                    gvDetailInfo.Visible = false;
                    BtnDelete.Visible = false;
                    BtnCancel.Visible = false;
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.Focus();
                    txtVoucherNo.ReadOnly = false;
                    Successful();
                }

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnDelete_Click Problem');</script>");
                //throw ex;
            }

        }

        private void Successful()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Transaction Delete successfully completed');", true);
            return;
        }

        private void VoucherMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Not Found');", true);
            return;

        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtVoucherNo.Text = string.Empty;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Focus();
            BtnDelete.Visible = false;
            BtnCancel.Visible = false;
            gvDetailInfo.Visible = false;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "30px");
                e.Row.Style.Add("top", "-1500px");


                if (lblChgFlag.Text == "1")
                {
                    TextBox lblCrAmt = (TextBox)e.Row.FindControl("lblCreditAmt");
                    TextBox lblDrAmt = (TextBox)e.Row.FindControl("lblDebitAmt");

                    decimal CrAmt = Converter.GetDecimal(lblCrAmt.Text);
                    decimal DrAmt = Converter.GetDecimal(lblDrAmt.Text);

                    Button BtnDel = (Button)e.Row.FindControl("BtnDel");
                    Button BtnChg = (Button)e.Row.FindControl("BtnChg");
                    Button BtnUpd = (Button)e.Row.FindControl("BtnUpd");
                    Button BtnCan = (Button)e.Row.FindControl("BtnCan");


                    BtnDel.Visible = false;
                    BtnChg.Visible = false;
                    BtnUpd.Visible = true;
                    BtnCan.Visible = true;

                    if (CrAmt > 0)
                    {
                        lblCrAmt.Enabled = true;
                    }

                    if (DrAmt > 0)
                    {
                        lblDrAmt.Enabled = true;
                    }


                }

            }
        }


        protected void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");
                Label lblTrnType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[7].FindControl("TrnType");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;
                int idincrement = 0;

                if (lblTrnType.Text == "3")
                {
                    for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                    {
                        Label ClblId = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("lblID");

                        int CID = Converter.GetInteger(ClblId.Text);

                        if (CID == ID)
                        {
                            RecID = i;
                            RecID = RecID + 1;
                        }
                    }

                    int mod = RecID % 2;

                    if (mod != 0)
                    {
                        idincrement = ID + 1;
                        var prm = new object[2];
                        prm[0] = ID;
                        prm[1] = idincrement;

                        int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLDeleteYESingleTransaction", prm, "A2ZGLMCUS"));
                        if (result == 0)
                        {
                        }
                    }
                    else
                    {
                        idincrement = ID - 1;
                        var prm = new object[2];
                        prm[0] = idincrement;
                        prm[1] = ID;

                        int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLDeleteYESingleTransaction", prm, "A2ZGLMCUS"));
                        if (result == 0)
                        {
                        }
                    }
                }
                else
                {
                    idincrement = ID + 1;
                    var prm = new object[2];
                    prm[0] = ID;
                    prm[1] = idincrement;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLDeleteYESingleTransaction", prm, "A2ZGLMCUS"));
                    if (result == 0)
                    {
                    }

                }

                var prm4 = new object[3];

                prm4[0] = txtVoucherNo.Text;
                prm4[1] = hdnID.Text;
                prm4[2] = Converter.GetDateToYYYYMMDD(CtrlTrnDate.Text);

                int result4 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLGetRevYETransaction", prm4, "A2ZGLMCUS"));
                if (result4 == 0)
                {
                    gvPreview();
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }

        protected void gvChgDetail()
        {
            string Qry = "SELECT TrnId,TrnDate,TrnGLAccNoDr,TrnGLAccNoCr,GLCreditAmt,GLDebitAmt,TrnDrCr,TrnType,'TrnSign' = CASE WHEN GLAmount < 0 THEN '1' END FROM WF_REVERSETRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'and TrnFlag=0 and TrnCSGL=1 AND TrnId = '" + lblTrnID.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZGLMCUS");
        }
        protected void BtnChg_Click(object sender, EventArgs e)
        {
            try
            {
                BtnDelete.Visible = false;



                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;

                lblTrnID.Text = Converter.GetString(lblId.Text);


                for (int i = 0; i < gvDetailInfo.Rows.Count; ++i)
                {
                    Label ClblId = (Label)gvDetailInfo.Rows[i].Cells[0].FindControl("lblID");

                    int CID = Converter.GetInteger(ClblId.Text);

                    if (CID == ID)
                    {
                        RecID = i;
                        RecID = RecID + 1;
                    }
                }

                lblRecID.Text = Converter.GetString(RecID);

                lblChgFlag.Text = "1";

                gvChgDetail();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");
                TextBox lblCrAmt = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblCreditAmt");
                TextBox lblDrAmt = (TextBox)gvDetailInfo.Rows[r.RowIndex].Cells[5].FindControl("lblDebitAmt");
                Label lblTrnDrCr = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[6].FindControl("TrnDrCr");
                Label lblTrnType = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[7].FindControl("TrnType");
                Label lblTrnSign = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[8].FindControl("TrnSign");

                int ID = Converter.GetInteger(lblId.Text);

                int RecID = 0;
                int idincrement = 0;

                Decimal Tamount = 0;
                Decimal Gamount = 0;
                Decimal Cramount = Converter.GetDecimal(lblCrAmt.Text);
                Decimal Dramount = Converter.GetDecimal(lblDrAmt.Text);


                if (Cramount != 0)
                {
                    Gamount = Cramount;
                }

                if (Dramount != 0)
                {
                    Gamount = Dramount;
                }

                Tamount = Gamount;


                if (lblTrnSign.Text == "1")
                {
                    Tamount = (0 - Gamount);
                }


                if (lblTrnType.Text == "3")
                {

                    RecID = Converter.GetInteger(lblRecID.Text);


                    int mod = RecID % 2;

                    if (mod != 0)
                    {
                        idincrement = ID + 1;
                    }
                    else
                    {
                        idincrement = ID - 1;
                    }
                }
                else
                {
                    idincrement = ID + 1;
                }

                if (lblTrnDrCr.Text == "1")
                {
                    var prm = new object[4];
                    prm[0] = Gamount;
                    prm[1] = Tamount;
                    prm[2] = ID;
                    prm[3] = 1;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLChangeYESingleTransaction", prm, "A2ZGLMCUS"));
                    if (result == 0)
                    {
                    }


                    //string strQuery1 = "UPDATE A2ZTRANSACTION SET  GLCreditAmt = '" + Gamount + "', GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    //int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string qry = "SELECT TrnId,GLAmount FROM WF_REVERSETRANSACTION WHERE TrnId = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(Gamount);

                        if (Amount < 0)
                        {
                            Camount = (0 - Gamount);
                        }

                        var prm1 = new object[4];
                        prm1[0] = Gamount;
                        prm1[1] = Camount;
                        prm1[2] = idincrement;
                        prm1[3] = 0;

                        int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLChangeYESingleTransaction", prm1, "A2ZGLMCUS"));
                        if (result1 == 0)
                        {
                        }


                        //string strQuery2 = "UPDATE A2ZTRANSACTION SET  GLDebitAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        //int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }
                }
                else
                {
                    var prm2 = new object[4];
                    prm2[0] = Gamount;
                    prm2[1] = Tamount;
                    prm2[2] = ID;
                    prm2[3] = 0;

                    int result2 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLChangeYESingleTransaction", prm2, "A2ZGLMCUS"));
                    if (result2 == 0)
                    {
                    }


                    //string strQuery1 = "UPDATE A2ZTRANSACTION SET  GLDebitAmt = '" + Gamount + "',GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    //int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));


                    string qry = "SELECT TrnId,GLAmount FROM WF_REVERSETRANSACTION WHERE TrnId = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(Gamount);

                        if (Amount < 0)
                        {
                            Camount = (0 - Gamount);
                        }

                        var prm3 = new object[4];
                        prm3[0] = Gamount;
                        prm3[1] = Camount;
                        prm3[2] = idincrement;
                        prm3[3] = 1;

                        int result3 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLChangeYESingleTransaction", prm3, "A2ZGLMCUS"));
                        if (result3 == 0)
                        {
                        }

                        //string strQuery2 = "UPDATE A2ZTRANSACTION SET  GLCreditAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        //int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }
                }


                lblChgFlag.Text = "0";

                BtnDelete.Visible = true;

                var prm4 = new object[3];

                prm4[0] = txtVoucherNo.Text;
                prm4[1] = hdnID.Text;
                prm4[2] = Converter.GetDateToYYYYMMDD(CtrlTrnDate.Text);

                int result4 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLGetRevYETransaction", prm4, "A2ZGLMCUS"));
                if (result4 == 0)
                {
                    gvPreview();
                }


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnCan_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblID");

                int ID = Converter.GetInteger(lblId.Text);

                lblChgFlag.Text = "0";
                lblCanFlag.Text = "1";

                BtnDelete.Visible = true;

                gvPreview();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }




    }
}