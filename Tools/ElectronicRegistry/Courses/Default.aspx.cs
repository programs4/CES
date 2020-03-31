using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ElectronicRegistry_Courses_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_ServicesCourses;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    [Serializable]
    public class CoursesInfo
    {
        public int OrganizationsID { get; set; }
        public int ServicesID { get; set; }
        public int ServicesCoursesTypesID { get; set; }
        public int ServicesCoursesID { get; set; }
    }

    public CoursesInfo GetInfo
    {
        get
        {
            if (ViewState["CoursesInfo"] != null)
            {
                CoursesInfo Info = new CoursesInfo();
                Info = (CoursesInfo)ViewState["CoursesInfo"];
                return Info;
            }

            return null;
        }
    }

    private void BindDList()
    {
        DListFilterOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterOrganizations.DataBind();

        if (DListFilterOrganizations.Items.Count > 1)
        {
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }
        DListFilterOrganizations_SelectedIndexChanged(null, null);

        DListFilterServices.DataSource = DALC.GetServicesForServicesCourses();
        DListFilterServices.DataBind();
        DListFilterServices.Items.Insert(0, new ListItem("--", "-1"));


    }

    private void BindDListAddCourses()
    {
        DListOrganizations.DataSource = DALC.GetOrganizations();
        DListOrganizations.DataBind();
        if (DListOrganizations.Items.Count > 1)
        {
            DListOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }
        DListOrganizations_SelectedIndexChanged(null, null);

        DListServices.DataSource = DALC.GetServicesForServicesCourses();
        DListServices.DataBind();
        DListServices.Items.Insert(0, new ListItem("--", "-1"));

        DListServicesCoursesTypes.DataSource = DALC.GetList(Tools.Table.ServicesCoursesTypes);
        DListServicesCoursesTypes.DataBind();
        DListServicesCoursesTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListLessonsWeekDay.DataSource = Config.GetNumber(1, 5);
        DListLessonsWeekDay.DataBind();

    }

    private void BindGrdCourses()
    {
        GrdServicesCourses.DataSource = null;
        GrdServicesCourses.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        #region BetweenDateFromat

        string Date = "";

        string Dt1 = "20170101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = Config.DateTimeFormat(TxtFilterStart_Dt.Text.Trim());
        object DateFilter2 = Config.DateTimeFormat(TxtFilterEnd_Dt.Text.Trim());

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
            {"ServicesID",int.Parse(DListFilterServices.SelectedValue)},
            {"TeacherUsersID",int.Parse(DListFilterTeacherUsers.SelectedValue)},
            {"Name(LIKE)",TxtCourseName.Text},
            {"Start_Dt(BETWEEN)", Config.DateTimeFilter(TxtFilterStart_Dt.Text,TxtFilterEnd_Dt.Text)},
            {"IsActive",int.Parse(DListFilterStatus.SelectedValue)},
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
            Config.RedirectURL(string.Format("/tools/electronicregistry/courses/?p={0}", PageNum - 1));
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

    private void BindInfoServicesCoursesPersons()
    {
        DListPersons.DataSource = DALC.GetPersonsForServicesCourses(GetInfo.OrganizationsID, GetInfo.ServicesID, GetInfo.ServicesCoursesTypesID);
        DListPersons.DataBind();

        GrdServicesCoursesPersons.DataSource = DALC.GetServicesCoursesPersons(GetInfo.ServicesCoursesID);
        GrdServicesCoursesPersons.DataBind();
    }

    private void BindGrdServicesCoursesPlans()
    {
        GrdServicesCoursesPlans.DataSource = DALC.GetServicesCoursesPlans(GetInfo.ServicesCoursesID, GetInfo.ServicesID);
        GrdServicesCoursesPlans.DataBind();
    }

    private bool Validations()
    {

        if (DListOrganizations.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Mərkəzi seçin.");
            return false;
        }

        if (DListServices.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Xidmət seçin.");
            return false;
        }

        if (DListServicesCoursesTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Kursun növünü qeyd edin.");
            return false;
        }

        if (DListTeacherUsers.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Müəllim seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtCourseName.Text))
        {
            Config.MsgBoxAjax("Kursun adını qeyd edin.");
            return false;
        }

        decimal LessonsCount;
        if (!decimal.TryParse(TxtLessonCount.Text, out LessonsCount) || LessonsCount < 1)
        {
            Config.MsgBoxAjax("Məşğələ sayını qeyd edin.");
            return false;
        }

        decimal LessonsHours;
        if (!decimal.TryParse(TxtLessonsHours.Text, out LessonsHours) || LessonsHours < 1)
        {
            Config.MsgBoxAjax("Məşğələ saatını qeyd edin.");
            return false;
        }

        string LessonsWeekDay = "";
        for (int i = 0; i < DListLessonsWeekDay.Items.Count; i++)
        {
            if (DListLessonsWeekDay.Items[i].Selected)
            {
                LessonsWeekDay += DListLessonsWeekDay.Items[i].Value + ",";
            }
        }

        if (string.IsNullOrEmpty(LessonsWeekDay))
        {
            Config.MsgBoxAjax("Həftənin günlərini seçin!");
            return false;
        }

        BtnAddCourse.Attributes["LessonsWeekDay"] = LessonsWeekDay.Trim(',');

        TimeSpan LessonsStartTime;
        if (!TimeSpan.TryParse(TxtLessonsStartTime.Text, out LessonsStartTime))
        {
            Config.MsgBoxAjax("Başlama vaxtını düzgün daxil edin.");
            return false;
        }

        TimeSpan LessonsEndTime;
        if (!TimeSpan.TryParse(TxtLessonsEndTime.Text, out LessonsEndTime))
        {
            Config.MsgBoxAjax("Bitmə vaxtını düzgün daxil edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtCourseName.Text))
        {
            Config.MsgBoxAjax("Kursun adını qeyd edin.");
            return false;
        }

        object Date = Config.DateTimeFormat(TxtStartDate.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Başlama tarixini düzgün daxil edin.");
            return false;
        }
        TxtStartDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");


        Date = Config.DateTimeFormat(TxtEndDate.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Bitmə tarixini düzgün daxil edin.");
            return false;
        }
        TxtEndDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        return true;
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "multiSelectAll();", true);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "DateTime", "DateTime();Time();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Kurslar))
        {
            Config.RedirectURL("/tools");
            return;
        }
        LoadScriptManager();
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Kurslar";
            BindDList();
            BindGrdCourses();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/electronicregistry/courses/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

    protected void BtnAddCourse_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("OrganizationsID", int.Parse(DListOrganizations.SelectedValue));
        Dictionary.Add("ServicesID", int.Parse(DListServices.SelectedValue));
        Dictionary.Add("ServicesCoursesTypesID", int.Parse(DListServicesCoursesTypes.SelectedValue));
        Dictionary.Add("TeacherUsersID", int.Parse(DListTeacherUsers.SelectedValue));
        Dictionary.Add("Name", TxtCourseName.Text);
        Dictionary.Add("LessonsCount", int.Parse(TxtLessonCount.Text));
        Dictionary.Add("LessonsHours", decimal.Parse(TxtLessonsHours.Text));
        Dictionary.Add("LessonsWeekDays", BtnAddCourse.Attributes["LessonsWeekDay"]);
        Dictionary.Add("LessonsStart_Tm", TimeSpan.Parse(TxtLessonsStartTime.Text));
        Dictionary.Add("LessonsEnd_Tm", TimeSpan.Parse(TxtLessonsEndTime.Text));
        Dictionary.Add("Start_Dt", Config.DateTimeFormat(TxtStartDate.Text));
        Dictionary.Add("End_Dt", Config.DateTimeFormat(TxtEndDate.Text));
        Dictionary.Add("Description", TxtDescription.Text);
        Dictionary.Add("IsActive", int.Parse(DListStatus.SelectedValue));

        int Check;
        if (BtnAddCourse.CommandName == "add")
        {
            Dictionary.Add("Add_Dt", DateTime.Now);
            Check = DALC.InsertDatabase(Tools.Table.ServicesCourses, Dictionary);
        }
        else
        {
            Dictionary.Add("WhereID", GetInfo.ServicesCoursesID);
            Check = DALC.UpdateDatabase(Tools.Table.ServicesCourses, Dictionary);
        }

        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, "/tools/electronicregistry/courses");
    }

    protected void LnkAddCourse_Click(object sender, EventArgs e)
    {
        //Yeni muraciet əlavə etdikde title-lı dəyişək və butun kontrollari boshaldaq
        LtrTitle.Text = "Yeni kurs əlavə et";
        BtnAddCourse.CommandName = "add";
        BtnAddCourse.Text = "Əlavə et";

        BindDListAddCourses();

        DListOrganizations.SelectedValue = DListServices.SelectedValue = DListTeacherUsers.SelectedValue = "-1";
        DListStatus.SelectedValue = "1";
        TxtCourseName.Text = TxtLessonCount.Text = TxtLessonsHours.Text = TxtFilterStart_Dt.Text = TxtFilterEnd_Dt.Text = TxtDescription.Text = "";
        MultiView1.ActiveViewIndex = 0;
        Config.Modal();
    }

    protected void LnkEditCourses_Click(object sender, EventArgs e)
    {
        BtnAddCourse.CommandName = "edit";
        BtnAddCourse.Text = "Yenilə";
        LtrTitle.Text = "Düzəliş et";

        BindDListAddCourses();

        DataTable DtServicesCourse = DALC.GetServicesCoursesByID(GetInfo.ServicesCoursesID);
        if (DtServicesCourse == null || DtServicesCourse.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DListOrganizations.SelectedValue = DtServicesCourse._Rows("OrganizationsID");
        DListOrganizations_SelectedIndexChanged(null, null);

        DListServices.SelectedValue = DtServicesCourse._Rows("ServicesID");
        DListServicesCoursesTypes.SelectedValue = DtServicesCourse._Rows("ServicesCoursesTypesID");
        DListTeacherUsers.SelectedValue = DtServicesCourse._Rows("TeacherUsersID");
        TxtCourseName.Text = DtServicesCourse._Rows("Name");
        TxtLessonCount.Text = DtServicesCourse._Rows("LessonsCount");
        TxtLessonsHours.Text = DtServicesCourse._Rows("LessonsHours");

        for (int i = 0; i < DListLessonsWeekDay.Items.Count; i++)
        {
            if (("," + DtServicesCourse._Rows("LessonsWeekDays") + ",").IndexOf("," + DListLessonsWeekDay.Items[i].Value + ",") > -1)
            {
                DListLessonsWeekDay.Items[i].Selected = true;
            }
        }

        TxtLessonsStartTime.Text = DtServicesCourse._RowsTime("LessonsStart_Tm").ToString("hh\\:mm");
        TxtLessonsEndTime.Text = DtServicesCourse._RowsTime("LessonsEnd_Tm").ToString("hh\\:mm");

        TxtStartDate.Text = DtServicesCourse._RowsDatetime("Start_Dt").ToString("dd.MM.yyyy");
        TxtEndDate.Text = DtServicesCourse._RowsDatetime("End_Dt").ToString("dd.MM.yyyy");
        TxtDescription.Text = DtServicesCourse._Rows("Description");
        DListStatus.SelectedValue = DtServicesCourse._RowsInt("IsActive")._ToString();

        MultiView1.ActiveViewIndex = 0;
        Config.Modal();
    }

    protected void LnkAddPlans_Click(object sender, EventArgs e)
    {
        LtrTitle.Text = "Təqvim planı";

        BindGrdServicesCoursesPlans();

        MultiView1.ActiveViewIndex = 1;
        Config.Modal();
    }

    protected void LnkAddPersons_Click(object sender, EventArgs e)
    {
        LtrTitle.Text = "İştirakçılar";

        BindInfoServicesCoursesPersons();

        TxtDescriptionForPersons.Text = "";
        MultiView1.ActiveViewIndex = 2;
        Config.Modal();
    }

    protected void GrdServicesCoursesPlans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex = e.CommandArgument._ToInt32();

        GridViewRow row = GrdServicesCoursesPlans.Rows[RowIndex];
        int ServicesCoursesPlansID;
        int ServicesPlansID = GrdServicesCoursesPlans.DataKeys[RowIndex]["ServicesPlansID"]._ToInt32();
        int ServicesPlansTypesID = int.Parse((row.FindControl("DListServicesPlansTypes") as DropDownList).SelectedValue);

        decimal Hours;
        if (!decimal.TryParse((row.FindControl("TxtLessonsHours") as TextBox).Text, out Hours) || Hours < 1)
        {
            Config.MsgBoxAjax("Saatı düzgün qeyd edin!");
        }

        int Count;
        if (!int.TryParse((row.FindControl("TxtLessonsCount") as TextBox).Text, out Count) || Count < 1)
        {
            Config.MsgBoxAjax("Sayı düzgün qeyd edin!");
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("ServicesCoursesID", GetInfo.ServicesCoursesID);
        Dictionary.Add("ServicesPlansID", ServicesPlansID);
        Dictionary.Add("ServicesPlansTypesID", ServicesPlansTypesID);
        Dictionary.Add("Hours", Hours);
        Dictionary.Add("Count", Count);


        int Check = 0;
        if (e.CommandName == "add")
        {
            Dictionary.Add("IsDeleted", 0);
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            Check = DALC.InsertDatabase(Tools.Table.ServicesCoursesPlans, Dictionary);

        }
        else if (e.CommandName == "editing")
        {
            ServicesCoursesPlansID = GrdServicesCoursesPlans.DataKeys[RowIndex]["ID"]._ToInt32();
            Dictionary.Add("IsDeleted", 0);
            Dictionary.Add("WhereID", ServicesCoursesPlansID);

            Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesPlans, Dictionary);
        }
        else if (e.CommandName == "deleting")
        {
            ServicesCoursesPlansID = GrdServicesCoursesPlans.DataKeys[RowIndex]["ID"]._ToInt32();
            Dictionary.Add("IsDeleted", 1);
            Dictionary.Add("WhereID", ServicesCoursesPlansID);

            Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesPlans, Dictionary);
        }

        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        int ServicesID = GrdServicesCoursesPlans.DataKeys[RowIndex]["ServicesID"]._ToInt32();

        BindGrdServicesCoursesPlans();
    }

    protected void GrdServicesCoursesPlans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList _DListServicesPlansTypes = (DropDownList)e.Row.FindControl("DListServicesPlansTypes");
            _DListServicesPlansTypes.DataSource = DALC.GetList(Tools.Table.ServicesPlansTypes);
            _DListServicesPlansTypes.DataBind();

            string ServicesPlansTypesID = GrdServicesCoursesPlans.DataKeys[e.Row.RowIndex]["ServicesPlansTypesID"]._ToString();
            _DListServicesPlansTypes.SelectedValue = ServicesPlansTypesID;
        }
    }

    protected void GrdServicesCoursesPersons_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex = e.CommandArgument._ToInt32();
        int ServicesCoursesPersonsID = GrdServicesCoursesPersons.DataKeys[RowIndex]["ID"]._ToInt32();

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        int Check = 0;
        if (e.CommandName == "deleting")
        {
            Dictionary.Add("IsActive", 0);
            Dictionary.Add("WhereID", ServicesCoursesPersonsID);
            Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesPersons, Dictionary);
        }

        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindInfoServicesCoursesPersons();


        Config.MsgBoxAjax(Config._DefaultSuccessMessages);

    }

    protected void BtnAddPersons_Click(object sender, EventArgs e)
    {

        DataTable DtPersons = new DataTable();
        DtPersons.Columns.Add("ServicesCoursesID", typeof(int));
        DtPersons.Columns.Add("ApplicationsPersonsID", typeof(int));
        DtPersons.Columns.Add("Description", typeof(string));
        DtPersons.Columns.Add("IsActive", typeof(bool));
        DtPersons.Columns.Add("Add_Dt", typeof(DateTime));
        DtPersons.Columns.Add("Add_Ip", typeof(int));

        for (int i = 0; i < DListPersons.Items.Count; i++)
        {
            if (DListPersons.Items[i].Selected)
            {
                DtPersons.Rows.Add
                    (
                        GetInfo.ServicesCoursesID,
                        int.Parse(DListPersons.Items[i].Value),
                        TxtDescriptionForPersons.Text,
                        true,
                        DateTime.Now,
                        Request.UserHostAddress.IPToInteger()
                    );
            }
        }

        int Check = DALC.InsertBulk(Tools.Table.ServicesCoursesPersons, DtPersons);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSuccessMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);

        BindInfoServicesCoursesPersons();
        TxtDescriptionForPersons.Text = "";

    }

    protected void DListOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        DListTeacherUsers.DataSource = DALC.GetUsersByOrganizationsID(int.Parse(DListOrganizations.SelectedValue));
        DListTeacherUsers.DataBind();
        DListTeacherUsers.Items.Insert(0, new ListItem("--", "-1"));

        PnlTeacherUsers.Visible = DListTeacherUsers.Items.Count > 1;
    }

    protected void DListFilterOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        DListFilterTeacherUsers.DataSource = DALC.GetUsersByOrganizationsID(int.Parse(DListFilterOrganizations.SelectedValue));
        DListFilterTeacherUsers.DataBind();
        DListFilterTeacherUsers.Items.Insert(0, new ListItem("--", "-1"));

        PnlFilterTeacherUsers.Visible = DListFilterTeacherUsers.Items.Count > 1;
    }

    protected void GrdServicesCourses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex = e.CommandArgument._ToInt32();

        CoursesInfo Info = new CoursesInfo();
        Info.ServicesCoursesID = GrdServicesCourses.DataKeys[RowIndex]["ID"]._ToInt32();
        Info.OrganizationsID = GrdServicesCourses.DataKeys[RowIndex]["OrganizationsID"]._ToInt32();
        Info.ServicesID = GrdServicesCourses.DataKeys[RowIndex]["ServicesID"]._ToInt32();
        Info.ServicesCoursesTypesID = GrdServicesCourses.DataKeys[RowIndex]["ServicesCoursesTypesID"]._ToInt32();
        ViewState["CoursesInfo"] = Info;

        if (e.CommandName == "editcourses")
        {
            LnkEditCourses_Click(null, null);
        }
        else if (e.CommandName == "addplans")
        {
            LnkAddPlans_Click(null, null);
        }
        else if (e.CommandName == "addpersons")
        {
            LnkAddPersons_Click(null, null);
        }
    }
}