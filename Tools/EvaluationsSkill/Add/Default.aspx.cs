using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_EvaluationsSkill_Add_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;    

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) ||
            Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindRpt()
    {
        DataTable Dt = DALC.GetList(Tools.Table.EvaluationsSkillGroups);

        RptTab.DataSource =
        RptContent.DataSource = DALC.GetList(Tools.Table.EvaluationsSkillGroups);
        RptTab.DataBind();
        RptContent.DataBind();

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
            ((Literal)Master.FindControl("LtrTitle")).Text = "Daxili qiymətləndirmə 2";
            ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

            BindRpt();
        }
    }

    protected void RptContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string EvaluationsSkillGroupsID = (e.Item.FindControl("HdnEvaluationsSkillGroupsID") as HiddenField).Value;

            Repeater RptQuestions = e.Item.FindControl("RptQuestions") as Repeater;
            Repeater RptHeader = e.Item.FindControl("RptHeader") as Repeater;
            Repeater RptBody = e.Item.FindControl("RptBody") as Repeater;

            DataTable Dt = DALC.GetDataTableBySqlCommand("GetEvaluationsSkill", new string[] { "ApplicationsPersonsID", "EvaluationsSkillGroupsID" }, new object[] { _ApplicationsPersonsID, EvaluationsSkillGroupsID }, CommandType.StoredProcedure);

            if (Dt == null || Dt.Rows.Count < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            string IncompleteID = DALC.CheckEvaluationsSkillIsCompleted(_ApplicationsPersonsID);

            if (IncompleteID == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            RptQuestions.DataSource = Dt;
            RptQuestions.DataBind();

            DataTable DtColum = new DataTable();
            DtColum.Columns.Add("EvaluationsSkillID", typeof(int));
            DtColum.Columns.Add("Create_Dt", typeof(string));
            DtColum.Columns.Add("DisplayName", typeof(string));

            string EvaluationsSkillID;
            string EvaluationsSkillCreateDt;

            DataRow DrHeader;
            for (int i = 2; i < Dt.Columns.Count; i++)
            {
                EvaluationsSkillID = Dt.Columns[i].ColumnName.Split(',')[0];
                EvaluationsSkillCreateDt = Dt.Columns[i].ColumnName.Split(',')[1];

                DrHeader = DtColum.NewRow();
                DrHeader["EvaluationsSkillID"] = EvaluationsSkillID;
                DrHeader["Create_Dt"] = Dt.Columns[i].ColumnName;
                DrHeader["DisplayName"] = string.Format("{0} {1} {2}", EvaluationsSkillCreateDt.Split('-')[0], Config.GetMonthName(int.Parse(EvaluationsSkillCreateDt.Split('-')[1])), EvaluationsSkillCreateDt.Split('-')[2]);

                DtColum.Rows.Add(DrHeader);
            }

            RptHeader.DataSource = DtColum;
            RptHeader.DataBind();

            DataTable DtBody = new DataTable();
            DtBody.Columns.Add("Body", typeof(string));
            StringBuilder StringRows = new StringBuilder();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                foreach (DataRow Dr in DtColum.Rows)
                {
                    string PointsID = Config.Split(Dt._Rows(Dr["Create_Dt"]._ToString(), i), '-', 0, "");
                    string Points = Config.Split(Dt._Rows(Dr["Create_Dt"]._ToString(), i), '-', 1, "");

                    if (Dr["EvaluationsSkillID"]._ToString() == IncompleteID)
                    {

                        StringRows.Append("<td class=\"cell100 column2\">" +
                                              string.Format("<i class=\"fa fa-plus {0}\" onclick=\"selectValue({1},{2},10)\"></i>", PointsID == Tools.EvaluationsSkillPoints.İcra_edir.ToString("d") ? "active" : "", Dr["EvaluationsSkillID"], Dt._Rows("EvaluationsSkillQuestionsID", i)) +
                                              string.Format("<i class=\"fa {0}\" onclick=\"selectValue({1},{2},20)\">ŞD</i>", PointsID == Tools.EvaluationsSkillPoints.Şifahi_dəstək.ToString("d") ? "active" : "", Dr["EvaluationsSkillID"], Dt._Rows("EvaluationsSkillQuestionsID", i)) +
                                              string.Format("<i class=\"fa {0}\" onclick=\"selectValue({1},{2},30)\">FD</i>", PointsID == Tools.EvaluationsSkillPoints.Fiziki_dəstək.ToString("d") ? "active" : "", Dr["EvaluationsSkillID"], Dt._Rows("EvaluationsSkillQuestionsID", i)) +
                                              string.Format("<i class=\"fa fa-minus {0}\" onclick=\"selectValue({1},{2},40)\"></i>", PointsID == Tools.EvaluationsSkillPoints.Asılı.ToString("d") ? "active" : "", Dr["EvaluationsSkillID"], Dt._Rows("EvaluationsSkillQuestionsID", i)) +
                                          "</td>");
                    }
                    else
                    {
                        StringRows.AppendFormat("<td class=\"cell100 column2\">{0}</td>", Points);
                    }
                }

                DtBody.Rows.Add(StringRows);
                StringRows.Clear();
            }

            RptBody.DataSource = DtBody;
            RptBody.DataBind();
        }
    }

    [System.Web.Services.WebMethod]
    public static void UpdateEvaluationsSkillValues(string evaluationsSkillID, string evaluationsSkillQuestionsID, string evaluationsSkillPointsID)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("EvaluationsSkillPointsID", evaluationsSkillPointsID);
        Dictionary.Add("WhereEvaluationsSkillID", evaluationsSkillID);
        Dictionary.Add("WhereEvaluationsSkillQuestionsID", evaluationsSkillQuestionsID);

        int Check = DALC.UpdateDatabase(Tools.Table.EvaluationsSkillValues, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }



}