using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ElectronicRegistry_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_ServicesCourses;
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

    private void BindGrdServicesCourses()
    {
        GrdServicesCourses.DataSource = null;
        GrdServicesCourses.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);
        #region BetweenDateFromat
        
        string Start_Dt = "";
        string End_Dt = "";

        object DateFilterStart_Dt = Config.DateTimeFormat(TxtFilterStart_Dt.Text.Trim());
        object DateFilterEnd_Dt = Config.DateTimeFormat(TxtFilterEnd_Dt.Text.Trim());


        if (DateFilterStart_Dt != null)
        {
            Start_Dt = ((DateTime)DateFilterStart_Dt).ToString("yyyyMMdd");
        }

        if (DateFilterEnd_Dt != null)
        {
            End_Dt = ((DateTime)DateFilterEnd_Dt).ToString("yyyyMMdd");
        }

        #endregion

        var Dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue)},
            {"Start_Dt(>=)",Start_Dt },
            {"End_Dt(<=)",End_Dt }
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
            Config.RedirectURL(string.Format("/tools/electronicregistry/?p={0}", PageNum - 1));
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
        GrdServicesCourses.DataSource = FilterList.Dt;
        GrdServicesCourses.DataBind();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Elektron_jurnal))
        {
            Config.RedirectURL("/tools");
            return;
        }
      

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Elektron jurnal";

            BindDList();
            BindGrdServicesCourses();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/electronicregistry/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

    protected void LnkBeginLesson_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = sender as LinkButton;
        LtrTitle.Text = "Dərsə başla";
        BtnAddLessons.CommandArgument = Lnk.CommandArgument;

        if (Lnk.Attributes["data-iscomplate"] == "0")
        {
            Config.RedirectURL(string.Format("/tools/electronicregistry/lessons/?i={0}", (Lnk.CommandArgument + "-" + Lnk.Attributes["data-servicescourseslessonsid"] + "-" + DALC._GetUsersLogin.Key).Encrypt()));
            return;
        }

        DListTeacherUsers.DataSource = DALC.GetUsersByOrganizationsID(int.Parse(Lnk.CommandName));
        DListTeacherUsers.DataBind();
        DListTeacherUsers.SelectedValue = Lnk.Attributes["data-teacherusersid"];

        DListServicesCoursesPlans.DataSource = DALC.GetServicesCoursesPlans(int.Parse(Lnk.CommandArgument));
        DListServicesCoursesPlans.DataBind();
        DListServicesCoursesPlans.Items.Insert(0, new ListItem("--", "-1"));

        TxtCreate_Dt.Text = DateTime.Now.ToString("dd.MM.yyyy");

        GrdServicesCoursesPlans.DataSource = DALC.GetServicesCoursesPlans(int.Parse(Lnk.CommandArgument));
        GrdServicesCoursesPlans.DataBind();

        Config.Modal();
    }

    protected void BtnAddLessons_Click(object sender, EventArgs e)
    {
        if (DListServicesCoursesPlans.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Mövzunu seçin!");
            return;
        }

        if (DListTeacherUsers.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Müəllim seçin!");
            return;
        }

        object Date = Config.DateTimeFormat(TxtCreate_Dt.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün qeyd edin.");
            return;
        }
        TxtCreate_Dt.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        int ServicesCoursesID = int.Parse(BtnAddLessons.CommandArgument);

        //Check
        int Count = DALC.CheckServicesCoursesLessons(ServicesCoursesID, ((DateTime)Date).ToString("yyyy-MM-dd"));
        if (Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (Count > 0)
        {
            Config.MsgBoxAjax("Bu tarixdə dərs yaradılıb!");
            return;
        }

        int ServicesCoursesLessonsID = DALC.ExecuteProcedure("InsertServicesCoursesLessons",
            "ServicesCoursesID,ServicesCoursesPlansID,TeacherUsersID,Create_Dt,Description,Date,Add_Ip",
            new object[]
            {
                ServicesCoursesID,
                int.Parse(DListServicesCoursesPlans.SelectedValue),
                int.Parse(DListTeacherUsers.SelectedValue),
                Date,
                TxtDescription.Text,
                DateTime.Now,
                Request.UserHostAddress.IPToInteger()
            });

        if (ServicesCoursesLessonsID < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax("Dərsə başladınız", string.Format("/tools/electronicregistry/lessons/?i={0}", (BtnAddLessons.CommandArgument + "-" + ServicesCoursesLessonsID + "-" + DALC._GetUsersLogin.Key).Encrypt()));
    }
}