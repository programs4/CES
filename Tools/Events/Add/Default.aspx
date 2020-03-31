<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Events_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" href="/css/lightbox.css" type="text/css" media="screen" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-6">
            Təlim/Tədbir:<br />
            <asp:DropDownList ID="DListEventsTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            <br />
            <br />

            Təlim/Tədbirin keçirildiyi mərkəz:<br />
            <asp:DropDownList ID="DListOrganizations" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            <br />
            <br />

            İştiralk növü:<br />
            <asp:DropDownList ID="DListEventsDirectionTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            <br />
            <br />

            Tipi:<br />
            <asp:DropDownList ID="DListEventsPolicyTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
            <br />
            <br />

            Adı:<br />
            <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            Mövzusu:<br />
            <asp:TextBox ID="TxtSubject" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            Keçirildiyi yer:<br />
            <asp:TextBox ID="TxtPlace" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            Təşkilatçı:<br />
            <asp:TextBox ID="TxtOrganizer" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            İştirakçı sayı:<br />
            <asp:TextBox ID="TxtMemberCount" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <br />

            Keçirildiyi tarix (başlanğıc):<br />
            <asp:TextBox ID="TxtEvents_StartDt" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
            <br />
            <br />

            Keçirildiyi tarix (son):<br />
            <asp:TextBox ID="TxtEvents_EndDt" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
            <br />
            <br />

            Şəkillər:<br />
            <asp:FileUpload ID="FlUpImages" AllowMultiple="true" CssClass="form-control" runat="server" />
            <br />
            <br />

            Qeyd:<br />
            <asp:TextBox ID="TxtDescription" runat="server" Height="96px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />

            <div class="text-right">
                <asp:Button ID="BtnSave" runat="server" Width="150px" Height="45px" CommandArgument="0" Text="Yadda saxla" CssClass="btn btn-default" OnClick="BtnSave_Click" />
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

    <section class="allgallery">
        <div class="allgallery-holder">
            <div class="row">
                <div class="col-md-12">
                    <asp:Repeater ID="RptFotoGallery" runat="server">
                        <ItemTemplate>
                            <div class="gallery-item gallery-item-events">
                                <a data-fancybox="gallery-photo" href='<%#Config.UploadsImagePath("events/original", Eval("Data_Dt"), Eval("FileName"), Eval("FileType")) %>'>
                                    <img class="gallery-img imgBorder" src='<%#Config.UploadsImagePath("events/small", Eval("Data_Dt"), Eval("FileName"), Eval("FileType")) %>' />
                                </a>
                                <div class="item-overlay">
                                    <img src="/images/zoom.png" alt="Alternate Text" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

