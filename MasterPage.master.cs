using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    private void BindUsersPermissionsModules()
    {
        RptUsersPermissionsModules.DataSource = DALC.GetUsersPermissionsModules();
        RptUsersPermissionsModules.DataBind();
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "DateTime", "DateTime();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PnlTemporary.Style.Clear();

        LtrOrganizations.Text = DALC._GetUsersLogin.Organizations;
        LtrFullname.Text = DALC._GetUsersLogin.Fullname;
        string F = "", L = "";
        try
        {
            F = DALC._GetUsersLogin.Fullname.Trim().Substring(0, 1);
            L = DALC._GetUsersLogin.Fullname.Split(' ')[1].Substring(0, 1);
        }
        catch { }
        LtrSN.Text = string.Format("{0}{1}", F, L);

        LoadScriptManager();

        if (!IsPostBack)
        {
            BindUsersPermissionsModules();
        }

    }
}
