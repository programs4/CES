using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Tools_Gallery_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_Downloads;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListFilterOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterOrganizations.DataBind();

        if (DListFilterOrganizations.Items.Count > 1)
        {
            PnlFilterOrganizations.Visible = true;
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        var FilterDictionary = (Dictionary<string, object>)Session[TableName.ToString()];
        object OrganizationsID = null;
        if (FilterDictionary != null)
        {
            OrganizationsID = FilterDictionary[DListFilterOrganizations.ID];
        }

        DListFilterOrganizations_SelectedIndexChanged(OrganizationsID, null);

        DListFilterDownloadsQualityTypes.DataSource = DALC.GetList(Tools.Table.DownloadsQualityTypes);
        DListFilterDownloadsQualityTypes.DataBind();
        DListFilterDownloadsQualityTypes.Items.Insert(0, new ListItem("--", "-1"));

    }

    private void BindGallery()
    {
        RptFotoGallery.DataSource = null;
        RptFotoGallery.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue) },
            {"DataID(!=)",0 },
            {"DownloadsQualityTypesID",int.Parse(DListFilterDownloadsQualityTypes.SelectedValue) },
            {"DisplayName(LIKE)",TxtFilterFileName.Text },
            {"IsActive",int.Parse(DListFilterStatus.SelectedValue) },
        };

        if (PnlFilterDate.Visible == true)
        {
            FilterDictionary.Add("DownloadsTypesID", int.Parse(DListFilterDownloadsTypes.SelectedValue));
            FilterDictionary.Add("Years", int.Parse(DListFilterYears.SelectedValue));
            FilterDictionary.Add("Months", int.Parse(DListFilterMonths.SelectedValue));
        }


        int PageNum;
        int RowNumber = 20;
        if (!int.TryParse(Config._GetQueryString("p"), out PageNum))
        {
            PageNum = 1;
        }


        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult FilterList = DALC.GetFilterList(TableName, FilterDictionary, PageNum, RowNumber);

        if (FilterList.Count == -1)
        {
            return;
        }

        if (FilterList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/downloads/?p={0}", PageNum - 1));
        }

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

        RptFotoGallery.DataSource = FilterList.Dt;
        RptFotoGallery.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Qaleriya))
        {
            Config.RedirectURL("/tools");
            return;
        }


        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Foto qalereya";

            BindDList();
            BindGallery();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/gallery/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

    protected void BtnQuality_Click(object sender, EventArgs e)
    {
        Button Btn = (sender as Button);
        int DownloadsID = int.Parse(Btn.CommandName);
        int QualityTypesID = int.Parse(Btn.CommandArgument);

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("DownloadsQualityTypesID", QualityTypesID);
        dictionary.Add("WhereID", DownloadsID);

        int Check = DALC.UpdateDatabase(Tools.Table.Downloads, dictionary);

        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGallery();
    }

    protected void DListFilterOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        int OrganizationsID = int.Parse(DListFilterOrganizations.SelectedValue);
        if (e == null)
        {
            OrganizationsID = sender._ToInt16();
        }

        PnlFilterDate.Visible = false;
        DListFilterDownloadsTypes.DataSource = DALC.GetDataTableBySqlCommand("GetDownloadsTypesFromDownloads",
            new string[] { "OrganizationsID" },
            new object[] { OrganizationsID },
            CommandType.StoredProcedure);
        DListFilterDownloadsTypes.DataBind();

        if (DListFilterDownloadsTypes.Items.Count > 1)
        {
            DListFilterDownloadsTypes.Items.Insert(0, new ListItem("--", "-1"));
        }

        if (DListFilterDownloadsTypes.Items.Count > 0)
        {
            PnlFilterDate.Visible = true;
            DListFilterDownloadsTypes_SelectedIndexChanged(null, null);
        }
    }

    protected void DListFilterDownloadsTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        DListFilterYears.DataSource = DALC.GetDataTableBySqlCommand("GetYearsFromDownloads",
            new string[] { "OrganizationsID", "DownloadsTypesID" },
            new object[] { int.Parse(DListFilterOrganizations.SelectedValue), int.Parse(DListFilterDownloadsTypes.SelectedValue) },
            System.Data.CommandType.StoredProcedure);
        DListFilterYears.DataBind();

        if (DListFilterYears.Items.Count > 1)
        {
            DListFilterYears.Items.Insert(0, new ListItem("--", "-1"));
        }
        DListFilterYears_SelectedIndexChanged(null, null);
    }

    protected void DListFilterYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable DtMonths = new DataTable();
        DtMonths.Columns.Add("ID", typeof(int));
        DtMonths.Columns.Add("Name", typeof(string));

        DataTable Dt = DALC.GetDataTableBySqlCommand("GetMonthsFromDownloads",
           new string[] { "OrganizationsID", "DownloadsTypesID", "Year" },
           new object[] { int.Parse(DListFilterOrganizations.SelectedValue), int.Parse(DListFilterDownloadsTypes.SelectedValue), int.Parse(DListFilterYears.SelectedValue) },
           CommandType.StoredProcedure);

        foreach (DataRow Dr in Dt.Rows)
        {
            DtMonths.Rows.Add(Dr["ID"]._ToInt16(), Config.GetMonthName(Dr["ID"]._ToInt16()));
        }

        DListFilterMonths.DataSource = DtMonths;
        DListFilterMonths.DataBind();

        if (DListFilterMonths.Items.Count > 1)
        {
            DListFilterMonths.Items.Insert(0, new ListItem("--", "-1"));
        }
    }
}