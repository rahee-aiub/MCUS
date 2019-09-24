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
    public partial class CSCheckStatusChange : System.Web.UI.Page
    {
        int LeafNo = 0;
        int BegNo = 0;
        int EndNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Memdropdown();
                AccTypedropdown();
                txtMemNo.Focus();

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

        
        protected void txtMemNo_TextChanged(object sender, EventArgs e)
        {
            A2ZMEMBERDTO getDTO = new A2ZMEMBERDTO();

            Int16 CuType = 0;
            int CreditNumber = 0;
            int MemNumber = Converter.GetInteger(txtMemNo.Text);
            getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CreditNumber, MemNumber));

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
                int CreditNumber = 0;
                int MemNumber = Converter.GetInteger(ddlMemNo.SelectedValue);
                getDTO = (A2ZMEMBERDTO.GetInformation(CuType,CreditNumber, MemNumber));

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
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                Int16 CuType = 0;
                int CUnumber = 0;
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
                Int64 AccNumber = Converter.GetLong(txtAccNo.Text);
                Int16 CuType = 0;
                int CUnumber = 0;
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


        private static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCheBook.SelectedIndex == 1)
                {
                    A2ZCHQBOOKDTO objDTO = new A2ZCHQBOOKDTO();
                    int page = Converter.GetSmallInteger(txtNumPage.Text);
                    int leafNo = Converter.GetSmallInteger(txtCheqLeafNo.Text);
                    int opt = Converter.GetSmallInteger(ddlChqOption.SelectedValue);
                    string optvalue = Converter.GetString(opt);
                    DataTable dt = new DataTable();
                    string qry = "SELECT * from A2ZCHQBOOK where MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccNo='" + txtAccNo.Text + "'";
                    dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {

                        string value = Converter.GetString(dt.Rows[0]["ChqPStat"]).TrimEnd();
                        string datevalue = Converter.GetString(dt.Rows[0]["ChqPDt"]).TrimStart();
                        DateTime issuedt = Converter.GetDateTime(dt.Rows[0]["ChqBIssDt"]);
                        string IssueResultdt = issuedt.ToString("dd/MM/yyyy");
                        for (int i = 0; i <= page; i++)
                        {

                            string dtsub = datevalue.Substring(i, 10);

                            if (i == leafNo)
                            {

                                string sub4 = datevalue.Replace(dtsub, IssueResultdt);
                                lblvalue.Text = Converter.GetString(sub4);
                            }

                        }
                        value = DataAccessLayer.Utility.AtoZUtility.ConvertTextArray(leafNo, value, optvalue, page, 1, page, 0);
                        //datevalue = DataAccessLayer.Utility.AtoZUtility.ConvertTextArray(leafNo, datevalue, IssueResultdt, page, 1, page, 0);
                        lblvalue2.Text = Converter.GetString(value);

                    }


                    objDTO.MemberNo = Converter.GetInteger(txtMemNo.Text);
                    objDTO.AccNo = Converter.GetLong(txtAccNo.Text);
                    objDTO.AccType = Converter.GetSmallInteger(txtAccType.Text);
                    objDTO.ChqPStatus = Converter.GetString(lblvalue2.Text);
                    objDTO.ChqPDate = Converter.GetString(lblvalue.Text);
                    int roweffect = A2ZCHQBOOKDTO.UpdateInformation(objDTO);
                    if (roweffect > 0)
                    {
                        clearinfo();
                    }


                }
                else
                {
                    A2ZCHQBOOKDTO objDTO = new A2ZCHQBOOKDTO();
                    int page = Converter.GetSmallInteger(txtNumPage.Text);
                    int opt = Converter.GetSmallInteger(ddlChqOption.SelectedValue);
                    string optvalue = Converter.GetString(opt);
                    DataTable dt = new DataTable();

                    string qry = "SELECT * from A2ZCHQBOOK where MemNo='" + txtMemNo.Text + "' and AccType='" + txtAccType.Text + "' and AccNo='" + txtAccNo.Text + "'";
                    dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        string value = Converter.GetString(dt.Rows[0]["ChqPStat"]);
                        string datevalue = Converter.GetString(dt.Rows[0]["ChqPDt"]).TrimStart();
                        DateTime issuedt = Converter.GetDateTime(dt.Rows[0]["ChqBIssDt"]);
                        string IssueResultdt = issuedt.ToString("dd/MM/yyyy");
                        for (int i = 0; i <= page; i++)
                        {

                            string sub2 = value.Substring(i, 1);
                            string dtsub = datevalue.Substring(i, 10);

                            if (dtsub != "0")
                            {

                                string sub4 = datevalue.Replace(dtsub, IssueResultdt);
                                lblvalue.Text = Converter.GetString(sub4);
                            }
                            if (sub2 == "1")
                            {
                                string sub3 = value.Replace(sub2, optvalue);

                                lblvalue2.Text = Converter.GetString(sub3);

                            }

                        }

                    }
                    objDTO.MemberNo = Converter.GetInteger(txtMemNo.Text);
                    objDTO.AccNo = Converter.GetLong(txtAccNo.Text);
                    objDTO.AccType = Converter.GetSmallInteger(txtAccType.Text);
                    objDTO.ChqPStatus = Converter.GetString(lblvalue2.Text);
                    objDTO.ChqPDate = Converter.GetString(lblvalue.Text);
                    int roweffect = A2ZCHQBOOKDTO.UpdateInformation(objDTO);
                    if (roweffect > 0)
                    {
                        clearinfo();
                    }



                }
            }

            catch (Exception ex)
            {
                throw ex;
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
            ddlCheBook.SelectedIndex = 0;
            ddlChqOption.SelectedIndex = 0;
            txtCheqLeafNo.Text = string.Empty;

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
                String cstext1 = "alert('Invalid Cheque Leaf No.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }





        protected void txtCheqLeafNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlquery = "SELECT * FROM A2ZCHQBOOK WHERE ChqeFx = '" + txtChqprefix.Text + "' AND ChqSlNo !< '" + txtCheqLeafNo.Text + "'";

                dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    lblSlNo.Text = Converter.GetString(dt.Rows[0]["ChqSlNo"]);
                    lblbPage.Text = Converter.GetString(dt.Rows[0]["ChqbPage"]);
                    int a = Converter.GetInteger(lblSlNo.Text);
                    int b = Converter.GetInteger(lblbPage.Text);
                    LeafNo = Converter.GetInteger(txtCheqLeafNo.Text);

                    int Qresult = Converter.GetInteger(a - b + 1);
                    if (LeafNo < Qresult || LeafNo > a)
                    {
                        ValidityChkPrfx();
                        txtCheqLeafNo.Text = string.Empty;
                        txtCheqLeafNo.Focus();
                    }
                    else
                    {
                        BegNo = (a - b + 1);
                        txtNumPage.Text = lblbPage.Text;
                        txtEndNo.Text = lblSlNo.Text;
                        txtBeginingNo.Text = Converter.GetString(BegNo);
                        ddlCheBook.Focus();

                    }
                }
                else
                {
                    ValidityChkPrfx();
                    txtCheqLeafNo.Text = string.Empty;
                    txtCheqLeafNo.Focus();
                }

            }

            catch (Exception ex)
            {

                throw ex;

            }


        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }

}