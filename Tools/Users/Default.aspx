<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <!-- Modal -->
    <div class="modal fade" id="Modal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">İşçi məlumatları</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body row">
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#login-password" aria-controls="login-password" role="tab" data-toggle="tab">İstifadəçi adı və şifrə</a></li>
                                <li id="PermissionsTab" runat="server" role="presentation"><a href="#permission" aria-controls="permission" role="tab" data-toggle="tab">İcazələr</a></li>
                            </ul>

                            <!-- Tab panes -->

                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane active" id="login-password" style="margin-top: 20px;">
                                    <div class="col-lg-12 col-md-12">
                                        İstifadəçi adı:
                                        <br />
                                        <asp:TextBox ID="TxtLogin" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                    </div>
                                    <asp:Panel ID="PnlPassword" runat="server">
                                        <div class="col-lg-12 col-md-12">
                                            Şifrə:
                                            <br />
                                            <asp:TextBox ID="TxtPassword" CssClass="form-control" Width="100%" runat="server" TextMode="Password"></asp:TextBox>
                                            <br />
                                            <br />
                                        </div>
                                        <div class="col-lg-12 col-md-12">
                                            Təkrar şifrə:
                                            <br />
                                            <asp:TextBox ID="TxtPasswordRepeat" CssClass="form-control" Width="100%" runat="server" TextMode="Password"></asp:TextBox>
                                            <br />
                                            <br />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlResetUser" runat="server">
                                        <asp:Panel ID="PnlResetPassword" runat="server" CssClass="col-lg-12 col-md-12">
                                            <asp:Button ID="BtnReset" CssClass="btn btn-default" OnClick="BtnReset_Click" OnClientClick="return confirm('Şifrəni sıfırlamaq istədiyinizə əminsinizmi?');" Text="Şifrəni sıfırlamaq" runat="server" />
                                            <br />
                                            <asp:Label ID="LblResettingPassword" Style="position: relative; top: 5px" Visible="false" CssClass="" runat="server" Text=""></asp:Label>
                                            <br />
                                            <br />
                                        </asp:Panel>
                                        <div class="col-lg-12 col-md-12">
                                            <asp:CheckBox ID="CheckIsActive" OnCheckedChanged="CheckIsActive_CheckedChanged" CssClass="Checkbx" Text="Sistemə giriş hüququnu ləğv et" runat="server" AutoPostBack="True" />
                                            <br />
                                            <br />
                                        </div>
                                    </asp:Panel>
                                    <div class="modal-footer" style="border-top: none;">
                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Bağla</button>--%>
                                        <asp:Button ID="BtnUserInfoSave" CssClass="btn btn-default" CommandName="Password" CommandArgument="" OnClick="BtnUserInfoSave_Click" Text="Yadda saxla" runat="server" />
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="permission" style="margin-top: 20px;">
                                    <div class="col-lg-12 col-md-12">
                                        <div class="GrdList">
                                            <asp:GridView ID="GrdPermissions" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px; margin-bottom: 20px;" DataKeyNames="ID">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="№">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Name" HeaderText="Huquqlar">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckPermissions" CssClass="Checkbx" runat="server" />
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
                                    </div>
                                    <div class="modal-footer" style="border-top: none;">
                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Bağla</button>--%>
                                        <asp:Button ID="BtnPermissionsSave" OnClick="BtnPermissionsSave_Click" CssClass="btn btn-default" CommandArgument="" Text="Yadda saxla" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:MultiView ID="MultiViewUsers" runat="server" ActiveViewIndex="1">
        <%--yeni isci elave etmek veya etrafli baxis--%>
        <asp:View ID="ViewEdit" runat="server">

            <asp:Literal ID="LtrTab" Visible="false" runat="server">
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#users-info" aria-controls="users-info" role="tab" data-toggle="tab">Şəxsi məlumatlar</a></li>
                    <li role="presentation"><a href="#password" aria-controls="password" role="tab" data-toggle="tab">Şifrəni dəyiş</a></li>
                </ul>
            </asp:Literal>

            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="users-info" style="margin-top: 20px;">
                    <div class="modal-body row">
                        <%--1-ci hisse--%>
                        <div class="col-lg-4 col-md-4">
                            Qurumun adı:<br />
                            <asp:DropDownList ID="DListOrganizations" CssClass="form-control" Width="100%" Enabled="false" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                            <br />
                            <br />

                            Sənədin növü:<br />
                            <asp:DropDownList ID="DListDocTypes" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                            <br />
                            <br />

                            Sənədin nömrəsi:<br />
                            <asp:TextBox ID="TxtDocNumber" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Fin:<br />
                            <asp:TextBox ID="TxtPin" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Soyadı, adı və atasının adı:<br />
                            <asp:TextBox ID="TxtFullname" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Doğum tarixi:<br />
                            <asp:TextBox ID="TxtBirthDate" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Cins:<br />
                            <asp:DropDownList ID="DListGender" CssClass="form-control" Width="100%" runat="server">
                                <asp:ListItem Value="-1"> -- </asp:ListItem>
                                <asp:ListItem Value="0">Qadın</asp:ListItem>
                                <asp:ListItem Value="1">Kişi</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />

                            Ailə vəziyyəti:<br />
                            <asp:DropDownList ID="DListMarital" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                            <br />
                            <br />

                        </div>
                        <%--2-ci hisse--%>
                        <div class="col-lg-4 col-md-4">
                            Təhsil:<br />
                            <asp:DropDownList ID="DListEducationTypes" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                            <br />
                            <br />

                            Təhsil müəssisəsinin adı:<br />
                            <asp:TextBox ID="TxtEducationPlace" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            İxtisas:<br />
                            <asp:TextBox ID="TxtEducationSpecialty" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Ümumi iş stajı:<br />
                            <asp:TextBox ID="TxtWorkExperience" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Vəzifə:<br />
                            <asp:TextBox ID="TxtPosition" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Mərkəzdə olan iş öhdəlikləri:<br />
                            <asp:TextBox ID="TxtTasks" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Mərkəzdə işə başlama tarixi:<br />
                            <asp:TextBox ID="TxtStartWorkDate" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            İş yerləri haqqında melumat:<br />
                            <asp:TextBox ID="TxtOtherWorks" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>
                        <%--3-cu hisse--%>
                        <div class="col-lg-4 col-md-4">
                            Attestasiyadan keçmə tarixi:<br />
                            <asp:TextBox ID="TxtAttestationDate" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            SSN(Sosial siğorta №):<br />
                            <asp:TextBox ID="TxtSSN" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Əlaqə telefonu:<br />
                            <asp:TextBox ID="TxtContact" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            İştirak edilən təlim və kursların adı:<br />
                            <asp:TextBox ID="TxtTrainingAndCourses" CssClass="form-control" Width="100%" Height="116px" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <br />
                            <br />

                            Hazırkı iş statusu:<br />
                            <asp:DropDownList ID="DListUsersStatus" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                            <br />
                            <br />

                            Qeyd:<br />
                            <asp:TextBox ID="TxtDescription" CssClass="form-control" Width="100%" Height="116px" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>
                        <div class="col-lg-12 col-md-12" style="text-align: right">
                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Bağla</button>--%>
                            <asp:Button ID="BtnCancel" CssClass="btn btn-default" Style="background: #a94442;" Text="Imtina et" runat="server" OnClick="BtnCancel_Click" />
                            <asp:Button ID="BtnAddUser" CssClass="btn btn-default" Text="Əlavə et" runat="server" OnClick="BtnAddUser_Click" />
                        </div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane" id="password" style="margin-top: 20px;">
                    <asp:Panel ID="PnlTabPassword" runat="server" Visible="false">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4">
                                        Köhnə Şifrə:<br />
                                        <asp:TextBox ID="TxtOldPassword" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        <br />
                                        <br />

                                        Yeni Şifrə:<br />
                                        <asp:TextBox ID="TxtNewPassword" CssClass="form-control" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
                                        <br />
                                        <span style="font-size: 8pt">- minimum 4, maksimum 11 simvol</span>
                                        <br />
                                        <br />

                                        Təkrar şifrə:<br />
                                        <asp:TextBox ID="TxtRepeatPassword" CssClass="form-control" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
                                        <br />
                                        <br />

                                        <div class="text-left">
                                            <asp:Button ID="BtnChangePassword" runat="server" CssClass="btn btn-default" CommandArgument="" Text="Yadda saxla" OnClick="BtnChangePassword_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </asp:View>
        <%--esas sehife--%>
        <asp:View ID="ViewMain" runat="server">
            <ul class="ListSearch">
                <asp:Panel ID="PnlFilterOrganizations" Visible="false" runat="server">
                    <li>Qurum:<br />
                        <asp:DropDownList ID="DListFilterOrganizations" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                    </li>
                </asp:Panel>
                <li>Soyadı, adı və atasının adı:<br />
                    <asp:TextBox ID="TxtFilterFullname" CssClass="form-control" runat="server" Width="250px"></asp:TextBox>
                </li>
                <li>Sənəd nömrəsi:<br />
                    <asp:TextBox ID="TxtFilterDocNumber" CssClass="form-control" runat="server" Width="250px"></asp:TextBox>
                </li>
                <li>Pin:<br />
                    <asp:TextBox ID="TxtFilterPin" CssClass="form-control" runat="server" Width="250px"></asp:TextBox>
                </li>
                <li>Sənədin növü:<br />
                    <asp:DropDownList ID="DListFilterDocType" runat="server" CssClass="form-control" Width="250px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </li>

                <li class="NoStyle">&nbsp;
                        <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" />
                </li>
            </ul>
            <br />

            <div class="row">
                <div class="col-md-6">
                    <asp:Panel ID="PnlAddAplicatons" runat="server">
                        <asp:LinkButton ID="LnkAddApp" runat="server" OnClick="LnkAddApp_Click">
                        <img class="alignMiddle" src="/images/add.png" /> YENİ İŞÇİ
                        </asp:LinkButton>
                    </asp:Panel>
                </div>
                <div class="col-md-6 text-right">
                    <asp:Label ID="LblCount" runat="server" Text="Axatarış üzrə nəticə: 123"></asp:Label>
                </div>
            </div>

            <div class="GrdList">
                <asp:GridView ID="GrdUsers" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID">
                    <Columns>

                        <asp:BoundField DataField="ID" HeaderText="№">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DocumentNumber" HeaderText="Sənədin nömrəsi">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PIN" HeaderText="Pin">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Fullname" HeaderText="Soyadı, adı və atasının adı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Birthdate" HeaderText="Doğum tarixi" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Positions" HeaderText="Vəzifəsi">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkSecurity" runat="server" CommandArgument='<%#Eval("ID")%>' Visible='<%#(int)Eval("ID")!=DALC._GetUsersLogin.ID%>' OnClick="LnkSecurity_Click">   
                                     <img src="/images/security_<%#Eval("UsersAccountsIsActive")._ToString()=="True"?"1":"0" %>.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click" runat="server"><img src="/images/edit.png" title="Düzəliş et" /></asp:LinkButton>
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
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />

    <script src="/js/jquery.bootpag.js" type="text/javascript"></script>

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
                window.location.href = '/tools/users/?p=' + num;
            }).find('.pagination');
        }
        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });
    </script>

</asp:Content>

