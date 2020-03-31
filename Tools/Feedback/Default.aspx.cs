using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Feedback_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }
       

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Əks Əlaqə";
            TxtOrganizations.Text = DALC._GetUsersLogin.Organizations;
            TxtFullName.Text = DALC._GetUsersLogin.Fullname;
        }
    }

    protected void BtnInsert_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtSubject.Text))
        {
            Config.MsgBoxAjax("Başlıq yazın.");
            return;
        }

        if (string.IsNullOrEmpty(TxtText.Text))
        {
            Config.MsgBoxAjax("Mətn qeyd edin.");
            return;
        }

        if (TxtText.Text.Length > 2000)
        {
            Config.MsgBoxAjax("Mətn 2000 simvoldan çox olmamalıdır.");
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Subject", TxtSubject.Text);
        Dictionary.Add("Text", TxtText.Text);
        Dictionary.Add("Contacts", TxtContact.Text);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress);

        int ResultID = DALC.InsertDatabase(Tools.Table.Feedback,Dictionary);

        if (ResultID < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DALC.SendMail(Config._GetAppSettings("FeedBackMailList"),
                      "CES Feedback", string.Format(@"Qurum: <b>{0}</b> <br/> 
                       Adı, soyadı və atasının adı: <b>{1}</b> <br/> 
                       Əlaqə: <b>{2}</b> <br/> 
                       Mövzu: <b>{3}</b> <br/> 
                       Mətin: <b>{4}</b> <br/>",
                       TxtOrganizations.Text,
                       TxtFullName.Text,
                       TxtContact.Text,
                       TxtSubject.Text,
                       TxtText.Text));

        MultiView1.ActiveViewIndex = 1;
    }
}