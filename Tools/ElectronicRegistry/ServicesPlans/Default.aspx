<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ElectronicRegistry_ServicesPlans_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">

        <div class="row">

            <div class="col-md-2">
                Mərkəz:<br />
                <asp:DropDownList ID="DListFilterOrganizations" runat="server" CssClass="form-control"  DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <br />
                <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" />
                <asp:Button ID="BtnClear" CssClass="btn btn-default" Width="50px" Height="40px" runat="server" Text="X" Font-Bold="False" OnClick="BtnClear_Click" />
            </div>
        </div>
    </asp:Panel>   
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-6">
                </div>
                <div class="col-md-6 text-right">
                    <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: 123"></asp:Label>
                </div>
            </div>
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdServices" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                    <Columns>
                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Name" HeaderText="Xidmət">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Hours" HeaderText="Məşğələ saatı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Count" HeaderText="Məşğələ sayı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="<%# string.Format("/tools/electronicregistry/servicesplans/add/?i={0}&type=add",(Eval("ID")+"-0-"+DALC._GetUsersLogin.Key).Encrypt()) %>">
                                    <img src="/images/edit.png" title="Düzəliş et" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                            Məlumat tapılmadı
                        </div>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="#e7e8ee" ForeColor="#333" Height="40px" />
                    <PagerSettings PageButtonCount="20" />
                    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                    <RowStyle CssClass="hoverLink" HorizontalAlign="Center" Font-Bold="False" Font-Size="11pt" />
                    <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <br />
                <br />
            </div>
            <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
                <ul class="pagination bootpag"></ul>
            </asp:Panel>
        </div>
    </div>

    <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />

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
                window.location.href = '/tools/electronicregistry/servicesplans/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>
</asp:Content>

