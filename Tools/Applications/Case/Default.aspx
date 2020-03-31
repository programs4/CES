<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Applications_Case_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <div class="row">
        <div class="col-md-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Növü:<br />
                    <asp:DropDownList ID="DListApplicationsCaseTypes" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="DListApplicationsCaseTypes_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            Ailə vəziyyəti:<br />
            <asp:DropDownList ID="DListMaritalStatus" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            Yaşayış yeri:<br />
            <asp:DropDownList ID="DListApplicationsCasePlaceTypes" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            Məşğulluq:<br />
            <asp:DropDownList ID="DListApplicationsCaseWork" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            Təhsili:<br />
            <asp:DropDownList ID="DListEducationsTypes" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            İcmada iştirakı:<br />
            <asp:DropDownList ID="DListApplicationsCaseCommunities" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            Ailənin maadi təminatı:<br />
            <asp:DropDownList ID="DListApplicationsCaseFinancial" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />

            Gəlir:<br />
            <asp:DropDownList ID="DListApplicationsCaseFinancialIncome" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />
        </div>

        <div class="col-md-6">
            Təxirə salınmaz hallarda əlaqə saxlanııacaq şəx:<br />
            <asp:TextBox ID="TxtContactsOther" CssClass="form-control" runat="server"></asp:TextBox>
            <br />
            <br />

            Ailə vəziyyəti və tərkibi haqqında qısa məlumat:<br />
            <asp:TextBox ID="TxtFamilyInformation" CssClass="form-control" TextMode="MultiLine" Height="96px" MaxLength="300" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblFamilyInformation" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 300 simvoldan ibarət qısa məlumat qeyd edin"></asp:Label>
            <br />
            <br />

            Problemin təsviri (həll olunması üçün qarşıya qoyulan məqsəd):<br />
            <asp:TextBox ID="TxtProblemInformation" CssClass="form-control" TextMode="MultiLine" Height="96px" MaxLength="300" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblProblemInformation" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 300 simvoldan ibarət qısa məlumat qeyd edin"></asp:Label>
            <br />
            <br />

            Qeyd:<br />
            <asp:TextBox ID="TxtDescription" CssClass="form-control" TextMode="MultiLine" Height="96px" MaxLength="300" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblDescription" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-ən çox 500 simvoldan ibarət qısa məlumat qeyd edin"></asp:Label>
            <br />
            <br />

            Case açılma tarixi:
            <br />
            <asp:TextBox ID="TxtOpeningDt" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblOpeningDt" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-case açılma tarixi gün.ay.il olaraq daxil edilməlidir (nümunə: 25.09.2017)"></asp:Label>
            <br />
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <a href="/tools/services/?i=<%= Config._GetQueryString("i") %>">
                <img class="alignMiddle" src="/images/add.png" />
                XİDMƏT PLANINA ƏLAVƏ VƏ YA DÜZƏLİŞ ET
            </a>
        </div>
        <asp:Panel ID="PnlServices" runat="server" CssClass="col-md-12">
            <asp:GridView ID="GrdAppPersonsServices" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="98%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Şəxsi kod">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Fullname" HeaderText="Vətəndaşın adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ServicesName" HeaderText="Xidmət planı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#Eval("ApplicationsPersonsServicesStatus")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
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
            <br />
            <br />

        </asp:Panel>
        <asp:Panel ID="PnlEvaluations" runat="server" CssClass="col-md-12">
            Qiymətləndirmə:                    
            <asp:GridView ID="GrdEvaluations" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="98%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="ApplicationsPersonsID" HeaderText="Şəxsi kod">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Fullname" HeaderText="Vətəndaşın adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PointSum" HeaderText="Bal">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#(bool)Eval("IsCompleted")?"Tamamlanıb":"Tamamlanmayıb"%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Add_Dt" HeaderText="Tarix" DataFormatString="{0:dd.MM.yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:BoundField>

                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <EmptyDataTemplate>
                    <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                        Qiymətləndirmə yoxdur.
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

        </asp:Panel>
    </div>

    <div class="row">
        <div class="col-md-6">
            Statusu:<br />
            <asp:DropDownList ID="DListApplicationsCaseStatus" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />
        </div>
        <div class="col-md-6">
            Case bağlanma tarixi:
            <br />
            <asp:TextBox ID="TxtClosingDt" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblClosingDt" runat="server" Font-Size="8pt" ForeColor="Silver" Text="-case balanma tarixi gün.ay.il olaraq daxil edilməlidir (nümunə: 25.09.2017)"></asp:Label>
            <br />
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="LblTextUsers" Font-Size="12pt" Text="Sosial işçi:" runat="server"></asp:Label>
            <asp:Label ID="LblUsersFullName" Font-Bold="true" Font-Size="12pt" Text="" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 text-right">
            <asp:Button ID="BtnCase" runat="server" Width="170" Height="80px" CssClass="btn btn-default" Text="Yadda saxla" OnClick="BtnCase_Click" />
        </div>
    </div>
</asp:Content>

