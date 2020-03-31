using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Downloads_Add_Default : System.Web.UI.Page
{
    int _DownloadsID = 0;
    string _filePath = "/uploads/Downloads/{0}";
    private void BindDList()
    {
        DListDownloadsTypes.DataSource = DALC.GetList(Tools.Table.DownloadsTypes);
        DListDownloadsTypes.DataBind();
        if (DListDownloadsTypes.Items.Count > 1)
        {
            DListDownloadsTypes.Items.Insert(0, new ListItem("--", "-1"));
        }
    }

    private void BindInfo()
    {
        if (_DownloadsID != 0)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Düzəliş et";
            BtnAdd.Text = "Yadda saxla";

            DataTable Dt = new DataTable();
            Dt = DALC.GetDownloadsByID(_DownloadsID);

            if (Dt == null || Dt.Rows.Count < 1)
            {
                Response.Write("Məlumat tapılmadı!");
                Response.End();
            }

            DListDownloadsTypes.SelectedValue = Dt._Rows("DownloadsTypesID");
            TxtName.Text = Dt._Rows("DisplayName");
            ltrFileDownload.Text = string.Format("<a href=\"/uploads/downloads/{0}\" download><span class=\"glyphicon glyphicon-download-alt\"></span> faylı yüklə</a><br/>", Dt._Rows("FileName"));
            TxtDescription.Text = Dt._Rows("Description");
            DListStatus.SelectedValue = (bool)Dt._RowsObject("IsActive") ? "1" : "0";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectLogin();
            return;
        }

        //Yalniz huququ olan ve komitenen emekdashlari girish ede biler
        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Yükləmələr) && DALC._GetUsersLogin.OrganizationsParentID != 0)
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!int.TryParse(Config._GetQueryString("id"), out _DownloadsID))
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Yeni yükləmə";
            BindDList();
            BindInfo();
        }

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (DListDownloadsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Sənədin növünü seçin!");
            return;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            Config.MsgBoxAjax("Sənədin adını qeyd edin!");
            return;
        }

        string FileName = TxtName.Text.ClearTitle();
        if (DALC.CheckDownloadsByName(FileName, _DownloadsID))
        {
            Config.MsgBoxAjax("Bu sənəd adı, artıq istifadə edilib!");
            return;
        }

        if (_DownloadsID == 0)
        {
            if (!FileUp.HasFile)
            {
                Config.MsgBoxAjax("Fayl seçin!");
                return;
            }
        }

        if (DListStatus.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Sənədin statusunu qeyd edin!");
            return;
        }


        string FileType = "";
        decimal FileContentLength = 0;
        string AllowType = "-xls-xlsx-doc-docx-pdf-rar-zip-jpg-jpeg-png-txt-rtf-";

        if (FileUp.HasFile)
        {
            FileType = Path.GetExtension(FileUp.PostedFile.FileName);
            //FileName = string.Format("{0}{1}", FileName, FileType);
            FileContentLength = FileUp.PostedFile.ContentLength / 1024;

            if (AllowType.IndexOf(string.Format("-{0}-", FileType.Trim('.'))) < 0)
            {
                Config.MsgBoxAjax("Fayl formatı düzgün deyil!");
                return;
            }

            if (FileUp.PostedFile.CheckIfFileIsExecutable())
            {
                Config.MsgBoxAjax("Fayl formatı düzgün deyil!");
                return;
            }

            if (!FileUp.PostedFile.CheckFileContentLength(20))
            {
                Config.MsgBoxAjax("Faylın həcmi ən cox 20MB ola bilər!");
                return;
            }

            if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FileName) || FileContentLength == 0)
            {
                Config.MsgBoxAjax("Xətalı fayl!");
                return;
            }

            try
            {
                FileUp.SaveAs(Server.MapPath(string.Format(_filePath, FileName + FileType)));
            }
            catch (Exception er)
            {
                DALC.ErrorLogs(string.Format("Sənədlər bölməsində fayl yüklənərkən xəta: {0}", er.Message));
                Config.MsgBoxAjax("Fayl yüklənərkən xəta baş verdi!");
                return;
            }
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("DownloadsTypesID", int.Parse(DListDownloadsTypes.SelectedValue));
        Dictionary.Add("DataID", 0);
        Dictionary.Add("DisplayName", TxtName.Text);

        if (FileUp.HasFile)
        {
            Dictionary.Add("FileName", FileName);
            Dictionary.Add("FileType", FileType);
            Dictionary.Add("ContentLength", FileContentLength);
        }

        Dictionary.Add("Description", TxtDescription.Text);
        Dictionary.Add("IsActive", int.Parse(DListStatus.SelectedValue));


        int Result;
        if (_DownloadsID == 0)
        {
            Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
            Dictionary.Add("DownloadsQualityTypesID", (int)Tools.DownloadsQualityTypes.Qiymətləndirilməyib);
            Dictionary.Add("Data_Dt", DateTime.Now);
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            Result = DALC.InsertDatabase(Tools.Table.Downloads, Dictionary);
            if (Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }
        else
        {
            Dictionary.Add("WhereID", _DownloadsID);
            Result = DALC.UpdateDatabase(Tools.Table.Downloads, Dictionary);
            if (Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, "/tools/downloads");
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Config.RedirectURL("/tools/downloads");
    }

}