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


    public partial class CSCheckBook : System.Web.UI.Page
    {
        int BegNo = 0;
        int EndNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BtnUpdate.Visible = false;
                
                AccTypedropdown();
                Memdropdown();
                txtMemNo.Focus();
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                txtIssueDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
                ddlMemNo.SelectedIndex = 0;
              
            
            }

        }
                
          protected void Memdropdown()
        {
            string sqquery = @"SELECT MemNo,MemName FROM A2ZMEMBER";

            ddlMemNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlMemNo, "A2ZCSMCUS");


        }


          private void AccTypedropdown()
        {

            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
        }


        private void PreviousChqBook()
        {
            try
            {
                string sqlquery = "SELECT * FROM A2ZCHQBOOK WHERE AccType = '" + txtAccType.Text + "' AND AccNo = '" + txtAccNo.Text + "' AND MemNo='" + txtMemNo.Text + "' AND ChqBStat='" + "1" + "'";

                int result1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(sqlquery, "A2ZCSMCUS"));

                if (result1 > 0)
                {
                    ValidityChkPrfx1();

                    A2ZCHQBOOKDTO objDTO = new A2ZCHQBOOKDTO();

                    var value = "ChqPStat".ToString();
                    string pa = value.Replace("1", "3");
                    objDTO.ChqBStatus = 3;
                    objDTO.ChqPStatus = Converter.GetString(pa);
                    int roweffect = A2ZCHQBOOKDTO.UpdateInformation(objDTO);


                }
                else
                {



                }

            }
            catch (Exception ex)
            {

                throw ex;

            }
        }


        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            //  if (txtMemNo.Text != string.Empty)
            {
                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                Int16 CuType = 0;
                int CuNo = 0;
                int MemNumber = Converter.GetInteger(txtMemNo.Text);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CuNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    ddlMemNo.SelectedValue = Converter.GetString(getDTO.MemberNo);
                    txtAccType.Focus();
                }
                else
                {
                    ddlMemNo.SelectedValue = "-Select-";
                    txtMemNo.Focus();
                }
            }
        }

        protected void ddlMemNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemNo.SelectedValue == "-Select-")
            {
                txtMemNo.Text = string.Empty;
                txtMemNo.Focus();
            }
            if (ddlMemNo.SelectedValue != "-Select-")
            {
                A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

                Int16 CuType = 0;
                int CuNo = 0;
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CuNo, MemNumber));

                if (getDTO.NoRecord > 0)
                {

                    txtMemNo.Text = Converter.GetString(getDTO.MemberNo);
                    txtAccType.Focus();
                }
                else
                {
                    txtMemNo.Text = string.Empty;
                }
            }
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccType.SelectedValue == "-Select-")
            {
                txtAccType.Text = string.Empty;
                txtAccType.Focus();

            }
            if (ddlAccType.SelectedValue != "-Select-")
            {
                Int16 MainCode = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                }
                else
                {
                    txtAccType.Text = string.Empty;
                }
                //   string qry = "SELECT AccNo,AccNo FROM A2ZACCOUNT WHERE AccType = '" + txtAccType.Text + "'";
                //   DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                //   if (dt.Rows.Count > 0)

                Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
                int AccNumber = Converter.GetInteger(txtAccNo.Text);
                int CUnumber = 0;
                Int16 CuType = 0;
                int MemNumber = Converter.GetInteger(txtMemNo.Text);


                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInformation(Acctype, AccNumber, CuType,CUnumber, MemNumber));

                if (accgetDTO.a > 0)
                {

                    txtAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                }

                else
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Account Does Not Exist');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }

                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();

                    return;



                }
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            if (txtAccType.Text != string.Empty)
            {
                Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                if (getDTO.AccTypeCode > 0)
                {
                    ddlAccType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                }
                else
                {
                    ddlAccType.SelectedValue = "-Select-";
                }

                Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);
                int AccNumber = Converter.GetInteger(txtAccNo.Text);
                int CUnumber = 0;
                Int16 CuType = 0;
                int MemNumber = Converter.GetInteger(txtMemNo.Text);


                A2ZACCOUNTDTO accgetDTO = (A2ZACCOUNTDTO.GetInformation(Acctype, AccNumber, CuType,CUnumber, MemNumber));

                if (accgetDTO.a > 0)
                {

                    txtAccNo.Text = Converter.GetString(accgetDTO.AccNo);

                }




                else
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Account Does Not Exist');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }

                    txtAccType.Text = string.Empty;
                    txtAccType.Focus();

                    return;



                }
            }

        }
        protected void txtBeginingNo_TextChanged(object sender, EventArgs e)
        {
            string value = txtBeginingNo.Text;


            string sub2 = (Right(value, 1));

            if (sub2 != "1")
            {
                InvalidChqNo();
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
                DataTable dt = new DataTable();
                string sqlquery = "SELECT * FROM A2ZCHQBOOK WHERE ChqeFx = '" + txtChqprefix.Text + "' AND ChqSlNo !< '" + txtBeginingNo.Text + "'";

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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            PreviousChqBook();


            A2ZCHQBOOKDTO objDTO = new A2ZCHQBOOKDTO();
            
            objDTO.MemberNo = Converter.GetInteger(txtMemNo.Text);
            objDTO.AccType = Converter.GetSmallInteger(txtAccType.Text);
            objDTO.AccNo = Converter.GetLong(txtAccNo.Text);
            objDTO.ChqPrefix = Converter.GetString(txtChqprefix.Text);
            objDTO.ChqSerialNo = Converter.GetInteger(txtEndNo.Text);
            objDTO.ChqPage = Converter.GetSmallInteger(txtNumPage.Text);
            objDTO.ChqBStatus = 1;
            int num = 1;
            int a = Converter.GetInteger(txtNumPage.Text);
            int[] p = new int[a];
            string pa = "";
            for (int i = 0; i <= (a - 1); i = i + 1)
            {
                p[(a - 1) - i] = num % 2;
                num = num % 2;
            }
            for (int i = 0; i <= (a - 1); i = i + 1)
            {
                pa += p[i].ToString();
            }
            string cx=" ";

            string  dat = Converter.GetString(txtIssueDate.Text);
            string[] datetime = new string[a];
            foreach (string x in datetime)
            {

              
                     cx += dat.ToString();
         
            }

            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            objDTO.ChqPStatus = Converter.GetString(pa);
            objDTO.ChqBStatusDate = Convert.ToDateTime(dto.ProcessDate);
            objDTO.ChqPDate = Converter.GetString(cx);
            objDTO.ChqBIssueDate = Converter.GetDateTime(dto.ProcessDate);
            int roweffect = A2ZCHQBOOKDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {
                clearinfo();

            }
        }

        public void clearinfo()
        {
            
            txtMemNo.Text = string.Empty;
            txtAccType.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtChqprefix.Text = string.Empty;
            txtNumPage.Text = string.Empty;
            txtBeginingNo.Text = string.Empty;
            txtEndNo.Text = string.Empty;
            ddlMemNo.SelectedIndex = 0;
            ddlAccType.SelectedIndex = 0;
            txtMemNo.Focus();



        }

        protected void txtAccNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlquery = "SELECT * FROM A2ZACCOUNT WHERE AccType = '" + txtAccType.Text + "' and AccNo = '" + txtAccNo.Text + "' and CuType='" + 0 + "' and CuNo='" + 0 + "' and MemNo='" + txtMemNo.Text + "'";

                int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(sqlquery, "A2ZCSMCUS"));

                if (result > 0)
                {
                    txtChqprefix.Focus();
                }
                else
                {
                    Validity();
                    txtAccNo.Text = string.Empty;
                    txtAccNo.Focus();

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
        private void Validity()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('This Account Does Not Exists ');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }
        private void ValidityChkPrfx()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('This Cheque Series Overlaps with Issued');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        private void ValidityChkPrfx1()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "confirm('Cheque Book/Leaf Already Exit');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        protected void txtNumPage_TextChanged(object sender, EventArgs e)
        {
            txtBeginingNo.Text = string.Empty;
            txtBeginingNo.Focus();
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