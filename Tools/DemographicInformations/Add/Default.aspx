<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_DemographicInformations_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">

    <script type="text/javascript" lang="js">

        $(document).ready(function () {
            $(".selectAll").focus(function () { $(this).select(); });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-6">

            <asp:Repeater ID="RptDemographicInformationsTypes" runat="server">
                <ItemTemplate>
                    <%#Eval("DemographicInformationsTypesName") %>:<br />
                    <asp:TextBox ID="TxtName" runat="server" Text='<%#Eval("Count")%>' data-typeId='<%#Eval("DemographicInformationsTypesID")%>' TextMode="Number" CssClass="selectAll form-control"></asp:TextBox>
                    <br />
                    <br />
                </ItemTemplate>
            </asp:Repeater>

            Tarix:<br />
            <asp:TextBox ID="TxtCreate_Dt" runat="server" CssClass="form_datetime form-control"></asp:TextBox>
            <br />
            <br />

            Qeyd:<br />
            <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Height="80px" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            <div class="text-right">
                <asp:Button ID="BtnSave" runat="server" Width="120px" Height="45px" CommandArgument="0" Text="Yadda Saxla" CssClass="btn btn-default" OnClick="BtnSave_Click" />
                <br />
                <br />
            </div>

        </div>
        <div class="col-md-6">
            <div class="text-justify">
                <h2>İstifadə Təlimatı</h2>
                <p>Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan "de Finibus Bonorum et Malorum" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan "Lorem ipsum dolor sit amet" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.</p>
                <p>1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.</p>
            </div>
        </div>
    </div>
</asp:Content>

