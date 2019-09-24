using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSGenerateMiscellaneousAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProceedMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Process Completed');", true);
            return;

        }
        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateMiscellaneousAccount", "A2ZCSMCUS"));

            if (result == 0)
            {
                ProceedMSG();
            }




            //string qry1 = "SELECT CuType,CuNo FROM A2ZMEMBER WHERE MemNo=0";
            //DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
            //if (dt1.Rows.Count > 0)
            //{
            //    foreach (DataRow dr1 in dt1.Rows)
            //    {
            //        var CuType = dr1["CuType"].ToString();
            //        var CuNo = dr1["CuNo"].ToString();

            //        lblCuType.Text = Converter.GetString(CuType);
            //        lblCuNum.Text = Converter.GetString(CuNo);


            //        string qry2 = "SELECT PayType  FROM A2ZPAYTYPE WHERE AtyClass=7";
            //        DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZCSMCUS");

            //        if (dt2.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr2 in dt2.Rows)
            //            {
            //                var paytype = dr2["PayType"].ToString();
            //                lblptype.Text = Converter.GetString(paytype);

            //                GenerateNewAccNo();

            //                string qry3 = "SELECT AccNo FROM A2ZACCOUNT WHERE  AccNo='" + txtAccNo.Text + "'";
            //                DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZCSMCUS");

            //                if(dt3.Rows.Count==0)
            //                {
            //                    string strQuery = "INSERT INTO A2ZACCOUNT(AccType,CuType,CuNo,MemNo,AccNo)VALUES(99,'" + lblCuType.Text + "','" + lblCuNum.Text + "',0,'" + txtAccNo.Text + "') ";
            //                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                
            //                }
            //                else
            //                {
                              
            //                }
            //            }
            //        }
            //    }
            //}
            
        }


        //protected void GenerateNewAccNo()
        //{

        //    string input1 = Converter.GetString(lblCuNum.Text).Length.ToString();
           
        //    string result1 = "";
           
        //    if (input1 == "1")
        //    {
        //        result1 = "000";
        //    }
        //    if (input1 == "2")
        //    {
        //        result1 = "00";
        //    }
        //    if (input1 == "3")
        //    {
        //        result1 = "0";
        //    }

        //    if (input1 != "4")
        //    {
        //        txtAccNo.Text = "99" + lblCuType.Text + result1 + lblCuNum.Text + "0000" +  "0" + lblptype.Text;
        //    }

        //    if (input1 == "4")
        //    {
        //        txtAccNo.Text = "99" + lblCuType.Text + result1 + lblCuNum.Text + "0000" + "0" + lblptype.Text;
        //    }
         
          

        //}

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}