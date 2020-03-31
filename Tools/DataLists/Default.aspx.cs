using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_DataLists_Default : System.Web.UI.Page
{
    string _SelectedTableName = "";
    Tools.Table DataListType;

    private void BindDList()
    {
        DListDataListsType.DataSource = DALC.GetList(Tools.Table.DataLists);
        DListDataListsType.DataBind();
    }

    private void BindGridDataLists()
    {
        if (string.IsNullOrEmpty(_SelectedTableName))
        {
            GrdDataLists.DataSource = null;
            GrdDataLists.DataBind();
        }
        else
        {
            DataListType = (Tools.Table)Enum.Parse(typeof(Tools.Table), _SelectedTableName);
            GrdDataLists.DataSource = DALC.GetList(DataListType, "Order By Priority asc,IsActive desc");
            GrdDataLists.DataBind();
            Session["SelectedTable"] = _SelectedTableName;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Sorağçlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        _SelectedTableName = Session["SelectedTable"]._ToString();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Sorağçalar";
            BindDList();
            DListDataListsType_SelectedIndexChanged(null, null);
        }
    }

    protected void DListDataListsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        _SelectedTableName = DListDataListsType.SelectedValue;
        BindGridDataLists();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataListType = (Tools.Table)Enum.Parse(typeof(Tools.Table), _SelectedTableName);

        TxtDataListsName.BorderColor = Color.Empty;
        if (string.IsNullOrEmpty(TxtDataListsName.Text))
        {
            TxtDataListsName.BorderColor = Color.Red;
            return;
        }
        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"Name",TxtDataListsName.Text},
            {"Priority",0},
            {"IsActive",true}
        };

        int Check = 0;
        Check = DALC.InsertDatabase(DataListType, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
        TxtDataListsName.Text = "";
        BindGridDataLists();
    }

    protected void CheckIsActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Chk = sender as CheckBox;

        //Secilen setirin rengini deyishek
        GridViewRow row = (GridViewRow)Chk.Parent.Parent;
        row.BackColor = Color.FromName("#bbffc7");

        _SelectedTableName = Session["SelectedTable"]._ToString();
        DataListType = (Tools.Table)Enum.Parse(typeof(Tools.Table), _SelectedTableName);

        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"IsActive", !Chk.Checked},
            {"WhereID",int.Parse(Chk.Attributes["data-id"])}
        };

        int Check = DALC.UpdateDatabase(DataListType, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

}