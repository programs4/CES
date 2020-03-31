<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ElectronicRegistry_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="Modal" role="dialog">
                <div class="modal-dialog">
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
                                    Mövzu:<br />
                                    <asp:DropDownList ID="DListServicesCoursesPlans" DataValueField="ID" DataTextField="Name" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />

                                    <%-- Heleki istifade olunmur normal bir variatdir --%>
                                    <asp:GridView ID="GrdServicesCoursesPlans" runat="server" Visible="false" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
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

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" CssClass="Checkbx" runat="server" />
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


                                    Müəllim:<br />
                                    <asp:DropDownList ID="DListTeacherUsers" DataValueField="ID" DataTextField="Fullname" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />


                                    Dərs tarixi:<br />
                                    <asp:TextBox ID="TxtCreate_Dt" CssClass="form-control form_datetime" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
                                    <br />
                                    <br />


                                    Qeyd:<br />
                                    <asp:TextBox ID="TxtDescription" CssClass="form-control" TextMode="MultiLine" Height="110px" runat="server"></asp:TextBox>
                                    <br />
                                    <br />
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="BtnAddLessons" CssClass="btn btn-default" Text="Başla" OnClientClick="this.style.display='none';document.getElementById('addloading').style.display=''" OnClick="BtnAddLessons_Click" runat="server" />
                                    <img id="addloading" src="/images/loading.gif" style="display: none" />
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
                        <asp:DropDownList ID="DListFilterOrganizations" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        Tarix (başlanğıc):<br />
                        <asp:TextBox ID="TxtFilterStart_Dt" CssClass="form-control form_datetime" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        Tarix (son):<br />
                        <asp:TextBox ID="TxtFilterEnd_Dt" CssClass="form-control form_datetime" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
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
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: 123"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="GrdList">
                        <asp:GridView ID="GrdServicesCourses" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
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

                                <asp:BoundField DataField="ServicesName" HeaderText="Xidmət">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TeacherUsersName" HeaderText="Müəllim">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Name" HeaderText="Kurs">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                 <asp:BoundField DataField="LessonsWeekDays" HeaderText="Günlər">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Saat">
                                    <ItemTemplate>
                                        <%#((TimeSpan)Eval("LessonsStart_Tm")).ToString("hh\\:mm")%> - 
                                        <%#((TimeSpan)Eval("LessonsEnd_Tm")).ToString("hh\\:mm")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href='/tools/electronicregistry/lessons/?i=<%#(Eval("ID")._ToString()+"-"+Eval("ServicesCoursesLessonsID")._ToString()+"-"+DALC._GetUsersLogin.Key).Encrypt() %>'>
                                            <img src="/images/details.png" title="ətraflı" />
                                        </a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkBeginLesson" OnClick="LnkBeginLesson_Click" data-servicescourseslessonsid='<%#Eval("ServicesCoursesLessonsID")%>' CommandName='<%#Eval("OrganizationsID")%>' CommandArgument='<%#Eval("ID")%>' data-iscomplate='<%# Eval("LessonsIsComplated")._ToInt16()%>' data-teacherusersid='<%#Eval("TeacherUsersID")%>' runat="server">
                                            <img src="/images/edit.png" title="kursa başla" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
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
                window.location.href = '/tools/demographicinformations/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>

</asp:Content>

