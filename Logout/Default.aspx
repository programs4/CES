<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UsersLogin"] = null;
        Session.RemoveAll();
        Session.Clear();
        Config.RedirectLogin(false);
    }
</script>


