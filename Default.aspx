<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CES | Centralized Evaluation System</title>
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/styles.css" />
</head>
<body>
    <form id="AspnetForm" runat="server" class="main-login-holder">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="main-login-holder">
            <div class="container full-height">
                <div class="row full-height">
                    <div class="col-md-4 col-sm-12 col-xs-12 full-height">
                        <div class="form-holder">
                            <div class="logo">
                                <img src="/images/logo-white.png" class="img-responsive" />
                            </div>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <label>İstifadəçi adınız:</label>
                                    <asp:TextBox ID="TxtLogin" runat="server" CssClass="form-control inputText"></asp:TextBox>
                                    <label>Şifrəniz:</label>
                                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control inputText"></asp:TextBox>
                                    <a href="#">Şifrəni unutmusunuz?</a>
                                    <br>
                                    <asp:Button ID="BtnLogin" runat="server" Text="Sistemə daxil olun" CssClass="login-btn btn btn-success" OnClick="BtnLogin_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
    <script type="text/javascript" src="/js/jquery.min.js"></script>
    <script type="text/javascript" src="/js/customscroll.min.js"></script>
</body>
</html>
