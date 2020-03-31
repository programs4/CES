using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;


public class ReportGenerate
{

    public static string GenerateReports(DataTable Dt, string ParentColumn, string InVisibleColumns)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            return "";
        }

        if (string.IsNullOrEmpty(ParentColumn) || string.IsNullOrEmpty(InVisibleColumns))
        {
            return "";
        }

        StringBuilder tbody = new StringBuilder();
        bool IsDownIcon = false;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row[ParentColumn]._ToInt32() == 0)
            {
                if (tbody.Length != 0)
                {
                    tbody.Append("</tbody>");
                }

                tbody.Append("<tbody class=\"inner-table-holder\">");
                tbody.Append("<tr class=\"new-table\">");
            }
            else
            {
                tbody.Append("<tr>");
            }
            foreach (DataColumn Column in Dt.Columns)
            {
                if (InVisibleColumns.IndexOf(Column.ColumnName) == -1)
                {
                    if (Row[ParentColumn]._ToInt32() == 0)
                    {
                        tbody.Append("<th>");
                        tbody.Append(Row[Column]);
                        if (!IsDownIcon)
                        {
                            tbody.Append(" <i class=\"fa fa-angle-down\" aria-hidden=\"true\"></i>");
                            IsDownIcon = true;
                        }

                        tbody.Append("</th>");
                    }
                    else
                    {  
                        //Xidmetler hesabati uchun
                        if (Row.Table.Columns.Contains("Level") && Row.Table.Columns.Contains("Priority"))
                        {
                            
                            if(Row["Priority"]._ToInt32() == 0)
                            {
                                tbody.Append(string.Format("<td><div class=\"level{0}\">", Row["Level"]));
                            }
                            else
                            {
                                tbody.Append("<td><div>");
                            }
                        }
                        //Telim/Tedbir hesabati uchun
                        else if (Row.Table.Columns.Contains("Level") )
                        {
                            if (Row["Level"]._ToInt32() != 2)
                            {
                                tbody.Append(string.Format("<td><div class=\"level{0}\">", Row["Level"]));
                            }
                            else
                            {
                                tbody.Append("<td><div>");
                            }
                        }
                        //Digərləri
                        else
                        {                            
                            tbody.Append("<td><div>");
                        }

                        tbody.Append(Row[Column]);
                        tbody.Append("</div></td>");
                    }
                }
            }
            IsDownIcon = false;
            tbody.Append("</tr>");
        }

        return tbody.Append("</tbody>").ToString();
    }

    public static string GetReports
    (
        string ProcedureName,
        Tools.ReportsTypes ReportsTypes,
        StringBuilder Organizations,
        StringBuilder Years,
        StringBuilder Months,
        string ParentColumn,
        string InVisibleColumns
    )
    {
        if (Organizations.Length < 1 || Years.Length < 1)
        {
            return "";
        }

        DataTable Dt = new DataTable();
        if (ReportsTypes == Tools.ReportsTypes.Aylıq_hesabat)
        {
            if (Months.Length < 1)
            {
                return "";
            }
            Dt = DALC.GetReportsMonthly(ProcedureName, Organizations.ToString().Trim(','), int.Parse(Years.ToString()), Months.ToString().Trim(','));
        }
        else
        {
            Dt = DALC.GetReportsYearly(ProcedureName, Organizations.ToString().Trim(','), Years.ToString().Trim(','));
        }

        return GenerateReports(Dt, ParentColumn, InVisibleColumns);
    }

    public static string GetReports(string ProcedureName, string ParentColumn, string InVisibleColumns)
    {
        DataTable Dt = DALC.GetDataTableBySqlCommand(ProcedureName, null, null, CommandType.StoredProcedure);

        return GenerateReports(Dt, ParentColumn, InVisibleColumns);
    }

}