<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ElectronicRegistry_ServicesPlans_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-2">
            Xidmət növü:<br />
            <asp:DropDownList ID="DListServicesPlansTypes" CssClass="form-control" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            Saatı:<br />
            <asp:TextBox ID="TxtHours" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
        </div>
          <div class="col-md-2">
            Sayı:<br />
            <asp:TextBox ID="TxtCount" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2">
            Sıralama:<br />
            <asp:TextBox ID="TxtPriority" CssClass="form-control" TextMode="Number"  runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2">
            Statusu:<br />
            <asp:DropDownList ID="DListStatus" CssClass="form-control" runat="server">
                <asp:ListItem Value="1" Text="Aktiv"></asp:ListItem>
                <asp:ListItem Value="0" Text="Daktiv"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-12">          
            <br />
            Mövzu:<br />
            <asp:TextBox ID="TxtName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-default" Text="Əlavə et" OnClick="BtnAdd_Click" />
        </div>
    </div>
    <br />
    <div class="GrdList">
        <asp:GridView ID="GrdServicesPlans" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
            <Columns>
                <asp:TemplateField HeaderText="Say">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="ServicesName" HeaderText="Xidmət">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="ServicesPlansTypesName" HeaderText="Xidmətin Növü">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="Name" HeaderText="Mövzu">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="Hours" HeaderText="Saatı">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                 <asp:BoundField DataField="Count" HeaderText="Sayı">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Priority" HeaderText="S/s">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>

                <asp:BoundField DataField="Update_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="<%# string.Format("/tools/electronicregistry/servicesplans/add/?i={0}&type=edit",(Eval("ServicesID")+"-"+Eval("ID")+"-"+DALC._GetUsersLogin.Key).Encrypt()) %>">
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

</asp:Content>

