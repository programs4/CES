<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ElectronicRegistry_Courses_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
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
                                    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">

                                        <asp:View ID="View1" runat="server">
                                            Mərkəz:<br />
                                            <asp:DropDownList ID="DListOrganizations" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DListOrganizations_SelectedIndexChanged" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                            <br />
                                            <br />

                                            Xidmət:<br />
                                            <asp:DropDownList ID="DListServices" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                            <br />
                                            <br />

                                            Kursun növü:<br />
                                            <asp:DropDownList ID="DListServicesCoursesTypes" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
                                            <br />
                                            <br />

                                            <asp:Panel ID="PnlTeacherUsers" Visible="false" runat="server">
                                                Müəllim:<br />
                                                <asp:DropDownList ID="DListTeacherUsers" CssClass="form-control" DataTextField="FullName" DataValueField="ID" runat="server"></asp:DropDownList>
                                                <br />
                                                <br />
                                            </asp:Panel>
                                            Kursun adı:<br />
                                            <asp:TextBox ID="TxtCourseName" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                            <br />
                                            <br />

                                            <div class="row">
                                                <div class="col-md-6">
                                                    Ümumi məşğələ sayı:<br />
                                                    <asp:TextBox ID="TxtLessonCount" TextMode="Number" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-md-6">
                                                    Ümumi məşğələ saatı:<br />
                                                    <asp:TextBox ID="TxtLessonsHours" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    Həftənin günləri:<br />
                                                    <asp:ListBox ID="DListLessonsWeekDay" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataTextField="Name" DataValueField="ID"></asp:ListBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-md-3">
                                                    Başlama vaxtı:<br />
                                                    <asp:TextBox ID="TxtLessonsStartTime" CssClass="form-control form_time" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-md-3">
                                                    Bitmə vaxtı:<br />
                                                    <asp:TextBox ID="TxtLessonsEndTime" CssClass="form-control form_time" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    Başlama tarixi:<br />
                                                    <asp:TextBox ID="TxtStartDate" CssClass="form-control form_datetime" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-md-6">
                                                    Bitmə tarixi:<br />
                                                    <asp:TextBox ID="TxtEndDate" CssClass="form-control form_datetime" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>

                                            Qeyd:<br />
                                            <asp:TextBox ID="TxtDescription" CssClass="form-control" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                                            <br />
                                            <br />

                                            Status:<br />
                                            <asp:DropDownList ID="DListStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Aktiv"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Deaktiv"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <div class="modal-footer">
                                                <asp:Button ID="BtnAddCourse" CssClass="btn btn-default" Text="Əlavə et" OnClientClick="this.style.display='none';document.getElementById('addloading').style.display=''" OnClick="BtnAddCourse_Click" runat="server" />
                                                <img id="addloading" src="/images/loading.gif" style="display: none" />
                                            </div>
                                        </asp:View>

                                        <asp:View ID="View2" runat="server">
                                            <asp:Panel ID="Panel1" maxheight="500px" runat="server">
                                                <asp:GridView ID="GrdServicesCoursesPlans" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID,ServicesPlansID,ServicesID,ServicesPlansTypesID" OnRowDataBound="GrdServicesCoursesPlans_RowDataBound" OnRowCommand="GrdServicesCoursesPlans_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S/s">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="Name" HeaderText="Mövzu">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Növü">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DListServicesPlansTypes" CssClass="form-control" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Saat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtLessonsHours" CssClass="form-control" Text='<%#Eval("Hours")%>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Say">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtLessonsCount" TextMode="Number" Text='<%#Eval("Count")%>' CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkSave" CommandArgument='<%# Container.DataItemIndex %>' CommandName='<%#string.IsNullOrEmpty(Eval("IsDeleted")._ToString())?"add":"editing"%>' runat="server">
                                                                    <img width="30" src="/images/<%#Eval("IsDeleted").ToString().ToLower()=="false"?"success":"nosuccess"%>.png" title="Əlavə et" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkDelete" Visible='<%#Eval("IsDeleted")._ToString().ToLower()=="false"%>' CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandName="deleting" runat="server">
                                                                 <img  src="/images/delete.png" title="Sil" />
                                                                </asp:LinkButton>
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
                                            </asp:Panel>
                                            <div class="modal-footer">
                                            </div>
                                        </asp:View>

                                        <asp:View ID="View3" runat="server">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    Kursa qatılmaq istəyənlər:<br />
                                                    <asp:ListBox ID="DListPersons" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataTextField="FullName" DataValueField="ApplicationsPersonsID"></asp:ListBox>
                                                    <br />
                                                    <br />
                                                    <%--<asp:DropDownList ID="DListPersons" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ApplicationsPersonsID"></asp:DropDownList>--%>
                                                </div>
                                                <div class="col-md-6">
                                                    Qeyd:<br />
                                                    <asp:TextBox ID="TxtDescriptionForPersons" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <br />
                                                    <asp:Button ID="BtnAddPersons" CssClass="btn btn-primary" Text="Əlavə et" OnClick="BtnAddPersons_Click" runat="server" />
                                                </div>
                                            </div>
                                            <br />
                                            <asp:GridView ID="GrdServicesCoursesPersons" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID" OnRowCommand="GrdServicesCoursesPersons_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S/s">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="FullName" HeaderText="Adı">
                                                        <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Description" HeaderText="Qeyd">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LnkDelete" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandName="deleting" runat="server">
                                                                 <img  src="/images/delete.png" title="Sil" />
                                                            </asp:LinkButton>
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
                                            <div class="modal-footer">
                                            </div>
                                        </asp:View>

                                    </asp:MultiView>


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <asp:Panel ID="PnlSearch" CssClass="Filter" runat="server">

                <div class="row">

                    <div class="col-md-2">
                        Mərkəz:<br />
                        <asp:DropDownList ID="DListFilterOrganizations" AutoPostBack="true" runat="server" CssClass="form-control" Width="250px" OnSelectedIndexChanged="DListFilterOrganizations_SelectedIndexChanged" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Xidmət:<br />
                        <asp:DropDownList ID="DListFilterServices" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                    </div>

                    <asp:Panel ID="PnlFilterTeacherUsers" Visible="false" runat="server" CssClass="col-md-2">
                        Müəllim:<br />
                        <asp:DropDownList ID="DListFilterTeacherUsers" CssClass="form-control" DataTextField="FullName" DataValueField="ID" runat="server"></asp:DropDownList>
                    </asp:Panel>

                    <div class="col-md-2">
                        Adı:<br />
                        <asp:TextBox ID="TxtFilterCourseName" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Tarix (başlama):<br />
                        <asp:TextBox ID="TxtFilterStart_Dt" CssClass="form-control form_datetime" runat="server" Width="250px" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Tarix (bitmə):<br />
                        <asp:TextBox ID="TxtFilterEnd_Dt" CssClass="form-control form_datetime" runat="server" Width="250px" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Status:<br />
                        <asp:DropDownList ID="DListFilterStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Aktiv"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Deaktiv"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <br />
                        <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" />
                        <asp:Button ID="BtnClear" CssClass="btn btn-default" Width="50px" Height="40px" runat="server" Text="X" Font-Bold="False" OnClick="BtnClear_Click" />
                    </div>

                </div>
            </asp:Panel>

            <br />

            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="LnkAddCourse" OnClick="LnkAddCourse_Click" runat="server">
                        <img class="alignMiddle" src="/images/add.png" /> YENİ KURS
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: 123"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="GrdList">
                        <asp:GridView ID="GrdServicesCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,OrganizationsID,ServicesID,ServicesCoursesTypesID" OnRowCommand="GrdServicesCourses_RowCommand" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                            <Columns>
                                <asp:TemplateField HeaderText="S/s">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="OrganizationsName" HeaderText="Qurumun adı">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="350px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ServicesName" HeaderText="Xidmətin adı">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Name" HeaderText="Kurs">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ServicesCoursesTypesName" HeaderText="Növü">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TeacherUsersName" HeaderText="Müəllim">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>



                                <asp:BoundField DataField="LessonsCount" HeaderText="Say">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="LessonsHours" HeaderText="Saat">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Başlama/Bitmə">
                                    <ItemTemplate>
                                        <%# ((DateTime)Eval("Start_Dt")).ToString("dd.MM.yyyy") %>
                                        <br />
                                        <%# ((DateTime)Eval("End_Dt")).ToString("dd.MM.yyyy") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEditCourses"  CommandName="editcourses" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                                             <img src="/images/edit.png" title="Düzəliş et" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkAddPlans"  CommandName="addplans" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                                             <img src="/images/details.png" title="Təqvim planı" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkAddPersons" CommandName="addpersons" data-organizationsid='<%#Eval("OrganizationsID")%>' CommandArgument='<%# Container.DataItemIndex %>' data-servicescoursestypesid='<%#Eval("ServicesCoursesTypesID") %>' runat="server">
                                             <img src="/images/addpersons.png" title="İştirakçı əlavə et" />
                                        </asp:LinkButton>
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
                        window.location.href = '/tools/electronicregistry/courses/?p=' + num;
                    }).find('.pagination');
                }
                $(document).ready(function () {
                    GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
                });
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

