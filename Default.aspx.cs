using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();

        if (!IsPostBack)
            TxtLogin.Focus();
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        string Login = TxtLogin.Text;
        string Password = TxtPassword.Text;

        if (string.IsNullOrEmpty(Login))
        {
            Config.MsgBoxAjax("İstifadəçi adı daxil edin.");
            TxtLogin.Focus();
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            Config.MsgBoxAjax("Şifrə daxil edin.");
            TxtPassword.Focus();
            return;
        }

        int CheckResult = DALC.SetByUsersInfo(Login, Password.SHA1Special());

        if (CheckResult == -1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (CheckResult == 0)
        {
            Config.MsgBoxAjax("Giriş baş tutmadı! İstifadəçi adınızın və ya şifrənizin doğruluğuna əmin olun.");
            return;
        }

        string ReturnResult = Config._GetQueryString("return");

        if (ReturnResult.Length > 0)
        {
            Config.RedirectURL(ReturnResult);
            return;
        }
        else
        {
            Config.RedirectURL("/tools");
        }
    }
}