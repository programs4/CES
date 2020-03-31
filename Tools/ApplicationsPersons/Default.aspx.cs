using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ApplicationsPersons_Default : System.Web.UI.Page
{
    Tools.Table _TableName = Tools.Table.V_ApplicationsPersons;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListFltOrganizations.DataSource = DALC.GetOrganizations();
        DListFltOrganizations.DataBind();

        if (DListFltOrganizations.Items.Count > 1)
        {
            PnlFilterOrganizations.Visible = true;
            DListFltOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListFltApplicationsTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsTypes);
        DListFltApplicationsTypes.DataBind();
        DListFltApplicationsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListFltListOperationTypes.DataSource = DALC.GetList(Tools.Table.ListOperationTypes);
        DListFltListOperationTypes.DataBind();

        DListFltSocialStatusID.DataSource = DALC.GetList(Tools.Table.SocialStatus);
        DListFltSocialStatusID.DataBind();
        DListFltSocialStatusID.Items.Insert(0, new ListItem("--", "-1"));

        DListFltApplicationsPersonsType.DataSource = DALC.GetList(Tools.Table.ApplicationsPersonsTypes);
        DListFltApplicationsPersonsType.DataBind();
        DListFltApplicationsPersonsType.Items.Insert(0, new ListItem("--", "-1"));

        DListFltDocumentTypes.DataSource = DALC.GetList(Tools.Table.DocumentTypes);
        DListFltDocumentTypes.DataBind();
        DListFltDocumentTypes.Items.Insert(0, new ListItem("--", "-1"));

    }

    private void BindGrdApplicationsPersons()
    {
        GrdApplicationsPersons.DataSource = null;
        GrdApplicationsPersons.DataBind();

        PnlSearch.BindControls(FilterDictionary, _TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFltOrganizations.SelectedValue)},
            {"ID",TxtFltPersonID.Text},
            {"ApplicationsID",TxtFltApplicationsID.Text},
            {"ApplicationsTypesID",int.Parse(DListFltApplicationsTypes.SelectedValue)},
            {"ApplicationsPersonsTypesID",int.Parse(DListFltApplicationsPersonsType.SelectedValue)},
            {"DocumentTypesID",int.Parse(DListFltDocumentTypes.SelectedValue)},
            {"DocumentNumber",TxtFltDocumentNumber.Text},
            {"Surname(LIKE)",TxtFltSurname.Text},
            {"Name(LIKE)",TxtFltName.Text},
            {"Patronymic(LIKE)",TxtFltPatronymic.Text},
            {"SocialStatusID",int.Parse(DListFltSocialStatusID.SelectedValue)},
            {"RegisteredAddress(LIKE)",TxtFilterRegisteredAddress.Text },
            {"CurrentAddress(LIKE)",TxtFilterCurrentAddress.Text },
            {"Add_Dt(Between)",Config.DateTimeFilter(TxtFilterDate1.Text,TxtFilterDate2.Text)},
        };

        if (PnlOperations.Visible == true)
        {
            //Əməliyyatlar multi secim oldugu ucun onun axtarish algoritmasini elave olaraq burda yazdim
            for (int i = 0; i < DListFltListOperationTypes.Items.Count; i++)
            {
                ListItem Item = DListFltListOperationTypes.Items[i];
                if (Item.Selected == true)
                {
                    FilterDictionary.Add(((Tools.ListOperationTypes)int.Parse(Item.Value)).ToDescriptionString() + "(OR)", "IS NOT NULL");
                }
            }
        }
        else
        {
            switch (Config._GetQueryString("type"))
            {
               
                case "sibr-evaluations":
                    FilterDictionary.Add("SIBRID", "IS NOT NULL");
                    ((Literal)Master.FindControl("LtrTitle")).Text = "SIB-R Qiymətləndirilənlər";
                    break;
                case "evaluations":
                    FilterDictionary.Add("EvaluationsID", "IS NOT NULL");
                    ((Literal)Master.FindControl("LtrTitle")).Text = "Qiymətləndirilənlər";
                    break;
                case "evaluationsskill":
                    FilterDictionary.Add("EvaluationsSkillID", "IS NOT NULL");
                    ((Literal)Master.FindControl("LtrTitle")).Text = "Qiymətləndirilənlər";
                    break;
                case "services":
                    FilterDictionary.Add("ApplicationsPersonsServicesID", "IS NOT NULL");
                    ((Literal)Master.FindControl("LtrTitle")).Text = "Xidmətlərdən istifadə edənlər";
                    break;
                case "case":
                    FilterDictionary.Add("ApplicationsCaseID", "IS NOT NULL");
                    ((Literal)Master.FindControl("LtrTitle")).Text = "CASE açılanlar";
                    break;
                default:
                    break;
            }
        }

        int PageNum;
        int RowNumber = 20;
        if (!int.TryParse(Config._GetQueryString("p"), out PageNum))
        {
            PageNum = 1;
        }             

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult FilterList = DALC.GetFilterList(_TableName, FilterDictionary, PageNum, RowNumber);

        if (FilterList.Count == -1)
        {
            return;
        }

        if (FilterList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/applicationspersons/?p={0}", PageNum - 1));
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
        GrdApplicationsPersons.DataSource = FilterList.Dt;
        GrdApplicationsPersons.DataBind();
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "$('.multiSelect').multiselect({buttonWidth: '100%',includeSelectAllOption: true,});", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Vətəndaşlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Vətəndaşlar";
            BindDList();
            BindGrdApplicationsPersons();
        }

        LoadScriptManager();
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, _TableName, true);
        Cache.Remove(_TableName._ToString());
        Config.RedirectURL("/tools/applicationspersons/?p=1");
    }

    protected void LnkOperations_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (sender as LinkButton);
        string PersonsID = Lnk.CommandArgument;
        string ApplicationsID = Lnk.CommandName;
        LtrTitle.Text = "Əməliyyatlar";

        HyperLink HypEvaluations = (HyperLink)Operations.FindControl("HypEvaluations");
        HyperLink HypCase = (HyperLink)Operations.FindControl("HypCase");
        HyperLink HypServices = (HyperLink)Operations.FindControl("HypServices");

        HypEvaluations.NavigateUrl = string.Format("/tools/sib-r/?i={0}", Cryptography.Encrypt(string.Format("{0}-{1}-{2}", ApplicationsID, PersonsID, DALC._GetUsersLogin.Key)));
        HypCase.NavigateUrl = string.Format("/tools/applications/case/?i={0}", Cryptography.Encrypt(string.Format("{0}-{1}-{2}", ApplicationsID, PersonsID, DALC._GetUsersLogin.Key)));
        HypServices.NavigateUrl = string.Format("/tools/services/?i={0}", Cryptography.Encrypt(string.Format("{0}-{1}-{2}", ApplicationsID, PersonsID, DALC._GetUsersLogin.Key)));

        Config.Modal();
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlSearch.ClearControls();
        Session["FilterSession"] = null;
        BtnFilter_Click(null, null);
    }

}
