using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Applications_Details_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    ListItem _Applicant;
    ListItem _Private;
    ListItem _Primary;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || Query[1] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindDList()
    {
        DListPersonsTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsPersonsTypes);
        DListPersonsTypes.DataBind();
        DListPersonsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListDocumentTypes.DataSource = DALC.GetList(Tools.Table.DocumentTypes);
        DListDocumentTypes.DataBind();
        DListDocumentTypes.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindGrdApplicationsPersons()
    {

        GrdApplicationsDetails.DataSource = null;
        GrdApplicationsDetails.DataBind();

        var Dictionary = new Dictionary<string, object>()
        {
            { "ApplicationsID",_ApplicationsID}
        };

        DALC.DataTableResult PersonsList = DALC.GetFilterList(Tools.Table.V_ApplicationsPersons, Dictionary, 1, 20);

        GrdApplicationsDetails.DataSource = PersonsList.Dt;
        GrdApplicationsDetails.DataBind();

    }

    private bool Validations()
    {
        if (DListPersonsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Vətəndaşın statusunu seçin.");
            return false;
        }

        if (DListDocumentTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Sənədin növünü seçin.");
            return false;
        }

        if (!(DListDocumentTypes.SelectedValue == Tools.DocumentTypes.Sənədini_təqdim_etməyib.ToString("d")))
        {
            if (string.IsNullOrEmpty(TxtDocumentNumber.Text))
            {
                Config.MsgBoxAjax("Sənədin nömrəsini qeyd edin.");
                return false;
            }
        }
        else
        {
            TxtDocumentNumber.Text = TxtDocumentNumber.Text.PadLeft(8, '0');
        }

        if (string.IsNullOrEmpty(TxtSurname.Text))
        {
            Config.MsgBoxAjax("Soyadını qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            Config.MsgBoxAjax("Adını qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtPatronymic.Text))
        {
            Config.MsgBoxAjax("Atasının adını qeyd edin.");
            return false;
        }

        object Date = Config.DateTimeFormat(TxtBirthDate.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Doğum tarixini düzgün daxil edin.");
            return false;
        }

        TxtBirthDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        if (DListGender.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Cinsi qeyd edin.");
            return false;
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

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Müraciətlər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Ətraflı məlumat";
            BindDList();
            BindGrdApplicationsPersons();
        }

        // Her function-lari cagiraq
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "MultiSelect", @" $(function () {$('.multiSelect').multiselect({buttonWidth: '100%',});});", true);
        Config.Calendar();
    }

    protected void LnkAddPersons_Click(object sender, EventArgs e)
    {
        //Yeni şəxs əlavə etdikde title-lı dəyişək və butun kontrollari boshaldaq
        LtrTitle.Text = "Yeni şəxs əlavə et";

        DListPersonsTypes.SelectedValue = DListDocumentTypes.SelectedValue = "-1";
        TxtDocumentNumber.Text = TxtSurname.Text =
        TxtName.Text = TxtPatronymic.Text = TxtBirthDate.Text =
        TxtContacts.Text = TxtDescription.Text = "";
        BtnAddPersons.CommandArgument = "";

        BtnAddPersons.Text = "Əlavə et";
        MultiView1.ActiveViewIndex = 0;
        Config.Modal();
    }

    protected void BtnAddPersons_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        bool IsPrimary = GrdApplicationsDetails.Rows.Count < 1;
        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            {"ApplicationsPersonsTypesID", int.Parse(DListPersonsTypes.SelectedValue) },
            {"DocumentTypesID",int.Parse(DListDocumentTypes.SelectedValue) },
            {"DocumentNumber",TxtDocumentNumber.Text},
            {"Name",TxtName.Text },
            {"Surname", TxtSurname.Text},
            {"Patronymic",TxtPatronymic.Text },
            {"BirthDate",Config.DateTimeFormat(TxtBirthDate.Text)},
            {"Contacts", TxtContacts.Text},
            {"Gender",int.Parse(DListGender.SelectedValue) },
            {"Description",TxtDescription.Text },
        };


        int Check = 0;
        if (!BtnAddPersons.CommandArgument.IsNumeric())
        {
            //Yalniz insertde lazim olacaq melumatlar
            Dictionary.Add("ApplicationsID", _ApplicationsID);
            Dictionary.Add("IsApplicant", false);
            Dictionary.Add("IsPrivate", false);
            Dictionary.Add("IsPrimary", IsPrimary);
            Dictionary.Add("IsActive", true);
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            Check = DALC.InsertDatabase(Tools.Table.ApplicationsPersons, Dictionary);
        }
        else
        {
            //Update ucun where elave edek
            Dictionary.Add("WhereID", int.Parse(BtnAddPersons.CommandArgument));
            Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersons, Dictionary);
        }

        //Her iki emeliyyat ucun eyni yoxlama...
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);
        //Config.Modal("hide");
        Config.RedirectURL(Request.RawUrl.ToString());
        // BindGrdApplicationsPersons();
    }

    protected void GrdApplicationsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            //Əgər əsas şəxs seçilibsə həmin şəxs gizli seçilə və silinə bilməz
            LinkButton LnkDelete = (LinkButton)e.Row.FindControl("LnkDelete");
            LnkDelete.Visible = !(bool)rowView["IsPrimary"];

            ListBox DList = (ListBox)e.Row.FindControl("DListPersonsStatus");
            DList.DataSource = DALC.GetList(Tools.Table.ListPersonsStatus);
            DList.DataBind();
            DList.Attributes.Add("data-id", rowView["ID"]._ToString());

            _Applicant = DList.Items.FindByValue(Tools.ListPersonsStatus.Müraciətçi.ToString("d"));
            _Private = DList.Items.FindByValue(Tools.ListPersonsStatus.Məxfi.ToString("d"));
            _Primary = DList.Items.FindByValue(Tools.ListPersonsStatus.Əsas_şəxs.ToString("d"));

            _Applicant.Selected = (bool)rowView["IsApplicant"];
            _Private.Selected = (bool)rowView["IsPrivate"];
            _Primary.Selected = (bool)rowView["IsPrimary"];

            if (_Private.Selected)
            {
                DList.Items.Remove(_Primary);
            }
            else if (_Primary.Selected)
            {
                DList.Items.Remove(_Private);
                _Primary.Attributes.Add("disabled", "true");
            }
        }
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"IsActive",false },
            {"WhereID",int.Parse((sender as LinkButton).CommandArgument) }
        };

        int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersons, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdApplicationsPersons();
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

        //Eger muracietde CASE acilan vetendash varsa onun ID sini aliriq
        string CasePersonsID = DALC.GetSingleValues("ID", Tools.Table.V_ApplicationsPersons, "ApplicationsCaseID is not null and ApplicationsID", _ApplicationsID, "");
        if (CasePersonsID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        //Varsa yoxalayiriq secilenle eynidirse Case buttonunu aktiv edirik deyilse false ve ya yoxdursa her ikisinde aktiv olur
        if (!string.IsNullOrEmpty(CasePersonsID) && CasePersonsID != PersonsID)
        {
            HypCase.Enabled = false;
            HypCase.Text = HypCase.Text.Replace("case_on", "case_of");
        }
        else
        {
            HypCase.Text = HypCase.Text.Replace("case_of", "case_on");
            HypCase.Enabled = true;
        }

        MultiView1.ActiveViewIndex = 1;
        Config.Modal();
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        //Edit-e basanda duzelish etmek ucun ApplicationsID-ye gore melumatlari  kontrollara dolduraq        
        string AppPersonsID = BtnAddPersons.CommandArgument = (sender as LinkButton).CommandArgument;
        DataTable Dt = DALC.GetApplicationsPersonsByID(int.Parse(AppPersonsID));

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSuccessMessages);
            return;
        }

        LtrTitle.Text = "Düzəliş et";

        DListPersonsTypes.SelectedValue = Dt._Rows("ApplicationsPersonsTypesID");
        DListDocumentTypes.SelectedValue = Dt._Rows("DocumentTypesID");
        DListDocumentTypes_SelectedIndexChanged(null,null);
        TxtDocumentNumber.Text = Dt._Rows("DocumentNumber");
        TxtSurname.Text = Dt._Rows("Surname");
        TxtName.Text = Dt._Rows("Name");
        TxtPatronymic.Text = Dt._Rows("Patronymic");
        TxtBirthDate.Text = ((DateTime)Dt._RowsObject("BirthDate")).ToString("dd.MM.yyyy");
        DListGender.SelectedValue = ((bool)Dt._RowsObject("Gender") == true ? 1 : 0)._ToString();
        TxtContacts.Text = Dt._Rows("Contacts");
        TxtDescription.Text = Dt._Rows("Description");

        BtnAddPersons.Text = "Yadda saxla";
        MultiView1.ActiveViewIndex = 0;
        Config.Modal();
    }

    protected void DListPersonsStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox DList = sender as ListBox;

        _Applicant = DList.Items.FindByValue(Tools.ListPersonsStatus.Müraciətçi.ToString("d"));
        _Private = DList.Items.FindByValue(Tools.ListPersonsStatus.Məxfi.ToString("d"));
        _Primary = DList.Items.FindByValue(Tools.ListPersonsStatus.Əsas_şəxs.ToString("d"));

        if (_Primary == null)
        {
            _Primary = new ListItem("Əsas şəxs", Tools.ListPersonsStatus.Əsas_şəxs.ToString("d"));
            DList.Items.Add(_Primary);
        }

        if (_Private == null)
        {
            _Private = new ListItem("Məxfi", Tools.ListPersonsStatus.Məxfi.ToString("d"));
            DList.Items.Add(_Private);
        }

        var Dictionary = new Dictionary<string, object>();

        for (int i = 0; i < DList.Items.Count; i++)
        {
            if (DList.Items[i].Selected)
            {
                //Əgər məxfi seçilibsə əsası, əsas seçilibsə məxfini siyahıdan çıxardaq
                if (DList.Items[i].Value == Tools.ListPersonsStatus.Məxfi.ToString("d"))
                {
                    DList.Items.Remove(_Primary);
                }
                else if (DList.Items[i].Value == Tools.ListPersonsStatus.Əsas_şəxs.ToString("d"))
                {
                    DList.Items.Remove(_Private);
                }
            }
        }

        DALC.Transaction Transaction = new DALC.Transaction();
        //Əgər vetendasha  əsas statusu seçilibsə hamisi false edek...
        if (_Primary.Selected)
        {
            Dictionary.Add("IsPrimary", false);
            Dictionary.Add("WhereApplicationsID", _ApplicationsID);

            int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersons, Dictionary, Transaction);
            if (Check < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }
        Dictionary.Clear();

        //Her seçimdə bazada update edək
        Dictionary.Add("IsApplicant", _Applicant.Selected);
        Dictionary.Add("IsPrivate", _Private.Selected);
        Dictionary.Add("IsPrimary", _Primary.Selected);
        Dictionary.Add("WhereID", DList.Attributes["data-id"]);

        int Chek = DALC.UpdateDatabase(Tools.Table.ApplicationsPersons, Dictionary, Transaction, true);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    protected void DListDocumentTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtDocumentNumber.Text = "";
        PnlDocumentNumber.Visible = !(int.Parse(DListDocumentTypes.SelectedValue) == (int)Tools.DocumentTypes.Sənədini_təqdim_etməyib);
    }
}