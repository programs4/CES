using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Evaluations_Add_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;
    int _EvaluationsID = 0;
    int _Result = 0;

    string Point = "";
    string PersonsPoint = "";
    string Description = "";
    LinkButton Lnk;
    TextBox Txt;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) ||
            Query[2] != DALC._GetUsersLogin.Key || !int.TryParse(Server.UrlDecode(Config._GetQueryString("id")).Replace(' ', '+').Decrypt(), out _EvaluationsID))
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }

        //Burda sessiaya ona gore atiriqki eger yeni elave de sehife ni yenilese query-de id =0 olduguna gore 
        //Evaluations table-na yeniden insertin qarshisini alaceyik
        if (Session["_EvaluationsID"] == null)
        {
            Session["_EvaluationsID"] = _EvaluationsID;
        }
        else
        {
            _EvaluationsID = Session["_EvaluationsID"]._ToInt32();
        }
    }

    private void BindEvaluationsQuestions()
    {
        DataTable Dt = DALC.GetEvaluationsQuestions(0, _EvaluationsID);

        RptQuestions.DataSource = Dt;
        RptQuestions.DataBind();

        int ParentID;
        foreach (RepeaterItem item in RptQuestions.Items)
        {
            ParentID = ((HiddenField)item.FindControl("HdnParentID")).Value._ToInt32();

            ((Repeater)item.FindControl("RptAnswer")).DataSource = DALC.GetEvaluationsQuestions(ParentID, _EvaluationsID);
            ((Repeater)item.FindControl("RptAnswer")).DataBind();
        }

        if (_EvaluationsID != 0)
        {
            if (DALC.CheckEvaluationsIsCompleted(_EvaluationsID))
            {
                PnlQuestions.Enabled = false;
                PnlCompleted.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
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
            ((Literal)Master.FindControl("LtrTitle")).Text = "Yeni qiymətləndirmə";
            ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);
            
            BindEvaluationsQuestions();
        }
    }

    protected void LnkAnswer_Click(object sender, EventArgs e)
    {
        //Icindekileri deaktiv edek.
        LinkButton Lnk = sender as LinkButton;

        Repeater Rpt = (Repeater)Lnk.Parent.Parent;
        for (int i = 0; i < Rpt.Items.Count; i++)
        {
            ((LinkButton)Rpt.Items[i].Controls[1]).CssClass = "list-group-item answerEvo";
        }

        int Point = int.Parse(Lnk.CommandArgument);
        Lnk.CssClass = "list-group-item answerActive";
        int EvaluationsQuestionsID = int.Parse(Lnk.CommandName);

        var Dictionary = new Dictionary<string, object>();

        //Eger yeni gelibse ve Evaluations table-inda yoxdursa insert edek
        if (_EvaluationsID == 0)
        {
            Dictionary = new Dictionary<string, object>()
            {
                {"ApplicationsPersonsID", _ApplicationsPersonsID},
                {"PointSum",0 },
                {"IsCompleted",false },
                {"IsActive",true },
                {"Add_Dt",DateTime.Now},
                {"Add_Ip",Request.UserHostAddress.IPToInteger()}
            };

            _Result = DALC.InsertDatabase(Tools.Table.Evaluations, Dictionary);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            _EvaluationsID = _Result;
            Session["_EvaluationsID"] = _EvaluationsID;
        }

        Dictionary = new Dictionary<string, object>()
        {
            {"EvaluationsID", _EvaluationsID},
            {"EvaluationsQuestionsID",EvaluationsQuestionsID},
            {"Point",Point},
            {"Description",DBNull.Value}
        };

        string EvaluationsPointsID = DALC.CheckEvaluationsPoints(_EvaluationsID, EvaluationsQuestionsID);
        if (!string.IsNullOrEmpty(EvaluationsPointsID))
        {
            //Her defe bura update gedende Trg_EvaluationsPoints le Evaluations table-da PointSum update olur
            Dictionary.Add("WhereID", int.Parse(EvaluationsPointsID));
            _Result = DALC.UpdateDatabase(Tools.Table.EvaluationsPoints, Dictionary);
        }
        else
        {
            //Her defe bura insert gedende Trg_EvaluationsPoints le Evaluations table-da PointSum update-le artir
            _Result = DALC.InsertDatabase(Tools.Table.EvaluationsPoints, Dictionary);
            EvaluationsPointsID = _Result._ToString();
        }


        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    protected void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        TextBox TxtDescription = sender as TextBox;


        for (int i = 0; i < RptQuestions.Items.Count; i++)
        {
            ((TextBox)RptQuestions.Items[i].FindControl("TxtDescription")).CssClass = "form-control";
            ((TextBox)RptQuestions.Items[i].FindControl("TxtDescription")).BorderWidth = 1;
        }

        var Dictionary = new Dictionary<string, object>()
        {
            {"Description",TxtDescription.Text},
            {"WhereID",TxtDescription.Attributes["data-evaluationspointsid"]}
        };

        _Result = DALC.UpdateDatabase(Tools.Table.EvaluationsPoints, Dictionary);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        TxtDescription.BorderColor = System.Drawing.Color.FromName("#00ab1d");
        TxtDescription.BorderWidth = 2;
    }

    protected void RptAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Lnk = (LinkButton)e.Item.FindControl("LnkAnswer");
        Txt = ((TextBox)Lnk.Parent.Parent.Parent.Controls[9]);

        //Bu bize her balin verilme sebebini update etdikde lazim olacaq

        Txt.Attributes.Add("data-evaluationspointsid", (DataBinder.Eval(e.Item.DataItem, "EvaluationsPointsID") ?? 0)._ToString());
        Description = DataBinder.Eval(e.Item.DataItem, "Description")._ToString().Trim();

        if (string.IsNullOrEmpty(Description))
        {
            Txt.Attributes.Add("placeholder", "Balın verilmə səbəbi");
        }
        else
        {
            Txt.Text = Description;
        }

        Point = (DataBinder.Eval(e.Item.DataItem, "Point") ?? 0)._ToString();
        PersonsPoint = (DataBinder.Eval(e.Item.DataItem, "PersonsPoint") ?? -1)._ToString();

        if (Point == PersonsPoint)
        {
            Lnk.CssClass = "list-group-item answerActive";
        }
        else
        {
            Lnk.CssClass = "list-group-item answerEvo";
        }

    }

    protected void BtnProcessEnds_Click(object sender, EventArgs e)
    {
        DALC.Transaction Transaction = new DALC.Transaction();
        var Dictionary = new Dictionary<string, object>()
        {
            {"IsCompleted",true},
            {"WhereID",_EvaluationsID}
        };

        _Result = DALC.UpdateDatabase(Tools.Table.Evaluations, Dictionary, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Dictionary.Clear();

        Dictionary.Add("ApplicationsPersonsID", _ApplicationsPersonsID);
        Dictionary.Add("ServicesID", (int)Tools.Services.Daxili_qiymətləndirmə);
        Dictionary.Add("ApplicationsPersonsServicesStatusID", (int)Tools.ApplicationsPersonsServicesStatus.Həll_olunub_təmin_edilib);
        Dictionary.Add("IsFirstApplication", false);
        Dictionary.Add("IsActive", true);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

        _Result = DALC.InsertDatabase(Tools.Table.ApplicationsPersonsServices, Dictionary, Transaction, true);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.RedirectURL(string.Format("/tools/evaluations/?i={0}", Config._GetQueryString("i")));
    }

}