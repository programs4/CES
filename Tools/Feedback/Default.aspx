<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Feedback_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View runat="server">
            <asp:Panel ID="PnlFeedBack" runat="server" Width="600px">
                Qurum:<br />
                <asp:TextBox ID="TxtOrganizations" runat="server" ReadOnly="true" CssClass="form-control" Width="600px"></asp:TextBox><br />
                <br />
                Adı, soyadı və atasının adı:<br />
                <asp:TextBox ID="TxtFullName" runat="server" ReadOnly="true" CssClass="form-control" Width="600px"></asp:TextBox><br />
                <br />
                Mövzu:<br />
                <asp:TextBox ID="TxtSubject" runat="server" CssClass="form-control" Width="600px"></asp:TextBox><br />
                <br />
                Mətn:<br />
                <asp:TextBox ID="TxtText" runat="server" CssClass="form-control" Width="600px" Height="200px" TextMode="MultiLine"></asp:TextBox><br />
                <br />
                Əlaqə vasitəsi:<br />
                <asp:TextBox ID="TxtContact" runat="server" CssClass="form-control" Width="600px"></asp:TextBox><br />
                <br />
                <div class="floatRight">
                    &nbsp;<asp:Button ID="BtnInsert" runat="server" CssClass="btn btn-default" Text="GÖNDƏR" OnClick="BtnInsert_Click" OnClientClick="this.style.display='none';document.getElementById('loading').style.display=''" />
                    <img id="loading" src="/images/loading.gif" style="display: none" />
                </div>
            </asp:Panel>
        </asp:View>

        <asp:View runat="server">
            <div class="successMessage">
                <img src="/images/success.png" />
                <span class="mainText">Əməliyyat uğurla yerinə yetirildi!</span>
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>

