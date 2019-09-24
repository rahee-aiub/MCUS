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
using System.Globalization;
using System.Data;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSChequeCounter : System.Web.UI.Page
    {
      int BegNo = 0;
      int EndNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //DateTime dt = System.DateTime.Today;
                //string date = dt.ToString("dd/MM/yyyy");
                //txtIssueDate.Text = date;
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                txtIssueDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
                DateTime ProcDate = Converter.GetDateTime(dto.ProcessDate.ToShortDateString());

                
                txtChqprefix.Focus();

            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
           // string sqlquery = "SELECT * FROM A2ZCHQBOOK WHERE ChqeFx = '" + txtChqprefix.Text + "' AND ChqbPage = '" + txtNumPage.Text + "'";

         //   int result1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(sqlquery, "A2ZCSMCUS"));

          //  if (result1 > 0)
          //  {
          //      ValidityChkPrfx();

          //      txtBeginingNo.Text = string.Empty;
         //       txtEndNo.Text = string.Empty;
           //     txtChqprefix.Focus();
        //    }
        //    else
       //     {

                A2ZCHQCOUNTERDTO objDTO = new A2ZCHQCOUNTERDTO();
                string qry = "SELECT Id,ChqPStat,ChqbPage FROM A2ZCHQCOUNTER where ChqBStat='1'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        var ParentId = dr["Id"].ToString();
                        var page = dr["ChqbPage"].ToString();
                        var result = dr["ChqPStat"].ToString();

                        string value = Converter.GetString(dt.Rows[0]["ChqPStat"]);

                        string pa = value.Replace("1", "9");

                       
                        objDTO.ChqPStatus = Converter.GetString(pa);
                        objDTO.ChqBStatus = 9;
                        int roweffect = A2ZCHQCOUNTERDTO.UpdateInformation(objDTO);
                        if (roweffect > 0)
                        {

                        }
                    }

                }

                objDTO.ChqPrefix = Converter.GetString(txtChqprefix.Text);
                objDTO.ChqSerialNo = Converter.GetInteger(txtEndNo.Text);
                objDTO.ChqPage = Converter.GetSmallInteger(txtNumPage.Text);
                int num = 1;
                int a = Converter.GetInteger(txtNumPage.Text);
                int[] p = new int[a];
                string pag = "";
                for (int i = 0; i <= (a - 1); i = i + 1)
                {
                    p[(a - 1) - i] = num % 2;
                    num = num % 2;
                }
                for (int i = 0; i <= (a - 1); i = i + 1)
                {
                    pag += p[i].ToString();
                }
                objDTO.ChqPStatus = Converter.GetString(pag);

                string cx = " ";

                string dat = Converter.GetString(txtIssueDate.Text);
                string[] datetime = new string[a];
                foreach (string x in datetime)
                {

                    cx += dat.ToString();

                }
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.ChqBStatusDate = Converter.GetDateTime(dto.ProcessDate);
                objDTO.ChqPDate = Converter.GetString(cx);
                objDTO.ChqBIssueDate = Converter.GetDateTime(dto.ProcessDate);
                objDTO.ChqBStatus = 1;

                int roweffect1 = A2ZCHQCOUNTERDTO.InsertInformation(objDTO);
                if (roweffect1 > 0)
                {
                    clearinfo();
                    txtChqprefix.Focus();

                }
            //}



        }

        private void ValidityChkPrfx()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Series Overlaps with Issued');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        private void InvalidChqNo()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Invalid Cheque Serial No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        protected void txtNumPage_TextChanged(object sender, EventArgs e)
        {

           
            txtBeginingNo.Focus();
        }
        private void clearinfo()
        {
            txtChqprefix.Text = string.Empty;
            txtNumPage.Text = string.Empty;
            txtBeginingNo.Text = string.Empty;
            txtEndNo.Text = string.Empty;

        }

        protected void txtBeginingNo_TextChanged(object sender, EventArgs e)
        {

            string value = txtBeginingNo.Text;


            string sub2 = (Right(value, 1));

            if (sub2 != "1")
            {
                InvalidChqNo();
                txtBeginingNo.Text = string.Empty;
                txtBeginingNo.Focus();
            }
            else
            {
                int NoPage = Converter.GetInteger(txtNumPage.Text);
                BegNo = Converter.GetInteger(txtBeginingNo.Text);
                EndNo = (BegNo + NoPage - 1);
                txtEndNo.Text = Converter.GetString(EndNo);
            }

             try
            {
            
            DataTable dt=new DataTable();
            string sqlquery = "SELECT * FROM A2ZCHQCOUNTER WHERE ChqeFx = '" + txtChqprefix.Text + "' AND ChqSlNo !< '" + txtBeginingNo.Text + "'";

            dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
            if (dt.Rows.Count > 0)
                     
            {
                lblSlNo.Text = Converter.GetString(dt.Rows[0]["ChqSlNo"]); 
                lblbPage.Text = Converter.GetString(dt.Rows[0]["ChqbPage"]);
                int a = Converter.GetInteger(lblSlNo.Text);
                int b = Converter.GetInteger(lblbPage.Text);

                int Qresult = Converter.GetInteger(a - b + 1);
                if (BegNo == a || Qresult <= EndNo)               
                 {
                   ValidityChkPrfx();

                   txtBeginingNo.Text = string.Empty;
                   txtEndNo.Text = string.Empty;
                   txtChqprefix.Focus();
                 }
                
            }
                 
            }

                  catch (Exception ex)
            {

                throw ex;

            }
    }

        private static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        private static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        protected void txtIssueDate_TextChanged(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = DateTime.ParseExact(txtIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt3 = Converter.GetDateTime(dto.ProcessDate);
            if (dt3.Subtract(dt2).Days < 0)
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please Input Valid Date!');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    txtIssueDate.Focus();
                    txtIssueDate.Text = string.Empty;

                }
                return;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}