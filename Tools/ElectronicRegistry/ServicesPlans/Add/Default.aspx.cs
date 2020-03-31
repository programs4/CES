using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_ElectronicRegistry_ServicesPlans_Add_Default : System.Web.UI.Page
{
    int _ServicesID = 0;
    int _ServicesPlansID = 0;
    string _type;
    private void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        _type = Config._GetQueryString("type");
        if (!int.TryParse(Query[0], out _ServicesID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }

        if (_type == "edit" && !int.TryParse(Query[1], out _ServicesPlansID))
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }

    }
    private void BindDList()
    {
        DListServicesPlansTypes.DataSource = DALC.GetList(Tools.Table.ServicesPlansTypes);
        DListServicesPlansTypes.DataBind();
        DListServicesPlansTypes.Items.Insert(0, new ListItem("--", "-1"));

        string Priority = DALC.GetSercicesPlansMaxPriority(_ServicesID);
        if (Priority != "-1")
        {
            TxtPriority.Text = string.IsNullOrEmpty(Priority) ? "10" : (int.Parse(Priority) + 10).ToString();
        }
    }

    private void BindGrdServicesPlans()
    {
        GrdServicesPlans.DataSource = DALC.GetServicesPlans(_ServicesID);
        GrdServicesPlans.DataBind();
    }

    private void BindEdit()
    {
        if (_type == "edit")
        {
            DataTable Dt = DALC.GetServicesPlansByID(_ServicesPlansID);
            if (Dt == null || Dt.Rows.Count < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            DListServicesPlansTypes.SelectedValue = Dt._Rows("ServicesPlansTypesID");
            TxtName.Text = Dt._Rows("Name");
            TxtHours.Text = Dt._Rows("Hours");
            TxtCount.Text = Dt._Rows("Count");
            TxtPriority.Text = Dt._Rows("Priority");
            DListStatus.SelectedValue = Dt._RowsInt("IsActive").ToString();
            BtnAdd.Text = "Yenilə";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Təqvim_planı))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Ümumi təqvim planı";
            BindDList();
            BindGrdServicesPlans();
            BindEdit();
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (DListServicesPlansTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Xidmət növünü qeyd edin!");
            return;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            Config.MsgBoxAjax("Mövzu qeyd edin!");
            return;
        }

        decimal Hours;
        if (!decimal.TryParse(TxtHours.Text, out Hours))
        {
            Config.MsgBoxAjax("Saatı qeyd edin!");
            return;
        }

        int Count;
        if (!int.TryParse(TxtCount.Text, out Count))
        {
            Config.MsgBoxAjax("Sayı qeyd edin!");
            return;
        }

        if (string.IsNullOrEmpty(TxtPriority.Text) || int.Parse(TxtPriority.Text) < 1)
        {
            Config.MsgBoxAjax("Sıralamanı qeyd edin!");
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        Dictionary.Add("ServicesPlansTypesID", int.Parse(DListServicesPlansTypes.SelectedValue));
        Dictionary.Add("Name", TxtName.Text);
        Dictionary.Add("Hours", Hours);
        Dictionary.Add("Count", Count);
        Dictionary.Add("Priority", int.Parse(TxtPriority.Text));
        Dictionary.Add("IsActive", int.Parse(DListStatus.SelectedValue));
        Dictionary.Add("Update_Dt", DateTime.Now);

        int check;
        if (_type == "add")
        {
            Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
            Dictionary.Add("ServicesID", _ServicesID);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            Dictionary.Add("Add_Dt", DateTime.Now);

            check = DALC.InsertDatabase(Tools.Table.ServicesPlans, Dictionary);
        }
        else
        {
            Dictionary.Add("WhereID", _ServicesPlansID);
            check = DALC.UpdateDatabase(Tools.Table.ServicesPlans, Dictionary);
        }

        if (check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, string.Format("/tools/electronicregistry/servicesplans/add/?i={0}&type=add", (_ServicesID + "-0-" + DALC._GetUsersLogin.Key).Encrypt()));
    }
}