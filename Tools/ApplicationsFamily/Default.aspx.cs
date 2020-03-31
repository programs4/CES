using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ApplicationsFamily_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_ApplicationsFamily;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        #region FilterDList

        DListFltOrganizations.DataSource = DALC.GetOrganizations();
        DListFltOrganizations.DataBind();

        if (DListFltOrganizations.Items.Count > 1)
        {
            PnlFltOrganizations.Visible = GrdApplicationsFamily.Columns[2].Visible = true;
            DListFltOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListFltApplicationsFamilyTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyTypes);
        DListFltApplicationsFamilyTypes.DataBind();
        DListFltApplicationsFamilyTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFltApplicationsFamilyStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyStatus);
        DListFltApplicationsFamilyStatus.DataBind();
        DListFltApplicationsFamilyStatus.Items.Insert(0, new ListItem("--", "-1"));

        //DListFltApplicationsFamilyPartnersTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyPartnersTypes);
        //DListFltApplicationsFamilyPartnersTypes.DataBind();

        #endregion

    }

    private void BindGridApplications()
    {
        GrdApplicationsFamily.DataSource = null;
        GrdApplicationsFamily.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFltOrganizations.SelectedValue)},
            {"ID",TxtApplicationsFamilyID.Text},
            {"ApplicationsFamilyTypesID",int.Parse(DListFltApplicationsFamilyTypes.SelectedValue)},
            {"ApplicationsFamilyStatusID",int.Parse(DListFltApplicationsFamilyStatus.SelectedValue)},
            {"Tour_Dt(BETWEEN)",Config.DateTimeFilter(TxtFilterTourDt1.Text, TxtFilterTourDt2.Text)},
            {"Add_Dt(Between)",Config.DateTimeFilter(TxtFilterDate1.Text,TxtFilterDate2.Text)},
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
            Config.RedirectURL(string.Format("/tools/applicationsfamily/?p={0}", PageNum - 1));
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
        GrdApplicationsFamily.DataSource = FilterList.Dt;
        GrdApplicationsFamily.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Ailə_səfərləri))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Ailə səfərləri";
            BindDList();
            BindGridApplications();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/applicationsfamily/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

}