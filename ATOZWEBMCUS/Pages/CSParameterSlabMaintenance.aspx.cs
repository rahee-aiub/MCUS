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
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSParameterSlabMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtPensionDate.Focus();
                ddlSlabFlag.Focus();
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                BtnPreSubmit.Visible = false;
                BtnPreUpdate.Visible = false;
                BtnPreDelete.Visible = false;
                BtnExit.Visible = false;
                string typecls = (string)Session["TypeClass"];
                lblTypecls.Text = typecls;
                string typecode = (string)Session["TypeCode"];
                lblTCode.Text = typecode;
                gvDetail();
                if (lblTypecls.Text == "4")
                {
                    PensionSlab2Hide();
                    PensionSlab3Hide();
                    PensionSlab1();
                    Prematureshow();
                    BtnExit.Visible = true;
                    clearInfo();
                }
                if (lblTypecls.Text == "2")
                {
                    PensionSlab1Hide();
                    PensionSlab3Hide();
                    PensionSlab2();
                    Prematureshow();
                    BtnExit.Visible = true;
                    clearInfo();
                }
                if (lblTypecls.Text == "3")
                {
                    PensionSlab1Hide();
                    PensionSlab2Hide();
                    PensionSlab3();
                    Prematureshow();
                    BtnExit.Visible = true;
                    clearInfo();
                }
               
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT  AtyDate,AtyAccType,AtyFlag,AtyRecords,AtyPeriod,AtyMaturedAmt,AtyIntRate,AtyPenalAmt,AtyBonusAmt from A2ZATYSLAB where AtyAccType='"+lblTCode.Text+"'";

            
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            A2ZPENSIONDTO objDTO = new A2ZPENSIONDTO();
            objDTO.AccType = Converter.GetInteger(lblTCode.Text);
            //if (txtPensionDate.Text != string.Empty)
            //{
            //    DateTime opdate = DateTime.ParseExact(txtPensionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    objDTO.Date = opdate;
            //}
            //else
            //{
            //    objDTO.Date = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
            //}
            objDTO.SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
            objDTO.DepositeAmount = Converter.GetDecimal(txtPensionRecord.Text);
            objDTO.Period = Converter.GetInteger(txtPeriodMth.Text);
            objDTO.MaturedAmount = Converter.GetDecimal(txtBenefitAmt.Text);
            objDTO.InterestRate = Converter.GetDecimal(txtPensionIntRate.Text);
            objDTO.PenalAmount = Converter.GetDecimal(txtPenalAmt.Text);
            objDTO.BonusAmount = Converter.GetDecimal(txtBonusAmt.Text);
            int roweffect = A2ZPENSIONDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                gvDetail();
                clearInfo();
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                ddlSlabFlag.Focus();
                //txtPensionDate.Focus();
                

            }
        }

        protected void Hide()
        {
            //txtPensionDate.Visible = false;
            ddlSlabFlag.Visible = false;
            txtPensionRecord.Visible = false;
            txtPeriodMth.Visible=false;
            txtBenefitAmt.Visible = false;
            txtPensionIntRate.Visible=false;
            txtPenalAmt.Visible = false;
            txtBonusAmt.Visible=false;
             
            //txtPreDate.Visible = false;
            txtMnthBelow.Visible = false;
            txtIntRate.Visible = false;

            //lblDate.Visible = false;
            lblSlabFlag.Visible = false;
            lblRecord.Visible = false;
            lblPeriodMth.Visible = false;
            lblBenefitAmt.Visible = false;
            lblPensionIntRate.Visible = false;
            lblPenalAmt.Visible = false;
            lblBonusAmt.Visible = false;

            //lblPreDate.Visible = false;
            lblMnthBelow.Visible = false;
            lblPreIntRate.Visible = false;

            lblPensionHead.Visible = false;
            lblPrematureHead.Visible = false;
        }

        private void clearInfo()
        {
            //txtPensionDate.Text = string.Empty;
            ddlSlabFlag.SelectedValue = "0";
            txtPensionRecord.Text = string.Empty;
            txtPeriodMth.Text = string.Empty;
            txtBenefitAmt.Text = string.Empty;
            txtPensionIntRate.Text = string.Empty;
            txtPenalAmt.Text = string.Empty;
            txtBonusAmt.Text = string.Empty;
            
            //txtPreDate.Text = string.Empty;
            txtMnthBelow.Text = string.Empty;
            txtIntRate.Text = string.Empty;
            
        }

        protected void PensionSlab1()
        {
            lblPensionHead.Visible = true;
            //txtPensionDate.Visible = true;
            ddlSlabFlag.Visible = true;
            txtPensionRecord.Visible = true;
            txtPeriodMth.Visible = true;
            txtPensionIntRate.Visible = true;
            txtBenefitAmt.Visible = true;
            txtPenalAmt.Visible = true;
            txtBonusAmt.Visible = true;

            //lblDate.Visible = true;
            lblSlabFlag.Visible = true;
            lblRecord.Visible = true;
            lblPeriodMth.Visible = true;
            lblBenefitAmt.Visible = true;
            lblPensionIntRate.Visible = true;
            lblPenalAmt.Visible = true;
            lblBonusAmt.Visible = true;

        }

        protected void PensionSlab1Hide()
        {
            lblPensionHead.Visible = false;
            //txtPensionDate.Visible = false;
            ddlSlabFlag.Visible = false;
            txtPensionRecord.Visible = false;
            txtPeriodMth.Visible = false;
            txtBenefitAmt.Visible = false;
            txtPensionIntRate.Visible = false;
            txtPenalAmt.Visible = false;
            txtBonusAmt.Visible = false;

            //lblDate.Visible = false;
            lblSlabFlag.Visible = false;
            lblRecord.Visible = false;
            lblPeriodMth.Visible = false;
            lblBenefitAmt.Visible = false;
            lblPensionIntRate.Visible = false;
            lblPenalAmt.Visible = false;
            lblBonusAmt.Visible = false;
            
        }
        protected void PensionSlab2()
        {
            lblPensionHead.Visible = true;
            //txtPensionDate.Visible = true;
            ddlSlabFlag.Visible = true;
            txtPeriodMth.Visible = true;
            txtPensionIntRate.Visible = true;

            //lblDate.Visible = true;
            lblSlabFlag.Visible = true;
            lblPeriodMth.Visible = true;
            lblPensionIntRate.Visible = true;
            

        }
        protected void PensionSlab2Hide()
        {
            lblPensionHead.Visible = false;
            //txtPensionDate.Visible = false;
            ddlSlabFlag.Visible = false;
            txtPeriodMth.Visible = false;
            txtPensionIntRate.Visible = false;

            //lblDate.Visible = false;
            lblSlabFlag.Visible = false;
            lblPeriodMth.Visible = false;
            lblPensionIntRate.Visible = false;                     
        }

        protected void PensionSlab3()
        {
            lblPensionHead.Visible = true;
            //txtPensionDate.Visible = true;
            ddlSlabFlag.Visible = true;
            txtPensionRecord.Visible = true;
            txtPeriodMth.Visible = true;
            txtBenefitAmt.Visible = true;

            //lblDate.Visible = true;
            lblSlabFlag.Visible = true;
            lblRecord.Visible = true;
            lblPeriodMth.Visible = true;
            lblBenefitAmt.Visible = true;
         

        }
        protected void PensionSlab3Hide()
        {
            lblPensionHead.Visible = false;
            //txtPensionDate.Visible = false;
            ddlSlabFlag.Visible = false;
            txtPensionRecord.Visible = false;
            txtPeriodMth.Visible = false;
            txtBenefitAmt.Visible = false;

            //lblDate.Visible = false;
            lblSlabFlag.Visible = false;
            lblRecord.Visible = false;
            lblPeriodMth.Visible = false;
            lblBenefitAmt.Visible = false;
        }
        protected void Prematureshow()
        {
            lblPrematureHead.Visible = true;
            //txtPreDate.Visible = true;
            txtMnthBelow.Visible = true;
            txtIntRate.Visible = true;
            lblMnthBelow.Visible = true;
            //lblPreDate.Visible = true;
            lblPreIntRate.Visible = true;
          
        }

        protected void PensionGetInfo()
        {
            A2ZPENSIONDTO getDTO = new A2ZPENSIONDTO();
            int accType = Converter.GetInteger(lblTCode.Text);
            Int16 SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
            double record = Converter.GetDouble(txtPensionRecord.Text);
            Int16 period = Converter.GetSmallInteger(txtPeriodMth.Text);
            //DateTime dat = DateTime.ParseExact(txtPensionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            getDTO = (A2ZPENSIONDTO.PGetInformation(accType,SlabFlag,record, period));
            if (getDTO.NoRecord>0)
            {
               
                txtBenefitAmt.Text = Converter.GetString(string.Format("{0:0,0.00}",getDTO.MaturedAmount));
                txtPensionIntRate.Text = Converter.GetString(string.Format("{0:0,0.00}",getDTO.InterestRate));
                txtPenalAmt.Text = Converter.GetString(string.Format("{0:0,0.00}",getDTO.PenalAmount));
                txtBonusAmt.Text = Converter.GetString(string.Format("{0:0,0.00}",getDTO.BonusAmount));
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
                BtnDelete.Visible = true;
            }
            else
            {
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
            }

        }



        //protected void txtAccType_TextChanged(object sender, EventArgs e)
        //{
        //    A2ZACCTYPEDTO getDTO = new A2ZACCTYPEDTO();
        //    Int16 accType = Converter.GetSmallInteger(txtAccType.Text);
        //    getDTO = (A2ZACCTYPEDTO.GetInformation(accType));
        //    if (getDTO.AccTypeCode > 0)
        //    {
        //        lblTypecls.Text = Converter.GetString(getDTO.AccTypeClass);
        //        lblTypeName.Text = Converter.GetString(getDTO.AccTypeDescription);
        //        gvDetail();
        //    }
        //    if (lblTypecls.Text == "4")
        //    {
        //        PensionSlab2Hide();
        //        PensionSlab3Hide();
        //        PensionSlab1();
        //        Prematureshow();
        //        BtnExit.Visible = true;
        //        clearInfo();
        //    }
        //    if (lblTypecls.Text == "2")
        //    {
        //        PensionSlab1Hide();
        //        PensionSlab3Hide();
        //        PensionSlab2();
        //        Prematureshow();
        //        BtnExit.Visible = true;
        //        clearInfo();
        //    }
        //    if (lblTypecls.Text == "3")
        //    {
        //        PensionSlab1Hide();
        //        PensionSlab2Hide();
        //        PensionSlab3();
        //        Prematureshow();
        //        BtnExit.Visible = true;
        //        clearInfo();
        //    }
        //}

    

        protected void txtPeriodMth_TextChanged(object sender, EventArgs e)
        {
          PensionGetInfo();
          if (lblTypecls.Text == "2")
          {
              txtPensionIntRate.Focus();
          }
          else
          {
              txtBenefitAmt.Focus();
          }
        
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZPENSIONDTO upDTO = new A2ZPENSIONDTO();
            upDTO.AccType = Converter.GetInteger(lblTCode.Text);
            upDTO.SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
            //DateTime opdate = DateTime.ParseExact(txtPensionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //upDTO.Date = opdate;
            upDTO.DepositeAmount = Converter.GetDecimal(txtPensionRecord.Text);
            upDTO.Period = Converter.GetInteger(txtPeriodMth.Text);
            upDTO.MaturedAmount = Converter.GetDecimal(txtBenefitAmt.Text);
            upDTO.InterestRate = Converter.GetDecimal(txtPensionIntRate.Text);
            upDTO.PenalAmount = Converter.GetDecimal(txtPenalAmt.Text);
            upDTO.BonusAmount = Converter.GetDecimal(txtBonusAmt.Text);
            int roweffect = A2ZPENSIONDTO.UpdateInformation(upDTO);
            if (roweffect > 0)
            {
                gvDetail();
                clearInfo();
                ddlSlabFlag.Focus();
                //txtPensionDate.Focus();
            
            }
            BtnSubmit.Visible = false;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string strQuery = @"Delete from A2ZATYSLAB where AtyAccType='" + lblTCode.Text + "' and AtyRecords='" + txtPensionRecord.Text 
                + "' and AtyFlag='" + ddlSlabFlag.Text + "' and AtyPeriod='" + txtPeriodMth.Text + "'";
            int roweffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
            if (roweffect > 0)
            {
                clearInfo();
                gvDetail();
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                ddlSlabFlag.Focus();
                //txtPensionDate.Focus();
                
            }
        }

        protected void PrematureGetInfo()
        {
            Errmsg.Text = "0";
            if (ddlSlabFlag.SelectedIndex == 0)
            {
                Errmsg.Text = "1";
                txtMnthBelow.Text = string.Empty;
                ddlSlabFlag.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Member Type');", true);
                return;
            }

            A2ZPENSIONDTO getDTO = new A2ZPENSIONDTO();
            int accType = Converter.GetInteger(lblTCode.Text);
            Int16 SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
            double record = Converter.GetDouble(99);
            Int16 period = Converter.GetSmallInteger(txtMnthBelow.Text);
            //DateTime dat = DateTime.ParseExact(txtPreDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            getDTO = (A2ZPENSIONDTO.PGetInformation(accType,SlabFlag,record, period));
            if (getDTO.NoRecord>0)
            {

                txtMnthBelow.ReadOnly = true;
                txtIntRate.Text = Converter.GetString(string.Format("{0:0,0.00}",getDTO.InterestRate));
                BtnPreSubmit.Visible = false;
                BtnPreUpdate.Visible = true;
                BtnPreDelete.Visible = true;

            }
            else
            {
                BtnPreSubmit.Visible = true;
                BtnPreUpdate.Visible = false;
                BtnPreDelete.Visible = false;
            }
        }

        protected void BtnPreSubmit_Click(object sender, EventArgs e)
        {
            A2ZPENSIONDTO objDTO = new A2ZPENSIONDTO();
            objDTO.AccType = Converter.GetInteger(lblTCode.Text);
            objDTO.SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
            //if (txtPensionDate.Text != string.Empty)
            //{
            //    DateTime opdate = DateTime.ParseExact(txtPreDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    objDTO.Date = opdate;
            //}
            //else
            //{
            //    objDTO.Date = Converter.GetDateTime(System.DateTime.Now.ToShortDateString());
            //}
            objDTO.DepositeAmount = Converter.GetSmallInteger(99);
            objDTO.Period = Converter.GetSmallInteger(txtMnthBelow.Text);
            objDTO.InterestRate = Converter.GetDecimal(txtIntRate.Text);
            int roweffect = A2ZPENSIONDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                gvDetail();
                clearInfo();
                BtnPreSubmit.Visible = false;
                BtnPreUpdate.Visible = false;

                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                txtMnthBelow.Focus();
                //txtPreDate.Focus();

            }

        }

        protected void BtnPreUpdate_Click(object sender, EventArgs e)
        {
            A2ZPENSIONDTO upDTO = new A2ZPENSIONDTO();
            upDTO.AccType = Converter.GetInteger(lblTCode.Text);
            upDTO.SlabFlag = Converter.GetSmallInteger(ddlSlabFlag.SelectedValue);
           //DateTime opdate = DateTime.ParseExact(txtPreDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
           //upDTO.Date = opdate;
           upDTO.DepositeAmount = Converter.GetDecimal(99);
           upDTO.Period = Converter.GetSmallInteger(txtMnthBelow.Text);
           upDTO.InterestRate = Converter.GetDecimal(txtIntRate.Text);
           string strQuery = "UPDATE A2ZATYSLAB set AtyRecords='" + upDTO.DepositeAmount + "',AtyPeriod='" + upDTO.Period + "', AtyIntRate='" + upDTO.InterestRate + "' where AtyAccType='"+lblTCode.Text+"' and AtyPeriod='" + txtMnthBelow.Text + "'";
           int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
           if (rowEffect > 0)
           {
               gvDetail();
               clearInfo();
               txtMnthBelow.Focus();
               //txtPreDate.Focus();

           }

           txtMnthBelow.ReadOnly = false;

           BtnPreSubmit.Visible = false;
           BtnPreUpdate.Visible = false;
           BtnPreDelete.Visible = false;

           BtnSubmit.Visible = false;
           BtnUpdate.Visible = false;
           BtnDelete.Visible = false;
        }

        protected void txtMnthBelow_TextChanged(object sender, EventArgs e)
        {
            PrematureGetInfo();

            if (Errmsg.Text == "1")
            {
                return;
            }

            txtIntRate.Focus();
        }

        protected void BtnPreDelete_Click(object sender, EventArgs e)
        {
            string strQuery = @"Delete from A2ZATYSLAB where  AtyAccType='" + lblTCode.Text + "' and AtyFlag='" + ddlSlabFlag.Text + "' and AtyRecords='99' and AtyPeriod='" + txtMnthBelow.Text + "'";
            int roweffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
            if (roweffect > 0)
            {
                clearInfo();
                gvDetail();

                txtMnthBelow.ReadOnly = false;

                BtnPreSubmit.Visible = false;
                BtnPreUpdate.Visible = false;
                BtnPreDelete.Visible = false;

                BtnSubmit.Visible = false;
                BtnUpdate.Visible = false;
                BtnDelete.Visible = false;
                txtMnthBelow.Focus();
                //txtPreDate.Focus();

            }
        }

        protected void txtPreDate_TextChanged(object sender, EventArgs e)
        {
            txtMnthBelow.Focus();
        }

        protected void txtPensionDate_TextChanged(object sender, EventArgs e)
        {
            ddlSlabFlag.Focus();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
              "click", @"<script>window.opener.location.href='CSParameterOpeningMaintenance'; self.close();</script>", false);


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>var loc = window.opener.location; window.opener.location = loc;self.close();</script>");
            ////lblTest.Text = "1";
            ////Session["test"] = lblTest.Text;
        }

        protected void txtPensionRecord_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtPensionRecord.Text);
            txtPensionRecord.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtPeriodMth.Focus();

        }

        protected void txtBenefitAmt_TextChanged(object sender, EventArgs e)
        {
            double ValueConvert = Converter.GetDouble(txtBenefitAmt.Text);
            txtBenefitAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", ValueConvert));
            txtPensionIntRate.Focus();

        }

        protected void ddlSlabFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
             txtPensionRecord.Focus();
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

     
    }
}
