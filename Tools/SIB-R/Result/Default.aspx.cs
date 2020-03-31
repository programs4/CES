using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_SIB_R_Result_Default : System.Web.UI.Page
{
    int _SIBRID = 0;
    int _ApplicationsPersonsID = 0;
    int _Result;

    void CheckQuery()
    {
        try
        {
            string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
            if (Query[2] != DALC._GetUsersLogin.Key || !int.TryParse(Query[1], out _ApplicationsPersonsID) || !int.TryParse(Config._GetQueryString("id").Replace(' ', '+').Decrypt(), out _SIBRID))
            {
                Response.Write("Məlumat tapılmadı.");
                Response.End();
                return;
            }
        }
        catch
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindRptSIBRAdaptiveCalculation()
    {

        int SIBRTypesID = int.Parse(DALC.GetSingleValues("SIBRTypesID", Tools.Table.SIBR, "ID", _SIBRID, ""));
        PnlCluster.Visible = (SIBRTypesID == (int)Tools.SIBRTypes.Full_Scale);
        RptSIBRScoringTypesGroups.DataSource = DALC.GetSIBRScoringTypesGroups();
        RptSIBRScoringTypesGroups.DataBind();

        RptSIBRAdaptiveScores.DataSource = DALC.GetSIBRAdaptiveScores(_SIBRID);
        RptSIBRAdaptiveScores.DataBind();

        DataTable DtAdaptiveResult = DALC.GetSIBRAdaptiveResult(_SIBRID);
        RptSIBRAdaptiveResult.DataSource = DtAdaptiveResult;
        RptSIBRAdaptiveResult.DataBind();

        RptSIBRAdaptiveSubscalesScores.DataSource = DALC.GetSIBRAdaptiveSubscalesScores(_SIBRID);
        RptSIBRAdaptiveSubscalesScores.DataBind();

        RptSIBRAdaptiveClustersScores.DataSource = DtAdaptiveResult;
        RptSIBRAdaptiveClustersScores.DataBind();
    }

    private void BindMaladaptive()
    {
        DataTable Dt = DALC.GetSIBRMaladaptiveScores(_SIBRID);
        if (Dt == null || Dt.Rows.Count < 1)
        {
            return;
        }

        LblIMI.Text = Dt._Rows("Internalized");
        LblAMI.Text = Dt._Rows("Asocial");
        LblEMI.Text = Dt._Rows("Externalized");
        LblGMI.Text = Dt._Rows("General");

        LblIMI_N.Text = (int.Parse(LblIMI.Text) - 3)._ToString();
        LblIMI_P.Text = (int.Parse(LblIMI.Text) + 3)._ToString();

        LblAMI_N.Text = (int.Parse(LblAMI.Text) - 4)._ToString();
        LblAMI_P.Text = (int.Parse(LblAMI.Text) + 4)._ToString();

        LblEMI_N.Text = (int.Parse(LblEMI.Text) - 3)._ToString();
        LblEMI_P.Text = (int.Parse(LblEMI.Text) + 3)._ToString();

        LblGMI_N.Text = (int.Parse(LblGMI.Text) - 2)._ToString();
        LblGMI_P.Text = (int.Parse(LblGMI.Text) + 2)._ToString();

        lblSupporScoreBI_W.Text = LtrBI.Text;
        lblSupporScoreGMI.Text = LblGMI.Text;
        lblSupporScore.Text = Dt._Rows("SupportScore");
        lblSupportLevel.Text = DALC.GetSIBRMaladaptiveSupportScore(int.Parse(lblSupporScore.Text));

    }

    private void InsertSIBRAdaptiveSubscalesScore()
    {
        DataTable Dt = DALC.GetSIBRAdaptiveSubscalesScores(_SIBRID);

        if (Dt == null || Dt.Rows.Count > 0)
        {
            return;
        }

        DataTable DtSIBR = DALC.GetSIBR(_SIBRID);

        if (DtSIBR == null || DtSIBR.Rows.Count < 1)
        {
            return;
        }

        int AgeYear, AgeMonth;
        if (!int.TryParse(DtSIBR._Rows("AgeYear"), out AgeYear))
        {
            return;
        }
        if (!int.TryParse(DtSIBR._Rows("AgeMonth"), out AgeMonth))
        {
            return;
        }

        //SIB-R id-ye gore melumatlari getirek ve NormJ table-inda gelen melumatlari SIBRAdaptiveSubscalesScores-a insert edek
        DataTable DtAdaptiveScores = DALC.GetSIBRAdaptiveScores(_SIBRID, "ID,W");
        DataTable DtNormJ = DALC.GetSIBRNormJ(AgeYear, AgeMonth);

        if (DtAdaptiveScores == null || DtNormJ == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (DtAdaptiveScores.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Adaptive Scores cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        if (DtNormJ.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Norm J cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        DataTable DtSIBRAdaptiveSubscalesScores = new DataTable();
        DtSIBRAdaptiveSubscalesScores.Columns.Add("SIBRAdaptiveScoresID", typeof(int));
        DtSIBRAdaptiveSubscalesScores.Columns.Add("REFW", typeof(int));
        DtSIBRAdaptiveSubscalesScores.Columns.Add("SIBRAdaptiveSkillLevelsID", typeof(byte));

        int W;
        int REFW;
        int DIF;
        int SIBRAdaptiveSkillLevelsID;
        for (int i = 0; i < DtAdaptiveScores.Rows.Count; i++)
        {
            W = int.Parse(DtAdaptiveScores._Rows("W", i));
            REFW = int.Parse(DtNormJ._Rows("REFW", i));
            DIF = W - REFW;
            SIBRAdaptiveSkillLevelsID = DALC.GetSIBRAdaptiveSkillLevels(DIF);
            if (SIBRAdaptiveSkillLevelsID == -1)
            {
                Config.MsgBoxAjax("Adaptive Behavior Skill Levels cədvəlindən məlumat tapılmadı.");
                return;
            }
            DtSIBRAdaptiveSubscalesScores.Rows.Add(int.Parse(DtAdaptiveScores._Rows("ID", i)), REFW, SIBRAdaptiveSkillLevelsID);
        }

        _Result = DALC.InsertBulk(Tools.Table.SIBRAdaptiveSubscalesScores, DtSIBRAdaptiveSubscalesScores);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    private void BindChart()
    {
        DataTable DtSIBRAdaptiveResult = DALC.GetSIBRAdaptiveResultForChart(_ApplicationsPersonsID);
        foreach (DataRow Dr in DtSIBRAdaptiveResult.Rows)
        {
            switch (Dr["SIBRScoringTypesGroupsID"]._ToInt16())
            {
                case (int)Tools.SIBRScoringTypesGroups.Motor_Skills:
                    HdnMotorSkillsSumW.Value += string.Format("{0},", Dr["SumW"]);
                    HdnMotorSkillsREFW.Value += string.Format("{0},", Dr["REFW"]);
                    HdnMotorSkillsDate.Value += string.Format("{0} {1},", int.Parse(((DateTime)Dr["Create_Dt"]).ToString("MM")).GetMonthName(), ((DateTime)Dr["Create_Dt"]).ToString("yyyy"));
                    break;
                case (int)Tools.SIBRScoringTypesGroups.Social_Interaction_Communication_Skills:
                    HdnSocialInteractionSumW.Value += string.Format("{0},", Dr["SumW"]);
                    HdnSocialInteractionREFW.Value += string.Format("{0},", Dr["REFW"]);
                    HdnSocialInteractionDate.Value += string.Format("{0} {1},", int.Parse(((DateTime)Dr["Create_Dt"]).ToString("MM")).GetMonthName(), ((DateTime)Dr["Create_Dt"]).ToString("yyyy"));
                    break;
                case (int)Tools.SIBRScoringTypesGroups.Personal_Living_Skills:
                    HdnPersonalLivingSumW.Value += string.Format("{0},", Dr["SumW"]);
                    HdnPersonalLivingREFW.Value += string.Format("{0},", Dr["REFW"]);
                    HdnPersonalLivingDate.Value += string.Format("{0} {1},", int.Parse(((DateTime)Dr["Create_Dt"]).ToString("MM")).GetMonthName(), ((DateTime)Dr["Create_Dt"]).ToString("yyyy"));
                    break;
                case (int)Tools.SIBRScoringTypesGroups.Community_Living_Skills:
                    HdnCommunityLivingSumW.Value += string.Format("{0},", Dr["SumW"]);
                    HdnCommunityLivingREFW.Value += string.Format("{0},", Dr["REFW"]);
                    HdnCommunityLivingDate.Value += string.Format("{0} {1},", int.Parse(((DateTime)Dr["Create_Dt"]).ToString("MM")).GetMonthName(), ((DateTime)Dr["Create_Dt"]).ToString("yyyy"));
                    break;
                case (int)Tools.SIBRScoringTypesGroups.Broad_Independence_Full_Scale:
                    HdnBroadIndependenceSumW.Value += string.Format("{0},", Dr["SumW"]);
                    HdnBroadIndependenceREFW.Value += string.Format("{0},", Dr["REFW"]);
                    HdnBroadIndependenceDate.Value += string.Format("{0} {1},", int.Parse(((DateTime)Dr["Create_Dt"]).ToString("MM")).GetMonthName(), ((DateTime)Dr["Create_Dt"]).ToString("yyyy"));
                    break;
                default:
                    break;
            }
        }

        HdnMotorSkillsSumW.Value = HdnMotorSkillsSumW.Value.Trim(',');
        HdnMotorSkillsREFW.Value = HdnMotorSkillsREFW.Value.Trim(',');
        HdnMotorSkillsDate.Value = HdnMotorSkillsDate.Value.Trim(',');

        HdnSocialInteractionSumW.Value = HdnSocialInteractionSumW.Value.Trim(',');
        HdnSocialInteractionREFW.Value = HdnSocialInteractionREFW.Value.Trim(',');
        HdnSocialInteractionDate.Value = HdnSocialInteractionDate.Value.Trim(',');

        HdnPersonalLivingSumW.Value = HdnPersonalLivingSumW.Value.Trim(',');
        HdnPersonalLivingREFW.Value = HdnPersonalLivingREFW.Value.Trim(',');
        HdnPersonalLivingDate.Value = HdnPersonalLivingDate.Value.Trim(',');

        HdnCommunityLivingSumW.Value = HdnCommunityLivingSumW.Value.Trim(',');
        HdnCommunityLivingREFW.Value = HdnCommunityLivingREFW.Value.Trim(',');
        HdnCommunityLivingDate.Value = HdnCommunityLivingDate.Value.Trim(',');

        HdnBroadIndependenceSumW.Value = HdnBroadIndependenceSumW.Value.Trim(',');
        HdnBroadIndependenceREFW.Value = HdnBroadIndependenceREFW.Value.Trim(',');
        HdnBroadIndependenceDate.Value = HdnBroadIndependenceDate.Value.Trim(',');
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
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
            ((Literal)Master.FindControl("LtrTitle")).Text = "SIB-R qiymətləndirmənin nəticəsi";
            ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

            InsertSIBRAdaptiveSubscalesScore();
            BindRptSIBRAdaptiveCalculation();
            BindMaladaptive();
            BindChart();
        }
    }

    int count = 0;
    decimal MS = 0, SC = 0, PL = 0, CL = 0, BI = 0, W = 0;
    protected void RptSIBRAdaptiveScores_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal LtrStart = (Literal)e.Item.FindControl("LtrStart");
            Literal LtrEnd = (Literal)e.Item.FindControl("LtrEnd");
            HiddenField HdnHdnSIBRScoringTypesID = (HiddenField)e.Item.FindControl("HdnSIBRScoringTypesID");
            HiddenField HdnW = (HiddenField)e.Item.FindControl("HdnW");
            decimal.TryParse(HdnW.Value, out W);

            if (Tools.SIBRScoringTypesGroups.Motor_Skills.ToDescriptionString().Contains(string.Format(",{0},", HdnHdnSIBRScoringTypesID.Value)))
            {
                LtrStart.Visible = count == 0;
                count++;
                MS += W;
                if (count == 2)
                {
                    count = 0;
                    LtrEnd.Visible = true;
                    MS = Math.Round(MS / 2);
                    BI += MS;
                    LtrMS.Text = LtrBIMS.Text = MS._ToString();
                }

            }
            else if (Tools.SIBRScoringTypesGroups.Social_Interaction_Communication_Skills.ToDescriptionString().Contains(string.Format(",{0},", HdnHdnSIBRScoringTypesID.Value)))
            {
                if (count == 0)
                {
                    LtrStart.Visible = true;
                }
                count++;
                SC += W;
                if (count == 3)
                {
                    count = 0;
                    LtrEnd.Visible = true;
                    SC = Math.Round(SC / 3);
                    BI += SC;
                    LtrSC.Text = LtrBISC.Text = SC._ToString();
                }
            }
            else if (Tools.SIBRScoringTypesGroups.Personal_Living_Skills.ToDescriptionString().Contains(string.Format(",{0},", HdnHdnSIBRScoringTypesID.Value)))
            {
                if (count == 0)
                {
                    LtrStart.Visible = true;
                }
                count++;
                PL += W;
                if (count == 5)
                {
                    count = 0;
                    LtrEnd.Visible = true;
                    PL = Math.Round(PL / 5);
                    BI += PL;
                    LtrPL.Text = LtrBIPL.Text = PL._ToString();
                }
            }
            else if (Tools.SIBRScoringTypesGroups.Community_Living_Skills.ToDescriptionString().Contains(string.Format(",{0},", HdnHdnSIBRScoringTypesID.Value)))
            {
                if (count == 0)
                {
                    LtrStart.Visible = true;
                }
                count++;
                CL += W;
                if (count == 4)
                {
                    count = 0;
                    LtrEnd.Visible = count == 0;
                    CL = Math.Round(CL / 4);
                    BI += CL;
                    LtrCL.Text = LtrBICL.Text = CL._ToString();
                    BI = Math.Round(BI / 4);
                    LtrBI.Text = BI._ToString();
                }
            }
        }
    }

}