<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Downloads_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">
        <div class="row">
            <div class="col-md-2">
                Növü:<br />
                <asp:DropDownList ID="DListFilterDownloadsTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                Adı:<br />
                <asp:TextBox ID="TxtFilterFileName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Panel ID="PnlStatus" runat="server" CssClass="col-md-2">
                Statusu:<br />
                <asp:DropDownList ID="DListFilterStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Deaktiv" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>

            <div class="col-md-2">
                <br />
                <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" />
                <asp:Button ID="BtnClear" CssClass="btn btn-default" Width="50px" Height="40px" runat="server" Text="X" Font-Bold="False" OnClick="BtnClear_Click" />
            </div>
        </div>
    </asp:Panel>
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:Panel ID="PnlAddFile" runat="server">
                <a href="/tools/downloads/add/?id=0">
                    <img class="alignMiddle" src="/images/add.png" />
                    YENİ SƏNƏD
                </a>
            </asp:Panel>
        </div>
        <div class="col-md-6 text-right">
            <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: {0}"></asp:Label>
        </div>
    </div>
    <br />
    <div class="GrdList">
        <asp:GridView ID="GrdDownloads" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
            <Columns>

                <asp:TemplateField HeaderText="S/s">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="DownloadsTypes" HeaderText="Növü">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>

                <asp:BoundField DataField="DisplayName" HeaderText="Adı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="350px" />
                </asp:BoundField>

                <asp:BoundField DataField="ContentLength" HeaderText="Həcmi" DataFormatString="{0} KB">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>

                <asp:BoundField DataField="Description" HeaderText="Təsviri">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%#(bool)Eval("IsActive")?"Aktiv":"Deaktiv" %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>

                <asp:BoundField DataField="Add_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/uploads/downloads/<%#Eval("FileName") %><%#Eval("FileType") %>" download>
                            <span class="glyphicon glyphicon-download-alt"></span> faylı yüklə
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="/tools/downloads/add/?id=<%# Eval("ID") %>">
                            <img src="/images/edit.png" title="Düzəliş et" alt="Düzəliş et" />
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                    Məlumat yoxdur
                </div>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="#e7e8ee" ForeColor="#333" Height="40px" />
            <PagerSettings PageButtonCount="20" />
            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
            <RowStyle CssClass="hoverLink" HorizontalAlign="Center" Font-Bold="False" Font-Size="11pt" />
            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
            <ul class="pagination bootpag"></ul>
        </asp:Panel>
        <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />
    </div>
    <script>
        function GetPagination(t, p) {
            $('.pager_top').bootpag({
                total: t,
                page: p,
                maxVisible: 15,
                leaps: true,
                firstLastUse: true,
                first: '<span aria-hidden="true">&larr;</span>',
                last: '<span aria-hidden="true">&rarr;</span>',
                wrapClass: 'pagination',
                activeClass: 'active',
                disabledClass: 'disabled',
                nextClass: 'next',
                prevClass: 'prev',
                lastClass: 'last',
                firstClass: 'first',

            }).on("page", function (event, num) {
                window.location.href = '/tools/downloads/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>
</asp:Content>

