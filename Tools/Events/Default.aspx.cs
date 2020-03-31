using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Events_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_Events;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListFilterEventsTypes.DataSource = DALC.GetList(Tools.Table.EventsTypes);
        DListFilterEventsTypes.DataBind();
        DListFilterEventsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterOrganizations.DataBind();

        if (DListFilterOrganizations.Items.Count > 1)
        {
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListFilterEventsDirectionTypes.DataSource = DALC.GetList(Tools.Table.EventsDirectionTypes);
        DListFilterEventsDirectionTypes.DataBind();
        DListFilterEventsDirectionTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterEventsPolicyTypes.DataSource = DALC.GetList(Tools.Table.EventsPolicyTypes);
        DListFilterEventsPolicyTypes.DataBind();
        DListFilterEventsPolicyTypes.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindGrdEvents()
    {
        GrdEvents.DataSource = null;
        GrdEvents.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);
        #region BetweenDateFromat

        string Date = "";

        string Dt1 = "20170101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtFilterEvents_StartDt.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtFilterEvents_EndDt.Text.Trim());

        if (DateFilter1 == null && DateFilter2 == null)
        {
            Date = "";
        }
        else
        {
            if (DateFilter1 != null)
            {
                Dt1 = ((DateTime)DateFilter1).ToString("yyyyMMdd");
            }

            if (DateFilter2 != null)
            {
                Dt2 = ((DateTime)DateFilter2).ToString("yyyyMMdd");
            }

            Date = Dt1 + "&" + Dt2;
        }
        #endregion

        var Dictionary = new Dictionary<string, object>()
        {
            {"ID",TxtFilterEventsID.Text},
            {"EventsTypesID",int.Parse(DListFilterEventsTypes.SelectedValue)},
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue)},
            {"EventsDirectionTypesID",int.Parse(DListFilterEventsDirectionTypes.SelectedValue)},
            {"EventsPolicyTypesID",int.Parse(DListFilterEventsPolicyTypes.SelectedValue)},
            {"Name(LIKE)",TxtFilterName.Text},
            {"Subject(LIKE)",TxtFilterSubject.Text},
            {"Place(LIKE)",TxtFilterPlace.Text},
            {"Organizer(LIKE)",TxtFilterOrganizer.Text},
            {"Events_StartDt(BETWEEN)", Config.DateTimeFilter(TxtFilterEvents_StartDt.Text,TxtFilterEvents_EndDt.Text)},
            {"Events_EndDt(BETWEEN)",Config.DateTimeFilter(TxtFilterEvents_StartDt.Text,TxtFilterEvents_EndDt.Text)},
        };

        int PageNum;
        int RowNumber = 20;
        if (!int.TryParse(Config._GetQueryString("p"), out PageNum))
        {
            PageNum = 1;
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult FilterList = DALC.GetFilterList(TableName, Dictionary, PageNum, RowNumber);

        if (FilterList.Count == -1)
        {
            return;
        }

        if (FilterList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/events/?p={0}", PageNum - 1));
        }

        LblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", FilterList.Count);
        int Total_Count = 0;
        if (FilterList.Count % RowNumber > 0)
        {
            Total_Count = (FilterList.Count / RowNumber) + 1;
        }
        else
        {
            Total_Count = FilterList.Count / RowNumber;
        }
        HdnTotalCount.Value = Total_Count.ToString();
        PnlPager.Visible = FilterList.Count > RowNumber;
        GrdEvents.DataSource = FilterList.Dt;
        GrdEvents.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Təlim_və_tədbirlər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Təlimlər və tədbirlər";
            BindDList();
            BindGrdEvents();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/events/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

}