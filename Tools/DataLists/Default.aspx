<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_DataLists_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-3">
                    Sorağçanın növü:
                    <br />
                    <asp:DropDownList ID="DListDataListsType" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="TableName" DataTextField="Name" OnSelectedIndexChanged="DListDataListsType_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                </div>
                <div class="col-md-3">
                    Sorağçanın adı:<br />
                    <asp:TextBox ID="TxtDataListsName" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="Əlavə et" CommandArgument="" CssClass="btn btn-default" Width="111px" OnClick="BtnSave_Click" />
                </div>
                <br />
                <br />

                <div class="col-lg-12 col-md-12">
                    <div class="GrdList">
                        <asp:GridView ID="GrdDataLists" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="98%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px; margin-bottom: 20px;" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="№">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Name" HeaderText="Sorağçalar">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Deaktiv">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckIsActive" Checked='<%#!(bool)Eval("IsActive")%>' data-id='<%#Eval("ID")%>' CssClass="Checkbx" OnCheckedChanged="CheckIsActive_CheckedChanged" AutoPostBack="true" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

