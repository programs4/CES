<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Applications_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            var can = document.getElementsByClassName("filler-canvas");
            for (i = 0; i < can.length; i++) {

                var context = can[i].getContext('2d');

                var percentage = can[i].getAttribute("data-percent") / 100;
                var degrees = percentage * 360.0;
                var radians = degrees * (Math.PI / 180);

                var x = 19;
                var y = 19;
                var r = 15;
                var s = 0;

                var start = 0;
                context.beginPath();
                context.lineWidth = 4;
                context.arc(x, y, r, s, radians, false);
                context.strokeStyle = '#38bb6d';
                context.stroke();
                context.closePath();
            }
        })


    <%-- Səhifələmə üçün script --%>
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
                window.location.href = '/tools/applications/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- Modal -->
            <div class="modal fade" id="Modal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">
                                        <asp:Literal ID="LtrTitle" runat="server"></asp:Literal>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    Növü:
                                    <br />
                                    <asp:DropDownList ID="DListApplicationsTypes" CssClass="form-control" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />
                                    Qeydiyyatda olduğu ünvan:
                                    <br />
                                    <asp:TextBox ID="TxtRegisteredAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                    <br />
                                    <br />

                                    Yaşadığı ünvan:
                                    <br />
                                    <asp:TextBox ID="TxtCurrentAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                    <br />
                                    <br />

                                    <div class="row">
                                        <div class="col-md-10">
                                            Sosial statusu:<br />
                                            <asp:ListBox ID="DListApplicationsSocialStatus" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataTextField="Name" DataValueField="ID"></asp:ListBox>
                                            <br />
                                            <br />
                                        </div>
                                        <div class="col-md-2">
                                            <br />
                                            <asp:Button ID="BtnAddSosialStatus" runat="server" CommandArgument="0" Text="Əlavə et" OnClick="BtnAddSosialStatus_Click" CssClass="btn btn-default" />
                                        </div>
                                    </div>

                                    <asp:Panel ID="PnlGrdList" runat="server" Style="margin-bottom: 20px" ScrollBars="Vertical" CssClass="GrdList">
                                        <asp:GridView ID="GrdApplicationsSocialStatus" runat="server" ShowHeader="false" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S/s">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Name" HeaderText="Adı">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkDelete" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?')" CommandArgument='<%#Eval("ID")%>' OnClick="LnkDelete_Click" runat="server">
                                                        <img src="/images/delete.png" title="Sil" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <EmptyDataTemplate>
                                                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                                                </div>
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle Font-Bold="True" Font-Size="10pt" BackColor="#e7e8ee" ForeColor="#333" Height="40px" />
                                            <PagerSettings PageButtonCount="20" />
                                            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                                            <RowStyle CssClass="hoverLink" HorizontalAlign="Center" Font-Bold="False" Font-Size="11pt" />
                                            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </asp:Panel>


                                    <asp:CheckBox ID="CheckIsRepeat" CssClass="Checkbx" Text="Təkrar müraciətdir" runat="server" />
                                    <br />
                                    <br />

                                    Qeyd:
                                    <br />
                                    <asp:TextBox ID="TxtDescription" CssClass="form-control" Height="115px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LblDescription" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 500 simvoldan ibarət qeyd oluna bilər"></asp:Label>
                                    <br />
                                    <br />

                                    Nəticəsi:
                                    <br />
                                    <asp:DropDownList ID="DListApplicationsStatus" CssClass="form-control" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />

                                    Müraciət tarixi:
                                    <br />
                                    <asp:TextBox ID="TxtCreateDt" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LblCreateDt" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-müraciət tarixi gün.ay.il olaraq daxil edilməlidir (nümunə: 25.09.2017)"></asp:Label>
                                    <br />
                                    <br />
                                    <div class="modal-footer">
                                        <asp:Button ID="BtnAddApp" CssClass="btn btn-default" Text="Əlavə et" OnClientClick="this.style.display='none';document.getElementById('addloading').style.display=''" OnClick="BtnAddApp_Click" runat="server" />
                                        <img id="addloading" src="/images/loading.gif" style="display: none" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


            <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">
                <div class="row">
                    <asp:Panel ID="PnlFilterOrganizations" CssClass="col-md-2" Visible="false" runat="server">
                        Mərkəz:<br />
                        <asp:DropDownList ID="DListFilterOrganizations" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                    </asp:Panel>

                    <div class="col-md-2">
                        Müraciətin nömrəsi:<br />
                        <asp:TextBox ID="TxtFilterAppID" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Müraciətin növü:<br />
                        <asp:DropDownList ID="DListFilterApplicationsTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Əməliyyatlar:<br />
                        <asp:ListBox ID="DListFilterListOperationTypes" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataTextField="Name" DataValueField="ID"></asp:ListBox>
                    </div>

                    <div class="col-md-2">
                        CASE Statusu:<br />
                        <asp:DropDownList ID="DListFilterApplicationsCaseStatus" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
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
                        Müraciətçinin sosial statusu:<br />
                        <asp:DropDownList ID="DListFilterSocialStatusID" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Dərəcəsi:<br />
                        <asp:DropDownList ID="DListFilterApplicationsPersonsTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Sənədin növü:<br />
                        <asp:DropDownList ID="DListFilterDocumentTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Sənədin nömrəsi:<br />
                        <asp:TextBox ID="TxtFilterDocumentNumber" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Soyadı:<br />
                        <asp:TextBox ID="TxtFilterSurname" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Adı:<br />
                        <asp:TextBox ID="TxtFilterName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Atasının adı:<br />
                        <asp:TextBox ID="TxtFilterPatronymic" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <div style="padding-top: 5px">
                            <asp:CheckBox ID="CheckFilterIsRepeat" CssClass="Checkbx" Text="Təkrar müraciət" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-2">
                        <br />
                        <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" OnClientClick="this.style.display='none';document.getElementById('loading').style.display=''" />
                        <asp:Button ID="BtnClear" CssClass="btn btn-default" Width="50px" Height="40px" runat="server" Text="X" Font-Bold="False" OnClick="BtnClear_Click" />
                        <img id="loading" src="/images/loading.gif" style="display: none" />
                    </div>
                </div>
            </asp:Panel>
            <br />

            <div class="row">
                <div class="col-md-6">
                    <asp:Panel ID="PnlAddAplicatons" runat="server">
                        <asp:LinkButton ID="LnkAddApp" OnClick="LnkAddApp_Click" runat="server">
                        <img class="alignMiddle" src="/images/add.png" /> YENİ MÜRACİƏT
                        </asp:LinkButton>
                    </asp:Panel>
                </div>
                <div class="col-md-6 text-right">
                    <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: 123"></asp:Label>
                </div>
            </div>

            <div class="GrdList">
                <asp:GridView ID="GrdApplications" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="№">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#(!string.IsNullOrEmpty(Eval("EvaluationsID")._ToString()) || !string.IsNullOrEmpty(Eval("SIBRID")._ToString()))?"/images/gridicons/eval_on.png":"/images/gridicons/eval_off.png"%>' ToolTip="Qimətləndirilmə aparılıb" />
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsPersonsServicesID")._ToString())?"/images/gridicons/service_on.png":"/images/gridicons/service_off.png"%>' ToolTip="Xidmətdən istifadə edir" />
                                <asp:Image ID="Image3" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsFamilyID")._ToString())?"/images/gridicons/Family_On.png":"/images/gridicons/family_off.png"%>' ToolTip="Ailə səfəri edilib" />
                                <asp:Image ID="Image4" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsCaseID")._ToString())?"/images/gridicons/case_on.png":"/images/gridicons/case_off.png"%>' ToolTip="CASE açılıb" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Organizations" HeaderText="Qurumun adı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ApplicationsTypes" HeaderText="Müraciət növü">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <%--<asp:BoundField DataField="Fullname" HeaderText="Soyadı, adı və atasının adı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="Soyadı, adı və atasının adı">
                            <ItemTemplate>
                                <%# Eval("ApplicantFullName") %>
                                <span class="adress">(Müraciətçi)</span>
                                <br />
                                <%# Eval("FullNamePrimaryPerson") %>
                                <span class="adress">(Əsas şəxs)</span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ünvan">
                            <ItemTemplate>
                                <%# Eval("RegisteredAddress") %>
                                <span class="adress">(Qeydiyyat ünvanı)</span>
                                <br />
                                <%# Eval("CurrentAddress") %>
                                <span class="adress">(Yaşadığı ünvan)</span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Create_Dt" HeaderText="Müraciət tarixi" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click" runat="server"><img src="/images/edit.png" title="Düzəliş et" /></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='<%#string.Format("/tools/applications/details/?i={0}",Cryptography.Encrypt(Eval("ID")._ToString()+"-"+DALC._GetUsersLogin.Key)) %>'>
                                    <img src="/images/details.png" title="Ətraflı məlumat" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='<%#string.Format("/tools/applicationsfamily/add/?i={0}",Cryptography.Encrypt("0-"+Eval("ID")._ToString()+"-"+DALC._GetUsersLogin.Key)) %>'>
                                    <img src="/images/ailesefer.png" alt="Ailə səfəri" title="Ailə səfəri" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <%--  <asp:TemplateField>
                            <ItemTemplate>
                                <div class="filler-ico-holder">
                                    <i class="fa fa-pencil" aria-hidden="true"></i>
                                    <canvas class="filler-canvas" width="37" height="37" data-percent='<%#(Eval("ID")._ToInt32()+10-1000000)*5 %>'></canvas>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>--%>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

