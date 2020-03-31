<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ElectronicRegistry_Lessons_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <div class="report-table-tabs">
        <div class="tab-heads-holder">
            <div class="tab-heads">
                <div class="tab-head active" data-target="tab-1">Jurnal</div>
                <div class="tab-head" data-target="tab-2">Tarixcə</div>

            </div>
        </div>

        <div class="tab-content-reports active" id="tab-1">
            <h3 class="tab-name">Jurnal</h3>
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdElectronicRegistry" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                    <Columns>

                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="FullName" HeaderText="İştirakçı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Date1">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse1" AutoPostBack="true" data-personsid='<%#Eval("ServicesCoursesPersonsID")%>' CssClass="Checkbx" OnCheckedChanged="CheckIsUse_CheckedChanged" runat="server" />
                                <asp:TextBox ID="TxtScore1" AutoPostBack="true" OnTextChanged="TxtScore_TextChanged" data-personsid='<%#Eval("ServicesCoursesPersonsID")%>' CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date2">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse2" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore2" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date3">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse3" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore3" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Date4">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse4" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore4" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date5">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse5" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore5" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date6">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse6" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore6" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date7">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckIsUse7" CssClass="Checkbx" runat="server" />
                                <asp:TextBox ID="TxtScore7" CssClass="form-control" Height="30" Width="41px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
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
            </div>
            <br />
            <br />
            <div class="row">
                <div class="text-right col-md-12">
                    <asp:Button ID="BtnComplate" runat="server" CssClass="btn btn-default" OnClientClick="return confirm('Dərsi tamamlamaq istədiyinizə əminsinizmi?');" OnClick="BtnComplate_Click" Text="Dərsi tamamla" />
                </div>
            </div>
        </div>

        <div class="tab-content-reports" id="tab-2">
            <h3 class="tab-name">Tarixcə</h3>
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdLessonsHistory" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                    <Columns>

                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="ServicesCoursesName" HeaderText="Kursun adı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ServicesPlansName" HeaderText="Mövzu">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FullName" HeaderText="Müəllim">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>


                        <asp:BoundField DataField="Create_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

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
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            Report();
        });

        function Report() {
            $(".report-table .inner-table-holder th:first-child").on("click", function () {
                $(this).parent().parent().find("tr:not(.new-table)").toggle();
            });

            $(".report-table-tabs .tab-heads .tab-head").on("click", function () {
                $(".tab-head.active").removeClass("active");
                $(this).addClass("active");
                var target = $(this).attr("data-target");
                $(".report-table-tabs .tab-content-reports").removeClass("active");
                $("#" + target).addClass("active");
            });

            var isDropdownsOpen = false;
            $(".toggle-dropdowns").on("click", function () {
                if (isDropdownsOpen) {
                    $(".tab-content-reports tr:not(.new-table):not(.tr-month)").hide();
                    $(".toggle-dropdowns").attr("src", "/images/down.png");
                    isDropdownsOpen = false;
                } else {
                    $(".tab-content-reports tr:not(.new-table):not(.tr-month)").show();
                    $(".toggle-dropdowns").attr("src", "/images/top.png");
                    isDropdownsOpen = true;
                }
            });

            var isTabsOpen = false;
            $(".toggle-tabs").on("click", function () {
                if (isTabsOpen) {
                    $(".report-table-tabs").removeClass("show-all");
                    $(".toggle-tabs").attr("src", "/Images/open.png");
                    isTabsOpen = false;
                } else {
                    $(".report-table-tabs").addClass("show-all");
                    $(".toggle-tabs").attr("src", "/images/tabclose.png");
                    isTabsOpen = true;
                }

            })
        }
    </script>
</asp:Content>

