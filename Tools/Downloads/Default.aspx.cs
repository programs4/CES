using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Downloads_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_Downloads;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListFilterDownloadsTypes.DataSource = DALC.GetList(Tools.Table.DownloadsTypes);
        DListFilterDownloadsTypes.DataBind();
        if (DListFilterDownloadsTypes.Items.Count > 1)
        {
            DListFilterDownloadsTypes.Items.Insert(0, new ListItem("--", "-1"));
        }
    }

    private void BindGrid()
    {
        GrdDownloads.DataSource = null;
        GrdDownloads.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
            {"DownloadsTypesID",DListFilterDownloadsTypes.SelectedValue },
            {"DisplayName(LIKE)",TxtFilterFileName.Text },
            {"IsActive",int.Parse(DListFilterStatus.SelectedValue) },
        };


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

        LblCount.Text = string.Format(LblCount.Text, FilterList.Count);

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

        GrdDownloads.DataSource = FilterList.Dt;
        GrdDownloads.DataBind();

        GrdDownloads.Columns[GrdDownloads.Columns.Count - 1].Visible = DALC._GetUsersLogin.OrganizationsParentID == 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Yükləmələr))
        {
            Config.RedirectURL("/tools");
            return;
        }

        PnlAddFile.Visible = PnlStatus.Visible = DALC._GetUsersLogin.OrganizationsParentID == 0;

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Yükləmələr";
            BindDList();
            BindGrid();
        }
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {

    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/downloads/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }
}