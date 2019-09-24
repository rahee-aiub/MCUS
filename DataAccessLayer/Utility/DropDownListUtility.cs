using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DataAccessLayer.Utility
{

    /// <summary>
    /// <remarks>
    /// e.g.: How to use this class in a aspx or ascx page 
    /// DropDownListUtility dropDownListUtilityObj = new DropDownListUtility();
    /// DataTable dt = getDT();
    /// dropDownListUtilityObj.FillDropDownList(productDropDownList, dt, "ProductInfo", "ID", 0, "- Select -");
    /// </remarks
    /// </summary>
    public class DropDownListUtility
    {
        private static DataTable AddOneRowToDataTable(DataTable dt, string dataTextField, string dataValueField, int newValue, string newText)
        {
            try
            {
                DataTable table = new DataTable();
                DataTable table2 = new DataTable();
                table = dt;
                table2.Columns.Add(dataValueField);
                table2.Columns.Add(dataTextField);
                DataRow row = table2.NewRow();
                row[dataValueField] = newValue;
                row[dataTextField] = newText;
                table2.Rows.Add(row);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = table2.NewRow();
                    row[dataValueField] = table.Rows[i][dataValueField];
                    row[dataTextField] = table.Rows[i][dataTextField];
                    table2.Rows.Add(row);
                }
                return table2;
            }
            catch (Exception ex)
            {
                return dt;
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlName"></param>
        /// <param name="dt"></param>
        /// <param name="dataTextField"></param>
        /// <param name="dataValueField"></param>
        public void FillDropDownList(DropDownList ddlName, DataTable dt, string dataTextField, string dataValueField)
        {
            try
            {
                ddlName.DataTextField = dataTextField;
                ddlName.DataValueField = dataValueField;
                ddlName.DataSource = dt;
                ddlName.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlName"></param>
        /// <param name="dt"></param>
        /// <param name="dataTextField"></param>
        /// <param name="dataValueField"></param>
        /// <param name="newValue"></param>
        /// <param name="newText"></param>
        public void FillDropDownList(DropDownList ddlName, DataTable dt, string dataTextField, string dataValueField, int newValue, string newText)
        {
            try
            {
                dt = AddOneRowToDataTable(dt, dataTextField, dataValueField, newValue, newText);
                ddlName.DataTextField = dataTextField;
                ddlName.DataValueField = dataValueField;
                ddlName.DataSource = dt;
                ddlName.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlYear"></param>
        /// <param name="presentYear"></param>
        /// <param name="fromYear"></param>
        /// <param name="endYear"></param>
        public void FillYearDropDownList(DropDownList ddlYear, int presentYear, int fromYear, int endYear)
        {
            try
            {
                if (ddlYear.Items.Count > 0)
                    ddlYear.Items.Clear();
                for (int year = fromYear; year >= endYear; year--)
                {
                    ListItem l = new ListItem(year.ToString(), year.ToString(), true);
                    ddlYear.Items.Add(l);
                }
                ddlYear.SelectedValue = presentYear.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlMonth"></param>
        /// <param name="presentMonth"></param>
        public void FillMonthDropDownList(DropDownList ddlMonth, int presentMonth)
        {
            try
            {
                if (ddlMonth.Items.Count > 0)
                    ddlMonth.Items.Clear();
                for (int month = 1; month <= 12; month++)
                {
                    ListItem l = new ListItem(months[month - 1], month.ToString(), true);
                    ddlMonth.Items.Add(l);
                }
                ddlMonth.SelectedValue = presentMonth.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlDay"></param>
        /// <param name="toDay"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void FillDayDropDownList(DropDownList ddlDay, int toDay, int month, int year)
        {
            try
            {
                if (ddlDay.Items.Count > 0)
                    ddlDay.Items.Clear();

                int startDay = 1;
                int endDay = 28;

                switch (month)
                {
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        endDay = 30;
                        break;
                    case 2:
                        bool isLeapYear = DateTime.IsLeapYear(year);
                        if (isLeapYear)
                            endDay = 29;
                        break;
                    default:
                        endDay = 31;
                        break;
                }

                for (int day = startDay; day <= endDay; day++)
                {
                    ListItem l = new ListItem(day.ToString(), day.ToString(), true);
                    ddlDay.Items.Add(l);
                }
                ddlDay.SelectedValue = toDay.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlDay"></param>
        /// <param name="ddlMonth"></param>
        /// <param name="ddlYear"></param>
        /// <returns></returns>
        public DateTime GetDate(DropDownList ddlDay, DropDownList ddlMonth, DropDownList ddlYear)
        {
            try
            {
                return Convert.ToDateTime(string.Format("{0} {1}, {2}", ddlDay.SelectedValue, ddlMonth.SelectedItem.Text, ddlYear.SelectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
