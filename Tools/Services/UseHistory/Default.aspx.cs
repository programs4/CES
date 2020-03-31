using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Services_UseHistory_Default : System.Web.UI.Page
{
    int _ServicesID = 0;
    int _ApplicationsPersonsID = 0;
    int _Result = 0;

    private void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ServicesID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindGrdServicesUseHistory()
    {
        GrdServicesUseHistory.DataSource = DALC.GetApplicationsPersonsServicesUseHistory(_ApplicationsPersonsID, _ServicesID);
        GrdServicesUseHistory.DataBind();
    }

    private void BindDList()
    {
        DListUsers.DataSource = DALC.GetUsersList();
        DListUsers.DataBind();
        DListUsers.Items.Insert(0, new ListItem("--", "-1"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Xidmətdən_istifadələr))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Xidmətdən istifadə tarixcəsi.";
            ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

            BindDList();
            BindGrdServicesUseHistory();
        }
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"WhereID",int.Parse((sender as LinkButton).CommandArgument) },
            {"IsActive",false },
        };
        _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsPersonsServicesUseHistory, Dictionary);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    protected void LnkAddUsehistory_Click(object sender, EventArgs e)
    {
        BtnSave.CommandArgument = "0";
        LtrTitle.Text = "Yeni əlavə";
        DListUsers.SelectedValue = "-1";
        TxtDescription.Text = TxtUseDate.Text = "";
        Config.Modal();
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (sender as LinkButton);
        BtnSave.CommandArgument = Lnk.CommandArgument;

        DataTable Dt = DALC.GetApplicationsPersonsServicesUseHistoryByID(int.Parse(Lnk.CommandArgument));

        DListUsers.SelectedValue = Dt._Rows("UsersID");
        TxtUseDate.Text = ((DateTime)Dt._RowsObject("Use_Dt")).ToString("dd.MM.yyyy");
        TxtDescription.Text = Dt._Rows("Description");

        LtrTitle.Text = "Düzəlt";
        BtnSave.Text = "Yadda saxla";
        Config.Modal();

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (DListUsers.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Xidmət göstərəni seçin");
            Config.Modal();
            return;
        }

        object Date = Config.DateTimeFormat(TxtUseDate.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün daxil edin.");
            Config.Modal();
            return;
        }

        TxtUseDate.Text = ((DateTime)Date).ToString("dd.MM.yyyy");

        var Dictionary = new Dictionary<string, object>()
        {
            {"UsersID",int.Parse(DListUsers.SelectedValue) },
            {"ApplicationsPersonsID",_ApplicationsPersonsID },
            {"ServicesID",_ServicesID },
            {"Description",TxtDescription.Text },
            {"Use_Dt", (DateTime)Date},
            {"IsActive",true },
        };

        if (BtnSave.CommandArgument == "0")
        {
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            _Result = DALC.InsertDatabase(Tools.Table.ApplicationsPersonsServicesUseHistory, Dictionary);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }
        else
        {
            Dictionary.Add("WhereID", int.Parse(BtnSave.CommandArgument));
            _Result = DALC.UpdateDatabase(Tools.Table.ApplicationsPersonsServicesUseHistory, Dictionary);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages);
        BindGrdServicesUseHistory();
        BtnSave.CommandArgument = "0";
    }

}