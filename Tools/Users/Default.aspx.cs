using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Users_Default : System.Web.UI.Page
{
    string _CountCacheName = "Users";
    string _FilterSessionName = "UsersFilter";
    string _RouteType = "";

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
        DListFilterDocType.DataSource = DALC.GetList(Tools.Table.DocumentTypes);
        DListFilterDocType.DataBind();
        DListFilterDocType.Items.Insert(0, new ListItem("--", "-1"));

        #endregion

        #region FormDList

        DListOrganizations.DataSource = DALC.GetOrganizations();
        DListOrganizations.DataBind();

        if (DListOrganizations.Items.Count > 1)
        {
            DListOrganizations.Enabled = true;
            DListOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListEducationTypes.DataSource = DALC.GetList(Tools.Table.EducationsTypes);
        DListEducationTypes.DataBind();
        DListEducationTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListDocTypes.DataSource = DALC.GetList(Tools.Table.DocumentTypes);
        DListDocTypes.DataBind();
        DListDocTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListUsersStatus.DataSource = DALC.GetList(Tools.Table.UsersStatus);
        DListUsersStatus.DataBind();
        DListUsersStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListMarital.DataSource = DALC.GetList(Tools.Table.MaritalStatus);
        DListMarital.DataBind();
        DListMarital.Items.Insert(0, new ListItem("--", "-1"));

        #endregion
    }

    private void BindGridUsers()
    {
        GrdUsers.DataSource = null;
        GrdUsers.DataBind();

        if (Session[_FilterSessionName] != null)
        {
            var DictionarySession = (Dictionary<string, object>)Session[_FilterSessionName];
            if (DictionarySession.Count != 0)
            {
                DListFilterOrganizations.SelectedValue = DictionarySession["OrganizationsID"]._ToString();
                TxtFilterFullname.Text = DictionarySession["Fullname"]._ToString();
                TxtFilterDocNumber.Text = DictionarySession["DocumentNumber"]._ToString();
                TxtFilterPin.Text = DictionarySession["PIN"]._ToString();
                DListFilterDocType.SelectedValue = DictionarySession["DocumentsTypesID"]._ToString();
            }
        }

        TxtFilterFullname.Text = TxtFilterFullname.Text.ToUpper();
        TxtFilterPin.Text = TxtFilterPin.Text.ToUpper();

        var Dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",int.Parse(DListFilterOrganizations.SelectedValue)},
            {"Fullname(LIKE)",TxtFilterFullname.Text},
            {"DocumentNumber",TxtFilterDocNumber.Text},
            {"PIN",TxtFilterPin.Text},
            {"DocumentsTypesID",int.Parse(DListFilterDocType.SelectedValue)}
        };

        int PageNum;
        int RowNumber = 20;
        if (!int.TryParse(Config._GetQueryString("p"), out PageNum))
        {
            PageNum = 1;
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult UsersList = DALC.GetUsers(Dictionary, PageNum, RowNumber);

        if (UsersList.Count == -1)
        {
            return;
        }

        if (UsersList.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.RedirectURL(string.Format("/tools/users/?p={0}", PageNum - 1));
        }

        LblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", UsersList.Count);
        int Total_Count = UsersList.Count % RowNumber > 0 ? (UsersList.Count / RowNumber) + 1 : UsersList.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = UsersList.Count > RowNumber;

        GrdUsers.DataSource = UsersList.Dt;
        GrdUsers.DataBind();
    }

    private void BindUsersInfo(int UsersID)
    {
        DataTable Dt = DALC.GetUsersInfoByID(UsersID);
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
        string s = Dt._Rows("OrganizationsID");
        DListOrganizations.SelectedValue = Dt._Rows("OrganizationsID");
        DListDocTypes.SelectedValue = Dt._Rows("DocumentsTypesID");
        TxtDocNumber.Text = Dt._Rows("DocumentNumber");
        TxtPin.Text = Dt._Rows("PIN");
        TxtFullname.Text = Dt._Rows("Fullname");
        TxtBirthDate.Text = ((DateTime)Dt._RowsObject("BirthDate")).ToString("dd.MM.yyyy");

        if ((bool)Dt._RowsObject("Gender"))
        {
            DListGender.SelectedValue = "1";
        }
        else
        {
            DListGender.SelectedValue = "0";
        }

        string StartWorkDate = "", AttestationDate = "";
        if (Dt._RowsObject("StartWork_Dt") != DBNull.Value)
        {
            StartWorkDate = ((DateTime)Dt._RowsObject("StartWork_Dt")).ToString("dd.MM.yyyy");
        }

        if (Dt._RowsObject("Attestation_Dt") != DBNull.Value)
        {
            AttestationDate = ((DateTime)Dt._RowsObject("Attestation_Dt")).ToString("dd.MM.yyyy");
        }

        DListMarital.SelectedValue = Dt._Rows("MaritalStatusID");
        DListEducationTypes.SelectedValue = Dt._Rows("EducationsTypesID");
        TxtEducationPlace.Text = Dt._Rows("EducationsPlace");
        TxtEducationSpecialty.Text = Dt._Rows("EducationSpecialty");
        TxtWorkExperience.Text = Dt._Rows("WorkExperience");
        TxtPosition.Text = Dt._Rows("Positions");
        TxtTasks.Text = Dt._Rows("Tasks");
        TxtOtherWorks.Text = Dt._Rows("OtherWorks");
        TxtStartWorkDate.Text = StartWorkDate;
        TxtAttestationDate.Text = AttestationDate;
        TxtSSN.Text = Dt._Rows("SSN");
        TxtContact.Text = Dt._Rows("Contacts");
        TxtTrainingAndCourses.Text = Dt._Rows("TraningAndCourses");
        DListUsersStatus.SelectedValue = Dt._Rows("UsersStatusID");
        TxtDescription.Text = Dt._Rows("Description");

        if (_RouteType == "info")
        {
            DListOrganizations.Enabled = DListDocTypes.Enabled = DListGender.Enabled =
            DListEducationTypes.Enabled = DListUsersStatus.Enabled = false;

            TxtDocNumber.Enabled = TxtPin.Enabled = TxtFullname.Enabled =
            TxtBirthDate.Enabled = DListEducationTypes.Enabled = TxtEducationPlace.Enabled = TxtEducationSpecialty.Enabled = TxtWorkExperience.Enabled =
            TxtPosition.Enabled = TxtTasks.Enabled = TxtStartWorkDate.Enabled = TxtOtherWorks.Enabled = TxtAttestationDate.Enabled = TxtSSN.Enabled = false;
        }

    }

    private bool Validations()
    {
        if (DListOrganizations.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Qurumun adını seçin.");
            return false;
        }

        if (DListDocTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Sənədin növünü seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtDocNumber.Text))
        {
            Config.MsgBoxAjax("Sənədin nömrəsi seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtPin.Text))
        {
            Config.MsgBoxAjax("Fin-i qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtFullname.Text))
        {
            Config.MsgBoxAjax("Soyadı, adı və atasının adının adını qeyd edin.");
            return false;
        }

        object Date = TxtBirthDate.Text.DateTimeFormat();
        if (Date == null)
        {
            Config.MsgBoxAjax("Doğum tarixini düzgün qeyd edin.");
            return false;
        }
        TxtBirthDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        if (DListGender.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Cinsni qeyd edin.");
            return false;
        }

        if (DListMarital.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Ailə vəziyyətini seçin.");
            return false;
        }

        if (DListEducationTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Təhsilini seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtEducationPlace.Text))
        {
            Config.MsgBoxAjax("Təhsil müəssisəsinin adını qeyd edin.");
            return false;
        }

        if (!string.IsNullOrEmpty(TxtStartWorkDate.Text))
        {
            Date = TxtStartWorkDate.Text.DateTimeFormat();
            if (Date == null)
            {
                Config.MsgBoxAjax("Mərkəzdə işə başlama tarixini düzgün qeyd edin.");
                return false;
            }
            TxtStartWorkDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");
        }


        if (!string.IsNullOrEmpty(TxtAttestationDate.Text))
        {
            Date = TxtAttestationDate.Text.DateTimeFormat();
            if (Date == null)
            {
                Config.MsgBoxAjax("Attestasiyadan keçmə tarixini düzgün qeyd edin.");
                return false;
            }
            TxtAttestationDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");
        }

        if (DListUsersStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Hazırkı iş statusunu seçin.");
            return false;
        }

        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.İşçilər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        _RouteType = Config._Route("type");

        if (!IsPostBack)
        {
            BindDList();

            ((Literal)Master.FindControl("LtrTitle")).Text = "İşçilər";

            //Eger istifadeci oz shexsi meluatlarina baxmaq ucun gelibse
            //Burda Route ishletdik lazim ola query-ye cevirerik
            if (_RouteType == "info")
            {
                ((Literal)Master.FindControl("LtrTitle")).Text = "Şəxsi məlumatlar";
                LtrTab.Visible = true;
                PnlTabPassword.Visible = true;
                BindUsersInfo(DALC._GetUsersLogin.ID);
                BtnAddUser.CommandArgument = DALC._GetUsersLogin.ID._ToString();
                BtnAddUser.Text = "Yadda Saxla";
                MultiViewUsers.ActiveViewIndex = 0;
                return;
            }
            BindGridUsers();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",DListFilterOrganizations.SelectedValue},
            {"Fullname",TxtFilterFullname.Text},
            {"DocumentNumber",TxtFilterDocNumber.Text},
            {"PIN",TxtFilterPin.Text},
            {"DocumentsTypesID",int.Parse(DListFilterDocType.SelectedValue)}
        };

        Session[_FilterSessionName] = Dictionary;
        Cache.Remove(_CountCacheName);


        Config.RedirectURL("/tools/users/?p=1");
    }

    protected void LnkAddApp_Click(object sender, EventArgs e)
    {
        ((Literal)Master.FindControl("LtrTitle")).Text = "Yeni işçi əlavə et";

        DListOrganizations.SelectedIndex = DListDocTypes.SelectedIndex = DListGender.SelectedIndex =
        DListMarital.SelectedIndex = DListEducationTypes.SelectedIndex = DListUsersStatus.SelectedIndex = 0;

        TxtDocNumber.Text = TxtPin.Text = TxtFullname.Text = TxtBirthDate.Text =
        TxtEducationPlace.Text = TxtEducationSpecialty.Text = TxtWorkExperience.Text =
        TxtPosition.Text = TxtTasks.Text = TxtOtherWorks.Text = TxtStartWorkDate.Text =
        TxtAttestationDate.Text = TxtSSN.Text = TxtContact.Text = TxtTrainingAndCourses.Text = TxtDescription.Text = "";


        BtnAddUser.CommandArgument = "";
        BtnAddUser.Text = "Əlavə et";
        MultiViewUsers.ActiveViewIndex = 0;
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        string UsersID = BtnAddUser.CommandArgument = (sender as LinkButton).CommandArgument;
        BindUsersInfo(int.Parse(UsersID));
        BtnAddUser.Text = "Yadda saxla";
        MultiViewUsers.ActiveViewIndex = 0;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiViewUsers.ActiveViewIndex = 1;
    }

    protected void BtnAddUser_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
           {"DocumentTypesID",int.Parse(DListDocTypes.SelectedValue)},
           {"DocumentNumber",TxtDocNumber.Text},
           {"PIN",TxtPin.Text.ToUpper()},
           {"Fullname",TxtFullname.Text.ToUpper()},
           {"BirthDate",Config.DateTimeFormat(TxtBirthDate.Text)},
           {"Gender",DListGender.SelectedValue},
           {"MaritalStatusID",int.Parse(DListMarital.SelectedValue)},
           {"EducationsTypesID",int.Parse(DListEducationTypes.SelectedValue)},
           {"EducationsPlace",TxtEducationPlace.Text.ToUpper()},
           {"EducationSpecialty",TxtEducationSpecialty.Text.ToUpper()},
           {"WorkExperience",TxtWorkExperience.Text.ToUpper()},
           {"Positions",TxtPosition.Text.ToUpper()},
           {"Tasks",TxtTasks.Text.ToUpper()},
           {"OtherWorks",TxtOtherWorks.Text.ToUpper()},
           {"StartWork_Dt", string.IsNullOrEmpty(TxtStartWorkDate.Text)?DBNull.Value: Config.DateTimeFormat(TxtStartWorkDate.Text)},
           {"Attestation_Dt",string.IsNullOrEmpty(TxtAttestationDate.Text)?DBNull.Value: Config.DateTimeFormat(TxtAttestationDate.Text)},
           {"SSN",TxtSSN.Text},
           {"Contacts",TxtContact.Text},
           {"TraningAndCourses",TxtTrainingAndCourses.Text.ToUpper()},
           {"Description",TxtDescription.Text},
           {"UsersStatusID",int.Parse(DListUsersStatus.SelectedValue)},
        };

        int Check = 0;
        if (!BtnAddUser.CommandArgument.IsNumeric())
        {
            DataTable DtUser = DALC.GetDataTable("*", Tools.Table.Users, "PIN", new object[] { TxtPin.Text });
            if (DtUser == null)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            if (DtUser.Rows.Count > 0)
            {
                Config.MsgBoxAjax("Bu işçi artıq mövcuddur.");
                return;
            }

            Dictionary.Add("OrganizationsID", int.Parse(DListOrganizations.SelectedValue));
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            Check = DALC.InsertDatabase(Tools.Table.Users, Dictionary);
        }
        else
        {
            Dictionary.Add("WhereID", int.Parse(BtnAddUser.CommandArgument));
            Check = DALC.UpdateDatabase(Tools.Table.Users, Dictionary);
        }


        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);

        if (_RouteType != "info")
        {
            BindGridUsers();
            MultiViewUsers.ActiveViewIndex = 1;
        }
    }

    protected void LnkSecurity_Click(object sender, EventArgs e)
    {
        Config.Modal();
        TxtLogin.BorderColor = TxtPassword.BorderColor = TxtPasswordRepeat.BorderColor = Color.Empty;
        TxtLogin.Enabled = PnlResetPassword.Visible = true;
        string UsersID = BtnUserInfoSave.CommandArgument = BtnPermissionsSave.CommandArgument = (sender as LinkButton).CommandArgument;

        DataTable Dt = DALC.GetUsersAccountsByID(int.Parse(UsersID));

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (Dt.Rows.Count < 1)
        {
            PnlPassword.Visible = true;
            PnlResetUser.Visible = PermissionsTab.Visible = false;
            BtnUserInfoSave.CommandName = "Add";
            TxtLogin.Text = TxtPassword.Text = TxtPasswordRepeat.Text = "";
        }
        else
        {
            PnlPassword.Visible = LblResettingPassword.Visible = false;
            PnlResetUser.Visible = PermissionsTab.Visible = true;
            BtnUserInfoSave.CommandName = "Edit";
            TxtLogin.Text = Dt._Rows("Login");
            CheckIsActive.Checked = !(bool)Dt._RowsObject("IsActive");
            if (CheckIsActive.Checked == true)
            {
                TxtLogin.Enabled = false;
                PnlResetPassword.Visible = false;
            }

            //Permissions bolmesi        
            GrdPermissions.DataSource = DALC.GetList(Tools.Table.UsersPermissionsModules, "Where IsPermission=1 and IsActive=1 Order By Priority asc");
            GrdPermissions.DataBind();

            //Hansi huquqlari var onu grid-de qeyd edek
            string UsersPermissionsModules = "," + Dt._Rows("UsersPermissionsModules") + ",";
            foreach (GridViewRow row in GrdPermissions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string UsersPermissionsModulesID = GrdPermissions.DataKeys[row.RowIndex]["ID"]._ToString();
                    CheckBox ChkRow = (row.Cells[2].FindControl("CheckPermissions") as CheckBox);

                    if (UsersPermissionsModules.IndexOf("," + UsersPermissionsModulesID + ",") > -1)
                    {
                        ChkRow.Checked = true;
                    }
                }
            }
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        string GeneratedPassword = Config.Key(4);
        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"Password",Config.SHA1Special(GeneratedPassword) },
            {"WhereUsersID",int.Parse(BtnUserInfoSave.CommandArgument) }
        };

        int Check = DALC.UpdateDatabase(Tools.Table.UsersAccounts, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        LblResettingPassword.Visible = true;
        LblResettingPassword.Text = "Şifrə uğurla sıfırlandı.Yeni şifrəniz: <b>" + GeneratedPassword + "</b>";

    }

    protected void BtnUserInfoSave_Click(object sender, EventArgs e)
    {
        TxtLogin.BorderColor = Color.Empty;
        if (string.IsNullOrEmpty(TxtLogin.Text))
        {
            TxtLogin.BorderColor = Color.Red;
            return;
        }
        DataTable DtCheckLogin = DALC.CheckLoginExist(TxtLogin.Text);
        if (DtCheckLogin == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
        if (DtCheckLogin.Rows.Count > 0)
        {
            if (DtCheckLogin._Rows("UsersID") != BtnUserInfoSave.CommandArgument)
            {
                Config.MsgBoxAjax("Bu istifadəçi adı artıq mövcuddur.");
                return;
            }
        }

        if (BtnUserInfoSave.CommandName == "Add")
        {
            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                TxtPassword.BorderColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(TxtPasswordRepeat.Text))
            {
                TxtPasswordRepeat.BorderColor = Color.Red;
                return;
            }
            if (TxtPassword.Text != TxtPasswordRepeat.Text)
            {
                TxtPassword.BorderColor = TxtPasswordRepeat.BorderColor = Color.Red;
                Config.MsgBoxAjax("Şifrə ilə şifrə təkrarı uyğun deyil.");
                return;
            }

            Dictionary<string, object> Dictionary = new Dictionary<string, object>()
            {
                {"UsersID",BtnUserInfoSave.CommandArgument},
                {"Login",TxtLogin.Text },
                {"Password",Config.SHA1Special(TxtPassword.Text)},
                {"UsersPermissionsModules",DBNull.Value },
                {"PermissionsIP","*" },
                {"IsActive",true},
                {"Add_Dt",DateTime.Now},
                {"Add_Ip",Request.UserHostAddress.IPToInteger()},
            };

            int Check = 0;
            Check = DALC.InsertDatabase(Tools.Table.UsersAccounts, Dictionary);
            if (Check < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }
        else if (BtnUserInfoSave.CommandName == "Edit")
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>()
            {
                {"Login",TxtLogin.Text },
                {"IsActive",!CheckIsActive.Checked },
                {"WhereUsersID",int.Parse(BtnUserInfoSave.CommandArgument) }
            };

            int Check = DALC.UpdateDatabase(Tools.Table.UsersAccounts, Dictionary);
            if (Check < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, Request.RawUrl);
    }

    protected void CheckIsActive_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckIsActive.Checked == true)
        {
            TxtLogin.Enabled = false;
            PnlResetPassword.Visible = false;
        }
        else
        {
            TxtLogin.Enabled = true;
            PnlResetPassword.Visible = true;
        }
    }

    protected void BtnPermissionsSave_Click(object sender, EventArgs e)
    {
        string UsersPermissionsModules = "";
        foreach (GridViewRow r in GrdPermissions.Rows)
        {
            CheckBox chk = (CheckBox)r.FindControl("CheckPermissions");
            if (chk != null && chk.Checked)
            {
                UsersPermissionsModules = UsersPermissionsModules + (r.Cells[0].Text)._ToString() + ",";
            }
        }
        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"UsersPermissionsModules", UsersPermissionsModules.Trim(',')},
            {"WhereUsersID",int.Parse(BtnPermissionsSave.CommandArgument) }
        };

        int Check = DALC.UpdateDatabase(Tools.Table.UsersAccounts, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
        Config.MsgBoxAjax(Config._DefaultSuccessMessages);
        Config.Modal("hide");
    }

    protected void BtnChangePassword_Click(object sender, EventArgs e)
    {
        TxtPassword.BorderColor = TxtPasswordRepeat.BorderColor = Color.Empty;

        if (string.IsNullOrEmpty(TxtOldPassword.Text))
        {
            Config.MsgBoxAjax("Köhnə şifrəni daxil edin.");
            return;
        }

        if (string.IsNullOrEmpty(TxtNewPassword.Text))
        {
            Config.MsgBoxAjax("Yeni şifrəni daxil edin.");
            return;
        }

        if (TxtNewPassword.Text != TxtRepeatPassword.Text)
        {
            TxtPassword.BorderColor = TxtPasswordRepeat.BorderColor = Color.Red;
            Config.MsgBoxAjax("Yeni şifrə ilə təkrar şifrə uyğun deyil.");
            return;
        }

        int Count = int.Parse(DALC.GetSingleValues("Count(ID)", Tools.Table.UsersAccounts, "ID,Password", new object[] { DALC._GetUsersLogin.ID, Config.SHA1Special(TxtOldPassword.Text) }));
        if (Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
        if (Count == 0)
        {
            Config.MsgBoxAjax("Köhnə şifrə düzgün daxil edilməyib.");
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"Password",Config.SHA1Special(TxtNewPassword.Text) },
            {"WhereUsersID",DALC._GetUsersLogin.ID }
        };

        int Check = DALC.UpdateDatabase(Tools.Table.UsersAccounts, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);
    }

}