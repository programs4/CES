<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Evaluations_Add_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
    <div class="row">
        <asp:Panel ID="PnlQuestions" CssClass="col-md-6" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HdnEvaluations" Value="0" runat="server" />
                    <asp:Repeater ID="RptQuestions" runat="server">
                        <ItemTemplate>
                            <div class="list-group evaluations">
                                <div class="list-group-item question">
                                    <asp:Literal ID="LtrQuestions" runat="server" Text='<%#Eval("Text") %>'></asp:Literal>
                                    <asp:HiddenField ID="HdnParentID" runat="server" Value='<%#Eval("ID")%>' />
                                    <asp:HiddenField ID="EvaluationsPointsID" runat="server" Value='<%#Eval("EvaluationsPointsID")._ToString()%>' />
                                </div>
                                <asp:Repeater ID="RptAnswer" runat="server" OnItemDataBound="RptAnswer_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkAnswer" runat="server" CommandName='<%#Eval("ParentID")%>' CommandArgument='<%#Eval("Point")%>' OnClick="LnkAnswer_Click"><span class="badge"><%#Eval("Point")%></span> <%#Eval("Text") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="desc">
                                    <asp:TextBox placeholder="Balın verilmə səbəbi" ID="TxtDescription" runat="server" CssClass="form-control" Height="60px" TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="TxtDescription_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="PnlCompleted" runat="server">
                <div class="alert alert-warning" role="alert">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="Qiymətləndirməni bitirdikdən sonra düzəliş etmə hüququnuz olmayacaq"></asp:Label>
                </div>
                <div class="text-right">
                    <asp:Button ID="BtnProcessEnds" Width="180px" runat="server" CssClass="btn btn-default" Text="Qiymətləndirməni bitir" OnClick="BtnProcessEnds_Click" />
                </div>
            </asp:Panel>
        </asp:Panel>
        <div class="col-md-6">
            <div class="text-justify">
                <h2>İstifadə Təlimatı</h2>
                <p>Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan "de Finibus Bonorum et Malorum" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan "Lorem ipsum dolor sit amet" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.</p>
                <p>1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.</p>
            </div>
        </div>
    </div>
</asp:Content>

