<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Services_UseHistory_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>


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
                    <h4 class="modal-title">
                        <asp:Literal ID="LtrTitle" runat="server"></asp:Literal>
                    </h4>
                </div>
                <div class="modal-body">
                    Xidmət göstərən:<br />
                    <asp:DropDownList ID="DListUsers" CssClass="form-control" Width="100%" DataTextField="Fullname" DataValueField="ID" runat="server"></asp:DropDownList>
                    <br />
                    <br />

                    Qeyd:<br />
                    <asp:TextBox ID="TxtDescription" CssClass="form-control" Width="100%" Height="115px" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <br />
                    <br />

                    Tarix:<br />
                    <asp:TextBox ID="TxtUseDate" CssClass="form-control form_datetime" Width="100%" runat="server"></asp:TextBox>
                    <br />
                    <br />

                    <div class="modal-footer">
                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-default" Text="Əlavə et" CommandArgument="0" OnClick="BtnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
    <asp:LinkButton ID="LnkAddUsehistory" OnClick="LnkAddUsehistory_Click" runat="server">
            <img class="alignMiddle" src="/images/add.png" /> YENİ ƏLAVƏ
    </asp:LinkButton>
    <br />
    <br />

    <div class="row">
        <div class="col-md-12">
            <div class="GrdList">
                <asp:GridView ID="GrdServicesUseHistory" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                    <Columns>
                        <asp:TemplateField HeaderText="S/s">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Fullname" HeaderText="Xidmət göstərən">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Description" HeaderText="Qeyd">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Use_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDelete" CommandArgument='<%#Eval("ID")%>' OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?')" runat="server" OnClick="LnkDelete_Click"><img src="/images/delete.png" title="Sil" /></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID")%>' runat="server" OnClick="LnkEdit_Click"><img src="/images/edit.png" title="Düzəliş et" /></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <EmptyDataTemplate>
                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                            Məlumat yoxdur
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

