using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Applications_Default : System.Web.UI.Page
{
    Tools.Table TableName = Tools.Table.V_Applications;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        #region FilterDList

        DListFilterOrganizations.DataSource = DALC.GetOrganizations();
        DListFilterOrganizations.DataBind();

        if (DListFilterOrganizations.Items.Count > 1)
        {
            PnlFilterOrganizations.Visible = true;
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListFilterApplicationsTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsTypes);
        DListFilterApplicationsTypes.DataBind();
        DListFilterApplicationsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterListOperationTypes.DataSource = DALC.GetList(Tools.Table.ListOperationTypes);
        DListFilterListOperationTypes.DataBind();

        DListFilterApplicationsCaseStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseStatus);
        DListFilterApplicationsCaseStatus.DataBind();
        DListFilterApplicationsCaseStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterSocialStatusID.DataSource = DALC.GetList(Tools.Table.SocialStatus);
        DListFilterSocialStatusID.DataBind();
        DListFilterSocialStatusID.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterApplicationsPersonsTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsPersonsTypes);
        DListFilterApplicationsPersonsTypes.DataBind();
        DListFilterApplicationsPersonsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFilterDocumentTypes.DataSource = DALC.GetList(Tools.Table.DocumentTypes);
        DListFilterDocumentTypes.DataBind();
        DListFilterDocumentTypes.Items.Insert(0, new ListItem("--", "-1"));

        #endregion

        #region FormDList

        DListApplicationsTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsTypes);
        DListApplicationsTypes.DataBind();
        DListApplicationsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsStatus, "");
        DListApplicationsStatus.DataBind();

        #endregion
    }

    private void BindGridApplications()
    {
        GrdApplications.DataSource = null;
        GrdApplications.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue)},
            {"ID",TxtFilterAppID.Text},
            {"ApplicationsTypesID",int.Parse(DListFilterApplicationsTypes.SelectedValue)},
            {"ApplicationsCaseStatusID",int.Parse(DListFilterApplicationsCaseStatus.SelectedValue)},
            {"RegisteredAddress(LIKE)",TxtFilterRegisteredAddress.Text },
            {"CurrentAddress(LIKE)",TxtFilterCurrentAddress.Text },
            {"SocialStatusID",int.Parse(DListFilterSocialStatusID.SelectedValue)},
            {"ApplicationsPersonsTypesID",int.Parse(DListFilterApplicationsPersonsTypes.SelectedValue)},
            {"DocumentTypesID",int.Parse(DListFilterDocumentTypes.SelectedValue)},
            {"DocumentNumber",TxtFilterDocumentNumber.Text},
            {"Name(LIKE)",TxtFilterName.Text},
            {"Surname(LIKE)",TxtFilterSurname.Text},
            {"Patronymic(LIKE)",TxtFilterPatronymic.Text},
            {"Create_Dt(Between)",Config.DateTimeFilter(TxtFilterDate1.Text,TxtFilterDate2.Text)},
            {"IsRepeat",CheckFilterIsRepeat.Checked ? 1 : -1},
        };

        //Əməliyyatlar multi secim oldugu ucun onun axtarish algoritmasini elave olaraq burda yazdim
        for (int i = 0; i < DListFilterListOperationTypes.Items.Count; i++)
        {
            ListItem Item = DListFilterListOperationTypes.Items[i];
            if (Item.Selected == true)
            {
                FilterDictionary.Add(((Tools.ListOperationTypes)int.Parse(Item.Value)).ToDescriptionString() + "(OR)", "IS NOT NULL");
            }
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
            Config.RedirectURL(string.Format("/tools/applications/?p={0}", PageNum - 1));
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

        GrdApplications.DataSource = FilterList.Dt;
        GrdApplications.DataBind();
    }

    private void BindGridApplicationsStatus(int ApplicationsID)
    {
        GrdApplicationsSocialStatus.DataSource = DALC.GetApplicationsSocialStatusByApplicationsID(ApplicationsID);
        GrdApplicationsSocialStatus.DataBind();

        PnlGrdList.Visible = GrdApplicationsSocialStatus.Rows.Count > 0;
        BtnAddSosialStatus.Visible = ApplicationsID != 0;

        DListApplicationsSocialStatus.DataSource = DALC.GetApplicationsSocialStatusNotIn(ApplicationsID);
        DListApplicationsSocialStatus.DataBind();
    }

    private bool Validations()
    {
        if (DListApplicationsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Müraciətin növünü seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtRegisteredAddress.Text))
        {
            Config.MsgBoxAjax("Qeydiyyatda olduğu ünvanı qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtCurrentAddress.Text))
        {
            Config.MsgBoxAjax("Yaşadığı ünvanı qeyd edin.");
            return false;
        }

        if (GrdApplicationsSocialStatus.Rows.Count == 0)
        {
            int SelectedCount = 0;
            for (int i = 0; i < DListApplicationsSocialStatus.Items.Count; i++)
            {
                if (DListApplicationsSocialStatus.Items[i].Selected)
                {
                    SelectedCount++;
                }
            }

            if (SelectedCount == 0)
            {
                Config.MsgBoxAjax("Sosial statusunu seçin.");
                return false;
            }
        }

        object Date = Config.DateTimeFormat(TxtCreateDt.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün daxil edin.");
            return false;
        }

        TxtCreateDt.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        return true;
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "$('.multiSelectAll').multiselect({buttonWidth: '100%',includeSelectAllOption: true,});", true);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "DateTime", "dateTime();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Müraciətlər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Müraciətlər";
            BindDList();
            BindGridApplications();
        }

        LoadScriptManager();
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Cache.Remove(TableName._ToString());
        Config.RedirectURL("/tools/applications/?p=1");
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

    protected void LnkAddApp_Click(object sender, EventArgs e)
    {
        //Yeni muraciet əlavə etdikde title-lı dəyişək və butun kontrollari boshaldaq
        LtrTitle.Text = "Yeni müraciət əlavə et";
        BindGridApplicationsStatus(0);
        DListApplicationsTypes.SelectedValue = "-1";
        TxtRegisteredAddress.Text = TxtCurrentAddress.Text = TxtDescription.Text = "";
        TxtCreateDt.Text = DateTime.Now.ToString("dd.MM.yyyy");
        CheckIsRepeat.Checked = false;
        BtnAddApp.CommandArgument = "";
        BtnAddApp.Text = "Əlavə et";

        Config.Modal();
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        //Edit-e basanda duzelish etmek ucun ApplicationsID-ye gore melumatlari  kontrollara dolduraq        
        string ApplicationsID = BtnAddApp.CommandArgument = (sender as LinkButton).CommandArgument;
        DataTable Dt = DALC.GetApplicationsByID(int.Parse(ApplicationsID));
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSuccessMessages);
            return;
        }

        LtrTitle.Text = "Düzəliş et";
        BindGridApplicationsStatus(int.Parse(ApplicationsID));
        DListApplicationsTypes.SelectedValue = Dt._Rows("ApplicationsTypesID");
        TxtRegisteredAddress.Text = Dt._Rows("RegisteredAddress");
        TxtCurrentAddress.Text = Dt._Rows("CurrentAddress");
        CheckIsRepeat.Checked = (bool)Dt._RowsObject("IsRepeat");
        TxtDescription.Text = Dt._Rows("Description");
        TxtCreateDt.Text = ((DateTime)Dt._RowsObject("Create_Dt")).ToString("dd.MM.yyyy");
        BtnAddApp.Text = "Yadda saxla";

        Config.Modal();
    }

    protected void BtnAddApp_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        //Insert ve Update-de lazim olacaqlari dictionary-ye dolduraq
        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
           {"ApplicationsTypesID",int.Parse(DListApplicationsTypes.SelectedValue)},
           {"RegisteredAddress",TxtRegisteredAddress.Text},
           {"CurrentAddress",TxtCurrentAddress.Text},
           {"IsRepeat",CheckIsRepeat.Checked},
           {"Description",TxtDescription.Text},
           {"ApplicationsStatusID",int.Parse(DListApplicationsStatus.SelectedValue)},
           {"Create_Dt",Config.DateTimeFormat(TxtCreateDt.Text)}
        };

        int Check = 0;
        if (!BtnAddApp.CommandArgument.IsNumeric())
        {
            //Yalniz insertde lazim olacaq melumatlar
            Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            Check = DALC.InsertDatabase(Tools.Table.Applications, Dictionary);

            BtnAddApp.CommandArgument = Check.ToString();

        }
        else
        {
            //Update ucun where elave edek
            Dictionary.Add("WhereID", int.Parse(BtnAddApp.CommandArgument));
            Check = DALC.UpdateDatabase(Tools.Table.Applications, Dictionary);
        }

        //Her iki emeliyyat ucun eyni yoxlama...
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BtnAddSosialStatus_Click(null, null);

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, Request.RawUrl);
    }

    protected void BtnAddSosialStatus_Click(object sender, EventArgs e)
    {
        int ApplicationsID = int.Parse(BtnAddApp.CommandArgument);
        DataTable DtApplicationsSocialStatus = new DataTable();
        DtApplicationsSocialStatus.Columns.Add("SosialStatusID", typeof(int));

        for (int i = 0; i < DListApplicationsSocialStatus.Items.Count; i++)
        {
            if (DListApplicationsSocialStatus.Items[i].Selected)
            {
                DtApplicationsSocialStatus.Rows.Add(DListApplicationsSocialStatus.Items[i].Value);
            }
        }

        if (DtApplicationsSocialStatus.Rows.Count > 0)
        {
            int result = DALC.ExecuteProcedure("BulkInsertApplicationsSocialStatus",
                         "ApplicationsID,Add_Dt,Add_Ip,ImportTable",
                          new object[]
                          {
                              ApplicationsID,
                              DateTime.Now,
                              Request.UserHostAddress.IPToInteger(),
                              DtApplicationsSocialStatus
                          });

            if (result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            BindGridApplicationsStatus(ApplicationsID);
        }
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        int ApplicationsSocialStatusID = int.Parse((sender as LinkButton).CommandArgument);

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            {"IsDeleted", 1},
            {"WhereID", ApplicationsSocialStatusID},
        };

        int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsSocialStatus, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGridApplicationsStatus(int.Parse(BtnAddApp.CommandArgument));
    }

}