using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_SIB_R_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Config.RedirectURL("/tools/applicationspersons/?type=sibr-evaluations");
            return;
        }
    }

    private void BindGrdList()
    {
        GrdSIBR.DataSource = DALC.GetSIBRByApplicationsPersonsID(_ApplicationsPersonsID);
        GrdSIBR.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.SIB_R))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            BindGrdList();
        }

        ((Literal)Master.FindControl("LtrTitle")).Text = "SIB-R qiymətləndirmə";
        ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

        HyperLink HyperLnk1 = (HyperLink)HeaderTab.FindControl("HrpTab1");
        HyperLink HyperLnk2 = (HyperLink)HeaderTab.FindControl("HrpTab2");
        HyperLink HyperLnk3 = (HyperLink)HeaderTab.FindControl("HrpTab3");

        int EvaluationsCount = DALC.GetEvaluationsCountByPersonsID(_ApplicationsPersonsID);
        int EvaluationsSkillCount = DALC.GetEvaluationsSkillCountByPersonsID(_ApplicationsPersonsID);
        int SIBRCount = DALC.GetSIBRCountByApplicationsPersonsID(_ApplicationsPersonsID);

        HyperLnk1.Text = string.Format(HyperLnk1.Text, SIBRCount);
        HyperLnk1.NavigateUrl = string.Format(HyperLnk1.NavigateUrl, Config._GetQueryString("i").Replace(' ', '+'));
        HyperLnk1.CssClass = "active";

        HyperLnk2.Text = string.Format(HyperLnk2.Text, EvaluationsCount);
        HyperLnk2.NavigateUrl = string.Format(HyperLnk2.NavigateUrl, Config._GetQueryString("i").Replace(' ', '+'));

        HyperLnk3.Text = string.Format(HyperLnk3.Text, EvaluationsSkillCount);
        HyperLnk3.NavigateUrl = string.Format(HyperLnk3.NavigateUrl, Config._GetQueryString("i").Replace(' ', '+'));
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)(sender);

        int _Result = DALC.UpdateDatabase(Tools.Table.SIBR,
                                          new string[] { "SIBRStatusID", "WhereID" },
                                          new object[] { (int)Tools.SIBRStatus.Deaktiv, int.Parse(Lnk.CommandArgument) });

        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdList();
    }

}