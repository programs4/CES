using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ApplicationsReports_Default : System.Web.UI.Page
{

    private void BindList()
    {
        DListFilterMultiOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterMultiOrganizations.DataBind();
        for (int i = 0; i < DListFilterMultiOrganizations.Items.Count; i++)
        {
            DListFilterMultiOrganizations.Items[i].Selected = true;
        }
        PnlOrganizations.Visible = DListFilterMultiOrganizations.Items.Count > 1;


        DListFilterReportsTypes.DataSource = DALC.GetList(Tools.Table.ReportsTypes);
        DListFilterReportsTypes.DataBind();
        DListFilterReportsTypes_SelectedIndexChanged(null, null);

        DListFilterMultiYears.DataSource =
        DListFilterYears.DataSource = DALC.GetYearsByTableName();
        DListFilterMultiYears.DataBind();
        DListFilterYears.DataBind();

        if (DListFilterYears.Items.Count > 0)
        {
            DListFilterYears.SelectedIndex = DListFilterYears.Items.Count - 1;
            DListFilterYears_SelectedIndexChanged(null, null);
        }

    }

    //private string GenerateReports(Tools.ReportsTypes ReportsTypes,
    //                                string ProcedureName,
    //                                string ParentColumn,
    //                                string InVisibleColumns)
    //{
    //    StringBuilder Organizations = new StringBuilder();
    //    StringBuilder Months = new StringBuilder();
    //    StringBuilder Years = new StringBuilder();

    //    for (int i = 0; i < DListFilterMultiOrganizations.Items.Count; i++)
    //    {
    //        if (DListFilterMultiOrganizations.Items[i].Selected)
    //            Organizations.Append(DListFilterMultiOrganizations.Items[i].Value + ",");
    //    }

    //    DataTable Dt = new DataTable();
    //    if (ReportsTypes == Tools.ReportsTypes.Aylıq_hesabat)
    //    {
    //        for (int i = 0; i < DListFilterMultiMonths.Items.Count; i++)
    //        {
    //            if (DListFilterMultiMonths.Items[i].Selected)
    //                Months.Append(DListFilterMultiMonths.Items[i].Value + ",");
    //        }

    //        Dt = DALC.GetReportsMonthly(ProcedureName, Organizations.ToString().Trim(','), int.Parse(DListFilterYears.SelectedValue), Months.ToString().Trim(','));
    //    }
    //    else
    //    {
    //        for (int i = 0; i < DListFilterMultiMonths.Items.Count; i++)
    //        {
    //            if (DListFilterMultiYears.Items[i].Selected)
    //                Years.Append(DListFilterMultiYears.Items[i].Value + ",");
    //        }

    //        Dt = DALC.GetReportsYearly(ProcedureName, Organizations.ToString().Trim(','), Years.ToString().Trim(','));

    //    }

    //    if (Dt == null || Dt.Rows.Count < 1)
    //        return "";

    //    StringBuilder tbody = new StringBuilder();
    //    int Count = 0;

    //    foreach (DataRow Row in Dt.Rows)
    //    {
    //        if (Row[ParentColumn]._ToInt32() == 0)
    //        {
    //            if (tbody.Length != 0)
    //                tbody.Append("</tbody>");

    //            tbody.Append("<tbody class=\"inner-table-holder\">");
    //            tbody.Append("<tr class=\"new-table\">");
    //        }
    //        else
    //        {
    //            tbody.Append("<tr>");
    //        }
    //        foreach (DataColumn Column in Dt.Columns)
    //        {
    //            if (ParentColumn.IndexOf(Column.ColumnName) == -1)
    //            {

    //                if (Row[ParentColumn]._ToInt32() == 0)
    //                {
    //                    tbody.Append("<th>");
    //                    tbody.Append(Row[Column]);
    //                    if (Count == 1)
    //                    {
    //                        tbody.Append(" <i class=\"fa fa-angle-down\" aria-hidden=\"true\"></i>");

    //                    }

    //                    tbody.Append("</th>");
    //                }
    //                else
    //                {
    //                    tbody.Append("<td><div>");
    //                    tbody.Append(Row[Column]);
    //                    tbody.Append("</div></td>");
    //                }
    //            }
    //            Count++;
    //        }
    //        Count = 0;
    //        tbody.Append("</tr>");
    //    }

    //    return tbody.Append("</tbody>").ToString();

    //}

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "$('.multiSelectAll').multiselect({buttonWidth: '100%',includeSelectAllOption: true,});", true);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Report", "Report();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Hesabatlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Hesabatlar";
            BindList();
            BtnFilter_Click(null, null);
        }

    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        LoadScriptManager();

        LtrOrganizationsMonths.Text = LtrReportsOrganizations.Text =
        LtrReportsSosialStaus.Text = LtrReportsApplicationsFamily.Text =
        LtrReportsCase.Text = LtrReportsServices.Text = LtrReportsEvents.Text = "";

        StringBuilder th = new StringBuilder();
        StringBuilder Organizations = new StringBuilder();
        StringBuilder Months = new StringBuilder();
        StringBuilder Years = new StringBuilder();

        for (int i = 0; i < DListFilterMultiOrganizations.Items.Count; i++)
        {
            if (DListFilterMultiOrganizations.Items[i].Selected)
                Organizations.Append(DListFilterMultiOrganizations.Items[i].Value + ",");
        }

        if (Organizations.Length < 1)
        {
            Config.MsgBoxAjax("Heç bir qurum seçilməyib");
            return;
        }

        if (int.Parse(DListFilterReportsTypes.SelectedValue) == (int)Tools.ReportsTypes.Aylıq_hesabat)
        {
            Years.Append(DListFilterYears.SelectedValue);

            for (int i = 0; i < DListFilterMultiMonths.Items.Count; i++)
            {
                if (DListFilterMultiMonths.Items[i].Selected)
                {
                    Months.Append(DListFilterMultiMonths.Items[i].Value + ",");
                    th.AppendFormat("<th>{0}</th>", Config.GetMonthShortName(int.Parse(DListFilterMultiMonths.Items[i].Value)));
                }
            }

            if (Months.Length < 1)
            {
                Config.MsgBoxAjax("Heç bir ay seçilməyib");
                return;
            }

            LtrReportsOrganizations.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsForOrganizations", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "ID", "ID");
            LtrReportsOrganizations.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsPersonsForGender", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "Gender,Priority");

            LtrReportsServices.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsPersonsServices", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "ParentID", "ServicesID,Priority,ParentID,Level,Rownumber");
            LtrReportsSosialStaus.Text += ReportGenerate.GetReports("ReportsMonthlySosialStatus", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");

            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsCaseOpening", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsCaseOpeningForTypes", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsCaseClosing", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsCaseClosingForTypes", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            
            LtrReportsApplicationsFamily.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsFamilyForStatus", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsApplicationsFamily.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsApplicationsFamily.Text += ReportGenerate.GetReports("ReportsMonthlyApplicationsFamilyForTypes", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");

            LtrReportsEvents.Text += ReportGenerate.GetReports("ReportsMonthlyEventsForPolicyTypes", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsEvents.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsEvents.Text += ReportGenerate.GetReports("ReportsMonthlyEventsForOrganizations", Tools.ReportsTypes.Aylıq_hesabat, Organizations, Years, Months, "Level", "ID,Level");

        }
        else if (int.Parse(DListFilterReportsTypes.SelectedValue) == (int)Tools.ReportsTypes.İllik_hesabat)
        {
            for (int i = 0; i < DListFilterMultiYears.Items.Count; i++)
            {
                if (DListFilterMultiYears.Items[i].Selected)
                {
                    Years.Append(DListFilterMultiYears.Items[i].Value + ",");
                    th.AppendFormat("<th>{0}</th>", DListFilterMultiYears.Items[i].Value);
                }
            }

            if (Years.Length < 1)
            {
                Config.MsgBoxAjax("Heç bir il seçilməyib");
                return;
            }

            LtrReportsOrganizations.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsForOrganizations", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "ID", "ID");
            LtrReportsOrganizations.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsPersonsForGender", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "Gender,Priority");

            LtrReportsServices.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsPersonsServices", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "ParentID", "ServicesID,Priority,ParentID,Level,Rownumber");

            LtrReportsSosialStaus.Text += ReportGenerate.GetReports("ReportsYearlySosialStatus", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");

            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsCaseOpening", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsCaseOpeningForTypes", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsCaseClosing", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsCase.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsCaseClosingForTypes", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");

            LtrReportsApplicationsFamily.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsFamilyForStatus", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsApplicationsFamily.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsApplicationsFamily.Text += ReportGenerate.GetReports("ReportsYearlyApplicationsFamilyForTypes", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");

            LtrReportsEvents.Text += ReportGenerate.GetReports("ReportsYearlyEventsForPolicyTypes", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Priority", "ID,Priority");
            LtrReportsEvents.Text += "<tbody class=\"gap\"></tbody>";
            LtrReportsEvents.Text += ReportGenerate.GetReports("ReportsYearlyEventsForOrganizations", Tools.ReportsTypes.İllik_hesabat, Organizations, Years, Months, "Level", "ID,Level");

        }



        LtrOrganizationsMonths.Text = LtrSosialStatusMonths.Text =
        LtrApplicationFammilyMonths.Text = LtrCaseMonths.Text =
        LtrServicesMonths.Text = LtrEventsMonths.Text = th.ToString();
    }

    protected void lnkDetails_Click(object sender, EventArgs e)
    {

    }

    protected void DListFilterYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadScriptManager();
        if (DListFilterYears.SelectedValue != "-1")
        {
            DListFilterMultiMonths.DataSource = Config.GetMonths(int.Parse(DListFilterYears.SelectedValue));
            DListFilterMultiMonths.DataBind();

            if (sender == null)
            {
                for (int i = 0; i < DListFilterMultiMonths.Items.Count; i++)
                {
                    DListFilterMultiMonths.Items[i].Selected = true;
                }
            }
        }
    }

    protected void DListFilterReportsTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadScriptManager();
        PnlFilterMultiYears.Visible = PnlFilterYears.Visible =
        PnlFilterMultiMonths.Visible = false;

        switch (int.Parse(DListFilterReportsTypes.SelectedValue))
        {
            case (int)Tools.ReportsTypes.Aylıq_hesabat:
                PnlFilterYears.Visible = PnlFilterMultiMonths.Visible = true;
                break;
            case (int)Tools.ReportsTypes.İllik_hesabat:
                PnlFilterMultiYears.Visible = true;
                break;
            default:
                break;
        }

    }

    protected void GrdReportsApllicationsPersonsServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            int Priority = rowView["Priority"]._ToInt32();

            switch (Priority)
            {
                case 0:
                    e.Row.CssClass = "grdRowTopStyle";
                    break;
                default:
                    e.Row.Cells[0].CssClass = "grdRowSubStyle";
                    break;
            }

        }
    }


}