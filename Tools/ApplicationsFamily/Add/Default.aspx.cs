using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Tools_ApplicationsFamily_Add_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsFamilyID = 0;
    int _Result = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _ApplicationsFamilyID) || !int.TryParse(Query[1], out _ApplicationsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindDList()
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsID",DALC._GetUsersLogin.OrganizationsID },
            {"UsersStatus(NOT IN)", string.Format("{0},{1}",(int)Tools.UsersStatus.Silinib,(int)Tools.UsersStatus.İşdən_ayrılıb)}
        };

        DListUsers.DataSource = DALC.GetUsers(Dictionary, 1, 100000).Dt;
        DListUsers.DataBind();

        DListApplicationsFamilyTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyTypes);
        DListApplicationsFamilyTypes.DataBind();
        DListApplicationsFamilyTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsFamilyStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyStatus);
        DListApplicationsFamilyStatus.DataBind();
        DListApplicationsFamilyStatus.Items.Insert(0, new ListItem("--", "-1"));

        DListApplicationsFamilyPartnersTypes.DataSource = DALC.GetList(Tools.Table.ApplicationsFamilyPartnersTypes);
        DListApplicationsFamilyPartnersTypes.DataBind();
    }

    private void BindGrdApplicationsFamily()
    {
        var Dictionary = new Dictionary<string, object>
        {
            {"ApplicationsID",_ApplicationsID},
        };

        GrdApplicationsFamily.DataSource = DALC.GetFilterList(Tools.Table.V_ApplicationsFamily, Dictionary, 1, 10000).Dt;
        GrdApplicationsFamily.DataBind();

        if (GrdApplicationsFamily.Rows.Count < 1)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }

    private void GenerateControls(int index)
    {
        Label Lbl = new Label();
        Lbl.ID = "Lbl" + index.ToString();
        Lbl.Text = DListApplicationsFamilyPartnersTypes.Items[index].Text + " (Əməkdaşların adı və soyadı)";
        PnlPersons.Controls.Add(Lbl);


        TextBox Txt = new TextBox();
        Txt.ID = "Txt" + DListApplicationsFamilyPartnersTypes.Items[index].Value;
        Txt.CssClass = "form-control";
        Txt.TextMode = TextBoxMode.MultiLine;
        Txt.Height = 50;
        PnlPersons.Controls.Add(Txt);

        PnlPersons.Controls.Add(new LiteralControl("<br/><br/>"));
    }

    private void TarnsactionCommitOrRollback(DALC.Transaction Transaction, bool IsCommit = false)
    {
        if (IsCommit)
        {
            Transaction.SqlTransaction.Commit();
        }
        else
        {
            Transaction.SqlTransaction.Rollback();
        }

        Transaction.Com.Connection.Close();
        Transaction.Com.Connection.Dispose();
        Transaction.Com.Dispose();
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "$('.multiSelect').multiselect({buttonWidth: '100%',numberDisplayed: 5,});", true);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "DateTime", "dateTime();", true);
    }

    private bool Validations()
    {
        int UsersCount = 0;
        for (int i = 0; i < DListUsers.Items.Count; i++)
        {
            if (DListUsers.Items[i].Selected)
            {
                UsersCount++;
            }
        }

        if (UsersCount == 0)
        {
            Config.MsgBoxAjax("Səfər edən əməkdaşları seçin!");
            return false;
        }

        if (DListApplicationsFamilyTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Səfərin məqsədini seçin!");
            return false;
        }

        if (DListApplicationsFamilyStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Səfərin statusunu seçin!");
            return false;
        }

        if (string.IsNullOrEmpty(TxtAddress.Text))
        {
            Config.MsgBoxAjax("Səfər ünvanını qeyd edin");
            return false;
        }

        object Datet = Config.DateTimeFormat(TxtDate.Text);

        if (Datet == null)
        {
            Config.MsgBoxAjax("Səfər tarixini düzgün qeyd edin.");
            return false;
        }

        TxtDate.Text = ((DateTime)Datet).ToString("dd.MM.yyyy");

        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Ailə_səfərləri))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Seçilmiş müraciətə edilmiş səfərlər";

            BindDList();

            if (_ApplicationsFamilyID == 0)
            {
                //Egere Muracietler bolmesinden gelirse _ApplicationsFamilyID hemishe 0 olur
                //cunki muraciete edilmish seferleri acmalidir eger yoxdusa avtomatik yeni elave bolmesini acacaq
                BindGrdApplicationsFamily();
            }
            else
            {
                //Eks halda edit bolmesini achacaq
                LnkEdit_Click(null, null);
            }
        }

        LoadScriptManager();
        DListApplicationsFamilyPartnersTypes_SelectedIndexChanged(null, null);
    }

    protected void DListApplicationsFamilyPartnersTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DListApplicationsFamilyPartnersTypes.Items.Count; i++)
        {
            if (PnlPersons.FindControl("Txt" + DListApplicationsFamilyPartnersTypes.Items[i].Value) == null)
            {
                if (DListApplicationsFamilyPartnersTypes.Items[i].Selected)
                {
                    GenerateControls(i);
                }
            }
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        ((Literal)Master.FindControl("LtrTitle")).Text = "Seçilmiş müraciətə yeni səfər əlavə et";
        BtnSave.CommandArgument = "0";
        MultiView1.ActiveViewIndex = 0;
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        ((Literal)Master.FindControl("LtrTitle")).Text = "Seçilmiş səfər üzərində düzəliş";
        if (sender != null)
        {
            BtnSave.CommandArgument = (sender as LinkButton).CommandArgument;
            BtnSave.CommandName = (sender as LinkButton).CommandName;
            _ApplicationsFamilyID = int.Parse(BtnSave.CommandArgument);
        }

        //ApplicationsFamily ID ye gore melulatlari getirek
        DataTable Dt = DALC.GetApplicationsFamilyByID(_ApplicationsFamilyID);
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DListApplicationsFamilyTypes.SelectedValue = Dt._Rows("ApplicationsFamilyTypesID");
        TxtAddress.Text = Dt._Rows("Address");
        DListApplicationsFamilyStatus.SelectedValue = Dt._Rows("ApplicationsFamilyStatusID");
        TxtDate.Text = ((DateTime)Dt._RowsObject("Tour_Dt")).ToString("dd.MM.yyy");
        TxtDescriptions.Text = Dt._Rows("Description");

        //ApplicationsFamilyUsers-den ApplicationsFamilyID ye gore  melumatlari getirek
        DataTable DtApplicationsFamilyUsers = DALC.GetApplicationsFamilyUsers(_ApplicationsFamilyID);
        if (DtApplicationsFamilyUsers == null || DtApplicationsFamilyUsers.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        //ApplicationsFamilyPartners-den ApplicationsFamilyID-ye gore  melumatlari getirek
        DataTable DtApplicationsFamilyPartners = DALC.GetApplicationsFamilyPartners(_ApplicationsFamilyID);
        if (DtApplicationsFamilyPartners == null || DtApplicationsFamilyPartners.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        //ApplicationsFamilyUsers-den gelen melumatlara gore forla listbox-da secek
        foreach (DataRow Dr in DtApplicationsFamilyUsers.Rows)
        {
            for (int i = 0; i < DListUsers.Items.Count; i++)
            {
                if (!DListUsers.Items[i].Selected)
                {
                    DListUsers.Items[i].Selected = (DListUsers.Items[i].Value == Dr["UsersID"]._ToString());
                }
            }
        }

        //ApplicationsFamilyPartners-den gelen melumatlara gore forla listbox-da secek ve control generate edek 
        foreach (DataRow Dr in DtApplicationsFamilyPartners.Rows)
        {
            for (int i = 0; i < DListApplicationsFamilyPartnersTypes.Items.Count; i++)
            {
                if (!DListApplicationsFamilyPartnersTypes.Items[i].Selected)
                {
                    if (DListApplicationsFamilyPartnersTypes.Items[i].Value == Dr["ApplicationsFamilyPartnersTypesID"]._ToString())
                    {
                        DListApplicationsFamilyPartnersTypes.Items[i].Selected = true;
                        GenerateControls(i);
                        ((TextBox)PnlPersons.FindControl("Txt" + Dr["ApplicationsFamilyPartnersTypesID"])).Text = Dr["PersonsFullname"]._ToString();
                    }
                }
            }
        }

        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (!Validations())
        {
            return;
        }

        if (_ApplicationsFamilyID == 0)
        {
            _ApplicationsFamilyID = int.Parse(BtnSave.CommandArgument);
        }
        if (_ApplicationsID == 0)
        {
            _ApplicationsID = int.Parse(BtnSave.CommandName);
        }

        DALC.Transaction Transaction = new DALC.Transaction();
        var Dictionary = new Dictionary<string, object>()
        {
            {"ApplicationsID", _ApplicationsID},
            {"ApplicationsFamilyTypesID",int.Parse(DListApplicationsFamilyTypes.SelectedValue)},
            {"Address",TxtAddress.Text },
            {"ApplicationsFamilyStatusID",int.Parse(DListApplicationsFamilyStatus.SelectedValue)},
            {"Tour_Dt",TxtDate.Text.DateTimeFormat() },
            {"Description", TxtDescriptions.Text},

        };

        //Insert edek
        if (_ApplicationsFamilyID == 0)
        {
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            _Result = DALC.InsertDatabase(Tools.Table.ApplicationsFamily, Dictionary, Transaction);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            _ApplicationsFamilyID = _Result;

            DataTable DtUsers = new DataTable();
            DtUsers.Columns.Add("ApplicationsFamilyID", typeof(int));
            DtUsers.Columns.Add("UsersID", typeof(int));
            DtUsers.Columns.Add("IsDeleted", typeof(bool));

            for (int i = 0; i < DListUsers.Items.Count; i++)
            {
                if (DListUsers.Items[i].Selected)
                {
                    DtUsers.Rows.Add(_ApplicationsFamilyID, DListUsers.Items[i].Value, false);
                }
            }

            _Result = DALC.InsertBulk(Tools.Table.ApplicationsFamilyUsers, DtUsers, Transaction);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }


            DataTable DtPartners = new DataTable();
            DtPartners.Columns.Add("ApplicationsFamilyID", typeof(int));
            DtPartners.Columns.Add("ApplicationsFamilyPartnersTypesID", typeof(int));
            DtPartners.Columns.Add("PersonsFullname", typeof(string));
            DtPartners.Columns.Add("IsDeleted", typeof(bool));

            for (int i = 0; i < DListApplicationsFamilyPartnersTypes.Items.Count; i++)
            {
                if (DListApplicationsFamilyPartnersTypes.Items[i].Selected)
                {
                    DtPartners.Rows.Add(_ApplicationsFamilyID, DListApplicationsFamilyPartnersTypes.Items[i].Value, ((TextBox)PnlPersons.FindControl(string.Format("Txt{0}", DListApplicationsFamilyPartnersTypes.Items[i].Value))).Text, false);
                }
            }

            if (DtPartners.Rows.Count > 0)
            {
                _Result = DALC.InsertBulk(Tools.Table.ApplicationsFamilyPartners, DtPartners, Transaction, true);
                if (_Result < 1)
                {
                    Config.MsgBoxAjax(Config._DefaultErrorMessages);
                    return;
                }
            }
            else
            {
                TarnsactionCommitOrRollback(Transaction, true);
            }
        }
        else
        {
            Dictionary.Add("WhereID", _ApplicationsFamilyID);
            _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsFamily, Dictionary, Transaction);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            Dictionary.Clear();


            //Butun sitifadecileri IsDeleted true edek ki sonra olanlari false, olmayanlari insert edeceyik
            Dictionary.Add("ApplicationsFamilyID", _ApplicationsFamilyID);
            Dictionary.Add("IsDeleted", true);
            Dictionary.Add("WhereApplicationsFamilyID", _ApplicationsFamilyID);
            _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsFamilyUsers, Dictionary);
            if (_Result < 1)
            {
                //Eger xeta bash verirse Transactionla emeliyyatlari geri qaytaraq      
                TarnsactionCommitOrRollback(Transaction);
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            bool IsUpdate = false;
            string DataID = DALC.GetSingleValues("CONCAT(CAST(UsersID as varchar),',')", Tools.Table.ApplicationsFamilyUsers, "ApplicationsFamilyID", _ApplicationsFamilyID, "for xml path('')");
            if (DataID == "-1")
            {
                //Eger xeta bash verirse Transactionla emeliyyatlari geri qaytaraq                   
                TarnsactionCommitOrRollback(Transaction);
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            for (int i = 0; i < DListUsers.Items.Count; i++)
            {
                if (DListUsers.Items[i].Selected)
                {
                    Dictionary.Clear();
                    IsUpdate = (("," + DataID).IndexOf("," + DListUsers.Items[i].Value + ",") > -1);
                    Dictionary.Add("ApplicationsFamilyID", _ApplicationsFamilyID);
                    Dictionary.Add("UsersID", int.Parse(DListUsers.Items[i].Value));
                    Dictionary.Add("IsDeleted", false);
                    _Result = DALC.InsertOrUpdateApplicationsFamilyUsersOrPartners(Tools.Table.ApplicationsFamilyUsers, Dictionary, IsUpdate, Transaction);
                    if (_Result < 1)
                    {
                        Config.MsgBoxAjax(Config._DefaultErrorMessages);
                        return;
                    }
                }
            }

            //Butun ApplicationsFamilyPartners IsDeleted true edek ki sonra olanlari false, olmayanlari insert edeceyik
            Dictionary.Clear();
            Dictionary.Add("ApplicationsFamilyID", _ApplicationsFamilyID);
            Dictionary.Add("IsDeleted", true);
            Dictionary.Add("WhereApplicationsFamilyID", _ApplicationsFamilyID);
            _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsFamilyPartners, Dictionary);
            if (_Result < 1)
            {
                //Eger xeta bash verirse Transactionla emeliyyatlari geri qaytaraq                   
                TarnsactionCommitOrRollback(Transaction);
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            int ApplicationsFamilyPartnersTypesID = 0;
            DataID = DALC.GetSingleValues("CONCAT(CAST(ApplicationsFamilyPartnersTypesID as varchar),',')", Tools.Table.ApplicationsFamilyPartners, "ApplicationsFamilyID", _ApplicationsFamilyID, "for xml path('')");
            if (DataID == "-1")
            {
                //Eger Count alanda xeta bash verirse Transactionla emeliyyatlari geri qaytaraq                   
                Transaction.SqlTransaction.Rollback();
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            for (int i = 0; i < DListApplicationsFamilyPartnersTypes.Items.Count; i++)
            {
                if (DListApplicationsFamilyPartnersTypes.Items[i].Selected)
                {
                    Dictionary.Clear();
                    ApplicationsFamilyPartnersTypesID = int.Parse(DListApplicationsFamilyPartnersTypes.Items[i].Value);

                    IsUpdate = (("," + DataID).IndexOf("," + ApplicationsFamilyPartnersTypesID._ToString() + ",") > -1);
                    Dictionary.Add("ApplicationsFamilyID", _ApplicationsFamilyID);
                    Dictionary.Add("ApplicationsFamilyPartnersTypesID", ApplicationsFamilyPartnersTypesID);
                    Dictionary.Add("IsDeleted", false);
                    _Result = DALC.InsertOrUpdateApplicationsFamilyUsersOrPartners(Tools.Table.ApplicationsFamilyPartners, Dictionary, IsUpdate, Transaction);
                    if (_Result < 1)
                    {
                        Config.MsgBoxAjax(Config._DefaultErrorMessages);
                        return;
                    }

                    if (PnlPersons.FindControl(string.Format("Txt{0}", ApplicationsFamilyPartnersTypesID)) != null)
                    {
                        Dictionary.Clear();
                        Dictionary.Add("PersonsFullname", ((TextBox)PnlPersons.FindControl(string.Format("Txt{0}", ApplicationsFamilyPartnersTypesID))).Text);
                        Dictionary.Add("WhereApplicationsFamilyID", _ApplicationsFamilyID);
                        Dictionary.Add("WhereApplicationsFamilyPartnersTypesID", ApplicationsFamilyPartnersTypesID);
                        _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsFamilyPartners, Dictionary, Transaction);
                        if (_Result < 1)
                        {
                            Config.MsgBoxAjax(Config._DefaultErrorMessages);
                            return;
                        }
                    }
                }
            }
            TarnsactionCommitOrRollback(Transaction, true);
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, Request.RawUrl);
    }

}