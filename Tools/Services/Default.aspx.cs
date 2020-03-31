using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_Services_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;

    void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');
        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) || Query[2] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindDList()
    {
        DListServices.DataSource = DALC.GetServices(_ApplicationsPersonsID);
        DListServices.DataBind();
        DListServices.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindGrdServices()
    {
        GrdServices.DataSource = DALC.GetServicesByPersonsID(_ApplicationsPersonsID);
        GrdServices.DataBind();
    }

    private void LoadScriptManager()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Multiselect", "$('.multiSelect').multiselect({buttonWidth: '100%',numberDisplayed: 5,});", true);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "DateTime", "dateTime();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/tools");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Xidmətdən_istifadələr))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Xidmətlər və yeni xidmət əlavə et";
            ((Literal)HeaderInfo.FindControl("ltrFullName")).Text = DALC.GetApplicationsPersonsFullName(_ApplicationsPersonsID);

            BindDList();
            BindGrdServices();
            TxtCreateDt.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        LoadScriptManager();
    }

    protected void DListServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList _DListServices = (sender as DropDownList);
        DataTable Dt = new DataTable();

        PnlSubServices2.Visible = PnlFirstApplication.Visible = PnlCreateDt.Visible = PnlBtnSave.Visible = false;

        DListSubServices2.DataSource = Dt;
        DListSubServices2.DataBind();
        DListFirstApplication.SelectedIndex = 0;

        if (_DListServices.ID == "DListServices")
        {
            PnlSubServices1.Visible = false;
            DListSubServices1.DataSource = Dt;
            DListSubServices1.DataBind();
        }

        Dt = DALC.GetServices(_ApplicationsPersonsID, int.Parse(_DListServices.SelectedValue));

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        //Secilen xidmete gore gelen alt qruplarin her hansi birinin alt qruplari varmi yoxmu ona baxilir...
        bool IsSubServices = false;
        foreach (DataRow Dr in Dt.Rows)
        {
            if (Dr["SubServicesCount"]._ToInt32() > 0)
            {
                IsSubServices = true;
                break;
            }
        }

        //Eger varsa multi secim etmeyek adi dropdownlist istifade edek
        bool IsUseFullService = false;
        if (IsSubServices)
        {
            DListSubServices1.DataSource = Dt;
            DListSubServices1.DataBind();
            PnlSubServices1.Visible = true;
            if (Dt.Rows.Count > 0)
            {
                DListSubServices1.Items.Insert(0, new ListItem("--", "-1"));
            }
            else
            {
                DListSubServices1.Items.Insert(0, new ListItem("Bütün növlər əlavə olunub", "-1"));
                IsUseFullService = true;
            }
        }
        else
        {
            //Burda yoxlayaq secilen xidmetin alt qurupu varmi yoxmu
            if (DALC.GetSubServicesCountByID(int.Parse(_DListServices.SelectedValue)) > 0)
            {
                DListSubServices2.DataSource = Dt;
                DListSubServices2.DataBind();
                PnlSubServices2.Visible = true;

                if (Dt.Rows.Count > 0)
                {
                    DListSubServices2.Attributes.Add("data-placeholder", "");
                }
                else
                {
                    DListSubServices2.Attributes.Add("data-placeholder", "Bütün növlər əlavə olunub");
                    IsUseFullService = true;
                }
            }
        }

        if (!IsUseFullService && DListServices.SelectedValue != "-1")
        {
            PnlFirstApplication.Visible = PnlBtnSave.Visible = PnlCreateDt.Visible = true;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (DListServices.SelectedValue == "-1")
        {
            Config.MsgBoxAjax("Əlavə etmək üçün xidmət seçin!");
            return;
        }

        object CreateDt = Config.DateTimeFormat(TxtCreateDt.Text);
        if (CreateDt == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün daxil edin.");
            return;
        }

        TxtCreateDt.Text = ((DateTime)CreateDt).ToString("dd.MM.yyyy");

        DataTable DtServices = new DataTable();
        DtServices.Columns.Add("ApplicationsPersonsID", typeof(int));
        DtServices.Columns.Add("ServicesID", typeof(int));
        //DtServices.Columns.Add("ServicesName", typeof(string));
        DtServices.Columns.Add("ApplicationsPersonsServicesStatusID", typeof(int));
        DtServices.Columns.Add("IsFirstApplication", typeof(int));
        DtServices.Columns.Add("IsActive", typeof(bool));
        DtServices.Columns.Add("Create_Dt", typeof(DateTime));
        DtServices.Columns.Add("Add_Dt", typeof(DateTime));
        DtServices.Columns.Add("Add_Ip", typeof(int));

        if (PnlSubServices2.Visible)
        {
            for (int i = 0; i < DListSubServices2.Items.Count; i++)
            {
                if (DListSubServices2.Items[i].Selected)
                {
                    if (DListSubServices2.Items[i].Value == Tools.Services.SIB_R_qısa_forma.ToString("d") ||
                        DListSubServices2.Items[i].Value == Tools.Services.SIB_R_erkən_inkişaf_fomrası.ToString("d") ||
                        DListSubServices2.Items[i].Value == Tools.Services.SIB_R_tam_miqyaslı_forma.ToString("d") ||
                        DListSubServices2.Items[i].Value == Tools.Services.Daxili_qiymətləndirmə.ToString("d"))
                    {
                        Config.MsgBoxAjax("\"SIB-R\" və \"Daxili qimətləndirmə\" uyğun bölmələrdə aparılmalıdır!");
                        return;
                    }

                    DtServices.Rows.Add(_ApplicationsPersonsID,
                                        int.Parse(DListSubServices2.Items[i].Value),
                                        //DListSubServices2.Items[i].Text,
                                        (int)Tools.ApplicationsPersonsServicesStatus.İcra_olunur,
                                        int.Parse(DListFirstApplication.SelectedValue),
                                        true,
                                        CreateDt,
                                        DateTime.Now,
                                        Request.UserHostAddress.IPToInteger());
                }
            }
        }
        else if (PnlSubServices1.Visible)
        {
            if (DListSubServices1.SelectedValue == "-1")
            {
                Config.MsgBoxAjax("Əlavə etmək üçün xidmət seçin!");
                return;
            }

            DtServices.Rows.Add(_ApplicationsPersonsID,
                                int.Parse(DListSubServices1.SelectedValue),
                                //DListSubServices1.SelectedItem.Text,
                                (int)Tools.ApplicationsPersonsServicesStatus.İcra_olunur,
                                int.Parse(DListFirstApplication.SelectedValue),
                                true,
                                CreateDt,
                                DateTime.Now,
                                Request.UserHostAddress.IPToInteger());
        }
        else
        {         

            DtServices.Rows.Add(_ApplicationsPersonsID,
                                int.Parse(DListServices.SelectedValue),
                                //DListServices.SelectedItem.Text,
                                (int)Tools.ApplicationsPersonsServicesStatus.İcra_olunur,
                                int.Parse(DListFirstApplication.SelectedValue),
                                true,
                                CreateDt,
                                DateTime.Now,
                                Request.UserHostAddress.IPToInteger());
        }

        if (DtServices.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Əlavə etmək üçün xidmət seçin!");
            return;
        }

        //Ehtiyac yoxdu o halda ehtiyyac olacaqki elave olunan xidmetler siyahidan chixarilmayacaq
        //foreach (DataRow Dr in DtServices.Rows)
        //{
        //    int CheckServicesCount = DALC.CheckServicesCount(_ApplicationsPersonsID, Dr["ServicesID"]._ToInt32());
        //    if (CheckServicesCount == -1)
        //    {
        //        Config.MsgBoxAjax(Config._DefaultErrorMessages);
        //        return;
        //    }
        //    else if (CheckServicesCount > 0)
        //    {
        //        Config.MsgBoxAjax(string.Format("{0} xidmət növü artıq əlavə olunub.", Dr["ServicesName"]._ToString()));
        //        return;
        //    }
        //}


        //Burda ServicesName-i remove edek chunki table-da ele bir column yoxdu. bunu yuxarida error-da istifade edirem
       // DtServices.Columns.Remove("ServicesName");

        int Check = DALC.InsertBulk(Tools.Table.ApplicationsPersonsServices, DtServices);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdServices();

        if (PnlSubServices1.Visible && PnlSubServices2.Visible)
        {
            DListServices_SelectedIndexChanged(DListSubServices1, EventArgs.Empty);
        }
        else if (PnlSubServices1.Visible || PnlSubServices2.Visible)
        {
            DListServices_SelectedIndexChanged(DListServices, EventArgs.Empty);
        }
        else
        {            
            BindDList();
            DListServices_SelectedIndexChanged(DListServices, EventArgs.Empty);
        }
           
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            {"IsActive", false},
            {"WhereID", int.Parse((sender as LinkButton).CommandArgument)},
        };

        int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersonsServices, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        BindGrdServices();
    }

    protected void DListServicesStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList DList = sender as DropDownList;
        GridViewRow row = (GridViewRow)DList.Parent.Parent;
        row.BackColor = System.Drawing.Color.FromName("#bbffc7");//#bbffc7

        int ApplicationsPersonsServicesID = GrdServices.DataKeys[row.RowIndex]["ID"]._ToInt32();

        var Dictionary = new Dictionary<string, object>()
        {
            {"ApplicationsPersonsServicesStatusID",DList.SelectedValue },
            {"WhereID",ApplicationsPersonsServicesID}
        };

        int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersonsServices, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }

    protected void GrdServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView RowView = (DataRowView)e.Row.DataItem;
            DropDownList DListServicesStatus = (e.Row.FindControl("DListServicesStatus") as DropDownList);
            TextBox TxtDescription = (e.Row.FindControl("TxtDescription") as TextBox);

            DListServicesStatus.DataSource = DALC.GetList(Tools.Table.ApplicationsPersonsServicesStatus);
            DListServicesStatus.DataBind();

            TxtDescription.Text = RowView["Description"]._ToString();
            DListServicesStatus.SelectedValue = RowView["ApplicationsPersonsServicesStatusID"]._ToString();
            DListServicesStatus.Enabled = (int.Parse(DListServicesStatus.SelectedValue) == (int)Tools.ApplicationsPersonsServicesStatus.İcra_olunur);
            TxtDescription.ReadOnly = (int.Parse(DListServicesStatus.SelectedValue) != (int)Tools.ApplicationsPersonsServicesStatus.İcra_olunur);
        }
    }

    protected void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        TextBox TxtDescription = sender as TextBox;
        GridViewRow row = (GridViewRow)TxtDescription.Parent.Parent;
        row.BackColor = System.Drawing.Color.FromName("#bbffc7");//#bbffc7

        int ApplicationsPersonsServicesID = GrdServices.DataKeys[row.RowIndex]["ID"]._ToInt32();

        var Dictionary = new Dictionary<string, object>()
        {
            {"Description",TxtDescription.Text},
            {"WhereID",ApplicationsPersonsServicesID}
        };

        int Check = DALC.UpdateDatabase(Tools.Table.ApplicationsPersonsServices, Dictionary);
        if (Check < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }
    }
}