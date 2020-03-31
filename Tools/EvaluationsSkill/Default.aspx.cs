using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_EvaluationsSkill_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Config.RedirectURL("/tools/applicationspersons/?type=evaluationsskill");
            return;
        }
    }

    private void BindGrdEvaluations()
    {
        GrdEvaluationsSkill.DataSource = DALC.GetEvaluationsSkillByPersonsID(_ApplicationsPersonsID);
        GrdEvaluationsSkill.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Qiymətləndirilənlər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        ((Literal)Master.FindControl("LtrTitle")).Text = "Daxili qiymətləndirmə 2";
        ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

        HyperLink HyperLnk1 = (HyperLink)HeaderTab.FindControl("HrpTab1");
        HyperLink HyperLnk2 = (HyperLink)HeaderTab.FindControl("HrpTab2");
        HyperLink HyperLnk3 = (HyperLink)HeaderTab.FindControl("HrpTab3");

        int EvaluationsCount = DALC.GetEvaluationsCountByPersonsID(_ApplicationsPersonsID);
        int SIBRCount = DALC.GetSIBRCountByApplicationsPersonsID(_ApplicationsPersonsID);
        int EvaluationsSkillCount = DALC.GetEvaluationsSkillCountByPersonsID(_ApplicationsPersonsID);

        HyperLnk1.Text = string.Format(HyperLnk1.Text, SIBRCount);
        HyperLnk1.NavigateUrl = string.Format("/tools/sib-r/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));

        HyperLnk2.Text = string.Format(HyperLnk2.Text, EvaluationsCount);
        HyperLnk2.NavigateUrl = string.Format("/tools/evaluations/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));

        HyperLnk3.Text = string.Format(HyperLnk3.Text, EvaluationsSkillCount);
        HyperLnk3.NavigateUrl = string.Format("/tools/evaluationsskill/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));
        HyperLnk3.CssClass = "active";

        if (!IsPostBack)
        {
            BindGrdEvaluations();
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        DListCompleted.SelectedIndex = 0;
        TxtDate.Text = "";
        PnlCompleted.Visible = false;
        PnlModalFooter.Visible = true;
        BtnSave.CommandArgument = "add";
        string EvaluationsSkillID = DALC.CheckEvaluationsSkillIsCompleted(_ApplicationsPersonsID);
        if (EvaluationsSkillID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (!string.IsNullOrEmpty(EvaluationsSkillID))
        {
            Config.RedirectURL(string.Format("/tools/evaluationsskill/add/?i={0}", Config._GetQueryString("i")));
            return;
        }

        Config.Modal();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        object Date = Config.DateTimeFormat(TxtDate.Text);
        if (Date == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün qeyd edin!");
            return;
        }

        int Check;

        if (BtnSave.CommandArgument == "add")
        {
            Check = DALC.CheckEvaluationsSkillByPersonsID(_ApplicationsPersonsID, ((DateTime)Date).ToString("yyyy-MM-dd"));
            if (Check < 0)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            if (Check > 0)
            {
                Config.MsgBoxAjax("Bu tarixdə qiymətləndirmə əlavə olunub.");
                return;
            }

            Check = DALC.ExecuteProcedure("InsertEvaluationsSkill",
                "ApplicationsPersonsID,UsersID,Create_Dt,Add_Dt,Add_Ip",
                new object[]
                {
                _ApplicationsPersonsID,
                DALC._GetUsersLogin.ID,
                Date,
                DateTime.Now,
                Request.UserHostAddress.IPToInteger()
                });

            if (Check < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            Config.RedirectURL(string.Format("/tools/evaluationsskill/add/?i={0}", Config._GetQueryString("i")));

        }
        else
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            Dictionary.Add("Create_Dt", Date);
            Dictionary.Add("IsCompleted", int.Parse(DListCompleted.SelectedValue));
            Dictionary.Add("WhereID", int.Parse(BtnSave.CommandName));

            Check = DALC.UpdateDatabase(Tools.Table.EvaluationsSkill, Dictionary);

            if (Check < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            Config.MsgBoxAjax(Config._DefaultSuccessMessages, string.Format("/tools/evaluationsskill/?i={0}", Config._GetQueryString("i")));

        }
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        string EvaluationsSkillID = (sender as LinkButton).CommandArgument;
        DataTable Dt = DALC.GetEvaluationsSkillByID(int.Parse(EvaluationsSkillID));
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BtnSave.CommandName = EvaluationsSkillID;
        BtnSave.CommandArgument = "edit";
        PnlCompleted.Visible = true;
        TxtDate.Text = ((DateTime)Dt._RowsObject("Create_Dt")).ToString("dd.MM.yyyy");
        DListCompleted.SelectedValue = Dt._RowsObject("IsCompleted")._ToInt16().ToString();
        DListCompleted.Enabled = PnlModalFooter.Visible = DListCompleted.SelectedValue == "0";

        Config.Modal();
    }
}