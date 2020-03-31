using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_DemographicInformations_Add_Default : System.Web.UI.Page
{
    int _DemographicInformationsID = 0;
    private void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _DemographicInformationsID) || Query[1] != DALC._GetUsersLogin.Key)
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private void BindRptDemographicInformations()
    {
        if (_DemographicInformationsID == 0)
        {
            RptDemographicInformationsTypes.DataSource = DALC.GetDemographicInformationsDetailsTypes();
            RptDemographicInformationsTypes.DataBind();
        }
        else
        {
            DataTable DtDemographicInformationsDetails= DALC.GetDemographicInformationsDetailsByID(_DemographicInformationsID);
            RptDemographicInformationsTypes.DataSource = DtDemographicInformationsDetails;
            RptDemographicInformationsTypes.DataBind();

            TxtCreate_Dt.Text = DtDemographicInformationsDetails._RowsDatetime("Create_Dt").ToString("dd.MM.yyyy");
            TxtDescription.Text = DtDemographicInformationsDetails._Rows("Description");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.Demoqrafik_məlumatlar))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Demoqrafik məlumatlar";

            BindRptDemographicInformations();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        object Date = Config.DateTimeFormat(TxtCreate_Dt.Text.Trim());
        if (Date == null)
        {
            Config.MsgBoxAjax("Tarixi düzgün formatda daxil edin!");
            return;
        }

        if (TxtDescription.Text.Length > 300)
        {
            Config.MsgBoxAjax("Qeyd 300 simvoldan böyük olmamalıdır!");
            return;
        }

        DataTable DtDemographicInformationsDetails = new DataTable();
        DtDemographicInformationsDetails.Columns.Add("DemographicInformationsTypesID", typeof(int));
        DtDemographicInformationsDetails.Columns.Add("Count", typeof(int));

        DataRow Dr;
        int Count;
        for (int i = 0; i < RptDemographicInformationsTypes.Items.Count; i++)
        {
            TextBox Txt = (TextBox)RptDemographicInformationsTypes.Items[i].FindControl("TxtName");

            int.TryParse(Txt.Text, out Count);

            Dr = DtDemographicInformationsDetails.NewRow();
            Dr["DemographicInformationsTypesID"] = Txt.Attributes["data-typeId"]._ToInt32();
            Dr["Count"] = Count;

            DtDemographicInformationsDetails.Rows.Add(Dr);
        }

        int result = DALC.ExecuteProcedure(
              "BulkInsertUpdateDemographicInformationsDetails",
              "DemographicInformationsID,OrganizationsID,Description,Create_Dt,Add_Dt,Add_Ip,ImportTable",
              new object[] 
              {
                  _DemographicInformationsID,
                  DALC._GetUsersLogin.OrganizationsID,
                  TxtDescription.Text,
                  Date,
                  DateTime.Now,
                  Request.UserHostAddress.IPToInteger(),
                  DtDemographicInformationsDetails
              });

        if (result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetrildi!", "/tools/demographicinformations");

    }
}