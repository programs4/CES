<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError().GetBaseException();
        DALC.ErrorLogs("CES - Global.asax sehv verdi: " + ex.Message + "  |  Source: " + ex.Source, true);

        //Master Page-də səhv çıxan da 
        if (Request.Url.ToString().ToLower().IndexOf("/error") > -1)
        {
            Response.Write("Error 404");
            Response.End();
        }

        Config.RedirectError();
        Response.End();
    }

    void Application_Start(object sender, EventArgs e)
    {
        System.Web.Routing.RouteCollection Collection = new System.Web.Routing.RouteCollection();

        //Tools page
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Users", "tools/users/{type}", "~/tools/users/default.aspx"));

    }
</script>
