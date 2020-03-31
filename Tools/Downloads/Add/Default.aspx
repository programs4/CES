<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Downloads_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">
                    Növü:
                    <br />
                    <asp:DropDownList ID="DListDownloadsTypes" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Adı:
                    <br />
                    <asp:TextBox ID="TxtName" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Fayl:
                    <br />
                    <asp:FileUpload ID="FileUp" CssClass="form-control" runat="server" />
                    <asp:Literal ID="ltrFileDownload" runat="server"></asp:Literal>
                    <br />
                </div>

                <div class="col-md-12">
                    Təsviri:
                    <br />
                    <asp:TextBox ID="TxtDescription" CssClass="form-control" Height="80px" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Statusu:
                    <br />
                    <asp:DropDownList ID="DListStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Deaktiv" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    <br />
                    <asp:Button ID="BtnAdd" runat="server" Text="Əlavə et" CssClass="btn btn-default" Width="111px" OnClick="BtnAdd_Click" OnClientClick="this.style.display='none';document.getElementById('loading').style.display=''" />
                    <asp:Button ID="BtnCancel" runat="server" Text="İmtina" CssClass="btn btn-danger" Width="111px" OnClick="BtnCancel_Click" OnClientClick="return confirm('İmtina etmək istədiyinizə əminsinizmi?')" />
                    <img id="loading" src="/images/loading.gif" style="display: none" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

