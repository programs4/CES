<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_EvaluationsSkill_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>
<%@ Register Src="~/Tools/UserControls/HeaderTab.ascx" TagPrefix="uc1" TagName="HeaderTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <div class="modal fade" id="Modal" role="dialog">
        <div class="modal-dialog" style="width: 400px">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">
                                <asp:Literal ID="LtrTitle" runat="server">Yeni qiymətləndirmə</asp:Literal>
                            </h4>
                        </div>
                        <div class="modal-body">
                            Qiymətləndirmə tarixi:
                            <asp:TextBox ID="TxtDate" CssClass="form-control form_datetime" autocomplete="off" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Panel ID="PnlCompleted" Visible="false" runat="server">
                                <asp:DropDownList ID="DListCompleted" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Tamamlandı"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                            </asp:Panel>

                        </div>
                        <asp:Panel ID="PnlModalFooter" runat="server" class="modal-footer">
                            <asp:Button ID="BtnSave" CssClass="btn btn-default" runat="server" Text="Əlavə et" OnClick="BtnSave_Click" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row">
        <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
        <uc1:HeaderTab runat="server" ID="HeaderTab" />

        <div class="col-md-12">
            <div style="margin-top: 20px">
                <asp:LinkButton ID="LnkAdd" OnClick="LnkAdd_Click" runat="server">
                     <img class="alignMiddle" src="/images/add.png" />
                    YENİ
                </asp:LinkButton>
            </div>
            <br />
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdEvaluationsSkill" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID">
                    <Columns>
                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="FullName" HeaderText="İstifadəçi">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Create_Dt" HeaderText="Qiymətləndirmə tarixi" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Statusu">
                            <ItemTemplate>
                                <%# (bool)Eval("IsCompleted")?"Tamamlanıb":"Hazırlanır" %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Add_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy HH:mm:ss}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click" runat="server">
                                      <img src="/images/edit.png" title="Düzəliş et" />
                                </asp:LinkButton>                            
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="/tools/evaluationsskill/add/?i=<%#Config._GetQueryString("i")%>">
                                    <img src="/images/details.png" title="Ətraflı" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                            Qiymətləndirilmə aparılmayıb
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
        </div>
    </div>
</asp:Content>

