<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ApplicationsPersons_Default" %>

<%@ Register Src="~/Tools/UserControls/Operations.ascx" TagPrefix="uc1" TagName="Operations" %>

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
                window.location.href = '/tools/applicationspersons/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <!-- Modal -->
    <div class="modal fade" id="Modal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Literal ID="LtrTitle" runat="server"></asp:Literal>
                    </h4>
                </div>
                <div class="modal-body" style="min-height: 500px">
                    <uc1:Operations runat="server" ID="Operations" />
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">
        <div class="row">

            <asp:Panel ID="PnlFilterOrganizations" CssClass="col-md-2" Visible="false" runat="server">
                Qurum:<br />
                <asp:DropDownList ID="DListFltOrganizations" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                <br />
            </asp:Panel>

            <div class="col-md-2">
                Şəxsi kod:<br />
                <asp:TextBox ID="TxtFltPersonID" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Müraciətin nömrəsi:<br />
                <asp:TextBox ID="TxtFltApplicationsID" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Müraciət növü:<br />
                <asp:DropDownList ID="DListFltApplicationsTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </div>

            <%-- Paneli koddan ve ya burdan ne vaxt true etsek axtarish ishleyecek yalniz vetendashlar bolmesinde aktiv etmek lazimdir
                sol menuda bu bolmeler ayri ayri olduguna gore axtarishda heliki gizledirem isteseler acariq --%>
            <asp:Panel ID="PnlOperations" CssClass="col-md-2" runat="server" Visible="false">
                Əməliyyatlar:<br />
                <asp:ListBox ID="DListFltListOperationTypes" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataTextField="Name" DataValueField="ID"></asp:ListBox>
            </asp:Panel>

            <div class="col-md-2">
                Dərəcəsi:<br />
                <asp:DropDownList ID="DListFltApplicationsPersonsType" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                Sənədin növü:<br />
                <asp:DropDownList ID="DListFltDocumentTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                Sənəd nömrəsi:<br />
                <asp:TextBox ID="TxtFltDocumentNumber" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Soyadı:<br />
                <asp:TextBox ID="TxtFltSurname" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Adı:<br />
                <asp:TextBox ID="TxtFltName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Atasının adı:<br />
                <asp:TextBox ID="TxtFltPatronymic" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Sosial statusu:<br />
                <asp:DropDownList ID="DListFltSocialStatusID" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                Qeydiyyat ünvaı:<br />
                <asp:TextBox ID="TxtFilterRegisteredAddress" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Yaşadığı ünvan:<br />
                <asp:TextBox ID="TxtFilterCurrentAddress" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Tarix (başlanğıc):<br />
                <asp:TextBox ID="TxtFilterDate1" CssClass="form_datetime form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Tarix (son):<br />
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

    <div class="GrdList">
        <div class="row">
            <div class="col-md-12 text-right">
                <asp:Label ID="LblCount" runat="server"></asp:Label>
            </div>
        </div>
        <asp:GridView ID="GrdApplicationsPersons" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Şəxsi kod">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#(!string.IsNullOrEmpty(Eval("EvaluationsID")._ToString()) || !string.IsNullOrEmpty(Eval("SIBRID")._ToString()))?"/images/GridIcons/Eval_On.png":"/images/GridIcons/Eval_Off.png"%>' ToolTip="Qimətləndirilmə aparılıb" />
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsPersonsServicesID")._ToString())?"/images/GridIcons/Service_On.png":"/images/GridIcons/Service_Off.png"%>' ToolTip="Xidmətdən istifadə edir" />
                        <asp:Image ID="Image3" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsFamilyID")._ToString())?"/images/GridIcons/Family_On.png":"/images/GridIcons/Family_Off.png"%>' ToolTip="Ailə səfəri edilib" />
                        <asp:Image ID="Image4" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsCaseID")._ToString())?"/images/GridIcons/Case_On.png":"/images/GridIcons/Case_Off.png"%>' ToolTip="CASE açılıb" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:TemplateField>

                <asp:BoundField DataField="ApplicationsID" HeaderText="Müraciət №">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>

                <asp:BoundField DataField="DocumentTypes" HeaderText="Sənədinin növü">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                </asp:BoundField>

                <asp:BoundField DataField="DocumentNumber" HeaderText="Sənədinin nömrəsi">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>

                <asp:BoundField DataField="Fullname" HeaderText="Soyadı, adı və atasının adı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="BirthDate" HeaderText="Doğum tarixi" DataFormatString="{0:dd.MM.yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>

                <asp:BoundField DataField="Add_Dt" HeaderText="Əlavə olunma tarixi" DataFormatString="{0:dd.MM.yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkOperations" CommandArgument='<%#Eval("ID") %>' CommandName='<%#Eval("ApplicationsID") %>' OnClick="LnkOperations_Click" runat="server">
                            <img src="/images/operations.png" title="Əməliyyatlar" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                    Məlumat tapılmadı.
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

</asp:Content>

