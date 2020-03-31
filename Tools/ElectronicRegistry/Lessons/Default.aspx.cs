using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ElectronicRegistry_Lessons_Default : System.Web.UI.Page
{
    int _ServicesCoursesID = 0;
    int _ServicesCoursesLessonsID = 0;

    private void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _ServicesCoursesID) || !int.TryParse(Query[1], out _ServicesCoursesLessonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindGrdElectronicRegistry()
    {
        DataTable DtElectronicRegistry = DALC.GetDataTableBySqlCommand("GetElectronicRegistry", new string[] { "ServicesCoursesID" }, new object[] { _ServicesCoursesID }, CommandType.StoredProcedure);

        if (DtElectronicRegistry == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (DtElectronicRegistry.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Məlumat tapılmadı!", "/tools/electronicregistry");
            return;
        }


        int IsComplated = DALC.GetLessonsIsComplated(_ServicesCoursesLessonsID);
        if (IsComplated == -1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BtnComplate.Visible = IsComplated == 0;

        for (int i = 2; i < GrdElectronicRegistry.Columns.Count; i++)
        {
            GrdElectronicRegistry.Columns[i].Visible = false;
        }

        for (int i = 2; i < DtElectronicRegistry.Columns.Count; i++)
        {
            GrdElectronicRegistry.Columns[i].HeaderText = DtElectronicRegistry.Columns[i].ColumnName;
            GrdElectronicRegistry.Columns[i].Visible = true;
        }

        GrdElectronicRegistry.DataSource = DtElectronicRegistry;
        GrdElectronicRegistry.DataBind();


        for (int i = 2; i < DtElectronicRegistry.Columns.Count; i++)
        {
            for (int j = 0; j < DtElectronicRegistry.Rows.Count; j++)
            {
                if (IsComplated == 1 || (i != 2))
                {
                    ((CheckBox)GrdElectronicRegistry.Rows[j].Cells[i].Controls[1]).Enabled = false;
                    ((TextBox)GrdElectronicRegistry.Rows[j].Cells[i].Controls[3]).Enabled = false;
                }

                ((CheckBox)GrdElectronicRegistry.Rows[j].Cells[i].Controls[1]).Checked = (DtElectronicRegistry._Rows(GrdElectronicRegistry.Columns[i].HeaderText, j).Split('-')[0] == "10");
                ((TextBox)GrdElectronicRegistry.Rows[j].Cells[i].Controls[3]).Text = DtElectronicRegistry._Rows(GrdElectronicRegistry.Columns[i].HeaderText, j).Split('-')[1];
            }
        }
    }

    private void BindGrdLessonsHistory()
    {
        GrdLessonsHistory.DataSource = DALC.GetServicesCoursesLessons(_ServicesCoursesID);
        GrdLessonsHistory.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Elektron_jurnal))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Elektron jurnal";
            BindGrdElectronicRegistry();
            BindGrdLessonsHistory();
        }

    }

    protected void BtnComplate_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("IsComplated", true);
        Dictionary.Add("Update_Dt", DateTime.Now);
        Dictionary.Add("WhereID", _ServicesCoursesLessonsID);

        int Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesLessons, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, "/tools/electronicregistry");

    }

    protected void CheckIsUse_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Chk = sender as CheckBox;
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("ServicesCoursesLessonsUseHistoryTypesID", Chk.Checked ? (int)Tools.ServicesCoursesLessonsUseHistoryTypes.İştirak_edib : (int)Tools.ServicesCoursesLessonsUseHistoryTypes.İştirak_etməyib);
        Dictionary.Add("WhereServicesCoursesLessonsID", _ServicesCoursesLessonsID);
        Dictionary.Add("WhereServicesCoursesPersonsID", Chk.Attributes["data-personsid"]);

        int Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesLessonsUseHistory, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    protected void TxtScore_TextChanged(object sender, EventArgs e)
    {
        TextBox Txt = sender as TextBox;
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Score", Txt.Text);
        Dictionary.Add("WhereServicesCoursesLessonsID", _ServicesCoursesLessonsID);
        Dictionary.Add("WhereServicesCoursesPersonsID", Txt.Attributes["data-personsid"]);

        int Check = DALC.UpdateDatabase(Tools.Table.ServicesCoursesLessonsUseHistory, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }
}