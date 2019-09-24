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

namespace ATOZWEBMCUS.Pages
{
    public partial class GLDailyReverseTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPrmValue.Text = Request.QueryString["a%b"];
                string b = hdnPrmValue.Text;
                HdnModule.Text = b;

                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlTrnDate.Text = date;

                txtVoucherNo.Focus();
                BtnDelete.Visible = false;
                BtnCancel.Visible = false;

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string qry = "SELECT Id,AccNo FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "' and TrnCSGL=1 AND TrnModule = '" + HdnModule.Text + "' AND FromCashCode = '" + lblCashCode.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
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
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSearch_Click Problem');</script>");
                //throw ex;
            }
        }

        private void gvPreview()
        {
            try
            {
                DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string Qry = "SELECT Id,TrnDate,TrnGLAccNoDr,TrnGLAccNoCr,GLCreditAmt,GLDebitAmt,TrnDrCr,TrnType,'TrnSign' = CASE WHEN GLAmount < 0 THEN '1' END FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'and TrnFlag=0 and TrnCSGL=1";
                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");

            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime opdate = DateTime.ParseExact(CtrlTrnDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string str = "DELETE A2ZTRANSACTION WHERE VchNo='" + txtVoucherNo.Text + "'and TrnCSGL=1";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str, "A2ZCSMCUS"));
                if (rowEffect > 0)
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
                        string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + ID + "' and '" + idincrement + "'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    }
                    else
                    {
                        idincrement = ID - 1;
                        string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + idincrement + "' and '" + ID + "'";
                        int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    }
                }
                else
                {
                    idincrement = ID + 1;
                    string strQuery1 = "DELETE FROM A2ZTRANSACTION WHERE Id between '" + ID + "' and '" + idincrement + "'";
                    int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                }


                gvPreview();

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_RowDeleting Problem');</script>");
                //throw ex;
            }
        }

        protected void gvChgDetail()
        {
            string Qry = "SELECT Id,TrnDate,TrnGLAccNoDr,TrnGLAccNoCr,GLCreditAmt,GLDebitAmt,TrnDrCr,TrnType,'TrnSign' = CASE WHEN GLAmount < 0 THEN '1' END FROM A2ZTRANSACTION  WHERE VchNo = '" + txtVoucherNo.Text + "'and TrnFlag=0 and TrnCSGL=1 AND Id = '" + lblTrnID.Text + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(Qry, gvDetailInfo, "A2ZCSMCUS");
        }
        protected void BtnChg_Click(object sender, EventArgs e)
        {
            try
            {
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
                    string strQuery1 = "UPDATE A2ZTRANSACTION SET  GLCreditAmt = '" + Gamount + "', GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));

                    string qry = "SELECT Id,GLAmount FROM A2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(Gamount);

                        if (Amount < 0)
                        {
                            Camount = (0 - Gamount);
                        }

                        string strQuery2 = "UPDATE A2ZTRANSACTION SET  GLDebitAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }
                }
                else
                {
                    string strQuery1 = "UPDATE A2ZTRANSACTION SET  GLDebitAmt = '" + Gamount + "',GLAmount = '" + Tamount + "' WHERE Id = '" + ID + "' ";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));


                    string qry = "SELECT Id,GLAmount FROM A2ZTRANSACTION WHERE Id = '" + idincrement + "' ";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        Decimal Amount = Converter.GetDecimal(dt.Rows[0]["GLAmount"]);

                        Decimal Camount = Converter.GetDecimal(Gamount);

                        if (Amount < 0)
                        {
                            Camount = (0 - Gamount);
                        }

                        string strQuery2 = "UPDATE A2ZTRANSACTION SET  GLCreditAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";
                        int rowEffect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery2, "A2ZCSMCUS"));
                    }
                }


                lblChgFlag.Text = "0";

                gvPreview();


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