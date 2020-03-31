<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_SIB_R_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderTab.ascx" TagPrefix="uc1" TagName="HeaderTab" %>
<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
        <uc1:HeaderTab runat="server" ID="HeaderTab" />
        <div class="col-md-12">
            <div style="margin-top: 20px">
                <a href="<%=string.Format("/tools/sib-r/add/?i={0}&id={1}",Config._GetQueryString("i"),"0".Encrypt()) %>">
                    <img class="alignMiddle" src="/images/add.png" />
                    YENİ
                </a>
            </div>
            <br />
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdSIBR" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID">
                    <Columns>
                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>

                         <asp:BoundField DataField="SIBRTypesName" HeaderText="Növü">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
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
                                <asp:LinkButton ID="LnkDelete" CommandArgument='<%#Eval("ID")%>' OnClick="LnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?')" runat="server"><img src="/images/delete.png" title="Sil" /></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="/tools/sib-r/result/?i=<%# string.Format(Config._GetQueryString("i")+"&id={0}",Eval("ID")._ToString().Encrypt()) %>">
                                    <img src="/images/edit.png" title="Düzəliş et" />
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

