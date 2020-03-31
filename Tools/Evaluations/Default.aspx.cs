using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Evaluations_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Config.RedirectURL("/tools/applicationspersons/?type=evaluations");
            return;
        }
    }

    private void BindGrdEvaluations()
    {
        GrdEvaluations.DataSource = DALC.GetEvaluationsByPersonsID(_ApplicationsPersonsID);
        GrdEvaluations.DataBind();
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

        if (!IsPostBack)
        {
            BindGrdEvaluations();
        }
        ((Literal)Master.FindControl("LtrTitle")).Text = "Daxili qiymətləndirmə";
        ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

        HyperLink HyperLnk1 = (HyperLink)HeaderTab.FindControl("HrpTab1");
        HyperLink HyperLnk2 = (HyperLink)HeaderTab.FindControl("HrpTab2");
        HyperLink HyperLnk3 = (HyperLink)HeaderTab.FindControl("HrpTab3");

        int EvaluationsCount = DALC.GetEvaluationsCountByPersonsID(_ApplicationsPersonsID);
        int EvaluationsSkillCount = DALC.GetEvaluationsSkillCountByPersonsID(_ApplicationsPersonsID);
        int SIBRCount = DALC.GetSIBRCountByApplicationsPersonsID(_ApplicationsPersonsID);

        HyperLnk1.Text = string.Format(HyperLnk1.Text, SIBRCount);
        HyperLnk1.NavigateUrl = string.Format("/tools/sib-r/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));

        HyperLnk2.Text = string.Format(HyperLnk2.Text, EvaluationsCount);
        HyperLnk2.NavigateUrl = string.Format("/tools/evaluations/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));
        HyperLnk2.CssClass = "active";

        HyperLnk3.Text = string.Format(HyperLnk3.Text, EvaluationsSkillCount);
        HyperLnk3.NavigateUrl = string.Format("/tools/evaluationsskill/?i={0}", Config._GetQueryString("i").Replace(' ', '+'));
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)(sender);

        int _Result = DALC.UpdateDatabase(Tools.Table.Evaluations,
                                          new string[] { "IsActive", "WhereID" },
                                          new object[] { false, int.Parse(Lnk.CommandArgument) });

        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdEvaluations();
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        //Sessiyani sifirlayaqki yeni duymesine klik etdikde kohne melumatlari achmasin
        Session["_EvaluationsID"] = null;
        Config.RedirectURL(string.Format("/tools/evaluations/add/?i={0}&id={1}", Config._GetQueryString("i"), "0".Encrypt()));
    }
}