<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Gallery_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">
                <div class="row">

                    <asp:Panel ID="PnlFilterOrganizations" CssClass="col-md-2" Visible="false" runat="server">
                        Mərkəz:<br />
                        <asp:DropDownList ID="DListFilterOrganizations" AutoPostBack="true" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListFilterOrganizations_SelectedIndexChanged"></asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="PnlFilterDate" Visible="false" runat="server">
                        <div class="col-md-2">
                            Növü:<br />
                            <asp:DropDownList ID="DListFilterDownloadsTypes" AutoPostBack="true" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListFilterDownloadsTypes_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            İl:<br />
                            <asp:DropDownList ID="DListFilterYears" AutoPostBack="true" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListFilterYears_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Ay:<br />
                            <asp:DropDownList ID="DListFilterMonths" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                        </div>
                    </asp:Panel>
                    <div class="col-md-2">
                        Dəyər:<br />
                        <asp:DropDownList ID="DListFilterDownloadsQualityTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <section class="allgallery">
        <div class="allgallery-holder">
            <div class="row">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="RptFotoGallery" runat="server">
                                <ItemTemplate>
                                    <div class="gallery-item">
                                        <div class="img-Mainholder">
                                            <a data-fancybox="gallery-photo" href='<%#Config.UploadsImagePath("events/original", Eval("Data_Dt"), Eval("FileName"), Eval("FileType")) %>'>
                                                <div class="img-holder">
                                                    <img class="gallery-img" src='<%#Config.UploadsImagePath("events/small", Eval("Data_Dt"), Eval("FileName"), Eval("FileType")) %>' />
                                                </div>
                                            </a>
                                            <div class="rating d-flex justify-content-between">
                                                <asp:Button ID="btnQualityA" CssClass='<%#Eval("DownloadsQualityTypesID")._ToInt32()==10?"active10":""%>' CommandArgument="10" CommandName='<%# Eval("ID") %>' runat="server" Text="ƏLA" OnClick="BtnQuality_Click" />
                                                <asp:Button ID="btnQualityB" CssClass='<%#Eval("DownloadsQualityTypesID")._ToInt32()==20?"active20":""%>' CommandArgument="20" CommandName='<%# Eval("ID") %>' runat="server" Text="YAXŞI" OnClick="BtnQuality_Click" />
                                                <asp:Button ID="btnQualityC" CssClass='<%#Eval("DownloadsQualityTypesID")._ToInt32()==30?"active30":""%>' CommandArgument="30" CommandName='<%# Eval("ID") %>' runat="server" Text="KAFİ" OnClick="BtnQuality_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </section>

    <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
        <ul class="pagination bootpag"></ul>
    </asp:Panel>
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
                window.location.href = '/tools/gallery/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>
</asp:Content>

