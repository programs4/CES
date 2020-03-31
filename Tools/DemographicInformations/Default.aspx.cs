using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_DemographicInformations_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_DemographicInformations;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListFilterOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterOrganizations.DataBind();

        if (DListFilterOrganizations.Items.Count > 1)
        {
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }
    }

    private void BindGrdDemographicInformations()
    {
        GrdDemographicInformations.DataSource = null;
        GrdDemographicInformations.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);
        #region BetweenDateFromat

        string Date = "";

        string Dt1 = "20170101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtFilterCreate_DtStart.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtFilterCreate_DtEnd.Text.Trim());

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
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue)},
            {"Create_Dt(BETWEEN)", Config.DateTimeFilter(TxtFilterCreate_DtStart.Text,TxtFilterCreate_DtEnd.Text)},
            {"IsDeleted", 0},
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
            Config.RedirectURL(string.Format("/tools/demographicinformations/?p={0}", PageNum - 1));
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
        GrdDemographicInformations.DataSource = FilterList.Dt;
        GrdDemographicInformations.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Demoqrafik_məlumatlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Demoqrafik məlumatlar";
            BindDList();
            BindGrdDemographicInformations();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/demographicinformations/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            {"IsDeleted", true},
            {"WhereID", int.Parse((sender as LinkButton).CommandArgument)},
        };

        int Check = DALC.UpdateDatabase(Tools.Table.DemographicInformations, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdDemographicInformations();
    }
}