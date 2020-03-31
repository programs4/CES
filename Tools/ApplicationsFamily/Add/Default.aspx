<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ApplicationsFamily_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <script>
        //MasterPage-de ve burda yeniden cagirmagimin meqsedi elave ozellikler elave etekdir
        $(function () {
            $('.multiSelect').multiselect({
                buttonWidth: '100%',
                numberDisplayed: 5,
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
        <asp:View ID="View1" runat="server">
            <div class="row">
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            Səfər edən əməkdaşlarımız:<br />
                            <asp:ListBox ID="DListUsers" runat="server" CssClass="multiSelect form-control" Style="display: none" SelectionMode="Multiple" data-placeholder=" " DataValueField="ID" DataTextField="Fullname"></asp:ListBox>
                            <br />
                            <br />


                            Səfərin məqsədi:
                            <br />
                            <asp:DropDownList ID="DListApplicationsFamilyTypes" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                            <br />
                            <br />

                            Səfərin statusu:
                            <br />
                            <asp:DropDownList ID="DListApplicationsFamilyStatus" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                            <br />
                            <br />

                            Ünvan:<br />
                            <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <br />

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    Səfərdə iştrak edən tərəftaşlar:<br />
                                    <asp:ListBox ID="DListApplicationsFamilyPartnersTypes" runat="server" CssClass="multiSelect form-control" Style="display: none" SelectionMode="Multiple" data-placeholder=" " DataValueField="ID" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="DListApplicationsFamilyPartnersTypes_SelectedIndexChanged"></asp:ListBox>
                                    <br />
                                    <br />
                                    <asp:Panel ID="PnlPersons" runat="server">
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            Tarix:<br />
                            <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
                            <br />
                            <br />

                            Qeyd:<br />
                            <asp:TextBox ID="TxtDescriptions" runat="server" Height="150px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            <br />
                            <br />

                            <div class="text-right">
                                <asp:Button ID="BtnSave" runat="server" Width="150px" Height="45px" CommandArgument="0" Text="Yadda saxla" CssClass="btn btn-default" OnClick="BtnSave_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-6">
                    <div class="text-justify">
                        <h2>İstifadə Təlimatı</h2>
                        <p>Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan "de Finibus Bonorum et Malorum" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan "Lorem ipsum dolor sit amet" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.</p>
                        <p>1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.</p>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click">            
                <img class="alignMiddle" src="/images/add.png" /> YENİ SƏFƏR
            </asp:LinkButton>
            <br />
            <br />
            <div class="GrdList">
                <asp:GridView ID="GrdApplicationsFamily" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="№">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ApplicationsFamilyTypes" HeaderText="Səfərin növü">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ApplicationsFamilyStatus" HeaderText="Səfərin statusu">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Description" HeaderText="Qeyd">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Tour_Dt" HeaderText="Səfər tarixi" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Add_Dt" HeaderText="Əlavə olunma tarixi" DataFormatString="{0:dd.MM.yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                        </asp:BoundField>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" runat="server" CommandName='<%#Eval("ApplicationsID")%>' CommandArgument='<%#Eval("ID")%>' OnClick="LnkEdit_Click">
                                     <img src="/images/edit.png" title="Düzəliş et" />
                                </asp:LinkButton>                               
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
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

