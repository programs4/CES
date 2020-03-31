<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Applications_Details_Default" %>

<%@ Register Src="~/Tools/UserControls/Operations.ascx" TagPrefix="uc1" TagName="Operations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                                        <asp:View ID="View1" runat="server">
                                            Statusu:
                                            <br />
                                            <asp:DropDownList ID="DListPersonsTypes" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                            <br />
                                            <br />

                                            <div class="row">
                                                <div class="col-md-6">
                                                    Sənədin növü:
                                                    <br />
                                                    <asp:DropDownList ID="DListDocumentTypes" CssClass="form-control" Width="100%" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="DListDocumentTypes_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                    <br />
                                                    <asp:Label ID="LblDocumentTypes" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-sənədin növü həmçinin sənədin seriasını bildirir"></asp:Label>
                                                </div>
                                                <asp:Panel ID="PnlDocumentNumber" CssClass="col-md-6" runat="server" >
                                                    Sənədin nömrəsi:
                                                    <br />
                                                    <asp:TextBox ID="TxtDocumentNumber" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="LblDocumentNumber" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-yalnız sənədin nömrəsini qeyd edin"></asp:Label>
                                                </asp:Panel>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-4">
                                                    Soyadı:
                                                    <br />
                                                    <asp:TextBox ID="TxtSurname" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    Adı:
                                                    <br />
                                                    <asp:TextBox ID="TxtName" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    Atasının adı:
                                                    <br />
                                                    <asp:TextBox ID="TxtPatronymic" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />

                                            Doğum tarixi:
                                            <br />
                                            <asp:TextBox ID="TxtBirthDate" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="LblBirthDate" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-doğum tarixi gün.ay.il olaraq daxil edilməlidir (nümunə: 25.09.1989)"></asp:Label>
                                            <br />
                                            <br />

                                            Cinsi:<br />
                                            <asp:DropDownList ID="DListGender" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Kişi"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Qadın"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            Əlaqə vasitələri:
                                            <br />
                                            <asp:TextBox ID="TxtContacts" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="LblContacts" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 200 simvoldan ibarət telefon nömrələri və ya e-mail qeyd oluna bilər"></asp:Label>
                                            <br />
                                            <br />

                                            Qeyd:
                                            <br />
                                            <asp:TextBox ID="TxtDescription" CssClass="form-control" Width="100%" Height="70px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="LblDescription" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 500 simvoldan ibarət qeyd oluna bilər"></asp:Label>
                                            <br />
                                            <br />

                                            <div class="modal-footer">
                                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">Bağla</button>--%>
                                                <asp:Button ID="BtnAddPersons" CssClass="btn btn-default" Text="Əlavə et" OnClientClick="this.style.display='none';document.getElementById('loading').style.display=''" OnClick="BtnAddPersons_Click" runat="server" />
                                                <img id="loading" src="/images/loading.gif" style="display: none" />
                                            </div>
                                        </asp:View>
                                        <asp:View ID="View2" runat="server">
                                            <uc1:Operations runat="server" ID="Operations" />
                                        </asp:View>
                                    </asp:MultiView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PnlAddAplicatons" runat="server">
                <asp:LinkButton ID="LnkAddPersons" runat="server" OnClick="LnkAddPersons_Click">
                    <img class="alignMiddle" src="/images/add.png" /> YENİ ƏLAVƏ
                </asp:LinkButton>
            </asp:Panel>

            <div class="GrdList">
                <asp:GridView ID="GrdApplicationsDetails" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="98%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID" OnRowDataBound="GrdApplicationsDetails_RowDataBound">
                    <Columns>

                        <asp:BoundField DataField="ID" HeaderText="Şəxsi kod">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("EvaluationsID")._ToString())?"/images/GridIcons/Eval_On.png":"/images/GridIcons/Eval_Off.png"%>' ToolTip="Qimətləndirilmə aparılıb" />
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsPersonsServicesID")._ToString())?"/images/GridIcons/Service_On.png":"/images/GridIcons/Service_Off.png"%>' ToolTip="Xidmətdən istifadə edir" />
                                <asp:Image ID="Image3" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsFamilyID")._ToString())?"/images/GridIcons/Family_On.png":"/images/GridIcons/Family_Off.png"%>' ToolTip="Ailə səfəri edilib" />
                                <asp:Image ID="Image4" runat="server" ImageUrl='<%#!string.IsNullOrEmpty(Eval("ApplicationsCaseID")._ToString())?"/images/GridIcons/Case_On.png":"/images/GridIcons/Case_Off.png"%>' ToolTip="CASE açılıb" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                        </asp:TemplateField>


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

                        <asp:BoundField DataField="Add_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Vətəndaşın statusu">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="DListPersonsStatus" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelect form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="DListPersonsStatus_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDelete" CommandArgument='<%#Eval("ID")%>' OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?')" OnClick="LnkDelete_Click" runat="server"><img src="/images/delete.png" title="Sil" /></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkOperations" CommandArgument='<%#Eval("ID") %>' CommandName='<%#Eval("ApplicationsID") %>' OnClick="LnkOperations_Click" runat="server"><img src="/images/operations.png" title="Əməliyyatlar" /></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

