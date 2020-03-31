<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Services_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <script> 
        $(document).ready(function () {
            EnterKey();
        });

        function EnterKey() {
            $(".description").keypress(function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

            <asp:Panel ID="PnlServices" runat="server" CssClass="row">

                <div class="col-md-2">
                    Xidmət:
                    <br />
                    <asp:DropDownList ID="DListServices" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="DListServices_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                </div>

                <asp:Panel ID="PnlSubServices1" runat="server" Visible="false" CssClass="col-md-2">
                    Növü:
                    <br />
                    <asp:DropDownList ID="DListSubServices1" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="DListServices_SelectedIndexChanged"></asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="PnlSubServices2" runat="server" Visible="false" CssClass="col-md-2">
                    Alt növ:
                    <br />
                    <asp:ListBox ID="DListSubServices2" runat="server" CssClass="multiSelect form-control" Style="display: none" SelectionMode="Multiple" data-placeholder=" " DataValueField="ID" DataTextField="Name"></asp:ListBox>
                </asp:Panel>

                <asp:Panel ID="PnlFirstApplication" runat="server" Visible="false" CssClass="col-md-2">
                    İlkin müraciət:
                    <br />
                    <asp:DropDownList ID="DListFirstApplication" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="İlkin müraciət səbəbi" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="PnlCreateDt" Visible="false" CssClass="col-md-2" runat="server">
                    Tarix:
                    <br />
                    <asp:TextBox ID="TxtCreateDt" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </asp:Panel>

                <asp:Panel ID="PnlBtnSave" runat="server" Visible="false" CssClass="col-md-2">
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="Əlavə et" CssClass="btn btn-default" Width="111px" OnClick="BtnSave_Click" />
                </asp:Panel>

            </asp:Panel>

            <div class="row">
                <div class="col-md-12">
                    <div class="GrdList">
                        <asp:GridView ID="GrdServices" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID,ApplicationsPersonsServicesStatusID,Description" OnRowDataBound="GrdServices_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="S/s">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Xidmətin adı">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTopServicesName" runat="server" ForeColor="#c0c0c0" Font-Size="10pt" Text='<%# (!string.IsNullOrEmpty(Eval("TopServicesName").ToString()))?Eval("TopServicesName")+"<br/>":"" %>'></asp:Label>
                                        <%#Eval("ServicesName") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="İlkin müraciət səbəbi">
                                    <ItemTemplate>
                                        <%#(bool)Eval("IsFirstApplication")==true?"Bəli":"Xeyr" %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qeyd">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control description" TextMode="MultiLine" Height="35px" AutoPostBack="true" OnTextChanged="TxtDescription_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nəticəsi">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DListServicesStatus" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="DListServicesStatus_SelectedIndexChanged"></asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Create_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
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
                                        <asp:HyperLink ID="HypUseHistory" Visible='<%#(bool)Eval("IsUseHistory")==true %>' NavigateUrl='<%#string.Format("/tools/services/usehistory/?i={0}",Cryptography.Encrypt(Eval("ID")+"-"+Eval("ApplicationsPersonsID")+"-"+DALC._GetUsersLogin.Key)) %>' runat="server"> 
                                            <img src="/images/davamiyyet.png" title="Davamiyyət" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataTemplate>
                                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                                    Xidmət əlavə olunmayıb
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

