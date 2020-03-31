using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Events_Add_Default : System.Web.UI.Page
{
    int _EventsID = 0;
    int _Result = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _EventsID) || Query[1] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindDList()
    {
        DListEventsTypes.DataSource = DALC.GetList(Tools.Table.EventsTypes);
        DListEventsTypes.DataBind();
        DListEventsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListOrganizations.DataSource = DALC.GetOrganizations();
        DListOrganizations.DataBind();

        if (DListOrganizations.Items.Count > 1)
        {
            DListOrganizations.Items.Insert(0, new ListItem("--", "-1"));
        }

        DListEventsDirectionTypes.DataSource = DALC.GetList(Tools.Table.EventsDirectionTypes);
        DListEventsDirectionTypes.DataBind();
        DListEventsDirectionTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListEventsPolicyTypes.DataSource = DALC.GetList(Tools.Table.EventsPolicyTypes);
        DListEventsPolicyTypes.DataBind();
        DListEventsPolicyTypes.Items.Insert(0, new ListItem("--", "-1"));
    }

    private Tools.Result BindEvents()
    {
        DataTable Dt = DALC.GetEventsByID(_EventsID);

        if (Dt == null)
        {
            return Tools.Result.Dt_Null;
        }

        if (Dt.Rows.Count < 1)
        {
            return Tools.Result.Dt_Rows_Count_0;
        }

        DListEventsTypes.SelectedValue = Dt._Rows("EventsTypesID");
        DListOrganizations.SelectedValue = Dt._Rows("OrganizationsID");
        DListEventsDirectionTypes.SelectedValue = Dt._Rows("EventsDirectionTypesID");
        DListEventsPolicyTypes.SelectedValue = Dt._Rows("EventsPolicyTypesID");
        TxtName.Text = Dt._Rows("Name");
        TxtSubject.Text = Dt._Rows("Subject");
        TxtPlace.Text = Dt._Rows("Place");
        TxtOrganizer.Text = Dt._Rows("Organizer");
        TxtMemberCount.Text = Dt._Rows("MemberCount");
        TxtDescription.Text = Dt._Rows("Description");
        TxtEvents_StartDt.Text = ((DateTime)Dt._RowsObject("Events_StartDt")).ToString("dd.MM.yyyy");
        TxtEvents_EndDt.Text = ((DateTime)Dt._RowsObject("Events_EndDt")).ToString("dd.MM.yyyy");

        #region Photos
        //Other fotos

        DataTable DtImages = DALC.GetDownloadsByDataID((int)Tools.DownloadsTypes.Təlim_Tədbir, _EventsID);
        RptFotoGallery.DataSource = DtImages;
        RptFotoGallery.DataBind();

        #endregion

        return Tools.Result.Succes;
    }

    private bool Validations()
    {
        if (DListEventsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Təlim vəya Tədbir seçin.");
            return false;
        }

        if (DListOrganizations.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Təlim/Tədbirin keçirildiyi mərkəzi seçin.");
            return false;
        }

        if (DListEventsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("İştiralk növü seçin.");
            return false;
        }

        if (DListEventsTypes.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Tipini seçin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            Config.MsgBoxAjax("Adını qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtSubject.Text))
        {
            Config.MsgBoxAjax("Mövzunu qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtPlace.Text))
        {
            Config.MsgBoxAjax("Keçirildiyi yeri qeyd edin.");
            return false;
        }

        if (string.IsNullOrEmpty(TxtOrganizer.Text))
        {
            Config.MsgBoxAjax("Təşkilatçını qeyd edin.");
            return false;
        }

        if (!TxtMemberCount.Text.IsNumeric())
        {
            Config.MsgBoxAjax("İştirakçı sayını qeyd edin.");
            return false;
        }

        object Datet1 = Config.DateTimeFormat(TxtEvents_StartDt.Text);
        object Datet2 = Config.DateTimeFormat(TxtEvents_EndDt.Text);

        if (Datet1 == null || Datet2 == null)
        {
            Config.MsgBoxAjax("Keçirilmə tarixlərini düzgün qeyd edin.");
            return false;
        }

        TxtEvents_StartDt.Text = ((DateTime)Datet1).ToString("dd.MM.yyyy");
        TxtEvents_EndDt.Text = ((DateTime)Datet2).ToString("dd.MM.yyyy");


        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Təlim_və_tədbirlər))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Yeni Təlim/Tədbir";
            BindDList();

            _Result = (int)BindEvents();
            if (_Result == (int)Tools.Result.Dt_Null)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (!Validations())
        {
            return;
        }

        var Dictionary = new Dictionary<string, object>()
        {
            {"EventsTypesID",int.Parse(DListEventsTypes.SelectedValue)},
            {"OrganizationsID",int.Parse(DListOrganizations.SelectedValue)},
            {"EventsDirectionTypesID",int.Parse(DListEventsDirectionTypes.SelectedValue)},
            {"EventsPolicyTypesID",int.Parse(DListEventsPolicyTypes.SelectedValue)},
            {"Name",TxtName.Text},
            {"Subject",TxtSubject.Text},
            {"Place",TxtPlace.Text},
            {"Organizer",TxtOrganizer.Text},
            {"MemberCount",int.Parse(TxtMemberCount.Text)},
            {"Description",TxtDescription.Text},
            {"Events_StartDt",(DateTime)TxtEvents_StartDt.Text.DateTimeFormat()},
            {"Events_EndDt",(DateTime)TxtEvents_EndDt.Text.DateTimeFormat()},

        };

        DateTime Date = DateTime.Now;
        if (_EventsID == 0)
        {
            Dictionary.Add("Add_Dt", Date);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            _Result = DALC.InsertDatabase(Tools.Table.Events, Dictionary);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }
            _EventsID = _Result;
        }
        else
        {
            Dictionary.Add("WhereID", _EventsID);
            _Result = DALC.UpdateDatabase(Tools.Table.Events, Dictionary);
            if (_Result < 1)
            {
                Config.MsgBoxAjax(Config._DefaultErrorMessages);
                return;
            }

            Date = DALC.GetDataTable("Add_Dt", Tools.Table.Events, "ID", _EventsID)._RowsDatetime("Add_Dt");
        }

        string FileName = "";
        string FileType = "";

        string Path = "";
        string PathFormat = string.Format("/uploads/events/small/{0}/{1}/", Date.ToString("yyyy"), Date.ToString("MM"));
        string _AllowTypes = "-gif-jpg-jpeg-bmp-png-";

        DataTable DtImages = new DataTable();
        DtImages.Columns.Add("UsersID", typeof(int));
        DtImages.Columns.Add("DownloadsTypesID", typeof(byte));
        DtImages.Columns.Add("DataID", typeof(int));
        DtImages.Columns.Add("DisplayName", typeof(string));
        DtImages.Columns.Add("FileName", typeof(string));
        DtImages.Columns.Add("FileType", typeof(string));
        DtImages.Columns.Add("ContentLength", typeof(decimal));
        DtImages.Columns.Add("DownloadsQualityTypesID", typeof(int));
        DtImages.Columns.Add("Description", typeof(string));
        DtImages.Columns.Add("IsActive", typeof(bool));
        DtImages.Columns.Add("Data_Dt", typeof(DateTime));
        DtImages.Columns.Add("Add_Dt", typeof(DateTime));
        DtImages.Columns.Add("Add_Ip", typeof(int));


        if (!System.IO.Directory.Exists(Server.MapPath(PathFormat)))
            System.IO.Directory.CreateDirectory(Server.MapPath(PathFormat));

        if (!System.IO.Directory.Exists(Server.MapPath(PathFormat.Replace("small", "original"))))
            System.IO.Directory.CreateDirectory(Server.MapPath(PathFormat.Replace("small", "original")));

        HttpFileCollection Files = Request.Files;

        for (int i = 0; i < Files.Count; i++)
        {
            Path = PathFormat;
            if (Files[i].ContentLength > 0)
            {
                FileType = System.IO.Path.GetExtension(Files[i].FileName);
                if (_AllowTypes.IndexOf("-" + FileType.Trim('.').ToLower() + "-") > -1)
                {
                    if (!Files[i].CheckFileContentLength(10))
                        continue;

                    FileName = string.Format("{0}_{1}", _EventsID, DateTime.Now.ToString("ddMMyyyyHHmmssfff"));
                    Path = Path + FileName + FileType;


                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Files[i].InputStream);

                    ImageResize.ImgResize(Path.Replace("small", "original"), bmp.Width, bmp.Height, Files[i].InputStream, 95L);
                    ImageResize.ImgResize(Path, 200, -1, Files[i].InputStream, 95L);

                    DataRow Dr = DtImages.NewRow();
                    Dr["UsersID"] = DALC._GetUsersLogin.ID;
                    Dr["DownloadsTypesID"] = 2;
                    Dr["DataID"] = _EventsID;
                    Dr["DisplayName"] = FileName;
                    Dr["FileName"] = FileName;
                    Dr["FileType"] = FileType;
                    Dr["ContentLength"] = Files[i].ContentLength / 1024;
                    Dr["DownloadsQualityTypesID"] = (int)Tools.DownloadsQualityTypes.Qiymətləndirilməyib;
                    Dr["Description"] = DBNull.Value;
                    Dr["IsActive"] = true;
                    Dr["Data_Dt"] = Date;
                    Dr["Add_Dt"] = DateTime.Now;
                    Dr["Add_Ip"] = Request.UserHostAddress.IPToInteger();
                    DtImages.Rows.Add(Dr);
                }
            }
        }

        _Result = DALC.InsertBulk(Tools.Table.Downloads, DtImages);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax(Config._DefaultSuccessMessages, "/tools/events");
    }
}