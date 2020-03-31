<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ApplicationsFamily_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <%-- Səhifələmə üçün script --%>
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
                window.location.href = '/tools/applicationsfamily/?p=' + num;
            }).find('.pagination');
        }

        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="PnlSearch" runat="server" CssClass="Filter">
        <div class="row">
            <asp:Panel ID="PnlFltOrganizations" Visible="false" runat="server" CssClass="col-md-2">
                Qurum:<br />
                <asp:DropDownList ID="DListFltOrganizations" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </asp:Panel>

            <div class="col-md-2">
                Nömrəsi:<br />
                <asp:TextBox ID="TxtApplicationsFamilyID" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
            <div class="col-md-2">
                Məqsədi:<br />
                <asp:DropDownList ID="DListFltApplicationsFamilyTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                Statusu:<br />
                <asp:DropDownList ID="DListFltApplicationsFamilyStatus" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
            </div>
          
            <div class="col-md-2">
                Səfər tarixi (başlanğıc):<br />
                <asp:TextBox ID="TxtFilterTourDt1" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2">
                Səfər tarixi (son):<br />
                <asp:TextBox ID="TxtFilterTourDt2" CssClass="form-control form_datetime" runat="server"></asp:TextBox>

            </div>

            <div class="col-md-2">
                Əlavə olunma tarixi (başlanğıc):<br />
                <asp:TextBox ID="TxtFilterDate1" CssClass="form_datetime form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Əlavə olunma tarixi (son):<br />
                <asp:TextBox ID="TxtFilterDate2" CssClass="form_datetime form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                <br />
                <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" />
                <asp:Button ID="BtnClear" CssClass="btn btn-default" Width="50px" Height="40px" runat="server" Text="X" Font-Bold="False" OnClick="BtnClear_Click" />
            </div>
        </div>
    </asp:Panel>
    <br />

    <div class="row GrdList">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Label ID="LblCount" runat="server"></asp:Label>
                </div>
            </div>
            <asp:GridView ID="GrdApplicationsFamily" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="№">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ApplicationsID" HeaderText="Müraciət №">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="OrganizationsName" Visible="false" HeaderText="Qurumun adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ApplicationsFamilyTypes" HeaderText="Səfərin növü">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ApplicationsFamilyStatus" HeaderText="Səfərin statusu">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Description" HeaderText="Qeyd">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Tour_Dt" HeaderText="Səfər tarixi" DataFormatString="{0:dd.MM.yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Add_Dt" HeaderText="Əlavə olunma tarixi" DataFormatString="{0:dd.MM.yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="160px" />
                    </asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='<%#string.Format("/tools/applicationsfamily/add/?i={0}",Cryptography.Encrypt(Eval("ID")._ToString()+"-"+Eval("ApplicationsID")._ToString()+"-"+DALC._GetUsersLogin.Key)) %>'>
                                <img src="/images/edit.png" alt="Ailə səfəri" title="Düzəliş et" />
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
        </div>
        <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
            <ul class="pagination bootpag"></ul>
        </asp:Panel>

        <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />
    </div>
</asp:Content>

