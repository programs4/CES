using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Applications_Case_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindDList()
    {
        DListApplicationsCaseTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseTypes);
        DListApplicationsCaseTypes.DataBind();
        DListApplicationsCaseTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListMaritalStatus.DataSource = DALC.GetList(Tools.Table.MaritalStatus);
        DListMaritalStatus.DataBind();
        DListMaritalStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCasePlaceTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsCasePlaceTypes);
        DListApplicationsCasePlaceTypes.DataBind();
        DListApplicationsCasePlaceTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCaseWork.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseWork);
        DListApplicationsCaseWork.DataBind();
        DListApplicationsCaseWork.Items.Insert(0, new ListItem("--", "-1"));

        DListEducationsTypes.DataSource = DALC.GetList(Tools.Table.EducationsTypes);
        DListEducationsTypes.DataBind();
        DListEducationsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCaseCommunities.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseCommunities);
        DListApplicationsCaseCommunities.DataBind();
        DListApplicationsCaseCommunities.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCaseFinancial.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseFinancial);
        DListApplicationsCaseFinancial.DataBind();
        DListApplicationsCaseFinancial.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCaseFinancialIncome.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseFinancialIncome);
        DListApplicationsCaseFinancialIncome.DataBind();
        DListApplicationsCaseFinancialIncome.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsCaseStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsCaseStatus);
        DListApplicationsCaseStatus.DataBind();
        DListApplicationsCaseStatus.Items.Insert(0, new ListItem("--", "-1"));

    }

    private void BindGrds()
    {
        GrdAppPersonsServices.DataSource = DALC.GetApplicationsPersonsServices(_ApplicationsID);
        GrdAppPersonsServices.DataBind();
        PnlServices.Visible = GrdAppPersonsServices.Rows.Count > 0;

        GrdEvaluations.DataSource = DALC.GetEvaluationsByApplicationsID(_ApplicationsID);
        GrdEvaluations.DataBind();
        PnlEvaluations.Visible = GrdEvaluations.Rows.Count > 0;
    }

    private Tools.Result BindApplicationsCase()
    {
        DataTable Dt = DALC.GetApplicationsCaseByID(_ApplicationsPersonsID);

        if (Dt == null)
        {
            return Tools.Result.Dt_Null;
        }

        if (Dt.Rows.Count < 1)
        {
            return Tools.Result.Dt_Rows_Count_0;
        }

        DListApplicationsCaseTypes.SelectedValue = Dt._Rows("ApplicationsCaseTypesID");
        DListMaritalStatus.SelectedValue = Dt._Rows("MaritalStatusID");
        DListApplicationsCasePlaceTypes.SelectedValue = Dt._Rows("ApplicationsCasePlaceTypesID");
        DListApplicationsCaseWork.SelectedValue = Dt._Rows("ApplicationsCaseWorkID");
        DListEducationsTypes.SelectedValue = Dt._Rows("EducationsTypesID");
        DListApplicationsCaseCommunities.SelectedValue = Dt._Rows("ApplicationsCaseCommunitiesID");
        DListApplicationsCaseFinancial.SelectedValue = Dt._Rows("ApplicationsCaseFinancialID");
        DListApplicationsCaseFinancialIncome.SelectedValue = Dt._Rows("ApplicationsCaseFinancialIncomeID");
        TxtContactsOther.Text = Dt._Rows("ContactsOther");
        TxtFamilyInformation.Text = Dt._Rows("FamilyInformation");
        TxtProblemInformation.Text = Dt._Rows("ProblemInformation");
        DListApplicationsCaseStatus.SelectedValue = Dt._Rows("ApplicationsCaseStatusID");
        TxtOpeningDt.Text = ((DateTime)Dt._RowsObject("Opening_Dt")).ToString("dd.MM.yyyy");
        TxtClosingDt.Text = Dt._RowsObject("Closing_Dt") == DBNull.Value ? "" : ((DateTime)Dt._RowsObject("Closing_Dt")).ToString("dd.MM.yyyy");
        TxtDescription.Text = Dt._Rows("Description");
        BtnCase.CommandArgument = Dt._Rows("ID");
        return Tools.Result.Succes;

    }

    private bool Validations()
    {
        if (DListApplicationsCaseTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("CASE növünü seçin.");
            return false;
        }

        DListApplicationsCaseTypes_SelectedIndexChanged(null, null);

        if (DListMaritalStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Ailə vəziyyətini seçin.");
            return false;
        }

        if (DListApplicationsCasePlaceTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Yaşayış yerini seçin.");
            return false;
        }

        if (DListApplicationsCaseWork.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Məşğulluq vəziyyətini seçin.");
            return false;
        }

        if (DListEducationsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Təhsilini seçin");
            return false;
        }

        if (DListApplicationsCaseCommunities.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("İcmada iştirakını seçin");
            return false;
        }

        if (DListApplicationsCaseFinancial.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Ailənin maadi təminatını seçin.");
            return false;
        }

        if (DListApplicationsCaseFinancialIncome.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Gəliri seçin");
            return false;
        }

        object Date = Config.DateTimeFormat(TxtOpeningDt.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Case açılma tarixini düzgün daxil edin.");
            return false;
        }

        TxtOpeningDt.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        if (DListApplicationsCaseStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("CASE statusunu seçin.");
            return false;
        }

        if (DListApplicationsCaseStatus.SelectedValue == Tools.ApplicationsCaseStatus.Bağlı.ToString("d"))
        {
            Date = Config.DateTimeFormat(TxtClosingDt.Text);
            if (Date == null)
            {
                Config.MsgBoxAjax("Case bağlanma tarixini düzgün daxil edin.");
                return false;
            }

            TxtClosingDt.Text = ((DateTime)Date).ToString("dd.MM.yyyy");
        }

        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.CASE_açılanlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "CASE";
            LblUsersFullName.Text = DALC._GetUsersLogin.Fullname;
            BindDList();

            int Result = (int)BindApplicationsCase();
            if (Result == (int)Tools.Result.Dt_Null)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            BindGrds();
        }

    }

    protected void BtnCase_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"ApplicationsID",_ApplicationsID },
            {"ApplicationsPersonsID",_ApplicationsPersonsID },
            {"ApplicationsCaseTypesID",int.Parse(DListApplicationsCaseTypes.SelectedValue)},
            {"MaritalStatusID",int.Parse(DListMaritalStatus.SelectedValue)},
            {"ApplicationsCasePlaceTypesID", int.Parse(DListApplicationsCasePlaceTypes.SelectedValue)},
            {"ApplicationsCaseWorkID",int.Parse(DListApplicationsCaseWork.SelectedValue) },
            {"EducationsTypesID",int.Parse(DListEducationsTypes.SelectedValue)},
            {"ApplicationsCaseCommunitiesID", int.Parse(DListApplicationsCaseCommunities.SelectedValue)},
            {"ApplicationsCaseFinancialID", int.Parse(DListApplicationsCaseFinancial.SelectedValue)},
            {"ApplicationsCaseFinancialIncomeID",int.Parse(DListApplicationsCaseFinancialIncome.SelectedValue)},
            {"ContactsOther",TxtContactsOther.Text },
            {"FamilyInformation",TxtFamilyInformation.Text },
            {"ProblemInformation",TxtProblemInformation.Text },
            {"ApplicationsCaseStatusID",int.Parse(DListApplicationsCaseStatus.SelectedValue)},
            {"Description",TxtDescription.Text },
            {"Opening_Dt",Config.DateTimeFormat(TxtOpeningDt.Text)}
        };

        if (DListApplicationsCaseStatus.SelectedValue == Tools.ApplicationsCaseStatus.Bağlı.ToString("d"))
        {
            Dictionary.Add("Closing_Dt", Config.DateTimeFormat(TxtClosingDt.Text));
        }

        int Check = 0;
        if (!BtnCase.CommandArgument.IsNumeric())
        {
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            Check = DALC.InsertDatabase(Tools.Table.ApplicationsCase, Dictionary);
        }
        else
        {
            Dictionary.Add("WhereID", int.Parse(BtnCase.CommandArgument));
            Check = DALC.UpdateDatabase(Tools.Table.ApplicationsCase, Dictionary);
        }

        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);

    }

    protected void DListApplicationsCaseTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (int.Parse(DListApplicationsCaseTypes.SelectedValue) == (int)Tools.ApplicationsCaseTypes.Uşaq)
        {
            int ServicesCheck = int.Parse(DALC.GetSingleValues("Count(*)", Tools.Table.Evaluations, "IsActive=1 and ApplicationsPersonsID", _ApplicationsPersonsID, ""));
            if (ServicesCheck == -1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            if (ServicesCheck < 1)
            {
                Config.MsgBoxAjax("Uşaq keysi açmaq üçün qiymətləndirilmə aparılmalıdır");
                return;
            }
        }
    }

}