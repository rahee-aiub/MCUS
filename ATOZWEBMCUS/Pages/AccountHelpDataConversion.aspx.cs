using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.BLL;

namespace ATOZWEBMCUS.Pages
{
    public partial class AccountHelpDataConversion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            MEMBERDATA();

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateMemberHelp", "A2ZCSMCUS"));
            if (result == 0)
            {

            }

            Successful();


        }


        private void Successful()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Data Conversion successfully completed.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }

        private void MEMBERDATA()
        {

            string sqlquery4 = "Truncate table dbo.A2ZMEMBERHELP";
            int resultM = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery4, "A2ZCSMCUS"));


            string qry4 = "SELECT CuType,CuNo,MemNo,MemName,MemOldCuNo,MemOldMemNo,MemOld1CuNo,MemOld1MemNo,MemOld2CuNo,MemOld2MemNo FROM A2ZMEMBER WHERE CuType !=0 AND CuNo !=0";
            DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZCSMCUS");
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt4.Rows)
                {
                    var CuType = dr2["CuType"].ToString();
                    var CuNo = dr2["CuNo"].ToString();
                    var MemNo = dr2["MemNo"].ToString();
                    var MemName = dr2["MemName"].ToString();
                    var OldCuNo = dr2["MemOldCuNo"].ToString();
                    var OldMemNo = dr2["MemOldMemNo"].ToString();
                    var Old1CuNo = dr2["MemOld1CuNo"].ToString();
                    var Old1MemNo = dr2["MemOld1MemNo"].ToString();
                    var Old2CuNo = dr2["MemOld2CuNo"].ToString();
                    var Old2MemNo = dr2["MemOld2MemNo"].ToString();

                    MemName = (MemName != null) ? MemName.Trim().Replace("'", "''") : "";


                    string qry5 = "SELECT AccType,AccNo,AccOldNumber FROM A2ZACCOUNT WHERE AccType !=99 AND CuType = '" + CuType + "' AND CuNo = '" + CuNo + "' AND MemNo = '" + MemNo + "'";
                    DataTable dt5 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry5, "A2ZCSMCUS");
                    if (dt5.Rows.Count > 0)
                    {
                        foreach (DataRow dr3 in dt5.Rows)
                        {
                            var AccType = dr3["AccType"].ToString();
                            var AccNo = dr3["AccNo"].ToString();
                            var AccOldNumber = dr3["AccOldNumber"].ToString();

                            if (OldCuNo != "")
                            {
                                if (OldMemNo != "")
                                {
                                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBERHELP(OldCuNo,CuType,CuNo,OldMemNo,MemNo,MemName,AccType,OldAccNumber,AccNo) VALUES('" + OldCuNo + "','" + CuType + "','" + CuNo + "','" + OldMemNo + "','" + MemNo + "','" + MemName + "','" + AccType + "','" + AccOldNumber + "','" + AccNo + "')";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                }
                            }


                            if (Old1CuNo != "")
                            {
                                if (Old1MemNo != "")
                                {
                                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBERHELP(OldCuNo,CuType,CuNo,OldMemNo,MemNo,MemName,AccType,OldAccNumber,AccNo) VALUES('" + Old1CuNo + "','" + CuType + "','" + CuNo + "','" + Old1MemNo + "','" + MemNo + "','" + MemName + "','" + AccType + "','" + AccOldNumber + "','" + AccNo + "')";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                }
                            }

                            if (Old2CuNo != "")
                            {
                                if (Old2MemNo != "")
                                {
                                    string strQuery = "INSERT INTO A2ZCSMCUS.dbo.A2ZMEMBERHELP(OldCuNo,CuType,CuNo,OldMemNo,MemNo,MemName,AccType,OldAccNumber,AccNo) VALUES('" + Old2CuNo + "','" + CuType + "','" + CuNo + "','" + Old2MemNo + "','" + MemNo + "','" + MemName + "','" + AccType + "','" + AccOldNumber + "','" + AccNo + "')";
                                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
                                }
                            }

                        }
                    }

                }
            }
        }
    }
}