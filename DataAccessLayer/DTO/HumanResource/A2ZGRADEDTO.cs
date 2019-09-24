using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZGRADEDTO
    {
        #region Properties

        public Int16 ID { set; get; }
        public int Grade { set; get; }
        public String GradeDesc { set; get; }
        public Int16 GradeFlag { set; get; }
        public Int16 GradeType { set; get; }
        public Int16 GradeStatus { set; get; }
        public Int16 UserId { set; get; }
        public DateTime CreateDate { set; get; }

        #endregion

        public static A2ZGRADEDTO GetGradeInformation(int Grade)
        {
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(
            //    "SELECT * FROM A2ZGRADE WHERE ID = " + ID, "A2ZERPHR");
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery(
                "SELECT * FROM A2ZGRADE WHERE Grade ='" + Grade + "'", "A2ZHRMCUS");


            var p = new A2ZGRADEDTO();

            if (dt.Rows.Count > 0)
            {
                p.ID = Converter.GetSmallInteger(dt.Rows[0]["ID"]);
                p.Grade = Converter.GetInteger(dt.Rows[0]["Grade"]);
                p.GradeDesc = Converter.GetString(dt.Rows[0]["GradeDesc"]);

                p.GradeFlag = Converter.GetSmallInteger(dt.Rows[0]["GradeFlag"]);
                p.GradeType = Converter.GetSmallInteger(dt.Rows[0]["GradeType"]);
                p.GradeStatus = Converter.GetSmallInteger(dt.Rows[0]["GradeStatus"]);

                p.UserId = Converter.GetSmallInteger(dt.Rows[0]["UserId"]);
                p.CreateDate = Converter.GetDateTime(dt.Rows[0]["CreateDate"]);



                return p;
            }

            p.ID = 0;

            return p;
        }

        public static DropDownList GetDropDownList(DropDownList ddlGrade)
        {
            return BLL.CommonManager.Instance.FillDropDownList("SELECT ID,Grade FROM A2ZGRADE", ddlGrade, "A2ZHRMCUS");
        }

        public static int InsertGradeInformation(A2ZGRADEDTO dto)
        {
            int rowEffect = 0;

            string strQuery = "INSERT INTO A2ZGRADE(Grade,GradeDesc,UserId)VALUES('" + dto.Grade + "','" + dto.GradeDesc + "','"+dto.UserId+"')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            return rowEffect;
        }

        public static int UpdateGradeInformation(A2ZGRADEDTO dto)
        {
            string strQuery = "UPDATE A2ZGRADE SET GradeDesc ='" + dto.GradeDesc + "',UserId='"+dto.UserId+"' WHERE Grade='" + dto.Grade + "'";
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

        }

    }
}
